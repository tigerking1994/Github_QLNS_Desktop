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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Report
{
    public class SocialInsuranceEstimateReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private TongHopThuChiViewModel TongHopThuChiViewModel;
        private SocialInsuranceDivisionEstimatePrintSheetViewModel SocialInsuranceDivisionEstimatePrintSheetViewModel;
        private PrintPhuLucDuToanThuViewModel PrintPhuLucDuToanThuViewModel;
        private PrintPhuLucDuToanThuMuaBHYTViewModel PrintPhuLucDuToanThuMuaBHYTViewModel;
        public PrintPhanBoTuToanChiTheoDonViViewModel PrintPhanBoTuToanChiTheoDonViViewModel;

        public override string GroupName => BHXHConstants.GROUP_ESTIMATE_REPORT;
        public override string Name => "Báo cáo giao dự toán thu, chi";
        public override string Description => "Danh mục báo cáo giao dự toán thu, chi";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocialInsuranceEstimateReportIndex);    

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


        public SocialInsuranceEstimateReportIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            SocialInsuranceDivisionEstimatePrintSheetViewModel socialInsuranceDivisionEstimatePrintSheetViewModel,
            PrintPhuLucDuToanThuViewModel printPhuLucDuToanThuViewModel,
            PrintPhuLucDuToanThuMuaBHYTViewModel printPhuLucDuToanThuMuaBHYTViewModel,
            PrintPhanBoTuToanChiTheoDonViViewModel printPhanBoTuToanChiTheoDonViViewModel)
        {
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            SocialInsuranceDivisionEstimatePrintSheetViewModel = socialInsuranceDivisionEstimatePrintSheetViewModel;
            PrintPhuLucDuToanThuViewModel = printPhuLucDuToanThuViewModel;
            PrintPhuLucDuToanThuMuaBHYTViewModel = printPhuLucDuToanThuMuaBHYTViewModel;
            PrintPhanBoTuToanChiTheoDonViViewModel = printPhanBoTuToanChiTheoDonViViewModel;

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
                BHXHConstants.GIAO_DUTOAN_THU_CHI
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
                case BHXHConstants.GIAO_DUTOAN_THU_CHI:
                    return "GIAO DỰ TOÁN THU, CHI BHXH";
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
                case TypeChuKy.GIAO_DU_TOAN_TONG_HOP_THU_CHI:
                    TongHopThuChiViewModel.ReportNameTypeValue = (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    TongHopThuChiViewModel.ReportTypeValue = SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    TongHopThuChiViewModel.Init();
                    view = new TongHopThuChi
                    {
                        DataContext = TongHopThuChiViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_DU_TOAN_THU_BHXH:
                    PrintPhuLucDuToanThuViewModel.Init();
                    view = new PrintPhuLucDuToanThu
                    {
                        DataContext = PrintPhuLucDuToanThuViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHYT_KHTM_HSSV:
                    PrintPhuLucDuToanThuMuaBHYTViewModel.Init();
                    view = new PrintPhuLucDuToanThuMuaBHYT
                    {
                        DataContext = PrintPhuLucDuToanThuMuaBHYTViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_DT_PBC_CHI_BHXH_TONGHOP_DONVI_PHULUC:
                    PrintPhanBoTuToanChiTheoDonViViewModel.Init();
                    view = new PrintPhanBoTuToanChiTheoDonVi
                    {
                        DataContext = PrintPhanBoTuToanChiTheoDonViViewModel
                    };

                    break;
                case TypeChuKy.DU_TOAN_TONG_HOP_THU_CHI:
                    TongHopThuChiViewModel.ReportNameTypeValue = (int)SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    TongHopThuChiViewModel.ReportTypeValue = SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP;
                    TongHopThuChiViewModel.Init();
                    view = new TongHopThuChi
                    {
                        DataContext = TongHopThuChiViewModel
                    };

                    break;
                case TypeChuKy.DU_TOAN_DIEU_CHINH_BO_SUNG_THU_CHI:
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Init();
                    view = new PhanBoDuToanThuPrintReport
                    {
                        DataContext = SocialInsuranceDivisionEstimatePrintSheetViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
