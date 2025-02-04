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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.DieuChinhDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.Report
{
    public class SocialInsuranceEstimateAdjustReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private TongHopThuChiViewModel TongHopThuChiViewModel;
        private PrintReportDieuChinhDuToanViewModel PrintReportDieuChinhDuToanViewModel;
        private PrintReportDieuChinhDuToanThuViewModel PrintReportDieuChinhDuToanThuViewModel;

        public override string GroupName => BHXHConstants.GROUP_ESTIMATE_REPORT;
        public override string Name => "Báo cáo điều chỉnh dự toán thu, chi";
        public override string Description => "Danh mục báo cáo điều chỉnh dự toán thu, chi";
        public override PackIconKind IconKind => PackIconKind.FileDocumentEdit;
        public override Type ContentType => typeof(SocialInsuranceEstimateAdjustReportIndex);    

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


        public SocialInsuranceEstimateAdjustReportIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            PrintReportDieuChinhDuToanViewModel printReportDieuChinhDuToanViewModel,
            PrintReportDieuChinhDuToanThuViewModel printReportDieuChinhDuToanThuViewModel)
        {
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            PrintReportDieuChinhDuToanViewModel = printReportDieuChinhDuToanViewModel;
            PrintReportDieuChinhDuToanThuViewModel = printReportDieuChinhDuToanThuViewModel;

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
                BHXHConstants.DIEUCHINH_DUTOAN_THU,
                BHXHConstants.DIEUCHINH_DUTOAN_CHI
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
                case BHXHConstants.DIEUCHINH_DUTOAN_THU:
                    return "ĐIỀU CHỈNH DỰ TOÁN THU";
                case BHXHConstants.DIEUCHINH_DUTOAN_CHI:
                    return "ĐIỀU CHỈNH DỰ TOÁN CHI";
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
                case TypeChuKy.RPT_BHXH_DT_DC_DTT_CHITIET:
                    PrintReportDieuChinhDuToanThuViewModel.IsEnabledUnit = true;
                    PrintReportDieuChinhDuToanThuViewModel.IsPrintByUnit = true;
                    PrintReportDieuChinhDuToanThuViewModel.Name = "In báo cáo điều chỉnh dự toán thu BHXH, BHYT, BHTN";
                    PrintReportDieuChinhDuToanThuViewModel.Description = "In báo cáo điều chỉnh dự toán thu BHXH, BHYT, BHTN";
                    PrintReportDieuChinhDuToanThuViewModel.Init();

                    view = new PrintDieuChinhDuToanThu
                    {
                        DataContext = PrintReportDieuChinhDuToanThuViewModel
                    };
                    break;
                case TypeChuKy.RPT_BHXH_DT_DCDT_CHIBHXH_CHITIET:
                    PrintReportDieuChinhDuToanViewModel.dtDcDtCheckPrintType = DtDcDtCheckPrintType.DTDCTheoDonVi;
                    PrintReportDieuChinhDuToanViewModel.IsShowInTheoTongHop = true;
                    PrintReportDieuChinhDuToanViewModel.Name = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Description = "In điều chỉnh dự toán";
                    PrintReportDieuChinhDuToanViewModel.Init();

                    view = new PrintReportDieuChinhDuToanChiTiet
                    {
                        DataContext = PrintReportDieuChinhDuToanViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
