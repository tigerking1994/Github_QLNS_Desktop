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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using static VTS.QLNS.CTC.App.ViewModel.Budget.Allocation.AllocationDetailViewModel;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc
{
    public class MSTNNHHopDongTrongNuocChiTietHangMucViewModel : DetailViewModelBase<NhDaHopDongGoiThauNhaThauModel, NhDaHopDongGoiThauNhaThauModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<MSTNNHHopDongTrongNuocChiTietHangMucViewModel> _logger;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;

        public override string Name => "THÔNG TIN TRÚNG THẦU CHI TIẾT";
        public override string Title => "Thông tin trúng thầu chi tiết";

        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNNHHopDongTrongNuoc.NHHopDongGoiThauDetail);

        public event DataChangedEventHandler ClosePopup;
        public RelayCommand SaveDataCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand OpenHangMucCommand { get; }
        public RelayCommand OnAddChiPhiHangMucCommand { get; }

        public bool IsEdit { get; set; }
        public Action<object, string> CurrencyExchangeActionChiPhi { get; set; }
        public Action<object, string> CurrencyExchangeActionHangMuc { get; set; }

        private ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel> _itemsChiPhi;
        public ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }
      
        private NhDaHopDongTrongNuocChiPhiGoiThauModel _selectedChiPhi;
        public NhDaHopDongTrongNuocChiPhiGoiThauModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel> _itemsHangMuc;
        public ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel> ItemsHangMuc
        {
            get => _itemsHangMuc;
            set => SetProperty(ref _itemsHangMuc, value);
        }

        private NhDaHopDongTrongNuocHangMucGoiThauModel _selectedHangMuc;
        public NhDaHopDongTrongNuocHangMucGoiThauModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set => SetProperty(ref _selectedHangMuc, value);
        }

        private List<NhDaHopDongTrongNuocChiPhiGoiThauModel> _listChiPhi;
        public List<NhDaHopDongTrongNuocChiPhiGoiThauModel> ListChiPhi
        {
            get => _listChiPhi;
            set => SetProperty(ref _listChiPhi, value);
        }

        private List<NhDaHopDongTrongNuocHangMucGoiThauModel> _listHangMuc;
        public List<NhDaHopDongTrongNuocHangMucGoiThauModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        private bool _selectAllHangMucFilter;
        public bool SelectAllHangMucFilter
        {
            get => _selectAllHangMucFilter;
            set
            {
                SetProperty(ref _selectAllHangMucFilter, value);
                if (_itemsHangMuc != null)
                {
                    _itemsHangMuc.Select(c => { c.IsChecked = SelectAllHangMucFilter; return c; }).ToList();
                }
            }
        }

        public List<NhDaHopDongTrongNuocChiPhiGoiThauModel> ListChiPhiLoad { get; set; }
        private List<NhDaHopDongTrongNuocHangMucGoiThauModel> ListHangMucAdd;
        public MSTNNHHopDongTrongNuocChiTietHangMucViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILogger<MSTNNHHopDongTrongNuocChiTietHangMucViewModel> logger,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
            //OpenHangMucCommand = new RelayCommand(obj => LoadHangMuc());
            //OnAddChiPhiHangMucCommand = new RelayCommand(obj => OnAddChiPhiHangMuc(obj));
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
            //LoadChiPhi();
            LoadHangMucNew();
        }
        private void LoadHangMucNew()
        {
            if (Model.IIdGoiThauId != null)
            {
                var dataGoiThau = _nhDaGoiThauHangMucSerrvice.FindGoiThauChiPhiHangMucByGoiThauId(Model.IIdGoiThauId.Value);
                dataGoiThau = dataGoiThau.Where(x => x.IIdHopDongId.IsNullOrEmpty() || x.IIdHopDongId.Equals(Model.IIdHopDongId) && x.IIdHopDongGoiThauNhaThauId.Equals(Model.Id)).OrderBy(o => o.SMaOrder);
                _itemsHangMuc = _mapper.Map<ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel>>(dataGoiThau);
                foreach (var item in _itemsHangMuc)
                {
                    if (item.IIdHopDongId.Equals(Model.IIdHopDongId))
                    {
                        item.IsChecked = true;
                    }
                    if (item.IIdParentId == null)
                    {
                        item.IsHangCha = true;
                    }
                    item.IIdHopDongGoiThauNhaThauId = Model.Id;
                }
            }
        }

        private void OnSaveData(object obj)
        {
            SavedAction?.Invoke(null);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }
        //private void LoadChiPhi()
        //{
        //    ListHangMucAdd = new List<NhDaHopDongTrongNuocHangMucGoiThauModel>();
        //    ItemsChiPhi = new ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel>();
        //    ItemsHangMuc = new ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel>();
        //    if (!IsEdit)
        //    {
        //        var data = _nhDaGoiThauChiPhiService.FindByGoiThauId((Guid)Model.IIdGoiThauId);
        //        _itemsChiPhi = _mapper.Map<ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel>>(data);
        //    }
        //    else
        //    {
        //        var data = _nhDaHopDongChiPhiService.FindByHopDongGoiThauNhaThauID((Guid)Model.Id);

        //        if(Model.IIdGoiThauId != null)
        //        {
        //            var data2 = _nhDaGoiThauChiPhiService.FindByGoiThauId((Guid)Model.IIdGoiThauId);
        //            // _itemsChiPhi = _mapper.Map<ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel>>(data2);

        //            foreach (var item in data)
        //            {
        //                foreach (var item2 in data2)
        //                {
        //                    if (item.IIdGoiThauChiPhiId == item2.Id)
        //                    {
        //                        item.FGiaTriEur = item2.FTienGoiThauEur;
        //                        item.FGiaTriUsd = item2.FTienGoiThauUsd;
        //                        item.FGiaTriVnd = item2.FTienGoiThauVnd;
        //                        item.FGiaTriNgoaiTeKhac = item2.FTienGoiThauNgoaiTeKhac;
        //                    }

        //                }
        //            }
        //        }

        //        _itemsChiPhi = _mapper.Map<ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel>>(data);
        //        foreach(var item in _itemsChiPhi)
        //        {
        //            item.FTienGoiThauEUR = item.FGiaTriEur;
        //            item.FTienGoiThauUSD= item.FGiaTriUsd;
        //            item.FTienGoiThauVND = item.FGiaTriVnd;
        //            item.FTienGoiThauNgoaiTeKhac = item.FGiaTriNgoaiTeKhac;
        //        }    
        //    }
        //    foreach (var item in _itemsChiPhi)
        //    {
        //        item.Id = Guid.NewGuid();
        //        foreach (var itemChiPhi in ListChiPhiLoad)
        //        {
        //            if (itemChiPhi.IIdHopDongGoiThauNhaThauId != Model.Id && item.IIdGoiThauChiPhiId == itemChiPhi.IIdGoiThauChiPhiId)
        //            {
        //                item.FTienGoiThauUSD = item.FTienGoiThauUSD - itemChiPhi.FTienHopDongUSD;
        //                item.FTienGoiThauVND = item.FTienGoiThauVND - itemChiPhi.FTienHopDongVND;
        //                item.FTienGoiThauEUR = item.FTienGoiThauEUR - itemChiPhi.FTienHopDongEUR;
        //                item.FTienGoiThauNgoaiTeKhac = item.FTienGoiThauNgoaiTeKhac - itemChiPhi.FTienHopDongNgoaiTeKhac;
        //            }
        //            if (itemChiPhi.IIdHopDongGoiThauNhaThauId == Model.Id && item.IIdGoiThauChiPhiId == itemChiPhi.IIdGoiThauChiPhiId)
        //            {
        //                item.FTienHopDongUSD = itemChiPhi.FTienHopDongUSD;
        //                item.FTienHopDongVND = itemChiPhi.FTienHopDongVND;
        //                item.FTienHopDongEUR = itemChiPhi.FTienHopDongEUR;
        //                item.FTienHopDongNgoaiTeKhac = itemChiPhi.FTienHopDongNgoaiTeKhac;
        //            }
        //        }          
        //        var lstHangMuc = _nhDaGoiThauHangMucSerrvice.FindByChiPhiId((Guid)item.IIdGoiThauChiPhiId);
        //        if (lstHangMuc != null && lstHangMuc.Count() > 0) item.EditChiPhi = true;
        //        item.PropertyChanged += ChiPhi_PropertyChanged;
        //    }
        //    var listTmp = _itemsChiPhi.ToList();
        //    listTmp.RemoveAll(x => x.FTienGoiThauUSD == 0 || x.FTienHopDongEUR == 0 || x.FTienHopDongVND == 0 || x.FTienHopDongNgoaiTeKhac == 0);
        //    _itemsChiPhi = new ObservableCollection<NhDaHopDongTrongNuocChiPhiGoiThauModel>(listTmp);
        //    OnPropertyChanged(nameof(ItemsChiPhi));
        //}

        //private void LoadHangMuc()
        //{
        //    var data = _nhDaGoiThauHangMucSerrvice.FindByChiPhiId((Guid)SelectedChiPhi.IIdGoiThauChiPhiId);
        //    _itemsHangMuc = _mapper.Map<ObservableCollection<NhDaHopDongTrongNuocHangMucGoiThauModel>>(data);
        //    foreach (var item in _itemsHangMuc)
        //    {
        //        item.Id = Guid.NewGuid();
        //        ListHangMucAdd.Add(item);
        //        item.PropertyChanged += HangMuc_PropertyChanged;
        //    }
        //    OnPropertyChanged(nameof(ItemsHangMuc));
        //}

        //private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NhDaHopDongTrongNuocHangMucGoiThauModel item = (NhDaHopDongTrongNuocHangMucGoiThauModel)sender;
        //    if (e.PropertyName == nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongUSD)
        //        || e.PropertyName == nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongVND)
        //        || e.PropertyName == nameof(NhDaHopDongTrongNuocHangMucGoiThauModel.FTienHopDongEUR))
        //    {
        //        CurrencyExchangeActionHangMuc?.Invoke(sender, e.PropertyName);
        //        SumTrungThauChiPhi();
        //        item.IsSelected = true;
        //        item.IIdHopDongChiPhiId = SelectedChiPhi.Id;
        //    }
        //    item.IsModified = true;
        //    ListHangMucAdd.RemoveAll(x => x.Id == item.Id);
        //    ListHangMucAdd.Add(item);
        //}

        //private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NhDaHopDongTrongNuocChiPhiGoiThauModel item = (NhDaHopDongTrongNuocChiPhiGoiThauModel)sender;
        //    if (e.PropertyName == nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongUSD)
        //        || e.PropertyName == nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongVND)
        //        || e.PropertyName == nameof(NhDaHopDongTrongNuocChiPhiGoiThauModel.FTienHopDongEUR))
        //    {
        //        CurrencyExchangeActionChiPhi?.Invoke(sender, e.PropertyName);
        //    }
        //    item.IsModified = true;
        //    item.IsSelected = true;
        //}

        //private void SumTrungThauChiPhi()
        //{
        //    SelectedChiPhi.FTienHopDongUSD = ItemsHangMuc.Where(x => x.FTienHopDongUSD != null).Sum(x => x.FTienHopDongUSD);
        //    SelectedChiPhi.FTienHopDongVND = ItemsHangMuc.Where(x => x.FTienHopDongVND != null).Sum(x => x.FTienHopDongVND);
        //    SelectedChiPhi.FTienHopDongEUR = ItemsHangMuc.Where(x => x.FTienHopDongEUR != null).Sum(x => x.FTienHopDongEUR);
        //    SelectedChiPhi.FTienHopDongNgoaiTeKhac = ItemsHangMuc.Where(x => x.FTienHopDongNgoaiTeKhac != null).Sum(x => x.FTienHopDongNgoaiTeKhac);
        //    if (SelectedChiPhi.FTienHopDongUSD != null || SelectedChiPhi.FTienHopDongVND != null || SelectedChiPhi.FTienHopDongEUR != null || SelectedChiPhi.FTienHopDongNgoaiTeKhac != null)
        //    {
        //        SelectedChiPhi.IsSelected = true;
        //    }
        //}

        //private void OnAddChiPhiHangMuc(object obj)
        //{
        //    ListChiPhi = new List<NhDaHopDongTrongNuocChiPhiGoiThauModel>();
        //    ListHangMuc = new List<NhDaHopDongTrongNuocHangMucGoiThauModel>();
        //    ListChiPhi = ItemsChiPhi.Where(x => x.IsSelected).Select(x =>
        //    {
        //        x.IIdHopDongGoiThauNhaThauId = Model.Id;
        //        x.IIdGoiThauId = Model.IIdGoiThauId;
        //        return x;
        //    }).ToList();
        //    ListHangMuc = ListHangMucAdd.Where(x => x.IsSelected).ToList();
        //    SavedAction?.Invoke(ListChiPhi);
        //    SavedAction?.Invoke(ListHangMuc);
        //    ((Window)obj).Close();
        //}

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}

