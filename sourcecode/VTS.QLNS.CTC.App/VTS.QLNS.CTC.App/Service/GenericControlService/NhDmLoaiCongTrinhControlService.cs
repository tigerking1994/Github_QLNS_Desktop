using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Windows.Controls;
using VTS.QLNS.CTC.Utility;
using System.Reflection;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.View.Shared;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class NhDmLoaiCongTrinhControlService:GenericControlBaseService<NhDmLoaiCongTrinhModel, NhDmLoaiCongTrinh, NhDmLoaiCongTrinhService>
    {
        public override void CustomValueProps(NhDmLoaiCongTrinhModel newRow, NhDmLoaiCongTrinhModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            //newRow.Id = Guid.NewGuid();
            newRow.Id = Guid.Empty;
        }
        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);
            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);
            NhDmLoaiCongTrinhModel parent = sourceVM.SelectedItem;
            NhDmLoaiCongTrinhModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.Id = Guid.Empty;
            newRow.IIdParent = parent.Id;
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
           if(property.Name.Equals(nameof(NhDmLoaiCongTrinhModel.TenLoaiCongTrinhCha)))
            {
                var NhDmLoaiCongTrinhService = sourceVM._serviceProvider.GetService(typeof(INhDmLoaiCongTrinhService));
                GenericControlCustomViewModel<NhDmLoaiCongTrinhModel, NhDmLoaiCongTrinh, NhDmLoaiCongTrinhService> dialogVM = new GenericControlCustomViewModel<NhDmLoaiCongTrinhModel, NhDmLoaiCongTrinh, NhDmLoaiCongTrinhService>
                    ((NhDmLoaiCongTrinhService)NhDmLoaiCongTrinhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục loại công trình";
                dialogVM.Title = "Danh mục loại công trình";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.Id.Equals(sourceVM.SelectedItem.IIdParent)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    NhDmLoaiCongTrinhModel parent = obj as NhDmLoaiCongTrinhModel;
                    sourceVM.SelectedItem.IIdParent = parent.Id;
                    sourceVM.SelectedItem.Parent = parent;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }    
        }
    }
}
