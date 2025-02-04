using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.Initialization.InitializationProject
{
    public class InitializationProjectDialogViewModel : DialogViewModelBase<InitializationProjectDialogModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly ILog _logger;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IVdtKtKhoiTaoService _vdtKtKhoiTaoService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtDaDuToanService _vdtDaDuToanService;

        public Action<VTS.QLNS.CTC.App.Model.InitializationProjectModel> SavedAction;
        public override string Name => "Khởi tạo dự án";
        public override string Title => "Khởi tạo dự án";
        public override string Description => "Nhập thông tin khởi tạo dự án";
        public override Type ContentType => typeof(View.Investment.Initialization.InitializationProject.InitializationProjectDialog);
        public override PackIconKind IconKind => PackIconKind.Projector;
        public List<VdtDaDuAnQuery> ListDuAn;

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public bool IsEnableDropdown => IsAdd && _isUsingExistProject;
        public bool IsUpdateDuAn => !_isUsingExistProject;
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
                SetProperty(ref _selectedDonVi, value);
                if (_selectedDonVi != null && !string.IsNullOrEmpty(_selectedDonVi.ValueItem) && IsUsingExistProject)
                    LoadComboboxDuAn(_selectedDonVi.ValueItem);
            }
        }

        private ObservableCollection<ComboboxItem> _dataChuDauTu;
        public ObservableCollection<ComboboxItem> DataChuDauTu
        {
            get => _dataChuDauTu;
            set => SetProperty(ref _dataChuDauTu, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                SetProperty(ref _selectedChuDauTu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DataLoaiCongTrinh
        {
            get => _dataLoaiCongTrinh;
            set => SetProperty(ref _dataLoaiCongTrinh, value);
        }

        private ComboboxItem _selectedLoaiCongTrinh;
        public ComboboxItem SelectedLoaiCongTrinh
        {
            get => _selectedLoaiCongTrinh;
            set
            {
                SetProperty(ref _selectedLoaiCongTrinh, value);
            }
        }

        private ObservableCollection<ComboboxItem> _dataPhanCapPheDuyet;
        public ObservableCollection<ComboboxItem> DataPhanCapPheDuyet
        {
            get => _dataPhanCapPheDuyet;
            set => SetProperty(ref _dataPhanCapPheDuyet, value);
        }

        private ComboboxItem _selectedPhanCapPheDuyet;
        public ComboboxItem SelectedPhanCapPheDuyet
        {
            get => _selectedPhanCapPheDuyet;
            set
            {
                SetProperty(ref _selectedPhanCapPheDuyet, value);
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
                SetProperty(ref _selectedDuAn, value);
                if (IsUsingExistProject && _selectedDuAn != null)
                {
                    LoadInfo();
                }
            }
        }

        private bool _isUsingExistProject;
        public bool IsUsingExistProject
        {
            get => _isUsingExistProject;
            set
            {
                SetProperty(ref _isUsingExistProject, value);
                if (_selectedDonVi != null && !string.IsNullOrEmpty(_selectedDonVi.ValueItem) && IsUsingExistProject)
                    LoadComboboxDuAn(_selectedDonVi.ValueItem);
                if (!_isUsingExistProject)
                {
                    ResetInfo();
                }
                OnPropertyChanged(nameof(IsEnableDropdown));
                OnPropertyChanged(nameof(IsUpdateDuAn));
                OnPropertyChanged(nameof(SelectedLoaiCongTrinh));
                OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
                OnPropertyChanged(nameof(SelectedChuDauTu));
                OnPropertyChanged(nameof(SelectedDuAn));
            }
        }

        private VTS.QLNS.CTC.App.Model.ProjectManagerModel _projectManager;
        public VTS.QLNS.CTC.App.Model.ProjectManagerModel ProjectManager
        {
            get => _projectManager;
            set => SetProperty(ref _projectManager, value);
        }

        private string _soQdQdDauTu;
        public string SoQdQdDauTu
        {
            get => _soQdQdDauTu;
            set => SetProperty(ref _soQdQdDauTu, value);
        }

        private DateTime? _ngayDuyetQDDT;
        public DateTime? NgayDuyetQDDT
        {
            get => _ngayDuyetQDDT;
            set => SetProperty(ref _ngayDuyetQDDT, value);
        }

        private double _giaTriDauTu;
        public double GiaTriDauTu
        {
            get => _giaTriDauTu;
            set => SetProperty(ref _giaTriDauTu, value);
        }

        private string _soQdTKDT;
        public string SoQdTKDT
        {
            get => _soQdTKDT;
            set => SetProperty(ref _soQdTKDT, value);
        }

        private DateTime? _ngayTKDT;
        public DateTime? NgayTKDT
        {
            get => _ngayTKDT;
            set => SetProperty(ref _ngayTKDT, value);
        }

        private double _giaTriDuToan;
        public double GiaTriDuToan
        {
            get => _giaTriDuToan;
            set => SetProperty(ref _giaTriDuToan, value);
        }

        public InitializationProjectDialogViewModel(INsDonViService nsDonViService,
                                              ISessionService sessionService,
                                              IVdtDaDuAnService vdtDaDuAnService,
                                              IProjectManagerService projectManagerService,
                                              IVdtKtKhoiTaoService vdtKtKhoiTaoService,
                                              ILog logger,
                                              IApproveProjectService approveProjectService,
                                              IVdtDaDuToanService vdtDaDuToanService,
                                              IMapper mapper)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _projectManagerService = projectManagerService;
            _vdtKtKhoiTaoService = vdtKtKhoiTaoService;
            _approveProjectService = approveProjectService;
            _vdtDaDuToanService = vdtDaDuToanService;
            _mapper = mapper;
            _logger = logger;
        }

        private void ResetCondition()
        {
            if (Model == null)
                Model = new InitializationProjectDialogModel();
            Model.INamKhoiTao = 0;
            if (DataDonVi != null && DataDonVi.Count > 0)
            {
                SelectedDonVi = DataDonVi.FirstOrDefault();
            }
            IsUsingExistProject = false;
            ProjectManager = new ProjectManagerModel();
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                SelectedDuAn = DataDuAn.FirstOrDefault();
            }

            if (DataChuDauTu != null && DataChuDauTu.Count > 0)
            {
                SelectedChuDauTu = DataChuDauTu.FirstOrDefault();
            }

            if (DataLoaiCongTrinh != null && DataLoaiCongTrinh.Count > 0)
            {
                SelectedLoaiCongTrinh = DataLoaiCongTrinh.FirstOrDefault();
            }

            if (DataPhanCapPheDuyet != null && DataPhanCapPheDuyet.Count > 0)
            {
                SelectedPhanCapPheDuyet = DataPhanCapPheDuyet.FirstOrDefault();
            }
            SoQdQdDauTu = string.Empty;
            NgayDuyetQDDT = null;
            GiaTriDauTu = 0;
            SoQdTKDT = string.Empty;
            NgayTKDT = null;
            GiaTriDuToan = 0;
            OnPropertyChanged(nameof(Model.INamKhoiTao));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(IsUsingExistProject));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedLoaiCongTrinh));
            OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            OnPropertyChanged(nameof(SoQdQdDauTu));
            OnPropertyChanged(nameof(NgayDuyetQDDT));
            OnPropertyChanged(nameof(GiaTriDauTu));
            OnPropertyChanged(nameof(SoQdTKDT));
            OnPropertyChanged(nameof(NgayTKDT));
            OnPropertyChanged(nameof(GiaTriDuToan));
        }

        public override void Init()
        {
            try
            {
                ResetCondition();
                LoadDonVi();
                LoadPhanCapPheDuyet();
                LoadLoaiCongTrinh();
                IsAdd = false;
                OnPropertyChanged(nameof(ProjectManager.SMaDuAn));
                if (Model == null || Model.Id == Guid.Empty)
                {
                    IsAdd = true;
                    Model = new InitializationProjectDialogModel();
                    InitCombobox();
                    ProjectManager = new ProjectManagerModel();
                    //IsAdd = true;
                }
                else
                {
                    //IsAdd = false;
                    VdtKtKhoiTao entityKhoiTao = _vdtKtKhoiTaoService.Find(Model.Id);
                    IsUsingExistProject = entityKhoiTao.BIsDuAnCu;
                    if (!IsUsingExistProject)
                    {
                        IsAdd = true;
                    }
                    if (entityKhoiTao != null)
                    {
                        Model = _mapper.Map<InitializationProjectDialogModel>(entityKhoiTao);
                        ProjectManager = new ProjectManagerModel();

                        if (DataDonVi != null && DataDonVi.Count > 0 && entityKhoiTao.IIdDonViId.HasValue)
                        {
                            SelectedDonVi = DataDonVi.Where(n => n.HiddenValue == entityKhoiTao.IIdDonViId.ToString()).FirstOrDefault();
                        }
                        LoadComboboxDuAn(_selectedDonVi.ValueItem);
                        if (SelectedDonVi != null && DataDuAn != null && DataDuAn.Count > 0 && entityKhoiTao.IIdDuAnId.HasValue)
                        {
                            SelectedDuAn = DataDuAn.Where(n => n.HiddenValue == entityKhoiTao.IIdDuAnId.ToString()).FirstOrDefault();
                        }
                        if (SelectedDuAn != null && !string.IsNullOrEmpty(SelectedDuAn.HiddenValue))
                        {
                            LoadInfo();
                        }
                        if (SelectedDuAn == null || SelectedDonVi == null)
                        {
                            InitCombobox();
                        }
                    }
                    else
                    {
                        InitCombobox();
                        ProjectManager = new ProjectManagerModel();
                    }
                }
                OnPropertyChanged(nameof(IsAdd));
                OnPropertyChanged(nameof(IsEnableDropdown));
                OnPropertyChanged(nameof(IsUpdateDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void InitCombobox()
        {
            if (DataDonVi != null && DataDonVi.Count > 0)
                SelectedDonVi = DataDonVi.FirstOrDefault();
            if (DataChuDauTu != null && DataChuDauTu.Count > 0)
                SelectedChuDauTu = DataChuDauTu.FirstOrDefault();
            if (DataLoaiCongTrinh != null && DataLoaiCongTrinh.Count > 0)
                SelectedLoaiCongTrinh = DataLoaiCongTrinh.FirstOrDefault();
            if (DataPhanCapPheDuyet != null && DataPhanCapPheDuyet.Count > 0)
                SelectedPhanCapPheDuyet = DataPhanCapPheDuyet.FirstOrDefault();
        }

        private bool ValiDateDataProject(bool isUpdate = false)
        {
            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDonVi, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(ProjectManager.STenDuAn))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTenDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(ProjectManager.SMaDuAn))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckMaDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (!isUpdate && ProjectManager.Id != null && _projectManagerService.CheckDuplicateMaDuAn(ProjectManager.SMaDuAn, ProjectManager.Id))
                {
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungMaDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (SelectedLoaiCongTrinh == null || string.IsNullOrEmpty(SelectedLoaiCongTrinh.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckLoaiCongTrinh, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedPhanCapPheDuyet == null || string.IsNullOrEmpty(SelectedPhanCapPheDuyet.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckPhanCapPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValiDateDataInitialization()
        {
            if (Model.INamKhoiTao <= 0)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNamKhoiTao, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedDonVi == null || string.IsNullOrEmpty(SelectedDonVi.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckDonVi, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (IsUsingExistProject && (SelectedDuAn == null || string.IsNullOrEmpty(SelectedDuAn.HiddenValue)))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTenDuAn, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedLoaiCongTrinh == null || string.IsNullOrEmpty(SelectedLoaiCongTrinh.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckLoaiCongTrinh, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (SelectedPhanCapPheDuyet == null || string.IsNullOrEmpty(SelectedPhanCapPheDuyet.ValueItem))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckPhanCapPheDuyet, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValiDateQDDauTu()
        {
            if (string.IsNullOrEmpty(SoQdQdDauTu))
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckSoQDDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (NgayDuyetQDDT == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgCheckNgayQDDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public override void OnSave()
        {
            try
            {
                DonVi donVi = _nsDonViService.FindByIdDonVi(SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork);
                DonVi chuDauTu = _nsDonViService.FindByIdDonVi(SelectedChuDauTu.ValueItem, _sessionService.Current.YearOfWork);
                VdtKtKhoiTao entityKhoiTao;
                VdtDaDuAn entityDuAn = new VdtDaDuAn();
                VdtDaQddauTu entityQDDT = new VdtDaQddauTu();
                VdtDaDuToan entityDuToan = new VdtDaDuToan();
                //VdtDaQddauTuNguonVon entityDauTuNguonVon = new VdtDaQddauTuNguonVon();
                if (Model == null || Model.Id == Guid.Empty)
                {
                    //add
                    if (!IsUsingExistProject)
                    {
                        if (SelectedDonVi != null && SelectedDonVi.ValueItem != null)
                        {
                            if (donVi != null)
                            {
                                ProjectManager.IIdDonViQuanLyId = donVi.Id;
                                ProjectManager.IIdMaDonViQuanLy = donVi.IIDMaDonVi;
                            }
                        }
                        if (SelectedChuDauTu != null && SelectedChuDauTu.ValueItem != null)
                        {

                            if (chuDauTu != null)
                            {
                                ProjectManager.IIdChuDauTuId = chuDauTu.Id;
                                ProjectManager.IIdMaChuDauTuId = chuDauTu.IIDMaDonVi;
                            }
                        }
                        if (SelectedPhanCapPheDuyet != null && SelectedPhanCapPheDuyet.ValueItem != null)
                        {
                            ProjectManager.IIdCapPheDuyetId = Guid.Parse(SelectedPhanCapPheDuyet.ValueItem);
                        }
                        if (SelectedLoaiCongTrinh != null && SelectedLoaiCongTrinh.ValueItem != null)
                        {
                            ProjectManager.IIdLoaiCongTrinhId = Guid.Parse(SelectedLoaiCongTrinh.ValueItem);
                        }
                        if (!ValiDateDataProject() || !ValiDateDataInitialization() || !ValiDateQDDauTu())
                        {
                            return;
                        }

                        entityDuAn = _mapper.Map<VdtDaDuAn>(ProjectManager);
                        entityDuAn.DDateCreate = DateTime.Now;
                        entityDuAn.SUserCreate = _sessionService.Current.Principal;
                        _projectManagerService.Add(entityDuAn);

                        //add quyet dinh dau tu
                        entityQDDT.SSoQuyetDinh = SoQdQdDauTu;
                        entityQDDT.DNgayQuyetDinh = NgayDuyetQDDT;
                        entityQDDT.IIdDuAnId = entityDuAn.Id;
                        entityQDDT.FTongMucDauTuPheDuyet = GiaTriDauTu;
                        entityQDDT.BActive = true;
                        entityQDDT.BIsGoc = true;
                        entityQDDT.DDateCreate = DateTime.Now;
                        entityQDDT.SUserCreate = _sessionService.Current.Principal;
                        _approveProjectService.Add(entityQDDT);

                        //add quyet dinh dau tu nguon von
                        //entityDauTuNguonVon.IIdQddauTuId = entityQDDT.Id;
                        //entityDauTuNguonVon.IIdNguonVonId = 1;
                        //_approveProjectService.Add(entityDauTuNguonVon);
                        //add du toan
                        if (!string.IsNullOrEmpty(SoQdTKDT) && NgayTKDT != null)
                        {
                            entityDuToan.SSoQuyetDinh = SoQdTKDT;
                            entityDuToan.DNgayQuyetDinh = NgayTKDT;
                            entityDuToan.FTongDuToanPheDuyet = GiaTriDuToan;
                            entityDuToan.IIdDuAnId = entityDuAn.Id;
                            entityDuToan.BActive = true;
                            entityDuToan.BIsGoc = true;
                            entityDuToan.DDateCreate = DateTime.Now;
                            entityDuToan.SUserCreate = _sessionService.Current.Principal;
                            _vdtDaDuToanService.Add(entityDuToan);
                        }
                    }
                    if (IsUsingExistProject && !ValiDateDataInitialization())
                    {
                        return;
                    }
                    entityKhoiTao = new VdtKtKhoiTao();
                    entityKhoiTao = _mapper.Map<VdtKtKhoiTao>(Model);
                    entityKhoiTao.DDateCreate = DateTime.Now;
                    entityKhoiTao.IIdDonViId = donVi.Id;
                    entityKhoiTao.BIsDuAnCu = IsUsingExistProject;
                    entityKhoiTao.IIdMaDonVi = donVi.IIDMaDonVi;
                    if (!IsUsingExistProject && entityDuAn != null && entityDuAn.Id != Guid.Empty)
                    {
                        entityKhoiTao.IIdDuAnId = entityDuAn.Id;
                    }
                    else
                    {
                        entityKhoiTao.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                    }
                    if (!IsUsingExistProject && entityQDDT != null && entityQDDT.Id != Guid.Empty)
                    {
                        entityKhoiTao.IIdQddauTuId = entityQDDT.Id;
                    }
                    if (!IsUsingExistProject && entityDuToan != null && entityDuToan.Id != Guid.Empty)
                    {
                        entityKhoiTao.IIdDuToanId = entityDuToan.Id;
                    }
                    entityKhoiTao.SUserCreate = _sessionService.Current.Principal;
                    _vdtKtKhoiTaoService.Add(entityKhoiTao);
                }
                else
                {
                    if (!IsUsingExistProject)
                    {
                        if (SelectedDonVi != null && SelectedDonVi.ValueItem != null)
                        {
                            if (donVi != null)
                            {
                                ProjectManager.IIdDonViQuanLyId = donVi.Id;
                                ProjectManager.IIdMaDonViQuanLy = donVi.IIDMaDonVi;
                            }
                        }
                        if (SelectedChuDauTu != null && SelectedChuDauTu.ValueItem != null)
                        {

                            if (chuDauTu != null)
                            {
                                ProjectManager.IIdChuDauTuId = chuDauTu.Id;
                                ProjectManager.IIdMaChuDauTuId = chuDauTu.IIDMaDonVi;
                            }
                        }
                        if (SelectedPhanCapPheDuyet != null && SelectedPhanCapPheDuyet.ValueItem != null)
                        {
                            ProjectManager.IIdCapPheDuyetId = Guid.Parse(SelectedPhanCapPheDuyet.ValueItem);
                        }
                        if (SelectedLoaiCongTrinh != null && SelectedLoaiCongTrinh.ValueItem != null)
                        {
                            ProjectManager.IIdLoaiCongTrinhId = Guid.Parse(SelectedLoaiCongTrinh.ValueItem);
                        }
                        // update project
                        if (!ValiDateDataProject(true) || !ValiDateQDDauTu())
                        {
                            return;
                        }
                        entityDuAn = _projectManagerService.FindById(Model.IIdDuAnId.Value);
                        entityDuAn.SKhoiCong = ProjectManager.SKhoiCong;
                        entityDuAn.SKetThuc = ProjectManager.SKetThuc;
                        entityDuAn.IIdDonViQuanLyId = donVi.Id;
                        entityDuAn.IIdMaDonViQuanLy = donVi.IIDMaDonVi;
                        entityDuAn.IIdChuDauTuId = chuDauTu.Id;
                        entityDuAn.IIdMaChuDauTuId = chuDauTu.IIDMaDonVi;
                        entityDuAn.IIdCapPheDuyetId = Guid.Parse(SelectedPhanCapPheDuyet.ValueItem);
                        entityDuAn.IIdLoaiCongTrinhId = Guid.Parse(SelectedLoaiCongTrinh.ValueItem);
                        entityDuAn.DDateUpdate = DateTime.Now;
                        entityDuAn.SUserUpdate = _sessionService.Current.Principal;
                        _projectManagerService.Update(entityDuAn);
                        if (Model.IIdQddauTuId.HasValue)
                        {
                            entityQDDT = _approveProjectService.FindById(Model.IIdQddauTuId.Value);
                            if (entityQDDT != null)
                            {
                                entityQDDT.SSoQuyetDinh = SoQdQdDauTu;
                                entityQDDT.DNgayQuyetDinh = NgayDuyetQDDT;
                                entityQDDT.IIdDuAnId = entityDuAn.Id;
                                entityQDDT.FTongMucDauTuPheDuyet = GiaTriDauTu;
                                entityQDDT.DDateUpdate = DateTime.Now;
                                entityQDDT.SUserUpdate = _sessionService.Current.Principal;
                                _approveProjectService.Update(entityQDDT);
                            }
                        }

                        if (Model.IIdDuToanId.HasValue && !string.IsNullOrEmpty(SoQdTKDT) && NgayTKDT != null)
                        {
                            entityDuToan = _vdtDaDuToanService.FindById(Model.IIdDuToanId.Value);
                            if (entityDuToan != null)
                            {
                                entityDuToan.SSoQuyetDinh = SoQdTKDT;
                                entityDuToan.DNgayQuyetDinh = NgayTKDT;
                                entityDuToan.FTongDuToanPheDuyet = GiaTriDuToan;
                                entityDuToan.IIdDuAnId = entityDuAn.Id;
                                entityDuToan.DDateUpdate = DateTime.Now;
                                entityDuToan.SUserUpdate = _sessionService.Current.Principal;
                                _vdtDaDuToanService.Update(entityDuToan);
                            }
                        }
                    }

                    //update khoi tao
                    if (!ValiDateDataInitialization())
                    {
                        return;
                    }
                    entityKhoiTao = _vdtKtKhoiTaoService.Find(Model.Id);
                    if (entityKhoiTao != null)
                    {
                        _mapper.Map(Model, entityKhoiTao);
                        entityKhoiTao.IIdDonViId = donVi.Id;
                        entityKhoiTao.IIdMaDonVi = donVi.IIDMaDonVi;
                        entityKhoiTao.BIsDuAnCu = IsUsingExistProject;
                        if (!IsUsingExistProject && entityDuAn != null && entityDuAn.Id != Guid.Empty)
                        {
                            entityKhoiTao.IIdDuAnId = entityDuAn.Id;
                        }
                        else
                        {
                            entityKhoiTao.IIdDuAnId = Guid.Parse(SelectedDuAn.HiddenValue);
                        }

                        if (!IsUsingExistProject && entityQDDT != null && entityQDDT.Id != Guid.Empty)
                        {
                            entityKhoiTao.IIdQddauTuId = entityQDDT.Id;
                        }
                        entityKhoiTao.DDateUpdate = DateTime.Now;
                        entityKhoiTao.SUserUpdate = _sessionService.Current.Principal;
                        _vdtKtKhoiTaoService.Update(entityKhoiTao);
                    }
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<VTS.QLNS.CTC.App.Model.InitializationProjectModel>(entityKhoiTao));
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
                _selectedDuAn = null;
                return;
            }
            ListDuAn = _vdtDaDuAnService.FindByIdDonVi(idDonVi).ToList();
            DataDuAn = new ObservableCollection<ComboboxItem>();
            foreach (VdtDaDuAnQuery item in ListDuAn)
            {
                DataDuAn.Add(new ComboboxItem { ValueItem = item.SMaDuAn, DisplayItem = string.Format("{0} - {1}", item.SMaDuAn, item.STenDuAn), HiddenValue = item.IID_DuAnID.ToString() });
            }
            if (DataDuAn != null && DataDuAn.Count > 0)
            {
                _selectedDuAn = DataDuAn.FirstOrDefault();
            }
            if (IsUsingExistProject)
            {
                LoadInfo();
            }
        }

        public void LoadInfo()
        {
            if (_selectedDuAn != null && _dataDuAn != null && _dataDuAn.Count > 0)
            {
                VdtDaDuAnQuery item = ListDuAn.Where(n => n.IID_DuAnID.ToString() == _selectedDuAn.HiddenValue).FirstOrDefault();
                if (item != null)
                {
                    if (_dataLoaiCongTrinh != null && _dataLoaiCongTrinh.Count > 0 && item.IID_LoaiCongTrinhID.HasValue && item.IID_LoaiCongTrinhID.Value != Guid.Empty)
                    {
                        SelectedLoaiCongTrinh = _dataLoaiCongTrinh.Where(n => n.ValueItem == item.IID_LoaiCongTrinhID.Value.ToString()).FirstOrDefault();
                    }
                    if (_dataPhanCapPheDuyet != null && _dataPhanCapPheDuyet.Count > 0 && item.IID_CapPheDuyetID.HasValue && item.IID_CapPheDuyetID.Value != Guid.Empty)
                    {
                        SelectedPhanCapPheDuyet = _dataPhanCapPheDuyet.Where(n => n.ValueItem == item.IID_CapPheDuyetID.Value.ToString()).FirstOrDefault();
                    }
                    if (_dataChuDauTu != null && _dataChuDauTu.Count > 0 && !string.IsNullOrEmpty(item.iID_MaChuDauTuID))
                    {
                        SelectedChuDauTu = _dataChuDauTu.Where(n => n.ValueItem == item.iID_MaChuDauTuID).FirstOrDefault();
                    }

                    if (!string.IsNullOrEmpty(_selectedDuAn.HiddenValue))
                    {
                        VdtDaQddauTu itemQDDT = _approveProjectService.FindByDuAnId(Guid.Parse(_selectedDuAn.HiddenValue));
                        if (itemQDDT != null)
                        {
                            SoQdQdDauTu = itemQDDT.SSoQuyetDinh;
                            NgayDuyetQDDT = itemQDDT.DNgayQuyetDinh;
                            GiaTriDauTu = itemQDDT.FTongMucDauTuPheDuyet.HasValue ? itemQDDT.FTongMucDauTuPheDuyet.Value : 0;
                        }
                        else
                        {
                            ResetInfoQDDT();
                        }
                    }
                    else
                    {
                        ResetInfoQDDT();
                    }
                    if (!string.IsNullOrEmpty(_selectedDuAn.HiddenValue))
                    {
                        VdtDaDuToan itemDuToan = _vdtDaDuToanService.FindByDuAnId(Guid.Parse(_selectedDuAn.HiddenValue));
                        if (itemDuToan != null)
                        {
                            SoQdTKDT = itemDuToan.SSoQuyetDinh;
                            NgayTKDT = itemDuToan.DNgayQuyetDinh;
                            GiaTriDuToan = itemDuToan.FTongDuToanPheDuyet.HasValue ? itemDuToan.FTongDuToanPheDuyet.Value : 0;
                        }
                        else
                        {
                            ResetInfoDuToan();
                        }
                    }
                    else
                    {
                        ResetInfoDuToan();
                    }
                    ProjectManager.STenDuAn = item.STenDuAn;
                    ProjectManager.SMaDuAn = item.SMaDuAn;
                    ProjectManager.SKhoiCong = item.SKhoiCong;
                    ProjectManager.SKetThuc = item.SKetThuc;
                    ProjectManager.SoQdQdDauTu = item.SoQDDauTu;
                    ProjectManager.NgayQdQdDauTu = item.NgayQDDauTu;
                    OnPropertyChanged(nameof(SelectedLoaiCongTrinh));
                    OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
                    OnPropertyChanged(nameof(SelectedChuDauTu));
                    OnPropertyChanged(nameof(ProjectManager.STenDuAn));
                    OnPropertyChanged(nameof(ProjectManager.SMaDuAn));
                    OnPropertyChanged(nameof(ProjectManager.SKhoiCong));
                    OnPropertyChanged(nameof(ProjectManager.SKetThuc));
                    OnPropertyChanged(nameof(ProjectManager.SoQdQdDauTu));
                    OnPropertyChanged(nameof(ProjectManager.NgayQdQdDauTu));
                }
                else
                {
                    ResetInfo();
                }
            }
        }

        public void ResetInfoQDDT()
        {
            SoQdQdDauTu = string.Empty;
            NgayDuyetQDDT = null;
            GiaTriDauTu = 0;

            OnPropertyChanged(nameof(SoQdQdDauTu));
            OnPropertyChanged(nameof(NgayDuyetQDDT));
            OnPropertyChanged(nameof(GiaTriDauTu));
        }

        public void ResetInfoDuToan()
        {
            SoQdTKDT = string.Empty;
            NgayTKDT = null;
            GiaTriDuToan = 0;

            OnPropertyChanged(nameof(SoQdTKDT));
            OnPropertyChanged(nameof(NgayTKDT));
            OnPropertyChanged(nameof(GiaTriDuToan));
        }

        public void ResetInfo()
        {
            if (ProjectManager == null)
                return;
            ProjectManager.STenDuAn = string.Empty;
            ProjectManager.SMaDuAn = string.Empty;
            ProjectManager.SKhoiCong = string.Empty;
            ProjectManager.SKetThuc = string.Empty;
            ProjectManager.SoQdQdDauTu = string.Empty;
            ProjectManager.NgayQdQdDauTu = null;

            if (_dataLoaiCongTrinh != null && _dataLoaiCongTrinh.Count > 0)
            {
                SelectedLoaiCongTrinh = _dataLoaiCongTrinh.FirstOrDefault();
            }
            if (_dataPhanCapPheDuyet != null && _dataPhanCapPheDuyet.Count > 0)
            {
                SelectedPhanCapPheDuyet = _dataPhanCapPheDuyet.FirstOrDefault();
            }
            if (_dataChuDauTu != null && _dataChuDauTu.Count > 0)
            {
                SelectedChuDauTu = _dataChuDauTu.FirstOrDefault();
            }

            OnPropertyChanged(nameof(SelectedLoaiCongTrinh));
            OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(ProjectManager.SMaDuAn));
            OnPropertyChanged(nameof(ProjectManager.SKhoiCong));
            OnPropertyChanged(nameof(ProjectManager.SKetThuc));
            OnPropertyChanged(nameof(ProjectManager.SoQdQdDauTu));
            OnPropertyChanged(nameof(ProjectManager.NgayQdQdDauTu));
            ResetInfoQDDT();
            ResetInfoDuToan();
        }

        private void LoadDonVi()
        {
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            _dataDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDonVi);
            _dataChuDauTu = _dataDonVi;
        }

        private void LoadLoaiCongTrinh()
        {
            IEnumerable<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _projectManagerService.GetAllDMLoaiCongTrinh();
            _dataLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        private void LoadPhanCapPheDuyet()
        {
            IEnumerable<VdtDmPhanCapDuAn> listPhanCap = _projectManagerService.GetAllPhanCapDuAn();
            _dataPhanCapPheDuyet = _mapper.Map<ObservableCollection<ComboboxItem>>(listPhanCap);
        }
    }
}
