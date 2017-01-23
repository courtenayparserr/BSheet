using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Saastrack.DAL.Operations;

namespace SaasTrackProcessTransactions
{
    public class ProcessHelper
    {
        //month/year ranges
        private static decimal monthMin = 30m - 5m;
        private const decimal monthMax = 30m + 5m;
        private const decimal monthMinLoop = 30m - 2m;
        private const decimal yearMin = 365m - 12m;
        private const decimal yearMax = 365m + 12m;
        //columns to use from original file; 'dateCol', 'nameCol' are added
        private const int amtCol = 1;
        private const int dateCol = 0;
        private const int nameCol = 2;
        private const int rowKeyCol = 3;
        
        private static decimal rate; //precision

        //private static readonly string[] disclNames = AccountOperations.GetDiscludedWords().ToArray();        


        public static List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>> GetGroupsOfViableTransactions(DataTable dt, string[] disclNames, string[] inclNames)
        {
            List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>> listOfLists = new List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>>();            
            var rate = 0.5m; 
            
            var resArr = new List<int>();
            var listRes = new List<int[]>();
            var iter1 = -1;
            foreach (DataRow dataRow in dt.Rows)
            //I put this algorithm here to make a better debugging if there are any errors
            {
                iter1++;
                resArr.Clear();
                var iter2 = -1;
                bool toInclude = false;
                var name1 = dataRow[nameCol].ToString();
                //
                if (!nameIncCheck(dataRow[nameCol].ToString(), disclNames)) continue;
                if (!nameIncCheck(dataRow[nameCol].ToString(), inclNames)) toInclude = true;
                int toIncludeIndex = nameIncCheckIndex(dataRow[nameCol].ToString(), inclNames);
                decimal maxTaniCoeff = 0;
                foreach (DataRow dataRowCheck in dt.Rows)
                //we use it to understand to which max comparison coeff should we look for
                {
                    iter2++;
                    var name2 = dataRowCheck[nameCol].ToString();
                    if ((TaniAlgo(name1, name2) >= maxTaniCoeff) &&
                        (iter1 != iter2))
                        maxTaniCoeff = TaniAlgo(name1, name2);
                }
                //
                if (maxTaniCoeff < rate && !toInclude) continue;
                resArr.Add(iter1);
                iter2 = -1;
                foreach (DataRow dataRowCheck in dt.Rows)
                {
                    iter2++;
                    if (iter2 == iter1) continue;//here taniCoeff will be = 1
                    if (!nameIncCheck(dataRowCheck[nameCol].ToString(), disclNames)) continue;
                    var name2 = dataRowCheck[nameCol].ToString();
                    //
                    var taniCoeff = TaniAlgo(name1, name2);
                    if (taniCoeff < maxTaniCoeff - (1 - rate) / 7)
                        continue;
                    //I put here (1 - rate)/7 for some variation; remove it or make '/7' bigger for better comparison
                    if (nameIncCheckIndex(name2, inclNames) != toIncludeIndex) continue;
                    if (toInclude && nameIncCheckIndex(name2, inclNames)==toIncludeIndex)
                    {
                        resArr.Add(iter2);
                        continue;
                    }
                    resArr.Add(iter2);
                }
                if (resArr.Count <= 1) continue;
                resArr.Sort();
                if (getMaxDays(resArr, dt, false) && !toInclude) //date periods checking (everywhere there is a month or year periods)
                    continue;
                listRes.Add(resArr.ToArray());
            }
            //check for loops
            iter1 = -1;
            foreach (DataRow dataRow in dt.Rows)
            //I put this algorithm here to make a better debugging if there are any errors
            {
                iter1++;
                if (checkIterInList(iter1, listRes)) continue;
                resArr.Clear();
                var date1 = DateTime.Parse(dataRow[dateCol].ToString());
                var iter2 = -1;
                bool toInclude = false;
                var name1 = dataRow[nameCol].ToString();
                //
                if (!nameIncCheck(dataRow[nameCol].ToString(), disclNames)) continue;
                if (!nameIncCheck(dataRow[nameCol].ToString(), inclNames)) toInclude = true;
                int toIncludeIndex = nameIncCheckIndex(dataRow[nameCol].ToString(), inclNames);
                decimal maxTaniCoeff = 0;
                foreach (DataRow dataRowCheck in dt.Rows)
                //we use it to understand to which max comparison coeff should we look for
                {
                    iter2++;
                    var name2 = dataRowCheck[nameCol].ToString();
                    if ((TaniAlgo(name1, name2) >= maxTaniCoeff) &&
                        (iter1 != iter2))
                        maxTaniCoeff = TaniAlgo(name1, name2);
                }
                //
                if (maxTaniCoeff < rate) continue;
                resArr.Add(iter1);
                iter2 = -1;
                foreach (DataRow dataRowCheck in dt.Rows)
                {
                    iter2++;
                    if (iter2 == iter1) continue;//here taniCoeff will be = 1
                    if (!nameIncCheck(dataRowCheck[nameCol].ToString(), disclNames)) continue;
                    var date2 = DateTime.Parse(dataRowCheck[dateCol].ToString());
                    var name2 = dataRowCheck[nameCol].ToString();
                    //
                    var taniCoeff = TaniAlgo(name1, name2);
                    if (taniCoeff < maxTaniCoeff - (1 - rate) / 7)
                        continue;
                    //I put here (1 - rate)/7 for some variation; remove it or make '/7' bigger for better comparison
                    if (Math.Abs((date2 - date1).TotalDays) <= Convert.ToDouble(monthMin)) continue;
                    if (nameIncCheckIndex(name2, inclNames) != toIncludeIndex) continue;
                    resArr.Add(iter2);
                    date1 = date2;
                }
                if (resArr.Count <= 1) continue;
                resArr.Sort();
                if (getMaxDays(resArr, dt, true)) //date periods checking (everywhere there is a month or year periods)
                    continue;
                listRes.Add(resArr.ToArray());
            }
            listRes = removeDup(listRes); //removing full duplicates
            listRes = removeHighVar(listRes, dt, inclNames);
            listRes = removeIntersect(listRes, dt, inclNames); //removing intersects (one group may be combined with other)
            return resToCsv(listRes, dt);            
        }

        
        private static List<int[]> removeHighVar(List<int[]> listRes, DataTable dt, string[] inclNames)
        {
            List<decimal> pricesArr = new List<decimal>();
            for (var i = 0; i < listRes.Count; i++)
            {
                bool toInclude = false;
                pricesArr.Clear();
                var check = false;
                if ((listRes[i] == null) || (listRes[i].Length == 2)) continue;
                for (var j = 0; j < listRes[i].Length; j++)
                {
                    pricesArr.Add(Convert.ToDecimal(dt.Rows[listRes[i][j]][1]));
                    if (!nameIncCheck(dt.Rows[listRes[i][j]][2].ToString(), inclNames)) toInclude = true;
                }
                if (toInclude) continue;
                decimal average = pricesArr.Average();
                decimal sumOfSquaresOfDifferences = pricesArr.Select(val => (val - average) * (val - average)).Sum();
                decimal sd = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(sumOfSquaresOfDifferences / pricesArr.Count)));
                if (Math.Abs(sd/average)>(1-rate)*0.2m) listRes[i] = null;
            }
            return listRes;
        }

        private static bool checkIterInList(int iter1, List<int[]> listRes)
        {
            for (int i = 0; i < listRes.Count; i++)
            {
                if (listRes[i].Contains(iter1)) return true;
            }
            return false;
        }

        private static List<int[]> removeIntersect(List<int[]> listRes, DataTable dt, string[] inclNames)
        {
            var tempArr = new List<int>();
            var jRem = 0;
            for (var i = 0; i < listRes.Count; i++)
            {
                var check = false;
                if (listRes[i] == null) continue;
                for (var j = 0; j < listRes[i].Length; j++)
                {
                    if (listRes[j] == null) continue;
                    jRem = checkThatContains(listRes, listRes[i][j], i); //is this element is included in other groups?
                    if (jRem == -1) continue;
                    check = true;
                    break;
                }
                if (!check) continue;
                tempArr.Clear();
                //union 2 groups with the same element
                for (var ii = 0; ii < listRes[i].Length; ii++) tempArr.Add(listRes[i][ii]);
                for (var jj = 0; jj < listRes[jRem].Length; jj++) tempArr.Add(listRes[jRem][jj]);
                tempArr.Sort();
                tempArr = tempArr.Distinct().ToList();
                //
                listRes[i] = null;
                listRes[jRem] = null;
                //checking that the element must be included and we have not checked it
                bool checkInc = false;
                for (var ii = 0; ii < tempArr.Count; ii++)
                {
                    if (!nameIncCheck(dt.Rows[tempArr[ii]][nameCol].ToString(), inclNames))
                    {
                        checkInc = true;
                        break;
                    }
                }
                //
                if (getMaxDays(tempArr, dt, false) && !checkInc) continue; //checking that after union our date periods are ok
                listRes[i] = tempArr.ToArray();
            }
            return listRes;
        }

        private static int checkThatContains(IReadOnlyList<int[]> listRes, int el, int iExc)
        //when we get row number from one group we check that this row is not included in other groups
        {
            var res = -1;
            for (var i = 0; i < listRes.Count; i++)
            {
                if (i == iExc) continue;
                if (listRes[i] == null) continue;
                for (var j = 0; j < listRes[i].Length; j++)
                {
                    if (el != listRes[i][j]) continue;
                    res = i;
                    return res;
                }
            }
            return res;
        }

        private static bool getMaxDays(IReadOnlyList<int> resArr, DataTable dt, bool loop)
        {
            if (loop) monthMin = monthMinLoop;
            var mnth = false;
            var yr = false;
            List<DateTime> dateTime = new List<DateTime>();
            for (int i = 0; i < resArr.Count; i++)
            {
                dateTime.Add(DateTime.Parse(dt.Rows[resArr[i]][dateCol].ToString()));
            }
            dateTime=dateTime.OrderByDescending(x => x.Ticks).ToList();
            //checking the first period: is it a month or a year?
            var date1 = dateTime[0];
            var date2 = dateTime[1];
            var dateNum = Convert.ToDecimal(Math.Abs((date1 - date2).TotalDays));
            if ((dateNum > monthMin) && (dateNum < monthMax)) mnth = true;
            if ((dateNum > yearMin) && (dateNum < yearMax)) yr = true;
            //
            for (var i = 1; i < resArr.Count; i++)
            {
                date1 = dateTime[i-1];
                date2 = dateTime[i];
                dateNum = Convert.ToDecimal(Math.Abs((date1 - date2).TotalDays));
                if (!(((dateNum > monthMin) && (dateNum < monthMax) && mnth) ||
                    ((dateNum > yearMin) && (dateNum < yearMax) && yr)))
                    return true;
            }
            return false;
        }

        private static bool nameIncCheck(string row, string[] Names) => Names.All(t => !row.ToLower().Contains(t.ToLower()));

        private static int nameIncCheckIndex(string row, string[] Names)
        {
            for (int i = 0; i < Names.Length; i++)
            {
                if (row.ToLower().Contains(Names[i].ToLower())) return i;
            }
            return -1;
        }
        
       private static decimal TaniAlgo(string s1, string s2)
        {
            var listS1 = s1.Split(' ');
            var listS2 = s2.Split(' ');
            for (var i = 0; i < listS1.Length; i++)
            {
                //here we are cleaning name from:
                if (isDigit(listS1[i])) listS1[i] = ""; //words from all numbers only (number of transaction e.g.)
                if (isSep(listS1[i])) listS1[i] = ""; //date (12/09)
                try
                {
                    if (isCardName(listS1[i])) 
                        listS1[i] = ""; //'V1234'
                }
                catch(Exception Exception){

                }
            }
            for (var i = 0; i < listS2.Length; i++)
            {
                if (isDigit(listS2[i])) listS2[i] = "";
                if (isSep(listS2[i])) listS2[i] = "";
                try
                {
                    if (isCardName(listS2[i])) 
                        listS2[i] = ""; //'V1234'
                }
                catch(Exception Exception){

                }
            }
            listS1 = listS1.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            listS2 = listS2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //
            decimal lenS1 = listS1.Length;
            decimal lenS2 = listS2.Length;
            decimal c = 0;
            for (var i = 0; i < lenS1; i++)
            {
                for (var j = 0; j < lenS2; j++)
                {
                    if (listS1[i] == listS2[j]) c++;
                }
            }
            decimal res;
            try
            {
                res = c / (lenS1 + lenS2 - c);
            }
            catch
            {
                if ((lenS1 != 0) && (lenS2 != 0)) res = 0;
                else res = 1;
            }
            return res;
        }

        private static bool isDigit(string s) => s.All(c => (c >= '0') && (c <= '9'));

        private static bool isSep(string s) => s.Any(c => c == '/');

        private static bool isCardName(string s) => (s.Length > 0) && (s[0] == 'V') && (s[1] > '0') && (s[1] < '9');

        private static List<int[]> removeDup(List<int[]> listRes)
        {
            for (var i = 0; i < listRes.Count; i++)
            {
                for (var j = 0; j < listRes.Count; j++)
                {
                    if ((i == j) || (listRes[i] == null) || (listRes[j] == null)) continue;
                    if (listRes[j].SequenceEqual(listRes[i])) listRes[j] = null;
                }
                if ((listRes[i] != null) && (listRes[i].Length == 1)) listRes[i] = null;
            }
            return listRes;
        }

        private static List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>> resToCsv(IReadOnlyList<int[]> listRes, DataTable dt)
        {
            List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>> listOfLists = new List<List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>>();            
            var filePath = Environment.CurrentDirectory + "\\results.csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            var length = listRes.Count;

            using (TextWriter writer = File.CreateText(filePath))
            {
                for (var index = 0; index < length; index++)
                {
                    List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity> listOfEntity = new List<Saastrack.DAL.Operations.TransactionOperations.QuoteEntity>();
                    if (listRes[index] != null)
                    {
                        for (var len = 0; len < listRes[index].Length; len++)
                        {
                            //check if any amounts are negative
                            Saastrack.DAL.Operations.TransactionOperations.QuoteEntity qe = new Saastrack.DAL.Operations.TransactionOperations.QuoteEntity();
                            qe.amount = Convert.ToDouble(dt.Rows[listRes[index][len]][amtCol]);
                            qe.date = Convert.ToDateTime(dt.Rows[listRes[index][len]][dateCol]);
                            qe.name = dt.Rows[listRes[index][len]][nameCol].ToString();
                            qe.RowKey = dt.Rows[listRes[index][len]][rowKeyCol].ToString();

                            var temp = dt.Rows[listRes[index][len]][amtCol] + " ||| " +
                                       dt.Rows[listRes[index][len]][dateCol] +
                                       " ||| " + dt.Rows[listRes[index][len]][nameCol];
                            writer.WriteLine(temp);

                            listOfEntity.Add(qe);
                        }
                        listOfLists.Add(listOfEntity);
                    }
                }                
            }

            return listOfLists;
        }
    }
}
