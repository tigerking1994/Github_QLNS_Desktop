using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using DanhMuc = VTS.QLNS.CTC.Core.Domain.DanhMuc;
namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanChiNamBHXH.PrintReport
{
    public class PrintNhanVaQuyetToanChiNamBHXHViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ICollectionView _donViCollectionView;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsBaoCaoGhiChuService _ghiChuService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly ITnDtdnChungTuService _tnDtdnChungTuService;
        private readonly ITnDtdnChungTuChiTietService _tnDtdnChungTuChiTietService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;
        private List<TnDtdnChungTuChiTietModel> _listChungTuChiTiet;

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
            get => $"Báo cáo tình hình nhận và quyết toán kinh phí (Mẫu 01/BHXH-{_sessionInfo.YearOfWork})";
        }

        public override string Title
        {
            get => $"Báo cáo tình hình nhận và quyết toán kinh phí (Mẫu 01/BHXH-{_sessionInfo.YearOfWork})";
        }

        public override string Description
        {
            get => $"Báo cáo tình hình nhận và quyết toán kinh phí (Mẫu 01/BHXH-{_sessionInfo.YearOfWork})";
        }

        public override Type ContentType => typeof(PrintNhanVaQuyetToanChiNamBHXH);
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

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
                OnPropertyChanged(nameof(ListDonVi));
                OnPropertyChanged(nameof(LabelSelectedCountDonVi));
                OnPropertyChanged(nameof(SelectAllDonVi));

            }
        }

        private List<DataReportDynamic> _lstDataDynamic = new List<DataReportDynamic>();

        private ObservableCollection<ComboboxItem> _reportTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ReportTypes
        {
            get => _reportTypes;
            set => SetProperty(ref _reportTypes, value);
        }

        private ComboboxItem _reportTypeSelected;

        public ComboboxItem ReportTypeSelected
        {
            get => _reportTypeSelected;
            set
            {
                SetProperty(ref _reportTypeSelected, value);
                OnPropertyChanged(nameof(IsEnableInMotTo));
            }
        }

        public bool IsEnableInMotTo => ReportTypeSelected != null && ReportTypeSelected.ValueItem.Equals("2");
        public bool IsShowLoaiDuLieu { get; set; }

        public bool IsInMotTo { get; set; }
        private DonVi DonViRoot;

        public PrintNhanVaQuyetToanChiNamBHXHViewModel(INsDonViService nsDonViService, IExportService exportService,
            IDanhMucService danhMucService,
            INsBaoCaoGhiChuService ghiChuService,
            IDmChuKyService dmChuKyService,
            ISessionService sessionService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            ITnDtdnChungTuChiTietService tnDtdnChungTuChiTietService,
            ITnDtdnChungTuService tnDtdnChungTuService,
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
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _ghiChuService = ghiChuService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _tnDtdnChungTuChiTietService = tnDtdnChungTuChiTietService;
            _tnDtdnChungTuService = tnDtdnChungTuService;

            NsBaoCaoGhiChuDialogViewModel = nsBaoCaoGhiChuDialogViewModel;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            ExportExcelActionCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportPdfActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportSignatureActionCommand = new RelayCommand(ExportSignature);
            PrintActionCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }


        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            _isInTheoTongHop = false;
            InMotToChecked = false;
            InitReportDefaultDate();
            Clear();
            LoadDonVi();
            LoadTypeChuKy();
            LoadTitleFirst();
            LoadCatUnitTypes();
            LoadReportType();
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
            DonViRoot = _nsDonViService.FindByCondition(NSConstants.ZERO, StatusType.ACTIVE, _sessionInfo.YearOfWork).FirstOrDefault();
        }

        public void Clear()
        {
            _donViCollectionView = null;
        }

        private void LoadTypeChuKy()
        {
            _typeChuky = TypeChuKy.RPT_BH_QTC_NHAN_VA_QTKP;
        }

        public void LoadTitleFirst()
        {
            string first = $"BÁO CÁO";
            string second = $"TÌNH HÌNH NHẬN VÀ QUYẾT TOÁN KINH PHÍ";

            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            _dmChuKy ??= new DmChuKy();
            _dmChuKy.TieuDe1MoTa = string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa) ? first : _dmChuKy.TieuDe1MoTa;
            _dmChuKy.TieuDe2MoTa = string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa) ? second : _dmChuKy.TieuDe2MoTa;
            TxtTitleFirst = _dmChuKy.TieuDe1MoTa;
            TxtTitleSecond = _dmChuKy.TieuDe2MoTa;
            TxtTitleThird = _dmChuKy.TieuDe3MoTa;
        }

        public virtual void LoadReportType()
        {
            var data = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Chi tiết đơn vị ", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Tổng hợp đơn vị ", ValueItem = "2"},
            };

            ReportTypes = new ObservableCollection<ComboboxItem>(data);
            ReportTypeSelected = ReportTypes.ElementAt(0);
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
            var iTrangThai = StatusType.ACTIVE;
            var lstMaDonVi = _nsDonViService.FindAllDonViByBaoCaoThamDinhBH(yearOfWork);
            if (lstMaDonVi.IsEmpty()) return new ObservableCollection<CheckBoxItem>();
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.ITrangThai == iTrangThai && lstMaDonVi.Contains(x.IIDMaDonVi));
            //bao cao so nhu cau tong hop
            ListUnit = _nsDonViService.FindByCondition(predicate);
            var result = ListUnit.Select(item => new CheckBoxItem
            {
                ValueItem = item.IIDMaDonVi,
                ValueItem2 = item.iCapDonVi?.ToString(),
                DisplayItem = string.Join("-", item.IIDMaDonVi, item.TenDonVi),
                NameItem = item.TenDonVi,

            }).OrderBy(item => item.ValueItem);
            return new ObservableCollection<CheckBoxItem>(result);
        }

        public void LoadDonVi()
        {
            ListDonVi = LoadNsDonVisTheoLoaiChungTu();
            ListDonVi = new ObservableCollection<CheckBoxItem>(ListDonVi.GroupBy(item => item.ValueItem).Select(item => item.First()));

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

        public void OnExport(ExportType exportType)
        {
            OnPrintRepportByAgency(exportType);
        }


        public void OnPrintRepportByAgency(ExportType exportType)
        {
            try
            {

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int yearOfWork = _sessionService.Current.YearOfWork;
                    int donViTinh = int.Parse(CatUnitTypeSelected.ValueItem);
                    List<ExportResult> results = new List<ExportResult>();
                    var lstDonVi = ListDonVi.Where(x => x.IsChecked).ToList();
                    string sCap1 = GetLevelTitle(_dmChuKy, 1);
                    string sCap2 = GetLevelTitle(_dmChuKy, 2);

                    foreach (var item in lstDonVi)
                    {
                        List<BaoCaoNhanVaQuyetToanKinhPhi> lstData = new List<BaoCaoNhanVaQuyetToanKinhPhi>();
                        Dictionary<string, object> data = new Dictionary<string, object>();

                        string sMaDonVi = string.Empty;
                        if(ReportTypeSelected.ValueItem == "2")
                        {
                            sMaDonVi = string.Join(",", lstDonVi.Select(x => x.ValueItem));
                            data.Add("TenDonVi", string.Empty);

                        }
                        else
                        {
                            sMaDonVi = item.ValueItem;
                            data.Add("TenDonVi", item.NameItem);

                        }
                        lstData = _tnDtdnChungTuService.GetBaoCaoNhanVaQuyetToanKinhPhis(sMaDonVi, yearOfWork,
                                                                                            donViTinh).ToList();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        data.Add("YearWork", yearOfWork);
                        data.Add("Cap1", !string.IsNullOrEmpty(sCap1) ? sCap1 : "BỘ QUỐC PHÒNG");
                        data.Add("Cap2", !string.IsNullOrEmpty(sCap2) ? sCap2 : (_sessionInfo.TenDonVi));
                        data.Add("DonVi", _sessionInfo.TenDonVi.ToUpper());
                        data.Add("Items", lstData);
                        data.Add("TieuDe1", TxtTitleFirst);
                        data.Add("TieuDe2", TxtTitleSecond);
                        data.Add("TieuDe3", TxtTitleThird);
                        data.Add("FormatNumber", formatNumber);

                        AddChuKy(data, _typeChuky);
                        AddNgayDiaDiem(data);
                        data.Add("Year", _sessionInfo.YearOfWork);
                        data.Add("Count", 100000);

                        //data.Add("TienBangChu", StringUtils.NumberToText(tienTong.Value * donViTinh, true));
                        data.Add("h2", "Đơn vị tính: " + (CatUnitTypeSelected != null ? CatUnitTypeSelected.DisplayItem : ""));
                        string templateFileName;
                        templateFileName = GetTemplate(ExportFileName.RPT_BH_QTC_NHAN_VA_QTKP);
                        string fileNamePrefix;
                        fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string filename = StringUtils.CreateExportFileName(fileNamePrefix + "_" + _sessionInfo.TenDonVi);
                        var xlsFile = _exportService.Export<BaoCaoNhanVaQuyetToanKinhPhi>(templateFileName, data);
                        results.Add(new ExportResult("NHẬN VÀ QUYẾT TOÁN KINH PHÍ BHXH " + _sessionInfo.YearOfWork, filename, null, xlsFile));
                        if (ReportTypeSelected.ValueItem == "2") break;

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

        private List<DataReportDynamic> GetDataDefault()
        {
            List<DataReportDynamic> result = new List<DataReportDynamic>();
            foreach (var item in _lstDataDynamic)
            {
                result.Add(new DataReportDynamic());
            }

            return result;
        }
        public void ExportSignature(object param)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(_typeChuky) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
            {
                chuKyModel.IdType = _typeChuky;
                chuKyModel.TieuDe1MoTa = TxtTitleFirst;
                chuKyModel.TieuDe2MoTa = TxtTitleSecond;
                chuKyModel.TieuDe3MoTa = TxtTitleThird;
            }
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
            return Path.Combine(ExportPrefix.PATH_BH_QTT, input + FileExtensionFormats.Xlsx);
        }

        private class GhiChu
        {
            public string Content { get; set; }
            //public string SGhiChu => $"- {Content}";
            public string SGhiChu => Content;
        }
    }
}
