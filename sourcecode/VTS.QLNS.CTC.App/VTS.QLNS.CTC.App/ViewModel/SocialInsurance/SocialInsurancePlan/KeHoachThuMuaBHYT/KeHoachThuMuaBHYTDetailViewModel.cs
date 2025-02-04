using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThuMuaBHYT
{
    public class KeHoachThuMuaBHYTDetailViewModel : DetailViewModelBase<BhKhtmBHYTModel, BhKhtmBHYTChiTietModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IKhtmBHYTChiTietService _khtmBHYTChiTietService;
        private readonly IKhtmBHYTService _khtmBHYTService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IMapper _mapper;
        private readonly INsDonViService _nsDonViService;
        private ICollection<BhKhtmBHYTChiTietModel> _filterResult = new HashSet<BhKhtmBHYTChiTietModel>();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateParentWindowEventHandler;
        private string xnmConcatenation = "";
        private ICollectionView _khtmBHYTChiTietModelsView { get; set; }
        private ICollectionView _searchPopupView { get; set; }
        public List<BhKhtmBHYTModel> ListIdsKhtmBHYTSummary { get; set; }
        private BhKhtmBHYTModel _ctTongHop;
        public BhKhtmBHYTModel CtTongHop
        {
            get => _ctTongHop;
            set => SetProperty(ref _ctTongHop, value);
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set
            {
                SetProperty(ref _isLock, value);
                OnPropertyChanged(nameof(IsEnabledDelete));
            }
        }

        private string _sNoiDungSearch;
        public string SNoiDungSearch
        {
            get => _sNoiDungSearch;
            set => SetProperty(ref _sNoiDungSearch, value);
        }

        private ObservableCollection<BhKhtmBHYTChiTietModel> _dataPopupSearchItems;
        public ObservableCollection<BhKhtmBHYTChiTietModel> DataPopupSearchItems
        {
            get => _dataPopupSearchItems;
            set => SetProperty(ref _dataPopupSearchItems, value);
        }

        private BhKhtmBHYTChiTietModel _selectedPopupItem;
        public BhKhtmBHYTChiTietModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                SNoiDungSearch = _selectedPopupItem?.STenNoiDung;
                OnPropertyChanged(nameof(SNoiDungSearch));
                IsPopupOpen = false;
            }
        }

        private ObservableCollection<BhKhtmBHYTChiTietModel> _dataSearch;
        public ObservableCollection<BhKhtmBHYTChiTietModel> DataSearch
        {
            get => _dataSearch;
            set => SetProperty(ref _dataSearch, value);
        }

        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted || !item.IsHangCha);
        public bool IsShowAgencyFilter => IsVoucherSummary && _selectedTypeShowAgencyBHXH != null && _selectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI;

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private bool _isShowColumnUnit;
        public bool IsShowColumnUnit
        {
            get => _isShowColumnUnit;
            set => SetProperty(ref _isShowColumnUnit, value);
        }

        private ObservableCollection<ComboboxItem> _typeShowAgencyBHXH;
        public ObservableCollection<ComboboxItem> TypeShowAgencyBHXH
        {
            get => _typeShowAgencyBHXH;
            set => SetProperty(ref _typeShowAgencyBHXH, value);
        }

        private ComboboxItem _selectedTypeShowAgencyBHXH;
        public ComboboxItem SelectedTypeShowAgencyBHXH
        {
            get => _selectedTypeShowAgencyBHXH;
            set
            {
                if (SetProperty(ref _selectedTypeShowAgencyBHXH, value))
                {
                    if (_selectedTypeShowAgencyBHXH != null && _selectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI)
                        _isShowColumnUnit = true;
                    else _isShowColumnUnit = false;
                    LoadData();
                    OnPropertyChanged(nameof(IsShowColumnUnit));
                    OnPropertyChanged(nameof(IsShowAgencyFilter));
                }
            }
        }

        private List<ComboboxItem> _agencies;
        public List<ComboboxItem> Agencies
        {
            get => _agencies;
            set => SetProperty(ref _agencies, value);
        }

        private ComboboxItem _selectedAgency;
        public ComboboxItem SelectedAgency
        {
            get => _selectedAgency;
            set
            {
                SetProperty(ref _selectedAgency, value);
                BeForeRefresh();
                _khtmBHYTChiTietModelsView.Refresh();
                CalculateData();
            }
        }

        private ObservableCollection<ComboboxItem> _viewSummary = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> ViewSummary
        {
            get => _viewSummary;
            set => SetProperty(ref _viewSummary, value);
        }

        private ComboboxItem _viewSummarySelected;

        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadData();
            }
        }

        public bool IsFilterDonVi => _viewSummarySelected != null &&
                                     _viewSummarySelected.ValueItem.Equals(TypeViewSummary.Detail.ToString());
        public bool IsAnotherUserCreate { get; set; }
        public bool IsEnabledDelete => !IsLock && SelectedItem != null;
        public bool IsDeleteAll => !IsLock && Items.Any(item => !item.IsModified);
        public PrintReportKhtmBhytViewModel PrintReportKhtmBhytViewModel { get; }
        public int NamLamViec { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }
        public new RelayCommand SaveCommand { get; }
        public new RelayCommand CloseCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchTextCommand { get; }

        public bool IsInit { get; set; }
        public DateTime DtNow => DateTime.Now;
        public Visibility SoNguoiVisible { get; set; }
        public Visibility SoThangVisible { get; set; }
        public Visibility DinhMucVisible { get; set; }
        public Visibility ThanhTienVisible { get; set; }

        public KeHoachThuMuaBHYTDetailViewModel(
            IKhtmBHYTChiTietService khtmBHYTChiTietService,
            IKhtmBHYTService khtmBHYTService,
            ISessionService sessionService,
            IMapper mapper,
            ISysAuditLogService log,
            PrintReportKhtmBhytViewModel printReportKhtmBhytViewModel,
            INsDonViService nsDonViService)
        {
            _khtmBHYTChiTietService = khtmBHYTChiTietService;
            _khtmBHYTService = khtmBHYTService;
            _sessionService = sessionService;
            _log = log;
            _mapper = mapper;
            PrintReportKhtmBhytViewModel = printReportKhtmBhytViewModel;
            _nsDonViService = nsDonViService;

            ClearSearchCommand = new RelayCommand(OnClearSearch);
            SaveCommand = new RelayCommand(o => OnSave());
            CloseCommand = new RelayCommand(OnClose);
            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            SearchTextCommand = new RelayCommand(obj => SearchTextFilter());

        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            _selectedTypeShowAgencyBHXH = null;
            _isShowColumnUnit = false;
            NamLamViec = _sessionService.Current.YearOfWork;
            if (Model != null)
            {
                IsLock = Model.BKhoa;
                IsAnotherUserCreate = Model.SNguoiTao != _sessionInfo.Principal;
            }
            if (!string.IsNullOrEmpty(Model.STongHop))
            {
                LoadComboboxTypeShow();
            }
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        private void LoadComboboxTypeShow()
        {
            TypeShowAgencyBHXH = new ObservableCollection<ComboboxItem>();
            TypeShowAgencyBHXH.Add(new ComboboxItem { ValueItem = TypeDisplay.TONG_DONVI, DisplayItem = TypeDisplay.TONG_DONVI });
            TypeShowAgencyBHXH.Add(new ComboboxItem { ValueItem = TypeDisplay.CHITIET_DONVI, DisplayItem = TypeDisplay.CHITIET_DONVI });
            _selectedTypeShowAgencyBHXH = TypeShowAgencyBHXH.FirstOrDefault();
            OnPropertyChanged(nameof(SelectedTypeShowAgencyBHXH));
        }

        private void OnClearSearch(object obj)
        {
            SNoiDungSearch = string.Empty;
            _khtmBHYTChiTietModelsView.Refresh();
        }

        public override void OnClose(object o)
        {
            ((Window)o).Close();
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null || IsLock) return;
            if (Model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, Model.SNguoiTao));
                return;
            }
            SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
        }

        public override void OnSave()
        {
            if (!IsSaveData)
            {
                return;
            }
            Func<BhKhtmBHYTChiTietModel, bool> isAdd = x => x.IsModified && !x.IsDeleted && x.IsAdd && !x.IsHangCha;
            Func<BhKhtmBHYTChiTietModel, bool> isUpdate = x => x.IsModified && !x.IsDeleted && !x.IsAdd && !x.IsHangCha;
            Func<BhKhtmBHYTChiTietModel, bool> isDelete = x => x.IsDeleted && !x.IsHangCha;

            var detailsAdd = Items.Where(isAdd).ToList();
            var detailsUpdate = Items.Where(isUpdate).ToList();
            var detailsDelete = Items.Where(isDelete).ToList();

            //thêm mới chứng từ chi tiết
            if (detailsAdd.Count > 0)
            {
                var addItems = new List<BhKhtmBHYTChiTiet>();
                _mapper.Map(detailsAdd, addItems);
                _khtmBHYTChiTietService.AddRange(addItems);

                Items.Where(isAdd).Select(x =>
                {
                    x.IsModified = false;
                    x.IsAdd = false;
                    return x;
                }).ToList();
            }

            //cập nhật chứng từ chi tiết
            if (detailsUpdate.Count > 0)
            {
                foreach (var updateItem in detailsUpdate)
                {
                    var khtmBHYTChiTiet = _khtmBHYTChiTietService.FindById(updateItem.Id);
                    _mapper.Map(updateItem, khtmBHYTChiTiet);
                    _khtmBHYTChiTietService.Update(khtmBHYTChiTiet);
                    updateItem.IsModified = false;
                }
            }
            //cập nhật tổng cộng chứng từ
            var bhKhtmChungTu = _khtmBHYTService.FindById(Model.Id);
            bhKhtmChungTu.ITongSoNguoi = Model.ITongSoNguoi;
            bhKhtmChungTu.ITongSoThang = Model.ITongSoNguoi;
            bhKhtmChungTu.FTongDinhMuc = Model.FTongDinhMuc;
            bhKhtmChungTu.FTongThanhTien = Model.FTongThanhTien;
            _khtmBHYTService.Update(bhKhtmChungTu);
            OnRefresh();
            _log.WriteLog(Resources.ApplicationName, "Số nhu cầu - chứng từ chi tiết", (int)TypeExecute.Adjust, DtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            UpdateParentWindowEventHandler?.Invoke(Model, new EventArgs());
            var message = Resources.MsgSaveDone;
            MessageBoxHelper.Info(message);
        }

        protected override void OnRefresh()
        {
            IsInit = true;
            LoadData();
            IsInit = false;
        }

        public bool IsVoucherSummary { get; set; }

        public string NumOfMonth { get; set; }

        public override void LoadData(params object[] args)
        {
            List<BhKhtmBHYTChiTiet> temp = new List<BhKhtmBHYTChiTiet>();
            KhtmBHYTChiTietCriteria searchCondition = new KhtmBHYTChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            
            if (_selectedDonVi != null)
            {
                searchCondition.IdDonViFilter = _selectedDonVi.IIDMaDonVi;
                searchCondition.MaDonVi = _selectedDonVi.IIDMaDonVi;
            }

            if (IsVoucherSummary && SelectedTypeShowAgencyBHXH != null && SelectedTypeShowAgencyBHXH.ValueItem == TypeDisplay.CHITIET_DONVI)
            {
                var voucherNos = Model.STongHop.Split(",").ToList();
                List<BhKhtmBHYT> listChungTu = _khtmBHYTService.FindByAggregateVoucher(voucherNos, _sessionInfo.YearOfWork).ToList();
                List<BhKhtmBHYTChiTiet> listChungTuChiTietParent = new List<BhKhtmBHYTChiTiet>();
                List<BhKhtmBHYTChiTiet> listChungTuChiTietChildren = new List<BhKhtmBHYTChiTiet>();
                foreach (var chungTu in listChungTu)
                {
                    searchCondition.khtmBhytId = chungTu.Id;
                    searchCondition.MaDonVi = chungTu.IIDMaDonVi;
                    var listQuery = _khtmBHYTChiTietService.FindBhKhtmBHYTChiTietByCondition(searchCondition).ToList();
                    listChungTuChiTietParent.AddRange(listQuery.Where(x => x.IsHangCha));
                    listChungTuChiTietChildren.AddRange(listQuery.Where(x => !x.IsHangCha));
                }
                var listXauNoiMa = listChungTuChiTietChildren.Select(x => x.SXauNoiMa).Distinct().ToList();
                listChungTuChiTietParent = listChungTuChiTietParent.Where(x => listXauNoiMa.Any(y => y.Contains(x.SXauNoiMa))).GroupBy(x => x.SXauNoiMa).Select(x => x.First()).Distinct().ToList();
                temp.AddRange(listChungTuChiTietParent);
                temp.AddRange(listChungTuChiTietChildren);
                temp = temp.OrderBy(x => x.SXauNoiMa).ThenBy(x => x.IIDMaDonVi).ToList();
                string agencyIds = string.Join(",", listChungTu.Select(x => x.IIDMaDonVi));
                LoadAgencies(agencyIds);
            }
            else
            {
                searchCondition.khtmBhytId = Model.Id;
                searchCondition.MaDonVi = Model.IIDMaDonVi;
                temp = _khtmBHYTChiTietService.FindBhKhtmBHYTChiTietByCondition(searchCondition).ToList();
            }
            
            var existBhChiTiet = _khtmBHYTChiTietService.ExistBHYTChiTiet(Model.Id);
            foreach (var item in temp)
            {
                item.IsAuToFillTuChi = !existBhChiTiet;
            }
            Items = _mapper.Map<ObservableCollection<BhKhtmBHYTChiTietModel>>(temp);
            _khtmBHYTChiTietModelsView = CollectionViewSource.GetDefaultView(Items);
            _khtmBHYTChiTietModelsView.Filter = ItemsViewFilter;
            foreach (var khtmBhytChiTietModel in Items)
            {
                khtmBhytChiTietModel.IsFilter = true;
                if (!khtmBhytChiTietModel.IsHangCha)
                {
                    khtmBhytChiTietModel.IsAggregate = !string.IsNullOrEmpty(Model.STongHop);
                    khtmBhytChiTietModel.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(BhKhtmBHYTChiTietModel.ISoNguoi) || args.PropertyName == nameof(BhKhtmBHYTChiTietModel.ISoThang)
                            || args.PropertyName == nameof(BhKhtmBHYTChiTietModel.FDinhMuc) || args.PropertyName == nameof(BhKhtmBHYTChiTietModel.FThanhTien)
                            || args.PropertyName == nameof(BhKhtmBHYTChiTietModel.SGhiChu))
                        {
                            BhKhtmBHYTChiTietModel itemCT = (BhKhtmBHYTChiTietModel)sender;
                            itemCT.IsModified = true;
                            CalculateData();
                            khtmBhytChiTietModel.IsModified = true;
                            OnPropertyChanged(nameof(IsSaveData));
                        }
                    };
                }
            }
            CalculateData();
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (BhKhtmBHYTChiTietModel)obj;
            result = VoucherDetailFilter(item);
            if (!result && item.IsHangCha)
            {
               result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            if (!string.IsNullOrEmpty(SNoiDungSearch))
                result = DataSearch.Any(x => x.IIDNoiDung.Equals(item.IIDNoiDung));
            return result;
        }

        private bool VoucherDetailFilter(object obj)
        {
            bool result = true;
            var item = (BhKhtmBHYTChiTietModel)obj;
            if (IsShowAgencyFilter && SelectedAgency != null)
                result = result && item.IIDMaDonVi == _selectedAgency.ValueItem;
            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => VoucherDetailFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        private void LoadAgencies(string agencyIds)
        {
            var listDonVi = _nsDonViService.FindByListIdDonVi(agencyIds, _sessionInfo.YearOfWork);
            _agencies = _mapper.Map<List<ComboboxItem>>(listDonVi);
            OnPropertyChanged(nameof(Agencies));
        }

        private void UpdateTotal()
        {
            Model.ITongSoNguoi = 0;
            Model.ITongSoThang = 0;
            Model.FTongDinhMuc = 0;
            Model.FTongThanhTien = 0;

            var roots = Items.Where(t => !t.IsHangCha).ToList();
            foreach (var item in roots)
            {
                Model.ITongSoNguoi += item.ISoNguoi.GetValueOrDefault();
                Model.ITongSoThang += item.ISoThang.GetValueOrDefault();
                Model.FTongDinhMuc += item.FDinhMuc.GetValueOrDefault();
                Model.FTongThanhTien += item.FThanhTien.GetValueOrDefault();
            }
        }
        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabledDelete));
        }
        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .ForAll(x =>
                {
                    x.ISoNguoi = 0;
                    if(IsVoucherSummary)
                    {
                        x.ISoThang = 0;
                        x.FDinhMuc = 0;
                    }
                    x.FThanhTien = 0;
                });

            var temp = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            var dictByMlns = Items.GroupBy(x => x.IIDNoiDung).ToDictionary(x => x.Key, x => x.First());
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, dictByMlns);
            }

            UpdateTotal();
        }
        private void CalculateParent(Guid idParent, BhKhtmBHYTChiTietModel item, Dictionary<Guid, BhKhtmBHYTChiTietModel> dictByMlns)
        {
            if (!dictByMlns.ContainsKey(idParent))
            {
                return;
            }

            var model = dictByMlns[idParent];

            model.ISoNguoi += item.ISoNguoi.GetValueOrDefault();
            model.FThanhTien += item.FThanhTien.GetValueOrDefault();

            CalculateParent(model.IdParent, item, dictByMlns);
        }

        private void OnPrint(object param)
        {
            var bhytCheckPrintType = (BHYTCheckPrintType)(int)param;
            object content;
            PrintReportKhtmBhytViewModel.BHYTCheckPrintType = bhytCheckPrintType;
            if (Items != null && Items.Count > 0)
            {
                PrintReportKhtmBhytViewModel.DonViChaChungTu = GetParentUnit();
                PrintReportKhtmBhytViewModel.DonViChungTu = Model.STenDonVi;
                PrintReportKhtmBhytViewModel.KhtmBhytId = Model.Id;
            }
            PrintReportKhtmBhytViewModel.Init();
            content = new PrintKhtmBHYT
            {
                DataContext = PrintReportKhtmBhytViewModel
            };
            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.DETAIL_DIALOG, null, null);
            }
        }
        private string GetParentUnit()
        {
            string sParent = NSConstants.BO_QUOC_PHONG;
            DonVi donvi = _nsDonViService.FindByIdDonVi(Model.IIDMaDonVi, _sessionService.Current.YearOfWork);
            if (!"0".Equals(donvi?.Loai))
            {
                DonVi donViCapTren = _nsDonViService.FindByLoai("0", _sessionService.Current.YearOfWork);
                sParent = donViCapTren?.TenDonVi;
            }
            return sParent;
        }

        private void SearchTextFilter()
        {
            if (!string.IsNullOrEmpty(SNoiDungSearch))
            {
                List<string> lstResult = new List<string>();
                List<string> lstParents = new List<string>();
                List<BhKhtmBHYTChiTietModel> results = new List<BhKhtmBHYTChiTietModel>();

                List<string> lstSXaNoiMaChildSearch = Items.Where(x => x.STenNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && !x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                List<string> lstSXaNoiMaParentSearch = Items.Where(x => x.STenNoiDung.ToLower().Contains(SNoiDungSearch.ToLower()) && x.IsHangCha).Select(s => s.SXauNoiMa).Distinct().ToList();
                if (!lstSXaNoiMaChildSearch.IsEmpty())
                {
                    lstParents.AddRange(StringUtils.GetListKyHieuParent(lstSXaNoiMaChildSearch));
                    if (lstParents.Any(x => x.Count() >= 3))
                    {
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                        lstParents.Add(lstParents.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
                    }
                    results = Items.Where(x => lstParents.Contains(x.SXauNoiMa)).ToList();
                }
                if (!lstSXaNoiMaParentSearch.IsEmpty())
                {
                    if (results.IsEmpty())
                        results = GetDataParent(lstSXaNoiMaParentSearch);
                    else
                        results.AddRange(GetDataParent(lstSXaNoiMaParentSearch.Where(x => !lstParents.Contains(x)).ToList()));
                }
                DataSearch = new ObservableCollection<BhKhtmBHYTChiTietModel>(results);
            }
            else
            {
                DataSearch = new ObservableCollection<BhKhtmBHYTChiTietModel>();
            }
            _khtmBHYTChiTietModelsView.Refresh();
        }

        private List<BhKhtmBHYTChiTietModel> GetDataParent(List<string> lstInput)
        {
            List<BhKhtmBHYTChiTietModel> result = new List<BhKhtmBHYTChiTietModel>();
            List<string> lstParent = StringUtils.GetListKyHieuParent(lstInput);
            if (!lstParent.IsEmpty() && lstParent.Any(x => x.Count() >= 3))
            {
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 1));
                lstParent.Add(lstParent.FirstOrDefault(x => x.Count() >= 3).Substring(0, 3));
            }
            var lstData = Items.Where(x => lstParent.Contains(x.SXauNoiMa)).ToList();
            result.AddRange(lstData);
            GetListChild(lstData.Where(x => lstInput.Contains(x.SXauNoiMa)).ToList(), result);
            return result;
        }

        private void GetListChild(List<BhKhtmBHYTChiTietModel> lstInput, List<BhKhtmBHYTChiTietModel> results)
        {
            var itemChild = Items.Where(x => lstInput.Select(x => x.IIDNoiDung).Distinct().Contains(x.IdParent)).ToList();
            if (!itemChild.IsEmpty())
            {
                results.AddRange(itemChild);
                foreach (var item in itemChild.Where(x => Items.Select(y => y.IdParent).Distinct().Contains(x.IIDNoiDung)))
                {
                    GetListChild(new List<BhKhtmBHYTChiTietModel>() { item }, results);
                }
            }
        }
    }
}
