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

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Plan
{
    public class PrintReportCompareDemandCheckViewModel : ViewModelBase
    {
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private INsDonViService _nSDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private ISktSoLieuService _sktSoLieuService;
        private ISktMucLucService _sktMucLucService;
        private INsPhongBanService _phongBanService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsNguoiDungDonViService _nsNguoiDungDonViService;
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;

        public override Type ContentType => typeof(View.Budget.DemandCheck.Plan.PrintReportCompareDemandCheck);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportDuToanDauNamSoSanhQuery> DataReport;
        public List<DonVi> ListDonViData;
        public double TongQuyetToan;
        public double TongDuToanDauNam;
        public double TongSoKiemTra;
        public double TongDuToan;
        public double TongTang;
        public double TongGiam;

        private ObservableCollection<ComboboxItem> _loaiBaoCao;
        public ObservableCollection<ComboboxItem> LoaiBaoCao
        {
            get => _loaiBaoCao;
            set => SetProperty(ref _loaiBaoCao, value);
        }

        private ComboboxItem _selectedLoaiBaoCao;
        public ComboboxItem SelectedLoaiBaoCao
        {
            get => _selectedLoaiBaoCao;
            set
            {
                SetProperty(ref _selectedLoaiBaoCao, value);
                IsInTheoTongHop = false;
                OnPropertyChanged(nameof(IsShowInTheoTongHop));
            }
        }

        private ObservableCollection<ComboboxItem> _dataKieuGiay;
        public ObservableCollection<ComboboxItem> DataKieuGiay
        {
            get => _dataKieuGiay;
            set => SetProperty(ref _dataKieuGiay, value);
        }

        private ComboboxItem _selectedKieuGiay;
        public ComboboxItem SelectedKieuGiay
        {
            get => _selectedKieuGiay;
            set => SetProperty(ref _selectedKieuGiay, value);
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

        private string _loaiChungTu;
        public string LoaiChungTu
        {
            get => _loaiChungTu;
            set => SetProperty(ref _loaiChungTu, value);
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

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
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
                    var selected = ListDonVi.Select(item => item.IsChecked).Distinct().ToList();
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

        private static void SelectAll(bool select, ObservableCollection<CheckBoxItem> models)
        {
            foreach (var model in models)
            {
                model.IsChecked = select;
            }
        }

        public bool IsEnableButtonPrint
        {
            get
            {
                if (ListDonVi != null && ListDonVi.Where(n => n.IsChecked).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
                }
            }
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
                LoaiChungTu = string.Empty;
                if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                {
                    LoaiChungTu = VoucherType.NSSD_Key;
                }
                else
                {
                    LoaiChungTu = VoucherType.NSBD_Key;
                }
            }
        }

        private List<ComboboxItem> _bQuanLy;
        public List<ComboboxItem> BQuanLy
        {
            get => _bQuanLy;
            set => SetProperty(ref _bQuanLy, value);
        }

        private ComboboxItem _selectedBQuanLy;
        public ComboboxItem SelectedBQuanLy
        {
            get => _selectedBQuanLy;
            set => SetProperty(ref _selectedBQuanLy, value);
        }

        private bool _isInTheoTongHop;
        public bool IsInTheoTongHop
        {
            get => _isInTheoTongHop;
            set
            {
                SetProperty(ref _isInTheoTongHop, value);
                LoadDonVi();
            }
        }

        public bool IsShowInTheoTongHop => _sessionService.Current.IsQuanLyDonViCha && SelectedLoaiBaoCao != null && !SelectedLoaiBaoCao.ValueItem.Equals(Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI);
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public PrintReportCompareDemandCheckViewModel(
             IDanhMucService danhMucService,
             IMapper mapper,
             INsDonViService nSDonViService,
             ISessionService sessionService,
             ILog logger,
             ISktSoLieuService sktSoLieuService,
             ISktMucLucService sktMucLucService,
             INsPhongBanService phongBanService,
             INsMucLucNganSachService iNsMucLucNganSachService,
             IExportService exportService,
             IDmChuKyService dmChuKyService,
             INsNguoiDungDonViService nsNguoiDungDonViService,
             DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _danhMucService = danhMucService;
            _mapper = mapper;
            _nSDonViService = nSDonViService;
            _sessionService = sessionService;
            _exportService = exportService;
            _phongBanService = phongBanService;
            _sktSoLieuService = sktSoLieuService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _sktMucLucService = sktMucLucService;
            _dmChuKyService = dmChuKyService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => PrintPDF());
            PrintExcelCommand = new RelayCommand(o => PrintExcel());
            PrintBrowserCommand = new RelayCommand(o => OnPrintBrowser());
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                _isInTheoTongHop = false;
                InitReportDefaultDate();
                LoadCombobox();
                LoadVoucherTypes();
                LoadBQuanLy();
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                TieuDe1 = _dmChuKy != null ? _dmChuKy.TieuDe1MoTa : string.Empty;
                TieuDe2 = _dmChuKy != null ? _dmChuKy.TieuDe2MoTa : string.Empty;
                TieuDe3 = _dmChuKy != null ? _dmChuKy.TieuDe3MoTa : string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadBQuanLy()
        {
            var predicate = PredicateBuilder.True<DmBQuanLy>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            List<DmBQuanLy> listPhongBan = _phongBanService.FindByCondition(predicate);
            _bQuanLy = _mapper.Map<List<ComboboxItem>>(listPhongBan);
            if (_bQuanLy.Count() > 0)
            {
                _bQuanLy.Insert(0, new ComboboxItem("Tất cả", string.Empty));
                SelectedBQuanLy = _bQuanLy.FirstOrDefault();
            }
        }


        private void CalculateTotal()
        {
            TongQuyetToan = 0;
            TongDuToanDauNam = 0;
            TongSoKiemTra = 0;
            TongDuToan = 0;
            TongTang = 0;
            TongGiam = 0;

            List<ReportDuToanDauNamSoSanhQuery> listChildren = DataReport.Where(x => !x.IsHangCha).ToList();
            foreach (ReportDuToanDauNamSoSanhQuery item in listChildren)
            {
                TongQuyetToan += item.QuyetToan;
                TongDuToanDauNam += item.DuToan;
                TongSoKiemTra += item.TuChi;
                TongDuToan += item.TuChi2;
                TongTang += item.Tang;
                TongGiam += item.Giam;
            };
        }

        private void CalculateParent(ReportDuToanDauNamSoSanhQuery currentItem, ReportDuToanDauNamSoSanhQuery selfItem)
        {
            var parentItem = DataReport.Where(x => x.IDMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.QuyetToan += selfItem.QuyetToan;
            parentItem.DuToan += selfItem.DuToan;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChi2 += selfItem.TuChi2;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateData()
        {
            DataReport.Where(x => x.IsHangCha).Select(x => { x.QuyetToan = 0; x.DuToan = 0; x.TuChi = 0; x.TuChi2 = 0; return x; }).ToList();
            foreach (var item in DataReport.Where(x => !x.IsHangCha && (x.QuyetToan != 0 || x.DuToan != 0 || x.TuChi != 0 || x.TuChi2 != 0)))
            {
                CalculateParent(item, item);
            }
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
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

        private void PrintReportChiTietDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_SOSANHDONVI);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    foreach (string idDonVi in listIdDonVi)
                    {
                        DonVi itemDonVi = ListDonViData.Where(n => n.IIDMaDonVi == idDonVi).FirstOrDefault();
                        string loai = "2,4";
                        if (itemDonVi == null)
                            return;
                        if (itemDonVi.Loai == LoaiDonVi.ROOT)
                        {
                            loai = "3";
                        }

                        string listLNS = "";
                        if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                        {
                            var temp = _sktSoLieuService.FindByCondition(x => x.IIdMaDonVi == idDonVi && x.INamLamViec == _sessionService.Current.YearOfWork && x.ILoaiChungTu == LoaiChungTu);
                            var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(temp.Select(x => x.SLns).ToList(), _sessionService.Current.YearOfWork);
                            if (lns.Any(x => x.IdPhongBan == SelectedBQuanLy.ValueItem))
                            {
                                listLNS = string.Join(",", lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns));
                            }
                            else
                            {
                                listLNS = "-1";
                            }
                        }
                        if (VoucherTypeSelected != null && VoucherTypeSelected.ValueItem == VoucherType.NSSD_Key)
                        {
                            DataReport = _sktSoLieuService.GetDataReportDuToanDauNamSoSanh_1(loai, idDonVi, _sessionService.Current.YearOfWork, donViTinh, LoaiChungTu, listLNS).ToList();
                        }
                        else
                        {
                            DataReport = _sktSoLieuService.GetDataReportDuToanDauNamSoSanhAll_1(loai, idDonVi, _sessionService.Current.YearOfWork, donViTinh, LoaiChungTu, listLNS).ToList();
                        }

                        List<ReportDuToanDauNamSoSanhQuery> listHeader = new List<ReportDuToanDauNamSoSanhQuery>();
                        CalculateTotal();
                        if (DataReport != null && DataReport.Count > 0)
                        {
                            List<string> listKyHieuParent = StringUtils.GetListKyHieuParent(DataReport.Select(n => n.KyHieu).ToList());
                            List<NsSktMucLuc> listParent = _sktMucLucService.FindByKyHieu(_sessionService.Current.YearOfWork, listKyHieuParent).ToList();
                            if (listParent != null && listParent.Count > 0)
                            {
                                foreach (NsSktMucLuc item in listParent)
                                {
                                    listHeader.Add(new ReportDuToanDauNamSoSanhQuery
                                    {
                                        Id = item.Id,
                                        IDMucLuc = item.IIDMLSKT,
                                        STT = item.SSTT,
                                        MoTa = item.SMoTa,
                                        IdParent = item.IIDMLSKTCha.HasValue ? item.IIDMLSKTCha.Value : Guid.Empty,
                                        STTBC = item.SSttBC,
                                        M = item.SM,
                                        IsHangCha = true,
                                        KyHieu = item.SKyHieu
                                    });
                                }
                                foreach (ReportDuToanDauNamSoSanhQuery item in DataReport)
                                {
                                    int index = listHeader.IndexOf(listHeader.Where(n => n.IDMucLuc == item.IdParent).FirstOrDefault());
                                    ReportDuToanDauNamSoSanhQuery itemCheck = listHeader.Where(n => n.IDMucLuc == item.IDMucLuc).FirstOrDefault();
                                    if (itemCheck != null)
                                    {
                                        listHeader.Remove(itemCheck);
                                    }
                                    if (index >= 0)
                                    {
                                        listHeader.Insert(index + 1, item);
                                    }
                                }
                            }
                            listHeader = listHeader.OrderBy(n => n.STTBC).ToList();
                            DataReport = new List<ReportDuToanDauNamSoSanhQuery>(listHeader);
                        }
                        CalculateData();
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                        //data.Add("LoaiBaoCaoString", SelectedLoaiBaoCao.ValueItem.ToString());
                        data.Add("TenDonVi", "Đơn vi:" + itemDonVi.TenDonVi);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("HeaderCap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                        data.Add("HeaderCap2", GetHeader2Report());
                        data.Add("TieuDe1", TieuDe1.ToUpper());
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        data.Add("TongQuyetToan", TongQuyetToan);
                        data.Add("TongDuToanDauNam", TongDuToanDauNam);
                        data.Add("TongSoKiemTra", TongSoKiemTra);
                        data.Add("TongDuToan", TongDuToan);
                        data.Add("TongTang", TongTang);
                        data.Add("TongGiam", TongGiam);
                        //data.Add("ThoiGian", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        data.Add("ThoiGian", ReportDate.HasValue ? ReportDate.Value.ToString("dd/MM/yyyy HH:mm") : "");
                        data.Add("NguoiIn", _sessionService.Current.Principal);
                        data.Add("Nam_2", _sessionService.Current.YearOfWork - 2);
                        data.Add("Nam_1", _sessionService.Current.YearOfWork - 1);
                        data.Add("Nam", _sessionService.Current.YearOfWork);
                        data.Add("Items", DataReport);
                        data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        fileNamePrefix = "rpt_ReportDuToanDauNam_SoSanh";
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<ReportDuToanDauNamSoSanhQuery>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public string GetPath(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DTDN, input);
        }

        private void OnPrintReportBudget(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    if (listIdDonVi == null || listIdDonVi.Count == 0)
                        return;
                    List<SktSoLieuChiTietMlnsQuery> dataReport = new List<SktSoLieuChiTietMlnsQuery>();
                    dataReport = _sktSoLieuService.GetDataReportChiNganSach(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget,
                                            string.Join(",", listIdDonVi), LoaiChungTu);

                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(dataReport.Select(x => x.LNS).ToList(), _sessionService.Current.YearOfWork);
                        var listLNS = lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns).ToList();
                        dataReport = dataReport.Where(x => listLNS.Contains(x.LNS)).ToList();
                    }

                    int donViTinh = GetDonViTinh();
                    dataReport.Select(n => { n.ChiTiet = n.ChiTiet / donViTinh; return n; }).ToList();
                    double tongtien = dataReport.Select(n => n.ChiTiet.HasValue ? n.ChiTiet.Value : 0).Sum();
                    CalculateDataReport(ref dataReport);
                    dataReport = dataReport.Where(n => (!n.BHangCha || (n.BHangCha && (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.TM)))) && n.ChiTiet != 0 && n.ChiTiet.HasValue).ToList();
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                    List<SktSoLieuChiTietMlnsQuery> listResult = new List<SktSoLieuChiTietMlnsQuery>();
                    foreach (SktSoLieuChiTietMlnsQuery item in dataReport)
                    {
                        if (item.BHangCha && string.IsNullOrEmpty(item.TM) && !string.IsNullOrEmpty(item.SKT_KyHieu))
                        {
                            SktSoLieuChiTietMlnsQuery itemParent = new SktSoLieuChiTietMlnsQuery();
                            SktSoLieuChiTietMlnsQuery itemParentSub = new SktSoLieuChiTietMlnsQuery();
                            string header1 = string.Empty;
                            string header2 = string.Empty;
                            _sktSoLieuService.GetHeaderReportChiNganSach(item.SKT_KyHieu, _sessionService.Current.YearOfWork, ref header1, ref header2);
                            itemParent.M = header1.Length < 36 ? header1 + "                                            " : header1;
                            itemParentSub.M = header2.Length < 36 ? header2 + "                                            " : header2;
                            if (!string.IsNullOrEmpty(itemParent.M))
                            {
                                itemParent.BHangCha = true;
                                listResult.Add(itemParent);
                            }
                            if (!string.IsNullOrEmpty(itemParentSub.M))
                            {
                                itemParentSub.BHangCha = true;
                                listResult.Add(itemParentSub);
                            }
                            item.M = "     " + item.M;
                            listResult.Add(item);
                        }
                        else
                        {
                            item.M = "     " + item.M;
                            listResult.Add(item);
                        }
                    }
                    listResult = listResult.Where(n => string.IsNullOrEmpty(n.TNG)).ToList();
                    listResult.Where(n => !string.IsNullOrEmpty(n.NG) && n.BHangCha).Select(n => n.BHangCha = false).ToList();
                    listResult.Where(n => n.BHangCha && !string.IsNullOrEmpty(n.TM)).Select(n => { n.M = string.Empty; return n; }).ToList();
                    listResult.Where(n => !n.BHangCha && (!string.IsNullOrEmpty(n.TTM) || !string.IsNullOrEmpty(n.NG))).Select(n => { n.M = string.Empty; n.TM = string.Empty; return n; }).ToList();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", listResult);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("TongTien", tongtien);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    string templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_CHINGANSACH);
                    string fileNamePrefix = "rptDuToanDauNamChiNganSach";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<SktSoLieuChiTietMlnsQuery>(templateFileName, data);
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDataReport(ref List<SktSoLieuChiTietMlnsQuery> dataReport)
        {
            dataReport.Where(x => x.BHangCha).Select(x => { x.ChiTiet = 0; x.UocThucHien = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; x.ChuaPhanCap = 0; return x; }).ToList();
            foreach (var item in dataReport.Where(x => !x.BHangCha && (x.ChiTiet != 0 || x.UocThucHien != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0 || x.ChuaPhanCap != 0)))
            {
                CalculateParent(ref dataReport, item, item);
            }
        }

        private void CalculateParent(ref List<SktSoLieuChiTietMlnsQuery> dataReport, SktSoLieuChiTietMlnsQuery currentItem, SktSoLieuChiTietMlnsQuery selfItem)
        {
            var parentItem = dataReport.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.ChiTiet += selfItem.ChiTiet;
            parentItem.UocThucHien += selfItem.UocThucHien;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            parentItem.ChuaPhanCap += selfItem.ChuaPhanCap;
            if (!string.IsNullOrEmpty(selfItem.SKT_KyHieu))
                parentItem.SKT_KyHieu = selfItem.SKT_KyHieu;
            CalculateParent(ref dataReport, parentItem, selfItem);
        }

        private void PrintReportTongHopDonVi(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<string> listIdDonVi = ListDonVi.Where(n => n.IsChecked).Select(n => n.ValueItem).ToList();
                    if (listIdDonVi == null || listIdDonVi.Count == 0)
                        return;
                    int donViTinh = GetDonViTinh();
                    bool isPrintPhuLuc = SelectedKieuGiay != null && SelectedKieuGiay.ValueItem == KieuGiay.PHU_LUC;
                    string loai = "2,4";
                    if (IsInTheoTongHop)
                    {
                        loai = "3";
                    }
                    _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();

                    string listLNS = "";
                    if (!string.IsNullOrEmpty(SelectedBQuanLy.ValueItem))
                    {
                        var temp = _sktSoLieuService.FindByCondition(x => listIdDonVi.Contains(x.IIdMaDonVi) && x.INamLamViec == _sessionService.Current.YearOfWork && x.ILoaiChungTu == LoaiChungTu);
                        var lns = _iNsMucLucNganSachService.FindByListLnsDonVi(temp.Select(x => x.SLns).ToList(), _sessionService.Current.YearOfWork);
                        if (lns.Any(x => x.IdPhongBan == SelectedBQuanLy.ValueItem))
                        {
                            listLNS = string.Join(",", lns.Where(x => x.IdPhongBan == SelectedBQuanLy.ValueItem).Select(x => x.Lns));
                        }
                        else
                        {
                            listLNS = "-1";
                        }
                    }
                    DataReport = _sktSoLieuService.GetDataReportDuToanDauNamSoSanh_1(loai, string.Join(",", listIdDonVi),
                                    _sessionService.Current.YearOfWork, donViTinh, LoaiChungTu, listLNS).ToList();
                    List<ReportDuToanDauNamSoSanhQuery> listHeader = new List<ReportDuToanDauNamSoSanhQuery>();
                    CalculateTotal();
                    if (DataReport != null && DataReport.Count > 0)
                    {
                        List<string> listKyHieuParent = StringUtils.GetListKyHieuParent(DataReport.Select(n => n.KyHieu).ToList());
                        List<NsSktMucLuc> listParent = _sktMucLucService.FindByKyHieu(_sessionService.Current.YearOfWork, listKyHieuParent).ToList();
                        if (listParent != null && listParent.Count > 0)
                        {
                            foreach (NsSktMucLuc item in listParent)
                            {
                                listHeader.Add(new ReportDuToanDauNamSoSanhQuery
                                {
                                    Id = item.Id,
                                    IDMucLuc = item.IIDMLSKT,
                                    STT = item.SSTT,
                                    MoTa = item.SMoTa,
                                    IdParent = item.IIDMLSKTCha.HasValue ? item.IIDMLSKTCha.Value : Guid.Empty,
                                    STTBC = item.SSttBC,
                                    M = item.SM,
                                    IsHangCha = true,
                                    KyHieu = item.SKyHieu
                                });
                            }
                            foreach (ReportDuToanDauNamSoSanhQuery item in DataReport)
                            {
                                int index = listHeader.IndexOf(listHeader.Where(n => n.IDMucLuc == item.IdParent).FirstOrDefault());
                                ReportDuToanDauNamSoSanhQuery itemCheck = listHeader.Where(n => n.IDMucLuc == item.IDMucLuc).FirstOrDefault();
                                if (itemCheck != null)
                                {
                                    listHeader.Remove(itemCheck);
                                }
                                if (index >= 0)
                                {
                                    listHeader.Insert(index + 1, item);
                                }
                            }
                        }
                        listHeader = listHeader.OrderBy(n => n.STTBC).ToList();
                        DataReport = new List<ReportDuToanDauNamSoSanhQuery>(listHeader);
                    }
                    CalculateData();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TenDonVi", "Tổng Hợp Đơn Vị");
                    data.Add("HeaderCap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                    data.Add("HeaderCap2", GetHeader2Report());
                    data.Add("TieuDe1", TieuDe1.ToUpper());
                    data.Add("TieuDe2", TieuDe2);
                    data.Add("TieuDe3", TieuDe3);
                    data.Add("TongQuyetToan", TongQuyetToan);
                    data.Add("TongDuToanDauNam", TongDuToanDauNam);
                    data.Add("TongSoKiemTra", TongSoKiemTra);
                    data.Add("TongDuToan", TongDuToan);
                    data.Add("TongTang", TongTang);
                    data.Add("TongGiam", TongGiam);
                    //data.Add("ThoiGian", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    data.Add("ThoiGian", ReportDate.HasValue ? ReportDate.Value.ToString("dd/MM/yyyy HH:mm") : "");
                    data.Add("NguoiIn", _sessionService.Current.Principal);
                    data.Add("Nam_2", _sessionService.Current.YearOfWork - 2);
                    data.Add("Nam_1", _sessionService.Current.YearOfWork - 1);
                    data.Add("Nam", _sessionService.Current.YearOfWork);
                    data.Add("Items", DataReport);
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                    data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                    data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                    data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                    data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                    data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                    data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                    data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                    data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                    string templateFileName = GetPath(ExportFileName.RPT_NS_DUTOANDAUNAM_SOSANHDONVI);
                    string fileNamePrefix = "rpt_ReportDuToanDauNam_TongHop_SoSanh";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportDuToanDauNamSoSanhQuery>(templateFileName, data);
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintBrowser()
        {
            try
            {
                string loaiBaoCao = SelectedLoaiBaoCao != null ? SelectedLoaiBaoCao.ValueItem : "";
                switch (loaiBaoCao)
                {
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportChiTietDonVi(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP:
                        PrintReportTongHopDonVi(ExportType.PDF);
                        break;
                    case Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_NGAN_SACH:
                        OnPrintReportBudget(ExportType.PDF);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintExcel()
        {
            try
            {
                string loaiBaoCao = SelectedLoaiBaoCao != null ? SelectedLoaiBaoCao.ValueItem : "";
                switch (loaiBaoCao)
                {
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportChiTietDonVi(ExportType.EXCEL);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP:
                        PrintReportTongHopDonVi(ExportType.EXCEL);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_NGAN_SACH:
                        OnPrintReportBudget(ExportType.EXCEL);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void PrintPDF()
        {
            try
            {
                string loaiBaoCao = SelectedLoaiBaoCao != null ? SelectedLoaiBaoCao.ValueItem : "";
                switch (loaiBaoCao)
                {
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI:
                        PrintReportChiTietDonVi(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP:
                        PrintReportTongHopDonVi(ExportType.PDF);
                        break;
                    case VTS.QLNS.CTC.Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_NGAN_SACH:
                        OnPrintReportBudget(ExportType.PDF);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi!.ToLower());
        }

        private List<NguoiDungDonVi> GetListDonViPhanQuyen()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.IIDMaNguoiDung == _sessionService.Current.Principal);
            List<NguoiDungDonVi> listNguoiDungDonVi = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        public void LoadCombobox()
        {
            LoaiBaoCao = new ObservableCollection<ComboboxItem>();
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI, ValueItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI });
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP, ValueItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_TONG_HOP });
            LoaiBaoCao.Add(new ComboboxItem { DisplayItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_NGAN_SACH, ValueItem = Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHI_NGAN_SACH });
            SelectedLoaiBaoCao = LoaiBaoCao.FirstOrDefault();

            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = KieuGiay.BIEU_TRINH_KY, DisplayItem = KieuGiay.BIEU_TRINH_KY });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = KieuGiay.PHU_LUC, DisplayItem = KieuGiay.PHU_LUC });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
            LoadDonVi();
        }

        private void LoadDonVi()
        {
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListDonViPhanQuyen();
            DonVi donvi0 = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            ListDonViData = new List<DonVi>();
            if (SelectedLoaiBaoCao != null && !SelectedLoaiBaoCao.ValueItem.Equals(Utility.LoaiBaoCao.DU_TOAN_DAU_NAM_CHITIET_DONVI))
            {
                if (IsInTheoTongHop)
                {
                    ListDonViData.Add(donvi0);
                }
                else
                {
                    var lstIdDonViDuocXem = ListIdDonViDuocXem();
                    var predicateDv = PredicateBuilder.True<DonVi>();
                    predicateDv = predicateDv.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicateDv = predicateDv.And(x => !x.Loai.Equals(LoaiDonVi.ROOT));
                    predicateDv = predicateDv.And(x => lstIdDonViDuocXem.Contains(x.IIDMaDonVi));
                    ListDonViData = _nSDonViService.FindByCondition(predicateDv).ToList();
                }
            }
            else
            {
                ListDonViData = _nSDonViService.FindDonViHasDataSktSoLieuChiTiet(_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget,
                    _sessionService.Current.Budget, LoaiChungTu, 0).ToList();
                if (listNguoiDungDonVi != null && donvi0 != null && !listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(donvi0.IIDMaDonVi))
                {
                    ListDonViData = ListDonViData.Where(x => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(x.IIDMaDonVi)).ToList();
                }
            }
            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(ListDonViData);
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
                        OnPropertyChanged(nameof(IsEnableButtonPrint));
                    }
                };
            }
            OnPropertyChanged(nameof(ListDonVi));
            OnPropertyChanged(nameof(SelectedCountDonVi));
        }

        private List<string> ListIdDonViDuocXem()
        {
            string loaiChungTu = string.Empty;
            loaiChungTu = (VoucherTypeSelected?.ValueItem == VoucherType.NSSD_Key) ? VoucherType.NSSD_Key : VoucherType.NSBD_Key;

            List<DonViPlanBeginYearQuery> data = _nSDonViService.FindPlanBeginYearByConditon
                (_sessionService.Current.YearOfWork, _sessionService.Current.YearOfBudget, _sessionService.Current.Budget, loaiChungTu, 0, _sessionService.Current.Principal).ToList();
            return data.Select(x => x.Id_DonVi).Distinct().ToList();
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_SOSANH_SOKIEMTRA;
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
