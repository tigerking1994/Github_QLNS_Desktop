using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy
{
    public class NhuCauChiQuyDialogViewModel : DialogAttachmentViewModelBase<NhNhuCauChiQuyModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<NhuCauChiQuyDialogViewModel> _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhNhuCauChiQuyService _service;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhNhuCauChiQuyChiTietService _nhNhuCauChiQuyChiTietService;

        public override Type ContentType => typeof(View.Forex.ForexPlan.NhuCauChiQuy.NhuCauChiQuyDialog);
        public RelayCommand ShowDetailCommand { get; }
        public RelayCommand OnCloseCommand { get; }
        public NhuCauChiQuyDetailViewModel NhuCauChiQuyDetailViewModel { get; }

        private ObservableCollection<ComboboxItem> _itemsQuy;
        public ObservableCollection<ComboboxItem> ItemsQuy
        {
            get => _itemsQuy;
            set => SetProperty(ref _itemsQuy, value);
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadDsHopDong();
                }
            }
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonNganSach;
        public ObservableCollection<NguonNganSachModel> ItemsNguonNganSach
        {
            get => _itemsNguonNganSach;
            set => SetProperty(ref _itemsNguonNganSach, value);
        }

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value))
                {
                    LoadTiGiaChiTiet();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        private ObservableCollection<NhKhTongTheModel> _itemsKHTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsKHTongThe
        {
            get => _itemsKHTongThe;
            set => SetProperty(ref _itemsKHTongThe, value);
        }

        private NhKhTongTheModel _selectedKHTongThe;
        public NhKhTongTheModel SelectedKHTongThe
        {
            get => _selectedKHTongThe;
            set
            {
                if (SetProperty(ref _selectedKHTongThe, value))
                {
                    LoadDonVi();
                }
            }
        }

        private ObservableCollection<NhDaHopDongQuery> _itemsHopDong;
        public ObservableCollection<NhDaHopDongQuery> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }

        private NhDaHopDongQuery _selectedHopDong;
        public NhDaHopDongQuery SelectedHopDong
        {
            get => _selectedHopDong;
            set => SetProperty(ref _selectedHopDong, value);
        }

        private NhDaHopDongQuery _nHDAHopDong;
        public NhDaHopDongQuery NHDAHopDong
        {
            get => _nHDAHopDong;
            set => SetProperty(ref _nHDAHopDong, value);
        }

        public bool? IsAllSelected
        {
            get
            {
                if (ItemsHopDong != null)
                {
                    var selected = ItemsHopDong.Select(x => x.isCheck).Distinct().ToList();
                    return selected.Count() == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    if (ItemsHopDong != null)
                    {
                        foreach (var model in ItemsHopDong)
                        {
                            model.isCheck = value.Value;
                        }
                    }
                    OnPropertyChanged(nameof(ItemsHopDong));
                }
            }
        }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();

        public RelayCommand SearchHongDongCommand { get; }
        public RelayCommand ResetFilterHopDongCommand { get; }


        public NhuCauChiQuyDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<NhuCauChiQuyDialogViewModel> logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INsDonViService nsDonViService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhNhuCauChiQuyService service,
            INhKhTongTheService nhKhTongTheService,
            INhNhuCauChiQuyChiTietService nhNhuCauChiQuyChiTietService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhDaHopDongService nhDaHopDongService,
            NhuCauChiQuyDetailViewModel nhuCauChiQuyDetailViewModel) : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _service = service;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDaHopDongService = nhDaHopDongService;
            NhuCauChiQuyDetailViewModel = nhuCauChiQuyDetailViewModel;
            _nhNhuCauChiQuyChiTietService = nhNhuCauChiQuyChiTietService;
            ShowDetailCommand = new RelayCommand(obj => OnOpenShowDetail(obj));
            OnCloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            SearchHongDongCommand = new RelayCommand(obj => LoadDsHopDong());
            ResetFilterHopDongCommand = new RelayCommand(obj => onResetFilter());
        }

        public override void Init()
        {
            base.Init();
            NHDAHopDong = new NhDaHopDongQuery();
            LoadQuy();
            LoadNguonNganSach();
            LoadTiGia();
            LoadKHTongThe();
            LoadDonVi();
            LoadData();
        }

        private void LoadQuy()
        {
            _itemsQuy = new ObservableCollection<ComboboxItem>();
            for (int i = 1; i <= 4; i++)
            {
                ComboboxItem quy = new ComboboxItem("Quý " + i, i.ToString());
                _itemsQuy.Add(quy);
            }
            OnPropertyChanged(nameof(ItemsQuy));
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            if (_selectedKHTongThe != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> data = _nhKhTongTheNhiemVuChiService.FindAllDonViByKhTongTheId(_selectedKHTongThe.Id).Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadDsHopDong()
        {
            _itemsHopDong = new ObservableCollection<NhDaHopDongQuery>();
            var lstChiTiet = _nhNhuCauChiQuyChiTietService.FindByIdChiQuy(Model.Id).Where(x => x.IIdHopDongId != null);
            if (_selectedDonVi != null)
            {
                List<NhDaHopDongQuery> data = _nhDaHopDongService.FindByIdDonVi(_selectedDonVi.Id).ToList();
                if (NHDAHopDong.SSoHopDong != null)
                {
                    data = data.Where(x => x.SSoHopDong != null ? x.SSoHopDong.Contains(NHDAHopDong.SSoHopDong) : x.SSoHopDong != null).ToList();
                }
                if (NHDAHopDong.STenHopDong != null)
                {
                    data = data.Where(x => x.STenHopDong != null ? x.STenHopDong.Contains(NHDAHopDong.STenHopDong) : x.STenHopDong != null).ToList();
                }
                if (NHDAHopDong.DNgayHopDong.HasValue)
                {
                    data = data.Where(x => x.DNgayHopDong.HasValue ? x.DNgayHopDong.Value.Date == NHDAHopDong.DNgayHopDong.Value.Date : x.DNgayHopDong.HasValue).ToList();
                }
                if(lstChiTiet != null)
                {
                    foreach (var item in data)
                    {
                        if (lstChiTiet.Any(x => x.IIdHopDongId == item.Id)) item.isCheck = true;
                    }
                }
                
                _itemsHopDong = _mapper.Map<ObservableCollection<NhDaHopDongQuery>>(data);
               
            }
            OnPropertyChanged(nameof(ItemsHopDong));
        }
        private void onResetFilter()
        {
            NHDAHopDong = new NhDaHopDongQuery();
            LoadDsHopDong();
            OnPropertyChanged(nameof(NHDAHopDong));
        }
        private void LoadNguonNganSach()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonNganSach = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonNganSach));
        }

        private void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            _itemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void LoadKHTongThe()
        {
            //IEnumerable<NhKhTongTheQuery> data = _nhKhTongTheService.FindAllOverview();
            //_itemsKHTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            //OnPropertyChanged(nameof(ItemsKHTongThe));
             IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(x => x.DNgayTao).ThenBy(x => x.DNgaySua);
            //data.OrderByDescending(x => x.DNgayTao);
            _itemsKHTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsKHTongThe.ForAll(s =>
            {
                if (s.ILoai == Loai_KHTT.GIAIDOAN)
                {
                    //s.TenKeHoach = $"KHTT {s.IGiaiDoanTu} - {s.IGiaiDoanDen} - Số KH: {s.SSoKeHoachBqp}";
                    s.TenKeHoach = $"KHTT {s.IGiaiDoanTu_BQP} - {s.IGiaiDoanDen_BQP} - Số KH: {s.SSoKeHoachBqp}";
                }
                else
                {
                    s.TenKeHoach = $"KHTT {s.INamKeHoach} - Số KH: {s.SSoKeHoachBqp}";
                }
            });
            OnPropertyChanged(nameof(ItemsKHTongThe));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                Title = "NHU CẦU CHI QUÝ";
                Description = "Thêm mới nhu cầu chi quý";
            }
            else
            {
                Title = "NHU CẦU CHI QUÝ";
                Description = "Cập nhật nhu cầu chi quý";

                NhNhuCauChiQuy entity = _service.FindById(Model.Id);
                Model = _mapper.Map<NhNhuCauChiQuyModel>(entity);
                SelectedKHTongThe = ItemsKHTongThe.FirstOrDefault(x => x.Id == Model.IIdKHTongTheID);
                SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonVi));
                // Load tỉ giá và ngoại tệ khác
                SelectedTiGia = ItemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                LoadTiGiaChiTiet();
                SelectedTiGiaChiTiet = ItemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            }
        }

        private void ConverData()
        {
            if (SelectedDonVi != null)
            {
                Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                Model.IIdDonViId = SelectedDonVi.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }
            if (SelectedKHTongThe != null)
            {
                Model.IIdKHTongTheID = SelectedKHTongThe.Id;
            }
        }

        //private bool ValidateData()
        //{
        //    if (string.IsNullOrEmpty(Model.SSoDeNghi))
        //    {
        //        MessageBoxHelper.Error("Nhập số đề nghị");
        //        return false;
        //    }
        //    if (Model.DNgayDeNghi == null)
        //    {
        //        MessageBoxHelper.Error("Nhập ngày đề nghị");
        //        return false;
        //    }
        //    if (Model.INamKeHoach == null)
        //    {
        //        MessageBoxHelper.Error("Nhập năm kế hoạch");
        //        return false;
        //    }
        //    if (Model.IQuy == null)
        //    {
        //        MessageBoxHelper.Error("Chọn quý");
        //        return false;
        //    }
        //    if (SelectedDonVi == null)
        //    {
        //        MessageBoxHelper.Error("Chọn đơn vị");
        //        return false;
        //    }
        //    if (Model.IIdNguonVonId == null)
        //    {
        //        MessageBoxHelper.Error("Chọn nguồn vốn");
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(Model.SNguoiLap))
        //    {
        //        MessageBoxHelper.Error("Nhập người lập kế hoạch");
        //        return false;
        //    }
        //    if (SelectedTiGia == null)
        //    {
        //        MessageBoxHelper.Error("Chọn tỉ giá");
        //        return false;
        //    }
        //    if (SelectedTiGiaChiTiet == null)
        //    {
        //        MessageBoxHelper.Error("Chọn tỉ giá chi tiết");
        //        return false;
        //    }
        //    return true;
        //}

        private void OnOpenShowDetail(object obj)
        {
            ConverData();
            //Model.IIdDonViId = SelectedDonVi.Id;
            //Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
            if (!ValidateViewModelHelper.Validate(Model)) return;
            //if (!ValidateData()) return;
            if (obj is Window window)
            {
                window.Close();
            }
            NhuCauChiQuyDetailViewModel.IsEditable = true;
            NhuCauChiQuyDetailViewModel.Model = Model;
            NhuCauChiQuyDetailViewModel.IsDetail = false;
            NhuCauChiQuyDetailViewModel.iTiGia = SelectedTiGia != null ? SelectedTiGia.Id : Guid.Empty;
            NhuCauChiQuyDetailViewModel.ItemsHopDong = ItemsHopDong != null ? _mapper.Map<ObservableCollection<NhDaHopDongQuery>>(ItemsHopDong.Where(x => x.isCheck).ToList()) : null;
            NhuCauChiQuyDetailViewModel.TenDonVi = ItemsDonVi.FirstOrDefault(x => x.Id == SelectedDonVi.Id).TenDonVi;
            NhuCauChiQuyDetailViewModel.TenKeHoach = ItemsKHTongThe.FirstOrDefault(x => x.Id == SelectedKHTongThe.Id).TenKeHoach;
            NhuCauChiQuyDetailViewModel.CurrencyExchangeAction = (obj, propName) => NhuCauChiQuyChiTietCurrencyExChange(obj, propName);
            NhuCauChiQuyDetailViewModel.SavedAction = obj => this.OnCloseWindow(obj);
            NhuCauChiQuyDetailViewModel.Init();
            NhuCauChiQuyDetailViewModel.ShowDialog();
        }

        private void NhuCauChiQuyChiTietCurrencyExChange(object sender, string propName)
        {
            if (SelectedTiGia != null)
            {
                NhNhuCauChiQuyChiTietModel objectSender = (NhNhuCauChiQuyChiTietModel)sender;
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (propName)
                {
                    case nameof(NhNhuCauChiQuyChiTietModel.FNhuCauQuyNayVnd):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objectSender.FNhuCauQuyNayVnd.Value;
                        break;
                    case nameof(NhNhuCauChiQuyChiTietModel.FNhuCauQuyNayEur):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = objectSender.FNhuCauQuyNayEur.Value;
                        break;
                    case nameof(NhNhuCauChiQuyChiTietModel.FNhuCauQuyNayNgoaiTeKhac):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC;
                        value = objectSender.FNhuCauQuyNayNgoaiTeKhac.Value;
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objectSender.FNhuCauQuyNayUsd.Value;
                        break;
                }
                objectSender.FNhuCauQuyNayVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FNhuCauQuyNayEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FNhuCauQuyNayUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                objectSender.FNhuCauQuyNayNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
            }
        }

        private void OnCloseWindow(object obj)
        {
            SavedAction?.Invoke(Model);
            if (obj is Window window)
            {
                window.Close();
            }
        }
    }
}
