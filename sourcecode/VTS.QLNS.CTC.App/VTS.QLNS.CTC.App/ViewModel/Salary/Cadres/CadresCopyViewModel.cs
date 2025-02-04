using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresCopyViewModel : DialogViewModelBase<CadresModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private ICollectionView _dataCopyCarderView;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_COPY;
        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng theo tháng";
        public override Type ContentType => typeof(View.Salary.Cadres.CadresCopy);

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
            set
            {
                if (SetProperty(ref _selectedDonVi, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private List<ComboboxItem> _months;
        public List<ComboboxItem> ItemsMonth
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _selectedFromMonth;
        public ComboboxItem SelectedFromMonth
        {
            get => _selectedFromMonth;
            set
            {
                if (SetProperty(ref _selectedFromMonth, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _selectedToMonth;
        public ComboboxItem SelectedToMonth
        {
            get => _selectedToMonth;
            set => SetProperty(ref _selectedToMonth, value);
        }

       

        private List<ComboboxItem> _years;
        public List<ComboboxItem> ItemsYear
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _selectedFromYear;
        public ComboboxItem SelectedFromYear
        {
            get => _selectedFromYear;
            set
            {
                if (SetProperty(ref _selectedFromYear, value) && _dataCopyCarderView != null && _selectedFromYear != null)
                {
                    LoadData();
                }
            }
        }

        private ComboboxItem _selectedToYear;
        public ComboboxItem SelectedToYear
        {
            get => _selectedToYear;
            set => SetProperty(ref _selectedToYear, value);
        }

        private ObservableCollection<CadresModel> _itemsCadres;
        public ObservableCollection<CadresModel> ItemsCadres
        {
            get => _itemsCadres;
            set => SetProperty(ref _itemsCadres, value);
        }

        private CadresModel _selectedCarder;
        public CadresModel SelectedCarder
        {
            get => _selectedCarder;
            set => SetProperty(ref _selectedCarder, value);
        }

        private bool _selectedAllCadres;
        public bool SelectedAllCadres
        {
            get => ItemsCadres.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectedAllCadres, value);
                foreach (var item in ItemsCadres) item.IsSelected = _selectedAllCadres;
            }
        }

        public string LabelSelectedCountCadres
        {
            get
            {
                var totalCount = ItemsCadres.Count;
                var totalSelected = ItemsCadres.Count(x => x.IsSelected);
                return $"ĐỐI TƯỢNG ({totalSelected}/{totalCount})";
            }
        }

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

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonVi.TenDonVi);
        public bool IsReadOnly => ViewState == FormViewState.ADD;

        public CadresCopyViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            ITlDmCanBoService cadresService,
            ITlDmDonViService tlDmDonViService,
            ITlCanBoPhuCapService tlCanBoPhuCapService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
            LoadDonVi();
            LoadData();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(ItemsMonth));
            SelectedFromMonth = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
        }

        private void LoadDonVi()
        {
            TlDmDonViModel donViDefault = new TlDmDonViModel();
            donViDefault.Id = Guid.Empty;
            donViDefault.TenDonVi = "-- Tất cả --";

            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            _itemsDonVi = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            _itemsDonVi.Insert(0, donViDefault);
            SelectedDonVi = donViDefault;
            OnPropertyChanged(nameof(ItemsDonVi));   
        }

        private void LoadData()
        {
            try
            {
                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Nam == int.Parse(_selectedFromYear.ValueItem));
                predicate = predicate.And(x => x.Thang == int.Parse(_selectedFromMonth.ValueItem));
                //predicate = predicate.And(x => x.IsDelete == true);
                if (!SelectedDonVi.Id.Equals(Guid.Empty))
                {
                    predicate = predicate.And(x => x.Parent == SelectedDonVi.MaDonVi);
                }
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                ItemsCadres = _mapper.Map<ObservableCollection<CadresModel>>(data);
                _dataCopyCarderView = CollectionViewSource.GetDefaultView(ItemsCadres);
                _dataCopyCarderView.Filter = CadresFilter;
                SelectedAllCadres = true;
                foreach (var org in ItemsCadres)
                {
                    org.PropertyChanged += (sender, args) =>
                    {
                        OnPropertyChanged(nameof(LabelSelectedCountCadres));
                        OnPropertyChanged(nameof(SelectedAllCadres));
                    };
                }
                OnPropertyChanged(nameof(LabelSelectedCountCadres));
                OnPropertyChanged(nameof(SelectedAllCadres));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CadresFilter(object obj)
        {
            bool result = true;
            var item = (CadresModel)obj;
            if (SelectedFromMonth != null)
            {
                result = result && item.Thang == int.Parse(SelectedFromMonth.ValueItem);
            }
            if (SelectedFromYear != null)
            {
                result = result && item.Nam == int.Parse(SelectedFromYear.ValueItem);
            }
            if (!SelectedDonVi.Id.Equals(Guid.Empty))
            {
                result = result && item.Parent == SelectedDonVi.MaDonVi;
            }
            return result;
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                var year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            OnPropertyChanged(nameof(ItemsYear));
            SelectedFromYear = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
            SelectedToYear = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        public override void OnSave()
        {
            if (!Validate()) return;

            MessageBoxResult dialog = MessageBoxHelper.Confirm("Đ/c có muốn chuyển các phụ cấp nhập bằng tiền sang tháng sau không?");
            bool isCopyValue = dialog == MessageBoxResult.Yes;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                int toYear = int.Parse(SelectedToYear.ValueItem);
                int toMonth = int.Parse(SelectedToMonth.ValueItem);
                int fromYear = int.Parse(SelectedFromYear.ValueItem);
                int fromMonth = int.Parse(SelectedFromMonth.ValueItem);

                // Sao chép cán bộ
                var lstCanBoEntities = new List<TlDmCanBo>();
                var lstCanBoSelect = ItemsCadres.Where(x => x.IsSelected).ToList();

                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Nam == toYear);
                predicate = predicate.And(x => x.Thang == toMonth);
                var lstCanBoExits = _cadresService.FindByCondition(predicate);
                var lstCanBo = lstCanBoSelect.Where(x => !lstCanBoExits.Any(y => x.MaHieuCanBo.Equals(y.MaHieuCanBo)));

                foreach (var item in lstCanBo)
                {
                    var copyItem = item.Clone();
                    copyItem.Id = Guid.NewGuid();
                    copyItem.MaCanBo = toYear.ToString("D4") + toMonth.ToString("D2") + item.MaHieuCanBo;
                    copyItem.MaCbCu = string.Empty;
                    copyItem.Thang = toMonth;
                    copyItem.Nam = toYear;
                    copyItem.MaTangGiam = null;
                    copyItem.NgayTruyLinh = null;
                    copyItem.IsLock = false;
                    copyItem.ITrangThai = 0;
                    copyItem.BTinhBHXH = false;
                    copyItem.NamTn = DateUtils.TinhNamThamNien(copyItem.NgayNn, copyItem.NgayXn, copyItem.NgayTn, (int)(copyItem.ThangTnn ?? 0), toMonth, toYear);
                    lstCanBoEntities.Add(_mapper.Map<TlDmCanBo>(copyItem));
                }

                // Sao chép cán bộ phụ cấp
                string maCanBo = string.Join(",", lstCanBo.Select(x => x.MaCanBo));
                var lstCanBoPhuCap = _tlCanBoPhuCapService.Copy(maCanBo, fromYear, fromMonth, toYear, toMonth, isCopyValue);
                var lstCanBoPhuCapEntities = _mapper.Map<List<TlCanBoPhuCap>>(lstCanBoPhuCap);

                _cadresService.Copy(lstCanBoEntities, lstCanBoPhuCapEntities);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Thành công
                    MessageBoxHelper.Info("Sao chép thành công");
                    SavedAction?.Invoke(null);
                    DialogHost.Close("RootDialog");
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });

        }

        private bool Validate()
        {
            if (!ItemsCadres.Any(x => x.IsSelected))
            {
                MessageBoxHelper.Warning(Resources.MsgNullCadres);
                return false;
            }
            if (SelectedFromYear != null && SelectedToMonth != null && SelectedFromYear != null && SelectedFromMonth != null)
            {
                int toYear = int.Parse(SelectedToYear.ValueItem);
                int toMonth = int.Parse(SelectedToMonth.ValueItem);
                int fromYear = int.Parse(SelectedFromYear.ValueItem);
                int fromMonth = int.Parse(SelectedFromMonth.ValueItem);

                //if (fromYear > toYear || (fromYear == toYear && fromMonth >= toMonth))
                //{
                //    MessageBoxHelper.Warning(Resources.MsgCopyCadres);
                //    return false;
                //}
                return true;
            }
            else
            {
                MessageBoxHelper.Warning(Resources.MsgCopyNull);
                return false;
            }
        }
    }
}
