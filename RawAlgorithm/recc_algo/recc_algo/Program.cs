using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace recc_algo
{
    internal static class Program
    {
        private static readonly Dictionary<string, string> monthNum = new Dictionary<string, string>();
        private static decimal rate; //precision
        private static readonly string[] restrNames = {"MCDONALD", "HURRICAN"};
        private const decimal monthMin = 30m - 5m;
        private const decimal monthMax = 30m + 5m;
        private const decimal yearMin = 365m - 12m;
        private const decimal yearMax = 365m + 12m;

        private static void Main()
        {
            Console.Write("Input rate >> ");
            try
            {
                rate = decimal.Parse(Console.ReadLine());
            }
            catch
            {
                rate = 0.5m;
            }
            var dt = dataPreparation(GetDataTableFromCsv(Environment.CurrentDirectory + "\\otheraccount2.csv", false));
            //
            var arrOfIndexes = new List<decimal>();
            var resArr = new List<int>();
            var listRes = new List<int[]>();
            var iter1 = -1;
            var rnd = new Random();
            foreach (DataRow dataRow in dt.Rows)
            {
                iter1++;
                resArr.Clear();
                var date1 = DateTime.Parse(dataRow[1].ToString());
                var iter2 = -1;
                var price1 = Convert.ToDouble(dataRow[0]);
                if (price1 == 0) price1 += Convert.ToDouble(rnd.Next(-1000, 1000)/1000000);
                //
                if (!nameInRow(dataRow[2].ToString())) continue;
                decimal maxTaniCoeff = 0;
                foreach (DataRow dataRowCheck in dt.Rows)
                {
                    iter2++;
                    if ((TaniAlgo(dataRow[2].ToString(), dataRowCheck[2].ToString()) >= maxTaniCoeff) &&
                        (iter1 != iter2))
                        maxTaniCoeff = TaniAlgo(dataRow[2].ToString(), dataRowCheck[2].ToString());
                }
                //
                if (maxTaniCoeff < rate) continue;
                resArr.Add(iter1);
                iter2 = -1;
                foreach (DataRow dataRowCheck in dt.Rows)
                {
                    iter2++;
                    if (iter2 == iter1)
                    {
                        arrOfIndexes.Add(-1);
                        continue;
                    }
                    if (!nameInRow(dataRowCheck[2].ToString())) continue;
                    var date2 = DateTime.Parse(dataRowCheck[1].ToString());
                    var price2 = Convert.ToDouble(dataRowCheck[0]);
                    if (price2 == 0) price2 += Convert.ToDouble(rnd.Next(-1000, 1000)/1000000);
                    var taniCoeff = TaniAlgo(dataRow[2].ToString(), dataRowCheck[2].ToString());
                    if (taniCoeff < maxTaniCoeff - (1 - rate)/7) continue;
                    if (Math.Abs((date2 - date1).TotalDays) <= Convert.ToDouble(monthMin))
                    {
                        resArr.Clear();
                        break;
                    }
                    if (Convert.ToDecimal(Math.Abs(Math.Max(Math.Abs(price1), Math.Abs(price2)) / Math.Min(Math.Abs(price1), Math.Abs(price2)) - 1)) >= (1 - rate)*2m)
                    {
                        resArr.Clear();
                        break;
                    }
                    resArr.Add(iter2);
                    date1 = date2;
                    price1 = (price1 + price2)/2;
                }
                if (resArr.Count <= 1) continue;
                resArr.Sort();
                if (/*(resArr.Count >= 3) && */getMaxDays(resArr, dt))
                    continue;
                listRes.Add(resArr.ToArray());
            }
            listRes = removeDup(listRes);
            listRes = removeIntersect(listRes, dt);
            //listRes = removeOutliers(listRes, dt);
            resToCsv(listRes, dt);
        }
        private static List<int[]> removeIntersect(List<int[]> listRes, DataTable dt)
        {
            var tempArr = new List<int>();
            int jRem = 0;
            bool check = false;
            for (var i = 0; i < listRes.Count; i++)
            {
                check = false;
                if (listRes[i] == null) continue;
                for (var j = 0; j < listRes[i].Length; j++)
                {
                    if (listRes[j] == null) continue;
                    jRem = checkThatContains(listRes, listRes[i][j], i);
                    if (jRem!=-1)
                    {
                        check = true;
                        break;
                    }
                }
                if (check)
                {
                    tempArr.Clear();
                    for (var ii = 0; ii < listRes[i].Length; ii++) tempArr.Add(listRes[i][ii]);
                    for (var jj = 0; jj < listRes[jRem].Length; jj++) tempArr.Add(listRes[jRem][jj]);
                    tempArr.Sort();
                    tempArr=tempArr.Distinct().ToList();
                    //
                    listRes[i] = null;
                    listRes[jRem] = null;
                    if (getMaxDays(tempArr, dt)) continue;
                    listRes[i] = tempArr.ToArray();
                }
            }
            return listRes;
        }

        private static int checkThatContains(List<int[]> listRes, int el, int iExc)
        {
            int res = -1;
            for (var i = 0; i < listRes.Count; i++)
            {
                if (i == iExc) continue;
                if (listRes[i] == null) continue;
                for (var j = 0; j < listRes[i].Length; j++)
                {
                    if (el == listRes[i][j])
                    {
                        res = i;
                        return res;
                    }
                }
            }
            return res;
        }

        private static
            bool getMaxDays(IReadOnlyList<int> resArr, DataTable dt)
        {
            var dateNum = 0m;
            bool check = false;
            for (var i = 1; i < resArr.Count; i++)
            {
                var date1 = DateTime.Parse(dt.Rows[resArr[i - 1]][1].ToString());
                var date2 = DateTime.Parse(dt.Rows[resArr[i]][1].ToString());
                dateNum = Convert.ToDecimal(Math.Abs((date1 - date2).TotalDays));
                if ((dateNum > monthMin) && (dateNum < monthMax) || ((dateNum > yearMin) && (dateNum < yearMax)))
                    check = false;
                else
                {
                    return true;
                }
            }
            return check;
        }

        private static bool nameInRow(string row) => restrNames.All(t => !row.Contains(t));

        private static void FillDic(IDictionary<string, string> monthNumDic)
        {
            monthNumDic.Add("Jan", "01");
            monthNumDic.Add("Feb", "02");
            monthNumDic.Add("Mar", "03");
            monthNumDic.Add("Apr", "04");
            monthNumDic.Add("May", "05");
            monthNumDic.Add("Jun", "06");
            monthNumDic.Add("Jul", "07");
            monthNumDic.Add("Aug", "08");
            monthNumDic.Add("Sep", "09");
            monthNumDic.Add("Oct", "10");
            monthNumDic.Add("Nov", "11");
            monthNumDic.Add("Dec", "12");
        }

        private static string getMonthIndex(IReadOnlyDictionary<string, string> monthNumDic, string monthName) => monthNumDic[monthName];

        private static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            var header = isFirstRowHeader ? "Yes" : "No";

            var pathOnly = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            var sql = @"SELECT * FROM [" + fileName + "]";

            using (var connection = new OleDbConnection(
                @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (var command = new OleDbCommand(sql, connection))
            using (var adapter = new OleDbDataAdapter(command))
            {
                var dataTable = new DataTable {Locale = CultureInfo.CurrentCulture};
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private static DataTable dataPreparation(DataTable dt)
        {
            FillDic(monthNum); //fill fictionary to get month number
            foreach (
                var column in
                    dt.Columns.Cast<DataColumn>()
                        .ToArray()
                        .Where(column => dt.AsEnumerable().All(dr => dr.IsNull(column))))
            {
                dt.Columns.Remove(column);
            }
            //
            dt.Columns.Add("fullDate", typeof (DateTime));
            dt.Columns.Add("fullName", typeof (string));
            //if(dt.Columns.Count == 6)
            //    dt.Columns.Add();
            ////
            var i = 0;
            foreach (var myDate in from DataRow row in dt.Rows
                select row[0].ToString().Split('-')
                into dateFormat
                select dateFormat[2].Replace(" 12:00:00 AM", "") + "-" + getMonthIndex(monthNum, dateFormat[1]) + "-" + dateFormat[0] + " 00:00:00"
                into s
                select DateTime.ParseExact(s, "yy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture))
            {
                dt.Rows[i][5] = myDate;
                //dt.Rows[i][6] = dt.Rows[i][3];
                i++;
            }
            //
            dt.Columns[0].SetOrdinal(1); 
            dt.Columns.Remove(dt.Columns[3]); //removing unused columns
            dt.Columns.Remove(dt.Columns[3]);
            dt.Columns.Remove(dt.Columns[3]);
            //
            return dt;
        }

        private static decimal TaniAlgo(string s1, string s2)
        {
            var listS1 = s1.Split(' ');
            var listS2 = s2.Split(' ');
            for (var i = 0; i < listS1.Length; i++)
            {
                if (isDigit(listS1[i])) listS1[i] = "";
                if (isSep(listS1[i])) listS1[i] = "";
                if (isCardName(listS1[i])) listS1[i] = "";
            }
            for (var i = 0; i < listS2.Length; i++)
            {
                if (isDigit(listS2[i])) listS2[i] = "";
                if (isSep(listS2[i])) listS2[i] = "";
                if (isCardName(listS2[i])) listS2[i] = "";
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
                res = c/(lenS1 + lenS2 - c);
            }
            catch
            {
                if ((lenS1 != 0) && (lenS2 != 0)) res = 0;
                else res = 1;
            }
            return res;
        }

        private static bool isDigit(string s)
        {
            return s.All(c => (c >= '0') && (c <= '9'));
        }

        private static bool isSep(string s)
        {
            return s.Any(c => c == '/');
        }

        private static bool isCardName(string s)
        {
            return (s.Length > 0) && (s[0] == 'V') && (s[1] > '0') && (s[1] < '9');
        }

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

        private static void resToCsv(IReadOnlyList<int[]> listRes, DataTable dt)
        {
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
                    if ((listRes[index] == null) || (listRes[index].Length == 2)) continue;
                    for (var len = 0; len < listRes[index].Length; len++)
                    {
                        var temp = dt.Rows[listRes[index][len]][0] + " ||| " + dt.Rows[listRes[index][len]][1] +
                                   " ||| " + dt.Rows[listRes[index][len]][2];
                        writer.WriteLine(temp);
                    }
                    writer.WriteLine("");
                }
                //
                writer.WriteLine("Groups of two");
                for (var index = 0; index < length; index++)
                {
                    if ((listRes[index] == null) || (listRes[index].Length != 2)) continue;
                    for (var len = 0; len < listRes[index].Length; len++)
                    {
                        var temp = dt.Rows[listRes[index][len]][0] + " ||| " + dt.Rows[listRes[index][len]][1] +
                                   " ||| " + dt.Rows[listRes[index][len]][2];
                        writer.WriteLine(temp);
                    }
                    writer.WriteLine("");
                }
            }
        }
    }
}