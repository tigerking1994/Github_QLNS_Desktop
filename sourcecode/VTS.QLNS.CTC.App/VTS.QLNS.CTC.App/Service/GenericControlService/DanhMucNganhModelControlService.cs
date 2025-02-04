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
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DanhMucNganhModelControlService : GenericControlBaseService<DanhMucNganhModel, Core.Domain.DanhMuc, DanhMucNganhService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DanhMucNganhModel.TenDonViNoiBo)))
            {
                var nsphongBanService = sourceVM._serviceProvider.GetService(typeof(INsDonViService));
                GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService> dialogVM = new GenericControlCustomViewModel<DonViModel, DonVi, NsDonViService>
                    ((NsDonViService)nsphongBanService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = new object[] { DialogType.LoadDonViOfDanhMucNganh };
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục đơn vị";
                dialogVM.Title = "Danh mục đơn vị";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMaDonVi.Equals(sourceVM.SelectedItem.SGiaTri)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    DonViModel model = obj as DonViModel;
                    sourceVM.SelectedItem.SGiaTri = model.IIDMaDonVi;
                    sourceVM.SelectedItem.TenDonViNoiBo = model.TenDonVi;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
