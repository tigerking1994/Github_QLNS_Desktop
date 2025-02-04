using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.BaoCaoQuyetToan;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.PheDuyetQuyetToanDAHT;
//using VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.QuyetToanNienDo.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.BaoCaoQuyetToan
{
    public class BaoCaoKetLuanQuyetToanIndexViewModel : GridViewModelBase<NhQtPheDuyetQuyetToanDAHTChiTietModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhQtPheDuyetQuyetToanDAHTService _service;
        private readonly INhThTongHopService _nhThTongHopService;

        private ICollectionView _itemsCollectionView;
        public BaoCaoKetLuanQuyetToanPrintDialogViewModel BaoCaoKetLuanQuyetToanPrintDialogViewModel { get; set; }
        public override string GroupName => MenuItemContants.GROUP_FOREX_QUYETTOAN_BAOCAO;
        public override string Name => "Báo cáo kết luận quyết toán";
        public override string Title => "Báo cáo kết luận quyết toán";
        public override string Description => "Danh sách Báo cáo kết luận quyết toán";
        public override Type ContentType => typeof(BaoCaoKetLuanQuyetToanIndex);
        public override PackIconKind IconKind => PackIconKind.BagChecked;
        public bool IsEditable => SelectedItem != null;
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
                if (SetProperty(ref _selectedDonVi, value))
                {
                }
            }
        }
        private FilterBaoCaoKetLuanQuyetToan _itemsFilter;
        public FilterBaoCaoKetLuanQuyetToan ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        public int _CountGiaiDoanKeHoach;
        public int _CountGiaiDoanKinhPhi;
        public int _CountGiaiDoanQuyetToan;
        public int CountGiaiDoanKeHoach
        {
            get => _CountGiaiDoanKeHoach;
            set => SetProperty(ref _CountGiaiDoanKeHoach, value);
        }
        public int CountGiaiDoanKinhPhi
        {
            get => _CountGiaiDoanKinhPhi;
            set => SetProperty(ref _CountGiaiDoanKinhPhi, value);
        }
        public int CountGiaiDoanQuyetToan
        {
            get => _CountGiaiDoanQuyetToan;
            set => SetProperty(ref _CountGiaiDoanQuyetToan, value);
        }
        #region giaiDoan
        public string _GiaiDoan1;
        public string _GiaiDoan2;
        public string _GiaiDoan3;
        public string _GiaiDoan4;
        public string _GiaiDoan5;
        public string _GiaiDoan6;
        public string _GiaiDoan7;
        public string _GiaiDoan8;
        public string _GiaiDoan9;
        public string _GiaiDoan10;
        public string GiaiDoan1
        {
            get => _GiaiDoan1;
            set => SetProperty(ref _GiaiDoan1, value);
        }

        public string GiaiDoan2
        {
            get => _GiaiDoan2;
            set => SetProperty(ref _GiaiDoan2, value);
        }
        public string GiaiDoan3
        {
            get => _GiaiDoan3;
            set => SetProperty(ref _GiaiDoan3, value);
        }
        public string GiaiDoan4
        {
            get => _GiaiDoan4;
            set => SetProperty(ref _GiaiDoan4, value);
        }
        public string GiaiDoan5
        {
            get => _GiaiDoan5;
            set => SetProperty(ref _GiaiDoan5, value);
        }
        public string GiaiDoan6
        {
            get => _GiaiDoan6;
            set => SetProperty(ref _GiaiDoan6, value);
        }
        public string GiaiDoan7
        {
            get => _GiaiDoan7;
            set => SetProperty(ref _GiaiDoan7, value);
        }
        public string GiaiDoan8
        {
            get => _GiaiDoan8;
            set => SetProperty(ref _GiaiDoan8, value);
        }
        public string GiaiDoan9
        {
            get => _GiaiDoan9;
            set => SetProperty(ref _GiaiDoan9, value);
        }
        public string GiaiDoan10
        {
            get => _GiaiDoan10;
            set => SetProperty(ref _GiaiDoan10, value);
        }
        #endregion

        #region hideGiaiDoan
        public bool _VGiaiDoan1;
        public bool _VGiaiDoan2;
        public bool _VGiaiDoan3;
        public bool _VGiaiDoan4;
        public bool _VGiaiDoan5;
        public bool _VGiaiDoan6;
        public bool _VGiaiDoan7;
        public bool _VGiaiDoan8;
        public bool _VGiaiDoan9;
        public bool _VGiaiDoan10;

        public bool VGiaiDoan1
        {
            get => _VGiaiDoan1;
            set => SetProperty(ref _VGiaiDoan1, value);
        }
        public bool VGiaiDoan2
        {
            get => _VGiaiDoan2;
            set => SetProperty(ref _VGiaiDoan2, value);
        }
        public bool VGiaiDoan3
        {
            get => _VGiaiDoan3;
            set => SetProperty(ref _VGiaiDoan3, value);
        }
        public bool VGiaiDoan4
        {
            get => _VGiaiDoan4;
            set => SetProperty(ref _VGiaiDoan4, value);
        }
        public bool VGiaiDoan5
        {
            get => _VGiaiDoan5;
            set => SetProperty(ref _VGiaiDoan6, value);
        }
        public bool VGiaiDoan6
        {
            get => _VGiaiDoan6;
            set => SetProperty(ref _VGiaiDoan6, value);
        }
        public bool VGiaiDoan7
        {
            get => _VGiaiDoan7;
            set => SetProperty(ref _VGiaiDoan7, value);
        }
        public bool VGiaiDoan8
        {
            get => _VGiaiDoan8;
            set => SetProperty(ref _VGiaiDoan8, value);
        }
        public bool VGiaiDoan9
        {
            get => _VGiaiDoan9;
            set => SetProperty(ref _VGiaiDoan9, value);
        }
        public bool VGiaiDoan10
        {
            get => _VGiaiDoan10;
            set => SetProperty(ref _VGiaiDoan10, value);
        }
        #endregion



        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand PrintCommand { get; }

        public BaoCaoKetLuanQuyetToanIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhQtPheDuyetQuyetToanDAHTService service,
            INhThTongHopService nhThTongHopService,
            BaoCaoKetLuanQuyetToanPrintDialogViewModel baoCaoKetLuanQuyetToanPrintDialogViewModel
            //PheDuyetQuyetToanDAHTDetailViewModel pheDuyetQuyetToanDAHTDetailViewModel,
            //PheDuyetQuyetToanDAHTPrintDialogViewModel pheDuyetQuyetToanDAHTPrintDialogViewModel
            )
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _service = service;
            _nhThTongHopService = nhThTongHopService;

            BaoCaoKetLuanQuyetToanPrintDialogViewModel = baoCaoKetLuanQuyetToanPrintDialogViewModel;
            //PheDuyetQuyetToanDAHTDialogViewModel = pheDuyetQuyetToanDAHTDialogViewModel;
            //PheDuyetQuyetToanDAHTDetailViewModel = pheDuyetQuyetToanDAHTDetailViewModel;
            //PheDuyetQuyetToanDAHTPrintDialogViewModel = pheDuyetQuyetToanDAHTPrintDialogViewModel;

            SearchCommand = new RelayCommand(obj => FindDataDetail());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            LockUnLockCommand = new RelayCommand(o => OnLockUnLock(), obj => SelectedItem != null);
            PrintCommand = new RelayCommand(obj => OnPrint());
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new FilterBaoCaoKetLuanQuyetToan();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindInternalByNamLamViec(_sessionService.Current.YearOfWork);
                ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        public override void LoadData(params object[] args)
        {
            //Items = new ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            //var listGiaiDoan = _service.GetGiaiDoan().OrderBy(x => x.iGiaiDoanTu).ThenBy(x => x.iGiaiDoanDen).Take(10).ToList();
            //for (var i = 0; i < listGiaiDoan.Count(); i++)
            //{
            //    GiaiDoan1 = i == 0 ? listGiaiDoan[0].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[0].iGiaiDoanDen.ToString() : GiaiDoan1 != null ? GiaiDoan1 : null;
            //    GiaiDoan2 = i == 1 ? listGiaiDoan[1].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[1].iGiaiDoanDen.ToString() : GiaiDoan2 != null ? GiaiDoan2 : null;
            //    GiaiDoan3 = i == 2 ? listGiaiDoan[2].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[2].iGiaiDoanDen.ToString() : GiaiDoan3 != null ? GiaiDoan3 : null;
            //    GiaiDoan4 = i == 3 ? listGiaiDoan[3].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[3].iGiaiDoanDen.ToString() : GiaiDoan4 != null ? GiaiDoan4 : null;
            //    GiaiDoan5 = i == 4 ? listGiaiDoan[4].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[4].iGiaiDoanDen.ToString() : GiaiDoan5 != null ? GiaiDoan5 : null;
            //    GiaiDoan6 = i == 5 ? listGiaiDoan[5].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[5].iGiaiDoanDen.ToString() : GiaiDoan6 != null ? GiaiDoan6 : null;
            //    GiaiDoan7 = i == 6 ? listGiaiDoan[6].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[6].iGiaiDoanDen.ToString() : GiaiDoan7 != null ? GiaiDoan7 : null;
            //    GiaiDoan8 = i == 7 ? listGiaiDoan[7].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[7].iGiaiDoanDen.ToString() : GiaiDoan8 != null ? GiaiDoan8 : null;
            //    GiaiDoan9 = i == 8 ? listGiaiDoan[8].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[8].iGiaiDoanDen.ToString() : GiaiDoan9 != null ? GiaiDoan9 : null;
            //    GiaiDoan10 = i == 9 ? listGiaiDoan[9].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[9].iGiaiDoanDen.ToString() : GiaiDoan10 != null ? GiaiDoan10 : null;

            //}

            //VGiaiDoan1 = GiaiDoan1 != null;
            //VGiaiDoan2 = GiaiDoan2 != null;
            //VGiaiDoan3 = GiaiDoan3 != null;
            //VGiaiDoan4 = GiaiDoan4 != null;
            //VGiaiDoan5 = GiaiDoan5 != null;
            //VGiaiDoan6 = GiaiDoan6 != null;
            //VGiaiDoan7 = GiaiDoan7 != null;
            //VGiaiDoan8 = GiaiDoan8 != null;
            //VGiaiDoan9 = GiaiDoan9 != null;
            //VGiaiDoan10 = GiaiDoan10 != null;

            //CountGiaiDoanKeHoach = listGiaiDoan.Count() != 0 ? listGiaiDoan.Count() : 1;
            //CountGiaiDoanKinhPhi = listGiaiDoan.Count() != 0 ? listGiaiDoan.Count() * 2 : 2;
            //CountGiaiDoanQuyetToan = listGiaiDoan.Count() != 0 ? listGiaiDoan.Count() * 2 : 2;
            FindDataDetail();

        }
        public void FindDataDetail()
        {
            Items = new ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            var data = _service.GetDataBaoCaoKetLuanDetail(SelectedDonVi != null ? _selectedDonVi.Id : Guid.Empty, null, null, ItemsFilter.DNgayPheDuyetTu, ItemsFilter.DNgayPheDuyetDen);
            List<NhQtPheDuyetQuyetToanDAHTChiTietQuery> getlistGiaiDoan = data.Where(x => x.INamBaoCaoTu != null && x.INamBaoCaoDen != null).ToList();

            List<NhTtThucHienNganSachGiaiDoanQuery> listGiaiDoan = getlistGiaiDoan
                    .GroupBy(x => (x.INamBaoCaoTu, x.INamBaoCaoDen)).Select(x => x.First())
                    .Select(x => new NhTtThucHienNganSachGiaiDoanQuery
                    {
                        sGiaiDoan = "Giai đoạn " + x.INamBaoCaoTu + " - " + x.INamBaoCaoDen,
                        iGiaiDoanTu = x.INamBaoCaoTu,
                        iGiaiDoanDen = x.INamBaoCaoDen
                    }).Take(10).ToList();
            CountGiaiDoanKeHoach = listGiaiDoan.Count() != 0 ? (listGiaiDoan.Count() + 1) : 1;
            CountGiaiDoanKinhPhi = listGiaiDoan.Count() != 0 ? (listGiaiDoan.Count() + 1) * 2 : 2;
            CountGiaiDoanQuyetToan = listGiaiDoan.Count() != 0 ? (listGiaiDoan.Count() + 1) * 2 : 2;

            for (var i = 0; i < listGiaiDoan.Count(); i++)
            {
                GiaiDoan1 = i == 0 ? listGiaiDoan[0].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[0].iGiaiDoanDen.ToString() : GiaiDoan1 != null ? GiaiDoan1 : null;
                GiaiDoan2 = i == 1 ? listGiaiDoan[1].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[1].iGiaiDoanDen.ToString() : GiaiDoan2 != null ? GiaiDoan2 : null;
                GiaiDoan3 = i == 2 ? listGiaiDoan[2].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[2].iGiaiDoanDen.ToString() : GiaiDoan3 != null ? GiaiDoan3 : null;
                GiaiDoan4 = i == 3 ? listGiaiDoan[3].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[3].iGiaiDoanDen.ToString() : GiaiDoan4 != null ? GiaiDoan4 : null;
                GiaiDoan5 = i == 4 ? listGiaiDoan[4].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[4].iGiaiDoanDen.ToString() : GiaiDoan5 != null ? GiaiDoan5 : null;
                GiaiDoan6 = i == 5 ? listGiaiDoan[5].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[5].iGiaiDoanDen.ToString() : GiaiDoan6 != null ? GiaiDoan6 : null;
                GiaiDoan7 = i == 6 ? listGiaiDoan[6].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[6].iGiaiDoanDen.ToString() : GiaiDoan7 != null ? GiaiDoan7 : null;
                GiaiDoan8 = i == 7 ? listGiaiDoan[7].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[7].iGiaiDoanDen.ToString() : GiaiDoan8 != null ? GiaiDoan8 : null;
                GiaiDoan9 = i == 8 ? listGiaiDoan[8].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[8].iGiaiDoanDen.ToString() : GiaiDoan9 != null ? GiaiDoan9 : null;
                GiaiDoan10 = i == 9 ? listGiaiDoan[9].iGiaiDoanTu.ToString() + " - " + listGiaiDoan[9].iGiaiDoanDen.ToString() : GiaiDoan10 != null ? GiaiDoan10 : null;
            }

            VGiaiDoan1 = GiaiDoan1 != null;
            VGiaiDoan2 = GiaiDoan2 != null;
            VGiaiDoan3 = GiaiDoan3 != null;
            VGiaiDoan4 = GiaiDoan4 != null;
            VGiaiDoan5 = GiaiDoan5 != null;
            VGiaiDoan6 = GiaiDoan6 != null;
            VGiaiDoan7 = GiaiDoan7 != null;
            VGiaiDoan8 = GiaiDoan8 != null;
            VGiaiDoan9 = GiaiDoan9 != null;
            VGiaiDoan10 = GiaiDoan10 != null;

            var returnData = getList(_mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(data).ToList(), listGiaiDoan);

            Items = _mapper.Map<ObservableCollection<NhQtPheDuyetQuyetToanDAHTChiTietModel>>(returnData);

            OnPropertyChanged(nameof(CountGiaiDoanKeHoach));
            OnPropertyChanged(nameof(CountGiaiDoanKinhPhi));
            OnPropertyChanged(nameof(CountGiaiDoanQuyetToan));

            OnPropertyChanged(nameof(GiaiDoan1));
            OnPropertyChanged(nameof(GiaiDoan2));
            OnPropertyChanged(nameof(GiaiDoan3));
            OnPropertyChanged(nameof(GiaiDoan4));
            OnPropertyChanged(nameof(GiaiDoan5));
            OnPropertyChanged(nameof(GiaiDoan6));
            OnPropertyChanged(nameof(GiaiDoan7));
            OnPropertyChanged(nameof(GiaiDoan8));
            OnPropertyChanged(nameof(GiaiDoan9));
            OnPropertyChanged(nameof(GiaiDoan10));

            OnPropertyChanged(nameof(VGiaiDoan1));
            OnPropertyChanged(nameof(VGiaiDoan2));
            OnPropertyChanged(nameof(VGiaiDoan3));
            OnPropertyChanged(nameof(VGiaiDoan4));
            OnPropertyChanged(nameof(VGiaiDoan5));
            OnPropertyChanged(nameof(VGiaiDoan6));
            OnPropertyChanged(nameof(VGiaiDoan7));
            OnPropertyChanged(nameof(VGiaiDoan8));
            OnPropertyChanged(nameof(VGiaiDoan9));
            OnPropertyChanged(nameof(VGiaiDoan10));

            OnPropertyChanged(nameof(Items));
        }

        protected override void OnAdd()
        {
            //PheDuyetQuyetToanDAHTDialogViewModel.IsDetail = false;
            //PheDuyetQuyetToanDAHTDialogViewModel.Model = new NhQtPheDuyetQuyetToanDAHTModel();
            //PheDuyetQuyetToanDAHTDialogViewModel.Init();
            //PheDuyetQuyetToanDAHTDialogViewModel.SavedAction = obj => this.OnRefresh();
            //PheDuyetQuyetToanDAHTDialogViewModel.ShowDialogHost();
        }

        protected override void OnUpdate()
        {
            //PheDuyetQuyetToanDAHTDialogViewModel.IsDetail = false;
            //PheDuyetQuyetToanDAHTDialogViewModel.Model = SelectedItem;
            //PheDuyetQuyetToanDAHTDialogViewModel.Init();
            //PheDuyetQuyetToanDAHTDialogViewModel.SavedAction = obj => this.OnRefresh();
            //PheDuyetQuyetToanDAHTDialogViewModel.ShowDialogHost();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            //PheDuyetQuyetToanDAHTDetailViewModel.Model = SelectedItem;
            //PheDuyetQuyetToanDAHTDetailViewModel.Init();
            //PheDuyetQuyetToanDAHTDetailViewModel.ShowDialog();
        }

        private void OnPrint()
        {
            BaoCaoKetLuanQuyetToanPrintDialogViewModel.DNgayPheDuyetTu = ItemsFilter.DNgayPheDuyetTu;
            BaoCaoKetLuanQuyetToanPrintDialogViewModel.DNgayPheDuyetDen = ItemsFilter.DNgayPheDuyetDen;
            BaoCaoKetLuanQuyetToanPrintDialogViewModel.IIdDonVi = ItemsFilter.IIdDonViId;

            BaoCaoKetLuanQuyetToanPrintDialogViewModel.Init();
            BaoCaoKetLuanQuyetToanPrintDialogViewModel.ShowDialogHost();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new FilterBaoCaoKetLuanQuyetToan();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        public override void Dispose()
        {
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            // Method intentionally left empty.
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEditable));
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhQtPheDuyetQuyetToanDAHTModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdMaDonVi != null && item.IIdMaDonVi.Equals(SelectedDonVi.IIDMaDonVi);
                }

                if (ItemsFilter != null)
                {

                    if (ItemsFilter.DNgayPheDuyetTu != null && ItemsFilter.DNgayPheDuyetDen != null)
                    {
                        result &= item.DNgayPheDuyet.HasValue && item.DNgayPheDuyet.Value.Date >= ItemsFilter.DNgayPheDuyetTu.Value.Date && item.DNgayPheDuyet.Value.Date <= ItemsFilter.DNgayPheDuyetDen.Value.Date;
                    }
                    if (ItemsFilter.DNgayPheDuyetTu != null && ItemsFilter.DNgayPheDuyetDen != null)
                    {
                        result &= item.DNgayPheDuyet.HasValue && item.DNgayPheDuyet.Value.Date >= ItemsFilter.DNgayPheDuyetTu.Value.Date && item.DNgayPheDuyet.Value.Date <= ItemsFilter.DNgayPheDuyetDen.Value.Date;
                    }
                    //if (ItemsFilter.INamBaoCaoTu != null)
                    //{
                    //    result &= item.INamBaoCaoTu != null && item.INamBaoCaoTu == ItemsFilter.INamBaoCaoTu;
                    //}
                    //if (ItemsFilter.INamBaoCaoDen != null)
                    //{
                    //    result &= item.INamBaoCaoDen != null && item.INamBaoCaoDen == ItemsFilter.INamBaoCaoDen;
                    //}

                    //if (!string.IsNullOrEmpty(ItemsFilter.IIdMaDonVi))
                    //{
                    //    result &= item.IIdMaDonVi != null && item.IIdMaDonVi.Equals(ItemsFilter.IIdMaDonVi);
                    //}

                }
                return result;
            }
            return false;
        }
        private List<NhQtPheDuyetQuyetToanDAHTChiTietModel> getList(List<NhQtPheDuyetQuyetToanDAHTChiTietModel> list, List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listData = new List<NhQtPheDuyetQuyetToanDAHTChiTietModel>();
            int SttLoai = 0;
            int SttHopDong = 0;
            int SttDuAn = 0;
            int SttChuongTrinh = 0;
            Guid? idDuAn = null;
            Guid? idHopDong = null;
            Guid? idChuongTrinh = null;
            int? idLoai = null;
            int sttTong = 0;
            NhQtPheDuyetQuyetToanDAHTChiTietModel DataTong = new NhQtPheDuyetQuyetToanDAHTChiTietModel();

            DataTong.STenNoiDungChi = "Tổng cộng: ";
            if (list != null || list.Count == 0)
            {
                var newListData = SetNewListData(list, lstGiaiDoan);
                foreach (var item in newListData)
                {
                    var newData = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                    sttTong++;
                    if (lstGiaiDoan != null)
                    {
                        newData = SetData(item, lstGiaiDoan);
                    }
                    if (item.IIDKHTTNhiemVuChiId != idChuongTrinh/* && item.IIDKHTTNhiemVuChiId != Guid.Empty*/)
                    {
                        SttChuongTrinh++;
                        SttDuAn = 0;
                        SttLoai = 0;
                        SttDuAn = 0;
                        idDuAn = null;
                        idLoai = null;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();

                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();
                        DataCha.FHopDongUsd = null;
                        DataCha.FHopDongVnd = null;

                        DataCha.FKeHoachTTCPUsd = listDataCha.Sum(x => x.FKeHoachTTCPUsd);

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.IIDKHTTNhiemVuChiId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLetter(SttChuongTrinh) + ", " + item.STenNhiemVuChi;
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = ConvertLetter(SttChuongTrinh) + ", " + "Nội dung chi khác"; 
                        }
                        idChuongTrinh = item.IIDKHTTNhiemVuChiId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;
                        DataCha.IsHangCha = true;

                        listData.Add(SetDataSum(DataCha, lstGiaiDoan, listDataCha));
                    }
                    if (item.IIDDuAnId != idDuAn /*&& item.IIDDuAnId != Guid.Empty*/)
                    {
                        SttDuAn++;
                        SttLoai = 0;
                        SttHopDong = 0;
                        idLoai = null;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdDuAn);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndDuAn);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.IIDDuAnId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + item.STenDuAn;
                        }
                        else if (item.IIDHopDongId != Guid.Empty)
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + "Chi hợp đồng";
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = ConvertLaMa(SttDuAn) + ", " + "Chi khác";
                        }
                        idDuAn = item.IIDDuAnId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;
                        DataCha.IsHangCha = true;
                        listData.Add(SetDataSum(DataCha, lstGiaiDoan, listDataCha));
                    }
                    if (item.ILoaiNoiDungChi != idLoai && item.ILoaiNoiDungChi != 0)
                    {
                        SttLoai++;
                        SttHopDong = 0;
                        idHopDong = null;
                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.ILoaiNoiDungChi == item.ILoaiNoiDungChi && x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdHopDong);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndHopDong);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        if (item.ILoaiNoiDungChi == 1)
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi ngoại tệ";
                        }
                        else if (item.ILoaiNoiDungChi == 2)
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi trong nước";
                        }
                        else
                        {
                            DataCha.STenNoiDungChi = SttLoai.ToString() + ", Chi khác";
                        }
                        idLoai = item.ILoaiNoiDungChi;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;
                        DataCha.IsHangCha = true;
                        listData.Add(SetDataSum(DataCha, lstGiaiDoan, listDataCha));
                    }
                    if (item.IIDHopDongId != idHopDong && item.IIDHopDongId != Guid.Empty)
                    {
                        SttHopDong++;

                        NhQtPheDuyetQuyetToanDAHTChiTietModel DataCha = new NhQtPheDuyetQuyetToanDAHTChiTietModel();
                        List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataCha = list.Where(x => x.IIDHopDongId == item.IIDHopDongId && x.ILoaiNoiDungChi == item.ILoaiNoiDungChi && x.IIDDuAnId == item.IIDDuAnId && x.IIDKHTTNhiemVuChiId == item.IIDKHTTNhiemVuChiId).ToList();

                        DataCha.FHopDongUsd = listDataCha.Sum(x => x.FHopDongUsdHopDong);
                        DataCha.FHopDongVnd = listDataCha.Sum(x => x.FHopDongVndHopDong);

                        DataCha.FKeHoachTTCPUsd = null;

                        DataCha.FKinhPhiDuocCapTongUsd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongUsd);
                        DataCha.FKinhPhiDuocCapTongVnd = listDataCha.Sum(x => x.FKinhPhiDuocCapTongVnd);

                        DataCha.FQuyetToanDuocDuyetTongUsd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                        DataCha.FQuyetToanDuocDuyetTongVnd = listDataCha.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                        DataCha.FSoSanhKinhPhiUsd = listDataCha.Sum(x => x.FSoSanhKinhPhiUsd);
                        DataCha.FSoSanhKinhPhiVnd = listDataCha.Sum(x => x.FSoSanhKinhPhiVnd);

                        DataCha.FThuaTraNSNNUsd = listDataCha.Sum(x => x.FThuaTraNSNNUsd);
                        DataCha.FThuaTraNSNNVnd = listDataCha.Sum(x => x.FThuaTraNSNNVnd);

                        DataCha.STenNoiDungChi = SttLoai.ToString() + "." + SttHopDong.ToString() + ", " + item.STenHopDong;
                        idHopDong = item.IIDHopDongId;
                        DataCha.listDataTTCP = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataKPDC = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.listDataQTDD = new List<NH_QT_PheDuyetQuyetToanDAHTDataGiaiDoan>();
                        DataCha.INamBaoCaoDen = item.INamBaoCaoDen;
                        DataCha.INamBaoCaoTu = item.INamBaoCaoTu;


                        listData.Add(SetDataSum(DataCha, lstGiaiDoan, listDataCha));
                    }

                    listData.Add(newData);
                }




                DataTong.FKinhPhiDuocCapTongUsd = list.Sum(x => x.FKinhPhiDuocCapTongUsd);
                DataTong.FKinhPhiDuocCapTongVnd = list.Sum(x => x.FKinhPhiDuocCapTongVnd);

                DataTong.FQuyetToanDuocDuyetTongUsd = list.Sum(x => x.FQuyetToanDuocDuyetTongUsd);
                DataTong.FQuyetToanDuocDuyetTongVnd = list.Sum(x => x.FQuyetToanDuocDuyetTongVnd);

                DataTong.FSoSanhKinhPhiUsd = list.Sum(x => x.FSoSanhKinhPhiUsd);
                DataTong.FSoSanhKinhPhiVnd = list.Sum(x => x.FSoSanhKinhPhiVnd);

                DataTong.FThuaTraNSNNUsd = list.Sum(x => x.FThuaTraNSNNUsd);
                DataTong.FThuaTraNSNNVnd = list.Sum(x => x.FThuaTraNSNNVnd);
                DataTong.IsHangCha = true;
                listData.Add(SetDataSum(DataTong, lstGiaiDoan, list));
            }

            return listData;
        }
        private NhQtPheDuyetQuyetToanDAHTChiTietModel SetData(NhQtPheDuyetQuyetToanDAHTChiTietModel item, List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            for (var i = 0; i < lstGiaiDoan.Count(); i++)
            {
                if (item.INamBaoCaoTu == lstGiaiDoan[i].iGiaiDoanTu && item.INamBaoCaoDen == lstGiaiDoan[i].iGiaiDoanDen)
                {
                    item.FKeHoachTTCPUsd1 = i == 0 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd1 != null ? item.FKeHoachTTCPUsd1 : null;
                    item.FKeHoachTTCPUsd2 = i == 1 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd2 != null ? item.FKeHoachTTCPUsd2 : null;
                    item.FKeHoachTTCPUsd3 = i == 2 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd3 != null ? item.FKeHoachTTCPUsd3 : null;
                    item.FKeHoachTTCPUsd4 = i == 3 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd4 != null ? item.FKeHoachTTCPUsd4 : null;
                    item.FKeHoachTTCPUsd5 = i == 4 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd5 != null ? item.FKeHoachTTCPUsd5 : null;
                    item.FKeHoachTTCPUsd6 = i == 5 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd6 != null ? item.FKeHoachTTCPUsd6 : null;
                    item.FKeHoachTTCPUsd7 = i == 6 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd7 != null ? item.FKeHoachTTCPUsd7 : null;
                    item.FKeHoachTTCPUsd8 = i == 7 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd8 != null ? item.FKeHoachTTCPUsd8 : null;
                    item.FKeHoachTTCPUsd9 = i == 8 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd9 != null ? item.FKeHoachTTCPUsd9 : null;
                    item.FKeHoachTTCPUsd10 = i == 9 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd10 != null ? item.FKeHoachTTCPUsd10 : null;

                    item.FKinhPhiDuocCapTongVnd1 = i == 0 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                    item.FKinhPhiDuocCapTongVnd2 = i == 1 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                    item.FKinhPhiDuocCapTongVnd3 = i == 2 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                    item.FKinhPhiDuocCapTongVnd4 = i == 3 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                    item.FKinhPhiDuocCapTongVnd5 = i == 4 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                    item.FKinhPhiDuocCapTongVnd6 = i == 5 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                    item.FKinhPhiDuocCapTongVnd7 = i == 6 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                    item.FKinhPhiDuocCapTongVnd8 = i == 7 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                    item.FKinhPhiDuocCapTongVnd9 = i == 8 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                    item.FKinhPhiDuocCapTongVnd10 = i == 9 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                    item.FKinhPhiDuocCapTongUsd1 = i == 0 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                    item.FKinhPhiDuocCapTongUsd2 = i == 1 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                    item.FKinhPhiDuocCapTongUsd3 = i == 2 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                    item.FKinhPhiDuocCapTongUsd4 = i == 3 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                    item.FKinhPhiDuocCapTongUsd5 = i == 4 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                    item.FKinhPhiDuocCapTongUsd6 = i == 5 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                    item.FKinhPhiDuocCapTongUsd7 = i == 6 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                    item.FKinhPhiDuocCapTongUsd8 = i == 7 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                    item.FKinhPhiDuocCapTongUsd9 = i == 8 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                    item.FKinhPhiDuocCapTongUsd10 = i == 9 ? item.FKinhPhiDuocCapTongUsd : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;

                    item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                    item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                    item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                    item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                    item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                    item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                    item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                    item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                    item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                    item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;

                    item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                    item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                    item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                    item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                    item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                    item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                    item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                    item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                    item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                    item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                }
            }

            return item;
        }

        private List<NhQtPheDuyetQuyetToanDAHTChiTietModel> SetNewListData(List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listData, List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            var dataTongHops = LoadDataTongHop(lstGiaiDoan);
            var lstMaNguon = NHConstants.MA_TH_BCKL_QT.Split(",").Select(x => x.Trim()).ToList();
            foreach (var item in listData)
            {
                for (var i = 0; i < lstGiaiDoan.Count(); i++)
                {
                    if (item.INamBaoCaoTu == lstGiaiDoan[i].iGiaiDoanTu && item.INamBaoCaoDen == lstGiaiDoan[i].iGiaiDoanDen)
                    {                 
                        item.FKeHoachTTCPUsd1 = i == 0 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd1 != null ? item.FKeHoachTTCPUsd1 : null;
                        item.FKeHoachTTCPUsd2 = i == 1 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd2 != null ? item.FKeHoachTTCPUsd2 : null;
                        item.FKeHoachTTCPUsd3 = i == 2 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd3 != null ? item.FKeHoachTTCPUsd3 : null;
                        item.FKeHoachTTCPUsd4 = i == 3 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd4 != null ? item.FKeHoachTTCPUsd4 : null;
                        item.FKeHoachTTCPUsd5 = i == 4 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd5 != null ? item.FKeHoachTTCPUsd5 : null;
                        item.FKeHoachTTCPUsd6 = i == 5 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd6 != null ? item.FKeHoachTTCPUsd6 : null;
                        item.FKeHoachTTCPUsd7 = i == 6 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd7 != null ? item.FKeHoachTTCPUsd7 : null;
                        item.FKeHoachTTCPUsd8 = i == 7 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd8 != null ? item.FKeHoachTTCPUsd8 : null;
                        item.FKeHoachTTCPUsd9 = i == 8 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd9 != null ? item.FKeHoachTTCPUsd9 : null;
                        item.FKeHoachTTCPUsd10 = i == 9 ? item.FKeHoachTTCPUsd : item.FKeHoachTTCPUsd10 != null ? item.FKeHoachTTCPUsd10 : null;
                        //dataTHKinhphi = nguon 306(aj) - nguon306(ai-1)
                        var dataThKinhPhiCapVnd = (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd)) - (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd));
                        //item.FKinhPhiDuocCapTongVnd1 = i == 0 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                        //item.FKinhPhiDuocCapTongVnd2 = i == 1 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                        //item.FKinhPhiDuocCapTongVnd3 = i == 2 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                        //item.FKinhPhiDuocCapTongVnd4 = i == 3 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                        //item.FKinhPhiDuocCapTongVnd5 = i == 4 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                        //item.FKinhPhiDuocCapTongVnd6 = i == 5 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                        //item.FKinhPhiDuocCapTongVnd7 = i == 6 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                        //item.FKinhPhiDuocCapTongVnd8 = i == 7 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                        //item.FKinhPhiDuocCapTongVnd9 = i == 8 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                        //item.FKinhPhiDuocCapTongVnd10 = i == 9 ? item.FKinhPhiDuocCapTongVnd : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                        item.FKinhPhiDuocCapTongVnd1 = i == 0 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                        item.FKinhPhiDuocCapTongVnd2 = i == 1 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                        item.FKinhPhiDuocCapTongVnd3 = i == 2 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                        item.FKinhPhiDuocCapTongVnd4 = i == 3 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                        item.FKinhPhiDuocCapTongVnd5 = i == 4 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                        item.FKinhPhiDuocCapTongVnd6 = i == 5 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                        item.FKinhPhiDuocCapTongVnd7 = i == 6 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                        item.FKinhPhiDuocCapTongVnd8 = i == 7 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                        item.FKinhPhiDuocCapTongVnd9 = i == 8 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                        item.FKinhPhiDuocCapTongVnd10 = i == 9 ? dataThKinhPhiCapVnd : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                        var dataThKinhPhiCapUsd = (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd)) - (dataTongHops.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306 && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd));
                        item.FKinhPhiDuocCapTongUsd1 = i == 0 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                        item.FKinhPhiDuocCapTongUsd2 = i == 1 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                        item.FKinhPhiDuocCapTongUsd3 = i == 2 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                        item.FKinhPhiDuocCapTongUsd4 = i == 3 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                        item.FKinhPhiDuocCapTongUsd5 = i == 4 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                        item.FKinhPhiDuocCapTongUsd6 = i == 5 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                        item.FKinhPhiDuocCapTongUsd7 = i == 6 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                        item.FKinhPhiDuocCapTongUsd8 = i == 7 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                        item.FKinhPhiDuocCapTongUsd9 = i == 8 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                        item.FKinhPhiDuocCapTongUsd10 = i == 9 ? dataThKinhPhiCapUsd : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;

                        //dataTHKinhPhiDuocDuyet = nguon 301,304(aj) - nguon301,304(ai-1)

                        //item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                        //item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                        //item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                        //item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                        //item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                        //item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                        //item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                        //item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                        //item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                        //item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? item.FQuyetToanDuocDuyetTongUsd : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;

                        var dataThKinhPhiDuocDuyetUsd = (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriUsd)) - (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriUsd));
                        item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                        item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                        item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                        item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                        item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                        item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                        item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                        item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                        item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                        item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? dataThKinhPhiDuocDuyetUsd : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;

                        //item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                        //item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                        //item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                        //item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                        //item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                        //item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                        //item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                        //item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                        //item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                        //item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? item.FQuyetToanDuocDuyetTongVnd : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                        var dataThKinhPhiDuocDuyetVnd = (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen)
                                                                    .Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanDen).Sum(x => x.FGiaTriVnd)) -
                                                                    (dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguon) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1)
                                                                    .Sum(x => x.FGiaTriVnd) - dataTongHops.Where(x => lstMaNguon.Contains(x.SMaNguonCha) && x.INamKeHoach == lstGiaiDoan[i].iGiaiDoanTu - 1).Sum(x => x.FGiaTriVnd));
                        item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                        item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                        item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                        item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                        item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                        item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                        item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                        item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                        item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                        item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? dataThKinhPhiDuocDuyetVnd : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                    }
                }
            }

            return listData;
        }

        private NhQtPheDuyetQuyetToanDAHTChiTietModel SetDataSum(NhQtPheDuyetQuyetToanDAHTChiTietModel item, List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan, List<NhQtPheDuyetQuyetToanDAHTChiTietModel> listDataChaGiaiDoan)
        {
            for (var i = 0; i < lstGiaiDoan.Count(); i++)
            {
                if (item.INamBaoCaoTu == lstGiaiDoan[i].iGiaiDoanTu && item.INamBaoCaoDen == lstGiaiDoan[i].iGiaiDoanDen)
                {
                    item.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd1) : item.FKeHoachTTCPUsd1 != null ? item.FKeHoachTTCPUsd1 : null;
                    item.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd2) : item.FKeHoachTTCPUsd2 != null ? item.FKeHoachTTCPUsd2 : null;
                    item.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd3) : item.FKeHoachTTCPUsd3 != null ? item.FKeHoachTTCPUsd3 : null;
                    item.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd4) : item.FKeHoachTTCPUsd4 != null ? item.FKeHoachTTCPUsd4 : null;
                    item.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd5) : item.FKeHoachTTCPUsd5 != null ? item.FKeHoachTTCPUsd5 : null;
                    item.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd6) : item.FKeHoachTTCPUsd6 != null ? item.FKeHoachTTCPUsd6 : null;
                    item.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd7) : item.FKeHoachTTCPUsd7 != null ? item.FKeHoachTTCPUsd7 : null;
                    item.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd8) : item.FKeHoachTTCPUsd8 != null ? item.FKeHoachTTCPUsd8 : null;
                    item.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd9) : item.FKeHoachTTCPUsd9 != null ? item.FKeHoachTTCPUsd9 : null;
                    item.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.FKeHoachTTCPUsd10) : item.FKeHoachTTCPUsd10 != null ? item.FKeHoachTTCPUsd10 : null;

                    item.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd1) : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                    item.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd2) : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                    item.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd3) : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                    item.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd4) : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                    item.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd5) : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                    item.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd6) : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                    item.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd7) : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                    item.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd8) : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                    item.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd9) : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                    item.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongVnd10) : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;

                    item.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd1) : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                    item.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd2) : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                    item.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd3) : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                    item.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd4) : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                    item.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd5) : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                    item.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd6) : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                    item.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd7) : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                    item.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd8) : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                    item.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd9) : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                    item.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.FKinhPhiDuocCapTongUsd10) : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;

                    item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd1) : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                    item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd2) : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                    item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd3) : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                    item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd4) : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                    item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd5) : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                    item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd6) : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                    item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd7) : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                    item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd8) : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                    item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd9) : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                    item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongUsd10) : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;

                    item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd1) : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                    item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd2) : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                    item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd3) : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                    item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd4) : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                    item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd5) : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                    item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd6) : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                    item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd7) : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                    item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd8) : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                    item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd9) : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                    item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.FQuyetToanDuocDuyetTongVnd10) : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                }
            }

            return item;
        }
        private string ConvertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }
        private string ConvertLaMa(decimal num)
        {
            string strRet = string.Empty;
            decimal _Number = num;
            Boolean _Flag = true;
            string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int i = 0;
            while (_Flag)
            {
                while (_Number >= ArrNumber[i])
                {
                    _Number -= ArrNumber[i];
                    strRet += ArrLama[i];
                    if (_Number < 1)
                        _Flag = false;
                }
                i++;
            }
            return strRet;
        }

        //Ham tinh data tong hop
        private List<NHTHTongHop> LoadDataTongHop(List<NhTtThucHienNganSachGiaiDoanQuery> lstGiaiDoan)
        {
            if (lstGiaiDoan.Any())
            {
                var listGiaiDoan = new List<int?>();
                listGiaiDoan.AddRange(lstGiaiDoan.Where(w => w.iGiaiDoanTu != null).Select(x => x.iGiaiDoanTu - 1).ToList());
                listGiaiDoan.AddRange(lstGiaiDoan.Where(w => w.iGiaiDoanDen != null).Select(x => x.iGiaiDoanDen).ToList());

                List<NHTHTongHop> data = new List<NHTHTongHop>();
                var lstSMaNguon = new List<string> { NHConstants.MA_TH_BCTH_NS_GIAIDOAN };
                var predicate = PredicateBuilder.True<NHTHTongHop>();
                predicate = predicate.And(x => (lstSMaNguon.Contains(x.SMaNguon) || lstSMaNguon.Contains(x.SMaNguonCha)) && listGiaiDoan.Contains(x.INamKeHoach));

                return _nhThTongHopService.FindByCondition(predicate).ToList();

            }
            return null;

        }
    }
}
