using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.Core;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.TKTCVaTongDuToan
{
    public class TKTCVaTongDuToanDialogViewModel : DialogAttachmentViewModelBase<VdtDuToanModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly IExportService _exportService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;


        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_TKTC_VA_TONG_DU_TOAN_DIALOG;
        public override string Name => "THÊM MỚI THÔNG TIN TKTC VÀ TỔNG DỰ TOÁN";
        public override string Title { get; set; }
        public override string Description { get; set; }
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.TKTCVaTongDuToan.VdtDaDuToanDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_TKTC_TONGDUTOAN;
        public bool IsEdit => !IsAdd;
        public bool IsEditable => (Model.BActive == true || IsAdd) && IsNotViewDetail;
        //public double? TongGiaTriPheDuyetDuAn = 0;
        private bool _isImport;
        public bool IsImport
        {
            get => _isImport;
            set => SetProperty(ref _isImport, value);
        }

        private double? _fTongNguonVon;
        public double? FTongNguonVon
        {
            get => _fTongNguonVon;
            set => SetProperty(ref _fTongNguonVon, value);
        }

        private double? _tongGiaTriPheDuyetDuAn;
        public double? TongGiaTriPheDuyetDuAn
        {
            get => _tongGiaTriPheDuyetDuAn;
            set => SetProperty(ref _tongGiaTriPheDuyetDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _dataDonVi;
        public ObservableCollection<ComboboxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDuAnByChuDauTu();
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
                if (SetProperty(ref _selectedDuAn, value))
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        GetDuToanCungDuAn();
                    }

                    GetDuAnChiTiet();
                }
            }
        }

        private List<ComboboxItem> _dataLoaiQD;
        public List<ComboboxItem> DataLoaiQD
        {
            get => _dataLoaiQD;
            set => SetProperty(ref _dataLoaiQD, value);
        }

        private ComboboxItem _selectedLoaiQD;
        public ComboboxItem SelectedLoaiQD
        {
            get => _selectedLoaiQD;
            set
            {
                SetProperty(ref _selectedLoaiQD, value);
                OnPropertyChanged(nameof(IsDuToan));
                LoadDuAnByChuDauTu();
            }
        }

        public bool IsDuToan => (_selectedLoaiQD != null && "False".Equals(_selectedLoaiQD.ValueItem)) ? true : false;

        private ObservableCollection<VdtDaDuToanNguonVonModel> _dataDuToanNguonVon;
        public ObservableCollection<VdtDaDuToanNguonVonModel> DataDuToanNguonVon
        {
            get => _dataDuToanNguonVon;
            set => SetProperty(ref _dataDuToanNguonVon, value);
        }

        private ObservableCollection<VdtDaDuToanNguonVonModel> _dataDuToanNguonVonPheDuyet;
        public ObservableCollection<VdtDaDuToanNguonVonModel> DataDuToanNguonVonPheDuyet
        {
            get => _dataDuToanNguonVonPheDuyet;
            set => SetProperty(ref _dataDuToanNguonVonPheDuyet, value);
        }

        private VdtDaDuToanNguonVonModel _selectedNguonVon;
        public VdtDaDuToanNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<VdtDaDuToanChiPhiModel> _dataDuToanChiPhi;
        public ObservableCollection<VdtDaDuToanChiPhiModel> DataDuToanChiPhi
        {
            get => _dataDuToanChiPhi;
            set
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _dataDuToanChiPhi, value);
                OnPropertyChanged(nameof(DataDuToanChiPhi));
            }

        }

        private ObservableCollection<VdtDaDuToanChiPhiModel> _dataDuToanChiPhiPheDuyet;
        public ObservableCollection<VdtDaDuToanChiPhiModel> DataDuToanChiPhiPheDuyet
        {
            get => _dataDuToanChiPhiPheDuyet;
            set => SetProperty(ref _dataDuToanChiPhiPheDuyet, value);
        }

        private VdtDaDuToanChiPhiModel _selectedChiPhi;
        public VdtDaDuToanChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private List<ComboboxItem> _dataChiPhi;
        public List<ComboboxItem> DataChiPhi
        {
            get => _dataChiPhi;
            set => SetProperty(ref _dataChiPhi, value);
        }

        private List<ComboboxItem> _dataNguonVon;
        public List<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataQDDauTuHangMuc;
        public List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataQDDauTuHangMuc
        {
            get => _dataQDDauTuHangMuc;
            set => SetProperty(ref _dataQDDauTuHangMuc, value);
        }

        private List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataDuToanHangMuc;
        public List<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataDuToanHangMuc
        {
            get => _dataDuToanHangMuc;
            set => SetProperty(ref _dataDuToanHangMuc, value);
        }

        private ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataDuToanHangMucByChiPhi;
        public ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataDuToanHangMucByChiPhi
        {
            get => _dataDuToanHangMucByChiPhi;
            set => SetProperty(ref _dataDuToanHangMucByChiPhi, value);
        }

        private List<DuToanDetailModel> _hangMucPhanChiaSaved;
        public List<DuToanDetailModel> HangMucPhanChiaSaved
        {
            get => _hangMucPhanChiaSaved;
            set => SetProperty(ref _hangMucPhanChiaSaved, value);
        }

        private double? _conLai;
        public double? ConLai
        {
            get => _conLai;
            set => SetProperty(ref _conLai, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public bool IsDieuChinhTKTC { get; set; }

        private double? _tongDuToanByDuAn;
        public double? TongDuToanByDuAn
        {
            get => _tongDuToanByDuAn;
            set => SetProperty(ref _tongDuToanByDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _itemsChuDauTu;
        public ObservableCollection<ComboboxItem> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                if (SetProperty(ref _selectedChuDauTu, value))
                {
                    LoadDuAnByChuDauTu();
                }
            }
        }

        public TKTCVaTongDuToanDetailViewModel TKTCVaTongDuToanDetailViewModel { get; }
        public ImportTKTCVaTongDuToanViewModel ImportTKTCVaTongDuToanViewModel { get; }
        public RelayCommand AddChiPhiDetailCommand { get; }
        public RelayCommand AddChiPhiChildDetailCommand { get; }
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }
        public RelayCommand DeleteChiPhiCommand { get; }
        public RelayCommand DeleteNguonVonCommand { get; }

        public RelayCommand ExportCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        public TKTCVaTongDuToanDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IApproveProjectService approveProjectService,
            IProjectManagerService projectManagerService,
            IVdtDaDuToanService vdtDaDuToanService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IDmChuDauTuService chuDauTuService,
            IExportService exportService,
            IVdtDmChiPhiService vdtDmChiPhiService,
            TKTCVaTongDuToanDetailViewModel tKTCVaTongDuToanDetailViewModel,
            ImportTKTCVaTongDuToanViewModel importTKTCVaTongDuToanViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _chuDauTuService = chuDauTuService;
            _exportService = exportService;
            _vdtDmChiPhiService = vdtDmChiPhiService;

            TKTCVaTongDuToanDetailViewModel = tKTCVaTongDuToanDetailViewModel;
            ImportTKTCVaTongDuToanViewModel = importTKTCVaTongDuToanViewModel;

            AddChiPhiDetailCommand = new RelayCommand(obj => OnAddChiPhi());
            AddChiPhiChildDetailCommand = new RelayCommand(obj => OnAddChiPhiChild());
            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddNguonVon());
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowDetailDuToan());
            DeleteNguonVonCommand = new RelayCommand(obj => OnDeleteNguonVon());
            DeleteChiPhiCommand = new RelayCommand(obj => OnDeleteChiPhi());
            ExportCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        public override void Init()
        {
            HangMucPhanChiaSaved = new List<DuToanDetailModel>();
            LoadAttach();
            LoadChiPhi();
            LoadNguonVon();
            LoadDonVi();
            LoadDataLoaiQD();
            LoadChuDauTu();
            LoadDuAnByChuDauTu();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            DataDuToanChiPhi = new ObservableCollection<VdtDaDuToanChiPhiModel>();
            DataDuToanNguonVon = new ObservableCollection<VdtDaDuToanNguonVonModel>();
            DataDuToanHangMuc = new List<DuToanDetailModel>();
            IsImport = false;
            OnPropertyChanged(nameof(IsImport));
            if (Model.Id.IsNullOrEmpty() || IsDieuChinhTKTC)
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    Title = "THÊM MỚI";
                    Description = "Thêm mới thông tin thiết kế thi công và tổng dự toán";
                    IsAdd = true;
                    Model = new Model.VdtDuToanModel();
                    Model.IsAdd = true;
                    DataDuAn = new ObservableCollection<ComboboxItem>();
                    Model.DNgayQuyetDinh = DateTime.Now;
                    TongGiaTriPheDuyetDuAn = 0;
                    OnPropertyChanged(nameof(TongGiaTriPheDuyetDuAn));
                    IsImport = true;
                    OnPropertyChanged(nameof(IsImport));

                }
                else
                {
                    Title = "ĐIỀU CHỈNH";
                    Description = "Điều chỉnh thông tin thiết kế thi công và tổng dự toán";
                    IsAdd = true;
                    Model.IsAdd = true;
                    Model.DNgayQuyetDinh = DateTime.Now;
                    Model.SSoQuyetDinh = null;

                    SelectedDonVi = DataDonVi.FirstOrDefault(x => x.ValueItem == Model.IIdMaDonViQuanLy);
                    List<ProjectManagerQuery> listDuan = _projectManagerService.FindByCondition().ToList();
                    DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuan);
                    if (DataDuAn != null && DataDuAn.Count > 0 && Model.IIdDuAnId.HasValue)
                    {
                        SelectedDuAn = DataDuAn.FirstOrDefault(x => x.ValueItem == Model.IIdDuAnId.ToString());
                        var idChuDauTu = listDuan.FirstOrDefault(x => x.Id.ToString() == Model.IIdDuAnId.ToString())?.IIdChuDauTuId;
                        if (idChuDauTu != null) SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.ValueItem == idChuDauTu.ToString());
                    }
                    SelectedLoaiQD = DataLoaiQD.FirstOrDefault(x => x.ValueItem == Model.BLaTongDuToan.ToString());
                    Model.Id = Guid.Empty;
                }
            }
            else if (!Model.Id.IsNullOrEmpty())
            {
                Title = "CẬP NHẬT";
                Description = "Cập nhật thông tin thiết kế thi công và tổng dự toán";
                IsAdd = false;
                SelectedDonVi = DataDonVi.FirstOrDefault(x => x.ValueItem == Model.IIdMaDonViQuanLy);
                List<ProjectManagerQuery> listDuan = _projectManagerService.FindByCondition().ToList();
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuan);
                if (DataDuAn != null && DataDuAn.Count > 0 && Model.IIdDuAnId.HasValue)
                {
                    SelectedDuAn = DataDuAn.FirstOrDefault(x => x.ValueItem == Model.IIdDuAnId.ToString());
                    OnPropertyChanged(nameof(SelectedDuAn));

                    var idChuDauTu = listDuan.FirstOrDefault(x => x.Id.ToString() == Model.IIdDuAnId.ToString())?.IIdChuDauTuId;
                    if (idChuDauTu != null) SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.ValueItem == idChuDauTu.ToString());
                }
                SelectedLoaiQD = DataLoaiQD.FirstOrDefault(x => x.ValueItem == Model.BLaTongDuToan.ToString());

                LoadDataChiPhiByDuToanId(Model.Id);
                LoadDataNguonVonByQDDauTuId(Model.Id);
                LoadDataDuToanHangMuc(Model.Id);
                UpdateListChiPhiCanEdit();
            }
            if (!IsNotViewDetail)
            {
                Title = "CHI TIẾT";
                Description = "Xem chi tiết thông tin thiết kế thi công và tổng dự toán";
                IsImport = false;
            }
        }

        private void GetDuToanCungDuAn()
        {
            if (SelectedDuAn == null)
            {
                return;
            }
            var listDuToan = _vdtDaDuToanService.FindListByDuAnId(SelectedDuAn.Id);
            var qdDauTuByDuAn = _approveProjectService.FindByDuAnId(SelectedDuAn.Id);

            if (listDuToan != null && listDuToan.Count > 0)
            {
                TongDuToanByDuAn = listDuToan.Sum(x => x.FTongDuToanPheDuyet);
            }
            else
            {
                TongDuToanByDuAn = 0;
            }

            if (!IsDieuChinh && TongDuToanByDuAn > (qdDauTuByDuAn?.FTongMucDauTuPheDuyet ?? 0))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgDuToanLonHonTongMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetDataWhenChangeDuAn();
            }
        }

        private void LoadDonVi()
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT);
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);

            var strUnitManager = _sessionService.Current.IdsDonViQuanLy;

            if (!string.IsNullOrEmpty(strUnitManager))
            {
                List<string> listUnitManager = strUnitManager.Split(StringUtils.COMMA).ToList();
                _dataDonVi = new ObservableCollection<ComboboxItem>(_dataDonVi.Where(x => listUnitManager.Any(y => y.Contains(x.ValueItem))).ToList());
            }
            else
            {
                _dataDonVi = new ObservableCollection<ComboboxItem>();
            }
        }

        private void LoadDuAnByDonVi()
        {
            if (SelectedDonVi != null && SelectedLoaiQD != null && !string.IsNullOrEmpty(SelectedLoaiQD.ValueItem))
            {
                IEnumerable<VdtDaDuAn> listDuAnByDonVi = _vdtDaDuToanService.FindDuAnByDonViAndLoaiQD(SelectedDonVi.ValueItem, SelectedLoaiQD.ValueItem);
                _dataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnByDonVi);
                OnPropertyChanged(nameof(DataDuAn));
                if (Model != null && Model.IIdDuAnId.HasValue)
                {
                    _selectedDuAn = DataDuAn.FirstOrDefault(n => n.ValueItem.ToLower() == Model.IIdDuAnId.Value.ToString().ToLower());
                    OnPropertyChanged(nameof(SelectedDuAn));
                }
            }
        }

        private void LoadDataLoaiQD()
        {
            List<ComboboxItem> listLoaiQD = new List<ComboboxItem>();
            listLoaiQD.Add(new ComboboxItem { ValueItem = "True", DisplayItem = "Là tổng dự toán" });
            listLoaiQD.Add(new ComboboxItem { ValueItem = "False", DisplayItem = "Theo giai đoạn" });
            DataLoaiQD = listLoaiQD;
        }

        private void LoadChiPhi()
        {
            IEnumerable<VdtDmChiPhi> listChiPhi = _approveProjectService.GetAllDmChiPhi();
            DataChiPhi = _mapper.Map<List<ComboboxItem>>(listChiPhi);
        }

        private void LoadNguonVon()
        {
            IEnumerable<Core.Domain.NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
            DataNguonVon = _mapper.Map<List<ComboboxItem>>(listNguonVon);
        }

        private void GetDuAnChiTiet()
        {
            if (SelectedDuAn != null && SelectedDuAn.ValueItem != null)
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(Guid.Parse(SelectedDuAn.ValueItem));
                if (duAn != null)
                {
                    Model.IIdDuAnId = duAn.Id;
                    Model.DiaDiem = duAn.SDiaDiem;
                    Model.FTongDuToanPheDuyet = duAn.FTongMucDauTu;
                    Model.ThoiGianThucHien = duAn.SKhoiCong + "-" + duAn.SKetThuc;
                    var objQdDauTu = _approveProjectService.FindListQDDauTuByDuAnId(duAn.Id).FirstOrDefault(n => (n.BActive ?? false));
                    if (objQdDauTu != null)
                        TongGiaTriPheDuyetDuAn = objQdDauTu.FTongMucDauTuPheDuyet;
                    else
                        TongGiaTriPheDuyetDuAn = 0;
                    LoadDataNguonVonByDuAn(duAn.Id);
                    Dictionary<Guid, Guid> dicChiPhiConvert = new Dictionary<Guid, Guid>();
                    LoadDataChiPhiByDuAn(duAn.Id, ref dicChiPhiConvert);
                    LoadDataQDDauTuHangMuc(duAn.Id, dicChiPhiConvert);
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        private void LoadDataNguonVonByDuAn(Guid duAnId)
        {
            if (Model.Id.IsNullOrEmpty() || IsDieuChinhTKTC)
            {
                List<VdtDaDuToanNguonVonQuery> listDuToanNguonVonQuery = _vdtDaDuToanService.FindListDuToanNguonVonByDuAn(duAnId).ToList();
                DataDuToanNguonVon = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listDuToanNguonVonQuery);
                DataDuToanNguonVonPheDuyet = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listDuToanNguonVonQuery);
                foreach (var item in DataDuToanNguonVon)
                {
                    item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
                }
            }
        }

        private void LoadDataNguonVonByQDDauTuId(Guid duToanId)
        {
            List<VdtDaDuToanNguonVonQuery> listDuToanNguonVonQuery = _vdtDaDuToanService.FindListDuToanNguonVonByDuToanId(duToanId).ToList();
            DataDuToanNguonVon = _mapper.Map<ObservableCollection<VdtDaDuToanNguonVonModel>>(listDuToanNguonVonQuery);
            CalculateConLaiNguonVon();
            foreach (var item in DataDuToanNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataChiPhiByDuAn(Guid duAnId, ref Dictionary<Guid, Guid> dicChiPhiConvert)
        {
            List<VdtDaDuToanChiPhiQuery> listDuToanChiPhiQuery = _vdtDaDuToanService.FindListDuToanChiPhiByDuAn(duAnId).ToList();
            if (listDuToanChiPhiQuery != null && listDuToanChiPhiQuery.Count != 0)
            {
                foreach (var item in listDuToanChiPhiQuery)
                {
                    if (!dicChiPhiConvert.ContainsKey(item.IdChiPhiDuAn.Value))
                        dicChiPhiConvert.Add(item.IdChiPhiDuAn.Value, Guid.NewGuid());
                    item.IdChiPhiDuAn = dicChiPhiConvert[item.IdChiPhiDuAn.Value];
                    if (item.IdChiPhiDuAnParent.HasValue)
                    {
                        if (!dicChiPhiConvert.ContainsKey(item.IdChiPhiDuAnParent.Value))
                            dicChiPhiConvert.Add(item.IdChiPhiDuAnParent.Value, Guid.NewGuid());
                        item.IdChiPhiDuAnParent = dicChiPhiConvert[item.IdChiPhiDuAnParent.Value];
                    }
                    item.IsDuAnChiPhiOld = false;
                }
            }
            DataDuToanChiPhi = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listDuToanChiPhiQuery);
            DataDuToanChiPhiPheDuyet = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listDuToanChiPhiQuery);
            IsImport = true;
            CalculateDataChiPhi();
            CalculateConLaiNguonVon();

            foreach (var item in DataDuToanChiPhi)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
            UpdateListChiPhiCanEdit();

        }

        public void LoadDataQDDauTuHangMuc(Guid duAnId, Dictionary<Guid, Guid> dicChiPhiConvert)
        {
            List<DuToanDetailQuery> listData = new List<DuToanDetailQuery>();

            VdtDaQddauTu qddDauTu = _approveProjectService.FindByDuAnId(duAnId);

            if (qddDauTu == null)
            {
                return;
            }

            listData = _vdtDaDuToanService.ListHangMucByQDDauTu(qddDauTu.Id).ToList();
            if (listData != null && listData.Count != 0)
            {
                foreach (var item in listData)
                {
                    if (!dicChiPhiConvert.ContainsKey(item.IdDuAnChiPhi.Value)) continue;
                    item.IdDuAnChiPhi = dicChiPhiConvert[item.IdDuAnChiPhi.Value];
                }
            }

            DataQDDauTuHangMuc = _mapper.Map<List<Model.DuToanDetailModel>>(listData);
            DataDuToanHangMuc = _mapper.Map<List<Model.DuToanDetailModel>>(listData);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        public void LoadDataDuToanHangMuc(Guid duToanId)
        {
            List<DuToanDetailQuery> listData = _vdtDaDuToanService.ListHangMucByDuToan(duToanId).ToList();
            DataDuToanHangMuc = _mapper.Map<List<Model.DuToanDetailModel>>(listData);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        private void LoadDataChiPhiByDuToanId(Guid duToanId)
        {
            List<VdtDaDuToanChiPhiQuery> listDuToanChiPhiQuery = _vdtDaDuToanService.FindListDuToanChiPhiByDuToanId(duToanId).ToList();
            listDuToanChiPhiQuery = OrderListChiPhi(listDuToanChiPhiQuery);
            DataDuToanChiPhi = _mapper.Map<ObservableCollection<VdtDaDuToanChiPhiModel>>(listDuToanChiPhiQuery);
            CalculateDataChiPhi();
            CalculateConLaiNguonVon();

            foreach (var item in DataDuToanChiPhi)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private List<VdtDaDuToanChiPhiQuery> OrderListChiPhi(List<VdtDaDuToanChiPhiQuery> list)
        {
            List<VdtDaDuToanChiPhiQuery> listResult = new List<VdtDaDuToanChiPhiQuery>();
            var listParent = list.Where(x => x.IdChiPhiDuAnParent == null).ToList();
            if (listParent.Count > 0)
            {
                foreach (var item in listParent)
                {
                    listResult.Add(item);
                    List<VdtDaDuToanChiPhiQuery> listChild = FindListQueryChild(list, item.Id.Value);
                    listResult.AddRange(listChild);
                }
            }
            return listResult;
        }

        public List<VdtDaDuToanChiPhiQuery> FindListQueryChild(List<VdtDaDuToanChiPhiQuery> items, Guid parentId)
        {
            List<VdtDaDuToanChiPhiQuery> inner = new List<VdtDaDuToanChiPhiQuery>();
            foreach (var t in items.Where(item => item.IdChiPhiDuAnParent == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListQueryChild(items, t.Id.Value)).ToList();
            }
            return inner;
        }

        public List<VdtDaDuToanChiPhiModel> FindListChildChiPhi(Guid parentId)
        {
            List<VdtDaDuToanChiPhiModel> inner = new List<VdtDaDuToanChiPhiModel>();
            foreach (var t in DataDuToanChiPhi.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildChiPhi(t.Id)).ToList();
            }
            return inner;
        }

        public List<VdtDaDuToanChiPhiModel> FindListChiPhiParent(Guid chiPhiId)
        {
            List<VdtDaDuToanChiPhiModel> inner = new List<VdtDaDuToanChiPhiModel>();
            foreach (var t in DataDuToanChiPhi.Where(item => item.IdChiPhiDuAn == chiPhiId && !item.IsDeleted))
            {
                inner.Add(t);
                if (t.IdChiPhiDuAnParent != null)
                {
                    inner = inner.Union(FindListChiPhiParent(t.IdChiPhiDuAnParent.Value)).ToList();
                }
            }
            return inner;
        }

        private void UpdateListChiPhiCanEdit()
        {
            foreach (var item in DataDuToanChiPhi.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                var listChild = FindListChildChiPhi(item.IdChiPhiDuAn.Value);
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        private void UpdateListChiPhiCanEditGiaTriChiPhi()
        {
            foreach (var item in DataDuToanChiPhi.Where(x => x.IsEditHangMuc && !x.IsDeleted))
            {
                if (CheckChiPhiCanDelete(item.IdChiPhiDuAn.Value))
                {
                    item.IsEditGiaTriChiPhi = true;
                }
                else
                {
                    item.IsEditGiaTriChiPhi = false;
                }
            }
        }

        public override void OnSave(object obj)
        {
            if (SelectedDonVi != null)
            {
                Model.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = Guid.Parse(SelectedDuAn.ValueItem);
            }
            if (SelectedLoaiQD != null)
            {
                Model.BLaTongDuToan = Convert.ToBoolean(SelectedLoaiQD.ValueItem);
            }

            if (!ValiDateData() || !ValidateDataDetail())
            {
                return;
            }
            VdtDaDuToan entity;
            try
            {
                if (Model.Id != Guid.Empty)
                {
                    // Update
                    entity = _vdtDaDuToanService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.FTongDuToanPheDuyet = FTongNguonVon;
                    entity.BActive = true;
                    entity.BIsGoc = true;
                    entity.DDateUpdate = DateTime.Now;
                    entity.IIdDuToanGocId = entity.Id;
                    entity.SUserUpdate = _sessionService.Current.Principal;
                    _vdtDaDuToanService.Update(entity);
                }
                else
                {
                    // Add VdtDaDuToan
                    entity = _mapper.Map<VdtDaDuToan>(Model);
                    entity.FTongDuToanPheDuyet = FTongNguonVon;
                    entity.BActive = true;
                    entity.BIsGoc = true;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    Model.DDateCreate = entity.DDateCreate;
                    Model.SUserCreate = entity.SUserCreate;

                    //tìm QDDT bactive = 1
                    VdtDaQddauTu qdDauTu = _approveProjectService.FindByDuAnId(Model.IIdDuAnId.Value);
                    if (qdDauTu != null)
                    {
                        entity.IIdQddauTuId = qdDauTu.Id;
                    }

                    // nếu là tạo mới từ điều chỉnh thì phải thêm DutoanparentId, update dự toán parent bactive = 0
                    if (IsDieuChinhTKTC)
                    {
                        VdtDaDuToan duToanParent = _vdtDaDuToanService.FindByDuAnId(SelectedDuAn.Id);
                        if (duToanParent != null)
                        {
                            entity.BActive = true;
                            entity.BIsGoc = false;
                            entity.IIdParentId = duToanParent.Id;
                            entity.IIdDuToanGocId = duToanParent.IIdDuToanGocId;

                            // update dự toán cha bactive = 0;
                            duToanParent.BActive = false;
                            _vdtDaDuToanService.Update(duToanParent);
                        }
                    }

                    _vdtDaDuToanService.Add(entity);
                    Model.BActive = entity.BActive;

                    //update DutoanGocId
                    entity.IIdDuToanGocId = entity.Id;
                    _vdtDaDuToanService.Update(entity);
                }
                Model.Id = entity.Id;
                // Lưu chi tiết nguồn vốn và chi phí
                SaveDetail(entity.Id);

                // Save attach file
                SaveAttachment(entity.Id);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.VdtDuToanModel>(entity));
            IsDieuChinhTKTC = false;
            LoadData();
            ((Window)obj).Close();
        }

        protected void OnAddChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IsLoaiChiPhi || SelectedChiPhi.IsDeleted)
            {
                return;
            }
            VdtDaDuToanChiPhiModel targetItem = new VdtDaDuToanChiPhiModel()
            {
                Id = Guid.Empty,
                IdDuToanChiPhi = Guid.Empty,
                IdChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                currentRow = 0;
                VdtDaDuToanChiPhiModel sourceItem = new VdtDaDuToanChiPhiModel();
                if (SelectedChiPhi != null)
                {
                    var listChiPhiChild = FindListChildChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                    if (listChiPhiChild == null || listChiPhiChild.Count == 0)
                    {
                        currentRow = DataDuToanChiPhi.IndexOf(SelectedChiPhi);
                    }
                    else
                    {
                        var indexOfLastChild = DataDuToanChiPhi.FirstOrDefault(x => x.IdChiPhiDuAn == listChiPhiChild.Last().IdChiPhiDuAn);
                        currentRow = DataDuToanChiPhi.IndexOf(indexOfLastChild);
                    }

                    sourceItem = DataDuToanChiPhi.FirstOrDefault(x => x.IdChiPhiDuAn == SelectedChiPhi.IdChiPhiDuAn);
                }

                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.IdDuToanChiPhi = Guid.Empty;
                targetItem.TenChiPhi = string.Empty;
                targetItem.IdChiPhi = sourceItem.IdChiPhi;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IsLoaiChiPhi = false;
                targetItem.IsDuAnChiPhiOld = false;
                targetItem.IsEditHangMuc = true;
            }
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            DataDuToanChiPhi.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        protected void OnAddChiPhiChild()
        {
            if (SelectedChiPhi == null || SelectedDuAn == null || SelectedChiPhi.IsDeleted)
            {
                return;
            }
            VdtDaDuToanChiPhiModel targetItem = new VdtDaDuToanChiPhiModel()
            {
                Id = Guid.Empty,
                IdDuToanChiPhi = Guid.Empty,
                IdChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    if (!CheckChiPhiCanDelete(SelectedChiPhi.IdChiPhiDuAn.Value))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgErrAddChiPhiChild, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    currentRow = DataDuToanChiPhi.IndexOf(SelectedChiPhi);
                }
                VdtDaDuToanChiPhiModel sourceItem = DataDuToanChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.FTienPheDuyetQDDT = 0;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.Id = Guid.Empty;
                targetItem.IdDuToanChiPhi = Guid.Empty;
                targetItem.IdChiPhi = sourceItem.IdChiPhi;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IdChiPhiDuAnParent = sourceItem.IdChiPhiDuAn;
                targetItem.IsLoaiChiPhi = false;
                targetItem.IsEditHangMuc = true;
                targetItem.IsEditGiaTriChiPhi = true;
                sourceItem.IsHangCha = true;
                sourceItem.GiaTriPheDuyet = 0;
                sourceItem.IsModified = true;
                sourceItem.IsEditHangMuc = false;
                targetItem.IsDuAnChiPhiOld = false;
            }

            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            DataDuToanChiPhi.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        public bool CheckChiPhiCanDelete(Guid duanChiPhiId)
        {
            var listHangMucByChiPhi = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == duanChiPhiId && !x.IsDeleted).ToList();
            if (listHangMucByChiPhi.Count > 0)
            {
                return false;
            }
            return true;
        }

        protected void OnAddNguonVon()
        {
            if (SelectedDuAn == null)
            {
                return;
            }
            VdtDaDuToanNguonVonModel targetItem = new VdtDaDuToanNguonVonModel()
            {
                Id = Guid.NewGuid(),
                IdDuToanNguonVon = Guid.Empty,
                IdNguonVon = null
            };
            int currentRow = -1;
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
            {
                currentRow = 0;
                if (SelectedNguonVon != null)
                {
                    currentRow = DataDuToanNguonVon.IndexOf(SelectedNguonVon);
                }
                else
                {
                    currentRow = DataDuToanNguonVon.IndexOf(DataDuToanNguonVon.Last());
                }

                VdtDaDuToanNguonVonModel sourceItem = DataDuToanNguonVon.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.Id = Guid.NewGuid();
                targetItem.IdDuToanNguonVon = Guid.Empty;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.FTienPheDuyetQDDT = 0;
                targetItem.IdNguonVon = null;
                targetItem.IsDeleted = false;
            }
            targetItem.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            DataDuToanNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataDuToanNguonVon));
        }

        protected void OnDeleteNguonVon()
        {
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0 && SelectedNguonVon != null)
            {
                SelectedNguonVon.IsDeleted = !SelectedNguonVon.IsDeleted;
                OnPropertyChanged(nameof(DataDuToanNguonVon));
            }
        }

        protected void OnDeleteChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IsLoaiChiPhi)
            {
                if (SelectedChiPhi.IdChiPhiDuAnParent == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgVocherParent, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (!CheckChiPhiCanDelete(SelectedChiPhi.IdChiPhiDuAn.Value))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgDeleteChiPhiHasHangMuc, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DataDuToanChiPhi.Count > 0 && SelectedChiPhi != null)
            {
                var listChiPhi = FindListChildChiPhi(SelectedChiPhi.Id);
                listChiPhi.Add(SelectedChiPhi);
                foreach (var item in listChiPhi)
                {
                    item.IsDeleted = !SelectedChiPhi.IsDeleted;
                }
                UpdateListChiPhiCanEdit();
                OnPropertyChanged(nameof(DataDuToanChiPhi));
            }
        }

        private bool ValiDateData()
        {
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckSoQD, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (_vdtDaDuToanService.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgTrungSoQD, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (Model.DNgayQuyetDinh == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNgayPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedLoaiQD == null || (string.IsNullOrEmpty(SelectedLoaiQD.ValueItem)))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckLoaiQuyetDinh, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (_vdtDaDuToanService.checkExistLoaiQuyetDinh(Model.BLaTongDuToan, Model.IIdDuAnId))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckExistLoaiQuyetDinh, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
 
            
            //if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            //{
            //    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDonVi, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (SelectedDuAn == null || string.IsNullOrEmpty(SelectedDuAn.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedChuDauTu == null || string.IsNullOrEmpty(SelectedChuDauTu.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckChuDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (DataDuToanNguonVon.Count > 0)
            {
                var tongNguonVon = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
                if (!IsDieuChinhTKTC && tongNguonVon > TongGiaTriPheDuyetDuAn)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotEqualPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (Model.FTongDuToanPheDuyet > 0)
            {
                double? tongNguonVon = 0;
                if (IsEdit || IsDieuChinhTKTC)
                {
                    tongNguonVon = _vdtDaDuToanService.TinhTongPheDuyetDuAn(Model.IIdDuAnId, Model.Id) + Model.FTongDuToanPheDuyet;
                }
                else
                {
                    tongNguonVon = _vdtDaDuToanService.TinhTongPheDuyetDuAn(Model.IIdDuAnId, null) + Model.FTongDuToanPheDuyet;
                }
                if (tongNguonVon > TongGiaTriPheDuyetDuAn)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.ErrorNguonVonNotEqualPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            //if (!IsDieuChinhTKTC && Model.FTongDuToanPheDuyet != null && ((Model.FTongDuToanPheDuyet + TongDuToanByDuAn) > TongGiaTriPheDuyetDuAn))
            if (!IsDieuChinhTKTC && Model.FTongDuToanPheDuyet != null && (Model.FTongDuToanPheDuyet > TongGiaTriPheDuyetDuAn))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgTongDuToanLonHonTongMucDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //var listDuToan = _vdtDaDuToanService.FindListByDuAnId(SelectedDuAn.Id);
            //if (listDuToan.Count > 0 && Convert.ToBoolean(SelectedLoaiQD.ValueItem))
            //{
            //    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTongDuToan, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    ResetDataWhenChangeDuAn();
            //    return false;
            //}

            var listDaDuToan = _vdtDaDuToanService.FindListByDuAnId(SelectedDuAn.Id);
            var listTongDuToan = listDaDuToan.Where(x => x.BLaTongDuToan);
            var listDuToan = listDaDuToan.Where(x => x.BLaThayThe);

            if (!IsEdit && (listTongDuToan.Any() || (listDuToan.Any() && Convert.ToBoolean(SelectedLoaiQD.ValueItem))))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTongDuToan, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetDataWhenChangeDuAn();
                return false;
            }            

            return true;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaDuToanChiPhiModel objectSender = (VdtDaDuToanChiPhiModel)sender;

            if (args.PropertyName == nameof(VdtDaDuToanChiPhiModel.GiaTriPheDuyet) || args.PropertyName == nameof(VdtDaDuToanChiPhiModel.IsDeleted))
            {
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
                objectSender.IsModified = true;
            }
        }

        private void DetailNguonVonModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaDuToanNguonVonModel objectSender = (VdtDaDuToanNguonVonModel)sender;

            if (args.PropertyName == nameof(VdtDaDuToanNguonVonModel.IdNguonVon))
            {
                if (objectSender.IdNguonVon != null && objectSender.IdNguonVon != 0)
                {
                    if (CheckDuplicateNguonVon(objectSender.IdNguonVon.Value))
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SelectedNguonVon.IdNguonVon = 0;
                        return;
                    }
                }
            }
            if (args.PropertyName == nameof(VdtDaDuToanNguonVonModel.GiaTriPheDuyet) || args.PropertyName == nameof(VdtDaDuToanNguonVonModel.IsDeleted))
            {
                if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
                {
                    var tongNguonVon = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
                    Model.FTongDuToanPheDuyet = tongNguonVon;
                    CalculateConLaiNguonVon();
                    OnPropertyChanged(nameof(Model));
                }
            }

            objectSender.IsModified = true;
        }

        private void CalculateDataChiPhi()
        {
            if (DataDuToanChiPhi == null) return;
            foreach (var item in DataDuToanChiPhi.Where(n => !n.IdChiPhiDuAnParent.HasValue))
            {
                CalculateParent(item);
            }
            OnPropertyChanged(nameof(DataDuToanChiPhi));
        }

        private void CalculateParent(VdtDaDuToanChiPhiModel parentItem)
        {
            int childRow = 0;
            foreach (var item in DataDuToanChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.GiaTriPheDuyet = DataDuToanChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn && !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
        }

        private void SaveDetail(Guid idDuToan)
        {
            List<VdtDaDuToanNguonVonModel> listNguonVonAdd = DataDuToanNguonVon.Where(x => x.IdDuToanNguonVon == null || x.IdDuToanNguonVon == Guid.Empty && !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiAdd = DataDuToanChiPhi.Where(x => (x.Id == null || x.Id == Guid.Empty) && !x.IsDeleted).ToList();
            List<VdtDaDuToanNguonVonModel> listNguonVonEdit = DataDuToanNguonVon.Where(x => x.IdDuToanNguonVon != null && x.IdDuToanNguonVon != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiEdit = DataDuToanChiPhi.Where(x => x.Id != null && x.Id != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaDuToanNguonVonModel> listNguonVonDelete = DataDuToanNguonVon.Where(x => x.IsDeleted && x.IdDuToanNguonVon != null && x.IdDuToanNguonVon != Guid.Empty).ToList();
            List<VdtDaDuToanChiPhiModel> listChiPhiDelete = DataDuToanChiPhi.Where(x => x.IsDeleted && x.IdDuToanChiPhi != null && x.IdDuToanChiPhi != Guid.Empty).ToList();
            // Thêm mới, sửa vào các bảng chi tiết
            if (listNguonVonAdd.Count > 0)
            {
                AddNguonVonDetail(listNguonVonAdd, idDuToan);
            }
            if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
            {
                AddChiPhiDetail(listChiPhiAdd, idDuToan);
            }
            if (listChiPhiEdit != null && listChiPhiEdit.Count > 0)
            {
                UpdateChiPhiDetail(listChiPhiEdit);
            }
            if (listNguonVonEdit != null && listNguonVonEdit.Count > 0)
            {
                UpdateNguonVonDetail(listNguonVonEdit);
            }
            if (listNguonVonDelete.Count > 0)
            {
                DeleteNguonVon(listNguonVonDelete);
            }
            if (listChiPhiDelete.Count > 0)
            {
                DeleteChiPhi(listChiPhiDelete);
            }

            SaveHangMuc();
        }

        private void SaveHangMuc()
        {
            List<DuToanDetailModel> listDMHangMucAdd = DataDuToanHangMuc.Where(x => (x.Id == Guid.Empty || x.Id == null) && !x.IsDeleted && !string.IsNullOrEmpty(x.TenHangMuc)).ToList();
            List<DuToanDetailModel> listDuToanHangMucAdd = DataDuToanHangMuc.Where(x => (x.IdDuToanHangMuc == Guid.Empty || x.IdDuToanHangMuc == null) && !x.IsDeleted && !string.IsNullOrEmpty(x.TenHangMuc)).ToList();
            List<DuToanDetailModel> listDanhMucHangMucEdit = DataDuToanHangMuc.Where(x => !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<DuToanDetailModel> listDuToanHangMucEdit = DataDuToanHangMuc.Where(x => !x.IsDeleted && x.IdDuToanHangMuc != Guid.Empty && x.IdDuToanHangMuc != null).ToList();
            List<DuToanDetailModel> listDetailDelete = DataDuToanHangMuc.Where(x => x.IdDuToanHangMuc != Guid.Empty && x.IdDuToanHangMuc != null && x.IsDeleted).ToList();

            //Thêm mới,sửa vào các bảng chi tiết
            if (listDMHangMucAdd.Count > 0)
            {
                AddDanhMucHangMucDetail(listDMHangMucAdd);
            }
            if (listDuToanHangMucAdd.Count > 0)
            {
                AddDuToanHangMucDetail(listDuToanHangMucAdd);
            }
            if (listDanhMucHangMucEdit.Count > 0)
            {
                UpdateDanhMucHangMucDetail(listDanhMucHangMucEdit);
            }
            if (listDuToanHangMucEdit.Count > 0)
            {
                UpdateDuToanHangMucDetail(listDuToanHangMucEdit);
            }

            if (listDetailDelete.Count > 0)
            {
                DeleteHangMuc(listDetailDelete);
            }
        }

        private void AddChiPhiDetail(List<VdtDaDuToanChiPhiModel> listAdd, Guid idDuToan)
        {
            foreach (var item in listAdd)
            {
                item.IdDuToan = idDuToan;
            }
            // add vào bảng Vdt_Dm_DuAn_ChiPhi
            List<VdtDaDuToanChiPhiModel> listDuanChiPhi = listAdd.Where(x => !x.IsDuAnChiPhiOld).ToList();
            if (listDuanChiPhi != null && listDuanChiPhi.Count > 0)
            {
                List<VdtDmDuAnChiPhi> listDuAnChiPhiAdd = new List<VdtDmDuAnChiPhi>();
                listDuAnChiPhiAdd = _mapper.Map<List<VdtDmDuAnChiPhi>>(listDuanChiPhi);
                _approveProjectService.AddRangeDMDuAnChiPhi(listDuAnChiPhiAdd);
            }

            #region add vào bảng VDt_DA_DuToan_Chiphi
            //List<VdtDaDuToanChiPhiModel> listDuToanChiPhiAdd = listAdd.Where(x => x.IsEditHangMuc).ToList();
            if (listAdd != null && listAdd.Count > 0)
            {
                List<VdtDaDuToanChiPhi> listDuToanChiPhi = new List<VdtDaDuToanChiPhi>();
                listDuToanChiPhi = _mapper.Map<List<VdtDaDuToanChiPhi>>(listAdd);

                _vdtDaDuToanService.AddRangeDuToanChiPhi(listDuToanChiPhi);
            }


            #endregion
        }

        private void AddNguonVonDetail(List<VdtDaDuToanNguonVonModel> listAdd, Guid idDuToan)
        {
            foreach (var item in listAdd)
            {
                item.IdDuToan = idDuToan;
            }
            #region add Nguon von
            List<VdtDaDuToanNguonvon> listDuToanNguonVon = new List<VdtDaDuToanNguonvon>();
            listDuToanNguonVon = _mapper.Map<List<VdtDaDuToanNguonvon>>(listAdd);
            _vdtDaDuToanService.AddRangeDuToanNguonVon(listDuToanNguonVon);

            #endregion
        }

        private void UpdateChiPhiDetail(List<VdtDaDuToanChiPhiModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                // sửa VDT_DM_Duan_Chiphi
                VdtDmDuAnChiPhi duAnChiPhi = _approveProjectService.FindDMDuAnChiPhi(item.IdChiPhiDuAn);
                if (duAnChiPhi != null)
                {
                    duAnChiPhi.STenChiPhi = item.TenChiPhi;
                    duAnChiPhi.IIdChiPhiParent = item.IdChiPhiDuAnParent;
                    _approveProjectService.UpdateVdtDmDuAnChiPhi(duAnChiPhi);
                }

                VdtDaDuToanChiPhi duToanChiPhi = _vdtDaDuToanService.FindDuToanChiPhi(item.IdDuToanChiPhi);
                if (duToanChiPhi != null)
                {
                    duToanChiPhi.FTienPheDuyet = item.GiaTriPheDuyet;
                    _vdtDaDuToanService.UpdateDuToanChiPhi(duToanChiPhi);
                }
            }
        }

        private void UpdateNguonVonDetail(List<VdtDaDuToanNguonVonModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaDuToanNguonvon duToanNV = _vdtDaDuToanService.FindDuToanNguonVon(item.IdDuToanNguonVon);
                if (duToanNV != null)
                {
                    _mapper.Map(item, duToanNV);
                    _vdtDaDuToanService.UpdateDuToanNguonVon(duToanNV);
                }
            }
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<VdtDaDuToanNguonVonModel> listNguonVon = DataDuToanNguonVon.Where(x => x.IdNguonVon == idNguonVon && !x.IsDeleted).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        public bool ValidateDataDetail()
        {
            if (DataDuToanChiPhi.Count > 0 && DataDuToanNguonVon.Count > 0)
            {
                var listChiPhi = DataDuToanChiPhi.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
                var listNguonVon = DataDuToanNguonVon.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0 && x.IdNguonVon != null && x.IdNguonVon != 0).ToList();
                if (listChiPhi == null || listChiPhi.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckChiPhiDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (listNguonVon == null || listNguonVon.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (ConLai != null && ConLai != 0)
                {
                    if (MessageBoxHelper.Confirm(Resources.MsgConfirmErrorChiPhiNotEqualNguonVon) == MessageBoxResult.Yes)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return true;
        }

        private void CalculateConLaiNguonVon()
        {
            double tongNguonVon = 0;
            double tongChiPhi = 0;
            ConLai = 0;
            if (DataDuToanNguonVon != null && DataDuToanNguonVon.Count > 0)
            {
                tongNguonVon = DataDuToanNguonVon.Where(y => !y.IsDeleted).Sum(x => x.GiaTriPheDuyet);
            }

            if (DataDuToanChiPhi != null && DataDuToanChiPhi.Count > 0)
            {
                var listChiPhiCha = DataDuToanChiPhi.Where(x => x.IsLoaiChiPhi).ToList();
                tongChiPhi = listChiPhiCha.Sum(x => x.GiaTriPheDuyet);
            }
            ConLai = tongNguonVon - tongChiPhi;
            FTongNguonVon = tongNguonVon;
            OnPropertyChanged(nameof(FTongNguonVon));
        }

        public void OnShowDetailDuToan()
        {
            if (SelectedChiPhi != null)
            {
                if (HangMucPhanChiaSaved != null)
                    TKTCVaTongDuToanDetailViewModel.HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(n => n.IdDuAnChiPhi == SelectedChiPhi.IdChiPhiDuAn).ToList();
                TKTCVaTongDuToanDetailViewModel.DataChiPhiModel = SelectedChiPhi;
                TKTCVaTongDuToanDetailViewModel.Model = Model;
                TKTCVaTongDuToanDetailViewModel.Items = GetListHangMucDetailByChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                TKTCVaTongDuToanDetailViewModel.IsNotViewDetail = IsNotViewDetail;
                TKTCVaTongDuToanDetailViewModel.Init();
                TKTCVaTongDuToanDetailViewModel.SavedAction = obj => this.LoadDataQDDauTuHangMucSave();
                TKTCVaTongDuToanDetailViewModel.ShowDialog();
            }
        }

        public void LoadDataQDDauTuHangMucSave()
        {
            var lstHangMucPhanChia = TKTCVaTongDuToanDetailViewModel.HangMucPhanChiaSaved;
            if (HangMucPhanChiaSaved == null)
            {
                HangMucPhanChiaSaved = new List<DuToanDetailModel>();
            }
            HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(n => n.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
            HangMucPhanChiaSaved.AddRange(lstHangMucPhanChia);

            DataDuToanHangMucByChiPhi = TKTCVaTongDuToanDetailViewModel.Items;
            if (DataDuToanHangMucByChiPhi.Count > 0)
            {
                SelectedChiPhi.IsEditGiaTriChiPhi = false;
                var giaTriChiPhi = DataDuToanHangMucByChiPhi.Where(x => x.HangMucParentId == null && !x.IsDeleted).Sum(y => y.GiaTriPheDuyet);

                if (giaTriChiPhi != null)
                {
                    SelectedChiPhi.GiaTriPheDuyet = giaTriChiPhi.Value;
                }
                DataDuToanHangMuc = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
                DataDuToanHangMuc.AddRange(DataDuToanHangMucByChiPhi.ToList());
            }
        }

        public ObservableCollection<DuToanDetailModel> GetListHangMucDetailByChiPhi(Guid chiPhiId)
        {
            var result = new List<DuToanDetailModel>();
            // tìm trong ListHangMucSave có data chưa, nếu chưa có data thì lấy theo list DataQDDauTuHangMuc load từ chủ trương đầu tư
            List<DuToanDetailModel> listHangMucByChiPhi = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            if (listHangMucByChiPhi.Count < 1 && (Model.Id == null || Model.Id == Guid.Empty))
            {
                result = DataQDDauTuHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            }

            if (listHangMucByChiPhi.Count > 0)
            {
                result = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();
            }

            return new ObservableCollection<DuToanDetailModel>(ObjectCopier.Clone(result));
        }

        private void DeleteNguonVon(List<VdtDaDuToanNguonVonModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _vdtDaDuToanService.DeleteDuToanNguonVon(item.IdDuToanNguonVon);
            }
        }

        private void DeleteChiPhi(List<VdtDaDuToanChiPhiModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _vdtDaDuToanService.DeleteDuToanChiPhi(item.IdDuToanChiPhi);
            }
        }

        private void AddDanhMucHangMucDetail(List<DuToanDetailModel> listAdd)
        {
            // add hạng mục
            List<VdtDaDuToanDmHangMuc> listDMHangMuc = new List<VdtDaDuToanDmHangMuc>();
            if (listAdd != null && listAdd.Count > 0)
            {
                listDMHangMuc = _mapper.Map<List<VdtDaDuToanDmHangMuc>>(listAdd);
                foreach (var item in listDMHangMuc)
                {
                    VdtDaDuToanDmHangMuc dmHangMuc = _vdtDaDuToanService.FindDuToanDMHangMuc(item.Id);
                    if (dmHangMuc == null)
                    {
                        _vdtDaDuToanService.AddDuToanDanhMucHangMuc(item);
                    }
                }
            }
        }

        private void AddDuToanHangMucDetail(List<DuToanDetailModel> listAdd)
        {
            #region add vào bảng Vdt_da_DuToan_HangMuc
            //List<DuToanDetailModel> listDuToanHangMuc = listAdd.Where(x => !x.IsHangCha).ToList();
            foreach (var item in listAdd)
            {
                item.IIdDuToanId = Model.Id;
            }
            List<VdtDaDuToanHangMuc> listDuToanHangMucAdd = new List<VdtDaDuToanHangMuc>();
            listDuToanHangMucAdd = _mapper.Map<List<VdtDaDuToanHangMuc>>(listAdd);
            _vdtDaDuToanService.AddRangeDuToanHangMuc(listDuToanHangMucAdd);

            #endregion
        }

        private void UpdateDanhMucHangMucDetail(List<DuToanDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaDuToanDmHangMuc danhMucHangMuc = _vdtDaDuToanService.FindDuToanDMHangMuc(item.IdDuAnHangMuc.Value);
                if (danhMucHangMuc != null)
                {
                    _mapper.Map(item, danhMucHangMuc);
                    _vdtDaDuToanService.UpdateDuToanDanhMucHangMuc(danhMucHangMuc);
                }
            }
        }

        private void UpdateDuToanHangMucDetail(List<DuToanDetailModel> listEdit)
        {
            //List<DuToanDetailModel> listDTHangMuc = listEdit.Where(x => x.IsCPNV.Value).ToList();
            foreach (var item in listEdit)
            {
                VdtDaDuToanHangMuc duToanHangMuc = _vdtDaDuToanService.FindDuToanHangMuc(item.IdDuToanHangMuc.Value);
                if (duToanHangMuc != null)
                {
                    _mapper.Map(item, duToanHangMuc);
                    _vdtDaDuToanService.UpdateDuToanHangMuc(duToanHangMuc);
                }
            }
        }

        private void DeleteHangMuc(List<DuToanDetailModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                if (item.IdDuToanHangMuc.HasValue)
                {
                    _vdtDaDuToanService.DeleteDuToanHangMucDetail(item.IdDuToanHangMuc.Value);
                }
            }
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            DialogHost.CloseDialogCommand.Execute(null, null);
            DataDuToanNguonVon = null;
            DataDuToanChiPhi = null;
            Model.IIdDuAnId = null;

            SelectedLoaiQD = null;
            OnPropertyChanged(nameof(DataDuToanNguonVon));
            OnPropertyChanged(nameof(DataDuToanChiPhi));
            OnPropertyChanged(nameof(Model));
        }

        private void ResetDataWhenChangeDuAn()
        {
            SelectedDuAn = null;
            DataDuToanNguonVon = new ObservableCollection<VdtDaDuToanNguonVonModel>();
            ConLai = 0;
            Model.DiaDiem = string.Empty;
            Model.ThoiGianThucHien = string.Empty;
            Model.FTongDuToanPheDuyet = 0;
            DataDuToanChiPhi = new ObservableCollection<VdtDaDuToanChiPhiModel>();
            DataDuToanHangMuc = new List<DuToanDetailModel>();
            OnPropertyChanged(nameof(Model));
        }

        private void LoadChuDauTu()
        {
            //IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByAll();
            ItemsChuDauTu = _mapper.Map<ObservableCollection<ComboboxItem>>(listChuDauTu.OrderBy(x => x.IIDMaDonVi));
        }

        private void LoadDuAnByChuDauTu()
        {
            List<ProjectManagerQuery> listDuan = _projectManagerService.FindByCondition().ToList();

            if (SelectedChuDauTu != null)
            {
                var listDuAnByChuDauTu = listDuan.Where(x => x.IIDMaChuDauTuID == SelectedChuDauTu.HiddenValue).GroupBy(x => x.Id).Select(y => y.First());
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnByChuDauTu);
                OnPropertyChanged(nameof(DataDuAn));

                if (Model != null && Model.IIdDuAnId.HasValue)
                {
                    SelectedDuAn = DataDuAn.FirstOrDefault(n => n.ValueItem.ToLower() == Model.IIdDuAnId.Value.ToString().ToLower());
                    OnPropertyChanged(nameof(SelectedDuAn));
                }
            }
            //if (Model.Id != Guid.Empty)
            //{
            //    DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuan);
            //    OnPropertyChanged(nameof(DataDuAn));
            //}


        }

        private void TaoMaOrders(List<VdtDaDuToanChiPhiModel> data)
        {
            int currentIdThuTu = 0;
            int index = 0;
            foreach (var item in data)
            {
                if (item.IThuTu == currentIdThuTu && !item.IsHangCha)
                {
                    index++;
                    item.SMaOrder = $"{item.IThuTu}_{index}";
                }
                else
                {
                    item.SMaOrder = $"{item.IThuTu}";
                    index = 0;
                }

                currentIdThuTu = item.IThuTu;
            }
        }


        private void OnExport(ExportType exportType)
        {
            try
            {
                if(SelectedDuAn == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        List<VdtQDTChiPhiHangMucExportQuery> lstChiphi = new List<VdtQDTChiPhiHangMucExportQuery>();
                        List<VdtQDTChiPhiHangMucExportQuery> lstResult = new List<VdtQDTChiPhiHangMucExportQuery>();
                        int index = 1;
                        foreach (var cp in DataDuToanChiPhi)
                        {
                            VdtQDTChiPhiHangMucExportQuery result = new VdtQDTChiPhiHangMucExportQuery();
                            result.Muc = 1;
                            result.STT = ToRoman(index);
                            result.SLoai = "CP";
                            result.sTenHangMucCP = cp.TenChiPhi;
                            result.IsHangCha = true;
                            result.IdChiPhi = cp.IdChiPhi;
                            result.IdDAChiPhi = cp.IdChiPhiDuAn;
                            result.fPheDuyetDA = cp.FTienPheDuyetQDDT;
                            lstChiphi.Add(result);
                            index++;
                        }
                        //Add hạng mục cho từng loại chi phí
                        foreach (var item in lstChiphi)
                        {
                            lstResult.Add(item);
                            foreach (var hm in DataQDDauTuHangMuc.Where(x=> x.IdDuAnChiPhi == item.IdDAChiPhi))
                            {
                                VdtQDTChiPhiHangMucExportQuery hmModel = new VdtQDTChiPhiHangMucExportQuery();
                                hmModel.STT = hm.MaOrDer;
                                hmModel.SLoai = "HM";
                                hmModel.sTenHangMucCP = hm.TenHangMuc;
                                if (hm.HangMucParentId == null)
                                {
                                    hmModel.IsHangCha = true;
                                }
                                hmModel.fPheDuyetDA = hm.FTienPheDuyetQDDT;
                                hmModel.IdChiPhi = item.IdChiPhi;
                                hmModel.IdDAChiPhi = item.IdDAChiPhi;
                                lstResult.Add(hmModel);
                            }
                        }


                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Items", lstResult);


                        string templateFileName = Path.Combine(ExportPrefix.PATH_VDT_PDDA, MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_TKTCTDT);
                        string fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_TKTCTDT);
                        string fileNamePrefix = string.Format("Template Import hạng mục TKTCTDT");
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<VdtQDTChiPhiHangMucExportQuery>(templateFileName, data);
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
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnImportData()
        {
            try
            {
                if (SelectedDuAn == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ImportTKTCVaTongDuToanViewModel.Init();
                ImportTKTCVaTongDuToanViewModel.DataDuToanHangMucByChiPhiImport = new ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel>();
                ImportTKTCVaTongDuToanViewModel.DataDuToanChiPhiImport = DataDuToanChiPhi;
                ImportTKTCVaTongDuToanViewModel.SavedAction = obj =>
                {
                    this.LoadDataQDDauTuHangMucImportSave();
                };
                ImportTKTCVaTongDuToanViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadDataQDDauTuHangMucImportSave()
        {
            //var lstHangMucPhanChia = TKTCVaTongDuToanDetailViewModel.HangMucPhanChiaSaved;
            //if (HangMucPhanChiaSaved == null)
            //{
            //    HangMucPhanChiaSaved = new List<DuToanDetailModel>();
            //}
            //HangMucPhanChiaSaved = HangMucPhanChiaSaved.Where(n => n.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
            //HangMucPhanChiaSaved.AddRange(lstHangMucPhanChia);

            foreach (var dtcp in DataDuToanChiPhi)
            {
                dtcp.GiaTriPheDuyet = ImportTKTCVaTongDuToanViewModel.DataDuToanHangMucByChiPhiImport.Where(x => x.IdDuAnChiPhi == dtcp.IdChiPhiDuAn && x.HangMucParentId == null).Sum(x => x.GiaTriPheDuyet) ?? 0;
                dtcp.FTienPheDuyetQDDT = ImportTKTCVaTongDuToanViewModel.DataDuToanHangMucByChiPhiImport.Where(x => x.IdDuAnChiPhi == dtcp.IdChiPhiDuAn && x.HangMucParentId == null).Sum(x => x.FTienPheDuyetQDDT) ?? 0;
            }
            //DataDuToanHangMuc = DataDuToanHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
            DataDuToanHangMuc = new List<VTS.QLNS.CTC.App.Model.DuToanDetailModel>();
            DataDuToanHangMuc.AddRange(ImportTKTCVaTongDuToanViewModel.DataDuToanHangMucByChiPhiImport);
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException(nameof(number), "insert value between 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            else return null;
        }
    }
}
