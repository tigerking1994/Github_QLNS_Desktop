using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using System.Linq;
using System.Windows.Controls;
using log4net;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DmChuDauTuModelControlService : GenericControlBaseService<DmChuDauTuModel, Core.Domain.DmChuDauTu, DmChuDauTuService>
    {
        public override void CustomValueProps(DmChuDauTuModel newRow, DmChuDauTuModel currentRow)
        {
            newRow.BHangCha = false;
            base.CustomValueProps(newRow, currentRow);
        }
        
        public override void OnDelete(object obj)
        {
            List<DmChuDauTuModel> children = sourceVM.Items.Where(s => sourceVM.SelectedItem.Id.Equals(s.IIDDonViCha)).ToList();
            foreach (var child in children)
            {
                child.IIDDonViCha = null;
                child.TenCdtParent = null;
            }
            base.OnDelete(obj);
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DmChuDauTuModel.TenCdtParent)))
            {
                var dmCdtService = sourceVM._serviceProvider.GetService(typeof(IDmChuDauTuService));
                GenericControlCustomViewModel<DmChuDauTuModel, DmChuDauTu, DmChuDauTuService> dialogVM = new GenericControlCustomViewModel<DmChuDauTuModel, DmChuDauTu, DmChuDauTuService>
                    ((DmChuDauTuService)dmCdtService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục chủ đầu tư";
                dialogVM.Title = "Danh mục chủ đầu tư";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.Id.Equals(sourceVM.SelectedItem.IIDDonViCha)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    DmChuDauTuModel source = obj as DmChuDauTuModel;
                    sourceVM.SelectedItem.IIDDonViCha = source.Id;
                    sourceVM.SelectedItem.TenCdtParent = source.STenDonVi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
