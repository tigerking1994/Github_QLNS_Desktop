using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
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
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate
{
    public class PhanBoDuToanChiDialogViewModel : DialogViewModelBase<BhPbdtcBHXHModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IPbdtcBHXHService _pbdtcBHXHService;
        private readonly IPbdtcBHXHChiTietService _pbdtcBHXHChiTietService;
        private readonly IPbdtcMapBHXHService _pbdtcMapBHXHService;
        private readonly INdtctgBHXHService _ndtctgBHXHService;
        private readonly INdtctgBHXHChiTietService _ndtctgBHXHChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;
        private ICollectionView _dataDotPhanBoView;
        private ICollectionView _loaiChiView;
        private bool _isNamLuyKe;
        private SessionInfo _sessionInfo;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler RefreshVoucherEvent;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanChi.PhanBoDuToanChiDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI" : "CẬP NHẬT";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đợt phân bổ dự toán chi trên giao" : "Cập nhật đợt phân bổ dự toán chi trên giao";
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

        private ObservableCollection<BhDtctgBHXHModel> _dataDot;
        public ObservableCollection<BhDtctgBHXHModel> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ObservableCollection<BhDtctgBHXHModel> _initDataDot;
        public ObservableCollection<BhDtctgBHXHModel> InitDataDot
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


        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        //private ComboboxItem _cbxExpenseTypeSelected;
        //public ComboboxItem CbxExpenseTypeSelected
        //{
        //    get => _cbxExpenseTypeSelected;
        //    set
        //    {
        //        SetProperty(ref _cbxExpenseTypeSelected, value);
        //        if (_cbxExpenseTypeSelected != null)
        //        {
        //            LoadLNS();
        //        }
        //    }
        //}

        //private ObservableCollection<ComboboxItem> _cbxExpenseType;
        //public ObservableCollection<ComboboxItem> CbxExpenseType
        //{
        //    get => _cbxExpenseType;
        //    set => SetProperty(ref _cbxExpenseType, value);
        //}


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
                    LoadLoaiChi();
                    //LoadLNS();
                    //LoadDataDotNhan();
                    LoadDataDotNhanCustom();
                    OnPropertyChanged(nameof(IsAdjusted));
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

        #region Combox loai chi
        private ObservableCollection<CheckBoxItem> _lstLoaiChi;
        public ObservableCollection<CheckBoxItem> LstLoaiChi
        {
            get => _lstLoaiChi;
            set
            {
                SetProperty(ref _lstLoaiChi, value);
                OnPropertyChanged();
            }
        }

        private string _searchLoaiChi;
        public string SearchLoaiChi
        {
            get => _searchLoaiChi;
            set
            {
                if (SetProperty(ref _searchLoaiChi, value))
                {
                    _loaiChiView.Refresh();
                }
            }
        }

        private bool _selectAllLoaiChi;
        public bool SelectAllLoaiChi
        {
            get => LstLoaiChi.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLoaiChi, value);
                foreach (CheckBoxItem item in LstLoaiChi.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLoaiChi;
                }
            }
        }

        public string SelectedCountLoaiChi
        {
            get => $"Loại chi ({LstLoaiChi.Count(item => item.IsChecked)}/{LstLoaiChi.Count})";
        }
        #endregion

        public PhanBoDuToanChiDialogViewModel(
            IMapper mapper,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IPbdtcBHXHService pbdtcBHXHService,
            IPbdtcBHXHChiTietService pbdtcBHXHChiTietService,
            INdtctgBHXHService ndtctgBHXHService,
            IPbdtcMapBHXHService pbdtcMapBHXHService,
            INdtctgBHXHChiTietService ndtctgBHXHChiTietService,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            IDanhMucService danhMucService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            ILog log)
        {
            _mapper = mapper;
            _logger = log;
            _sessionService = sessionService;
            _nSDonViService = nSDonViService;
            _danhMucService = danhMucService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcBHXHChiTietService = pbdtcBHXHChiTietService;
            _ndtctgBHXHService = ndtctgBHXHService;
            _ndtctgBHXHChiTietService = ndtctgBHXHChiTietService;
            _pbdtcBHXHService = pbdtcBHXHService;
            _pbdtcMapBHXHService = pbdtcMapBHXHService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
        }

        public override void Init()
        {
            _isInitilizationFirstTime = false;
            bDeleteDetail = false;
            IsSaveData = true;
            DataDot = new ObservableCollection<BhDtctgBHXHModel>();
            _sessionInfo = _sessionService.Current;
            LoadNamLuyKe();
            LoadBudgetType();
            LoadLoaiChi();
            //LoadExpenseType();
            LoadAgencies();
            //LoadLNS();
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
                new ComboboxItem {DisplayItem = "Bổ sung", ValueItem = "2"},
                //new ComboboxItem {DisplayItem = "Điều chỉnh", ValueItem = "3"}
            };

            CbxBudgetType = new ObservableCollection<ComboboxItem>(cbxVoucher);


            if (Model != null && Model.Id != Guid.Empty)
            {
                _cbxBudgetTypeSelected = CbxBudgetType.Single(item => item.ValueItem.Equals(Model.ILoaiDotNhanPhanBo.ToString()));
            }
            else
            {
                CbxBudgetTypeSelected = CbxBudgetType.ElementAt(0);
            }

        }

        //private void LoadExpenseType()
        //{
        //    var listDanhMucChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork).ToList();
        //    var cbxExpense = listDanhMucChi?.Select(x => new ComboboxItem
        //    {
        //        DisplayItem = x.STenDanhMucLoaiChi,
        //        HiddenValue = x.SMaLoaiChi,
        //        ValueItem = x.Id.ToString()
        //    }).ToList();
        //    CbxExpenseType = new ObservableCollection<ComboboxItem>(cbxExpense);
        //    if (CbxExpenseType.Count() > 0)
        //    {
        //        CbxExpenseTypeSelected = CbxExpenseType.ElementAt(0);
        //    }

        //    if (Model != null && !string.IsNullOrEmpty(Model.IIDLoaiDanhMucChi.ToString()))
        //    {
        //        CbxExpenseTypeSelected = CbxExpenseType.Single(item => item.ValueItem.Equals(Model.IIDLoaiDanhMucChi.ToString()));
        //    }
        //}

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                // Add
                Model = new BhPbdtcBHXHModel()
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
                //AddDataDotNhan(InitDataDot);
                LoadDataDotNhanCustom();
            }

        }
        private void DialogModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Model.DNgayChungTu) && Model.DNgayQuyetDinh == null && _cbxBudgetTypeSelected != null)
            {
                //LoadDataDotNhan();
                LoadDataDotNhanCustom();
            }
            else if (args.PropertyName == nameof(Model.DNgayQuyetDinh) && _cbxBudgetTypeSelected != null)
            {
                //LoadDataDotNhan();
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
            int soChungTuIndex = _pbdtcBHXHService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            Model.SSoChungTu = "PB-" + soChungTuIndex.ToString("D3");
            //OnPropertyChanged(nameof(Division));
        }


        private void LoadLoaiChi()
        {
            try
            {
                LstLoaiChi = new ObservableCollection<CheckBoxItem>();
                var yearOfWork = _sessionInfo.YearOfWork;
                var lstDmLoaiChi = _bhDanhMucLoaiChiService.FindByNamLamViec(_sessionInfo.YearOfWork);
                if (lstDmLoaiChi != null && lstDmLoaiChi.Count() > 0)
                    LstLoaiChi = _mapper.Map<ObservableCollection<CheckBoxItem>>(lstDmLoaiChi);
                _loaiChiView = CollectionViewSource.GetDefaultView(LstLoaiChi);
                _loaiChiView.Filter = ListLoaiChiFilter;

                if (_lstLoaiChi != null && _lstLoaiChi.Count > 0)
                {

                    foreach (var model in LstLoaiChi)
                    {
                        if (Model != null && Model.Id != Guid.Empty)
                        {
                            var lstLNS = Model.SMaLoaiChi.Split(",");
                            if (lstLNS.Contains(model.ValueItem))
                            {
                                model.IsChecked = true;
                            }
                        }
                        else
                        {
                            if (model.NameItem == LNSValue.LNS_9010001_9010002 || model.NameItem == LNSValue.LNS_9010003 || model.NameItem == LNSValue.LNS_901_9010001_9010002
                           || model.NameItem == LNSValue.LNS_9010004_9010005 || model.NameItem == LNSValue.LNS_9010006_9010007)
                            {
                                model.IsChecked = true;
                            }
                        }

                        model.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                            {
                                OnPropertyChanged(nameof(SelectedCountLoaiChi));
                                OnPropertyChanged(nameof(SelectAllLoaiChi));
                            }
                        };
                    }


                    OnPropertyChanged(nameof(LstLoaiChi));
                    OnPropertyChanged(nameof(SelectAllLoaiChi));
                    OnPropertyChanged(nameof(SelectedCountLoaiChi));

                    OnPropertyChanged(nameof(SelectedCountDot));
                    if (!IsCheckDot && _dataDotPhanBoView != null)
                    {
                        _dataDotPhanBoView.Refresh();
                    }
                }


                LoadLoaiChiPropertyChanged();

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListLoaiChiFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(SearchLoaiChi))
                result = item.ValueItem.ToLower().Contains(_searchLoaiChi!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private void LoadLoaiChiPropertyChanged()
        {
            _loaiChiView = CollectionViewSource.GetDefaultView(LstLoaiChi);
            _loaiChiView.Filter = ListLoaiChiFilter;
            OnPropertyChanged(nameof(LstLoaiChi));
            OnPropertyChanged(nameof(SelectAllLoaiChi));
            OnPropertyChanged(nameof(SelectedCountLoaiChi));

            if (_lstLoaiChi != null && _lstLoaiChi.Count > 0)
            {
                foreach (var model in _lstLoaiChi)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectAllLoaiChi));
                            OnPropertyChanged(nameof(SelectedCountLoaiChi));
                            OnPropertyChanged(nameof(SelectedCountDot));
                            if (!IsCheckDot && _dataDotPhanBoView != null)
                            {
                                _dataDotPhanBoView.Refresh();
                            }
                        }
                    };

                }
            }

        }

        //private void LoadLNS()
        //{
        //    int yearOfWork = _sessionService.Current.YearOfWork;
        //    var listNsMucLucNganSach = new List<BhDmMucLucNganSach>();
        //    List<BhDmMucLucNganSachModel> listNsMucLucNganSachModel = new List<BhDmMucLucNganSachModel>();
        //    if (_cbxBudgetTypeSelected != null)
        //    {
        //        var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
        //        predicate = predicate.And(x => x.INamLamViec == yearOfWork);

        //        listNsMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate).OrderBy(x => x.SXauNoiMa).ToList();
        //        listNsMucLucNganSachModel = _mapper.Map(listNsMucLucNganSach, listNsMucLucNganSachModel);
        //    }
        //    if (CbxExpenseTypeSelected != null)
        //    {
        //        var danhmucchi = _bhDanhMucLoaiChiService.FindByNamLamViec(yearOfWork).Where(x => x.Id == Guid.Parse(CbxExpenseTypeSelected.ValueItem)).FirstOrDefault();
        //        if (danhmucchi != null)
        //        {
        //            List<string> sLNSDmChi = new List<string>();
        //            sLNSDmChi = danhmucchi.SLNS.Split(",").ToList();
        //            listNsMucLucNganSachModel = listNsMucLucNganSachModel.Where(x => sLNSDmChi.Contains(x.SLNS)).ToList();
        //        }
        //    }
        //    DataLNS = new ObservableCollection<BhDmMucLucNganSachModel>(listNsMucLucNganSachModel);
        //    if (Model != null && Model.Id != Guid.Empty)
        //    {
        //        List<string> sLnsHasData = Model.SLNS.Split(",").Distinct().ToList();
        //        DataLNS.Where(x => sLnsHasData.Contains(x.SLNS)).Select(x => { x.IsSelected = true; return x; }).ToList();
        //    }
        //    if (_dataLNS != null && _dataLNS.Count > 0)
        //    {
        //        foreach (var model in _dataLNS)
        //        {
        //            model.IsSelected = true;
        //        }
        //        _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
        //        _dataLNSView.Filter = ListLNSFilter;
        //        OnPropertyChanged(nameof(DataLNS));
        //        OnPropertyChanged(nameof(SelectAllLNS));
        //        OnPropertyChanged(nameof(SelectedCountLNS));
        //        OnPropertyChanged(nameof(SelectAllLNS));
        //        OnPropertyChanged(nameof(SelectedCountLNS));
        //        OnPropertyChanged(nameof(SelectedCountDot));
        //        if (!IsCheckDot && _dataDotPhanBoView != null)
        //        {
        //            _dataDotPhanBoView.Refresh();
        //        }
        //    }

        //    LoadLNSPropertyChanged();
        //}

        //private void LoadLNSPropertyChanged()
        //{
        //    _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
        //    _dataLNSView.Filter = ListLNSFilter;
        //    OnPropertyChanged(nameof(DataLNS));
        //    OnPropertyChanged(nameof(SelectAllLNS));
        //    OnPropertyChanged(nameof(SelectedCountLNS));

        //    if (_dataLNS != null && _dataLNS.Count > 0)
        //    {
        //        foreach (var model in _dataLNS)
        //        {
        //            model.PropertyChanged += (sender, args) =>
        //            {
        //                if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
        //                {
        //                    foreach (var item in _dataLNS)
        //                    {
        //                        if (item.IIDMLNSCha == model.IIDMLNS)
        //                        {
        //                            item.IsSelected = model.IsSelected;
        //                        }
        //                    }
        //                    OnPropertyChanged(nameof(SelectAllLNS));
        //                    OnPropertyChanged(nameof(SelectedCountLNS));
        //                    OnPropertyChanged(nameof(SelectedCountDot));
        //                    if (!IsCheckDot && _dataDotPhanBoView != null)
        //                    {
        //                        _dataDotPhanBoView.Refresh();
        //                    }
        //                }
        //            };

        //        }
        //    }

        //}

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

                    // remove 999 hard code
                    predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
                    listUnit = _nSDonViService.FindByCondition(predicate).OrderBy(n => n.IIDMaDonVi).ToList();
                }
            }


            DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<string> lstDonViHasData = Model.SID_MaDonVi.Split(",").ToList();
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
            var predicate = PredicateBuilder.True<BhPbdtcBHXH>();
            IEnumerable<BhPbdtcBHXH> listChungTu = new List<BhPbdtcBHXH>();
            DataDot = new ObservableCollection<BhDtctgBHXHModel>();
            int yearOfWork = _sessionInfo.YearOfWork;
            DateTime date = Model.DNgayQuyetDinh ?? DateTime.Now;

            if (_cbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (BudgetType.ADJUSTED.Equals(budgetType))
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    LblPhanBo = "ĐỢT ĐÃ PHÂN BỔ";
                    predicate = CreatePredicateChungTuPhanBo();
                    predicate = predicate.And(x => !string.IsNullOrEmpty(x.SSoQuyetDinh));
                    //predicate = predicate.And(x => !x.iLoaiDotNhanPhanBo.Equals(SoChungTuType.ReceiveEstimate));
                    predicate = predicate.And(x => (x.DNgayChungTu.HasValue && x.DNgayQuyetDinh.Value.Date <= dateTime) || (!x.DNgayQuyetDinh.HasValue && x.DNgayChungTu.HasValue && x.DNgayChungTu.Value.Date <= dateTime));
                    listChungTu = _pbdtcBHXHService.FindByCondition(predicate);
                    listChungTu = listChungTu.Where(x =>
                    {
                        if (x.ILoaiDotNhanPhanBo != (int)BudgetType.ADJUSTED)
                        {
                            return x.FTongTienHienVat != 0 || x.FTongTienTuChi != 0;
                        }
                        else { return true; }
                    });

                    if (Guid.Empty.Equals(Model.Id))
                    {
                        List<BhdtcnpbMapBHXH> dtNhanPhanBoMaps = _pbdtcMapBHXHService.FindByListIdNhanDuToan(listChungTu.Select(x => x.Id.ToString()).ToList()).ToList();
                        listChungTu = listChungTu.Where(x => !dtNhanPhanBoMaps.Select(x => x.iID_BHDTC_NhanPhanBo).Contains(x.Id)).ToList();
                    }
                    else
                    {
                        listChungTu = listChungTu.Where(x => x.Id != Model.Id).ToList();

                    }
                    List<BhDtctgBHXHModel> lstDtctg = new List<BhDtctgBHXHModel>();
                    lstDtctg = listChungTu.Select(x => new BhDtctgBHXHModel
                    {
                        Id = x.Id,
                        SSoChungTu = x.SSoChungTu,
                        DNgayChungTu = x.DNgayChungTu,
                        SSoQuyetDinh = x.SSoQuyetDinh,
                        DNgayQuyetDinh = x.DNgayQuyetDinh,
                        FSoPhanBo = x.FTongTienHienVat + x.FTongTienTuChi,
                        SLNS = x.SLNS,
                        IsAdjusted = true
                    }).ToList();

                    DataDot = _mapper.Map<ObservableCollection<BhDtctgBHXHModel>>(lstDtctg);
                }
                else
                {
                    LblPhanBo = "ĐỢT NHẬN PHÂN BỔ";
                    int iBudgetType = (int.Parse(_cbxBudgetTypeSelected.ValueItem));
                    List<BhDtctgBHXHQuery> listDotNhanQuery = new List<BhDtctgBHXHQuery>();
                    listDotNhanQuery = _ndtctgBHXHService.GetDuToanDanhSachDotNhanPhanBo(yearOfWork, date, iBudgetType).ToList();

                    if (_isNamLuyKe && listDotNhanQuery.Count > 0)
                    {
                        //Nếu thực hiện tính theo năm lũy kế thì lấy từ bảng BH_DTC_PhanBoDuToanChi
                        predicate = CreatePredicateChungTuPhanBo();
                        var listChungTuPhanBo = _pbdtcBHXHService.FindByCondition(predicate);
                        List<List<string>> listIdDotNhan = listChungTuPhanBo.Select(x => x.IID_DotNhan.Split(",").Distinct().ToList()).Distinct().ToList();
                        foreach (var dotNhan in listDotNhanQuery)
                        {
                            if (!listIdDotNhan.Any(x => x.Contains(dotNhan.Id.ToString())))
                                listIdDotNhan.Add(new List<string> { dotNhan.Id.ToString() });
                        }
                        List<BhDtctgBHXHQuery> listDotNhanQueryLuyKe = new List<BhDtctgBHXHQuery>();
                        foreach (var idDotNhan in listIdDotNhan)
                        {
                            string idLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id));
                            if (!listDotNhanQueryLuyKe.Any(x => x.IdLuyKe == idLuyKe))
                            {
                                BhDtctgBHXHQuery dotNhanQuery = new BhDtctgBHXHQuery
                                {
                                    IdLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id)),
                                    SSoChungTu = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoChungTu)),
                                    DNgayChungTu = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayChungTu),
                                    SSoQuyetDinh = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoQuyetDinh)),
                                    DNgayQuyetDinh = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayQuyetDinh),
                                    FSoPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoPhanBo),
                                    FSoChuaPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoChuaPhanBo),
                                    SLNS = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SLNS))

                                };
                                listDotNhanQueryLuyKe.Add(dotNhanQuery);
                            }
                        }
                        DataDot = _mapper.Map<ObservableCollection<BhDtctgBHXHModel>>(listDotNhanQueryLuyKe);
                    }
                    else
                    {
                        DataDot = _mapper.Map<ObservableCollection<BhDtctgBHXHModel>>(listDotNhanQuery);
                    }
                }
                if (Model != null && Model.Id != Guid.Empty)
                {
                    var predicate_map = PredicateBuilder.True<BhdtcnpbMapBHXH>();
                    predicate_map = predicate_map.And(x => x.iID_BHDTC_PhanBo == Model.Id);

                    List<Guid> lstChungTuMap = new List<Guid>();
                    lstChungTuMap = _pbdtcMapBHXHService.FindByCondition(predicate_map).Select(x => x.iID_BHDTC_NhanPhanBo).ToList();
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
        private Expression<Func<BhPbdtcBHXH, bool>> CreatePredicateChungTuPhanBo()
        {
            var predicate = PredicateBuilder.True<BhPbdtcBHXH>();
            predicate = predicate.And(x => x.INamChungTu == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ILoaiDotNhanPhanBo != int.Parse(_cbxBudgetTypeSelected.ValueItem));
            return predicate;
        }

        private bool ListDataDotPhanBoFilter(object obj)
        {
            var item = (BhDtctgBHXHModel)obj;
            bool res = true;
            if (LstLoaiChi.Any(x => x.IsChecked))
            {
                string sLNS = string.Join(",", LstLoaiChi.Where(n => n.IsChecked == true).Select(n => n.NameItem).Distinct().ToList());
                var lstsLNSChecked = sLNS.Split(",");
                var lstSLNSDotNhan = item.SLNS.Split(',');
                bool exist = lstsLNSChecked.Where(x => lstSLNSDotNhan.Contains(x)).Any();
                res = res && exist;
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
            return obj is BhDmMucLucNganSachModel item && item.LNSDisplay.ToLower().Contains(_searchLNS!.Trim().ToLower());
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

            string messageCheckBoxLoaiChi = GetMessageValidateCheckBoxLoaiChi();
            if (!string.IsNullOrEmpty(messageCheckBoxLoaiChi))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxLoaiChi);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    bDeleteDetail = false;
                    LstLoaiChi.Select(n =>
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

            var listDataDotSelected = DataDot.Where(n => n.IsChecked).Select(n => n.Id).ToList();
            var listDataDotExist = InitDataDot?.Where(n => _pbdtcMapBHXHService.IsExistEstimate(n.Id, Model.Id)).ToList();
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

                //List don vi uncheck
                var lstDonViUnCheck = DataUnit.Where(x => !x.IsChecked).Select(x => x.ValueItem).ToList();
                if (listDataDotUnchecked.Count() > 0 || lstDonViUnCheck.Count() > 0)
                {
                    //Xóa chứng từ chi tiết
                    var predicate_chitiet = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
                    predicate_chitiet = predicate_chitiet.And(x => x.IID_DTC_PhanBoDuToanChi == Model.Id);
                    List<BhPbdtcBHXHChiTiet> lstChungTuChiTiet = new List<BhPbdtcBHXHChiTiet>();
                    lstChungTuChiTiet = _pbdtcBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();

                    lstChungTuChiTiet = lstChungTuChiTiet.Where(x => listDataDotUncheckedId.Contains(x.IID_DTC_DuToanChiTrenGiao) || lstDonViUnCheck.Contains(x.IID_MaDonVi)).ToList();

                    _pbdtcBHXHChiTietService.RemoveRange(lstChungTuChiTiet);

                    //Xóa chứng từ map
                    var predicate_map = PredicateBuilder.True<BhdtcnpbMapBHXH>();
                    predicate_map = predicate_map.And(x => x.iID_BHDTC_PhanBo == Model.Id);
                    List<BhdtcnpbMapBHXH> lstChungTuMap = new List<BhdtcnpbMapBHXH>();
                    lstChungTuMap = _pbdtcMapBHXHService.FindByCondition(predicate_map).ToList();
                    lstChungTuMap = lstChungTuMap.Where(x => listDataDotUncheckedId.Contains(x.iID_BHDTC_NhanPhanBo)).ToList();
                    _pbdtcMapBHXHService.RemoveRange(lstChungTuMap);
                }
            }

            //Thực hiện Insert/Update

            if (Model == null) Model = new BhPbdtcBHXHModel();
            string sLNSCheck = string.Join(",", LstLoaiChi.Where(n => n.IsChecked == true).Select(n => n.NameItem).Distinct().ToList());
            Model.SLNS = sLNSCheck;
            Model.INamChungTu = _sessionInfo.YearOfWork;
            //Model.IIDLoaiDanhMucChi = Guid.Parse(CbxExpenseTypeSelected.ValueItem);
            Model.SMaLoaiChi = string.Join(",", LstLoaiChi.Where(n => n.IsChecked == true).Select(n => n.ValueItem).Distinct().ToList());
            if (Model.ILoaiDotNhanPhanBo != (int)BudgetType.ADJUSTED)
            {
                Model.SID_MaDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit);
            }
            Model.SDotNhan = string.Join(",", DataDot.Where(n => n.IsChecked).Select(n => n.SSoChungTu).ToList());
            Model.IID_DotNhan = string.Join(",", DataDot.Where(n => n.IsChecked).Select(n => n.Id).ToList());
            Model.ILoaiDotNhanPhanBo = int.Parse(_cbxBudgetTypeSelected.ValueItem);
            BhPbdtcBHXH entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new BhPbdtcBHXH();
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                entity.DNgayChungTu = Model.DNgayChungTu;
                entity.DNgayTao = DateTime.Now;
                entity.DNgaySua = null;
                entity.SNguoiTao = _sessionService.Current.Principal;
                _pbdtcBHXHService.Add(entity);
            }
            else
            {
                // Update
                entity = _pbdtcBHXHService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                entity.DNgayChungTu = Model.DNgayChungTu;
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _pbdtcBHXHService.Update(entity);
            }

            //Thêm mới chứng từ nhận phân bổ map
            var dtChungTuMaps = DataDot.Where(x => x.IsChecked).Select(idDotNhan => new BhdtcnpbMapBHXH
            {
                iID_BHDTC_NhanPhanBo = idDotNhan.Id,
                iID_BHDTC_PhanBo = entity.Id,
                sNguoiTao = _sessionService.Current.Principal,
                sNguoiSua = _sessionService.Current.Principal,
                dNgayTao = DateTime.Now,
                dNgaySua = DateTime.Now
            }).ToList();
            if (listDataDotExist != null)
            {
                dtChungTuMaps = dtChungTuMaps.Where(x => !listDataDotExist.Select(x => x.Id).Contains(x.iID_BHDTC_NhanPhanBo)).ToList();
            }

            _pbdtcMapBHXHService.Save(dtChungTuMaps);

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhPbdtcBHXHModel>(entity));
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
                messages.AddRange(ValidateSoQuyetDinh());
                List<string> listIdDotNhan = new List<string>();

                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (!BudgetType.ADJUSTED.Equals(budgetType))
                {
                    if (_dataUnit.All(x => !x.IsChecked))
                    {
                        messages.Add("Hãy chọn đơn vị");
                    }
                    if (LstLoaiChi.All(x => !x.IsChecked))
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
                    if (Model.ILoaiDotNhanPhanBo != null && Model.ILoaiDotNhanPhanBo != (int)BudgetType.ADJUSTED)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.ILoaiDotNhanPhanBo.ToString()).ToList();
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
            var predicate_chitiet = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTC_PhanBoDuToanChi == Model.Id);

            List<BhPbdtcBHXHChiTiet> lstChungTuChiTiet = new List<BhPbdtcBHXHChiTiet>();
            lstChungTuChiTiet = _pbdtcBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();

            var lstUnitHasData = lstChungTuChiTiet.Where(x => x.FTienTuChi > 0 || x.FTienHienVat > 0).Select(x => x.IID_MaDonVi).ToList();
            var lstUnitHasDataUnCheck = DataUnit.Where(x => !x.IsChecked && lstUnitHasData.Contains(x.ValueItem)).ToList();

            string unitText = string.Join(StringUtils.COMMA_SPLIT, lstUnitHasDataUnCheck.Select(x=>x.DisplayItem));

            if (!string.IsNullOrEmpty(unitText))
            {
                return string.Format(Resources.DivisionEstimateHasDataUnit, unitText);
            }
            else return "";
        }

        private string GetMessageValidateCheckBoxLoaiChi()
        {
            var predicate_chitiet = PredicateBuilder.True<BhPbdtcBHXHChiTiet>();
            predicate_chitiet = predicate_chitiet.And(x => x.IID_DTC_PhanBoDuToanChi == Model.Id);

            List<BhPbdtcBHXHChiTiet> lstChungTuChiTiet = new List<BhPbdtcBHXHChiTiet>();
            lstChungTuChiTiet = _pbdtcBHXHChiTietService.FindByCondition(predicate_chitiet).ToList();

            var lstLNSHasData = lstChungTuChiTiet.Where(x => x.FTienTuChi > 0 || x.FTienHienVat > 0).Select(x => x.SLNS).ToList();
            var lstLNSHasDataUnCheck = LstLoaiChi.Where(x => !x.IsChecked && lstLNSHasData.Contains(x.NameItem)).Select(x => x.NameItem).ToList();

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
                var predicate = PredicateBuilder.True<BhPbdtcBHXH>();
                predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.SSoQuyetDinh == Model.SSoQuyetDinh);

                if (!Guid.Empty.Equals(Model.Id))
                    predicate = predicate.And(x => x.Id != Model.Id);
                var listChungTu = _pbdtcBHXHService.FindByCondition(predicate).ToList();
                if (listChungTu.Count > 0)
                {
                    if (listChungTu.Any(x => x.DNgayQuyetDinh.Value.Date != Model.DNgayQuyetDinh.Value.Date))
                    {
                        messages.Add(string.Format(Resources.VoucherValidateSoQuyetDinhNgayQuyetDinh, Model.DNgayQuyetDinh, listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")));
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
                    if (args.PropertyName == nameof(BhDtctgBHXHModel.IsChecked))
                    {
                        if (Model.Id != null && Model.Id != Guid.Empty)
                        {
                            var isExist = _ndtctgBHXHService.IsExitsDuToanDaDuocPhanBo(model.Id, Model.Id);
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
