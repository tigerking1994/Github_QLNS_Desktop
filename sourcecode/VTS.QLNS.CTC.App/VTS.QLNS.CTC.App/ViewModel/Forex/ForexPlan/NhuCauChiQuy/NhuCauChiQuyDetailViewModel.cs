using AutoMapper;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Markup;
using DataGrid = System.Windows.Controls.DataGrid;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexPlan.NhuCauChiQuy
{
    public class NhuCauChiQuyDetailViewModel : DetailViewModelBase<NhNhuCauChiQuyModel, NhNhuCauChiQuyChiTietModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<NhuCauChiQuyDetailViewModel> _logger;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhNhuCauChiQuyChiTietService _nhNhuCauChiQuyChiTietService;
        //private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhNhuCauChiQuyService _nhNhuCauChiQuyService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly IServiceProvider _provider;
        private readonly INhThTongHopService _nhThTongHopService;


        public override Type ContentType => typeof(View.Forex.ForexPlan.NhuCauChiQuy.NhuCauChiQuyDetail);
        public override string Description => string.Format("Số đề nghị: {0} - Ngày đề nghị: {1} - Năm: {2} | Đơn vị: {3}", Model.SSoDeNghi, Model.DNgayDeNghi.Value.ToString("dd/mm/yyyy"), Model.INamKeHoach, TenDonVi);

        public string TenDonVi { get; set; }
        public string TenKeHoach { get; set; }
        public Action<object, string> CurrencyExchangeAction { get; set; }
        public bool IsDetail { get; set; }
        public bool IsTongHop => Model.STongHop + "" == "" && IsEditable;

        public RelayCommand AddHopDongCommand { get; }
        public RelayCommand AddNoiDungCommand { get; }
        public RelayCommand DeleteItemCommand { get; }

        private ObservableCollection<NhNhuCauChiQuyChiTietModel> itemsChiQuyChiTiet = new ObservableCollection<NhNhuCauChiQuyChiTietModel>();
        public ObservableCollection<NhNhuCauChiQuyChiTietModel> ItemsChiQuyChiTiet
        {
            get => itemsChiQuyChiTiet;
            set => SetProperty(ref itemsChiQuyChiTiet, value);
        }

        public bool HopDong => true;
        public Guid? iTiGia { get; set; }

        private NhNhuCauChiQuyChiTietModel _selectedChiQuyChiTiet;
        public NhNhuCauChiQuyChiTietModel SelectedChiQuyChiTiet
        {
            get => _selectedChiQuyChiTiet;
            set => SetProperty(ref _selectedChiQuyChiTiet, value);
        }

        private List<ComboboxItem> _itemsNoiDungChi;
        public List<ComboboxItem> ItemsNoiDungChi
        {
            get => _itemsNoiDungChi;
            set => SetProperty(ref _itemsNoiDungChi, value);
        }

        private ComboboxItem _selectedNoiDungChi;
        public ComboboxItem SelectedNoiDungChi
        {
            get => _selectedNoiDungChi;
            set => SetProperty(ref _selectedNoiDungChi, value);
        }

        private int currentRow = -1;
        public bool IsSaveData => ItemsChiQuyChiTiet.Any(x => x.IsModified || x.IsDeleted);

        public bool IsEditable { get; internal set; }

        private ObservableCollection<NhDaHopDongQuery> _itemsHopDong;
        public ObservableCollection<NhDaHopDongQuery> ItemsHopDong
        {
            get => _itemsHopDong;
            set => SetProperty(ref _itemsHopDong, value);
        }

        public RelayCommand OpenReferencePopupCommand { get; }

        public NhuCauChiQuyDetailViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<NhuCauChiQuyDetailViewModel> logger,
            INhDaHopDongService nhDaHopDongService,
            INhNhuCauChiQuyChiTietService nhNhuCauChiQuyChiTietService,
            INhNhuCauChiQuyService nhNhuCauChiQuyService,
            INhDaDuAnService nhDaDuAnService,
            INhDmTiGiaService nhDmTiGiaService,
            IServiceProvider serviceProvider,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhThTongHopService nhThTongHopService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nhDaHopDongService = nhDaHopDongService;
            _nhNhuCauChiQuyChiTietService = nhNhuCauChiQuyChiTietService;
            _nhNhuCauChiQuyService = nhNhuCauChiQuyService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _provider = serviceProvider;
            _nhThTongHopService = nhThTongHopService;
            AddHopDongCommand = new RelayCommand(obj => OnAddHopDong());
            AddNoiDungCommand = new RelayCommand(obj => OnAddNoiDung());
            DeleteItemCommand = new RelayCommand(obj => OnDeleteItems());
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
        }

        public override void Init()
        {
            base.Init();
            LoadNoiDungChi();
            LoadData();
            //LoadLoaiNoiDungChi();
            //LoadTenNHiemVuChi();
        }
        public void NhuCauChiQuyChiTiet_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedItem = (NhNhuCauChiQuyChiTietModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhNhuCauChiQuyChiTietModel.SNoiDung)))
            {
                e.Cancel = SelectedItem.IIdHopDongId != null;
            }
        }

        private void LoadNoiDungChi()
        {
            var data = _nhDaHopDongService.FindAll().Where(x => x.IIdDonViQuanLyId == Model.IIdDonViId).OrderBy(x => x.DNgayTao);
            var dataModel = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(data).ToList();
            _itemsNoiDungChi = dataModel.Select(x => new ComboboxItem() { DisplayItem = x.STenHopDong, ValueItem = x.ILoai.ToString(), HiddenValue = x.Id.ToString(), DisplayItemOption2 = x.IIdKhTongTheNhiemVuChiId.ToString() }).ToList();
            OnPropertyChanged(nameof(ItemsNoiDungChi));
        }

        public override void LoadData(params object[] args)
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                var data = _nhNhuCauChiQuyChiTietService.FindByIdChiQuy(Model.Id);
                itemsChiQuyChiTiet = _mapper.Map<ObservableCollection<NhNhuCauChiQuyChiTietModel>>(data);
                var dataTongHops = LoadDataTongHop();
                foreach (var item in itemsChiQuyChiTiet)
                {
                    var dataNhiemVuChi = _nhNhuCauChiQuyChiTietService.FindNhiemVuChiByIdDonVi(Model.IIdDonViId);
                    item.ItemsTenNhiemVuChi = _mapper.Map<List<ComboboxItem>>(dataNhiemVuChi);
                    var dataDuAn = _nhDaDuAnService.FindAll();
                    //item.ItemsTenDuAn = dataDuAn.Select(x => new ComboboxItem() { ValueItem = x.Id.ToString(), DisplayItem = x.STenDuAn }).ToList();
                    var hopDong = _nhDaHopDongService.FindAll().FirstOrDefault(x => x.Id == item.IIdHopDongId);
                    if (hopDong != null)
                    {
                        LoadLoaiNoiDungChi(hopDong.ILoai.ToString(), item);
                        var dataHopDongTH = dataTongHops.Where(x => x.iID_HopDongID.Equals(item.IIdHopDongId));
                        item.FKinhPhiDuocCapCacNamTruocSetUSD = dataHopDongTH.Sum(x => x.KinhPhiUSD);
                        item.FKinhPhiDuocCapCacNamTruocSetVND = dataHopDongTH.Sum(x => x.KinhPhiVND);
                        item.FKinhPhiDaChiCacNamTruocSetUSD = dataHopDongTH.Sum(x => x.KinhPhiDaChiUSD);
                        item.FKinhPhiDaChiCacNamTruocSetVND = dataHopDongTH.Sum(x => x.KinhPhiDaChiVND);
                        item.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = dataHopDongTH.Sum(x => x.KinhPhiToYUSD);
                        item.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = dataHopDongTH.Sum(x => x.KinhPhiToYVND);
                        item.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = dataHopDongTH.Sum(x => x.KinhPhiDaChiToYUSD);
                        item.FKinhPhiDaChiDenCuoiQuyTruocSetVND = dataHopDongTH.Sum(x => x.KinhPhiDaChiToYVND);
                    }
                    else
                    {
                        LoadLoaiNoiDungChi(item.ILoaiNoiDungChi.ToString(), item);
                    }
                    if (item.IIdDuAnId != null)
                    {
                        var itemDuAn = _nhDaDuAnService.FindById(item.IIdDuAnId.Value);
                        item.STenDuAn = itemDuAn != null ? itemDuAn.STenDuAn : string.Empty;
                        if (item.IIdHopDongId.IsNullOrEmpty())
                        {
                            var dataDuAnTH = dataTongHops.Where(x => x.iID_DuAnID.Equals(item.IIdDuAnId));
                            item.FKinhPhiDuocCapCacNamTruocSetUSD = dataDuAnTH.Sum(x => x.KinhPhiUSD);
                            item.FKinhPhiDuocCapCacNamTruocSetVND = dataDuAnTH.Sum(x => x.KinhPhiVND);
                            item.FKinhPhiDaChiCacNamTruocSetUSD = dataDuAnTH.Sum(x => x.KinhPhiDaChiUSD);
                            item.FKinhPhiDaChiCacNamTruocSetVND = dataDuAnTH.Sum(x => x.KinhPhiDaChiVND);
                            item.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = dataDuAnTH.Sum(x => x.KinhPhiToYUSD);
                            item.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = dataDuAnTH.Sum(x => x.KinhPhiToYVND);
                            item.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = dataDuAnTH.Sum(x => x.KinhPhiDaChiToYUSD);
                            item.FKinhPhiDaChiDenCuoiQuyTruocSetVND = dataDuAnTH.Sum(x => x.KinhPhiDaChiToYVND);
                        }
                    }
                    item.HopDong = item.IIdHopDongId != null;
                    item.NoiDung = item.IIdHopDongId == null;
                    /*if (!ItemsHopDong.Any(x => x.Id == item.IIdHopDongId) && item.IIdHopDongId != null)
                    {
                        item.IsDeleted = true;
                    }*/
                    //ItemsHopDong?? chưa có chỗ nào gán dữ liệu cho ItemsHopDong
                    item.PropertyChanged += Detail_PropertyChanged;
                }
            }
            //ItemsHopDong?? chưa có chỗ nào gán dữ liệu cho ItemsHopDong nên đoạn if ở dưới vô nghĩa
            if (ItemsHopDong != null)
            {
                foreach (var item in ItemsHopDong)
                {
                    if (!itemsChiQuyChiTiet.Any(x => x.IIdHopDongId == item.Id))
                    {
                        NhNhuCauChiQuyChiTietModel itemHopDong = new NhNhuCauChiQuyChiTietModel();
                        itemHopDong.HopDong = true;
                        itemHopDong.NoiDung = false;
                        itemHopDong.IIdNhuCauChiQuyId = !Model.Id.IsNullOrEmpty() ? Model.Id : Guid.Empty;
                        itemHopDong.IIdHopDongId = item.Id;
                        itemHopDong.SNoiDung = item.STenHopDong;
                        itemHopDong.IIdDuAnId = item.IIdDuAnId;
                        itemHopDong.IIdKhttNhiemVuChiId = item.IIdKhTongTheNhiemVuChiId;
                        //itemHopDong.IsAdded = true;
                        //itemHopDong.Id = Guid.NewGuid();
                        var noiDungChi = ItemsNoiDungChi.FirstOrDefault(x => itemHopDong.IIdHopDongId.ToString().Equals(x.HiddenValue));

                        LoadTenNHiemVuChi(string.IsNullOrEmpty(noiDungChi.DisplayItemOption2) ? Guid.Empty : Guid.Parse(noiDungChi.DisplayItemOption2), itemHopDong);
                        itemHopDong.SelectedTenNhiemVuChi = itemHopDong.ItemsTenNhiemVuChi.Where(x => x.HiddenValue == item.IIdKhTongTheNhiemVuChiId + "").FirstOrDefault();
                        itemHopDong.ILoaiNoiDungChi = item.ILoai;
                        LoadLoaiNoiDungChi(noiDungChi.ValueItem, itemHopDong);
                        if (item.IIdDuAnId != null)
                        {
                            itemHopDong.STenDuAn = item.STenDuAn;
                        }
                        itemHopDong.FNoiDungChiUSD = item.FGiaTriUsd;
                        itemHopDong.FNoiDungChiVND = item.FGiaTriVnd;

                        NhNhuCauChiQuyChiTietModel FLoatData = _mapper.Map<NhNhuCauChiQuyChiTietModel>(_nhNhuCauChiQuyChiTietService.FindByIdHopDong(item.Id));
                        if (FLoatData != null)
                        {
                            itemHopDong.FKinhPhiDuocCapCacNamTruocSetUSD = FLoatData.FKinhPhiDuocCapCacNamTruocSetUSD;
                            itemHopDong.FKinhPhiDuocCapCacNamTruocSetVND = FLoatData.FKinhPhiDuocCapCacNamTruocSetVND;
                            itemHopDong.FKinhPhiDuocCapCacNamTruocSetEUR = FLoatData.FKinhPhiDuocCapCacNamTruocSetEUR;
                            itemHopDong.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac;
                            itemHopDong.FKinhPhiDaChiCacNamTruocSetUSD = FLoatData.FKinhPhiDaChiCacNamTruocSetUSD;
                            itemHopDong.FKinhPhiDaChiCacNamTruocSetVND = FLoatData.FKinhPhiDaChiCacNamTruocSetVND;
                            itemHopDong.FKinhPhiDaChiCacNamTruocSetEUR = FLoatData.FKinhPhiDaChiCacNamTruocSetEUR;
                            itemHopDong.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac;
                            itemHopDong.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD;
                            itemHopDong.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetVND;
                            itemHopDong.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR;
                            itemHopDong.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac;
                            itemHopDong.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetUSD;
                            itemHopDong.FKinhPhiDaChiDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetVND;
                            itemHopDong.FKinhPhiDaChiDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetEUR;
                            itemHopDong.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac;
                        }
                        itemHopDong.PropertyChanged += Detail_PropertyChanged;
                        ItemsChiQuyChiTiet.Add(itemHopDong);
                    }
                }

                ItemsChiQuyChiTiet = _mapper.Map<ObservableCollection<NhNhuCauChiQuyChiTietModel>>(ItemsChiQuyChiTiet.ToList().OrderBy(x => x.SNoiDung));
                OnPropertyChanged(nameof(ItemsChiQuyChiTiet));
            }
            
        }

        private void LoadLoaiNoiDungChi(string noiDungChi, NhNhuCauChiQuyChiTietModel item)
        {

            if (noiDungChi == "1" || noiDungChi == "2" || noiDungChi == "3")
            {
                ComboboxItem ngoaiTe = new ComboboxItem("Chi bằng ngoại tệ", "1");
                item.ItemsLoaiNoiDungChi = new List<ComboboxItem>() { ngoaiTe };              
                item.SelectedLoaiNoiDungChi = ngoaiTe;
            }
            if (noiDungChi == "4" || noiDungChi == "5")
            {
                ComboboxItem noiTe = new ComboboxItem("Chi bằng nội tệ", "0");
                item.ItemsLoaiNoiDungChi = new List<ComboboxItem>() { noiTe };
                item.SelectedLoaiNoiDungChi = noiTe;
            }
            if (noiDungChi == "0")
            {
                ComboboxItem noiTe = new ComboboxItem("Chi bằng nội tệ", "0");
                ComboboxItem ngoaiTe = new ComboboxItem("Chi bằng ngoại tệ", "1");
                item.ItemsLoaiNoiDungChi = new List<ComboboxItem>() { noiTe, ngoaiTe };
            }
        }

        private void LoadTenNHiemVuChi(Guid IdKhTTNhiemVuChi, NhNhuCauChiQuyChiTietModel item)
        {
            var data = _nhNhuCauChiQuyChiTietService.FindNhiemVuChiByIdDonVi(Model.IIdDonViId);
            if (!IdKhTTNhiemVuChi.IsNullOrEmpty())
            {
                var dataMap = data.Where(x => x.Id == IdKhTTNhiemVuChi).ToList();
                item.ItemsTenNhiemVuChi = _mapper.Map<List<ComboboxItem>>(dataMap);
            }
            else
            {
                if (item.IIdKhttNhiemVuChiId == null)
                {
                    item.ItemsTenNhiemVuChi = _mapper.Map<List<ComboboxItem>>(data);
                }
                else
                {
                    data = data.Where(x => x.IIdDuAnId == item.IIdDuAnId);
                    item.ItemsTenNhiemVuChi = _mapper.Map<List<ComboboxItem>>(data);
                }
            }
        }

        //private void LoadDuAn(Guid idDonVi, Guid iIdDuAn, bool hopDong, NhNhuCauChiQuyChiTietModel item)
        //{
        //    List<NhDaDuAn> data = new List<NhDaDuAn>();
        //    if (hopDong && iIdDuAn != null)
        //    {
        //        data = _nhDaDuAnService.FindAll(x => x.Id == iIdDuAn).ToList();
        //        item.ItemsTenDuAn = data.Select(x => new ComboboxItem() { ValueItem = x.Id.ToString(), DisplayItem = x.STenDuAn }).ToList();
        //    }
        //    if (hopDong && iIdDuAn.IsNullOrEmpty())
        //    {
        //        data = null;
        //        item.ItemsTenDuAn = null;
        //    }
        //    if (!hopDong)
        //    {
        //        data = _nhDaDuAnService.FindAll(x => x.IIdDonViQuanLyId == idDonVi).ToList();
        //        item.ItemsTenDuAn = data.Select(x => new ComboboxItem() { ValueItem = x.Id.ToString(), DisplayItem = x.STenDuAn }).ToList();
        //        var dataNhiemVuChi = _nhNhuCauChiQuyChiTietService.FindNhiemVuChiByIdDonVi(Model.IIdDonViId);
        //        item.ItemsTenNhiemVuChi = _mapper.Map<List<ComboboxItem>>(dataNhiemVuChi);
        //    }         

        //}

        private void OnAddHopDong()
        {
            //ItemsChiQuyChiTiet = new List<NhNhuCauChiQuyChiTietModel>();
            if (ItemsChiQuyChiTiet == null || ItemsChiQuyChiTiet.Count == 0 || SelectedChiQuyChiTiet == null)
            {
                NhNhuCauChiQuyChiTietModel nhuCauChiQuyChiTietModel = new NhNhuCauChiQuyChiTietModel();
                nhuCauChiQuyChiTietModel.HopDong = true;
                nhuCauChiQuyChiTietModel.NoiDung = false;
                nhuCauChiQuyChiTietModel.IIdNhuCauChiQuyId = Model.Id;
                nhuCauChiQuyChiTietModel.PropertyChanged += Detail_PropertyChanged;
                itemsChiQuyChiTiet.Add(nhuCauChiQuyChiTietModel);
            }
            else
            {
                if (!SelectedChiQuyChiTiet.HopDong)
                {
                    NhNhuCauChiQuyChiTietModel nhuCauChiQuyChiTietModel = new NhNhuCauChiQuyChiTietModel();
                    nhuCauChiQuyChiTietModel.HopDong = true;
                    nhuCauChiQuyChiTietModel.NoiDung = false;
                    nhuCauChiQuyChiTietModel.IIdNhuCauChiQuyId = Model.Id;
                    nhuCauChiQuyChiTietModel.PropertyChanged += Detail_PropertyChanged;
                    currentRow = ItemsChiQuyChiTiet.IndexOf(SelectedChiQuyChiTiet);
                    itemsChiQuyChiTiet.Insert(currentRow + 1, nhuCauChiQuyChiTietModel);
                }
                else
                {
                    NhNhuCauChiQuyChiTietModel item = SelectedChiQuyChiTiet;
                    NhNhuCauChiQuyChiTietModel target = ObjectCopier.Clone(item);

                    currentRow = ItemsChiQuyChiTiet.IndexOf(SelectedChiQuyChiTiet);
                    target.Id = Guid.NewGuid();
                    target.IsModified = true;
                    target.PropertyChanged += Detail_PropertyChanged;
                    itemsChiQuyChiTiet.Insert(currentRow + 1, target);
                }
            }
            OnPropertyChanged(nameof(ItemsChiQuyChiTiet));
        }

        private void OnAddNoiDung()
        {
            //if (ItemsChiQuyChiTiet == null || ItemsChiQuyChiTiet.Count == 0 || SelectedChiQuyChiTiet == null)
            //{

            //}
            //else
            //{
            //    NhNhuCauChiQuyChiTietModel item = SelectedChiQuyChiTiet;
            //    NhNhuCauChiQuyChiTietModel target = ObjectCopier.Clone(item);
            //    currentRow = ItemsChiQuyChiTiet.IndexOf(SelectedChiQuyChiTiet);
            //    target.Id = Guid.NewGuid();
            //    target.IsModified = true;
            //    target.PropertyChanged += Detail_PropertyChanged;
            //    LoadLoaiNoiDungChi("0", nhuCauChiQuyChiTietModel);
            //    LoadDuAn((Guid)Model.IIdDonViId, Guid.Empty, false, nhuCauChiQuyChiTietModel);
            //    itemsChiQuyChiTiet.Insert(currentRow + 1, target);
            //}

            NhNhuCauChiQuyChiTietModel nhuCauChiQuyChiTietModel = new NhNhuCauChiQuyChiTietModel();
            nhuCauChiQuyChiTietModel.HopDong = false;
            nhuCauChiQuyChiTietModel.NoiDung = true;
            nhuCauChiQuyChiTietModel.IIdNhuCauChiQuyId = Model != null ? Model.Id : Guid.Empty;
            LoadTenNHiemVuChi(Guid.Empty, nhuCauChiQuyChiTietModel);
            LoadLoaiNoiDungChi("0", nhuCauChiQuyChiTietModel);
            //LoadDuAn((Guid)Model.IIdDonViId, Guid.Empty, false, nhuCauChiQuyChiTietModel);
            nhuCauChiQuyChiTietModel.PropertyChanged += Detail_PropertyChanged;
            itemsChiQuyChiTiet.Add(nhuCauChiQuyChiTietModel);

            OnPropertyChanged(nameof(ItemsChiQuyChiTiet));
        }

        private void OnDeleteItems()
        {
            if (ItemsChiQuyChiTiet != null && ItemsChiQuyChiTiet.Count() > 0 && SelectedChiQuyChiTiet != null)
            {
                SelectedChiQuyChiTiet.IsDeleted = !SelectedChiQuyChiTiet.IsDeleted;
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            NhNhuCauChiQuyChiTietModel nhNhuCauChiQuyChiTietModel = (NhNhuCauChiQuyChiTietModel)sender;
            var listHopDong = _nhDaHopDongService.FindAll();
            var a = iTiGia != null ? iTiGia.Value : Guid.Empty;
            NhDmTiGia tiGia = _nhDmTiGiaService.FindAll().Where(x => x.Id == a ).FirstOrDefault();
            NhDmTiGiaChiTiet tiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(tiGia != null ? tiGia.Id : Guid.Empty).Where(x => x.SMaTienTeQuyDoi == "USD" || x.SMaTienTeQuyDoi == "VND").FirstOrDefault();
            if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.IIdHopDongId))
            {
                if (nhNhuCauChiQuyChiTietModel.HopDong)
                {
                    nhNhuCauChiQuyChiTietModel.IIdHopDongId = Guid.Parse(ItemsNoiDungChi.FirstOrDefault(x => nhNhuCauChiQuyChiTietModel.IIdHopDongId.ToString().Equals(x.HiddenValue)).HiddenValue);
                    var noiDungChi = ItemsNoiDungChi.FirstOrDefault(x => nhNhuCauChiQuyChiTietModel.IIdHopDongId.ToString().Equals(x.HiddenValue));
                    var hopDong = listHopDong.FirstOrDefault(x => x.Id == nhNhuCauChiQuyChiTietModel.IIdHopDongId);
                    NhNhuCauChiQuyChiTietModel FLoatData = _mapper.Map<NhNhuCauChiQuyChiTietModel>(_nhNhuCauChiQuyChiTietService.FindByIdHopDong(nhNhuCauChiQuyChiTietModel.IIdHopDongId));
                    if (nhNhuCauChiQuyChiTietModel.IIdHopDongId != null)
                    {
                        //if (noiDungChi.ValueItem == "1" || noiDungChi.ValueItem == "2" || noiDungChi.ValueItem == "3")
                        //{

                        //}
                        //else if (noiDungChi.ValueItem == "3" || noiDungChi.ValueItem == "4")
                        //{
                        //    LoadLoaiNoiDungChi(noiDungChi.ValueItem, nhNhuCauChiQuyChiTietModel);
                        //    nhNhuCauChiQuyChiTietModel.FNoiDungChiUSD = hopDong.FGiaTriHopDongUSD;
                        //    nhNhuCauChiQuyChiTietModel.FNoiDungChiVND = hopDong.FGiaTriHopDongVND;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD = FLoatData.FKinhPhiDuocCapCacNamTruocSetUSD;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND = FLoatData.FKinhPhiDuocCapCacNamTruocSetVND;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetEUR = FLoatData.FKinhPhiDuocCapCacNamTruocSetEUR;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD = FLoatData.FKinhPhiDaChiCacNamTruocSetUSD;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND = FLoatData.FKinhPhiDaChiCacNamTruocSetVND;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetEUR = FLoatData.FKinhPhiDaChiCacNamTruocSetEUR;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetVND;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetUSD;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetVND;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetEUR;
                        //    nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac;

                        LoadLoaiNoiDungChi(noiDungChi.ValueItem, nhNhuCauChiQuyChiTietModel);
                        nhNhuCauChiQuyChiTietModel.FNoiDungChiUSD = hopDong.FGiaTriUsd;
                        nhNhuCauChiQuyChiTietModel.FNoiDungChiVND = hopDong.FGiaTriVnd;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD = FLoatData.FKinhPhiDuocCapCacNamTruocSetUSD;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND = FLoatData.FKinhPhiDuocCapCacNamTruocSetVND;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetEUR = FLoatData.FKinhPhiDuocCapCacNamTruocSetEUR;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapCacNamTruocSetNgoaiTeKhac;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD = FLoatData.FKinhPhiDaChiCacNamTruocSetUSD;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND = FLoatData.FKinhPhiDaChiCacNamTruocSetVND;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetEUR = FLoatData.FKinhPhiDaChiCacNamTruocSetEUR;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiCacNamTruocSetNgoaiTeKhac;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetVND;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetEUR;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDuocCapDenCuoiQuyTruocSetNgoaiTeKhac;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetUSD;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetVND;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetEUR = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetEUR;
                        nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac = FLoatData.FKinhPhiDaChiDenCuoiQuyTruocSetNgoaiTeKhac;
                    
                        LoadTenNHiemVuChi(string.IsNullOrEmpty(noiDungChi.DisplayItemOption2) ? Guid.Empty : Guid.Parse(noiDungChi.DisplayItemOption2), nhNhuCauChiQuyChiTietModel);
                        nhNhuCauChiQuyChiTietModel.SelectedTenNhiemVuChi = nhNhuCauChiQuyChiTietModel.ItemsTenNhiemVuChi.FirstOrDefault();
                    }
                    //if (hopDong.IIdDuAnId != null)
                    //{
                    //    LoadDuAn((Guid)Model.IIdDonViId, (Guid)hopDong.IIdDuAnId, true, nhNhuCauChiQuyChiTietModel);
                    //    nhNhuCauChiQuyChiTietModel.SelectedTenDuAn = nhNhuCauChiQuyChiTietModel.ItemsTenDuAn.FirstOrDefault();
                    //}
                    //nhNhuCauChiQuyChiTietModel.HopDongDuToanPheDuyeUsd = hopDong.FGiaTriUsd;
                    //nhNhuCauChiQuyChiTietModel.HopDongDuToanPheDuyeVnd = hopDong.FGiaTriVnd;
                    //var kinhPhiNamTruoc = _nhNhuCauChiQuyChiTietService.KinhPhiDaChi(hopDong.Id, (int)(Model.INamKeHoach ?? 0) - 1).ToList();
                    //nhNhuCauChiQuyChiTietModel.KinhPhiNamTruocDaChiUsd = kinhPhiNamTruoc.Sum(x => x.Usd);
                    //nhNhuCauChiQuyChiTietModel.KinhPhiNamTruocDaChiVnd = kinhPhiNamTruoc.Sum(x => x.Vnd);
                    //nhNhuCauChiQuyChiTietModel.KinhPhiNamTruocDaChiEur = kinhPhiNamTruoc.Sum(x => x.Eur);
                    //nhNhuCauChiQuyChiTietModel.KinhPhiNamTruocDaChiNgoaiTeKhac = kinhPhiNamTruoc.Sum(x => x.NgoaiTe);
                }
            }
            //if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.SNoiDung))
            //{
            //    LoadLoaiNoiDungChi("0", nhNhuCauChiQuyChiTietModel);
            //    LoadDuAn((Guid)Model.IIdDonViId, Guid.Empty, false, nhNhuCauChiQuyChiTietModel);
            //    if (args.PropertyName == nameof(NhNhuCauChiQuyChiTiet.IIdDuAnId))
            //    {
            //        LoadTenNHiemVuChi(Guid.Empty, nhNhuCauChiQuyChiTietModel);
            //    }
            //}
            if (args.PropertyName == nameof(NhNhuCauChiQuyChiTiet.FNhuCauQuyNayUsd) ||
                args.PropertyName == nameof(NhNhuCauChiQuyChiTiet.FNhuCauQuyNayVnd) ||
                args.PropertyName == nameof(NhNhuCauChiQuyChiTiet.FNhuCauQuyNayEur))
            {
                CurrencyExchangeAction?.Invoke(nhNhuCauChiQuyChiTietModel, args.PropertyName);
            }
            //if (tiGiaChiTiet != null && nhNhuCauChiQuyChiTietModel.IIdHopDongId == null)
            //{
            //    if (tiGiaChiTiet.FTiGia > 0)
            //    {
            //        //1
            //        if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD / tiGiaChiTiet.FTiGia;
            //        }else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetUSD = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapCacNamTruocSetVND / tiGiaChiTiet.FTiGia;
            //        }
            //        //2
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD / tiGiaChiTiet.FTiGia;
            //        }
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetUSD = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiCacNamTruocSetVND / tiGiaChiTiet.FTiGia;
            //        }
            //        //2
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD / tiGiaChiTiet.FTiGia;
            //        }
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetUSD = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDuocCapDenCuoiQuyTruocSetVND / tiGiaChiTiet.FTiGia;
            //        }
            //        //3
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD / tiGiaChiTiet.FTiGia;
            //        }
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetUSD = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FKinhPhiDaChiDenCuoiQuyTruocSetVND / tiGiaChiTiet.FTiGia;
            //        }
            //        //4
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FNoiDungChiUSD))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FNoiDungChiVND = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FNoiDungChiUSD * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FNoiDungChiUSD / tiGiaChiTiet.FTiGia;
            //        }
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FNoiDungChiVND))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FNoiDungChiUSD = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FNoiDungChiVND * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FNoiDungChiVND / tiGiaChiTiet.FTiGia;
            //        }
            //        //5
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FNhuCauQuyNayUsd))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayVnd = tiGiaChiTiet.SMaTienTeQuyDoi == "VND" ? nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayUsd * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayUsd / tiGiaChiTiet.FTiGia;
            //        }
            //        else if (args.PropertyName == nameof(NhNhuCauChiQuyChiTietModel.FNhuCauQuyNayVnd))
            //        {
            //            nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayUsd = tiGiaChiTiet.SMaTienTeQuyDoi == "USD" ? nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayVnd * tiGiaChiTiet.FTiGia : nhNhuCauChiQuyChiTietModel.FNhuCauQuyNayVnd / tiGiaChiTiet.FTiGia;
            //        }
            //    }
            //}
            
            nhNhuCauChiQuyChiTietModel.IsModified = true;
        }

        private bool ValidateData()
        {
            var ModelNCCQChiTiet = Model.NhNhuCauChiQuyChiTiets;

            List<string> lstError = new List<string>();
            var i = 0;
            foreach (var item in ModelNCCQChiTiet)
            {
                i++;
                if (!item.IsDeleted && string.IsNullOrEmpty(item.SNoiDung))
                {
                    lstError.Add(string.Format("Dòng " + i +": Chưa nhập nội dung chi"));
                }
                if (!item.IsDeleted && (item.FNhuCauQuyNayUsd == null || item.FNhuCauQuyNayUsd == null))
                {
                    lstError.Add(string.Format("Dòng " + i + ": Chưa nhập nhu cầu sử dụng kinh phí"));
                }
            }

            if (lstError.Count() > 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        public override void OnSave(object obj)
        {
            Model.NhNhuCauChiQuyChiTiets = ItemsChiQuyChiTiet;
            if (!ValidateViewModelHelper.Validate(Model)) return;
            if (!ValidateData()) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                if (Model.Id.IsNullOrEmpty())
                {
                    var entity = _mapper.Map<NhNhuCauChiQuy>(Model);
                    entity.Id = Guid.NewGuid();
                    foreach (var item in entity.NhNhuCauChiQuyChiTiets)
                    {
                        item.IIdNhuCauChiQuyId = entity.Id;
                    }
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    _nhNhuCauChiQuyService.Add(entity);
                }
                else
                {
                    var entity = _nhNhuCauChiQuyService.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    _nhNhuCauChiQuyService.Update(entity);
                }
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    DialogResult dialog = System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
                    if(dialog == DialogResult.OK)
                    {
                        Model = _mapper.Map<NhNhuCauChiQuyModel>(e.Result);
                        SavedAction?.Invoke(Model);

                        if (obj is Window window)
                        {
                            Dispose();
                            window.Close();
                        }
                    }
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    _logger.LogError(e.Error.Message);
                }
            });
        }

        public override void Dispose()
        {
            // clear item
            if (!itemsChiQuyChiTiet.IsEmpty()) itemsChiQuyChiTiet.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                if ((SelectedChiQuyChiTiet != null ? SelectedChiQuyChiTiet.IIdHopDongId : Guid.Empty) == null)
                {
                    var iIDDon_Vi = Model.IIdDonViId;
                    var iIdKhttNhiemVuChiId = SelectedChiQuyChiTiet != null ? SelectedChiQuyChiTiet.IIdKhttNhiemVuChiId : null;
                    DataGrid dataGrid = obj as DataGrid;
                    GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService> viewModelBase = new GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService>((NhDaDuAnService)_nhDaDuAnService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh sách dự án",
                        Title = "Danh sách dự án",
                        Description = "Danh sách dự án",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Building,
                        IsDialog = true,
                        iID_DonViID = iIDDon_Vi,
                        iIdKhttNhiemVuChiId = iIdKhttNhiemVuChiId
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh sách dự án",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            NhDaDuAnModel item2 = (NhDaDuAnModel)obj;

                            if (item2 != null && SelectedChiQuyChiTiet != null)
                            {
                                var temp = ItemsChiQuyChiTiet;
                                foreach (var item in temp)
                                {
                                    if (item == SelectedChiQuyChiTiet)
                                    {
                                        item.STenDuAn = item2.STenDuAn;
                                        item.IIdDuAnId = item2.Id;
                                    }
                                }
                                Items = _mapper.Map<ObservableCollection<NhNhuCauChiQuyChiTietModel>>(temp);
                            }
                            GenericControlCustomWindow.Close();
                            OnPropertyChanged(nameof(Items));
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message, ex);
                        }
                    };
                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
                else
                {
                    MessageBoxHelper.Warning("Nội dung chi đã tồn tại dự án không được chọn lại !");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        private List<NhNhuCauChiQuyBaoCaoModel> LoadDataTongHop()
        {
            List<NHTHTongHop> data = new List<NHTHTongHop>();
            var lstSMaNguon = new List<string> { NHConstants.MA_TH_BC_NCCQ };
            var lstSMaNguonQT = new List<string> { NHConstants.MA_TH_BCTH_NS_QUY };
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            //predicate = predicate.And(x => x.INamKeHoach == Model.INamKeHoach);
            predicate = predicate.And(x => (lstSMaNguon.Contains(x.SMaNguon) || lstSMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach - 1);
            predicate = predicate.Or(x => (lstSMaNguonQT.Contains(x.SMaNguon) || lstSMaNguonQT.Contains(x.SMaNguonCha)) && x.INamKeHoach == Model.INamKeHoach && x.IQuyKeHoach == Model.IQuy);

            return CalculateDataTongHop(_nhThTongHopService.FindByCondition(predicate));
        }

        private List<NhNhuCauChiQuyBaoCaoModel> CalculateDataTongHop(IEnumerable<NHTHTongHop> lstData)
        {
            List<NhNhuCauChiQuyBaoCaoModel> results = new List<NhNhuCauChiQuyBaoCaoModel>();
            if (lstData.Any())
            {
                var listHopDong = lstData.Where(x => x.IIdHopDongId != null).GroupBy(g => g.IIdHopDongId).Select(x => x.First());
                var listDuAn = lstData.Where(x => x.IIdDuAnId != null &&
                                !listHopDong.Where(w => w.IIdDuAnId != null || w.IIdDuAnId != Guid.Empty).Select(s => s.IIdDuAnId).Contains(x.IIdDuAnId))
                                .GroupBy(g => g.IIdDuAnId).Select(s => s.First());

                foreach (var item in listHopDong)
                {
                    var dataHandlerNccq = new NhNhuCauChiQuyBaoCaoModel();
                    var dataHopDong = lstData.Where(x => x.IIdHopDongId.Equals(item.IIdHopDongId));
                    //KinhPhiVND8- nguon 306(n-1)
                    var dataCol8 = dataHopDong.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306);
                    var dataUsdCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd);
                    var dataVndCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd);

                    //KinhPhiDaChiUSD10- nguon 308(n-1)
                    var dataCol10 = dataHopDong.Where(x => x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308);
                    var dataUsdCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd);
                    var dataVndCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd);

                    //KinhPhiToYUSD12 <-> nguon(101,102,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    var lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPDC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus12 = dataHopDong.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    //data minus
                    var lstSmaNguonMinus = NHConstants.MA_TH_BC_NCCQ.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataMinus = dataHopDong.Where(x => (lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha)));
                    var dataMinusUsd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinusVnd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                    var dataUsdCol12 = dataPlusUsd12 - dataMinusUsd;
                    var dataVndCol12 = dataPlusVnd12 - dataMinusVnd;
                    //KinhPhiDaChiToYUSD14 <-> nguon(141,142,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus14 = dataHopDong.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataUsdCol14 = dataPlusUsd14 - dataMinusUsd;
                    var dataVndCol14 = dataPlusVnd14 - dataMinusVnd;

                    dataHandlerNccq.KinhPhiUSD = dataUsdCol8;
                    dataHandlerNccq.KinhPhiVND = dataVndCol8;
                    dataHandlerNccq.KinhPhiDaChiUSD = dataUsdCol10;
                    dataHandlerNccq.KinhPhiDaChiVND = dataVndCol10;
                    dataHandlerNccq.KinhPhiToYUSD = dataUsdCol12;
                    dataHandlerNccq.KinhPhiToYVND = dataVndCol12;
                    dataHandlerNccq.KinhPhiDaChiToYUSD = dataUsdCol14;
                    dataHandlerNccq.KinhPhiDaChiToYVND = dataVndCol14;

                    dataHandlerNccq.Id = item.Id;
                    dataHandlerNccq.iID_HopDongID = item.IIdHopDongId;
                    dataHandlerNccq.iID_DuAnID = item.IIdDuAnId;
                    dataHandlerNccq.ID_NhiemVuChi = item.IIDKHTTNhiemVuChiID ?? Guid.Empty;
                    dataHandlerNccq.iID_KHCTBQP_NhiemVuChiID = item.IIDKHTTNhiemVuChiID;
                    dataHandlerNccq.iID_NhuCauChiQuyID = item.IIdChungTu;
                    dataHandlerNccq.iID_DonViID = item.IIdDonVi ?? Guid.Empty;
                    //dataHandlerQtnd.ILoaiNoiDungChi = item.lo;


                    //dataHandlerQtnd.
                    results.Add(dataHandlerNccq);
                }

                foreach (var item in listDuAn)
                {
                    var dataHandlerNccq = new NhNhuCauChiQuyBaoCaoModel();
                    var dataDuAn = lstData.Where(x => x.IIdDuAnId.Equals(item.IIdDuAnId));
                    //KinhPhiVND8- nguon 306(n-1)
                    var dataCol8 = dataDuAn.Where(x => x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306);
                    var dataUsdCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriUsd);
                    var dataVndCol8 = dataCol8.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd) - dataCol8.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(s => s.FGiaTriVnd);

                    //KinhPhiDaChiUSD10- nguon 308(n-1)
                    var dataCol10 = dataDuAn.Where(x => x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308);
                    var dataUsdCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriUsd);
                    var dataVndCol10 = dataCol10.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd) - dataCol10.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(s => s.FGiaTriVnd);

                    //KinhPhiToYUSD12 <-> nguon(101,102,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    var lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPDC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus12 = dataDuAn.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd12 = dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus12.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    //data minus
                    var lstSmaNguonMinus = NHConstants.MA_TH_BC_NCCQ_MINUS.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataMinus = dataDuAn.Where(x => (lstSmaNguonMinus.Contains(x.SMaNguon) || lstSmaNguonMinus.Contains(x.SMaNguonCha)));
                    var dataMinusUsd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinusVnd = dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstSmaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);

                    var dataUsdCol12 = dataPlusUsd12 - dataMinusUsd;
                    var dataVndCol12 = dataPlusVnd12 - dataMinusVnd;
                    //KinhPhiDaChiToYUSD14 <-> nguon(141,142,111,112,121,122) - (131,132)(quy, nam ke hoach)
                    lstSmaNguonPlus = NHConstants.MA_TH_BC_NCCQ_KPC.Split(StringUtils.COMMA).Select(x => x.Trim());
                    var dataPlus14 = dataDuAn.Where(x => (lstSmaNguonPlus.Contains(x.SMaNguon) || lstSmaNguonPlus.Contains(x.SMaNguonCha)));
                    var dataPlusUsd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlusVnd14 = dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus14.Where(x => lstSmaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataUsdCol14 = dataPlusUsd14 - dataMinusUsd;
                    var dataVndCol14 = dataPlusVnd14 - dataMinusVnd;

                    dataHandlerNccq.KinhPhiUSD = dataUsdCol8;
                    dataHandlerNccq.KinhPhiVND = dataVndCol8;
                    dataHandlerNccq.KinhPhiDaChiUSD = dataUsdCol10;
                    dataHandlerNccq.KinhPhiDaChiVND = dataVndCol10;
                    dataHandlerNccq.KinhPhiToYUSD = dataUsdCol12;
                    dataHandlerNccq.KinhPhiToYVND = dataVndCol12;
                    dataHandlerNccq.KinhPhiDaChiToYUSD = dataUsdCol14;
                    dataHandlerNccq.KinhPhiDaChiToYVND = dataVndCol14;

                    dataHandlerNccq.Id = item.Id;
                    dataHandlerNccq.iID_HopDongID = item.IIdHopDongId;
                    dataHandlerNccq.iID_DuAnID = item.IIdDuAnId;
                    dataHandlerNccq.ID_NhiemVuChi = item.IIDKHTTNhiemVuChiID ?? Guid.Empty;
                    dataHandlerNccq.iID_KHCTBQP_NhiemVuChiID = item.IIDKHTTNhiemVuChiID;
                    dataHandlerNccq.iID_NhuCauChiQuyID = item.IIdChungTu;
                    dataHandlerNccq.iID_DonViID = item.IIdDonVi ?? Guid.Empty;

                    //dataHandlerQtnd.
                    results.Add(dataHandlerNccq);

                }
            }
            return results;
        }
    }
}
