using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Data;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Helper;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber
{
    public class SettlementNumberDialogViewModel : DialogViewModelBase<TlQsChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlQsChungTuService _tlQsChungTuService;
        private readonly ITlDmCanBoService _carderService;
        private readonly INsQsMucLucService _qsMucLucService;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmCapBacService _dmCapBacService;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _listAgency;
        private List<TlQsChungTuChiTiet> _listChungTuTongHop = new List<TlQsChungTuChiTiet>();
        private List<NsQsMucLuc> _listMucLucQs = new List<NsQsMucLuc>();
        private Dictionary<string, TlDmCapBac> _capBacs = new Dictionary<string, TlDmCapBac>();

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_SETTLEMENT_NUMBER_DIALOG;
        public override Type ContentType => typeof(View.Salary.Settlement.SalarySettlementNumber.SalarySettlementNumberDialog);
        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        private FormViewState _viewState;
        public FormViewState ViewState
        {
            get => _viewState;
            set
            {
                SetProperty(ref _viewState, value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private List<ComboboxItem> _itemsMonth;
        public List<ComboboxItem> ItemsMonth
        {
            get => _itemsMonth;
            set => SetProperty(ref _itemsMonth, value);
        }

        private ComboboxItem _selectedMonth;
        public ComboboxItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                SetProperty(ref _selectedMonth, value);
                if (_selectedMonth != null)
                {
                    LoadDonVi();
                }
            }
        }

        private List<ComboboxItem> _itemsYear;
        public List<ComboboxItem> ItemsYear
        {
            get => _itemsYear;
            set => SetProperty(ref _itemsYear, value);
        }

        private ComboboxItem _selectedYear;
        public ComboboxItem SelectedYear
        {
            get => _selectedYear;
            set
            {
                SetProperty(ref _selectedYear, value);
                if (_selectedYear != null)
                {
                    LoadDonVi();
                }
            }
        }

        private ObservableCollection<TlDmDonViModel> _itemsDonVi;
        public ObservableCollection<TlDmDonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private TlDmDonViModel _selectedDonVi;
        public TlDmDonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => !ItemsDonVi.IsEmpty() && ItemsDonVi.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in ItemsDonVi) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = ItemsDonVi.IsEmpty() ? 0 : ItemsDonVi.Count();
                var totalSelectedCount = ItemsDonVi.IsEmpty() ? 0 : ItemsDonVi.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
            }
        }

        private string _moTaChungTu;
        public string MoTaChungTu
        {
            get => _moTaChungTu;
            set => SetProperty(ref _moTaChungTu, value);
        }

        private ObservableCollection<DonViModel> _itemsNsDonViModel;
        public ObservableCollection<DonViModel> ItemsNsDonVi
        {
            get => _itemsNsDonViModel;
            set => SetProperty(ref _itemsNsDonViModel, value);
        }

        private DonViModel _selectedNsDonVi;
        public DonViModel SelectedNsDonVi
        {
            get => _selectedNsDonVi;
            set
            {
                SetProperty(ref _selectedNsDonVi, value);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    _listAgency.Refresh();
                }
            }
        }

        public RelayCommand SaveSyntheticCommand { get; }

        public SettlementNumberDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlQsChungTuService tlQsChungTuService,
            ITlDmCanBoService carderService,
            INsQsMucLucService qsMucLucService,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            INsDonViService nsDonViService,
            ITlDmCapBacService dmCapBacService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlQsChungTuService = tlQsChungTuService;
            _carderService = carderService;
            _qsMucLucService = qsMucLucService;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _nsDonViService = nsDonViService;
            _dmCapBacService = dmCapBacService;

            SaveSyntheticCommand = new RelayCommand(obj => OnSaveSynthetic());
        }

        public override void Init()
        {
            try
            {
                LoadMonths();
                LoadYears();
                LoadDonViNs();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadMonths()
        {
            _itemsMonth = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _itemsMonth.Add(month);
            }
            OnPropertyChanged(nameof(ItemsMonth));
        }

        private void LoadYears()
        {
            _itemsYear = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _itemsYear.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
        }

        private void LoadDonVi()
        {
            try
            {
                int nam = int.Parse(SelectedYear.ValueItem);
                int thang = int.Parse(SelectedMonth.ValueItem);
                var data = _tlDmDonViService.FindAllDonViQuanSo(thang, nam);
                _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
                foreach (var item in _itemsDonVi)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(TlDmDonViModel.IsSelected))
                        {
                            foreach (var donVi in ItemsDonVi)
                            {
                                if (donVi.ParentId == item.MaDonVi)
                                {
                                    donVi.IsSelected = item.IsSelected;
                                }
                            }
                        }
                        OnPropertyChanged(nameof(SelectedAllDonVi));
                        OnPropertyChanged(nameof(LabelSelectedDonVi));
                    };
                }

                SelectedAllDonVi = true;
                _listAgency = CollectionViewSource.GetDefaultView(ItemsDonVi);
                _listAgency.Filter = ListAgencyFilter;
                OnPropertyChanged(nameof(SelectedAllDonVi));
                OnPropertyChanged(nameof(LabelSelectedDonVi));
                OnPropertyChanged(nameof(ItemsDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDonViNs()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => x.Loai == "0" || x.Loai == "1");
            var data = _nsDonViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();

            ItemsNsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            SelectedNsDonVi = null;
        }

        public override void LoadData(params object[] args)
        {
            _moTaChungTu = "Chứng từ chi tiết";
            _selectedYear = _itemsYear.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
            if (string.IsNullOrEmpty(Model.Thang.ToString()))
            {
                var thang = _sessionService.Current.Month;
                _selectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem.Equals(thang.ToString()));
            }
            else
            {
                _selectedMonth = _itemsMonth.FirstOrDefault(x => x.ValueItem.Equals(Model.Thang.ToString()));
            }

            LoadDonVi();
            OnPropertyChanged(nameof(MoTaChungTu));
            OnPropertyChanged(nameof(SelectedYear));
            OnPropertyChanged(nameof(SelectedMonth));
        }

        private bool ListAgencyFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                return true;
            }
            return obj is TlDmDonViModel item && item.TenDonVi.ToLower().Contains(_searchText!.ToLower());
        }

        public override void OnSave()
        {
            string message = GetValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    _capBacs = _dmCapBacService.FindAll().ToDictionary(x => x.MaCb, x => x);
                    TlQsChungTu tlQsChungTu;
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
                    foreach (var donVi in lstDonVi)
                    {
                        tlQsChungTu = new TlQsChungTu();
                        Model.MoTa = MoTaChungTu;
                        DateTime myDate = DateTime.Today;
                        DateTime createDay = new DateTime(myDate.Year, DateTime.Now.Month, DateTime.Now.Day);
                        Model.DateCreated = createDay;
                        Model.Thang = int.Parse(SelectedMonth.ValueItem);
                        Model.MaDonVi = donVi.MaDonVi;
                        Model.TenDonVi = donVi.TenDonVi;
                        Model.IsLock = false;
                        Model.SoChungTu = SinhMaChungTu();
                        Model.Nam = int.Parse(SelectedYear.ValueItem);

                        _mapper.Map(Model, tlQsChungTu);
                        _tlQsChungTuService.Add(tlQsChungTu);
                        SaveChungTuChiTiet(tlQsChungTu, false);
                    }
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        SavedAction?.Invoke(null);
                        // Invoke message
                        MessageBoxHelper.Info(Resources.MsgSaveDone);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
        }

        public void CapNhatChungTuTongHopChiTiet(TlQsChungTu tlQsChungTuModel)
        {
            List<TlQsChungTuChiTiet> chungTuChiTiet = new List<TlQsChungTuChiTiet>();
            List<TlQsChungTuChiTietModel> chungTuChiTietTongHop = new List<TlQsChungTuChiTietModel>();
            
            var predicate = PredicateBuilder.True<TlQsChungTuChiTiet>();
            predicate = predicate.And(x => tlQsChungTuModel.STongHop.Contains(x.IdChungTu));
            var TlQsChungTu = _tlQsChungTuChiTietService.FindAll(predicate).ToList();
            chungTuChiTiet.AddRange(TlQsChungTu);

            var predicateMucLuc = PredicateBuilder.True<NsQsMucLuc>();
            predicateMucLuc = predicateMucLuc.And(x => !x.SHienThi.Equals("2"));
            predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == tlQsChungTuModel.Nam);
            var listQsMucLuc = _qsMucLucService.FindAll(predicateMucLuc);

            foreach (var mucLuc in listQsMucLuc)
            {
                TlQsChungTuChiTietModel model = new TlQsChungTuChiTietModel();
                model.IdChungTu = tlQsChungTuModel.Id.ToString();
                model.MlnsIdParent = mucLuc.IIdMlnsCha.ToString();
                model.MlnsId = mucLuc.IIdMlns.ToString();
                model.XauNoiMa = mucLuc.SKyHieu;
                model.MoTa = mucLuc.SMoTa;
                model.Thang = tlQsChungTuModel.Thang;
                model.NamLamViec = tlQsChungTuModel.Nam;
                model.IdDonVi = tlQsChungTuModel.MaDonVi;
                model.TenDonVi = tlQsChungTuModel.TenDonVi;
                model.DateCreated = (DateTime)tlQsChungTuModel.DateCreated;
                model.UserCreator = tlQsChungTuModel.UserCreated;

                var ChungTuChiTietMucLuc = chungTuChiTiet.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa));

                model.ThieuUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuUy);
                model.TrungUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungUy);
                model.ThuongUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongUy);
                model.DaiUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiUy);
                model.ThieuTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuTa);
                model.TrungTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungTa);
                model.ThuongTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongTa);
                model.DaiTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiTa);
                model.BinhNhi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.BinhNhi);
                model.BinhNhat = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.BinhNhat);
                model.HaSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.HaSi);
                model.TrungSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungSi);
                model.ThuongSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongSi);
                model.Tuong = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Tuong);
                model.ThieuUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuUyCn);
                model.TrungUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungUyCn);
                model.ThuongUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongUyCn);
                model.DaiUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiUyCn);
                model.ThieuTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuTaCn);
                model.TrungTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungTaCn);
                model.ThuongTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongTaCn);
                model.Qncn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Qncn);
                model.Vcqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Vcqp);
                model.Ldhd = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Ldhd);
                model.Cnqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Cnqp); ;

                chungTuChiTietTongHop.Add(model);
            }
            var tlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(chungTuChiTietTongHop).ToList();
            _tlQsChungTuChiTietService.Add(tlQsChungTuChiTiet);
        }

        private bool IsVcqp(CadresModel model)
        {
            if (MA_CAP_BAC.VCQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.VCQP.Equals(capBac.Parent) && !IsLdhd(model);
        }

        private bool IsVcqpCu(CadresModel model)
        {
            if (MA_CAP_BAC.VCQP.Equals(model.MaCbCu)) return true;
            _ = _capBacs.TryGetValue(model.MaCbCu, out var capBac);
            return MA_CAP_BAC.VCQP.Equals(capBac.Parent) && !IsLdhdCu(model);
        }

        private bool IsCnqp(CadresModel model)
        {
            if (MA_CAP_BAC.CNQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.CNQP.Equals(capBac.Parent);
        }

        private bool IsCnqpCu(CadresModel model)
        {
            if (MA_CAP_BAC.CNQP.Equals(model.MaCbCu)) return true;
            _ = _capBacs.TryGetValue(model.MaCbCu, out var capBac);
            return MA_CAP_BAC.CNQP.Equals(capBac.Parent);
        }

        private bool IsCcqp(CadresModel model)
        {
            if (MA_CAP_BAC.CCQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.CCQP.Equals(capBac.Parent);
        }

        private bool IsCcqpCu(CadresModel model)
        {
            if (MA_CAP_BAC.CCQP.Equals(model.MaCbCu)) return true;
            _ = _capBacs.TryGetValue(model.MaCbCu, out var capBac);
            return MA_CAP_BAC.CCQP.Equals(capBac.Parent);
        }

        private bool IsLdhd(CadresModel model)
        {
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCb);
        }

        private bool IsLdhdCu(CadresModel model)
        {
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCbCu);
        }

        public void CapNhatChungTuKhoiTao(TlQsChungTu tlQsChungTu)
        {
            // lay lai danh sach can bo
            var predicate = PredicateBuilder.True<TlDmCanBo>();
            predicate = predicate.And(x => x.Thang == tlQsChungTu.Thang);
            predicate = predicate.And(x => x.Nam == tlQsChungTu.Nam);
            predicate = predicate.And(x => tlQsChungTu.MaDonVi.Equals(x.Parent));
            var listCanBo = _mapper.Map<List<CadresModel>>(_carderService.FindByCondition(predicate).ToList());

            var listQsMucLuc = _qsMucLucService.FindAll(x => x.SHienThi != "2" && x.INamLamViec == tlQsChungTu.Nam).ToList();
            var lisTQsMucLucModel = _mapper.Map<ObservableCollection<QsMucLucModel>>(listQsMucLuc);
            List<TlQsChungTuChiTietModel> tlQsChungTuChiTietModel = new List<TlQsChungTuChiTietModel>();
            foreach (var item in lisTQsMucLucModel)
            {
                TlQsChungTuChiTietModel model = new TlQsChungTuChiTietModel();
                model.IdChungTu = tlQsChungTu.Id.ToString();
                model.MlnsId = item.IIdMlns.ToString();
                model.MlnsIdParent = item.IIdMlnsCha.ToString();
                model.XauNoiMa = item.SKyHieu;
                model.MoTa = item.SMoTa;
                model.Thang = tlQsChungTu.Thang;
                model.NamLamViec = tlQsChungTu.Nam;
                model.IdDonVi = tlQsChungTu.MaDonVi;
                model.TenDonVi = tlQsChungTu.TenDonVi;
                model.DateCreated = DateTime.Now;
                if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_THANG_TRUOC || item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY || item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG)
                {
                    model.ThieuUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY);
                    model.TrungUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY);
                    model.ThuongUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY));
                    model.DaiUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY));
                    model.ThieuTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA));
                    model.TrungTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA));
                    model.ThuongTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA));
                    model.DaiTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA));
                    model.Tuong = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG));
                    model.Qncn = listCanBo.Count(x => x.MaCb.StartsWith("2"));
                    model.BinhNhat = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT);
                    model.BinhNhi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI);
                    model.HaSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.HA_SI);
                    model.TrungSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI);
                    model.ThuongSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI);
                    model.Ldhd = listCanBo.Count(x => IsLdhd(x));
                    model.Vcqp = listCanBo.Count(x => IsVcqp(x));
                    model.Cnqp = listCanBo.Count(x => IsCnqp(x));
                    model.Ccqp = listCanBo.Count(x => IsCcqp(x));
                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY_CN);
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY_CN);
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN));
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN));
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN));
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN));
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN));
                }
                else
                {
                    model.ThieuUy = 0;
                    model.TrungUy = 0;
                    model.ThuongUy = 0;
                    model.DaiUy = 0;
                    model.ThieuTa = 0;
                    model.TrungTa = 0;
                    model.ThuongTa = 0;
                    model.DaiTa = 0;
                    model.Tuong = 0;
                    model.Qncn = 0;
                    model.BinhNhat = 0;
                    model.BinhNhi = 0;
                    model.HaSi = 0;
                    model.TrungSi = 0;
                    model.ThuongSi = 0;
                    model.Ldhd = 0;
                    model.Vcqp = 0;
                    model.Cnqp = 0;
                    model.Ccqp = 0;
                    model.ThieuUyCn = 0;
                    model.TrungUyCn = 0;
                    model.ThuongUyCn = 0;
                    model.DaiUyCn = 0;
                    model.ThieuTaCn = 0;
                    model.TrungTaCn = 0;
                    model.ThuongTaCn = 0;
                }
                tlQsChungTuChiTietModel.Add(model);
            }
            var listtlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(tlQsChungTuChiTietModel);
            _tlQsChungTuChiTietService.Add(listtlQsChungTuChiTiet);
        }

        public void SaveChungTuChiTiet(TlQsChungTu tlQsChungTu, bool choice)
        {
            //var listCanBo = _carderService.FindByCondition(x => x.Thang == tlQsChungTu.Thang && x.Parent == tlQsChungTu.MaDonVi && x.Nam == tlQsChungTu.Nam).ToList();
            var listCanBo = _mapper.Map<IEnumerable<CadresModel>>(_carderService.FindCanBoQuyetToanQuanSo(tlQsChungTu.MaDonVi, tlQsChungTu.Thang, tlQsChungTu.Nam));
            var lstCanBoGiam290 = _mapper.Map<IEnumerable<CadresModel>>(_carderService.FindCanBoQuyetToanQuanSoGiam(tlQsChungTu.MaDonVi, tlQsChungTu.Thang, tlQsChungTu.Nam));

            Dictionary<string, CadresModel> dicCanBoCu = new Dictionary<string, CadresModel>();
            int iThangTruoc = 0;
            int iNamTruoc = 0;
            if(tlQsChungTu.Thang == 1)
            {
                iThangTruoc = 12;
                iNamTruoc = tlQsChungTu.Nam - 1;
            }
            else
            {
                iThangTruoc = tlQsChungTu.Thang - 1;
                iNamTruoc = tlQsChungTu.Nam;
            }
            //var lstCanBoThangTruoc = _carderService.FindByCondition(x => x.Thang == iThangTruoc && x.Parent == tlQsChungTu.MaDonVi && x.Nam == iNamTruoc).ToList();
            var lstCanBoThangTruoc = _mapper.Map<IEnumerable<CadresModel>>(_carderService.FindCanBoQuyetToanQuanSo(tlQsChungTu.MaDonVi, iThangTruoc, iNamTruoc));
            if (lstCanBoThangTruoc != null)
            {
                foreach(var item in lstCanBoThangTruoc)
                {
                    if (!dicCanBoCu.ContainsKey(item.MaHieuCanBo))
                        dicCanBoCu.Add(item.MaHieuCanBo, item);
                }
            }

            var listCanBoModel = _mapper.Map<List<CadresModel>>(listCanBo);
            var listQsMucLuc = _qsMucLucService.FindAll(x => x.SHienThi != "2" && x.INamLamViec == tlQsChungTu.Nam).ToList();
            var lisTQsMucLucModel = _mapper.Map<ObservableCollection<QsMucLucModel>>(listQsMucLuc);

            var predicate = PredicateBuilder.True<TlCanBoPhuCap>();
            predicate = predicate.And(x => x.Flag == true);
            predicate = predicate.And(x => x.MaPhuCap == "LHT_HS");
            var tlCanBoPhuCap = _tlCanBoPhuCapService.FindAll(predicate);

            int qncn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.QNCN1) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                || (x.MaCb.StartsWith(MA_CAP_BAC.QNCN1) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
            int ldhd = listCanBo.Count(x => (IsLdhd(x) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                || (IsLdhd(x) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
            int vcqp = listCanBo.Count(x => (IsVcqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                || (IsVcqp(x) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
            int cnqp = listCanBo.Count(x => (IsCnqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                || (IsCnqp(x) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
            
            var listQsMucThangTruoc = _tlQsChungTuChiTietService.FirstOrDefault(x => x.Thang == iThangTruoc && x.NamLamViec == iNamTruoc && x.IdDonVi == tlQsChungTu.MaDonVi && x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);

            List<TlQsChungTuChiTietModel> tlQsChungTuChiTietModel = new List<TlQsChungTuChiTietModel>();
            var listQsMucLucParent = lisTQsMucLucModel.Where(x => x.IIdMlnsCha == null && x.SKyHieu != MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC && x.SKyHieu != MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).OrderBy(x => x.SKyHieu).ToList();
            var listQsMucLucChid = lisTQsMucLucModel.Where(x => x.IIdMlnsCha != null || x.SKyHieu == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC || x.SKyHieu == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY);

            List<TlQsChungTuChiTietModel> listChungTuChiTietChid = new List<TlQsChungTuChiTietModel>();
            List<TlQsChungTuChiTietModel> listChungTuChiTietParent = new List<TlQsChungTuChiTietModel>();
            foreach (var item in listQsMucLucChid)
            {
                TlQsChungTuChiTietModel model = new TlQsChungTuChiTietModel();
                model.IdChungTu = tlQsChungTu.Id.ToString();
                model.MlnsId = item.IIdMlns.ToString();
                model.MlnsIdParent = item.IIdMlnsCha.ToString();
                model.XauNoiMa = item.SKyHieu;
                model.MoTa = item.SMoTa;
                model.Thang = tlQsChungTu.Thang;
                model.NamLamViec = tlQsChungTu.Nam;
                model.IdDonVi = tlQsChungTu.MaDonVi;
                model.TenDonVi = tlQsChungTu.TenDonVi;
                model.DateCreated = (DateTime)tlQsChungTu.DateCreated;
                if (item.SKyHieu == MA_TANG_GIAM.TANG_QUAN_HAM_MA_250)
                {
                    model.ThieuUy = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.THIEU_UY && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungUy = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.TRUNG_UY && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongUy = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiUy = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.Tuong = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhat = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.BINH_NHAT && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.BINH_NHI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.HaSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.HA_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.TRUNG_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.THUONG_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    
                    model.ThieuUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.DaiUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThieuTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.Qncn = qncn;
                    model.Vcqp = vcqp;
                    model.Ldhd = ldhd;
                    model.Cnqp = cnqp;
                    model.Ccqp = listCanBo.Count(x => (IsCcqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (IsCcqp(x) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;
                }
                else if (item.SKyHieu == MA_TANG_GIAM.TANG_QUAN_HAM_MA_350)
                {
                    model.ThieuUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.THIEU_UY && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.TRUNG_UY && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.Tuong = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TUONG) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhat = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.BINH_NHAT && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.BINH_NHI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.HaSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.HA_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.TRUNG_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.THUONG_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.DaiUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThieuTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.Qncn = qncn;
                    model.Vcqp = vcqp;
                    model.Ldhd = ldhd;
                    model.Cnqp = cnqp;
                    model.Ccqp = listCanBo.Count(x => (IsCcqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CCQP) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                         || (IsCcqp(x) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));

                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;
                }
                else if (item.SKyHieu == MA_TANG_GIAM.CHUYEN_CHE_DO_280)
                {
                    model.ThieuUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Tuong = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.BinhNhat = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.BinhNhi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.HaSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.HA_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));

                    model.Qncn = listCanBoModel.Count(x => x.MaCb.StartsWith("2") && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Ldhd = listCanBoModel.Count(x => IsLdhd(x) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Vcqp = listCanBoModel.Count(x => IsVcqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Cnqp = listCanBoModel.Count(x => IsCnqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Ccqp = listCanBoModel.Count(x => IsCcqp(x) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa
                    //    + model.Vcqp + model.Cnqp + model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi
                    //    + model.TrungSi + model.ThuongSi;
                }
                else if (item.SKyHieu == MA_TANG_GIAM.CHUYEN_CHE_DO_380)
                {
                    model.ThieuUy = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_UY);
                    model.TrungUy = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_UY);
                    model.ThuongUy = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_UY);
                    model.DaiUy = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_UY);
                    model.ThieuTa = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_TA);
                    model.TrungTa = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_TA);
                    model.ThuongTa = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_TA);
                    model.DaiTa = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_TA);
                    model.Tuong = this.Count380(listCanBoModel, MA_CAP_BAC.TUONG);
                    model.BinhNhat = this.Count380(listCanBoModel, MA_CAP_BAC.BINH_NHAT);
                    model.BinhNhi = this.Count380(listCanBoModel, MA_CAP_BAC.BINH_NHI);
                    model.HaSi = this.Count380(listCanBoModel, MA_CAP_BAC.HA_SI);
                    model.TrungSi = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_SI);
                    model.ThuongSi = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_SI);
                    model.Qncn = this.Count380(listCanBoModel, MA_CAP_BAC.QNCN1);
                    model.Ldhd = this.Count380(listCanBoModel, MA_CAP_BAC.LDHD);
                    model.Ldhd = listCanBoModel.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && IsLdhdCu(x) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Vcqp = listCanBoModel.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && IsVcqpCu(x) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Cnqp = listCanBoModel.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && IsCnqpCu(x) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Ccqp = listCanBoModel.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && IsCcqpCu(x) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa
                    //    + model.Vcqp + model.Cnqp + model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi
                    //    + model.TrungSi + model.ThuongSi;

                    model.ThieuUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_UY_CN);
                    model.TrungUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_UY_CN);
                    model.ThuongUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_UY_CN);
                    model.DaiUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_UY_CN);
                    model.ThieuTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_TA_CN);
                    model.TrungTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_TA_CN);
                    model.ThuongTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_TA_CN);
                } else if (item.SKyHieu == MA_TANG_GIAM.GIAM_NOI_BO)
                {
                    model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Qncn = listCanBoModel.Count(x => x.MaCb.StartsWith("2") && x.MaTangGiam == item.SKyHieu.ToString());
                    model.BinhNhat = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT && x.MaTangGiam == item.SKyHieu.ToString());
                    model.BinhNhi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.HaSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HA_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Ldhd = listCanBoModel.Count(x => IsLdhd(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Vcqp = listCanBoModel.Count(x => IsVcqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Cnqp = listCanBoModel.Count(x => IsCnqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Ccqp = listCanBoModel.Count(x => IsCcqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    + model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    if (lstCanBoGiam290 != null && lstCanBoGiam290.Any())
                    {
                        model.ThieuUy += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY);
                        model.TrungUy += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY);
                        model.ThuongUy += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY));
                        model.DaiUy += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY));
                        model.ThieuTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA));
                        model.TrungTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA));
                        model.ThuongTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA));
                        model.DaiTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA));
                        model.Tuong += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG));
                        model.Qncn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith("2"));
                        model.BinhNhat += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT);
                        model.BinhNhi += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI);
                        model.HaSi += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.HA_SI);
                        model.TrungSi += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI);
                        model.ThuongSi += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI);
                        model.Ldhd += lstCanBoGiam290.Count(x => IsLdhd(x));
                        model.Vcqp += lstCanBoGiam290.Count(x => IsVcqp(x));
                        model.Cnqp += lstCanBoGiam290.Count(x => IsCnqp(x));
                        model.Ccqp += lstCanBoGiam290.Count(x => IsCcqp(x));


                        model.ThieuUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN));
                        model.TrungUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN));
                        model.ThuongUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN));
                        model.DaiUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN));
                        model.ThieuTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN));
                        model.TrungTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN));
                        model.ThuongTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN));
                    }
                }
                else
                {
                    model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Qncn = listCanBoModel.Count(x => x.MaCb.StartsWith("2") && x.MaTangGiam == item.SKyHieu.ToString());
                    model.BinhNhat = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT && x.MaTangGiam == item.SKyHieu.ToString());
                    model.BinhNhi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.HaSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HA_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Ldhd = listCanBoModel.Count(x => IsLdhd(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Vcqp = listCanBoModel.Count(x => IsVcqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Cnqp = listCanBoModel.Count(x => IsCnqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Ccqp = listCanBoModel.Count(x => IsCcqp(x) && x.MaTangGiam == item.SKyHieu.ToString());
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    + model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString());
                }
                listChungTuChiTietChid.Add(model);
            }

            TlQsChungTuChiTietModel qsTang = new TlQsChungTuChiTietModel();
            TlQsChungTuChiTietModel qsGiam = new TlQsChungTuChiTietModel();
            TlQsChungTuChiTietModel tmp = new TlQsChungTuChiTietModel();
            foreach (var item in listQsMucLucParent)
            {
                TlQsChungTuChiTietModel model = new TlQsChungTuChiTietModel();
                model.IdChungTu = tlQsChungTu.Id.ToString();
                model.MlnsId = item.IIdMlns.ToString();
                model.MlnsIdParent = item.IIdMlnsCha.ToString();
                model.XauNoiMa = item.SKyHieu;
                model.MoTa = item.SMoTa;
                model.Thang = tlQsChungTu.Thang;
                model.NamLamViec = tlQsChungTu.Nam;
                model.IdDonVi = tlQsChungTu.MaDonVi;
                model.TenDonVi = tlQsChungTu.TenDonVi;
                model.DateCreated = (DateTime)tlQsChungTu.DateCreated;
                model.DateCreated = (DateTime)tlQsChungTu.DateCreated;
                if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG || item.SKyHieu == MA_TANG_GIAM.QUAN_SO_GIAM_TRONG_THANG)
                {
                    List<TlQsChungTuChiTietModel> listChungTuTangQs;
                    if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)
                    {
                        listChungTuTangQs = listChungTuChiTietChid.Where(x => x.XauNoiMa.StartsWith("2")).ToList();
                    }
                    else
                    {
                        listChungTuTangQs = listChungTuChiTietChid.Where(x => x.XauNoiMa.StartsWith("3")).ToList();
                    }
                    model.ThieuUy = listChungTuTangQs.Sum(x => x.ThieuUy);
                    model.TrungUy = listChungTuTangQs.Sum(x => x.TrungUy);
                    model.ThuongUy = listChungTuTangQs.Sum(x => x.ThuongUy);
                    model.DaiUy = listChungTuTangQs.Sum(x => x.DaiUy);
                    model.ThieuTa = listChungTuTangQs.Sum(x => x.ThieuTa);
                    model.TrungTa = listChungTuTangQs.Sum(x => x.TrungTa);
                    model.ThuongTa = listChungTuTangQs.Sum(x => x.ThuongTa);
                    model.DaiTa = listChungTuTangQs.Sum(x => x.DaiTa);
                    model.Tuong = listChungTuTangQs.Sum(x => x.Tuong);
                    model.Qncn = listChungTuTangQs.Sum(x => x.Qncn);
                    model.BinhNhat = listChungTuTangQs.Sum(x => x.BinhNhat);
                    model.BinhNhi = listChungTuTangQs.Sum(x => x.BinhNhi);
                    model.HaSi = listChungTuTangQs.Sum(x => x.HaSi);
                    model.TrungSi = listChungTuTangQs.Sum(x => x.TrungSi);
                    model.ThuongSi = listChungTuTangQs.Sum(x => x.ThuongSi);
                    model.Ldhd = listChungTuTangQs.Sum(x => x.Ldhd);
                    model.Vcqp = listChungTuTangQs.Sum(x => x.Vcqp);
                    model.Cnqp = listChungTuTangQs.Sum(x => x.Cnqp);
                    model.Ccqp = listChungTuTangQs.Sum(x => x.Ccqp);
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;
                    model.ThieuUyCn = listChungTuTangQs.Sum(x => x.ThieuUyCn);
                    model.TrungUyCn = listChungTuTangQs.Sum(x => x.TrungUyCn);
                    model.ThuongUyCn = listChungTuTangQs.Sum(x => x.ThuongUyCn);
                    model.DaiUyCn = listChungTuTangQs.Sum(x => x.DaiUyCn);
                    model.ThieuTaCn = listChungTuTangQs.Sum(x => x.ThieuTaCn);
                    model.TrungTaCn = listChungTuTangQs.Sum(x => x.TrungTaCn);
                    model.ThuongTaCn = listChungTuTangQs.Sum(x => x.ThuongTaCn);
                    if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_TANG_TRONG_THANG)
                    {
                        qsTang = model.Clone();
                    }
                    else
                    {
                        qsGiam = model.Clone();
                    }
                }
                if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG)
                {
                    model.ThieuUy = listQsMucThangTruoc.ThieuUy + qsTang.ThieuUy - qsGiam.ThieuUy;
                    model.TrungUy = listQsMucThangTruoc.TrungUy + qsTang.TrungUy - qsGiam.TrungUy;
                    model.ThuongUy = listQsMucThangTruoc.ThuongUy + qsTang.ThuongUy - qsGiam.ThuongUy;
                    model.DaiUy = listQsMucThangTruoc.DaiUy + qsTang.DaiUy - qsGiam.DaiUy;
                    model.ThieuTa = listQsMucThangTruoc.ThieuTa + qsTang.ThieuTa - qsGiam.ThieuTa;
                    model.TrungTa = listQsMucThangTruoc.TrungTa + qsTang.TrungTa - qsGiam.TrungTa;
                    model.ThuongTa = listQsMucThangTruoc.ThuongTa + qsTang.ThuongTa - qsGiam.ThuongTa;
                    model.DaiTa = listQsMucThangTruoc.DaiTa + qsTang.DaiTa - qsGiam.DaiTa;
                    model.Tuong = listQsMucThangTruoc.Tuong + qsTang.Tuong - qsGiam.Tuong;
                    model.Qncn = listQsMucThangTruoc.Qncn + qsTang.Qncn - qsGiam.Qncn;
                    model.BinhNhat = listQsMucThangTruoc.BinhNhat + qsTang.BinhNhat - qsGiam.BinhNhat;
                    model.BinhNhi = listQsMucThangTruoc.BinhNhi + qsTang.BinhNhi - qsGiam.BinhNhi;
                    model.HaSi = listQsMucThangTruoc.HaSi + qsTang.HaSi - qsGiam.HaSi;
                    model.TrungSi = listQsMucThangTruoc.TrungSi + qsTang.TrungSi - qsGiam.TrungSi;
                    model.ThuongSi = listQsMucThangTruoc.ThuongSi + qsTang.ThuongSi - qsGiam.ThuongSi;
                    model.Ldhd = listQsMucThangTruoc.Ldhd + qsTang.Ldhd - qsGiam.Ldhd;
                    model.Vcqp = listQsMucThangTruoc.Vcqp + qsTang.Vcqp - qsGiam.Vcqp;
                    model.Cnqp = listQsMucThangTruoc.Cnqp + qsTang.Cnqp - qsGiam.Cnqp;
                    model.Ccqp = listQsMucThangTruoc.Ccqp ?? 0 + qsTang.Ccqp - qsGiam.Ccqp;
                    //model.TongSo = qsTang.TongSo - qsGiam.TongSo;
                    model.ThieuUyCn = listQsMucThangTruoc.ThieuUyCn + qsTang.ThieuUyCn - qsGiam.ThieuUyCn;
                    model.TrungUyCn = listQsMucThangTruoc.TrungUyCn + qsTang.TrungUyCn - qsGiam.TrungUyCn;
                    model.ThuongUyCn = listQsMucThangTruoc.ThuongUyCn + qsTang.ThuongUyCn - qsGiam.ThuongUyCn;
                    model.DaiUyCn = listQsMucThangTruoc.DaiUyCn + qsTang.DaiUyCn - qsGiam.DaiUyCn;
                    model.ThieuTaCn = listQsMucThangTruoc.ThieuTaCn + qsTang.ThieuTaCn - qsGiam.ThieuTaCn;
                    model.TrungTaCn = listQsMucThangTruoc.TrungTaCn + qsTang.TrungTaCn - qsGiam.TrungTaCn;
                    model.ThuongTaCn = listQsMucThangTruoc.ThuongTaCn + qsTang.ThuongTaCn - qsGiam.ThuongTaCn;
                }
                if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY)
                {
                    model.ThieuUy = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThieuUy + (qsTang.ThieuUy - qsGiam.ThieuUy) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThieuUy + tmp.ThieuUy;
                    model.TrungUy = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TrungUy + (qsTang.TrungUy - qsGiam.TrungUy) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TrungUy + tmp.TrungUy;
                    model.ThuongUy = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThuongUy + (qsTang.ThuongUy - qsGiam.ThuongUy) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThuongUy + tmp.ThuongUy;
                    model.DaiUy = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).DaiUy + (qsTang.DaiUy - qsGiam.DaiUy) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).DaiUy + tmp.DaiUy;
                    model.ThieuTa = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThieuTa + (qsTang.ThieuTa - qsGiam.ThieuTa) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThieuTa + tmp.ThieuTa;
                    model.TrungTa = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TrungTa + (qsTang.TrungTa - qsGiam.TrungTa) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TrungTa + tmp.TrungTa;
                    model.ThuongTa = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThuongTa + (qsTang.ThuongTa - qsGiam.ThuongTa) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThuongTa + tmp.ThuongTa;
                    model.DaiTa = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).DaiTa + (qsTang.DaiTa - qsGiam.DaiTa) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).DaiTa + tmp.DaiTa;
                    model.Tuong = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Tuong + (qsTang.Tuong - qsGiam.Tuong) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Tuong + tmp.Tuong;
                    model.Qncn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Qncn + (qsTang.Qncn - qsGiam.Qncn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Qncn + tmp.Qncn;
                    model.BinhNhat = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).BinhNhat + (qsTang.BinhNhat - qsGiam.BinhNhat) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).BinhNhat + tmp.BinhNhat;
                    model.BinhNhi = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).BinhNhi + (qsTang.BinhNhi - qsGiam.BinhNhi) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).BinhNhi + tmp.BinhNhi;
                    model.HaSi = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).HaSi + (qsTang.HaSi - qsGiam.HaSi) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).HaSi + tmp.HaSi;
                    model.TrungSi = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TrungSi + (qsTang.TrungSi - qsGiam.TrungSi) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TrungSi + tmp.TrungSi;
                    model.ThuongSi = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThuongSi + (qsTang.ThuongSi - qsGiam.ThuongSi) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThuongSi + tmp.ThuongSi;
                    model.Ldhd = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Ldhd + (qsTang.Ldhd - qsGiam.Ldhd) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Ldhd + tmp.Ldhd;
                    model.Vcqp = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Vcqp + (qsTang.Vcqp - qsGiam.Vcqp) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Vcqp + tmp.Vcqp;
                    model.Cnqp = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Cnqp + (qsTang.Cnqp - qsGiam.Cnqp) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Cnqp + tmp.Cnqp;
                    model.Ccqp = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).Ccqp + (qsTang.Ccqp - qsGiam.Ccqp) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).Ccqp + (tmp.Ccqp ?? 0);
                    //model.TongSo = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TongSo + (qsTang.TongSo - qsGiam.TongSo) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TongSo + tmp.TongSo;

                    model.ThieuUyCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThieuUyCn + (qsTang.ThieuUyCn - qsGiam.ThieuUyCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThieuUyCn + tmp.ThieuUyCn;
                    model.TrungUyCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TrungUyCn + (qsTang.TrungUyCn - qsGiam.TrungUyCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TrungUyCn + tmp.TrungUyCn;
                    model.ThuongUyCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThuongUyCn + (qsTang.ThuongUyCn - qsGiam.ThuongUyCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThuongUyCn + tmp.ThuongUyCn;
                    model.DaiUyCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).DaiUyCn + (qsTang.DaiUyCn - qsGiam.DaiUyCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).DaiUyCn + tmp.DaiUyCn;
                    model.ThieuTaCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThieuTaCn + (qsTang.ThieuTaCn - qsGiam.ThieuTaCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThieuTaCn + tmp.ThieuTaCn;
                    model.TrungTaCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).TrungTaCn + (qsTang.TrungTaCn - qsGiam.TrungTaCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).TrungTaCn + tmp.TrungTaCn;
                    model.ThuongTaCn = listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC).ThuongTaCn + (qsTang.ThuongTaCn - qsGiam.ThuongTaCn) + listChungTuChiTietChid.FirstOrDefault(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).ThuongTaCn + tmp.ThuongTaCn;
                }
                if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_THANG_TRUOC)
                {
                    var predicateCheck = PredicateBuilder.True<TlQsChungTuChiTiet>();
                    DateTime date = new DateTime(tlQsChungTu.Nam, tlQsChungTu.Thang, 1).AddMonths(-1);
                    predicateCheck = predicateCheck.And(x => x.Thang == date.Month);
                    predicateCheck = predicateCheck.And(x => x.NamLamViec == date.Year);
                    predicateCheck = predicateCheck.And(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);
                    predicateCheck = predicateCheck.And(x => x.IdDonVi.Equals(tlQsChungTu.MaDonVi));
                    var dataCheck = _tlQsChungTuChiTietService.FindAll(predicateCheck).FirstOrDefault();
                    if (dataCheck == null)
                    {
                        model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY);
                        model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY);
                        model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY));
                        model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY));
                        model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA));
                        model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA));
                        model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA));
                        model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA));
                        model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG));
                        model.Qncn = listCanBoModel.Count(x => x.MaCb.StartsWith("2"));
                        model.BinhNhat = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT);
                        model.BinhNhi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI);
                        model.HaSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HA_SI);
                        model.TrungSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI);
                        model.ThuongSi = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI);
                        model.Ldhd = listCanBoModel.Count(x => IsLdhd(x));
                        model.Vcqp = listCanBoModel.Count(x => IsVcqp(x));
                        model.Cnqp = listCanBoModel.Count(x => IsCnqp(x));
                        model.Ccqp = listCanBoModel.Count(x => IsCcqp(x));
                        //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                        //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

                        model.ThieuUyCn = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY_CN);
                        model.TrungUyCn = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY_CN);
                        model.ThuongUyCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN));
                        model.DaiUyCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN));
                        model.ThieuTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN));
                        model.TrungTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN));
                        model.ThuongTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN));
                        tmp = model;
                    }
                    else
                    {
                        model = _mapper.Map<TlQsChungTuChiTietModel>(listQsMucThangTruoc).Clone();
                        model.Ccqp = model.Ccqp ?? 0;
                        model.Id = Guid.NewGuid();
                        model.IdChungTu = tlQsChungTu.Id.ToString();
                        model.MlnsId = item.IIdMlns.ToString();
                        model.MlnsIdParent = item.IIdMlnsCha.ToString();
                        model.XauNoiMa = item.SKyHieu;
                        model.MoTa = item.SMoTa;
                        model.Thang = tlQsChungTu.Thang;
                        model.NamLamViec = tlQsChungTu.Nam;
                        model.IdDonVi = tlQsChungTu.MaDonVi;
                        model.TenDonVi = tlQsChungTu.TenDonVi;
                        model.DateCreated = (DateTime)tlQsChungTu.DateCreated;
                        model.DateCreated = (DateTime)tlQsChungTu.DateCreated;
                        tmp = model.Clone();
                    }
                }
                listChungTuChiTietParent.Add(model);
            }
            tlQsChungTuChiTietModel.AddRange(listChungTuChiTietParent);
            tlQsChungTuChiTietModel.AddRange(listChungTuChiTietChid);

            var listtlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(tlQsChungTuChiTietModel);
            if (choice)
            {
                _listChungTuTongHop.AddRange(listtlQsChungTuChiTiet.ToList());
                _listMucLucQs = listQsMucLuc.Clone();
                _tlQsChungTuChiTietService.Add(listtlQsChungTuChiTiet);
            }
            else
            {
                _tlQsChungTuChiTietService.Add(listtlQsChungTuChiTiet);
            }
        }

        private void OnSaveSynthetic()
        {
            if (SelectedNsDonVi == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(Resources.MsgThQtQsDonViTh), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SaveChungTuCon();
            }
        }

        private void SaveChungTuCon()
        {
            string message = GetValidate();
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.Forms.MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<TlQsChungTu> listChungTuCon = new List<TlQsChungTu>();
                    var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);

                    //reset chung tu tong hop
                    _listChungTuTongHop = new List<TlQsChungTuChiTiet>();
                    foreach (var donVi in lstDonVi)
                    {
                        TlQsChungTu tlQsChungTu = new TlQsChungTu();
                        Model.MoTa = MoTaChungTu;
                        Model.DateCreated = DateTime.Now;
                        Model.Thang = int.Parse(SelectedMonth.ValueItem);
                        Model.MaDonVi = donVi.MaDonVi;
                        Model.TenDonVi = donVi.TenDonVi;
                        Model.IsLock = true;
                        Model.SoChungTu = SinhMaChungTu();
                        Model.Nam = int.Parse(SelectedYear.ValueItem);

                        _mapper.Map(Model, tlQsChungTu);
                        _tlQsChungTuService.Add(tlQsChungTu);
                        listChungTuCon.Add(tlQsChungTu);
                        SaveChungTuChiTiet(tlQsChungTu, true);
                    }

                    TlQsChungTuModel tlQsChungTuModel = new TlQsChungTuModel();
                    tlQsChungTuModel.Thang = int.Parse(SelectedMonth.ValueItem);
                    tlQsChungTuModel.Nam = int.Parse(SelectedYear.ValueItem);
                    tlQsChungTuModel.MaDonVi = SelectedNsDonVi.IIDMaDonVi;
                    tlQsChungTuModel.TenDonVi = SelectedNsDonVi.TenDonVi;
                    tlQsChungTuModel.DateCreated = DateTime.Now;
                    tlQsChungTuModel.UserCreated = _sessionService.Current.Principal;
                    tlQsChungTuModel.IsLock = false;
                    tlQsChungTuModel.MoTa = MoTaChungTu;
                    tlQsChungTuModel.NgayTao = Model.NgayTao;
                    tlQsChungTuModel.STongHop = string.Join("','", listChungTuCon.Select(x => x.Id.ToString()));
                    tlQsChungTuModel.BDaTongHop = false;
                    tlQsChungTuModel.BNganSachNhanDuLieu = false;
                    tlQsChungTuModel.SoChungTu = SinhMaChungTu();
                    tlQsChungTuModel.Id = Guid.NewGuid();

                    List<TlQsChungTuChiTietModel> chungTuChiTietTongHop = new List<TlQsChungTuChiTietModel>();
                    foreach (var mucLuc in _listMucLucQs)
                    {
                        TlQsChungTuChiTietModel model = new TlQsChungTuChiTietModel();
                        model.IdChungTu = tlQsChungTuModel.Id.ToString();
                        model.MlnsIdParent = mucLuc.IIdMlnsCha.ToString();
                        model.XauNoiMa = mucLuc.SKyHieu;
                        model.MlnsId = mucLuc.IIdMlns.ToString();
                        model.MoTa = mucLuc.SMoTa;
                        model.Thang = tlQsChungTuModel.Thang;
                        model.NamLamViec = tlQsChungTuModel.Nam;
                        model.IdDonVi = tlQsChungTuModel.MaDonVi;
                        model.TenDonVi = tlQsChungTuModel.TenDonVi;
                        model.DateCreated = (DateTime)tlQsChungTuModel.DateCreated;
                        model.UserCreator = tlQsChungTuModel.UserCreated;

                        var ChungTuChiTietMucLuc = _listChungTuTongHop.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa));

                        model.ThieuUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuUy);
                        model.TrungUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungUy);
                        model.ThuongUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongUy);
                        model.DaiUy = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiUy);
                        model.ThieuTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuTa);
                        model.TrungTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungTa);
                        model.ThuongTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongTa);
                        model.DaiTa = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiTa);
                        model.BinhNhi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.BinhNhi);
                        model.BinhNhat = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.BinhNhat);
                        model.HaSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.HaSi);
                        model.TrungSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungSi);
                        model.ThuongSi = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongSi);
                        model.Tuong = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Tuong);
                        model.ThieuUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuUyCn);
                        model.TrungUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungUyCn);
                        model.ThuongUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongUyCn);
                        model.DaiUyCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.DaiUyCn);
                        model.ThieuTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThieuTaCn);
                        model.TrungTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TrungTaCn);
                        model.ThuongTaCn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.ThuongTaCn);
                        model.Qncn = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Qncn);
                        model.Vcqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Vcqp);
                        model.Ldhd = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Ldhd);
                        model.Cnqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Cnqp);
                        model.Ccqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Ccqp);
                        //model.TongSo = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TongSo);

                        chungTuChiTietTongHop.Add(model);
                    }

                    var tlQsChungTuTH = _mapper.Map<TlQsChungTu>(tlQsChungTuModel);
                    var tlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(chungTuChiTietTongHop).ToList();
                    _tlQsChungTuService.Add(tlQsChungTuTH);
                    _tlQsChungTuChiTietService.Add(tlQsChungTuChiTiet);
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        SavedAction?.Invoke(null);

                        // Invoke message
                        MessageBoxHelper.Info(Resources.MsgSaveDone);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
        }

        private string SinhMaChungTu()
        {
            var soChungTuIndex = _tlQsChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            return "QS-" + soChungTuIndex.ToString("D3");
        }

        private int Count380(List<CadresModel> listCanBo, string maCbCu)
        {
            return listCanBo.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(maCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
        }

        private string GetValidate()
        {
            int thang = int.Parse(SelectedMonth.ValueItem);
            int nam = int.Parse(SelectedYear.ValueItem);

            List<string> messages = new List<string>();
            if (!ItemsDonVi.Any(x => x.IsSelected))
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
            else if (SelectedMonth == null)
            {
                messages.Add(string.Format(Resources.MonthNull));
                goto End;
            }

            var lstDonVi = ItemsDonVi.Where(x => x.IsSelected);
            var data = _tlQsChungTuService.FindAll().ToList();
            foreach (var donVi in lstDonVi)
            {
                int iThangTruoc = 0;
                int iNamTruoc = 0;
                if (int.Parse(SelectedMonth.ValueItem) == 1)
                {
                    iThangTruoc = 12;
                    iNamTruoc = int.Parse(SelectedYear.ValueItem) - 1;
                }
                else
                {
                    iThangTruoc = int.Parse(SelectedMonth.ValueItem) - 1;
                    iNamTruoc = int.Parse(SelectedYear.ValueItem);
                }
                var predicateCheck = PredicateBuilder.True<TlQsChungTuChiTiet>();
                DateTime date = new DateTime(int.Parse(SelectedYear.ValueItem), int.Parse(SelectedMonth.ValueItem), 1).AddMonths(-1);
                predicateCheck = predicateCheck.And(x => x.Thang == date.Month);
                predicateCheck = predicateCheck.And(x => x.NamLamViec == date.Year);
                predicateCheck = predicateCheck.And(x => x.IdDonVi == donVi.MaDonVi);
                var dataCheck = _tlQsChungTuChiTietService.FindAll(predicateCheck).ToList();

                if (dataCheck.IsEmpty())
                {
                    messages.Add(string.Format(Resources.LicenseNull, iThangTruoc, iNamTruoc, donVi.TenDonVi));
                    goto End;
                }
                else if (data.Count(x => x.Thang == thang && x.MaDonVi == donVi.MaDonVi && x.Nam == nam) > 1)
                {
                    messages.Add(string.Format(Resources.MsgExistLicene, SelectedMonth.ValueItem, SelectedYear.ValueItem, donVi.TenDonVi));
                    goto End;
                }
            }
            foreach (var donVi in lstDonVi)
            {
                var predicate = PredicateBuilder.True<TlQsChungTu>();
                predicate = predicate.And(x => x.Thang == int.Parse(SelectedMonth.ValueItem));
                predicate = predicate.And(x => x.Nam == int.Parse(SelectedYear.ValueItem));
                predicate = predicate.And(x => x.MaDonVi == donVi.MaDonVi);
                var tlChungTu = _tlQsChungTuService.FindAll(predicate);
                if (!tlChungTu.IsEmpty())
                {
                    messages.Add(string.Format(Resources.MsgChungTuQsTonTai, donVi.TenDonVi));
                    goto End;
                }
            }
        End:
            return string.Join(Environment.NewLine, messages);
        }
    }
}
