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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PrintReportQtcqBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.Report;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiKinhPhiQuanLy.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyBHXH.PritnReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKCB.PritnReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiQuyKinhPhiKhac.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.Report
{
    public class SocailInsuranceSettlementReportQuarterIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintQuyetToanChiQuyBHXHViewModel PrintQuyetToanChiQuyBHXHViewModel;
        private PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel;
        private PrintQuyetToanChiQuyKCBViewModel PrintQuyetToanChiQuyKCBViewModel;
        private PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel;

        public override string GroupName => BHXHConstants.GROUP_SETTLEMENT_REPORT;
        public override string Name => "Báo cáo quyết toán chi - Quý";
        public override string Description => "Danh mục báo cáo quyết toán chi - Quý";
        public override PackIconKind IconKind => PackIconKind.FileDocument;
        public override Type ContentType => typeof(SocailInsuranceSettlementReportQuarterIndex);

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


        public SocailInsuranceSettlementReportQuarterIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            PrintQuyetToanChiQuyBHXHViewModel printQuyetToanChiQuyBHXHViewModel,
            PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel printQuyetToanChiKinhPhiQuanLyNoticeViewModel,
            PrintQuyetToanChiQuyKCBViewModel printQuyetToanChiQuyKCBViewModel,
            PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel printQuyetToanChiQuyKinhPhiKhacNoticeViewModel)
        {
            PrintQuyetToanChiQuyBHXHViewModel = printQuyetToanChiQuyBHXHViewModel;
            PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel = printQuyetToanChiKinhPhiQuanLyNoticeViewModel;
            PrintQuyetToanChiQuyKCBViewModel = printQuyetToanChiQuyKCBViewModel;
            PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel = printQuyetToanChiQuyKinhPhiKhacNoticeViewModel;

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
                BHXHConstants.QTC_CHEDOBHXH_QUY,
                BHXHConstants.QTC_KINHPHIQUANLY_QUY,
                BHXHConstants.QTC_KCB_QUANLYDONVI_QUY,
                BHXHConstants.QTC_KINHPHIKHAC_QUY,
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
                case BHXHConstants.QTC_CHEDOBHXH_QUY:
                    return "QUYẾT TOÁN CHI CHẾ ĐỘ BHXH";
                case BHXHConstants.QTC_KINHPHIQUANLY_QUY:
                    return "QUYẾT TOÁN CHI KINH PHÍ QUẢN LÝ";
                case BHXHConstants.QTC_KCB_QUANLYDONVI_QUY:
                    return "QUYẾT TOÁN CHI KCB QUÂN Y ĐƠN VỊ";
                case BHXHConstants.QTC_KINHPHIKHAC_QUY:
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
                case TypeChuKy.RPT_BH_QUYETTOANQUY_THONGTRIXACNHANQUYETTOANBHXH:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_THONGTRIXACNHANQUYETTOANBHXH;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOANQUY_BAOCAOQUYETTOANCHIBHXH:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_BAOCAOQUYETTOANCHIBHXH;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPOMDAU:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPOMDAU;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPTHAISAN:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPTHAISAN;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTAINHANLAODONGNGHENGHIEP:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTAINHANLAODONGNGHENGHIEP;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QUYETTOANQUY_GIAITHICHTROCAPHUUTRIXUATNGU:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_GIAITHICHTROCAPHUUTRIXUATNGU;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_NS_QUYETTOAN_TATCA_TONGHOP:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;

                case TypeChuKy.RPT_BH_QTC_QKPQL_THONGTRILOAI1:
                    PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_COMMUNICATE_SETTLEMENT_LNS;
                    PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.Init();
                    view = new PrintQuyetToanChiKinhPhiQuanLyNotice
                    {
                        DataContext = PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QTC_QKPQL_CHITIET:
                    PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT;
                    PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel.Init();
                    view = new PrintQuyetToanChiKinhPhiQuanLyNotice
                    {
                        DataContext = PrintQuyetToanChiKinhPhiQuanLyNoticeViewModel
                    };
                    break;

                case TypeChuKy.RPT_BH_QTC_KCB_THONGTRIQUYETTOANCHIKCB:
                    PrintQuyetToanChiQuyKCBViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyKCBType.PRINT_THONGTRIQUYETTOANCHIKINHPHIKCB;
                    PrintQuyetToanChiQuyKCBViewModel.Init();
                    view = new PrintQuyetToanChiQuyKCB
                    {
                        DataContext = PrintQuyetToanChiQuyKCBViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QTC_KCB_TONGHOPCACDONVICHIPHIKCB:
                    PrintQuyetToanChiQuyKCBViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyKCBType.PRINT_BAOCAOKCBQUANYDONVI;
                    PrintQuyetToanChiQuyKCBViewModel.Init();
                    view = new PrintQuyetToanChiQuyKCB
                    {
                        DataContext = PrintQuyetToanChiQuyKCBViewModel
                    };
                    break;
                case TypeChuKy.PRINT_DANHSACHNLDNGHIVIEC:
                    PrintQuyetToanChiQuyBHXHViewModel.SettlementTypeValue = (int)BhQuyetToanChiQuyType.PRINT_DANHSACHNLDNGHIVIEC;
                    PrintQuyetToanChiQuyBHXHViewModel.Init();
                    view = new PrintQuyetToanChiQuyBHXH
                    {
                        DataContext = PrintQuyetToanChiQuyBHXHViewModel
                    };
                    break;
                case TypeChuKy.RPT_BH_QTC_QKPK_MACDINH_KEHOACH:
                    PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.SettlementTypeValue = (int)SettlementTypePrint.PRINT_REGULARLY_SETTLEMENT;
                    PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel.Init();
                    view = new PrintQuyetToanChiQuyKinhPhiKhacNotice
                    {
                        DataContext = PrintQuyetToanChiQuyKinhPhiKhacNoticeViewModel
                    };
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
