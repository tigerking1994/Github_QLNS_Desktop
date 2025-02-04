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
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.RevenueExpenditure.RevenueExpenditureDivision;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.RevenueExpenditureType;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.RevenueExpenditure.RevenueExpenditureDivision
{
    public class RevenueExpenditureDivisionDialogViewModel : DialogViewModelBase<TnDtChungTuModel>
    {
        private readonly IMapper _mapper;
        private readonly INsMucLucNganSachService _nsMucLucNganSachService;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly ITnDtChungTuService _tnDtChungTuService;
        private readonly ITnDtChungTuChiTietService _tnDtChungTuChiTietService;
        private readonly ILog _logger;
        private ICollectionView _dataLNSView;
        private ICollectionView _dataUnitView;

        public override Type ContentType => typeof(RevenueExpenditureDivisionDialog);
        public override string Name => Model.Id == Guid.Empty ? "THÊM CHỨNG TỪ" : "CẬP NHẬT CHỨNG TỪ";
        public override string Description => Model.Id == Guid.Empty ? "Tạo mới chứng từ phân bổ dự toán" : "Cập nhật chứng từ phân bổ dự toán";

        //Sửa lỗi tự động cập nhập vào màn danh sách khi chưa nhấn lưu
        public DateTime? NgayChungTu { get; set; }

        private ObservableCollection<NsMuclucNgansachModel> _dataLNS;
        public ObservableCollection<NsMuclucNgansachModel> DataLNS
        {
            get => _dataLNS;
            set => SetProperty(ref _dataLNS, value);
        }

        public string SelectedCountLNS
        {
            get
            {
                int totalItems = DataLNS != null ? DataLNS.Count() : 0;
                int totalSelected = DataLNS != null ? DataLNS.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN LNS ({0}/{1})", totalSelected, totalItems);
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => (DataLNS == null || !DataLNS.Any()) ? false : DataLNS.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllLNS, value);
                if (DataLNS != null)
                {
                    DataLNS.Select(c => { c.IsSelected = _selectAllLNS; return c; }).ToList();
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

        private ObservableCollection<CheckBoxItem> _dataUnit;
        public ObservableCollection<CheckBoxItem> DataUnit
        {
            get => _dataUnit;
            set => SetProperty(ref _dataUnit, value);
        }

        public string SelectedCountUnit
        {
            get
            {
                int totalCount = DataUnit != null ? DataUnit.Count : 0;
                int totalSelected = DataUnit != null ? DataUnit.Count(item => item.IsChecked) : 0;
                return string.Format("CHỌN ĐƠN VỊ ({0}/{1})", totalSelected, totalCount);
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

        private ObservableCollection<TnDtChungTuModel> _dataDotNhan;
        public ObservableCollection<TnDtChungTuModel> DataDotNhan
        {
            get => _dataDotNhan;
            set => SetProperty(ref _dataDotNhan, value);
        }

        private TnDtChungTuModel _dotNhanSelected;
        public TnDtChungTuModel DotNhanSelected
        {
            get => _dotNhanSelected;
            set
            {
                SetProperty(ref _dotNhanSelected, value);
            }
        }

        private List<string> ListAgencyHasDataUnchecked { get; set; }


        public RevenueExpenditureDivisionDialogViewModel(IMapper mapper,
            INsMucLucNganSachService nsMucLucNganSachService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ITnDtChungTuService tnDtChungTuService,
            ITnDtChungTuChiTietService tnDtChungTuChiTietService,
            ILog logger)
        {
            _mapper = mapper;
            _nsMucLucNganSachService = nsMucLucNganSachService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _logger = logger;
            _tnDtChungTuService = tnDtChungTuService;
            _tnDtChungTuChiTietService = tnDtChungTuChiTietService;
        }

        public override void Init()
        {
            LoadDataLNS();
            LoadDataUnit();
            LoadData();
            LoadDotNhan();
        }

        private void LoadDataLNS()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            string idDonVi = _sessionService.Current.IdDonVi;
            List<NsMucLucNganSach> listNsMucLucNganSach = new List<NsMucLucNganSach>();

            if (_sessionService.Current.Budget.Equals(NSQP))
            {
                listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_QP).ToList();
            }
            else if (_sessionService.Current.Budget.Equals(NSNN))
            {
                listNsMucLucNganSach = _nsMucLucNganSachService.FindByMLNS(yearOfWork, MLNS_NN).ToList();
            }

            DataLNS = new ObservableCollection<NsMuclucNgansachModel>();
            DataLNS = _mapper.Map<ObservableCollection<NsMuclucNgansachModel>>(listNsMucLucNganSach);

            _dataLNSView = CollectionViewSource.GetDefaultView(DataLNS);
            _dataLNSView.Filter = ListLNSFilter;

            if (_dataLNS != null && _dataLNS.Count > 0)
            {
                foreach (var model in _dataLNS)
                {
                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(NsMuclucNgansachModel.IsSelected))
                        {
                            foreach (var item in _dataLNS)
                            {
                                if (item.MlnsIdParent == model.MlnsId)
                                {
                                    item.IsSelected = model.IsSelected;
                                }
                            }
                            OnPropertyChanged(nameof(SelectAllLNS));
                            OnPropertyChanged(nameof(SelectedCountLNS));
                        }
                    };
                }
            }
        }

        private bool ListLNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
            {
                return true;
            }
            return obj is NsMuclucNgansachModel item && item.Lns.ToLower().Contains(_searchLNS!.ToLower());
        }

        private void LoadDataUnit()
        {
            int namLamViec = _sessionService.Current.YearOfWork;
            IEnumerable<DonVi> listUnit = _nsDonViService.FindByCondition(RevenueAndExpenditureType.UnitType, NSEntityStatus.ACTIVED, namLamViec);

            var lstUnitCreated = _tnDtChungTuService.FindByType(RevenueAndExpenditureType.DivisionEstimation).Select(x => x.IdDonVi).ToArray();

            if (Model.Id == Guid.Empty)
            {
                listUnit = listUnit.Where(x => !lstUnitCreated.Contains(x.IIDMaDonVi)).ToList();
            }
            else
            {
                lstUnitCreated = lstUnitCreated.Where(x => !x.Contains(Model.IdDonVi)).ToArray();
                listUnit = listUnit.Where(x => !lstUnitCreated.Contains(x.IIDMaDonVi)).ToList();
            }

            DataUnit = _mapper.Map<ObservableCollection<CheckBoxItem>>(listUnit);

            _dataUnitView = CollectionViewSource.GetDefaultView(DataUnit);
            _dataUnitView.Filter = ListUnitFilter;

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
            return obj is CheckBoxItem item && item.ValueItem.ToLower().Contains(_searchUnit!.ToLower());
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (Model != null && Model.Id != Guid.Empty)
                {
                    var itemDetails = _tnDtChungTuChiTietService.FindByChungtuId(Model.Id);
                    BudgetCatalogSelectedToStringConvert.SetCheckboxSelected(_dataLNS, Model.Lns);
                    CheckboxSelectedToStringConvert.SetCheckboxSelected(_dataUnit, Model.IdDonVi);
                    if (!itemDetails.IsEmpty() && _dataUnit.Any(x => x.IsChecked))
                    {
                        var itemUnits = itemDetails.Where(x => !string.IsNullOrEmpty(x.IdDonVi)).Select(x => x.IdDonVi.Trim()).Distinct().ToList();
                        var itemsm = _dataUnit.Where(x => x.IsChecked && itemUnits.Contains(x.ValueItem));
                        _dataUnit.Where(x => x.IsChecked && itemUnits.Contains(x.ValueItem)).ForAll(s => { s.IsHitTestVisible = false; });
                    }
                    NgayChungTu = Model.NgayChungTu;
                }
                else
                {
                    _selectAllLNS = false;
                    _selectAllUnit = false;

                    // Add
                    var predicate = this.CreatePredicateChungTuIndex();
                    int soChungTuIndex = _tnDtChungTuService.FindNextSoChungTuIndex(predicate);
                    Model = new TnDtChungTuModel()
                    {
                        SoChungTu = "CT-" + soChungTuIndex.ToString("D3"),
                        SoChungTuIndex = soChungTuIndex,
                        NgayChungTu = DateTime.Now,
                        NgayQuyetDinh = DateTime.Now,
                        SoQuyetDinh = string.Empty,
                        MoTaChiTiet = string.Empty
                    };
                    NgayChungTu = DateTime.Now;
                    OnPropertyChanged(nameof(SelectAllLNS));
                    OnPropertyChanged(nameof(SelectedCountLNS));
                    OnPropertyChanged(nameof(SelectAllUnit));
                    OnPropertyChanged(nameof(SelectedCountUnit));
                }

            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private Expression<Func<TnDtChungTu, bool>> CreatePredicateChungTuIndex()
        {
            var inner = PredicateBuilder.False<TnDtChungTu>();
            inner = inner.Or(x => x.IdDonViTao == _sessionService.Current.IdDonVi);
            inner = inner.Or(x => x.IdDonViTao != _sessionService.Current.IdDonVi && x.IGuiNhan == IguiNhanStatus.GUINHAN);

            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.ILoai == RevenueAndExpenditureType.DivisionEstimation);
            predicate = predicate.And(x => x.IdDonViTao == _sessionService.Current.IdDonVi);
            predicate = predicate.And(inner);
            return predicate;
        }

        private Expression<Func<TnDtChungTu, bool>> CreatePredicate(bool isDotNhan = true)
        {
            var predicate = PredicateBuilder.True<TnDtChungTu>();
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.NamNganSach == _sessionService.Current.YearOfBudget);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            predicate = predicate.And(x => x.ILoai == (isDotNhan ? RevenueAndExpenditureType.ApprovedEstimation : RevenueAndExpenditureType.DivisionEstimation));
            return predicate;
        }

        private void LoadDotNhan()
        {
            DataDotNhan = new ObservableCollection<TnDtChungTuModel>();
            var predicateDotNhan = CreatePredicate();
            var predicatePhanBo = CreatePredicate(false);
            var itemsDotNhan = _tnDtChungTuService.FindByCondition(predicateDotNhan);
            var itemsPhanBo = _tnDtChungTuService.FindByCondition(predicatePhanBo);
            if (itemsDotNhan.IsEmpty() && itemsPhanBo.IsEmpty()) return;
            var itemsDaNhan = itemsPhanBo.Where(x => !string.IsNullOrEmpty(x.IdDotNhan)).Select(s => s.IdDotNhan);
            itemsDotNhan = itemsDotNhan.Where(x => (!itemsDaNhan.Contains(x.Id.ToString()) && x.TuChiSum != NSConstants.ZERO) || x.Id.ToString() == Model.IdDotNhan).OrderByDescending(o => o.NgayQuyetDinh).ToList();
            DataDotNhan = new ObservableCollection<TnDtChungTuModel>(_mapper.Map<List<TnDtChungTuModel>>(itemsDotNhan));
            DotNhanSelected = DataDotNhan.FirstOrDefault(x => x.Id.ToString() == Model.IdDotNhan);
            if (Model.Id.IsNullOrEmpty()) DotNhanSelected = DataDotNhan.FirstOrDefault();
            OnPropertyChanged(nameof(DataDotNhan));
            OnPropertyChanged(nameof(DotNhanSelected));
        }


        public override void OnSave()
        {
            try
            {
                if (Model == null) Model = new TnDtChungTuModel();
                Model.Lns = BudgetCatalogSelectedToStringConvert.GetValueSelected(DataLNS);
                Model.NamLamViec = _sessionService.Current.YearOfWork;
                Model.NamNganSach = _sessionService.Current.YearOfBudget;
                Model.NguonNganSach = _sessionService.Current.Budget;
                Model.IdDonVi = CheckboxSelectedToStringConvert.GetValueSelected(_dataUnit);
                Model.IdDonViTao = _sessionService.Current.IdDonVi;
                Model.ILoai = SoChungTuType.EstimateDivision;
                Model.NgayChungTu = NgayChungTu;

                string message = GetMessageValidate();
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool bDeleteDetail = false;
                string messageCheckBox = GetMessageValidateCheckBox();
                if (!string.IsNullOrEmpty(messageCheckBox))
                {
                    MessageBoxResult messageValidate = MessageBoxHelper.Confirm(messageCheckBox);
                    if (messageValidate.Equals(MessageBoxResult.Yes))
                    {
                        bDeleteDetail = true;
                    }
                    else
                    {
                        return;
                    }
                }
                if (bDeleteDetail && !Model.Id.IsNullOrEmpty())
                {
                    var itemDetails = _tnDtChungTuChiTietService.FindByChungtuId(Model.Id);
                    var itemRemoves = itemDetails.Where(x => ListAgencyHasDataUnchecked.Contains(x.IdDonVi)).ToList();
                    _tnDtChungTuChiTietService.RemoveRange(itemRemoves);
                    var itemRemain = itemDetails.Except(itemRemoves);
                    Model.TuChiSum = itemRemain.Sum(x => x.TuChi);
                }

                TnDtChungTu entity;
                if (Model.Id == Guid.Empty)
                {
                    // Add
                    entity = new TnDtChungTu();
                    _mapper.Map(Model, entity);
                    entity.IdDotNhan = DotNhanSelected?.Id.ToString();
                    entity.DateCreated = DateTime.Now;
                    entity.UserCreator = _sessionService.Current.Principal;
                    _tnDtChungTuService.Add(entity);
                }
                else
                {
                    // Update
                    entity = _tnDtChungTuService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.IdDotNhan = DotNhanSelected?.Id.ToString();
                    entity.DateModified = DateTime.Now;
                    entity.UserModifier = _sessionService.Current.Principal;
                    _tnDtChungTuService.Update(entity);
                }

                DialogHost.CloseDialogCommand.Execute(null, null);
                // Show detail page when saved
                SavedAction?.Invoke(_mapper.Map<TnDtChungTuModel>(entity));
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private string GetMessageValidate()
        {
            IList<string> messages = new List<string>();

            if (!Model.NgayChungTu.HasValue)
            {
                messages.Add("Hãy nhập ngày chứng từ.");
            }

            if (string.IsNullOrEmpty(Model.SoQuyetDinh))
            {
                messages.Add("Hãy nhập số quyết định.");
            }

            if (!Model.NgayQuyetDinh.HasValue)
            {
                messages.Add("Hãy nhập ngày quyết định.");
            }

            if (DataLNS.All(x => !x.IsSelected))
            {
                messages.Add("Hãy chọn LNS");
            }

            if (DotNhanSelected is null)
            {
                messages.Add("Hãy chọn đợt nhận dự toán");
            }
            if (string.IsNullOrEmpty(Model.IdDonVi))
            {
                messages.Add(Resources.AlertAgencyEmpty);
            }

            return string.Join(Environment.NewLine, messages);
        }

        private string GetMessageValidateCheckBox()
        {
            List<string> messages = new List<string>();

            ListAgencyHasDataUnchecked = DataUnit.Where(n => !n.IsHitTestVisible && !n.IsChecked).Select(n => n.ValueItem).ToList();
            string sDonViText = string.Join(StringUtils.COMMA_SPLIT, DataUnit.Where(n => !n.IsHitTestVisible && !n.IsChecked).Select(n => n.DisplayItem));

            if (!string.IsNullOrEmpty(sDonViText))
            {
                messages.Add(string.Format(Resources.DivisionEstimateHasDataUnit, sDonViText));
            }

            return string.Join(Environment.NewLine, messages);
        }
    }
}
