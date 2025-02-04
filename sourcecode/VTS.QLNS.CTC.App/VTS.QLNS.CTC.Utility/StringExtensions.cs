using System;

namespace VTS.QLNS.CTC.Utility
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string text, string emptyValue = null) => string.IsNullOrWhiteSpace(text) || text.Equals(emptyValue);
        public static bool Contains(this string text, string value, StringComparison comparisonType) => text.IndexOf(value, comparisonType) >= 0;
        public static string[] Split(this string text, string value, StringSplitOptions options = StringSplitOptions.None) => text.Split(new string[] { value }, options);
    }
}
