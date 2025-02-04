using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber
{
    public class SettlementNumberInittizationDialogViewModel : DialogViewModelBase<TlQsChungTuModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly INsQsMucLucService _nsQsMucLucService;
        private readonly ITlQsChungTuService _tlQsChungTuService;
        private readonly ITlDmCanBoService _tlDmCanBoService;
        private readonly ITlDmCapBacService _dmCapBacService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private ICollectionView _itemsDonViCollectionView;
        private Dictionary<string, TlDmCapBac> _capBacs = new Dictionary<string, TlDmCapBac>();

        public override string FuncCode => NSFunctionCode.SALARY_SETTLEMENT_SETTLEMENT_NUMBER_INITIALIZATION;
        public override string Title => "Khởi tạo dữ liệu";
        public override string Description => "Khởi tạo dữ liệu quân số tháng trước mã 100";
        public override Type ContentType => typeof(View.Salary.Settlement.SalarySettlementNumber.SettlementNumberInittizationDialog);
        public bool IsReadOnly => ViewState == FormViewState.ADD;

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

        private ObservableCollection<TlDmDonViModel> _donViItems;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViModel _selectedDonViItems;
        public TlDmDonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _itemsDonViCollectionView != null)
                {
                    _itemsDonViCollectionView.Refresh();
                }
            }
        }

        private ObservableCollection<TlQsChungTuChiTietModel> _dataItems;
        public ObservableCollection<TlQsChungTuChiTietModel> DataItems
        {
            get => _dataItems;
            set => SetProperty(ref _dataItems, value);
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private bool _selectedAllDonVi;
        public bool SelectedAllDonVi
        {
            get => !DonViItems.IsEmpty() && DonViItems.All(x => x.IsSelected);
            set
            {
                SetProperty(ref _selectedAllDonVi, value);
                foreach (var item in DonViItems) item.IsSelected = _selectedAllDonVi;
            }
        }

        public string LabelSelectedDonVi
        {
            get
            {
                var totalCount = DonViItems.IsEmpty() ? 0 : DonViItems.Count();
                var totalSelectedCount = DonViItems.IsEmpty() ? 0 : DonViItems.Count(x => x.IsSelected);
                return $"Đơn vị ({totalSelectedCount} / {totalCount})";
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
                    _itemsDonViCollectionView.Refresh();
                }
            }
        }

        public RelayCommand OnRefreshCommand { get; }

        public SettlementNumberInittizationDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViService tlDmDonViService,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            INsQsMucLucService nsQsMucLucService,
            ITlQsChungTuService tlQsChungTuService,
            ITlDmCanBoService tlDmCanBoService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmCapBacService dmCapBacService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _tlDmDonViService = tlDmDonViService;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            _nsQsMucLucService = nsQsMucLucService;
            _tlQsChungTuService = tlQsChungTuService;
            _tlDmCanBoService = tlDmCanBoService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            OnRefreshCommand = new RelayCommand(obj => OnRefresh());
            _dmCapBacService = dmCapBacService;
        }

        public override void Init()
        {
            base.Init();
            LoadDonVi();
            LoadData();
        }

        public void LoadDonVi()
        {
            try
            {
                var data = _tlDmDonViService.FindAllDonViQuanSo(null, Model.Nam);
                _donViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
                foreach (var item in _donViItems)
                {
                    item.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(SelectedAllDonVi));
                        OnPropertyChanged(nameof(LabelSelectedDonVi));
                    };
                }
                _itemsDonViCollectionView = CollectionViewSource.GetDefaultView(_donViItems);
                _itemsDonViCollectionView.Filter = ListAgencyFilter;
                OnPropertyChanged(nameof(SelectedAllDonVi));
                OnPropertyChanged(nameof(LabelSelectedDonVi));
                OnPropertyChanged(nameof(DonViItems));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
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
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                var listDonViSelect = DonViItems.Where(x => x.IsSelected).ToList();
                foreach (var item in listDonViSelect)
                {
                    var tlQsChungTu = new TlQsChungTu();
                    var predicate = PredicateBuilder.True<TlDmCanBo>();
                    predicate = predicate.And(x => x.Nam == Model.Nam);
                    predicate = predicate.And(x => item.MaDonVi.Equals(x.Parent));
                    var listCanBo = _tlDmCanBoService.FindByCondition(predicate).ToList();
                    List<TlDmCanBo> listCbonChungTu = new List<TlDmCanBo>();
                    int thang = 0;
                    for (int i = 1; i <= 12; i++)
                    {
                        listCbonChungTu = listCanBo.Where(x => x.Thang == i).ToList();
                        if (!listCbonChungTu.IsEmpty())
                        {
                            thang = i;
                            break;
                        }
                    }
                    if (!listCbonChungTu.IsEmpty())
                    {
                        tlQsChungTu.MoTa = "Chứng từ chi tiết khởi tạo đầu kì";
                        tlQsChungTu.DateCreated = DateTime.Now;
                        tlQsChungTu.UserCreated = _sessionService.Current.Principal;
                        tlQsChungTu.NgayTao = DateTime.Now;
                        tlQsChungTu.Thang = thang;
                        tlQsChungTu.IsLock = false;
                        tlQsChungTu.MaDonVi = item.MaDonVi;
                        tlQsChungTu.TenDonVi = item.TenDonVi;
                        tlQsChungTu.SoChungTu = SinhMaChungTu();
                        tlQsChungTu.Nam = Model.Nam;

                        _tlQsChungTuService.Add(tlQsChungTu);
                        SaveChungTuChiTiet(tlQsChungTu, listCbonChungTu);
                    }
                    e.Result = tlQsChungTu;
                }
            }, (s, e) =>
            {
                IsLoading = false;

                if (e.Error == null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    SavedAction?.Invoke(null);

                    // Invoke message
                    var chungtu = _mapper.Map<TlQsChungTu>(e.Result);
                    MessageBoxHelper.Info(string.Format(Resources.MsgKhoiTaoChungTuQuanSo , chungtu.Thang, chungtu.Nam));
                }
                else
                {
                    _logger.Error(e.Error.Message);

                }
            });
        }

        private bool IsVcqp(TlDmCanBo model)
        {
            if (MA_CAP_BAC.VCQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.VCQP.Equals(capBac.Parent) && !IsLdhd(model);
        }

        private bool IsCnqp(TlDmCanBo model)
        {
            if (MA_CAP_BAC.CNQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.CNQP.Equals(capBac.Parent);
        }
        private bool IsCcqp(TlDmCanBo model)
        {
            if (MA_CAP_BAC.CCQP.Equals(model.MaCb)) return true;
            _ = _capBacs.TryGetValue(model.MaCb, out var capBac);
            return MA_CAP_BAC.CCQP.Equals(capBac.Parent);
        }
        private bool IsLdhd(TlDmCanBo model)
        {
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCb);
        }

        public bool CheckParent(string key, string maCB, Dictionary<string, TlDmCapBac> capBacs)
        {
            _ = capBacs.TryGetValue(maCB, out var child) ? child : null;
            if (child is object)
            {
                if (key.Equals(child.MaCb)) return true;
                return !string.IsNullOrEmpty(child.Parent) && CheckParent(key, child.Parent, capBacs);
            }
            return false;
        }

        public void SaveChungTuChiTiet(TlQsChungTu tlQsChungTu, List<TlDmCanBo> listCbonChungTu)
        {
            var listQsMucLuc = _nsQsMucLucService.FindAll(x => x.SHienThi != "2" && x.INamLamViec == tlQsChungTu.Nam).ToList();
            var lisTQsMucLucModel = _mapper.Map<ObservableCollection<QsMucLucModel>>(listQsMucLuc);
            _capBacs = _dmCapBacService.FindAll().ToDictionary(x => x.MaCb, x => x);
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
                    model.ThieuUy = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY)+ listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1);
                    model.TrungUy = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY) + listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2);
                    model.ThuongUy = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3));
                    model.DaiUy = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4));
                    model.ThieuTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5));
                    model.TrungTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6));
                    model.ThuongTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7));
                    model.DaiTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8));
                    model.Tuong = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9));
                    model.Qncn = listCbonChungTu.Count(x => x.MaCb.StartsWith("2"));
                    model.BinhNhat = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT);
                    model.BinhNhi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI);
                    model.HaSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.HA_SI);
                    model.TrungSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI);
                    model.ThuongSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI);
                    model.Ldhd = listCbonChungTu.Count(x => IsLdhd(x));
                    model.Vcqp = listCbonChungTu.Count(x => IsVcqp(x));
                    model.Cnqp = listCbonChungTu.Count(x => IsCnqp(x));
                    model.Ccqp = listCbonChungTu.Count(x => IsCcqp(x));
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

                    model.ThieuUyCn = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY_CN) + listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.CMKYCY_THIEUUY);
                    model.TrungUyCn = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY_CN) + listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.CMKYCY_TRUNGUY);
                    model.ThuongUyCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY));
                    model.DaiUyCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY));
                    model.ThieuTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA));
                    model.TrungTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA));
                    model.ThuongTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN)) + listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA));
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
                    //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
                    //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

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

        public void OnRefresh()
        {
            LoadData();
        }

        private string SinhMaChungTu()
        {
            var soChungTuIndex = _tlQsChungTuService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            return "QS-" + soChungTuIndex.ToString("D3");
        }
    }
}
