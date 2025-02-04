using AutoMapper;
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
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.AdvanceCapitalApproved;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.AdvanceCapitalApproved
{
    public class AdvanceCapitalApprovedIndexViewModel : GridViewModelBase<VdtKhvKeHoachVonUngModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_STANDARD_ADVANCE_CAPITAL_APPROVED_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN;
        public override string Name => "Kế hoạch vốn ứng được duyệt";
        public override string Description => "Danh sách thông tin kế hoạch vốn ứng được duyệt";
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty && SelectedItem.bActive;
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.AdvanceCapitalApproved.AdvanceCapitalApprovedIndex);

        #region Private
        private static string[] _lstDonViExclude = new string[] { "0", "1" };
        private readonly ILog _logger;
        private readonly IVdtKhvKeHoachVonUngService _keHoachVonUngService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private ICollectionView _deNghiThanhToanView;
        private IMapper _mapper;
        private readonly IExportService _exportService;
        #endregion

        #region declare RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand FixDataCommand { get; }
        public RelayCommand ExportCommand { get; set; }
        #endregion

        #region Componer
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

        public AdvanceCapitalApprovedDialogViewModel AdvanceCapitalApprovedDialogViewModel { get; set; }
        public AdvanceCapitalApprovedDetailViewModel AdvanceCapitalApprovedDetailViewModel { get; set; }

        public AdvanceCapitalApprovedIndexViewModel(AdvanceCapitalApprovedDialogViewModel advanceCapitalApprovedDialogViewModel,
            AdvanceCapitalApprovedDetailViewModel advanceCapitalApprovedDetailViewModel,
            IVdtKhvKeHoachVonUngService _keHoachVonUngService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nguonVonService,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IExportService exportService,
            IMapper mapper)
        {
            AdvanceCapitalApprovedDialogViewModel = advanceCapitalApprovedDialogViewModel;
            AdvanceCapitalApprovedDialogViewModel.ParentPage = this;
            AdvanceCapitalApprovedDetailViewModel = advanceCapitalApprovedDetailViewModel;
            AdvanceCapitalApprovedDetailViewModel.ParentPage = this;
            this._keHoachVonUngService = _keHoachVonUngService;
            _sessionService = sessionService;
            _tonghopService = tonghopService;
            _nguonVonService = nguonVonService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _mapper = mapper;
            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            FixDataCommand = new RelayCommand(obj => OnFixData());
            ExportCommand = new RelayCommand(obj => OnExportExcel());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            GetNguonVon();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            IEnumerable<VdtKhvKeHoachVonUngQuery> listChungTu = _keHoachVonUngService.GetKeHoachVonUngIndex();
            var lstItem = _mapper.Map<List<VdtKhvKeHoachVonUngModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtKhvKeHoachVonUngModel>>(lstItem);
            _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
            _deNghiThanhToanView.Filter = VdtKhvKeHoachVonUngFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
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
            AdvanceCapitalApprovedDialogViewModel.Model = new VdtKhvKeHoachVonUngModel();
            AdvanceCapitalApprovedDialogViewModel.IsInsertDieuChinh = true;
            AdvanceCapitalApprovedDialogViewModel.Init();
            AdvanceCapitalApprovedDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenAdvanceCapitalApprovedDetail((VdtKhvKeHoachVonUngModel)obj);
            };
            var view = new AdvanceCapitalApprovedDialog
            {
                DataContext = AdvanceCapitalApprovedDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnUpdate()
        {
            AdvanceCapitalApprovedDialogViewModel.Model = SelectedItem;
            AdvanceCapitalApprovedDialogViewModel.IsInsertDieuChinh = AdvanceCapitalApprovedDialogViewModel.Model.Id==Guid.Empty;
            AdvanceCapitalApprovedDialogViewModel.Init();
            AdvanceCapitalApprovedDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenAdvanceCapitalApprovedDetail((VdtKhvKeHoachVonUngModel)obj);
            };
            var view = new AdvanceCapitalApprovedDialog
            {
                DataContext = AdvanceCapitalApprovedDialogViewModel
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

        private void OnExportExcel()
        {
            try
            {
                List<VdtKhvKeHoachVonUngModel> listExport = Items.Where(x => x.Selected).ToList();
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
                    MessageBox.Show("Bạn không có quyền export KHVU");
                    return;
                }

                if (listExport != null && listExport.Count == 0)
                {
                    MessageBox.Show(Resources.MsgRecordEmpty);
                    return;
                }

                int iNamKeHoach = listExport.FirstOrDefault().iNamKeHoach.Value;
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

        private ExportResult ExportData(ExportVonUngDonViModel item, int iNamKeHoach, ExportType eXCEL)
        {
            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                item.Datas = item.Datas.Select((x, index) => { x.iStt = (index + 1).ToString(); return x; }).ToList();

                data.Add("iNamLamViec", iNamKeHoach);
                data.Add("Items", item.Datas);                

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHVU, YearPlanManagerType.RPT_KH_VONUNG_DUOCDUYET);
                string fileNamePrefix = YearPlanManagerType.OUTPUT_KH_VONUNG_DUOCDUYET;
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
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            var data = (VdtKhvKeHoachVonUngModel)obj;
            OnOpenAdvanceCapitalApprovedDetail(data, true);
        }

        #endregion

        #region Helper
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
            if (!(obj is VdtKhvKeHoachVonUngModel temp)) return true;
            var bCondition = true;
            int iNamKeHoach = 0;
            if (DNgayLapFrom.HasValue)
            {
                bCondition &= (temp.dNgayQuyetDinh.HasValue && temp.dNgayQuyetDinh >= DNgayLapFrom);
            }
            if (DNgayLapTo.HasValue)
            {
                bCondition &= (temp.dNgayQuyetDinh.HasValue && temp.dNgayQuyetDinh <= DNgayLapTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (!string.IsNullOrEmpty(SNamKeHoach) && int.TryParse(SNamKeHoach, out iNamKeHoach))
            {
                bCondition &= (temp.iNamKeHoach == iNamKeHoach);
            }
            if (SelectedNguonVon != null)
            {
                bCondition &= (temp.iId_NguonVonId == int.Parse(SelectedNguonVon.ValueItem));
            }
            return bCondition;
        }

        private void OnOpenAdvanceCapitalApprovedDetail(VdtKhvKeHoachVonUngModel SelectedItem, bool bIsDetail = false)
        {
            AdvanceCapitalApprovedDetailViewModel.BIsDetail = bIsDetail;
            AdvanceCapitalApprovedDetailViewModel.Model = SelectedItem;
            AdvanceCapitalApprovedDetailViewModel.Init();
            var view = new AdvanceCapitalApprovedDetail { DataContext = AdvanceCapitalApprovedDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _tonghopService.InsertTongHopNguonDauTu_Tang(LOAI_CHUNG_TU.KE_HOACH_VON_UNG, (int)TypeExecute.Delete, SelectedItem.Id);
            _keHoachVonUngService.DeleteKeHoachVonUng(_mapper.Map<VdtKhvKeHoachVonUng>(SelectedItem));
            LoadData();
        }
        public void OnFixData()
        {
            try
            {
                AdvanceCapitalApprovedDialogViewModel.Model = SelectedItem;
                AdvanceCapitalApprovedDialogViewModel.IsInsertDieuChinh = AdvanceCapitalApprovedDialogViewModel.Model.Id != Guid.Empty;
                AdvanceCapitalApprovedDialogViewModel.Init();
                AdvanceCapitalApprovedDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnOpenAdvanceCapitalApprovedDetail((VdtKhvKeHoachVonUngModel)obj);
                };
                var view = new AdvanceCapitalApprovedDialog
                {
                    DataContext = AdvanceCapitalApprovedDialogViewModel
                };
                DialogHost.Show(view, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
