using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHKeHoachLuaChonNhaThau.ImportGoiThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.PlanDetail;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation
{
    public class DACBDTForexProjectInformationDialogViewModel : DialogAttachmentViewModelBase<NhDaDuAnModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private SessionInfo _sessionInfo;

        public bool IsDetail { get; set; }
        public bool IsReadOnly => ViewState == FormViewState.DETAIL;
        public bool IsNotQuyetDinhChiTrongNuoc { get; set; }
        public int ILoai { get; set; }
        public override Type ContentType => typeof(DACBDTForexProjectInformationDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.NH_DA_THONGTIN_DUAN;

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        #region Items

        private ObservableCollection<NhKhTongTheModel> _itemsSoKeHoachTongTheBQP;
        public ObservableCollection<NhKhTongTheModel> ItemsSoKeHoachTongTheBQP
        {
            get => _itemsSoKeHoachTongTheBQP;
            set => SetProperty(ref _itemsSoKeHoachTongTheBQP, value);
        }

        private NhKhTongTheModel _selectedSoKeHoachTongTheBQP;
        public NhKhTongTheModel SelectedSoKeHoachTongTheBQP
        {
            get => _selectedSoKeHoachTongTheBQP;
            set
            {
                if (SetProperty(ref _selectedSoKeHoachTongTheBQP, value))
                {
                    LoadDonVi();
                    LoadNhiemVuChi();
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
                    LoadNhiemVuChi();
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
        //private ObservableCollection<ComboboxItem> _itemsNguonVon;
        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        //public ObservableCollection<ComboboxItem> ItemsNguonVon
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

        private ObservableCollection<NhDaDuAnNguonVonModel> _itemsDuAnNguonVon;
        public ObservableCollection<NhDaDuAnNguonVonModel> ItemsDuAnNguonVon
        {
            get => _itemsDuAnNguonVon;
            set => SetProperty(ref _itemsDuAnNguonVon, value);
        }

        private ObservableCollection<NhDaChuTruongDauTuNguonVonModel> _itemsChuTruongDauTuNguonVon;
        public ObservableCollection<NhDaChuTruongDauTuNguonVonModel> ItemsChuTruongDauTuNguonVon
        {
            get => _itemsChuTruongDauTuNguonVon;
            set => SetProperty(ref _itemsChuTruongDauTuNguonVon, value);
        }
        private NhDaDuAnNguonVonModel _selectedDuAnNguonVon;
        public NhDaDuAnNguonVonModel SelectedDuAnNguonVon
        {
            get => _selectedDuAnNguonVon;
            set => SetProperty(ref _selectedDuAnNguonVon, value);
        }

        private ObservableCollection<NhDaDuAnHangMucModel> _itemsDuAnHangMuc;
        public ObservableCollection<NhDaDuAnHangMucModel> ItemsDuAnHangMuc
        {
            get => _itemsDuAnHangMuc;
            set => SetProperty(ref _itemsDuAnHangMuc, value);
        }

        private NhDaDuAnHangMucModel _selectedDuAnHangMuc;
        public NhDaDuAnHangMucModel SelectedDuAnHangMuc
        {
            get => _selectedDuAnHangMuc;
            set => SetProperty(ref _selectedDuAnHangMuc, value);
        }

        private ObservableCollection<NhDmNhiemVuChiModel> _itemsNhiemVuChi;
        public ObservableCollection<NhDmNhiemVuChiModel> ItemsNhiemVuChi
        {
            get => _itemsNhiemVuChi;
            set => SetProperty(ref _itemsNhiemVuChi, value);
        }

        private NhDmNhiemVuChiModel _selectedNhiemVuChi;
        public NhDmNhiemVuChiModel SelectedNhiemVuChi
        {
            get => _selectedNhiemVuChi;
            set => SetProperty(ref _selectedNhiemVuChi, value);
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value))
                {
                    LoadTiGiaChiTiet();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        public bool IsHieuChinhImport { get; set; } = false;

        public DmTiGiaDialogViewModel DmTiGiaDialogViewModel { get; set; }
        public DACBDTForexImportBudgetSourceDialogViewModel DACBDTForexImportBudgetSourceDialogViewModel { get; set; }
        public DACBDTForexImportCatalogDialogViewModel DACBDTForexImportCatalogDialogViewModel { get; set; }

        public RelayCommand AddDuAnNguonVonCommand { get; }
        public RelayCommand AddDuAnHangMucCommand { get; }
        public RelayCommand DeleteDuAnNguonVonCommand { get; }
        public RelayCommand DeleteDuAnHangMucCommand { get; }
        public RelayCommand ReOrderDuAnHangMucCommand { get; }
        public RelayCommand ImportHangMucCommand { get; }
        public RelayCommand ImportNguonVonCommand { get; }
        #endregion

        public DACBDTForexProjectInformationDialogViewModel(
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INsDonViService nsDonViService,
            IDmChuDauTuService dmChuDauTuService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INhDaDuAnService nhDaDuAnService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            DACBDTForexImportBudgetSourceDialogViewModel dACBDTForexImportBudgetSourceDialogViewModel,
            DACBDTForexImportCatalogDialogViewModel dACBDTForexImportCatalogDialogViewModel) : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nsDonViService = nsDonViService;
            _dmChuDauTuService = dmChuDauTuService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            DACBDTForexImportBudgetSourceDialogViewModel = dACBDTForexImportBudgetSourceDialogViewModel;

            AddDuAnNguonVonCommand = new RelayCommand(obj => OnAddDuAnNguonVon());
            AddDuAnHangMucCommand = new RelayCommand(obj => OnAddDuAnHangMuc(obj), obj => (bool)obj || SelectedDuAnHangMuc != null);
            DeleteDuAnNguonVonCommand = new RelayCommand(obj => OnDeleteDuAnNguonVon(), obj => SelectedDuAnNguonVon != null);
            DeleteDuAnHangMucCommand = new RelayCommand(obj => OnDeleteDuAnHangMuc(), obj => SelectedDuAnHangMuc != null);
            ReOrderDuAnHangMucCommand = new RelayCommand(obj => OnReOrderDuAnHangMuc(), obj => !ItemsDuAnHangMuc.IsEmpty());
            ImportHangMucCommand = new RelayCommand(obj => OnShowImportHangMuc());
            ImportNguonVonCommand = new RelayCommand(obj => OnShowImportNguonVon());
            DACBDTForexImportCatalogDialogViewModel = dACBDTForexImportCatalogDialogViewModel;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadAttach();
            LoadSoKeHoachTongThe();
            LoadDonVi();
            LoadChuDauTu();
            LoadPhanCapPheDuyet();
            LoadNguonVon();
            LoadLoaiCongTrinh();
            LoadTiGia();
            LoadData();
            //LoadNhiemVuChi();
        }

        private void SetEnableComboboxItemNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsDuAnNguonVon.IsEmpty() || !_itemsDuAnNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }

        private void LoadSoKeHoachTongThe()
        {
            //_itemsSoKeHoachTongTheBQP = new ObservableCollection<NhKhTongTheModel>();
            //var predicate = PredicateBuilder.True<NhKhTongThe>();
            //predicate = predicate.And(x => x.BIsActive);
            //var data = _nhKhTongTheService.FindAll();
            //_itemsSoKeHoachTongTheBQP = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            //OnPropertyChanged(nameof(ItemsSoKeHoachTongTheBQP));

            IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(x => x.DNgayTao);
            _itemsSoKeHoachTongTheBQP = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsSoKeHoachTongTheBQP.ForAll(s =>
            {
                if (s.ILoai == Loai_KHTT.GIAIDOAN)
                {
                    //s.TenKeHoach = $"KHTT {s.IGiaiDoanTu} - {s.IGiaiDoanDen} - Số KH: {s.SSoKeHoachBqp}";
                    s.TenKeHoach = $"KHTT {s.IGiaiDoanTu_BQP} - {s.IGiaiDoanDen_BQP} - Số KH: {s.SSoKeHoachBqp}";
                }
                else
                {
                    s.TenKeHoach = $"KHTT {s.INamKeHoach} - Số KH: {s.SSoKeHoachBqp}";
                }
            });
            OnPropertyChanged(nameof(ItemsSoKeHoachTongTheBQP));
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            if (_selectedSoKeHoachTongTheBQP != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> data = _nhKhTongTheNhiemVuChiService.FindAllDonViByKhTongTheId(_selectedSoKeHoachTongTheBQP.Id).Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadNhiemVuChi()
        {
            _itemsNhiemVuChi = new ObservableCollection<NhDmNhiemVuChiModel>();
            if (SelectedDonVi != null && SelectedSoKeHoachTongTheBQP != null)
            {
                var data = _nhDmNhiemVuChiService.FindByKhTongTheIdAndDonViId(SelectedSoKeHoachTongTheBQP.Id, SelectedDonVi.Id);
                _itemsNhiemVuChi = _mapper.Map<ObservableCollection<NhDmNhiemVuChiModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsNhiemVuChi));
        }

        private void LoadChuDauTu()
        {
            var data = _dmChuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            _itemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(data);
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void LoadPhanCapPheDuyet()
        {
            var data = _nhDmPhanCapPheDuyetService.FindAll().Where(x => x.BActive == true);
            _itemsPhanCapPheDuyet = _mapper.Map<ObservableCollection<NhDmPhanCapPheDuyetModel>>(data);
            OnPropertyChanged(nameof(ItemsPhanCapPheDuyet));
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            //_itemsNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadLoaiCongTrinh()
        {
            var data = _nhDmLoaiCongTrinhService.FindAll();
            _itemsLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(data);
            OnPropertyChanged(nameof(ItemsLoaiCongTrinh));
        }

        private void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            _itemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "THÊM MỚI THÔNG TIN DỰ ÁN";
                Description = "Thêm mới thông tin dự án";
                if (IsHieuChinhImport)
                {
                    LoadThongTinImportHieuChinh();
                    return;
                }
            }
            else
            {
                NhDaDuAn entity = _nhDaDuAnService.FindById(Model.Id);
                Model = _mapper.Map<NhDaDuAnModel>(entity);
                if (IsDetail)
                {
                    Description = "Chi tiết thông tin dự án";
                    Title = "THÔNG TIN DỰ ÁN";
                }
                else
                {
                    Description = "Cập nhật thông tin dự án";
                    Title = "CẬP NHẬP THÔNG TIN DỰ ÁN";
                }
                LoadDonVi();
                _selectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViQuanLyId);
                if (Model.IIdKhttNhiemVuChiId.HasValue)
                {
                    var khthNhiemVuChi = _nhKhTongTheNhiemVuChiService.Find(Model.IIdKhttNhiemVuChiId.Value);
                    if (khthNhiemVuChi != null)
                    {
                        var khth = _nhKhTongTheService.Find(khthNhiemVuChi.IIdKhTongTheId);
                        if (khth != null)
                        {
                            var loaiKeHoachTongTheValue = khth.IIdParentId.IsNullOrEmpty() ? "0" : "1";
                            LoadSoKeHoachTongThe();
                        }

                        _selectedSoKeHoachTongTheBQP = _itemsSoKeHoachTongTheBQP.FirstOrDefault(x => x.Id == khthNhiemVuChi.IIdKhTongTheId);
                        LoadDonVi();
                        _selectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViQuanLyId);
                        LoadNhiemVuChi();
                        _selectedNhiemVuChi = _itemsNhiemVuChi.FirstOrDefault(x => x.Id == khthNhiemVuChi.IIdNhiemVuChiId);

                        OnPropertyChanged(nameof(ItemsSoKeHoachTongTheBQP));
                        OnPropertyChanged(nameof(SelectedSoKeHoachTongTheBQP));
                        OnPropertyChanged(nameof(ItemsNhiemVuChi));
                        OnPropertyChanged(nameof(SelectedNhiemVuChi));
                    }
                }

                _selectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi == Model.IIdMaChuDauTu);
                _selectedPhanCapPheDuyet = ItemsPhanCapPheDuyet.FirstOrDefault(x => x.Id == Model.IIdCapPheDuyetId);

                // Load tỉ giá và ngoại tệ
                _selectedTiGia = _itemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                _selectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            }

            LoadDuAnNguonVon();
            LoadDuAnHangMuc();

            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedNhiemVuChi));
            OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadThongTinImportHieuChinh()
        {
            LoadDonVi();
            _selectedDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == Model.IIdDonViQuanLyId);
            if (Model.IIdKhttNhiemVuChiId.HasValue)
            {
                var khthNhiemVuChi = _nhKhTongTheNhiemVuChiService.Find(Model.IIdKhttNhiemVuChiId.Value);
                if (khthNhiemVuChi != null)
                {
                    var khth = _nhKhTongTheService.Find(khthNhiemVuChi.IIdKhTongTheId);
                    if (khth != null)
                    {
                        var loaiKeHoachTongTheValue = khth.IIdParentId.IsNullOrEmpty() ? "0" : "1";
                        LoadSoKeHoachTongThe();
                    }

                    _selectedSoKeHoachTongTheBQP = _itemsSoKeHoachTongTheBQP.FirstOrDefault(x => x.Id == khthNhiemVuChi.IIdKhTongTheId);
                    LoadNhiemVuChi();
                    _selectedNhiemVuChi = _itemsNhiemVuChi.FirstOrDefault(x => x.Id == khthNhiemVuChi.IIdNhiemVuChiId);

                    OnPropertyChanged(nameof(ItemsSoKeHoachTongTheBQP));
                    OnPropertyChanged(nameof(SelectedSoKeHoachTongTheBQP));
                    OnPropertyChanged(nameof(ItemsNhiemVuChi));
                    OnPropertyChanged(nameof(SelectedNhiemVuChi));
                }
            }

            _selectedChuDauTu = ItemsChuDauTu.FirstOrDefault(x => x.Id == Model.IIdChuDauTuId);
            _selectedPhanCapPheDuyet = ItemsPhanCapPheDuyet.FirstOrDefault(x => x.Id == Model.IIdCapPheDuyetId);

            // Load tỉ giá và ngoại tệ
            _selectedTiGia = _itemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            LoadTiGiaChiTiet();
            _selectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));

            // load nguon von
            _itemsDuAnNguonVon = Model.DuAnNguonVons != null ? Model.DuAnNguonVons : new ObservableCollection<NhDaDuAnNguonVonModel>();
            foreach (var item in _itemsDuAnNguonVon)
            {
                item.PropertyChanged += DuAnNguonVon_PropertyChanged;
            }
            CaculateDuAnNguonVon();

            // load hang muc
            _itemsDuAnHangMuc = Model.DuAnHangMucs != null ? Model.DuAnHangMucs : new ObservableCollection<NhDaDuAnHangMucModel>();

            foreach (var item in _itemsDuAnHangMuc)
            {
                item.PropertyChanged += DuAnHangMuc_PropertyChanged;
            }

            OnPropertyChanged(nameof(ItemsDuAnNguonVon));
            OnPropertyChanged(nameof(ItemsDuAnHangMuc));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedNhiemVuChi));
            OnPropertyChanged(nameof(SelectedPhanCapPheDuyet));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadDuAnNguonVon()
        {
            _itemsDuAnNguonVon = new ObservableCollection<NhDaDuAnNguonVonModel>();
            NhDaDuAnNguonVonModel targetItem = new NhDaDuAnNguonVonModel
            {
                Id = Guid.NewGuid(),
                IIdNguonVonId = 5,
                IsAdded = true,
                IsModified = true
            };

            //LoadDuToanChiPhi(targetItem);
            targetItem.PropertyChanged += DuAnNguonVon_PropertyChanged;
            _itemsDuAnNguonVon.Insert(0, targetItem);

            if (!Model.Id.IsNullOrEmpty())
            {
                IEnumerable<NhDaDuAnNguonVon> data = _nhDaDuAnNguonVonService.FindByDuAnId(Model.Id);
                _itemsDuAnNguonVon = _mapper.Map<ObservableCollection<NhDaDuAnNguonVonModel>>(data);
                foreach (var item in _itemsDuAnNguonVon)
                {
                    item.PropertyChanged += DuAnNguonVon_PropertyChanged;
                }
            }

            CaculateDuAnNguonVon();
            SetEnableComboboxItemNguonVon();
            OnPropertyChanged(nameof(ItemsDuAnNguonVon));
        }

        private void LoadDuAnHangMuc()
        {
            _itemsDuAnHangMuc = new ObservableCollection<NhDaDuAnHangMucModel>();
            if (!Model.Id.IsNullOrEmpty())
            {
                IEnumerable<NhDaDuAnHangMuc> data = _nhDaDuAnHangMucService.FindByDuAnId(Model.Id);
                _itemsDuAnHangMuc = _mapper.Map<ObservableCollection<NhDaDuAnHangMucModel>>(data);
                foreach (var item in _itemsDuAnHangMuc)
                {
                    item.PropertyChanged += DuAnHangMuc_PropertyChanged;
                }
            }
            OnPropertyChanged(nameof(ItemsDuAnHangMuc));
        }

        private void OnAddDuAnNguonVon()
        {
            if (_itemsDuAnNguonVon == null) _itemsDuAnNguonVon = new ObservableCollection<NhDaDuAnNguonVonModel>();

            int currentRow = -1;
            if (!_itemsDuAnNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedDuAnNguonVon != null)
                {
                    currentRow = _itemsDuAnNguonVon.IndexOf(SelectedDuAnNguonVon);
                }
            }

            NhDaDuAnNguonVonModel targetItem = new NhDaDuAnNguonVonModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += DuAnNguonVon_PropertyChanged;
            _itemsDuAnNguonVon.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(ItemsDuAnNguonVon));
        }

        private void OnAddDuAnHangMuc(object obj)
        {
            if (_itemsDuAnHangMuc == null) _itemsDuAnHangMuc = new ObservableCollection<NhDaDuAnHangMucModel>();

            NhDaDuAnHangMucModel sourceItem = SelectedDuAnHangMuc;
            NhDaDuAnHangMucModel targetItem = new NhDaDuAnHangMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsDuAnHangMuc.IsEmpty())
            {
                currentRow = 0;
                if (sourceItem != null)
                {
                    currentRow = _itemsDuAnHangMuc.IndexOf(sourceItem);
                    if (isParent)
                    {
                        currentRow += CountTreeChildItems(sourceItem);
                    }
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += DuAnHangMuc_PropertyChanged;
            _itemsDuAnHangMuc.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            OnPropertyChanged(nameof(ItemsDuAnHangMuc));
        }

        private void OnDeleteDuAnNguonVon()
        {
            if (SelectedDuAnNguonVon != null)
            {
                SelectedDuAnNguonVon.IsDeleted = !SelectedDuAnNguonVon.IsDeleted;
                CaculateDuAnNguonVon();
            }
        }

        protected void OnDeleteDuAnHangMuc()
        {
            if (SelectedDuAnHangMuc != null)
            {
                DeleteTreeItems(SelectedDuAnHangMuc, !SelectedDuAnHangMuc.IsDeleted);
            }
        }

        private void OnReOrderDuAnHangMuc()
        {
            foreach (var item in _itemsDuAnHangMuc)
            {
                var parent = _itemsDuAnHangMuc.FirstOrDefault(x => x.Id == item.IIdParentId);
                if (parent == null) item.IIdParentId = null;
            }
            OrderItems();
        }

        public override void OnSave(object obj)
        {
            if (!ValidateDateTime()) return;
            ConvertData();

            //Validate
            if (!ValidateViewModelHelper.Validate(Model)) return;

            if (IsHieuChinhImport)
            {
                SavedAction?.Invoke(Model);
                OnClose(obj);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                NhDaDuAn entity;
                if (Model.Id.IsNullOrEmpty())
                {
                    entity = _mapper.Map<NhDaDuAn>(Model);
                    entity.DNgayTao = DateTime.Now;
                    entity.ILoai = 2;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _nhDaDuAnService.Add(entity);
                }
                else
                {
                    entity = _nhDaDuAnService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.ILoai = 2;
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _nhDaDuAnService.Update(entity);
                }

                SaveAttachment(entity.Id);
                e.Result = entity;
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    // Reload data
                    Model = _mapper.Map<NhDaDuAnModel>(e.Result);

                    SavedAction?.Invoke(Model);
                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    //Đóng popup
                    if (obj is System.Windows.Window window)
                    {
                        window.Close();
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        public override void OnClosing()
        {
            // Clear items
            if (!_itemsDuAnNguonVon.IsEmpty()) _itemsDuAnNguonVon.Clear();
            if (!_itemsDuAnHangMuc.IsEmpty()) _itemsDuAnHangMuc.Clear();
            if (!_itemsSoKeHoachTongTheBQP.IsEmpty()) _itemsSoKeHoachTongTheBQP.Clear();
            if (!_itemsNguonVon.IsEmpty()) _itemsNguonVon.Clear();
            if (!_itemsLoaiCongTrinh.IsEmpty()) _itemsLoaiCongTrinh.Clear();
            if (!_itemsTiGiaChiTiet.IsEmpty()) _itemsTiGiaChiTiet.Clear();
            if (!_itemsTiGia.IsEmpty()) _itemsTiGia.Clear();
            if (!_itemsPhanCapPheDuyet.IsEmpty()) _itemsPhanCapPheDuyet.Clear();
            if (!_itemsChuDauTu.IsEmpty()) _itemsChuDauTu.Clear();
            if (!_itemsDonVi.IsEmpty()) _itemsDonVi.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.Close();
            }
        }

        private void DuAnNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var objSender = (NhDaDuAnNguonVonModel)sender;
            if (args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.IsDeleted))
                || args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.FGiaTriUsd))
                || args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.FGiaTriVnd))
                || args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.FGiaTriEur))
                || args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.FGiaTriNgoaiTeKhac)))
            {
                if (SelectedTiGia != null && !args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.IsDeleted)))
                {
                    var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                    string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                    string sourceCurrency;
                    string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                    double value;
                    switch (args.PropertyName)
                    {
                        case nameof(NhDaDuAnNguonVonModel.FGiaTriVnd):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                            value = objSender.FGiaTriVnd.Value;
                            break;
                        case nameof(NhDaDuAnNguonVonModel.FGiaTriEur):
                            sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                            value = objSender.FGiaTriEur.Value;
                            break;
                        case nameof(NhDaDuAnNguonVonModel.FGiaTriNgoaiTeKhac):
                            sourceCurrency = otherCurrency;
                            value = objSender.FGiaTriNgoaiTeKhac.Value;
                            break;
                        default:
                            sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                            value = objSender.FGiaTriUsd.Value;
                            break;
                    }
                    objSender.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                    objSender.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                }

                CaculateDuAnNguonVon();
            }
            if (args.PropertyName.Equals(nameof(NhDaDuAnNguonVonModel.IIdNguonVonId)))
            {
                SetEnableComboboxItemNguonVon();
            }
            objSender.IsModified = true;
        }

        private void DuAnHangMuc_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhDaDuAnHangMucModel objectSender = (NhDaDuAnHangMucModel)sender;
            objectSender.IsModified = true;
        }

        private void CaculateDuAnNguonVon()
        {
            if (!ItemsDuAnNguonVon.IsEmpty())
            {
                Model.FUsd = ItemsDuAnNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriUsd);
                Model.FVnd = ItemsDuAnNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriVnd);
                Model.FEur = ItemsDuAnNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriEur);
                Model.FNgoaiTeKhac = ItemsDuAnNguonVon.Where(x => !x.IsDeleted).Sum(x => x.FGiaTriNgoaiTeKhac);
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
                    tempKhoiCong[0] = (tempKhoiCong[0].Length == 1) ? "0" + tempKhoiCong[0] : tempKhoiCong[0];
                    tempKetThuc[0] = (tempKetThuc[0].Length == 1) ? "0" + tempKetThuc[0] : tempKetThuc[0];
                    strKhoiCong = "01/" +tempKhoiCong[0] + "/" + tempKhoiCong[1];
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

        private void ConvertData()
        {
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
            if (SelectedPhanCapPheDuyet != null)
            {
                Model.IIdCapPheDuyetId = SelectedPhanCapPheDuyet.Id;
            }
            if (SelectedNhiemVuChi != null)
            {
                Model.IIdKhttNhiemVuChiId = SelectedNhiemVuChi.IIdKHTTNhiemVuChiId;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            Model.SMaDuAn = Model.IIdMaDonViQuanLy + "-" + Model.IIdMaChuDauTu;

            // Chi tiết
            Model.DuAnNguonVons = _mapper.Map<ObservableCollection<NhDaDuAnNguonVonModel>>(ItemsDuAnNguonVon);
            Model.DuAnHangMucs = _mapper.Map<ObservableCollection<NhDaDuAnHangMucModel>>(ItemsDuAnHangMuc);
        }

        private void OrderItems(Guid? parentId = null)
        {
            var items = _itemsDuAnHangMuc;
            var childs = items.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = items.FirstOrDefault(x => x.Id == parentId);
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

        private int CountTreeChildItems(NhDaDuAnHangMucModel currentItem)
        {
            var items = _itemsDuAnHangMuc;
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

        private void DeleteTreeItems(NhDaDuAnHangMucModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = _itemsDuAnHangMuc;
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

        private void OnShowImportNguonVon()
        {
            DACBDTForexImportCatalogDialogViewModel.Init();
            DACBDTForexImportCatalogDialogViewModel.SavedAction = obj =>
            {

                if (DACBDTForexImportCatalogDialogViewModel != null && DACBDTForexImportCatalogDialogViewModel.ItemsDuAnNguonVonImport.Any())
                {
                    foreach (var item in DACBDTForexImportCatalogDialogViewModel.ItemsDuAnNguonVonImport)
                    {
                        ItemsDuAnNguonVon.Add(item);
                    }
                }
            };
            DACBDTForexImportCatalogDialogViewModel.ShowDialog();
        }

        private void OnShowImportHangMuc()
        {
            DACBDTForexImportBudgetSourceDialogViewModel.Init();
            DACBDTForexImportBudgetSourceDialogViewModel.SavedAction = obj =>
            {
                var index = 0;
                if (ItemsDuAnHangMuc.Any())
                {
                    if (!string.IsNullOrEmpty(ItemsDuAnHangMuc.LastOrDefault().SMaOrder) && ItemsDuAnHangMuc.LastOrDefault().SMaOrder.Contains('.'))
                    {
                        index = int.TryParse(ItemsDuAnHangMuc.LastOrDefault().SMaOrder.Substring(0, ItemsDuAnHangMuc.LastOrDefault().SMaOrder.IndexOf('.')), out int indexOut) ? indexOut : 0;
                    }
                    else
                    {
                        index = int.TryParse(ItemsDuAnHangMuc.LastOrDefault().SMaOrder, out int indexOut) ? indexOut : 0;
                    }
                }
                if (DACBDTForexImportBudgetSourceDialogViewModel != null && DACBDTForexImportBudgetSourceDialogViewModel.ItemsDuAnHangMucImport.Any())
                {
                    foreach (var item in DACBDTForexImportBudgetSourceDialogViewModel.ItemsDuAnHangMucImport)
                    {
                        if (index != 0)
                        {
                            item.SMaOrder = ConvertMaOrder(item.SMaOrder, index);
                            item.SMaHangMuc = item.SMaOrder;
                            item.STT = item.SMaOrder;
                            ItemsDuAnHangMuc.Add(item);
                        }
                        else
                        {
                            ItemsDuAnHangMuc.Add(item);
                        }

                    }
                }
            };
            DACBDTForexImportBudgetSourceDialogViewModel.ShowDialog();
        }

        private string ConvertMaOrder(string value, int indexMaxOld)
        {
            if (value.Contains('.'))
            {
                var indexValue = value.Substring(0, value.IndexOf("."));
                if (int.TryParse(indexValue, out int indexOut))
                {
                    if (indexOut < indexMaxOld)
                    {
                        value.Remove(0, indexValue.Length);
                        return value.Insert(0, (indexMaxOld + 1).ToString());
                    }

                }
                return value;
            }
            else
            {
                if (int.TryParse(value, out int indexOut))
                {
                    if (indexOut < indexMaxOld)
                        return (indexMaxOld + 1).ToString();
                }
                return value;
            }
        }

    }
}