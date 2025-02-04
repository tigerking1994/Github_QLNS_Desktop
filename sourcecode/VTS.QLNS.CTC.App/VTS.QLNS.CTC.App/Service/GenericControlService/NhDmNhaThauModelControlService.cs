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
    public class NhDmNhaThauModelControlService : GenericControlBaseService<NhDmNhaThauModel, NhDmNhaThau, NhDmNhaThauService>
    {
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NhDmNhaThauModel.ILoai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "1 - Nhà thầu", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "2 - Đơn vị ủy thác", ValueItem = "2" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
