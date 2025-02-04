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
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewSettlement.NewSettlementNumber
{
    public class NewSettlementNumberInittizationDialogViewModel : DialogViewModelBase<TlQsChungTuNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlQsChungTuChiTietNq104Service _tlQsChungTuChiTietService;
        private readonly INsQsMucLucService _nsQsMucLucService;
        private readonly ITlQsChungTuNq104Service _tlQsChungTuService;
        private readonly ITlDmCanBoNq104Service _tlDmCanBoService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private ICollectionView _itemsDonViCollectionView;

        public override string FuncCode => NSFunctionCode.NEW_SALARY_SETTLEMENT_SETTLEMENT_NUMBER_INITIALIZATION;
        public override string Title => "Khởi tạo dữ liệu";
        public override string Description => "Khởi tạo dữ liệu quân số tháng trước mã 100";
        public override Type ContentType => typeof(View.NewSalary.NewSettlement.NewSalarySettlementNumber.NewSettlementNumberInittizationDialog);
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

        private ObservableCollection<TlDmDonViNq104Model> _donViItems;
        public ObservableCollection<TlDmDonViNq104Model> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private TlDmDonViNq104Model _selectedDonViItems;
        public TlDmDonViNq104Model SelectedDonViItems
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

        private List<TlQsChungTuChiTietNq104> _listChungTuTongHop = new List<TlQsChungTuChiTietNq104>();
        private List<NsQsMucLuc> _listMucLucQs = new List<NsQsMucLuc>();
        private ObservableCollection<TlQsChungTuChiTietNq104Model> _dataItems;
        public ObservableCollection<TlQsChungTuChiTietNq104Model> DataItems
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

        public NewSettlementNumberInittizationDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            ITlDmDonViNq104Service tlDmDonViService,
            ITlQsChungTuChiTietNq104Service tlQsChungTuChiTietService,
            INsQsMucLucService nsQsMucLucService,
            ITlQsChungTuNq104Service tlQsChungTuService,
            ITlDmCanBoNq104Service tlDmCanBoService,
            ITlCanBoPhuCapNq104Service tlCanBoPhuCapService)
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
                _donViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
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
            return obj is TlDmDonViNq104Model item && item.TenDonVi.ToLower().Contains(_searchText!.ToLower());
        }

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                var listDonViSelect = DonViItems.Where(x => x.IsSelected).ToList();
                foreach (var item in listDonViSelect)
                {
                    var tlQsChungTu = new TlQsChungTuNq104();
                    var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                    predicate = predicate.And(x => x.Nam == Model.Nam);
                    predicate = predicate.And(x => item.MaDonVi.Equals(x.Parent));
                    var listCanBo = _tlDmCanBoService.FindByCondition(predicate).ToList();
                    List<TlDmCanBoNq104> listCbonChungTu = new List<TlDmCanBoNq104>();
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
                        tlQsChungTu.Thang = 0;
                        tlQsChungTu.IsLock = false;
                        tlQsChungTu.MaDonVi = item.MaDonVi;
                        tlQsChungTu.TenDonVi = item.TenDonVi;
                        tlQsChungTu.SoChungTu = SinhMaChungTu();
                        tlQsChungTu.Nam = Model.Nam;

                        _tlQsChungTuService.Add(tlQsChungTu);
                        SaveChungTuChiTiet(tlQsChungTu, false);
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
                    var chungtu = _mapper.Map<TlQsChungTuNq104>(e.Result);
                    MessageBoxHelper.Info(string.Format(Resources.MsgKhoiTaoChungTuQuanSo, chungtu.Thang, chungtu.Nam));
                }
                else
                {
                    _logger.Error(e.Error.Message);

                }
            });
        }

        //private bool IsVcqp(TlDmCanBoNq104 model)
        //{
        //    return model.MaCb.Equals(MA_CAP_BAC.VCQP) || (model.Parent.Equals(MA_CAP_BAC.VCQP) && !IsLdhd(model));
        //}

        //private bool IsCnqp(TlDmCanBoNq104 model)
        //{
        //    return model.MaCb.Equals(MA_CAP_BAC.CNQP) || model.Parent.Equals(MA_CAP_BAC.CNQP);
        //}
        //private bool IsCcqp(TlDmCanBoNq104 model)
        //{
        //    return model.MaCb.Equals(MA_CAP_BAC.CCQP) || model.Parent.Equals(MA_CAP_BAC.CCQP);
        //}
        //private bool IsLdhd(TlDmCanBoNq104 model)
        //{
        //    var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
        //    return listMaCapBacLdhd.Contains(model.MaCb);
        //}


        private bool IsVcqp(CadresNq104Model model)
        {
            return model.MaCb.Equals(MA_CAP_BAC.VCQP) || (model.Parent.Equals(MA_CAP_BAC.VCQP) && !IsLdhd(model));
        }

        private bool IsVcqpCu(CadresNq104Model model)
        {
            return model.MaCbCu.Equals(MA_CAP_BAC.VCQP) || (model.Parent.Equals(MA_CAP_BAC.VCQP) && !IsLdhdCu(model));
        }

        private bool IsCnqp(CadresNq104Model model)
        {
            return model.MaCb.Equals(MA_CAP_BAC.CNQP) || model.Parent.Equals(MA_CAP_BAC.CNQP);
        }

        private bool IsCnqpCu(CadresNq104Model model)
        {
            return model.MaCbCu.Equals(MA_CAP_BAC.CNQP) || model.ParentCu.Equals(MA_CAP_BAC.CNQP);
        }

        private bool IsCcqp(CadresNq104Model model)
        {
            return model.MaCb.Equals(MA_CAP_BAC.CCQP) || model.Parent.Equals(MA_CAP_BAC.CCQP);
        }

        private bool IsCcqpCu(CadresNq104Model model)
        {
            return model.MaCbCu.Equals(MA_CAP_BAC.CCQP) || model.ParentCu.Equals(MA_CAP_BAC.CCQP);
        }

        private bool IsLdhd(CadresNq104Model model)
        {
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCb);
        }

        private bool IsLdhdCu(CadresNq104Model model)
        {
            var listMaCapBacLdhd = new List<string>() { "423", "425", "43" };
            return listMaCapBacLdhd.Contains(model.MaCbCu);
        }


        //public void SaveChungTuChiTiet(TlQsChungTuNq104 tlQsChungTu, List<TlDmCanBoNq104> listCbonChungTu)
        //{
        //    var listQsMucLuc = _nsQsMucLucService.FindAll(x => x.SHienThi != "2" && x.SM != "0" && x.INamLamViec == tlQsChungTu.Nam && x.ITrangThai == ItrangThaiStatus.ON).ToList();
        //    var lisTQsMucLucModel = _mapper.Map<ObservableCollection<QsMucLucModel>>(listQsMucLuc);
        //    List<TlQsChungTuChiTietNq104Model> tlQsChungTuChiTietModel = new List<TlQsChungTuChiTietNq104Model>();
        //    foreach (var item in lisTQsMucLucModel)
        //    {
        //        TlQsChungTuChiTietNq104Model model = new TlQsChungTuChiTietNq104Model();
        //        model.IdChungTu = tlQsChungTu.Id.ToString();
        //        model.MlnsId = item.IIdMlns.ToString();
        //        model.MlnsIdParent = item.IIdMlnsCha.ToString();
        //        model.XauNoiMa = item.SKyHieu;
        //        model.MoTa = item.SMoTa;
        //        model.Thang = tlQsChungTu.Thang;
        //        model.NamLamViec = tlQsChungTu.Nam;
        //        model.IdDonVi = tlQsChungTu.MaDonVi;
        //        model.TenDonVi = tlQsChungTu.TenDonVi;
        //        model.DateCreated = DateTime.Now;
        //        if (item.SKyHieu == MA_TANG_GIAM.QUAN_SO_THANG_TRUOC || item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY || item.SKyHieu == MA_TANG_GIAM.QUAN_SO_QT_TRONG_THANG)
        //        {
        //            model.ThieuUy = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY);
        //            model.TrungUy = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY);
        //            model.ThuongUy = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY));
        //            model.DaiUy = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY));
        //            model.ThieuTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA));
        //            model.TrungTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA));
        //            model.ThuongTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA));
        //            model.DaiTa = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA));
        //            model.Tuong = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG));
        //            model.Qncn = listCbonChungTu.Count(x => x.MaCb.StartsWith("2"));
        //            model.BinhNhat = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT);
        //            model.BinhNhi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI);
        //            model.HaSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.HA_SI);
        //            model.TrungSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI);
        //            model.ThuongSi = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI);
        //            model.Ldhd = listCbonChungTu.Count(x => IsLdhd(x));
        //            model.Vcqp = listCbonChungTu.Count(x => IsVcqp(x));
        //            model.Cnqp = listCbonChungTu.Count(x => IsCnqp(x));
        //            model.Ccqp = listCbonChungTu.Count(x => IsCcqp(x));
        //            //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
        //            //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

        //            model.ThieuUyCn = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY_CN);
        //            model.TrungUyCn = listCbonChungTu.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY_CN);
        //            model.ThuongUyCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN));
        //            model.DaiUyCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN));
        //            model.ThieuTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN));
        //            model.TrungTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN));
        //            model.ThuongTaCn = listCbonChungTu.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN));
        //        }
        //        else
        //        {
        //            model.ThieuUy = 0;
        //            model.TrungUy = 0;
        //            model.ThuongUy = 0;
        //            model.DaiUy = 0;
        //            model.ThieuTa = 0;
        //            model.TrungTa = 0;
        //            model.ThuongTa = 0;
        //            model.DaiTa = 0;
        //            model.Tuong = 0;
        //            model.Qncn = 0;
        //            model.BinhNhat = 0;
        //            model.BinhNhi = 0;
        //            model.HaSi = 0;
        //            model.TrungSi = 0;
        //            model.ThuongSi = 0;
        //            model.Ldhd = 0;
        //            model.Vcqp = 0;
        //            model.Cnqp = 0;
        //            model.Ccqp = 0;
        //            //model.TongSo = model.ThieuUy + model.TrungUy + model.ThuongUy + model.DaiUy + model.ThieuTa + model.TrungTa + model.ThuongTa + model.Vcqp + model.Cnqp +
        //            //    +model.DaiTa + model.Tuong + model.Qncn + model.Ldhd + model.BinhNhat + model.BinhNhi + model.HaSi + model.TrungSi + model.ThuongSi;

        //            model.ThieuUyCn = 0;
        //            model.TrungUyCn = 0;
        //            model.ThuongUyCn = 0;
        //            model.DaiUyCn = 0;
        //            model.ThieuTaCn = 0;
        //            model.TrungTaCn = 0;
        //            model.ThuongTaCn = 0;
        //        }
        //        tlQsChungTuChiTietModel.Add(model);
        //    }
        //    var listtlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104>>(tlQsChungTuChiTietModel);
        //    _tlQsChungTuChiTietService.Add(listtlQsChungTuChiTiet);
        //}

        public void SaveChungTuChiTiet(TlQsChungTuNq104 tlQsChungTu, bool choice)
        {
            //var listCanBo = _tlDmCanBoService.FindByCondition(x => x.Thang == tlQsChungTu.Thang && x.Parent == tlQsChungTu.MaDonVi && x.Nam == tlQsChungTu.Nam).ToList();
            var listCanBo = _mapper.Map<IEnumerable<CadresNq104Model>>(_tlDmCanBoService.FindCanBoQuyetToanQuanSo(tlQsChungTu.MaDonVi, tlQsChungTu.Thang, tlQsChungTu.Nam));
            var lstCanBoGiam290 = _mapper.Map<IEnumerable<CadresNq104Model>>(_tlDmCanBoService.FindCanBoQuyetToanQuanSoGiam(tlQsChungTu.MaDonVi, tlQsChungTu.Thang, tlQsChungTu.Nam));

            Dictionary<string, CadresNq104Model> dicCanBoCu = new Dictionary<string, CadresNq104Model>();
            int iThangTruoc = 0;
            int iNamTruoc = 0;
            if (tlQsChungTu.Thang == 0)
            {
                iThangTruoc = 12;
                iNamTruoc = tlQsChungTu.Nam - 1;
            }
            else
            {
                iThangTruoc = tlQsChungTu.Thang - 1;
                iNamTruoc = tlQsChungTu.Nam;
            }
            //var lstCanBoThangTruoc = _tlDmCanBoService.FindByCondition(x => x.Thang == iThangTruoc && x.Parent == tlQsChungTu.MaDonVi && x.Nam == iNamTruoc).ToList();
            var lstCanBoThangTruoc = _mapper.Map<IEnumerable<CadresNq104Model>>(_tlDmCanBoService.FindCanBoQuyetToanQuanSo(tlQsChungTu.MaDonVi, iThangTruoc, iNamTruoc));
            if (lstCanBoThangTruoc != null)
            {
                foreach (var item in lstCanBoThangTruoc)
                {
                    if (!dicCanBoCu.ContainsKey(item.MaHieuCanBo))
                        dicCanBoCu.Add(item.MaHieuCanBo, item);
                }
            }

            var listCanBoModel = _mapper.Map<List<CadresNq104Model>>(listCanBo);
            var listQsMucLuc = _nsQsMucLucService.FindAll(x => x.SHienThi != "2" && x.SM != "0" && x.INamLamViec == tlQsChungTu.Nam && x.ITrangThai == ItrangThaiStatus.ON).ToList();
            var lisTQsMucLucModel = _mapper.Map<ObservableCollection<QsMucLucModel>>(listQsMucLuc);

            var predicate = PredicateBuilder.True<TlCanBoPhuCapNq104>();
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

            List<TlQsChungTuChiTietNq104Model> tlQsChungTuChiTietModel = new List<TlQsChungTuChiTietNq104Model>();
            var listQsMucLucParent = lisTQsMucLucModel.Where(x => x.IIdMlnsCha == null && x.SKyHieu != MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC && x.SKyHieu != MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY).OrderBy(x => x.SKyHieu).ToList();
            var listQsMucLucChid = lisTQsMucLucModel.Where(x => x.IIdMlnsCha != null || x.SKyHieu == MA_TANG_GIAM.QUAN_SO_BO_SUNG_THANG_TRUOC || x.SKyHieu == MA_TANG_GIAM.QUAN_SO_CHUA_QT_THANG_NAY);

            List<TlQsChungTuChiTietNq104Model> listChungTuChiTietChid = new List<TlQsChungTuChiTietNq104Model>();
            List<TlQsChungTuChiTietNq104Model> listChungTuChiTietParent = new List<TlQsChungTuChiTietNq104Model>();
            foreach (var item in listQsMucLucChid)
            {
                TlQsChungTuChiTietNq104Model model = new TlQsChungTuChiTietNq104Model();
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
                    model.ThieuUy = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.THIEU_UY && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.HCY_BAC1 && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungUy = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.TRUNG_UY && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.HCY_BAC2 && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongUy = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiUy = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiTa = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.Tuong = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhat = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.BINH_NHAT && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.BINH_NHI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.HaSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.HA_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.TRUNG_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongSi = listCanBo.Count(x => (x.MaCb == MA_CAP_BAC.THUONG_SI && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));

                    model.ThieuUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.DaiUyCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThieuTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongTaCn = listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && !string.IsNullOrEmpty(x.MaCbCu) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
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
                    model.ThieuUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.THIEU_UY && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.HCY_BAC1 && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.TRUNG_UY && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.HCY_BAC2 && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                    ) + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC3) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiUy = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC4) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC5) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC6) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC7) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.DaiTa = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC8) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.Tuong = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TUONG) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))))
                        + listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.HCY_BAC9) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhat = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.BINH_NHAT && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.BinhNhi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.BINH_NHI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.HaSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.HA_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.TrungSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.TRUNG_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThuongSi = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu == MA_CAP_BAC.THUONG_SI && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1))));
                    model.ThieuUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                       listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                         listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.DaiUyCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThieuTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.TrungTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                        listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
                    model.ThuongTaCn = listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong))
                        +
                         listCanBo.Count(x => (!string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && x.MaCb.Substring(0, 1).Equals(x.MaCbCu.Substring(0, 1)))
                        || (x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && dicCanBoCu.ContainsKey(x.MaHieuCanBo) && x.MaCb == dicCanBoCu[x.MaHieuCanBo].MaCb && x.HeSoLuong != dicCanBoCu[x.MaHieuCanBo].HeSoLuong));
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
                    model.ThieuUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1 && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungUy = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2 && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiUy = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiTa = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.Tuong = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.BinhNhat = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHAT && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.BinhNhi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.BINH_NHI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.HaSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.HA_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongSi = listCanBo.Count(x => x.MaCb == MA_CAP_BAC.THUONG_SI && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                         listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)))
                        +
                        listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && !string.IsNullOrEmpty(x.MaCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));

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
                    model.ThieuUy = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_UY) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC1);
                    model.TrungUy = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_UY) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC2);
                    model.ThuongUy = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_UY) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC3);
                    model.DaiUy = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_UY) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC4);
                    model.ThieuTa = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_TA) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC5);
                    model.TrungTa = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_TA) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC6);
                    model.ThuongTa = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_TA) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC7);
                    model.DaiTa = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_TA) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC8);
                    model.Tuong = this.Count380(listCanBoModel, MA_CAP_BAC.TUONG) + this.Count380(listCanBoModel, MA_CAP_BAC.HCY_BAC9);
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

                    model.ThieuUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_UY_CN) + this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_THIEUUY);
                    model.TrungUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_UY_CN) + this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_TRUNGUY);
                    model.ThuongUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_UY_CN) + this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_THUONGUY);
                    model.DaiUyCn = this.Count380(listCanBoModel, MA_CAP_BAC.DAI_UY_CN)+ this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_DAIUY);
                    model.ThieuTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.THIEU_TA_CN)+ this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_THIEUTA);
                    model.TrungTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.TRUNG_TA_CN)+ this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_TRUNGTA);
                    model.ThuongTaCn = this.Count380(listCanBoModel, MA_CAP_BAC.THUONG_TA_CN)+ this.Count380(listCanBoModel, MA_CAP_BAC.CMKYCY_THUONGTA);
                }
                else if (item.SKyHieu == MA_TANG_GIAM.GIAM_NOI_BO)
                {
                    model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1 && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2 && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9) && x.MaTangGiam == item.SKyHieu.ToString());
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

                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && x.MaTangGiam == item.SKyHieu.ToString());
                    if (lstCanBoGiam290 != null && lstCanBoGiam290.Any())
                    {
                        model.ThieuUy += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY) + lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1);
                        model.TrungUy += lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY) + lstCanBoGiam290.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2);
                        model.ThuongUy += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3));
                        model.DaiUy += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4));
                        model.ThieuTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5));
                        model.TrungTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6));
                        model.ThuongTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7));
                        model.DaiTa += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8));
                        model.Tuong += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9));
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


                        model.ThieuUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN))+ lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY));
                        model.TrungUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN))+ lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY));
                        model.ThuongUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY));
                        model.DaiUyCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY));
                        model.ThieuTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA));
                        model.TrungTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA));
                        model.ThuongTaCn += lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN)) + lstCanBoGiam290.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA));
                    }
                }
                else
                {
                    model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1 && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2 && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9) && x.MaTangGiam == item.SKyHieu.ToString());
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

                    model.ThieuUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString()) 
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.DaiUyCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThieuTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.TrungTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA) && x.MaTangGiam == item.SKyHieu.ToString());
                    model.ThuongTaCn = listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN) && x.MaTangGiam == item.SKyHieu.ToString())
                        + listCanBo.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA) && x.MaTangGiam == item.SKyHieu.ToString());
                }
                listChungTuChiTietChid.Add(model);
            }

            TlQsChungTuChiTietNq104Model qsTang = new TlQsChungTuChiTietNq104Model();
            TlQsChungTuChiTietNq104Model qsGiam = new TlQsChungTuChiTietNq104Model();
            TlQsChungTuChiTietNq104Model tmp = new TlQsChungTuChiTietNq104Model();
            foreach (var item in listQsMucLucParent)
            {
                TlQsChungTuChiTietNq104Model model = new TlQsChungTuChiTietNq104Model();
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
                    List<TlQsChungTuChiTietNq104Model> listChungTuTangQs;
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
                    model.ThieuUy = listQsMucThangTruoc?.ThieuUy ?? 0 + qsTang.ThieuUy - qsGiam.ThieuUy;
                    model.TrungUy = listQsMucThangTruoc?.TrungUy ?? 0 + qsTang.TrungUy - qsGiam.TrungUy;
                    model.ThuongUy = listQsMucThangTruoc?.ThuongUy ?? 0 + qsTang.ThuongUy - qsGiam.ThuongUy;
                    model.DaiUy = listQsMucThangTruoc?.DaiUy ?? 0 + qsTang.DaiUy - qsGiam.DaiUy;
                    model.ThieuTa = listQsMucThangTruoc?.ThieuTa ?? 0 + qsTang.ThieuTa - qsGiam.ThieuTa;
                    model.TrungTa = listQsMucThangTruoc?.TrungTa ?? 0 + qsTang.TrungTa - qsGiam.TrungTa;
                    model.ThuongTa = listQsMucThangTruoc?.ThuongTa ?? 0 + qsTang.ThuongTa - qsGiam.ThuongTa;
                    model.DaiTa = listQsMucThangTruoc?.DaiTa ?? 0 + qsTang.DaiTa - qsGiam.DaiTa;
                    model.Tuong = listQsMucThangTruoc?.Tuong ?? 0 + qsTang.Tuong - qsGiam.Tuong;
                    model.Qncn = listQsMucThangTruoc?.Qncn ?? 0 + qsTang.Qncn - qsGiam.Qncn;
                    model.BinhNhat = listQsMucThangTruoc?.BinhNhat ?? 0 + qsTang.BinhNhat - qsGiam.BinhNhat;
                    model.BinhNhi = listQsMucThangTruoc?.BinhNhi ?? 0 + qsTang.BinhNhi - qsGiam.BinhNhi;
                    model.HaSi = listQsMucThangTruoc?.HaSi ?? 0 + qsTang.HaSi - qsGiam.HaSi;
                    model.TrungSi = listQsMucThangTruoc?.TrungSi ?? 0 + qsTang.TrungSi - qsGiam.TrungSi;
                    model.ThuongSi = listQsMucThangTruoc?.ThuongSi ?? 0 + qsTang.ThuongSi - qsGiam.ThuongSi;
                    model.Ldhd = listQsMucThangTruoc?.Ldhd ?? 0 + qsTang.Ldhd - qsGiam.Ldhd;
                    model.Vcqp = listQsMucThangTruoc?.Vcqp ?? 0 + qsTang.Vcqp - qsGiam.Vcqp;
                    model.Cnqp = listQsMucThangTruoc?.Cnqp ?? 0 + qsTang.Cnqp - qsGiam.Cnqp;
                    model.Ccqp = listQsMucThangTruoc?.Ccqp ?? 0 + qsTang.Ccqp - qsGiam.Ccqp;
                    //model.TongSo = qsTang.TongSo - qsGiam.TongSo;
                    model.ThieuUyCn = listQsMucThangTruoc?.ThieuUyCn ?? 0 + qsTang.ThieuUyCn - qsGiam.ThieuUyCn;
                    model.TrungUyCn = listQsMucThangTruoc?.TrungUyCn ?? 0 + qsTang.TrungUyCn - qsGiam.TrungUyCn;
                    model.ThuongUyCn = listQsMucThangTruoc?.ThuongUyCn ?? 0 + qsTang.ThuongUyCn - qsGiam.ThuongUyCn;
                    model.DaiUyCn = listQsMucThangTruoc?.DaiUyCn ?? 0 + qsTang.DaiUyCn - qsGiam.DaiUyCn;
                    model.ThieuTaCn = listQsMucThangTruoc?.ThieuTaCn ?? 0 + qsTang.ThieuTaCn - qsGiam.ThieuTaCn;
                    model.TrungTaCn = listQsMucThangTruoc?.TrungTaCn ?? 0 + qsTang.TrungTaCn - qsGiam.TrungTaCn;
                    model.ThuongTaCn = listQsMucThangTruoc?.ThuongTaCn ?? 0 + qsTang.ThuongTaCn - qsGiam.ThuongTaCn;
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
                    var predicateCheck = PredicateBuilder.True<TlQsChungTuChiTietNq104>();
                    DateTime date = new DateTime(tlQsChungTu.Nam - 1, 12, 1);
                    predicateCheck = predicateCheck.And(x => x.Thang == date.Month);
                    predicateCheck = predicateCheck.And(x => x.NamLamViec == date.Year);

                    predicateCheck = predicateCheck.And(x => x.XauNoiMa == MA_TANG_GIAM.QUAN_SO_QT_THANG_NAY);
                    predicateCheck = predicateCheck.And(x => x.IdDonVi.Equals(tlQsChungTu.MaDonVi));
                    var dataCheck = _tlQsChungTuChiTietService.FindAll(predicateCheck).FirstOrDefault();
                    if (dataCheck == null)
                    {
                        model.ThieuUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY) + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC1);
                        model.TrungUy = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY) + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.HCY_BAC2);
                        model.ThuongUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC3));
                        model.DaiUy = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC4));
                        model.ThieuTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC5));
                        model.TrungTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC6));
                        model.ThuongTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC7));
                        model.DaiTa = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_TA)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC8));
                        model.Tuong = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TUONG)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.HCY_BAC9));
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

                        model.ThieuUyCn = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.THIEU_UY_CN) + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.CMKYCY_THIEUUY);
                        model.TrungUyCn = listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.TRUNG_UY_CN) + listCanBoModel.Count(x => x.MaCb == MA_CAP_BAC.CMKYCY_TRUNGUY);
                        model.ThuongUyCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_UY_CN)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGUY));
                        model.DaiUyCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.DAI_UY_CN)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_DAIUY));
                        model.ThieuTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THIEU_TA_CN)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THIEUTA));
                        model.TrungTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.TRUNG_TA_CN)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_TRUNGTA));
                        model.ThuongTaCn = listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.THUONG_TA_CN)) + listCanBoModel.Count(x => x.MaCb.StartsWith(MA_CAP_BAC.CMKYCY_THUONGTA));
                        tmp = model;
                    }
                    else
                    {
                        model = _mapper.Map<TlQsChungTuChiTietNq104Model>(listQsMucThangTruoc).Clone();
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

            var listtlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTietNq104>>(tlQsChungTuChiTietModel);
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

        private int Count380(List<CadresNq104Model> listCanBo, string maCbCu)
        {
            return listCanBo.Count(x => !string.IsNullOrEmpty(x.MaCbCu) && x.MaCbCu.StartsWith(maCbCu) && !x.MaCbCu.Substring(0, 1).Equals(x.MaCb.Substring(0, 1)));
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
