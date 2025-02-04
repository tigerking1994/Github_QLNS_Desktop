using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonUngDeXuat;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat
{
    public class KeHoachVonUngDeXuatIndexViewModel : GridViewModelBase<VdtKhvKeHoachVonUngDxModel>
    {
        #region Public
        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_ADVANCE_CAPITAL_APPROVED_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN;
        public override string Name => "Kế hoạch vốn ứng đề xuất";
        public override string Description => "Danh sách thông tin kế hoạch vốn ứng đề xuất";
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.KeHoachVonUngDeXuat.KeHoachVonUngDeXuatIndex);
        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa && SelectedItem.BActive  ;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;
        public bool IsAggregate => TabIndex == VoucherTabIndex.VOUCHER;
        public bool IsEnableLock => SelectedItem != null;
        #endregion

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly IVdtKhvKeHoachVonUngDxService _keHoachVonUngService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly IVdtDmDonViThucHienDuAnService _vdtDmDonViThucHienDuAnService;
        private ICollectionView _deNghiThanhToanView;
        private IMapper _mapper;
        private List<VdtKhvKeHoachVonUngDxModel> _lstDataRoot;
        private Dictionary<Guid, List<VdtKhvKeHoachVonUngDxModel>> _dicDataChild;
        private readonly ILog _logger;
        private readonly IExportService _exportService;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand SummaryCommand { get; set; }
        public RelayCommand FixDataCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportDataCommand { get; }
        #endregion

        #region Componer
        private bool _bIsShowChild;
        public bool BIsShowChild
        {
            get => _bIsShowChild;
            set
            {
                SetProperty(ref _bIsShowChild, value);
                LoadDataByTabIndex();
            }
        }

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadDataByTabIndex();
            }
        }

        private DateTime? _dNgayLapFrom;
        public DateTime? DNgayLapFrom
        {
            get => _dNgayLapFrom;
            set
            {
                SetProperty(ref _dNgayLapFrom, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayLapTo;
        public DateTime? DNgayLapTo
        {
            get => _dNgayLapTo;
            set
            {
                SetProperty(ref _dNgayLapTo, value);
                OnSearch();
            }
        }

        private string _sNamKeHoach;
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set => SetProperty(ref _sNamKeHoach, value);
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
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
        #endregion

        public KeHoachVonUngDeXuatDialogViewModel KeHoachVonUngDeXuatDialogViewModel { get; set; }
        public KeHoachVonUngDeXuatDetailViewModel KeHoachVonUngDeXuatDetailViewModel { get; set; }
        public KeHoachVonUngDeXuatImportViewModel KeHoachVonUngDeXuatImportViewModel { get; set; }
        public KeHoachVonUngDeXuatImport KeHoachVonUngDeXuatImport { get; set; }  

        public KeHoachVonUngDeXuatIndexViewModel(KeHoachVonUngDeXuatDialogViewModel keHoachVonUngDeXuatDialogViewModel,
            KeHoachVonUngDeXuatDetailViewModel keHoachVonUngDeXuatDetailViewModel,
            KeHoachVonUngDeXuatImportViewModel keHoachVonUngDeXuatImportViewModel,
            IVdtKhvKeHoachVonUngDxService keHoachVonUngService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nguonVonService,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IMapper mapper, 
            ILog logger, 
            IExportService exportService, 
            IVdtDmDonViThucHienDuAnService dmDonViThucHienDuAnService)
        {
            KeHoachVonUngDeXuatDialogViewModel = keHoachVonUngDeXuatDialogViewModel;
            KeHoachVonUngDeXuatDialogViewModel.ParentPage = this;
            KeHoachVonUngDeXuatDetailViewModel = keHoachVonUngDeXuatDetailViewModel;
            KeHoachVonUngDeXuatDetailViewModel.ParentPage = this;
            KeHoachVonUngDeXuatImportViewModel = keHoachVonUngDeXuatImportViewModel;
            KeHoachVonUngDeXuatImportViewModel.ParentPage = this;
            _keHoachVonUngService = keHoachVonUngService;
            _sessionService = sessionService;
            _tonghopService = tonghopService;
            _nsDonViService = nsDonViService;
            _nguonVonService = nguonVonService;
            _exportService = exportService;
            _vdtDmDonViThucHienDuAnService = dmDonViThucHienDuAnService;
            _mapper = mapper;
            _logger = logger;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            SummaryCommand = new RelayCommand(obj => OnSummary());
            FixDataCommand = new RelayCommand(obj => OnFixData());
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            _tabIndex = VoucherTabIndex.VOUCHER;
            GetDonViQuanLy();
            GetNguonVon();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            _lstDataRoot = new List<VdtKhvKeHoachVonUngDxModel>();
            _dicDataChild = new Dictionary<Guid, List<VdtKhvKeHoachVonUngDxModel>>();
            List<VdtKhvKeHoachVonUngDxModel> listChungTu = _mapper.Map<List<VdtKhvKeHoachVonUngDxModel>>(_keHoachVonUngService.GetKeHoachVonUngIndex());
            ConvertDataDefault(listChungTu);
            LoadDataByTabIndex();
        }

        private void ConvertDataDefault(List<VdtKhvKeHoachVonUngDxModel> lstChungTu)
        {
            Dictionary<Guid, Guid> dicRefer = new Dictionary<Guid, Guid>();
            foreach (var item in lstChungTu)
            {
                if (dicRefer.ContainsKey(item.Id))
                {
                    item.BIsTongHop = true;
                    if (!_dicDataChild.ContainsKey(dicRefer[item.Id]))
                        _dicDataChild.Add(dicRefer[item.Id], new List<VdtKhvKeHoachVonUngDxModel>());
                    _dicDataChild[dicRefer[item.Id]].Add(item);
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.STongHop))
                    {
                        item.BIsTongHop = true;
                        foreach (var iIdChild in item.STongHop.Split(";").Select(n => Guid.Parse(n)))
                        {
                            if(!dicRefer.ContainsKey(iIdChild))
                                dicRefer.Add(iIdChild, item.Id);
                        }
                            
                    }
                    _lstDataRoot.Add(item);
                }
            }
        }

        private void LoadDataByTabIndex()
        {
            if (_lstDataRoot == null) return;
            var lstItem = _lstDataRoot.Where(n => n.BIsTongHop == (TabIndex == VoucherTabIndex.VOUCHER_AGREGATE));
            List<VdtKhvKeHoachVonUngDxModel> datas = new List<VdtKhvKeHoachVonUngDxModel>();
            int i = 1;
            foreach (var item in lstItem)
            {
                item.IRowIndex = i;
                datas.Add(item);
                if (_dicDataChild.ContainsKey(item.Id))
                {
                    if (SelectedItem != null && item.Id == SelectedItem.Id)
                        item.BIsShowChild = !item.BIsShowChild;
                    if (item.BIsShowChild)
                        datas.AddRange(_dicDataChild[item.Id]);
                }
                ++i;
            }
            Items = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngDxModel>>(datas);
            _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
            _deNghiThanhToanView.Filter = VdtKhvKeHoachVonUngFilter;
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _deNghiThanhToanView.Refresh();
        }

        protected override void OnAdd()
        {
            KeHoachVonUngDeXuatDialogViewModel.IsInsert = true;
            KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = false;
            KeHoachVonUngDeXuatDialogViewModel.IsDieuChinh = true;
            KeHoachVonUngDeXuatDialogViewModel.Model = new VdtKhvKeHoachVonUngDxModel();
            KeHoachVonUngDeXuatDialogViewModel.Init();
            KeHoachVonUngDeXuatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenKeHoachVonUngDeXuatDetailViewModel(_mapper.Map<VdtKhvKeHoachVonUngDxModel>(obj));
            };
            var view = new KeHoachVonUngDeXuatDialog
            {
                DataContext = KeHoachVonUngDeXuatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            KeHoachVonUngDeXuatDialogViewModel.IsInsert = false;
            KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = !string.IsNullOrEmpty(SelectedItem.STongHop);
            KeHoachVonUngDeXuatDialogViewModel.Model = SelectedItem;
            KeHoachVonUngDeXuatDialogViewModel.IsDieuChinh = false;

            if (!string.IsNullOrEmpty(SelectedItem.STongHop))
            {
                KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = true;
                if (_dicDataChild.ContainsKey(SelectedItem.Id))
                {
                    KeHoachVonUngDeXuatDialogViewModel.ItemsTongHop = new ObservableCollection<VdtKhvKeHoachVonUngDxModel>(_dicDataChild[SelectedItem.Id]);
                }
            }

            KeHoachVonUngDeXuatDialogViewModel.Init();
            KeHoachVonUngDeXuatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenKeHoachVonUngDeXuatDetailViewModel(_mapper.Map<VdtKhvKeHoachVonUngDxModel>(obj));
            };
            var view = new KeHoachVonUngDeXuatDialog
            {
                DataContext = KeHoachVonUngDeXuatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteKeHoachVonUng);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            DNgayLapFrom = null;
            DNgayLapTo = null;
            DrpDonViQuanLySelected = null;
            SNamKeHoach = null;
            SelectedNguonVon = null;
            OnPropertyChanged(nameof(DNgayLapFrom));
            OnPropertyChanged(nameof(DNgayLapTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(SNamKeHoach));
            OnPropertyChanged(nameof(SelectedNguonVon));
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
            var data = (VdtKhvKeHoachVonUngDxModel)obj;
            if (!string.IsNullOrEmpty(data.STongHop))
            {
                OnOpenKeHoachVonUngDeXuatDetailViewModel(data, true, true);
            }
            else
            {
                OnOpenKeHoachVonUngDeXuatDetailViewModel(data, true);
            }
        }

        protected override void OnLockUnLock()
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
            }

            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                LockConfirmEventHandler();
        }

        private void OnSummary()
        {
            //check quyền được tổng hợp
            List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAggregate, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //kiểm tra trạng thái các bản ghi
            if (Items.Where(x => x.Selected).Any(x => !x.BKhoa))
            {
                MessageBox.Show(Resources.AlertAggregateUnLocked, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // kiểm tra cùng giai đoạn
            if (Items.Where(x => x.Selected).GroupBy(x => new { x.INamKeHoach, x.IIDNguonVonID }).Count() > 1)
            {
                MessageBox.Show(Resources.MsgErrorTongHopKeHoachVonUng, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //kiểm tra đã tồn tại chứng từ tổng hợp từ các chứng từ đã chọn chưa
            OnAddTongHopChungTu();
        }

        private void OnFixData()
        {
            try
            {
                KeHoachVonUngDeXuatDialogViewModel.IsInsert = false;
                KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = !string.IsNullOrEmpty(SelectedItem.STongHop);
                KeHoachVonUngDeXuatDialogViewModel.Model = SelectedItem;
                KeHoachVonUngDeXuatDialogViewModel.IsDieuChinh = true;


                if (!string.IsNullOrEmpty(SelectedItem.STongHop))
                {
                    KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = true;
                    if (_dicDataChild.ContainsKey(SelectedItem.Id))
                    {
                        KeHoachVonUngDeXuatDialogViewModel.ItemsTongHop = new ObservableCollection<VdtKhvKeHoachVonUngDxModel>(_dicDataChild[SelectedItem.Id]);
                    }
                }

                KeHoachVonUngDeXuatDialogViewModel.Init();
                KeHoachVonUngDeXuatDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenKeHoachVonUngDeXuatDetailViewModel(_mapper.Map<VdtKhvKeHoachVonUngDxModel>(obj));
                };
                var view = new KeHoachVonUngDeXuatDialog
                {
                    DataContext = KeHoachVonUngDeXuatDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnExportExcel()
        {
            try
            {
                List<VdtKhvKeHoachVonUngDxModel> listExport = Items.Where(x => x.Selected).ToList();
                if (listExport.GroupBy(x => x.INamKeHoach).Count() > 1)
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
                Items.Where(x => x.Selected).Select(x => x.IIDMaDonViQuanLy).Select(item =>
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
                    MessageBox.Show("Bạn không có quyền export KHVU");
                    return;
                }

                if (listExport != null && listExport.Count == 0)
                {
                    MessageBox.Show(Resources.MsgRecordEmpty);
                    return;
                }

                int iNamKeHoach = listExport.FirstOrDefault().INamKeHoach;
                List<ExportVonUngDonViQuery> lstData = _keHoachVonUngService.GetKeHoachVonUngDonViExport(listExport.Select(n => n.Id).ToList()).ToList();
                var data = lstData.GroupBy(n => new { n.iID_KeHoachUngID })
                    .Select(n => new ExportVonUngDonViModel()
                    {
                        Datas = n.ToList(),
                    });

                if (data != null && data.Count() == 0)
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

        private ExportResult ExportData(ExportVonUngDonViModel item, int iNamKeHoach, ExportType exportType)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                item.Datas = item.Datas.Select((x, index) => { x.iStt = (index + 1).ToString(); return x; }).ToList();
                
                data.Add("iNamLamViec", iNamKeHoach);
                data.Add("Items", item.Datas);
                data.Add("dv", _vdtDmDonViThucHienDuAnService.GetDonViThucHienDuAnExport().ToList());

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVU, YearPlanManagerType.RPT_KH_VONUNG_DONVI);
                string fileNamePrefix = YearPlanManagerType.OUTPUT_KH_VONUNG_DONVI;
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<ExportVonUngDonViQuery, NSDonViThucHienDuAnExportQuery>(templateFileName, data);
                return new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);                
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return null;
            }
        }

        private void OnImportData()
        {
            try
            {
                KeHoachVonUngDeXuatImportViewModel.SavedAction = obj =>
                {
                    KeHoachVonUngDeXuatImport.Close();
                    OnRefresh();
                };
                KeHoachVonUngDeXuatImportViewModel.Init();
                //
                KeHoachVonUngDeXuatImport = new KeHoachVonUngDeXuatImport { DataContext = KeHoachVonUngDeXuatImportViewModel };

                KeHoachVonUngDeXuatImport.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private void OnAddTongHopChungTu()
        {
            var childItem = Items.FirstOrDefault(n => n.Selected);
            if (childItem == null)
            {
                MessageBox.Show(Resources.MsgRecordEmpty);
                return;
            }
            KeHoachVonUngDeXuatDialogViewModel.IsInsert = true;
            KeHoachVonUngDeXuatDialogViewModel.IsDieuChinh = true;
            KeHoachVonUngDeXuatDialogViewModel.Model = childItem.Clone();
            KeHoachVonUngDeXuatDialogViewModel.Model.Id = Guid.Empty;
            KeHoachVonUngDeXuatDialogViewModel.BIsTongHop = true;
            KeHoachVonUngDeXuatDialogViewModel.ItemsTongHop = new ObservableCollection<VdtKhvKeHoachVonUngDxModel>(Items.Where(n => n.Selected));
            KeHoachVonUngDeXuatDialogViewModel.Init();
            KeHoachVonUngDeXuatDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenKeHoachVonUngDeXuatDetailViewModel(_mapper.Map<VdtKhvKeHoachVonUngDxModel>(obj), false, true);
            };
            var view = new KeHoachVonUngDeXuatDialog
            {
                DataContext = KeHoachVonUngDeXuatDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void LockConfirmEventHandler()
        {
            _keHoachVonUngService.LogItem(SelectedItem.Id, _sessionService.Current.Principal);
            SelectedItem.BKhoa = !SelectedItem.BKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
        }

        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) }).Distinct().ToList();
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void GetNguonVon()
        {
            var cbxNguonVonData = _nguonVonService.FindNguonNganSach()
                .OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { DisplayItem = n.STen, ValueItem = n.IIdMaNguonNganSach.ToString() });
            ItemsNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private bool VdtKhvKeHoachVonUngFilter(object obj)
        {
            if (!(obj is VdtKhvKeHoachVonUngDxModel temp)) return true;
            var bCondition = true;
            int iNamKeHoach = 0;
            if (DNgayLapFrom.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi.Value.Date >= DNgayLapFrom.Value.Date);
            }
            if (DNgayLapTo.HasValue)
            {
                bCondition &= (temp.DNgayDeNghi.HasValue && temp.DNgayDeNghi.Value.Date <= DNgayLapTo.Value.Date);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.IIDMaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if(!string.IsNullOrEmpty(SNamKeHoach) && int.TryParse(SNamKeHoach, out iNamKeHoach))
            {
                bCondition &= (temp.INamKeHoach == iNamKeHoach);
            }
            if(SelectedNguonVon != null)
            {
                bCondition &= (temp.IIDNguonVonID == int.Parse(SelectedNguonVon.ValueItem));
            }
            return bCondition;
        }

        private void OnOpenKeHoachVonUngDeXuatDetailViewModel(VdtKhvKeHoachVonUngDxModel SelectedItem, bool bIsDetail = false, bool BIsTongHop = false)
        {
            KeHoachVonUngDeXuatDetailViewModel.BIsDetail = bIsDetail;
            KeHoachVonUngDeXuatDetailViewModel.BIsTongHop = BIsTongHop;
            KeHoachVonUngDeXuatDetailViewModel.Model = SelectedItem;
            KeHoachVonUngDeXuatDetailViewModel.Init();
            var view = new KeHoachVonUngDeXuatDetail { DataContext = KeHoachVonUngDeXuatDetailViewModel };
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_UNG, (int)TypeExecute.Delete, SelectedItem.Id);
            _keHoachVonUngService.DeleteKeHoachVonUng(_mapper.Map<VdtKhvKeHoachVonUngDx>(SelectedItem));
            LoadData();
        }
        #endregion
    }
}
