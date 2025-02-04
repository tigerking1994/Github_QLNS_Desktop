using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using System.IO;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PrintReportProcessProject
{
    public class PrintReportProcessProjectViewModel : DialogViewModelBase<ReportProcessProjectViewModel>
    {
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly IMapper _mapper;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IVdtKhvKeHoachVonUngChiTietService _vonungService;
        private readonly IApproveProjectService _qddautuService;
        private readonly INsDonViService _nsDonViService;
        private ISessionService _sessionService;
        private readonly IExportService _exportService;
        private IVdtKhvPhanBoVonChiTietService _phanBoChiTiet;
        private readonly ILog _logger;
        private List<VdtDaDuAnReportQuery> _listDuAn;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PRINT_REPORT_PROCESS_PROJECT;
        public override string GroupName => MenuItemContants.GROUP_REPORT;
        public override string Name => "Báo cáo tình hình thực hiện dự án";
        public override string Title => "Báo cáo tình hình thực hiện dự án";
        public override string Description => "Báo cáo tình hình thực hiện dự án";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Investment.InvestmentImplementation.PrintReportProcessProject.PrintReportProcessProject);

        private ObservableCollection<ComboboxItem> _dataDonViQuanLy;
        public ObservableCollection<ComboboxItem> DataDonViQuanLy
        {
            get => _dataDonViQuanLy;
            set => SetProperty(ref _dataDonViQuanLy, value);
        }

        private ComboboxItem _selectedDonViQuanLy;
        public ComboboxItem SelectedDonViQuanLy
        {
            get => _selectedDonViQuanLy;
            set
            {
                if (SetProperty(ref _selectedDonViQuanLy, value) && _selectedDonViQuanLy != null)
                {
                    LoadComboboxDuAn(_selectedDonViQuanLy.ValueItem);
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value) && _selectedDuAn != null)
                {
                    LoadInfo(_selectedDuAn.HiddenValue);
                }
            }
        }

        private ObservableCollection<NganSachDuAnInfoQuery> _dataNganSachInfo;
        public ObservableCollection<NganSachDuAnInfoQuery> DataNganSachInfo
        {
            get => _dataNganSachInfo;
            set => SetProperty(ref _dataNganSachInfo, value);
        }

        private ObservableCollection<ReportProcessProjectViewModel> _dataReport;
        public ObservableCollection<ReportProcessProjectViewModel> DataReport
        {
            get => _dataReport;
            set => SetProperty(ref _dataReport, value);
        }

        private DateTime? _thoiGianBaoCao;
        public DateTime? ThoiGianBaoCao
        {
            get => _thoiGianBaoCao;
            set
            {
                SetProperty(ref _thoiGianBaoCao, value);
                LoadInfo(_selectedDuAn != null ? _selectedDuAn.HiddenValue : Guid.Empty.ToString());
            }
        }

        private string _tenDuAn;
        public string TenDuAn
        {
            get => _tenDuAn;
            set => SetProperty(ref _tenDuAn, value);
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get => _soQuyetDinh;
            set => SetProperty(ref _soQuyetDinh, value);
        }

        private string _ngayThangNam;
        public string NgayThangNam
        {
            get => _ngayThangNam;
            set => SetProperty(ref _ngayThangNam, value);
        }

        private string _diaDiem;
        public string DiaDiem
        {
            get => _diaDiem;
            set => SetProperty(ref _diaDiem, value);
        }

        private string _thoiGianThucHien;
        public string ThoiGianThucHien
        {
            get => _thoiGianThucHien;
            set => SetProperty(ref _thoiGianThucHien, value);
        }

        private double _tongMucDauTu;
        public double TongMucDauTu
        {
            get => _tongMucDauTu;
            set => SetProperty(ref _tongMucDauTu, value);
        }

        private decimal _keHoachUng;
        public decimal KeHoachUng
        {
            get => _keHoachUng;
            set => SetProperty(ref _keHoachUng, value);
        }

        private double _vonUngDaCap;
        public double VonUngDaCap
        {
            get => _vonUngDaCap;
            set => SetProperty(ref _vonUngDaCap, value);
        }

        private double _vonUngDaThuHoi;
        public double VonUngDaThuHoi
        {
            get => _vonUngDaThuHoi;
            set => SetProperty(ref _vonUngDaThuHoi, value);
        }

        private double _giaTriConPhaiThuHoi;
        public double GiaTriConPhaiThuHoi
        {
            get => _giaTriConPhaiThuHoi;
            set => SetProperty(ref _giaTriConPhaiThuHoi, value);
        }

        private decimal _luyKeVonDaBoTri;
        public decimal LuyKeVonDaBoTri
        {
            get => _luyKeVonDaBoTri;
            set => SetProperty(ref _luyKeVonDaBoTri, value);
        }

        private decimal _luyKeVonNSQP;
        public decimal LuyKeVonNSQP
        {
            get => _luyKeVonNSQP;
            set => SetProperty(ref _luyKeVonNSQP, value);
        }

        private decimal _luyKeThanhToanQuaKhoBac;
        public decimal LuyKeThanhToanQuaKhoBac
        {
            get => _luyKeThanhToanQuaKhoBac;
            set => SetProperty(ref _luyKeThanhToanQuaKhoBac, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCommand { get; }

        public PrintReportProcessProjectViewModel(INsDonViService nsDonViService,
            IApproveProjectService qddautuService,
            ISessionService sessionService,
            IVdtDaDuAnService vdtDaDuAnService,
            IVdtKhvKeHoachVonUngChiTietService vonungService,
            IExportService exportService,
            IVdtKhvPhanBoVonChiTietService phanboChiTiet,
            ILog logger,
            IMapper mapper)
        {
            _vdtDaDuAnService = vdtDaDuAnService;
            _nsDonViService = nsDonViService;
            _vonungService = vonungService;
            _exportService = exportService;
            _sessionService = sessionService;
            _qddautuService = qddautuService;
            _logger = logger;
            _mapper = mapper;
            _phanBoChiTiet = phanboChiTiet;
            SearchCommand = new RelayCommand(o => OnSearch());
            ExportCommand = new RelayCommand(o => OnExport());
        }

        public override void Init()
        {
            try
            {
                DataReport = new ObservableCollection<ReportProcessProjectViewModel>();
                ThoiGianBaoCao = DateTime.Now;
                LoadComboboxDonVi();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadComboboxDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(n => _lstDonViInclude.Contains(n.Loai)).ToList();
            DataDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            if (DataDonViQuanLy != null && DataDonViQuanLy.Count > 0)
            {
                SelectedDonViQuanLy = DataDonViQuanLy.FirstOrDefault();
            }
        }

        private void HandleData(List<ReportTinhHinhDuAnQuery> lstData)
        {
            try
            {
                lstData.Select(x => { x.TongCongGiaiNgan = x.SoThanhToan + x.SoTamUng - x.SoThuHoiTamUng; return x; }).ToList();
                lstData = lstData.GroupBy(x => new { x.IsHangCha, x.Mlns, x.TenNhaThau, x.SoHopDong, x.SoDeNghi, x.TienHopDong, x.NgayThanhToan, x.NgayCapUng, x.ThoiGianThucHien })
                    .Select(x => new ReportTinhHinhDuAnQuery()
                    {
                        IsHangCha = x.Key.IsHangCha,
                        Mlns = x.Key.Mlns,
                        TenNhaThau = x.Key.TenNhaThau,
                        SoHopDong = x.Key.SoHopDong,
                        ThoiGianThucHien = x.Key.ThoiGianThucHien,
                        TienHopDong = x.Key.TienHopDong,
                        NgayThanhToan = x.Key.NgayThanhToan,
                        SoThanhToan = x.Sum(grp => grp.SoThanhToan),
                        SoTamUng = x.Sum(grp => grp.SoTamUng),
                        SoThuHoiTamUng = x.Sum(grp => grp.SoThuHoiTamUng),
                        TongCongGiaiNgan = x.Sum(grp => grp.TongCongGiaiNgan),
                        NgayCapUng = x.Key.NgayCapUng,
                        SoDaCapUng = x.Sum(grp => grp.SoDaCapUng),
                        SoDeNghi = x.Key.SoDeNghi
                    }).ToList();

                DataReport = _mapper.Map<ObservableCollection<ReportProcessProjectViewModel>>(lstData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSearch()
        {
            try
            {
                if (SelectedDuAn == null || string.IsNullOrEmpty(_selectedDuAn.HiddenValue) || ThoiGianBaoCao == null)
                    return;
                List<ReportTinhHinhDuAnQuery> lstQuery = _vdtDaDuAnService.GetDataReportTinhHinhDuAnV1(_selectedDuAn.HiddenValue, ThoiGianBaoCao.Value).ToList();
                HandleData(lstQuery);
                OnPropertyChanged(nameof(DataReport));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TenDuAn", TenDuAn);
                    data.Add("SoQuyetDinh", SoQuyetDinh);
                    data.Add("NgayThangNam", NgayThangNam);
                    data.Add("TongMucDauTu", TongMucDauTu);
                    data.Add("ThoiGianThucHien", ThoiGianThucHien);
                    data.Add("KeHoachUng", KeHoachUng);
                    data.Add("Items", DataReport);
                    data.Add("Ngansach", DataNganSachInfo);
                    data.Add("LuyKeVonDaBoTri", LuyKeVonDaBoTri);
                    data.Add("LuyKeVonNSQP", LuyKeVonNSQP);
                    data.Add("LuyKeThanhToanQuaKhoBac", LuyKeThanhToanQuaKhoBac);
                    data.Add("Ngay", ThoiGianBaoCao.HasValue ? ThoiGianBaoCao.Value.ToString("dd/MM/yyyy") : string.Empty);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_THDA, ExportFileName.RPT_VDT_TINHHINHTHUCHIENDUAN);
                    string fileNamePrefix = "ReportTinhHinhDuAn";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportProcessProjectViewModel, NganSachDuAnInfoQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, Utility.Enum.ExportType.EXCEL);
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

        private void LoadComboboxDuAn(string idDonVi)
        {
            if (string.IsNullOrEmpty(idDonVi))
            {
                DataDuAn = new ObservableCollection<ComboboxItem>();
                SelectedDuAn = null;
                return;
            }
            _listDuAn = _vdtDaDuAnService.FindDuAnInfoByIdDonVi(idDonVi).ToList();
            DataDuAn = new ObservableCollection<ComboboxItem>();
            foreach (VdtDaDuAnReportQuery item in _listDuAn)
            {
                if (!DataDuAn.Select(n => n.ValueItem).ToList().Contains(item.SMaDuAn))
                    DataDuAn.Add(new ComboboxItem { ValueItem = item.SMaDuAn, DisplayItem = string.Format("{0} - {1}", item.SMaDuAn, item.STenDuAn), HiddenValue = item.IID_DuAnID.ToString() });
            }
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                SelectedDuAn = DataDuAn.FirstOrDefault();
            }
            else
            {
                LoadInfo(Guid.Empty.ToString());
            }
        }

        private void LoadInfo(string idDuAn)
        {
            VdtDaDuAnReportQuery itemDuAn = new VdtDaDuAnReportQuery();
            if (_listDuAn != null)
                itemDuAn = _listDuAn.Where(n => n.IID_DuAnID.ToString() == idDuAn).FirstOrDefault();
            if (itemDuAn == null)
            {
                TenDuAn = string.Empty;
                SoQuyetDinh = string.Empty;
                NgayThangNam = string.Empty;
                DiaDiem = string.Empty;
                ThoiGianThucHien = string.Empty;
                TongMucDauTu = 0;
                VonUngDaCap = 0;
                VonUngDaThuHoi = 0;
                GiaTriConPhaiThuHoi = 0;
            }
            else
            {
                TenDuAn = itemDuAn.STenDuAn;
                SoQuyetDinh = itemDuAn.SoQuyetDinh;
                NgayThangNam = itemDuAn.NgayThangNam.HasValue ? itemDuAn.NgayThangNam.Value.ToString("dd/MM/yyyy") : string.Empty;
                DiaDiem = itemDuAn.SDiaDiem;
                ThoiGianThucHien = string.Format("{0} - {1}", itemDuAn.SKhoiCong, itemDuAn.SKetThuc);
                var objQDDauTu = _qddautuService.FindByDuAnId(itemDuAn.IID_DuAnID);
                if (objQDDauTu != null)
                    TongMucDauTu = objQDDauTu.FTongMucDauTuPheDuyet ?? 0;
                else
                    TongMucDauTu = 0;
                VonUngDaCap = itemDuAn.VonUngDaCap.HasValue ? itemDuAn.VonUngDaCap.Value : 0;
                VonUngDaThuHoi = itemDuAn.VonUngThuHoi.HasValue ? itemDuAn.VonUngThuHoi.Value : 0;
                GiaTriConPhaiThuHoi = VonUngDaCap - VonUngDaThuHoi;
            }
            DataNganSachInfo = new ObservableCollection<NganSachDuAnInfoQuery>(_vdtDaDuAnService.FindNganSachDuAnInfoByIdDuAn(idDuAn));

            if (!string.IsNullOrEmpty(idDuAn) && ThoiGianBaoCao != null)
            {
                List<ChiTieuNganSachQuery> lstChiTieuNganSach = _phanBoChiTiet.GetChiTieuNganSach(idDuAn, ThoiGianBaoCao.Value).ToList();
                LuyKeVonDaBoTri = (decimal)lstChiTieuNganSach.Sum(x => x != null && x.LuyKeVonDaBoTri.HasValue ? x.LuyKeVonDaBoTri.Value : 0);
                LuyKeVonNSQP = (decimal)lstChiTieuNganSach.Sum(x => x != null && x.LuyKeVonNSQP.HasValue ? x.LuyKeVonNSQP.Value : 0);
                LuyKeThanhToanQuaKhoBac = (decimal)lstChiTieuNganSach.Sum(x => x != null && x.LuyKeThanhToanQuaKhoBac.HasValue ? x.LuyKeThanhToanQuaKhoBac.Value : 0);
                KeHoachUng = (decimal)_vonungService.GetkeHoachUng(Guid.Parse(idDuAn), ThoiGianBaoCao.Value);
            }

            OnPropertyChanged(nameof(TenDuAn));
            OnPropertyChanged(nameof(SoQuyetDinh));
            OnPropertyChanged(nameof(NgayThangNam));
            OnPropertyChanged(nameof(DataNganSachInfo));
            OnPropertyChanged(nameof(DiaDiem));
            OnPropertyChanged(nameof(ThoiGianThucHien));
            OnPropertyChanged(nameof(TongMucDauTu));
            OnPropertyChanged(nameof(KeHoachUng));
            OnPropertyChanged(nameof(VonUngDaCap));
            OnPropertyChanged(nameof(VonUngDaThuHoi));
            OnPropertyChanged(nameof(GiaTriConPhaiThuHoi));
        }
    }
}
