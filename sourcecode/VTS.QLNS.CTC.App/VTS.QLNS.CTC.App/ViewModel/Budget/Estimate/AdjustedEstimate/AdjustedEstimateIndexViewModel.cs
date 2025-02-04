using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
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
using VTS.QLNS.CTC.App.View.Budget.Estimate.AdjustedEstimate;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate.SendDataAdjustedEstimate;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.Settlement.RegularBudget.SendDataRegularBudget;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate
{
    public class AdjustedEstimateIndexViewModel : GridViewModelBase<DcChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private readonly INsDcChungTuService _chungTuService;
        private readonly INsDcChungTuChiTietService _chungTuChiTietService;
        private readonly INsNguoiDungDonViService _nguoiDungDonViService;
        private readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private readonly IGroupService _groupService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly AdjustedEstimateDialogViewModel AdjustedEstimateDialogViewModel;
        private readonly AdjustedEstimateDetailViewModel AdjustedEstimateDetailViewModel;
        private readonly AdjustedEstimateReportViewModel AdjustedEstimateReportViewModel;
        private readonly AdjustedEstimateImportJsonViewModel AdjustedEstimateImportJsonViewModel;
        private readonly INsDtChungTuService _dtChungTuService;
        private AdjustedEstimateImportJson _importJsonView;
        private readonly ReportDieuChinhDuToanTongHop ReportBaoCaoNganSachNhaNuocTongHop;
        private readonly AdjustedEstimateTheoLanReportViewModel AdjustedEstimateTheoLanReportViewModel;
        private readonly AdjustedEstimateImportViewModel AdjustedEstimateImportViewModel;
        private ICollectionView _dcChungTuView;
        private ICollectionView _dcChungTuTongHopView;
        private SessionInfo _sessionInfo;
        private List<DcChungTuChiTietModel> _adjustedEstimateDetailExports;
        private AdjustedEstimateImport _adjustedEstimateImportView;
        private List<DcChungTuModel> _allVoucher;
        private readonly IHTTPUploadFileService _hTTPUploadFileService;

        public ExportAdjustedEstimateViewModel ExportAdjustedEstimateViewModel { get; set; }
        private readonly SendDataAdjustedEstimateViewModel SendDataAdjustedEstimateViewModel;

        public override string Name => "Điều chỉnh dự toán";
        public override string Description => "Điều chỉnh số liệu dự toán";
        public override Type ContentType => typeof(AdjustedEstimateIndex);
        public override PackIconKind IconKind => PackIconKind.AxisZRotateClockwise;
        public bool IsEdit => TabIndex == ImportTabIndex.Data && SelectedItem != null && !SelectedItem.BKhoa;
        public bool IsProcessing { get; set; } = false;
        public bool IsButtonSendDataEnable => TabIndex != ImportTabIndex.Data;

        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsDelete => TabIndex == ImportTabIndex.Data ? SelectedItem != null && !SelectedItem.BKhoa : SelectedItemSummary != null && !SelectedItemSummary.BKhoa;

        private ObservableCollection<DcChungTuModel> _adjustedEstimateSummaries;
        public ObservableCollection<DcChungTuModel> AdjustedEstimateSummaries
        {
            get => _adjustedEstimateSummaries;
            set => SetProperty(ref _adjustedEstimateSummaries, value);
        }

        private DcChungTuModel _selectedItemSummary;
        public DcChungTuModel SelectedItemSummary
        {
            get => _selectedItemSummary;
            set
            {
                if (SetProperty(ref _selectedItemSummary, value))
                    OnSelectedItemChanged();
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                _dcChungTuView?.Refresh();

                if (!IsProcessing) LoadData();
            }
        }

        private ObservableCollection<ComboboxItem> _lockStatus = new ObservableCollection<ComboboxItem>();

        public ObservableCollection<ComboboxItem> LockStatus
        {
            get => _lockStatus;
            set => SetProperty(ref _lockStatus, value);
        }

        public bool IsButtonEnable
        {
            get
            {
                bool result = false;
                ObservableCollection<DcChungTuModel> lstItem = (TabIndex == ImportTabIndex.Data) ? Items : AdjustedEstimateSummaries;
                List<DcChungTuModel> lstSelected = lstItem.Where(x => x.Selected).ToList();
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && lstSelected.Any())
                {
                    result = true;
                }
                else
                {
                    result = lstSelected.Any() && (lstSelected.All(x => x.BKhoa) || lstSelected.All(x => !x.BKhoa));
                    IsLock = lstSelected.Any(x => x.BKhoa);
                }
                return result;
            }
        }

        private ComboboxItem _lockStatusSelected;

        public ComboboxItem LockStatusSelected
        {
            get => _lockStatusSelected;
            set
            {
                SetProperty(ref _lockStatusSelected, value);
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                if (!IsProcessing) LoadData();
            }
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null && Items.Count > 0)
                {
                    List<bool> selected = Items.Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (Items != null && _cbxVoucherTypeSelected != null)
                    {
                        Items.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem))).ForAll(c => c.Selected = value.Value);
                    }
                }
            }
        }

        public bool? IsAllItemSummariesSelected
        {
            get
            {
                if (AdjustedEstimateSummaries != null)
                {
                    List<bool> selected = AdjustedEstimateSummaries.Where(x => !x.IsChildSummary).Select(item => item.Selected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (AdjustedEstimateSummaries != null && _cbxVoucherTypeSelected != null)
                    {
                        AdjustedEstimateSummaries.Where(x => x.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem)) && !x.IsChildSummary).ForAll(c => c.Selected = value.Value);
                    }
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        public bool IsAggregate
        {
            get
            {
                IEnumerable<DcChungTuModel> listSelected = Items.Where(x => x.Selected);
                if (listSelected.Count() > 0)
                    if (listSelected.Any(x => !string.IsNullOrEmpty(x.STongHop)))
                        return false;
                    else return true;
                return false;
            }
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnSelectedItemChanged();
                OnPropertyChanged(nameof(IsExportAggregateData));
            }
        }

        public bool IsExportAggregateData => AdjustedEstimateSummaries.Any(x => x.Selected) || Items.Any(x => x.Selected);

        private bool _isRoleTroLyDonVi;
        public bool IsRoleTroLyDonVi
        {
            get => _isRoleTroLyDonVi;
            set => SetProperty(ref _isRoleTroLyDonVi, value);
        }

        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand AggregateCommand { get; }
        public RelayCommand ExportVoucherDataCommand { get; }
        public RelayCommand ExportDataByConditionCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportJsonCommand { get; }
        public RelayCommand ImportJsonCommand { get; }
        public RelayCommand UploadFileCommandHTTP { get; set; }
        public RelayCommand UploadFileCommandFTP { get; set; }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsButtonEnable));
            OnPropertyChanged(nameof(IsButtonSendDataEnable));
        }

        public AdjustedEstimateIndexViewModel(
            ISessionService sessionService,
            INsDonViService donViService,
            IMapper mapper,
            ILog logger,
            INsDcChungTuService chungTuService,
            INsDcChungTuChiTietService chungTuChiTietService,
            INsNguoiDungDonViService nguoiDungDonViService,
            IExportService exportService,
            IDanhMucService danhMucService,
            INsMucLucNganSachService mucLucNganSachService,
            IGroupService groupService,
            IHTTPUploadFileService hTTPUploadFileService,
            AdjustedEstimateDialogViewModel adjustedEstimateDialogViewModel,
            AdjustedEstimateDetailViewModel adjustedEstimateDetailViewModel,
            AdjustedEstimateReportViewModel adjustedEstimateReportViewModel,
            AdjustedEstimateImportJsonViewModel adjustedEstimateImportJsonViewModel,
            AdjustedEstimateTheoLanReportViewModel adjustedEstimateTheoLanReportViewModel,
            ReportDieuChinhDuToanTongHop reportBaoCaoNganSachNhaNuocTongHop,
            AdjustedEstimateImportViewModel adjustedEstimateImportViewModel,
            ExportAdjustedEstimateViewModel exportAdjustedEstimateViewModel,
            SendDataAdjustedEstimateViewModel sendDataAdjustedEstimateViewModel,
            INsDtChungTuService dtChungTuService)
        {
            _sessionService = sessionService;
            _donViService = donViService;
            _mapper = mapper;
            _logger = logger;
            _chungTuService = chungTuService;
            _groupService = groupService;
            _chungTuChiTietService = chungTuChiTietService;
            _nguoiDungDonViService = nguoiDungDonViService;
            _exportService = exportService;
            _danhMucService = danhMucService;
            _mucLucNganSachService = mucLucNganSachService;
            _dtChungTuService = dtChungTuService;
            _hTTPUploadFileService = hTTPUploadFileService;
            AdjustedEstimateDialogViewModel = adjustedEstimateDialogViewModel;
            AdjustedEstimateDetailViewModel = adjustedEstimateDetailViewModel;
            AdjustedEstimateReportViewModel = adjustedEstimateReportViewModel;
            AdjustedEstimateImportJsonViewModel = adjustedEstimateImportJsonViewModel;
            AdjustedEstimateTheoLanReportViewModel = adjustedEstimateTheoLanReportViewModel;
            AdjustedEstimateImportViewModel = adjustedEstimateImportViewModel;
            ReportBaoCaoNganSachNhaNuocTongHop = reportBaoCaoNganSachNhaNuocTongHop;
            ExportAdjustedEstimateViewModel = exportAdjustedEstimateViewModel;
            SendDataAdjustedEstimateViewModel = sendDataAdjustedEstimateViewModel;

            AggregateCommand = new RelayCommand(obj => OnAggregate());
            PrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
            ExportVoucherDataCommand = new RelayCommand(obj => OnExportVoucherData());
            ExportDataByConditionCommand = new RelayCommand(obj => OnExportDataByCondition());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportJsonCommand = new RelayCommand(obj => OnExportJson());
            ImportJsonCommand = new RelayCommand(obj => OnImportJson());
            UploadFileCommandHTTP = new RelayCommand(obj => OnUploadDialog(true));
            UploadFileCommandFTP = new RelayCommand(obj => OnUploadDialog(false));
        }

        public override void Init()
        {
            base.Init();
            IsProcessing = true;
            _sessionInfo = _sessionService.Current;
            _tabIndex = ImportTabIndex.Data;
            LoadVoucherType();
            LoadLockStatus();
            ChecKNhomQuanLy();
            LoadData();
            IsProcessing = false;
        }

        private void LoadVoucherType()
        {
            List<ComboboxItem> cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
                new ComboboxItem {DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key}
            };

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            CbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
        }

        private void LoadLockStatus()
        {
            List<ComboboxItem> lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Toàn bộ", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        private bool IsDonViRoot(string iIDMaDonVi) => iIDMaDonVi == _sessionInfo.IdDonVi;

        public override void LoadData(params object[] args)
        {
            EstimationVoucherCriteria condition = new EstimationVoucherCriteria
            {
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                UserName = _sessionInfo.Principal,
                VoucherTypes = CbxVoucherTypeSelected == null ? CbxVoucherType.Select(item => item.ValueItem).FirstOrDefault() : CbxVoucherTypeSelected.ValueItem
            };
            List<NsDcChungTuQuery> listChungTu = _chungTuService.FindByCondition(condition).ToList();
            _allVoucher = _mapper.Map<ObservableCollection<DcChungTuModel>>(listChungTu).ToList();

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                listChungTu = listChungTu.Where(x => x.BKhoa).ToList();
            }
            else if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                listChungTu = listChungTu.Where(x => !x.BKhoa).ToList();
            }

            if (_sessionService.Current.IsQuanLyDonViCha)
            {
                Items = _mapper.Map<ObservableCollection<DcChungTuModel>>(listChungTu.Where(x => !IsDonViRoot(x.IIdMaDonVi) && x.BDaTongHop.HasValue && !x.BDaTongHop.Value));

                List<DcChungTuModel> listChungTuTongHop = new List<DcChungTuModel>();
                foreach (NsDcChungTuQuery chungTu in listChungTu.Where(x => IsDonViRoot(x.IIdMaDonVi)))
                {
                    DcChungTuModel parent = _mapper.Map<DcChungTuModel>(chungTu);
                    parent.IsExpand = true;
                    listChungTuTongHop.Add(parent);
                    if (!string.IsNullOrEmpty(chungTu.STongHop))
                    {
                        List<DcChungTuModel> listChild = _mapper.Map<List<DcChungTuModel>>(listChungTu.Where(x => chungTu.STongHop.Split(",").Contains(x.SSoChungTu)).ToList());
                        listChild.ForEach(x => { x.IsChildSummary = true; x.SoChungTuParent = chungTu.SSoChungTu; });
                        listChungTuTongHop.AddRange(listChild);
                    }
                }
                AdjustedEstimateSummaries = new ObservableCollection<DcChungTuModel>(listChungTuTongHop);
            }
            else
            {
                Items = _mapper.Map<ObservableCollection<DcChungTuModel>>(listChungTu.Where(x => string.IsNullOrEmpty(x.STongHop)));
                AdjustedEstimateSummaries = new ObservableCollection<DcChungTuModel>();
            }

            foreach (DcChungTuModel model in Items)
            {
                model.PropertyChanged += Model_PropertyChanged;
            }
            _dcChungTuView = CollectionViewSource.GetDefaultView(Items);
            _dcChungTuView.Filter = VoucherFilter;
            SelectedItem = Items.FirstOrDefault();

            foreach (DcChungTuModel model in AdjustedEstimateSummaries)
            {
                model.PropertyChanged += Model_PropertyChanged;
            }
            _dcChungTuTongHopView = CollectionViewSource.GetDefaultView(AdjustedEstimateSummaries);
            _dcChungTuTongHopView.Filter = VoucherFilter;
            SelectedItemSummary = AdjustedEstimateSummaries.FirstOrDefault();
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DcChungTuModel.Selected))
            {
                OnPropertyChanged(nameof(IsButtonEnable));
                if (TabIndex == ImportTabIndex.Data)
                    OnPropertyChanged(nameof(IsAllItemsSelected));
                else
                {
                    OnPropertyChanged(nameof(IsAllItemSummariesSelected));
                }
                OnPropertyChanged(nameof(IsAggregate));
                OnPropertyChanged(nameof(IsExportAggregateData));
            }

            if (args.PropertyName == nameof(DcChungTuModel.IsCollapse))
            {
                ExpandChild();
            }
        }

        private void ExpandChild()
        {
            if (SelectedItemSummary != null)
            {
                AdjustedEstimateSummaries.Where(n => n.SoChungTuParent == SelectedItemSummary.SSoChungTu).Select(n => { n.IsExpand = !n.IsExpand; return n; }).ToList();
            }
            OnPropertyChanged(nameof(AdjustedEstimateSummaries));
        }

        private bool VoucherFilter(object obj)
        {
            bool result = true;
            DcChungTuModel item = (DcChungTuModel)obj;

            if (CbxVoucherTypeSelected == null)
                return true;

            return result && item.ILoaiChungTu.Equals(int.Parse(_cbxVoucherTypeSelected.ValueItem));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            StringBuilder messageBuilder2 = new StringBuilder();
            DcChungTuModel model = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;
            //check quyền được chỉnh sửa
            if (model.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleDelete, model.SNguoiTao));
                return;
            }

            messageBuilder.AppendFormat(Resources.DeleteChungTu, model.SSoChungTu, model.DNgayChungTu.HasValue ? model.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBoxHelper.Confirm(messageBuilder.ToString());
            if (result == MessageBoxResult.Yes)
            {
                if (IsUsedFromEstimation(model))
                {
                    messageBuilder2.AppendFormat(Resources.AlertDeleteAggregatedVoucher2);
                    MessageBoxResult result2 = MessageBoxHelper.Confirm(messageBuilder2.ToString());
                    if (result2 == MessageBoxResult.Yes)
                        DeleteSelectedVoucher(model);
                }
                else
                    DeleteSelectedVoucher(model);
            }
        }

        private void DeleteSelectedVoucher(DcChungTuModel model)
        {
            if (IsAggregatedVoucher(model))
            {
                MessageBoxHelper.Warning(Resources.AlertDeleteAggregatedVoucher);
                return;
            }

            Guid voucherId = model.Id;
            _chungTuChiTietService.DeleteByIdChungTu(voucherId);
            _chungTuService.Delete(voucherId);
            if (!string.IsNullOrEmpty(model.STongHop))
            {
                List<Guid> voucherIds = AdjustedEstimateSummaries.Where(x => x.SoChungTuParent == model.SSoChungTu).Select(x => x.Id).ToList();
                _chungTuService.UpdateAggregateStatus(string.Join(",", voucherIds));
            }
            MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
            OnRefresh();
        }

        private bool IsAggregatedVoucher(DcChungTuModel model)
        {
            List<string> aggregatedVouchers = AdjustedEstimateSummaries.Where(x => !string.IsNullOrEmpty(x.STongHop)).Select(x => x.STongHop).ToList();
            if (aggregatedVouchers.Any(x => x.Contains(model.SSoChungTu)))
                return true;
            return false;
        }

        private bool IsUsedFromEstimation(DcChungTuModel model)
        {
            return _dtChungTuService.CheckByIdAdjVoucher(model.Id);
        }

        protected override void OnLockUnLock()
        {
            //var model = TabIndex == ImportTabIndex.Data ? SelectedItem : SelectedItemSummary;
            //if (IsLock)
            //{
            //    //chỉ có đơn vị cha mới được mở khóa chứng từ
            //    List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
            //    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            //    {
            //        MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }
            //}
            //else
            //{
            //    //chỉ có người tạo chứng từ mới được khóa chứng từ
            //    if (model.SNguoiTao != _sessionInfo.Principal)
            //    {
            //        MessageBox.Show(string.Format(Resources.MsgRoleLock, model.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }
            //}
            //string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            //string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            //MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.Yes)
            //{
            //    var rs = _chungTuService.LockOrUnLock(model.Id, !model.BKhoa);
            //    if (rs == DBContextSaveChangeState.SUCCESS)
            //    {
            //        model.BKhoa = !model.BKhoa;
            //        OnPropertyChanged(nameof(IsLock));
            //        OnPropertyChanged(nameof(IsEdit));
            //        OnPropertyChanged(nameof(IsDelete));
            //        OnPropertyChanged(nameof(IsAggregate));
            //        MessageBoxHelper.Info(msgDone);
            //        LockStatusSelected = LockStatus.ElementAt(0);
            //    }
            //}

            ObservableCollection<DcChungTuModel> lstItem = (TabIndex == ImportTabIndex.Data) ? Items : AdjustedEstimateSummaries;

            if (IsLock)
            {
                string listSoChungTu = string.Join(", ", lstItem.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    //MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do không có quyền tổng hợp", listSoChungTu));
                    return;
                }

                string listSoChungTuDaTongHop = string.Join(", ", lstItem.Where(n => n.Selected && n.BDaTongHop.GetValueOrDefault() && n.BKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuDaTongHop))
                {
                    //MessageBoxHelper.Warning(Resources.AlertUnlockAggregatedVoucher);
                    MessageBoxHelper.Warning(string.Format("Đồng chí không được mở khóa chứng từ {0} do đã gửi lên tổng hợp", listSoChungTuDaTongHop));
                    return;
                }

            }
            else
            {
                string listSoChungTuInvalid = string.Join(", ", lstItem.Where(n => n.Selected & n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                {
                    //MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedItemElement.SNguoiTao));
                    MessageBoxHelper.Warning(string.Format("Đồng chí không có quyền khóa chứng từ {0} do không phải người tạo", listSoChungTuInvalid));
                    return;
                }

            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBoxHelper.Confirm(message);
            if (result == MessageBoxResult.Yes)
            {
                foreach (DcChungTuModel selectedItemElement in lstItem.Where(n => n.Selected))
                {
                    LockConfirmEventHandler(selectedItemElement);
                }
                MessageBoxHelper.Info(msgDone);
                //LockStatusSelected = IsLock ? LockStatus.ElementAt(2) : LockStatus.ElementAt(1);
                LockStatusSelected = LockStatus.ElementAt(0);

                //OnPropertyChanged(nameof(LockStatusSelected));
                //_listSettlementVoucher.Refresh();
                //_listSettlementVoucherSummary.Refresh();


            }

        }

        private void LockConfirmEventHandler(DcChungTuModel model)
        {
            //string msgDone = model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            _chungTuService.LockOrUnLock(model.Id, !model.BKhoa);
            model.BKhoa = !model.BKhoa;

            //OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsDelete));
            OnPropertyChanged(nameof(IsAggregate));
            //MessageBoxHelper.Info(msgDone);
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            AdjustedEstimateDialogViewModel.Id = Guid.Empty;
            AdjustedEstimateDialogViewModel.IsAggregate = false;
            AdjustedEstimateDialogViewModel.Init();
            AdjustedEstimateDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenAdjustedEstimateDetail((DcChungTuModel)obj);
            };
            AdjustedEstimateDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBoxHelper.Warning(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao));
                return;
            }

            AdjustedEstimateDialogViewModel.Id = SelectedItem.Id;
            AdjustedEstimateDialogViewModel.IsAggregate = false;
            AdjustedEstimateDialogViewModel.Init();
            AdjustedEstimateDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            AdjustedEstimateDialogViewModel.ShowDialogHost();
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

            List<DcChungTuModel> selectedItems = Items.Where(x => x.Selected).ToList();
            //kiểm tra trạng thái các bản ghi
            if (selectedItems.Any(x => !x.BKhoa))
            {
                MessageBoxHelper.Info(Resources.AlertAggregateUnLocked);
                return;
            }

            if (selectedItems.GroupBy(x => x.ILoaiChungTu).ToList().Count() > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateLoaiChungTu);
                return;
            }

            if (selectedItems.GroupBy(x => x.ILoaiChungTu).ToList().Count() > 1)
            {
                MessageBoxHelper.Info(Resources.AlertAggregateLoaiDuKienQt);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            Dictionary<Guid, List<string>> dicTongHop = new Dictionary<Guid, List<string>>();
            foreach (DcChungTuModel item in AdjustedEstimateSummaries.Where(x => !x.IsChildSummary))
            {
                if (!dicTongHop.ContainsKey(item.Id) && !string.IsNullOrEmpty(item.STongHop))
                    dicTongHop.Add(item.Id, item.STongHop.Split(",").ToList());
            }
            List<string> listChungTu = selectedItems.Select(x => x.SSoChungTu).ToList();
            if (dicTongHop.Values.Any(x => x.Intersect(listChungTu).Any()))
            {
                MessageBoxResult result = MessageBoxHelper.Confirm(Resources.AlertExistAggregateVoucher);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        foreach (KeyValuePair<Guid, List<string>> item in dicTongHop)
                        {
                            if (item.Value.Intersect(listChungTu).Any())
                                _chungTuService.Delete(item.Key);
                        }
                        CreateAggregateVoucher(selectedItems);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            else CreateAggregateVoucher(selectedItems);
        }

        private void CreateAggregateVoucher(List<DcChungTuModel> selectedSettlementVouchers)
        {
            DonVi _aggregateAgency = _donViService.FindByIdDonVi(_sessionInfo.IdDonVi, _sessionInfo.YearOfWork);
            AdjustedEstimateDialogViewModel.Id = Guid.Empty;
            AdjustedEstimateDialogViewModel.AggregateAdjustedEstimates = selectedSettlementVouchers;
            AdjustedEstimateDialogViewModel.AggregateAgency = _aggregateAgency;
            AdjustedEstimateDialogViewModel.AggregateLNS = string.Join(",", selectedSettlementVouchers.Select(x => x.SDslns).Distinct());
            AdjustedEstimateDialogViewModel.IsAggregate = true;
            AdjustedEstimateDialogViewModel.AggregateLoaiChungTu = selectedSettlementVouchers.FirstOrDefault().ILoaiChungTu;
            AdjustedEstimateDialogViewModel.AggregateLoaiDuKienQt = selectedSettlementVouchers.FirstOrDefault().ILoaiDuKien;
            AdjustedEstimateDialogViewModel.IsAggregate = true;
            AdjustedEstimateDialogViewModel.Init();
            AdjustedEstimateDialogViewModel.SavedAction = obj =>
            {
                TabIndex = ImportTabIndex.MLNS;
                this.OnRefresh();
                OnOpenAdjustedEstimateDetail((DcChungTuModel)obj);
            };
            AdjustedEstimateDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenAdjustedEstimateDetail((DcChungTuModel)obj);
        }

        private void OnOpenAdjustedEstimateDetail(DcChungTuModel SelectedItem, bool afterImport = false)
        {
            AdjustedEstimateDetailViewModel.Model = SelectedItem;
            if (CbxVoucherTypeSelected != null)
                AdjustedEstimateDetailViewModel.LoaiChungTu = int.Parse(CbxVoucherTypeSelected.ValueItem);
            else
                AdjustedEstimateDetailViewModel.LoaiChungTu = int.Parse(VoucherType.NSSD_Key);
            AdjustedEstimateDetailViewModel.AfterImport = afterImport;
            AdjustedEstimateDetailViewModel.UpdateVoucherEvent += RefreshAfterSaveData;
            AdjustedEstimateDetailViewModel.Init();
            AdjustedEstimateDetail view = new AdjustedEstimateDetail { DataContext = AdjustedEstimateDetailViewModel };
            view.ShowDialog();
            AdjustedEstimateDetailViewModel.UpdateVoucherEvent -= RefreshAfterSaveData;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (sender is DcChungTuModel model)
            {
                if (Items.FirstOrDefault(x => x.Id == model.Id) is DcChungTuModel item)
                {
                    item.FDieuChinh = model.FDieuChinh;
                }
                if (AdjustedEstimateSummaries.FirstOrDefault(x => x.Id == model.Id) is DcChungTuModel itemSummary)
                {
                    itemSummary.FDieuChinh = model.FDieuChinh;
                }
                _dcChungTuView?.Refresh();
                _dcChungTuTongHopView?.Refresh();
            }
        }

        private void OpenPrintDialog(object param)
        {
            AjustmentEstimatePrintType ajustmentEstimatePrintType = (AjustmentEstimatePrintType)((int)param);
            switch (ajustmentEstimatePrintType)
            {
                case AjustmentEstimatePrintType.PHANBO_DUTOAN:
                    AdjustedEstimateReportViewModel.Init();
                    AdjustedEstimateReportViewModel.ShowDialogHost();
                    break;
                case AjustmentEstimatePrintType.PHANBO_DUTOAN_THEOLAN:
                    AdjustedEstimateTheoLanReportViewModel.PrintType = AjustmentEstimatePrintType.PHANBO_DUTOAN_THEOLAN;
                    AdjustedEstimateTheoLanReportViewModel.Init();
                    AdjustedEstimateTheoLanReportViewModel.ShowDialogHost();
                    break;
                case AjustmentEstimatePrintType.QUYET_TOAN_CHI_NSNN:
                    ReportBaoCaoNganSachNhaNuocTongHop.PrintType = AjustmentEstimatePrintType.QUYET_TOAN_CHI_NSNN;
                    ReportBaoCaoNganSachNhaNuocTongHop.Init();
                    ReportBaoCaoNganSachNhaNuocTongHop.ShowDialogHost();
                    break;
            }
        }

        public void ChecKNhomQuanLy()
        {
            List<Guid> listSystem = _sessionInfo.SysGroupUsers.Select(x => x.IIDNhom).ToList();
            ObservableCollection<HtNhom> groupEntities = new ObservableCollection<HtNhom>(_groupService.FindAll(g => g.BKichHoat && listSystem.Contains(g.Id)));
            List<string> groupNames = _mapper.Map<ObservableCollection<HtNhom>, ObservableCollection<HTNhomModel>>(groupEntities).Select(x => x.STenNhom.ToLower().Trim()).ToList();
            IsRoleTroLyDonVi = !groupNames.Contains("admin") && !groupNames.Contains("ns trợ lý tổng hợp");
        }

        /// <summary>
        /// Xuất excel chứng từ chi tiết
        /// </summary>
        private void OnExportVoucherData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, ExportFileName.RPT_NS_DUTOAN_DIEUCHINH_TONGHOP);
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    string donVi1 = _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper();
                    string donVi2 = _sessionInfo.TenDonVi;
                    List<DcChungTuModel> settlementVouchers = new List<DcChungTuModel>();
                    if (AdjustedEstimateSummaries.Where(x => x.Selected).Any())
                    {
                        settlementVouchers = AdjustedEstimateSummaries.Where(x => x.Selected && IsDonViRoot(x.IIdMaDonVi)).ToList();
                    }
                    else if (Items.Where(x => x.Selected).Any())
                    {
                        settlementVouchers = Items.Where(x => x.Selected).ToList();
                    }
                    
                    foreach (DcChungTuModel item in settlementVouchers)
                    {
                        _adjustedEstimateDetailExports = GetAdjustedEstimateDetailSummary(item);
                        CalculateData();
                        _adjustedEstimateDetailExports = _adjustedEstimateDetailExports.Where(x => x.FDuToanNganSachNam != 0
                                                            || x.FDuToanChuyenNamSau != 0
                                                            || x.FDuKienQtDauNam != 0
                                                            || x.FDuKienQtCuoiNam != 0).OrderBy(x => x.SXauNoiMa).ToList();
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionInfo.YearOfWork).ToList();
                        RptDuToanDieuChinhTongHop ctTongHop = new RptDuToanDieuChinhTongHop
                        {
                            DonVi1 = donVi1,
                            DonVi2 = donVi2,
                            TieuDe1 = "Chứng từ chi tiết",
                            TieuDe2 = "Điều chỉnh số liệu dự toán",
                            ThoiGian = string.Format("Ngày chứng từ: {0}", item.DNgayChungTu.Value.ToString("dd/MM/yyyy")),
                            Items = _adjustedEstimateDetailExports.OrderBy(x => x.SXauNoiMa).ToList(),
                            Count = _adjustedEstimateDetailExports.Count,
                            MLNS = mucLucNganSaches
                        };
                        if (item.ILoaiDuKien == (int)EstimateSettlementType.SIX_MONTH)
                        {
                            ctTongHop.QtDauNam = 6;
                            ctTongHop.QtCuoiNam = 6;
                        }
                        else
                        {
                            ctTongHop.QtDauNam = 9;
                            ctTongHop.QtCuoiNam = 3;
                        }
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        foreach (System.Reflection.PropertyInfo prop in ctTongHop.GetType().GetProperties())
                        {
                            data.Add(prop.Name, prop.GetValue(ctTongHop));
                        }

                        fileNamePrefix = item.SSoChungTu + "_" + item.STenDonVi;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<DcChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, null, true);
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        List<ExportResult> result = (List<ExportResult>)e.Result;
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

        private List<DcChungTuChiTietModel> GetAdjustedEstimateDetail(DcChungTuModel model)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = model.Id,
                LNS = model.SDslns,
                YearOfWork = model.INamLamViec,
                YearOfBudget = model.INamNganSach,
                BudgetSource = model.IIdMaNguonNganSach,
                LoaiDuKien = model.ILoaiDuKien,
                LoaiChungTu = model.ILoaiChungTu,
                VoucherDate = model.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };
            List<string> soChungTus = model.STongHop?.Split(",").ToList();
            System.Linq.Expressions.Expression<Func<NsDcChungTu, bool>> predicate = PredicateBuilder.True<NsDcChungTu>();
            predicate = predicate.And(x => soChungTus.Contains(x.SSoChungTu));
            List<NsDcChungTu> chungTus = _chungTuService.FindByCondition(predicate).ToList();
            searchCondition.IdDonVi = string.Join(",", chungTus.Select(x => x.IIdMaDonVi).ToList());
            IEnumerable<NsDcChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);
            return _mapper.Map<List<DcChungTuChiTietModel>>(listChungTuChiTiet);
        }

        private List<DcChungTuChiTietModel> GetAdjustedEstimateDetailSummary(DcChungTuModel model)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = model.Id,
                LNS = model.SDslns,
                YearOfWork = model.INamLamViec,
                YearOfBudget = model.INamNganSach,
                BudgetSource = model.IIdMaNguonNganSach,
                LoaiDuKien = model.ILoaiDuKien,
                LoaiChungTu = model.ILoaiChungTu,
                VoucherDate = model.DNgayChungTu,
                UserName = _sessionInfo.Principal
            };

            searchCondition.IdDonVi = _sessionInfo.IdDonVi;

            IEnumerable<NsDcChungTuChiTietQuery> listChungTuChiTiet = _chungTuChiTietService.FindByConditionTongSo(searchCondition);
            return _mapper.Map<List<DcChungTuChiTietModel>>(listChungTuChiTiet);
        }

        private List<NsDcChungTuChiTiet> GetAdjustedEstimateDetail(NsDcChungTu model, bool isDetail)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = model.Id,
                LNS = model.SDslns,
                YearOfWork = model.INamLamViec,
                YearOfBudget = model.INamNganSach,
                BudgetSource = model.IIdMaNguonNganSach,
                LoaiDuKien = model.ILoaiDuKien,
                LoaiChungTu = model.ILoaiChungTu,
                VoucherDate = model.DNgayChungTu,
                UserName = _sessionInfo.Principal,
                IdDonVi = model.IIdMaDonVi
            };

            IEnumerable<NsDcChungTuChiTietQuery> listChungTuChiTiet;
            if (isDetail)
            {
                listChungTuChiTiet = _chungTuChiTietService.FindByCondition(searchCondition);

            }
            else
            {
                searchCondition.IdDonVi = _donViService.FindByCondition(n => n.NamLamViec == _sessionInfo.YearOfWork && n.ITrangThai == 1 && n.Loai == "0").Select(n => n.IIDMaDonVi).FirstOrDefault();
                listChungTuChiTiet = _chungTuChiTietService.FindByConditionTongSo(searchCondition);
            }


            return _mapper.Map<List<NsDcChungTuChiTiet>>(listChungTuChiTiet);
        }

        private void CalculateData()
        {
            _adjustedEstimateDetailExports.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FDuToanNganSachNam = 0;
                    x.FDuToanChuyenNamSau = 0;
                    x.FDuKienQtDauNam = 0;
                    x.FDuKienQtCuoiNam = 0;
                    return x;
                }).ToList();

            foreach (DcChungTuChiTietModel item in _adjustedEstimateDetailExports.Where(x => x.FDuToanNganSachNam != 0
                            || x.FDuToanChuyenNamSau != 0
                            || x.FDuKienQtDauNam != 0
                            || x.FDuKienQtCuoiNam != 0))
            {
                CalculateParent(item, item);
            }
            //_adjustedEstimateDetailExports.Select(x =>
            //{
            //    x.FTang = x.FTongCong > x.FDuToanNganSachNam ? x.FTongCong - x.FDuToanNganSachNam : 0;
            //    x.FGiam = x.FDuToanNganSachNam > x.FTongCong ? x.FDuToanNganSachNam - x.FTongCong : 0;
            //    return x;
            //}).ToList();
        }

        private void CalculateParent(DcChungTuChiTietModel currentItem, DcChungTuChiTietModel seftItem)
        {
            DcChungTuChiTietModel parrentItem = _adjustedEstimateDetailExports.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FDuToanNganSachNam += seftItem.FDuToanNganSachNam;
            parrentItem.FDuToanChuyenNamSau += seftItem.FDuToanChuyenNamSau;
            parrentItem.FDuKienQtDauNam += seftItem.FDuKienQtDauNam;
            parrentItem.FDuKienQtCuoiNam += seftItem.FDuKienQtCuoiNam;
            CalculateParent(parrentItem, seftItem);
        }

        private void OnImportData()
        {

            AdjustedEstimateImportViewModel.SavedAction = obj =>
            {
                _adjustedEstimateImportView.Close();
                OnRefresh();
                OnOpenAdjustedEstimateDetail((DcChungTuModel)obj, true);
            };
            AdjustedEstimateImportViewModel.Init();
            _adjustedEstimateImportView = new AdjustedEstimateImport { DataContext = AdjustedEstimateImportViewModel };
            _adjustedEstimateImportView.ShowDialog();
        }

        #region Json
        private void OnImportJson()
        {
            AdjustedEstimateImportJsonViewModel.Init();
            AdjustedEstimateImportJsonViewModel.SavedAction = obj =>
            {
                OnRefresh();
                _importJsonView.Close();
            };
            _importJsonView = new AdjustedEstimateImportJson { DataContext = AdjustedEstimateImportJsonViewModel };
            _importJsonView.Show();
        }

        private void OnExportJson()
        {
            if (CbxVoucherTypeSelected != null && CbxVoucherTypeSelected.ValueItem.Equals(VoucherType.NSSD_Key))
            {
                if (!Items.Any(n => n.Selected))
                {
                    MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                    return;
                }
                List<NsDcChungTu> lstData = GetDataExportJson();
                _exportService.OpenJson(lstData);
            }
            else
            {
                if (!AdjustedEstimateSummaries.Any(n => n.Selected))
                {
                    MessageBoxHelper.Error(Resources.MsgRecordEmpty);
                    return;
                }
                List<NsDcChungTu> lstData = GetDataExportJson();
                _exportService.OpenJson(lstData);
            }

        }

        private List<NsDcChungTu> GetDataExportJson()
        {
            List<NsDcChungTu> lstData = new List<NsDcChungTu>();

            if (CbxVoucherTypeSelected != null && CbxVoucherTypeSelected.ValueItem.Equals(VoucherType.NSSD_Key))
            {
                if (!Items.Any(n => n.Selected)) return lstData;
                ObservableCollection<NsDcChungTu> itemsMap = _mapper.Map<ObservableCollection<NsDcChungTu>>(Items.Where(n => n.Selected));
                return itemsMap.Select(n =>
                {
                    n.ListDetail = GetAdjustedEstimateDetail(n, true);
                    return n;
                }).ToList();
            }
            else
            {
                if (!AdjustedEstimateSummaries.Any(n => n.Selected)) return lstData;
                ObservableCollection<NsDcChungTu> itemsMap = _mapper.Map<ObservableCollection<NsDcChungTu>>(AdjustedEstimateSummaries.Where(n => n.Selected));
                return itemsMap.Select(n =>
                {
                    n.ListDetail = GetAdjustedEstimateDetail(n, false);
                    return n;
                }).ToList();
            }
        }
        #endregion

        private void OnExportDataByCondition()
        {
            ExportAdjustedEstimateViewModel.ListChungTu = _allVoucher;
            ExportAdjustedEstimateViewModel.Init();
            ExportAdjustedEstimateViewModel.ShowDialogHost();
        }

        /*
         *  Thêm popup chọn tiêu chí để gửi dữ liệu
         */
        private async void OnUploadDialog(bool isSendHTTP)
        {
            if (AdjustedEstimateSummaries.Where(n => n.Selected).Count() == 0)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn ít nhất 1 bản ghi !");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            IsLoading = true;
            try
            {
                (int, string) info = await _hTTPUploadFileService.GetToken(isSendHTTP);
                if (info.Item1 != 200)
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel(info.Item2).ShowDialogHost();
                    return;
                }
                else if (string.IsNullOrEmpty(info.Item2))
                {
                    IsLoading = false;
                    new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                    return;
                }
            }
            catch (ConfigurationErrorsException)
            {
                IsLoading = false;
                new NSMessageBoxViewModel("Cấu hình sai đường dẫn hoặc cổng HTTP").ShowDialogHost();
                return;
            }
            SendDataAdjustedEstimateViewModel._adjustedEstimateSummaries = _adjustedEstimateSummaries;
            SendDataAdjustedEstimateViewModel.TabIndex = TabIndex;
            SendDataAdjustedEstimateViewModel.IsSendHTTP = isSendHTTP;
            SendDataAdjustedEstimateViewModel.Init();
            SendDataAdjustedEstimateViewModel.ClosePopup += RefreshAfterClosePopupSendData;
            View.Budget.Estimate.AdjustedEstimate.SendDataAdjustedEstimate.SendDataAdjustedEstimate addView = new View.Budget.Estimate.AdjustedEstimate.SendDataAdjustedEstimate.SendDataAdjustedEstimate() { DataContext = SendDataAdjustedEstimateViewModel };
            IsLoading = false;
            DialogHost.Show(addView, SettlementScreen.ROOT_DIALOG, null, null);
        }

        private void RefreshAfterClosePopupSendData(object sender, EventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            OnRefresh();
        }

    }
}
