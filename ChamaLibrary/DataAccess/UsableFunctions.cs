using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ChamaLibrary.DataAccess
{
    public static class UsableFunctions
    {
        #region ToWords
        private static readonly string[] _ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };
        private static readonly string[] _teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };
        private static readonly string[] _tens =
        {
            "",
            "ten",
            "twenty",
            "thirty",
            "fourty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };
        private static readonly string[] _thousands =
        {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion"
        };
        public static string ToWordsProcessing(string digits, int det)
        {
            string temp;
            bool showthousands = false;
            bool allzeros = true;
            bool no_and = false;
            // use string builder to build result
            StringBuilder builder = new StringBuilder();

            //traverse characters in reverse order

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int ndigit = (int)(digits[i] - '0');
                int column = (digits.Length - (i + 1));

                //determine if ones,tens, or hundreds column
                switch (column % 3)
                {

                    case 0: //ones position
                        showthousands = true;
                        if (i == 0)
                        {
                            //first digit in number (last in loop)
                            temp = string.Format("{0} ", _ones[ndigit]);
                        }
                        else if (digits[i - 1] == '1')
                        {
                            //this digit is part of "teen" value
                            temp = string.Format("{0} ", _teens[ndigit]);
                            //skip teens position
                            i--;
                        }
                        else if (ndigit != 0)
                        {
                            //any non zero digit
                            temp = string.Format("{0} ", _ones[ndigit]);
                        }
                        else
                        {
                            // ensure 'and' is included in text
                            no_and = true;
                            //this digit is zero.if digit in tens and hundreds
                            //column are also zero dont show "Thousands"
                            temp = string.Empty;
                            // test fornon-zero digit in this grouping
                            if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                            {
                                //if (digits[digits.Length - 3] != '0')
                                //{
                                //    no_and_afterathousand = true;
                                //}

                                showthousands = true;
                            }
                            else
                            {
                                showthousands = false;
                            }

                        }

                        //show thousands if non zero in grouping

                        if (showthousands)
                        {
                            if (column > 0)
                            {
                                //if millions and above separate with ,
                                //if (column > 3)
                                //{
                                //   no_and_afterathousand = true;
                                //}

                                if (column > 3 || digits[digits.Length - 3] != '0')
                                {
                                    temp = string.Format("{0}{1}{2}", temp, _thousands[column / 3], allzeros ? " " : ",");
                                }
                                else
                                {
                                    temp = string.Format("{0}{1}{2}", temp, _thousands[column / 3], allzeros ? " " : " and ");
                                }



                            }
                            //indicate non zero digit encountered
                            allzeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:         // tens column
                        if (ndigit > 0)
                        {
                            temp = string.Format("{0}{1} ", _tens[ndigit], (digits[i + 1] != '0') ? "" : " ");
                            builder.Insert(0, temp);
                        }
                        break;
                    case 2:
                        if (ndigit > 0)
                        {
                            if (no_and)
                            {
                                temp = string.Format("{0} hundred ", _ones[ndigit]);
                            }
                            else
                            {
                                temp = string.Format("{0} hundred and ", _ones[ndigit]);
                            }

                            builder.Insert(0, temp);
                        }
                        break;
                }
            }

            if (det == 1)
            {
                //capitalize first letter
                return string.Format("{0}{1}", char.ToUpper(builder[0]), builder.ToString(1, builder.Length - 1));
            }

            return builder.ToString();

        }
        public static string ToWords(decimal value)
        {
            string digits;
            string decims;
            string int_wording;
            string dec_wording;

            //convert the integer portion of value to string
            digits = ((long)value).ToString();
            int_wording = ToWordsProcessing(digits, 1);

            // fractionalportion /cents
            decims = ((value - (long)value) * 100).ToString();

            if (Convert.ToDecimal(decims) == 0)
            {
                return int_wording.Trim() + " shillings";
            }
            else
            {
                decims = ((long)Convert.ToDecimal(decims)).ToString();
                if (decims.Length > 2)
                {
                    decims = decims.Substring(0, 2);
                }

                dec_wording = ToWordsProcessing(decims, 0);
                //concatenate the two wordings
                return int_wording.Trim() + " shillings and " + dec_wording.Trim() + " cents";

            }


        }
        #endregion

        #region GetMonthInWords
        public static string GetMonthInWords(int MonthVal, string Yeaz)
        {
            string MonthWord = "";
            if (MonthVal == 1)
            {
                MonthWord = $"January {Yeaz}";
            }
            else if (MonthVal == 2)
            {
                MonthWord = $"February {Yeaz}";
            }
            else if (MonthVal == 3)
            {
                MonthWord = $"March {Yeaz}";
            }
            else if (MonthVal == 4)
            {
                MonthWord = $"April {Yeaz}";
            }
            else if (MonthVal == 5)
            {
                MonthWord = $"May {Yeaz}";
            }
            else if (MonthVal == 6)
            {
                MonthWord = $"June {Yeaz}";
            }
            else if (MonthVal == 7)
            {
                MonthWord = $"July {Yeaz}";
            }
            else if (MonthVal == 8)
            {
                MonthWord = $"August {Yeaz}";
            }
            else if (MonthVal == 9)
            {
                MonthWord = $"September {Yeaz}";
            }
            else if (MonthVal == 10)
            {
                MonthWord = $"October {Yeaz}";
            }
            else if (MonthVal == 11)
            {
                MonthWord = $"November {Yeaz}";
            }
            else if (MonthVal == 12)
            {
                MonthWord = $"December {Yeaz}";
            }
            return MonthWord;
        }

        public static string GetMonthInWords(int MonthVal)
        {
            string MonthWord = "";
            if (MonthVal == 1)
            {
                MonthWord = $"January";
            }
            else if (MonthVal == 2)
            {
                MonthWord = $"February";
            }
            else if (MonthVal == 3)
            {
                MonthWord = $"March";
            }
            else if (MonthVal == 4)
            {
                MonthWord = $"April";
            }
            else if (MonthVal == 5)
            {
                MonthWord = $"May";
            }
            else if (MonthVal == 6)
            {
                MonthWord = $"June";
            }
            else if (MonthVal == 7)
            {
                MonthWord = $"July";
            }
            else if (MonthVal == 8)
            {
                MonthWord = $"August";
            }
            else if (MonthVal == 9)
            {
                MonthWord = $"September";
            }
            else if (MonthVal == 10)
            {
                MonthWord = $"October";
            }
            else if (MonthVal == 11)
            {
                MonthWord = $"November";
            }
            else if (MonthVal == 12)
            {
                MonthWord = $"December";
            }
            return MonthWord;
        }
        #endregion


        #region Get Last Date Of the month

        public static DateTime GetDateTo(string PayMonth)
        {
            string PMonth = "";
            int PYear = 0;
            int PYear2 = 0;

            if (int.TryParse(PayMonth.Substring(PayMonth.Length - 2), out _)) //month string has both month and year
            {
                PMonth = PayMonth.Trim().Split(null)[0];

                if (PayMonth.Trim().Split(null)[1].Length > 4) // years string has got two years concanated
                {
                    PYear = int.Parse(PayMonth.Trim().Split(null)[1].Split('/')[0]);
                    PYear2 = int.Parse(PayMonth.Trim().Split(null)[1].Split('/')[1]);
                }
                else
                {
                    PYear = int.Parse(PayMonth.Trim().Split(null)[1]);
                }
            }
            else //month string has got no year
            {
                PMonth = PayMonth.Trim();

                if (SqliteDataAccess.FinYear.Length > 4) // years string has got two years concanated
                {
                    PYear = int.Parse(SqliteDataAccess.FinYear.Split('/')[0]);
                    PYear2 = int.Parse(SqliteDataAccess.FinYear.Split('/')[1]);
                }

                else
                {
                    PYear = int.Parse(SqliteDataAccess.FinYear);
                }
            }

            if (SqliteDataAccess.FinYear.Length > 4)
            {

                DateTime DateTo = Convert.ToDateTime($"{PYear}-01-01");
                if (PMonth.Equals("January"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-02-01");
                }
                else if (PMonth.Equals("February"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-03-01");
                }
                else if (PMonth.Equals("March"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-04-01");
                }
                else if (PMonth.Equals("April"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-05-01");
                }
                else if (PMonth.Equals("May"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-06-01");
                }
                else if (PMonth.Equals("June"))
                {
                    DateTo = Convert.ToDateTime($"{PYear2}-07-01");
                }
                else if (PMonth.Equals("July"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-08-01");
                }
                else if (PMonth.Equals("August"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-09-01");
                }
                else if (PMonth.Equals("September"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-10-01");
                }
                else if (PMonth.Equals("October"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-11-01");
                }
                else if (PMonth.Equals("November"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-12-01");
                }
                else if (PMonth.Equals("December"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-12-31");
                    DateTo = DateTo.AddDays(1);
                }
                else if (PMonth.Equals("All"))
                {
                    DateTo = SqliteDataAccess.FinYearEnd.AddDays(1);
                }

                return DateTo;
            }
            else
            {
                //int PYear = int.Parse(PayMonth.Trim().Split(null)[1]);

                DateTime DateTo = Convert.ToDateTime($"{PYear}-02-01");
                if (PMonth.Equals("January"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-02-01");
                }
                else if (PMonth.Equals("February"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-03-01");
                }
                else if (PMonth.Equals("March"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-04-01");
                }
                else if (PMonth.Equals("April"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-05-01");
                }
                else if (PMonth.Equals("May"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-06-01");
                }
                else if (PMonth.Equals("June"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-07-01");
                }
                else if (PMonth.Equals("July"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-08-01");
                }
                else if (PMonth.Equals("August"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-09-01");
                }
                else if (PMonth.Equals("September"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-10-01");
                }
                else if (PMonth.Equals("October"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-11-01");
                }
                else if (PMonth.Equals("November"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-12-01");
                }
                else if (PMonth.Equals("December"))
                {
                    DateTo = Convert.ToDateTime($"{PYear}-12-31");
                    DateTo = DateTo.AddDays(1);
                }
                else if (PMonth.Equals("All"))
                {
                    DateTo = SqliteDataAccess.FinYearEnd.AddDays(1);
                }

                return DateTo;
            }

        }
        #endregion


        #region Get First Date Of the month
        public static DateTime GetDateFrom(string PayMonth)
        {
            string PMonth = "";

            int PYear = 0;
            int PYear2 = 0;

            if (int.TryParse(PayMonth.Substring(PayMonth.Length - 2), out _)) //month string has both month and year
            {
                PMonth = PayMonth.Trim().Split(null)[0];

                if (PayMonth.Trim().Split(null)[1].Length > 4) // years string has got two years concanated
                {
                    PYear = int.Parse(PayMonth.Trim().Split(null)[1].Split('/')[0]);
                    PYear2 = int.Parse(PayMonth.Trim().Split(null)[1].Split('/')[1]);
                }
                else
                {
                    PYear = int.Parse(PayMonth.Trim().Split(null)[1]);
                }
            }
            else //month string has got no year
            {
                PMonth = PayMonth.Trim();

                if (SqliteDataAccess.FinYear.Length > 4) // years string has got two years concanated
                {
                    PYear = int.Parse(SqliteDataAccess.FinYear.Split('/')[0]);
                    PYear2 = int.Parse(SqliteDataAccess.FinYear.Split('/')[1]);
                }
                else
                {
                    PYear = int.Parse(SqliteDataAccess.FinYear);
                }
            }

            if (SqliteDataAccess.FinYear.Length > 4)
            {

                DateTime DateFrom = Convert.ToDateTime($"{PYear}-01-01");

                if (PMonth.Equals("January"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-01-01");
                }
                else if (PMonth.Equals("February"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-02-01");
                }
                else if (PMonth.Equals("March"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-03-01");
                }
                else if (PMonth.Equals("April"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-04-01");
                }
                else if (PMonth.Equals("May"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-05-01");
                }
                else if (PMonth.Equals("June"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear2}-06-01");
                }
                else if (PMonth.Equals("July"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-07-01");
                }
                else if (PMonth.Equals("August"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-08-01");
                }
                else if (PMonth.Equals("September"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-09-01");
                }
                else if (PMonth.Equals("October"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-10-01");
                }
                else if (PMonth.Equals("November"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-11-01");
                }
                else if (PMonth.Equals("December"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-12-01");
                }
                return DateFrom;
            }
            else
            {
                //int PYear = int.Parse(PayMonth.Trim().Split(null)[1]);

                DateTime DateFrom = Convert.ToDateTime($"{PYear}-01-01");

                if (PMonth.Equals("January"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-01-01");
                }
                else if (PMonth.Equals("February"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-02-01");
                }
                else if (PMonth.Equals("March"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-03-01");
                }
                else if (PMonth.Equals("April"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-04-01");
                }
                else if (PMonth.Equals("May"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-05-01");
                }
                else if (PMonth.Equals("June"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-06-01");
                }
                else if (PMonth.Equals("July"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-07-01");
                }
                else if (PMonth.Equals("August"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-08-01");
                }
                else if (PMonth.Equals("September"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-09-01");
                }
                else if (PMonth.Equals("October"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-10-01");
                }
                else if (PMonth.Equals("November"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-11-01");
                }
                else if (PMonth.Equals("December"))
                {
                    DateFrom = Convert.ToDateTime($"{PYear}-12-01");
                }
                return DateFrom;
            }
        }
        #endregion
    }
}
