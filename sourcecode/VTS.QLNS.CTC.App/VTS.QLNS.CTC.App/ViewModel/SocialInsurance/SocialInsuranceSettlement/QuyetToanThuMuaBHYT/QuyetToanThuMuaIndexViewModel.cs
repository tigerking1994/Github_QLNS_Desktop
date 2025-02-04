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
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.ImportExcel;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.ImportExcel;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuMuaBHYT
{
    public class QuyetToanThuMuaIndexViewModel : GridViewModelBase<BhQttmBHYTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IQttmBHYTService _voucherService;
        private readonly IQttmBHYTChiTietService _voucherDetailService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly ISysAuditLogService _log;
        private IDanhMucService _danhMucService;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private ICollectionView _bhDonViModelsView;
        private ICollectionView _bhQuyModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private DonVi _aggregateAgency;
        private QuyetToanThuMuaImport _quyetToanThuMuaImport;
        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit = TabIndex == ImportTabIndex.Data ? SelectedItem != null && !SelectedItem.BIsKhoa : false;
            set => SetProperty(ref _isEdit, value);
        }
        private bool _isDelete;
        public bool IsDelete
        {
            get => _isDelete = (TabIndex == ImportTabIndex.Data || TabIndex == ImportTabIndex.Aggregate) ? SelectedItem != null && !SelectedItem.BIsKhoa : false;
            set => SetProperty(ref _isDelete, value);
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

        private DonViModel _selectedDonViModel;
        public DonViModel SelectedDonViModel
        {
            get => _selectedDonViModel;
            set
            {
                SetProperty(ref _selectedDonViModel, value);
                SearchData();
            }
        }

        private BhQttQuarterQuery _selectedQTTMQuarterModel;
        public BhQttQuarterQuery SelectedQTTMQuarterModel
        {
            get => _selectedQTTMQuarterModel;
            set
            {
                SetProperty(ref _selectedQTTMQuarterModel, value);
                SearchData();
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private BhQttmBHYTModel _selectedBhQTTMModel;
        public BhQttmBHYTModel SelectedBhQTTMModel
        {
            get => _selectedBhQTTMModel;
            set
            {
                SetProperty(ref _selectedBhQTTMModel, value);
                if (_selectedBhQTTMModel != null)
                {
                    IsLock = _selectedBhQTTMModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                    IsDelete = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedBhQTTMModel == null)
                {
                    IsEdit = false;
                    IsDelete = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhQttmBHYTModel> _lstChungTuOrigin;
        public List<BhQttmBHYTModel> LstChungTuOrigin
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

        private ObservableCollection<BhQttQuarterQuery> _quarterYearItems = new ObservableCollection<BhQttQuarterQuery>();

        public ObservableCollection<BhQttQuarterQuery> QuarterYearItems
        {
            get => _quarterYearItems;
            set => SetProperty(ref _quarterYearItems, value);
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
                return itemSelected.Any() && itemSelected.All(x => !x.IsSummaryVocher && x.BIsKhoa);
            }
        }

        public bool IsExportAggregateData => Items != null && Items.Any(n => n.Selected);
        public bool IsExportDataFilter => _selectedBhQTTMModel != null;

        private void SelectAll(bool select, IEnumerable<BhQttmBHYTModel> models)
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
                OnPropertyChanged(nameof(IsButtonEnable));
            }
        }

        private List<ComboboxItem> _quarters;
        public List<ComboboxItem> Quarters
        {
            get => _quarters;
            set => SetProperty(ref _quarters, value);
        }
        public string ComboboxDisplayMemberPath => nameof(SelectedDonViModel.TenDonViIdDonVi);
        public string ComboboxQuarterDisplayMemberPath => nameof(SelectedQTTMQuarterModel.SQuyNamMoTa);
        public override Type ContentType => typeof(QuyetToanThuMuaIndex);
        public override string GroupName => MenuItemContants.GROUP_QTT_THU;
        public override string Description => "Danh sách báo cáo QT thu mua BHYT thân nhân";
        public override string Name => "QT thu mua BHYT thân nhân";
        public override PackIconKind IconKind => PackIconKind.BankTransferIn;
        public QuyetToanThuMuaImportViewModel QuyetToanThuMuaImportViewModel { get; }
        public QuyetToanThuMuaDialogViewModel QuyetToanThuMuaDialogViewModel { get; }
        public QuyetToanThuMuaDetailViewModel QuyetToanThuMuaDetailViewModel { get; }
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
        public PrintQuyetToanThuMuaViewModel PrintQuyetToanThuMuaViewModel { get; }

        public QuyetToanThuMuaIndexViewModel(
            IQttmBHYTService qttmBHYTService,
            IQttmBHYTChiTietService qttmBHYTChiTietService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            QuyetToanThuMuaDialogViewModel quyetToanThuMuaDialogViewModel,
            QuyetToanThuMuaDetailViewModel quyetToanThuMuaDetailViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log,
            IDanhMucService danhMucService,
            QuyetToanThuMuaImportViewModel quyetToanThuMuaImportViewModel,
            PrintQuyetToanThuMuaViewModel printQuyetToanThuMuaViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _voucherService = qttmBHYTService;
            _voucherDetailService = qttmBHYTChiTietService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            QuyetToanThuMuaDialogViewModel = quyetToanThuMuaDialogViewModel;
            QuyetToanThuMuaDetailViewModel = quyetToanThuMuaDetailViewModel;
            QuyetToanThuMuaImportViewModel = quyetToanThuMuaImportViewModel;
            PrintQuyetToanThuMuaViewModel = printQuyetToanThuMuaViewModel;
            _danhMucService = danhMucService;

            QuyetToanThuMuaImportViewModel.ParentPage = this;
            QuyetToanThuMuaDialogViewModel.ParentPage = this;
            QuyetToanThuMuaDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
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
            OpenDetailDialog((BhQttmBHYTModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedBhQTTMModel == null) return;
            if (SelectedBhQTTMModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhQTTMModel.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhQTTMModel.SSoChungTu, SelectedBhQTTMModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool BhxhChungTuModelsFilter(object obj)
        {
            if (!(obj is BhQttmBHYTModel temp)) return true;
            var keyword = SearchText?.Trim().ToLower() ?? string.Empty;
            var condition1 = false;
            var condition2 = true;
            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(temp.SSoChungTu))
                    condition1 = condition1 || temp.SSoChungTu.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SMoTa))
                    condition1 = condition1 || temp.SMoTa.ToLower().Contains(keyword);
                if (!string.IsNullOrEmpty(temp.SNguoiTao))
                    condition1 = condition1 || temp.SNguoiTao.ToLower().Contains(keyword);
            }
            else
            {
                condition1 = true;
            }

            if (SelectedDonViModel != null)
            {
                condition2 = condition2 && temp.IIDMaDonVi == SelectedDonViModel.IIDMaDonVi;
            }

            if (SelectedQTTMQuarterModel != null)
            {
                condition2 = condition2 && temp.IQuyNam == SelectedQTTMQuarterModel.IQuyNam && temp.IQuyNamLoai == SelectedQTTMQuarterModel.IQuyNamLoai
                    && temp.SQuyNamMoTa == SelectedQTTMQuarterModel.SQuyNamMoTa;
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
            temp.IsFilter = result;
            return result;
        }
        private void OnDeleteHandler(NSDialogResult result)
        {
            BhQttmBHYTChiTietCriteria searchCondition = new BhQttmBHYTChiTietCriteria();
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedBhQTTMModel != null)
            {
                var qttmChungTu = _voucherService.FindById(SelectedBhQTTMModel.Id);
                searchCondition.VoucherID = qttmChungTu.Id;
                if (qttmChungTu != null)
                {
                    var lstChungTuChiTiet = _voucherDetailService.FindVoucherDetailById(searchCondition).ToList();
                    //Xóa chứng từ
                    _voucherService.Delete(qttmChungTu);

                    if (!string.IsNullOrEmpty(qttmChungTu.STongHop))
                    {
                        var lstSoCtChild = qttmChungTu.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _voucherService.FindAggregateVoucher(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _voucherService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _voucherDetailService.RemoveRange(lstChungTuChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ kế hoạch thu mua BHYT", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    LoadVoucherData();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            QuyetToanThuMuaImportViewModel.Init();
            QuyetToanThuMuaImportViewModel.SavedAction = obj =>
            {
                _quyetToanThuMuaImport.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQttmBHYTModel)obj);
            };
            _quyetToanThuMuaImport = new QuyetToanThuMuaImport
            {
                DataContext = QuyetToanThuMuaImportViewModel
            };
            _quyetToanThuMuaImport.ShowDialog();
        }

        private void ConfirmAggregate()
        {
            List<BhQttmBHYTModel> selectedVouchers = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
            bool checkAllowAggregate = selectedVouchers.All(x => x.BIsKhoa);
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
            //check quyền được tổng hợp
            List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBoxHelper.Warning(Resources.MsgRoleAggregate);
                return;
            }

            List<BhQttmBHYTModel> selectedVouchers = Items.Where(x => x.Selected && !x.IsSummaryVocher && x.IsFilter).ToList();
            if (selectedVouchers.GroupBy(x => x.IQuyNam).ToList().Count() > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateQuarterYear);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (selectedVouchers.Any(x => !x.BIsKhoa))
            {
                MessageBoxHelper.Info(Resources.AlertAggregateUnLocked);
                return;
            }

            else CreateAggregateVoucher();
        }
        private void OnSelectedChange(object obj)
        {
            SelectedBhQTTMModel = (BhQttmBHYTModel)obj;
            if (SelectedBhQTTMModel is { BIsKhoa: true } || SelectedBhQTTMModel == null)
            {
                IsEdit = false;
                IsDelete = false;
            }
            else
            {
                IsEdit = true;
                IsDelete = true;
            }
        }

        private void CreateAggregateVoucher()
        {
            //kiểm tra trạng thái các bản ghi
            if (!_sessionService.Current.IsQuanLyDonViCha)
            {
                MessageBoxHelper.Warning(Resources.MsgRoleSummary);
                return;
            }
            List<BhQttmBHYTModel> selectedChungTus = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher).ToList();

            QuyetToanThuMuaDialogViewModel.Name = "TỔNG HỢP";
            QuyetToanThuMuaDialogViewModel.Description = "Tổng hợp quyết toán thu mua BHYT thân nhân";
            QuyetToanThuMuaDialogViewModel.Model = new BhQttmBHYTModel();
            QuyetToanThuMuaDialogViewModel.Id = Guid.Empty;
            QuyetToanThuMuaDialogViewModel.VoucherNoIndex = _voucherService.GetVoucherIndex(_sessionInfo.YearOfWork);
            QuyetToanThuMuaDialogViewModel.IsAggregate = true;
            QuyetToanThuMuaDialogViewModel.AggregateAgency = _aggregateAgency;
            QuyetToanThuMuaDialogViewModel.AggregateLNS = string.Join(",", selectedChungTus.Select(x => x.sDSMLNS).Distinct());
            QuyetToanThuMuaDialogViewModel.AggregateBhVouchers = selectedChungTus;
            QuyetToanThuMuaDialogViewModel.Init();
            QuyetToanThuMuaDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.Aggregate;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQttmBHYTModel)obj);
            };
            var addView = new QuyetToanThuMuaDialog() { DataContext = QuyetToanThuMuaDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhQttmBHYTModel)eventArgs.Parameter);
        }

        private void LoadData()
        {
            _sessionInfo = _sessionService.Current;
            LoadVoucherData();
            LoadDonVi();
            LoadQuarterYear();
            OnPropertyChanged(nameof(IsCensorship));
        }

        private void LoadDonVi()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            _aggregateAgency = _donViService.FindByIdDonVi(_sessionInfo.IdDonVi, yearOfWork);
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            if (Items != null && Items.Count > 0)
            {
                var idDonVis = Items.Select(x => x.IIDMaDonVi).ToList();
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

        private void LoadQuarterYear()
        {
            /*if (Items != null && Items.Count > 0)
            {
                var iDQuarterYear = Items.Select(x => x.IQuyNam).ToList();
                var listQuarterYear = _voucherService.GetQuarterYearByYear(_sessionInfo.YearOfWork).Where(x => iDQuarterYear.Any(y => y == x.IQuyNam)).ToList();
                QuarterYearItems = new ObservableCollection<BhQttQuarterQuery>();
                QuarterYearItems = _mapper.Map<ObservableCollection<BhQttQuarterQuery>>(listQuarterYear);
            }*/
            var yearOfWork = _sessionInfo.YearOfWork;
            QuarterYearItems = new ObservableCollection<BhQttQuarterQuery>();
            QuarterYearItems.Add(new BhQttQuarterQuery(3, 1, "Quý I"));
            QuarterYearItems.Add(new BhQttQuarterQuery(6, 1, "Quý II"));
            QuarterYearItems.Add(new BhQttQuarterQuery(9, 1, "Quý III"));
            QuarterYearItems.Add(new BhQttQuarterQuery(12, 1, "Quý IV"));
            QuarterYearItems.Add(new BhQttQuarterQuery(yearOfWork, 2, "Năm " + yearOfWork));
            _bhQuyModelsView = CollectionViewSource.GetDefaultView(QuarterYearItems);
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

        private void LoadVoucherData()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            int yearOfBudget = _sessionInfo.YearOfBudget;
            int budgetSource = _sessionInfo.Budget;
            var currentIdDonVi = _sessionInfo.IdDonVi;

            var listChungTu = _voucherService.FindByCondition(_sessionInfo.YearOfWork);
            _lstChungTuOrigin = _mapper.Map<List<BhQttmBHYTModel>>(listChungTu);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<BhQttmBHYTModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop)).ToList();
                    var listTongHop = new List<BhQttmBHYTModel>();
                    foreach (var ctTongHop in listCTTongHop)
                    {
                        var parent = _mapper.Map<BhQttmBHYTModel>(ctTongHop);
                        parent.IsExpand = true;
                        listTongHop.Add(parent);

                        if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                        {
                            var listChild = _mapper.Map<List<BhQttmBHYTModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<BhQttmBHYTModel>>(listTongHop);
                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<BhQttmBHYTModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()));
            }

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhQttmBHYTModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                    }
                    if (args.PropertyName == nameof(BhQttmBHYTModel.IsCollapse))
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
                Items.Where(n => n.SoChungTuParent == SelectedBhQTTMModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _voucherService.LockOrUnlock(ct.Id, isLock);
                var qttmBHYT = Items.First(x => x.Id == ct.Id);
                qttmBHYT.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ quyết toán thu mua BHYT", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadVoucherData();
        }

        protected override void OnAdd()
        {
            QuyetToanThuMuaDialogViewModel.Name = "Thêm mới";
            QuyetToanThuMuaDialogViewModel.Description = "Thêm mới báo cáo QT thu mua BHYT";
            QuyetToanThuMuaDialogViewModel.Id = Guid.Empty;
            QuyetToanThuMuaDialogViewModel.VoucherNoIndex = _voucherService.GetVoucherIndex(_sessionInfo.YearOfWork);
            QuyetToanThuMuaDialogViewModel.IsAggregate = false;
            QuyetToanThuMuaDialogViewModel.Init();
            QuyetToanThuMuaDialogViewModel.SavedAction = obj =>
            {
                var chungTu = (BhQttmBHYTModel)obj;
                this.LoadData();
                OpenDetailDialog(chungTu);
            };
            var exportView = new QuyetToanThuMuaDialog() { DataContext = QuyetToanThuMuaDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedBhQTTMModel != null)
            {
                if (SelectedBhQTTMModel.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && SelectedBhQTTMModel.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedBhQTTMModel.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedBhQTTMModel.SNguoiTao));
                        return;
                    }
                    QuyetToanThuMuaDialogViewModel.Name = "Cập nhật";
                    QuyetToanThuMuaDialogViewModel.Description = "Cập nhật thông tin báo cáo QT thu mua BHYT thân nhân";
                    QuyetToanThuMuaDialogViewModel.Id = SelectedBhQTTMModel.Id;
                    QuyetToanThuMuaDialogViewModel.IsAggregate = false;
                    QuyetToanThuMuaDialogViewModel.Init();
                    QuyetToanThuMuaDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    QuyetToanThuMuaDialogViewModel.ShowDialogHost();
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
            LoadVoucherData();
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
        /// <param name="BhQttmBHYTModel"></param>
        private void OpenDetailDialog(BhQttmBHYTModel bhQttmBHYTDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTuTH = Items.FirstOrDefault(item => item.IIDMaDonVi.Equals(idDonViCurrent));
            QuyetToanThuMuaDetailViewModel.Model = ObjectCopier.Clone(bhQttmBHYTDetail);
            QuyetToanThuMuaDetailViewModel.CtTongHop = chungTuTH;
            QuyetToanThuMuaDetailViewModel.ILoaiQuyNam = bhQttmBHYTDetail.IQuyNamLoai;
            QuyetToanThuMuaDetailViewModel.IsVoucherSummary = bhQttmBHYTDetail.IIDMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(bhQttmBHYTDetail.STongHop);

            QuyetToanThuMuaDetailViewModel.Init();
            var view = new QuyetToanThuMuaDetail() { DataContext = QuyetToanThuMuaDetailViewModel };
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
            QuyetToanThuMuaDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {

        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;
                    List<BhQttmBHYTModel> voucherModel = Items.Where(x => x.Selected).ToList();
                    var yearOfWork = _sessionInfo.YearOfWork;

                    foreach (var item in voucherModel)
                    {
                        var voucherItem = _voucherService.FindById(item.Id);
                        var voucherUnit = _donViService.FindByMaDonViAndNamLamViec(voucherItem.IIDMaDonVi, yearOfWork);
                        BhQttmBHYTChiTietCriteria searchCondition = new BhQttmBHYTChiTietCriteria();
                        searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IIDMaDonVi = item.IIDMaDonVi;
                        searchCondition.VoucherID = item.Id;
                        searchCondition.SLns = item.sDSMLNS;
                        searchCondition.IQuyNamLoai = item.IQuyNamLoai;
                        searchCondition.IsDonViCha = CheckParentUnit(item.IIDMaDonVi);
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        var voucherDetail = _voucherDetailService.FindVoucherDetailByCondition(searchCondition).ToList();
                        var lstExportData = _mapper.Map<ObservableCollection<BhQttmBHYTChiTietModel>>(voucherDetail).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        CalculateData(lstExportData);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("DonVi", _sessionInfo.TenDonVi);
                        Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        Data.Add("SQuyNamMoTa", voucherItem.SQuyNamMoTa);
                        Data.Add("DNgayChungTu", voucherItem.DNgayChungTu.ToString("dd/MM/yyyy"));
                        Data.Add("TieuDe1", "BÁO CÁO CHI TIẾT QT THU MUA BHYT THÂN NHÂN NĂM " + _sessionInfo.YearOfWork);
                        Data.Add("h2", "Lữ đoàn X");
                        Data.Add("h1", "Lữ đoàn X");
                        Data.Add("ListMLNS", lstExportData);
                        Data.Add("ListData", lstExportData);
                        Data.Add("FormatNumber", formatNumber);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTTM, ExportFileName.RPT_BH_QTTM_BHYT_CHUNGTU_CHITIET);
                        fileNamePrefix = item.SSoChungTu + "_" + voucherUnit.TenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQttmBHYTChiTietModel, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, Data);
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
        private DonVi GetDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _donViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }
        private void OnPrint(object param)
        {
            int dialogType = (int)param;

            switch (dialogType)
            {
                case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN_NLD:
                    PrintQuyetToanThuMuaViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuMuaViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuMuaViewModel.Init();
                    var view1 = new PrintQuyetToanThuMua
                    {
                        DataContext = PrintQuyetToanThuMuaViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                    /*case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_THAN_NHAN:
                    case (int)QttmType.QUYET_TOAN_THU_MUA_BHYT_BHYT_HSSV_HVQS_SQDB:
                        PrintQuyetToanThuMuaViewModel.SettlementTypeValue = dialogType;
                        PrintQuyetToanThuMuaViewModel.IsEnableInTheo = true;
                        PrintQuyetToanThuMuaViewModel.Init();
                        var view2 = new PrintQuyetToanThuMua
                        {
                            DataContext = PrintQuyetToanThuMuaViewModel
                        };
                        DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                        break;
                    */
            }

        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(IsAllItemsSelected));
        }
        private void CalculateData(List<BhQttmBHYTChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FSoPhaiThu = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhQttmBHYTChiTietModel item, List<BhQttmBHYTChiTietModel> lstChungTuChiTiet)
        {
            var model = lstChungTuChiTiet.FirstOrDefault(x => x.IIDMLNS == idParent);
            if (model == null) return;
            model.FDuToan += item.FDuToan;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FSoPhaiThu += item.FSoPhaiThu;
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private bool CheckParentUnit(string maDonVi)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            return _donViService.IsDonViCha(maDonVi, yearOfWork);
        }
    }
}
