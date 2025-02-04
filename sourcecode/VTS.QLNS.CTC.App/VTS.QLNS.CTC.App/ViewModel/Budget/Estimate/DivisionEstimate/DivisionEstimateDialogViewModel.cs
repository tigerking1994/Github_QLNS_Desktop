using Aspose.Cells.Drawing.Texts;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.SqlServer.Management.XEvent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate
{
    public class DivisionEstimateDialogViewModel : DialogViewModelBase<DtChungTuModel>
    {
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDtNhanPhanBoMapService _dtChungTuMapService;
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly INsDcChungTuService _chungTuDieuChinhService;
        private readonly IMapper _mapper;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;
        private ICollectionView _dataDotPhanBoView;
        private bool _isNamLuyKe;
        private SessionInfo _sessionInfo;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler RefreshVoucherEvent;
        public override Type ContentType => typeof(View.Budget.Estimate.DivisionEstimateDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới chứng từ phân bổ dự toán" : "Cập nhật chứng từ phân bổ dự toán";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        bool bDeleteDetail { get; set; }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;

        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        private ObservableCollection<CheckBoxItem> _dataUnit;
        public ObservableCollection<CheckBoxItem> DataUnit
        {
            get => _dataUnit;
            set => SetProperty(ref _dataUnit, value);
        }

        public string _selectedCountLNS;
        public string SelectedCountLNS
        {
            get
            {
                int totalCount = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Where(x => x.IsFilter).Count(item => item.IsSelected) : 0;
                return $"CHỌN LNS ({totalSelected}/{totalCount})";
            }
        }

        public bool Flag { get; set; } = false;

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Where(x => x.IsFilter).Any()) ? false : DataLNS.Where(x => x.IsFilter).All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    Flag = true;
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
                    Flag = false;
                }
                OnPropertyChanged(nameof(SelectedCountLNS));
                if (_dataDotPhanBoView != null)
                {
                    _dataDotPhanBoView.Refresh();
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

        public string SelectedCountUnit
        {
            get
            {
                int totalCount = DataUnit != null ? DataUnit.Count : 0;
                int totalSelected = DataUnit != null ? DataUnit.Count(item => item.IsChecked) : 0;
                return $"CHỌN ĐƠN VỊ ({totalSelected}/{totalCount})";
            }
        }

        private bool _selectAllUnit;
        public bool SelectAllUnit
        {
            get => (DataUnit == null || !DataUnit.Any()) ? false : DataUnit.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllUnit, value);
                if (DataUnit != null)
                {
                    DataUnit.Select(c => { c.IsChecked = _selectAllUnit; return c; }).ToList();
                }
            }
        }

        public string LblPhanBo { get; set; }

        private ObservableCollection<DtChungTuModel> _dataDot;
        public ObservableCollection<DtChungTuModel> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ObservableCollection<DtChungTuModel> _initDataDot;
        public ObservableCollection<DtChungTuModel> InitDataDot
        {
            get => _initDataDot;
            set => SetProperty(ref _initDataDot, value);
        }

        private bool _selectAllDot;
        public bool SelectAllDot
        {
            get => (DataDot == null || !DataDot.Any()) ? false : DataDot.Where(x => x.IsFilter).All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDot, value);
                if (DataDot != null)
                {
                    DataDot.Where(x => x.IsFilter).Select(c => { c.IsChecked = _selectAllDot; return c; }).ToList();
                }
            }
        }

        public string SelectedCountDot
        {
            get
            {
                int totalCount = DataDot != null ? DataDot.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataDot != null ? DataDot.Where(x => x.IsFilter).Count(item => item.IsChecked) : 0;
                return $"{LblPhanBo} ({totalSelected}/{totalCount})";
            }
        }

        private string _searchUnit;
        public string SearchUnit
        {
            get => _searchUnit;
            set
            {
                SetProperty(ref _searchUnit, value);
                _dataUnitView.Refresh();
            }
        }

        private string _searchDotPhanBo;
        public string SearchDotPhanBo
        {
            get => _searchDotPhanBo;
            set
            {
                SetProperty(ref _searchDotPhanBo, value);
                _dataDotPhanBoView.Refresh();
            }
        }

        private ComboboxItem _cbxVoucherTypeSelected;
        public ComboboxItem CbxVoucherTypeSelected
        {
            get => _cbxVoucherTypeSelected;
            set
            {
                SetProperty(ref _cbxVoucherTypeSelected, value);
                if (_cbxVoucherTypeSelected != null)
                {
                    if (Model != null && Guid.Empty.Equals(Model.Id))
                    {
                        LoadChungTuIndex();
                    }
                    LoadAgencies();
                    LoadLNS();
                    LoadDataDotNhanCustom();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxBudgetTypeSelected;
        public ComboboxItem CbxBudgetTypeSelected
        {
            get => _cbxBudgetTypeSelected;
            set
            {
                if (SetProperty(ref _cbxBudgetTypeSelected, value))
                {
                    LoadAgencies();
                    LoadLNS();
                    LoadDataDotNhanCustom();
                    OnPropertyChanged(nameof(IsAdjusted));
                }
            }
        }

        public bool IsAdjusted
        {
            get
            {
                if (_cbxBudgetTypeSelected != null)
                {
                    var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                    if (BudgetType.ADJUSTED.Equals(budgetType))
                        return true;
                }
                return false;
            }
        }

        private ObservableCollection<ComboboxItem> _cbxBudgetType;
        public ObservableCollection<ComboboxItem> CbxBudgetType
        {
            get => _cbxBudgetType;
            set => SetProperty(ref _cbxBudgetType, value);
        }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                LoadLNS();
                if (_dataDotPhanBoView != null)
                {
                    _dataDotPhanBoView.Refresh();
                }
            }
        }

        List<Guid> selecteds = new List<Guid>();
        DateTime? InitDNgayChungTu { get; set; }
        DateTime? InitDNgayQuyetDinh { get; set; }

        public DivisionEstimateDialogViewModel(
            INsMucLucNganSachService nsMucLucNganSachService,
            IMapper mapper,
            INsDtChungTuService dtChungTuService,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            INsDtNhanPhanBoMapService dtChungTuMapService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDcChungTuService chungTuDieuChinhService,
            IDanhMucService danhMucService)
        {
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _mapper = mapper;
            _sessionService = sessionService;
            _dtChungTuService = dtChungTuService;
            _nSDonViService = nSDonViService;
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _danhMucService = danhMucService;
            _chungTuDieuChinhService = chungTuDieuChinhService;
        }

        public override void Init()
        {
            bDeleteDetail = false;
            IsSaveData = true;
            DataDot = new ObservableCollection<DtChungTuModel>();
            _sessionInfo = _sessionService.Current;
            LoadNamLuyKe();
            LoadBudgetType();
            LoadVoucherType();
            LoadAgencies();
            TabIndex = ImportTabIndex.Data;
            LoadData();
            CheckLastDivisionEstimateVoucher();
        }
        private void CheckLastDivisionEstimateVoucher()
        {
            if (Model != null && !Model.Id.IsNullOrEmpty())
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                predicate = predicate.And(x => x.ILoai == Model.ILoai);
                predicate = predicate.And(x => x.ILoaiChungTu == Model.ILoaiChungTu);
                predicate = predicate.And(x => x.Id != Model.Id);

                var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
                listChungTu = listChungTu.Where(x => x.IIdDotNhan.Split(",").Intersect(Model.IIdDotNhan.Split(",").ToList()).Any()).ToList();

                if (listChungTu.Count > 0)
                {
                    var maxDate = listChungTu.Select(x => { return x.DNgayQuyetDinh.HasValue ? x.DNgayQuyetDinh.Value.Date : x.DNgayChungTu.Value.Date; }).Max(x => x);
                    var modelDate = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    if (listChungTu.Any(n => n.ISoChungTuIndex > Model.ISoChungTuIndex) && modelDate == maxDate)
                    {
                        MessageBoxHelper.Info(string.Format(Resources.AlertUpdateDivisionEstimate, Model.SSoChungTu));
                    }
                    else if (listChungTu.Any() && modelDate < maxDate)
                        MessageBoxHelper.Info(string.Format(Resources.AlertUpdateDivisionEstimate, Model.SSoChungTu));
                    else
                    {
                        List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(Model.Id).ToList();
                        if (dtNhanPhanBoMaps.Count() > 0)
                            MessageBoxHelper.Info(Resources.AlertDivisionEstimateAdjusted);
                    }
                }
            }
        }


        private void LoadNamLuyKe()
        {
            DanhMuc dmNamLuyKe = _danhMucService.FindByCode(MaDanhMuc.NAM_LUY_KE, _sessionService.Current.YearOfWork);
            if (dmNamLuyKe != null)
                bool.TryParse(dmNamLuyKe.SGiaTri, out _isNamLuyKe);
            else _isNamLuyKe = false;
        }

        private void LoadBudgetType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.YEAR], ValueItem = ((int) BudgetType.YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.LAST_YEAR], ValueItem = ((int) BudgetType.LAST_YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL], ValueItem = ((int) BudgetType.ADDITIONAL).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR], ValueItem = ((int)BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR).ToString()},
                new ComboboxItem {DisplayItem = VoucherType.BudgetTypeName[BudgetType.ADJUSTED], ValueItem = ((int) BudgetType.ADJUSTED).ToString()}
            };

            CbxBudgetType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiDuToan.HasValue)
            {
                _cbxBudgetTypeSelected = CbxBudgetType.Single(item => item.ValueItem.Equals(Model.ILoaiDuToan.ToString()));
            }
            else if (_sessionService.Current.YearOfBudget != NAM_NGAN_SACH.NAM_NAY)
            {
                _cbxBudgetTypeSelected = CbxBudgetType.SingleOrDefault(item => item.ValueItem.Equals(((int)BudgetType.LAST_YEAR).ToString()));
            }
            else
            {
                _cbxBudgetTypeSelected = CbxBudgetType.First();
            }
        }

        private void LoadVoucherType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = VoucherType.NSSD_Value, ValueItem = VoucherType.NSSD_Key},
            };

            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => true.Equals(x.BCoNSNganh));
            predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai));
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            var listDonVi = _nSDonViService.FindByCondition(predicate).ToList();
            if (listDonVi.Any())
            {
                cbxVoucher.Add(new ComboboxItem { DisplayItem = VoucherType.NSBD_Value, ValueItem = VoucherType.NSBD_Key });
            }

            CbxVoucherType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiChungTu.HasValue)
            {
                _cbxVoucherTypeSelected = CbxVoucherType.Where(item => item.ValueItem.Equals(Model.ILoaiChungTu.ToString()))
                    .Select(x => x).DefaultIfEmpty(CbxVoucherType.ElementAt(0)).FirstOrDefault();
            }
            else
            {
                _cbxVoucherTypeSelected = CbxVoucherType.ElementAt(0);
            }
        }

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                // Add
                Model = new DtChungTuModel()
                {
                    DNgayChungTu = DateTime.Now,
                    DNgayQuyetDinh = null,
                    SSoQuyetDinh = string.Empty
                };
                LoadChungTuIndex();
                Model.DNgayChungTu = DateTime.Now;
                Model.DNgayQuyetDinh = DateTime.Now;
                Model.PropertyChanged += DialogModel_PropertyChanged;
            }
            else
            {
                InitDNgayChungTu = Model.DNgayChungTu;
                InitDNgayQuyetDinh = Model.DNgayQuyetDinh;
                EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                {
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    BudgetSource = _sessionInfo.Budget,
                    IdNhanPhanBos = Model.IIdDotNhan,
                };
                List<NsDtChungTuDotNhanQuery> listDotNhanQuery = _dtChungTuService.FindChungTuDotNhan(condition, false).ToList();
                if (_isNamLuyKe)
                {
                    NsDtChungTuDotNhanQuery dotNhan = new NsDtChungTuDotNhanQuery
                    {
                        SSoChungTu = string.Join(",", listDotNhanQuery.Select(x => x.SSoChungTu).ToList()),
                        DNgayChungTu = listDotNhanQuery.Max(x => x.DNgayChungTu),
                        SSoQuyetDinh = string.Join(",", listDotNhanQuery.Select(x => x.SSoQuyetDinh).ToList()),
                        DNgayQuyetDinh = listDotNhanQuery.Max(x => x.DNgayQuyetDinh),
                        SoPhanBo = listDotNhanQuery.Sum(x => x.SoPhanBo),
                        SoChuaPhanBo = listDotNhanQuery.Sum(x => x.SoChuaPhanBo),
                        IdLuyKe = Model.IIdDotNhan
                    };

                    listDotNhanQuery = new List<NsDtChungTuDotNhanQuery> { dotNhan };
                }

                DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDotNhanQuery);
                DataDot.ForAll(x => { x.IsChecked = true; });
                BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(_dataLNS, Model.SDslns);
                CheckboxSelectedToStringConvert.SetCheckboxSelected(_dataUnit, Model.SDsidMaDonVi);

                List<NsDtChungTuChiTiet> listChungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(Model.Id.ToString()).ToList();
                List<string> listLNS = listChungTuChiTiet.Select(x => x.SLns).ToList();
                List<string> listDonVi = listChungTuChiTiet.Select(x => x.IIdMaDonVi).ToList();
                List<Guid> listIdNhan = listChungTuChiTiet.Where(x => x.IIdCtduToanNhan.HasValue).Select(x => (Guid)x.IIdCtduToanNhan).ToList();
                _dataLNS.Where(x => listLNS.Contains(x.Lns)).ForAll(x => x.IsHitTestVisible = false);
                _dataUnit.Where(x => listDonVi.Contains(x.ValueItem)).ForAll(x => x.IsHitTestVisible = false);
                DataDot.Where(x => listIdNhan.Contains(x.Id)).ForAll(x => x.IsEnabled = true);

                InitDataDot = DataDot;

                if (Model.ILoaiDuToan.Value != (int)BudgetType.ADJUSTED)
                {
                    selecteds.AddRange(Model.IIdDotNhan.Split(',').Select(c => Guid.Parse(c)));
                    Model.PropertyChanged += DialogModel_PropertyChanged;
                }
                else
                {
                    DataDot.ForAll(c => c.IsEnabled = false);
                }
            }
            LoadDataDotNhanCustom();
        }

        private void LoadChungTuIndex()
        {
            var predicate = CreatePredicateChungTuPhanBo();
            int soChungTuIndex = _dtChungTuService.FindNextSoChungTuIndex(predicate);
            Model.SSoChungTu = "CT-" + soChungTuIndex.ToString("D3");
            Model.ISoChungTuIndex = soChungTuIndex;
            OnPropertyChanged(nameof(Division));
        }

        private void DialogModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Model.DNgayChungTu) && Model.DNgayQuyetDinh == null && _cbxBudgetTypeSelected != null)
            {
                LoadDataDotNhanCustom();
            }
            if (args.PropertyName == nameof(Model.DNgayQuyetDinh) && _cbxBudgetTypeSelected != null)
            {
                LoadDataDotNhanCustom();
            }
            DataChangedEventHandler handler = RefreshVoucherEvent;

            if (handler != null)
            {
                handler(Model, new EventArgs());
            }
        }

        private void LoadDataDotNhanCustom()
        {
            Expression<Func<NsDtChungTu, bool>> predicate;
            IEnumerable<NsDtChungTu> listChungTu = new List<NsDtChungTu>();
            DataDot = new ObservableCollection<DtChungTuModel>();
            if (_cbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (BudgetType.ADJUSTED.Equals(budgetType))
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    DateTime date = DateTime.Parse(dateTime.ToString());
                    LblPhanBo = "ĐỢT ĐÃ PHÂN BỔ";
                    predicate = CreatePredicateChungTuPhanBo();
                    predicate = predicate.And(x => !x.ILoaiDuToan.Equals(SoChungTuType.ReceiveEstimate));
                    predicate = predicate.And(x => (x.DNgayQuyetDinh.HasValue && x.DNgayQuyetDinh.Value.Date <= dateTime) || (!x.DNgayQuyetDinh.HasValue && x.DNgayChungTu.HasValue && x.DNgayChungTu.Value.Date <= dateTime));
                    listChungTu = _dtChungTuService.FindByCondition(predicate);

                    if (Guid.Empty.Equals(Model.Id))
                    {
                        List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByListIdNhanDuToan(listChungTu.Select(x => x.Id.ToString()).ToList()).ToList();
                        listChungTu = listChungTu.Where(x => !dtNhanPhanBoMaps.Select(x => x.IIdCtduToanNhan).Contains(x.Id)).ToList();
                    }
                    else
                    {
                        listChungTu = listChungTu.Where(x => x.Id != Model.Id).ToList();
                    }

                    DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
                }
                else
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value : Model.DNgayChungTu.Value;
                    LblPhanBo = "ĐỢT NHẬN PHÂN BỔ";
                    EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        EstimationType = SoChungTuType.ReceiveEstimate,
                        VoucherType = int.Parse(_cbxVoucherTypeSelected.ValueItem),
                        Date = dateTime,
                        SoChungTuIndex = (int)Model.ISoChungTuIndex
                    };

                    List<NsDtChungTuDotNhanQuery> listDotNhanQuery = _dtChungTuService.FindAllChungTuDotNhan(condition).ToList();

                    if (_isNamLuyKe && listDotNhanQuery.Count > 0)
                    {
                        predicate = CreatePredicateChungTuPhanBo();
                        var listChungTuPhanBo = _dtChungTuService.FindByCondition(predicate);
                        List<List<string>> listIdDotNhan = listChungTuPhanBo.Select(x => x.IIdDotNhan.Split(",").Distinct().ToList()).Distinct().ToList();
                        foreach (var dotNhan in listDotNhanQuery)
                        {
                            if (!listIdDotNhan.Any(x => x.Contains(dotNhan.Id.ToString())))
                                listIdDotNhan.Add(new List<string> { dotNhan.Id.ToString() });
                        }
                        List<NsDtChungTuDotNhanQuery> listDotNhanQueryLuyKe = new List<NsDtChungTuDotNhanQuery>();
                        foreach (var idDotNhan in listIdDotNhan)
                        {
                            string idLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id));
                            if (!listDotNhanQueryLuyKe.Any(x => x.IdLuyKe == idLuyKe))
                            {
                                NsDtChungTuDotNhanQuery dotNhanQuery = new NsDtChungTuDotNhanQuery
                                {
                                    IdLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id)),
                                    SSoChungTu = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoChungTu)),
                                    DNgayChungTu = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayChungTu),
                                    SSoQuyetDinh = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoQuyetDinh)),
                                    DNgayQuyetDinh = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayQuyetDinh),
                                    SoPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.SoPhanBo),
                                    SoChuaPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.SoChuaPhanBo),
                                    SDslns = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDslns)),
                                    SDsidMaDonVi = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDsidMaDonVi)),
                                };
                                listDotNhanQueryLuyKe.Add(dotNhanQuery);
                            }
                        }
                        DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDotNhanQueryLuyKe);
                    }
                    else
                    {
                        DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDotNhanQuery);
                    }
                }
            }

            _dataDotPhanBoView = CollectionViewSource.GetDefaultView(DataDot);
            _dataDotPhanBoView.Filter = ListDataDotPhanBoFilter;

            OnPropertyChanged(nameof(DataDot));
            OnPropertyChanged(nameof(SelectedCountDot));
            OnPropertyChanged(nameof(SelectAllDot));

            if (_dataDot != null && _dataDot.Count > 0)
            {
                foreach (var model in _dataDot)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            if (_isNamLuyKe && _cbxBudgetTypeSelected != null && !BudgetType.ADJUSTED.Equals((BudgetType)int.Parse(_cbxBudgetTypeSelected.ValueItem)))
                            {
                                var ngayChungTu = model.DNgayQuyetDinh.HasValue ? model.DNgayQuyetDinh.Value.Date : model.DNgayChungTu.Value.Date;
                                _dataDot.Where(item => ((item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date <= ngayChungTu) ||
                                                         item.DNgayChungTu.HasValue && item.DNgayChungTu.Value.Date <= ngayChungTu)).ForAll(item => item.IsChecked = model.IsChecked);
                                if (!string.IsNullOrEmpty(model.IdLuyKe) && _dataDot.Any(x => string.IsNullOrEmpty(x.IdLuyKe) && x.IsChecked))
                                {
                                    model.IsChecked = true;
                                }
                            }

                            var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                            if ((DataDot.Any(x => x.IsChecked) && (BudgetType.ADDITIONAL.Equals(budgetType) || BudgetType.ADDITIONAL_TRANSFER_LAST_YEAR.Equals(budgetType)))
                                && (Model == null || Model.Id == Guid.Empty))
                            {
                                var selectedDotNhan = _dataDot.Where(x => x.IsChecked).ToList();
                                var iDs = selectedDotNhan.Where(x => !string.IsNullOrEmpty(x.IIDChungTuDieuChinh)).SelectMany(x => x.IIDChungTuDieuChinh.Split(",")).ToList();
                                var sIDs = string.Join(",", iDs);
                                List<string> donViDieuChinh = new List<string>();
                                if (!string.IsNullOrEmpty(sIDs))
                                {
                                    donViDieuChinh = _chungTuDieuChinhService.GetDonViDieuChinh(sIDs, _sessionInfo.YearOfWork);
                                    var unitMap = DataUnit.Where(x => donViDieuChinh.Contains(x.ValueItem));
                                    unitMap.ForAll(a => a.IsChecked = true);
                                }
                            }

                            OnPropertyChanged(nameof(SelectAllDot));
                            OnPropertyChanged(nameof(SelectedCountDot));
                        }

                    };
                }
            }

            SetCheckedChungTuNhan();
        }

        private void LoadDataDotNhan()
        {
            Expression<Func<NsDtChungTu, bool>> predicate;
            IEnumerable<NsDtChungTu> listChungTu = new List<NsDtChungTu>();
            DataDot = new ObservableCollection<DtChungTuModel>();
            if (_cbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (BudgetType.ADJUSTED.Equals(budgetType))
                {
                    DateTime? dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    LblPhanBo = "ĐỢT ĐÃ PHÂN BỔ";
                    predicate = CreatePredicateChungTuPhanBo();
                    predicate = predicate.And(x => !string.IsNullOrEmpty(x.SSoQuyetDinh));
                    predicate = predicate.And(x => (x.DNgayQuyetDinh.HasValue && x.DNgayQuyetDinh.Value.Date <= dateTime) || (!x.DNgayQuyetDinh.HasValue && x.DNgayChungTu.HasValue && x.DNgayChungTu.Value.Date <= dateTime));
                    predicate = predicate.And(x => x.FTongTuChi > 0 || x.FTongHienVat != 0 || x.FTongHangMua != 0 || x.FTongHangNhap != 0 || x.FTongPhanCap != 0 || x.FTongDuPhong != 0);
                    listChungTu = _dtChungTuService.FindByCondition(predicate);

                    if (Guid.Empty.Equals(Model.Id))
                    {
                        List<NsDtNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByListIdNhanDuToan(listChungTu.Select(x => x.Id.ToString()).ToList()).ToList();
                        listChungTu = listChungTu.Where(x => !dtNhanPhanBoMaps.Select(x => x.IIdCtduToanNhan).Contains(x.Id)).ToList();
                    }
                    else
                        listChungTu = listChungTu.Where(x => x.Id != Model.Id).ToList();
                    DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listChungTu);
                }
                else
                {
                    LblPhanBo = "ĐỢT NHẬN PHÂN BỔ";
                    EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        EstimationType = SoChungTuType.ReceiveEstimate,
                        VoucherType = int.Parse(_cbxVoucherTypeSelected.ValueItem),
                        Date = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value : Model.DNgayChungTu.Value,
                        SoChungTuIndex = (int)Model.ISoChungTuIndex
                    };
                    List<NsDtChungTuDotNhanQuery> listDotNhanQuery = _dtChungTuService.FindAllChungTuDotNhan(condition).ToList();

                    if (_isNamLuyKe && listDotNhanQuery.Count > 0)
                    {
                        predicate = CreatePredicateChungTuPhanBo();
                        var listChungTuPhanBo = _dtChungTuService.FindByCondition(predicate);
                        List<List<string>> listIdDotNhan = listChungTuPhanBo.Select(x => x.IIdDotNhan.Split(",").Distinct().ToList()).Distinct().ToList();
                        foreach (var dotNhan in listDotNhanQuery)
                        {
                            if (!listIdDotNhan.Any(x => x.Contains(dotNhan.Id.ToString())))
                                listIdDotNhan.Add(new List<string> { dotNhan.Id.ToString() });
                        }
                        List<NsDtChungTuDotNhanQuery> listDotNhanQueryLuyKe = new List<NsDtChungTuDotNhanQuery>();
                        foreach (var idDotNhan in listIdDotNhan)
                        {
                            string idLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id));
                            if (!listDotNhanQueryLuyKe.Any(x => x.IdLuyKe == idLuyKe))
                            {
                                NsDtChungTuDotNhanQuery dotNhanQuery = new NsDtChungTuDotNhanQuery
                                {
                                    IdLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id)),
                                    SSoChungTu = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoChungTu)),
                                    DNgayChungTu = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayChungTu),
                                    SSoQuyetDinh = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoQuyetDinh)),
                                    DNgayQuyetDinh = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayQuyetDinh),
                                    SoPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.SoPhanBo),
                                    SoChuaPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.SoChuaPhanBo),
                                    SDslns = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDslns)),
                                    SDsidMaDonVi = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDsidMaDonVi)),
                                };
                                listDotNhanQueryLuyKe.Add(dotNhanQuery);
                            }
                        }
                        DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDotNhanQueryLuyKe);
                    }
                    else
                    {
                        DataDot = _mapper.Map<ObservableCollection<DtChungTuModel>>(listDotNhanQuery);
                    }
                }
            }

            _dataDotPhanBoView = CollectionViewSource.GetDefaultView(DataDot);
            _dataDotPhanBoView.Filter = ListDataDotPhanBoFilter;

            OnPropertyChanged(nameof(DataDot));
            OnPropertyChanged(nameof(SelectedCountDot));
            OnPropertyChanged(nameof(SelectAllDot));

            if (_dataDot != null && _dataDot.Count > 0)
            {
                foreach (var model in _dataDot)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            if (_isNamLuyKe && _cbxBudgetTypeSelected != null && !BudgetType.ADJUSTED.Equals((BudgetType)int.Parse(_cbxBudgetTypeSelected.ValueItem)))
                            {
                                var ngayChungTu = model.DNgayQuyetDinh.HasValue ? model.DNgayQuyetDinh.Value.Date : model.DNgayChungTu.Value.Date;
                                _dataDot.Where(item => ((item.DNgayQuyetDinh.HasValue && item.DNgayQuyetDinh.Value.Date <= ngayChungTu) ||
                                                         item.DNgayChungTu.HasValue && item.DNgayChungTu.Value.Date <= ngayChungTu)).ForAll(item => item.IsChecked = model.IsChecked);
                                if (!string.IsNullOrEmpty(model.IdLuyKe) && _dataDot.Any(x => string.IsNullOrEmpty(x.IdLuyKe) && x.IsChecked))
                                {
                                    model.IsChecked = true;
                                }
                            }

                            var listLnsOfModel = model.SDslns.Split(StringUtils.COMMA);
                            var listLns = DataLNS.Where(x => x.IsSelected && !listLnsOfModel.Contains(x.Lns)).Select(x => x.Lns).ToList();
                            var listLnsDotNhan = DataDot.Where(item => item.IsChecked).SelectMany(item => item.SDslns.Split(StringUtils.COMMA)).ToList();
                            listLns.AddRange(listLnsDotNhan);

                            var listNsMucLucNganSach = new List<NsMuclucNgansachModel>(DataLNS);
                            listNsMucLucNganSach.ForEach(item => item.IsSelected = listLns.Contains(item.Lns));
                            DataLNS = new ObservableCollection<NsMuclucNgansachModel>(listNsMucLucNganSach);
                            LoadLNSPropertyChanged();

                            OnPropertyChanged(nameof(SelectAllDot));
                            OnPropertyChanged(nameof(SelectedCountDot));
                        }
                    };
                }
            }

            SetCheckedChungTuNhan();
        }

        private List<DtChungTuModel> CalculatorTotalDotNhanLuyKe(List<NsDtChungTuChiTietQuery> chungTuChiTiets)
        {
            var dtChungTuMapByIdNhanDuToan = _dtChungTuMapService.FindByListIdNhanDuToan(DataDot.Select(x => x.Id.ToString())).ToList();
            var dictByNhanDuToan = dtChungTuMapByIdNhanDuToan.GroupBy(x => x.IIdCtduToanNhan.ToString())
                .ToDictionary(x => x.Key, x => x.ToList());

            var dictByChungTuId = chungTuChiTiets.GroupBy(x => x.IIdDtchungTu.ToString()).ToDictionary(x => x.Key, x => x.ToList());
            var dictByChungTuIdForCalculator = chungTuChiTiets.GroupBy(x => x.IIdDtchungTu.ToString()).ToDictionary(x => x.Key, x => x.ToList());

            var usedBudgetEstimateDivisionMapByCategoryId = chungTuChiTiets
                .Where(budget => budget.IIdCtduToanNhan.HasValue && (Guid.Empty.Equals(Model.Id) || !Model.Id.Equals(budget.IIdDtchungTu)))
                .GroupBy(budget => budget.IIdCtduToanNhan.ToString())
                .ToDictionary(g => g.Key, g => g.GroupBy(e => e.IIdMlns.ToString()).ToDictionary(e => e.Key, e => e.ToList()));

            var listItemAvailable = new List<DtChungTuModel>();
            var listItemUsed = new List<DtChungTuModel>();

            foreach (var nhanPhanBo in DataDot)
            {
                var detailTotal = new DivisionEstimateDetailPropertyHelper();
                nhanPhanBo.DetailTotal = detailTotal;

                var listPhanBoDuToanId = dictByNhanDuToan
                    .GetValueOrDefault(nhanPhanBo.Id.ToString(), new List<NsDtNhanPhanBoMap>())
                    .Select(x => x.IIdCtduToanPhanBo.ToString()).ToHashSet();
                var listPhanBoDuToanIdUsed = listPhanBoDuToanId.Where(x => dictByChungTuId.ContainsKey(x)).ToList();

                var isLuyKe = Guid.Empty.Equals(Model.Id) ? listPhanBoDuToanId.Any() : listPhanBoDuToanIdUsed.Any();

                var dataUsed = new List<NsDtChungTuChiTietQuery>();
                if (isLuyKe)
                {
                    dataUsed = listPhanBoDuToanIdUsed.SelectMany(x =>
                        {
                            var dataOfDict = new List<NsDtChungTuChiTietQuery>();
                            if (dictByChungTuIdForCalculator.ContainsKey(x))
                            {
                                dataOfDict = dictByChungTuIdForCalculator[x];
                                dictByChungTuIdForCalculator.Remove(x);
                            }

                            return dataOfDict;
                        }
                    ).ToList();
                }
                else
                {
                    dataUsed = usedBudgetEstimateDivisionMapByCategoryId.GetValueOrDefault(nhanPhanBo.Id.ToString(), new Dictionary<string, List<NsDtChungTuChiTietQuery>>())
                        .SelectMany(x => x.Value).ToList();
                }

                detailTotal.TotalPhanBo = nhanPhanBo.FTongTuChi + nhanPhanBo.FTongHienVat + nhanPhanBo.FTongHangNhap + nhanPhanBo.FTongHangMua
                                        + nhanPhanBo.FTongDuPhong + nhanPhanBo.FTongTonKho;
                double daPhanBo = dataUsed.Sum(x => x.FTuChi + x.FHienVat + x.FHangNhap + x.FHangMua + x.FPhanCap + x.FDuPhong);
                detailTotal.TotalChuaPhanBo = detailTotal.TotalPhanBo - daPhanBo;

                listItemAvailable.Add(nhanPhanBo);
            }
            return listItemAvailable;
        }

        private bool ListDataDotPhanBoFilter(object obj)
        {
            var item = (DtChungTuModel)obj;
            bool res = true;
            if (DataLNS.Any(x => x.IsSelected))
            {
                res = res && item.SDslns.Split(",").Any(x => DataLNS.Where(x => x.IsSelected).Select(y => y.Lns).Contains(x));
            }
            else
            {
                res = false;
            }

            if (TabIndex == ImportTabIndex.Data && _cbxBudgetTypeSelected != null && (BudgetType)int.Parse(_cbxBudgetTypeSelected.ValueItem) == BudgetType.YEAR)
            {
                res = res && (item.SoChuaPhanBo > 0 ||
                    (item.SoPhanBo == 0 && item.SoChuaPhanBo == 0) ||
                    (!string.IsNullOrEmpty(Model.IIdDotNhan) && Model.IIdDotNhan.Split(",").ToList().Contains(item.Id.ToString())));
            }

            if (!string.IsNullOrWhiteSpace(_searchDotPhanBo))
            {
                res = res && item.SSoChungTu.ToLower().Contains(_searchDotPhanBo, StringComparison.OrdinalIgnoreCase);
            }
            item.IsFilter = res;
            return res;
        }

        private List<NsDtChungTuChiTietQuery> GetListDataTotalBudgetUsed(string procedure)
        {
            if (!DataDot.Any())
            {
                return new List<NsDtChungTuChiTietQuery>();
            }

            var searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Guid.Empty,
                ChungTuId = string.Join(",", DataDot.Select(x => x.Id.ToString())),
                LNS = string.Empty,
                YearOfWork = _sessionService.Current.YearOfWork,
                YearOfBudget = _sessionService.Current.YearOfBudget,
                BudgetSource = _sessionService.Current.Budget,
                SoChungTu = string.IsNullOrEmpty(Model.SSoChungTu) ? string.Empty : Model.SSoChungTu
            };
            if (Model.DNgayQuyetDinh.HasValue)
                searchCondition.VoucherDate = Model.DNgayQuyetDinh;
            else if (Model.DNgayChungTu.HasValue)
                searchCondition.VoucherDate = Model.DNgayChungTu;
            else searchCondition.VoucherDate = DateTime.Now;
            var listDetail = _dtChungTuChiTietService.FindByCond(searchCondition, procedure: procedure).ToList();
            return listDetail;
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var listNsMucLucNganSach = new List<NsMucLucNganSach>();
            List<NsMuclucNgansachModel> listNsMucLucNganSachModel = new List<NsMuclucNgansachModel>();
            if (_cbxBudgetTypeSelected != null)
            {
                int loaiChungTu = int.Parse(VoucherType.NSSD_Key);
                if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
                {
                    loaiChungTu = int.Parse(VoucherType.NSBD_Key);
                }
                listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, NSEntityStatus.ACTIVED, loaiChungTu).ToList();

                listNsMucLucNganSachModel = _mapper.Map<List<NsMuclucNgansachModel>>(listNsMucLucNganSach);

                if (TabIndex == ImportTabIndex.Data)
                {
                    var predicate = PredicateBuilder.True<NsDtChungTu>();
                    predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
                    predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
                    predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
                    predicate = predicate.And(x => x.ILoai == 0);

                    var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
                    if (listChungTu.Any())
                    {
                        var listLNS = listChungTu.SelectMany(x => x.SDslns.Split(",")).Distinct().ToList();
                        listNsMucLucNganSachModel = listNsMucLucNganSachModel.Where(x => listLNS.Contains(x.Lns)).ToList();
                    }
                }
            }
            DataLNS = new ObservableCollection<NsMuclucNgansachModel>(listNsMucLucNganSachModel);
            LoadLNSPropertyChanged();
        }

        private void SetCheckChildren(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsIdParent == item.MlnsId)
                {
                    e.IsSelected = item.IsSelected;
                    SetCheckChildren(items, e);
                }
            }
        }

        private void SetCheckParent(ObservableCollection<NsMuclucNgansachModel> items, NsMuclucNgansachModel item)
        {
            foreach (var e in items)
            {
                if (e.MlnsId == item.MlnsIdParent)
                {
                    e.IsSelected = items.Where(x => x.MlnsIdParent == item.MlnsIdParent).All(x => x.IsSelected);
                    SetCheckParent(items, e);
                }
            }
        }

        private void LoadLNSPropertyChanged()
        {
            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;
            OnPropertyChanged(nameof(DataLNS));
            OnPropertyChanged(nameof(SelectAllLNS));
            OnPropertyChanged(nameof(SelectedCountLNS));

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected) && !Flag)
                        {
                            Flag = true;
                            SetCheckChildren(_dataLNS, model);
                            SetCheckParent(_dataLNS, model);
                            Flag = false;
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                            if (_dataDotPhanBoView != null)
                            {
                                _dataDotPhanBoView.Refresh();
                            }
                        }
                    };
                }
            }

        }

        private void LoadAgencies()
        {
            var listUnit = new List<DonVi>();
            if (_cbxBudgetTypeSelected != null)
            {
                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (!BudgetType.ADJUSTED.Equals(budgetType))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai));
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
                    if (_cbxVoucherTypeSelected != null && VoucherType.NSBD_Key.Equals(_cbxVoucherTypeSelected.ValueItem))
                    {
                        predicate = predicate.And(x => true.Equals(x.BCoNSNganh));
                    }
                    // remove 999 hard code
                    predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
                    listUnit = _nSDonViService.FindByCondition(predicate).OrderBy(n => n.IIDMaDonVi).ToList();
                }
            }

            DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);

            _dataUnitView = CollectionViewSource.GetDefaultView(DataUnit);
            _dataUnitView.Filter = ListUnitFilter;
            OnPropertyChanged(nameof(DataUnit));
            OnPropertyChanged(nameof(SelectAllUnit));
            OnPropertyChanged(nameof(SelectedCountUnit));

            if (_dataUnit != null && _dataUnit.Count > 0)
            {
                foreach (var model in _dataUnit)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(CheckBoxItem.IsChecked))
                        {
                            OnPropertyChanged(nameof(SelectAllUnit));
                            OnPropertyChanged(nameof(SelectedCountUnit));
                        }
                    };
                }
            }
        }

        private bool ListUnitFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchUnit))
            {
                return true;
            }
            return obj is CheckBoxItem item && item.DisplayItem.ToLower().Contains(_searchUnit!.Trim().ToLower());
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.LNSDisplay.ToLower().Contains(_searchLNS!.Trim().ToLower());
        }

        public override void OnSave()
        {
            string message = GetMessageValidate();
            if (!string.IsNullOrEmpty(message))
            {
                MessageBoxHelper.Error(message);
                return;
            }

            bDeleteDetail = false;
            string messageCheckBoxUnit = GetMessageValidateCheckBoxUnit();
            if (!string.IsNullOrEmpty(messageCheckBoxUnit))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxUnit);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    bDeleteDetail = false;
                    DataUnit.Select(n =>
                    {
                        if (!n.IsHitTestVisible)
                        {
                            n.IsChecked = true;
                        }
                        return n;
                    }).ToList();
                    _dataDotPhanBoView.Refresh();
                    return;
                }
            }

            string messageCheckBoxLNS = GetMessageValidateCheckBoxLNS();
            if (!string.IsNullOrEmpty(messageCheckBoxLNS))
            {
                MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBoxLNS);
                if (messageValidate.Equals(MessageBoxResult.Yes))
                {
                    bDeleteDetail = true;
                }
                else
                {
                    bDeleteDetail = false;
                    DataLNS.Select(n =>
                    {
                        if (!n.IsHitTestVisible)
                        {
                            n.IsSelected = true;
                        }
                        return n;
                    }).ToList();
                    _dataDotPhanBoView.Refresh();
                    return;
                }
            }

            var listDataDotSelected = DataDot.Where(n => n.IsChecked).Select(n => n.Id).ToList();
            var listDataDotExist = InitDataDot?.Where(n => _dtChungTuChiTietService.isExistEstimate(Model.Id, n.Id)).ToList();

            if (listDataDotExist != null)
            {
                var listDataDotUnchecked = listDataDotExist?.Where(n => !listDataDotSelected.Contains(n.Id)).Select(n => n.SSoChungTu);
                string listDuToanExist = string.Join(StringUtils.COMMA_SPLIT, listDataDotUnchecked);
                if (!string.IsNullOrEmpty(listDuToanExist))
                {
                    string message2 = string.Format(Resources.DivisionEstimateHasDataReceive, listDuToanExist, listDuToanExist, listDuToanExist);
                    var confirm = MessageBoxHelper.Confirm(message2);
                    if (confirm == MessageBoxResult.Yes)
                    {
                        listDataDotExist.ForAll(n => n.IsChecked = false);
                    }
                    else
                    {
                        DataDot.Select(n =>
                        {
                            if (listDataDotUnchecked.Contains(n.SSoChungTu))
                            {
                                n.IsChecked = true;
                            }
                            return n;
                        }).ToList();
                        _dataDotPhanBoView.Refresh();
                        return;
                    }
                }
            }

            IsSaveData = false;

            if (bDeleteDetail)
            {
                List<NsDtChungTuChiTiet> listChungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(Model.Id.ToString()).ToList();
                var listLNSHasDataUnchecked = _dataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
                var listUnitHasDataUnchecked = _dataUnit.Where(n => !n.IsHitTestVisible && !n.IsChecked).Select(n => n.ValueItem).ToList();
                listChungTuChiTiet = listChungTuChiTiet.Where(x => listUnitHasDataUnchecked.Contains(x.IIdMaDonVi) || listLNSHasDataUnchecked.Contains(x.SLns)).ToList();
                _dtChungTuChiTietService.RemoveRange(listChungTuChiTiet);
            }


            // get du toan chung tu mapping
            var listIdDotNhan = DataDot.Where(data => data.IsChecked && string.IsNullOrEmpty(data.IdLuyKe)).Select(data => data.Id.ToString()).ToList();
            var listIdDotNhanLuyKe = DataDot.Where(data => !string.IsNullOrEmpty(data.IdLuyKe)).SelectMany(data => data.IdLuyKe.Split(",")).ToList();
            listIdDotNhan.AddRange(listIdDotNhanLuyKe);

            var listGuidDotNhan = listIdDotNhan.Select(y =>
                Guid.TryParse(y, out Guid result) ? result : Guid.Empty);

            var listSDslns = _dtChungTuService.FindByCondition(x => listGuidDotNhan.Contains(x.Id)).SelectMany(x => x.SDslns.Split(",")).OrderBy(x => x).Distinct();

            if (Model == null) Model = new DtChungTuModel();
            //Model.SDslns = BudgetCatalogSelectedToStringConvert.GetValueSelectedSoOn(DataLNS, listSDslns);
            Model.SDslns = string.Join(",", listSDslns);
            Model.INamLamViec = _sessionInfo.YearOfWork;
            Model.IIdMaNguonNganSach = _sessionInfo.Budget;
            Model.INamNganSach = _sessionInfo.YearOfBudget;
            if (Model.ILoaiChungTu == null || Model.ILoaiDuToan.Value != (int)BudgetType.ADJUSTED)
            {
                Model.SDsidMaDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit);
            }
            Model.ILoai = SoChungTuType.EstimateDivision;
            Model.IIdDotNhan = string.Join(",", listIdDotNhan);
            Model.ILoaiChungTu = int.Parse(_cbxVoucherTypeSelected.ValueItem);
            Model.ILoaiDuToan = int.Parse(_cbxBudgetTypeSelected.ValueItem);
            NsDtChungTu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new NsDtChungTu();
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                entity.DNgayChungTu = Model.DNgayChungTu;
                entity.DNgayTao = DateTime.Now;
                entity.SNguoiTao = _sessionService.Current.Principal;
                entity = _dtChungTuService.Add(entity);
            }
            else
            {
                // Update
                entity = _dtChungTuService.FindById(Model.Id);
                _mapper.Map(Model, entity);
                entity.DNgayQuyetDinh = Model.DNgayQuyetDinh;
                entity.DNgayChungTu = Model.DNgayChungTu;
                entity.DNgaySua = DateTime.Now;
                entity.SNguoiSua = _sessionService.Current.Principal;
                _dtChungTuService.Update(entity);
            }

            if (BudgetType.ADJUSTED.Equals((BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem))))
            {
                //var listIdNhan = _dtChungTuMapService.FindByListIdPhanBo(listIdDotNhan).Select(n => n.IIdCtduToanNhan);
                var listIdNhan = _dtChungTuMapService.FindListIdByListIdPhanBo(listIdDotNhan, _sessionInfo.YearOfWork).Select(n => n.IIdCtduToanNhan);
                _dtChungTuMapService.DeleteByIdPhanBoDuToan(entity.Id);
                _dtChungTuChiTietService.DeleteByIdChungTuDuToanNhan(entity.Id, string.Join(",", listIdNhan));
            }
            else
            {
                _dtChungTuChiTietService.DeleteByIdChungTuDuToanNhan(entity.Id, string.Join(",", listIdDotNhan));
                // delete dtChungTuMap
                _dtChungTuMapService.DeleteByIdPhanBoDuToan(entity.Id);
            }

            // save dtChungTuMap
            var dtChungTuMaps = listIdDotNhan.Select(idDotNhan => new NsDtNhanPhanBoMap
            {
                IIdCtduToanPhanBo = entity.Id,
                IIdCtduToanNhan = new Guid(idDotNhan),
                SNguoiTao = _sessionService.Current.Principal,
                SNgaySua = _sessionService.Current.Principal,
                DNgayTao = DateTime.Now,
                DNgaySua = DateTime.Now
            }).ToList();
            _dtChungTuMapService.Save(dtChungTuMaps);

            UpdateChungTu(entity.Id);

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<DtChungTuModel>(entity));

        }

        private void UpdateChungTu(Guid id)
        {
            NsDtChungTu chungTu = _dtChungTuService.FindById(id);
            List<NsDtChungTuChiTiet> chungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(id.ToString()).ToList();
            var childs = chungTuChiTiet.Where(x => !x.BHangCha && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 ||
                                    x.FPhanCap != 0 || x.FDuPhong != 0)).ToList();

            chungTu.FTongTuChi = childs.Sum(x => x.FTuChi);
            chungTu.FTongHienVat = childs.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = childs.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = childs.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = childs.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = childs.Sum(x => x.FDuPhong);

            _dtChungTuService.Update(chungTu);

            Model.FTongTuChi = chungTu.FTongTuChi;
            Model.FTongHienVat = chungTu.FTongHienVat;
            Model.FTongHangNhap = chungTu.FTongHangNhap;
            Model.FTongHangMua = chungTu.FTongHangMua;
            Model.FTongPhanCap = chungTu.FTongPhanCap;
            Model.FTongDuPhong = chungTu.FTongDuPhong;
        }

        private string GetMessageValidate()
        {
            List<string> messages = new List<string>();

            if (!Model.DNgayChungTu.HasValue)
            {
                messages.Add(Resources.AlertNgayChungTuEmpty);
            }

            if (!(Model.DNgayQuyetDinh.HasValue && !string.IsNullOrEmpty(Model.SSoQuyetDinh) ||
                 (Model.DNgayQuyetDinh == null && string.IsNullOrEmpty(Model.SSoQuyetDinh))))
            {
                if (Model.DNgayQuyetDinh == null)
                {
                    messages.Add(Resources.AlertNgayQuyetDinhEmpty);
                }
                if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
                {
                    messages.Add(Resources.AlertSoQuyetDinhEmpty);
                }
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }

            if (DataDot.All(x => !x.IsChecked))
            {
                messages.Add("Hãy chọn đợt phân bổ");
            }

            if (_cbxBudgetTypeSelected == null)
            {
                messages.Add("Hãy chọn loại dự toán");
            }

            if (!messages.Any())
            {
                messages.AddRange(ValidateSoQuyetDinh());
                List<string> listIdDotNhan = new List<string>();

                var budgetType = (BudgetType)(int.Parse(_cbxBudgetTypeSelected.ValueItem));
                if (!BudgetType.ADJUSTED.Equals(budgetType))
                {
                    if (_dataUnit.All(x => !x.IsChecked))
                    {
                        messages.Add("Hãy chọn đơn vị");
                    }
                    if (DataLNS.All(x => !x.IsSelected))
                    {
                        messages.Add("Hãy chọn LNS");
                    }
                    if (!_isNamLuyKe)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.Id.ToString()).ToList();
                    }
                }
                else
                {
                    if (Model.ILoaiDuToan.HasValue && Model.ILoaiDuToan.Value != (int)BudgetType.ADJUSTED)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.IIdDotNhan.ToString()).ToList();
                        if (listIdDotNhan.Count() > 1)
                        {
                            messages.Add("Đ/c chỉ được điều chỉnh một chứng từ duy nhất.");
                        }
                    }

                }
                if (!_isNamLuyKe)
                {
                    if (!Guid.Empty.Equals(Model.Id))
                    {
                        DateTime? minDate;
                        minDate = DataDot.Where(x => x.IsChecked).Max(x => x.DNgayQuyetDinh);
                        if (!minDate.HasValue)
                            minDate = DataDot.Where(x => x.IsChecked).Max(x => x.DNgayChungTu);
                        var date = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh : Model.DNgayChungTu;
                        if (date.HasValue && minDate.HasValue && date.Value.Date < minDate.Value.Date)
                            messages.Add($"Ngày phân bổ phải lớn hơn ngày {minDate.Value.Date.ToString("dd/MM/yyyy")}");
                    }
                }
            }

            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidateCheckBoxUnit()
        {
            var listUnitHasDataUnchecked = _dataUnit.Where(n => !n.IsHitTestVisible && !n.IsChecked).Select(n => n.DisplayItem).ToList();
            string unitText = string.Join(StringUtils.COMMA_SPLIT, listUnitHasDataUnchecked);

            if (!string.IsNullOrEmpty(unitText))
            {
                return string.Format(Resources.DivisionEstimateHasDataUnit, unitText);
            }
            else return "";
        }

        private string GetMessageValidateCheckBoxLNS()
        {
            var listLNSHasDataUnchecked = _dataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.Lns).ToList();
            string lnsText = string.Join(StringUtils.COMMA_SPLIT, listLNSHasDataUnchecked);

            if (!string.IsNullOrEmpty(lnsText))
            {
                return string.Format(Resources.DivisionEstimateHasDataLNS, lnsText);
            }
            else return "";

        }

        private List<string> ValidateSoQuyetDinh()
        {
            List<string> messages = new List<string>();
            if (!string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                var predicate = PredicateBuilder.True<NsDtChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
                predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionService.Current.Budget);
                predicate = predicate.And(x => x.INamNganSach == _sessionService.Current.YearOfBudget);
                predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
                predicate = predicate.And(x => x.SSoQuyetDinh == Model.SSoQuyetDinh);
                if (!Guid.Empty.Equals(Model.Id))
                    predicate = predicate.And(x => x.Id != Model.Id);
                var listChungTu = _dtChungTuService.FindByCondition(predicate).ToList();
                if (listChungTu.Count > 0)
                {
                    if (listChungTu.Any(x => x.DNgayQuyetDinh.Value.Date != Model.DNgayQuyetDinh.Value.Date))
                    {
                        messages.Add(string.Format(Resources.VoucherValidateSoQuyetDinhNgayQuyetDinh, Model.SSoQuyetDinh, listChungTu.First().DNgayQuyetDinh.Value.ToString("dd/MM/yyyy")));
                    }
                }
            }
            return messages;
        }

        private Expression<Func<NsDtChungTu, bool>> CreatePredicateChungTuPhanBo()
        {
            var predicate = PredicateBuilder.True<NsDtChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
            predicate = predicate.And(x => x.IIdMaNguonNganSach == _sessionInfo.Budget);
            predicate = predicate.And(x => x.INamNganSach == _sessionInfo.YearOfBudget);
            predicate = predicate.And(x => x.ILoai == SoChungTuType.EstimateDivision);
            predicate = predicate.And(x => x.ILoaiChungTu == int.Parse(_cbxVoucherTypeSelected.ValueItem));
            return predicate;
        }

        public void SetCheckedChungTuNhan()
        {
            if (Model.IIdDotNhan != null)
            {
                var ids = Model.IIdDotNhan.Split(',').Select(c => Guid.Parse(c)).ToList();
                foreach (var dot in DataDot)
                {
                    if (ids != null && ids.Contains(dot.Id))
                    {
                        dot.IsChecked = true;
                    }
                }
            }

        }
    }
}
