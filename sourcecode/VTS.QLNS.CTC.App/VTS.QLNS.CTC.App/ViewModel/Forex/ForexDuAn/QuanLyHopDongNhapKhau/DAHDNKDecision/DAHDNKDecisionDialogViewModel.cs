using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;
using System.Windows;
using VTS.QLNS.CTC.App.Properties;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision
{
    public class DAHDNKDecisionDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhQuyetDinhDamPhamModel>
    {
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INhHdnkCacQuyetDinhService _quyetDinhService;
        private readonly INhHdnkCacQuyetDinhNguonVonService _quyetDinhNguonVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private readonly INhHdnkCacQuyetDinhChiPhiService _quyetDinhChiPhiService;
        private readonly INhHdnkCacQuyetDinhChiPhiHangMucService _quyetDinhChiPhiHangMucService;
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhHdnkPhuongAnNhapKhauService _phuongAnNhapKhauService;
        private readonly INhDmNhaThauService _dmNhaThauService;
        private readonly INhDmLoaiTienTeService _dmLoaiTienTeService;
        private SessionInfo _sessionInfo;

        public override string Name => "QUYẾT ĐỊNH PHÊ DUYỆT CHI TIẾT";
        public override string Title => "QUYẾT ĐỊNH PHÊ DUYỆT CHI TIẾT";
        public override Type ContentType => typeof(View.Forex.ForexDuAn.QuanLyHopDongNhapKhau.DAHDNKDecision.DAHDNKDecisionDialog);
        public int ILoai { get; set; }
        public bool IsViewDuAn => ILoai != 1;
        public bool IsEnableLoaiQD => ILoai == 1;

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
                    LoadPhuongAnNhapKhau();
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
                    LoadPhuongAnNhapKhau();
                }
            }
        }

        private ObservableCollection<NhHdnkPhuongAnNhapKhauModel> _itemsPhuongAnNhapKhau;
        public ObservableCollection<NhHdnkPhuongAnNhapKhauModel> ItemsPhuongAnNhapKhau
        {
            get => _itemsPhuongAnNhapKhau;
            set => SetProperty(ref _itemsPhuongAnNhapKhau, value);
        }

        private NhHdnkPhuongAnNhapKhauModel _selectedPhuongAnNhapKhau;
        public NhHdnkPhuongAnNhapKhauModel SelectedPhuongAnNhapKhau
        {
            get => _selectedPhuongAnNhapKhau;
            set
            {
                if (SetProperty(ref _selectedPhuongAnNhapKhau, value))
                {
                    LoadDsGoiThau();
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

        private string _textHeader;
        public string TextHeader
        {
            get => _textHeader;
            set => SetProperty(ref _textHeader, value);
        }

        private ObservableCollection<ComboboxItem> _itemsDecisionType;
        public ObservableCollection<ComboboxItem> ItemsDecisionType
        {
            get => _itemsDecisionType;
            set => SetProperty(ref _itemsDecisionType, value);
        }

        private ComboboxItem _selectedDecisionType;
        public ComboboxItem SelectedDecisionType
        {
            get => _selectedDecisionType;
            set
            {
                if (SetProperty(ref _selectedDecisionType, value))
                {
                    if (_selectedDecisionType != null)
                    {
                        TextHeader = $"Giá trị {_selectedDecisionType.DisplayItem}";
                    }
                }
            }
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
                    if (value is null)
                    {
                        IsVisibleTiGiaNhap = false;
                        FTiGiaNhap = 0;
                        Model.SNgoaiTeGoc = string.Empty;
                    }
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
                    if (value is null)
                    {
                        FTiGiaNhap = 0;
                        Model.SNgoaiTeGoc = string.Empty;
                    }
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
                    if (ItemsGoiThau != null)
                    {
                        CalculateModel();
                        CaculatorGiaTriPheDuyet();
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
        public RelayCommand ShowChiTietGoiThauCommand { get; }
        public RelayCommand CellEditingCommand { get; }

        public DAHDNKDecisionDialogDetailViewModel DAHDNKDecisionDialogDetailViewModel { get; }

        public DAHDNKDecisionDialogViewModel
        (
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhHdnkCacQuyetDinhService nhHdnkCacQuyetDinhService,
            INsDonViService nsDonViService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INsNguonNganSachService nsNguonNganSachService,
            INhHdnkCacQuyetDinhNguonVonService quyetDinhNguonVonService,
            INhDmChiPhiService nhDmChiPhiService,
            INhHdnkCacQuyetDinhChiPhiService nhHdnkCacQuyetDinhChiPhiService,
            INhHdnkCacQuyetDinhChiPhiHangMucService nhHdnkCacQuyetDinhChiPhiHangMucService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDaQdDauTuService nhDaQdDauTuService,
            INhDaDuAnService nhDaDuAnService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaGoiThauService nhDaGoiThauService,
            INhHdnkPhuongAnNhapKhauService phuongAnNhapKhauService,
            INhDmNhaThauService dmNhaThauService,
            INhDmLoaiTienTeService dmLoaiTienTeService,

            DAHDNKDecisionDialogDetailViewModel DAHDNKdecisionDialogDetailViewModel
        )
        : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _quyetDinhService = nhHdnkCacQuyetDinhService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nsNguonVonService = nsNguonNganSachService;
            _quyetDinhNguonVonService = quyetDinhNguonVonService;
            _nhDmChiPhiService = nhDmChiPhiService;
            _quyetDinhChiPhiService = nhHdnkCacQuyetDinhChiPhiService;
            _quyetDinhChiPhiHangMucService = nhHdnkCacQuyetDinhChiPhiHangMucService;
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaGoiThauService = nhDaGoiThauService;
            _phuongAnNhapKhauService = phuongAnNhapKhauService;
            _dmNhaThauService = dmNhaThauService;
            _dmLoaiTienTeService = dmLoaiTienTeService;

            DAHDNKDecisionDialogDetailViewModel = DAHDNKdecisionDialogDetailViewModel;

            ShowChiTietGoiThauCommand = new RelayCommand(obj => OnShowGoiThau());
            CellEditingCommand = new RelayCommand(obj => OnCellEditEnding(obj));
            SaveCommand = new RelayCommand(obj => OnSave(obj));
        }

        public override void Init()
        {
            LoadDefault();
            LoadLoaiQuyetDinh();
            LoadKeHoachTongThe();
            LoadDonVi();
            LoadTiGia(1);
            LoadTiGiaChiTiet(1);
            LoadDmNhaThau();
            LoadLoaiTienTe();
            LoadData();
            TextHeader = "Giá trị Phê duyệt kết quả đàm phán";
        }

        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }

        private void LoadLoaiQuyetDinh()
        {
            ObservableCollection<ComboboxItem> comboboxItems = new ObservableCollection<ComboboxItem>();
            comboboxItems.Add(new ComboboxItem() { ValueItem = "1", DisplayItem = "Phê duyệt kết quả đàm phán" });
            comboboxItems.Add(new ComboboxItem() { ValueItem = "2", DisplayItem = "Phê duyệt chi trong nước" });
            comboboxItems.Add(new ComboboxItem() { ValueItem = "3", DisplayItem = "Phê duyệt chi đoàn ra" });
            _itemsDecisionType = comboboxItems;
            OnPropertyChanged(nameof(ItemsDecisionType));
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
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
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

        private void LoadDuAn()
        {
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedChuongTrinh != null)
            {
                IEnumerable<NhDaDuAn> listDuAn = _nhDaDuAnService.FindAll(s => s.IIdKhttNhiemVuChiId == _selectedChuongTrinh.Id && s.ILoai == 2);
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadPhuongAnNhapKhau()
        {
            _itemsPhuongAnNhapKhau = new ObservableCollection<NhHdnkPhuongAnNhapKhauModel>();
            if (_selectedDuAn != null)
            {
                IEnumerable<NhHdnkPhuongAnNhapKhau> data = _phuongAnNhapKhauService.FindIndex().Where(s => s.IIdDuAnId == _selectedDuAn.Id && s.BIsActive == true && s.ILoai == 3);
                _itemsPhuongAnNhapKhau = _mapper.Map<ObservableCollection<NhHdnkPhuongAnNhapKhauModel>>(data);
            }
            else
            {
                // Nếu ko hiển thị dự án, thì load PANK theo Kế hoạch tổng thể NV Chi
                if (_selectedChuongTrinh != null)
                {
                    IEnumerable<NhHdnkPhuongAnNhapKhau> data = _phuongAnNhapKhauService.FindIndex().Where(s => s.IIdKhttNhiemVuChiId == _selectedChuongTrinh.Id && s.BIsActive==true && s.ILoai == 3).Where(x => x.SLoaiSoCu == SO_CU_TRUC_TIEP.CHUONG_TRINH);
                    _itemsPhuongAnNhapKhau = _mapper.Map<ObservableCollection<NhHdnkPhuongAnNhapKhauModel>>(data);
                }
            }
            OnPropertyChanged(nameof(ItemsPhuongAnNhapKhau));
        }

        private void LoadDsGoiThau()
        {
            _itemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            if (_selectedPhuongAnNhapKhau != null)
            {
                IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdPhuongAnNhapKhauId == _selectedPhuongAnNhapKhau.Id);
                if (data.Any())
                {
                    if (Model.Id.IsNullOrEmpty())
                    {
                        // Nếu là tạo mới qđct cho cùng 1 PANK thì filter danh sách gói thầu đã có qđct.
                        data = data.Where(s => !s.IIdQuyetDinhChiTietId.HasValue);
                    }
                    else // chinh sua/xem
                    {
                        //KhaiPD update 05/12/2022
                        if (Model.BIsActive)
                        {
                            data = data.Where(s => s.IIdQuyetDinhChiTietId == Model.Id || !s.IIdQuyetDinhChiTietId.HasValue);
                        }
                        else
                        {
                            data = data.Where(s => s.IIdQuyetDinhChiTietId == Model.IIdParentAdjustId || !s.IIdQuyetDinhChiTietId.HasValue);
                        }
                    }
                    _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                    foreach (var item in _itemsGoiThau)
                    {
                        item.IsChecked = item.IIdQuyetDinhChiTietId.HasValue;
                        item.PropertyChanged += GoiThau_PropertyChanged;
                    }
                }
            }
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void GoiThau_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhDaGoiThauModel objectSender = (NhDaGoiThauModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaGoiThauModel.IsChecked))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.IIdNhaThauId))
                || e.PropertyName.Equals(nameof(NhDaGoiThauModel.IIdDonViTienTeId)))
            {
                objectSender.IsModified = true;
            }
            CaculatorGiaTriPheDuyet();
        }

        public void DanhSachGoiThau_BeginningEdit(DataGridBeginningEditEventArgs e)
        {
            NhDaGoiThauModel objectSender = (NhDaGoiThauModel)e.Row.Item;
            if (Model.Id.IsNullOrEmpty() && objectSender.IIdQuyetDinhChiTietId.HasValue)
            {
                e.Cancel = true;
            }
        }

        public override void OnCellEditEnding(object obj)
        {
            if (obj is DataGridCellEditEndingEventArgs e)
            {
                // Tính toán chuyển đổi tiền tệ
                if (e.EditAction == DataGridEditAction.Commit && e.Row.Item is NhDaGoiThauModel item)
                {
                    string propertyName = e.Column.SortMemberPath;
                    if (propertyName.Equals(nameof(NhDaGoiThauModel.FGiaQuyetDinhChiTietNgoaiTeKhac)))
                    {
                        if (FTiGiaNhap != null)
                        {
                            item.FGiaQuyetDinhChiTietUsd = item.FGiaQuyetDinhChiTietNgoaiTeKhac * FTiGiaNhap;
                        }
                    }
                    else if (propertyName.Equals(nameof(NhDaGoiThauModel.FGiaQuyetDinhChiTietUsd)))
                    {
                        if (FTiGiaNhap != null)
                        {
                            item.FGiaQuyetDinhChiTietNgoaiTeKhac = FTiGiaNhap == 0 ? 0 : item.FGiaQuyetDinhChiTietUsd / FTiGiaNhap;
                        }
                    }
                }
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

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Description = "Thêm mới quyết định";
            }
            else if (IsDieuChinh)
            {
                Description = "Điều chỉnh quyết định";
            }
            else
            {
                if (BIsReadOnly)
                {
                    Description = "Xem thông tin quyết định";
                }
                else
                {
                    Description = "Cập nhật quyết định";
                }
            }

            if (IsViewDuAn)
            {
                _selectedDecisionType = _itemsDecisionType.FirstOrDefault(s => s.ValueItem == "1");
            }
            else
            {
                _selectedDecisionType = _itemsDecisionType.FirstOrDefault(s => s.ValueItem == Model.ILoaiQuyetDinh.ToString());
            }
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id.Equals(Model.IIdKhTongTheId));
            LoadDonVi();
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id.Equals(Model.IIdDonViQuanLy));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id.Equals(Model.IIdKhTongTheNhiemVuChiId));
            LoadDuAn();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id.Equals(Model.IIdDuAnId));
            LoadPhuongAnNhapKhau();
            _selectedPhuongAnNhapKhau = _itemsPhuongAnNhapKhau.FirstOrDefault(s => s.Id.Equals(Model.IIdPhuongAnNhapKhauId));
            LoadDsGoiThau();
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            IsVisibleTiGiaNhap = SelectedTiGiaDialog != null;
            FTiGiaNhap = Model.FTiGiaNhap;

            OnPropertyChanged(nameof(SelectedDecisionType));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedPhuongAnNhapKhau));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
        }

        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (_selectedKeHoachTongThe == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckKeHoachTongThe));
            }
            if (_selectedDonVi == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckDonVi));
            }
            if (_selectedChuongTrinh == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckChuongTrinh));
            }
            //if (_selectedDuAn == null && IsViewDuAn)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckDuAn));
            //}
            if (_selectedPhuongAnNhapKhau == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckPhuongAnNhapKhau));
            }
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                lstError.Add(string.Format(Resources.MsgCheckSoQD));
            }
            else
            {
                var listQuyetDinh = _quyetDinhService.FindByCondition(ILoai).Where(x => x.SSoQuyetDinh == Model.SSoQuyetDinh).FirstOrDefault();
                if (listQuyetDinh != null && Model.Id.IsNullOrEmpty())
                {
                    lstError.Add(string.Format(Resources.MsgTrungSoQD));
                }
            }
            if (Model.DNgayQuyetDinh != null)
            {
                if (DateTime.Now < Model.DNgayQuyetDinh)
                {
                    lstError.Add(string.Format(Resources.MsgCheckNgayDeNghiNhoHon));
                }
            }
            if (SelectedTiGiaDialog == null && SelectedTiGiaChiTietDialog != null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            }
            //if (SelectedTiGia == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            //}
            //if (SelectedTiGiaChiTiet == null)
            //{
            //    lstError.Add(string.Format(Resources.MsgCheckMaNgoaiTeNgoaiHoi));
            //}
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void OnShowGoiThau()
        {
            DAHDNKDecisionDialogDetailViewModel.Model = SelectedGoiThau;
            DAHDNKDecisionDialogDetailViewModel.SoQuetDinhChiTiet = Model.SSoQuyetDinh;
            DAHDNKDecisionDialogDetailViewModel.Init();
            DAHDNKDecisionDialogDetailViewModel.ShowDialog();
        }

        public override void OnSave(object obj)
        {
            if (!ValidateData()) return;

            if (SelectedDecisionType != null)
            {
                Model.ILoaiQuyetDinh = int.Parse(SelectedDecisionType.ValueItem);
            }
            Model.IIdKhTongTheId = SelectedKeHoachTongThe?.Id;
            Model.IIdDonViQuanLy = SelectedDonVi?.Id;
            Model.IIdMaDonViQuanLy = SelectedDonVi?.IIDMaDonVi;
            Model.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh?.Id;
            Model.IIdDuAnId = SelectedDuAn?.Id;
            Model.IIdPhuongAnNhapKhauId = SelectedPhuongAnNhapKhau?.Id;
            Model.IIdTiGiaId = SelectedTiGiaDialog?.Id;
            Model.SMaNgoaiTeKhac = SelectedTiGiaChiTietDialog?.SMaTienTeQuyDoi;
            Model.ILoai = ILoai;
            Model.FTiGiaNhap = FTiGiaNhap;
            if (!ValidateViewModelHelper.Validate(Model)) return;

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Lưu những gói thầu có đưa vào quyết định chi tiết IsChecked = true
                var listGoiThau = _mapper.Map<IEnumerable<NhDaGoiThau>>(ItemsGoiThau.Where(s => s.IsChecked));

                NhHdnkCacQuyetDinh entity;
                //= _mapper.Map<NhHdnkCacQuyetDinh>(Model);
                if (Model.Id.IsNullOrEmpty())
                {
                    entity = _mapper.Map<NhHdnkCacQuyetDinh>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.BIsActive = true;
                    entity.BIsGoc = true;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.ILanDieuChinh = 0;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _mapper.Map(entity, Model);
                    _quyetDinhService.Add(entity);

                    // Update ds gói thầu được đưa vào quyết định
                    listGoiThau.ForAll(s => s.IIdQuyetDinhChiTietId = entity.Id);
                    _nhDaGoiThauService.UpdateRange(listGoiThau);
                }
                else if (IsDieuChinh)
                {
                    // Điều chỉnh
                    entity = _mapper.Map<NhHdnkCacQuyetDinh>(Model);
                    entity.Id = Guid.NewGuid();
                    entity.IIdParentId = Model.Id;
                    entity.IIdParentAdjustId = Model.Id;
                    entity.ILanDieuChinh++;
                    entity.BIsActive = true;
                    entity.BIsGoc = false;
                    entity.BIsKhoa = false;
                    entity.BIsXoa = false;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _mapper.Map(entity, Model);
                    _quyetDinhService.Adjust(entity); // Cần confirm lại
                    // Update ds gói thầu được đưa vào quyết định
                    listGoiThau.ForAll(s =>
                    {
                        //KhaiPD update 05/12/2022
                        s.IIdParentAdjustId = s.IIdQuyetDinhChiTietId;
                        s.IIdQuyetDinhChiTietId = entity.Id;
                    });
                    _nhDaGoiThauService.UpdateRange(listGoiThau);
                }
                else
                {
                    entity = _quyetDinhService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _quyetDinhService.Update(entity);
                    listGoiThau.ForAll(s => s.IIdQuyetDinhChiTietId = entity.Id);
                    //listGoiThau.Where(s => s.IIdQuyetDinhChiTietId == entity.Id && s.IIdQuyetDinhChiTietId.HasValue);
                    var listGoiThauNotCheck = _mapper.Map<IEnumerable<NhDaGoiThau>>(ItemsGoiThau.Where(s => !s.IsChecked && s.IIdQuyetDinhChiTietId != null));
                    foreach (var item in listGoiThauNotCheck)
                    {
                        item.IIdQuyetDinhChiTietId = null;
                        item.FGiaQuyetDinhChiTietUsd = null;
                        item.FGiaQuyetDinhChiTietVnd = null;
                        item.FGiaQuyetDinhChiTietEur = null;
                        item.FGiaQuyetDinhChiTietNgoaiTeKhac = null;
                    }
                    _nhDaGoiThauService.UpdateRange(listGoiThau);
                    _nhDaGoiThauService.UpdateRange(listGoiThauNotCheck);
                    LoadDsGoiThau();

                }
                e.Result = Model;

            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    // Reload data
                    IsDieuChinh = false;
                    LoadData();

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    Window window = obj as Window;
                    SavedAction?.Invoke(Model);
                    window.Close();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void CaculatorGiaTriPheDuyet()
        {
            if (ItemsGoiThau.IsEmpty()) return;

            IEnumerable<NhDaGoiThauModel> listGoiThau = ItemsGoiThau.Where(s => s.IsChecked);
            // Map giá trị quyết định chi tiết = Tổng gói thầu IsChecked = true
            if (!listGoiThau.IsEmpty())
            {
                Model.FGiaTriUsd = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietUsd);
                Model.FGiaTriVnd = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietVnd);
                Model.FGiaTriEur = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietEur);
                Model.FGiaTriNgoaiTeKhac = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietNgoaiTeKhac);
                OnPropertyChanged(nameof(Model));
            }else
            {
                Model.FGiaTriUsd = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietUsd);
                Model.FGiaTriVnd = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietVnd);
                Model.FGiaTriEur = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietEur);
                Model.FGiaTriNgoaiTeKhac = listGoiThau.Sum(s => s.FGiaQuyetDinhChiTietNgoaiTeKhac);
                OnPropertyChanged(nameof(Model));

            }
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }

        public override void Dispose()
        {
            // Clear items
            if (!ItemsDecisionType.IsEmpty()) ItemsDecisionType.Clear();
            if (!ItemsKeHoachTongThe.IsEmpty()) ItemsKeHoachTongThe.Clear();
            if (!ItemsDonVi.IsEmpty()) ItemsDonVi.Clear();
            if (!ItemsChuongTrinh.IsEmpty()) ItemsChuongTrinh.Clear();
            if (!ItemsDuAn.IsEmpty()) ItemsDuAn.Clear();
            if (!ItemsPhuongAnNhapKhau.IsEmpty()) ItemsPhuongAnNhapKhau.Clear();
            if (!ItemsTiGiaChiTietDialog.IsEmpty()) ItemsTiGiaChiTietDialog.Clear();
            if (!ItemsTiGiaDialog.IsEmpty()) ItemsTiGiaDialog.Clear();
        }

        private void CalculateModel()
        {

            if (ItemsGoiThau != null && FTiGiaNhap != null)
            {
                foreach (var item in ItemsGoiThau)
                {
                    item.FGiaQuyetDinhChiTietNgoaiTeKhac = FTiGiaNhap == 0 ? 0 : item.FGiaQuyetDinhChiTietUsd / FTiGiaNhap;
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
