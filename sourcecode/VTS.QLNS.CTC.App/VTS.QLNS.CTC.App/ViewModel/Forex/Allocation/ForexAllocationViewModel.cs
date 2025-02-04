using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexDeNghiThanhToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexPheDuyetThanhToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexThongTriCapPhat;
using VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexTinhHinhThucHienNganSach;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexAllocation
{
    public class ForexAllocationViewModel : ViewModelBase
    {
        public override string Name => "CẤP PHÁT";
        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexAllocation);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public ForexDeNghiThanhToanIndexViewModel ForexDeNghiThanhToanIndexViewModel { get; }
        public ForexPheDuyetThanhToanIndexViewModel ForexPheDuyetThanhToanIndexViewModel { get; }
        public ForexThongTriCapPhatIndexViewModel ForexThongTriCapPhatIndexViewModel { get; }
        public ForexTinhHinhThucHienNganSachIndexViewModel ForexTinhHinhThucHienNganSachIndexViewModel { get; }

        public ForexAllocationViewModel(ForexDeNghiThanhToanIndexViewModel forexDeNghiThanhToanIndexViewModel,
            ForexPheDuyetThanhToanIndexViewModel forexPheDuyetThanhToanIndexViewModel,
            ForexThongTriCapPhatIndexViewModel forexThongTriCapPhatIndexViewModel,
            ForexTinhHinhThucHienNganSachIndexViewModel forexTinhHinhThucHienNganSachIndexViewModel)
        {
            ForexDeNghiThanhToanIndexViewModel = forexDeNghiThanhToanIndexViewModel;
            ForexPheDuyetThanhToanIndexViewModel = forexPheDuyetThanhToanIndexViewModel;
            ForexThongTriCapPhatIndexViewModel = forexThongTriCapPhatIndexViewModel;
            ForexTinhHinhThucHienNganSachIndexViewModel = forexTinhHinhThucHienNganSachIndexViewModel;

            ForexDeNghiThanhToanIndexViewModel.ParentPage = this;
            ForexPheDuyetThanhToanIndexViewModel.ParentPage = this;
            ForexThongTriCapPhatIndexViewModel.ParentPage = this;
            ForexTinhHinhThucHienNganSachIndexViewModel.ParentPage = this;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                ForexDeNghiThanhToanIndexViewModel,
                ForexPheDuyetThanhToanIndexViewModel,
                ForexThongTriCapPhatIndexViewModel,
                ForexTinhHinhThucHienNganSachIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
