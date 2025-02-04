using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
    public class VDTDmDTChiModelControlService : GenericControlBaseService<VdtDmDuToanChiModel, VdtDmDuToanChi, DmDTChiService>
    {
        public override void OnAddChild(object obj)
        {
            if (sourceVM.SelectedItem == null)
                return;
            DataGrid dgdData = obj as DataGrid;
            this.CancelEditData(dgdData);
            VdtDmDuToanChiModel parent = sourceVM.SelectedItem;
            parent.BHangCha = true;
            sourceVM.SelectedItem.BHangCha = true;
            int currentRow = sourceVM.Items.IndexOf(sourceVM.SelectedItem);

            VdtDmDuToanChiModel newRow = ObjectCopier.Clone(sourceVM.SelectedItem);
            newRow.IIdDuToanChi = Guid.NewGuid();
            newRow.IIdDuToanChiParent = parent.IIdDuToanChi;
            newRow.IsModified = true;
            newRow.BHangCha = false;

            newRow.PropertyChanged += Item_PropertyChanged;
            sourceVM.Items.Insert(currentRow + 1, newRow);
            sourceVM.InvokePropertyChange("Items");
            var cell = new DataGridCellInfo(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.ScrollIntoView(sourceVM.Items[currentRow + 1], dgdData.Columns[0]);
            dgdData.CurrentCell = cell;
            dgdData.BeginEdit();
        }

        public override void CustomValueProps(VdtDmDuToanChiModel newRow, VdtDmDuToanChiModel currentRow)
        {
            //newRow.IIdDuToanChi = Guid.NewGuid();                     //dong gay loi khi them moi!
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(VdtDmDuToanChiModel.DuToanChiParent)))
            {
                var dtChiService = sourceVM._serviceProvider.GetService(typeof(IDmDTChiService));
                GenericControlCustomViewModel<VdtDmDuToanChiModel, VdtDmDuToanChi, DmDTChiService> dialogVM =
                    new GenericControlCustomViewModel<VdtDmDuToanChiModel, VdtDmDuToanChi, DmDTChiService>
                    ((DmDTChiService)dtChiService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục dự toán chi";
                dialogVM.Title = "Danh mục dự toán chi";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIdDuToanChi.Equals(sourceVM.SelectedItem.IIdDuToanChiParent)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    VdtDmDuToanChiModel data = obj as VdtDmDuToanChiModel;
                    var parent = sourceVM.Items.FirstOrDefault(t => t.IIdDuToanChi.Equals(data.IIdDuToanChi));
                    if (parent != null)
                        parent.BHangCha = true;
                    sourceVM.SelectedItem.IIdDuToanChiParent = data.IIdDuToanChi;
                    sourceVM.SelectedItem.DuToanChiParent = data.STenDuToanChi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
