using AutoMapper;
using FlexCel.Core;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.PrintReport
{
    public class PrintAllocationNoticeViewModel : ViewModelBase
    {
        private readonly ICpChungTuService _chungTuService;
        private readonly INsDonViService _donViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private List<CpChungTuQuery> _listChungTu;
        private List<CpChungTuQuery> _listChungTuDotCap;
        private CpChungTuQuery _chungTuSelected;
        private SessionInfo _sessionInfo;
        private List<ReportCapPhatThongTriQuery> _reportData;
        private bool _isCapPhatToanDonVi;
        private string _diaDiem;

        public override Type ContentType => typeof(View.Budget.Allocation.PrintReport.PrintAllocationNotice);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public bool IsShowRadioLoaiChungTu => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _listDonViView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountDonVi));
                    OnPropertyChanged(nameof(SelectAllDonVi));
                }
            }
        }

        private LoaiChungTu _loaiChungTuValue;
        public LoaiChungTu LoaiChungTuValue
        {
            get => _loaiChungTuValue;
            set
            {
                if (SetProperty(ref _loaiChungTuValue, value))
                    LoadDotCap();
            }
        }

        private string _mota;
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiBaoCao;
        public ObservableCollection<ComboboxItem> DataLoaiBaoCao
        {
            get => _dataLoaiBaoCao;
            set => SetProperty(ref _dataLoaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set => SetProperty(ref _selectedLoaiBaoCao, value);
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }

        private ObservableCollection<ComboboxItem> _dataToiDotCap;
        public ObservableCollection<ComboboxItem> DataDotCap
        {
            get => _dataToiDotCap;
            set => SetProperty(ref _dataToiDotCap, value);
        }

        private ComboboxItem _selectedDotCap;
        public ComboboxItem SelectedDotCap
        {
            get => _selectedDotCap;
            set
            {
                SetProperty(ref _selectedDotCap, value);
                LoadDonVi();
                GetMota();
            }
        }

        private string _tieuDe1;
        public string TieuDe1
        {
            get => _tieuDe1;
            set => SetProperty(ref _tieuDe1, value);
        }

        private string _tieuDe2;
        public string TieuDe2
        {
            get => _tieuDe2;
            set => SetProperty(ref _tieuDe2, value);
        }

        private string _tieuDe3;
        public string TieuDe3
        {
            get => _tieuDe3;
            set => SetProperty(ref _tieuDe3, value);
        }

        private string _ghiChu;
        public string GhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        public bool? SelectAllDonVi
        {
            get
            {
                if (ListDonVi != null)
                {
                    var selected = ListDonVi.Where(x => x.IsFilter).Select(item => item.IsChecked).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ListDonVi);
                    OnPropertyChanged();
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        private ObservableCollection<ComboboxItem> _dataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _dataDonViTinh;
            set => SetProperty(ref _dataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintAllocationNoticeViewModel(
            ICpChungTuService chungTuService,
            INsDonViService donViService,
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            IDanhMucService danhMucService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            INsNguoiDungDonViService nguoiDungDonViService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _chungTuService = chungTuService;
            _donViService = donViService;
            _mapper = mapper;
            _sessionService = sessionService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ShowPopupPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintPDFCommand = new RelayCommand(o => ExportFile(true));
            PrintExcelCommand = new RelayCommand(o => ExportFile(false));
            PrintBrowserCommand = new RelayCommand(o => ExportFile(true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadSettingCapPhat();
                if (!_isCapPhatToanDonVi)
                {
                    LoaiChungTuValue = LoaiChungTu.TONG_HOP;
                }
                LoadDanhMuc();
                LoadDonViTinh();
                LoadLoaiBaoCao();
                LoadTitle();
                LoadDotCap();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }


        private void LoadTitle()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
        }

        public void LoadDanhMuc()
        {
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadDonViTinh()
        {
            _dataDonViTinh = new ObservableCollection<ComboboxItem>();
            var predicate = PredicateBuilder.True<DanhMuc>();
            predicate = predicate.And(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH));
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);

            List<DanhMuc> data = _danhMucService.FindByCondition(predicate).OrderBy(n => n.SGiaTri).ToList();
            _dataDonViTinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            if (data.Count == 0)
            {
                _dataDonViTinh.Insert(0, new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            SelectedDonViTinh = _dataDonViTinh.FirstOrDefault();
            OnPropertyChanged(nameof(DataDonViTinh));
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        public void LoadDotCap()
        {
            DataDotCap = new ObservableCollection<ComboboxItem>();
            _listChungTu = _chungTuService.FindByCondition(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget,
                                                    _sessionInfo.Principal, _isCapPhatToanDonVi, 1).ToList();
            if (_isCapPhatToanDonVi)
                _listChungTuDotCap = _listChungTu.Where(x => x.IsLocked).ToList();
            else
            {
                if (!_sessionService.Current.IsQuanLyDonViCha || (_sessionService.Current.IsQuanLyDonViCha && LoaiChungTuValue == LoaiChungTu.THUONG))
                    _listChungTuDotCap = _listChungTu.Where(x => x.IsLocked && string.IsNullOrEmpty(x.DSSoChungTuTongHop)).ToList();
                else _listChungTuDotCap = _listChungTu.Where(x => !string.IsNullOrEmpty(x.DSSoChungTuTongHop)).ToList();
            }
            DataDotCap = _mapper.Map<ObservableCollection<ComboboxItem>>(_listChungTuDotCap);
            if (DataDotCap != null && DataDotCap.Count > 0)
            {
                SelectedDotCap = DataDotCap.FirstOrDefault();
            }
            else SelectedDotCap = null;
        }

        public void GetMota()
        {
            MoTa = string.Empty;
            if (SelectedDotCap != null)
                MoTa += string.Format("- {0}({1}): {2}", _chungTuSelected.SoChungTu, _chungTuSelected.UserCreator, _chungTuSelected.MoTa);
            OnPropertyChanged(nameof(MoTa));
        }

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.MAC_DINH, DisplayItem = LoaiBaoCao.MAC_DINH });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.LNS, DisplayItem = LoaiBaoCao.LNS });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.MUC, DisplayItem = LoaiBaoCao.MUC });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TIEU_MUC, DisplayItem = LoaiBaoCao.TIEU_MUC });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.TOI_NGANH, DisplayItem = LoaiBaoCao.TOI_NGANH });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
        }

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            if (SelectedDotCap == null)
            {
                OnPropertyChanged(nameof(ListDonVi));
                OnPropertyChanged(nameof(SelectedCountDonVi));
                OnPropertyChanged(nameof(IsExportEnable));
                OnPropertyChanged(nameof(SelectAllDonVi));
                return;
            }
            _chungTuSelected = _listChungTuDotCap.Where(n => n.SoChungTu == SelectedDotCap.ValueItem).First();
            List<DonVi> listDonVi = _donViService.FindByCapPhatId(_sessionInfo.YearOfWork, _chungTuSelected.Id.ToString()).ToList();
            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                        OnPropertyChanged(nameof(IsExportEnable));
                    }
                };
            }
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
            OnPropertyChanged(nameof(IsExportEnable));
            OnPropertyChanged(nameof(SelectAllDonVi));
        }

        private string GetLevelTitle(DmChuKy dmChuKy, int level, string donVi)
        {
            var iLoaiDVBanHanh = dmChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return iLoaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => donVi ?? string.Empty,
                LoaiDonViBanHanh.TUY_CHINH => dmChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(dmChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchDonVi))
                result = item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public void ExportFile(bool isPdf)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                int donViTinh = GetDonViTinh();
                FormatNumber formatNumber = new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL);
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, ExportFileName.RPT_NS_CAPPHAT_THONGTRI_MACDINH);


                foreach (CheckBoxItem donvi in ListDonVi.Where(n => n.IsChecked))
                {
                    _reportData = _chungTuService.GetDataReportCapPhatThongTri(_sessionInfo.YearOfWork, _chungTuSelected.Id,
                        donvi.ValueItem, _chungTuSelected.ChiTietToi, _chungTuSelected.Lns, _sessionInfo.Principal, donViTinh).ToList();
                    double tongTien = _reportData.Sum(x => x.TuChi);
                    CalculateData();
                    FormatDisplay();
                    FormatTypeReport();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    //data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper())

                    data.Add("Cap1", GetLevelTitle(_dmChuKy, 1, donvi.NameItem));
                    data.Add("Cap2", GetLevelTitle(_dmChuKy, 2, donvi.NameItem));
                    //data.Add("Cap1", _sessionInfo.TenDonVi)
                    //data.Add("Cap2", GetHeader2Report())
                    data.Add("Nam", DateTime.Now.Year.ToString());
                    data.Add("TieuDe1", TieuDe1);
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("DonVi", donvi.NameItem);
                    data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                    data.Add("TongChiTieu", tongTien);
                    data.Add("Items", _reportData);
                    data.Add("Header1", SelectedDonViTinh.DisplayItem);
                    data.Add("TienBangChu", StringUtils.NumberToText(tongTien * donViTinh, true));
                    data.Add("GhiChu", GhiChu);
                    data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                    ExcelFile xlsFile = _exportService.Export<ReportCapPhatThongTriQuery>(templateFileName, data);
                    /*
                    switch (SelectedLoaiBaoCao.ValueItem)
                    {
                        case LoaiBaoCao.MAC_DINH:
                        case LoaiBaoCao.TOI_NGANH:
                            break;
                        case LoaiBaoCao.TIEU_MUC:
                            xlsFile.SetColHidden(8, true);
                            xlsFile.SetColHidden(9, true);
                            break;
                        case LoaiBaoCao.MUC:
                            int width = 5000;
                            xlsFile.SetColWidth(5, width);
                            xlsFile.SetColWidth(6, width);
                            xlsFile.SetColHidden(7, true);
                            xlsFile.SetColHidden(8, true);
                            xlsFile.SetColHidden(9, true);
                            break;
                        case LoaiBaoCao.LNS:
                            int widthLNS = 8000;
                            xlsFile.SetColWidth(4, 7000);
                            xlsFile.SetColWidth(10, widthLNS);
                            xlsFile.SetColWidth(11, widthLNS);
                            xlsFile.SetColHidden(5, true);
                            xlsFile.SetColHidden(6, true);
                            xlsFile.SetColHidden(7, true);
                            xlsFile.SetColHidden(8, true);
                            xlsFile.SetColHidden(9, true);
                            break;
                        default:
                            break;
                    }
                    */
                    xlsFile.DrawBorders(16, 10, 16, 10, TFlxBorderStyle.Thin, TExcelColor.Automatic, true);
                    string fileNameWithoutExtension = string.Format("rptCapPhat_ThongTri_MacDinh_{0}_{1}", donvi.ValueItem, DateTime.Now.ToStringTimeStamp());
                    results.Add(new ExportResult(donvi.DisplayItem, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
                }
                else
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
        }

        private void CalculateData()
        {
            foreach (var item in _reportData.Where(x => !x.IsHangCha && (x.TuChi != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ReportCapPhatThongTriQuery currentItem, ReportCapPhatThongTriQuery selfItem)
        {
            var parentItem = _reportData.Where(x => x.IdMlns == currentItem.IdMlnsCha).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            CalculateParent(parentItem, selfItem);
        }

        private void FormatTypeReport()
        {
            switch (SelectedLoaiBaoCao.ValueItem)
            {
                case LoaiBaoCao.TIEU_MUC:
                    _reportData = _reportData.Where(n => string.IsNullOrEmpty(n.TTM)).ToList();
                    break;
                case LoaiBaoCao.MUC:
                    _reportData = _reportData.Where(n => string.IsNullOrEmpty(n.TM)).ToList();
                    break;
                case LoaiBaoCao.LNS:
                    _reportData = _reportData.Where(n => string.IsNullOrEmpty(n.M)).ToList();
                    break;
                default:
                    break;
            }
        }

        public string GetHeader2Report()
        {
            //DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork)
            //return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty
            return _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH).FirstOrDefault()?.SGiaTri ?? string.Empty;
        }

        private void FormatDisplay()
        {
            _reportData = _reportData.Where(n => n.TuChi != 0).ToList();
            if (SelectedLoaiBaoCao.ValueItem != LoaiBaoCao.LNS)
            {
                _reportData.Where(s => !string.IsNullOrEmpty(s.LK)).Select(s => { s.LNS = string.Empty; return s; }).ToList();
                foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.M)))
                {
                    var parent = _reportData.Where(x => x.IdMlns == item.IdMlnsCha).FirstOrDefault();
                    if (parent != null && parent.LK != string.Empty)
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
                    }
                }
            }

            string loaiBaoCao = string.Empty;
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.MAC_DINH)
            {
                string phanCap = _chungTuSelected.ChiTietToi;
                if (phanCap == NSChiTietToi.DB_VALUE_NGANH)
                    loaiBaoCao = LoaiBaoCao.TOI_NGANH;
                else if (phanCap == NSChiTietToi.DB_VALUE_TIEU_MUC)
                    loaiBaoCao = LoaiBaoCao.TIEU_MUC;
                else if (phanCap == NSChiTietToi.DB_VALUE_MUC)
                    loaiBaoCao = LoaiBaoCao.MUC;
            }
            else
                loaiBaoCao = SelectedLoaiBaoCao.ValueItem;
            switch (loaiBaoCao)
            {
                case LoaiBaoCao.MAC_DINH:
                case LoaiBaoCao.TOI_NGANH:
                    foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.TM)))
                    {
                        var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                        if (parent != null && parent.M != string.Empty)
                        {
                            item.M = string.Empty;
                            item.L = string.Empty;
                            item.K = string.Empty;
                            item.LNS = string.Empty;
                        }
                    }
                    foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.TTM)))
                    {
                        var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                        if (parent != null && parent.TM != string.Empty)
                        {
                            item.TM = string.Empty;
                            item.M = string.Empty;
                            item.L = string.Empty;
                            item.K = string.Empty;
                            item.LNS = string.Empty;
                        }
                    }
                    break;
                case LoaiBaoCao.TIEU_MUC:
                    _reportData.Where(n => !string.IsNullOrEmpty(n.TM)).Select(m =>
                    {
                        m.IsHangCha = false;
                        return m;
                    }).ToList();
                    foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.TM)))
                    {
                        var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                        if (parent != null && parent.M != string.Empty)
                        {
                            item.M = string.Empty;
                            item.L = string.Empty;
                            item.K = string.Empty;
                            item.LNS = string.Empty;
                        }
                    }
                    break;
                case LoaiBaoCao.MUC:
                    _reportData.Where(n => !string.IsNullOrEmpty(n.M)).Select(m =>
                    {
                        m.IsHangCha = false;
                        return m;
                    }).ToList();
                    foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.M)))
                    {
                        var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                        if (parent != null && parent.LK != string.Empty)
                        {
                            item.L = string.Empty;
                            item.K = string.Empty;
                            item.LNS = string.Empty;
                        }
                    }
                    break;
                default:
                    _reportData.Where(n => !string.IsNullOrEmpty(n.LK)).Select(m =>
                    {
                        m.IsHangCha = false;
                        return m;
                    }).ToList();
                    foreach (var item in _reportData.Where(x => !string.IsNullOrEmpty(x.LK)))
                    {
                        var parent = _reportData.FirstOrDefault(x => x.IdMlns == item.IdMlnsCha);
                        if (parent != null && parent.LNS != string.Empty)
                        {
                            item.LNS = string.Empty;
                            item.M = string.Empty;
                            item.TM = string.Empty;
                            item.TTM = string.Empty;
                            item.NG = string.Empty;
                        }
                    }
                    break;
            }
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_LNS) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_CAPPHAT_LNS;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                DmChuKyModel chuKy = (DmChuKyModel)obj;
                TieuDe1 = chuKy.TieuDe1MoTa;
                TieuDe2 = chuKy.TieuDe2MoTa;
                TieuDe3 = chuKy.TieuDe3MoTa;
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }
    }
}
