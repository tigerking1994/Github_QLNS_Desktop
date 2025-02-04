using System.Collections.ObjectModel;
using System.Reflection;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhDMCoSoYTeModelControlService : GenericControlBaseService<BhDmCoSoYTeModel, BhDmCoSoYTe, BhDmCoSoYTeService>
    {
        public override void CustomValueProps(BhDmCoSoYTeModel newRow, BhDmCoSoYTeModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            //newRow.DanhMucNganSach = new List<BhDmMucLucNganSachModel>();
            newRow.IIDMaCoSoYTe = null;
            newRow.STenCoSoYTe = null;
            newRow.ITrangThai = 1;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmCoSoYTeModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }

            return new ObservableCollection<ComboboxItem>();
        }
    }
}
