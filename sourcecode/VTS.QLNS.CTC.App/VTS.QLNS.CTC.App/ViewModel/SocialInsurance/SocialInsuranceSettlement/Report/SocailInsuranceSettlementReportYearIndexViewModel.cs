using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.Report
{
    public class SocailInsuranceSettlementReportYearIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintQuyetToanChiNamBHXHViewModel PrintQuyetToanChiNamBHXHViewModel;        
        private PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel;        
        private PrintQuyetToanChiNamKCViewModel PrintQuyetToanChiNamKCViewModel;        
        private PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel;        

        public override string GroupName => BHXHConstants.GROUP_SETTLEMENT_REPORT;
        public override string Name => "Báo cáo quyết toán chi - Năm";
        public override string Description => "Danh mục báo cáo quyết toán chi - Năm";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocailInsuranceSettlementReportYearIndex);

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


        public SocailInsuranceSettlementReportYearIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            PrintQuyetToanChiNamBHXHViewModel printQuyetToanChiNamBHXHViewModel,
            PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel,
            PrintQuyetToanChiNamKCViewModel printQuyetToanChiNamKCViewModel,
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel printQuyetToanChiNamKinhPhiKhacNoticeViewModel
            )
        {
            PrintQuyetToanChiNamBHXHViewModel = printQuyetToanChiNamBHXHViewModel;
            PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel = printQuyetToanChiNamKinhPhiQuanLyNoticeViewModel;
            PrintQuyetToanChiNamKCViewModel = printQuyetToanChiNamKCViewModel;
            PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel = printQuyetToanChiNamKinhPhiKhacNoticeViewModel;

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
                BHXHConstants.QTC_CHEDOBHXH_NAM,
                BHXHConstants.QTC_KINHPHIQUANLY_NAM,
                BHXHConstants.QTC_KCB_QUANYDONVI_NAM,
                BHXHConstants.QTC_KINHPHIKHAC_NAM,
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
                case BHXHConstants.QTC_CHEDOBHXH_NAM:
                    return "QUYẾT TOÁN CHI CHẾ ĐỘ BHXH";
                case BHXHConstants.QTC_KINHPHIQUANLY_NAM:
                    return "QUYẾT TOÁN CHI KINH PHÍ QUẢN LÝ";
                case BHXHConstants.QTC_KCB_QUANYDONVI_NAM:
                    return "QUYẾT TOÁN CHI KCB QUÂN Y ĐƠN VỊ";
                case BHXHConstants.QTC_KINHPHIKHAC_NAM:
                    return "QUYẾT TOÁN CHI KINH PHÍ KHÁC";
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
                case TypeChuKy.RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH:
                    PrintQuyetToanChiNamBHXHViewModel.SettlementTypeValue = (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH;
                    PrintQuyetToanChiNamBHXHViewModel.Init();
                    view = new PrintQuyetToanChiNam
                    {
                        DataContext = PrintQuyetToanChiNamBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH:
                    PrintQuyetToanChiNamBHXHViewModel.SettlementTypeValue = (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH;
                    PrintQuyetToanChiNamBHXHViewModel.Init();
                    view = new PrintQuyetToanChiNam
                    {
                        DataContext = PrintQuyetToanChiNamBHXHViewModel
                    };
                    break;

                case TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET:
                    PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN;
                    PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.Init();
                    view = new PrintQuyetToanNamChiKinhPhiQuanLyNotice
                    {
                        DataContext = PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QTC_NKPQL_CHITIET_PHULUC:
                    PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM;
                    PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel.Init();
                    view = new PrintQuyetToanNamChiKinhPhiQuanLyNotice
                    {
                        DataContext = PrintQuyetToanChiNamKinhPhiQuanLyNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOAN_BAOCAOQUYETTOANCHIBHXH_KCB:
                    PrintQuyetToanChiNamKCViewModel.SettlementTypeValue = (int)BhQuyeToanChiNamType.PRINT_BAOCAOQUYETTOANCHIBHXH;
                    PrintQuyetToanChiNamKCViewModel.Init();
                    view = new PrintQuyetToanChiNamKCB
                    {
                        DataContext = PrintQuyetToanChiNamKCViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOAN_QUYETTOANCHIBHXH_KCB:
                    PrintQuyetToanChiNamKCViewModel.SettlementTypeValue = (int)BhQuyeToanChiNamType.PRINT_QUYETTOANCHIBHXH;
                    PrintQuyetToanChiNamKCViewModel.Init();
                    view = new PrintQuyetToanChiNamKCB
                    {
                        DataContext = PrintQuyetToanChiNamKCViewModel
                    };
                    break;

                case TypeChuKy.RPT_BH_QTC_NKPK_TSDK_CHITIET:
                    PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_SETTLEMENT_PALN;
                    PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.Init();
                    view = new PrintQuyetToanNamChiKinhPhiKhacNotice
                    {
                        DataContext = PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QTC_NKPK_TSDK_DONVI_PHULUC:
                    PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_SETTLEMENT_ADDENDUM;
                    PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel.Init();
                    view = new PrintQuyetToanNamChiKinhPhiKhacNotice
                    {
                        DataContext = PrintQuyetToanChiNamKinhPhiKhacNoticeViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
