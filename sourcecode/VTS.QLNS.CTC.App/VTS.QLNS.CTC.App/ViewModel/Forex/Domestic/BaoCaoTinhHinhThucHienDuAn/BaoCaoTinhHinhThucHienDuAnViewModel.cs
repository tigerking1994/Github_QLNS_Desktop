using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using log4net.DateFormatter;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.BaoCaoTinhHinhThucHienDuAn
{
    public class BaoCaoTinhHinhThucHienDuAnViewModel : GridViewModelBase<NhBaoCaoTinhHinhThucHienDuAnModel>
    {
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaDuAnService _iNhDaDuAnService;
        private readonly INhDaQdDauTuService _iNhDaQdDauTuService;
        private ISessionService _sessionService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly INhDaHopDongService _iNhDaHopDongService;
        private readonly INhKhTongTheNhiemVuChiService _iNhKhTongTheNhiemVuChiService;
        private List<NhDaDuAnTinhHinhDuAnQuery> _listDuAn;
        protected readonly INhThTongHopService _nhThTongHopService;


        public override string FuncCode => NSFunctionCode.INVESTMENT_END_OF_INVESTMENT;
        public override string GroupName => "BÁO CÁO";
        public override string Name => "Báo cáo tình hình thực hiện dự án";
        public override string Title => "Báo cáo tình hình thực hiện dự án";
        public override string Description => "Báo cáo tình hình thực hiện dự án";
        public override PackIconKind IconKind => PackIconKind.ClipboardFileOutline;
        public override Type ContentType => typeof(View.Forex.Domestic.BaoCaoTinhHinhThucHienDuAn.BaoCaoTinhHinhThucHienDuAn);

        private ObservableCollection<ComboboxItem> _itemsDonViQuanLy;
        public ObservableCollection<ComboboxItem> ItemsDonViQuanLy
        {
            get => _itemsDonViQuanLy;
            set => SetProperty(ref _itemsDonViQuanLy, value);
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
                    LoadComboboxHopDong(_selectedDonViQuanLy.ValueItem);
                    LoadComboboxNhiemVuChi(_selectedDonViQuanLy.ValueItem);
                    
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
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
        
        private ObservableCollection<ComboboxItem> _itemsHopDong;
        public ObservableCollection<ComboboxItem> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }

        private ComboboxItem _selectedHopDong;
        public ComboboxItem SelectedHopDong
        {
            get => _selectedHopDong;
            set
            {
                if (SetProperty(ref _selectedHopDong, value) && _selectedHopDong != null)
                {
                    LoadInfoHopDong(SelectedHopDong.HiddenValue);
                    LoadComboboxDuAnByHopDong();

                }
            }
        }
        
        private ObservableCollection<ComboboxItem> _itemsNVC;
        public ObservableCollection<ComboboxItem> ItemsNVC
        {
            get => _itemsNVC;
            set => SetProperty(ref _itemsNVC, value);
        }

        private ComboboxItem _selectedNVC;
        public ComboboxItem SelectedNVC
        {
            get => _selectedNVC;
            set
            {
                SetProperty(ref _selectedNVC, value);
            }
        }

        private List<NhDaHopDongQuery> _listHopDong;
        private List<NhKhTongTheNhiemVuChiQuery> _listNVC;
        private List<NhBaoCaoTinhHinhThucHienDuAnModel> _dataReport;

        private ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel> _displayItems;
        public ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel> DisplayItems
        {
            get => _displayItems;
            set => SetProperty(ref _displayItems, value);
        }

        private NhBaoCaoTinhHinhThucHienDuAnModel _selectedItemTTDATTDA;
        public NhBaoCaoTinhHinhThucHienDuAnModel SelectedItemTTDA
        {
            get => _selectedItemTTDATTDA;
            set => SetProperty(ref _selectedItemTTDATTDA, value);
        }

        private DateTime? _thoiGianBatDau;
        public DateTime? ThoiGianBatDau
        {
            get => _thoiGianBatDau;
            set
            {
                SetProperty(ref _thoiGianBatDau, value);
                LoadInfo(_selectedDuAn != null ? _selectedDuAn.HiddenValue : Guid.Empty.ToString());
            }
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

        private DateTime? _thoiGianKetThuc;
        public DateTime? ThoiGianKetThuc
        {
            get => _thoiGianKetThuc;
            set
            {
                SetProperty(ref _thoiGianKetThuc, value);
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
        private string _sTenChuDauTu;
        public string sTenChuDauTu
        {
            get => _sTenChuDauTu;
            set => SetProperty(ref _sTenChuDauTu, value);
        }
        private string _sPhanCap;
        public string sPhanCap
        {
            get => _sPhanCap;
            set => SetProperty(ref _sPhanCap, value);
        }
        private string _sSoQuyetDinhDauTu;
        public string sSoQuyetDinhDauTu
        {
            get => _sSoQuyetDinhDauTu;
            set => SetProperty(ref _sSoQuyetDinhDauTu, value);
        }

        private DateTime? _dNgayQuyetDinhDauTu;
        public DateTime? dNgayQuyetDinhDauTu
        {
            get => _dNgayQuyetDinhDauTu;
            set => SetProperty(ref _dNgayQuyetDinhDauTu, value);
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
        private double _luyKeCDT;
        public double LuyKeCDT
        {
            get => _luyKeCDT;
            set => SetProperty(ref _luyKeCDT, value);
        }
        private double _luyKeNT;
        public double LuyKeNT
        {
            get => _luyKeNT;
            set => SetProperty(ref _luyKeNT, value);
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

        private string _sSoHopDong;
        public string SSoHopDong
        {
            get => _sSoHopDong;
            set => SetProperty(ref _sSoHopDong, value);
        }
        private string _sTenHopDong;
        public string STenHopDong
        {
            get => _sTenHopDong;
            set => SetProperty(ref _sTenHopDong, value);
        }
        private string _sTenNhaThauHD;
        public string STenNhaThauHD
        {
            get => _sTenNhaThauHD;
            set => SetProperty(ref _sTenNhaThauHD, value);
        }

        private double _dGiaTriVND;
        public double DGiaTriVND
        {
            get => _dGiaTriVND;
            set => SetProperty(ref _dGiaTriVND, value);
        }
        
        private double _dGiaTriUSD;
        public double DGiaTriUSD
        {
            get => _dGiaTriUSD;
            set => SetProperty(ref _dGiaTriUSD, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportCommand { get; }

        public BaoCaoTinhHinhThucHienDuAnViewModel(INsDonViService nsDonViService,
            ISessionService sessionService,
            INhDaDuAnService iNhDaDuAnService,
            INhDaQdDauTuService iNhDaQdDauTuService,
            IExportService exportService,
            ILog logger,
            IMapper mapper,
            INhDaHopDongService iNhDaHopDongService,
            INhKhTongTheNhiemVuChiService iNhKhTongTheNhiemVuChiService,
            INhThTongHopService nhThTongHopService)
        {
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _sessionService = sessionService;
            _iNhDaDuAnService = iNhDaDuAnService;
            _iNhDaQdDauTuService = iNhDaQdDauTuService;
            _logger = logger;
            _mapper = mapper;
            SearchCommand = new RelayCommand(o => OnSearch());
            ExportCommand = new RelayCommand(o => OnExport());
            _iNhDaHopDongService = iNhDaHopDongService;
            _iNhKhTongTheNhiemVuChiService = iNhKhTongTheNhiemVuChiService;
            _nhThTongHopService = nhThTongHopService;
        }

        public override void Init()
        {
            try
            {
                DisplayItems = new ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel>();
                ThoiGianBaoCao = DateTime.Now;
                LoadComboboxDonVi();
                onResetFilter();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void onResetFilter()
        {
            ThoiGianBatDau = null;
            ThoiGianKetThuc = null;
        }

        private void LoadComboboxDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(n => _lstDonViInclude.Contains(n.Loai)).ToList();
            ItemsDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            if (ItemsDonViQuanLy != null && ItemsDonViQuanLy.Count > 0)
            {
                SelectedDonViQuanLy = ItemsDonViQuanLy.FirstOrDefault();
            }
        }

        private void HandleData(List<NhBaoCaoTinhHinhThucHienDuAnQuery> lstData)
        {
            try
            {
                lstData = lstData.Any() ? lstData : new List<NhBaoCaoTinhHinhThucHienDuAnQuery>();
                var itemModel = _mapper.Map<ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel>>(lstData);
                var itemGroup = itemModel.GroupBy(x => new { x.IsHangCha, x.TenNhaThau, x.SoHopDong, x.ThoiGianThucHien, x.GiaTriHopDong}).ToList();
                List<NhBaoCaoTinhHinhThucHienDuAnModel> results = new List<NhBaoCaoTinhHinhThucHienDuAnModel>();
                int stt = 1;
                foreach (var gr in itemGroup)
                {
                    var parent = new NhBaoCaoTinhHinhThucHienDuAnModel()
                    {
                        Stt = stt++.ToString(),
                        TenNhaThau = gr.Key.TenNhaThau,
                        SoHopDong = gr.Key.SoHopDong,
                        ThoiGianThucHien = gr.Key.ThoiGianThucHien,
                        GiaTriHopDong = gr.Key.GiaTriHopDong,
                        NgayThanhToan = null,
                        SoThanhToan = gr.Sum(grp => grp.SoThanhToan),
                        SoTamUng = gr.Sum(grp => grp.SoTamUng),
                        SoThuHoiTamUng = gr.Sum(grp => grp.SoThuHoiTamUng),
                        TongCongGiaiNgan = gr.Sum(grp => grp.TongCongGiaiNgan),
                        Id = gr.First().Id
                    };
                    results.Add(parent);
                }
                //foreach (var item in lstData)
                //{
                //    if (item.iCoQuanThanhToan == null)
                //        continue;
                //    if (item.iCoQuanThanhToan.Value == 1)
                //        LuyKeCDT += (item.fTongPheDuyet_USD ?? 0d);
                //    if (item.iCoQuanThanhToan.Value == 2 && item.iLoaiDeNghi.Value == 1)
                //        LuyKeCDT += item.fTongPheDuyet_USD ?? 0d;
                //    if (item.iCoQuanThanhToan.Value == 2 && item.iLoaiDeNghi.Value != 1)
                //        LuyKeNT += item.fTongPheDuyet_USD ?? 0d;
                //}
                //if(lstData.Count > 0)
                //{
                //    LuyKeCDT = lstData.FirstOrDefault().fGiaTriDuocCap_USD ?? 0d;
                //    LuyKeNT = lstData.FirstOrDefault().fGiaTriTTTU_USD ?? 0d;
                //}

                //DataReport = new ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel>(results);
                DisplayItems = itemModel;
                
                foreach (var item in DisplayItems.Select((value, index) => (value, index)))
                {
                    item.value.Stt = (item.index + 1).ToString();
                    if (_dataReport != null) item.value.HasChildren = _dataReport.Any(t => t.IdParent.Equals(item.value.Id));
                    if (item.value.HasChildren)
                    {
                        item.value.PropertyChanged += NhBaoCaoTinhHinhThucHienDuAnModelPropertyChanged;
                    }
                    item.value.AncestorIds = new HashSet<Guid>();
                    if (item.value.iLoaiNoiDungChi == null)
                        continue;
                    if (item.value.iLoaiNoiDungChi == 1)
                        item.value.sLoaiNoiDung = "Chi ngoại tệ";
                    if (item.value.iLoaiNoiDungChi == 2)
                        item.value.sLoaiNoiDung = "Chi trong nước";
                    if (item.value.iCoQuanThanhToan == 1)
                        item.value.sCoQuanTT = "CTC cấp";
                    if (item.value.iCoQuanThanhToan == 2)
                        item.value.sCoQuanTT = "Đơn vị cấp";
                    if (item.value.iLoaiDeNghi == (int)NhLoaiDeNghi.Type.CAP_KINH_PHI)
                        item.value.sLoaiDeNghiTT = NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.CAP_KINH_PHI);
                    if (item.value.iLoaiDeNghi == (int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI)
                        item.value.sLoaiDeNghiTT = NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_KINH_PHI);
                    if (item.value.iLoaiDeNghi == (int)NhLoaiDeNghi.Type.THANH_TOAN)
                        item.value.sLoaiDeNghiTT = NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.THANH_TOAN);
                    if (item.value.iLoaiDeNghi == (int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO)
                        item.value.sLoaiDeNghiTT = NhLoaiDeNghi.Get((int)NhLoaiDeNghi.Type.TAM_UNG_THEO_CHE_DO);
                    if (item.value.dNgayDeNghi != null)
                        item.value.NgayDeNghi = item.value.dNgayDeNghi.ToShortDateString();
                    _dataReport = DisplayItems.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExpand()
        {
            int currentIndex = DisplayItems.IndexOf(SelectedItemTTDA);
            SelectedItemTTDA.IsShowChildren = true;
            IEnumerable<NhBaoCaoTinhHinhThucHienDuAnModel> children = new List<NhBaoCaoTinhHinhThucHienDuAnModel>(_dataReport.Where(t => t.IdParent.HasValue && t.IdParent.Equals(SelectedItemTTDA.Id)));
            foreach (var item in children)
            {
                //item.Stt = SelectedItemTTDA.Stt + "_" + ++stt;
                item.AncestorIds = new HashSet<Guid>();
                foreach (var ancestor in SelectedItemTTDA.AncestorIds)
                {
                    item.AncestorIds.Add(ancestor);
                }
                item.AncestorIds.Add(SelectedItemTTDA.Id);
                item.HasChildren = _dataReport.Any(t => t.IdParent.Equals(item.Id));
                if (item.HasChildren)
                {
                    item.PropertyChanged += NhBaoCaoTinhHinhThucHienDuAnModelPropertyChanged;
                }
                DisplayItems.Insert(++currentIndex, item);
            }
            //OnPropertyChanged(nameof(DisplayItems));
        }

        private void OnCollapse()
        {
            SelectedItemTTDA.IsShowChildren = false;
            var collapseItems = DisplayItems.Where(t => t.AncestorIds.Contains(SelectedItemTTDA.Id));
            foreach (var item in collapseItems)
            {
                item.IsShowChildren = false;
                item.PropertyChanged -= NhBaoCaoTinhHinhThucHienDuAnModelPropertyChanged;
            }
            DisplayItems = new ObservableCollection<NhBaoCaoTinhHinhThucHienDuAnModel>(DisplayItems.Where(t => !t.AncestorIds.Contains(SelectedItemTTDA.Id)));
            var stt = 0;
            foreach (var item in DisplayItems)
            {
                item.Stt = (++stt).ToString();
            }
        }

        private void NhBaoCaoTinhHinhThucHienDuAnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(NhBaoCaoTinhHinhThucHienDuAnModel.IsShowChildren)))
            {
                NhBaoCaoTinhHinhThucHienDuAnModel model = sender as NhBaoCaoTinhHinhThucHienDuAnModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private void OnSearch()
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedDuAn?.HiddenValue) && string.IsNullOrEmpty(SelectedHopDong?.HiddenValue))
                {
                    MessageBoxHelper.Info(Resources.InValidProjectConTract);
                    return;
                }

                List<NhBaoCaoTinhHinhThucHienDuAnQuery> lstQuery = _iNhDaDuAnService.GetDataReportTinhHinhThucHienDuAn((_selectedDuAn?.HiddenValue), ThoiGianBatDau, ThoiGianKetThuc, (_selectedHopDong?.HiddenValue), (SelectedNVC?.ValueItem)).ToList();
                LuyKeCDT = 0;
                LuyKeNT = 0;
                HandleData(lstQuery);
                LoadDataTongHop();
                OnPropertyChanged(nameof(_dataReport));
                OnPropertyChanged(nameof(DisplayItems));
                OnPropertyChanged(nameof(LuyKeCDT));
                OnPropertyChanged(nameof(LuyKeNT));
                OnPropertyChanged(nameof(ThoiGianBatDau));
                OnPropertyChanged(nameof(ThoiGianKetThuc));
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
                    FormatNumber formatNumber = new FormatNumber(1, Utility.Enum.ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("TenDuAn", TenDuAn);
                    data.Add("SoQuyetDinh", sSoQuyetDinhDauTu);
                    data.Add("NgayThangNam", NgayThangNam);
                    data.Add("TongMucDauTu", TongMucDauTu);
                    data.Add("ThoiGianThucHien", ThoiGianThucHien);
                    data.Add("sTenChuDauTu", sTenChuDauTu);
                    data.Add("Items", DisplayItems.ToList<NhBaoCaoTinhHinhThucHienDuAnModel>());
                    data.Add("sPhanCap", sPhanCap);
                    data.Add("LuyKeCDT", LuyKeCDT);
                    data.Add("LuyKeNT", LuyKeNT);
                    data.Add("NgayBatDau", ThoiGianBatDau != null ? ThoiGianBatDau.Value.ToString("dd/MM/yyyy") : string.Empty);
                    data.Add("NgayKetThuc", ThoiGianKetThuc != null ? ThoiGianKetThuc.Value.ToString("dd/MM/yyyy") : string.Empty);
                    data.Add("Ngay", DateTime.Now.Day.ToString());
                    data.Add("Thang", DateTime.Now.Month.ToString());
                    data.Add("Nam", DateTime.Now.Year.ToString());
                    string templateFileName = Path.Combine(ExportPrefix.PATH_NH_THDA, ExportFileName.RPT_NH_TINHHINHTHUCHIENDUAN);
                    string fileNamePrefix = "ReportTinhHinhDuAn";
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<NhBaoCaoTinhHinhThucHienDuAnModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, Utility.Enum.ExportType.PDF);
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

        private void LoadComboboxHopDong(string idMadonVi)
        {
            ItemsHopDong = new ObservableCollection<ComboboxItem>();
            if (string.IsNullOrWhiteSpace(idMadonVi))
            {
                SelectedHopDong = null;
                return;
            }

            _listHopDong = _iNhDaHopDongService.FindByIdDonVi(Guid.TryParse( _selectedDonViQuanLy.HiddenValue, out Guid idDonVi) ? idDonVi : Guid.Empty).ToList();
            if (_listHopDong.Any())
                ItemsHopDong = _mapper.Map<ObservableCollection<ComboboxItem>>(_listHopDong);
            //if (ItemsHopDong.Any())
            //{
            //    SelectedHopDong = ItemsHopDong.FirstOrDefault();
            //}
            //else
            //{
            //    LoadInfoHopDong(Guid.Empty.ToString());
            //}
        }

        private void LoadComboboxNhiemVuChi(string idMadonVi)
        {
            ItemsNVC = new ObservableCollection<ComboboxItem>();
            if (string.IsNullOrWhiteSpace(idMadonVi))
            {
                SelectedHopDong = null;
                return;
            }

            _listNVC = _iNhKhTongTheNhiemVuChiService.FindByIdDonVi(Guid.TryParse(SelectedDonViQuanLy.HiddenValue, out Guid idDonvi) ? idDonvi : Guid.Empty).ToList();
            if (_listNVC.Any())
                ItemsNVC = _mapper.Map<ObservableCollection<ComboboxItem>>(_listNVC);
            if (ItemsNVC.Any())
                SelectedNVC = ItemsNVC.FirstOrDefault();
        }

        private void LoadComboboxDuAn(string idDonVi)
        {
            if (string.IsNullOrEmpty(idDonVi))
            {
                ItemsDuAn = new ObservableCollection<ComboboxItem>();
                SelectedDuAn = null;
                return;
            }
            _listDuAn = _iNhDaDuAnService.GetInfoDuAnTinhHinhDuAnReport(_sessionService.Current.YearOfWork, idDonVi).ToList();
            ItemsDuAn = new ObservableCollection<ComboboxItem>();
            foreach (var item in _listDuAn)
            {
                //if (!ItemsDuAn.Select(n => n.ValueItem).ToList().Contains(item.SMaDuAn))
                ItemsDuAn.Add(new ComboboxItem { ValueItem = item.SMaDuAn, DisplayItem = string.Format("{0} - {1}", item.SMaDuAn, item.STenDuAn), HiddenValue = item.Id.ToString() });
            }
            if (ItemsDuAn != null && ItemsDuAn.Count > 0)
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault();
            }
            else
            {
                LoadInfo(Guid.Empty.ToString());
            }
        }

        private void LoadComboboxDuAnByHopDong()
        {
            if (_listDuAn.Any())
            {
                var lstIdDuAnByHopDong = _listHopDong.Select(x => x.IIdDuAnId.ToString() ?? null).ToList();
                ItemsDuAn = new ObservableCollection<ComboboxItem>(ItemsDuAn.Where(x => lstIdDuAnByHopDong.Contains(x.HiddenValue)));
            }
            if (ItemsDuAn.Any())
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault();
            }
            else
            {
                LoadInfo(Guid.Empty.ToString());
            }
        }

        private void LoadInfoHopDong(string idHopDong)
        {
            NhDaHopDongQuery itemHopDong = new NhDaHopDongQuery();
            if (_listHopDong.Any())
                itemHopDong = _listHopDong.FirstOrDefault(x => x.Id.ToString().Equals(idHopDong));
            if (itemHopDong != null)
            {
                STenHopDong = itemHopDong.STenHopDong;
                SSoHopDong = itemHopDong.SSoHopDong;
                STenNhaThauHD = itemHopDong.STenNhaThauThucHien;
                DGiaTriUSD = itemHopDong.FGiaTriUsd;
                DGiaTriVND = itemHopDong.FGiaTriVnd;
            }
        }

        private void LoadInfo(string idDuAn)
        {
            NhDaDuAnTinhHinhDuAnQuery itemDuAn = new NhDaDuAnTinhHinhDuAnQuery();
            if (_listDuAn != null)
                itemDuAn = _listDuAn.FirstOrDefault(n => n.Id.ToString() == idDuAn);
            if (itemDuAn == null)
            {
                TenDuAn = string.Empty;
                SoQuyetDinh = string.Empty;
                NgayThangNam = string.Empty;
                sTenChuDauTu = string.Empty;
                sPhanCap = string.Empty;
                sSoQuyetDinhDauTu = string.Empty;
                DiaDiem = string.Empty;
                ThoiGianThucHien = string.Empty;
                dNgayQuyetDinhDauTu = null;
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
                sTenChuDauTu = itemDuAn.sTenChuDauTu;
                sPhanCap = itemDuAn.sPhanCap;
                sSoQuyetDinhDauTu = itemDuAn.sSoQuyetDinhDauTu;
                dNgayQuyetDinhDauTu = itemDuAn.dNgayQuyetDinhDauTu;
                DiaDiem = itemDuAn.SDiaDiem;
                ThoiGianThucHien = string.Format("{0} - {1}", itemDuAn.SKhoiCong, itemDuAn.SKetThuc);
                var objQDDauTu = _iNhDaQdDauTuService.FindByDuAnId(itemDuAn.Id);
                if (objQDDauTu != null)
                {
                    TongMucDauTu = objQDDauTu.FGiaTriUsd.GetValueOrDefault();
                }
                else
                {
                    TongMucDauTu = 0;
                }
                VonUngDaCap =  0;
                VonUngDaThuHoi =  0;
                GiaTriConPhaiThuHoi = VonUngDaCap - VonUngDaThuHoi;
            }


            OnPropertyChanged(nameof(TenDuAn));
            OnPropertyChanged(nameof(SoQuyetDinh));
            OnPropertyChanged(nameof(NgayThangNam));
            OnPropertyChanged(nameof(sTenChuDauTu));
            OnPropertyChanged(nameof(sPhanCap));
            OnPropertyChanged(nameof(ThoiGianThucHien));
            OnPropertyChanged(nameof(TongMucDauTu));
            OnPropertyChanged(nameof(KeHoachUng));
            OnPropertyChanged(nameof(VonUngDaCap));
            OnPropertyChanged(nameof(VonUngDaThuHoi));
            OnPropertyChanged(nameof(GiaTriConPhaiThuHoi));
        }

        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }

        private void LoadDataTongHop()
        {
            if ((SelectedHopDong == null && SelectedDuAn == null) || ThoiGianBatDau == null || ThoiGianKetThuc == null)
            {
                LuyKeCDT = 0;
                LuyKeNT = 0;
            }
            else
            {
                List<NHTHTongHop> data = new List<NHTHTongHop>();
                var lstSMaNguon = NHConstants.MA_TH_BC_NCCQ_MINUS.Split(StringUtils.COMMA).Select(s => s.Trim());
                var lstSMaNguonQT = NHConstants.MA_TH_BCTH_NS_QUY.Split(StringUtils.COMMA).Select(s => s.Trim());
                var predicate = PredicateBuilder.True<NHTHTongHop>();
                //predicate = predicate.And(x => x.INamKeHoach == Model.INamKeHoach);
                predicate = predicate.And(x => (lstSMaNguonQT.Contains(x.SMaNguon) || lstSMaNguonQT.Contains(x.SMaNguonCha))
                                                && x.INamKeHoach >= ThoiGianBatDau.Value.Year && x.INamKeHoach <= ThoiGianKetThuc.Value.Year);
                predicate = predicate.Or(x => (lstSMaNguon.Contains(x.SMaNguon) || lstSMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == ThoiGianBatDau.Value.Year - 1);
                var lstData = _nhThTongHopService.FindByCondition(predicate);
                if (lstData.Any())
                {
                    // data minus nguon 131,132
                    var lstSmaNguonMinus = NHConstants.MA_TH_BC_NCCQ.Split(StringUtils.COMMA).Select(s => s.Trim());
                    var dataMinus = lstData.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha));
                    var dataMinusUsd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) ?? 0;
                    var dataMinusVnd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(s => s.FGiaTriVnd) ?? 0;
                    //data lũy kế phí được cấp
                    lstSMaNguon = NHConstants.MA_TH_BC_NCHTDA_KPC.Split(StringUtils.COMMA).Select(s => s.Trim());
                    var dataLKCap = lstData.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha));
                    var dataLKCapUsd = dataLKCap.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataLKCap.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) ?? 0;
                    var dataLKCapVnd = dataLKCap.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataLKCap.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriVnd) ?? 0;
                    // data lũy kế kinh phí đã giải ngân
                    lstSMaNguon = NHConstants.MA_TH_BC_NCHTDA_KPDGN.Split(StringUtils.COMMA).Select(s => s.Trim());
                    var dataLKDaGiaiNgan = lstData.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha));
                    var dataLKDaGiaiNganUsd = dataLKDaGiaiNgan.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataLKDaGiaiNgan.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) ?? 0;
                    var dataLKDaGiaiNganVnd = dataLKDaGiaiNgan.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriUsd) - dataLKDaGiaiNgan.Where(x => lstSMaNguon.Contains(x.SMaNguon)).Sum(s => s.FGiaTriVnd) ?? 0;

                    //Set data lũy kế
                    LuyKeCDT = dataLKCapUsd - dataMinusUsd;
                    LuyKeNT = dataLKDaGiaiNganUsd - dataMinusUsd;
                }
            }
            OnPropertyChanged(nameof(LuyKeCDT));
            OnPropertyChanged(nameof(LuyKeNT));
        }
    }
}
