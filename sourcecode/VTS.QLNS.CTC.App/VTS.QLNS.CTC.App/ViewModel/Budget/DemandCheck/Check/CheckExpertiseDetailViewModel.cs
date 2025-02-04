using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.DemandCheck.Check;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Check
{
    public class CheckExpertiseDetailViewModel : DetailViewModelBase<ExpertiseModel, NsSktNganhThamDinhChiTietSktModel>
    {
        private ISktNganhThamDinhService _chungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ISktNganhThamDinhChiTietService _chungTuChiTietService;
        private INsDonViService _nsDonViService;
        private IDanhMucService _danhMucService;
        private readonly ISktMucLucService _sktMucLucService;
        private readonly INsSktNganhThamDinhChiTietSktService _insSktNganhThamDinhChiTietSktService;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _searchPopupView;
        private readonly ILog _logger;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler ClosePopup;

        public override Type ContentType => typeof(CheckExpertiseDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha && !IsReadOnly;
        public bool IsDeleteAll => Items.Any(item => !item.IsModified);
        public string TitleColumn => "Tự chi";
        public int PhanLoai;
        public Guid IdMucLucSeleted { get; set; }

        private ObservableCollection<ComboboxItem> _typeDisplays;

        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;

        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailFilter != null)
                {
                    OnRefeshFilter();
                }
            }
        }

        private bool _isPopupOpen;

        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set { SetProperty(ref _isPopupOpen, value); }
        }

        private string _popupSearchText;

        public string PopupSearchText
        {
            set
            {
                SetProperty(ref _popupSearchText, value);
                _searchPopupView?.Refresh();
            }
            get => _popupSearchText;
        }

        private NsSktChungTuChiTietModel _nsSktChungTuChiTietSearchModel;

        public NsSktChungTuChiTietModel NsSktChungTuChiTietSearchModel
        {
            get => _nsSktChungTuChiTietSearchModel;
            set => SetProperty(ref _nsSktChungTuChiTietSearchModel, value);
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;

        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
        }

        private SktMucLucModel _selectedPopupItem;

        public SktMucLucModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                NsSktChungTuChiTietSearchModel.SKyHieu = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(NsSktChungTuChiTietSearchModel.SKyHieu));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<NsSktNganhThamDinhChiTietSktModel> _dataThamDinh;
        public ObservableCollection<NsSktNganhThamDinhChiTietSktModel> DataThamDinh
        {
            get => _dataThamDinh;
            set
            {
                if (SetProperty(ref _dataThamDinh, value))
                {
                    OnItemsChanged();
                }
            }
        }

        public bool IsReadOnly { get; set; }
        public CheckExpertiseDetail CheckExpertiseDetail { get; set; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }

        public CheckExpertiseDetailViewModel(ISktNganhThamDinhService chungTuService,
            ISktNganhThamDinhChiTietService chungTuChiTietService,
            IDanhMucService danhMucService,
            INsNguoiDungDonViService nsNguoiDungDonViService,
            IMapper mapper,
            ISktMucLucService sktMucLucService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsSktNganhThamDinhChiTietSktService insSktNganhThamDinhChiTietSktService,
            ILog logger)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _danhMucService = danhMucService;
            _sktMucLucService = sktMucLucService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _insSktNganhThamDinhChiTietSktService = insSktNganhThamDinhChiTietSktService;
            _logger = logger;

            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();

            SaveCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj => { OnRefeshFilter(); });
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new Thickness(10);
                LoadTypeDisplay();
                LoadData();
                OnPropertyChanged(nameof(TitleColumn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                List<NsSktNganhThamDinhChiTietSktModel> data = new List<NsSktNganhThamDinhChiTietSktModel>();
                List<NsSktMucLuc> lstMucLucs = FindListParentMucLucByChild(new List<Guid>() { IdMucLucSeleted });
                List<DonViModel> lstDonViModels = GetLstDonViLoai2();
                foreach (var ml in lstMucLucs)

                {
                    NsSktNganhThamDinhChiTietSktModel it;
                    if (ml.BHangCha)
                    {
                        it = new NsSktNganhThamDinhChiTietSktModel();
                        it.IIdCtnganhThamDinh = Guid.NewGuid();
                        it.IIdMucLuc = ml.IIDMLSKT;
                        it.IsHangCha = ml.BHangCha;
                        it.SMoTa = ml.SMoTa;
                        it.SKyHieu = ml.SKyHieu;
                        it.SStt = ml.SSTT;
                        it.SM = ml.SM;
                        it.IIdMucLucParent = ml.IIDMLSKTCha;
                        it.INamLamViec = _sessionService.Current.YearOfWork;
                        it.INamNganSach = _sessionService.Current.YearOfBudget;
                        it.IIdMaNguonNganSach = _sessionService.Current.Budget;
                        data.Add(it);
                    }
                    else
                    {
                        foreach (var dv in lstDonViModels)
                        {
                            it = new NsSktNganhThamDinhChiTietSktModel();
                            it.IIdCtnganhThamDinh = Guid.NewGuid();
                            it.IIdMaDonVi = dv.IIDMaDonVi;
                            it.STenDonVi = dv.TenDonVi;
                            it.IIdMucLuc = ml.IIDMLSKT;
                            it.IsHangCha = ml.BHangCha;
                            it.SMoTa = ml.SMoTa;
                            it.SKyHieu = ml.SKyHieu;
                            it.SStt = ml.SSTT;
                            it.SM = ml.SM;
                            it.IIdMucLucParent = ml.IIDMLSKTCha;
                            it.INamLamViec = _sessionService.Current.YearOfWork;
                            it.INamNganSach = _sessionService.Current.YearOfBudget;
                            it.IIdMaNguonNganSach = _sessionService.Current.Budget;
                            data.Add(it);
                        }
                    }
                }

                if (DataThamDinh != null && DataThamDinh.Any())
                {
                    foreach (var it in data)
                    {
                        var itTd = DataThamDinh
                            .Where(x => x.IIdMaDonVi.Equals(it.IIdMaDonVi) && x.IIdMucLuc.Equals(it.IIdMucLuc)).ToList();
                        if (itTd.Count > 0)
                        {
                            it.Id = itTd.First().Id;
                            it.FTuChi = itTd.Sum(x => x.FTuChi);
                            it.SGhiChu = itTd.First().SGhiChu;
                        }
                    }
                }
                else
                {
                    var predicate = PredicateBuilder.True<NsSktNganhThamDinhChiTietSkt>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                    predicate = predicate.And(x => x.IIdMucLuc.Equals(IdMucLucSeleted));
                    var dtTd = _insSktNganhThamDinhChiTietSktService.FindByCondition(predicate).ToList();
                    var dtTdModel = _mapper.Map<List<NsSktNganhThamDinhChiTietSktModel>>(dtTd);
                    foreach (var it in data)
                    {
                        var itTd = dtTdModel
                            .Where(x => x.IIdMaDonVi.Equals(it.IIdMaDonVi) && x.IIdMucLuc.Equals(it.IIdMucLuc)).ToList();
                        if (itTd.Count > 0)
                        {
                            it.Id = itTd.First().Id;
                            it.FTuChi = itTd.Sum(x => x.FTuChi);
                            it.SGhiChu = itTd.First().SGhiChu;
                        }
                    }
                }
                Items = new ObservableCollection<NsSktNganhThamDinhChiTietSktModel>(data);
                _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
                _dataDetailFilter.Filter = DataDetailFilter;
                foreach (var sktNganhTham in Items)
                {
                    if (!sktNganhTham.IsHangCha)
                    {
                        sktNganhTham.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName == nameof(SelectedItem.FTuChi) ||
                                args.PropertyName == nameof(SelectedItem.SGhiChu))
                            {
                                NsSktNganhThamDinhChiTietSktModel item = (NsSktNganhThamDinhChiTietSktModel)sender;
                                item.IsModified = true;
                                CalculateData();
                                OnPropertyChanged(nameof(IsSaveData));
                            }
                        };
                    }
                }
                CalculateData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public List<DonViModel> GetLstDonViLoai2()
        {
            List<DonViModel> results;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => StatusType.ACTIVE.Equals(x.ITrangThai));
            predicate = predicate.And(x => LoaiDonVi.TOAN_QUAN.Equals(x.Loai));
            var data = _nsDonViService.FindByCondition(predicate).ToList();
            results = _mapper.Map<List<DonViModel>>(data);
            return results;
        }


        public List<NsSktMucLuc> FindListParentMucLucByChild(List<Guid> listIdMucLuc)
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var listMucLuc = _sktMucLucService
                .FindByCondition(x => listIdMucLuc.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
            List<Guid> listIdMlskt = new List<Guid>();
            List<NsSktMucLuc> sktMucLucs = new List<NsSktMucLuc>();
            if (listMucLuc.Count > 0)
            {
                listIdMlskt = listMucLuc.Select(item => item.IIDMLSKT).ToList();
                sktMucLucs = listMucLuc;
                while (true)
                {
                    var listIdParent = listMucLuc.Where(x => !listIdMlskt.Contains(x.IIDMLSKTCha.GetValueOrDefault())).Select(x => x.IIDMLSKTCha).ToList();
                    var listParent1 = _sktMucLucService.FindByCondition(x => listIdParent.Contains(x.IIDMLSKT) && x.INamLamViec == yearOfWork).ToList();
                    if (listParent1.Count > 0)
                    {
                        var lstId = listParent1.Select(item => item.IIDMLSKT).ToList();
                        listIdMlskt.AddRange(lstId);
                        sktMucLucs.AddRange(listParent1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            sktMucLucs = sktMucLucs.GroupBy(x => x.IIDMLSKT).Select(x => x.First()).OrderBy(x => x.SKyHieu).ToList();
            return sktMucLucs;
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        protected override void OnRefresh()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveData()
        {
            try
            {
                Items.Where(x => x.IsDeleted).Select(x =>
                {
                    x.FTuChi = 0;
                    return x;
                }).ToList();
                CheckExpertiseDetail?.SavedAction?.Invoke(Items.Where(x => !x.IsHangCha).ToList());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            SelectedTypeDisplays = TypeDisplay.TAT_CA;
        }

        private void OnRefeshFilter()
        {
            _dataDetailFilter.Refresh();
        }

        private void OnClearSearch(object obj)
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            _selectedTypeDisplays = TypeDisplay.TAT_CA;
            OnPropertyChanged(nameof(SelectedTypeDisplays));
            _dataDetailFilter.Refresh();
        }

        private bool DataDetailFilter(object obj)
        {
            bool result = true;
            var item = (NsSktNganhThamDinhChiTietSktModel)obj;
            if (!string.IsNullOrEmpty(SelectedTypeDisplays))
            {
                if (SelectedTypeDisplays == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.FTuChi != 0);
            }
            item.IsFilter = result;
            return result;
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    return x;
                }).ToList();
            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter);
            foreach (var item in temp)
            {
                CalculateParent(item.IIdMucLucParent, item);
            }
        }

        private void CalculateParent(Guid? idParent, NsSktNganhThamDinhChiTietSktModel item)
        {
            var model = Items.FirstOrDefault(x => x.IIdMucLuc == idParent.GetValueOrDefault());
            if (model == null) return;
            model.FTuChi += item.FTuChi.GetValueOrDefault();
            CalculateParent(model.IIdMucLucParent, item);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        private void OnCloseWindow()
        {
            CheckExpertiseDetail.Close();
        }
    }
}