using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DmCapBacModelControlService : GenericControlBaseService<DmCapBacModel, Core.Domain.DanhMuc, DmCapBacService>
    {
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DmCapBacModel.SType)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Chữ ký - Tên", ValueItem = DanhMucChuKy.TEN },
                    new ComboboxItem { DisplayItem = "Chữ ký - Chức danh", ValueItem = DanhMucChuKy.CHUC_DANH },
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
