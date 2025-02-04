using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.ViewModel.Category;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Report
{
    public class ReportDivisionCurrentBatchDialogViewModel : ViewModelBase
    {
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly IExportService _exportService;
        private readonly IDmChuKyService _dmChuKyService;
        private ICollectionView _dataLNSView;
        private EstimationVoucherDetailCriteria _searchCondition;
        private DtChungTuModel _dtChungTuModel;
        private SessionInfo _sessionInfo;
        private DmChuKyDialogViewModel DmChuKyDialogViewModel;
        private IDanhMucService _danhMucService;
        private string _cap1;
        private DmChuKy _dmChuKy;

        public override string Name => "In báo cáo - Số phân cấp hiện tại";
        public override Type ContentType => typeof(View.Budget.Estimate.Report.ReportDivisionCurrentBatchDialog);
        public override PackIconKind IconKind => PackIconKind.Printer;
        public bool TypePrintSelected { get; set; }

        private ObservableCollection<DtChungTuChiTietModel> _chungTuChiTietItems;
        public ObservableCollection<DtChungTuChiTietModel> ChungTuChiTietItems
        {
            get => _chungTuChiTietItems;
            set => SetProperty(ref _chungTuChiTietItems, value);
        }

        private ObservableCollection<ComboboxManyItem> _dataDot;
        public ObservableCollection<ComboboxManyItem> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ComboboxManyItem _selectedDataDot;
        public ComboboxManyItem SelectedDataDot
        {
            get => _selectedDataDot;
            set
            {
                SetProperty(ref _selectedDataDot, value);
                GetListChungTuLuyKe();
                ResetConditionSearch();
            }
        }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }

        private ObservableCollection<CheckBoxItem> _dataLNS;
        public ObservableCollection<CheckBoxItem> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Count : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsChecked = _selectAllLNS; return c; }).ToList();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                SetProperty(ref _searchLNS, value);
                _dataLNSView.Refresh();
            }
        }

        private bool _isSwitch;
        public bool IsSwitch
        {
            get => _isSwitch;
            set => SetProperty(ref _isSwitch, value);
        }

        private bool _checkPrintAccumulation;
        public bool CheckPrintAccumulation
        {
            get => _checkPrintAccumulation;
            set => SetProperty(ref _checkPrintAccumulation, value);
        }

        private string _header1;
        public string Header1
        {
            get => _header1;
            set => SetProperty(ref _header1, value);
        }

        private string _header2;
        public string Header2
        {
            get => _header2;
            set => SetProperty(ref _header2, value);
        }

        private string _header3;
        public string Header3
        {
            get => _header3;
            set => SetProperty(ref _header3, value);
        }

        private double _totalTuChiConLai;
        public double TotalTuChiConLai
        {
            get => _totalTuChiConLai;
            set => SetProperty(ref _totalTuChiConLai, value);
        }

        private double _totalHienVatConLai;
        public double TotalHienVatConLai
        {
            get => _totalHienVatConLai;
            set => SetProperty(ref _totalHienVatConLai, value);
        }

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

        private List<NsDtChungTu> _listDataChungTu;

        public RelayCommand PrintCommand { get; }
        public RelayCommand ExportPdfCommand { get; }
        public RelayCommand ExportExcelCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }

        public ReportDivisionCurrentBatchDialogViewModel(
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            INsMucLucNganSachService nsMucLucNganSachService,
            IExportService exportService,
            ISessionService sessionService,
            IDanhMucService danhMucService,
            IDmChuKyService dmChuKyService,
            ILog logger,
            IMapper mapper,
            DmChuKyDialogViewModel dmChuKyDialogViewModel)
        {
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _exportService = exportService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _logger = logger;
            _mapper = mapper;
            _dmChuKyService = dmChuKyService;
            DmChuKyDialogViewModel = dmChuKyDialogViewModel;

            PrintCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportPdfCommand = new RelayCommand(obj => OnExport(ExportType.PDF));
            ExportExcelCommand = new RelayCommand(obj => OnExport(ExportType.EXCEL));
            ConfigSignCommand = new RelayCommand(obj => OnConfigSign());
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            LoadDataDot();

            if (DataLNS != null && DataLNS.Count > 0)
            {
                foreach (var model in DataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                        }
                    };
                }
            }
            LoadDanhMuc();
        }

        private void LoadDataDot()
        {
            var predicate = CreatePredicate();
            _listDataChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(_listDataChungTu);

            SelectedDataDot = DataDot.Where(item => item.ValueItem == SoChungTu).FirstOrDefault();

            if (IsSwitch)
            {
                _header1 = "Tổng hợp dự toán phân cấp ngân sách năm " + (_sessionInfo.YearOfWork + 1).ToString();
            }
            else
            {
                _header1 = "Tổng hợp số dự phòng ngân sách năm " + (_sessionInfo.YearOfWork + 1).ToString();
            }

            if (SelectedDataDot != null)
            {
                _header2 = string.Format("(Tới đợt {0}, ngày {1})", SelectedDataDot.DisplayItem1.ToString(),
                   SelectedDataDot.DisplayItem2 != null ? SelectedDataDot.DisplayItem2.ToString() : string.Empty);
            }
        }

        public List<DtChungTuModel> GetListChungTuLuyKe()
        {
            if (SelectedDataDot != null)
            {
                if (DateTime.TryParse(SelectedDataDot.DisplayItem3, out DateTime ngayQuyetDinh))
                {
                    var temp = _listDataChungTu.Where(x => x.SSoQuyetDinh != null
                                                            && !x.SSoQuyetDinh.Equals(SelectedDataDot.ValueItem)
                                                            && x.DNgayQuyetDinh.HasValue
                                                            && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Date).ToList();
                    return _mapper.Map<List<DtChungTuModel>>(temp);
                }
            }
            return new List<DtChungTuModel>();
        }

        private void LoadDataLNS(string lns)
        {
            IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", lns.Split(",").ToList().Where(x => x.Length.Equals(7))), _sessionInfo.YearOfWork).ToList();
            DataLNS = new ObservableCollection<CheckBoxItem>();
            DataLNS = _mapper.Map<ObservableCollection<CheckBoxItem>>(listNsMucLucNganSach);
            OnPropertyChanged(nameof(DataLNS));

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;
        }

        private void LoadDanhMuc()
        {
            _units = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri)
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
        }

        private void ResetConditionSearch()
        {
            if (SelectedDataDot != null)
            {
                var chungtuLuykeItems = GetListChungTuLuyKe();
                if (chungtuLuykeItems.Any())
                {
                    List<string> sDLnsLuyke = new List<string>();

                    _searchCondition = new EstimationVoucherDetailCriteria
                    {
                        VoucherIds = string.Join(",", chungtuLuykeItems.Select(x => x.Id)),
                        LNS = string.Join(",", chungtuLuykeItems.SelectMany(x => x.SDslns.Split(",")).Distinct()),
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget
                    };
                    _searchCondition.IsPhanBo = false;
                    var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                    _searchCondition.IsPhanBo = true;
                    var division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                    var listChungTuChiTiet = (from e in estimate
                                              join d in division on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                              equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                              select new DtChungTuChiTietModel()
                                              {
                                                  IIdMlns = e.IIdMlns,
                                                  IIdMlnsCha = e.IIdMlnsCha,
                                                  SXauNoiMa = e.SXauNoiMa,
                                                  SLns = e.SLns,
                                                  SL = e.SL,
                                                  SK = e.SK,
                                                  SM = e.SM,
                                                  STm = e.STm,
                                                  STtm = e.STtm,
                                                  SNg = e.SNg,
                                                  STng = e.STng,
                                                  STng1 = d.STng1,
                                                  STng2 = d.STng2,
                                                  STng3 = d.STng3,
                                                  SMoTa = e.SMoTa,
                                                  FTuChi = e.FTuChi,
                                                  FHienVat = e.FHienVat,
                                                  TuChiDaCap = d?.FTuChi ?? 0,
                                                  HienVatDaCap = d?.FHienVat ?? 0,
                                                  FHangNhap = e.FHangNhap,
                                                  FHangMua = e.FHangMua,
                                                  FPhanCap = e.FPhanCap,
                                                  FDuPhong = e.FDuPhong,
                                                  IsHangCha = e.BHangChaDuToan
                                              });


                    listChungTuChiTiet = listChungTuChiTiet.Where(x => x.TuChiConLai != 0 || x.HienVatConLai != 0).ToList();
                    var listSDslns = string.Join(StringUtils.COMMA, listChungTuChiTiet.Select(x => x.SLns));
                    sDLnsLuyke.Add(listSDslns);

                    LoadDataLNS(string.Join(StringUtils.COMMA, sDLnsLuyke));
                    CheckboxSelectedToStringConvert.SetCheckboxSelected(_dataLNS, string.Join(StringUtils.COMMA, _dataLNS.Select(s => s.ValueItem)));
                    OnPropertyChanged(nameof(SelectedCountLNS));

                }
            }
        }

        private void LoadDslnsCondition(string sSochungTu, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            NsDtChungTu chungTu = _dtChungTuService.FindBySoChungTu(sSochungTu, yearOfWork, yearOfBudget, budgetSource);
            _dtChungTuModel = _mapper.Map<DtChungTuModel>(chungTu);

            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = _dtChungTuModel.Id,
                LNS = _dtChungTuModel.SDslns,
                YearOfWork = _dtChungTuModel.INamLamViec,
                YearOfBudget = _dtChungTuModel.INamNganSach,
                BudgetSource = _dtChungTuModel.IIdMaNguonNganSach,
                VoucherDate = _dtChungTuModel.DNgayChungTu,
                ILoai = _dtChungTuModel.ILoai,
                IdDotNhan = _dtChungTuModel.IIdDotNhan,
                SoChungTu = _dtChungTuModel.SSoChungTu
            };

            _searchCondition.IsPhanBo = false;
            var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
            _searchCondition.IsPhanBo = true;
            var division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);

            var listChungTuChiTiet = (from e in estimate
                                      join d in division
                                      on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                      equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                      select new DtChungTuChiTietModel()
                                      {
                                          IIdMlns = e.IIdMlns,
                                          IIdMlnsCha = e.IIdMlnsCha,
                                          SXauNoiMa = e.SXauNoiMa,
                                          SLns = e.SLns,
                                          SL = e.SL,
                                          SK = e.SK,
                                          SM = e.SM,
                                          STm = e.STm,
                                          STtm = e.STtm,
                                          SNg = e.SNg,
                                          STng = e.STng,
                                          STng1 = d.STng1,
                                          STng2 = d.STng2,
                                          STng3 = d.STng3,
                                          SMoTa = e.SMoTa,
                                          FTuChi = e.FTuChi,
                                          FHienVat = e.FHienVat,
                                          TuChiDaCap = d.FTuChi,
                                          HienVatDaCap = d.FHienVat,
                                          FHangNhap = e.FHangNhap,
                                          FHangMua = e.FHangMua,
                                          FPhanCap = e.FPhanCap,
                                          FDuPhong = e.FDuPhong,
                                          IsHangCha = e.BHangChaDuToan
                                      });


            listChungTuChiTiet = listChungTuChiTiet.Where(x => x.TuChiConLai != 0 || x.HienVatConLai != 0).ToList();
            var listSDslns = string.Join(StringUtils.COMMA, listChungTuChiTiet.Select(x => x.SLns));

            if (TypePrintSelected)
            {
                LoadDataLNS(listSDslns);
            }
            else
            {
                LoadDataLNS(_dtChungTuModel.SDslns.ToString());
            }

            CheckboxSelectedToStringConvert.SetCheckboxSelected(_dataLNS, _dtChungTuModel.SDslns);
            OnPropertyChanged(nameof(SelectedCountLNS));
        }

        private Dictionary<string, string> LoadDslnsConditionAccumulated(string sSochungTu, int yearOfWork, int yearOfBudget, int budgetSource)
        {
            NsDtChungTu chungTu = _dtChungTuService.FindBySoChungTu(sSochungTu, yearOfWork, yearOfBudget, budgetSource);
            _dtChungTuModel = _mapper.Map<DtChungTuModel>(chungTu);

            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = _dtChungTuModel.Id,
                LNS = _dtChungTuModel.SDslns,
                YearOfWork = _dtChungTuModel.INamLamViec,
                YearOfBudget = _dtChungTuModel.INamNganSach,
                BudgetSource = _dtChungTuModel.IIdMaNguonNganSach,
                VoucherDate = _dtChungTuModel.DNgayChungTu,
                ILoai = _dtChungTuModel.ILoai,
                IdDotNhan = _dtChungTuModel.IIdDotNhan,
                SoChungTu = _dtChungTuModel.SSoChungTu
            };


            _searchCondition.IsPhanBo = false;
            var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
            _searchCondition.IsPhanBo = true;
            var division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);

            var listChungTuChiTiet = (from e in estimate
                                      join d in division
                                      on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                      equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                      select new DtChungTuChiTietModel()
                                      {
                                          IIdMlns = e.IIdMlns,
                                          IIdMlnsCha = e.IIdMlnsCha,
                                          SXauNoiMa = e.SXauNoiMa,
                                          SLns = e.SLns,
                                          SL = e.SL,
                                          SK = e.SK,
                                          SM = e.SM,
                                          STm = e.STm,
                                          STtm = e.STtm,
                                          SNg = e.SNg,
                                          STng = e.STng,
                                          STng1 = d.STng1,
                                          STng2 = d.STng2,
                                          STng3 = d.STng3,
                                          SMoTa = e.SMoTa,
                                          FTuChi = e.FTuChi,
                                          FHienVat = e.FHienVat,
                                          TuChiDaCap = d?.FTuChi ?? 0,
                                          HienVatDaCap = d?.FHienVat ?? 0,
                                          FHangNhap = e.FHangNhap,
                                          FHangMua = e.FHangMua,
                                          FPhanCap = e.FPhanCap,
                                          FDuPhong = e.FDuPhong,
                                          IsHangCha = e.BHangChaDuToan
                                      });

            listChungTuChiTiet = listChungTuChiTiet.Where(x => x.TuChiConLai != 0 || x.HienVatConLai != 0).ToList();

            return listChungTuChiTiet.Select(x => x.SLns).Distinct().ToDictionary(x => x);
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void CalculateData()
        {
            _chungTuChiTietItems.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.TuChiDaCap = 0;
                    x.HienVatDaCap = 0;
                    x.FVal1 = 0;
                    x.FVal2 = 0;
                    x.FVal3 = 0;
                    x.FVal4 = 0;
                    x.FVal5 = 0;
                    x.FVal6 = 0;
                    return x;
                }).ToList();

            foreach (var item in _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = _chungTuChiTietItems.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FVal1 += seftItem.FVal1;
            parrentItem.FVal2 += seftItem.FVal2;
            parrentItem.FVal3 += seftItem.FVal3;
            parrentItem.FVal4 += seftItem.FVal4;
            parrentItem.FVal5 += seftItem.FVal5;
            parrentItem.FVal6 += seftItem.FVal6;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.TuChiDaCap += seftItem.TuChiDaCap;
            parrentItem.HienVatDaCap += seftItem.HienVatDaCap;
            CalculateParent(parrentItem, seftItem);
        }

        private void CaculateTotal()
        {
            TotalTuChiConLai = 0;
            TotalHienVatConLai = 0;
            var listChildren = _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted).ToList();
            foreach (var item in listChildren)
            {
                TotalTuChiConLai += item.FTuChi;
                TotalHienVatConLai += item.FHienVat;
            }
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.SDsidMaDonVi == _sessionInfo.IdDonVi);
            return predicate;
        }

        private List<Tuple<string, string, Dictionary<string, object>>> HandleDataConLai(ExportType exportType = ExportType.PDF)
        {
            List<Tuple<string, string, Dictionary<string, Object>>> listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            var chungtuLuykeItems = GetListChungTuLuyKe();

            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherIds = string.Join(",", chungtuLuykeItems.Select(x => x.Id)),
                LNS = CheckboxSelectedToStringConvert.GetValueSelected(DataLNS),
                YearOfWork = _sessionInfo.YearOfWork,
                YearOfBudget = _sessionInfo.YearOfBudget,
                BudgetSource = _sessionInfo.Budget,
                DonViTinh = Convert.ToInt32(SelectedUnit.ValueItem)
            };

            var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopDotSummary(_searchCondition);

            ChungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(estimate);
            CalculateData();
            ChungTuChiTietItems = new ObservableCollection<DtChungTuChiTietModel>(ChungTuChiTietItems.Where(x => x.FTuChi != 0 || x.FHienVat != 0 || x.TuChiConLai != 0 || x.TuChiDaCap != 0).OrderBy(o => o.SXauNoiMa));

            var IdChungtus = ChungTuChiTietItems.Where(x => !String.IsNullOrEmpty(x.SSoQuyetDinh)).Select(s => s.SSoQuyetDinh).ToHashSet();
            int pageSize = 3;
            var dictDotNhanByPageSize = IdChungtus.Select((x, i) => new { Group = i / pageSize, Value = x })
                            .GroupBy(item => item.Group, g => g.Value)
                            .ToDictionary(x => x.Key, x => x.ToList());


            if (exportType == ExportType.EXCEL)
            {
                //listResult.AddRange(HandlerDataExcelCustom(IdChungtus.ToList(), lstCtLuyKe, exportType));
            }
            else
            {
                foreach (var dictByPage in dictDotNhanByPageSize)
                {
                    int countCt = 1;
                    var a = dictByPage;
                    var dictByIndexCol = dictByPage.Value
                        .Select((value, index) => new { PairNum = index, value })
                        .GroupBy(pair => pair.PairNum)
                        .ToDictionary(x => x.Key, x => x.First().value);
                    var sSln = string.Join(",", chungtuLuykeItems.Where(x => dictByPage.Value.Contains(x.SSoQuyetDinh)).Select(x => x.SDslns)).Split(",").ToList();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    var listDataChitietPage = ChungTuChiTietItems.Clone().Where(x => sSln.Contains(x.SLns));
                    List<string> listSoquyetdinh = new List<string>();

                    foreach (var item in dictByIndexCol.Values)
                    {
                        var chungtu = chungtuLuykeItems.FirstOrDefault(x => x.SSoQuyetDinh == item);
                        if (chungtu != null)
                        {
                            listSoquyetdinh.Add(chungtu.SSoQuyetDinh);
                        }
                        switch (countCt)
                        {
                            case 1:
                                listDataChitietPage.Where(x => x.SSoQuyetDinh == item).ForAll(s =>
                                {
                                    s.FVal1 = s.FTuChi - s.TuChiDaCap;
                                    s.FVal4 = s.FHienVat - s.HienVatDaCap;
                                });
                                break;
                            case 2:
                                listDataChitietPage.Where(x => x.SSoQuyetDinh == item).ForAll(s =>
                                {
                                    s.FVal2 = s.FTuChi - s.TuChiDaCap;
                                    s.FVal5 = s.FHienVat - s.HienVatDaCap;
                                }); break;

                            case 3:
                                listDataChitietPage.Where(x => x.SSoQuyetDinh == item).ForAll(s =>
                                {
                                    s.FVal3 = s.FTuChi - s.TuChiDaCap;
                                    s.FVal6 = s.FHienVat - s.HienVatDaCap;
                                }); break;
                        }
                        //data.Add($"sSoQuyetDinh{countCt + 1}", string.Join(",", listSoquyetdinh.Where(x => !string.IsNullOrEmpty(x))));
                        countCt++;

                        //data.Add($"Total{i + 1}", dictTotalByIndexCol.GetValueOrDefault(i, 0.0));
                        //data.Add($"Index{i + 1}", idDotNhanByCols.Count() == 0 ? string.Empty : $"({index + 1})");
                    }

                    // Handler data groups
                    var dataChiTietGroups = listDataChitietPage.Where(w => !w.IsHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(e => new DtChungTuChiTietModel
                    {
                        IIdMlns = e.First().IIdMlns,
                        IIdMlnsCha = e.First().IIdMlnsCha,
                        SXauNoiMa = e.First().SXauNoiMa,
                        IIdDtchungTu = e.First().IIdDtchungTu,
                        SLns = e.First().SLns,
                        SL = e.First().SL,
                        SK = e.First().SK,
                        SM = e.First().SM,
                        STm = e.First().STm,
                        STtm = e.First().STtm,
                        SNg = e.First().SNg,
                        STng = e.First().STng,
                        SMoTa = e.First().SMoTa,
                        FTuChi = e.First().FTuChi,
                        FHienVat = e.First().FHienVat,
                        TuChiDaCap = e.Sum(x => x.TuChiDaCap),
                        HienVatDaCap = e.Sum(x => x.HienVatDaCap),
                        SGhiChu = e.First().SGhiChu,
                        INamLamViec = e.First().INamLamViec,
                        DNgayTao = e.First().DNgayTao,
                        SNguoiTao = e.First().SNguoiTao,
                        DNgaySua = e.First().DNgaySua,
                        SNguoiSua = e.First().SNguoiSua,
                        INamNganSach = e.First().INamNganSach,
                        IIdMaNguonNganSach = e.First().IIdMaNguonNganSach,
                        IIdMaDonVi = e.First().IIdMaDonVi,
                        IPhanCap = e.First().IPhanCap,
                        FHangNhap = e.Sum(x => x.FHangNhap),
                        FHangMua = e.Sum(x => x.FHangMua),
                        FPhanCap = e.Sum(x => x.FPhanCap),
                        FVal1 = e.Sum(x => x.FVal1),
                        FVal2 = e.Sum(x => x.FVal2),
                        FVal3 = e.Sum(x => x.FVal3),
                        FVal4 = e.Sum(x => x.FVal4),
                        FVal5 = e.Sum(x => x.FVal5),
                        FVal6 = e.Sum(x => x.FVal6),

                        IsHangCha = e.First().IsHangCha

                    }).ToList();
                    var dataParenGroups = listDataChitietPage.Where(w => w.IsHangCha).ToList();
                    var dataHandlerGroups = dataParenGroups;
                    dataHandlerGroups.AddRange(dataChiTietGroups);
                    listDataChitietPage = dataHandlerGroups.OrderBy(s => s.SXauNoiMa);
                    listDataChitietPage = CalculateDataPage(listDataChitietPage);
                    listDataChitietPage.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                    listDataChitietPage = listDataChitietPage.Where(x => string.IsNullOrEmpty(x.STng) && (x.FVal != 0 || x.FVal1 != 0 || x.FVal2 != 0 || x.FVal3 != 0 || x.FVal4 != 0 || x.FVal5 != 0 || x.FVal6 != 0)).ToList();

                    foreach (var item in listDataChitietPage.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
                    {
                        var parent = listDataChitietPage.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
                        if (parent != null && item.SM != string.Empty)
                        {
                            if (!string.IsNullOrEmpty(parent.SLns))
                            {
                                item.SLns = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.SL) && !string.IsNullOrEmpty(parent.SK))
                            {
                                item.SL = string.Empty;
                                item.SK = string.Empty;
                                //item.SLns = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(parent.SM))
                                item.SM = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STm))
                                item.STm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STtm))
                                item.STtm = string.Empty;
                            if (!string.IsNullOrEmpty(parent.SNg))
                                item.SNg = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng))
                                item.STng = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng1))
                                item.STng1 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng2))
                                item.STng2 = string.Empty;
                            if (!string.IsNullOrEmpty(parent.STng3))
                                item.STng3 = string.Empty;
                        }
                    }

                    for (var i = 0; i < pageSize; i++)
                    {
                        data.Add($"sSoQuyetDinh{i + 1}", listSoquyetdinh.ElementAtOrDefault(i));
                    }

                    FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", listDataChitietPage);
                    data.Add("TieuDe1", Header1);
                    data.Add("TieuDe2", Header2);
                    data.Add("TieuDe3", Header3);
                    data.Add("Cap1", _cap1);
                    data.Add("Cap2", _sessionInfo.TenDonVi);
                    data.Add("header1", SelectedUnit.DisplayItem);
                    data.Add("TongTuChi1", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal1));
                    data.Add("TongTuChi2", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal2));
                    data.Add("TongTuChi3", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal3));
                    data.Add("TongHienVat1", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal4));
                    data.Add("TongHienVat2", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal5));
                    data.Add("TongHienVat3", listDataChitietPage.Where(x => !x.IsHangCha).Sum(x => x.FVal6));
                    data.Add("Tong", listDataChitietPage.Where(x => !x.IsHangCha).Sum(s => s.FVal));
                    data.Add("Tien_TuChi", StringUtils.NumberToText(listDataChitietPage.Where(w => !w.IsHangCha).Sum(x => x.FVal7)));
                    data.Add("Tien_HienVat", StringUtils.NumberToText(listDataChitietPage.Where(w => !w.IsHangCha).Sum(s => s.FVal8)));

                    var strPageNumber = dictByPage.Key > 0 ? "_To2" : "_To1";
                    var templateFileName = GetFileTemplate(strPageNumber);
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    listResult.Add(Tuple.Create(templateFileName, fileNameWithoutExtension, data));

                }

            }
            return listResult;
        }

        private List<Tuple<string, string, Dictionary<string, object>>> HandlerDataExcelCustom(List<Guid?> listChungTuId, List<DtChungTuModel> lstCtLuyKe, ExportType exportType)
        {
            List<Tuple<string, string, Dictionary<string, Object>>> listResult = new List<Tuple<string, string, Dictionary<string, object>>>();
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<HeaderReportDivisionCurrentBatch> listHeader = new List<HeaderReportDivisionCurrentBatch>();
            List<HeaderReportDivisionCurrentBatchChild> listHeaderChild = new List<HeaderReportDivisionCurrentBatchChild>();
            List<DtChungTuChiTietModel> listDataChitiet = new List<DtChungTuChiTietModel>();

            int locationItem = 1;
            int index = 1;

            foreach (var item in listChungTuId)
            {
                var sSln = string.Join(",", lstCtLuyKe.Where(x => listChungTuId.Contains(x.Id)).Select(x => x.SDslns)).Split(",").ToList();
                var listDataChitietPage = ChungTuChiTietItems.Clone().Where(x => sSln.Contains(x.SLns));

                var chungtu = lstCtLuyKe.FirstOrDefault(x => x.Id == item);

                //Get column mergeRange
                int columnStarts = 12;

                if (chungtu != null)
                {
                    var columnStartName = GetExcelColumnName(columnStarts);
                    var columnEndName = GetExcelColumnName(columnStarts + 1);
                    listHeader.Add(new HeaderReportDivisionCurrentBatch
                    {
                        SSoQuyetDinh = chungtu.SSoQuyetDinh,
                        STT = index,
                        MergeRange = columnStartName + "8" + ":" + columnEndName + "8",
                    });
                    listHeader.Add(new HeaderReportDivisionCurrentBatch { STT = 0 });
                    columnStarts += 2;
                    index++;

                }

                // thêm giá trị cho từng cột tự chi hiện vật theo số quyết định
                for (int i = 1; i <= listHeader.Count; i += 2)
                {
                    if (i == locationItem)
                    {
                        listDataChitietPage.Where(x => x.IIdDtchungTu == item).ForAll(s =>
                        {
                            s.ListData.Add(new ReportDivisionCurrentBatchQuery
                            {
                                FVal1 = s.FTuChi - s.TuChiDaCap
                            });
                            s.ListData.Add(new ReportDivisionCurrentBatchQuery
                            {
                                FVal1 = s.FHienVat - s.HienVatDaCap
                            });
                        });
                    }
                    else
                    {
                        listDataChitietPage.Where(x => x.IIdDtchungTu == item).ForAll(s =>
                        {
                            s.ListData.Add(new ReportDivisionCurrentBatchQuery());
                            s.ListData.Add(new ReportDivisionCurrentBatchQuery());
                        });
                    }
                }

                locationItem += 2;
                listDataChitietPage = CalculateDataPage(listDataChitietPage);
                //listDataChitietPage.Where(x => !string.IsNullOrEmpty(x.SNg)).Select(x => x.IsHangCha = false).ToList();
                //listDataChitietPage = listDataChitietPage.Where(x => string.IsNullOrEmpty(x.STng) && (x.FTuChi != 0 || x.FHienVat != 0)).ToList();

                listDataChitiet.AddRange(listDataChitietPage);
            }

            //get list header child
            for (int i = 0; i < listHeader.Count; i++)
            {
                if (i % 2 == 0)
                    listHeaderChild.Add(new HeaderReportDivisionCurrentBatchChild { STen = "Tự chi", STT = i + 1 });
                else
                    listHeaderChild.Add(new HeaderReportDivisionCurrentBatchChild { STen = "Hiện vật", STT = i + 1 });
            }

            // them nhung phan tu thieu trong listData cua tung ban ghi theo header
            listDataChitiet.Where(w => w.ListData.Count < listHeader.Count).ForAll(f =>
            {
                int subCount = listHeader.Count - f.ListData.Count;
                for (int i = 0; i < subCount; i++)
                {
                    f.ListData.Add(new ReportDivisionCurrentBatchQuery());
                }
            });

            //Handler data groups
            var dataChiTietGroups = listDataChitiet.Where(w => !w.IsHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(e => new DtChungTuChiTietModel
            {
                IIdMlns = e.First().IIdMlns,
                IIdMlnsCha = e.First().IIdMlnsCha,
                SXauNoiMa = e.First().SXauNoiMa,
                IIdDtchungTu = e.First().IIdDtchungTu,
                SLns = e.First().SLns,
                SL = e.First().SL,
                SK = e.First().SK,
                SM = e.First().SM,
                STm = e.First().STm,
                STtm = e.First().STtm,
                SNg = e.First().SNg,
                STng = e.First().STng,
                SMoTa = e.First().SMoTa,
                FTuChi = e.First().FTuChi,
                FHienVat = e.First().FHienVat,
                TuChiDaCap = e.Sum(x => x.TuChiDaCap),
                HienVatDaCap = e.Sum(x => x.HienVatDaCap),
                SGhiChu = e.First().SGhiChu,
                INamLamViec = e.First().INamLamViec,
                DNgayTao = e.First().DNgayTao,
                SNguoiTao = e.First().SNguoiTao,
                DNgaySua = e.First().DNgaySua,
                SNguoiSua = e.First().SNguoiSua,
                INamNganSach = e.First().INamNganSach,
                IIdMaNguonNganSach = e.First().IIdMaNguonNganSach,
                IIdMaDonVi = e.First().IIdMaDonVi,
                IPhanCap = e.First().IPhanCap,
                FHangNhap = e.Sum(x => x.FHangNhap),
                FHangMua = e.Sum(x => x.FHangMua),
                FPhanCap = e.Sum(x => x.FPhanCap),
                IsHangCha = e.First().IsHangCha,
                ListData = CalculateListData(e.AsEnumerable()),
            }).ToList();

            var dataParenGroups = listDataChitiet.Where(w => w.IsHangCha).GroupBy(g => new { g.IIdMlns, g.IIdMlnsCha, g.SXauNoiMa, g.SLns, g.SL, g.SK, g.SM, g.STm, g.STtm, g.SNg, g.STng, g.STng1, g.STng2, g.STng3, g.SMoTa, g.IsHangCha }).Select(e => new DtChungTuChiTietModel
            {
                IIdMlns = e.First().IIdMlns,
                IIdMlnsCha = e.First().IIdMlnsCha,
                SXauNoiMa = e.First().SXauNoiMa,
                IIdDtchungTu = e.First().IIdDtchungTu,
                SLns = e.First().SLns,
                SL = e.First().SL,
                SK = e.First().SK,
                SM = e.First().SM,
                STm = e.First().STm,
                STtm = e.First().STtm,
                SNg = e.First().SNg,
                STng = e.First().STng,
                SMoTa = e.First().SMoTa,
                FTuChi = e.First().FTuChi,
                FHienVat = e.First().FHienVat,
                TuChiDaCap = e.Sum(x => x.TuChiDaCap),
                HienVatDaCap = e.Sum(x => x.HienVatDaCap),
                SGhiChu = e.First().SGhiChu,
                INamLamViec = e.First().INamLamViec,
                DNgayTao = e.First().DNgayTao,
                SNguoiTao = e.First().SNguoiTao,
                DNgaySua = e.First().DNgaySua,
                SNguoiSua = e.First().SNguoiSua,
                INamNganSach = e.First().INamNganSach,
                IIdMaNguonNganSach = e.First().IIdMaNguonNganSach,
                IIdMaDonVi = e.First().IIdMaDonVi,
                IPhanCap = e.First().IPhanCap,
                FHangNhap = e.Sum(x => x.FHangNhap),
                FHangMua = e.Sum(x => x.FHangMua),
                FPhanCap = e.Sum(x => x.FPhanCap),
                IsHangCha = e.First().IsHangCha,
            }).ToList();
            var dataHandlerGroups = dataParenGroups;
            dataHandlerGroups.AddRange(dataChiTietGroups);
            listDataChitiet = dataHandlerGroups.OrderBy(s => s.SXauNoiMa).ToList();

            listDataChitiet = CalculateDataExcel(listDataChitiet).ToList();
            var dataTuple = CalculateTuChiHienVat(listDataChitiet);

            FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Items", listDataChitiet);
            data.Add("ListHeader", listHeader);
            data.Add("ListHeaderChild", listHeaderChild);
            data.Add("TitleFirst", Header1);
            data.Add("TitleSecond", Header2);
            data.Add("TitleThird", Header3 ?? string.Empty);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("Ngay", string.Empty);
            data.Add("TenDonVi", _sessionInfo.TenDonVi);
            data.Add("header1", SelectedUnit.DisplayItem);
            data.Add("CATUNITTYPE", "Đơn vị tính: " + (SelectedUnit != null ? SelectedUnit.DisplayItem : StringUtils.COMMA));
            data.Add("Tong", listDataChitiet.Where(x => !x.IsHangCha).Sum(s => s.ListData.Sum(x => x.FVal1)));
            data.Add("Tien_TuChi", StringUtils.NumberToText(dataTuple.Item1));
            data.Add("Tien_HienVat", StringUtils.NumberToText(dataTuple.Item2));
            data.Add("ListSumDynamic", dataTuple.Item3);
            AddChuKy(data);
            var fileName = string.Empty;
            fileName = ExportFileName.RPT_NS_DUTOAN_CHITIEU_DUPHONG_DOTNHAN;
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, string.Format("{0}.xlsx", fileName));
            string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            listResult.Add(Tuple.Create(templateFileName, fileNameWithoutExtension, data));

            return listResult;
        }

        private List<ReportDivisionCurrentBatchQuery> CalculateListData(IEnumerable<DtChungTuChiTietModel> listData)
        {
            var result = new List<ReportDivisionCurrentBatchQuery>();
            for (int i = 0; i < listData.First().ListData.Count; i++)
            {
                result.Add(new ReportDivisionCurrentBatchQuery());
                foreach (var item in listData)
                {
                    result[i].FVal1 += item.ListData[i].FVal1;
                }
            }

            return result;
        }

        private Tuple<double, double, List<ReportDivisionCurrentBatchQuery>> CalculateTuChiHienVat(List<DtChungTuChiTietModel> dtChungTuChiTiets)
        {
            List<Tuple<double, double, List<ReportDivisionCurrentBatchQuery>>> tuple = new List<Tuple<double, double, List<ReportDivisionCurrentBatchQuery>>>();
            double fTongTuchi = 0;
            double fTongHienVat = 0;
            List<ReportDivisionCurrentBatchQuery> listDataSum = new List<ReportDivisionCurrentBatchQuery>();
            for (int i = 0; i < dtChungTuChiTiets.FirstOrDefault().ListData.Count; i++)
            {
                listDataSum.Add(new ReportDivisionCurrentBatchQuery());
                if (i % 2 == 0)
                    fTongTuchi += dtChungTuChiTiets.Where(w => !w.IsHangCha).Sum(s => s.ListData[i].FVal1);
                else
                    fTongHienVat += dtChungTuChiTiets.Where(w => !w.IsHangCha).Sum(s => s.ListData[i].FVal1);
                listDataSum[i].FVal1 = dtChungTuChiTiets.Where(w => !w.IsHangCha).Sum(s => s.ListData[i].FVal1);
            }
            tuple.Add(Tuple.Create(fTongTuchi, fTongHienVat, listDataSum));
            return tuple.FirstOrDefault();
        }

        private void AddChuKy(Dictionary<string, object> data)
        {
            data.Add("Diadiem", string.Format("{0}, ngày {1} tháng {2} năm {3}", string.Empty, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
            data.Add("ChucDanh1", _dmChuKy != null ? _dmChuKy.ChucDanh1MoTa : string.Empty);
            data.Add("ChucDanh2", _dmChuKy != null ? _dmChuKy.ChucDanh2MoTa : string.Empty);
            data.Add("ChucDanh3", _dmChuKy != null ? _dmChuKy.ChucDanh3MoTa : string.Empty);
            data.Add("ThuaLenh1", _dmChuKy != null ? _dmChuKy.ThuaLenh1MoTa : string.Empty);
            data.Add("ThuaLenh2", _dmChuKy != null ? _dmChuKy.ThuaLenh2MoTa : string.Empty);
            data.Add("ThuaLenh3", _dmChuKy != null ? _dmChuKy.ThuaLenh3MoTa : string.Empty);
            data.Add("Ten1", _dmChuKy != null ? _dmChuKy.Ten1MoTa : string.Empty);
            data.Add("Ten2", _dmChuKy != null ? _dmChuKy.Ten2MoTa : string.Empty);
            data.Add("Ten3", _dmChuKy != null ? _dmChuKy.Ten3MoTa : string.Empty);
        }

        public string GetFileTemplate(string strPageNumber = "")
        {
            return Path.Combine(ExportPrefix.PATH_TL_DT, $"{ExportFileName.RPT_NS_DUTOAN_CHITIEU_DUPHONG}{strPageNumber}.xls");
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                    switch (exportType)
                    {
                        case ExportType.EXCEL:
                            dataExport = HandleDataConLai(exportType);
                            break;
                        case ExportType.PDF:
                            dataExport = HandleDataConLai(exportType);
                            break;
                        case ExportType.WORD:
                            break;
                        case ExportType.BROWSER:
                            dataExport = HandleDataConLai(exportType);
                            break;
                        case ExportType.SIGNATURE:
                            break;
                    }

                    foreach (var item in dataExport)
                    {
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel, HeaderReportDivisionCurrentBatch, HeaderReportDivisionCurrentBatchChild, ReportDivisionCurrentBatchQuery>(item.Item1, item.Item3);
                        results.Add(new ExportResult(item.Item2, item.Item2, null, xlsFile));
                    }
                    e.Result = results;

                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
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

        private void OnConfigSign()
        {
            DmChuKyModel chuKyModel = new DmChuKyModel();
            _dmChuKy = _dmChuKyService.FindByCondition(x => x.IdType.Equals(TypeChuKy.RPT_NS_DUTOAN_SOPHANBO_HIENTAI) && x.ITrangThai == StatusType.ACTIVE).FirstOrDefault();
            if (_dmChuKy == null)
                chuKyModel.IdType = TypeChuKy.RPT_NS_DUTOAN_SOPHANBO_HIENTAI;
            else
                chuKyModel = _mapper.Map<DmChuKyModel>(_dmChuKy);

            if (_dmChuKy != null && !string.IsNullOrEmpty(_dmChuKy.TieuDe3MoTa))
            {
                Header3 = _dmChuKy.TieuDe3MoTa;
            }
            DmChuKyDialogViewModel.DmChuKyModel = chuKyModel;
            DmChuKyDialogViewModel.Init();
            DmChuKyDialogViewModel.ShowDialog();
        }

        private IEnumerable<DtChungTuChiTietModel> CalculateDataPage(IEnumerable<DtChungTuChiTietModel> listdata)
        {
            listdata.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.TuChiDaCap = 0;
                    x.HienVatDaCap = 0;
                    x.FVal1 = 0;
                    x.FVal2 = 0;
                    x.FVal3 = 0;
                    x.FVal4 = 0;
                    x.FVal5 = 0;
                    x.FVal6 = 0;
                    return x;
                }).ToList();

            foreach (var item in listdata.Where(x => !x.IsHangCha && !x.IsDeleted))
            {
                CalculateParentPage(item, item, listdata);
            }
            return listdata;
        }

        private void CalculateParentPage(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem, IEnumerable<DtChungTuChiTietModel> listdata)
        {
            var parrentItem = listdata.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FVal1 += seftItem.FVal1;
            parrentItem.FVal2 += seftItem.FVal2;
            parrentItem.FVal3 += seftItem.FVal3;
            parrentItem.FVal4 += seftItem.FVal4;
            parrentItem.FVal5 += seftItem.FVal5;
            parrentItem.FVal6 += seftItem.FVal6;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.TuChiDaCap += seftItem.TuChiDaCap;
            parrentItem.HienVatDaCap += seftItem.HienVatDaCap;
            CalculateParentPage(parrentItem, seftItem, listdata);
        }

        private IEnumerable<DtChungTuChiTietModel> CalculateDataExcel(List<DtChungTuChiTietModel> listdata)
        {
            var countItemData = listdata.FirstOrDefault(w => !w.IsHangCha).ListData.Count;
            listdata.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.TuChiDaCap = 0;
                    x.HienVatDaCap = 0;
                    x.ListData = new List<ReportDivisionCurrentBatchQuery>();
                    for (var i = 0; i < countItemData; i++)
                    {
                        x.ListData.Add(new ReportDivisionCurrentBatchQuery());
                    };
                    return x;
                }).ToList();

            foreach (var item in listdata.Where(x => !x.IsHangCha && !x.IsDeleted))
            {
                CalculateParentExcel(item, item, listdata);
            }
            return listdata;
        }

        private void CalculateParentExcel(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem, List<DtChungTuChiTietModel> listdata)
        {
            var parrentItem = listdata.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            if (parrentItem.ListData.Any())
            {
                for (var i = 0; i < parrentItem.ListData.Count; i++)
                {
                    parrentItem.ListData[i].FVal1 += seftItem.ListData[i].FVal1;
                }
            }

            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.TuChiDaCap += seftItem.TuChiDaCap;
            parrentItem.HienVatDaCap += seftItem.HienVatDaCap;
            CalculateParentExcel(parrentItem, seftItem, listdata);
        }

        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int module = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + module) + columnName;
                columnNumber = (columnNumber - module) / 26;
            }
            return columnName;
        }
    }
}
