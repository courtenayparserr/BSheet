using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace InStr
{
    internal static class Program
    {
        private static void Main()
        {
            //Console.Write("Pattern string >> ");
            //var pattern = Console.ReadLine();
            var pattern = "hlu*hulu 3183129-u hulu.com/billca".ToLower();
            pattern = Regex.Replace(pattern, @"[^a-zA-Z0-9 -.]", "");
            pattern = pattern.ToLower();
            pattern = pattern.Replace("-", string.Empty);
            pattern = pattern.Replace("recurring", string.Empty);
            pattern = pattern.Replace("payment", string.Empty).ToLower();
            //Console.Write("Test string >> ");
            //var test = Console.ReadLine();
            var test = "HULU.COM/BILL CA".ToLower();

            test = Regex.Replace(test, @"[^a-zA-Z0-9 -.]", "");
            test = test.ToLower();
            test = test.Replace("-", string.Empty);
            test = test.Replace("recurring", string.Empty);
            test = test.Replace("payment", string.Empty).ToLower();

            Console.WriteLine("Index >> " + StringIndex(pattern, test));
            Console.WriteLine("Index2 >> " + StringIndex(test, pattern));

            Console.WriteLine("FindMatch >> " + FindMatch(pattern, test));
            Console.WriteLine("FindMatch2 >> " + FindMatch(test, pattern));
            Console.ReadLine();
        }

        static int FindMatch(string text, string pattern)
        {
            var total = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                var max = 0;
                for (int j = 2; j <= pattern.Length - i; j++)
                {
                    var temp = pattern.Substring(i, j);
                    if (text.Contains(temp))
                        if (max < temp.Length)
                            max = temp.Length;
                }
                total += max;
                if (max > 0)
                    i += max - 1;
            }
            return total;
        }

        private static string StringIndex(string pattern, string test)
        {
            var patternSplit = pattern.Split(' ');
            var testSplit = test.Split(' ');
            List<int> jS = new List<int>();
            var index = 0;
            for (var i = 0; i < testSplit.Length; i++)
            {
                for (var j = 0; j < patternSplit.Length; j++)
                {
                    if (testSplit[i] == patternSplit[j])
                    {
                        index += patternSplit[j].Length;
                        if (((i == 0) && (j == patternSplit.Length - 1)) || ((i == testSplit.Length - 1) && (j == 0))) index += 0;
                        else if ((i == j) && ((i != 0) || (i != testSplit.Length - 1)) && (j != 0) &&
                                 (j != testSplit.Length - 1))
                        {
                            index += 2;
                            jS.Add(j);
                        }
                        else
                        {
                            index += 1;
                            jS.Add(j);
                        }
                    }
                    else if (testSplit[i].StartsWith(patternSplit[j], StringComparison.Ordinal))
                    {
                        index += patternSplit[j].Length;
                        if (((i == 0) && (j == patternSplit.Length - 1)) || ((i == testSplit.Length - 1) && (j == 0))) index += 0;
                        else if ((i == j) && ((i != 0) || (i != testSplit.Length - 1)) && (j != 0) &&
                                 (j != testSplit.Length - 1))
                        {
                            index += 1;
                            jS.Add(j);
                        }
                        else index += 0;
                    }
                }
            }
            return (index - neighborhoodCount(jS)).ToString();

        }
        private static int neighborhoodCount(List<int> jS)
        {
            int count = 0;
            for (int i = 0; i < jS.Count; i++)
            {
                for (int j = i + 1; j < jS.Count; j++)
                {
                    if (Math.Abs(jS[i] - jS[j]) == 1) count++;
                }
            }
            return count;
        }
    }
}
