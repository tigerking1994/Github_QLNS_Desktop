using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhCanCuModelControlService : GenericControlBaseService<CauHinhCanCuModel, NsCauHinhCanCu, CauHinhCanCuService>
    {
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(CauHinhCanCuModel.IThietLap)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Theo CT", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Nhiều chứng từ", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "Lũy kế đến 1 chứng từ", ValueItem = "3" },
                };
            }
            else if (property.Name.Equals(nameof(CauHinhCanCuModel.SModule)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Số nhu cầu", ValueItem = TypeModuleCanCu.DEMAND },
                    //new ComboboxItem { DisplayItem = "Phân bổ số kiểm tra", ValueItem = TypeModuleCanCu.DISTRIBUTION },
                    new ComboboxItem { DisplayItem = "Dự toán đầu năm", ValueItem = TypeModuleCanCu.PLAN_BEGIN_YEAR }
                };
            }
            else if (property.Name.Equals(nameof(CauHinhCanCuModel.IIDMaChucNang)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Dự toán", ValueItem = "BUDGET_ESTIMATE" },
                    new ComboboxItem { DisplayItem = "Quyết toán", ValueItem = "BUDGET_SETTLEMENT" },
                    //new ComboboxItem { DisplayItem = "Cấp phát", ValueItem = "BUDGET_ALLOCATION" },
                    new ComboboxItem { DisplayItem = "Số nhu cầu", ValueItem = "BUDGET_DEMANDCHECK_DEMAND" },
                    new ComboboxItem { DisplayItem = "Số kiểm tra", ValueItem = "BUDGET_DEMANDCHECK_CHECK" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
