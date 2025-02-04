using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class BhDmCheDoBhxhModelControlService : GenericControlBaseService<BhDmCheDoBhxhModel, BhDmCheDoBhxh, BhDmCheDoBhxhService>
    {
        public override void CustomValueProps(BhDmCheDoBhxhModel newRow, BhDmCheDoBhxhModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.NewGuid();
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += CheDoBhxhModel_PropertyChanged;
            }
        }

        private void CheDoBhxhModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IEnumerable<BhDmCheDoBhxhModel> models = sourceVM.Items;
            BhDmCheDoBhxhModel nsMuclucNgansachModel = sender as BhDmCheDoBhxhModel;
            if (e.PropertyName == nameof(BhDmCheDoBhxhModel.IsDeleted))
            {
                CheDoBhxhModel_OnDeleteMLNS(nsMuclucNgansachModel, models);
            }
        }

        private void CheDoBhxhModel_OnDeleteMLNS(BhDmCheDoBhxhModel model, IEnumerable<BhDmCheDoBhxhModel> models)
        {
            BhDmCheDoBhxhModel parent = models.FirstOrDefault(i => i.Id.Equals(model.IIdCheDoCha));
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.IIdCheDoCha.Equals(parent.Id) && !i.IsDeleted);
            parent.BHangCha = hasChild;
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;

            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            BhDmCheDoBhxhModel parent = sourceVM.SelectedItem;
            BhDmCheDoBhxhModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.NewGuid();
            newRow.IIdCheDoCha = parent.Id;
            newRow.TenCheDoCha = parent.STenCheDo;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += CheDoBhxhModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }


        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDmCheDoBhxhModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
