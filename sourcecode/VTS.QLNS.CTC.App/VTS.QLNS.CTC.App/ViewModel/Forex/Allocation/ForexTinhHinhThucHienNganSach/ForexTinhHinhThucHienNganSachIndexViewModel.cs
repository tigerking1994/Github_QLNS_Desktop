using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.Allocation.ForexTinhHinhThucHienNganSach
{
    public class ForexTinhHinhThucHienNganSachIndexViewModel : GridViewModelBase<NhTtThucHienNganSachModel>
    {
        private IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILogger<ForexTinhHinhThucHienNganSachIndexViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmLoaiTienTeService _nhDmLoaiTienTeService;
        private readonly INhTtThucHienNganSachService _nhTtThucHienNganSachService;
        private ICollectionView _itemsCollectionView;
        private readonly INhThTongHopService _nhThTongHopService;

        public override string Name => "Tình hình thực hiện ngân sách";
        public override string Title => "Tình hình thực hiện ngân sách";
        public override string Description => "Danh sách thực hiện ngân sách";
        public override Type ContentType => typeof(View.Forex.ForexAllocation.ForexTinhHinhThucHienNganSach.ForexTinhHinhThucHienNganSachIndex);

        public ForexTinhHinhThucHienNganSachDialogViewModel ForexTinhHinhThucHienNganSachDialogViewModel { get; }
        public ForexTtTinhHinhThucHienNganSachPrintDialogViewModel ForexTtTinhHinhThucHienNganSachPrintDialogViewModel { get; }

        private ImportTabIndex _tabIndex;
        public ImportTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
                OnPropertyChanged(nameof(IsEnabledTransferButton));
                LoadData();
            }
        }
        private int _CountGiaiDoanKeHoach;
        public int CountGiaiDoanKeHoach
        {
            get => _CountGiaiDoanKeHoach;
            set => SetProperty(ref _CountGiaiDoanKeHoach, value);
        }
        private int _CountGiaiDoanKinhPhi;
        public int CountGiaiDoanKinhPhi
        {
            get => _CountGiaiDoanKinhPhi;
            set => SetProperty(ref _CountGiaiDoanKinhPhi, value);
        }
        private int _CountGiaiDoanQuyetToan;
        public int CountGiaiDoanQuyetToan
        {
            get => _CountGiaiDoanQuyetToan;
            set => SetProperty(ref _CountGiaiDoanQuyetToan, value);
        }

        private string _GiaiDoan1;
        public string GiaiDoan1
        {
            get => _GiaiDoan1;
            set => SetProperty(ref _GiaiDoan1, value);
        }
        private string _GiaiDoan2;
        public string GiaiDoan2
        {
            get => _GiaiDoan2;
            set => SetProperty(ref _GiaiDoan2, value);
        }
        private string _GiaiDoan3;
        public string GiaiDoan3
        {
            get => _GiaiDoan3;
            set => SetProperty(ref _GiaiDoan3, value);  
        }
        private string _GiaiDoan4;
        public string GiaiDoan4
        {
            get => _GiaiDoan4;
            set => SetProperty(ref _GiaiDoan4, value);
        }
        private string _GiaiDoan5;
        public string GiaiDoan5
        {
            get => _GiaiDoan5;
            set => SetProperty(ref _GiaiDoan5, value);
        }
        private string _GiaiDoan6;
        public string GiaiDoan6
        {
            get => _GiaiDoan6;
            set => SetProperty(ref _GiaiDoan6, value);
        }
        private string _GiaiDoan7;
        public string GiaiDoan7
        {
            get => _GiaiDoan7;
            set => SetProperty(ref _GiaiDoan7, value);
        }
        private string _GiaiDoan8;
        public string GiaiDoan8
        {
            get => _GiaiDoan8;
            set => SetProperty(ref _GiaiDoan8, value);
        }
        private string _GiaiDoan9;
        public string GiaiDoan9
        {
            get => _GiaiDoan9;
            set => SetProperty(ref _GiaiDoan9, value);
        }
        private string _GiaiDoan10;
        public string GiaiDoan10
        {
            get => _GiaiDoan10;
            set => SetProperty(ref _GiaiDoan10, value);
        }

        private string _GiaiDoanVND1;
        public string GiaiDoanVND1
        {
            get => _GiaiDoanVND1;
            set => SetProperty(ref _GiaiDoanVND1, value);
        }
        private string _GiaiDoanVND2;
        public string GiaiDoanVND2
        {
            get => _GiaiDoanVND2;
            set => SetProperty(ref _GiaiDoanVND2, value);
        }
        private string _GiaiDoanVND3;
        public string GiaiDoanVND3
        {
            get => _GiaiDoanVND3;
            set => SetProperty(ref _GiaiDoanVND3, value);
        }
        private string _GiaiDoanVND4;
        public string GiaiDoanVND4
        {
            get => _GiaiDoanVND4;
            set => SetProperty(ref _GiaiDoanVND4, value);
        }
        private string _GiaiDoanVND5;
        public string GiaiDoanVND5
        {
            get => _GiaiDoanVND5;
            set => SetProperty(ref _GiaiDoanVND5, value);
        }
        private string _GiaiDoanVND6;
        public string GiaiDoanVND6
        {
            get => _GiaiDoanVND6;
            set => SetProperty(ref _GiaiDoanVND6, value);
        }
        private string _GiaiDoanVND7;
        public string GiaiDoanVND7
        {
            get => _GiaiDoanVND7;
            set => SetProperty(ref _GiaiDoanVND7, value);
        }
        private string _GiaiDoanVND8;
        public string GiaiDoanVND8
        {
            get => _GiaiDoanVND8;
            set => SetProperty(ref _GiaiDoanVND8, value);
        }
        private string _GiaiDoanVND9;
        public string GiaiDoanVND9
        {
            get => _GiaiDoanVND9;
            set => SetProperty(ref _GiaiDoanVND9, value);
        }
        private string _GiaiDoanVND10;
        public string GiaiDoanVND10
        {
            get => _GiaiDoanVND10;
            set => SetProperty(ref _GiaiDoanVND10, value);
        }


        private bool _VGiaiDoan1;
        public bool VGiaiDoan1
        {
            get => _VGiaiDoan1;
            set => SetProperty(ref _VGiaiDoan1, value);
        }
        private bool _VGiaiDoan2;
        public bool VGiaiDoan2
        {
            get => _VGiaiDoan2;
            set => SetProperty(ref _VGiaiDoan2, value);
        }

        private bool _VGiaiDoan3;
        public bool VGiaiDoan3
        {
            get => _VGiaiDoan3;
            set => SetProperty(ref _VGiaiDoan3, value);
        }
        private bool _VGiaiDoan4;
        public bool VGiaiDoan4
        {
            get => _VGiaiDoan4;
            set => SetProperty(ref _VGiaiDoan4, value);
        }
        private bool _VGiaiDoan5;
        public bool VGiaiDoan5
        {
            get => _VGiaiDoan5;
            set => SetProperty(ref _VGiaiDoan5, value);
        }
        private bool _VGiaiDoan6;
        public bool VGiaiDoan6
        {
            get => _VGiaiDoan6;
            set => SetProperty(ref _VGiaiDoan6, value);
        }
        private bool _VGiaiDoan7;
        public bool VGiaiDoan7
        {
            get => _VGiaiDoan7;
            set => SetProperty(ref _VGiaiDoan7, value);
        }
        private bool _VGiaiDoan8;
        public bool VGiaiDoan8
        {
            get => _VGiaiDoan8;
            set => SetProperty(ref _VGiaiDoan8, value);
        }
        private bool _VGiaiDoan9;
        public bool VGiaiDoan9
        {
            get => _VGiaiDoan9;
            set => SetProperty(ref _VGiaiDoan9, value);
        }
        private bool _VGiaiDoan10;
        public bool VGiaiDoan10
        {
            get => _VGiaiDoan10;
            set => SetProperty(ref _VGiaiDoan10, value);
        }
        
        public List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoanTTCP { get; set; }
        public bool IsEnabledTransferButton => TabIndex == ImportTabIndex.MLNS;

        private ObservableCollection<ComboboxItem> _itemsQuy;
        public ObservableCollection<ComboboxItem> ItemsQuy
        {
            get => _itemsQuy;
            set => SetProperty(ref _itemsQuy, value);
        }

        private ComboboxItem _selectedQuy;
        public ComboboxItem SelectedQuy
        {
            get => _selectedQuy;
            set
            {
                SetProperty(ref _selectedQuy, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNam;
        public ObservableCollection<ComboboxItem> ItemsNam
        {
            get => _itemsNam;
            set => SetProperty(ref _itemsNam, value);
        }

        private ComboboxItem _selectedNam;
        public ComboboxItem SelectedNam
        {
            get => _selectedNam;
            set
            {
                SetProperty(ref _selectedNam, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsTuNam;
        public ObservableCollection<ComboboxItem> ItemsTuNam
        {
            get => _itemsTuNam;
            set => SetProperty(ref _itemsTuNam, value);
        }

        private ComboboxItem _selectedTuNam;
        public ComboboxItem SelectedTuNam
        {
            get => _selectedTuNam;
            set
            {
                SetProperty(ref _selectedTuNam, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDenNam;
        public ObservableCollection<ComboboxItem> ItemsDenNam
        {
            get => _itemsDenNam;
            set => SetProperty(ref _itemsDenNam, value);
        }

        private ComboboxItem _selectedDenNam;
        public ComboboxItem SelectedDenNam
        {
            get => _selectedDenNam;
            set
            {
                SetProperty(ref _selectedDenNam, value);
            }
        }

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
            set => SetProperty(ref _selectedDonVi, value);
        }

        private NhTtThucHienNganSachModel _itemsFilter;
        public NhTtThucHienNganSachModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }
        public RelayCommand IndexCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }

        public ForexTinhHinhThucHienNganSachIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<ForexTinhHinhThucHienNganSachIndexViewModel> logger,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiTienTeService nhDmLoaiTienTeService,
            INhTtThucHienNganSachService nhTtThucHienNganSachService,
            INhThTongHopService nhThTongHopService,
            ForexTinhHinhThucHienNganSachDialogViewModel forexTinhHinhThucHienNganSachDialogViewModel,
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel forexTtTinhHinhThucHienNganSachPrintDialogViewModel)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiTienTeService = nhDmLoaiTienTeService;
            _nhTtThucHienNganSachService = nhTtThucHienNganSachService;
            _nhThTongHopService = nhThTongHopService;
            ForexTinhHinhThucHienNganSachDialogViewModel = forexTinhHinhThucHienNganSachDialogViewModel;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel = forexTtTinhHinhThucHienNganSachPrintDialogViewModel;

            IndexCommand = new RelayCommand(obj => LoadDataFillterIndex());
            SearchCommand = new RelayCommand(obj => LoadDataFillter());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            PrintReportCommand = new RelayCommand(obj => OnShowPrintReportDialog());
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadQuy();
            LoadNam();
            LoadDonVi();
            SelectedQuy = ItemsQuy.FirstOrDefault(x => x.ValueItem == "1");
            SelectedNam = ItemsNam.FirstOrDefault(x => x.ValueItem == _sessionService.Current.YearOfWork.ToString());
            SelectedTuNam = ItemsTuNam.FirstOrDefault(x => x.ValueItem == _sessionService.Current.YearOfWork.ToString());
            SelectedDenNam = ItemsDenNam.FirstOrDefault(x => x.ValueItem == _sessionService.Current.YearOfWork.ToString());
            OnPropertyChanged(nameof(SelectedQuy));
            OnPropertyChanged(nameof(SelectedNam));
            OnPropertyChanged(nameof(SelectedTuNam));
            OnPropertyChanged(nameof(SelectedDenNam));
            LoadData();
        }
        private void LoadDefault()
        {
            ItemsFilter = new NhTtThucHienNganSachModel();
        }
        private void OnRemoveFilter()
        {
            ItemsFilter = new NhTtThucHienNganSachModel();
            SelectedQuy = null;
            SelectedNam = null;
            SelectedDonVi = null;
            LoadData();
        }
        private void LoadQuy()
        {
            var loaiQuys = new ObservableCollection<ComboboxItem>();
            loaiQuys.Add(new ComboboxItem("Quý 1", "1"));
            loaiQuys.Add(new ComboboxItem("Quý 2", "2"));
            loaiQuys.Add(new ComboboxItem("Quý 3", "3"));
            loaiQuys.Add(new ComboboxItem("Quý 4", "4"));
            _itemsQuy = loaiQuys;
        }
        private void LoadNam()
        {
            var loaiNams = new ObservableCollection<ComboboxItem>();
            int namHienTai = _sessionService.Current.YearOfWork + 1;
            for (int i = 20; i > 0; i--)
            {
                namHienTai -= 1;
                loaiNams.Add(new ComboboxItem("Năm "+ namHienTai, ""+namHienTai));
            }
            _itemsNam = loaiNams;
            _itemsTuNam = loaiNams;
            _itemsDenNam = loaiNams;
        }
        private void LoadDonVi()
        {
            int year = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == year);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        
        public bool TabIndexGiaiDoan { get; set; }

        public bool TabIndexQuy { get; set; }

        public class Person
        {
            public ObservableCollection<Activity> Hobbys { get; set; }
            public string Name { get; set; }
        }
        public class Activity
        {
            public string Name { get; set; }
        }
        void LoadDataFillterIndex(params object[] args)
        {
            LoadDataFillter();
        }
        void LoadDataFillter(params object[] args)
        {
            try
            {
                var index = TabIndex == ImportTabIndex.Data ? 1 : 0;
                List<NhTtThucHienNganSachModel> list = _mapper.Map<ObservableCollection<NhTtThucHienNganSachModel>>(
                        _nhTtThucHienNganSachService.FindAllData(
                            index, SelectedQuy != null ? Convert.ToInt32(SelectedQuy.ValueItem) : -1, SelectedNam != null ? Convert.ToInt32(SelectedNam.ValueItem) : -1,
                            SelectedTuNam != null ? Convert.ToInt32(SelectedTuNam.ValueItem) : -1, SelectedDenNam != null ? Convert.ToInt32(SelectedDenNam.ValueItem) : -1,
                            SelectedDonVi != null ? Guid.Parse(SelectedDonVi.Id.ToString()) : Guid.Empty
                            )).ToList();
                //if (TabIndex == ImportTabIndex.MLNS)
                //{
                //    if (SelectedQuy != null)
                //    {
                //        list = list.Where(x => x.dNgayDeNghi.Month >= ((Convert.ToInt32(SelectedQuy.ValueItem) - 1) * 3 + 1) && x.dNgayDeNghi.Month <= (Convert.ToInt32(SelectedQuy.ValueItem) * 3)).ToList();
                //    }
                //    if (SelectedNam != null)
                //    {
                //        list = list.Where(x => x.dNgayDeNghi.Year == Convert.ToInt32(SelectedNam.ValueItem)).ToList();
                //    }
                //}
                //else
                //{
                //    if (SelectedTuNam != null)
                //    {
                //        list = list.Where(x => x.dNgayDeNghi.Year >= Convert.ToInt32(SelectedTuNam.ValueItem)).ToList();
                //    }
                //    if (SelectedDenNam != null)
                //    {
                //        list = list.Where(x => x.dNgayDeNghi.Year <= Convert.ToInt32(SelectedDenNam.ValueItem)).ToList();
                //    }
                //}
                
                //if (SelectedDonVi != null)
                //{
                //    list = list.Where(x => x.iID_DonVi == SelectedDonVi.Id).ToList();
                //}
                List<NhTtThucHienNganSachModel> getlistGiaiDoan = list.Where(x => x.iGiaiDoanTu != null && x.iGiaiDoanDen != null).ToList();
                List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan = getlistGiaiDoan
                    .GroupBy(x => (x.iGiaiDoanTu, x.iGiaiDoanDen)).Select(x => x.First())
                    .Select(x => new NhTtThucHienNganSachGiaiDoanModel
                    {
                        sGiaiDoan = "Giai đoạn " + x.iGiaiDoanTu + " - " + x.iGiaiDoanDen,
                        iGiaiDoanTu = x.iGiaiDoanTu,
                        iGiaiDoanDen = x.iGiaiDoanDen
                    }).ToList();
                _CountGiaiDoanKeHoach = lstGiaiDoan.Count() + 1;
                _CountGiaiDoanKinhPhi = (lstGiaiDoan.Count() + 1) * 2;
                _CountGiaiDoanQuyetToan = (lstGiaiDoan.Count() + 1) * 2;

                GiaiDoan1 = null;
                GiaiDoan2 = null;
                GiaiDoan3 = null;
                GiaiDoan4 = null;
                GiaiDoan5 = null;
                GiaiDoan6 = null;
                GiaiDoan7 = null;
                GiaiDoan8 = null;
                GiaiDoan9 = null;
                GiaiDoan10 = null;
                GiaiDoanVND1 = null;
                GiaiDoanVND2 = null;
                GiaiDoanVND3 = null;
                GiaiDoanVND4 = null;
                GiaiDoanVND5 = null;
                GiaiDoanVND6 = null;
                GiaiDoanVND7 = null;
                GiaiDoanVND8 = null;
                GiaiDoanVND9 = null;
                GiaiDoanVND10 = null;

                for (var i = 0; i < lstGiaiDoan.Count(); i++)
                {
                    GiaiDoan1 = i == 0 ? lstGiaiDoan[0].sGiaiDoan.ToString() + " (USD)" : GiaiDoan1 != null ? GiaiDoan1 : null;
                    GiaiDoan2 = i == 1 ? lstGiaiDoan[1].sGiaiDoan.ToString() + " (USD)" : GiaiDoan2 != null ? GiaiDoan2 : null;
                    GiaiDoan3 = i == 2 ? lstGiaiDoan[2].sGiaiDoan.ToString() + " (USD)" : GiaiDoan3 != null ? GiaiDoan3 : null;
                    GiaiDoan4 = i == 3 ? lstGiaiDoan[3].sGiaiDoan.ToString() + " (USD)" : GiaiDoan4 != null ? GiaiDoan4 : null;
                    GiaiDoan5 = i == 4 ? lstGiaiDoan[4].sGiaiDoan.ToString() + " (USD)" : GiaiDoan5 != null ? GiaiDoan5 : null;
                    GiaiDoan6 = i == 5 ? lstGiaiDoan[5].sGiaiDoan.ToString() + " (USD)" : GiaiDoan6 != null ? GiaiDoan6 : null;
                    GiaiDoan7 = i == 6 ? lstGiaiDoan[6].sGiaiDoan.ToString() + " (USD)" : GiaiDoan7 != null ? GiaiDoan7 : null;
                    GiaiDoan8 = i == 7 ? lstGiaiDoan[7].sGiaiDoan.ToString() + " (USD)" : GiaiDoan8 != null ? GiaiDoan8 : null;
                    GiaiDoan9 = i == 8 ? lstGiaiDoan[8].sGiaiDoan.ToString() + " (USD)" : GiaiDoan9 != null ? GiaiDoan9 : null;
                    GiaiDoan10 = i == 9 ? lstGiaiDoan[9].sGiaiDoan.ToString() + " (USD)" : GiaiDoan10 != null ? GiaiDoan10 : null;

                    GiaiDoanVND1 = i == 0 ? lstGiaiDoan[0].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND1 != null ? GiaiDoanVND1 : null;
                    GiaiDoanVND2 = i == 1 ? lstGiaiDoan[1].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND2 != null ? GiaiDoanVND2 : null;
                    GiaiDoanVND3 = i == 2 ? lstGiaiDoan[2].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND3 != null ? GiaiDoanVND3 : null;
                    GiaiDoanVND4 = i == 3 ? lstGiaiDoan[3].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND4 != null ? GiaiDoanVND4 : null;
                    GiaiDoanVND5 = i == 4 ? lstGiaiDoan[4].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND5 != null ? GiaiDoanVND5 : null;
                    GiaiDoanVND6 = i == 5 ? lstGiaiDoan[5].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND6 != null ? GiaiDoanVND6 : null;
                    GiaiDoanVND7 = i == 6 ? lstGiaiDoan[6].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND7 != null ? GiaiDoanVND7 : null;
                    GiaiDoanVND8 = i == 7 ? lstGiaiDoan[7].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND8 != null ? GiaiDoanVND8 : null;
                    GiaiDoanVND9 = i == 8 ? lstGiaiDoan[8].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND9 != null ? GiaiDoanVND9 : null;
                    GiaiDoanVND10 = i == 9 ? lstGiaiDoan[9].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND10 != null ? GiaiDoanVND10 : null;
                }
                
                OnPropertyChanged(nameof(_CountGiaiDoanKeHoach));
                OnPropertyChanged(nameof(_CountGiaiDoanKinhPhi));
                OnPropertyChanged(nameof(_CountGiaiDoanQuyetToan));
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

                        
                _VGiaiDoan1 = GiaiDoan1 != null;
                _VGiaiDoan2 = GiaiDoan2 != null;
                _VGiaiDoan3 = GiaiDoan3 != null;
                _VGiaiDoan4 = GiaiDoan4 != null;
                _VGiaiDoan5 = GiaiDoan5 != null;
                _VGiaiDoan6 = GiaiDoan6 != null;
                _VGiaiDoan7 = GiaiDoan7 != null;
                _VGiaiDoan8 = GiaiDoan8 != null;
                _VGiaiDoan9 = GiaiDoan9 != null;
                _VGiaiDoan10 = GiaiDoan10 != null;

                OnPropertyChanged(nameof(_VGiaiDoan1));
                OnPropertyChanged(nameof(_VGiaiDoan2));
                OnPropertyChanged(nameof(_VGiaiDoan3));
                OnPropertyChanged(nameof(_VGiaiDoan4));
                OnPropertyChanged(nameof(_VGiaiDoan5));
                OnPropertyChanged(nameof(_VGiaiDoan6));
                OnPropertyChanged(nameof(_VGiaiDoan7));
                OnPropertyChanged(nameof(_VGiaiDoan8));
                OnPropertyChanged(nameof(_VGiaiDoan9));
                OnPropertyChanged(nameof(_VGiaiDoan10));

                List<NhTtThucHienNganSachModel> listData = getList(list, lstGiaiDoan);
                Items = _mapper.Map<ObservableCollection<NhTtThucHienNganSachModel>>(listData);

                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var a = ex;
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                var index = TabIndex == ImportTabIndex.Data ? 1 : 0;
                List<NhTtThucHienNganSachModel> list = _mapper.Map<ObservableCollection<NhTtThucHienNganSachModel>>(_nhTtThucHienNganSachService.FindAllData(index, 1, DateTime.Now.Year, DateTime.Now.Year, DateTime.Now.Year, null)).ToList();
                List<NhTtThucHienNganSachModel> getlistGiaiDoan = list.Where(x => x.iGiaiDoanTu != null && x.iGiaiDoanDen != null).ToList();
                List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan = getlistGiaiDoan
                    .GroupBy(x => (x.iGiaiDoanTu, x.iGiaiDoanDen)).Select(x => x.First())
                    .Select(x => new NhTtThucHienNganSachGiaiDoanModel
                    {
                        sGiaiDoan = "Giai đoạn " + x.iGiaiDoanTu + " - " + x.iGiaiDoanDen,
                        iGiaiDoanTu = x.iGiaiDoanTu,
                        iGiaiDoanDen = x.iGiaiDoanDen
                    }).ToList();
                CountGiaiDoanKeHoach = lstGiaiDoan.Count() + 1;
                CountGiaiDoanKinhPhi = (lstGiaiDoan.Count() + 1) * 2;
                CountGiaiDoanQuyetToan = (lstGiaiDoan.Count() + 1) * 2;


                for (var i = 0; i < lstGiaiDoan.Count(); i++)
                {
                    GiaiDoan1 = i == 0 ? lstGiaiDoan[0].sGiaiDoan.ToString() + " (USD)" : GiaiDoan1 != null ? GiaiDoan1 : null;
                    GiaiDoan2 = i == 1 ? lstGiaiDoan[1].sGiaiDoan.ToString() + " (USD)" : GiaiDoan2 != null ? GiaiDoan2 : null;
                    GiaiDoan3 = i == 2 ? lstGiaiDoan[2].sGiaiDoan.ToString() + " (USD)" : GiaiDoan3 != null ? GiaiDoan3 : null;
                    GiaiDoan4 = i == 3 ? lstGiaiDoan[3].sGiaiDoan.ToString() + " (USD)" : GiaiDoan4 != null ? GiaiDoan4 : null;
                    GiaiDoan5 = i == 4 ? lstGiaiDoan[4].sGiaiDoan.ToString() + " (USD)" : GiaiDoan5 != null ? GiaiDoan5 : null;
                    GiaiDoan6 = i == 5 ? lstGiaiDoan[5].sGiaiDoan.ToString() + " (USD)" : GiaiDoan6 != null ? GiaiDoan6 : null;
                    GiaiDoan7 = i == 6 ? lstGiaiDoan[6].sGiaiDoan.ToString() + " (USD)" : GiaiDoan7 != null ? GiaiDoan7 : null;
                    GiaiDoan8 = i == 7 ? lstGiaiDoan[7].sGiaiDoan.ToString() + " (USD)" : GiaiDoan8 != null ? GiaiDoan8 : null;
                    GiaiDoan9 = i == 8 ? lstGiaiDoan[8].sGiaiDoan.ToString() + " (USD)" : GiaiDoan9 != null ? GiaiDoan9 : null;
                    GiaiDoan10 = i == 9 ? lstGiaiDoan[9].sGiaiDoan.ToString() + " (USD)" : GiaiDoan10 != null ? GiaiDoan10 : null;

                    GiaiDoanVND1 = i == 0 ? lstGiaiDoan[0].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND1 != null ? GiaiDoanVND1 : null;
                    GiaiDoanVND2 = i == 1 ? lstGiaiDoan[1].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND2 != null ? GiaiDoanVND2 : null;
                    GiaiDoanVND3 = i == 2 ? lstGiaiDoan[2].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND3 != null ? GiaiDoanVND3 : null;
                    GiaiDoanVND4 = i == 3 ? lstGiaiDoan[3].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND4 != null ? GiaiDoanVND4 : null;
                    GiaiDoanVND5 = i == 4 ? lstGiaiDoan[4].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND5 != null ? GiaiDoanVND5 : null;
                    GiaiDoanVND6 = i == 5 ? lstGiaiDoan[5].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND6 != null ? GiaiDoanVND6 : null;
                    GiaiDoanVND7 = i == 6 ? lstGiaiDoan[6].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND7 != null ? GiaiDoanVND7 : null;
                    GiaiDoanVND8 = i == 7 ? lstGiaiDoan[7].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND8 != null ? GiaiDoanVND8 : null;
                    GiaiDoanVND9 = i == 8 ? lstGiaiDoan[8].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND9 != null ? GiaiDoanVND9 : null;
                    GiaiDoanVND10 = i == 9 ? lstGiaiDoan[9].sGiaiDoan.ToString() + " (VND)" : GiaiDoanVND10 != null ? GiaiDoanVND10 : null;
                }



                IsLoading = true;
                Items = new ObservableCollection<NhTtThucHienNganSachModel>();

                List<NhTtThucHienNganSachModel> listData = getList(list, lstGiaiDoan);
                Items = _mapper.Map<ObservableCollection<NhTtThucHienNganSachModel>>(listData);


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

                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                //_itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                //_itemsCollectionView.Filter = Items_Filter;

            }
            catch (Exception ex)
            {
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhTtThucHienNganSachModel item)
            {
                bool reslut = true;
                if (SelectedQuy != null)
                {
                    if(((Convert.ToInt32(SelectedQuy.ValueItem)-1)*3 + 1) <= item.dNgayDeNghi.Month && item.dNgayDeNghi.Month <= (Convert.ToInt32(SelectedQuy.ValueItem) * 3))
                    {
                        reslut = true;
                    }
                    else
                    {
                        reslut = false;
                    }
                }
                if (SelectedNam != null)
                {
                    reslut = item.dNgayDeNghi.Year >= Convert.ToInt32(SelectedNam.ValueItem) ? true : false;
                }
                if (SelectedDenNam != null)
                {
                    reslut = item.dNgayDeNghi.Year <= Convert.ToInt32(SelectedDenNam.ValueItem) ? true : false;
                }
                if (SelectedDonVi != null)
                {
                    reslut &= item.iID_DonVi == SelectedDonVi.Id;
                }
                return reslut;
            }
            return false;
        }

        //protected override void OnAdd()
        //{
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Model = new NhThucHienNganSachModel();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.SavedAction = obj => this.OnRefresh();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Init();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.ShowDialog();
        //}

        //protected override void OnSelectionDoubleClick(object obj)
        //{
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Model = SelectedItem;
        //    ForexTinhHinhThucHienNganSachDialogViewModel.IsDetail = true;
        //    ForexTinhHinhThucHienNganSachDialogViewModel.SavedAction = obj => this.OnRefresh();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Init();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.ShowDialog();
        //}

        //protected override void OnUpdate()
        //{
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Model = SelectedItem;
        //    ForexTinhHinhThucHienNganSachDialogViewModel.IsDetail = false;
        //    ForexTinhHinhThucHienNganSachDialogViewModel.SavedAction = obj => this.OnRefresh();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.Init();
        //    ForexTinhHinhThucHienNganSachDialogViewModel.ShowDialog();
        //}

        //protected override void OnDelete()
        //{
        //    string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
        //    if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
        //    {
        //        var entity = _mapper.Map<NhThucHienNganSachModel>(SelectedItem);
        //        _nhTtThongTriCapPhatService.Delete(entity);
        //        OnRefresh();
        //    }
        //}

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnShowPrintReportDialog()
        {
            var index = TabIndex == ImportTabIndex.Data? 1 : 0;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.tabindex = Convert.ToInt32(index);
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.iQuyPrint = SelectedQuy != null ? Convert.ToInt32(SelectedQuy.ValueItem) : -2;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.iNamPrint = SelectedNam != null ? Convert.ToInt32(SelectedNam.ValueItem) : -2;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.iTuNamPrint = SelectedTuNam != null ? Convert.ToInt32(SelectedTuNam.ValueItem) : -2;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.iDenNamPrint = SelectedDenNam != null ? Convert.ToInt32(SelectedDenNam.ValueItem) : -2;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.iDonVi = SelectedDonVi != null ? SelectedDonVi.Id : Guid.Empty;
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.Init();
            ForexTtTinhHinhThucHienNganSachPrintDialogViewModel.ShowDialogHost();
        }

        public List<NhTtThucHienNganSachModel> getList(List<NhTtThucHienNganSachModel> list, List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan)
        {
            var tabTable = TabIndex == ImportTabIndex.Data ? 1 : 0;
            List<NhTtThucHienNganSachModel> listData = new List<NhTtThucHienNganSachModel>().ToList();
            int SttLoai = 0;
            int SttHopDong = 0;
            int SttDuAn = 0;
            int SttChuongTrinh = 0;
            Guid? idDuAn = null;
            Guid? idHopDong = null;
            Guid? idChuongTrinh = null;
            int? idLoai = null;
            int sttTong = 0;
            List<NhTtThucHienNganSachModel> listTong = list;
            NhTtThucHienNganSachModel DataTong = new NhTtThucHienNganSachModel();

            // Tính lại data bảng TH_TongHop
            var dataTongHops = GetDataNHTongHop(list.Select(x => x.IDNhiemVuChi).ToList());
            //

            DataTong.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataTong.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataTong.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

            DataTong.HopDongUSD = listTong.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongUSD);
            DataTong.HopDongVND = listTong.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongVND);

            DataTong.NCVTTCP = listTong.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP);
            DataTong.NhiemVuChi = listTong.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NhiemVuChi);
            //DataTong.KinhPhiUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
            //DataTong.KinhPhiVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
            DataTong.KinhPhiUSD = dataTongHops.Sum(x => x.KinhPhiUSD);//data col5
            DataTong.KinhPhiVND = dataTongHops.Sum(x => x.KinhPhiVND);

            //DataTong.KinhPhiToYUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
            //DataTong.KinhPhiToYVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
            DataTong.KinhPhiToYUSD = dataTongHops.Sum(x => x.KinhPhiToYUSD);//data col11
            DataTong.KinhPhiToYVND = dataTongHops.Sum(x => x.KinhPhiToYVND);

            //DataTong.KinhPhiDaChiUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
            //DataTong.KinhPhiDaChiVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);col 15
            DataTong.KinhPhiDaChiUSD = dataTongHops.Sum(x => x.KinhPhiDaChiUSD);//data col15
            DataTong.KinhPhiDaChiVND = dataTongHops.Sum(x => x.KinhPhiDaChiVND);

            //DataTong.KinhPhiDaChiToYUSD = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
            //DataTong.KinhPhiDaChiToYVND = listTong.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);          
            DataTong.KinhPhiDaChiToYUSD = dataTongHops.Sum(x => x.KinhPhiDaChiToYUSD);//data col17
            DataTong.KinhPhiDaChiToYVND = dataTongHops.Sum(x => x.KinhPhiDaChiToYVND);
            DataTong.TongKinhPhiUSD = DataTong.KinhPhiUSD + DataTong.KinhPhiToYUSD;
            DataTong.TongKinhPhiVND = DataTong.KinhPhiVND + DataTong.KinhPhiToYVND;

            DataTong.TongKinhPhiDaChiUSD = DataTong.KinhPhiDaChiUSD + DataTong.KinhPhiDaChiToYUSD;
            DataTong.TongKinhPhiDaChiVND = DataTong.KinhPhiDaChiVND + DataTong.KinhPhiDaChiToYVND;

            DataTong.KinhPhiDuocCapChuaChiUSD = DataTong.TongKinhPhiUSD - DataTong.TongKinhPhiDaChiUSD;
            DataTong.KinhPhiDuocCapChuaChiVND = DataTong.TongKinhPhiVND - DataTong.TongKinhPhiDaChiVND;
            DataTong.QuyGiaiNganTheoQuy = DataTong.NhiemVuChi - DataTong.TongKinhPhiUSD;
            DataTong.IsHangCha = true;
            if (lstGiaiDoan != null)
            {
                for (var i = 0; i < lstGiaiDoan.Count(); i++)
                {
                    List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listTong.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                    DataTong.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd1 != null ? DataTong.FKeHoachTTCPUsd1 : null;
                    DataTong.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd2 != null ? DataTong.FKeHoachTTCPUsd2 : null;
                    DataTong.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd3 != null ? DataTong.FKeHoachTTCPUsd3 : null;
                    DataTong.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd4 != null ? DataTong.FKeHoachTTCPUsd4 : null;
                    DataTong.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd5 != null ? DataTong.FKeHoachTTCPUsd5 : null;
                    DataTong.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd6 != null ? DataTong.FKeHoachTTCPUsd6 : null;
                    DataTong.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd7 != null ? DataTong.FKeHoachTTCPUsd7 : null;
                    DataTong.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd8 != null ? DataTong.FKeHoachTTCPUsd8 : null;
                    DataTong.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd9 != null ? DataTong.FKeHoachTTCPUsd9 : null;
                    DataTong.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataTong.FKeHoachTTCPUsd10 != null ? DataTong.FKeHoachTTCPUsd10 : null;

                    DataTong.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd1 != null ? DataTong.FKinhPhiDuocCapTongUsd1 : null;
                    DataTong.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd2 != null ? DataTong.FKinhPhiDuocCapTongUsd2 : null;
                    DataTong.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd3 != null ? DataTong.FKinhPhiDuocCapTongUsd3 : null;
                    DataTong.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd4 != null ? DataTong.FKinhPhiDuocCapTongUsd4 : null;
                    DataTong.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd5 != null ? DataTong.FKinhPhiDuocCapTongUsd5 : null;
                    DataTong.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd6 != null ? DataTong.FKinhPhiDuocCapTongUsd6 : null;
                    DataTong.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd7 != null ? DataTong.FKinhPhiDuocCapTongUsd7 : null;
                    DataTong.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd8 != null ? DataTong.FKinhPhiDuocCapTongUsd8 : null;
                    DataTong.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd9 != null ? DataTong.FKinhPhiDuocCapTongUsd9 : null;
                    DataTong.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataTong.FKinhPhiDuocCapTongUsd10 != null ? DataTong.FKinhPhiDuocCapTongUsd10 : null;

                    DataTong.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd1 != null ? DataTong.FKinhPhiDuocCapTongVnd1 : null;
                    DataTong.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd2 != null ? DataTong.FKinhPhiDuocCapTongVnd2 : null;
                    DataTong.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd3 != null ? DataTong.FKinhPhiDuocCapTongVnd3 : null;
                    DataTong.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd4 != null ? DataTong.FKinhPhiDuocCapTongVnd4 : null;
                    DataTong.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd5 != null ? DataTong.FKinhPhiDuocCapTongVnd5 : null;
                    DataTong.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd6 != null ? DataTong.FKinhPhiDuocCapTongVnd6 : null;
                    DataTong.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd7 != null ? DataTong.FKinhPhiDuocCapTongVnd7 : null;
                    DataTong.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd8 != null ? DataTong.FKinhPhiDuocCapTongVnd8 : null;
                    DataTong.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd9 != null ? DataTong.FKinhPhiDuocCapTongVnd9 : null;
                    DataTong.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataTong.FKinhPhiDuocCapTongVnd10 != null ? DataTong.FKinhPhiDuocCapTongVnd10 : null;

                    DataTong.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd1 != null ? DataTong.FQuyetToanDuocDuyetTongUsd1 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd2 != null ? DataTong.FQuyetToanDuocDuyetTongUsd2 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd3 != null ? DataTong.FQuyetToanDuocDuyetTongUsd3 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd4 != null ? DataTong.FQuyetToanDuocDuyetTongUsd4 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd5 != null ? DataTong.FQuyetToanDuocDuyetTongUsd5 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd6 != null ? DataTong.FQuyetToanDuocDuyetTongUsd6 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd7 != null ? DataTong.FQuyetToanDuocDuyetTongUsd7 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd8 != null ? DataTong.FQuyetToanDuocDuyetTongUsd8 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd9 != null ? DataTong.FQuyetToanDuocDuyetTongUsd9 : null;
                    DataTong.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataTong.FQuyetToanDuocDuyetTongUsd10 != null ? DataTong.FQuyetToanDuocDuyetTongUsd10 : null;

                    DataTong.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd1 != null ? DataTong.FQuyetToanDuocDuyetTongVnd1 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd2 != null ? DataTong.FQuyetToanDuocDuyetTongVnd2 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd3 != null ? DataTong.FQuyetToanDuocDuyetTongVnd3 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd4 != null ? DataTong.FQuyetToanDuocDuyetTongVnd4 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd5 != null ? DataTong.FQuyetToanDuocDuyetTongVnd5 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd6 != null ? DataTong.FQuyetToanDuocDuyetTongVnd6 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd7 != null ? DataTong.FQuyetToanDuocDuyetTongVnd7 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd8 != null ? DataTong.FQuyetToanDuocDuyetTongVnd8 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd9 != null ? DataTong.FQuyetToanDuocDuyetTongVnd9 : null;
                    DataTong.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataTong.FQuyetToanDuocDuyetTongVnd10 != null ? DataTong.FQuyetToanDuocDuyetTongVnd10 : null;
                }
            }
          
            if (list != null)
            {
                foreach (var item in list)
                {
                    sttTong++;
                    item.TongKinhPhiUSD = item.KinhPhiUSD + item.KinhPhiToYUSD;
                    item.TongKinhPhiVND = item.KinhPhiVND + item.KinhPhiToYVND;

                    item.TongKinhPhiDaChiUSD = item.KinhPhiDaChiUSD + item.KinhPhiDaChiToYUSD;
                    item.TongKinhPhiDaChiVND = item.KinhPhiDaChiVND + item.KinhPhiDaChiToYVND;

                    item.KinhPhiDuocCapChuaChiUSD = item.TongKinhPhiUSD - item.TongKinhPhiDaChiUSD;
                    item.KinhPhiDuocCapChuaChiVND = item.TongKinhPhiVND - item.TongKinhPhiDaChiVND;
                    item.QuyGiaiNganTheoQuy = item.NhiemVuChi - item.TongKinhPhiUSD;

                    item.KinhPhiChuaQuyetToanUSD = item.fLuyKeKinhPhiDuocCap_USD - item.fDeNghiQTNamNay_USD;
                    item.KinhPhiChuaQuyetToanVND = item.fLuyKeKinhPhiDuocCap_VND - item.fDeNghiQTNamNay_VND;
                    item.KeHoachGiaiNgan = item.NCVTTCP - item.fLuyKeKinhPhiDuocCap_USD;

                    if (lstGiaiDoan != null)
                    {
                        item.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        item.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        item.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        foreach (var giaiDoan in lstGiaiDoan)
                        {

                            if (item.iGiaiDoanTu == giaiDoan.iGiaiDoanTu && item.iGiaiDoanDen == giaiDoan.iGiaiDoanDen)
                            {

                                item.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.NCVTTCP });
                                item.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.fLuyKeKinhPhiDuocCap_USD, valueVND = item.fLuyKeKinhPhiDuocCap_VND });
                                item.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = item.fDeNghiQTNamNay_USD, valueVND = item.fDeNghiQTNamNay_VND });
                            }
                            else
                            {
                                item.lstGiaiDoanTTCP.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0 });
                                item.lstGiaiDoanKinhPhiDuocCap.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                                item.lstGiaiDoanKinhPhiDaGiaiNgan.Add(new NhTtThucHienNganSachGiaiDoanModel() { valueUSD = 0, valueVND = 0 });
                            }
                        }
                    }

                    if (lstGiaiDoan != null)
                    {
                        for (var i = 0; i < lstGiaiDoan.Count(); i++)
                        {
                            if (item.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && item.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen)
                            {
                                //item.FKeHoachTTCPUsd1 = i == 0 ? item.NCVTTCP : item.FKeHoachTTCPUsd1 != null ? item.FKeHoachTTCPUsd1 : null;
                                //item.FKeHoachTTCPUsd2 = i == 1 ? item.NCVTTCP : item.FKeHoachTTCPUsd2 != null ? item.FKeHoachTTCPUsd2 : null;
                                //item.FKeHoachTTCPUsd3 = i == 2 ? item.NCVTTCP : item.FKeHoachTTCPUsd3 != null ? item.FKeHoachTTCPUsd3 : null;
                                //item.FKeHoachTTCPUsd4 = i == 3 ? item.NCVTTCP : item.FKeHoachTTCPUsd4 != null ? item.FKeHoachTTCPUsd4 : null;
                                //item.FKeHoachTTCPUsd5 = i == 4 ? item.NCVTTCP : item.FKeHoachTTCPUsd5 != null ? item.FKeHoachTTCPUsd5 : null;
                                //item.FKeHoachTTCPUsd6 = i == 5 ? item.NCVTTCP : item.FKeHoachTTCPUsd6 != null ? item.FKeHoachTTCPUsd6 : null;
                                //item.FKeHoachTTCPUsd7 = i == 6 ? item.NCVTTCP : item.FKeHoachTTCPUsd7 != null ? item.FKeHoachTTCPUsd7 : null;
                                //item.FKeHoachTTCPUsd8 = i == 7 ? item.NCVTTCP : item.FKeHoachTTCPUsd8 != null ? item.FKeHoachTTCPUsd8 : null;
                                //item.FKeHoachTTCPUsd9 = i == 8 ? item.NCVTTCP : item.FKeHoachTTCPUsd9 != null ? item.FKeHoachTTCPUsd9 : null;
                                //item.FKeHoachTTCPUsd10 = i == 9 ? item.NCVTTCP : item.FKeHoachTTCPUsd10 != null ? item.FKeHoachTTCPUsd10 : null;

                                item.FKinhPhiDuocCapTongUsd1 = i == 0 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd1 != null ? item.FKinhPhiDuocCapTongUsd1 : null;
                                item.FKinhPhiDuocCapTongUsd2 = i == 1 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd2 != null ? item.FKinhPhiDuocCapTongUsd2 : null;
                                item.FKinhPhiDuocCapTongUsd3 = i == 2 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd3 != null ? item.FKinhPhiDuocCapTongUsd3 : null;
                                item.FKinhPhiDuocCapTongUsd4 = i == 3 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd4 != null ? item.FKinhPhiDuocCapTongUsd4 : null;
                                item.FKinhPhiDuocCapTongUsd5 = i == 4 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd5 != null ? item.FKinhPhiDuocCapTongUsd5 : null;
                                item.FKinhPhiDuocCapTongUsd6 = i == 5 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd6 != null ? item.FKinhPhiDuocCapTongUsd6 : null;
                                item.FKinhPhiDuocCapTongUsd7 = i == 6 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd7 != null ? item.FKinhPhiDuocCapTongUsd7 : null;
                                item.FKinhPhiDuocCapTongUsd8 = i == 7 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd8 != null ? item.FKinhPhiDuocCapTongUsd8 : null;
                                item.FKinhPhiDuocCapTongUsd9 = i == 8 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd9 != null ? item.FKinhPhiDuocCapTongUsd9 : null;
                                item.FKinhPhiDuocCapTongUsd10 = i == 9 ? item.fLuyKeKinhPhiDuocCap_USD : item.FKinhPhiDuocCapTongUsd10 != null ? item.FKinhPhiDuocCapTongUsd10 : null;
                                                                                                      
                                item.FKinhPhiDuocCapTongVnd1 = i == 0 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd1 != null ? item.FKinhPhiDuocCapTongVnd1 : null;
                                item.FKinhPhiDuocCapTongVnd2 = i == 1 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd2 != null ? item.FKinhPhiDuocCapTongVnd2 : null;
                                item.FKinhPhiDuocCapTongVnd3 = i == 2 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd3 != null ? item.FKinhPhiDuocCapTongVnd3 : null;
                                item.FKinhPhiDuocCapTongVnd4 = i == 3 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd4 != null ? item.FKinhPhiDuocCapTongVnd4 : null;
                                item.FKinhPhiDuocCapTongVnd5 = i == 4 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd5 != null ? item.FKinhPhiDuocCapTongVnd5 : null;
                                item.FKinhPhiDuocCapTongVnd6 = i == 5 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd6 != null ? item.FKinhPhiDuocCapTongVnd6 : null;
                                item.FKinhPhiDuocCapTongVnd7 = i == 6 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd7 != null ? item.FKinhPhiDuocCapTongVnd7 : null;
                                item.FKinhPhiDuocCapTongVnd8 = i == 7 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd8 != null ? item.FKinhPhiDuocCapTongVnd8 : null;
                                item.FKinhPhiDuocCapTongVnd9 = i == 8 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd9 != null ? item.FKinhPhiDuocCapTongVnd9 : null;
                                item.FKinhPhiDuocCapTongVnd10 = i == 9 ? item.fLuyKeKinhPhiDuocCap_VND : item.FKinhPhiDuocCapTongVnd10 != null ? item.FKinhPhiDuocCapTongVnd10 : null;
                                                                                                       
                                item.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd1 != null ? item.FQuyetToanDuocDuyetTongUsd1 : null;
                                item.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd2 != null ? item.FQuyetToanDuocDuyetTongUsd2 : null;
                                item.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd3 != null ? item.FQuyetToanDuocDuyetTongUsd3 : null;
                                item.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd4 != null ? item.FQuyetToanDuocDuyetTongUsd4 : null;
                                item.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd5 != null ? item.FQuyetToanDuocDuyetTongUsd5 : null;
                                item.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd6 != null ? item.FQuyetToanDuocDuyetTongUsd6 : null;
                                item.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd7 != null ? item.FQuyetToanDuocDuyetTongUsd7 : null;
                                item.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd8 != null ? item.FQuyetToanDuocDuyetTongUsd8 : null;
                                item.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd9 != null ? item.FQuyetToanDuocDuyetTongUsd9 : null;
                                item.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? item.fDeNghiQTNamNay_USD : item.FQuyetToanDuocDuyetTongUsd10 != null ? item.FQuyetToanDuocDuyetTongUsd10 : null;
                                                                                                       
                                item.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd1 != null ? item.FQuyetToanDuocDuyetTongVnd1 : null;
                                item.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd2 != null ? item.FQuyetToanDuocDuyetTongVnd2 : null;
                                item.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd3 != null ? item.FQuyetToanDuocDuyetTongVnd3 : null;
                                item.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd4 != null ? item.FQuyetToanDuocDuyetTongVnd4 : null;
                                item.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd5 != null ? item.FQuyetToanDuocDuyetTongVnd5 : null;
                                item.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd6 != null ? item.FQuyetToanDuocDuyetTongVnd6 : null;
                                item.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd7 != null ? item.FQuyetToanDuocDuyetTongVnd7 : null;
                                item.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd8 != null ? item.FQuyetToanDuocDuyetTongVnd8 : null;
                                item.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd9 != null ? item.FQuyetToanDuocDuyetTongVnd9 : null;
                                item.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? item.fDeNghiQTNamNay_VND : item.FQuyetToanDuocDuyetTongVnd10 != null ? item.FQuyetToanDuocDuyetTongVnd10 : null;
                            }
                        }
                    }
                    if (item.IDNhiemVuChi != idChuongTrinh/* && item.IDNhiemVuChi != Guid.Empty*/)
                    {
                        SttChuongTrinh++;
                        SttDuAn = 0;
                        SttLoai = 0;
                        SttDuAn = 0;
                        idDuAn = null;
                        idLoai = null;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        dataTongHops.ForEach(x =>
                        {
                            if (x.IDNhiemVuChi == item.IDNhiemVuChi)
                                x.iLoaiNoiDungChi = item.iLoaiNoiDungChi;
                        });
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();
                        var dataTongHopByNVC = dataTongHops.Where(x => x.IDNhiemVuChi == item.IDNhiemVuChi);
                        //DataCha.HopDongUSD = listDataCha.Sum(x => x.HopDongUSD);
                        //DataCha.HopDongVND = listDataCha.Sum(x => x.HopDongVND);

                        // cũ
                        //DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        //DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        //DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        //DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        //DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        //DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        //DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        //DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        //-----Tính lại data tổng hợp----------

                        DataCha.KinhPhiUSD = dataTongHopByNVC.Sum(x => x.KinhPhiUSD);//data col5
                        DataCha.KinhPhiVND = dataTongHopByNVC.Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = dataTongHopByNVC.Sum(x => x.KinhPhiToYUSD);//data col11
                        DataCha.KinhPhiToYVND = dataTongHopByNVC.Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = dataTongHopByNVC.Sum(x => x.KinhPhiDaChiUSD);//data col15
                        DataCha.KinhPhiDaChiVND = dataTongHopByNVC.Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = dataTongHopByNVC.Sum(x => x.KinhPhiDaChiToYUSD);//data col17
                        DataCha.KinhPhiDaChiToYVND = dataTongHopByNVC.Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);
                        DataCha.NCVTTCP = item.NCVTTCP;
                        DataCha.NhiemVuChi = item.NhiemVuChi;

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;
                        if (item.IDNhiemVuChi != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = item.sTenNhiemVuChi;
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Nội dung chi khác";
                        }
                        DataCha.depth = convertLetter(SttChuongTrinh) + ".";
                        DataCha.isTitle = "font-bold-red";
                        idChuongTrinh = item.IDNhiemVuChi;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        DataCha.IsHangCha = true;

                        if (lstGiaiDoan != null)
                        {
                            for (var i = 0; i < lstGiaiDoan.Count(); i++)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                                DataCha.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd1 != null ? DataCha.FKeHoachTTCPUsd1 : null;
                                DataCha.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd2 != null ? DataCha.FKeHoachTTCPUsd2 : null;
                                DataCha.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd3 != null ? DataCha.FKeHoachTTCPUsd3 : null;
                                DataCha.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd4 != null ? DataCha.FKeHoachTTCPUsd4 : null;
                                DataCha.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd5 != null ? DataCha.FKeHoachTTCPUsd5 : null;
                                DataCha.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd6 != null ? DataCha.FKeHoachTTCPUsd6 : null;
                                DataCha.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd7 != null ? DataCha.FKeHoachTTCPUsd7 : null;
                                DataCha.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd8 != null ? DataCha.FKeHoachTTCPUsd8 : null;
                                DataCha.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd9 != null ? DataCha.FKeHoachTTCPUsd9 : null;
                                DataCha.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd10 != null ? DataCha.FKeHoachTTCPUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd1 != null ? DataCha.FKinhPhiDuocCapTongUsd1 : null;
                                DataCha.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd2 != null ? DataCha.FKinhPhiDuocCapTongUsd2 : null;
                                DataCha.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd3 != null ? DataCha.FKinhPhiDuocCapTongUsd3 : null;
                                DataCha.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd4 != null ? DataCha.FKinhPhiDuocCapTongUsd4 : null;
                                DataCha.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd5 != null ? DataCha.FKinhPhiDuocCapTongUsd5 : null;
                                DataCha.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd6 != null ? DataCha.FKinhPhiDuocCapTongUsd6 : null;
                                DataCha.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd7 != null ? DataCha.FKinhPhiDuocCapTongUsd7 : null;
                                DataCha.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd8 != null ? DataCha.FKinhPhiDuocCapTongUsd8 : null;
                                DataCha.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd9 != null ? DataCha.FKinhPhiDuocCapTongUsd9 : null;
                                DataCha.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd10 != null ? DataCha.FKinhPhiDuocCapTongUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd1 != null ? DataCha.FKinhPhiDuocCapTongVnd1 : null;
                                DataCha.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd2 != null ? DataCha.FKinhPhiDuocCapTongVnd2 : null;
                                DataCha.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd3 != null ? DataCha.FKinhPhiDuocCapTongVnd3 : null;
                                DataCha.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd4 != null ? DataCha.FKinhPhiDuocCapTongVnd4 : null;
                                DataCha.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd5 != null ? DataCha.FKinhPhiDuocCapTongVnd5 : null;
                                DataCha.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd6 != null ? DataCha.FKinhPhiDuocCapTongVnd6 : null;
                                DataCha.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd7 != null ? DataCha.FKinhPhiDuocCapTongVnd7 : null;
                                DataCha.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd8 != null ? DataCha.FKinhPhiDuocCapTongVnd8 : null;
                                DataCha.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd9 != null ? DataCha.FKinhPhiDuocCapTongVnd9 : null;
                                DataCha.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd10 != null ? DataCha.FKinhPhiDuocCapTongVnd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd1 != null ? DataCha.FQuyetToanDuocDuyetTongUsd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd2 != null ? DataCha.FQuyetToanDuocDuyetTongUsd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd3 != null ? DataCha.FQuyetToanDuocDuyetTongUsd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd4 != null ? DataCha.FQuyetToanDuocDuyetTongUsd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd5 != null ? DataCha.FQuyetToanDuocDuyetTongUsd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd6 != null ? DataCha.FQuyetToanDuocDuyetTongUsd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd7 != null ? DataCha.FQuyetToanDuocDuyetTongUsd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd8 != null ? DataCha.FQuyetToanDuocDuyetTongUsd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd9 != null ? DataCha.FQuyetToanDuocDuyetTongUsd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd10 != null ? DataCha.FQuyetToanDuocDuyetTongUsd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd1 != null ? DataCha.FQuyetToanDuocDuyetTongVnd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd2 != null ? DataCha.FQuyetToanDuocDuyetTongVnd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd3 != null ? DataCha.FQuyetToanDuocDuyetTongVnd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd4 != null ? DataCha.FQuyetToanDuocDuyetTongVnd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd5 != null ? DataCha.FQuyetToanDuocDuyetTongVnd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd6 != null ? DataCha.FQuyetToanDuocDuyetTongVnd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd7 != null ? DataCha.FQuyetToanDuocDuyetTongVnd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd8 != null ? DataCha.FQuyetToanDuocDuyetTongVnd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd9 != null ? DataCha.FQuyetToanDuocDuyetTongVnd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd10 != null ? DataCha.FQuyetToanDuocDuyetTongVnd10 : null;
                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IDDuAn != idDuAn /*&& item.IDDuAn != Guid.Empty*/)
                    {
                        SttDuAn++;
                        SttLoai = 0;
                        SttHopDong = 0;
                        idLoai = null;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();
                        var dataTongHopByDuAn = dataTongHops.Where(x => x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi);

                        DataCha.HopDongUSD = listDataCha.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongUSD);
                        DataCha.HopDongVND = listDataCha.GroupBy(x => new { x.IDHopDong, x.iLoaiNoiDungChi }).Select(x => x.First()).Sum(x => x.HopDongVND);

                        //DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        //DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        //DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        //DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        //DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        //DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        //DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        //DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        //-----Tính lại data tổng hợp----------

                        DataCha.KinhPhiUSD = dataTongHopByDuAn.Sum(x => x.KinhPhiUSD);//data col5
                        DataCha.KinhPhiVND = dataTongHopByDuAn.Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = dataTongHopByDuAn.Sum(x => x.KinhPhiToYUSD);//data col11
                        DataCha.KinhPhiToYVND = dataTongHopByDuAn.Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = dataTongHopByDuAn.Sum(x => x.KinhPhiDaChiUSD);//data col15
                        DataCha.KinhPhiDaChiVND = dataTongHopByDuAn.Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = dataTongHopByDuAn.Sum(x => x.KinhPhiDaChiToYUSD);//data col17
                        DataCha.KinhPhiDaChiToYVND = dataTongHopByDuAn.Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);


                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;
                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;

                        if (item.IDDuAn != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = item.sTenDuAn;
                        }
                        else if (item.IDHopDong != Guid.Empty)
                        {
                            DataCha.sTenNoiDungChi = "Chi hợp đồng";
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Chi khác";
                        }
                        DataCha.sTenCDT = item.sTenCDT;
                        DataCha.isTitle = "font-bold";
                        DataCha.isDuAn = true;
                        DataCha.IsHangCha = true;
                        DataCha.depth = _nhTtThucHienNganSachService.GetSTTLAMA(SttDuAn) + ".";
                        idDuAn = item.IDDuAn;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            for (var i = 0; i < lstGiaiDoan.Count(); i++)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                                //DataCha.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd1 != null ? DataCha.FKeHoachTTCPUsd1 : null;
                                //DataCha.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd2 != null ? DataCha.FKeHoachTTCPUsd2 : null;
                                //DataCha.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd3 != null ? DataCha.FKeHoachTTCPUsd3 : null;
                                //DataCha.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd4 != null ? DataCha.FKeHoachTTCPUsd4 : null;
                                //DataCha.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd5 != null ? DataCha.FKeHoachTTCPUsd5 : null;
                                //DataCha.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd6 != null ? DataCha.FKeHoachTTCPUsd6 : null;
                                //DataCha.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd7 != null ? DataCha.FKeHoachTTCPUsd7 : null;
                                //DataCha.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd8 != null ? DataCha.FKeHoachTTCPUsd8 : null;
                                //DataCha.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd9 != null ? DataCha.FKeHoachTTCPUsd9 : null;
                                //DataCha.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd10 != null ? DataCha.FKeHoachTTCPUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd1 != null ? DataCha.FKinhPhiDuocCapTongUsd1 : null;
                                DataCha.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd2 != null ? DataCha.FKinhPhiDuocCapTongUsd2 : null;
                                DataCha.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd3 != null ? DataCha.FKinhPhiDuocCapTongUsd3 : null;
                                DataCha.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd4 != null ? DataCha.FKinhPhiDuocCapTongUsd4 : null;
                                DataCha.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd5 != null ? DataCha.FKinhPhiDuocCapTongUsd5 : null;
                                DataCha.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd6 != null ? DataCha.FKinhPhiDuocCapTongUsd6 : null;
                                DataCha.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd7 != null ? DataCha.FKinhPhiDuocCapTongUsd7 : null;
                                DataCha.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd8 != null ? DataCha.FKinhPhiDuocCapTongUsd8 : null;
                                DataCha.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd9 != null ? DataCha.FKinhPhiDuocCapTongUsd9 : null;
                                DataCha.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd10 != null ? DataCha.FKinhPhiDuocCapTongUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd1 != null ? DataCha.FKinhPhiDuocCapTongVnd1 : null;
                                DataCha.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd2 != null ? DataCha.FKinhPhiDuocCapTongVnd2 : null;
                                DataCha.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd3 != null ? DataCha.FKinhPhiDuocCapTongVnd3 : null;
                                DataCha.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd4 != null ? DataCha.FKinhPhiDuocCapTongVnd4 : null;
                                DataCha.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd5 != null ? DataCha.FKinhPhiDuocCapTongVnd5 : null;
                                DataCha.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd6 != null ? DataCha.FKinhPhiDuocCapTongVnd6 : null;
                                DataCha.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd7 != null ? DataCha.FKinhPhiDuocCapTongVnd7 : null;
                                DataCha.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd8 != null ? DataCha.FKinhPhiDuocCapTongVnd8 : null;
                                DataCha.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd9 != null ? DataCha.FKinhPhiDuocCapTongVnd9 : null;
                                DataCha.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd10 != null ? DataCha.FKinhPhiDuocCapTongVnd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd1 != null ? DataCha.FQuyetToanDuocDuyetTongUsd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd2 != null ? DataCha.FQuyetToanDuocDuyetTongUsd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd3 != null ? DataCha.FQuyetToanDuocDuyetTongUsd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd4 != null ? DataCha.FQuyetToanDuocDuyetTongUsd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd5 != null ? DataCha.FQuyetToanDuocDuyetTongUsd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd6 != null ? DataCha.FQuyetToanDuocDuyetTongUsd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd7 != null ? DataCha.FQuyetToanDuocDuyetTongUsd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd8 != null ? DataCha.FQuyetToanDuocDuyetTongUsd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd9 != null ? DataCha.FQuyetToanDuocDuyetTongUsd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd10 != null ? DataCha.FQuyetToanDuocDuyetTongUsd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd1 != null ? DataCha.FQuyetToanDuocDuyetTongVnd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd2 != null ? DataCha.FQuyetToanDuocDuyetTongVnd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd3 != null ? DataCha.FQuyetToanDuocDuyetTongVnd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd4 != null ? DataCha.FQuyetToanDuocDuyetTongVnd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd5 != null ? DataCha.FQuyetToanDuocDuyetTongVnd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd6 != null ? DataCha.FQuyetToanDuocDuyetTongVnd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd7 != null ? DataCha.FQuyetToanDuocDuyetTongVnd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd8 != null ? DataCha.FQuyetToanDuocDuyetTongVnd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd9 != null ? DataCha.FQuyetToanDuocDuyetTongVnd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd10 != null ? DataCha.FQuyetToanDuocDuyetTongVnd10 : null;
                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.iLoaiNoiDungChi != idLoai && item.iLoaiNoiDungChi != 0)
                    {
                        SttLoai++;
                        SttHopDong = 0;
                        idHopDong = null;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();
                        var dataTongHopByLoaiNoiDungChi = dataTongHops.Where(x => x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();

                        //DataCha.HopDongUSD = listDataCha.Sum(x => x.HopDongUSD);
                        //DataCha.HopDongVND = listDataCha.Sum(x => x.HopDongVND);

                        //DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        //DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        //DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        //DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        //DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        //DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        //DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        //DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

                        //-----Tính lại data tổng hợp----------

                        DataCha.KinhPhiUSD = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiUSD);//data col5
                        DataCha.KinhPhiVND = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiToYUSD);//data col11
                        DataCha.KinhPhiToYVND = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiDaChiUSD);//data col15
                        DataCha.KinhPhiDaChiVND = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiDaChiToYUSD);//data col17
                        DataCha.KinhPhiDaChiToYVND = dataTongHopByLoaiNoiDungChi.Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        //DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;
                        DataCha.IsHangCha = true;

                        if (item.iLoaiNoiDungChi == 1)
                        {
                            DataCha.sTenNoiDungChi = "Chi ngoại tệ";
                        }
                        else if (item.iLoaiNoiDungChi == 2)
                        {
                            DataCha.sTenNoiDungChi = "Chi trong nước";
                        }
                        else
                        {
                            DataCha.sTenNoiDungChi = "Chi khác";
                        }
                        DataCha.depth = SttLoai.ToString() + ".";
                        DataCha.isTitle = "font-bold";
                        idLoai = item.iLoaiNoiDungChi;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            for (var i = 0; i < lstGiaiDoan.Count(); i++)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                                //DataCha.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd1 != null ? DataCha.FKeHoachTTCPUsd1 : null;
                                //DataCha.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd2 != null ? DataCha.FKeHoachTTCPUsd2 : null;
                                //DataCha.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd3 != null ? DataCha.FKeHoachTTCPUsd3 : null;
                                //DataCha.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd4 != null ? DataCha.FKeHoachTTCPUsd4 : null;
                                //DataCha.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd5 != null ? DataCha.FKeHoachTTCPUsd5 : null;
                                //DataCha.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd6 != null ? DataCha.FKeHoachTTCPUsd6 : null;
                                //DataCha.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd7 != null ? DataCha.FKeHoachTTCPUsd7 : null;
                                //DataCha.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd8 != null ? DataCha.FKeHoachTTCPUsd8 : null;
                                //DataCha.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd9 != null ? DataCha.FKeHoachTTCPUsd9 : null;
                                //DataCha.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd10 != null ? DataCha.FKeHoachTTCPUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd1 != null ? DataCha.FKinhPhiDuocCapTongUsd1 : null;
                                DataCha.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd2 != null ? DataCha.FKinhPhiDuocCapTongUsd2 : null;
                                DataCha.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd3 != null ? DataCha.FKinhPhiDuocCapTongUsd3 : null;
                                DataCha.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd4 != null ? DataCha.FKinhPhiDuocCapTongUsd4 : null;
                                DataCha.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd5 != null ? DataCha.FKinhPhiDuocCapTongUsd5 : null;
                                DataCha.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd6 != null ? DataCha.FKinhPhiDuocCapTongUsd6 : null;
                                DataCha.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd7 != null ? DataCha.FKinhPhiDuocCapTongUsd7 : null;
                                DataCha.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd8 != null ? DataCha.FKinhPhiDuocCapTongUsd8 : null;
                                DataCha.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd9 != null ? DataCha.FKinhPhiDuocCapTongUsd9 : null;
                                DataCha.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd10 != null ? DataCha.FKinhPhiDuocCapTongUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd1 != null ? DataCha.FKinhPhiDuocCapTongVnd1 : null;
                                DataCha.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd2 != null ? DataCha.FKinhPhiDuocCapTongVnd2 : null;
                                DataCha.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd3 != null ? DataCha.FKinhPhiDuocCapTongVnd3 : null;
                                DataCha.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd4 != null ? DataCha.FKinhPhiDuocCapTongVnd4 : null;
                                DataCha.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd5 != null ? DataCha.FKinhPhiDuocCapTongVnd5 : null;
                                DataCha.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd6 != null ? DataCha.FKinhPhiDuocCapTongVnd6 : null;
                                DataCha.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd7 != null ? DataCha.FKinhPhiDuocCapTongVnd7 : null;
                                DataCha.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd8 != null ? DataCha.FKinhPhiDuocCapTongVnd8 : null;
                                DataCha.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd9 != null ? DataCha.FKinhPhiDuocCapTongVnd9 : null;
                                DataCha.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd10 != null ? DataCha.FKinhPhiDuocCapTongVnd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd1 != null ? DataCha.FQuyetToanDuocDuyetTongUsd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd2 != null ? DataCha.FQuyetToanDuocDuyetTongUsd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd3 != null ? DataCha.FQuyetToanDuocDuyetTongUsd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd4 != null ? DataCha.FQuyetToanDuocDuyetTongUsd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd5 != null ? DataCha.FQuyetToanDuocDuyetTongUsd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd6 != null ? DataCha.FQuyetToanDuocDuyetTongUsd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd7 != null ? DataCha.FQuyetToanDuocDuyetTongUsd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd8 != null ? DataCha.FQuyetToanDuocDuyetTongUsd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd9 != null ? DataCha.FQuyetToanDuocDuyetTongUsd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd10 != null ? DataCha.FQuyetToanDuocDuyetTongUsd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd1 != null ? DataCha.FQuyetToanDuocDuyetTongVnd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd2 != null ? DataCha.FQuyetToanDuocDuyetTongVnd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd3 != null ? DataCha.FQuyetToanDuocDuyetTongVnd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd4 != null ? DataCha.FQuyetToanDuocDuyetTongVnd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd5 != null ? DataCha.FQuyetToanDuocDuyetTongVnd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd6 != null ? DataCha.FQuyetToanDuocDuyetTongVnd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd7 != null ? DataCha.FQuyetToanDuocDuyetTongVnd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd8 != null ? DataCha.FQuyetToanDuocDuyetTongVnd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd9 != null ? DataCha.FQuyetToanDuocDuyetTongVnd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd10 != null ? DataCha.FQuyetToanDuocDuyetTongVnd10 : null;
                            }
                        }

                        listData.Add(DataCha);
                    }
                    if (item.IDHopDong != idHopDong && item.IDHopDong != Guid.Empty)
                    {
                        SttHopDong++;
                        NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();
                        List<NhTtThucHienNganSachModel> listDataCha = list.Where(x => x.IDHopDong == item.IDHopDong && x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();
                        var dataTongHopByHopDong = list.Where(x => x.IDHopDong == item.IDHopDong && x.iLoaiNoiDungChi == item.iLoaiNoiDungChi && x.IDDuAn == item.IDDuAn && x.IDNhiemVuChi == item.IDNhiemVuChi).ToList();

                        DataCha.HopDongUSD = item.HopDongUSD;
                        DataCha.HopDongVND = item.HopDongVND;

                        //DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
                        //DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
                        //DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
                        //DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
                        //DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
                        //DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
                        //DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
                        //DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);
                        //-----Tính lại data tổng hợp----------

                        DataCha.KinhPhiUSD = dataTongHopByHopDong.Sum(x => x.KinhPhiUSD);//data col5
                        DataCha.KinhPhiVND = dataTongHopByHopDong.Sum(x => x.KinhPhiVND);
                        DataCha.KinhPhiToYUSD = dataTongHopByHopDong.Sum(x => x.KinhPhiToYUSD);//data col11
                        DataCha.KinhPhiToYVND = dataTongHopByHopDong.Sum(x => x.KinhPhiToYVND);
                        DataCha.KinhPhiDaChiUSD = dataTongHopByHopDong.Sum(x => x.KinhPhiDaChiUSD);//data col15
                        DataCha.KinhPhiDaChiVND = dataTongHopByHopDong.Sum(x => x.KinhPhiDaChiVND);
                        DataCha.KinhPhiDaChiToYUSD = dataTongHopByHopDong.Sum(x => x.KinhPhiDaChiToYUSD);//data col17
                        DataCha.KinhPhiDaChiToYVND = dataTongHopByHopDong.Sum(x => x.KinhPhiDaChiToYVND);

                        DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
                        DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
                        DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
                        DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

                        //DataCha.NCVTTCP = listDataCha.Sum(x => x.NCVTTCP);
                        //DataCha.NhiemVuChi = listDataCha.Sum(x => x.NhiemVuChi);

                        DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
                        DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

                        DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
                        DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

                        DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
                        DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
                        //DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

                        DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
                        DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
                        //DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;
                        DataTong.IsHangCha = true;

                        DataCha.sTenNoiDungChi = item.sTenHopDong;
                        DataCha.isHopDong = true;
                        DataCha.depth = SttLoai.ToString() + "." + SttHopDong.ToString() + ".";
                        idHopDong = item.IDHopDong;
                        DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                        DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

                        DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
                        DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
                        if (lstGiaiDoan != null)
                        {
                            for (var i = 0; i < lstGiaiDoan.Count(); i++)
                            {
                                List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                                //DataCha.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd1 != null ? DataCha.FKeHoachTTCPUsd1 : null;
                                //DataCha.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd2 != null ? DataCha.FKeHoachTTCPUsd2 : null;
                                //DataCha.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd3 != null ? DataCha.FKeHoachTTCPUsd3 : null;
                                //DataCha.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd4 != null ? DataCha.FKeHoachTTCPUsd4 : null;
                                //DataCha.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd5 != null ? DataCha.FKeHoachTTCPUsd5 : null;
                                //DataCha.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd6 != null ? DataCha.FKeHoachTTCPUsd6 : null;
                                //DataCha.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd7 != null ? DataCha.FKeHoachTTCPUsd7 : null;
                                //DataCha.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd8 != null ? DataCha.FKeHoachTTCPUsd8 : null;
                                //DataCha.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd9 != null ? DataCha.FKeHoachTTCPUsd9 : null;
                                //DataCha.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd10 != null ? DataCha.FKeHoachTTCPUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd1 != null ? DataCha.FKinhPhiDuocCapTongUsd1 : null;
                                DataCha.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd2 != null ? DataCha.FKinhPhiDuocCapTongUsd2 : null;
                                DataCha.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd3 != null ? DataCha.FKinhPhiDuocCapTongUsd3 : null;
                                DataCha.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd4 != null ? DataCha.FKinhPhiDuocCapTongUsd4 : null;
                                DataCha.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd5 != null ? DataCha.FKinhPhiDuocCapTongUsd5 : null;
                                DataCha.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd6 != null ? DataCha.FKinhPhiDuocCapTongUsd6 : null;
                                DataCha.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd7 != null ? DataCha.FKinhPhiDuocCapTongUsd7 : null;
                                DataCha.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd8 != null ? DataCha.FKinhPhiDuocCapTongUsd8 : null;
                                DataCha.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd9 != null ? DataCha.FKinhPhiDuocCapTongUsd9 : null;
                                DataCha.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd10 != null ? DataCha.FKinhPhiDuocCapTongUsd10 : null;

                                DataCha.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd1 != null ? DataCha.FKinhPhiDuocCapTongVnd1 : null;
                                DataCha.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd2 != null ? DataCha.FKinhPhiDuocCapTongVnd2 : null;
                                DataCha.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd3 != null ? DataCha.FKinhPhiDuocCapTongVnd3 : null;
                                DataCha.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd4 != null ? DataCha.FKinhPhiDuocCapTongVnd4 : null;
                                DataCha.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd5 != null ? DataCha.FKinhPhiDuocCapTongVnd5 : null;
                                DataCha.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd6 != null ? DataCha.FKinhPhiDuocCapTongVnd6 : null;
                                DataCha.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd7 != null ? DataCha.FKinhPhiDuocCapTongVnd7 : null;
                                DataCha.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd8 != null ? DataCha.FKinhPhiDuocCapTongVnd8 : null;
                                DataCha.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd9 != null ? DataCha.FKinhPhiDuocCapTongVnd9 : null;
                                DataCha.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd10 != null ? DataCha.FKinhPhiDuocCapTongVnd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd1 != null ? DataCha.FQuyetToanDuocDuyetTongUsd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd2 != null ? DataCha.FQuyetToanDuocDuyetTongUsd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd3 != null ? DataCha.FQuyetToanDuocDuyetTongUsd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd4 != null ? DataCha.FQuyetToanDuocDuyetTongUsd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd5 != null ? DataCha.FQuyetToanDuocDuyetTongUsd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd6 != null ? DataCha.FQuyetToanDuocDuyetTongUsd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd7 != null ? DataCha.FQuyetToanDuocDuyetTongUsd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd8 != null ? DataCha.FQuyetToanDuocDuyetTongUsd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd9 != null ? DataCha.FQuyetToanDuocDuyetTongUsd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd10 != null ? DataCha.FQuyetToanDuocDuyetTongUsd10 : null;

                                DataCha.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd1 != null ? DataCha.FQuyetToanDuocDuyetTongVnd1 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd2 != null ? DataCha.FQuyetToanDuocDuyetTongVnd2 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd3 != null ? DataCha.FQuyetToanDuocDuyetTongVnd3 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd4 != null ? DataCha.FQuyetToanDuocDuyetTongVnd4 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd5 != null ? DataCha.FQuyetToanDuocDuyetTongVnd5 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd6 != null ? DataCha.FQuyetToanDuocDuyetTongVnd6 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd7 != null ? DataCha.FQuyetToanDuocDuyetTongVnd7 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd8 != null ? DataCha.FQuyetToanDuocDuyetTongVnd8 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd9 != null ? DataCha.FQuyetToanDuocDuyetTongVnd9 : null;
                                DataCha.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd10 != null ? DataCha.FQuyetToanDuocDuyetTongVnd10 : null;
                            }
                        }

                        listData.Add(DataCha);
                    }




                    //DataTong.KinhPhiUSD += item.KinhPhiUSD;
                    //DataTong.KinhPhiVND += item.KinhPhiVND;
                    //DataTong.KinhPhiToYUSD += item.KinhPhiToYUSD;
                    //DataTong.KinhPhiToYVND += item.KinhPhiToYVND;
                    //DataTong.KinhPhiDaChiUSD += item.KinhPhiDaChiUSD;
                    //DataTong.KinhPhiDaChiVND += item.KinhPhiDaChiVND;
                    //DataTong.KinhPhiDaChiToYUSD += item.KinhPhiDaChiToYUSD;
                    //DataTong.KinhPhiDaChiToYVND += item.KinhPhiDaChiToYVND;
                    //DataTong.KinhPhiDuocCapChuaChiUSD += item.KinhPhiDuocCapChuaChiUSD;
                    //DataTong.KinhPhiDuocCapChuaChiVND += item.KinhPhiDuocCapChuaChiVND;


                    //DataTong.TongKinhPhiUSD += item.TongKinhPhiUSD;
                    //DataTong.TongKinhPhiVND += item.TongKinhPhiVND;

                    //DataTong.TongKinhPhiDaChiUSD += item.TongKinhPhiDaChiUSD;
                    //DataTong.TongKinhPhiDaChiVND += item.TongKinhPhiDaChiVND;
                    //DataTong.QuyGiaiNganTheoQuy += item.QuyGiaiNganTheoQuy;

                    DataTong.fLuyKeKinhPhiDuocCap_USD += item.fLuyKeKinhPhiDuocCap_USD;
                    DataTong.fLuyKeKinhPhiDuocCap_VND += item.fLuyKeKinhPhiDuocCap_VND;
                    DataTong.fDeNghiQTNamNay_USD += item.fDeNghiQTNamNay_USD;
                    DataTong.fDeNghiQTNamNay_VND += item.fDeNghiQTNamNay_VND;

                    DataTong.KinhPhiChuaQuyetToanUSD += item.KinhPhiChuaQuyetToanUSD;
                    DataTong.KinhPhiChuaQuyetToanVND += item.KinhPhiChuaQuyetToanVND;

                    //DataTong.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();
                    //DataTong.iGiaiDoanDen = item.iGiaiDoanDen;
                    //DataTong.iGiaiDoanTu = item.iGiaiDoanTu;

                    if (tabTable == 1)
                    {
                        item.sTenCDT = "";
                        item.HopDongUSD = 0;
                        item.HopDongVND = 0;
                        item.NCVTTCP = 0;
                        item.NhiemVuChi = 0;
                        item.QuyGiaiNganTheoQuy = 0;
                        item.KeHoachGiaiNgan = 0;
                        listData.Add(item);
                    }

                    if (sttTong == list.Count())
                    {
                        DataTong.KeHoachGiaiNgan = DataTong.NCVTTCP - DataTong.fLuyKeKinhPhiDuocCap_USD;
                        DataTong.sTenNoiDungChi = "Tổng Cộng: ";
                        DataTong.isDuAn = true;
                        DataTong.isTitle = "font-bold";
                        DataTong.isSum = true;
                        listData.Add(DataTong);
                    }
                }
            }

            return listData;
        }

        public NhTtThucHienNganSachModel getDataCha(List<NhTtThucHienNganSachModel> list , List<NhTtThucHienNganSachModel> listDataCha, List<NhTtThucHienNganSachGiaiDoanModel> lstGiaiDoan, NhTtThucHienNganSachModel item , string depth , string sTenNoiDungChi)
        {
            NhTtThucHienNganSachModel DataCha = new NhTtThucHienNganSachModel();

            DataCha.HopDongUSD = listDataCha.Sum(x => x.HopDongUSD);
            DataCha.HopDongVND = listDataCha.Sum(x => x.HopDongVND);

            DataCha.KinhPhiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiUSD);
            DataCha.KinhPhiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiVND);
            DataCha.KinhPhiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYUSD);
            DataCha.KinhPhiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiToYVND);
            DataCha.KinhPhiDaChiUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiUSD);
            DataCha.KinhPhiDaChiVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiVND);
            DataCha.KinhPhiDaChiToYUSD = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYUSD);
            DataCha.KinhPhiDaChiToYVND = listDataCha.GroupBy(x => x.iID_ThanhToanID).Select(x => x.First()).Sum(x => x.KinhPhiDaChiToYVND);

            DataCha.fLuyKeKinhPhiDuocCap_USD = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_USD);
            DataCha.fLuyKeKinhPhiDuocCap_VND = listDataCha.Sum(x => x.fLuyKeKinhPhiDuocCap_VND);
            DataCha.fDeNghiQTNamNay_USD = listDataCha.Sum(x => x.fDeNghiQTNamNay_USD);
            DataCha.fDeNghiQTNamNay_VND = listDataCha.Sum(x => x.fDeNghiQTNamNay_VND);

            DataCha.NCVTTCP = listDataCha.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP);
            DataCha.NhiemVuChi = listDataCha.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NhiemVuChi);

            DataCha.TongKinhPhiUSD = DataCha.KinhPhiUSD + DataCha.KinhPhiToYUSD;
            DataCha.TongKinhPhiVND = DataCha.KinhPhiVND + DataCha.KinhPhiToYVND;

            DataCha.TongKinhPhiDaChiUSD = DataCha.KinhPhiDaChiUSD + DataCha.KinhPhiDaChiToYUSD;
            DataCha.TongKinhPhiDaChiVND = DataCha.KinhPhiDaChiVND + DataCha.KinhPhiDaChiToYVND;

            DataCha.KinhPhiDuocCapChuaChiUSD = DataCha.TongKinhPhiUSD - DataCha.TongKinhPhiDaChiUSD;
            DataCha.KinhPhiDuocCapChuaChiVND = DataCha.TongKinhPhiVND - DataCha.TongKinhPhiDaChiVND;
            DataCha.QuyGiaiNganTheoQuy = DataCha.NhiemVuChi - DataCha.TongKinhPhiUSD;

            DataCha.KinhPhiChuaQuyetToanUSD = DataCha.fLuyKeKinhPhiDuocCap_USD - DataCha.fDeNghiQTNamNay_USD;
            DataCha.KinhPhiChuaQuyetToanVND = DataCha.fLuyKeKinhPhiDuocCap_VND - DataCha.fDeNghiQTNamNay_VND;
            DataCha.KeHoachGiaiNgan = DataCha.NCVTTCP - DataCha.fLuyKeKinhPhiDuocCap_USD;

            DataCha.sTenNoiDungChi = sTenNoiDungChi;
            DataCha.isHopDong = true;
            DataCha.depth = depth;
            DataCha.lstGiaiDoanTTCP = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataCha.lstGiaiDoanKinhPhiDuocCap = new List<NhTtThucHienNganSachGiaiDoanModel>();
            DataCha.lstGiaiDoanKinhPhiDaGiaiNgan = new List<NhTtThucHienNganSachGiaiDoanModel>();

            DataCha.iGiaiDoanDen = item.iGiaiDoanDen;
            DataCha.iGiaiDoanTu = item.iGiaiDoanTu;
            if (lstGiaiDoan != null)
            {
                for (var i = 0; i < lstGiaiDoan.Count(); i++)
                {
                    List<NhTtThucHienNganSachModel> listDataChaGiaiDoan = listDataCha.Where(x => x.iGiaiDoanTu == lstGiaiDoan[i].iGiaiDoanTu && x.iGiaiDoanDen == lstGiaiDoan[i].iGiaiDoanDen).ToList();
                    DataCha.FKeHoachTTCPUsd1 = i == 0 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd1 != null ? DataCha.FKeHoachTTCPUsd1 : null;
                    DataCha.FKeHoachTTCPUsd2 = i == 1 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd2 != null ? DataCha.FKeHoachTTCPUsd2 : null;
                    DataCha.FKeHoachTTCPUsd3 = i == 2 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd3 != null ? DataCha.FKeHoachTTCPUsd3 : null;
                    DataCha.FKeHoachTTCPUsd4 = i == 3 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd4 != null ? DataCha.FKeHoachTTCPUsd4 : null;
                    DataCha.FKeHoachTTCPUsd5 = i == 4 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd5 != null ? DataCha.FKeHoachTTCPUsd5 : null;
                    DataCha.FKeHoachTTCPUsd6 = i == 5 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd6 != null ? DataCha.FKeHoachTTCPUsd6 : null;
                    DataCha.FKeHoachTTCPUsd7 = i == 6 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd7 != null ? DataCha.FKeHoachTTCPUsd7 : null;
                    DataCha.FKeHoachTTCPUsd8 = i == 7 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd8 != null ? DataCha.FKeHoachTTCPUsd8 : null;
                    DataCha.FKeHoachTTCPUsd9 = i == 8 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd9 != null ? DataCha.FKeHoachTTCPUsd9 : null;
                    DataCha.FKeHoachTTCPUsd10 = i == 9 ? listDataChaGiaiDoan.GroupBy(x => x.IDNhiemVuChi).Select(x => x.First()).Sum(x => x.NCVTTCP) : DataCha.FKeHoachTTCPUsd10 != null ? DataCha.FKeHoachTTCPUsd10 : null;
                    
                    DataCha.FKinhPhiDuocCapTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd1 != null ? DataCha.FKinhPhiDuocCapTongUsd1 : null;
                    DataCha.FKinhPhiDuocCapTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd2 != null ? DataCha.FKinhPhiDuocCapTongUsd2 : null;
                    DataCha.FKinhPhiDuocCapTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd3 != null ? DataCha.FKinhPhiDuocCapTongUsd3 : null;
                    DataCha.FKinhPhiDuocCapTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd4 != null ? DataCha.FKinhPhiDuocCapTongUsd4 : null;
                    DataCha.FKinhPhiDuocCapTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd5 != null ? DataCha.FKinhPhiDuocCapTongUsd5 : null;
                    DataCha.FKinhPhiDuocCapTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd6 != null ? DataCha.FKinhPhiDuocCapTongUsd6 : null;
                    DataCha.FKinhPhiDuocCapTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd7 != null ? DataCha.FKinhPhiDuocCapTongUsd7 : null;
                    DataCha.FKinhPhiDuocCapTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd8 != null ? DataCha.FKinhPhiDuocCapTongUsd8 : null;
                    DataCha.FKinhPhiDuocCapTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd9 != null ? DataCha.FKinhPhiDuocCapTongUsd9 : null;
                    DataCha.FKinhPhiDuocCapTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_USD) : DataCha.FKinhPhiDuocCapTongUsd10 != null ? DataCha.FKinhPhiDuocCapTongUsd10 : null;
                    
                    DataCha.FKinhPhiDuocCapTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd1 != null ? DataCha.FKinhPhiDuocCapTongVnd1 : null;
                    DataCha.FKinhPhiDuocCapTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd2 != null ? DataCha.FKinhPhiDuocCapTongVnd2 : null;
                    DataCha.FKinhPhiDuocCapTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd3 != null ? DataCha.FKinhPhiDuocCapTongVnd3 : null;
                    DataCha.FKinhPhiDuocCapTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd4 != null ? DataCha.FKinhPhiDuocCapTongVnd4 : null;
                    DataCha.FKinhPhiDuocCapTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd5 != null ? DataCha.FKinhPhiDuocCapTongVnd5 : null;
                    DataCha.FKinhPhiDuocCapTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd6 != null ? DataCha.FKinhPhiDuocCapTongVnd6 : null;
                    DataCha.FKinhPhiDuocCapTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd7 != null ? DataCha.FKinhPhiDuocCapTongVnd7 : null;
                    DataCha.FKinhPhiDuocCapTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd8 != null ? DataCha.FKinhPhiDuocCapTongVnd8 : null;
                    DataCha.FKinhPhiDuocCapTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd9 != null ? DataCha.FKinhPhiDuocCapTongVnd9 : null;
                    DataCha.FKinhPhiDuocCapTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fLuyKeKinhPhiDuocCap_VND) : DataCha.FKinhPhiDuocCapTongVnd10 != null ? DataCha.FKinhPhiDuocCapTongVnd10 : null;
                   
                    DataCha.FQuyetToanDuocDuyetTongUsd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd1 != null ? DataCha.FQuyetToanDuocDuyetTongUsd1 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd2 != null ? DataCha.FQuyetToanDuocDuyetTongUsd2 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd3 != null ? DataCha.FQuyetToanDuocDuyetTongUsd3 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd4 != null ? DataCha.FQuyetToanDuocDuyetTongUsd4 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd5 != null ? DataCha.FQuyetToanDuocDuyetTongUsd5 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd6 != null ? DataCha.FQuyetToanDuocDuyetTongUsd6 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd7 != null ? DataCha.FQuyetToanDuocDuyetTongUsd7 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd8 != null ? DataCha.FQuyetToanDuocDuyetTongUsd8 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd9 != null ? DataCha.FQuyetToanDuocDuyetTongUsd9 : null;
                    DataCha.FQuyetToanDuocDuyetTongUsd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_USD) : DataCha.FQuyetToanDuocDuyetTongUsd10 != null ? DataCha.FQuyetToanDuocDuyetTongUsd10 : null;
                   
                    DataCha.FQuyetToanDuocDuyetTongVnd1 = i == 0 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd1 != null ? DataCha.FQuyetToanDuocDuyetTongVnd1 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd2 = i == 1 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd2 != null ? DataCha.FQuyetToanDuocDuyetTongVnd2 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd3 = i == 2 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd3 != null ? DataCha.FQuyetToanDuocDuyetTongVnd3 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd4 = i == 3 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd4 != null ? DataCha.FQuyetToanDuocDuyetTongVnd4 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd5 = i == 4 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd5 != null ? DataCha.FQuyetToanDuocDuyetTongVnd5 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd6 = i == 5 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd6 != null ? DataCha.FQuyetToanDuocDuyetTongVnd6 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd7 = i == 6 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd7 != null ? DataCha.FQuyetToanDuocDuyetTongVnd7 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd8 = i == 7 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd8 != null ? DataCha.FQuyetToanDuocDuyetTongVnd8 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd9 = i == 8 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd9 != null ? DataCha.FQuyetToanDuocDuyetTongVnd9 : null;
                    DataCha.FQuyetToanDuocDuyetTongVnd10 = i == 9 ? listDataChaGiaiDoan.Sum(x => x.fDeNghiQTNamNay_VND) : DataCha.FQuyetToanDuocDuyetTongVnd10 != null ? DataCha.FQuyetToanDuocDuyetTongVnd10 : null;
                }
            }
            return DataCha;
        }

        private string convertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }

        private List<NhTtThucHienNganSachModel> GetDataNHTongHop(List<Guid> lstNhiemVuChi)
        {
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            var lstMaNguon = new List<string>();
            var lstMaNguonBeforeYear = new List<string>();
            if (TabIndex == ImportTabIndex.MLNS)
            {
                predicate = predicate.And(x => x.INamKeHoach == (SelectedNam != null ? int.Parse(SelectedNam.ValueItem) : 0));
                predicate = predicate.And(x => lstNhiemVuChi.Contains(x.IIDKHTTNhiemVuChiID ?? Guid.Empty));
                lstMaNguon = NHConstants.MA_TH_BCTH_NS_QUY.Split(StringUtils.COMMA).Select(x => x.Trim()).ToList();
                predicate = predicate.And(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.IQuyKeHoach == (SelectedQuy != null ? int.Parse(SelectedQuy.ValueItem) : 0));
                lstMaNguonBeforeYear = new List<string> { NhTongHopConstants.MA_306, NhTongHopConstants.MA_308 };
                predicate = predicate.Or(x => (lstMaNguonBeforeYear.Contains(x.SMaNguon) || lstMaNguonBeforeYear.Contains(x.SMaNguonCha))
                                          && x.INamKeHoach == (SelectedNam != null ? int.Parse(SelectedNam.ValueItem) - 1 : 0));
            }
            else
            {
                predicate = predicate.And(x => (x.INamKeHoach == (SelectedTuNam != null ? int.Parse(SelectedTuNam.ValueItem) - 1 : 0)) || (x.INamKeHoach == (SelectedDenNam != null ? int.Parse(SelectedDenNam.ValueItem) : 0)));
                predicate = predicate.And(x => lstNhiemVuChi.Contains(x.IIDKHTTNhiemVuChiID ?? Guid.Empty));
                lstMaNguon = NHConstants.MA_TH_BCTH_NS_GIAIDOAN.Split(StringUtils.COMMA).Select(x => x.Trim()).ToList();
                predicate = predicate.And(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)));
            }


            return CalculateDataTongHop(_nhThTongHopService.FindByCondition(predicate));
        }


        private List<NhTtThucHienNganSachModel> CalculateDataTongHop(IEnumerable<NHTHTongHop> lstData)
        {
            List<NhTtThucHienNganSachModel> dataResults = new List<NhTtThucHienNganSachModel>();
            if (TabIndex == ImportTabIndex.MLNS)
            {
                if (lstData.Any())
                {
                    //var lstIdNhiemVuChi = lstData.Select(x => x.IIDKHTTNhiemVuChiID).Distinct();
                    var listHopDong = lstData.Where(x => x.IIdHopDongId != null).GroupBy(g => g.IIdHopDongId).Select(x => x.First());
                    var listDuAn = lstData.Where(x => x.IIdDuAnId != null && !listHopDong.Select(x => x.IIdDuAnId).Contains(x.IIdDuAnId)).GroupBy(x => x.IIdDuAnId).Select(x => x.First());
                    foreach (var item in listHopDong)
                    {
                        NhTtThucHienNganSachModel dataHandler = new NhTtThucHienNganSachModel();
                        var dataDetails = lstData.Where(x => x.IIdHopDongId == item.IIdHopDongId);
                        //Data col 9 kinh phí được cấp các năm trước

                        var dataCol9 = dataDetails.Where(x => (x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306));
                        var dataCol9Usd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd);
                        var dataCol9Vnd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd);

                        //Data col 15 kinh phí đã giải ngân các năm trước
                        var dataCol15 = dataDetails.Where(x => (x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308));
                        var dataCol15Usd = dataCol15.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd);
                        var dataCol15Vnd = dataCol15.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd);

                        //Data col 11 Kinh phí được cấp từ đầu năm đến quý này
                        var lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                        var lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                        //data plus 11
                        var dataPlus = dataDetails.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)));
                        var dataPlus11Usd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataPlus11Vnd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                        // data minus 11
                        var dataMinus = dataDetails.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)));
                        var dataMinus11Usd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataMinus12Vnd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var dataCol11Usd = dataPlus11Usd - dataMinus11Usd;
                        var dataCol12Vnd = dataPlus11Vnd - dataMinus12Vnd;

                        //Data col 17: Kinh phí đã giải ngân từ đầu năm đến quý này
                        lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_141, NhTongHopConstants.MA_142, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };

                        //data plus 17
                        var dataPlus17 = lstData.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)));
                        var dataPlus17Usd = dataPlus17.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataPlus17Vnd = dataPlus17.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var dataCol17Usd = dataPlus17Usd - dataMinus11Usd;
                        var dataCol17Vnd = dataPlus17Vnd - dataMinus12Vnd;
                        dataHandler.iID_ThanhToanID = item.IIdChungTu;
                        dataHandler.IDNhiemVuChi = (Guid)item.IIDKHTTNhiemVuChiID;
                        dataHandler.IDDuAn = (Guid)item.IIdDuAnId;
                        dataHandler.IDHopDong = (Guid)item.IIdHopDongId;
                        dataHandler.ID = item.Id;
                        dataResults.Add(dataHandler);
                    }

                    foreach (var item in listDuAn)
                    {
                        NhTtThucHienNganSachModel dataHandler = new NhTtThucHienNganSachModel();
                        var dataDetails = lstData.Where(x => x.IIdDuAnId == item.IIdDuAnId);
                        //Data col 9 kinh phí được cấp các năm trước

                        var dataCol9 = dataDetails.Where(x => (x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306));
                        var dataCol9Usd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd);
                        var dataCol9Vnd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd);

                        //Data col 15 kinh phí đã giải ngân các năm trước
                        var dataCol15 = dataDetails.Where(x => (x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308));
                        var dataCol15Usd = dataCol15.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd);
                        var dataCol15Vnd = dataCol15.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd);

                        //Data col 11 Kinh phí được cấp từ đầu năm đến quý này
                        var lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                        var lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                        //data plus 11
                        var dataPlus = dataDetails.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)));
                        var dataPlus11Usd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataPlus11Vnd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                        // data minus 11
                        var dataMinus = dataDetails.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)));
                        var dataMinus11Usd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataMinus12Vnd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var dataCol11Usd = dataPlus11Usd - dataMinus11Usd;
                        var dataCol12Vnd = dataPlus11Vnd - dataMinus12Vnd;

                        //Data col 17: Kinh phí đã giải ngân từ đầu năm đến quý này
                        lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_141, NhTongHopConstants.MA_142, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };

                        //data plus 17
                        var dataPlus17 = lstData.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)));
                        var dataPlus17Usd = dataPlus17.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                        var dataPlus17Vnd = dataPlus17.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                        var dataCol17Usd = dataPlus17Usd - dataMinus11Usd;
                        var dataCol17Vnd = dataPlus17Vnd - dataMinus12Vnd;
                        dataHandler.iID_ThanhToanID = item.IIdChungTu;
                        dataHandler.IDNhiemVuChi = (Guid)item.IIDKHTTNhiemVuChiID;
                        dataHandler.IDDuAn = (Guid)item.IIdDuAnId;
                        dataHandler.IDHopDong = (Guid)item.IIdHopDongId;
                        dataHandler.ID = item.Id;
                        dataResults.Add(dataHandler);
                    }
                }
            }
            else
            {

            }
            return dataResults;
        }
    }
}
