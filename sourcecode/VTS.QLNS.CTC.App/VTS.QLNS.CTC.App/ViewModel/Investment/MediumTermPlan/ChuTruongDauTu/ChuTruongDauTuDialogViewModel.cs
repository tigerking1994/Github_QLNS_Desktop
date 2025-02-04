using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ChuTruongDauTu
{
    public class ChuTruongDauTuDialogViewModel : DialogAttachmentViewModelBase<ChuTruongDauTuModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IProjectManagerService _projectManagerService;
        private readonly IApproveProjectService _approveProjectService;
        private readonly IVdtDaChuTruongDauTuService _service;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private int currentRow = -1;
        private int _indexHangMucMax = 0;
        private static string[] lstDonViExclude = new string[] { "0" };

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.ChuTruongDauTu.ChuTruongDauTuDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_CHUTRUONG_DAUTU;
        public bool IsSaveData => ItemsHangMuc.Any(item => item.IsModified || item.IsDeleted);
        public bool IsAdd => Model.Id.IsNullOrEmpty() && IsNotViewDetail;
        public bool IsEditable => Model.Id.IsNullOrEmpty() || Model.BActive;
        public bool IsShowGiaTriTruocDieuChinh => IsDieuChinh || !Model.IIdParentId.IsNullOrEmpty();
        public string HeaderGiaTri => string.Format("Giá trị phê duyệt{0}", IsShowGiaTriTruocDieuChinh ? " (Sau điều chỉnh)" : "");

        public override void OnIsDieuChinhChanged()
        {
            base.OnIsDieuChinhChanged();
            OnPropertyChanged(nameof(IsShowGiaTriTruocDieuChinh));
            OnPropertyChanged(nameof(HeaderGiaTri));
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
                if (SetProperty(ref _selectedDuAn, value))
                {
                    LoadDuAnById();
                    OnPropertyChanged(nameof(Model.IIdDonViQuanLyId));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    if (SelectedDonVi != null)
                    {
                        Model.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
                        Model.IIdDonViQuanLyId = Guid.Parse(SelectedDonVi.HiddenValue);
                    }
                    LoadDuAn();
                }
            }
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
                SetProperty(ref _selectedChuDauTu, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsPhanCapPheDuyet;
        public ObservableCollection<ComboboxItem> ItemsPhanCapPheDuyet
        {
            get => _itemsPhanCapPheDuyet;
            set => SetProperty(ref _itemsPhanCapPheDuyet, value);
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

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNhomDuAn;
        public ObservableCollection<ComboboxItem> ItemsNhomDuAn
        {
            get => _itemsNhomDuAn;
            set => SetProperty(ref _itemsNhomDuAn, value);
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

        private ObservableCollection<ComboboxItem> _itemsHinhThucQL;
        public ObservableCollection<ComboboxItem> ItemsHinhThucQL
        {
            get => _itemsHinhThucQL;
            set => SetProperty(ref _itemsHinhThucQL, value);
        }

        private ComboboxItem _selectedHinhThucQL;
        public ComboboxItem SelectedHinhThucQL
        {
            get => _selectedHinhThucQL;
            set
            {
                SetProperty(ref _selectedHinhThucQL, value);
            }
        }

        private ObservableCollection<VdtDaHangMucModel> _itemsHangMuc;
        public ObservableCollection<VdtDaHangMucModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
        }

        private ObservableCollection<VdtDaChuTruongDTNguonVonModel> _itemsChuTruongNguonVon;
        public ObservableCollection<VdtDaChuTruongDTNguonVonModel> ItemsChuTruongNguonVon
        {
            get => _itemsChuTruongNguonVon;
            set => SetProperty(ref _itemsChuTruongNguonVon, value);
        }

        private VdtDaHangMucModel _selectedHangMuc;
        public VdtDaHangMucModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set
            {
                SetProperty(ref _selectedHangMuc, value);
            }
        }

        private VdtDaChuTruongDTNguonVonModel _selectedNguonVon;
        public VdtDaChuTruongDTNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucNguonVon;
        public ObservableCollection<ComboboxItem> ItemsDanhMucNguonVon
        {
            get => _itemsDanhMucNguonVon;
            set => SetProperty(ref _itemsDanhMucNguonVon, value);
        }

        private ComboboxItem _selectedLoaiCTHangMuc;
        public ComboboxItem SelectedLoaiCTHangMuc
        {
            get => _selectedLoaiCTHangMuc;
            set
            {
                SetProperty(ref _selectedLoaiCTHangMuc, value);
            }
        }

        private bool _isNotViewDetail;
        public bool IsNotViewDetail
        {
            get => _isNotViewDetail;
            set => SetProperty(ref _isNotViewDetail, value);
        }

        public RelayCommand AddDetailCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand DeleteDetailCommand { get; }
        public RelayCommand AddDetailNguonVonCommand { get; }
        public RelayCommand DeleteDetailNguonVonCommand { get; }

        public ChuTruongDauTuDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IApproveProjectService approveProjectService,
            IVdtDaChuTruongDauTuService vdtDaChuTruongDauTuService,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            IDmChuDauTuService chuDauTuService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _projectManagerService = projectManagerService;
            _approveProjectService = approveProjectService;
            _service = vdtDaChuTruongDauTuService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _chuDauTuService = chuDauTuService;

            AddDetailCommand = new RelayCommand(obj => OnAddHangMuc());
            AddChildCommand = new RelayCommand(obj => OnAddHangMucChild());
            DeleteDetailCommand = new RelayCommand(obj => OnDeleteHangMuc());
            AddDetailNguonVonCommand = new RelayCommand(obj => OnAddNguonVon());
            DeleteDetailNguonVonCommand = new RelayCommand(obj => OnDeleteNguonVon());
        }

        protected override void OnModelPropertyChanged()
        {
            base.OnModelPropertyChanged();
            OnPropertyChanged(nameof(IsAdd));
            OnPropertyChanged(nameof(IsEditable));
            OnPropertyChanged(nameof(IsShowGiaTriTruocDieuChinh));
            OnPropertyChanged(nameof(HeaderGiaTri));
        }

        public override void Init()
        {
            LoadAttach();
            LoadDonVi();
            LoadChuDauTu();
            LoadPhanCapPheDuyet();
            LoadLoaiCongTrinh();
            LoadNhomDuAn();
            LoadHinhThucQL();
            LoadNguonVon();
            LoadData();
            LoadDuAn();
        }

        private void LoadDonVi()
        {
            List<DonVi> listDonVi = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork).ToList();

            var drpItem = listDonVi.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString(), DisplayItem = (n.IIDMaDonVi + " - " + n.TenDonVi) });
            ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            var strUnitManager = _sessionService.Current.IdsDonViQuanLy;

            if (!string.IsNullOrEmpty(strUnitManager))
            {
                List<string> listUnitManager = strUnitManager.Split(",").ToList();
                ItemsDonVi = new ObservableCollection<ComboboxItem>(ItemsDonVi.Where(x => listUnitManager.Any(y => y.Contains(x.ValueItem))).ToList());
            }
            else
            {
                ItemsDonVi = new ObservableCollection<ComboboxItem>();
            }

            var idDonViCurrent = _sessionService.Current.IdDonVi;
            SelectedDonVi = ItemsDonVi.Where(x => x.ValueItem == idDonViCurrent).FirstOrDefault();
        }

        private void LoadChuDauTu()
        {
            //IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            IEnumerable<DmChuDauTu> listChuDauTu = _chuDauTuService.FindByAll();
            ItemsChuDauTu = _mapper.Map<ObservableCollection<ComboboxItem>>(listChuDauTu);
        }

        private void LoadPhanCapPheDuyet()
        {
            IEnumerable<VdtDmPhanCapDuAn> listPhanCap = _projectManagerService.GetAllPhanCapDuAn();
            ItemsPhanCapPheDuyet = _mapper.Map<ObservableCollection<ComboboxItem>>(listPhanCap);
        }

        private void LoadLoaiCongTrinh()
        {
            IEnumerable<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _projectManagerService.GetAllDMLoaiCongTrinh();
            ItemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        private void LoadNhomDuAn()
        {
            IEnumerable<VdtDmNhomDuAn> listNhomDuAn = _approveProjectService.GetAllNhomDuAn();
            ItemsNhomDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listNhomDuAn);
        }

        private void LoadHinhThucQL()
        {
            IEnumerable<VdtDmHinhThucQuanLy> listHinhThucQL = _approveProjectService.GetAllHinhThucQuanLy();
            ItemsHinhThucQL = _mapper.Map<ObservableCollection<ComboboxItem>>(listHinhThucQL);
        }

        private void LoadNguonVon()
        {
            IEnumerable<Core.Domain.NsNguonNganSach> listNguonVon = _approveProjectService.GetAllNguonNS();
            ItemsDanhMucNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(listNguonVon);
        }

        public override void LoadData(params object[] args)
        {
            BIsReadOnly = !IsNotViewDetail;
            base.LoadData(args);
            if (Model == null || Model.Id.IsNullOrEmpty())
            {
                Title = "Quản lý phê duyệt chủ trương đầu tư";
                IconKind = PackIconKind.PlaylistPlus;
                Name = "Thêm mới";
                Description = "Thêm mới thông tin phê duyệt chủ trương đầu tư";
                ItemsDuAn = new ObservableCollection<ComboboxItem>();
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                ChuTruongDauTuQuery chuTruongDauTuQuery = _service.FindChuTruongById(Model.Id);
                Model = _mapper.Map<ChuTruongDauTuModel>(chuTruongDauTuQuery);
                if (IsDieuChinh && IsNotViewDetail)
                {
                    Title = "Quản lý phê duyệt chủ trương đầu tư";
                    IconKind = PackIconKind.Adjust;
                    Name = "Điều chỉnh";
                    Description = "Điều chỉnh thông tin phê duyệt chủ trương đầu tư";
                    Model.SSoQuyetDinh = null;
                    Model.DNgayQuyetDinh = DateTime.Now;
                    OnPropertyChanged(nameof(Model));
                }
                else if (!IsNotViewDetail)
                {
                    Title = "Quản lý phê duyệt chủ trương đầu tư";
                    Name = "Chi tiết";
                    Description = "Xem chi tiết thông tin phê duyệt chủ trương đầu tư";
                }
                else
                {
                    Title = "Quản lý phê duyệt chủ trương đầu tư";
                    IconKind = PackIconKind.NoteEditOutline;
                    Name = "Cập nhật";
                    Description = "Cập nhật thông tin phê duyệt chủ trương đầu tư";
                }

                SelectedPhanCapPheDuyet = Model.IIdCapPheDuyetId.HasValue ? ItemsPhanCapPheDuyet.FirstOrDefault(x => x.ValueItem == Model.IIdCapPheDuyetId.ToString()) : null;
                SelectedNhomDuAn = ItemsNhomDuAn.FirstOrDefault(x => x.ValueItem == Model.IIdNhomDuAnId.ToString());
                SelectedHinhThucQL = Model.IIdHinhThucQuanLyId.HasValue ? ItemsHinhThucQL.FirstOrDefault(x => x.ValueItem == Model.IIdHinhThucQuanLyId.ToString()) : null;
                if (Model != null && Model.IIdMaDonViQuanLy != null)
                {
                    SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.ValueItem == Model.IIdMaDonViQuanLy);
                }
                if (Model.IIdDuAnId.HasValue && ItemsDuAn != null && ItemsDuAn.Count > 0)
                {
                    SelectedDuAn = ItemsDuAn.FirstOrDefault(x => x.ValueItem == Model.IIdDuAnId.Value.ToString());
                }
                if (!string.IsNullOrEmpty(Model.IIdMaChuDauTuId))
                {
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.ValueItem == Model.IIdChuDauTuId.ToString());
                }
            }
            LoadDataNguonVon();
            LoadDataHangMuc();
            _indexHangMucMax = _vdtDuAnHangMucService.FindNextSoChungTuIndex() - 1;
        }

        private void LoadDuAn()
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(base.Model.IIdDuAnId.Value);
                List<VdtDaDuAn> listDuAn = new List<VdtDaDuAn>();
                listDuAn.Add(duAn);
                ItemsDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAn);
                SelectedDuAn = ItemsDuAn.FirstOrDefault();
            }
            else
            {
                if (SelectedDonVi == null)
                {
                    return;
                }
                IEnumerable<VdtDaDuAn> listDuAnNotExsitsInChuTruong = _service.FindDuAnNotExistsInChuTruongDT(Model.Id, SelectedDonVi.ValueItem, _sessionService.Current.YearOfWork).OrderByDescending(x => x.DDateCreate);
                _itemsDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(listDuAnNotExsitsInChuTruong);
                OnPropertyChanged(nameof(ItemsDuAn));
            }
        }

        private void LoadDuAnById()
        {
            if (SelectedDuAn != null && Model.Id.IsNullOrEmpty())
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(Guid.Parse(SelectedDuAn.ValueItem));
                if (duAn != null)
                {
                    Model.SDiaDiem = duAn.SDiaDiem;
                    Model.SMucTieu = duAn.SMucTieu;
                    Model.SQuyMo = duAn.SQuyMo;
                    Model.SKhoiCong = duAn.SKhoiCong;
                    Model.SHoanThanh = duAn.SKetThuc;
                    Model.IIdDuAnId = duAn.Id;
                    Model.FTmdtduKienPheDuyet = duAn.FHanMucDauTu;
                    Model.IIdDonViQuanLyId = duAn.IIdDonViThucHienDuAnId;
                    Model.IIdDonViQuanLyId = (duAn.IIdDonViQuanLyId.HasValue && duAn.IIdDonViQuanLyId != Guid.Empty) ? duAn.IIdDonViQuanLyId.Value : Guid.Parse(SelectedDonVi.HiddenValue);
                    Model.IIdMaDonViQuanLy = duAn.IIdMaDonViQuanLy;
                    Model.IIdChuDauTuId = duAn.IIdChuDauTuId;
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.Id == duAn.IIdChuDauTuId);
                    SelectedNhomDuAn = ItemsNhomDuAn.FirstOrDefault(x => x.Id == duAn.IIdNhomDuAnId);
                    if (duAn.IIdCapPheDuyetId.HasValue)
                    {
                        SelectedPhanCapPheDuyet = ItemsPhanCapPheDuyet.FirstOrDefault(x => x.ValueItem == duAn.IIdCapPheDuyetId.ToString());
                    }
                    OnPropertyChanged(nameof(Model));
                    LoadDataNguonVonByDuAn(duAn.Id);
                }
            }
        }

        private void LoadDataHangMuc()
        {
            List<VdtDaHangMucQuery> listHangMuc = new List<VdtDaHangMucQuery>();
            if (!Model.Id.IsNullOrEmpty())
            {
                listHangMuc = _service.FindListDAHangMucDetailAfterSaveChuTruong(Model.Id).ToList();
            }
            else if (!Model.IIdDuAnId.IsNullOrEmpty())
            {
                listHangMuc = _service.FindListDAHangMucDetail(Model.IIdDuAnId.Value).ToList();
            }

            if (IsDieuChinh)
            {
                // Renew id điều chỉnh
                var refDictionary = listHangMuc.ToDictionary(x => x.Id, x => Guid.NewGuid());
                var refDictionaryDM = listHangMuc.ToDictionary(x => x.IdChuTruongHangMuc, x => Guid.NewGuid());
                listHangMuc = listHangMuc.Select(x =>
                {
                    x.Id = refDictionary[x.Id];
                    x.IdDuAnHangMuc = refDictionary[x.IdDuAnHangMuc];
                    if (x.IIdParentId != null)
                    {
                        x.IIdParentId = refDictionary[x.IIdParentId];
                    }
                    x.IdChuTruongHangMuc = refDictionaryDM[x.IdChuTruongHangMuc];
                    return x;
                }).ToList();
            }

            ItemsHangMuc = _mapper.Map<ObservableCollection<VdtDaHangMucModel>>(listHangMuc);
            if (ItemsHangMuc.Count > 0)
            {
                foreach (VdtDaHangMucModel model in ItemsHangMuc)
                {
                    model.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
        }

        private void LoadDataNguonVon()
        {
            List<VdtDaChuTruongDauTuNguonVonQuery> listNguonVon = new List<VdtDaChuTruongDauTuNguonVonQuery>();
            if (Model.Id != Guid.Empty)
            {
                listNguonVon = _service.FindListChuTruongNguonVonDetail(Model.Id).ToList();
                if (IsDieuChinh)
                {
                    // Renew id điều chỉnh
                    listNguonVon = listNguonVon.Select(x =>
                    {
                        // Reset id
                        x.Id = Guid.NewGuid();
                        x.IdChuTruongNguonVon = x.Id;

                        // Reset giá trị mới
                        x.GiaTriTruocDieuChinh = x.FTienPheDuyet;
                        return x;
                    }).ToList();
                }
            }

            ItemsChuTruongNguonVon = _mapper.Map<ObservableCollection<VdtDaChuTruongDTNguonVonModel>>(listNguonVon);
            if (ItemsChuTruongNguonVon.Count > 0)
            {
                foreach (VdtDaChuTruongDTNguonVonModel model in ItemsChuTruongNguonVon)
                {
                    model.PropertyChanged += DetailNguonVon_PropertyChanged;
                }
            }
        }

        private void LoadDataNguonVonByDuAn(Guid duAnId)
        {
            List<VdtDaChuTruongDauTuNguonVonQuery> listNguonVon = _service.FindListChuTruongDauTuNguonVonByDuAn(duAnId).ToList();
            ItemsChuTruongNguonVon = _mapper.Map<ObservableCollection<VdtDaChuTruongDTNguonVonModel>>(listNguonVon);
            if (ItemsChuTruongNguonVon.Count > 0)
            {
                Model.FTmdtduKienPheDuyet = ItemsChuTruongNguonVon.Sum(x => x.FTienPheDuyet);
                OnPropertyChanged(nameof(Model));
                foreach (VdtDaChuTruongDTNguonVonModel model in ItemsChuTruongNguonVon)
                {
                    model.PropertyChanged += DetailNguonVon_PropertyChanged;
                }
            }
            LoadDataHangMuc();
        }

        protected void OnAddHangMuc()
        {
            if (SelectedDuAn == null)
            {
                return;
            }
            if (SelectedHangMuc != null && SelectedHangMuc.IsDeleted)
            {
                SelectedHangMuc = null;
            }
            VdtDaHangMucModel targetItem = new VdtDaHangMucModel()
            {
                IdDuAnHangMuc = Guid.NewGuid(),
                SMaHangMuc = GetMaHangMuc(),
                MaOrDer = GetSTTHangMuc(false),
                IsHangCha = true,
                IsEditHangMuc = true,
                indexMaHangMuc = _indexHangMucMax
            };

            if (ItemsHangMuc != null && ItemsHangMuc.Count > 0)
            {
                if (SelectedHangMuc != null && !SelectedHangMuc.IsDeleted)
                {

                    VdtDaHangMucModel sourceItem = SelectedHangMuc;
                    targetItem = ObjectCopier.Clone(sourceItem);
                    if (!sourceItem.IsHangCha)
                    {
                        targetItem.IsHangCha = false;
                        targetItem.IIdParentId = sourceItem.IIdParentId;
                    }
                    else
                    {
                        targetItem.IIdParentId = null;
                    }
                    targetItem.Id = Guid.Empty;
                    targetItem.IdDuAnHangMuc = Guid.NewGuid();
                    targetItem.HanMucDT = 0;
                    targetItem.STenHangMuc = string.Empty;
                    targetItem.SMaHangMuc = GetMaHangMuc();
                    targetItem.indexMaHangMuc = _indexHangMucMax;
                    targetItem.IdChuTruongHangMuc = null;
                    targetItem.MaOrDer = GetSTTHangMuc(false);
                    targetItem.IsEditHangMuc = true;
                }
            }
            targetItem.PropertyChanged += DetailModel_PropertyChanged;
            ItemsHangMuc.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsHangMuc));
        }

        protected void OnAddHangMucChild()
        {
            if (ItemsHangMuc == null || ItemsHangMuc.Count == 0 || SelectedHangMuc == null)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgNotSelectHangMucParent, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedHangMuc != null && SelectedHangMuc.IsDeleted)
            {
                System.Windows.Forms.MessageBox.Show(Resources.MsgHMChaDelete, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSTT = GetSTTHangMuc(true);
            VdtDaHangMucModel sourceItem = SelectedHangMuc;
            VdtDaHangMucModel targetItem = ObjectCopier.Clone(sourceItem);
            sourceItem.IsHangCha = true;
            targetItem.Id = Guid.Empty;
            targetItem.IdDuAnHangMuc = Guid.NewGuid();
            targetItem.IIdParentId = sourceItem.IdDuAnHangMuc;
            targetItem.IdChuTruongHangMuc = null;
            targetItem.HanMucDT = 0;
            targetItem.IsHangCha = false;
            targetItem.SMaHangMuc = GetMaHangMuc();
            targetItem.STenHangMuc = string.Empty;
            targetItem.indexMaHangMuc = _indexHangMucMax;
            targetItem.MaOrDer = maSTT;
            targetItem.IsEditHangMuc = true;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;

            ItemsHangMuc.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsHangMuc));
        }

        private string GetSTTHangMuc(bool isAddChild = false)
        {
            string sttHangMuc = string.Empty;
            int inDexSTTHangMucLast = 1;
            if (SelectedHangMuc == null && isAddChild == false)
            {
                if (ItemsHangMuc.Count < 1)
                {
                    sttHangMuc = "1";
                    currentRow = -1;
                }
                else
                {
                    var hangMucItemLast = ItemsHangMuc.Where(x => x.IIdParentId == null).Last();
                    inDexSTTHangMucLast = Int32.Parse(hangMucItemLast.MaOrDer);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = ItemsHangMuc.IndexOf(ItemsHangMuc.Last());
                }
            }
            if (SelectedHangMuc != null && isAddChild == false)
            {
                // lấy giá trị mặc định là giá trị đầu tiên
                var hangMucLast = ItemsHangMuc.First();

                // tìm giá trị ngang hàng cuối cùng trong list => giá trị thêm mới được copy từ giá trị ngang hàng cuối cùng
                if (SelectedHangMuc.IIdParentId == null)
                {
                    hangMucLast = ItemsHangMuc.Last(x => x.IIdParentId == null && x.MaOrDer.Length == 1);
                    inDexSTTHangMucLast = Int32.Parse(hangMucLast.MaOrDer);
                    sttHangMuc = (inDexSTTHangMucLast + 1).ToString();
                    currentRow = ItemsHangMuc.Count - 1;
                }
                else
                {
                    hangMucLast = ItemsHangMuc.Last(x => x.IIdParentId == SelectedHangMuc.IIdParentId);
                    string sTTHangMucLast = hangMucLast.MaOrDer;
                    inDexSTTHangMucLast = Int32.Parse(sTTHangMucLast.Substring(sTTHangMucLast.Length - 1));
                    sttHangMuc = sTTHangMucLast.Substring(0, (sTTHangMucLast.Length - 1)) + (inDexSTTHangMucLast + 1).ToString();
                    currentRow = ItemsHangMuc.IndexOf(hangMucLast);
                }
            }
            if (SelectedHangMuc != null && isAddChild == true)
            {
                var listChild = ItemsHangMuc.Where(x => x.IIdParentId == SelectedHangMuc.IdDuAnHangMuc).ToList();
                if (listChild == null || listChild.Count == 0)
                {
                    sttHangMuc = SelectedHangMuc.MaOrDer + "_1";
                    currentRow = ItemsHangMuc.IndexOf(SelectedHangMuc);
                }
                else
                {
                    var hangMucChildLast = ItemsHangMuc.Last(x => x.IIdParentId == SelectedHangMuc.IdDuAnHangMuc);
                    string sTTHangMucLast = hangMucChildLast.MaOrDer;
                    if (string.IsNullOrEmpty(sTTHangMucLast))
                    {
                        sttHangMuc = SelectedHangMuc.MaOrDer + "_1";
                    }
                    List<string> arrayMaOrDer = sTTHangMucLast.Split("_").ToList();
                    if (arrayMaOrDer.Count > 0)
                    {
                        string maOrderOld = arrayMaOrDer.Last();
                        inDexSTTHangMucLast = Int32.Parse(maOrderOld) + 1;
                        arrayMaOrDer.RemoveAt(arrayMaOrDer.Count - 1);
                        arrayMaOrDer.Add(inDexSTTHangMucLast.ToString());
                        sttHangMuc = string.Join("_", arrayMaOrDer);
                    }

                    //tìm vị trí của dòng con cuối cùng của hạng mục ngang hàng cuối cùng
                    var listChildOfHangMucLast = FindListChildHangMuc(hangMucChildLast.IdDuAnHangMuc);
                    if (listChildOfHangMucLast == null || listChildOfHangMucLast.Count == 0)
                    {
                        currentRow = ItemsHangMuc.IndexOf(hangMucChildLast);
                    }
                    else
                    {
                        currentRow = ItemsHangMuc.IndexOf(listChildOfHangMucLast.Last());
                    }
                }
            }
            return sttHangMuc;
        }

        public List<VdtDaHangMucModel> FindListChildHangMuc(Guid parentId)
        {
            List<VdtDaHangMucModel> inner = new List<VdtDaHangMucModel>();
            foreach (var t in ItemsHangMuc.Where(item => item.IIdParentId == parentId))
            {
                inner.Add(t);
                inner = inner.Union(FindListChildHangMuc(t.Id)).ToList();
            }
            return inner;
        }

        private void OnAddNguonVon()
        {
            if (SelectedDuAn == null)
            {
                return;
            }
            VdtDaChuTruongDTNguonVonModel targetItem = new VdtDaChuTruongDTNguonVonModel()
            {
                IdChuTruongNguonVon = Guid.NewGuid(),
                IIdNguonVonId = null,
                FTienPheDuyet = 0
            };
            int currentRow = -1;
            if (ItemsChuTruongNguonVon != null && ItemsChuTruongNguonVon.Count > 0)
            {
                currentRow = 0;
                if (SelectedNguonVon != null)
                {
                    currentRow = ItemsChuTruongNguonVon.IndexOf(SelectedNguonVon);
                }
                VdtDaChuTruongDTNguonVonModel sourceItem = ItemsChuTruongNguonVon.ElementAt(currentRow);
                targetItem.Id = Guid.Empty;
                targetItem.IdChuTruongNguonVon = Guid.NewGuid();
                targetItem.IIdNguonVonId = null;
                targetItem.FTienPheDuyet = 0;
            }

            targetItem.PropertyChanged += DetailNguonVon_PropertyChanged;
            if (ItemsChuTruongNguonVon == null) ItemsChuTruongNguonVon = new ObservableCollection<VdtDaChuTruongDTNguonVonModel>();
            ItemsChuTruongNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsChuTruongNguonVon));
        }

        protected void OnDeleteHangMuc()
        {
            if (ItemsHangMuc != null && ItemsHangMuc.Count > 0 && SelectedHangMuc != null)
            {
                var listDelete = FindListChildHangMuc(SelectedHangMuc.IdDuAnHangMuc);
                listDelete.Add(SelectedHangMuc);
                foreach (var item in listDelete)
                {
                    item.IsDeleted = !SelectedHangMuc.IsDeleted;
                }
            }
        }

        protected void OnDeleteNguonVon()
        {
            if (ItemsChuTruongNguonVon != null && ItemsChuTruongNguonVon.Count > 0 && SelectedNguonVon != null)
            {
                SelectedNguonVon.IsDeleted = !SelectedNguonVon.IsDeleted;
                OnPropertyChanged(nameof(ItemsChuTruongNguonVon));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        public override void OnSave(object obj)
        {
            // Convert data
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = Guid.Parse(SelectedDuAn.ValueItem);
                Model.STenDuAn = SelectedDuAn.DisplayItem;
            }

            string maChuDT = string.Empty;
            if (SelectedChuDauTu != null && SelectedChuDauTu.ValueItem != null)
            {
                DmChuDauTu chuDauTu = _chuDauTuService.FindById(SelectedChuDauTu.Id);
                Model.IIdChuDauTuId = SelectedChuDauTu.Id;
                Model.IIdMaChuDauTuId = chuDauTu.IIDMaDonVi;
                maChuDT = chuDauTu.IIDMaDonVi;
            }
            if (SelectedNhomDuAn != null)
            {
                Model.IIdNhomDuAnId = Guid.Parse(SelectedNhomDuAn.ValueItem);
            }
            if (SelectedHinhThucQL != null)
            {
                Model.IIdHinhThucQuanLyId = Guid.Parse(SelectedHinhThucQL.ValueItem);
            }
            if (SelectedPhanCapPheDuyet != null)
            {
                Model.IIdCapPheDuyetId = Guid.Parse(SelectedPhanCapPheDuyet.ValueItem);
            }

            if (!ValiDateData()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                // Main process
                VdtDaChuTruongDauTu entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Add VdtDaChuTruongDT
                    entity = _mapper.Map<VdtDaChuTruongDauTu>(Model);
                    entity.BActive = true;
                    entity.BIsGoc = true;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;

                    _service.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = _mapper.Map<VdtDaChuTruongDauTu>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.IIdParentId = Model.Id;
                    entity.BActive = true;
                    entity.BIsGoc = false;
                    entity.DDateCreate = DateTime.Now;
                    entity.SUserCreate = _sessionService.Current.Principal;

                    _service.Adjust(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.BActive = true;
                    entity.BIsGoc = true;
                    entity.DDateUpdate = DateTime.Now;
                    entity.SUserUpdate = _sessionService.Current.Principal;

                    _service.Update(entity);
                }

                // Update vao bang VdtDaDuAn
                if (entity.IIdDuAnId.HasValue && !_service.CheckDuAnExistQDDauTu(entity.IIdDuAnId.Value))
                {
                    VdtDaDuAn duAn = _projectManagerService.FindById(entity.IIdDuAnId.Value);
                    if (duAn != null)
                    {
                        duAn.SDiaDiem = Model.SDiaDiem;
                        duAn.SMucTieu = Model.SMucTieu;
                        duAn.SQuyMo = Model.SQuyMo;
                        duAn.SKhoiCong = Model.SKhoiCong;
                        duAn.SKetThuc = Model.SHoanThanh;
                        duAn.IIdChuDauTuId = Model.IIdChuDauTuId;
                        duAn.IIdMaChuDauTuId = Model.IIdMaChuDauTuId;
                        duAn.IIdNhomDuAnId = Model.IIdNhomDuAnId;
                        duAn.IIdCapPheDuyetId = Model.IIdCapPheDuyetId;
                        duAn.FTongMucDauTu = Model.FTmdtduKienPheDuyet;
                        duAn.DDateUpdate = DateTime.Now;

                        // update mã dự án theo mã chủ đầu tư
                        if (maChuDT != null && duAn.SMaDuAn != null)
                        {
                            string maDuAn = duAn.SMaDuAn;
                            //maDuAn = maDuAn.Remove(4, 3);
                            //maDuAn = maDuAn.Insert(4, maChuDT);
                            string[] childMaDuAn = maDuAn.Split('-');
                            childMaDuAn[1] = maChuDT;
                            maDuAn = string.Join("-", childMaDuAn);
                            duAn.SMaDuAn = maDuAn;
                        }

                        _projectManagerService.Update(duAn);
                    }
                }

                // Save chi tiết
                SaveNguonVon(entity.Id, entity.IIdDuAnId.Value);
                SaveHangMuc(entity.Id);

                // Save attach file
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<ChuTruongDauTuModel>(e.Result);
                    IsDieuChinh = false;

                    SavedAction?.Invoke(Model);
                    LoadDuAn();
                    LoadData();
                    // Invoke message
                    System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ((Window)obj).Close();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void SaveHangMuc(Guid id)
        {
            List<VdtDaHangMucModel> listAdd = new List<VdtDaHangMucModel>();
            List<VdtDaHangMucModel> listEdit = new List<VdtDaHangMucModel>();
            List<VdtDaHangMucModel> listDelete = new List<VdtDaHangMucModel>();

            if (IsDieuChinh)
            {
                listAdd = ItemsHangMuc.Where(x => !x.IsDeleted).ToList();
            }
            else
            {
                listAdd = ItemsHangMuc.Where(x => x.Id.IsNullOrEmpty() && !x.IsDeleted && !string.IsNullOrEmpty(x.STenHangMuc)).ToList();
                listEdit = ItemsHangMuc.Where(x => !x.Id.IsNullOrEmpty() && x.IsModified && !x.IsDeleted).ToList();
                listDelete = ItemsHangMuc.Where(x => !x.Id.IsNullOrEmpty() && x.IsDeleted).ToList();
            }

            if (listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    item.IIdDuAnId = Model.IIdDuAnId;
                    item.IdChuTruong = id;
                }

                List<VdtDaChuTruongDauTuDmHangMuc> listHangMuc = _mapper.Map<List<VdtDaChuTruongDauTuDmHangMuc>>(listAdd);
                _service.AddRangeChuTruongDMHangMuc(listHangMuc);

                // Add data vào bảng chutruonghangmuc
                List<VdtDaChuTruongDauTuHangMuc> listChutruongHangMuc = _mapper.Map<List<VdtDaChuTruongDauTuHangMuc>>(listAdd);
                _service.AddRangeChuTruongHangMuc(listChutruongHangMuc);
            }
            if (listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    VdtDaChuTruongDauTuDmHangMuc duAnHangMuc = _service.FindChuTruongDMHangMuc(item.IdDuAnHangMuc);
                    _mapper.Map(item, duAnHangMuc);
                    _service.UpdateChuTruongDMHangMuc(duAnHangMuc);
                }
            }
            if (listDelete.Count > 0)
            {
                foreach (var item in listDelete)
                {
                    _service.DeleteChuTruongDMHangMuc(item.IdDuAnHangMuc);
                    _service.DeleteChuTruongHangMuc(item.IdChuTruongHangMuc.Value);
                }
            }
        }

        private void SaveNguonVon(Guid chuTruongId, Guid duAnId)
        {
            List<VdtDaChuTruongDTNguonVonModel> listAdd = new List<VdtDaChuTruongDTNguonVonModel>();
            List<VdtDaChuTruongDTNguonVonModel> listEdit = new List<VdtDaChuTruongDTNguonVonModel>();
            List<VdtDaChuTruongDTNguonVonModel> listDelete = new List<VdtDaChuTruongDTNguonVonModel>();

            if (IsDieuChinh)
            {
                listAdd = ItemsChuTruongNguonVon.Where(x => !x.IsDeleted).ToList();
            }
            else
            {
                listAdd = ItemsChuTruongNguonVon.Where(x => x.Id.IsNullOrEmpty() && !x.IsDeleted).ToList();
                listEdit = ItemsChuTruongNguonVon.Where(x => !x.Id.IsNullOrEmpty() && x.IsModified && !x.IsDeleted).ToList();
                listDelete = ItemsChuTruongNguonVon.Where(x => !x.Id.IsNullOrEmpty() && x.IsDeleted).ToList();
            }

            if (!listAdd.IsEmpty())
            {
                foreach (var item in listAdd)
                {
                    item.IIdChuTruongDauTuId = chuTruongId;
                    item.DuAnId = duAnId;
                }

                List<VdtDaChuTruongDauTuNguonVon> listNguonVon = _mapper.Map<List<VdtDaChuTruongDauTuNguonVon>>(listAdd);
                _service.AddRangeChuTruongNguonVon(listNguonVon);
            }
            if (listEdit.Count > 0)
            {
                foreach (var item in listEdit)
                {
                    VdtDaChuTruongDauTuNguonVon chuTruongNV = _service.FindChuTruongNguonVon(item.IdChuTruongNguonVon);
                    if (chuTruongNV != null)
                    {
                        _mapper.Map(item, chuTruongNV);
                        chuTruongNV.DuAnId = Model.IIdDuAnId;
                        chuTruongNV.IIdChuTruongDauTuId = Model.Id;
                        _service.UpdateChuTruongNguonVon(chuTruongNV);
                    }
                }
            }
            if (listDelete.Count > 0)
            {
                foreach (var item in listDelete)
                {
                    _service.DeleteChuTruongNguonVon(item.IdChuTruongNguonVon);
                }
            }
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            ItemsChuTruongNguonVon = null;
            OnPropertyChanged(nameof(ItemsChuTruongNguonVon));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaHangMucModel objectSender = (VdtDaHangMucModel)sender;
            if (args.PropertyName == nameof(objectSender.SMaHangMuc) || args.PropertyName == nameof(objectSender.STenHangMuc))
            {
                SelectedHangMuc = objectSender;
                if (args.PropertyName == nameof(objectSender.SMaHangMuc) && !string.IsNullOrEmpty(objectSender.SMaHangMuc))
                {
                    if (string.IsNullOrEmpty(objectSender.MaOrDer))
                    {
                        objectSender.MaOrDer = objectSender.SMaHangMuc;
                    }
                }
            }
            objectSender.IsModified = true;
            OnPropertyChanged(nameof(ItemsHangMuc));
        }

        private void DetailNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtDaChuTruongDTNguonVonModel objectSender = (VdtDaChuTruongDTNguonVonModel)sender;
            if (args.PropertyName == nameof(objectSender.IIdNguonVonId) || args.PropertyName == nameof(objectSender.FTienPheDuyet)
                || args.PropertyName == nameof(objectSender.IsDeleted))
            {
                if (!objectSender.IIdNguonVonId.HasValue)
                {
                    return;
                }
                if (ItemsChuTruongNguonVon.Count(x => x.IIdNguonVonId == objectSender.IIdNguonVonId && !x.IsDeleted) > 1)
                {
                    SelectedNguonVon.IIdNguonVonId = null;
                    System.Windows.Forms.MessageBox.Show(Resources.MsgCheckTrungNguonVonDauTu, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (args.PropertyName == nameof(objectSender.FTienPheDuyet) || args.PropertyName == nameof(objectSender.IsDeleted))
                {
                    Model.FTmdtduKienPheDuyet = ItemsChuTruongNguonVon.Where(y => !y.IsDeleted).Sum(x => x.FTienPheDuyet);
                    OnPropertyChanged(nameof(Model));
                }
                OnPropertyChanged(nameof(ItemsChuTruongNguonVon));
            }
            objectSender.IsModified = true;
        }

        private string GetMaHangMuc()
        {
            string maHM = string.Empty;
            if (SelectedDuAn != null)
            {
                VdtDaDuAn duAn = _projectManagerService.FindById(SelectedDuAn.Id);
                if (duAn != null)
                {
                    if (duAn.SMaDuAn != null && duAn.SMaDuAn.Length > 4)
                    {
                        maHM = duAn.SMaDuAn.Substring(duAn.SMaDuAn.Length - 4);
                    }
                }

                int indexHangMuc = _indexHangMucMax + 1;
                _indexHangMucMax = indexHangMuc;
                maHM += indexHangMuc.ToString("D3");
            }

            return maHM;
        }

        private bool ValiDateData()
        {
            List<string> lstMess = new List<string>();
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                lstMess.Add(Resources.MsgCheckSoQD);
            }
            else
            {
                if (_service.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
                {
                    lstMess.Add(Resources.MsgTrungSoQD);
                }
            }

            if (!Model.DNgayQuyetDinh.HasValue)
            {
                lstMess.Add(Resources.MsgCheckNgayPheDuyet);
            }
            if (Model.IIdDuAnId == null || Model.IIdDuAnId == Guid.Empty)
            {
                lstMess.Add(Resources.MsgCheckDuAn);
            }
            //if (Model.IIdDonViQuanLyId == null || Model.IIdDonViQuanLyId == Guid.Empty)
            if (String.IsNullOrEmpty(Model.IIdMaDonViQuanLy))
            {
                lstMess.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedChuDauTu == null)
            {
                lstMess.Add(Resources.MsgCheckChuDauTu);
            }
            if (string.IsNullOrEmpty(Model.SKhoiCong))
            {
                lstMess.Add(Resources.MsgCheckThoiGianKhoiCong);
            }
            if (string.IsNullOrEmpty(Model.SHoanThanh))
            {
                lstMess.Add(Resources.MsgCheckThoiGianHoanThanh);
            }
            if (SelectedNhomDuAn == null)
            {
                lstMess.Add(Resources.MsgCheckNhomDuAn);
            }

            if (!string.IsNullOrEmpty(Model.SKhoiCong) && !string.IsNullOrEmpty(Model.SHoanThanh))
            {
                if (string.Compare(Model.SKhoiCong, Model.SHoanThanh) > 0)
                    lstMess.Add(string.Format(Resources.MsgCheckThoiGianBatDauKetThuc, "khởi công", "hoàn thành"));
            }

            if (lstMess.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstMess), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
