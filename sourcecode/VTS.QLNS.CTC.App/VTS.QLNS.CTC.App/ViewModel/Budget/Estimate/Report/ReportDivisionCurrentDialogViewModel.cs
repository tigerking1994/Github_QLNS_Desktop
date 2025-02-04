using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Report
{
    public class ReportDivisionCurrentDialogViewModel : ViewModelBase
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
        public override Type ContentType => typeof(View.Budget.Estimate.Report.ReportDivisionCurrentDialog);
        public override PackIconKind IconKind => PackIconKind.Printer;
        public bool TypePrintSelected { get; set; }

        private ObservableCollection<DtChungTuChiTietModel> _chungTuChiTietItems;
        public ObservableCollection<DtChungTuChiTietModel> ChungTuChiTietItems
        {
            get => _chungTuChiTietItems;
            set => SetProperty(ref _chungTuChiTietItems, value);
        }

        private List<NsDtChungTu> _listDataChungTu;

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
            set
            {
                SetProperty(ref _checkPrintAccumulation, value);
                ResetConditionSearch();
            }
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

        public RelayCommand PrintCommand { get; }
        public RelayCommand ExportPdfCommand { get; }
        public RelayCommand ExportExcelCommand { get; set; }
        public RelayCommand ConfigSignCommand { get; }

        public ReportDivisionCurrentDialogViewModel(
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
            _checkPrintAccumulation = false;
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
            _listDataChungTu = _dtChungTuService.FindByCondition(x => x.INamLamViec == _sessionInfo.YearOfWork && x.SDsidMaDonVi == _sessionInfo.IdDonVi).ToList();
            DataDot = _mapper.Map<ObservableCollection<ComboboxManyItem>>(_listDataChungTu);

            SelectedDataDot = DataDot.FirstOrDefault(item => item.ValueItem == SoChungTu);
            _header1 = IsSwitch
                                ? "Tổng hợp dự toán phân cấp ngân sách năm " + (_sessionInfo.YearOfWork + 1).ToString()
                                : "Tổng hợp số dự phòng ngân sách năm " + (_sessionInfo.YearOfWork + 1).ToString();

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
                if (CheckPrintAccumulation)
                {
                    if (DateTime.TryParse(SelectedDataDot.DisplayItem3, out DateTime ngayQuyetDinh))
                    {
                        var listLuyKe = _listDataChungTu.Where(x =>
                            x.SSoQuyetDinh != null
                            && !x.SSoQuyetDinh.Equals(SelectedDataDot.ValueItem)
                            && x.DNgayQuyetDinh.HasValue
                            && x.DNgayQuyetDinh.Value.Date <= ngayQuyetDinh.Date).ToList();
                        return _mapper.Map<List<DtChungTuModel>>(listLuyKe);
                    }
                }
            }
            return new List<DtChungTuModel>();
        }

        private void LoadDataLNS(string lns)
        {
            //IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", lns.Split(",").ToList().Where(x => x.Length.Equals(7))), _sessionInfo.YearOfWork).ToList();
            IEnumerable<NsMucLucNganSach> listNsMucLucNganSach = _nsMucLucNganSachService.FindByListLnsDonVi(string.Join(",", lns.Split(",").Distinct().ToList()), _sessionInfo.YearOfWork).ToList();
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
                if (CheckPrintAccumulation)
                {
                    var _chungtuLuykeItems = GetListChungTuLuyKe();
                    if (_chungtuLuykeItems.Any())
                    {
                        List<string> sDLnsLuyke = new List<string>();

                        _searchCondition = new EstimationVoucherDetailCriteria
                        {
                            VoucherIds = string.Join(",", _chungtuLuykeItems.Select(x => x.Id)),
                            LNS = string.Join(",", _chungtuLuykeItems.SelectMany(x => x.SDslns.Split(",")).Distinct()),
                            YearOfWork = _sessionInfo.YearOfWork,
                            YearOfBudget = _sessionInfo.YearOfBudget,
                            BudgetSource = _sessionInfo.Budget
                        };

                        _searchCondition.IsPhanBo = false;
                        var estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                        _searchCondition.IsPhanBo = true;
                        var divistion = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                        //GroupBy(ref estimate);
                        //GroupBy(ref divistion);

                        var listChungTuChiTietLk = (from e in estimate
                                                    join d in divistion
                                                    on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3, e.SMoTa }
                                                    equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3, d.SMoTa }
                                                    into gj
                                                    from sub in gj.DefaultIfEmpty()
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
                                                        STng1 = sub.STng1,
                                                        STng2 = sub.STng2,
                                                        STng3 = sub.STng3,
                                                        SMoTa = e.SMoTa,
                                                        FTuChi = e.FTuChi,
                                                        FHienVat = e.FHienVat,
                                                        TuChiDaCap = sub.FTuChi,
                                                        HienVatDaCap = sub.FHienVat,
                                                        FHangNhap = e.FHangNhap,
                                                        FHangMua = e.FHangMua,
                                                        FPhanCap = e.FPhanCap,
                                                        FDuPhong = e.FDuPhong,
                                                        IsHangCha = e.BHangChaDuToan
                                                    });

                        listChungTuChiTietLk = listChungTuChiTietLk.Where(x => x.TuChiConLai != 0 || x.HienVatConLai != 0).ToList();
                        var listSDslnsLk = string.Join(StringUtils.COMMA, listChungTuChiTietLk.Select(x => x.SLns));
                        sDLnsLuyke.Add(listSDslnsLk);

                        LoadDataLNS(string.Join(StringUtils.COMMA, sDLnsLuyke));
                        CheckboxSelectedToStringConvert.SetCheckboxSelected(_dataLNS, string.Join(StringUtils.COMMA, _dataLNS.Select(s => s.ValueItem)));
                        OnPropertyChanged(nameof(SelectedCountLNS));
                    }

                }
                else
                {
                    NsDtChungTu chungTu = _dtChungTuService.FindBySoChungTu(SelectedDataDot.ValueItem, _sessionInfo.YearOfWork, _sessionInfo.YearOfBudget, _sessionInfo.Budget);
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
                    var estimate = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);
                    _searchCondition.IsPhanBo = true;
                    var division = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);
                    //GroupBy(ref estimate);
                    //GroupBy(ref division);

                    var listChungTuChiTiet = (from e in estimate
                                              join d in division
                                              on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                              equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                              into gj
                                              from sub in gj.DefaultIfEmpty()
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
                                                  STng1 = sub.STng1,
                                                  STng2 = sub.STng2,
                                                  STng3 = sub.STng3,
                                                  SMoTa = e.SMoTa,
                                                  FTuChi = e.FTuChi,
                                                  FHienVat = e.FHienVat,
                                                  TuChiDaCap = sub.FTuChi,
                                                  HienVatDaCap = sub.FHienVat,
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
            }
        }

        private void GroupBy(ref IEnumerable<NsDtChungTuChiTietQuery> listData)
        {
            listData = listData.GroupBy(g => new
            {
                g.IIdMlns,
                g.IIdMlnsCha,
                g.SXauNoiMa,
                g.SLns,
                g.SL,
                g.SK,
                g.SM,
                g.STm,
                g.STtm,
                g.SNg,
                g.STng,
                g.STng1,
                g.STng2,
                g.STng3,
                g.SMoTa,
                g.BHangChaDuToan
            }).Select(x => new NsDtChungTuChiTietQuery
            {
                IIdMlns = x.Key.IIdMlns,
                IIdMlnsCha = x.Key.IIdMlnsCha,
                SXauNoiMa = x.Key.SXauNoiMa,
                SLns = x.Key.SLns,
                SL = x.Key.SL,
                SK = x.Key.SK,
                SM = x.Key.SM,
                STm = x.Key.STm,
                STtm = x.Key.STtm,
                SNg = x.Key.SNg,
                STng = x.Key.STng,
                STng1 = x.Key.STng1,
                STng2 = x.Key.STng2,
                STng3 = x.Key.STng3,
                SMoTa = x.Key.SMoTa,
                FTuChi = x.Sum(x => x.FTuChi),
                FHienVat = x.Sum(x => x.FHienVat),
                FHangNhap = x.Sum(x => x.FHangNhap),
                FHangMua = x.Sum(x => x.FHangMua),
                FPhanCap = x.Sum(x => x.FPhanCap),
                FDuPhong = x.Sum(x => x.FDuPhong),
                BHangChaDuToan = x.Key.BHangChaDuToan
            });
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
            var listChungTuChiTietEstimation = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);
            _searchCondition.IsPhanBo = true;
            var listChungTuChiTietEstimationDivision = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);

            var listChungTuChiTiet = (from e in listChungTuChiTietEstimation
                                      join d in listChungTuChiTietEstimationDivision on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
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
                    return x;
                }).ToList();

            foreach (var item in _chungTuChiTietItems.Where(x => !x.IsHangCha && !x.IsDeleted))
            {
                CalculateParent(item, item);
            }
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = _chungTuChiTietItems.FirstOrDefault(x => x.IIdMlns == currentItem.IIdMlnsCha);
            if (parrentItem == null) return;
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
                TotalTuChiConLai += item.TuChiConLai;
                TotalHienVatConLai += item.HienVatConLai;
            }
        }

        private Dictionary<string, object> HandleData(ExportType exportType = ExportType.PDF)
        {
            int donViTinh = Convert.ToInt32(SelectedUnit.ValueItem);
            IEnumerable<NsDtChungTuChiTietQuery> estimate = new List<NsDtChungTuChiTietQuery>();
            IEnumerable<NsDtChungTuChiTietQuery> division = new List<NsDtChungTuChiTietQuery>();

            if (!CheckPrintAccumulation)
            {
                _searchCondition = new EstimationVoucherDetailCriteria()
                {
                    VoucherId = _dtChungTuModel.Id,
                    LNS = CheckboxSelectedToStringConvert.GetValueSelected(DataLNS),
                    YearOfWork = _dtChungTuModel.INamLamViec,
                    YearOfBudget = _dtChungTuModel.INamNganSach,
                    BudgetSource = _dtChungTuModel.IIdMaNguonNganSach,
                    VoucherDate = _dtChungTuModel.DNgayChungTu != null ? _dtChungTuModel.DNgayChungTu : DateTime.Now,
                    ILoai = _dtChungTuModel.ILoai,
                    IdDotNhan = _dtChungTuModel.IIdDotNhan != null ? _dtChungTuModel.IIdDotNhan : string.Empty
                };
                estimate = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);
                division = _dtChungTuChiTietService.FindByLuyKePhanBoTongHop(_searchCondition);
                GroupBy(ref estimate);
                GroupBy(ref division);
            }
            else
            {
                var sLNSCheckeds = DataLNS.Where(x => x.IsChecked).Select(s => s.ValueItem).ToList();
                var lstCtLuyKe = GetListChungTuLuyKe();
                if (lstCtLuyKe.Count > 0)
                {
                    var chungtuLuykeItems = GetListChungTuLuyKe();

                    _searchCondition = new EstimationVoucherDetailCriteria
                    {
                        VoucherIds = string.Join(",", chungtuLuykeItems.Select(x => x.Id)),
                        LNS = CheckboxSelectedToStringConvert.GetValueSelected(DataLNS),
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                    };
                    _searchCondition.IsPhanBo = false;
                    estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                    _searchCondition.IsPhanBo = true;
                    division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                }
            }

            //GroupBy(ref estimate);
            //GroupBy(ref division);

            var listChungTuChiTiet = (from e in estimate
                                      join d in division
                                      on new { e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                      equals new { d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                      into result
                                      from r in result.DefaultIfEmpty()
                                      select new DtChungTuChiTietModel()
                                      {
                                          IIdMlns = e.IIdMlns,
                                          IIdMlnsCha = e.IIdMlnsCha,
                                          SXauNoiMa = e.SXauNoiMa,
                                          IIdDtchungTu = e.IIdDtchungTu,
                                          SLns = e.SLns,
                                          SL = e.SL,
                                          SK = e.SK,
                                          SM = e.SM,
                                          STm = e.STm,
                                          STtm = e.STtm,
                                          SNg = e.SNg,
                                          STng = e.STng,
                                          SMoTa = e.SMoTa,
                                          FTuChi = e.FTuChi / donViTinh,
                                          FHienVat = e.FHienVat / donViTinh,
                                          TuChiDaCap = r != null ? r.FTuChi / donViTinh : 0,
                                          HienVatDaCap = r != null ? r.FHienVat / donViTinh : 0,
                                          SGhiChu = e.SGhiChu,
                                          INamLamViec = e.INamLamViec,
                                          DNgayTao = e.DNgayTao,
                                          SNguoiTao = e.SNguoiTao,
                                          DNgaySua = e.DNgaySua,
                                          SNguoiSua = e.SNguoiSua,
                                          INamNganSach = e.INamNganSach,
                                          IIdMaNguonNganSach = e.IIdMaNguonNganSach,
                                          IIdMaDonVi = e.IIdMaDonVi,
                                          IPhanCap = e.IPhanCap,
                                          FHangNhap = e.FHangNhap / donViTinh,
                                          FHangMua = e.FHangMua / donViTinh,
                                          FPhanCap = e.FPhanCap / donViTinh,
                                          IsHangCha = e.BHangChaDuToan
                                      });

            ChungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(listChungTuChiTiet);
            CalculateData();
            CaculateTotal();
            ChungTuChiTietItems = new ObservableCollection<DtChungTuChiTietModel>(ChungTuChiTietItems.Where(x => x.FTuChi != 0 || x.FHienVat != 0).OrderBy(x => x.SXauNoiMa));

            foreach (var item in ChungTuChiTietItems.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
            {
                var parent = ChungTuChiTietItems.FirstOrDefault(x => x.IIdMlns == item.IIdMlnsCha);
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

            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Items", ChungTuChiTietItems);
            data.Add("TieuDe1", Header1);
            data.Add("TieuDe2", Header2);
            data.Add("TieuDe3", Header3);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("header1", SelectedUnit.DisplayItem);
            return data;
        }


        private Dictionary<string, object> HandleDataConLai(ExportType exportType = ExportType.PDF)
        {
            int donViTinh = Convert.ToInt32(SelectedUnit.ValueItem);
            IEnumerable<NsDtChungTuChiTietQuery> estimate = new List<NsDtChungTuChiTietQuery>();
            IEnumerable<NsDtChungTuChiTietQuery> division = new List<NsDtChungTuChiTietQuery>();
            if (!CheckPrintAccumulation)
            {
                _searchCondition = new EstimationVoucherDetailCriteria()
                {
                    VoucherId = _dtChungTuModel.Id,
                    LNS = CheckboxSelectedToStringConvert.GetValueSelected(DataLNS),
                    YearOfWork = _dtChungTuModel.INamLamViec,
                    YearOfBudget = _dtChungTuModel.INamNganSach,
                    BudgetSource = _dtChungTuModel.IIdMaNguonNganSach,
                    VoucherDate = _dtChungTuModel.DNgayChungTu ?? DateTime.Now,
                    ILoai = _dtChungTuModel.ILoai,
                    IdDotNhan = _dtChungTuModel.IIdDotNhan ?? string.Empty
                };

                estimate = _dtChungTuChiTietService.FindByLuyKeTongHop(_searchCondition);
                division = _dtChungTuChiTietService.FindByLuyKePhanBoTongHop(_searchCondition);
                GroupBy(ref estimate);
                GroupBy(ref division);
            }
            else
            {
                var sLNSCheckeds = DataLNS.Where(x => x.IsChecked).Select(s => s.ValueItem).ToList();
                var lstCtLuyKe = GetListChungTuLuyKe();
                if (lstCtLuyKe.Count > 0)
                {
                    var chungtuLuykeItems = GetListChungTuLuyKe();

                    _searchCondition = new EstimationVoucherDetailCriteria
                    {
                        VoucherIds = string.Join(",", chungtuLuykeItems.Select(x => x.Id)),
                        LNS = CheckboxSelectedToStringConvert.GetValueSelected(DataLNS),
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget
                    };
                    _searchCondition.IsPhanBo = false;
                    estimate = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                    _searchCondition.IsPhanBo = true;
                    division = _dtChungTuChiTietService.FindByLuyKeTongHopSummary(_searchCondition);
                }
            }




            var listChungTuChiTiet = (from e in estimate
                                      join d in division
                                      on new { x1 = e.SLns, e.SL, e.SK, e.SM, e.STm, e.STtm, e.SNg, e.STng, e.STng1, e.STng2, e.STng3 }
                                      equals new { x1 = d.SLns, d.SL, d.SK, d.SM, d.STm, d.STtm, d.SNg, d.STng, d.STng1, d.STng2, d.STng3 }
                                      into result
                                      from r in result.DefaultIfEmpty()
                                      select new DtChungTuChiTietModel()
                                      {
                                          IIdMlns = e.IIdMlns,
                                          IIdMlnsCha = e.IIdMlnsCha,
                                          SXauNoiMa = e.SXauNoiMa,
                                          IIdDtchungTu = e.IIdDtchungTu ?? r?.IIdCtduToanNhan,
                                          IIdCtduToanNhan = r?.IIdCtduToanNhan,
                                          SLns = e.SLns,
                                          SL = e.SL,
                                          SK = e.SK,
                                          SM = e.SM,
                                          STm = e.STm,
                                          STtm = e.STtm,
                                          SNg = e.SNg,
                                          STng = e.STng,
                                          SMoTa = e.SMoTa,
                                          FTuChi = e.FTuChi / donViTinh,
                                          FHienVat = e.FHienVat / donViTinh,
                                          TuChiDaCap = (r?.FTuChi ?? 0) / donViTinh,
                                          HienVatDaCap = (r?.FHienVat ?? 0) / donViTinh,
                                          SGhiChu = e.SGhiChu,
                                          INamLamViec = e.INamLamViec,
                                          DNgayTao = e.DNgayTao,
                                          SNguoiTao = e.SNguoiTao,
                                          DNgaySua = e.DNgaySua,
                                          SNguoiSua = e.SNguoiSua,
                                          INamNganSach = e.INamNganSach,
                                          IIdMaNguonNganSach = e.IIdMaNguonNganSach,
                                          IIdMaDonVi = e.IIdMaDonVi,
                                          IPhanCap = e.IPhanCap,
                                          FHangNhap = e.FHangNhap / donViTinh,
                                          FHangMua = e.FHangMua / donViTinh,
                                          FPhanCap = e.FPhanCap / donViTinh,
                                          IsHangCha = e.BHangChaDuToan
                                      });


            ChungTuChiTietItems = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(listChungTuChiTiet);
            CalculateData();
            CaculateTotal();
            ChungTuChiTietItems = new ObservableCollection<DtChungTuChiTietModel>(ChungTuChiTietItems.Where(x => x.TuChiConLai != 0 || x.HienVatConLai != 0).OrderBy(x => x.SXauNoiMa));

            foreach (var item in ChungTuChiTietItems.Where(x => !string.IsNullOrEmpty(x.SL)).OrderByDescending(x => x.SXauNoiMa))
            {
                var parent = ChungTuChiTietItems.Where(x => x.IIdMlns == item.IIdMlnsCha).LastOrDefault();
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

            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(Convert.ToInt32(SelectedUnit.ValueItem), exportType);
            data.Add("FormatNumber", formatNumber);
            data.Add("Items", ChungTuChiTietItems);
            data.Add("TieuDe1", Header1);
            data.Add("TieuDe2", Header2);
            data.Add("TieuDe3", Header3);
            data.Add("Cap1", _cap1);
            data.Add("Cap2", _sessionInfo.TenDonVi);
            data.Add("header1", SelectedUnit.DisplayItem);

            return data;
        }

        private void OnExport(ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    string fileName = string.Empty;
                    if (IsSwitch)
                    {
                        Dictionary<string, object> data = HandleData(exportType);
                        fileName = ExportFileName.RPT_NS_DUTOAN_CHITIEU_LUYKE_TONGHOP;
                        data.Add("TongTuChi", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.FTuChi));
                        data.Add("TongHienVat", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.FHienVat));
                        data.Add("TongTuChiDaCap", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.TuChiDaCap));
                        data.Add("TongHienVatDaCap", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.HienVatDaCap));
                        data.Add("TongTuChiConLai", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.TuChiConLai));
                        data.Add("TongHienVatConLai", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.HienVatConLai));
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, string.Format("{0}.xls", fileName));
                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel>(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
                    else
                    {
                        Dictionary<string, object> data = HandleDataConLai(exportType);
                        fileName = ExportFileName.RPT_NS_DUTOAN_CHITIEU_DUPHONG;
                        data.Add("TongTuChi", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.TuChiConLai));
                        data.Add("TongHienVat", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.HienVatConLai));
                        data.Add("Tong", ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.TuChiConLai) + ChungTuChiTietItems.Where(x => !x.IsHangCha).Sum(x => x.HienVatConLai));
                        data.Add("Tien_TuChi", StringUtils.NumberToText(TotalTuChiConLai * Convert.ToInt32(SelectedUnit.ValueItem)));
                        data.Add("Tien_HienVat", StringUtils.NumberToText(TotalHienVatConLai * Convert.ToInt32(SelectedUnit.ValueItem)));
                        string templateFileName = Path.Combine(ExportPrefix.PATH_TL_DT, string.Format("{0}.xls", fileName));
                        string fileNamePrefix = Path.GetFileNameWithoutExtension(templateFileName);
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        var xlsFile = _exportService.Export<DtChungTuChiTietModel>(templateFileName, data);
                        e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                    }
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
    }
}
