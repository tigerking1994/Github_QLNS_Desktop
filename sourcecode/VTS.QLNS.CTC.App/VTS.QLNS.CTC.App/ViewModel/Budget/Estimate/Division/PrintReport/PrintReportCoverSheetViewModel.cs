using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class PrintReportCoverSheetViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DtChungTuChiTietModel>
    {
        private ISessionService _sessionService;
        private ISktSoLieuService _sktSoLieuService;
        private INsDtChungTuChiTietService _chungTuChiTietService;
        private IExportService _exportService;
        private INsDonViService _nSDonViService;
        private INsDtChungTuService _dtChungTuService;
        private readonly IDanhMucService _danhMucService;
        private readonly IDmChuKyService _dmChuKyService;
        private readonly INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private readonly INsMucLucNganSachService _iNsMucLucNganSachService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private DmChuKy _dmChuKy;
        private string _diaDiem;
        private SessionInfo _sessionInfo;
        private DuToanTongChiTieu _tongChiTieu;

        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportCoverSheet);
        public List<ReportChiTieuDuToanQuery> ListDataToBia { get; set; }

        private ObservableCollection<ComboboxItem> _dataDotIn;
        public ObservableCollection<ComboboxItem> DataDotIn
        {
            get => _dataDotIn;
            set => SetProperty(ref _dataDotIn, value);
        }

        private ComboboxItem _selectedDotIn;
        public ComboboxItem SelectedDotIn
        {
            get => _selectedDotIn;
            set
            {
                if (SetProperty(ref _selectedDotIn, value))
                {
                    LoadTitle();
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

        private ObservableCollection<ComboboxItem> _dataInToiMuc;
        public ObservableCollection<ComboboxItem> DataInToiMuc
        {
            get => _dataInToiMuc;
            set => SetProperty(ref _dataInToiMuc, value);
        }

        private ComboboxItem _selectedInToiMuc;
        public ComboboxItem SelectedInToiMuc
        {
            get => _selectedInToiMuc;
            set => SetProperty(ref _selectedInToiMuc, value);
        }

        private bool _isLuyKeToiDot;
        public bool IsLuyKeToiDot
        {
            get => _isLuyKeToiDot;
            set => SetProperty(ref _isLuyKeToiDot, value);
        }

        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }
        private string _chiTietToi;

        public PrintReportCoverSheetViewModel(
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDtChungTuChiTietService chungTuChiTietService,
            ISktSoLieuService sktSoLieuService,
            ILog logger,
            IDanhMucService danhMucService,
            INsDonViService nSDonViService,
            IExportService exportService,
            IDmChuKyService dmChuKyService,
            INsMucLucNganSachService iNsMucLucNganSachService,
            INsDtNhanPhanBoMapService iNsDtNhanPhanBoMapService,
            DmChuKyDialogViewModel dmChuKyDialogViewModel
            )
        {
            _sessionService = sessionService;
            _chungTuChiTietService = chungTuChiTietService;
            _sktSoLieuService = sktSoLieuService;
            _exportService = exportService;
            _nSDonViService = nSDonViService;
            _logger = logger;
            _mapper = mapper;
            _dtChungTuService = dtChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsDtNhanPhanBoMapService = iNsDtNhanPhanBoMapService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
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
                base.Init();
                _sessionInfo = _sessionService.Current;
                InitReportDefaultDate();
                LoadDotPhanBo();
                LoadDonViTinh();
                LoadTieuDe();
                LoadPaperPrintTypes();
                LoadChiTietToi();
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionInfo.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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
                ExportReport();
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
                ExportReport(false, false);
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
                ExportReport(true, true);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void ExportReport(bool isPrintPDF = true, bool isShowOnBrowser = false)
        {
            if (SelectedDotIn != null)
            {
                try
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        int donViTinh = GetDonViTinh();
                        _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TOBIA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                        var lstIdChungTu = GetListChungTuReport();
                        ListDataToBia = _chungTuChiTietService.GetDataReportChiTieuToBia(string.Join(",", lstIdChungTu), donViTinh).ToList();
                        List<ReportChiTieuDuToanQuery> listHeader = new List<ReportChiTieuDuToanQuery>();
                        CalculateTotal();
                        if (ListDataToBia != null && ListDataToBia.Count > 0)
                        {
                            List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                                                                        string.Join(",", ListDataToBia.Select(n => n.XauNoiMa).ToList())).ToList();
                            listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                            if (listParent != null && listParent.Count > 0)
                            {
                                foreach (NsMucLucNganSach item in listParent)
                                {
                                    listHeader.Add(new ReportChiTieuDuToanQuery
                                    {
                                        LNS = item.Lns,
                                        L = item.L,
                                        K = item.K,
                                        M = item.M,
                                        TM = item.Tm,
                                        TTM = item.Ttm,
                                        NG = item.Ng,
                                        TNG = item.Tng,
                                        TNG1 = item.Tng1,
                                        TNG2 = item.Tng2,
                                        TNG3 = item.Tng3,
                                        MoTa = item.MoTa,
                                        XauNoiMa = item.XauNoiMa,
                                        HienVat = 0,
                                        TuChi = 0,
                                        IsHangCha = true,
                                        MlnsId = item.MlnsId,
                                        MlnsIdParent = item.MlnsIdParent ?? Guid.Empty,
                                    });
                                }
                                foreach (ReportChiTieuDuToanQuery item in ListDataToBia)
                                {
                                    int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                                    if (index >= 0)
                                    {
                                        item.MlnsIdParent = listHeader[index].MlnsId;
                                        listHeader.Insert(index + 1, item);
                                    }
                                }
                            }
                            ListDataToBia = new List<ReportChiTieuDuToanQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                        }

                        CalculateDataLNS();
                        var listChungTuModel = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(ListDataToBia).ToList();
                        List<DtChungTuChiTietModel> resultsTotal = new List<DtChungTuChiTietModel>();
                        List<GenericReportHeader> headers = new List<GenericReportHeader>();
                        _exportService.GenerateImportData(listChungTuModel.Select(x => x.SLns).ToList(),
                            listChungTuModel, resultsTotal,
                            headers, _sessionService.Current.YearOfWork, filterResultTotal: t => !t.IsHangCha);

                        switch (SelectedInToiMuc.ValueItem)
                        {
                            case nameof(MLNSFiled.NG):
                                listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                                listChungTuModel.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG):
                                listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                                listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG1):
                                listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                                listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng1)).Select(x => x.IsHangCha = false).ToList();
                                break;
                            case nameof(MLNSFiled.TNG2):
                                listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                                listChungTuModel.Where(x => !string.IsNullOrEmpty(x.STng2)).Select(x => x.IsHangCha = false).ToList();
                                break;
                        }
                        foreach (var item in listChungTuModel.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
                        {
                            var parent = listChungTuModel.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();

                            if (parent != null && item.SM != string.Empty)
                            {
                                if (!string.IsNullOrEmpty(parent.SL) && !string.IsNullOrEmpty(parent.SK))
                                {
                                    item.SL = string.Empty;
                                    item.SK = string.Empty;
                                    item.SLns = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(parent.SM))
                                    item.SM = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STm))
                                    item.STm = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STtm))
                                    item.STtm = string.Empty;
                                if (!string.IsNullOrEmpty(parent.SNg))
                                    item.SNg = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STng))
                                    item.STng = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STng1))
                                    item.STng1 = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STng2))
                                    item.STng2 = string.Empty;
                                if (!string.IsNullOrEmpty(parent.STng3))
                                    item.STng3 = string.Empty;
                            }
                        }

                        double lastRowTotal = resultsTotal.First().LstDataTotalModels.Sum(t => t.Value);
                        string nameColunmMerge;
                        string rangeHeader1;
                        int countHasData = headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count();
                        nameColunmMerge = GetExcelColumnName(16 + headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count());
                        //rangeHeader1 = "N7:" + nameColunmMerge + "7";
                        rangeHeader1 = "Q8:" + nameColunmMerge + "8";
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        FormatNumber formatNumber = new FormatNumber(donViTinh, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
                        CheckCotHienThiBangChu(listChungTuModel.ToList(), data);
                        data.Add("FormatNumber", formatNumber);
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("TieuDe1", TieuDe1.ToUpper());
                        data.Add("TieuDe2", TieuDe2);
                        data.Add("TieuDe3", TieuDe3);
                        data.Add("Items", listChungTuModel);
                        data.Add("ItemsTotal", resultsTotal);
                        data.Add("LastRowTotal", lastRowTotal);
                        data.Add("Headers", headers);
                        data.Add("rangeHeader1", rangeHeader1);
                        data.Add("TongSo", _tongChiTieu.TongSo);
                        data.Add("Header1", SelectedDonViTinh.DisplayItem);
                        data.Add("TongSoBangChu", StringUtils.NumberToText(lastRowTotal * donViTinh, true));
                        data.Add("TongTonKhoBangChu", StringUtils.NumberToText(0, true));
                        data.Add("TongTuChiBangChu", StringUtils.NumberToText(_tongChiTieu.TongTuChi * donViTinh, true));
                        data.Add("TongHienVatBangChu", StringUtils.NumberToText(_tongChiTieu.TongHienVat * donViTinh, true));
                        data.Add("TongDuPhongBangChu", StringUtils.NumberToText(_tongChiTieu.TongDuPhong * donViTinh, true));
                        data.Add("TongHangNhapBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangNhap * donViTinh, true));
                        data.Add("TongHangMuaBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangMua * donViTinh, true));
                        data.Add("TongPhanCapBangChu", StringUtils.NumberToText(_tongChiTieu.TongPhanCap * donViTinh, true));
                        data.Add("ThoiGian", string.Format("{0}, ngày {1} tháng {2} năm {3}", _diaDiem, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                        data.Add("DiaDiem", _diaDiem);
                        data.Add("Ngay", DateUtils.FormatDateReport(ReportDate));
                        data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                        data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                        data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
                        data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                        data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                        data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
                        data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                        data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                        data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
                        data.Add("Range", 10000);

                        string templateFileName = "";
                        if (countHasData == 1)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA);
                        else if (countHasData == 2)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA2);
                        else if (countHasData == 3)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA3);
                        else if (countHasData == 4)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA4);
                        else if (countHasData == 5)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA5);
                        else if (countHasData == 6)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA6);
                        else if (countHasData == 7)
                            templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_TOBIA7);
                        string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_TOBIA;
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                        List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi);
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel, GenericReportHeader>(templateFileName, data, hideColumns);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = (ExportResult)e.Result;
                            if (result != null)
                            {
                                _exportService.Open(result, isPrintPDF ? ExportType.PDF : ExportType.EXCEL);
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

        public string GetHeader2Report()
        {
            DonVi donViParent = _nSDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public string GetPath(string input)
        {
            if (PaperPrintTypeSelected.ValueItem == "2")
                input = input + "_Ngang";
            return Path.Combine(ExportPrefix.PATH_TL_DT, input + FileExtensionFormats.Xlsx);
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CalculateDataLNS()
        {
            ListDataToBia.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in ListDataToBia.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = ListDataToBia.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParent(parentItem, selfItem);
        }

        private void CalculateTotal()
        {
            _tongChiTieu = new DuToanTongChiTieu();
            foreach (ReportChiTieuDuToanQuery item in ListDataToBia.Where(x => !x.IsHangCha))
            {
                _tongChiTieu.TongTuChi += item.TuChi.HasValue ? item.TuChi.Value : 0;
                _tongChiTieu.TongHienVat += item.HienVat.HasValue ? item.HienVat.Value : 0;
                _tongChiTieu.TongDuPhong += item.DuPhong.HasValue ? item.DuPhong.Value : 0;
                _tongChiTieu.TongHangNhap += item.HangNhap.HasValue ? item.HangNhap.Value : 0;
                _tongChiTieu.TongHangMua += item.HangMua.HasValue ? item.HangMua.Value : 0;
                _tongChiTieu.TongPhanCap += item.PhanCap.HasValue ? item.PhanCap.Value : 0;
                _tongChiTieu.TongSo += item.TongSo;
            };
        }

        private void LoadDotPhanBo()
        {
            DataDotIn = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicateChungTuIndex();
            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            Models.OrderByDescending(c => c.DNgayQuyetDinh).ThenByDescending(c => c.DNgayChungTu);
            List<string> lstSoQuyetDinh = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh))
                .Select(x => x.SSoQuyetDinh.Trim()).Distinct().ToList();
            List<DateTime> lstNgayChungTu = Models
                .Where(x => string.IsNullOrEmpty(x.SSoQuyetDinh) && x.DNgayChungTu.HasValue)
                .Select(x => x.DNgayChungTu.Value.Date).Distinct().ToList();

            foreach (var qd in lstSoQuyetDinh)
            {
                var lstCt = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(qd)).ToList();
                var firstList = lstCt.FirstOrDefault();

                if (firstList == null) continue;

                string mota = firstList.DNgayQuyetDinh.HasValue ? firstList.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty;
                mota += StringUtils.SPACE;
                mota += firstList.SMoTa;

                DataDotIn.Add(new ComboboxItem
                {
                    ValueItem = qd,
                    DisplayItem = string.Format("{0}\n{1}", qd, mota),
                    HiddenValue = "1"
                });
            }

            foreach (var ngayCt in lstNgayChungTu)
            {
                var lstCt = Models.Where(x => string.IsNullOrEmpty(x.SSoQuyetDinh) && x.DNgayChungTu.Value.Date.Equals(ngayCt)).ToList();
                string mota = "";
                string soCt = string.Join(",", lstCt.Select(x => x.SSoChungTu));
                foreach (var ct in lstCt)
                {
                    if (!string.IsNullOrEmpty(mota))
                    {
                        mota += "\n";
                    }

                    mota += ct.DNgayChungTu.HasValue ? ct.DNgayChungTu.Value.ToString("dd/MM/yyyy") : "";
                    mota += " ";
                    mota += ct.SMoTa;
                }
                DataDotIn.Add(new ComboboxItem
                {
                    ValueItem = ngayCt.ToString("dd/MM/yyyy"),
                    DisplayItem = string.Format("{0}\n{1}", soCt, mota),
                    HiddenValue = "2"
                });
            }
            var ordered = DataDotIn.OrderByDescending(c => DateTime.Parse(c.DisplayItem.Split('\n')[1].Split(" ")[0])).ToList();
            DataDotIn = new ObservableCollection<ComboboxItem>(ordered);
            if (DataDotIn != null && DataDotIn.Count > 0)
            {
                SelectedDotIn = DataDotIn.FirstOrDefault();
            }
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        private void LoadTitle()
        {
            if (DataDotIn != null && DataDotIn.Count > 0 && SelectedDotIn != null)
            {
                TieuDe1 = "Dự toán chi ngân sách năm " + _sessionInfo.YearOfWork;
                if (SelectedDotIn.HiddenValue.Equals("1"))
                {
                    var itemSelected = Models.DefaultIfEmpty(new DtChungTuModel()).FirstOrDefault(n => n.SSoQuyetDinh == SelectedDotIn.ValueItem);
                    TieuDe2 = string.Format("Kèm theo Quyết định số {0}, ngày {1}", itemSelected.SSoQuyetDinh, DateUtils.Format(itemSelected.DNgayQuyetDinh));
                }
                else
                {
                    TieuDe2 = string.Format("Ngày chứng từ {0}", SelectedDotIn.ValueItem);
                }
            }
            else
            {
                TieuDe1 = string.Empty;
                TieuDe2 = string.Empty;
            }
            TieuDe3 = string.Empty;
            OnPropertyChanged(nameof(TieuDe1));
            OnPropertyChanged(nameof(TieuDe2));
        }

        private void LoadTieuDe()
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TOBIA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                TieuDe1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                TieuDe2 = _dmChuKy.TieuDe2MoTa;

            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
            {
                TieuDe3 = _dmChuKy.TieuDe3MoTa;
            }
        }

        private void LoadChiTietToi()
        {
            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                _chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                DataInToiMuc = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(_chiTietToi));
                _selectedInToiMuc = DataInToiMuc != null ? DataInToiMuc[0] : null;
            }
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            return predicate;
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_TOBIA) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_TOBIA;
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

        private List<Guid> GetListChungTuReport()
        {
            List<Guid> result = new List<Guid>();
            List<DtChungTuModel> lstChungTu = new List<DtChungTuModel>();
            DateTime ngayLuyKe = DateTime.MinValue;
            if (SelectedDotIn == null) return new List<Guid>();
            if (SelectedDotIn.HiddenValue.Equals("1"))
            {
                lstChungTu = Models.Where(n => !string.IsNullOrEmpty(n.SSoQuyetDinh) && n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayQuyetDinh.GetValueOrDefault().Date;
                }
            }
            else
            {
                var ngayChungTu = SelectedDotIn.ValueItem;
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy") == ngayChungTu).ToList();
                if (lstChungTu.Count > 0)
                {
                    ngayLuyKe = lstChungTu.FirstOrDefault().DNgayChungTu.GetValueOrDefault().Date;
                }
            }
            result.AddRange(lstChungTu.Select(x => x.Id));
            // ds chung tu luy ke
            if (IsLuyKeToiDot)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => (x.DNgayQuyetDinh == null && x.DNgayChungTu != null && x.DNgayChungTu.Value.Date <= ngayLuyKe) || (x.DNgayQuyetDinh != null && x.DNgayQuyetDinh.Value.Date <= ngayLuyKe));
                var lstCtLuyKe = _dtChungTuService.FindByCondition(predicate).ToList();
                if (lstCtLuyKe.Count > 0)
                {
                    result.AddRange(lstCtLuyKe.Select(x => x.Id));
                }
            }
            return result;
        }

        public void CheckCotHienThiBangChu(List<DtChungTuChiTietModel> lstData, Dictionary<string, object> dic)
        {
            var lstLns = lstData.Select(x => x.SLns).ToList();
            string concatLns = string.Join(",", lstLns);
            var listSettingMLNS = _iNsMucLucNganSachService.FindByListLnsDonVi(concatLns, _sessionInfo.YearOfWork).ToList();

            if (listSettingMLNS.Any(x => x.BTonKho))
            {
                dic.Add("BTonKho", true);
            }
            if (listSettingMLNS.Any(x => x.BTuChi))
            {
                dic.Add("BTuChi", true);
            }

            if (listSettingMLNS.Any(x => x.BHienVat))
            {
                dic.Add("BHienVat", true);
            }

            if (listSettingMLNS.Any(x => x.BDuPhong))
            {
                dic.Add("BDuPhong", true);
            }

            if (listSettingMLNS.Any(x => x.BHangMua))
            {
                dic.Add("BHangMua", true);
            }

            if (listSettingMLNS.Any(x => x.BHangNhap))
            {
                dic.Add("BHangNhap", true);
            }

            if (listSettingMLNS.Any(x => x.BPhanCap))
            {
                dic.Add("BPhanCap", true);
            }
        }
    }
}
