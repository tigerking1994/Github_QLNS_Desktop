    using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.PrintReport;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.YearPlan;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlanImport;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanManagerApproved;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using FlexCel.Core;
using System.IO;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan
{
    public class YearPlanIndexViewModel : GridViewModelBase<PhanBoVonModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonService _phanBoVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly ISysAuditLogService _log;
        private readonly IExportService _exportService;
        private readonly IVdtKhvPhanBoVonChiTietService _phanBoVonChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;

        private readonly ILog _logger;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;

        #region Set current data
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty && SelectedItem.BActive;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_YEAR_PLAN_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN_OF_YEAR;
        public override string Name => "Dự toán được giao";
        public override string Description => "Danh sách dự toán được giao";
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.YearPlan.YearPlanIndex);

        #region Item
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEnableLock => SelectedItem != null;

        private string _sSoQuyetDinh;
        public string SSoQuyetDinh
        {
            get => _sSoQuyetDinh;
            set => SetProperty(ref _sSoQuyetDinh, value);
        }

        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }

        private DateTime? _dNgayQuyetDinhFrom;
        public DateTime? DNgayQuyetDinhFrom
        {
            get => _dNgayQuyetDinhFrom;
            set
            {
                SetProperty(ref _dNgayQuyetDinhFrom, value);
                OnSearch();
            }
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
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set
            {
                SetProperty(ref _drpNguonVonSelected, value);
                OnSearch();
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

        #endregion

        #region RelayCommand
        public RelayCommand FixDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand UploadFileCommand { get; set; }

        public RelayCommand PrintActionCommand { get; }
        #endregion

        #region View
        public YearPlanDialogViewModel YearPlanDialogViewModel { get; set; }
        public YearPlanDetailViewModel YearPlanDetailViewModel { get; set; }
        public YearPlanImportViewModel YearPlanImportViewModel { get; set; }
        public PrintReportYearPlanViewModel PrintReportYearPlanViewModel { get; set; }
        #endregion

        public YearPlanIndexViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonService phanBoVonService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            ITongHopNguonNSDauTuService tonghopService,
            IMapper mapper,
            IExportService exportService,
            ISysAuditLogService log,
            IVdtKhvPhanBoVonChiTietService vdtKhvPhanBoVonChiTietService,
            ILog logger,
            YearPlanDialogViewModel yearPlanDialogViewModel,
            YearPlanDetailViewModel yearPlanDetailViewModel,
            YearPlanImportViewModel yearPlanImportViewModel,
            FtpStorageService ftpStorageService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            PrintReportYearPlanViewModel printReportYearPlanViewModel)
        {
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _tonghopService = tonghopService;
            _nsDonViService = nsDonViService;
            _phanBoVonChiTietService = vdtKhvPhanBoVonChiTietService;
            _ftpStorageService = ftpStorageService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _mapper = mapper;
            _log = log;
            _exportService = exportService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _logger = logger;

            YearPlanDialogViewModel = yearPlanDialogViewModel;
            YearPlanDialogViewModel.ParentPage = this;
            YearPlanDetailViewModel = yearPlanDetailViewModel;
            YearPlanDetailViewModel.ParentPage = this;
            YearPlanImportViewModel = yearPlanImportViewModel;
            YearPlanImportViewModel.ParentPage = this;
            PrintReportYearPlanViewModel = printReportYearPlanViewModel;
            PrintReportYearPlanViewModel.ParentPage = this;

            FixDataCommand = new RelayCommand(obj => OnFixData());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportCommand = new RelayCommand(obj => OnExportExcel(ExportType.EXCEL));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
        }

        public override void Init()
        {
            try
            {
                MarginRequirement = new System.Windows.Thickness(10);
                GetDonViQuanLy();
                GetNguonVon();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetNguonVon()
        {
            List<NsNguonNganSach> lstNguonNganSach = _nsNguonNganSachService.FindNguonNganSach().ToList();
            _drpNguonVon = _mapper.Map<ObservableCollection<ComboboxItem>>(lstNguonNganSach);
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                List<PhanBoVonQuery> listChungTu = _phanBoVonService.GetDataPhanBoVonInIndexView((int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet).ToList();
                var lstItem = _mapper.Map<List<PhanBoVonModel>>(listChungTu);

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    lstItem = lstItem.Where(x => x.BKhoa).ToList();
                }

                lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
                Items = _mapper.Map<ObservableCollection<PhanBoVonModel>>(lstItem);
                _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(PhanBoVonModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        #region Relay Command

        // <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<PhanBoVonModel> models)
        {
            foreach (var model in models)
            {
                model.Selected = select;
            }
        }
        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd()
        {
            YearPlanDialogViewModel.Model = new PhanBoVonModel();
            YearPlanDialogViewModel.Init();
            YearPlanDialogViewModel.Model.dNgayQuyetDinh = null;
            YearPlanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                PhanBoVonModel item = _mapper.Map<PhanBoVonModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new YearPlanDialog
            {
                DataContext = YearPlanDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null && SelectedItem.Id != null)
            {
                var entity = _phanBoVonService.FindById(SelectedItem.Id);
                if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                {
                    MessageBox.Show(string.Format(Resources.VoucherUpdateKHVNWarning, entity.SUserCreate), Resources.Alert);
                    return;
                }
            }

            YearPlanDialogViewModel.Model = SelectedItem;
            YearPlanDialogViewModel.Model.IsEdit = IsEdit;
            YearPlanDialogViewModel.Model.IsAdjust = false;
            YearPlanDialogViewModel.Init();
            YearPlanDialogViewModel.ClosedAction = obj =>
            {
                OnRefresh();
            };
            YearPlanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                PhanBoVonModel item = _mapper.Map<PhanBoVonModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new YearPlanDialog
            {
                DataContext = YearPlanDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        public void OnFixData()
        {
            if (SelectedItem != null && SelectedItem.Id != null)
            {
                var entity = _phanBoVonService.FindById(SelectedItem.Id);
                if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                {
                    MessageBox.Show(string.Format(Resources.VoucherModifiedKHVNWarning, entity.SUserCreate), Resources.Alert);
                    return;
                }
            }

            YearPlanDialogViewModel.Model = SelectedItem;
            YearPlanDialogViewModel.Model.IsEdit = false;
            YearPlanDialogViewModel.Model.IsAdjust = IsEdit;
            YearPlanDialogViewModel.Init();
            YearPlanDialogViewModel.ClosedAction = obj =>
            {
                OnRefresh();
            };
            YearPlanDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                PhanBoVonModel item = _mapper.Map<PhanBoVonModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new YearPlanDialog
            {
                DataContext = YearPlanDialogViewModel
            };
            YearPlanDialogViewModel.Model.sSoQuyetDinh = null;
            YearPlanDialogViewModel.Model.dNgayQuyetDinh = null;
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            try
            {
                if (!SelectedItem.BActive) return;
                if (SelectedItem != null)
                {
                    var entity = _phanBoVonService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHVNWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
                var iExistInDeNghiThanhToan = _deNghiThanhToanService.FindByCondition(x => x.IIdPhanBoVonID == SelectedItem.Id).ToList();
                if (iExistInDeNghiThanhToan.Count > 0)
                {
                    MessageBox.Show(string.Format(Resources.MsgDeleteDuToanDuocGiao), Resources.Alert);
                    return;
                }

                base.OnDelete();
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.sSoQuyetDinh, SelectedItem.dNgayQuyetDinh.HasValue ? DateTimeExtension.ToStringDate(SelectedItem.dNgayQuyetDinh.Value) : string.Empty);
                var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
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
                    if (SelectedItem.sUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBox.Show(string.Format(Resources.MsgRoleLock, SelectedItem.sUserCreate), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!SelectedItem.BActive)
                    {
                        MessageBox.Show(string.Format(Resources.VoucherLockModified, SelectedItem.sUserCreate), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
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
            _phanBoVonService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
            SelectedItem.BKhoa = !SelectedItem.BKhoa;

            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (PhanBoVonModel)obj;
            data.IsViewDetail = true;
            if (SelectedItem.iSoLanDieuChinh != 0)
            {
                data.IsEdit = IsEdit;
                data.IsAdjust = IsEdit;
            }
            else
            {
                data.IsEdit = IsEdit;
                data.IsAdjust = false;
            }
            OnOpenYearPlanDetail(data, true);
        }

        public void OnSearch()
        {
            _phanBoVonView.Refresh();
        }

        private void OnResetFilter()
        {
            iNamKeHoach = null;
            DNgayQuyetDinhFrom = null;
            DrpDonViQuanLySelected = null;
            DrpNguonVonSelected = null;
            SSoQuyetDinh = string.Empty;

            OnPropertyChanged(nameof(iNamKeHoach));
            OnPropertyChanged(nameof(DNgayQuyetDinhFrom));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(DrpNguonVonSelected));
            OnSearch();
        }
        #endregion

        private void OnExportExcel(ExportType exportType)
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1 || (Items != null && Items.Count.Equals(0)))
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.VoucherKHVNExportWarning);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    PhanBoVonModel item = Items.Where(x => x.Selected).FirstOrDefault();
                    List<PhanBoVonChiTietQuery> lstQuery = _phanBoVonChiTietService.GetPhanBoVonChiTietByParentId(item.Id);
                    List<YearPlanManagerExportModel> items = _mapper.Map<List<YearPlanManagerExportModel>>(lstQuery);
                    items = items.Select((x, index) => { x.STT = (index + 1).ToString(); return x; }).ToList();

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("NamKeHoach", item.iNamKeHoach.ToString());
                    data.Add("Items", items);

                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<YearPlanManagerExportModel, YearPlanManagerExportModel>(templateFileName, data);
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
        private void OnUpload()
        {
            try
            {
                if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1 || (Items != null && Items.Count.Equals(0)))
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.VoucherKHVNExportWarning);
                    return;
                }
                IsLoading = true;

                PhanBoVonModel item = Items.Where(x => x.Selected).FirstOrDefault();
                List<PhanBoVonChiTietQuery> lstQuery = _phanBoVonChiTietService.GetPhanBoVonChiTietByParentId(item.Id);
                List<YearPlanManagerExportModel> items = _mapper.Map<List<YearPlanManagerExportModel>>(lstQuery);
                items = items.Select((x, index) => { x.STT = (index + 1).ToString(); return x; }).ToList();

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("NamKeHoach", item.iNamKeHoach.ToString());
                data.Add("Items", items);
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
                string fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<YearPlanManagerExportModel, YearPlanManagerExportModel>(templateFileName, data);
                
                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                string filePathLocal = string.Empty;
                string sStage = string.Empty;
                if (SelectedItem != null)
                {
                    sStage = item.iNamKeHoach.ToString();
                }
                _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);

                var objdv = _nsDonViService.FindByMaDonViAndNamLamViec(item.iID_MaDonViQuanLy, Convert.ToInt32(item.iNamKeHoach));
                string sFolderRoot = ConstantUrlPathPhanHe.UrlKhnddWinformReceive;
                var strUrl = string.Format("{0}/{1}/{2}", objdv.IIDMaDonVi, sFolderRoot, sStage);

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
                    dataRoot = _ftpService.GetVdtFtpRoot(objdv.IIDMaDonVi, SIpAddress, sFolderRoot);
                    if (dataRoot == null)
                    {
                        dataRoot = new VdtFtpRoot()
                        {
                            SMaDonVi = objdv.IIDMaDonVi,
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

        #region Helper
        private bool VdtKhPhanBoVonFilter(object obj)
        {
            try
            {
                if (!(obj is PhanBoVonModel temp)) return true;
                var bCondition = true;
                int iNamKeHoachParse = 0;
                if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
                {
                    bCondition &= (temp.iNamKeHoach.HasValue && temp.iNamKeHoach.Value == iNamKeHoachParse);
                }
                if (DNgayQuyetDinhFrom.HasValue)
                {
                    bCondition &= (temp.dNgayQuyetDinh.HasValue && temp.dNgayQuyetDinh.ToShortDateString() == DNgayQuyetDinhFrom.Value.ToShortDateString());
                }
                if (DrpDonViQuanLySelected != null)
                {
                    bCondition &= !string.IsNullOrEmpty(temp.iID_MaDonViQuanLy) && (temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
                }
                if (_drpNguonVonSelected != null)
                {
                    bCondition &= (temp.iId_NguonVonId == Int32.Parse(_drpNguonVonSelected.ValueItem));
                }
                if (!string.IsNullOrEmpty(_sSoQuyetDinh))
                {
                    bCondition &= !string.IsNullOrEmpty(temp.sSoQuyetDinh) && (temp.sSoQuyetDinh.ToLower().Contains(_sSoQuyetDinh.ToLower()));
                }
                return bCondition;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void OnOpenYearPlanDetail(PhanBoVonModel SelectedItem, bool bIsDetail = false)
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _phanBoVonService.FindById(SelectedItem.Id);
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDetailPermissionWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
                YearPlanDetailViewModel.BIsDetail = bIsDetail;
                YearPlanDetailViewModel.Model = SelectedItem;
                YearPlanDetailViewModel.Init();
                var view = new YearPlanDetail { DataContext = YearPlanDetailViewModel };
                view.ShowDialog();
                LoadData();
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
                DateTime dStartDate = DateTime.Now;
                string sError = string.Empty;
                if (result != NSDialogResult.Yes) return;
                _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, (int)TypeExecute.Delete, SelectedItem.Id);
                _phanBoVonService.DeletePhanBoVon(_mapper.Map<VdtKhvPhanBoVon>(SelectedItem), _sessionService.Current.Principal, ref sError);
                _log.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Delete, dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetDonViQuanLy()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => lstDonViExclude.Contains(n.Loai)).ToList();

                _drpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(cbxLoaiDonViData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnImportData()
        {
            try
            {
                YearPlanImportViewModel.YearPlanTypes = (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet;
                YearPlanImportViewModel.SavedAction = obj =>
                {
                    OnRefresh();
                };
                YearPlanImportViewModel.Init();
                YearPlanImportViewModel.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog()
        {
            try
            {
                PrintReportYearPlanViewModel.Model = SelectedItem;
                PrintReportYearPlanViewModel.Models = Items;
                PrintReportYearPlanViewModel.Init();
                object content = new View.Investment.InvestmentStandard.PrintReport.YearPlanReport
                {
                    DataContext = PrintReportYearPlanViewModel
                };
                if (content != null)
                {
                    DialogHost.Show(content, DivisionScreen.ROOT_DIALOG, null, null);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
