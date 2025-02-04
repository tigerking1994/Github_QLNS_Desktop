using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Helper
{
    public static class ValidateViewModelHelper
    {
        public static bool Validate<T>(T obj)
        {
            if (obj == null) return false;
            List<string> lstMess = new List<string>();

            if (!ValidateProcess(obj, ref lstMess))
            {
                MessageBoxHelper.Error(string.Join("\n", lstMess));
                return false;
            }
            return true;
        }

        public static bool Validate<T>(IEnumerable<T> lstObj)
        {
            if (lstObj == null) return false;
            List<string> lstMess = new List<string>();
            bool bStatus = true;
            foreach (var item in lstObj)
            {
                if (!bStatus) break;
                bStatus &= ValidateProcess(item, ref lstMess);
            }

            if (!bStatus)
            {
                MessageBoxHelper.Error(string.Join("\n", lstMess));
                return false;
            }
            return true;
        }

        private static bool ValidateProcess<T>(T obj, ref List<string> lstMess)
        {
            bool bStatus = true;
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length == 0) continue;
                ValidateAttribute atr = prop.GetCustomAttributes(true).Where(x => x.GetType().Equals(typeof(ValidateAttribute))).FirstOrDefault() as ValidateAttribute;
                if (atr == null) continue;
                var value = prop.GetValue(obj);
                if (!OnCheckType(value, prop, atr, ref lstMess)) { bStatus &= false; continue; }
                if (!OnCheckCondition(value, obj, prop, atr, ref lstMess)) { bStatus &= false; continue; }
            }
            return bStatus;
        }

        private static bool OnCheckType(object value, PropertyInfo prop, ValidateAttribute atr, ref List<string> lstMess)
        {
            if (value == null) return true;
            if (!ValidateType(value, prop, atr))
            {
                lstMess.Add(string.Format(Resources.MsgErrorFormat, atr.DisplayName));
                return false;
            }
            return true;
        }

        private static bool OnCheckCondition(object value, object item, PropertyInfo prop, ValidateAttribute atr, ref List<string> lstMess)
        {
            if (!string.IsNullOrEmpty(atr.DefaultValue) && value == null
                && ValidateType(atr.DefaultValue, prop, atr))
            {
                if (atr.DataType == DATA_TYPE.Date)
                {
                    DateTime objDate = DateTime.Now;
                    DateTime.TryParseExact(Convert.ToString(atr.DefaultValue), DateUtils.DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out objDate);
                    prop.SetValue(item, objDate);
                }
                else
                {
                    prop.SetValue(item, StringToType(atr.DefaultValue, prop.PropertyType));
                }

                value = atr.DefaultValue;
            }
            if (atr.IsRequired && string.IsNullOrEmpty(Convert.ToString(value).Trim()))
            {
                lstMess.Add(string.Format(Resources.MsgErrorRequire, atr.DisplayName));
                return false;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(value).Trim()) && atr.MaxLength != 0 && Convert.ToString(value).Trim().Length > atr.MaxLength)
            {
                lstMess.Add(string.Format(Resources.MsgCheckLengthInput, atr.DisplayName, atr.MaxLength));
                return false;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(value).Trim()) && !string.IsNullOrEmpty(atr.RegularExpression)
                && !Regex.IsMatch(Convert.ToString(value).Trim(), atr.RegularExpression))
            {
                lstMess.Add(string.Format(Resources.MsgInputError, atr.DisplayName));
                return false;
            }
            return true;
        }

        private static bool ValidateType(object value, PropertyInfo prop, ValidateAttribute atr)
        {
            switch (atr.DataType)
            {
                case DATA_TYPE.Int:
                    if (int.TryParse(Convert.ToString(value).Trim(), out int objInt)) return true;
                    break;
                case DATA_TYPE.Double:
                    if (double.TryParse(Convert.ToString(value).Trim(), out double objDouble)) return true;
                    break;
                case DATA_TYPE.Date:
                    if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?)
                        || DateTime.TryParseExact(Convert.ToString(value).Trim(), DateUtils.DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime objDate))
                        return true;
                    break;
                case DATA_TYPE.Decimal:
                    if (decimal.TryParse(Convert.ToString(value).Trim(), out decimal objDecimal)) return true;
                    break;
                case DATA_TYPE.Guid:
                    if (Guid.TryParse(Convert.ToString(value).Trim(), out Guid objGuid)) return true;
                    break;
                case DATA_TYPE.Bool:
                    if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?)
                        || bool.TryParse(Convert.ToString(value).Trim(), out bool objBool))
                        return true;
                    break;
                default:
                    return true;
            }
            return false;
        }

        private static object StringToType(string value, Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType == null)
            {
                return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            }
            return string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
        }
    }
}
