using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public class BudgetCatalogSelectedToStringConvert
    {
        public static string GetValueSelected(ObservableCollection<NsMuclucNgansachModel> data, bool isOnlyChild = false)
        {
            if (data.Count > 0)
            {
                if (isOnlyChild)
                {
                    return string.Join(",", data.Where(n => n.IsSelected && !n.IsParent).Select(n => n.Lns).ToList());                }
                else
                {
                    return string.Join(",", data.Where(n => n.IsSelected).Select(n => n.Lns).ToList());
                }
            }
            return string.Empty;
        }


        public static string GetValueSelectedSoOn(ObservableCollection<NsMuclucNgansachModel> data, IEnumerable<string> dataAdd)
        {
            if (data.Count > 0)
            {
                var fullList = data.Where(n => n.IsSelected).Select(n => n.Lns).Union(dataAdd).OrderBy(x => x).Distinct();
                return string.Join(",", fullList);
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<NsMuclucNgansachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (NsMuclucNgansachModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.Lns);
            }
        }

        public static void SetAllCheckboxTrueOrFalse(ObservableCollection<NsMuclucNgansachModel> data, bool value)
        {
            foreach (NsMuclucNgansachModel item in data)
            {
                item.IsSelected = value;
            }
        }
    }
}
