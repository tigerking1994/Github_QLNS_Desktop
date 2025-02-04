using AutoMapper;
using ControlzEx.Standard;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.ViewModel.MediumTermPlan.PheDuyetDuAn;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.NhanDuToanChiTrenGiao.ImportNdtBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PheDuyetDuAn
{
    public class PheDuyetDuAnDialogViewModel : DialogAttachmentViewModelBase<ApproveProjectModel>
    {
        #region Private
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtDaChuTruongDauTuService _vdtDaChuTruongDauTuService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly IExportService _exportService;
        private readonly IVdtDmChiPhiService _vdtDmChiPhiService;
        private Guid? iIdDieuChinh;
        private Dictionary<Guid, List<ApproveProjectDetailModel>> lstHangMucNotSave;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_PHE_DUYET_DU_AN_DIALOG;
        public override string Title => "Quản lý phê duyệt dự án";
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override Type ContentType => typeof(PheDuyetDuAnDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_PHEDUYET_DUAN;

        #region Items
        private string _sHeaderNguonVon;
        public string SHeaderNguonVon
        {
            get => _sHeaderNguonVon;
            set => SetProperty(ref _sHeaderNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiQuyetDinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiQuyetDinh
        {
            get => _itemsLoaiQuyetDinh;
            set => SetProperty(ref _itemsLoaiQuyetDinh, value);
        }

        private ComboboxItem _selectedLoaiQuyetDinh;
        public ComboboxItem SelectedLoaiQuyetDinh
        {
            get => _selectedLoaiQuyetDinh;
            set
            {
                if (SetProperty(ref _selectedLoaiQuyetDinh, value))
                {
                    if (value != null && value.ValueItem == ((int)LOAI_QD_PDDA.Type.BC_KINH_TE_KY_THUAT).ToString())
                    {
                        SHeaderNguonVon = "Giá trị phê duyệt BCKTKT";
                    }
                    else
                    {
                        SHeaderNguonVon = "Giá trị phê duyệt PDDA";
                    }
                }
                OnPropertyChanged(nameof(SHeaderNguonVon));
            }
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
                    LoadDuAn();
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
                    if (_selectedDuAn != null)
                    {
                        Model.IIdDuAnId = Guid.Parse(_selectedDuAn.ValueItem);
                        GetDuAnById();
                    }
                }
            }
        }

        private List<ComboboxItem> _dataNhomDuAn;
        public List<ComboboxItem> DataNhomDuAn
        {
            get => _dataNhomDuAn;
            set => SetProperty(ref _dataNhomDuAn, value);
        }

        private ComboboxItem _selectedNhomDuAn;
        public ComboboxItem SelectedNhomDuAn
        {
            get => _selectedNhomDuAn;
            set
            {
                SetProperty(ref _selectedNhomDuAn, value);
            }
        }

        private ObservableCollection<VdtDaQddtChiPhiModel> _dataQDDauTuChiPhi;
        public ObservableCollection<VdtDaQddtChiPhiModel> DataQDDauTuChiPhi
        {
            get => _dataQDDauTuChiPhi;
            set
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _dataQDDauTuChiPhi, value);
                OnPropertyChanged(nameof(DataQDDauTuChiPhi));
            }
        }

        private ObservableCollection<VdtDaQddtNguonVonModel> _dataQDDauTuNguonVon;
        public ObservableCollection<VdtDaQddtNguonVonModel> DataQDDauTuNguonVon
        {
            get => _dataQDDauTuNguonVon;
            set => SetProperty(ref _dataQDDauTuNguonVon, value);
        }

        private List<ComboboxItem> _dataNguonVon;
        public List<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private VdtDaQddtChiPhiModel _selectedChiPhi;
        public VdtDaQddtChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set
            {
                SetProperty(ref _selectedChiPhi, value);
            }
        }

        private VdtDaQddtNguonVonModel _selectedNguonVon;
        public VdtDaQddtNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _hinhThucQLItems;
        public ObservableCollection<ComboboxItem> HinhThucQLItems
        {
            get => _hinhThucQLItems;
            set => SetProperty(ref _hinhThucQLItems, value);
        }

        private ComboboxItem _selectedHinhThucQL;
        public ComboboxItem SelectedHinhThucQL
        {
            get => _selectedHinhThucQL;
            set => SetProperty(ref _selectedHinhThucQL, value);
        }

        private List<ApproveProjectDetailModel> _dataChuTruongHangMuc;
        public List<ApproveProjectDetailModel> DataChuTruongHangMuc
        {
            get => _dataChuTruongHangMuc;
            set => SetProperty(ref _dataChuTruongHangMuc, value);
        }

        private List<ApproveProjectDetailModel> _dataQDDTHangMuc;
        public List<ApproveProjectDetailModel> DataQDDTHangMuc
        {
            get => _dataQDDTHangMuc;
            set => SetProperty(ref _dataQDDTHangMuc, value);
        }

        private ObservableCollection<ApproveProjectDetailModel> _dataQDDTHangMucByChiPhi;
        public ObservableCollection<ApproveProjectDetailModel> DataQDDTHangMucByChiPhi
        {
            get => _dataQDDTHangMucByChiPhi;
            set => SetProperty(ref _dataQDDTHangMucByChiPhi, value);
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
        private bool _isImport;
        public bool IsImport
        {
            get => _isImport;
            set => SetProperty(ref _isImport, value);
        }
        private bool _isActiveImport;
        public bool IsActiveImport
        {
            get => _isActiveImport;
            set => SetProperty(ref _isActiveImport, value);
        }

        public bool IsEdit => !IsAdd;
        public bool IsEditable => (Model.BActive == true || IsAdd) && IsNotViewDetail;

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public bool IsDieuChinhPDDA { get; set; }

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
                    if (_selectedChuDauTu != null) Model.IIdChuDauTuId = Guid.Parse(_selectedChuDauTu.ValueItem);
                    LoadDuAnByChuDauTu();
                }
            }
        }


        #endregion

        #region RelayCommand
        public RelayCommand AddChiPhiDetailCommand { get; }
        public RelayCommand AddChiPhiChildDetailCommand { get; }
        public RelayCommand AddNguonVonDetailCommand { get; }
        public RelayCommand DeleteNguonVonDetailCommand { get; }
        public RelayCommand DeleteChiPhiDetailCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        #endregion

        public PheDuyetDuAnDetailViewModel PheDuyetDuAnDetailViewModel { get; }
        public ImportPheDuyetDuAnViewModel ImportPheDuyetDuAnViewModel { get; }

        public PheDuyetDuAnDialogViewModel(
            ILog logger,
            IMapper mapper,
            IApproveProjectService approveProjectService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IDmChuDauTuService chuDauTuService,
            IExportService exportService,
            IVdtDmChiPhiService vdtDmChiPhiService,
            PheDuyetDuAnDetailViewModel pheDuyetDuAnDetailViewModel,
            ImportPheDuyetDuAnViewModel importPheDuyetDuAnViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _approveProjectService = approveProjectService;
            _projectManagerService = projectManagerService;
            _vdtDaChuTruongDauTuService = vdtDaChuTruongDauTuService;
            _chuDauTuService = chuDauTuService;
            _exportService = exportService;
            _vdtDmChiPhiService = vdtDmChiPhiService;

            PheDuyetDuAnDetailViewModel = pheDuyetDuAnDetailViewModel;
            ImportPheDuyetDuAnViewModel = importPheDuyetDuAnViewModel;

            AddChiPhiDetailCommand = new RelayCommand(obj => OnAddChiPhi());
            AddChiPhiChildDetailCommand = new RelayCommand(obj => OnAddChiPhiChild());
            AddNguonVonDetailCommand = new RelayCommand(obj => OnAddNguonVonDetail());
            DeleteNguonVonDetailCommand = new RelayCommand(obj => OnDeleteNguonVon());
            DeleteChiPhiDetailCommand = new RelayCommand(obj => OnDeleteChiPhi());
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowDetailApproveProject());
            ExportCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        #region Event
        public override void Init()
        {
            lstHangMucNotSave = new Dictionary<Guid, List<ApproveProjectDetailModel>>();
            LoadLoaiQuyetDinh();
            LoadAttach();
            LoadDonViByNamLamViec();
            LoadNguonVon();
            LoadDuAn();
            LoadHinhThucQL();
            LoadChuDauTu();
            LoadData();
        }

        private void LoadDonViByNamLamViec()
        {
            var listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).Where(x => x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString(), DisplayItem = n.TenDonVi });
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);

            var strUnitManager = _sessionService.Current.IdsDonViQuanLy;

            if (!string.IsNullOrEmpty(strUnitManager))
            {
                List<string> listUnitManager = strUnitManager.Split(",").ToList();
                _dataDonVi = new ObservableCollection<ComboboxItem>(_dataDonVi.Where(x => listUnitManager.Any(y => y.Contains(x.ValueItem))).ToList());
            }
            else
            {
                _dataDonVi = new ObservableCollection<ComboboxItem>();
            }
            OnPropertyChanged(nameof(DataDuAn));
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            BIsReadOnly = !IsNotViewDetail;
            IsImport = false;
            iIdDieuChinh = null;
            if (Model.Id != Guid.Empty && !IsDieuChinhPDDA)
            {
                Name = "CẬP NHẬT";
                IsAdd = false;
                Description = "Cập nhật thông tin phê duyệt dự án";

                if (DataDonVi != null && Model.IIdMaDonViQuanLy != null)
                {
                    SelectedDonVi = DataDonVi.Where(x => x.ValueItem == Model.IIdMaDonViQuanLy).FirstOrDefault();
                }

                if (DataNhomDuAn != null && Model.IIdNhomDuAnId.HasValue)
                {
                    SelectedNhomDuAn = DataNhomDuAn.Where(x => x.ValueItem == Model.IIdNhomDuAnId.ToString()).FirstOrDefault();
                }
                if (HinhThucQLItems != null && Model.IIdHinhThucQuanLyId.HasValue)
                {
                    SelectedHinhThucQL = HinhThucQLItems.Where(x => x.ValueItem == Model.IIdHinhThucQuanLyId.ToString()).FirstOrDefault();
                }

                //if (Model.IIdChuDauTuId.HasValue)
                //{
                //    SelectedChuDauTu = ItemsChuDauTu.SingleOrDefault(x => x.Id == Model.IIdChuDauTuId);
                //}
                
                if (!String.IsNullOrEmpty(Model.IIdMaChuDauTuId))
                {
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.HiddenValue == Model.IIdMaChuDauTuId);
                }

                if (DataDuAn != null && Model.IIdDuAnId.HasValue)
                {
                    SelectedDuAn = DataDuAn.Where(x => x.Id == Model.IIdDuAnId.Value).FirstOrDefault();
                }

                LoadDataNguonVonByQDDauTuId();
                LoadDataQDDauTuChiPhi();
                LoadDataQDDauTuHangMucByQDDTId();
                // show data dự án theo bản ghi điều chỉnh
                if (IsNotViewDetail == false)
                {
                    Name = "CHI TIẾT";
                    Description = "Xem chi tiết thông tin phê duyệt dự án";
                }
            }
            else
            {
                if (IsDieuChinhPDDA)
                {
                    Name = "ĐIỀU CHỈNH";
                    Description = "Điều chỉnh thông tin phê duyệt dự án";
                    IsAdd = true;
                    ConLai = 0;
                    Model.SSoQuyetDinh = string.Empty;
                    Model.DNgayQuyetDinh = DateTime.Now;
                    if (DataDonVi != null && Model.IIdMaDonViQuanLy != null)
                    {
                        SelectedDonVi = DataDonVi.Where(x => x.ValueItem == Model.IIdMaDonViQuanLy).FirstOrDefault();
                    }

                    if (DataDuAn != null && Model.IIdDuAnId.HasValue)
                    {
                        SelectedDuAn = DataDuAn.Where(x => x.Id == Model.IIdDuAnId.Value).FirstOrDefault();
                    }

                    if (DataNhomDuAn != null && Model.IIdNhomDuAnId.HasValue)
                    {
                        SelectedNhomDuAn = DataNhomDuAn.Where(x => x.ValueItem == Model.IIdNhomDuAnId.ToString()).FirstOrDefault();
                    }
                    if (HinhThucQLItems != null && Model.IIdHinhThucQuanLyId.HasValue)
                    {
                        SelectedHinhThucQL = HinhThucQLItems.Where(x => x.ValueItem == Model.IIdHinhThucQuanLyId.ToString()).FirstOrDefault();
                    }
                    LoadDataQDDauTuHangMucByQDDTId();
                    iIdDieuChinh = Model.Id.Clone();
                    Model.Id = Guid.Empty;
                }
                else
                {
                    Name = "THÊM MỚI";
                    Description = "Thêm mới thông tin phê duyệt dự án";
                    IsAdd = true;
                    DataQDDauTuChiPhi = new ObservableCollection<VdtDaQddtChiPhiModel>();
                    DataDuAn = new ObservableCollection<ComboboxItem>();
                    DataQDDauTuNguonVon = new ObservableCollection<VdtDaQddtNguonVonModel>();
                    DataQDDTHangMuc = new List<ApproveProjectDetailModel>();
                    ConLai = 0;
                    IsImport = true ;
                    Model.DNgayQuyetDinh = DateTime.Now;
                }
            }
            double fSumNguonVon = 0, fSumChiPhi = 0;
            if (DataQDDauTuNguonVon != null)
                fSumNguonVon = DataQDDauTuNguonVon.Where(n => !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
            if (DataQDDauTuChiPhi != null)
                fSumChiPhi = DataQDDauTuChiPhi.Where(n => !n.IsDeleted && !n.IdChiPhiDuAnParent.HasValue).Sum(n => n.GiaTriPheDuyet);
            ConLai = fSumNguonVon - fSumChiPhi;
            OnPropertyChanged(nameof(ConLai));
            OnPropertyChanged(nameof(IsImport));
        }

        private void LoadDataNguonVonByQDDauTuId()
        {
            List<VdtDaQDNguonVonQuery> listQdNguonVonQuery = _approveProjectService.FindListQDDauTuNguonVon(Model.Id).ToList();
            DataQDDauTuNguonVon = _mapper.Map<ObservableCollection<VdtDaQddtNguonVonModel>>(listQdNguonVonQuery);
            foreach (var item in DataQDDauTuNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
        }

        private void LoadDataQDDauTuChiPhi()
        {
            List<VdtDaQddtChiPhiQuery> listQdChiPhiQuery = _approveProjectService.FindListQDDauTuChiPhi(Model.Id).ToList();
            DataQDDauTuChiPhi = _mapper.Map<ObservableCollection<VdtDaQddtChiPhiModel>>(listQdChiPhiQuery);
            UpdateListChiPhiCanEdit();
            foreach (var item in DataQDDauTuChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }

            TaoMaOrders(DataQDDauTuChiPhi.ToList());
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        private void LoadDataQDDauTuChiPhiDefault()
        {
            List<VdtDaQddtChiPhiQuery> listQdChiPhiQuery = _approveProjectService.FindListAllLoaiChiPhi().ToList();
            DataQDDauTuChiPhi = _mapper.Map<ObservableCollection<VdtDaQddtChiPhiModel>>(listQdChiPhiQuery);
            foreach (var item in DataQDDauTuChiPhi)
            {
                item.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            }
        }

        private void LoadHinhThucQL()
        {
            IEnumerable<VdtDmHinhThucQuanLy> listHinhThucQL = _approveProjectService.GetAllHinhThucQuanLy();
            HinhThucQLItems = _mapper.Map<ObservableCollection<ComboboxItem>>(listHinhThucQL);
            OnPropertyChanged(nameof(HinhThucQLItems));
        }

        private void OnAddChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IdChiPhiDuAnParent == null || SelectedChiPhi.IsDeleted)
            {
                return;
            }
            VdtDaQddtChiPhiModel targetItem = new VdtDaQddtChiPhiModel();

            int currentRow = -1;
            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    currentRow = DataQDDauTuChiPhi.IndexOf(SelectedChiPhi);
                }

                VdtDaQddtChiPhiModel sourceItem = DataQDDauTuChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.Id = Guid.Empty;
                targetItem.IdQDChiPhi = Guid.Empty;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();

            }
            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataQDDauTuChiPhi.Insert(currentRow + 1, targetItem);
            UpdateListChiPhiCanEdit();
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        private void OnAddChiPhiChild()
        {
            VdtDaQddtChiPhiModel targetItem = new VdtDaQddtChiPhiModel()
            {
                Id = Guid.Empty,
                IdQDChiPhi = Guid.Empty
            };
            int currentRow = -1;
            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                currentRow = 0;
                if (SelectedChiPhi != null)
                {
                    if (SelectedChiPhi.IsDeleted)
                    {
                        return;
                    }
                    if (!CheckChiPhiCanDelete())
                    {
                        System.Windows.Forms.MessageBox.Show(Resources.MsgErrAddChiPhiChild, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    currentRow = DataQDDauTuChiPhi.IndexOf(SelectedChiPhi);
                }
                VdtDaQddtChiPhiModel sourceItem = DataQDDauTuChiPhi.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.GiaTriPheDuyet = 0;
                targetItem.IsHangCha = false;
                targetItem.TenChiPhi = string.Empty;
                targetItem.Id = Guid.Empty;
                targetItem.IdQDChiPhi = Guid.Empty;
                targetItem.IdChiPhiDuAn = Guid.NewGuid();
                targetItem.IdChiPhiDuAnParent = sourceItem.IdChiPhiDuAn;
                targetItem.IsEditHangMuc = true;
                sourceItem.IsHangCha = true;
                sourceItem.GiaTriPheDuyet = 0;
                sourceItem.IsModified = true;
                sourceItem.IsEditHangMuc = false;
            }

            targetItem.PropertyChanged += DetailChiPhiModel_PropertyChanged;
            DataQDDauTuChiPhi.Insert(currentRow + 1, targetItem);
            UpdateListChiPhiCanEdit();
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        protected void OnDeleteChiPhi()
        {
            if (SelectedChiPhi == null || SelectedChiPhi.IdChiPhiDuAnParent == null)
            {
                if (SelectedChiPhi.IdChiPhiDuAnParent == null)
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgVocherParent, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }
            if (!CheckChiPhiCanDelete())
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgDeleteChiPhiHasHangMuc, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DataQDDauTuChiPhi.Count > 0 && SelectedChiPhi != null)
            {
                var listChiPhi = FindListChildChiPhi(SelectedChiPhi.Id);
                listChiPhi.Add(SelectedChiPhi);
                foreach (var item in listChiPhi)
                {
                    item.IsDeleted = !SelectedChiPhi.IsDeleted;
                }
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
                UpdateListChiPhiCanEdit();
                OnPropertyChanged(nameof(DataQDDauTuChiPhi));
            }
        }

        public bool CheckChiPhiCanDelete()
        {
            var listHangMucByChiPhi = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == SelectedChiPhi.IdChiPhiDuAn && !x.IsDeleted).ToList();
            if (listHangMucByChiPhi.Count > 0)
            {
                return false;
            }
            return true;
        }

        private void UpdateListChiPhiCanEdit()
        {
            foreach (var item in DataQDDauTuChiPhi.Where(x => x.IsHangCha && !x.IsDeleted))
            {
                List<VdtDaQddtChiPhiModel> listChild = null;
                if (item.IdChiPhiDuAn.HasValue)
                {
                    listChild = FindListChildChiPhi(item.IdChiPhiDuAn.Value);
                }
                if (listChild == null || listChild.Count == 0)
                {
                    item.IsEditHangMuc = true;
                }
                else
                {
                    item.IsEditHangMuc = false;
                }
            }
            OnPropertyChanged(nameof(DataQDDauTuChiPhi));
        }

        public List<VdtDaQddtChiPhiModel> FindListChildChiPhi(Guid parentId)
        {
            List<VdtDaQddtChiPhiModel> inner = new List<VdtDaQddtChiPhiModel>();
            foreach (var t in DataQDDauTuChiPhi.Where(item => item.IdChiPhiDuAnParent == parentId && !item.IsDeleted))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildChiPhi(t.Id)).ToList();
            }

            return inner;
        }

        private void OnAddNguonVonDetail()
        {
            VdtDaQddtNguonVonModel targetItem = new VdtDaQddtNguonVonModel()
            {
                Id = Guid.NewGuid(),
                IdQDNguonVon = Guid.Empty,
                IdNguonVon = 0
            };
            int currentRow = -1;
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0)
            {
                currentRow = 0;
                if (SelectedNguonVon != null)
                {
                    currentRow = DataQDDauTuNguonVon.IndexOf(SelectedNguonVon);
                }
                else
                {
                    currentRow = DataQDDauTuNguonVon.IndexOf(DataQDDauTuNguonVon.Last());
                }

                VdtDaQddtNguonVonModel sourceItem = DataQDDauTuNguonVon.ElementAt(currentRow);
                targetItem = ObjectCopier.Clone(sourceItem);
                targetItem.Id = Guid.NewGuid();
                targetItem.IdQDNguonVon = Guid.Empty;
                targetItem.IdNguonVon = 0;
                targetItem.GiaTriPheDuyet = 0;
                targetItem.GiaTriPheDuyetCTDT = 0;
                targetItem.IsDeleted = false;
            }
            targetItem.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            DataQDDauTuNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(DataQDDauTuNguonVon));
            CalculateConLaiNguonVon();
        }

        protected void OnDeleteNguonVon()
        {
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0 && SelectedNguonVon != null)
            {
                SelectedNguonVon.IsDeleted = !SelectedNguonVon.IsDeleted;
                CalculateConLaiNguonVon();
                OnPropertyChanged(nameof(DataQDDauTuNguonVon));
            }
        }

        private void DetailNguonVonModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaQddtNguonVonModel objectSender = (VdtDaQddtNguonVonModel)sender;
            if (args.PropertyName == nameof(VdtDaQddtNguonVonModel.GiaTriPheDuyet))
            {
                List<VdtDaQddtNguonVonModel> listNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted && (x.IdNguonVon != 0)).ToList();
                var tongGiaTriPheDuyet = DataQDDauTuNguonVon.Sum(x => x.GiaTriPheDuyet);
                Model.FTongMucDauTuPheDuyet = tongGiaTriPheDuyet;
                OnPropertyChanged(nameof(DataQDDauTuNguonVon));
                OnPropertyChanged(nameof(Model));
                CalculateConLaiNguonVon();
            }

            if (args.PropertyName == nameof(VdtDaQddtNguonVonModel.IdNguonVon))
            {
                if (objectSender.IdNguonVon != 0)
                {
                    if (CheckDuplicateNguonVon(objectSender.IdNguonVon))
                    {
                        MessageBoxHelper.Error(Resources.MsgCheckTrungNguonVonDauTu);
                        SelectedNguonVon.IdNguonVon = 0;
                        return;
                    }
                }
            }

            objectSender.IsModified = true;
        }

        private bool CheckDuplicateNguonVon(int idNguonVon)
        {
            List<VdtDaQddtNguonVonModel> listNguonVon = DataQDDauTuNguonVon.Where(x => x.IdNguonVon == idNguonVon && !x.IsDeleted).ToList();
            if (listNguonVon != null && listNguonVon.Count > 1)
            {
                return true;
            }
            return false;
        }

        private void DetailChiPhiModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaQddtChiPhiModel objectSender = (VdtDaQddtChiPhiModel)sender;

            if (args.PropertyName == nameof(VdtDaQddtChiPhiModel.GiaTriPheDuyet))
            {
                CalculateDataChiPhi();
                CalculateConLaiNguonVon();
            }

            objectSender.IsModified = true;
        }

        public void OnShowDetailApproveProject()
        {
            if (SelectedChiPhi != null)
            {
                if (SelectedChiPhi.IdChiPhiDuAn == null)
                {
                    return;
                }
                bool bIsSave = false;
                PheDuyetDuAnDetailViewModel.DataChiPhiModel = SelectedChiPhi;
                PheDuyetDuAnDetailViewModel.Model = Model;
                PheDuyetDuAnDetailViewModel.IsNotViewDetail = IsNotViewDetail;
                PheDuyetDuAnDetailViewModel.Items = GetListHangMucDetailByChiPhi(SelectedChiPhi.IdChiPhiDuAn.Value);
                PheDuyetDuAnDetailViewModel.Init();
                PheDuyetDuAnDetailViewModel.SavedAction = obj => this.LoadDataQDDauTuHangMucSave(ref bIsSave);
                PheDuyetDuAnDetailViewModel.ShowDialog();
            }
        }

        public void LoadDataQDDauTuHangMucSave(ref bool bIsSave)
        {
            bIsSave = true;
            DataQDDTHangMucByChiPhi = PheDuyetDuAnDetailViewModel.Items;
            //nếu là trường hợp thêm mới phê duyệt dự án , thì xử lý với các hạng mục kế thừa từ chủ trương đầu tư
            if (Model.Id.IsNullOrEmpty())
            {
                foreach (var item in DataQDDTHangMucByChiPhi.Where(x => x.GiaTriPheDuyet > 0))
                {
                    var hangMucInChuTruong = DataChuTruongHangMuc.Where(x => x.IdDuAnHangMuc == item.IdDuAnHangMuc).FirstOrDefault();
                    if (hangMucInChuTruong != null)
                    {
                        if (DataQDDTHangMucByChiPhi.Any(n => n.HangMucParentId == item.IdDuAnHangMuc && (n.GiaTriPheDuyet ?? 0) == 0)) continue;
                        DataChuTruongHangMuc.Remove(hangMucInChuTruong);
                    }

                }
            }

            var giaTriChiPhi = DataQDDTHangMucByChiPhi.Where(x => x.HangMucParentId == null && !x.IsDeleted).Sum(y => y.GiaTriPheDuyet);

            if (giaTriChiPhi != null)
            {
                SelectedChiPhi.GiaTriPheDuyet = giaTriChiPhi.Value;
            }
            DataQDDTHangMuc = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
            //DataQDDTHangMuc.AddRange(DataQDDTHangMucByChiPhi.Where(n => (n.GiaTriPheDuyet ?? 0) != 0).ToList());
            DataQDDTHangMuc.AddRange(DataQDDTHangMucByChiPhi);
            UpdateListChiPhiCanEditGiaTriChiPhi();
        }

        private void UpdateListChiPhiCanEditGiaTriChiPhi()
        {
            foreach (var item in DataQDDauTuChiPhi.Where(x => x.IsEditHangMuc && !x.IsDeleted))
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

        public bool CheckChiPhiCanDelete(Guid duanChiPhiId)
        {
            var listHangMucByChiPhi = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == duanChiPhiId && !x.IsDeleted).ToList();
            if (listHangMucByChiPhi.Count > 0)
            {
                return false;
            }
            return true;
        }

        public void LoadDataChuTruongHangMuc()
        {
            List<ApproveProjectDetailQuery> listData = new List<ApproveProjectDetailQuery>();

            listData = _approveProjectService.FindListDetailBeforeSave(Model.IIdDuAnId.Value, Model.Id).ToList();

            DataChuTruongHangMuc = _mapper.Map<List<Model.ApproveProjectDetailModel>>(listData);
        }

        public void LoadDataQDDauTuHangMucByQDDTId()
        {
            List<ApproveProjectDetailQuery> listData = new List<ApproveProjectDetailQuery>();
            listData = _approveProjectService.FindListHangMucByQDDauTu(Model.Id).ToList();
            DataQDDTHangMuc = _mapper.Map<List<Model.ApproveProjectDetailModel>>(listData);
        }

        public ObservableCollection<ApproveProjectDetailModel> GetListHangMucDetailByChiPhi(Guid chiPhiId)
        {
            var result = new List<ApproveProjectDetailModel>();
            // tìm trong ListHangMucSave có data chưa, nếu chưa có data thì lấy theo list DataQDDauTuHangMuc load từ chủ trương đầu tư
            List<ApproveProjectDetailModel> listHangMucByChiPhi = new List<ApproveProjectDetailModel>();
            listHangMucByChiPhi = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi == chiPhiId).ToList();

            var dataChuTruongHangMucClone = DataChuTruongHangMuc != null ? ObjectCopier.Clone(DataChuTruongHangMuc) : new List<ApproveProjectDetailModel>();

            foreach (var dt in dataChuTruongHangMucClone)
            {
                dt.BIsEdit = true;
            }
            if (IsAdd == true)
            {
                result = dataChuTruongHangMucClone.Where(x => x.GiaTriPheDuyet == 0).ToList();
            }

            // trường hợp import thì ưu tiên lấy theo case import
            if (listHangMucByChiPhi.Count > 0 || IsActiveImport == true)
            {
                result = listHangMucByChiPhi;
                foreach (var item in DataQDDTHangMuc.ToList())
                {
                    if(result.Count() > 0)
                    {
                        if (result.FirstOrDefault(n => n.IdDuAnHangMuc == item.IdDuAnHangMuc) == null && result.FirstOrDefault().IdDuAnChiPhi == item.IdDuAnChiPhi)
                            result.Add(item);
                    }    
                    
                }

            }
            result.OrderBy(x => x.MaOrDer);

            return new ObservableCollection<ApproveProjectDetailModel>(ObjectCopier.Clone(result));
        }

        public override void OnSave(object obj)
        {
            if (!ValiDateData() || !ValidateDataDetail())
            {
                return;
            }

            if (DataQDDTHangMuc == null || DataQDDTHangMuc.Count == 0)
            {
                if (MessageBoxHelper.Confirm(Resources.MsgConfirmNotHaveHangMucSave) == MessageBoxResult.No) return;
            }

            VdtDaQddauTu entity;
            try
            {
                int result;
                if (SelectedHinhThucQL != null)
                {
                    Model.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
                }
                if (!IsAdd)
                {
                    // Update
                    entity = _approveProjectService.FindById(Model.Id);
                    var bIsGoc = entity.BIsGoc.Clone();
                    _mapper.Map(Model, entity);
                    entity.ILoaiQuyetDinh = int.Parse(SelectedLoaiQuyetDinh.ValueItem);
                    entity.BIsGoc = bIsGoc;
                    entity.BActive = true;
                    entity.DDateUpdate = DateTime.Now;
                    entity.SUserUpdate = _sessionService.Current.Principal;
                    entity.SSoBuocThietKe = Model.SSoBuocThietKe;
                    result = _approveProjectService.Update(entity);
                    IsAdd = false;
                }
                else
                {
                    // Add VdtDaQddauTu
                    entity = _mapper.Map<VdtDaQddauTu>(Model);
                    entity.BIsGoc = true;
                    entity.BActive = true;
                    entity.ILoaiQuyetDinh = int.Parse(SelectedLoaiQuyetDinh.ValueItem);
                    if (IsDieuChinhPDDA)
                    {
                        entity.BIsGoc = false;
                        entity.IIdParentId = iIdDieuChinh;
                        if (iIdDieuChinh.HasValue)
                        {
                            var dataParent = _approveProjectService.FindById(iIdDieuChinh.Value);
                            if (dataParent != null)
                            {
                                dataParent.BActive = false;
                                _approveProjectService.Update(dataParent);
                            }
                        }
                    }
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;
                    entity.SSoBuocThietKe = Model.SSoBuocThietKe;
                    Model.SUserCreate = entity.SUserCreate;

                    //tìm CTDT bactive = 1
                    VdtDaChuTruongDauTu chutruong = _vdtDaChuTruongDauTuService.FindByDuAnId(Model.IIdDuAnId.Value);
                    if (chutruong != null)
                    {
                        entity.IIdChuTruongDauTuId = chutruong.Id;
                    }
                    result = _approveProjectService.Add(entity);
                    Model.Id = entity.Id;
                    Model.BActive = entity.BActive;
                }

                if (result > 0)
                {
                    UpdateDuAn();
                }

                Model.Id = entity.Id;
                IsAdd = false;
                SaveDataDetail();

                // Save attach file
                SaveAttachment(Model.Id);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveError, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            SavedAction?.Invoke(_mapper.Map<ApproveProjectModel>(entity));
            ((Window)obj).Close();
            LoadData();
        }
        private void LoadChuDauTu()
        {
            //IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByAll();
            ItemsChuDauTu = _mapper.Map<ObservableCollection<ComboboxItem>>(listChuDauTu.OrderBy(x => x.IIDMaDonVi));
        }

        private void TaoMaOrders(List<VdtDaQddtChiPhiModel> data)
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
        #endregion

        #region Helper
        private void SaveDataDetail()
        {
            SaveChiPhi();
            SaveNguonVon();
            SaveHangMuc();
        }

        private void SaveChiPhi()
        {
            if (DataQDDauTuChiPhi.Count > 0)
            {
                foreach (var item in DataQDDauTuChiPhi)
                {
                    item.IdQDDauTu = Model.Id;
                }
            }
            //var lstDelete = DataQDDauTuChiPhi.Where(n => n.IsDeleted);
            var listChiPhiAdd = DataQDDauTuChiPhi.Where(x => (x.Id == null || x.Id == Guid.Empty) && !x.IsDeleted).ToList();
            var listChiPhiEdit = DataQDDauTuChiPhi.Where(x => x.IsModified && x.IdQDChiPhi != null && x.IdQDChiPhi != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaQddtChiPhiModel> listChiPhiDelete = DataQDDauTuChiPhi.Where(x => (x.IsDeleted && x.IdQDChiPhi != null && x.IdQDChiPhi != Guid.Empty)).ToList();

            if (listChiPhiAdd.Count > 0)
            {
                // add vào bảng Vdt_Dm_DuAn_ChiPhi
                //List<VdtDaQddtChiPhiModel> listDuanChiPhi = listChiPhiAdd.Where(x => x.IdChiPhiDuAnParent != null).ToList();
                List<VdtDmDuAnChiPhi> listDuAnChiPhiAdd = new List<VdtDmDuAnChiPhi>();
                listDuAnChiPhiAdd = _mapper.Map<List<VdtDmDuAnChiPhi>>(listChiPhiAdd);
                _approveProjectService.AddRangeDMDuAnChiPhi(listDuAnChiPhiAdd);

                #region add vào bảng VDt_DA_QDDauTu_Chi phi
                //List<VdtDaQddtChiPhiModel> listQDChiPhiAdd = listAdd.Where(x => x.IsEditHangMuc).ToList();
                if (listChiPhiAdd != null && listChiPhiAdd.Count > 0)
                {
                    List<VdtDaQddauTuChiPhi> listQDChiPhi = new List<VdtDaQddauTuChiPhi>();
                    listQDChiPhi = _mapper.Map<List<VdtDaQddauTuChiPhi>>(listChiPhiAdd);

                    _approveProjectService.AddRangeChiPhi(listQDChiPhi);
                }

                if (listChiPhiDelete.Count > 0)
                {
                    foreach (var item in listChiPhiDelete)
                    {
                        if (item.IdQDChiPhi.HasValue)
                            _approveProjectService.DeleteChiPhi(item.IdQDChiPhi.Value);
                    }
                }
                #endregion
            }

            //update
            if (listChiPhiEdit.Count > 0)
            {
                foreach (var item in listChiPhiEdit)
                {
                    // sửa VDT_DM_Duan_Chiphi
                    VdtDmDuAnChiPhi duAnChiPhi = _approveProjectService.FindDMDuAnChiPhi(item.IdChiPhiDuAn);
                    if (duAnChiPhi != null)
                    {
                        duAnChiPhi.STenChiPhi = item.TenChiPhi;
                        duAnChiPhi.IIdChiPhiParent = item.IdChiPhiDuAnParent;
                        _approveProjectService.UpdateVdtDmDuAnChiPhi(duAnChiPhi);
                    }

                    VdtDaQddauTuChiPhi qDChiPhi = _approveProjectService.FindChiPhi(item.IdQDChiPhi);
                    if (qDChiPhi != null)
                    {
                        qDChiPhi.FTienPheDuyet = item.GiaTriPheDuyet;
                        _approveProjectService.UpdateChiPhi(qDChiPhi);
                    }
                }
            }

            //delete QDDauTuChiPhi
            if (listChiPhiDelete.Count > 0)
            {
                foreach (var item in listChiPhiDelete)
                {
                    if (item.IdQDChiPhi.HasValue)
                        _approveProjectService.DeleteChiPhi(item.IdQDChiPhi.Value);
                }
            }
        }

        private void SaveNguonVon()
        {
            List<VdtDaQddtNguonVonModel> listNguonVonAdd = DataQDDauTuNguonVon.Where(x => x.IdQDNguonVon == null || x.IdQDNguonVon == Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaQddtNguonVonModel> listNguonVonEdit = DataQDDauTuNguonVon.Where(x => x.IsModified && x.IdQDNguonVon != null && x.IdQDNguonVon != Guid.Empty && !x.IsDeleted).ToList();
            List<VdtDaQddtNguonVonModel> listNguonVonDelete = DataQDDauTuNguonVon.Where(x => x.IsDeleted && x.IdQDNguonVon != null && x.IdQDNguonVon != Guid.Empty).ToList();

            if (listNguonVonAdd.Count > 0)
            {
                foreach (var item in listNguonVonAdd)
                {
                    item.IdQDDauTu = Model.Id;
                }

                List<VdtDaQddauTuNguonVon> listQDNguonVon = new List<VdtDaQddauTuNguonVon>();
                listQDNguonVon = _mapper.Map<List<VdtDaQddauTuNguonVon>>(listNguonVonAdd);
                _approveProjectService.AddRangeNguonVon(listQDNguonVon);
            }

            if (listNguonVonEdit.Count > 0)
            {
                foreach (var item in listNguonVonEdit)
                {
                    VdtDaQddauTuNguonVon quyetDinhNV = _approveProjectService.FindNguonVon(item.IdQDNguonVon);
                    if (quyetDinhNV != null)
                    {
                        _mapper.Map(item, quyetDinhNV);
                        _approveProjectService.UpdateNguonVon(quyetDinhNV);
                    }
                }
            }

            if (listNguonVonDelete.Count > 0)
            {
                foreach (var item in listNguonVonDelete)
                {
                    _approveProjectService.DeleteNguonVon(item.IdQDNguonVon);
                }
            }
        }


        private void SaveHangMuc()
        {
            // List<ApproveProjectDetailModel> listDMHangMucAdd = DataQDDTHangMuc.Where(x => !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && x.GiaTriPheDuyet > 0).ToList();
            List<ApproveProjectDetailModel> listQDHangMucAdd = DataQDDTHangMuc.Where(x => x.IsModified && !x.IsDeleted && (x.IdQDHangMuc == Guid.Empty || x.IdQDHangMuc == null) && x.GiaTriPheDuyet > 0).ToList();  // những hạng mục thêm mới
            // List<ApproveProjectDetailModel> listDMHangMucEdit = DataQDDTHangMuc.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<ApproveProjectDetailModel> listQDHangMucEdit = DataQDDTHangMuc.Where(x => x.IsModified && !x.IsDeleted && x.IdQDHangMuc != Guid.Empty && x.IdQDHangMuc != null).ToList();  // những hạng mục thêm sửa

            List<ApproveProjectDetailModel> listDetailDelete = DataQDDTHangMuc.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            //Thêm mới,sửa vào các bảng chi tiết
            /*
            if (listDMHangMucAdd != null && listDMHangMucAdd.Count > 0)
            {
                AddDanhMucHangMucDetail(listDMHangMucAdd);
            }
            */

            //Nếu thực hiện import thì xóa dữ liệu cũ, đi theo dữ liệu mới
            if (IsActiveImport)
            {
                //Tìm các hạng mục theo qddt
                _vdtDaChuTruongDauTuService.DeleteChuTruongDauTuHangMuc(Model.Id);


            }

            if (listQDHangMucAdd != null && listQDHangMucAdd.Count > 0 && listQDHangMucEdit.Count == 0)
            {
                AddDanhMucHangMucDetail(listQDHangMucAdd, false);
            }
            if (listQDHangMucAdd != null && listQDHangMucAdd.Count > 0 && listQDHangMucEdit.Count > 0)
            {
                AddDanhMucHangMucDetail(listQDHangMucAdd, true);
            }

            if (listQDHangMucEdit != null && listQDHangMucEdit.Count > 0)
            {
                UpdateDanhMucHangMucDetail(listQDHangMucEdit);
            }
            if (listQDHangMucEdit != null && listQDHangMucEdit.Count > 0)
            {
                UpdateQDHangMucDetail(listQDHangMucEdit);
            }

            //Sửa vào các bảng chi tiết

            if (listDetailDelete.Count > 0)
            {
                DeleteDetail(listDetailDelete);
            }
        }

        //private void AddDanhMucHangMucDetail(List<ApproveProjectDetailModel> listAdd, ref Dictionary<Guid, Guid> dicIdConvert)
        private void AddDanhMucHangMucDetail(List<ApproveProjectDetailModel> listAdd, bool isEdit)
        {
            // add hạng mục vào bảng VdtDmDuAnHangMuc

            List<VdtDaQddauTuDmHangMuc> listDMHangMuc = new List<VdtDaQddauTuDmHangMuc>();
            List<VdtDaQddauTuHangMuc> listHangMucNew = new List<VdtDaQddauTuHangMuc>();
            Dictionary<Guid, Guid> dicConvert = new Dictionary<Guid, Guid>();
            List<Tuple<Guid, Guid, Guid>> listConvert = new List<Tuple<Guid, Guid, Guid>>();
            // dicIdConvert = new Dictionary<Guid, Guid>();
            foreach (var item in listAdd)
            {
                VdtDaQddauTuDmHangMuc data = new VdtDaQddauTuDmHangMuc();
                // viết cj k hiểu ?????
                /*
                if (!dicIdConvert.ContainsKey(item.IdDuAnHangMuc.Value))
                {
                    data.Id = Guid.NewGuid();
                    dicIdConvert.Add(item.IdDuAnHangMuc.Value, data.Id);
                }
                else
                {
                    data.Id = dicIdConvert[item.IdDuAnHangMuc.Value];
                }

                data.SMaHangMuc = item.MaHangMuc;
                data.STenHangMuc = item.TenHangMuc;
                // nếu có cha mà chưa có trong convert thì key = id cha, value
                if (item.HangMucParentId.HasValue)
                {
                    if (!dicIdConvert.ContainsKey(item.HangMucParentId.Value))
                    {
                        data.IIdParentId = Guid.NewGuid();
                        dicIdConvert.Add(item.HangMucParentId.Value, data.IIdParentId.Value);
                    }
                    else
                    {
                        data.IIdParentId = item.HangMucParentId.Value;
                    }
                }
                data.MaOrder = item.MaOrDer;
                data.IdLoaiCongTrinh = item.IdLoaiCongTrinh;
                data.BIsEdit = false;
                listDMHangMuc.Add(data);
                */
                data.Id = /*item.IdDuAnHangMuc ??*/ Guid.NewGuid();
                if (item.IdDuAnHangMuc.HasValue)
                {
                    listConvert.Add(new Tuple<Guid, Guid, Guid>(item.IdDuAnHangMuc.Value, item.IdDuAnChiPhi.Value, data.Id));
                    //dicConvert.Add(item.IdDuAnHangMuc.Value, data.Id); //Không dùng dic được do có trường hợp bị trùng IdDuAnHangMuc
                }


                data.STenHangMuc = item.TenHangMuc;
                data.SMaHangMuc = item.MaHangMuc;
                data.MaOrder = item.MaOrDer;
                data.IdLoaiCongTrinh = item.IdLoaiCongTrinh;
                data.BIsEdit = false;
                if (isEdit == true)
                    data.IIdParentId = item.HangMucParentId;
                else
                {
                    if (item.HangMucParentId.HasValue && listConvert.Any(x=> x.Item1 == item.HangMucParentId.Value && x.Item2 == item.IdDuAnChiPhi.Value))
                    {
                        data.IIdParentId = listConvert.Find(x => x.Item1 == item.HangMucParentId.Value && x.Item2 == item.IdDuAnChiPhi.Value).Item3;
                    }
                    //if (item.HangMucParentId.HasValue && dicConvert.ContainsKey(item.HangMucParentId.Value))
                    //{
                    //    data.IIdParentId = dicConvert[item.HangMucParentId.Value];
                    //}
                    //if (listDMHangMuc.Count > 0)
                    //    if (item.HangMucParentId != null)
                    //        data.IIdParentId = listDMHangMuc.Last().Id;
                    //    else
                    //        data.IIdParentId = listDMHangMuc.Last().IIdParentId;
                }

                data.IIdDuAnId = item.IIdDuAnId;
                data.SMoTa = "";
                data.SQuyMo = "";
                listDMHangMuc.Add(data);

                VdtDaQddauTuHangMuc hm = new VdtDaQddauTuHangMuc();
                hm.Id = Guid.NewGuid();
                hm.IIdQddauTuId = Model.Id;
                hm.STenHangMuc = item.TenHangMuc;
                hm.SMaHangMuc = item.MaHangMuc;
                hm.MaOrder = item.MaOrDer;
                //if (isEdit == false && listHangMucNew != null && item.HangMucParentId != null)
                //    if (listHangMucNew.Last().ParentId == null && item.HangMucParentId != null)
                //        hm.IIdParentId = listHangMucNew.Last().Id;
                //    else
                //        hm.IIdParentId = listHangMucNew.Last().IIdParentId;
                hm.IdLoaiCongTrinh = item.IdLoaiCongTrinh;
                hm.IIdHangMucId = data.Id;
                hm.IIdChiPhiId = item.IdChiPhi;
                hm.IIdDuAnChiPhi = item.IdDuAnChiPhi;
                hm.FTienPheDuyet = item.GiaTriPheDuyet;
                listHangMucNew.Add(hm);
            }

            // insert vào VDT_DA_QDDauTu_HangMuc trước rồi mới insert vào VDT_DA_QDDauTu_DM_HangMuc do lúc lấy ra dùng inner join
            _approveProjectService.AddRangeQdDauTuDMHangMuc(listDMHangMuc);
            _approveProjectService.AddRangeHangMuc(listHangMucNew);
        }

        //private void AddQDHangMucDetail(List<ApproveProjectDetailModel> listAdd, Dictionary<Guid, Guid> dicIdConvert)
        private void AddQDHangMucDetail(List<ApproveProjectDetailModel> listAdd)
        {
            // add vào bảng QDDauTuTHangMuc
            /*
            foreach (var item in listAdd)
            {
                if (dicIdConvert.ContainsKey(item.IdDuAnHangMuc.Value))
                    item.IdDuAnHangMuc = dicIdConvert[item.IdDuAnHangMuc.Value];
                item.IIdQddauTuId = Model.Id;
            }
            */
            List<VdtDaQddauTuHangMuc> listQdDtHangMuc = new List<VdtDaQddauTuHangMuc>();
            listQdDtHangMuc = _mapper.Map<List<VdtDaQddauTuHangMuc>>(listAdd);

            _approveProjectService.AddRangeHangMuc(listQdDtHangMuc);
        }

        private void UpdateDanhMucHangMucDetail(List<ApproveProjectDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaQddauTuDmHangMuc danhMucHangMuc = _approveProjectService.FindDanhMucHangMuc(item.IdDuAnHangMuc);
                if (danhMucHangMuc != null)
                {
                    _mapper.Map(item, danhMucHangMuc);
                    _approveProjectService.UpdateVDTDanhMucHangMuc(danhMucHangMuc);
                }
            }
        }

        private void UpdateQDHangMucDetail(List<ApproveProjectDetailModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtDaQddauTuHangMuc qdDauTuHangMuc = _approveProjectService.FindQDDTHangMuc(item.IdQDHangMuc);
                if (qdDauTuHangMuc != null)
                {
                    _mapper.Map(item, qdDauTuHangMuc);
                    _approveProjectService.UpdateQDDTHangMuc(qdDauTuHangMuc);
                }
            }
        }

        private void DeleteDetail(List<ApproveProjectDetailModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _approveProjectService.DeleteQDDTHangMuc(item.IdQDHangMuc.Value);
            }
        }

        private void UpdateDuAn()
        {
            // Update VDT_DA_DuAn
            VdtDaDuAn entityDuAn = _approveProjectService.FindDuAnById(Model.IIdDuAnId.Value);
            _mapper.Map(Model, entityDuAn);

            if (SelectedHinhThucQL != null)
            {
                entityDuAn.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
            }
            //entityDuAn.IIdChuDauTuId = Model.IIdChuDauTuId;
            entityDuAn.SKhoiCong = Model.SKhoiCong;
            entityDuAn.SKetThuc = Model.SKetThuc;
            entityDuAn.SDiaDiem = Model.SDiaDiem;
            entityDuAn.SUserUpdate = _sessionService.Current.Principal;
            entityDuAn.DDateUpdate = DateTime.Now;
            _approveProjectService.UpdateVdtDuAn(entityDuAn);
        }

        private bool ValiDateData()
        {
            List<string> lstError = new List<string>();
            if (SelectedLoaiQuyetDinh == null)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Loại quyết định"));
            }
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                lstError.Add(Resources.MsgCheckSoQD);
            }
            else
            {
                if (Model.Id != null && _approveProjectService.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
                {
                    lstError.Add(Resources.MsgTrungSoQD);
                }
            }
            if (!Model.DNgayQuyetDinh.HasValue)
            {
                lstError.Add(Resources.MsgCheckNgayPheDuyet);
            }
            //if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            //{
            //    lstError.Add(Resources.MsgCheckDonVi);
            //}

            if (SaveCommand == null || SelectedDuAn == null || string.IsNullOrEmpty(SelectedDuAn.ValueItem))
            {
                lstError.Add(Resources.MsgCheckDuAn);
            }

            if (string.IsNullOrEmpty(Model.SKhoiCong))
            {
                lstError.Add(Resources.MsgCheckThoiGianThucHien);
            }
            if (string.IsNullOrEmpty(Model.SKetThuc))
            {
                lstError.Add(Resources.MsgCheckThoiGianThucHien);
            }

            if (SelectedChuDauTu == null || string.IsNullOrEmpty(SelectedChuDauTu.ValueItem))
            {
                lstError.Add(Resources.MsgCheckChuDauTu);
            }

            if (!string.IsNullOrEmpty(Model.SKhoiCong) && !string.IsNullOrEmpty(Model.SKetThuc))
            {
                if (string.Compare(Model.SKhoiCong, Model.SKetThuc) > 0)
                    lstError.Add(string.Format(Resources.MsgCheckThoiGianBatDauKetThuc, "khởi công", "hoàn thành"));
            }

            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //Model.Id_DonVi = SelectedDonVi.ValueItem;
            //Model.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
            //Model.IIdDonViQuanLyId = Guid.Parse(SelectedDonVi.HiddenValue);
            if (SelectedDuAn != null)
                Model.IIdDuAnId = Guid.Parse(SelectedDuAn.ValueItem);

            if (SelectedNhomDuAn != null)
                Model.IIdNhomDuAnId = Guid.Parse(SelectedNhomDuAn.ValueItem);

            return true;
        }

        public bool ValidateDataDetail()
        {
            double fTongChiPhi = 0;
            double fTongNguonVon = 0;

            var listChiPhi = DataQDDauTuChiPhi.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0).ToList();
            var listNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted && x.GiaTriPheDuyet > 0 && x.IdNguonVon != 0).ToList();

            if (listNguonVon == null || listNguonVon.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (listChiPhi != null && listChiPhi.Count > 0)
                fTongChiPhi = listChiPhi.Where(x => x.IdChiPhiDuAnParent == null).Sum(x => x.GiaTriPheDuyet);
            if (listNguonVon != null && listNguonVon.Count > 0)
                fTongNguonVon = listNguonVon.Sum(x => x.GiaTriPheDuyet);

            if (fTongChiPhi != fTongNguonVon)
            {
                if (MessageBoxHelper.Confirm(Resources.MsgConfirmErrorChiPhiNotEqualNguonVon) == MessageBoxResult.Yes)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private void LoadDuAn()
        {
            if (SelectedDonVi != null)
            {
                IEnumerable<VdtDaDuAn> listDuAnByDonVi = _approveProjectService.FindDuAnByDonVi(SelectedDonVi.ValueItem);
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnByDonVi);
                OnPropertyChanged(nameof(DataDuAn));
            }
            if (Model.Id != Guid.Empty)
            {
                List<ProjectManagerQuery> listDuan = _projectManagerService.FindByCondition().ToList();
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuan);
            }
            OnPropertyChanged(nameof(DataDuAn));
        }

        private void GetDuAnById()
        {
            if (SelectedDuAn != null)
            {
                ProjectManagerQuery duAn = _projectManagerService.FindDuAnById(Guid.Parse(SelectedDuAn.ValueItem));
                if (duAn != null)
                {
                    Model.SDiaDiem = duAn.SDiaDiem;
                    Model.SKhoiCong = duAn.SKhoiCong;
                    Model.SKetThuc = duAn.SKetThuc;
                    Model.SMaDuAn = duAn.SMaDuAn;
                    Model.TenChuDauTu = duAn.TenChuDauTu;
                    Model.TenHinhThucQL = duAn.TenHinhThucQL;
                    if (duAn.IIdNhomDuAnId.HasValue)
                    {
                        VdtDmNhomDuAn nhomDuAn = _projectManagerService.FindNhomDuAnById(duAn.IIdNhomDuAnId.Value);
                        if (nhomDuAn != null)
                        {
                            Model.TenNhomDuAn = nhomDuAn.STenNhomDuAn;
                            Model.IIdNhomDuAnId = duAn.IIdNhomDuAnId;
                        }
                    }

                    VdtDaChuTruongDauTu chutruong = _vdtDaChuTruongDauTuService.FindByDuAnId(duAn.Id);
                    if (chutruong != null)
                    {
                        Model.FTongMucDauTuChuTruong = chutruong.FTmdtduKienPheDuyet;

                    }

                    //LoadDefaultDetailView();
                    LoadDataNguonVonByDuAn(duAn.Id);
                    LoadDataQDDauTuChiPhiDefault();
                    LoadDataChuTruongHangMuc();
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        private void LoadDataNguonVonByDuAn(Guid duAnId)
        {
            List<VdtDaQDNguonVonQuery> listQdNguonVonQuery = _approveProjectService.FindListQDDauTuNguonVonByDuAn(duAnId).ToList();
            DataQDDauTuNguonVon = _mapper.Map<ObservableCollection<VdtDaQddtNguonVonModel>>(listQdNguonVonQuery);
            foreach (var item in DataQDDauTuNguonVon)
            {
                item.PropertyChanged += DetailNguonVonModel_PropertyChanged;
            }
            CalculateConLaiNguonVon();
        }

        private void DeleteChildChiPhi(DuAnDetailModel item, Dictionary<Guid, List<DuAnDetailModel>> dicData)
        {
            item.IsDeleted = true;
            if (!dicData.ContainsKey(item.iIdChiPhi.Value)) return;
            foreach (var child in dicData[item.iIdChiPhi.Value])
            {
                DeleteChildChiPhi(child, dicData);
            }
        }

        private void LoadNguonVon()
        {
            IEnumerable<Core.Domain.NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
            DataNguonVon = _mapper.Map<List<ComboboxItem>>(listNguonVon);
        }

        private void CalculateConLaiNguonVon()
        {
            double tongNguonVon = 0;
            double tongChiPhi = 0;
            if (DataQDDauTuNguonVon != null && DataQDDauTuNguonVon.Count > 0)
            {
                tongNguonVon = DataQDDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.GiaTriPheDuyet);
                Model.FTongMucDauTuPheDuyet = tongNguonVon;
                OnPropertyChanged(nameof(Model));
            }

            if (DataQDDauTuChiPhi != null && DataQDDauTuChiPhi.Count > 0)
            {
                var listChiPhiCha = DataQDDauTuChiPhi.Where(x => x.IdChiPhiDuAnParent == null).ToList();
                tongChiPhi = listChiPhiCha.Sum(x => x.GiaTriPheDuyet);
            }
            ConLai = tongNguonVon - tongChiPhi;
        }

        private void CalculateDataChiPhi()
        {
            if (DataQDDauTuChiPhi == null) return;
            foreach (var item in DataQDDauTuChiPhi.Where(n => !n.IdChiPhiDuAnParent.HasValue))
            {
                CalculateParent(item);
            }
        }

        private void CalculateParent(VdtDaQddtChiPhiModel parentItem)
        {
            int childRow = 0;
            foreach (var item in DataQDDauTuChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn))
            {
                CalculateParent(item);
                childRow++;
            }
            if (childRow != 0)
                parentItem.GiaTriPheDuyet = DataQDDauTuChiPhi.Where(n => n.IdChiPhiDuAnParent.HasValue && n.IdChiPhiDuAnParent == parentItem.IdChiPhiDuAn && !n.IsDeleted).Sum(n => n.GiaTriPheDuyet);
        }

        public bool CheckChiPhiHaveHangMuc()
        {
            var item = SelectedChiPhi;
            if (item == null) return false;

            var lstData = GetListHangMucDetailByChiPhi(item.IdChiPhiDuAn.Value);
            if (lstData == null || !lstData.Any(n => n.GiaTriPheDuyet != 0))
            {
                return false;
            }
            return true;
        }

        private void LoadLoaiQuyetDinh()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LOAI_QD_PDDA.TypeName.PHE_DUYET_DU_AN,
                ValueItem = ((int)LOAI_QD_PDDA.Type.PHE_DUYET_DU_AN).ToString()
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = LOAI_QD_PDDA.TypeName.BC_KINH_TE_KY_THUAT,
                ValueItem = ((int)LOAI_QD_PDDA.Type.BC_KINH_TE_KY_THUAT).ToString()
            });
            ItemsLoaiQuyetDinh = new ObservableCollection<ComboboxItem>(lstData);
            if (Model.Id == Guid.Empty)
            {
                SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault();
            }
            else
            {
                SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault(n => n.ValueItem == Model.ILoaiQuyetDinh.ToString());
                if (SelectedLoaiQuyetDinh == null) SelectedLoaiQuyetDinh = ItemsLoaiQuyetDinh.FirstOrDefault();
            }
            OnPropertyChanged(nameof(SelectedLoaiQuyetDinh));
        }

        private void LoadDuAnByChuDauTu()
        {
            List<ProjectManagerQuery> listDuan = _projectManagerService.FindByCondition().ToList();

            if (SelectedChuDauTu != null)
            {
                var listDuAnByChuDauTu = listDuan.Where(x => x.IIDMaChuDauTuID == SelectedChuDauTu.HiddenValue && x.IdChuTruongDauTu.HasValue && !x.IdQdDauTu.HasValue);                
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnByChuDauTu);
                OnPropertyChanged(nameof(DataDuAn));
            }
            if (Model.Id != Guid.Empty)
            {
                DataDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuan);
            }

            OnPropertyChanged(nameof(DataDuAn));
        }


        private void OnExport(ExportType exportType)
        {
            try
            {
                if(SelectedDuAn is null) {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                BackgroundWorkerHelper.Run((s, e) =>
                {

                    List<VdtQDTChiPhiHangMucExportQuery> lstChiphi = new List<VdtQDTChiPhiHangMucExportQuery>();
                    List<VdtQDTChiPhiHangMucExportQuery> lstResult = new List<VdtQDTChiPhiHangMucExportQuery>();
                    //Lấy danh mục chi phi
                    int index = 1;
                    foreach (var cp in DataQDDauTuChiPhi)
                    {
                        VdtQDTChiPhiHangMucExportQuery result = new VdtQDTChiPhiHangMucExportQuery();
                        result.Muc = 1;
                        result.STT = ToRoman(index);
                        result.SLoai = "CP";
                        result.sTenHangMucCP = cp.TenChiPhi;
                        result.IsHangCha = true;
                        result.fPheDuyetDA = cp.GiaTriPheDuyet;
                        result.IdChiPhi = cp.IdChiPhi;
                        result.IdDAChiPhi = cp.IdChiPhiDuAn;
                        result.SMaChiPhi = cp.SMaChiPhi;
                        lstChiphi.Add(result);
                        index++;
                    }
                    List<ApproveProjectDetailModel> lstDataHangMuc = new List<ApproveProjectDetailModel>();
                    if(Model.Id != Guid.Empty || IsDieuChinhPDDA)
                    {
                        lstDataHangMuc = DataQDDTHangMuc;
                    }
                    else
                    {
                        lstDataHangMuc = DataChuTruongHangMuc;
                    }    
                    //Add hạng mục cho từng loại chi phí
                    foreach (var item in lstChiphi)
                    {
                        lstResult.Add(item);
                        if((Model.Id != Guid.Empty || IsDieuChinhPDDA))
                        {
                            foreach (var hm in lstDataHangMuc.Where(x=> x.IdDuAnChiPhi == item.IdDAChiPhi).ToList())
                            {
                                VdtQDTChiPhiHangMucExportQuery hmModel = new VdtQDTChiPhiHangMucExportQuery();
                                hmModel.STT = hm.MaOrDer;
                                hmModel.SLoai = "HM";
                                hmModel.fPheDuyetDA = hm.GiaTriPheDuyet;
                                hmModel.sTenHangMucCP = hm.TenHangMuc;
                                if (hm.HangMucParentId == null)
                                {
                                    hmModel.IsHangCha = true;
                                }
                                hmModel.IdChiPhi = item.IdChiPhi;
                                hmModel.IdDAChiPhi = item.IdDAChiPhi;
                                lstResult.Add(hmModel);
                            }
                        }
                        else
                        {
                            foreach (var hm in lstDataHangMuc)
                            {
                                VdtQDTChiPhiHangMucExportQuery hmModel = new VdtQDTChiPhiHangMucExportQuery();
                                hmModel.STT = hm.MaOrDer;
                                hmModel.SLoai = "HM";
                                hmModel.sTenHangMucCP = hm.TenHangMuc;
                                if (hm.HangMucParentId == null)
                                {
                                    hmModel.IsHangCha = true;
                                }
                                hmModel.IdChiPhi = item.IdChiPhi;
                                hmModel.IdDAChiPhi = item.IdDAChiPhi;
                                lstResult.Add(hmModel);
                            }
                        }    
                       
                    }    

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Items", lstResult);


                    string templateFileName = Path.Combine(ExportPrefix.PATH_VDT_PDDA, MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_PHEDUYETDA);
                    string fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONGTINCHIPHI_PHEDUYETDA);
                    string fileNamePrefix = string.Format("Template Import hạng mục PDDA");
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

                ImportPheDuyetDuAnViewModel.Init();
                ImportPheDuyetDuAnViewModel.DataQDDTHangMucByChiPhiImport = new ObservableCollection<ApproveProjectDetailModel>();
                ImportPheDuyetDuAnViewModel.DataQDDauTuChiPhiImport = DataQDDauTuChiPhi;
                ImportPheDuyetDuAnViewModel.SavedAction = obj =>
                {
                    IsActiveImport = true;
                    this.LoadDataQDDauTuHangMucImportSave();
                };
                ImportPheDuyetDuAnViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadDataQDDauTuHangMucImportSave()
        {
            foreach(var dtcp in DataQDDauTuChiPhi)
            {
                if (ImportPheDuyetDuAnViewModel.DataQDDTHangMucByChiPhiImport.Where(x => x.IdDuAnChiPhi == dtcp.IdChiPhiDuAn && x.TenLoaiCT == "HM" && x.HangMucParentId == null).Count() > 0)
                {
                    dtcp.GiaTriPheDuyet = ImportPheDuyetDuAnViewModel.DataQDDTHangMucByChiPhiImport.Where(x => x.IdDuAnChiPhi == dtcp.IdChiPhiDuAn && x.TenLoaiCT == "HM" && x.HangMucParentId == null).Sum(x => x.GiaTriPheDuyet) ?? 0;
                }
                else
                {
                    dtcp.GiaTriPheDuyet = ImportPheDuyetDuAnViewModel.DataQDDTHangMucByChiPhiImport.Where(x => x.IdDuAnChiPhi == dtcp.IdChiPhiDuAn && x.TenLoaiCT == "CP").Sum(x => x.GiaTriPheDuyet) ?? 0;
                }    
                
            }
            if(SelectedChiPhi != null)
            {
                DataQDDTHangMuc = DataQDDTHangMuc.Where(x => x.IdDuAnChiPhi != SelectedChiPhi.IdChiPhiDuAn).ToList();
            }    
           
            //DataQDDTHangMuc.AddRange(DataQDDTHangMucByChiPhi.Where(n => (n.GiaTriPheDuyet ?? 0) != 0).ToList());
            DataQDDTHangMuc = new List<ApproveProjectDetailModel>();
            DataQDDTHangMuc.AddRange(ImportPheDuyetDuAnViewModel.DataQDDTHangMucByChiPhiImport.Where(x=> x.TenLoaiCT =="HM").ToList());
            UpdateListChiPhiCanEditGiaTriChiPhi();
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
        #endregion
    }
}
