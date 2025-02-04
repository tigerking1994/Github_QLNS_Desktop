using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.NewSalary.NewReport;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport
{
    public class NewBaoCaoQttxTheoCachTinhLuongViewModel : DialogViewModelBase<TlBaoCaoModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;      
        private ICollectionView _dataDonViView;
        private ICollectionView _dataPhuCapView;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _iDmChuKyService;
        private readonly IDanhMucService _iDanhMucService;
        private readonly ITlQtChungTuChiTietNq104Repository _iTlQtChungTuChiTietRepository;
        private readonly ITlQtChungTuNq104Service _tlQtChungTuService;
        private readonly INsDonViService _donViService;
        private IServiceProvider _serviceProvider;
        private DmChuKy _dmChuKy;
        private string _typeChuky;
        private string _diaDiem;

        public override Type ContentType => typeof(NewBaoCaoQttxTheoCachTinhLuong);
        public TlQtChungTuNq104Model ChungTuModel { get; set; }
        private List<ComboboxItem> _itemsMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonth;
            set => SetProperty(ref _itemsMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value);
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set => SetProperty(ref _selectedYear, value);
        }

        private List<ComboboxItem> _itemsFileExport;
        public List<ComboboxItem> ItemsFileExport
        {
            get => _itemsFileExport;
            set => SetProperty(ref _itemsFileExport, value);
        }

        private ComboboxItem _selectedFileExport;
        public ComboboxItem SelectedFileExport
        {
            get => _selectedFileExport;
            set => SetProperty(ref _selectedFileExport, value);
        }

        private List<ComboboxItem> _itemsCachTinhLuong;
        public List<ComboboxItem> ItemsCachTinhLuong
        {
            get => _itemsCachTinhLuong;
            set => SetProperty(ref _itemsCachTinhLuong, value);
        }

        private ComboboxItem _selectedCachTinhLuong;
        public ComboboxItem SelectedCachTinhLuong
        {
            get => _selectedCachTinhLuong;
            set => SetProperty(ref _selectedCachTinhLuong, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _itemsDonVi;
        public ObservableCollection<TlDmDonViNq104Model> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value) && _dataDonViView != null)
                {
                    _dataDonViView.Refresh();
                }
            }
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => ItemsDonVi.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                foreach (var item in ItemsDonVi) item.IsSelected = _selectAllAgency;
            }
        }

        private string _reportName;
        public string ReportName
        {
            get => _reportName;
            set => SetProperty(ref _reportName, value);
        }

        public string LabelSelectedCountAgency
        {
            get
            {
                var totalCount = ItemsDonVi.Count;
                var totalSelected = ItemsDonVi.Count(item => item.IsSelected);
                return "ĐƠN VỊ";
            }
        }

        private ObservableCollection<ComboboxItem> _itemsUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> ItemsUnitType
        {
            get => _itemsUnitTypes;
            set => SetProperty(ref _itemsUnitTypes, value);
        }

        private ComboboxItem _selectedUnitType;

        public ComboboxItem SelectedUnitType
        {
            get => _selectedUnitType;
            set => SetProperty(ref _selectedUnitType, value);
        }

        private List<ComboboxItem> _ItemsKhoIn;
        public List<ComboboxItem> ItemsKhoIn
        {
            get => _ItemsKhoIn;
            set => SetProperty(ref _ItemsKhoIn, value);
        }

        private ComboboxItem _selectedKhoIn;
        public ComboboxItem SelectedKhoIn
        {
            get => _selectedKhoIn;
            set => SetProperty(ref _selectedKhoIn, value);
        }

        private SessionInfo _sessionInfo;
        public TlRptLuongKeHoachNq104Model TlRptLuongKeHoachModel { get; set; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }

        public NewBaoCaoQttxTheoCachTinhLuongViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            IExportService exportService,
            IServiceProvider serviceProvider,
            IDmChuKyService iDmChuKyService,
            IDanhMucService iDanhMucService,
            ITlQtChungTuChiTietNq104Repository iTlQtChungTuChiTietRepository,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlQtChungTuNq104Service tlQtChungTuService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _exportService = exportService;
            _donViService = donViService;
            _serviceProvider = serviceProvider;
            _iDmChuKyService = iDmChuKyService;
            _iDanhMucService = iDanhMucService;
            
            _iTlQtChungTuChiTietRepository = iTlQtChungTuChiTietRepository;
            _tlQtChungTuService = tlQtChungTuService;

            ExportCommand = new RelayCommand(o => OnExport());
            ExportSignatureActionCommand = new RelayCommand(o => OnOpenCauHinhChuKyDialog());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadCatUnitTypes();
            LoadTypeChuKy();
            LoadMonths();
            LoadYears();
            LoadDonViData();
            LoadLoaiFileOutPut();
            LoadKhoIn();
            LoadDiaDiem();
            LoadCachTinhLuong();
        }

        private void LoadDiaDiem()
        {
            var danhMucDiaDiem = _iDanhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void LoadMonths()
        {
            _itemsMonth = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _itemsMonth.Add(month);
            }
            OnPropertyChanged(nameof(ItemsMonth));
            SelectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem.Equals(ChungTuModel.Thang.ToString()));
        }

        private void LoadYears()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _itemsYear.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            SelectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem.Equals(ChungTuModel.Nam.ToString()));
        }

        private void LoadKhoIn()
        {
            ComboboxItem A3 = new ComboboxItem("Khổ", "A3");
            ComboboxItem A4 = new ComboboxItem("Khổ", "A4");
            ItemsKhoIn = new List<ComboboxItem>() { A3, A4 };
            SelectedKhoIn = A3;
        }

        private void LoadLoaiFileOutPut()
        {
            ItemsFileExport = new List<ComboboxItem>();
            _itemsFileExport.Add(new ComboboxItem("Excel", "Excel"));
            _itemsFileExport.Add(new ComboboxItem("PDF", "PDF"));
            SelectedFileExport = ItemsFileExport.Where(x => x.ValueItem == "PDF").FirstOrDefault();
        }

        private void LoadCachTinhLuong()
        {
            _itemsCachTinhLuong = new List<ComboboxItem>();
            _itemsCachTinhLuong.Add(new ComboboxItem("Lương tháng", CachTinhLuong.CACH0));
            _itemsCachTinhLuong.Add(new ComboboxItem("Lương truy lĩnh", CachTinhLuong.CACH5));
            _selectedCachTinhLuong = _itemsCachTinhLuong.FirstOrDefault();
        }

        private void LoadDonViData()
        {
            List<TlDmDonViNq104> data = new List<TlDmDonViNq104>();
            if (!string.IsNullOrEmpty(ChungTuModel.STongHop))
            {
                var lstIdChungTu = ChungTuModel.STongHop.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                var lstChungTu = _tlQtChungTuService.FindAll(x => lstIdChungTu.Contains(x.Id.ToString()));
                var lstMaDonVi = lstChungTu.Select(x => x.MaDonVi).Distinct().ToList();
                data = _tlDmDonViService.FindByCondition(x => lstMaDonVi.Contains(x.MaDonVi)).ToList();
            }
            else
            {
                data = _tlDmDonViService.FindByCondition(x => x.MaDonVi.Equals(ChungTuModel.MaDonVi)).ToList();
            }
            ItemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            foreach (var dv in ItemsDonVi)
            {
                dv.IsSelected = true;
            }

            if (ItemsDonVi != null && ItemsDonVi.Count > 0)
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault();
            }

            _dataDonViView = CollectionViewSource.GetDefaultView(ItemsDonVi);
        }

        public void LoadCatUnitTypes()
        {
            var listDonViTinh = _iDanhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE)
                .ToList();
            var expenseTypes = new List<ComboboxItem>();
            if (listDonViTinh.Count <= 0)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = "Đồng";
                cb.ValueItem = "1";
                cb.Type = "Đồng";
                expenseTypes.Add(cb);
            }
            else
            {
                listDonViTinh = listDonViTinh.OrderBy(x => x.SGiaTri).ToList();
                foreach (var dvt in listDonViTinh)
                {
                    ComboboxItem cb = new ComboboxItem();
                    cb.DisplayItem = dvt.STen;
                    cb.ValueItem = dvt.SGiaTri;
                    cb.Type = dvt.SMoTa;
                    expenseTypes.Add(cb);
                }
            }
            ItemsUnitType = new ObservableCollection<ComboboxItem>(expenseTypes);
            _selectedUnitType = expenseTypes.ElementAt(0);
        }

        private void LoadTypeChuKy()
        {
            _typeChuky = "";
        }

        private void OnExport()
        {
            ExportType exportType = SelectedFileExport != null && "PDF".Equals(SelectedFileExport.ValueItem)
                ? ExportType.PDF
                : ExportType.EXCEL;
            ExportQuyetToanLuongKeHoach(exportType);
        }

        private void ExportQuyetToanLuongKeHoach(ExportType exportType)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QTTX_THEO_CACH_TINH_LUONG_NEW);
                var nam = int.Parse(SelectedYear.ValueItem);
                var thang = int.Parse(SelectedMonth.ValueItem);
                var cachTinhLuong = _selectedCachTinhLuong.ValueItem;
                string tieuDe1 = "";
                if (cachTinhLuong.Equals(CachTinhLuong.CACH0))
                {
                    tieuDe1 = "(theo số liệu lương tháng)";
                }
                else
                {
                    tieuDe1 = "(theo số liệu lương truy lĩnh)";
                }
                FormatNumber formatNumber = new FormatNumber(1, exportType);
                var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                string maDonVi = "";
                if (!string.IsNullOrEmpty(ChungTuModel.STongHop))
                {
                    var lstMaDonVi = lstDonVi.Select(x => x.MaDonVi).Distinct().ToList();
                    maDonVi = string.Join(",", lstMaDonVi);
                }
                else
                {
                    maDonVi = ChungTuModel.MaDonVi;
                }
                string tenDonVi = ChungTuModel.TenDonVi;

                List<TlQtChungTuChiTietNq104Query> data = new List<TlQtChungTuChiTietNq104Query>();
                if (!string.IsNullOrEmpty(ChungTuModel.STongHop))
                {
                    data = _iTlQtChungTuChiTietRepository.GetDataChungTuChiTietNq104(ChungTuModel.STongHop.ToString(), ChungTuModel.Nam, cachTinhLuong).ToList();
                }
                else
                {
                    data = _iTlQtChungTuChiTietRepository.GetDataChungTuChiTietNq104(ChungTuModel.Id.ToString(), ChungTuModel.Nam, cachTinhLuong).ToList();
                }

                //data = _iTlQtChungTuChiTietRepository.GetDataChungTuChiTiet(ChungTuModel.Id.ToString(), ChungTuModel.Nam, cachTinhLuong).ToList();
                CalculateData(data);
                data = data.Where(x => x.SoNguoi != 0 || x.DieuChinh.GetValueOrDefault() != 0 ).ToList();
                data = new List<TlQtChungTuChiTietNq104Query>(data.OrderBy(x => x.XauNoiMa));
                data.Where(n => !n.BHangCha.GetValueOrDefault()).Select(n => { n.Lns = string.Empty; n.L = string.Empty; n.K = string.Empty; n.M = string.Empty; n.Tm = string.Empty; return n; }).ToList();
                data.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.M)).Select(n => { n.L = string.Empty; n.K = string.Empty; n.Lns = string.Empty; return n; }).ToList();
                data.Where(n => n.BHangCha.GetValueOrDefault() && !string.IsNullOrEmpty(n.Tm)).Select(n => { n.M = string.Empty; return n; }).ToList();

                var items = data.Where(x => x.DieuChinh != 0 && x.DieuChinh != null);

                string title = string.Format("{0} - Tháng {1} - năm {2}", tenDonVi, thang, nam);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Cap1", _iDanhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                dic.Add("Cap2", GetHeader2Report());
                dic.Add("FormatNumber", formatNumber);
                dic.Add("TieuDe1", tieuDe1);
                dic.Add("TieuDe2", title);
                dic.Add("Items", items);
                dic.Add("NgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString("D2"), DateTime.Now.Month, DateTime.Now.Year));
                //AddChuKy(dic, _typeChuky);

                string fileNamePrefix = string.Format("rpt_Qttx_theo_cach_tinh_luong_{0}_{1}", nam, tenDonVi);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<TlQtChungTuChiTietNq104Query>(templateFileName, dic);
                results.Add(new ExportResult(string.Format("{0} - {1}", ChungTuModel.MaDonVi, tenDonVi), fileNameWithoutExtension, null, xlsFile));

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

        private void OnOpenCauHinhChuKyDialog()
        {
            try
            {
                var idTypeBc = _typeChuky;
                var dmChuKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idTypeBc)).FirstOrDefault();
                DmChuKyDialogViewModel dmChuKyDialogViewModel = new DmChuKyDialogViewModel(_mapper, _serviceProvider, _sessionService);
                dmChuKyDialogViewModel.DmChuKyModel =
                    dmChuKy != null ? _mapper.Map<DmChuKyModel>(dmChuKy) : new DmChuKyModel()
                    {
                        IdType = idTypeBc,
                        IdCode = "xx"
                    };
                dmChuKyDialogViewModel.IsLuong = true;
                dmChuKyDialogViewModel.BaoCaoLuongModel = Model;
                dmChuKyDialogViewModel.Init();
                dmChuKyDialogViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public string GetIdTypeBaoCao()
        {
            return "";
        }

        private void CalculateData(List<TlQtChungTuChiTietNq104Query> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.DieuChinh = 0;
                    return x;
                }).ToList();
            var temp = lstQtChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false) && x.TongCong.GetValueOrDefault() != 0 && x.SoNguoi != 0);
            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietNq104Query item, List<TlQtChungTuChiTietNq104Query> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            model.DieuChinh += item.DieuChinh;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        public void AddChuKy(Dictionary<string, object> data, string idType)
        {
            //add ngày địa điểm
            data.Add("Ngay", DateUtils.GetCurrentDateReport());
            data.Add("DiaDiem", _diaDiem);
            // add chữ ký
            var dmChyKy = _iDmChuKyService.FindByCondition(x => x.IdType.Equals(idType) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh1MoTa))
            {
                data.Add("ThuaLenh1", dmChyKy.ThuaLenh1MoTa);
                data.Add("ChucDanh1", dmChyKy.ChucDanh1MoTa);
                data.Add("GhiChuKy1", "(Ký, họ tên)");
                data.Add("Ten1", dmChyKy.Ten1MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh2MoTa))
            {
                data.Add("ThuaLenh2", dmChyKy.ThuaLenh2MoTa);
                data.Add("ChucDanh2", dmChyKy.ChucDanh2MoTa);
                data.Add("GhiChuKy2", "(Ký, họ tên)");
                data.Add("Ten2", dmChyKy.Ten2MoTa);
            }
            if (dmChyKy != null && !string.IsNullOrEmpty(dmChyKy.ThuaLenh3MoTa))
            {
                data.Add("ThuaLenh3", dmChyKy.ThuaLenh3MoTa);
                data.Add("ChucDanh3", dmChyKy.ChucDanh3MoTa);
                data.Add("GhiChuKy3", "(Ký, họ tên, đóng dấu)");
                data.Add("Ten3", dmChyKy.Ten3MoTa);
            }
        }
    }
}
