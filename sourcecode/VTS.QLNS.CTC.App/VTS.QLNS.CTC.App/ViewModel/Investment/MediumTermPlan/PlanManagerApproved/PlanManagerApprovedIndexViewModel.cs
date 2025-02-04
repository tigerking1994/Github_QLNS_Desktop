using AutoMapper;
using FlexCel.Core;
using MaterialDesignThemes.Wpf;
using log4net;
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
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManager
{
    public class PlanManagerApprovedIndexViewModel : GridViewModelBase<VdtKhvKeHoach5NamModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };

        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvKeHoach5NamService _vdtKhvKeHoach5NamService;
        private readonly IVdtKhvKeHoach5NamChiTietService _vdtKhvKeHoach5NamChiTietService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly IAttachmentService _iAttachmentService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly IDanhMucService _danhMucService;

        private ICollectionView _vdtKhvKeHoach5NamView;

        public override string FuncCode => NSFunctionCode.INVESTMENT_MEDIUM_TERM_PLAN_PLAN_MANAGER_APPROVED_INDEX;
        public override string GroupName => MenuItemContants.GROUP_MID_TERM_PLAN;

        public override string Name => "Kế hoạch trung hạn được duyệt";
        public override string Description => "Kế hoạch trung hạn được duyệt";
        public override Type ContentType => typeof(PlanManagerApprovedIndex);
        public override PackIconKind IconKind => PackIconKind.Approve;

        private ObservableCollection<VdtKhvKeHoach5NamReportQuery> _itemsReport;
        public ObservableCollection<VdtKhvKeHoach5NamReportQuery> ItemsReport
        {
            get => _itemsReport;
            set => SetProperty(ref _itemsReport, value);
        }

        private VdtKhvKeHoach5NamExportQuery _rptSummary = new VdtKhvKeHoach5NamExportQuery();
        public VdtKhvKeHoach5NamExportQuery RptSummary
        {
            get => _rptSummary;
            set => SetProperty(ref _rptSummary, value);
        }

        
        public bool IsEnableLock => SelectedItem != null;
        //public bool IsEdit => SelectedItem != null && SelectedItem.BActive;
        public bool IsEdit => SelectedItem != null && SelectedItem.BActive && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;

        private string _searchSoKeHoachText;
        public string SearchSoKeHoachText
        {
            get => _searchSoKeHoachText;
            set => SetProperty(ref _searchSoKeHoachText, value);
        }

        private DateTime? _dNgayLap;
        public DateTime? DNgayLap
        {
            get => _dNgayLap;
            set
            {
                SetProperty(ref _dNgayLap, value);
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }
        }

        private string _giaiDoan;
        public string GiaiDoan
        {
            get => _giaiDoan;
            set => SetProperty(ref _giaiDoan, value);
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }    
        }

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

        private ObservableCollection<ComboboxItem> _projectTypeItems;
        public ObservableCollection<ComboboxItem> ProjectTypeItems
        {
            get => _projectTypeItems;
            set => SetProperty(ref _projectTypeItems, value);
        }

        private ComboboxItem _selectedProjectTypeSelected;
        public ComboboxItem SelectedProjectTypeSelected
        {
            get => _selectedProjectTypeSelected;
            set
            {
                SetProperty(ref _selectedProjectTypeSelected, value);
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }    
        }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FixDataCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportKHTHDDCommand { get; set; }
        public RelayCommand ImportCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand UploadFileCommand { get; set; }
        public PlanManagerApprovedDialogViewModel PlanManagerApprovedDialogViewModel { get; set; }
        public PlanManagerApprovedDetailViewModel PlanManagerApprovedDetailViewModel { get; set; }
        public PrintReportMediumTermPlanViewModel PrintReportMediumTermPlanViewModel { get; set; }
        public PlanManagerApprovedImportViewModel PlanManagerApprovedImportModel { get; set; }
        public PlanManagerApprovedImport PlanManagerApprovedImport { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public PlanManagerApprovedIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvKeHoach5NamService vdtKhvKeHoach5NamService,
            IVdtKhvKeHoach5NamChiTietService vdtKhvKeHoach5NamChiTietService,
            IExportService exportService,
            IAttachmentService iAttachmentService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            FtpStorageService ftpStorageService,
            PlanManagerApprovedDialogViewModel planManagerApprovedDialogViewModel,
            PlanManagerApprovedDetailViewModel planManagerApprovedDetailViewModel,
            PrintReportMediumTermPlanViewModel printReportMediumTermPlanViewModel,
            PlanManagerApprovedImportViewModel planManagerApprovedImportModel,
            AttachmentViewModel attachmentViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _vdtKhvKeHoach5NamService = vdtKhvKeHoach5NamService;
            _vdtKhvKeHoach5NamChiTietService = vdtKhvKeHoach5NamChiTietService;
            _iAttachmentService = iAttachmentService;
            _ftpStorageService = ftpStorageService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            PlanManagerApprovedDialogViewModel = planManagerApprovedDialogViewModel;
            PlanManagerApprovedDetailViewModel = planManagerApprovedDetailViewModel;
            PrintReportMediumTermPlanViewModel = printReportMediumTermPlanViewModel;
            PlanManagerApprovedImportModel = planManagerApprovedImportModel;
            AttachmentViewModel = attachmentViewModel;

            SearchCommand = new RelayCommand(obj => _vdtKhvKeHoach5NamView.Refresh());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            FixDataCommand = new RelayCommand(obj => OnFixData());
            ExportCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportKHTHDDCommand = new RelayCommand(obj => OnExportKHTHDD());
            ImportCommand = new RelayCommand(obj => OnImport());
            PrintActionCommand = new RelayCommand(obj => OnOpenReport(obj));
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);
            UploadFileCommand = new RelayCommand(obj => OnUpload());

        }

        public override void Init()
        {
            try
            {
                LoadProjectType();
                LoadDonViQuanLy();
                LoadData();
                OnResetFilter();

                PlanManagerApprovedDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadProjectType()
        {
            ProjectTypeItems = new ObservableCollection<ComboboxItem>(new[]
            {
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI), ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()),
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP), ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString())
            });
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                    .Where(n => lstDonViExclude.Contains(n.Loai));

                DrpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
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
                //int yearOfWork = _sessionService.Current.YearOfWork;
                //List<VdtKhvKeHoach5NamQuery> listChungTu = _vdtKhvKeHoach5NamService.FindConditionIndex(yearOfWork).ToList();
                var listChungTu = _vdtKhvKeHoach5NamService.FindAllDuocDuyet().ToList();
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    listChungTu = listChungTu.Where(x => x.BKhoa.HasValue && x.BKhoa.Value).ToList();
                }

                Items = _mapper.Map<ObservableCollection<VdtKhvKeHoach5NamModel>>(listChungTu);

                _vdtKhvKeHoach5NamView = CollectionViewSource.GetDefaultView(Items);
                _vdtKhvKeHoach5NamView.Filter = ListSettlementVoucherFilter;

                if (Items != null && Items.Count > 0)
                    SelectedItem = Items.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            try
            {
                bool result = true;
                var item = (VdtKhvKeHoach5NamModel)obj;

                if (!string.IsNullOrEmpty(SearchSoKeHoachText))
                {
                    result = result && item.SSoQuyetDinh.ToLower().Contains(SearchSoKeHoachText.ToLower());
                }

                if (DNgayLap != null)
                {
                    result = result && item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.ToString("yyyy-MM-dd").ToLower().Contains(DNgayLap.Value.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(GiaiDoan))
                {
                    result = result && !string.IsNullOrEmpty(item.GiaiDoan) && item.GiaiDoan.ToLower().Contains(GiaiDoan.ToLower());
                }

                if (DrpDonViQuanLySelected != null)
                {
                    result = result && DrpDonViQuanLySelected.DisplayItem.ToLower().Contains(item.STenDonVi.ToLower());
                }

                if (SelectedProjectTypeSelected != null)
                {
                    result = result && item.ILoai != null && item.ILoai == int.Parse(SelectedProjectTypeSelected.ValueItem);
                }

                return result;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (Items.Count() > 0)
            {
                VdtKhvKeHoach5NamModel item = Items.Where(x => x.Id == SelectedItem.Id).First();
                item.FGiaTriKeHoach = ((VdtKhvKeHoach5NamModel)sender).FGiaTriKeHoach;
            }

            this.OnRefresh();
        }

        private static void SelectAll(bool select, IEnumerable<VdtKhvKeHoach5NamModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }

        protected override void OnAdd()
        {
            try
            {
                PlanManagerApprovedDialogViewModel.Model = new VdtKhvKeHoach5NamModel();
                PlanManagerApprovedDialogViewModel.Model.IIdMaDonVi = _sessionService.Current.IdDonVi;
                PlanManagerApprovedDialogViewModel.Model.STenDonVi = _sessionService.Current.TenDonVi;
                var itemDonViQuanLy = _nsDonViService.FindByIdDonVi(PlanManagerApprovedDialogViewModel.Model.IIdMaDonVi, _sessionService.Current.YearOfWork);
                if (itemDonViQuanLy != null)
                {
                    PlanManagerApprovedDialogViewModel.Model.IIdDonViId = itemDonViQuanLy.Id;
                }
                PlanManagerApprovedDialogViewModel.IsDieuChinh = false;
                PlanManagerApprovedDialogViewModel.Init();
                PlanManagerApprovedDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamModel item = (VdtKhvKeHoach5NamModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanDetail(item);
                };
                PlanManagerApprovedDialogViewModel.ShowDialogHost();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _vdtKhvKeHoach5NamService.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherUpdateKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                //PlanManagerApprovedDialogViewModel.Model = SelectedItem;
                PlanManagerApprovedDialogViewModel.Model = ObjectCopier.Clone(SelectedItem);
                PlanManagerApprovedDialogViewModel.IsDieuChinh = false;
                PlanManagerApprovedDialogViewModel.Init();
                PlanManagerApprovedDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamModel item = (VdtKhvKeHoach5NamModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanDetail(item);
                };
                PlanManagerApprovedDialogViewModel.ShowDialogHost();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnFixData()
        {
            try
            {
                if(SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _vdtKhvKeHoach5NamService.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherModifiedKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                //PlanManagerApprovedDialogViewModel.Model = SelectedItem;
                PlanManagerApprovedDialogViewModel.Model = ObjectCopier.Clone(SelectedItem);
                PlanManagerApprovedDialogViewModel.IsDieuChinh = true;
                PlanManagerApprovedDialogViewModel.Init();
                PlanManagerApprovedDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamModel item = (VdtKhvKeHoach5NamModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanDetail(item);
                };
                PlanManagerApprovedDialogViewModel.Model.SSoQuyetDinh = null;
                PlanManagerApprovedDialogViewModel.Model.DNgayQuyetDinh = null;
                PlanManagerApprovedDialogViewModel.ShowDialogHost();                
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null)
                {
                    var entity = _vdtKhvKeHoach5NamService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoQuyetDinh, SelectedItem.DNgayQuyetDinh.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.DNgayQuyetDinh.Value) : string.Empty);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            try
            {
                if (result != NSDialogResult.Yes) return;

                List<VdtKhvKeHoach5NamChiTiet> lstKeHoachChiTiet = _vdtKhvKeHoach5NamChiTietService.FindByIdKeHoach5Nam(SelectedItem.Id).ToList();

                if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                {
                    foreach (var item in lstKeHoachChiTiet)
                    {
                        _vdtKhvKeHoach5NamChiTietService.Delete(item.Id);
                    }
                }

                if (SelectedItem != null && SelectedItem.IIdParentId.HasValue)
                {
                    VdtKhvKeHoach5Nam itemGoc = _vdtKhvKeHoach5NamService.FindById(SelectedItem.IIdParentId.Value);
                    if (itemGoc != null)
                    {
                        itemGoc.BActive = true;
                        _vdtKhvKeHoach5NamService.Update(itemGoc);
                    }

                    List<VdtKhvKeHoach5NamChiTiet> lstGoc = _vdtKhvKeHoach5NamChiTietService.FindByIdKeHoach5Nam(SelectedItem.IIdParentId.Value).ToList();

                    if (lstGoc != null && lstGoc.Count > 0)
                    {
                        lstGoc.Select(x => { x.BActive = true; return x; }).ToList();
                        foreach (var item in lstGoc)
                        {
                            _vdtKhvKeHoach5NamChiTietService.Update(item);
                        }
                    }
                }

                _vdtKhvKeHoach5NamService.Delete(SelectedItem.Id);
                _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int) AttachmentEnum.Type.VDT_KHTH_DUOCDUYET);

                var itemDeleted = Items.Where(x => x.Id == SelectedItem.Id).First();
                Items.Remove(itemDeleted);

                OnPropertyChanged(nameof(Items));
                LoadData();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                DNgayLap = null;
                GiaiDoan = string.Empty;
                DrpDonViQuanLySelected = null;
                SelectedProjectTypeSelected = null;

                _vdtKhvKeHoach5NamView.Refresh();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            try
            {
                base.OnSelectionDoubleClick(obj);
                VdtKhvKeHoach5NamModel item = (VdtKhvKeHoach5NamModel)obj;
                item.IsViewDetail = true;
                OnOpenPlanDetail(item);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnViewAttachment(object obj)
        {
            try
            {
                if (SelectedItem != null)
                {
                    AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_KHTH_DUOCDUYET;
                    AttachmentViewModel.ObjectId = SelectedItem.Id;
                    AttachmentViewModel.Init();
                    AttachmentViewModel.ShowDialogHost();
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenPlanDetail(VdtKhvKeHoach5NamModel SelectedItem)
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _vdtKhvKeHoach5NamService.FindById(SelectedItem.Id);
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDetailPermissionWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
                PlanManagerApprovedDetailViewModel.Model = SelectedItem;
                PlanManagerApprovedDetailViewModel.Init();
                PlanManagerApprovedDetailViewModel.ShowDialog();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    VdtKhvKeHoach5NamModel item = Items.FirstOrDefault(n => n.Selected);
                    List<VdtKhvKeHoach5NamExportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietService.GetDataExportKeHoachTrungHan(item.Id.ToString()).ToList();
                    RptSummary.FHanMucDauTu = lstQuery.Sum(x => x.FHanMucDauTu);
                    RptSummary.FVonDaGiao = lstQuery.Sum(x => x.FVonDaGiao);
                    RptSummary.FTongSoNhuCauNSQP = lstQuery.Sum(x => x.FTongSoNhuCauNSQP);
                    RptSummary.FVonBoTriTuNamDenNam = lstQuery.Sum(x => x.FVonBoTriTuNamDenNam);
                    RptSummary.FGiaTriSau5Nam = lstQuery.Sum(x => x.FGiaTriSau5Nam);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("TenDonVi", !string.IsNullOrEmpty(item.STenDonVi) ? item.STenDonVi.ToUpper() : string.Empty);
                    data.Add("Year", item.IGiaiDoanTu);
                    data.Add("YearAfter", item.IGiaiDoanDen);
                    data.Add("Period", string.Format("{0} - {1}", item.IGiaiDoanTu, item.IGiaiDoanDen));
                    data.Add("Items", lstQuery);
                    data.Add("FHanMucDauTuSum", RptSummary.FHanMucDauTu);
                    data.Add("FVonDaGiaoSum", RptSummary.FVonDaGiao);
                    data.Add("FTongSoNhuCauNSQPSum", RptSummary.FTongSoNhuCauNSQP);
                    data.Add("FVonBoTriTuNamDenNamSum", RptSummary.FVonBoTriTuNamDenNam);
                    data.Add("FGiaTriSau5NamSum", RptSummary.FGiaTriSau5Nam);
                    data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                    data.Add("dv", _nsDonViService.GetDonViExportByNamLamViec(_sessionService.Current.YearOfWork).ToList());

                    string templateFileName = MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_TEMPLATE;
                    string fileNamePrefix = string.Format("{0}_{1}", MediumTermPlanType.OUTPUT_KE_HOACH_TRUNG_HAN_TEMPLATE, item.BIsGoc.Value ? "Goc" : "DieuChinh");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamExportQuery, NSDonViExportQuery>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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

        private void OnExportKHTHDD()
        {
            try
            {
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }

                List<VdtKhvKeHoach5NamModel> lstItem = Items.Where(x => x.Selected).ToList();
                if (!Items.Any(n => n.Selected))
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat(Resources.VoucherExportEmpty);
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                try
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;

                        List<ExportResult> results = new List<ExportResult>();
                        string templateFileName = MediumTermPlanType.EXPORT_KE_HOACH_TRUNG_HAN_DUOC_DUYET_TEMPLATE;
                        string fileName;
                        string fileNamePrefix;
                        string fileNameWithoutExtension;
                        FormatNumber formatNumber = new FormatNumber(Int32.Parse(DonViTinh.NGHIN_DONG_VALUE), ExportType.EXCEL);

                        List<VdtKhvKeHoach5NamModel> lstItemGoc = lstItem.Where(x => x.BIsGoc.Value).ToList();
                        List<VdtKhvKeHoach5NamModel> lstItemModified = lstItem.Where(x => !x.BIsGoc.Value).ToList();
                        Dictionary<string, List<VdtKhvKeHoach5NamModel>> dctDataExport = lstItemGoc.GroupBy(x => new { x.IIdMaDonVi, x.NamLamViec, x.IGiaiDoanTu, x.IGiaiDoanDen }).ToDictionary(x =>
                            string.Join(",", lstItemGoc.Where(y => y.IIdMaDonVi == x.Key.IIdMaDonVi && y.NamLamViec == x.Key.NamLamViec
                                && y.IGiaiDoanTu == x.Key.IGiaiDoanTu && y.IGiaiDoanDen == x.Key.IGiaiDoanDen)
                                    .Select(x => x.Id.ToString())), x => x.ToList());

                        var dctDataModified = lstItemModified.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.ToList());
                        dctDataModified.Select(obj =>
                        {
                            dctDataExport.Add(string.Join(",", obj.Key.ToString()), obj.Value);
                            return obj;
                        }).ToList();

                        foreach (var item in dctDataExport.Keys)
                        {
                            List<VdtKhvKeHoach5NamExportQuery> lstDataCTMM = new List<VdtKhvKeHoach5NamExportQuery>();
                            List<VdtKhvKeHoach5NamExportQuery> lstDataCTCT = new List<VdtKhvKeHoach5NamExportQuery>();
                            List<string> lstId = item.Split(",").ToList();
                            foreach (var item1 in lstId)
                            {
                                List<VdtKhvKeHoach5NamExportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietService.GetDataExportKeHoachTrungHan(item1.ToString()).ToList();
                                int? voucherType = Items.Where(x => x.Id.ToString() == item1).Select(x => x.ILoai).FirstOrDefault();
                                if (voucherType.Equals((int)(LoaiDuAnEnum.Type.KHOI_CONG_MOI)))
                                {
                                    lstDataCTMM.AddRange(lstQuery);
                                }
                                else
                                {
                                    lstDataCTCT.AddRange(lstQuery);
                                }
                            }

                            Dictionary<string, object> data = new Dictionary<string, object>();
                            VdtKhvKeHoach5NamExportQuery RptSummaryCtmm = new VdtKhvKeHoach5NamExportQuery();
                            VdtKhvKeHoach5NamExportQuery RptSummaryCtct = new VdtKhvKeHoach5NamExportQuery();

                            int iGiaiDoanTu = dctDataExport[item].FirstOrDefault().IGiaiDoanTu;
                            int iGiaiDoanDen = dctDataExport[item].FirstOrDefault().IGiaiDoanDen;
                            string sTenDonVi = _nsDonViService.FindByIdDonVi(dctDataExport[item].FirstOrDefault().IIdMaDonVi, _sessionService.Current.YearOfWork).TenDonVi;

                            RptSummaryCtmm.FHanMucDauTu = lstDataCTMM.Sum(x => x.FHanMucDauTu);
                            RptSummaryCtmm.FVonDaGiao = lstDataCTMM.Sum(x => x.FVonDaGiao);
                            RptSummaryCtmm.FTongSoNhuCauNSQP = lstDataCTMM.Sum(x => x.FTongSoNhuCauNSQP);
                            RptSummaryCtmm.FVonBoTriTuNamDenNam = lstDataCTMM.Sum(x => x.FVonBoTriTuNamDenNam);
                            RptSummaryCtmm.FGiaTriSau5Nam = lstDataCTMM.Sum(x => x.FGiaTriSau5Nam);

                            RptSummaryCtct.FHanMucDauTu = lstDataCTCT.Sum(x => x.FHanMucDauTu);
                            RptSummaryCtct.FVonDaGiao = lstDataCTCT.Sum(x => x.FVonDaGiao);
                            RptSummaryCtct.FTongSoNhuCauNSQP = lstDataCTCT.Sum(x => x.FTongSoNhuCauNSQP);
                            RptSummaryCtct.FVonBoTriTuNamDenNam = lstDataCTCT.Sum(x => x.FVonBoTriTuNamDenNam);
                            RptSummaryCtct.FGiaTriSau5Nam = lstDataCTCT.Sum(x => x.FGiaTriSau5Nam);

                            data.Add("Year", iGiaiDoanTu);
                            data.Add("YearAfter", iGiaiDoanDen);
                            data.Add("Period", string.Format("{0} - {1}", iGiaiDoanTu, iGiaiDoanDen));
                            data.Add("FHanMucDauTuCtmmSum", RptSummaryCtmm.FHanMucDauTu);
                            data.Add("FVonDaGiaoCtmmSum", RptSummaryCtmm.FVonDaGiao);
                            data.Add("FTongSoNhuCauNSQPCtmmSum", RptSummaryCtmm.FTongSoNhuCauNSQP);
                            data.Add("FVonBoTriTuNamDenNamCtmmSum", RptSummaryCtmm.FVonBoTriTuNamDenNam);
                            data.Add("FGiaTriSau5NamCtmmSum", RptSummaryCtmm.FGiaTriSau5Nam);
                            data.Add("FHanMucDauTuCtctSum", RptSummaryCtct.FHanMucDauTu);
                            data.Add("FVonDaGiaoCtctSum", RptSummaryCtct.FVonDaGiao);
                            data.Add("FTongSoNhuCauNSQPCtctSum", RptSummaryCtct.FTongSoNhuCauNSQP);
                            data.Add("FVonBoTriTuNamDenNamCtctSum", RptSummaryCtct.FVonBoTriTuNamDenNam);
                            data.Add("FGiaTriSau5NamCtctSum", RptSummaryCtct.FGiaTriSau5Nam);
                            data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                            data.Add("TenDonVi", !string.IsNullOrEmpty(sTenDonVi) ? sTenDonVi.ToUpper() : string.Empty);
                            data.Add("dv", _nsDonViService.GetDonViExportByNamLamViec(_sessionService.Current.YearOfWork).ToList());
                            data.Add("ItemsCtmm", lstDataCTMM);
                            data.Add("ItemsCtct", lstDataCTCT);
                            data.Add("FormatNumber", formatNumber);

                            fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.OUTPUT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                            fileNamePrefix = string.Format("{0}-{1}_{2}-{3}_{4}_{5}",
                                dctDataExport[item].FirstOrDefault().IIdMaDonVi, sTenDonVi,
                                iGiaiDoanTu, iGiaiDoanDen, (dctDataExport[item].FirstOrDefault().BIsGoc.Value) ? "Goc" : "DieuChinh", fileName);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<VdtKhvKeHoach5NamExportQuery, NSDonViExportQuery>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                        e.Result = results;
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = (List<ExportResult>)e.Result;
                            if(result != null && result.Count() == 1)
                            {
                                _exportService.Open(result.FirstOrDefault(), ExportType.EXCEL);
                            }
                            else
                            {
                                _exportService.Open(result, ExportType.EXCEL);
                            }
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void OnUpload()
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }
                VdtKhvKeHoach5NamModel item = Items.FirstOrDefault(n => n.Selected);
                List<VdtKhvKeHoach5NamExportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietService.GetDataExportKeHoachTrungHan(item.Id.ToString()).ToList();
                RptSummary.FHanMucDauTu = lstQuery.Sum(x => x.FHanMucDauTu);
                RptSummary.FVonDaGiao = lstQuery.Sum(x => x.FVonDaGiao);
                RptSummary.FTongSoNhuCauNSQP = lstQuery.Sum(x => x.FTongSoNhuCauNSQP);
                RptSummary.FVonBoTriTuNamDenNam = lstQuery.Sum(x => x.FVonBoTriTuNamDenNam);
                RptSummary.FGiaTriSau5Nam = lstQuery.Sum(x => x.FGiaTriSau5Nam);

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("TenDonVi", !string.IsNullOrEmpty(item.STenDonVi) ? item.STenDonVi.ToUpper() : string.Empty);
                data.Add("Year", item.IGiaiDoanTu);
                data.Add("YearAfter", item.IGiaiDoanDen);
                data.Add("Period", string.Format("{0} - {1}", item.IGiaiDoanTu, item.IGiaiDoanDen));
                data.Add("Items", lstQuery);
                data.Add("FHanMucDauTuSum", RptSummary.FHanMucDauTu);
                data.Add("FVonDaGiaoSum", RptSummary.FVonDaGiao);
                data.Add("FTongSoNhuCauNSQPSum", RptSummary.FTongSoNhuCauNSQP);
                data.Add("FVonBoTriTuNamDenNamSum", RptSummary.FVonBoTriTuNamDenNam);
                data.Add("FGiaTriSau5NamSum", RptSummary.FGiaTriSau5Nam);
                data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                data.Add("dv", _nsDonViService.GetDonViExportByNamLamViec(_sessionService.Current.YearOfWork).ToList());

                string templateFileName = MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_TEMPLATE;
                string fileNamePrefix = string.Format("{0}_{1}", MediumTermPlanType.OUTPUT_KE_HOACH_TRUNG_HAN_TEMPLATE, item.BIsGoc.Value ? "Goc" : "DieuChinh");
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                 var xlsFile = _exportService.Export<VdtKhvKeHoach5NamExportQuery, NSDonViExportQuery>(templateFileName, data);
                 var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                //
                string filePathLocal = string.Empty;
                string sStage = string.Empty;
                string sMaDonVi = string.Empty;
                if (SelectedItem != null)
                {
                    sStage = SelectedItem.GiaiDoan;
                }
                if (item.IIdDonViId != Guid.Empty)
                {
                    var itemDonVi = _nsDonViService.FindById(item.IIdDonViId);
                    if (itemDonVi != null)
                    {
                        sMaDonVi = itemDonVi.IIDMaDonVi;
                    }
                }
                _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                string sFolderRoot = ConstantUrlPathPhanHe.UrlKhchddWinformReceive;
                var strUrl = string.Format("{0}/{1}/{2}", sMaDonVi, sFolderRoot, sStage);
                if (!File.Exists(strUrl))
                {
                    string strActiveFileName = "";
                    string[] splitActiveFiName = xlsFile.ActiveFileName.Split("\\");
                    if (strActiveFileName != null && splitActiveFiName.Length != 0)
                    {
                        strActiveFileName = splitActiveFiName[splitActiveFiName.Length - 1];
                    }
                    VdtFtpRoot dataRoot = new VdtFtpRoot();
                    List<string> configCodes = new List<string>()
                    {
                        STORAGE_CONFIG.FTP_HOST
                    };
                    var rs = _danhMucService.FindByCodes(configCodes).ToList();
                    var SIpAddress = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                    dataRoot = _ftpService.GetVdtFtpRoot(sMaDonVi, SIpAddress, sFolderRoot);
                    if (dataRoot == null)
                    {
                        dataRoot = new VdtFtpRoot()
                        {
                            SMaDonVi = sMaDonVi,
                            SIpAddress = SIpAddress, // vd: ftp:\\10.60.108.246
                            SFolderRoot = sFolderRoot,
                            SNguoiTao = _sessionService.Current.Principal,
                            DNgayTao = DateTime.Now
                        };
                        _ftpService.Add(dataRoot);
                    }
                    var result = _ftpStorageService.UploadCommand(dataRoot.Id, filePathLocal, strActiveFileName, strUrl);
                    if (result != 1)
                    {
                        StringBuilder messageBuilder = new StringBuilder();
                        messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
                        MessageBox.Show(messageBuilder.ToString());
                        return;
                    }
                    else
                    {
                        StringBuilder messageBuilder = new StringBuilder();
                        messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                        MessageBox.Show(messageBuilder.ToString());
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.SUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.SUserCreate), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!SelectedItem.BActive)
                    {
                        MessageBox.Show(string.Format(Resources.VoucherLockModified, SelectedItem.SUserCreate), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            try
            {
                _vdtKhvKeHoach5NamService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnImport()
        {
            try
            {
                PlanManagerApprovedImportModel.Init();
                PlanManagerApprovedImportModel.SavedAction = obj =>
                {
                    PlanManagerApprovedImport.Close();
                    this.OnRefresh();
                };
                PlanManagerApprovedImport = new PlanManagerApprovedImport { DataContext = PlanManagerApprovedImportModel };
                PlanManagerApprovedImport.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenReport(object param)
        {
            try
            {
                var mediumTermPlanPrintType = (ReportMediumType)((int)param);
                PrintReportMediumTermPlanViewModel.ReportMediumTypes = mediumTermPlanPrintType;
                PrintReportMediumTermPlanViewModel.Init();
                var view = new PrintReportMediumTermPlan
                {
                    DataContext = PrintReportMediumTermPlanViewModel
                };

                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
