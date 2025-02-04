using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTPlanImport
{
    public class MSNTNHPhuongAnNhapKhauDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhHdnkPhuongAnNhapKhauModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;

        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;

        private readonly INhDaQdDauTuService _qdDauTuService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;

        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;

        private readonly INhDaChuTruongDauTuService _nhDaChuTruongDauTuService;
        private readonly INhDaChuTruongDauTuNguonVonService _nhDaChuTruongDauTuNguonVonService;
        private readonly INhDaChuTruongDauTuHangMucService _nhDaChuTruongDauTuHangMucService;

        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDmNhiemVuChiService _nhDmNhiemVuChiService;
        private readonly INhHdnkPhuongAnNhapKhauService _service;

        private readonly INhDaGoiThauService _nhDaGoiThauService;

        private readonly INhDmNhaThauService _dmNhaThauService;
        private readonly INhDmLoaiTienTeService _dmLoaiTienTeService;
        private readonly INhDmTiGiaService _iNhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _iNhDmTiGiaChiTietService;
        private SessionInfo _sessionInfo;

        public override string Name => "QUYẾT ĐỊNH PHÊ DUYỆT PHƯƠNG ÁN NHẬP KHẨU";
        public override string Title => "Quyết định phê duyệt phương án nhập khẩu";
        public override Type ContentType => typeof(MSNTNHPhuongAnNhapKhauDialog);
        public bool IsDetail { get; set; }
        public bool LengthValidate { get; set; }
        public bool IsAdded => Model.Id.IsNullOrEmpty();
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public int ILoai { get; set; }
        public bool IsEnableSoCu => ILoai != 1;
        public bool IsEnableSoCuChuongTrinh => SelectedSoCuTrucTiep != null && SelectedSoCuTrucTiep.ValueItem == SO_CU_TRUC_TIEP.CHUONG_TRINH;

        private ObservableCollection<ComboboxItem> _itemsSoCuTrucTiep;
        public ObservableCollection<ComboboxItem> ItemsSoCuTrucTiep
        {
            get => _itemsSoCuTrucTiep;
            set => SetProperty(ref _itemsSoCuTrucTiep, value);
        }

        private ComboboxItem _selectedSoCuTrucTiep;
        public ComboboxItem SelectedSoCuTrucTiep
        {
            get => _selectedSoCuTrucTiep;
            set
            {
                if (SetProperty(ref _selectedSoCuTrucTiep, value))
                {
                    SetVisibilityForm();
                    LoadGiaTriPheDuyet();
                }
            }
        }

        private ObservableCollection<NhKhTongTheModel> _itemsKeHoachTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private NhKhTongTheModel _selectedKeHoachTongThe;
        public NhKhTongTheModel SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                if (SetProperty(ref _selectedKeHoachTongThe, value))
                {
                    LoadChuongTrinh();
                    LoadDonVi();
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
                    LoadChuongTrinh();
                }
            }
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhKhTongTheNhiemVuChiModel _selectedChuongTrinh;
        public NhKhTongTheNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set
            {
                if (SetProperty(ref _selectedChuongTrinh, value))
                {
                    LoadDuAn();
                }
            }
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
                    LoadGiaTriPheDuyet();
                }
            }
        }

        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThau;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }

        private NhDaGoiThauModel _selectedGoiThau;
        public NhDaGoiThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }
        public ObservableCollection<NhDaGoiThauModel> GoiThauTemps { get; set; } = new ObservableCollection<NhDaGoiThauModel>();
        public NhDaGoiThauModel GoiThauTemp { get; set; }

        private ObservableCollection<NhDmNhaThauModel> _itemsDmNhaThau;
        public ObservableCollection<NhDmNhaThauModel> ItemsDmNhaThau
        {
            get => _itemsDmNhaThau;
            set => SetProperty(ref _itemsDmNhaThau, value);
        }

        private NhDmNhaThauModel _selectedDmNhaThau;
        public NhDmNhaThauModel SelectedDmNhaThau
        {
            get => _selectedDmNhaThau;
            set => SetProperty(ref _selectedDmNhaThau, value);
        }

        private ObservableCollection<DmLoaiTienTeModel> _itemsLoaiTienTe;
        public ObservableCollection<DmLoaiTienTeModel> ItemsLoaiTienTe
        {
            get => _itemsLoaiTienTe;
            set => SetProperty(ref _itemsLoaiTienTe, value);
        }

        private DmLoaiTienTeModel _selectedLoaiTienTe;
        public DmLoaiTienTeModel SelectedLoaiTienTe
        {
            get => _selectedLoaiTienTe;
            set => SetProperty(ref _selectedLoaiTienTe, value);
        }

        private bool _isViewDuAn;
        public bool IsViewDuAn
        {
            get => _isViewDuAn;
            set => SetProperty(ref _isViewDuAn, value);
        }

        private bool _isViewTTDuAn;
        public bool IsViewTTDuAn
        {
            get => _isViewTTDuAn;
            set => SetProperty(ref _isViewTTDuAn, value);
        }

        private bool _isViewCCDT;
        public bool IsViewCCDT
        {
            get => _isViewCCDT;
            set => SetProperty(ref _isViewCCDT, value);
        }

        private bool _isViewQDDT;
        public bool IsViewQDDT
        {
            get => _isViewQDDT;
            set => SetProperty(ref _isViewQDDT, value);
        }
        public bool IsAdd { get; set; }

        public RelayCommand AddGoiThauCommand { get; }
        public RelayCommand DeleteGoiThauCommand { get; }
        public RelayCommand ShowGoiThauCommand { get; }
        public RelayCommand SavePhuongAnNhapKhauCommand { get; }
        public MSNTNHPhuongAnNhapKhauDetailViewModel MSNTNHPhuongAnNhapKhauDetailViewModel { get; }

        public MSNTNHPhuongAnNhapKhauDialogViewModel
        (
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaQdDauTuService qdDauTuService,
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
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDmNhiemVuChiService nhDmNhiemVuChiService,
            INhHdnkPhuongAnNhapKhauService service,
            INhDaGoiThauService nhDaGoiThauService,
            INhDmNhaThauService dmNhaThauService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucService,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDmLoaiTienTeService dmLoaiTienTeService,
            INhDmTiGiaChiTietService iNhDmTiGiaChiTietService,
            INhDmTiGiaService iNhDmTiGiaService,

            MSNTNHPhuongAnNhapKhauDetailViewModel msntnhPhuongAnNhapKhauDetailViewModel
        )
        : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _qdDauTuService = qdDauTuService;
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
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDmNhiemVuChiService = nhDmNhiemVuChiService;
            _service = service;
            _nhDaGoiThauService = nhDaGoiThauService;
            _dmNhaThauService = dmNhaThauService;
            _nhDaGoiThauHangMucService = nhDaGoiThauHangMucService;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _dmLoaiTienTeService = dmLoaiTienTeService;
            _iNhDmTiGiaService = iNhDmTiGiaService;
            _iNhDmTiGiaChiTietService = iNhDmTiGiaChiTietService;

            AddGoiThauCommand = new RelayCommand(obj => OnAddGoiThau());
            DeleteGoiThauCommand = new RelayCommand(obj => OnDeleteGoiThau(), obj => SelectedGoiThau != null);
            ShowGoiThauCommand = new RelayCommand(obj => OnShowGoiThau());
            SavePhuongAnNhapKhauCommand = new RelayCommand(obj => OnSavePhuongAnNhapKhau(obj));
            MSNTNHPhuongAnNhapKhauDetailViewModel = msntnhPhuongAnNhapKhauDetailViewModel;
            MSNTNHPhuongAnNhapKhauDetailViewModel.ParentPage = this;
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
            LengthValidate = false;
            LoadDefault();
            LoadSoCuTrucTiep();
            LoadKeHoachTongThe();
            //LoadTiGia();
            LoadDsGoiThau();
            LoadDmNhaThau();
            LoadLoaiTienTe();
            LoadData();
            LoadAttach();
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
            _itemsDonVi = new ObservableCollection<DonViModel>();
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();

            OnPropertyChanged(nameof(ItemsDonVi));
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadSoCuTrucTiep()
        {
            List<ComboboxItem> listSoCu = new List<ComboboxItem>
            {
                new ComboboxItem{ DisplayItem = "Chương trình", ValueItem = SO_CU_TRUC_TIEP.CHUONG_TRINH},
                new ComboboxItem{ DisplayItem = "Thông tin dự án", ValueItem = SO_CU_TRUC_TIEP.THONG_TIN_DU_AN},
                new ComboboxItem{ DisplayItem = "Chủ trương đầu tư", ValueItem = SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU},
                new ComboboxItem{ DisplayItem = "Quyết định đầu tư", ValueItem = SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU}
            };
            ItemsSoCuTrucTiep = new ObservableCollection<ComboboxItem>(listSoCu);

            if (ILoai == 1)
            {
                _selectedSoCuTrucTiep = ItemsSoCuTrucTiep.FirstOrDefault(s => s.ValueItem == SO_CU_TRUC_TIEP.CHUONG_TRINH);
            }
            OnPropertyChanged(nameof(ItemsSoCuTrucTiep));
            OnPropertyChanged(nameof(SelectedSoCuTrucTiep));
        }

        private void LoadKeHoachTongThe()
        {
            IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(x => x.DNgayTao);
            _itemsKeHoachTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsKeHoachTongThe.ForAll(s =>
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
            OnPropertyChanged(nameof(ItemsKeHoachTongThe));
        }

        private void LoadChuongTrinh()
        {
            _itemsChuongTrinh = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> listKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndIdDonVi(_selectedKeHoachTongThe.Id, _selectedDonVi.Id).ToList();
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listKhTongTheNhiemVuChi);
                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindByYearAndIDNhiemVuChi(_sessionInfo.YearOfWork, SelectedKeHoachTongThe != null ? SelectedKeHoachTongThe.Id : Guid.Empty);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDuAn()
        {
            if (ILoai == 1) return;

            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedChuongTrinh != null)
            {
                IEnumerable<NhDaDuAn> listDuAn = _nhDaDuAnService.FindAll(s => s.IIdKhttNhiemVuChiId == _selectedChuongTrinh.Id);
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
                OnPropertyChanged(nameof(ItemsDuAn));
            }
        }

        private void LoadGiaTriPheDuyet()
        {
            if (_selectedSoCuTrucTiep != null && _selectedDuAn != null)
            {
                if (_selectedSoCuTrucTiep.ValueItem == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
                {
                    NhDaDuAn duAn = _nhDaDuAnService.FindById(_selectedDuAn.Id);
                    Model.FGiaTriUsd = duAn?.FUsd;
                    Model.FGiaTriVnd = duAn?.FVnd;
                    Model.FGiaTriEur = duAn?.FEur;
                    Model.FGiaTriNgoaiTeKhac = duAn?.FNgoaiTeKhac;
                    Model.IIdDuAnId = duAn?.Id;
                }
                if (_selectedSoCuTrucTiep.ValueItem == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
                {
                    NhDaChuTruongDauTu chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(_selectedDuAn.Id);
                    // Do FGiaTriUsd, FGiaTriVnd, FGiaTriEur, FGiaTriNgoaiTeKhac Không lưu nên tạm thời get ở nguồn vốn ra
                    if (chuTruongDauTu != null)
                    {
                        IEnumerable<NhDaChuTruongDauTuNguonVon> nguonVons = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                        Model.FGiaTriUsd = nguonVons?.Sum(s => s.FGiaTriUsd);
                        Model.FGiaTriVnd = nguonVons?.Sum(s => s.FGiaTriVnd);
                        Model.FGiaTriEur = nguonVons?.Sum(s => s.FGiaTriEur);
                        Model.FGiaTriNgoaiTeKhac = nguonVons?.Sum(s => s.FGiaTriNgoaiTeKhac);
                        Model.IIdChuTruongDauTuId = chuTruongDauTu.Id;
                        Model.SSoSoCu = chuTruongDauTu.SSoQuyetDinh;
                    }
                }
                if (_selectedSoCuTrucTiep.ValueItem == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU)
                {
                    NhDaQdDauTu qdDauTu = _qdDauTuService.FindByDuAnId(_selectedDuAn.Id);
                    // Do FGiaTriUsd, FGiaTriVnd, FGiaTriEur, FGiaTriNgoaiTeKhac Không lưu nên tạm thời get ở nguồn vốn ra
                    if (qdDauTu != null)
                    {
                        IEnumerable<NhDaQdDauTuNguonVon> nguonVons = _nhDaQdDauTuNguonVonService.FindByQdDauTuId(qdDauTu.Id);
                        Model.FGiaTriUsd = nguonVons?.Sum(s => s.FGiaTriUsd);
                        Model.FGiaTriVnd = nguonVons?.Sum(s => s.FGiaTriVnd);
                        Model.FGiaTriEur = nguonVons?.Sum(s => s.FGiaTriEur);
                        Model.FGiaTriNgoaiTeKhac = nguonVons?.Sum(s => s.FGiaTriNgoaiTeKhac);
                        Model.IIdQddauTuId = qdDauTu.Id;
                        Model.SSoSoCu = qdDauTu.SSoQuyetDinh;
                    }
                }
                OnPropertyChanged(nameof(Model));
            }
        }

        private void LoadDsGoiThau()
        {
            _itemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdPhuongAnNhapKhauId == Model.Id);
            foreach (var goithau in data)
            {
                goithau.GoiThauNguonVons = _nhDaGoiThauNguonVonService.FindByListNguonVon(goithau.Id);
            }
            foreach (var goithau in data)
            {
                foreach (var nguonvon in goithau.GoiThauNguonVons)
                {
                    nguonvon.GoiThauChiPhis = _nhDaGoiThauChiPhiService.FindListChiPhiByNguonVon(nguonvon.Id);
                }
            }
            foreach (var goithau in data)
            {
                foreach (var nguonvon in goithau.GoiThauNguonVons)
                {
                    foreach (var chiphi in nguonvon.GoiThauChiPhis)
                    {
                        chiphi.GoiThauHangMucs = _nhDaGoiThauHangMucService.FindListHangMuc(chiphi.Id);
                    }
                }
            }
            if (data.Any())
            {
                _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                foreach (var item in _itemsGoiThau)
                {
                    item.PropertyChanged += GoiThau_PropertyChanged;
                }
                OnPropertyChanged(nameof(ItemsGoiThau));
            }
        }

        private void LoadDmNhaThau()
        {
            IEnumerable<NhDmNhaThau> data = _dmNhaThauService.FindAll().Where(s => s.ILoai == 2);
            _itemsDmNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsDmNhaThau));
        }

        private void LoadLoaiTienTe()
        {
            IEnumerable<NhDmLoaiTienTe> data = _dmLoaiTienTeService.FindAll();
            _itemsLoaiTienTe = _mapper.Map<ObservableCollection<DmLoaiTienTeModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiTienTe));
        }

        private void SetVisibilityForm()
        {
            if (_selectedSoCuTrucTiep != null)
            {
                switch (_selectedSoCuTrucTiep.ValueItem)
                {
                    case SO_CU_TRUC_TIEP.CHUONG_TRINH:
                        _isViewDuAn = false;
                        _isViewTTDuAn = false;
                        _isViewCCDT = false;
                        _isViewQDDT = false;
                        break;

                    case SO_CU_TRUC_TIEP.THONG_TIN_DU_AN:
                        _isViewTTDuAn = true;
                        _isViewDuAn = true;
                        _isViewCCDT = false;
                        _isViewQDDT = false;
                        break;

                    case SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU:
                        _isViewCCDT = true;
                        _isViewDuAn = true;
                        _isViewTTDuAn = false;
                        _isViewQDDT = false;
                        break;

                    default:
                        _isViewQDDT = true;
                        _isViewDuAn = true;
                        _isViewTTDuAn = false;
                        _isViewCCDT = false;
                        break;
                }

                OnPropertyChanged(nameof(IsViewDuAn));
                OnPropertyChanged(nameof(IsViewTTDuAn));
                OnPropertyChanged(nameof(IsViewCCDT));
                OnPropertyChanged(nameof(IsViewQDDT));
            }
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới quyết định phê duyệt phương án nhập khẩu";
                Model.DNgayQuyetDinh = DateTime.Now;
            }
            else
            {
                if (BIsReadOnly)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết quyết định phê duyệt phương án nhập khẩu";
                }
                else if (IsDieuChinh)
                {
                    IconKind = PackIconKind.Adjust;
                    Description = "Điều chỉnh quyết định phê duyệt phương án nhập khẩu";
                   // Model.SSoQuyetDinh = string.Empty;
                   // Model.DNgayQuyetDinh = null;
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật quyết định phê duyệt phương án nhập khẩu";
                }
                _selectedSoCuTrucTiep = _itemsSoCuTrucTiep.FirstOrDefault(s => s.ValueItem == Model.SLoaiSoCu);
                _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
                
                LoadDuAn();
                _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                LoadGiaTriPheDuyet();
                // Load tỉ giá và ngoại tệ khác
                //SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                //LoadTiGiaChiTiet();
                //SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
                SelectedKeHoachTongThe = ItemsKeHoachTongThe.FirstOrDefault(x => x.Id == Model.IIdKhTongTheId);
                LoadDonVi();
                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
                LoadChuongTrinh();
                _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhttNhiemVuChiId);
            }
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedSoCuTrucTiep));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            SetVisibilityForm();
            //foreach (var item in ItemsGoiThau)
            //{
            //    item.PropertyChanged += DetailModel_PropertyChanged;
            //}
        }
        
        private void OnAddGoiThau()
        {
            //if (!ValidateGoiThau()) return;
            if (_itemsGoiThau == null)
            {
                _itemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            }
            int currentRow = -1;
            if (!_itemsGoiThau.IsEmpty())
            {
                currentRow = 0;
                if (SelectedGoiThau != null)
                {
                    currentRow = _itemsGoiThau.IndexOf(SelectedGoiThau);
                }
            }
            //NhDaGoiThauModel sourceItem = _selectedGoiThau != null ? _selectedGoiThau : new NhDaGoiThauModel();
            //NhDaGoiThauModel targetItem = ObjectCopier.Clone(sourceItem);          
                NhDaGoiThauModel targetItem = new NhDaGoiThauModel();
                targetItem.Id = Guid.NewGuid();
                //targetItem.IIdDuAnId = _selectedDuAn?.Id;                             // chi gan khi an Save
                //targetItem.IIdDonViQuanLyId = _selectedDonVi.Id;
                //targetItem.IIdMaDonViQuanLy = _selectedDonVi.IIDMaDonVi;
                //targetItem.IIdPhuongAnNhapKhauId = Model.Id;
                //targetItem.IIdKhTongTheNhiemVuChiId = _selectedChuongTrinh.Id;
                targetItem.DNgayTao = DateTime.Now;
                targetItem.IsAdded = true;
                targetItem.IsModified = true;
                targetItem.PropertyChanged += GoiThau_PropertyChanged;
            //targetItem.PropertyChanged += DetailModel_PropertyChanged;
            _itemsGoiThau.Insert(currentRow + 1, targetItem);
                OnPropertyChanged(nameof(ItemsGoiThau));         
        }

        private void OnDeleteGoiThau()
        {
            if (SelectedGoiThau != null)
            {
                SelectedGoiThau.IsDeleted = !SelectedGoiThau.IsDeleted;
            }
        }

        private void GoiThau_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaGoiThauModel objectSender = (NhDaGoiThauModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaGoiThauModel.IsDeleted))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.STenGoiThau))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.IIdNhaThauId))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.IThoiGianThucHien))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.FGiaGoiThauVnd))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.FGiaGoiThauUsd)))
            {
                objectSender.IsModified = true;
            }
        }

        private void OnShowGoiThau()
        {
            SelectedGoiThau?.GoiThauNguonVons?.ForAll(m => m.IsChecked = true);
            MSNTNHPhuongAnNhapKhauDetailViewModel.Model = SelectedGoiThau;
            MSNTNHPhuongAnNhapKhauDetailViewModel.IsDieuChinh = IsDieuChinh;
            MSNTNHPhuongAnNhapKhauDetailViewModel.BIsReadOnly = BIsReadOnly;
            MSNTNHPhuongAnNhapKhauDetailViewModel.IsAdded = IsAdded;
            MSNTNHPhuongAnNhapKhauDetailViewModel.SLoaiSoCu = SelectedSoCuTrucTiep?.ValueItem;
            MSNTNHPhuongAnNhapKhauDetailViewModel.IsEnableSoCuChuongTrinh = IsEnableSoCuChuongTrinh;
            MSNTNHPhuongAnNhapKhauDetailViewModel.SoPANK = Model.SSoQuyetDinh;
            MSNTNHPhuongAnNhapKhauDetailViewModel.SelectedTiGia = SelectedTiGia;
            MSNTNHPhuongAnNhapKhauDetailViewModel.SelectedTiGiaChiTiet = SelectedTiGiaChiTiet;
            MSNTNHPhuongAnNhapKhauDetailViewModel.Init();
            MSNTNHPhuongAnNhapKhauDetailViewModel.SavedAction = obj => SaveGoiThauDetail(obj);
            MSNTNHPhuongAnNhapKhauDetailViewModel.ShowDialog();
        }

        private void SaveGoiThauDetail(object obj)
        {
            NhDaGoiThauModel data = obj as NhDaGoiThauModel;
            if (data != null)
            {
                // Tính tổng tiền gói thầu
                var dataSum = data.GoiThauNguonVons;               
                data.IsModified = true;
                SelectedGoiThau.FGiaGoiThauUsd = dataSum.Sum(x => x.FGiaTriUsd);
                SelectedGoiThau.FGiaGoiThauVnd = dataSum.Sum(x => x.FGiaTriVnd);
                SelectedGoiThau.FGiaGoiThauEur = dataSum.Sum(x => x.FGiaTriEur);
                SelectedGoiThau.FGiaGoiThauNgoaiTeKhac = dataSum.Sum(x => x.FGiaTriNgoaiTeKhac);
                foreach (var item in _itemsGoiThau)
                {
                    if (item.Id == data.Id)
                    {
                        ItemsGoiThau.Remove(item);
                        break;
                    }
                }
                _itemsGoiThau.Add(data.Clone());
                OnPropertyChanged(nameof(ItemsGoiThau));                
                foreach (var goithau in ItemsGoiThau)
                    foreach (var nguonvon in goithau.GoiThauNguonVons)
                    {
                        nguonvon.IsSaved = true;
                    }
            }            
        }

        private void OnSavePhuongAnNhapKhau(object obj)
        {
            if (!Validate()) return;
            if (ItemsGoiThau != null)
            {
                foreach (var item in ItemsGoiThau)
                {
                    item.IThuocMenu = 1;
                    item.ILoai = 1;
                }
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                // Mapper object
                if (SelectedSoCuTrucTiep != null)
                {
                    Model.SLoaiSoCu = SelectedSoCuTrucTiep.ValueItem;
                }
                if (SelectedDuAn != null)
                {
                    Model.IIdDuAnId = SelectedDuAn.Id;
                    Model.IIdKhttNhiemVuChiId = SelectedDuAn.IIdKhttNhiemVuChiId;
                }
                if(SelectedKeHoachTongThe!=null)
                {
                    Model.IIdKhTongTheId = SelectedKeHoachTongThe.Id;   
                }    
                if (SelectedChuongTrinh != null)
                {
                    Model.IIdKhttNhiemVuChiId = SelectedChuongTrinh.Id;
                }
                if (SelectedDonVi != null)
                {
                    Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                    Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
                }
                if (SelectedTiGia != null)
                {
                    Model.IIdTiGiaId = SelectedTiGia.Id;
                }
                if (SelectedTiGiaChiTiet != null)
                {
                    Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
                }
                // Map gói thầu
                Model.NhDaGoiThaus = ItemsGoiThau;

                //gan lai mot so truong cho Du an Goi thau
                Model.NhDaGoiThaus.ForAll(x =>
                {
                    x.IIdDuAnId = _selectedDuAn?.Id;
                    x.IIdDonViQuanLyId = _selectedDonVi.Id;
                    x.IIdMaDonViQuanLy = _selectedDonVi.IIDMaDonVi;
                    x.IIdKhTongTheNhiemVuChiId = _selectedChuongTrinh.Id;
                    x.IIdPhuongAnNhapKhauId = Model.Id;
                });
                Model.ILoai = ILoai;
                if (!ValidateViewModelHelper.Validate(Model))
                {
                    LengthValidate = true;
                    return;
                }


                var entity = _mapper.Map<NhHdnkPhuongAnNhapKhau>(Model);
                entity.NhDaGoiThaus = _mapper.Map<IEnumerable<NhDaGoiThau>>(ItemsGoiThau);
                if (Model.Id.IsNullOrEmpty())
                {
                    entity.Id = Guid.NewGuid();
                    entity.IIdPhuongAnNhapKhauGocId = entity.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = 0;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _service.Add(entity);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity.Id = Guid.NewGuid();
                    entity.IIdPhuongAnNhapKhauGocId = entity.Id;
                    foreach (var item in entity.NhDaGoiThaus)
                    {
                        item.IIdPhuongAnNhapKhauId = entity.Id;
                        item.Id = Guid.NewGuid();
                        _nhDaGoiThauService.Add(item);
                        item.IsAdded = false;
                        item.IsModified = true;
                   }    
                    entity.IIdParentId = Model.Id;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = entity.ILanDieuChinh + 1;
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

                e.Result = entity;

            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    // Reload data
                    if (!LengthValidate)
                    {
                        Model = _mapper.Map<NhHdnkPhuongAnNhapKhauModel>(e.Result);
                        IsDieuChinh = false;

                        // Invoke message
                        MessageBoxHelper.Info(Resources.MsgSaveDone);
                        Window window = obj as Window;
                        SavedAction?.Invoke(Model);
                        window.Close();
                    }
                    LengthValidate = false;
                    LoadData();

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
            if (!ItemsSoCuTrucTiep.IsEmpty()) ItemsSoCuTrucTiep.Clear();
            if (!ItemsKeHoachTongThe.IsEmpty()) ItemsKeHoachTongThe.Clear();
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
            if (!ItemsChuongTrinh.IsEmpty()) ItemsChuongTrinh.Clear();
            if (!ItemsDuAn.IsEmpty()) ItemsDuAn.Clear();
            if (!ItemsGoiThau.IsEmpty()) ItemsGoiThau.Clear();
            if (!ItemsTiGia.IsEmpty()) ItemsTiGia.Clear();
            if (!ItemsTiGiaChiTiet.IsEmpty()) ItemsTiGiaChiTiet.Clear();
            if (!ItemsDmNhaThau.IsEmpty()) ItemsDmNhaThau.Clear();
            if (!ItemsLoaiTienTe.IsEmpty()) ItemsLoaiTienTe.Clear();
            if (!GoiThauTemps.IsEmpty()) GoiThauTemps.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private bool Validate()
        {
            // validation form here ...
            List<string> lstError = new List<string>();
            
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if(string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                lstError.Add(string.Format(Resources.MsgCheckSoQD));
            }
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            //}
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckMaNgoaiTeNgoaiHoi));
            //}
            //if (Model.DNgayQuyetDinh != null && Model.DNgayQuyetDinh > DateTime.Now)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckNgayQDLonHon));
            //}
            if(_service.FindIndex(Model.ILoai).ToList().Select(n => n.SSoQuyetDinh).Contains(Model.SSoQuyetDinh) && IsAdd == true)
            {
                lstError.Add(string.Format(Resources.MsgTrungSoQD));
            }
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }

            return true;
        }

        private bool ValidateGoiThau()
        {
            List<string> lstError = new List<string>();
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            //}
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(Resources.MsgCheckMaNgoaiTeNgoaiHoi);
            //}
            if(SelectedChuongTrinh == null)
            {
                lstError.Add(Resources.MsgCheckChuongTrinh);
            }    
            if(SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if(string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                lstError.Add(Resources.MsgCheckSoQD);
            }    
            if(SelectedKeHoachTongThe == null)
            {
                lstError.Add(Resources.MsgCheckKeHoachTongThe);
            }    
            
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        //private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NhDaGoiThauModel item = (NhDaGoiThauModel)sender;
        //    Guid idTyGia = SelectedTiGia.Id;
        //    var dmTyGia = _iNhDmTiGiaService.FindById(idTyGia);
        //    var listTyGiaCT = _iNhDmTiGiaChiTietService.FindByTiGiaId(idTyGia);
        //    var rootCurrency = dmTyGia.SMaTienTeGoc;
        //    double value = 0;
        //    string ngoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
        //    switch (e.PropertyName)
        //    {
        //        case nameof(NhDaGoiThauModel.FGiaGoiThauUsd):
        //            value = Convert.ToDouble(item.FGiaGoiThauUsd.GetValueOrDefault());
        //            item.FGiaGoiThauVnd = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauEur = _iNhDmTiGiaService.CurrencyExchange("USD", "EUR", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauNgoaiTeKhac = _iNhDmTiGiaService.CurrencyExchange("USD", ngoaiTeKhac, rootCurrency, listTyGiaCT, value);
        //            break;
        //        case nameof(NhDaGoiThauModel.FGiaGoiThauVnd):
        //            value = Convert.ToDouble(item.FGiaGoiThauVnd.GetValueOrDefault());
        //            item.FGiaGoiThauUsd = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauEur = _iNhDmTiGiaService.CurrencyExchange("VND", "EUR", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauNgoaiTeKhac = _iNhDmTiGiaService.CurrencyExchange("VND", ngoaiTeKhac, rootCurrency, listTyGiaCT, value);
        //            break;
        //        case nameof(NhDaGoiThauModel.FGiaGoiThauEur):
        //            value = Convert.ToDouble(item.FGiaGoiThauEur.GetValueOrDefault());
        //            item.FGiaGoiThauVnd = _iNhDmTiGiaService.CurrencyExchange("EUR", "VND", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauUsd = _iNhDmTiGiaService.CurrencyExchange("EUR", "USD", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauNgoaiTeKhac = _iNhDmTiGiaService.CurrencyExchange("EUR", ngoaiTeKhac, rootCurrency, listTyGiaCT, value);
        //            break;
        //        case nameof(NhDaGoiThauModel.FGiaGoiThauNgoaiTeKhac):
        //            value = Convert.ToDouble(item.FGiaGoiThauNgoaiTeKhac.GetValueOrDefault());
        //            item.FGiaGoiThauVnd = _iNhDmTiGiaService.CurrencyExchange(ngoaiTeKhac, "VND", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauEur = _iNhDmTiGiaService.CurrencyExchange(ngoaiTeKhac, "EUR", rootCurrency, listTyGiaCT, value);
        //            item.FGiaGoiThauUsd = _iNhDmTiGiaService.CurrencyExchange(ngoaiTeKhac, "USD", rootCurrency, listTyGiaCT, value);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
