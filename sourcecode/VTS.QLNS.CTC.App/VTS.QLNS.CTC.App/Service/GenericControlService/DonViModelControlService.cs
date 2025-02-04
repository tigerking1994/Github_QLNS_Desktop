using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Service;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DonViModelControlService : GenericControlBaseService<DonViModel, Core.Domain.DonVi, NsDonViService>
    {
        public override void CustomValueProps(DonViModel newRow, DonViModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.DanhMucChuyenNganh = new List<DanhMucNganhModel>();
            newRow.TenDanhMuc = null;
            newRow.LNS = new List<NsMuclucNgansachModel>();
            newRow.TenLNS = null;
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DonViModel.TenDanhMuc)))
            {
                var DanhMucNganhService = sourceVM._serviceProvider.GetService(typeof(DanhMucNganhService));
                GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> dialogVM = new GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService>
                    ((DanhMucNganhService)DanhMucNganhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = InitParamsChuyenNganhDialog();
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục chuyên ngành";
                dialogVM.Title = "Danh mục chuyên ngành";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                SetSelectedChuyenNganh(dialogVM);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<DanhMucNganhModel> data = obj as IEnumerable<DanhMucNganhModel>;
                    sourceVM.SelectedItem.DanhMucChuyenNganh = data.ToList();
                    sourceVM.SelectedItem.TenDanhMuc = string.Join(", ", data.Select(s => s.STen));
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
            else if (property.Name.Equals(nameof(DonViModel.TenLNS)))
            {
                var MucLucNganSachService = sourceVM._serviceProvider.GetService(typeof(IMucLucNganSachService));
                GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM = new GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService>
                    ((MucLucNganSachService)MucLucNganSachService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM._authenticationInfo.OptionalParam = InitParamsMLNSDialog();
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục MLNS";
                dialogVM.Title = "Danh mục MLNS";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                SetSelectedLNS(dialogVM);
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<NsMuclucNgansachModel> data = obj as IEnumerable<NsMuclucNgansachModel>;
                    sourceVM.SelectedItem.LNS = data.ToList();
                    sourceVM.SelectedItem.TenLNS = string.Join(", ", data.Select(s => s.Lns));
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private object[] InitParamsChuyenNganhDialog()
        {
            DonViModel nsDonVi = sourceVM.SelectedItem;
            IEnumerable<DonViModel> otherDonVi = sourceVM.Items.Where(p => !p.Id.Equals(nsDonVi.Id));
            IEnumerable<Guid> excludeDmChuyenNganhIds = otherDonVi.SelectMany(i => i.DanhMucChuyenNganh).Select(t => t.Id);
            return new object[] { DialogType.LoadDMChuyenNganhOfDonVi, excludeDmChuyenNganhIds };
        }

        private void SetSelectedChuyenNganh(GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> dialogVM)
        {
            IEnumerable<string> DanhMucNganhModelIdCodes = sourceVM.SelectedItem.DanhMucChuyenNganh.Select(t => t.IIDMaDanhMuc);
            foreach (var model in dialogVM.Items)
            {
                if (DanhMucNganhModelIdCodes.Contains(model.IIDMaDanhMuc))
                {
                    model.IsSelected = true;
                }
            }
        }

        private object[] InitParamsMLNSDialog()
        {
            DonViModel nsDonVi = sourceVM.SelectedItem;
            IEnumerable<DonViModel> otherDonVi = sourceVM.Items.Where(p => !p.Id.Equals(nsDonVi.Id));
            IEnumerable<string> excludeMLNS = otherDonVi.SelectMany(i => i.LNS).Select(t => t.Lns);
            return new object[] { DialogType.LoadMLNSOfNsDonVi, excludeMLNS };
        }

        private void SetSelectedLNS(GenericControlCustomViewModel<NsMuclucNgansachModel, NsMucLucNganSach, MucLucNganSachService> dialogVM)
        {
            IEnumerable<string> lns = sourceVM.SelectedItem.LNS.Select(t => t.Lns);
            foreach (var model in dialogVM.Items)
            {
                if (lns.Contains(model.Lns))
                {
                    model.IsSelected = true;
                }
            }
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DonViModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }
            else if (property.Name.Equals(nameof(DonViModel.Loai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Nội bộ", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Toàn quân", ValueItem = "2" }
                };
            }
            else if (property.Name.Equals(nameof(DonViModel.Khoi)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "", ValueItem = "" },
                    new ComboboxItem { DisplayItem = "Khối doanh nghiệp", ValueItem = "1" },
                    new ComboboxItem { DisplayItem = "Khối dự toán", ValueItem = "2" },
                    new ComboboxItem { DisplayItem = "Bệnh viện tự chủ", ValueItem = "3" }
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }
    }
}
