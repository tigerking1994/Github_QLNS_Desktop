using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.Report;

using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKCBQYDV.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChiQuanLy.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Report
{
    public class SocialInsurancePlanReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintReportKhtBhxhViewModel PrintReportKhtBhxhViewModel;
        private PrintReportKhtmBhytViewModel PrintReportKhtmBhytViewModel;
        private PrintReportLapKeHoachChiViewModel PrintReportLapKeHoachChiViewModel;
        private PrintReportQuanLyKinhPhiViewModel PrintReportQuanLyKinhPhiViewModel;
        private PrintReportKhcKCBQYDVViewModel PrintReportKhcKCBQYDVViewModel;
        private PrintReportKhcKViewModel PrintReportKhcKcbViewModel;

        public override string GroupName => BHXHConstants.GROUP_PLAN_REPORT;
        public override string Name => "Báo cáo kế hoạch thu, chi BHXH";
        public override string Description => "Danh mục báo cáo kế hoạch thu, chi BHXH";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocialInsurancePlanReportIndex);    

        private ObservableCollection<DmChuKyModel> _listBaoCao;
        public ObservableCollection<DmChuKyModel> ListBaoCao
        {
            get => _listBaoCao;
            set => SetProperty(ref _listBaoCao, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(_searchText.Trim()))
                    _listBaoCaoView.Refresh();
            }
        }

        public List<string> ListLoaiBaoCao { get; set; }


        public SocialInsurancePlanReportIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            PrintReportKhtBhxhViewModel printReportKhtBhxh,
            PrintReportKhtmBhytViewModel printReportKhtmBhytViewModel,
            PrintReportLapKeHoachChiViewModel printReportLapKeHoachChiViewModel,
            PrintReportQuanLyKinhPhiViewModel printReportQuanLyKinhPhiViewModel,
            PrintReportKhcKCBQYDVViewModel printReportKhcKCBQYDVViewModel,
            PrintReportKhcKViewModel printReportKhcKcbViewModel)
        {
            PrintReportKhtBhxhViewModel = printReportKhtBhxh;
            PrintReportKhtmBhytViewModel = printReportKhtmBhytViewModel;
            PrintReportLapKeHoachChiViewModel = printReportLapKeHoachChiViewModel;
            PrintReportQuanLyKinhPhiViewModel = printReportQuanLyKinhPhiViewModel;
            PrintReportKhcKCBQYDVViewModel = printReportKhcKCBQYDVViewModel;
            PrintReportKhcKcbViewModel = printReportKhcKcbViewModel;

            _chuKyService = chuKyService;
            _mapper = mapper;
        }

        public override void Init()
        {
            base.Init();
            LoadData();
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            _searchText = string.Empty;
            LoadData();
        }

        private void LoadData()
        {
            //MarginRequirement = new Thickness(0);
            ListLoaiBaoCao = new List<string>
            {
                BHXHConstants.KHT_BHXH_BHYT_BHTN,
                BHXHConstants.KHTM_BHYT_THANNHAN,
                BHXHConstants.KHC_CHEDOBHXH,
                BHXHConstants.KHC_KINHPHIQUANLY,
                BHXHConstants.KHC_KCB_QUANYDONVI,
                BHXHConstants.KHC_KINHPHIKHAC,
                //BHXHConstants.TONGHOP_KH_THU_CHI
            };
            var predicate = PredicateBuilder.True<DmChuKy>();
            predicate = predicate.And(x => ListLoaiBaoCao.Contains(x.SLoai) && x.BDanhSach == true);
            List<DmChuKy> listBaoCao = _chuKyService.FindByCondition(predicate).OrderBy(x => x.Ten).ToList();
            List<DmChuKyModel> listBaoCaoModel = _mapper.Map<List<DmChuKyModel>>(listBaoCao);
            List<DmChuKyModel> data = new List<DmChuKyModel>();
            int parent = 1;
            int child = 0;
            foreach (var loaiBaoCao in ListLoaiBaoCao)
            {
                data.Add(new DmChuKyModel
                {
                    Ten = GetTenLoaiBaoCao(loaiBaoCao),
                    Stt = parent.ToString()
                });
                child = 1;
                foreach (var baoCao in listBaoCaoModel.Where(x => x.SLoai == loaiBaoCao))
                {
                    baoCao.Stt = string.Format("{0}.{1}", parent, child);
                    data.Add(baoCao);
                    child++;
                }
                parent++;
            }
            _listBaoCao = new ObservableCollection<DmChuKyModel>(data);
            OnPropertyChanged(nameof(ListBaoCao));
            _listBaoCaoView = CollectionViewSource.GetDefaultView(ListBaoCao);
            _listBaoCaoView.Filter = ListBaoCaoFilter;
        }

        private string GetTenLoaiBaoCao(string loaiBaoCao)
        {
            switch (loaiBaoCao)
            {
                case BHXHConstants.KHT_BHXH_BHYT_BHTN:
                    return "KẾ HOẠCH THU BHXH, BHYT, BHTN";
                case BHXHConstants.KHTM_BHYT_THANNHAN:
                    return "KẾ HOẠCH THU BHYT THÂN NHÂN";
                case BHXHConstants.KHC_CHEDOBHXH:
                    return "KẾ HOẠCH CHI CHẾ ĐỘ";
                case BHXHConstants.KHC_KINHPHIQUANLY:
                    return "KẾ HOẠCH CHI KINH PHÍ QUẢN LÝ";
                case BHXHConstants.KHC_KCB_QUANYDONVI:
                    return "KẾ HOẠCH CHI KCB";
                case BHXHConstants.KHC_KINHPHIKHAC:
                    return "KẾ HOẠCH CHI KINH PHÍ KHÁC";
                case BHXHConstants.TONGHOP_KH_THU_CHI:
                    return "TỔNG HỢP KẾ HOẠCH THU, CHI";
            }
            return string.Empty;
        }

        private bool ListBaoCaoFilter(object obj)
        {
            bool result = true;
            var item = (DmChuKyModel)obj;
            if (!string.IsNullOrEmpty(SearchText))
                result = result && item.Ten.ToLower().Contains(SearchText.Trim().ToLower());
            item.IsFilter = result;
            return result;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            object view = null;

            switch (SelectedItem.IdType)
            {
                case TypeChuKy.RPT_BHXH_KHT_CHITIET:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH { DataContext = PrintReportKhtBhxhViewModel };
                    break;
                case TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.PHU_LUC_II;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.PHU_LUC_II;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH { DataContext = PrintReportKhtBhxhViewModel };
                    break;
                case TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHTN:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.PHU_LUC_III;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.PHU_LUC_III;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH { DataContext = PrintReportKhtBhxhViewModel };
                    break;
                case TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_QUAN_NHAN:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.PHU_LUC_IV;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.PHU_LUC_IV;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH { DataContext = PrintReportKhtBhxhViewModel };
                    break;
                case TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHYT_NLD:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.PHU_LUC_V;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.PHU_LUC_V;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH { DataContext = PrintReportKhtBhxhViewModel };
                    break;
                case TypeChuKy.RPT_BHYT_KHTM_CHITIET:
                    PrintReportKhtmBhytViewModel.BHYTCheckPrintType = BHYTCheckPrintType.BHYT_DETAIL;
                    PrintReportKhtmBhytViewModel.ReportNameTypeValue = (int)BHYTCheckPrintType.BHYT_DETAIL;
                    PrintReportKhtmBhytViewModel.Init();
                    view = new PrintKhtmBHYT { DataContext = PrintReportKhtmBhytViewModel };
                    break;
                case TypeChuKy.RPT_BHYT_KHTM_THAN_NHAN:
                    PrintReportKhtmBhytViewModel.BHYTCheckPrintType = BHYTCheckPrintType.BHYT_THAN_NHAN;
                    PrintReportKhtmBhytViewModel.ReportNameTypeValue = (int)BHYTCheckPrintType.BHYT_THAN_NHAN;
                    PrintReportKhtmBhytViewModel.Init();
                    view = new PrintKhtmBHYT { DataContext = PrintReportKhtmBhytViewModel };
                    break;
                case TypeChuKy.RPT_BHYT_KHTM_HSSV:
                    PrintReportKhtmBhytViewModel.BHYTCheckPrintType = BHYTCheckPrintType.BHYT_HSSV;
                    PrintReportKhtmBhytViewModel.ReportNameTypeValue = (int)BHYTCheckPrintType.BHYT_HSSV;
                    PrintReportKhtmBhytViewModel.Init();
                    view = new PrintKhtmBHYT { DataContext = PrintReportKhtmBhytViewModel };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_CHITIET:
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = KhcCheckPrintType.KHCBHXHCT;

                    PrintReportLapKeHoachChiViewModel.IsSummary = false;
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = true;
                    PrintReportLapKeHoachChiViewModel.Name = "In kế hoạch chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Description = "In kế hoạch chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Init();
                    view = new PrintReportKeHoachChiCheDoBhXhChiTiet
                    {
                        DataContext = PrintReportLapKeHoachChiViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_TONGHOP:
                    PrintReportLapKeHoachChiViewModel.Name = " In dự toán chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.Description = "In dự toán chi các chế độ BHXH";
                    PrintReportLapKeHoachChiViewModel.IsShowTheoTongHop = false;
                    PrintReportLapKeHoachChiViewModel.KhcCheckPrintType = KhcCheckPrintType.KHCBHXHTH;
                    PrintReportLapKeHoachChiViewModel.Init();
                    PrintReportLapKeHoachChiViewModel.IsSummary = true;
                    view = new PrintReportKeHoachChiCheDoBhXhChiTiet
                    {
                        DataContext = PrintReportLapKeHoachChiViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_QLKP_CHITIET:
                    PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType.KHCQLKPCT;
                    PrintReportQuanLyKinhPhiViewModel.IsSummary = false;
                    PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = true;
                    PrintReportQuanLyKinhPhiViewModel.Name = "Kế hoạch chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.Description = "Kế hoạch chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.Init();

                    view = new PrintReportKeHoachChiQLKinhPhiChiTiet
                    {
                        DataContext = PrintReportQuanLyKinhPhiViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_QLKP_TONGHOP:
                    PrintReportQuanLyKinhPhiViewModel.Name = "Dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.Description = "Dự toán chi kinh phí quản lý BHXH, BHYT, BHTN";
                    PrintReportQuanLyKinhPhiViewModel.KhcQLKPCheckPrintType = KhcQLKPCheckPrintType.KHCQLKPTH;
                    PrintReportQuanLyKinhPhiViewModel.IsShowTheoTongHop = false;
                    PrintReportQuanLyKinhPhiViewModel.IsSummary = true;
                    PrintReportQuanLyKinhPhiViewModel.Init();
                    view = new PrintReportKeHoachChiQLKinhPhiChiTiet
                    {
                        DataContext = PrintReportQuanLyKinhPhiViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_CHITIET:
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcKcbCheckPrintType.KHCKCBBHXHCT;
                    PrintReportKhcKCBQYDVViewModel.IsSummary = false;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    PrintReportKhcKCBQYDVViewModel.Name = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = true;
                    PrintReportKhcKCBQYDVViewModel.Init();

                    view = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_KCB_QYDV_PHUlUC:
                    PrintReportKhcKCBQYDVViewModel.Name = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.Description = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKCBQYDVViewModel.KhcKcbCheckType = KhcKcbCheckPrintType.KHCKCBBHXHTH;
                    PrintReportKhcKCBQYDVViewModel.IsShowTheoTongHop = false;
                    PrintReportKhcKCBQYDVViewModel.Init();
                    PrintReportKhcKCBQYDVViewModel.IsSummary = true;
                    PrintReportKhcKCBQYDVViewModel.IsLoaiKCB = true;
                    view = new PrintReportKeHoachChiKCBQYDVChiTiet
                    {
                        DataContext = PrintReportKhcKCBQYDVViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_K_TSDK_CHITIET:
                    PrintReportKhcKViewModel.KhcKcbCheckType = KhcKcbCheckPrintType.KHCKCBBHXHCT;
                    PrintReportKhcKcbViewModel.IsSummary = false;
                    PrintReportKhcKcbViewModel.IsLoaiKCB = true;
                    PrintReportKhcKcbViewModel.Name = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKcbViewModel.Description = "In kế hoạch chi khác BHXH, BHYT";
                    PrintReportKhcKcbViewModel.IsShowTheoTongHop = true;
                    PrintReportKhcKcbViewModel.Init();

                    view = new PrintReportKeHoachChiKhacChiTiet
                    {
                        DataContext = PrintReportKhcKcbViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHC_K_HSSV_NLD_CHITIET:
                    PrintReportKhcKcbViewModel.Name = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKcbViewModel.Description = "In dự toán chi khác BHXH, BHYT";
                    PrintReportKhcKViewModel.KhcKcbCheckType = KhcKcbCheckPrintType.KHCKCBBHXHTH;
                    PrintReportKhcKcbViewModel.IsShowTheoTongHop = false;
                    PrintReportKhcKcbViewModel.Init();
                    PrintReportKhcKcbViewModel.IsSummary = true;
                    PrintReportKhcKcbViewModel.IsLoaiKCB = true;
                    view = new PrintReportKeHoachChiKhacChiTiet
                    {
                        DataContext = PrintReportKhcKcbViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_KHTC_TONG_HOP:
                    PrintReportKhtBhxhViewModel.IsEnabledUnit = true;
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    PrintReportKhtBhxhViewModel.Init();
                    view = new PrintKhtBHXH
                    {
                        DataContext = PrintReportKhtBhxhViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
