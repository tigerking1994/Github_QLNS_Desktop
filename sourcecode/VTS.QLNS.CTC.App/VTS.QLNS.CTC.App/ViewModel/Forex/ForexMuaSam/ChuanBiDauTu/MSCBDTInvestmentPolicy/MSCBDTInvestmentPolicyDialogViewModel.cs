using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentPolicy;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Globalization;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentPolicy
{
    public class MSCBDTInvestmentPolicyDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhDaChuTruongDauTuModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaChuTruongDauTuService _service;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDaChuTruongDauTuHangMucService _nhDaChuTruongDauTuHangMucService;
        private readonly INhDaChuTruongDauTuNguonVonService _nhDaChuTruongDauTuNguonVonService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private SessionInfo _sessionInfo;

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Chủ trương đầu tư";
        public override string Name => "Chủ trương đầu tư";
        public override Type ContentType => typeof(MSCBDTInvestmentPolicyDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_CHUTRUONG_DAUTU;
        public bool IsDetail { get; set; }

        public int ILoai { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        private ObservableCollection<NhDaDuAnModel> _itemsDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAnModel _selectedDuAn;
        public NhDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                    LoadThongTinDuAn();
                }
            }
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
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

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set => SetProperty(ref _selectedChuDauTu, value);
        }

        private ObservableCollection<NhDmPhanCapPheDuyetModel> _itemsPhanCapPheDuyet;
        public ObservableCollection<NhDmPhanCapPheDuyetModel> ItemsPhanCapPheDuyet
        {
            get => _itemsPhanCapPheDuyet;
            set => SetProperty(ref _itemsPhanCapPheDuyet, value);
        }

        private NhDmPhanCapPheDuyetModel _selectedPhanCapPheDuyet;
        public NhDmPhanCapPheDuyetModel SelectedPhanCapPheDuyet
        {
            get => _selectedPhanCapPheDuyet;
            set => SetProperty(ref _selectedPhanCapPheDuyet, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private ObservableCollection<NhDaChuTruongDauTuNguonVonModel> _itemsChuTruongDauTuNguonVon;
        public ObservableCollection<NhDaChuTruongDauTuNguonVonModel> ItemsChuTruongDauTuNguonVon
        {
            get => _itemsChuTruongDauTuNguonVon;
            set => SetProperty(ref _itemsChuTruongDauTuNguonVon, value);
        }

        private NhDaChuTruongDauTuNguonVonModel _selectedChuTruongDauTuNguonVon;
        public NhDaChuTruongDauTuNguonVonModel SelectedChuTruongDauTuNguonVon
        {
            get => _selectedChuTruongDauTuNguonVon;
            set => SetProperty(ref _selectedChuTruongDauTuNguonVon, value);
        }

        private ObservableCollection<NhDaChuTruongDauTuHangMucModel> _itemsChuTruongDauTuHangMuc;
        public ObservableCollection<NhDaChuTruongDauTuHangMucModel> ItemsChuTruongDauTuHangMuc
        {
            get => _itemsChuTruongDauTuHangMuc;
            set => SetProperty(ref _itemsChuTruongDauTuHangMuc, value);
        }

        private NhDaChuTruongDauTuHangMucModel _selectedChuTruongDauTuHangMuc;
        public NhDaChuTruongDauTuHangMucModel SelectedChuTruongDauTuHangMuc
        {
            get => _selectedChuTruongDauTuHangMuc;
            set => SetProperty(ref _selectedChuTruongDauTuHangMuc, value);
        }

        public RelayCommand AddChuTruongDauTuNguonVonCommand { get; }
        public RelayCommand AddChuTruongDauTuHangMucCommand { get; }
        public RelayCommand DeleteChuTruongDauTuNguonVonCommand { get; }
        public RelayCommand DeleteChuTruongDauTuHangMucCommand { get; }
        public RelayCommand ReOrderChuTruongDauTuHangMucCommand { get; }

        public MSCBDTInvestmentPolicyDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaChuTruongDauTuService service,
            IDmChuDauTuService chuDauTuService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INhDaDuAnService nhDaDuAnService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDaChuTruongDauTuNguonVonService nhDaChuTruongDauTuNguonVonService,
            INhDaChuTruongDauTuHangMucService nhDaChuTruongDauTuHangMucService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;
            _chuDauTuService = chuDauTuService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nhDaDuAnService = nhDaDuAnService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDaChuTruongDauTuNguonVonService = nhDaChuTruongDauTuNguonVonService;
            _nhDaChuTruongDauTuHangMucService = nhDaChuTruongDauTuHangMucService;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;

            AddChuTruongDauTuNguonVonCommand = new RelayCommand(obj => OnAddChuTruongDauTuNguonVon());
            AddChuTruongDauTuHangMucCommand = new RelayCommand(obj => OnAddChuTruongDauTuHangMuc(obj), obj => (bool)obj || SelectedChuTruongDauTuHangMuc != null);
            DeleteChuTruongDauTuNguonVonCommand = new RelayCommand(obj => OnDeleteChuTruongDauTuNguonVon(), obj => SelectedChuTruongDauTuNguonVon != null);
            DeleteChuTruongDauTuHangMucCommand = new RelayCommand(obj => OnDeleteChuTruongDauTuHangMuc(), obj => SelectedChuTruongDauTuHangMuc != null);
            ReOrderChuTruongDauTuHangMucCommand = new RelayCommand(obj => OnReOrderChuTruongDauTuHangMuc(), obj => !ItemsChuTruongDauTuHangMuc.IsEmpty());
        }

        public override void OnIsDieuChinhChanged()
        {
            // Method intentionally left empty.
        }

        protected override void OnModelPropertyChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadChuDauTu();
            LoadPhanCapPheDuyet();
            LoadNguonVon();
            LoadLoaiCongTrinh();
            LoadTiGia();
            LoadData();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindInternalByNamLamViec(_sessionInfo.YearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDuAn()
        {
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedDonVi != null)
            {
                int yearOfWork = _sessionInfo.YearOfWork;
                string maDonVi = _selectedDonVi.IIDMaDonVi;
                IEnumerable<NhDaDuAnQuery> data;
                if (Model.Id.IsNullOrEmpty())
                {
                    data = _nhDaDuAnService.FindFromChuTruongDauTu(yearOfWork, maDonVi).Where(x => x.ILoai == 1);
                }
                else
                {
                    data = _nhDaDuAnService.FindFromChuTruongDauTu(yearOfWork, maDonVi, Model.Id).Where(x => x.ILoai == 1);
                }
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadChuDauTu()
        {
            var data = _chuDauTuService.FindByNamLamViec(_sessionInfo.YearOfWork);
            _itemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(data);
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void LoadPhanCapPheDuyet()
        {
            var data = _nhDmPhanCapPheDuyetService.FindAll();
            _itemsPhanCapPheDuyet = _mapper.Map<ObservableCollection<NhDmPhanCapPheDuyetModel>>(data);
            OnPropertyChanged(nameof(ItemsPhanCapPheDuyet));
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            CalculateNguonVon();
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _nhDmLoaiCongTrinhService.FindAll();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới chủ trương đầu tư";
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                NhDaChuTruongDauTu entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhDaChuTruongDauTuModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết chủ trương đầu tư";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Description = "Điều chỉnh chủ trương đầu tư";
                    Model.SSoQuyetDinh = string.Empty;
                    Model.DNgayQuyetDinh = null;
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật chủ trương đầu tư";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
                LoadDuAn();
                _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                _selectedChuDauTu = _itemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaChuDauTu));
                _selectedPhanCapPheDuyet = _itemsPhanCapPheDuyet.FirstOrDefault(x => x.Id == Model.IIdCapPheDuyetId);

                // Load tỉ giá và ngoại tệ khác
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            }

            LoadChuTruongDauTuNguonVon();
            LoadChuTruongDauTuHangMuc();

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadThongTinDuAn()
        {
            if (SelectedDuAn != null)
            {
                // Fill Thông tin dự án
                SelectedPhanCapPheDuyet = ItemsPhanCapPheDuyet.FirstOrDefault(x => x.Id == SelectedDuAn.IIdCapPheDuyetId);
                SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi.Equals(SelectedDuAn.IIdMaChuDauTu));
                Model.SKhoiCong = SelectedDuAn.SKhoiCong;
                Model.SKetThuc = SelectedDuAn.SKetThuc;
                Model.SDiaDiem = SelectedDuAn.SDiaDiem;
                Model.SMucTieu = SelectedDuAn.SMucTieu;
                Model.SQuyMo = SelectedDuAn.SQuyMo;
                OnPropertyChanged(nameof(Model));

                if (Model.Id.IsNullOrEmpty())
                {
                    bool isExitsData = (!_itemsChuTruongDauTuNguonVon.IsEmpty()) || (!_itemsChuTruongDauTuHangMuc.IsEmpty());
                    if (isExitsData)
                    {
                        // Confirm xem có muốn load lại thông tin nguồn vốn và hạng mục hay không
                        var result = MessageBoxHelper.Confirm("Đồng chí có muốn tải lại thông tin nguồn vốn và hạng mục từ dự án được chọn không?");
                        if (result == System.Windows.MessageBoxResult.No) return;
                    }

                    LoadChuTruongDauTuNguonVon();
                    LoadChuTruongDauTuHangMuc();
                }
            }
        }

        private void LoadChuTruongDauTuNguonVon()
        {
            _itemsChuTruongDauTuNguonVon = new ObservableCollection<NhDaChuTruongDauTuNguonVonModel>();
            NhDaChuTruongDauTuNguonVonModel targetItem = new NhDaChuTruongDauTuNguonVonModel
            {
                Id = Guid.NewGuid(),
                IIdNguonVonId = 5,
                IsAdded = true,
                IsModified = true
            };
            //LoadDuToanChiPhi(targetItem);
            targetItem.PropertyChanged += ChuTruongDauTuNguonVon_PropertyChanged;
            _itemsChuTruongDauTuNguonVon.Insert(0, targetItem);

            if (Model.Id.IsNullOrEmpty())
            {
                // Thêm mới
                if (SelectedDuAn != null)
                {
                    // Nếu thêm mới thì load thông tin nguồn vốn hạng mục từ dự án
                    var listNguonVon = _nhDaDuAnNguonVonService.FindByDuAnId(SelectedDuAn.Id);
                    _itemsChuTruongDauTuNguonVon = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuNguonVonModel>>(listNguonVon);
                    foreach (var item in _itemsChuTruongDauTuNguonVon)
                    {
                        item.IsAdded = true;
                        item.IIdDuAnNguonVonId = item.Id;
                        item.Id = Guid.NewGuid();
                        item.PropertyChanged += ChuTruongDauTuNguonVon_PropertyChanged;
                    }
                }
            }
            else
            {
                // Cập nhật hoặc Điều chỉnh
                IEnumerable<NhDaChuTruongDauTuNguonVon> data = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(Model.Id);
                _itemsChuTruongDauTuNguonVon = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuNguonVonModel>>(data);
                foreach (var item in _itemsChuTruongDauTuNguonVon)
                {
                    if (IsDieuChinh)
                    {
                        // Renew id
                        item.Id = Guid.NewGuid();
                        item.IIdChuTruongDauTuId = Guid.Empty;
                        item.IsAdded = true;
                    }
                    item.PropertyChanged += ChuTruongDauTuNguonVon_PropertyChanged;
                }
            }

            CalculateNguonVon();
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsChuTruongDauTuNguonVon));
        }

        private void LoadChuTruongDauTuHangMuc()
        {
            _itemsChuTruongDauTuHangMuc = new ObservableCollection<NhDaChuTruongDauTuHangMucModel>();
            if (Model.Id.IsNullOrEmpty())
            {
                // Thêm mới
                if (SelectedDuAn != null)
                {
                    var listHangMuc = _nhDaDuAnHangMucService.FindByDuAnId(SelectedDuAn.Id);
                    _itemsChuTruongDauTuHangMuc = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuHangMucModel>>(listHangMuc);
                    var refDictionary = _itemsChuTruongDauTuHangMuc.ToDictionary(x => x.Id, x => Guid.NewGuid());
                    foreach (var item in _itemsChuTruongDauTuHangMuc)
                    {
                        item.IsAdded = true;
                        item.IIdDuAnHangMucId = item.Id;
                        // Renew id
                        item.Id = refDictionary[item.Id];
                        if (!item.IIdParentId.IsNullOrEmpty() && refDictionary.ContainsKey(item.IIdParentId.Value))
                        {
                            item.IIdParentId = refDictionary[item.IIdParentId.Value];
                        }
                        item.PropertyChanged += ChuTruongDauTuHangMuc_PropertyChanged;
                    }
                }
            }
            else
            {
                // Cập nhật hoặc Điều chỉnh
                IEnumerable<NhDaChuTruongDauTuHangMuc> data = _nhDaChuTruongDauTuHangMucService.FindByChuTruongDauTuId(Model.Id);
                _itemsChuTruongDauTuHangMuc = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuHangMucModel>>(data);

                // Renew id for adjust
                var refDictionary = _itemsChuTruongDauTuHangMuc.ToDictionary(x => x.Id, x => Guid.NewGuid());
                foreach (var item in _itemsChuTruongDauTuHangMuc)
                {
                    if (IsDieuChinh)
                    {
                        item.Id = refDictionary[item.Id];
                        if (!item.IIdParentId.IsNullOrEmpty())
                        {
                            item.IIdParentId = refDictionary[item.IIdParentId.Value];
                        }
                        item.IIdChuTruongDauTuId = Guid.Empty;
                        item.IsAdded = true;
                    }
                    item.PropertyChanged += ChuTruongDauTuHangMuc_PropertyChanged;
                }
            }

            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsChuTruongDauTuHangMuc));
        }

        private void OnAddChuTruongDauTuNguonVon()
        {
            if (_itemsChuTruongDauTuNguonVon == null) _itemsChuTruongDauTuNguonVon = new ObservableCollection<NhDaChuTruongDauTuNguonVonModel>();

            int currentRow = -1;
            if (!_itemsChuTruongDauTuNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedChuTruongDauTuNguonVon != null)
                {
                    currentRow = ItemsChuTruongDauTuNguonVon.IndexOf(SelectedChuTruongDauTuNguonVon);
                }
            }

            NhDaChuTruongDauTuNguonVonModel targetItem = new NhDaChuTruongDauTuNguonVonModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += ChuTruongDauTuNguonVon_PropertyChanged;
            _itemsChuTruongDauTuNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsChuTruongDauTuNguonVon));
        }

        private void OnAddChuTruongDauTuHangMuc(object obj)
        {
            if (_itemsChuTruongDauTuHangMuc == null) _itemsChuTruongDauTuHangMuc = new ObservableCollection<NhDaChuTruongDauTuHangMucModel>();

            NhDaChuTruongDauTuHangMucModel sourceItem = _selectedChuTruongDauTuHangMuc;
            NhDaChuTruongDauTuHangMucModel targetItem = new NhDaChuTruongDauTuHangMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsChuTruongDauTuHangMuc.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = _itemsChuTruongDauTuHangMuc.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _itemsChuTruongDauTuHangMuc.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += ChuTruongDauTuHangMuc_PropertyChanged;
            _itemsChuTruongDauTuHangMuc.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsChuTruongDauTuHangMuc));
        }

        private void OnDeleteChuTruongDauTuNguonVon()
        {
            if (SelectedChuTruongDauTuNguonVon != null)
            {
                SelectedChuTruongDauTuNguonVon.IsDeleted = !SelectedChuTruongDauTuNguonVon.IsDeleted;
                CalculateNguonVon();
                OnPropertyChanged(nameof(ItemsChuTruongDauTuNguonVon));
            }
        }

        private void OnDeleteChuTruongDauTuHangMuc()
        {
            if (SelectedChuTruongDauTuHangMuc != null)
            {
                DeleteTreeItems(SelectedChuTruongDauTuHangMuc, !SelectedChuTruongDauTuHangMuc.IsDeleted);
            }
        }

        private void OnReOrderChuTruongDauTuHangMuc()
        {
            foreach (var item in _itemsChuTruongDauTuHangMuc)
            {
                var parent = _itemsChuTruongDauTuHangMuc.FirstOrDefault(x => x.Id == item.IIdParentId);
                if (parent == null) item.IIdParentId = null;
            }
            OrderItems();
        }

        public override void OnSave(object obj)
        {
            // Mapper object
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedChuDauTu != null)
            {
                Model.IIdChuDauTuId = SelectedChuDauTu.Id;
                Model.IIdMaChuDauTu = SelectedChuDauTu.IIDMaDonVi;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = SelectedDuAn.Id;
                Model.IIdKhttNhiemVuChiId = SelectedDuAn.IIdKhttNhiemVuChiId;
            }
            if (SelectedPhanCapPheDuyet != null)
            {
                Model.IIdCapPheDuyetId = SelectedPhanCapPheDuyet.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }

            //Validate
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateDateTime()) return;
            if (!CheckUniqueSoQuyetDinh()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Chi tiết
                Model.ILoai = ILoai;
                Model.ChuTruongDauTuNguonVons = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuNguonVonModel>>(ItemsChuTruongDauTuNguonVon);
                Model.ChuTruongDauTuHangMucs = _mapper.Map<ObservableCollection<NhDaChuTruongDauTuHangMucModel>>(ItemsChuTruongDauTuHangMuc);

                // Main process
                NhDaChuTruongDauTu entity;
                NhDaDuAn entityDuan;
                entityDuan = _mapper.Map<List<NhDaDuAn>>(_itemsDuAn).Where(x=>x.Id==Model.IIdDuAnId).FirstOrDefault();
                if (Model.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    entity = _mapper.Map<NhDaChuTruongDauTu>(Model);
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    // thêm mới lưu vào trong dự án
                    entityDuan.DNgaySua = DateTime.Now;
                    entityDuan.SNguoiSua = _sessionInfo.Principal;
                    entityDuan.SDiaDiem = entity.SDiaDiem;
                    entityDuan.SKhoiCong = entity.SKhoiCong;
                    entityDuan.SKetThuc = entity.SKetThuc;
                    entityDuan.SMucTieu = entity.SMucTieu;
                    entityDuan.SQuyMo = entity.SQuyMo;
                    entityDuan.ILoai = Model.ILoai;
                    _nhDaDuAnService.Update(entityDuan);
                    _service.Add(entity);
                    ItemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(entityDuan);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = _mapper.Map<NhDaChuTruongDauTu>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.IIdParentId = Model.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _service.Adjust(entity);
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _service.Update(entity);
                    // Cập nhập thông tin dự án
                    entityDuan.DNgaySua = DateTime.Now;
                    entityDuan.SNguoiSua = _sessionInfo.Principal;
                    entityDuan.SDiaDiem = entity.SDiaDiem;
                    entityDuan.SKhoiCong = entity.SKhoiCong;
                    entityDuan.SKetThuc = entity.SKetThuc;
                    entityDuan.SMucTieu = entity.SMucTieu;
                    entityDuan.SQuyMo = entity.SQuyMo;
                    entityDuan.ILoai = Model.ILoai;
                    _nhDaDuAnService.Update(entityDuan);
                    ItemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(entityDuan);
                }

                // Save attach file
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhDaChuTruongDauTuModel>(e.Result);
                    IsDieuChinh = false;

                    SavedAction?.Invoke(Model);
                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    //Đóng popup
                    if (obj is Window window)
                    {
                        Dispose();
                        window.Close();
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        public override void OnClosing()
        {
            // Clear items
            if (!ItemsChuTruongDauTuNguonVon.IsEmpty()) ItemsChuTruongDauTuNguonVon.Clear();
            if (!ItemsChuTruongDauTuHangMuc.IsEmpty()) ItemsChuTruongDauTuHangMuc.Clear();
            if (!ItemsNguonVon.IsEmpty()) ItemsNguonVon.Clear();
            if (!ItemsLoaiCongTrinh.IsEmpty()) ItemsLoaiCongTrinh.Clear();
            if (!ItemsTiGiaChiTiet.IsEmpty()) ItemsTiGiaChiTiet.Clear();
            if (!ItemsTiGia.IsEmpty()) ItemsTiGia.Clear();
            if (!ItemsPhanCapPheDuyet.IsEmpty()) ItemsPhanCapPheDuyet.Clear();
            if (!ItemsChuDauTu.IsEmpty()) ItemsChuDauTu.Clear();
            if (!ItemsDuAn.IsEmpty()) ItemsDuAn.Clear();
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void ChuTruongDauTuNguonVon_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaChuTruongDauTuNguonVonModel objectSender = (NhDaChuTruongDauTuNguonVonModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaChuTruongDauTuNguonVonModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaChuTruongDauTuNguonVonModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaChuTruongDauTuNguonVonModel.FGiaTriVnd))
                || e.PropertyName.Equals(nameof(NhDaChuTruongDauTuNguonVonModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateNguonVon();
            }
            if (e.PropertyName.Equals(nameof(NhDaChuTruongDauTuNguonVonModel.IIdNguonVonId)))
            {
                SetEnableComboboxItemNguonVon();
            }

            objectSender.IsModified = true;
        }

        private void ChuTruongDauTuHangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaChuTruongDauTuHangMucModel objectSender = (NhDaChuTruongDauTuHangMucModel)sender;
            if (!e.PropertyName.Equals(nameof(NhDaChuTruongDauTuHangMucModel.IsHangCha)))
            {
                objectSender.IsModified = true;
            }
        }

        private void CalculateNguonVon()
        {
            if (!_itemsChuTruongDauTuNguonVon.IsEmpty())
            {
                Model.FGiaTriNgoaiTeKhac = _itemsChuTruongDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhac);
                Model.FGiaTriUsd = _itemsChuTruongDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriUsd);
                Model.FGiaTriEur = _itemsChuTruongDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEur);
                Model.FGiaTriVnd = _itemsChuTruongDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriVnd);
                OnPropertyChanged(nameof(Model));
            }
        }

        private bool ValidateDateTime() //KhaiPD update
        {
            List<string> lstError = new List<string>();

            string[] tempKhoiCong = Model.SKhoiCong.Split('/');
            string[] tempKetThuc = Model.SKetThuc.Split('/');
            string strKhoiCong = "", strKetThuc = "";
            DateTime typeDate;

            if (tempKhoiCong.Length > 3)
            {
                lstError.Add(string.Format(Resources.MsgDateTimeFromInValid));
            }
            if (tempKetThuc.Length > 3)
            {
                lstError.Add(string.Format(Resources.MsgDateTimeToInValid));
            }
            if (tempKhoiCong.Length != tempKetThuc.Length)
            {
                lstError.Add(string.Format(Resources.MsgDateTimeFromToInValid));
            }
            else
            {
                if (tempKhoiCong.Length == 1)
                {
                    strKhoiCong = "01/01/" + tempKhoiCong[0];
                    strKetThuc = "01/01/" + tempKetThuc[0];
                }
                else if (tempKhoiCong.Length == 2)
                {
                    strKhoiCong = "01/" + tempKhoiCong[0] + "/" + tempKhoiCong[1];
                    strKetThuc = "01/" + tempKetThuc[0] + "/" + tempKetThuc[1];


                }
                else if (tempKhoiCong.Length == 3)
                {
                    strKhoiCong = tempKhoiCong[0] + "/" + tempKhoiCong[1] + "/" + tempKhoiCong[2];
                    strKetThuc = tempKetThuc[0] + "/" + tempKetThuc[1] + "/" + tempKetThuc[2];
                }

                if (!DateTime.TryParseExact(strKhoiCong, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out typeDate))
                {
                    lstError.Add(string.Format(Resources.MsgDateTimeFromInValid));
                }
                if (!DateTime.TryParseExact(strKetThuc, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out typeDate))
                {
                    lstError.Add(string.Format(Resources.MsgDateTimeToInValid));
                }
            }

            if (lstError.Count == 0)
            {
                DateTime dateKhoiCong = Convert.ToDateTime(strKhoiCong);
                DateTime dateKetThuc = Convert.ToDateTime(strKetThuc);

                if (dateKhoiCong >= dateKetThuc)
                {
                    lstError.Add(string.Format(Resources.MsgDateTimeFromSmall));
                }
            }

            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private bool CheckUniqueSoQuyetDinh()
        {
            bool myCheck = true;
            List<string> lstError = new List<string>();

            var data = _service.FindAll();

            foreach (var item in data)
            {
                if(item.SSoQuyetDinh == Model.SSoQuyetDinh && item.Id != Model.Id)
                {
                    myCheck = false;
                    lstError.Add(string.Format(Resources.MsgTrungSoQD));
                    break;
                }
            }

            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                myCheck = false;
            }
            return myCheck;
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = _itemsChuTruongDauTuHangMuc.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = _itemsChuTruongDauTuHangMuc.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaHangMuc = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaChuTruongDauTuHangMucModel currentItem)
        {
            var items = _itemsChuTruongDauTuHangMuc;
            int count = 0;
            var childs = items.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        private void DeleteTreeItems(NhDaChuTruongDauTuHangMucModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _itemsChuTruongDauTuHangMuc;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!_itemsChuTruongDauTuHangMuc.IsEmpty())
            {
                _itemsChuTruongDauTuHangMuc.ForAll(s => s.IsHangCha = _itemsChuTruongDauTuHangMuc.Any(y => y.IIdParentId == s.Id));
                _itemsChuTruongDauTuHangMuc.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !_itemsChuTruongDauTuHangMuc.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (_itemsChuTruongDauTuHangMuc.Any(y => y.IIdParentId == x.IIdParentId && y.IsHangCha)) x.IsHangCha = true;
                });
            }
        }

        private void SetEnableComboboxItemNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsChuTruongDauTuNguonVon.IsEmpty() || !_itemsChuTruongDauTuNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }
    }
}
