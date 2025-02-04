using AutoMapper;
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
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Globalization;
using VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT.PrintDialog;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.QuyetToanVDT.PrintDialog;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.Model.Report;
using System.Net;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.QuyetToanVDT
{
    public class QuyetToanVDTIndexViewModel : GridViewModelBase<VdtQtBcquyetToanNienDoModel>
    {
        #region Public
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_QUYET_TOAN_VDT_INDEX;
        public override string GroupName => MenuItemContants.GROUP_ANNUAL_SETTLEMENT;
        public override string Name => "Báo cáo quyết toán các nguồn vốn đầu tư";
        public override string Description => "Danh sách quyết toán các nguồn vốn đầu tư";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.QuyetToanVDT.QuyetToanVDTIndex);
        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsAggregate => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsEnableLock => SelectedItem != null;
        private List<VdtQtBcquyetToanNienDoModel> _lstDataRoot;
        private Dictionary<Guid, List<VdtQtBcquyetToanNienDoModel>> _dicDataChild;
        private readonly FtpStorageService _ftpStorageService;
        #endregion

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtQtBcQuyetToanNienDoService _service;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private IDmChuKyService _dmChuKyService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private IDanhMucService _danhMucService;
        private ICollectionView _quyetToanNienDoView;
        private readonly IVdtFtpRootService _ftpService;
        private string _cap1;
        private string _diaDiem;
        private DmChuKy _dmChuKy;
        private string _title1;
        private string _title2;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ImportVonNamCommand { get; }
        public RelayCommand ImportVonUngCommand { get; }
        public RelayCommand AggregateCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }
        #endregion

        #region Componer
        private List<ComboboxItem> _units;
        public List<ComboboxItem> Units
        {
            get => _units;
            set => SetProperty(ref _units, value);
        }

        private ComboboxItem _selectedUnit;
        public ComboboxItem SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
                OnSearch();
            }
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

        private DateTime? _dNgayQuyetDinhTo;
        public DateTime? DNgayQuyetDinhTo
        {
            get => _dNgayQuyetDinhTo;
            set
            {
                SetProperty(ref _dNgayQuyetDinhTo, value);
                OnSearch();
            }
        }
        public bool IsEnableButtonDataShow => TabIndex == VoucherTabIndex.VOUCHER;

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadDataByTabIndex();
                OnPropertyChanged(nameof(IsEnableButtonDataShow));

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

        private ObservableCollection<ComboboxItem> _drpNguonNganSach;
        public ObservableCollection<ComboboxItem> DrpNguonNganSach
        {
            get => _drpNguonNganSach;
            set => SetProperty(ref _drpNguonNganSach, value);
        }

        private ComboboxItem _drpNguonNganSachSelected;
        public ComboboxItem DrpNguonNganSachSelected
        {
            get => _drpNguonNganSachSelected;
            set
            {
                SetProperty(ref _drpNguonNganSachSelected, value);
                OnSearch();
            }
        }
        #endregion

        public QuyetToanVDTDialogViewModel QuyetToanVDTDialogViewModel { get; set; }
        public QuyetToanVDTDetailViewModel QuyetToanVDTDetailViewModel { get; set; }
        public QuyetToanVDTVonUngDetailViewModel QuyetToanVDTCQTCDetailViewModel { get; set; }
        public QuyetToanVDTImportViewModel QuyetToanVDTImportViewModel { get; set; }
        public QuyetToanVDTVonUngImportViewModel QuyetToanVDTVonUngImportViewModel { get; set; }
        public QuyetToanVDTPrintReportViewModel QuyetToanVDTPrintReportViewModel { get; set; }
        public QuyetToanVDTPrintDialogViewModel QuyetToanVDTPrintDialogViewModel { get; set; }

        public QuyetToanVDTIndexViewModel(
            QuyetToanVDTDialogViewModel quyetToanVDTDialogViewModel,
            QuyetToanVDTDetailViewModel quyetToanVDTDetailViewModel,
            QuyetToanVDTVonUngDetailViewModel quyetToanVDTCQTCDetailViewModel,
            QuyetToanVDTImportViewModel quyetToanVDTImportViewModel,
            QuyetToanVDTVonUngImportViewModel quyetToanVDTVonUngImportViewModel,
            QuyetToanVDTPrintReportViewModel quyetToanVDTPrintReportViewModel,
            QuyetToanVDTPrintDialogViewModel quyetToanVDTPrintDialogViewModel,
            IVdtQtBcQuyetToanNienDoService service,
            FtpStorageService ftpStorageService,
            INsNguonNganSachService nsNguonNganSachService,
            ISessionService sessionService,
            IExportService exportService,
            ITongHopNguonNSDauTuService tonghopService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            IVdtFtpRootService ftpService,
            IMapper mapper,
            ILog logger)
        {
            QuyetToanVDTDialogViewModel = quyetToanVDTDialogViewModel;
            QuyetToanVDTDialogViewModel.ParentPage = this;
            QuyetToanVDTDetailViewModel = quyetToanVDTDetailViewModel;
            QuyetToanVDTDetailViewModel.ParentPage = this;
            QuyetToanVDTCQTCDetailViewModel = quyetToanVDTCQTCDetailViewModel;
            QuyetToanVDTCQTCDetailViewModel.ParentPage = this;
            QuyetToanVDTImportViewModel = quyetToanVDTImportViewModel;
            QuyetToanVDTImportViewModel.ParentPage = this;
            QuyetToanVDTVonUngImportViewModel = quyetToanVDTVonUngImportViewModel;
            QuyetToanVDTVonUngImportViewModel.ParentPage = this;
            QuyetToanVDTPrintReportViewModel = quyetToanVDTPrintReportViewModel;
            QuyetToanVDTPrintReportViewModel.ParentPage = this;
            QuyetToanVDTPrintDialogViewModel = quyetToanVDTPrintDialogViewModel;
            QuyetToanVDTPrintDialogViewModel.ParentPage = this;

            _service = service;
            _nsNguonNganSachService = nsNguonNganSachService;
            _danhMucService = danhMucService;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
            _exportService = exportService;
            _nsDonViService = nsDonViService;
            _dmChuKyService = dmChuKyService;
            _ftpStorageService = ftpStorageService;

            _ftpService = ftpService;
            _mapper = mapper;
            _logger = logger;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            //PrintReportCommand = new RelayCommand(obj => OnPrintReport());
            ExportCommand = new RelayCommand(obj => OnPrintReport());
            ImportVonNamCommand = new RelayCommand(obj => OnImportVonNamData());
            ImportVonUngCommand = new RelayCommand(obj => OnImportVonUngData());
            AggregateCommand = new RelayCommand(obj => OnAggregate());
            PrintReportCommand = new RelayCommand(obj => OnOpenReport());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _tabIndex = VoucherTabIndex.VOUCHER;
            LoadDanhMuc();
            GetDonViQuanLy();
            GetNguonNganSach();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            _lstDataRoot = new List<VdtQtBcquyetToanNienDoModel>();
            _dicDataChild = new Dictionary<Guid, List<VdtQtBcquyetToanNienDoModel>>();
            var lstItem = _mapper.Map<List<VdtQtBcquyetToanNienDoModel>>(_service.GetDeNghiQuyetToanNienDoIndex());
            ConvertDataDefault(lstItem);
            LoadDataByTabIndex();
        }

        private void ConvertDataDefault(List<VdtQtBcquyetToanNienDoModel> lstChungTu)
        {
            Dictionary<Guid, Guid> dicRefer = new Dictionary<Guid, Guid>();
            foreach (var item in lstChungTu)
            {
                if (dicRefer.ContainsKey(item.Id))
                {
                    item.BIsTongHop = true;
                    if (!_dicDataChild.ContainsKey(dicRefer[item.Id]))
                        _dicDataChild.Add(dicRefer[item.Id], new List<VdtQtBcquyetToanNienDoModel>());
                    _dicDataChild[dicRefer[item.Id]].Add(item);
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.STongHop))
                    {
                        item.BIsTongHop = true;
                        foreach (var iIdChild in item.STongHop.Split(";").Select(n => Guid.Parse(n)))
                            dicRefer.Add(iIdChild, item.Id);
                    }
                    _lstDataRoot.Add(item);
                }
            }
        }

        private void LoadDataByTabIndex()
        {
            if (_lstDataRoot == null) return;
            var lstItem = _lstDataRoot.Where(n => n.BIsTongHop == (TabIndex == VoucherTabIndex.VOUCHER_AGREGATE));
            List<VdtQtBcquyetToanNienDoModel> datas = new List<VdtQtBcquyetToanNienDoModel>();
            int i = 1;
            foreach (var item in lstItem)
            {
                item.IRowIndex = i;
                datas.Add(item);
                if (_dicDataChild.ContainsKey(item.Id))
                {
                    if (item.BIsShowChild)
                        datas.AddRange(_dicDataChild[item.Id]);
                }
                ++i;
            }
            Items = _mapper.Map<ObservableCollection<VdtQtBcquyetToanNienDoModel>>(datas);
            foreach (var item in Items)
                item.PropertyChanged += DetailModel_PropertyChanged;
            _quyetToanNienDoView = CollectionViewSource.GetDefaultView(Items);
            _quyetToanNienDoView.Filter = VdtQtQuyetToanNienDoFilter;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _quyetToanNienDoView.Refresh();
        }

        protected override void OnAdd()
        {
            QuyetToanVDTDialogViewModel.IsInsert = true;
            QuyetToanVDTDialogViewModel.BIsTongHop = false;
            QuyetToanVDTDialogViewModel.Model = new VdtQtBcquyetToanNienDoModel();
            QuyetToanVDTDialogViewModel.Init();
            QuyetToanVDTDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtQtBcquyetToanNienDoModel>(obj));
            };
            var view = new QuyetToanVDTDialog
            {
                DataContext = QuyetToanVDTDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            QuyetToanVDTDialogViewModel.IsInsert = false;
            QuyetToanVDTDialogViewModel.BIsTongHop = !string.IsNullOrEmpty(SelectedItem.STongHop);
            QuyetToanVDTDialogViewModel.Model = SelectedItem;
            if (!string.IsNullOrEmpty(SelectedItem.STongHop))
            {
                QuyetToanVDTDialogViewModel.BIsTongHop = true;
                if (_dicDataChild.ContainsKey(SelectedItem.Id))
                {
                    QuyetToanVDTDialogViewModel.ItemsTongHopQuyetToan = new ObservableCollection<VdtQtBcquyetToanNienDoModel>(_dicDataChild[SelectedItem.Id]);
                }
            }
            QuyetToanVDTDialogViewModel.Init();
            QuyetToanVDTDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtQtBcquyetToanNienDoModel>(obj), QuyetToanVDTDialogViewModel.BIsTongHop);
            };
            var view = new QuyetToanVDTDialog
            {
                DataContext = QuyetToanVDTDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnAddTongHopChungTu()
        {
            var childItem = Items.FirstOrDefault(n => n.IsChecked);
            if (childItem == null)
            {
                MessageBox.Show(Resources.MsgRecordEmpty);
                return;
            }
            QuyetToanVDTDialogViewModel.IsInsert = false;
            QuyetToanVDTDialogViewModel.Model = childItem.Clone();
            QuyetToanVDTDialogViewModel.Model.Id = Guid.Empty;
            QuyetToanVDTDialogViewModel.BIsTongHop = true;
            QuyetToanVDTDialogViewModel.ItemsTongHopQuyetToan = new ObservableCollection<VdtQtBcquyetToanNienDoModel>(Items.Where(n => n.IsChecked));
            QuyetToanVDTDialogViewModel.Init();
            QuyetToanVDTDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtQtBcquyetToanNienDoModel>(obj), false, true);
            };
            var view = new QuyetToanVDTDialog
            {
                DataContext = QuyetToanVDTDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteQuyetToanNienDo);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            INamKeHoach = null;
            DNgayQuyetDinhFrom = null;
            DNgayQuyetDinhTo = null;
            DrpDonViQuanLySelected = null;
            OnPropertyChanged(nameof(INamKeHoach));
            OnPropertyChanged(nameof(DNgayQuyetDinhFrom));
            OnPropertyChanged(nameof(DNgayQuyetDinhTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnSearch();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (VdtQtBcquyetToanNienDoModel)obj;
            if (!string.IsNullOrEmpty(data.STongHop))
                OnOpenDisbursementPaymentDetail(data, true, true);
            else
                OnOpenDisbursementPaymentDetail(data, true);
        }

        private void OnExportExcel()
        {
            if (Items == null || !Items.Any(n => n.IsChecked)) return;
            try
            {
                QuyetToanVDTPrintDialogViewModel.VdtQtBcquyetToanNienDoModels = Items.Where(n => n.IsChecked).ToList();
                QuyetToanVDTPrintDialogViewModel.Init();
                object content = new QuyetToanVDTPrintDialog
                {
                    DataContext = QuyetToanVDTPrintDialogViewModel
                };
                DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrintReport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();

                    foreach (var item in Items.Where(n => n.IsChecked))
                    {
                        var objDonVi = _nsDonViService.Find(item.IIDDonViQuanLyID ?? Guid.Empty);

                        if (objDonVi == null || objDonVi.Loai == "0")
                        {
                            _cap1 = (NSConstants.BO_QUOC_PHONG).ToUpper();

                        }
                        else
                        {
                            var currentDivision = _nsDonViService.FindCurrentDonViSuDungByNamLamViec(_sessionService.Current.YearOfWork);
                            if (currentDivision != null)
                                _cap1 = currentDivision.TenDonVi.ToUpper();
                        }

                        switch (item.ILoaiThanhToan)
                        {
                            case (int)PaymentTypeEnum.Type.THANH_TOAN:
                                results.Add(ExportQuyetToanVonNam(item, ExportType.EXCEL));
                                break;
                            case (int)PaymentTypeEnum.Type.TAM_UNG:
                                results.Add(ExportQuyetToanVonUng(item, ExportType.EXCEL));
                                break;
                        }
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
        private void OnUpload()
        {
            //|| Items.Where(n => n.IsSelected).Count() > 1
            if (!Items.Any(n => n.IsChecked) || Items.Count(n => n.IsChecked) > 1)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn duy nhất 1 bản ghi !");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            List<ExportResult> results = new List<ExportResult>();
            foreach (var item in Items.Where(n => n.IsChecked))
            {
                var objDonVi = _nsDonViService.Find(item.IIDDonViQuanLyID ?? Guid.Empty);

                if (objDonVi == null || objDonVi.Loai == "0")
                {
                    _cap1 = (NSConstants.BO_QUOC_PHONG).ToUpper();

                }
                else
                {
                    var currentDivision = _nsDonViService.FindCurrentDonViSuDungByNamLamViec(_sessionService.Current.YearOfWork);
                    if (currentDivision != null)
                        _cap1 = currentDivision.TenDonVi.ToUpper();
                }

                switch (item.ILoaiThanhToan)
                {
                    case (int)PaymentTypeEnum.Type.THANH_TOAN:
                        ExportQuyetToanVonNamFtpServer(item, ExportType.EXCEL);
                        break;
                    case (int)PaymentTypeEnum.Type.TAM_UNG:
                        ExportQuyetToanVonUngFtpServer(item, ExportType.EXCEL);
                        break;
                }
            }
        }
        private void OnImportVonNamData()
        {
            QuyetToanVDTImportViewModel.DataModel = new VdtQtBcquyetToanNienDoModel();
            QuyetToanVDTImportViewModel.SavedAction = obj =>
            {
                OnRefresh();
            };
            QuyetToanVDTImportViewModel.Init();
            QuyetToanVDTImportViewModel.ShowDialog();
        }

        private void OnImportVonUngData()
        {
            QuyetToanVDTVonUngImportViewModel.SavedAction = obj =>
            {
                OnRefresh();
            };
            QuyetToanVDTVonUngImportViewModel.Init();
            QuyetToanVDTVonUngImportViewModel.ShowDialog();
        }

        protected override void OnLockUnLock()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Resources.MsgRecordEmpty);
                return;
            }
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
            }

            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                LockConfirmEventHandler();
        }

        private void OnAggregate()
        {
            //check quyền được tổng hợp
            List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAggregate, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (Items.Where(x => x.IsChecked).Any(x => !x.BKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // kiểm tra cùng giai đoạn
            if (Items.Where(x => x.IsChecked).GroupBy(x => new { x.INamKeHoach, x.IIDNguonVonID, x.ILoaiThanhToan, x.ICoQuanThanhToan }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopQuyetToanNienDo, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnOpenReport()
        {
            try
            {
                if (Items.Any(n => n.IsChecked))
                {
                    QuyetToanVDTPrintReportViewModel.Selected = Items.FirstOrDefault(n => n.IsChecked);
                }
                else
                {
                    QuyetToanVDTPrintReportViewModel.Selected = new VdtQtBcquyetToanNienDoModel();
                }
                QuyetToanVDTPrintReportViewModel.Init();
                var view = new QuyetToanVDTPrintReport
                {
                    DataContext = QuyetToanVDTPrintReportViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private void LockConfirmEventHandler()
        {
            var data = _service.Find(SelectedItem.Id);
            data.BKhoa = !SelectedItem.BKhoa;
            _service.Update(data, _sessionService.Current.Principal);
            SelectedItem.BKhoa = !SelectedItem.BKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
        }

        private ExportResult ExportQuyetToanVonNam(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {
            LoadTieuDe(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM);
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("h2", string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(_title1, item.INamKeHoach));
            data.Add("Title2", _title2);
            data.Add("Items", GetDataExportQuyetToanVonNam(item));
            string templateFileName = string.Empty;
            if ((item.IIDNguonVonID ?? 0) == 1)
            {
                data.Add("ItemsPhanTich", LoadDataPhanTich(item));
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM_NSQP);
                string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM, item.SSoDeNghi);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model, RptVdtQtBcQuyetToanNienDoPhanTichModel>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
            else
            {
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM);
                string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM, item.SSoDeNghi);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }
        }
        private void ExportQuyetToanVonNamFtpServer(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {
            LoadTieuDe(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONNAM);
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("h2", string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(_title1, item.INamKeHoach));
            data.Add("Title2", _title2);
            data.Add("Items", GetDataExportQuyetToanVonNam(item));
            string templateFileName = string.Empty;
            if ((item.IIDNguonVonID ?? 0) == 1)
            {
                data.Add("ItemsPhanTich", LoadDataPhanTich(item));
                templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM_NSQP);
                string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONNAM, item.SSoDeNghi);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportVdtQtBcquyetToanNienDoChiTiet1Model, RptVdtQtBcQuyetToanNienDoPhanTichModel>(templateFileName, data);
                //
                var objdv = _nsDonViService.FindByMaDonViAndNamLamViec(item.IIDMaDonViQuanLy,Convert.ToInt32(item.INamKeHoach));
                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                var strUrl = "";
                string filePathLocal = string.Empty;
                string sStage = string.Empty;
                _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
                string sFolderRoot = "";
                if (SelectedItem != null)
                {
                    sStage = item.INamKeHoach.ToString();
                }
                if (item.ILoaiThanhToan == 1)
                {
                    sFolderRoot = ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTt;
                    strUrl = string.Format("{0}/{1}/{2}", objdv.IIDMaDonVi, ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTt, sStage);
                }
                else
                {
                    sFolderRoot = ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTu;
                    strUrl = string.Format("{0}/{1}/{2}", objdv.IIDMaDonVi, ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTu, sStage);
                }
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
        }
        private List<ExportVdtQtBcquyetToanNienDoChiTiet1Model> GetDataExportQuyetToanVonNam(VdtQtBcquyetToanNienDoModel item)
        {
            Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01> dicDataDetail = new Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01>();
            List<VdtQtBcquyetToanNienDoChiTiet1Query> data = new List<VdtQtBcquyetToanNienDoChiTiet1Query>();
            var defaultData = _service.GetQuyetToanNienDoVonNamByParentId(item.Id);
            if (defaultData == null || defaultData.Count == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoDetail(item.IIDMaDonViQuanLy, item.INamKeHoach ?? 0, item.IIDNguonVonID ?? 0);
            }
            var results = SetupViewData(defaultData, (item.IIDNguonVonID ?? 0));
            int i = 0;
            foreach (var child in results)
            {
                if (child.IsHangCha)
                {
                    i = 0;
                    continue;
                }
                i++;
                child.iStt = i.ToString();
                child.FVonConLaiHuyBoKeoDaiNamNay = child.FKHVNamTruocChuyenNamNay - child.FTongThanhToanVonKeoDaiNamNay - child.FGiaTriNamTruocChuyenNamSau;
                child.FVonConLaiHuyBoNamNay = child.FKHVNamNay - child.FTongKeHoachThanhToanVonNamNay - child.FGiaTriNamNayChuyenNamSau;
                child.FLuyKeTamUngChuaThuHoiChuyenSangNam =
                    child.FTamUngTheoCheDoChuaThuHoiNamTruoc - child.FGiaTriTamUngDieuChinhGiam - child.FTamUngNamTruocThuHoiNamNay
                    + child.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay + child.FTamUngTheoCheDoChuaThuHoiNamNay;
            }
            return _mapper.Map<List<ExportVdtQtBcquyetToanNienDoChiTiet1Model>>(results);
        }

        private ExportResult ExportQuyetToanVonUng(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {
            LoadTieuDe(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG);
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("h2", string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(_title1, item.INamKeHoach));
            data.Add("Title2", string.Format(_title2, item.INamKeHoach));
            data.Add("Items", GetDataExportQuyetToanVonUng(item));

            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG);
            string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG, item.SSoDeNghi);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ExportBcquyetToanNienDoVonUngChiTietModel>(templateFileName, data);
            return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
        }
        private void ExportQuyetToanVonUngFtpServer(VdtQtBcquyetToanNienDoModel item, ExportType exportType)
        {

            LoadTieuDe(TypeChuKy.RPT_VDT_QUYETTOANNIENDO_VONUNG);
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("sTenDonVi", item.STenDonVi);
            data.Add("dNgayHienTai", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("iNam", item.INamKeHoach);
            data.Add("DiaDiem", _diaDiem);
            data.Add("Cap1", _cap1);
            data.Add("h2", string.Format("Đơn vị tính: {0}", SelectedUnit.DisplayItem));
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
            data.Add("Title1", string.Format(_title1, item.INamKeHoach));
            data.Add("Title2", string.Format(_title2, item.INamKeHoach));
            data.Add("Items", GetDataExportQuyetToanVonUng(item));
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QT, ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG);
            string fileNamePrefix = string.Format("{0}_{1}", ExportFileName.RPT_VDT_QUYETTOANNIENDO_VONUNG, item.SSoDeNghi);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<ExportBcquyetToanNienDoVonUngChiTietModel>(templateFileName, data);
            var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            var objdv = _nsDonViService.FindByMaDonViAndNamLamViec(item.IIDMaDonViQuanLy, Convert.ToInt32(item.INamKeHoach));
            var strUrl = "";
            string filePathLocal = string.Empty;
            string sStage = string.Empty;
            _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
            string sFolderRoot = "";
            if (SelectedItem != null)
            {
                sStage = item.INamKeHoach.ToString();
            }
            if (item.ILoaiThanhToan == 1)
            {
                sFolderRoot = ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTt;
                strUrl = string.Format("{0}/{1}/{2}", objdv.IIDMaDonVi, ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTt, sStage);
            }
            else
            {
                sFolderRoot = ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTu;
                strUrl = string.Format("{0}/{1}/{2}", objdv.IIDMaDonVi, ConstantUrlPathPhanHe.UrlBcqtcnvdtWinformReceiveTu, sStage);
            }
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

        private List<ExportBcquyetToanNienDoVonUngChiTietModel> GetDataExportQuyetToanVonUng(VdtQtBcquyetToanNienDoModel item)
        {
            Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01> dicDataDetail = new Dictionary<Guid, VdtQtBcQuyetToanNienDoChiTiet01>();
            List<BcquyetToanNienDoVonUngChiTietQuery> data = new List<BcquyetToanNienDoVonUngChiTietQuery>();
            var defaultData = _service.GetQuyetToanNienDoVonUngByParentId(item.Id);
            if (defaultData == null || defaultData.Count() == 0)
            {
                defaultData = _service.GetDeNghiQuyetToanNienDoVonUngDetail(item.IIDMaDonViQuanLy, item.INamKeHoach ?? 0, item.IIDNguonVonID ?? 0);
            }
            var results = SetupViewDataVonUng(defaultData);
            int i = 0;
            foreach (var child in results)
            {
                if (child.IsHangCha)
                {
                    i = 0;
                    continue;
                }
                i++;
                child.iStt = i.ToString();
            }
            return _mapper.Map<List<ExportBcquyetToanNienDoVonUngChiTietModel>>(results);
        }

        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => _lstDonViExclude.Contains(n.Loai))
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void GetNguonNganSach()
        {
            var cbxNguonNganSachData = _nsNguonNganSachService.FindNguonNganSach()
                .OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
            _drpNguonNganSach = new ObservableCollection<ComboboxItem>(cbxNguonNganSachData);
        }

        private bool VdtQtQuyetToanNienDoFilter(object obj)
        {
            if (!(obj is VdtQtBcquyetToanNienDoModel temp)) return true;
            var bCondition = true;
            int iNamKeHoachParse = 0;

            if (!string.IsNullOrEmpty(INamKeHoach) && int.TryParse(INamKeHoach, out iNamKeHoachParse))
            {
                bCondition &= (temp.INamKeHoach == iNamKeHoachParse);
            }
            if (DNgayQuyetDinhFrom.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi >= DNgayQuyetDinhFrom);
            }
            if (DNgayQuyetDinhTo.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi <= DNgayQuyetDinhTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.IIDMaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (DrpNguonNganSachSelected != null)
            {
                bCondition &= (temp.IIDNguonVonID.ToString() == DrpNguonNganSachSelected.ValueItem);
            }
            return bCondition;
        }

        private void OnOpenDisbursementPaymentDetail(VdtQtBcquyetToanNienDoModel SelectedItem, bool bIsDetail = false, bool BIsTongHop = false)
        {
            switch (SelectedItem.ILoaiThanhToan)
            {
                case (int)PaymentTypeEnum.Type.THANH_TOAN:
                    QuyetToanVDTDetailViewModel.BIsDetail = bIsDetail;
                    QuyetToanVDTDetailViewModel.BIsTongHop = BIsTongHop;
                    QuyetToanVDTDetailViewModel.Model = SelectedItem;
                    QuyetToanVDTDetailViewModel.Init();
                    var view = new QuyetToanVDTDetail { DataContext = QuyetToanVDTDetailViewModel };
                    view.ShowDialog();
                    break;
                case (int)PaymentTypeEnum.Type.TAM_UNG:
                    QuyetToanVDTCQTCDetailViewModel.BIsDetail = bIsDetail;
                    QuyetToanVDTCQTCDetailViewModel.BIsTongHop = BIsTongHop;
                    QuyetToanVDTCQTCDetailViewModel.Model = SelectedItem;
                    QuyetToanVDTCQTCDetailViewModel.Init();
                    var viewCQTC = new QuyetToanVDTVonUngDetail { DataContext = QuyetToanVDTCQTCDetailViewModel };
                    viewCQTC.ShowDialog();
                    break;
            }
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _service.DeleteDeNghiQuyetToan(SelectedItem.Id);
            _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.QUYET_TOAN, (int)TypeExecute.Delete, SelectedItem.Id);
            LoadData();
        }

        private void LoadTieuDe(string typeChuKy)
        {
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(typeChuKy) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                _dmChuKy = new DmChuKy();
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe1MoTa))
                _title1 = _dmChuKy.TieuDe1MoTa;
            if (!string.IsNullOrEmpty(_dmChuKy.TieuDe2MoTa))
                _title2 = _dmChuKy.TieuDe2MoTa;
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE)
                .ToList();
            if (listDonViTinh.Count == 0)
                _units.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                _units.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            OnPropertyChanged(nameof(Units));
            _selectedUnit = Units.ElementAt(0);

            var danhMucQuanLy = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
            _cap1 = danhMucQuanLy == null ? string.Empty : danhMucQuanLy.SGiaTri;
            var danhMucDiaDiem = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DIADIEM).FirstOrDefault();
            _diaDiem = danhMucDiaDiem == null ? string.Empty : danhMucDiaDiem.SGiaTri;
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtQtBcquyetToanNienDoModel item = (VdtQtBcquyetToanNienDoModel)sender;
            if (args.PropertyName != nameof(VdtQtBcquyetToanNienDoModel.BIsShowChild)) return;
            LoadDataByTabIndex();
            OnPropertyChanged(nameof(Items));
        }

        private List<VdtQtBcquyetToanNienDoChiTiet1Model> SetupViewData(List<VdtQtBcquyetToanNienDoChiTiet1Query> lstData, int iIdNguonVonId)
        {
            List<VdtQtBcquyetToanNienDoChiTiet1Model> results = new List<VdtQtBcquyetToanNienDoChiTiet1Model>();
            if (lstData == null) return results;
            List<VdtQtBcquyetToanNienDoChiTiet1Model> dataConvert = _mapper.Map<List<VdtQtBcquyetToanNienDoChiTiet1Model>>(lstData);

            if (iIdNguonVonId == 1)
            {
                return dataConvert.GroupBy(n => new { n.IIDDuAnID, n.SMaDuAn, n.SDiaDiem, n.STenDuAn, n.FTongMucDauTu, n.SMaLoaiCongTrinh })
                    .Select(n => new VdtQtBcquyetToanNienDoChiTiet1Model()
                    {
                        IIDDuAnID = n.Key.IIDDuAnID,
                        SMaDuAn = n.Key.SMaDuAn,
                        SDiaDiem = n.Key.SDiaDiem,
                        STenDuAn = n.Key.STenDuAn,
                        SMaLoaiCongTrinh = n.Key.SMaLoaiCongTrinh,
                        FTongMucDauTu = n.Key.FTongMucDauTu,
                        FLuyKeThanhToanNamTruoc = n.Sum(k => k.FLuyKeThanhToanNamTruoc),
                        FTamUngTheoCheDoChuaThuHoiNamTruoc = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamTruoc),
                        FGiaTriTamUngDieuChinhGiam = n.Sum(k => k.FGiaTriTamUngDieuChinhGiam),
                        FTamUngNamTruocThuHoiNamNay = n.Sum(k => k.FTamUngNamTruocThuHoiNamNay),
                        FKHVNamTruocChuyenNamNay = n.Sum(k => k.FKHVNamTruocChuyenNamNay),
                        FTongThanhToanVonKeoDaiNamNay = n.Sum(k => k.FTongThanhToanVonKeoDaiNamNay),
                        FTongThanhToanSuDungVonNamTruoc = n.Sum(k => k.FTongThanhToanSuDungVonNamTruoc),
                        FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiKeoDaiNamNay),
                        FGiaTriNamTruocChuyenNamSau = n.Sum(k => k.FGiaTriNamTruocChuyenNamSau),
                        FVonConLaiHuyBoKeoDaiNamNay = n.Sum(k => k.FVonConLaiHuyBoKeoDaiNamNay),
                        FKHVNamNay = n.Sum(k => k.FKHVNamNay),
                        FTongKeHoachThanhToanVonNamNay = n.Sum(k => k.FTongKeHoachThanhToanVonNamNay),
                        FTongThanhToanSuDungVonNamNay = n.Sum(k => k.FTongThanhToanSuDungVonNamNay),
                        FTamUngTheoCheDoChuaThuHoiNamNay = n.Sum(k => k.FTamUngTheoCheDoChuaThuHoiNamNay),
                        FGiaTriNamNayChuyenNamSau = n.Sum(k => k.FGiaTriNamNayChuyenNamSau),
                        FVonConLaiHuyBoNamNay = n.Sum(k => k.FVonConLaiHuyBoNamNay),
                        FTongVonThanhToanNamNay = n.Sum(k => k.FTongVonThanhToanNamNay),
                        FLuyKeTamUngChuaThuHoiChuyenSangNam = n.Sum(k => k.FLuyKeTamUngChuaThuHoiChuyenSangNam),
                        FLuyKeConDaThanhToanHetNamNay = n.Sum(k => k.FLuyKeConDaThanhToanHetNamNay)
                    }).ToList();
            }

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    iStt = "A",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new VdtQtBcquyetToanNienDoChiTiet1Model()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    iStt = "B",
                    IsHangCha = true
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }

        private List<BcquyetToanNienDoVonUngChiTietModel> SetupViewDataVonUng(List<BcquyetToanNienDoVonUngChiTietQuery> lstData)
        {
            List<BcquyetToanNienDoVonUngChiTietModel> results = new List<BcquyetToanNienDoVonUngChiTietModel>();
            if (lstData == null) return results;
            List<BcquyetToanNienDoVonUngChiTietModel> dataConvert = _mapper.Map<List<BcquyetToanNienDoVonUngChiTietModel>>(lstData);

            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA BỘ QUỐC PHÒNG",
                    IsHangCha = true,
                    iStt = "A"
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.CQTC));
            }
            if (dataConvert.Any(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC))
            {
                results.Add(new BcquyetToanNienDoVonUngChiTietModel()
                {
                    STenDuAn = "* CẤP QUA KHO BẠC",
                    IsHangCha = true,
                    iStt = "B"
                });
                results.AddRange(dataConvert.Where(n => n.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC));
            }
            return results;
        }

        private List<RptVdtQtBcQuyetToanNienDoPhanTichModel> LoadDataPhanTich(VdtQtBcquyetToanNienDoModel objQuyetToan)
        {
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> data = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            List<VdtQtBcQuyetToanNienDoPhanTichQuery> defaultDatas = new List<VdtQtBcQuyetToanNienDoPhanTichQuery>();
            List<RptVdtQtBcQuyetToanNienDoPhanTichModel> result = new List<RptVdtQtBcQuyetToanNienDoPhanTichModel>();
            defaultDatas = _service.GetBaoCaoQuyetToanNienDoPhanTichById(objQuyetToan.Id).ToList();
            if (defaultDatas != null && defaultDatas.Count != 0)
            {
                result.AddRange(_mapper.Map<List<RptVdtQtBcQuyetToanNienDoPhanTichModel>>(defaultDatas));
            }
            else
            {
                data = _service.GetBaoCaoQuyetToanNienDoPhanTich(objQuyetToan.IIDMaDonViQuanLy, objQuyetToan.INamKeHoach.Value, objQuyetToan.IIDNguonVonID.Value).ToList();
                result.AddRange(_mapper.Map<List<RptVdtQtBcQuyetToanNienDoPhanTichModel>>(data));
            }
            int i = 0;
            foreach (var item in result)
            {
                i++;
                item.SSoThuTu = i.ToString();
            }
            return result;
        }
        #endregion
    }
}
