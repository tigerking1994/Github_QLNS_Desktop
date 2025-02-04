using ControlzEx.Standard;
using FlexCel.Report;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.UserFunction
{
    public class FormatNumber : TFlexCelUserFunction
    {
        private int _maxDecimalPlace;
        private string _defaultIfZero;
        private ExportType _exportType;

        /*public FormatNumber(int dvt)
        {
            _exportType = ExportType.PDF;
            _maxDecimalPlace = dvt == 1 ? 1 : dvt == 1000 ? 3 : 9;
            _defaultIfZero = string.Empty;
        }*/

        public FormatNumber(int dvt, ExportType exportType)
        {
            _maxDecimalPlace = dvt == 1 ? 1 : dvt == 1000 ? 3 : 9;
            _defaultIfZero = string.Empty;
            _exportType = exportType;
        }

        /*public FormatNumber(int dvt, string defaultIfZero)
        {
            _exportType = ExportType.PDF;
            _maxDecimalPlace = dvt == 1 ? 1 : dvt == 1000 ? 3 : 9;
            _defaultIfZero = defaultIfZero;
        }*/

        public FormatNumber(int dvt, string defaultIfZero, ExportType exportType)
        {
            _exportType = ExportType.PDF;
            _maxDecimalPlace = dvt == 1 ? 1 : dvt == 1000 ? 3 : 9;
            _defaultIfZero = defaultIfZero;
        }

        public FormatNumber(int decimalPlace)
        {
            _exportType = ExportType.PDF;
            _maxDecimalPlace = decimalPlace;
            _defaultIfZero = "0,00";
        }

        public override object Evaluate(object[] parameters)
        {
            if (parameters.Length == 0) return string.Empty;
            if (parameters[0] is string) return _defaultIfZero;
            decimal val = 0;
            if (parameters[0] != null)
            {
                if (parameters[0] is decimal decimalParam)
                {
                    val = decimalParam;
                }
                else if (parameters[0] is System.DBNull)
                {
                    return string.Empty;
                }
                else if(parameters[0] is int intParam)
                {
                    val = intParam;
                }
                else if (parameters[0] is long longParam)
                {
                    val = longParam;
                }
                else
                {
                    if (Math.Abs(Convert.ToDouble(parameters[0]) % 1) <= (Double.Epsilon * 100))
                    {
                        val = Convert.ToInt64(parameters[0]);
                    } else
                    {
                        val = Convert.ToDecimal(parameters[0]);
                    }
                }
            }

            if (val == 0) return _defaultIfZero;
            return formatNumber(val);
        }

        private object formatNumber(decimal number)
        {
            if (_exportType == ExportType.PDF || _exportType == ExportType.EXCEL)
            {
                CultureInfo vi = new CultureInfo("vi-VN", true);
                vi.NumberFormat.NumberDecimalSeparator = ",";
                vi.NumberFormat.NumberGroupSeparator = ".";
                return Regex.Replace(String.Format(vi, "{0:n}", number),
                                     @"[" + vi.NumberFormat.NumberDecimalSeparator + "]?0+$", "");
            }
            return number;
        }
    }
}
