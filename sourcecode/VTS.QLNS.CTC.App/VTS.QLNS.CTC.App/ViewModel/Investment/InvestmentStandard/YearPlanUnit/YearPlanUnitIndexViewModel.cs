using AutoMapper;
using MaterialDesignThemes.Wpf;
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
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.PrintReport;
using log4net;
using FlexCel.Core;
using System.IO;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi
{
    public class YearPlanUnitIndexViewModel : GridViewModelBase<PhanBoVonDonViModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private readonly ISysAuditLogService _log;
        private readonly IExportService _exportService;
        private ICollectionView _phanBoVonView;
        private IMapper _mapper;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IDanhMucService _danhMucService;
        private readonly IVdtFtpRootService _ftpService;

        private static Dictionary<int, string> _dicNguonVon;
        private static Dictionary<string, Guid?> _dicParentDonVi;
        private static Dictionary<Guid, DonVi> _dicDonVi;
        private readonly ILog _logger;
        private List<PhanBoVonDonViModel> _lstItemVoucher = new List<PhanBoVonDonViModel>();

        #region Set current data
        public bool IsAggregate => Items.Any(x => x.Selected);
        public bool IsEdit => SelectedItem != null && SelectedItem.BActive && !SelectedItem.BKhoa;
        public bool IsAdjust => SelectedItem != null && SelectedItem.Id != Guid.Empty;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsEnableLock => SelectedItem != null;
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;

        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_VON_NAM_DON_VI_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN_OF_YEAR;
        //public override string Name => "Quản lý đề xuất nhu cầu lập kế hoạch vốn năm";
        public override string Name => "Kế hoạch vốn năm đề xuất";
        public override string Description => "Danh sách kế hoạch vốn năm đề xuất";
        public override Type ContentType => typeof(VonNamDonViIndex);

        #region Item

        private string _sSoKeHoach;
        public string SSoKeHoach
        {
            get => _sSoKeHoach;
            set => SetProperty(ref _sSoKeHoach, value);
        }

        private string _iNamKeHoach;
        public string iNamKeHoach
        {
            get => _iNamKeHoach;
            set => SetProperty(ref _iNamKeHoach, value);
        }
        private ObservableCollection<FileFtpModel> _lstFile;
        public ObservableCollection<FileFtpModel> LstFile
        {
            get => _lstFile;
            set => SetProperty(ref _lstFile, value);
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
        #endregion

        #region RelayCommand
        public RelayCommand FixDataCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        public RelayCommand UploadFileCommand { get; }


        #endregion

        #region View
        public YearPlanUnitDialogViewModel VonNamDonViDialogViewModel { get; set; }
        public YearPlanUnitDetailViewModel VonNamDonViDetailViewModel { get; set; }
        public YearPlanUnitImportViewModel VonNamDonViImportViewModel { get; set; }
        public YearPlanUnitReportViewModel VonNamDonViReportViewModel { get; set; }
        public VonNamDonViImport VonNamDonViImport { get; set; }
        #endregion

        public YearPlanUnitIndexViewModel(ISessionService sessionService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonVonService,
            IExportService exportService,
            IMapper mapper,
            ISysAuditLogService log,
            ILog logger,
            IVdtDmDonViThucHienDuAnService dmDonViThucHienDuAnService,
            YearPlanUnitDialogViewModel vonNamDonViDialogViewModel,
            YearPlanUnitDetailViewModel vonNamDonViDetailViewModel,
            YearPlanUnitImportViewModel vonNamDonViImportViewModel,
            YearPlanUnitReportViewModel vonNamDonViReportViewModel,
            FtpStorageService ftpStorageService,
            IDanhMucService danhMucService,
            IVdtFtpRootService ftpService
            )
        {
            _logger = logger;
            _sessionService = sessionService;
            _phanBoVonService = phanBoVonService;
            _nsDonViService = nsDonViService;
            _nsNguonVonService = nsNguonVonService;
            _danhMucService = danhMucService;
            _vdtDmDonViThucHienDuAnService = dmDonViThucHienDuAnService;
            _ftpStorageService = ftpStorageService;
            _ftpService = ftpService;
            _exportService = exportService;
            _mapper = mapper;
            _log = log;

            VonNamDonViDialogViewModel = vonNamDonViDialogViewModel;
            VonNamDonViDialogViewModel.ParentPage = this;
            VonNamDonViDetailViewModel = vonNamDonViDetailViewModel;
            VonNamDonViDetailViewModel.ParentPage = this;
            VonNamDonViImportViewModel = vonNamDonViImportViewModel;
            VonNamDonViImportViewModel.ParentPage = this;
            VonNamDonViReportViewModel = vonNamDonViReportViewModel;
            VonNamDonViReportViewModel.ParentPage = this;

            FixDataCommand = new RelayCommand(obj => OnFixData());
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            PrintActionCommand = new RelayCommand(obj => OpenPrintDialog());
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
        }

        public override void Init()
        {
            try
            {
                _tabIndex = VoucherTabIndex.VOUCHER;
                
                LoadDonVi();
                LoadNguonVon();
                LoadData();
                OnResetFilter();
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
                List<VdtKhvPhanBoVonDonViQuery> listChungTu = _phanBoVonService.GetDataPhanBoVonDonViIndexView().ToList();

                _lstItemVoucher = _mapper.Map<List<PhanBoVonDonViModel>>(listChungTu);

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
                List<PhanBoVonDonViModel> lstItemDvViaUser = new List<PhanBoVonDonViModel>();
                List<PhanBoVonDonViModel> lstItemParentViaUser = new List<PhanBoVonDonViModel>();
                _lstItemVoucher.Select(item =>
                {
                    if (lstDv.Contains(item.iID_MaDonViQuanLy))
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

                List<PhanBoVonDonViModel> lstDataAgregate = new List<PhanBoVonDonViModel>();

                List<PhanBoVonDonViModel> lstVoucherAgregateParent = _lstItemVoucher.Where(x => !string.IsNullOrEmpty(x.STongHop)).ToList();
                Dictionary<string, List<string>> dctVoucher = lstVoucherAgregateParent.GroupBy(x => x.Id).ToDictionary(x => x.Key.ToString(), x => x.Select(y => y.STongHop).ToList());

                foreach (var key in dctVoucher.Keys)
                {
                    List<string> lstValue = dctVoucher[key].ToList();
                    List<PhanBoVonDonViModel> lstChildrent = _lstItemVoucher.Where(x => lstValue[0].Split(",").ToList().Contains(x.Id.ToString())).ToList();
                    List<PhanBoVonDonViModel> lstParent = _lstItemVoucher.Where(x => x.Id.ToString() == key).ToList();
                    lstParent.Select(x => { x.IsHangCha = true; x.IsTongHop = true; return x; }).ToList();
                    lstChildrent.Select(x => { x.IsTongHop = true; return x; }).ToList();
                    lstDataAgregate.AddRange(lstParent);
                    lstDataAgregate.AddRange(lstChildrent);
                }

                if (_tabIndex.Equals(VoucherTabIndex.VOUCHER_AGREGATE))
                {
                    lstDataAgregate.Select((item, index) => { item.iRowIndex = (index + 1); return item; }).ToList();
                    Items = new ObservableCollection<PhanBoVonDonViModel>(lstDataAgregate);
                }
                else
                {
                    var lstVoucher = _lstItemVoucher.Where(x => !lstDataAgregate.Select(x => x.Id.ToString()).ToList().Contains(x.Id.ToString())).ToList();
                    lstVoucher.Select((item, index) => { item.iRowIndex = (index + 1); item.IsTongHop = false; return item; }).ToList();
                    Items = new ObservableCollection<PhanBoVonDonViModel>(lstVoucher);
                }

                _phanBoVonView = CollectionViewSource.GetDefaultView(Items);
                _phanBoVonView.Filter = VdtKhPhanBoVonFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault(t => VdtKhPhanBoVonFilter(t));
                }

                Items.Select(x => { x.PropertyChanged += ItemsModel_PropertyChanged; return x; }).ToList();
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
                if (args.PropertyName == nameof(PhanBoVonDonViModel.Selected))
                {
                    OnPropertyChanged(nameof(IsAggregate));
                }
            }
            catch(Exception ex)
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
        private static void SelectAll(bool select, IEnumerable<PhanBoVonDonViModel> models)
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
            VonNamDonViDialogViewModel.Model = new PhanBoVonDonViModel();
            VonNamDonViDialogViewModel.Model.iID_MaDonViQuanLy = _sessionService.Current.IdDonVi;
            VonNamDonViDialogViewModel.Model.sTenDonVi = _sessionService.Current.TenDonVi;
            var itemDonViQuanLy = _nsDonViService.FindByIdDonVi(VonNamDonViDialogViewModel.Model.iID_MaDonViQuanLy, _sessionService.Current.YearOfWork);
            if (itemDonViQuanLy != null)
            {
                VonNamDonViDialogViewModel.Model.iID_DonViQuanLyID = itemDonViQuanLy.Id;
            }
            VonNamDonViDialogViewModel.IsDieuChinh = false;
            VonNamDonViDialogViewModel.IsAggregate = false;
            VonNamDonViDialogViewModel.Init();
            VonNamDonViDialogViewModel.DNgayQuyetDinh = null;
            VonNamDonViDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                PhanBoVonDonViModel item = _mapper.Map<PhanBoVonDonViModel>(obj);
                item.IsViewDetail = false;
                OnOpenVonNamDonViDetail(item);
            };
            VonNamDonViDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            try
            {
                if(SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _phanBoVonService.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherUpdateKHVNWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    VonNamDonViDialogViewModel.IsAggregate = true;
                    List<string> lstChungTu = SelectedItem.STongHop.Split(",").ToList();
                    List<PhanBoVonDonViModel> lstAgregate = _lstItemVoucher.Where(x => lstChungTu.Contains(x.Id.ToString())).ToList();
                    VonNamDonViDialogViewModel.LstVoucherAgregate = lstAgregate;
                }
                else
                {
                    VonNamDonViDialogViewModel.IsAggregate = false;
                }

                VonNamDonViDialogViewModel.Model = SelectedItem;
                VonNamDonViDialogViewModel.Model.IsEdit = true;
                VonNamDonViDialogViewModel.IsDieuChinh = false;
                VonNamDonViDialogViewModel.Init();
                VonNamDonViDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    var dataEdit = _mapper.Map<PhanBoVonDonViModel>(obj);
                    dataEdit.IsEdit = true;
                    dataEdit.IsViewDetail = false;
                    OnOpenVonNamDonViDetail(dataEdit);
                };
                VonNamDonViDialogViewModel.ClosedAction = obj =>
                {
                    OnRefresh();
                };
                VonNamDonViDialogViewModel.ShowDialogHost();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void OnFixData()
        {
            try
            {
                if(SelectedItem != null && SelectedItem.Id != null)
                {
                    var entity = _phanBoVonService.FindById(SelectedItem.Id);
                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherModifiedKHVNWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }

                if (SelectedItem != null && !string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    MessageBox.Show(Resources.VoucherModifiedAction, Resources.Alert);
                    return;
                }
                VonNamDonViDialogViewModel.Model = SelectedItem;
                VonNamDonViDialogViewModel.Model.IsEdit = false;              
                VonNamDonViDialogViewModel.IsDieuChinh = true;
                VonNamDonViDialogViewModel.IsAggregate = false;
                VonNamDonViDialogViewModel.Model.IsAdjust = true;
                VonNamDonViDialogViewModel.Init();
                VonNamDonViDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    PhanBoVonDonViModel item = _mapper.Map<PhanBoVonDonViModel>(obj);
                    item.IsViewDetail = false;
                    OnOpenVonNamDonViDetail(item);
                };
                VonNamDonViDialogViewModel.ClosedAction = obj =>
                {
                    OnRefresh();
                };
                VonNamDonViDialogViewModel.Model.sSoQuyetDinh = null;
                VonNamDonViDialogViewModel.DNgayQuyetDinh=null;
                VonNamDonViDialogViewModel.ShowDialogHost();
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            _phanBoVonService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
            SelectedItem.BKhoa = !SelectedItem.BKhoa;

            LoadData();
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
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
                if (Items.Where(x => x.Selected).Any(x => !x.BKhoa))
                {
                    MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // kiểm tra cùng nam kế hoạch
                if (Items.Where(x => x.Selected).GroupBy(x => x.iNamKeHoach).Count() > 1)
                {
                    MessageBox.Show(Resources.VoucherYear, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Kiểm tra cùng nguồn vốn
                if (Items.Where(x => x.Selected).GroupBy(x => x.iId_NguonVonId).Count() > 1)
                {
                    MessageBox.Show(Resources.VoucherBudget, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Kiểm tra trùng lặp
                if (Items.Where(x => x.Selected).Any(x => x.IsTongHop))
                {
                    MessageBox.Show(Resources.VoucherAgregateExisted, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                ////kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
                var sTongHop = string.Join(",", Items.Where(x => x.IsSelected).Select(x => x.Id.ToString()).ToList());
                var aggregateVoucher = _phanBoVonService.FindAggregateVoucher(sTongHop);
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
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateAggregateVoucher(VdtKhvPhanBoVonDonVi voucherAgrateOld)
        {
            try
            {
                if (voucherAgrateOld != null)
                {
                    List<VdtKhvPhanBoVonDonViChiTiet> lstDetail = _phanBoVonService.GetPhanBoVonDonViByIdPhanBoVon(voucherAgrateOld.Id).ToList();
                    _phanBoVonService.DeletePhanBoVonDonVi(voucherAgrateOld);
                }

                List<PhanBoVonDonViModel> lstChoose = Items.Where(x => x.Selected).ToList();

                PhanBoVonDonViModel model = new PhanBoVonDonViModel();
                model.BKhoa = false;
                model.STongHop = string.Join(",", lstChoose.Select(x => x.Id).ToList());
                model.sTenDonVi = _sessionService.Current.TenDonVi;
                model.iID_MaDonViQuanLy = _sessionService.Current.IdDonVi;
                model.iNamKeHoach = lstChoose.FirstOrDefault().iNamKeHoach;
                model.iId_NguonVonId = lstChoose.FirstOrDefault().iId_NguonVonId;
                model.BActive = true;

                var itemDonViQuanLy = _nsDonViService.FindByIdDonVi(model.iID_MaDonViQuanLy, _sessionService.Current.YearOfWork);
                if (itemDonViQuanLy != null)
                {
                    model.iID_DonViQuanLyID = itemDonViQuanLy.Id;
                }

                VonNamDonViDialogViewModel.Model = model;
                VonNamDonViDialogViewModel.LstVoucherAgregate = lstChoose;
                VonNamDonViDialogViewModel.IsAggregate = true;
                VonNamDonViDialogViewModel.IsDieuChinh = false;
                VonNamDonViDialogViewModel.Init();
                VonNamDonViDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenVonNamDonViDetail((PhanBoVonDonViModel)obj);
                };

                VonNamDonViDialogViewModel.ShowDialogHost();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (!SelectedItem.BActive || SelectedItem.BKhoa) return;
                if (SelectedItem != null)
                {
                    var entity = _phanBoVonService.FindById(SelectedItem.Id);

                    if (entity != null && !string.IsNullOrEmpty(entity.SUserCreate) && !entity.SUserCreate.ToLower().Equals(_sessionService.Current.Principal.ToLower()))
                    {
                        MessageBox.Show(string.Format(Resources.VoucherDeleteKHVNWarning, entity.SUserCreate), Resources.Alert);
                        return;
                    }
                }
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

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsAdjust));
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            try
            {
                base.OnSelectionDoubleClick(obj);

                if (obj is PhanBoVonDonViModel item)
                {
                    item.IsEdit = IsEdit;
                    item.IsViewDetail = true;
                    OnOpenVonNamDonViDetail(item);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
            SSoKeHoach = string.Empty;

            OnSearch();
        }

        private void OnExportExcel()
        {
            try
            {
                List<PhanBoVonDonViModel> listExport = Items.Where(x => x.Selected).ToList();
                if (listExport.GroupBy(x => x.iNamKeHoach).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn bản ghi cùng năm kế hoạch");
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
                Items.Where(x => x.Selected).Select(x => x.iID_MaDonViQuanLy).Select(item =>
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
                    MessageBox.Show(Resources.VoucherKHVNExportWarning);
                    return;
                }

                if (listExport != null && listExport.Count == 0)
                {
                    MessageBox.Show(Resources.MsgRecordEmpty);
                    return;
                }

                int iNamKeHoach = listExport.FirstOrDefault().iNamKeHoach;
                List<ExportVonNamDonViQuery> lstData = _phanBoVonService.GetKeHoachVonNamDonViExport(listExport.Select(n => n.Id).ToList()).ToList();
                var data = lstData.GroupBy(n => new { n.iID_NguonVonID, n.iID_MaDonViQuanLy, n.IdChungTu })                
                    .Select(n => new ExportVonNamDonViModel()
                    {
                        iID_NguonVonID = n.Key.iID_NguonVonID,
                        iID_MaDonViQuanLy = n.Key.iID_MaDonViQuanLy,
                        Datas = n.ToList()
                    });
                if(data != null && data.Count() == 0)
                {
                    MessageBox.Show(Resources.VoucherDataEmpty);
                    return;
                }

                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> lstResult = new List<ExportResult>();

                    foreach (var item in data)
                    {
                        ExportResult result = ExportData(item, iNamKeHoach, ExportType.EXCEL);
                        if (result != null)
                        {
                            lstResult.Add(result);
                        }
                    }
                    e.Result = lstResult;
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
        private void OnUpload()
        {
            try
            {
                List<PhanBoVonDonViModel> listExport = Items.Where(x => x.Selected).ToList();
                if (listExport.GroupBy(x => x.iNamKeHoach).Count() > 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn bản ghi cùng năm kế hoạch");
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
                Items.Where(x => x.Selected).Select(x => x.iID_MaDonViQuanLy).Select(item =>
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
                    MessageBox.Show(Resources.VoucherKHVNExportWarning);
                    return;
                }

                if (listExport != null && listExport.Count == 0)
                {
                    MessageBox.Show(Resources.MsgRecordEmpty);
                    return;
                }

                int iNamKeHoach = listExport.FirstOrDefault().iNamKeHoach;
                List<ExportVonNamDonViQuery> lstData = _phanBoVonService.GetKeHoachVonNamDonViExport(listExport.Select(n => n.Id).ToList()).ToList();
                var data = lstData.GroupBy(n => new { n.iID_NguonVonID, n.iID_MaDonViQuanLy })
                    .Select(n => new ExportVonNamDonViModel()
                    {
                        iID_NguonVonID = n.Key.iID_NguonVonID,
                        iID_MaDonViQuanLy = n.Key.iID_MaDonViQuanLy,
                        Datas = n.ToList()
                    });
                if (data != null && data.Count() == 0)
                {
                    MessageBox.Show(Resources.VoucherDataEmpty);
                    return;
                }
                IsLoading = true;
                List<ExportResult> lstResult = new List<ExportResult>();
                string filePathLocal = string.Empty;
                foreach (var item in data)
                {
                    ExportDataServerFtp(item, iNamKeHoach, ExportType.EXCEL);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private ExportResult ExportData(ExportVonNamDonViModel item, int iNamKeHoach, ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<PhanBoVonDonViChiTietModel> itemData = _mapper.Map<List<PhanBoVonDonViChiTietModel>>(item.Datas);
                itemData = itemData.Select((x, index) => { x.STT = (index + 1).ToString(); return x; }).ToList();

                data.Add("sTenNguonVon", string.Join("", _dicNguonVon[item.iID_NguonVonID].Split(' ').Select(n => n[0].ToString().ToUpper())));
                data.Add("iNamLamViec", iNamKeHoach);
                data.Add("iNamTruoc", iNamKeHoach - 2);
                data.Add("iNamHienTai", iNamKeHoach - 1);
                data.Add("Items", itemData);
                data.Add("dv", _vdtDmDonViThucHienDuAnService.GetDonViThucHienDuAnExport().ToList());

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DONVI);
                string fileNamePrefix = YearPlanManagerType.OUTPUT_KH_NAM_DONVI;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<PhanBoVonDonViChiTietModel, NSDonViThucHienDuAnExportQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }
        private void ExportDataServerFtp(ExportVonNamDonViModel item, int iNamKeHoach, ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<PhanBoVonDonViChiTietModel> itemData = _mapper.Map<List<PhanBoVonDonViChiTietModel>>(item.Datas);
                itemData = itemData.Select((x, index) => { x.STT = (index + 1).ToString(); return x; }).ToList();
                var objdv = _nsDonViService.FindByMaDonViAndNamLamViec(item.iID_MaDonViQuanLy, iNamKeHoach);
                data.Add("sTenNguonVon", string.Join("", _dicNguonVon[item.iID_NguonVonID].Split(' ').Select(n => n[0].ToString().ToUpper())));
                data.Add("iNamLamViec", iNamKeHoach);
                data.Add("iNamTruoc", iNamKeHoach - 2);
                data.Add("iNamHienTai", iNamKeHoach - 1);
                data.Add("Items", itemData);
                data.Add("dv", _vdtDmDonViThucHienDuAnService.GetDonViThucHienDuAnExport().ToList());
                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVN, YearPlanManagerType.RPT_KH_NAM_DONVI);
                string fileNamePrefix = YearPlanManagerType.OUTPUT_KH_NAM_DONVI;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                // code
                var xlsFile = _exportService.Export<PhanBoVonDonViChiTietModel, NSDonViThucHienDuAnExportQuery>(templateFileName, data);
                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

                string filePathLocal = string.Empty;
                _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                string sStage = string.Empty;
                if (SelectedItem != null)
                {
                    sStage = iNamKeHoach.ToString();
                }
                string sFolderRoot = ConstantUrlPathPhanHe.UrlKhvndxWinformReceive;
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
        private bool VdtKhPhanBoVonFilter(object obj)
        {
            try
            {
                if (!(obj is PhanBoVonDonViModel temp)) return true;
                var bCondition = true;
                int iNamKeHoachParse;
                if (!string.IsNullOrEmpty(iNamKeHoach) && int.TryParse(iNamKeHoach, out iNamKeHoachParse))
                {
                    bCondition &= (temp.iNamKeHoach == iNamKeHoachParse);
                }
                if (DNgayQuyetDinhFrom.HasValue)
                {
                    bCondition &= (temp.dNgayQuyetDinh.HasValue && temp.dNgayQuyetDinh == DNgayQuyetDinhFrom);
                }
                if (DrpDonViQuanLySelected != null)
                {
                    bCondition &= (!string.IsNullOrEmpty(temp.iID_MaDonViQuanLy) && temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
                }
                if (DrpNguonVonSelected != null)
                {
                    bCondition &= (temp.iId_NguonVonId == int.Parse(DrpNguonVonSelected.ValueItem));
                }
                if (!string.IsNullOrEmpty(SSoKeHoach))
                {
                    bCondition &= !string.IsNullOrEmpty(temp.sSoQuyetDinh) && (temp.sSoQuyetDinh.ToLower().Contains(SSoKeHoach.ToLower()));
                }
                return bCondition;
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void OnOpenVonNamDonViDetail(PhanBoVonDonViModel SelectedItem)
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
                VonNamDonViDetailViewModel.Model = SelectedItem;
                VonNamDonViDetailViewModel.Init();
                var view = new VonNamDonViDetail { DataContext = VonNamDonViDetailViewModel };
                view.ShowDialog();
                LoadData();
            }
            catch(Exception ex)
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
            VonNamDonViReportViewModel.Init();
            VonNamDonViReportViewModel.ShowDialogHost();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            try
            {
                DateTime dStartDate = DateTime.Now;
                //string sError = string.Empty;
                if (result != NSDialogResult.Yes) return;
                _phanBoVonService.DeletePhanBoVonDonVi(_mapper.Map<VdtKhvPhanBoVonDonVi>(SelectedItem));
                _log.WriteLog(Resources.ApplicationName, Name, (int)TypeExecute.Delete, dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                LoadData();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnImportData()
        {
            try
            {
                VonNamDonViImportViewModel.SavedAction = obj =>
                {
                    VonNamDonViImport.Close();
                    OnRefresh();
                };
                VonNamDonViImportViewModel.Init();
                //
                VonNamDonViImport = new VonNamDonViImport { DataContext = VonNamDonViImportViewModel };

                VonNamDonViImport.ShowDialog();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonVi()
        {
            try
            {
                _dicDonVi = new Dictionary<Guid, DonVi>();
                _dicParentDonVi = new Dictionary<string, Guid?>();
                List<DonVi> lstDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork).ToList();
                _dicDonVi = lstDonVi.ToDictionary(n => n.Id, n => n);
                foreach (var item in lstDonVi)
                {
                    if (!_dicParentDonVi.ContainsKey(item.IIDMaDonVi))
                    {
                        _dicParentDonVi.Add(item.IIDMaDonVi, item.IdParent);
                    }
                }

                _drpDonViQuanLy = _mapper.Map<ObservableCollection<ComboboxItem>>(lstDonVi);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadNguonVon()
        {
            try
            {
                _dicNguonVon = new Dictionary<int, string>();
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                    .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _dicNguonVon = cbxNguonVonData.ToDictionary(n => int.Parse(n.ValueItem), n => n.DisplayItem);
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
