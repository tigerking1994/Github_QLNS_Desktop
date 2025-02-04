using Aspose.Cells;
using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.LapKeHoachChi.ImportKhcBHXH;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu
{
    public class KeHoachThuIndexViewModel : GridViewModelBase<BhKhtBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IKhtBHXHService _khtBHXHService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private ICollectionView _bhDonViModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;

        private View.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH.ImportKhtBHXH _importKhtBHXH;
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        public bool IsButtonEnable
        {
            get
            {
                var result = false;
                var lstSelected = Items.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    result = true;
                }
                else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("0") && lstSelected.Count > 0)
                {
                    var lstSelectedKhoa = lstSelected.Where(x => x.BIsKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BIsKhoa).ToList();
                    if (lstSelectedKhoa.Count() > 0 && lstSelectedMo.Count() > 0)
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Count() > 0)
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Count() > 0)
                    {
                        IsLock = false;
                        result = true;
                    }

                }
                return result;

            }
        }

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }

        private DonViModel _selectedNsDonViModel;
        public DonViModel SelectedNsDonViModel
        {
            get => _selectedNsDonViModel;
            set
            {
                SetProperty(ref _selectedNsDonViModel, value);
                SearchData();
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private BhKhtBHXHModel _selectedBhLapKeHoachThuModel;
        public BhKhtBHXHModel SelectedBhLapKeHoachThuModel
        {
            get => _selectedBhLapKeHoachThuModel;
            set
            {
                SetProperty(ref _selectedBhLapKeHoachThuModel, value);
                if (_selectedBhLapKeHoachThuModel != null)
                {
                    IsLock = _selectedBhLapKeHoachThuModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedBhLapKeHoachThuModel == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhKhtBHXHModel> _lstChungTuOrigin;
        public List<BhKhtBHXHModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ObservableCollection<DonViModel> _bhDonViModelItems;
        public ObservableCollection<DonViModel> BhDonViModelItems
        {
            get => _bhDonViModelItems;
            set => SetProperty(ref _bhDonViModelItems, value);
        }

        private ObservableCollection<ComboboxItem> _voucherTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> VoucherTypes
        {
            get => _voucherTypes;
            set => SetProperty(ref _voucherTypes, value);
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null)
                    {
                        Items.Where(x => !x.BDaTongHop.Value).ForAll(c => c.Selected = value.Value);
                    }
                }
            }
        }
        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                OnRefresh();
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
            }
        }

        private ObservableCollection<ComboboxItem> _budgetSourceTypes = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> BudgetSourceTypes
        {
            get => _budgetSourceTypes;
            set => SetProperty(ref _budgetSourceTypes, value);
        }

        /// <summary>
        /// Checkbox select all property
        /// </summary>
        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCensorship
        {
            get
            {
                var itemSelected = Items.Where(x => x.Selected);
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedBhLapKeHoachThuModel != null;

        private void SelectAll(bool select, IEnumerable<BhKhtBHXHModel> models)
        {
            foreach (var model in models)
            {
                if (!model.BDaTongHop.GetValueOrDefault(false))
                {
                    model.Selected = select;
                }
            }
        }
        public bool IsEnableButtonDataShow => TabIndex == ImportTabIndex.Data;

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));

            }
        }
        public string ComboboxDisplayMemberPath => nameof(SelectedNsDonViModel.TenDonViIdDonVi);
        public override Type ContentType => typeof(KeHoachThuIndex);
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Description => "Danh sách kế hoạch thu BHXH, BHYT, BHTN";
        public override string Name => "KH thu BHXH, BHYT, BHTN";
        public override PackIconKind IconKind => PackIconKind.RhombusOutline;
        public ImportKhtBHXHViewModel ImportKhtBHXHViewModel { get; }
        public KeHoachThuDialogViewModel KeHoachThuDialogViewModel { get; }
        public KeHoachThuDetailViewModel KeHoachThuDetailViewModel { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }
        public PrintReportKhtBhxhViewModel PrintReportKhtBhxhViewModel { get; }

        public KeHoachThuIndexViewModel(
            IKhtBHXHService khtBHXHService,
            IKhtBHXHChiTietService khtBHXHChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            KeHoachThuDialogViewModel keHoachThuDialogViewModel,
            KeHoachThuDetailViewModel keHoachThuDetailViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log,
            ImportKhtBHXHViewModel importKhtBHXHViewModel,
            IDanhMucService danhMucService,
            PrintReportKhtBhxhViewModel printReportKhtBhxhViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _khtBHXHService = khtBHXHService;
            _khtBHXHChiTietService = khtBHXHChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _danhMucService = danhMucService;
            KeHoachThuDialogViewModel = keHoachThuDialogViewModel;
            KeHoachThuDetailViewModel = keHoachThuDetailViewModel;
            ImportKhtBHXHViewModel = importKhtBHXHViewModel;
            PrintReportKhtBhxhViewModel = printReportKhtBhxhViewModel;

            ImportKhtBHXHViewModel.ParentPage = this;
            KeHoachThuDialogViewModel.ParentPage = this;
            KeHoachThuDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(OnPrint);
            SearchCommand = new RelayCommand(obj => SearchData());
        }
        private void SearchData()
        {
            if (_bhChungTuModelsView != null)
                _bhChungTuModelsView.Refresh();
        }
        public override void OnCancel()
        {
            base.OnCancel();
            ParentPage.ParentPage.CurrentPage = null;
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OpenDetailDialog((BhKhtBHXHModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedBhLapKeHoachThuModel == null) return;
            if (SelectedBhLapKeHoachThuModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhLapKeHoachThuModel.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhLapKeHoachThuModel.SSoChungTu, SelectedBhLapKeHoachThuModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool BhxhChungTuModelsFilter(object obj)
        {
            if (!(obj is BhKhtBHXHModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SSoQuyetDinh))
                    condition1 = condition1 || temp.SSoQuyetDinh.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.STenDonVi))
                    condition1 = condition1 || temp.STenDonVi.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (SelectedNsDonViModel != null)
            {
                condition2 = condition2 && temp.IID_MaDonVi == SelectedNsDonViModel.IIDMaDonVi;
            }

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BIsKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BIsKhoa == false;
                }
            }
            var result = condition1 && condition2;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            KhtBHXHChiTietCriteria searchCondition = new KhtBHXHChiTietCriteria();
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedBhLapKeHoachThuModel != null)
            {
                var khtChungTu = _khtBHXHService.FindById(SelectedBhLapKeHoachThuModel.Id);
                searchCondition.khtBhxhId = khtChungTu.Id;
                if (khtChungTu != null)
                {
                    var lstKhtBhxhChiTiet = _khtBHXHChiTietService.FindKhtBHXHChiTietByIdBhxh(searchCondition).ToList();
                    //Xóa chứng từ BHXH
                    _khtBHXHService.Delete(khtChungTu);

                    if (!string.IsNullOrEmpty(khtChungTu.STongHop))
                    {
                        var lstSoCtChild = khtChungTu.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _khtBHXHService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _khtBHXHService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ BHXH
                    _khtBHXHChiTietService.RemoveRange(lstKhtBhxhChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ kế hoạch thu BHXH", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    LoadKhtBHXHs();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            
            ImportKhtBHXHViewModel.SavedAction = obj =>
            {
                _importKhtBHXH.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhtBHXHModel)obj);
            };
            ImportKhtBHXHViewModel.Init();
            _importKhtBHXH = new View.SocialInsurance.SocialInsurancePlan.KeHoachThu.ImportKhtBHXH.ImportKhtBHXH { DataContext = ImportKhtBHXHViewModel };
            _importKhtBHXH.ShowDialog();
        }

        private void ConfirmAggregate()
        {
            List<BhKhtBHXHModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.BIsKhoa);
            if (checkAllowAggregate)
            {
                OnAggregate();
            }
            else
            {
                string message = Resources.ConfirmAggregate;
                MessageBoxResult result = MessageBoxHelper.Confirm(message);
                if (result == MessageBoxResult.Yes)
                    OnAggregate();
            }
        }

        private void OnAggregate()
        {
            bool existTongHop = false;
            existTongHop = _khtBHXHService.IsExistChungTuTongHop(_sessionInfo.YearOfWork);
            if (existTongHop)
            {
                MessageBoxResult result = MessageBoxHelper.Confirm(Resources.MesConfirmSaveAggregateDemand);
                if (result != MessageBoxResult.Yes)
                    return;
            }
            //kiểm tra trạng thái các bản ghi
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }
            List<BhKhtBHXHModel> selectedKhtChungTus = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher).ToList();

            KeHoachThuDialogViewModel.Name = "Thêm chứng từ";
            KeHoachThuDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
            KeHoachThuDialogViewModel.BhKhtBHXHModel = new BhKhtBHXHModel();
            KeHoachThuDialogViewModel.isSummary = true;
            KeHoachThuDialogViewModel.ListIdsKhtBHXHSummary = selectedKhtChungTus;
            KeHoachThuDialogViewModel.Init();
            KeHoachThuDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhKhtBHXHModel)obj);
            };
            var addView = new KeHoachThuDialog() { DataContext = KeHoachThuDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }
        private void OnSelectedChange(object obj)
        {
            SelectedBhLapKeHoachThuModel = (BhKhtBHXHModel)obj;
            if (SelectedBhLapKeHoachThuModel is { BIsKhoa: true } || SelectedBhLapKeHoachThuModel == null)
            {
                IsEdit = false;
            }
            else
            {
                IsEdit = true;
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhKhtBHXHModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadKhtBHXHs();
            LoadDonVi();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IID_MaDonVi).ToList();
                predicate = predicate.And(x => idDonVis.Any(y => y == x.IIDMaDonVi));
                var listUnit = _donViService.FindByCondition(predicate).ToList();
                BhDonViModelItems = new ObservableCollection<DonViModel>();
                BhDonViModelItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
                _bhDonViModelsView = CollectionViewSource.GetDefaultView(BhDonViModelItems);
                _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.Loai),
                    ListSortDirection.Ascending));
                _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.TenDonVi),
                    ListSortDirection.Ascending));
            }
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private void LoadKhtBHXHs()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;

            var listChungTu = _khtBHXHService.FindByCondition(_sessionInfo.YearOfWork);
            _lstChungTuOrigin = _mapper.Map<List<BhKhtBHXHModel>>(listChungTu);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<BhKhtBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.IID_MaDonVi.Equals(_sessionInfo.IdDonVi) && x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop)).ToList();
                    var listTongHop = new List<BhKhtBHXHModel>();
                    foreach (var ctTongHop in listCTTongHop)
                    {
                        var parent = _mapper.Map<BhKhtBHXHModel>(ctTongHop);
                        parent.IsExpand = true;
                        listTongHop.Add(parent);

                        if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                        {
                            var listChild = _mapper.Map<List<BhKhtBHXHModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<BhKhtBHXHModel>>(listTongHop);
                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<BhKhtBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
            }

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhKhtBHXHModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(BhKhtBHXHModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }
            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _bhChungTuModelsView.Filter = BhxhChungTuModelsFilter;
        }
        
        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedBhLapKeHoachThuModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _khtBHXHService.LockOrUnlock(ct.Id, isLock);
                var khtBhxh = Items.First(x => x.Id == ct.Id);
                khtBhxh.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ kế hoạch thu", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadKhtBHXHs();
        }

        protected override void OnAdd()
        {
            KeHoachThuDialogViewModel.Name = "Thêm mới kế hoạch thu";
            KeHoachThuDialogViewModel.Description = "Tạo mới kế hoạch thu BHXH, BHYT, BHTN";
            KeHoachThuDialogViewModel.BhKhtBHXHModel = new BhKhtBHXHModel();
            KeHoachThuDialogViewModel.isSummary = false;
            KeHoachThuDialogViewModel.Init();
            KeHoachThuDialogViewModel.SavedAction = obj =>
            {
                var khtChungTu = (BhKhtBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            var exportView = new KeHoachThuDialog() { DataContext = KeHoachThuDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedBhLapKeHoachThuModel != null)
            {
                if (SelectedBhLapKeHoachThuModel.IID_MaDonVi.Equals(_sessionInfo.IdDonVi) && SelectedBhLapKeHoachThuModel.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedBhLapKeHoachThuModel.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedBhLapKeHoachThuModel.SNguoiTao));
                        return;
                    }
                    KeHoachThuDialogViewModel.BhKhtBHXHModel = SelectedBhLapKeHoachThuModel;
                    KeHoachThuDialogViewModel.Name = "Sửa kế hoạch thu";
                    KeHoachThuDialogViewModel.Description = "Cập nhật kế hoạch thu BHXH, BHYT, BHTN";
                    KeHoachThuDialogViewModel.isSummary = false;
                    KeHoachThuDialogViewModel.Init();
                    KeHoachThuDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    KeHoachThuDialogViewModel.ShowDialogHost();
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BIsKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BIsKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuInvalid))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, lstSoChungTuInvalid));
                    return;
                }
            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult dialogResult = MessageBoxHelper.Confirm(message);
            if (dialogResult == MessageBoxResult.Yes)
            {
                LockOrUnLockMultiVoucher();
                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }
        protected override void OnRefresh()
        {
            LoadKhtBHXHs();
        }

        private void UnCheckBoxAll()
        {
            foreach (var item in Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Open Detail
        /// </summary>
        /// <param name="BhKhtBHXHModel"></param>
        private void OpenDetailDialog(BhKhtBHXHModel bhKhtBHXHDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTuTH = Items.FirstOrDefault(item => item.IID_MaDonVi.Equals(idDonViCurrent));
            var listDonViCheckBox = Items.Select(item => new CheckBoxItem
            {
                ValueItem = item.IID_MaDonVi,
                DisplayItem = string.Join("-", item.IID_MaDonVi, item.STenDonVi)
            }).OrderBy(item => item.ValueItem);
            KeHoachThuDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            KeHoachThuDetailViewModel.CtTongHop = chungTuTH;
            KeHoachThuDetailViewModel.IsVoucherSummary = bhKhtBHXHDetail.IID_MaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(bhKhtBHXHDetail.STongHop);
            KeHoachThuDetailViewModel.ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);

            KeHoachThuDetailViewModel.Init();
            var view = new KeHoachThuDetail() { DataContext = KeHoachThuDetailViewModel };
            view.ShowDialog();
        }

        private void SelfRefresh(object sender, EventArgs e)
        {
            OnRefresh();
        }

        public override void Init()
        {
            base.Init();
            _tabIndex = ImportTabIndex.Data;
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadLockStatus();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            KeHoachThuDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhKhtBHXHModel> selectedKhtBhxhs = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedBhLapKeHoachThuModel.STongHop) && SelectedBhLapKeHoachThuModel.STongHop.Contains(x.SSoChungTu)).ToList();
            KeHoachThuDialogViewModel.Name = "Sửa chứng từ";
            KeHoachThuDialogViewModel.Description = "Sửa chứng từ tổng hợp";
            KeHoachThuDialogViewModel.BhKhtBHXHModel = SelectedBhLapKeHoachThuModel;
            KeHoachThuDialogViewModel.ListIdsKhtBHXHSummary = selectedKhtBhxhs;
            KeHoachThuDialogViewModel.isSummary = true;
            KeHoachThuDialogViewModel.Init();
            KeHoachThuDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
            };
            var addView = new KeHoachThuDialog() { DataContext = KeHoachThuDialogViewModel };
            DialogHost.Show(addView);
        }

        /// <summary>
        /// Xuất excel chứng từ lập kế hoạch thu BHXH
        /// </summary>
        private void OnExportAggregateData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<BhKhtBHXHModel> khtBhxhModelsSummary = Items.Where(x => x.Selected).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var yearOfWork = _sessionInfo.YearOfWork;

                    foreach (var item in khtBhxhModelsSummary)
                    {
                        var voucherItem = _khtBHXHService.FindById(item.Id);
                        var voucherUnit = _donViService.FindByMaDonViAndNamLamViec(voucherItem.IID_MaDonVi, yearOfWork);
                        KhtBHXHChiTietCriteria searchCondition = new KhtBHXHChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.MaDonVi = item.IID_MaDonVi;
                        searchCondition.khtBhxhId = item.Id;
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        var khtMucLucsOrder = _khtBHXHChiTietService.FindBhKhtBHXHChiTietByCondition(searchCondition).ToList();
                        var lstData = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietModel>>(khtMucLucsOrder).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        CalculateData(lstData);
                        var lstQueryData = _mapper.Map<ObservableCollection<BhKhtBHXHChiTietQuery>>(lstData).ToList();
                        foreach (var row in lstQueryData)
                        {
                            row.FLuongChinh = Math.Round(row.FLuongChinh.GetValueOrDefault());
                            row.FNghiOm = Math.Round(row.FNghiOm.GetValueOrDefault());
                            row.FPCTNNghe = Math.Round(row.FPCTNNghe.GetValueOrDefault());
                            row.FPCTNVuotKhung = Math.Round(row.FPCTNVuotKhung.GetValueOrDefault());
                            row.FPhuCapChucVu = Math.Round(row.FPhuCapChucVu.GetValueOrDefault());
                            row.FHSBL = Math.Round(row.FHSBL.GetValueOrDefault());
                            row.FThuBHXHNguoiLaoDong = Math.Round(row.FThuBHXHNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHXHNguoiSuDungLaoDong = Math.Round(row.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FThuBHYTNguoiLaoDong = Math.Round(row.FThuBHYTNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHYTNguoiSuDungLaoDong = Math.Round(row.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FThuBHTNNguoiLaoDong = Math.Round(row.FThuBHTNNguoiLaoDong.GetValueOrDefault());
                            row.FThuBHTNNguoiSuDungLaoDong = Math.Round(row.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault());
                            row.FTongQuyTienLuongNam = Math.Round(row.FTongQuyTienLuongNam.GetValueOrDefault());
                            row.FTongThuBHXH = Math.Round(row.FTongThuBHXH.GetValueOrDefault());
                            row.FTongThuBHYT = Math.Round(row.FTongThuBHYT.GetValueOrDefault());
                            row.FTongThuBHTN = Math.Round(row.FTongThuBHTN.GetValueOrDefault());
                            row.FTongCong = Math.Round(row.FTongCong.GetValueOrDefault());
                        }
                        Data.Add("TotalQSBQ", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.IQSBQNam));
                        Data.Add("TotalLuongChinh", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FLuongChinh));
                        Data.Add("TotalPCCV", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FPhuCapChucVu));
                        Data.Add("TotalPCTNN", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNNghe));
                        Data.Add("TotalPCTNVK", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FPCTNVuotKhung));
                        Data.Add("TotalNghiOm", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FNghiOm));
                        Data.Add("TotalHSBL", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FHSBL));
                        Data.Add("TotalTongQTLN", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FTongQuyTienLuongNam));
                        Data.Add("TotalBHXHNld", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiLaoDong));
                        Data.Add("TotalBHXHNsd", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHXHNguoiSuDungLaoDong));
                        Data.Add("TotalTongBHXH", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHXH));
                        Data.Add("TotalBHYTNld", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiLaoDong));
                        Data.Add("TotalBHYTNsd", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHYTNguoiSuDungLaoDong));
                        Data.Add("TotalTongBHYT", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHYT));
                        Data.Add("TotalBHTNNld", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiLaoDong));
                        Data.Add("TotalBHTNNsd", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FThuBHTNNguoiSuDungLaoDong));
                        Data.Add("TotalTongBHTN", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FTongThuBHTN));
                        Data.Add("TotalTongCong", lstQueryData.Where(x => x.IsHangCha == false).Sum(x => x.FTongCong));
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("TieuDe1", "BÁO CÁO CHI TIẾT KẾ HOẠCH THU BHXH NĂM " + _sessionInfo.YearOfWork);
                        Data.Add("DonVi", _sessionInfo.TenDonVi);
                        Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        Data.Add("YearOfWork", "Năm " + _sessionInfo.YearOfWork);
                        Data.Add("h2", "");
                        Data.Add("h1", "");
                        Data.Add("DonViChungTu", item.STenDonVi);
                        Data.Add("ListMLNS", lstQueryData);
                        Data.Add("ListData", lstQueryData);
                        Data.Add("FormatNumber", formatNumber);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_KHT, ExportFileName.RPT_BH_KHT_CHUNGTU_CHITIET_BHXH);
                        fileNamePrefix = item.SSoChungTu + "_" + voucherUnit.TenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhKhtBHXHChiTietQuery, BhKhtBHXHChiTietModel, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, Data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);

                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnPrint(object param)
        {
            var bhxhCheckPrintType = (BHXHCheckPrintType)((int)param);
            object content;
            switch (bhxhCheckPrintType)
            {
                case BHXHCheckPrintType.KE_HOACH_THU_BHXH_BHYT_BHTN:
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = bhxhCheckPrintType;

                    var listDonViCheckBox = Items.Select(item => new CheckBoxItem
                    {
                        ValueItem = item.IID_MaDonVi,
                        DisplayItem = string.Join("-", item.IID_MaDonVi, item.STenDonVi)
                    }).OrderBy(item => item.ValueItem);
                    PrintReportKhtBhxhViewModel.ListDonVi = new ObservableCollection<CheckBoxItem>(listDonViCheckBox);
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)bhxhCheckPrintType;
                    PrintReportKhtBhxhViewModel.Init();
                    content = new PrintKhtBHXH
                    {
                        DataContext = PrintReportKhtBhxhViewModel
                    };
                    break;
                case BHXHCheckPrintType.PHU_LUC_II:
                case BHXHCheckPrintType.PHU_LUC_III:
                case BHXHCheckPrintType.PHU_LUC_IV:
                case BHXHCheckPrintType.PHU_LUC_V:
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = bhxhCheckPrintType;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)bhxhCheckPrintType;
                    PrintReportKhtBhxhViewModel.Init();
                    content = new PrintKhtBHXH
                    {
                        DataContext = PrintReportKhtBhxhViewModel
                    };
                    break;
                case BHXHCheckPrintType.DU_TOAN_THU_CHI_TONG_HOP:
                    PrintReportKhtBhxhViewModel.BHXHCheckPrintType = bhxhCheckPrintType;
                    PrintReportKhtBhxhViewModel.ReportNameTypeValue = (int)bhxhCheckPrintType;
                    PrintReportKhtBhxhViewModel.Init();
                    content = new PrintKhtBHXH
                    {
                        DataContext = PrintReportKhtBhxhViewModel
                    };
                    break;
                default:
                    content = null;
                    break;
            }

            if (content != null)
            {
                DialogHost.Show(content, SystemConstants.ROOT_DIALOG, null, null);
            }
        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        private void CalculateData(List<BhKhtBHXHChiTietModel> lstKhtChungTuChiTiet)
        {
            lstKhtChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.IQSBQNam = 0;
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FThuBHXHNguoiLaoDong = 0;
                    x.FThuBHXHNguoiSuDungLaoDong = 0;
                    x.FThuBHYTNguoiLaoDong = 0;
                    x.FThuBHYTNguoiSuDungLaoDong = 0;
                    x.FThuBHTNNguoiLaoDong = 0;
                    x.FThuBHTNNguoiSuDungLaoDong = 0;
                    return x;
                }).ToList();
            var temp = lstKhtChungTuChiTiet.Where(x => !x.IsHangCha);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstKhtChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhKhtBHXHChiTietModel item, List<BhKhtBHXHChiTietModel> lstKhtChungTuChiTiet)
        {
            var model = lstKhtChungTuChiTiet.FirstOrDefault(x => x.IIDMucLucNganSach == idParent);
            if (model == null) return;
            model.IQSBQNam += item.IQSBQNam.GetValueOrDefault();
            model.FLuongChinh += item.FLuongChinh.GetValueOrDefault();
            model.FPhuCapChucVu += item.FPhuCapChucVu.GetValueOrDefault();
            model.FPCTNNghe += item.FPCTNNghe.GetValueOrDefault();
            model.FPCTNVuotKhung += item.FPCTNVuotKhung.GetValueOrDefault();
            model.FNghiOm += item.FNghiOm.GetValueOrDefault();
            model.FHSBL += item.FHSBL.GetValueOrDefault();
            model.FThuBHXHNguoiLaoDong += item.FThuBHXHNguoiLaoDong.GetValueOrDefault();
            model.FThuBHXHNguoiSuDungLaoDong += item.FThuBHXHNguoiSuDungLaoDong.GetValueOrDefault();
            model.FThuBHYTNguoiLaoDong += item.FThuBHYTNguoiLaoDong.GetValueOrDefault();
            model.FThuBHYTNguoiSuDungLaoDong += item.FThuBHYTNguoiSuDungLaoDong.GetValueOrDefault();
            model.FThuBHTNNguoiLaoDong += item.FThuBHTNNguoiLaoDong.GetValueOrDefault();
            model.FThuBHTNNguoiSuDungLaoDong += item.FThuBHTNNguoiSuDungLaoDong.GetValueOrDefault();
            CalculateParent(model.IdParent, item, lstKhtChungTuChiTiet);
        }
    }
}

