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
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresCopyInMonthViewModel : DialogViewModelBase<CadresModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private ICollectionView _dataCopyCarderView;

        public override string FuncCode => NSFunctionCode.SALARY_CADRES_COPY_IN_MONTH;

        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng hưởng lương, phụ cấp trong tháng";
        public override Type ContentType => typeof(View.Salary.Cadres.CadresCopyInMonthDialog);

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
            set
            {
                if (SetProperty(ref _monthSelected, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set
            {
                if (SetProperty(ref _yearSelected, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
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
                if (SetProperty(ref _selectedDonViItems, value) && _dataCopyCarderView != null)
                {
                    LoadDataAnDonVi();
                }
            }
        }

        private ObservableCollection<CadresModel> _cadresItems;
        public ObservableCollection<CadresModel> CadresItems
        {
            get => _cadresItems;
            set => SetProperty(ref _cadresItems, value);
        }

        private CadresModel _selectedCarderItems;
        public CadresModel SelectedCarderItems
        {
            get => _selectedCarderItems;
            set => SetProperty(ref _selectedCarderItems, value);
        }

        private bool _selectedAllCadres;
        public bool SelectedAllCadres
        {
            get => CadresItems.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectedAllCadres, value);
                foreach (var item in CadresItems) item.IsSelected = _selectedAllCadres;
            }
        }

        public string LabelSelectedCountCadres
        {
            get
            {
                var totalCount = CadresItems.Count;
                var totalSelected = CadresItems.Count(x => x.IsSelected);
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

        public string ComboboxDisplayMemberPathDonVi => nameof(SelectedDonViItems.TenDonVi);
        public bool IsReadOnly => ViewState == FormViewState.ADD;

        public CadresCopyInMonthViewModel(
          ISessionService sessionService,
          IMapper mapper,
          ILog logger,
          ITlDmCanBoService cadresService,
          ITlDmDonViService tlDmDonViService,
          ITlCanBoPhuCapService tlCanBoPhuCapService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
        }

        public override void Init()
        {
            base.Init();
            LoadMonth();
            LoadDonVi();
            LoadYear();
            LoadData();
        }

        private void LoadMonth()
        {
            _months = new List<ComboboxItem>();
            for (var i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem(i.ToString(), i.ToString());
                _months.Add(month);
            }
            OnPropertyChanged(nameof(Months));
            var thang = _sessionService.Current.Month;
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == Model.Thang.ToString());
        }

        private void LoadDonVi()
        {
            var data = _tlDmDonViService.FindByCondition(x => x.ITrangThai.HasValue && (bool)x.ITrangThai);
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViModel>>(data);
            if (DonViItems != null)
            {
                SelectedDonViItems = _donViItems.FirstOrDefault(x => x.MaDonVi == Model.Parent);
            }
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (var i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem(i.ToString(), i.ToString());
                _years.Add(year);
            }
            // var thang = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == Model.Nam.ToString());
        }

        private void LoadData()
        {
            try
            {
                var predicate = PredicateBuilder.True<TlDmCanBo>();
                predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
                predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                predicate = predicate.And(x => x.IsDelete == true);
                predicate = predicate.And(x => x.ITrangThai != 3);
                if (SelectedDonViItems != null)
                {
                //    predicate = predicate.And(x => x.Parent == DonViItems.FirstOrDefault().MaDonVi);
                //}
                //else
                //{
                    predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);
                }
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                CadresItems = _mapper.Map<ObservableCollection<CadresModel>>(data);
                _dataCopyCarderView = CollectionViewSource.GetDefaultView(CadresItems);
                _dataCopyCarderView.Filter = CadresFilter;
                foreach (var org in CadresItems)
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
            if (SelectedDonViItems != null)
            {
                result = result && item.Parent.Equals(SelectedDonViItems.MaDonVi);
            }
            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (MonthSelected != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            return result;
        }

        private void LoadDataAnDonVi()
        {
            LoadData();
        }

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<CadresModel> listCadres = new List<CadresModel>();
                List<TlCanBoPhuCapModel> listCanBophuCapModelSave = new List<TlCanBoPhuCapModel>();
                var data = _cadresService.FindAllInCludeDelete();
                int max = data.Max(x => int.Parse(x.MaHieuCanBo));
                if (data != null && data.Count() > 0)
                {
                    int t = 1;
                    foreach (var item in CadresItems)
                    {
                        if (item.IsSelected)
                        {
                            var listCanBoPhuCap = _tlCanBoPhuCapService.FindByMaCanBo(item.MaCanBo);
                            var listCanBoPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapModel>>(listCanBoPhuCap).ToList();
                            string maHieu = (max + t).ToString();
                            string maCanBo = YearSelected.ValueItem.ToString() + int.Parse(MonthSelected.ValueItem).ToString("D2") + maHieu;
                            item.Id = Guid.NewGuid();
                            item.MaHieuCanBo = maHieu;
                            item.MaCanBo = maCanBo;
                            item.SoSoLuong = maHieu.PadLeft(7, '0');
                            item.MaCbCu = string.Empty;
                            item.BTinhBHXH = false;
                            //item.ITrangThai = 1;
                            listCadres.Add(item);
                            listCanBoPhuCapModel.Select(x =>
                            {
                                x.MaCbo = maCanBo;
                                x.Id = Guid.NewGuid();
                                return x;
                            }).ToList();
                            listCanBophuCapModelSave.AddRange(listCanBoPhuCapModel);
                            t++;
                        }
                    }
                }
                IEnumerable<TlDmCanBo> listCadresSave = _mapper.Map<ObservableCollection<TlDmCanBo>>(listCadres);
                IEnumerable<TlCanBoPhuCap> listCanBoPhuCapSave = _mapper.Map<ObservableCollection<TlCanBoPhuCap>>(listCanBophuCapModelSave);
                _cadresService.BulkInsert(listCadresSave);
                _tlCanBoPhuCapService.BulkInsert(listCanBoPhuCapSave);
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
    }
}
