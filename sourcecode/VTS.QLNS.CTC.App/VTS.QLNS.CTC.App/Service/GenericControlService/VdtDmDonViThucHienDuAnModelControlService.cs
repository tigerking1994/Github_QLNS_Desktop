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
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class VdtDmDonViThucHienDuAnModelControlService : GenericControlBaseService<VdtDmDonViThucHienDuAnModel, VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService>
    {
        public override void CustomValueProps(VdtDmDonViThucHienDuAnModel newRow, VdtDmDonViThucHienDuAnModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.IIdDonVi = Guid.NewGuid();
        }

        public override void OnPropertyChanged()
        {
            foreach (var t in sourceVM.Items)
            {
                t.PropertyChanged += VdtDmDonViThucHienDuAnModel_PropertyChanged;
            }
        }

        private void VdtDmDonViThucHienDuAnModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            IEnumerable<VdtDmDonViThucHienDuAnModel> models = sourceVM.Items;
            VdtDmDonViThucHienDuAnModel nsMuclucNgansachModel = sender as VdtDmDonViThucHienDuAnModel;
            if (args.PropertyName == nameof(VdtDmDonViThucHienDuAnModel.IsDeleted))
            {
                VdtDmDonViThucHienDuAnModel_OnDeleteMLNS(nsMuclucNgansachModel, models);
            }
        }

        private void VdtDmDonViThucHienDuAnModel_OnDeleteMLNS(VdtDmDonViThucHienDuAnModel model, IEnumerable<VdtDmDonViThucHienDuAnModel> models)
        {
            VdtDmDonViThucHienDuAnModel parent = models.FirstOrDefault(i => i.IIdDonVi.Equals(model.IIdDonViCha));
            if (parent == null)
            {
                return;
            }
            bool hasChild = models.Any(i => i.IIdDonViCha.Equals(parent.IIdDonVi) && !i.IsDeleted);
            parent.BHangCha = hasChild;
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            VdtDmDonViThucHienDuAnService mucLucNganSachService = sourceVM._service;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);

            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            VdtDmDonViThucHienDuAnModel parent = sourceVM.SelectedItem;
            VdtDmDonViThucHienDuAnModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.IIdDonVi = Guid.NewGuid();
            newRow.IIdDonViCha = parent.IIdDonVi;
            newRow.TenDonViCha = parent.STenDonVi;
            newRow.IsModified = true;
            newRow.BHangCha = false;
            parent.BHangCha = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            newRow.PropertyChanged += VdtDmDonViThucHienDuAnModel_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(VdtDmDonViThucHienDuAnModel.TenDonViNS)))
            {
                var donviService = sourceVM._serviceProvider.GetService(typeof(INsDonViService));
                GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> dialogVM = new GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService>
                    ((NsDonViService)donviService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục đơn vị";
                dialogVM.Title = "Danh mục đơn vị";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMaDonVi.Equals(sourceVM.SelectedItem.IIDMaDonViNS)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    DonViModel data = obj as DonViModel;
                    sourceVM.SelectedItem.IIDMaDonViNS = data.IIDMaDonVi;
                    sourceVM.SelectedItem.TenDonViNS = data.TenDonVi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            } 
            else if (property.Name.Equals(nameof(VdtDmDonViThucHienDuAnModel.TenDonViCha)))
            {
                var donviService = sourceVM._serviceProvider.GetService(typeof(IVdtDmDonViThucHienDuAnService));
                GenericControlCustomViewModel<VdtDmDonViThucHienDuAnModel, VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService> dialogVM = new GenericControlCustomViewModel<VdtDmDonViThucHienDuAnModel, VdtDmDonViThucHienDuAn, VdtDmDonViThucHienDuAnService>
                    ((VdtDmDonViThucHienDuAnService)donviService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục đơn vị";
                dialogVM.Title = "Danh mục đơn vị";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIdMaDonVi.Equals(sourceVM.SelectedItem.IIdMaDonVi)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    VdtDmDonViThucHienDuAnModel data = obj as VdtDmDonViThucHienDuAnModel;
                    sourceVM.SelectedItem.IIdDonViCha = data.IIdDonVi;
                    sourceVM.SelectedItem.TenDonViCha = data.STenDonVi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
