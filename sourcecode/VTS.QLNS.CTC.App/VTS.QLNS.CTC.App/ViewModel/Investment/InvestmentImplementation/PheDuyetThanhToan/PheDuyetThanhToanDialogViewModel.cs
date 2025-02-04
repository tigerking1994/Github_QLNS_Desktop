using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan
{
    public class PheDuyetThanhToanDialogViewModel : DialogAttachmentViewModelBase<VdtTtDeNghiThanhToanModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IVdtTtDeNghiThanhToanChiTietService _deNghiThanhToanChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly IVdtTtPheDuyetThanhToanService _pheDuyetChiTietService;
        private readonly IVdtDaTtHopDongService _vdtDaTtHopDongService;
        private readonly IVdtDmNhaThauService _vdtDmNhaThauService;
        private readonly IVdtKhvKeHoachVonUngService _vdtKhvKeHoachVonUngService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly IVdtTtDeNghiThanhToanKhvService _keHoachVonDeNghiThanhToanService;
        private readonly IVdtNhaThauService _nhaThauService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IMapper _mapper;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private Dictionary<Guid, ObservableCollection<VdtDmNhaThau>> _dicNhaThau;
        #endregion

        public override string Name => "Quản lý cấp phát cấp thanh toán trong chỉ tiêu";
        public bool IsInsert => Model.Id == Guid.Empty;
        public override string Description => string.Format("{0} thông tin cấp phát cấp thanh toán trong chỉ tiêu", IsInsert ? "Thêm mới" : "Cập nhật");
        public string sNguonVon { get; set; }
        public Visibility ShowThuHoiTamUng => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "0") ? Visibility.Collapsed : Visibility.Visible;
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_DENGHI_THANHTOAN;

        public bool ShowThuHoiTamUngUngTruoc => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "1") || (NguonVonSelected != null && NguonVonSelected.ValueItem == "2" && BHoanTraUngTruoc);
        public string SHoanTraUngTruoc => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "0") ? "Thu ứng khác chuyển sang TU theo chế độ" : "Thanh toán hoàn trả số đã ứng trước";
        public bool BIsChangePaymentType => CheckChangePaymentType();
        public override Type ContentType => typeof(PheDuyetThanhToanDialog);

        #region Componer
        private bool _bHoanTraUngTruoc;
        public bool BHoanTraUngTruoc
        {
            get => _bHoanTraUngTruoc;
            set
            {
                if (SetProperty(ref _bHoanTraUngTruoc, value))
                {
                    OnPropertyChanged(nameof(ShowThuHoiTamUngUngTruoc));
                }
            }
        }

        private bool _bIsVisibleHoanTraUngTruoc;
        public bool BIsVisibleHoanTraUngTruoc
        {
            get => _bIsVisibleHoanTraUngTruoc;
            set => SetProperty(ref _bIsVisibleHoanTraUngTruoc, value);
        }

        private DateTime? _dNgayDeNghi;
        public DateTime? DNgayDeNghi
        {
            get => _dNgayDeNghi;
            set
            {
                if (SetProperty(ref _dNgayDeNghi, value))
                {
                    LoadKeHoachVon();
                    LoadHopDong();
                    LoadGiaTriThanhToan();
                }
            }
        }

        private DateTime? _dNgayPheDuyet;
        public DateTime? DNgayPheDuyet
        {
            get => _dNgayPheDuyet;
            set => SetProperty(ref _dNgayPheDuyet, value);
        }

        private int? _iNamKeHoach;
        public int? INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                if (SetProperty(ref _iNamKeHoach, value))
                {
                    LoadKeHoachVon();
                    LoadGiaTriThanhToan();
                }
            }
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiDonViSelected, value))
                {
                    LoadChuDauTu();
                    LoadHopDong();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ComboboxItem _chuDauTuSelected;
        public ComboboxItem ChuDauTuSelected
        {
            get => _chuDauTuSelected;
            set
            {
                if (SetProperty(ref _chuDauTuSelected, value))
                {
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataChuDauTu;
        public ObservableCollection<ComboboxItem> DataChuDauTu
        {
            get => _dataChuDauTu;
            set => SetProperty(ref _dataChuDauTu, value);
        }

        private ComboboxItem _cbxLoaiThanhToanSelected;
        public ComboboxItem CbxLoaiThanhToanSelected
        {
            get => _cbxLoaiThanhToanSelected;
            set
            {
                if (SetProperty(ref _cbxLoaiThanhToanSelected, value))
                {
                    OnPropertyChanged(nameof(SHoanTraUngTruoc));
                    OnPropertyChanged(nameof(ShowThuHoiTamUngUngTruoc));
                    OnPropertyChanged(nameof(ShowThuHoiTamUng));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiThanhToan;
        public ObservableCollection<ComboboxItem> CbxLoaiThanhToan
        {
            get => _cbxLoaiThanhToan;
            set => SetProperty(ref _cbxLoaiThanhToan, value);
        }

        private ComboboxItem _nguonVonSelected;
        public ComboboxItem NguonVonSelected
        {
            get => _nguonVonSelected;
            set
            {
                if (SetProperty(ref _nguonVonSelected, value))
                {
                    if (_nguonVonSelected != null && NguonVonSelected.ValueItem == "2")
                        BIsVisibleHoanTraUngTruoc = true;
                    else
                        BIsVisibleHoanTraUngTruoc = false;
                    OnPropertyChanged(nameof(BIsVisibleHoanTraUngTruoc));
                    OnPropertyChanged(nameof(ShowThuHoiTamUngUngTruoc));
                    LoadKeHoachVon();
                    LoadGiaTriThanhToan();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataNguonVon;
        public ObservableCollection<ComboboxItem> DataNguonVon
        {
            get => _dataNguonVon;
            set => SetProperty(ref _dataNguonVon, value);
        }

        private ComboboxItem _duAnHangMucSelected;
        public ComboboxItem DuAnHangMucSelected
        {
            get => _duAnHangMucSelected;
            set => SetProperty(ref _duAnHangMucSelected, value);
        }

        private ObservableCollection<ComboboxItem> _dataDuAnHangMuc;
        public ObservableCollection<ComboboxItem> DataDuAnHangMuc
        {
            get => _dataDuAnHangMuc;
            set => SetProperty(ref _dataDuAnHangMuc, value);
        }

        private ComboboxItem _duAnSelected;
        public ComboboxItem DuAnSelected
        {
            get => _duAnSelected;
            set
            {
                if (SetProperty(ref _duAnSelected, value))
                {
                    if (_duAnSelected != null)
                    {
                        LoadHopDong();
                        LoadGiaTriThanhToan();
                        LoadKeHoachVon();
                        LoadDuAnHangMucByDuAn();
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ComboboxItem _hopDongSelected;
        public ComboboxItem HopDongSelected
        {
            get => _hopDongSelected;
            set
            {
                if (SetProperty(ref _hopDongSelected, value))
                {
                    if (_hopDongSelected != null)
                    {
                        LoadNhaThau(Guid.Parse(_hopDongSelected.ValueItem));
                        LoadGiaTriHopDong();
                        LoadGiaTriThanhToan();
                    }
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataHopDong;
        public ObservableCollection<ComboboxItem> DataHopDong
        {
            get => _dataHopDong;
            set => SetProperty(ref _dataHopDong, value);
        }

        private ObservableCollection<KeHoachVonModel> _listKeHoachVon;
        public ObservableCollection<KeHoachVonModel> ListKeHoachVon
        {
            get => _listKeHoachVon;
            set => SetProperty(ref _listKeHoachVon, value);
        }

        private ComboboxItem _coQuanThanhToanSelected;
        public ComboboxItem CoQuanThanhToanSelected
        {
            get => _coQuanThanhToanSelected;
            set
            {
                if (SetProperty(ref _coQuanThanhToanSelected, value))
                {
                    LoadKeHoachVon();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan
        {
            get => _dataCoQuanThanhToan;
            set => SetProperty(ref _dataCoQuanThanhToan, value);
        }

        private ObservableCollection<VdtDmNhaThau> _nhaThauItems;
        public ObservableCollection<VdtDmNhaThau> NhaThauItems
        {
            get => _nhaThauItems;
            set => SetProperty(ref _nhaThauItems, value);
        }

        private VdtDmNhaThau _nhaThauSelected;
        public VdtDmNhaThau NhaThauSelected
        {
            get => _nhaThauSelected;
            set
            {
                if (SetProperty(ref _nhaThauSelected, value))
                {
                    LoadNhaThauInfo();
                }
            }
        }

        private string _tenNhaThau;
        public string TenNhaThau
        {
            get => _tenNhaThau;
            set => SetProperty(ref _tenNhaThau, value);
        }

        private string _stkNhaThau;
        public string StkNhaThau
        {
            get => _stkNhaThau;
            set => SetProperty(ref _stkNhaThau, value);
        }

        private string _maNganHang;
        public string MaNganHang
        {
            get => _maNganHang;
            set => SetProperty(ref _maNganHang, value);
        }

        private double _giaTriHopDong;
        public double GiaTriHopDong
        {
            get => _giaTriHopDong;
            set => SetProperty(ref _giaTriHopDong, value);
        }

        private double _luyKeThanhToanKLHTVonTN;
        public double LuyKeThanhToanKLHTVonTN
        {
            get => _luyKeThanhToanKLHTVonTN;
            set => SetProperty(ref _luyKeThanhToanKLHTVonTN, value);
        }

        private double _luyKeThanhToanKLHTVonNN;
        public double LuyKeThanhToanKLHTVonNN
        {
            get => _luyKeThanhToanKLHTVonNN;
            set => SetProperty(ref _luyKeThanhToanKLHTVonNN, value);
        }

        private double _luyKeTamUngChuaThuHoiVonTN;
        public double LuyKeTamUngChuaThuHoiVonTN
        {
            get => _luyKeTamUngChuaThuHoiVonTN;
            set => SetProperty(ref _luyKeTamUngChuaThuHoiVonTN, value);
        }

        private double _luyKeTamUngChuaThuHoiVonNN;
        public double LuyKeTamUngChuaThuHoiVonNN
        {
            get => _luyKeTamUngChuaThuHoiVonNN;
            set => SetProperty(ref _luyKeTamUngChuaThuHoiVonNN, value);
        }

        private double _luyKeTamUngChuaThuHoiVonUngTruocTN;
        public double LuyKeTamUngChuaThuHoiVonUngTruocTN
        {
            get => _luyKeTamUngChuaThuHoiVonUngTruocTN;
            set => SetProperty(ref _luyKeTamUngChuaThuHoiVonUngTruocTN, value);
        }

        private double _luyKeTamUngChuaThuHoiVonUngTruocNN;
        public double LuyKeTamUngChuaThuHoiVonUngTruocNN
        {
            get => _luyKeTamUngChuaThuHoiVonUngTruocNN;
            set => SetProperty(ref _luyKeTamUngChuaThuHoiVonUngTruocNN, value);
        }

        private double _fGiaTriThanhToanPheDuyetTN;
        public double FGiaTriThanhToanPheDuyetTN
        {
            get => _fGiaTriThanhToanPheDuyetTN;
            set => SetProperty(ref _fGiaTriThanhToanPheDuyetTN, value);
        }

        private double _fGiaTriThanhToanPheDuyetNN;
        public double FGiaTriThanhToanPheDuyetNN
        {
            get => _fGiaTriThanhToanPheDuyetNN;
            set => SetProperty(ref _fGiaTriThanhToanPheDuyetNN, value);
        }

        private double _fGiaTriThuHoiPheDuyetTN;
        public double FGiaTriThuHoiPheDuyetTN
        {
            get => _fGiaTriThuHoiPheDuyetTN;
            set => SetProperty(ref _fGiaTriThuHoiPheDuyetTN, value);
        }

        private double _fGiaTriThuHoiPheDuyetNN;
        public double FGiaTriThuHoiPheDuyetNN
        {
            get => _fGiaTriThuHoiPheDuyetNN;
            set => SetProperty(ref _fGiaTriThuHoiPheDuyetNN, value);
        }

        private double _fGiaTriTuChoiTN;
        public double FGiaTriTuChoiTN
        {
            get => _fGiaTriTuChoiTN;
            set => SetProperty(ref _fGiaTriTuChoiTN, value);
        }

        private double _fGiaTriTuChoiNN;
        public double FGiaTriTuChoiNN
        {
            get => _fGiaTriTuChoiNN;
            set => SetProperty(ref _fGiaTriTuChoiNN, value);
        }
        #endregion

        public PheDuyetThanhToanDialogViewModel(
            INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IVdtTtDeNghiThanhToanChiTietService deNghiThanhToanChiTietService,
            IVdtDaTtHopDongService vdtDaTtHopDongService,
            IVdtDmNhaThauService vdtDmNhaThauService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            IVdtTtPheDuyetThanhToanService pheDuyetChiTietService,
            IDmChuDauTuService chuDauTuService,
            IVdtDaDuAnService duAnService,
            IVdtKhvKeHoachVonUngService vdtKhvKeHoachVonUngService,
            IVdtTtDeNghiThanhToanKhvService keHoachVonDeNghiThanhToanService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IVdtNhaThauService nhaThauService,
            IStorageServiceFactory storageServiceFactory,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            IAttachmentService attachService,
            IMapper mapper) : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _deNghiThanhToanChiTietService = deNghiThanhToanChiTietService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _vdtDaTtHopDongService = vdtDaTtHopDongService;
            _vdtDmNhaThauService = vdtDmNhaThauService;
            _chuDauTuService = chuDauTuService;
            _duAnService = duAnService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _nhaThauService = nhaThauService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _keHoachVonDeNghiThanhToanService = keHoachVonDeNghiThanhToanService;
            _vdtKhvKeHoachVonUngService = vdtKhvKeHoachVonUngService;
            _pheDuyetChiTietService = pheDuyetChiTietService;
            _mapper = mapper;
        }

        #region RelayCommand Event
        public override void Init()
        {
            ResetCondition();
            LoadChuDauTu();
            LoadNguonNganSach();
            LoadData();
            LoadAttach();
        }

        public void ResetCondition()
        {
            INamKeHoach = null;
            CbxLoaiDonVi = new ObservableCollection<ComboboxItem>();
            CbxLoaiDonViSelected = null;
            DataNguonVon = new ObservableCollection<ComboboxItem>();
            NguonVonSelected = null;
            ListKeHoachVon = new ObservableCollection<KeHoachVonModel>();
            CbxLoaiThanhToan = new ObservableCollection<ComboboxItem>();
            CbxLoaiThanhToanSelected = null;
            DataDuAn = new ObservableCollection<ComboboxItem>();
            DuAnSelected = null;
            DataHopDong = new ObservableCollection<ComboboxItem>();
            HopDongSelected = null;
            NhaThauItems = new ObservableCollection<VdtDmNhaThau>();
            NhaThauSelected = null;
            DNgayPheDuyet = null;
            DNgayDeNghi = null;
            GiaTriHopDong = 0;
            LuyKeThanhToanKLHTVonTN = 0;
            LuyKeThanhToanKLHTVonNN = 0;
            LuyKeTamUngChuaThuHoiVonTN = 0;
            LuyKeTamUngChuaThuHoiVonNN = 0;
            TenNhaThau = string.Empty;
            StkNhaThau = string.Empty;
            MaNganHang = string.Empty;
        }

        public override void LoadData(params object[] args)
        {
            if (Model != null && Model.Id != Guid.Empty)
            {
                //update
                DNgayDeNghi = Model.dNgayDeNghi;
                DNgayPheDuyet = Model.dNgayPheDuyet;
                BHoanTraUngTruoc = Model.BHoanTraUngTruoc;
                _iNamKeHoach = Model.iNamKeHoach;
                LoadCoQuanThanhToan();
                LoadLoaiThanhToan();
                LoadComboBoxLoaiDonVi(Model.iID_MaDonViQuanLy);
                if (Model.iID_DuAnId.HasValue)
                {
                    VdtDaDuAn duan = _duAnService.FindById(Model.iID_DuAnId.Value);
                    if (DataChuDauTu != null && DataChuDauTu.Count > 0 && duan.IIdChuDauTuId.HasValue)
                    {
                        ChuDauTuSelected = DataChuDauTu.Where(n => n.ValueItem == duan.IIdChuDauTuId.Value.ToString()).FirstOrDefault();
                        if (ChuDauTuSelected == null)
                        {
                            ChuDauTuSelected = DataChuDauTu.FirstOrDefault();
                        }
                    }
                }
                if (QlnsData.nguonNganSach.ContainsKey(Model.iID_NguonVonID.ToString()))
                {
                    sNguonVon = QlnsData.nguonNganSach[Model.iID_NguonVonID.ToString()];
                }
                LoadGiaTriPheDuyetThanhToan();
                OnPropertyChanged(nameof(Model.fGiaTriThanhToanTN));
            }
            else
            {
                //add
                INamKeHoach = DateTime.Now.Year;
                DNgayDeNghi = DateTime.Now;
                DNgayPheDuyet = DateTime.Now;
                sNguonVon = _sessionService.Current.BudgetStr;
                Model.iID_NguonVonID = _sessionService.Current.Budget;
                LoadCoQuanThanhToan();
                if (Model.iID_DonViQuanLyID != Guid.Empty)
                {
                    LoadComboBoxLoaiDonVi(Model.iID_MaDonViQuanLy);
                }
                else
                {
                    LoadComboBoxLoaiDonVi();
                }
                LoadLoaiThanhToan();
            }
        }

        private void LoadDuAnHangMucByDuAn()
        {
            _dataDuAnHangMuc = new ObservableCollection<ComboboxItem>();
            List<VdtDaDuAnHangMuc> listHangMuc = _vdtDuAnHangMucService.FindByIdDuAn(Guid.Parse(DuAnSelected.HiddenValue)).ToList();
            listHangMuc.ForEach(hm =>
            {
                _dataDuAnHangMuc.Add(new ComboboxItem() { DisplayItem = hm.STenHangMuc, ValueItem = hm.Id.ToString() });
            });
            if (Model.ID_DuAn_HangMuc != null)
            {
                DuAnHangMucSelected = _dataDuAnHangMuc.Where(h => h.ValueItem == Model.ID_DuAn_HangMuc.ToString()).FirstOrDefault();
            }
            else
            {
                DuAnHangMucSelected = _dataDuAnHangMuc.FirstOrDefault();
            }
            OnPropertyChanged(nameof(DataDuAnHangMuc));
            OnPropertyChanged(nameof(DuAnHangMucSelected));
        }

        public override void OnSave(object obj)
        {
            if (!Validate()) return;
            Guid deNghiThanhToanId = Guid.Empty;
            var dataInsert = _mapper.Map<VdtTtDeNghiThanhToan>(Model);
            if (dataInsert.Id == Guid.Empty)
            {
                dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                dataInsert.DNgayDeNghi = DNgayDeNghi;
                dataInsert.INamKeHoach = INamKeHoach;
                dataInsert.IIdNguonVonId = int.Parse(NguonVonSelected.ValueItem);
                dataInsert.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                dataInsert.IIdDuAnId = Guid.Parse(DuAnSelected.HiddenValue);
                dataInsert.IIdHopDongId = Guid.Parse(HopDongSelected.ValueItem);
                dataInsert.IIdNhaThauId = NhaThauSelected.Id;
                dataInsert.iCoQuanThanhToan = int.Parse(CoQuanThanhToanSelected.ValueItem);
                dataInsert.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                dataInsert.BKhoa = false;
                dataInsert.BHoanTraUngTruoc = BHoanTraUngTruoc;
                dataInsert.SGhiChu = Model.sGhiChu;
                dataInsert.FGiaTriThanhToanTN = Model.fGiaTriThanhToanTN;
                dataInsert.FGiaTriThanhToanNN = Model.fGiaTriThanhToanNN;
                dataInsert.FGiaTriThuHoiTN = Model.fGiaTriThuHoiTN;
                dataInsert.FGiaTriThuHoiNN = Model.fGiaTriThuHoiNN;
                dataInsert.FThueGiaTriGiaTang = Model.FThueGiaTriGiaTang;
                dataInsert.FChuyenTienBaoHanh = Model.FChuyenTienBaoHanh;
                if (ListKeHoachVon != null && ListKeHoachVon.Any(n => n.IsChecked))
                    dataInsert.IIdPhanBoVonID = ListKeHoachVon.FirstOrDefault(n => n.IsChecked).Id;
                _deNghiThanhToanService.Insert(dataInsert, _sessionService.Current.Principal);
                deNghiThanhToanId = dataInsert.Id;
                OnPropertyChanged(nameof(IsInsert));
            }
            else
            {
                VdtTtDeNghiThanhToan itemUpdate = _deNghiThanhToanService.Find(dataInsert.Id);
                itemUpdate.SSoDeNghi = Model.sSoDeNghi;
                itemUpdate.SNguoiLap = Model.sNguoiLap;
                itemUpdate.FGiaTriThanhToanNN = Model.fGiaTriThanhToanNN;
                itemUpdate.FGiaTriThanhToanTN = Model.fGiaTriThanhToanTN;
                itemUpdate.FGiaTriThuHoiNN = Model.fGiaTriThuHoiNN;
                itemUpdate.FGiaTriThuHoiTN = Model.fGiaTriThuHoiTN;
                itemUpdate.FThueGiaTriGiaTang = Model.FThueGiaTriGiaTang;
                itemUpdate.FChuyenTienBaoHanh = Model.FChuyenTienBaoHanh;
                itemUpdate.SGhiChu = Model.sGhiChu;
                itemUpdate.DDateUpdate = DateTime.Now;
                itemUpdate.SUserUpdate = _sessionService.Current.Principal;
                itemUpdate.iCoQuanThanhToan = int.Parse(CoQuanThanhToanSelected.ValueItem);
                itemUpdate.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                itemUpdate.BHoanTraUngTruoc = BHoanTraUngTruoc;
                itemUpdate.SLyDoTuChoi = Model.SLyDoTuChoi;
                itemUpdate.FGiaTriThanhToanTN = Model.fGiaTriThanhToanTN;
                itemUpdate.FGiaTriThanhToanNN = Model.fGiaTriThanhToanNN;
                itemUpdate.FGiaTriThuHoiTN = Model.fGiaTriThuHoiTN;
                itemUpdate.FGiaTriThuHoiNN = Model.fGiaTriThuHoiNN;
                itemUpdate.FThueGiaTriGiaTang = Model.FThueGiaTriGiaTang;
                itemUpdate.FChuyenTienBaoHanh = Model.FChuyenTienBaoHanh;
                if (ListKeHoachVon != null && ListKeHoachVon.Any(n => n.IsChecked))
                    itemUpdate.IIdPhanBoVonID = ListKeHoachVon.FirstOrDefault(n => n.IsChecked).Id;
                _deNghiThanhToanService.Update(itemUpdate);
                deNghiThanhToanId = itemUpdate.Id;
            }
            SaveAttachment(dataInsert.Id);
            _keHoachVonDeNghiThanhToanService.DeleteByDeNghiThanhToanId(deNghiThanhToanId);
            foreach (var item in ListKeHoachVon.Where(n => n.IsChecked))
            {
                VdtTtDeNghiThanhToanKhv entityKhV = new VdtTtDeNghiThanhToanKhv();
                entityKhV.IIdDeNghiThanhToanId = deNghiThanhToanId;
                entityKhV.IIdKeHoachVonId = item.Id;
                entityKhV.ILoai = item.PhanLoai;
                //entityKhV.Id = item.Id;
                _keHoachVonDeNghiThanhToanService.Add(entityKhV);
            }

            Model = _mapper.Map<VdtTtDeNghiThanhToanModel>(dataInsert);
            if (ListKeHoachVon != null && ListKeHoachVon.Any(n => n.IsChecked))
                Model.iID_PhanBoVonID = ListKeHoachVon.FirstOrDefault(n => n.IsChecked).Id;
            Model.sTenDonVi = CbxLoaiDonViSelected.DisplayItem;
            Model.sTenDuAn = DuAnSelected.DisplayItem;
            Model.dNgayPheDuyet = DNgayPheDuyet;
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(Model);
            ((Window)obj).Close();
        }
        #endregion

        #region Helper
        private void KeHoachVonDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            KeHoachVonModel item = (KeHoachVonModel)sender;
            if (!item.IsChecked) return;
            foreach (var child in ListKeHoachVon)
            {
                if (child.Id != item.Id || child.PhanLoai != item.PhanLoai)
                    child.IsChecked = false;
            }
            OnPropertyChanged(nameof(ListKeHoachVon));
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();
            if (Model == null)
                Model = new VdtTtDeNghiThanhToanModel();
            if (CbxLoaiDonViSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
            }
            else
            {
                Model.iID_MaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                Model.iID_DonViQuanLyID = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
            }
            if (ChuDauTuSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Chủ đầu tư");
            }
            if (string.IsNullOrEmpty(Model.sSoDeNghi))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số phê duyệt");
            }
            //if (!DNgayPheDuyet.HasValue)
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày phê duyệt");
            //}
            if (CbxLoaiThanhToanSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại thanh toán");
            }
            if (DuAnSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Dự án");
            }
            //if (HopDongSelected == null)
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Hợp đồng");
            //}
            //if (NhaThauSelected == null)
            //{
            //    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nhà thầu");
            //}
            if (INamKeHoach == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
            }
            if (NguonVonSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
            }
            if (ListKeHoachVon == null || ListKeHoachVon.Where(n => n.IsChecked).Count() == 0)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Kế hoạch vốn");
            }
            if (CoQuanThanhToanSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Cơ quan thanh toán");
            }
            if (_deNghiThanhToanService.CheckExistSoQuyetDinh(Model.Id, Model.sSoDeNghi))
            {
                messageBuilder.Append(Resources.MsgTrungSoQuyetDinhs);
            }

            if (messageBuilder.Length != 0)
            {
                System.Windows.MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return false;
            }
            return true;
        }

        private void LoadCoQuanThanhToan()
        {
            DataCoQuanThanhToan = new ObservableCollection<ComboboxItem>();
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.KHO_BAC, ValueItem = ((int)CoQuanThanhToanEnum.Type.KHO_BAC).ToString() });
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.TONKHOAN_DONVI, ValueItem = ((int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI).ToString() });
            if (Model != null && Model.Id != Guid.Empty && Model.iCoQuanThanhToan.HasValue)
                CoQuanThanhToanSelected = DataCoQuanThanhToan.FirstOrDefault(n => n.ValueItem == Model.iCoQuanThanhToan.Value.ToString());
        }

        private void LoadDuAn()
        {
            if (ChuDauTuSelected == null)
            {
                DataDuAn = new ObservableCollection<ComboboxItem>();
                return;
            }
            DataDuAn = new ObservableCollection<ComboboxItem>();
            List<VdtDaDuAn> list = _duAnService.FindByChuDauTuByMaChuDauTu(ChuDauTuSelected.HiddenValue);
            foreach (VdtDaDuAn item in list)
            {
                _dataDuAn.Add(new ComboboxItem { DisplayItem = item.SMaDuAn + "-" + item.STenDuAn, ValueItem = item.SMaDuAn, HiddenValue = item.Id.ToString() });
            }
            if (_dataDuAn != null & _dataDuAn.Count > 0)
            {
                if (Model != null && Model.iID_DuAnId.HasValue)
                    DuAnSelected = _dataDuAn.Where(n => n.HiddenValue == Model.iID_DuAnId.Value.ToString()).FirstOrDefault();
            }
            OnPropertyChanged(nameof(DuAnSelected));
            OnPropertyChanged(nameof(DataDuAn));
        }

        private void LoadChuDauTu()
        {
            if (_cbxLoaiDonViSelected == null)
            {
                DataChuDauTu = new ObservableCollection<ComboboxItem>();
                return;
            }
            GetDataChuDauTu();
        }

        private void GetDataChuDauTu()
        {
            DataChuDauTu = new ObservableCollection<ComboboxItem>();
            DmChuDauTu parent = _chuDauTuService.FindAllByMaDonVi(CbxLoaiDonViSelected.ValueItem);
            List<DmChuDauTu> result = new List<DmChuDauTu>();
            if (parent != null)
            {
                result.Add(parent);
                GetChildChuDauTu(parent.Id, ref result);
            }
            foreach (DmChuDauTu item in result)
            {
                _dataChuDauTu.Add(new ComboboxItem { ValueItem = item.Id.ToString(), DisplayItem = string.Format("{0}-{1}", item.IIDMaDonVi, item.STenDonVi), HiddenValue = item.IIDMaDonVi });
            }
            if (DataChuDauTu != null && DataChuDauTu.Count > 0)
            {
                ChuDauTuSelected = DataChuDauTu.FirstOrDefault();
            }

            OnPropertyChanged(nameof(DataChuDauTu));
            OnPropertyChanged(nameof(ChuDauTuSelected));
        }

        private void GetChildChuDauTu(Guid id, ref List<DmChuDauTu> result)
        {
            DmChuDauTu parent = _chuDauTuService.FindByParentId(id, _sessionService.Current.YearOfWork);
            if (parent == null)
            {
                return;
            }
            else
            {
                if (result.Count == 0)
                {
                    GetChildChuDauTuByDonVi(parent.Id, ref result);
                }
                else
                {
                    GetChildChuDauTuByDonVi(parent.IIDDonViCha.GetValueOrDefault(), ref result);
                }
            }
        }

        private void GetChildChuDauTuByDonVi(Guid id, ref List<DmChuDauTu> result)
        {
            DmChuDauTu parent = _chuDauTuService.FindByParentId(id, _sessionService.Current.YearOfWork);
            if (result == null)
            {
                result = _chuDauTuService.FindByIdDonViCha(id, _sessionService.Current.YearOfWork);
            }
            else
            {
                var dataChild = _chuDauTuService.FindByIdDonViCha(id, _sessionService.Current.YearOfWork);
                if (dataChild != null)
                {
                    result.AddRange(dataChild);
                }
            }
        }

        private void LoadHopDong()
        {
            DataHopDong = new ObservableCollection<ComboboxItem>();
            if (_cbxLoaiDonViSelected == null || _duAnSelected == null || DNgayDeNghi == null)
            {
                HopDongSelected = null;
                return;
            }
            var predicate = PredicateBuilder.True<VdtDaTtHopDong>();
            predicate = predicate.And(x => x.IIdDuAnId == Guid.Parse(_duAnSelected.HiddenValue));
            predicate = predicate.And(x => x.DNgayHopDong.Date <= DNgayDeNghi.Value.Date);
            List<VdtDaTtHopDong> list = _vdtDaTtHopDongService.FindAll(predicate).ToList();

            foreach (VdtDaTtHopDong item in list)
            {
                _dataHopDong.Add(new ComboboxItem
                {
                    DisplayItem = string.Format("{0};{1}", item.SSoHopDong, item.DNgayHopDong.ToString("dd/MM/yyyy")),
                    ValueItem = item.Id.ToString(),
                    HiddenValue = item.IIdNhaThauThucHienId.ToString(),
                    Type = item.FTienHopDong.ToString()
                });
            }
            if (_dataHopDong != null && _dataHopDong.Count > 0)
            {
                if (Model != null && Model.iID_HopDongId.HasValue)
                {
                    HopDongSelected = _dataHopDong.Where(n => n.ValueItem == Model.iID_HopDongId.Value.ToString()).FirstOrDefault();
                    GiaTriHopDong = _hopDongSelected != null && !string.IsNullOrEmpty(_hopDongSelected.Type) ? double.Parse(_hopDongSelected.Type) : 0;
                }
                else
                {
                    HopDongSelected = _dataHopDong.FirstOrDefault();
                    GiaTriHopDong = _hopDongSelected != null && !string.IsNullOrEmpty(_hopDongSelected.Type) ? double.Parse(_hopDongSelected.Type) : 0;
                }
            }
            else
            {
                GiaTriHopDong = 0;
            }
            OnPropertyChanged(nameof(HopDongSelected));
            OnPropertyChanged(nameof(DataHopDong));
            OnPropertyChanged(nameof(GiaTriHopDong));
            if (_hopDongSelected != null && !string.IsNullOrEmpty(_hopDongSelected.HiddenValue))
            {
                LoadNhaThau(Guid.Parse(_hopDongSelected.ValueItem));
            }
            else
            {
                LoadNhaThau(Guid.Empty);
            }
        }

        private void LoadGiaTriHopDong()
        {
            if (_hopDongSelected != null)
            {
                GiaTriHopDong = _hopDongSelected != null && !string.IsNullOrEmpty(_hopDongSelected.Type) ? double.Parse(_hopDongSelected.Type) : 0;
            }
            else
            {
                GiaTriHopDong = 0;
            }
            OnPropertyChanged(nameof(GiaTriHopDong));
        }

        private void LoadNhaThau(Guid iIdHopDong)
        {
            if (_dicNhaThau == null)
                _dicNhaThau = new Dictionary<Guid, ObservableCollection<VdtDmNhaThau>>();
            if (!_dicNhaThau.ContainsKey(iIdHopDong))
            {
                var lstNhaThau = _nhaThauService.GetNhaThauByHopDong(iIdHopDong);
                _dicNhaThau.Add(iIdHopDong, new ObservableCollection<VdtDmNhaThau>(lstNhaThau));
            }
            NhaThauItems = _dicNhaThau[iIdHopDong];
            if (Model.iID_NhaThauId.HasValue)
            {
                NhaThauSelected = NhaThauItems.FirstOrDefault(n => n.Id == Model.iID_NhaThauId.Value);
            }
            OnPropertyChanged(nameof(NhaThauItems));
        }

        private void LoadNhaThauInfo()
        {
            if (NhaThauSelected != null)
            {
                TenNhaThau = NhaThauSelected.STenNhaThau;
                StkNhaThau = NhaThauSelected.SSoTaiKhoan;
                MaNganHang = Model.SMaNganHang;
            }
            else
            {
                TenNhaThau = string.Empty;
                StkNhaThau = string.Empty;
                MaNganHang = string.Empty;
            }
            OnPropertyChanged(nameof(TenNhaThau));
            OnPropertyChanged(nameof(StkNhaThau));
            OnPropertyChanged(nameof(MaNganHang));
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            List<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai) && (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi)).ToList();
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                listDonVi = new List<DonVi>();
            }
            var cbxLoaiDonViData = listDonVi.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIDMaDonVi,
                DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi),
                HiddenValue = n.Id.ToString()
            });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);

            if (!string.IsNullOrEmpty(iIdDonVi))
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());

            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        public void LoadLoaiThanhToan()
        {
            _cbxLoaiThanhToan = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            _cbxLoaiThanhToan.Add(new ComboboxItem { DisplayItem = "Thanh toán", ValueItem = "1" });
            _cbxLoaiThanhToan.Add(new ComboboxItem { DisplayItem = "Tạm ứng", ValueItem = "2" });

            if (Model != null)
                _cbxLoaiThanhToanSelected = _cbxLoaiThanhToan.Where(n => n.ValueItem == Model.iLoaiThanhToan.ToString()).FirstOrDefault();
            OnPropertyChanged(nameof(CbxLoaiThanhToan));
            OnPropertyChanged(nameof(CbxLoaiThanhToanSelected));
        }

        public void LoadNguonNganSach()
        {
            List<NsNguonNganSach> lstNganSach = _nsNguonNganSachService.FindAll().OrderBy(n => n.IStt).ToList();
            _dataNguonVon = new ObservableCollection<ComboboxItem>();
            foreach (NsNguonNganSach item in lstNganSach)
            {
                _dataNguonVon.Add(new ComboboxItem { DisplayItem = item.STen, ValueItem = item.IIdMaNguonNganSach.HasValue ? item.IIdMaNguonNganSach.Value.ToString() : string.Empty });
            }
            if (Model != null && Model.Id != Guid.Empty)
                NguonVonSelected = _dataNguonVon.FirstOrDefault(n => n.ValueItem == Model.iID_NguonVonID.ToString());
            else
                NguonVonSelected = _dataNguonVon.FirstOrDefault();
            OnPropertyChanged(nameof(DataNguonVon));
            OnPropertyChanged(nameof(NguonVonSelected));
        }

        public void LoadKeHoachVon()
        {
            if (DuAnSelected == null || NguonVonSelected == null || INamKeHoach == null || CoQuanThanhToanSelected == null || !DNgayDeNghi.HasValue)
            {
                ListKeHoachVon = new ObservableCollection<KeHoachVonModel>();
                return;
            }
            List<KeHoachVonQuery> list = _vdtKhvPhanBoVonService.GetKeHoachVonCapPhatThanhToan(
                DuAnSelected.HiddenValue,
                int.Parse(NguonVonSelected.ValueItem),
                DNgayDeNghi.Value,
                INamKeHoach.Value,
                int.Parse(CoQuanThanhToanSelected.ValueItem),
                Model.Id);

            ListKeHoachVon = _mapper.Map<ObservableCollection<KeHoachVonModel>>(list);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<VdtTtDeNghiThanhToanKhv> listKeHoachVon = _keHoachVonDeNghiThanhToanService.FindByDeNghiThanhToanId(Model.Id);
                foreach (var item in ListKeHoachVon)
                {
                    if (listKeHoachVon.Where(n => n.IIdKeHoachVonId == item.Id && n.ILoai == item.PhanLoai).Count() > 0)
                    {
                        item.IsChecked = true;
                    }
                    item.PropertyChanged += KeHoachVonDetailModel_PropertyChanged;
                }
            }
            else
            {
                foreach (var item in ListKeHoachVon)
                    item.PropertyChanged += KeHoachVonDetailModel_PropertyChanged;
            }
            OnPropertyChanged(nameof(ListKeHoachVon));
        }

        public void LoadGiaTriThanhToan()
        {
            if (DNgayDeNghi == null || HopDongSelected == null || string.IsNullOrEmpty(HopDongSelected.ValueItem)
                || NguonVonSelected == null || string.IsNullOrEmpty(NguonVonSelected.ValueItem) || INamKeHoach == null || CoQuanThanhToanSelected == null)
            {
                return;
            }
            double luyKeTTTN = 0;
            double luyKeTTNN = 0;
            double luyKeTUTN = 0;
            double luyKeTUNN = 0;
            double luyKeTUUngTruocTN = 0;
            double luyKeTUUngTruocNN = 0;

            double sumTN = 0;
            double sumNN = 0;

            Guid iIdChungTu = new Guid();
            if (Model.BThanhToanTheoHopDong)
                iIdChungTu = (Model.iID_HopDongId ?? Guid.Empty);
            else
                iIdChungTu = (Model.IIdChiPhiId ?? Guid.Empty);

            _deNghiThanhToanService.LoadGiaTriThanhToan(int.Parse(CoQuanThanhToanSelected.ValueItem), DNgayDeNghi.Value, Model.BThanhToanTheoHopDong, iIdChungTu.ToString(), int.Parse(NguonVonSelected.ValueItem), INamKeHoach.Value, Model.Id, Model.loaiCoQuanTaiChinh,
                ref luyKeTTTN, ref luyKeTTNN, ref luyKeTUTN, ref luyKeTUNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, null);
            if(Model.Id != Guid.Empty || Model.Id != null)
            {
                LuyKeThanhToanKLHTVonTN = Model.fLuyKeThanhToanTN;
                LuyKeThanhToanKLHTVonNN = Model.fLuyKeThanhToanNN;
                LuyKeTamUngChuaThuHoiVonTN = Model.fLuyKeTUChuaThuHoiTN;
                LuyKeTamUngChuaThuHoiVonNN = Model.fLuyKeTUChuaThuHoiNN;
                LuyKeTamUngChuaThuHoiVonUngTruocTN = Model.fLuyKeTUChuaThuHoiKhacTN;
                LuyKeTamUngChuaThuHoiVonUngTruocNN = Model.fLuyKeTUChuaThuHoiKhacNN;
            }
            else
            {
                LuyKeThanhToanKLHTVonTN = luyKeTTTN;
                LuyKeThanhToanKLHTVonNN = luyKeTTNN;
                LuyKeTamUngChuaThuHoiVonTN = luyKeTUTN;
                LuyKeTamUngChuaThuHoiVonNN = luyKeTUNN;
                LuyKeTamUngChuaThuHoiVonUngTruocTN = luyKeTUUngTruocTN;
                LuyKeTamUngChuaThuHoiVonUngTruocNN = luyKeTUUngTruocNN;
            }
            
            OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonTN));
            OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonNN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonTN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonNN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocTN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocNN));
        }

        private void LoadGiaTriPheDuyetThanhToan()
        {
            var data = _deNghiThanhToanService.LoadGiaTriPheDuyetThanhToanByParentId(Model.Id);
            FGiaTriThanhToanPheDuyetTN = (Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? data.ThanhToanTN : data.TamUngTN;
            FGiaTriThanhToanPheDuyetNN = (Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? data.ThanhToanNN : data.TamUngNN;
            FGiaTriThuHoiPheDuyetTN = data.ThuHoiUngTN;
            FGiaTriThuHoiPheDuyetNN = data.ThuHoiUngNN;
            FGiaTriTuChoiTN = Model.fGiaTriThanhToanTN - FGiaTriThanhToanPheDuyetTN;
            FGiaTriTuChoiNN = Model.fGiaTriThanhToanNN - FGiaTriThanhToanPheDuyetNN;
            OnPropertyChanged(nameof(FGiaTriThanhToanPheDuyetTN));
            OnPropertyChanged(nameof(FGiaTriThanhToanPheDuyetNN));
            OnPropertyChanged(nameof(FGiaTriThuHoiPheDuyetTN));
            OnPropertyChanged(nameof(FGiaTriThuHoiPheDuyetNN));
            OnPropertyChanged(nameof(FGiaTriTuChoiTN));
            OnPropertyChanged(nameof(FGiaTriTuChoiNN));
        }

        private bool CheckChangePaymentType()
        {
            var lstDetail = _pheDuyetChiTietService.GetAllVdtTTPheDuyetThanhToanChiTiet(Model.Id);
            if (lstDetail == null || lstDetail.Count == 0) return true;
            return false;
        }
        #endregion
    }
}
