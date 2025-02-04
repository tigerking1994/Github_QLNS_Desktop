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
    public class BhDmThamDinhQuyetToanDanhMucModelControlService : GenericControlBaseService<BhDmThamDinhQuyetToanModel, BhDmThamDinhQuyetToan, BhDmThamDinhQuyetToanService>
    {

        public override void CustomValueProps(BhDmThamDinhQuyetToanModel newRow, BhDmThamDinhQuyetToanModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            //newRow.DanhMucNganSach = new List<BhDmMucLucNganSachModel>();
            //newRow.SBH_XauNoiMa = null;
            //newRow.SNoiDung = null;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmThamDinhQuyetToanModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "False" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "True" }
                };
            }
            else if (property.Name.Equals(nameof(BhDmThamDinhQuyetToanModel.IKieuChu)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "In đậm", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Thường", ValueItem = "3" },
                    new ComboboxItem { DisplayItem = "In nghiêng", ValueItem = "2" }
                };
            }

            return new ObservableCollection<ComboboxItem>();
        }
    }
}
