using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NhDmNhiemVuChiModelControlService : GenericControlBaseService<NhDmNhiemVuChiModel, Core.Domain.NhDmNhiemVuChi, NhDmNhiemVuChiService>
    {
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(NhDmNhiemVuChiModel.ILoaiNhiemVuChi)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Mua sắm", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Dự án", ValueItem = "2" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
