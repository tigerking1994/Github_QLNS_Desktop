using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport
{
    public class HospitalPrintReportTargetAgencyViewModel : ReportViewModelBase<DtChungTuModel, DtChungTuChiTietModel, DtChungTuChiTietModel>
    {
        private INsDtNhanPhanBoMapService _dtChungTuMapService;
        private IMapper _mapper;
        private ICollectionView _listDonViView;
        private INsDonViService _nsDonViService;
        private ISessionService _sessionService;
        private IExportService _exportService;
        private INsDtChungTuService _dtChungTuService;
        private INsDtChungTuChiTietService _chungTuChiTietService;
        private ISktSoLieuService _sktSoLieuService;
        private IDanhMucService _danhMucService;
        private IDmChuKyService _dmChuKyService;
        private INsMucLucNganSachService _iNsMucLucNganSachService;
        private INsDtNhanPhanBoMapService _iNsDtNhanPhanBoMapService;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private readonly ILog _logger;
        private DmChuKy _dmChuKy;
        private SessionInfo _sessionInfo;
        private bool _hasParentAgency;
        private List<DonVi> _listAgencyOfUser;
        private DuToanTongChiTieu _tongChiTieu;
        private string _diaDiem;
        private string _cap1;
        public override Type ContentType => typeof(View.Budget.Estimate.Division.PrintReport.PrintReportTargetAgency);
        public ObservableCollection<CheckBoxItem> ListDonVi { get; set; }
        public List<ReportChiTieuDuToanQuery> ListDataReportChiTieuDonVi { get; set; }

        public IEnumerable<DtChungTuModel> DtChungTuModelNhanPhanBos { get; set; }
        public ObservableCollection<ComboboxItem> Agencies;

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
                    LoadTieuDe();
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

        private bool _isLuyKeToiDot;
        public bool IsLuyKeToiDot
        {
            get => _isLuyKeToiDot;
            set
            {
                if (SetProperty(ref _isLuyKeToiDot, value))
                {
                    LoadTieuDe();
                    LoadDonVi();
                }
            }
        }

        private bool _isInTieuNganh;
        public bool IsInTieuNganh
        {
            get => _isInTieuNganh;
            set => SetProperty(ref _isInTieuNganh, value);
        }

        //private bool _isInTongHop;
        //public bool IsInTongHop
        //{
        //    get => _isInTongHop;
        //    set => SetProperty(ref _isInTongHop, value);
        //}

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

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Count : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
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

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        /*private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                    LoadDotPhanBo();
            }
        }*/

        private string _chiTietToi;
        public RelayCommand PrintExcelCommand { get; }
        public RelayCommand PrintPDFCommand { get; }
        public RelayCommand PrintBrowserCommand { get; }
        public RelayCommand ConfigSignCommand { get; }

        public HospitalPrintReportTargetAgencyViewModel(
           INsDtNhanPhanBoMapService dtChungTuMapService,
           IMapper mapper,
           INsDonViService nSDonViService,
           ISessionService sessionService,
           ISktSoLieuService sktSoLieuService,
           ILog logger,
           INsDtChungTuChiTietService chungTuChiTietService,
           IExportService exportService,
           INsDtChungTuService dtChungTuService,
           IDanhMucService danhMucService,
           IDmChuKyService dmChuKyService,
           INsDtNhanPhanBoMapService iNsDtNhanPhanBoMapService,
           INsMucLucNganSachService iNsMucLucNganSachService,
           DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _dtChungTuMapService = dtChungTuMapService;
            _mapper = mapper;
            _nsDonViService = nSDonViService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _exportService = exportService;
            _sktSoLieuService = sktSoLieuService;
            _dtChungTuService = dtChungTuService;
            _danhMucService = danhMucService;
            _dmChuKyService = dmChuKyService;
            _iNsDtNhanPhanBoMapService = iNsDtNhanPhanBoMapService;
            _iNsMucLucNganSachService = iNsMucLucNganSachService;
            _logger = logger;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintPDFCommand = new RelayCommand(o => OnExportReport(ExportType.PDF));
            PrintExcelCommand = new RelayCommand(o => OnExportReport(ExportType.EXCEL));
            PrintBrowserCommand = new RelayCommand(o => OnExportReport(ExportType.PDF, true));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            try
            {
                if (Model == null)
                {
                    Model = new DtChungTuModel();
                }
                _sessionInfo = _sessionService.Current;
                LoadLoaiBaoCao();
                LoadUserAgency();
                /*LoadVoucherType();*/
                LoadDotPhanBo();
                LoadCombobox();
                LoadTieuDe();
                LoadDonVi();
                LoadDanhMuc();
                LoadChiTietToi();
                var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
                _diaDiem = danhMucDiaDiem == null ? "Hà Nội" : danhMucDiaDiem.SGiaTri;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnExportReport(ExportType exportType, bool isBrowser = false)
        {
            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI || SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_CHITIET_DONVI)
            {
                try
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;
                        List<ExportResult> results = new List<ExportResult>();
                        if (SelectedDotIn != null)
                        {
                            if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_DONVI)
                            {
                                results.AddRange(PrintDonViTongHop(exportType));
                            }
                            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_CHITIET_DONVI)
                            {
                                foreach (CheckBoxItem itemDonVi in ListDonVi.Where(n => n.IsChecked))
                                {
                                    if (itemDonVi.ValueItem == "@")
                                    {
                                        results.Add(PrintDuToan(exportType));
                                    }
                                    else
                                    {
                                        results.Add(PrintDonVi(itemDonVi, exportType));
                                    }
                                }
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
            else if (SelectedLoaiBaoCao.ValueItem == LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO)
            {
                int type = 0;
                if (exportType == ExportType.EXCEL)
                {
                    type = 2;
                }
                if (exportType == ExportType.PDF && !isBrowser)
                {
                    type = 1;
                }
                if (exportType == ExportType.PDF && isBrowser)
                {
                    type = 3;
                }
                ExportReportSummary(type, exportType);
            }
        }

        public void ExportReportSummary(int type, ExportType exportType = ExportType.PDF)
        {
            if (SelectedDotIn != null)
            {
                PrintDonVi(type, exportType);
            }
        }

        public double TinhDuToan()
        {
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            List<DtChungTuModel> lstChungTu = Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
            var chungTu = lstChungTu.FirstOrDefault();
            ListDataReportChiTieuDonVi = _chungTuChiTietService.GetDataReportChiTieuDonViDuToan(_sessionInfo.YearOfWork,
                                                                             _sessionInfo.Budget,
                                                                             _sessionInfo.YearOfBudget,
                                                                             chungTu.DNgayChungTu,
                                                                             chungTu.Id.ToString(),
                                                                             donViTinh,
                                                                             IsInTieuNganh).ToList();

            var modelTemp = Model;
            Model = chungTu;
            var listDuToan = CalculateDataPrintDuToan();
            var dictConLaiPhanBoGroupByXauNoiMa = listDuToan.Where(x => x.IsConLai).GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.ToList());
            ListDataReportChiTieuDonVi = ListDataReportChiTieuDonVi.Select(item =>
            {
                var listDataByMlns = dictConLaiPhanBoGroupByXauNoiMa.GetValueOrDefault(item.XauNoiMa, new List<DtChungTuChiTietModel>());
                item.TuChi = listDataByMlns.Sum(x => Math.Round(x.FTuChi / donViTinh, 2));
                item.HienVat = listDataByMlns.Sum(x => Math.Round(x.FHienVat / donViTinh, 2));
                return item;
            }).Where(item => item.TuChi != 0 || item.HienVat != 0).ToList();
            Model = modelTemp;
            return ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha)
                .Sum(x => x.TuChi.GetValueOrDefault(0) + x.HienVat.GetValueOrDefault(0));
        }

        public void PrintDonVi(int type, ExportType exportType = ExportType.PDF)
        {
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            List<Guid> lstIdChungTu = GetListChungTuReport();
            var lstDuToanChiTiet = _chungTuChiTietService.GetDataReportChiTieuDonVi(_sessionInfo.YearOfWork,
                                                                             _sessionInfo.Budget,
                                                                             _sessionInfo.YearOfBudget,
                                                                             null,
                                                                             string.Join(",", lstIdChungTu),
                                                                             DateTime.Now,
                                                                             donViTinh,
                                                                             IsLuyKeToiDot).ToList();

            List<ReportDuToanDonViTongHopModel> result = new List<ReportDuToanDonViTongHopModel>();
            int stt = 1;
            foreach (CheckBoxItem itemDonVi in ListDonVi.Where(n => n.IsChecked))
            {
                if (itemDonVi.ValueItem == "@")
                {
                    ReportDuToanDonViTongHopModel rs = new ReportDuToanDonViTongHopModel();
                    rs.Stt = stt++;
                    rs.DonVi = itemDonVi.DisplayItem;
                    rs.SoDuToan = TinhDuToan();
                    result.Add(rs);
                }
                else
                {
                    ReportDuToanDonViTongHopModel rs = new ReportDuToanDonViTongHopModel();
                    rs.Stt = stt++;
                    rs.DonVi = itemDonVi.DisplayItem;
                    rs.SoDuToan = lstDuToanChiTiet.Where(x => x.MaDonVi.Equals(itemDonVi.ValueItem))
                        .Sum(x => x.TuChi.GetValueOrDefault() + x.HienVat.GetValueOrDefault() + x.HangNhap.GetValueOrDefault() + x.HangMua.GetValueOrDefault() + x.PhanCap.GetValueOrDefault() + x.DuPhong.GetValueOrDefault());
                    result.Add(rs);

                }
            }
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("TieuDe1", TieuDe1);
            data.Add("TieuDe2", TieuDe2);
            data.Add("Items", result);
            data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
            data.Add("TongTien", StringUtils.NumberToText(result.Sum(x => x.SoDuToan) * donViTinh));
            data.Add("DiaDiem", _diaDiem);
            data.Add("Ngay", StringUtils.CreateDateTimeString());
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            string template = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DONVI_TONGHOP);
            string templateWithOutExtension;
            if (SelectedKieuGiay.ValueItem == LoaiGiay.MACDINH)
            {
                template += "_doc";
            }
            else
            {
                template += "_ngang";
            }
            template += ".xlsx";
            templateWithOutExtension = Path.GetFileNameWithoutExtension(template) + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            var xlsFile = _exportService.Export<ReportDuToanDonViTongHopModel>(template, data);
            List<PdfFileModel> lstFilePath = new List<PdfFileModel>();
            string filePath = "";
            if (type == 1)
            {
                filePath = _exportService.ExportExcel(xlsFile, templateWithOutExtension + FileExtensionFormats.Xlsx);
            }
            else if (type == 2 || type == 3)
            {
                filePath = _exportService.ExportPdf(xlsFile, templateWithOutExtension + FileExtensionFormats.Pdf);
                lstFilePath.Add(new PdfFileModel("Thông báo dự toán đơn vị tổng hợp", filePath, xlsFile));
            }

            if (type == 3 && lstFilePath.Count > 0)
            {
                // Show pdf
                _exportService.ShowPdf(lstFilePath);
            }
            else
            {
                // Show folder
                IOExtensions.OpenFolder(Path.GetDirectoryName(filePath));
            }
        }

        public List<ExportResult> PrintDonViTongHop(ExportType exportType)
        {
            IsLoading = true;
            List<ExportResult> exportResults = new List<ExportResult>();
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            var lstDonViSelect = ListDonVi.Where(n => n.IsChecked).ToList();
            var lstDonViSplits = SplitList(lstDonViSelect, 4).ToList();
            var lstIdDonViString = string.Join(",", lstDonViSelect.Select(x => x.ValueItem));
            List<Guid> lstIdChungTu = GetListChungTuReport();
            ListDataReportChiTieuDonVi = _chungTuChiTietService.GetDataReportChiTieuDonVi(_sessionInfo.YearOfWork,
                _sessionInfo.Budget, _sessionInfo.YearOfBudget,
                lstIdDonViString, string.Join(",", lstIdChungTu), DateTime.Now, donViTinh, IsLuyKeToiDot).ToList();

            List<ReportChiTieuDuToanQuery> data = new List<ReportChiTieuDuToanQuery>();
            if (ListDataReportChiTieuDonVi != null && ListDataReportChiTieuDonVi.Count > 0)
            {
                List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                    string.Join(",", ListDataReportChiTieuDonVi.Select(n => n.XauNoiMa).ToList())).ToList();
                listParent = listParent.Where(x => x.BHangChaDuToan.HasValue && x.BHangChaDuToan.Value).ToList();
                if (listParent != null && listParent.Count > 0)
                {
                    foreach (var dv in lstDonViSelect)
                    {
                        foreach (NsMucLucNganSach item in listParent)
                        {
                            data.Add(new ReportChiTieuDuToanQuery
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
                                DuPhong = 0,
                                HangNhap = 0,
                                HangMua = 0,
                                PhanCap = 0,
                                MaDonVi = dv.ValueItem,
                                IsHangCha = true,
                                MlnsId = item.MlnsId,
                                MlnsIdParent = item.MlnsIdParent
                            });
                        }
                    }
                    data.AddRange(ListDataReportChiTieuDonVi);
                }
            }

            CalculateDataLNSDonVi(data);
            var lstXauNoiMa = data.Select(x => x.XauNoiMa).Distinct().ToList();
            var lstMlns = _iNsMucLucNganSachService.FindByCondition(x => x.NamLamViec == _sessionInfo.YearOfWork && lstXauNoiMa.Contains(x.XauNoiMa)).ToList();
            for (int i = 0; i < lstDonViSplits.Count(); i++)
            {
                if (lstDonViSplits[i].Count < 4)
                {
                    var colRemain = 4 - lstDonViSplits[i].Count;
                    for (int j = 0; j < colRemain; j++)
                    {
                        CheckBoxItem dvEmpty = new CheckBoxItem();
                        lstDonViSplits[i].Add(dvEmpty);
                    }
                }
                List<HeaderReportDuToanDonViTongHop> headers = new List<HeaderReportDuToanDonViTongHop>();
                List<ReportDuToanPhanBoTungDonViTongHopModel> result =
                    new List<ReportDuToanPhanBoTungDonViTongHopModel>();
                bool IsFirst = true;
                foreach (var donVi in lstDonViSplits[i])
                {
                    HeaderReportDuToanDonViTongHop hd = new HeaderReportDuToanDonViTongHop();
                    hd.Header1 = "Trong đó";
                    hd.Header2 = donVi.DisplayItem;
                    hd.MergeRangeHeader1 = "L8" + ":" + GetExcelColumnName(11 + lstDonViSplits[i].Count) + "8";
                    hd.IsFirst = IsFirst;
                    IsFirst = false;
                    headers.Add(hd);
                }

                foreach (var ml in lstMlns)
                {
                    ReportDuToanPhanBoTungDonViTongHopModel it = new ReportDuToanPhanBoTungDonViTongHopModel();
                    it.IIdMlns = ml.MlnsId;
                    it.IIdMlnsParent = ml.MlnsIdParent;
                    it.LNS = ml.Lns;
                    it.L = ml.L;
                    it.K = ml.K;
                    it.M = ml.M;
                    it.TM = ml.Tm;
                    it.TTM = ml.Ttm;
                    it.NG = ml.Ng;
                    it.TNG = ml.Tng;
                    it.TNG1 = ml.Tng1;
                    it.TNG2 = ml.Tng2;
                    it.TNG3 = ml.Tng3;
                    it.XauNoiMa = ml.XauNoiMa;
                    it.IsHangCha = ml.BHangChaDuToan.HasValue ? ml.BHangChaDuToan.Value : false;
                    it.MoTa = ml.MoTa;
                    it.TongCong = data.Where(x => x.MlnsId.Equals(ml.MlnsId))
                        .Sum(x => x.TuChi.GetValueOrDefault() + x.HienVat.GetValueOrDefault() + x.HangNhap.GetValueOrDefault() + x.HangMua.GetValueOrDefault() + x.PhanCap.GetValueOrDefault() + x.DuPhong.GetValueOrDefault());
                    it.LstData = new List<DuLieu>();
                    foreach (var dvIt in lstDonViSplits[i])
                    {
                        var giaTri = data.Where(x => x.MlnsId.Equals(ml.MlnsId) && x.MaDonVi.Equals(dvIt.ValueItem))
                            .Sum(x => x.TuChi.GetValueOrDefault() + x.HienVat.GetValueOrDefault() + x.HangNhap.GetValueOrDefault() + x.HangMua.GetValueOrDefault() + x.PhanCap.GetValueOrDefault() + x.DuPhong.GetValueOrDefault());
                        DuLieu dl = new DuLieu();
                        dl.GiaTri = giaTri;
                        it.LstData.Add(dl);
                    }
                    result.Add(it);
                }

                result = result.OrderBy(x => x.XauNoiMa).ToList();
                var tongCong = result.Where(x => !x.IsHangCha)
                    .Sum(x => x.TongCong);

                switch (SelectedInToiMuc.ValueItem)
                {
                    case nameof(MLNSFiled.NG):
                        result = result.Where(x => string.IsNullOrEmpty(x.TNG)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG):
                        result = result.Where(x => string.IsNullOrEmpty(x.TNG1)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG1):
                        result = result.Where(x => string.IsNullOrEmpty(x.TNG2)).ToList();
                        break;
                    case nameof(MLNSFiled.TNG2):
                        result = result.Where(x => string.IsNullOrEmpty(x.TNG3)).ToList();
                        break;
                }
                foreach (var item in result.Where(x => !string.IsNullOrEmpty(x.L)).OrderByDescending(x => x.XauNoiMa))
                {
                    var parent = result.Where(x => x.IIdMlns == item.IIdMlnsParent).LastOrDefault();
                    if (parent != null)
                    {
                        item.L = string.Empty;
                        item.K = string.Empty;
                        item.LNS = string.Empty;
                        if (!string.IsNullOrEmpty(parent.M))
                            item.M = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TM))
                            item.TM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TTM))
                            item.TTM = string.Empty;
                        if (!string.IsNullOrEmpty(parent.NG))
                            item.NG = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TNG))
                            item.TNG = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TNG1))
                            item.TNG1 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TNG2))
                            item.TNG2 = string.Empty;
                        if (!string.IsNullOrEmpty(parent.TNG3))
                            item.TNG3 = string.Empty;
                    }
                }

                List<ReportDuToanPhanBoTungDonViTongHopModel> resutlTotal = new List<ReportDuToanPhanBoTungDonViTongHopModel>();
                ReportDuToanPhanBoTungDonViTongHopModel itTotal = new ReportDuToanPhanBoTungDonViTongHopModel();
                itTotal.LstDataTotal = new List<DuLieu>();
                foreach (var dvIt in lstDonViSplits[i])
                {
                    var giaTri = data.Where(x => !x.IsHangCha && x.MaDonVi.Equals(dvIt.ValueItem))
                        .Sum(x => x.TuChi.GetValueOrDefault() + x.HienVat.GetValueOrDefault() + x.HangNhap.GetValueOrDefault() + x.HangMua.GetValueOrDefault() + x.PhanCap.GetValueOrDefault() + x.DuPhong.GetValueOrDefault());
                    DuLieu dl = new DuLieu();
                    dl.GiaTri = giaTri;
                    itTotal.LstDataTotal.Add(dl);
                }
                resutlTotal.Add(itTotal);

                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
                dic.Add("FormatNumber", formatNumber);
                dic.Add("TieuDe1", TieuDe1);
                dic.Add("TieuDe2", TieuDe2);
                dic.Add("Items", result);
                dic.Add("ItemsTotal", resutlTotal);
                dic.Add("Headers", headers);
                dic.Add("TongCong", tongCong);
                dic.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                dic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                dic.Add("Cap2", GetHeader2Report());
                dic.Add("CatUnitType",
                    "Đơn vị tính: " + (SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : " "));
                dic.Add("DiaDiem", _diaDiem);
                dic.Add("Ngay", DateUtils.GetCurrentDateReport());
                dic.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
                dic.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
                dic.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

                dic.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
                dic.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
                dic.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

                dic.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
                dic.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
                dic.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;
                if (i == 0)
                {
                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO1);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO1);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                }
                else
                {
                    templateFileName = GetPath(ExportFileName.RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO2);
                    fileNamePrefix = Path.GetFileNameWithoutExtension(ExportFileName.RPT_NS_DUTOAN_PHANBO_TONGHOP_DONVI_TO2);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix) + "_" + (i + 1);
                }
                var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
                List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi);
                var xlsFile = _exportService.Export<ReportDuToanPhanBoTungDonViTongHopModel, HeaderReportDuToanDonViTongHop>(
                        templateFileName, dic, hideColumns);
                exportResults.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
            }
            return exportResults;
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
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

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public ExportResult PrintDuToan(ExportType exportType)
        {
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            List<DtChungTuModel> lstChungTu =
                Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList();
            var chungTu = lstChungTu.FirstOrDefault();
            ListDataReportChiTieuDonVi = _chungTuChiTietService.GetDataReportChiTieuDonViDuToan(_sessionInfo.YearOfWork,
                _sessionInfo.Budget, _sessionInfo.YearOfBudget,
                chungTu.DNgayChungTu, chungTu.Id.ToString(), donViTinh, IsInTieuNganh).ToList();

            var modelTemp = Model;
            Model = chungTu;
            var listDuToan = CalculateDataPrintDuToan();
            var dictConLaiPhanBoGroupByXauNoiMa = listDuToan.Where(x => x.IsConLai).GroupBy(x => x.SXauNoiMa)
                .ToDictionary(x => x.Key, x => x.ToList());
            ListDataReportChiTieuDonVi = ListDataReportChiTieuDonVi.Select(item =>
            {
                var listDataByMlns =
                    dictConLaiPhanBoGroupByXauNoiMa.GetValueOrDefault(item.XauNoiMa, new List<DtChungTuChiTietModel>());
                item.TuChi = listDataByMlns.Sum(x => Math.Round(x.FTuChi / donViTinh, 2));
                item.HienVat = listDataByMlns.Sum(x => Math.Round(x.FHienVat / donViTinh, 2));
                return item;
            }).Where(item => item.TuChi != 0 || item.HienVat != 0).ToList();
            Model = modelTemp;

            List<ReportChiTieuDuToanQuery> listHeader = new List<ReportChiTieuDuToanQuery>();
            CalculateTotal();
            if (ListDataReportChiTieuDonVi != null && ListDataReportChiTieuDonVi.Count > 0)
            {
                List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                    string.Join(",", ListDataReportChiTieuDonVi.Select(n => n.XauNoiMa).ToList())).ToList();
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
                            MoTa = item.MoTa,
                            XauNoiMa = item.XauNoiMa,
                            HienVat = 0,
                            TuChi = 0,
                            IsHangCha = true,
                            MlnsId = item.MlnsId,
                            MlnsIdParent = item.MlnsIdParent
                        });
                    }

                    foreach (ReportChiTieuDuToanQuery item in ListDataReportChiTieuDonVi)
                    {
                        int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId == item.MlnsIdParent).FirstOrDefault());
                        if (index >= 0)
                        {
                            item.MlnsIdParent = listHeader[index].MlnsId;
                            listHeader.Insert(index + 1, item);
                        }
                    }
                }

                ListDataReportChiTieuDonVi = new List<ReportChiTieuDuToanQuery>(listHeader.OrderBy(x => x.XauNoiMa));
                ListDataReportChiTieuDonVi.Where(n => !n.IsHangCha).Select(n =>
                {
                    n.LNS = string.Empty;
                    n.M = string.Empty;
                    n.TM = string.Empty;
                    return n;
                }).ToList();
                ListDataReportChiTieuDonVi.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.M)).Select(n =>
                {
                    n.LNS = string.Empty;
                    return n;
                }).ToList();
                ListDataReportChiTieuDonVi.Where(n => n.IsHangCha && !string.IsNullOrEmpty(n.TM)).Select(n =>
                {
                    n.M = string.Empty;
                    return n;
                }).ToList();
            }

            CalculateDataLNS();
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("TieuDe1", TieuDe1);
            data.Add("TieuDe2", TieuDe2);
            data.Add("Items", ListDataReportChiTieuDonVi.OrderBy(x => x.XauNoiMa));
            data.Add("TongSo", _tongChiTieu.TongSo);
            data.Add("TongTuChi", _tongChiTieu.TongTuChi);
            data.Add("TongHienVat", _tongChiTieu.TongHienVat);
            data.Add("TongDuPhong", _tongChiTieu.TongDuPhong);
            data.Add("TongHangNhap", _tongChiTieu.TongHangNhap);
            data.Add("TongHangMua", _tongChiTieu.TongHangMua);
            data.Add("TongPhanCap", _tongChiTieu.TongPhanCap);
            data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
            data.Add("TenDonVi", "Dự phòng");
            data.Add("TongTuChiBangChu", StringUtils.NumberToText(_tongChiTieu.TongTuChi * donViTinh, true));
            data.Add("TongHienVatBangChu", StringUtils.NumberToText(_tongChiTieu.TongHienVat * donViTinh, true));
            data.Add("TongDuPhongBangChu", StringUtils.NumberToText(_tongChiTieu.TongDuPhong * donViTinh, true));
            data.Add("TongHangNhapBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangNhap * donViTinh, true));
            data.Add("TongHangMuaBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangMua * donViTinh, true));
            data.Add("TongPhanCapBangChu", StringUtils.NumberToText(_tongChiTieu.TongPhanCap * donViTinh, true));

            string templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI));
            string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI + "_DuPhong";
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ReportChiTieuDuToanQuery>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        public ExportResult PrintDonVi(CheckBoxItem itemDonVi, ExportType exportType)
        {
            int donViTinh = SelectedDonViTinh != null ? int.Parse(SelectedDonViTinh.ValueItem) : 1;
            List<Guid> lstIdChungTu = GetListChungTuReport();
            ListDataReportChiTieuDonVi = _chungTuChiTietService.GetDataReportChiTieuDonVi(_sessionInfo.YearOfWork,
                _sessionInfo.Budget, _sessionInfo.YearOfBudget,
                itemDonVi.ValueItem, string.Join(",", lstIdChungTu), DateTime.Now, donViTinh, IsLuyKeToiDot).ToList();
            List<ReportChiTieuDuToanQuery> listHeader = new List<ReportChiTieuDuToanQuery>();
            CalculateTotal();
            if (ListDataReportChiTieuDonVi != null && ListDataReportChiTieuDonVi.Count > 0)
            {
                List<NsMucLucNganSach> listParent = _sktSoLieuService.GetParentReportTongHop(_sessionInfo.YearOfWork,
                    string.Join(",", ListDataReportChiTieuDonVi.Select(n => n.XauNoiMa).ToList())).ToList();
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
                            DuPhong = 0,
                            HangNhap = 0,
                            HangMua = 0,
                            PhanCap = 0,
                            IsHangCha = true,
                            MlnsId = item.MlnsId,
                            MlnsIdParent = item.MlnsIdParent
                        });
                    }
                    foreach (ReportChiTieuDuToanQuery item in ListDataReportChiTieuDonVi)
                    {
                        int index = listHeader.IndexOf(listHeader.Where(n => n.MlnsId.Equals(item.MlnsIdParent)).FirstOrDefault());
                        if (index >= 0)
                        {
                            item.MlnsIdParent = listHeader[index].MlnsId;
                            listHeader.Insert(index + 1, item);
                        }
                    }
                }
                ListDataReportChiTieuDonVi = new List<ReportChiTieuDuToanQuery>(listHeader.OrderBy(x => x.XauNoiMa));
            }

            CalculateDataLNS();
            var listChungTuModel = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(ListDataReportChiTieuDonVi.OrderBy(x => x.XauNoiMa)).ToList();
            List<DtChungTuChiTietModel> resultsTotal = new List<DtChungTuChiTietModel>();
            List<GenericReportHeader> headers = new List<GenericReportHeader>();
            _exportService.GenerateImportData(listChungTuModel.Select(x => x.SLns).ToList(),
                listChungTuModel, resultsTotal,
                headers, _sessionService.Current.YearOfWork, filterResultTotal: t => !t.IsHangCha);

            switch (SelectedInToiMuc.ValueItem)
            {
                case nameof(MLNSFiled.NG):
                    listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng)).ToList();
                    break;
                case nameof(MLNSFiled.TNG):
                    listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng1)).ToList();
                    break;
                case nameof(MLNSFiled.TNG1):
                    listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng2)).ToList();
                    break;
                case nameof(MLNSFiled.TNG2):
                    listChungTuModel = listChungTuModel.Where(x => string.IsNullOrEmpty(x.STng3)).ToList();
                    break;
            }
            foreach (var item in listChungTuModel.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
            {
                var parent = listChungTuModel.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                if (parent != null)
                {
                    item.SL = string.Empty;
                    item.SK = string.Empty;
                    item.SLns = string.Empty;
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
            nameColunmMerge = GetExcelColumnName(13 + headers.Where(x => !string.IsNullOrEmpty(x.Loai)).Count());
            rangeHeader1 = "N7:" + nameColunmMerge + "7";

            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(donViTinh, exportType);
            CheckCotHienThiBangChu(listChungTuModel.ToList(), data);
            data.Add("FormatNumber", formatNumber);
            data.Add("TieuDe1", TieuDe1);
            data.Add("TieuDe2", TieuDe2);
            data.Add("Items", listChungTuModel);
            data.Add("ItemsTotal", resultsTotal);
            data.Add("LastRowTotal", lastRowTotal);
            data.Add("Headers", headers);
            data.Add("rangeHeader1", rangeHeader1);
            data.Add("TongSo", _tongChiTieu.TongSo);
            data.Add("TongTonKho", 0);
            data.Add("TongTuChi", _tongChiTieu.TongTuChi);
            data.Add("TongHienVat", _tongChiTieu.TongHienVat);
            data.Add("TongDuPhong", _tongChiTieu.TongDuPhong);
            data.Add("TongHangNhap", _tongChiTieu.TongHangNhap);
            data.Add("TongHangMua", _tongChiTieu.TongHangMua);
            data.Add("TongPhanCap", _tongChiTieu.TongPhanCap);
            data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
            data.Add("TenDonVi", itemDonVi.DisplayItem);
            data.Add("TongSoBangChu", StringUtils.NumberToText(lastRowTotal * donViTinh, true));
            data.Add("TongTonKhoBangChu", StringUtils.NumberToText(0, true));
            data.Add("TongTuChiBangChu", StringUtils.NumberToText(_tongChiTieu.TongTuChi * donViTinh, true));
            data.Add("TongHienVatBangChu", StringUtils.NumberToText(_tongChiTieu.TongHienVat * donViTinh, true));
            data.Add("TongDuPhongBangChu", StringUtils.NumberToText(_tongChiTieu.TongDuPhong * donViTinh, true));
            data.Add("TongHangNhapBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangNhap * donViTinh, true));
            data.Add("TongHangMuaBangChu", StringUtils.NumberToText(_tongChiTieu.TongHangMua * donViTinh, true));
            data.Add("TongPhanCapBangChu", StringUtils.NumberToText(_tongChiTieu.TongPhanCap * donViTinh, true));

            // them dia diem, chu ky
            data.Add("DiaDiem", _diaDiem);
            data.Add("Ngay", DateUtils.GetCurrentDateReport());
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);

            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);

            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);

            string templateFileName = "";
            if (countHasData == 1)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI));
            else if (countHasData == 2)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI2));
            else if (countHasData == 3)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI3));
            else if (countHasData == 4)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI4));
            else if (countHasData == 5)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI5));
            else if (countHasData == 6)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI6));
            else if (countHasData == 7)
                templateFileName = GetPath(GetTemplateName(ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI7));
            string fileNamePrefix = ExportFileName.RPT_NS_DUTOAN_CHITIET_DONVI + "_" + itemDonVi.ValueItem;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var chiTietToi = _selectedInToiMuc != null ? _selectedInToiMuc.ValueItem : "NG";
            List<int> hideColumns = ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi);
            var xlsFile = _exportService.Export<DtChungTuChiTietModel, GenericReportHeader>(templateFileName, data, hideColumns);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }

        public override void HandleAfterExport()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public string GetTemplateName(string input)
        {
            string loaiChungTuStr = "_NSSD";
            input += loaiChungTuStr;
            if (SelectedKieuGiay.ValueItem == LoaiGiay.NGANG)
            {
                input += "_landscape";
            }
            input += ".xlsx";
            return input;
        }

        private void CalculateDataLNS()
        {
            ListDataReportChiTieuDonVi.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0)))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = ListDataReportChiTieuDonVi.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
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
            foreach (ReportChiTieuDuToanQuery item in ListDataReportChiTieuDonVi.Where(x => !x.IsHangCha))
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

        private void CalculateDataLNSDonVi(List<ReportChiTieuDuToanQuery> data)
        {
            data.Where(x => x.IsHangCha).Select(x => { x.TuChi = 0; x.HienVat = 0; x.DuPhong = 0; x.HangNhap = 0; x.HangMua = 0; x.PhanCap = 0; return x; }).ToList();
            foreach (var item in data.Where(x => !x.IsHangCha && (x.TuChi != 0 || x.HienVat != 0 || x.DuPhong != 0 || x.HangNhap != 0 || x.HangMua != 0 || x.PhanCap != 0)))
            {
                CalculateParentDonVi(data, item, item);
            }
        }

        private void CalculateParentDonVi(List<ReportChiTieuDuToanQuery> data, ReportChiTieuDuToanQuery currentItem, ReportChiTieuDuToanQuery selfItem)
        {
            var parentItem = data.Where(x => x.MlnsId == currentItem.MlnsIdParent && x.MaDonVi.Equals(currentItem.MaDonVi)).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.HienVat += selfItem.HienVat;
            parentItem.DuPhong += selfItem.DuPhong;
            parentItem.HangNhap += selfItem.HangNhap;
            parentItem.HangMua += selfItem.HangMua;
            parentItem.PhanCap += selfItem.PhanCap;
            CalculateParentDonVi(data, parentItem, selfItem);
        }

        private void LoadDotPhanBo()
        {
            DataDotIn = new ObservableCollection<ComboboxItem>();
            var predicate = CreatePredicateChungTuIndex();
            var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            Models = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
            List<string> lstSoQuyetDinh = new List<string>();
            List<DateTime> lstNgayChungTu = new List<DateTime>();
            foreach (DtChungTuModel item in Models)
            {
                if (!string.IsNullOrEmpty(item.SSoQuyetDinh))
                {
                    if (!lstSoQuyetDinh.Contains(item.SSoQuyetDinh))
                    {
                        lstSoQuyetDinh.Add(item.SSoQuyetDinh);
                        var lstDotBySoQuyetDinh = Models.Where(x => !string.IsNullOrEmpty(x.SSoQuyetDinh) && x.SSoQuyetDinh.Equals(item.SSoQuyetDinh));
                        string mota = "";
                        foreach (var dt in lstDotBySoQuyetDinh)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.DNgayQuyetDinh.HasValue
                                ? dt.DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.SMoTa;
                        }
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = item.SSoQuyetDinh,
                            DisplayItem = string.Format("{0}\n{1}", item.SSoQuyetDinh, mota),
                            HiddenValue = "1"
                        });
                    }
                }
                else
                {
                    var ngayChungTu = item.DNgayChungTu.GetValueOrDefault().Date;
                    if (!lstNgayChungTu.Contains(ngayChungTu))
                    {
                        lstNgayChungTu.Add(item.DNgayChungTu.GetValueOrDefault().Date);
                        var lstDotByNgayChungTu = Models.Where(x => x.DNgayChungTu.GetValueOrDefault().Date.Equals(ngayChungTu) && string.IsNullOrEmpty(x.SSoQuyetDinh));
                        string mota = "";
                        foreach (var dt in lstDotByNgayChungTu)
                        {
                            if (!string.IsNullOrEmpty(mota))
                            {
                                mota += "\n";
                            }
                            mota += (dt.DNgayChungTu.HasValue
                                ? dt.DNgayChungTu.Value.ToString("dd/MM/yyyy")
                                : string.Empty) + " " + dt.SMoTa;
                        }
                        var lstSoChungTuMota = string.Join(",", lstDotByNgayChungTu.Select(x => x.SSoChungTu));
                        DataDotIn.Add(new ComboboxItem
                        {
                            ValueItem = ngayChungTu.ToString("dd/MM/yyyy"),
                            DisplayItem = string.Format("{0}\n{1}", lstSoChungTuMota, mota),
                            HiddenValue = "2"
                        });
                    }
                }
            }
            if (DataDotIn != null && DataDotIn.Count > 0)
            {
                SelectedDotIn = DataDotIn.Where(x => x.ValueItem.Equals(Model.SSoQuyetDinh) || x.ValueItem.Equals(Model.SSoChungTu)).Select(x => x).DefaultIfEmpty(DataDotIn.ElementAt(0)).FirstOrDefault();
            }
        }

        private void LoadCombobox()
        {
            DataKieuGiay = new ObservableCollection<ComboboxItem>();
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.MACDINH, DisplayItem = LoaiGiay.MACDINH });
            DataKieuGiay.Add(new ComboboxItem { ValueItem = LoaiGiay.NGANG, DisplayItem = LoaiGiay.NGANG });
            SelectedKieuGiay = DataKieuGiay.FirstOrDefault();

            List<ComboboxItem> units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            DataDonViTinh = new ObservableCollection<ComboboxItem>(units);
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
        }

        private void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            List<Guid> lstIdChungTu = GetListChungTuReport();
            if (lstIdChungTu == null || lstIdChungTu.Count <= 0)
                return;
            List<DonVi> listDonVi = _nsDonViService.FindHospitalTargetAgencyReportDonVi(_sessionInfo.YearOfWork, string.Join(",", lstIdChungTu), 2).ToList();
            if (!_hasParentAgency)
            {
                listDonVi = listDonVi.Where(x => _listAgencyOfUser.Select(y => y.IIDMaDonVi).Contains(x.IIDMaDonVi))
                    .ToList();
            }

            foreach (DonVi item in listDonVi)
            {
                if ((ListDonVi.Count() == 0 || ListDonVi.Where(n => n.ValueItem == item.IIDMaDonVi).Count() == 0) &&
                    !string.IsNullOrEmpty(item.IIDMaDonVi)
                    && !string.IsNullOrEmpty(item.TenDonVi))
                    ListDonVi.Add(new CheckBoxItem { ValueItem = item.IIDMaDonVi, DisplayItem = item.TenDonVi });
            }

            //ListDonVi.Add(new CheckBoxItem { ValueItem = "@", DisplayItem = string.Format("{0} - {1}", "@", "Dự phòng") });
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

        private bool ListDonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonVi))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchDonVi.Trim()!.ToLower());
        }

        private void LoadTieuDe()
        {
            try
            {
                _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
                if (_dmChuKy == null)
                    _dmChuKy = new DmChuKy();
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                    TieuDe1 = _dmChuKy.TieuDe1MoTa;
                if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                    TieuDe2 = _dmChuKy.TieuDe2MoTa;
                if (IsLuyKeToiDot)
                {
                    List<DtChungTuModel> lstChungTu = SelectedDotIn != null ? Models.Where(n => n.SSoQuyetDinh.Equals(SelectedDotIn.ValueItem)).ToList() : new List<DtChungTuModel>();
                    if (lstChungTu.Count > 0)
                    {
                        var ngayChungTu = lstChungTu.FirstOrDefault().DNgayQuyetDinh.HasValue
                            ? lstChungTu.FirstOrDefault().DNgayChungTu.Value.ToString("dd/MM/yyyy")
                            : string.Empty;
                        TieuDe2 = string.Format("(Tới đợt ngày: {0})", ngayChungTu);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDanhMuc()
        {
            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
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

        private void LoadLoaiBaoCao()
        {
            DataLoaiBaoCao = new ObservableCollection<ComboboxItem>();
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_CHITIET_DONVI, DisplayItem = LoaiBaoCao.DUTOAN_CHITIET_DONVI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_DONVI });
            DataLoaiBaoCao.Add(new ComboboxItem { ValueItem = LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO, DisplayItem = LoaiBaoCao.DUTOAN_TONGHOP_SOPHANBO });
            SelectedLoaiBaoCao = DataLoaiBaoCao.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedLoaiBaoCao));
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.HospitalEstimate);
            // predicate = predicate.And(x => !string.IsNullOrEmpty(x.IIdDotNhan));
            return predicate;
        }

        public LinkedList<DtChungTuChiTietModel> CalculateDataPrintDuToan()
        {
            LoadNhanPhanBo();
            LoadAgencies();
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                IdDotNhan = Model.IIdDotNhan,
                SoChungTu = Model.SSoChungTu,
                UserName = _sessionInfo.Principal
            };

            List<NsDtChungTuChiTietQuery> listChungTuChiTietOrigin = _chungTuChiTietService.FindByCond(searchCondition).ToList();
            var listChungTuChiTietModelOrigin = _mapper.Map<IList<DtChungTuChiTietModel>>(listChungTuChiTietOrigin);

            var listChungTuChiTietAdjusted = _chungTuChiTietService.FindByCond(searchCondition, procedure: "sp_dt_phan_bo_du_toan_chi_tiet_dieu_chinh");
            var totalBudgetAdjustedMapByCategoryId = listChungTuChiTietAdjusted.GroupBy(x => x.IIdMlns.ToString()).ToDictionary(x => x.Key, x => x.ToList());

            var result = CalculateData(listChungTuChiTietModelOrigin, totalBudgetAdjustedMapByCategoryId);
            return result;
        }

        private LinkedList<DtChungTuChiTietModel> CalculateData(IList<DtChungTuChiTietModel> listDataOrigin, Dictionary<string, List<NsDtChungTuChiTietQuery>> totalBudgetAdjustedMapByCategoryId)
        {
            // get total budget
            var listDetailAvailable = GetTotalBudgetAvailable();
            var totalBudgetEstimateDivisionMapByCategoryId = listDetailAvailable
                .GroupBy(budget => budget.IIdDtchungTu.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdMlns.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var listDetailUsed = GetTotalBudgetUsed("sp_dt_phan_bo_du_toan_chi_tiet_used");
            var usedBudgetEstimateDivisionMapByCategoryId = listDetailUsed
                .GroupBy(budget => budget.IIdCtduToanNhan.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdMlns.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var distinctArr = listDataOrigin.Where(x => x.IsEditable).GroupBy(p => p.IIdMlns).Select(g => g.First()).ToArray();
            var dictDataOrigin = listDataOrigin.Where(x => !Guid.Empty.Equals(x.Id) && x.IIdDtchungTu.HasValue).ToDictionary(CreateKeyMapRow);
            var lengthDistinctArr = distinctArr.Length;
            var resultData = new LinkedList<DtChungTuChiTietModel>();
            for (var i = 0; i < lengthDistinctArr; i++)
            {
                var item = distinctArr[i];
                CalculateRow(resultData, dictDataOrigin, item, totalBudgetEstimateDivisionMapByCategoryId, usedBudgetEstimateDivisionMapByCategoryId, totalBudgetAdjustedMapByCategoryId);
            }

            return resultData;
        }

        private void CalculateRow(
            LinkedList<DtChungTuChiTietModel> resultData,
            Dictionary<string, DtChungTuChiTietModel> dictDataOrigin,
            DtChungTuChiTietModel item,
            Dictionary<string, Dictionary<string, List<NsDtChungTuChiTietQuery>>> totalBudgetEstimateDivisionMapByCategoryId,
            Dictionary<string, Dictionary<string, List<NsDtChungTuChiTietQuery>>> usedBudgetEstimateDivisionMapByCategoryId,
            Dictionary<string, List<NsDtChungTuChiTietQuery>> totalBudgetAdjustedMapByCategoryId)
        {

            //var listInsert = new List<DtChungTuChiTietModel>();
            var listHeader = new List<DtChungTuChiTietModel>();

            foreach (var nhanPhanBo in DtChungTuModelNhanPhanBos)
            {
                var itemPhanBo = ObjectCopier.Clone(item);
                var totalBudgetByIdNhanPhanBo = totalBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(nhanPhanBo.Id.ToString(), new Dictionary<string, List<NsDtChungTuChiTietQuery>>());
                var usedBudgetByIdNhanPhanBo = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(nhanPhanBo.Id.ToString(), new Dictionary<string, List<NsDtChungTuChiTietQuery>>());

                var totalBudget = totalBudgetByIdNhanPhanBo.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());
                var usedBudget = usedBudgetByIdNhanPhanBo.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());
                var totalBudgetAdjusted = totalBudgetAdjustedMapByCategoryId.GetValueOrDefault(item.IIdMlns.ToString(), new List<NsDtChungTuChiTietQuery>());

                itemPhanBo.Id = Guid.Empty;
                itemPhanBo.IIdDtchungTu = Guid.Empty;
                itemPhanBo.FHienVat = totalBudget.Sum(x => x.FHienVat) - usedBudget.Sum(x => x.FHienVat) + totalBudgetAdjusted.Sum(x => x.FHienVat);
                itemPhanBo.FTuChi = totalBudget.Sum(x => x.FTuChi) - usedBudget.Sum(x => x.FTuChi) + totalBudgetAdjusted.Sum(x => x.FTuChi);
                itemPhanBo.FHangNhap = totalBudget.Sum(x => x.FHangNhap) - usedBudget.Sum(x => x.FHangNhap) + totalBudgetAdjusted.Sum(x => x.FHangNhap);
                itemPhanBo.FHangMua = totalBudget.Sum(x => x.FHangMua) - usedBudget.Sum(x => x.FHangMua) + totalBudgetAdjusted.Sum(x => x.FHangMua);
                itemPhanBo.FPhanCap = totalBudget.Sum(x => x.FPhanCap) - usedBudget.Sum(x => x.FPhanCap) + totalBudgetAdjusted.Sum(x => x.FPhanCap);
                itemPhanBo.IsPhanBo = true;
                itemPhanBo.IIdCtduToanNhan = nhanPhanBo.Id;
                itemPhanBo.SoChungTuDotNhan = nhanPhanBo.SSoChungTu;
                itemPhanBo.SSoQuyetDinh = nhanPhanBo.SSoQuyetDinh;

                var itemConlai = ObjectCopier.Clone(item);
                var totalItemConLai = new DivisionEstimateDetailPropertyHelper();
                itemConlai.Id = Guid.Empty;
                itemConlai.IIdDtchungTu = Guid.Empty;
                itemConlai.FHienVat = 0;
                itemConlai.FTuChi = 0;
                itemConlai.FHangNhap = 0;
                itemConlai.FHangMua = 0;
                itemConlai.FPhanCap = 0;
                itemConlai.IsConLai = true;
                itemConlai.SMoTa = "Số chưa phân bổ";
                itemConlai.IIdCtduToanNhan = nhanPhanBo.Id;
                itemConlai.SoChungTuDotNhan = nhanPhanBo.SSoChungTu;
                itemConlai.SSoQuyetDinh = nhanPhanBo.SSoQuyetDinh;

                //listHeader.Add(itemPhanBo);
                listHeader.Add(itemConlai);

                // get all don vi, dot nhan
                foreach (var agency in Agencies)
                {
                    DtChungTuChiTietModel itemFromDB = null;
                    if (item.IIdDtchungTu.HasValue)
                    {
                        var key = string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), agency.ValueItem, nhanPhanBo.Id.ToString());
                        itemFromDB = dictDataOrigin.GetValueOrDefault(key, itemFromDB);
                    }

                    if (itemFromDB != null)
                    {
                        // calculator total
                        totalItemConLai.TotalTuChi += itemFromDB.FTuChi;
                        totalItemConLai.TotalHienVat += itemFromDB.FHienVat;
                        totalItemConLai.TotalHangNhap += itemFromDB.FHangNhap;
                        totalItemConLai.TotalHangMua += itemFromDB.FHangMua;
                        totalItemConLai.TotalPhanCap += itemFromDB.FPhanCap;
                    }
                }

                // calculator total
                itemConlai.FTuChi = itemPhanBo.FTuChi - totalItemConLai.TotalTuChi;
                itemConlai.FHienVat = itemPhanBo.FHienVat - totalItemConLai.TotalHienVat;
                itemConlai.FHangNhap = itemPhanBo.FHangNhap - totalItemConLai.TotalHangNhap;
                itemConlai.FHangMua = itemPhanBo.FHangMua - totalItemConLai.TotalHangMua;
                itemConlai.FPhanCap = itemPhanBo.FPhanCap - totalItemConLai.TotalPhanCap;

                listHeader.ForAll(x =>
                {
                    x.IsModified = false;
                    x.IsSelected = false;
                    x.IIdMaDonVi = string.Empty;
                    x.STenDonVi = string.Empty;
                    resultData.AddLast(x);
                }
                );
                listHeader = new List<DtChungTuChiTietModel>();
            }

        }

        private List<NsDtChungTuChiTietQuery> GetTotalBudgetAvailable()
        {
            var budgetEstimateDivisionCondition = new EstimationVoucherDetailCriteria
            {
                ChungTuId = Model.Id.ToString(),
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                Level = 0,
                Status = NSEntityStatus.ACTIVED
            };

            var listDetail = _chungTuChiTietService.FindBudgetEstimateDivision(budgetEstimateDivisionCondition).ToList();
            return listDetail;
        }

        private List<NsDtChungTuChiTietQuery> GetTotalBudgetUsed(string procedure)
        {
            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                IdDotNhan = Model.IIdDotNhan,
                SoChungTu = Model.SSoChungTu
            };

            var listDetail = _chungTuChiTietService.FindByCond(searchCondition, procedure).ToList();
            return listDetail;
        }

        private void LoadNhanPhanBo()
        {
            var dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(Model.Id.ToString()).ToList();
            var listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            var listNhanPhanBo = new List<NsDtChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _dtChungTuService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            DtChungTuModelNhanPhanBos = _mapper.Map<ObservableCollection<DtChungTuModel>>(listNhanPhanBo);
        }

        private void LoadAgencies()
        {
            var idDonVi = _sessionInfo.IdDonVi;
            var namLamViec = _sessionInfo.YearOfWork;
            var iLoai = SoChungTuType.HospitalEstimate;

            if (Model.ILoaiChungTu.HasValue && VoucherType.NSBD_Key.Equals(Model.ILoaiChungTu.ToString()))
            {
                var listDonVi = _nsDonViService.FindByListIdDonVi(idDonVi, namLamViec);
                if (listDonVi.Any(item => true.Equals(item.BCoNSNganh) && item.ITrangThai == NSEntityStatus.ACTIVED && item.Loai == SoChungTuType.ReceiveEstimate.ToString()))
                {
                    iLoai = 2;
                }
                else
                {
                    iLoai = SoChungTuType.HospitalEstimate;
                }
            }

            var listNsDonVi = _nsDonViService.FindByCondition(iLoai, NSEntityStatus.ACTIVED, namLamViec).ToList();
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                var listIdDonVi = string.IsNullOrEmpty(Model.SDsidMaDonVi) ? new List<string>() : Model.SDsidMaDonVi.Split(",").ToList();
                listNsDonVi = listNsDonVi.Where(x => listIdDonVi.Contains(x.IIDMaDonVi)).ToList();
            }

            Agencies = _mapper.Map<ObservableCollection<ComboboxItem>>(listNsDonVi);
        }

        private DtChungTuChiTietModel CloneRow(DtChungTuChiTietModel sourceItem)
        {
            var targetItem = ObjectCopier.Clone(sourceItem);

            targetItem.Id = Guid.Empty;
            targetItem.IIdDtchungTu = null;
            targetItem.IIdMaDonVi = null;
            targetItem.FTuChi = 0;
            targetItem.FHienVat = 0;
            targetItem.FHangNhap = 0;
            targetItem.FHangMua = 0;
            targetItem.FPhanCap = 0;
            targetItem.SGhiChu = null;
            targetItem.IsModified = true;
            targetItem.IsEnabledCbxDotNhan = true;
            targetItem.IsEnabledCbxDonVi = true;
            targetItem.FTuChiTruocDieuChinh = 0;
            targetItem.FTuChiSauDieuChinh = 0;
            targetItem.FHienVatTruocDieuChinh = 0;
            targetItem.FHienVatSauDieuChinh = 0;

            if (sourceItem.CbxDonVi != null && sourceItem.CbxDonVi.Any())
            {
                var defaultValue = Agencies.ElementAt(0);
                var agency = Agencies.Where(x => x.ValueItem.Equals(sourceItem.IIdMaDonVi)).DefaultIfEmpty(defaultValue).First();
                targetItem.IIdMaDonVi = agency.ValueItem;
                targetItem.STenDonVi = agency.DisplayItem;
            }

            return targetItem;
        }

        private string CreateKeyMapRow(DtChungTuChiTietModel item)
        {
            if (Model.ILoaiDuToan.HasValue && !BudgetType.ADJUSTED.Equals((BudgetType)Model.ILoaiDuToan.Value))
            {
                return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdMaDonVi, item.IIdCtduToanNhan);
            }
            return string.Join(StringUtils.DELIMITER, item.IIdDtchungTu.ToString(), item.IIdMlns.ToString(), item.IIdCtduToanNhan, item.IdDotPhanBoTruoc);
        }

        public string GetPath(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, input);
        }

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_CHITIET_DONVI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.SavedAction = obj =>
            {
                LoadTieuDe();
            };
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private void LoadVoucherType()
        {
            /*var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);

            OnPropertyChanged(nameof(CbxVoucherType));
            OnPropertyChanged(nameof(CbxVoucherTypeSelected));*/
        }

        private void LoadUserAgency()
        {
            _hasParentAgency = false;
            _listAgencyOfUser = _nsDonViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork).ToList();
            if (_listAgencyOfUser.Any(x => x.Loai == LoaiDonVi.ROOT))
                _hasParentAgency = true;
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
                lstChungTu = Models.Where(n => string.IsNullOrEmpty(n.SSoQuyetDinh) && n.DNgayChungTu.HasValue && n.DNgayChungTu.Value.ToString("dd/MM/yyyy").Equals(ngayChungTu)).ToList();
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
                predicate = predicate.And(x => x.ILoai == SoChungTuType.HospitalEstimate);
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
