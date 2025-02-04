using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using FlexCel.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel.NewSalary.NewReport;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan
{
    public class NewSalaryYearPlanDetailViewModel : DetailViewModelBase<TlDsBangLuongKeHoachNq104Model, TlBangLuongKeHoachNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private readonly ITlBangLuongKeHoachNq104Service _tlBangLuongKeHoachService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ITlPhuCapMlnsNq104Service _iTlPhuCapMlnsService;
        private readonly IExportService _exportService;
        private readonly ITlDieuChinhQsKeHoachNq104Service _tlDieuChinhQsKeHoachService;
        private readonly ITlQtChungTuChiTietKeHoachNq104Service _tlQtChungTuChiTietKeHoachService;
        private readonly ITlDmCanBoKeHoachNq104Service _iDmCanBoKeHoachService;
        private readonly ITlBaoCaoNq104Service _tlBaoCaoService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private ICollectionView _chungTuView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_DETAIL;

        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Bảng lương năm kế hoạch";
        public override Type ContentType => typeof(View.NewSalary.NewSalaryManagementPlan.NewSalaryYearPlan.NewSalaryYearPlanDetail);
        public override PackIconKind IconKind => PackIconKind.Finance;
        public override string Title => "Bảng lương năm kế hoạch ";
        public override string Description => "Bảng lương năm kế hoạch " + Model.TenDonVi + " - Năm " + Model.Nam + " theo mục lục ngân sách";

        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand ExportPdfCommand { get; }
        public RelayCommand ExportExcelChiTietCanBoCommand { get; }
        public RelayCommand ExportPdfChiTietCanBoCommand { get; }

        private List<ComboboxItem> _itemsTypeExport;
        public List<ComboboxItem> ItemsTypeExport
        {
            get => _itemsTypeExport;
            set => SetProperty(ref _itemsTypeExport, value);
        }

        private ComboboxItem _selectedTypeExport;
        public ComboboxItem SelectedTypeExport
        {
            get => _selectedTypeExport;
            set
            {
                if(SetProperty(ref _selectedTypeExport, value) && _chungTuView != null)
                {
                    _chungTuView.Refresh();
                }
            }
        }

        private DataTable _dataBangluong;
        public DataTable DataBangLuong
        {
            get => _dataBangluong;
            set => _dataBangluong = value;
        }

        private NsMuclucNgansachModel _detailFilter;
        public NsMuclucNgansachModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private DataTable _dataBangluongClone;
        public DataTable DataBangLuongClone
        {
            get => _dataBangluongClone;
            set => SetProperty(ref _dataBangluongClone, value);
        }

        private ObservableCollection<TlQtChungTuChiTietKeHoachNq104Model> _itemsChungTuChiTiet;
        public ObservableCollection<TlQtChungTuChiTietKeHoachNq104Model> ItemsChungTuChiTiet
        {
            get => _itemsChungTuChiTiet;
            set => SetProperty(ref _itemsChungTuChiTiet, value);
        }

        private TlQtChungTuChiTietKeHoachNq104Model _selectedItemChungTu;
        public TlQtChungTuChiTietKeHoachNq104Model SelectedItemChungTu
        {
            get => _selectedItemChungTu;
            set => SetProperty(ref _selectedItemChungTu, value);
        }

        private decimal _tongQuyetToan;
        public decimal TongQuyetToan
        {
            get => _tongQuyetToan;
            set
            {
                SetProperty(ref _tongQuyetToan, value);
                CalculateTongCong();
            }
        }

        private decimal _chiPhiVaoQuan;
        public decimal ChiPhiVaoQuan
        {
            get => _chiPhiVaoQuan;
            set => SetProperty(ref _chiPhiVaoQuan, value);
        }

        private decimal _phuCapRaQuan;
        public decimal PhuCapRaQuan
        {
            get => _phuCapRaQuan;
            set => SetProperty(ref _phuCapRaQuan, value);
        }

        private decimal _saiSo;
        public decimal SaiSo
        {
            get => _saiSo;
            set => SetProperty(ref _saiSo, value);
        }

        private decimal _phanTram;
        public decimal PhanTram
        {
            get => _phanTram;
            set
            {
                SetProperty(ref _phanTram, value);
                CalculateTongCong();
            }
        }

        private decimal _tongCong;
        public decimal TongCong
        {
            get => _tongCong;
            set => SetProperty(ref _tongCong, value);
        }

        public NewSalaryYearPlanVolatilityViewModel VolatilitySalaryYearPlanDialogViewModel { get; }
        public NewReportDialogViewModel ReportDialogViewModel { get; }

        public RelayCommand ShowPopupDetailCommand { get; }
        public RelayCommand ExportBcMlnsCommand { get; }

        public NewSalaryYearPlanDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlBangLuongKeHoachNq104Service tlBangLuongKeHoachService,
            IExportService exportService,
            ITlDieuChinhQsKeHoachNq104Service tlDieuChinhQsKeHoachService,
            ITlQtChungTuChiTietKeHoachNq104Service tlQtChungTuChiTietKeHoachService,
            ITlPhuCapMlnsNq104Service iTlPhuCapMlnsService,
            IDanhMucService danhMucService,
            INsDonViService donViService,
            ITlDmCanBoKeHoachNq104Service iDmCanBoKeHoachService,
            ITlBaoCaoNq104Service tlBaoCaoService,
            ITlDmDonViNq104Service tlDmDonViService,
            NewSalaryYearPlanVolatilityViewModel volatilitySalaryYearPlanDialogViewModel,
            NewReportDialogViewModel reportDialogViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _danhMucService = danhMucService;
            _donViService = donViService;
            _tlBangLuongKeHoachService = tlBangLuongKeHoachService;
            _exportService = exportService;
            _tlDieuChinhQsKeHoachService = tlDieuChinhQsKeHoachService;
            _tlQtChungTuChiTietKeHoachService = tlQtChungTuChiTietKeHoachService;
            _iTlPhuCapMlnsService = iTlPhuCapMlnsService;
            _iDmCanBoKeHoachService = iDmCanBoKeHoachService;
            _tlBaoCaoService = tlBaoCaoService;
            _tlDmDonViService = tlDmDonViService;

            VolatilitySalaryYearPlanDialogViewModel = volatilitySalaryYearPlanDialogViewModel;
            VolatilitySalaryYearPlanDialogViewModel.ParentPage = this;
            ReportDialogViewModel = reportDialogViewModel;
            ReportDialogViewModel.ParentPage = this;

            ExportExcelCommand = new RelayCommand(obj => ExportLuongNam(ExportType.EXCEL));
            ExportPdfCommand = new RelayCommand(obj => ExportLuongNam(ExportType.PDF));
            ExportExcelChiTietCanBoCommand = new RelayCommand(obj => ExportChiTietCanBoMlns(ExportType.EXCEL));
            ExportPdfChiTietCanBoCommand = new RelayCommand(obj => ExportChiTietCanBoMlns(ExportType.PDF));
            ExportBcMlnsCommand = new RelayCommand(o => OnOpenReportDialog());

            ShowPopupDetailCommand = new RelayCommand(o => OnSelectionDoubleClick(o));
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            base.Init();
            DetailFilter = new NsMuclucNgansachModel();
            var lstDieuChinhQsKeHoach = _tlDieuChinhQsKeHoachService.FindData(Model.Nam, Model.MaDonVi);
            if (lstDieuChinhQsKeHoach != null && lstDieuChinhQsKeHoach.Count() > 0)
            {
                ChiPhiVaoQuan = (decimal)lstDieuChinhQsKeHoach.Sum(x => x.FLuongTuyenSinh);
                var raQuan = lstDieuChinhQsKeHoach.Sum(x => x.FPcrqBinhNhat ?? 0) + lstDieuChinhQsKeHoach.Sum(x => x.FPcrqBinhNhi ?? 0)
                    + lstDieuChinhQsKeHoach.Sum(x => x.FPcrqHaSi ?? 0) + lstDieuChinhQsKeHoach.Sum(x => x.FPcrqThuongSi ?? 0) + lstDieuChinhQsKeHoach.Sum(x => x.FPcrqTrungSi ?? 0);
                PhuCapRaQuan = raQuan;
            }
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            var data = _tlQtChungTuChiTietKeHoachService.GetDataChungTuChiTiet(Model.Nam - 1, Model.Nam, Model.MaDonVi).ToList();
            _itemsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQtChungTuChiTietKeHoachNq104Model>>(data);

            var ctPcqr = _itemsChungTuChiTiet.FirstOrDefault(x => "1010000-010-011-8000-8049-10-00".Equals(x.XauNoiMa));
            if (ctPcqr != null)
            {
                ctPcqr.TongCong = ctPcqr.TongCong == null ? PhuCapRaQuan : (ctPcqr.TongCong + PhuCapRaQuan);
                ctPcqr.DieuChinh = ctPcqr.DieuChinh == null ? PhuCapRaQuan : (ctPcqr.DieuChinh + PhuCapRaQuan);
            }

            var ctPcqh = _itemsChungTuChiTiet.FirstOrDefault(x => "1010000-010-011-6400-6449-10-00".Equals(x.XauNoiMa));
            if (ctPcqh != null)
            {
                ctPcqh.TongCong = ctPcqh.TongCong == null ? ChiPhiVaoQuan : (ctPcqh.TongCong + ChiPhiVaoQuan);
                ctPcqh.DieuChinh = ctPcqh.DieuChinh == null ? ChiPhiVaoQuan : (ctPcqh.DieuChinh + ChiPhiVaoQuan);
            }

            CalculateData(_itemsChungTuChiTiet.ToList());

            if (_itemsChungTuChiTiet != null && _itemsChungTuChiTiet.Count > 0)
            {
                TongQuyetToan = (decimal)_itemsChungTuChiTiet.FirstOrDefault().DieuChinh;
            }

            foreach (var item in _itemsChungTuChiTiet)
            {
                if (item.DieuChinh != null && item.TongNamTruoc == null)
                {
                    item.ChenhLech = item.DieuChinh;
                }
                if (item.DieuChinh == null && item.TongNamTruoc != null)
                {
                    item.ChenhLech = -item.TongNamTruoc;
                }
                if (item.DieuChinh != null && item.TongNamTruoc != null)
                {
                    item.ChenhLech = item.DieuChinh - item.TongNamTruoc;
                }
            }

            foreach (var item in _itemsChungTuChiTiet)
            {
                if (item.BHangCha == false)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            _chungTuView = CollectionViewSource.GetDefaultView(_itemsChungTuChiTiet);
            _chungTuView.Filter = ChungTuFilter;

            OnPropertyChanged(nameof(ItemsChungTuChiTiet));
            LoadTypeExport();
        }

        private void LoadTypeExport()
        {
            ItemsTypeExport = new List<ComboboxItem>();
            _itemsTypeExport.Add(new ComboboxItem("Hiển thị tất cả", "1"));
            _itemsTypeExport.Add(new ComboboxItem("Có dữ liệu", "2"));
            _selectedTypeExport = ItemsTypeExport.Where(x => x.ValueItem == "1").FirstOrDefault();

            OnPropertyChanged(nameof(_selectedTypeExport));
        }

        private void CalculateData(List<TlQtChungTuChiTietKeHoachNq104Model> lstQtChungTuChiTiet)
        {
            lstQtChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.TongCong = 0;
                    x.DieuChinh = 0;
                    x.TongNamTruoc = 0;
                    return x;
                }).ToList();
            var temp = lstQtChungTuChiTiet.Where(x => (!x.BHangCha.GetValueOrDefault(false) && x.TongCong != null && x.TongCong != 0)
            || (!x.BHangCha.GetValueOrDefault(false) && x.DieuChinh != null && x.DieuChinh != 0)
            || (!x.BHangCha.GetValueOrDefault(false) && x.TongNamTruoc != null && x.TongNamTruoc != 0));
            foreach (var item in temp)
            {
                CalculateParent(item.MlnsIdParent, item, lstQtChungTuChiTiet);
            }
        }

        private void CalculateParent(Guid? idParent, TlQtChungTuChiTietKeHoachNq104Model item, List<TlQtChungTuChiTietKeHoachNq104Model> lstQtChungTuChiTiet)
        {
            var model = lstQtChungTuChiTiet.FirstOrDefault(x => x.MlnsId == idParent);
            if (model == null) return;
            model.TongCong += item.TongCong ?? 0;
            model.DieuChinh += item.DieuChinh ?? 0;
            model.TongNamTruoc += item.TongNamTruoc ?? 0;
            CalculateParent(model.MlnsIdParent, item, lstQtChungTuChiTiet);
        }

        private void OnOpenReportDialog()
        {
            TlBaoCaoModel tlBaoCaoModel = new TlBaoCaoModel();
            var lstBaoCao = _tlBaoCaoService.FindAll();
            var baoCao = lstBaoCao.FirstOrDefault(x => x.MaBaoCao.Equals("12.5"));
            if (baoCao != null)
            {
                tlBaoCaoModel = _mapper.Map<TlBaoCaoModel>(baoCao);
            }
            tlBaoCaoModel.SelectedYear = Model.Nam;

            TlRptLuongKeHoachNq104Model tlRptLuongKeHoachModel = new TlRptLuongKeHoachNq104Model();
            tlRptLuongKeHoachModel.ItemsChungTuChiTiet = ItemsChungTuChiTiet;
            tlRptLuongKeHoachModel.TongQuyetToan = TongQuyetToan;
            tlRptLuongKeHoachModel.ChiPhiVaoQuan = ChiPhiVaoQuan;
            tlRptLuongKeHoachModel.PhanTram = PhanTram;
            tlRptLuongKeHoachModel.SaiSo = SaiSo;
            tlRptLuongKeHoachModel.TongCong = TongCong;

            ReportDialogViewModel.TlRptLuongKeHoachModel = tlRptLuongKeHoachModel;
            ReportDialogViewModel.Model = tlBaoCaoModel;
            ReportDialogViewModel.LoaiBaoCao = BaoCaoLuong.LKHMLNS;
            ReportDialogViewModel.Init();
            var donVi = ReportDialogViewModel.ItemsDonVi.FirstOrDefault(x => Model.MaDonVi.Equals(x.MaDonVi));
            if (donVi != null)
            {
                donVi.IsSelected = true;
            }
            ReportDialogViewModel.ShowDialogHost("NEW_SALARYDetailDialog");
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenCadresDetail((TlQtChungTuChiTietKeHoachNq104Model)obj);
        }

        private void OnOpenCadresDetail(TlQtChungTuChiTietKeHoachNq104Model obj)
        {
            try
            {
                if (obj == null || obj.BHangCha == true)
                {
                    return;
                }

                VolatilitySalaryYearPlanDialogViewModel.Model = obj;
                VolatilitySalaryYearPlanDialogViewModel.Init();
                VolatilitySalaryYearPlanDialogViewModel.ShowDialogHost("SalaryDetailDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            base.OnSave();

            var lstUpdate = _itemsChungTuChiTiet.Where(x => x.IsModified && !Guid.Empty.Equals(x.Id) && x.BHangCha == false);
            var lstAdd = _itemsChungTuChiTiet.Where(x => x.IsModified && Guid.Empty.Equals(x.Id) && x.BHangCha == false);

            var lstAddEntity = new List<TlQtChungTuChiTietKeHoachNq104>();
            foreach (var item in lstAdd)
            {
                TlQtChungTuChiTietKeHoachNq104 tlQtChungTuChiTietKeHoach = new TlQtChungTuChiTietKeHoachNq104();
                tlQtChungTuChiTietKeHoach = _mapper.Map<TlQtChungTuChiTietKeHoachNq104>(item);

                if ("1010000-010-011-6400-6449-10-00".Equals(item.XauNoiMa))
                {
                    tlQtChungTuChiTietKeHoach.TongCong = item.TongCong - ChiPhiVaoQuan;
                    tlQtChungTuChiTietKeHoach.DieuChinh = item.DieuChinh - ChiPhiVaoQuan;
                }
                else if ("1010000-010-011-8000-8049-10-00".Equals(item.XauNoiMa))
                {
                    tlQtChungTuChiTietKeHoach.TongCong = item.TongCong - PhuCapRaQuan;
                    tlQtChungTuChiTietKeHoach.DieuChinh = item.DieuChinh - PhuCapRaQuan;
                }
                else
                {
                    tlQtChungTuChiTietKeHoach.DieuChinh = item.DieuChinh - (item.TongCong == null ? 0 : item.TongCong);
                }
                if (!string.IsNullOrEmpty(item.GhiChu))
                {
                    tlQtChungTuChiTietKeHoach.GhiChu = item.GhiChu;
                }
                tlQtChungTuChiTietKeHoach.Id = Guid.NewGuid();
                tlQtChungTuChiTietKeHoach.Thang = null;
                tlQtChungTuChiTietKeHoach.NamLamViec = Model.Nam;
                tlQtChungTuChiTietKeHoach.IdDonVi = Model.MaDonVi;
                tlQtChungTuChiTietKeHoach.TenDonVi = Model.TenDonVi;
                tlQtChungTuChiTietKeHoach.UserCreator = _sessionService.Current.Principal;
                tlQtChungTuChiTietKeHoach.TongNamTruoc = null;
                tlQtChungTuChiTietKeHoach.TongCong = null;
                tlQtChungTuChiTietKeHoach.DateCreated = DateTime.Now;

                lstAddEntity.Add(tlQtChungTuChiTietKeHoach);
            }

            var lstUpdateEntity = new List<TlQtChungTuChiTietKeHoachNq104>();
            foreach (var item in lstUpdate)
            {
                var ctChiTiet = _tlQtChungTuChiTietKeHoachService.Find(item.Id);
                if (ctChiTiet != null)
                {
                    if ("1010000-010-011-6400-6449-10-00".Equals(item.XauNoiMa))
                    {
                        ctChiTiet.TongCong = item.TongCong - ChiPhiVaoQuan;
                        ctChiTiet.DieuChinh = item.DieuChinh - ChiPhiVaoQuan;
                    }
                    else if ("1010000-010-011-8000-8049-10-00".Equals(item.XauNoiMa))
                    {
                        ctChiTiet.TongCong = item.TongCong - PhuCapRaQuan;
                        ctChiTiet.DieuChinh = item.DieuChinh - PhuCapRaQuan;
                    }
                    else
                    {
                        ctChiTiet.DieuChinh = item.DieuChinh - (item.TongCong == null ? 0 : item.TongCong);
                    }
                }
                if (!string.IsNullOrEmpty(item.GhiChu))
                {
                    ctChiTiet.GhiChu = item.GhiChu;
                }
                ctChiTiet.UserModifier = _sessionService.Current.Principal;
                ctChiTiet.DateModified = DateTime.Now;

                lstUpdateEntity.Add(ctChiTiet);
            }

            _tlQtChungTuChiTietKeHoachService.BulkInsert(lstAddEntity);
            _tlQtChungTuChiTietKeHoachService.BulkUpdate(lstUpdateEntity);
            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OnRefresh();
        }

        private void CalculateTongCong()
        {
            var tong = TongQuyetToan;
            SaiSo = tong * PhanTram;
            TongCong = tong + SaiSo;
            OnPropertyChanged(nameof(TongCong));
            OnPropertyChanged(nameof(SaiSo));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            this.LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlQtChungTuChiTietKeHoachNq104Model item = (TlQtChungTuChiTietKeHoachNq104Model)sender;
            if (args.PropertyName == nameof(TlQtChungTuChiTietKeHoachNq104Model.GhiChu) || args.PropertyName == nameof(TlQtChungTuChiTietKeHoachNq104Model.DieuChinh))
            {
                if (args.PropertyName == nameof(TlQtChungTuChiTietKeHoachNq104Model.DieuChinh))
                {
                    if (item.DieuChinh != null && item.TongNamTruoc == null)
                    {
                        item.ChenhLech = item.DieuChinh;
                    }
                    if (item.DieuChinh == null && item.TongNamTruoc != null)
                    {
                        item.ChenhLech = -item.TongNamTruoc;
                    }
                    if (item.DieuChinh != null && item.TongNamTruoc != null)
                    {
                        item.ChenhLech = item.DieuChinh - item.TongNamTruoc;
                    }
                    CalculateDieuChinhData();
                    TongQuyetToan = (decimal)ItemsChungTuChiTiet.FirstOrDefault().DieuChinh;
                    OnPropertyChanged(nameof(TongQuyetToan));
                }
                item.IsModified = true;
            }
            OnPropertyChanged(nameof(ItemsChungTuChiTiet));
        }

        private void CalculateDieuChinhData()
        {
            _itemsChungTuChiTiet.Where(x => (bool)x.BHangCha)
                .Select(x =>
                {
                    x.DieuChinh = 0;
                    x.ChenhLech = 0;
                    return x;
                }).ToList();
            foreach (var item in _itemsChungTuChiTiet.Where(x => x.DieuChinh != 0 || x.ChenhLech != 0))
            {
                CalculateDieuChinhParent(item, item);
            }
        }

        private void ExportLuongNam(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string title = string.Format("{0} - Năm {1}", Model.TenDonVi, Model.Nam);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("TieuDe", title);
                    data.Add("Items", ItemsChungTuChiTiet);
                    data.Add("TongQuyetToan", TongQuyetToan);
                    data.Add("ChiPhiVaoQuan", ChiPhiVaoQuan);
                    data.Add("PhuCapRaQuan", PhuCapRaQuan);
                    data.Add("PhanTram", PhanTram);
                    data.Add("SaiSo", SaiSo);
                    data.Add("TongCong", TongCong);
                    data.Add("NgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString("D2"), DateTime.Now.Month, DateTime.Now.Year));

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_QT_NAM_KEHOACH_NEW);
                    string fileNamePrefix = string.Format("rpt_Bang_Luong_Ke_Hoach_Nam_{0}_{1}", Model.Nam, Model.TenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TlChungTuChiTietKeHoachNq104Query>(templateFileName, data);
                    e.Result = new ExportResult(title, fileNameWithoutExtension, null, xlsFile);
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

        private void CalculateDieuChinhParent(TlQtChungTuChiTietKeHoachNq104Model currentItem, TlQtChungTuChiTietKeHoachNq104Model selfItem)
        {
            var parentItem = _itemsChungTuChiTiet.FirstOrDefault(x => x.MlnsId == currentItem.MlnsIdParent);
            if (parentItem == null) return;
            parentItem.DieuChinh += selfItem.DieuChinh ?? 0;
            parentItem.ChenhLech += selfItem.ChenhLech ?? 0;
            CalculateDieuChinhParent(parentItem, selfItem);
        }

        private bool ChungTuFilter(object obj)
        {
            bool result = true;
            var item = (TlQtChungTuChiTietKeHoachNq104Model)obj;
            if (!string.IsNullOrEmpty(DetailFilter.Lns))
                result = result && item.Lns.ToLower().StartsWith(DetailFilter.Lns.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.Tm.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.Ttm.ToLower().StartsWith(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.Ng.ToLower().StartsWith(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.Tng.ToLower().StartsWith(DetailFilter.TNG.ToLower());
            if (SelectedTypeExport != null && SelectedTypeExport.ValueItem.Equals("2"))
            {
                result &= (item.TongNamTruoc != null && item.TongNamTruoc != 0)
                    || (item.TongCong != null && item.TongCong != 0)
                    || (item.DieuChinh != null && item.DieuChinh != 0);
            }
            item.IsFilter = result;
            return result;
        }
        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private void ExportChiTietCanBoMlns(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int namKeHoach = Model.Nam;
                    int namTruoc = namKeHoach - 2;
                    string maDonVi = Model.MaDonVi;
                    var lstCanBoKh = _iDmCanBoKeHoachService.FindByCondition(x => x.Nam == namKeHoach).Distinct()
                        .ToList();
                    var data = _tlQtChungTuChiTietKeHoachService.GetDataChiTietCanBoKeHoach(namKeHoach, maDonVi).ToList();
                    List<MucLucReportChiTietCanBo> noiDungBienDong = new List<MucLucReportChiTietCanBo>
                    {
                        new MucLucReportChiTietCanBo("I", "6000-6001", "Lương cơ bản"),
                        new MucLucReportChiTietCanBo("II", "6100-6115", "Phụ cấp thâm niên"),
                        new MucLucReportChiTietCanBo("III", "8000-8049-10-00", "Phụ cấp ra quân"),
                        new MucLucReportChiTietCanBo("IV", "6400-6449-10-00", "Phụ cấp quân hàm"),
                        new MucLucReportChiTietCanBo("V", "6400-6401", "Tiền ăn")
                    };
                    // sheet 1
                    List<ReportChiTietCanBoMlnsModel> results = new List<ReportChiTietCanBoMlnsModel>();
                    int count = 1;
                    foreach (var nd in noiDungBienDong)
                    {
                        ReportChiTietCanBoMlnsModel root = new ReportChiTietCanBoMlnsModel();
                        root.Stt = nd.Stt;
                        root.NoiDung = nd.MoTa;
                        root.BHangCha = true;
                        results.Add(root);
                        var lstMucLuc = _iTlPhuCapMlnsService.FindByCondition(x => x.Nam == (namKeHoach - 1) && x.XauNoiMa.Contains(nd.XauNoiMa));
                        int stt = 1;
                        foreach (var ml in lstMucLuc)
                        {
                            string maCbStart = "-1";
                            if (ml.MaCb.Equals("1") || ml.MaCb.Equals("2"))
                            {
                                maCbStart = ml.MaCb;
                            }
                            else if (ml.MaCb.Equals("3"))
                            {
                                maCbStart = "4";
                            }
                            else
                            {
                                maCbStart = "0";
                            }
                            var lstCanBoPhuCap = lstCanBoKh.Where(x => x.MaCb.StartsWith(maCbStart)).ToList();
                            if (lstCanBoPhuCap.Count > 0)
                            {
                                ReportChiTietCanBoMlnsModel child = new ReportChiTietCanBoMlnsModel();
                                child.Stt = nd.Stt + "." + stt++;
                                child.BHangCha = true;
                                child.NoiDung = "Thay đổi " + ml.MoTa;
                                child.TongSoTienBienDong = data.Where(x => x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap)).Sum(x => x.ChenhLech);
                                for (int i = 1; i <= 12; i++)
                                {
                                    string property = "Thang" + i;
                                    PropertyInfo prop = child.GetType().GetProperty(property);
                                    var giaTri = data.Where(x =>
                                        x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap) && x.Thang == i).Sum(x => x.ChenhLech);
                                    prop.SetValue(child, giaTri);
                                }
                                results.Add(child);
                                int sttDetail = 1;
                                List<string> lstMaHieu = new List<string>();
                                foreach (var cb in lstCanBoPhuCap)
                                {
                                    if (!lstMaHieu.Contains(cb.MaHieuCanBo))
                                    {
                                        lstMaHieu.Add(cb.MaHieuCanBo);
                                        ReportChiTietCanBoMlnsModel it = new ReportChiTietCanBoMlnsModel();
                                        it.Stt = Convert.ToString(sttDetail++);
                                        it.NoiDung = cb.TenCanBo;
                                        it.TongSoTienBienDong = data.Where(x =>
                                            x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap) && x.MaHieuCanBo.Equals(cb.MaHieuCanBo)).Sum(x => x.ChenhLech);
                                        for (int i = 1; i <= 12; i++)
                                        {
                                            string property = "Thang" + i;
                                            PropertyInfo prop = it.GetType().GetProperty(property);
                                            var giaTri = data.Where(x =>
                                                x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap) && x.MaHieuCanBo.Equals(cb.MaHieuCanBo) && x.Thang == i).Sum(x => x.ChenhLech);
                                            prop.SetValue(it, giaTri);
                                        }
                                        results.Add(it);
                                    }
                                }
                            }
                        }
                    }

                    // sheet 2
                    List<ReportChiTietCanBoMlnsModel> resultsSheet2 = new List<ReportChiTietCanBoMlnsModel>();
                    count = 1;
                    foreach (var nd in noiDungBienDong)
                    {
                        var lstMucLuc = _iTlPhuCapMlnsService.FindByCondition(x => x.Nam == (namKeHoach - 1) && x.XauNoiMa.Contains(nd.XauNoiMa));
                        var lstCbStart = lstMucLuc.Select(x =>
                        {
                            if (x.MaCb.Equals("1") || x.MaCb.Equals("2"))
                            {
                                return x.MaCb;
                            }
                            else if (x.MaCb.Equals("3"))
                            {
                                return "4";
                            }
                            else
                            {
                                return "0";
                            }
                        }).Distinct().ToList();
                        var lstPhucap = lstMucLuc.Select(x => x.MaPhuCap).Distinct().ToList();
                        ReportChiTietCanBoMlnsModel root = new ReportChiTietCanBoMlnsModel();
                        root.Stt = nd.Stt;
                        root.NoiDung = nd.MoTa;
                        root.BHangCha = true;
                        root.TongSoTienBienDong = data.Where(x => lstCbStart.Any(f => x.MaCapBac.StartsWith(f))
                                                                  && lstPhucap.Contains(x.MaPhuCap))
                            .Sum(x => x.ChenhLech);
                        for (int i = 1; i <= 12; i++)
                        {
                            //So nguoi
                            string propertyTotal = "Thang" + i;
                            PropertyInfo propTotal = root.GetType().GetProperty(propertyTotal);
                            var giaTriTotal = data.Where(x => lstCbStart.Any(f => x.MaCapBac.StartsWith(f))
                                 && lstPhucap.Contains(x.MaPhuCap) && x.Thang == i).Sum(x => x.ChenhLech);
                            propTotal.SetValue(root, giaTriTotal);

                            //So tien
                            propertyTotal = "SoNguoiThang" + i;
                            propTotal = root.GetType().GetProperty(propertyTotal);
                            giaTriTotal = lstCanBoKh.Count(x => lstCbStart.Any(f => x.MaCb.StartsWith(f)) && x.Thang == i);
                            propTotal.SetValue(root, giaTriTotal);
                        }
                        resultsSheet2.Add(root);
                        int stt = 1;
                        foreach (var ml in lstMucLuc)
                        {
                            string maCbStart = "-1";
                            if (ml.MaCb.Equals("1") || ml.MaCb.Equals("2"))
                            {
                                maCbStart = ml.MaCb;
                            }
                            else if (ml.MaCb.Equals("3"))
                            {
                                maCbStart = "4";
                            }
                            else
                            {
                                maCbStart = "0";
                            }
                            var lstCanBoPhuCap = lstCanBoKh.Where(x => x.MaCb.StartsWith(maCbStart)).ToList();
                            if (lstCanBoPhuCap.Count > 0)
                            {
                                ReportChiTietCanBoMlnsModel child = new ReportChiTietCanBoMlnsModel();
                                child.Stt = nd.Stt + "." + stt++;
                                child.BHangCha = false;
                                child.NoiDung = "Thay đổi " + ml.MoTa;
                                child.TongSoTienBienDong = data.Where(x =>
                                    x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap)).Sum(x => x.ChenhLech);
                                for (int i = 1; i <= 12; i++)
                                {
                                    //So nguoi
                                    string property = "Thang" + i;
                                    PropertyInfo prop = child.GetType().GetProperty(property);
                                    var giaTri = data.Where(x =>
                                        x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap) && x.Thang == i).Sum(x => x.ChenhLech);
                                    prop.SetValue(child, giaTri);

                                    //So tien
                                    property = "SoNguoiThang" + i;
                                    prop = child.GetType().GetProperty(property);
                                    giaTri = lstCanBoKh.Count(x => x.MaCb.StartsWith(maCbStart) && x.Thang == i);
                                    prop.SetValue(child, giaTri);
                                }
                                resultsSheet2.Add(child);
                            }
                        }
                    }

                    //sheet 3
                    List<ReportChiTietCanBoMlnsModel> resultsSheet3 = new List<ReportChiTietCanBoMlnsModel>();
                    count = 1;
                    foreach (var nd in noiDungBienDong)
                    {
                        var lstMucLuc = _iTlPhuCapMlnsService.FindByCondition(x => x.Nam == (namKeHoach - 1) && x.XauNoiMa.Contains(nd.XauNoiMa));
                        var lstCbStart = lstMucLuc.Select(x =>
                        {
                            if (x.MaCb.Equals("1") || x.MaCb.Equals("2"))
                            {
                                return x.MaCb;
                            }
                            else if (x.MaCb.Equals("3"))
                            {
                                return "4";
                            }
                            else
                            {
                                return "0";
                            }
                        }).Distinct().ToList();
                        var lstPhucap = lstMucLuc.Select(x => x.MaPhuCap).Distinct().ToList();
                        ReportChiTietCanBoMlnsModel root = new ReportChiTietCanBoMlnsModel();
                        root.Stt = nd.Stt;
                        root.NoiDung = nd.MoTa;
                        root.BHangCha = true;
                        resultsSheet3.Add(root);
                        root = new ReportChiTietCanBoMlnsModel();
                        root.NoiDung = "Số người";
                        for (int i = 1; i <= 12; i++)
                        {
                            //So nguoi 
                            string propertyTotal = "Thang" + i;
                            PropertyInfo propTotal = root.GetType().GetProperty(propertyTotal);
                            var giaTriTotal = lstCanBoKh.Count(x => lstCbStart.Any(f => x.MaCb.StartsWith(f)) && x.Thang == i);
                            propTotal.SetValue(root, (decimal)giaTriTotal);
                        }
                        resultsSheet3.Add(root);
                        root = new ReportChiTietCanBoMlnsModel();
                        root.NoiDung = "Số tiền";
                        root.IsSum = true;
                        root.TongSoTienBienDong = data.Where(x => lstCbStart.Any(f => x.MaCapBac.StartsWith(f))
                                                                  && lstPhucap.Contains(x.MaPhuCap))
                            .Sum(x => x.ChenhLech);
                        for (int i = 1; i <= 12; i++)
                        {
                            //So tien
                            string propertyTotal = "Thang" + i;
                            PropertyInfo propTotal = root.GetType().GetProperty(propertyTotal);
                            var giaTriTotal = data.Where(x => lstCbStart.Any(f => x.MaCapBac.StartsWith(f))
                                                              && lstPhucap.Contains(x.MaPhuCap) && x.Thang == i).Sum(x => x.ChenhLech);
                            propTotal.SetValue(root, giaTriTotal);
                        }
                        resultsSheet3.Add(root);
                        int stt = 1;
                        foreach (var ml in lstMucLuc)
                        {
                            string maCbStart = "-1";
                            if (ml.MaCb.Equals("1") || ml.MaCb.Equals("2"))
                            {
                                maCbStart = ml.MaCb;
                            }
                            else if (ml.MaCb.Equals("3"))
                            {
                                maCbStart = "4";
                            }
                            else
                            {
                                maCbStart = "0";
                            }
                            var lstCanBoPhuCap = lstCanBoKh.Where(x => x.MaCb.StartsWith(maCbStart)).ToList();
                            if (lstCanBoPhuCap.Count > 0)
                            {
                                ReportChiTietCanBoMlnsModel child = new ReportChiTietCanBoMlnsModel();
                                child.Stt = nd.Stt + "." + stt++;
                                child.BHangCha = true;
                                child.NoiDung = "Thay đổi " + ml.MoTa;
                                resultsSheet3.Add(child);
                                child = new ReportChiTietCanBoMlnsModel();
                                child.NoiDung = "Số người";
                                for (int i = 1; i <= 12; i++)
                                {
                                    //So nguoi
                                    string property = "Thang" + i;
                                    PropertyInfo prop = child.GetType().GetProperty(property);
                                    var giaTri = lstCanBoKh.Count(x => x.MaCb.StartsWith(maCbStart) && x.Thang == i);
                                    prop.SetValue(child, (decimal)giaTri);
                                }
                                resultsSheet3.Add(child);
                                child = new ReportChiTietCanBoMlnsModel();
                                child.NoiDung = "Số tiền";
                                child.TongSoTienBienDong = data.Where(x =>
                                    x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap)).Sum(x => x.ChenhLech);
                                for (int i = 1; i <= 12; i++)
                                {
                                    //So tien
                                    string property = "Thang" + i;
                                    PropertyInfo prop = child.GetType().GetProperty(property);
                                    var giaTri = data.Where(x =>
                                        x.MaCapBac.StartsWith(maCbStart) && x.MaPhuCap.Equals(ml.MaPhuCap) && x.Thang == i).Sum(x => x.ChenhLech);
                                    prop.SetValue(child, giaTri);
                                }
                                resultsSheet3.Add(child);
                            }
                        }
                    }

                    string title = string.Format("Năm {0}", Model.Nam);
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionInfo.YearOfWork).ToUpper());
                    dic.Add("Cap2", GetHeader2Report());

                    dic.Add("FormatNumber", formatNumber);
                    dic.Add("TieuDe", title);
                    dic.Add("TenDonVi", Model.TenDonVi.ToUpper());

                    var donVi = _tlDmDonViService.FindByMaDonVi(Model.MaDonVi);

                    if (!string.IsNullOrEmpty(donVi.ParentId))
                    {
                        var donViCha = _tlDmDonViService.FindAll().FirstOrDefault(x => x.MaDonVi.Equals(donVi.ParentId));
                        if (donViCha != null)
                        {
                            dic.Add("DonViCha", donViCha.TenDonVi.ToUpper());
                        }
                        else
                        {
                            dic.Add("DonViCha", string.Empty);
                        }
                    }
                    else
                    {
                        dic.Add("DonViCha", string.Empty);
                    }

                    dic.Add("Items", results);
                    dic.Add("Items2", resultsSheet2);
                    dic.Add("Items3", resultsSheet3);
                    dic.Add("NgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.Day.ToString("D2"), DateTime.Now.Month, DateTime.Now.Year));

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG_NEW, ExportFileName.RPT_TL_CHITIET_CANBO_KEHOACH_NEW);
                    string fileNamePrefix = string.Format("rpt_ChiTiet_CanBo_Ke_Hoach_Nam_{0}_{1}", Model.Nam, Model.TenDonVi);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<ReportChiTietCanBoMlnsModel>(templateFileName, dic);
                    if (exportType == ExportType.PDF)
                    {
                        ExcelFile[] xlsFiles = new ExcelFile[1];
                        xlsFiles[0] = xlsFile;
                        _exportService.ExportPdf(xlsFiles, fileNameWithoutExtension);
                    }
                    e.Result = new ExportResult(title, fileNameWithoutExtension, fileNameWithoutExtension, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            if (exportType == ExportType.PDF)
                            {
                                // Show PDF
                                _exportService.ShowPdf(_mapper.Map<PdfFileModel>(result));
                            }
                            else
                            {
                                _exportService.Open(result, exportType);
                            }
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
}