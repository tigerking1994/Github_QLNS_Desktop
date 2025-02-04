using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.CapPhatThanhToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan
{
    public class CapPhatThanhToanDialogViewModel : DialogAttachmentViewModelBase<VdtTtDeNghiThanhToanModel>
    {
        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IVdtTtPheDuyetThanhToanService _pheDuyetChiTietService;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtKhvPhanBoVonService _vdtKhvPhanBoVonService;
        private readonly IVdtDaTtHopDongService _vdtDaTtHopDongService;
        private readonly IDmChuDauTuService _chuDauTuService;
        private readonly IMapper _mapper;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtTtDeNghiThanhToanKhvService _keHoachVonDeNghiThanhToanService;
        private readonly IVdtNhaThauService _nhaThauService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private Dictionary<Guid, ObservableCollection<VdtDmNhaThau>> _dicNhaThau;
        #endregion

        public string SHoanTraUngTruoc => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "0") ? "Thu ứng khác chuyển sang TU theo chế độ" : "Thanh toán hoàn trả số đã ứng trước";
        public override string Name => "Đề nghị cấp phát thanh toán";
        public override string Title => "Đề nghị cấp phát thanh toán";
        public override Type ContentType => typeof(CapPhatThanhToanDialog);
        public bool IsInsert => Model.Id == Guid.Empty;
        public bool BIsNotApprove => Model.Id == Guid.Empty || !Model.dNgayPheDuyet.HasValue;
        public override string Description => string.Format("{0} thông tin đề nghị cấp phát cấp thanh toán", IsInsert ? "Thêm mới" : "Cập nhật");
        public string sNguonVon { get; set; }
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_DENGHI_THANHTOAN;

        public Visibility ShowThuHoiTamUng => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "0") ? Visibility.Collapsed : Visibility.Visible;
        public bool ShowThuHoiTamUngUngTruoc => (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "1") || (NguonVonSelected != null && NguonVonSelected.ValueItem == "2" && BHoanTraUngTruoc);

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

        public bool IsShowDoubleClick { get; set; }

        private bool _bIsVisibleHoanTraUngTruoc;
        public bool BIsVisibleHoanTraUngTruoc
        {
            get => _bIsVisibleHoanTraUngTruoc;
            set => SetProperty(ref _bIsVisibleHoanTraUngTruoc, value);
        }

        public bool BIsChangePaymentType => CheckChangePaymentType();

        private bool _isLuuChuyen;
        public bool IsLuuChuyen
        {
            get => _isLuuChuyen;
            set => SetProperty(ref _isLuuChuyen, value);
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
                    LoadChiPhi();
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
                    onChangeNamKeHoach();
                }
            }
        }

        private ObservableCollection<VdtTtDeNghiThanhToanChiPhiModel> _itemsChiPhi;
        public ObservableCollection<VdtTtDeNghiThanhToanChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private VdtTtDeNghiThanhToanChiPhiModel _selectedChiPhi;
        public VdtTtDeNghiThanhToanChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
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

        private string _sSoDeNghi;
        public string sSoDeNghi
        {
            get => _sSoDeNghi;
            set => SetProperty(ref _sSoDeNghi, value);
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
                    BIsVisibleHoanTraUngTruoc = false;
                    if (NguonVonSelected != null)
                    {
                        switch (NguonVonSelected.ValueItem)
                        {
                            case "2":
                                BIsVisibleHoanTraUngTruoc = true;
                                break;
                            case "1":
                                if (CbxLoaiThanhToanSelected != null && CbxLoaiThanhToanSelected.ValueItem == "0")
                                {
                                    BIsVisibleHoanTraUngTruoc = true;
                                }
                                break;
                        }
                    }
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
                        LoadNguonNganSach();
                        LoadKeHoachVon();
                        LoadDuAnHangMucByDuAn();
                        LoadChiPhi();
                        onChangeTyLeThanhToan();
                        OnPropertyChanged(nameof(FTyLeThanhToan));
                    }
                }
            }
        }

        private ComboboxItem _duAnHangMucSelected;
        public ComboboxItem DuAnHangMucSelected
        {
            get => _duAnHangMucSelected;
            set => SetProperty(ref _duAnHangMucSelected, value);
        }

        private ObservableCollection<ComboboxItem> _dataDuAn;
        public ObservableCollection<ComboboxItem> DataDuAn
        {
            get => _dataDuAn;
            set => SetProperty(ref _dataDuAn, value);
        }

        private ObservableCollection<ComboboxItem> _dataDuAnHangMuc;
        public ObservableCollection<ComboboxItem> DataDuAnHangMuc
        {
            get => _dataDuAnHangMuc;
            set => SetProperty(ref _dataDuAnHangMuc, value);
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

        private bool _bThanhToanTheoHopDong;
        public bool BThanhToanTheoHopDong
        {
            get => _bThanhToanTheoHopDong;
            set
            {
                SetProperty(ref _bThanhToanTheoHopDong, value);
                if (_bThanhToanTheoHopDong)
                {
                    IndexForLuyKeThanhToanKLHTTrongNuoc = 1;
                    IndexForLuyKeThanhToanKLHTNgoaiNuoc = 2;
                }
                else
                {
                    IndexForLuyKeThanhToanKLHTTrongNuoc = 0;
                    IndexForLuyKeThanhToanKLHTNgoaiNuoc = 1;
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
                if (_coQuanThanhToanSelected != null)
                {
                    if (_coQuanThanhToanSelected.DisplayItem == CoQuanThanhToanEnum.TypeName.CQTC)
                    {
                        loaiCoQuanTaiChinh = 0;
                    }
                    if (_coQuanThanhToanSelected.DisplayItem == CoQuanThanhToanEnum.TypeName.CTC)
                    {
                        loaiCoQuanTaiChinh = 1;
                    }
                    LoadGiaTriThanhToan();
                    onChangeTyLeThanhToan();
                    OnPropertyChanged(nameof(FTyLeThanhToan));
                }
            }
        }

        private ObservableCollection<ComboboxItem> _dataCoQuanThanhToan;
        public ObservableCollection<ComboboxItem> DataCoQuanThanhToan
        {
            get => _dataCoQuanThanhToan;
            set => SetProperty(ref _dataCoQuanThanhToan, value);
        }

        private ObservableCollection<VdtTtThongTinCanCuModel> _thongTinCanCuItems;
        public ObservableCollection<VdtTtThongTinCanCuModel> ThongTinCanCuItems
        {
            get => _thongTinCanCuItems;
            set => SetProperty(ref _thongTinCanCuItems, value);
        }

        private VdtTtThongTinCanCuModel _thongTinCanCuSelected;
        public VdtTtThongTinCanCuModel ThongTinCanCuSelected
        {
            get => _thongTinCanCuSelected;
            set => SetProperty(ref _thongTinCanCuSelected, value);
        }
        #endregion

        public RelayCommand SaveAndTranferCommand { get; }
        public RelayCommand AddThongTinCanCuCommand { get; }
        public RelayCommand DeleteThongTinCanCuCommand { get; }
        public string tyLeThanhToan = "0";

        public string FTyLeThanhToan
        {
            get => tyLeThanhToan;
            set
            {
                SetProperty(ref tyLeThanhToan, value);
                OnPropertyChanged(nameof(FTyLeThanhToan));
            }
        }

        private int? loaiCoQuanTaiChinh;
        public int? LoaiCoQuanTaiChinh
        {
            get => loaiCoQuanTaiChinh;
            set
            {
                SetProperty(ref loaiCoQuanTaiChinh, value);
            }
        }

        private int? indexForLuyKeThanhToanKLHTTrongNuoc;
        public int? IndexForLuyKeThanhToanKLHTTrongNuoc
        {
            get => indexForLuyKeThanhToanKLHTTrongNuoc;
            set => SetProperty(ref indexForLuyKeThanhToanKLHTTrongNuoc, value);
        }

        private int? indexForLuyKeThanhToanKLHTNgoaiNuoc;
        public int? IndexForLuyKeThanhToanKLHTNgoaiNuoc
        {
            get => indexForLuyKeThanhToanKLHTNgoaiNuoc;
            set => SetProperty(ref indexForLuyKeThanhToanKLHTNgoaiNuoc, value);
        }

        private int selectedKeHoachVon = 0;

        public CapPhatThanhToanDialogViewModel(
            INsDonViService nsDonViService,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IVdtTtPheDuyetThanhToanService pheDuyetChiTietService,
            IVdtDaTtHopDongService vdtDaTtHopDongService,
            IVdtKhvPhanBoVonService vdtKhvPhanBoVonService,
            IDmChuDauTuService chuDauTuService,
            IVdtDaDuAnService duAnService,
            IVdtTtDeNghiThanhToanKhvService keHoachVonDeNghiThanhToanService,
            IVdtNhaThauService nhaThauService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper) : base(mapper, storageServiceFactory, attachService)
        {
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _vdtKhvPhanBoVonService = vdtKhvPhanBoVonService;
            _vdtDaTtHopDongService = vdtDaTtHopDongService;
            _chuDauTuService = chuDauTuService;
            _duAnService = duAnService;
            _pheDuyetChiTietService = pheDuyetChiTietService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _nhaThauService = nhaThauService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _keHoachVonDeNghiThanhToanService = keHoachVonDeNghiThanhToanService;
            _mapper = mapper;

            SaveAndTranferCommand = new RelayCommand(obj => OnSaveAndTranfer(obj));
            AddThongTinCanCuCommand = new RelayCommand(obj => OnAddThongTinCanCu());
            DeleteThongTinCanCuCommand = new RelayCommand(obj => OnDeleteThongTinCanCu());
        }

        #region RelayCommand Event
        public override void Init()
        {
            ResetCondition();
            LoadData();
            LoadAttach();
            onChangeTyLeThanhToan();
            OnPropertyChanged(nameof(FTyLeThanhToan));
        }

        public void ResetCondition()
        {
            INamKeHoach = null;
            CbxLoaiDonVi = new ObservableCollection<ComboboxItem>();
            DataNguonVon = new ObservableCollection<ComboboxItem>();
            //DataKeHoachVon = new ObservableCollection<ComboboxItem>();
            ListKeHoachVon = new ObservableCollection<KeHoachVonModel>();
            //CbxLoaiThanhToan = new ObservableCollection<ComboboxItem>();
            DataDuAn = new ObservableCollection<ComboboxItem>();
            DataDuAnHangMuc = new ObservableCollection<ComboboxItem>();
            DataHopDong = new ObservableCollection<ComboboxItem>();
            NhaThauItems = new ObservableCollection<VdtDmNhaThau>();
            DNgayPheDuyet = null;
            DNgayDeNghi = null;
            GiaTriHopDong = 0;
            LuyKeThanhToanKLHTVonTN = 0;
            LuyKeThanhToanKLHTVonNN = 0;
            LuyKeTamUngChuaThuHoiVonUngTruocTN = 0;
            LuyKeTamUngChuaThuHoiVonUngTruocNN = 0;
            LuyKeTamUngChuaThuHoiVonTN = 0;
            LuyKeTamUngChuaThuHoiVonNN = 0;
            tyLeThanhToan = "0";
            TenNhaThau = string.Empty;
            StkNhaThau = string.Empty;
            MaNganHang = string.Empty;
        }


        public override void LoadData(params object[] args)
        {
            LoadAttach();
            Model.PropertyChanged += VdtTtDeNghiThanhToanModel_PropertyChanged;
            if (Model != null && Model.Id != Guid.Empty)
            {
                //update
                DNgayDeNghi = Model.dNgayDeNghi;
                DNgayPheDuyet = Model.dNgayPheDuyet;
                _iNamKeHoach = Model.iNamKeHoach;
                BHoanTraUngTruoc = Model.BHoanTraUngTruoc;
                BThanhToanTheoHopDong = Model.BThanhToanTheoHopDong;
                sSoDeNghi = Model.sSoDeNghi;

                //LoadThongTinCanCu();
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
                            OnPropertyChanged(nameof(ChuDauTuSelected));
                        }
                    }
                }
                if (QlnsData.nguonNganSach.ContainsKey(Model.iID_NguonVonID.ToString()))
                {
                    sNguonVon = QlnsData.nguonNganSach[Model.iID_NguonVonID.ToString()];
                }
                // calculate ty le thanh toan
                onChangeTyLeThanhToan();
            }
            else
            {
                //add
                ThongTinCanCuItems = new ObservableCollection<VdtTtThongTinCanCuModel>();
                DNgayDeNghi = DateTime.Now;
                INamKeHoach = DateTime.Now.Year;
                sNguonVon = _sessionService.Current.BudgetStr;
                Model.sSoDeNghi = generateSoDeNghiByNamLamViec(DateTime.Now.Year);
                Model.iID_NguonVonID = _sessionService.Current.Budget;
                BThanhToanTheoHopDong = true;
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
            IsLuuChuyen = false;
            OnPropertyChanged(nameof(IsLuuChuyen));
        }

        private void LoadThongTinCanCu()
        {
            List<VdtTtThongTinCanCu> listThongTinCanCu = _keHoachVonDeNghiThanhToanService.GetThongTinCanCuByIdDeNghiThanhToan(Model.Id).ToList();
            ThongTinCanCuItems = _mapper.Map<ObservableCollection<VdtTtThongTinCanCuModel>>(listThongTinCanCu);

            foreach (var model in ThongTinCanCuItems)
            {
                model.PropertyChanged += ThongTinCanCuModel_PropertyChanged;
            }
        }

        private void OnSaveAndTranfer(object obj)
        {
            IsLuuChuyen = true;
            OnSave(obj);
        }

        private void OnAddThongTinCanCu()
        {
            int stt = 1;
            VdtTtThongTinCanCuModel newItem = new VdtTtThongTinCanCuModel();
            stt = GetSoThuTu(newItem);
            if (ThongTinCanCuItems != null && ThongTinCanCuItems.Count > 0)
            {
                if (ThongTinCanCuSelected != null)
                {

                    VdtTtThongTinCanCuModel sourceItem = ThongTinCanCuSelected;
                    newItem = ObjectCopier.Clone(sourceItem);
                    newItem.Id = Guid.Empty;
                    newItem.iSTT = GetSoThuTu(newItem);
                }
            }
            newItem.PropertyChanged += ThongTinCanCuModel_PropertyChanged;
            ThongTinCanCuItems.Insert(ThongTinCanCuItems.Count, newItem);
            OnPropertyChanged(nameof(ThongTinCanCuItems));
        }

        private int GetSoThuTu(VdtTtThongTinCanCuModel targetItem)
        {
            int stt = 1;
            if (ThongTinCanCuItems.Count >= 1)
            {
                var Count = ThongTinCanCuItems.Count();

                var thongTinCanCuItemLastStt = ThongTinCanCuItems.Select(x => x.iSTT).Last() ?? 0;

                if (Count == thongTinCanCuItemLastStt)
                {
                    stt = thongTinCanCuItemLastStt + 1;
                    targetItem.iSTT = thongTinCanCuItemLastStt + 1;
                }
                else
                {
                    stt = Count + 1;
                    targetItem.iSTT = Count + 1;
                }

            }
            else
            {
                targetItem.iSTT = 1;
            }

            return stt;
        }

        private void OnDeleteThongTinCanCu()
        {
            if (ThongTinCanCuSelected != null)
            {
                ThongTinCanCuSelected.IsDeleted = !ThongTinCanCuSelected.IsDeleted;
            }            
            OnPropertyChanged(nameof(ThongTinCanCuItems));            
        }

        private void ThongTinCanCuModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (VdtTtThongTinCanCuModel)sender;            
            //FHanMucDauTu = DuAnNguonVonItems.Where(n => !n.IsDeleted).Sum(n => (n.FThanhTien ?? 0));
            //OnPropertyChanged(nameof(FHanMucDauTu));
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
                if (DNgayPheDuyet.HasValue)
                    dataInsert.DNgayPheDuyet = DNgayPheDuyet;
                dataInsert.INamKeHoach = INamKeHoach;
                dataInsert.IIdNguonVonId = int.Parse(NguonVonSelected.ValueItem);
                dataInsert.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                dataInsert.IIdDuAnId = Guid.Parse(DuAnSelected.HiddenValue);
                dataInsert.iCoQuanThanhToan = int.Parse(CoQuanThanhToanSelected.ValueItem);
                dataInsert.loaiCoQuanTaiChinh = loaiCoQuanTaiChinh;
                dataInsert.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                dataInsert.FGiaTriThuHoiUngTruocNn = Model.FGiaTriThuHoiUngTruocNn;
                dataInsert.BHoanTraUngTruoc = BHoanTraUngTruoc;
                dataInsert.FGiaTriThuHoiUngTruocTn = Model.FGiaTriThuHoiUngTruocTn;
                dataInsert.BThanhToanTheoHopDong = BThanhToanTheoHopDong;
                dataInsert.SMaNganHang = Model.SMaNganHang;
                dataInsert.sThongTinCanCu = Model.sThongTinCanCu;
                if (BThanhToanTheoHopDong)
                {
                    dataInsert.IIdChiPhiId = null;
                    dataInsert.IIdHopDongId = Guid.Parse(HopDongSelected.ValueItem);
                    dataInsert.IIdNhaThauId = NhaThauSelected.Id;
                }
                else
                {
                    dataInsert.IIdHopDongId = null;
                    dataInsert.IIdNhaThauId = null;
                    dataInsert.IIdChiPhiId = SelectedChiPhi.IIdChiPhiId;
                }
                dataInsert.BKhoa = false;
                if (ListKeHoachVon != null && ListKeHoachVon.Any(n => n.IsChecked))
                    dataInsert.IIdPhanBoVonID = ListKeHoachVon.FirstOrDefault(n => n.IsChecked).Id;

                // save gia tri thanh toan
                dataInsert.fLuyKeThanhToanTN = LuyKeThanhToanKLHTVonTN;
                dataInsert.fLuyKeThanhToanNN = LuyKeThanhToanKLHTVonNN;
                dataInsert.fLuyKeTUChuaThuHoiTN = LuyKeTamUngChuaThuHoiVonTN;
                dataInsert.fLuyKeTUChuaThuHoiNN = LuyKeTamUngChuaThuHoiVonNN;
                dataInsert.fLuyKeTUChuaThuHoiKhacTN = LuyKeTamUngChuaThuHoiVonUngTruocTN;
                dataInsert.fLuyKeTUChuaThuHoiKhacNN = LuyKeTamUngChuaThuHoiVonUngTruocNN;
                dataInsert.ID_DuAn_HangMuc = Guid.Parse(DuAnHangMucSelected.ValueItem);
                
                _deNghiThanhToanService.Insert(dataInsert, _sessionService.Current.Principal);
                deNghiThanhToanId = dataInsert.Id;                
                Model = _mapper.Map<VdtTtDeNghiThanhToanModel>(dataInsert);
                Model.Id = dataInsert.Id;
                OnPropertyChanged(nameof(IsInsert));
            }
            else
            {
                VdtTtDeNghiThanhToan itemUpdate = _deNghiThanhToanService.Find(dataInsert.Id);
                itemUpdate.SSoDeNghi = Model.sSoDeNghi;
                itemUpdate.SNguoiLap = Model.sNguoiLap;
                itemUpdate.FGiaTriThanhToanNN = Model.fGiaTriThanhToanNN;
                itemUpdate.FGiaTriThanhToanTN = Model.fGiaTriThanhToanTN;
                itemUpdate.DNgayDeNghi = DNgayDeNghi;
                itemUpdate.FGiaTriThuHoiNN = Model.fGiaTriThuHoiNN;
                itemUpdate.FGiaTriThuHoiTN = Model.fGiaTriThuHoiTN;
                itemUpdate.FThueGiaTriGiaTang = Model.FThueGiaTriGiaTang;
                itemUpdate.FChuyenTienBaoHanh = Model.FChuyenTienBaoHanh;
                itemUpdate.FGiaTriThuHoiUngTruocNn = Model.FGiaTriThuHoiUngTruocNn;
                itemUpdate.loaiCoQuanTaiChinh = loaiCoQuanTaiChinh;
                itemUpdate.FGiaTriThuHoiUngTruocTn = Model.FGiaTriThuHoiUngTruocTn;
                itemUpdate.FLuyKeGiaTriNghiemThuKlht = Model.FLuyKeGiaTriNghiemThuKlht;
                itemUpdate.ID_DuAn_HangMuc = Model.ID_DuAn_HangMuc;
                itemUpdate.SGhiChu = Model.sGhiChu;
                itemUpdate.SMaNganHang = Model.SMaNganHang;
                itemUpdate.DDateUpdate = DateTime.Now;
                itemUpdate.SUserUpdate = _sessionService.Current.Principal;
                itemUpdate.sThongTinCanCu = Model.sThongTinCanCu;
                if (BThanhToanTheoHopDong)
                {
                    itemUpdate.IIdChiPhiId = null;
                    itemUpdate.IIdHopDongId = Guid.Parse(HopDongSelected.ValueItem);
                    itemUpdate.IIdNhaThauId = NhaThauSelected.Id;
                }
                else
                {
                    itemUpdate.IIdHopDongId = null;
                    itemUpdate.IIdNhaThauId = null;
                    itemUpdate.IIdChiPhiId = SelectedChiPhi.IIdChiPhiId;
                }
                itemUpdate.iCoQuanThanhToan = int.Parse(CoQuanThanhToanSelected.ValueItem);
                itemUpdate.ILoaiThanhToan = int.Parse(CbxLoaiThanhToanSelected.ValueItem);
                itemUpdate.BHoanTraUngTruoc = BHoanTraUngTruoc;
                if (ListKeHoachVon != null && ListKeHoachVon.Any(n => n.IsChecked))
                    itemUpdate.IIdPhanBoVonID = ListKeHoachVon.FirstOrDefault(n => n.IsChecked).Id;

                // save gia tri thanh toan
                itemUpdate.fLuyKeThanhToanTN = LuyKeThanhToanKLHTVonTN;
                itemUpdate.fLuyKeThanhToanNN = LuyKeThanhToanKLHTVonNN;
                itemUpdate.fLuyKeTUChuaThuHoiTN = LuyKeTamUngChuaThuHoiVonTN;
                itemUpdate.fLuyKeTUChuaThuHoiNN = LuyKeTamUngChuaThuHoiVonNN;
                itemUpdate.fLuyKeTUChuaThuHoiKhacTN = LuyKeTamUngChuaThuHoiVonUngTruocTN;
                itemUpdate.fLuyKeTUChuaThuHoiKhacNN = LuyKeTamUngChuaThuHoiVonUngTruocNN;

                _deNghiThanhToanService.Update(itemUpdate);
                deNghiThanhToanId = itemUpdate.Id;
            }

            _keHoachVonDeNghiThanhToanService.DeleteByDeNghiThanhToanId(deNghiThanhToanId);
            foreach (KeHoachVonModel item in ListKeHoachVon.Where(n => n.IsChecked))
            {
                VdtTtDeNghiThanhToanKhv entityKhV = new VdtTtDeNghiThanhToanKhv();
                entityKhV.IIdDeNghiThanhToanId = deNghiThanhToanId;
                entityKhV.IIdKeHoachVonId = item.Id;
                entityKhV.ILoai = item.PhanLoai;
                //entityKhV.Id = item.Id;
                _keHoachVonDeNghiThanhToanService.Add(entityKhV);
            }

            //SaveThongTinCanCu();
            SaveAttachment(dataInsert.Id);
            Model = _mapper.Map<VdtTtDeNghiThanhToanModel>(dataInsert);
            Model.sTenDonVi = CbxLoaiDonViSelected.DisplayItem;
            Model.sTenDuAn = DuAnSelected.DisplayItem;
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (!IsLuuChuyen)
                System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            Model.IsLuuChuyen = IsLuuChuyen;
            SavedAction?.Invoke(Model);
            ((Window)obj).Close();
        }

        private void SaveThongTinCanCu()
        {
            var lstInsert = ThongTinCanCuItems.Where(x => !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null) && !String.IsNullOrEmpty(x.sNoiDung)).ToList();
            var lstUpdate = ThongTinCanCuItems.Where(x => !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            var lstDelete = ThongTinCanCuItems.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();

            if (lstInsert != null && lstInsert.Count > 0)
            {
                AddThongTinCanCuSave(lstInsert);
            }
            if (lstUpdate != null && lstUpdate.Count > 0)
            {
                UpdateThongTinCanCuSave(lstUpdate);
            }
            if (lstDelete != null && lstDelete.Count > 0)
            {
                DeleteThongTinCanCuSave(lstDelete);
            }
        }        

        private void AddThongTinCanCuSave(List<VdtTtThongTinCanCuModel> listAdd)
        {
            if (listAdd != null && listAdd.Count > 0)
            {
                foreach (var item in listAdd)
                {
                    item.iID_DeNghiThanhToanID = Model.Id;
                    item.dDateCreate = DateTime.Now;
                }
                List<VdtTtThongTinCanCu> listThongTinCanCu = new List<VdtTtThongTinCanCu>();
                listThongTinCanCu = _mapper.Map<List<VdtTtThongTinCanCu>>(listAdd);
                listThongTinCanCu.Select(n => { n.Id = Guid.NewGuid(); return n; }).ToList();
                _keHoachVonDeNghiThanhToanService.AddRangeThongTinCanCu(listThongTinCanCu);
            }
        }

        private void UpdateThongTinCanCuSave(List<VdtTtThongTinCanCuModel> listEdit)
        {
            foreach (var item in listEdit)
            {
                VdtTtThongTinCanCu thongTinCanCu = _keHoachVonDeNghiThanhToanService.FindThongTinCanCuById(item.Id);
                if (thongTinCanCu != null)
                {
                    thongTinCanCu.sNoiDung = item.sNoiDung;
                    thongTinCanCu.fGiaTri = item.fGiaTri;
                    thongTinCanCu.dDateUpdate = DateTime.Now;
                    _keHoachVonDeNghiThanhToanService.UpdateThongTinCanCu(thongTinCanCu);
                }
            }
        }

        private void DeleteThongTinCanCuSave(List<VdtTtThongTinCanCuModel> listDelete)
        {
            foreach (var item in listDelete)
            {
                _keHoachVonDeNghiThanhToanService.DeleteThongTinCanCu(item.Id);
            }
        }
        #endregion

        #region Helper
        private bool CheckChangePaymentType()
        {
            var lstDetail = _pheDuyetChiTietService.GetAllVdtTTPheDuyetThanhToanChiTiet(Model.Id);
            if (lstDetail == null || lstDetail.Count == 0) return true;
            return false;
        }

        private void LoadDuAnHangMucByDuAn()
        {
            _dataDuAnHangMuc = new ObservableCollection<ComboboxItem>();
            List<VdtDaDuAnHangMuc> listHangMuc = _vdtDuAnHangMucService.FindByIdDuAn(Guid.Parse(DuAnSelected.HiddenValue)).ToList();
            listHangMuc.ForEach(hm =>
            {
                _dataDuAnHangMuc.Add(new ComboboxItem() { DisplayItem = hm.STenHangMuc, ValueItem = hm.Id.ToString() });
            });
            if(Model.ID_DuAn_HangMuc != null)
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

        private void KeHoachVonDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            KeHoachVonModel item = (KeHoachVonModel)sender;

            if (!item.IsChecked) return;
            foreach (var child in ListKeHoachVon)
            {
                if (child.Id != item.Id || child.PhanLoai != item.PhanLoai)
                    child.IsChecked = false;
            }
            selectedKeHoachVon = item.PhanLoai;
            LoadGiaTriThanhToan();
            OnPropertyChanged(nameof(ListKeHoachVon));
        }

        private void VdtTtDeNghiThanhToanModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtTtDeNghiThanhToanModel item = (VdtTtDeNghiThanhToanModel)sender;
            List<string> propertiesFiresChange = new List<string>();
            propertiesFiresChange.Add(nameof(item.fGiaTriThanhToanTN));
            propertiesFiresChange.Add(nameof(item.fGiaTriThanhToanNN));
            propertiesFiresChange.Add(nameof(item.fGiaTriThuHoiTN));
            propertiesFiresChange.Add(nameof(item.fGiaTriThuHoiNN));

            if (args.PropertyName == nameof(item.iNamKeHoach))
            {
                onChangeNamKeHoach();
                OnPropertyChanged(nameof(item.iNamKeHoach));
            }
            if (propertiesFiresChange.Contains(args.PropertyName))
            {
                onChangeTyLeThanhToan();
                OnPropertyChanged(nameof(FTyLeThanhToan));
            }
        }

        private void onChangeTyLeThanhToan()
        {
            double tyLeThanhToanDouble = (LuyKeThanhToanKLHTVonTN + LuyKeThanhToanKLHTVonNN + LuyKeTamUngChuaThuHoiVonTN +
                LuyKeTamUngChuaThuHoiVonNN + LuyKeTamUngChuaThuHoiVonUngTruocTN + LuyKeTamUngChuaThuHoiVonUngTruocNN +
                Model.fGiaTriThanhToanTN + Model.fGiaTriThanhToanNN - (Model.fGiaTriThuHoiTN + Model.fGiaTriThuHoiNN +
                                                                       Model.FGiaTriThuHoiUngTruocTn +
                                                                       Model.FGiaTriThuHoiUngTruocNn)) / GiaTriHopDong;
            if (Double.IsNaN(tyLeThanhToanDouble))
            {
                tyLeThanhToan = "0";
            }
            else tyLeThanhToan = String.Format("{0:0.00}", tyLeThanhToanDouble);
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
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số đề nghị");
            }
            if (!DNgayDeNghi.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày đề nghị");
            }
            if (CbxLoaiThanhToanSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Loại thanh toán");
            }
            if (DuAnSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Dự án");
            }
            if (BThanhToanTheoHopDong)
            {
                if (HopDongSelected == null)
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Hợp đồng");
                if (NhaThauSelected == null)
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nhà thầu");
            }
            else
            {
                if (SelectedChiPhi == null)
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Chi phí");
            }

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
            //if (_deNghiThanhToanService.CheckExistSoQuyetDinh(Model.Id, Model.sSoDeNghi))
            //{
            //    messageBuilder.Append(Resources.MsgTrungSoQuyetDinhs);
            //}
            if (DuAnHangMucSelected == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Hạng mục dự án");
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
            // 2 option này có cùng giá trị, nhưng sẽ phân biệt bằng cột khác
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.CQTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.CTC, ValueItem = ((int)CoQuanThanhToanEnum.Type.CQTC).ToString() });
            DataCoQuanThanhToan.Add(new ComboboxItem { DisplayItem = CoQuanThanhToanEnum.TypeName.TONKHOAN_DONVI, ValueItem = ((int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI).ToString() });
            if (Model != null && Model.Id != Guid.Empty && Model.iCoQuanThanhToan.HasValue)
            {
                CoQuanThanhToanSelected = DataCoQuanThanhToan.FirstOrDefault(n =>
                {
                    if (Model.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.KHO_BAC || Model.iCoQuanThanhToan.Value == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI)
                    {
                        return n.ValueItem == Model.iCoQuanThanhToan.Value.ToString();
                    }
                    else
                    {
                        if (Model.loaiCoQuanTaiChinh == 0)
                        {
                            return n.ValueItem == Model.iCoQuanThanhToan.Value.ToString() && n.DisplayItem == CoQuanThanhToanEnum.TypeName.CQTC;
                        }
                        else return n.ValueItem == Model.iCoQuanThanhToan.Value.ToString() && n.DisplayItem == CoQuanThanhToanEnum.TypeName.CTC;
                    }
                });
            }
            else
            {
                CoQuanThanhToanSelected = DataCoQuanThanhToan.FirstOrDefault();
            }
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
                {
                    DuAnSelected = _dataDuAn.Where(n => n.HiddenValue == Model.iID_DuAnId.Value.ToString()).FirstOrDefault();
                }
                else
                {
                    DuAnSelected = _dataDuAn.FirstOrDefault();
                }
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
            //DmChuDauTu parent = _chuDauTuService.FindByMaDonVi(CbxLoaiDonViSelected.ValueItem, _sessionService.Current.YearOfWork);
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
            //DmChuDauTu parent = _chuDauTuService.FindByParentId(id, _sessionService.Current.YearOfWork);
            DmChuDauTu parent = _chuDauTuService.FindByAllParentId(id);
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
            //DmChuDauTu parent = _chuDauTuService.FindByParentId(id, _sessionService.Current.YearOfWork);
            DmChuDauTu parent = _chuDauTuService.FindByAllParentId(id);
            if (result == null)
            {
                //result = _chuDauTuService.FindByIdDonViCha(id, _sessionService.Current.YearOfWork);
                var dataChild = _chuDauTuService.FindByAllIdDonViCha(id);
            }
            else
            {
                //var dataChild = _chuDauTuService.FindByIdDonViCha(id, _sessionService.Current.YearOfWork);
                var dataChild = _chuDauTuService.FindByAllIdDonViCha(id);
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
                    DisplayItem = string.Format("{0} - {1}", item.SSoHopDong, item.STenHopDong),
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
            if (_hopDongSelected != null && !string.IsNullOrEmpty(_hopDongSelected.ValueItem))
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
            //if (_dicNhaThau == null)
            //    _dicNhaThau = new Dictionary<Guid, ObservableCollection<VdtDmNhaThau>>();
            //if (!_dicNhaThau.ContainsKey(iIdHopDong))
            //{
            //    var lstNhaThau = _nhaThauService.GetNhaThauByHopDong(iIdHopDong);
            //    _dicNhaThau.Add(iIdHopDong, new ObservableCollection<VdtDmNhaThau>(lstNhaThau));
            //}
            //NhaThauItems = _dicNhaThau[iIdHopDong];

            var lstNhaThau = _nhaThauService.GetNhaThauByHopDong(iIdHopDong);
            NhaThauItems = new ObservableCollection<VdtDmNhaThau>(lstNhaThau);
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
                if (Model.Id == Guid.Empty)
                {
                    Model.STenDonViThuHuong = NhaThauSelected.STenNhaThau;
                    Model.SSoTaiKhoanNhaThau = NhaThauSelected.SSoTaiKhoan;
                    Model.SMaNganHang = NhaThauSelected.SMaNganHang;
                }
            }
            OnPropertyChanged(nameof(Model));
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            //predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            List<DonVi> donViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
            //List<DonVi> donViData = _nsDonViService.FindByAllDataDonVi()
            .Where(n => _lstDonViExclude.Contains(n.Loai) && (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi)).ToList();
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
            {
                donViData = donViData.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
            }
            else
            {
                donViData = new List<DonVi>();
            }
            var cbxLoaiDonViData = donViData.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIDMaDonVi,
                DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi),
                HiddenValue = n.Id.ToString()
            });
            _cbxLoaiDonVi = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
            if (!string.IsNullOrEmpty(iIdDonVi))
            {
                CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
            }
            else
            {
                CbxLoaiDonViSelected = null;
            }
            OnPropertyChanged(nameof(CbxLoaiDonVi));
        }

        public void LoadLoaiThanhToan()
        {
            CbxLoaiThanhToan = new ObservableCollection<ComboboxItem>(new List<ComboboxItem>());
            CbxLoaiThanhToan.Add(new ComboboxItem { DisplayItem = "Thanh toán", ValueItem = "1" });
            CbxLoaiThanhToan.Add(new ComboboxItem { DisplayItem = "Tạm ứng", ValueItem = "2" });

            if (!Model.Id.IsNullOrEmpty())
            {
                CbxLoaiThanhToanSelected = CbxLoaiThanhToan.Where(n => n.ValueItem == Model.iLoaiThanhToan.ToString()).FirstOrDefault();
            }
            else
            {
                CbxLoaiThanhToanSelected = CbxLoaiThanhToan.FirstOrDefault();
            }
            OnPropertyChanged(nameof(CbxLoaiThanhToan));
            OnPropertyChanged(nameof(CbxLoaiThanhToanSelected));
        }

        public void LoadNguonNganSach()
        {
            if (DuAnSelected == null)
            {
                DataNguonVon = new ObservableCollection<ComboboxItem>();
                return;
            }
            List<NsNguonNganSach> lstNganSach = _nsNguonNganSachService.FindAll().OrderBy(n => n.IStt).ToList();
            _dataNguonVon = new ObservableCollection<ComboboxItem>();
            foreach (NsNguonNganSach item in lstNganSach)
            {
                _dataNguonVon.Add(new ComboboxItem { DisplayItem = item.STen, ValueItem = item.IIdMaNguonNganSach.HasValue ? item.IIdMaNguonNganSach.Value.ToString() : string.Empty });
            }
            if (_dataNguonVon != null && _dataNguonVon.Count > 0)
            {
                if (Model != null && Model.Id != Guid.Empty)
                {
                    NguonVonSelected = _dataNguonVon.Where(n => n.ValueItem == Model.iID_NguonVonID.ToString()).FirstOrDefault();
                }
                else
                {
                    NguonVonSelected = _dataNguonVon.FirstOrDefault();
                }
            }
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
                foreach (KeHoachVonModel item in ListKeHoachVon)
                {
                    item.PropertyChanged += KeHoachVonDetailModel_PropertyChanged;
                    if (listKeHoachVon.Where(n => n.IIdKeHoachVonId == item.Id && n.ILoai == item.PhanLoai).Count() > 0)
                    {
                        item.IsChecked = true;
                    }
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
            double luyKeTTTN = 0;
            double luyKeTTNN = 0;
            double luyKeTUTN = 0;
            double luyKeTUNN = 0;
            double luyKeTUUngTruocTN = 0;
            double luyKeTUUngTruocNN = 0;

            double sumTN = 0;
            double sumNN = 0;
            // add
            if (DNgayDeNghi == null
                || (BThanhToanTheoHopDong && (HopDongSelected == null || string.IsNullOrEmpty(HopDongSelected.ValueItem)))
                || (!BThanhToanTheoHopDong && (SelectedChiPhi == null || !SelectedChiPhi.IsChecked))
                || NguonVonSelected == null || string.IsNullOrEmpty(NguonVonSelected.ValueItem) || INamKeHoach == null || CoQuanThanhToanSelected == null || selectedKeHoachVon == 0)
            {
                LuyKeThanhToanKLHTVonTN = 0;
                LuyKeThanhToanKLHTVonNN = 0;
                LuyKeTamUngChuaThuHoiVonTN = 0;
                LuyKeTamUngChuaThuHoiVonNN = 0;
                LuyKeTamUngChuaThuHoiVonUngTruocTN = 0;
                LuyKeTamUngChuaThuHoiVonUngTruocNN = 0;
                OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonTN));
                OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonNN));
                OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonTN));
                OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonNN));
                OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocTN));
                OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocNN));
                return;
            }
            string iIdChungTuId = string.Empty;

            if (BThanhToanTheoHopDong)
                iIdChungTuId = HopDongSelected.ValueItem;
            else
                iIdChungTuId = SelectedChiPhi.IIdChiPhiId.ToString();

            // add
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                _deNghiThanhToanService.LoadGiaTriThanhToan(int.Parse(CoQuanThanhToanSelected.ValueItem), DNgayDeNghi.Value, BThanhToanTheoHopDong, iIdChungTuId, int.Parse(NguonVonSelected.ValueItem), INamKeHoach.Value, Model.Id, loaiCoQuanTaiChinh,
                    ref luyKeTTTN, ref luyKeTTNN, ref luyKeTUTN, ref luyKeTUNN, ref luyKeTUUngTruocTN, ref luyKeTUUngTruocNN, ref sumTN, ref sumNN, selectedKeHoachVon);
                LuyKeThanhToanKLHTVonTN = luyKeTTTN;
                LuyKeThanhToanKLHTVonNN = luyKeTTNN;
                LuyKeTamUngChuaThuHoiVonTN = luyKeTUTN;
                LuyKeTamUngChuaThuHoiVonNN = luyKeTUNN;
                LuyKeTamUngChuaThuHoiVonUngTruocTN = luyKeTUUngTruocTN;
                LuyKeTamUngChuaThuHoiVonUngTruocNN = luyKeTUUngTruocNN;
            }
            else
            {
                LuyKeThanhToanKLHTVonTN = Model.fLuyKeThanhToanTN;
                LuyKeThanhToanKLHTVonNN = Model.fLuyKeThanhToanNN;
                LuyKeTamUngChuaThuHoiVonTN = Model.fLuyKeTUChuaThuHoiTN;
                LuyKeTamUngChuaThuHoiVonNN = Model.fLuyKeTUChuaThuHoiNN;
                LuyKeTamUngChuaThuHoiVonUngTruocTN = Model.fLuyKeTUChuaThuHoiKhacTN;
                LuyKeTamUngChuaThuHoiVonUngTruocNN = Model.fLuyKeTUChuaThuHoiKhacNN;
            }
            OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonTN));
            OnPropertyChanged(nameof(LuyKeThanhToanKLHTVonNN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonTN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonNN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocTN));
            OnPropertyChanged(nameof(LuyKeTamUngChuaThuHoiVonUngTruocNN));
        }

        private void LoadChiPhi()
        {
            ItemsChiPhi = new ObservableCollection<VdtTtDeNghiThanhToanChiPhiModel>();
            if (!DNgayDeNghi.HasValue || DuAnSelected == null) return;
            var lstChiPhi = _deNghiThanhToanService.GetChiPhiInDenghiThanhToanScreen(DNgayDeNghi.Value, Guid.Parse(DuAnSelected.HiddenValue)).Where(x=>x.FGiaTriPheDuyetQdDauTu != 0 || x.FGiaTriPheDuyetDuToan != 0);
            if (lstChiPhi == null) return;
            ItemsChiPhi = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiPhiModel>>(lstChiPhi);
            foreach (var item in ItemsChiPhi)
                item.PropertyChanged += ChiPhiDetailModel_PropertyChanged;
            if (Model.IIdChiPhiId.HasValue)
            {
                SelectedChiPhi = ItemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == Model.IIdChiPhiId.Value);
                SelectedChiPhi.IsChecked = true;
            }
            else
            {
                SelectedChiPhi = null;
            }
        }

        private void ChiPhiDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtTtDeNghiThanhToanChiPhiModel item = (VdtTtDeNghiThanhToanChiPhiModel)sender;
            LoadGiaTriThanhToan();
            if (!item.IsChecked) return;
            foreach (var child in ItemsChiPhi)
            {
                if (child.IIdChiPhiId != item.IIdChiPhiId)
                    child.IsChecked = false;
            }
            OnPropertyChanged(nameof(ListKeHoachVon));
        }

        private string generateSoDeNghiByNamLamViec(int? namLamViec)
        {
            VdtTtDeNghiThanhToan lastRow = _deNghiThanhToanService.getLastRowBySoDeNghi(namLamViec);
            if (lastRow != null)
            {
                int lastIndex = int.Parse(lastRow.SSoDeNghi.Split("-")[2]);
                string outputSoDeNghi = "DN-" + namLamViec + "-";
                if (lastIndex + 1 < 10)
                {
                    outputSoDeNghi += "000";
                }
                else if (lastIndex + 1 >= 10 && lastIndex + 1 < 100)
                {
                    outputSoDeNghi += "00";
                }
                else if (lastIndex + 1 >= 100 && lastIndex + 1 < 1000)
                {
                    outputSoDeNghi += "0";
                }

                outputSoDeNghi += (lastIndex + 1).ToString();
                return outputSoDeNghi;
            }
            else
            {
                return "DN-" + namLamViec + "-0001";
            }
        }

        private void onChangeNamKeHoach()
        {
            if (INamKeHoach > 0)
            {
                Model.sSoDeNghi = generateSoDeNghiByNamLamViec(INamKeHoach);
            }
        }

        private void KeHoachVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            LoadGiaTriThanhToan();
        }

        #endregion
    }
}
