using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PrintReport;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division.PrintReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.ExportReport;
using VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.Report;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu
{
    public class PhanBoDuToanThuIndexViewModel : GridViewModelBase<BhDtPhanBoChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly IDttBHXHPhanBoService _estimationService;
        private readonly ISessionService _sessionService;
        private readonly IBhDttNhanPhanBoMapService _dtChungTuMapService;
        private readonly IDttBHXHPhanBoChiTietService _dtChungTuChiTietService;
        private readonly IBhDmMucLucNganSachService _bhMucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly INsDonViService _donViService;
        private readonly IDanhMucService _danhMucService;
        private ICollectionView _dtChungTuView;
        private List<BhDtPhanBoChungTuChiTietModel> _listChungTuChiTiet;
        private SessionInfo _sessionInfo;


        //public override string FuncCode => NSFunctionCode.BUDGET_ESTIMATE_DIVISION;
        public override string GroupName => MenuItemContants.GROUP_THU;
        public override string Name => "Phân bổ DT thu BHXH, BHYT, BHTN";
        public override string Description => "Danh sách đợt phân bổ dự toán thu BHXH, BHYT, BHTN";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PhanBoDuToanThuIndex);
        public override PackIconKind IconKind => PackIconKind.AxisArrow;

        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        private bool _isLock;
        public bool IsLock
        {
            get => _isLock;
            set => SetProperty(ref _isLock, value);
        }
        public bool IsEnableLockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && !x.BKhoa);
        public bool IsEnableUnlockMultiple => Items.All(x => !x.Selected) ? false : Items.Any(x => x.Selected && x.BKhoa);
        public bool IsEnableLock
        {
            get
            {
                var listItemSelected = Items.Where(n => n.Selected).ToList();
                var result = false;
                if (LockStatusSelected != null && !LockStatusSelected.ValueItem.Equals("0") && listItemSelected.Count > 0)
                {
                    result = true;
                }
                else
                {
                    var lstSelectedKhoa = listItemSelected.Where(x => x.BKhoa).ToList();
                    var lstSelectedMo = listItemSelected.Where(x => !x.BKhoa).ToList();
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
        public bool HasParentAgency { get; set; }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
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
                OnPropertyChanged(nameof(IsEnableLock));
                if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
                {
                    IsLock = true;
                }
                else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
                {
                    IsLock = false;
                }
                if (_dtChungTuView != null)
                {
                    _dtChungTuView.Refresh();
                }
            }
        }

        public bool IsExportGridData
        {
            get
            {
                if (Items == null || !Items.Any())
                {
                    return false;
                }
                return Items.Any(item => item.Selected);
            }
        }

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get
            {
                if (Items == null || !Items.Any())
                {
                    return false;
                }

                var listItemFilter = Items;
                return !listItemFilter.Any() ? false : listItemFilter.All(item => item.Selected);
            }
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                if (Items != null)
                {
                    Items.ForAll(c => c.Selected = _isAllItemsSelected);
                }
            }
        }

        private bool _isOpenPrintPopup;
        public bool IsOpenPrintPopup
        {
            get => _isOpenPrintPopup;
            set => SetProperty(ref _isOpenPrintPopup, value);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsEnableLock));
        }

        public PhanBoDuToanThuDialogViewModel PhanBoDuToanThuDialogViewModel { get; set; }
        public PhanBoDuToanThuDetailViewModel PhanBoDuToanThuDetailViewModel { get; set; }
        public ExportPhanBoDuToanThuViewModel ExportPhanBoDuToanThuViewModel { get; set; }
        public SocialInsuranceDivisionEstimatePrintSheetViewModel SocialInsuranceDivisionEstimatePrintSheetViewModel { get; set; }
        public PrintPhuLucGiaoDuToanDuToanThuViewModel PrintPhuLucGiaoDuToanDuToanThuViewModel { get; set; }
        public RelayCommand ExportGridDataCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand PrintActionCommand { get; }
        public RelayCommand UploadFileCommand { get; }
        public TongHopThuChiViewModel TongHopThuChiViewModel { get; }
        public PrintPhuLucDuToanThuViewModel PrintPhuLucDuToanThuViewModel { get; }

        public PhanBoDuToanThuIndexViewModel(IDttBHXHPhanBoService estimationService,
            IBhDttNhanPhanBoMapService dtChungTuMapService,
            IDttBHXHPhanBoChiTietService dtChungTuChiTietService,
            IExportService exportService,
            IBhDmMucLucNganSachService nsMucLucNganSachService,
            INsDonViService donViService,
            ILog logger,
            IMapper mapper,
            IDanhMucService danhMucService,
            ISessionService sessionService,
            PhanBoDuToanThuDialogViewModel phanBoDuToanThuDialogViewModel,
            PhanBoDuToanThuDetailViewModel phanBoDuToanThuDetailViewModel,
            ExportPhanBoDuToanThuViewModel exportPhanBoDuToanThuViewModel,
            TongHopThuChiViewModel tongHopThuChiViewModel,
            SocialInsuranceDivisionEstimatePrintSheetViewModel socialInsuranceDivisionEstimatePrintSheetViewModel,
            PrintPhuLucDuToanThuViewModel printPhuLucDuToanThuViewModel,
            PrintPhuLucGiaoDuToanDuToanThuViewModel printPhuLucGiaoDuToanDuToanThuViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _estimationService = estimationService;
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _exportService = exportService;
            _sessionService = sessionService;
            _bhMucLucNganSachService = nsMucLucNganSachService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            PhanBoDuToanThuDialogViewModel = phanBoDuToanThuDialogViewModel;
            PhanBoDuToanThuDetailViewModel = phanBoDuToanThuDetailViewModel;
            ExportPhanBoDuToanThuViewModel = exportPhanBoDuToanThuViewModel;
            TongHopThuChiViewModel = tongHopThuChiViewModel;
            PrintPhuLucDuToanThuViewModel = printPhuLucDuToanThuViewModel;
            ExportGridDataCommand = new RelayCommand(obj => OnExportData());
            PrintCommand = new RelayCommand(obj => OnPrint(obj));
            SocialInsuranceDivisionEstimatePrintSheetViewModel = socialInsuranceDivisionEstimatePrintSheetViewModel;
            PrintPhuLucGiaoDuToanDuToanThuViewModel = printPhuLucGiaoDuToanDuToanThuViewModel;
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

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
            LoadLockStatus();
            CheckParentAgency();
        }

        protected override void OnDelete()
        {
            base.OnDelete();

            if (SelectedItem.SNguoiTao != _sessionService.Current.Principal)
            {
                System.Windows.MessageBox.Show(string.Format(Resources.MsgRoleDelete, SelectedItem.SNguoiTao), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.DeleteChungTu, SelectedItem.SSoChungTu, SelectedItem.DNgayChungTu.HasValue ? SelectedItem.DNgayChungTu.Value.ToString("dd/MM/yyyy") : string.Empty);
            MessageBoxResult result = MessageBox.Show(messageBuilder.ToString(), Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                DeleteSelectedVoucher();
        }

        private void DeleteSelectedVoucher()
        {
            _dtChungTuMapService.DeleteByIdPhanBoDuToan(SelectedItem.Id);
            _dtChungTuChiTietService.DeleteByIdChungTu(SelectedItem.Id);
            _estimationService.Delete(SelectedItem.Id);
            MessageBox.Show(Resources.MsgDeleteSuccess, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            OnRefresh();
        }

        protected override void OnLockUnLock()
        {
            if (IsLock)
            {
                var listSoChungTu = string.Join(", ", Items.Where(n => n.Selected && n.BKhoa).Select(n => n.SSoChungTu));
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBox.Show(Resources.MsgRoleUnlock, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                var listSoChungTuInvalid = string.Join(", ", Items.Where(n => n.Selected && n.SNguoiTao != _sessionInfo.Principal && !n.BKhoa).Select(n => n.SSoChungTu));

                if (!string.IsNullOrEmpty(listSoChungTuInvalid))
                {
                    MessageBox.Show(string.Format(Resources.MsgLockDeny, listSoChungTuInvalid), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
            string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            MessageBoxResult result = MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);

            var lstChungTuChon = Items.Where(n => n.Selected).ToList();
            if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("1"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => x.BKhoa).ToList();
            }
            else if (_lockStatusSelected != null && _lockStatusSelected.ValueItem.Equals("2"))
            {
                lstChungTuChon = lstChungTuChon.Where(x => !x.BKhoa).ToList();
            }

            if (result == MessageBoxResult.Yes)
            {
                foreach (var SelectedItemElement in lstChungTuChon)
                {
                    var rs = _estimationService.LockOrUnLock(SelectedItemElement.Id, !SelectedItemElement.BKhoa);
                    if (rs == DBContextSaveChangeState.SUCCESS)
                    {
                        SelectedItemElement.BKhoa = !SelectedItemElement.BKhoa;
                        OnPropertyChanged(nameof(IsLock));
                        OnPropertyChanged(nameof(IsEdit));
                    }
                }
                MessageBoxHelper.Info(msgDone);
                LockStatusSelected = LockStatus.ElementAt(0);
            }
        }

        private void LoadData()
        {
            Dictionary<string, string> dictIdChungTu = new Dictionary<string, string>();
            DuToanThuChungTuCriteria condition = new DuToanThuChungTuCriteria
            {
                EstimationType = SoChungTuType.EstimateDivision,
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                Status = (int)Status.ACTIVE,
                UserName = _sessionInfo.Principal,
            };
            var listChungTu = _estimationService.FindByCondition(condition).ToList();
            Items = _mapper.Map<ObservableCollection<BhDtPhanBoChungTuModel>>(listChungTu);

            _dtChungTuView = CollectionViewSource.GetDefaultView(Items);
            _dtChungTuView.Filter = VoucherFilter;

            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            foreach (var item in Items)
            {
                dictIdChungTu = _estimationService.FindAllDict(_sessionInfo.YearOfWork, item.ILoaiDuToan);
                item.ListSoChungTuDotNhan = string.Join(",", item.IIdDotNhan.Split(",").Select(x => dictIdChungTu.GetValueOrDefault(x, string.Empty)));
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(BhDtPhanBoChungTuModel.Selected))
                    {
                        OnPropertyChanged(nameof(IsExportGridData));
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                        OnPropertyChanged(nameof(IsEnableLock));
                        OnPropertyChanged(nameof(IsEnableLockMultiple));
                        OnPropertyChanged(nameof(IsEnableUnlockMultiple));
                    }
                };
            }
        }

        private bool VoucherFilter(object obj)
        {
            bool result = true;
            var item = (BhDtPhanBoChungTuModel)obj;

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("1"))
            {
                result = result && item.BKhoa;
            }

            if (LockStatusSelected != null && LockStatusSelected.ValueItem.Equals("2"))
            {
                result = result && !item.BKhoa;
            }


            return result;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnAdd()
        {
            //check quyền được tạo mới
            List<DonVi> userAgency = _donViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
            if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
            {
                MessageBox.Show(Resources.MsgRoleAdd, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PhanBoDuToanThuDialogViewModel.Model = new BhDtPhanBoChungTuModel();
            PhanBoDuToanThuDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            PhanBoDuToanThuDialogViewModel.Init();
            PhanBoDuToanThuDialogViewModel.SavedAction = obj =>
            {
                if (obj.GetType().Equals(typeof(BhDtPhanBoChungTuModel)))
                {
                    OnRefresh();
                    OnOpenDivisionDetail((BhDtPhanBoChungTuModel)obj);
                }
            };
            var view = new PhanBoDuToanThuDialog
            {
                DataContext = PhanBoDuToanThuDialogViewModel
            };
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            PhanBoDuToanThuDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };

        }

        protected override void OnUpdate()
        {
            //check quyền được chỉnh sửa
            if (SelectedItem.SNguoiTao != _sessionInfo.Principal)
            {
                MessageBox.Show(string.Format(Resources.MsgRoleUpdate, SelectedItem.SNguoiTao), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PhanBoDuToanThuDialogViewModel.Model = SelectedItem;
            PhanBoDuToanThuDialogViewModel.RefreshVoucherEvent += (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
            PhanBoDuToanThuDialogViewModel.Init();
            PhanBoDuToanThuDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
            };
            var view = new PhanBoDuToanThuDialog { DataContext = PhanBoDuToanThuDialogViewModel };

            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
            PhanBoDuToanThuDialogViewModel.RefreshVoucherEvent -= (object sender, EventArgs e) =>
            {
                OnRefresh();
            };
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((BhDtPhanBoChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(BhDtPhanBoChungTuModel selectEstimation)
        {
            PhanBoDuToanThuDetailViewModel.Model = selectEstimation;
            PhanBoDuToanThuDetailViewModel.UpdateVoucherEvent += RefreshAfterSaveData;
            PhanBoDuToanThuDetailViewModel.Init();
            var view = new PhanBoDuToanThuDetail { DataContext = PhanBoDuToanThuDetailViewModel };
            view.ShowDialog();
            PhanBoDuToanThuDetailViewModel.UpdateVoucherEvent -= RefreshAfterSaveData;
        }

        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            BhDtPhanBoChungTuModel model = (BhDtPhanBoChungTuModel)sender;
            BhDtPhanBoChungTuModel item = Items.Where(x => x.Id == model.Id).FirstOrDefault();
            if (item != null)
            {
                item.FThuBHXHNLD = model.FThuBHXHNLD;
                item.FThuBHXHNSD = model.FThuBHXHNSD;
                item.FTongBHXH = model.FTongBHXH;
                item.FThuBHYTNLD = model.FThuBHYTNLD;
                item.FThuBHYTNSD = model.FThuBHYTNSD;
                item.FTongBHYT = model.FTongBHYT;
                item.FThuBHTNNLD = model.FThuBHTNNLD;
                item.FThuBHTNNSD = item.FThuBHTNNSD;
                item.FTongBHTN = item.FTongBHTN;
                item.FTongDuToan = item.FTongDuToan;
            }
            OnPropertyChanged(nameof(item.FTongDuToan));
            OnPropertyChanged(nameof(IsEdit));
        }

        private void OnExportData()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = ExportFileName.EXPORT_BH_DTT_BHXH_CHUNGTU_CHITIET;
                    var namLamViec = _sessionService.Current.YearOfWork;
                    var listBhDmMucLucNganSach = _bhMucLucNganSachService.FindAllByYear(namLamViec).Where(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN)).ToList();
                    var itemsExport = Items.Where(x => x.Selected);
                    var dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.SDsidMaDonVi)), namLamViec)
                        .GroupBy(x => x.IIDMaDonVi)
                        .ToDictionary(x => x.Key, x => x.First());
                    if (listBhDmMucLucNganSach.Any())
                        listBhDmMucLucNganSach.RemoveAt(0);

                    FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
                    foreach (var item in itemsExport)
                    {
                        var dataExportDetail = LoadDataExportDetail(item).OrderBy(s => s.SXauNoiMa).ToList();
                        var listDonVi = item.SDsidMaDonVi.Split(",");
                        var listMLNS = _bhMucLucNganSachService.FindByListLnsDonVi(item.SDslns, _sessionService.Current.YearOfWork).ToList();

                        foreach (var idDonVi in listDonVi)
                        {
                            var data = new Dictionary<string, object>();
                            var tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;
                            data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                            data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                            data.Add("TitleFirst", $"Dự toán thu BHXH, BHYT, BHTN năm {_sessionService.Current.YearOfWork}");
                            data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");
                            data.Add("FormatNumber", formatNumber);
                            data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                            data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                            data.Add("SoChungTu", item.SSoChungTu);
                            data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                            data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                            data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                            data.Add("MoTa", item.SMoTa);
                            data.Add("LoaiDuToan", VoucherType.BudgetTypeDict.GetValueOrDefault(item.ILoaiDuToan, string.Empty));
                            data.Add("NguoiTao", item.SNguoiTao);
                            data.Add("NgayTao", DateUtils.Format(item.DNgayTao));

                            var listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IIdMaDonVi)).ToList();
                            CalculateData(listData);
                            var chungTu = CalculateTotal(listData);
                            var lstQueryData = listData.Where(CheckIsHasData).ToList();
                            var listDataExport = _mapper.Map<ObservableCollection<BhDtPhanBoChungTuChiTietQuery>>(lstQueryData).ToList();
                            if (listDataExport.Any())
                                listDataExport.RemoveAt(0);
                            foreach (var row in listDataExport)
                            {
                                row.FBHXHNLD = Math.Round(row.FBHXHNLD.GetValueOrDefault());
                                row.FBHXHNSD = Math.Round(row.FBHXHNSD.GetValueOrDefault());
                                row.FThuBHXH = Math.Round(row.FThuBHXH.GetValueOrDefault());
                                row.FBHYTNLD = Math.Round(row.FBHYTNLD.GetValueOrDefault());
                                row.FBHYTNSD = Math.Round(row.FBHYTNSD.GetValueOrDefault());
                                row.FThuBHYT = Math.Round(row.FThuBHYT.GetValueOrDefault());
                                row.FBHTNNLD = Math.Round(row.FBHTNNLD.GetValueOrDefault());
                                row.FBHTNNSD = Math.Round(row.FBHTNNSD.GetValueOrDefault());
                                row.FThuBHTN = Math.Round(row.FThuBHTN.GetValueOrDefault());
                                row.FTongCong = Math.Round(row.FTongCong.GetValueOrDefault());
                            }
                            data.Add("Items", listDataExport);
                            data.Add("MLNS", listBhDmMucLucNganSach);
                            data.Add("TotalBHXHNLD", Math.Round(chungTu.FThuBHXHNLD.GetValueOrDefault()));
                            data.Add("TotalBHYTNLD", Math.Round(chungTu.FThuBHYTNLD.GetValueOrDefault()));
                            data.Add("TotalBHTNNLD", Math.Round(chungTu.FThuBHTNNLD.GetValueOrDefault()));
                            data.Add("TotalBHXHNSD", Math.Round(chungTu.FThuBHXHNSD.GetValueOrDefault()));
                            data.Add("TotalBHYTNSD", Math.Round(chungTu.FThuBHYTNSD.GetValueOrDefault()));
                            data.Add("TotalBHTNNSD", Math.Round(chungTu.FThuBHTNNSD.GetValueOrDefault()));
                            data.Add("TotalBHXH", Math.Round(chungTu.FTongBHXH.GetValueOrDefault()));
                            data.Add("TotalBHYT", Math.Round(chungTu.FTongBHYT.GetValueOrDefault()));
                            data.Add("TotalBHTN", Math.Round(chungTu.FTongBHTN.GetValueOrDefault()));
                            data.Add("Total", Math.Round(chungTu.FTongDuToan.GetValueOrDefault()));

                            List<int> hideColumns = new List<int>();
                            var xlsFile = _exportService.Export<BhDtPhanBoChungTuChiTietQuery, BhDtPhanBoChungTuChiTietModel, BhDmMucLucNganSach>(templateFileName, data);
                            var nameRange = xlsFile.GetNamedRange(1);
                            nameRange.Comment = "Workbook";
                            xlsFile.SetNamedRange(nameRange);
                            xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                            xlsFile.SetCellValue(50, 50, "CheckSum");
                            xlsFile.SetRowHidden(50, true);
                            tenDonVi = tenDonVi ?? "DonVi";
                            string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(tenDonVi));
                            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                            results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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


        

        private void CheckParentAgency()
        {
            HasParentAgency = false;
            List<DonVi> donVis = _donViService.GetDanhSachDonViByNguoiDung(_sessionInfo.Principal, _sessionInfo.YearOfWork);
            if (donVis.Any(x => x.Loai == LoaiDonVi.ROOT))
                HasParentAgency = true;
        }

        private void OnPrint(object param)
        {
            var divisionPrintType = (SocialInsuranceDivisionEstimatePrintType)((int)param);
            object view = null;
            switch (divisionPrintType)
            {
                case SocialInsuranceDivisionEstimatePrintType.COVER_SHEET:
                case SocialInsuranceDivisionEstimatePrintType.DU_TOAN_THU_CHI_TONG_HOP:
                    TongHopThuChiViewModel.ReportNameTypeValue = (int)divisionPrintType;
                    TongHopThuChiViewModel.ReportTypeValue = divisionPrintType;
                    TongHopThuChiViewModel.Init();
                    view = new TongHopThuChi
                    {
                        DataContext = TongHopThuChiViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.TARGET_AGENCY:
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Models = new ObservableCollection<DtChungTuModel>();
                    SocialInsuranceDivisionEstimatePrintSheetViewModel.Init();
                    view = new PhanBoDuToanThuPrintReport
                    {
                        DataContext = SocialInsuranceDivisionEstimatePrintSheetViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.APPENDIX:
                    PrintPhuLucDuToanThuViewModel.Init();
                    view = new PrintPhuLucDuToanThu
                    {
                        DataContext = PrintPhuLucDuToanThuViewModel
                    };
                    break;
                case SocialInsuranceDivisionEstimatePrintType.DELIVER:
                    PrintPhuLucGiaoDuToanDuToanThuViewModel.Init();
                    view = new PrintPhuLucGiaoDuToanDuToanThu
                    {
                        DataContext = PrintPhuLucGiaoDuToanDuToanThuViewModel
                    };
                    break;
                default:
                    view = null;
                    break;
            }            
            DialogHost.Show(view, SystemConstants.ROOT_DIALOG, null, null);
        }

        private List<BhDtPhanBoChungTuChiTietModel> LoadDataExportDetail(BhDtPhanBoChungTuModel item)
        {
            var searchCondition = new DuToanThuChungTuChiTietCriteria
            {
                VoucherId = item.Id,
                LNS = item.SDslns,
                YearOfWork = item.INamLamViec,
                VoucherDate = item.DNgayChungTu,
                IdDotNhan = item.IIdDotNhan,
                SoChungTu = item.SSoChungTu
            };

            if (item.ILoaiDuToan.HasValue && EstimateTypeNum.ADJUSTED.Equals((EstimateTypeNum)item.ILoaiDuToan.Value))
            {
                var listNhanPhanBo = LoadNhanPhanBo(item.Id.ToString());
                searchCondition.LNS = string.Join(",", listNhanPhanBo.Select(x => x.SDslns));
            }

            var listChungTuChiTiet = _dtChungTuChiTietService.FindByCondition(searchCondition).ToList();
            _listChungTuChiTiet = _mapper.Map<List<BhDtPhanBoChungTuChiTietModel>>(listChungTuChiTiet);
            return _listChungTuChiTiet;
        }

        private IEnumerable<BhDtPhanBoChungTu> LoadNhanPhanBo(string idPhanBoDuToan)
        {
            var dtChungTuMapByIdPhanBoDuToan = _dtChungTuMapService.FindByIdPhanBoDuToan(idPhanBoDuToan).ToList();
            var listIdNhanPhanBo = dtChungTuMapByIdPhanBoDuToan.Select(e => e.IIdCtduToanNhan.ToString()).ToHashSet();

            var listNhanPhanBo = new List<BhDtPhanBoChungTu>();
            if (dtChungTuMapByIdPhanBoDuToan.Count() > 0)
            {
                var predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
                predicate = predicate.And(x => listIdNhanPhanBo.Contains(x.Id.ToString()));
                listNhanPhanBo = _estimationService.FindByCondition(predicate).OrderBy(x => x.DNgayChungTu).ToList();
            }
            return listNhanPhanBo;
        }

        private void CalculateData(List<BhDtPhanBoChungTuChiTietModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FBHXHNLD = 0;
                    x.FBHXHNSD = 0;
                    x.FBHYTNLD = 0;
                    x.FBHYTNSD = 0;
                    x.FBHTNNLD = 0;
                    x.FBHTNNSD = 0;
                    return x;
                }).ToList();

            foreach (var item in listData.Where(x => x.IsEditable && (x.HasDataChild)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhDtPhanBoChungTuChiTietModel> listData, BhDtPhanBoChungTuChiTietModel currentItem, BhDtPhanBoChungTuChiTietModel seftItem)
        {
            var parrentItem = listData.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
            parrentItem.FBHXHNLD += seftItem.FBHXHNLD;
            parrentItem.FBHXHNSD += seftItem.FBHXHNSD;
            parrentItem.FBHYTNLD += seftItem.FBHYTNLD;
            parrentItem.FBHYTNSD += seftItem.FBHYTNSD;
            parrentItem.FBHTNNLD += seftItem.FBHTNNLD;
            parrentItem.FBHTNNSD += seftItem.FBHTNNSD;
            CalculateParent(listData, parrentItem, seftItem);
        }

        private BhDtPhanBoChungTuModel CalculateTotal(List<BhDtPhanBoChungTuChiTietModel> listData)
        {
            BhDtPhanBoChungTuModel chungTu = new BhDtPhanBoChungTuModel();
            chungTu.FThuBHXHNLD = 0;
            chungTu.FThuBHXHNSD = 0;
            chungTu.FTongBHXH = 0;
            chungTu.FThuBHYTNLD = 0;
            chungTu.FThuBHYTNSD = 0;
            chungTu.FTongBHYT = 0;
            chungTu.FThuBHTNNLD = 0;
            chungTu.FThuBHTNNSD = 0;
            chungTu.FTongBHTN = 0;
            var listChildren = listData.Where(x => x.IsEditable).ToList();
            foreach (var item in listChildren)
            {
                chungTu.FThuBHXHNLD += item.FBHXHNLD.GetValueOrDefault();
                chungTu.FThuBHXHNSD += item.FBHXHNSD.GetValueOrDefault();
                chungTu.FTongBHXH += item.FThuBHXH.GetValueOrDefault();
                chungTu.FThuBHYTNLD += item.FBHYTNLD.GetValueOrDefault();
                chungTu.FThuBHYTNSD += item.FBHYTNSD.GetValueOrDefault();
                chungTu.FTongBHYT += item.FThuBHYT.GetValueOrDefault();
                chungTu.FThuBHTNNLD += item.FBHTNNLD.GetValueOrDefault();
                chungTu.FThuBHTNNSD += item.FBHTNNSD.GetValueOrDefault();
                chungTu.FTongBHTN += item.FThuBHTN.GetValueOrDefault();
            }

            return chungTu;
        }

        private bool CheckIsHasData(BhDtPhanBoChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.HasDataChild;
        }
    }
}
