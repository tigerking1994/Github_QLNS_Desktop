using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.VoucherList
{
    public class VoucherListDialogViewModel : DialogViewModelBase<VoucherListModel>
    {
        private readonly ISessionService _sessionService;
        private readonly INsBkChungTuService _chungTuService;
        private readonly IMapper _mapper;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly DmDeTaiService _dmDeTaiService;
        private SessionInfo _sessionInfo;
        private List<NsMucLucNganSach> _listMucLucNganSach;
        private ICollectionView _listBudgetIndex;
        private ICollectionView _budgetCatalogItemsView;
        private const string SELECTED_BUDGET_INDEX_COUNT_STR = "({0}/{1})";

        public override Type ContentType => typeof(View.Budget.Settlement.VoucherList.VoucherListDialog);
        public override string Name => Model.Id == Guid.Empty ? "Thêm chứng từ" : "Sửa chứng từ";
        public override string Description => Model.Id == Guid.Empty ? "Tạo mới bảng kê chứng từ chi tiêu" : "Cập nhật thông tin bảng kê chứng từ chi tiêu";

        public Guid Id;
        public int VoucherNoIndex;

        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }

        private ComboboxItem _quarterSelected;
        public ComboboxItem QuarterSelected
        {
            get => _quarterSelected;
            set => SetProperty(ref _quarterSelected, value);
        }

        private VoucherListLNS _lnsDisplay;
        public VoucherListLNS LnsDisplay
        {
            get => _lnsDisplay;
            set
            {
                SetProperty(ref _lnsDisplay, value);
                LoadBudgetIndexs();
            }
        }

        private ObservableCollection<NsMuclucNgansachModel> _budgetIndexes;
        public ObservableCollection<NsMuclucNgansachModel> BudgetIndexes
        {
            get => _budgetIndexes;
            set => SetProperty(ref _budgetIndexes, value);
        }

        private string _searchBudgetIndexText;
        public string SearchBudgetIndexText
        {
            set
            {
                if (SetProperty(ref _searchBudgetIndexText, value))
                {
                    _listBudgetIndex.Refresh();
                }
            }
        }

        private string _searchBudgetCatalogText;
        public string SearchBudgetCatalogText
        {
            set
            {
                if (SetProperty(ref _searchBudgetCatalogText, value))
                {
                    _budgetCatalogItemsView.Refresh();
                }
            }
        }

        public string SelectedBudgetIndexCount
        {
            get
            {
                int totalCount = BudgetIndexes != null ? BudgetIndexes.Count : 0;
                int totalSelected = BudgetIndexes != null ? BudgetIndexes.Count(item => item.IsSelected) : 0;
                return string.Format(SELECTED_BUDGET_INDEX_COUNT_STR, totalSelected, totalCount);
            }
        }

        private ObservableCollection<NsMuclucNgansachModel> _budgetCatalogItems;
        public ObservableCollection<NsMuclucNgansachModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private NsMuclucNgansachModel _selectedBudgetCatalog;
        public NsMuclucNgansachModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value) && value != null)
                {
                    DetailFilter.Lns = _selectedBudgetCatalog.Lns;
                    _listBudgetIndex.Refresh();
                }
                IsOpenLnsPopup = false;
            }
        }

        private ObservableCollection<DmDeTaiModel> _itemsDmDeTai;
        public ObservableCollection<DmDeTaiModel> ItemsDmDeTai
        {
            get => _itemsDmDeTai;
            set => SetProperty(ref _itemsDmDeTai, value);
        }

        private DmDeTaiModel _selectedDmDeTai;
        public DmDeTaiModel SelectedDmDeTai
        {
            get => _selectedDmDeTai;
            set
            {
                SetProperty(ref _selectedDmDeTai, value);
            }
        }

        private NsMuclucNgansachModel _detailFilter;
        public NsMuclucNgansachModel DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set => SetProperty(ref _isOpenLnsPopup, value);
        }

        public bool HasLNS
        {
            get => !string.IsNullOrEmpty(Model.SXauNoiMa);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetSearchCommand { get; }

        public VoucherListDialogViewModel(INsBkChungTuService chungTuService,
            IMapper mapper,
            INsMucLucNganSachService mucLucNganSachService,
            ISessionService sessionService,
            IDmDeTaiService dmDeTaiService)
        {
            _chungTuService = chungTuService;
            _mapper = mapper;
            _mucLucNganSachService = mucLucNganSachService;
            _sessionService = sessionService;
            _dmDeTaiService = (DmDeTaiService)dmDeTaiService;

            SearchCommand = new RelayCommand(obj => _listBudgetIndex.Refresh());
            ResetSearchCommand = new RelayCommand(obj => OnResetFilter());
        }

        public override void Init()
        {
            base.Init();
            _lnsDisplay = VoucherListLNS.All;
            _detailFilter = new NsMuclucNgansachModel();
            _sessionInfo = _sessionService.Current;
            _listMucLucNganSach = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
            LoadQuarters();
            LoadBudgetCatalog();
            LoadData();
            LoadBudgetIndexs();
            LoadDmDeTai();
        }

        /// <summary>
        /// Tạo data cho combobox qúy
        /// </summary>
        private void LoadQuarters()
        {
            _quarters = new List<ComboboxItem>();
            _quarters.Add(new ComboboxItem("Quý I", "3"));
            _quarters.Add(new ComboboxItem("Quý II", "6"));
            _quarters.Add(new ComboboxItem("Quý III", "9"));
            _quarters.Add(new ComboboxItem("Quý IV", "12"));
            _quarterSelected = _quarters.First();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            if (Id == Guid.Empty)
            {
                Model = new VoucherListModel
                {
                    SSoChungTu = "BK-" + VoucherNoIndex.ToString().PadLeft(3, '0'),
                    ISoChungTuIndex = VoucherNoIndex,
                    DNgayChungTu = DateTime.Now,
                    DNgayQuyetDinh = DateTime.Now
                };
            }
            else
            {
                NsBkChungTu chungTu = _chungTuService.FindById(Id);
                Model = _mapper.Map<VoucherListModel>(chungTu);
                _quarterSelected.ValueItem = Model.IThangQuy.ToString();
                SelectedDmDeTai = ItemsDmDeTai != null ? ItemsDmDeTai.FirstOrDefault(x => x.Id.Equals(Model.IIdDeTaiId.GetValueOrDefault())) : null;
            }
        }

        private void LoadBudgetIndexs()
        {
            List<NsMucLucNganSach> listMucLuc = new List<NsMucLucNganSach>();
            if (!HasLNS)
            {
                if (LnsDisplay == VoucherListLNS.All)
                    listMucLuc = _listMucLucNganSach.Where(x => x.BHangChaQuyetToan == false && !x.Lns.StartsWith("101")).ToList();
                else
                    listMucLuc = _mucLucNganSachService.FindByVoucherList(_sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget, LnsDisplay).ToList();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc.Where(x => x.ITrangThai == StatusType.ACTIVE));
            }
            else
            {
                listMucLuc = _listMucLucNganSach.Where(x => x.XauNoiMa == Model.SXauNoiMa).ToList();
                BudgetIndexes = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listMucLuc.Where(x => x.ITrangThai == StatusType.ACTIVE));
                if (BudgetIndexes.Count > 0)
                    BudgetIndexes.First().IsSelected = true;
            }
            OnPropertyChanged(nameof(SelectedBudgetIndexCount));
            _listBudgetIndex = CollectionViewSource.GetDefaultView(BudgetIndexes);
            _listBudgetIndex.Filter = ListBudgetIndexFilter;
            foreach (var model in BudgetIndexes)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                    {
                        var mlns = (NsMuclucNgansachModel)sender;
                        Model.SXauNoiMa = mlns.XauNoiMa;
                        Model.SNoiDung = mlns.MoTa;
                        OnPropertyChanged(nameof(SelectedBudgetIndexCount));
                    }
                };
            }
        }

        public void LoadDmDeTai()
        {
            var lstResults = _dmDeTaiService.FindAll(new AuthenticationInfo());
            ItemsDmDeTai = _mapper.Map<ObservableCollection<DmDeTaiModel>>(lstResults);
        }

        private void LoadBudgetCatalog()
        {
            BudgetCatalogItems = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(_listMucLucNganSach);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = ListBudgetCatalogFilter;
        }

        private bool ListBudgetIndexFilter(object obj)
        {
            bool result = true;
            var item = (NsMuclucNgansachModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchBudgetIndexText))
                result = result && item.LNSDisplay.ToLower().Contains(_searchBudgetIndexText.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.Lns))
                result = result && item.Lns.ToLower().Contains(DetailFilter.Lns.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.Lns.ToLower().Contains(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.Lns.ToLower().Contains(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.M.ToLower().Contains(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.TM.ToLower().Contains(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.TTM.ToLower().Contains(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.NG.ToLower().Contains(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.TNG.ToLower().Contains(DetailFilter.TNG.ToLower());
            return result;
        }

        private bool ListBudgetCatalogFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchBudgetCatalogText))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchBudgetCatalogText.ToLower());
        }

        /// <summary>
        /// Reset các điều kiện tìm kiếm
        /// </summary>
        private void OnResetFilter()
        {
            DetailFilter = new NsMuclucNgansachModel();
            _listBudgetIndex.Refresh();
        }

        public override void OnSave()
        {
            base.OnSave();
            NsBkChungTu chungTu = new NsBkChungTu();
            if (Model.DNgayChungTu is null)
            {
                MessageBoxHelper.Warning(Resources.MsgNgayChungTuEmpty);
                return;
            }
            if (!BudgetIndexes.Any(x => x.IsSelected))
            {
                MessageBoxHelper.Warning(Resources.MsgNguonNganSachEmpty);
                return;
            }

            Model.IThangQuy = int.Parse(QuarterSelected.ValueItem);
            Model.SThangQuyMoTa = QuarterSelected.DisplayItem;
            Model.IIdDeTaiId = SelectedDmDeTai != null ? SelectedDmDeTai.Id : (Guid?)null;
            if (Id == Guid.Empty)
            {
                Model.INamLamViec = _sessionInfo.YearOfWork;
                Model.INamNganSach = _sessionInfo.YearOfBudget;
                Model.IIdMaNguonNganSach = _sessionInfo.Budget;
                Model.SNguoiTao = _sessionInfo.Principal;
                Model.DNgayTao = DateTime.Now;
                Model.IThangQuyLoai = 1;
                _mapper.Map(Model, chungTu);
                _chungTuService.Add(chungTu);
            }
            else
            {
                Model.SNguoiSua = _sessionInfo.Principal;
                Model.DNgaySua = DateTime.Now;
                chungTu = _chungTuService.FindById(Model.Id);
                _mapper.Map(Model, chungTu);
                _chungTuService.Update(chungTu);
            }

            DialogHost.CloseDialogCommand.Execute(Model, null);
            SavedAction?.Invoke(_mapper.Map<VoucherListModel>(chungTu));
        }
    }
}
