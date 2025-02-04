using AutoMapper;
using log4net;
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
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ChuyenDuLieuQuyetToan;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ChuyenDuLieuQuyetToan
{
    public class ChuyenDulieuQuyetToanDetailViewModel: DetailViewModelBase<NhQtChuyenQuyetToanModel, NhQtChuyenQuyetToanChiTietModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhQtChuyenQuyetToanService _service;
        private readonly INhQtChuyenQuyetToanChiTietService _serviceChiTiet;
        private readonly INsDonViService _nsDonViService;
        private ICollectionView _itemsCollectionView;

        public override string Name => "Chuyển dữ liệu quyết toán";
        public override string Title => "Chi tiết chuyển dữ liệu quyết toán";
        public override string Description => "Chi tiết chuyển dữ liệu quyết toán";
        public override Type ContentType => typeof(ChuyenDuLieuQuyetToanDetail);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiThoiGian;
        public ObservableCollection<ComboboxItem> ItemsLoaiThoiGian
        {
            get => _itemsLoaiThoiGian;
            set => SetProperty(ref _itemsLoaiThoiGian, value);
        }

        private ObservableCollection<ComboboxItem> _itemsThoiGian;
        public ObservableCollection<ComboboxItem> ItemsThoiGian
        {
            get => _itemsThoiGian;
            set => SetProperty(ref _itemsThoiGian, value);
        }

        private NhQtChuyenQuyetToanChiTietModel _itemsMLNSFilter;
        public NhQtChuyenQuyetToanChiTietModel ItemsMLNSFilter
        {
            get => _itemsMLNSFilter;
            set => SetProperty(ref _itemsMLNSFilter, value);
        }

        public RelayCommand SearchCommand { get; }

        public ChuyenDulieuQuyetToanDetailViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhQtChuyenQuyetToanService service,
            INhQtChuyenQuyetToanChiTietService serviceChiTiet)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = service;
            _serviceChiTiet = serviceChiTiet;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadLoaiThoiGian();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsMLNSFilter = new NhQtChuyenQuyetToanChiTietModel();
        }

        private void LoadDonVi()
        {
            try
            {
                var data = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadLoaiThoiGian()
        {
            _itemsLoaiThoiGian = new ObservableCollection<ComboboxItem>();
            _itemsLoaiThoiGian.Add(new ComboboxItem("Tháng", "1"));
            _itemsLoaiThoiGian.Add(new ComboboxItem("Quý", "2"));
        }

        private void LoadThoiGian()
        {
            _itemsThoiGian = new ObservableCollection<ComboboxItem>();
            if (Model.iLoaiThoiGian.HasValue)
            {
                switch (Model.iLoaiThoiGian.Value)
                {
                    case 1:
                        ComboboxItem month;
                        for (int i = 1; i <= 12; i++)
                        {
                            month = new ComboboxItem("Tháng " + i, i.ToString());
                            _itemsThoiGian.Add(month);
                        }
                        break;
                    case 2:
                        ComboboxItem quy;
                        for (int i = 1; i <= 4; i++)
                        {
                            quy = new ComboboxItem(LoaiQuyEnum.Get(i), i.ToString());
                            _itemsThoiGian.Add(quy);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                NhQtChuyenQuyetToan entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhQtChuyenQuyetToanModel>(entity);
                LoadThoiGian();

                Items = new ObservableCollection<NhQtChuyenQuyetToanChiTietModel>();
                var data = _serviceChiTiet.FindAll()
                    .Where(x => x.iID_ChuyenQuyetToanID.HasValue && x.iID_ChuyenQuyetToanID.Value.Equals(Model.Id))
                    .OrderBy(s => s.sXauNoiMa);
                Items = _mapper.Map<ObservableCollection<NhQtChuyenQuyetToanChiTietModel>>(data);

                _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                _itemsCollectionView.Filter = Items_Filter;
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhQtChuyenQuyetToanChiTietModel item)
            {
                bool result = true;
                if (ItemsMLNSFilter != null)
                {
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sLNS)) result = result && item.sLNS.ToLower().Contains(ItemsMLNSFilter.sLNS.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sL)) result = result && item.sL.ToLower().Contains(ItemsMLNSFilter.sL.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sK)) result = result && item.sK.ToLower().Contains(ItemsMLNSFilter.sK.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sM)) result = result && item.sM.ToLower().Contains(ItemsMLNSFilter.sM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTM)) result = result && item.sTM.ToLower().Contains(ItemsMLNSFilter.sTM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTTM)) result = result && item.sTTM.ToLower().Contains(ItemsMLNSFilter.sTTM.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sNG)) result = result && item.sNG.ToLower().Contains(ItemsMLNSFilter.sNG.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sTNG)) result = result && item.sTNG.ToLower().Contains(ItemsMLNSFilter.sTNG.Trim().ToLower());
                    if (!string.IsNullOrWhiteSpace(ItemsMLNSFilter.sMoTa)) result = result && item.sMoTa.ToLower().Contains(ItemsMLNSFilter.sMoTa.Trim().ToLower());
                }
                return result;
            }
            return false;
        }

        public override void OnClosing()
        {
            // Clear items
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
