
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.App.Helper;
using AutoMapper;
using System.Windows;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo
{
    public class MSCNTForexContractInfoDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhDaHopDongModel>
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhHdnkCacQuyetDinhService _nhHdnkCacQuyetDinhService;
        private readonly INhDaHopDongNguonVonService _nhDaHopDongNguonVonService;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;
        private readonly INhDaHopDongHangMucService _nhDaHopDongHangMucService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhHdnkPhuongAnNhapKhauService _phuongAnNhapKhauService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        #endregion

        public override string Name => "THÔNG TIN HỢP ĐỒNG";
        public override string Title => "THÔNG TIN HỢP ĐỒNG";
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo.ForexContractInfoDialog);
        public bool TypeView => !(ILoai == 1 && IThuocMenu == 1);
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit => !(IsInsert || IsDieuChinh);
        public bool IsReadOnly { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public bool IsHieuChinhImport { get; set; }

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
                    LoadDonVi();
                    LoadChuongTrinh();
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
        private ObservableCollection<NhDaHopDongHangMucModel> _itemsHopDongHangMuc;
        public ObservableCollection<NhDaHopDongHangMucModel> ItemsHopDongHangMuc
        {
            get => _itemsHopDongHangMuc;
            set => SetProperty(ref _itemsHopDongHangMuc, value);
        }

        private ObservableCollection<NhDaHopDongHangMucModel> _originItems;
        public bool HasChanged => !ObjectCopier.ToJsonString(ItemsHopDongHangMuc).Equals(ObjectCopier.ToJsonString(_originItems));

        private NhDaHopDongHangMucModel _selectedHopDongHangMuc;
        public NhDaHopDongHangMucModel SelectedHopDongHangMuc
        {
            get => _selectedHopDongHangMuc;
            set => SetProperty(ref _selectedHopDongHangMuc, value);
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
                    LoadDsGoiThau();
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
            set
            {
                if (SetProperty(ref _selectedGoiThau, value))
                {
                    LoadSoQDAndSoPhuongAnNK();
                    LoadDsHangMuc();
                    if (!ItemsChiPhi.IsEmpty()) ItemsChiPhi.Clear();
                }
            }
        }

        private ObservableCollection<NhDaHopDongNguonVonModel> _itemsHopDongNguonVon;
        public ObservableCollection<NhDaHopDongNguonVonModel> ItemsHopDongNguonVon
        {
            get => _itemsHopDongNguonVon;
            set => SetProperty(ref _itemsHopDongNguonVon, value);
        }

        private NhDaHopDongNguonVonModel _selectedHopDongNguonVon;
        public NhDaHopDongNguonVonModel SelectedHopDongNguonVon
        {
            get => _selectedHopDongNguonVon;
            set => SetProperty(ref _selectedHopDongNguonVon, value);
        }

        private ObservableCollection<NhDaHopDongChiPhiModel> _itemsChiPhi;
        public ObservableCollection<NhDaHopDongChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private NhDaHopDongChiPhiModel _selectedChiPhi;
        public NhDaHopDongChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private string _sDonViUyThac;
        public string SDonViUyThac
        {
            get => _sDonViUyThac;
            set => SetProperty(ref _sDonViUyThac, value);
        }

        private string _sPhuongAnNhapKhau;
        public string SPhuongAnNhapKhau
        {
            get => _sPhuongAnNhapKhau;
            set => SetProperty(ref _sPhuongAnNhapKhau, value);
        }

        private string _sQuyetDinhChiTiet;
        public string SQuyetDinhChiTiet
        {
            get => _sQuyetDinhChiTiet;
            set => SetProperty(ref _sQuyetDinhChiTiet, value);
        }

        private ObservableCollection<ComboboxItem> _itemsContractType;
        public ObservableCollection<ComboboxItem> ItemsContractType
        {
            get => _itemsContractType;
            set => SetProperty(ref _itemsContractType, value);
        }

        private ComboboxItem _selectedContractType;
        public ComboboxItem SelectedContractType
        {
            get => _selectedContractType;
            set => SetProperty(ref _selectedContractType, value);
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGiaDialog;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGiaDialog
        {
            get => _itemsTiGiaDialog;
            set => SetProperty(ref _itemsTiGiaDialog, value);
        }

        private NhDmTiGiaModel _selectedTiGiaDialog;
        public NhDmTiGiaModel SelectedTiGiaDialog
        {
            get => _selectedTiGiaDialog;
            set
            {
                if (SetProperty(ref _selectedTiGiaDialog, value))
                {
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTietDialog;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTietDialog
        {
            get => _itemsTiGiaChiTietDialog;
            set => SetProperty(ref _itemsTiGiaChiTietDialog, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTietDialog;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTietDialog
        {
            get => _selectedTiGiaChiTietDialog;
            set
            {
                if (SetProperty(ref _selectedTiGiaChiTietDialog, value))
                {
                    IsVisibleTiGiaNhap = false;
                    LoadTiGia(1);
                }
            }
        }

        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (ItemsChiPhi != null)
                    {
                        CalculateModel();
                        CaculateHopDong();
                    }
                }
            }
        }

        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }

        public RelayCommand ShowHangMucDetailCommand { get; }
        public RelayCommand CellEditingCommand { get; }
        public MSCNTForexContractInfoItemsViewModel ForexContractInfoItemsViewModel { get; set; }
        public bool IsDieuChinhImport { get; private set; }
        public bool IsDetail { get; internal set; }
        public bool IsChecked { get; private set; }

        public MSCNTForexContractInfoDialogViewModel
        (
            ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhKhChiTietHopDongService nhKhChiTietHopDongService,
            INhKhChiTietService nhKhChiTietService,
            INhDaHopDongService nhDaHopDongService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhHdnkCacQuyetDinhService nhHdnkCacQuyetDinhService,
            INhDaHopDongNguonVonService nhDaHopDongNguonVonService,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService,
            INhDaHopDongHangMucService nhDaHopDongHangMucService,
            INsNguonNganSachService nsNguonVonService,
            INhDaGoiThauService nhDaGoiThauService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaDuAnService nhDaDuAnService,
            INhHdnkPhuongAnNhapKhauService phuongAnNhapKhauService,
            ILog logger,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            MSCNTForexContractInfoItemsViewModel forexContractInfoItemsViewModel
        )
        : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhHdnkCacQuyetDinhService = nhHdnkCacQuyetDinhService;
            _nhDaHopDongNguonVonService = nhDaHopDongNguonVonService;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;
            _nhDaHopDongHangMucService = nhDaHopDongHangMucService;
            _nsNguonVonService = nsNguonVonService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaDuAnService = nhDaDuAnService;
            _phuongAnNhapKhauService = phuongAnNhapKhauService;
            CellEditingCommand = new RelayCommand(obj => OnCellEditEnding(obj));
            ForexContractInfoItemsViewModel = forexContractInfoItemsViewModel;
        }

        public override void Init()
        {
            OnPropertyChanged(nameof(TypeView));
            LoadDefaul();
            LoadKeHoachTongThe();
            LoadDonVi();
            LoadChuongTrinh();
            LoadAttach();
            LoadContractType();
            LoadTiGia(1);
            LoadTiGiaChiTiet(1);
            LoadData();
        }

        private void LoadDefaul()
        {
            _sessionInfo = _sessionService.Current;
            _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();
        }

        private void LoadKeHoachTongThe()
        {
            IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(s => s.DNgayTao);
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
        public virtual void LoadTiGia(int i)
        {
            if (SelectedTiGiaChiTietDialog != null)
            {
                var data = _nhDmTiGiaService.FindAll().Where(x => x.SMaTienTeGoc.ToUpper() == SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()).ToList();
                var dataUSD = _nhDmTiGiaService.FindAll().Where(x => x.SMaTienTeGoc.ToUpper() == "USD");
                if (dataUSD != null)
                {
                    foreach (var item in dataUSD)
                    {
                        var lstChiTiet = _nhDmTiGiaChiTietService.FindAll().Where(x => x.IIdTiGiaId == item.Id).ToList();
                        if (lstChiTiet != null)
                        {
                            if (lstChiTiet.Any(x => x.SMaTienTeQuyDoi == SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()))
                            {
                                data.Add(item);
                            }
                        }
                    }
                }

                ItemsTiGiaDialog = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            }

        }
        public void LoadTiGiaChiTiet(int i)
        {
            _itemsTiGiaChiTietDialog = new ObservableCollection<NhDmTiGiaChiTietModel>();
            var data = _nhDmTiGiaChiTietService.FindAll().Where(x => x.SMaTienTeQuyDoi.ToUpper() != "USD" && x.SMaTienTeQuyDoi.ToUpper() != "VND").GroupBy(x => x.SMaTienTeQuyDoi.ToUpper()).Select(x => x.First());
            _itemsTiGiaChiTietDialog = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            _itemsTiGiaDialog = new ObservableCollection<NhDmTiGiaModel>();
            OnPropertyChanged(nameof(ItemsTiGiaChiTietDialog));
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            if (_selectedKeHoachTongThe != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> data = _nhKhTongTheNhiemVuChiService.FindAllDonViByKhTongTheId(_selectedKeHoachTongThe.Id).Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadChuongTrinh()
        {
            _itemsChuongTrinh = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> listKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndIdDonVi(_selectedKeHoachTongThe.Id, _selectedDonVi.Id).ToList();
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listKhTongTheNhiemVuChi);
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }
        
        private void LoadDuAn()
        {
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedChuongTrinh != null)
            {
                IEnumerable<NhDaDuAn> listDuAn = _nhDaDuAnService.FindAll(s => s.IIdKhttNhiemVuChiId == _selectedChuongTrinh.Id);
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadDsGoiThau()
        {
            _itemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            if (_selectedDuAn != null)
            {
                IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdDuAnId == _selectedDuAn.Id).Where(s => s.ILoai == 1 && s.IThuocMenu == 5);
                if (data.Any())
                {
                    _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                }
                if (_selectedChuongTrinh != null)
                {
                    List<NhDaGoiThauQuery> listNhDaGoiThauQuery = _nhKhTongTheNhiemVuChiService.FindByIdNhiemVuChi(_selectedChuongTrinh.Id).Where(s => s.ILoai == 1 && s.IThuocMenu == 5).ToList();
                    _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(listNhDaGoiThauQuery);
                }
            }
            else
            {
                // Nếu ko có dự án, thì lấy ds gói thầu theo NV chi
                if (_selectedChuongTrinh != null)
                {
                    IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdKhTongTheNhiemVuChiId == _selectedChuongTrinh.Id).Where(s => s.ILoai == 1 && s.IThuocMenu == 5);
                    if (data.Any())
                    {
                        _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                    }
                }
            }
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void CaculateHopDong()
        {
            if (!_itemsHopDongHangMuc.IsEmpty())
            {
                Model.FGiaTriUsd = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriUsd);
                Model.FGiaTriVnd = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriVnd);
                Model.FGiaTriEur = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriEur);
                Model.FGiaTriNgoaiTeKhac = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(Model));
            }
        }

        private void LoadSoQDAndSoPhuongAnNK()
        {
            SDonViUyThac = string.Empty;
            SPhuongAnNhapKhau = string.Empty;
            SQuyetDinhChiTiet = string.Empty;
            if (_selectedGoiThau != null)
            {
                if (_selectedGoiThau.IIdNhaThauId.HasValue)
                {
                    SDonViUyThac = _nhDmNhaThauService.FindById(_selectedGoiThau.IIdNhaThauId.Value)?.STenNhaThau;
                }
                if (_selectedGoiThau.IIdPhuongAnNhapKhauId.HasValue)
                {
                    SPhuongAnNhapKhau = _phuongAnNhapKhauService.FindById(_selectedGoiThau.IIdPhuongAnNhapKhauId.Value)?.SSoQuyetDinh;
                }
                if (_selectedGoiThau.IIdQuyetDinhChiTietId.HasValue)
                {
                    SQuyetDinhChiTiet = _nhHdnkCacQuyetDinhService.FindById(_selectedGoiThau.IIdQuyetDinhChiTietId.Value)?.SSoQuyetDinh;
                }
            }
        }

        private void LoadDsHangMuc()
        {
            _itemsHopDongHangMuc = new ObservableCollection<NhDaHopDongHangMucModel>();

            if (_selectedGoiThau != null)
            {
                if (Model.Id.IsNullOrEmpty())
                {
                    IEnumerable<NhDaGoiThauHopDongHangMucQuery> goithauhm = _nhDaHopDongHangMucService.FindByIdGoiThau(_selectedGoiThau.Id);

                    if (!goithauhm.IsEmpty())
                    {
                        var hangmucModels = goithauhm.Select(s => new NhDaHopDongHangMucModel
                        {
                            IsAdded = true,
                            IIdHangMucId = Guid.NewGuid(),
                            IIdGoiThauHangMucId = s.Id,
                            STenHangMuc = s.STenHangMuc,
                            STenChiPhi = s.STenChiPhi,
                            SMaHangMuc = s.SMaHangMuc,
                            IIdParentId = s.IIdParentId,
                            FGiaTriUsd = s.FTienGoiThauUsd,
                            FGiaTriVnd = s.FTienGoiThauVnd,
                            FGiaTriEur = s.FTienGoiThauEur,
                            FGiaTriNgoaiTeKhac = s.FTienGoiThauNgoaiTeKhac,
                            IsChecked = false,
                            IsModified = false
                        });
                        _itemsHopDongHangMuc = new ObservableCollection<NhDaHopDongHangMucModel>(hangmucModels);
                        _itemsHopDongHangMuc.ForAll(s =>
                        {
                            s.PropertyChanged += DsHangMuc_PropertyChanged;
                        });
                    }
                }
                else
                {
                    // Cập nhật hoặc điều chỉnh
                    IEnumerable<NhDaHopDongHangMuc> data = _nhDaHopDongHangMucService.FindByIdHopDong(Model.Id);

                    if (!data.IsEmpty())
                    {
                        _itemsHopDongHangMuc = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(data);
                        _itemsHopDongHangMuc.ForAll(s =>
                        {
                            if (s.IIdParentId == null && s.FGiaTriUsd < _itemsHopDongHangMuc.Where(y => y.IIdParentId == s.IIdGoiThauHangMucId).Sum(x => x.FGiaTriUsd))
                                s.IsNotCheckedAll = true;
                            s.IsChecked = true;
                            s.PropertyChanged += DsHangMuc_PropertyChanged;
                            if (IsDieuChinh)
                            {
                                s.IsAdded = true;
                            }
                            else s.IsModified = true;
                        });
                    }
                    IEnumerable<Guid?> idhangmucs = data.Select(d => d.IIdGoiThauHangMucId);
                    IEnumerable<NhDaGoiThauHopDongHangMucQuery> goithauhm = _nhDaHopDongHangMucService.FindByIdGoiThau(_selectedGoiThau.Id);
                    foreach (var item in goithauhm)
                    {
                        if (!idhangmucs.Contains(item.Id))
                            _itemsHopDongHangMuc.Add(new NhDaHopDongHangMucModel
                            {
                                IsAdded = true,
                                IIdHangMucId = Guid.NewGuid(),
                                IIdGoiThauHangMucId = item.Id,
                                STenHangMuc = item.STenHangMuc,
                                STenChiPhi = item.STenChiPhi,
                                SMaHangMuc = item.SMaHangMuc,
                                IIdParentId = item.IIdParentId,
                                FGiaTriUsd = item.FTienGoiThauUsd,
                                FGiaTriVnd = item.FTienGoiThauVnd,
                                FGiaTriEur = item.FTienGoiThauEur,
                                FGiaTriNgoaiTeKhac = item.FTienGoiThauNgoaiTeKhac,
                                IsChecked = false
                            });
                    }
                }
            }
            foreach (var item in _itemsHopDongHangMuc)
            {
                item.Order = string.Concat(item.STenChiPhi, item.SMaHangMuc);
            }
            _itemsHopDongHangMuc = new ObservableCollection<NhDaHopDongHangMucModel>(_itemsHopDongHangMuc.OrderBy(x => x.Order));
            int i = 0;
            _itemsHopDongHangMuc.ForAll(s =>
            {
                s.STT = ++i;
                s.PropertyChanged += DsHangMuc_PropertyChanged;
            });
            UpdateTreeItemsHangMuc();
            OnPropertyChanged(nameof(ItemsHopDongHangMuc));
            CaculateHopDong();
        }

        private void DsHangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaHopDongHangMucModel obj = (NhDaHopDongHangMucModel)sender;
            if (e.PropertyName == nameof(NhDaHopDongHangMucModel.IsChecked))
            {

                obj.IsModified = true;
                if (obj.IsChecked == true)
                    CheckHangMuc(obj);
                else if (obj.IsChecked == false)
                    UnCheckHangMuc(obj);

                OnPropertyChanged(nameof(HasChanged));
                CaculateHangMuc();
                CaculateHopDong();
            }
            else if (e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriNgoaiTeKhac)))
            {

                OnPropertyChanged(nameof(HasChanged));
                CaculateHangMuc();
                CaculateHopDong();
            }
        }

        public void ChiPhi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedChiPhi = (NhDaHopDongChiPhiModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedChiPhi.CanEditValue;
            }
        }

        private List<NhDaHopDongHangMucModel> LoadHopDongThauHangMucs(NhDaHopDongChiPhiModel chiPhiModel)
        {
            var result = new List<NhDaHopDongHangMucModel>();
            if (Model.Id.IsNullOrEmpty())
            {
                if (chiPhiModel != null)
                {
                    IEnumerable<NhDaGoiThauHangMuc> data = _nhDaGoiThauHangMucSerrvice.FindAll(s => s.IIdGoiThauChiPhiId == chiPhiModel.IIdGoiThauChiPhiId);
                    result = data?.Select(s => new NhDaHopDongHangMucModel
                    {
                        Id = Guid.NewGuid(),
                        IIdHopDongChiPhiId = chiPhiModel.Id,
                        IIdGoiThauHangMucId = s.Id,
                        IIdParentId = s.IIdParentId,
                        FGiaTriUsd = s.FTienGoiThauUsd,
                        FGiaTriVnd = s.FTienGoiThauVnd,
                        FGiaTriEur = s.FTienGoiThauEur,
                        FGiaTriNgoaiTeKhac = s.FTienGoiThauNgoaiTeKhac,
                        STenHangMuc = s.STenHangMuc,
                        SMaHangMuc = StringUtils.ConvertMaOrder(s.SMaOrder),
                        SMaOrder = s.SMaOrder,
                        IsAdded = true
                    }).ToList();
                }
            }
            else
            {
                // Cập nhật hoặc điều chỉnh
                var data = _nhDaHopDongHangMucService.FindAll(s => s.IIdHopDongChiPhiId == chiPhiModel.Id);
                result = _mapper.Map<List<NhDaHopDongHangMucModel>>(data);
                result.ForEach(s =>
                {
                    if (IsDieuChinh)
                    {
                        s.IsAdded = true;
                    }
                });
            }

            return result;
        }

        private void UpdateTreeItemsHangMuc()
        {
            if (!ItemsHopDongHangMuc.IsEmpty())
            {
                ItemsHopDongHangMuc.ForAll(s => s.CanEditValue = !ItemsHopDongHangMuc.Any(y => y.IIdParentId == s.IIdGoiThauHangMucId));
                ItemsHopDongHangMuc.ForAll(x =>
                {
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsHopDongHangMuc.Any(y => y.IIdGoiThauHangMucId == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsHopDongHangMuc.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }
        private void UpdateTreeItemsChiPhi()
        {
            if (!ItemsChiPhi.IsEmpty())
            {
                ItemsChiPhi.ForAll(s => s.CanEditValue = !ItemsChiPhi.Any(y => y.IIdParentId == s.IIdGoiThauChiPhiId));
                ItemsChiPhi.ForAll(x =>
                {
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.IIdGoiThauChiPhiId == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsChiPhi.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        public override void LoadData(params object[] args)
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                if (IsReadOnly)
                {
                    Description = "Xem thông tin hợp đồng";
                }
                else
                {
                    Description = "Cập nhật thông tin hợp đồng";
                    if (IsHieuChinhImport)
                    {
                        LoadHopDongImportHieuChinh();
                        return;
                    }
                }
            }
            else
            {
                Description = "Thêm mới thông tin hợp đồng";
                if (IsDieuChinhImport)
                {
                    LoadThongTinImportHieuChinh();
                    return;
                }
            }
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            if (SelectedKeHoachTongThe != null)
            {
                LoadDonVi();
                LoadChuongTrinh();
            }
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id.Equals(Model.IIdDonViQuanLyId));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            LoadDsGoiThau();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsHangMuc();

            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            IsVisibleTiGiaNhap = SelectedTiGiaDialog != null;
            FTiGiaNhap = Model.FTiGiaNhap;
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
            OnPropertyChanged(nameof(Description));
        }

        private void LoadHopDongImportHieuChinh()
        {
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            LoadDsGoiThau();
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsHangMuc();
            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
            OnPropertyChanged(nameof(Description));
        }

        private void LoadThongTinImportHieuChinh()
        {
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            LoadDsGoiThau();
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsHangMuc();
            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
            OnPropertyChanged(nameof(Description));
        }



        private bool ValidateNguonVon()
        {
            List<string> lstError = new List<string>();
            if (SelectedTiGia == null)
            {
                lstError.Add("Vui lòng chọn tỉ giá");
            }
            if (SelectedTiGiaChiTiet == null)
            {
                lstError.Add("Vui lòng chọn mã ngoại tệ khác");
            }
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool ValidationData()
        {
            List<string> lstError = new List<string>();
            //if (SelectedKeHoachTongThe == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn số kế hoạch tổng thể BQP"));
            //}
            //if (SelectedDonVi == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn đơn vị quản lý"));
            //}
            //if (SelectedChuongTrinh == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn chương trình"));
            //}
            //if (SelectedGoiThau == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn gói thầu"));
            //}
            //if (string.IsNullOrEmpty(Model.SSoHopDong))
            //{
            //    lstError.Add(string.Format("Nhập số hợp đồng"));
            //}
            //if (string.IsNullOrEmpty(Model.STenHopDong))
            //{
            //    lstError.Add(string.Format("Nhập tên hợp đồng"));
            //}
            //if (Model.DNgayHopDong == null)
            //{
            //    lstError.Add(string.Format("Nhập ngày ban hành"));
            //}
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(string.Format("Chọn tỉ giá"));
            //}
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn mã ngoại tệ khác"));
            //}
            if (SelectedTiGiaDialog == null && SelectedTiGiaChiTietDialog != null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            }
            if (Model.DKetThucDuKien <= Model.DKhoiCongDuKien)
            {
                lstError.Add(Resources.MsgContractDateTime);
            }
            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }
        public override void OnSave(object obj)
        {
            SetDataModel();
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidationData()) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                Model.HopDongHangMucs = ItemsHopDongHangMuc;
                Model.ILoai = ILoai;
                Model.IThuocMenu = IThuocMenu;
                Model.IsChecked = IsChecked;
                Model.FTiGiaNhap = FTiGiaNhap;
                Model.FGiaTriHopDongUSD = Model.FGiaTriUsd;
                Model.FGiaTriHopDongEUR = Model.FGiaTriEur;
                Model.FGiaTriHopDongVND = Model.FGiaTriVnd;
                Model.FGiaTriHopDongNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac;
                foreach (var item in Model.HopDongHangMucs)
                {
                    if (item.IsChecked == false)
                        item.IsDeleted = true;
                    else
                        item.IsDeleted = false;
                }
                NhDaHopDong entity = _mapper.Map<NhDaHopDong>(Model);
                if (IsInsert)
                {
                    // insert
                    entity.Id = Guid.NewGuid();
                    Model.Id = entity.Id;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.ILanDieuChinh = 0;
                    entity.BIsActive = true;
                    entity.BIsKhoa = false;
                    entity.BIsGoc = true;

                    _nhDaHopDongService.Add(entity);
                    IsInsert = false;
                    e.Result = entity;
                }
                else if (IsEdit)
                {
                    // update
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;

                    _nhDaHopDongService.Update(entity);
                    e.Result = entity;
                }
                else
                {
                    // adjust
                    if (IsDieuChinh)
                    {
                        entity.Id = Guid.NewGuid();
                        entity.IIdParentAdjustId = Model.Id;
                        entity.ILanDieuChinh += 1;
                        entity.BIsActive = true;
                        entity.BIsKhoa = false;
                        entity.BIsGoc = false;
                        entity.DNgayTao = DateTime.Now;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        entity.IIdParentId = Model.IIdParentId != null ? Model.IIdParentId : Model.Id;
                        Model.Id = entity.Id;

                        _nhDaHopDongService.Adjust(entity);
                        e.Result = entity;
                    }
                }
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    SavedAction?.Invoke(Model);
                    LoadData();
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    if (obj is Window window)
                    {
                        Dispose();
                        window.Close();
                    };
                }
                else
                {
                    MessageBoxHelper.Info(e.Error.Message);
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void SetDataModel()
        {
            if (SelectedKeHoachTongThe != null)
            {
                Model.IIdKhTongTheId = SelectedKeHoachTongThe.Id;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = SelectedDuAn.Id;
            }
            if (SelectedGoiThau != null)
            {
                Model.IIdGoiThauId = SelectedGoiThau.Id;
                Model.IIdNhaThauThucHienId = SelectedGoiThau.IIdNhaThauId;
            }
            if (SelectedChuongTrinh != null)
            {
                Model.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.Id;
            }
            if (SelectedDonVi != null)
            {
                Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
            }
            if (SelectedContractType != null)
            {
                Model.IIdLoaiHopDongId = SelectedContractType.Id;
            }
            if (SelectedTiGiaDialog != null)
            {
                Model.IIdTiGiaId = SelectedTiGiaDialog.Id;
            }
            if (SelectedTiGiaChiTietDialog != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi;
            }
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedHopDongHangMuc = (NhDaHopDongHangMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedHopDongHangMuc.CanEditValue;
            }
        }
        public bool CheckChildHangMucParent(NhDaHopDongHangMucModel obj)
        {
            if (obj.IIdParentId == null)
            {
                if (_itemsHopDongHangMuc.Where(s => s.IIdParentId == obj.IIdGoiThauHangMucId) != null)
                    return true;
                else
                    return false;
            }
            else
            {
                foreach (var item in _itemsHopDongHangMuc)
                {
                    if (item.IIdParentId == obj.IIdParentId && item.IsChecked == true)
                        return false;
                }
                return true;
            }
        }

        public void CheckHangMuc(NhDaHopDongHangMucModel obj)
        {
            if (obj == null) return;
            if (obj.IIdParentId == null)
            {
                if (CheckChildHangMucParent(obj) && obj.IsNotCheckedAll == false)
                    _itemsHopDongHangMuc.Where(s => s.IIdParentId == obj.IIdGoiThauHangMucId).ForAll(n => n.IsChecked = true);
            }
            else
            {
                foreach (var item in _itemsHopDongHangMuc)
                {
                    if (item.IIdGoiThauHangMucId == obj.IIdParentId)
                    {
                        item.IsNotCheckedAll = true;
                        item.IsChecked = true;
                    }
                }
            }

        }

        public void UnCheckHangMuc(NhDaHopDongHangMucModel obj)
        {
            int s;
            if (obj == null) return;
            if (obj.IIdParentId == null)
            {
                if (SelectedHopDongHangMuc.IIdParentId == null && CheckChildHangMucParent(obj))
                {
                    _itemsHopDongHangMuc.Where(s => s.IIdParentId == obj.IIdGoiThauHangMucId).ForAll(n => n.IsChecked = false);
                    _itemsHopDongHangMuc.Where(s => s.IIdGoiThauHangMucId == obj.IIdGoiThauHangMucId).FirstOrDefault().IsNotCheckedAll = false;
                }

            }
            else if (CheckChildHangMucParent(obj))
            {
                foreach (var item in _itemsHopDongHangMuc)
                {
                    if (item.IIdGoiThauHangMucId == obj.IIdParentId)
                    {
                        item.IsNotCheckedAll = false;
                        item.IsChecked = false;
                    }
                }
            }
        }
        private void LoadContractType()
        {
            var lstContractType = _nhDmLoaiHopDongService.FindAll();
            if (lstContractType == null) return;
            var drpItem = lstContractType.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIdLoaiHopDongId.ToString(),
                DisplayItem = n.STenLoaiHopDong,
                Id = n.IIdLoaiHopDongId
            });
            _itemsContractType = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(ItemsContractType));
        }

        public bool CheckHangMucCanEditGiatri()
        {
            var item = SelectedHopDongHangMuc;
            if (item == null) return false;
            var lstChild = FindListChildHangMuc(item.IIdGoiThauHangMucId);
            if (lstChild == null || lstChild.Count == 0)
            {
                return true;
            }
            return false;
        }

        public List<NhDaHopDongHangMucModel> FindListChildHangMuc(Guid? parentId)
        {
            List<NhDaHopDongHangMucModel> inner = new List<NhDaHopDongHangMucModel>();
            if (parentId != null)
            {

                foreach (var t in ItemsHopDongHangMuc.Where(item => item.IIdParentId == parentId))
                {
                    inner.Add(t);
                    inner = inner.Union(FindListChildHangMuc(t.IIdGoiThauHangMucId)).ToList();
                }
            }
            return inner;
        }

        private void CaculateHangMuc()
        {
            if (!_itemsHopDongHangMuc.IsEmpty())
            {
                var parents = ItemsHopDongHangMuc.Where(x => x.IIdParentId.IsNullOrEmpty() || !ItemsHopDongHangMuc.Any(y => y.IIdGoiThauHangMucId == x.IIdParentId));
                foreach (var item in parents)
                {
                    CalculateHangMuc(item);
                }
            }
        }

        private void CalculateHangMuc(NhDaHopDongHangMucModel parentItem)
        {
            var childs = ItemsHopDongHangMuc.Where(x => x.IIdParentId == parentItem.IIdGoiThauHangMucId && x.IsChecked == true);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                }
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        private void HopDongHangMucCurrencyExchange(object obj)
        {
            OnCellEditEnding(obj);
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public override void OnCellEditEnding(object obj)
        {
            if (obj is DataGridCellEditEndingEventArgs e)
            {
                // Tính toán chuyển đổi tiền tệ
                if (e.EditAction == DataGridEditAction.Commit && e.Row.Item is CurrencyDetailModelBase item)
                {
                    string propertyName = e.Column.SortMemberPath;
                    if (propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriNgoaiTeKhac)))
                    {
                        if (FTiGiaNhap != null)
                        {
                            item.FGiaTriUsd = item.FGiaTriNgoaiTeKhac * FTiGiaNhap;
                        }
                    }
                    else if (propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriUsd)))
                    {
                        if (FTiGiaNhap != null && FTiGiaNhap != 0)
                        {
                            item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                        }
                    }
                }
            }
        }
        private void CalculateModel()
        {

            if (ItemsHopDongHangMuc != null && FTiGiaNhap != null)
            {
                foreach (var item in ItemsHopDongHangMuc)
                {
                    item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                }
            }
        }
        private void ShowTiGiaNhap()
        {
            if (SelectedTiGiaDialog != null)
            {
                IEnumerable<NhDmTiGiaChiTiet> tiGiaChiTietList = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGiaDialog.Id);
                NhDmTiGiaChiTiet tiGiaChiTietObj = null;
                if (SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() == "USD")
                {
                    tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()));
                }
                else
                {
                    tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(LoaiTienTeEnum.TypeCode.USD));
                }
                if (tiGiaChiTietObj != null)
                {
                    if (SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() == "USD")
                    {
                        if (tiGiaChiTietObj.FTiGia != null && tiGiaChiTietObj.FTiGia != 0)
                            FTiGiaNhap = 1 / tiGiaChiTietObj.FTiGia;

                        Model.SNgoaiTeGoc = "1 " + SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper() + " ";
                    }
                    else
                    {
                        Model.SNgoaiTeGoc = "1 " + SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() + " ";
                        FTiGiaNhap = tiGiaChiTietObj.FTiGia;
                    }

                }
            }
        }
    }
}
