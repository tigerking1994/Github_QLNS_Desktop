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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.Import;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatBoSung
{
    public class CapPhatBoSungIndexViewModel : GridViewModelBase<BhCpBsChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IBhCpBsChungTuService _cpBsService;
        private readonly IBhCpBsChungTuChiTietService _cpBsChiTietService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private readonly IBhDmCoSoYTeService _iBhDmCoSoYTeService;

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
                    var lstSelectedKhoa = lstSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = lstSelected.Where(x => !x.BKhoa).ToList();
                    if (lstSelectedKhoa.Any() && lstSelectedMo.Any())
                    {
                        result = false;
                    }
                    else if (lstSelectedKhoa.Any())
                    {
                        IsLock = true;
                        result = true;
                    }
                    else if (lstSelectedMo.Any())
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

        private BhCpBsChungTuModel _selectedChungTu;
        public BhCpBsChungTuModel SelectedChungTu
        {
            get => _selectedChungTu;
            set
            {
                SetProperty(ref _selectedChungTu, value);
                if (_selectedChungTu != null)
                {
                    IsLock = _selectedChungTu.BKhoa;
                }
                else
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedChungTu == null)
                {
                    IsEdit = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhCpBsChungTuModel> _lstChungTuOrigin;
        public List<BhCpBsChungTuModel> LstChungTuOrigin
        {
            get => _lstChungTuOrigin;
            set
            {
                SetProperty(ref _lstChungTuOrigin, value);
            }
        }

        private ComboboxItem _selectedLoaiKinhPhi;
        public ComboboxItem SelectedLoaiKinhPhi
        {
            get => _selectedLoaiKinhPhi;
            set
            {
                SetProperty(ref _selectedLoaiKinhPhi, value);
                if (_selectedLoaiKinhPhi != null)
                {
                    this.LoadData();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiKinhPhi;
        public ObservableCollection<ComboboxItem> ItemsLoaiKinhPhi
        {
            get => _itemsLoaiKinhPhi;
            set => SetProperty(ref _itemsLoaiKinhPhi, value);
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
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedChungTu != null;

        private void SelectAll(bool select, IEnumerable<BhCpBsChungTuModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
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
        public override Type ContentType => typeof(CapPhatBoSungIndex);
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Description => "Danh sách cấp bổ sung KP KCB BHYT";
        public override string Name => "Cấp bổ sung KP KCB BHYT";
        public override PackIconKind IconKind => PackIconKind.ViewList;
        public CapPhatBoSungImportViewModel CapPhatBoSungImportViewModel { get; }
        public CapPhatBoSungDialogViewModel CapPhatBoSungDialogViewModel { get; }
        public CapPhatBoSungDetailViewModel CapPhatBoSungDetailViewModel { get; }
        public RelayCommand SelectionChangedCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand LockCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataFilterCommand { get; }
        public RelayCommand UploadFileCommand { get; }
        public CapPhatBoSungReportViewModel CapPhatBoSungReportViewModel { get; }

        public CapPhatBoSungIndexViewModel(
            IBhCpBsChungTuService cpBsService,
            IBhCpBsChungTuChiTietService cpBsChungTuChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            CapPhatBoSungDialogViewModel capPhatBoSungDialogViewModel,
            CapPhatBoSungDetailViewModel capPhatBoSungDetailViewModel,
            CapPhatBoSungImportViewModel capPhatBoSungImportViewModel,
            CapPhatBoSungReportViewModel capPhatBoSungReportViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            IBhDmCoSoYTeService iBhDmCoSoYTeService,
            ISysAuditLogService log)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _cpBsService = cpBsService;
            _cpBsChiTietService = cpBsChungTuChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _iBhDmCoSoYTeService = iBhDmCoSoYTeService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            CapPhatBoSungDialogViewModel = capPhatBoSungDialogViewModel;
            CapPhatBoSungDetailViewModel = capPhatBoSungDetailViewModel;
            CapPhatBoSungImportViewModel = capPhatBoSungImportViewModel;
            CapPhatBoSungReportViewModel = capPhatBoSungReportViewModel;

            CapPhatBoSungImportViewModel.ParentPage = this;
            CapPhatBoSungDialogViewModel.ParentPage = this;
            CapPhatBoSungDetailViewModel.ParentPage = this;
            CapPhatBoSungReportViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportDataCommand = new RelayCommand(obj => OnExportDataCommand());
            AggregateCommand = new RelayCommand(obj => ConfirmAggregate());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintCommand = new RelayCommand(obj => OnPrint(obj));
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
            OpenDetailDialog((BhCpBsChungTuModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedChungTu == null)
                return;
            if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedChungTu.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedChungTu.SSoChungTu, SelectedChungTu.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool ChungTuModelsFilter(object obj)
        {
            if (!(obj is BhCpBsChungTuModel temp))
                return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            var condition2 = true;
            var condition3 = true;
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

            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    condition2 = condition2 && temp.BKhoa == true;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    condition2 = condition2 && temp.BKhoa == false;
                }
            }

            if (SelectedLoaiKinhPhi != null && SelectedLoaiKinhPhi.ValueItem != "-1")
            {
                if (SelectedLoaiKinhPhi.ValueItem == "1")
                {
                    // Do khi lưu dữ liệu với trường hợp "Kinh phí KCB BHYT quân nhân" thì ILoaiKinhPhi có thể lưu giá trị 0 hoặc 1
                    condition3 = condition3 && (temp.ILoaiKinhPhi == 0 || temp.ILoaiKinhPhi == 1);
                }
                else
                {
                    condition3 = condition3 && temp.ILoaiKinhPhi == Convert.ToInt32(SelectedLoaiKinhPhi.ValueItem);
                }
            }
            var result = condition1 && condition2 && condition3;
            temp.IsFilter = result;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            BhCpBsChungTuChiTietCriteria searchCondition = new BhCpBsChungTuChiTietCriteria();
            if (result != NSDialogResult.Yes)
                return;
            DateTime dtNow = DateTime.Now;
            if (SelectedChungTu != null)
            {
                var khtChungTu = _cpBsService.FindById(SelectedChungTu.Id);
                searchCondition.IIDCTCapPhatBS = khtChungTu.Id;
                if (khtChungTu != null)
                {
                    var lstChungTuChiTiet = _cpBsChiTietService.FindChungTuChiTietByChungTuId(searchCondition).ToList();
                    //Xóa chứng từ
                    _cpBsService.Delete(khtChungTu);

                    if (!string.IsNullOrEmpty(khtChungTu.SDSSoChungTuTongHop))
                    {
                        var lstSoCtChild = khtChungTu.SDSSoChungTuTongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _cpBsService.FindChungTuDaTongHopBySCT(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _cpBsService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _cpBsChiTietService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    OnRefresh();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            CapPhatBoSungImportViewModel.Init();
            CapPhatBoSungImportViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhCpBsChungTuModel)obj);
            };
            CapPhatBoSungImportViewModel.ShowDialog();
        }

        private void ConfirmAggregate()
        {
            List<BhCpBsChungTuModel> selectedSktChungTus = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedSktChungTus.All(x => x.BKhoa);
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
            existTongHop = _cpBsService.IsExistChungTuTongHop(_sessionInfo.YearOfWork);
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
            List<BhCpBsChungTuModel> selectedVouchers = Items.Where(x => x.Selected && x.BKhoa && !x.IsSummaryVocher).ToList();

            CapPhatBoSungDialogViewModel.Name = "Thêm chứng từ";
            CapPhatBoSungDialogViewModel.Description = "Tạo mới chứng từ tổng hợp";
            CapPhatBoSungDialogViewModel.Model = new BhCpBsChungTuModel();
            CapPhatBoSungDialogViewModel.isSummary = true;
            CapPhatBoSungDialogViewModel.ListIdsChungTuSummary = selectedVouchers;
            CapPhatBoSungDialogViewModel.Init();
            CapPhatBoSungDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhCpBsChungTuModel)obj);
            };
            var addView = new CapPhatBoSungDialog() { DataContext = CapPhatBoSungDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG, null, ClosingEventHandler);
        }
        private void OnSelectedChange(object obj)
        {
            SelectedChungTu = (BhCpBsChungTuModel)obj;
            if (SelectedChungTu is { BKhoa: true } || SelectedChungTu == null)
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
                OpenDetailDialog((BhCpBsChungTuModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadCapPhatChungTu();
            OnPropertyChanged(nameof(IsCensorship));
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

        private void LoadCapPhatChungTu()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var listChungTu = _cpBsService.FindByYear(_sessionInfo.YearOfWork);
            _lstChungTuOrigin = _mapper.Map<List<BhCpBsChungTuModel>>(listChungTu);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<BhCpBsChungTuModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()).OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop)).ToList();
                    var listTongHop = new List<BhCpBsChungTuModel>();
                    foreach (var ctTongHop in listCTTongHop)
                    {
                        var parent = _mapper.Map<BhCpBsChungTuModel>(ctTongHop);
                        parent.IsExpand = true;
                        listTongHop.Add(parent);

                        if (!string.IsNullOrEmpty(ctTongHop.SDSSoChungTuTongHop))
                        {
                            var listChild = _mapper.Map<List<BhCpBsChungTuModel>>(listChungTu.Where(x => ctTongHop.SDSSoChungTuTongHop != null && ctTongHop.SDSSoChungTuTongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<BhCpBsChungTuModel>>(listTongHop.OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<BhCpBsChungTuModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()).OrderBy(x => x.ILoaiKinhPhi).ThenBy(x => x.IQuy));
            }

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhCpBsChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(BhCpBsChungTuModel.IsCollapse))
                    {
                        ExpandChild();
                    }
                };
            }
            _bhChungTuModelsView = CollectionViewSource.GetDefaultView(Items);
            _bhChungTuModelsView.Filter = ChungTuModelsFilter;
        }
        
        private void ExpandChild()
        {
            if (Items != null)
            {
                Items.Where(n => n.SoChungTuParent == SelectedChungTu.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BKhoa;
            foreach (var ct in lstSelected)
            {
                _cpBsService.LockOrUnlock(ct.Id, isLock);
                var chungTu = Items.First(x => x.Id == ct.Id);
                chungTu.BKhoa = !ct.BKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadCapPhatChungTu();
        }

        protected override void OnAdd()
        {
            CapPhatBoSungDialogViewModel.Name = "Thêm mới chứng từ";
            CapPhatBoSungDialogViewModel.Description = "Tạo mới chứng từ";
            CapPhatBoSungDialogViewModel.Model = new BhCpBsChungTuModel();
            CapPhatBoSungDialogViewModel.isSummary = false;
            CapPhatBoSungDialogViewModel.Init();
            CapPhatBoSungDialogViewModel.SavedAction = obj =>
            {
                var khtChungTu = (BhCpBsChungTuModel)obj;
                this.LoadData();
                OpenDetailDialog(khtChungTu);
            };
            var exportView = new CapPhatBoSungDialog() { DataContext = CapPhatBoSungDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedChungTu != null)
            {
                if (SelectedChungTu.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedChungTu.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedChungTu.SNguoiTao));
                        return;
                    }
                    CapPhatBoSungDialogViewModel.Model = SelectedChungTu;
                    CapPhatBoSungDialogViewModel.Name = "Sửa chứng từ";
                    CapPhatBoSungDialogViewModel.Description = "Cập nhật thông tin chứng từ số nhu cầu";
                    CapPhatBoSungDialogViewModel.isSummary = false;
                    CapPhatBoSungDialogViewModel.Init();
                    CapPhatBoSungDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    CapPhatBoSungDialogViewModel.ShowDialogHost();
                }
            }
        }

        private void OnLock(object obj)
        {
            if (IsLock)
            {
                string lstSoChungTu = string.Join(", ", Items.Where(n => n.Selected && (bool)n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (userAgency.All(x => x.Loai != LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUnlock, lstSoChungTu));
                    return;
                }
                string lstSoChungTuDaTongHop = string.Join(", ", Items.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && (bool)n.BKhoa && !n.IIDMaDonVi.Equals(_sessionInfo.IdDonVi)).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(lstSoChungTuDaTongHop))
                {
                    MessageBoxHelper.Warning(string.Format(Resources.AlertUnlockAggregatedVoucher, lstSoChungTuDaTongHop));
                    return;
                }
            }
            else
            {
                string lstSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !(bool)n.BKhoa).Select(n => n.SSoChungTu));

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
            LoadCapPhatChungTu();
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
        /// <param name="BhCpBsChungTuModel"></param>
        private void OpenDetailDialog(BhCpBsChungTuModel bhKhtBHXHDetail, params bool[] isNew)
        {
            CapPhatBoSungDetailViewModel.Model = ObjectCopier.Clone(bhKhtBHXHDetail);
            CapPhatBoSungDetailViewModel.IsVoucherSummary = !string.IsNullOrEmpty(bhKhtBHXHDetail.SDSSoChungTuTongHop);

            CapPhatBoSungDetailViewModel.Init();
            var view = new CapPhatBoSungDetail() { DataContext = CapPhatBoSungDetailViewModel };
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
            LoadDanhMucLoaiChi();
            CapPhatBoSungDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {
            //kiểm tra trạng thái các bản ghi
            List<BhCpBsChungTuModel> selectedVoucher = LstChungTuOrigin.Where(x => !string.IsNullOrEmpty(SelectedChungTu.SDSSoChungTuTongHop) && SelectedChungTu.SDSSoChungTuTongHop.Contains(x.SSoChungTu)).ToList();
            CapPhatBoSungDialogViewModel.Name = "Sửa chứng từ tổng hợp";
            CapPhatBoSungDialogViewModel.Description = "Sửa chứng từ cấp phát bổ sung tổng hợp";
            CapPhatBoSungDialogViewModel.Model = SelectedChungTu;
            CapPhatBoSungDialogViewModel.ListIdsChungTuSummary = selectedVoucher;
            CapPhatBoSungDialogViewModel.isSummary = true;
            CapPhatBoSungDialogViewModel.Init();
            CapPhatBoSungDialogViewModel.SavedAction = obj =>
            {
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
            };
            var addView = new CapPhatBoSungDialog() { DataContext = CapPhatBoSungDialogViewModel };
            DialogHost.Show(addView);
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// 

        private void OnExportDataCommand()
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
                    List<BhCpBsChungTuModel> chungTu = Items.Where(x => x.Selected).ToList();
                    var currentDonVi = GetNsDonViOfCurrentUser();
                    var yearOfWork = _sessionService.Current.YearOfWork;

                    foreach (var item in chungTu)
                    {
                        //List<string> lstCsYTe = item.SCoSoYTe.Split(",").ToList();
                        BhCpBsChungTuChiTietCriteria searchCondition = new BhCpBsChungTuChiTietCriteria();
                        searchCondition.NamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IQuy = item.IQuy;
                        searchCondition.IIDCTCapPhatBS = item.Id;
                        searchCondition.LstLns = item.SDslns.Split(",").Distinct().ToList();
                        searchCondition.LstCSYT = item.SCoSoYTe;
                        var exportVoucher = _cpBsChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                        var exportVoucherMap = _mapper.Map<ObservableCollection<BhCpBsChungTuChiTietModel>>(exportVoucher).ToList();

                        //var coSoYTe = _iBhDmCoSoYTeService.GetCSYTByMa(dv, yearOfWork);
                        var exportData = _cpBsChiTietService.ExportTheoCoSoYTe(item.Id, item.SCoSoYTe, yearOfWork);
                        var exportDataMap = _mapper.Map<ObservableCollection<BhCpBsChungTuChiTietModel>>(exportData).ToList();
                        CalculateData(exportDataMap);
                        var total = CalculateTotal(exportDataMap);
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        string SoDaCap = item.IQuy switch
                        {
                            1 => $"Số đã cấp, thanh toán quý 4/{yearOfWork - 1} và tạm ứng quý 1/{yearOfWork}",
                            _ => $"Số đã cấp, thanh toán quý {item.IQuy - 1}/{yearOfWork} và tạm ứng quý {item.IQuy}/{yearOfWork}"
                        };
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        Data.Add("TitleFirst", $"CẤP PHÁT BỔ SUNG KCB BHYT {yearOfWork}");
                        Data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoChungTu}, ngày: {DateUtils.Format(item.DNgayChungTu)})");
                        Data.Add("Quy", item.IQuy);
                        Data.Add("Nam", yearOfWork);
                        Data.Add("SoDaCap", SoDaCap);
                        Data.Add("FormatNumber", formatNumber);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("Cap1", currentDonVi.TenDonVi);
                        Data.Add("DonVi", _sessionService.Current.TenDonVi);
                        Data.Add("YearWork", yearOfWork);
                        Data.Add("YearWorkOld", yearOfWork - 1);
                        Data.Add("H2", "Lữ đoàn X");
                        Data.Add("H1", "Lữ đoàn X");
                        Data.Add("Items", exportDataMap.Where(x => !string.IsNullOrEmpty(x.STenCSYT)));
                        Data.Add("MLNS", exportVoucherMap);
                        Data.Add("TenCSYT", "");
                        Data.Add("TongDaQuyetToan", total.FTongDaQuyetToan);
                        Data.Add("TongDaCapUng", total.FTongDaCapUng);
                        Data.Add("TongThua", total.FTongThua);
                        Data.Add("TongThieu", total.FTongThieu);
                        Data.Add("TongSoCapBoSung", total.FTongSoCapBoSung);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_CPBS, ExportFileName.RPT_BH_CPBS_CHUNG_TU_CHITIET);
                        fileNamePrefix = item.SSoChungTu + "_" + item.SSoQuyetDinh;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhCpBsChungTuModel, BhDmMucLucNganSach, BhCpBsChungTuChiTietModel>(templateFileName, Data);
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
            try
            {
                int dialogType = (int)param;
                switch (dialogType)
                {
                    case (int)CapPhatBoSungPrintType.PRINT_IN_KE_HOACH:
                        CapPhatBoSungReportViewModel.IsEnableLNS = true;
                        CapPhatBoSungReportViewModel.IsEnableKehoach = false;
                        CapPhatBoSungReportViewModel.IsEnableThongTri = true;
                        CapPhatBoSungReportViewModel.IsEnableTongTop = true;
                        CapPhatBoSungReportViewModel.Init();
                        var view1 = new CapPhatBoSungReport
                        {
                            DataContext = CapPhatBoSungReportViewModel
                        };
                        DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)CapPhatBoSungPrintType.PRINT_IN_THONG_TRI_TONG_HOP:
                        CapPhatBoSungReportViewModel.IsEnableLNS = true;
                        CapPhatBoSungReportViewModel.IsEnableThongTri = true;
                        CapPhatBoSungReportViewModel.IsEnableTongTop = false;
                        CapPhatBoSungReportViewModel.IsEnableKehoach = true;
                        CapPhatBoSungReportViewModel.Init();
                        var view2 = new CapPhatBoSungReport
                        {
                            DataContext = CapPhatBoSungReportViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    case (int)CapPhatBoSungPrintType.PRINT_IN_THONG_TRI:
                        CapPhatBoSungReportViewModel.IsEnableLNS = true;
                        CapPhatBoSungReportViewModel.IsEnableThongTri = false;
                        CapPhatBoSungReportViewModel.IsEnableTongTop = true;
                        CapPhatBoSungReportViewModel.IsEnableKehoach = true;
                        CapPhatBoSungReportViewModel.Init();
                        var view3 = new CapPhatBoSungReport
                        {
                            DataContext = CapPhatBoSungReportViewModel
                        };
                        DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        private void CalculateData(List<BhCpBsChungTuChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .ForAll(x =>
                {
                    x.FDaQuyetToan = 0;
                    x.FDaCapUng = 0;
                    //x.FThuaThieu = 0
                    x.FThua = 0;
                    x.FThieu = 0;
                    x.FSoCapBoSung = 0;
                });
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhCpBsChungTuChiTietModel item, List<BhCpBsChungTuChiTietModel> lstKhtChungTuChiTiet)
        {
            var model = lstKhtChungTuChiTiet.FirstOrDefault(x => x.IIdMlns == idParent);
            if (model == null)
                return;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FDaCapUng += item.FDaCapUng;
            //model.FThuaThieu += item.FThuaThieu
            model.FThua += item.FThua;
            model.FThieu += item.FThieu;
            model.FSoCapBoSung += item.FSoCapBoSung;
            CalculateParent(model.IdParent, item, lstKhtChungTuChiTiet);
        }

        private BhCpBsChungTuModel CalculateTotal(List<BhCpBsChungTuChiTietModel> listData)
        {
            BhCpBsChungTuModel chungTu = new BhCpBsChungTuModel();
            chungTu.FTongDaQuyetToan = 0;
            chungTu.FTongDaCapUng = 0;
            //chungTu.FTongThuaThieu = 0
            chungTu.FTongThieu = 0;
            chungTu.FTongThua = 0;
            chungTu.FTongSoCapBoSung = 0;
            var listChildren = listData.Where(x => x.IsEditable).ToList();
            foreach (var item in listChildren)
            {
                chungTu.FTongDaQuyetToan += item.FDaQuyetToan.GetValueOrDefault();
                chungTu.FTongDaCapUng += item.FDaCapUng.GetValueOrDefault();
                //chungTu.FTongThuaThieu += item.FThuaThieu.GetValueOrDefault()
                chungTu.FTongThua += item.FThua.GetValueOrDefault();
                chungTu.FTongThieu += item.FThieu.GetValueOrDefault();
                chungTu.FTongSoCapBoSung += item.FSoCapBoSung.GetValueOrDefault();
            }

            return chungTu;
        }

        #region Load danh muc loai chi
        private void LoadDanhMucLoaiChi()
        {
            ObservableCollection<ComboboxItem> lstKinhPhi = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QUAN_NHAN,
                    HiddenValue = LNSValue.LNS_9040001
                },
                new ComboboxItem
                {
                    ValueItem = ((int)TypeCapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD).ToString(),
                    DisplayItem = CapKinhPhiBHYT.KINH_PHI_KCB_BHYT_QN_NLD,
                    HiddenValue = LNSValue.LNS_9040002
                },

            };
            ItemsLoaiKinhPhi = lstKinhPhi;
            ItemsLoaiKinhPhi.Insert(0, new ComboboxItem
            {
                DisplayItem = "--Tất cả--",
                ValueItem = "-1",
                HiddenValue = "-1",
            });

            SelectedLoaiKinhPhi = ItemsLoaiKinhPhi.First();
            OnPropertyChanged(nameof(ItemsLoaiKinhPhi));
        }
        #endregion
    }
}
