using Aspose.Cells.Revisions;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.DivisionEstimate;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu
{
    public class PhanBoDuToanThuDialogViewModel : DialogViewModelBase<BhDtPhanBoChungTuModel>
    {
        private readonly IBhDmMucLucNganSachService _bhMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly IDttBHXHPhanBoService _dtChungTuService;
        private readonly IBhDttNhanPhanBoMapService _dtChungTuMapService;
        private readonly IDttBHXHPhanBoChiTietService _dtChungTuChiTietService;
        private readonly INsDonViService _nSDonViService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;
        private ICollectionView _dataDotPhanBoView;
        private bool _isNamLuyKe;
        private SessionInfo _sessionInfo;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler RefreshVoucherEvent;
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsuranceEstimate.PhanBoDuToanThu.PhanBoDuToanThuDialog);
        public override string Name => Guid.Empty.Equals(Model.Id) ? "THÊM MỚI ĐỢT PHÂN BỔ" : "CẬP NHẬT ĐỢT PHÂN BỔ";
        public override string Description => Guid.Empty.Equals(Model.Id) ? "Tạo mới đợt phân bổ dự toán thu BHXH" : "Cập nhật đợt phân bổ dự toán thu BHXH";
        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        bool bDeleteDetail { get; set; }

        private ObservableCollection<BhDmMucLucNganSachModel> _dataLNS;

        public ObservableCollection<BhDmMucLucNganSachModel> DataLNS
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

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Where(x => x.IsFilter).Any()) ? false : DataLNS.Where(x => x.IsFilter).All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Where(x => x.IsFilter).Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
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

        private ObservableCollection<BhDttBHXHModel> _dataDot;
        public ObservableCollection<BhDttBHXHModel> DataDot
        {
            get => _dataDot;
            set => SetProperty(ref _dataDot, value);
        }

        private ObservableCollection<BhDttBHXHModel> _initDataDot;
        public ObservableCollection<BhDttBHXHModel> InitDataDot
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

        private ObservableCollection<ComboboxItem> _cbxVoucherType;
        public ObservableCollection<ComboboxItem> CbxVoucherType
        {
            get => _cbxVoucherType;
            set => SetProperty(ref _cbxVoucherType, value);
        }

        private ComboboxItem _cbxEstimateTypeSelected;
        public ComboboxItem CbxEstimateTypeSelected
        {
            get => _cbxEstimateTypeSelected;
            set
            {
                SetProperty(ref _cbxEstimateTypeSelected, value);
                if (_cbxEstimateTypeSelected != null)
                {
                    LoadAgencies();
                    LoadLNS();
                    LoadDataDotNhanCustom();
                    OnPropertyChanged(nameof(IsAdjusted));
                    SelectAllLNS = true;
                }
            }
        }

        public bool IsAdjusted
        {
            get
            {
                if (_cbxEstimateTypeSelected != null)
                {
                    var estimateType = (EstimateTypeNum)(int.Parse(_cbxEstimateTypeSelected.ValueItem));
                    if (EstimateTypeNum.ADJUSTED.Equals(estimateType))
                        return true;
                }
                return false;
            }
        }

        public bool IsCheckDot { get; set; } = false;

        private bool _isInitilizationFirstTime;

        private ObservableCollection<ComboboxItem> _cbxEstimateType;
        public ObservableCollection<ComboboxItem> CbxEstimateType
        {
            get => _cbxEstimateType;
            set => SetProperty(ref _cbxEstimateType, value);
        }

        List<Guid> selecteds = new List<Guid>();
        DateTime? InitDNgayChungTu { get; set; }
        DateTime? InitDNgayQuyetDinh { get; set; }

        public PhanBoDuToanThuDialogViewModel(
            IBhDmMucLucNganSachService bhMucLucNganSachService,
            IMapper mapper,
            IDttBHXHPhanBoService dtChungTuService,
            ISessionService sessionService,
            INsDonViService nSDonViService,
            IBhDttNhanPhanBoMapService dtChungTuMapService,
            IDttBHXHPhanBoChiTietService dtChungTuChiTietService,
            IDanhMucService danhMucService)
        {
            _bhMucLucNganSachService = bhMucLucNganSachService;
            _mapper = mapper;
            _sessionService = sessionService;
            _dtChungTuService = dtChungTuService;
            _nSDonViService = nSDonViService;
            _dtChungTuMapService = dtChungTuMapService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _danhMucService = danhMucService;
        }

        public override void Init()
        {
            _isInitilizationFirstTime = false;
            bDeleteDetail = false;
            IsSaveData = true;
            DataDot = new ObservableCollection<BhDttBHXHModel>();
            _sessionInfo = _sessionService.Current;
            LoadEstimateType();
            LoadNamLuyKe();
            LoadVoucherType();
            LoadAgencies();
            LoadLNS();
            LoadData();
            CheckLastDivisionEstimateVoucher();
        }
        private void CheckLastDivisionEstimateVoucher()
        {
            if (Model != null && !Model.Id.IsNullOrEmpty())
            {
                var predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
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
                        List<BhDttNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByIdNhanDuToan(Model.Id).ToList();
                        if (dtNhanPhanBoMaps.Count() > 0)
                            MessageBoxHelper.Info(Resources.AlertDivisionEstimateAdjusted);
                    }
                }
            }
            _isInitilizationFirstTime = true;
        }


        private void LoadNamLuyKe()
        {
            DanhMuc dmNamLuyKe = _danhMucService.FindByCode(MaDanhMuc.NAM_LUY_KE, _sessionService.Current.YearOfWork);
            if (dmNamLuyKe != null)
                bool.TryParse(dmNamLuyKe.SGiaTri, out _isNamLuyKe);
            else _isNamLuyKe = false;
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
        }

        public override void LoadData(params object[] args)
        {
            if (Model == null || Model.Id == Guid.Empty)
            {
                // Add
                Model = new BhDtPhanBoChungTuModel()
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
                if (Model.ILoaiDuToan.Value != (int)EstimateTypeNum.ADJUSTED)
                {
                    //selecteds.AddRange(Model.IIdDotNhan.Split(',').Select(c => Guid.Parse(c)));
                    //Model.PropertyChanged += DialogModel_PropertyChanged;
                }
                else
                {
                    DataDot.ForAll(c => c.IsEnabled = false);
                }
                LoadDataDotNhanCustom();
            }
        }

        private void LoadChungTuIndex()
        {
            var predicate = CreatePredicateChungTuPhanBo();
            int soChungTuIndex = _dtChungTuService.FindNextSoChungTuIndex(predicate);
            Model.SSoChungTu = "PB-" + soChungTuIndex.ToString("D3");
            Model.ISoChungTuIndex = soChungTuIndex;
        }

        private void DialogModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Model.DNgayChungTu) && Model.DNgayQuyetDinh == null && _cbxEstimateTypeSelected != null)
            {
                //LoadDataDotNhan();
                LoadDataDotNhanCustom();
            }
            if (args.PropertyName == nameof(Model.DNgayQuyetDinh) && _cbxEstimateTypeSelected != null)
            {
                //LoadDataDotNhan();
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
            Expression<Func<BhDtPhanBoChungTu, bool>> predicate;
            IEnumerable<BhDtPhanBoChungTu> listChungTu = new List<BhDtPhanBoChungTu>();
            DataDot = new ObservableCollection<BhDttBHXHModel>();
            if (CbxEstimateTypeSelected != null)
            {
                var estimateType = (EstimateTypeNum)(int.Parse(_cbxEstimateTypeSelected.ValueItem));
                if (EstimateTypeNum.ADJUSTED.Equals(estimateType))
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value.Date : Model.DNgayChungTu.Value.Date;
                    DateTime date = DateTime.Parse(dateTime.ToString());
                    LblPhanBo = "ĐỢT ĐÃ PHÂN BỔ";
                    predicate = CreatePredicateChungTuPhanBo();
                    predicate = predicate.And(x => !string.IsNullOrEmpty(x.SSoQuyetDinh));
                    predicate = predicate.And(x => !x.ILoaiDuToan.Equals(SoChungTuType.ReceiveEstimate));
                    predicate = predicate.And(x => (x.DNgayQuyetDinh.HasValue && x.DNgayQuyetDinh.Value.Date <= dateTime) || (!x.DNgayQuyetDinh.HasValue && x.DNgayChungTu.HasValue && x.DNgayChungTu.Value.Date <= dateTime));
                    listChungTu = _dtChungTuService.FindByCondition(predicate);
                    listChungTu = listChungTu.Where(x =>
                    {
                        if (x.ILoaiDuToan != (int)EstimateTypeNum.ADJUSTED)
                        {
                            return x.IsHasDttData;
                        }
                        else { return true; }
                    });

                    if (Guid.Empty.Equals(Model.Id))
                    {
                        List<BhDttNhanPhanBoMap> dtNhanPhanBoMaps = _dtChungTuMapService.FindByListIdNhanDuToan(listChungTu.Select(x => x.Id.ToString()).ToList()).ToList();
                        listChungTu = listChungTu.Where(x => !dtNhanPhanBoMaps.Select(x => x.IIdCtduToanNhan).Contains(x.Id)).ToList();
                    }
                    else
                        listChungTu = listChungTu.Where(x => x.Id != Model.Id).ToList();
                    DataDot = _mapper.Map<ObservableCollection<BhDttBHXHModel>>(listChungTu);
                }
                else
                {
                    DateTime dateTime = Model.DNgayQuyetDinh != null ? Model.DNgayQuyetDinh.Value : DateTime.Now;
                    LblPhanBo = "ĐỢT NHẬN PHÂN BỔ";
                    EstimationVoucherCriteria condition = new EstimationVoucherCriteria
                    {
                        YearOfWork = _sessionInfo.YearOfWork,
                        YearOfBudget = _sessionInfo.YearOfBudget,
                        BudgetSource = _sessionInfo.Budget,
                        EstimationType = (int.Parse(_cbxEstimateTypeSelected.ValueItem)),
                        Date = dateTime,
                        //SoChungTuIndex = (int)Model.ISoChungTuIndex
                    };

                    List<BhDttChungTuDotNhanQuery> listDotNhanQuery = _dtChungTuService.FindAllChungTuDotNhan(condition).ToList();

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
                        List<BhDttChungTuDotNhanQuery> listDotNhanQueryLuyKe = new List<BhDttChungTuDotNhanQuery>();
                        foreach (var idDotNhan in listIdDotNhan)
                        {
                            string idLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id));
                            if (!listDotNhanQueryLuyKe.Any(x => x.IdLuyKe == idLuyKe))
                            {
                                BhDttChungTuDotNhanQuery dotNhanQuery = new BhDttChungTuDotNhanQuery
                                {
                                    IdLuyKe = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.Id)),
                                    SSoChungTu = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoChungTu)),
                                    DNgayChungTu = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayChungTu),
                                    SSoQuyetDinh = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SSoQuyetDinh)),
                                    DNgayQuyetDinh = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Max(x => x.DNgayQuyetDinh),
                                    FSoNhanPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoPhanBo),
                                    FSoChuaPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoChuaPhanBo),
                                    FSoPhanBo = listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoPhanBo) - listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Sum(x => x.FSoChuaPhanBo),
                                    SDslns = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDslns)),
                                    SDsidMaDonVi = string.Join(",", listDotNhanQuery.Where(x => idDotNhan.Contains(x.Id.ToString())).Select(x => x.SDsidMaDonVi)),
                                };
                                listDotNhanQueryLuyKe.Add(dotNhanQuery);
                            }
                        }
                        DataDot = _mapper.Map<ObservableCollection<BhDttBHXHModel>>(listDotNhanQueryLuyKe);
                    }
                    else
                    {
                        DataDot = _mapper.Map<ObservableCollection<BhDttBHXHModel>>(listDotNhanQuery);
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
                            IsCheckDot = true;
                            IsCheckDot = false;

                            OnPropertyChanged(nameof(SelectAllDot));
                            OnPropertyChanged(nameof(SelectedCountDot));
                        }
                    };
                }
            }    
            SetCheckedChungTuNhan();
        }

        private bool ListDataDotPhanBoFilter(object obj)
        {
            var item = (BhDttBHXHModel)obj;
            bool res = true;
            if (item != null)
            {
                if (DataLNS.Any(x => x.IsSelected))
                {
                    res = res && item.SDslns.Split(",").Any(x => DataLNS.Where(x => x.IsSelected).Select(y => y.SLNS).Contains(x));
                }
                else
                {
                    res = false;
                }

                if (!string.IsNullOrWhiteSpace(_searchDotPhanBo))
                {
                    res = res && item.SSoChungTu.ToLower().Contains(_searchDotPhanBo, StringComparison.OrdinalIgnoreCase);
                }
                item.IsFilter = res;
                item.IsEnabled = res;
            }
            return res;
        }

        private void LoadEstimateType()
        {
            var cbxVoucher = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.YEAR], ValueItem = ((int)EstimateTypeNum.YEAR).ToString()},
                new ComboboxItem {DisplayItem = EstimateType.EstimateTypeName[EstimateTypeNum.ADDITIONAL], ValueItem = ((int)EstimateTypeNum.ADDITIONAL).ToString()}
            };

            CbxEstimateType = new ObservableCollection<ComboboxItem>(cbxVoucher);
            if (Model != null && Model.Id != Guid.Empty && Model.ILoaiDuToan.HasValue)
            {
                CbxEstimateTypeSelected = CbxEstimateType.Single(item => item.ValueItem.Equals(Model.ILoaiDuToan.ToString()));
            }
            else
            {
                CbxEstimateTypeSelected = CbxEstimateType.First();
            }
        }

        private void LoadLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var listNsMucLucNganSach = new List<BhDmMucLucNganSach>();
            List<BhDmMucLucNganSachModel> listBhDmMucLucNganSachModel = new List<BhDmMucLucNganSachModel>();
            if (CbxEstimateTypeSelected != null)
            {
                var predicate = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate = predicate.And(x => x.INamLamViec == yearOfWork).And(x => x.SLNS.StartsWith(BhxhMLNS.KHT_BHXH_BHYT_BHTN));
                listNsMucLucNganSach = _bhMucLucNganSachService.FindByCondition(predicate).OrderBy(x => x.SXauNoiMa).ToList();

                listBhDmMucLucNganSachModel = _mapper.Map<List<BhDmMucLucNganSachModel>>(listNsMucLucNganSach);
            }
            DataLNS = new ObservableCollection<BhDmMucLucNganSachModel>(listBhDmMucLucNganSachModel);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<string> sLnsHasData = Model.SDslns.Split(",").Distinct().ToList();
                DataLNS.Where(x => sLnsHasData.Contains(x.SLNS)).Select(x => { x.IsSelected = true; return x; }).ToList();
            }

            LoadLNSPropertyChanged();
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
                        if (args.PropertyName == nameof(BhDmMucLucNganSachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.IIDMLNSCha == model.IIDMLNS)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                            if (!IsCheckDot && _dataDotPhanBoView != null)
                            {
                                _dataDotPhanBoView.Refresh();
                            }
                        }
                    };
                }
                SelectAllLNS = true;
            }

        }

        private void LoadAgencies()
        {
            var listUnit = new List<DonVi>();
            if (_cbxEstimateTypeSelected != null)
            {
                var estimateType = (EstimateTypeNum)(int.Parse(_cbxEstimateTypeSelected.ValueItem));
                if (!EstimateTypeNum.ADJUSTED.Equals(estimateType))
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => LoaiDonVi.NOI_BO.Equals(x.Loai));
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
                    
                    // remove 999 hard code
                    predicate = predicate.And(x => !x.IIDMaDonVi.Equals("999"));
                    listUnit = _nSDonViService.FindByCondition(predicate).OrderBy(n => n.IIDMaDonVi).ToList();
                }
            }

            DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);
            if (Model != null && Model.Id != Guid.Empty)
            {
                List<string> lstDonViHasData = Model.SDsidMaDonVi.Split(",").ToList();
                DataUnit.Where(x => lstDonViHasData.Contains(x.ValueItem)).Select(x => { x.IsChecked = true; return x; }).ToList();
            }

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
            return obj is BhDmMucLucNganSachModel item && item.LNSDisplay.ToLower().Contains(_searchLNS!.Trim().ToLower());
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
                List<BhDtPhanBoChungTuChiTiet> listChungTuChiTiet = _dtChungTuChiTietService.FindByIdChungTu(Model.Id.ToString()).ToList();
                var listLNSHasDataUnchecked = _dataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.SLNS).ToList();
                var listUnitHasDataUnchecked = _dataUnit.Where(n => !n.IsHitTestVisible && !n.IsChecked).Select(n => n.ValueItem).ToList();
                listChungTuChiTiet = listChungTuChiTiet.Where(x => listUnitHasDataUnchecked.Contains(x.IIdMaDonVi) || listLNSHasDataUnchecked.Contains(x.SLns)).ToList();
                _dtChungTuChiTietService.RemoveRange(listChungTuChiTiet);
            }

            var listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.Id.ToString()).ToList();

            if (Model == null) Model = new BhDtPhanBoChungTuModel();
            Model.SDslns = SocialInsuranceCatalogSelectedToStringConvert.GetValueSelected(DataLNS);
            Model.INamLamViec = _sessionInfo.YearOfWork;
            if (Model.ILoaiDuToan != (int)EstimateTypeNum.ADJUSTED)
            {
                Model.SDsidMaDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit);
            }
            Model.IIdDotNhan = string.Join(",", DataDot.Where(n => n.IsChecked == true).Select(n => n.Id).ToList());
            Model.ILoaiDuToan = int.Parse(_cbxEstimateTypeSelected.ValueItem);
            BhDtPhanBoChungTu entity;
            if (Model.Id == Guid.Empty)
            {
                // Add
                entity = new BhDtPhanBoChungTu();
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

            if (EstimateTypeNum.ADJUSTED.Equals((EstimateTypeNum)(int.Parse(_cbxEstimateTypeSelected.ValueItem))))
            {
                var listIdNhan = _dtChungTuMapService.FindByListIdPhanBo(listIdDotNhan).Select(n => n.IIdCtduToanNhan);
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
            var dtChungTuMaps = DataDot.Where(x => x.IsChecked).Select(idDotNhan => new BhDttNhanPhanBoMap
            {
                IIdCtduToanPhanBo = entity.Id,
                IIdCtduToanNhan = idDotNhan.Id,
                SNguoiTao = _sessionService.Current.Principal,
                SNgaySua = _sessionService.Current.Principal,
                DNgayTao = DateTime.Now,
                DNgaySua = DateTime.Now
            }).ToList();
            _dtChungTuMapService.Save(dtChungTuMaps);

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            // Show detail page when saved
            SavedAction?.Invoke(_mapper.Map<BhDtPhanBoChungTuModel>(entity));

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

            if (_cbxEstimateTypeSelected == null)
            {
                messages.Add(Resources.AlertLoaiDuToanEmpty);
            }

            if (DataDot.All(x => !x.IsChecked))
            {
                messages.Add("Hãy chọn đợt phân bổ");
            }

            if (_cbxEstimateTypeSelected == null)
            {
                messages.Add("Hãy chọn loại dự toán");
            }

            if (!messages.Any())
            {
                messages.AddRange(ValidateSoQuyetDinh());
                List<string> listIdDotNhan = new List<string>();

                var estimateType = (EstimateTypeNum)(int.Parse(_cbxEstimateTypeSelected.ValueItem));
                if (!EstimateTypeNum.ADJUSTED.Equals(estimateType))
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
                    if (Model.ILoaiDuToan.HasValue && Model.ILoaiDuToan.Value != (int)EstimateTypeNum.ADJUSTED)
                    {
                        listIdDotNhan = DataDot.Where(data => data.IsChecked).Select(data => data.ILoaiDuToan.ToString()).ToList();
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
            var predicateChitiet = PredicateBuilder.True<BhDtPhanBoChungTuChiTiet>();
            predicateChitiet = predicateChitiet.And(x => x.IIdDttBHXH == Model.Id);
            var lstChungTuChiTiet = _dtChungTuChiTietService.FindByCondition(predicateChitiet).ToList();

            var lstUnitHasData = lstChungTuChiTiet.Where(x => x.FTongCong > 0).Select(x => x.IIdMaDonVi).ToList();
            var listUnitHasDataUnchecked = _dataUnit.Where(x => !x.IsChecked && lstUnitHasData.Contains(x.ValueItem)).ToList();
            string unitText = string.Join(StringUtils.COMMA_SPLIT, listUnitHasDataUnchecked.Select(x => x.NameItem));

            if (!string.IsNullOrEmpty(unitText))
            {
                return string.Format(Resources.DivisionEstimateHasDataUnit, unitText);
            }
            else return "";
        }

        private string GetMessageValidateCheckBoxLNS()
        {
            var listLNSHasDataUnchecked = _dataLNS.Where(n => !n.IsHitTestVisible && !n.IsSelected).Select(n => n.SLNS).ToList();
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
                var predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
                predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
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

        private Expression<Func<BhDtPhanBoChungTu, bool>> CreatePredicateChungTuPhanBo()
        {
            var predicate = PredicateBuilder.True<BhDtPhanBoChungTu>();
            predicate = predicate.And(x => x.INamLamViec == _sessionInfo.YearOfWork);
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
