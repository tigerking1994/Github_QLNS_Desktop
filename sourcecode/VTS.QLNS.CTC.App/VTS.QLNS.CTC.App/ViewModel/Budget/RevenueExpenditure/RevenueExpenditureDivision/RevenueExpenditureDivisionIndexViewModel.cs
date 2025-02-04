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
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RevenueExpenditureDivision;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RevenueExpenditureSettlement;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Import;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureDivision;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Net;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.Plan.PrintPlanReport;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureSettlement
{
    public class RevenueExpenditureDivisionIndexViewModel : GridViewModelBase<TnDtChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly INsMucLucNganSachService _mucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDonViService _donViService;
        private readonly ILog _logger;
        private ICollectionView _tnDtChungTuView;
        private Dictionary<Guid, TnDtChungTuChiTietModel> _dicItems;
        public override string Name => "Phân bổ dự toán";
        public override string Description => "Phân bổ dự toán";
        public override Type ContentType => typeof(RevenueExpenditureDivisionIndex);
        public override PackIconKind IconKind => PackIconKind.Buddhism;
        public override string FuncCode => NSFunctionCode.BUDGET_REVENUE_EXPENDITURE_DIVISION_INDEX;

        //public bool IsEdit => SelectedItem != null && !SelectedItem.IsLocked;
        // public bool IsLock => SelectedItem != null && SelectedItem.IsLocked;
        //public bool IsEnableLock => SelectedItem != null;
        public bool IsExportAggregateData => Items.Where(x => x.Selected).ToList().Count > 0 ? true : false;


        public bool IsLock => IsEnableLock && Items.FirstOrDefault(x => x.Selected).IsLocked;
        public bool IsEdit => Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Count() == NSConstants.DEFAULT_INDEX) && !Items.FirstOrDefault(x => x.Selected).IsLocked;
        public bool IsEnableLock => Items.Any(x => x.Selected) && (Items.Where(x => x.Selected).Select(s => s.IsLocked).Distinct().Count() == NSConstants.DEFAULT_INDEX);

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

        public RevenueExpenditureDivisionDialogViewModel RevenueExpenditureDivisionDialogViewModel { get; set; }
        public RevenueExpenditureDivisionDetailViewModel RevenueExpenditureDivisionDetailViewModel { get; set; }
        public RevenueExpenditureImportViewModel RevenueExpenditureImportViewModel { get; set; }
        public RevenueExpenditureDivisionReportViewModel RevenueExpenditureDivisionReportViewModel { get; set; }

        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportAggregateDataCommand { get; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand PrintActionCommand { get; set; }

        public RevenueExpenditureDivisionIndexViewModel(IMapper mapper,
            ISessionService sessionService,
            ITnDtChungTuService tnDtChungTuService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IExportService exportService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            IDanhMucService danhMucService,
            INsDonViService nsDonViService,
            ILog logger,
            RevenueExpenditureDivisionDialogViewModel revenueExpenditureDivisionDialogViewModel,
            RevenueExpenditureDivisionDetailViewModel revenueExpenditureDivisionDetailViewModel,
            RevenueExpenditureImportViewModel revenueExpenditureImportViewModel,
            RevenueExpenditureDivisionReportViewModel revenueExpenditureDivisionReportViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _tnDtChungTuService = tnDtChungTuService;
            _mucLucNganSachService = nsMucLucNganSachService;
            _exportService = exportService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
            _logger = logger;
            _danhMucService = danhMucService;
            _donViService = nsDonViService;

            RevenueExpenditureDivisionDialogViewModel = revenueExpenditureDivisionDialogViewModel;
            RevenueExpenditureDivisionDetailViewModel = revenueExpenditureDivisionDetailViewModel;
            RevenueExpenditureImportViewModel = revenueExpenditureImportViewModel;
            RevenueExpenditureDivisionReportViewModel = revenueExpenditureDivisionReportViewModel;
            ImportDataCommand = new RelayCommand(obj => OnImportData(obj));
            ExportAggregateDataCommand = new RelayCommand(obj => OnExportGridData());
            SearchCommand = new RelayCommand(obj => _tnDtChungTuView.Refresh());
            PrintActionCommand = new RelayCommand(obj => OnOpenReport());
        }

        public override void Init()
        {
            LoadLockStatus();
            LoadData();
            RevenueExpenditureDivisionDetailViewModel.UpdateSettlementVoucherEvent += RefreshAfterSaveData;
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
                Items = _mapper.Map<ObservableCollection<TnDtChungTuModel>>(listChungTu.OrderBy(x => x.NgayChungTu));

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
                            OnPropertyChanged(nameof(IsExportAggregateData));
                            OnPropertyChanged(nameof(IsEnableLock));
                            OnPropertyChanged(nameof(IsLock));
                            OnPropertyChanged(nameof(IsEdit));
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
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.DivisionEstimation);

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
            RevenueExpenditureImportViewModel.RevenueExpenditureImportTypes = (RevenueExpenditureImportType)((int)param);
            RevenueExpenditureImportViewModel.Init();
            RevenueExpenditureImportViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };

            var view = new RevenueExpenditureImport { DataContext = RevenueExpenditureImportViewModel };
            var result = view.ShowDialog();
        }


        private void OnOpenReport()
        {
            RevenueExpenditureDivisionReportViewModel.Init();
            var view = new RevenueExpenditureDivisionReport
            {
                DataContext = RevenueExpenditureDivisionReportViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
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

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName = RevenueExpenditureType.RPT_DT_PHAN_BO_CHUNG_TU_TONG_HOP;

                    List<TnDtChungTuModel> settlementVouchers = Items.Where(x => x.Selected).ToList();
                    foreach (var item in settlementVouchers)
                    {
                        List<TnDtChungTuChiTietQuery> settlementVoucherDetails = GetSettlementVoucherDetail(item);
                        List<NsMucLucNganSach> mucLucNganSaches = _mucLucNganSachService.FindAll(_sessionService.Current.YearOfWork).ToList();

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("SoChungTu", item.SoChungTu);
                        data.Add("NgayChungTu", item.NgayChungTu.ToString());
                        data.Add("DonVi", item.TenDonVi);
                        data.Add("TuChi", item.TuChiSum);
                        data.Add("LNS", item.Lns);
                        data.Add("SoQuyetDinh", item.SoQuyetDinh);
                        data.Add("NgayQuyetDinh", item.NgayQuyetDinh.ToString());
                        data.Add("MoTa", item.MoTaChiTiet);
                        data.Add("DotNhan", item.IdDotNhan);
                        data.Add("NguoiTao", item.UserCreator);
                        data.Add("NgayTao", item.DateCreated != null ? item.DateCreated.Value.ToString("dd/MM/yyyy") : string.Empty);
                        data.Add("Items", settlementVoucherDetails);
                        data.Add("MLNS", mucLucNganSaches);

                        var xlsFile = _exportService.Export<TnDtChungTuChiTietQuery, NsMucLucNganSach>(templateFileName, data);
                        string fileNamePrefix = item.SoChungTu;
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

        /// <summary>
        /// Xuất excel chứng từ Import
        /// </summary>
        /// 

        private void OnExportGridData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {

                string chiTietToi = "NG";
                //DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                //if (danhMucChiTietToi != null)
                //    chiTietToi = danhMucChiTietToi.SGiaTri;
                DanhMuc itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = "rpt_TN_DT_ChungTu.xlsx";

                int namLamViec = _sessionService.Current.YearOfWork;
                IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _mucLucNganSachService.FindAll(namLamViec);
                var itemsExport = Items.Where(x => x.Selected);
                Dictionary<string, DonVi> dictDonVi = _donViService.FindByListIdDonVi(string.Join(",", itemsExport.Select(x => x.IdDonVi)), namLamViec)
                    .GroupBy(x => x.IIDMaDonVi)
                    .ToDictionary(x => x.Key, x => x.First());

                int count = itemsExport.Select(x => x.IdDonVi.Split(",")).SelectMany(x => x).Count();
                foreach (TnDtChungTuModel item in itemsExport)
                {
                    List<TnDtChungTuChiTietModel> dataExportDetail = LoadDataExportDetail(item);
                    string[] listDonVi = item.IdDonVi.Split(",");

                    foreach (string idDonVi in listDonVi)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        string tenDonVi = dictDonVi.GetValueOrDefault(idDonVi, new DonVi()).TenDonVi;
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"DỰ TOÁN CHI NGÂN SÁCH NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SoChungTu}, ngày: {DateUtils.Format(item.NgayChungTu)})");

                        data.Add("HeaderTenDonVi", $"Đơn vị: {idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("TenDonVi", $"{idDonVi.PadLeft(3, '0')}{StringUtils.DIVISION}{tenDonVi}");
                        data.Add("SoChungTu", item.SoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.NgayChungTu));
                        data.Add("SoQuyetDinh", item.SoChungTu);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.NgayChungTu));
                        data.Add("MoTa", item.MoTaChiTiet);
                        data.Add("LoaiChungTu", VoucherType.VoucherTypeDict.GetValueOrDefault(item.LoaiChungTu ?? 0, string.Empty));
                        data.Add("NguoiTao", item.UserCreator);
                        data.Add("NgayTao", DateUtils.Format(item.DateCreated));

                        var listData = dataExportDetail.Where(x => x.IsHangCha || idDonVi.Equals(x.IdDonVi)).ToList();
                        var listDatagroups = listData.GroupBy(x => x.MlnsId).Select(x => new TnDtChungTuChiTietModel
                        {
                            Id = x.FirstOrDefault().Id,
                            MlnsId = x.FirstOrDefault().MlnsId,
                            MlnsIdParent = x.FirstOrDefault().MlnsIdParent,
                            XauNoiMa = x.FirstOrDefault().XauNoiMa,
                            Lns = x.FirstOrDefault().Lns,
                            L = x.FirstOrDefault().L,
                            K = x.FirstOrDefault().K,
                            M = x.FirstOrDefault().M,
                            TM = x.FirstOrDefault().TM,
                            TTM = x.FirstOrDefault().TTM,
                            Ng = x.FirstOrDefault().Ng,
                            Tng = x.FirstOrDefault().Tng,
                            Tng1 = x.FirstOrDefault().Tng1,
                            Tng2 = x.FirstOrDefault().Tng2,
                            Tng3 = x.FirstOrDefault().Tng3,
                            IsHangCha = x.LastOrDefault().IsHangCha,
                            TuChi = x.LastOrDefault().TuChi,
                            Loai = x.FirstOrDefault().Loai,
                            NamLamViec = x.FirstOrDefault().NamLamViec,
                            NamNganSach = x.FirstOrDefault().NamNganSach,
                            GhiChu = x.FirstOrDefault().GhiChu,
                            NguonNganSach = x.FirstOrDefault().NguonNganSach,
                            IdDonVi = x.FirstOrDefault().IdDonVi,
                            TenDonVi = x.FirstOrDefault().TenDonVi,
                            NoiDung = x.FirstOrDefault().NoiDung,
                            SChiTietToi = x.FirstOrDefault().SChiTietToi

                        }).ToList();
                        CalculateData(listDatagroups);
                        var listDataExport = listDatagroups.Where(CheckIsHasData).ToList();
                        data.Add("Items", listDataExport);
                        data.Add("MLNS", listNsMucLucNganSach);
                        data.Add("FTotalTuChi", listDataExport.Where(x => !x.IsHangCha).Sum(f => f.TuChi));


                        List<int> hideColumns = new List<int>();
                        chiTietToi = DynamicMLNS.GetMaxNameColumnByChiTietToi(listDataExport.Select(x => x.SChiTietToi).Distinct().ToList());
                        hideColumns.AddRange(ExportExcelHelper<DuToanChiTieuToBia>.HideColumn(chiTietToi));
                        FlexCel.Core.ExcelFile xlsFile = _exportService.Export<TnDtChungTuChiTietModel, NsMucLucNganSach>(templateFileName, data, hideColumns.Select(x => x + 3).ToList());
                        FlexCel.Core.TXlsNamedRange nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("{0}_{1}", item.SoChungTu, StringUtils.ConvertVN(tenDonVi));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }
                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    List<ExportResult> result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            }, (s, e) =>
            {
            });
        }


        private bool CheckIsHasData(TnDtChungTuChiTietModel chiTietModel)
        {
            return chiTietModel.TuChi != 0;
        }

        private List<TnDtChungTuChiTietModel> LoadDataExportDetail(TnDtChungTuModel item)
        {
            var condition = GetCondition(item);
            return _mapper.Map<List<TnDtChungTuChiTietModel>>(_tnDtChungTuChiTietService.FindByRevenueExpendDivisionCondition(condition).ToList());
        }

        private EstimationVoucherDetailCriteria GetCondition(TnDtChungTuModel item)
        {

            var condition = new EstimationVoucherDetailCriteria
            {
                VoucherId = item.Id,
                LNS = item.Lns,
                YearOfWork = item.NamLamViec,
                YearOfBudget = item.NamNganSach,
                BudgetSource = item.NguonNganSach,
                VoucherDate = item.NgayChungTu,
                IdDotNhan = item.IdDotNhan,
                SoChungTu = item.SoChungTu,
                DateCreated = item.DateCreated
            };
            return condition;
        }

        private void CalculateParent(TnDtChungTuChiTietModel currentItem, TnDtChungTuChiTietModel seftItem)
        {
            var model = _dicItems.ContainsKey(currentItem.MlnsIdParent ?? Guid.NewGuid()) ? _dicItems[currentItem.MlnsIdParent.Value] : null;
            if (model == null) return;
            model.TuChi += seftItem.TuChi;
            model.TuChiNhanPhanBo += seftItem.TuChiNhanPhanBo;
            CalculateParent(model, seftItem);
        }

        private void CalculateData(List<TnDtChungTuChiTietModel> items)
        {
            items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0;
                    // x.TuChiNhanPhanBo = 0;
                    return x;
                }).ToList();
            var itemsChild = items.Where(x => !x.IsHangCha && x.TuChi != 0).ToList();
            _dicItems = items.ToDictionary(key => key.MlnsId ?? Guid.NewGuid(), value => value);

            foreach (var item in itemsChild)
            {
                CalculateParent(item, item);
            }
        }

        private List<TnDtChungTuChiTietQuery> GetSettlementVoucherDetail(TnDtChungTuModel settlementVoucher)
        {
            EstimationVoucherDetailCriteria searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = settlementVoucher.Id,
                LNS = settlementVoucher.Lns,
                YearOfWork = settlementVoucher.NamLamViec,
                YearOfBudget = settlementVoucher.NamNganSach,
                BudgetSource = settlementVoucher.NguonNganSach,
                VoucherDate = settlementVoucher.NgayChungTu,
                IdDotNhan = settlementVoucher.IdDotNhan,
                SoChungTu = settlementVoucher.SoChungTu
            };

            List<TnDtChungTuChiTietQuery> _listChungTuChiTiet = _tnDtChungTuChiTietService.FindByRevenueExpendDivisionCondition(searchCondition).ToList();
            _listChungTuChiTiet = _listChungTuChiTiet.Where(x => !x.BHangCha && x.TuChi != 0).ToList();
            return _mapper.Map<List<TnDtChungTuChiTietQuery>>(_listChungTuChiTiet);
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
            foreach (var item in Items.Where(x => x.Selected))
            {
                var rs = _tnDtChungTuService.LockOrUnLock(item.Id, !item.IsLocked);

            }
            //if (rs == DBContextSaveChangeState.SUCCESS)
            //{
            SelectedItem.IsLocked = !SelectedItem.IsLocked;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(Items));
            OnRefresh();
            //}
        }

        protected override void OnAdd()
        {
            RevenueExpenditureDivisionDialogViewModel.Model = new TnDtChungTuModel();
            RevenueExpenditureDivisionDialogViewModel.Init();
            RevenueExpenditureDivisionDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };

            var view = new RevenueExpenditureDivisionDialog
            {
                DataContext = RevenueExpenditureDivisionDialogViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnUpdate()
        {
            RevenueExpenditureDivisionDialogViewModel.Model = SelectedItem;
            RevenueExpenditureDivisionDialogViewModel.Init();
            RevenueExpenditureDivisionDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDivisionDetail((TnDtChungTuModel)obj);
            };

            var view = new RevenueExpenditureDivisionDialog
            {
                DataContext = RevenueExpenditureDivisionDialogViewModel
            };
            DialogHost.Show(view, ROOT_DIALOG);
        }

        protected override void OnDelete()
        {
            try
            {
                base.OnDelete();
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

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDivisionDetail((TnDtChungTuModel)obj);
        }

        private void OnOpenDivisionDetail(TnDtChungTuModel SelectedItem)
        {
            RevenueExpenditureDivisionDetailViewModel.Model = SelectedItem;
            RevenueExpenditureDivisionDetailViewModel.Init();
            var view = new RevenueExpenditureDivisionDetail { DataContext = RevenueExpenditureDivisionDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
        }
    }
}
