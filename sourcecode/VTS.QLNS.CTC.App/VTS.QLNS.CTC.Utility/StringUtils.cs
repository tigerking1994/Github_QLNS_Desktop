using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VTS.QLNS.CTC.Utility
{
    public class StringUtils
    {
        public static string DOT = ".";
        public static string DOT_SPLIT = " . ";
        public static string SPACE = " ";
        public static string COMMA = ",";
        public static string COMMA_SPLIT = ", ";
        public static string SEMICOLON = ";";
        public static string UNDERSCORE = "_";
        public static string DIVISION = "-";
        public static string DIVISION_SPLIT = " - ";
        public static string ZERO = "0";

        public static string EXCEL = "xlsx";
        public static string ZIP = "zip";
        public static string PDF = "pdf";
        public static string DBF = "DBF";
        public static string EXCEL_EXTENSION = $"{DOT}{EXCEL}";
        public static string PDF_EXTENSION = $"{DOT}{PDF}";
        public static string DBF_EXTENSION = $"{DOT}{DBF}";
        public static string ZIP_EXTENSION = $"{DOT}{ZIP}";
        public static string DELIMITER = ":::";
        public static string FORMAT_ZERO = "{0:N0}";
        public static string SPACE_DIVISION = "   ";

        public static char[] SPLITCHARS = { '+', '-', '*', '/', '(', ')', };

        public static char CHAR_COMMA = ',';
        public static char CHAR_DIVISION = '-';

        public static string RemoveAccents(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            string normalized = input.Normalize(NormalizationForm.FormKD);
            Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
                new EncoderReplacementFallback(""),
                new DecoderReplacementFallback(""));
            byte[] bytes = removal.GetBytes(normalized);
            return Encoding.ASCII.GetString(bytes);
        }

        public static bool IsNullOrEmpty(String str)
        {
            return String.IsNullOrEmpty(str);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return !string.IsNullOrEmpty(str)
                ? Regex.Replace(str, "[^-a-zA-Z0-9.]+", "", RegexOptions.Compiled)
                : string.Empty;
        }

        public static string Join(string separator, params string[] value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = string.Join(separator, value.Where(x => !string.IsNullOrWhiteSpace(x) && !DIVISION.Equals(x.Trim())).Select(RemoveSpecialCharacters));
            return result;
        }

        public static string Joining(IEnumerable<string> values)
        {
            return Joining("'", "'", "','", values);
        }

        public static string Joining(string startStr, string endStr, string separator, IEnumerable<string> values)
        {
            return startStr + string.Join(separator, values) + endStr;
        }

        public static string GetValue(string val)
        {
            return string.IsNullOrEmpty(val) ? string.Empty : val;
        }

        public static string NumberToText(double inputNumber, bool suffix = true, string donvi = "")
        {
            inputNumber = Math.Round(inputNumber);
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = Convert.ToInt64(inputNumber).ToString();
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }

            Int64 ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt64(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt64(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt64(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "mốt " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            if (isNegative) result = "Giảm " + result;
            if (string.IsNullOrEmpty(result))
            {
                result = "Không";
            }

            string donviTinh = donvi != "" ? donvi.ToLower() : "đồng";
            return FirstLetterToUpper(Regex.Replace(result, @"\s+", " ") + (suffix ? " " + donviTinh : " USD"));
        }


        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;
            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1) + ".";
            return str.ToUpper() + ".";
        }

        public static string CreateDateTimeString()
        {
            StringBuilder builder = new StringBuilder();
            DateTime now = DateTime.Now;
            builder.AppendFormat("Ngày {0} tháng {1} năm {2}", now.Day, now.Month, now.Year);
            return builder.ToString();
        }

        public static string CreateExportFileName(string fileName, string extension = null)
        {
            return string.Format("{0}_{1}{2}", fileName, DateTime.Now.ToStringTimeStampMiliSeconds(), extension != null ? extension : string.Empty);
        }

        public static string GetXauNoiMaParent(string stringInput)
        {
            List<string> listInput = stringInput.Split(',').ToList();
            List<string> result = new List<string>();
            foreach (string item in listInput)
            {
                result.AddRange(GetIncrementalSubstrings(item));
            }
            return string.Join(",", result.Distinct().OrderBy(n => n).ToList());
        }

        public static HashSet<string> GetXauNoiMaParent(IEnumerable<string> listInput)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (string item in listInput)
            {
                foreach (string substring in GetIncrementalSubstrings(item))
                {
                    result.Add(substring);
                }
            }
            return result;
        }

        public static HashSet<string> GetXauNoiMaParentOptimize(IEnumerable<string> listInput)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (string item in listInput)
            {
                var res = GetIncrementalSubstringsOptimize(item, result);

                foreach (string substring in res)
                {
                    result.Add(substring);
                }
            }
            return result;
        }

        public static List<string> GetListXauNoiMaParent(List<string> listInput)
        {
            List<string> result = new List<string>();
            foreach (string item in listInput)
            {
                result.AddRange(SplitXauNoiMaParent(item));
            }
            return result.Distinct().OrderBy(n => n).ToList();
        }

        public static List<string> SplitXauNoiMaParent(string input)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(input))
                return new List<string>();
            if (input.Length == 43)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input.Substring(0, 25));
                result.Add(input.Substring(0, 28));
                result.Add(input.Substring(0, 31));
                result.Add(input.Substring(0, 34));
                result.Add(input.Substring(0, 37));
                result.Add(input.Substring(0, 40));
                result.Add(input);
            }
            else if (input.Length == 40)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input.Substring(0, 25));
                result.Add(input.Substring(0, 28));
                result.Add(input.Substring(0, 31));
                result.Add(input.Substring(0, 34));
                result.Add(input.Substring(0, 37));
                result.Add(input);
            }
            else if (input.Length == 37)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input.Substring(0, 25));
                result.Add(input.Substring(0, 28));
                result.Add(input.Substring(0, 31));
                result.Add(input.Substring(0, 34));
                result.Add(input);
            }
            else if (input.Length == 34 || input.Length == 35)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input.Substring(0, 25));
                result.Add(input.Substring(0, 28));
                result.Add(input.Substring(0, 31));
                result.Add(input);
            }
            else if (input.Length == 31)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 5));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input.Substring(0, 25));
                result.Add(input.Substring(0, 28));
                result.Add(input);
            }
            else if (input.Length == 20)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input);
            }
            else if (input.Length == 25)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 7));
                result.Add(input.Substring(0, 11));
                result.Add(input.Substring(0, 15));
                result.Add(input.Substring(0, 20));
                result.Add(input);
            }
            else if (input.Length == 7)
            {
                result.Add(input.Substring(0, 1));
                result.Add(input.Substring(0, 3));
                result.Add(input.Substring(0, 5));
                result.Add(input);
            }
            else if (input.Length == 3 || input.Length == 1)
            {
                result.Add(input);
            }
            return result;
        }

        private static IEnumerable<string> GetIncrementalSubstrings(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                yield return string.Empty;
                yield break;
            }

            string[] parts = input.Split('-');
            if (parts.Length == 0)
            {
                yield return string.Empty;
                yield break;
            }

            string currentSubstring = parts[0];
            if (currentSubstring.Length == 3 || currentSubstring.Length == 1) yield return currentSubstring;
            else if (currentSubstring.Length == 7)
            {
                yield return currentSubstring.Substring(0, 1);
                yield return currentSubstring.Substring(0, 3);
                yield return currentSubstring.Substring(0, 5);
                yield return currentSubstring.Substring(0, 7);
            }

            for (int i = 1; i < parts.Length; i++)
            {
                currentSubstring += "-" + parts[i];
                yield return currentSubstring;
            }
        }

        private static IEnumerable<string> GetIncrementalSubstringsOptimize(string input, HashSet<string> exists)
        {
            if (string.IsNullOrEmpty(input))
            {
                yield return string.Empty;
                yield break;
            }

            string[] parts = input.Split('-');
            if (parts.Length == 0)
            {
                yield return string.Empty;
                yield break;
            }

            string currentSubstring = parts[0];
            if ((currentSubstring.Length == 3 || currentSubstring.Length == 1) && !exists.Contains(currentSubstring)) yield return currentSubstring;
            else if (currentSubstring.Length == 7 && !exists.Contains(currentSubstring))
            {
                yield return currentSubstring.Substring(0, 1);
                yield return currentSubstring.Substring(0, 3);
                yield return currentSubstring.Substring(0, 5);
                yield return currentSubstring.Substring(0, 7);
            }

            var inputTest = input;
            yield return inputTest;
            for (int i = 1; i < parts.Length; i++)
            {
                var lastIndex = inputTest.LastIndexOf('-');
                inputTest = inputTest.Substring(0, lastIndex);
                if (exists.Contains(inputTest) || string.IsNullOrEmpty(inputTest)) yield break;
                //currentSubstring += "-" + parts[i];
                yield return inputTest;
            }
        }


        public static List<string> GetListKyHieuParent(List<string> listInput)
        {
            List<string> result = new List<string>();
            foreach (string item in listInput)
            {
                result.AddRange(SplitKyHieuParent(item));
            }
            return result.Distinct().OrderBy(n => n).ToList();
        }

        public static List<string> SplitKyHieuParent(string input)
        {
            List<string> rs = new List<string>();
            if (string.IsNullOrEmpty(input))
                return new List<string>();
            if (input.Length == 1)
            {
                rs.Add(input);
                return rs;
            }
            List<string> s = input.Split("-").ToList();
            if (s.Count == 1)
            {
                rs.Add(input.Substring(0, 1));
                rs.Add(input);
                return rs;
            }

            for (int i = 1; i <= s.Count; i++)
            {
                rs.Add(string.Join("-", s.TakeWhile((ele, index) => index < i)));
            }

            return rs;
        }

        public static string ConvertMaOrder(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            int[] indexes = str.Split("-").Select(x => int.Parse(x)).ToArray();
            return string.Join(".", indexes);
        }

        public static string ConvertMaOrderNew(string str, int index)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            List<int> indexes = str.Split("-").Select(x => int.Parse(x)).ToList();
            indexes.RemoveAt(0);
            indexes.Insert(0, index);
            return string.Join(".", indexes);
        }
        public static int ChooseCaretIndex(int oldIndex, string oldText, string newText)
        {
            // There is no exact algorithm for this.  Instead we use some heuristics.
            //   First handle some frequent special cases

            // oldText appears within newText, translate the index
            int index = newText.IndexOf(oldText, StringComparison.Ordinal);
            if (oldText.Length > 0 && index >= 0)
                return index + oldIndex;

            // caret was at one edge of oldText, return corresponding edge
            if (oldIndex == 0)
                return 0;
            if (oldIndex == oldText.Length)
                return newText.Length;

            // newText differs from oldText by a small replacement
            // (this is common when doing conversions to numeric types - adding
            // leading or trailing zeros, decimal separators, thousand separators,
            // etc.).
            // The two strings share a common prefix and suffix - find those
            int prefix, suffix;
            for (prefix = 0;
                prefix < oldText.Length && prefix < newText.Length;
                ++prefix)
            {
                if (oldText[prefix] != newText[prefix])
                    break;
            }

            for (suffix = 0;
                suffix < oldText.Length && suffix < newText.Length;
                ++suffix)
            {
                if (oldText[oldText.Length - 1 - suffix] != newText[newText.Length - 1 - suffix])
                    break;
            }

            // if the prefix and suffix account for enough of the text, treat the
            // rest as a small replacement
            if (2 * (prefix + suffix) >= Math.Min(oldText.Length, newText.Length))
            {
                // if the caret was in or next to the prefix or suffix, return the
                // corresponding position in newText
                if (oldIndex <= prefix)
                    return oldIndex;
                if (oldIndex >= oldText.Length - suffix)
                    return newText.Length - (oldText.Length - oldIndex);
            }

            // we're left with the hard case - newText is substantially different
            // from oldText.  Look for the longest matching substring that includes
            // the character just before the (old) caret - this is what the user
            // just typed, so it should participate in the match.
            char anchor = oldText[oldIndex - 1];
            int anchorIndex = newText.IndexOf(anchor);
            int bestIndex = -1;
            int bestLength = 1; // match at least 2 chars

            while (anchorIndex >= 0)
            {
                int matchLength = 1;

                // match backward from the anchor position
                for (index = anchorIndex - 1;
                    index >= 0 && oldIndex - (anchorIndex - index) >= 0;
                    --index)
                {
                    if (newText[index] != oldText[oldIndex - (anchorIndex - index)])
                        break;
                    ++matchLength;
                }

                // match forward from the anchor position
                for (index = anchorIndex + 1;
                    index < newText.Length && oldIndex + (index - anchorIndex) < oldText.Length;
                    ++index)
                {
                    if (newText[index] != oldText[oldIndex + (index - anchorIndex)])
                        break;
                    ++matchLength;
                }

                // remember the best match
                if (matchLength > bestLength)
                {
                    bestIndex = anchorIndex + 1;
                    bestLength = matchLength;
                }

                // advance to the next occurrence of the anchor character
                anchorIndex = newText.IndexOf(anchor, anchorIndex + 1);
            }

            // return the index of the best match.  If none found, put the cursor at the end
            return (bestIndex < 0) ? newText.Length : bestIndex;
        }

        public static string UCS2Convert(string sContent)
        {
            sContent = sContent.Trim().ToLower();
            string sUTF8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            string sUCS2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            string[] aUTF8Lower = sUTF8Lower.Split(new[] { '|' });

            string[] aUCS2Lower = sUCS2Lower.Split(new[] { '|' });

            int l = aUTF8Lower.GetUpperBound(0);

            for (int i = 1; i < l; i++)
            {
                sContent = sContent.Replace(aUTF8Lower[i], aUCS2Lower[i]);
            }
            sContent = sContent.Replace(" ", "-");
            List<string> list = new List<string> { "/", "?", "&", ":", "#", "*", "\"", "@", "%", "$", "!", "~" };
            sContent = list.Aggregate(sContent, (current, str) => current.Replace(str, "-"));

            const string filter = ".-_[]0123456789abcdefghijklmnopqrstuvwxyz";
            string s = "";
            l = sContent.Length;
            for (int i = 0; i < l; i++)
            {
                if (filter.IndexOf(sContent[i]) >= 0)
                {
                    s = s + sContent[i];
                }
            }

            return s;
        }

        public static string DinhDangSo(object so, int soSauDauPhay, bool removeSign = false, bool isRound = false, bool kieuSoVietNam = true)
        {
            string result = "";
            bool flagSign = false;
            string text = Convert.ToString(so, new CultureInfo("en-US"));
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {

                double num = Convert.ToDouble(text);
                if (isRound)
                {
                    num = Math.Round(num);
                    text = Convert.ToString(num, new CultureInfo("en-US"));
                }
                if (num == 0.0)
                {
                    return result;
                }
            }
            else if (string.IsNullOrEmpty(text))
            {
                return result;
            }

            if (so != null)
            {
                int num2 = text.IndexOf("-");
                if (num2 >= 0)
                {
                    flagSign = true;
                    text = text.Replace("-", "");
                }

                int num3 = text.IndexOf(".");
                int num4 = 0;
                string text2 = "";
                if (num3 >= 0)
                {
                    text2 = text.Substring(num3 + 1, text.Length - num3 - 1);
                    text = text.Substring(0, num3);
                }

                int length = text.Length;
                if (text.Length > 3)
                {
                    for (num2 = length; num2 > 1; num2--)
                    {
                        num4++;
                        if (num4 % 3 == 0)
                        {
                            text = !kieuSoVietNam ? text.Insert(num2 - 1, ",") : text.Insert(num2 - 1, ".");
                        }
                    }
                }

                if (soSauDauPhay >= 0)
                {
                    string text3 = "";
                    for (num3 = 0; num3 < soSauDauPhay; num3++)
                    {
                        text3 = ((num3 >= text2.Length) ? (text3 + "0") : (text3 + text2[num3]));
                    }

                    text2 = text3;
                }

                if (text2 != "")
                {
                    text = !kieuSoVietNam ? (text + "." + text2) : (text + "," + text2);
                }

                if (flagSign && !removeSign)
                {
                    text = "-" + text;
                }

                result = text;
            }

            return result;
        }

        // Hàm dùng để convert chữ tiếng việt có dấu thành chữ tiếng việt không dấu.
        public static string ConvertVN(string strVietNamese)
        {
            const string FindText = "/:*?<>|áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "-------aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            if (string.IsNullOrEmpty(strVietNamese)) return string.Empty;
            while ((index = strVietNamese.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(strVietNamese[index]);
                strVietNamese = strVietNamese.Replace(strVietNamese[index], ReplText[index2]);
            }
            return strVietNamese;
        }

        public static string GetExcelColumnName(int colIndex)
        {
            string columnName = string.Empty;

            while (colIndex > 0)
            {
                int module = (colIndex - 1) % 26;
                columnName = Convert.ToChar('A' + module) + columnName;
                colIndex = (colIndex - module) / 26;
            }
            return columnName;
        }
    }
}
