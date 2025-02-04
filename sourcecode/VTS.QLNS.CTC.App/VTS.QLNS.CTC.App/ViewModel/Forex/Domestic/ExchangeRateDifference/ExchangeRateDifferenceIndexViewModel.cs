using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.Domestic.ExchangeRateDifference;
using VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.ExchangeRateDifference.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Domestic.ExchangeRateDifference
{
    public class ExchangeRateDifferenceIndexViewModel : GridViewModelBase<NhDaChenhLechTiGiaModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhKhTongTheNhiemVuChiService _nvcService;
        private readonly INhDaChenhLechTiGiaService _service;
        private ExchangeRateDifferenceCriteria _conditionSearch;

        public override string GroupName => "BÁO CÁO";
        public override string Name => "Chênh lệch tỉ giá hối đoái";
        public override string Title => "Chênh lệch tỉ giá hối đoái";
        public override string Description => "Danh sách chênh lệch tỉ giá hối đoái";
        public override Type ContentType => typeof(ExchangeRateDifferenceIndex);
        public override PackIconKind IconKind => PackIconKind.StackExchange;

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadChuongTrinh();
                _selectedChuongTrinh = null;
                LoadHopDong();
            }
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiQuery> _itemsChuongTrinh;
        public ObservableCollection<NhKhTongTheNhiemVuChiQuery> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhKhTongTheNhiemVuChiQuery _selectedChuongTrinh;
        public NhKhTongTheNhiemVuChiQuery SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set
            {
                SetProperty(ref _selectedChuongTrinh, value);
                LoadHopDong();
            }
        }

        private ObservableCollection<NhDaHopDong> _itemsContract;
        public ObservableCollection<NhDaHopDong> ItemsContract
        {
            get => _itemsContract;
            set => SetProperty(ref _itemsContract, value);
        }

        private NhDaHopDong _selectedContract;
        public NhDaHopDong SelectedContract
        {
            get => _selectedContract;
            set => SetProperty(ref _selectedContract, value);
        }

        public bool IsAllowPrint
        {
            get
            {
                return Items != null && Items.Count() > 0;
            }
        }

        //private bool _isOpenPrintPopup;
        //public bool IsOpenPrintPopup
        //{
        //    get => _isOpenPrintPopup;
        //    set => SetProperty(ref _isOpenPrintPopup, value);
        //}

        public ExchangeRateDifferencePrintDialogViewModel ExchangeRateDifferencePrintDialogViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }
        //public RelayCommand BtnPrintCommand { get; }

        public ExchangeRateDifferenceIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaChenhLechTiGiaService service,
            INhDaHopDongService nhDaHopDongService,
            INhKhTongTheNhiemVuChiService nvcService,
            ExchangeRateDifferencePrintDialogViewModel exchangeRateDifferencePrintDialogViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhDaHopDongService = nhDaHopDongService;
            _nvcService = nvcService;
            _service = service;
            ExchangeRateDifferencePrintDialogViewModel = exchangeRateDifferencePrintDialogViewModel;

            SearchCommand = new RelayCommand(obj => LoadData());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            PrintCommand = new RelayCommand(obj => OnPrint());
            //BtnPrintCommand = new RelayCommand(obj => IsOpenPrintPopup = true);
        }

        public override void Init()
        {
            LoadDonVi();
            LoadChuongTrinh();
            LoadHopDong();
            LoadData();
        }

        private void LoadDonVi()
        {
            try
            {
                IEnumerable<DonVi> data = _nsDonViService.FindByYearAndNhiemVuChi(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
                OnPropertyChanged(nameof(ItemsDonVi));
                SelectedDonVi = null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadHopDong()
        {
            try
            {
                IEnumerable<NhDaHopDong> contractList;
                if (SelectedChuongTrinh != null)
                {
                    contractList = _nhDaHopDongService.FindByIdKHTongTheNhiemVuChi(SelectedChuongTrinh.Id);
                }
                else
                {
                    contractList = _nhDaHopDongService.FindByIdKHTongTheNhiemVuChi(null);
                }
                ItemsContract = _mapper.Map<ObservableCollection<NhDaHopDong>>(contractList);
                OnPropertyChanged(nameof(ItemsContract));
                SelectedContract = null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadChuongTrinh()
        {
            try
            {
                IEnumerable<NhKhTongTheNhiemVuChiQuery> nvcList;
                if (SelectedDonVi != null)
                {
                    nvcList = _nvcService.FindByIdDonVi(SelectedDonVi.Id);
                }
                else
                {
                    nvcList = _nvcService.FindByIdDonVi(null);
                }
                ItemsChuongTrinh = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiQuery>>(nvcList);
                OnPropertyChanged(nameof(ItemsChuongTrinh));
                SelectedChuongTrinh = null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnPrint()
        {
            ResetConditionSearch();
            ExchangeRateDifferencePrintDialogViewModel.Model = SelectedItem;
            ExchangeRateDifferencePrintDialogViewModel.conditionSearch = _conditionSearch;
            ExchangeRateDifferencePrintDialogViewModel.Init();
            ExchangeRateDifferencePrintDialogViewModel.ShowDialogHost();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                ResetConditionSearch();

                Items = new ObservableCollection<NhDaChenhLechTiGiaModel>();
                e.Result = _service.FindAllExchangeRateDifference(_conditionSearch);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhDaChenhLechTiGiaModel>>(e.Result);
                    if (Items != null && Items.Count() > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    OnPropertyChanged(nameof(IsAllowPrint));
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private void ResetConditionSearch()
        {
            Guid? iID_DonVi = null;
            Guid? iID_KHTongThe_Nvc_ID = null;
            Guid? iID_HopDongID = null;
            if (SelectedDonVi != null) iID_DonVi = SelectedDonVi.Id;
            if (SelectedChuongTrinh != null) iID_KHTongThe_Nvc_ID = SelectedChuongTrinh.Id;
            if (SelectedContract != null) iID_HopDongID = SelectedContract.Id;
            _conditionSearch = new ExchangeRateDifferenceCriteria()
            {
                IID_DonVi = iID_DonVi,
                IID_KHTongThe_Nvc_ID = iID_KHTongThe_Nvc_ID,
                IID_HopDongID = iID_HopDongID,
                INamKeHoach = _sessionService.Current.YearOfWork
            };
        }

        private void OnRemoveFilter()
        {
            SelectedDonVi = null;
            SelectedChuongTrinh = null;
            SelectedContract = null;
            LoadData();
        }

        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }
    }
}
