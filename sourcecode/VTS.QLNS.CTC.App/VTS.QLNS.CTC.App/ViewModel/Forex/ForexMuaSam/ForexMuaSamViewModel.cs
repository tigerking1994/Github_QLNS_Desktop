using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPCNTDecision;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTDecision;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNThietKeKyThuatTongDuToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTGoiThauTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTKeHoachLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNKeHoachLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentPolicy;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTNHHopDongTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNNHHopDongTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNKeHoachDatHang;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChiPhiKhac.QuyetDinhChiPhiKhac;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam
{
    public class ForexMuaSamViewModel : ViewModelBase
    {
        public override string Name => "MUA SẮM";
        public override string Description => "Quản lý nhiệm vụ chi mua sắm ";
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.ForexMuaSam);
        public override PackIconKind IconKind => PackIconKind.BagChecked;

        public MSNTNHPhuongAnNhapKhauIndexViewModel PhuongAnNhapKhauIndexViewModel_1 { get; }
        public MSNTDecisionIndexViewModel DecisionIndexViewModel_1 { get; }
        public ForexContractInfoIndexViewModel ForexContractInfoIndexViewModel_1 { get; }
        public MSNTThietKeKyThuatTongDuToanIndexViewModel ThietKeKyThuatTongDuToanIndexViewModel_1 { get; }
        public MSNTKeHoachLuaChonNhaThauIndexViewModel MSNTKeHoachLuaChonNhaThauIndexViewModel { get; }
        public MSNTGoiThauTrongNuocIndexViewModel MSTNGoiThauTrongNuocViewModel { get; }
        public MSNTNHHopDongTrongNuocIndexViewModel HopDongTrongNuocIndexViewModel_1 { get; }

        public MSTNThietKeKyThuatTongDuToanIndexViewModel ThietKeKyThuatTongDuToanIndexViewModel_2 { get; }
        public MSTNKeHoachLuaChonNhaThauIndexViewModel MSTNKeHoachLuaChonNhaThauIndexViewModel { get; }
        public MSTNKeHoachDatHangIndexViewModel MSTNKeHoachDatHangIndexViewModel { get; }
        public MSTNGoiThauTrongNuocIndexViewModel MSTNGoiThauTrongNuocIndexViewModel { get; }
        public MSTNNHHopDongTrongNuocIndexViewModel HopDongTrongNuocIndexViewModel_2 { get; }

        public MSTNHopDongTrongNuocKhongGoiThauIndexViewModel MSTNHopDongTrongNuocKhongGoiThauIndexViewModel { get; }

        public MSCBDTForexProjectInformationIndexViewModel ForexProjectInformationIndexViewModel_3_1 { get; }
        public MSCBDTInvestmentPolicyIndexViewModel ForexInvestmentPolicyIndexViewModel_3_1 { get; }
        public MSCBDTInvestmentDecisionIndexViewModel ForexInvestmentDecisionIndexViewModel_3_1 { get; }

        public MSCTNThietKeKyThuatTongDuToanIndexViewModel ThietKeKyThuatTongDuToanIndexViewModel_3_2 { get; }
        public MSCTNKeHoachLuaChonNhaThauIndexViewModel MSCTNKeHoachLuaChonNhaThauIndexViewModel { get; }
        public MSCTNGoiThauTrongNuocIndexViewModel MSCTNGoiThauTrongNuocIndexViewModel { get; }
        public MSCTNNHHopDongTrongNuocIndexViewModel HopDongTrongNuocIndexViewModel_3_2 { get; }

        public MSNHPhuongAnNhapKhauIndexViewModel PhuongAnNhapKhauIndexViewModel_3_3 { get; }
        public MSCNTForexContractInfoIndexViewModel MSCNTForexContractInfoIndexViewModel { get; }
        public MSPCNTDecisionIndexViewModel DecisionIndexViewModel_3_3 { get; }
        public ForexContractInfoIndexViewModel ForexContractInfoIndexViewModel_3_3 { get; }
        public MuaSamQuyetDinhChiPhiKhacIndexViewModel MuaSamQuyetDinhChiPhiKhacIndexViewModel { get; }

        public ForexMuaSamViewModel(
            MSNTNHPhuongAnNhapKhauIndexViewModel phuongAnNhapKhauIndexViewModel_1,
            MSNTDecisionIndexViewModel decisionIndexViewModel_1,
            ForexContractInfoIndexViewModel forexContractInfoIndexViewModel_1,
            MSNTThietKeKyThuatTongDuToanIndexViewModel thietKeKyThuatTongDuToanIndexViewModel_1,
            MSNTKeHoachLuaChonNhaThauIndexViewModel msntKeHoachLuaChonNhaThauIndexViewModel,
            MSNTGoiThauTrongNuocIndexViewModel msntGoiThauTrongNuocIndexViewModel,
            MSNTNHHopDongTrongNuocIndexViewModel hopDongTrongNuocIndexViewModel_1,
            MSTNThietKeKyThuatTongDuToanIndexViewModel thietKeKyThuatTongDuToanIndexViewModel_2,
            MSTNKeHoachDatHangIndexViewModel mstnKeHoachDatHangIndexViewModel,
            MSTNKeHoachLuaChonNhaThauIndexViewModel mstnKeHoachLuaChonNhaThauIndexViewModel,
            MSTNGoiThauTrongNuocIndexViewModel mstnGoiThauTrongNuocIndexViewModel,
            MSTNNHHopDongTrongNuocIndexViewModel hopDongTrongNuocIndexViewModel_2,
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel mstnHopDongTrongNuocKhongGoiThauIndexViewModel,
            MSCBDTForexProjectInformationIndexViewModel forexProjectInformationIndexViewModel_3_1,
            MSCTNThietKeKyThuatTongDuToanIndexViewModel thietKeKyThuatTongDuToanIndexViewModel_3_2,
            MSCBDTInvestmentPolicyIndexViewModel forexInvestmentPolicyIndexViewModel_3_1,
            MSCBDTInvestmentDecisionIndexViewModel forexInvestmentDecisionIndexViewModel_3_1,
            MSCTNKeHoachLuaChonNhaThauIndexViewModel msctnKeHoachLuaChonNhaThauIndexViewModel,
            MSCTNGoiThauTrongNuocIndexViewModel msctnGoiThauTrongNuocIndexViewModel,
            MSCTNNHHopDongTrongNuocIndexViewModel hopDongTrongNuocIndexViewModel_3_2,
            MSNHPhuongAnNhapKhauIndexViewModel phuongAnNhapKhauIndexViewModel_3_3,
            MSCNTForexContractInfoIndexViewModel mSCNTForexContractInfoIndexViewModel,
            MSPCNTDecisionIndexViewModel decisionIndexViewModel_3_3,
            MuaSamQuyetDinhChiPhiKhacIndexViewModel muaSamQuyetDinhChiPhiKhacIndexViewModel)
        {
            PhuongAnNhapKhauIndexViewModel_1 = phuongAnNhapKhauIndexViewModel_1;
            DecisionIndexViewModel_1 = decisionIndexViewModel_1;
            ForexContractInfoIndexViewModel_1 = forexContractInfoIndexViewModel_1;
            ThietKeKyThuatTongDuToanIndexViewModel_1 = thietKeKyThuatTongDuToanIndexViewModel_1;
            MSNTKeHoachLuaChonNhaThauIndexViewModel = msntKeHoachLuaChonNhaThauIndexViewModel;
            MSTNGoiThauTrongNuocViewModel = msntGoiThauTrongNuocIndexViewModel;
            HopDongTrongNuocIndexViewModel_1 = hopDongTrongNuocIndexViewModel_1;

            ThietKeKyThuatTongDuToanIndexViewModel_2 = thietKeKyThuatTongDuToanIndexViewModel_2;
            MSTNKeHoachLuaChonNhaThauIndexViewModel = mstnKeHoachLuaChonNhaThauIndexViewModel;
            MSTNGoiThauTrongNuocIndexViewModel = mstnGoiThauTrongNuocIndexViewModel;
            HopDongTrongNuocIndexViewModel_2 = hopDongTrongNuocIndexViewModel_2;

            MSTNKeHoachDatHangIndexViewModel = mstnKeHoachDatHangIndexViewModel;
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel = mstnHopDongTrongNuocKhongGoiThauIndexViewModel;

            ForexProjectInformationIndexViewModel_3_1 = forexProjectInformationIndexViewModel_3_1;
            ForexInvestmentPolicyIndexViewModel_3_1 = forexInvestmentPolicyIndexViewModel_3_1;
            ForexInvestmentDecisionIndexViewModel_3_1 = forexInvestmentDecisionIndexViewModel_3_1;

            ThietKeKyThuatTongDuToanIndexViewModel_3_2 = thietKeKyThuatTongDuToanIndexViewModel_3_2;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel = msctnKeHoachLuaChonNhaThauIndexViewModel;
            MSCTNGoiThauTrongNuocIndexViewModel = msctnGoiThauTrongNuocIndexViewModel;
            HopDongTrongNuocIndexViewModel_3_2 = hopDongTrongNuocIndexViewModel_3_2;

            PhuongAnNhapKhauIndexViewModel_3_3 = phuongAnNhapKhauIndexViewModel_3_3;
            DecisionIndexViewModel_3_3 = decisionIndexViewModel_3_3;
            MSCNTForexContractInfoIndexViewModel = mSCNTForexContractInfoIndexViewModel;
            MuaSamQuyetDinhChiPhiKhacIndexViewModel = muaSamQuyetDinhChiPhiKhacIndexViewModel;
            PhuongAnNhapKhauIndexViewModel_1.ParentPage = this;
            DecisionIndexViewModel_1.ParentPage = this;
            ForexContractInfoIndexViewModel_1.ParentPage = this;
            ThietKeKyThuatTongDuToanIndexViewModel_1.ParentPage = this;
            MSNTKeHoachLuaChonNhaThauIndexViewModel.ParentPage = this;
            MSTNGoiThauTrongNuocViewModel.ParentPage = this;
            HopDongTrongNuocIndexViewModel_1.ParentPage = this;

            ThietKeKyThuatTongDuToanIndexViewModel_2.ParentPage = this;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.ParentPage = this;
            MSTNGoiThauTrongNuocIndexViewModel.ParentPage = this;
            HopDongTrongNuocIndexViewModel_2.ParentPage = this;

            MSTNKeHoachDatHangIndexViewModel.ParentPage = this;
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel.ParentPage = this;

            ForexProjectInformationIndexViewModel_3_1.ParentPage = this;
            ForexInvestmentPolicyIndexViewModel_3_1.ParentPage = this;
            ForexInvestmentDecisionIndexViewModel_3_1.ParentPage = this;

            ThietKeKyThuatTongDuToanIndexViewModel_3_2.ParentPage = this;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.ParentPage = this;
            MSCTNGoiThauTrongNuocIndexViewModel.ParentPage = this;
            HopDongTrongNuocIndexViewModel_3_2.ParentPage = this;

            PhuongAnNhapKhauIndexViewModel_3_3.ParentPage = this;
            PhuongAnNhapKhauIndexViewModel_3_3.ILoai = 2;
            DecisionIndexViewModel_3_3.ParentPage = this;
            DecisionIndexViewModel_3_3.ILoai = 2;
            MSCNTForexContractInfoIndexViewModel.ParentPage = this;
            MuaSamQuyetDinhChiPhiKhacIndexViewModel.ParentPage = this;
            MuaSamQuyetDinhChiPhiKhacIndexViewModel.IThuocMenu = NHConstants.IMENU_MUASAM_QUYET_DINH_CHI_PHI_KHAC;

            PhuongAnNhapKhauIndexViewModel_1.ILoai = 1;
            DecisionIndexViewModel_1.ILoai = 1;

            // Hợp đồng
            ForexContractInfoIndexViewModel_1.ILoai = 1;
            ForexContractInfoIndexViewModel_1.IThuocMenu = 1;
            HopDongTrongNuocIndexViewModel_1.ILoai = 2;
            HopDongTrongNuocIndexViewModel_1.IThuocMenu = 2;
            HopDongTrongNuocIndexViewModel_1.IsShowDuAn = false;
            HopDongTrongNuocIndexViewModel_2.ILoai = 2;
            HopDongTrongNuocIndexViewModel_2.IThuocMenu = 3;
            HopDongTrongNuocIndexViewModel_2.IsShowDuAn = false;
            HopDongTrongNuocIndexViewModel_3_2.ILoai = 2;
            HopDongTrongNuocIndexViewModel_3_2.IThuocMenu = 4;
            HopDongTrongNuocIndexViewModel_3_2.IsShowDuAn = true;
            MSCNTForexContractInfoIndexViewModel.ILoai = 1;
            MSCNTForexContractInfoIndexViewModel.IThuocMenu = 5;
            //Dự toán
            ThietKeKyThuatTongDuToanIndexViewModel_1.ILoai = 1;
            ThietKeKyThuatTongDuToanIndexViewModel_1.IsShowDuAn = false;
            ThietKeKyThuatTongDuToanIndexViewModel_2.ILoai = 2;
            ThietKeKyThuatTongDuToanIndexViewModel_2.IsShowDuAn = false;
            ThietKeKyThuatTongDuToanIndexViewModel_3_2.ILoai = 3;
            MSNTKeHoachLuaChonNhaThauIndexViewModel.IThuocMenu = 1;
            MSNTKeHoachLuaChonNhaThauIndexViewModel.ILoai = 1;
            MSNTKeHoachLuaChonNhaThauIndexViewModel.IsShowDuAn = false;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.IThuocMenu = 2;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.ILoai = 2;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.IsShowDuAn = false;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.IThuocMenu = 4;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.ILoai = 3;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.IsShowDuAn = true;
            ForexProjectInformationIndexViewModel_3_1.ILoai = 1;
            // Chủ trương đầu tư
            ForexInvestmentPolicyIndexViewModel_3_1.ILoai = 1;

            ForexInvestmentDecisionIndexViewModel_3_1.ILoai = 1;
            // Gói thầu
            MSTNGoiThauTrongNuocViewModel.ILoai = 2;
            MSTNGoiThauTrongNuocViewModel.IThuocMenu = 1;
            MSTNGoiThauTrongNuocViewModel.IsShowDuAn = false;
            MSTNGoiThauTrongNuocIndexViewModel.ILoai = 2;
            MSTNGoiThauTrongNuocIndexViewModel.IThuocMenu = 2;
            MSTNGoiThauTrongNuocIndexViewModel.IsShowDuAn = false;
            MSCTNGoiThauTrongNuocIndexViewModel.ILoai = 2;
            MSCTNGoiThauTrongNuocIndexViewModel.IThuocMenu = 4;
            MSCTNGoiThauTrongNuocIndexViewModel.IsShowDuAn = true;

            //Không gói thầu
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel.ILoai = 2;
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel.IThuocMenu = 8;

            ThietKeKyThuatTongDuToanIndexViewModel_1.Name = "Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_1.Title = "Quản lý Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_1.Description = "Danh sách Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_2.Name = "Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_2.Title = "Quản lý Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_2.Description = "Danh sách Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_3_2.Name = "Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_3_2.Title = "Quản lý Dự toán đặt hàng/mua sắm được duyệt";
            ThietKeKyThuatTongDuToanIndexViewModel_3_2.Description = "Danh sách Dự toán đặt hàng/mua sắm được duyệt";

            PhuongAnNhapKhauIndexViewModel_1.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            DecisionIndexViewModel_1.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            ForexContractInfoIndexViewModel_1.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            ThietKeKyThuatTongDuToanIndexViewModel_1.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            MSNTKeHoachLuaChonNhaThauIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            MSTNGoiThauTrongNuocViewModel.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            HopDongTrongNuocIndexViewModel_1.GroupName = MenuItemContants.GROUP_FOREX_MUASAM_NGOAITHUONG;
            HopDongTrongNuocIndexViewModel_1.ILoaiMenu = 1;

            ThietKeKyThuatTongDuToanIndexViewModel_2.GroupName = MenuItemContants.GROUP_FOREX_CO_HINH_THANH_GOITHAU;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_CO_HINH_THANH_GOITHAU;
            MSTNGoiThauTrongNuocIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_CO_HINH_THANH_GOITHAU;
            HopDongTrongNuocIndexViewModel_2.GroupName = MenuItemContants.GROUP_FOREX_CO_HINH_THANH_GOITHAU;
            HopDongTrongNuocIndexViewModel_2.ILoaiMenu = 1;

            MSTNKeHoachDatHangIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_KHONG_HINH_THANH_GOITHAU;
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_KHONG_HINH_THANH_GOITHAU;

            ThietKeKyThuatTongDuToanIndexViewModel_3_2.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_TRONGNUOC;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_TRONGNUOC;
            MSCTNGoiThauTrongNuocIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_TRONGNUOC;
            HopDongTrongNuocIndexViewModel_3_2.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_TRONGNUOC;
            HopDongTrongNuocIndexViewModel_3_2.ILoaiMenu = 1;

            PhuongAnNhapKhauIndexViewModel_3_3.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_NGOAITHUONG;
            DecisionIndexViewModel_3_3.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_NGOAITHUONG;
            MSCNTForexContractInfoIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_PHANCHI_NGOAITHUONG;
            MuaSamQuyetDinhChiPhiKhacIndexViewModel.GroupName = MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC;

            ThietKeKyThuatTongDuToanIndexViewModel_2.IsUseExpand = true;
            MSTNKeHoachLuaChonNhaThauIndexViewModel.IsUseExpand = true;
            MSTNGoiThauTrongNuocIndexViewModel.IsUseExpand = true;
            HopDongTrongNuocIndexViewModel_2.IsUseExpand = true;

            MSTNKeHoachDatHangIndexViewModel.IsUseExpand = true;
            MSTNHopDongTrongNuocKhongGoiThauIndexViewModel.IsUseExpand = true;

            ForexProjectInformationIndexViewModel_3_1.IsUseExpand = true;
            ForexInvestmentPolicyIndexViewModel_3_1.IsUseExpand = true;
            ForexInvestmentDecisionIndexViewModel_3_1.IsUseExpand = true;
            ThietKeKyThuatTongDuToanIndexViewModel_3_2.IsUseExpand = true;
            MSCTNKeHoachLuaChonNhaThauIndexViewModel.IsUseExpand = true;
            MSCTNGoiThauTrongNuocIndexViewModel.IsUseExpand = true;
            HopDongTrongNuocIndexViewModel_3_2.IsUseExpand = true;
            PhuongAnNhapKhauIndexViewModel_3_3.IsUseExpand = true;
            DecisionIndexViewModel_3_3.IsUseExpand = true;
            MSCNTForexContractInfoIndexViewModel.IsUseExpand = true;
            MuaSamQuyetDinhChiPhiKhacIndexViewModel.IsUseExpand = true;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness();
            Documentation = new ObservableCollection<ViewModelBase>()
            {
                // 1. Mua sắm ngoại thương
                PhuongAnNhapKhauIndexViewModel_1,
                DecisionIndexViewModel_1,
                ForexContractInfoIndexViewModel_1,
                ThietKeKyThuatTongDuToanIndexViewModel_1,
                MSNTKeHoachLuaChonNhaThauIndexViewModel,
                MSTNGoiThauTrongNuocViewModel,
                HopDongTrongNuocIndexViewModel_1,
                // 2. Mua sắm trong nước
                new MenuItemViewModel() { GroupName = MenuItemContants.GROUP_FOREX_MUASAM_TRONGNUOC },
                // 2.1 Có hình thành gói thầu
                ThietKeKyThuatTongDuToanIndexViewModel_2,
                MSTNKeHoachLuaChonNhaThauIndexViewModel,
                MSTNGoiThauTrongNuocIndexViewModel,
                HopDongTrongNuocIndexViewModel_2,
                // 2.1 Không hình thành gói thầu
                MSTNKeHoachDatHangIndexViewModel,
                MSTNHopDongTrongNuocKhongGoiThauIndexViewModel,
                // 3. Dự án
                new MenuItemViewModel() { GroupName = MenuItemContants.GROUP_FOREX_PROJECT },
                // 3.1. Chuẩn bị đầu tư
                ForexProjectInformationIndexViewModel_3_1,
                ForexInvestmentPolicyIndexViewModel_3_1,
                ForexInvestmentDecisionIndexViewModel_3_1,
                // 3.2. Phần chi trong nước
                ThietKeKyThuatTongDuToanIndexViewModel_3_2,
                MSCTNKeHoachLuaChonNhaThauIndexViewModel,
                MSCTNGoiThauTrongNuocIndexViewModel,
                HopDongTrongNuocIndexViewModel_3_2,
                // 3.3. Phần chi ngoại thương
                PhuongAnNhapKhauIndexViewModel_3_3,
                DecisionIndexViewModel_3_3,
                MSCNTForexContractInfoIndexViewModel,
                new MenuItemViewModel() {GroupName = MenuItemContants.GROUP_FOREX_CHI_PHI_KHAC},
                MuaSamQuyetDinhChiPhiKhacIndexViewModel
            };
            DocumentationSelectedItem = Documentation.FirstOrDefault(x => x.IsAuthorized);
        }
    }
}
