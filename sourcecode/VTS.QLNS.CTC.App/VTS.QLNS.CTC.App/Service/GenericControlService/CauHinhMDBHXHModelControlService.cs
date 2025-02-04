using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhMDBHXHModelControlService : GenericControlBaseService<BhDmMucDongBHXHModel, BhDmMucDongBHXH, BhDmMucDongBHXHService>
    {
        public override void CustomValueProps(BhDmMucDongBHXHModel newRow, BhDmMucDongBHXHModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.DanhMucNganSach = new List<BhDmMucLucNganSachModel>();
            newRow.SMaMucDong = null;
            newRow.SBH_XauNoiMa = null;
            newRow.SNoiDung = null;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmMucDongBHXHModel.ITrangThai)))
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
