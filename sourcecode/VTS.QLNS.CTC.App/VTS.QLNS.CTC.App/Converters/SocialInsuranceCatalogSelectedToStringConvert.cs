using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Converters
{
    public class SocialInsuranceCatalogSelectedToStringConvert
    {
        public static string GetValueSelected(ObservableCollection<BhDmMucLucNganSachModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.SLNS).ToList());
            }
            return string.Empty;
        }

        public static void SetCheckboxSelected(ObservableCollection<BhDmMucLucNganSachModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = (selectedValues.Contains(item.SLNS));
            }
        }

        public static void SetAllCheckboxTrueOrFalse(ObservableCollection<BhDmMucLucNganSachModel> data, bool value)
        {
            foreach (BhDmMucLucNganSachModel item in data)
            {
                item.IsSelected = value;
            }
        }
    }
}
