using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.Report
{
    public class SocailInsuranceSettlementReportIndexViewModel : GridViewModelBase<DmChuKyModel>
    {
        private readonly IDmChuKyService _chuKyService;
        private readonly IMapper _mapper;
        private ICollectionView _listBaoCaoView;
        private PrintQuyetToanThuViewModel PrintQuyetToanThuViewModel;
        private PrintQuyetToanThuMuaViewModel PrintQuyetToanThuMuaViewModel;

        public override string GroupName => BHXHConstants.GROUP_SETTLEMENT_REPORT;
        public override string Name => "Báo cáo quyết toán thu ";
        public override string Description => "Danh mục báo cáo quyết toán thu";
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


        public SocailInsuranceSettlementReportIndexViewModel(IDmChuKyService chuKyService,
            IMapper mapper,
            PrintQuyetToanThuViewModel printQuyetToanThuViewModel,
            PrintQuyetToanThuMuaViewModel printQuyetToanThuMuaViewModel)
        {
            PrintQuyetToanThuViewModel = printQuyetToanThuViewModel;
            PrintQuyetToanThuMuaViewModel = printQuyetToanThuMuaViewModel;

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
                BHXHConstants.QT_THU_BHXH_BHYT_BHTN,
                BHXHConstants.QT_THU_BHYT_THANNHAN,                
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
                case BHXHConstants.QT_THU_BHXH_BHYT_BHTN:
                    return "QUYẾT TOÁN THU BHXH, BHYT, BHTN";
                case BHXHConstants.QT_THU_BHYT_THANNHAN:
                    return "QUYẾT TOÁN THU BHYT THÂN NHÂN";
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
                case TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = false;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuViewModel.IsEnableReportType = false;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuViewModel.IsEnableReportType = false;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_BHXH:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_BHXH;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_BHTN:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_BHTN;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_BHYT_QUAN_NHAN:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_BHYT_NLD:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = (int)QttType.QUYET_TOAN_THU_BHYT_NLD;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    view = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    break;

                case TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD:
                    PrintQuyetToanThuMuaViewModel.SettlementTypeValue = (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD;
                    PrintQuyetToanThuMuaViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuMuaViewModel.Init();
                    view = new PrintQuyetToanThuMua
                    {
                        DataContext = PrintQuyetToanThuMuaViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN:
                    PrintQuyetToanThuMuaViewModel.SettlementTypeValue = (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN;
                    PrintQuyetToanThuMuaViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuMuaViewModel.Init();
                    view = new PrintQuyetToanThuMua
                    {
                        DataContext = PrintQuyetToanThuMuaViewModel
                    };
                    break;
                case TypeChuKy.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB:
                    PrintQuyetToanThuMuaViewModel.SettlementTypeValue = (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB;
                    PrintQuyetToanThuMuaViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuMuaViewModel.Init();
                    view = new PrintQuyetToanThuMua
                    {
                        DataContext = PrintQuyetToanThuMuaViewModel
                    };
                    DialogHost.Show(view, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }
            if (view != null)
                DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }
    }
}
