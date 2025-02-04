using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.FunctionMap.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using DanhMuc = VTS.QLNS.CTC.Core.Domain.DanhMuc;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.PrintReport
{
    public class PrintReportDemandOrgViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private ICollectionView _nNganhCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsPhongBanService _iNsPhongBanService;
        private readonly ISktMucLucService _iSktMucLucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        public RelayCommand NoteCommand { get; }

        public NsBaoCaoGhiChuDialogViewModel NsBaoCaoGhiChuDialogViewModel { get; }
        public DmChuKyDialogViewModel DmChuKyDialogViewModel { get; }



        public override string Name
        {
            get => DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách theo từng đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo tổng hợp phân bổ số kiểm tra năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết nhận số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2}) theo từng đơn vị"
                : DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2})"
                : $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp năm {_sessionInfo.YearOfWork}";
        }

        public override string Title
        {
            get => DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách theo từng đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo tổng hợp phân bổ số kiểm tra năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết nhận số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2}) theo từng đơn vị"
                : DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2})"
                : DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo phương án phân bổ số kiểm tra"
                : DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo phương án phân bổ số kiểm tra 4554"
                : DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType)
                ? $"Báo cáo so sánh SKT phân bổ năm trước - năm nay"
                : $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp năm {_sessionInfo.YearOfWork}";
        }

        public override string Description
        {
            get => DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách theo từng đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo đơn vị năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo tổng hợp phân bổ số kiểm tra năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết phân bổ số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết nhận số kiểm tra theo ngành năm {_sessionInfo.YearOfWork}"
                : DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2}) theo từng đơn vị"
                : DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                ? $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp 3 năm ({_sessionInfo.YearOfWork} - {_sessionInfo.YearOfWork + 2})"
                : DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo phương án phân bổ số kiểm tra"
                : DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? $"Báo cáo phương án phân bổ số kiểm tra 4554"
                : DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType)
                ? $"Báo cáo so sánh SKT phân bổ năm trước - năm nay"
                : $"Báo cáo chi tiết số nhu cầu ngân sách tổng hợp năm {_sessionInfo.YearOfWork}";
        }

        public override Type ContentType => typeof(PrintReportDemandOrg);
        public static DemandCheckPrintType DemandCheckPrintType { get; set; }

        private string _txtTitleFirst;

        public string TxtTitleFirst
        {
            get => _txtTitleFirst;
            set
            {
                SetProperty(ref _txtTitleFirst, value);
                //SetProperty(ref _txtTitleSecond, "(Kèm theo Quyết định số ........., ngày 11/05/2021)");
            }
        }

        private string _txtTitleSecond;

        public string TxtTitleSecond
        {
            get => _txtTitleSecond;
            set => SetProperty(ref _txtTitleSecond, value);
        }
        private string _txtTitleThird;

        public string TxtTitleThird
        {
            get => _txtTitleThird;
            set
            {
                SetProperty(ref _txtTitleThird, value);
            }
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        public bool InMotToChecked { get; set; }
        public bool IsContainBVTC => DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType) && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem != "1";

        private bool _isContainBVTCChecked = true;

        public bool IsContainBVTCChecked
        {
            get => _isContainBVTCChecked;
            set
            {
                SetProperty(ref _isContainBVTCChecked, value);
                LoadDonVi();
            }
        }


        private ComboboxItem _paperPrintTypeSelected;

        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set
            {
                SetProperty(ref _paperPrintTypeSelected, value);
                LoadTitleFirst();
                if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
                {
                    LoadNNganh();
                }
                else if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
                {
                    if (value != null && value.ValueItem == "1")
                    {
                        IsInTheoTongHop = true;
                    }
                }
                KhoiSelected = KhoiItems.ElementAt(0);
                LoadDonVi();
                LoadLoaiDuLieu();
                LoadTypeChuKy();
                LoadTitleFirst();
                OnPropertyChanged(nameof(IsVisibilityRadioButtonNSBD));
                OnPropertyChanged(nameof(InMotToCheckedVisibility));
                OnPropertyChanged(nameof(IsContainBVTC));
                OnPropertyChanged(nameof(IsShowInTheoLoaiBaoCao));
                OnPropertyChanged(nameof(IsBaoCaoSoNhuCauTongHop_Nganh));
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiDuLieu = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsLoaiDuLieu
        {
            get => _itemsLoaiDuLieu;
            set => SetProperty(ref _itemsLoaiDuLieu, value);
        }

        private ComboboxItem _selectedLoaiDuLieu;

        public ComboboxItem SelectedLoaiDuLieu
        {
            get => _selectedLoaiDuLieu;
            set
            {
                SetProperty(ref _selectedLoaiDuLieu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsKieuGiayIn = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ItemsKieuGiayIn
        {
            get => _itemsKieuGiayIn;
            set => SetProperty(ref _itemsKieuGiayIn, value);
        }

        private ComboboxItem _selectedKieuGiayIn;

        public ComboboxItem SelectedKieuGiayIn
        {
            get => _selectedKieuGiayIn;
            set => SetProperty(ref _selectedKieuGiayIn, value);
        }

        private ObservableCollection<ComboboxItem> _khoiItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> KhoiItems
        {
            get => _khoiItems;
            set => SetProperty(ref _khoiItems, value);
        }

        private ComboboxItem _khoiSelected;

        public ComboboxItem KhoiSelected
        {
            get => _khoiSelected;
            set
            {
                SetProperty(ref _khoiSelected, value);
                LoadDonVi();
            }
        }

        private ObservableCollection<ComboboxItem> _bQuanLyItems = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BQuanLyItems
        {
            get => _bQuanLyItems;
            set => SetProperty(ref _bQuanLyItems, value);
        }

        private ComboboxItem _bQuanLySelected;

        public ComboboxItem BQuanLySelected
        {
            get => _bQuanLySelected;
            set
            {
                SetProperty(ref _bQuanLySelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;

        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        public IEnumerable<DonVi> ListUnit { get; set; }

        private ObservableCollection<CheckBoxItem> _listDonVi = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private List<CheckBoxItem> _lstIIdMaDonVi = new List<CheckBoxItem>();
        private bool _selectAllDonVi;

        public bool SelectAllDonVi
        {
            get => ListDonVi.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (var item in ListDonVi) item.IsChecked = _selectAllDonVi;
            }
        }

        public string LabelSelectedCountDonVi
        {
            get => $"ĐƠN VỊ ({ListDonVi.Count(item => item.IsChecked)}/{ListDonVi.Count})";
        }

        private string _searchDonVi;

        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _donViCollectionView.Refresh();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        private ComboboxItem _budgetSourceTypeSelected;

        public ComboboxItem BudgetSourceTypeSelected
        {
            get => _budgetSourceTypeSelected;
            set
            {
                SetProperty(ref _budgetSourceTypeSelected, value);
                LoadDonVi();
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
            }
        }

        private ObservableCollection<ComboboxItem> _budgetTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetTypes
        {
            get => _budgetTypes;
            set => SetProperty(ref _budgetTypes, value);
        }

        private ComboboxItem _budgetTypeSelected;

        public ComboboxItem BudgetTypeSelected
        {
            get => _budgetTypeSelected;
            set
            {
                SetProperty(ref _budgetTypeSelected, value);
                LoadDonVi();
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
            }
        }

        private ObservableCollection<CheckBoxItem> _listNNganh = new ObservableCollection<CheckBoxItem>();

        public ObservableCollection<CheckBoxItem> ListNNganh
        {
            get => _listNNganh;
            set => SetProperty(ref _listNNganh, value);
        }

        private bool _selectAllNNganh;

        public bool SelectAllNNganh
        {
            get
            {
                if (ListNNganh != null)
                {
                    return ListNNganh.All(item => item.IsChecked);
                }
                return false;
            }

            set
            {
                SetProperty(ref _selectAllNNganh, value);
                foreach (var item in ListNNganh) item.IsChecked = _selectAllNNganh;
            }
        }

        private ObservableCollection<CheckBoxItem> _listChuyenNganh = new ObservableCollection<CheckBoxItem>();
        public ObservableCollection<CheckBoxItem> ListChuyenNganh
        {
            get => _listChuyenNganh;
            set => SetProperty(ref _listChuyenNganh, value);
        }

        public string LabelSelectedCountNNganh
        {
            get
            {
                if (IsChuyenNganh)
                {
                    return ListNNganh != null
                        ? $"CHUYÊN NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                        : "CHUYÊN NGÀNH (0/0)";
                }
                return ListNNganh != null
                    ? $"NGÀNH ({ListNNganh.Count(item => item.IsChecked)}/{ListNNganh.Count})"
                    : "NGÀNH (0/0)";
            }
        }

        private string _searchNNganh;

        public string SearchNNganh
        {
            get => _searchNNganh;
            set
            {
                if (SetProperty(ref _searchNNganh, value))
                {
                    _nNganhCollectionView.Refresh();
                }
            }
        }

        private bool _isChuyenNganh;
        public bool IsChuyenNganh
        {
            get => _isChuyenNganh;
            set
            {
                if (SetProperty(ref _isChuyenNganh, value))
                {
                    if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                        || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
                    {
                        LoadNNganh();
                    }
                    if (DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType))
                    {
                        LoadNNganhSnc();
                    }
                    if (DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
                    {
                        LoadNNganhNhanSKT();
                    }
                }
                OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                OnPropertyChanged(nameof(SelectAllNNganh));
            }
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
                LoadNNganhSnc();
            }
        }

        public List<Guid> ListIdChungTuBaoCaoSncNganh { get; set; }


        public Visibility IsVisibilityNganh =>
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? Visibility.Visible
                : Visibility.Collapsed;
        public Visibility IsShowVoucherTypes =>
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? Visibility.Collapsed
                : Visibility.Visible;

        public Visibility IsShowBudgetSourceType => DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType) ? Visibility.Visible : Visibility.Collapsed;

        public Visibility IsShowVoucherType =>
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                ? Visibility.Collapsed
                : Visibility.Visible;

        public Visibility IsShowType =>
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
            || DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                ? Visibility.Collapsed
                : Visibility.Visible;

        public bool IsVisibilityRadioButtonNSBD =>
            (DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType)
            && VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSBD_Key)))
            ||
            ((DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType) || DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem == "1"
            && VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSBD_Key)));

        public Visibility InMotToCheckedVisibility => (DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType) && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem != "1") ? Visibility.Visible : Visibility.Collapsed;

        public bool IsBaoCaoSoKiemTra => DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType);

        public bool IsBaoCaoSoNhuCauTongHop =>
           DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType);

        public bool IsBaoCaoSoNhuCauTheoNganh =>
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType);

        public bool IsBaoCaoSoNhuCauTongHop_Nganh
        {
            get
            {
                if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                    || DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType))
                {
                    if (VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        return true;
                    }
                    else if (VoucherTypeSelected != null && int.Parse(VoucherTypeSelected.ValueItem).Equals(int.Parse(VoucherType.NSBD_Key)))
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        private List<DataReportDynamic> _lstDataDynamic = new List<DataReportDynamic>();
        private List<DataReportDynamic> _lstDataDynamicSummary = new List<DataReportDynamic>();
        private bool _isCheckedRadioBtnDacThu;


        private LoaiNSBD _loaiNSBD;
        public LoaiNSBD LoaiNSBD
        {
            get => _loaiNSBD;
            set
            {
                SetProperty(ref _loaiNSBD, value);
                LoadDonVi();
                LoadTypeChuKy();
                LoadTitleFirst();
            }
        }

        public bool IsShowLoaiDuLieu { get; set; }
        public bool IsShowLoaiBaoCao => !DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                                            && !DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                                                && !DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType);
        public bool IsBaoCaoTypeInValid => !DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                                                && !DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                                            && !DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType)
                                             && !DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType);
        public bool IsShowInTheoTongHop => (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                                                    || DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                                                        || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)) && _sessionInfo.IsQuanLyDonViCha;
        public Visibility IsShowInTheoLoaiBaoCao => ((
            DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
            && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem == "2")
            || DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType))
            ? Visibility.Visible : Visibility.Collapsed;


        public bool IsInMotTo { get; set; }


        public PrintReportDemandOrgViewModel(INsDonViService nsDonViService, IExportService exportService,
            ISktChungTuChiTietService sktChungTuChiTietService,
            ISktChungTuService sktChungTuService,
            IDanhMucService danhMucService,
            ISktMucLucService iSktMucLucService,
            INsBaoCaoGhiChuService ghiChuService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            INsPhongBanService iNsPhongBanService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper,
            ILog logger,
            DmChuKyDialogViewModel dmChuKyDialogViewModel,
            NsBaoCaoGhiChuDialogViewModel nsBaoCaoGhiChuDialogViewModel)
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _sktChungTuService = sktChungTuService;
            _danhMucService = danhMucService;
            _iSktMucLucService = iSktMucLucService;
            _dmChuKyService = dmChuKyService;
            _ghiChuService = ghiChuService;
            _iNsPhongBanService = iNsPhongBanService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
            NoteCommand = new RelayCommand(obj => OnNoteCommand());
        }


        #region Note
        private void OnNoteCommand()
        {
            var typeChuKy = (_typeChuky == TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A || _typeChuky == TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02B) ? TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A : _typeChuky;
            //string sType = (VoucherTypeSelected.ValueItem == "1") ? TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD : TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN;
            NsBaoCaoGhiChuDialogViewModel.IsBaoCaoSoNhuCauTongHop = IsBaoCaoSoNhuCauTongHop;
            NsBaoCaoGhiChuDialogViewModel.IsBaoCaoSoKiemTra = IsBaoCaoSoKiemTra;
            NsBaoCaoGhiChuDialogViewModel.LoaiNSBD = LoaiNSBD;
            NsBaoCaoGhiChuDialogViewModel.IsInTheoTongHop = IsInTheoTongHop;
            NsBaoCaoGhiChuDialogViewModel.SMaBaoCao = typeChuKy;
            NsBaoCaoGhiChuDialogViewModel.Init();

            NsBaoCaoGhiChuDialogViewModel.BQuanLySelected = NsBaoCaoGhiChuDialogViewModel.BQuanLyItems.FirstOrDefault(x => x.ValueItem == BQuanLySelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.BudgetSourceTypeSelected = NsBaoCaoGhiChuDialogViewModel.BudgetSourceTypes.FirstOrDefault(x => x.ValueItem == BudgetSourceTypeSelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.BudgetTypeSelected = NsBaoCaoGhiChuDialogViewModel.BudgetTypes.FirstOrDefault(x => x.ValueItem == BudgetTypeSelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.KhoiSelected = NsBaoCaoGhiChuDialogViewModel.KhoiItems.FirstOrDefault(x => x.ValueItem == KhoiSelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.PaperPrintTypeSelected = NsBaoCaoGhiChuDialogViewModel.PaperPrintTypes.FirstOrDefault(x => x.ValueItem == PaperPrintTypeSelected.ValueItem);
            NsBaoCaoGhiChuDialogViewModel.VoucherTypeSelected = NsBaoCaoGhiChuDialogViewModel.VoucherTypes.FirstOrDefault(x => x.ValueItem == VoucherTypeSelected.ValueItem);

            NsBaoCaoGhiChuDialogViewModel.ShowDialogHost("PrintReportDemand");
        }
        #endregion

        private void LoadBudgetSourceTypes()
        {
            BudgetSourceTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Tất cả", ValueItem = TypeLoaiNNS.TAT_CA.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách dự toán", ValueItem = TypeLoaiNNS.DU_TOAN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách bệnh viện tự chủ", ValueItem = TypeLoaiNNS.BENH_VIEN.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách doanh nghiệp", ValueItem = TypeLoaiNNS.DOANH_NGHIEP.ToString() }
            };

            BudgetSourceTypeSelected = BudgetSourceTypes.ElementAt(0);
        }

        private void LoadBudgetTypes()
        {
            BudgetTypes = new ObservableCollection<ComboboxItem> {
                new ComboboxItem() { DisplayItem = "Ngân sách quốc phòng", ValueItem = TypeLoaiNS.NS_QUOC_PHONG.ToString() },
                new ComboboxItem() { DisplayItem = "Ngân sách nhà nước chi hoạt động sự nghiệp và các hoạt động khác", ValueItem = TypeLoaiNS.NS_NHA_NUOC.ToString() },
            };

            BudgetTypeSelected = BudgetTypes.ElementAt(0);
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = true;
            LoaiNSBD = LoaiNSBD.DAC_THU;
            InitReportDefaultDate();
            Clear();
            if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                || DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
            {
                LoadNNganh();
            }
            if (DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                LoadNNganhSnc();
            }
            if (DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                LoadNNganhNhanSKT();
            }
            LoadBudgetSourceTypes();
            LoadBudgetTypes();
            LoadDonVi();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadPaperPrintTypes();
            LoadKieuGiayIn();
            LoadLoaiDuLieu();
            LoadVoucherTypes();
            LoadKhois();
            IsContainBVTCChecked = true;
            LoadBQuanLys();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        public void Clear()
        {
            _donViCollectionView = null;
        }

        private void LoadTypeChuKy()
        {
            switch (DemandCheckPrintType)
            {
                case DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH:
                    _typeChuky = TypeChuKy.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH;
                    break;
                case DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA:
                    _typeChuky = TypeChuKy.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA;
                    break;
                case DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI:
                    if (VoucherTypeSelected is object)
                    {
                        _typeChuky = VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key ? TypeChuKy.RPT_NS_PHANBO_SOKIEMTRA_DONVI_NSSD
                            : VoucherTypeSelected.ValueItem == VoucherType.NSBD_Key ? TypeChuKy.RPT_NS_PHANBO_SOKIEMTRA_DONVI_NSDTN : TypeChuKy.RPT_NS_PHANBO_SOKIEMTRA_DONVI;
                    }
                    break;
                case DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER:
                    if (VoucherTypeSelected is object)
                    {
                        _typeChuky = VoucherTypeSelected.ValueItem switch
                        {
                            VoucherType.NSSD_Key => TypeChuKy.RPT_NS_SNC_CHITIET_NSSD,
                            _ => LoaiNSBD == LoaiNSBD.DAC_THU ? TypeChuKy.RPT_NS_SNC_CHITIET_NSBD_DT : TypeChuKy.RPT_NS_SNC_CHITIET_NSBD_MHHV
                        };
                    }
                    break;
                case DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER:
                    _typeChuky = TypeChuKy.RPT_NS_SNC3Y_CHITIET;
                    break;
                case DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY:
                    if (VoucherTypeSelected is object)
                    {
                        _typeChuky = VoucherTypeSelected.ValueItem switch
                        {
                            VoucherType.NSSD_Key => TypeChuKy.RPT_NS_SNC_TONGHOP_NSSD,
                            _ => LoaiNSBD == LoaiNSBD.DAC_THU ? TypeChuKy.RPT_NS_SNC_TONGHOP_NSBD_DT : TypeChuKy.RPT_NS_SNC_TONGHOP_NSBD_MHHV
                        };
                    }
                    break;
                case DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY:
                    _typeChuky = TypeChuKy.RPT_NS_SNC3Y_TONGHOP;
                    break;
                case DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH:
                    _typeChuky = TypeChuKy.RPT_NS_SO_NHU_CAU_THEONGANH;
                    break;
                case DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH:
                    _typeChuky = TypeChuKy.RPT_NS_NHAN_SO_KIEM_TRA_THEONGANH;
                    break;
                case DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA:
                    if (PaperPrintTypeSelected is object)
                    {
                        _typeChuky = PaperPrintTypeSelected.ValueItem == "1" ? TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A : TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02B;
                    }
                    break;
                case DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY:
                    if (VoucherTypeSelected?.ValueItem == "1")
                    {
                        if (PaperPrintTypeSelected?.ValueItem == "1")
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD;
                        }
                        else
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD_TONGHOP;
                        }

                    }
                    else
                    {
                        if (PaperPrintTypeSelected?.ValueItem == "1")
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN;
                        }
                        else
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN_TONGHOP;
                        }
                    }
                    break;
            }

            if (DemandCheckPrintType.Equals(DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA) && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem == "2")
            {
                _typeChuky = TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA;
            }
        }

        public void LoadTitleFirst()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TxtTitleFirst = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TxtTitleSecond = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TxtTitleThird = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }


        public void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>();
            if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "3"}
                };
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    //new ComboboxItem {DisplayItem = "Biểu trình ký", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị dọc, mục lục ngang", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Tổng hợp mục lục - đơn vị", ValueItem = "3"}
                };
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp SNC mục lục ngang - đơn vị dọc", ValueItem = "3"},
                    new ComboboxItem {DisplayItem = "Tổng hợp SNC mục lục dọc - đơn vị ngang", ValueItem = "2"}
                };
            }
            else if (DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Chi tiết đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Tổng hợp chi tiết đơn vị", ValueItem = "3"}
                };
            }
            else if (DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Phụ lục", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Biểu trình ký", ValueItem = "1"}
                };
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ngang - mục lục dọc", ValueItem = "1"}
                };
            }
            else if (DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Tổng hợp đơn vị dọc - mục lục ngang", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp - đơn vị", ValueItem = "2"}
                };
            }
            else if (DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType))
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Chi tiết đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp - Chi tiết đơn vị", ValueItem = "2"}
                };
            }
            else
            {
                paperPrintTypes = new List<ComboboxItem>
                {
                    //new ComboboxItem {DisplayItem = "Số kiểm tra", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tổng hợp số kiểm tra giao cho đơn vị", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Phương án phân bổ số kiểm tra - HD4554", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Phương án phân bổ số kiểm tra", ValueItem = "3"}
                };
            }

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            _paperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }

        public virtual void LoadKieuGiayIn()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"}
            };

            ItemsKieuGiayIn = new ObservableCollection<ComboboxItem>(data);
            SelectedKieuGiayIn = _itemsKieuGiayIn.ElementAt(0);
        }

        public virtual void LoadLoaiDuLieu()
        {
            IsShowLoaiDuLieu = VoucherTypeSelected != null && VoucherTypeSelected.ValueItem.Equals(VoucherType.NSSD_Key)
                               && (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType)
                               && PaperPrintTypeSelected != null && PaperPrintTypeSelected.ValueItem.Equals("3")
                               || DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType)
                               && PaperPrintTypeSelected != null && PaperPrintTypeSelected.ValueItem.Equals("2"));
            if (IsShowLoaiDuLieu)
            {
                var data = new List<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Huy động tồn kho & tự chi", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tự chi", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Huy động tồn kho", ValueItem = "3"}
                };

                ItemsLoaiDuLieu = new ObservableCollection<ComboboxItem>(data);
                SelectedLoaiDuLieu = ItemsLoaiDuLieu.ElementAt(0);
            }
            OnPropertyChanged(nameof(IsShowLoaiDuLieu));
        }

        public void LoadCatUnitTypes()
        {
            _catUnitTypes = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(x => x.SGiaTri).ToList();
            _catUnitTypes = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _catUnitTypes.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            _catUnitTypeSelected = _catUnitTypes.Where(t => t.ValueItem == DonViTinh.NGHIN_DONG_VALUE).FirstOrDefault();
        }


        private ObservableCollection<CheckBoxItem> LoadNsDonVisTheoLoaiChungTu()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            var iTrangThai = StatusType.ACTIVE;
            var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
            var predicateKiemTraCapDV = PredicateBuilder.True<DonVi>();
            predicateKiemTraCapDV = predicateKiemTraCapDV.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1");
            bool isDvCap4 = _nsDonViService.FindByCondition(predicateKiemTraCapDV).Count() <= 0;
            if (isDvCap4)
            {
                predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "0");
            }
            else
            {
                if (loaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1");

                }
                else if (loaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)))
                {
                    predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && x.Loai == "1" && true.Equals(x.BCoNSNganh));

                }
            }
            //bao cao so nhu cau tong hop
            if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                var listIdDv = _sktChungTuService.FindByCondition(item => item.INamLamViec == yearOfWork
                                                                          && item.ILoai == DemandCheckType.DEMAND
                                                                          && item.ILoaiChungTu == loaiChungTu).Select(item => item.IIdMaDonVi).ToList();
                predicate = predicate.And(x => listIdDv.Contains(x.IIDMaDonVi));
            }
            ListUnit = _nsDonViService.FindByCondition(predicate);
            var result = ListUnit.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                NameItem = item.TenDonVi
            }).OrderBy(item => item.ValueItem);
            return new ObservableCollection<CheckBoxItem>(result);
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoChiTietSoNhuCau()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstIdDonVi = GetListIdDonViChungTuDuocXem();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));

                if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }

                var lstDonVi = _nsDonViService.FindByCondition(predicateDv);
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoChiTietSoNhuCau3Y()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var lstIdDonVi = GetListIdDonViNC3YChungTuDuocXem();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv);
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoPhanTongHopPhanBoSoKiemTra()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DISTRIBUTION;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.INamNganSach == yearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == budgetSource);
            predicate = predicate.And(x => x.ILoai == DemandCheckType.CHECK);
            predicate = predicate.And(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS);
            var lstChungTu = _sktChungTuService.FindByCondition(predicate);

            var predicateCt = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicateCt = predicateCt.And(x => x.INamLamViec == yearOfWork);
            predicateCt = predicateCt.And(x => x.INamNganSach == yearOfBudget);
            predicateCt = predicateCt.And(x => x.IIdMaNguonNganSach == budgetSource);
            if (IsContainBVTCChecked || (PaperPrintTypeSelected != null && PaperPrintTypeSelected.ValueItem.Equals("1")))
            {
                predicateCt = predicateCt.And(x => x.ILoai == iLoai || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL);
            }
            else
            {
                predicateCt = predicateCt.And(x => x.ILoai == iLoai);
            }
            predicateCt = predicateCt.And(x => x.ILoaiChungTu == loaiChungTu);
            var lstChiTiet = _sktChungTuChiTietService.FindByCondition(predicateCt).ToList();
            lstChiTiet = lstChiTiet.Where(x => x.FTuChi != 0 || x.FPhanCap != 0 || x.FMuaHangCapHienVat != 0).ToList();

            var lstCondition = from x in lstChiTiet
                               join y in lstChungTu on x.IIdCtsoKiemTra equals y.Id
                               select x;

            var lstIdDonVi = lstCondition.Select(x => x.IIdMaDonVi).Distinct().ToList();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                if (PaperPrintTypeSelected != null && _khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }
                //var donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList();
                ListUnit = lstDonVi;
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoSoSanhPhanBoSKT()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DISTRIBUTION;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork || x.INamLamViec == yearOfWork - 1);
            predicate = predicate.And(x => x.INamNganSach == yearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == budgetSource);
            predicate = predicate.And(x => x.ILoai == DemandCheckType.CHECK);
            predicate = predicate.And(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS);
            var lstChungTu = _sktChungTuService.FindByCondition(predicate);

            var predicateCt = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicateCt = predicateCt.And(x => x.INamLamViec == yearOfWork || x.INamLamViec == yearOfWork - 1);
            predicateCt = predicateCt.And(x => x.INamNganSach == yearOfBudget);
            predicateCt = predicateCt.And(x => x.IIdMaNguonNganSach == budgetSource);
            if (IsContainBVTCChecked || (PaperPrintTypeSelected != null && PaperPrintTypeSelected.ValueItem.Equals("1")))
            {
                predicateCt = predicateCt.And(x => x.ILoai == iLoai || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL);
            }
            else
            {
                predicateCt = predicateCt.And(x => x.ILoai == iLoai);
            }
            predicateCt = predicateCt.And(x => x.ILoaiChungTu == loaiChungTu);
            var lstChiTiet = _sktChungTuChiTietService.FindByCondition(predicateCt).ToList();
            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem.Equals("2"))
            {
                if (LoaiNSBD == LoaiNSBD.DAC_THU)
                {
                    lstChiTiet = lstChiTiet.Where(x => x.FPhanCap != 0).ToList();
                }
                else
                {
                    lstChiTiet = lstChiTiet.Where(x => x.FMuaHangCapHienVat != 0).ToList();
                }
            }

            if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem.Equals("1"))
            {
                lstChiTiet = lstChiTiet.Where(x => x.FTuChi != 0 || x.FPhanCap != 0 || x.FMuaHangCapHienVat != 0).ToList();
            }

            var lstCondition = from x in lstChiTiet
                               join y in lstChungTu on x.IIdCtsoKiemTra equals y.Id
                               select x;

            var lstIdDonVi = lstCondition.Select(x => x.IIdMaDonVi).Distinct().ToList();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                if (PaperPrintTypeSelected != null && _khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }
                //var donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList();
                ListUnit = lstDonVi;
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }
        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoPhanBoKiemTraDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            // var iLoai = DemandCheckType.DISTRIBUTION;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");

            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x => x.INamLamViec == yearOfWork);
            predicate = predicate.And(x => x.INamNganSach == yearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == budgetSource);
            predicate = predicate.And(x => x.ILoai == DemandCheckType.CHECK || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL);
            predicate = predicate.And(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS);
            var lstChungTu = _sktChungTuService.FindByCondition(predicate);


            var predicateCt = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicateCt = predicateCt.And(x => x.INamLamViec == yearOfWork);
            predicateCt = predicateCt.And(x => x.INamNganSach == yearOfBudget);
            predicateCt = predicateCt.And(x => x.IIdMaNguonNganSach == budgetSource);
            predicateCt = predicateCt.And(x => x.ILoai == DemandCheckType.DISTRIBUTION || x.ILoai == DemandCheckType.CORPORATIZED_HOSPITAL);
            predicateCt = predicateCt.And(x => x.ILoaiChungTu == loaiChungTu);
            var lstChiTiet = _sktChungTuChiTietService.FindByCondition(predicateCt).ToList();
            lstChiTiet = lstChiTiet.Where(x => x.FTuChi != 0 || x.FPhanCap != 0 || x.FMuaHangCapHienVat != 0 || x.FHuyDongTonKho != 0 || x.FThongBaoDonVi != 0).ToList();

            var lstCondition = from x in lstChiTiet
                               join y in lstChungTu on x.IIdCtsoKiemTra equals y.Id
                               select x;

            var lstIdDonVi = lstCondition.Select(x => x.IIdMaDonVi).Distinct().ToList();
            if (lstIdDonVi != null && lstIdDonVi.Count > 0)
            {
                var predicateDv = PredicateBuilder.True<DonVi>();
                predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
                predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
                if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }
                var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList();
                ListUnit = lstDonVi;
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        private ComboboxItem _voucherTypeSelected;

        public ComboboxItem VoucherTypeSelected
        {
            get => _voucherTypeSelected;
            set
            {
                SetProperty(ref _voucherTypeSelected, value);
                LoadDonVi();
                LoadLoaiDuLieu();
                LoadTypeChuKy();
                LoadTitleFirst();
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                OnPropertyChanged(nameof(IsVisibilityRadioButtonNSBD));
                OnPropertyChanged(nameof(IsBaoCaoSoNhuCauTongHop_Nganh));
            }
        }

        public void LoadDonVi()
        {
            if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType)
                || DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType)
                || DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                ListDonVi = new ObservableCollection<CheckBoxItem>();
            }
            else if (DemandCheckPrintType.REPORT_ORG_DEMAND_DETAIL_NUMBER.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoChiTietSoNhuCau();
            }
            else if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoChiTietSoNhuCauTongHop();
            }
            else if (DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoPhanBoKiemTraDonVi();
            }
            else if (DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoPhanTongHopPhanBoSoKiemTra();
            }
            else if (DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoChiTietSoNhuCau3YTongHop();
            }
            else if (DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoChiTietSoNhuCau3Y();
            }
            else if (DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType))
            {
                ListDonVi = LoadDonViBaoCaoSoSanhPhanBoSKT();
            }
            else
            {
                ListDonVi = LoadNsDonVisTheoLoaiChungTu();
                ListDonVi = new ObservableCollection<CheckBoxItem>(ListDonVi.GroupBy(item => item.ValueItem).Select(item => item.First()));
            }


            // Filter
            _donViCollectionView = CollectionViewSource.GetDefaultView(ListDonVi);
            _donViCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchDonVi)
                                                 || (obj is CheckBoxItem item &&
                                                     item.DisplayItem.Contains(_searchDonVi, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListDonVi)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                };
            }
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoChiTietSoNhuCauTongHop()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicateDv = PredicateBuilder.True<DonVi>();
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
            if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
            {
                predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
            }

            if (IsInTheoTongHop)
            {
                var lstChungTuDuocXem = GetListChungTuDuocXem();
                var chungTuDonVi = lstChungTuDuocXem.Where(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).FirstOrDefault();
                if (chungTuDonVi == null) return new ObservableCollection<CheckBoxItem>();

                predicateDv = predicateDv.And(x => chungTuDonVi.IIdMaDonVi == x.IIDMaDonVi);
                predicateDv = predicateDv.And(x => x.Loai.Equals(LoaiDonVi.ROOT));
            }
            else
            {
                var lstIdDonViCtuDuocXem = GetListIdDonViChungTuDuocXem();
                predicateDv = predicateDv.And(x => x.Loai.Equals(LoaiDonVi.NOI_BO));
                predicateDv = predicateDv.And(x => lstIdDonViCtuDuocXem.Contains(x.IIDMaDonVi));
                if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }
            }

            var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList();
            if (lstDonVi.Any())
            {
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private ObservableCollection<CheckBoxItem> LoadDonViBaoCaoChiTietSoNhuCau3YTongHop()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicateDv = PredicateBuilder.True<DonVi>();
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
            predicateDv = predicateDv.And(x => loaiNNS == 0 || (loaiNNS == 1 && x.Khoi == "2") || (loaiNNS == 2 && x.Khoi == "3") || (loaiNNS == 3 && x.Khoi == "1"));

            if (IsInTheoTongHop)
            {
                var lstChungTuDuocXem = GetListChungTuNC3YDuocXem();
                var chungTuDonVi = lstChungTuDuocXem.Where(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi)).FirstOrDefault();
                if (chungTuDonVi == null) return new ObservableCollection<CheckBoxItem>();

                predicateDv = predicateDv.And(x => chungTuDonVi.IIdMaDonVi == x.IIDMaDonVi);
                predicateDv = predicateDv.And(x => x.Loai.Equals(LoaiDonVi.ROOT));
            }
            else
            {
                var lstIdDonViCtuDuocXem = GetListIdDonViNC3YChungTuDuocXem();
                predicateDv = predicateDv.And(x => x.Loai.Equals(LoaiDonVi.NOI_BO));
                predicateDv = predicateDv.And(x => lstIdDonViCtuDuocXem.Contains(x.IIDMaDonVi));
                if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    predicateDv = predicateDv.And(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem));
                }
            }

            var lstDonVi = _nsDonViService.FindByCondition(predicateDv).ToList();
            if (lstDonVi.Any())
            {
                var result = lstDonVi.Select(item => new CheckBoxItem
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                    NameItem = item.TenDonVi
                }).OrderBy(item => item.ValueItem);
                return new ObservableCollection<CheckBoxItem>(result);
            }

            return new ObservableCollection<CheckBoxItem>();
        }

        private List<string> GetListIdDonViChungTuConTongHop()
        {
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
            predicate = predicate.And(x => x.ILoaiChungTu.Equals(loaiChungTu));
            predicate = predicate.And(x => true.Equals(x.BDaTongHop));
            var lstCtCon = _sktChungTuService.FindByCondition(predicate).ToList();
            if (lstCtCon.Any())
            {
                return lstCtCon.Select(x => x.IIdMaDonVi).ToList();
            }

            return new List<string>();
        }

        private List<string> GetListIdDonViChungTuDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DEMAND;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var loaiNS = BudgetTypeSelected != null ? BudgetTypeSelected.ValueItem : "1";
            IEnumerable<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, loaiNS,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            listChungTu = listChungTu.Where(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS).ToList();
            var lstIdDonVi = listChungTu.Select(x => x.IIdMaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }

        private List<string> GetListIdDonViNC3YChungTuDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            var iLoai = DemandCheckType.DEMAND3Y;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            IEnumerable<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            listChungTu = listChungTu.Where(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS).ToList();
            if (DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                listChungTu = listChungTu.Where(t => (bool)t.BDaTongHop);
            }
            var lstIdDonVi = listChungTu.Select(x => x.IIdMaDonVi).Distinct().ToList();
            return lstIdDonVi;
        }

        private List<NsSktChungTu> GetListChungTuDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DEMAND;
            int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            List<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            listChungTu = listChungTu.Where(x => loaiNNS == 0 || x.ILoaiNguonNganSach == loaiNNS).ToList();

            return listChungTu;
        }

        private List<NsSktChungTu> GetListChungTuNC3YDuocXem()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var iLoai = DemandCheckType.DEMAND3Y;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            List<NsSktChungTu> listChungTu;
            listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            return listChungTu;
        }

        public void LoadNNganh()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var iTrangThai = StatusType.ACTIVE;
            var predicateChungTu = PredicateBuilder.True<NsSktChungTuChiTiet>();
            List<NsSktChungTuChiTiet> lstCtChiTietPhanBo = new List<NsSktChungTuChiTiet>();

            predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.DISTRIBUTION);
            //if (_paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem.Equals("3"))
            //{
            predicateChungTu = predicateChungTu.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
            if (DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType) && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem != "1")
            {
                predicateChungTu = predicateChungTu.And(x => x.FTuChi != 0 || x.SoKiemTra != 0 || x.DuToan != 0);
                predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork - 1 || x.INamLamViec == yearOfWork);
                var lstCtChiTietPhanBoLastYearYear = _sktChungTuChiTietService.FindByCondition(predicateChungTu).ToList();
                if (lstCtChiTietPhanBoLastYearYear.Any())
                    lstCtChiTietPhanBo.AddRange(lstCtChiTietPhanBoLastYearYear);
            }
            else
            {
                predicateChungTu = predicateChungTu.And(x => x.FTuChi != 0);
                predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
                var lstCtChiTietPhanBoyear = _sktChungTuChiTietService.FindByCondition(predicateChungTu).ToList();
                if (lstCtChiTietPhanBoyear.Any())
                    lstCtChiTietPhanBo.AddRange(lstCtChiTietPhanBoyear);
            }
            //}

            List<string> lstChuyenNganh = new List<string>();
            List<NsSktMucLuc> lstMucLuc = new List<NsSktMucLuc>();
            if (lstCtChiTietPhanBo != null && lstCtChiTietPhanBo.Count() > 0)
            {
                var lstIdMl = lstCtChiTietPhanBo.Select(x => x.SKyHieu).Distinct().ToList();


                if (DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType) && _paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem != "1")
                {
                    var lstMucLucLastYear = _iSktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork - 1
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstIdMl.Contains(x.SKyHieu)).ToList();
                    if (lstMucLucLastYear.Any()) lstMucLuc.AddRange(lstMucLucLastYear);

                    var lstMucLucYear = _iSktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstIdMl.Contains(x.SKyHieu)).ToList();
                    if (lstMucLucYear.Any()) lstMucLuc.AddRange(lstMucLucYear);
                }
                else
                {
                    var lstMucLucYear = _iSktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstIdMl.Contains(x.SKyHieu)).ToList();
                    if (lstMucLucYear.Any()) lstMucLuc.AddRange(lstMucLucYear);
                }


                lstChuyenNganh = lstMucLuc.Select(x => x.SNg).Distinct().ToList();
            }

            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            predicate = predicate.And(item => item.ITrangThai == iTrangThai);

            // lấy list chuyên ngành
            var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
            predicateChuyenNganh = predicateChuyenNganh.And(item => lstChuyenNganh.Contains(item.IIDMaDanhMuc));
            var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
            var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDanhMuc,
                DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                NameItem = item.STen
            }).OrderBy(item => item.ValueItem);
            ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
            if (IsChuyenNganh)
            {
                ListNNganh = ListChuyenNganh;
            }
            else
            {
                var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
                lstDmNganh = lstDmNganh.Where(item => lstChuyenNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
                var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
                {
                    ValueItem = item.SGiaTri,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                    NameItem = item.STen
                }).OrderBy(item => item.ValueItem);
                ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
            }

            // Filter
            _nNganhCollectionView = CollectionViewSource.GetDefaultView(ListNNganh);
            _nNganhCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchNNganh)
                || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchNNganh, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListNNganh)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                    OnPropertyChanged(nameof(SelectAllNNganh));
                };
            }
        }

        public void LoadNNganhSnc()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var iTrangThai = StatusType.ACTIVE;
            ListIdChungTuBaoCaoSncNganh = new List<Guid>();
            var predicateTh = PredicateBuilder.True<NsSktChungTu>();
            predicateTh = predicateTh.And(x => x.INamLamViec == yearOfWork);
            predicateTh = predicateTh.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicateTh = predicateTh.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicateTh = predicateTh.And(x => x.ILoai == DemandCheckType.DEMAND);
            predicateTh = predicateTh.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
            predicateTh = predicateTh.And(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi));
            var ctTongHop = _sktChungTuService.FindByCondition(predicateTh).FirstOrDefault();
            List<NsSktChungTuChiTiet> lstCtChiTiet = new List<NsSktChungTuChiTiet>();
            if (IsInTheoTongHop)
            {
                if (ctTongHop != null)
                {
                    var predicateChungTu = PredicateBuilder.True<NsSktChungTuChiTiet>();
                    predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
                    predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                    predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                    predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.DEMAND);
                    predicateChungTu = predicateChungTu.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
                    if (!string.IsNullOrEmpty(ctTongHop.SDssoChungTuTongHop))
                    {
                        predicateChungTu = predicateChungTu.And(x => ctTongHop.SDssoChungTuTongHop.Contains(x.ChungTu.SSoChungTu));
                    }
                    else
                    {
                        predicateChungTu = predicateChungTu.And(x => x.IIdCtsoKiemTra == ctTongHop.Id);
                    }
                    lstCtChiTiet = _sktChungTuChiTietService.FindByCondition(predicateChungTu).ToList();
                    ListIdChungTuBaoCaoSncNganh = lstCtChiTiet.Select(x => x.IIdCtsoKiemTra).Distinct().ToList();
                }
            }
            else
            {
                IEnumerable<NsSktChungTu> listChungTu;
                listChungTu = _sktChungTuService
                    .FindChungTuIndexByCondition(DemandCheckType.DEMAND, yearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, int.Parse(VoucherType.NSSD_Key), "1", _sessionInfo.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
                listChungTu = listChungTu.Where(x => x.IIdMaDonVi != _sessionInfo.IdDonVi);
                var lstIdChungTuKhoa = listChungTu.Select(x => x.Id).ToList();
                ListIdChungTuBaoCaoSncNganh.AddRange(lstIdChungTuKhoa);
                var predicateChungTu = PredicateBuilder.True<NsSktChungTuChiTiet>();
                predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
                predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.DEMAND);
                predicateChungTu = predicateChungTu.And(x => lstIdChungTuKhoa.Contains(x.IIdCtsoKiemTra));
                lstCtChiTiet = _sktChungTuChiTietService.FindByCondition(predicateChungTu).ToList();
            }


            List<string> lstChuyenNganh = new List<string>();
            if (lstCtChiTiet.Count() > 0)
            {
                var lstIdMl = lstCtChiTiet.Select(x => x.IIdMlskt).Distinct().ToList();
                var lstMucLuc = _iSktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstIdMl.Contains(x.IIDMLSKT)).ToList();

                lstChuyenNganh = lstMucLuc.Select(x => x.SNg).Distinct().ToList();
            }

            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            predicate = predicate.And(item => item.ITrangThai == iTrangThai);
            // lấy list chuyên ngành
            var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
            predicateChuyenNganh = predicateChuyenNganh.And(item => lstChuyenNganh.Contains(item.IIDMaDanhMuc));
            var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
            var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDanhMuc,
                DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                NameItem = item.STen
            }).OrderBy(item => item.ValueItem);
            ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
            if (IsChuyenNganh)
            {
                ListNNganh = ListChuyenNganh;
            }
            else
            {
                var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
                lstDmNganh = lstDmNganh.Where(item => lstChuyenNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
                var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
                {
                    ValueItem = item.SGiaTri,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                    NameItem = item.STen
                }).OrderBy(item => item.ValueItem);
                ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
            }

            // Filter
            _nNganhCollectionView = CollectionViewSource.GetDefaultView(ListNNganh);
            _nNganhCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchNNganh)
                || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchNNganh, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListNNganh)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                    OnPropertyChanged(nameof(SelectAllNNganh));
                };
            }
        }

        public void LoadNNganhNhanSKT()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var iTrangThai = StatusType.ACTIVE;
            var predicateChungTu = PredicateBuilder.True<NsSktChungTuChiTiet>();
            predicateChungTu = predicateChungTu.And(x => x.INamLamViec == yearOfWork);
            predicateChungTu = predicateChungTu.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicateChungTu = predicateChungTu.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicateChungTu = predicateChungTu.And(x => x.ILoai == DemandCheckType.CHECK);
            //if (_paperPrintTypeSelected != null && _paperPrintTypeSelected.ValueItem.Equals("3"))
            //{
            predicateChungTu = predicateChungTu.And(x => x.ILoaiChungTu == int.Parse(VoucherType.NSSD_Key));
            predicateChungTu = predicateChungTu.And(x => x.FTuChi != 0);
            //}
            var lstCtChiTietPhanBo = _sktChungTuChiTietService.FindByCondition(predicateChungTu);
            List<string> lstChuyenNganh = new List<string>();
            if (lstCtChiTietPhanBo != null && lstCtChiTietPhanBo.Count() > 0)
            {
                var lstSKyHieu = lstCtChiTietPhanBo.Select(x => x.SKyHieu).Distinct().ToList();
                var lstMucLuc = _iSktMucLucService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork
                                                                        && x.ITrangThai == StatusType.ACTIVE
                                                                        && lstSKyHieu.Contains(x.SKyHieu)).ToList();

                lstChuyenNganh = lstMucLuc.Select(x => x.SNg).Distinct().ToList();
            }

            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(item => item.INamLamViec == yearOfWork);
            predicate = predicate.And(item => item.ITrangThai == iTrangThai);

            // lấy list chuyên ngành
            var predicateChuyenNganh = predicate.And(item => VoucherType.DM_Nganh.Equals(item.SType));
            predicateChuyenNganh = predicateChuyenNganh.And(item => lstChuyenNganh.Contains(item.IIDMaDanhMuc));
            var lstDmChuyenNganh = _danhMucService.FindByCondition(predicateChuyenNganh).ToList();
            var result = lstDmChuyenNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDanhMuc,
                DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                NameItem = item.STen
            }).OrderBy(item => item.ValueItem);
            ListChuyenNganh = new ObservableCollection<CheckBoxItem>(result);
            if (IsChuyenNganh)
            {
                ListNNganh = ListChuyenNganh;
            }
            else
            {
                var predicateNhomNganh = predicate.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                var lstDmNganh = _danhMucService.FindByCondition(predicateNhomNganh).ToList();
                lstDmNganh = lstDmNganh.Where(item => lstChuyenNganh.Any(x => item.SGiaTri.Contains(x))).ToList();
                var resultN = lstDmNganh.Where(item => item.ITrangThai == 1).Select(item => new CheckBoxItem
                {
                    ValueItem = item.SGiaTri,
                    DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDanhMuc, item.STen),
                    NameItem = item.STen
                }).OrderBy(item => item.ValueItem);
                ListNNganh = new ObservableCollection<CheckBoxItem>(resultN);
            }

            // Filter
            _nNganhCollectionView = CollectionViewSource.GetDefaultView(ListNNganh);
            _nNganhCollectionView.Filter = obj => string.IsNullOrWhiteSpace(_searchNNganh)
                || (obj is CheckBoxItem item && item.DisplayItem.Contains(_searchNNganh, StringComparison.OrdinalIgnoreCase));

            foreach (var org in ListNNganh)
            {
                org.PropertyChanged += (sender, args) =>
                {
                    OnPropertyChanged(nameof(LabelSelectedCountNNganh));
                    OnPropertyChanged(nameof(SelectAllNNganh));
                };
            }
        }

        private void LoadVoucherTypes()
        {
            var voucherTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key},
            };

            VoucherTypes = new ObservableCollection<ComboboxItem>(voucherTypes);
            VoucherTypeSelected = VoucherTypes.ElementAt(0);
        }

        public void LoadKhois()
        {
            var khoiItems = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = TypeKhoi.TAT_CA.ToString()},
                new ComboboxItem {DisplayItem = "Doanh nghiệp", ValueItem = TypeKhoi.DOANH_NGHIEP.ToString()},
                new ComboboxItem {DisplayItem = "Dự toán", ValueItem = TypeKhoi.DU_TOAN.ToString()},
                new ComboboxItem {DisplayItem = "Bệnh viện tự chủ", ValueItem = TypeKhoi.BENH_VIEN.ToString()},
            };
            KhoiItems = new ObservableCollection<ComboboxItem>(khoiItems);
            KhoiSelected = KhoiItems.ElementAt(0);
        }

        public void LoadBQuanLys()
        {
            _bQuanLyItems = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);

            List<DmBQuanLy> data = _iNsPhongBanService.FindByCondition(predicate).ToList();
            _bQuanLyItems = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _bQuanLyItems.Insert(0, new ComboboxItem { DisplayItem = "Tất cả", ValueItem = "0" });
            _bQuanLySelected = _bQuanLyItems.FirstOrDefault();
        }

        public void OnExport(ExportType exportType)
        {
            if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    if (_voucherTypeSelected != null && int.Parse(_voucherTypeSelected.ValueItem)
                        .Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        OnPrintReportDemandSummaryPhuLuc3(exportType);
                    }
                    else
                    {
                        if (LoaiNSBD == LoaiNSBD.DAC_THU)
                        {
                            OnPrintReportDemandSummaryPhuLuc4(exportType);
                        }
                        else
                        {
                            OnPrintReportDemandSummaryPhuLuc5(exportType);
                        }
                    }

                }
                else if (_paperPrintTypeSelected.ValueItem == "2")
                {
                    OnPrintReportDemandSummaryPhuLuc2(exportType);
                }
                else if (_paperPrintTypeSelected.ValueItem == "3")
                {
                    OnPrintReportDemandSummaryPhuLuc1(exportType);
                }
            }
            else if (DemandCheckPrintType.REPORT_DEMAND3Y_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                OnPrintReportDemandSummaryPhuLuc6(exportType);
            }
            else if (DemandCheckPrintType.REPORT_ORG_DEMAND3Y_DETAIL_NUMBER.Equals(DemandCheckPrintType))
            {
                OnPrintReportDemandDetailPhuLuc6(exportType);
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_SO_NHU_CAU_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "2")
                {
                    OnPrintReportSoNhuCauTheoNganhPhuLuc(exportType);
                }
                else
                {
                    OnPrintReportSoNhuCauTheoNganhPhuLucDonViDoc(exportType);
                }
            }
            else if (DemandCheckPrintType.REPORT_PHAN_BO_SO_KIEM_TRA_THEO_DON_VI.Equals(DemandCheckPrintType))
            {
                if (_voucherTypeSelected != null && int.Parse(_voucherTypeSelected.ValueItem)
                    .Equals(int.Parse(VoucherType.NSSD_Key)))
                {
                    if (IsInMotTo)
                    {
                        if (exportType == ExportType.EXCEL)
                        {
                            OnPrintReportPhanBoTheoDonViExcel(ExportType.EXCEL_ONE_PAPER);
                        }
                        else
                        {
                            OnPrintReportPhanBoTheoDonVi(ExportType.PDF_ONE_PAPER);
                        }
                    }
                    else
                    {
                        OnPrintReportPhanBoTheoDonVi(exportType);
                    }
                }
                else
                {
                    OnPrintReportChiTietPhanBoSoKiemTraNSBD(exportType);
                }

            }
            else if (DemandCheckPrintType.REPORT_TONG_HOP_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    OnPrintReportTongHopPhanBoTrinhKy(exportType);
                }
                else
                {
                    if (!InMotToChecked)
                    {
                        OnPrintReportTongHopPhanBo(exportType);
                    }
                    else if (exportType != ExportType.EXCEL)
                    {
                        OnPrintReportTongHopPhanBoOnePaper(ExportType.PDF_ONE_PAPER);
                    }
                    else
                    {
                        OnPrintReportTongHopPhanBoOnePaper_Excel(ExportType.EXCEL);
                    }
                }
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_PHAN_BO_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    OnPrintReportPhanBoTheoNganhPhuLuc(exportType);
                }
                else if (_paperPrintTypeSelected.ValueItem == "2")
                {
                    if (IsInMotTo)
                    {
                        if (exportType == ExportType.EXCEL)
                        {
                            OnPrintReportPhanBoTheoNganhPhuLucDonViDocExcelTren1To(ExportType.EXCEL_ONE_PAPER);
                        }
                        else
                        {
                            OnPrintReportPhanBoTheoNganhPhuLucDonViDoc(ExportType.PDF_ONE_PAPER);
                        }
                    }
                    else
                    {
                        OnPrintReportPhanBoTheoNganhPhuLucDonViDoc(exportType);
                    }
                }
                else
                {
                    ReportDemandDetail(exportType);
                }
            }
            else if (DemandCheckPrintType.REPORT_CHI_TIET_NHAN_SO_KIEM_TRA_THEO_NGANH.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    OnPrintReportNhanSoKiemTraTheoNganhPhuLuc(exportType);
                }
            }
            else if (DemandCheckPrintType.REPORT_PHUONG_AN_PHAN_BO_SO_KIEM_TRA.Equals(DemandCheckPrintType))
            {
                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    OnPrintReportPhuongAnPhanBoSKT02A(exportType);
                }
                else
                {
                    OnPrintReportPhuongAnPhanBoSKT02B(exportType);
                }
            }
            else if (DemandCheckPrintType.REPORT_SO_SANH_PHAN_BO_SKT_NAM_TRUOC_NAM_NAY.Equals(DemandCheckPrintType))
            {
                if (PaperPrintTypeSelected != null && PaperPrintTypeSelected.ValueItem.Equals("1"))
                {
                    OnExportCompare(exportType);
                }
                else
                {
                    OnExportCompareTongHop(exportType);
                }
            }
            else
            {
                if (ListDonVi.Where(item => item.IsChecked).Count() <= 0)
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }

                if (_paperPrintTypeSelected.ValueItem == "1")
                {
                    if (_voucherTypeSelected != null && int.Parse(_voucherTypeSelected.ValueItem)
                        .Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        OnPrintReportDemandDetailPhuLuc3(exportType);
                    }
                    else
                    {
                        if (LoaiNSBD == LoaiNSBD.DAC_THU)
                        {
                            OnPrintReportDemandDetailPhuLuc4(exportType);
                        }
                        else
                        {
                            OnPrintReportDemandDetailPhuLuc5(exportType);
                        }
                    }

                }
                else if (_paperPrintTypeSelected.ValueItem == "2")
                {
                    OnPrintReportDemandSummaryPhuLuc1(exportType);
                }
                else if (_paperPrintTypeSelected.ValueItem == "3")
                {
                    OnPrintReportDemandSummaryPhuLuc2(exportType);
                }
            }
        }

        public void OnPrintReportPhanBoTheoNganh(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected != null ? $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}" : "";
                    var tenNganh = "";
                    var predicateN = PredicateBuilder.True<DanhMuc>();
                    predicateN = predicateN.And(item => item.INamLamViec == yearOfWork);
                    predicateN = predicateN.And(item => item.ITrangThai == StatusType.ACTIVE);
                    predicateN = predicateN.And(item => VoucherType.VOCHER_TYPE.Equals(item.SType));
                    var lstNganhh = _danhMucService.FindByCondition(predicateN).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ReportPhanBoKiemTraTheoNganhQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhQuery>();
                    var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    {
                        listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganh(lstIdCodeSelected, null, yearOfWork, yearOfBudget, budgetSource, donViTinh)
                            .ToList();
                        listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0 || x.MuaHangHienVat != 0 || x.DacThu != 0).ToList();
                        var listIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                        foreach (var mlc in sktMucLucs)
                        {
                            if (!listIdMucLuc.Contains(mlc.SKyHieu))
                            {
                                ReportPhanBoKiemTraTheoNganhQuery mlCha = new ReportPhanBoKiemTraTheoNganhQuery();
                                mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                                mlCha.IIdMlskt = mlc.IIDMLSKT;
                                mlCha.sKyHieu = mlc.SKyHieu;
                                mlCha.STT = mlc.SSTT;
                                mlCha.SSTTBC = mlc.SSttBC;
                                mlCha.sNG = mlc.SNg;
                                mlCha.sMoTa = mlc.SMoTa;
                                mlCha.sNgCha = mlc.SNGCha;
                                mlCha.BHangCha = mlc.BHangCha;
                                listDataDonVi.Add(mlCha);
                            }
                        }

                        listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                        foreach (var nn in ListNNganh)
                        {
                            if (nn.IsChecked)
                            {
                                var nnganh = lstNganhh.FirstOrDefault(item => item.SGiaTri != null && item.SGiaTri.Contains(nn.ValueItem));
                                if (nnganh != null)
                                {
                                    tenNganh = nnganh.STen;
                                    break;
                                }
                            }
                        }
                    }

                    CalculateData(listDataDonVi);
                    listDataDonVi = listDataDonVi.Where(item => Math.Abs(item.MuaHangHienVat) >= double.Epsilon ||
                        Math.Abs(item.DacThu) >= double.Epsilon || Math.Abs(item.PhanCap) >= double.Epsilon).OrderBy(t => t.SSTTBC).ToList();

                    var sumTuChi = listDataDonVi.Where(item => item.IIdMlsktCha == Guid.Empty).Sum(item => item.TuChi);
                    var sumMuaHangHienVat = listDataDonVi.Where(item => item.IIdMlsktCha == Guid.Empty).Sum(item => item.MuaHangHienVat);
                    var sumDacThu = listDataDonVi.Where(item => item.IIdMlsktCha == Guid.Empty).Sum(item => item.PhanCap);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", (_diaDiem != null ? _diaDiem : "") + " ," + DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", "");
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    data.Add("h2", "");
                    data.Add("TenNganh", tenNganh);
                    data.Add("SumTuChi", sumTuChi);
                    data.Add("SumMuaHangHienVat", sumMuaHangHienVat);
                    data.Add("SumDacThu", sumDacThu);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_TRINHKY);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhQuery>(templateFileName, data);
                    e.Result = new ExportResult("Báo cáo phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportChiTietPhanBoSoKiemTraNSBD(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected != null ? $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}" : "";
                    List<CheckBoxItem> listIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ReportChiTietPhanBoKiemTraNsbdQuery> listDataDonVi = new List<ReportChiTietPhanBoKiemTraNsbdQuery>();
                    foreach (var dv in listIdDonVi)
                    {
                        listDataDonVi = _sktChungTuChiTietService.FindReportChiTietPhanBoKiemTraNSBD(dv.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh)
                                .ToList();
                        listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0 || x.MuaHangHienVat != 0 || x.DacThu != 0 || x.HuyDongTonKho != 0 || x.ThongBaoDV != 0).ToList();
                        var listIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                        foreach (var mlc in sktMucLucs)
                        {
                            if (!listIdMucLuc.Contains(mlc.SKyHieu))
                            {
                                ReportChiTietPhanBoKiemTraNsbdQuery mlCha = new ReportChiTietPhanBoKiemTraNsbdQuery();
                                mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                                mlCha.IIdMlskt = mlc.IIDMLSKT;
                                mlCha.sKyHieu = mlc.SKyHieu;
                                mlCha.STT = mlc.SSTT;
                                mlCha.SSTTBC = mlc.SSttBC;
                                mlCha.sL = (string.IsNullOrEmpty(mlc.SNGCha) && mlc.IIDMLSKTCha != Guid.Empty) ? null : mlc.SL;
                                mlCha.sK = mlc.SK;
                                mlCha.sM = mlc.SM;
                                mlCha.sNG = mlc.SNg;
                                mlCha.sMoTa = mlc.SMoTa;
                                mlCha.sNgCha = mlc.SNGCha;
                                mlCha.BHangCha = mlc.BHangCha;
                                listDataDonVi.Add(mlCha);
                            }
                        }

                        listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();

                        CalculateData(listDataDonVi);
                        listDataDonVi = listDataDonVi.Where(item => Math.Abs(item.MuaHangHienVat) >= double.Epsilon ||
                            Math.Abs(item.DacThu) >= double.Epsilon || Math.Abs(item.PhanCap) >= double.Epsilon
                            || Math.Abs(item.HuyDongTonKho) >= double.Epsilon
                            || Math.Abs(item.ThongBaoDV) >= double.Epsilon).OrderBy(t => t.SSTTBC).ToList();

                        var sumTuChi = listDataDonVi.Where(item => (item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null)).Sum(item => item.TuChi);
                        var sumMuaHangHienVat = listDataDonVi.Where(item => (item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null)).Sum(item => item.MuaHangHienVat);
                        var sumDacThu = listDataDonVi.Where(item => (item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null)).Sum(item => item.PhanCap);
                        var sumThongBaoDv = listDataDonVi.Where(item => (item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null)).Sum(item => item.ThongBaoDV);
                        var sumHuyDongTonKho = listDataDonVi.Where(item => (item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null)).Sum(item => item.HuyDongTonKho);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listDataDonVi);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", (_diaDiem != null ? _diaDiem : "") + " ," + DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", dv.NameItem);
                        data.Add("Cap1", GetDonViBanHanh(1, _dmChuKy.LoaiDVBanHanh1, itemDanhMuc, dv.NameItem));
                        data.Add("Cap2", GetDonViBanHanh(2, _dmChuKy.LoaiDVBanHanh2, itemDanhMuc, dv.NameItem));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("h2", "");
                        data.Add("SumTuChi", sumTuChi);
                        data.Add("SumMuaHangHienVat", sumMuaHangHienVat);
                        data.Add("SumDacThu", sumDacThu);
                        data.Add("SumThongBaoDv", sumThongBaoDv);
                        data.Add("SumHuyDongTonKho", sumHuyDongTonKho);
                        if (_paperPrintTypeSelected.ValueItem == "2")
                        {
                            data.Add("IsPhuLuc", true);
                            data.Add("IsTrinhKy", false);
                        }
                        else
                        {
                            data.Add("IsPhuLuc", false);
                            data.Add("IsTrinhKy", true);
                        }

                        AddChuKy(data, _typeChuky);
                        string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_CHITIET_PHANBO_SOKIEMTRA_NSBD));
                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + dv.NameItem;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportChiTietPhanBoKiemTraNsbdQuery>(templateFileName, data);
                        results.Add(new ExportResult("Báo cáo chi tiết phân bổ số kiểm tra theo ngành " + dv.NameItem, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportPhanBoTheoNganhPhuLuc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DISTRIBUTION;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                    //var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        //lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                        var lstNNganhSelected = ListNNganh.Where(item => item.IsChecked).ToList();
                        foreach (var nnganh in lstNNganhSelected)
                        {
                            if (!StringUtils.IsNullOrEmpty(nnganh.ValueItem))
                            {
                                listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(nnganh.ValueItem, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh, false)
                                    .ToList();
                                var listDonViAll = _nsDonViService.FindByNamLamViec(yearOfWork);
                                if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                                {
                                    listDonViAll = listDonViAll.Where(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem)).ToList();
                                }
                                var listDataDonVi1 = from donVi1 in listDataDonVi
                                                     join donVi2 in listDonViAll
                                           on donVi1.IdDonVi equals donVi2.IIDMaDonVi
                                                     select donVi1;
                                listDataDonVi = _mapper.Map<List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>>(listDataDonVi1);
                                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                            }
                            tenNganh = nnganh.NameItem;

                            var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                            var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).Where(n => Convert.ToInt32(n.Loai) == 1).ToList();
                            //if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                            //{
                            //    listDonVi = listDonVi.Where(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem)).ToList();
                            //}
                            var listIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();
                            List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                            foreach (var mlc in sktMucLucs)
                            {
                                if (!listIdMucLuc.Contains(mlc.SKyHieu))
                                {
                                    foreach (var dv in listDonVi.OrderBy(x => x.IIDMaDonVi))
                                    {
                                        ReportPhanBoKiemTraTheoNganhPhuLucQuery mlCha = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                        mlCha.BHangCha = mlc.BHangCha;
                                        mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                                        mlCha.IIdMlskt = mlc.IIDMLSKT;
                                        mlCha.IdDonVi = dv.IIDMaDonVi;
                                        mlCha.sNG = mlc.SNg;
                                        mlCha.sMoTa = mlc.SMoTa;
                                        mlCha.STT = mlc.SSTT;
                                        mlCha.SSTTBC = mlc.SSttBC;
                                        mlCha.sKyHieu = mlc.SKyHieu;
                                        listDataDonVi.Add(mlCha);
                                    }
                                }
                            }
                            CalculateData(listDataDonVi);
                            listIdMucLuc = listDataDonVi.OrderBy(x => x.SSTTBC).Select(x => x.sKyHieu).Distinct().ToList();
                            listDonVi = listDonVi.OrderBy(x => x.IIDMaDonVi).ToList();
                            var listDonViSplits = SplitList(listDonVi, 6).ToList();
                            string path = "";
                            for (int i = 0; i < listDonViSplits.Count; i++)
                            {
                                if (listDonViSplits[i].Count < 6)
                                {
                                    var coutRowEmpty = 6 - listDonViSplits[i].Count;
                                    for (int j = 0; j < coutRowEmpty; j++)
                                    {
                                        DonVi empty = new DonVi();
                                        listDonViSplits[i].Add(empty);
                                    }
                                }

                                List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                                foreach (var ml in listIdMucLuc)
                                {
                                    var mucLuc = _iSktMucLucService.FindByCondition(x => x.SKyHieu == ml && x.INamLamViec == yearOfWork).FirstOrDefault();
                                    if (mucLuc != null)
                                    {
                                        ReportPhanBoKiemTraTheoNganhPhuLucQuery kq = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                        kq.IIdMlskt = mucLuc.IIDMLSKT;
                                        kq.STT = mucLuc.SSTT;
                                        kq.sMoTa = mucLuc.SMoTa;
                                        kq.sNG = mucLuc.SNg;
                                        kq.BHangCha = mucLuc.BHangCha;
                                        kq.TongCong = listDataDonVi.Where(x => x.sKyHieu == ml).Sum(x => x.TuChi);
                                        kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                        foreach (var dv in listDonViSplits[i])
                                        {
                                            NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                            if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                            {
                                                var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.sKyHieu == ml);
                                                giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            else
                                            {
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        results.Add(kq);
                                    }
                                }

                                List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                                ReportPhanBoKiemTraTheoNganhPhuLucQuery total = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        var gtDonVi = listDataDonVi.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlsktCha.IsNullOrEmpty()).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    else
                                    {
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                }
                                var prefixTenNganh = "Ngành";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành";
                                }
                                var tongCong = listDataDonVi.Where(x => x.IIdMlsktCha.IsNullOrEmpty()).Sum(x => x.TuChi);
                                resultsTotal.Add(total);
                                var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + 3);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", results);
                                data.Add("ListDataTotal", resultsTotal);
                                data.Add("TongCong", tongCong);
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("h1", h1);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                                data.Add("h2", "");
                                //data.Add("TenNganh", nnganh.NameItem);
                                data.Add("TenNganh", prefixTenNganh + ": " + tenNganh);
                                data.Add("Headers", listDonViSplits[i]);
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                                AddChuKy(data, _typeChuky);

                                if (i == 0)
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO1));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + nnganh.NameItem;
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO2));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + nnganh.NameItem + (i + 1);
                                }
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành_" + nnganh.NameItem + "- Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }

                            // Không có dữ liệu
                            if (listDonVi.Count <= 0)
                            {
                                var nameColunmMerge = GetExcelColumnName(8);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                                data.Add("h1", h1);
                                data.Add("h2", _sessionInfo.TenDonVi);
                                data.Add("TenNganh", tenNganh);
                                data.Add("Headers", new List<DonVi>());
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                                templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY);
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }

                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportSoNhuCauTheoNganhPhuLuc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                    var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        //lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                        //var lstChuyenNganh = ListChuyenNganh.Where(x => lstIdCodeSelected.Contains(x.ValueItem)).ToList();
                        //tenNganh = string.Join(", ", lstChuyenNganh.Select(x => x.NameItem));

                        var lstNNganhSelected = ListNNganh.Where(item => item.IsChecked).ToList();
                        foreach (var nnganh in lstNNganhSelected)
                        {
                            var lstChungTuSnc = string.Join(",", ListIdChungTuBaoCaoSncNganh);
                            //if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                            //{
                            //    listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTheoNganhPhuLuc(lstIdCodeSelected, null, lstChungTuSnc, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                            //        .ToList();
                            //    listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                            //}
                            if (!StringUtils.IsNullOrEmpty(nnganh.ValueItem))
                            {
                                listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTheoNganhPhuLuc(nnganh.ValueItem, null, lstChungTuSnc, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                                    .ToList();
                                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                                if (int.Parse(_catUnitTypeSelected.ValueItem) != 1)
                                {
                                    foreach (var prop in typeof(ReportPhanBoKiemTraTheoNganhPhuLucQuery).GetProperties())
                                    {
                                        if (prop.PropertyType.Name == "Double")
                                        {
                                            listDataDonVi.ForEach(x => prop.SetValue(x, Math.Round(Convert.ToDouble(prop.GetValue(x)))));
                                        }
                                    }
                                }
                            }
                            tenNganh = nnganh.NameItem;

                            var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                            var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                            var listIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();
                            List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                            foreach (var mlc in sktMucLucs)
                            {
                                if (!listIdMucLuc.Contains(mlc.SKyHieu))
                                {
                                    foreach (var dv in listDonVi)
                                    {
                                        ReportPhanBoKiemTraTheoNganhPhuLucQuery mlCha = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                        mlCha.BHangCha = mlc.BHangCha;
                                        mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                                        mlCha.IIdMlskt = mlc.IIDMLSKT;
                                        mlCha.IdDonVi = dv.IIDMaDonVi;
                                        mlCha.sNG = mlc.SNg;
                                        mlCha.sMoTa = mlc.SMoTa;
                                        mlCha.STT = mlc.SSTT;
                                        mlCha.SSTTBC = mlc.SSttBC;
                                        mlCha.sKyHieu = mlc.SKyHieu;
                                        listDataDonVi.Add(mlCha);
                                    }
                                }
                            }
                            CalculateData(listDataDonVi);
                            listIdMucLuc = listDataDonVi.OrderBy(x => x.SSTTBC).Select(x => x.sKyHieu).Distinct().ToList();
                            var listDonViSplits = SplitList(listDonVi, 5, 6).ToList();
                            string path = "";
                            for (int i = 0; i < listDonViSplits.Count; i++)
                            {
                                if (listDonViSplits[i].Count < 6)
                                {
                                    var coutRowEmpty = 6 - listDonViSplits[i].Count;
                                    for (int j = 0; j < coutRowEmpty; j++)
                                    {
                                        DonVi empty = new DonVi();
                                        listDonViSplits[i].Add(empty);
                                    }
                                }

                                List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                                foreach (var ml in listIdMucLuc)
                                {
                                    var mucLuc = _iSktMucLucService.FindByCondition(x => x.SKyHieu == ml && x.INamLamViec == yearOfWork).FirstOrDefault();
                                    if (mucLuc != null)
                                    {
                                        ReportPhanBoKiemTraTheoNganhPhuLucQuery kq = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                        kq.IIdMlskt = mucLuc.IIDMLSKT;
                                        kq.STT = mucLuc.SSTT;
                                        kq.sMoTa = mucLuc.SMoTa;
                                        kq.sNG = mucLuc.SNg;
                                        kq.BHangCha = mucLuc.BHangCha;
                                        kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                        kq.TongCong = listDataDonVi.Where(x => x.sKyHieu == ml).Sum(x => x.TuChi);
                                        foreach (var dv in listDonViSplits[i])
                                        {
                                            NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                            if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                            {
                                                var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.sKyHieu == ml);
                                                giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                                kq.LstGiaTri.Add(giaTri);
                                            }
                                            else
                                            {
                                                kq.LstGiaTri.Add(giaTri);
                                            }

                                        }
                                        results.Add(kq);
                                    }
                                }

                                List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                                ReportPhanBoKiemTraTheoNganhPhuLucQuery total = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        var gtDonVi = listDataDonVi.Where(x => x.IdDonVi == dv.IIDMaDonVi && !(bool)x.BHangCha).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    else
                                    {
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                }
                                resultsTotal.Add(total);
                                var prefixTenNganh = "Ngành: ";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành: ";
                                }
                                var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + 3);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", results);
                                data.Add("ListDataTotal", resultsTotal);
                                data.Add("TongCong", results.Where(x => !x.BHangCha.GetValueOrDefault()).Sum(x => x.TongCong));
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("h1", h1);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                                data.Add("h2", "");
                                //data.Add("TenNganh", tenNganh);
                                data.Add("TenNganh", prefixTenNganh + tenNganh);
                                data.Add("Headers", listDonViSplits[i]);
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                                AddChuKy(data, _typeChuky);

                                if (i == 0)
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO1));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO2));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                                }
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                //exportResults.Add(new ExportResult("Tổng hợp số nhu cầu theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }

                            // Không có dữ liệu
                            if (listDonVi.Count <= 0)
                            {
                                var nameColunmMerge = GetExcelColumnName(8);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                                data.Add("h1", h1);
                                data.Add("h2", _sessionInfo.TenDonVi);
                                data.Add("TenNganh", tenNganh);
                                data.Add("Headers", new List<DonVi>());
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                exportResults.Add(new ExportResult("Tổng hợp số nhu cầu theo ngành", fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportNhanSoKiemTraTheoNganhPhuLuc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.CHECK;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = "";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                    var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    }
                    if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    {
                        listDataDonVi = _sktChungTuChiTietService.FindReportNhanSoKiemTraTheoNganhPhuLuc(lstIdCodeSelected, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                            .ToList();
                        listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                    }

                    var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                    var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                    var listIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                    foreach (var mlc in sktMucLucs)
                    {
                        if (!listIdMucLuc.Contains(mlc.SKyHieu))
                        {
                            foreach (var dv in listDonVi)
                            {
                                ReportPhanBoKiemTraTheoNganhPhuLucQuery mlCha = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                mlCha.BHangCha = mlc.BHangCha;
                                mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                                mlCha.IIdMlskt = mlc.IIDMLSKT;
                                mlCha.IdDonVi = dv.IIDMaDonVi;
                                mlCha.sNG = mlc.SNg;
                                mlCha.sMoTa = mlc.SMoTa;
                                mlCha.STT = mlc.SSTT;
                                mlCha.SSTTBC = mlc.SSttBC;
                                mlCha.sKyHieu = mlc.SKyHieu;
                                listDataDonVi.Add(mlCha);
                            }
                        }
                    }
                    CalculateData(listDataDonVi);
                    listIdMucLuc = listDataDonVi.OrderBy(x => x.SSTTBC).Select(x => x.sKyHieu).Distinct().ToList();
                    var listDonViSplits = SplitList(listDonVi, 6).ToList();
                    string path = "";
                    for (int i = 0; i < listDonViSplits.Count; i++)
                    {
                        //if (listDonViSplits[i].Count < 6)
                        //{
                        //    var coutRowEmpty = 6 - listDonViSplits[i].Count;
                        //    for (int j = 0; j < coutRowEmpty; j++)
                        //    {
                        //        DonVi empty = new DonVi();
                        //        listDonViSplits[i].Add(empty);
                        //    }
                        //}

                        List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        foreach (var ml in listIdMucLuc)
                        {
                            var mucLuc = _iSktMucLucService.FindByCondition(x => x.SKyHieu == ml && x.INamLamViec == yearOfWork).FirstOrDefault();
                            if (mucLuc != null)
                            {
                                ReportPhanBoKiemTraTheoNganhPhuLucQuery kq = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                                kq.IIdMlskt = mucLuc.IIDMLSKT;
                                kq.STT = mucLuc.SSTT;
                                kq.sMoTa = mucLuc.SMoTa;
                                kq.sNG = mucLuc.SNg;
                                kq.sKyHieu = mucLuc.SKyHieu;
                                kq.BHangCha = mucLuc.BHangCha;
                                kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                foreach (var dv in listDonViSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                                    {
                                        var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.sKyHieu == ml);
                                        giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                    else
                                    {
                                        kq.LstGiaTri.Add(giaTri);
                                    }

                                }
                                results.Add(kq);
                            }
                        }

                        List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        ReportPhanBoKiemTraTheoNganhPhuLucQuery total = new ReportPhanBoKiemTraTheoNganhPhuLucQuery();
                        total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                        foreach (var dv in listDonViSplits[i])
                        {
                            NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                            if (!string.IsNullOrEmpty(dv.IIDMaDonVi))
                            {
                                var gtDonVi = listDataDonVi.Where(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlsktCha.IsNullOrEmpty()).Sum(x => x.TuChi);
                                giaTri.FTuChi = gtDonVi;
                                total.LstGiaTriTotal.Add(giaTri);
                            }
                            else
                            {
                                total.LstGiaTriTotal.Add(giaTri);
                            }
                        }
                        resultsTotal.Add(total);
                        var nameColunmMerge = GetExcelColumnName(listDonViSplits[i].Count() + 3);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", results.OrderBy(x => x.sKyHieu));
                        data.Add("ListDataTotal", resultsTotal);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", "");
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                        data.Add("h2", "");
                        data.Add("TenNganh", tenNganh);
                        data.Add("Headers", listDonViSplits[i]);
                        data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                        data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                        data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                        data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                        data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                        AddChuKy(data, _typeChuky);
                        templateFileName = "";
                        fileNamePrefix = "";
                        if (i == 0)
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_NHAN_SOKIEMTRA_THEONGANH_PHULUC));
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        }
                        //else
                        //{
                        //    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_TO2);
                        //    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                        //}
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                        exportResults.Add(new ExportResult("Nhận số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                    }

                    // Không có dữ liệu
                    if (listDonVi.Count <= 0)
                    {
                        var nameColunmMerge = GetExcelColumnName(8);
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                        data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("DonVi", "");
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                        data.Add("h1", h1);
                        data.Add("h2", _sessionInfo.TenDonVi);
                        data.Add("TenNganh", tenNganh);
                        data.Add("Headers", new List<DonVi>());
                        data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                        data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                        data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                        data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                        data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY);
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                        exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportPhanBoTheoNganhPhuLucDonViDoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DISTRIBUTION;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    //var lstIdCodeSelected = "";

                    var listSpecializedChecked = ListNNganh.Where(x => x.IsChecked).ToList();

                    foreach (var item in listSpecializedChecked.Select((value, i) => new { i, value }))
                    {
                        List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(item.value.ValueItem, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh, false).ToList();

                        var listDonViAll = _nsDonViService.FindByNamLamViec(yearOfWork);
                        if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                        {
                            listDonViAll = listDonViAll.Where(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem)).ToList();
                        }
                        var listDataDonVi1 = from donVi1 in listDataDonVi
                                             join donVi2 in listDonViAll
                                   on donVi1.IdDonVi equals donVi2.IIDMaDonVi
                                             select donVi1;
                        listDataDonVi = _mapper.Map<List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>>(listDataDonVi1);
                        listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();




                        var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).Where(n => Convert.ToInt32(n.Loai) == 1).OrderBy(n => n.IIDMaDonVi).ToList();

                        var listIdMucLuc = listDataDonVi.Select(x => x.IIdMlskt).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = _iSktMucLucService
                            .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).OrderBy(x => x.SNg).ToList();
                        List<NsSktMucLuc> lstMlResult = new List<NsSktMucLuc>();
                        List<string> lstNg = new List<string>();
                        foreach (var mucluc in sktMucLucs)
                        {
                            if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.SNg))
                            {
                                NsSktMucLuc mlTong = new NsSktMucLuc();
                                mlTong.SNg = mucluc.SNg;
                                mlTong.SMoTa = "(+)";
                                lstMlResult.Add(mlTong);
                                lstNg.Add(mucluc.SNg);
                            }
                            lstMlResult.Add(mucluc);
                        }

                        var groupNg = lstMlResult.GroupBy(x => x.SNg, (key, value) => new { key, value = value.ToList() }).ToList();
                        foreach (var gr in groupNg)
                        {
                            var count = gr.value.Count();
                            for (int i = 0; i < 6 - count % 6; i++)
                            {
                                NsSktMucLuc empty = new NsSktMucLuc();
                                empty.SNg = gr.key;
                                empty.INamLamViec = _sessionService.Current.YearOfWork;
                                gr.value.Add(empty);
                            }
                        }

                        //lstMlResult = groupNg.SelectMany(x => x.value).ToList();
                        var listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                        for (int i = 0; i < listMucLucSplits.Count; i++)
                        {
                            List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel> headers = new List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>();
                            var lstNganhHeader = listMucLucSplits[i].Select(x => x.SNg).Distinct().ToList();
                            //var tenNganh = ListChuyenNganh.FirstOrDefault(x => lstNganhHeader != null && x.ValueItem.Contains(lstNganhHeader.FirstOrDefault())).NameItem;
                            var tenNganh = item.value.NameItem;
                            int columnStart = 5;
                            foreach (var nganhHeader in lstNganhHeader)
                            {
                                var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                                var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.SNg)).Select(x => x.SMoTa).ToList();
                                var mergeRange = "";
                                var columnStartName = GetExcelColumnName(columnStart);
                                var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                mergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                                //columnStart += 1;

                                HeaderReportPhanBoKiemTraTheoNganhDonViDocModel hd = new HeaderReportPhanBoKiemTraTheoNganhDonViDocModel();
                                hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                hd.LstMucLuc = new List<HeaderDetail>();
                                hd.LstNganhHeader = new List<HeaderDetail>();
                                hd.MergeRange = mergeRange;
                                int j = 0;
                                foreach (var mlHeader in lstMlHeader)
                                {
                                    HeaderDetail mlhd = new HeaderDetail();
                                    if (j == 0)
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = nganh != null ? nganh.NameItem : "";
                                        mlhd.SSttBC = "1";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    else
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = "";
                                        mlhd.SSttBC = "2";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    mlhd = new HeaderDetail();
                                    mlhd.MoTa = mlHeader;
                                    hd.LstMucLuc.Add(mlhd);
                                    j++;
                                }
                                headers.Add(hd);
                            }
                            int stt = 1;
                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            foreach (var dv in listDonVi)
                            {
                                ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel kq = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                                kq.Stt = stt++;
                                kq.MaDonVi = dv.IIDMaDonVi;
                                kq.TenDonVi = dv.TenDonVi;
                                kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                foreach (var ml in listMucLucSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (ml.INamLamViec.Equals(0))
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.Where(x => ml.SNg.Contains(x.sNG) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                    else
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlskt == ml.IIDMLSKT);
                                        giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                }
                                results.Add(kq);
                            }
                            // results = results.OrderBy(n => n.MaDonVi).ToList();

                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel total = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                            total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                            foreach (var dv in listMucLucSplits[i])
                            {
                                NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                if (dv.INamLamViec.Equals(0))
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => dv.SNg.Contains(x.sNG)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                                else
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => x.IIdMlskt.Equals(dv.IIDMLSKT)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);
                            var sumTotal = listDataDonVi.Sum(x => x.TuChi);
                            var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                            var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var prefixTenNganh = "Ngành";
                            if (IsChuyenNganh)
                            {
                                prefixTenNganh = "Chuyên ngành";
                            }
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", results);
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("SumTotal", sumTotal);
                            data.Add("TongSoTien", sumTotal * donViTinh);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            //data.Add("TenNganh", tenNganh);
                            data.Add("TenNganh", prefixTenNganh + ": " + tenNganh);
                            data.Add("Headers", headers);
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                            AddChuKy(data, _typeChuky);

                            if (i == 0)
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO1));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            }
                            else
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO2));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                            }
                            //string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix + "_" + item.ValueItem);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel, HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>(templateFileName, data);
                            if (IsChuyenNganh)
                            {
                                xlsFile.SetColHidden(4, true);
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                            else
                            {
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                        }

                        // Không có dữ liệu
                        if (listDonVi.Count <= 0)
                        {
                            var nameColunmMerge = GetExcelColumnName(8);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                            data.Add("h1", h1);
                            data.Add("h2", "");
                            data.Add("TenNganh", "");
                            data.Add("Headers", new List<DonVi>());
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix + "_" + item.value.ValueItem);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                            exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;



                    //if (ListNNganh != null)
                    //{
                    //    lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    //}



                    //if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    //{
                    //    listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(lstIdCodeSelected, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                    //        .ToList();
                    //    listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                    //}


                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnExportCompareTongHop(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var suffix = _catUnitTypeSelected.ValueItem == "1" ? true : false;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var yearOfWork = _sessionService.Current.YearOfWork;
                    var yearOfBudget = _sessionService.Current.YearOfBudget;
                    var loaiNguonNganSach = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNguonNganSach != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                    var budgetSource = _sessionService.Current.Budget;
                    var currentIdDonVi = _sessionService.Current.IdDonVi;
                    var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
                    List<Guid> lstSKTCt = new List<Guid>();

                    SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
                    searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                    searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
                    searchCondition.NguonNganSach = _sessionInfo.Budget;
                    searchCondition.ITrangThai = StatusType.ACTIVE;
                    searchCondition.ILoai = DemandCheckType.DISTRIBUTION;
                    searchCondition.IdDonVi = string.Join(StringUtils.COMMA, ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem));
                    searchCondition.LoaiChungTu = loaiChungTu;
                    searchCondition.KieuBaoCao = 2;
                    searchCondition.UserName = _sessionInfo.Principal;
                    searchCondition.DonViTinh = donViTinh;
                    if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                    {
                        searchCondition.LoaiBaoCao = 1;
                    }
                    else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                    {
                        searchCondition.LoaiBaoCao = 2;
                    }
                    else
                    {
                        searchCondition.LoaiBaoCao = 3;
                    }
                    searchCondition.ILoaiNguonNganSach = int.Parse(BudgetSourceTypeSelected.ValueItem);
                    searchCondition.lstSktChungTuId = lstSKTCt;
                    var listData = _sktChungTuChiTietService.FindReportSoSanhSKT(searchCondition).ToList();
                    CalculateData(listData);

                    listData = listData.Where(item => Math.Abs(item.SoLieuCot1) >= Double.Epsilon
                                                      || Math.Abs(item.SoLieuCot2) >= Double.Epsilon
                                                      || Math.Abs(item.ChenhLech) >= Double.Epsilon).ToList();
                    int iNumber = 0;
                    for (int i = 0; i < listData.Count; i++)
                    {
                        if (listData[i].MoTa.IndexOf("   +") >= 0)
                        {
                            iNumber++;
                            listData[i].IIdMlskt = Guid.NewGuid();
                            listData[i].IIdParentMlskt = listData[i - iNumber].IIdMlskt;
                            listData[i].IsMaDonVi = true;
                        }
                        else
                        {
                            iNumber = 0;
                        }
                    }

                    if (VoucherTypeSelected?.ValueItem == "1")
                    {
                        if (PaperPrintTypeSelected?.ValueItem == "1")
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD;
                        }
                        else
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD_TONGHOP;
                        }

                    }
                    else
                    {
                        if (PaperPrintTypeSelected?.ValueItem == "1")
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN;
                        }
                        else
                        {
                            _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN_TONGHOP;
                        }
                    }

                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);
                    FormatDisplay(listData);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst + " " + _sessionInfo.YearOfWork);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : (itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""));
                    data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : "");
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("Count", 10000);
                    data.Add("HasChiTiet", false);
                    data.Add("Donvi", string.Empty);
                    var tongSoLieuCot1 = listData.Sum(x => x.SoLieuCot1);
                    var tongSoLieuCot2 = listData.Sum(x => x.SoLieuCot2);
                    var tongSoLieuCot1BangChu = listData.Where(x => !x.BHangCha && x.IsMaDonVi).Sum(x => x.SoLieuCot1);
                    var tongSoLieuCot2BangChu = listData.Where(x => !x.BHangCha && x.IsMaDonVi).Sum(x => x.SoLieuCot2);
                    data.Add("TongSoLieuCot1", tongSoLieuCot1BangChu);
                    data.Add("TongSoLieuCot2", tongSoLieuCot2BangChu);
                    data.Add("TongChenhLech", tongSoLieuCot2BangChu - tongSoLieuCot1BangChu);
                    data.Add("Tien", StringUtils.NumberToText((double)tongSoLieuCot2BangChu * donViTinh, true));

                    if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                    {
                        data.Add("TieuDeCot1", "Số kiểm tra năm trước");
                        data.Add("TieuDeCot2", "Số kiểm tra năm nay");
                    }
                    else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                    {
                        data.Add("TieuDeCot1", "Mua hàng hiện vật năm trước");
                        data.Add("TieuDeCot2", "Mua hàng hiện vật năm nay");
                    }
                    else
                    {
                        data.Add("TieuDeCot1", "Đặc thù năm trước");
                        data.Add("TieuDeCot2", "Đặc thù năm nay");
                    }

                    string templateFileName;
                    if (exportType == ExportType.EXCEL)
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY_TONGHOP_EXCEL);
                    else
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY);

                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<SktChungTuChiTietQuery>(templateFileName, data);
                    e.Result = new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile);

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level)
        {
            if (dmChuKy == null) return string.Empty;
            var loaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        public void OnExportCompare(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();

                    ListDonVi.Where(item => item.IsChecked).ForAll(item =>
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                        var suffix = _catUnitTypeSelected.ValueItem == "1" ? true : false;
                        var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                        var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                        var yearOfWork = _sessionService.Current.YearOfWork;
                        var yearOfBudget = _sessionService.Current.YearOfBudget;
                        var loaiNguonNganSach = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                        if (loaiNguonNganSach != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                        var budgetSource = _sessionService.Current.Budget;
                        var currentIdDonVi = _sessionService.Current.IdDonVi;
                        var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
                        List<Guid> lstSKTCt = new List<Guid>();

                        SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
                        searchCondition.NguonNganSach = _sessionInfo.Budget;
                        searchCondition.ITrangThai = StatusType.ACTIVE;
                        searchCondition.ILoai = DemandCheckType.DISTRIBUTION;
                        searchCondition.IdDonVi = item.ValueItem;
                        searchCondition.LoaiChungTu = loaiChungTu;
                        searchCondition.UserName = _sessionInfo.Principal;
                        searchCondition.DonViTinh = donViTinh;
                        if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                        {
                            searchCondition.LoaiBaoCao = 1;
                        }
                        else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                        {
                            searchCondition.LoaiBaoCao = 2;
                        }
                        else
                        {
                            searchCondition.LoaiBaoCao = 3;
                        }
                        searchCondition.ILoaiNguonNganSach = int.Parse(BudgetSourceTypeSelected.ValueItem);
                        searchCondition.lstSktChungTuId = lstSKTCt;
                        var listData = _sktChungTuChiTietService.FindReportSoSanhSKT(searchCondition).ToList();
                        CalculateData(listData);

                        listData = listData.Where(item => Math.Abs(item.SoLieuCot1) >= Double.Epsilon
                                                          || Math.Abs(item.SoLieuCot2) >= Double.Epsilon
                                                          || Math.Abs(item.ChenhLech) >= Double.Epsilon).ToList();

                        if (VoucherTypeSelected?.ValueItem == "1")
                        {
                            if (PaperPrintTypeSelected?.ValueItem == "1")
                            {
                                _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD;
                            }
                            else
                            {
                                _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSSD_TONGHOP;
                            }

                        }
                        else
                        {
                            if (PaperPrintTypeSelected?.ValueItem == "1")
                            {
                                _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN;
                            }
                            else
                            {
                                _typeChuky = TypeChuKy.RPT_NS_SKT_NHANSOKIEMTRA_NSDTN_TONGHOP;
                            }
                        }
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        string sCap1 = GetLevelTitle(_dmChuKy, 1);
                        string sCap2 = GetLevelTitle(_dmChuKy, 2);
                        FormatDisplay(listData);
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst + " " + _sessionInfo.YearOfWork);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : (itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""));
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : _sessionInfo.TenDonVi);
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : "");
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : "");
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : "");
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", _sessionInfo.TenDonVi);
                        data.Add("HasChiTiet", true);
                        data.Add("Donvi", item.NameItem);
                        data.Add("Count", 10000);
                        var tongSoLieuCot1 = listData.Sum(x => x.SoLieuCot1);
                        var tongSoLieuCot2 = listData.Sum(x => x.SoLieuCot2);
                        var tongSoLieuCot1BangChu = listData.Where(x => !x.BHangCha).Sum(x => x.SoLieuCot1);
                        var tongSoLieuCot2BangChu = listData.Where(x => !x.BHangCha).Sum(x => x.SoLieuCot2);
                        data.Add("TongSoLieuCot1", tongSoLieuCot1BangChu);
                        data.Add("TongSoLieuCot2", tongSoLieuCot2BangChu);
                        data.Add("TongChenhLech", tongSoLieuCot2BangChu - tongSoLieuCot1BangChu);
                        data.Add("Tien", StringUtils.NumberToText((double)tongSoLieuCot2BangChu * donViTinh, true));

                        if (VoucherType.NSSD_Key.Equals(VoucherTypeSelected.ValueItem))
                        {
                            data.Add("TieuDeCot1", "Số kiểm tra năm trước");
                            data.Add("TieuDeCot2", "Số kiểm tra năm nay");
                        }
                        else if (LoaiNSBD.Equals(LoaiNSBD.MHHV))
                        {
                            data.Add("TieuDeCot1", "Mua hàng hiện vật năm trước");
                            data.Add("TieuDeCot2", "Mua hàng hiện vật năm nay");
                        }
                        else
                        {
                            data.Add("TieuDeCot1", "Đặc thù năm trước");
                            data.Add("TieuDeCot2", "Đặc thù năm nay");
                        }

                        string templateFileName;
                        if (exportType == ExportType.EXCEL)
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY_EXCEL);
                        else
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SKT_SO_SANH_NAM_TRUOC_NAM_NAY);

                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<SktChungTuChiTietQuery>(templateFileName, data);
                        results.Add(new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                    });

                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportPhuongAnPhanBoSKT02A(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    CurrencyToText currencyToText = new CurrencyToText();
                    var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var listSpecializedChecked = ListNNganh.Where(x => x.IsChecked).ToList();
                    var iKhoi = int.Parse(_khoiSelected.ValueItem);
                    var loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    var lstMajors = string.Join(",", listSpecializedChecked.Select(x => x.ValueItem).Distinct().ToList());
                    var totalResult = _sktChungTuChiTietService.PrintReportPhuongAnPhanBoSKT02A(lstMajors, yearOfWork, yearOfBudget, budgetSource, iKhoi, donViTinh, false, loaiNNS).Where(x => x.TuChi != 0).ToList();
                    foreach (var item in listSpecializedChecked.Select((value, i) => new { i, value }))
                    {
                        List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        listDataDonVi = _sktChungTuChiTietService.PrintReportPhuongAnPhanBoSKT02A(item.value.ValueItem, yearOfWork, yearOfBudget, budgetSource, iKhoi, donViTinh, false, loaiNNS).Where(x => x.TuChi != 0).ToList();
                        var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).Where(n => Convert.ToInt32(n.Loai) == 1).OrderBy(n => n.IIDMaDonVi).ToList();
                        var listIdMucLuc = listDataDonVi.Select(x => x.IIdMlskt).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = _iSktMucLucService
                            .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).OrderBy(x => x.SNg).ToList();
                        List<NsSktMucLuc> lstMlResult = new List<NsSktMucLuc>();
                        List<string> lstNg = new List<string>();
                        foreach (var mucluc in sktMucLucs)
                        {
                            if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.SNg))
                            {
                                NsSktMucLuc mlTong = new NsSktMucLuc();
                                mlTong.SNg = mucluc.SNg;
                                mlTong.SMoTa = "(+)";
                                lstMlResult.Add(mlTong);
                                lstNg.Add(mucluc.SNg);
                            }
                            lstMlResult.Add(mucluc);
                        }

                        var groupNg = lstMlResult.GroupBy(x => x.SNg, (key, value) => new { key, value = value.ToList() }).ToList();
                        foreach (var gr in groupNg)
                        {
                            var count = gr.value.Count();
                            for (int i = 0; i < 6 - count % 6; i++)
                            {
                                NsSktMucLuc empty = new NsSktMucLuc();
                                empty.SNg = gr.key;
                                empty.INamLamViec = _sessionService.Current.YearOfWork;
                                gr.value.Add(empty);
                            }
                        }

                        var listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                        for (int i = 0; i < listMucLucSplits.Count; i++)
                        {
                            List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel> headers = new List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>();
                            var lstNganhHeader = listMucLucSplits[i].Select(x => x.SNg).Distinct().ToList();
                            var tenNganh = item.value.NameItem;
                            int columnStart = 5;
                            int sttMLNS = 1;
                            foreach (var nganhHeader in lstNganhHeader)
                            {
                                var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                                var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.SNg)).Select(x => x.SMoTa).ToList();
                                var mergeRange = "";
                                var columnStartName = GetExcelColumnName(columnStart);
                                var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                mergeRange = columnStartName + "9" + ":" + columnEndName + "9";
                                HeaderReportPhanBoKiemTraTheoNganhDonViDocModel hd = new HeaderReportPhanBoKiemTraTheoNganhDonViDocModel();
                                hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                hd.LstMucLuc = new List<HeaderDetail>();
                                hd.LstNganhHeader = new List<HeaderDetail>();
                                hd.MergeRange = mergeRange;
                                int j = 0;

                                foreach (var mlHeader in lstMlHeader)
                                {
                                    HeaderDetail mlhd = new HeaderDetail();
                                    sttMLNS++;
                                    if (j == 0)
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = nganh != null ? nganh.NameItem : "";
                                        mlhd.SSttBC = "1";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    else
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = "";
                                        mlhd.SSttBC = "2";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    mlhd = new HeaderDetail();
                                    mlhd.MoTa = mlHeader;
                                    if (i == 0)
                                        mlhd.Stt = sttMLNS;
                                    else
                                        mlhd.Stt = sttMLNS + i * 6;
                                    hd.LstMucLuc.Add(mlhd);
                                    j++;
                                }
                                headers.Add(hd);
                            }
                            int stt = 1;
                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            foreach (var dv in listDonVi)
                            {
                                ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel kq = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                                kq.Stt = stt++;
                                kq.MaDonVi = dv.IIDMaDonVi;
                                kq.TenDonVi = dv.TenDonVi;
                                kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                kq.TongCongChuyenNganh += kq.TongCong;
                                kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                foreach (var ml in listMucLucSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (ml.INamLamViec.Equals(0))
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.Where(x => ml.SNg.Contains(x.sNG) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                    else
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlskt == ml.IIDMLSKT);
                                        giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                }
                                results.Add(kq);
                            }

                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel total = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                            total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                            foreach (var dv in listMucLucSplits[i])
                            {
                                NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                if (dv.INamLamViec.Equals(0))
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => dv.SNg.Contains(x.sNG)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                                else
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => x.IIdMlskt.Equals(dv.IIDMLSKT)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);
                            var sumTotal = listDataDonVi.Sum(x => x.TuChi);
                            var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                            var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var prefixTenNganh = "Ngành";
                            if (IsChuyenNganh)
                            {
                                prefixTenNganh = "Chuyên ngành";
                            }
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("CurrencyToText", currencyToText);
                            data.Add("ListData", results);
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("SumTotal", sumTotal);
                            data.Add("TongSoTien", sumTotal * donViTinh);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond + " NĂM " + yearOfWork);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            //data.Add("TenNganh", tenNganh);
                            data.Add("TenNganh", prefixTenNganh + ": " + tenNganh);
                            data.Add("Headers", headers);
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                            AddChuKy(data, _typeChuky);
                            var ghiChu = GetGhiChu();
                            data.Add("HasGhiChu", ghiChu.Any());
                            data.Add("ListGhiChu", ghiChu);

                            if (i == 0)
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_TO1));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            }
                            else
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_TO2));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                            }
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel, HeaderReportPhanBoKiemTraTheoNganhDonViDocModel, GhiChu>(templateFileName, data);
                            if (IsChuyenNganh)
                            {
                                //xlsFile.SetColHidden(4, true);
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                            else
                            {
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                        }

                        // Không có dữ liệu
                        if (listDonVi.Count <= 0)
                        {
                            var nameColunmMerge = GetExcelColumnName(8);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond + " NĂM " + yearOfWork);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                            data.Add("h1", h1);
                            data.Add("h2", "");
                            data.Add("TenNganh", "");
                            data.Add("Headers", new List<DonVi>());
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02A_EMPTY);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix + "_" + item.value.ValueItem);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                            exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportPhuongAnPhanBoSKT02B(ExportType exportType)
        {
            try
            {
                if (!ListNNganh.Where(x => x.IsChecked).Any())
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckNganh);
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _isInTheoTongHop = false;
                    List<ExportResult> exportResults = new List<ExportResult>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    CurrencyToText currencyToText = new CurrencyToText();
                    var donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var listSpecializedChecked = ListNNganh.Where(x => x.IsChecked).ToList();
                    var iKhoi = int.Parse(_khoiSelected.ValueItem);
                    var loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    string listNganh = string.Join(",", listSpecializedChecked.Select(x => x.ValueItem.ToString()).ToList());

                    List<ReportPhanBoKiemTraPhuongAnPhanBoQuery> listDataDonVi = new List<ReportPhanBoKiemTraPhuongAnPhanBoQuery>();
                    listDataDonVi = _sktChungTuChiTietService.PrintReportPhuongAnPhanBoSKT02B(listNganh, yearOfWork, yearOfBudget, budgetSource, iKhoi, donViTinh, false, loaiNNS).ToList();
                    var lstIdMucLuc = listDataDonVi.Select(x => x.sKyHieu).Distinct().ToList();

                    var lstMucLuc = _iSktMucLucService.FindByCondition(x => x.INamLamViec == yearOfWork && (lstIdMucLuc.Contains(x.SKyHieuCu) || lstIdMucLuc.Contains(x.SKyHieu))).ToList();
                    var lstIddonVi = listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList();
                    var lstDonVi = _nsDonViService.FindByCondition(x => x.NamLamViec == yearOfWork && x.ITrangThai == 1 && lstIddonVi.Contains(x.IIDMaDonVi)).ToList();
                    List<ReportPhanBoKiemTraPhuongAnPhanBoQuery> results = new List<ReportPhanBoKiemTraPhuongAnPhanBoQuery>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    foreach (var dt in lstMucLuc)
                    {
                        ReportPhanBoKiemTraPhuongAnPhanBoQuery itemResult = new ReportPhanBoKiemTraPhuongAnPhanBoQuery();
                        itemResult.BHangCha = true;
                        itemResult.IIdMlskt = dt.IIDMLSKT;
                        itemResult.IIdMlsktCha = dt.IIDMLSKTCha;
                        itemResult.sKyHieu = dt.SKyHieu;
                        itemResult.STT = dt.SSTT;
                        itemResult.sMoTa = dt.SMoTa;
                        itemResult.sL = dt.SL;
                        itemResult.sK = dt.SK;
                        itemResult.sM = dt.SM;
                        itemResult.sNG = dt.SNg;
                        itemResult.Level = 4;
                        itemResult.SSTTBC = dt.SSttBC;
                        results.Add(itemResult);
                        foreach (var dv in lstDonVi)
                        {
                            var mlLastYear = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi) && x.sKyHieu.Equals(dt.SKyHieuCu)).FirstOrDefault();
                            var mlYear = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi) && x.sKyHieu.Equals(dt.SKyHieu)).FirstOrDefault();

                            itemResult = new ReportPhanBoKiemTraPhuongAnPhanBoQuery();
                            itemResult.STT = "";
                            itemResult.sMoTa = dt.SMoTa;
                            itemResult.sL = dt.SL;
                            itemResult.sM = dt.SM;
                            itemResult.sK = dt.SK;
                            itemResult.sNG = dt.SNg;
                            itemResult.IdDonVi = dv.IIDMaDonVi;
                            itemResult.STenDonVi = dv.TenDonVi;
                            itemResult.sKyHieu = dt.SKyHieu;
                            itemResult.BHangCha = false;
                            itemResult.IIdMlsktCha = dt.IIDMLSKT;
                            itemResult.IIdMlskt = Guid.NewGuid();
                            itemResult.FSoKiemTraNS = mlLastYear?.FSoKiemTraNS ?? 0;
                            itemResult.FDuToanDauNam = mlLastYear?.FDuToanDauNam ?? 0;
                            itemResult.FSoDuKienPB = mlYear?.FSoDuKienPB ?? 0;
                            itemResult.FTang = ((itemResult.FSoDuKienPB = mlYear?.FSoDuKienPB ?? 0) - (itemResult.FSoKiemTraNS = mlLastYear?.FSoKiemTraNS ?? 0)) > 0 ? (itemResult.FSoDuKienPB = mlYear?.FSoDuKienPB ?? 0) - (itemResult.FSoKiemTraNS = mlLastYear?.FSoKiemTraNS ?? 0) : 0;
                            itemResult.FGiam = ((itemResult.FSoKiemTraNS = mlLastYear?.FSoKiemTraNS ?? 0) - (itemResult.FSoDuKienPB = mlYear?.FSoDuKienPB ?? 0)) > 0 ? (itemResult.FSoKiemTraNS = mlLastYear?.FSoKiemTraNS ?? 0) - (itemResult.FSoDuKienPB = mlYear?.FSoDuKienPB ?? 0) : 0;
                            itemResult.Level = 4;
                            itemResult.SSTTBC = dt.SSttBC;
                            results.Add(itemResult);
                        }
                    }

                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    foreach (var mlc in sktMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                        {
                            ReportPhanBoKiemTraPhuongAnPhanBoQuery mlCha = new ReportPhanBoKiemTraPhuongAnPhanBoQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IIdMlsktCha = mlc.IIDMLSKTCha;
                            mlCha.IIdMlskt = mlc.IIDMLSKT;
                            mlCha.sMoTa = mlc.SMoTa;
                            mlCha.sKyHieu = mlc.SKyHieu;
                            mlCha.sL = mlc.IIDMLSKTCha == Guid.Empty ? mlc.SL : null;
                            mlCha.sK = mlc.SK;
                            mlCha.sM = mlc.SM;
                            mlCha.sNG = mlc.SNg;
                            mlCha.STT = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            results.Add(mlCha);
                        }
                    }

                    CalculateData(results);

                    foreach (var par in results.Where(x => x.BHangCha.GetValueOrDefault()))
                    {
                        if (par.FSoDuKienPB - par.FSoKiemTraNS > 0)
                        {
                            par.FTang = par.FSoDuKienPB - par.FSoKiemTraNS;
                            par.FGiam = 0;
                        }
                        else
                        {
                            par.FGiam = par.FSoKiemTraNS - par.FSoDuKienPB;
                            par.FTang = 0;
                        }
                    }
                    results = results.Where(x => x.FSoKiemTraNS != 0 || x.FDuToanDauNam != 0 || x.FSoDuKienPB != 0).OrderBy(x => x.SSTTBC).ToList();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", results);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond + " NĂM " + yearOfWork);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("DonVi", "");
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("h1", h1);
                    data.Add("h2", "");
                    data.Add("SumTotalSoKiemTraNS", results.Where(x => x.BHangCha == false).Sum(x => x.FSoKiemTraNS));
                    data.Add("SumTotalDuToanDauNam", results.Where(x => x.BHangCha == false).Sum(x => x.FDuToanDauNam));
                    data.Add("SumTotalSoDuKienPB", results.Where(x => x.BHangCha == false).Sum(x => x.FSoDuKienPB));
                    data.Add("SumTotalTang", results.Where(x => x.BHangCha == false).Sum(x => x.FTang));
                    data.Add("SumTotalGiam", results.Where(x => x.BHangCha == false).Sum(x => x.FGiam));
                    data.Add("YearOfWorkBefore", yearOfWork - 1);
                    data.Add("YearOfWork", yearOfWork);
                    data.Add("DonViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                    AddChuKy(data, _typeChuky);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    var templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHUONG_AN_PHANBO_SOKIEMTRA_02B));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportPhanBoKiemTraPhuongAnPhanBoQuery, GhiChu>(templateFileName, data);
                    exportResults.Add(new ExportResult("Báo cáo phương án phân bổ số kiểm tra - 02B", fileNameWithoutExtension, null, xlsFile));
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportPhanBoTheoNganhPhuLucDonViDocExcelTren1To(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DISTRIBUTION;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    //var lstIdCodeSelected = "";

                    var listSpecializedChecked = ListNNganh.Where(x => x.IsChecked).ToList();

                    foreach (var item in listSpecializedChecked.Select((value, i) => new { i, value }))
                    {
                        List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(item.value.ValueItem, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh, false).ToList();

                        var listDonViAll = _nsDonViService.FindByNamLamViec(yearOfWork);
                        if (_khoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                        {
                            listDonViAll = listDonViAll.Where(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(_khoiSelected.ValueItem)).ToList();
                        }
                        var listDataDonVi1 = from donVi1 in listDataDonVi
                                             join donVi2 in listDonViAll
                                   on donVi1.IdDonVi equals donVi2.IIDMaDonVi
                                             select donVi1;
                        listDataDonVi = _mapper.Map<List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>>(listDataDonVi1);
                        listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();




                        var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                        var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).Where(n => Convert.ToInt32(n.Loai) == 1).OrderBy(n => n.IIDMaDonVi).ToList();

                        var listIdMucLuc = listDataDonVi.Select(x => x.IIdMlskt).Distinct().ToList();
                        List<NsSktMucLuc> sktMucLucs = _iSktMucLucService
                            .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).OrderBy(x => x.SNg).ToList();
                        List<NsSktMucLuc> lstMlResult = new List<NsSktMucLuc>();
                        List<string> lstNg = new List<string>();
                        foreach (var mucluc in sktMucLucs)
                        {
                            if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.SNg))
                            {
                                NsSktMucLuc mlTong = new NsSktMucLuc();
                                mlTong.SNg = mucluc.SNg;
                                mlTong.SMoTa = "(+)";
                                lstMlResult.Add(mlTong);
                                lstNg.Add(mucluc.SNg);
                            }
                            lstMlResult.Add(mucluc);
                        }

                        //var groupNg = lstMlResult.GroupBy(x => x.SNg, (key, value) => new { key, value = value.ToList() }).ToList();
                        //foreach (var gr in groupNg)
                        //{
                        //    var count = gr.value.Count();
                        //    for (int i = 0; i < 6 - count % 6; i++)
                        //    {
                        //        NsSktMucLuc empty = new NsSktMucLuc();
                        //        empty.SNg = gr.key;
                        //        empty.INamLamViec = _sessionService.Current.YearOfWork;
                        //        gr.value.Add(empty);
                        //    }
                        //}

                        //lstMlResult = groupNg.SelectMany(x => x.value).ToList();
                        var listMucLucSplits = SplitList(lstMlResult, lstMlResult.Count).ToList();
                        for (int i = 0; i < listMucLucSplits.Count; i++)
                        {
                            List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel> headers = new List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>();
                            var lstNganhHeader = listMucLucSplits[i].Select(x => x.SNg).Distinct().ToList();
                            //var tenNganh = ListChuyenNganh.FirstOrDefault(x => lstNganhHeader != null && x.ValueItem.Contains(lstNganhHeader.FirstOrDefault())).NameItem;
                            var tenNganh = item.value.NameItem;
                            int columnStart = 5;
                            foreach (var nganhHeader in lstNganhHeader)
                            {
                                var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                                var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.SNg)).Select(x => x.SMoTa).ToList();
                                var mergeRange = "";
                                var columnStartName = GetExcelColumnName(columnStart);
                                var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                mergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                                //columnStart += 1;

                                HeaderReportPhanBoKiemTraTheoNganhDonViDocModel hd = new HeaderReportPhanBoKiemTraTheoNganhDonViDocModel();
                                hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                hd.LstMucLuc = new List<HeaderDetail>();
                                hd.LstNganhHeader = new List<HeaderDetail>();
                                hd.MergeRange = mergeRange;
                                int j = 0;
                                foreach (var mlHeader in lstMlHeader)
                                {
                                    HeaderDetail mlhd = new HeaderDetail();
                                    if (j == 0)
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = nganh != null ? nganh.NameItem : "";
                                        mlhd.SSttBC = "1";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    else
                                    {
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = "";
                                        mlhd.SSttBC = "2";
                                        hd.LstNganhHeader.Add(mlhd);
                                    }
                                    mlhd = new HeaderDetail();
                                    mlhd.MoTa = mlHeader;
                                    hd.LstMucLuc.Add(mlhd);
                                    j++;
                                }
                                headers.Add(hd);
                            }
                            int stt = 1;
                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            foreach (var dv in listDonVi)
                            {
                                ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel kq = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                                kq.Stt = stt++;
                                kq.MaDonVi = dv.IIDMaDonVi;
                                kq.TenDonVi = dv.TenDonVi;
                                kq.TongCong = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.IIDMaDonVi)).Sum(x => x.TuChi);
                                kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                foreach (var ml in listMucLucSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (ml.INamLamViec.Equals(0))
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.Where(x => ml.SNg.Contains(x.sNG) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                    else
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlskt == ml.IIDMLSKT);
                                        giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                        kq.LstGiaTri.Add(giaTri);
                                    }
                                }
                                results.Add(kq);
                            }
                            // results = results.OrderBy(n => n.MaDonVi).ToList();

                            List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                            ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel total = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                            total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                            foreach (var dv in listMucLucSplits[i])
                            {
                                NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                if (dv.INamLamViec.Equals(0))
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => dv.SNg.Contains(x.sNG)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                                else
                                {
                                    giaTri = new NsSktChungTuChiTiet();
                                    var gtDonVi = listDataDonVi.Where(x => x.IIdMlskt.Equals(dv.IIDMLSKT)).Sum(x => x.TuChi);
                                    giaTri.FTuChi = gtDonVi;
                                    total.LstGiaTriTotal.Add(giaTri);
                                }
                            }
                            resultsTotal.Add(total);
                            var sumTotal = listDataDonVi.Sum(x => x.TuChi);
                            var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                            var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            var prefixTenNganh = "Ngành";
                            if (IsChuyenNganh)
                            {
                                prefixTenNganh = "Chuyên ngành";
                            }
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", results);
                            data.Add("ListDataTotal", resultsTotal);
                            data.Add("SumTotal", sumTotal);
                            data.Add("TongSoTien", sumTotal * donViTinh);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("h1", h1);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                            data.Add("h2", "");
                            //data.Add("TenNganh", tenNganh);
                            data.Add("TenNganh", prefixTenNganh + ": " + tenNganh);
                            data.Add("Headers", headers);
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                            AddChuKy(data, _typeChuky);

                            if (i == 0)
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_EXECL_1TO));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            }
                            else
                            {
                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO2));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                            }
                            //string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix + "_" + item.ValueItem);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel, HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>(templateFileName, data);
                            if (IsChuyenNganh)
                            {
                                xlsFile.SetColHidden(4, true);
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                            else
                            {
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                exportResults.Add(new ExportResult(prefixTenNganh + " " + tenNganh + " - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                            }
                        }

                        // Không có dữ liệu
                        if (listDonVi.Count <= 0)
                        {
                            var nameColunmMerge = GetExcelColumnName(8);
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("DonVi", "");
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                            data.Add("h1", h1);
                            data.Add("h2", "");
                            data.Add("TenNganh", "");
                            data.Add("Headers", new List<DonVi>());
                            data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                            data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                            data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                            data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                            data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY);
                            fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix + "_" + item.value.ValueItem);
                            var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                            exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = exportResults;



                    //if (ListNNganh != null)
                    //{
                    //    lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                    //}



                    //if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                    //{
                    //    listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(lstIdCodeSelected, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                    //        .ToList();
                    //    listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                    //}


                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportSoNhuCauTheoNganhPhuLucDonViDoc(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> exportResults = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;

                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = _catUnitTypeSelected.DisplayItem;
                    var tenNganh = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.NameItem).ToList());
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listDataDonVi = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                    //var lstIdCodeSelected = "";
                    if (ListNNganh != null)
                    {
                        //lstIdCodeSelected = string.Join(",", ListNNganh.Where(item => item.IsChecked).Select(item => item.ValueItem).ToList());
                        var lstNNganhSelected = ListNNganh.Where(item => item.IsChecked).ToList();
                        foreach (var nnganh in lstNNganhSelected)
                        {
                            var lstChungTuSnc = string.Join(",", ListIdChungTuBaoCaoSncNganh);
                            if (!StringUtils.IsNullOrEmpty(nnganh.ValueItem))
                            {
                                listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTheoNganhPhuLuc(nnganh.ValueItem, null, lstChungTuSnc, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                                    .ToList();
                                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0).ToList();
                                if (int.Parse(_catUnitTypeSelected.ValueItem) != 1)
                                {
                                    foreach (var prop in typeof(ReportPhanBoKiemTraTheoNganhPhuLucQuery).GetProperties())
                                    {
                                        if (prop.PropertyType.Name == "Double")
                                        {
                                            listDataDonVi.ForEach(x => prop.SetValue(x, Math.Round(Convert.ToDouble(prop.GetValue(x)))));
                                        }
                                    }
                                }
                            }
                            tenNganh = nnganh.NameItem;
                            //}                         
                            //}
                            //if (!StringUtils.IsNullOrEmpty(lstIdCodeSelected))
                            //{
                            //    listDataDonVi = _sktChungTuChiTietService.FindReportPhanBoKiemTraTheoNganhPhuLuc(lstIdCodeSelected, null, yearOfWork, yearOfBudget, budgetSource, loai, donViTinh)
                            //        .ToList();
                            //    listDataDonVi = listDataDonVi.Where(x => !x.IdDonVi.Equals(_sessionInfo.IdDonVi) && x.TuChi != 0).ToList();
                            //}

                            var listIdDonVi = string.Join(",", listDataDonVi.Select(x => x.IdDonVi).Distinct().ToList());
                            //var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).Where(n => !n.Loai.Equals("0")).ToList();
                            var listDonVi = _nsDonViService.FindByListIdDonVi(listIdDonVi, yearOfWork).ToList();
                            var listIdMucLuc = listDataDonVi.Select(x => x.IIdMlskt).Distinct().ToList();
                            List<NsSktMucLuc> sktMucLucs = _iSktMucLucService
                                .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).OrderBy(x => x.SNg).ToList();
                            List<NsSktMucLuc> lstMlResult = new List<NsSktMucLuc>();
                            List<string> lstNg = new List<string>();
                            foreach (var mucluc in sktMucLucs)
                            {
                                //if (IsChuyenNganh)
                                //{
                                if (lstNg.Count <= 0 || !lstNg.Contains(mucluc.SNg))
                                {
                                    NsSktMucLuc mlTong = new NsSktMucLuc();
                                    mlTong.SNg = mucluc.SNg;
                                    mlTong.SMoTa = "(+)";
                                    lstMlResult.Add(mlTong);
                                    lstNg.Add(mucluc.SNg);
                                }
                                lstMlResult.Add(mucluc);
                                //}
                                //else
                                //{
                                //    CheckBoxItem nNganh = new CheckBoxItem();
                                //    if (ListNNganh != null)
                                //    {
                                //        nNganh = ListNNganh.First(item => item.IsChecked && item.ValueItem.Contains(mucluc.SNg));
                                //    }
                                //    if (lstNg.Count <= 0 || !string.Join(",", lstNg).Contains(mucluc.SNg))
                                //    {
                                //        NsSktMucLuc mlTong = new NsSktMucLuc();
                                //        mlTong.SNg = nNganh.ValueItem;
                                //        mlTong.SMoTa = "(+)";
                                //        lstMlResult.Add(mlTong);
                                //        lstNg.Add(nNganh.ValueItem);
                                //    }
                                //    mucluc.SNg = nNganh.ValueItem;
                                //    lstMlResult.Add(mucluc);
                                //}
                            }

                            var listMucLucSplits = SplitList(lstMlResult, 6).ToList();
                            string path = "";
                            for (int i = 0; i < listMucLucSplits.Count; i++)
                            {
                                List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel> headers = new List<HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>();
                                var lstNganhHeader = listMucLucSplits[i].Select(x => x.SNg).Distinct().ToList();
                                int columnStart = 5;
                                foreach (var nganhHeader in lstNganhHeader)
                                {
                                    var nganh = ListChuyenNganh.FirstOrDefault(x => x.ValueItem.Contains(nganhHeader));
                                    var lstMlHeader = listMucLucSplits[i].Where(x => nganhHeader.Contains(x.SNg)).Select(x => x.SMoTa).ToList();
                                    var mergeRange = "";
                                    var columnStartName = GetExcelColumnName(columnStart);
                                    var columnEndName = GetExcelColumnName(lstMlHeader.Count() + columnStart - 1);
                                    mergeRange = columnStartName + "8" + ":" + columnEndName + "8";
                                    //columnStart += 1;

                                    HeaderReportPhanBoKiemTraTheoNganhDonViDocModel hd = new HeaderReportPhanBoKiemTraTheoNganhDonViDocModel();
                                    hd.TenNganh = nganh != null ? nganh.NameItem : "";
                                    hd.LstMucLuc = new List<HeaderDetail>();
                                    hd.LstNganhHeader = new List<HeaderDetail>();
                                    hd.MergeRange = mergeRange;
                                    int j = 0;
                                    foreach (var mlHeader in lstMlHeader)
                                    {
                                        HeaderDetail mlhd = new HeaderDetail();
                                        if (j == 0)
                                        {
                                            mlhd = new HeaderDetail();
                                            mlhd.MoTa = nganh != null ? nganh.NameItem : "";
                                            mlhd.SSttBC = "1";
                                            hd.LstNganhHeader.Add(mlhd);
                                        }
                                        else
                                        {
                                            mlhd = new HeaderDetail();
                                            mlhd.MoTa = "";
                                            mlhd.SSttBC = "2";
                                            hd.LstNganhHeader.Add(mlhd);
                                        }
                                        mlhd = new HeaderDetail();
                                        mlhd.MoTa = mlHeader;
                                        hd.LstMucLuc.Add(mlhd);
                                        j++;
                                    }
                                    headers.Add(hd);
                                }
                                int stt = 1;
                                List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> results = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                                foreach (var dv in listDonVi.OrderBy(x => x.IIDMaDonVi))
                                {
                                    ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel kq = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                                    kq.Stt = stt++;
                                    kq.MaDonVi = dv.IIDMaDonVi;
                                    kq.TenDonVi = dv.TenDonVi;
                                    kq.TongCong = listDataDonVi.Where(x => x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                    kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                                    foreach (var ml in listMucLucSplits[i])
                                    {
                                        NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                        if (ml.INamLamViec.Equals(0))
                                        {
                                            giaTri = new NsSktChungTuChiTiet();
                                            var gtDonVi = listDataDonVi.Where(x => ml.SNg.Contains(x.sNG) && x.IdDonVi == dv.IIDMaDonVi).Sum(x => x.TuChi);
                                            giaTri.FTuChi = gtDonVi;
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                        else
                                        {
                                            giaTri = new NsSktChungTuChiTiet();
                                            var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.IIDMaDonVi && x.IIdMlskt == ml.IIDMLSKT);
                                            giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                            kq.LstGiaTri.Add(giaTri);
                                        }
                                    }
                                    results.Add(kq);
                                }

                                List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel> resultsTotal = new List<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel>();
                                ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel total = new ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel();
                                total.LstGiaTriTotal = new List<NsSktChungTuChiTiet>();
                                foreach (var dv in listMucLucSplits[i])
                                {
                                    NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                    if (dv.INamLamViec.Equals(0))
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.Where(x => dv.SNg.Contains(x.sNG)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                    else
                                    {
                                        giaTri = new NsSktChungTuChiTiet();
                                        var gtDonVi = listDataDonVi.Where(x => x.IIdMlskt.Equals(dv.IIDMLSKT)).Sum(x => x.TuChi);
                                        giaTri.FTuChi = gtDonVi;
                                        total.LstGiaTriTotal.Add(giaTri);
                                    }
                                }
                                resultsTotal.Add(total);
                                var prefixTenNganh = "Ngành: ";
                                if (IsChuyenNganh)
                                {
                                    prefixTenNganh = "Chuyên ngành: ";
                                }
                                var sumTotal = listDataDonVi.Sum(x => x.TuChi);
                                var numColumnMerge = listMucLucSplits[i].Count() < 3 ? 4 : listMucLucSplits[i].Count();
                                var nameColunmMerge = GetExcelColumnName(numColumnMerge + 4);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", results);
                                data.Add("ListDataTotal", resultsTotal);
                                data.Add("SumTotal", sumTotal);
                                data.Add("TongSoTien", sumTotal * donViTinh);
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("h1", h1);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : "") + " - Tờ: " + (i + 1));
                                data.Add("h2", "");
                                //data.Add("TenNganh", tenNganh);
                                data.Add("TenNganh", prefixTenNganh + tenNganh);
                                data.Add("Headers", headers);
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");
                                AddChuKy(data, _typeChuky);

                                if (i == 0)
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO1));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_DONVI_DOC_TO2));
                                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + (i + 1);
                                }
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucDonViDocModel, HeaderReportPhanBoKiemTraTheoNganhDonViDocModel>(templateFileName, data);
                                //exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                if (IsChuyenNganh)
                                {
                                    xlsFile.SetColHidden(4, true);
                                    exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                }
                                else
                                {
                                    exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành - Tờ " + (i + 1), fileNameWithoutExtension, null, xlsFile));
                                }
                            }

                            // Không có dữ liệu
                            if (listDonVi.Count <= 0)
                            {
                                var nameColunmMerge = GetExcelColumnName(8);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                                data.Add("FormatNumber", formatNumber);
                                data.Add("ListData", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("ListDataTotal", new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>());
                                data.Add("TieuDe1", TxtTitleFirst);
                                data.Add("TieuDe2", TxtTitleSecond);
                                data.Add("TieuDe3", TxtTitleThird);
                                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                                data.Add("DonVi", "");
                                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                                data.Add("Cap2", _sessionInfo.TenDonVi);
                                data.Add("DiaDiem", _diaDiem);
                                data.Add("donViTinh", "Đơn vị tính: " + (_catUnitTypeSelected != null ? _catUnitTypeSelected.DisplayItem : ""));
                                data.Add("h1", h1);
                                data.Add("h2", "");
                                data.Add("TenNganh", tenNganh);
                                data.Add("Headers", new List<DonVi>());
                                data.Add("rangeRow1", "A1:" + nameColunmMerge + "1");
                                data.Add("rangeRow2", "A2:" + nameColunmMerge + "2");
                                data.Add("rangeRow3", "A3:" + nameColunmMerge + "3");
                                data.Add("rangeRow4", "A4:" + nameColunmMerge + "4");
                                data.Add("rangeRow6", "A6:" + nameColunmMerge + "6");

                                templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_THEONGANH_PHULUC_EMPTY));
                                fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                                var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery, DonVi>(templateFileName, data);
                                exportResults.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra theo ngành", fileNameWithoutExtension, null, xlsFile));
                            }
                        }
                    }
                    e.Result = exportResults;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize1, int nSize2)
        {
            for (int i = 0; i < bigList.Count; i += nSize1)
            {
                yield return bigList.GetRange(i, Math.Min(nSize1, bigList.Count - i));
                break;
            }
            for (int i = nSize2 - 1; i < bigList.Count; i += nSize2)
            {
                yield return bigList.GetRange(i, Math.Min(nSize2, bigList.Count - i));
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

        public void ReportDemandSummary(ExportType exportType)
        {
            string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                Dictionary<string, object> data = new Dictionary<string, object>();
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var loai = DemandCheckType.DEMAND;
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportSoNhuCauTongHopQuery> listData = _sktChungTuChiTietService.FindReportSoNhuCauTongHop(listIdTongHop, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, donViTinh, loai).ToList();
                CalculateData(listData);
                listData = listData.Where(item =>
                    Math.Abs(item.TuChi) >= double.Epsilon || Math.Abs(item.HuyDong) >= double.Epsilon ||
                    Math.Abs(item.MuaHangHienVat) >= double.Epsilon || Math.Abs(item.PhanCap) >= double.Epsilon).ToList();
                foreach (var item in listData)
                {
                    item.TongCongNSSD = item.TuChi + item.HuyDong;
                    item.TongCongNSBD = item.MuaHangHienVat + item.PhanCap;
                }
                //NSSD
                double SumTotalHuyDong = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.HuyDong);
                double SumTotalTuChi = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TuChi);
                double SumTotalTongCongNSSD = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongCongNSSD);
                //NSBD           
                double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.MuaHangHienVat);
                double SumTotalDacThu = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.PhanCap);
                double SumTotalTongCongNSBD = listData.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongCongNSBD);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", listData);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DiaDiem", _diaDiem);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                data.Add("SumTotalHuyDong", SumTotalHuyDong);
                data.Add("SumTotalTuChi", SumTotalTuChi);
                data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                data.Add("SumTotalDacThu", SumTotalDacThu);
                data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));

                string templateFileName;
                if (loaiChungTu == 1)
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_TONGHOP_TRINHKY_NSSD);
                }
                else
                {
                    templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_TONGHOP_TRINHKY_NSBD);
                }
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportSoNhuCauTongHopQuery>(templateFileName, data);
                e.Result = new ExportResult(data["DonVi"].ToString(), fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandSummaryPhuLuc1(ExportType exportType)
        {
            string listIdTongHop = "";
            if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
            {
                listIdTongHop = GetListDonViChungTuTongHop();
                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.AlertKhongCoChungTuTongHop);
                    return;
                }
            }
            else
            {
                listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                List<ExportResult> exportResults = new List<ExportResult>();
                string templateFileName = "";
                string fileNamePrefix;

                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstIdDonVi = ListDonVi.Where(item => item.IsChecked).ToList();
                string maBQuanLy = "0";
                if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
                {
                    maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                }

                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var loai = DemandCheckType.DEMAND;
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                List<ReportSoNhuCauTongHopDonViQuery> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopDonVi(listIdTongHop, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, donViTinh, loai, maBQuanLy, loaiNNS).ToList();
                XuLyDuLieu(listDataDonVi);
                int size = SelectedLoaiDuLieu != null && SelectedLoaiDuLieu.ValueItem.Equals("1") ? 2 : 6;
                var listDonViSplits = SplitList(lstIdDonVi, size).ToList();
                var listIdMucLuc = listDataDonVi.Select(x => x.KyHieu).Distinct().ToList();
                List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(listIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!listIdMucLuc.Contains(mlc.SKyHieu))
                    {
                        foreach (var dv in lstIdDonVi)
                        {
                            ReportSoNhuCauTongHopDonViQuery mlCha = new ReportSoNhuCauTongHopDonViQuery();
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.IdParent = mlc.IIDMLSKTCha;
                            mlCha.IdMucLuc = mlc.IIDMLSKT;
                            mlCha.IdDonVi = dv.ValueItem;
                            mlCha.MoTa = mlc.SMoTa;
                            mlCha.KyHieu = mlc.SKyHieu;
                            mlCha.M = mlc.SM;
                            mlCha.Stt = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            listDataDonVi.Add(mlCha);
                        }
                    }
                }
                CalculateData(listDataDonVi);
                listIdMucLuc = listDataDonVi.OrderBy(x => x.SSTTBC).Select(x => x.KyHieu).Distinct().ToList();
                for (int i = 0; i < listDonViSplits.Count; i++)
                {
                    List<ReportSoNhuCauTongHopQuery> results = new List<ReportSoNhuCauTongHopQuery>();
                    List<HeaderReportSoNhuCauTongHopPhuLuc1> headers = new List<HeaderReportSoNhuCauTongHopPhuLuc1>();
                    if (listDonViSplits[i].Count < size)
                    {
                        var countEmpty = size - listDonViSplits[i].Count;
                        for (int j = 0; j < countEmpty; j++)
                        {
                            CheckBoxItem emptyCb = new CheckBoxItem();
                            listDonViSplits[i].Add(emptyCb);
                        }
                    }
                    foreach (var tenDv in listDonViSplits[i])
                    {
                        HeaderReportSoNhuCauTongHopPhuLuc1 hd = new HeaderReportSoNhuCauTongHopPhuLuc1();
                        hd.TenDonVi = tenDv.NameItem;
                        headers.Add(hd);
                    }
                    foreach (var ml in listIdMucLuc)
                    {
                        var mucLuc = _iSktMucLucService.FindByCondition(x => x.SKyHieu == ml && x.INamLamViec == yearOfWork).FirstOrDefault();
                        if (mucLuc != null)
                        {
                            ReportSoNhuCauTongHopQuery kq = new ReportSoNhuCauTongHopQuery();
                            kq.IdMucLuc = mucLuc.Id;
                            kq.Stt = mucLuc.SSTT;
                            kq.MoTa = mucLuc.SMoTa;
                            kq.bHangCha = mucLuc.BHangCha;
                            kq.M = mucLuc.SM;
                            kq.KyHieu = mucLuc.SKyHieu;
                            kq.IdParent = mucLuc.IIDMLSKTCha;
                            kq.TongCong = listDataDonVi.Where(x => x.KyHieu == ml).Sum(x => x.HuyDong + x.TuChi + x.PhanCap + x.MuaHangHienVat);
                            kq.LstGiaTri = new List<NsSktChungTuChiTiet>();
                            foreach (var dv in listDonViSplits[i])
                            {
                                NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                                if (!string.IsNullOrEmpty(dv.ValueItem))
                                {
                                    var gtDonVi = listDataDonVi.FirstOrDefault(x => x.IdDonVi == dv.ValueItem && x.KyHieu == ml);
                                    giaTri.FTuChi = gtDonVi != null ? gtDonVi.TuChi : 0;
                                    giaTri.FHuyDongTonKho = gtDonVi != null ? gtDonVi.HuyDong : 0;
                                    giaTri.FMuaHangCapHienVat = gtDonVi != null ? gtDonVi.MuaHangHienVat : 0;
                                    giaTri.FPhanCap = gtDonVi != null ? gtDonVi.PhanCap : 0;
                                    giaTri.FCong = giaTri.FTuChi + giaTri.FHuyDongTonKho + giaTri.FMuaHangCapHienVat + giaTri.FPhanCap;
                                    kq.LstGiaTri.Add(giaTri);
                                }
                                else
                                {
                                    kq.LstGiaTri.Add(giaTri);
                                }
                            }
                            results.Add(kq);
                        }
                    }

                    List<ReportSoNhuCauTongHopQuery> resultsTotal = new List<ReportSoNhuCauTongHopQuery>();
                    ReportSoNhuCauTongHopQuery total = new ReportSoNhuCauTongHopQuery();
                    total.TongCong = results.Where(x => x.IdParent.IsNullOrEmpty()).Sum(x => x.TongCong);
                    total.LstTong = new List<NsSktChungTuChiTiet>();
                    foreach (var dv in listDonViSplits[i])
                    {
                        NsSktChungTuChiTiet giaTri = new NsSktChungTuChiTiet();
                        if (!string.IsNullOrEmpty(dv.ValueItem))
                        {
                            var gtDonViTuChi = listDataDonVi.Where(x => x.IdDonVi == dv.ValueItem && x.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                            var gtDonViHuyDong = listDataDonVi.Where(x => x.IdDonVi == dv.ValueItem && x.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                            var gtDonViMuaHangHienVat = listDataDonVi.Where(x => x.IdDonVi == dv.ValueItem && x.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                            var gtDonViPhanCap = listDataDonVi.Where(x => x.IdDonVi == dv.ValueItem && x.IdParent.IsNullOrEmpty()).Sum(x => x.PhanCap);
                            giaTri.FTuChi = gtDonViTuChi;
                            giaTri.FHuyDongTonKho = gtDonViHuyDong;
                            giaTri.FMuaHangCapHienVat = gtDonViMuaHangHienVat;
                            giaTri.FPhanCap = gtDonViPhanCap;
                            giaTri.FCong = giaTri.FTuChi + giaTri.FHuyDongTonKho + giaTri.FMuaHangCapHienVat + giaTri.FPhanCap;
                            total.LstTong.Add(giaTri);
                        }
                        else
                        {
                            total.LstTong.Add(giaTri);
                        }
                    }
                    resultsTotal.Add(total);
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_SNC_TONGHOP) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", results);
                    data.Add("ListTotal", resultsTotal);
                    data.Add("Headers", headers);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    AddChuKy(data, _typeChuky);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    if (i == 0)
                    {
                        if (loaiChungTu == 1)
                        {
                            if (SelectedLoaiDuLieu != null)
                            {
                                if (SelectedLoaiDuLieu.ValueItem.Equals("1"))
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TO1));
                                }
                                else if (SelectedLoaiDuLieu.ValueItem.Equals("2"))
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TUCHI_TO1));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_HUYDONG_TO1));
                                }
                            }
                        }
                        else
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSBD_TO1));
                        }
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    }
                    else
                    {
                        if (loaiChungTu == 1)
                        {
                            if (SelectedLoaiDuLieu != null)
                            {
                                if (SelectedLoaiDuLieu.ValueItem.Equals("1"))
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TO2));
                                }
                                else if (SelectedLoaiDuLieu.ValueItem.Equals("2"))
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_TUCHI_TO2));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSSD_HUYDONG_TO2));
                                }
                            }
                        }
                        else
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC1_NSBD_TO2));
                        }
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + (i + 1);
                    }

                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportSoNhuCauTongHopQuery, HeaderReportSoNhuCauTongHopPhuLuc1, GhiChu>(templateFileName, data);
                    exportResults.Add(new ExportResult("Báo cáo số nhu cầu tổng hợp phụ lục tờ" + (i + 1), fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = exportResults;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, exportType);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandSummaryPhuLuc2(ExportType exportType)
        {
            string listIdTongHop = "";
            if (IsInTheoTongHop)
            {
                listIdTongHop = GetListDonViChungTuTongHop();
                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.AlertKhongCoChungTuTongHop);
                    return;
                }
            }
            else
            {
                listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                if (listIdTongHop == "")
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                Dictionary<string, object> data = new Dictionary<string, object>();
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();

                string maBQuanLy = "0";
                if (DemandCheckPrintType.REPORT_DEMAND_NUMBER_SUMMARY.Equals(DemandCheckPrintType))
                {
                    maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                }
                if (listIdTongHop == "")
                {
                    this.IsLoading = false;
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var loai = DemandCheckType.DEMAND;
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportSoNhuCauTongHopDonViQuery> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopDonVi(listIdTongHop, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, donViTinh, loai, maBQuanLy, loaiNNS).ToList();
                listDataDonVi = listDataDonVi.Where(x => x.TuChi != 0 || x.MuaHangHienVat != 0 || x.PhanCap != 0).ToList();
                var lstIdMucLuc = listDataDonVi.Select(x => x.KyHieu).Distinct().ToList();
                var lstMucLuc = _iSktMucLucService.FindByCondition(x => x.INamLamViec == yearOfWork && lstIdMucLuc.Contains(x.SKyHieu)).ToList();
                List<ReportSoNhuCauTongHopQuery> results = new List<ReportSoNhuCauTongHopQuery>();
                foreach (var dt in lstMucLuc)
                {
                    ReportSoNhuCauTongHopQuery itemResult = new ReportSoNhuCauTongHopQuery();
                    itemResult.bHangCha = true;
                    itemResult.IdMucLuc = dt.IIDMLSKT;
                    itemResult.IdParent = dt.IIDMLSKTCha;
                    itemResult.KyHieu = dt.SKyHieu;
                    itemResult.Stt = dt.SSTT;
                    itemResult.MoTa = dt.SMoTa;
                    itemResult.M = dt.SM;
                    itemResult.Level = 4;
                    results.Add(itemResult);
                    foreach (var dv in lstDonViSelected)
                    {
                        var ml = listDataDonVi.Where(x => x.IdDonVi.Equals(dv.ValueItem) && x.IdMucLuc.Equals(dt.IIDMLSKT))
                            .FirstOrDefault();
                        if (ml != null)
                        {
                            itemResult = new ReportSoNhuCauTongHopQuery();
                            itemResult.Stt = "";
                            itemResult.MoTa = dt.SMoTa;
                            itemResult.M = dt.SM;
                            itemResult.TenDonVi = dv.NameItem;
                            itemResult.KyHieu = dt.SKyHieu;
                            itemResult.bHangCha = false;
                            itemResult.IdParent = dt.IIDMLSKT;
                            itemResult.IdMucLuc = Guid.NewGuid();
                            itemResult.HuyDong = ml.HuyDong;
                            itemResult.TuChi = ml.TuChi;
                            itemResult.MuaHangHienVat = ml.MuaHangHienVat;
                            itemResult.PhanCap = ml.PhanCap;
                            itemResult.Level = 4;
                            results.Add(itemResult);
                        }
                    }
                }
                List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                    {
                        ReportSoNhuCauTongHopQuery mlCha = new ReportSoNhuCauTongHopQuery();
                        mlCha.bHangCha = mlc.BHangCha;
                        mlCha.IdParent = mlc.IIDMLSKTCha;
                        mlCha.IdMucLuc = mlc.IIDMLSKT;
                        mlCha.MoTa = mlc.SMoTa;
                        mlCha.KyHieu = mlc.SKyHieu;
                        mlCha.M = mlc.SM;
                        mlCha.Stt = mlc.SSTT;
                        results.Add(mlCha);
                    }
                }
                results = results.OrderBy(x => x.KyHieu).ToList();
                CalculateData(results);
                foreach (var item in results)
                {
                    item.TongCongNSSD = item.TuChi + item.HuyDong;
                    item.TongCongNSBD = item.MuaHangHienVat + item.PhanCap;
                }
                //NSSD
                double SumTotalHuyDong = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                double SumTotalTuChi = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                double SumTotalTongCongNSSD = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSSD);
                //NSBD           
                double SumTotalMuaHangHienVat = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                double SumTotalDacThu = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.PhanCap);
                double SumTotalTongCongNSBD = results.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSBD);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", results);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DiaDiem", _diaDiem);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("SumTotalHuyDong", SumTotalHuyDong);
                data.Add("SumTotalTuChi", SumTotalTuChi);
                data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                data.Add("SumTotalDacThu", SumTotalDacThu);
                data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));
                AddChuKy(data, _typeChuky);
                var ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);

                string templateFileName;
                if (loaiChungTu == 1)
                {
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC2_NSSD));
                }
                else
                {
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC2_NSBD));
                }
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportSoNhuCauTongHopQuery, GhiChu>(templateFileName, data);
                e.Result = new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void FormatDisplay(List<SktChungTuChiTietQuery> lstData)
        {

            foreach (var item in lstData.Where(x => !string.IsNullOrEmpty(x.SK)))
            {
                var parent = lstData.FirstOrDefault(x => x.IIdMlskt == item.IIdParentMlskt);
                if (parent != null && parent.SL != string.Empty)
                {
                    //item.sM = string.Empty;
                    item.SL = string.Empty;
                    //item.sK = string.Empty;
                    //item.sLNS = string.Empty;

                }
            }
        }

        public void OnPrintReportDemandSummaryPhuLuc3(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                if (listIdTongHop == "")
                {
                    this.IsLoading = false;
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = 1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                var listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopPhuLuc3(loaiChungTu, listIdTongHop, yearOfWork, yearOfBudget, budgetSource, maBQuanLy, donViTinh, loaiNNS).Where(x => (BudgetTypeSelected.ValueItem == "1" && x.SKyHieu.StartsWith("1")) || (BudgetTypeSelected.ValueItem != "1" && !x.SKyHieu.StartsWith("1"))).ToList();

                if (int.Parse(_catUnitTypeSelected.ValueItem) != 1)
                {
                    foreach (var prop in typeof(ReportTongHopSoNhuCauPhuLuc3Query).GetProperties())
                    {
                        if (prop.PropertyType.Name == "Double")
                        {
                            listDataDonVi.ForEach(x => prop.SetValue(x, Math.Round(Convert.ToDouble(prop.GetValue(x)))));
                        }
                    }
                }


                var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                    {
                        ReportTongHopSoNhuCauPhuLuc3Query mlCha = new ReportTongHopSoNhuCauPhuLuc3Query();
                        mlCha.Stt = mlc.SSTT;
                        mlCha.SSTTBC = mlc.SSttBC;
                        mlCha.BHangCha = mlc.BHangCha;
                        mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                        mlCha.iID_MLSKT = mlc.IIDMLSKT;
                        mlCha.SL = mlc.SL;
                        mlCha.SK = mlc.SK;
                        mlCha.SM = mlc.SM;
                        mlCha.SNG = mlc.SNg;
                        mlCha.SKyHieu = mlc.SKyHieu;
                        mlCha.SMoTa = mlc.SMoTa;
                        listDataDonVi.Add(mlCha);
                    }
                }
                listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                CalculateData(listDataDonVi);
                foreach (var item in listDataDonVi)
                {
                    item.TongSo = item.TuChi + item.HuyDongTonKho;
                    var diff = item.SKyHieu.StartsWith("1") ? item.TuChi - item.SoKiemTraNamTruoc : item.TuChi - item.DuToanDauNam;
                    item.Tang = diff > 0 ? diff : 0;
                    item.Giam = diff < 0 ? -diff : 0;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                AddChuKy(data, _typeChuky);
                AddNgayDiaDiem(data);
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongSo) * donViTinh);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", listDataDonVi);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("NamNay", yearOfWork);
                data.Add("NamTruoc", yearOfWork - 1);
                var ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);
                data.Add("IsTongHop", true);

                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(BudgetTypeSelected.ValueItem == "1" ? ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC3_NSQP : ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC3_NSNN));
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc3Query, GhiChu>(templateFileName, data);
                e.Result = new ExportResult("Tổng hợp nhu cầu chi ngân sách", fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private string GetMaGhiChu()
        {
            if (IsBaoCaoSoNhuCauTongHop)
            {
                if (PaperPrintTypeSelected != null
                && KhoiSelected != null
                && BQuanLySelected != null
                && VoucherTypeSelected != null
                && BudgetSourceTypeSelected != null
                && BudgetTypeSelected != null)
                {
                    if (IsVisibilityRadioButtonNSBD)
                    {
                        var data = JsonConvert.SerializeObject(new
                        {
                            LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                            InTheoChungTuTongHop = IsInTheoTongHop,
                            BQuanLy = BQuanLySelected.DisplayItem,
                            LoaiChungTu = VoucherTypeSelected.DisplayItem,
                            LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                            LoaiNganSach = BudgetTypeSelected.DisplayItem,
                            DacThu = LoaiNSBD == LoaiNSBD.DAC_THU,
                            MHHV = LoaiNSBD == LoaiNSBD.MHHV
                        });
                        return CompressExtension.CompressToBase64(data);
                    }
                    else
                    {
                        var data = JsonConvert.SerializeObject(new
                        {
                            LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                            InTheoChungTuTongHop = IsInTheoTongHop,
                            Khoi = KhoiSelected.DisplayItem,
                            BQuanLy = BQuanLySelected.DisplayItem,
                            LoaiChungTu = VoucherTypeSelected.DisplayItem,
                            LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                            LoaiNganSach = BudgetTypeSelected.DisplayItem
                        });
                        return CompressExtension.CompressToBase64(data);
                    }
                }
                else
                    return string.Empty;
            }
            else
            {

                if (VoucherTypeSelected != null
                    && BudgetSourceTypeSelected != null
                    && BudgetTypeSelected != null)
                {
                    if (IsVisibilityRadioButtonNSBD)
                    {
                        var data = JsonConvert.SerializeObject(new
                        {
                            LoaiChungTu = VoucherTypeSelected.DisplayItem,
                            LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                            LoaiNganSach = BudgetTypeSelected.DisplayItem,
                            DacThu = LoaiNSBD == LoaiNSBD.DAC_THU,
                            MHHV = LoaiNSBD == LoaiNSBD.MHHV
                        });
                        return CompressExtension.CompressToBase64(data);
                    }
                    else if (IsBaoCaoSoKiemTra)
                    {
                        var data = JsonConvert.SerializeObject(new
                        {
                            LoaiBaoCao = PaperPrintTypeSelected.DisplayItem,
                            LoaiChungTu = VoucherTypeSelected.DisplayItem,
                            LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                            LoaiNganSach = BudgetTypeSelected.DisplayItem,
                            Khoi = KhoiSelected.DisplayItem,
                        });
                        return CompressExtension.CompressToBase64(data);
                    }
                    else
                    {
                        var data = JsonConvert.SerializeObject(new
                        {
                            LoaiChungTu = VoucherTypeSelected.DisplayItem,
                            LoaiNguonNganSach = BudgetSourceTypeSelected.DisplayItem,
                            LoaiNganSach = BudgetTypeSelected.DisplayItem,
                        });
                        return CompressExtension.CompressToBase64(data);
                    }
                }
                else
                    return string.Empty;
            }
        }

        private List<GhiChu> GetGhiChu()
        {
            var typeChuKy = (_typeChuky == TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A || _typeChuky == TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02B) ? TypeChuKy.RPT_NS_PHUONGAN_PHANBO_SOKIEMTRA_02A : _typeChuky;
            var iNamLamViec = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<NsCauHinhBaoCao>();
            predicate = predicate.And(x => x.INamLamViec.Equals(iNamLamViec));
            predicate = predicate.And(x => x.SMaBaoCao == typeChuKy);
            predicate = predicate.And(x => x.SMaGhiChu == GetMaGhiChu());
            var data = _ghiChuService.FindByCondition(predicate).ToList();
            if (!string.IsNullOrEmpty(data.FirstOrDefault()?.SGhiChu))
            {
                return data.FirstOrDefault()?.SGhiChu.Split(Environment.NewLine).Select(x => new GhiChu()
                {
                    Content = x
                }).ToList();
            }
            else if (IsInTheoTongHop)
            {
                return GetDefaultGhiChu();
            }
            else
            {
                return new List<GhiChu>();
            }
        }

        private List<GhiChu> GetDefaultGhiChu()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            int yearOfBudget = _sessionService.Current.YearOfBudget;
            int budgetSource = _sessionService.Current.Budget;
            var iLoai = DemandCheckType.DEMAND;
            var loaiChungTu = VoucherTypeSelected != null ? Int32.Parse(VoucherTypeSelected.ValueItem) : 1;
            var listChungTu = _sktChungTuService
                .FindChungTuIndexByCondition(iLoai, yearOfWork, yearOfBudget, budgetSource, loaiChungTu, null,
                    _sessionService.Current.Principal, "sp_skt_nhap_so_nhu_cau").ToList();
            if (BudgetSourceTypeSelected is null)
                return new List<GhiChu>();
            var chungTu = listChungTu.Where(x => x.IIdMaDonVi == _sessionInfo.IdDonVi && (int.Parse(BudgetSourceTypeSelected.ValueItem) == 0 || x.ILoaiNguonNganSach.Value == int.Parse(BudgetSourceTypeSelected.ValueItem))).Select(x => x.Id);
            var chitiets = _sktChungTuChiTietService.FindByCondition(x => chungTu.Contains(x.IIdCtsoKiemTra)).Where(x => !string.IsNullOrEmpty(x.SGhiChu)).Select(x => x.SGhiChu).Distinct().OrderBy(x => x).ToList();
            return chitiets.Select(x => new GhiChu()
            {
                Content = x
            }).ToList();
            ;
        }
        public void OnPrintReportDemandSummaryPhuLuc6(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                if (listIdTongHop == "")
                {
                    this.IsLoading = false;
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var loaiChungTu = 1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportTongHopSoNhuCauPhuLuc6Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopPhuLuc6(loaiChungTu, listIdTongHop, yearOfWork, yearOfBudget, budgetSource, maBQuanLy, donViTinh, loaiNNS).ToList();
                var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                    {
                        ReportTongHopSoNhuCauPhuLuc6Query mlCha = new ReportTongHopSoNhuCauPhuLuc6Query();
                        mlCha.Stt = mlc.SSTT;
                        mlCha.SSTTBC = mlc.SSttBC;
                        mlCha.BHangCha = mlc.BHangCha;
                        mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                        mlCha.iID_MLSKT = mlc.IIDMLSKT;
                        mlCha.SL = mlc.SL;
                        mlCha.SK = mlc.SK;
                        mlCha.SM = mlc.SM;
                        mlCha.SNG = mlc.SNg;
                        mlCha.SKyHieu = mlc.SKyHieu;
                        mlCha.SMoTa = mlc.SMoTa;
                        listDataDonVi.Add(mlCha);
                    }
                }
                CalculateDataNC3Y(listDataDonVi);

                Dictionary<string, object> data = new Dictionary<string, object>();
                AddChuKy(data, _typeChuky);
                AddNgayDiaDiem(data);
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongNhuCau) * donViTinh);
                data.Add("TongDuToan", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.DuToan));
                data.Add("TongUocThucHien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien));
                data.Add("TongNhuCauNam1", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1));
                data.Add("TongSSNam1", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien) : 0);
                data.Add("TongNhuCauNam2", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2));
                data.Add("TongSSNam2", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) : 0);
                data.Add("TongNhuCauNam3", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam3));
                data.Add("TongSSNam3", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam3) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) : 0);
                data.Add("TONGTONGNHUCAU", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongNhuCau));
                data.Add("FormatNumber", formatNumber);
                listDataDonVi = listDataDonVi.Where(x => x.HasData && ((!x.SKyHieu.StartsWith("1") && ((x.SKyHieu.StartsWith("2") && x.SKyHieu.Length < 9) || !x.SKyHieu.StartsWith("2"))) || x.SKyHieu == "1")).OrderBy(x => x.SSTTBC).ToList();
                foreach (var item in listDataDonVi)
                {
                    if (item.SKyHieu.Length == 8)
                    {
                        item.BHangCha = false;
                    }
                }
                data.Add("ListData", listDataDonVi);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("NamNay", yearOfWork);
                data.Add("NamSau", yearOfWork + 1);
                data.Add("NamSau2", yearOfWork + 2);
                data.Add("NamTruoc", yearOfWork - 1);

                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC6));
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc6Query>(templateFileName, data);
                e.Result = new ExportResult("Tổng hợp nhu cầu chi ngân sách", fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandSummaryPhuLuc4(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                if (listIdTongHop == "")
                {
                    this.IsLoading = false;
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                var loaiChungTu = 2;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var loai = DemandCheckType.DEMAND;
                var maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportTongHopSoNhuCauPhuLuc4Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopPhuLuc4(loaiChungTu, listIdTongHop, yearOfWork, yearOfBudget, budgetSource, maBQuanLy, donViTinh, loaiNNS).Where(x => (BudgetTypeSelected.ValueItem == "1" && x.SKyHieu.StartsWith("1")) || (BudgetTypeSelected.ValueItem != "1" && !x.SKyHieu.StartsWith("1"))).ToList();
                var lstIdMucLuc = listDataDonVi.Select(x => x.iID_MLSKT).Distinct().ToList();
                List<NsSktMucLuc> sktMucLucs = FindListParent2CapMucLucByChild(lstIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!lstIdMucLuc.Contains(mlc.IIDMLSKT))
                    {
                        ReportTongHopSoNhuCauPhuLuc4Query mlCha = new ReportTongHopSoNhuCauPhuLuc4Query();
                        mlCha.Stt = mlc.SSTT;
                        mlCha.SSTTBC = mlc.SSttBC;
                        mlCha.BHangCha = mlc.BHangCha;
                        mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                        mlCha.iID_MLSKT = mlc.IIDMLSKT;
                        mlCha.SL = mlc.SL;
                        mlCha.SK = mlc.SK;
                        mlCha.SM = mlc.SM;
                        mlCha.SNG = mlc.SNg;
                        mlCha.SKyHieu = mlc.SKyHieu;
                        mlCha.SMoTa = mlc.SMoTa;
                        mlCha.Rank = mlc.Rank;
                        listDataDonVi.Add(mlCha);
                    }
                }
                CalculateData(listDataDonVi);
                listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                int stt = 1;
                foreach (var item in listDataDonVi)
                {
                    item.TongSo = item.SoNganhPhanCap + item.DacThu;
                    var diff = item.SKyHieu.StartsWith("1") ? item.TongSo - item.KhungNganSachDuocDuyet : item.TongSo - item.DuToanDacThuNamTruoc;
                    item.Tang = diff > 0 ? diff : 0;
                    item.Giam = diff < 0 ? -diff : 0;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                AddChuKy(data, _typeChuky);
                AddNgayDiaDiem(data);
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongSo) * donViTinh);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", listDataDonVi);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("NamNay", yearOfWork);
                data.Add("NamTruoc", yearOfWork - 1);
                var ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);

                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(BudgetTypeSelected.ValueItem == "1" ? ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC4 : ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC4_NSNN));
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc4Query, GhiChu>(templateFileName, data);
                e.Result = new ExportResult("Tổng hợp nhu cầu chi ngân sách(Chi đặc thù)", fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandSummaryPhuLuc5(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                string listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                if (listIdTongHop == "")
                {
                    this.IsLoading = false;
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                var loaiChungTu = 2;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var loai = DemandCheckType.DEMAND;
                var maBQuanLy = _bQuanLySelected != null ? _bQuanLySelected.ValueItem : "0";
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportTongHopSoNhuCauPhuLuc5Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauTongHopPhuLuc5(loaiChungTu, listIdTongHop, yearOfWork, yearOfBudget, budgetSource, maBQuanLy, donViTinh, loaiNNS).ToList();
                var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                foreach (var mlc in sktMucLucs)
                {
                    if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                    {
                        ReportTongHopSoNhuCauPhuLuc5Query mlCha = new ReportTongHopSoNhuCauPhuLuc5Query();
                        mlCha.Stt = mlc.SSTT;
                        mlCha.SSTTBC = mlc.SSttBC;
                        mlCha.BHangCha = mlc.BHangCha;
                        mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                        mlCha.iID_MLSKT = mlc.IIDMLSKT;
                        mlCha.SL = mlc.SL;
                        mlCha.SK = mlc.SK;
                        mlCha.SM = mlc.SM;
                        mlCha.SNG = mlc.SNg;
                        mlCha.SKyHieu = mlc.SKyHieu;
                        mlCha.SMoTa = mlc.SMoTa;
                        listDataDonVi.Add(mlCha);
                    }
                }
                listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                CalculateData(listDataDonVi);
                foreach (var item in listDataDonVi)
                {
                    item.TongSo = item.HuyDongTonKho + item.MuaHangCapHienVat + item.TonKhoDenNgay;
                    var tang = item.MuaHangCapHienVat - item.SoKiemTraMHHVNamTruoc;
                    var giam = item.SoKiemTraMHHVNamTruoc - item.MuaHangCapHienVat;
                    item.Tang = tang > 0 ? tang : 0;
                    item.Giam = giam > 0 ? giam : 0;
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                AddChuKy(data, _typeChuky);
                AddNgayDiaDiem(data);
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongSo));
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", listDataDonVi);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DonVi", _sessionInfo.TenDonVi);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("NamNay", yearOfWork);
                data.Add("NamTruoc", yearOfWork - 1);
                var ghiChu = GetGhiChu();
                data.Add("HasGhiChu", ghiChu.Any());
                data.Add("ListGhiChu", ghiChu);

                string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC5));
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc5Query, GhiChu>(templateFileName, data);
                e.Result = new ExportResult("Tổng hợp nhu cầu chi ngân sách(Mua hàng cung ứng cấp hiện vật cho các đơn vị)", fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandDetailPhuLuc3(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                foreach (var dv in lstDonViSelected)
                {
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = 1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauChiTietPhuLuc3(loaiChungTu, dv.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).Where(x => (BudgetTypeSelected.ValueItem == "1" && x.SKyHieu.StartsWith("1")) || (BudgetTypeSelected.ValueItem != "1" && !x.SKyHieu.StartsWith("1"))).ToList();

                    if (int.Parse(_catUnitTypeSelected.ValueItem) != 1)
                    {
                        foreach (var prop in typeof(ReportTongHopSoNhuCauPhuLuc3Query).GetProperties())
                        {
                            if (prop.PropertyType.Name == "Double")
                            {
                                listDataDonVi.ForEach(x => prop.SetValue(x, Math.Round(Convert.ToDouble(prop.GetValue(x)))));
                            }
                        }
                    }
                    var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);

                    foreach (var mlc in sktMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                        {
                            ReportTongHopSoNhuCauPhuLuc3Query mlCha = new ReportTongHopSoNhuCauPhuLuc3Query();
                            mlCha.Stt = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                            mlCha.iID_MLSKT = mlc.IIDMLSKT;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            mlCha.SM = mlc.SM;
                            mlCha.SNG = mlc.SNg;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            listDataDonVi.Add(mlCha);
                        }
                    }

                    CalculateData(listDataDonVi);
                    foreach (var item in listDataDonVi)
                    {
                        item.TongSo = item.HuyDongTonKho + item.TuChi;
                        var diff = item.SKyHieu.StartsWith("1") ? item.TuChi - item.SoKiemTraNamTruoc : item.TuChi - item.DuToanDauNam;
                        item.Tang = diff > 0 ? diff : 0;
                        item.Giam = diff < 0 ? -diff : 0;
                    }

                    listDataDonVi = listDataDonVi.Where(x => x.HasData).OrderBy(x => x.SSTTBC).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    AddChuKy(data, _typeChuky);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongSo) * donViTinh);
                    data.Add("ListData", listDataDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonVi", dv.NameItem);
                    data.Add("h1", h1);
                    data.Add("h2", dv.NameItem);
                    data.Add("NamNay", yearOfWork);
                    data.Add("NamTruoc", yearOfWork - 1);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DiaDiem", _diaDiem);
                    var ghiChu = listDataDonVi.Where(x => !string.IsNullOrEmpty(x.SGhiChu)).OrderBy(x => x.SKyHieu).Select(x => new GhiChu()
                    {
                        Content = x.SGhiChu
                    }).Distinct().ToList();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);
                    data.Add("IsTongHop", false);

                    string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(BudgetTypeSelected.ValueItem == "1" ? ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC3_NSQP : ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC3_NSNN));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + dv.NameItem;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc3Query, GhiChu>(templateFileName, data);
                    results.Add(new ExportResult("Tổng hợp nhu cầu chi ngân sách " + dv.NameItem, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandDetailPhuLuc6(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                foreach (var dv in lstDonViSelected)
                {
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = 1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportTongHopSoNhuCauPhuLuc6Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauChiTietPhuLuc6(loaiChungTu, dv.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);

                    foreach (var mlc in sktMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                        {
                            ReportTongHopSoNhuCauPhuLuc6Query mlCha = new ReportTongHopSoNhuCauPhuLuc6Query();
                            mlCha.Stt = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                            mlCha.iID_MLSKT = mlc.IIDMLSKT;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            mlCha.SM = mlc.SM;
                            mlCha.SNG = mlc.SNg;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            listDataDonVi.Add(mlCha);
                        }
                    }

                    CalculateDataNC3Y(listDataDonVi);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    AddChuKy(data, _typeChuky);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongNhuCau) * donViTinh);
                    data.Add("TongDuToan", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.DuToan));
                    data.Add("TongUocThucHien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien));
                    data.Add("TongNhuCauNam1", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1));
                    data.Add("TongSSNam1", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.UocThucHien) : 0);
                    data.Add("TongNhuCauNam2", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2));
                    data.Add("TongSSNam2", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam1) : 0);
                    data.Add("TongNhuCauNam3", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam3));
                    data.Add("TongSSNam3", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) != 0 ? listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam3) * 100 / listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.NhuCauNam2) : 0);
                    data.Add("TONGTONGNHUCAU", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongNhuCau));
                    listDataDonVi = listDataDonVi.Where(x => x.HasData && ((!x.SKyHieu.StartsWith("1") && ((x.SKyHieu.StartsWith("2") && x.SKyHieu.Length < 9) || !x.SKyHieu.StartsWith("2"))) || x.SKyHieu == "1")).OrderBy(x => x.SSTTBC).ToList();
                    foreach (var item in listDataDonVi)
                    {
                        if (item.SKyHieu.Length == 8)
                        {
                            item.BHangCha = false;
                        }
                    }
                    data.Add("ListData", listDataDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonVi", dv.NameItem);
                    data.Add("h1", h1);
                    data.Add("h2", dv.NameItem);
                    data.Add("NamNay", yearOfWork);
                    data.Add("NamSau", yearOfWork + 1);
                    data.Add("NamSau2", yearOfWork + 2);
                    data.Add("NamTruoc", yearOfWork - 1);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DiaDiem", _diaDiem);

                    string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC6));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + dv.NameItem;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc6Query>(templateFileName, data);
                    results.Add(new ExportResult("Tổng hợp nhu cầu chi ngân sách " + dv.NameItem, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandDetailPhuLuc4(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                foreach (var dv in lstDonViSelected)
                {
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = 2;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportTongHopSoNhuCauPhuLuc4Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauChiTietPhuLuc4(loaiChungTu, dv.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    var lstIdMucLuc = listDataDonVi.Select(x => x.iID_MLSKT).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParent2CapMucLucByChild(lstIdMucLuc);
                    foreach (var mlc in sktMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.IIDMLSKT))
                        {
                            ReportTongHopSoNhuCauPhuLuc4Query mlCha = new ReportTongHopSoNhuCauPhuLuc4Query();
                            mlCha.Stt = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                            mlCha.iID_MLSKT = mlc.IIDMLSKT;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            mlCha.SM = mlc.SM;
                            mlCha.SNG = mlc.SNg;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            mlCha.Rank = mlc.Rank;
                            listDataDonVi.Add(mlCha);
                        }
                    }
                    CalculateData(listDataDonVi);
                    listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                    int stt = 1;
                    foreach (var item in listDataDonVi)
                    {
                        item.TongSo = item.SoNganhPhanCap + item.DacThu;
                        var diff = item.SKyHieu.StartsWith("1") ? item.TongSo - item.KhungNganSachDuocDuyet : item.TongSo - item.DuToanDacThuNamTruoc;
                        item.Tang = diff > 0 ? diff : 0;
                        item.Giam = diff < 0 ? -diff : 0;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    AddChuKy(data, _typeChuky);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.TongSo) * donViTinh);
                    data.Add("ListData", listDataDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonVi", dv.NameItem);
                    data.Add("h1", h1);
                    data.Add("h2", dv.NameItem);
                    data.Add("NamNay", yearOfWork);
                    data.Add("NamTruoc", yearOfWork - 1);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DiaDiem", _diaDiem);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(BudgetTypeSelected.ValueItem == "1" ? ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC4 : ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC4_NSNN));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + dv.NameItem;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc4Query, GhiChu>(templateFileName, data);
                    results.Add(new ExportResult("Tổng hợp nhu cầu chi ngân sách(Chi đặc thù) " + dv.NameItem, fileNameWithoutExtension, null, xlsFile));
                }

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void OnPrintReportDemandDetailPhuLuc5(ExportType exportType)
        {
            string listIdTongHop = "";
            listIdTongHop = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
            if (listIdTongHop == "")
            {
                MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                return;
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<CheckBoxItem> lstDonViSelected = ListDonVi.Where(item => item.IsChecked).ToList();
                foreach (var dv in lstDonViSelected)
                {
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = 2;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    var loai = DemandCheckType.DEMAND;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ReportTongHopSoNhuCauPhuLuc5Query> listDataDonVi = _sktChungTuChiTietService.FindReportSoNhuCauChiTietPhuLuc5(loaiChungTu, dv.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    var lstIdMucLuc = listDataDonVi.Select(x => x.SKyHieu).Distinct().ToList();
                    List<NsSktMucLuc> sktMucLucs = FindListParentMucLucByChild(lstIdMucLuc);
                    foreach (var mlc in sktMucLucs)
                    {
                        if (!lstIdMucLuc.Contains(mlc.SKyHieu))
                        {
                            ReportTongHopSoNhuCauPhuLuc5Query mlCha = new ReportTongHopSoNhuCauPhuLuc5Query();
                            mlCha.Stt = mlc.SSTT;
                            mlCha.SSTTBC = mlc.SSttBC;
                            mlCha.BHangCha = mlc.BHangCha;
                            mlCha.iID_MLSKTCha = mlc.IIDMLSKTCha;
                            mlCha.iID_MLSKT = mlc.IIDMLSKT;
                            mlCha.SL = mlc.SL;
                            mlCha.SK = mlc.SK;
                            mlCha.SM = mlc.SM;
                            mlCha.SNG = mlc.SNg;
                            mlCha.SKyHieu = mlc.SKyHieu;
                            mlCha.SMoTa = mlc.SMoTa;
                            listDataDonVi.Add(mlCha);
                        }
                    }
                    listDataDonVi = listDataDonVi.OrderBy(x => x.SSTTBC).ToList();
                    CalculateData(listDataDonVi);
                    foreach (var item in listDataDonVi)
                    {
                        item.TongSo = item.HuyDongTonKho + item.MuaHangCapHienVat + item.TonKhoDenNgay;
                        var tang = item.MuaHangCapHienVat - item.SoKiemTraMHHVNamTruoc;
                        var giam = item.SoKiemTraMHHVNamTruoc - item.MuaHangCapHienVat;
                        item.Tang = tang > 0 ? tang : 0;
                        item.Giam = giam > 0 ? giam : 0;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    AddChuKy(data, _typeChuky);
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("TongSoTien", listDataDonVi.Where(x => x.BHangCha.HasValue && !x.BHangCha.Value).Sum(x => x.MuaHangCapHienVat) * donViTinh);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataDonVi);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DonVi", dv.NameItem);
                    data.Add("h1", h1);
                    data.Add("h2", dv.NameItem);
                    data.Add("NamNay", yearOfWork);
                    data.Add("NamTruoc", yearOfWork - 1);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("DiaDiem", _diaDiem);
                    var ghiChu = GetGhiChu();
                    data.Add("HasGhiChu", ghiChu.Any());
                    data.Add("ListGhiChu", ghiChu);

                    string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_SNC_TONGHOP_PHULUC5));
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName) + "_" + dv.NameItem;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportTongHopSoNhuCauPhuLuc5Query, GhiChu>(templateFileName, data);
                    results.Add(new ExportResult("Tổng hợp nhu cầu chi ngân sách(Mua hàng cung ứng cấp hiện vật cho các đơn vị) " + dv.NameItem, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public void ReportDemandDetail(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var lstDonVi = _nsDonViService.FindAll().Where(n => n.NamLamViec == _sessionService.Current.YearOfWork);
                if (KhoiSelected != null && !_khoiSelected.ValueItem.Equals(TypeKhoi.TAT_CA.ToString()))
                {
                    lstDonVi = lstDonVi.Where(x => !string.IsNullOrEmpty(x.Khoi) && x.Khoi.Equals(KhoiSelected.ValueItem));
                }
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                foreach (var item in _listNNganh.Where(x => x.IsChecked))
                {
                    List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> listData = _sktChungTuChiTietService.FindReportPhanBoChiTietMucLucDonVi(item.ValueItem, yearOfWork, yearOfBudget, budgetSource, donViTinh).ToList();
                    if (!listData.IsEmpty())
                    {
                        listData.ForEach(x =>
                        {
                            if (x.LoaiNamNay == 0)
                            {
                                x.IIdMlskt = listData.Any(y => y.IIdMlsktCha.Equals(x.IIdMlsktCha) && y.LoaiNamNay == 1) ? listData.FirstOrDefault(y => y.IIdMlsktCha.Equals(x.IIdMlsktCha) && y.LoaiNamNay == 1).IIdMlskt : x.IIdMlskt;
                            }
                        });
                        listData = listData.Where(x => lstDonVi.Select(x => x.IIDMaDonVi).Contains(x.IdDonVi) || (x.IdDonVi.IsEmpty() && x.BHangCha.GetValueOrDefault(false))).ToList();
                        if (!listData.Any(x => !x.BHangCha.GetValueOrDefault(false)))
                        {
                            listData = new List<ReportPhanBoKiemTraTheoNganhPhuLucQuery>();
                        }
                    }
                    var dataGroups = listData.Where(x => !x.BHangCha.GetValueOrDefault(false)).GroupBy(g => new { g.IIdMlskt, g.IIdMlsktCha, g.sNG, g.sNgCha }).Select(s => new ReportPhanBoKiemTraTheoNganhPhuLucQuery()
                    {
                        STT = "*",
                        IIdMlskt = s.Key.IIdMlskt,
                        IIdMlsktCha = s.Key.IIdMlsktCha,
                        BHangCha = true,
                        sMoTa = s.FirstOrDefault().sMoTa,
                        sKyHieu = s.FirstOrDefault().sKyHieu,
                        sNG = s.Key.sNG,
                        sNgCha = s.Key.sNgCha
                    }).ToList();
                    foreach (var itemGroups in dataGroups)
                    {
                        var iIdMlnsNew = Guid.NewGuid();
                        var index = listData.IndexOf(listData.FirstOrDefault(x => x.IIdMlskt.Equals(itemGroups.IIdMlskt) && x.IIdMlsktCha.Equals(itemGroups.IIdMlsktCha) && x.sNG.Equals(itemGroups.sNG) && x.sNgCha.Equals(itemGroups.sNgCha)));
                        listData.Where(x => x.IIdMlskt.Equals(itemGroups.IIdMlskt) && x.IIdMlsktCha.Equals(itemGroups.IIdMlsktCha)).ForAll(s =>
                        {
                            s.IIdMlsktCha = iIdMlnsNew;
                            s.sMoTa = $"{s.IdDonVi} - {s.STenDonVi}";
                        });
                        itemGroups.IIdMlskt = iIdMlnsNew;
                        listData.Insert(index, itemGroups);
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    var prefixTenNganh = "Ngành: ";
                    var stenNganh = string.Empty;
                    if (IsChuyenNganh)
                    {
                        prefixTenNganh = "Chuyên ngành: ";
                        stenNganh = item.NameItem;
                    }
                    else
                    {
                        stenNganh = item.NameItem;
                    }
                    //listData = listData.Where(x => !x.BHangCha.GetValueOrDefault(false) && (x.TuChi != 0 || x.TuChiSoNhuCau != 0 || x.TuChiSoKiemTraDeXuat != 0 || x.TuChiSoKiemTraNamTruoc != 0)).ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("Count", 10000);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", (_diaDiem != null ? _diaDiem : "") + ", " + DateUtils.FormatDateReport(ReportDate));
                    data.Add("DonVi", _sessionInfo.TenDonVi);
                    data.Add("donViTinh", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : "");
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : "");
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : "");
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : "");
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : "");
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : "");
                    data.Add("TenNganh", prefixTenNganh + stenNganh);
                    data.Add("YearBefore", _sessionInfo.YearOfWork - 1);
                    data.Add("Year", _sessionInfo.YearOfWork);
                    data.Add("IsHasData", listData.IsEmpty());

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_PHAN_BO_PHU_LUC_DON_VI);

                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportPhanBoKiemTraTheoNganhPhuLucQuery>(templateFileName, data);
                    results.Add(new ExportResult(_sessionInfo.TenDonVi, fileNameWithoutExtension, null, xlsFile));
                }

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }


        bool CheckBenhVienTuChu(string iIDMaDonVi)
        {
            var predicateDv = PredicateBuilder.True<DonVi>();
            predicateDv = predicateDv.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicateDv = predicateDv.And(x => x.ITrangThai == 1);
            var lstDonVi = _nsDonViService.FindByCondition(predicateDv);
            var donVi = lstDonVi.FirstOrDefault(x => x.IIDMaDonVi == iIDMaDonVi);
            return donVi != null && donVi.Loai.Equals("1") && !string.IsNullOrEmpty(donVi.Khoi) && donVi.Khoi.Equals("3");
        }

        private void OnPrintReportPhanBoTheoDonViExcel(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;

                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var loai = DemandCheckType.CHECK;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    NsSktChungTu chungTu;
                    var predicate = PredicateBuilder.True<NsSktChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                    predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
                    predicate = predicate.And(x => x.ILoai == loai);
                    chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
                    var chungTuModel = _mapper.Map<NsSktChungTuModel>(chungTu);
                    foreach (var dv in listIdDonVi)
                    {
                        if (CheckBenhVienTuChu(dv))
                        {
                            List<ReportPhanBoSoKiemTraDonViQuery> listData = _sktChungTuChiTietService.FindReportPhanBoSoKiemTraDonVi(dv, chungTuModel.Id.ToString(), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, DemandCheckType.CORPORATIZED_HOSPITAL, loaiNNS).ToList();
                            DonVi donVi = ListUnit.Where(item => item.IIDMaDonVi.Equals(dv)).FirstOrDefault();
                            CalculateData(listData);
                            listData = listData.Where(item =>
                                Math.Abs(item.TuChi) >= double.Epsilon || Math.Abs(item.HuyDong) >= double.Epsilon ||
                                Math.Abs(item.MuaHangHienVat) >= double.Epsilon || Math.Abs(item.DacThu) >= double.Epsilon).ToList();
                            foreach (var item in listData)
                            {
                                item.TongCongNSSD = item.TuChi + item.HuyDong;
                                item.TongCongNSBD = item.MuaHangHienVat + item.DacThu;
                            }
                            //NSSD
                            double SumTotalHuyDong = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                            double SumTotalTuChi = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                            double SumTotalTongCongNSSD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSSD);

                            //NSBD
                            double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                            double SumTotalDacThu = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.DacThu);
                            double SumTotalTongCongNSBD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSBD);

                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", listData);
                            data.Add("Count", 10000);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("TenDonVi", donVi != null ? donVi.TenDonVi : "");
                            data.Add("h1", h1);
                            data.Add("h2", donVi != null ? donVi.TenDonVi : "");
                            data.Add("SumTotalHuyDong", SumTotalHuyDong);
                            data.Add("SumTotalTuChi", SumTotalTuChi);
                            data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                            data.Add("SumTotalDacThu", SumTotalDacThu);
                            data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                            data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                            data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                            data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));
                            AddChuKy(data, _typeChuky);

                            string fileNamePrefix;
                            if (loaiChungTu == 1)
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSSD));
                                }
                                else
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSSD));
                                }
                            }
                            else
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSBD));
                                }
                                else
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSBD));
                                }
                            }
                            fileNamePrefix = dv + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoSoKiemTraDonViQuery>(templateFileName, data);
                            results.Add(new ExportResult(donVi != null ? donVi.TenDonVi : "", fileNameWithoutExtension, null, xlsFile));
                        }
                        else
                        {
                            List<ReportPhanBoSoKiemTraDonViQuery> listData = _sktChungTuChiTietService.FindReportPhanBoSoKiemTraDonVi(dv, chungTuModel.Id.ToString(), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, DemandCheckType.DISTRIBUTION, loaiNNS).ToList();
                            DonVi donVi = ListUnit.Where(item => item.IIDMaDonVi.Equals(dv)).FirstOrDefault();
                            CalculateData(listData);
                            listData = listData.Where(item =>
                                Math.Abs(item.TuChi) >= double.Epsilon || Math.Abs(item.HuyDong) >= double.Epsilon ||
                                Math.Abs(item.MuaHangHienVat) >= double.Epsilon || Math.Abs(item.DacThu) >= double.Epsilon).ToList();
                            foreach (var item in listData)
                            {
                                item.TongCongNSSD = item.TuChi + item.HuyDong;
                                item.TongCongNSBD = item.MuaHangHienVat + item.DacThu;
                            }
                            //NSSD
                            double SumTotalHuyDong = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                            double SumTotalTuChi = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                            double SumTotalTongCongNSSD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSSD);

                            //NSBD
                            double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                            double SumTotalDacThu = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.DacThu);
                            double SumTotalTongCongNSBD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSBD);

                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", listData);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("TenDonVi", donVi != null ? donVi.TenDonVi : "");
                            data.Add("h1", h1);
                            data.Add("h2", donVi != null ? donVi.TenDonVi : "");
                            data.Add("SumTotalHuyDong", SumTotalHuyDong);
                            data.Add("SumTotalTuChi", SumTotalTuChi);
                            data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                            data.Add("SumTotalDacThu", SumTotalDacThu);
                            data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                            data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                            data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                            data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));
                            AddChuKy(data, _typeChuky);

                            string fileNamePrefix;
                            if (loaiChungTu == 1)
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSSD));
                                }
                                else
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSSD));
                                }
                            }
                            else
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSBD));
                                }
                                else
                                {
                                    templateFileName = GetTemplateExcel(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSBD));
                                }
                            }
                            fileNamePrefix = dv + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoSoKiemTraDonViQuery>(templateFileName, data);
                            results.Add(new ExportResult(donVi != null ? donVi.TenDonVi : "", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void OnPrintReportPhanBoTheoDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;

                    List<string> listIdDonVi = ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    //if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    //else TxtTitleThird = "";

                    var loai = DemandCheckType.CHECK;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();

                    NsSktChungTu chungTu;
                    var predicate = PredicateBuilder.True<NsSktChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                    predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
                    predicate = predicate.And(x => x.ILoai == loai);
                    chungTu = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
                    var chungTuModel = _mapper.Map<NsSktChungTuModel>(chungTu);
                    foreach (var dv in listIdDonVi)
                    {
                        if (CheckBenhVienTuChu(dv))
                        {
                            List<ReportPhanBoSoKiemTraDonViQuery> listData = _sktChungTuChiTietService.FindReportPhanBoSoKiemTraDonVi(dv, chungTuModel.Id.ToString(), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, DemandCheckType.CORPORATIZED_HOSPITAL, loaiNNS).ToList();
                            DonVi donVi = ListUnit.Where(item => item.IIDMaDonVi.Equals(dv)).FirstOrDefault();
                            CalculateData(listData);
                            listData = listData.Where(item =>
                                Math.Abs(item.TuChi) >= double.Epsilon || Math.Abs(item.HuyDong) >= double.Epsilon ||
                                Math.Abs(item.MuaHangHienVat) >= double.Epsilon || Math.Abs(item.DacThu) >= double.Epsilon).ToList();
                            foreach (var item in listData)
                            {
                                item.TongCongNSSD = item.TuChi + item.HuyDong;
                                item.TongCongNSBD = item.MuaHangHienVat + item.DacThu;
                            }
                            //NSSD
                            double SumTotalHuyDong = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                            double SumTotalTuChi = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                            double SumTotalTongCongNSSD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSSD);

                            //NSBD
                            double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                            double SumTotalDacThu = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.DacThu);
                            double SumTotalTongCongNSBD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSBD);

                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", listData);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("TenDonVi", donVi != null ? donVi.TenDonVi : "");
                            data.Add("h1", h1);
                            data.Add("h2", donVi != null ? donVi.TenDonVi : "");
                            data.Add("SumTotalHuyDong", SumTotalHuyDong);
                            data.Add("SumTotalTuChi", SumTotalTuChi);
                            data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                            data.Add("SumTotalDacThu", SumTotalDacThu);
                            data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                            data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                            data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                            data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));
                            AddChuKy(data, _typeChuky);

                            string fileNamePrefix;
                            if (loaiChungTu == 1)
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSSD));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSSD));
                                }
                            }
                            else
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSBD));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSBD));
                                }
                            }
                            fileNamePrefix = dv + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoSoKiemTraDonViQuery>(templateFileName, data);
                            results.Add(new ExportResult(donVi != null ? donVi.TenDonVi : "", fileNameWithoutExtension, null, xlsFile));
                        }
                        else
                        {
                            List<ReportPhanBoSoKiemTraDonViQuery> listData = _sktChungTuChiTietService.FindReportPhanBoSoKiemTraDonVi(dv, chungTuModel.Id.ToString(), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, DemandCheckType.DISTRIBUTION, loaiNNS).ToList();
                            DonVi donVi = ListUnit.Where(item => item.IIDMaDonVi.Equals(dv)).FirstOrDefault();
                            CalculateData(listData);
                            listData = listData.Where(item =>
                                Math.Abs(item.TuChi) >= double.Epsilon || Math.Abs(item.HuyDong) >= double.Epsilon ||
                                Math.Abs(item.MuaHangHienVat) >= double.Epsilon || Math.Abs(item.DacThu) >= double.Epsilon).ToList();
                            foreach (var item in listData)
                            {
                                item.TongCongNSSD = item.TuChi + item.HuyDong;
                                item.TongCongNSBD = item.MuaHangHienVat + item.DacThu;
                            }
                            //NSSD
                            double SumTotalHuyDong = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.HuyDong);
                            double SumTotalTuChi = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TuChi);
                            double SumTotalTongCongNSSD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSSD);

                            //NSBD
                            double SumTotalMuaHangHienVat = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.MuaHangHienVat);
                            double SumTotalDacThu = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.DacThu);
                            double SumTotalTongCongNSBD = listData.Where(item => item.IdParent.IsNullOrEmpty()).Sum(x => x.TongCongNSBD);

                            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                            data.Add("FormatNumber", formatNumber);
                            data.Add("ListData", listData);
                            data.Add("TieuDe1", TxtTitleFirst);
                            data.Add("TieuDe2", TxtTitleSecond);
                            data.Add("TieuDe3", TxtTitleThird);
                            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                            data.Add("Cap1", (itemDanhMuc != null && string.IsNullOrEmpty(_dmChuKy.TenDVBanHanh1)) ? itemDanhMuc.SGiaTri : _dmChuKy.TenDVBanHanh1);
                            data.Add("Cap2", _sessionInfo.TenDonVi);
                            data.Add("DiaDiem", _diaDiem);
                            data.Add("TenDonVi", donVi != null ? donVi.TenDonVi : "");
                            data.Add("h1", h1);
                            data.Add("h2", donVi != null ? donVi.TenDonVi : "");
                            data.Add("SumTotalHuyDong", SumTotalHuyDong);
                            data.Add("SumTotalTuChi", SumTotalTuChi);
                            data.Add("SumTotalMuaHangHienVat", SumTotalMuaHangHienVat);
                            data.Add("SumTotalDacThu", SumTotalDacThu);
                            data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                            data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                            data.Add("SumTotalTextNSSD", StringUtils.NumberToText(SumTotalTongCongNSSD * donViTinh));
                            data.Add("SumTotalTextNSBD", StringUtils.NumberToText(SumTotalTongCongNSBD * donViTinh));
                            AddChuKy(data, _typeChuky);

                            string fileNamePrefix;
                            if (loaiChungTu == 1)
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSSD));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSSD));
                                }
                            }
                            else
                            {
                                if (_paperPrintTypeSelected.ValueItem == "1")
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_TRINHKY_NSBD));
                                }
                                else
                                {
                                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_PHANBO_SOKIEMTRA_DONVI_PHULUC_NSBD));
                                }
                            }
                            fileNamePrefix = dv + "_" + Path.GetFileNameWithoutExtension(templateFileName);
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<ReportPhanBoSoKiemTraDonViQuery>(templateFileName, data);
                            results.Add(new ExportResult(donVi != null ? donVi.TenDonVi : "", fileNameWithoutExtension, null, xlsFile));
                        }
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportTongHopPhanBoOnePaper(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string idDonViString = string.Join(",",
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList());
                    List<string> listIdDonVi =
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var budgetSource = _sessionInfo.Budget;
                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    List<ReportTongHopPhanBoSoKiemTra> listData = new List<ReportTongHopPhanBoSoKiemTra>();

                    if (IsContainBVTCChecked)
                    {
                        listData = _sktChungTuChiTietService
                            .FindReportTongHopPhanBoSoKiemTraBVTC(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    }
                    else
                    {
                        listData = _sktChungTuChiTietService
                            .FindReportTongHopPhanBoSoKiemTra(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    }

                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    string path;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 4) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 2:
                                tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 3:
                                tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 4:
                                tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }

                        List<ReportTongHopPhanBoSoKiemTra> dataDonvi1 = new List<ReportTongHopPhanBoSoKiemTra>();
                        if (IsContainBVTCChecked)
                        {
                            dataDonvi1 = _sktChungTuChiTietService
                                .FindReportTongHopPhanBoSoKiemTraBVTC(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }
                        else
                        {
                            dataDonvi1 = _sktChungTuChiTietService
                                .FindReportTongHopPhanBoSoKiemTra(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }

                        foreach (ReportTongHopPhanBoSoKiemTra item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV1 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV1 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV1 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV2 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV2 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV2 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV3 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV3 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV3 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV4 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV4 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV4 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV5 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV5 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV5 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    foreach (ReportTongHopPhanBoSoKiemTra item in listData)
                    {
                        item.TongTuChiConLai = item.TongTuChi - item.TongTuChiPB;
                        item.TongMuaHangHienVatDacThuConLai = item.TongMuaHangHienVatDacThu - item.TongMuaHangHienVatDacThuPB;
                    }

                    CalculateData(listData);
                    listData = listData.Where(item =>
                            (_paperPrintTypeSelected.ValueItem == "3" && loaiChungTu == 1 && item.SoKiemTraDuocThongBao != 0)
                            || item.TongTuChiPB != 0 || item.TongTuChi != 0 || item.TongTuChiConLai != 0
                            || item.TuChiDV1 != 0
                            || item.TuChiDV2 != 0
                            || item.TuChiDV3 != 0
                            || item.TuChiDV4 != 0
                            || item.TuChiDV5 != 0
                            || item.TuChiDV6 != 0
                            || item.TuChiBanThan != 0
                            || item.TongMuaHangHienVatDacThu != 0
                            || item.TongMuaHangHienVatDacThuConLai != 0
                            || item.TongMuaHangHienVatDacThuPB != 0
                            || item.MuaHangHienVatDV1 != 0
                            || item.MuaHangHienVatDV2 != 0
                            || item.MuaHangHienVatDV3 != 0
                            || item.DacThuDV1 != 0
                            || item.DacThuDV2 != 0
                            || item.DacThuDV3 != 0)
                        .ToList();
                    List<ReportTongHopPhanBoSoKiemTra> listDataSummary = listData.Where(x => x.IIdMlsktCha == Guid.Empty || x.IIdMlsktCha == null).ToList();
                    double SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                    double SumTotalSoKiemTraDuocThongBao = listDataSummary.Sum(x => x.SoKiemTraDuocThongBao);
                    double SumTotalConLai = listDataSummary.Sum(x => x.ConLai);
                    double SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                    double SumTotalTuChiConLai = listDataSummary.Sum(x => x.TongTuChiConLai);
                    double SumTotalTuChiBanThan = listDataSummary.Sum(x => x.TuChiBanThan);
                    double SumTotalTuChiDV1 = listDataSummary.Sum(x => x.TuChiDV1);
                    double SumTotalTuChiDV2 = listDataSummary.Sum(x => x.TuChiDV2);
                    double SumTotalTuChiDV3 = listDataSummary.Sum(x => x.TuChiDV3);
                    double SumTotalTuChiDV4 = listDataSummary.Sum(x => x.TuChiDV4);
                    double SumTotalTuChiDV5 = listDataSummary.Sum(x => x.TuChiDV5);
                    double SumTotalTuChiDV6 = listDataSummary.Sum(x => x.TuChiDV6);
                    double SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                    double SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                    double SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                    double SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                    double SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                    double SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                    double SumTotalDacThuDV1 = listDataSummary.Sum(x => x.DacThuDV1);
                    double SumTotalDacThuDV2 = listDataSummary.Sum(x => x.DacThuDV2);
                    double SumTotalDacThuDV3 = listDataSummary.Sum(x => x.DacThuDV3);
                    foreach (var x in listData)
                    {
                        if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                        {
                            x.sM = "1000";
                        }
                    }
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("TenDV1", tenDonVi1);
                    data.Add("TenDV2", tenDonVi2);
                    data.Add("TenDV3", tenDonVi3);
                    data.Add("TenDV4", tenDonVi4);
                    data.Add("TenDV5", tenDonVi5);
                    data.Add("TenDV6", tenDonVi6);
                    data.Add("SumTotalConLai", SumTotalConLai);
                    data.Add("SumTotalSoKiemTraDuocThongBao", SumTotalSoKiemTraDuocThongBao);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalTuChiPB", SumTotalTuChiPB);
                    data.Add("SumTotalTuChiConLai", SumTotalTuChiConLai);
                    data.Add("SumTotalTuChiBanThan", SumTotalTuChiBanThan);
                    data.Add("SumTotalTuChiDV1", SumTotalTuChiDV1);
                    data.Add("SumTotalTuChiDV2", SumTotalTuChiDV2);
                    data.Add("SumTotalTuChiDV3", SumTotalTuChiDV3);
                    data.Add("SumTotalTuChiDV4", SumTotalTuChiDV4);
                    data.Add("SumTotalTuChiDV5", SumTotalTuChiDV5);
                    data.Add("SumTotalTuChiDV6", SumTotalTuChiDV6);
                    data.Add("SumTotalNSBD", SumTotalNSBD);
                    data.Add("SumTotalNSBDPB", SumTotalNSBDPB);
                    data.Add("SumTotalNSBDConLai", SumTotalNSBDConLai);
                    data.Add("SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1);
                    data.Add("SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2);
                    data.Add("SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3);
                    data.Add("SumTotalDacThuDV1", SumTotalDacThuDV1);
                    data.Add("SumTotalDacThuDV2", SumTotalDacThuDV2);
                    data.Add("SumTotalDacThuDV3", SumTotalDacThuDV3);
                    AddChuKy(data, _typeChuky);

                    if (loaiChungTu == 1)
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_ONEPAPER));
                    }
                    else
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO1_ONEPAPER));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    if (_paperPrintTypeSelected.ValueItem == "2" && loaiChungTu == 1)
                    {
                        if (SelectedKieuGiayIn.ValueItem == "1")
                        {
                            xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data, new List<int> { 9, 10 });
                            xlsFile.SetCellValue("K14", 1);
                            xlsFile.SetCellValue("L14", 2);
                            xlsFile.SetCellValue("M14", 3);
                            xlsFile.SetCellValue("N14", 4);
                            xlsFile.SetCellValue("O14", 5);
                            xlsFile.SetCellValue("J11", 6);
                        }
                        else
                        {
                            xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data, new List<int> { 8, 9 });
                            xlsFile.SetCellValue("J12", 1);
                            xlsFile.SetCellValue("K12", 2);
                            xlsFile.SetCellValue("L12", 3);
                            xlsFile.SetCellValue("M12", 4);
                            xlsFile.SetCellValue("N12", 5);
                            xlsFile.SetCellValue("O12", 6);
                            xlsFile.SetCellValue("P12", 7);
                        }
                    }
                    else
                    {
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data);
                    }
                    results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    int numberPage = 0;
                    var numOfCol = SelectedKieuGiayIn.ValueItem.Equals("1") ? 4 : 6;
                    var numFirstOfCol = SelectedKieuGiayIn.ValueItem.Equals("1") ? 3 : 5;
                    if (loaiChungTu == 1)
                    {
                        numberPage = (listIdDonVi.Count() - numFirstOfCol) / numOfCol;
                        if ((listIdDonVi.Count() - numFirstOfCol) % numOfCol > 0)
                        {
                            numberPage++;
                        }
                    }
                    else
                    {
                        numberPage = (listIdDonVi.Count() - 3) / 3;
                        if ((listIdDonVi.Count() - 3) % 3 > 0)
                        {
                            numberPage++;
                        }
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        tenDonVi6 = string.Empty;
                        if (IsContainBVTCChecked)
                        {
                            listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTraBVTC(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }
                        else
                        {
                            listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTra(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }

                        int sttPage = p - 2;
                        int countDonVi = 0;
                        int numberItemInpage = 0;
                        if (loaiChungTu == 1)
                        {
                            numberItemInpage = numOfCol;
                        }
                        else
                        {
                            numberItemInpage = 3;
                        }
                        for (int i = sttPage * numberItemInpage + numFirstOfCol; i <= sttPage * numberItemInpage + numberItemInpage + 4; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 3:
                                    tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 4:
                                    tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 5:
                                    tenDonVi6 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportTongHopPhanBoSoKiemTra> dataDonvi1 = new List<ReportTongHopPhanBoSoKiemTra>();
                            if (IsContainBVTCChecked)
                            {
                                dataDonvi1 = _sktChungTuChiTietService
                               .FindReportTongHopPhanBoSoKiemTraBVTC(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                            }
                            else
                            {
                                dataDonvi1 = _sktChungTuChiTietService
                               .FindReportTongHopPhanBoSoKiemTra(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                            }

                            foreach (ReportTongHopPhanBoSoKiemTra item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV1 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV1 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV1 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV2 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV2 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV2 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV3 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV3 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV3 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV4 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV5 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 5:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV6 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            countDonVi++;
                        }
                        foreach (ReportTongHopPhanBoSoKiemTra item in listData)
                        {
                            item.TongTuChiConLai = item.TongTuChi - item.TongTuChiPB;
                            item.TongMuaHangHienVatDacThuConLai = item.TongMuaHangHienVatDacThu - item.TongMuaHangHienVatDacThuPB;
                        }

                        CalculateData(listData);
                        listData = listData.Where(item =>
                                item.TongTuChiPB != 0 || item.TongTuChi != 0 || item.TongTuChiConLai != 0
                                || item.TuChiDV1 != 0
                                || item.TuChiDV2 != 0
                                || item.TuChiDV3 != 0
                                || item.TuChiDV4 != 0
                                || item.TuChiDV5 != 0
                                || item.TuChiDV6 != 0
                                || item.TuChiBanThan != 0
                                || item.TongMuaHangHienVatDacThu != 0
                                || item.TongMuaHangHienVatDacThuConLai != 0
                                || item.TongMuaHangHienVatDacThuPB != 0
                                || item.MuaHangHienVatDV1 != 0
                                || item.MuaHangHienVatDV2 != 0
                                || item.MuaHangHienVatDV3 != 0
                                || item.DacThuDV1 != 0
                                || item.DacThuDV2 != 0
                                || item.DacThuDV3 != 0)
                            .ToList();
                        listDataSummary = listData.Where(item => item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null).ToList();
                        SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                        SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                        SumTotalTuChiConLai = listDataSummary.Sum(x => x.TongTuChiConLai);
                        SumTotalTuChiBanThan = listDataSummary.Sum(x => x.TuChiBanThan);
                        SumTotalTuChiDV1 = listDataSummary.Sum(x => x.TuChiDV1);
                        SumTotalTuChiDV2 = listDataSummary.Sum(x => x.TuChiDV2);
                        SumTotalTuChiDV3 = listDataSummary.Sum(x => x.TuChiDV3);
                        SumTotalTuChiDV4 = listDataSummary.Sum(x => x.TuChiDV4);
                        SumTotalTuChiDV5 = listDataSummary.Sum(x => x.TuChiDV5);
                        SumTotalTuChiDV6 = listDataSummary.Sum(x => x.TuChiDV6);
                        SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                        SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                        SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                        SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                        SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                        SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                        SumTotalDacThuDV1 = listDataSummary.Sum(x => x.DacThuDV1);
                        SumTotalDacThuDV2 = listDataSummary.Sum(x => x.DacThuDV2);
                        SumTotalDacThuDV3 = listDataSummary.Sum(x => x.DacThuDV3);
                        foreach (var x in listData)
                        {
                            if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                            {
                                x.sM = "1000";
                            }
                        }
                        data = new Dictionary<string, object>();
                        data.Add("FormatNumber", formatNumber);
                        data.Add("ListData", listData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                        data.Add("Cap2", _sessionInfo.TenDonVi);
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("h1", h1);
                        data.Add("h2", _sessionInfo.TenDonVi);
                        data.Add("TenDV1", tenDonVi1);
                        data.Add("TenDV2", tenDonVi2);
                        data.Add("TenDV3", tenDonVi3);
                        data.Add("TenDV4", tenDonVi4);
                        data.Add("TenDV5", tenDonVi5);
                        data.Add("TenDV6", tenDonVi6);
                        data.Add("SumTotalTuChi", SumTotalTuChi);
                        data.Add("SumTotalTuChiPB", SumTotalTuChiPB);
                        data.Add("SumTotalTuChiConLai", SumTotalTuChiConLai);
                        data.Add("SumTotalTuChiBanThan", SumTotalTuChiBanThan);
                        data.Add("SumTotalTuChiDV1", SumTotalTuChiDV1);
                        data.Add("SumTotalTuChiDV2", SumTotalTuChiDV2);
                        data.Add("SumTotalTuChiDV3", SumTotalTuChiDV3);
                        data.Add("SumTotalTuChiDV4", SumTotalTuChiDV4);
                        data.Add("SumTotalTuChiDV5", SumTotalTuChiDV5);
                        data.Add("SumTotalTuChiDV6", SumTotalTuChiDV6);
                        data.Add("SumTotalNSBD", SumTotalNSBD);
                        data.Add("SumTotalNSBDPB", SumTotalNSBDPB);
                        data.Add("SumTotalNSBDConLai", SumTotalNSBDConLai);
                        data.Add("SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1);
                        data.Add("SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2);
                        data.Add("SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3);
                        data.Add("SumTotalDacThuDV1", SumTotalDacThuDV1);
                        data.Add("SumTotalDacThuDV2", SumTotalDacThuDV2);
                        data.Add("SumTotalDacThuDV3", SumTotalDacThuDV3);
                        AddChuKy(data, _typeChuky);

                        if (loaiChungTu == 1)
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO2_ONEPAPER));
                        }
                        else
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO2_ONEPAPER));
                        }
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data);
                        results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ " + p, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportTongHopPhanBoOnePaper_Excel(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    //typeClone
                    bool typeClone = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;
                    List<CheckBoxItem> lstDonViPage1 = new List<CheckBoxItem>();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    string idDonViString = string.Join(",",
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList());
                    List<string> listIdDonVi =
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    List<ReportTongHopPhanBoSoKiemTra> listData = new List<ReportTongHopPhanBoSoKiemTra>();

                    // create Header agencies
                    List<HeaderReportDynamic> ListHeader1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeaderPage1 = new List<HeaderReportDynamic>();
                    List<HeaderReportDynamic> ListHeader2 = new List<HeaderReportDynamic>();
                    List<DataReportDynamic> ListDataDefault = new List<DataReportDynamic>();
                    int columnStart = 13;
                    int columnStartPage1 = 12;
                    var ColNameStart = GetExcelColumnName(columnStart);
                    var ColNameStartPage1 = GetExcelColumnName(columnStartPage1);
                    int columnEnd = columnStart + (listIdDonVi.Count > 1 ? listIdDonVi.Count - 1 : 0);
                    int columnEndPage1 = columnStartPage1 + (listIdDonVi.Count > 1 ? listIdDonVi.Count - 1 : 0);
                    var ColNameEnd = GetExcelColumnName(columnEnd);
                    var ColNameEndPage1 = GetExcelColumnName(columnEndPage1);
                    var mergeRange = string.Format("{0}9:{1}9", ColNameStart, ColNameEnd);
                    var mergeRangePage1 = string.Format("{0}9:{1}9", ColNameStartPage1, ColNameEndPage1);
                    _lstIIdMaDonVi = ListDonVi.Where(x => x.IsChecked).ToList();

                    foreach (var item in _lstIIdMaDonVi.Select((value, index) => new { index, value }))
                    {

                        if (item.index == NSConstants.ZERO)
                        {
                            ListHeader1.Add(new HeaderReportDynamic() { Header = "Chi tiết theo đơn vị trực thuộc", Stt = 1, MergeRange = mergeRange });
                            ListHeaderPage1.Add(new HeaderReportDynamic() { Header = "Chi tiết theo đơn vị trực thuộc", Stt = 1, MergeRange = mergeRangePage1 });
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem, Stt = 1, MergeRange = mergeRange });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                        else
                        {
                            ListHeader1.Add(new HeaderReportDynamic());
                            ListHeaderPage1.Add(new HeaderReportDynamic());
                            ListHeader2.Add(new HeaderReportDynamic() { Header = item.value.DisplayItem });
                            ListDataDefault.Add(new DataReportDynamic());
                        }
                    }
                    _lstDataDynamic = ListDataDefault;
                    if (IsContainBVTCChecked)
                    {
                        listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTraBVTC(string.Join(StringUtils.COMMA, _lstIIdMaDonVi.Select(x => x.ValueItem)), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS, typeClone).ToList();

                    }
                    else
                    {
                        listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTra(string.Join(StringUtils.COMMA, _lstIIdMaDonVi.Select(x => x.ValueItem)), loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS, typeClone).ToList();

                    }

                    var listDataGroups = listData.GroupBy(g => new { g.IIdMlskt, g.IIdMlsktCha, g.sMoTa, g.sKyHieu, g.sM }).Select(x => new ReportTongHopPhanBoSoKiemTra
                    {
                        IIdMlskt = x.FirstOrDefault().IIdMlskt,
                        IIdMlsktCha = x.FirstOrDefault().IIdMlsktCha ?? Guid.Empty,
                        sM = x.FirstOrDefault().sM,
                        sKyHieu = x.FirstOrDefault().sKyHieu,
                        sStt = x.FirstOrDefault().sStt,
                        sMoTa = x.FirstOrDefault().sMoTa,
                        TongTuChi = x.Max(x => x.TongTuChi),
                        TongTuChiPB = x.Sum(x => x.TongTuChiPB),
                        bHangCha = x.FirstOrDefault().bHangCha,
                        TongMuaHangHienVat = x.FirstOrDefault().TongMuaHangHienVat,
                        TongMuaHangHienVatPB = x.FirstOrDefault().TongMuaHangHienVatPB,
                        TongDacThu = x.FirstOrDefault().TongDacThu,
                        TongDacThuPB = x.FirstOrDefault().TongDacThuPB,
                        TongMuaHangHienVatDacThu = x.FirstOrDefault().TongMuaHangHienVatDacThu,
                        TongMuaHangHienVatDacThuPB = x.FirstOrDefault().TongMuaHangHienVatDacThuPB,
                        TongMuaHangHienVatDacThuConLai = x.FirstOrDefault().TongMuaHangHienVatDacThuConLai,
                        TongTuChiConLai = x.Max(x => x.TongTuChi) - x.Sum(x => x.TongTuChiPB),
                        IIdMaDonVi = x.FirstOrDefault().IIdMaDonVi,
                        ListDataValue = GetDataDefault(),
                    }).ToList();

                    CalculateDataExcel(listData, listDataGroups);
                    _lstDataDynamicSummary = GetDataDefault();
                    for (int i = 0; i < _lstDataDynamicSummary.Count; i++)
                    {
                        _lstDataDynamicSummary[i].FVal = listDataGroups.Where(x => !x.bHangCha.GetValueOrDefault(false)).Sum(y => y.ListDataValue[i].FVal);
                    }
                    listDataGroups = listDataGroups.Where(item => CheckListValue(item.ListDataValue) || item.TongTuChi != 0 || (_paperPrintTypeSelected.ValueItem == "3" && loaiChungTu == 1 && item.SoKiemTraDuocThongBao != 0)).ToList();
                    List<ReportTongHopPhanBoSoKiemTra> listDataSummary = listDataGroups.Where(x => x.IIdMlsktCha == Guid.Empty || x.IIdMlsktCha == null).ToList();
                    double SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                    double SumTotalSoKiemTraDuocThongBao = listDataSummary.Sum(x => x.SoKiemTraDuocThongBao);
                    double SumTotalConLai = listDataSummary.Sum(x => x.ConLai);
                    double SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                    double SumTotalTuChiConLai = listDataSummary.Sum(x => x.TongTuChiConLai);
                    double SumTotalTuChiDV1 = listDataSummary.Sum(x => x.TuChiDV1);
                    double SumTotalTuChiDV2 = listDataSummary.Sum(x => x.TuChiDV2);
                    double SumTotalTuChiDV3 = listDataSummary.Sum(x => x.TuChiDV3);
                    double SumTotalTuChiDV4 = listDataSummary.Sum(x => x.TuChiDV4);
                    double SumTotalTuChiDV5 = listDataSummary.Sum(x => x.TuChiDV5);
                    double SumTotalTuChiDV6 = listDataSummary.Sum(x => x.TuChiDV6);
                    double SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                    double SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                    double SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                    double SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                    double SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                    double SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                    double SumTotalDacThuDV1 = listDataSummary.Sum(x => x.DacThuDV1);
                    double SumTotalDacThuDV2 = listDataSummary.Sum(x => x.DacThuDV2);
                    double SumTotalDacThuDV3 = listDataSummary.Sum(x => x.DacThuDV3);
                    foreach (var x in listData)
                    {
                        if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                        {
                            x.sM = "1000";
                        }
                    }
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataGroups);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalConLai", SumTotalConLai);
                    data.Add("SumTotalSoKiemTraDuocThongBao", SumTotalSoKiemTraDuocThongBao);
                    data.Add("SumTotalTuChiPB", SumTotalTuChiPB);
                    data.Add("SumTotalTuChiConLai", SumTotalTuChiConLai);
                    data.Add("SumTotalTuChiDV1", SumTotalTuChiDV1);
                    data.Add("SumTotalTuChiDV2", SumTotalTuChiDV2);
                    data.Add("SumTotalTuChiDV3", SumTotalTuChiDV3);
                    data.Add("SumTotalTuChiDV4", SumTotalTuChiDV4);
                    data.Add("SumTotalTuChiDV5", SumTotalTuChiDV5);
                    data.Add("SumTotalTuChiDV6", SumTotalTuChiDV6);
                    data.Add("SumTotalNSBD", SumTotalNSBD);
                    data.Add("SumTotalNSBDPB", SumTotalNSBDPB);
                    data.Add("SumTotalNSBDConLai", SumTotalNSBDConLai);
                    data.Add("SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1);
                    data.Add("SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2);
                    data.Add("SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3);
                    data.Add("SumTotalDacThuDV1", SumTotalDacThuDV1);
                    data.Add("SumTotalDacThuDV2", SumTotalDacThuDV2);
                    data.Add("SumTotalDacThuDV3", SumTotalDacThuDV3);
                    data.Add("ListDataSummary", _lstDataDynamicSummary);
                    data.Add("ListHeader1", ListHeaderPage1);
                    data.Add("ListHeader2", ListHeader2);
                    data.Add("Count", 10000);
                    AddChuKy(data, _typeChuky);

                    if (loaiChungTu == 1)
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_ONEPAPER_EXCEL));
                    }
                    else
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO1_ONEPAPER));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);

                    if (_paperPrintTypeSelected.ValueItem == "2" && loaiChungTu == 1)
                    {
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra, HeaderReportDynamic, DataReportDynamic>(templateFileName, data, new List<int> { 11, 12 }, true);
                    }
                    else
                    {
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra, HeaderReportDynamic, DataReportDynamic>(templateFileName, data, null, true);
                    }


                    results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    listDataSummary = listDataGroups.Where(item => item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null).ToList();
                    SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                    SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                    SumTotalTuChiConLai = listDataSummary.Sum(x => x.TongTuChiConLai);
                    SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                    SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                    SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                    SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                    SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                    SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                    foreach (var x in listDataGroups)
                    {
                        if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                        {
                            x.sM = "1000";
                        }
                    }
                    data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listDataGroups);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalTuChiPB", SumTotalTuChiPB);
                    data.Add("SumTotalTuChiConLai", SumTotalTuChiConLai);
                    data.Add("SumTotalTuChiDV1", SumTotalTuChiDV1);
                    data.Add("SumTotalTuChiDV2", SumTotalTuChiDV2);
                    data.Add("SumTotalTuChiDV3", SumTotalTuChiDV3);
                    data.Add("SumTotalTuChiDV4", SumTotalTuChiDV4);
                    data.Add("SumTotalTuChiDV5", SumTotalTuChiDV5);
                    data.Add("SumTotalTuChiDV6", SumTotalTuChiDV6);
                    data.Add("SumTotalNSBD", SumTotalNSBD);
                    data.Add("SumTotalNSBDPB", SumTotalNSBDPB);
                    data.Add("SumTotalNSBDConLai", SumTotalNSBDConLai);
                    data.Add("SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1);
                    data.Add("SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2);
                    data.Add("SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3);
                    data.Add("SumTotalDacThuDV1", SumTotalDacThuDV1);
                    data.Add("SumTotalDacThuDV2", SumTotalDacThuDV2);
                    data.Add("SumTotalDacThuDV3", SumTotalDacThuDV3);
                    data.Add("ListDataSummary", _lstDataDynamicSummary);
                    data.Add("ListHeader1", ListHeader1);
                    data.Add("ListHeader2", ListHeader2);
                    data.Add("Count", 10000);
                    AddChuKy(data, _typeChuky);

                    if (loaiChungTu == 1)
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_EXCEL));
                    }
                    else
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1_EXCEL));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra, HeaderReportDynamic, DataReportDynamic>(templateFileName, data, null, true);
                    if (listDataGroups.Any())
                        results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ 2 Excel", fileNameWithoutExtension, null, xlsFile));

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnPrintReportTongHopPhanBo(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    ExcelFile xlsFile;
                    var yearOfWork = _sessionInfo.YearOfWork;
                    var donViCap2 = _nsDonViService.FindByListDonViCap2KhacCha(yearOfWork).Select(x => x.IIDMaDonVi);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    string idDonViString = string.Join(",",
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList());
                    List<string> listIdDonVi =
                        ListDonVi.Where(item => item.IsChecked && !donViCap2.Contains(item.ValueItem)).Select(x => x.ValueItem.ToString()).ToList();
                    int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                    var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                    var yearOfBudget = _sessionInfo.YearOfBudget;
                    var budgetSource = _sessionInfo.Budget;
                    int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                    if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;
                    else TxtTitleThird = "";

                    var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<ReportTongHopPhanBoSoKiemTra> listData = new List<ReportTongHopPhanBoSoKiemTra>();
                    if (IsContainBVTCChecked)
                    {
                        listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTraBVTC(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    }
                    else
                    {
                        listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTra(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                    }

                    //page1
                    string tenDonVi1 = string.Empty;
                    string tenDonVi2 = string.Empty;
                    string tenDonVi3 = string.Empty;
                    string tenDonVi4 = string.Empty;
                    string tenDonVi5 = string.Empty;
                    string tenDonVi6 = string.Empty;
                    string tenDonVi7 = string.Empty;
                    string tenDonVi8 = string.Empty;
                    string tenDonVi9 = string.Empty;
                    string path;
                    for (int i = 0; i < listIdDonVi.Count; i++)
                    {
                        if (i > 4) break;
                        switch (i)
                        {
                            case 0:
                                tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 1:
                                tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 2:
                                tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 3:
                                tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            case 4:
                                tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                break;
                            default:
                                break;
                        }

                        List<ReportTongHopPhanBoSoKiemTra> dataDonvi1 = new List<ReportTongHopPhanBoSoKiemTra>();
                        if (IsContainBVTCChecked)
                        {
                            dataDonvi1 = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTraBVTC(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }
                        else
                        {
                            dataDonvi1 = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTra(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }
                        foreach (ReportTongHopPhanBoSoKiemTra item in dataDonvi1)
                        {
                            switch (i)
                            {
                                case 0:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV1 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV1 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV1 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 1:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV2 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV2 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV2 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 2:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV3 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV3 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV3 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 3:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV4 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV4 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV4 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                case 4:
                                    listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                    {
                                        n.TuChiDV5 = item.TongTuChiPB;
                                        n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                        n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                        n.MuaHangHienVatDV5 = item.TongMuaHangHienVatPB;
                                        n.DacThuDV5 = item.TongDacThuPB;
                                        return n;
                                    }).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    foreach (ReportTongHopPhanBoSoKiemTra item in listData)
                    {
                        item.TongTuChiConLai = item.TongTuChi - item.TongTuChiPB;
                        item.TongMuaHangHienVatDacThuConLai = item.TongMuaHangHienVatDacThu - item.TongMuaHangHienVatDacThuPB;
                    }

                    CalculateData(listData);
                    listData = listData.Where(item =>
                            (_paperPrintTypeSelected.ValueItem == "3" && loaiChungTu == 1 && item.SoKiemTraDuocThongBao != 0)
                            || item.TongTuChiPB != 0 || item.TongTuChi != 0 || item.TongTuChiConLai != 0
                            || item.TuChiDV1 != 0
                            || item.TuChiDV2 != 0
                            || item.TuChiDV3 != 0
                            || item.TuChiDV4 != 0
                            || item.TuChiDV5 != 0
                            || item.TuChiDV6 != 0
                            || item.TongMuaHangHienVatDacThu != 0
                            || item.TongMuaHangHienVatDacThuConLai != 0
                            || item.TongMuaHangHienVatDacThuPB != 0
                            || item.MuaHangHienVatDV1 != 0
                            || item.MuaHangHienVatDV2 != 0
                            || item.MuaHangHienVatDV3 != 0
                            || item.DacThuDV1 != 0
                            || item.DacThuDV2 != 0
                            || item.DacThuDV3 != 0)
                        .ToList();
                    List<ReportTongHopPhanBoSoKiemTra> listDataSummary = listData.Where(x => x.IIdMlsktCha == Guid.Empty || x.IIdMlsktCha == null).ToList();
                    double SumTotalTuChiBanThan = listDataSummary.Sum(x => x.TuChiBanThan);
                    double SumTotalSoKiemTraDuocThongBao = listDataSummary.Sum(x => x.SoKiemTraDuocThongBao);
                    double SumTotalConLai = listDataSummary.Sum(x => x.ConLai);
                    double SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                    double SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                    double SumTotalTuChiConLai = listDataSummary.Sum(x => x.TongTuChiConLai);
                    double SumTotalTuChiDV1 = listDataSummary.Sum(x => x.TuChiDV1);
                    double SumTotalTuChiDV2 = listDataSummary.Sum(x => x.TuChiDV2);
                    double SumTotalTuChiDV3 = listDataSummary.Sum(x => x.TuChiDV3);
                    double SumTotalTuChiDV4 = listDataSummary.Sum(x => x.TuChiDV4);
                    double SumTotalTuChiDV5 = listDataSummary.Sum(x => x.TuChiDV5);
                    double SumTotalTuChiDV6 = listDataSummary.Sum(x => x.TuChiDV6);
                    double SumTotalTuChiDV7 = listDataSummary.Sum(x => x.TuChiDV7);
                    double SumTotalTuChiDV8 = listDataSummary.Sum(x => x.TuChiDV8);
                    double SumTotalTuChiDV9 = listDataSummary.Sum(x => x.TuChiDV9);
                    double SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                    double SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                    double SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                    double SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                    double SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                    double SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                    double SumTotalDacThuDV1 = listDataSummary.Sum(x => x.DacThuDV1);
                    double SumTotalDacThuDV2 = listDataSummary.Sum(x => x.DacThuDV2);
                    double SumTotalDacThuDV3 = listDataSummary.Sum(x => x.DacThuDV3);
                    foreach (var x in listData)
                    {
                        if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                        {
                            x.sM = "1000";
                        }
                        if (x.bHangCha == false)
                            x.sMoTa = "    " + x.sMoTa;
                    }
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("ListData", listData);
                    data.Add("TieuDe1", TxtTitleFirst);
                    data.Add("TieuDe2", TxtTitleSecond);
                    data.Add("TieuDe3", TxtTitleThird);
                    data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                    data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("DiaDiem", _diaDiem);
                    data.Add("h1", h1);
                    data.Add("h2", _sessionInfo.TenDonVi);
                    data.Add("TenDV1", tenDonVi1);
                    data.Add("TenDV2", tenDonVi2);
                    data.Add("TenDV3", tenDonVi3);
                    data.Add("TenDV4", tenDonVi4);
                    data.Add("TenDV5", tenDonVi5);
                    data.Add("SumTotalSoKiemTraDuocThongBao", SumTotalSoKiemTraDuocThongBao);
                    data.Add("SumTotalConLai", SumTotalConLai);
                    data.Add("SumTotalTuChi", SumTotalTuChi);
                    data.Add("SumTotalTuChiPB", SumTotalTuChiPB);
                    data.Add("SumTotalTuChiConLai", SumTotalTuChiConLai);
                    data.Add("SumTotalTuChiBanThan", SumTotalTuChiBanThan);
                    data.Add("SumTotalTuChiDV1", SumTotalTuChiDV1);
                    data.Add("SumTotalTuChiDV2", SumTotalTuChiDV2);
                    data.Add("SumTotalTuChiDV3", SumTotalTuChiDV3);
                    data.Add("SumTotalTuChiDV4", SumTotalTuChiDV4);
                    data.Add("SumTotalTuChiDV5", SumTotalTuChiDV5);
                    data.Add("SumTotalTuChiDV6", SumTotalTuChiDV6);
                    data.Add("SumTotalNSBD", SumTotalNSBD);
                    data.Add("SumTotalNSBDPB", SumTotalNSBDPB);
                    data.Add("SumTotalNSBDConLai", SumTotalNSBDConLai);
                    data.Add("SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1);
                    data.Add("SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2);
                    data.Add("SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3);
                    data.Add("SumTotalDacThuDV1", SumTotalDacThuDV1);
                    data.Add("SumTotalDacThuDV2", SumTotalDacThuDV2);
                    data.Add("SumTotalDacThuDV3", SumTotalDacThuDV3);
                    AddChuKy(data, _typeChuky);

                    if (loaiChungTu == 1)
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO1));
                    }
                    else
                    {
                        templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO1));
                    }
                    fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    if (_paperPrintTypeSelected.ValueItem == "2" && loaiChungTu == 1)
                    {
                        if (SelectedKieuGiayIn.ValueItem == "1")
                        {
                            xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data, new List<int> { 9, 10 });
                            xlsFile.SetCellValue("K14", 1);
                            xlsFile.SetCellValue("L14", 2);
                            xlsFile.SetCellValue("M14", 3);
                            xlsFile.SetCellValue("N14", 4);
                            xlsFile.SetCellValue("O14", 5);
                            xlsFile.SetCellValue("J11", 6);
                        }
                        else
                        {
                            xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data, new List<int> { 8, 9 });
                            xlsFile.SetCellValue("J11", 1);
                            xlsFile.SetCellValue("K11", 2);
                            xlsFile.SetCellValue("L11", 3);
                            xlsFile.SetCellValue("M11", 4);
                            xlsFile.SetCellValue("N11", 5);
                            xlsFile.SetCellValue("O11", 6);
                            xlsFile.SetCellValue("P11", 7);
                        }
                    }
                    else
                    {
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data);
                    }
                    results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ 1", fileNameWithoutExtension, null, xlsFile));

                    //page 2
                    int numberPage = 0;
                    //Doc 4, Ngang 6
                    var numOfCol = SelectedKieuGiayIn.ValueItem.Equals("1") ? 7 : 9;
                    var numFirstOfCol = SelectedKieuGiayIn.ValueItem.Equals("1") ? 3 : 5;
                    if (loaiChungTu == 1)
                    {
                        numberPage = (listIdDonVi.Count() - numFirstOfCol) / numOfCol;
                        if ((listIdDonVi.Count() - numFirstOfCol) % numOfCol > 0)
                        {
                            numberPage++;
                        }
                    }
                    else
                    {
                        numberPage = (listIdDonVi.Count() - 3) / 3;
                        if ((listIdDonVi.Count() - 3) % 3 > 0)
                        {
                            numberPage++;
                        }
                    }

                    for (int p = 2; p < numberPage + 2; p++)
                    {
                        tenDonVi1 = string.Empty;
                        tenDonVi2 = string.Empty;
                        tenDonVi3 = string.Empty;
                        tenDonVi4 = string.Empty;
                        tenDonVi5 = string.Empty;
                        tenDonVi6 = string.Empty;
                        tenDonVi7 = string.Empty;
                        tenDonVi8 = string.Empty;
                        tenDonVi9 = string.Empty;
                        if (IsContainBVTCChecked)
                        {
                            listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTraBVTC(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }
                        else
                        {
                            listData = _sktChungTuChiTietService.FindReportTongHopPhanBoSoKiemTra(idDonViString, loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                        }

                        int sttPage = p - 2;
                        int countDonVi = 0;
                        int numberItemInpage = 0;
                        if (loaiChungTu == 1)
                        {
                            numberItemInpage = numOfCol;
                        }
                        else
                        {
                            numberItemInpage = 3;
                        }
                        for (int i = sttPage * numberItemInpage + numFirstOfCol; i <= sttPage * numberItemInpage + numberItemInpage + 4; i++)
                        {
                            if (i >= listIdDonVi.Count()) break;
                            switch (countDonVi)
                            {
                                case 0:
                                    tenDonVi1 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 1:
                                    tenDonVi2 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 2:
                                    tenDonVi3 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 3:
                                    tenDonVi4 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 4:
                                    tenDonVi5 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 5:
                                    tenDonVi6 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 6:
                                    tenDonVi7 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 7:
                                    tenDonVi8 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                case 8:
                                    tenDonVi9 = ListDonVi.Where(n => n.ValueItem == listIdDonVi[i]).FirstOrDefault().DisplayItem;
                                    break;
                                default:
                                    break;
                            }
                            List<ReportTongHopPhanBoSoKiemTra> dataDonvi1 = new List<ReportTongHopPhanBoSoKiemTra>();
                            if (IsContainBVTCChecked)
                            {
                                dataDonvi1 = _sktChungTuChiTietService
                                .FindReportTongHopPhanBoSoKiemTraBVTC(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                            }
                            else
                            {
                                dataDonvi1 = _sktChungTuChiTietService
                           .FindReportTongHopPhanBoSoKiemTra(listIdDonVi[i], loaiChungTu, yearOfWork, yearOfBudget, budgetSource, donViTinh, loaiNNS).ToList();
                            }

                            foreach (ReportTongHopPhanBoSoKiemTra item in dataDonvi1)
                            {
                                switch (countDonVi)
                                {
                                    case 0:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV1 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV1 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV1 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 1:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV2 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV2 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV2 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 2:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV3 = item.TongTuChiPB;
                                            n.TongMuaHangHienVatDacThu = item.TongMuaHangHienVat + item.TongDacThu;
                                            n.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatPB + item.TongDacThuPB;
                                            n.MuaHangHienVatDV3 = item.TongMuaHangHienVatPB;
                                            n.DacThuDV3 = item.TongDacThuPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 3:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV4 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 4:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV5 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 5:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV6 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 6:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV7 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 7:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV8 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    case 8:
                                        listData.Where(n => n.IIdMlskt == item.IIdMlskt).Select(n =>
                                        {
                                            n.TuChiDV9 = item.TongTuChiPB;
                                            return n;
                                        }).ToList();
                                        break;
                                    default:
                                        break;
                                }
                            }

                            countDonVi++;
                        }
                        foreach (ReportTongHopPhanBoSoKiemTra item in listData)
                        {
                            item.TongTuChiConLai = item.TongTuChi - item.TongTuChiPB;
                            item.TongMuaHangHienVatDacThuConLai = item.TongMuaHangHienVatDacThu - item.TongMuaHangHienVatDacThuPB;
                        }

                        CalculateData(listData);
                        listData = listData.Where(item =>
                                item.TongTuChiPB != 0 || item.TongTuChi != 0 || item.TongTuChiConLai != 0
                                || item.TuChiDV1 != 0
                                || item.TuChiDV2 != 0
                                || item.TuChiDV3 != 0
                                || item.TuChiDV4 != 0
                                || item.TuChiDV5 != 0
                                || item.TuChiDV6 != 0
                                || item.TuChiDV7 != 0
                                || item.TuChiDV8 != 0
                                || item.TuChiDV9 != 0
                                || item.TongMuaHangHienVatDacThu != 0
                                || item.TongMuaHangHienVatDacThuConLai != 0
                                || item.TongMuaHangHienVatDacThuPB != 0
                                || item.MuaHangHienVatDV1 != 0
                                || item.MuaHangHienVatDV2 != 0
                                || item.MuaHangHienVatDV3 != 0
                                || item.DacThuDV1 != 0
                                || item.DacThuDV2 != 0
                                || item.DacThuDV3 != 0)
                            .ToList();
                        listDataSummary = listData.Where(item => item.IIdMlsktCha == Guid.Empty || item.IIdMlsktCha == null).ToList();
                        SumTotalTuChi = listDataSummary.Sum(x => x.TongTuChi);
                        SumTotalTuChiPB = listDataSummary.Sum(x => x.TongTuChiPB);
                        SumTotalTuChiConLai =
                           listDataSummary.Sum(x => x.TongTuChiConLai);
                        SumTotalTuChiBanThan = listDataSummary.Sum(x => x.TuChiBanThan);
                        SumTotalTuChiDV1 = listDataSummary.Sum(x => x.TuChiDV1);
                        SumTotalTuChiDV2 = listDataSummary.Sum(x => x.TuChiDV2);
                        SumTotalTuChiDV3 = listDataSummary.Sum(x => x.TuChiDV3);
                        SumTotalTuChiDV4 = listDataSummary.Sum(x => x.TuChiDV4);
                        SumTotalTuChiDV5 = listDataSummary.Sum(x => x.TuChiDV5);
                        SumTotalTuChiDV6 = listDataSummary.Sum(x => x.TuChiDV6);
                        SumTotalTuChiDV7 = listDataSummary.Sum(x => x.TuChiDV7);
                        SumTotalTuChiDV8 = listDataSummary.Sum(x => x.TuChiDV8);
                        SumTotalTuChiDV9 = listDataSummary.Sum(x => x.TuChiDV9);
                        SumTotalNSBD = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThu);
                        SumTotalNSBDPB = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuPB);
                        SumTotalNSBDConLai = listDataSummary.Sum(x => x.TongMuaHangHienVatDacThuConLai);
                        SumTotalMuaHangHienVatDV1 = listDataSummary.Sum(x => x.MuaHangHienVatDV1);
                        SumTotalMuaHangHienVatDV2 = listDataSummary.Sum(x => x.MuaHangHienVatDV2);
                        SumTotalMuaHangHienVatDV3 = listDataSummary.Sum(x => x.MuaHangHienVatDV3);
                        SumTotalDacThuDV1 = listDataSummary.Sum(x => x.DacThuDV1);
                        SumTotalDacThuDV2 = listDataSummary.Sum(x => x.DacThuDV2);
                        SumTotalDacThuDV3 = listDataSummary.Sum(x => x.DacThuDV3);
                        foreach (var x in listData)
                        {
                            if (x.bHangCha == false && StringUtils.IsNullOrEmpty(x.sM))
                            {
                                x.sM = "1000";
                            }
                        }
                        data = new Dictionary<string, object>
                        {
                            { "FormatNumber", formatNumber },
                            { "ListData", listData },
                            //{ "TieuDe1", TxtTitleFirst },
                            //{ "TieuDe2", TxtTitleSecond },
                            //{ "TieuDe3", TxtTitleThird },
                            //{ "Ngay", DateUtils.FormatDateReport(ReportDate) },
                            //{ "Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "" },
                            //{ "Cap2", _sessionInfo.TenDonVi },
                            //{ "DiaDiem", _diaDiem },
                            // page 2 no header
                            { "TieuDe1", string.Empty },
                            { "TieuDe2", string.Empty },
                            { "TieuDe3", string.Empty },
                            { "Ngay", string.Empty },
                            { "Cap1", string.Empty },
                            { "Cap2", string.Empty },
                            { "DiaDiem", string.Empty },
                            { "h1", h1 },
                            { "h2", _sessionInfo.TenDonVi },
                            { "TenDV1", tenDonVi1 },
                            { "TenDV2", tenDonVi2 },
                            { "TenDV3", tenDonVi3 },
                            { "TenDV4", tenDonVi4 },
                            { "TenDV5", tenDonVi5 },
                            { "TenDV6", tenDonVi6 },
                            { "TenDV7", tenDonVi7 },
                            { "TenDV8", tenDonVi8 },
                            { "TenDV9", tenDonVi9 },
                            { "SumTotalTuChi", SumTotalTuChi },
                            { "SumTotalTuChiPB", SumTotalTuChiPB },
                            { "SumTotalTuChiConLai", SumTotalTuChiConLai },
                            { "SumTotalTuChiBanThan", SumTotalTuChiBanThan },
                            { "SumTotalTuChiDV1", SumTotalTuChiDV1 },
                            { "SumTotalTuChiDV2", SumTotalTuChiDV2 },
                            { "SumTotalTuChiDV3", SumTotalTuChiDV3 },
                            { "SumTotalTuChiDV4", SumTotalTuChiDV4 },
                            { "SumTotalTuChiDV5", SumTotalTuChiDV5 },
                            { "SumTotalTuChiDV6", SumTotalTuChiDV6 },
                            { "SumTotalTuChiDV7", SumTotalTuChiDV7 },
                            { "SumTotalTuChiDV8", SumTotalTuChiDV8 },
                            { "SumTotalTuChiDV9", SumTotalTuChiDV9 },
                            { "SumTotalNSBD", SumTotalNSBD },
                            { "SumTotalNSBDPB", SumTotalNSBDPB },
                            { "SumTotalNSBDConLai", SumTotalNSBDConLai },
                            { "SumTotalMuaHangHienVatDV1", SumTotalMuaHangHienVatDV1 },
                            { "SumTotalMuaHangHienVatDV2", SumTotalMuaHangHienVatDV2 },
                            { "SumTotalMuaHangHienVatDV3", SumTotalMuaHangHienVatDV3 },
                            { "SumTotalDacThuDV1", SumTotalDacThuDV1 },
                            { "SumTotalDacThuDV2", SumTotalDacThuDV2 },
                            { "SumTotalDacThuDV3", SumTotalDacThuDV3 }
                        };
                        AddChuKy(data, _typeChuky);

                        if (loaiChungTu == 1)
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSSD_TO2));
                        }
                        else
                        {
                            templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_PHULUC_NSBD_TO2));
                        }
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTra>(templateFileName, data);
                        results.Add(new ExportResult("Tổng hợp phân bổ số kiểm tra - Tờ " + p, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, exportType);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        public void OnPrintReportTongHopPhanBoTrinhKy(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                string idDonViString = string.Join(",", ListDonVi.Where(item => item.IsChecked).Select(x => x.ValueItem.ToString()).ToList());
                int donViTinh = int.Parse(_catUnitTypeSelected.ValueItem);
                int loaiNNS = int.Parse(BudgetSourceTypeSelected?.ValueItem ?? "0");
                if (loaiNNS != 0) TxtTitleThird = BudgetSourceTypeSelected.DisplayItem;

                var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
                var yearOfWork = _sessionInfo.YearOfWork;
                var yearOfBudget = _sessionInfo.YearOfBudget;
                var budgetSource = _sessionInfo.Budget;
                var userName = _sessionInfo.Principal;
                var h1 = $"Đơn vị tính: {_catUnitTypeSelected.DisplayItem}";
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ReportTongHopPhanBoSoKiemTraTrinhKyQuery> listData = _sktChungTuChiTietService
                    .FindReportPhanBoSoKiemTraDonViTrinhKy(idDonViString, loaiChungTu, yearOfWork, yearOfBudget,
                        budgetSource, donViTinh, userName, loaiNNS).ToList();
                var stt = 1;
                foreach (var dt in listData)
                {
                    dt.Stt = stt++;
                }

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                Dictionary<string, object> data = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                data.Add("FormatNumber", formatNumber);
                data.Add("ListData", listData);
                data.Add("TieuDe1", TxtTitleFirst);
                data.Add("TieuDe2", TxtTitleSecond);
                data.Add("TieuDe3", TxtTitleThird);
                data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : "");
                data.Add("Cap2", _sessionInfo.TenDonVi);
                data.Add("DiaDiem", _diaDiem);
                data.Add("h1", h1);
                data.Add("h2", _sessionInfo.TenDonVi);
                data.Add("NamNay", _sessionInfo.YearOfWork);
                data.Add("NamTruoc", _sessionInfo.YearOfWork - 1);
                AddChuKy(data, _typeChuky);

                string templateFileName;
                if (loaiChungTu == 1)
                {
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_TRINHKY_NSSD));
                }
                else
                {
                    templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_TONGHOP_PHANBO_SOKIEMTRA_TRINHKY_NSBD));
                }
                string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportTongHopPhanBoSoKiemTraTrinhKyQuery>(templateFileName, data);
                e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, exportType);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private bool CheckListValue(List<DataReportDynamic> listResult)
        {
            return listResult.Any(x => x.FVal != 0);
        }

        private void CalculateDataExcel(List<ReportTongHopPhanBoSoKiemTra> listData, List<ReportTongHopPhanBoSoKiemTra> listResult)
        {
            foreach (var item in _lstIIdMaDonVi.Select((value, index) => new { index, value }))
            {
                var listDataAgencies = listData.Where(x => !string.IsNullOrEmpty(x.IIdMaDonVi) && x.IIdMaDonVi.Equals(item.value.ValueItem)).ToList();
                listDataAgencies.ForEach(x =>
                {
                    if (listResult.Any(y => y.sKyHieu.Equals(x.sKyHieu)))
                    {
                        x.TongTuChi = listResult.FirstOrDefault(y => y.sKyHieu.Equals(x.sKyHieu)).TongTuChi;
                    }
                });
                listResult.Where(x => x.bHangCha.GetValueOrDefault(false))
                    .Select(x =>
                    {
                        x.TongTuChi = 0;
                        x.TongTuChiConLai = 0;
                        x.TongTuChiPB = 0;
                        x.TuChiBanThan = 0;
                        x.TuChiDV1 = 0;
                        x.TuChiDV2 = 0;
                        x.TuChiDV3 = 0;
                        x.TuChiDV4 = 0;
                        x.TuChiDV5 = 0;
                        x.TuChiDV6 = 0;
                        x.DacThuDV1 = 0;
                        x.DacThuDV2 = 0;
                        x.DacThuDV3 = 0;
                        x.TongMuaHangHienVatDacThu = 0;
                        x.TongMuaHangHienVatDacThuConLai = 0;
                        x.TongMuaHangHienVatDacThuPB = 0;
                        x.MuaHangHienVatDV1 = 0;
                        x.MuaHangHienVatDV2 = 0;
                        x.MuaHangHienVatDV3 = 0;
                        x.ListDataValue = GetDataDefault();
                        return x;
                    }).ToList();

                foreach (var itemData in listResult.Where(x => !x.bHangCha.GetValueOrDefault(false)))
                {
                    //itemData.TongTuChi = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongTuChi);
                    //itemData.TongTuChiConLai = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongTuChiConLai);
                    //itemData.TongTuChiPB = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongTuChiPB);
                    //itemData.TongMuaHangHienVatDacThu = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongMuaHangHienVatDacThu);
                    //itemData.TongMuaHangHienVatDacThuConLai = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongMuaHangHienVatDacThuConLai);
                    //itemData.TongMuaHangHienVatDacThuPB = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu) && y.sMoTa.Equals(itemData.sMoTa)).Sum(s => s.TongMuaHangHienVatDacThuPB);
                    itemData.ListDataValue[item.index].FVal = listDataAgencies.Where(y => y.sKyHieu.Equals(itemData.sKyHieu)).Sum(s => s.TongTuChiPB);
                }

                var temp = listResult.Where(x => !x.bHangCha.GetValueOrDefault(false));
                foreach (var child in temp)
                {
                    CalculateParentExcel(child.IIdMlsktCha, child, listResult);
                }

            }
        }
        private List<DataReportDynamic> GetDataDefault()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (var item in _lstDataDynamic)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }

        private void CalculateData(List<ReportPhanBoSoKiemTraDonViQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.bHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.HuyDong = 0;
                    x.MuaHangHienVat = 0;
                    x.DacThu = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.bHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateData(List<ReportPhanBoKiemTraPhuongAnPhanBoQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.FSoKiemTraNS = 0;
                    x.FDuToanDauNam = 0;
                    x.FSoDuKienPB = 0;
                    x.FTang = 0;
                    x.FGiam = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, ReportPhanBoKiemTraPhuongAnPhanBoQuery item, List<ReportPhanBoKiemTraPhuongAnPhanBoQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FSoKiemTraNS += item.FSoKiemTraNS;
            model.FDuToanDauNam += item.FDuToanDauNam;
            model.FSoDuKienPB += item.FSoDuKienPB;
            model.FTang += item.FTang;
            model.FGiam += item.FGiam;
            CalculateParent(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateDataNC3Y(List<ReportTongHopSoNhuCauPhuLuc6Query> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.DuToan = 0;
                    x.UocThucHien = 0;
                    x.NhuCauNam1 = 0;
                    x.NhuCauNam2 = 0;
                    x.NhuCauNam3 = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParentNC3Y(item.iID_MLSKTCha, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, ReportPhanBoSoKiemTraDonViQuery item, List<ReportPhanBoSoKiemTraDonViQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IdMucLuc == idParent);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HuyDong += item.HuyDong;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }
        private void CalculateParentNC3Y(Guid? idParent, ReportTongHopSoNhuCauPhuLuc6Query item, List<ReportTongHopSoNhuCauPhuLuc6Query> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.iID_MLSKT == idParent);
            if (model == null) return;
            model.DuToan += item.DuToan;
            model.UocThucHien += item.UocThucHien;
            model.NhuCauNam1 += item.NhuCauNam1;
            model.NhuCauNam2 += item.NhuCauNam2;
            model.NhuCauNam3 += item.NhuCauNam3;
            CalculateParentNC3Y(model.iID_MLSKTCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportSoNhuCauTongHopQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.bHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.HuyDong = 0;
                    x.MuaHangHienVat = 0;
                    x.PhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.bHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportSoNhuCauTongHopQuery item, List<ReportSoNhuCauTongHopQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IdMucLuc == idParent);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HuyDong += item.HuyDong;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.PhanCap += item.PhanCap;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportTongHopPhanBoSoKiemTra> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.bHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.SoKiemTraDuocThongBao = 0;
                    x.TongTuChi = 0;
                    x.TongTuChiConLai = 0;
                    x.TongTuChiPB = 0;
                    x.TuChiBanThan = 0;
                    x.TuChiDV1 = 0;
                    x.TuChiDV2 = 0;
                    x.TuChiDV3 = 0;
                    x.TuChiDV4 = 0;
                    x.TuChiDV5 = 0;
                    x.TuChiDV6 = 0;
                    x.TuChiDV7 = 0;
                    x.TuChiDV8 = 0;
                    x.TuChiDV9 = 0;
                    x.TongMuaHangHienVatDacThu = 0;
                    x.TongMuaHangHienVatDacThuConLai = 0;
                    x.TongMuaHangHienVatDacThuPB = 0;
                    x.MuaHangHienVatDV1 = 0;
                    x.MuaHangHienVatDV2 = 0;
                    x.MuaHangHienVatDV3 = 0;
                    x.DacThuDV1 = 0;
                    x.DacThuDV2 = 0;
                    x.DacThuDV3 = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.bHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportTongHopPhanBoSoKiemTra item, List<ReportTongHopPhanBoSoKiemTra> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.SoKiemTraDuocThongBao += item.SoKiemTraDuocThongBao;
            model.TongTuChi += item.TongTuChi;
            model.TongTuChiConLai += item.TongTuChiConLai;
            model.TongTuChiPB += item.TongTuChiPB;
            model.TuChiBanThan += item.TuChiBanThan;
            model.TuChiDV1 += item.TuChiDV1;
            model.TuChiDV2 += item.TuChiDV2;
            model.TuChiDV3 += item.TuChiDV3;
            model.TuChiDV4 += item.TuChiDV4;
            model.TuChiDV5 += item.TuChiDV5;
            model.TuChiDV6 += item.TuChiDV6;
            model.TuChiDV7 += item.TuChiDV7;
            model.TuChiDV8 += item.TuChiDV8;
            model.TuChiDV9 += item.TuChiDV9;
            model.TongMuaHangHienVatDacThu += item.TongMuaHangHienVatDacThu;
            model.TongMuaHangHienVatDacThuConLai += item.TongMuaHangHienVatDacThuConLai;
            model.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatDacThuPB;
            model.MuaHangHienVatDV1 += item.MuaHangHienVatDV1;
            model.MuaHangHienVatDV2 += item.MuaHangHienVatDV2;
            model.MuaHangHienVatDV3 += item.MuaHangHienVatDV3;
            model.DacThuDV1 += item.DacThuDV1;
            model.DacThuDV2 += item.DacThuDV2;
            model.DacThuDV3 += item.DacThuDV3;
            CalculateParent(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateParentExcel(Guid? idParent, ReportTongHopPhanBoSoKiemTra item, List<ReportTongHopPhanBoSoKiemTra> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.TongTuChi += item.TongTuChi;
            model.TongTuChiConLai += item.TongTuChiConLai;
            model.TongTuChiPB += item.TongTuChiPB;
            model.TongTuChiPB += item.TongTuChiPB;
            model.TongMuaHangHienVatDacThu += item.TongMuaHangHienVatDacThu;
            model.TongMuaHangHienVatDacThuConLai += item.TongMuaHangHienVatDacThuConLai;
            model.TongMuaHangHienVatDacThuPB += item.TongMuaHangHienVatDacThuPB;
            model.MuaHangHienVatDV1 += item.MuaHangHienVatDV1;
            model.MuaHangHienVatDV2 += item.MuaHangHienVatDV2;
            model.MuaHangHienVatDV3 += item.MuaHangHienVatDV3;
            model.TuChiDV1 += item.TuChiDV1;
            model.TuChiDV2 += item.TuChiDV2;
            model.TuChiDV3 += item.TuChiDV3;
            model.TuChiDV4 += item.TuChiDV4;
            model.TuChiDV5 += item.TuChiDV5;
            model.TuChiDV6 += item.TuChiDV6;
            model.TuChiDV7 += item.TuChiDV7;
            model.TuChiDV8 += item.TuChiDV8;
            model.TuChiDV9 += item.TuChiDV9;
            model.DacThuDV1 += item.DacThuDV1;
            model.DacThuDV2 += item.DacThuDV2;
            model.DacThuDV3 += item.DacThuDV3;
            for (int i = 0; i < _lstIIdMaDonVi.Count; i++)
            {
                model.ListDataValue[i].FVal += item.ListDataValue[i].FVal;
            }
            CalculateParentExcel(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, NsSktChungTuChiTietModel item, List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            if (item.FTuChi != 0
                || item.FTuChi != 0
                || item.FMuaHangCapHienVat != 0
                || item.FPhanCap != 0)
            {
                model.isPrintDisplay = true;
            }
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        private void CalculateParent(Guid? idParent, string idDonVi, ReportPhanBoKiemTraTheoNganhPhuLucQuery item, List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent && x.IdDonVi == idDonVi);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            model.PhanCap += item.PhanCap;
            CalculateParent(model.IIdMlsktCha, model.IdDonVi, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportPhanBoKiemTraTheoNganhPhuLucQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.MuaHangHienVat = 0;
                    x.DacThu = 0;
                    x.PhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item.IdDonVi, item, lstSktChungTuChiTiet);
            }
        }
        private void CalculateData(List<ReportPhanBoKiemTraTheoNganhQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.MuaHangHienVat = 0;
                    x.DacThu = 0;
                    x.PhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportPhanBoKiemTraTheoNganhQuery item, List<ReportPhanBoKiemTraTheoNganhQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            model.PhanCap += item.PhanCap;
            CalculateParent(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportChiTietPhanBoKiemTraNsbdQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.MuaHangHienVat = 0;
                    x.DacThu = 0;
                    x.PhanCap = 0;
                    x.ThongBaoDV = 0;
                    x.HuyDongTonKho = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMlsktCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportChiTietPhanBoKiemTraNsbdQuery item, List<ReportChiTietPhanBoKiemTraNsbdQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.DacThu += item.DacThu;
            model.PhanCap += item.PhanCap;
            model.ThongBaoDV += item.ThongBaoDV;
            model.HuyDongTonKho += item.HuyDongTonKho;
            CalculateParent(model.IIdMlsktCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportSoNhuCauTongHopDonViQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.HuyDong = 0;
                    x.MuaHangHienVat = 0;
                    x.PhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item.IdDonVi, item, lstSktChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, string idDonVi, ReportSoNhuCauTongHopDonViQuery item, List<ReportSoNhuCauTongHopDonViQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IdMucLuc == idParent && x.IdDonVi == idDonVi);
            if (model == null) return;
            model.TuChi += item.TuChi;
            model.HuyDong += item.HuyDong;
            model.MuaHangHienVat += item.MuaHangHienVat;
            model.PhanCap += item.PhanCap;
            CalculateParent(model.IdParent, model.IdDonVi, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportTongHopSoNhuCauPhuLuc3Query> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.SoKiemTraNamTruoc = 0;
                    x.DuToanDauNam = 0;
                    x.TongSo = 0;
                    x.TonKhoDenNgay = 0;
                    x.HuyDongTonKho = 0;
                    x.TuChi = 0;
                    x.Tang = 0;
                    x.Giam = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.iID_MLSKTCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportTongHopSoNhuCauPhuLuc3Query item, List<ReportTongHopSoNhuCauPhuLuc3Query> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.iID_MLSKT == idParent);
            if (model == null) return;
            model.SoKiemTraNamTruoc += item.SoKiemTraNamTruoc;
            model.DuToanDauNam += item.DuToanDauNam;
            model.TongSo += item.TongSo;
            model.TonKhoDenNgay += item.TonKhoDenNgay;
            model.HuyDongTonKho += item.HuyDongTonKho;
            model.TuChi += item.TuChi;
            model.Tang += item.Tang;
            model.Giam += item.Giam;
            CalculateParent(model.iID_MLSKTCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportTongHopSoNhuCauPhuLuc4Query> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongSo = 0;
                    x.SoKiemTraDacThuNamTruoc = 0;
                    x.SoNganhPhanCap = 0;
                    x.KhungNganSachDuocDuyet = 0;
                    x.DacThu = 0;
                    x.Tang = 0;
                    x.Giam = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.iID_MLSKTCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportTongHopSoNhuCauPhuLuc4Query item, List<ReportTongHopSoNhuCauPhuLuc4Query> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.iID_MLSKT == idParent);
            if (model == null) return;
            model.SoKiemTraDacThuNamTruoc += item.SoKiemTraDacThuNamTruoc;
            model.TongSo += item.TongSo;
            model.DacThu += item.DacThu;
            model.KhungNganSachDuocDuyet += item.KhungNganSachDuocDuyet;
            model.SoNganhPhanCap += item.SoNganhPhanCap;
            model.Tang += item.Tang;
            model.Giam += item.Giam;
            CalculateParent(model.iID_MLSKTCha, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<ReportTongHopSoNhuCauPhuLuc5Query> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongSo = 0;
                    x.SoKiemTraMHHVNamTruoc = 0;
                    x.DuToanDauNam = 0;
                    x.HuyDongTonKho = 0;
                    x.TonKhoDenNgay = 0;
                    x.MuaHangCapHienVat = 0;
                    x.Tang = 0;
                    x.Giam = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.iID_MLSKTCha, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid? idParent, ReportTongHopSoNhuCauPhuLuc5Query item, List<ReportTongHopSoNhuCauPhuLuc5Query> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.iID_MLSKT == idParent);
            if (model == null) return;
            model.SoKiemTraMHHVNamTruoc += item.SoKiemTraMHHVNamTruoc;
            model.DuToanDauNam += item.DuToanDauNam;
            model.TongSo += item.TongSo;
            model.HuyDongTonKho += item.HuyDongTonKho;
            model.TonKhoDenNgay += item.TonKhoDenNgay;
            model.MuaHangCapHienVat += item.MuaHangCapHienVat;
            model.Tang += item.Tang;
            model.Giam += item.Giam;
            CalculateParent(model.iID_MLSKTCha, item, lstSktChungTuChiTiet);
        }

        public void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        //public void OnExportChiTietSoNhuCau(ExportType exportType)
        //{
        //    try
        //    {
        //        BackgroundWorkerHelper.Run((s, e) =>
        //        {
        //            IsLoading = true;

        //            List<ExportResult> results = new List<ExportResult>();
        //            string templateFileName;

        //            var yearOfWork = _sessionInfo.YearOfWork;
        //            var yearOfBudget = _sessionInfo.YearOfBudget;
        //            var budgetSource = _sessionInfo.Budget;
        //            var donViTinh = CatUnitTypeSelected != null ? Convert.ToInt32(CatUnitTypeSelected.ValueItem) : 1;
        //            var loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
        //            var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
        //                .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
        //            var listData = ListDonVi.Where(item => item.IsChecked).ToList().Select(item =>
        //            {
        //                List<SktChungTuChiTietQuery> listSktChungTuDetail = _sktChungTuChiTietService.FindReportNhapSoKiemTra(
        //                    item.ValueItem, yearOfWork, yearOfBudget, budgetSource,
        //                    0, (int)donViTinh, loaiChungTu).ToList();
        //                var listIdMucLuc = listSktChungTuDetail.Select(x => x.KyHieu).Distinct().ToList();
        //                var sktMuclucs = FindListParentMucLucByChild(listIdMucLuc);
        //                foreach (var ml in sktMuclucs)
        //                {
        //                    if (!listIdMucLuc.Contains(ml.SKyHieu))
        //                    {
        //                        SktChungTuChiTietQuery ct = new SktChungTuChiTietQuery();
        //                        ct.MoTa = ml.SMoTa;
        //                        ct.IdParent = ml.IIDMLSKTCha;
        //                        ct.IdMucLuc = ml.IIDMLSKT;
        //                        ct.Stt = ml.SSTT;
        //                        ct.KyHieu = ml.SKyHieu;
        //                        ct.BHangCha = ml.BHangCha;
        //                        ct.TotalTuChi = 0;
        //                        ct.TotalHuyDong = 0;
        //                        ct.TotalPhanCap = 0;
        //                        ct.TotalMuaHangHienVat = 0;
        //                        listSktChungTuDetail.Add(ct);
        //                    }
        //                }

        //                listSktChungTuDetail = listSktChungTuDetail.OrderBy(x => x.KyHieu).ToList();
        //                CalculateData(listSktChungTuDetail);

        //                var dataExportsFilter = listSktChungTuDetail.Where(item => Math.Abs(item.TotalTuChi) >= double.Epsilon
        //                || Math.Abs(item.TotalHuyDong) >= double.Epsilon
        //                || Math.Abs(item.TotalMuaHangHienVat) >= double.Epsilon
        //                || Math.Abs(item.TotalPhanCap) >= double.Epsilon).ToList();
        //                foreach (var skt in dataExportsFilter)
        //                {
        //                    skt.TotalNSSD = skt.TotalHuyDong + skt.TotalTuChi;
        //                    skt.TotalNSBD = skt.TotalMuaHangHienVat + skt.TotalPhanCap;
        //                }

        //                //NSSD
        //                var SumTotalHuyDong = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalHuyDong);
        //                var SumTotalTuChi = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalTuChi);
        //                var SumTotalNSSD = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalNSSD);

        //                //NSBD
        //                var SumTotalMuaHangHienVat = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalMuaHangHienVat);
        //                var SumTotalDacThu = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalPhanCap);
        //                var SumTotalNSBD = dataExportsFilter.Where(item => item.IdParent.Equals(Guid.Empty))
        //                    .Sum(item => item.TotalNSBD);

        //                var parentList = dataExportsFilter
        //                    .Where(entity => entity.BHangCha && Guid.Empty.Equals(entity.IdParent)).ToList();
        //                parentList.ForEach(parent => parent.Level = 1);

        //                dataExportsFilter.Where(entity =>
        //                        entity.BHangCha && parentList.Any(parent => parent.IdMucLuc.Equals(entity.IdParent)))
        //                    .ToList().ForEach(subParent => subParent.Level = 2);

        //                string tenDonVi = item.DisplayItem.Split("-")[1];
        //                _dmChuKy = _dmChuKyService
        //                    .FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE)
        //                    .FirstOrDefault();
        //                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
        //                Dictionary<string, object> dictionaryData = new Dictionary<string, object>
        //                {
        //                    {"FormatNumber", formatNumber},
        //                    {"Items", dataExportsFilter},
        //                    {"SumTotalHuyDong", SumTotalHuyDong},
        //                    {"SumTotalTuChi", SumTotalTuChi},
        //                    {"SumTotalNSSD", SumTotalNSSD},
        //                    {"SumTotalMuaHangHienVat", SumTotalMuaHangHienVat},
        //                    {"SumTotalDacThu", SumTotalDacThu},
        //                    {"SumTotalNSBD", SumTotalNSBD},
        //                    {"TieuDe1", TxtTitleFirst},
        //                    {"TieuDe2", TxtTitleSecond},
        //                    {"TieuDe3", TxtTitleThird},
        //                    {"Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri : ""},
        //                    {"Cap2", _sessionInfo.TenDonVi},
        //                    {"TenDonVi", tenDonVi},
        //                    {"h1", $"Đơn vị tính: {CatUnitTypeSelected.DisplayItem}"},
        //                    {"h2", tenDonVi},
        //                    {"ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : ""},
        //                    {"ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : ""},
        //                    {"ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : ""},
        //                    {"ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : ""},
        //                    {"Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : ""},
        //                    {"Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : ""},
        //                    {"TienNSSD", StringUtils.NumberToText(SumTotalNSSD * donViTinh)},
        //                    {"TienNSBD", StringUtils.NumberToText(SumTotalNSBD * donViTinh)},
        //                    {"DiaDiem", _diaDiem},
        //                    {"Ngay", DateUtils.FormatDateReport(ReportDate)},
        //                    {"HoTen", "Quản trị hệ thống"},
        //                    {"NamKia", yearOfWork - 2},
        //                    {"NamTruoc", yearOfWork - 1},
        //                    {"Nam", yearOfWork}
        //                };

        //                return dictionaryData;
        //            });

        //            templateFileName = GetTemplateReportByPaperPrint();
        //            if (string.IsNullOrEmpty(templateFileName))
        //            {
        //                return;
        //            }

        //            foreach (var data in listData)
        //            {
        //                string fileNamePrefix = string.Format("{0}_{1}", Path.GetFileNameWithoutExtension(templateFileName), data["TenDonVi"]);
        //                fileNamePrefix = RemoveIllegalCharacterFileName(fileNamePrefix);
        //                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
        //                var xlsFile = _exportService.Export<SktChungTuChiTietQuery>(templateFileName, data);
        //                results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
        //            }
        //            e.Result = results;
        //        }, (s, e) =>
        //        {
        //            if (e.Error == null)
        //            {
        //                var result = (List<ExportResult>)e.Result;
        //                _exportService.Open(result, exportType);
        //            }
        //            else
        //            {
        //                _logger.Error(e.Error.Message);
        //            }
        //            IsLoading = false;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message, ex);
        //    }


        //}

        private void CalculateParent(Guid? idParent, SktChungTuChiTietQuery item, List<SktChungTuChiTietQuery> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.SoLieuCot1 += item.SoLieuCot1;
            model.SoLieuCot2 += item.SoLieuCot2;
            model.TotalTuChi += item.TotalTuChi;
            model.TotalHuyDong += item.TotalHuyDong;
            model.TotalMuaHangHienVat += item.TotalMuaHangHienVat;
            model.TotalPhanCap += item.TotalPhanCap;
            CalculateParent(model.IIdParentMlskt, item, lstSktChungTuChiTiet);
        }

        private void CalculateData(List<SktChungTuChiTietQuery> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.SoLieuCot1 = 0;
                    x.SoLieuCot2 = 0;
                    x.TotalTuChi = 0;
                    x.TotalHuyDong = 0;
                    x.TotalMuaHangHienVat = 0;
                    x.TotalPhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.BHangCha && !x.MoTa.StartsWith("   +"));
            foreach (var item in temp)
            {
                CalculateParent(item.IIdParentMlskt, item, lstSktChungTuChiTiet);
            }
        }

        public void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, ObservableCollection<NsSktChungTuChiTietModel> dataExports)
        {
            var model = dataExports.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            CalculateParent(model.IdParent, item, dataExports);
        }

        public void XuLyDuLieu(List<ReportSoNhuCauTongHopDonViQuery> lstData)
        {
            foreach (var it in lstData)
            {
                if (SelectedLoaiDuLieu != null)
                {
                    if (SelectedLoaiDuLieu.ValueItem.Equals("2"))
                    {
                        it.HuyDong = 0;
                    }
                    if (SelectedLoaiDuLieu.ValueItem.Equals("3"))
                    {
                        it.TuChi = 0;
                    }
                }
            }
        }

        public List<NsSktMucLuc> FindListParentMucLucByChild(List<string> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.SKyHieu) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }

        public List<NsSktMucLuc> FindListParentMucLucByKyHieuCu(List<string> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            List<NsSktMucLuc> listMucLuc = new List<NsSktMucLuc>();
            var listMucLucYear = _iSktMucLucService.FindByCondition(x => listIdMucLuc.Contains(x.SKyHieu) && x.INamLamViec == yearOfWork).ToList();
            if (listMucLucYear.Any()) listMucLuc.AddRange(listMucLucYear);
            var listMucLucLastYear = _iSktMucLucService.FindByCondition(x => listIdMucLuc.Contains(x.SKyHieu) && x.INamLamViec == yearOfWork - 1).ToList();
            if (listMucLucLastYear.Any()) listMucLuc.AddRange(listMucLucLastYear);
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    List<NsSktMucLuc> listParent1 = new List<NsSktMucLuc>();
                    var listParent1Year = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1Year.Any()) listParent1.AddRange(listParent1Year);
                    var listParent1LastYear = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork - 1).ToList();
                    if (listParent1LastYear.Any()) listParent1.AddRange(listParent1LastYear);
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }

        public List<NsSktMucLuc> FindListParent2CapMucLucByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listMucLuc = _iSktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            int count = 0;
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _iSktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1.Select(x =>
                        {
                            x.Rank = count + 1;
                            return x;
                        }));
                    }
                    else
                    {
                        break;
                    }

                    count++;
                    // luc dau dang de la count > 1 nen lay 2 dong
                    if (count > 3) break;
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }

        public string GetTemplateReportByPaperPrint()
        {
            string templateFileName = string.Empty;
            int loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
            if (loaiChungTu == 1)
            {
                templateFileName = PaperPrintTypeSelected.ValueItem switch
                {
                    "1" => Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_DONVI_NSSD),
                    "2" => Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_DONVI_PHULUC_NSSD),
                    _ => string.Empty,
                };
            }
            else if (loaiChungTu == 2)
            {
                templateFileName = PaperPrintTypeSelected.ValueItem switch
                {
                    "1" => Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_DONVI_NSBD),
                    "2" => Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SNC_DONVI_PHULUC_NSBD),
                    _ => string.Empty,
                };
            }
            return templateFileName;
        }

        public string GetListDonViChungTuTongHop()
        {
            string lstIdDonVi = "";
            int loaiChungTu = VoucherTypeSelected != null ? int.Parse(VoucherTypeSelected.ValueItem) : -1;
            var predicate = PredicateBuilder.True<NsSktChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.ILoai == DemandCheckType.DEMAND);
            predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
            predicate = predicate.And(x => x.IIdMaDonVi.Equals(_sessionInfo.IdDonVi));
            var ctTongHop = _sktChungTuService.FindByCondition(predicate).FirstOrDefault();
            if (ctTongHop != null)
            {
                if (string.IsNullOrEmpty(ctTongHop.SDssoChungTuTongHop)) return ctTongHop.IIdMaDonVi;
                predicate = PredicateBuilder.True<NsSktChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == DemandCheckType.DEMAND);
                predicate = predicate.And(x => x.ILoaiChungTu == loaiChungTu);
                predicate = predicate.And(x => ctTongHop.SDssoChungTuTongHop.Contains(x.SSoChungTu));
                var ctChild = _sktChungTuService.FindByCondition(predicate).ToList();
                lstIdDonVi = string.Join(",", ctChild.Select(x => x.IIdMaDonVi));
            }
            return lstIdDonVi;
        }

        private List<CheckBoxItem> LoadDonViByIdDonVi(string lstIdDonVi)
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicateDv = PredicateBuilder.True<DonVi>();
            predicateDv = predicateDv.And(x => x.NamLamViec == yearOfWork);
            predicateDv = predicateDv.And(x => lstIdDonVi.Contains(x.IIDMaDonVi));
            var lstDonVi = _nsDonViService.FindByCondition(predicateDv);
            var result = lstDonVi.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                DisplayItem = string.Join(StringUtils.DIVISION_SPLIT, item.IIDMaDonVi, item.TenDonVi),
                NameItem = item.TenDonVi
            }).OrderBy(item => item.ValueItem);
            return new List<CheckBoxItem>(result);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = _typeChuky;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TxtTitleFirst = chuKy.TieuDe1MoTa;
                TxtTitleSecond = chuKy.TieuDe2MoTa;
                TxtTitleThird = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        public void AddNgayDiaDiem(Dictionary<string, object> data)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
            data.Add("DiaDiem", _diaDiem);
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add chữ ký
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh1MoTa);
            data.Add("ChucDanh1", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh1MoTa);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy == null ? string.Empty : dmChyKy.Ten1MoTa);
            data.Add("ThuaLenh2", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh2MoTa);
            data.Add("ChucDanh2", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh2MoTa);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy == null ? string.Empty : dmChyKy.Ten2MoTa);
            data.Add("ThuaLenh3", dmChyKy == null ? string.Empty : dmChyKy.ThuaLenh3MoTa);
            data.Add("ChucDanh3", dmChyKy == null ? string.Empty : dmChyKy.ChucDanh3MoTa);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy == null ? string.Empty : dmChyKy.Ten3MoTa);
        }

        public string RemoveIllegalCharacterFileName(string fileName)
        {
            return new Regex(@"[<>:""/\\|?*]").Replace(fileName, "-");
        }

        public string GetTemplate(string input)
        {
            var reportOnlyLandscape = "rptNS_SNC_TongHop_PhuLuc6,rptNS_SNC_TongHop_PhuLuc3,rptNS_SNC_TongHop_PhuLuc4,rptNS_SNC_TongHop_PhuLuc5,rptNS_SNC_TongHop_PhuLuc3_NSNN,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang1,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02b";
            if (SelectedKieuGiayIn.ValueItem == "1" && !reportOnlyLandscape.Contains(input) && !input.StartsWith("rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_DonVi") && !input.StartsWith("rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc"))
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_TL_SKT, input + FileExtensionFormats.Xlsx);
        }

        public string GetTemplateExcel(string input)
        {
            var reportOnlyLandscape = "rptNS_SNC_TongHop_PhuLuc6,rptNS_SNC_TongHop_PhuLuc4,rptNS_SNC_TongHop_PhuLuc5,rptNS_SNC_TongHop_PhuLuc3_NSNN,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02a_Trang1,rptNS_PhanBo_SoKiemTra_PhuongAn_PhanBo_SKT_02b";
            if (SelectedKieuGiayIn.ValueItem == "1" && !reportOnlyLandscape.Contains(input) && !input.StartsWith("rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc_DonVi") && !input.StartsWith("rptNS_PhanBo_SoKiemTra_TheoNganh_PhuLuc"))
                input = input + "_Doc";
            return Path.Combine(ExportPrefix.PATH_TL_SKT, input + "_Excel" + FileExtensionFormats.Xlsx);
        }

        private class GhiChu
        {
            public string Content { get; set; }
            //public string SGhiChu => $"- {Content}";
            public string SGhiChu => Content;
        }

        private string GetDonViBanHanh(int iDonVi, string loaiDVBH, DanhMuc itemDanhMuc, string selectedUnit)
        {
            string dvBanHanh = "";
            if (itemDanhMuc != null)
            {
                switch (loaiDVBH)
                {
                    case "1":
                        dvBanHanh = itemDanhMuc.SGiaTri;
                        break;
                    case "2":
                        dvBanHanh = _sessionInfo.TenDonVi;
                        break;
                    case "3":
                        dvBanHanh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).SGiaTri;
                        break;
                    case "4":
                        dvBanHanh = selectedUnit;
                        break;
                    case "5":
                        dvBanHanh = iDonVi == 1 ? _dmChuKy.TenDVBanHanh1 : _dmChuKy.TenDVBanHanh2;
                        break;
                }
            }
            return dvBanHanh;
        }
    }
}
