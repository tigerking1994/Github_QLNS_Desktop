using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.NewSalary.NewCadres;
using VTS.QLNS.CTC.App.ViewModel.Salary.NewCadres.NewCadresBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresDialogViewModel : DialogViewModelBase<CadresNq104Model>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmPhuCapNq104Service _phuCapService;
        private readonly ITlDmPhuCapService _phuCapCuService;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmCapBacNq104Service _tlDmCapBacService;
        private readonly ITlDmCapBacLuongNq104Service _tlDmCapBacLuongService;
        private readonly ITlDmChucVuNq104Service _tlDmChucVuService;
        private readonly ITlDmTangGiamService _tlDmTangGiamService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapCuService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private readonly ITlDsCapNhapBangLuongNq104Service _tlDsCapNhapBangLuongService;
        private readonly ITlBangLuongThangNq104Service _tlBangLuongThangService;
        private readonly ITlDmCachTinhLuongChuanNq104Service _tlDmCachTinhLuongChuanService;
        private readonly ITlDmHslKeHoachService _tlDmHslKeHoachService;
        private readonly ITlCanBoCheDoBHXHService _tlCanBoCheDoBHXHService;
        private readonly TlDmCheDoBHXHService _tlDmCheDoBHXHService;
        private readonly ITlDmNgayNghiService _tTlDmNgayNghiService;
        private readonly ITlBangLuongThangBHXHNq104Service _iTlBangLuongThangBHXHService;
        private readonly ITlDmCachTinhLuongBaoHiemNq104Service _tlDmCachTinhLuongBaoHiemService;
        private readonly ITlCanBoCheDoBHXHChiTietService _canBoCheDoBHXHChiTietService;
        private readonly ITlCanBoPhuCapBridgeNq104Service _tlCanBoPhuCapBridgeNq104Service;
        private ICollectionView _phuCapView;
        private ICollectionView _canBoSearchView;
        private ICollectionView _cheDoView;
        private ICollectionView _chucVuView;
        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_DETAIL;

        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadresDialog);
        public override PackIconKind IconKind => PackIconKind.AccountDetails;
        public override string Title => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP" : "CẬP NHẬT ĐỐI TƯỢNG HƯỞNG LƯƠNG, PHỤ CẤP";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đối tượng hưởng lương, phụ cấp" : "Cập nhật đối tượng hưởng lương, phụ cấp";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;
        public bool IsFirst { get; set; }
        public bool IsVisibleCommonInfo => IsCommonInfoCollapse;
        public bool IsVisibleCapBacInfo => IsCapbacInfoCollapse;
        public bool IsVisibleChucVuInfo => IsChucVuInfoCollapse;
        public bool IsDisableLoaiNhom => IsLoaiNhom;
        private bool _isModifiedBHXH;
        public NewCadresDialog CadresDetail { get; set; }
        public List<DateTime?> LstHoliday { get; set; }
        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
            }
        }

        private bool _isLoaiNhom;
        public bool IsLoaiNhom
        {
            get => _isLoaiNhom;
            set
            {
                SetProperty(ref _isLoaiNhom, value);
                OnPropertyChanged(nameof(IsDisableLoaiNhom));
            }
        }

        private bool _isCommonInfoCollapse;
        public bool IsCommonInfoCollapse
        {
            get => _isCommonInfoCollapse;
            set
            {
                SetProperty(ref _isCommonInfoCollapse, value);
                OnPropertyChanged(nameof(IsVisibleCommonInfo));
            }
        }
        private bool _isCapbacInfoCollapse;
        public bool IsCapbacInfoCollapse
        {
            get => _isCapbacInfoCollapse;
            set
            {
                SetProperty(ref _isCapbacInfoCollapse, value);
                OnPropertyChanged(nameof(IsVisibleCommonInfo));
            }
        }
        private bool _isChucVuInfoCollapse;
        public bool IsChucVuInfoCollapse
        {
            get => _isChucVuInfoCollapse;
            set
            {
                SetProperty(ref _isChucVuInfoCollapse, value);
                OnPropertyChanged(nameof(IsVisibleCommonInfo));
            }
        }
        private ObservableCollection<TlCanBoCheDoBHXHModel> _dataCanBoCheDoBHXH;
        public ObservableCollection<TlCanBoCheDoBHXHModel> DataCanBoCheDoBHXH
        {
            get => _dataCanBoCheDoBHXH;
            set => SetProperty(ref _dataCanBoCheDoBHXH, value);
        }
        private ObservableCollection<TlCanBoCheDoBHXHModel> _allDataCanBoCheDoBHXH;
        public ObservableCollection<TlCanBoCheDoBHXHModel> AllDataCanBoCheDoBHXH
        {
            get => _allDataCanBoCheDoBHXH;
            set => SetProperty(ref _allDataCanBoCheDoBHXH, value);
        }
        private TlCanBoCheDoBHXHModel _selectedCanBoCheDoBHXH;
        public TlCanBoCheDoBHXHModel SelectedCanBoCheDoBHXH
        {
            get => _selectedCanBoCheDoBHXH;
            set
            {
                SetProperty(ref _selectedCanBoCheDoBHXH, value);
            }
        }
        private List<ComboboxItem> _cbxThangLuongCanCuDong;
        public List<ComboboxItem> CbxThangLuongCanCuDong
        {
            get => _cbxThangLuongCanCuDong;
            set => SetProperty(ref _cbxThangLuongCanCuDong, value);
        }
        private ComboboxItem _cbxThangLuongCanCuDongSelected;
        public ComboboxItem CbxThangLuongCanCuDongSelected
        {
            get => _cbxThangLuongCanCuDongSelected;
            set
            {
                SetProperty(ref _cbxThangLuongCanCuDongSelected, value);
            }
        }
        private ObservableCollection<AllowenceNq104Model> _itemsAllowence;
        public ObservableCollection<AllowenceNq104Model> ItemsAllowence
        {
            get => _itemsAllowence;
            set => SetProperty(ref _itemsAllowence, value);
        }

        private AllowenceNq104Model _selectedAllowence;
        public AllowenceNq104Model SelectedAllowence
        {
            get => _selectedAllowence;
            set
            {
                if (SetProperty(ref _selectedAllowence, value) && _selectedAllowence != null)
                {
                    SelectedAllowenceHuong = ItemsAllowenceHuong.FirstOrDefault(x => x.MaPhuCap.Equals(_selectedAllowence.MaPhuCap));
                }
            }
        }

        private ObservableCollection<AllowenceNq104Model> _itemsAllowenceHuong;
        public ObservableCollection<AllowenceNq104Model> ItemsAllowenceHuong
        {
            get => _itemsAllowenceHuong;
            set => SetProperty(ref _itemsAllowenceHuong, value);
        }

        private AllowenceNq104Model _selectedAllowenceHuong;
        public AllowenceNq104Model SelectedAllowenceHuong
        {
            get => _selectedAllowenceHuong;
            set => SetProperty(ref _selectedAllowenceHuong, value);
        }

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsEditVisibility));
                OnPropertyChanged(nameof(IsAddVisibility));
            }
        }

        public bool IsAddVisibility => ViewState == FormViewState.ADD;

        private LoaiSave _loaiSave;
        public LoaiSave LoaiSave
        {
            get => _loaiSave;
            set => SetProperty(ref _loaiSave, value);
        }

        private DateTime? _ngayNhapNgu;
        public DateTime? NgayNhapNgu
        {
            get => _ngayNhapNgu;
            set
            {
                SetProperty(ref _ngayNhapNgu, value);
                TinhNamThamNien();
            }
        }

        private DateTime? _ngayXuatNgu;
        public DateTime? NgayXuatNgu
        {
            get => _ngayXuatNgu;
            set
            {
                SetProperty(ref _ngayXuatNgu, value);
                TinhNamThamNien();
            }
        }

        private DateTime? _ngayTaiNgu;
        public DateTime? NgayTaiNgu
        {
            get => _ngayTaiNgu;
            set
            {
                SetProperty(ref _ngayTaiNgu, value);
                TinhNamThamNien();
            }
        }

        private DateTime? _ngayBatDauBaoLuuCvd;
        public DateTime? NgayBatDauBaoLuuCvd
        {
            get => _ngayBatDauBaoLuuCvd;
            set
            {
                SetProperty(ref _ngayBatDauBaoLuuCvd, value);
            }
        }

        private int _namThamNien;
        public int NamThamNien
        {
            get => _namThamNien;
            set => SetProperty(ref _namThamNien, value);
        }

        private bool _bTrangThaiNN;
        public bool BTrangThaiNN
        {
            get => _bTrangThaiNN;
            set
            {
                SetProperty(ref _bTrangThaiNN, value);
                Model.BNuocNgoai = value;
                LoadTiLeHuongNN(value);

            }
        }

        private decimal? _tienBaoLuuCvd;
        public decimal? TienBaoLuuCvd
        {
            get => _tienBaoLuuCvd;
            set
            {
                if (SetProperty(ref _tienBaoLuuCvd, value))
                {
                    DataModelChange(TypeChangeCustom.TienLuongBaoLuuCvd, value ?? 0);
                    SetPhucapTienLuongBaoLuuCVD();
                }
            }
        }

        private decimal? _tienLuongCvdCu;
        public decimal? TienLuongCvdCu
        {
            get => _tienLuongCvdCu;
            set
            {
                SetProperty(ref _tienLuongCvdCu, value);
            }
        }

        private decimal? _tienLuongCvd;
        public decimal? TienLuongCvd
        {
            get => _tienLuongCvd;
            set
            {
                SetProperty(ref _tienLuongCvd, value);
                //DataModelChange(1, value);

            }
        }

        private decimal? _tienNangLuongCvd;
        public decimal? TienNangLuongCvd
        {
            get => _tienNangLuongCvd;
            set
            {
                if (SetProperty(ref _tienNangLuongCvd, value))
                {
                    DataModelChange(TypeChangeCustom.TienNangLuongCvd, value);
                    SetPhucapTienNangLuongCVD();
                }
            }
        }

        private int? _soThangTinhBaoLuuCvd;
        public int? SoThangTinhBaoLuuCvd
        {
            get => _soThangTinhBaoLuuCvd;
            set
            {
                SetProperty(ref _soThangTinhBaoLuuCvd, value);
                Model.SoThangTinhBaoLuuCvd = value;
                if (!IsFirst)
                    SetValueBaoLuuCvd();
            }
        }


        private decimal? _tienBaoLuuCb;
        public decimal? TienBaoLuuCb
        {
            get => _tienBaoLuuCb;
            set
            {
                if (SetProperty(ref _tienBaoLuuCb, value))
                {
                    DataModelChange(TypeChangeCustom.TienLuongBaoLuuCb, value ?? 0);
                    SetPhucapLuongBaoLuuCapbac();
                }
            }
        }

        private decimal? _tienLuongCbCu;
        public decimal? TienLuongCbCu
        {
            get => _tienLuongCbCu;
            set
            {
                SetProperty(ref _tienLuongCbCu, value);
            }
        }

        private decimal? _tienLuongCb;
        public decimal? TienLuongCb
        {
            get => _tienLuongCb;
            set
            {
                SetProperty(ref _tienLuongCb, value);
                DataModelChange(TypeChangeCustom.TienLuongCb, value);

            }
        }

        private decimal? _tienNangLuongCb;
        public decimal? TienNangLuongCb
        {
            get => _tienNangLuongCb;
            set
            {
                if (SetProperty(ref _tienNangLuongCb, value))
                {
                    DataModelChange(TypeChangeCustom.TienNangLuongCb, value ?? 0);
                    SetPhucapNangLuongCapbac();
                }
            }
        }

        private int? _soThangTinhBaoLuuCb;
        public int? SoThangTinhBaoLuuCb
        {
            get => _soThangTinhBaoLuuCb;
            set
            {
                SetProperty(ref _soThangTinhBaoLuuCb, value);
                Model.SoThangTinhBaoLuuCb = value;
                if (!IsFirst && Model.NgayBaoLuuCb.HasValue && Model.TienLuongCb.HasValue)
                    SetValueBaoLuuCb();
            }
        }

        private int _thangThamNienNghe;
        public int ThangThamNienNghe
        {
            get => _thangThamNienNghe;
            set
            {
                SetProperty(ref _thangThamNienNghe, value);
                TinhNamThamNien();
            }
        }

        public bool IsReadOnly => _viewState == FormViewState.DETAIL;
        public bool ChooseEnabled => SelectedCanBo != null;

        private ObservableCollection<TlDmCapBacLuongNq104Model> _itemsLoaiNhom;
        public ObservableCollection<TlDmCapBacLuongNq104Model> ItemsLoaiNhom
        {
            get => _itemsLoaiNhom;
            set => SetProperty(ref _itemsLoaiNhom, value);
        }

        private TlDmCapBacLuongNq104Model _selectedLoaiNhom;
        public TlDmCapBacLuongNq104Model SelectedLoaiNhom
        {
            get => _selectedLoaiNhom;
            set
            {
                SetProperty(ref _selectedLoaiNhom, value);
                if (value != null)
                {
                    LoadDanhMucBacLuong();
                }
            }
        }

        private ObservableCollection<TlDmCapBacLuongNq104Model> _itemsBacTienLuong;
        public ObservableCollection<TlDmCapBacLuongNq104Model> ItemsBacTienLuong
        {
            get => _itemsBacTienLuong;
            set => SetProperty(ref _itemsBacTienLuong, value);
        }

        private TlDmCapBacLuongNq104Model _selectedBacTienLuong;
        public TlDmCapBacLuongNq104Model SelectedBacTienLuong
        {
            get => _selectedBacTienLuong;
            set
            {
                if (SetProperty(ref _selectedBacTienLuong, value))
                {
                    if (!IsFirst)
                    {
                        TienNangLuongCb = value?.TienNangLuong;
                        //Model.TienLuongCbCu = Model.TienLuongCbCu.HasValue ? Model.TienLuongCbCu : Model.TienLuongCb;
                    }
                    TienLuongCb = value?.TienLuong;
                    var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCB_TT"));
                    var tempLuongCu = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCB_TT_CU"));
                    if (temp != null && !IsFirst && _selectedBacTienLuong != null)
                    {
                        temp.GiaTri = _selectedBacTienLuong?.TienLuong;
                        SetValueBaoLuuCb();
                        //GetTienLuongBaoLuuCb(Model.TienLuongCbCu ?? Model.TienLuongCb, Model.TienBaoLuuCb, _selectedBacTienLuong.TienNangLuong, temp.GiaTri);
                    }
                    else if (_selectedBacTienLuong == null)
                    {
                        //SetDefaultValueBaoLuu();
                    }

                    if (tempLuongCu != null && !IsFirst && _selectedBacTienLuong != null)
                    {
                        tempLuongCu.GiaTri = _selectedBacTienLuong?.TienLuong;
                    }
                    SetPhucapLuongCapbacLuong();
                }
            }
        }

        private decimal? GetTienLuongBaoLuuCb(decimal? TienLuongCbCu, decimal? TienLuongBaoLuuCbCu, decimal? TienNangLuongCb, decimal? TienLuongCbMoi)
        {
            if (TienLuongCbCu > TienLuongCbMoi)
            {
                // tiền lương cũ + tiền lương bảo lưu cũ
                var sumLuongCuAndTienBaoLuu = TienLuongCbCu.GetValueOrDefault();
                Model.NgayBaoLuuCb = Model.NgayBaoLuuCb ?? DateTime.Now;
                if (Model.SoThangTinhBaoLuuCb != null)
                {
                    var lastDayOfMonth = Model.NgayBaoLuuCb.Value.AddMonths(Model.SoThangTinhBaoLuuCb ?? 0).AddDays(0);
                    if (Model.NgayBaoLuuCb >= lastDayOfMonth)
                    {
                        Model.NgayBaoLuuCb = null;
                        TienBaoLuuCb = 0;
                        Model.SoThangTinhBaoLuuCb = 0;
                    }
                    else
                    {
                        var sumLuongMoiAndTienNangluong = TienLuongCbMoi.GetValueOrDefault() + TienNangLuongCb.GetValueOrDefault();
                        TienBaoLuuCb = sumLuongCuAndTienBaoLuu - sumLuongMoiAndTienNangluong;
                    }
                }
                else
                {
                    var sumLuongMoiAndTienNangluong = TienLuongCbMoi.GetValueOrDefault() + TienNangLuongCb.GetValueOrDefault();
                    TienBaoLuuCb = sumLuongCuAndTienBaoLuu - sumLuongMoiAndTienNangluong < 0 ? 0 : sumLuongCuAndTienBaoLuu - sumLuongMoiAndTienNangluong;
                    Model.SoThangTinhBaoLuuCb = sumLuongCuAndTienBaoLuu - sumLuongMoiAndTienNangluong < 0 ? 0 : Model.SoThangTinhBaoLuuCb;
                }
            }
            else
            {
                Model.NgayBaoLuuCb = null;
                TienBaoLuuCb = 0;
            }
            OnPropertyChanged(nameof(TienBaoLuuCb));
            return 0;
        }
        private ObservableCollection<TlDmCapBacNq104Model> _itemsLoaiDoiTuong;
        public ObservableCollection<TlDmCapBacNq104Model> ItemsLoaiDoiTuong
        {
            get => _itemsLoaiDoiTuong;
            set => SetProperty(ref _itemsLoaiDoiTuong, value);
        }

        private TlDmCapBacNq104Model _selectedLoaiDoiTuong;
        public TlDmCapBacNq104Model SelectedLoaiDoiTuong
        {
            get => _selectedLoaiDoiTuong;
            set
            {
                if (SetProperty(ref _selectedLoaiDoiTuong, value))
                {
                    //LoadDanhMucCapBac();
                    //LoadDanhMucLoaiNhom();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _huongNNItems;
        public ObservableCollection<ComboboxItem> HuongNNItems
        {
            get => _huongNNItems;
            set => SetProperty(ref _huongNNItems, value);
        }

        private ComboboxItem _selectedHuongNN;
        public ComboboxItem SelectedHuongNN
        {
            get => _selectedHuongNN;
            set
            {
                SetProperty(ref _selectedHuongNN, value);
                LoadTiLeHuongNN();
            }
        }

        private ObservableCollection<TlDmCapBacNq104Model> _itemsCapBac;
        public ObservableCollection<TlDmCapBacNq104Model> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacNq104Model _selectedCapBac;
        public TlDmCapBacNq104Model SelectedCapBac
        {
            get => _selectedCapBac;
            set
            {
                SetProperty(ref _selectedCapBac, value);
                if (value != null)
                {
                    LoadBacLuongChange();
                    LoadDanhMucLoaiNhom();
                    if (_selectedCapBac.Parent == "1" || _selectedCapBac.Parent == "5" || _selectedCapBac.Parent == "4" || _selectedCapBac.Parent == "3.2")
                    {
                        IsLoaiNhom = false;
                    }
                    else
                    {
                        IsLoaiNhom = true;
                    }
                }
                else
                {
                    IsLoaiNhom = true;
                    //LoadDanhMucLoaiNhom();
                    //SetDefaultValueBaoLuu();
                }
                SetPhucapLuongCapbac();
            }
        }

        private ObservableCollection<TlDmCapBacKeHoachModel> _itemsCapBacKeHoach;
        public ObservableCollection<TlDmCapBacKeHoachModel> ItemsCapBacKeHoach
        {
            get => _itemsCapBacKeHoach;
            set => SetProperty(ref _itemsCapBacKeHoach, value);
        }

        private TlDmCapBacKeHoachModel _selectedCapBacKeHoach;
        public TlDmCapBacKeHoachModel SelectedCapBacKeHoach
        {
            get => _selectedCapBacKeHoach;
            set => SetProperty(ref _selectedCapBacKeHoach, value);
        }

        private TlDmCapBacKeHoachModel _selectedCapBacKeHoachTran;
        public TlDmCapBacKeHoachModel SelectedCapBacKeHoachTran
        {
            get => _selectedCapBacKeHoachTran;
            set => SetProperty(ref _selectedCapBacKeHoachTran, value);
        }

        private ObservableCollection<TlDmHslKeHoachModel> _itemsHslKeHoach;
        public ObservableCollection<TlDmHslKeHoachModel> ItemsHslKeHoach
        {
            get => _itemsHslKeHoach;
            set => SetProperty(ref _itemsHslKeHoach, value);
        }

        private TlDmHslKeHoachModel _selectedHslKeHoach;
        public TlDmHslKeHoachModel SelectedHslKeHoach
        {
            get => _selectedHslKeHoach;
            set => SetProperty(ref _selectedHslKeHoach, value);
        }

        private TlDmHslKeHoachModel _selectedHslKeHoachTran;
        public TlDmHslKeHoachModel SelectedHslKeHoachTran
        {
            get => _selectedHslKeHoachTran;
            set => SetProperty(ref _selectedHslKeHoachTran, value);
        }

        private ObservableCollection<TlDmChucVuNq104Model> _itemsChucVu;
        public ObservableCollection<TlDmChucVuNq104Model> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuNq104Model _selectedChucVu;
        public TlDmChucVuNq104Model SelectedChucVu
        {
            get => _selectedChucVu;
            set
            {
                SetProperty(ref _selectedChucVu, value);
                TienLuongCvd = value?.TienLuong;

                if (!IsFirst)
                {
                    TienNangLuongCvd = value?.TienNangLuong;
                    //Model.TienNangLuongCvd = value?.TienNangLuong;
                    SetValueBaoLuuCvd();
                    LoadHeSoChucVu();
                }


            }
        }

        private ObservableCollection<TlDmTangGiamModel> _itemsTangGiam;
        public ObservableCollection<TlDmTangGiamModel> ItemsTangGiam
        {
            get => _itemsTangGiam;
            set => SetProperty(ref _itemsTangGiam, value);
        }

        private ObservableCollection<QsMucLucModel> _tangGiamItems;
        public ObservableCollection<QsMucLucModel> TangGiamItems
        {
            get => _tangGiamItems;
            set => SetProperty(ref _tangGiamItems, value);
        }

        private QsMucLucModel _selectedTangGiamItems;
        public QsMucLucModel SelectedTangGiamItems
        {
            get => _selectedTangGiamItems;
            set => SetProperty(ref _selectedTangGiamItems, value);
        }

        private string _selectedTangGiamIndex;
        public string SelectedTangGiamIndex
        {
            get => _selectedTangGiamIndex;
            set => SetProperty(ref _selectedTangGiamIndex, value);
        }

        public string ComboboxDisplayMemberPathTangGiam => nameof(SelectedTangGiamItems.TangGiam);

        private List<ComboboxItem> _gender;
        public List<ComboboxItem> GenderData
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private ComboboxItem _selectedGender;
        public ComboboxItem SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value);
        }

        private ObservableCollection<TlDmDonViNq104Model> _donviData;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViNq104Model _selectedDonVi;
        public TlDmDonViNq104Model SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ICollectionView _canBoView;
        public ICollectionView CanBoView
        {
            get => _canBoView;
            set
            {
                SetProperty(ref _canBoView, value);
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }

        public bool IsEditVisibility => ViewState != FormViewState.ADD;

        private bool _nextEnabled;
        public bool NextEnabled
        {
            get => _nextEnabled;
            set => SetProperty(ref _nextEnabled, value);
        }

        private bool _backEnabled;
        public bool BackEnabled
        {
            get => _backEnabled;
            set => SetProperty(ref _backEnabled, value);
        }

        private ObservableCollection<CadresNq104Model> _lstCanBo;
        public ObservableCollection<CadresNq104Model> LstCanBo
        {
            get => _lstCanBo;
            set
            {
                SetProperty(ref _lstCanBo, value);
                OnPropertyChanged(nameof(Model));
            }
        }

        private CadresNq104Model _selectedCanBo;
        public CadresNq104Model SelectedCanBo
        {
            get => _selectedCanBo;
            set
            {
                SetProperty(ref _selectedCanBo, value);
                OnPropertyChanged(nameof(ChooseEnabled));
            }
        }

        private string _searchMaPhuCap;
        public string SearchMaPhuCap
        {
            get => _searchMaPhuCap;
            set => SetProperty(ref _searchMaPhuCap, value);
        }

        private string _searchTenPhuCap;
        public string SearchTenPhuCap
        {
            get => _searchTenPhuCap;
            set => SetProperty(ref _searchTenPhuCap, value);
        }

        private string _searchMaCheDo;
        public string SearchMaCheDo
        {
            get => _searchMaCheDo;
            set => SetProperty(ref _searchMaCheDo, value);
        }

        private string _searchTenCheDo;
        public string SearchTenCheDo
        {
            get => _searchTenCheDo;
            set => SetProperty(ref _searchTenCheDo, value);
        }

        private string _searchCanbo;
        public string SearchCanBo
        {
            get => _searchCanbo;
            set => SetProperty(ref _searchCanbo, value);
        }

        private string _searchSoSoLuong;
        public string SearchSoSoLuong
        {
            get => _searchSoSoLuong;
            set => SetProperty(ref _searchSoSoLuong, value);
        }

        private string _searchDoiTuong;
        public string SearchDoiTuong
        {
            get => _searchDoiTuong;
            set => SetProperty(ref _searchDoiTuong, value);
        }

        private string _searchCapBac;
        public string SearchCapBac
        {
            get => _searchCapBac;
            set => SetProperty(ref _searchCapBac, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private decimal? _heSoLuong;
        public decimal? HeSoLuong
        {
            get => _heSoLuong;
            set
            {
                if (SetProperty(ref _heSoLuong, value))
                {
                    LoadHsl();
                }
                Model.HeSoLuong = _heSoLuong;
            }
        }

        private decimal? _soNguoiPhuThuoc;
        public decimal? SoNguoiPhuThuoc
        {
            get => _soNguoiPhuThuoc;
            set
            {
                SetProperty(ref _soNguoiPhuThuoc, value);
                if (ItemsAllowence != null && _soNguoiPhuThuoc != null)
                {
                    ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN).GiaTri = _soNguoiPhuThuoc;
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private List<TlCachTinhLuongNq104Model> dsCongThucLuong;
        public List<TlCachTinhLuongNq104Model> DsCongThucLuong
        {
            get => dsCongThucLuong;
            set => dsCongThucLuong = value;
        }

        private List<ComboboxItem> _itemsNhom;
        public List<ComboboxItem> ItemsNhom
        {
            get => _itemsNhom;
            set => SetProperty(ref _itemsNhom, value);
        }

        private ComboboxItem _selectedNhom;
        public ComboboxItem SelectedNhom
        {
            get => _selectedNhom;
            set
            {
                SetProperty(ref _selectedNhom, value);
                if (_selectedCapBac != null)
                {
                    Model.MaCb = _selectedCapBac.MaCb;
                    LoadTangQuanHam(Model.MaCb);
                }
            }
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                SetProperty(ref _selectedTab, value);
            }
        }

        private int _selectedPhuCapTab;
        public int SelectedPhuCapTab
        {
            get => _selectedPhuCapTab;
            set
            {
                SetProperty(ref _selectedPhuCapTab, value);
            }
        }

        private bool _isReadOnlyBHXH;
        public bool IsReadOnlyBHXH
        {
            get => _isReadOnlyBHXH;
            set => SetProperty(ref _isReadOnlyBHXH, value);
        }

        private int? _selectedThangLCCD;
        public int? SelectedThangThangLCCD
        {
            get => _selectedThangLCCD;
            set
            {
                SetProperty(ref _selectedThangLCCD, value);
            }
        }

        public NewCadresBHXHViewModel CadresBHXHViewModel { get; }

        public RelayCommand FirstCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand NextCommand { get; }
        public RelayCommand LastCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ResetFilterRegimeCommand { get; set; }
        public RelayCommand SuaCommand { get; set; }
        public RelayCommand SearchCanBoCommand { get; set; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand SaveAndCloseCommand { get; }
        public RelayCommand SaveAndCopyCommand { get; }
        public RelayCommand CloseDatagridCommand { get; }
        public RelayCommand ChooseCommand { get; }
        public RelayCommand DeleteCheDoBHXHCommand { get; }
        public RelayCommand SearchRegimeCommand { get; set; }
        public RelayCommand ShowPopupDetailCommand { get; }

        public NewCadresDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmPhuCapNq104Service phuCapService,
            ITlDmPhuCapService phuCapCuService,
            ITlDmCanBoNq104Service cadresService,
            ITlDmCapBacNq104Service tlDmCapBacService,
            ITlDmCapBacLuongNq104Service tlDmCapBacLuongService,
            ITlDmChucVuNq104Service tlDmChucVuService,
            ITlDmTangGiamService tlDmTangGiamService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
            ITlCanBoPhuCapService tlCanBoPhuCapCuService,
            ITlDmDonViNq104Service tlDmDonViService,
            INsQsMucLucService qsMucLucService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            ITlDsCapNhapBangLuongNq104Service tlDsCapNhapBangLuongService,
            ITlBangLuongThangNq104Service tlBangLuongThangService,
            ITlDmCachTinhLuongChuanNq104Service tlDmCachTinhLuongChuanService,
            ITlDmHslKeHoachService tlDmHslKeHoachService,
            ITlCanBoCheDoBHXHService tlCanBoCheDoBHXHService,
            TlDmCheDoBHXHService tlDmCheDoBHXHService,
            ITlDmNgayNghiService tlDmNgayNghiService,
            ITlBangLuongThangBHXHNq104Service iTlBangLuongThangBHXHService,
            ITlDmCachTinhLuongBaoHiemNq104Service tlDmCachTinhLuongBaoHiemService,
            NewCadresBHXHViewModel cadresBHXHViewModel,
            ITlCanBoCheDoBHXHChiTietService canBoCheDoBHXHChiTietService,
            ITlCanBoPhuCapBridgeNq104Service tlCanBoPhuCapBridgeNq104Service)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _cadresService = cadresService;
            _phuCapService = phuCapService;
            _phuCapCuService = phuCapCuService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmCapBacLuongService = tlDmCapBacLuongService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlDmTangGiamService = tlDmTangGiamService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlCanBoPhuCapCuService = tlCanBoPhuCapCuService;
            _tlDmDonViService = tlDmDonViService;
            _qsMucLucService = qsMucLucService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _tlDsCapNhapBangLuongService = tlDsCapNhapBangLuongService;
            _tlBangLuongThangService = tlBangLuongThangService;
            _tlDmCachTinhLuongChuanService = tlDmCachTinhLuongChuanService;
            _tlDmHslKeHoachService = tlDmHslKeHoachService;
            _tlCanBoCheDoBHXHService = tlCanBoCheDoBHXHService;
            _tlDmCheDoBHXHService = tlDmCheDoBHXHService;
            _tTlDmNgayNghiService = tlDmNgayNghiService;
            _iTlBangLuongThangBHXHService = iTlBangLuongThangBHXHService;
            _tlDmCachTinhLuongBaoHiemService = tlDmCachTinhLuongBaoHiemService;
            _tlCanBoPhuCapBridgeNq104Service = tlCanBoPhuCapBridgeNq104Service;
            _canBoCheDoBHXHChiTietService = canBoCheDoBHXHChiTietService;

            CadresBHXHViewModel = cadresBHXHViewModel;
            CadresBHXHViewModel.ParentPage = this;

            FirstCommand = new RelayCommand(o => OnFirst());
            PreviousCommand = new RelayCommand(o => OnPrevious());
            NextCommand = new RelayCommand(o => OnNext());
            LastCommand = new RelayCommand(o => OnLast());
            SearchCommand = new RelayCommand(o => _phuCapView.Refresh());
            SearchRegimeCommand = new RelayCommand(o => _cheDoView.Refresh());
            ResetFilterCommand = new RelayCommand(o => OnResetFilter());
            ResetFilterRegimeCommand = new RelayCommand(o => OnResetRegimeFilter());
            SuaCommand = new RelayCommand(o => OnUpdate());
            RefreshCommand = new RelayCommand(o => OnRefresh());
            SaveDataCommand = new RelayCommand(o => OnSaveAndClear());
            SaveAndCloseCommand = new RelayCommand(o => OnSaveAndClose(o));
            SaveAndCopyCommand = new RelayCommand(o => OnSaveAndCopy());
            CloseDatagridCommand = new RelayCommand(o => OnCloseSearchDatagrid());
            SearchCanBoCommand = new RelayCommand(o => OnSearchCanBo());
            ChooseCommand = new RelayCommand(o => OnChooseCanBo());
            DeleteCheDoBHXHCommand = new RelayCommand(obj => OnDeleteCheDoBHXH());
            ShowPopupDetailCommand = new RelayCommand(o => OnSelectionDoubleClick(o));
        }

        public override void Init()
        {
            try
            {
                if (!Model.Id.IsNullOrEmpty())
                {
                    var model = _cadresService.FindById(Model.Id);

                    Model = _mapper.Map<CadresNq104Model>(model);
                }
                _selectedTab = 0;
                _selectedPhuCapTab = 0;
                TabIndex = ImportTabIndex.Data;
                IsFirst = true;
                SearchCanBo = string.Empty;
                SearchCapBac = string.Empty;
                SearchDoiTuong = string.Empty;
                SearchSoSoLuong = string.Empty;
                if (Model.bKhongTinhNTN == null)
                    Model.bKhongTinhNTN = false;
                IsPopupOpen = false;
                //MarginRequirement = new Thickness(10);
                Model.PropertyChanged += DetailModel_PropertyChanged;
                NgayNhapNgu = Model.NgayNn;
                NgayXuatNgu = Model.NgayXn;
                NgayTaiNgu = Model.NgayTn;
                SelectedAllowence = null;
                if (IsFirst)
                {
                    SelectedBacTienLuong = null;
                    SelectedLoaiNhom = null;
                }
                LoadDonVi();
                LoadData();
                LoadTienLuongBaoLuu();
                LoadDanhMucCapBac();
                LoadListNhom();
                LoadCapBacKeHoach();
                LoadHslKeHoach();
                LoadDanhMucChucVu();
                LoadDanhMucTangGiam();
                LoadGender();
                LoadCanBo();
                FindCongThucLuong();
                LoadHuongNN();
                IsFirst = false;
                CheckValidModel();
                GetHolidays();
                CadresBHXHViewModel.UpdateParentWindowEventHandler += RefreshCBCD;
                _isModifiedBHXH = false;

                NgayBatDauBaoLuuCvd = Model.NgayBaoLuuCvd;
                TienBaoLuuCb = Model.TienBaoLuuCb;
                TienLuongCb = Model.TienLuongCb;
                TienLuongCbCu = Model.TienLuongCbCu;
                SoThangTinhBaoLuuCb = Model.SoThangTinhBaoLuuCb;
                TienNangLuongCb = Model.TienNangLuongCb;
                //cvcd
                TienBaoLuuCvd = Model.TienBaoLuuCvd;
                TienLuongCvd = Model.TienLuongCvd;
                TienLuongCvdCu = Model.TienLuongCvdCu;
                SoThangTinhBaoLuuCvd = Model.SoThangTinhBaoLuuCvd;
                TienNangLuongCvd = Model.TienNangLuongCvd;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHuongNN()
        {
            HuongNNItems = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem("40 %", "40"),
                new ComboboxItem("100 %", "100")
            };

            SelectedHuongNN = HuongNNItems.FirstOrDefault(x => (Decimal.TryParse(x.ValueItem, out decimal value) ? value : 0m) / 100 == Model.TyLeHuongNN);
            BTrangThaiNN = Model.BNuocNgoai ?? false;
        }

        private void LoadTiLeHuongNN(bool isChecked = true)
        {
            if (!isChecked && !ItemsAllowence.IsEmpty() && ItemsAllowence.Any(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONGNN)))
            {
                ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONGNN)).GiaTri = 0;
                return;

            }
            if (isChecked && SelectedHuongNN != null && Decimal.TryParse(SelectedHuongNN.ValueItem, out Decimal tileNN)
                && !ItemsAllowence.IsEmpty() && ItemsAllowence.Any(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONGNN)) && !IsFirst)
            {
                ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TILE_HUONGNN)).GiaTri = tileNN / 100;
            }

        }

        private void CheckValidModel()
        {
            var dtNow = DateTime.Now;
            var dNgayNhapNgu = NgayNhapNgu.HasValue ? NgayNhapNgu.Value.Date : dtNow.Date;
            var dNgayXuatNgu = NgayXuatNgu.HasValue ? NgayXuatNgu.Value.Date : dtNow.Date;
            if (NgayTaiNgu.HasValue && !NgayXuatNgu.HasValue || dNgayNhapNgu > dNgayXuatNgu)
            {
                MessageBoxHelper.Info(Resources.MessInvalidDateDemobilization);
            }
        }

        public override void LoadData(params object[] args)
        {

            base.LoadData(args);
            var canBoPhuCap = _tlCanBoPhuCapService.FirstOrDefault(x => x.MaCbo == Model.MaCanBo);
            var allowences = _mapper.Map<ObservableCollection<AllowenceNq104Model>>(_phuCapService.FindByCondition(x => x.Nam == _sessionService.Current.YearOfWork));

            var allowencesSaved = new List<AllowencePhuCapNq104Criteria>();
            try
            {
                if (canBoPhuCap != null)
                {
                    var plainText = CompressExtension.DecompressFromBase64(canBoPhuCap.Data);
                    allowencesSaved = JsonConvert.DeserializeObject<AllowenceCanBoNq104Criteria>(plainText).X.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            var listParent = allowences.Where(y => string.IsNullOrEmpty(y.Parent) && y.Chon.HasValue && y.Chon.Value).Select(x => x.MaPhuCap);

            var allowence = from x in allowences.Where(x => listParent.Contains(x.Parent)
                                && !new List<string> { "THUONG_TT", "GIAMTHUE_TT", "THUNHAPKHAC_TT", "THUEDANOP_TT", "TIENCTLH_TT", "TIENANDUONG_TT", "TIENTAUXE_TT" }.Contains(x.MaPhuCap)
                                && x.IsFormula == false
                                && x.Chon == true
                                && x.IsReadonly == false)
                            join z in allowences on x.Parent equals z.MaPhuCap into zj
                            from supez in zj.DefaultIfEmpty()
                            join y in allowencesSaved on x.MaPhuCap equals y.A into gj
                            from subpet in gj.DefaultIfEmpty()
                            select new
                            {
                                Data = x,
                                GiaTri = subpet?.B ?? x.GiaTri,
                                NgayHuongPhuCap = subpet?.C ?? x.HuongPCSN,
                                ParentName = $"{supez.MaPhuCap} - {supez.TenPhuCap}"
                            };
            foreach (var item in allowence)
            {
                item.Data.GiaTri = item.GiaTri;
                item.Data.HuongPCSN = item.NgayHuongPhuCap;
                item.Data.ParentName = item.ParentName;
            }

            var items = allowence.Select(x => x.Data).OrderBy(x => x.ParentName);
            foreach (var model in items)
            {
                model.PropertyChanged += DetailPhuCap_PropertyChanged;
            }

            //var checkHsl = items.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);
            var checkSoNgPT = items.FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN);

            if (checkSoNgPT != null && checkSoNgPT.GiaTri != 0m)
            {
                //_heSoLuong = checkHsl.GiaTri;
                _soNguoiPhuThuoc = checkSoNgPT.GiaTri;
            }
            else
            {
                var phuCapCanBo = _tlCanBoPhuCapService.FindByMaCanBo(Model.MaCanBo).FirstOrDefault(x => x.MaPhuCap == PhuCap.GTPT_SN);
                _heSoLuong = 0;
                _soNguoiPhuThuoc = phuCapCanBo?.GiaTri ?? 0;
                if (checkSoNgPT != null)
                    checkSoNgPT.GiaTri = phuCapCanBo?.GiaTri ?? 0;
            }

            if (ViewState == FormViewState.ADD)
            {
                var tm = items.FirstOrDefault(x => x.MaPhuCap == PhuCap.TM);
                if (tm != null)
                {
                    tm.GiaTri = 1;
                    tm.IsModified = false;
                }
            }
            ItemsAllowenceHuong = new ObservableCollection<AllowenceNq104Model>(items);
            foreach (var item in ItemsAllowenceHuong)
            {
                item.PropertyChanged += DetailPhuCapHuong_PropertyChanged;
            }


            ItemsAllowence = new ObservableCollection<AllowenceNq104Model>(items);
            _phuCapView = CollectionViewSource.GetDefaultView(ItemsAllowence);
            _phuCapView.GroupDescriptions.Add(new PropertyGroupDescription("ParentName"));
            _phuCapView.Filter = ListPhuCapFilter;

            LoadDataCheDoBHXH();
            OnPropertyChanged(nameof(ItemsAllowence));
            OnPropertyChanged(nameof(HeSoLuong));
            OnPropertyChanged(nameof(SoNguoiPhuThuoc));
        }

        private void OnSaveData()
        {
            try
            {
                TlDmCanBoNq104 tlDmCanBo;
                TlDmCanBoNq104 oldData = new TlDmCanBoNq104();
                if (Model.Id != Guid.Empty)
                {
                    oldData = _cadresService.Find(Model.Id);

                }

                //List<AllowenceNq104Model> listSave = ItemsAllowence.Where(x => !x.IsHangCha && x.IsModified).ToList();
                ObservableCollection<TlCanBoPhuCapNq104Model> tlCanBoPhuCapModels = new ObservableCollection<TlCanBoPhuCapNq104Model>();
                Model.NgayNn = NgayNhapNgu;
                Model.NgayXn = NgayXuatNgu;
                Model.NgayTn = NgayTaiNgu;
                Model.NamTn = NamThamNien;
                Model.ThangTnn = ThangThamNienNghe;
                Model.MaCb = SelectedCapBac.MaCb;
                Model.MaCvd104 = SelectedChucVu?.Ma;
                Model.LoaiDoiTuong = SelectedCapBac?.Parent;
                Model.MaCb104 = SelectedCapBac?.MaCb;
                Model.Loai = SelectedLoaiNhom?.MaDm;
                Model.NhomChuyenMon = SelectedLoaiNhom?.MaNhom;
                Model.MaBacLuong = SelectedBacTienLuong?.MaDm;
                Model.TienLuongCb = SelectedBacTienLuong?.TienLuong;
                Model.TienNangLuongCb = TienNangLuongCb;
                Model.TienNangLuongCvd = TienNangLuongCvd;
                Model.SoNguoiPhuThuoc = Convert.ToInt32(SoNguoiPhuThuoc);
                Model.TyLeHuongNN = BTrangThaiNN ? (Decimal.TryParse(SelectedHuongNN.ValueItem, out decimal value) ? value : 0m) / 100 : 0m;
                Model.SoThangTinhBaoLuuCvd = SoThangTinhBaoLuuCvd;
                Model.SoThangTinhBaoLuuCb = SoThangTinhBaoLuuCb;

                //Model.CapBac = SelectedCapBac.Note;
                Model.Parent = SelectedDonVi.MaDonVi;
                Model.TenDonVi = SelectedDonVi.TenDonVi;
                Model.ITrangThai = 2;
                if (SelectedCapBacKeHoach != null)
                {
                    Model.ThoiHanTangCb = SelectedCapBacKeHoach.ThoiHanTang;
                }

                //if (SelectedHslKeHoach != null)
                //{
                //    Model.CbKeHoach = SelectedHslKeHoach.Id.ToString();
                //    Model.HsLuongKeHoach = SelectedHslKeHoach.LhtHsKh;
                //}
                //else
                //{
                //    Model.ThoiHanTangCb = null;
                //    Model.CbKeHoach = Guid.Empty.ToString();
                //}

                //if (SelectedHslKeHoachTran != null)
                //{
                //    Model.IdLuongTran = SelectedHslKeHoachTran.Id;
                //    Model.HsLuongTran = SelectedHslKeHoachTran.LhtHsKh;
                //}

                Model.IsNam = (SelectedGender.ValueItem == Gender.NAM);

                //if (SelectedChucVu != null)
                //{
                //    Model.MaCv = SelectedChucVu.Ma;
                //}
                //else
                //{
                //    Model.MaCv = string.Empty;
                //}


                if (SelectedTangGiamItems != null)
                {
                    Model.MaTangGiam = SelectedTangGiamItems.SKyHieu;
                    if (Model.MaTangGiam.StartsWith("3")
                        && Model.MaTangGiam != "350"
                        && Model.MaTangGiam != "380")
                    {
                        Model.IsDelete = false;
                        Model.ITrangThai = 3;
                    }
                    if (Model.MaTangGiam == "290")
                    {
                        Model.ParentOld = oldData.Parent;
                    }
                }
                else
                {
                    if (oldData != null && oldData.Id != Guid.Empty
                        && !string.IsNullOrEmpty(oldData.MaTangGiam)
                        && oldData.MaTangGiam.StartsWith("3")
                        && oldData.MaTangGiam != "350"
                        && oldData.MaTangGiam != "380")
                    {
                        Model.IsDelete = true;
                    }
                    Model.MaTangGiam = String.Empty;
                }

                //if (SelectedNhom != null)
                //{
                //    Model.Nhom = SelectedNhom.ValueItem;
                //}

                var dataAllAllowence = _phuCapService.FindByCondition(x => x.Nam == _sessionService.Current.YearOfWork);

                foreach (var item in ItemsAllowence)
                {
                    var phucap = dataAllAllowence.FirstOrDefault(x => x.MaPhuCap == item.MaPhuCap);
                    if (phucap != null)
                    {
                        phucap.IsModified = item.IsModified;
                        phucap.GiaTri = item.GiaTri;
                        phucap.HuongPCSN = item.HuongPCSN;
                    }
                }

                foreach (var item in dataAllAllowence)
                {
                    if (item.MaPhuCap == PhuCap.PCTN_HS)
                    {
                        if (SelectedCapBac.MaCb.StartsWith("0"))
                            item.GiaTri = 0;
                    }
                    else if (item.MaPhuCap == PhuCap.PCANQP_HS)
                    {
                        if (SelectedCapBac.MaCb.Equals("415"))
                            item.GiaTri = (decimal?)0.5;
                        else if (SelectedCapBac.MaCb.Equals("413"))
                            item.GiaTri = (decimal?)0.3;
                    }
                    else if (new List<string>() { PhuCap.PCTEMTHU_TT, PhuCap.PCNU_HS }.Contains(item.MaPhuCap))
                    {
                        if (!SelectedCapBac.MaCb.StartsWith("0"))
                            item.GiaTri = 0;
                    }
                    else if (item.MaPhuCap == PhuCap.TM)
                    {
                        item.GiaTri = Model.Tm == true ? 0 : 1;
                    }
                    else if (item.MaPhuCap == PhuCap.Nop_BHTN)
                    {
                        item.GiaTri = Model.BHTN == true ? 1 : 0;
                    }
                    else if (item.MaPhuCap == PhuCap.Huong_PCCOV)
                    {
                        item.GiaTri = Model.PCCV == true ? 1 : 0;
                    }
                    else if (item.MaPhuCap == PhuCap.THANG_TCXN)
                    {
                        if (Model.NgayXn is object && SelectedCapBac.MaCb.StartsWith("0"))
                            item.GiaTri = TinhThangHuongTcxn(Model.NgayNn, Model.NgayXn);
                    }
                    else if (item.MaPhuCap == PhuCap.THANG_TCVIECLAM)
                    {
                        if (Model.NgayXn is null)
                            item.GiaTri = 0;
                    }
                    else if (item.MaPhuCap == PhuCap.BHXHDV_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.BhxhCq;
                    }
                    else if (item.MaPhuCap == PhuCap.BHXHCN_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.HsBhxh;
                    }
                    else if (item.MaPhuCap == PhuCap.BHYTDV_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.BhytCq;
                    }
                    else if (item.MaPhuCap == PhuCap.BHYTCN_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.HsBhyt;
                    }
                    else if (item.MaPhuCap == PhuCap.BHTNDV_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.BhtnCq;
                    }
                    else if (item.MaPhuCap == PhuCap.BHTNCN_HS)
                    {
                        item.GiaTri = SelectedBacTienLuong.HsBhtn;
                    }

                    else if (item.MaPhuCap == PhuCap.NTN)
                    {
                        item.GiaTri = NamThamNien;
                    }
                }

                var dataSave = dataAllAllowence
                    //.Where(x => (x.GiaTri.HasValue && x.GiaTri.Value > 0) || (x.HuongPCSN.HasValue && x.HuongPCSN.Value > 0))
                    .Select(x => new AllowencePhuCapNq104Criteria()
                    {
                        A = x.MaPhuCap,
                        B = x.GiaTri,
                        C = x.HuongPCSN
                    });

                string strJson = JsonConvert.SerializeObject(new AllowenceCanBoNq104Criteria()
                {
                    X = dataSave
                });

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    tlDmCanBo = _cadresService.Find(Model.Id);
                    var phuCapCanBo = _tlCanBoPhuCapService.FindByMaCanBo(Model.MaCanBo).FirstOrDefault();
                    _tlCanBoPhuCapService.DeleteByMaCanBo(Model.MaCanBo);
                    phuCapCanBo ??= new TlCanBoPhuCapNq104();
                    phuCapCanBo.MaCbo = Model.MaCanBo;

                    phuCapCanBo.MaPhuCap = "";
                    phuCapCanBo.Data = CompressExtension.CompressToBase64(strJson);
                    Model.DateModified = DateTime.Now;
                    // Tính toán tiền bảo lưu cấp bậc 
                    Model.TienBaoLuuCb = TienBaoLuuCb;
                    Model.TienLuongCb = TienLuongCb;
                    Model.TienLuongCbCu = TienLuongCbCu;
                    Model.NgayBaoLuuCb = NumberUtils.NumberIsNullOrZero(TienBaoLuuCb) ? null : Model.NgayBaoLuuCb;
                    if (Model.TienLuongCbCu.HasValue)
                        Model.NgayBaoLuuCb = Model.NgayBaoLuuCb ?? DateTime.Now;
                    // Tính toán tiền bảo lưu CV/CD
                    Model.TienBaoLuuCvd = TienBaoLuuCvd;
                    Model.TienLuongCvd = TienLuongCvd;
                    Model.TienLuongCvdCu = TienLuongCvdCu;
                    Model.NgayBaoLuuCvd = NumberUtils.NumberIsNullOrZero(TienBaoLuuCvd) ? null : NgayBatDauBaoLuuCvd;
                    if (Model.TienLuongCvdCu.HasValue)
                        Model.NgayBaoLuuCvd = Model.NgayBaoLuuCvd ?? DateTime.Now;
                    Model.UserModifier = _sessionService.Current.Principal;
                    if (_isModifiedBHXH)
                        SaveCheDoBHXH();
                    UpdateStatusHuongCheDoBHXH();
                    _mapper.Map(Model, tlDmCanBo);
                    _cadresService.Update(tlDmCanBo);
                    _tlCanBoPhuCapService.Add(phuCapCanBo);
                }
                else
                {
                    var phuCapCanBo = new TlCanBoPhuCapNq104();
                    phuCapCanBo.MaCbo = Model.MaCanBo;
                    phuCapCanBo.MaPhuCap = "";
                    phuCapCanBo.Data = CompressExtension.CompressToBase64(strJson);
                    Model.Thang = Int32.Parse(Model.MaCanBo.Substring(4, 2));
                    Model.IsDelete = true;
                    Model.IsLock = false;
                    // Tính toán tiền bảo lưu cấp bậc 
                    Model.TienBaoLuuCb = TienBaoLuuCb;
                    Model.TienLuongCb = TienLuongCb;
                    Model.TienLuongCbCu = TienLuongCbCu;
                    if (Model.TienLuongCbCu.HasValue)
                        Model.NgayBaoLuuCb = Model.NgayBaoLuuCb ?? DateTime.Now;

                    // Tính toán tiền bảo lưu CV/CD
                    Model.TienBaoLuuCvd = TienBaoLuuCvd;
                    Model.TienLuongCvd = TienLuongCvd;
                    Model.TienLuongCvdCu = TienLuongCvdCu;
                    if (Model.TienLuongCvdCu.HasValue)
                        Model.NgayBaoLuuCvd = Model.NgayBaoLuuCvd ?? DateTime.Now;
                    Model.UserModifier = _sessionService.Current.Principal;
                    Model.UserCreator = _sessionService.Current.Principal;
                    Model.DateCreated = DateTime.Now;
                    Model.MaCbCu = string.Empty;
                    Model.ITrangThai = 1;
                    if (_isModifiedBHXH)
                        SaveCheDoBHXH();
                    UpdateStatusHuongCheDoBHXH();
                    tlDmCanBo = new TlDmCanBoNq104();
                    _mapper.Map(Model, tlDmCanBo);
                    _cadresService.Add(tlDmCanBo);
                    _tlCanBoPhuCapService.Add(phuCapCanBo);
                }



                if (ViewState != FormViewState.ADD)
                {
                    ViewState = FormViewState.DETAIL;
                    LoadCanBo();
                }
                _tlCanBoPhuCapBridgeNq104Service.DataPreprocess(Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault());
                if (_isModifiedBHXH)
                    LoadDataCheDoBHXH();
                // Refresh state form
                ItemsAllowence.Select(x => { x.IsModified = false; return x; }).ToList();
                SavedAction?.Invoke(_mapper.Map<CadresNq104Model>(tlDmCanBo));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private decimal? GetLuongBaoLuu()
        {
            DateTime date = DateTime.Now;
            decimal? luongBaoLuu = 0;
            if (Model.NgayBaoLuuCb != null && Model.Thang != null)
            {
                var lastDayOfMonth = Model.NgayBaoLuuCb.Value.AddMonths(Model.Thang.Value).AddDays(-1);

                if (date > lastDayOfMonth)
                {
                    luongBaoLuu = 0;
                }
                else
                {
                    luongBaoLuu = Model.TienLuongCb.GetValueOrDefault() >= Model.TienLuongCbCu.GetValueOrDefault() ? null : Model?.TienBaoLuuCb;
                }
            }

            return luongBaoLuu;
        }

        private void OnSaveAndClear()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLEAR;
                OnSaveData();
                UpdateStatusHuongCheDoBHXH();
                if (ViewState == FormViewState.ADD)
                {
                    CadresNq104Model newModel = new CadresNq104Model();
                    newModel.Parent = Model.Parent;
                    newModel.TenDonVi = Model.TenDonVi;
                    NgayNhapNgu = null;
                    NgayXuatNgu = null;
                    NgayTaiNgu = null;
                    NamThamNien = 0;
                    ThangThamNienNghe = 0;
                    newModel.KhongLuong = false;
                    newModel.Tm = false;
                    newModel.BHTN = false;
                    newModel.PCCV = true;
                    newModel.MaHieuCanBo = (int.Parse(Model.MaHieuCanBo) + 1).ToString();
                    newModel.MaCanBo = Model.MaCanBo.Substring(0, 6) + newModel.MaHieuCanBo;
                    newModel.SoSoLuong = newModel.MaHieuCanBo.PadLeft(7, '0');
                    newModel.Nam = Model.Nam;
                    newModel.Thang = Model.Thang;
                    Model = newModel;
                    Init();

                }
                else
                {
                    ViewState = FormViewState.DETAIL;
                    LoadEnabled();
                }
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
            }
        }

        private void OnSaveAndClose(object obj)
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_CLOSE;
                OnSaveData();
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
                Window window = obj as Window;
                window.Close();
            }
        }

        private void OnSaveAndCopy()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Info(message);
            }
            else
            {
                LoaiSave = LoaiSave.SAVE_AND_COPY;
                OnSaveData();
                if (ViewState == FormViewState.ADD)
                {
                    MessageBoxHelper.Info(Resources.MsgAddCanbo);
                }
                else
                {
                    MessageBoxHelper.Info(Resources.MsgEditCanBo);
                }
                Model.MaHieuCanBo = (int.Parse(Model.MaHieuCanBo) + 1).ToString();
                Model.MaCanBo = Model.MaCanBo.Substring(0, 6) + Model.MaHieuCanBo;
                Model.SoSoLuong = Model.MaHieuCanBo.PadLeft(7, '0');
                OnPropertyChanged(nameof(Model));
            }
        }

        private void LoadCanBo()
        {
            BackEnabled = false;
            NextEnabled = false;

            if (CanBoView != null)
            {
                _lstCanBo = new ObservableCollection<CadresNq104Model>(CanBoView.Cast<CadresNq104Model>());
                _canBoSearchView = CollectionViewSource.GetDefaultView(_lstCanBo);
                _canBoSearchView.Filter = CanBoFilter;
            }

            LoadEnabled();
            OnPropertyChanged(nameof(LstCanBo));
            OnPropertyChanged(nameof(SelectedCanBo));
        }

        private void LoadListNhom()
        {
            ItemsNhom = new List<ComboboxItem>();
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN1, NhomQncn.SOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.SOCAPN2, NhomQncn.SOCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN1, NhomQncn.TRUNGCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.TRUNGCAPN2, NhomQncn.TRUNGCAPN2));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN1, NhomQncn.CAOCAPN1));
            _itemsNhom.Add(new ComboboxItem(NhomQncn.CAOCAPN2, NhomQncn.CAOCAPN2));

            _selectedNhom = _itemsNhom.FirstOrDefault(x => x.ValueItem.Equals(Model.Nhom));
            OnPropertyChanged(nameof(SelectedNhom));
            OnPropertyChanged(nameof(ItemsNhom));
        }

        private void LoadCapBacKeHoach()
        {
            var data = _tlDmCapBacKeHoachService.FindAll().OrderBy(x => x.MaCb);
            _itemsCapBacKeHoach = _mapper.Map<ObservableCollection<TlDmCapBacKeHoachModel>>(data);
            if (!string.IsNullOrEmpty(Model.CbKeHoach))
            {
                _selectedCapBacKeHoach = _itemsCapBacKeHoach.FirstOrDefault(x => x.Id.ToString() == Model.CbKeHoach);
            }
            if (Model.IdLuongTran != null)
            {
                _selectedCapBacKeHoachTran = _itemsCapBacKeHoach.FirstOrDefault(x => x.Id == Model.IdLuongTran);
            }
            OnPropertyChanged(nameof(SelectedCapBacKeHoach));
            OnPropertyChanged(nameof(SelectedCapBacKeHoachTran));
            OnPropertyChanged(nameof(ItemsCapBacKeHoach));
        }

        private void LoadHslKeHoach()
        {
            var data = _tlDmHslKeHoachService.FindAll().OrderBy(x => x.MaCb).ToList();
            ItemsHslKeHoach = _mapper.Map<ObservableCollection<TlDmHslKeHoachModel>>(data);
            SelectedHslKeHoach = _itemsHslKeHoach.FirstOrDefault(x => x.Id.ToString().Equals(Model.CbKeHoach));
            SelectedHslKeHoachTran = _itemsHslKeHoach.FirstOrDefault(x => x.Id == Model.IdLuongTran);
        }

        private void LoadDanhMucLoaiDoiTuong()
        {
            var listLoaiDoiTuong = _tlDmCapBacService.FindAll(x => string.IsNullOrEmpty(x.Parent)).OrderBy(x => x.MaCb).ToList();
            ItemsLoaiDoiTuong = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(listLoaiDoiTuong);
            SelectedLoaiDoiTuong = _itemsLoaiDoiTuong.FirstOrDefault(x => x.MaCb.Equals(Model.LoaiDoiTuong));
        }

        private void LoadDanhMucLoaiNhom()
        {
            string[] text = { "1", "3.2", "4", "5" };

            if (SelectedCapBac != null && text.Contains(SelectedCapBac.Parent))
            {
                ItemsLoaiNhom = new ObservableCollection<TlDmCapBacLuongNq104Model>();
                SelectedLoaiNhom = null;
                LoadDanhMucBacLuong();
            }
            else
            {
                var listLoaiNhom = _tlDmCapBacLuongService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).Where(x =>
                                SelectedCapBac != null
                                && !string.IsNullOrEmpty(x.LoaiDoiTuong)
                                && x.LoaiDoiTuong.Split(',').Contains(SelectedCapBac.Parent)
                                && (x.Loai == 1 || x.Loai == 2))
                        .OrderBy(x => x.XauNoiMa)
                        .ToList();

                var listLoai = listLoaiNhom.Where(x => x.Loai == 1).ToList();
                var listNhom = listLoaiNhom.Where(x => x.Loai == 2).ToList();

                var listData = from loai in listLoai
                               join nhom in listNhom on loai.MaDm equals nhom.MaDmCha
                               into gj
                               from full in gj.DefaultIfEmpty()
                               select new
                               {
                                   Data = loai,
                                   TenNhom = full?.TenDm,
                                   MaNhom = full?.MaDm,
                                   XauNoiMaNhom = full?.XauNoiMa
                               };
                var listEnd = ObjectCopier.Clone(listData).Select(x =>
                {
                    if (!string.IsNullOrEmpty(x.TenNhom))
                    {
                        x.Data.LoaiNhom = $"{x.Data.TenDm} - {x.TenNhom}";
                        x.Data.MaNhom = x.MaNhom;
                        x.Data.MaLoai = x.Data.MaDm;
                        x.Data.XauNoiMaNhom = x.XauNoiMaNhom;
                    }
                    else
                    {
                        x.Data.LoaiNhom = x.Data.TenDm;
                    }
                    return x.Data;
                }).ToList();
                ItemsLoaiNhom = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(listEnd);
                SelectedLoaiNhom = _itemsLoaiNhom.FirstOrDefault(x => x.MaLoai == Model.Loai && x.MaNhom == Model.NhomChuyenMon);
            }
        }

        private void LoadDanhMucBacLuong()
        {
            string[] text = { "1", "3.2", "4", "5" };

            var listBacTienLuong = _tlDmCapBacLuongService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork && x.Loai == 3)
                .OrderBy(x => x.XauNoiMa).ToList();

            if (SelectedCapBac != null && text.Contains(SelectedCapBac.Parent))
            {
                listBacTienLuong = listBacTienLuong.Where(x => !string.IsNullOrEmpty(x.LoaiDoiTuong)
                                && x.LoaiDoiTuong.Split(',').Contains(SelectedCapBac.Parent)).ToList();
            }
            else
            {
                listBacTienLuong = listBacTienLuong.Where(x => SelectedLoaiNhom != null
                                            && x.XauNoiMa.Contains(SelectedLoaiNhom.XauNoiMa + "-" + SelectedLoaiNhom.MaNhom)).ToList();
            }

            ItemsBacTienLuong = _mapper.Map<ObservableCollection<TlDmCapBacLuongNq104Model>>(listBacTienLuong);

            List<string> lstString = new List<string>() { LoaiDoiTuong.SQ, LoaiDoiTuong.HCY, LoaiDoiTuong.HSQCS };
            if (Model != null && !string.IsNullOrEmpty(Model.MaBacLuong) && SelectedLoaiNhom != null)
            {
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(string.IsNullOrEmpty(Model.MaBacLuong) ? SelectedCapBac?.MaCb : Model.MaBacLuong));
            }
            else if (lstString.Contains(SelectedCapBac.Parent) && SelectedLoaiNhom == null)
            {
                string sMaCapBac = SelectedCapBac.Parent.Equals(LoaiDoiTuong.HCY) ? SelectedCapBac.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ) : SelectedCapBac.MaCb;
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(sMaCapBac));
            }
            else if (SelectedLoaiNhom != null && SelectedLoaiNhom.XauNoiMaNhom != null)
            {
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.XauNoiMa.Contains(SelectedLoaiNhom?.XauNoiMaNhom));
            }
            else
            {
                SelectedBacTienLuong = ItemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(SelectedCapBac?.MaCb));
            }
        }

        private void SetValueBaoLuuCb()
        {
            if (Model.NgayBaoLuuCb.HasValue && SoThangTinhBaoLuuCb.HasValue && TienLuongCbCu.HasValue && SelectedBacTienLuong != null && SelectedBacTienLuong.TienLuong.HasValue)
            {
                if (Model.NgayBaoLuuCb.Value.Date.AddMonths(SoThangTinhBaoLuuCb.Value) > DateTime.Now.Date)
                {
                    //var fNangLuong = Model.IsNangLuongCb ? SelectedBacTienLuong.TienNangLuong : 0;
                    var fNangLuong = SelectedBacTienLuong?.TienNangLuong ?? 0;
                    Model.TienNangLuongCb = SelectedBacTienLuong.TienNangLuong;
                    if (TienLuongCbCu.Value > SelectedBacTienLuong.TienLuong.Value)
                    {
                        TienBaoLuuCb = TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - fNangLuong;
                        TienBaoLuuCb = TienBaoLuuCb < 0 ? null : TienBaoLuuCb;
                    }
                    //else
                    //{
                    //    SetDefaultValueBaoLuu();
                    //}
                }
                else
                {
                    SetDefaultValueBaoLuu();
                }
            }
            else if (Model.NgayBaoLuuCb.HasValue && TienLuongCbCu.HasValue && SelectedBacTienLuong != null && SelectedBacTienLuong.TienLuong.HasValue)
            {
                if (Model.TienLuongCb > SelectedBacTienLuong.TienLuong)
                {
                    TienBaoLuuCb = (Model.TienLuongCb - SelectedBacTienLuong.TienLuong) < 0 ? null : Model.TienLuongCb - SelectedBacTienLuong.TienLuong;
                }
                else if (Model.TienLuongCb == SelectedBacTienLuong.TienLuong && !string.IsNullOrEmpty(Model.NgayBaoLuuCb.ToString()))
                {
                    var fTienBaoLuuCb = TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - SelectedBacTienLuong.TienNangLuong.GetValueOrDefault();
                    TienBaoLuuCb = fTienBaoLuuCb <= 0 ? 0 : fTienBaoLuuCb;
                }
                else
                {
                    TienBaoLuuCb = null;
                }
            }
            else if (SelectedBacTienLuong != null && SelectedBacTienLuong.TienLuong.HasValue && SelectedBacTienLuong.TienLuong != Model.TienLuongCb)
            {
                if (Model.TienLuongCb > SelectedBacTienLuong.TienLuong)
                {
                    TienBaoLuuCb = (Model.TienLuongCb.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault()) <= 0 ? 0 : Model.TienLuongCb.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault();
                    TienLuongCbCu = Model.TienLuongCb;
                }
                else if (Model.TienLuongCb == SelectedBacTienLuong.TienLuong && !string.IsNullOrEmpty(Model.NgayBaoLuuCb.ToString()))
                {
                    TienBaoLuuCb = (TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - SelectedBacTienLuong.TienNangLuong.GetValueOrDefault()) <= 0 ? 0 : (TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - SelectedBacTienLuong.TienNangLuong.GetValueOrDefault());
                    TienLuongCbCu = TienLuongCbCu;
                }
                else
                {
                    TienBaoLuuCb = null;
                    TienLuongCbCu = null;
                }
            }
            else if (SelectedBacTienLuong != null && SelectedBacTienLuong.TienLuong.HasValue && SelectedBacTienLuong.TienLuong == Model.TienLuongCb)
            {
                if (Model.TienLuongCb > SelectedBacTienLuong.TienLuong.GetValueOrDefault())
                {
                    TienBaoLuuCb = Model.TienLuongCb.GetValueOrDefault() - (SelectedBacTienLuong.TienNangLuong.GetValueOrDefault()) - SelectedBacTienLuong.TienLuong.GetValueOrDefault();
                    TienLuongCbCu = Model.TienLuongCb;
                    TienBaoLuuCb = TienBaoLuuCb < 0 ? null : TienBaoLuuCb;
                }
                else if (Model.TienLuongCb == SelectedBacTienLuong.TienLuong && !string.IsNullOrEmpty(Model.NgayBaoLuuCb.ToString()))
                {
                    TienBaoLuuCb = (TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - SelectedBacTienLuong.TienNangLuong.GetValueOrDefault()) <= 0 ? 0 : (TienLuongCbCu.GetValueOrDefault() - SelectedBacTienLuong.TienLuong.GetValueOrDefault() - SelectedBacTienLuong.TienNangLuong.GetValueOrDefault());
                    TienLuongCbCu = TienLuongCbCu;
                }
                else
                {
                    TienBaoLuuCb = null;
                    TienLuongCbCu = null;
                }
            }
            else
            {
                SetDefaultValueBaoLuu();
            }
            IsFirst = true;
            SoThangTinhBaoLuuCb = NumberUtils.NumberIsNullOrZero(TienBaoLuuCb) ? null : SoThangTinhBaoLuuCb;
            IsFirst = false;
            OnPropertyChanged(nameof(SoThangTinhBaoLuuCb));
            OnPropertyChanged(nameof(TienBaoLuuCb));
            OnPropertyChanged(nameof(TienLuongCbCu));
            OnPropertyChanged(nameof(TienLuongCb));

            if (TienBaoLuuCb.GetValueOrDefault() == 0)
            {
                SetDefaultValueBaoLuu();
            }
        }

        private void SetDefaultValueBaoLuu()
        {
            TienBaoLuuCb = null;
            TienLuongCbCu = null;
            Model.NgayBaoLuuCb = null;
            Model.SoThangTinhBaoLuuCb = null;
        }

        private void SetValueBaoLuuCvd()
        {
            if (NgayBatDauBaoLuuCvd.HasValue && SoThangTinhBaoLuuCvd.HasValue && TienLuongCvdCu.HasValue && SelectedChucVu != null && SelectedChucVu.TienLuong.HasValue)
            {
                if (NgayBatDauBaoLuuCvd.Value.Date.AddMonths(SoThangTinhBaoLuuCvd.Value) > DateTime.Now.Date)
                {
                    //var fNangLuong = Model.IsNangLuongCvd ? SelectedChucVu.TienNangLuong : 0;
                    var fNangLuong = SelectedChucVu?.TienNangLuong ?? 0;
                    Model.TienNangLuongCvd = SelectedChucVu.TienNangLuong;
                    if (TienLuongCvdCu.Value > SelectedChucVu.TienLuong.Value)
                    {
                        TienBaoLuuCvd = TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - fNangLuong;
                        TienBaoLuuCvd = TienBaoLuuCvd < 0 ? null : TienBaoLuuCvd;
                    }
                    //else
                    //{
                    //    SetDefaultValueBaoLuuCvd();
                    //}
                }
                else
                {
                    SetDefaultValueBaoLuuCvd();
                }
            }
            else if (NgayBatDauBaoLuuCvd.HasValue && TienLuongCvdCu.HasValue && SelectedChucVu != null && SelectedChucVu.TienLuong.HasValue)
            {
                if (Model.TienLuongCvd > SelectedChucVu.TienLuong)
                {
                    TienBaoLuuCvd = (Model.TienLuongCvd.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault()) <= 0 ? 0 : Model.TienLuongCvd.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault();
                }
                else if (Model.TienLuongCvd == SelectedChucVu.TienLuong && !string.IsNullOrEmpty(NgayBatDauBaoLuuCvd.ToString()))
                {
                    TienBaoLuuCvd = (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault()) <= 0 ? 0 : (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault());
                }
                else
                {
                    TienBaoLuuCvd = null;
                }
            }
            else if (SelectedChucVu != null && SelectedChucVu.TienLuong.HasValue && SelectedChucVu.TienLuong != Model.TienLuongCvd)
            {
                if (Model.TienLuongCvd > SelectedChucVu.TienLuong)
                {
                    TienBaoLuuCvd = (Model.TienLuongCvd.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault()) <= 0 ? 0 : Model.TienLuongCvd.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault();
                    TienLuongCvdCu = Model.TienLuongCvd;
                    NgayBatDauBaoLuuCvd = DateTime.Now;
                }
                else if (Model.TienLuongCvd == SelectedChucVu.TienLuong && !string.IsNullOrEmpty(NgayBatDauBaoLuuCvd.ToString()))
                {
                    TienBaoLuuCvd = (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault()) <= 0 ? 0 : (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault());
                    TienLuongCvdCu = TienLuongCvdCu;
                }
                else
                {
                    TienBaoLuuCvd = null;
                    TienLuongCvdCu = null;
                }
            }
            else if (SelectedChucVu != null && SelectedChucVu.TienLuong.HasValue && SelectedChucVu.TienLuong == Model.TienLuongCvd)
            {
                if (Model.TienLuongCvd > SelectedChucVu.TienLuong)
                {
                    TienBaoLuuCvd = Model.TienLuongCvd.GetValueOrDefault() - (SelectedChucVu.TienNangLuong.GetValueOrDefault()) - SelectedChucVu.TienLuong.GetValueOrDefault();
                    TienLuongCvdCu = Model.TienLuongCvd;
                    TienBaoLuuCvd = TienBaoLuuCvd < 0 ? null : TienBaoLuuCvd;
                }
                else if (Model.TienLuongCvd == SelectedChucVu.TienLuong.GetValueOrDefault() && !string.IsNullOrEmpty(NgayBatDauBaoLuuCvd.ToString()))
                {
                    TienBaoLuuCvd = (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault()) <= 0 ? 0 : (TienLuongCvdCu.GetValueOrDefault() - SelectedChucVu.TienLuong.GetValueOrDefault() - SelectedChucVu.TienNangLuong.GetValueOrDefault());
                    TienLuongCvdCu = TienLuongCvdCu;
                }
                else
                {
                    TienBaoLuuCvd = null;
                    TienLuongCvdCu = null;
                }
            }
            else
            {
                SetDefaultValueBaoLuuCvd();
            }
            IsFirst = true;
            SoThangTinhBaoLuuCvd = TienBaoLuuCvd.GetValueOrDefault() <= 0 ? null : SoThangTinhBaoLuuCvd;
            IsFirst = false;
            OnPropertyChanged(nameof(TienBaoLuuCvd));
            OnPropertyChanged(nameof(TienLuongCvdCu));
            OnPropertyChanged(nameof(TienLuongCvd));

            if (TienBaoLuuCvd.GetValueOrDefault() == 0)
            {
                TienLuongCvdCu = null;
                NgayBatDauBaoLuuCvd = null;
            }
        }

        private void SetDefaultValueBaoLuuCvd()
        {
            TienBaoLuuCvd = null;
            TienLuongCvdCu = null;
            NgayBatDauBaoLuuCvd = null;
            Model.SoThangTinhBaoLuuCvd = null;
        }

        private void LoadBacLuongChange()
        {
            if (SelectedCapBac != null && !_itemsBacTienLuong.IsEmpty())
            {
                List<string> lstString = new List<string>() { LoaiDoiTuong.SQ, LoaiDoiTuong.HCY, LoaiDoiTuong.HSQCS };
                if (string.IsNullOrEmpty(SelectedCapBac.MaCb)) return;
                if (!lstString.Contains(SelectedCapBac.Parent)) return;
                string sMaCapBac = SelectedCapBac.Parent.Equals(LoaiDoiTuong.HCY) ? SelectedCapBac.MaCb.Remove(0, 1).Insert(0, LoaiDoiTuong.SQ) : SelectedCapBac.MaCb;
                SelectedBacTienLuong = _itemsBacTienLuong.FirstOrDefault(x => x.MaDm.Equals(sMaCapBac));
            }
        }
        private void LoadDanhMucCapBac()
        {
            var data = _tlDmCapBacService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).OrderBy(x => x.XauNoiMa).ToList();
            var listCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(data);
            var capBacItem = listCapBac.Select(x =>
            {
                var dict = data.Select(x => x.Parent).ToHashSet();
                x.IsHangCha = dict.Contains(x.MaCb);
                x.TenCha = data.FirstOrDefault(y => y.MaCb == x.Parent)?.TenCb;
                return x;
            }).Where(x => !x.IsHangCha);
            _itemsCapBac = _mapper.Map<ObservableCollection<TlDmCapBacNq104Model>>(capBacItem);
            _chucVuView = CollectionViewSource.GetDefaultView(_itemsCapBac);
            _chucVuView.GroupDescriptions.Add(new PropertyGroupDescription("TenCha"));
            SelectedCapBac = _itemsCapBac.FirstOrDefault(x => x.MaCb.Equals(Model.MaCb104));
        }

        private void LoadTienLuongBaoLuu()
        {
            if (Model.Id != null && Model.Id != Guid.Empty)
            {
                TienBaoLuuCb = Model.TienBaoLuuCb;
                OnPropertyChanged(nameof(TienBaoLuuCb));
            }
        }

        private void LoadDanhMucChucVu()
        {
            var data = _tlDmChucVuService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).OrderBy(x => x.XauNoiMa).ThenBy(x => x.MaSo).ToList();
            var chucVus = _mapper.Map<List<TlDmChucVuNq104Model>>(data);
            ItemsChucVu = _mapper.Map<ObservableCollection<TlDmChucVuNq104Model>>(chucVus.Select(x =>
            {
                var dict = data.Select(x => x.MaCha).ToHashSet();
                x.IsHangCha = dict.Contains(x.Ma);
                x.TenCha = data.FirstOrDefault(y => y.Ma == x.MaCha)?.Ten;
                return x;
            }).Where(x => !x.IsHangCha));
            _chucVuView = CollectionViewSource.GetDefaultView(ItemsChucVu);
            _chucVuView.GroupDescriptions.Add(new PropertyGroupDescription("LoaiTen"));
            _chucVuView.GroupDescriptions.Add(new PropertyGroupDescription("TenCha"));

            SelectedChucVu = _itemsChucVu.FirstOrDefault(x => x.Ma == Model.MaCvd104);
        }

        private void LoadDanhMucTangGiam()
        {
            try
            {
                var data = _qsMucLucService.FindAll().Where(x => x.BHangCha == false && x.SHienThi != "2" && (x.SKyHieu.StartsWith("2") || x.SKyHieu.StartsWith("3") || x.SKyHieu.StartsWith("0")) && x.INamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == ItrangThaiStatus.ON).ToList();
                _tangGiamItems = new ObservableCollection<QsMucLucModel>();
                data = data.OrderBy(x => x.SKyHieu).ToList();
                _tangGiamItems = _mapper.Map<ObservableCollection<QsMucLucModel>>(data);
                if (Model.MaTangGiam != null)
                {
                    SelectedTangGiamItems = TangGiamItems.FirstOrDefault(x => x.SKyHieu == Model.MaTangGiam);
                }
                OnPropertyChanged(nameof(TangGiamItems));
                OnPropertyChanged(nameof(SelectedTangGiamItems));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadGender()
        {
            GenderData = new List<ComboboxItem>()
            {
                new ComboboxItem(Gender.NAM, Gender.NAM),
                new ComboboxItem(Gender.NU, Gender.NU)
            };
            SelectedGender = ViewState == FormViewState.ADD || (Model.IsNam.HasValue && Model.IsNam.Value)
                ? GenderData.ElementAt(0)
                : GenderData.ElementAt(1);
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _donviData = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
            if (SelectedDonVi != null)
            {
                SelectedDonVi = ViewState != FormViewState.ADD ?
                    _donviData.FirstOrDefault(x => x.MaDonVi == Model.Parent) :
                    _donviData.FirstOrDefault(x => x.MaDonVi == SelectedDonVi.MaDonVi);
            }
            OnPropertyChanged(nameof(DonViItems));
        }

        private void DetailPhuCap_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceNq104Model.GiaTri)
                || args.PropertyName == nameof(AllowenceNq104Model.HuongPCSN)
                || args.PropertyName == nameof(AllowenceNq104Model.DateStart)
                || args.PropertyName == nameof(AllowenceNq104Model.ISoThangHuong))
            {
                AllowenceNq104Model item = sender as AllowenceNq104Model;
                item.IsModified = true;

                if (item.MaPhuCap == PhuCap.LHT_HS)
                {
                    _heSoLuong = item.GiaTri;
                    OnPropertyChanged(nameof(HeSoLuong));
                }

                if (item.MaPhuCap == PhuCap.GTPT_SN)
                {
                    _soNguoiPhuThuoc = item.GiaTri;
                    OnPropertyChanged(nameof(SoNguoiPhuThuoc));
                }

                var lstHsTruyLinh = new List<string>() { PhuCap.LHT_HS, PhuCap.PCCV_HS, PhuCap.PCTHUHUT_HS, PhuCap.PCCOV_HS, PhuCap.PCCU_HS };
                if (ViewState == FormViewState.ADD)
                {
                    if (lstHsTruyLinh.Contains(item.MaPhuCap))
                    {
                        var phuCapTruyLinh = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == string.Format("{0}{1}", item.MaPhuCap, "_CU"));
                        if (phuCapTruyLinh != null)
                        {
                            phuCapTruyLinh.GiaTri = item.GiaTri;
                            phuCapTruyLinh.IsModified = true;
                        }
                    }
                }

                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private void DetailPhuCapHuong_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(AllowenceNq104Model.DateStart)
                || args.PropertyName == nameof(AllowenceNq104Model.ISoThangHuong))
            {
                AllowenceNq104Model item = (AllowenceNq104Model)sender;
                var phuCap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(item.MaPhuCap));
                if (phuCap != null)
                {
                    phuCap.ISoThangHuong = item.ISoThangHuong;
                    phuCap.DateStart = item.DateStart;
                    phuCap.IsModified = true;
                    OnPropertyChanged(nameof(ItemsAllowence));
                }
            }
        }

        private void DataModelChange(TypeChangeCustom type, decimal? value)
        {
            AllowenceNq104Model temp = new AllowenceNq104Model();
            AllowenceNq104Model tempOld = new AllowenceNq104Model();
            if (ItemsAllowence.IsEmpty()) return;
            switch (type)
            {
                case TypeChangeCustom.TienLuongCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLCB_TT"));
                    if (temp != null)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienNangLuongCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienLuongBaoLuuCb:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value < 0 ? 0 : value;
                    break;
                case TypeChangeCustom.TienNangLuongCvd:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_CC_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;
                case TypeChangeCustom.TienLuongBaoLuuCvd:
                    temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_CC_TT"));
                    tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_TT_CU"));
                    if (tempOld != null && !IsFirst)
                        tempOld.GiaTri = temp?.GiaTri;
                    if (temp != null && !IsFirst)
                        temp.GiaTri = value;
                    break;

                default:
                    break;

            }
            OnPropertyChanged(nameof(ItemsAllowence));

        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            CadresNq104Model item = (CadresNq104Model)sender;

            if (args.PropertyName == nameof(CadresNq104Model.TienLuongCb))
            {
                var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLCB_TT"));
                if (temp != null)
                    temp.GiaTri = item.TienLuongCb;
            }
            else if (args.PropertyName == nameof(CadresNq104Model.TienNangLuongCb))
            {
                var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT"));
                var tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT_CU"));
                //if (IsNangLuongCb)
                //{
                //    if (tempOld != null && !IsFirst)
                //        tempOld.GiaTri = temp?.GiaTri;
                //    if (temp != null && !IsFirst)
                //        temp.GiaTri = item.TienNangLuongCb;
                //}
                //else
                //{
                //    if (tempOld != null && !IsFirst)
                //        tempOld.GiaTri = 0;
                //    if (temp != null && !IsFirst)
                //        temp.GiaTri = 0;
                //}
                if (tempOld != null && !IsFirst)
                    tempOld.GiaTri = temp?.GiaTri;
                if (temp != null && !IsFirst)
                    temp.GiaTri = item.TienNangLuongCb;
            }
            else if (args.PropertyName == nameof(CadresNq104Model.TienNangLuongCvd))
            {
                var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_CC_TT"));
                var tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TNLCV_CD_TT_CU"));

                //if (IsNangLuongCvd)
                //{
                //    if (tempOld != null && !IsFirst)
                //        tempOld.GiaTri = temp?.GiaTri;
                //    if (temp != null && !IsFirst)
                //        temp.GiaTri = item.TienNangLuongCvd;
                //}
                //else
                //{
                //    if (tempOld != null && !IsFirst)
                //        tempOld.GiaTri = 0;
                //    if (temp != null && !IsFirst)
                //        temp.GiaTri = 0;
                //}
                if (tempOld != null && !IsFirst)
                    tempOld.GiaTri = temp?.GiaTri;
                if (temp != null && !IsFirst)
                    temp.GiaTri = item.TienNangLuongCvd;
            }
            else if (args.PropertyName == nameof(CadresNq104Model.TienBaoLuuCb))
            {
                var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT"));
                var tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT_CU"));
                if (tempOld != null && !IsFirst)
                    tempOld.GiaTri = temp?.GiaTri;
                if (temp != null && !IsFirst)
                    temp.GiaTri = item.TienBaoLuuCb;
            }
            else if (args.PropertyName == nameof(CadresNq104Model.TienBaoLuuCvd))
            {
                var temp = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_CC_TT"));
                var tempOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("TLBLCV_CD_TT_CU"));
                if (tempOld != null && !IsFirst)
                    tempOld.GiaTri = temp?.GiaTri;
                if (temp != null && !IsFirst)
                    temp.GiaTri = item.TienBaoLuuCvd;
            }

            if (args.PropertyName == nameof(CadresNq104Model.Tm))
            {
                var Tm = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.TM));
                if ((bool)item.Tm && Tm != null)
                {
                    Tm.GiaTri = 0;
                }
                else if ((bool)!item.Tm && Tm != null)
                {
                    Tm.GiaTri = 1;
                }
            }
            if (args.PropertyName == nameof(CadresNq104Model.BHTN))
            {
                var Nop_BHTN = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Nop_BHTN));
                if ((bool)item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 1;
                }
                else if ((bool)!item.BHTN && Nop_BHTN != null)
                {
                    Nop_BHTN.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(CadresNq104Model.PCCV))
            {
                var Huong_PCCOV = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.Huong_PCCOV));
                if ((bool)item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 1;
                }
                else if ((bool)!item.PCCV && Huong_PCCOV != null)
                {
                    Huong_PCCOV.GiaTri = 0;
                }
            }
            if (args.PropertyName == nameof(CadresNq104Model.SoTaiKhoan))
            {
                if (!string.IsNullOrEmpty(item.SoTaiKhoan))
                {
                    item.Tm = true;
                }
                else if (string.IsNullOrEmpty(item.SoTaiKhoan))
                {
                    item.Tm = false;
                }
            }
            if (args.PropertyName == nameof(CadresNq104Model.bKhongTinhNTN))
            {
                if (item.bKhongTinhNTN != null)
                    TinhNamThamNien();
            }
            //if (args.PropertyName == nameof(CadresModel.NgayTruyLinh))
            //{
            //    if (item.NgayTruyLinh != null)
            //    {
            //        var pcTtl = ItemsAllowence.Where(x => x.MaPhuCap.Contains("TTL"));
            //        var ngayTruyLinh = (DateTime)item.NgayTruyLinh;
            //        var monthDiff = ((int)Model.Nam - ngayTruyLinh.Year) * 12 + (int)Model.Thang - ngayTruyLinh.Month + 1;

            //        foreach (var item1 in pcTtl)
            //        {
            //            //var dateTimeNow = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
            //            if (ngayTruyLinh.Day >= 15)
            //            {
            //                var value = monthDiff - 0.5;
            //                item1.GiaTri = (decimal?)value;
            //                item1.IsModified = true;
            //            }
            //            else
            //            {
            //                item1.GiaTri = monthDiff;
            //                item1.IsModified = true;
            //            }
            //        }
            //    }
            //}
            OnPropertyChanged(nameof(ItemsAllowence));
        }

        private void LoadHsl()
        {
            var pc = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals(PhuCap.LHT_HS));
            if (HeSoLuong <= 0)
            {
                HeSoLuong = 0;
            }
            pc.GiaTri = HeSoLuong;
            if (_selectedCapBac != null)
            {
                Model.MaCb = _selectedCapBac.MaCb;
                LoadTangQuanHam(Model.MaCb);
            }
        }

        //private void LoadHeSoLuong()
        //{
        //    if (ItemsAllowence != null)
        //    {
        //        var phucap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.LHT_HS);
        //        if (_selectedCapBac != null && phucap != null)
        //        {
        //            phucap.GiaTri = _selectedCapBac.LhtHs;
        //            phucap.IsModified = false;
        //            Model.HeSoLuong = _selectedCapBac.LhtHs;
        //        }
        //        var phucapTiLeHuong = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == PhuCap.TILE_HUONG);
        //        if (_selectedCapBac != null && phucapTiLeHuong != null)
        //        {
        //            phucapTiLeHuong.GiaTri = _selectedCapBac.TiLeHuong;
        //            phucapTiLeHuong.IsModified = false;
        //        }
        //        OnPropertyChanged(nameof(ItemsAllowence));
        //        OnPropertyChanged(nameof(CadresModel));
        //    }
        //    if (_selectedCapBac.MaCb.StartsWith("1") || _selectedCapBac.MaCb.StartsWith("4"))
        //    {
        //        SelectedNhom = null;
        //        OnPropertyChanged(nameof(SelectedNhom));
        //    }
        //    if (_selectedCapBac.MaCb.StartsWith("0"))
        //    {
        //        Model.PCCV = true;
        //    }
        //}

        private void LoadEnabled()
        {
            if (ViewState == FormViewState.DETAIL && LstCanBo.Count() > 1)
            {
                if (Model.MaCanBo == _lstCanBo.FirstOrDefault().MaCanBo)
                {
                    BackEnabled = false;
                    NextEnabled = true;
                }
                else if (Model.MaCanBo == _lstCanBo.LastOrDefault().MaCanBo)
                {
                    BackEnabled = true;
                    NextEnabled = false;
                }
                else
                {
                    BackEnabled = true;
                    NextEnabled = true;
                }
            }
        }

        private void LoadHeSoChucVu()
        {
            if (ItemsAllowence != null)
            {
                var phuCap = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap == "TLCV_CD_CC_TT");

                if (_selectedChucVu != null && phuCap != null)
                {
                    phuCap.GiaTri = _selectedChucVu.TienLuong;
                    phuCap.IsModified = false;
                }
                else if (_selectedChucVu == null && phuCap != null)
                {
                    phuCap.GiaTri = 0;
                }

                //Chuc Vu
                var cvLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCV_TT"));
                var cvLNLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCV_TT"));
                var cvLLBLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCV_TT"));

                //Chuc Vu Cu
                var cvLCVTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCV_TT_CU"));
                var cvLNLCVTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCV_TT_CU"));
                var cvLLBLCVTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCV_TT_CU"));

                //Chuc Danh
                var cdLCDTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCD_TT"));
                var cdNLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCD_TT"));
                var cdNLLBLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCD_TT"));
                //Chuc Danh Cu
                var cdLCDTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCD_TT_CU"));
                var cdNLCDHTTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCD_TT_CU"));
                var cdNLLBLCDHTTTOld = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCD_TT_CU"));

                //Chuc Vu
                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLCVTT != null)
                {
                    cvLCVTT.GiaTri = _selectedChucVu.TienLuong;
                    cvLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cvLCVTT.GiaTri = 0;
                    cvLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLNLCVTT != null)
                {
                    cvLNLCVTT.GiaTri = TienNangLuongCvd;
                    cvLNLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cvLNLCVTT.GiaTri = 0;
                    cvLNLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLLBLCVTT != null)
                {
                    cvLLBLCVTT.GiaTri = TienBaoLuuCvd;
                    cvLLBLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cvLLBLCVTT.GiaTri = 0;
                    cvLLBLCVTT.IsModified = true;
                    if (cdLCDTT != null) cdLCDTT.GiaTri = 0;
                    if (cdNLCDHTTT != null) cdNLCDHTTT.GiaTri = 0;
                    if (cdNLLBLCDHTTT != null) cdNLLBLCDHTTT.GiaTri = 0;
                }

                ////Chuc Vu Cu
                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLCVTTOld != null)
                {
                    cvLCVTTOld.GiaTri = _selectedChucVu.TienLuong;
                    cvLCVTTOld.IsModified = true;
                    if (cdLCDTTOld != null) cdLCDTTOld.GiaTri = 0;
                    if (cdNLCDHTTTOld != null) cdNLCDHTTTOld.GiaTri = 0;
                    if (cdNLLBLCDHTTTOld != null) cdNLLBLCDHTTTOld.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLNLCVTTOld != null)
                {
                    cvLNLCVTTOld.GiaTri = TienNangLuongCvd;
                    cvLNLCVTTOld.IsModified = true;
                    if (cdLCDTTOld != null) cdLCDTTOld.GiaTri = 0;
                    if (cdNLCDHTTTOld != null) cdNLCDHTTTOld.GiaTri = 0;
                    if (cdNLLBLCDHTTTOld != null) cdNLLBLCDHTTTOld.GiaTri = 0;
                }


                if (_selectedChucVu != null && _selectedChucVu.Loai == false && cvLLBLCVTTOld != null)
                {
                    cvLLBLCVTTOld.GiaTri = TienBaoLuuCvd;
                    cvLLBLCVTTOld.IsModified = true;
                    if (cdLCDTTOld != null) cdLCDTTOld.GiaTri = 0;
                    if (cdNLCDHTTTOld != null) cdNLCDHTTTOld.GiaTri = 0;
                    if (cdNLLBLCDHTTTOld != null) cdNLLBLCDHTTTOld.GiaTri = 0;
                }

                //Chuc Danh
                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdLCDTT != null)
                {
                    cdLCDTT.GiaTri = _selectedChucVu.TienLuong;
                    cdLCDTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cdLCDTT.GiaTri = 0;
                    cdLCDTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdNLCDHTTT != null)
                {
                    cdNLCDHTTT.GiaTri = TienNangLuongCvd;
                    cdNLCDHTTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cdNLCDHTTT.GiaTri = 0;
                    cdNLCDHTTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdNLLBLCDHTTT != null)
                {
                    cdNLLBLCDHTTT.GiaTri = TienBaoLuuCvd;
                    cdNLLBLCDHTTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }
                else if (_selectedChucVu == null)
                {
                    cdNLLBLCDHTTT.GiaTri = 0;
                    cdNLLBLCDHTTT.IsModified = true;
                    if (cvLCVTT != null) cvLCVTT.GiaTri = 0;
                    if (cvLNLCVTT != null) cvLNLCVTT.GiaTri = 0;
                    if (cvLLBLCVTT != null) cvLLBLCVTT.GiaTri = 0;
                }

                //Chuc Danh Cu
                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdLCDTTOld != null)
                {
                    cdLCDTTOld.GiaTri = _selectedChucVu.TienLuong;
                    cdLCDTTOld.IsModified = true;
                    if (cvLCVTTOld != null) cvLCVTTOld.GiaTri = 0;
                    if (cvLNLCVTTOld != null) cvLNLCVTTOld.GiaTri = 0;
                    if (cvLLBLCVTTOld != null) cvLLBLCVTTOld.GiaTri = 0;
                }
                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdNLCDHTTTOld != null)
                {
                    cdNLCDHTTTOld.GiaTri = TienNangLuongCvd;
                    cdNLCDHTTTOld.IsModified = true;
                    if (cvLCVTTOld != null) cvLCVTTOld.GiaTri = 0;
                    if (cvLNLCVTTOld != null) cvLNLCVTTOld.GiaTri = 0;
                    if (cvLLBLCVTTOld != null) cvLLBLCVTTOld.GiaTri = 0;
                }

                if (_selectedChucVu != null && _selectedChucVu.Loai == true && cdNLLBLCDHTTTOld != null)
                {
                    cdNLLBLCDHTTTOld.GiaTri = TienBaoLuuCvd;
                    cdNLLBLCDHTTTOld.IsModified = true;
                    if (cvLCVTTOld != null) cvLCVTTOld.GiaTri = 0;
                    if (cvLNLCVTTOld != null) cvLNLCVTTOld.GiaTri = 0;
                    if (cvLLBLCVTTOld != null) cvLLBLCVTTOld.GiaTri = 0;
                }

                OnPropertyChanged(nameof(ItemsAllowence));
            }
        }

        private void TinhNamThamNien()
        {
            if (Model != null)
            {
                if (Model.bKhongTinhNTN.HasValue && !Model.bKhongTinhNTN.Value)
                {
                    try
                    {
                        NamThamNien = DateUtils.TinhNamThamNien(NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangThamNienNghe, (int)Model.Thang, (int)Model.Nam);

                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        NamThamNien = 0;
                    }
                    if (NamThamNien < 0)
                    {
                        NamThamNien = 0;
                    }
                }
                else
                    NamThamNien = ThangThamNienNghe / 12;
                OnPropertyChanged(nameof(NamThamNien));
            }
        }

        private int TinhThangHuongTcxn(DateTime? ngayNn, DateTime? ngayXn)
        {
            var ngayNhapNgu = (DateTime)ngayNn;
            var ngayXuatNgu = (DateTime)ngayXn;
            var monthDiff = (ngayXuatNgu.Year - ngayNhapNgu.Year) * 12 + ngayXuatNgu.Month - ngayNhapNgu.Month + 1;
            var phanNguyen = monthDiff / 12;
            var phanDu = monthDiff % 12;

            int thangDu = 0;
            if (1 <= phanDu && phanDu <= 6)
            {
                thangDu = 1;
            }
            else if (7 <= phanDu && phanDu <= 12)
            {
                thangDu = 2;
            }

            return (phanNguyen * 2 + thangDu);
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();
            DateTime ngay = new DateTime((int)Model.Nam, (int)Model.Thang, 1);
            if (string.IsNullOrEmpty(Model.TenCanBo))
            {
                messages.Add(string.Format(Resources.CadresNameNull));
                goto End;
            }
            if (SelectedCapBac == null)
            {
                messages.Add(string.Format(Resources.CadresRankNull));
                goto End;
            }
            if (SelectedGender == null)
            {
                messages.Add(string.Format(Resources.GenderNull));
                goto End;
            }
            if (SelectedDonVi == null)
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
            if (NgayNhapNgu > ngay)
            {
                messages.Add(string.Format(Resources.NgayNhapNgu));
                goto End;
            }
            if ((Model.KhongLuong ?? false))
            {
                if (SelectedTangGiamItems == null)
                {
                    messages.Add("Nếu không tính lương thì phải nhập mã tăng giảm !");
                    goto End;
                }
            }
            var ngayThangCanBo = new DateTime((int)Model.Nam, (int)Model.Thang, DateTime.DaysInMonth((int)Model.Nam, (int)Model.Thang));
            if (Model.NgayTruyLinh != null && Model.NgayTruyLinh > ngayThangCanBo)
            {
                messages.Add(string.Format(Resources.PursuitDayNotValid));
                goto End;
            }
        //foreach (var item in DataCanBoCheDoBHXH)
        //{
        //    if (item.FSoNgayHuongBHXH > 24)
        //    {
        //        messages.Add(string.Format(Resources.ValidateDayOfApplied, item.STenCheDo));
        //    }
        //}
        //foreach (var item in ItemsAllowence)
        //{
        //    if (item.DateStart != null && item.DateStart > ngayThangCanBo)
        //    {
        //        messages.Add(string.Format(Resources.DateStartNotValid, item.MaPhuCap));
        //        goto End;
        //    }
        //    if (item.ISoThangHuong != null && item.ISoThangHuong < 0)
        //    {
        //        messages.Add(string.Format(Resources.SoThangHuongInvalid, item.MaPhuCap));
        //        goto End;
        //    }
        //    if (item.DateStart != null && item.DateStart <= ngayThangCanBo && item.ISoThangHuong != null && item.ISoThangHuong > 0)
        //    {
        //        DateTime ngayBatDau = (DateTime)item.DateStart;
        //        if (ngayBatDau.AddMonths((int)item.ISoThangHuong) > ngayThangCanBo)
        //        {
        //            messages.Add(string.Format(Resources.DateStartAndSoThangHuongInvalid, item.MaPhuCap));
        //            goto End;
        //        }
        //    }
        //}
        End:
            return string.Join(Environment.NewLine, messages);
        }

        private void OnFirst()
        {
            Model = LstCanBo.First();
            Init();
        }

        private void OnPrevious()
        {
            var currentCanBo = LstCanBo.Where(x => x.MaCanBo == Model.MaCanBo).FirstOrDefault();
            var index = LstCanBo.IndexOf(currentCanBo);
            Model = LstCanBo[index - 1];
            Init();
        }

        private void OnNext()
        {
            var currentCanBo = LstCanBo.Where(x => x.MaCanBo == Model.MaCanBo).FirstOrDefault();
            var index = LstCanBo.IndexOf(currentCanBo);
            Model = LstCanBo[index + 1];
            Init();
        }

        private void OnLast()
        {
            Model = LstCanBo.Last();
            Init();
        }

        private bool ListPhuCapFilter(object obj)
        {
            bool result = true;
            var item = (AllowenceNq104Model)obj;

            if (!string.IsNullOrEmpty(SearchMaPhuCap))
            {
                result = result && !string.IsNullOrEmpty(item.MaPhuCap) && item.MaPhuCap.ToLower().Contains(SearchMaPhuCap.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenPhuCap))
            {
                result = result && !string.IsNullOrEmpty(item.TenPhuCap) && item.TenPhuCap.ToLower().Contains(SearchTenPhuCap.ToLower());
            }

            return result;
        }

        private bool ListCheDoFilter(object obj)
        {
            bool result = true;
            var item = (TlCanBoCheDoBHXHModel)obj;

            if (!string.IsNullOrEmpty(SearchMaCheDo))
            {
                result = result && !string.IsNullOrEmpty(item.SMaCheDo) && item.SMaCheDo.ToLower().Contains(SearchMaCheDo.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenCheDo))
            {
                result = result && !string.IsNullOrEmpty(item.STenCheDo) && item.STenCheDo.ToLower().Contains(SearchTenCheDo.ToLower());
            }

            return result;
        }

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            if (IsFirst)
            {
                return false;
            }
            var item = (CadresNq104Model)obj;
            if (!string.IsNullOrEmpty(SearchSoSoLuong))
            {
                result = result && !string.IsNullOrEmpty(item.MaCanBo) && item.MaCanBo.ToLower().Contains(SearchSoSoLuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchDoiTuong))
            {
                result = result && !string.IsNullOrEmpty(item.TenCanBo) && item.TenCanBo.ToLower().Contains(SearchDoiTuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchCapBac))
            {
                result = result && !string.IsNullOrEmpty(item.MaCb) && item.MaCb.ToLower().Contains(SearchCapBac.ToLower());
            }
            return result;
        }

        private void OnResetFilter()
        {
            SearchMaPhuCap = string.Empty;
            SearchTenPhuCap = string.Empty;

            _phuCapView.Refresh();
        }

        private void OnUpdate()
        {
            if (Model.IsLock == true)
            {
                ViewState = FormViewState.DETAIL;
                MessageBoxHelper.Info(Resources.MsgNotEditCadre);
            }
            else
            {
                ViewState = FormViewState.UPDATE;
            }
            NextEnabled = false;
            BackEnabled = false;
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            ClosePopup?.Invoke(Model, new EventArgs());
        }

        public void OnRefresh()
        {
            Init();
        }

        private void LoadTangQuanHam(string maCapBac)
        {
            if (maCapBac.StartsWith("1") || maCapBac.StartsWith("0"))
            {
                var cbKeHoach = _tlDmCapBacKeHoachService.FindByMaCb(maCapBac);
                if (cbKeHoach != null)
                {
                    SelectedCapBacKeHoach = ItemsCapBacKeHoach.FirstOrDefault(x => x.Id.Equals(cbKeHoach.Id));
                    SelectedHslKeHoach = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslKeHoach);
                    SelectedHslKeHoachTran = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslTran);
                    Model.ThoiHanTangCb = cbKeHoach.ThoiHanTang;
                }
                else
                {
                    SelectedHslKeHoach = null;
                    SelectedHslKeHoachTran = null;
                }
            }
            else
            {
                var cbKeHoach = _tlDmCapBacKeHoachService.FindByMaCbAndHslAndNhom(maCapBac, HeSoLuong, SelectedNhom == null ? string.Empty : SelectedNhom.ValueItem);
                if (cbKeHoach != null)
                {
                    SelectedCapBacKeHoach = ItemsCapBacKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.Id);
                    SelectedHslKeHoach = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslKeHoach);
                    SelectedHslKeHoachTran = ItemsHslKeHoach.FirstOrDefault(x => x.Id == cbKeHoach.IdHslTran);
                    Model.ThoiHanTangCb = cbKeHoach.ThoiHanTang;
                }
                else
                {
                    SelectedHslKeHoach = null;
                    SelectedHslKeHoachTran = null;
                }
            }

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedHslKeHoach));
            OnPropertyChanged(nameof(SelectedHslKeHoachTran));
            OnPropertyChanged(nameof(SelectedCapBacKeHoach));
            OnPropertyChanged(nameof(SelectedCapBacKeHoachTran));
            OnPropertyChanged(nameof(ItemsCapBacKeHoach));
        }

        private void OnCloseSearchDatagrid()
        {
            IsPopupOpen = false;
            OnPropertyChanged(nameof(IsPopupOpen));
        }

        private void OnSearchCanBo()
        {
            _canBoSearchView.Refresh();
            IsPopupOpen = true;
        }

        private void OnChooseCanBo()
        {
            Model = SelectedCanBo;
            NgayNhapNgu = Model.NgayNn;
            NgayXuatNgu = Model.NgayXn;
            NgayTaiNgu = Model.NgayTn;
            SelectedAllowence = null;
            LoadDonVi();
            LoadEnabled();
            LoadData();
            LoadDanhMucCapBac();
            LoadListNhom();
            LoadCapBacKeHoach();
            LoadHslKeHoach();
            LoadDanhMucChucVu();
            LoadDanhMucTangGiam();
            LoadGender();
            LoadDonVi();
            LoadCanBo();
            FindCongThucLuong();
            SearchCapBac = string.Empty;
            SearchDoiTuong = string.Empty;
            SearchSoSoLuong = string.Empty;
            IsPopupOpen = false;
        }

        public void SaveBangLuong(CadresNq104Model cadres)
        {

            try
            {
                TlDsCapNhapBangLuongNq104 tlDsCapNhapBangLuong = new TlDsCapNhapBangLuongNq104();
                TlDSCapNhapBangLuongNq104Model tlDsCapNhapBangLuongModel = new TlDSCapNhapBangLuongNq104Model();
                tlDsCapNhapBangLuongModel.TenDsCnbluong = string.Format("Bảng lương {0} - {1} - {2}", cadres.Thang, cadres.Nam, SelectedDonVi.TenDonVi);
                tlDsCapNhapBangLuongModel.MaCachTl = CachTinhLuong.CACH0;
                tlDsCapNhapBangLuongModel.Status = true;
                tlDsCapNhapBangLuongModel.KhoaBangLuong = false;
                tlDsCapNhapBangLuongModel.NgayTaoBL = DateTime.Now;
                tlDsCapNhapBangLuongModel.MaCbo = cadres.Parent;
                DateTime firstDayOfMonth = new DateTime((int)cadres.Nam, (int)cadres.Thang, 1);
                DateTime lastDayOfMonth = new DateTime((int)cadres.Nam, (int)cadres.Thang, 1).AddMonths(1).AddDays(-1);
                tlDsCapNhapBangLuongModel.TuNgay = firstDayOfMonth;
                tlDsCapNhapBangLuongModel.DenNgay = lastDayOfMonth;
                tlDsCapNhapBangLuongModel.Thang = cadres.Thang;
                tlDsCapNhapBangLuongModel.Nam = cadres.Nam;
                tlDsCapNhapBangLuongModel.NguoiTao = _sessionService.Current.Principal;
                tlDsCapNhapBangLuongModel.Note = CachTinhLuong.NOTE;
                _mapper.Map(tlDsCapNhapBangLuongModel, tlDsCapNhapBangLuong);
                _tlDsCapNhapBangLuongService.Add(tlDsCapNhapBangLuong);
                var id = tlDsCapNhapBangLuong.Id;

                // lưu bảng lương tháng chi tiết
                var dataCanBo = _tlBangLuongThangService.FindCbLuong(Model.Thang, Model.Nam, Model.Parent).Where(x => x.IsDelete == true).ToList();
                IEnumerable<CadresNq104Model> cadresModels = _mapper.Map<ObservableCollection<CadresNq104Model>>(dataCanBo);
                ObservableCollection<TlBangLuongThangNq104Model> tlBangLuongThangModels = new ObservableCollection<TlBangLuongThangNq104Model>();

                foreach (var item in cadresModels)
                {
                    TlBangLuongThangNq104Model tlBangLuongThangModel = new TlBangLuongThangNq104Model();
                    var listPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                    var listPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapNq104Model>>(listPhuCap).Select(x =>
                    {
                        x.GiaTri = (x.MaPhuCap == PhuCap.NTN && x.GiaTri < 5) ? x.GiaTri = 0 : x.GiaTri;
                        return x;
                    }).ToList();
                    if (listPhuCapModel != null && listPhuCapModel.Count > 0)
                    {

                        foreach (var congThucLuong in DsCongThucLuong)
                        {
                            congThucLuong.Value = 0;
                            congThucLuong.IsCalculated = false;
                        }
                        foreach (var item1 in listPhuCap)
                        {
                            var bangLuong = CreateBangLuongThangModel(id, item, item1.MaPhuCap, item1.GiaTri);
                            tlBangLuongThangModels.Add(bangLuong);
                        }
                        Dictionary<string, decimal> results = new Dictionary<string, decimal>();
                        foreach (var cachTinhLuong in DsCongThucLuong)
                        {
                            results.Add(cachTinhLuong.MaCot, TinhLuong(listPhuCapModel, cachTinhLuong));
                        }

                        var keys = results.Keys;
                        foreach (var key in keys.ToList())
                        {
                            string value = results[key].ToString("N4");
                            var bangLuong = tlBangLuongThangModels.Where(x => x.MaPhuCap == key && x.MaCbo == item.MaCanBo).FirstOrDefault();
                            bangLuong.GiaTri = Decimal.Parse(value);
                        }
                    }
                }
                IEnumerable<TlBangLuongThangNq104> tlBangLuongThangs = _mapper.Map<ObservableCollection<TlBangLuongThangNq104>>(tlBangLuongThangModels);
                _tlBangLuongThangService.Add(tlBangLuongThangs);

                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<TlDSCapNhapBangLuongNq104Model>(tlDsCapNhapBangLuong));

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void FindCongThucLuong()
        {
            var data = _tlDmCachTinhLuongChuanService.FindAll(x => x.Nam == _sessionService.Current.YearOfWork).ToList();
            DsCongThucLuong = _mapper.Map<List<TlCachTinhLuongNq104Model>>(data);

        }

        private TlCachTinhLuongNq104Model CheckExitCongThuc(string congThuc)
        {
            return DsCongThucLuong.Where(x => x.MaCot == congThuc).FirstOrDefault();
        }

        private TlBangLuongThangNq104Model CreateBangLuongThangModel(Guid id, CadresNq104Model cadresModel, string maPhuCap,
            decimal? giaTri)
        {
            TlBangLuongThangNq104Model model = new TlBangLuongThangNq104Model();
            model.Parent = id;
            model.MaCachTl = CachTinhLuong.CACH0;
            model.Thang = cadresModel.Thang;
            model.Nam = cadresModel.Nam;
            model.MaCbo = cadresModel.MaCanBo;
            model.MaCb = cadresModel.MaCb;
            model.TenCbo = cadresModel.TenCanBo;
            model.MaDonVi = cadresModel.Parent;
            model.MaPhuCap = maPhuCap;
            model.MaCbo = cadresModel.MaCanBo;
            model.GiaTri = giaTri;
            model.MaHieuCanBo = cadresModel.MaHieuCanBo;
            return model;
        }

        private decimal TinhLuong(List<TlCanBoPhuCapNq104Model> tlBangLuongThangModel, TlCachTinhLuongNq104Model congThucLuong)
        {
            if (congThucLuong.IsCalculated == true)
            {
                return congThucLuong.Value;
            }
            else
            {
                var data = new Dictionary<string, object>();
                List<string> phuCap = congThucLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (congThucLuong.CongThuc != PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    foreach (var congThuc in phuCap)
                    {
                        var congThucExit = CheckExitCongThuc(congThuc);
                        if (congThucExit != null)
                        {
                            data.Add(congThuc, TinhLuong(tlBangLuongThangModel, congThucExit));
                        }
                        else
                        {
                            var property = tlBangLuongThangModel.Where(x => x.MaPhuCap == congThuc).FirstOrDefault();
                            if (property != null)
                            {
                                data.Add(congThuc, property.GiaTri ?? 0);
                            }
                        }
                    }
                }
                else if (congThucLuong.CongThuc == PhuCap.THUETNCN_TT_CONGTHUC)
                {
                    var luongThueTNCN = CheckExitCongThuc(PhuCap.LUONGTHUE_TT);
                    if (luongThueTNCN.Value == 0)
                    {
                        data.Add(PhuCap.LUONGTHUE_TT, TinhLuong(tlBangLuongThangModel, luongThueTNCN));
                    }
                    else
                    {
                        var tien = Convert.ToString(ThueTN(luongThueTNCN.Value));
                        data.Add(PhuCap.THUETNCN_TT_CONGTHUC, tien);
                    }
                }
                congThucLuong.IsCalculated = true;
                if (data.Count() > 0)
                {
                    var result = EvalExtensions.Execute(congThucLuong.CongThuc, data);
                    congThucLuong.Value = Decimal.Parse(result.ToString());
                    return congThucLuong.Value;
                }
            }
            return 0;
        }

        private decimal ThueTN(decimal luongThuThue)
        {
            var data = _tlBangLuongThangService.FindThue().OrderBy(x => x.ThuNhapTu).ToList();
            decimal tienThue = 0;
            decimal t = luongThuThue;
            var DmThuThue = _mapper.Map<List<TlDmThueThuNhapCaNhanModel>>(data);
            if (luongThuThue <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in DmThuThue)
                {
                    if (luongThuThue >= (decimal)item.ThuNhapDen && item.ThuNhapDen != 0)
                    {
                        tienThue += ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                        t = t - ((decimal)item.ThuNhapDen - (decimal)item.ThuNhapTu);
                    }
                    else if (item.ThuNhapDen == 0)
                    {
                        tienThue += (luongThuThue - (decimal)item.ThuNhapTu) * ((decimal)item.ThueXuat / 100);
                    }
                    else if (luongThuThue < (decimal)item.ThuNhapDen)
                    {
                        decimal tien = t * ((decimal)item.ThueXuat / 100);
                        tienThue += tien;
                        return tienThue;
                    }
                }
                return tienThue;
            }
        }

        private void DetailCanBoCheDoBHXHModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlCanBoCheDoBHXHModel objectSender = (TlCanBoCheDoBHXHModel)sender;
            if (args.PropertyName == nameof(TlCanBoCheDoBHXHModel.SSoQuyetDinh)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DDenNgay)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DNgayQuyetDinh)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FSoTien)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.IThangLuongCanCuDong)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FGiaTriCanCu)
                || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.FSoNgayHuongBHXH))
            {
                List<TlCanBoCheDoBHXHModel> listCheDoBHXH = DataCanBoCheDoBHXH.Where(x => !x.IsDeleted).ToList();
                OnPropertyChanged(nameof(DataCanBoCheDoBHXH));
                OnPropertyChanged(nameof(Model));
                _isModifiedBHXH = true;
            }
            if (args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DTuNgay) || args.PropertyName == nameof(TlCanBoCheDoBHXHModel.DDenNgay))
            {
                objectSender.FSoNgayHuongBHXH = CountDayOfBHXH(objectSender);
            }
            objectSender.IsModified = true;
            IsReadOnlyBHXH = objectSender.IsHangCha;
        }

        private void LoadDataCheDoBHXH()
        {
            var maCanBo = Model.MaCanBo;
            var maHieuCanBo = Model.MaHieuCanBo;
            var thang = Model.Thang;
            var nam = Model.Nam;
            var listCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetCanBoCheDoIndex(maCanBo).ToList();
            var listAllCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetDataCheDoBHXH(maCanBo).ToList();
            var lstCanBoBHXHModel = _mapper.Map<List<TlCanBoCheDoBHXHModel>>(listCanBoCheDoBHXH);
            if (lstCanBoBHXHModel == null || lstCanBoBHXHModel.Count == 0)
                return;
            List<TlCanBoCheDoBHXHModel> results = new List<TlCanBoCheDoBHXHModel>();
            DataCanBoCheDoBHXH = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(lstCanBoBHXHModel.Where(x => x.IsDisplay));
            AllDataCanBoCheDoBHXH = _mapper.Map<ObservableCollection<TlCanBoCheDoBHXHModel>>(listAllCanBoCheDoBHXH);

            foreach (var item in DataCanBoCheDoBHXH)
            {
                item.INam = nam;
                item.IThang = thang;
                item.PropertyChanged += DetailCanBoCheDoBHXHModel_PropertyChanged;
                if (!item.IsHangCha)
                {
                    LoadCbxThangCanCuDong(maHieuCanBo);
                }
            }
            _cheDoView = CollectionViewSource.GetDefaultView(DataCanBoCheDoBHXH);
            _cheDoView.GroupDescriptions.Add(new PropertyGroupDescription("STenCheDoCha"));
            _cheDoView.Filter = ListCheDoFilter;
        }

        private void LoadCbxThangCanCuDong(string maHieuCanBo)
        {
            var currYear = Model.Nam;
            var currMonth = Model.Thang;
            CbxThangLuongCanCuDong = new List<ComboboxItem>();
            var latestMonth = _tlBangLuongThangService.GetLatestSalary(maHieuCanBo, currMonth, currYear);

            CbxThangLuongCanCuDong.Add(new ComboboxItem { ValueItem = null, DisplayItem = "" });
            CbxThangLuongCanCuDong.Add(new ComboboxItem { ValueItem = currMonth.ToString(), DisplayItem = "Tháng hiện tại" });
            if (latestMonth != null)
            {
                CbxThangLuongCanCuDong.Add(new ComboboxItem { ValueItem = latestMonth.Thang.ToString(), DisplayItem = "Tháng gần nhất đóng BHXH" });
            }
            else
            {
                CbxThangLuongCanCuDong.Add(new ComboboxItem { ValueItem = null, DisplayItem = "Tháng gần nhất đóng BHXH" });
            }
        }

        private void SaveCheDoBHXH()
        {
            if (DataCanBoCheDoBHXH != null)
            {
                List<TlCanBoCheDoBHXHModel> listCheDoAdd = DataCanBoCheDoBHXH.Where(x => !x.IsHangCha && x.IsModified && x.Id == Guid.Empty).ToList();
                List<TlCanBoCheDoBHXHModel> listCheDoEdit = DataCanBoCheDoBHXH.Where(x => !x.IsHangCha && x.IsModified && x.Id != Guid.Empty).ToList();

                if (listCheDoAdd.Count > 0)
                {
                    foreach (var item in listCheDoAdd)
                    {
                        item.SMaCanBo = Model.MaCanBo;
                        if (item.Id == Guid.Empty)
                            item.Id = Guid.NewGuid();
                        if (BhxhSalary.GIAMTRU_BH.Contains(item.SMaCheDo))
                            UpdateCanBoCheDoCon(item);

                    }
                    var lstEntities = _mapper.Map<List<TlCanBoCheDoBHXH>>(listCheDoAdd);
                    _tlCanBoCheDoBHXHService.AddRangeCanBoCheDo(lstEntities);
                }

                if (listCheDoEdit.Count > 0)
                {
                    foreach (var item in listCheDoEdit)
                    {
                        var cheDoBHXH = _tlCanBoCheDoBHXHService.FindCanBoCheDo(item.Id);
                        if (cheDoBHXH != null)
                        {
                            _mapper.Map(item, cheDoBHXH);
                            _tlCanBoCheDoBHXHService.UpdateCanBoCheDo(cheDoBHXH);
                            if (BhxhSalary.GIAMTRU_BH.Contains(item.SMaCheDo))
                                UpdateCanBoCheDoCon(item);
                        }
                    }
                }
                //Xóa item trống
                var listCanBoCheDoBHXH = _tlCanBoCheDoBHXHService.GetDataCheDoBHXH(Model.MaCanBo).ToList();
                var lstCanBoBHXHModel = _mapper.Map<List<TlCanBoCheDoBHXHModel>>(listCanBoCheDoBHXH);
                var deleteItem = lstCanBoBHXHModel.Where(x => x.IsDelete && !x.IsHangCha).ToList();
                if (deleteItem != null)
                {
                    foreach (var item in deleteItem)
                    {
                        if (item.Id != Guid.Empty)
                            _tlCanBoCheDoBHXHService.DeleteCanBoCheDo(item.Id);
                        var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTiet(Model.MaCanBo, item.SMaCheDo,
                            Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
                        if (canBoCheDoChiTiet != null)
                            _canBoCheDoBHXHChiTietService.RemoveRange(canBoCheDoChiTiet);
                    }
                }
                UpdateStatusHuongCheDoBHXH();

                foreach (var item in DataCanBoCheDoBHXH)
                {
                    UpdateCBCDChiTiet(item.SMaCheDo);
                }
            }
        }

        protected void UpdateCanBoCheDoCon(TlCanBoCheDoBHXHModel item)
        {
            try
            {
                var cachTinhLuong = _tlDmCachTinhLuongBaoHiemService.FindByMaCot(item.SMaCheDo);
                List<string> lstCheDo = new List<string>();

                if (cachTinhLuong != null)
                {
                    lstCheDo = cachTinhLuong.CongThuc.Split(StringUtils.SPLITCHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                var lstCheDoCon = AllDataCanBoCheDoBHXH.Where(x => lstCheDo.Contains(x.SMaCheDo));
                List<TlCanBoCheDoBHXHModel> lstCheDoConNew = new List<TlCanBoCheDoBHXHModel>();

                if (lstCheDoCon != null)
                {
                    foreach (var cheDo in lstCheDoCon)
                    {
                        if (cheDo.Id == Guid.Empty)
                        {
                            cheDo.Id = Guid.NewGuid();
                            cheDo.FSoNgayHuongBHXH = item.FSoNgayHuongBHXH;
                            cheDo.SMaCanBo = item.SMaCanBo;
                            cheDo.INam = item.INam;
                            cheDo.IThang = item.IThang;
                            cheDo.IThangLuongCanCuDong = item.IThangLuongCanCuDong;
                            lstCheDoConNew.Add(cheDo);
                        }
                        else
                        {
                            var cheDoBHXHCon = _tlCanBoCheDoBHXHService.FindCanBoCheDo(cheDo.Id);
                            cheDoBHXHCon.FSoNgayHuongBHXH = item.FSoNgayHuongBHXH;
                            cheDoBHXHCon.SMaCanBo = item.SMaCanBo;
                            cheDoBHXHCon.INam = item.INam;
                            cheDoBHXHCon.IThang = item.IThang;
                            cheDoBHXHCon.IThangLuongCanCuDong = item.IThangLuongCanCuDong;
                            _tlCanBoCheDoBHXHService.UpdateCanBoCheDo(cheDoBHXHCon);
                        }
                    }
                    if (lstCheDoConNew != null)
                        _tlCanBoCheDoBHXHService.AddRangeCanBoCheDo(_mapper.Map<List<TlCanBoCheDoBHXH>>(lstCheDoConNew));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected void OnDeleteCheDoBHXH()
        {
            if (DataCanBoCheDoBHXH != null && DataCanBoCheDoBHXH.Count > 0 && SelectedCanBoCheDoBHXH != null)
            {
                SelectedCanBoCheDoBHXH.IsDeleted = !SelectedCanBoCheDoBHXH.IsDeleted;
                OnPropertyChanged(nameof(DataCanBoCheDoBHXH));
            }
        }

        private void GetHolidays()
        {
            LstHoliday = new List<DateTime?>();
            var holidays = _tTlDmNgayNghiService.FindAll();
            if (holidays != null)
            {
                foreach (var typeH in holidays)
                {
                    DateTime? currDate = typeH.DTuNgay.GetValueOrDefault().Date;
                    while (currDate <= typeH.DDenNgay.GetValueOrDefault().Date)
                    {
                        LstHoliday.Add(currDate);
                        currDate = currDate.GetValueOrDefault().AddDays(1);
                    }
                }
            }
        }

        private int CountHolidays(DateTime? startDate, DateTime? endDate, List<DateTime?> lstHoliday)
        {
            int holidays = 0;
            DateTime? currDate = startDate.GetValueOrDefault().Date;

            while (currDate <= endDate.GetValueOrDefault().Date)
            {
                if (lstHoliday.Contains(currDate) && currDate.GetValueOrDefault().DayOfWeek == DayOfWeek.Sunday)
                {
                    holidays++;
                }
                currDate = currDate.GetValueOrDefault().AddDays(1);
            }

            return holidays;
        }

        private double CountDayOfBHXH(TlCanBoCheDoBHXHModel item)
        {
            double soNgayHuong;
            int sunDays = 0;
            var period = (item.DDenNgay.GetValueOrDefault() - item.DTuNgay.GetValueOrDefault()).Days + 1;
            for (int i = 0; i <= period; i++)
            {
                DateTime currDay = item.DTuNgay.GetValueOrDefault().AddDays(i);
                if (currDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    sunDays++;
                }
            }
            var holidays = CountHolidays(item.DTuNgay, item.DDenNgay, LstHoliday);
            soNgayHuong = period - sunDays - holidays;
            return soNgayHuong;
        }

        private void UpdateStatusHuongCheDoBHXH()
        {
            var isExistCanBoCheDo = _tlCanBoCheDoBHXHService.ExistCanBoCheDo(Model.MaCanBo);
            Model.BTinhBHXH = isExistCanBoCheDo ? true : false;
        }

        private void OnResetRegimeFilter()
        {
            SearchMaCheDo = string.Empty;
            SearchTenCheDo = string.Empty;

            _cheDoView.Refresh();
        }

        public List<TlCanBoCheDoBHXHModel> Recusive(TlCanBoCheDoBHXHModel item, List<TlCanBoCheDoBHXHModel> lstItem)
        {
            List<TlCanBoCheDoBHXHModel> lstData = new List<TlCanBoCheDoBHXHModel>();
            lstData.Add(item);
            if (lstItem.Any(n => n.SMaCheDoCha == item.SMaCheDo))
            {
                foreach (var child in lstItem.Where(n => n.SMaCheDoCha == item.SMaCheDo).OrderBy(n => n.SMaCheDo))
                {
                    lstData.AddRange(Recusive(child, lstItem));
                }
            }
            return lstData;
        }

        private void OnSelectionDoubleClick(object obj)
        {
            OnOpenCadresBHXHDetail((TlCanBoCheDoBHXHModel)obj);
        }

        private void OnOpenCadresBHXHDetail(TlCanBoCheDoBHXHModel obj)
        {
            try
            {
                if (SelectedCanBoCheDoBHXH == null || SelectedCanBoCheDoBHXH.BHangCha == true)
                {
                    return;
                }

                CadresBHXHViewModel.TlCanBoCheDoBHXHModel = SelectedCanBoCheDoBHXH;
                CadresBHXHViewModel.SMaCanBo = Model.MaCanBo;
                CadresBHXHViewModel.IThang = Model.Thang.GetValueOrDefault();
                CadresBHXHViewModel.INam = Model.Nam.GetValueOrDefault();
                CadresBHXHViewModel.Init();
                CadresBHXHViewModel.ShowDialogHost("CadresDetail");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void RefreshCBCD(object sender, EventArgs e)
        {
            var maCanBo = Model.MaCanBo;
            var thang = Model.Thang;
            var nam = Model.Nam;

            foreach (var item in DataCanBoCheDoBHXH)
            {
                var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetTongSoNgayHuong(maCanBo, item.SMaCheDo, thang.GetValueOrDefault(), nam.GetValueOrDefault());
                if (canBoCheDoChiTiet != null)
                {
                    item.DTuNgay = canBoCheDoChiTiet.DTuNgay;
                    item.DDenNgay = canBoCheDoChiTiet.DDenNgay;
                    item.FSoNgayHuongBHXH = canBoCheDoChiTiet.FSoNgayHuongBHXH;
                }
            }
        }

        private void UpdateCBCDChiTiet(string sMaCheDo)
        {
            var canBoCheDoChiTiet = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTiet(Model.MaCanBo, sMaCheDo, Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
            if (canBoCheDoChiTiet != null)
            {
                foreach (var item in canBoCheDoChiTiet)
                {
                    item.BTrangThai = true;
                    _canBoCheDoBHXHChiTietService.UpdateCBCDChiTiet(item);
                }
            }

            var inactiveItems = _canBoCheDoBHXHChiTietService.GetCanBoCheDoChiTietInactive(Model.Thang.GetValueOrDefault(), Model.Nam.GetValueOrDefault()).ToList();
            if (inactiveItems != null)
            {
                _canBoCheDoBHXHChiTietService.RemoveRange(inactiveItems);
            }
        }

        private void SetPhucapTienNangLuongCVD()
        {
            if (ItemsAllowence.Any())
            {
                var cvLNLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCV_TT"));
                var cdNLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCD_TT"));

                if (TienNangLuongCvd.GetValueOrDefault() == 0 && cvLNLCVTT != null)
                {
                    cvLNLCVTT.GiaTri = 0;
                }
                if (TienNangLuongCvd.GetValueOrDefault() == 0 && cdNLCDHTTT != null)
                {
                    cdNLCDHTTT.GiaTri = 0;
                }
            }
        }

        private void SetPhucapTienLuongBaoLuuCVD()
        {
            if (ItemsAllowence.Any())
            {
                var cvLLBLCVTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCV_TT"));
                var cdNLLBLCDHTTT = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCD_TT"));
                //Chuc vu
                if (_selectedChucVu != null && _selectedChucVu.Loai == false)
                {
                    if (TienBaoLuuCvd.GetValueOrDefault() == 0 && cvLLBLCVTT != null)
                    {
                        cvLLBLCVTT.GiaTri = 0;
                    }
                    else
                    {
                        cvLLBLCVTT.GiaTri = TienBaoLuuCvd;
                        cdNLLBLCDHTTT.GiaTri = 0;
                    }
                }
                else if (_selectedChucVu == null)
                {
                    cvLLBLCVTT.GiaTri = 0;
                    cdNLLBLCDHTTT.GiaTri = 0;
                }

                //Chuc danh
                if (_selectedChucVu != null && _selectedChucVu.Loai == true)
                {
                    if (TienBaoLuuCvd.GetValueOrDefault() == 0 && cdNLLBLCDHTTT != null)
                    {
                        cdNLLBLCDHTTT.GiaTri = 0;
                    }
                    else
                    {
                        cdNLLBLCDHTTT.GiaTri = TienBaoLuuCvd;
                        cvLLBLCVTT.GiaTri = 0;
                    }
                }
                else if (_selectedChucVu == null)
                {
                    cvLLBLCVTT.GiaTri = 0;
                    cdNLLBLCDHTTT.GiaTri = 0;
                }
            }
        }

        private void SetPhucapLuongCapbac()
        {
            if (ItemsAllowence.Any())
            {
                var luongCapBac = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCB_TT"));

                if (SelectedCapBac == null && luongCapBac != null)
                {
                    luongCapBac.GiaTri = 0;
                }
                else
                {
                    luongCapBac.GiaTri = TienLuongCb;
                }
            }
        }

        private void SetPhucapLuongCapbacLuong()
        {
            if (ItemsAllowence.Any())
            {
                var luongCapBac = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LCB_TT"));

                if (SelectedBacTienLuong == null && luongCapBac != null)
                {
                    luongCapBac.GiaTri = 0;
                }
            }
        }

        private void SetPhucapNangLuongCapbac()
        {
            if (ItemsAllowence.Any())
            {
                var nangLuongCapBac = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("NLCB_TT"));

                if (TienNangLuongCb.GetValueOrDefault() == 0 && nangLuongCapBac != null)
                {
                    nangLuongCapBac.GiaTri = 0;
                }
            }
        }

        private void SetPhucapLuongBaoLuuCapbac()
        {
            if (ItemsAllowence.Any())
            {
                var baoLuuCapBac = ItemsAllowence.FirstOrDefault(x => x.MaPhuCap.Equals("LBLCB_TT"));

                if (TienBaoLuuCb.GetValueOrDefault() == 0 && baoLuuCapBac != null)
                {
                    baoLuuCapBac.GiaTri = 0;
                }
            }
        }
    }
}
