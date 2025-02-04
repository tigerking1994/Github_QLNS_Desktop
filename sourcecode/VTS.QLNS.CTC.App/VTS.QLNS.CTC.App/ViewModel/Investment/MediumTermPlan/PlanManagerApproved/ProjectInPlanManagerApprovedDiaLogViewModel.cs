using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved
{
    public class ProjectInPlanManagerApprovedDiaLogViewModel : DetailViewModelBase<VdtKhvKeHoach5NamModel, DuAnKeHoachTrungHanModel>
    {
        #region Private

        private readonly IMapper _mapper;
        private readonly INsDonViService _donViService;
        private readonly ISessionService _sessionService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IVdtDaDuAnService _duAnService;
        private readonly IVdtDmLoaiCongTrinhService _vdtDmLoaiCongTrinhService;
        private readonly IVdtDaNguonVonService _vdtDaNguonVonService;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;
        private readonly ILog _logger;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuatService;

        private List<VdtKhvKeHoach5NamDeXuatChiTietModel> _vdtKhvFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
        private ICollectionView _vdtKhvKhthView;
        private ICollectionView _duAnView;
        private Dictionary<int, string> _dicNguonVon;
        private int _index = 0;
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private static int indexSMaDuAn;
        #endregion

        public override string Name => "Chọn dự án";
        public override string Description => "Kế hoạch trung hạn được duyệt";
        public override Type ContentType => typeof(ProjectInPlanManagerApprovedDiaLog);

        #region componer
        public Action<object> ChooseDuAnAction;
        public Action<object> ChooseDuAnDeXuatAction;
        public static List<DuAnKeHoachTrungHanModel> DuAnExisted = new List<DuAnKeHoachTrungHanModel>();
        public bool IsDuAnChuyenTiep => Model != null && (int)LoaiDuAnEnum.Type.CHUYEN_TIEP == Model.ILoai;

        private string _txtMaDuAn;
        public string TxtMaDuAn
        {
            get => _txtMaDuAn;
            set => SetProperty(ref _txtMaDuAn, value);
        }

        private string _txtName;
        public string TxtName
        {
            get => _txtName;
            set => SetProperty(ref _txtName, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonVi;
        public ObservableCollection<ComboboxItem> DrpDonVi
        {
            get => _drpDonVi;
            set => SetProperty(ref _drpDonVi, value);
        }

        private ComboboxItem _drpDonViSelected;
        public ComboboxItem DrpDonViSelected
        {
            get => _drpDonViSelected;
            set
            {
                SetProperty(ref _drpDonViSelected, value);
                if (_vdtKhvKhthView != null) _vdtKhvKhthView.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _drpChuDauTu;
        public ObservableCollection<ComboboxItem> DrpChuDauTu
        {
            get => _drpChuDauTu;
            set => SetProperty(ref _drpChuDauTu, value);
        }

        private ComboboxItem _drpChuDauTuSelected;
        public ComboboxItem DrpChuDauTuSelected
        {
            get => _drpChuDauTuSelected;
            set
            {
                SetProperty(ref _drpChuDauTuSelected, value);
                if (_duAnView != null) _duAnView.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _drpLoaiDuAn;
        public ObservableCollection<ComboboxItem> DrpLoaiDuAn
        {
            get => _drpLoaiDuAn;
            set => SetProperty(ref _drpLoaiDuAn, value);
        }

        private ComboboxItem _drpLoaiDuAnSelected;
        public ComboboxItem DrpLoaiDuAnSelected
        {
            get => _drpLoaiDuAnSelected;
            set => SetProperty(ref _drpLoaiDuAnSelected, value);
        }

        private bool _selectAllDuAn;
        public bool SelectAllDuAn
        {
            get => (Items == null || !Items.Any()) ? false : Items.Where(x => x.IsFilter).All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuAn, value);
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter).Select(c => { c.IsChecked = _selectAllDuAn; return c; }).ToList();
                }
            }
        }

        #region Dexuat
        private string _txtDiaDiemThucHien;
        public string TxtDiaDiemThucHien
        {
            get => _txtDiaDiemThucHien;
            set => SetProperty(ref _txtDiaDiemThucHien, value);
        }

        private string _txtThoiGianTu;
        public string TxtThoiGianTu
        {
            get => _txtThoiGianTu;
            set => SetProperty(ref _txtThoiGianTu, value);
        }

        private string _txtThoiGianDen;
        public string TxtThoiGianDen
        {
            get => _txtThoiGianDen;
            set => SetProperty(ref _txtThoiGianDen, value);
        }

        private ObservableCollection<ComboboxItem> _drpLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> DrpLoaiCongTrinh
        {
            get => _drpLoaiCongTrinh;
            set => SetProperty(ref _drpLoaiCongTrinh, value);
        }

        private ComboboxItem _drpLoaiCongTrinhSelected;
        public ComboboxItem DrpLoaiCongTrinhSelected
        {
            get => _drpLoaiCongTrinhSelected;
            set
            {
                SetProperty(ref _drpLoaiCongTrinhSelected, value);
                if (_vdtKhvKhthView != null) _vdtKhvKhthView.Refresh();
            }    
        }

        private ComboboxItem _drpNguonNganSachSelected;
        public ComboboxItem DrpNguonNganSachSelected
        {
            get => _drpNguonNganSachSelected;
            set
            {
                SetProperty(ref _drpNguonNganSachSelected, value);
                if (_vdtKhvKhthView != null) _vdtKhvKhthView.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _drpNguonNganSach;
        public ObservableCollection<ComboboxItem> DrpNguonNganSach
        {
            get => _drpNguonNganSach;
            set => SetProperty(ref _drpNguonNganSach, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLyDuAn;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLyDuAn
        {
            get => _drpDonViQuanLyDuAn;
            set => SetProperty(ref _drpDonViQuanLyDuAn, value);
        }

        private ComboboxItem _drpDonViQuanLyDuAnSelected;
        public ComboboxItem DrpDonViQuanLyDuAnSelected
        {
            get => _drpDonViQuanLyDuAnSelected;
            set
            {
                SetProperty(ref _drpDonViQuanLyDuAnSelected, value);
                if (_duAnView != null) _duAnView.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViThucHienDuAnDeXuat;
        public ObservableCollection<ComboboxItem> DrpDonViThucHienDuAnDeXuat
        {
            get => _drpDonViThucHienDuAnDeXuat;
            set => SetProperty(ref _drpDonViThucHienDuAnDeXuat, value);
        }

        private ComboboxItem _drpDonViThucHienDuAnDeXuatSelected;
        public ComboboxItem DrpDonViThucHienDuAnDeXuatSelected
        {
            get => _drpDonViThucHienDuAnDeXuatSelected;
            set
            {
                SetProperty(ref _drpDonViThucHienDuAnDeXuatSelected, value);
                if (_vdtKhvKhthView != null) _vdtKhvKhthView.Refresh();
            }
        }

        private VdtKhvKeHoach5NamDeXuatModel _model1 = new VdtKhvKeHoach5NamDeXuatModel();
        public VdtKhvKeHoach5NamDeXuatModel Model1
        {
            get => _model1;
            set => SetProperty(ref _model1, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel> _itemsKhthDeXuat;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel> ItemsKhthDeXuat
        {
            get => _itemsKhthDeXuat;
            set => SetProperty(ref _itemsKhthDeXuat, value);
        }

        private VdtKhvKeHoach5NamDeXuatChiTietModel _selectedKhthDeXuat;
        public VdtKhvKeHoach5NamDeXuatChiTietModel SelectedKhthDeXuat
        {
            get => _selectedKhthDeXuat;
            set => SetProperty(ref _selectedKhthDeXuat, value);
        }

        private VdtKhvKeHoach5NamDeXuatChiTietModel _summaryItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();
        public VdtKhvKeHoach5NamDeXuatChiTietModel SummaryItem
        {
            get => _summaryItem;
            set => SetProperty(ref _summaryItem, value);
        }

        private string _txtStt;
        public string TxtSTT
        {
            get => _txtStt;
            set => SetProperty(ref _txtStt, value);
        }

        private string _txtTenDuAn;
        public string TxtTenDuAn
        {
            get => _txtTenDuAn;
            set => SetProperty(ref _txtTenDuAn, value);
        }

        private bool _selectAllDuAn1;
        public bool SelectAllDuAn1
        {
            get => (ItemsKhthDeXuat == null || !ItemsKhthDeXuat.Any()) ? false : ItemsKhthDeXuat.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuAn1, value);
                if (ItemsKhthDeXuat != null)
                {
                    ItemsKhthDeXuat.Select(c => { c.IsChecked = _selectAllDuAn1; return c; }).ToList();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _drpVoucherSuggestionAgregates;
        public ObservableCollection<ComboboxItem> DrpVoucherSuggestionAgregates
        {
            get => _drpVoucherSuggestionAgregates;
            set => SetProperty(ref _drpVoucherSuggestionAgregates, value);
        }

        private ComboboxItem _drpVoucherSuggestionSelected;
        public ComboboxItem DrpVoucherSuggestionSelected
        {
            get => _drpVoucherSuggestionSelected;
            set
            {
                SetProperty(ref _drpVoucherSuggestionSelected, value);
                if(value != null)
                {
                    LoadDataDeXuat();
                }
            }
        }
        #endregion

        public RelayCommand DuAnDeXuatSearchCommand { get; set; }
        public RelayCommand DuAnDeXuatRefreshCommand { get; set; }
        public RelayCommand DuAnDeXuatChoiseCommand { get; set; }
        public RelayCommand DuAnDeXuatSaveCommand { get; set; }
        public RelayCommand DuAnDeXuatResetFilterCommand { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand ChooseCommand { get; }
        #endregion

        public ProjectInPlanManagerApprovedDiaLogViewModel(IMapper mapper,
            ISessionService sessionService,
            INsNguonNganSachService nsNguonNganSachService,
            INsDonViService donViService,
            IVdtDaDuAnService duAnService,
            ILog logger,
            IVdtDaNguonVonService vdtDaNguonVonService,
            IVdtDuAnHangMucService vdtDuAnHangMucService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuatService,
            IVdtKhvKeHoach5NamChiTietService vdtKhvKeHoach5NamChiTietService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuatService,
            IVdtDmLoaiCongTrinhService vdtDmLoaiCongTrinhService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IDmChuDauTuService dmChuDauTuService)
        {
            _mapper = mapper;
            _logger = logger;
            _duAnService = duAnService;
            _donViService = donViService;
            _dmChuDauTuService = dmChuDauTuService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _sessionService = sessionService;
            _vdtDaNguonVonService = vdtDaNguonVonService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _vdtDmLoaiCongTrinhService = vdtDmLoaiCongTrinhService;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _vdtKhvKeHoach5NamDeXuatService = vdtKhvKeHoach5NamDeXuatService;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuatService;

            DuAnDeXuatSearchCommand = new RelayCommand(obj => OnDuAnDeXuatSearch());
            DuAnDeXuatRefreshCommand = new RelayCommand(obj => OnDuAnDeXuatRefresh());
            DuAnDeXuatResetFilterCommand = new RelayCommand(obj => OnDuAnDeXuatResetFilter());
            DuAnDeXuatSaveCommand = new RelayCommand(obj => OnDuAnDeXuatSave(obj));
            DuAnDeXuatChoiseCommand = new RelayCommand(obj => OnDuAnDeXuatChoise(obj));

            SearchCommand = new RelayCommand(obj => _duAnView.Refresh());
            ChooseCommand = new RelayCommand(obj => OnChoose(obj));
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
        }

        #region RelayCommand
        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                OnResetFilter();
                LoadNguonNganSach();
                LoadLoaiCongTrinh();
                LoadDonViQuanLy();
                LoadChuDauTu();
                LoadLoaiDuAn();
                LoadHeader();
                LoadData();
                LoadDataSuggestionAgregate();
                LoadDataDeXuat();
                OnDuAnDeXuatResetFilter();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNguonNganSach()
        {
            var lstData = _nsNguonNganSachService.FindNguonNganSach();
            _dicNguonVon = lstData.ToDictionary(n => n.IIdMaNguonNganSach ?? 0, n => n.STen);
            List<ComboboxItem> lstCombobox = lstData.Select(n =>
            new ComboboxItem
            {
                DisplayItem = n.STen,
                ValueItem = n.IIdMaNguonNganSach.ToString()
            }).ToList();
            _drpNguonNganSach = new ObservableCollection<ComboboxItem>(lstCombobox);
        }

        private void LoadLoaiCongTrinh()
        {
            List<VdtDmLoaiCongTrinh> listLoaiCongTrinh = _vdtDmLoaiCongTrinhService.FindAll().ToList();
            _drpLoaiCongTrinh = _mapper.Map<ObservableCollection<ComboboxItem>>(listLoaiCongTrinh);
        }

        private void LoadDonViQuanLy()
        {
            var cbxLoaiDonViData = _vdtDmDonViThucHienDuAnService.FindAll().ToList();
            _drpDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
            _drpDonViQuanLyDuAn = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
        }

        private void LoadChuDauTu()
        {
            try
            {
                var cbxChuDauTu = _dmChuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork).Select(item => new ComboboxItem()
                {
                    ValueItem = item.IIDMaDonVi,
                    DisplayItem = string.Format("{0} - {1}", item.IIDMaDonVi, item.STenDonVi),
                    HiddenValue = item.Id.ToString()
                });

                _drpChuDauTu = new ObservableCollection<ComboboxItem>(cbxChuDauTu);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiDuAn()
        {
            List<ComboboxItem> lstLoaiDuAn = new List<ComboboxItem>();
            lstLoaiDuAn.Add(new ComboboxItem()
            {
                DisplayItem = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP),
                ValueItem = ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString()
            });
            lstLoaiDuAn.Add(new ComboboxItem()
            {
                DisplayItem = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI),
                ValueItem = ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()
            });
            _drpLoaiDuAn = new ObservableCollection<ComboboxItem>(lstLoaiDuAn);
        }

        private void LoadHeader()
        {
            if (Model != null && Model.IGiaiDoanTu != 0)
            {
                Model.HeaderAfterYear = string.Format("Vốn bố trí sau năm {0}", (Model.IGiaiDoanTu + 4).ToString());
                Model.HeaderGroupVonBoTri = string.Format("NHU CẦU BỐ TRÍ VỐN NSQP {0} - {1}", Model.IGiaiDoanTu, Model.IGiaiDoanDen);
                Model.HeaderAfterYearModified = string.Format("Vốn bố trí sau năm {0} (Sau điều chỉnh)", Model.IGiaiDoanDen.ToString());

                Model.HeaderPlan1 = string.Format("Năm {0}", (Model.IGiaiDoanTu).ToString());
                Model.HeaderPlan2 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 1).ToString());
                Model.HeaderPlan3 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 2).ToString());
                Model.HeaderPlan4 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 3).ToString());
                Model.HeaderPlan5 = string.Format("Năm {0}", (Model.IGiaiDoanTu + 4).ToString());
                Model.HeaderAfterYearPlan = string.Format("Sau năm {0}", (Model.IGiaiDoanTu + 4).ToString());
            }
        }

        #region DeXuat

        private void LoadDataSuggestionAgregate()
        {
            try
            {
                var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
                predicate = predicate.And(x => x.ILoai == Model.ILoai);
                predicate = predicate.And(x => x.IGiaiDoanTu == Model.IGiaiDoanTu);
                predicate = predicate.And(x => x.IGiaiDoanDen == Model.IGiaiDoanDen);
                predicate = predicate.And(x => x.IIdMaDonViQuanLy == Model.IIdMaDonVi);
                predicate = predicate.And(x => !string.IsNullOrEmpty(x.STongHop));
                predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);

                var itemQuery = _vdtKhvKeHoach5NamDeXuatService.FindByCondition(predicate).ToList();
                DrpVoucherSuggestionAgregates = _mapper.Map<ObservableCollection<ComboboxItem>>(itemQuery);
                
                if(DrpVoucherSuggestionAgregates.Count() > 0 && DrpVoucherSuggestionSelected == null)
                {
                    DrpVoucherSuggestionSelected = DrpVoucherSuggestionAgregates.FirstOrDefault();
                }

                OnPropertyChanged(nameof(DrpVoucherSuggestionSelected));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDataDeXuat()
        {
            try
            {
                if (IsDuAnChuyenTiep)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstItems = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(DrpVoucherSuggestionSelected != null ? DrpVoucherSuggestionSelected.ValueItem : Guid.Empty.ToString()).ToList();
                    lstItems = lstItems.Where(x => x.IIdKeHoach5NamId != null && x.IIdKeHoach5NamId != Guid.Empty).ToList();

                    // Remove data exits
                    if (this.ParentPage is PlanManagerApprovedDetailViewModel parentVm)
                    {
                        lstItems = lstItems.Where(x => !parentVm.Items.Any(y => x.IIdDuAnId == y.IIdDuAnId && x.IIdNguonVonId == y.IIdNguonVonId && x.IIdLoaiCongTrinhId == y.IIdLoaiCongTrinhId)).ToList();
                    }
                    ItemsKhthDeXuat = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>>(lstItems);
                }
                else
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstItems = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex((DrpVoucherSuggestionSelected != null) ? DrpVoucherSuggestionSelected.ValueItem : Guid.Empty.ToString()).ToList();

                    ItemsKhthDeXuat = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamDeXuatChiTietModel>>(lstItems);
                }

                var max_level = ItemsKhthDeXuat.Select(x => x.Level).Max();
                VdtKhvKeHoach5NamDeXuatChiTietModel itemMaxLevel = ItemsKhthDeXuat.Where(x => x.Level.Equals(max_level)).FirstOrDefault();
                if (itemMaxLevel != null)
                {
                    CalculateDataDeXuat(itemMaxLevel);
                }
                CalculatTotal();

                foreach (var item in ItemsKhthDeXuat)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged1;
                }

                _vdtKhvKhthView = CollectionViewSource.GetDefaultView(ItemsKhthDeXuat);
                _vdtKhvKhthView.Filter = KeHoachTrungHanFilter;

                OnPropertyChanged(nameof(Model1));
                OnPropertyChanged(nameof(ItemsKhthDeXuat));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged1(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IsChecked))
                {
                    VdtKhvKeHoach5NamDeXuatChiTietModel itemChoose = (VdtKhvKeHoach5NamDeXuatChiTietModel)sender;
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildrent = GetChildren(ItemsKhthDeXuat.ToList(), itemChoose.Id).ToList();
                    lstChildrent.Select(x => { x.IsChecked = itemChoose.IsChecked; return x; }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private IEnumerable<VdtKhvKeHoach5NamDeXuatChiTietModel> GetParent(List<VdtKhvKeHoach5NamDeXuatChiTietModel> foos, VdtKhvKeHoach5NamDeXuatChiTietModel childrent)
        {
            var parent = foos.FirstOrDefault(x => x.Id == childrent.IdParent);
            if (parent == null)
                return Enumerable.Empty<VdtKhvKeHoach5NamDeXuatChiTietModel>();

            return new[] { parent }.Concat(GetParent(foos, parent));
        }

        private static List<VdtKhvKeHoach5NamDeXuatChiTietModel> GetChildren(List<VdtKhvKeHoach5NamDeXuatChiTietModel> foos, Guid id)
        {
            return foos
                .Where(x => x.IdParent == id)
                .Union(foos.Where(x => x.IdParent == id)
                    .SelectMany(y => GetChildren(foos, y.Id))
                ).ToList();
        }

        private void CalculateDataDeXuat(VdtKhvKeHoach5NamDeXuatChiTietModel? itemSelected = null)
        {
            try
            {
                if (itemSelected != null)
                {
                    if (itemSelected.Level > 1)
                    {
                        List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstParent = GetParent(ItemsKhthDeXuat.ToList(), itemSelected).ToList();
                        List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildren = ItemsKhthDeXuat.Where(x => x.Level <= itemSelected.Level && !x.Level.Equals(1)).ToList();

                        lstChildren = lstChildren.Where(x => x.IdParent != null && lstParent.Select(y => y.Id).Contains(x.IdParent.Value)).ToList();
                        lstChildren.Where(x => !x.IIdNguonVonId.Equals((int)MediumTermType.Nsqp)).Select(x => { x.FTongSoNhuCauNSQP = 0; x.FTongSoNhuCauNSQPOrigin = 0; return x; }).ToList();

                        lstParent.Select(x =>
                        {
                            x.FTongSoNhuCauNSQP = 0;

                            return x;
                        }).ToList();

                        if (lstChildren != null && lstChildren.Count > 0)
                        {
                            foreach (var item in lstChildren)
                            {
                                CalculateDeXuatParent(item, item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateDeXuatParent(VdtKhvKeHoach5NamDeXuatChiTietModel currentItem, VdtKhvKeHoach5NamDeXuatChiTietModel seftItem)
        {
            try
            {
                var parrentItem = ItemsKhthDeXuat.Where(x => x.Id == currentItem.IdParent && x.IsFilter).FirstOrDefault();
                if (parrentItem == null) return;
                parrentItem.FTongSoNhuCauNSQP += seftItem.FTongSoNhuCauNSQP;
                CalculateDeXuatParent(parrentItem, seftItem);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculatTotal()
        {
            try
            {
                if (SummaryItem == null) SummaryItem = new VdtKhvKeHoach5NamDeXuatChiTietModel();
                SummaryItem.FGiaTriBoTri = 0;
                SummaryItem.FGiaTriKeHoach = 0;
                SummaryItem.FGiaTriNamThuNhat = 0;
                SummaryItem.FGiaTriNamThuHai = 0;
                SummaryItem.FGiaTriNamThuBa = 0;
                SummaryItem.FGiaTriNamThuTu = 0;
                SummaryItem.FGiaTriNamThuNam = 0;
                SummaryItem.FHanMucDauTu = 0;
                SummaryItem.FTongSoNhuCauNSQP = 0;

                foreach (var item in ItemsKhthDeXuat.Where(x => x.IsFilter))
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildrent = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
                    if (item.IsParent && item.IdParent == null)
                    {
                        lstChildrent = GetChildren(ItemsKhthDeXuat.ToList(), item.Id);
                        lstChildrent = lstChildrent.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();
                    }

                    if ((!item.IsParent && item.IsFilter) || (lstChildrent != null && lstChildrent.Count == 0 && item.IsParent && item.IdParent == null))
                    {
                        SummaryItem.FTongSoNhuCauNSQP += item.FTongSoNhuCauNSQP;
                        SummaryItem.FHanMucDauTu += item.FHanMucDauTu;
                        SummaryItem.FGiaTriNamThuNam += item.FGiaTriNamThuNam;
                        SummaryItem.FGiaTriNamThuTu += item.FGiaTriNamThuTu;
                        SummaryItem.FGiaTriNamThuBa += item.FGiaTriNamThuBa;
                        SummaryItem.FGiaTriNamThuHai += item.FGiaTriNamThuHai;
                        SummaryItem.FGiaTriNamThuNhat += item.FGiaTriNamThuNhat;
                        SummaryItem.FGiaTriBoTri += item.FGiaTriBoTri;
                        SummaryItem.FGiaTriKeHoach += item.FGiaTriKeHoach;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private bool KeHoachTrungHanFilter(object obj)
        {
            try
            {
                if (!(obj is VdtKhvKeHoach5NamDeXuatChiTietModel temp)) return true;
                var bCondition = true;

                if (_vdtKhvFilter != null && _vdtKhvFilter.Count > 0)
                {
                    bCondition &= _vdtKhvFilter.Where(x => x.Id.Equals(temp.Id)).Count() > 0;
                }

                if (DrpLoaiCongTrinhSelected != null)
                {
                    bCondition &= temp.IIdLoaiCongTrinhId.ToString().Equals(DrpLoaiCongTrinhSelected.ValueItem.ToString());
                }

                if (DrpDonViSelected != null)
                {
                    bCondition &= !string.IsNullOrEmpty(temp.IIdMaDonVi) && (temp.IIdMaDonVi.Equals(DrpDonViSelected.ValueItem));
                }

                if (!string.IsNullOrEmpty(TxtDiaDiemThucHien))
                {
                    bCondition &= (temp.SDiaDiem.ToLower().Contains(TxtDiaDiemThucHien.ToLower()));
                }

                if (!string.IsNullOrEmpty(TxtThoiGianTu))
                {
                    bCondition &= (temp.IGiaiDoanTu.ToString().ToLower().Equals(TxtThoiGianTu.ToLower()));
                }

                if (!string.IsNullOrEmpty(TxtThoiGianDen))
                {
                    bCondition &= (temp.IGiaiDoanDen.ToString().ToLower().Equals(TxtThoiGianDen.ToLower()));
                }

                if (DrpNguonNganSachSelected != null)
                {
                    bCondition &= (temp.IIdNguonVonId.ToString().Equals(DrpNguonNganSachSelected.ValueItem.ToString()));
                }

                _index += 1;
                if (ItemsKhthDeXuat.Count.Equals(_index))
                {
                    _vdtKhvFilter = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();
                    _index = 0;
                }

                temp.IsFilter = bCondition;

                return bCondition;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void OnDuAnDeXuatSearch()
        {
            List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstFilterSTen = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

            if (!string.IsNullOrEmpty(TxtTenDuAn))
            {
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstParent = ItemsKhthDeXuat.Where(x => x.STen.ToLower().Contains(TxtTenDuAn.ToLower())).ToList();
                lstFilterSTen.AddRange(lstParent);
                foreach (var item in lstParent)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChildrent = GetChildren(ItemsKhthDeXuat.ToList(), item.Id);
                    lstFilterSTen.AddRange(lstChildrent);
                }
                lstFilterSTen = lstFilterSTen.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();
                _vdtKhvFilter.AddRange(lstFilterSTen);
            }

            _vdtKhvKhthView.Refresh();
        }

        private void OnDuAnDeXuatChoise(object obj)
        {
            if (obj is Window mainWindow)
            {
                ChooseDuAnDeXuatAction?.Invoke(ItemsKhthDeXuat.Where(x => x.IsChecked).ToList());
                mainWindow.Close();
            }
        }

        private void OnDuAnDeXuatSave(object obj)
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstChoose = ItemsKhthDeXuat.Where(x => x.IsChecked).ToList();         //chi tiết của CT tổng hợp, được chọn
                List<VdtKhvKeHoach5NamDeXuatChiTietModel> lstResult = new List<VdtKhvKeHoach5NamDeXuatChiTietModel>();

                List<string> lstIdDuAn = new List<string>();        //id trong bang VDT_DA_DuAn
                if (lstChoose != null && lstChoose.Count > 0)
                {
                    foreach (var item in lstChoose)
                    {
                        if (item.IdReference.HasValue)
                        {
                            List<VdtKhvKeHoach5NamDeXuatChiTietModel> listDuAn = ItemsKhthDeXuat.Where(x => x.Id == item.IdReference).ToList();     //lấy ra dự án cha(hoặc dự án ông nội nếu có)
                            lstResult.AddRange(listDuAn);
                        }
                    }

                    lstResult.AddRange(lstChoose);
                    lstResult = lstResult.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();

                    string lstId = string.Join(",", lstResult.Select(x => x.Id).ToList());
                    List<DuAnQuery> lstDuAn = _vdtKhvKeHoach5NamChiTietDexuatService.FindListDuAn(lstId).ToList();
                    List<VdtDaDuAn> lstDuanAdded = new List<VdtDaDuAn>();
                    if (lstDuAn != null && lstDuAn.Count > 0)
                    {
                        foreach (var item in lstDuAn.OrderBy(x => x.STT))
                        {
                            if (!item.Id.HasValue)
                            {
                                VdtDaDuAn entity = new VdtDaDuAn();
                                entity.FHanMucDauTu = item.FHanMucDauTu;
                                entity.IIdDonViThucHienDuAnId = item.IdDonViQuanLy;
                                entity.STenDuAn = item.STenDuAn;
                                entity.SMaDuAn = item.SMaDuAn.Remove(0, item.SMaDuAn.IndexOf("-")).Insert(0, Model.IIdMaDonVi);
                                entity.IdDuAnKhthDeXuat = item.IdReference;
                                entity.IMaDuAnIndex = item.IndexDuAn;
                                entity.SDiaDiem = item.SDiaDiem;
                                entity.SKhoiCong = item.SKhoiCong;
                                entity.SKetThuc = item.SKetThuc;
                                entity.Id = Guid.NewGuid();
                                entity.DDateCreate = DateTime.Now;
                                entity.SUserCreate = _sessionService.Current.Principal;
                                entity.STrangThaiDuAn = PROJECT_STATUS.KHOI_TAO;
                                entity.IIdMaDonViThucHienDuAn = item.SMaDonViQuanLy;
                                entity.IIdLoaiCongTrinhId = item.IIdLoaiCongTrinhId;
                                entity.IIdMaDonViQuanLy = Model.IIdMaDonVi;
                                entity.IIdDonViQuanLyId = Model.IIdDonViId;
                                lstIdDuAn.Add(entity.Id.ToString());
                                lstDuanAdded.Add(entity);

                                _duAnService.Add(entity);
                            }
                            else
                            {
                                VdtDaDuAn entity = _duAnService.FindById(item.Id.Value);
                                if (entity != null)
                                {
                                    entity.FHanMucDauTu = item.FHanMucDauTu;
                                    entity.IIdDonViThucHienDuAnId = item.IdDonViQuanLy;
                                    entity.STenDuAn = item.STenDuAn;
                                    entity.SMaDuAn = item.SMaDuAn;
                                    entity.IdDuAnKhthDeXuat = item.IdReference;
                                    entity.IMaDuAnIndex = item.IndexDuAn;
                                    entity.SDiaDiem = item.SDiaDiem;
                                    entity.SKhoiCong = item.SKhoiCong;
                                    entity.SKetThuc = item.SKetThuc;
                                    entity.IIdMaDonViThucHienDuAn = item.SMaDonViQuanLy;
                                    entity.IIdLoaiCongTrinhId = item.IIdLoaiCongTrinhId;
                                    entity.IIdMaDonViQuanLy = Model.IIdMaDonVi;
                                    entity.IIdDonViQuanLyId = Model.IIdDonViId;
                                    entity.DDateUpdate = DateTime.Now;
                                    entity.SUserUpdate = _sessionService.Current.Principal;
                                    lstIdDuAn.Add(entity.Id.ToString());
                                    lstDuanAdded.Add(entity);

                                    _duAnService.Update(entity);
                                }
                            }
                        }
                    }

                    string lstDuAnId = string.Join(",", lstIdDuAn.ToList());
                    List<DuAnHangMucQuery> lstDuAnHangMuc = _vdtKhvKeHoach5NamChiTietDexuatService.FindListDuAnHangMuc(lstId, lstDuAnId).ToList();

                    // chỉ lưu những chi tiết dự án vào bảng dự án hạng mục nếu có chi tiết còn nếu dự án ko có chi tiết thì vẫn lưu.
                    var dicParent = lstDuAnHangMuc.Where(x => x.IdParentKhth is null).Select(s => s.IdKhth).Distinct().ToDictionary(x => x, x => x);
                    foreach (var item in dicParent)
                    {
                        if (lstDuAnHangMuc.Where(x => x.IdParentKhth != null).Select(s => s.IdParentKhth).Contains(item.Key))
                        {
                            lstDuAnHangMuc.Remove(lstDuAnHangMuc.FirstOrDefault(x => x.IdKhth == item.Key));
                        }
                    }

                    List<DuAnNguonVonQuery> lstDuAnNguonVon = _vdtKhvKeHoach5NamChiTietDexuatService.FindListNguonVon(lstId, lstDuAnId).ToList();
                    if (lstDuAnHangMuc != null && lstDuAnHangMuc.Count > 0)
                    {
                        foreach (var item in lstDuAnHangMuc)
                        {
                            VdtDaDuAnHangMuc duAnHangMuc = _vdtDuAnHangMucService.FindByDuAnHangMuc(item.IdDuAn.Value, item.IdNguonVon, item.IdLoaiCongTrinh).FirstOrDefault();
                            if (duAnHangMuc != null)
                            {
                                duAnHangMuc.IIdDuAnId = item.IdDuAn;
                                duAnHangMuc.iID_NguonVonID = item.IdNguonVon;
                                duAnHangMuc.fHanMucDauTu = item.FHanMucDauTu;
                                duAnHangMuc.IIdParentId = item.IdParent;
                                duAnHangMuc.MaOrDer = item.SMaHangMuc;
                                duAnHangMuc.IdLoaiCongTrinh = item.IdLoaiCongTrinh;
                                duAnHangMuc.STenHangMuc = item.STenLoaiCongTrinh;
                                duAnHangMuc.indexMaHangMuc = item.IndexHangMuc;
                                duAnHangMuc.SMaHangMuc = item.SMaHangMuc;

                                _vdtDuAnHangMucService.Update(duAnHangMuc);
                            }
                            else
                            {
                                VdtDaDuAnHangMuc entity = new VdtDaDuAnHangMuc();
                                entity.IIdDuAnId = item.IdDuAn;
                                entity.iID_NguonVonID = item.IdNguonVon;
                                entity.fHanMucDauTu = item.FHanMucDauTu;
                                entity.IIdParentId = item.IdParent;
                                entity.MaOrDer = item.SMaHangMuc;
                                entity.IdLoaiCongTrinh = item.IdLoaiCongTrinh;
                                entity.STenHangMuc = item.STenLoaiCongTrinh;
                                entity.indexMaHangMuc = item.IndexHangMuc;
                                entity.SMaHangMuc = item.SMaHangMuc;

                                _vdtDuAnHangMucService.Add(entity);
                            }
                        }
                    }

                    if (lstDuAnNguonVon != null && lstDuAnNguonVon.Count > 0)
                    {
                        foreach (var item in lstDuAnNguonVon)
                        {
                            VdtDaNguonVon ngVon = _vdtDaNguonVonService.FindByNguonVon(item.IdDuAn.Value, item.IdNguonVon.Value).FirstOrDefault();
                            if (ngVon != null)
                            {
                                ngVon.IIdNguonVonId = item.IdNguonVon.Value;
                                ngVon.FThanhTien = item.FHanMucDauTu.Value;
                                ngVon.IIdDuAn = item.IdDuAn.Value;

                                _vdtDaNguonVonService.Update(ngVon);
                            }
                            else
                            {
                                VdtDaNguonVon entity = new VdtDaNguonVon();
                                entity.Id = Guid.NewGuid();
                                entity.IIdDuAn = item.IdDuAn.Value;
                                entity.IIdNguonVonId = item.IdNguonVon;
                                entity.FThanhTien = item.FHanMucDauTu.Value;

                                _vdtDaNguonVonService.Add(entity);
                            }
                        }
                    }

                    foreach (var item in lstDuanAdded)
                    {
                        var itemDuAnHangMuc = lstDuAnHangMuc.Where(x => x.IdDuAn == item.Id);

                        //nếu không thêm chi tiết, cần insert 1 dòng hạng mục mặc định
                        if (itemDuAnHangMuc == null || itemDuAnHangMuc.Count() == 0)
                        {
                            if (item.IIdLoaiCongTrinhId != null)
                            {
                                var itemDuAnHangMucNew = new VdtDaDuAnHangMuc();
                                itemDuAnHangMucNew.IIdDuAnId = item.Id;
                                itemDuAnHangMucNew.IdLoaiCongTrinh = item.IIdLoaiCongTrinhId;

                                var lct = _vdtDmLoaiCongTrinhService.FindById(item.IIdLoaiCongTrinhId.HasValue ? item.IIdLoaiCongTrinhId.Value : Guid.NewGuid());
                                itemDuAnHangMucNew.STenHangMuc = string.Format("{0}-{1}", item.STenDuAn, lct.STenLoaiCongTrinh);
                                itemDuAnHangMucNew.fHanMucDauTu = item.FHanMucDauTu;
                                itemDuAnHangMucNew.iID_NguonVonID = item.IIdNguonVonId;
                                _vdtDuAnHangMucService.Add(itemDuAnHangMucNew);
                            }
                        }
                        //nếu thêm chi tiết, cần set IIdLoaiCongTrinhId của dự án = null
                        else
                        {
                            var itemDuAn = _duAnService.FindById(item.Id);
                            if (itemDuAn != null)
                            {
                                itemDuAn.IIdLoaiCongTrinhId = null;
                                _duAnService.Update(itemDuAn);
                            }
                        }
                    }
                }

                List<DuAnKeHoachTrungHanQuery> lstData = new List<DuAnKeHoachTrungHanQuery>();
                foreach (var item in lstIdDuAn.ToList())
                {
                    List<DuAnKeHoachTrungHanQuery> lstItem = new List<DuAnKeHoachTrungHanQuery>();
                    var itemHangMuc = _vdtDuAnHangMucService.FindByIdDuAn(Guid.Parse(item)).ToList();
                    var itemDuAn = _duAnService.FindById(Guid.Parse(item));
                    if (itemHangMuc != null && itemHangMuc.Count() > 0 && (itemDuAn.IIdLoaiCongTrinhId == null || itemDuAn.IIdLoaiCongTrinhId == Guid.Empty))
                    {
                        lstItem = _duAnService.GetDuAnChooseInKeHoachTrungHanDeXuat(item, MediumTermPlanType.DETAIL_PROJECT);
                    }
                    else
                    {
                        lstItem = _duAnService.GetDuAnChooseInKeHoachTrungHanDeXuat(item, MediumTermPlanType.PROJECT);
                    }
                    lstData.AddRange(lstItem);
                }

                List<DuAnKeHoachTrungHanModel> lstDeXuatChoose = _mapper.Map<List<DuAnKeHoachTrungHanModel>>(lstData);

                StringBuilder messageBuilder = new StringBuilder();

                if(DuAnExisted != null && DuAnExisted.Count() > 0)
                {
                    var lstDuAnDeXuatDuplicate = lstDeXuatChoose.Where(x => DuAnExisted.Any(y => y.IIdDuAnId == x.IIdDuAnId && y.IIdNguonVonId == x.IIdNguonVonId && y.IIdLoaiCongTrinhId == x.IIdLoaiCongTrinhId)).ToList();
                    lstDuAnDeXuatDuplicate.Select(x => { messageBuilder.AppendLine(string.Format(Resources.ProjectExisted, x.STenDuAn, x.STenLoaiCongTrinh, x.STenNguonVon)); return x; }).ToList();
                    lstDeXuatChoose = lstDeXuatChoose.Where(x => !DuAnExisted.Any(y => x.IIdDuAnId == y.IIdDuAnId && x.IIdNguonVonId == y.IIdNguonVonId && x.IIdLoaiCongTrinhId == y.IIdLoaiCongTrinhId)).ToList();
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(messageBuilder.ToString(), Resources.Alert);
                }

                List<DuAnKeHoachTrungHanModel> lstDeXuatResult = lstDeXuatChoose.Where(x => lstResult.Any(y => y.IIdNguonVonId == x.IIdNguonVonId && x.IIdLoaiCongTrinhId == y.IIdLoaiCongTrinhId)).ToList();
                DuAnExisted.AddRange(lstDeXuatResult);

                var window = obj as Window;
                window.Close();
                ChooseDuAnAction?.Invoke(lstDeXuatResult);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        public override void LoadData(params object[] args)
        {
            try
            {
                List<DuAnKeHoachTrungHanQuery> lstData = _duAnService.GetDuAnChooseInKeHoachTrungHan().ToList();
                var lstItems = _mapper.Map<List<DuAnKeHoachTrungHanModel>>(lstData);
                // Disable parent project
                lstItems = lstItems.Select(x =>
                {
                    if (x.ILoaiDuAn.Equals(MediumTermPlanType.DUAN))
                    {
                        var items = lstItems.Where(y => y.IIdDuAnId == x.IIdDuAnId).ToList();
                        if (items != null && items.Count > 1)
                        {
                            x.IsEnableChk = false;
                        }
                    }
                    x.IsDuAnOffer = false;
                    x.IsEnableDd = true;
                    x.IIdKeHoach5NamChiTietId = Guid.NewGuid();
                    return x;
                }).ToList();

                // Remove data exits
                if (DuAnExisted != null && DuAnExisted.Count > 0)
                {
                    lstItems = lstItems.Where(x => !DuAnExisted.Any(y => x.IIdDuAnId == y.IIdDuAnId && x.IIdNguonVonId == y.IIdNguonVonId && x.IIdLoaiCongTrinhId == y.IIdLoaiCongTrinhId)).ToList();
                }

                lstItems = lstItems.OrderBy(x => x.IIdDuAnId).OrderBy(x => x.ILoaiDuAn).OrderByDescending(x => x.DDateCreate).ToList();
                Items = _mapper.Map<ObservableCollection<DuAnKeHoachTrungHanModel>>(lstItems);

                _duAnView = CollectionViewSource.GetDefaultView(Items);
                _duAnView.Filter = DuAnFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }

                foreach (var item in Items)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }

                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdMaChuDauTu)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.STenDuAn)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdNguonVonId)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdLoaiCongTrinhId)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdMaDonVi)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.SDiaDiem)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.SKhoiCong)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.SKetThuc)
                || args.PropertyName == nameof(DuAnKeHoachTrungHanModel.FHanMucDauTu))
            {
                DuAnKeHoachTrungHanModel item = (DuAnKeHoachTrungHanModel)sender;
                item.IsModified = true;
                if (SelectedItem != null && (args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdMaChuDauTu)
                    || (args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdMaDonVi))))
                {
                    string sMaDuAn = UpdateMaDuAn();
                    List<DuAnKeHoachTrungHanModel> lstUpdate = Items.Where(x => x.IIdDuAnId == SelectedItem.IIdDuAnId).ToList();
                    lstUpdate.Select(x => { x.SMaDuAn = sMaDuAn; return x; }).ToList();
                    OnPropertyChanged(nameof(Items));
                }

                if (args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdNguonVonId))
                {
                    if (SelectedItem != null)
                    {
                        if (ValidateDetailProject())
                        {
                            SelectedItem.IIdNguonVonId = 0;
                            SelectedItem.STenNguonVon = string.Empty;
                            MessageBox.Show(Resources.MsgDaNVHangMucExisted);
                            return;
                        }
                    }
                }

                if (args.PropertyName == nameof(DuAnKeHoachTrungHanModel.IIdLoaiCongTrinhId))
                {
                    if (SelectedItem != null)
                    {
                        if (ValidateDetailProject())
                        {
                            SelectedItem.IIdLoaiCongTrinhId = null;
                            SelectedItem.STenLoaiCongTrinh = string.Empty;
                            MessageBox.Show(Resources.MsgDaNVHangMucExisted);
                            return;
                        }
                    }
                }

                OnPropertyChanged(nameof(Items));
            }
        }

        private string UpdateMaDuAn()
        {
            string sMaDuAn = string.Empty;
            if (SelectedItem != null && !SelectedItem.IsClone)
            {
                string sMaChuDauTu = !string.IsNullOrEmpty(SelectedItem.IIdMaChuDauTu) ? SelectedItem.IIdMaChuDauTu : "XXX";
                int indexGenerate = _duAnService.FindNextSoChungTuIndex();
                string sGenerate = indexGenerate.ToString("D4");
                string sMaDonVi = !string.IsNullOrEmpty(SelectedItem.IIdMaDonVi) ? SelectedItem.IIdMaDonVi : "XXX";
                sMaDuAn = string.Format("{0}-{1}-{2}", sMaDonVi, sMaChuDauTu, sGenerate);
            }
            return sMaDuAn;
        }

        private void OnResetFilter()
        {
            TxtName = string.Empty;
            TxtMaDuAn = string.Empty;
            DrpDonViSelected = null;
            DrpDonViQuanLyDuAnSelected = null;
            DrpChuDauTuSelected = null;
            DrpVoucherSuggestionSelected = null;

            this.LoadData();

            OnPropertyChanged(nameof(DrpDonViSelected));
            OnPropertyChanged(nameof(DrpDonViQuanLyDuAnSelected));
            OnPropertyChanged(nameof(DrpChuDauTuSelected));
            OnPropertyChanged(nameof(DrpVoucherSuggestionSelected));
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected void OnDuAnDeXuatRefresh()
        {
            this.LoadDataDeXuat();
            OnPropertyChanged(nameof(ItemsKhthDeXuat));
            OnPropertyChanged(nameof(Model1));
        }

        private void OnDuAnDeXuatResetFilter()
        {
            DrpLoaiCongTrinhSelected = null;
            DrpDonViQuanLyDuAnSelected = null;
            TxtTenDuAn = string.Empty;
            TxtDiaDiemThucHien = string.Empty;
            TxtThoiGianTu = string.Empty;
            TxtThoiGianDen = string.Empty;
            TxtSTT = string.Empty;
            DrpNguonNganSachSelected = null;
            this.LoadDataDeXuat();

            OnPropertyChanged(nameof(DrpLoaiCongTrinhSelected));
            OnPropertyChanged(nameof(DrpDonViQuanLyDuAnSelected));
            OnPropertyChanged(nameof(DrpNguonNganSachSelected));
        }

        public void OnChoose(object obj)
        {
            try
            {
                List<DuAnKeHoachTrungHanModel> lstDataChoose = Items.Where(n => n.IsChecked).ToList();
                if (!lstDataChoose.Any())
                {
                    MessageBox.Show(Resources.MsgCheckDuAn);
                    return;
                }
                if (lstDataChoose.Any(x => x.IsModified))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotSave);
                    return;
                }
                if (lstDataChoose.Any(n => n.IIdNguonVonId == 0))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveNguonvon);
                    return;
                }
                if (lstDataChoose.Any(n => n.IIdLoaiCongTrinhId == null || n.IIdLoaiCongTrinhId == Guid.Empty))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveLoaiCongTrinh);
                    return;
                }

                DuAnExisted.AddRange(lstDataChoose);

                var window = obj as Window;
                window.Close();
                ChooseDuAnAction?.Invoke(lstDataChoose);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBox.Show(Resources.MsgErrorDuAnNotSave);
                return;
            }
        }

        public override void OnSave()
        {
            try
            {
                List<DuAnKeHoachTrungHanModel> lstDuAn = Items.Where(n => n.IsNew).ToList();
                List<DuAnKeHoachTrungHanModel> lstClone = Items.Where(n => n.IsClone).ToList();
                List<DuAnKeHoachTrungHanModel> listUpdate = Items.Where(x => x.IsModified && !x.IsNew && !x.IsClone).ToList();

                if (lstDuAn.Any(n => string.IsNullOrEmpty(n.IIdMaDonVi)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveDonViQuanLy);
                    return;
                }


                if (lstDuAn.Any(n => n.IIdNguonVonId == 0))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveNguonvon);
                    return;
                }

                if (lstClone.Any(n => string.IsNullOrEmpty(n.IIdMaDonVi)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveDonViQuanLy);
                    return;
                }

                if (lstDuAn.Any(n => (n.IIdLoaiCongTrinhId == null || n.IIdLoaiCongTrinhId == Guid.Empty)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveLoaiCongTrinh);
                    return;
                }

                if (lstDuAn != null && lstDuAn.Count > 0)
                {
                    lstDuAn = lstDuAn.GroupBy(x => x.SMaDuAn).Select(g => g.FirstOrDefault()).ToList();
                    var lstDataInsert = _mapper.Map<List<VdtDaDuAn>>(lstDuAn);
                    List<VdtDaNguonVon> lstNguonVonInsert = new List<VdtDaNguonVon>();

                    lstDataInsert.Select(
                        n =>
                        {
                            n.DDateCreate = DateTime.Now;
                            n.SUserCreate = _sessionService.Current.Principal;
                            n.STrangThaiDuAn = PROJECT_STATUS.KHOI_TAO;
                            n.IIdMaDonViThucHienDuAn = n.IIdMaDonViQuanLy;
                            var itemHangMuc = _vdtDmLoaiCongTrinhService.FindById(n.IIdLoaiCongTrinhId.HasValue ? n.IIdLoaiCongTrinhId.Value : Guid.NewGuid());
                            if (itemHangMuc != null)
                            {
                                n.STenHangMuc = string.Format("{0}-{1}", n.STenDuAn, itemHangMuc.STenLoaiCongTrinh);
                            }
                            else
                            {
                                n.STenHangMuc = n.STenDuAn;
                            }
                            var itemDonViQuanLy = _vdtDmDonViThucHienDuAnService.FindByMaDonVi(n.IIdMaDonViQuanLy);
                            if(itemDonViQuanLy != null)
                            {
                                n.IIdDonViThucHienDuAnId = itemDonViQuanLy.IIdDonVi;
                            }
                            var itemChuDauTu = _dmChuDauTuService.FindByMaDonVi(n.IIdMaChuDauTuId, _sessionService.Current.YearOfWork);
                            if (itemChuDauTu != null)
                            {
                                n.IIdChuDauTuId = itemChuDauTu.Id;
                            }
                            n.IIdMaDonViQuanLy = Model.IIdMaDonVi;
                            n.IIdDonViQuanLyId = Model.IIdDonViId;
                            n.FHanMucDauTu = (!n.FHanMucDauTu.HasValue || n.FHanMucDauTu == 0) ? lstClone.Where(x => x.SMaDuAn.Equals(n.SMaDuAn)) .Sum(x => x.FHanMucDauTu) : n.FHanMucDauTu;
                            n.SDiaDiem = (string.IsNullOrEmpty(n.SDiaDiem)) ? (lstClone.Count() > 0 ? lstClone.Where(x => x.SMaDuAn.Equals(n.SMaDuAn)).FirstOrDefault().SDiaDiem : string.Empty) : n.SDiaDiem;
                            n.SKhoiCong = (string.IsNullOrEmpty(n.SKhoiCong)) ? (lstClone.Count() > 0 ? lstClone.Where(x => x.SMaDuAn.Equals(n.SMaDuAn)).FirstOrDefault().SKhoiCong : string.Empty) : n.SKhoiCong;
                            n.SKetThuc = (string.IsNullOrEmpty(n.SKetThuc)) ? (lstClone.Count() > 0 ? lstClone.Where(x => x.SMaDuAn.Equals(n.SMaDuAn)).FirstOrDefault().SKetThuc : string.Empty) : n.SKetThuc;

                            if (!lstClone.Select(x => x.IIdDuAnId).ToList().Contains(n.Id))
                            {
                                var itemNguonVon = new VdtDaNguonVon();
                                itemNguonVon.Id = Guid.NewGuid();
                                itemNguonVon.IIdDuAn = n.Id;
                                itemNguonVon.FThanhTien = n.FHanMucDauTu;
                                itemNguonVon.IIdNguonVonId = n.IIdNguonVonId;
                                lstNguonVonInsert.Add(itemNguonVon);
                            }

                            return n;
                        }).ToList();

                    _duAnService.Insert(lstDataInsert);

                    foreach (var item in lstNguonVonInsert)
                    {
                        var items = _vdtDaNguonVonService.FindByNguonVon(item.IIdDuAn, item.IIdNguonVonId.Value).ToList();
                        if (items != null && items.Count == 0)
                        {
                            _vdtDaNguonVonService.Add(item);
                        }
                    }
                }
                

                List<DuAnKeHoachTrungHanModel> lstDuAnUpdate = listUpdate.Where(x => x.ILoaiDuAn.Equals(MediumTermPlanType.DUAN)).ToList();
                List<DuAnKeHoachTrungHanModel> lstHangMucUpdate = listUpdate.Where(x => x.ILoaiDuAn.Equals(MediumTermPlanType.HANG_MUC)).ToList();

                if (lstDuAnUpdate.Any(n => string.IsNullOrEmpty(n.IIdMaDonVi)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveDonViQuanLy);
                    return;
                }

                if (lstHangMucUpdate.Any(n => (n.IIdNguonVonId.Equals(0))))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveNguonvon);
                    return;
                }

                if (lstHangMucUpdate.Any(n => string.IsNullOrEmpty(n.IIdMaDonVi)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveDonViQuanLy);
                    return;
                }

                if (lstHangMucUpdate.Any(n => (n.IIdLoaiCongTrinhId == null || n.IIdLoaiCongTrinhId == Guid.Empty)))
                {
                    MessageBox.Show(Resources.MsgErrorDuAnNotHaveLoaiCongTrinh);
                    return;
                }

                if (lstDuAnUpdate != null && lstDuAnUpdate.Count > 0)
                {
                    foreach (var item2 in lstDuAnUpdate)
                    {
                        if (item2.IIdDuAnId.HasValue)
                        {
                            var itemDuAnHangMuc = lstClone.Where(x => x.IIdDuAnId == item2.IIdDuAnId);
                            VdtDaDuAn duAn = _duAnService.FindById(item2.IIdDuAnId.Value);
                            if (duAn != null)
                            {
                                duAn.STenDuAn = item2.STenDuAn;
                                duAn.SMaDuAn = item2.SMaDuAn;
                                duAn.IIdChuDauTuId = item2.IIdChuDauTuId;
                                duAn.SDiaDiem = item2.SDiaDiem;
                                duAn.SKhoiCong = item2.SKhoiCong;
                                duAn.SKetThuc = item2.SKetThuc;
                                //duAn.IIdLoaiCongTrinhId = item2.IIdLoaiCongTrinhId;
                                duAn.IIdMaDonViThucHienDuAn = item2.IIdMaDonVi;
                                duAn.IIdMaChuDauTuId = item2.IIdMaChuDauTu;
                                duAn.DDateUpdate = DateTime.Now;
                                duAn.SUserUpdate = _sessionService.Current.Principal;
                                duAn.FHanMucDauTu = item2.FHanMucDauTu;

                                if (!string.IsNullOrEmpty(item2.IIdMaDonVi))
                                {
                                    var itemDonViQuanLy = _vdtDmDonViThucHienDuAnService.FindByMaDonVi(item2.IIdMaDonVi);
                                    if (itemDonViQuanLy != null)
                                    {
                                        duAn.IIdDonViThucHienDuAnId = itemDonViQuanLy.IIdDonVi;
                                    }
                                }

                                if (!string.IsNullOrEmpty(item2.IIdMaChuDauTu))
                                {
                                    var itemChuDauTu = _dmChuDauTuService.FindByMaDonVi(item2.IIdMaChuDauTu, _sessionService.Current.YearOfWork);
                                    if (itemChuDauTu != null)
                                    {
                                        duAn.IIdChuDauTuId = itemChuDauTu.Id;
                                    }
                                }
                                duAn.IIdMaDonViQuanLy = Model.IIdMaDonVi;
                                duAn.IIdDonViQuanLyId = Model.IIdDonViId;                                
                                
                                // nếu không thêm chi tiết, chỉ sửa dòng dự án thì cần update lại dòng hạng mục đã insert mặc định
                                if (itemDuAnHangMuc == null || itemDuAnHangMuc.Count() == 0)
                                {
                                    if (duAn.IIdLoaiCongTrinhId != null && duAn.IIdLoaiCongTrinhId != Guid.Empty)
                                    {
                                        var hangMuc = _vdtDuAnHangMucService.FindByIdDuAn(item2.IIdDuAnId.Value);
                                        var lct = _vdtDmLoaiCongTrinhService.FindById(item2.IIdLoaiCongTrinhId.HasValue ? item2.IIdLoaiCongTrinhId.Value : Guid.NewGuid());

                                        hangMuc.FirstOrDefault().STenHangMuc = string.Format("{0}-{1}", item2.STenDuAn, lct.STenLoaiCongTrinh);
                                        hangMuc.FirstOrDefault().IdLoaiCongTrinh = item2.IIdLoaiCongTrinhId;
                                        hangMuc.FirstOrDefault().iID_NguonVonID = item2.IIdNguonVonId;
                                        hangMuc.FirstOrDefault().fHanMucDauTu = item2.FHanMucDauTu;
                                        _vdtDuAnHangMucService.Update(hangMuc.FirstOrDefault());
                                    }
                                    else
                                    {
                                        var itemDuAnHangMucNew = new VdtDaDuAnHangMuc();
                                        itemDuAnHangMucNew.IIdDuAnId = item2.IIdDuAnId;
                                        itemDuAnHangMucNew.IdLoaiCongTrinh = item2.IIdLoaiCongTrinhId;

                                        var lct = _vdtDmLoaiCongTrinhService.FindById(item2.IIdLoaiCongTrinhId.HasValue ? item2.IIdLoaiCongTrinhId.Value : Guid.NewGuid());
                                        itemDuAnHangMucNew.STenHangMuc = string.Format("{0}-{1}", item2.STenDuAn, lct.STenLoaiCongTrinh);
                                        itemDuAnHangMucNew.fHanMucDauTu = item2.FHanMucDauTu;
                                        itemDuAnHangMucNew.iID_NguonVonID = item2.IIdNguonVonId;
                                        _vdtDuAnHangMucService.Add(itemDuAnHangMucNew);
                                    }
                                }
                                // nếu thêm chi tiết cho dự án, cần set IIdLoaiCongTrinhId của dự án = null và hủy dòng hạng mục, nguồn vốn insert mặc định
                                else
                                {
                                    duAn.IIdLoaiCongTrinhId = null;
                                    _vdtDuAnHangMucService.DeleteByDuAnId(item2.IIdDuAnId.Value);
                                    _vdtDaNguonVonService.DeleteByIdDuAn(item2.IIdDuAnId.Value);
                                }

                                duAn.IIdLoaiCongTrinhId = item2.IIdLoaiCongTrinhId;

                                _duAnService.Update(duAn);
                            }
                        }

                        if (Items.Where(x => x.IIdDuAnId == item2.IIdDuAnId).Count().Equals(1) && item2.IIdDuAnId.HasValue)
                        {
                            List<VdtDaNguonVon> lstBudgetDelete = _vdtDaNguonVonService.FindByIdDuAn(item2.IIdDuAnId.Value).ToList();
                            foreach (var itemNv in lstBudgetDelete)
                            {
                                _vdtDaNguonVonService.Delete(itemNv.Id);
                            }

                            VdtDaNguonVon itemBudgetNew = new VdtDaNguonVon();
                            itemBudgetNew.Id = Guid.NewGuid();
                            itemBudgetNew.IIdDuAn = item2.IIdDuAnId.Value;
                            itemBudgetNew.IIdNguonVonId = item2.IIdNguonVonId;
                            itemBudgetNew.FThanhTien = item2.FHanMucDauTu;

                            _vdtDaNguonVonService.Add(itemBudgetNew);
                        }
                    }
                }

                List<Guid?> lstIdDuAn = lstHangMucUpdate.Select(x => x.IIdDuAnId).ToList();
                List<VdtDaNguonVon> listDaNguonVonDelete = _vdtDaNguonVonService.FindByIdDuAn(lstIdDuAn).ToList();

                if (listDaNguonVonDelete != null && listDaNguonVonDelete.Count > 0)
                {
                    foreach (var item in listDaNguonVonDelete)
                    {
                        _vdtDaNguonVonService.Delete(item.Id);
                    }
                }

                List<DuAnKeHoachTrungHanModel> listNguonVonModified = Items.Where(x => lstIdDuAn.Contains(x.IIdDuAnId) && x.ILoaiDuAn.Equals(MediumTermPlanType.HANG_MUC)).ToList();
                List<VdtDaNguonVonModel> lstNguonVonModel = _mapper.Map<List<VdtDaNguonVonModel>>(listNguonVonModified);
                List<VdtDaNguonVon> lstNguonVonModified = _mapper.Map<List<VdtDaNguonVon>>(lstNguonVonModel);

                lstNguonVonModified = lstNguonVonModified.GroupBy(x => new { x.IIdDuAn, x.IIdNguonVonId }).Select(x => new VdtDaNguonVon()
                {
                    IIdDuAn = x.Key.IIdDuAn,
                    IIdNguonVonId = x.Key.IIdNguonVonId,
                    FThanhTien = x.Sum(grp => grp.FThanhTien)
                }).ToList();
                lstNguonVonModified = lstNguonVonModified.Select(x => { x.Id = Guid.NewGuid(); return x; }).ToList();

                if (lstNguonVonModified != null && lstNguonVonModified.Count > 0)
                {
                    foreach (var item in lstNguonVonModified)
                    {
                        var items = _vdtDaNguonVonService.FindByNguonVon(item.IIdDuAn, item.IIdNguonVonId.Value).ToList();
                        if (items != null && items.Count == 0)
                        {
                            _vdtDaNguonVonService.Add(item);
                        }
                    }
                }

                if (lstHangMucUpdate != null && lstHangMucUpdate.Count > 0)
                {
                    foreach (var item2 in lstHangMucUpdate)
                    {
                        if (item2.IIdDuAnHangMucId.HasValue && item2.ILoaiDuAn.Equals(MediumTermPlanType.HANG_MUC))
                        {
                            VdtDaDuAnHangMuc duAnHangMuc = _vdtDuAnHangMucService.FindById(item2.IIdDuAnHangMucId.Value);
                            if (duAnHangMuc != null)
                            {
                                duAnHangMuc.IIdDuAnId = item2.IIdDuAnId;
                                duAnHangMuc.iID_NguonVonID = item2.IIdNguonVonId;
                                duAnHangMuc.fHanMucDauTu = item2.FHanMucDauTu;
                                duAnHangMuc.IIdParentId = item2.IIdParentHangMuc;
                                duAnHangMuc.IdLoaiCongTrinh = item2.IIdLoaiCongTrinhId;
                                _vdtDuAnHangMucService.Update(duAnHangMuc);
                            }
                        }
                        else if (!item2.IIdDuAnHangMucId.HasValue && item2.ILoaiDuAn.Equals(MediumTermPlanType.HANG_MUC))
                        {
                            VdtDaDuAnHangMuc entity = new VdtDaDuAnHangMuc();
                            entity.Id = Guid.NewGuid();
                            entity.IIdDuAnId = item2.IIdDuAnId;
                            entity.iID_NguonVonID = item2.IIdNguonVonId;
                            entity.fHanMucDauTu = item2.FHanMucDauTu;
                            entity.IIdParentId = item2.IIdParentHangMuc;
                            entity.IdLoaiCongTrinh = item2.IIdLoaiCongTrinhId;
                            _vdtDuAnHangMucService.Add(entity);
                        }
                    }
                }

                if (lstClone != null && lstClone.Count > 0)
                {
                    var lstDataInsert = _mapper.Map<List<VdtDaDuAn>>(lstClone);
                    lstDataInsert = lstDataInsert.Select(
                        n =>
                        {
                            if (string.IsNullOrEmpty(n.IIdMaDonViQuanLy) && lstDuAn.Count() > 0)
                            {
                                var itemMaDonViQuanLyDuAn = lstDuAn.Where(x => x.SMaDuAn.Equals(n.SMaDuAn)).FirstOrDefault();
                                if (itemMaDonViQuanLyDuAn != null)
                                {
                                    n.IIdMaDonViThucHienDuAn = itemMaDonViQuanLyDuAn.IIdMaDonVi;
                                }
                            }

                            var itemDonViQuanLy = _vdtDmDonViThucHienDuAnService.FindByMaDonVi(n.IIdMaDonViQuanLy);
                            if (itemDonViQuanLy != null)
                            {
                                n.IIdDonViQuanLyId = itemDonViQuanLy.IIdDonVi;
                            }

                            n.IIdMaDonViQuanLy = Model.IIdMaDonVi;
                            n.IIdDonViQuanLyId = Model.IIdDonViId;
                            n.DDateCreate = DateTime.Now;
                            n.SUserCreate = _sessionService.Current.Principal;
                            var itemHangMuc = _vdtDmLoaiCongTrinhService.FindById(n.IIdLoaiCongTrinhId.HasValue ? n.IIdLoaiCongTrinhId.Value : Guid.NewGuid());
                            if (itemHangMuc != null)
                            {
                                n.STenHangMuc = string.Format("{0}-{1}", n.STenDuAn, itemHangMuc.STenLoaiCongTrinh);
                            }
                            else
                            {
                                n.STenHangMuc = n.STenDuAn;
                            }

                            return n;
                        }).ToList();

                    if (lstDataInsert != null && lstDataInsert.Count > 0)
                    {
                        foreach (var item in lstDataInsert)
                        {
                            var items = _duAnService.FindById(item.Id);
                            if (items == null)
                            {
                                _duAnService.Add(item);
                            }
                        }
                    }

                    List<VdtDaNguonVonModel> lstNguonVon = _mapper.Map<List<VdtDaNguonVonModel>>(lstDataInsert);
                    List<VdtDaNguonVon> lstInsert = _mapper.Map<List<VdtDaNguonVon>>(lstNguonVon);

                    lstInsert = lstInsert.GroupBy(x => new { x.IIdDuAn, x.IIdNguonVonId }).Select(x => new VdtDaNguonVon()
                    {
                        IIdDuAn = x.Key.IIdDuAn,
                        IIdNguonVonId = x.Key.IIdNguonVonId,
                        FThanhTien = x.Sum(grp => grp.FThanhTien)
                    }).ToList();
                    lstInsert = lstInsert.Select(x => { x.Id = Guid.NewGuid(); return x; }).ToList();

                    foreach (var item in lstInsert)
                    {
                        var items = _vdtDaNguonVonService.FindByNguonVon(item.IIdDuAn, item.IIdNguonVonId.Value).ToList();
                        if (items != null && items.Count == 0)
                        {
                            _vdtDaNguonVonService.Add(item);
                        }
                    }

                    List<VdtDaDuAnHangMuc> lstHangMuc = _mapper.Map<List<VdtDaDuAnHangMuc>>(lstDataInsert);

                    int indexHgMc = _vdtDuAnHangMucService.FindNextSoChungTuIndex();

                    int int_item = indexHgMc;
                    if (lstHangMuc != null && lstHangMuc.Count > 0)
                    {
                        foreach (var item in lstHangMuc)
                        {
                            var id = Guid.NewGuid();
                            item.Id = id;
                            item.SMaHangMuc = string.Format("{0}{1}", item.SMaDuAn.Substring(item.SMaDuAn.Length - 3), int_item.ToString("D3"));
                            item.MaOrDer = item.SMaHangMuc;
                            item.indexMaHangMuc = int_item;
                            int_item += 1;
                        }

                        foreach (var item in lstHangMuc)
                        {
                            var items = _vdtDuAnHangMucService.FindByDuAnHangMuc(item.IIdDuAnId.Value, item.iID_NguonVonID.Value, item.IdLoaiCongTrinh.Value).ToList();
                            if (items != null && items.Count == 0)
                            {
                                _vdtDuAnHangMucService.Add(item);
                            }
                        }
                    }
                }

                foreach (var item in lstDuAn)
                {
                    var itemDuAnHangMuc = lstClone.Where(x => x.IIdDuAnId == item.IIdDuAnId);

                    //nếu không thêm chi tiết, cần insert 1 dòng hạng mục mặc định
                    if( itemDuAnHangMuc == null || itemDuAnHangMuc.Count() == 0)
                    {
                        if(item.IIdLoaiCongTrinhId != null)
                        {
                            var itemDuAnHangMucNew = new VdtDaDuAnHangMuc();
                            itemDuAnHangMucNew.IIdDuAnId = item.IIdDuAnId;
                            itemDuAnHangMucNew.IdLoaiCongTrinh = item.IIdLoaiCongTrinhId;

                            var lct = _vdtDmLoaiCongTrinhService.FindById(item.IIdLoaiCongTrinhId.HasValue ? item.IIdLoaiCongTrinhId.Value : Guid.NewGuid());
                            itemDuAnHangMucNew.STenHangMuc = string.Format("{0}-{1}", item.STenDuAn, lct.STenLoaiCongTrinh);
                            itemDuAnHangMucNew.fHanMucDauTu = item.FHanMucDauTu;
                            itemDuAnHangMucNew.iID_NguonVonID = item.IIdNguonVonId;
                            _vdtDuAnHangMucService.Add(itemDuAnHangMucNew);
                        }
                        
                    }
                    //nếu thêm chi tiết, cần set IIdLoaiCongTrinhId của dự án = null
                    else
                    {
                        var itemDuAn = _duAnService.FindById(item.IIdDuAnId.Value);
                        if(itemDuAn != null)
                        {
                            itemDuAn.IIdLoaiCongTrinhId = null;
                            _duAnService.Update(itemDuAn);
                        }
                    }
                }                

                MessageBox.Show(Resources.MsgSaveDone);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd(object param)
        {
            try
            {
                var recordType = (MediumTermModifyType)((int)param);

                if (recordType.Equals(MediumTermModifyType.NEW))
                {
                    DuAnKeHoachTrungHanModel newItem = new DuAnKeHoachTrungHanModel();
                    newItem.DrpNguonVon = _drpNguonNganSach;
                    newItem.STenLoaiDuAn = LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI);
                    //newItem.IIdChuDauTuId = null;
                    newItem.IIdMaChuDauTu = null;
                    newItem.IsClone = false;
                    newItem.IsModified = true;
                    newItem.IsEnableDd = false;
                    newItem.IsHangMuc = false;
                    Guid idDuAn = Guid.NewGuid();
                    newItem.IIdDuAnId = idDuAn;
                    newItem.Id = idDuAn;
                    newItem.IsDuAnOffer = false;
                    newItem.PropertyChanged += DetailModel_PropertyChanged;

                    string sMaChuDauTu = "XXX";
                    if (newItem.IIdChuDauTuId != null)
                    {
                        var items = _dmChuDauTuService.FindById(newItem.IIdChuDauTuId.Value);
                        if (items != null)
                        {
                            sMaChuDauTu = items.IIDMaDonVi;
                        }
                    }

                    int indexGenerate = _duAnService.FindNextSoChungTuIndex();
                    if (indexSMaDuAn >= indexGenerate)
                    {
                        indexGenerate = indexSMaDuAn + 1;
                    }
                    indexSMaDuAn = indexGenerate;

                    string sGenerate = indexGenerate.ToString("D4");
                    string sMaDuAn = string.Format("{0}-{1}-{2}", Model.IIdMaDonVi, sMaChuDauTu, sGenerate);
                    newItem.SMaDuAn = sMaDuAn;
                    newItem.IMaDuAnIndex = indexGenerate;
                    newItem.IsNew = true;
                    newItem.ILoaiDuAn = MediumTermPlanType.DUAN;

                    Items.Insert(0, newItem);
                }

                if (SelectedItem != null)
                {

                    if (recordType.Equals(MediumTermModifyType.CLONE))
                    {
                        int currentRow = Items.IndexOf(SelectedItem);
                        DuAnKeHoachTrungHanModel newItem = new DuAnKeHoachTrungHanModel();
                        newItem.IsChecked = SelectedItem.IsChecked;
                        newItem.SMaDuAn = SelectedItem.SMaDuAn;
                        newItem.STenDuAn = SelectedItem.STenDuAn;
                        newItem.STenChuDauTu = SelectedItem.STenChuDauTu;
                        newItem.SDiaDiem = SelectedItem.SDiaDiem;
                        newItem.SKhoiCong = SelectedItem.SKhoiCong;
                        newItem.SKetThuc = SelectedItem.SKetThuc;
                        newItem.IIdNguonVonId = 0;
                        newItem.IIdLoaiCongTrinhId = null;
                        newItem.FHanMucDauTu = SelectedItem.FHanMucDauTu;
                        newItem.IIdChuDauTuId = SelectedItem.IIdChuDauTuId;
                        newItem.IIdMaChuDauTu = SelectedItem.IIdMaChuDauTu;
                        newItem.IMaDuAnIndex = SelectedItem.IMaDuAnIndex;
                        newItem.IIdDonViId = SelectedItem.IIdDonViId;
                        newItem.IIdMaDonVi = SelectedItem.IIdMaDonVi;
                        newItem.IsClone = true;
                        newItem.IIdDuAnId = SelectedItem.IIdDuAnId;
                        newItem.IsModified = true;
                        newItem.IsEnableDd = false;
                        newItem.IsHangMuc = false;
                        newItem.IsNew = false;
                        newItem.ILoaiDuAn = MediumTermPlanType.HANG_MUC;
                        newItem.IdDiscern = Guid.NewGuid();
                        newItem.IsDuAnOffer = false;
                        newItem.PropertyChanged += DetailModel_PropertyChanged;

                        SelectedItem.IsEnableChk = false;
                        SelectedItem.IIdLoaiCongTrinhId = null;
                        SelectedItem.IIdNguonVonId = 0;
                        SelectedItem.FHanMucDauTu = 0;

                        Items.Insert(currentRow + 1, newItem);
                    }
                }
                else if (recordType.Equals(MediumTermModifyType.CLONE))
                {
                    MessageBox.Show(Resources.MsgErrorVocherDetails);
                    return;
                }

                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ValidateDetailProject()
        {
            try
            {
                List<DuAnKeHoachTrungHanModel> listPartern = new List<DuAnKeHoachTrungHanModel>();

                if (SelectedItem.IIdDuAnHangMucId.HasValue)
                {
                    listPartern = Items.Where(x => x.IIdDuAnId == SelectedItem.IIdDuAnId).ToList();
                }
                else
                {
                    listPartern = Items.Where(x => x.IIdDuAnId == SelectedItem.IIdDuAnId && x.IsClone).ToList();
                }

                if (SelectedItem.IdDiscern.HasValue)
                {
                    listPartern = listPartern.Where(x => !(x.IdDiscern == SelectedItem.IdDiscern)).ToList();
                }
                else if (SelectedItem.IIdDuAnHangMucId.HasValue)
                {
                    listPartern = listPartern.Where(x => !(x.IIdDuAnHangMucId == SelectedItem.IIdDuAnHangMucId)).ToList();
                }

                foreach (var item in listPartern)
                {
                    if (SelectedItem.IIdLoaiCongTrinhId.HasValue && !SelectedItem.IIdNguonVonId.Equals(0))
                    {
                        if (item.IIdLoaiCongTrinhId == SelectedItem.IIdLoaiCongTrinhId && item.IIdNguonVonId == SelectedItem.IIdNguonVonId)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return false;
            }
        }
        #endregion

        #region Helper
        private bool DuAnFilter(object obj)
        {
            if (!(obj is DuAnKeHoachTrungHanModel temp)) return true;
            var bCondition = true;

            if (DrpLoaiDuAnSelected != null)
            {
                bCondition &= (temp.ILoaiDuAn.ToString() == DrpLoaiDuAnSelected.ValueItem);
            }
            if (DrpNguonNganSachSelected != null)
            {
                bCondition &= (temp.IIdNguonVonId.ToString() == DrpNguonNganSachSelected.ValueItem);
            }
            if (!string.IsNullOrEmpty(TxtName))
            {
                bCondition &= !string.IsNullOrEmpty(temp.STenDuAn) && (temp.STenDuAn.ToLower().Contains(TxtName.ToLower()));
            }
            if (!string.IsNullOrEmpty(TxtMaDuAn))
            {
                bCondition &= !string.IsNullOrEmpty(temp.SMaDuAn) && (temp.SMaDuAn.ToLower().Contains(TxtMaDuAn.ToLower()));
            }
            if (_drpDonViSelected != null)
            {
                bCondition &= _drpDonViSelected.ValueItem.Equals(temp.IIdMaDonVi);
            }
            if (_drpDonViQuanLyDuAnSelected != null)
            {
                bCondition &= _drpDonViQuanLyDuAnSelected.ValueItem.Equals(temp.IIdMaDonVi);
            }
            if(DrpChuDauTuSelected != null)
            {
                bCondition &= _drpChuDauTuSelected.ValueItem.Equals(temp.IIdMaChuDauTu);
            }
            temp.IsFilter = bCondition;

            return bCondition;
        }

        public override void OnClose(object obj)
        {
            var window = obj as Window;
            window.Close();
        }
        #endregion
    }
}
