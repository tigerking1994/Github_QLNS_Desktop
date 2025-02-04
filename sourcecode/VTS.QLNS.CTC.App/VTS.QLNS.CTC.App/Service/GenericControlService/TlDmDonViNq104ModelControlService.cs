using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class TlDmDonViNq104ModelControlService : GenericControlBaseService<TlDmDonViNq104Model, TlDmDonViNq104, TlDmDonViNq104Service>
    {
        public override void CustomValueProps(TlDmDonViNq104Model newRow, TlDmDonViNq104Model currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.Id = Guid.Empty;
            if (sourceVM.Items == null || sourceVM.Items.Count == 0)
                newRow.ITrangThai = true;
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += TlDmDonViModel_PropertyChanged;
            }
        }

        private void TlDmDonViModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<TlDmDonViNq104Model> models = sourceVM.Items;
            TlDmDonViNq104Model model = sender as TlDmDonViNq104Model;
            if (args.PropertyName == nameof(TlDmDonViNq104Model.IsDeleted))
            {
                TlDmDonViModel_OnDeleted(model, models);
            }
        }

        private void TlDmDonViModel_OnDeleted(TlDmDonViNq104Model model, IEnumerable<TlDmDonViNq104Model> models)
        {
            TlDmDonViNq104Model parent = models.FirstOrDefault(i => i.MaDonVi.Equals(model.ParentId));
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => parent.MaDonVi.Equals(i.ParentId) && !i.IsDeleted);
            parent.BHangCha = hasChild;
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            TlDmDonViNq104Service mucLucNganSachService = sourceVM._service;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            TlDmDonViNq104Model parent = sourceVM.SelectedItem;
            TlDmDonViNq104Model newRow = new TlDmDonViNq104Model();
            newRow.Id = Guid.Empty;
            newRow.MaDonVi = string.Empty;
            newRow.TenDonVi = string.Empty;
            newRow.ParentId = parent.MaDonVi;
            newRow.TenDonViCha = parent.TenDonVi;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += TlDmDonViModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(TlDmDonViNq104Model.TenDonViCha)))
            {
                var TlDmDonViService = sourceVM._serviceProvider.GetService(typeof(TlDmDonViNq104Service));
                GenericControlCustomViewModel<TlDmDonViNq104Model, TlDmDonViNq104, TlDmDonViNq104Service> dialogVM = new GenericControlCustomViewModel<TlDmDonViNq104Model, TlDmDonViNq104, TlDmDonViNq104Service>
                    ((TlDmDonViNq104Service)TlDmDonViService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục phân hộ";
                dialogVM.Title = "Danh mục phân hộ";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.MaDonVi.Equals(sourceVM.SelectedItem.ParentId)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    TlDmDonViNq104Model data = obj as TlDmDonViNq104Model;
                    sourceVM.SelectedItem.TenDonViCha = data.TenDonVi;
                    sourceVM.SelectedItem.ParentId = data.MaDonVi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
