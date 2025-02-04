using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Helper
{
    public static class DynamicMLNS
    {
        public static MLNSColumnDisplay SettingColumnVisibility(List<string> chiTietTois)
        {
            MLNSColumnDisplay columnDisplay = new MLNSColumnDisplay();

            int columnIndexMax = GetMaxColumnByChiTietToi(chiTietTois);
            if (columnIndexMax == 0)
                columnIndexMax = (int)MLNSFiled.TNG3;

            foreach (var prop in columnDisplay.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    MLNSColumnAttribute attribute = prop.GetCustomAttributes(true).First() as MLNSColumnAttribute;
                    if (attribute.ColumnIndex > columnIndexMax)
                        prop.SetValue(columnDisplay, Visibility.Collapsed);
                }
            }
            return columnDisplay;
        }

        public static string FormatLevel(string input)
        {
            switch (input)
            {
                case "Ngành":
                    return "NG";
                case "Mục":
                    return "M";
                case "Tiểu mục":
                    return "TM";
                case "Tiết mục":
                    return "TTM";
                case "Tiểu ngành":
                    return "TNG";
                case "Tiểu ngành 1":
                    return "TNG1";
                case "Tiểu ngành 2":
                    return "TNG2";
                case "Tiểu ngành 3":
                    return "TNG3";
                default:
                    return "TNG3";
            }
        }

        public static string FormatLevelAllocation(string input)
        {
            switch (input)
            {
                case "NG":
                    return "Ngành";
                case "M":
                    return "Mục";
                case "TM":
                    return "Tiểu mục";
                case "TTM":
                    return "Tiết mục";
                case "TNG":
                    return "Tiểu ngành";
                case "TNG1":
                    return "Tiểu ngành 1";
                case "TNG2":
                    return "Tiểu ngành 2";
                case "TNG3":
                    return "Tiểu ngành 3";
                default:
                    return "NG";
            }
        }

        public static MLNSColumnDisplay SettingColumnVisibilityByString(string chiTietTois)
        {
            chiTietTois = FormatLevel(chiTietTois);

            MLNSColumnDisplay columnDisplay = new MLNSColumnDisplay();

            int columnIndexMax = 0;

            MLNSFiled field;
            if (Enum.TryParse(chiTietTois, out field))
                if ((int)field > columnIndexMax)
                    columnIndexMax = (int)field;

            foreach (var prop in columnDisplay.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Length > 0)
                {
                    MLNSColumnAttribute attribute = prop.GetCustomAttributes(true).First() as MLNSColumnAttribute;
                    if (attribute.ColumnIndex > columnIndexMax)
                        prop.SetValue(columnDisplay, Visibility.Collapsed);
                }
            }
            return columnDisplay;
        }

        public static int GetColumnIndexByChiTietToi(string chiTietToi)
        {
            int columnIndex = 0;
            MLNSFiled field;
            if (Enum.TryParse(chiTietToi, out field))
                columnIndex = (int)field;
            return columnIndex;
        }

        public static int GetColumnIndexByFullNameMLNS(string chiTietToiFormat)
        {
            var chiTietToi = FormatLevel(chiTietToiFormat);
            int columnIndex = 0;
            MLNSFiled field;
            if (Enum.TryParse(chiTietToi, out field))
                columnIndex = (int)field;
            return columnIndex;
        }

        public static int GetColumnIndex<T>(T model)
        {
            int columnIndex = 0;
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetValue(model) != null && !string.IsNullOrEmpty(prop.GetValue(model).ToString()))
                {
                    string propName = string.Empty;
                    if (prop.Name.StartsWith("S"))
                        propName = prop.Name.Substring(1).ToUpper();
                    else propName = prop.Name.ToUpper();
                    MLNSFiled field;
                    if (Enum.TryParse(propName, out field))
                        if ((int)field > columnIndex)
                            columnIndex = (int)field;
                }
            }
            return columnIndex;
        }

        public static List<ComboboxItem> CreateMLNSReportSetting(string chiTietToi, bool isSettlementReport = false)
        {
            int columnIndex = GetColumnIndexByChiTietToi(chiTietToi);
            List<ComboboxItem> result = new List<ComboboxItem>();
            foreach (int i in Enum.GetValues(typeof(MLNSFiled)))
            {
                if ((isSettlementReport && (int)MLNSFiled.M <= i && i <= (int)MLNSFiled.NG)
                    || (i <= columnIndex && i >= (int)MLNSFiled.NG))
                {
                    result.Add(new ComboboxItem(((MLNSFiled)i).ToString(), ((MLNSFiled)i).ToString()));
                }
            }
            return result;
        }

        public static int GetMaxColumnByChiTietToi(List<string> chiTietTois)
        {
            int columnIndexMax = 0;
            foreach (var item in chiTietTois)
            {
                MLNSFiled field;
                if (Enum.TryParse(item, out field))
                    if ((int)field > columnIndexMax)
                        columnIndexMax = (int)field;
            }
            return columnIndexMax;
        }

        public static string GetMaxNameColumnByChiTietToi(List<string> chiTietTois)
        {
            int columnIndexMax = 0;
            string chiTietToi = "NG";
            foreach (var item in chiTietTois)
            {
                MLNSFiled field;
                if (Enum.TryParse(item, out field))
                    if ((int)field > columnIndexMax)
                    {
                        columnIndexMax = (int)field;
                        chiTietToi = item;
                    }
            }
            return chiTietToi;
        }
    }
}
