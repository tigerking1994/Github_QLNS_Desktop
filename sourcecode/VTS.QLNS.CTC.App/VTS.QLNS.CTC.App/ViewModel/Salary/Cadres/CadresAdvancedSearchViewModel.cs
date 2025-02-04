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
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.Cadres
{
    public class CadresAdvancedSearchViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly ITlDmDonViService _tlDmDonViService;

        public ICollectionView DoiTuongView { get; set; }
        public ObservableCollection<CadresModel> LstCadres { get; set; }
        public Action<object> SearchAction;

        public override Type ContentType => typeof(View.Salary.Cadres.CadresAdvancedSearch);
        public override PackIconKind IconKind => PackIconKind.ClipboardSearchOutline;
        public override string Title => "Tìm kiếm đối tượng hưởng lương, phụ cấp nâng cao";
        public override string Description => "Tìm kiếm đối tượng hưởng lương, phụ cấp nâng cao";

        private ObservableCollection<TlDmCapBacModel> _itemsCapBac;
        public ObservableCollection<TlDmCapBacModel> ItemsCapBac
        {
            get => _itemsCapBac;
            set => SetProperty(ref _itemsCapBac, value);
        }

        private TlDmCapBacModel _selectedCapBac;
        public TlDmCapBacModel SelectedCapBac
        {
            get => _selectedCapBac;
            set => SetProperty(ref _selectedCapBac, value);
        }

        private ObservableCollection<TlDmChucVuModel> _itemsChucVu;
        public ObservableCollection<TlDmChucVuModel> ItemsChucVu
        {
            get => _itemsChucVu;
            set => SetProperty(ref _itemsChucVu, value);
        }

        private TlDmChucVuModel _selectedChucVu;
        public TlDmChucVuModel SelectedChucVu
        {
            get => _selectedChucVu;
            set => SetProperty(ref _selectedChucVu, value);
        }

        private ObservableCollection<TlDmDonViModel> _donviData;
        public ObservableCollection<TlDmDonViModel> DonViItems
        {
            get => _donviData;
            set => SetProperty(ref _donviData, value);
        }

        private TlDmDonViModel _selectedDonVi;
        public TlDmDonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private string _searchSoTaiKhoan;
        public string SearchSoTaiKhoan
        {
            get => _searchSoTaiKhoan;
            set => SetProperty(ref _searchSoTaiKhoan, value);
        }

        private DateTime? _fromNgayNn;
        public DateTime? FromNgayNn
        {
            get => _fromNgayNn;
            set => SetProperty(ref _fromNgayNn, value);
        }

        private DateTime? _toNgayNn;
        public DateTime? ToNgayNn
        {
            get => _toNgayNn;
            set => SetProperty(ref _toNgayNn, value);
        }

        private string _fromNtn;
        public string FromNtn
        {
            get => _fromNtn;
            set => SetProperty(ref _fromNtn, value);
        }

        private string _toNtn;
        public string ToNtn
        {
            get => _toNtn;
            set => SetProperty(ref _toNtn, value);
        }

        private string _searchMaHuongLuong;
        public string SearchMaHuongLuong
        {
            get => _searchMaHuongLuong;
            set => SetProperty(ref _searchMaHuongLuong, value);
        }

        private string _searchTenDoiTuong;
        public string SearchTenDoiTuong
        {
            get => _searchTenDoiTuong;
            set => SetProperty(ref _searchTenDoiTuong, value);
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
            set => SetProperty(ref _yearSelected, value);
        }

        public RelayCommand SearchCommand { get; }

        public CadresAdvancedSearchViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            ITlDmDonViService tlDmDonViService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;

            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _tlDmDonViService = tlDmDonViService;

            SearchCommand = new RelayCommand(o => OnSearch());
        }

        public override void Init()
        {
            base.Init();
            DoiTuongView = CollectionViewSource.GetDefaultView(LstCadres);
            DoiTuongView.Filter = CanBoFilter;
        }

        private void OnSearch()
        {
            if (SelectedDonVi == null)
            {
                SelectedDonVi = DonViItems.FirstOrDefault(x => x.Id.Equals(Guid.Empty));
            }

            DoiTuongView.Refresh();
            SearchAction?.Invoke(DoiTuongView);
            DialogHost.Close("RootDialog");
            DoiTuongView = CollectionViewSource.GetDefaultView(LstCadres);
            DoiTuongView.Filter = CanBoFilter;
        }

        private bool CanBoFilter(object obj)
        {
            bool result = true;
            var item = (CadresModel)obj;
            if (SelectedDonVi != null && !SelectedDonVi.Id.Equals(Guid.Empty))
            {
                result &= item.Parent == SelectedDonVi.MaDonVi;
            }
            if (MonthSelected != null)
            {
                result &= item.Thang == int.Parse(MonthSelected.ValueItem);
            }
            if (YearSelected != null)
            {
                result &= item.Nam == int.Parse(YearSelected.ValueItem);
            }
            if (!string.IsNullOrEmpty(SearchMaHuongLuong))
            {
                result &= item.MaCanBo.ToLower().Contains(SearchMaHuongLuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchTenDoiTuong))
            {
                result &= item.TenCanBo.ToLower().Contains(SearchTenDoiTuong.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchSoTaiKhoan))
            {
                result &= item.SoTaiKhoan != null && item.SoTaiKhoan.ToLower().Contains(SearchSoTaiKhoan.ToLower());
            }
            if (SelectedChucVu != null)
            {
                result &= !string.IsNullOrEmpty(item.MaCv) && item.MaCv.Equals(SelectedChucVu.MaCv);
            }
            if (SelectedCapBac != null)
            {
                result &= item.MaCb.Equals(SelectedCapBac.MaCb);
            }
            if (FromNgayNn != null && ToNgayNn == null)
            {
                result &= item.NgayNn >= FromNgayNn;
            }
            if (FromNgayNn == null && ToNgayNn != null)
            {
                result &= item.NgayNn <= ToNgayNn;
            }
            if (FromNgayNn != null && ToNgayNn != null)
            {
                result &= item.NgayNn >= FromNgayNn && item.NgayNn <= ToNgayNn;
            }
            if (!string.IsNullOrEmpty(FromNtn) && string.IsNullOrEmpty(ToNtn))
            {
                int iFromNtn;
                if (int.TryParse(FromNtn, out iFromNtn))
                {
                    result &= item.NamTn >= int.Parse(FromNtn);
                }
            }
            if (string.IsNullOrEmpty(FromNtn) && !string.IsNullOrEmpty(ToNtn))
            {
                int iFromNtn;
                if (int.TryParse(ToNtn, out iFromNtn))
                {
                    result &= item.NamTn <= int.Parse(ToNtn);
                }
            }
            if (!string.IsNullOrEmpty(FromNtn) && !string.IsNullOrEmpty(ToNtn))
            {
                int iFromNtn;
                int iToNtn;
                if (int.TryParse(ToNtn, out iFromNtn) && int.TryParse(FromNtn, out iToNtn))
                {
                    result &= item.NamTn >= int.Parse(FromNtn) && item.NamTn <= int.Parse(ToNtn);
                }
            }

            return result;
        }
    }
}
