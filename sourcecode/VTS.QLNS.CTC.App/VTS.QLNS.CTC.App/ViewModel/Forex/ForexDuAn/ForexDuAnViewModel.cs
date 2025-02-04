using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.BaoCaoTinhHinhThucHienDuAn;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentPolicy;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTInvestmentDecision;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAPlanImport;
using VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.TongHopThongTinDuAn;
using VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.ExchangeRateDifference;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKForexContractInfo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.ThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.ForexDomesticBiddingPackagel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChiPhiKhac.QuyetDinhChiPhiKhac;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn
{
    public class ForexDuAnViewModel : ViewModelBase
    {
        public override string GroupName => MenuItemContants.GROUP_FOREX_PREPARE_TO_INVEST;
        public override string Name => "DỰ ÁN";
        public override string Description => "Khởi tạo thông tin dự án";
        public override Type ContentType => typeof(View.Forex.ForexDuAn.ForexDuAn);
        public override PackIconKind IconKind => PackIconKind.Projector;

        public ThietKeKyThuatTongDuToanIndexViewModel ThietKeKyThuatTongDuToanIndexViewModel { get; }
        public NHKeHoachLuaChonNhaThauIndexViewModel NHKeHoachLuaChonNhaThauIndexViewModel { get; }
        public ForexDomesticBiddingPackageIndexViewModel ForexDomesticBiddingPackageIndexViewModel { get; }
        public NHHopDongTrongNuocIndexViewModel NHHopDongTrongNuocIndexViewModel { get; }
        public DAHDNKDecisionIndexViewModel DAHDNKDecisionIndexViewModel { get; }
        public ForexContractInfoIndexViewModel ForexContractInfoIndexViewModel { get; }
        public DACBDTForexProjectInformationIndexViewModel ForexProjectInformationIndexViewModel { get; }
        public DACBDTInvestmentPolicyIndexViewModel DACBDTInvestmentPolicyIndexViewModel { get; }
        public DACBDTInvestmentDecisionIndexViewModel DACBDTInvestmentDecisionIndexViewModel { get; }
        public BaoCaoTinhHinhThucHienDuAnViewModel BaoCaoTinhHinhThucHienDuAnViewModel { get; }
        public DANHPhuongAnNhapKhauIndexViewModel PhuongAnNhapKhauIndexViewModel { get; }
        public NHTongHopTTDuAnViewModel NHTongHopTTDuAnViewModel { get; }
        public ExchangeRateDifferenceIndexViewModel ExchangeRateDifferenceIndexViewModel { get; }
        public DuAnQuyetDinhChiPhiKhacIndexViewModel DuAnQuyetDinhChiPhiKhacIndexViewModel { get; }

        public ForexDuAnViewModel(
            ThietKeKyThuatTongDuToanIndexViewModel thietKeKyThuatTongDuToanIndexViewModel,
            NHKeHoachLuaChonNhaThauIndexViewModel nHKeHoachLuaChonNhaThauIndexViewModel,
            ForexDomesticBiddingPackageIndexViewModel forexDomesticBiddingPackageIndexViewModel,
            NHHopDongTrongNuocIndexViewModel nHHopDongTrongNuocIndexViewModel,
            DAHDNKDecisionIndexViewModel dahdkndecisionIndexViewModel,
            ForexContractInfoIndexViewModel forexContractInfoIndexViewModel,
            DACBDTForexProjectInformationIndexViewModel forexProjectInformationIndexViewModel,
            DACBDTInvestmentPolicyIndexViewModel dacbdtInvestmentPolicyIndexViewModel,
            DACBDTInvestmentDecisionIndexViewModel dacbdtInvestmentDecisionIndexViewModel,
            BaoCaoTinhHinhThucHienDuAnViewModel baoCaoTinhHinhThucHienDuAnViewModel,
            DANHPhuongAnNhapKhauIndexViewModel phuongAnNhapKhauIndexViewModel,
            NHTongHopTTDuAnViewModel nHTongHopTTDuAnViewModel,
            ExchangeRateDifferenceIndexViewModel exchangeRateDifferenceIndexViewModel,
            DuAnQuyetDinhChiPhiKhacIndexViewModel  duAnQuyetDinhChiPhiKhacIndexViewModel)
        {
            ThietKeKyThuatTongDuToanIndexViewModel = thietKeKyThuatTongDuToanIndexViewModel;
            NHKeHoachLuaChonNhaThauIndexViewModel = nHKeHoachLuaChonNhaThauIndexViewModel;
            ForexDomesticBiddingPackageIndexViewModel = forexDomesticBiddingPackageIndexViewModel;
            NHHopDongTrongNuocIndexViewModel = nHHopDongTrongNuocIndexViewModel;
            DAHDNKDecisionIndexViewModel = dahdkndecisionIndexViewModel;
            ForexContractInfoIndexViewModel = forexContractInfoIndexViewModel;
            ForexProjectInformationIndexViewModel = forexProjectInformationIndexViewModel;
            DACBDTInvestmentPolicyIndexViewModel = dacbdtInvestmentPolicyIndexViewModel;
            DACBDTInvestmentDecisionIndexViewModel = dacbdtInvestmentDecisionIndexViewModel;
            BaoCaoTinhHinhThucHienDuAnViewModel = baoCaoTinhHinhThucHienDuAnViewModel;
            PhuongAnNhapKhauIndexViewModel = phuongAnNhapKhauIndexViewModel;
            NHTongHopTTDuAnViewModel = nHTongHopTTDuAnViewModel;
            ExchangeRateDifferenceIndexViewModel = exchangeRateDifferenceIndexViewModel;
            DuAnQuyetDinhChiPhiKhacIndexViewModel = duAnQuyetDinhChiPhiKhacIndexViewModel;

            ThietKeKyThuatTongDuToanIndexViewModel.ParentPage = this;
            NHKeHoachLuaChonNhaThauIndexViewModel.ParentPage = this;
            ForexDomesticBiddingPackageIndexViewModel.ParentPage = this;
            NHHopDongTrongNuocIndexViewModel.ParentPage = this;
            DAHDNKDecisionIndexViewModel.ParentPage = this;
            ForexContractInfoIndexViewModel.ParentPage = this;
            ForexProjectInformationIndexViewModel.ParentPage = this;
            DACBDTInvestmentPolicyIndexViewModel.ParentPage = this;
            DACBDTInvestmentDecisionIndexViewModel.ParentPage = this;
            BaoCaoTinhHinhThucHienDuAnViewModel.ParentPage = this;
            PhuongAnNhapKhauIndexViewModel.ParentPage = this;
            NHTongHopTTDuAnViewModel.ParentPage = this;
            ExchangeRateDifferenceIndexViewModel.ParentPage = this;
            DuAnQuyetDinhChiPhiKhacIndexViewModel.ParentPage = this;

            DAHDNKDecisionIndexViewModel.ILoai = 3;
            ForexContractInfoIndexViewModel.ILoai = 1;
            PhuongAnNhapKhauIndexViewModel.ILoai = 3;

            NHHopDongTrongNuocIndexViewModel.ILoai = 2;
            NHHopDongTrongNuocIndexViewModel.IThuocMenu = 5;
            NHHopDongTrongNuocIndexViewModel.IsShowDuAn = true;
            NHHopDongTrongNuocIndexViewModel.ILoaiMenu = 2;
            ForexContractInfoIndexViewModel.ILoai = 1;
            ForexContractInfoIndexViewModel.IThuocMenu = 7;
            ThietKeKyThuatTongDuToanIndexViewModel.ILoai = 4;
            ThietKeKyThuatTongDuToanIndexViewModel.IsShowDuAn = true;
            NHKeHoachLuaChonNhaThauIndexViewModel.IThuocMenu = 4;
            NHKeHoachLuaChonNhaThauIndexViewModel.ILoai = 2;
            NHKeHoachLuaChonNhaThauIndexViewModel.IsShowDuAn = true;
            ForexProjectInformationIndexViewModel.ILoai = 2;
            DACBDTInvestmentPolicyIndexViewModel.ILoai = 2;

            DACBDTInvestmentDecisionIndexViewModel.ILoai = 2;
            ForexDomesticBiddingPackageIndexViewModel.ILoai = 2;
            ForexDomesticBiddingPackageIndexViewModel.IThuocMenu = 4;
            ForexDomesticBiddingPackageIndexViewModel.IsShowDuAn = true;
            DuAnQuyetDinhChiPhiKhacIndexViewModel.IThuocMenu = NHConstants.IMENU_DUAN_QUYET_DINH_CHI_PHI_KHAC;

            ThietKeKyThuatTongDuToanIndexViewModel.Name = "Dự toán đặt hàng/ mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel.Title = "Quản lý dự toán đặt hàng/ mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel.Description = "Danh sách dự toán đặt hàng/ mua sắm được duyệt";

            ThietKeKyThuatTongDuToanIndexViewModel.GroupName = MenuItemContants.GROUP_PROJECT_MANAGER;
            NHKeHoachLuaChonNhaThauIndexViewModel.GroupName = MenuItemContants.GROUP_PROJECT_MANAGER;
            ForexDomesticBiddingPackageIndexViewModel.GroupName = MenuItemContants.GROUP_PROJECT_MANAGER;
            NHHopDongTrongNuocIndexViewModel.GroupName = MenuItemContants.GROUP_PROJECT_MANAGER;

            PhuongAnNhapKhauIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_IMPORT_CONTRACT;
            DAHDNKDecisionIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_IMPORT_CONTRACT;
            ForexContractInfoIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_IMPORT_CONTRACT;
            DuAnQuyetDinhChiPhiKhacIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                // Chuẩn bị đầu tư
                ForexProjectInformationIndexViewModel,
                DACBDTInvestmentPolicyIndexViewModel,
                DACBDTInvestmentDecisionIndexViewModel,
                // Quản lý dự án
                ThietKeKyThuatTongDuToanIndexViewModel,
                NHKeHoachLuaChonNhaThauIndexViewModel,
                ForexDomesticBiddingPackageIndexViewModel,
                NHHopDongTrongNuocIndexViewModel,
                PhuongAnNhapKhauIndexViewModel,
                // Quản lý hợp đồng nhập khẩu
                DAHDNKDecisionIndexViewModel,
                ForexContractInfoIndexViewModel,
                NHTongHopTTDuAnViewModel,
                // Báo cáo
                BaoCaoTinhHinhThucHienDuAnViewModel,
                ExchangeRateDifferenceIndexViewModel,
                DuAnQuyetDinhChiPhiKhacIndexViewModel
            };
            DocumentationSelectedItem = Documentation.First();
        }
    }
}
