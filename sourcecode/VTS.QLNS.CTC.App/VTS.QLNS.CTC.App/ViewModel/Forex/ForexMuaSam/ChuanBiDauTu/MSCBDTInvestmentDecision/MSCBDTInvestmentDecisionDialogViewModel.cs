using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Globalization;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTInvestmentDecision
{
    public class MSCBDTInvestmentDecisionDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhDaQdDauTuModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaQdDauTuService _service;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;
        private readonly INhDaChuTruongDauTuService _nhDaChuTruongDauTuService;
        private readonly INhDaChuTruongDauTuNguonVonService _nhDaChuTruongDauTuNguonVonService;
        private readonly INhDaChuTruongDauTuHangMucService _nhDaChuTruongDauTuHangMucService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private readonly INhDmTiGiaService _iNhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _iNhDmTiGiaChiTietService;
        private SessionInfo _sessionInfo;
        private List<NhDaChuTruongDauTuHangMucModel> _itemsChuTruongDauTuHangMuc;

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_CHU_TRUONG_DAU_TU_DIALOG;
        public override string Title => "Quyết định đầu tư";
        public override string Name => "Quyết định đầu tư";
        public override Type ContentType => typeof(MSCBDTInvestmentDecisionDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_QUYETDINH_DAUTU;
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public int ILoai { get; set; }
        public string STenNguonVon { get; set; }
        public Guid IIdQDDauTuNguonVonId { get; set; }

        private bool _isSelectedNguonVon;
        public bool IsSelectedNguonVon
        {
            get => _isSelectedNguonVon;
            set => SetProperty(ref _isSelectedNguonVon, value);
        }

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

        private ObservableCollection<NhDaQdDauTuNguonVonModel> _itemsQdDauTuNguonVon;
        public ObservableCollection<NhDaQdDauTuNguonVonModel> ItemsQdDauTuNguonVon
        {
            get => _itemsQdDauTuNguonVon;
            set => SetProperty(ref _itemsQdDauTuNguonVon, value);
        }

        private NhDaQdDauTuNguonVonModel _selectedQdDauTuNguonVon;
        public NhDaQdDauTuNguonVonModel SelectedQdDauTuNguonVon
        {
            get => _selectedQdDauTuNguonVon;
            set => SetProperty(ref _selectedQdDauTuNguonVon, value);
        }

        private ObservableCollection<NhDaQdDauTuChiPhiModel> _itemsQdDauTuChiPhi = new ObservableCollection<NhDaQdDauTuChiPhiModel>();
        public ObservableCollection<NhDaQdDauTuChiPhiModel> ItemsQdDauTuChiPhi
        {
            get => _itemsQdDauTuChiPhi;
            set => SetProperty(ref _itemsQdDauTuChiPhi, value);
        }

        private NhDaQdDauTuChiPhiModel _selectedQdDauTuChiPhi;
        public NhDaQdDauTuChiPhiModel SelectedQdDauTuChiPhi
        {
            get => _selectedQdDauTuChiPhi;
            set => SetProperty(ref _selectedQdDauTuChiPhi, value);
        }

        private Dictionary<string, NhDaChuTruongDauTuHangMuc> _ttNhDaChuTruongDauTuHangMuc;

        public bool? IsAllNGuonVonItemSelected
        {
            get
            {
                if (ItemsQdDauTuNguonVon != null)
                {
                    var selected = ItemsQdDauTuNguonVon.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, ItemsQdDauTuNguonVon);
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<NhDmChiPhiModel> _itemsDMChiPhi;
        public ObservableCollection<NhDmChiPhiModel> ItemsDMChiPhi
        {
            get => _itemsDMChiPhi;
            set => SetProperty(ref _itemsDMChiPhi, value);
        }

        private List<NhDaQdDauTuChiPhiModel> listSave = new List<NhDaQdDauTuChiPhiModel>();

        private List<NhDaChuTruongDauTuNguonVon> daChuTruongDauTuNguonVonResource = new List<NhDaChuTruongDauTuNguonVon>();
        public NhDaChuTruongDauTuNguonVon TongTienQdDauTuChiPhi { get; set; } = new NhDaChuTruongDauTuNguonVon();
        public NhDaChuTruongDauTuNguonVon TongTienChuChuongDauTuNguonVon { get; set; } = new NhDaChuTruongDauTuNguonVon();

        public MSCBDTInvestmentDecisionItemDialogViewModel MSCBDTInvestmentDecisionItemDialogViewModel { get; }

        public RelayCommand AddQdDauTuNguonVonCommand { get; }
        public RelayCommand AddQdDauTuChiPhiCommand { get; }
        public RelayCommand DeleteQdDauTuChiPhiCommand { get; }
        public RelayCommand DeleteQdDauTuNguonVonCommand { get; }
        public RelayCommand ReOrderQdDauTuChiPhiCommand { get; }
        public RelayCommand PhuLucHangMucCommand { get; }

        public MSCBDTInvestmentDecisionDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaQdDauTuService service,
            IDmChuDauTuService chuDauTuService,
            INhDaDuAnService nhDaDuAnService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INhDaChuTruongDauTuService nhDaChuTruongDauTuService,
            INhDaChuTruongDauTuNguonVonService nhDaChuTruongDauTuNguonVonService,
            INhDaChuTruongDauTuHangMucService nhDaChuTruongDauTuHangMucService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmChiPhiService nhDmChiPhiService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            INhDmTiGiaService iNhDmTiGiaService,
            INhDmTiGiaChiTietService iNhDmTiGiaChiTietService,
            MSCBDTInvestmentDecisionItemDialogViewModel mscbdtInvestmentDecisionItemDialogViewModel)
            : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;
            _chuDauTuService = chuDauTuService;
            _nhDaDuAnService = nhDaDuAnService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaChuTruongDauTuService = nhDaChuTruongDauTuService;
            _nhDaChuTruongDauTuNguonVonService = nhDaChuTruongDauTuNguonVonService;
            _nhDaChuTruongDauTuHangMucService = nhDaChuTruongDauTuHangMucService;
            _nhDmChiPhiService = nhDmChiPhiService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _iNhDmTiGiaService = iNhDmTiGiaService;
            _iNhDmTiGiaChiTietService = iNhDmTiGiaChiTietService;

            MSCBDTInvestmentDecisionItemDialogViewModel = mscbdtInvestmentDecisionItemDialogViewModel;

            AddQdDauTuNguonVonCommand = new RelayCommand(obj => OnAddQdDauTuNguonVon());
            AddQdDauTuChiPhiCommand = new RelayCommand(obj => OnAddQdDauTuChiPhi(obj), obj => (bool)obj || (SelectedQdDauTuChiPhi != null && SelectedQdDauTuChiPhi.QdDauTuHangMucs.IsEmpty()));
            DeleteQdDauTuNguonVonCommand = new RelayCommand(obj => OnDeleteQdDauTuNguonVon(), obj => SelectedQdDauTuNguonVon != null);
            DeleteQdDauTuChiPhiCommand = new RelayCommand(obj => OnDeleteQdDauTuChiPhi(), obj => SelectedQdDauTuChiPhi != null);
            ReOrderQdDauTuChiPhiCommand = new RelayCommand(obj => OnReOrderQdDauTuChiPhi(), obj => !ItemsQdDauTuChiPhi.IsEmpty());
            PhuLucHangMucCommand = new RelayCommand(obj => OnPhuLucHangMuc(obj));
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
            LoadDanhMucChiPhi();
            LoadDonVi();
            LoadChuDauTu();
            LoadNguonVon();
            LoadLoaiCongTrinh();
            LoadTiGia();
            LoadData();
            LoadChuTruongDauTuNguonVon();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
            _itemsDonVi = new ObservableCollection<DonViModel>();
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            _itemsChuDauTu = new ObservableCollection<DmChuDauTuModel>();

            OnPropertyChanged(nameof(ItemsDonVi));
            OnPropertyChanged(nameof(ItemsDuAn));
            OnPropertyChanged(nameof(ItemsChuDauTu));
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
                IEnumerable<NhDaDuAnQuery> listDuAn;
                if (Model.Id.IsNullOrEmpty())
                {
                    listDuAn = _nhDaDuAnService.FindFromQdDauTu(yearOfWork, maDonVi, 1).Where(x => x.ILoai == 1);
                }
                else
                {
                    listDuAn = _nhDaDuAnService.FindFromQdDauTu(yearOfWork, maDonVi, 1, Model.Id).Where(x => x.ILoai == 1);
                }
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadChuDauTu()
        {
            var data = _chuDauTuService.FindByNamLamViec(_sessionInfo.YearOfWork);
            _itemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(data);
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
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
                Description = "Thêm mới quyết định đầu tư";
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                NhDaQdDauTu entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhDaQdDauTuModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết quyết định đầu tư";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Description = "Điều chỉnh quyết định đầu tư";
                    //Model.SSoQuyetDinh = string.Empty;
                    //Model.DNgayQuyetDinh = null;
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật quyết định đầu tư";
                }

                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
                LoadDuAn();
                _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                LoadTongChuTruongDauTuNguonVon();
                _selectedChuDauTu = _itemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaChuDauTu));

                // Load tỉ giá và ngoại tệ khác
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            }
            LoadQdDauTuNguonVon();
            LoadChuTruongDauTuNguonVon();
            //LoadQdDauTuChiPhi();

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadTongChuTruongDauTuNguonVon()
        {
            if (SelectedDuAn != null)
            {
                var chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(SelectedDuAn.Id);
                if (chuTruongDauTu != null)
                {
                    var lstNguonVon = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriNgoaiTeKhac = lstNguonVon.Sum(x => x.FGiaTriNgoaiTeKhac);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriUsd = lstNguonVon.Sum(x => x.FGiaTriUsd);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriVnd = lstNguonVon.Sum(x => x.FGiaTriVnd);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriEur = lstNguonVon.Sum(x => x.FGiaTriEur);
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        private void LoadThongTinDuAn()
        {
            // Clear chủ trương đầu tư hạng mục khi dự án thay đổi
            _itemsChuTruongDauTuHangMuc = new List<NhDaChuTruongDauTuHangMucModel>();
            if (SelectedDuAn != null && Model.Id.IsNullOrEmpty())
            {
                // Lấy thông tin chủ trương đầu tư của dự án
                var chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(SelectedDuAn.Id);
                if (chuTruongDauTu != null)
                {
                    var lstChuTruongDauTuNguonVon = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriNgoaiTeKhac = lstChuTruongDauTuNguonVon.Sum(x => x.FGiaTriNgoaiTeKhac);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriUsd = lstChuTruongDauTuNguonVon.Sum(x => x.FGiaTriUsd);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriVnd = lstChuTruongDauTuNguonVon.Sum(x => x.FGiaTriVnd);
                    Model.TotalChuTruongDauTuNguonVon.FGiaTriEur = lstChuTruongDauTuNguonVon.Sum(x => x.FGiaTriEur);
                    Model.SKhoiCong = chuTruongDauTu.SKhoiCong;
                    Model.SKetThuc = chuTruongDauTu.SKetThuc;
                    Model.SDiaDiem = SelectedDuAn.SDiaDiem;
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.Id == chuTruongDauTu.IIdChuDauTuId);
                    OnPropertyChanged(nameof(Model));

                    bool isExitsData = (!_itemsQdDauTuNguonVon.IsEmpty())
                        || (!_itemsQdDauTuChiPhi.IsEmpty() && _itemsQdDauTuChiPhi.Any(x => !x.QdDauTuHangMucs.IsEmpty()));
                    if (isExitsData)
                    {
                        // Confirm xem có muốn load lại thông tin nguồn vốn và hạng mục hay không
                        var result = MessageBoxHelper.Confirm("Đồng chí có muốn tải lại thông tin nguồn vốn và hạng mục từ dự án được chọn không?");
                        if (result == System.Windows.MessageBoxResult.No) return;
                    }

                    LoadQdDauTuNguonVon(lstChuTruongDauTuNguonVon);
                    //LoadQdDauTuChiPhi(_nhDaChuTruongDauTuHangMucService.FindByChuTruongDauTuId(chuTruongDauTu.Id));
                }
            }
        }

        private void LoadQdDauTuNguonVon(IEnumerable<NhDaChuTruongDauTuNguonVon> lstChuTruongDauTuNguonVon = null)
        {
            _itemsQdDauTuNguonVon = new ObservableCollection<NhDaQdDauTuNguonVonModel>();
            NhDaQdDauTuNguonVonModel targetItem = new NhDaQdDauTuNguonVonModel
            {
                Id = Guid.NewGuid(),
                IIdNguonVonId = 5,
                IsAdded = true,
                IsModified = true
            };
            //LoadDuToanChiPhi(targetItem);
            targetItem.PropertyChanged += QdDauTuNguonVon_PropertyChanged;
            _itemsQdDauTuNguonVon.Insert(0, targetItem);

            if (Model.Id.IsNullOrEmpty())
            {
                if (!lstChuTruongDauTuNguonVon.IsEmpty())
                {
                    _itemsQdDauTuNguonVon = _mapper.Map<ObservableCollection<NhDaQdDauTuNguonVonModel>>(lstChuTruongDauTuNguonVon);
                    foreach (var item in _itemsQdDauTuNguonVon)
                    {
                        item.IsAdded = true;
                        item.IIdChuTruongDauTuNguonVonId = item.Id;
                        item.Id = Guid.NewGuid();
                        item.PropertyChanged += QdDauTuNguonVon_PropertyChanged;
                        item.nhDaChuTruongDauTuNguonVon.FGiaTriUsd = item.FGiaTriUsd;
                        item.nhDaChuTruongDauTuNguonVon.FGiaTriVnd = item.FGiaTriVnd;
                        item.nhDaChuTruongDauTuNguonVon.FGiaTriEur = item.FGiaTriEur;
                        item.nhDaChuTruongDauTuNguonVon.FGiaTriNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
                        item.FGiaTriUsd = null;
                        item.FGiaTriVnd = null;
                        item.FGiaTriEur = null;
                        item.FGiaTriNgoaiTeKhac = null;
                    }
                }
            }
            else
            {
                // Cập nhật hoặc Điều chỉnh
                IEnumerable<NhDaQdDauTuNguonVon> data = _nhDaQdDauTuNguonVonService.FindByQdDauTuId(Model.Id);
                _itemsQdDauTuNguonVon = _mapper.Map<ObservableCollection<NhDaQdDauTuNguonVonModel>>(data);

                foreach (var item in _itemsQdDauTuNguonVon)
                {
                    if (IsDieuChinh)
                    {
                        // Renew id
                        item.Id = Guid.NewGuid();
                        item.IIdQdDauTuId = Guid.Empty;
                        item.IsAdded = true;
                    }
                    item.PropertyChanged += QdDauTuNguonVon_PropertyChanged;
                    item.IsSelected = FilterNguonVonHasValue(item);
                }
            }

            CalculateNguonVon();
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsQdDauTuNguonVon));
        }

        private void LoadQdDauTuChiPhi(string sTenNguonVon, NhDaQdDauTuNguonVonModel nguonVon, IEnumerable<NhDaChuTruongDauTuHangMuc> listHangMuc = null)
        {
            // _itemsQdDauTuChiPhi = new ObservableCollection<NhDaQdDauTuChiPhiModel>();
            if (Model.Id.IsNullOrEmpty())
            {
                // Thêm mới
                // Load data from NH_DM_ChiPhi
                var data = listSave.Where(x => x.IIdQDDauTuNguonVonId == nguonVon.Id);
                foreach (var item in data)
                {
                    _itemsQdDauTuChiPhi.Add(item);
                    item.PropertyChanged += QdDauTuChiPhi_PropertyChanged;
                }
            }
            else
            {
                if (listSave.FirstOrDefault(x => x.IIdQDDauTuNguonVonId == nguonVon.Id) == null)
                {
                    // Cập nhật hoặc Điều chỉnh
                    IEnumerable<NhDaQdDauTuChiPhi> data = _nhDaQdDauTuChiPhiService.FindByNguonVonId(nguonVon.Id);
                    var dataMap = _mapper.Map<ObservableCollection<NhDaQdDauTuChiPhiModel>>(data);

                    // Renew id for adjust
                    var refDictionary = dataMap.ToDictionary(x => x.Id, x => Guid.NewGuid());
                    foreach (var item in dataMap)
                    {
                        // Load hạng mục. Cần optimize để lấy dữ liệu ngay từ lúc lấy chi phí để tối ưu hiệu năng
                        IEnumerable<NhDaQdDauTuHangMuc> dataHangMuc = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(item.Id);
                        item.QdDauTuHangMucs = _mapper.Map<ObservableCollection<NhDaQdDauTuHangMucModel>>(dataHangMuc.Where(hm => FilterHangMucHasValue(hm)));

                        // Tính lại giá trị của chi phí. Nếu có hạng mục thì tính tổng hạng mục. Không thì lấy trực tiếp theo chi phí
                        if (!item.QdDauTuHangMucs.IsEmpty())
                        {
                            //KhaiPD update comment code 22/11/2022
                            //item.FGiaTriUsd = item.QdDauTuHangMucs.Sum(x => x.FGiaTriUsd);
                            //item.FGiaTriEur = item.QdDauTuHangMucs.Sum(x => x.FGiaTriEur);
                            //item.FGiaTriVnd = item.QdDauTuHangMucs.Sum(x => x.FGiaTriVnd);
                            //item.FGiaTriNgoaiTeKhac = item.QdDauTuHangMucs.Sum(x => x.FGiaTriNgoaiTeKhac);
                        }

                        // Nếu là điều chỉnh, cần làm mới id liên quan
                        if (IsDieuChinh)
                        {
                            item.Id = refDictionary[item.Id];
                            if (!item.IIdParentId.IsNullOrEmpty() && refDictionary.ContainsKey(item.IIdParentId.Value))
                            {
                                item.IIdParentId = refDictionary[item.IIdParentId.Value];
                            }
                            else
                            {
                                item.IIdParentId = null;
                            }
                            item.IIdQdDauTuId = Guid.Empty;
                            item.IsAdded = true;

                            // Renew id hạng mục
                            var refItemDictionary = item.QdDauTuHangMucs.ToDictionary(x => x.Id, x => Guid.NewGuid());
                            foreach (var itemHangMuc in item.QdDauTuHangMucs)
                            {
                                itemHangMuc.Id = refItemDictionary[itemHangMuc.Id];
                                if (!itemHangMuc.IIdParentId.IsNullOrEmpty() && refItemDictionary.ContainsKey(itemHangMuc.IIdParentId.Value))
                                {
                                    itemHangMuc.IIdParentId = refItemDictionary[itemHangMuc.IIdParentId.Value];
                                }
                                else
                                {
                                    itemHangMuc.IIdParentId = null;
                                }
                                itemHangMuc.IIdQdDauTuChiPhiId = item.Id;
                                itemHangMuc.IsAdded = true;
                            }
                        }
                        item.STenNguonVon = sTenNguonVon;
                        _itemsQdDauTuChiPhi.Add(item);
                        item.PropertyChanged += QdDauTuChiPhi_PropertyChanged;
                    }
                }
                else
                {
                    var data = listSave.Where(x => x.IIdQDDauTuNguonVonId == nguonVon.Id);
                    foreach (var item in data)
                    {
                        _itemsQdDauTuChiPhi.Add(item);
                    }
                }
            }
            _itemsQdDauTuChiPhi.ForAll(s =>
            {
                s.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            });
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsQdDauTuChiPhi));
        }

        private void LoadChuTruongDauTuNguonVon()
        {
            foreach (var item in ItemsDuAn)
            {
                // Lấy thông tin chủ trương đầu tư của dự án
                var chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(item.Id);
                if (chuTruongDauTu == null || chuTruongDauTu.Id == Guid.Empty)
                    continue;

                var lstNguonVon = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                if (lstNguonVon == null || !lstNguonVon.Any())
                    continue;
                var diff = lstNguonVon.Except(daChuTruongDauTuNguonVonResource);
                if (diff != null && diff.Any())
                    daChuTruongDauTuNguonVonResource.AddRange(diff);
            }

            if (daChuTruongDauTuNguonVonResource?.Any() != true
                || ItemsQdDauTuNguonVon?.Any() != true)
                return;

            // lưu chủ trương đầu tư nguồn vốn vào ItemsQdDauTuNguonVon
            foreach (var item in ItemsQdDauTuNguonVon)
            {
                var searchDaChuTruongDauTuNguonVon = daChuTruongDauTuNguonVonResource?.FirstOrDefault(m => m.Id == item.IIdChuTruongDauTuNguonVonId && item.IIdChuTruongDauTuNguonVonId != Guid.Empty && !item.IsDeleted);
                if (searchDaChuTruongDauTuNguonVon != null)
                    item.nhDaChuTruongDauTuNguonVon = searchDaChuTruongDauTuNguonVon;
            }
            CalculateNguonVon();
        }

        private void OnAddQdDauTuNguonVon()
        {
            if (_itemsQdDauTuNguonVon == null) _itemsQdDauTuNguonVon = new ObservableCollection<NhDaQdDauTuNguonVonModel>();

            int currentRow = -1;
            if (!_itemsQdDauTuNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedQdDauTuNguonVon != null)
                {
                    currentRow = ItemsQdDauTuNguonVon.IndexOf(SelectedQdDauTuNguonVon);
                }
            }

            NhDaQdDauTuNguonVonModel targetItem = new NhDaQdDauTuNguonVonModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += QdDauTuNguonVon_PropertyChanged;
            _itemsQdDauTuNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsQdDauTuNguonVon));
        }

        private void OnAddQdDauTuChiPhi(object obj)
        {
            if (_itemsQdDauTuChiPhi == null) _itemsQdDauTuChiPhi = new ObservableCollection<NhDaQdDauTuChiPhiModel>();

            NhDaQdDauTuChiPhiModel sourceItem = _selectedQdDauTuChiPhi;
            NhDaQdDauTuChiPhiModel targetItem = new NhDaQdDauTuChiPhiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsQdDauTuChiPhi.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = _itemsQdDauTuChiPhi.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _itemsQdDauTuChiPhi.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                targetItem.IIdQDDauTuNguonVonId = sourceItem.IIdQDDauTuNguonVonId;
                targetItem.STenNguonVon = sourceItem.STenNguonVon;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.STenNguonVon = STenNguonVon;
            targetItem.IIdQDDauTuNguonVonId = IIdQDDauTuNguonVonId;
            targetItem.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += QdDauTuChiPhi_PropertyChanged;
            _itemsQdDauTuChiPhi.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsQdDauTuChiPhi));
        }

        private void OnDeleteQdDauTuNguonVon()
        {
            if (SelectedQdDauTuNguonVon != null)
            {
                SelectedQdDauTuNguonVon.IsDeleted = !SelectedQdDauTuNguonVon.IsDeleted;
            }
        }
        private void OnDeleteQdDauTuChiPhi()
        {
            if (SelectedQdDauTuChiPhi != null)
            {
                DeleteTreeItems(SelectedQdDauTuChiPhi, !SelectedQdDauTuChiPhi.IsDeleted);
            }
        }

        private void OnReOrderQdDauTuChiPhi()
        {
            foreach (var item in _itemsQdDauTuChiPhi)
            {
                var parent = _itemsQdDauTuChiPhi.FirstOrDefault(x => x.Id == item.IIdParentId);
                if (parent == null) item.IIdParentId = null;
            }
            OrderItems();
        }

        private void OnPhuLucHangMuc(object obj)
        {
            List<string> lstError = new List<string>();
            if (SelectedDuAn == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckDuAn));
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return;
            }

            var currentModel = (NhDaQdDauTuChiPhiModel)obj;
            var tempChiPhi = ItemsDMChiPhi.Where(x => x.IIdChiPhi == currentModel.IIdChiPhiId).FirstOrDefault();
            if (tempChiPhi != null)
            {
                currentModel.STenChiPhi = tempChiPhi.STenChiPhi;
            }
            MSCBDTInvestmentDecisionItemDialogViewModel.Model = currentModel;
            //MSCBDTInvestmentDecisionItemDialogViewModel.IIdTiGiaID = SelectedTiGia.Id;
            //MSCBDTInvestmentDecisionItemDialogViewModel.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            MSCBDTInvestmentDecisionItemDialogViewModel.ChuTruongDauTuHangMucs = _itemsChuTruongDauTuHangMuc;
            MSCBDTInvestmentDecisionItemDialogViewModel.CurrencyExchangeAction = (obj) => QdDauTuHangMucCurrencyExchange(obj);
            MSCBDTInvestmentDecisionItemDialogViewModel.Init();
            MSCBDTInvestmentDecisionItemDialogViewModel.SavedAction = obj =>
            {
                var data = obj as IEnumerable<NhDaQdDauTuHangMucModel>;
                var qdDauTuHangMucs = data.ToList();

                if (Model.Id.IsNullOrEmpty() && !_itemsChuTruongDauTuHangMuc.IsEmpty())
                {
                    // Xóa những hạng mục con có giá trị + hàng cha chỉ có con có giá trị
                    var dataDeleted = FindExceptData(data.Where(x => !x.IIdChuTruongDauTuHangMucId.IsNullOrEmpty()).ToList());
                    if (!dataDeleted.IsEmpty())
                    {
                        // Xóa items đã được add
                        _itemsChuTruongDauTuHangMuc.RemoveAll(s => dataDeleted.Any(x => x.IIdChuTruongDauTuHangMucId == s.Id));

                        // Chỉ lấy các phần tử thêm mới hoặc chọn từ chủ trương đầu tư hạng mục
                        qdDauTuHangMucs = data.Where(x => x.IIdChuTruongDauTuHangMucId.IsNullOrEmpty() || x.HasValue).ToList();
                    }
                }

                // Gán lại hạng mục
                currentModel.QdDauTuHangMucs = _mapper.Map<ObservableCollection<NhDaQdDauTuHangMucModel>>(qdDauTuHangMucs);

                // Tính tổng tiền hạng mục
                if (!currentModel.QdDauTuHangMucs.IsEmpty())
                {
                    var qdDauTuChiPhis = currentModel.QdDauTuHangMucs.Where(s => s.IIdParentId == null && !s.IsDeleted);
                    SelectedQdDauTuChiPhi.FGiaTriUsd = qdDauTuChiPhis.Sum(s => s.FGiaTriUsd);
                    SelectedQdDauTuChiPhi.FGiaTriEur = qdDauTuChiPhis.Sum(s => s.FGiaTriEur);
                    SelectedQdDauTuChiPhi.FGiaTriVnd = qdDauTuChiPhis.Sum(s => s.FGiaTriVnd);
                    SelectedQdDauTuChiPhi.FGiaTriNgoaiTeKhac = qdDauTuChiPhis.Sum(s => s.FGiaTriNgoaiTeKhac);
                }
            };
            MSCBDTInvestmentDecisionItemDialogViewModel.ShowDialogHost("ForexInvestmentDecisionDialog");

            #region Hoàng Tâm BA - Hiển thị kế thừa đầy đủ thông tin từ TTDA
            //KhaiPD
            var chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(SelectedDuAn.Id);
            _ttNhDaChuTruongDauTuHangMuc = _nhDaChuTruongDauTuHangMucService?.FindByChuTruongDauTuId(chuTruongDauTu.Id)?.ToDictionary(m => m.STenHangMuc);

            if (_ttNhDaChuTruongDauTuHangMuc != null && _ttNhDaChuTruongDauTuHangMuc.Count > 0)
            {
                // thêm dữ liệu từ được tải từ thông tin dự án vào
                var dsDauTuHangMuc = ItemsQdDauTuChiPhi?.SelectMany(m => m.QdDauTuHangMucs?.Where(h => FilterHangMucModelHasValue(h)).ToList());
                foreach (var item in _ttNhDaChuTruongDauTuHangMuc.Keys)
                {
                    if (dsDauTuHangMuc.Any(m => m.STenHangMuc == item)) continue;
                    if(MSCBDTInvestmentDecisionItemDialogViewModel.Items.Where(h => h.IIdGocId == _ttNhDaChuTruongDauTuHangMuc[item].Id).Count() == 0)
                    {
                        MSCBDTInvestmentDecisionItemDialogViewModel.OnAddQdDauTuHangMuc(new NhDaQdDauTuHangMucModel
                        {
                            IIdGocId = _ttNhDaChuTruongDauTuHangMuc[item].Id,
                            IIdLoaiCongTrinhId = _ttNhDaChuTruongDauTuHangMuc[item].IIdLoaiCongTrinhId,
                            IIdParentId = _ttNhDaChuTruongDauTuHangMuc[item].IIdParentId,
                            IsSuggestion = true,
                            SMaHangMuc = _ttNhDaChuTruongDauTuHangMuc[item].SMaHangMuc,
                            SMaOrder = _ttNhDaChuTruongDauTuHangMuc[item].SMaOrder,
                            STenHangMuc = _ttNhDaChuTruongDauTuHangMuc[item].STenHangMuc,
                        });
                    }                    
                }
            }
            #endregion
        }

        /// <summary>
        /// filter những hạng mục đã được điền giá trị hay đã được gán cho một chi phí nào đó
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        private bool FilterHangMucModelHasValue(NhDaQdDauTuHangMucModel hm)
        {
            return !hm.IsDeleted && (hm.FGiaTriUsd > 0 || hm.FGiaTriVnd > 0 || hm.FGiaTriEur > 0);
        }

        /// <summary>
        /// same as FilterHangMucModelHasValue, but diiferent class
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        private bool FilterHangMucHasValue(NhDaQdDauTuHangMuc hm)
        {
            return !hm.IsDeleted && (hm.FGiaTriUsd > 0 || hm.FGiaTriVnd > 0 || hm.FGiaTriEur > 0);
        }

        /// <summary>
        /// do lưu cả những nguồn vốn k được check vào DB nên phải filter ra những nguồn vốn nào có giá trị thì đánh checked
        /// </summary>
        /// <param name="nv"></param>
        /// <returns></returns>
        private bool FilterNguonVonHasValue(NhDaQdDauTuNguonVonModel nv)
        {
            return !nv.IsDeleted && (nv.FGiaTriEur > 0 || nv.FGiaTriVnd > 0 || nv.FGiaTriUsd > 0);
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
            if (!Validate()) return;
            if (!ValidateDateTime()) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Chi tiết
                Model.ILoai = ILoai;
                Model.QdDauTuNguonVons = _mapper.Map<ObservableCollection<NhDaQdDauTuNguonVonModel>>(ItemsQdDauTuNguonVon);
                foreach (var selectChiPhi in ItemsQdDauTuChiPhi.Where(x => x.IsDeleted != true))
                {
                    if (selectChiPhi.IIdChiPhiId != null)
                    {
                        var itemChiPhi = ItemsDMChiPhi.Where(x => x.IIdChiPhi == selectChiPhi.IIdChiPhiId).FirstOrDefault();
                        if (itemChiPhi != null)
                            selectChiPhi.STenChiPhi = itemChiPhi.STenChiPhi;
                    }
                }
                foreach (var item in Model.QdDauTuNguonVons)
                {
                    item.QdDauTuChiPhis = _mapper.Map<ObservableCollection<NhDaQdDauTuChiPhiModel>>(ItemsQdDauTuChiPhi.Where(n => !n.IsDeleted && n.IIdQDDauTuNguonVonId == item.Id));
                }    
                //Model.QdDauTuChiPhis = _mapper.Map<ObservableCollection<NhDaQdDauTuChiPhiModel>>(listSave);

                // Main process
                NhDaQdDauTu entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    // Add VdtDaChuTruongDT
                    entity = _mapper.Map<NhDaQdDauTu>(Model);
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _service.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = _mapper.Map<NhDaQdDauTu>(Model);
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
                }

                // Save attach file
                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhDaQdDauTuModel>(e.Result);
                    IsDieuChinh = false;

                    SavedAction?.Invoke(Model);
                    LoadData();

                    ItemsQdDauTuChiPhi.Clear();
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
            });
        }

        public override void Dispose()
        {
            // Clear items
            if (!_itemsChuTruongDauTuHangMuc.IsEmpty()) _itemsChuTruongDauTuHangMuc.Clear();
            if (!ItemsQdDauTuNguonVon.IsEmpty()) ItemsQdDauTuNguonVon.Clear();
            if (!ItemsQdDauTuChiPhi.IsEmpty()) ItemsQdDauTuChiPhi.Clear();
            if (!ItemsNguonVon.IsEmpty()) ItemsNguonVon.Clear();
            if (!ItemsLoaiCongTrinh.IsEmpty()) ItemsLoaiCongTrinh.Clear();
            if (!ItemsTiGiaChiTiet.IsEmpty()) ItemsTiGiaChiTiet.Clear();
            if (!ItemsTiGia.IsEmpty()) ItemsTiGia.Clear();
            if (!ItemsChuDauTu.IsEmpty()) ItemsChuDauTu.Clear();
            if (!ItemsDuAn.IsEmpty()) ItemsDuAn.Clear();
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }

        public void QdDauTuChiPhi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedQdDauTuChiPhi = (NhDaQdDauTuChiPhiModel)e.Row.Item;

            if (e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriNgoaiTeKhac))
                || e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriUsd))
                || e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriEur))
                || e.Column.SortMemberPath.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriVnd)))
            {
                if (!SelectedQdDauTuChiPhi.QdDauTuHangMucs.IsEmpty() || !SelectedQdDauTuChiPhi.CanEditValue)
                {
                    e.Cancel = true;
                }
            }
        }

        private void QdDauTuNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaQdDauTuNguonVonModel objectSender = (NhDaQdDauTuNguonVonModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.FGiaTriNgoaiTeKhac))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.FGiaTriEur))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.FGiaTriVnd)))
            {
                CalculateNguonVon();
            }
            if (e.PropertyName.Equals(nameof(NhDaQdDauTuNguonVonModel.IIdNguonVonId)))
            {
                SetEnableComboboxItemNguonVon();
            }
            if (e.PropertyName == nameof(NhDaQdDauTuNguonVonModel.IsSelected))
            {
                if (objectSender.IsSelected)
                {
                    IsSelectedNguonVon = true;
                    var tempNguonVon = ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach == objectSender.IIdNguonVonId);
                    if (tempNguonVon != null && !string.IsNullOrWhiteSpace(tempNguonVon.STen))
                    {
                        STenNguonVon = tempNguonVon.STen;
                    }
                    IIdQDDauTuNguonVonId = objectSender.Id;
                    LoadQdDauTuChiPhi(STenNguonVon, objectSender);
                }
                else
                {
                    if (!ItemsQdDauTuNguonVon.Any(x => x.IsSelected))
                    {
                        IsSelectedNguonVon = false;
                    }
                    var dataItems = ItemsQdDauTuChiPhi.Except(listSave);
                    var dataKhacItems = ItemsQdDauTuChiPhi.Where(x => x.IIdQDDauTuNguonVonId == objectSender.Id).ToList();
                    listSave.AddRange(dataItems);
                    foreach (var item in dataKhacItems)
                    {
                        _itemsQdDauTuChiPhi.Remove(item);
                    }
                    OnPropertyChanged(nameof(ItemsQdDauTuChiPhi));
                }
            }
            objectSender.IsModified = true;
            CalculateChiPhi();
        }

        private void QdDauTuChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaQdDauTuChiPhiModel objectSender = (NhDaQdDauTuChiPhiModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.QdDauTuHangMucs))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriNgoaiTeKhac))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriUsd))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriEur))
                || e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.FGiaTriVnd)))
            {
                if (e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.QdDauTuHangMucs)))
                {
                    CalculateHangMuc(objectSender);
                }
                CalculateChiPhi();
            }

            if (!e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaQdDauTuChiPhiModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
        }

        private void QdDauTuHangMucCurrencyExchange(object obj)
        {
            OnCellEditEnding(obj);
        }

        private void CalculateNguonVon()
        {
            if (!_itemsQdDauTuNguonVon.IsEmpty())
            {
                Model.FGiaTriNgoaiTeKhac = _itemsQdDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhac);
                Model.FGiaTriUsd = _itemsQdDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriUsd);
                Model.FGiaTriEur = _itemsQdDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEur);
                Model.FGiaTriVnd = _itemsQdDauTuNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriVnd);
                OnPropertyChanged(nameof(Model));

                TongTienChuChuongDauTuNguonVon.FGiaTriUsd = _itemsQdDauTuNguonVon.Sum(x => x.nhDaChuTruongDauTuNguonVon.FGiaTriUsd);
                TongTienChuChuongDauTuNguonVon.FGiaTriVnd = _itemsQdDauTuNguonVon.Sum(x => x.nhDaChuTruongDauTuNguonVon.FGiaTriVnd);
                TongTienChuChuongDauTuNguonVon.FGiaTriEur = _itemsQdDauTuNguonVon.Sum(x => x.nhDaChuTruongDauTuNguonVon.FGiaTriEur);
                TongTienChuChuongDauTuNguonVon.FGiaTriNgoaiTeKhac = _itemsQdDauTuNguonVon.Sum(x => x.nhDaChuTruongDauTuNguonVon.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(TongTienChuChuongDauTuNguonVon));
            }
        }

        private void CalculateChiPhi()
        {
            var parents = ItemsQdDauTuChiPhi.Where(x => !x.IsDeleted && x.IIdParentId.IsNullOrEmpty() || !ItemsQdDauTuChiPhi.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateChiPhi(item);
            }

            // tính toán giá QDDauTuNguonVon
            foreach (var item in ItemsQdDauTuNguonVon)
            {
                if (item.Id == Guid.Empty)
                    continue;

                var child = parents.Where(x => !x.IsDeleted && x.IIdQDDauTuNguonVonId == item.Id);
                if (child?.Any() != true)
                    continue;

                item.FGiaTriNgoaiTeKhac = child.Sum(x => x.FGiaTriNgoaiTeKhac);
                item.FGiaTriUsd = child.Sum(x => x.FGiaTriUsd);
                item.FGiaTriEur = child.Sum(x => x.FGiaTriEur);
                item.FGiaTriVnd = child.Sum(x => x.FGiaTriVnd);
            }

            TongTienQdDauTuChiPhi.FGiaTriNgoaiTeKhac = parents.Sum(x => x.FGiaTriNgoaiTeKhac);
            TongTienQdDauTuChiPhi.FGiaTriUsd = parents.Sum(x => x.FGiaTriUsd);
            TongTienQdDauTuChiPhi.FGiaTriEur = parents.Sum(x => x.FGiaTriEur);
            TongTienQdDauTuChiPhi.FGiaTriVnd = parents.Sum(x => x.FGiaTriVnd);
            OnPropertyChanged(nameof(TongTienQdDauTuChiPhi));
        }

        private void CalculateChiPhi(NhDaQdDauTuChiPhiModel parentItem)
        {
            var childs = ItemsQdDauTuChiPhi.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateChiPhi(item);
                }
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        private void CalculateHangMuc(NhDaQdDauTuChiPhiModel item)
        {
            if (item != null && !item.QdDauTuHangMucs.IsEmpty())
            {
                item.FGiaTriNgoaiTeKhac = item.QdDauTuHangMucs.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhac);
                item.FGiaTriUsd = item.QdDauTuHangMucs.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriUsd);
                item.FGiaTriEur = item.QdDauTuHangMucs.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEur);
                item.FGiaTriVnd = item.QdDauTuHangMucs.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriVnd);
            }
        }

        private bool Validate()
        {
            List<string> lstError = new List<string>();

            if (ItemsQdDauTuNguonVon == null || ItemsQdDauTuNguonVon.Count == 0)
            {
                lstError.Add(string.Format(Resources.MsgNguonVon));
            }
            else
            {
                bool isNotChooseNguonVon = ItemsQdDauTuNguonVon.Any(s => !s.IIdNguonVonId.HasValue && !s.IsDeleted && s.IsSelected);
                if (isNotChooseNguonVon)
                    lstError.Add(Resources.MsgCheckNguonVonDauTu);
            }
            var listSoSanh = ItemsQdDauTuNguonVon.Where(n => n.FGiaTriUsd > n.nhDaChuTruongDauTuNguonVon.FGiaTriUsd);
            if (!listSoSanh.IsEmpty())
            {
               //lstError.Add("Bạn có chắc chắn muốn nhập Giá trị Quyết định đầu tư > Giá trị Chủ trương đầu tư của Nguồn vốn ngân sách ?");
                string msgConfirm = "Bạn có chắc chắn muốn nhập Giá trị Quyết định đầu tư > Giá trị Chủ trương đầu tư của Nguồn vốn ngân sách ?";
                MessageBoxResult dialogResult = MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                MessageBoxHelper.Error(Resources.MsgCheckSoQD);
                return false;
            }
            else if (_service.CheckDuplicateSoQD(Model.SSoQuyetDinh, Model.Id))
            {
                MessageBoxHelper.Error(Resources.MsgTrungSoQD);
                return false;
            }
            return true;
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

        private void OrderItems(Guid? parentId = null)
        {
            var childs = _itemsQdDauTuChiPhi.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = _itemsQdDauTuChiPhi.FirstOrDefault(x => x.Id == parentId);
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
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaQdDauTuChiPhiModel currentItem)
        {
            var items = _itemsQdDauTuChiPhi;
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

        private void DeleteTreeItems(NhDaQdDauTuChiPhiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _itemsQdDauTuChiPhi;
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
            if (!_itemsQdDauTuChiPhi.IsEmpty())
            {
                _itemsQdDauTuChiPhi.ForAll(s => s.CanEditValue = !_itemsQdDauTuChiPhi.Any(y => y.IIdParentId == s.Id));
                _itemsQdDauTuChiPhi.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !_itemsQdDauTuChiPhi.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (_itemsQdDauTuChiPhi.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void SetEnableComboboxItemNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsQdDauTuNguonVon.IsEmpty() || !_itemsQdDauTuNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }

        private List<NhDaQdDauTuHangMucModel> FindExceptData(List<NhDaQdDauTuHangMucModel> items)
        {
            List<NhDaQdDauTuHangMucModel> results = new List<NhDaQdDauTuHangMucModel>();
            // Lấy danh sách cha trên cùng
            var parents = items.Where(x => x.IIdParentId.IsNullOrEmpty() || !items.Any(y => y.Id == x.IIdParentId)).ToList();
            foreach (var item in parents)
            {
                var rs = FindExceptData(item, items);
                if (!rs.IsEmpty()) results.AddRange(rs);
            }
            return results;
        }

        private List<NhDaQdDauTuHangMucModel> FindExceptData(NhDaQdDauTuHangMucModel currentItem, List<NhDaQdDauTuHangMucModel> items)
        {
            List<NhDaQdDauTuHangMucModel> results = new List<NhDaQdDauTuHangMucModel>();
            var childs = items.Where(x => x.IIdParentId == currentItem.Id).ToList();
            if (!childs.IsEmpty())
            {
                // Trường hợp có con => Đệ qui
                foreach (var item in childs)
                {
                    var rs = FindExceptData(item, items);
                    if (!rs.IsEmpty()) results.AddRange(rs);
                }
            }

            // Nếu item có giá trị + Trường hợp list con trống hoặc tất cả ds con được thêm hết => thêm phần tử hiện tại vào danh sách xóa
            bool isExceptParent = childs.IsEmpty() || childs.All(x => results.Any(y => y.Id == x.Id));
            if (currentItem.HasValue && isExceptParent)
            {
                results.Add(currentItem);
            }
            return results;
        }

        private static void SelectAll(bool select, IEnumerable<ModelBase> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
        private void LoadDanhMucChiPhi()
        {
            IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            _itemsDMChiPhi = _mapper.Map<ObservableCollection<NhDmChiPhiModel>>(data);

            OnPropertyChanged(nameof(ItemsDMChiPhi));
        }
    }
}
