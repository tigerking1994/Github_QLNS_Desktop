using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Allocation
{
    public class AllocationDialogViewModel : ViewModelBase
    {

        private readonly INsDonViService _donViService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly ICpChungTuService _chungTuService;
        private readonly ISessionService _sessionService;
        private readonly INsNguoiDungLnsService _nguoiDungLNSService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly ICpDanhMucService _cpdanhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _listDonViView;
        private ICollectionView _listLNSView;
        private SessionInfo _sessionInfo;
        private bool _isCapPhatToanDonVi;

        public bool IsEditProcess = false;
        public override string Name => "THÊM MỚI CẤP PHÁT";
        public override string Title => IsEditProcess ? "SỬA CHỨNG TỪ" : "THÊM CHỨNG TỪ";
        public override string Description => IsEditProcess ? "Sửa chứng từ cấp phát" : "Thêm mới chứng từ cấp phát";
        public string IconMode => IsEditProcess ? "Edit" : "PlaylistPlus";
        public override Type ContentType => typeof(View.Budget.Allocation.AllocationDialog);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }

        private AllocationModel _allocation;
        public AllocationModel Allocation
        {
            get => _allocation;
            set => SetProperty(ref _allocation, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = ListLNS != null ? ListLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListLNS != null ? ListLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = ListDonVi != null ? ListDonVi.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = ListDonVi != null ? ListDonVi.Count(item => item.IsChecked) : 0;
                return string.Format("ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
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
                    _listLNSView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountLNS));
                }
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
                    _listDonViView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountDonVi));
                }
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => ListLNS.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                foreach (CheckBoxItem item in ListLNS.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllLNS;
                }
            }
        }

        private bool _selectAllDonVi;
        public bool SelectAllDonVi
        {
            get => ListDonVi.Where(x => x.IsFilter).All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                foreach (CheckBoxItem item in ListDonVi.Where(x => x.IsFilter))
                {
                    item.IsChecked = _selectAllDonVi;
                }
            }
        }

        public bool IsEnableDonVi => true;

        private ObservableCollection<CheckBoxItem> _listDonVi;
        public ObservableCollection<CheckBoxItem> ListDonVi
        {
            get => _listDonVi;
            set => SetProperty(ref _listDonVi, value);
        }

        private ObservableCollection<CheckBoxTreeItem> _listLNS;
        public ObservableCollection<CheckBoxTreeItem> ListLNS
        {
            get => _listLNS;
            set => SetProperty(ref _listLNS, value);
        }

        private ObservableCollection<ComboboxItem> _dataLoaiCap;
        public ObservableCollection<ComboboxItem> DataLoaiCap
        {
            get => _dataLoaiCap;
            set => SetProperty(ref _dataLoaiCap, value);
        }

        private ComboboxItem _selectedLoaiCap;
        public ComboboxItem SelectedLoaiCap
        {
            get => _selectedLoaiCap;
            set => SetProperty(ref _selectedLoaiCap, value);
        }

        private ObservableCollection<ComboboxItem> _listChiTietToi;
        public ObservableCollection<ComboboxItem> ListChiTietToi
        {
            get => _listChiTietToi;
            set => SetProperty(ref _listChiTietToi, value);
        }

        private ComboboxItem _selectedChiTietToi;
        public ComboboxItem SelectedChiTietToi
        {
            get => _selectedChiTietToi;
            set
            {
                if (SetProperty(ref _selectedChiTietToi, value) && _selectedChiTietToi != null)
                {
                    LoadLNS();
                }
            }
        }

        public RelayCommand SaveCommand { get; }

        public AllocationDialogViewModel(
            INsDonViService donViService,
            INsMucLucNganSachService mucLucNganSachService,
            ICpChungTuService chungTuService,
            ISessionService sessionService,
            INsNguoiDungDonViService nguoiDungDonViService,
            INsNguoiDungLnsService nguoiDungLNSService,
            IDanhMucService danhMucService,
            ICpDanhMucService cpdanhMucService,
            ILog logger,
            IMapper mapper)
        {
            _donViService = donViService;
            _mucLucNganSachService = mucLucNganSachService;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _nguoiDungLNSService = nguoiDungLNSService;
            _danhMucService = danhMucService;
            _cpdanhMucService = cpdanhMucService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                _sessionInfo = _sessionService.Current;
                LoadSettingCapPhat();
                LoadLoaiCap();
                LoadChiTietToi();
                LoadDonVi();
                IsEnableView = (ParentPage.FuncCode == NSFunctionCode.BUDGET_ALLOCATION);
                if (Allocation == null)
                    Allocation = new AllocationModel();
                if (Allocation.Id == Guid.Empty)
                {
                    Allocation = new AllocationModel();
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork, _sessionInfo.Budget, _sessionInfo.YearOfBudget);
                    Allocation.SoChungTu = "CP-" + soChungTuIndex.ToString("D3");
                    Allocation.NgayChungTu = DateTime.Now;
                    Allocation.MoTa = "Chi tiết chứng từ";
                    Allocation.ILoai = ParentPage.FuncCode == NSFunctionCode.BUDGET_ALLOCATION_RECEIVE ? ((int)LoaiCapPhat.CAP_TREN).ToString() : ((int)LoaiCapPhat.CAP_DUOI).ToString();
                }
                else
                {
                    // Update
                    if (Allocation.ILoai == ((int)LoaiCapPhat.CAP_DUOI).ToString())
                        CheckboxSelectedToStringConvert.SetCheckboxSelected(ListDonVi, Allocation.IdDonVi);

                    CheckboxSelectedToStringConvert.SetCheckboxSelected(ListLNS, Allocation.Lns);
                    DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionInfo.YearOfWork);
                    if (donVi0.IIDMaDonVi == Allocation.IdDonVi)
                        IsEnableView = false;

                    var predicate = PredicateBuilder.True<NsCpChungTu>();
                    predicate = predicate.And(x => x.Id == Allocation.Id);
                    var ctcapphat = _chungTuService.FindByCondition(predicate);
                    Allocation.SoCapPhat = ctcapphat.First().SoCapPhat;
                    OnPropertyChanged(nameof(IsEnableView));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadSettingCapPhat()
        {
            DanhMuc dmCapPhatToanDonVi = _danhMucService.FindByCode(MaDanhMuc.CAP_PHAT_TOAN_DON_VI);
            if (dmCapPhatToanDonVi != null)
                bool.TryParse(dmCapPhatToanDonVi.SGiaTri, out _isCapPhatToanDonVi);
            else _isCapPhatToanDonVi = false;
        }

        private void LoadLoaiCap()
        {

            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_UNG.ToString(), DisplayItem = LoaiCap.CAP_UNG_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_HOP_THUC.ToString(), DisplayItem = LoaiCap.CAP_HOP_THUC_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_THANH_KHOAN.ToString(), DisplayItem = LoaiCap.CAP_THANH_KHOAN_DISPLAY });
            //DataLoaiCap.Add(new ComboboxItem { ValueItem = LoaiCap.CAP_THU.ToString(), DisplayItem = LoaiCap.CAP_THU_DISPLAY });
            //if (Allocation.Id == Guid.Empty)
            //    SelectedLoaiCap = DataLoaiCap.FirstOrDefault();
            //else
            //    SelectedLoaiCap = DataLoaiCap.Where(n => n.ValueItem == Allocation.LoaiCap).FirstOrDefault();

            try
            {
                DataLoaiCap = new ObservableCollection<ComboboxItem>();
                int namlamviec = _sessionService.Current.YearOfWork;
                List<CpDanhMuc> cbxCapPhatData = _cpdanhMucService.FindByNamLamViec(namlamviec);

                DataLoaiCap = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxCapPhatData);
                if (Allocation.Id == Guid.Empty)
                    SelectedLoaiCap = DataLoaiCap.FirstOrDefault();
                else
                    SelectedLoaiCap = DataLoaiCap.Where(n => n.ValueItem == Allocation.LoaiCap).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChiTietToi()
        {
            ListChiTietToi = new ObservableCollection<ComboboxItem>();
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_NGANH, ValueItem = NSChiTietToi.NGANH.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_NGANH });
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_TIEU_MUC, ValueItem = NSChiTietToi.TIEU_MUC.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_TIEU_MUC });
            ListChiTietToi.Add(new ComboboxItem { DisplayItem = NSChiTietToi.MOTA_MUC, ValueItem = NSChiTietToi.MUC.ToString(), HiddenValue = NSChiTietToi.DB_VALUE_MUC });
            if (Allocation.Id == Guid.Empty)
                SelectedChiTietToi = ListChiTietToi.FirstOrDefault();
            else
                SelectedChiTietToi = ListChiTietToi.Where(n => n.HiddenValue == Allocation.ChiTietToi).FirstOrDefault();
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

        public void LoadDonVi()
        {
            ListDonVi = new ObservableCollection<CheckBoxItem>();
            var predicate = CreatePredicate();
            IEnumerable<DonVi> listDonVi = _donViService.FindByCondition(predicate);
            if (!_isCapPhatToanDonVi)
            {
                List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
                if (listNguoiDungDonVi != null && listNguoiDungDonVi.Count > 0)
                    listDonVi = listDonVi.Where(n => listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(n.IIDMaDonVi)).ToList();
                else
                    listDonVi = new List<DonVi>();
            }

            ListDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
            // Filter
            _listDonViView = CollectionViewSource.GetDefaultView(ListDonVi);
            _listDonViView.Filter = ListDonViFilter;
            foreach (var model in ListDonVi)
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

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var session = _sessionInfo;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(n => n.Loai == LoaiDonVi.NOI_BO);
            return predicate;
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

        private List<NsNguoiDungLns> GetListLNSByUser()
        {
            var predicate = PredicateBuilder.True<NsNguoiDungLns>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.SMaNguoiDung == _sessionInfo.Principal);
            List<NsNguoiDungLns> listNguoiDungDonVi = _nguoiDungLNSService.FindAll(predicate).ToList();
            return listNguoiDungDonVi;
        }

        public void LoadLNS()
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            string idDonVi = _sessionInfo.IdDonVi;
            if (SelectedChiTietToi == null || string.IsNullOrEmpty(SelectedChiTietToi.HiddenValue))
            {
                ListLNS = new ObservableCollection<CheckBoxTreeItem>();
                OnPropertyChanged(nameof(SelectedCountLNS));
                OnPropertyChanged(nameof(SelectAllLNS));
                return;
            }

            var predicate = _mucLucNganSachService.createPredicateAllNull();
            predicate = predicate.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(n => !n.XauNoiMa.StartsWith("8"));
            predicate = predicate.And(n => n.SCPChiTietToi == SelectedChiTietToi.HiddenValue);
            IEnumerable<NsMucLucNganSach> listLNSPrev = _mucLucNganSachService.FindByCondition(predicate).OrderBy(n => n.XauNoiMa);
            if (listLNSPrev != null && listLNSPrev.Count() == 0)
            {
                ListLNS = new ObservableCollection<CheckBoxTreeItem>();
                OnPropertyChanged(nameof(SelectedCountLNS));
                OnPropertyChanged(nameof(SelectAllLNS));
                return;
            }
            List<string> xauNoiMaParent = StringUtils.GetListXauNoiMaParent(listLNSPrev.Select(n => n.XauNoiMa).ToList());

            var predicateMLNS = _mucLucNganSachService.createPredicateAllNull();
            predicateMLNS = predicateMLNS.And(x => x.NamLamViec == _sessionInfo.YearOfWork);
            predicateMLNS = predicateMLNS.And(n => !n.XauNoiMa.StartsWith("8"));
            predicateMLNS = predicateMLNS.And(n => xauNoiMaParent.Contains(n.XauNoiMa));
            IEnumerable<NsMucLucNganSach> listLNS = _mucLucNganSachService.FindByCondition(predicateMLNS).OrderBy(n => n.XauNoiMa);

            List<NsNguoiDungLns> listLNSNguoiDung = GetListLNSByUser();
            List<string> listParentLNS = StringUtils.GetListXauNoiMaParent(listLNSNguoiDung.Select(n => n.SLns).ToList());
            listLNS = listLNS.Where(n => listParentLNS.Contains(n.Lns));
            ListLNS = new ObservableCollection<CheckBoxTreeItem>();
            ListLNS = _mapper.Map<ObservableCollection<CheckBoxTreeItem>>(listLNS);
            // Filter
            _listLNSView = CollectionViewSource.GetDefaultView(ListLNS);
            _listLNSView.Filter = ListLNSFilter;
            foreach (CheckBoxTreeItem model in ListLNS)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                    {
                        FindChildCheckbox(model);
                        OnPropertyChanged(nameof(SelectedCountLNS));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
        }

        public void FindChildCheckbox(CheckBoxTreeItem parent)
        {
            ListLNS.Where(n => n.ParentId == parent.Id).Select(n => { n.IsChecked = parent.IsChecked; return n; }).ToList();
            if (ListLNS.Where(n => n.ParentId == parent.Id && n.IsChecked == !parent.IsChecked).ToList().Count == 0)
            {
                return;
            }
            else
            {
                foreach (CheckBoxTreeItem item in ListLNS.Where(n => n.ParentId == parent.Id))
                {
                    FindChildCheckbox(item);
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            bool result = true;
            var item = (CheckBoxItem)obj;
            if (!string.IsNullOrWhiteSpace(_searchLNS))
                result = item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void OnSave()
        {
            try
            {
                Allocation.LoaiCap = SelectedLoaiCap.ValueItem;
                Allocation.IType = int.Parse(SelectedChiTietToi.ValueItem);
                Allocation.ITypeMoTa = SelectedChiTietToi.DisplayItem;
                Allocation.ChiTietToi = SelectedChiTietToi.HiddenValue;
                Allocation.IIdMaDmcapPhat = SelectedLoaiCap.ValueItem;
                if (IsEnableView)
                {
                    Allocation.IdDonVi = CheckboxSelectedToStringConvert.GetValueSelected(ListDonVi);
                }
                else
                {
                    Allocation.IdDonVi = _sessionInfo.IdDonVi;
                }
                if (string.IsNullOrEmpty(Allocation.IdDonVi))
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckDonVi);
                    return;
                }
                Allocation.Lns = CheckboxSelectedToStringConvert.GetValueSelected(ListLNS);
                if (string.IsNullOrEmpty(Allocation.Lns))
                {
                    MessageBoxHelper.Warning(Resources.MsgCheckLNS);
                    return;
                }
                Allocation.NamLamViec = _sessionInfo.YearOfWork;
                Allocation.NguonNganSach = _sessionInfo.Budget;
                Allocation.NamNganSach = _sessionInfo.YearOfBudget;
                Allocation.MoTa = Allocation.MoTa != null ? Allocation.MoTa.Trim() : string.Empty;
                NsCpChungTu entity;
                if (Allocation.Id != Guid.Empty)
                {
                    // Update
                    entity = _chungTuService.FindById(Allocation.Id);
                    _mapper.Map(Allocation, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionInfo.Principal;
                    _chungTuService.Update(entity);
                }
                else
                {
                    // Add
                    int soChungTuIndex = _chungTuService.GetSoChungTuIndexByCondition(_sessionInfo.YearOfWork, _sessionInfo.Budget, _sessionInfo.YearOfBudget);
                    Allocation.IdDonViTao = _sessionInfo.IdDonVi;
                    entity = new NsCpChungTu();
                    entity = _mapper.Map<NsCpChungTu>(Allocation);
                    entity.ISoChungTuIndex = soChungTuIndex;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionInfo.Principal;
                    _chungTuService.Add(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<AllocationModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
