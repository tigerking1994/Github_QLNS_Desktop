using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate
{
    public class PhanBoDuToanThuMuaBHYTDialogViewModel : DialogViewModelBase<BhPbdttmBHYTModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IPbdttmBHYTService _pbdttmBHYTService;
        private readonly IPbdttmBHYTChiTietService _pbdttmBHYTChiTietService;
        private readonly IPbdttmMapBHYTService _pbdttmMapBHYTService;
        private readonly IBhDtTmBHYTTNService _bhDtTmBHYTTNService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;
        private ICollectionView _dataDotPhanBoView;
        private bool _isNamLuyKe;
        private SessionInfo _sessionInfo;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler RefreshVoucherEvent;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThuMuaBHYT.PhanBoDuToanThuMuaBHYTDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI ĐỢT PHÂN BỔ" : "CẬP NHẬT ĐỢT PHÂN BỔ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đợt phân bổ dự toán thu BHYT thân nhân" : "Cập nhật đợt phân bổ dự toán thu BHYT thân nhân";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        bool bDeleteDetail { get; set; }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;

        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        private ObservableCollection<CheckBoxItem> _dataUnit;
        public ObservableCollection<CheckBoxItem> DataUnit
        {
            get => _dataUnit;
            set => SetProperty(ref _dataUnit, value);
        }

        public string _selectedCountLNS;
        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter && x.IsSelected).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count(item => item.IsSelected) : 0;
                return $"CHỌN LNS ({totalSelected}/{totalCount})";
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS?.Where(x => x.IsFilter).All(item => item.IsSelected) ?? false);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Where(x => x.IsFilter).Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
                _dataLNSView.Refresh();
            }
        }

        public string SelectedCountUnit
        {
            get
            {
                int totalCount = DataUnit != null ? DataUnit.Count : 0;
                int totalSelected = DataUnit != null ? DataUnit.Count(item => item.IsChecked) : 0;
                return $"CHỌN ĐƠN VỊ ({totalSelected}/{totalCount})";
            }
        }

        private bool _selectAllUnit;
        public bool SelectAllUnit
        {
            get => (DataUnit == null || !DataUnit.Any()) ? false : DataUnit.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllUnit, value);
                if (DataUnit != null)
                {
                    DataUnit.Select(c => { c.IsChecked = _selectAllUnit; return c; }).ToList();
                }
            }
        }

        public string LblPhanBo { get; set; }

        private ObservableCollection<BhDtTmBHYTTNModel> _dataDot;
        public ObservableCollection<BhDtTmBHYTTNModel> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ObservableCollection<BhDtTmBHYTTNModel> _initDataDot;
        public ObservableCollection<BhDtTmBHYTTNModel> InitDataDot
        {
            get => _initDataDot;
            set => SetProperty(ref _initDataDot, value);
        }

        private bool _selectAllDot;
        public bool SelectAllDot
        {
            get => (DataDot == null || !DataDot.Any()) ? false : DataDot.Where(x => x.IsFilter).All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDot, value);
                if (DataDot != null)
                {
                    DataDot.Where(x => x.IsFilter).Select(c => { c.IsChecked = _selectAllDot; return c; }).ToList();
                }
            }
        }

        public string SelectedCountDot
        {
            get
            {
                int totalCount = DataDot != null ? DataDot.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataDot != null ? DataDot.Where(x => x.IsFilter).Count(item => item.IsChecked) : 0;
                return $"{LblPhanBo} ({totalSelected}/{totalCount})";
            }
        }

        private string _searchUnit;
        public string SearchUnit
        {
            get => _searchUnit;
            set
            {
                SetProperty(ref _searchUnit, value);
                _dataUnitView.Refresh();
            }
        }

        private string _searchDotPhanBo;
        public string SearchDotPhanBo
        {
            get => _searchDotPhanBo;
            set
            {
                SetProperty(ref _searchDotPhanBo, value);
                _dataDotPhanBoView.Refresh();
            }
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                {
                    if (Model != null && Guid.Empty.Equals(Model.Id))
                    {
                        LoadChungTuIndex();
                    }
                    LoadAgencies();
                    LoadLNS();
                    LoadDataDotNhanCustom();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxExpenseTypeSelected;
        public ComboboxItem CbxExpenseTypeSelected
        {
            get => _cbxExpenseTypeSelected;
            set
            {
                SetProperty(ref _cbxExpenseTypeSelected, value);
            }
        }

        private ObservableCollection<ComboboxItem> _cbxExpenseType;
        public ObservableCollection<ComboboxItem> CbxExpenseType
        {
            get => _cbxExpenseType;
            set => SetProperty(ref _cbxExpenseType, value);
        }


        private ComboboxItem _cbxBudgetTypeSelected;
        public ComboboxItem CbxBudgetTypeSelected
        {
            get => _cbxBudgetTypeSelected;
            set
            {
                SetProperty(ref _cbxBudgetTypeSelected, value);
                if (_cbxBudgetTypeSelected != null)
                {
                    LoadAgencies();
                    LoadLNS();
                    //LoadDataDotNhan();
                    LoadDataDotNhanCustom();
                    OnPropertyChanged(nameof(IsAdjusted));
                    SelectAllLNS = true;
                }
            }
        }

        public bool IsAdjusted
        {
            get
            {
                if (_cbxBudgetTypeSelected != null)
                {
                    var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                    if (BudgetType.ADJUSTED.Equals(budgetType))
                        return true;
                }
                return false;
            }
        }

        public bool IsCheckDot { get; set; } = false;

        private bool _isInitilizationFirstTime;

        private ObservableCollection<ComboboxItem> _cbxBudgetType;
        public ObservableCollection<ComboboxItem> CbxBudgetType
        {
            get => _cbxBudgetType;
            set => SetProperty(ref _cbxBudgetType, value);
        }

        List<Guid> selecteds = new List<Guid>();
        DateTime? InitDNgayChungTu { get; set; }
        DateTime? InitDNgayQuyetDinh { get; set; }

        public PhanBoDuToanThuMuaBHYTDialogViewModel(
            IMapper mapper,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IPbdttmBHYTService pbdttmBHYTService,
            IPbdttmBHYTChiTietService pbdttmBHYTChiTietService,
            IPbdttmMapBHYTService pbdttmMapBHYTService,
            IBhDtTmBHYTTNService bhDtTmBHYTTNService,
            IBhDtTmBHYTTNChiTietService bhDtTmBHYTTNChiTietService,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            IDanhMucService danhMucService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _nSDonViService = nSDonViService;
            _danhMucService = danhMucService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _pbdttmBHYTService = pbdttmBHYTService;
            _pbdttmBHYTChiTietService = pbdttmBHYTChiTietService;
            _pbdttmMapBHYTService = pbdttmMapBHYTService;
            _bhDtTmBHYTTNService = bhDtTmBHYTTNService;

        }

        public override void Init()
        {
            _isInitilizationFirstTime = false;
            bDeleteDetail = false;
            IsSaveData = true;
            DataDot = new ObservableCollection<BhDtTmBHYTTNModel>();
            _sessionInfo = _sessionService.Current;
            LoadNamLuyKe();
            LoadBudgetType();
            LoadAgencies();
            LoadLNS();
            LoadData();

        }
        private void LoadNamLuyKe()
        {
            DanhMuc dmNamLuyKe = _danhMucService.FindByCode(MaDanhMuc.NAM_LUY_KE, _sessionService.Current.YearOfWork);
            if (dmNamLuyKe != null)
                bool.TryParse(dmNamLuyKe.SGiaTri, out _isNamLuyKe);
            else _isNamLuyKe = false;
        }

        private void LoadBudgetType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Đầu năm", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"}
            };

            CbxBudgetType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty)
            {
                CbxBudgetTypeSelected = CbxBudgetType.Single(item => item.ValueItem.Equals(Model.ILoaiDuToan.ToString()));
            }
            else
            {
                CbxBudgetTypeSelected = CbxBudgetType.First();
            }
        }

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                // Add
                Model = new BhPbdttmBHYTModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayQuyetDinh = null,
                    SSoQuyetDinh = string.Empty
                };
                LoadChungTuIndex();
                Model.DNgayChungTu = DateTime.Now;
                Model.DNgayQuyetDinh = DateTime.Now;
                Model.PropertyChanged += DialogModel_PropertyChanged;
            }
            else
            {
                //Loại điều chỉnh
                if (Model.ILoaiDuToan != (int)BudgetType.ADJUSTED)
                {
                    Description = "Điều chỉnh chứng từ phân bổ thu mua BHYT";
                }
                else
                {
                    DataDot.ForAll(c => c.IsEnabled = false);
                }
                LoadDataDotNhanCustom();
            }

        }
        private void DialogModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Model.DNgayChungTu) && Model.DNgayQuyetDinh == null && _cbxBudgetTypeSelected != null)
            {
                LoadDataDotNhanCustom();
            }
            else if (args.PropertyName == nameof(Model.DNgayQuyetDinh) && _cbxBudgetTypeSelected != null)
            {
                LoadDataDotNhanCustom();
            }
            DataChangedEventHandler handler = RefreshVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }
        private void LoadChungTuIndex()
        {
            int soChungTuIndex = _pbdttmBHYTService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            Model.SSoChungTu = "PB-" + soChungTuIndex.ToString("D3");
            
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var listNsMucLucNganSach = new List<BhDmMucLucNganSach>();
            List<BhDmMucLucNganSachModel> listNsMucLucNganSachModel = new List<BhDmMucLucNganSachModel>();
            if (_cbxBudgetTypeSelected != null)
            {
                var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate = predicate.And(x => x.INamLamViec == yearOfWork);

                listNsMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate).Where(x => x.SLNS.StartsWith("903")).OrderBy(x => x.SXauNoiMa).ToList();
                listNsMucLucNganSachModel = _mapper.Map(listNsMucLucNganSach, listNsMucLucNganSachModel);
            }
            DataLNS = new ObservableCollection<BhDmMucLucNganSachModel>(listNsMucLucNganSachModel);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<string> sLnsHasData = Model.SDSLNS.Split(",").Distinct().ToList();
                DataLNS.Where(x => sLnsHasData.Contains(x.SLNS)).Select(x => { x.IsSelected = true; return x; }).ToList();
            }
            LoadLNSPropertyChanged();
        }

        private void LoadLNSPropertyChanged()
        {
            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;
            OnPropertyChanged(nameof(DataLNS));
            OnPropertyChanged(nameof(SelectAllLNS));
            OnPropertyChanged(nameof(SelectedCountLNS));

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                            OnPropertyChanged(nameof(SelectedCountDot));
                            if (!IsCheckDot && _dataDotPhanBoView != null)
                            {
                                _dataDotPhanBoView.Refresh();
                            }
                        }
                    };
                }
                SelectAllLNS = true;
            }

        }

        private void LoadAgencies()
        {
            var listUnit = new List<DonVi>();
            if (_cbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (!BudgetType.ADJUSTED.Equals(budgetType))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai));
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
                    if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
                    {
                        predicate = predicate.And(x => true.Equals(x.BCoNSNganh));
                    }
                    // remove 999 hard code
                    predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
                    listUnit = _nSDonViService.FindByCondition(predicate).OrderBy(n => n.IIDMaDonVi).ToList();
                }
            }


            DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<string> lstDonViHasData = Model.SDS_IDMaDonVi.Split(",").ToList();
                DataUnit.Where(x => lstDonViHasData.Contains(x.ValueItem)).Select(x => { x.IsChecked = true; return x; }).ToList();
            }

            _dataUnitView = CollectionViewSource.GetDefaultView(DataUnit);
            _dataUnitView.Filter = ListUnitFilter;
            OnPropertyChanged(nameof(DataUnit));
            OnPropertyChanged(nameof(SelectAllUnit));
            OnPropertyChanged(nameof(SelectedCountUnit));

            if (_dataUnit != null && _dataUnit.Count > 0)
            {
                foreach (var model in _dataUnit)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectAllUnit));
                            OnPropertyChanged(nameof(SelectedCountUnit));
                        }
                    };
                }
            }
        }

        private void LoadDataDotNhanCustom()
        {
            var predicate = PredicateBuilder.True<BhPbdttmBHYT>();
            IEnumerable<BhPbdttmBHYT> listChungTu = new List<BhPbdttmBHYT>();
            DataDot = new ObservableCollection<BhDtTmBHYTTNModel>();
            int yearOfWork = _sessionInfo.YearOfWork;
            DateTime date = Model.DNgayQuyetDinh ?? DateTime.Now;

            if (CbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(CbxBudgetTypeSelected.ValueItem));
                if (BudgetType.ADJUSTED.Equals(budgetType))
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    LblPhanBo = "ĐỢT ĐÃ PHÂN BỔ";

                    predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                    predicate = predicate.And(x => x.ILoaiDuToan != (int)BudgetType.ADJUSTED);
                    predicate = predicate.And(x => x.FDuToan > 0);
                    predicate = predicate.And(x => (x.DNgayQuyetDinh.Date <= date) ||  x.DNgayChungTu.Date <= date);

                    listChungTu = _pbdttmBHYTService.FindByCondition(predicate);

                    if(Guid.Empty.Equals(Model.Id))
                    {
                        List<BhPbdttmMapBHYT> dtNhanPhanBoMaps = _pbdttmMapBHYTService.FindByListIdNhanDuToan(listChungTu.Select(x => x.Id.ToString()).ToList()).ToList();
                        listChungTu = listChungTu.Where(x => !dtNhanPhanBoMaps.Select(x => x.IID_DTTM_BHYT_NhanPhanBo).Contains(x.Id)).ToList();
                    }
                    else
                    {
                        listChungTu = listChungTu.Where(x => x.Id != Model.Id).ToList();

                    }
                    List<BhDtTmBHYTTNModel> lstDttm = new List<BhDtTmBHYTTNModel>();
                    lstDttm = listChungTu.Select(x => new BhDtTmBHYTTNModel
                    {
                        Id = x.Id,
                        SSoChungTu = x.SSoChungTu,
                        DNgayChungTu = x.DNgayChungTu,
                        SSoQuyetDinh = x.SSoQuyetDinh,
                        DNgayQuyetDinh = x.DNgayQuyetDinh,
                        FSoPhanBo = x.FDuToan,
                        SDSLNS = x.SDSLNS,
                        IsAdjusted = true
                    }).ToList();
                    DataDot = _mapper.Map<ObservableCollection<BhDtTmBHYTTNModel>>(lstDttm);
                }
                else
                {
                    LblPhanBo = "ĐỢT NHẬN PHÂN BỔ";
                    int iBudgetType = (int.Parse(_cbxBudgetTypeSelected.ValueItem));
                    List<BhDtTmBHYTTNQuery> listDotNhanQuery = new List<BhDtTmBHYTTNQuery>();
                    listDotNhanQuery = _bhDtTmBHYTTNService.GetDanhSachDotNhanPhanBo(yearOfWork, date, iBudgetType).ToList();

                    if (_isNamLuyKe && listDotNhanQuery.Count > 0)
                    {
                        
                    }
                    else
                    {
                        DataDot = _mapper.Map<ObservableCollection<BhDtTmBHYTTNModel>>(listDotNhanQuery);
                    }
                }
                if (Model != null && Model.Id != Guid.Empty)
                {
                    var predicate_map = PredicateBuilder.True<BhPbdttmMapBHYT>();
                    predicate_map = predicate_map.And(x => x.IID_DTTM_BHYT_PhanBo == Model.Id);

                    List<Guid> lstChungTuMap = new List<Guid>();
                    lstChungTuMap = _pbdttmMapBHYTService.FindByCondition(predicate_map).Select(x => x.IID_DTTM_BHYT_NhanPhanBo).ToList();
                    DataDot.Where(x => lstChungTuMap.Contains(x.Id)).ForAll(x => x.IsChecked = true);
                    InitDataDot = DataDot;
                }
                _dataDotPhanBoView = CollectionViewSource.GetDefaultView(DataDot);
                _dataDotPhanBoView.Filter = ListDataDotPhanBoFilter;

                OnPropertyChanged(nameof(DataDot));
                OnPropertyChanged(nameof(SelectedCountDot));
                OnPropertyChanged(nameof(SelectAllDot));
                DataDot_PropertyChanged();
            }

        }

        private bool ListDataDotPhanBoFilter(object obj)
        {
            var item = (BhDtTmBHYTTNModel)obj;
            bool res = true;
            if (DataLNS.Any(x => x.IsSelected))
            {
                res = res && item.SDSLNS.Split(",").Any(x => DataLNS.Where(x => x.IsSelected).Select(y => y.SLNS).Contains(x));
            }
            else
            {
                res = false;
            }

            if (!string.IsNullOrWhiteSpace(_searchDotPhanBo))
            {
                res = res && item.SSoChungTu.ToLower().Contains(_searchDotPhanBo, StringComparison.OrdinalIgnoreCase);
            }
            item.IsFilter = res;
            item.IsEnabled = res;
            return res;

        }

        private bool ListUnitFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchUnit))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchUnit!.Trim().ToLower());
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchLNS!.Trim().ToLower());
        }

        public override void OnSave()
        {
            //Validate trước khi save
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Error(message);
                return;
            }

            bDeleteDetail = false;
            string messageCheckBoxUnit = GetMessageValidateCheckBoxUnit();
            if (!string.IsNullOrEmpty(messageCheckBoxUnit))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxUnit);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    bDeleteDetail = false;
                    DataUnit.Select(n =>
                    {
                        if (!n.IsHitTestVisible)
                        {
                            n.IsChecked = true;
                        }
                        return n;
                    }).ToList();
                    _dataDotPhanBoView.Refresh();
                    return;
                }
            }

            string messageCheckBoxLNS = GetMessageValidateCheckBoxLNS();
            if (!string.IsNullOrEmpty(messageCheckBoxLNS))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxLNS);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    bDeleteDetail = false;
                    DataLNS.Select(n =>
                    {
                        if (!n.IsHitTestVisible)
                        {
                            n.IsSelected = true;
                        }
                        return n;
                    }).ToList();
                    _dataDotPhanBoView.Refresh();
                    return;
                }
            }

            var listDataDotSelected = DataDot.Where(n => n.IsChecked).Select(n => n.Id).ToList();
            var listDataDotExist = InitDataDot?.Where(n => _pbdttmMapBHYTService.IsExistEstimate(n.Id, Model.Id)).ToList();
            var listDataDotUnchecked = listDataDotExist?.Where(n => !listDataDotSelected.Contains(n.Id)).ToList();
            var listDataDotUncheckedSCT = listDataDotUnchecked?.Select(x => x.SSoChungTu).ToList();
            var listDataDotUncheckedId = listDataDotUnchecked?.Select(x => x.Id).ToList();

            if (listDataDotExist != null)
            {
                string listDuToanExist = string.Join(StringUtils.COMMA_SPLIT, listDataDotUnchecked.Select(x => x.SSoChungTu).ToList());
                if (!string.IsNullOrEmpty(listDuToanExist))
                {
                    //string message2 = $"Đã có dữ liệu phân bổ thuộc đợt dự toán {listDuToanExist}, khi bỏ chọn chứng từ {listDuToanExist} dữ liệu phân bổ sẽ bị xóa. Bạn có chắc chắn muốn bỏ chọn chứng từ {listDuToanExist}?";
                    string message2 = string.Format(Resources.DivisionEstimateHasDataReceive, listDuToanExist, listDuToanExist, listDuToanExist);
                    var confirm = MessageBoxHelper.Confirm(message2);
                    if (confirm == MessageBoxResult.Yes)
                    {
                        listDataDotExist.ForAll(n => n.IsChecked = false);
                        bDeleteDetail = true;
                    }
                    else
                    {
                        DataDot.Select(n =>
                        {
                            if (listDataDotUncheckedSCT.Contains(n.SSoChungTu))
                            {
                                n.IsChecked = true;
                            }
                            return n;
                        }).ToList();
                        _dataDotPhanBoView.Refresh();
                        return;
                    }
                }
            }

            //Thực hiện xóa chi tiết
            if (bDeleteDetail)
            {
                if (listDataDotUnchecked.Count() > 0)
                {
                    //Xóa chứng từ chi tiết
                    var predicate_chitiet = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IID_DTTM_BHYT_ThanNhan == Model.Id);
                    List<BhPbdttmBHYTChiTiet> lstChungTuChiTiet = new List<BhPbdttmBHYTChiTiet>();
                    lstChungTuChiTiet = _pbdttmBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();
                    lstChungTuChiTiet = lstChungTuChiTiet.Where(x => listDataDotUncheckedId.Contains(x.IID_DTTM_BHYT_ThanNhan)).ToList();

                    _pbdttmBHYTChiTietService.RemoveRange(lstChungTuChiTiet);

                    //Xóa chứng từ map
                    var predicate_map = PredicateBuilder.True<BhPbdttmMapBHYT>();
                    predicate_map = predicate_map.And(x => x.IID_DTTM_BHYT_PhanBo == Model.Id);
                    List<BhPbdttmMapBHYT> lstChungTuMap = new List<BhPbdttmMapBHYT>();
                    lstChungTuMap = _pbdttmMapBHYTService.FindByCondition(predicate_map).ToList();
                    lstChungTuMap = lstChungTuMap.Where(x => listDataDotUncheckedId.Contains(x.IID_DTTM_BHYT_NhanPhanBo)).ToList();
                    _pbdttmMapBHYTService.RemoveRange(lstChungTuMap);
                }
            }

            //Thực hiện Insert/Update

            if (Model == null) Model = new BhPbdttmBHYTModel();
            string sLNSCheck = string.Join(",", DataLNS.Where(n => n.IsSelected).Select(n => n.SLNS).ToList());
            Model.SDSLNS = sLNSCheck;
            Model.INamLamViec = _sessionInfo.YearOfWork;
            if (Model.ILoaiDuToan != (int)BudgetType.ADJUSTED)
            {
                Model.SDS_IDMaDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit);
            }
            //Model.SD = string.Join(",", DataDot.Where(n => n.IsChecked).Select(n => n.SSoChungTu).ToList());
            Model.SDS_DotNhan = string.Join(",", DataDot.Where(n => n.IsChecked).Select(n => n.Id).ToList());
            Model.ILoaiDuToan = int.Parse(_cbxBudgetTypeSelected.ValueItem);
            BhPbdttmBHYT entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new BhPbdttmBHYT();
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh.Value;
                entity.DNgayChungTu = Model.DNgayChungTu.Value;
                entity.DNgayTao = DateTime.Now;
                entity.DNgaySua = null;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _pbdttmBHYTService.Add(entity);
            }
            else
            {
                // Update
                entity = _pbdttmBHYTService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh.Value;
                entity.DNgayChungTu = Model.DNgayChungTu.Value;
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _pbdttmBHYTService.Update(entity);
            }

            //Thêm mới chứng từ nhận phân bổ map
            var dtChungTuMaps = DataDot.Where(x => x.IsChecked).Select(idDotNhan => new BhPbdttmMapBHYT
            {
                IID_DTTM_BHYT_NhanPhanBo = idDotNhan.Id,
                IID_DTTM_BHYT_PhanBo = entity.Id,
                SNguoiTao = _sessionService.Current.Principal,
                SNguoiSua = _sessionService.Current.Principal,
                DNgayTao = DateTime.Now,
                DNgaySua = DateTime.Now
            }).ToList();
            if (listDataDotExist != null)
            {
                dtChungTuMaps = dtChungTuMaps.Where(x => !listDataDotExist.Select(x => x.Id).Contains(x.IID_DTTM_BHYT_NhanPhanBo)).ToList();
            }

            _pbdttmMapBHYTService.AddRange(dtChungTuMaps);

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhPbdttmBHYTModel>(entity));
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (!(Model.DNgayChungTu.HasValue && !string.IsNullOrEmpty(Model.SSoQuyetDinh) ||
                 (Model.DNgayQuyetDinh == null && string.IsNullOrEmpty(Model.SSoQuyetDinh))))
            {
                if (Model.DNgayQuyetDinh == null)
                {
                    messages.Add(Resources.AlertNgayQuyetDinhEmpty);
                }
                if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
                {
                    messages.Add(Resources.AlertSoQuyetDinhEmpty);
                }
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }

            if (DataDot.All(x => !x.IsChecked))
            {
                messages.Add("Hãy chọn đợt phân bổ");
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add("Hãy chọn loại dự toán");
            }

            if (!messages.Any())
            {
                //messages.AddRange(ValidateSoQuyetDinh());
                List<string> listIdDotNhan = new List<string>();

                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (!BudgetType.ADJUSTED.Equals(budgetType))
                {
                    if (_dataUnit.All(x => !x.IsChecked))
                    {
                        messages.Add("Hãy chọn đơn vị");
                    }
                    if (DataLNS.All(x => !x.IsSelected))
                    {
                        messages.Add("Hãy chọn LNS");
                    }
                    if (!_isNamLuyKe)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.Id.ToString()).ToList();
                    }
                }
                else
                {
                    if (Model.ILoaiDuToan != (int)BudgetType.ADJUSTED)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.ILoaiDuToan.ToString()).ToList();
                        if (listIdDotNhan.Count() > 1)
                        {
                            messages.Add("Đ/c chỉ được điều chỉnh một chứng từ duy nhất.");
                        }
                    }

                }
            }

            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidateCheckBoxUnit()
        {
            var predicate_chitiet = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTTM_BHYT_ThanNhan_PhanBo == Model.Id);

            List<BhPbdttmBHYTChiTiet> lstChungTuChiTiet = new List<BhPbdttmBHYTChiTiet>();
            lstChungTuChiTiet = _pbdttmBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();

            var lstUnitHasData = lstChungTuChiTiet.Where(x => x.FDuToan > 0).Select(x => x.IID_MaDonVi).ToList();
            var lstUnitHasDataUnCheck = DataUnit.Where(x => !x.IsChecked && lstUnitHasData.Contains(x.ValueItem)).ToList();

            string unitText = string.Join(StringUtils.COMMA_SPLIT, lstUnitHasDataUnCheck);

            if (!string.IsNullOrEmpty(unitText))
            {
                return string.Format(Resources.DivisionEstimateHasDataUnit, unitText);
            }
            else return "";
        }

        private string GetMessageValidateCheckBoxLNS()
        {
            var predicate_chitiet = PredicateBuilder.True<BhPbdttmBHYTChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTTM_BHYT_ThanNhan_PhanBo == Model.Id);

            List<BhPbdttmBHYTChiTiet> lstChungTuChiTiet = new List<BhPbdttmBHYTChiTiet>();
            lstChungTuChiTiet = _pbdttmBHYTChiTietService.FindByCondition(predicate_chitiet).ToList();

            var lstLNSHasData = lstChungTuChiTiet.Where(x => x.FDuToan > 0).Select(x => x.SLNS).ToList();
            var lstLNSHasDataUnCheck = DataLNS.Where(x => !x.IsSelected && lstLNSHasData.Contains(x.SLNS)).ToList();

            string lnsText = string.Join(StringUtils.COMMA_SPLIT, lstLNSHasDataUnCheck);

            if (!string.IsNullOrEmpty(lnsText))
            {
                return string.Format(Resources.DivisionEstimateHasDataLNS, lnsText);
            }
            else return "";

        }

        private List<string> ValidateSoQuyetDinh()
        {
            List<string> messages = new List<string>();
            if (!string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                var predicate = PredicateBuilder.True<BhPbdttmBHYT>();
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.SSoQuyetDinh == Model.SSoQuyetDinh);

                if (!Guid.Empty.Equals(Model.Id))
                    predicate = predicate.And(x => x.Id != Model.Id);
                var listChungTu = _pbdttmBHYTService.FindByCondition(predicate).ToList();
                if (listChungTu.Count > 0)
                {
                    if (listChungTu.Any(x => x.DNgayQuyetDinh != Model.DNgayQuyetDinh.Value.Date))
                    {
                        messages.Add(string.Format(Resources.VoucherValidateSoQuyetDinhNgayQuyetDinh, Model.DNgayQuyetDinh, listChungTu.First().DNgayQuyetDinh.ToString("dd/MM/yyyy")));
                    }
                }
            }
            return messages;
        }

        public void DataDot_PropertyChanged()
        {

            foreach (var model in DataDot)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhDtTmBHYTTNModel.IsChecked))
                    {
                        if (Model.Id != null && Model.Id != Guid.Empty)
                        {
                            var isExist = _pbdttmMapBHYTService.IsExistEstimate(model.Id, Model.Id);
                            if (isExist && selecteds.Contains(model.Id) && !model.IsChecked)
                            {
                                string message = $"Đã có dữ liệu phân bổ thuộc đợt dự toán {model.SSoChungTu}, khi bỏ chọn chứng từ {model.SSoChungTu} dữ liệu phân bổ sẽ bị xóa. Bạn có chắc chắn muốn bỏ chọn chứng từ {model.SSoChungTu}?";
                                var confirm = MessageBoxHelper.Confirm(message);
                                if (confirm == MessageBoxResult.Yes)
                                {
                                    model.IsChecked = false;
                                }
                                else
                                {
                                    model.IsChecked = true;
                                }
                            }
                            OnPropertyChanged(nameof(DataDot));
                        }
                    }
                };
            }
        }
    }
}
