using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.ChungTuCapPhat
{
    public class ChungTuCapPhatDialogViewModel : DialogViewModelBase<BhCpChungTuModel>
    {
        #region Interface
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly IBhDanhMucLoaiChiService _bhMucLoaiChiService;
        private readonly IBhCpChungTuService _bhCpChungTuService;
        private readonly IBhCpChungTuChiTietService _bhCpChungTuChiTietService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ISysAuditLogService _log;
        private ICollectionView _nsDonViModelsView;
        private ICollectionView _dataLNSView;
        private SessionInfo _sessionInfo;
        private ICollectionView _listAgency;
        #endregion

        #region Property
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        public override Type ContentType => typeof(ChungTuCapPhatDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ cấp phát" : "Cập nhật chứng từ cấp phát";
        private const string SELECTED_AGENCY_COUNT_STR = "Chọn đơn vị ({0}/{1})";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);
        public bool IsDetail { get; set; }
        public bool IsAgregate { get; set; }
        public bool IsSummary { get; set; }
        private bool _isFTyLeThu;
        public bool IsFTyLeThu
        {
            get => _isFTyLeThu;
            set => SetProperty(ref _isFTyLeThu, value);
        }
        private bool IsCapPhatToanDonVi;
        public List<string> ListIdDonViHasCt { get; set; }
        public List<BhCpChungTuModel> ListBhCpChungTuModel { get; set; }
        public List<BhCpChungTuModel> ListBhCpchungTuModelTongHop { get; set; }

        private ObservableCollection<ComboboxItem> _dataLoaiQuy;
        public ObservableCollection<ComboboxItem> DataLoaiQuy
        {
            get => _dataLoaiQuy;
            set => SetProperty(ref _dataLoaiQuy, value);
        }

        private ComboboxItem _selectedLoaiQuy;
        public ComboboxItem SelectedLoaiQuy
        {
            get => _selectedLoaiQuy;
            set => SetProperty(ref _selectedLoaiQuy, value);
        }

        private ComboboxItem _selectedDanhMucLoaiChi;
        public ComboboxItem SelectedDanhMucLoaiChi
        {
            get => _selectedDanhMucLoaiChi;
            set
            {
                SetProperty(ref _selectedDanhMucLoaiChi, value);
                if (Model != null && _selectedDanhMucLoaiChi != null && (_selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9050001_9050002
                    || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010008
                    || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010009
                    || _selectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_9010010))
                {
                    Model.FTyLeThu = null;
                    _isFTyLeThu = false;
                    OnPropertyChanged(nameof(IsFTyLeThu));
                }
                else
                {
                    Model.FTyLeThu = BHXHConstants.CKP_TYLETHU_DEFAULT;
                    _isFTyLeThu = true;
                    OnPropertyChanged(nameof(IsFTyLeThu));
                }
                //LoadBudgetIndexes();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDanhMucLoaiChi;
        public ObservableCollection<ComboboxItem> ItemsDanhMucLoaiChi
        {
            get => _itemsDanhMucLoaiChi;
            set => SetProperty(ref _itemsDanhMucLoaiChi, value);
        }

        public DateTime? DNgayChungTu { get; set; }
        public DateTime? DNgayQuyetDinh { get; set; }
        #endregion

        #region list Don Vi
        private ObservableCollection<CheckBoxItem> _donViModelItems;
        public ObservableCollection<CheckBoxItem> DonViModelItems
        {
            get => _donViModelItems;
            set
            {
                SetProperty(ref _donViModelItems, value);
                OnPropertyChanged();
            }
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value))
                {
                    _nsDonViModelsView.Refresh();
                }
            }
        }

        private bool _selectAllDonVi;
        public bool SelectAllDonVi
        {
            get => DonViModelItems.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (CheckBoxItem item in DonViModelItems.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllDonVi;
                }
            }
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = DonViModelItems != null ? DonViModelItems.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DonViModelItems != null ? DonViModelItems.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
            }
        }
        #endregion

        #region list agency
        private bool _checkAllAgencies;
        private ObservableCollection<AgencyModel> _agencies;
        public ObservableCollection<AgencyModel> Agencies
        {
            get => _agencies;
            set
            {
                SetProperty(ref _agencies, value);
                //OnPropertyChanged(nameof(IsExportEnable));
            }
        }

        private string _searchAgencyText;
        public string SearchAgencyText
        {
            get => _searchAgencyText;
            set
            {
                if (SetProperty(ref _searchAgencyText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public string SelectedAgencyCount
        {
            get
            {
                int totalCount = 0;
                int totalSelected = 0;
                if (_agencies != null)
                {
                    totalCount = Agencies != null ? Agencies.Where(x => x.IsFilter).Count() : 0;
                    totalSelected = Agencies != null ? Agencies.Count(item => item.Selected) : 0;
                }
                return string.Format(SELECTED_AGENCY_COUNT_STR, totalSelected, totalCount);
            }
        }
        private bool _isSelectedAllAgency;
        public bool IsSelectedAllAgency
        {
            get => Agencies.Count > 0 && Agencies.Where(x => x.IsFilter).All(x => x.Selected);
            set
            {
                SetProperty(ref _isSelectedAllAgency, value);
                _checkAllAgencies = true;
                foreach (AgencyModel item in Agencies)
                {
                    item.Selected = _isSelectedAllAgency;
                }
                _checkAllAgencies = false;
                OnPropertyChanged(nameof(SelectedAgencyCount));
                //OnPropertyChanged(nameof(IsExportEnable));
            }
        }
        #endregion

        #region list LNS theo danh muc loai chi

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;
        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => DataLNS != null && DataLNS.Any() && DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _dataLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
            }
        }
        #endregion

        #region Constructor
        public ChungTuCapPhatDialogViewModel(IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger,
            IBhCpChungTuService bhCpChungTuService,
            IBhCpChungTuChiTietService bhCpChungTuChiTietService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            INsNguoiDungDonViService nsNguoiDungDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _log = log;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _danhMucService = danhMucService;
            _bhCpChungTuService = bhCpChungTuService;
            _nguoiDungDonViService = nsNguoiDungDonViService;
            _bhCpChungTuChiTietService = bhCpChungTuChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhMucLoaiChiService = bhDanhMucLoaiChiService;
        }
        #endregion

        #region Init
        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                //LoadAttach();
                LoadNsDonVis();
                LoadAgencies();
                LoadDanhMucLoaiChi();
                LoadSettingCapPhat();
                LoadLoaiQuy();
                LoadData();

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Load loai quy
        private void LoadLoaiQuy()
        {
            DataLoaiQuy = new ObservableCollection<ComboboxItem>();
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "1", DisplayItem = "Quý I" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "2", DisplayItem = "Quý II" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "3", DisplayItem = "Quý III" });
            DataLoaiQuy.Add(new ComboboxItem { ValueItem = "4", DisplayItem = "Quý IV" });
            if (Model.Id == Guid.Empty)
                SelectedLoaiQuy = DataLoaiQuy.FirstOrDefault();
            else
                SelectedLoaiQuy = DataLoaiQuy.Where(n => n.ValueItem == Model.IQuy.ToString()).FirstOrDefault();
        }
        #endregion

        #region Load data
        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            // Add chung tu tong hop
            if (IsAgregate)
            {
                if (Model != null && Model.Id != Guid.Empty)
                {
                    SetCheckboxSLNSSelected(_dataLNS, Model.SLNS);
                    SetCheckboxDonViSelected(_donViModelItems, Model.SID_MaDonVi);
                    SetCheckboxDonViBHSelected(_agencies, Model.SID_MaDonVi);
                    DNgayChungTu = Model.DNgayChungTu;
                    DNgayQuyetDinh = Model.DNgayQuyetDinh;
                }
                else
                {
                    var soChungTuIndex = _bhCpChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                    Model = new BhCpChungTuModel()
                    {
                        DNgayChungTu = DateTime.Now,
                        DNgayTao = DateTime.Now,
                        DNgayQuyetDinh = DateTime.Now,
                        SSoChungTu = "CP-" + soChungTuIndex.ToString("D3"),
                        SNguoiTao = _sessionInfo.Principal,
                        INamChungTu = _sessionInfo.YearOfWork,
                        FTyLeThu = BHXHConstants.CKP_TYLETHU_DEFAULT
                    };
                    DNgayChungTu = DateTime.Now;
                    DNgayQuyetDinh = DateTime.Now;
                }
            }
            // add chung tu thuong
            else
            {
                if (Model != null && !Model.Id.IsNullOrEmpty())
                {
                    SetCheckboxSLNSSelected(_dataLNS, Model.SLNS);
                    SetCheckboxDonViSelected(_donViModelItems, Model.SID_MaDonVi);
                    SetCheckboxDonViBHSelected(_agencies, Model.SID_MaDonVi);
                    DNgayChungTu = Model.DNgayChungTu;
                    DNgayQuyetDinh = Model.DNgayQuyetDinh;
                }
                else
                {
                    var soChungTuIndex = _bhCpChungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork);
                    Model = new BhCpChungTuModel()
                    {
                        DNgayChungTu = DateTime.Now,
                        DNgayTao = DateTime.Now,
                        DNgayQuyetDinh = DateTime.Now,
                        SSoChungTu = "CP-" + soChungTuIndex.ToString("D3"),
                        SNguoiTao = _sessionInfo.Principal,
                        INamChungTu = _sessionInfo.YearOfWork,
                        FTyLeThu = BHXHConstants.CKP_TYLETHU_DEFAULT
                    };
                    DNgayChungTu = DateTime.Now;
                    DNgayQuyetDinh = DateTime.Now;
                }
            }

            OnPropertyChanged(nameof(IsEnabled));
        }

        private void SetCheckboxDonViSelected(ObservableCollection<CheckBoxItem> donViModelItems, string sID_MaDonVi)
        {
            if (string.IsNullOrEmpty(sID_MaDonVi) || DonViModelItems == null || DonViModelItems.Count == 0)
                return;
            List<string> selectedValues = sID_MaDonVi.Split(",").ToList();
            foreach (CheckBoxItem item in donViModelItems)
            {
                item.IsChecked = selectedValues.Contains(item.ValueItem);
            }
        }

        private void SetCheckboxDonViBHSelected(ObservableCollection<AgencyModel> _agencies, string sID_MaDonVi)
        {
            if (string.IsNullOrEmpty(sID_MaDonVi) || _agencies == null || _agencies.Count == 0)
                return;
            List<string> selectedValues = sID_MaDonVi.Split(",").ToList();
            foreach (AgencyModel item in _agencies)
            {
                item.Selected = selectedValues.Contains(item.IIDMaDonVi);
            }
        }

        private void SetCheckboxSLNSSelected(ObservableCollection<BhDmMucLucNganSachModel> dataLNS, string sLNS)
        {
            if (string.IsNullOrEmpty(sLNS) || dataLNS == null || dataLNS.Count == 0)
                return;
            List<string> selectedValues = sLNS.Split(",").ToList();
            foreach (BhDmMucLucNganSachModel item in dataLNS)
            {
                item.IsSelected = selectedValues.Contains(item.SLNS);
            }
        }

        private void LoadDanhMucLoaiChi()
        {
            ItemsDanhMucLoaiChi = new ObservableCollection<ComboboxItem>();
            IEnumerable<BhDanhMucLoaiChi> listDanhMucLoaiChi = _bhMucLoaiChiService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            if (listDanhMucLoaiChi != null)
            {
                ItemsDanhMucLoaiChi = _mapper.Map<ObservableCollection<ComboboxItem>>(listDanhMucLoaiChi.Select(n => new ComboboxItem()
                {
                    DisplayItem = n.STenDanhMucLoaiChi,
                    ValueItem = n.Id.ToString(),
                    HiddenValue = n.SLNS.ToString(),
                    Id = n.Id,
                    DisplayItemOption2 = n.SMaLoaiChi
                }));

                if (!Model.IID_LoaiCap.IsNullOrEmpty())
                {
                    var pair = listDanhMucLoaiChi.Select((Value, Index) => new { Value, Index })
                                                    .Single(p => p.Value.Id == Model.IID_LoaiCap);
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(pair.Index);
                }
                else
                {
                    SelectedDanhMucLoaiChi = ItemsDanhMucLoaiChi.ElementAt(0);
                }
            }

            OnPropertyChanged(nameof(ItemsDanhMucLoaiChi));
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out IsCapPhatToanDonVi);
            else IsCapPhatToanDonVi = false;
        }

        private void LoadNsDonVis()
        {
            DonViModelItems = new ObservableCollection<CheckBoxItem>();
            var predicate = CreatePredicate();
            var iNamChungTu = _sessionInfo.YearOfWork;
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate);
            if (!IsCapPhatToanDonVi)
            {
                List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
                if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
                    listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
                else
                    listDonVi = new List<DonVi>();
            }

            DonViModelItems = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            // Filter
            _nsDonViModelsView = CollectionViewSource.GetDefaultView(DonViModelItems);
            _nsDonViModelsView.Filter = ListDonViFilter;
            foreach (var model in DonViModelItems)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                    }
                };
            }
        }

        private void LoadAgencies()
        {
            try
            {
                IsLoading = true;
                var predicate = CreatePredicate();
                var iNamChungTu = _sessionInfo.YearOfWork;
                List<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate).ToList();
                if (!IsCapPhatToanDonVi)
                {
                    List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
                    if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
                        listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
                    else
                        listDonVi = new List<DonVi>();
                }
                if (listDonVi != null)
                {
                    _agencies = _mapper.Map<ObservableCollection<AgencyModel>>(listDonVi);
                }
                else
                    _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
                _listAgency.Filter = ListAgencyFilter;
                foreach (var model in Agencies)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(AgencyModel.Selected) && !_checkAllAgencies)
                        {
                            OnPropertyChanged(nameof(SelectedAgencyCount));
                            OnPropertyChanged(nameof(IsSelectedAllAgency));
                        }
                    };
                }

                OnPropertyChanged(nameof(Agencies));
                OnPropertyChanged(nameof(IsSelectedAllAgency));
                OnPropertyChanged(nameof(SelectedAgencyCount));
                IsLoading = false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                _agencies = new ObservableCollection<AgencyModel>();
                _listAgency = CollectionViewSource.GetDefaultView(_agencies);
            }
        }

        private bool ListAgencyFilter(object obj)
        {
            bool result = true;
            var item = (AgencyModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchAgencyText))
                result = item.AgencyName.ToLower().Contains(_searchAgencyText!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private bool ListDonViFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchDonVi))
                result = item.ValueItem.ToLower().Contains(_searchDonVi!.ToLower());
            item.IsFilter = result;
            return result;
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionInfo.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionInfo;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(n => n.Loai == LoaiDonVi.NOI_BO);
            return predicate;
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var currentIdDonVi = _sessionService.Current.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.IIDMaDonVi == currentIdDonVi);
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        #endregion

        #region On Save
        public override void OnSave()
        {
            try
            {
                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.Forms.MessageBox.Show(message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Model ??= new BhCpChungTuModel();
                Model.SLNS = SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : SelectedDanhMucLoaiChi.HiddenValue;
                Model.INamChungTu = _sessionService.Current.YearOfWork;
                Model.SID_MaDonVi = GetValueDonViBHSelected(Agencies);
                //Model.SID_MaDonVi = GetValueDonViSelected(DonViModelItems);
                Model.IID_LoaiCap = SelectedDanhMucLoaiChi.Id;
                Model.DNgayQuyetDinh = DNgayQuyetDinh;
                Model.DNgayChungTu = DNgayChungTu;
                Model.IQuy = Convert.ToInt32(_selectedLoaiQuy.ValueItem);
                var donVi = GetNsDonViOfCurrentUser();
                BhCpChungTu entity;
                if (Model.Id == Guid.Empty)
                {
                    // Add
                    entity = new BhCpChungTu();
                    _mapper.Map(Model, entity);

                    if (IsAgregate)
                    {
                        entity.IID_TongHop = Guid.NewGuid();
                        entity.ILoaiTongHop = AllocationTypeLoaiChungTu.ChungTuTongHop;
                        entity.SID_MaDonVi = donVi.IIDMaDonVi;
                        entity.STongHop = string.Join(",", ListBhCpChungTuModel.Select(x => x.SSoChungTu).OrderBy(x => x).ToList());
                        entity.IID_LoaiCap = ListBhCpChungTuModel[0].IID_LoaiCap;
                        entity.SLNS = SelectedDanhMucLoaiChi.HiddenValue == LNSValue.LNS_901_9010001_9010002 ? LNSValue.LNS_9010001_9010002 : SelectedDanhMucLoaiChi.HiddenValue;
                    }
                    else
                    {
                        entity.ILoaiTongHop = AllocationTypeLoaiChungTu.ChungTu;
                    }

                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _bhCpChungTuService.Add(entity);

                    if (IsAgregate)
                    {
                        CreateCapPhatDetailTongHop(_mapper.Map<BhCpChungTuModel>(entity));
                        var lstCapPhatDetailTongHop = _bhCpChungTuChiTietService.FindByCondition(item => item.IID_CP_ChungTu.Equals(entity.Id)).ToList();

                        if (lstCapPhatDetailTongHop.Count > 0)
                        {
                            entity.FTienDaCap = lstCapPhatDetailTongHop.Sum(item => item.FTienDaCap);
                            entity.FTienKeHoachCap = lstCapPhatDetailTongHop.Sum(item => item.FTienKeHoachCap);
                            entity.FTienDuToan = lstCapPhatDetailTongHop.Sum(item => item.FTienDuToan);
                            _bhCpChungTuService.Update(entity);
                        }
                    }
                }
                else
                {
                    entity = _bhCpChungTuService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.IQuy = int.Parse(SelectedLoaiQuy.ValueItem);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _bhCpChungTuService.Update(entity);
                }

                DialogHost.Close(SystemConstants.ROOT_DIALOG);

                DialogHost.CloseDialogCommand.Execute(null, null);
                BhCpChungTuModel model = _mapper.Map<BhCpChungTuModel>(entity);
                if (SelectedDanhMucLoaiChi != null)
                    model.SMaLoaiChi = SelectedDanhMucLoaiChi.DisplayItemOption2;
                // Show detail page when saved
                SavedAction?.Invoke(model);
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message, ex);
            }
        }

        private string GetValueDonViSelected(ObservableCollection<CheckBoxItem> donViModelItems)
        {
            if (donViModelItems.Any())
            {
                return string.Join(",", donViModelItems.Where(n => n.IsChecked == true).Select(n => n.ValueItem).Distinct().ToList());
            }

            return string.Empty;
        }


        private string GetValueDonViBHSelected(ObservableCollection<AgencyModel> agencyModels)
        {
            if (agencyModels.Any())
            {
                return string.Join(",", agencyModels.Where(n => n.Selected == true).Select(n => n.IIDMaDonVi).Distinct().ToList());
            }

            return string.Empty;
        }

        private string GetValueLNSSelected(ObservableCollection<BhDmMucLucNganSachModel> dataLNS)
        {
            if (dataLNS.Count > 0)
            {
                return string.Join(",", dataLNS.Where(n => n.IsSelected == true).Select(n => n.SLNS).Distinct().ToList());
            }
            return string.Empty;
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messages.Add(Resources.AlertSoKeHoachEmpty);
            }

            if (!DNgayQuyetDinh.HasValue)
            {
                messages.Add(Resources.AlertNgayQuyetDinhEmpty);
            }

            if (!IsAgregate)
            {

                if (_selectedDanhMucLoaiChi == null)
                {
                    messages.Add(Resources.AlertLoaiDuToanEmpty);
                }
                if (Agencies.All(x => !x.Selected))
                {
                    messages.Add(Resources.MsgCheckDonVi);
                }
                //if (DonViModelItems.All(x => !x.IsChecked))
                //{
                //    messages.Add(Resources.MsgCheckDonVi);
                //}
            }

            if (SelectedLoaiQuy == null)
            {
                messages.Add(Resources.AlertQuartyEmpty);
            }
            return string.Join(Environment.NewLine, messages);
        }

        private List<string> CheckExistDonVi()
        {
            var predicate = PredicateBuilder.True<BhCpChungTu>();
            predicate = predicate.And(x => x.INamChungTu == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.IID_LoaiCap == SelectedDanhMucLoaiChi.Id);
            predicate = predicate.And(x => x.IQuy == int.Parse(SelectedLoaiQuy.ValueItem));
            if (Model.Id != Guid.Empty)
                predicate = predicate.And(x => x.Id != Model.Id);
            List<BhCpChungTu> chungTus = _bhCpChungTuService.FindByCondition(predicate).ToList();
            List<string> listLNSExist = new List<string>();
            chungTus.ForEach(x =>
            {
                listLNSExist.AddRange(x.SLNS.Split(','));
            });
            List<string> listLNSSelected = DataLNS.Where(x => x.IsSelected).Select(x => x.SLNS).ToList();
            return listLNSSelected.Where(x => listLNSExist.Contains(x)).ToList();
        }

        private void CreateCapPhatDetailTongHop(BhCpChungTuModel bhCpChungTuModels)
        {
            BhCpChungTuChiChiTietCriteria criteria = new BhCpChungTuChiChiTietCriteria()
            {
                ListIdChungTu = string.Join(",", ListBhCpChungTuModel.Select(x => x.Id.ToString()).ToList()),
                IdChungTu = bhCpChungTuModels.Id.ToString(),
                NamLamViec = bhCpChungTuModels.INamChungTu,
                NguoiTao = bhCpChungTuModels.SNguoiTao,
            };
            _bhCpChungTuChiTietService.AddAggregate(criteria);
        }

        #endregion
    }
}
