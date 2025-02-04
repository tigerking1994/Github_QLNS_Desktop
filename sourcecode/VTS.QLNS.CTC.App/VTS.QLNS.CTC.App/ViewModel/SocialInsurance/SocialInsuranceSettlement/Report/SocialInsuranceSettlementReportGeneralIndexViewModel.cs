using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PritnReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamKCB.PritnReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanNamChiKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.ThamDinhQuyetToan.PrintReport;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.Report
{
    public class SocialInsuranceSettlementReportGeneralIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintQuyetToanChiNamBHXHViewModel PrintQuyetToanChiNamBHXHViewModel;
        private PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel;
        private PrintQuyetToanChiNamKCViewModel PrintQuyetToanChiNamKCViewModel;
        private PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel;
        private PrintQuyetToanThuViewModel PrintQuyetToanThuViewModel;
        private PrintThamDinhQuyetToanViewModel PrintThamDinhQuyetToanViewModel;
        private PrintThamDinhTongHopThuChiViewModel PrintThamDinhTongHopThuChiViewModel;
        private PrintBaoCaoQuyetToanThuViewModel PrintBaoCaoQuyetToanThuViewModel;
        private PrintChiTieuKinhPhiViewModel PrintChiTieuKinhPhiViewModel;
        private PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel;

        public override string GroupName => BHXHConstants.GROUP_SETTLEMENT_REPORT;
        public override string Name => "Báo cáo tổng hợp quyết toán thu, chi - Năm";
        public override string Description => "Danh mục báo cáo tổng hợp quyết toán thu, chi - Năm";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocialInsuranceSettlementReportGeneralIndex);

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


        public SocialInsuranceSettlementReportGeneralIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            PrintQuyetToanChiNamBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel,
            PrintQuyetToanChiNamKCViewModel printQuyetToanChiNamKCViewModel,
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel printQuyetToanChiNamKinhPhiKhacNoticeViewModel,
            PrintQuyetToanThuViewModel printQuyetToanThuViewModel,
            PrintThamDinhQuyetToanViewModel printThamDinhQuyetToanViewModel,
            PrintThamDinhTongHopThuChiViewModel printThamDinhTongHopThuChiViewModel,
            PrintBaoCaoQuyetToanThuViewModel printBaoCaoQuyetToanThuViewModel,
            PrintChiTieuKinhPhiViewModel printChiTieuKinhPhiViewModel,
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel
            )
        {
            PrintQuyetToanChiNamBHXHViewModel = printQuyetToanChiNamBHXHViewModel;
            PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel = printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel;
            PrintQuyetToanChiNamKCViewModel = printQuyetToanChiNamKCViewModel;
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel = printQuyetToanChiNamKinhPhiKhacNoticeViewModel;
            PrintQuyetToanThuViewModel = printQuyetToanThuViewModel;
            PrintThamDinhQuyetToanViewModel = printThamDinhQuyetToanViewModel;
            PrintThamDinhTongHopThuChiViewModel = printThamDinhTongHopThuChiViewModel;
            PrintBaoCaoQuyetToanThuViewModel = printBaoCaoQuyetToanThuViewModel;
            PrintChiTieuKinhPhiViewModel = printChiTieuKinhPhiViewModel;
            PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel = printBaoCaoQuyetToanChiKinhPhiQuanLyViewModel;

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
                BHXHConstants.TONGHOP_QT_THU_CHI_BHXH,
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
                case BHXHConstants.TONGHOP_QT_THU_CHI_BHXH:
                    return "TỔNG HỢP QUYẾT TOÁN THU, CHI";                
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
            //object view = null;

            switch (SelectedItem.IdType)
            {
                case TypeChuKy.RPT_BH_THAM_DINH_QUYET_TOAN_THU_CHI:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_THAM_DINH_QUYET_TOAN_THU_CHI;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.RPT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI:
                    PrintThamDinhTongHopThuChiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_THONG_BAO_PHE_DUYET_QUYET_TOAN_NAM_TONG_HOP_THU_CHI;
                    PrintThamDinhTongHopThuChiViewModel.Init();
                    DialogHost.Show(new PrintThamDinhTongHopThuChi() { DataContext = PrintThamDinhTongHopThuChiViewModel }, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case TypeChuKy.RPT_Bao_Cao_TH_Quyet_Toan_Thu_Chi_BHXH_BHYT_BHTN:
                    PrintThamDinhTongHopThuChiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_TONG_HOP_QUYET_TOAN_THU_CHI_BHXH_BHYT_BHTN;
                    PrintThamDinhTongHopThuChiViewModel.Init();
                    DialogHost.Show(new PrintThamDinhTongHopThuChi() { DataContext = PrintThamDinhTongHopThuChiViewModel }, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case TypeChuKy.QUYET_TOAN_THU_BHXH:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHXH_BHYT_BHTN;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = true;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = false;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN:
                    PrintBaoCaoQuyetToanThuViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_THU_BHYT_THAN_NHAN;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHXH = false;
                    PrintBaoCaoQuyetToanThuViewModel.IsTypeBHYT = true;
                    PrintBaoCaoQuyetToanThuViewModel.Init();
                    PrintBaoCaoQuyetToanThuViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CHE_DO_BHXH:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_QUYET_TOAN_CHI_CHE_DO_BHXH;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET:
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_QUAN_LY_BHXH_BHYT;
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.Init();
                    var view1 = new PrintBaoCaoQuyetToanChiKinhPhiQuanLy
                    {
                        DataContext = PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_KCB_QYDV:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_QUAN_Y_DON_VI;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC:
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_TRUONG_SA_DK;
                    PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel.Init();
                    var view2 = new PrintBaoCaoQuyetToanChiKinhPhiQuanLy
                    {
                        DataContext = PrintBaoCaoQuyetToanChiKinhPhiQuanLyViewModel
                    };
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_CSSK_HSSV:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_KCB_HSSV_NLD;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.BH_THAM_DINH_QUYET_TOAN_CHI_MUA_SAM_TTBYT:
                    PrintChiTieuKinhPhiViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_CHI_TIEU_QUYET_TOAN_KINH_PHI_MUA_SAM_TTBYT;
                    PrintChiTieuKinhPhiViewModel.Init();
                    PrintChiTieuKinhPhiViewModel.ShowDialogHost();
                    break;
                case TypeChuKy.RPT_BH_DU_TOAN_KINH_PHI_CHUYEN_NAM_SAU:
                    PrintThamDinhQuyetToanViewModel.SettlementTypeValue = (int)BhThamDinhQuyetToanType.PRINT_BAO_CAO_DU_TOAN_KINH_PHI_BHXH_BHYT_BHTN_CHUYEN_NAM_SAU;
                    PrintThamDinhQuyetToanViewModel.Init();
                    PrintThamDinhQuyetToanViewModel.ShowDialogHost();
                    break;


                    //case TypeChuKy.QUYET_TOAN_THU_CHI_TONG_HOP:
                    //    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP;
                    //    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    //    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    //    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    //    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = false;
                    //    PrintQuyetToanThuViewModel.Init();
                    //    view = new PrintQuyetToanThu
                    //    {
                    //        DataContext = PrintQuyetToanThuViewModel
                    //    };
                    //    break;
                    //case TypeChuKy.QUYET_TOAN_TONG_HOP_NAM:
                    //    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_TONG_HOP_NAM;
                    //    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    //    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    //    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    //    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = false;
                    //    PrintQuyetToanThuViewModel.Init();
                    //    view = new PrintQuyetToanThu
                    //    {
                    //        DataContext = PrintQuyetToanThuViewModel
                    //    };
                    //    break;
            }
            //if (view != null)
            //    DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
