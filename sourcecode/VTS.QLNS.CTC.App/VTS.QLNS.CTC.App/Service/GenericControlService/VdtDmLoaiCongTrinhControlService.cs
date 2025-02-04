using System;
using System.Collections.Generic;
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
    public class VdtDmLoaiCongTrinhControlService : GenericControlBaseService<VdtDmLoaiCongTrinhModel, Core.Domain.VdtDmLoaiCongTrinh, DmLoaiCongTrinhService>
    {
        public override void CustomValueProps(VdtDmLoaiCongTrinhModel newRow, VdtDmLoaiCongTrinhModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.IIdLoaiCongTrinh = Guid.NewGuid();
        }

        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);
            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            VdtDmLoaiCongTrinhModel parent = sourceVM.SelectedItem;
            VdtDmLoaiCongTrinhModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.IIdLoaiCongTrinh = Guid.NewGuid();
            newRow.IIdParent = parent.IIdLoaiCongTrinh;
            newRow.IsModified = true;
            newRow.PropertyChanged += Item_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(VdtDmLoaiCongTrinhModel.TenLoaiCongTrinhCha)))
            {
                var DmLoaiCongTrinhService = sourceVM._serviceProvider.GetService(typeof(IDmLoaiCongTrinhService));
                GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, VdtDmLoaiCongTrinh, DmLoaiCongTrinhService> dialogVM = new GenericControlCustomViewModel<VdtDmLoaiCongTrinhModel, VdtDmLoaiCongTrinh, DmLoaiCongTrinhService>
                    ((DmLoaiCongTrinhService)DmLoaiCongTrinhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục loại công trình";
                dialogVM.Title = "Danh mục loại công trình";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIdLoaiCongTrinh.Equals(sourceVM.SelectedItem.IIdParent)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    VdtDmLoaiCongTrinhModel parent = obj as VdtDmLoaiCongTrinhModel;
                    sourceVM.SelectedItem.IIdParent = parent.IIdLoaiCongTrinh;
                    sourceVM.SelectedItem.Parent = parent;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
