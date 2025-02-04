using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public static class DateTimeExtension
    {
        public static string ToStringTimeStampMiliSeconds(this DateTime dt) => dt.ToString("ddMMyyyyHHmmssfff");
        public static string ToStringTimeStamp(this DateTime dt) => dt.ToString("ddMMyyyyHHmmss"); 
        public static string ToStringMinute(this DateTime dt) => dt.ToString("ddMMyyyyHHmm");

        public static string ToShortDateString(this DateTime? dt, string defaultValue = null) => dt.HasValue ? dt.GetValueOrDefault().ToShortDateString() : defaultValue ?? string.Empty;

        public static string ToStringDate(this DateTime dt) => dt.ToString("dd/MM/yyyy");

        public static string ToStringDateText(this DateTime dt) => dt.ToString("ddMMyyyy");

        public static string ToStringMonth(this DateTime dt, string defaultValue = null) => dt.ToString("MM/yyyy");

        public static string ToStringMonthFull(this DateTime dt, string defaultValue = null) => string.Format("Tháng {0} năm {1}", (object)dt.Month.ToString("0#"), (object)dt.Year);

        public static int GetQuy(int month)
        {
            if (month <= 0)
                return 1;
            if (month > 12)
                return 4;
            if (month == 1 || month == 2 || month == 3)
                return 1;
            if (month == 4 || month == 5 || month == 6)
                return 2;
            if (month == 7 || month == 8 || month == 9)
                return 3;
            return month == 10 || month == 11 || month == 12 ? 4 : 1;
        }

        public static string ToStringDateDelta(this DateTime dateCreated)
        {
            TimeSpan timeSpan = DateTime.Now - dateCreated;
            string str;
            if (timeSpan.Days > 0)
                str = string.Format("{0}d {1}h {2}m {3}s", (object)timeSpan.Days, (object)timeSpan.Hours, (object)timeSpan.Minutes, (object)timeSpan.Seconds);
            else
                str = timeSpan.Hours <= 0 ? string.Format("{0}m {1}s", (object)timeSpan.Minutes, (object)timeSpan.Seconds) : string.Format("{0}h {1}m {2}s", (object)timeSpan.Hours, (object)timeSpan.Minutes, (object)timeSpan.Seconds);
            return str;
        }

        public static class TimeConst
        {
            public enum Types
            {
                CA_NAM = 0,
                THANG_1 = 1,
                THANG_2 = 2,
                THANG_3 = 3,
                THANG_4 = 4,
                THANG_5 = 5,
                THANG_6 = 6,
                THANG_7 = 7,
                THANG_8 = 8,
                THANG_9 = 9,
                THANG_10 = 10,
                THANG_11 = 11,
                THANG_12 = 12,
                QUY_1 = 21,
                QUY_2 = 22,
                QUY_3 = 23,
                QUY_4 = 24
            }

            public struct Names
            {
                public const string CA_NAM = "Cả năm";
                public const string THANG_1 = "Tháng 1";
                public const string THANG_2 = "Tháng 2";
                public const string THANG_3 = "Tháng 3";
                public const string THANG_4 = "Tháng 4";
                public const string THANG_5 = "Tháng 5";
                public const string THANG_6 = "Tháng 6";
                public const string THANG_7 = "Tháng 7";
                public const string THANG_8 = "Tháng 8";
                public const string THANG_9 = "Tháng 9";
                public const string THANG_10 = "Tháng 10";
                public const string THANG_11 = "Tháng 11";
                public const string THANG_12 = "Tháng 12";
                public const string QUY_1 = "Quý 1";
                public const string QUY_2 = "Quý 2";
                public const string QUY_3 = "Quý 3";
                public const string QUY_4 = "Quý 4";
            }
        }
    }
}
