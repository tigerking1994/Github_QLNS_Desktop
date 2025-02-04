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
    public class TlDmThueThuNhapCaNhanNq104ModelControllerService : GenericControlBaseService<TlDmThueThuNhapCaNhanNq104Model, Core.Domain.TlDmThueThuNhapCaNhanNq104, TlDmThueThuNhapCaNhanNq104Service>
    {
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Name.Equals(nameof(TlDmThueThuNhapCaNhanNq104Model.IIsThueThang)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Thuế năm", ValueItem = "0"},
                    new ComboboxItem {DisplayItem = "Thuế tháng", ValueItem = "1"}
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override void OrderData()
        {
            var datas = sourceVM.Items.ToList();
            sourceVM.Items = new ObservableCollection<TlDmThueThuNhapCaNhanNq104Model>(datas.OrderByDescending(n => n.IIsThueThang));
        }
    }
}
