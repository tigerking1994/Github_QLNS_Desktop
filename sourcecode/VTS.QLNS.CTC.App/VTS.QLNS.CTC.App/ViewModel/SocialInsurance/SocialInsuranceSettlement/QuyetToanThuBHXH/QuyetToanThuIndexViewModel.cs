using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.Smo;
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
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.ImportExcel;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.ImportExcel;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.PrintReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH
{
    public class QuyetToanThuIndexViewModel : GridViewModelBase<BhQttBHXHModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IQttBHXHService _qttBHXHService;
        private readonly IQttBHXHChiTietService _qttBHXHChiTietService;
        private readonly IQttBHXHChiTietGiaiThichService _giaiThichService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private readonly ISysAuditLogService _log;
        private SessionInfo _sessionInfo;
        private readonly IExportService _exportService;
        private ICollectionView _bhChungTuModelsView;
        private ICollectionView _bhDonViModelsView;
        private ICollectionView _bhQuyModelsView;
        private readonly INsNguoiDungDonViService _iNguoiDungDonViService;
        private DonVi _aggregateAgency;
        private bool _isEdit;
        private QuyetToanThuImport _quyetToanThuImport;
        public bool IsInBudget => ParentPage.FuncCode == NSFunctionCode.BUDGET_SETTLEMENT;
        public string S1 => IsInBudget ? "Danh sách chứng từ" : "Danh sách báo cáo";
        public string S2 => IsInBudget ? "Chứng từ tổng hợp" : "Báo cáo tổng hợp";
        public string S3 => IsInBudget ? "Tháng / Quý" : "Tháng / Quý / Năm";
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

        private BhQttQuarterQuery _selectedQuarterModel;
        public BhQttQuarterQuery SelectedQuarterModel
        {
            get => _selectedQuarterModel;
            set
            {
                SetProperty(ref _selectedQuarterModel, value);
                SearchData();
            }
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private BhQttBHXHModel _selectedBhQTTModel;
        public BhQttBHXHModel SelectedBhQTTModel
        {
            get => _selectedBhQTTModel;
            set
            {
                SetProperty(ref _selectedBhQTTModel, value);
                if (_selectedBhQTTModel != null)
                {
                    IsLock = _selectedBhQTTModel.BIsKhoa;
                }
                else
                {
                    IsEdit = false;
                    IsDelete = false;
                }
                OnPropertyChanged(nameof(IsButtonEnable));
                if (_selectedBhQTTModel == null)
                {
                    IsEdit = false;
                    IsDelete = false;
                }
                OnPropertyChanged(nameof(IsExportAggregateData));
                OnPropertyChanged(nameof(IsExportDataFilter));
            }
        }

        private List<BhQttBHXHModel> _lstChungTuOrigin;
        public List<BhQttBHXHModel> LstChungTuOrigin
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
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
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
        public bool IsExportDataFilter => _selectedBhQTTModel != null;

        private void SelectAll(bool select, IEnumerable<BhQttBHXHModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.Selected = select;
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
        public string ComboboxQuarterDisplayMemberPath => nameof(SelectedQuarterModel.SQuyNamMoTa);
        public override Type ContentType => typeof(QuyetToanThuIndex);
        public override string GroupName => MenuItemContants.GROUP_QTT_THU;
        public override string Description => "Danh sách báo cáo QT thu BHXH, BHYT, BHTN";
        public override string Name => "QT thu BHXH, BHYT, BHTN";
        public override PackIconKind IconKind => PackIconKind.BankTransferIn;
        public QuyetToanThuImportViewModel QuyetToanThuImportViewModel { get; }
        public QuyetToanThuDialogViewModel QuyetToanThuDialogViewModel { get; }
        public QuyetToanThuDetailViewModel QuyetToanThuDetailViewModel { get; }
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
        public RelayCommand DownloadTemplateCommand { get; }
        public PrintQuyetToanThuViewModel PrintQuyetToanThuViewModel { get; }
        public PrintThongTriQuyetToanViewModel PrintThongTriQuyetToanViewModel { get; }
        public PrintQuyetToanThuNopTheoThoiGianViewModel PrintQuyetToanThuNopTheoThoiGianViewModel { get; }

        public QuyetToanThuIndexViewModel(
            IQttBHXHService qttBHXHService,
            IQttBHXHChiTietService qttBHXHChiTietService,
            IQttBHXHChiTietGiaiThichService qttBHXHChiTietGiaiThichService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            INsDonViService nsDonViService,
            ILog logger,
            IMapper mapper,
            IExportService exportService,
            QuyetToanThuDialogViewModel quyetToanThuDialogViewModel,
            QuyetToanThuDetailViewModel quyetToanThuDetailViewModel,
            ISessionService sessionService,
            INsDonViService donViService,
            INsNguoiDungDonViService iNguoiDungDonViService,
            ISysAuditLogService log,
            QuyetToanThuImportViewModel quyetToanThuImportViewModel,
            PrintQuyetToanThuViewModel printQuyetToanThuViewModel,
            PrintThongTriQuyetToanViewModel printThongTriQuyetToanViewModel,
            PrintQuyetToanThuNopTheoThoiGianViewModel printQuyetToanThuNopTheoThoiGianViewModel,
            IDanhMucService danhMucService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _qttBHXHService = qttBHXHService;
            _qttBHXHChiTietService = qttBHXHChiTietService;
            _giaiThichService = qttBHXHChiTietGiaiThichService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _exportService = exportService;
            _donViService = donViService;
            _log = log;
            _iNguoiDungDonViService = iNguoiDungDonViService;
            _danhMucService = danhMucService;

            QuyetToanThuDialogViewModel = quyetToanThuDialogViewModel;
            QuyetToanThuDetailViewModel = quyetToanThuDetailViewModel;
            QuyetToanThuImportViewModel = quyetToanThuImportViewModel;
            PrintQuyetToanThuViewModel = printQuyetToanThuViewModel;
            PrintThongTriQuyetToanViewModel = printThongTriQuyetToanViewModel;
            PrintQuyetToanThuNopTheoThoiGianViewModel = printQuyetToanThuNopTheoThoiGianViewModel;
            QuyetToanThuImportViewModel.ParentPage = this;
            QuyetToanThuDialogViewModel.ParentPage = this;
            QuyetToanThuDetailViewModel.ParentPage = this;

            ExportCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            SelectionChangedCommand = new RelayCommand(OnSelectedChange);
            LockCommand = new RelayCommand(OnLock);
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportData());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
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
            OpenDetailDialog((BhQttBHXHModel)obj, false);
        }

        protected override void OnDelete()
        {
            if (SelectedBhQTTModel == null) return;
            if (SelectedBhQTTModel.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, SelectedBhQTTModel.SNguoiTao));
                return;
            }
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTuKhtBHXH, SelectedBhQTTModel.SSoChungTu, SelectedBhQTTModel.DNgayChungTu);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo,
                OnDeleteHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }
        private bool BhxhChungTuModelsFilter(object obj)
        {
            if (!(obj is BhQttBHXHModel temp)) return true;
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

            if (SelectedQuarterModel != null)
            {
                condition2 = condition2 && temp.IQuyNam == SelectedQuarterModel.IQuyNam && temp.IQuyNamLoai == SelectedQuarterModel.IQuyNamLoai
                    && temp.SQuyNamMoTa == SelectedQuarterModel.SQuyNamMoTa;
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
            BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
            if (result != NSDialogResult.Yes) return;
            DateTime dtNow = DateTime.Now;
            if (SelectedBhQTTModel != null)
            {
                var qttChungTu = _qttBHXHService.FindById(SelectedBhQTTModel.Id);
                searchCondition.IdQttBhxh = qttChungTu.Id;
                if (qttChungTu != null)
                {
                    var lstChungTuChiTiet = _qttBHXHChiTietService.FindVoucherDetailById(searchCondition).ToList();
                    var lstGiaiThichChiTiet = _giaiThichService.FindByVouCherId(qttChungTu.Id).ToList();
                    //Xóa chứng từ
                    _qttBHXHService.Delete(qttChungTu);

                    if (!string.IsNullOrEmpty(qttChungTu.STongHop))
                    {
                        var lstSoCtChild = qttChungTu.STongHop.Split(",");
                        foreach (var soct in lstSoCtChild)
                        {
                            var ctChild = _qttBHXHService.FindAggregateVoucher(soct, _sessionInfo.YearOfWork).FirstOrDefault();
                            if (ctChild != null)
                            {
                                ctChild.BDaTongHop = false;
                                _qttBHXHService.Update(ctChild);
                            }
                        }
                    }
                    //Xóa chi tiết chứng từ
                    _qttBHXHChiTietService.RemoveRange(lstChungTuChiTiet);
                    _giaiThichService.RemoveRange(lstGiaiThichChiTiet);
                    _log.WriteLog(Resources.ApplicationName, "Xóa chứng từ kế hoạch thu BHXH", (int)TypeExecute.Delete, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
                    LoadVoucherData();
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
            }
        }

        private void OnImportData()
        {
            QuyetToanThuImportViewModel.Init();
            QuyetToanThuImportViewModel.SavedAction = obj =>
            {
                _quyetToanThuImport.Close();
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQttBHXHModel)obj);
            };
            _quyetToanThuImport = new QuyetToanThuImport
            {
                DataContext = QuyetToanThuImportViewModel
            };
            _quyetToanThuImport.ShowDialog();
        }

        private void ConfirmAggregate()
        {
            List<BhQttBHXHModel> selectedVouchers = Items.Where(x => x.Selected && !x.IsSummaryVocher).ToList();
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

            List<BhQttBHXHModel> selectedVouchers = Items.Where(x => x.Selected && !x.IsSummaryVocher && x.IsFilter).ToList();
            if (selectedVouchers.GroupBy(x => new { x.IQuyNam, x.IQuyNamLoai }).ToList().Count() > 1)
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

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            //Dictionary<Guid, List<string>> dicTongHop = new Dictionary<Guid, List<string>>();
            //foreach (var item in Items.Where(x => !x.IsChildSummary))
            //{
            //    if (!dicTongHop.ContainsKey(item.Id))
            //        dicTongHop.Add(item.Id, item.STongHop?.Split(StringUtils.COMMA).ToList() ?? new List<string> { item.SSoChungTu });
            //}
            //List<string> listChungTu = selectedVouchers.Select(x => x.SSoChungTu).ToList();
            //if (dicTongHop.Values.Any(x => x.Intersect(listChungTu).Any()))
            //{
            //    MessageBoxResult result = MessageBoxHelper.Confirm(Resources.AlertExistAggregateVoucher);
            //    switch (result)
            //    {
            //        case MessageBoxResult.Yes:
            //            foreach (var item in dicTongHop)
            //            {
            //                if (item.Value.Intersect(listChungTu).Any())
            //                    _qttBHXHService.Delete(item.Key);
            //            }
            //            CreateAggregateVoucher();
            //            break;
            //        case MessageBoxResult.No:
            //            return;
            //    }
            //}
            CreateAggregateVoucher();
        }
        private void OnSelectedChange(object obj)
        {
            SelectedBhQTTModel = (BhQttBHXHModel)obj;
            if (SelectedBhQTTModel is { BIsKhoa: true } || SelectedBhQTTModel == null)
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
            List<BhQttBHXHModel> selectedChungTus = Items.Where(x => x.Selected && x.BIsKhoa && !x.IsSummaryVocher).ToList();

            QuyetToanThuDialogViewModel.Name = "TỔNG HỢP";
            QuyetToanThuDialogViewModel.Description = "Tổng hợp quyết toán thu BHXH, BHYT, BHTN";
            QuyetToanThuDialogViewModel.Model = new BhQttBHXHModel();
            QuyetToanThuDialogViewModel.Id = Guid.Empty;
            QuyetToanThuDialogViewModel.VoucherNoIndex = _qttBHXHService.GetVoucherIndex(_sessionInfo.YearOfWork);
            QuyetToanThuDialogViewModel.IsAggregate = true;
            QuyetToanThuDialogViewModel.AggregateAgency = _aggregateAgency;
            QuyetToanThuDialogViewModel.AggregateLNS = string.Join(",", selectedChungTus.Select(x => x.sDSMLNS).Distinct());
            QuyetToanThuDialogViewModel.AggregateQTTVouchers = selectedChungTus;
            QuyetToanThuDialogViewModel.Init();
            QuyetToanThuDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.Aggregate;
                this.LoadData();
                OnPropertyChanged(nameof(IsCensorship));
                IsAllItemsSelected = false;
                OpenDetailDialog((BhQttBHXHModel)obj);
            };
            var addView = new QuyetToanThuDialog() { DataContext = QuyetToanThuDialogViewModel };
            DialogHost.Show(addView, SystemConstants.ROOT_DIALOG);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            LoadData();
            if (eventArgs.Parameter != null)
                OpenDetailDialog((BhQttBHXHModel)eventArgs.Parameter);
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
                _bhDonViModelsView.SortDescriptions.Add(new SortDescription(nameof(DonViModel.IIDMaDonVi),
                    ListSortDirection.Ascending));
            }
        }

        private void LoadQuarterYear()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            /*if (Items != null && Items.Count > 0)
            {
                var iDQuarterYear = Items.Select(x => x.IQuyNam).ToList();
                var listQuarterYear = _qttBHXHService.GetQuarterYearByYear(_sessionInfo.YearOfWork).Where(x => iDQuarterYear.Any(y => y == x.IQuyNam)).ToList();
                QuarterYearItems = new ObservableCollection<BhQttQuarterQuery>();
                QuarterYearItems = _mapper.Map<ObservableCollection<BhQttQuarterQuery>>(listQuarterYear);
                _bhQuyModelsView = CollectionViewSource.GetDefaultView(QuarterYearItems);
                _bhQuyModelsView.SortDescriptions.Add(new SortDescription(nameof(BhQttBHXHModel.SQuyNamMoTa),
                    ListSortDirection.Ascending));
            }*/
            QuarterYearItems = new ObservableCollection<BhQttQuarterQuery>();
            for (int i = 1; i <= 12; i++)
            {
                QuarterYearItems.Add(new BhQttQuarterQuery(i, 0, "Tháng " + i));
            }

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

            var listChungTu = _qttBHXHService.FindByCondition(_sessionInfo.YearOfWork);
            _lstChungTuOrigin = _mapper.Map<List<BhQttBHXHModel>>(listChungTu);

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                if (TabIndex == ImportTabIndex.Data)
                {
                    Items = _mapper.Map<ObservableCollection<BhQttBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()).OrderBy(x => x.IQuyNam));
                }
                else
                {
                    var listCTTongHop = listChungTu.Where(x => x.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTuTongHop)).ToList();
                    var listTongHop = new List<BhQttBHXHModel>();
                    foreach (var ctTongHop in listCTTongHop)
                    {
                        var parent = _mapper.Map<BhQttBHXHModel>(ctTongHop);
                        parent.IsExpand = true;
                        listTongHop.Add(parent);

                        if (!string.IsNullOrEmpty(ctTongHop.STongHop))
                        {
                            var listChild = _mapper.Map<List<BhQttBHXHModel>>(listChungTu.Where(x => ctTongHop.STongHop != null && ctTongHop.STongHop.Contains(x.SSoChungTu)));
                            listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = ctTongHop.SSoChungTu; });
                            listTongHop.AddRange(listChild);
                        }
                    }

                    Items = _mapper.Map<ObservableCollection<BhQttBHXHModel>>(listTongHop.OrderBy(x => x.IQuyNam));
                }
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<BhQttBHXHModel>>(listChungTu.Where(x => x.ILoaiTongHop.Equals(BhxhLoaiChungTu.BhxhChungTu) && !x.BDaTongHop.GetValueOrDefault()).OrderBy(x => x.IQuyNam));
            }

            foreach (var model in Items)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhQttBHXHModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsCensorship));
                        OnPropertyChanged(nameof(IsExportAggregateData));
                        OnPropertyChanged(nameof(IsExportDataFilter));
                        OnPropertyChanged(nameof(IsButtonEnable));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsAllItemSummariesSelected));
                    }
                    if (args.PropertyName == nameof(BhQttBHXHModel.IsCollapse))
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
                Items.Where(n => n.SoChungTuParent == SelectedBhQTTModel.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
        }

        private void LockOrUnLockMultiVoucher()
        {
            DateTime dtNow = DateTime.Now;
            var lstSelected = Items.Where(x => x.Selected).ToList();
            var isLock = !lstSelected.FirstOrDefault().BIsKhoa;
            foreach (var ct in lstSelected)
            {
                _qttBHXHService.LockOrUnlock(ct.Id, isLock);
                var qttBHXH = Items.First(x => x.Id == ct.Id);
                qttBHXH.BIsKhoa = !ct.BIsKhoa;
            }
            _log.WriteLog(Resources.ApplicationName, "Khóa chứng từ quyết toán thu BHXH", (int)TypeExecute.Update, dtNow, TransactionStatus.Success, _sessionService.Current.Principal);
            LoadVoucherData();
        }

        protected override void OnAdd()
        {
            QuyetToanThuDialogViewModel.Name = "Thêm mới";
            QuyetToanThuDialogViewModel.Description = "Tạo mới báo cáo QT thu BHXH, BHYT, BHTN";
            QuyetToanThuDialogViewModel.IsInBudget = IsInBudget;
            QuyetToanThuDialogViewModel.Id = Guid.Empty;
            QuyetToanThuDialogViewModel.VoucherNoIndex = _qttBHXHService.GetVoucherIndex(_sessionInfo.YearOfWork);
            QuyetToanThuDialogViewModel.IsAggregate = false;
            QuyetToanThuDialogViewModel.Init();
            QuyetToanThuDialogViewModel.SavedAction = obj =>
            {
                var chungTu = (BhQttBHXHModel)obj;
                this.LoadData();
                OpenDetailDialog(chungTu);
            };
            var exportView = new QuyetToanThuDialog() { DataContext = QuyetToanThuDialogViewModel };
            DialogHost.Show(exportView, SystemConstants.ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            if (SelectedBhQTTModel != null)
            {
                if (SelectedBhQTTModel.IIDMaDonVi.Equals(_sessionInfo.IdDonVi) && SelectedBhQTTModel.ILoaiTongHop == BhxhLoaiChungTu.BhxhChungTuTongHop)
                {
                    OnAggregateEdit();
                }
                else
                {
                    if (SelectedBhQTTModel.SNguoiTao != _sessionInfo.Principal)
                    {
                        MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedBhQTTModel.SNguoiTao));
                        return;
                    }
                    QuyetToanThuDialogViewModel.Name = "Cập nhật";
                    QuyetToanThuDialogViewModel.Description = "Cập nhật thông tin báo cáo QT thu BHXH, BHYT, BHTN";
                    QuyetToanThuDialogViewModel.Id = SelectedBhQTTModel.Id;
                    QuyetToanThuDialogViewModel.IsAggregate = false;
                    QuyetToanThuDialogViewModel.Init();
                    QuyetToanThuDialogViewModel.SavedAction = obj =>
                    {
                        this.OnRefresh();
                    };
                    QuyetToanThuDialogViewModel.ShowDialogHost();
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
        /// <param name="BhQttBHXHModel"></param>
        private void OpenDetailDialog(BhQttBHXHModel bhQttBHXHDetail, params bool[] isNew)
        {
            var idDonViCurrent = _sessionInfo.IdDonVi;
            var chungTuTH = Items.FirstOrDefault(item => item.IIDMaDonVi.Equals(idDonViCurrent));
            QuyetToanThuDetailViewModel.Model = ObjectCopier.Clone(bhQttBHXHDetail);
            QuyetToanThuDetailViewModel.CtTongHop = chungTuTH;
            QuyetToanThuDetailViewModel.IsVoucherSummary = bhQttBHXHDetail.IIDMaDonVi.Equals(idDonViCurrent) && !string.IsNullOrEmpty(bhQttBHXHDetail.STongHop);
            QuyetToanThuDetailViewModel.IsInBudget = IsInBudget;
            QuyetToanThuDetailViewModel.Init();
            var view = new QuyetToanThuDetail() { DataContext = QuyetToanThuDetailViewModel };
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
            QuyetToanThuDetailViewModel.UpdateParentWindowEventHandler += SelfRefresh;
        }

        private void OnAggregateEdit()
        {

        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// 

        private void OnExportData()
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
                    List<BhQttBHXHModel> voucherModel = Items.Where(x => x.Selected).ToList();
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork)
                    .Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    var yearOfWork = _sessionInfo.YearOfWork;

                    foreach (var item in voucherModel)
                    {
                        var voucherItem = _qttBHXHService.FindById(item.Id);
                        var voucherUnit = _donViService.FindByMaDonViAndNamLamViec(voucherItem.IIDMaDonVi, yearOfWork);
                        BhQttBHXHChiTietCriteria searchCondition = new BhQttBHXHChiTietCriteria();
                        searchCondition.INamLamViec = _sessionInfo.YearOfWork;
                        searchCondition.IIDMaDonVi = item.IIDMaDonVi;
                        searchCondition.IdQttBhxh = item.Id;
                        searchCondition.SLns = item.sDSMLNS;
                        searchCondition.IQuyNamLoai = item.IQuyNamLoai;
                        searchCondition.IsDonViCha = CheckParentUnit(item.IIDMaDonVi);
                        FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                        var voucherDetail = _qttBHXHChiTietService.FindVoucherDetailByCondition(searchCondition).ToList();
                        var lstExportData = _mapper.Map<ObservableCollection<BhQttBHXHChiTietModel>>(voucherDetail).ToList();
                        Dictionary<string, object> Data = new Dictionary<string, object>();
                        CalculateData(lstExportData);
                        Data.Add("SNguoiTao", item.SNguoiTao);
                        Data.Add("DonVi", _sessionInfo.TenDonVi);
                        Data.Add("DonViCha", itemDanhMuc?.SGiaTri.ToUpper() ?? "");
                        Data.Add("SQuyNamMoTa", voucherItem.SQuyNamMoTa);
                        Data.Add("DNgayChungTu", voucherItem.DNgayChungTu.ToString("dd/MM/yyyy"));
                        Data.Add("TieuDe1", "BÁO CÁO CHI TIẾT QT THU BHXH, BHYT, BHTN NĂM " + _sessionInfo.YearOfWork);
                        Data.Add("h2", item.STenDonVi);
                        Data.Add("h1", item.STenDonVi);
                        Data.Add("ListMLNS", lstExportData);
                        Data.Add("ListData", lstExportData);
                        Data.Add("FormatNumber", formatNumber);

                        templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_QTT_BHXH_CHUNGTU_CHITIET);
                        fileNamePrefix = item.SSoChungTu + "_" + voucherUnit.TenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<BhQttBHXHChiTietModel, BhDmMucLucNganSach, BhKhtBHXHChiTiet>(templateFileName, Data);
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

        private void OnDownloadTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    var yearOfWork = _sessionInfo.YearOfWork;
                    var predicateMLNS = PredicateBuilder.True<BhDmMucLucNganSach>();
                    predicateMLNS = predicateMLNS.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    List<BhDmMucLucNganSach> items = _bhDmMucLucNganSachService.FindByCondition(predicateMLNS).Where(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN)).OrderBy(x => x.SXauNoiMa).ToList();
                    items.RemoveAt(0);
                    items.Select(x =>
                    {
                        if (!string.IsNullOrEmpty(x.SXauNoiMa) && x.SXauNoiMa.Length == 7)
                        {
                            x.IIDMLNSCha = Guid.Empty;
                        }
                        return x;
                    }).ToList();
                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    Dictionary<string, object> Data = new Dictionary<string, object>
                    {
                        { "Items", items },
                        { "ListData", items },
                        { "FormatNumber", formatNumber },
                        { "Count",10000 },
                        { "H1",string.Empty },
                        { "H2",string.Empty },
                    };

                    templateFileName = Path.Combine(ExportPrefix.PATH_BH_QTT, ExportFileName.RPT_BH_QTT_BHXH_TEMPLATE_IMPORT);
                    fileNamePrefix = ExportFileName.RPT_BH_QTT_BHXH_TEMPLATE_IMPORT.Remove(ExportFileName.RPT_BH_QTT_BHXH_TEMPLATE_IMPORT.IndexOf("."));
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<BhDmMucLucNganSach>(templateFileName, Data);

                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
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
                case (int)QttType.THONG_TRI_QUYET_TOAN_THU:
                    PrintThongTriQuyetToanViewModel.Model = SelectedItem;
                    PrintThongTriQuyetToanViewModel.Init();
                    PrintThongTriQuyetToanViewModel.ShowDialogHost();
                    break;
                case (int)QttType.QUYET_TOAN_THU_CHI_TONG_HOP:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = false;
                    PrintQuyetToanThuViewModel.Init();
                    DialogHost.Show(new PrintQuyetToanThu() { DataContext = PrintQuyetToanThuViewModel }, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_QUY:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = false;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuViewModel.IsEnableReportType = false;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    var view1 = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    DialogHost.Show(view1, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_NAM:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = false;
                    PrintQuyetToanThuViewModel.IsEnableReportType = false;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    var view2 = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    DialogHost.Show(view2, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                /*case (int)QttType.QUYET_TOAN_THU_BHXH:
                case (int)QttType.QUYET_TOAN_THU_BHTN:
                case (int)QttType.QUYET_TOAN_THU_BHYT_QUAN_NHAN:
                case (int)QttType.QUYET_TOAN_THU_BHYT_NLD:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = true;
                    PrintQuyetToanThuViewModel.Init();
                    var view3 = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    DialogHost.Show(view3, SettlementScreen.ROOT_DIALOG, null, null);
                    break;*/
                case (int)QttType.QUYET_TOAN_TONG_HOP_NAM:
                    PrintQuyetToanThuViewModel.SettlementTypeValue = dialogType;
                    PrintQuyetToanThuViewModel.IsEnableLoaiThu = true;
                    PrintQuyetToanThuViewModel.IsEnableInTheo = true;
                    PrintQuyetToanThuViewModel.IsEnableReportType = true;
                    PrintQuyetToanThuViewModel.IsEnableReportTypeYear = false;
                    PrintQuyetToanThuViewModel.Init();
                    var view7 = new PrintQuyetToanThu
                    {
                        DataContext = PrintQuyetToanThuViewModel
                    };
                    DialogHost.Show(view7, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
                case (int)QttType.QUYET_TOAN_THU_NOP_BHXH_BHYT_BHTN_THEO_THOI_GIAN:
                    PrintQuyetToanThuNopTheoThoiGianViewModel.Init();
                    var view8 = new PrintQuyetToanThuNopTheoThoiGian
                    {
                        DataContext = PrintQuyetToanThuNopTheoThoiGianViewModel
                    };
                    DialogHost.Show(view8, SettlementScreen.ROOT_DIALOG, null, null);
                    break;
            }

        }
        protected override void OnItemsChanged()
        {
            base.OnItemsChanged();
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(IsAllItemsSelected));
            OnPropertyChanged(nameof(IsAllItemSummariesSelected));
        }
        private void CalculateData(List<BhQttBHXHChiTietModel> lstChungTuChiTiet)
        {
            lstChungTuChiTiet.Where(x => x.BHangCha.GetValueOrDefault(false))
                .Select(x =>
                {
                    x.FLuongChinh = 0;
                    x.FPhuCapChucVu = 0;
                    x.FPCTNNghe = 0;
                    x.FPCTNVuotKhung = 0;
                    x.FNghiOm = 0;
                    x.FHSBL = 0;
                    x.FDuToan = 0;
                    x.FDaQuyetToan = 0;
                    x.FThuBHXHNLD = 0;
                    x.FThuBHXHNSD = 0;
                    x.FThuBHYTNLD = 0;
                    x.FThuBHYTNSD = 0;
                    x.FThuBHTNNLD = 0;
                    x.FThuBHTNNSD = 0;
                    return x;
                }).ToList();
            var temp = lstChungTuChiTiet.Where(x => !x.BHangCha.GetValueOrDefault(false));
            foreach (var item in temp)
            {
                CalculateParent(item.IIDMLNSCha, item, lstChungTuChiTiet);
            }

        }
        private void CalculateParent(Guid? idParent, BhQttBHXHChiTietModel item, List<BhQttBHXHChiTietModel> lstChungTuChiTiet)
        {
            var model = lstChungTuChiTiet.FirstOrDefault(x => x.IIDMLNS == idParent);
            if (model == null) return;
            model.IQSBQNam += item.IQSBQNam;
            model.FLuongChinh += item.FLuongChinh;
            model.FPhuCapChucVu += item.FPhuCapChucVu;
            model.FPCTNNghe += item.FPCTNNghe;
            model.FPCTNVuotKhung += item.FPCTNVuotKhung;
            model.FNghiOm += item.FNghiOm;
            model.FHSBL += item.FHSBL;
            model.FDuToan += item.FDuToan;
            model.FDaQuyetToan += item.FDaQuyetToan;
            model.FThuBHXHNLD += item.FThuBHXHNLD;
            model.FThuBHXHNSD += item.FThuBHXHNSD;
            model.FThuBHYTNLD += item.FThuBHYTNLD;
            model.FThuBHYTNSD += item.FThuBHYTNSD;
            model.FThuBHTNNLD += item.FThuBHTNNLD;
            model.FThuBHTNNSD += item.FThuBHTNNSD;
            CalculateParent(model.IIDMLNSCha, item, lstChungTuChiTiet);
        }

        private bool CheckParentUnit(string maDonVi)
        {
            int yearOfWork = _sessionInfo.YearOfWork;
            return _donViService.IsDonViCha(maDonVi, yearOfWork);
        }
    }
}
