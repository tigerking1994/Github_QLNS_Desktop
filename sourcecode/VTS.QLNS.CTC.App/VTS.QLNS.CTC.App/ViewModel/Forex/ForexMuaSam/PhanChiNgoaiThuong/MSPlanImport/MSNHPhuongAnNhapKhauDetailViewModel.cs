using log4net;
using System;
using AutoMapper;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;
using VTS.QLNS.CTC.App.Model;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Core.Domain.Query;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Properties;
using ControlzEx.Standard;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSPlanImport
{
    public class MSNHPhuongAnNhapKhauDetailViewModel : DialogCurrencyAttachmentViewModelBase<NhDaGoiThauModel>
    {
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaChuTruongDauTuService _nhDaChuTruongDauTuService;
        private readonly INhDaChuTruongDauTuNguonVonService _nhDaChuTruongDauTuNguonVonService;
        private readonly INhDaChuTruongDauTuHangMucService _nhDaChuTruongDauTuHangMucService;
        private readonly INhDmChiPhiService _nhDmChiPhiService;

        private readonly INhDaQdDauTuService _qdDauTuService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDaQdDauTuHangMucService _nhDaQdDauTuHangMucService;
        private readonly INhDaQdDauTuChiPhiService _nhDaQdDauTuChiPhiService;

        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;

        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private IMapper _mapper;
        private ObservableCollection<NhDaGoiThauHangMucModel> _originItems;
        public bool HasChanged => !ObjectCopier.ToJsonString(ItemsHangMuc).Equals(ObjectCopier.ToJsonString(_originItems));
        private ObservableCollection<NhDaGoiThauHangMucModel> tempListHangMuc = new ObservableCollection<NhDaGoiThauHangMucModel>();

        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu chi tiết";
        public override Type ContentType => typeof(MSNHPhuongAnNhapKhauDetail);
        public bool IsDetail { get; set; }
        public bool IsAdded { get; set; }
        public bool IsEnableSoCuChuongTrinh { get; set; }
        public bool IsEnableSoCuQdDauTu { get; set; }
        public string SLoaiSoCu { get; set; }
        public string SoPANK { get; set; }

        private bool _isSelectedNguonVon;
        public bool IsSelectedNguonVon
        {
            get => _isSelectedNguonVon;
            set => SetProperty(ref _isSelectedNguonVon, value);
        }

        private bool _isSelectedChiPhi;
        public bool IsSelectedChiPhi
        {
            get => _isSelectedChiPhi;
            set => SetProperty(ref _isSelectedChiPhi, value);
        }

        private ObservableCollection<NguonNganSachModel> _itemsNguonVon;
        public ObservableCollection<NguonNganSachModel> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauNguonVonModel> _itemsGoiThauNguonVon;
        public ObservableCollection<NhDaGoiThauNguonVonModel> ItemsGoiThauNguonVon
        {
            get => _itemsGoiThauNguonVon;
            set => SetProperty(ref _itemsGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _selectedGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel SelectedGoiThauNguonVon
        {
            get => _selectedGoiThauNguonVon;
            set => SetProperty(ref _selectedGoiThauNguonVon, value);
        }

        private NhDaGoiThauNguonVonModel _tongTienGoiThauNguonVon;
        public NhDaGoiThauNguonVonModel TongTienGoiThauNguonVon
        {
            get => _tongTienGoiThauNguonVon;
            set => SetProperty(ref _tongTienGoiThauNguonVon, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> _itemsChiPhiTemp = new ObservableCollection<NhDaGoiThauChiPhiModel>();
        public ObservableCollection<NhDaGoiThauChiPhiModel> ItemsChiPhiTemp
        {
            get => _itemsChiPhiTemp;
            set => SetProperty(ref _itemsChiPhiTemp, value);
        }

        private NhDaGoiThauChiPhiModel _selectedChiPhi;
        public NhDaGoiThauChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<NhDaGoiThauHangMucModel> _itemsHangMuc = new ObservableCollection<NhDaGoiThauHangMucModel>();
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

        private NhDaGoiThauChiPhiModel _tongTienChiPhi;
        public NhDaGoiThauChiPhiModel TongTienChiPhi
        {
            get => _tongTienChiPhi;
            set => SetProperty(ref _tongTienChiPhi, value);
        }

        private bool _selectAllNguonVon;
        public bool SelectAllNguonVon
        {
            get => !ItemsGoiThauNguonVon.IsEmpty() && ItemsGoiThauNguonVon.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllNguonVon, value);
                ItemsGoiThauNguonVon?.Select(c => { c.IsChecked = _selectAllNguonVon; return c; }).ToList();
            }
        }

        private ObservableCollection<NhDmChiPhiModel> _itemsDMChiPhi;
        public ObservableCollection<NhDmChiPhiModel> ItemsDMChiPhi
        {
            get => _itemsDMChiPhi;
            set => SetProperty(ref _itemsDMChiPhi, value);
        }

        private String _txtDetail;
        public String txtDetail
        {
            get => _txtDetail;
            set => SetProperty(ref _txtDetail, value);
        }

        public bool IsInitPackage { get; set; }
        public HashSet<Guid> LstPackgeModified;

        // Nguồn vốn
        public RelayCommand AddDuAnGoiThauNguonVonCommand { get; }
        public RelayCommand DeleteDuAnGoiThauNguonVonCommand { get; }

        // Chi phí
        public RelayCommand AddChiPhiCommand { get; set; }
        public RelayCommand DeleteChiPhiCommand { get; set; }
        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        public RelayCommand AddGoiThauHangMucCommand { get; }
        public RelayCommand DeleteGoiThauHangMucCommand { get; }

        public MSNHPhuongAnNhapKhauItemDialogViewModel MSNHPhuongAnNhapKhauItemDialogViewModel { get; }
        public NhHdnkPhuongAnNhapKhauModel NhHdnkPhuongAnNhapKhauModel { get; set; }

        private Dictionary<string, NhDaDuAnHangMuc> _ttdaDuanHangMuc = null;
        private Dictionary<string, NhDaDetailHangMucQuery> _ttdaDaQdDauTuHangMuc = null;
        private Dictionary<string, NhDaChuTruongDauTuHangMuc> _ttdaChuTruongDauTuHangMuc = null;

        public MSNHPhuongAnNhapKhauDetailViewModel
        (
            INhDaGoiThauService nhDaGoiThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            IMapper mapper,
            INhDaQdDauTuService qdDauTuService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuHangMucService nhDaQdDauTuHangMucService,
            INhDaQdDauTuChiPhiService nhDaQdDauTuChiPhiService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDaChuTruongDauTuService nhDaChuTruongDauTuService,
            INhDaChuTruongDauTuNguonVonService nhDaChuTruongDauTuNguonVonService,
            INhDaChuTruongDauTuHangMucService nhDaChuTruongDauTuHangMucService,
            INhDmChiPhiService nhDmChiPhiService,
            MSNHPhuongAnNhapKhauItemDialogViewModel msnhPhuongAnNhapKhauItemDialogViewModel
        ) : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _mapper = mapper;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _qdDauTuService = qdDauTuService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuHangMucService = nhDaQdDauTuHangMucService;
            _nhDaQdDauTuChiPhiService = nhDaQdDauTuChiPhiService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDaChuTruongDauTuService = nhDaChuTruongDauTuService;
            _nhDaChuTruongDauTuNguonVonService = nhDaChuTruongDauTuNguonVonService;
            _nhDaChuTruongDauTuHangMucService = nhDaChuTruongDauTuHangMucService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _nhDmChiPhiService = nhDmChiPhiService;

            MSNHPhuongAnNhapKhauItemDialogViewModel = msnhPhuongAnNhapKhauItemDialogViewModel;
            MSNHPhuongAnNhapKhauItemDialogViewModel.ParentPage = this;

            AddDuAnGoiThauNguonVonCommand = new RelayCommand(obj => OnAddDuAnGoiThauNguonVon());
            DeleteDuAnGoiThauNguonVonCommand = new RelayCommand(obj => OnDeleteDuAnGoiThauNguonVon(), s => SelectedGoiThauNguonVon != null);
            AddChiPhiCommand = new RelayCommand(obj => OnAddGoiThauChiPhi(obj), obj => (bool)obj || SelectedChiPhi != null);
            DeleteChiPhiCommand = new RelayCommand(obj => OnDeleteGoiThauChiPhi(), obj => SelectedChiPhi != null);
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail(obj));

            AddGoiThauHangMucCommand = new RelayCommand(obj => OnAddGoiThauHangMuc(obj), obj => (bool)obj || (SelectedHangMuc != null));
            DeleteGoiThauHangMucCommand = new RelayCommand(obj => OnDeleteGoiThauHangMuc(), obj => SelectedHangMuc != null);
        }

        public override void Init()
        {
            LoadDefault();
            LoadDanhMucChiPhi();
            LoadNguonVon();
            LoadDuAnGoiThauNguonVon();
            LoadSttChiPhi();
            Console.WriteLine(IsEnableSoCuChuongTrinh);
        }

        private void LoadSttChiPhi()
        {
            if (ItemsChiPhi.IsEmpty()) return;
            var dataItemsChiphi = ItemsChiPhi.OrderBy(x => x.IIdGoiThauNguonVonId).ToList();
            foreach (var item in dataItemsChiphi.Select((value, index) => new { value, index }))
            {
                item.value.STT = (item.index + 1).ToString();
            }
            ItemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(dataItemsChiphi);
        }

        private void LoadDefault()
        {
            tempListHangMuc = new ObservableCollection<NhDaGoiThauHangMucModel>();
            _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();
            _itemsHangMuc = new ObservableCollection<NhDaGoiThauHangMucModel>();
            _tongTienGoiThauNguonVon = new NhDaGoiThauNguonVonModel();
            _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            _itemsNguonVon = new ObservableCollection<NguonNganSachModel>();
            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
            _ttdaDuanHangMuc = new Dictionary<string, NhDaDuAnHangMuc>();
            _ttdaDaQdDauTuHangMuc = new Dictionary<string, NhDaDetailHangMucQuery>();
            _ttdaChuTruongDauTuHangMuc = new Dictionary<string, NhDaChuTruongDauTuHangMuc>();
        }

        private void LoadNguonVon()
        {
            var data = _nsNguonNganSachService.FindAll();
            _itemsNguonVon = _mapper.Map<ObservableCollection<NguonNganSachModel>>(data);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private ObservableCollection<NhDaGoiThauChiPhiModel> LoadChiPhiByNguonVon(NhDaGoiThauNguonVonModel nguonVon)
        {
            IEnumerable<NhDaGoiThauChiPhi> data = _nhDaGoiThauChiPhiService.FindAll(s => s.IIdGoiThauNguonVonId == nguonVon.Id).OrderBy(x => x.SMaOrder);
            ObservableCollection<NhDaGoiThauChiPhiModel> result = new ObservableCollection<NhDaGoiThauChiPhiModel>();
            if (!data.IsEmpty())
            {
                _itemsChiPhi ??= new ObservableCollection<NhDaGoiThauChiPhiModel>();
                var dataMap = _mapper.Map<List<NhDaGoiThauChiPhiModel>>(data);
                dataMap?.ForAll(s =>
                {
                    s.SMaChiPhi = s.SMaOrder;
                    s.STenNguonVon = nguonVon.STenNguonVon;
                    s.PropertyChanged += GoiThauChiPhi_PropertyChanged;
                    s.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(LoadNhDaGoiThauHangMucs(s));
                    s.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
                    // với trường hợp thông tin dự án thì không hiển thị giá trị sở cứ phê duyệt
                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
                    {
                        s.FTienGoiThauUsd = null;
                        s.FTienGoiThauVnd = null;
                        s.FTienGoiThauEur = null;
                        s.FTienGoiThauNgoaiTeKhac = null;
                    }
                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
                    {
                        s.FTienGoiThauUsd = null;
                        s.FTienGoiThauVnd = null;
                        s.FTienGoiThauEur = null;
                        s.FTienGoiThauNgoaiTeKhac = null;
                    }

                    // Map to giá trị sở cứ phê duyệt
                    if (s.IIdQdDauTuChiPhiId.HasValue && SLoaiSoCu == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU)
                    {
                        var qdDauTuChiPhi = _nhDaQdDauTuChiPhiService.FindById(s.IIdQdDauTuChiPhiId.Value);
                        if (qdDauTuChiPhi != null && qdDauTuChiPhi.IIdQdDauTuId != null)
                        {
                            s.FTienGoiThauUsd = qdDauTuChiPhi.FGiaTriUsd;
                            s.FTienGoiThauVnd = qdDauTuChiPhi.FGiaTriVnd;
                            s.FTienGoiThauEur = qdDauTuChiPhi.FGiaTriEur;
                            s.FTienGoiThauNgoaiTeKhac = qdDauTuChiPhi.FGiaTriNgoaiTeKhac;
                        }
                    }
                    _itemsChiPhi.Add(s);

                    var dataHangMuc = _nhDaGoiThauHangMucSerrvice.FindAll().Where(x => x.IIdGoiThauChiPhiId == s.Id).OrderBy(x => x.SMaOrder);
                    var temp = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(dataHangMuc);
                    foreach (var item in temp)
                    {
                        tempListHangMuc.Add(item);
                    }
                });
                result = new ObservableCollection<NhDaGoiThauChiPhiModel>(dataMap);
            }
            return result;
        }

        private void LoadNguonVonByGoiThau()
        {
            IEnumerable<NhDaGoiThauNguonVon> data = _nhDaGoiThauNguonVonService.GetListNguonVonByIdGoiThau(Model.Id);
            if (!data.IsEmpty())
            {
                _itemsGoiThauNguonVon = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVonModel>>(data);
                _itemsGoiThauNguonVon.ForAll(item =>
                {
                    item.STenNguonVon = _itemsNguonVon.Any(x => x.IIdMaNguonNganSach.Equals(item.IIdNguonVonId)) ? _itemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach.Equals(item.IIdNguonVonId)).STen : string.Empty;
                    item.GoiThauChiPhis = LoadChiPhiByNguonVon(item);
                    item.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                    item.IsChecked = true;
                    // Map giá trị sở cứ phê duyệt
                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN && item.IIdDuAnNguonVonId.HasValue)
                    {
                        var daNguonVon = _nhDaDuAnNguonVonService.FindById(item.IIdDuAnNguonVonId);
                        if (daNguonVon != null)
                        {
                            item.FTienGoiThauUsd = daNguonVon.FGiaTriUsd;
                            item.FTienGoiThauVnd = daNguonVon.FGiaTriVnd;
                            item.FTienGoiThauEur = daNguonVon.FGiaTriEur;
                            item.FTienGoiThauNgoaiTeKhac = daNguonVon.FGiaTriNgoaiTeKhac;
                        }
                    }

                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU && item.IIdChuTruongDauTuNguonVonId.HasValue)
                    {
                        var chuChuongDauTuNguonVon = _nhDaChuTruongDauTuNguonVonService.FindById(item.IIdChuTruongDauTuNguonVonId.Value);
                        if (chuChuongDauTuNguonVon != null)
                        {
                            item.FTienGoiThauUsd = chuChuongDauTuNguonVon.FGiaTriUsd;
                            item.FTienGoiThauVnd = chuChuongDauTuNguonVon.FGiaTriVnd;
                            item.FTienGoiThauEur = chuChuongDauTuNguonVon.FGiaTriEur;
                            item.FTienGoiThauNgoaiTeKhac = chuChuongDauTuNguonVon.FGiaTriNgoaiTeKhac;
                        }
                    }

                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU && item.IIdQddauTuNguonVonId.HasValue)
                    {
                        var qdDauTuNguonVon = _nhDaQdDauTuNguonVonService.FindById(item.IIdQddauTuNguonVonId.Value);
                        if (qdDauTuNguonVon != null)
                        {
                            item.FTienGoiThauUsd = qdDauTuNguonVon.FGiaTriUsd;
                            item.FTienGoiThauVnd = qdDauTuNguonVon.FGiaTriVnd;
                            item.FTienGoiThauEur = qdDauTuNguonVon.FGiaTriEur;
                            item.FTienGoiThauNgoaiTeKhac = qdDauTuNguonVon.FGiaTriNgoaiTeKhac;
                        }
                    }
                });
            }
            LoadDataDefaultByBase();
            Model.GoiThauNguonVons ??= new ObservableCollection<NhDaGoiThauNguonVonModel>();
            Model.GoiThauNguonVons = _itemsGoiThauNguonVon;
        }

        private void LoadDataDefaultByBase()
        {
            if (!NhHdnkPhuongAnNhapKhauModel.IIdDuAnId.IsNullOrEmpty() && SLoaiSoCu != SO_CU_TRUC_TIEP.CHUONG_TRINH)
            {
                List<NhDaGoiThauNguonVonModel> goiThauNguonVonModels = new List<NhDaGoiThauNguonVonModel>();
                if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
                {
                    IEnumerable<NhDaDuAnNguonVon> duAnNguonVons = _nhDaDuAnNguonVonService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                    goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                    {
                        //IIdDuAnNguonVonId = s.IIdDuAnId,
                        Id = Guid.NewGuid(),
                        IIdDuAnNguonVonId = s.Id,
                        IIdNguonVonId = s.IIdNguonVonId,
                        FTienGoiThauUsd = s.FGiaTriUsd,
                        FTienGoiThauVnd = s.FGiaTriVnd,
                        FTienGoiThauEur = s.FGiaTriEur,
                        FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                        //SMaOrder = s.SMaOrder
                    }).ToList();
                }
                else if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
                {
                    NhDaChuTruongDauTu chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                    if (chuTruongDauTu != null)
                    {
                        IEnumerable<NhDaChuTruongDauTuNguonVon> duAnNguonVons = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                        goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                        {
                            Id = Guid.NewGuid(),
                            IIdChuTruongDauTuNguonVonId = s.Id,
                            IIdNguonVonId = s.IIdNguonVonId,
                            FTienGoiThauUsd = s.FGiaTriUsd,
                            FTienGoiThauVnd = s.FGiaTriVnd,
                            FTienGoiThauEur = s.FGiaTriEur,
                            FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                            SMaOrder = s.SMaOrder
                        }).ToList();
                    }
                }
                else
                {
                    NhDaQdDauTu qdDauTu = _qdDauTuService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                    if (qdDauTu != null)
                    {
                        IEnumerable<NhDaQdDauTuNguonVon> duAnNguonVons = _nhDaQdDauTuNguonVonService.FindByQdDauTuId(qdDauTu.Id);
                        goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                        {
                            //IIdQddauTuNguonVonId = s.IIdQdDauTuId, // Chưa clear ??
                            Id = Guid.NewGuid(),
                            IIdQddauTuNguonVonId = s.Id,
                            IIdNguonVonId = s.IIdNguonVonId,
                            FTienGoiThauUsd = s.FGiaTriUsd,
                            FTienGoiThauVnd = s.FGiaTriVnd,
                            FTienGoiThauEur = s.FGiaTriEur,
                            FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                            SMaOrder = s.SMaOrder
                        }).ToList();
                    }
                }

                // nếu đã được lưu trong bảng tạm thì tải lên, thay vì lấy từ db
                if (!_itemsGoiThauNguonVon.IsEmpty())
                {
                    goiThauNguonVonModels = goiThauNguonVonModels.Where(x => !Model.GoiThauNguonVons.Select(s => s.IIdNguonVonId).Contains(x.IIdNguonVonId)).ToList();
                    goiThauNguonVonModels.ForAll(x =>
                    {
                        x.STenNguonVon = _itemsNguonVon.Any(y => y.IIdMaNguonNganSach.Equals(x.IIdNguonVonId)) ? _itemsNguonVon.FirstOrDefault(z => z.IIdMaNguonNganSach.Equals(x.IIdNguonVonId)).STen : string.Empty;
                        if (_itemsGoiThauNguonVon.Any(z => z.IIdNguonVonId.Equals(x.IIdNguonVonId)))
                        {
                            var item = _itemsGoiThauNguonVon.FirstOrDefault(z => z.IIdNguonVonId.Equals(x.IIdNguonVonId));
                            item.FTienGoiThauUsd = x.FTienGoiThauUsd;
                            item.FTienGoiThauVnd = x.FTienGoiThauVnd;
                        }
                        else
                        {
                            x.GoiThauChiPhis = LoadChiPhiByNguonVon(x);
                            _itemsGoiThauNguonVon.Add(x);
                        }
                    });
                    _itemsGoiThauNguonVon.ForAll(s =>
                    {
                        s.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                    });
                }

            }
        }

        private void LoadDuAnGoiThauNguonVon()
        {
            if (!Model.GoiThauNguonVons.IsEmpty() && !LstPackgeModified.IsEmpty() && LstPackgeModified.Any(x => x.Equals(Model.Id)))
            {
                _itemsGoiThauNguonVon = Model?.GoiThauNguonVons;
                _itemsGoiThauNguonVon.ForAll(s =>
                {
                    s.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                    if (s.IsChecked)
                        LoadGoiThauChiPhi(s);
                });
            }
            else
            {
                LoadNguonVonByGoiThau();
                _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>();
                if (!Model.GoiThauNguonVons.IsEmpty() && !IsAdded)
                {
                    _itemsGoiThauNguonVon = Model.GoiThauNguonVons;
                    if (!_itemsChiPhi.IsEmpty())
                        _itemsChiPhi.ForAll(f =>
                        {
                            f.STenNguonVon = Model.GoiThauNguonVons.Any(y => y.Id.Equals(f.IIdGoiThauNguonVonId)) ? ItemsNguonVon.FirstOrDefault(y => y.IIdMaNguonNganSach.Equals(Model.GoiThauNguonVons.FirstOrDefault(y => y.Id.Equals(f.IIdGoiThauNguonVonId)).IIdNguonVonId)).STen : string.Empty;
                            f.CanEditValue = _itemsChiPhi.Any(x => x.IsHangCha && !x.Id.Equals(f.IIdParentId) || !x.IsHangCha);
                        });
                }
                else if (IsAdded)
                {
                    // Thêm mới
                    //if (!Model.IIdDuAnId.IsNullOrEmpty() && SLoaiSoCu != SO_CU_TRUC_TIEP.CHUONG_TRINH)
                    if (!NhHdnkPhuongAnNhapKhauModel.IIdDuAnId.IsNullOrEmpty() && SLoaiSoCu != SO_CU_TRUC_TIEP.CHUONG_TRINH)
                    {
                        List<NhDaGoiThauNguonVonModel> goiThauNguonVonModels = new List<NhDaGoiThauNguonVonModel>();
                        if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
                        {
                            //IEnumerable<NhDaDuAnNguonVon> duAnNguonVons = _nhDaDuAnNguonVonService.FindByDuAnId(Model.IIdDuAnId ?? Guid.Empty);
                            IEnumerable<NhDaDuAnNguonVon> duAnNguonVons = _nhDaDuAnNguonVonService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                            goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                            {
                                //IIdDuAnNguonVonId = s.IIdDuAnId,
                                Id = Guid.NewGuid(),
                                IIdDuAnNguonVonId = s.Id,
                                IIdNguonVonId = s.IIdNguonVonId,
                                FTienGoiThauUsd = s.FGiaTriUsd,
                                FTienGoiThauVnd = s.FGiaTriVnd,
                                FTienGoiThauEur = s.FGiaTriEur,
                                FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                                //SMaOrder = s.SMaOrder
                            }).ToList();
                        }
                        else if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
                        {
                            //NhDaChuTruongDauTu chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(Model.IIdDuAnId ?? Guid.Empty);
                            NhDaChuTruongDauTu chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                            if (chuTruongDauTu != null)
                            {
                                IEnumerable<NhDaChuTruongDauTuNguonVon> duAnNguonVons = _nhDaChuTruongDauTuNguonVonService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
                                goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                                {
                                    Id = Guid.NewGuid(),
                                    IIdChuTruongDauTuNguonVonId = s.Id,
                                    IIdNguonVonId = s.IIdNguonVonId,
                                    FTienGoiThauUsd = s.FGiaTriUsd,
                                    FTienGoiThauVnd = s.FGiaTriVnd,
                                    FTienGoiThauEur = s.FGiaTriEur,
                                    FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                                    SMaOrder = s.SMaOrder
                                }).ToList();
                            }
                        }
                        else
                        {
                            //NhDaQdDauTu qdDauTu = _qdDauTuService.FindByDuAnId(Model.IIdDuAnId ?? Guid.Empty);
                            NhDaQdDauTu qdDauTu = _qdDauTuService.FindByDuAnId(NhHdnkPhuongAnNhapKhauModel.IIdDuAnId ?? Guid.Empty);
                            if (qdDauTu != null)
                            {
                                IEnumerable<NhDaQdDauTuNguonVon> duAnNguonVons = _nhDaQdDauTuNguonVonService.FindByQdDauTuId(qdDauTu.Id);
                                goiThauNguonVonModels = duAnNguonVons?.Select(s => new NhDaGoiThauNguonVonModel
                                {
                                    //IIdQddauTuNguonVonId = s.IIdQdDauTuId, // Chưa clear ??
                                    Id = Guid.NewGuid(),
                                    IIdQddauTuNguonVonId = s.Id,
                                    IIdNguonVonId = s.IIdNguonVonId,
                                    FTienGoiThauUsd = s.FGiaTriUsd,
                                    FTienGoiThauVnd = s.FGiaTriVnd,
                                    FTienGoiThauEur = s.FGiaTriEur,
                                    FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                                    SMaOrder = s.SMaOrder
                                }).ToList();
                            }
                        }

                        // nếu đã được lưu trong bảng tạm thì tải lên, thay vì lấy từ db
                        if (Model?.GoiThauNguonVons?.Any() == true)
                        {
                            _itemsGoiThauNguonVon = Model?.GoiThauNguonVons;
                        }
                        // nếu trong bảng tạm chưa có dữ liệu gói thầu nguồn vốn thì tự thêm vào
                        else
                        {
                            _itemsGoiThauNguonVon = new ObservableCollection<NhDaGoiThauNguonVonModel>(goiThauNguonVonModels);
                            Model ??= new NhDaGoiThauModel();
                            Model.GoiThauNguonVons = _itemsGoiThauNguonVon;
                            _itemsGoiThauNguonVon.ForAll(s =>
                            {
                                s.IsAdded = true;
                                s.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                            });
                        }

                        _itemsGoiThauNguonVon.ForAll(s =>
                        {
                            s.IsAdded = true;
                            s.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                        });
                    }
                    else if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHUONG_TRINH)
                    {

                        // nếu đã được lưu trong bảng tạm thì tải lên, thay vì lấy từ db
                        if (Model?.GoiThauNguonVons?.Any() == true)
                        {
                            _itemsGoiThauNguonVon = Model?.GoiThauNguonVons;
                            _itemsGoiThauNguonVon.ForAll(s =>
                            {
                                s.IsAdded = true;
                                s.PropertyChanged += GoiThauNguonVon_PropertyChanged;
                            });
                        }
                    }
                    else
                    {
                        NhDaGoiThauNguonVonModel item = new NhDaGoiThauNguonVonModel
                        {
                            Id = Guid.NewGuid(),
                            IIdNguonVonId = NHConstants.NGUON_VON_QUY_DU_TRU_NH,
                            IsAdded = true,
                            IsModified = true
                        };
                        item.PropertyChanged += GoiThauNguonVon_PropertyChanged;
                        _itemsGoiThauNguonVon.Insert(0, item);
                    }
                }
                else
                {
                    // Cập nhật hoặc điều chỉnh
                    IEnumerable<NhDaGoiThauNguonVon> data = _nhDaGoiThauNguonVonService.GetListNguonVonByIdGoiThau(Model.Id);
                    if (!data.IsEmpty())
                    {
                        _itemsGoiThauNguonVon = _mapper.Map<ObservableCollection<NhDaGoiThauNguonVonModel>>(data);
                        _itemsGoiThauNguonVon.ForAll(item =>
                        {
                            item.STenNguonVon = _itemsNguonVon.Any(x => x.IIdMaNguonNganSach.Equals(item.IIdNguonVonId)) ? _itemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach.Equals(item.IIdNguonVonId)).STen : string.Empty;
                            item.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
                            item.IsChecked = true;
                            // Map giá trị sở cứ phê duyệt
                            if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN && item.IIdDuAnNguonVonId.HasValue)
                            {
                                var daNguonVon = _nhDaDuAnNguonVonService.FindById(item.IIdDuAnNguonVonId);
                                if (daNguonVon != null)
                                {
                                    item.FTienGoiThauUsd = daNguonVon.FGiaTriUsd;
                                    item.FTienGoiThauVnd = daNguonVon.FGiaTriVnd;
                                    item.FTienGoiThauEur = daNguonVon.FGiaTriEur;
                                    item.FTienGoiThauNgoaiTeKhac = daNguonVon.FGiaTriNgoaiTeKhac;
                                }
                            }

                            if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU && item.IIdChuTruongDauTuNguonVonId.HasValue)
                            {
                                var chuChuongDauTuNguonVon = _nhDaChuTruongDauTuNguonVonService.FindById(item.IIdChuTruongDauTuNguonVonId.Value);
                                if (chuChuongDauTuNguonVon != null)
                                {
                                    item.FTienGoiThauUsd = chuChuongDauTuNguonVon.FGiaTriUsd;
                                    item.FTienGoiThauVnd = chuChuongDauTuNguonVon.FGiaTriVnd;
                                    item.FTienGoiThauEur = chuChuongDauTuNguonVon.FGiaTriEur;
                                    item.FTienGoiThauNgoaiTeKhac = chuChuongDauTuNguonVon.FGiaTriNgoaiTeKhac;
                                }
                            }

                            if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU && item.IIdQddauTuNguonVonId.HasValue)
                            {
                                var qdDauTuNguonVon = _nhDaQdDauTuNguonVonService.FindById(item.IIdQddauTuNguonVonId.Value);
                                if (qdDauTuNguonVon != null)
                                {
                                    item.FTienGoiThauUsd = qdDauTuNguonVon.FGiaTriUsd;
                                    item.FTienGoiThauVnd = qdDauTuNguonVon.FGiaTriVnd;
                                    item.FTienGoiThauEur = qdDauTuNguonVon.FGiaTriEur;
                                    item.FTienGoiThauNgoaiTeKhac = qdDauTuNguonVon.FGiaTriNgoaiTeKhac;
                                }
                            }
                        });
                    }
                }
            }            
            CaculatorTotalNguonVon();
            SetEnableItemsNguonVon();
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
        }

        private void LoadGoiThauChiPhi(NhDaGoiThauNguonVonModel nguonVon)
        {
            //_itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
            //if (_itemsChiPhiTemp.Count > 0 && _itemsChiPhiTemp.Clone().Select(n => n.IIdGoiThauNguonVonId).Contains(nguonVon.Id))
            //{
            //    foreach (var item in _itemsChiPhiTemp.Clone())
            //    {
            //        if (item.IIdGoiThauNguonVonId == nguonVon.Id)
            //            _itemsChiPhi.Add(item);
            //    }
            //    OnPropertyChanged(nameof(ItemsChiPhi));
            //}
            //else
            if (!Model.GoiThauNguonVons.IsEmpty() && !IsAdded)
            {
                foreach (var item in Model.GoiThauNguonVons)
                {
                    if (item.GoiThauChiPhis.IsEmpty()) break;
                    item.GoiThauChiPhis.ForAll(s =>
                    {
                        s.IsChecked = true;
                        if (s.IIdGoiThauNguonVonId == nguonVon.Id)
                            _itemsChiPhi.Add(s);
                    });
                }
            }
            else if (!Model.GoiThauNguonVons.IsEmpty() && !LstPackgeModified.IsEmpty() && LstPackgeModified.Any(x => x.Equals(Model.Id)))
            {
                foreach (var item in Model.GoiThauNguonVons)
                {
                    if (item.GoiThauChiPhis.IsEmpty()) continue;
                    item.GoiThauChiPhis.ForAll(s =>
                    {
                        s.IsChecked = true;
                        if (s.IIdGoiThauNguonVonId == nguonVon.Id)
                            _itemsChiPhi.Add(s);
                    });
                }
            }
            else if (IsAdded)
            {
                _itemsChiPhi ??= new ObservableCollection<NhDaGoiThauChiPhiModel>();
                // Thêm mới
                //if (!Model.IIdDuAnId.IsNullOrEmpty() && SLoaiSoCu != SO_CU_TRUC_TIEP.CHUONG_TRINH)
                if (!NhHdnkPhuongAnNhapKhauModel.IIdDuAnId.IsNullOrEmpty() && SLoaiSoCu != SO_CU_TRUC_TIEP.CHUONG_TRINH)
                {
                    if (SLoaiSoCu == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU)
                    {
                        IEnumerable<NhDaQdDauTuChiPhi> qdDauTuChiPhis = _nhDaQdDauTuChiPhiService.FindByNguonVonId(nguonVon.IIdQddauTuNguonVonId ?? Guid.Empty);
                        qdDauTuChiPhis.ForAll(s =>
                        {
                            if (_itemsChiPhi?.Any(m => m.STenChiPhi == s.STenChiPhi) == false)
                            {
                                _itemsChiPhi.Add(new NhDaGoiThauChiPhiModel
                                {
                                    Id = Guid.NewGuid(),
                                    IIdQdDauTuChiPhiId = s.Id,
                                    SMaOrder = s.SMaOrder,
                                    IIdGoiThauId = Model.Id,
                                    SMaChiPhi = StringUtils.ConvertMaOrder(s.SMaOrder),
                                    IIdGoiThauNguonVonId = nguonVon.Id,
                                    STenChiPhi = s.STenChiPhi,
                                    FTienGoiThauUsd = s.FGiaTriUsd,
                                    FTienGoiThauVnd = s.FGiaTriVnd,
                                    FTienGoiThauEur = s.FGiaTriEur,
                                    FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
                                    IIdParentId = s.IIdParentId,
                                    IIdChiPhiId = s.IIdChiPhiId,
                                    ItemsLoaiNoiDungChi = ItemsDMChiPhi,
                                });
                            }
                        });

                        _itemsChiPhi.ForAll(s =>
                        {
                            s.IsAdded = true;
                            s.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(LoadNhDaGoiThauHangMucs(s));
                            s.PropertyChanged += GoiThauChiPhi_PropertyChanged;
                        });

                        foreach (var item in _itemsChiPhi)
                        {
                            if (item.IIdParentId != null)
                            {
                                NhDaGoiThauChiPhiModel temp = _itemsChiPhi.Where(x => x.IIdQdDauTuChiPhiId == item.IIdParentId).FirstOrDefault();
                                if (temp != null)
                                {
                                    item.IIdParentId = temp.Id;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                _itemsChiPhi ??= new ObservableCollection<NhDaGoiThauChiPhiModel>();

                // Cập nhật hoặc điều chỉnh
                IEnumerable<NhDaGoiThauChiPhi> data = _nhDaGoiThauChiPhiService.FindAll(s => s.IIdGoiThauNguonVonId == nguonVon.Id).OrderBy(x => x.SMaOrder);
                if (!data.IsEmpty())
                {
                    var tempData = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == nguonVon.Id)?.GoiThauChiPhis;
                    var dataMap = tempData ?? _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(data);
                    dataMap?.ForAll(s =>
                        {
                            s.STenNguonVon = nguonVon.STenNguonVon;
                            s.PropertyChanged += GoiThauChiPhi_PropertyChanged;
                            s.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(LoadNhDaGoiThauHangMucs(s));

                            // với trường hợp thông tin dự án thì không hiển thị giá trị sở cứ phê duyệt
                            if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
                            {
                                s.FTienGoiThauUsd = null;
                                s.FTienGoiThauVnd = null;
                                s.FTienGoiThauEur = null;
                                s.FTienGoiThauNgoaiTeKhac = null;
                            }
                            if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
                            {
                                s.FTienGoiThauUsd = null;
                                s.FTienGoiThauVnd = null;
                                s.FTienGoiThauEur = null;
                                s.FTienGoiThauNgoaiTeKhac = null;
                            }

                            // Map to giá trị sở cứ phê duyệt
                            if (s.IIdQdDauTuChiPhiId.HasValue && SLoaiSoCu == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU)
                            {
                                var qdDauTuChiPhi = _nhDaQdDauTuChiPhiService.FindById(s.IIdQdDauTuChiPhiId.Value);
                                if (qdDauTuChiPhi != null && qdDauTuChiPhi.IIdQdDauTuId != null)
                                {
                                    s.FTienGoiThauUsd = qdDauTuChiPhi.FGiaTriUsd;
                                    s.FTienGoiThauVnd = qdDauTuChiPhi.FGiaTriVnd;
                                    s.FTienGoiThauEur = qdDauTuChiPhi.FGiaTriEur;
                                    s.FTienGoiThauNgoaiTeKhac = qdDauTuChiPhi.FGiaTriNgoaiTeKhac;
                                }
                            }
                            _itemsChiPhi.Add(s);

                            var dataHangMuc = _nhDaGoiThauHangMucSerrvice.FindAll().Where(x => x.IIdGoiThauChiPhiId == s.Id).OrderBy(x => x.SMaOrder);
                            var temp = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(dataHangMuc);
                            foreach (var item in temp)
                            {
                                tempListHangMuc.Add(item);
                            }
                        });
                }
            }

            //Add hạng mục vào list chung
            //foreach (var item in ItemsChiPhi)
            //{
            //    item.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            //    if (SLoaiSoCu == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU)
            //    {
            //        //Nếu lish kh tồn tại các hạng mục có idChiPhi đang chọn
            //        if (tempListHangMuc == null || (tempListHangMuc != null && !tempListHangMuc.Any(x => x.IIdGoiThauChiPhiId == item.Id)))
            //        {
            //            //Add danh sách hạng mục vào lish chung
            //            IEnumerable<NhDaQdDauTuHangMuc> qdDauTuHangMucs = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(item.IIdQdDauTuChiPhiId.Value);
            //            qdDauTuHangMucs?.ForAll(s =>
            //                {
            //                    tempListHangMuc.Add(new NhDaGoiThauHangMucModel
            //                    {
            //                        Id = Guid.NewGuid(),
            //                        IIdGoiThauChiPhiId = item.Id,
            //                        IIdParentId = s.IIdParentId,
            //                        FTienGoiThauUsd = s.FGiaTriUsd,
            //                        FTienGoiThauVnd = s.FGiaTriVnd,
            //                        FTienGoiThauEur = s.FGiaTriEur,
            //                        FTienGoiThauNgoaiTeKhac = s.FGiaTriNgoaiTeKhac,
            //                        SMaOrder = s.SMaOrder,
            //                        SMaHangMuc = s.SMaHangMuc,
            //                        STenHangMuc = s.STenHangMuc,
            //                        IIdQdDauTuHangMucId = s.Id,
            //                    });
            //                });
            //        }
            //    }
            //}

            tempListHangMuc ??= new ObservableCollection<NhDaGoiThauHangMucModel>();
            ItemsChiPhi.ForAll(x =>
            {
                x.STenNguonVon = Model.GoiThauNguonVons.Any(y => y.Id.Equals(x.IIdGoiThauNguonVonId)) ? ItemsNguonVon.FirstOrDefault(y => y.IIdMaNguonNganSach.Equals(Model.GoiThauNguonVons.FirstOrDefault(y => y.Id.Equals(x.IIdGoiThauNguonVonId)).IIdNguonVonId)).STen : string.Empty;
                if (!x.GoiThauHangMucs.IsEmpty())
                {
                    if (!tempListHangMuc.IsEmpty())
                    {
                        if (tempListHangMuc.Any(y => !x.GoiThauHangMucs.Select(z => z.Id).Contains(y.Id)))
                        {
                            x.GoiThauHangMucs.Where(y => !tempListHangMuc.Select(z => z.Id).Contains(y.Id)).ForAll(f =>
                            {
                                tempListHangMuc.Add(f);
                            });
                        }

                    }
                    else
                    {
                        x.GoiThauHangMucs.ForAll(x => { tempListHangMuc.Add(x); });
                    }
                }

            });

            OrderItemsChiPhi();
            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(_itemsChiPhi.OrderBy(s => s.SMaOrder));
            UpdateTreeItemsChiPhi();
            CaculatorTotalChiPhi();
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void OnAddDuAnGoiThauNguonVon()
        {
            _itemsGoiThauNguonVon ??= new ObservableCollection<NhDaGoiThauNguonVonModel>();
            int currentRow = -1;
            if (!_itemsGoiThauNguonVon.IsEmpty())
            {
                currentRow = 0;
                if (SelectedGoiThauNguonVon != null)
                {
                    currentRow = _itemsGoiThauNguonVon.IndexOf(SelectedGoiThauNguonVon);
                }
            }

            NhDaGoiThauNguonVonModel targetItem = new NhDaGoiThauNguonVonModel
            {
                Id = Guid.NewGuid(),
                IsAdded = true,
                IsModified = true
            };
            targetItem.PropertyChanged += GoiThauNguonVonRenew_PropertyChanged;
            _itemsGoiThauNguonVon.Insert(currentRow + 1, targetItem);
            if (Model.GoiThauNguonVons == null) Model.GoiThauNguonVons = new ObservableCollection<NhDaGoiThauNguonVonModel>();
            Model.GoiThauNguonVons.Add(targetItem);
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
        }

        private void OnDeleteDuAnGoiThauNguonVon()
        {
            if (SelectedGoiThauNguonVon != null)
            {
                SelectedGoiThauNguonVon.IsDeleted = !SelectedGoiThauNguonVon.IsDeleted;
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
                CaculatorTotalNguonVon();
            }
        }

        private void OnAddGoiThauChiPhi(object obj)
        {
            if (SelectedGoiThauNguonVon == null) return;
            _itemsChiPhi ??= new ObservableCollection<NhDaGoiThauChiPhiModel>();
            NhDaGoiThauChiPhiModel sourceItem = _selectedChiPhi;
            NhDaGoiThauChiPhiModel targetItem = new NhDaGoiThauChiPhiModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!_itemsChiPhi.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = _itemsChiPhi.IndexOf(sourceItem) + CountTreeChildItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = _itemsChiPhi.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                targetItem.IIdQdDauTuChiPhiId = sourceItem.IIdQdDauTuChiPhiId;
                // targetItem.IIdGoiThauNguonVonId = sourceItem.IIdGoiThauNguonVonId;
                // targetItem.STenChiPhi = sourceItem.STenChiPhi;
            }
            targetItem.IIdGoiThauNguonVonId = SelectedGoiThauNguonVon.Id;
            targetItem.STenNguonVon = ItemsNguonVon.Any(x => x.IIdMaNguonNganSach.Equals(SelectedGoiThauNguonVon.IIdNguonVonId)) ? ItemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach.Equals(SelectedGoiThauNguonVon.IIdNguonVonId)).STen : string.Empty;
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            targetItem.PropertyChanged += GoiThauChiPhi_PropertyChanged;
            _itemsChiPhi.Insert(currentRow + 1, targetItem);

            var rawGoiThauChiPhis = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis;
            rawGoiThauChiPhis?.Add(targetItem);
            OrderItemsChiPhi(targetItem.IIdParentId);
            UpdateTreeItemsChiPhi();
            SelectedGoiThauNguonVon.GoiThauChiPhis ??= new ObservableCollection<NhDaGoiThauChiPhiModel>();
            SelectedGoiThauNguonVon.GoiThauChiPhis.Add(targetItem);
            OnPropertyChanged(nameof(SelectedGoiThauNguonVon));
            OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        private void OnDeleteGoiThauChiPhi()
        {
            if (SelectedChiPhi != null)
            {
                DeleteTreeItemsChiPhi(SelectedChiPhi, !SelectedChiPhi.IsDeleted);
                CaculatorTotalChiPhi();
            }
        }

        private void GoiThauChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (NhDaGoiThauChiPhiModel)sender;

            if (e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)
                || e.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac))
            {
                CalculateChiPhi();
                CaculateNguonVon();
                CaculatorTotalChiPhi();
            }
        }

        private void UpdateTreeItemsChiPhi()
        {
            if (!ItemsChiPhi.IsEmpty())
            {
                ItemsChiPhi.ForAll(s => s.CanEditValue = !ItemsChiPhi.Any(y => y.IIdParentId == s.Id));
                ItemsChiPhi.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsChiPhi.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void DeleteTreeItemsChiPhi(NhDaGoiThauChiPhiModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = ItemsChiPhi;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItemsChiPhi(item, status);
                    }
                }
            }
        }

        private void OrderItemsChiPhi(Guid? parentId = null)
        {
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = ItemsChiPhi.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaChiPhi = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItemsChiPhi(child.Id);
                    index++;
                }
            }
        }

        private int CountTreeChildItems(NhDaGoiThauChiPhiModel currentItem)
        {
            int count = 0;
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildItems(item);
                }
            }
            return count;
        }

        public override void Dispose()
        {
            Model.GoiThauNguonVons.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                Dispose();
                window.Close();
            }
        }

        #region Event
        private void OnShowHangMucDetail(object obj)
        {
            SelectedChiPhi.STenChiPhi = (from data in ItemsDMChiPhi where data.IIdChiPhi == SelectedChiPhi.IIdChiPhiId select data).First().STenChiPhi.ToString();
            txtDetail = "THÔNG TIN HẠNG MỤC - Nguồn vốn: " + SelectedChiPhi.STenNguonVon + "    Chi phí: " + SelectedChiPhi.STenChiPhi;
            IsSelectedChiPhi = true;
            LoadDataHangMuc();
            //if (SelectedChiPhi == null) return;

            //var rawChiPhi = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == SelectedChiPhi.Id);

            //if (rawChiPhi?.GoiThauHangMucs?.Any() == true)
            //{
            //    SelectedChiPhi = rawChiPhi;
            //}

            //if (SelectedChiPhi?.GoiThauHangMucs?.Any() != true)
            //{
            //    foreach (var item in ItemsChiPhi)
            //    {
            //        var goiThauHangMucFromDatabase = LoadNhDaGoiThauHangMucsByIdGoiThauChiPhi(item);

            //        if (goiThauHangMucFromDatabase.Count > 0)
            //        {
            //            item.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(goiThauHangMucFromDatabase);
            //        }
            //    }
            //}

            //MSNHPhuongAnNhapKhauItemDialogViewModel.Model = SelectedChiPhi;
            ////MSNHPhuongAnNhapKhauItemDialogViewModel.NhDaGoiThauHangMucs = GetNhDaGoiThauHangMucs();
            //MSNHPhuongAnNhapKhauItemDialogViewModel.IsAdded = IsAdded;
            //MSNHPhuongAnNhapKhauItemDialogViewModel.IsDetail = BIsReadOnly;
            //MSNHPhuongAnNhapKhauItemDialogViewModel.IsEnableSoCuChuongTrinh = IsEnableSoCuChuongTrinh;
            //MSNHPhuongAnNhapKhauItemDialogViewModel.IsEnableSoCuQdDauTu = IsEnableSoCuQdDauTu;
            //MSNHPhuongAnNhapKhauItemDialogViewModel.CurrencyExchangeAction = (obj) => GoiThauHangMucCurrencyExchange(obj);
            //MSNHPhuongAnNhapKhauItemDialogViewModel.Init();
            //MSNHPhuongAnNhapKhauItemDialogViewModel.SavedAction = obj =>
            //{
            //    var data = (obj as IEnumerable<NhDaGoiThauHangMucModel>).Where(s => !s.IsDeleted);

            //    // Tính tổng tiền hạng mục
            //    if (!data.IsEmpty())
            //    {
            //        SelectedChiPhi.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(data);
            //        foreach (var ch in SelectedChiPhi.GoiThauHangMucs)
            //            ch.IIdGoiThauChiPhiId = SelectedChiPhi.Id;
            //        // gán giá trị cho dữ liệu tạm
            //        var rawGoiThauNguonVons = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id);
            //        var rawGoiThauChiPhi = rawGoiThauNguonVons?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == SelectedChiPhi.Id);
            //        var screenGoiThauChiPhi = ItemsChiPhi?.FirstOrDefault(m => m.Id == SelectedChiPhi.Id);

            //        if (rawGoiThauChiPhi != null)
            //        {
            //            rawGoiThauChiPhi.GoiThauHangMucs = SelectedChiPhi.GoiThauHangMucs;
            //            screenGoiThauChiPhi.GoiThauHangMucs = SelectedChiPhi.GoiThauHangMucs;
            //        }

            //        var goiThauChiPhis = data.Where(s => s.IIdParentId == null && !s.IsDeleted);
            //        SelectedChiPhi.FTienGoiThauUsd = goiThauChiPhis.Sum(s => s.FTienGoiThauUsd);
            //        SelectedChiPhi.FTienGoiThauVnd = goiThauChiPhis.Sum(s => s.FTienGoiThauVnd);
            //        SelectedChiPhi.FTienGoiThauEur = goiThauChiPhis.Sum(s => s.FTienGoiThauEur);
            //        SelectedChiPhi.FTienGoiThauNgoaiTeKhac = goiThauChiPhis.Sum(s => s.FTienGoiThauNgoaiTeKhac);
            //        SelectedChiPhi.FGiaTriUsd = goiThauChiPhis.Sum(s => s.FGiaTriUsd);
            //        SelectedChiPhi.FGiaTriEur = goiThauChiPhis.Sum(s => s.FGiaTriEur);
            //        SelectedChiPhi.FGiaTriVnd = goiThauChiPhis.Sum(s => s.FGiaTriVnd);
            //        SelectedChiPhi.FGiaTriNgoaiTeKhac = goiThauChiPhis.Sum(s => s.FGiaTriNgoaiTeKhac);
            //        SelectedChiPhi.IsModified = true;
            //        SelectedGoiThauNguonVon.IsModified = true;
            //        CalculateChiPhi();
            //    }
            //};
            //SuggestionsHangMuc();
        }

        /// <summary>
        /// Gợi ý nhập hạng mục, được lấy theo sở cứ trục tiếp tương ứng
        /// </summary>
        private void SuggestionsHangMuc()
        {
            MSNHPhuongAnNhapKhauItemDialogViewModel.ShowDialogHost("NHPhuongAnNhapKhauItemDialog");
            #region Hoàng tâm BA - Hiển thị kế thừa đầy đủ thông tin từ TTDA
            switch (SLoaiSoCu)
            {
                case SO_CU_TRUC_TIEP.THONG_TIN_DU_AN:
                    if (Model?.IIdDuAnId != null && (_ttdaDuanHangMuc == null || _ttdaDuanHangMuc.Count <= 0))
                        _ttdaDuanHangMuc = _nhDaDuAnHangMucService?.FindByDuAnId(Model.IIdDuAnId.Value)?.ToDictionary(m => m.STenHangMuc);

                    if (_ttdaDuanHangMuc != null && _ttdaDuanHangMuc.Count > 0)
                    {
                        // thêm dữ liệu từ được tải từ thông tin dự án vào
                        var dsDuanHangMuc = ItemsChiPhi?.SelectMany(m => m.GoiThauHangMucs?.ToList());
                        foreach (var item in _ttdaDuanHangMuc.Keys)
                        {
                            if (dsDuanHangMuc.Any(m => m.STenHangMuc == item)) continue;
                            MSNHPhuongAnNhapKhauItemDialogViewModel.OnAddGoiThauHangMuc(new NhDaGoiThauHangMucModel
                            {
                                Id = _ttdaDuanHangMuc[item].Id,
                                IIdCacQuyetDinhHangMucId = Model.IIdCacQuyetDinhId,
                                IIdParentId = _ttdaDuanHangMuc[item].IIdParentId,
                                IIdGoiThauChiPhiId = SelectedChiPhi.Id,
                                IsSuggestion = true,
                                SMaHangMuc = _ttdaDuanHangMuc[item].SMaHangMuc,
                                SMaOrder = StringUtils.ConvertMaOrder(_ttdaDuanHangMuc[item].SMaOrder),
                                STenHangMuc = _ttdaDuanHangMuc[item].STenHangMuc,
                            });
                        }
                    }
                    break;
                case SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU:
                    if (Model?.IIdDuAnId != null && (_ttdaDaQdDauTuHangMuc == null || _ttdaDaQdDauTuHangMuc.Count <= 0))
                        _ttdaDaQdDauTuHangMuc = _nhDaQdDauTuHangMucService?.GetHangMucByQdDauTuId(Model.IIdCacQuyetDinhId.Value)?.ToDictionary(m => m.STenHangMuc);

                    if (_ttdaDaQdDauTuHangMuc != null && _ttdaDaQdDauTuHangMuc.Count > 0)
                    {
                        // thêm dữ liệu từ được tải từ thông tin dự án vào
                        var dsDauTuHangMuc = ItemsChiPhi?.SelectMany(m => m.GoiThauHangMucs?.ToList());
                        foreach (var item in _ttdaDaQdDauTuHangMuc.Keys)
                        {
                            if (dsDauTuHangMuc.Any(m => m.STenHangMuc == item)) continue;
                            MSNHPhuongAnNhapKhauItemDialogViewModel.OnAddGoiThauHangMuc(new NhDaGoiThauHangMucModel
                            {
                                Id = _ttdaDaQdDauTuHangMuc[item].IIdHangMucID,
                                IIdParentId = _ttdaDuanHangMuc[item].IIdParentId,
                                IsSuggestion = true,
                                IIdCacQuyetDinhHangMucId = Model.IIdCacQuyetDinhId,
                                IIdGoiThauChiPhiId = SelectedChiPhi.Id,
                                SMaHangMuc = _ttdaDaQdDauTuHangMuc[item].SMaHangMuc,
                                SMaOrder = StringUtils.ConvertMaOrder(_ttdaDaQdDauTuHangMuc[item].SMaOrder),
                                STenHangMuc = _ttdaDaQdDauTuHangMuc[item].STenHangMuc,
                                FGiaTriUsd = _ttdaDaQdDauTuHangMuc[item].FGiaTriUSD,
                                FGiaTriVnd = _ttdaDaQdDauTuHangMuc[item].FGiaTriVND,
                                FGiaTriEur = _ttdaDaQdDauTuHangMuc[item].FGiaTriEUR,
                                FGiaTriNgoaiTeKhac = _ttdaDaQdDauTuHangMuc[item].FGiaTriNgoaiTeKhac
                            });
                        }
                    }
                    break;
                case SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU:
                    if (Model?.IIdDuAnId != null && (_ttdaChuTruongDauTuHangMuc == null || _ttdaChuTruongDauTuHangMuc.Count <= 0))
                        _ttdaChuTruongDauTuHangMuc = _nhDaChuTruongDauTuHangMucService?.FindByChuTruongDauTuId(Model.IIdCacQuyetDinhId.Value)?.ToDictionary(m => m.STenHangMuc);

                    if (_ttdaChuTruongDauTuHangMuc != null && _ttdaChuTruongDauTuHangMuc.Count > 0)
                    {
                        // thêm dữ liệu từ được tải từ thông tin dự án vào
                        var dsDauTuHangMuc = ItemsChiPhi?.SelectMany(m => m.GoiThauHangMucs?.ToList());
                        foreach (var item in _ttdaChuTruongDauTuHangMuc.Keys)
                        {
                            if (dsDauTuHangMuc.Any(m => m.STenHangMuc == item)) continue;
                            MSNHPhuongAnNhapKhauItemDialogViewModel.OnAddGoiThauHangMuc(new NhDaGoiThauHangMucModel
                            {
                                Id = _ttdaChuTruongDauTuHangMuc[item].Id,
                                IIdCacQuyetDinhHangMucId = Model.IIdCacQuyetDinhId,
                                IIdParentId = _ttdaChuTruongDauTuHangMuc[item].IIdParentId,
                                IIdGoiThauChiPhiId = SelectedChiPhi.Id,
                                IsSuggestion = true,
                                SMaHangMuc = _ttdaChuTruongDauTuHangMuc[item].SMaHangMuc,
                                SMaOrder = StringUtils.ConvertMaOrder(_ttdaChuTruongDauTuHangMuc[item].SMaOrder),
                                STenHangMuc = _ttdaChuTruongDauTuHangMuc[item].STenHangMuc,
                            }); ;
                        }
                    }
                    break;
                case SO_CU_TRUC_TIEP.CHUONG_TRINH:
                    break;
                default:
                    break;
            }
            #endregion
        }

        private void GoiThauHangMucCurrencyExchange(object obj)
        {
            OnCellEditEnding(obj);
        }

        private void OnSaveData(object obj)
        {
            LstPackgeModified ??= new HashSet<Guid>();
            LstPackgeModified.Add(Model.Id);
            if (SelectedChiPhi?.IsModified == true)
                SelectedGoiThauNguonVon.IsModified = true;
            Model.GoiThauNguonVons = ItemsGoiThauNguonVon;
            if (Model.GoiThauNguonVons.IsEmpty()) return;
            ItemsChiPhi.Where(x => !x.IsDeleted).ForAll(s =>
            {
                if (!s.IIdChiPhiId.IsNullOrEmpty())
                {
                    s.STenChiPhi = ItemsDMChiPhi.Any(y => y.IIdChiPhi.Equals(s.IIdChiPhiId)) ? ItemsDMChiPhi.FirstOrDefault(y => y.IIdChiPhi.Equals(s.IIdChiPhiId)).STenChiPhi : string.Empty;
                }
            });

            foreach (var item in Model.GoiThauNguonVons)
            {
                if (!ValidateViewModelHelper.Validate(item)) return;
                item.GoiThauChiPhis = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdGoiThauNguonVonId == item.Id));

                foreach (var itemHangMuc in item.GoiThauChiPhis)
                {
                    itemHangMuc.GoiThauHangMucs = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(tempListHangMuc.Where(x => !x.IsDeleted && x.IIdGoiThauChiPhiId == itemHangMuc.Id));
                }
            }

            var dataHangMuc = _mapper.Map<IEnumerable<NhDaGoiThauHangMucModel>>(ItemsHangMuc).ToList();
            dataHangMuc.ForEach(x =>
            {
                x.PropertyChanged -= HangMuc_PropertyChanged;
            });

            SavedAction?.Invoke(Model);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            Window window = obj as Window;
            window.Close();
            //this.OnClose();
        }

        public override void OnSave()
        {

        }

        #endregion

        private void GoiThauNguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SelectedGoiThauNguonVon = (NhDaGoiThauNguonVonModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriUsd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriVnd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriEur) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriNgoaiTeKhac))
            {
                CaculateNguonVon();
            }

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.IsChecked))
            {
                if (SelectedGoiThauNguonVon.IsChecked) // khi tích chọn nguồn vốn
                {
                    IsSelectedNguonVon = true;
                    var dataMap = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id).ToList();
                    var unsaveItemChiPhis = _itemsChiPhi?.Except(dataMap);

                    // nếu dữ liệu tạm chưa có và dữ liệu không thuộc SelectedGoiThauNguonVon hiện tại thì lưu lại
                    if (unsaveItemChiPhis?.Any() == true)
                    {
                        foreach (var item in unsaveItemChiPhis)
                        {
                            item.IsChecked = true;
                            var unsaveGoiThauNguonVon = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == item.IIdGoiThauNguonVonId);
                            if (unsaveGoiThauNguonVon?.GoiThauChiPhis != null
                                && !unsaveGoiThauNguonVon.GoiThauChiPhis.Any(m => m.Id == item.Id))
                                unsaveGoiThauNguonVon.GoiThauChiPhis.Add(item);
                        }
                    }

                    var rawGoiThauChiPhis = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis;
                    if (IsAdded && rawGoiThauChiPhis?.Any() == true)
                    {
                        foreach (var item in rawGoiThauChiPhis)
                        {
                            item.IsChecked = true;
                            if (ItemsChiPhi?.Any(m => m.Id == item.Id) == true) continue;
                            ItemsChiPhi.Add(item);
                        }
                    }
                    else
                    {
                        SelectedGoiThauNguonVon.GoiThauChiPhis = _mapper.Map<ObservableCollection<NhDaGoiThauChiPhiModel>>(dataMap);
                    }

                    LoadGoiThauChiPhi(SelectedGoiThauNguonVon);
                }
                else // khi bỏ chọn nguồn vốn
                {
                    var rawGoiThauNguonVon = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id) ?? SelectedGoiThauNguonVon;
                    if (IsAdded && Model?.GoiThauNguonVons?.Any(m => m.Id == SelectedGoiThauNguonVon.Id) != true)
                    {
                        Model.GoiThauNguonVons.Add(SelectedGoiThauNguonVon);
                    }

                    // không xoá trong bảng tạm đi khi bỏ chọn
                    var selectedItemChiPhis = _itemsChiPhi?.Where(s => s.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id)?.ToList();
                    if (selectedItemChiPhis?.Any() == true)
                    {
                        foreach (var item in selectedItemChiPhis)
                        {
                            item.IsChecked = false;
                        }
                        if (IsAdded && _itemsChiPhi?.Any() == true
                            && rawGoiThauNguonVon != null)
                        {
                            selectedItemChiPhis.ForEach(m =>
                            {
                                if (!rawGoiThauNguonVon.GoiThauChiPhis.Any(n => n.Id == m.Id))
                                {
                                    rawGoiThauNguonVon.GoiThauChiPhis.Add(m);
                                }
                            });
                        }
                    }

                    // Xoá những chi phí có nguồn vốn có IsChecked = false
                    IsSelectedNguonVon = false;
                    List<string> LstTenChiPhiItemTemp = _itemsChiPhiTemp.Select(n => n.STenChiPhi).ToList();
                    foreach (var item in _itemsChiPhi.Clone())
                    {
                        if (!LstTenChiPhiItemTemp.Contains(item.STenChiPhi))
                            _itemsChiPhiTemp.Add(item);
                    }
                    OnPropertyChanged(nameof(ItemsChiPhiTemp));
                    List<NhDaGoiThauChiPhiModel> lstRemove = selectedItemChiPhis.Where(m => !m.IsChecked && m.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id).ToList();
                    lstRemove.ForEach(s => _itemsChiPhi.Remove(s));
                }

                CaculateNguonVon();
            }

            if (args.PropertyName.Equals(nameof(NhDaGoiThauNguonVonModel.IIdNguonVonId)))
            {
                SetEnableItemsNguonVon();
                SelectedGoiThauNguonVon.IsModified = true;
            }
        }

        public void GoiThauChiPhi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedChiPhi = (NhDaGoiThauChiPhiModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedChiPhi.CanEditValue;
            }
        }

        private void GoiThauNguonVonRenew_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SelectedGoiThauNguonVon = (NhDaGoiThauNguonVonModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriUsd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriVnd) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriEur) ||
                args.PropertyName == nameof(NhDaGoiThauNguonVonModel.FGiaTriNgoaiTeKhac))
            {
                CaculateNguonVon();
            }

            if (args.PropertyName == nameof(NhDaGoiThauNguonVonModel.IsChecked))
            {
                if (SelectedGoiThauNguonVon.IsChecked) // khi tích chọn nguồn vốn
                {
                    IsSelectedNguonVon = true;
                    if (!SelectedGoiThauNguonVon.GoiThauChiPhis.IsEmpty())
                    {
                        if (_itemsChiPhi.IsEmpty())
                        {
                            _itemsChiPhi = SelectedGoiThauNguonVon.GoiThauChiPhis;
                        }
                        else
                        {
                            var dataChiPhisb = _itemsChiPhi.ToList();
                            dataChiPhisb.AddRange(SelectedGoiThauNguonVon.GoiThauChiPhis.Where(x => !dataChiPhisb.Select(y => y.Id).Contains(x.Id)));
                            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(dataChiPhisb);
                        }
                    }
                    else
                    {
                        LoadGoiThauChiPhi(SelectedGoiThauNguonVon);
                    }
                    //var dataMap = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == SelectedGoiThauNguonVon.Id).ToList();
                    //var unsaveItemChiPhis = _itemsChiPhi?.Except(dataMap);
                    //var unsaveItemChiPhisdd = _itemsChiPhi?.Intersect(dataMap);

                    //LoadGoiThauChiPhi(SelectedGoiThauNguonVon);
                }
                else // khi bỏ chọn nguồn vốn
                {
                    if (!SelectedGoiThauNguonVon.GoiThauChiPhis.IsEmpty())
                    {
                        if (!_itemsChiPhi.IsEmpty())
                        {
                            _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>(_itemsChiPhi.Where(x => !SelectedGoiThauNguonVon.GoiThauChiPhis.Select(y => y.Id).Contains(x.Id)).ToList());
                        }
                    }
                    else
                    {
                        _itemsChiPhi = new ObservableCollection<NhDaGoiThauChiPhiModel>();
                    }
                }
                OnPropertyChanged(nameof(ItemsChiPhi));
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
                CalculateChiPhi();
                CaculatorTotalChiPhi();
                CaculateNguonVon();
                LoadSttChiPhi();
            }

            if (args.PropertyName.Equals(nameof(NhDaGoiThauNguonVonModel.IIdNguonVonId)))
            {
                SetEnableItemsNguonVon();
                SelectedGoiThauNguonVon.IsModified = true;
            }
        }


        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaGoiThauChiPhiModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriUsd)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriVnd)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriEur)
                || args.PropertyName == nameof(NhDaGoiThauChiPhiModel.FGiaTriNgoaiTeKhac))
            {
                CalculateChiPhi();
                CaculatorTotalChiPhi();
            }
        }

        private void CaculatorTotalNguonVon()
        {
            if (ItemsGoiThauNguonVon.IsEmpty()) return;
            _tongTienGoiThauNguonVon.FGiaTriUsd = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted && n.IsChecked).Sum(n => n.FGiaTriUsd);
            _tongTienGoiThauNguonVon.FGiaTriEur = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted && n.IsChecked).Sum(n => n.FGiaTriEur);
            _tongTienGoiThauNguonVon.FGiaTriVnd = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted && n.IsChecked).Sum(n => n.FGiaTriVnd);
            _tongTienGoiThauNguonVon.FGiaTriNgoaiTeKhac = ItemsGoiThauNguonVon.Where(n => !n.IsDeleted && n.IsChecked).Sum(n => n.FGiaTriNgoaiTeKhac);
            OnPropertyChanged(nameof(TongTienGoiThauNguonVon));
        }

        private void CaculatorTotalChiPhi()
        {
            //if (ItemsChiPhi.IsEmpty()) return;
            //var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
            //if (sumChiPhis.Any())
            if (_itemsChiPhi.IsEmpty())
            {
                _tongTienChiPhi = new NhDaGoiThauChiPhiModel();
            }
            else
            {
                var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
                _tongTienChiPhi.FGiaTriUsd = sumChiPhis.Sum(n => n.FGiaTriUsd);
                _tongTienChiPhi.FGiaTriEur = sumChiPhis.Sum(n => n.FGiaTriEur);
                _tongTienChiPhi.FGiaTriVnd = sumChiPhis.Sum(n => n.FGiaTriVnd);
                _tongTienChiPhi.FGiaTriNgoaiTeKhac = sumChiPhis.Sum(n => n.FGiaTriNgoaiTeKhac);
                //_tongTienChiPhi.FGiaTriUsd = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriUsd);
                //_tongTienChiPhi.FGiaTriEur = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriEur);
                //_tongTienChiPhi.FGiaTriVnd = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriVnd);
                //_tongTienChiPhi.FGiaTriNgoaiTeKhac = ItemsChiPhi.Where(n => !n.IsDeleted).Sum(n => n.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(TongTienChiPhi));
            }
        }

        private void SetEnableItemsNguonVon()
        {
            if (!_itemsNguonVon.IsEmpty())
            {
                _itemsNguonVon.ForAll(x =>
                {
                    x.IsEnabled = _itemsGoiThauNguonVon.IsEmpty() || !_itemsGoiThauNguonVon.Any(y => y.IIdNguonVonId != null && y.IIdNguonVonId.Equals(x.IIdMaNguonNganSach));
                });
            }
        }

        private void CalculateChiPhi()
        {
            var parents = ItemsChiPhi.Where(x => x.IIdParentId.IsNullOrEmpty() || !ItemsChiPhi.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                item.IsModified = true;
                CalculateChiPhi(item);
            }
        }

        private void CalculateChiPhi(NhDaGoiThauChiPhiModel parentItem)
        {
            var childs = ItemsChiPhi.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateChiPhi(item);
                }
                parentItem.IsModified = true;
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        private void CaculateNguonVon()
        {
            if (!_itemsGoiThauNguonVon.IsEmpty())
            {
                foreach (var item in _itemsGoiThauNguonVon.Where(s => s.IsChecked))
                {
                    var dataSums = _itemsChiPhi.Where(s => s.IIdGoiThauNguonVonId == item.Id && !s.IIdParentId.HasValue);
                    if (dataSums.Any())
                    {
                        item.FGiaTriUsd = dataSums.Sum(s => s.FGiaTriUsd);
                        item.FGiaTriVnd = dataSums.Sum(s => s.FGiaTriVnd);
                        item.FGiaTriEur = dataSums.Sum(s => s.FGiaTriEur);
                        item.FGiaTriNgoaiTeKhac = dataSums.Sum(s => s.FGiaTriNgoaiTeKhac);
                        item.IsModified = true;
                    }
                }

                // Tính Giá trị hợp đồng = Tổng giá trị hợp đồng nguồn vốn
                OnPropertyChanged(nameof(ItemsGoiThauNguonVon));
                CaculatorTotalNguonVon();
            }
        }

        private List<NhDaGoiThauHangMucModel> LoadNhDaGoiThauHangMucs(NhDaGoiThauChiPhiModel chiPhiModel)
        {
            var result = new List<NhDaGoiThauHangMucModel>(); /*return result;*/
            if (chiPhiModel != null && chiPhiModel.IIdQdDauTuChiPhiId.HasValue)
            {
                if (IsAdded)
                {
                    // Thêm mới
                    var goiThauHangMucTemp = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == chiPhiModel.Id)?.GoiThauHangMucs?.ToList();

                    IEnumerable<NhDaQdDauTuHangMuc> hangMucs = _nhDaQdDauTuHangMucService.FindByQdDauTuChiPhiId(chiPhiModel.IIdQdDauTuChiPhiId.Value);
                    result = goiThauHangMucTemp ?? hangMucs?.Select(item => new NhDaGoiThauHangMucModel
                    {
                        Id = Guid.NewGuid(),
                        IIdGoiThauChiPhiId = chiPhiModel.Id,
                        IIdQdDauTuHangMucId = item.Id,
                        FTienGoiThauUsd = item.FGiaTriUsd,
                        FTienGoiThauVnd = item.FGiaTriVnd,
                        FTienGoiThauEur = item.FGiaTriEur,
                        FTienGoiThauNgoaiTeKhac = item.FGiaTriNgoaiTeKhac,
                        SMaHangMuc = item.SMaHangMuc,
                        STenHangMuc = item.STenHangMuc,
                        SMaOrder = item.SMaOrder,
                        IIdParentId = item.IIdParentId,
                        IsAdded = true
                    }).ToList();
                }
                else
                {
                    // Cập nhật hoặc điều chỉnh
                    IEnumerable<NhDaGoiThauHangMuc> data = _nhDaGoiThauHangMucSerrvice.FindAll(s => s.IIdGoiThauChiPhiId == chiPhiModel.Id);
                    var goiThauHangMucTemp = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == chiPhiModel.Id)?.GoiThauHangMucs?.ToList();

                    result = goiThauHangMucTemp ?? _mapper.Map<List<NhDaGoiThauHangMucModel>>(data);

                    result?.ForEach(s =>
                        {
                            // Map to giá trị sở cứ phê duyệt
                            if (s.IIdQdDauTuHangMucId.HasValue)
                            {
                                var qddtHangMuc = _nhDaQdDauTuHangMucService.FindById(s.IIdQdDauTuHangMucId.Value);
                                if (qddtHangMuc != null)
                                {
                                    s.FTienGoiThauUsd = qddtHangMuc.FGiaTriUsd;
                                    s.FTienGoiThauVnd = qddtHangMuc.FGiaTriVnd;
                                    s.FTienGoiThauEur = qddtHangMuc.FGiaTriEur;
                                    s.FTienGoiThauNgoaiTeKhac = qddtHangMuc.FGiaTriNgoaiTeKhac;
                                }
                            }
                        });
                }
            }
            return result;
        }
        private List<NhDaGoiThauHangMucModel> LoadNhDaGoiThauHangMucsByIdGoiThauChiPhi(
            NhDaGoiThauChiPhiModel chiPhiModel)
        {
            var result = new List<NhDaGoiThauHangMucModel>();
            // Cập nhật hoặc điều chỉnh
            IEnumerable<NhDaGoiThauHangMuc> data = _nhDaGoiThauHangMucSerrvice.FindAll(s => s.IIdGoiThauChiPhiId == chiPhiModel.Id);

            var goiThauHangMucTemp = Model?.GoiThauNguonVons?.FirstOrDefault(m => m.Id == SelectedGoiThauNguonVon.Id)?.GoiThauChiPhis?.FirstOrDefault(m => m.Id == chiPhiModel.Id)?.GoiThauHangMucs?.ToList();

            result = goiThauHangMucTemp ?? _mapper.Map<List<NhDaGoiThauHangMucModel>>(data);

            result?.ForEach(s =>
                {
                    // Map to giá trị sở cứ phê duyệt
                    if (s.IIdQdDauTuHangMucId.HasValue && s.IIdDuToanHangMucId != null)
                    {
                        var qddtHangMuc = _nhDaQdDauTuHangMucService.FindById(s.IIdQdDauTuHangMucId.Value);
                        if (qddtHangMuc != null)
                        {
                            s.FTienGoiThauUsd = qddtHangMuc.FGiaTriUsd;
                            s.FTienGoiThauVnd = qddtHangMuc.FGiaTriVnd;
                            s.FTienGoiThauEur = qddtHangMuc.FGiaTriEur;
                            s.FTienGoiThauNgoaiTeKhac = qddtHangMuc.FGiaTriNgoaiTeKhac;
                        }
                    }
                    else
                    {
                        s.FTienGoiThauUsd = null;
                        s.FTienGoiThauVnd = null;
                        s.FTienGoiThauEur = null;
                        s.FTienGoiThauNgoaiTeKhac = null;
                    }
                });
            return result;
        }

        private void LoadDanhMucChiPhi()
        {
            IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            _itemsDMChiPhi = _mapper.Map<ObservableCollection<NhDmChiPhiModel>>(data);

            OnPropertyChanged(nameof(ItemsDMChiPhi));
        }

        private void OnAddGoiThauHangMuc(object obj)
        {
            ItemsHangMuc ??= new ObservableCollection<NhDaGoiThauHangMucModel>();

            NhDaGoiThauHangMucModel sourceItem = SelectedHangMuc;
            NhDaGoiThauHangMucModel targetItem = new NhDaGoiThauHangMucModel();
            bool isParent = (bool)obj;
            int currentRow = -1;
            if (!ItemsHangMuc.IsEmpty())
            {
                if (sourceItem != null)
                {
                    currentRow = ItemsHangMuc.IndexOf(sourceItem) + CountTreeChildHangMucItems(sourceItem);
                }
                else
                {
                    // Thêm vào cuối danh sách
                    currentRow = ItemsHangMuc.Count() - 1;
                }
            }

            if (sourceItem != null)
            {
                targetItem.IIdParentId = isParent ? sourceItem.IIdParentId : sourceItem.Id;
                targetItem.IIdGoiThauChiPhiId = sourceItem.IIdGoiThauChiPhiId;
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            targetItem.IIdGoiThauChiPhiId = SelectedChiPhi.Id;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            ItemsHangMuc.Insert(currentRow + 1, targetItem);
            tempListHangMuc.Add(targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            OnPropertyChanged(nameof(ItemsHangMuc));
        }

        public void OnAddGoiThauHangMuc(
            NhDaGoiThauHangMucModel entity,
            bool isParent = true)
        {
            ItemsHangMuc ??= new ObservableCollection<NhDaGoiThauHangMucModel>();

            NhDaGoiThauHangMucModel targetItem = new NhDaGoiThauHangMucModel();
            int currentRow = -1;
            targetItem.Id = entity.Id;
            targetItem.IIdParentId = entity.IIdParentId;
            targetItem.IsAdded = true;
            targetItem.IIdQdDauTuHangMucId = entity.IIdQdDauTuHangMucId;
            targetItem.IsSuggestion = entity.IsSuggestion;
            targetItem.SMaHangMuc = entity.SMaHangMuc;
            targetItem.SMaOrder = entity.SMaOrder;
            targetItem.STenHangMuc = entity.STenHangMuc;
            targetItem.FTienGoiThauUsd = entity.FGiaTriUsd;
            targetItem.FTienGoiThauVnd = entity.FGiaTriVnd;
            targetItem.FTienGoiThauEur = entity.FGiaTriEur;
            targetItem.FTienGoiThauNgoaiTeKhac = entity.FGiaTriNgoaiTeKhac;
            targetItem.IIdGoiThauChiPhiId = SelectedChiPhi.Id;
            targetItem.PropertyChanged += HangMuc_PropertyChanged;
            ItemsHangMuc.Insert(currentRow + 1, targetItem);

            OrderItems(targetItem.IIdParentId);
            UpdateTreeItems();
            ItemsHangMuc.OrderBy(s => s.SMaHangMuc);
            ItemsHangMuc = new ObservableCollection<NhDaGoiThauHangMucModel>(ItemsHangMuc.OrderBy(h => h.SMaHangMuc));
        }

        private void OnDeleteGoiThauHangMuc()
        {
            if (SelectedHangMuc != null)
            {
                DeleteTreeItems(SelectedHangMuc, !SelectedHangMuc.IsDeleted);
            }
        }

        private int CountTreeChildHangMucItems(NhDaGoiThauHangMucModel currentItem)
        {
            int count = 0;
            var childs = ItemsHangMuc.Where(x => x.IIdParentId == currentItem.Id);
            if (!childs.IsEmpty())
            {
                count += childs.Count();
                foreach (var item in childs)
                {
                    count += CountTreeChildHangMucItems(item);
                }
            }
            return count;
        }

        private void DeleteTreeItems(NhDaGoiThauHangMucModel currentItem, bool status)
        {
            if (currentItem != null)
            {
                var items = ItemsHangMuc;
                currentItem.IsDeleted = status;
                var childs = items.Where(x => x.IIdParentId == currentItem.Id);
                if (!childs.IsEmpty())
                {
                    foreach (var item in childs)
                    {
                        DeleteTreeItems(item, status);
                    }
                }
            }
        }

        private void OrderItems(Guid? parentId = null)
        {
            var childs = ItemsHangMuc.Where(x => x.IIdParentId == parentId);
            if (!childs.IsEmpty())
            {
                var parent = ItemsHangMuc.FirstOrDefault(x => x.Id == parentId);
                int index = 1;
                foreach (var child in childs)
                {
                    if (parent != null)
                    {
                        child.SMaOrder = string.Format("{0}-{1}", parent.SMaOrder, index.ToString("D2"));
                    }
                    else
                    {
                        child.SMaOrder = index.ToString("D2");
                    }
                    child.SMaHangMuc = StringUtils.ConvertMaOrder(child.SMaOrder);
                    OrderItems(child.Id);
                    index++;
                }
            }
        }

        private void UpdateTreeItems()
        {
            if (!ItemsHangMuc.IsEmpty())
            {
                ItemsHangMuc.ForAll(s => s.CanEditValue = !ItemsHangMuc.Any(y => y.IIdParentId == s.Id));
                ItemsHangMuc.ForAll(x =>
                {
                    // Là hàng cha nếu thỏa mãn một trong các điều kiện sau
                    // 1. Có parent id là null hoặc ko nhận phần tử nào là cha
                    // 2. Có phần tử con. CanEditValue = false
                    // 3. Có phần tử cùng cấp là hàng cha
                    if (x.IIdParentId.IsNullOrEmpty() || !ItemsHangMuc.Any(y => y.Id == x.IIdParentId)) x.IsHangCha = true;
                    if (!x.CanEditValue) x.IsHangCha = true;
                    else if (ItemsHangMuc.Any(y => y.IIdParentId == x.IIdParentId && !y.CanEditValue)) x.IsHangCha = true;
                });
            }
        }

        private void HangMuc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaGoiThauHangMucModel objectSender = (NhDaGoiThauHangMucModel)sender;
            if (e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriUsd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriEur)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriVnd)) ||
                e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateHangMuc();
                CalculatorTotalHangMuc();
                foreach (var item in tempListHangMuc)
                {
                    if (item.Id == objectSender.Id)
                    {
                        item.FGiaTriUsd = objectSender.FGiaTriUsd;
                        item.FGiaTriEur = objectSender.FGiaTriEur;
                        item.FGiaTriVnd = objectSender.FGiaTriVnd;
                        item.FGiaTriNgoaiTeKhac = objectSender.FGiaTriNgoaiTeKhac;
                    }
                }
            }
            if (!e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.IsHangCha)) &&
                !e.PropertyName.Equals(nameof(NhDaGoiThauHangMucModel.CanEditValue)))
            {
                objectSender.IsModified = true;
            }
            OnPropertyChanged(nameof(HasChanged));
        }

        private void CalculateHangMuc()
        {
            var parents = ItemsHangMuc.Where(x => x.IIdParentId.IsNullOrEmpty() || !ItemsHangMuc.Any(y => y.Id == x.IIdParentId));
            foreach (var item in parents)
            {
                CalculateHangMuc(item);
            }
        }

        private void CalculateHangMuc(NhDaGoiThauHangMucModel parentItem)
        {
            var childs = ItemsHangMuc.Where(x => x.IIdParentId == parentItem.Id && !x.IsDeleted);
            if (!childs.IsEmpty())
            {
                foreach (var item in childs)
                {
                    CalculateHangMuc(item);
                }
                parentItem.FGiaTriUsd = childs.Sum(x => x.FGiaTriUsd);
                parentItem.FGiaTriEur = childs.Sum(x => x.FGiaTriEur);
                parentItem.FGiaTriVnd = childs.Sum(x => x.FGiaTriVnd);
                parentItem.FGiaTriNgoaiTeKhac = childs.Sum(x => x.FGiaTriNgoaiTeKhac);
            }
        }

        public void LoadDataHangMuc()
        {
            //if (IsAdded)
            //{
            //    //Add hạng mục vào list chung
            //    if (SLoaiSoCu == SO_CU_TRUC_TIEP.THONG_TIN_DU_AN)
            //    {
            //        IEnumerable<NhDaDuAnHangMuc> duAnHangMucs = _nhDaDuAnHangMucService.FindByDuAnId(Model.IIdDuAnId ?? Guid.Empty);
            //        duAnHangMucs.ForAll(s =>
            //        {
            //            if (tempListHangMuc == null || (tempListHangMuc != null && !tempListHangMuc.Any(x => x.IIdQdDauTuHangMucId == s.Id && x.IIdGoiThauChiPhiId == SelectedChiPhi.Id)))
            //            {
            //                tempListHangMuc.Add(new NhDaGoiThauHangMucModel
            //                {
            //                    Id = Guid.NewGuid(),
            //                    IIdGoiThauChiPhiId = SelectedChiPhi.Id,
            //                    IIdParentId = s.IIdParentId,
            //                    SMaOrder = s.SMaOrder,
            //                    SMaHangMuc = s.SMaHangMuc,
            //                    STenHangMuc = s.STenHangMuc,
            //                    IIdQdDauTuHangMucId = s.Id,
            //                });
            //            }
            //        });
            //    }
            //    else if (SLoaiSoCu == SO_CU_TRUC_TIEP.CHU_CHUONG_DAU_TU)
            //    {
            //        NhDaChuTruongDauTu chuTruongDauTu = _nhDaChuTruongDauTuService.FindByDuAnId(Model.IIdDuAnId ?? Guid.Empty);
            //        if (chuTruongDauTu != null)
            //        {
            //            IEnumerable<NhDaChuTruongDauTuHangMuc> duAnHangMucs = _nhDaChuTruongDauTuHangMucService.FindByChuTruongDauTuId(chuTruongDauTu.Id);
            //            duAnHangMucs.ForAll(s =>
            //            {
            //                if (tempListHangMuc == null || (tempListHangMuc != null && !tempListHangMuc.Any(x => x.IIdQdDauTuHangMucId == s.Id && x.IIdGoiThauChiPhiId == SelectedChiPhi.Id)))
            //                {
            //                    tempListHangMuc.Add(new NhDaGoiThauHangMucModel
            //                    {
            //                        Id = Guid.NewGuid(),
            //                        IIdGoiThauChiPhiId = SelectedChiPhi.Id,
            //                        IIdParentId = s.IIdParentId,
            //                        SMaOrder = s.SMaOrder,
            //                        SMaHangMuc = s.SMaHangMuc,
            //                        STenHangMuc = s.STenHangMuc,
            //                        IIdQdDauTuHangMucId = s.Id,
            //                    });
            //                }
            //            });
            //        }
            //    }
            //}

            //Thay Id parent mới
            if (tempListHangMuc != null)
            {
                foreach (var item in tempListHangMuc)
                {
                    if (item.IIdParentId != null)
                    {
                        //NhDaGoiThauHangMucModel temp = tempListHangMuc.Where(x => x.IIdQdDauTuHangMucId == item.IIdParentId && x.IIdGoiThauChiPhiId == SelectedChiPhi.Id).FirstOrDefault();
                        NhDaGoiThauHangMucModel temp = tempListHangMuc.FirstOrDefault(x => x.IIdQdDauTuHangMucId.Equals(item.IIdParentId) && x.IIdGoiThauChiPhiId.Equals(SelectedChiPhi.Id));
                        if (temp != null)
                        {
                            item.IIdParentId = temp.Id;
                        }
                    }
                }
            }

            //List<NhDaGoiThauHangMucModel> lstHangMucModel = new List<NhDaGoiThauHangMucModel>();
            //foreach (var item in Model.GoiThauNguonVons)
            //{
            //    foreach (var itemChiPhi in item.GoiThauChiPhis)
            //    {
            //        if (itemChiPhi.Id == SelectedChiPhi.Id)
            //        {
            //            foreach (var itemHangMuc in itemChiPhi.GoiThauHangMucs)
            //            {
            //                lstHangMucModel.Add(itemHangMuc);
            //            }
            //        }
            //    }
            //}

            //if (!lstHangMucModel.IsEmpty())
            //{
            //    ItemsHangMuc = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(lstHangMucModel);
            //}
            //else
            //{
            //    var listHangMuc = tempListHangMuc.Where(x => x.IIdGoiThauChiPhiId == SelectedChiPhi.Id);
            //    ItemsHangMuc = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(listHangMuc);
            //}

            if (!tempListHangMuc.IsEmpty())
            {
                var listHangMuc = tempListHangMuc.Where(x => x.IIdGoiThauChiPhiId == SelectedChiPhi.Id);
                ItemsHangMuc = _mapper.Map<ObservableCollection<NhDaGoiThauHangMucModel>>(listHangMuc);
            }

            foreach (var item in ItemsHangMuc)
            {
                // Map to giá trị sở cứ phê duyệt
                if (SLoaiSoCu == SO_CU_TRUC_TIEP.QUYET_DINH_DAU_TU && item.IIdQdDauTuHangMucId != null)
                {
                    var qdDauTuHangMuc = _nhDaQdDauTuHangMucService.FindById(item.IIdQdDauTuHangMucId.Value);
                    if (qdDauTuHangMuc != null)
                    {
                        item.FTienGoiThauUsd = qdDauTuHangMuc.FGiaTriUsd;
                        item.FTienGoiThauVnd = qdDauTuHangMuc.FGiaTriVnd;
                        item.FTienGoiThauEur = qdDauTuHangMuc.FGiaTriEur;
                        item.FTienGoiThauNgoaiTeKhac = qdDauTuHangMuc.FGiaTriNgoaiTeKhac;
                    }

                    var tempHangMuc = tempListHangMuc.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (tempHangMuc != null)
                    {
                        item.FGiaTriUsd = tempHangMuc.FGiaTriUsd;
                        item.FGiaTriVnd = tempHangMuc.FGiaTriVnd;
                        item.FGiaTriEur = tempHangMuc.FGiaTriEur;
                        item.FGiaTriNgoaiTeKhac = tempHangMuc.FGiaTriNgoaiTeKhac;
                    }
                }
            }

            foreach (var item in ItemsHangMuc)
            {
                item.PropertyChanged += HangMuc_PropertyChanged;
            }
            UpdateTreeItems();
            _originItems = ObjectCopier.Clone(ItemsHangMuc);
            _originItems = new ObservableCollection<NhDaGoiThauHangMucModel>(_originItems.OrderBy(i => i.SMaOrder));
        }

        public void HangMuc_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedHangMuc = (NhDaGoiThauHangMucModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaGoiThauHangMucModel.FGiaTriNgoaiTeKhac)))
            {
                e.Cancel = !SelectedHangMuc.CanEditValue;
            }
        }

        private void CalculatorTotalHangMuc()
        {
            var sumUsd = ItemsHangMuc.Where(x => x.IIdGoiThauChiPhiId == SelectedChiPhi.Id && x.IIdParentId == null).Sum(x => x.FGiaTriUsd);
            var sumVnd = ItemsHangMuc.Where(x => x.IIdGoiThauChiPhiId == SelectedChiPhi.Id && x.IIdParentId == null).Sum(x => x.FGiaTriVnd);

            SelectedChiPhi.FGiaTriUsd = sumUsd;
            SelectedChiPhi.FGiaTriVnd = sumVnd;
            CalculateChiPhi();
            CaculatorTotalChiPhi();
            CaculateNguonVon();
            CaculatorTotalNguonVon();
        }
    }
}
