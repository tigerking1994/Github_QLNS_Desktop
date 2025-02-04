using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class BhDmCauHinhThamSoModelControlService : GenericControlBaseService<BhDmCauHinhThamSoModel, BhDmCauHinhThamSo, BhDmCauHinhThamSoService>
    {
        public override void CustomValueProps(BhDmCauHinhThamSoModel newRow, BhDmCauHinhThamSoModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.NewGuid();
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmCauHinhThamSoModel.BTrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0"},
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1"},

                };
            }
            return new ObservableCollection<ComboboxItem>();
        }       
    }
}
