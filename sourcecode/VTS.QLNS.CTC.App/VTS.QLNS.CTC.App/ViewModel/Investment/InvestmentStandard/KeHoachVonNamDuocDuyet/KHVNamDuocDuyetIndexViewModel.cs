using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.YearPlan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.YearPlanImport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonNamDuocDuyet
{
    public class KHVNamDuocDuyetIndexViewModel : GridViewModelBase<VdtKhvPhanBoVonDonViPheDuyetModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonDonViPheDuyetService _iVdtKhvPhanBoVonDonViPheDuyetService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly ISysAuditLogService _log;
        private readonly IExportService _exportService;
        private readonly IVdtKhvPhanBoVonDonViChiTietPheDuyetService _iVdtKhvPhanBoVonDonViChiTietPheDuyetService;
        private readonly ILog _logger;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;

        #region Set current data
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty && SelectedItem.BActive;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_YEAR_PLAN_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN_OF_YEAR;
        public override string Name => "Kế hoạch vốn năm được duyệt";
        public override string Description => "Danh sách kế hoạch vốn năm được duyệt";
        public override Type ContentType => typeof(KHVNamDuocDuyetIndex);

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

        public RelayCommand PrintActionCommand { get; }
        #endregion

        #region View
        public KHVNamDuocDuyetDialogViewModel KHVNamDuocDuyetDialogViewModel { get; set; }
        public KHVNamDuocDuyetDetailViewModel KHVNamDuocDuyetDetailViewModel { get; set; }
        public YearPlanImportViewModel YearPlanImportViewModel { get; set; }
        public KHVNamDuocDuyetReportViewModel KHVNamDuocDuyetReportViewModel { get; set; }
        #endregion

        public KHVNamDuocDuyetIndexViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonDonViPheDuyetService vdtKhvPhanBoVonDonViPheDuyetService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            ITongHopNguonNSDauTuService tonghopService,
            IVdtDaDuAnService vdtDaDuAnService,
            IMapper mapper,
            IExportService exportService,
            ISysAuditLogService log,
            IVdtKhvPhanBoVonDonViChiTietPheDuyetService vdtKhvPhanBoVonDonViChiTietPheDuyetService,
            ILog logger,
            KHVNamDuocDuyetDialogViewModel khvNamDuocDuyetDialogViewModel,
            KHVNamDuocDuyetDetailViewModel khvNamDuocDuyetDetailViewModel,
            YearPlanImportViewModel yearPlanImportViewModel,
            KHVNamDuocDuyetReportViewModel khvNamDuocDuyetReportViewModel)
        {
            _sessionService = sessionService;
            _iVdtKhvPhanBoVonDonViPheDuyetService = vdtKhvPhanBoVonDonViPheDuyetService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _tonghopService = tonghopService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _iVdtKhvPhanBoVonDonViChiTietPheDuyetService = vdtKhvPhanBoVonDonViChiTietPheDuyetService;
            _mapper = mapper;
            _log = log;
            _exportService = exportService;
            _logger = logger;

            KHVNamDuocDuyetDialogViewModel = khvNamDuocDuyetDialogViewModel;
            KHVNamDuocDuyetDialogViewModel.ParentPage = this;
            KHVNamDuocDuyetDetailViewModel = khvNamDuocDuyetDetailViewModel;
            KHVNamDuocDuyetDetailViewModel.ParentPage = this;
            YearPlanImportViewModel = yearPlanImportViewModel;
            YearPlanImportViewModel.ParentPage = this;
            KHVNamDuocDuyetReportViewModel = khvNamDuocDuyetReportViewModel;
            KHVNamDuocDuyetReportViewModel.ParentPage = this;

            FixDataCommand = new RelayCommand(obj => OnFixData());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportCommand = new RelayCommand(obj => OnExportExcel(ExportType.EXCEL));
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog(obj));
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
                List<PhanBoVonDonViPheDuyetQuery> listChungTu = _iVdtKhvPhanBoVonDonViPheDuyetService.GetDataPhanBoVonInIndexView((int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet).ToList();
                var lstItem = _mapper.Map<List<VdtKhvPhanBoVonDonViPheDuyetModel>>(listChungTu);

                List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    lstItem = lstItem.Where(x => x.BKhoa).ToList();
                }

                lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
                Items = _mapper.Map<ObservableCollection<VdtKhvPhanBoVonDonViPheDuyetModel>>(lstItem);
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
                        if (args.PropertyName == nameof(VdtKhvPhanBoVonDonViPheDuyetModel.Selected))
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
        private static void SelectAll(bool select, IEnumerable<VdtKhvPhanBoVonDonViPheDuyetModel> models)
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
            KHVNamDuocDuyetDialogViewModel.Model = new VdtKhvPhanBoVonDonViPheDuyetModel();
            KHVNamDuocDuyetDialogViewModel.Init();
            KHVNamDuocDuyetDialogViewModel.Model.dNgayQuyetDinh = null;
            KHVNamDuocDuyetDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                VdtKhvPhanBoVonDonViPheDuyetModel item = _mapper.Map<VdtKhvPhanBoVonDonViPheDuyetModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new KHVNamDuocDuyetDialog
            {
                DataContext = KHVNamDuocDuyetDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null && SelectedItem.Id != null)
            {
                var entity = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(SelectedItem.Id);
                if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                {
                    MessageBox.Show(string.Format(Resources.VoucherUpdateKHVNWarning, entity.SUserCreate), Resources.Alert);
                    return;
                }
            }

            KHVNamDuocDuyetDialogViewModel.Model = SelectedItem;
            if (SelectedItem.iSoLanDieuChinh != 0)
            {
                KHVNamDuocDuyetDialogViewModel.Model.IsEdit = IsEdit;
                KHVNamDuocDuyetDialogViewModel.Model.IsAdjust = false;
            }
            else
            {
                KHVNamDuocDuyetDialogViewModel.Model.IsEdit = IsEdit;
                KHVNamDuocDuyetDialogViewModel.Model.IsAdjust = false;
            }
            KHVNamDuocDuyetDialogViewModel.Init();
            KHVNamDuocDuyetDialogViewModel.ClosedAction = obj =>
            {
                OnRefresh();
            };
            KHVNamDuocDuyetDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                VdtKhvPhanBoVonDonViPheDuyetModel item = _mapper.Map<VdtKhvPhanBoVonDonViPheDuyetModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new KHVNamDuocDuyetDialog
            {
                DataContext = KHVNamDuocDuyetDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        public void OnFixData()
        {
            if (SelectedItem != null && SelectedItem.Id != null)
            {
                var entity = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(SelectedItem.Id);
                if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                {
                    MessageBox.Show(string.Format(Resources.VoucherModifiedKHVNWarning, entity.SUserCreate), Resources.Alert);
                    return;
                }
            }

            KHVNamDuocDuyetDialogViewModel.Model = SelectedItem;
            KHVNamDuocDuyetDialogViewModel.Model.IsEdit = false;
            KHVNamDuocDuyetDialogViewModel.Model.IsAdjust = IsEdit;
            KHVNamDuocDuyetDialogViewModel.Init();
            KHVNamDuocDuyetDialogViewModel.ClosedAction = obj =>
            {
                OnRefresh();
            };
            KHVNamDuocDuyetDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                VdtKhvPhanBoVonDonViPheDuyetModel item = _mapper.Map<VdtKhvPhanBoVonDonViPheDuyetModel>(obj);
                item.IsViewDetail = false;
                OnOpenYearPlanDetail(item);
            };
            var view = new KHVNamDuocDuyetDialog
            {
                DataContext = KHVNamDuocDuyetDialogViewModel
            };
            KHVNamDuocDuyetDialogViewModel.Model.sSoQuyetDinh = null;
            KHVNamDuocDuyetDialogViewModel.Model.dNgayQuyetDinh = null;
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            try
            {
                if (!SelectedItem.BActive) return;
                if (SelectedItem != null)
                {
                    var entity = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {

                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHVNWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
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
            _iVdtKhvPhanBoVonDonViPheDuyetService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
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
            var data = (VdtKhvPhanBoVonDonViPheDuyetModel)obj;
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
            OnOpenYearPlanDetail(data);
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
            //try
            //{
            //    if (!Items.Any(n => n.Selected) || Items.Where(n => n.Selected).Count() > 1 || (Items != null && Items.Count.Equals(0)))
            //    {
            //        StringBuilder messageBuilder = new StringBuilder();
            //        messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
            //        MessageBox.Show(messageBuilder.ToString());
            //        return;
            //    }

            //    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            //    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            //    {
            //        MessageBox.Show(Resources.VoucherKHVNExportWarning);
            //        return;
            //    }

            //    BackgroundWorkerHelper.Run((s, e) =>
            //    {
            //        IsLoading = true;

            //        PhanBoVonModel item = Items.Where(x => x.Selected).FirstOrDefault();
            //        List<PhanBoVonChiTietQuery> lstQuery = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetPhanBoVonChiTietByParentId(item.Id);
            //        List<YearPlanManagerExportModel> items = _mapper.Map<List<YearPlanManagerExportModel>>(lstQuery);
            //        items = items.Select((x, index) => { x.STT = (index + 1).ToString(); return x; }).ToList();

            //        Dictionary<string, object> data = new Dictionary<string, object>();
            //        data.Add("NamKeHoach", item.iNamKeHoach.ToString());
            //        data.Add("Items", items);

            //        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
            //        string fileNamePrefix = Path.GetFileNameWithoutExtension(YearPlanManagerType.RPT_KH_NAM_DUOC_DUYET);
            //        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            //        var xlsFile = _exportService.Export<YearPlanManagerExportModel, YearPlanManagerExportModel>(templateFileName, data);
            //        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            //    }, (s, e) =>
            //    {
            //        if (e.Error == null)
            //        {
            //            var result = (ExportResult)e.Result;
            //            if (result != null)
            //            {
            //                _exportService.Open(result, exportType);
            //            }
            //        }
            //        else
            //        {
            //            _logger.Error(e.Error.Message);
            //        }
            //        IsLoading = false;
            //    });
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(ex.Message, ex);
            //}
        }

        #region Helper
        private bool VdtKhPhanBoVonFilter(object obj)
        {
            try
            {
                if (!(obj is VdtKhvPhanBoVonDonViPheDuyetModel temp)) return true;
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

        private void OnOpenYearPlanDetail(VdtKhvPhanBoVonDonViPheDuyetModel SelectedItem, bool bIsDetail = false)
        {
            try
            {
                if (SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _iVdtKhvPhanBoVonDonViPheDuyetService.FindById(SelectedItem.Id);
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT) && entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDetailPermissionWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
                KHVNamDuocDuyetDetailViewModel.Model = SelectedItem;
                KHVNamDuocDuyetDetailViewModel.Init();
                var view = new KHVNamDuocDuyetDetail { DataContext = KHVNamDuocDuyetDetailViewModel };
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
                // cập nhật lại trạng thái dự án
                var lstDuAn = _iVdtKhvPhanBoVonDonViChiTietPheDuyetService.GetPhanBoVonChiTietByParentId(SelectedItem.Id);
                foreach (var item in lstDuAn)
                {
                    var duan = _vdtDaDuAnService.Find(item.iID_DuAnID);
                    if (duan != null)
                    {
                        duan.STrangThaiDuAn = "KhoiTao";
                        _vdtDaDuAnService.Update(duan);
                    }
                }

                _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_NAM, (int)TypeExecute.Delete, SelectedItem.Id);
                _iVdtKhvPhanBoVonDonViPheDuyetService.DeletePhanBoVon(_mapper.Map<VdtKhvPhanBoVonDonViPheDuyet>(SelectedItem), _sessionService.Current.Principal, ref sError);
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
            //try
            //{
            //    YearPlanImportViewModel.YearPlanTypes = (int)LoaiKeHoachNam.KeHoachVonNamDuocDuyet;
            //    YearPlanImportViewModel.SavedAction = obj =>
            //    {
            //        OnRefresh();
            //    };
            //    YearPlanImportViewModel.Init();
            //    YearPlanImportViewModel.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(ex.Message, ex);
            //}
        }

        /// <summary>
        /// open screen print
        /// </summary>
        /// <param name="param"></param>
        private void OpenPrintDialog(object param)
        {
            try
            {
                var printType = (ReportMediumType)((int)param);
                KHVNamDuocDuyetReportViewModel.ReportMediumTypes = printType;
                KHVNamDuocDuyetReportViewModel.Init();
                KHVNamDuocDuyetReportViewModel.ShowDialogHost();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
