using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyDuAn.ForexDomesticBiddingPackage;
using VTS.QLNS.CTC.Core.Service;
using static VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.AllocationDetailViewModel;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.ForexDomesticBiddingPackagel
{
    public class ForexDomesticBiddingPackageDetailViewModel : DetailViewModelBase<NhDaGoiThauModel, NhDaGoiThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu";
        public override Type ContentType => typeof(ForexDomesticBiddingPackageDetail);

        public RelayCommand ShowHangMucDetailCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public event DataChangedEventHandler ClosePopup;

        private ObservableCollection<NhDaGoiThauNguonVonModel> _itemsNguonVon;
        public ObservableCollection<NhDaGoiThauNguonVonModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _selectedNguonVon;
        public NhDaGoiThauNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhi;
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private NhDaGoiThauChiPhiModel _selectedChiPhi;
        public NhDaGoiThauChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<NhDaGoiThauHangMucModel> _itemsHangMuc;
        public ObservableCollection<NhDaGoiThauHangMucModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
        }

        private NhDaGoiThauHangMucModel _selectedHangMuc;
        public NhDaGoiThauHangMucModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set => SetProperty(ref _selectedHangMuc, value);
        }

        private bool _isOpenNguonVonPopup;
        public bool IsOpenNguonVonPopup
        {
            get => _isOpenNguonVonPopup;
            set
            {
                SetProperty(ref _isOpenNguonVonPopup, value);
            }
        }

        public ForexDomesticBiddingPackageDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INhDaGoiThauService nhDaGoiThauService,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;

            ShowHangMucDetailCommand = new RelayCommand(obj => LoadGoiThauHangMuc());
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public override void Init()
        {
            base.Init();
            LoadData();
            LoadNguonVon();
            LoadChiPhi();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
        }

        private void LoadNguonVon()
        {
            var data = _nhDaGoiThauNguonVonService.FindByIdGoiThau(Model.Id).OrderBy(x => x.STenNguonVon);
            _itemsNguonVon = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVonModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void LoadChiPhi()
        {
            var data = _nhDaGoiThauChiPhiService.FindByGoiThauId(Model.Id);
            _itemsChiPhi = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(data);
            SelectedChiPhi = _itemsChiPhi.FirstOrDefault();
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void LoadGoiThauHangMuc()
        {
            var data = _nhDaGoiThauHangMucSerrvice.FindByChiPhiId(SelectedChiPhi.Id);
            _itemsHangMuc = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(data);
            OnPropertyChanged(nameof(ItemsHangMuc));
        }
    }
}
