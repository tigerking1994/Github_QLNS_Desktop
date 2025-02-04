using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PrintReport;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using System.IO;
using FlexCel.Core;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Net;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions
{
    public class PlanSuggestionsIndexViewModel : GridViewModelBase<VdtKhvKeHoach5NamDeXuatModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _vdtKhvKeHoach5NamDeXuat;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private readonly INsDonViService _nsDonViService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly IAttachmentService _attachService;
        private readonly FtpStorageService _ftpStorageService;
        private ICollectionView _vdtKhvKeHoach5NamView;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;

        private List<VdtKhvKeHoach5NamDeXuatModel> _lstItemVoucher = new List<VdtKhvKeHoach5NamDeXuatModel>();

        public override string FuncCode => NSFunctionCode.INVESTMENT_MEDIUM_TERM_PLAN_PLAN_SUGGESTION_INDEX;
        public override string GroupName => MenuItemContants.GROUP_MID_TERM_PLAN;
        public override string Name => "Kế hoạch trung hạn đề xuất";
        public override string Description => "Kế hoạch trung hạn đề xuất";
        public override Type ContentType => typeof(PlanSuggestionsIndex);
        public override PackIconKind IconKind => PackIconKind.FileDocument;

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery> _itemsReport;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatReportQuery> ItemsReport
        {
            get => _itemsReport;
            set => SetProperty(ref _itemsReport, value);
        }

        private VdtKhvKeHoach5NamDeXuatReportQuery _rptSummary = new VdtKhvKeHoach5NamDeXuatReportQuery();
        //public PlanSuggestionsDownloadFileDialogViewModel PlanSuggestionsDownloadFileDialogViewModel { get; }
        public VdtKhvKeHoach5NamDeXuatReportQuery RptSummary
        {
            get => _rptSummary;
            set => SetProperty(ref _rptSummary, value);
        }

        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsAggregate => Items.Any(x => x.IsSelected);
        public bool IsEdit => SelectedItem != null && SelectedItem.BActive && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEnableLock => SelectedItem != null;

        private string _searchSoKeHoachText;
        public string SearchSoKeHoachText
        {
            get => _searchSoKeHoachText;
            set => SetProperty(ref _searchSoKeHoachText, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
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
                    OnPropertyChanged(nameof(IsEnableButtonDataShow));
                }
            }
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

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _projectTypeItems;
        public ObservableCollection<ComboboxItem> ProjectTypeItems
        {
            get => _projectTypeItems;
            set => SetProperty(ref _projectTypeItems, value);
        }

        private ComboboxItem _selectedProjectType;
        public ComboboxItem SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                SetProperty(ref _selectedProjectType, value);
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }
        }

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadData();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
        }

        #region RelayCommand
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand FixDataCommand { get; set; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ExportKHTHDXCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }
        public RelayCommand DownloadFileCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        #endregion RelayCommand

        public PrintReportMediumTermPlanViewModel PrintReportMediumTermPlanViewModel { get; set; }
        public PlanSuggestionsDialogViewModel PlanSuggestionsDialogViewModel { get; set; }
        public PlanSuggestionsDetailViewModel PlanSuggestionsDetailViewModel { get; set; }
        public PlanSuggestionsImportViewModel PlanSuggestionsImportViewModel { get; set; }
        public PlanSuggestionsImport PlanSuggestionsImport { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public PlanSuggestionsIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuat,
            IExportService exportService,
            IVdtDaDuAnService vdtDaDuAnService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuat,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IAttachmentService attachService,
            PlanSuggestionsDialogViewModel planSuggestionsDialogViewModel,
            PlanSuggestionsDetailViewModel planSuggestionsDetailViewModel,
            PlanSuggestionsImportViewModel planSuggestionsImportViewModel,
            PrintReportMediumTermPlanViewModel printReportMediumTermPlanViewModel,
            //PlanSuggestionsDownloadFileDialogViewModel planSuggestionsDownloadFileDialogViewModel,
            FtpStorageService ftpStorageService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            AttachmentViewModel attachmentViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _vdtKhvKeHoach5NamDeXuat = vdtKhvKeHoach5NamDeXuat;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuat;
            _vdtDmDonViThucHienDuAnService = vdtDmDonViThucHienDuAnService;
            _exportService = exportService;
            _attachService = attachService;
            _ftpStorageService = ftpStorageService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;

            PlanSuggestionsDialogViewModel = planSuggestionsDialogViewModel;
            PlanSuggestionsDetailViewModel = planSuggestionsDetailViewModel;
            PlanSuggestionsImportViewModel = planSuggestionsImportViewModel;
            PrintReportMediumTermPlanViewModel = printReportMediumTermPlanViewModel;
            //PlanSuggestionsDownloadFileDialogViewModel = planSuggestionsDownloadFileDialogViewModel;
            AttachmentViewModel = attachmentViewModel;

            SearchCommand = new RelayCommand(obj => _vdtKhvKeHoach5NamView.Refresh());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            FixDataCommand = new RelayCommand(obj => OnFixData());
            ExportCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ExportKHTHDXCommand = new RelayCommand(obj => OnExportKHTHDX());
            UploadFileCommand = new RelayCommand(obj => OnUpload());

            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport(obj));
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);
            AggregateCommand = new RelayCommand(obj => OnAggregate());
        }

        public override void Init()
        {
            try
            {
                _tabIndex = VoucherTabIndex.VOUCHER;
                LoadProjectType();
                LoadDonViQuanLy();
                LoadData();
                OnResetFilter();
                PlanSuggestionsDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
                OnPropertyChanged(nameof(IsEnableButtonDataShow));
            }
            catch (Exception ex)
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

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            try
            {
                if (SelectedItem != null)
                {
                    VdtKhvKeHoach5NamDeXuatModel item = Items.Where(x => x.Id == SelectedItem.Id).First();
                    item.FGiaTriKeHoach = ((VdtKhvKeHoach5NamDeXuatModel)sender).FGiaTriKeHoach;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            this.OnRefresh();
        }

        private void LoadDonViQuanLy()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                    .Where(n => lstDonViExclude.Contains(n.Loai));

                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
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
                //List<VdtKhvKeHoach5NamDeXuatQuery> lstQuery = _vdtKhvKeHoach5NamDeXuat.FindConditionIndex(yearOfWork).ToList();
                List<VdtKhvKeHoach5NamDeXuatQuery> lstQuery = _vdtKhvKeHoach5NamDeXuat.FindConditionAll().ToList();
                _lstItemVoucher = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatModel>>(lstQuery);

                List<DonVi> lstUnitViaUser = new List<DonVi>();
                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);

                List<VdtKhvKeHoach5NamDeXuatModel> lstItemDvViaUser = new List<VdtKhvKeHoach5NamDeXuatModel>();
                List<VdtKhvKeHoach5NamDeXuatModel> lstItemParentViaUser = new List<VdtKhvKeHoach5NamDeXuatModel>();
                _lstItemVoucher.Select(item =>
                {
                    if (lstDv.Contains(item.IIdMaDonVi))
                    {
                        lstItemDvViaUser.Add(item);
                    }
                    return item;
                }).ToList();

                if (userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    lstItemParentViaUser = _lstItemVoucher.Where(x => x.BKhoa).ToList();
                }

                _lstItemVoucher = lstItemDvViaUser;
                _lstItemVoucher.AddRange(lstItemParentViaUser);
                _lstItemVoucher = _lstItemVoucher.GroupBy(x => x.Id).Select(grp => grp.FirstOrDefault()).ToList();

                List<VdtKhvKeHoach5NamDeXuatModel> lstDataAgregate = new List<VdtKhvKeHoach5NamDeXuatModel>();

                List<VdtKhvKeHoach5NamDeXuatModel> lstVoucherAgregateParent = _lstItemVoucher.Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
                Dictionary<string, List<string>> dctVoucher = lstVoucherAgregateParent.GroupBy(x => x.Id).ToDictionary(x => x.Key.ToString(), x => x.Select(y => y.STongHop).ToList());

                foreach (var key in dctVoucher.Keys)
                {
                    List<string> lstValue = dctVoucher[key].ToList();
                    List<VdtKhvKeHoach5NamDeXuatModel> lstChildrent = _lstItemVoucher.Where(x => lstValue[0].Split(",").ToList().Contains(x.Id.ToString())).ToList();
                    List<VdtKhvKeHoach5NamDeXuatModel> lstParent = _lstItemVoucher.Where(x => x.Id.ToString() == key).ToList();
                    lstParent.Select(x => { x.IsHangCha = true; x.IsTongHop = true; return x; }).ToList();
                    lstChildrent.Select(x => { x.IsTongHop = true; return x; }).ToList();
                    lstDataAgregate.AddRange(lstParent);
                    lstDataAgregate.AddRange(lstChildrent);
                }

                if (_tabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE))
                {
                    Items = new ObservableCollection<VdtKhvKeHoach5NamDeXuatModel>(lstDataAgregate);
                }
                else
                {
                    var lstVoucher = _lstItemVoucher.Where(x => !lstDataAgregate.Select(x => x.Id.ToString()).ToList().Contains(x.Id.ToString())).ToList();
                    lstVoucher.Select(x => { x.IsTongHop = false; return x; }).ToList();
                    Items = new ObservableCollection<VdtKhvKeHoach5NamDeXuatModel>(lstVoucher);
                }

                _vdtKhvKeHoach5NamView = CollectionViewSource.GetDefaultView(Items);
                _vdtKhvKeHoach5NamView.Filter = ListSettlementVoucherFilter;

                if (Items != null && Items.Count > 0)
                    SelectedItem = Items.FirstOrDefault();

                Items.Select(x => { x.PropertyChanged += ItemsModel_PropertyChanged; return x; }).ToList();
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void ItemsModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == nameof(VdtKhvKeHoach5NamDeXuatChiTietModel.IsSelected))
                {
                    OnPropertyChanged(nameof(IsAggregate));
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetFilter()
        {
            try
            {
                SearchSoKeHoachText = string.Empty;
                GiaiDoan = string.Empty;
                DNgayLap = null;
                SelectedDonVi = null;
                SelectedProjectType = null;
                if (_vdtKhvKeHoach5NamView != null) _vdtKhvKeHoach5NamView.Refresh();
            }
            catch (Exception ex)
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
                    AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_KHTH_DEXUAT;
                    AttachmentViewModel.ObjectId = SelectedItem.Id;
                    AttachmentViewModel.Init();
                    AttachmentViewModel.ShowDialogHost();
                }
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
                var item = (VdtKhvKeHoach5NamDeXuatModel)obj;

                if (!string.IsNullOrEmpty(SearchSoKeHoachText))
                {
                    result = result && !string.IsNullOrEmpty(item.SSoQuyetDinh) && item.SSoQuyetDinh.ToLower().Contains(SearchSoKeHoachText.ToLower());
                }

                if (DNgayLap != null)
                {
                    result = result && item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.ToString("yyyy-MM-dd").ToLower().Contains(DNgayLap.Value.ToString("yyyy-MM-dd"));
                }

                if (!string.IsNullOrEmpty(GiaiDoan))
                {
                    result = result && !string.IsNullOrEmpty(item.GiaiDoan) && item.GiaiDoan.ToLower().Contains(GiaiDoan.ToLower());
                }

                if (SelectedDonVi != null)
                {
                    result = result && !string.IsNullOrEmpty(item.IIdMaDonVi) && item.IIdMaDonVi == SelectedDonVi.ValueItem;
                }

                if (SelectedProjectType != null)
                {
                    result = result && item.ILoai != null && item.ILoai == int.Parse(SelectedProjectType.ValueItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem != null && (!SelectedItem.BActive || SelectedItem.BKhoa)) return;
                if(SelectedItem != null)
                {
                    var entity = _vdtKhvKeHoach5NamDeXuat.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                if (SelectedItem != null && SelectedItem.IIdTongHop.HasValue)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDataExisted = _vdtKhvKeHoach5NamChiTietDexuatService.FindListVoucherDetailsModified(SelectedItem.Id).ToList();
                    if (lstDataExisted.Count() > 0)
                    {
                        MessageBox.Show(Resources.VoucherKhthDelete, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    List<string> lstId = new List<string>();
                    if (SelectedItem.STongHop.Contains(","))
                    {
                        lstId = SelectedItem.STongHop.Split(",").ToList();
                    }
                    else
                    {
                        lstId.Add(SelectedItem.STongHop);
                    }

                    bool isDuocDuyet = false;
                    foreach (var item in lstId)
                    {
                        List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDataExisted = _vdtKhvKeHoach5NamChiTietDexuatService.FindListVoucherDetailsModified(Guid.Parse(item)).ToList();
                        if (lstDataExisted.Count() > 0)
                        {
                            isDuocDuyet = true;
                            break;
                        }
                    }

                    if (isDuocDuyet)
                    {
                        MessageBox.Show(Resources.VoucherKhthDelete, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
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

                List<VdtKhvKeHoach5NamDeXuatChiTiet> lstKeHoachChiTiet = _vdtKhvKeHoach5NamChiTietDexuatService.FindByIdKeHoach5Nam(SelectedItem.Id).ToList();
                if (lstKeHoachChiTiet != null && lstKeHoachChiTiet.Count() > 0)
                {
                    foreach (var item in lstKeHoachChiTiet)
                    {
                        if (item.IdParentModified.HasValue)
                        {
                            Guid idReferenceModified = Guid.Empty;
                            if (item.IdReference.HasValue)
                            {
                                idReferenceModified = item.IdReference.Value;
                            }
                            else
                            {
                                idReferenceModified = item.Id;
                            }

                            VdtDaDuAn itemDuAnUpdate = _vdtDaDuAnService.FindByIdDuAnKhthDeXuat(idReferenceModified).FirstOrDefault();
                            VdtKhvKeHoach5NamDeXuatChiTiet itemKhthGoc = _vdtKhvKeHoach5NamChiTietDexuatService.FindById(item.IdParentModified.Value);
                            if (itemDuAnUpdate != null)
                            {
                                if (itemKhthGoc != null && itemKhthGoc.IdReference.HasValue)
                                {
                                    itemDuAnUpdate.IdDuAnKhthDeXuat = itemKhthGoc.IdReference;
                                }
                                else if (itemKhthGoc != null)
                                {
                                    itemDuAnUpdate.IdDuAnKhthDeXuat = itemKhthGoc.Id;
                                }
                                _vdtDaDuAnService.Update(itemDuAnUpdate);
                            }
                        }

                        _vdtKhvKeHoach5NamChiTietDexuatService.Delete(item.Id);
                    }
                }

                if (SelectedItem != null && SelectedItem.IIdParentId.HasValue)
                {
                    VdtKhvKeHoach5NamDeXuat itemGoc = _vdtKhvKeHoach5NamDeXuat.FindById(SelectedItem.IIdParentId.Value);
                    if (itemGoc != null)
                    {
                        itemGoc.BActive = true;
                        _vdtKhvKeHoach5NamDeXuat.Update(itemGoc);
                    }

                    List<VdtKhvKeHoach5NamDeXuatChiTiet> lstGoc = _vdtKhvKeHoach5NamChiTietDexuatService.FindByIdKeHoach5Nam(SelectedItem.IIdParentId.Value).ToList();

                    if (lstGoc != null && lstGoc.Count > 0)
                    {
                        lstGoc.Select(x => { x.BActive = true; return x; }).ToList();
                        foreach (var item in lstGoc)
                        {
                            _vdtKhvKeHoach5NamChiTietDexuatService.Update(item);
                        }
                    }
                }

                _vdtKhvKeHoach5NamDeXuat.Delete(SelectedItem.Id);
                _attachService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int)AttachmentEnum.Type.VDT_KHTH_DEXUAT);

                var itemDeleted = Items.Where(x => x.Id == SelectedItem.Id).First();
                Items.Remove(itemDeleted);
                this.LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsAggregate));
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
                _vdtKhvKeHoach5NamDeXuat.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                LoadData();
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnAggregate()
        {
            try
            {
                //check quyền được tổng hợp
                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.LockAuthotizationWarning, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //kiểm tra trạng thái các bản ghi
                if (Items.Where(x => x.IsSelected).Any(x => !x.BKhoa))
                {
                    MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // kiểm tra cùng giai đoạn
                if (Items.Where(x => x.IsSelected).GroupBy(x => new { x.IGiaiDoanTu, x.IGiaiDoanDen }).Count() > 1)
                {
                    MessageBox.Show(Resources.VoucherPeriod, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // kiểm tra cùng loại dự án
                if (Items.Where(x => x.IsSelected).GroupBy(x => x.ILoai).Count() > 1)
                {
                    MessageBox.Show(Resources.VoucherProjectType, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Kiểm tra trùng lặp
                if (Items.Where(x => x.IsSelected).Any(x => x.IsTongHop))
                {
                    MessageBox.Show(Resources.VoucherAgregateExisted, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var sTongHop = string.Join(",", Items.Where(x => x.IsSelected).Select(x => x.Id.ToString()).ToList());
                //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa

                var aggregateVoucher = _vdtKhvKeHoach5NamDeXuat.FindAggregateVoucher(sTongHop);
                if (aggregateVoucher != null)
                {
                    MessageBoxResult result = MessageBox.Show(Resources.AlertExistAggregateVoucher, Resources.NotifiTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            CreateAggregateVoucher(aggregateVoucher);
                            break;
                        case MessageBoxResult.No:
                            return;
                    }
                }
                else CreateAggregateVoucher(aggregateVoucher);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateAggregateVoucher(VdtKhvKeHoach5NamDeXuat voucherAgrateOld)
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatModel> lstChoose = Items.Where(x => x.IsSelected).ToList();

                VdtKhvKeHoach5NamDeXuatModel model = new VdtKhvKeHoach5NamDeXuatModel();
                model.BKhoa = false;
                model.STongHop = string.Join(",", lstChoose.Select(x => x.Id).ToList());
                model.STenDonVi = _sessionService.Current.TenDonVi;
                model.IIdMaDonVi = _sessionService.Current.IdDonVi;
                model.ILoai = Items.Where(x => x.IsSelected).FirstOrDefault().ILoai;
                model.NamLamViec = _sessionService.Current.YearOfWork;
                model.BIsGoc = true;
                model.BActive = true;

                var itemDonViQuanLy = _nsDonViService.FindByIdDonVi(model.IIdMaDonVi, _sessionService.Current.YearOfWork);
                if (itemDonViQuanLy != null)
                {
                    model.IIdDonViId = itemDonViQuanLy.Id;
                }

                PlanSuggestionsDialogViewModel.Model = model;
                PlanSuggestionsDialogViewModel.LstVoucherAgregate = new ObservableCollection<VdtKhvKeHoach5NamDeXuatModel>(lstChoose);
                PlanSuggestionsDialogViewModel.IsAgregate = true;
                PlanSuggestionsDialogViewModel.IsDieuChinh = false;
                PlanSuggestionsDialogViewModel.Init();
                PlanSuggestionsDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenPlanManagerDetail((VdtKhvKeHoach5NamDeXuatModel)obj);
                };

                PlanSuggestionsDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            try
            {
                PlanSuggestionsDialogViewModel.Model = new VdtKhvKeHoach5NamDeXuatModel();
                PlanSuggestionsDialogViewModel.IsDieuChinh = false;
                PlanSuggestionsDialogViewModel.IsAgregate = false;
                PlanSuggestionsDialogViewModel.Init();
                PlanSuggestionsDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamDeXuatModel item = (VdtKhvKeHoach5NamDeXuatModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanManagerDetail(item);
                };

                PlanSuggestionsDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
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
                    var entity = _vdtKhvKeHoach5NamDeXuat.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherUpdateKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    PlanSuggestionsDialogViewModel.IsAgregate = true;
                    List<string> lstChungTu = SelectedItem.STongHop.Split(",").ToList();
                    List<VdtKhvKeHoach5NamDeXuatModel> lstAgregate = _lstItemVoucher.Where(x => lstChungTu.Contains(x.Id.ToString())).ToList();
                    PlanSuggestionsDialogViewModel.LstVoucherAgregate = new ObservableCollection<VdtKhvKeHoach5NamDeXuatModel>(lstAgregate);
                }
                else
                {
                    PlanSuggestionsDialogViewModel.IsAgregate = false;
                }

                PlanSuggestionsDialogViewModel.Model = ObjectCopier.Clone(SelectedItem);
                PlanSuggestionsDialogViewModel.IsDieuChinh = false;
                PlanSuggestionsDialogViewModel.TabIndex = TabIndex;
                PlanSuggestionsDialogViewModel.Init();

                PlanSuggestionsDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamDeXuatModel item = (VdtKhvKeHoach5NamDeXuatModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanManagerDetail(item);
                };
                PlanSuggestionsDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            try
            {
                base.OnSelectionDoubleClick(obj);
                VdtKhvKeHoach5NamDeXuatModel item = (VdtKhvKeHoach5NamDeXuatModel)obj;
                item.IsViewDetail = true;
                OnOpenPlanManagerDetail(item);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnOpenPlanManagerDetail(VdtKhvKeHoach5NamDeXuatModel SelectedItem)
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _vdtKhvKeHoach5NamDeXuat.FindById(SelectedItem.Id);
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDetailPermissionWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
                PlanSuggestionsDetailViewModel.Model = SelectedItem;
                PlanSuggestionsDetailViewModel.Init();
                PlanSuggestionsDetailViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnFixData()
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _vdtKhvKeHoach5NamDeXuat.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherModifiedKHTHWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    MessageBox.Show(Resources.VoucherModifiedAction, Resources.Alert);
                    return;
                }

                //PlanSuggestionsDialogViewModel.Model = SelectedItem;
                PlanSuggestionsDialogViewModel.Model = ObjectCopier.Clone(SelectedItem);
                PlanSuggestionsDialogViewModel.IsDieuChinh = true;
                PlanSuggestionsDialogViewModel.IsAgregate = false;
                PlanSuggestionsDialogViewModel.Init();
                PlanSuggestionsDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    VdtKhvKeHoach5NamDeXuatModel item = (VdtKhvKeHoach5NamDeXuatModel)obj;
                    item.IsViewDetail = false;
                    OnOpenPlanManagerDetail(item);
                };
                PlanSuggestionsDialogViewModel.Model.SSoQuyetDinh = null;
                PlanSuggestionsDialogViewModel.Model.DNgayQuyetDinh = null;
                PlanSuggestionsDialogViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            this.LoadData();
            OnPropertyChanged(nameof(Items));
        }

        private static void SelectAll(bool select, IEnumerable<VdtKhvKeHoach5NamDeXuatModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                if (!Items.Any(n => n.IsSelected) || Items.Where(n => n.IsSelected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }

                StringBuilder sError = new StringBuilder();
                Items.Where(x => x.IsSelected).Select(x => x.IIdMaDonVi).Select(item =>
                {
                    if (!lstDv.Contains(item))
                    {
                        sError.AppendLine(item);
                    }
                    return item;
                }).ToList();

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string sTenDonVi = string.Empty;
                    VdtKhvKeHoach5NamDeXuatModel item = Items.FirstOrDefault(n => n.IsSelected);
                    if (item.IIdDonViId != Guid.Empty)
                    {
                        var itemDonVi = _nsDonViService.FindById(item.IIdDonViId);
                        if (itemDonVi != null)
                        {
                            sTenDonVi = itemDonVi.TenDonVi;
                        }
                    }

                    List<VdtKhvKeHoach5NamDeXuatExportQuery> lstData = _vdtKhvKeHoach5NamChiTietDexuatService.GetDataExportKeHoachTrungHanDeXuat(item.Id).ToList();
                    RptSummary.FHanMucDauTu = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FHanMucDauTu);
                    RptSummary.FTongSoNhuCau = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FTongSoNhuCau);
                    RptSummary.FGiaTriNamThuNhat = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNhat);
                    RptSummary.FGiaTriNamThuHai = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuHai);
                    RptSummary.FGiaTriNamThuBa = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuBa);
                    RptSummary.FGiaTriNamThuTu = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuTu);
                    RptSummary.FGiaTriNamThuNam = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNam);
                    RptSummary.FGiaTriBoTri = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriBoTri);
                    RptSummary.FTongSo = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FTongSo);

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("Period", string.Format("{0}-{1}", item.IGiaiDoanTu, item.IGiaiDoanDen));
                    data.Add("TenDonVi", !string.IsNullOrEmpty(sTenDonVi) ? sTenDonVi.ToUpper() : string.Empty);
                    data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                    data.Add("Year1", item.IGiaiDoanTu);
                    data.Add("Year2", item.IGiaiDoanTu + 1);
                    data.Add("Year3", item.IGiaiDoanTu + 2);
                    data.Add("Year4", item.IGiaiDoanTu + 3);
                    data.Add("Year5", item.IGiaiDoanTu + 4);
                    data.Add("Items", lstData);
                    data.Add("FHanMucDauTuSum", RptSummary.FHanMucDauTu);
                    data.Add("FTongSoNhuCauSum", RptSummary.FTongSoNhuCau);
                    data.Add("FTongSoSum", RptSummary.FTongSo);
                    data.Add("FGiaTriNamThuNhatSum", RptSummary.FGiaTriNamThuNhat);
                    data.Add("FGiaTriNamThuHaiSum", RptSummary.FGiaTriNamThuHai);
                    data.Add("FGiaTriNamThuBaSum", RptSummary.FGiaTriNamThuBa);
                    data.Add("FGiaTriNamThuTuSum", RptSummary.FGiaTriNamThuTu);
                    data.Add("FGiaTriNamThuNamSum", RptSummary.FGiaTriNamThuNam);
                    data.Add("FGiaTriBoTriSum", RptSummary.FGiaTriBoTri);
                    data.Add("dv", _vdtDmDonViThucHienDuAnService.GetDonViThucHienDuAnExport().ToList());

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                    string fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                    string fileNamePrefix = string.Format("{0}_{1}-{2}_{3}", fileName, item.IGiaiDoanTu, item.IGiaiDoanDen,
                        item.BIsGoc.Value ? "Goc" : "DieuChinh");
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatExportQuery, NSDonViThucHienDuAnExportQuery>(templateFileName, data);
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

        private void OnExportKHTHDX()
        {
            try
            {
                List<VdtKhvKeHoach5NamDeXuatModel> lstItem = Items.Where(x => x.IsSelected).ToList();

                if (!Items.Any(n => n.IsSelected))
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat(Resources.VoucherExportEmpty);
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }

                StringBuilder sError = new StringBuilder();
                Items.Where(x => x.IsSelected).Select(x => x.IIdMaDonVi).Select(item =>
                {
                    if(!lstDv.Contains(item))
                    {
                        sError.AppendLine(item);
                    }
                    return item;
                }).ToList();

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }

                try
                {
                    BackgroundWorkerHelper.Run((s, e) =>
                    {
                        IsLoading = true;

                        List<ExportResult> results = new List<ExportResult>();
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.EXPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                        string fileName;
                        string fileNamePrefix;
                        string fileNameWithoutExtension;
                        FormatNumber formatNumber = new FormatNumber(Int32.Parse(DonViTinh.NGHIN_DONG_VALUE), ExportType.EXCEL);

                        List<VdtKhvKeHoach5NamDeXuatModel> lstItemGoc = lstItem.Where(x => x.BIsGoc.Value).ToList();
                        List<VdtKhvKeHoach5NamDeXuatModel> lstItemModified = lstItem.Where(x => !x.BIsGoc.Value).ToList();
                        //Dictionary<string, List<VdtKhvKeHoach5NamDeXuatModel>> dctDataExport = lstItemGoc.GroupBy(x => new { x.IIdMaDonVi, x.NamLamViec, x.IGiaiDoanTu, x.IGiaiDoanDen }).ToDictionary(x =>
                        //    string.Join(",", lstItemGoc.Where(y => y.IIdMaDonVi == x.Key.IIdMaDonVi && y.NamLamViec == x.Key.NamLamViec
                        //        && y.IGiaiDoanTu == x.Key.IGiaiDoanTu && y.IGiaiDoanDen == x.Key.IGiaiDoanDen)
                        //            .Select(x => x.Id.ToString())), x => x.ToList());
                        Dictionary<string, List<VdtKhvKeHoach5NamDeXuatModel>> dctDataExport = lstItemGoc.GroupBy(x => new { x.IIdMaDonVi, x.NamLamViec, x.IGiaiDoanTu, x.IGiaiDoanDen, x.SSoQuyetDinh }).ToDictionary(x =>
                            string.Join(",", lstItemGoc.Where(y => y.IIdMaDonVi == x.Key.IIdMaDonVi && y.NamLamViec == x.Key.NamLamViec
                                && y.IGiaiDoanTu == x.Key.IGiaiDoanTu && y.IGiaiDoanDen == x.Key.IGiaiDoanDen && y.SSoQuyetDinh == x.Key.SSoQuyetDinh)
                                    .Select(x => x.Id.ToString())), x => x.ToList());

                        var dctDataModified = lstItemModified.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.ToList());
                        dctDataModified.Select(obj =>
                        {
                            dctDataExport.Add(string.Join(",", obj.Key.ToString()), obj.Value);
                            return obj;
                        }).ToList();

                        foreach (var item in dctDataExport.Keys)
                        {
                            List<VdtKhvKeHoach5NamDeXuatExportQuery> lstDataCTMM = new List<VdtKhvKeHoach5NamDeXuatExportQuery>();
                            List<VdtKhvKeHoach5NamDeXuatExportQuery> lstDataCTCT = new List<VdtKhvKeHoach5NamDeXuatExportQuery>();
                            List<string> lstId = item.Split(",").ToList();
                            foreach (var item1 in lstId)
                            {
                                List<VdtKhvKeHoach5NamDeXuatExportQuery> lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.GetDataExportKeHoachTrungHanDeXuat(Guid.Parse(item1)).ToList();
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
                            VdtKhvKeHoach5NamDeXuatReportQuery RptSummaryCtmm = new VdtKhvKeHoach5NamDeXuatReportQuery();
                            VdtKhvKeHoach5NamDeXuatReportQuery RptSummaryCtct = new VdtKhvKeHoach5NamDeXuatReportQuery();

                            int iGiaiDoanTu = dctDataExport[item].FirstOrDefault().IGiaiDoanTu;
                            int iGiaiDoanDen = dctDataExport[item].FirstOrDefault().IGiaiDoanDen;
                            string sTenDonVi = _nsDonViService.FindByIdDonVi(dctDataExport[item].FirstOrDefault().IIdMaDonVi, _sessionService.Current.YearOfWork).TenDonVi;

                            RptSummaryCtmm.FHanMucDauTu = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FHanMucDauTu);
                            RptSummaryCtmm.FTongSoNhuCau = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FHanMucDauTu);
                            RptSummaryCtmm.FTongSo = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FTongSo);
                            RptSummaryCtmm.FGiaTriNamThuNhat = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNhat);
                            RptSummaryCtmm.FGiaTriNamThuHai = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuHai);
                            RptSummaryCtmm.FGiaTriNamThuBa = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuBa);
                            RptSummaryCtmm.FGiaTriNamThuTu = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuTu);
                            RptSummaryCtmm.FGiaTriNamThuNam = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNam);
                            RptSummaryCtmm.FGiaTriBoTri = lstDataCTMM.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriBoTri);

                            RptSummaryCtct.FHanMucDauTu = lstDataCTCT.Sum(x => x.FHanMucDauTu);
                            RptSummaryCtct.FTongSoNhuCau = lstDataCTCT.Sum(x => x.FHanMucDauTu);
                            RptSummaryCtct.FTongSo = lstDataCTCT.Sum(x => x.FTongSo);
                            RptSummaryCtct.FGiaTriNamThuNhat = lstDataCTCT.Sum(x => x.FGiaTriNamThuNhat);
                            RptSummaryCtct.FGiaTriNamThuHai = lstDataCTCT.Sum(x => x.FGiaTriNamThuHai);
                            RptSummaryCtct.FGiaTriNamThuBa = lstDataCTCT.Sum(x => x.FGiaTriNamThuBa);
                            RptSummaryCtct.FGiaTriNamThuTu = lstDataCTCT.Sum(x => x.FGiaTriNamThuTu);
                            RptSummaryCtct.FGiaTriNamThuNam = lstDataCTCT.Sum(x => x.FGiaTriNamThuNam);
                            RptSummaryCtct.FGiaTriBoTri = lstDataCTCT.Sum(x => x.FGiaTriBoTri);

                            data.Add("Period", string.Format("{0}-{1}", iGiaiDoanTu, iGiaiDoanDen));
                            data.Add("Year1", iGiaiDoanTu);
                            data.Add("Year2", (iGiaiDoanTu + 1).ToString());
                            data.Add("Year3", (iGiaiDoanTu + 2).ToString());
                            data.Add("Year4", (iGiaiDoanTu + 3).ToString());
                            data.Add("Year5", (iGiaiDoanTu + 4).ToString());

                            data.Add("FHanMucDauTuCtmmSum", RptSummaryCtmm.FHanMucDauTu);
                            data.Add("FTongSoNhuCauCtmmSum", RptSummaryCtmm.FTongSoNhuCau);
                            data.Add("FTongSoCtmmSum", RptSummaryCtmm.FTongSo);
                            data.Add("FGiaTriNamThuNhatCtmmSum", RptSummaryCtmm.FGiaTriNamThuNhat);
                            data.Add("FGiaTriNamThuHaiCtmmSum", RptSummaryCtmm.FGiaTriNamThuHai);
                            data.Add("FGiaTriNamThuBaCtmmSum", RptSummaryCtmm.FGiaTriNamThuBa);
                            data.Add("FGiaTriNamThuTuCtmmSum", RptSummaryCtmm.FGiaTriNamThuTu);
                            data.Add("FGiaTriNamThuNamCtmmSum", RptSummaryCtmm.FGiaTriNamThuNam);
                            data.Add("FGiaTriBoTriCtmmSum", RptSummaryCtmm.FGiaTriBoTri);
                            data.Add("FHanMucDauTuCtctSum", RptSummaryCtct.FHanMucDauTu);
                            data.Add("FTongSoNhuCauCtctSum", RptSummaryCtct.FTongSoNhuCau);
                            data.Add("FTongSoCtctSum", RptSummaryCtct.FTongSo);
                            data.Add("FGiaTriNamThuNhatCtctSum", RptSummaryCtct.FGiaTriNamThuNhat);
                            data.Add("FGiaTriNamThuHaiCtctSum", RptSummaryCtct.FGiaTriNamThuHai);
                            data.Add("FGiaTriNamThuBaCtctSum", RptSummaryCtct.FGiaTriNamThuBa);
                            data.Add("FGiaTriNamThuTuCtctSum", RptSummaryCtct.FGiaTriNamThuTu);
                            data.Add("FGiaTriNamThuNamCtctSum", RptSummaryCtct.FGiaTriNamThuNam);
                            data.Add("FGiaTriBoTriCtctSum", RptSummaryCtct.FGiaTriBoTri);

                            data.Add("dv", _nsDonViService.GetDonViExportByNamLamViec(_sessionService.Current.YearOfWork).ToList());
                            data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                            data.Add("TenDonVi", !string.IsNullOrEmpty(sTenDonVi) ? sTenDonVi.ToUpper() : string.Empty);
                            data.Add("ItemsCtmm", lstDataCTMM);
                            data.Add("ItemsCtct", lstDataCTCT);
                            data.Add("FormatNumber", formatNumber);

                            fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.OUTPUT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                            fileNamePrefix = string.Format("{0}_{1}_{2}_{3}",
                                string.Format("{0}-{1}", dctDataExport[item].FirstOrDefault().IIdMaDonVi, sTenDonVi),
                                string.Format("{0}-{1}", iGiaiDoanTu, iGiaiDoanDen),
                                ((dctDataExport[item].FirstOrDefault().BIsGoc.Value) ? "Goc" : "DieuChinh"), fileName);
                            fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatExportQuery, NSDonViExportQuery>(templateFileName, data);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                        }
                        e.Result = results;
                    }, (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            var result = (List<ExportResult>)e.Result;
                            if (result != null && result.Count() == 1)
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
        #region UploadFileCommand_byminh
        private void OnUpload()
        {
            try
            {
                if (!Items.Any(n => n.IsSelected) || Items.Where(n => n.IsSelected).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
                List<string> lstDv = new List<string>();
                if (lstUnitManager.Contains(","))
                {
                    lstDv = lstUnitManager.Split(",").ToList();
                }
                else
                {
                    lstDv.Add(lstUnitManager);
                }

                StringBuilder sError = new StringBuilder();
                Items.Where(x => x.IsSelected).Select(x => x.IIdMaDonVi).Select(item =>
                {
                    if (!lstDv.Contains(item))
                    {
                        sError.AppendLine(item);
                    }
                    return item;
                }).ToList();

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && sError.Length != 0)
                {
                    MessageBox.Show(Resources.VoucherKHTHExportWarning);
                    return;
                }

                string sTenDonVi = string.Empty;
                string sMaDonVi = string.Empty;
                string sStage = string.Empty;

                if (SelectedItem != null)
                {
                    sStage = SelectedItem.GiaiDoan;
                }
                VdtKhvKeHoach5NamDeXuatModel item = Items.FirstOrDefault(n => n.IsSelected);
                if (item.IIdDonViId != Guid.Empty)
                {
                    var itemDonVi = _nsDonViService.FindById(item.IIdDonViId);
                    if (itemDonVi != null)
                    {
                        sMaDonVi = itemDonVi.IIDMaDonVi;
                    }
                }
                List<VdtKhvKeHoach5NamDeXuatExportQuery> lstData = _vdtKhvKeHoach5NamChiTietDexuatService.GetDataExportKeHoachTrungHanDeXuat(item.Id).ToList();
                RptSummary.FHanMucDauTu = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FHanMucDauTu);
                RptSummary.FTongSoNhuCau = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FTongSoNhuCau);
                RptSummary.FGiaTriNamThuNhat = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNhat);
                RptSummary.FGiaTriNamThuHai = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuHai);
                RptSummary.FGiaTriNamThuBa = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuBa);
                RptSummary.FGiaTriNamThuTu = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuTu);
                RptSummary.FGiaTriNamThuNam = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriNamThuNam);
                RptSummary.FGiaTriBoTri = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriBoTri);
                RptSummary.FTongSo = lstData.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FTongSo);

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Period", string.Format("{0}-{1}", item.IGiaiDoanTu, item.IGiaiDoanDen));
                data.Add("TenDonVi", !string.IsNullOrEmpty(sTenDonVi) ? sTenDonVi.ToUpper() : string.Empty);
                data.Add("iNamLamViec", _sessionService.Current.YearOfWork);
                data.Add("Year1", item.IGiaiDoanTu);
                data.Add("Year2", item.IGiaiDoanTu + 1);
                data.Add("Year3", item.IGiaiDoanTu + 2);
                data.Add("Year4", item.IGiaiDoanTu + 3);
                data.Add("Year5", item.IGiaiDoanTu + 4);
                data.Add("Items", lstData);
                data.Add("FHanMucDauTuSum", RptSummary.FHanMucDauTu);
                data.Add("FTongSoNhuCauSum", RptSummary.FTongSoNhuCau);
                data.Add("FTongSoSum", RptSummary.FTongSo);
                data.Add("FGiaTriNamThuNhatSum", RptSummary.FGiaTriNamThuNhat);
                data.Add("FGiaTriNamThuHaiSum", RptSummary.FGiaTriNamThuHai);
                data.Add("FGiaTriNamThuBaSum", RptSummary.FGiaTriNamThuBa);
                data.Add("FGiaTriNamThuTuSum", RptSummary.FGiaTriNamThuTu);
                data.Add("FGiaTriNamThuNamSum", RptSummary.FGiaTriNamThuNam);
                data.Add("FGiaTriBoTriSum", RptSummary.FGiaTriBoTri);
                data.Add("ChucDanh1", "");
                data.Add("ChucDanh2", "");
                data.Add("ChucDanh3", "");
                data.Add("ThuaLenh1", "");
                data.Add("ThuaLenh2", "");
                data.Add("ThuaLenh3", "");
                data.Add("Ten1", "");
                data.Add("Ten2", "");
                data.Add("Ten3", "");
                data.Add("dv", _vdtDmDonViThucHienDuAnService.GetDonViThucHienDuAnExport().ToList());

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHTH, MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                string fileName = Path.GetFileNameWithoutExtension(MediumTermPlanType.IMPORT_KE_HOACH_TRUNG_HAN_DE_XUAT_TEMPLATE);
                string fileNamePrefix = string.Format("{0}_{1}-{2}_{3}", fileName, item.IGiaiDoanTu, item.IGiaiDoanDen,
                    item.BIsGoc.Value ? "Goc" : "DieuChinh");
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<VdtKhvKeHoach5NamDeXuatExportQuery, NSDonViThucHienDuAnExportQuery>(templateFileName, data);
                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                //code download file
                string filePathLocal = string.Empty;
                _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);

                string sFolderRoot = ConstantUrlPathPhanHe.UrlKhthdxWinformReceive;
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
                    // Lấy ra 1 bản ghi trong bảng VDT_FtpRoot để kiểm tra xem đã tồn tại hay chưa.
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
        
        #endregion
        private void OnImportData()
        {
            try
            {
                PlanSuggestionsImportViewModel.SavedAction = obj =>
                {
                    PlanSuggestionsImport.Close();
                    OnRefresh();
                };
                PlanSuggestionsImportViewModel.Init();
                PlanSuggestionsImport = new PlanSuggestionsImport { DataContext = PlanSuggestionsImportViewModel };
                PlanSuggestionsImport.ShowDialog();
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
