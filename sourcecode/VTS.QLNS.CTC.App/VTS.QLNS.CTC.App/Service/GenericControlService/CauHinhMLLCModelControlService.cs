using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using MaterialDesignThemes.Wpf;
using System.Windows.Data;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class CauHinhMLLCModelControlService : GenericControlBaseService<BhDanhMucLoaiChiModel, BhDanhMucLoaiChi, BhDanhMucLoaiChiService>
    {
        public override void CustomValueProps(BhDanhMucLoaiChiModel newRow, BhDanhMucLoaiChiModel currentRow)
        {
            base.CustomValueProps(newRow, currentRow);
            newRow.DanhMucNganSach = new List<BhDmMucLucNganSachModel>();
            newRow.SDSXauNoiMa = null;
            newRow.SMaLoaiChi = null;
            newRow.STenDanhMucLoaiChi = null;
        }

        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDanhMucLoaiChiModel.SDSXauNoiMa)))
            {
                MucLucNganSachBHXHViewModel mucLucNganSachViewModel = sourceVM._serviceProvider.GetService(typeof(MucLucNganSachBHXHViewModel)) as MucLucNganSachBHXHViewModel;
                mucLucNganSachViewModel.IsDialog = true;
                mucLucNganSachViewModel.CheckAllForDanhMuc = true;
                mucLucNganSachViewModel.IsShowCheckBox = false;
                mucLucNganSachViewModel.Init();
                mucLucNganSachViewModel._dataCollectionView = CollectionViewSource.GetDefaultView(mucLucNganSachViewModel.Items);
                MucLucDanhMucLoaiChiView mucLucDanhMucLoaiChiView = new MucLucDanhMucLoaiChiView()
                {
                    DataContext = mucLucNganSachViewModel
                };
                mucLucNganSachViewModel.SavedAction = obj =>
                {
                    IEnumerable<BhDmMucLucNganSachModel> data = obj as IEnumerable<BhDmMucLucNganSachModel>;
                    sourceVM.SelectedItem.SLNS = string.Join(",", data.Where(x => x.IsSelected).Select(s => s.SLNS).Distinct());
                    sourceVM.SelectedItem.SDSXauNoiMa = string.Join(", ", data.Where(x => x.IsSelected).Select(s => s.SXauNoiMa).Distinct());
                };
                mucLucNganSachViewModel.MucLucDanhMucLoaiChiView = mucLucDanhMucLoaiChiView;
                var dialog = DialogHost.Show(mucLucDanhMucLoaiChiView, "RootDialog");
                mucLucNganSachViewModel.IsShowCheckBox = false;
                mucLucNganSachViewModel.IsDialog = true;
                mucLucNganSachViewModel.CheckAllForDanhMuc = true;

            }
        }


        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(BhDanhMucLoaiChiModel.ITrangThai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem { DisplayItem = "Không sử dụng", ValueItem = "0" },
                    new ComboboxItem { DisplayItem = "Đang sử dụng", ValueItem = "1" }
                };
            }

            return new ObservableCollection<ComboboxItem>();
        }
    }
}
