using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
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
    public class PrintAllocationDonViViewModel : ViewModelBase
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
        private List<CpChungTuQuery> _listChungTu;
        private List<CpChungTuQuery> _listChungTuDotCap;
        private CpChungTuQuery _chungTuSelected;
        private List<ReportCapPhatThongTriDonViQuery> _reportData;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private string _diaDiem;
        private bool _isCapPhatToanDonVi;
        public override Type ContentType => typeof(View.Budget.Allocation.PrintReport.PrintAllocationDonVi);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public bool IsExportEnable => ListDonVi != null && ListDonVi.Any(x => x.IsChecked);
        public bool IsShowRadioLoaiChungTu => !_isCapPhatToanDonVi && _sessionService.Current.IsQuanLyDonViCha;

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

        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }

        private List<ComboboxItem> _loaiNganSach;
        public List<ComboboxItem> LoaiNganSach
        {
            get => _loaiNganSach;
            set => SetProperty(ref _loaiNganSach, value);
        }

        private ComboboxItem _loaiNganSachSelected;
        public ComboboxItem LoaiNganSachSelected
        {
            get => _loaiNganSachSelected;
            set
            {
                //SetProperty(ref _loaiNganSachSelected, value);
                if (SetProperty(ref _loaiNganSachSelected, value))
                {
                    LoadDonVi();
                }
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

        private string _mota;
        public string MoTa
        {
            get => _mota;
            set => SetProperty(ref _mota, value);
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

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public RelayCommand ShowPopupPrintCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintAllocationDonViViewModel(
           ICpChungTuService chungTuService,
           INsDonViService donViService,
           IMapper mapper,
           ILog logger,
           ISessionService sessionService,
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

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public override void Init()
        {
            try
            {
                base.Init();
                _sessionInfo = _sessionService.Current;
                LoaiNganSachSelected = null;
                InitReportDefaultDate();
                LoadSettingCapPhat();
                if (!_isCapPhatToanDonVi)
                {
                    LoaiChungTuValue = LoaiChungTu.TONG_HOP;
                }
                LoadDanhMuc();
                LoadDonViTinh();
                LoadLoaiNganSach();
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

        private void LoadLoaiNganSach()
        {
            _loaiNganSach = new List<ComboboxItem>();
            _loaiNganSach.Add(new ComboboxItem("Tất cả", "-1"));
            _loaiNganSach.Add(new ComboboxItem("Thường xuyên", "0"));
            _loaiNganSach.Add(new ComboboxItem("Nghiệp vụ", "1"));
            _loaiNganSach.Add(new ComboboxItem("NSNN", "2"));
            _loaiNganSach.Add(new ComboboxItem("Kinh phí khác", "3"));
            _loaiNganSach.Add(new ComboboxItem("Quốc phòng khác", "4"));
            //_loaiNganSach.Add(new ComboboxItem("Nhà nước khác", "4"));
            LoaiNganSachSelected = _loaiNganSach.FirstOrDefault();
        }

        private void LoadDonViTinh()
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionInfo.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void LoadDanhMuc()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
            TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
            TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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
            int loaiNganSach = -1;
            if (LoaiNganSachSelected != null)
                loaiNganSach = int.Parse(LoaiNganSachSelected.ValueItem);
            //List<DonVi> listDonVi = _donViService.FindByCapPhatId(_sessionInfo.YearOfWork, _chungTuSelected.Id.ToString()).ToList();
            List<DonVi> listDonVi = _donViService.FindByCapPhatId2(_sessionInfo.YearOfWork, _chungTuSelected.Id.ToString(), loaiNganSach).ToList();
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
                string listDonViSelected = CheckboxSelectedToStringConvert.GetValueSelected(ListDonVi);
                int donViTinh = GetDonViTinh();

                int loaiNganSach = -1;
                if (LoaiNganSachSelected != null)
                    loaiNganSach = int.Parse(LoaiNganSachSelected.ValueItem);

                _reportData = _chungTuService.GetDataReportCapPhatThongTriDonVi(_sessionInfo.YearOfWork, _chungTuSelected.Id, listDonViSelected, donViTinh, loaiNganSach).ToList();
                double tongCapPhat = _reportData.Sum(x => x.CapPhat);

                Dictionary<string, object> data = new Dictionary<string, object>();
                //data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                data.Add("Cap1", GetLevelTitle(1));
                data.Add("Cap2", GetLevelTitle(2));

                //data.Add("Cap1", _sessionInfo.TenDonVi);
                //data.Add("Cap2", GetHeader2Report());
                data.Add("TieuDe1", TieuDe1.ToUpper());
                data.Add("TieuDe2", TieuDe2);
                data.Add("TieuDe3", TieuDe3);
                data.Add("Ve", string.Format("Tháng {0} năm {1}", DateTime.Now.Month, DateTime.Now.Year));
                data.Add("TongCapPhat", tongCapPhat);
                data.Add("Nam", _sessionInfo.YearOfWork);
                data.Add("Items", _reportData);
                data.Add("Header1", SelectedDonViTinh.DisplayItem);
                data.Add("TienBangChu", StringUtils.NumberToText(tongCapPhat * donViTinh, true));
                data.Add("ThoiGian", string.Format("{0}, {1}", _diaDiem, DateUtils.FormatDateReport(ReportDate)));
                data.Add("FormatNumber", new FormatNumber(donViTinh, isPdf ? ExportType.PDF : ExportType.EXCEL));
                AddChuKy(data, TypeChuKy.RPT_NS_CAPPHAT_DONVI);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_CP, ExportFileName.RPT_NS_CAPPHAT_THONGTRI_NHIEUDONVI);
                string fileNamePrefix = ExportFileName.RPT_NS_CAPPHAT_THONGTRI_NHIEUDONVI;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ReportCapPhatThongTriDonViQuery>(templateFileName, data);
                e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                        _exportService.Open(result, isPdf ? ExportType.PDF : ExportType.EXCEL);
                }
                else
                    _logger.Error(e.Error.Message);
                IsLoading = false;
            });
        }

        private string GetLevelTitle(int level)
        {
            var danhMucChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            var loaiDVBanHanh = danhMucChuKy.GetType().GetProperty($"LoaiDVBanHanh{level}").GetValue(danhMucChuKy)?.ToString() ?? string.Empty;
            var danhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToDictionary(dm => dm.IIDMaDanhMuc);

            return loaiDVBanHanh switch
            {
                LoaiDonViBanHanh.DON_VI_QUAN_LY => danhMuc.GetValueOrDefault(MaDanhMuc.DV_QUANLY, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_SU_DUNG => _sessionService.Current.TenDonVi,
                LoaiDonViBanHanh.CAP_QUAN_LY_TAI_CHINH => danhMuc.GetValueOrDefault(MaDanhMuc.DV_THONGTRI_BANHANH, new DanhMuc())?.SGiaTri ?? string.Empty,
                LoaiDonViBanHanh.DON_VI_DUOC_CHON => _chungTuSelected?.TenDonVi,
                LoaiDonViBanHanh.TUY_CHINH => danhMucChuKy.GetType().GetProperty($"TenDVBanHanh{level}").GetValue(danhMucChuKy)?.ToString() ?? string.Empty,
                _ => string.Empty
            };
        }

        public string GetHeader2Report()
        {
            //DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            //return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
            return _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).FirstOrDefault(n => n.IIDMaDanhMuc == MaDanhMuc.DV_THONGTRI_BANHANH)?.SGiaTri ?? string.Empty;

        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            var dmChyKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            data.Add("ThuaLenh1", dmChyKy != null ? dmChyKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ChucDanh1", dmChyKy != null ? dmChyKy.ChucDanh1MoTa : string.Empty);
            data.Add("GhiChuKy1", "(Ký, họ tên)");
            data.Add("Ten1", dmChyKy != null ? dmChyKy.Ten1MoTa : string.Empty);
            data.Add("ThuaLenh2", dmChyKy != null ? dmChyKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ChucDanh2", dmChyKy != null ? dmChyKy.ChucDanh2MoTa : string.Empty);
            data.Add("GhiChuKy2", "(Ký, họ tên)");
            data.Add("Ten2", dmChyKy != null ? dmChyKy.Ten2MoTa : string.Empty);
            data.Add("ThuaLenh3", dmChyKy != null ? dmChyKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh3", dmChyKy != null ? dmChyKy.ChucDanh3MoTa : string.Empty);
            data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
            data.Add("Ten3", dmChyKy != null ? dmChyKy.Ten3MoTa : string.Empty);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_CAPPHAT_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_CAPPHAT_DONVI;
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
