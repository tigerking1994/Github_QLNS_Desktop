using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public static class CheckboxSelectedToStringConvert
    {
        public static string GetValueSelected<T>(ObservableCollection<T> data) where T : CheckBoxItem
        {
            if (data != null && data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsChecked == true).Select(n => n.ValueItem).ToList());
            }
            return string.Empty;
        }

        public static string GetValueSelected(ObservableCollection<DtChungTuModel> data)
        {
            if (data != null && data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsChecked == true).Select(n => n.Id.ToString()).ToList());
            }
            return string.Empty;
        }

        public static string GetValueSelected(ObservableCollection<CheckBoxTreeItem> data)
        {
            if (data != null && data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsChecked == true).Select(n => n.ValueItem).ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected<T>(ObservableCollection<T> data, string value) where T : CheckBoxItem
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            var selectedValues = value.Split(",").ToList();
            foreach (var item in data)
            {
                if (selectedValues.Contains(item.ValueItem))
                    item.IsChecked = true;
            }
        }

        public static void SetCheckboxSelected(ObservableCollection<DtChungTuModel> data, string value, bool isNamLuyKe, bool isAdjusted)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            if (isNamLuyKe && !isAdjusted && data.All(x => !string.IsNullOrEmpty(x.IIdDotNhan)))
            {
                foreach (var item in data)
                {
                    if (value.Contains(item.IIdDotNhan.ToString()))
                        item.IsChecked = true;
                }
            }
            else
            {
                var selectedValues = value.Split(",").ToList();
                foreach (var item in data)
                {
                    if (selectedValues.Contains(item.Id.ToString()))
                        item.IsChecked = true;
                }
            }
        }

    }
}