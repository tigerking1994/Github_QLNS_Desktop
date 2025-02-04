using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NguonNganSachModelControlService : GenericControlBaseService<NguonNganSachModel, Core.Domain.NsNguonNganSach, NsNguonNganSachService>
    {
        public override void CustomValueProps(NguonNganSachModel newRow, NguonNganSachModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.IIdMaNguonNganSach = null;
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NguonNganSachModel.ITrangThai)))
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
