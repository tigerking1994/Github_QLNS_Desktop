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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Settlement.SettlementNumber
{
    public class SettlementSyntheticDialogViewModel : DialogViewModelBase<List<TlQsChungTuModel>>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlQsChungTuService _tlQsChungTu;
        private readonly ITlQsChungTuChiTietService _tlQsChungTuChiTietService;
        private readonly INsQsMucLucService _qsMucLucService;
        private ICollectionView _dataDonViView;

        public override string Title => "Tổng hợp chứng từ quyết toán";
        public override Type ContentType => typeof(View.Salary.Settlement.SalarySettlementNumber.SalarySettlementSyntheticDialog);

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

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private string _note;
        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        private ObservableCollection<DonViModel> _donViItems;
        public ObservableCollection<DonViModel> DonViItems
        {
            get => _donViItems;
            set => SetProperty(ref _donViItems, value);
        }

        private DonViModel _selectedDonViItems;
        public DonViModel SelectedDonViItems
        {
            get => _selectedDonViItems;
            set
            {
                if (SetProperty(ref _selectedDonViItems, value) && _dataDonViView != null)
                {
                    _dataDonViView.Refresh();
                }
            }
        }

        private DateTime _ngayTao;
        public DateTime NgayTao
        {
            get => _ngayTao;
            set => SetProperty(ref _ngayTao, value);
        }

        private string _searchDonVi;
        public string SearchDonVi
        {
            get => _searchDonVi;
            set
            {
                if (SetProperty(ref _searchDonVi, value) && _dataDonViView != null)
                {
                    _dataDonViView.Refresh();
                }    
            }
        }

        public bool IsReadOnly => ViewState == FormViewState.ADD;

        public SettlementSyntheticDialogViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            INsDonViService nsDonViService,
            ITlQsChungTuService tlQsChungTu,
            ITlQsChungTuChiTietService tlQsChungTuChiTietService,
            INsQsMucLucService qsMucLucService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _tlQsChungTu = tlQsChungTu;
            _tlQsChungTuChiTietService = tlQsChungTuChiTietService;
            _qsMucLucService = qsMucLucService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadText();
            LoadDonVi();
            LoadNgayTao();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model[0].Thang.ToString());
        }

        private void LoadText()
        {
            _note = "Chứng từ tổng hợp";
        }

        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork);
            predicate = predicate.And(x => x.Loai == "0" || x.Loai == "1");
            var listUnit = _nsDonViService.FindByCondition(predicate).OrderBy(x => x.Loai).ThenBy(x => x.TenDonVi).ToList();
            DonViItems = _mapper.Map<ObservableCollection<DonViModel>>(listUnit);
            SelectedDonViItems = DonViItems.FirstOrDefault();
            _dataDonViView = CollectionViewSource.GetDefaultView(DonViItems);
            _dataDonViView.Filter = DonViFilter;
        }

        private bool DonViFilter(object obj)
        {
            bool result = true;
            var item = (DonViModel)obj;
            if (SearchDonVi != null)
            {
                result &= item.IIDMaDonVi.ToLower().Contains(SearchDonVi.ToLower())
                    || item.TenDonVi.ToLower().Contains(SearchDonVi.ToLower());
            }
            return result;
        }

        private void LoadNgayTao()
        {
            DateTime myDate = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(myDate.Year, DateTime.Now.Month, DateTime.Now.Day);
            _ngayTao = firstDayOfMonth;
        }

        public override void OnSave()
        {
            string validate = GetValidate();
            if (!string.IsNullOrEmpty(validate))
            {
                System.Windows.Forms.MessageBox.Show(validate, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var data = _tlQsChungTu.FindAll().OrderByDescending(x => x.SoChungTu).LastOrDefault();
                TlQsChungTuModel tlQsChungTuModel = new TlQsChungTuModel();
                tlQsChungTuModel.Thang = int.Parse(MonthSelected.ValueItem);
                tlQsChungTuModel.Nam = Model[0].Nam;
                tlQsChungTuModel.MaDonVi = SelectedDonViItems.IIDMaDonVi;
                tlQsChungTuModel.TenDonVi = SelectedDonViItems.TenDonVi;
                tlQsChungTuModel.DateCreated = DateTime.Now;
                tlQsChungTuModel.UserCreated = _sessionService.Current.Principal;
                tlQsChungTuModel.IsLock = false;
                tlQsChungTuModel.MoTa = Note;
                tlQsChungTuModel.NgayTao = NgayTao;
                tlQsChungTuModel.SoChungTu = SinhMaChungTu();
                tlQsChungTuModel.STongHop = string.Join("','", Model.Select(x => x.Id.ToString()));
                tlQsChungTuModel.BDaTongHop = false;
                tlQsChungTuModel.BNganSachNhanDuLieu = false;
                tlQsChungTuModel.Id = Guid.NewGuid();

                List<TlQsChungTuChiTiet> chungTuChiTiet = new List<TlQsChungTuChiTiet>();
                List<TlQsChungTuChiTietModel> chungTuChiTietTongHop = new List<TlQsChungTuChiTietModel>();
                foreach (var item in Model)
                {
                    var predicate = PredicateBuilder.True<TlQsChungTuChiTiet>();
                    predicate = predicate.And(x => item.Id.ToString().Equals(x.IdChungTu));
                    var TlQsChungTu = _tlQsChungTuChiTietService.FindAll(predicate).ToList();
                    chungTuChiTiet.AddRange(TlQsChungTu);
                }
                var predicateMucLuc = PredicateBuilder.True<NsQsMucLuc>();
                predicateMucLuc = predicateMucLuc.And(x => !x.SHienThi.Equals("2"));
                predicateMucLuc = predicateMucLuc.And(x => x.INamLamViec == Model[0].Nam);
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
                    model.Ccqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Ccqp);
                    model.Vcqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Vcqp);
                    model.Ldhd = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Ldhd);
                    model.Cnqp = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.Cnqp); ;
                    //model.TongSo = ChungTuChiTietMucLuc.Where(x => mucLuc.SKyHieu.Equals(x.XauNoiMa)).Sum(x => x.TongSo);

                    chungTuChiTietTongHop.Add(model);
                }
                var tlQsChungTu = _mapper.Map<TlQsChungTu>(tlQsChungTuModel);
                var tlQsChungTuChiTiet = _mapper.Map<ObservableCollection<TlQsChungTuChiTiet>>(chungTuChiTietTongHop).ToList();
                _tlQsChungTu.Add(tlQsChungTu);
                _tlQsChungTuChiTietService.Add(tlQsChungTuChiTiet);
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<TlQsChungTuModel>(tlQsChungTu));
            }
        }

        private string SinhMaChungTu()
        {
            var soChungTuIndex = _tlQsChungTu.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
            return "QS-" + soChungTuIndex.ToString("D3");
        }

        private string GetValidate()
        {
            List<string> messages = new List<string>();
            if (SelectedDonViItems == null)
            {
                messages.Add(string.Format(Resources.UnitNull));
                goto End;
            }
        End:
            return string.Join(Environment.NewLine, messages);
        }
    }
}