using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.DeNghiQTDAHT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.DeNghiQTDAHT
{
    public class DeNghiQTDAHTDialogViewModel : DialogAttachmentViewModelBase<NhQtQuyetToanDahtModel>
    {
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INhDaQdDauTuService _nhDaQdDauTuService;
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDaQdDauTuNguonVonService _nhDaQdDauTuNguonVonService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhQtQuyetToanDahtService _nhQtQuyetToanDahtService;
        private SessionInfo _sessionInfo;

        public override Type ContentType => typeof(DeNghiQTDAHTDialog);
        public override string Title => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Name => "Quản lý đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Danh sách đề nghị quyết toán dự án hoàn thành";
        public bool IsDetail { get; set; }
        public bool IsAdd { get; set; }

        public DeNghiQTDAHTDialogDetailViewModel DeNghiQTDAHTDialogDetailViewModel { get; }

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

        private ObservableCollection<NhDaDuAn> _itemsDuAn;
        public ObservableCollection<NhDaDuAn> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAn _selectedDuAn;
        public NhDaDuAn SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value) && value != null)
                {
                    LoadChuDauTu();
                    LoadQDDT();
                    LoadNguonVon();
                }
            }
        }

        private ObservableCollection<ChiPhiTSTDauTu> _listCP1;
        public ObservableCollection<ChiPhiTSTDauTu> ListCP1
        {
            get => _listCP1;
            set => SetProperty(ref _listCP1, value);
        }

        private ObservableCollection<ChiPhiTSTDauTu> _listCP2;
        public ObservableCollection<ChiPhiTSTDauTu> ListCP2
        {
            get => _listCP2;
            set => SetProperty(ref _listCP2, value);
        }

        private ChiPhiTSTDauTu _selectedChiPhiTSTDauTu;
        public ChiPhiTSTDauTu SelectedChiPhiTSTDauTu
        {
            get => _selectedChiPhiTSTDauTu;
            set => SetProperty(ref _selectedChiPhiTSTDauTu, value);
        }

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set
            {
                if (SetProperty(ref _selectedChuDauTu, value))
                {

                }
            }
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

        private ObservableCollection<NHDAQDDauTuNguonVonQueryModel> _nHDAQDDauTuNguonVonItems;
        public ObservableCollection<NHDAQDDauTuNguonVonQueryModel> NHDAQDDauTuNguonVonItems
        {
            get => _nHDAQDDauTuNguonVonItems;
            set => SetProperty(ref _nHDAQDDauTuNguonVonItems, value);
        }

        private ObservableCollection<NhDaQdDauTuModel> _itemsNhDaQdDauTu;
        public ObservableCollection<NhDaQdDauTuModel> ItemsNhDaQdDauTu
        {
            get => _itemsNhDaQdDauTu;
            set => SetProperty(ref _itemsNhDaQdDauTu, value);
        }
        private NhDaQdDauTuModel _nhDaQdDauTu;
        public NhDaQdDauTuModel NhDaQdDauTu
        {
            get => _nhDaQdDauTu;
            set => SetProperty(ref _nhDaQdDauTu, value);
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

        public DeNghiQTDAHTDialogViewModel(
            IMapper mapper,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            INhDaDuAnService nhDaDuAnService,
            IAttachmentService attachService,
            IDmChuDauTuService dmChuDauTuService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDaQdDauTuNguonVonService nhDaQdDauTuNguonVonService,
            INhDaQdDauTuService nhDaQdDauTuService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhQtQuyetToanDahtService nhQtQuyetToanDahtService,
            DeNghiQTDAHTDialogDetailViewModel deNghiQTDAHTDialogDetailViewModel)
            : base(mapper, storageServiceFactory, attachService)
        {
            _nsDonViService = nsDonViService;
            _sessionService = sessionService;
            _nhDaDuAnService = nhDaDuAnService;
            _dmChuDauTuService = dmChuDauTuService;
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDaQdDauTuNguonVonService = nhDaQdDauTuNguonVonService;
            _nhDaQdDauTuService = nhDaQdDauTuService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            DeNghiQTDAHTDialogDetailViewModel = deNghiQTDAHTDialogDetailViewModel;
            _nhQtQuyetToanDahtService = nhQtQuyetToanDahtService;
        }

        public override void Init()
        {
            LoadDefault();
            base.Init();
            SelectedDuAn = null;
            LoadDonVi();
            LoadDuAn();
            LoadDmTyGia();
            LoadListCPTS();
            LoadData();
        }
        private void LoadDefault()
        {
            _sessionInfo = _sessionService.Current;
        }
        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                IconKind = PackIconKind.PlaylistPlus;
                Description = "Thêm mới đề nghị quyết toán dự án hoàn thành";
                Model.DNgayDeNghi = DateTime.Now;
                _selectedChuDauTu = null;
                _selectedDonVi = null;
                _selectedTiGia = null;
                _selectedDuAn = null;
                _selectedTiGiaChiTiet = null;
                NhDaQdDauTu = null;
                NHDAQDDauTuNguonVonItems = null;
                //LoadQDDT();
                // OnPropertyChanged(nameof(NhDaQdDauTu));
            }
            else
            {
                NhQtQuyetToanDaht entity = _nhQtQuyetToanDahtService.FindById(Model.Id);
                Model = _mapper.Map<NhQtQuyetToanDahtModel>(entity);
                if (IsDetail)
                {
                    IconKind = PackIconKind.Details;
                    Description = "Chi tiết đề nghị quyết toán dự án hoàn thành";
                }
                else
                {
                    IconKind = PackIconKind.NoteEditOutline;
                    Description = "Cập nhật đề nghị quyết toán dự án hoàn thành";
                }
                _selectedChuDauTu = _itemsChuDauTu.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonVi));
                _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
                _selectedTiGia = _itemsTiGia.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
                _selectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
                LoadChuDauTu();
                LoadDonVi();
            }
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedChuDauTu));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedTiGia));
            OnPropertyChanged(nameof(SelectedTiGiaChiTiet));
        }

        private void LoadDonVi()
        {
            var data = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
            ItemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            if (!Model.Id.Equals(Guid.Empty))
            {
                SelectedDonVi = ItemsDonVi.FirstOrDefault(t => t.IIDMaDonVi.Equals(Model.IIdMaDonVi));
            }
        }

        private void LoadDuAn()
        {
            ItemsDuAn = new ObservableCollection<NhDaDuAn>(_nhDaDuAnService.FindAllDuAnByQDDT());
            if (!Model.Id.Equals(Guid.Empty))
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault(t => t.Id.Equals(Model.IIdDuAnId));
            }
            else if (ItemsDuAn.Count > 0)
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault();
            }
        }

        private void LoadChuDauTu()
        {
            var cdt = new ObservableCollection<DmChuDauTu>(_dmChuDauTuService.FindByDuAnId(SelectedDuAn.Id));
            ItemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(cdt);
            if (ItemsChuDauTu.Count > 0)
            {
                SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault();
            }
        }

        private void LoadQDDT()
        {
            //var qdDauTu = _nhDaQdDauTuService.FindByDuAnId(SelectedDuAn.Id);
            //NhDaQdDauTu = _mapper.Map<NhDaQdDauTuModel>(qdDauTu);
            //if(Model.Id.IsNullOrEmpty())
            //{
            //    _selectedDuAn = new NhDaDuAn();
            //    NhDaQdDauTu = _mapper.Map<NhDaQdDauTuModel>(_nhDaQdDauTuService.FindByDuAnId(SelectedDuAn.Id));
            //}    
            var qdDauTu = new ObservableCollection<NhDaQdDauTu>(_nhDaQdDauTuService.FindListByDuAnId(SelectedDuAn.Id));
            ItemsNhDaQdDauTu = _mapper.Map<ObservableCollection<NhDaQdDauTuModel>>(qdDauTu);
            if (ItemsNhDaQdDauTu.Count > 0)
            {
                NhDaQdDauTu = ItemsNhDaQdDauTu.FirstOrDefault();
            }
        }

        private void LoadDmTyGia()
        {
            var tiGia = new ObservableCollection<NhDmTiGia>(_nhDmTiGiaService.FindAll().OrderByDescending(t => t.DNgayTao));
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(tiGia);
            if (!Model.Id.Equals(Guid.Empty))
            {
                SelectedTiGia = ItemsTiGia.FirstOrDefault(t => t.Id.Equals(Model.IIdTiGiaId));
            }
            else if (ItemsTiGia.Count > 0)
            {
                SelectedTiGia = ItemsTiGia.FirstOrDefault();
            }
        }

        private void LoadNguonVon()
        {
            var data = new ObservableCollection<NHDAQDDauTuNguonVonQuery>(_nhDaQdDauTuNguonVonService.FindByDuAnId(SelectedDuAn.Id));
            NHDAQDDauTuNguonVonItems = _mapper.Map<ObservableCollection<NHDAQDDauTuNguonVonQueryModel>>(data);
        }

        private void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
                if (!Model.Id.Equals(Guid.Empty))
                {
                    SelectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault(t => t.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
                }
                else if (_itemsTiGiaChiTiet.Count > 0)
                {
                    SelectedTiGiaChiTiet = _itemsTiGiaChiTiet.FirstOrDefault();
                }
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        private void LoadListCPTS()
        {
            if (Model.Id == null || Model.Id.Equals(Guid.Empty))
            {
                ListCP1 = new ObservableCollection<ChiPhiTSTDauTu>();
                ListCP1.Add(new ChiPhiTSTDauTu { TenCP = "Chi phí thiệt hại do các nguyên nhân bất khả kháng" });
                ListCP1.Add(new ChiPhiTSTDauTu { TenCP = "Chi phí không tạo nên tài sản" });
                ListCP2 = new ObservableCollection<ChiPhiTSTDauTu>();
                ListCP2.Add(new ChiPhiTSTDauTu { TenCP = "Tài sản dài hạn (tài sản cố định)" });
                ListCP2.Add(new ChiPhiTSTDauTu { TenCP = "Tài sản ngắn hạn" });
                SelectedChiPhiTSTDauTu = _listCP1.FirstOrDefault();
            }
            else
            {
                ListCP1 = new ObservableCollection<ChiPhiTSTDauTu>();
                ListCP2 = new ObservableCollection<ChiPhiTSTDauTu>();
                ListCP1.Add(new ChiPhiTSTDauTu
                {
                    TenCP = "Chi phí thiệt hại do các nguyên nhân bất khả kháng",
                    USD = Model.FCpthietHaiUsd,
                    EURO = Model.FCpthietHaiEur,
                    VND = Model.FCpthietHaiVnd,
                    NgoaiTeKhac = Model.FCpthietHaiEur
                });
                ListCP1.Add(new ChiPhiTSTDauTu
                {
                    TenCP = "Chi phí không tạo nên tài sản",
                    USD = Model.FCpkhongTaoTaiSanUsd,
                    EURO = Model.FCpkhongTaoTaiSanEur,
                    VND = Model.FCpkhongTaoTaiSanVnd,
                    NgoaiTeKhac = Model.FCpkhongTaoTaiSanNgoaiTeKhac
                });

                ListCP2.Add(new ChiPhiTSTDauTu
                {
                    TenCP = "Tài sản dài hạn (tài sản cố định)",
                    USD = Model.FTaiSanDaiHanUsd,
                    EURO = Model.FTaiSanDaiHanEur,
                    VND = Model.FTaiSanDaiHanVnd,
                    NgoaiTeKhac = Model.FTaiSanDaiHanNgoaiTeKhac
                });
                ListCP2.Add(new ChiPhiTSTDauTu
                {
                    TenCP = "Tài sản ngắn hạn",
                    USD = Model.FTaiSanNganHanUsd,
                    EURO = Model.FTaiSanNganHanEur,
                    VND = Model.FTaiSanNganHanVnd,
                    NgoaiTeKhac = Model.FTaiSanNganHanNgoaiTeKhac
                });
                SelectedChiPhiTSTDauTu = _listCP1.FirstOrDefault();
            }
            foreach (var item in _listCP1)
            {
                item.PropertyChanged += ChiPhi_PropertyChanged;
            }
            foreach (var item in _listCP2)
            {
                item.PropertyChanged += ChiPhi_PropertyChanged;
            }
            OnPropertyChanged(nameof(ListCP1));
            OnPropertyChanged(nameof(ListCP2));
            OnPropertyChanged(nameof(SelectedChiPhiTSTDauTu));
        }

        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var obj = (ChiPhiTSTDauTu)sender;
            if(SelectedTiGia != null && SelectedTiGiaChiTiet != null)
            {
                obj.PropertyChanged -= ChiPhi_PropertyChanged;
                var listTiGiaChiTiet = _mapper.Map<IEnumerable<NhDmTiGiaChiTiet>>(ItemsTiGiaChiTiet);
                string rootCurrency = "";
                rootCurrency = SelectedTiGia.SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                double value;
                switch (args.PropertyName)
                {
                    case nameof(ChiPhiTSTDauTu.VND):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = obj.VND.GetValueOrDefault();
                        break;
                    case nameof(ChiPhiTSTDauTu.EURO):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                        value = obj.EURO.GetValueOrDefault();
                        break;
                    case nameof(ChiPhiTSTDauTu.NgoaiTeKhac):
                        sourceCurrency = otherCurrency;
                        value = obj.NgoaiTeKhac.GetValueOrDefault();
                        break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = obj.USD.GetValueOrDefault();
                        break;
                }
                obj.VND = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                obj.EURO = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                obj.USD = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                obj.NgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                obj.PropertyChanged += ChiPhi_PropertyChanged;
            }
        }
        private bool ValidateData()
        {
            List<string> lstError = new List<string>();
            if (SelectedDonVi == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckDonVi));
            }
            if (SelectedDuAn == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckDuAn));
            }
            if (SelectedTiGia == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckTiGiaNgoaiHoi));
            }
            if (Model.SSoDeNghi == null)
            {
                lstError.Add(string.Format(Resources.MsgCheckSoDeNghi));
            }
            if (Model.DNgayDeNghi > DateTime.Now)
            {
                lstError.Add(string.Format(Resources.MsgCheckNgayDeNghiLonHon));
            }
            if (!string.IsNullOrEmpty(Model.SSoDeNghi) && _nhQtQuyetToanDahtService.FindAllNhQtDaht().Select(n => n.SSoDeNghi).Contains(Model.SSoDeNghi) && IsAdd)
            {
                lstError.Add(string.Format(Resources.MsgCheckSoDeNghiTonTai));
            }
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Warning(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        public override void OnSave(object obj)
        {
            if (!ValidateData()) return;
            bool isAdd = false;
            if (Model.Id.Equals(Guid.Empty))
            {
                isAdd = true;
                Model.Id = Guid.NewGuid();
                Model.BIsActive = true;
            }
            if (SelectedDonVi != null)
            {
                Model.IIdMaDonVi = SelectedDonVi.IIDMaDonVi;
                Model.IIdDonViId = SelectedDonVi.Id;
            }
            if (SelectedTiGia != null)
            {
                Model.IIdTiGiaId = SelectedTiGia.Id;
            }
            if (SelectedDuAn != null)
            {
                Model.IIdDuAnId = SelectedDuAn.Id;
            }
            if (SelectedTiGiaChiTiet != null)
            {
                Model.SMaNgoaiTeKhac = SelectedTiGiaChiTiet.SMaTienTeQuyDoi;
            }

            Model.FCpthietHaiEur = ListCP1.ElementAt(0)?.EURO;
            Model.FCpthietHaiUsd = ListCP1.ElementAt(0)?.USD;
            Model.FCpthietHaiVnd = ListCP1.ElementAt(0)?.VND;
            Model.FCpthietHaiNgoaiTeKhac = ListCP1.ElementAt(0)?.NgoaiTeKhac;

            Model.FCpkhongTaoTaiSanEur = ListCP1.ElementAt(1)?.EURO;
            Model.FCpkhongTaoTaiSanUsd = ListCP1.ElementAt(1)?.USD;
            Model.FCpkhongTaoTaiSanVnd = ListCP1.ElementAt(1)?.VND;
            Model.FCpkhongTaoTaiSanNgoaiTeKhac = ListCP1.ElementAt(1)?.NgoaiTeKhac;

            Model.FTaiSanDaiHanEur = ListCP2.ElementAt(0)?.EURO;
            Model.FTaiSanDaiHanUsd = ListCP2.ElementAt(0)?.USD;
            Model.FTaiSanDaiHanVnd = ListCP2.ElementAt(0)?.VND;
            Model.FTaiSanDaiHanNgoaiTeKhac = ListCP2.ElementAt(0)?.NgoaiTeKhac;

            Model.FTaiSanNganHanEur = ListCP2.ElementAt(1)?.EURO;
            Model.FTaiSanNganHanUsd = ListCP2.ElementAt(1)?.USD;
            Model.FTaiSanNganHanVnd = ListCP2.ElementAt(1)?.VND;
            Model.FTaiSanNganHanNgoaiTeKhac = ListCP2.ElementAt(0)?.NgoaiTeKhac;
            if (!ValidateViewModelHelper.Validate(Model)) return;

            var entity = _mapper.Map<NhQtQuyetToanDaht>(Model);
            _nhQtQuyetToanDahtService.Save(entity);
            MessageBoxHelper.Info("Lưu dữ liệu thành công");
            try
            {
                DeNghiQTDAHTDialogDetailViewModel.NhQtQuyetToanDahtModel = Model;
                DeNghiQTDAHTDialogDetailViewModel.IsAdd = isAdd;
                DeNghiQTDAHTDialogDetailViewModel.ItemsTiGiaChiTiet = ItemsTiGiaChiTiet;
                DeNghiQTDAHTDialogDetailViewModel.SelectedTiGia = SelectedTiGia;
                DeNghiQTDAHTDialogDetailViewModel.SelectedTiGiaChiTiet = SelectedTiGiaChiTiet;
                DeNghiQTDAHTDialogDetailViewModel.SelectedDonVi = SelectedDonVi;
                DeNghiQTDAHTDialogDetailViewModel.Init();
                DeNghiQTDAHTDialogDetailViewModel.SavedAction = obj =>
                {

                };
                DeNghiQTDAHTDialogDetailViewModel.ShowDialog();
                var view = obj as Window;
                if (view != null) view.Close();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message, ex);
            }
        }
    }

    public class ChiPhiTSTDauTu : BindableBase
    {
        public string TenCP { get; set; }

        private double? _usd;
        public double? USD
        {
            get => _usd;
            set => SetProperty(ref _usd, value);
        }
        private double? _vnd;
        public double? VND
        {
            get => _vnd;
            set => SetProperty(ref _vnd, value);
        }

        private double? _euro;
        public double? EURO
        {
            get => _euro;
            set => SetProperty(ref _euro, value);
        }

        private double? _ngoaiTeKhac;
        public double? NgoaiTeKhac
        {
            get => _ngoaiTeKhac;
            set => SetProperty(ref _ngoaiTeKhac, value);
        }
    }
}
