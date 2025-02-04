using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public class BudgetLhCatalogSelectedToStringConvert
    {
        public static string GetValueSelected(ObservableCollection<TnDanhMucLoaiHinhModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.Lns).ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<TnDanhMucLoaiHinhModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (TnDanhMucLoaiHinhModel item in data)
            {
                if (selectedValues.Contains(item.Lns))
                {
                    item.IsSelected = true;
                }
            }
        }
    }
}
