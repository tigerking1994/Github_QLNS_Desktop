using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace VTS.QLNS.CTC.Utility
{
    public class DateUtils
    {
        public const string DATE_FORMAT = "dd/MM/yyyy";
        public static string GetCurrentDateReport()
        {
            return $"ngày {DateTime.Today.Day} tháng {DateTime.Today.Month} năm {DateTime.Today.Year}";
        }

        public static string GetFormatDateReport()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }

        public static DateTime StartOfDay(DateTime theDate)
        {
            return theDate.Date;
        }

        public static DateTime EndOfDay(DateTime theDate)
        {
            return theDate.Date.AddDays(1).AddTicks(-1);
        }

        public static string Format(DateTime? date, string format = "dd/MM/yyyy")
        {
            return date.HasValue ? date.Value.ToString(format) : string.Empty;
        }

        public static string FormatDateReport(DateTime? date)
        {
            if (date is null) return string.Empty;
            var month = date.Value.Month.ToString();
            var day = date.Value.Day.ToString();
            var year = date.Value.Year.ToString();
            if (date.Value.Month == 1 || date.Value.Month == 2)
            {
                month = "0" + date.Value.Month;
            }

            if (date.Value.Day < 10)
            {
                day = "0" + date.Value.Day;
            }
            return $"Ngày {day} tháng {month} năm {year}";
        }

        public static int TinhNamThamNien(DateTime? ngayNn, DateTime? ngayXn, DateTime? ngayTn, int thangTnn, int thangHienTai, int namHienTai)
        {
            int namThamNien = 0;
            if (ngayNn != null)
            {
                DateTime ngayNhapNgu = (DateTime)ngayNn;
                if (ngayXn == null && ngayTn == null)
                {
                    var monthDiff = ((namHienTai - ngayNhapNgu.Year) * 12) + thangHienTai - ngayNhapNgu.Month + 1 + thangTnn;
                    if (monthDiff % 12 >= 1)
                    {
                        namThamNien = monthDiff / 12;
                    }
                    else
                    {
                        namThamNien = monthDiff / 12 - 1;
                    }
                }
                else
                {
                    DateTime ngayXuatNgu = (DateTime)ngayXn;
                    if (ngayTn == null)
                    {
                        var monthDiff = ((ngayXuatNgu.Year - ngayNhapNgu.Year) * 12) + ngayXuatNgu.Month - ngayNhapNgu.Month + thangTnn;
                        if (monthDiff % 12 >= 1)
                        {
                            namThamNien = monthDiff / 12;
                        }
                        else
                        {
                            namThamNien = monthDiff / 12 - 1;
                        }
                    }
                    else
                    {
                        // có ngày tn
                        DateTime ngayTaiNgu = (DateTime)ngayTn;
                        var monthDiffLan1 = ((ngayXuatNgu.Year - ngayNhapNgu.Year) * 12) + ngayXuatNgu.Month - ngayNhapNgu.Month;
                        var monthNgoaiQD = ((ngayTaiNgu.Year - ngayXuatNgu.Year) * 12) + ngayTaiNgu.Month - ngayXuatNgu.Month;
                        if (monthNgoaiQD <= 12)
                        {
                            var monthDiff = ((namHienTai - ngayNhapNgu.Year) * 12) + thangHienTai - ngayNhapNgu.Month + 1 + thangTnn;
                            if (monthDiff % 12 >= 1)
                            {
                                namThamNien = monthDiff / 12;
                            }
                            else
                            {
                                namThamNien = monthDiff / 12 - 1;
                            }
                        }
                        else
                        {
                            if (monthDiffLan1 < 6)
                            {
                                var monthDiff = ((namHienTai - ngayTaiNgu.Year) * 12) + thangHienTai - ngayTaiNgu.Month + 1 + thangTnn;
                                if (monthDiff % 12 >= 1)
                                {
                                    namThamNien = monthDiff / 12;
                                }
                                else
                                {
                                    namThamNien = monthDiff / 12 - 1;
                                }
                            }
                            else
                            {
                                var monthDiff = ((namHienTai - ngayTaiNgu.Year) * 12) + thangHienTai - ngayTaiNgu.Month + 1 + thangTnn + monthDiffLan1;
                                if (monthDiff % 12 >= 1)
                                {
                                    namThamNien = monthDiff / 12;
                                }
                                else
                                {
                                    namThamNien = monthDiff / 12 - 1;
                                }
                            }
                        }
                    }
                }
            }
            return namThamNien;
        }

        public static DateTime? CheckDateFormatAndConverter(string text)
        {
            DateTime? dDateOut;
            if (text.IsEmpty()) return null;
            var regex = "^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$";
            if (Regex.Match(text, regex, RegexOptions.IgnoreCase).Success)
            {
                var arrText = text.Split("/");
                string textOutput = string.Empty;
                for (int i = 0; i < arrText.Length; i++)
                {
                    string textFormat;
                    if (i < arrText.Length - 1)
                    {
                        textFormat = arrText[i].PadLeft(2, '0') + "/";
                    }
                    else
                    {
                        textFormat = arrText[i];
                    }
                    textOutput += textFormat;
                }
                dDateOut = DateTime.ParseExact(textOutput, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dDateOut = null;
            }

            return dDateOut;
        }
    }
}
