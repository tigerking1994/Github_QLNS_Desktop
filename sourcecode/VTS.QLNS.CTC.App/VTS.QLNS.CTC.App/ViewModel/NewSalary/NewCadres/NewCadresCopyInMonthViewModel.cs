using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.NewSalary.NewCadres
{
    public class NewCadresCopyInMonthViewModel : DialogViewModelBase<CadresNq104Model>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmCanBoNq104Service _cadresService;
        private readonly ITlDmDonViNq104Service _tlDmDonViService;
        private readonly ITlCanBoPhuCapNq104Service _tlCanBoPhuCapService;
        private ICollectionView _dataCopyCarderView;
        private readonly ISysAuditLogService _sysAuditLogService;


        public override string FuncCode => NSFunctionCode.NEW_SALARY_CADRES_COPY_IN_MONTH;

        public override string Title => "Sao chép nhanh đối tượng";
        public override string Description => "Sao chép nhanh đối tượng hưởng lương, phụ cấp trong tháng";
        public override Type ContentType => typeof(View.NewSalary.NewCadres.NewCadresCopyInMonthDialog);

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
                if (SetProperty(ref _selectedDonViItems, value) && _dataCopyCarderView != null)
                {
                    LoadData();
                }
            }
        }

        private ObservableCollection<CadresNq104Model> _cadresItems;
        public ObservableCollection<CadresNq104Model> CadresItems
        {
            get => _cadresItems;
            set => SetProperty(ref _cadresItems, value);
        }

        private CadresNq104Model _selectedCarderItems;
        public CadresNq104Model SelectedCarderItems
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
                foreach (var item in CadresItems)
                    item.IsSelected = _selectedAllCadres;
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

        public NewCadresCopyInMonthViewModel(
          ISessionService sessionService,
          IMapper mapper,
          ILog logger,
          ITlDmCanBoNq104Service cadresService,
          ITlDmDonViNq104Service tlDmDonViService,
          ITlCanBoPhuCapNq104Service tlCanBoPhuCapService,
          ISysAuditLogService sysAuditLogService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _cadresService = cadresService;
            _tlDmDonViService = tlDmDonViService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _sysAuditLogService = sysAuditLogService;
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
            DonViItems = _mapper.Map<ObservableCollection<TlDmDonViNq104Model>>(data);
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
                var predicate = PredicateBuilder.True<TlDmCanBoNq104>();
                predicate = predicate.And(x => x.Thang == int.Parse(MonthSelected.ValueItem));
                predicate = predicate.And(x => x.Nam == int.Parse(YearSelected.ValueItem));
                predicate = predicate.And(x => x.IsDelete == true);
                predicate = predicate.And(x => x.ITrangThai != 3);
                if (SelectedDonViItems != null)
                {
                    predicate = predicate.And(x => x.Parent == SelectedDonViItems.MaDonVi);
                }
                var data = _cadresService.FindByCondition(predicate).OrderBy(x => x.TenCanBo.Split(" ").Last());
                CadresItems = _mapper.Map<ObservableCollection<CadresNq104Model>>(data);
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
            var item = (CadresNq104Model)obj;
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

        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<CadresNq104Model> listCadres = new List<CadresNq104Model>();
                List<TlCanBoPhuCapNq104Model> listCanBophuCapModelSave = new List<TlCanBoPhuCapNq104Model>();
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
                            var listCanBoPhuCapModel = _mapper.Map<ObservableCollection<TlCanBoPhuCapNq104Model>>(listCanBoPhuCap).ToList();
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
                IEnumerable<TlDmCanBoNq104> listCadresSave = _mapper.Map<ObservableCollection<TlDmCanBoNq104>>(listCadres);
                IEnumerable<TlCanBoPhuCapNq104> listCanBoPhuCapSave = _mapper.Map<ObservableCollection<TlCanBoPhuCapNq104>>(listCanBophuCapModelSave);
                _cadresService.BulkInsert(listCadresSave);
                _tlCanBoPhuCapService.BulkInsert(listCanBoPhuCapSave);
                _sysAuditLogService.WriteLog(Resources.ApplicationName, Description, (int)TypeExecute.Insert, DateTime.Now, TransactionStatus.Success, _sessionService.Current.Principal);

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
