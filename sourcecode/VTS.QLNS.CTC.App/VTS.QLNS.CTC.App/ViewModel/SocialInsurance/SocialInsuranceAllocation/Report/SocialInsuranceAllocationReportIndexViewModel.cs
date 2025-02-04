using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.Report;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.Report
{
    public class SocialInsuranceAllocationReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private TongHopThuChiViewModel TongHopThuChiViewModel;
        private SocialInsuranceDivisionEstimatePrintSheetViewModel SocialInsuranceDivisionEstimatePrintSheetViewModel;
        private PrintChungTuCapPhatNoticeViewModel PrintChungTuCapPhatNoticeViewModel;
        private PrintChungTuCapPhatDonViViewModel PrintChungTuCapPhatDonViViewModel;
        private PrintCapPhatTamUngKCBBHYTViewModel PrintCapPhatTamUngKCBBHYTViewModel;
        private CapPhatBoSungReportViewModel CapPhatBoSungReportViewModel;

        public override string GroupName => BHXHConstants.GROUP_ALLOCATION_REPORT;
        public override string Name => "Báo cáo cấp phát";
        public override string Description => "Danh mục báo cáo cấp phát";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocialInsuranceAllocationReportIndex);

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


        public SocialInsuranceAllocationReportIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            SocialInsuranceDivisionEstimatePrintSheetViewModel socialInsuranceDivisionEstimatePrintSheetViewModel,
            PrintChungTuCapPhatNoticeViewModel printChungTuCapPhatNoticeViewModel,
            PrintChungTuCapPhatDonViViewModel printChungTuCapPhatDonViViewModel,
            PrintCapPhatTamUngKCBBHYTViewModel printCapPhatTamUngKCBBHYTViewModel,
            CapPhatBoSungReportViewModel capPhatBoSungReportViewModel)
        {
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            SocialInsuranceDivisionEstimatePrintSheetViewModel = socialInsuranceDivisionEstimatePrintSheetViewModel;
            PrintChungTuCapPhatNoticeViewModel = printChungTuCapPhatNoticeViewModel;
            PrintChungTuCapPhatDonViViewModel = printChungTuCapPhatDonViViewModel;
            PrintCapPhatTamUngKCBBHYTViewModel = printCapPhatTamUngKCBBHYTViewModel;
            CapPhatBoSungReportViewModel = capPhatBoSungReportViewModel;

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
                BHXHConstants.CAP_KINH_PHI,
                BHXHConstants.CAP_TAM_UNG_KCB_BHYT,
                BHXHConstants.CAP_BOSUNG_KCB_BHYT
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
                foreach (var baoCao in listBaoCaoModel.GroupBy(y => y.Ten, (key, group) => group.First()).Where(x => x.SLoai == loaiBaoCao))
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
                case BHXHConstants.CAP_KINH_PHI:
                    return "CẤP KINH PHÍ";
                case BHXHConstants.CAP_TAM_UNG_KCB_BHYT:
                    return "CẤP TẠM ỨNG KP KCB BHYT";
                case BHXHConstants.CAP_BOSUNG_KCB_BHYT:
                    return "CẤP BỔ SUNG KP KCB BHYT";
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
                case TypeChuKy.RPT_BH_CHI_BHXH_CAPPHAT_LNS:
                    PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = true;
                    PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)AllocationPrintTypeOfBH.PRINT_AllOCATION_NOTICE;
                    PrintChungTuCapPhatNoticeViewModel.Init();
                    view = new PrintChungTuCapPhatNotice
                    {
                        DataContext = PrintChungTuCapPhatNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_CHI_KINH_PHI_BHXH_CAPPHAT_DONVI:
                    PrintChungTuCapPhatDonViViewModel.Init();
                    view = new PrintChungTuCapPhatDonVi
                    {
                        DataContext = PrintChungTuCapPhatDonViViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_CP_KH_CBHXH:
                case TypeChuKy.RPT_BH_CP_KH_CKPQl_BHXH_BHYT:
                    PrintChungTuCapPhatNoticeViewModel.IsShowDotCap = false;
                    PrintChungTuCapPhatNoticeViewModel.AllocationPrintType = (AllocationPrintTypeOfBH)AllocationPrintTypeOfBH.PRINT_ALLOCATION_PLAN;
                    PrintChungTuCapPhatNoticeViewModel.Init();
                    view = new PrintChungTuCapPhatNotice
                    {
                        DataContext = PrintChungTuCapPhatNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_CAPPHAT_THONGTRI_TNQN:
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = false;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.Init();
                    view = new PrintCapPhatTamUngKCBBHYT
                    {
                        DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_CAPPHAT_TONGHOP_TNQN:
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = false;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.Init();
                    view = new PrintCapPhatTamUngKCBBHYT
                    {
                        DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_CAPPHAT_KEHOACH_TNQN:
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableKehoach = false;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableThongTri = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.IsEnableTongTop = true;
                    PrintCapPhatTamUngKCBBHYTViewModel.Init();
                    view = new PrintCapPhatTamUngKCBBHYT
                    {
                        DataContext = PrintCapPhatTamUngKCBBHYTViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_CPBS_THONG_TRI_TNQN:
                    CapPhatBoSungReportViewModel.IsEnableLNS = true;
                    CapPhatBoSungReportViewModel.IsEnableThongTri = false;
                    CapPhatBoSungReportViewModel.IsEnableTongTop = true;
                    CapPhatBoSungReportViewModel.IsEnableKehoach = true;
                    CapPhatBoSungReportViewModel.Init();
                    view = new CapPhatBoSungReport
                    {
                        DataContext = CapPhatBoSungReportViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_CPBS_TONG_HOP_THONG_TRI_TNQN:
                    CapPhatBoSungReportViewModel.IsEnableLNS = true;
                    CapPhatBoSungReportViewModel.IsEnableThongTri = true;
                    CapPhatBoSungReportViewModel.IsEnableTongTop = false;
                    CapPhatBoSungReportViewModel.IsEnableKehoach = true;
                    CapPhatBoSungReportViewModel.Init();
                    view = new CapPhatBoSungReport
                    {
                        DataContext = CapPhatBoSungReportViewModel
                    };
                    DialogHost.Show(view, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case TypeChuKy.RPT_BHXH_CPBS_QN:
                    CapPhatBoSungReportViewModel.IsEnableLNS = true;
                    CapPhatBoSungReportViewModel.IsEnableKehoach = false;
                    CapPhatBoSungReportViewModel.IsEnableThongTri = true;
                    CapPhatBoSungReportViewModel.IsEnableTongTop = true;
                    CapPhatBoSungReportViewModel.Init();
                    view = new CapPhatBoSungReport
                    {
                        DataContext = CapPhatBoSungReportViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
