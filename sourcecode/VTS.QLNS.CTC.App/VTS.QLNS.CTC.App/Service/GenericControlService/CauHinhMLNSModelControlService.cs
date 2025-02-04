using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;
using System.Drawing.Printing;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhMLNSModelControlService : GenericControlBaseService<CauHinhMLNSModel, Core.Domain.NsMucLucNganSach, CauHinhMLNSService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(CauHinhMLNSModel.TenPhongBan)))
            {
                var nsphongBanService = sourceVM._serviceProvider.GetService(typeof(INsPhongBanService));
                GenericControlCustomViewModel<NSPhongBanModel, DmBQuanLy, NsPhongBanService> dialogVM = new GenericControlCustomViewModel<NSPhongBanModel, DmBQuanLy, NsPhongBanService>
                    ((NsPhongBanService)nsphongBanService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục B quản lý";
                dialogVM.Title = "Danh mục B quản lý";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.IIDMaBQuanLy.Equals(sourceVM.SelectedItem.IdPhongBan)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    NSPhongBanModel nSPhongBanModel = obj as NSPhongBanModel;
                    sourceVM.SelectedItem.TenPhongBan = nSPhongBanModel.STenBQuanLy;
                    sourceVM.SelectedItem.IdPhongBan = nSPhongBanModel.IIDMaBQuanLy;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }


        public override void DeleteColumnDataRefer(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(CauHinhMLNSModel.TenPhongBan)))
            {
                sourceVM.SelectedItem.TenPhongBan = null;
                sourceVM.SelectedItem.IdPhongBan = null;
            }
        }
        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(CauHinhMLNSModel.ChiTietToi)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = "" },
                    new ComboboxItem { DisplayItem = "NG", ValueItem = "NG" },
                    new ComboboxItem { DisplayItem = "TNG", ValueItem = "TNG" },
                    new ComboboxItem { DisplayItem = "TNG1", ValueItem = "TNG1" },
                    new ComboboxItem { DisplayItem = "TNG2", ValueItem = "TNG2" },
                    new ComboboxItem { DisplayItem = "TNG3", ValueItem = "TNG3" }
                };
            } else if (property.Name.Equals(nameof(CauHinhMLNSModel.ILoaiNganSach)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Thường xuyên", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Nghiệp vụ", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "NSNN", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "Kinh phí khác", ValueItem = "3" },
                    new ComboboxItem { DisplayItem = "Quốc phòng khác", ValueItem = "4" },
                    new ComboboxItem { DisplayItem = "Nhà nước khác", ValueItem = "5" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override bool IsDisableColumn(PropertyInfo property)
        {
            return  property.Name.Equals(nameof(CauHinhMLNSModel.Lns)) || property.Name.Equals(nameof(CauHinhMLNSModel.MoTa));
        }
    }
}
