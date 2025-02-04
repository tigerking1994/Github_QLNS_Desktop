using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.CollectionsBudget.BudgetApprobation;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.ApprovedEstimation;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.ApprovedEstimation;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain.Query;
using static VTS.QLNS.CTC.Core.Domain.Query.NsQtCongKhaiThuChi;
using VTS.QLNS.CTC.App.Model.Report;
using System.IO;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.CollectionsBudget.BudgetApprobation
{
    public class ApprovedEstimationIndexViewModel : GridViewModelBase<TnDtChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly IExportService _exportService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ILog _logger;
        private ICollectionView _tnDtChungTuView;

        public override string Name => "Nhận dự toán";
        public override string Description => "Nhận dự toán";
        public override Type ContentType => typeof(ApprovedEstimationIndex);
        public override PackIconKind IconKind => PackIconKind.Approve;
        public override string FuncCode => NSFunctionCode.BUDGET_REVENUE_EXPENDITURE_APPROVE_ESTIMATION;

       // public bool IsEdit => SelectedItem != null && !SelectedItem.IsLocked;
       // public bool IsLock => SelectedItem != null && SelectedItem.IsLocked;
       // public bool IsEnableLock => SelectedItem != null;

        public bool IsLock => IsEnableLock && Items.FirstOrDefault(x => x.Selected).IsLocked;
        public bool IsEdit => Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Count() == NSConstants.DEFAULT_INDEX) && !Items.FirstOrDefault(x => x.Selected).IsLocked;
        public bool IsEnableLock => Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Select(s => s.IsLocked).Distinct().Count() == NSConstants.DEFAULT_INDEX);

        public bool IsExportAggregateData => Items.Where(x => x.Selected).ToList().Count > 0 ? true : false;

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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEnableLock));
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
                OnRefresh();
            }
        }

        private List<TnDtChungTuChiTietModel> ItemDetails = new List<TnDtChungTuChiTietModel>();

        public ApprovedEstimationDialogViewModel ApprovedEstimationDialogViewModel { get; set; }
        public ApprovedEstimationDetailViewModel ApprovedEstimationDetailViewModel { get; set; }
        public RevenueExpenditureImportViewModel RevenueExpenditureImportViewModel { get; set; }
        public ApprovedEstimationImportViewModel ApprovedEstimationImportViewModel { get; set; }

        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand SearchCommand { get; set; }

        public ApprovedEstimationIndexViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnDtChungTuService tnDtChungTuService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            IExportService exportService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            ILog logger,
            ApprovedEstimationDialogViewModel approvedEstimationDialogViewModel,
            ApprovedEstimationDetailViewModel approvedEstimationDetailViewModel,
            RevenueExpenditureImportViewModel revenueExpenditureImportViewModel,
            ApprovedEstimationImportViewModel approvedEstimationImportViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _exportService = exportService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _danhMucService = danhMucService;
            _donViService = nsDonViService;
            _logger = logger;

            ApprovedEstimationDialogViewModel = approvedEstimationDialogViewModel;
            ApprovedEstimationDetailViewModel = approvedEstimationDetailViewModel;
            RevenueExpenditureImportViewModel = revenueExpenditureImportViewModel;
            ApprovedEstimationImportViewModel = approvedEstimationImportViewModel;

            ImportDataCommand = new RelayCommand(obj => OnImportData(obj));
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportAggregateData());
            SearchCommand = new RelayCommand(obj => _tnDtChungTuView.Refresh());
        }

        public override void Init()
        {
            LoadLockStatus();
            LoadData();
            ApprovedEstimationDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
        }

        private void LoadLockStatus()
        {
            var lockStatus = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "Tất cả", ValueItem = "0"},
                new ComboboxItem {DisplayItem = "Đã khóa", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "Chưa khóa", ValueItem = "2"},
            };

            LockStatus = new ObservableCollection<ComboboxItem>(lockStatus);
            LockStatusSelected = LockStatus.ElementAt(0);
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var predicate = CreatePredicate();
                List<TnDtChungTu> listChungTu = _tnDtChungTuService.FindByCondition(predicate).ToList();
                Items = _mapper.Map<ObservableCollection<TnDtChungTuModel>>(listChungTu);

                _tnDtChungTuView = CollectionViewSource.GetDefaultView(Items);
                _tnDtChungTuView.Filter = ListSettlementVoucherFilter;

                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }

                foreach (var model in Items)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TnDtChungTuModel.Selected))
                        {
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsEdit));
                            OnPropertyChanged(nameof(IsLock));
                            OnPropertyChanged(nameof(IsEnableLock));
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public Expression<Func<TnDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.ApprovedEstimation);

            return predicate;
        }

        private bool ListSettlementVoucherFilter(object obj)
        {
            bool result = true;
            var item = (TnDtChungTuModel)obj;
            if (!string.IsNullOrEmpty(SearchText))
                result = result && item.SoChungTu.ToLower().Contains(SearchText.ToLower());
            if (LockStatusSelected != null)
            {
                if (LockStatusSelected.ValueItem.Equals("1"))
                {
                    result = result && item.IsLocked;
                }
                if (LockStatusSelected.ValueItem.Equals("2"))
                {
                    result = result && item.IsLocked == false;
                }
            }
            item.IsFilter = result;
            return result;
        }
        private void RefreshAfterSaveData(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                TnDtChungTuModel item = Items.Where(x => x.Id == SelectedItem.Id).First();
                item.TuChiSum = ((TnDtChungTuModel)sender).TuChiSum;
            }

            this.OnRefresh();
        }

        private void OnImportData(object param)
        {
            //RevenueExpenditureImportViewModel.RevenueExpenditureImportTypes = (RevenueExpenditureImportType)((int)param);
            //RevenueExpenditureImportViewModel.Init();
            //RevenueExpenditureImportViewModel.SavedAction = obj =>
            //{
            //    this.OnRefresh();
            //    OnOpenDivisionDetail((TnDtChungTuModel)obj);
            //};

            //var view = new RevenueExpenditureImport { DataContext = RevenueExpenditureImportViewModel };
            //var result = view.ShowDialog();
            ApprovedEstimationImportViewModel.Init();
            ApprovedEstimationImportViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };

            var view = new ApprovedEstimationImport { DataContext = ApprovedEstimationImportViewModel };
            var result = view.ShowDialog();
        }

        /// <summary>
        /// Xuất excel chứng từ tổng hợp
        /// </summary>
        private void OnExportAggregateData()
        {
            try
            {            
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    int NamLamViec = _sessionService.Current.YearOfWork;
                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = GetTemplate(Path.GetFileNameWithoutExtension(RevenueExpenditureType.RPT_DT_CHUNG_TU_TONG_HOP));
                    DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, NamLamViec).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                    List<TnDtChungTuModel> settlementVouchers = Items.Where(x => x.Selected).ToList();
                    List<DonVi> lstDonVi = _donViService.FindByListIdDonVi(string.Join(",", settlementVouchers.Select(x => x.IdDonVi)), NamLamViec).ToList();

                    string chiTietToi = "NG";
                    foreach (var item in settlementVouchers)
                    {
                        List<TnDtChungTuChiTietModel> settlementVoucherDetails = GetSettlementVoucherDetail(item);
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(NamLamViec).ToList();
                        item.TenDonVi = lstDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(item.IdDonVi))?.TenDonVi;
                        Dictionary<string, object> data = new Dictionary<string, object>
                        {
                            { "Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "" },
                            { "Cap2", _sessionService.Current.TenDonVi.ToUpper() },
                            { "TitleFirst", $"NHẬN DỰ TOÁN NGÂN SÁCH NĂM {NamLamViec}" },
                            { "TitleSecond", $"(Kèm theo Quyết định số: {item.SoChungTu}, ngày: {DateUtils.Format(item.NgayChungTu)})" },

                            { "HeaderTenDonVi", $"Đơn vị: {item.IdDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{item.TenDonVi}" },
                            { "SoChungTu", item.SoChungTu },
                            { "NgayChungTu", item.NgayChungTu.ToString() },
                            { "DonVi", item.TenDonVi },
                            { "TuChi", item.TuChiSum },
                            { "LNS", item.Lns },
                            { "DotNhan", item.IdDotNhan },
                            { "SoQuyetDinh", item.SoQuyetDinh },
                            { "NgayQuyetDinh", item.NgayQuyetDinh.ToString() },
                            { "MoTa", item.MoTaChiTiet },
                            { "NguoiTao", item.UserCreator },
                            { "NgayTao", item.DateCreated != null ? item.DateCreated.Value.ToString("dd/MM/yyyy") : string.Empty },
                            { "Items", settlementVoucherDetails },
                            { "MLNS", mucLucNganSaches },
                            { "FTotalTuChi", settlementVoucherDetails.Where(x => !x.IsHangCha).Sum(s => s.TuChi) }
                        };
                        List<int> hideColumns = new List<int>();
                        chiTietToi = DynamicMLNS.GetMaxNameColumnByChiTietToi(settlementVoucherDetails.Select(x => x.SChiTietToi).Distinct().ToList());
                        hideColumns.AddRange(ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi));
                        var xlsFile = _exportService.Export<TnDtChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns.Select(x => x + 3).ToList());
                        string fileNamePrefix = $"{item.SoChungTu}_{item.TenDonVi}";
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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

        public string GetTemplate(string input)
        {
            return Path.Combine(ExportPrefix.PATH_TL_THUNOP_NGANSACH, input + FileExtensionFormats.Xlsx);
        }

        private List<TnDtChungTuChiTietModel> GetSettlementVoucherDetail(TnDtChungTuModel settlementVoucher)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = settlementVoucher.Id,
                LNS = settlementVoucher.Lns,
                YearOfWork = settlementVoucher.NamLamViec,
                YearOfBudget = settlementVoucher.NamNganSach,
                VoucherDate = settlementVoucher.NgayChungTu,
                BudgetSource = settlementVoucher.NguonNganSach
            };
            List<TnDtChungTuChiTietQuery> _listChungTuChiTiet = _tnDtChungTuChiTietService.FindByApprovedAndPlanEstimationCondition(searchCondition, RevenueAndExpenditureType.ApprovedEstimation).ToList();
            ItemDetails = _mapper.Map<List<TnDtChungTuChiTietModel>>(_listChungTuChiTiet);
            CalculateData();
            var results = ItemDetails.Where(x => x.IsHasData).OrderBy(o => o.XauNoiMa).ToList();
            return results;
        }


        private void CalculateData()
        {
            ItemDetails.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0;
                    return x;
                }).ToList();

            foreach (var item in ItemDetails.Where(x => x.IsEditable && (x.TuChi > 0)))
            {
                CalculateParent(item, item);
            }

        }

        private void CalculateParent(TnDtChungTuChiTietModel currentItem, TnDtChungTuChiTietModel seftItem)
        {
            var parrentItem = ItemDetails.Where(x => x.MlnsId == currentItem.MlnsIdParent).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.TuChi += seftItem.TuChi;
            CalculateParent(parrentItem, seftItem);
        }



        // <summary>
        /// Action when checkbox select all is selected
        /// </summary>
        /// <param name="select">true/false</param>
        /// <param name="models">items source of data grid</param>
        private static void SelectAll(bool select, IEnumerable<TnDtChungTuModel> models)
        {
            foreach (var model in models.Where(x => x.IsFilter))
            {
                model.Selected = select;
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string msgConfirm = string.Format(IsLock ? Resources.MsgConfirmUnLock : Resources.MsgConfirmLock, Environment.NewLine, Environment.NewLine);
                string msgDone = IsLock ? Resources.MsgUnLockDone : Resources.MsgLockDone;

                var messageBox = new NSMessageBoxViewModel(msgConfirm, "Xác nhận", NSMessageBoxButtons.YesNo, LockConfirmEventHandler);
                DialogHost.Show(messageBox.Content, "RootDialog");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            // var rs = _tnDtChungTuService.LockOrUnLock(SelectedItem.Id, !SelectedItem.IsLocked);
            foreach (var item in Items.Where(x => x.Selected))
            {
                var rs = _tnDtChungTuService.LockOrUnLock(item.Id, !item.IsLocked);

            }
            SelectedItem.IsLocked = !SelectedItem.IsLocked;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
            OnRefresh();

            //if (rs == DBContextSaveChangeState.SUCCESS)
            //{
            //    SelectedItem.IsLocked = !SelectedItem.IsLocked;
            //    OnPropertyChanged(nameof(IsLock));
            //    OnPropertyChanged(nameof(IsEdit));
            //}
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((TnDtChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(TnDtChungTuModel SelectedItem)
        {
            ApprovedEstimationDetailViewModel.Model = SelectedItem;
            ApprovedEstimationDetailViewModel.Init();
            var view = new ApprovedEstimationDetail { DataContext = ApprovedEstimationDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
        }

        protected override void OnAdd()
        {
            ApprovedEstimationDialogViewModel.Model = new TnDtChungTuModel();
            ApprovedEstimationDialogViewModel.Init();
            ApprovedEstimationDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };

            var view = new ApprovedEstimationDialog
            {
                DataContext = ApprovedEstimationDialogViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            ApprovedEstimationDialogViewModel.Model = SelectedItem;
            ApprovedEstimationDialogViewModel.Init();
            ApprovedEstimationDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };
            var view = new ApprovedEstimationDialog
            {
                DataContext = ApprovedEstimationDialogViewModel
            };

            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnDelete()
        {
            try
            {
                base.OnDelete();
                if (_tnDtChungTuService.CheckDeletePhanBo(SelectedItem.Id))
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendFormat(Resources.MsgRejectDeleteVoucher);
                    var messageBox1 = new NSMessageBoxViewModel(message.ToString());
                    ///MessageBoxHelper.Warning(Resources.MsgRejectDeleteVoucher);
                    DialogHost.Show(messageBox1.Content, "RootDialog");

                    return;
                }
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat(Resources.MsgRevDeleteChungTu, SelectedItem.SoChungTu, SelectedItem.SoQuyetDinh, SelectedItem.NgayChungTu);
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
            if (result != NSDialogResult.Yes) return;
            _tnDtChungTuService.Delete(SelectedItem.Id);

            var itemDeleted = Items.Where(x => x.Id == SelectedItem.Id).First();
            Items.Remove(itemDeleted);
        }
    }
}
