using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForeignTrade.ContractorPackage
{
    public class ContractorPackageHangMucDetailDialogViewModel : DialogViewModelBase<NhHdnkCacQuyetDinhChiPhiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhHdnkCacQuyetDinhChiPhiHangMucService _nhHdnkCacQuyetDinhChiPhiHangMucService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;

        public override string Title => "THÊM MỚI THÔNG TIN GÓI THẦU - PHỤ LỤC HẠNG MỤC";
        public override string Description => "Thông tin phụ lục - hạng mục";
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public override Type ContentType => typeof(View.Forex.ForeignTrade.ContractorPackage.ContractorPackageHangMucdetailDialog);
        public bool IsDetail { get; set; }

        public RelayCommand AddHangMucCommand { get; }

        public Action<object, string> CurrencyExchangeAction { get; set; }

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

        public bool IsReadOnly => ViewState == FormViewState.DETAIL;

        private ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel> _hangMucChiPhiItems;
        public ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel> HangMucChiPhiItems
        {
            get => _hangMucChiPhiItems;
            set => SetProperty(ref _hangMucChiPhiItems, value);
        }

        private NhHdnkCacQuyetDinhChiPhiHangMucModel _hangMucChiPhiSelected;
        public NhHdnkCacQuyetDinhChiPhiHangMucModel HangMucChiPhiSelected
        {
            get => _hangMucChiPhiSelected;
            set => SetProperty(ref _hangMucChiPhiSelected, value);
        }

        public bool? IsAllHangMucItemSelected
        {
            get
            {
                if (HangMucChiPhiItems != null)
                {
                    var selected = HangMucChiPhiItems.Select(x => x.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, HangMucChiPhiItems);
                    OnPropertyChanged();
                }
            }
        }

        private double? _fGiaTriNgoaiTeKhac;
        public double? FGiaTriNgoaiTeKhac
        {
            get => _fGiaTriNgoaiTeKhac;
            set => SetProperty(ref _fGiaTriNgoaiTeKhac, value);
        }

        private double? _fGiaTriUSD;
        public double? FGiaTriUSD
        {
            get => _fGiaTriUSD;
            set => SetProperty(ref _fGiaTriUSD, value);
        }

        private double? _fGiaTriVND;
        public double? FGiaTriVND
        {
            get => _fGiaTriVND;
            set => SetProperty(ref _fGiaTriVND, value);
        }

        private double? _fGiaTriEUR;
        public double? FGiaTriEUR
        {
            get => _fGiaTriEUR;
            set => SetProperty(ref _fGiaTriEUR, value);
        }

        private List<NhDaGoiThauHangMucModel> _listHangMuc;
        public List<NhDaGoiThauHangMucModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        public List<NhDaGoiThauHangMucModel> ListHangMucLoad { get; set; }

        public ContractorPackageHangMucDetailDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INhHdnkCacQuyetDinhChiPhiHangMucService nhHdnkCacQuyetDinhChiPhiHangMucService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nhHdnkCacQuyetDinhChiPhiHangMucService = nhHdnkCacQuyetDinhChiPhiHangMucService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;

            AddHangMucCommand = new RelayCommand(obj => AddHangMuc(obj));
        }

        public override void Init()
        {
            base.Init();
            InitData();
            LoadDataHangMuc();
        }

        private void LoadDataHangMuc()
        {
            if (IsDetail)
            {
                var data = _nhDaGoiThauHangMucSerrvice.FindByChiPhi(Model.Id);
                var dataMap = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>>(data).Select(x =>
                {
                    x.FGiaTriUSDConLai = x.FGiaTriUsd - x.FGiaTriUSDGoiThau;
                    x.FGiaTriVNDConLai = x.FGiaTriVnd - x.FGiaTriVNDGoiThau;
                    x.FGiaTriEURConLai = x.FGiaTriEur - x.FGiaTriEURGoiThau;
                    x.FGiaTriNgoaiTeKhacConLai = x.FGiaTriNgoaiTeKhac - x.FGiaTriNgoaiTeKhacGoiThau;
                    x.IsSelected = true;
                    return x;
                }).ToList();
                _hangMucChiPhiItems = new ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>(dataMap);
                foreach (var item in ListHangMucLoad)
                {
                    foreach (var itemHangMuc in _hangMucChiPhiItems)
                    {
                        if (itemHangMuc.Id == item.Id)
                        {
                            itemHangMuc.FGiaTriUSDGoiThau = item.FTienGoiThauUsd;
                            itemHangMuc.FGiaTriVNDGoiThau = item.FTienGoiThauVnd;
                            itemHangMuc.FGiaTriEURGoiThau = item.FTienGoiThauEur;
                            itemHangMuc.FGiaTriNgoaiTeKhacGoiThau = item.FTienGoiThauNgoaiTeKhac;
                            itemHangMuc.FGiaTriUSDConLai = itemHangMuc.FGiaTriUsd - itemHangMuc.FGiaTriUSDGoiThau;
                            itemHangMuc.FGiaTriVNDConLai = itemHangMuc.FGiaTriVnd - itemHangMuc.FGiaTriVNDGoiThau;
                            itemHangMuc.FGiaTriEURConLai = itemHangMuc.FGiaTriEur - itemHangMuc.FGiaTriEURGoiThau;
                            itemHangMuc.FGiaTriNgoaiTeKhacConLai = itemHangMuc.FGiaTriNgoaiTeKhac - itemHangMuc.FGiaTriNgoaiTeKhacGoiThau;
                            itemHangMuc.IsSelected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                var data = _nhHdnkCacQuyetDinhChiPhiHangMucService.FindByIdQuyetDinhChiPhi(Model.Id).OrderBy(x => x.SMaOrder);
                var dataMap = _mapper.Map<ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>>(data).Select(x =>
                {
                    x.IsHangCha = x.IIdParentId.IsNullOrEmpty() ? true : false;
                    return x;
                }).ToList();
                foreach (var item in ListHangMucLoad)
                {
                    foreach (var itemHangMuc in dataMap)
                    {
                        if (itemHangMuc.Id == item.IIdCacQuyetDinhHangMucId)
                        {
                            itemHangMuc.FGiaTriUSDGoiThau = item.FTienGoiThauUsd;
                            itemHangMuc.FGiaTriVNDGoiThau = item.FTienGoiThauVnd;
                            itemHangMuc.FGiaTriEURGoiThau = item.FTienGoiThauEur;
                            itemHangMuc.FGiaTriNgoaiTeKhacGoiThau = item.FTienGoiThauNgoaiTeKhac;
                            itemHangMuc.FGiaTriUSDConLai = itemHangMuc.FGiaTriUsd - itemHangMuc.FGiaTriUSDGoiThau;
                            itemHangMuc.FGiaTriVNDConLai = itemHangMuc.FGiaTriVnd - itemHangMuc.FGiaTriVNDGoiThau;
                            itemHangMuc.FGiaTriEURConLai = itemHangMuc.FGiaTriEur - itemHangMuc.FGiaTriEURGoiThau;
                            itemHangMuc.FGiaTriNgoaiTeKhacConLai = itemHangMuc.FGiaTriNgoaiTeKhac - itemHangMuc.FGiaTriNgoaiTeKhacGoiThau;
                            itemHangMuc.IsSelected = true;
                            break;
                        }
                    }
                }
                _hangMucChiPhiItems = new ObservableCollection<NhHdnkCacQuyetDinhChiPhiHangMucModel>(dataMap);
            }
            TinhTongTien();
            foreach (var item in HangMucChiPhiItems)
            {
                item.PropertyChanged += HangMuc_PropertyChanged;
            }
            OnPropertyChanged(nameof(HangMucChiPhiItems));
        }

        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhHdnkCacQuyetDinhChiPhiHangMucModel item = (NhHdnkCacQuyetDinhChiPhiHangMucModel)sender;
            if (args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.IsSelected))
            {
                UpdateStatusHangMuc(item);
            }
            else if (item.IsSelected && (args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriUSDGoiThau) ||
                args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriVNDGoiThau) ||
                args.PropertyName == nameof(NhHdnkCacQuyetDinhChiPhiHangMucModel.FGiaTriEURGoiThau)))
            {
                CurrencyExchangeAction?.Invoke(sender, args.PropertyName);
                CalculateGiaTriHangMucChiPhi(item);
            }
        }

        private void CalculateGiaTriHangMucChiPhi(NhHdnkCacQuyetDinhChiPhiHangMucModel item)
        {
            item.FGiaTriUSDConLai = item.FGiaTriUsd - item.FGiaTriUSDGoiThau;
            item.FGiaTriVNDConLai = item.FGiaTriVnd - item.FGiaTriVNDGoiThau;
            item.FGiaTriNgoaiTeKhacConLai = item.FGiaTriNgoaiTeKhac - item.FGiaTriNgoaiTeKhacGoiThau;
            item.FGiaTriEURConLai = item.FGiaTriEur - item.FGiaTriEURGoiThau;
            TinhTongTien();
        }

        private void UpdateStatusHangMuc(NhHdnkCacQuyetDinhChiPhiHangMucModel item)
        {
            if (!item.IsSelected)
            {
                item.FGiaTriVNDGoiThau = 0;
                item.FGiaTriUSDGoiThau = 0;
                item.FGiaTriNgoaiTeKhacGoiThau = 0;
                item.FGiaTriEURGoiThau = 0;
                item.FGiaTriUSDConLai = 0;
                item.FGiaTriVNDConLai = 0;
                item.FGiaTriNgoaiTeKhacConLai = 0;
                item.FGiaTriEURConLai = 0;
            }
            TinhTongTien();
        }

        private static void SelectAll(bool select, IEnumerable<ModelBase> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private void InitData()
        {
            FGiaTriNgoaiTeKhac = 0;
            FGiaTriUSD = 0;
            FGiaTriVND = 0;
            FGiaTriEUR = 0;
        }

        private void TinhTongTien()
        {
            var listData = HangMucChiPhiItems.Where(x => x.IsSelected).ToList();
            FGiaTriNgoaiTeKhac = listData.Sum(x => x.FGiaTriNgoaiTeKhacGoiThau);
            FGiaTriUSD = listData.Sum(x => x.FGiaTriUSDGoiThau);
            FGiaTriVND = listData.Sum(x => x.FGiaTriVNDGoiThau);
            FGiaTriEUR = listData.Sum(x => x.FGiaTriEURGoiThau);
        }

        public void AddHangMuc(object obj)
        {
            ListHangMuc = new List<NhDaGoiThauHangMucModel>();
            foreach (var item in HangMucChiPhiItems)
            {
                NhDaGoiThauHangMucModel itemHangMuc = new NhDaGoiThauHangMucModel();
                if (item.IsSelected)
                {
                    itemHangMuc.FTienGoiThauUsd = item.FGiaTriUSDGoiThau;
                    itemHangMuc.FTienGoiThauVnd = item.FGiaTriVNDGoiThau;
                    itemHangMuc.FTienGoiThauEur = item.FGiaTriEURGoiThau;
                    itemHangMuc.FTienGoiThauNgoaiTeKhac = item.FGiaTriNgoaiTeKhacGoiThau;
                    if (!IsDetail)
                    {
                        itemHangMuc.IIdGoiThauChiPhiId = Model.IIdChiPhiId;
                        itemHangMuc.IIdCacQuyetDinhHangMucId = item.Id;
                    }
                    else
                    {
                        itemHangMuc.Id = item.Id;
                    }
                    ListHangMuc.Add(itemHangMuc);
                }
            }
            SavedAction?.Invoke(ListHangMuc);
            ((Window)obj).Close();
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
