
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using log4net;
using VTS.QLNS.CTC.App.Helper;
using AutoMapper;
using System.Windows;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo
{
    public class ForexContractInfoDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhDaHopDongModel>
    {
        #region Private
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhKhTongTheService _nhKhTongTheService;
        private readonly INhKhTongTheNhiemVuChiService _nhKhTongTheNhiemVuChiService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhHdnkCacQuyetDinhService _nhHdnkCacQuyetDinhService;
        private readonly INhDaHopDongHangMucService _nhDaHopDongHangMucService;
        private readonly INhDaHopDongChiPhiService _nhDaHopDongChiPhiService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly INhDaGoiThauService _nhDaGoiThauService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDaGoiThauNguonVonService _nhDaGoiThauNguonVonService;
        private readonly INhDaGoiThauChiPhiService _nhDaGoiThauChiPhiService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhHdnkPhuongAnNhapKhauService _phuongAnNhapKhauService;
        private readonly ILog _logger;
        private readonly INhDmChiPhiService _nhDmChiPhiService;
        private SessionInfo _sessionInfo;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        #endregion

        public override string Name => "THÔNG TIN HỢP ĐỒNG";
        public override string Title => "THÔNG TIN HỢP ĐỒNG";
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTForexContractInfo.ForexContractInfoDialog);
        public bool TypeView => !(ILoai == 1 && IThuocMenu == 1);
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit => !(IsInsert || IsDieuChinh);
        public bool IsReadOnly { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public bool IsHieuChinhImport { get; set; }

        private ObservableCollection<NhKhTongTheModel> _itemsKeHoachTongThe;
        public ObservableCollection<NhKhTongTheModel> ItemsKeHoachTongThe
        {
            get => _itemsKeHoachTongThe;
            set => SetProperty(ref _itemsKeHoachTongThe, value);
        }

        private ObservableCollection<NhDmChiPhiModel> _itemsDMChiPhi;
        public ObservableCollection<NhDmChiPhiModel> ItemsDMChiPhi
        {
            get => _itemsDMChiPhi;
            set => SetProperty(ref _itemsDMChiPhi, value);
        }

        private NhKhTongTheModel _selectedKeHoachTongThe;
        public NhKhTongTheModel SelectedKeHoachTongThe
        {
            get => _selectedKeHoachTongThe;
            set
            {
                if (SetProperty(ref _selectedKeHoachTongThe, value))
                {
                    LoadDonVi();
                    LoadChuongTrinh();
                }
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
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                {
                    LoadChuongTrinh();
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<NhKhTongTheNhiemVuChiModel> _itemsChuongTrinh;
        public ObservableCollection<NhKhTongTheNhiemVuChiModel> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private NhKhTongTheNhiemVuChiModel _selectedChuongTrinh;
        public NhKhTongTheNhiemVuChiModel SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set
            {
                if (SetProperty(ref _selectedChuongTrinh, value))
                {
                    LoadDuAn();
                    LoadDsGoiThau();
                }
            }
        }

        private ObservableCollection<NhDaDuAnModel> _itemsDuAn;
        public ObservableCollection<NhDaDuAnModel> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private NhDaDuAnModel _selectedDuAn;
        public NhDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                if (SetProperty(ref _selectedDuAn, value))
                {
                }
            }
        }

        private ObservableCollection<NhDaGoiThauModel> _itemsGoiThau;
        public ObservableCollection<NhDaGoiThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }

        private NhDaGoiThauModel _selectedGoiThau;
        public NhDaGoiThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set
            {
                if (SetProperty(ref _selectedGoiThau, value))
                {
                    LoadSoQDAndSoPhuongAnNK();
                }
            }
        }

        private ObservableCollection<NhDaHopDongHangMucModel> _itemsHopDongHangMuc;
        public ObservableCollection<NhDaHopDongHangMucModel> ItemsHopDongHangMuc
        {
            get => _itemsHopDongHangMuc;
            set => SetProperty(ref _itemsHopDongHangMuc, value);
        }

        private ObservableCollection<NhDaHopDongHangMucModel> _originItems;
        public bool HasChanged => !ObjectCopier.ToJsonString(ItemsHopDongHangMuc).Equals(ObjectCopier.ToJsonString(_originItems));

        private NhDaHopDongHangMucModel _selectedHopDongHangMuc;
        public NhDaHopDongHangMucModel SelectedHopDongHangMuc
        {
            get => _selectedHopDongHangMuc;
            set => SetProperty(ref _selectedHopDongHangMuc, value);
        }

        private string _sDonViUyThac;
        public string SDonViUyThac
        {
            get => _sDonViUyThac;
            set => SetProperty(ref _sDonViUyThac, value);
        }

        private string _sPhuongAnNhapKhau;
        public string SPhuongAnNhapKhau
        {
            get => _sPhuongAnNhapKhau;
            set => SetProperty(ref _sPhuongAnNhapKhau, value);
        }

        private string _sQuyetDinhChiTiet;
        public string SQuyetDinhChiTiet
        {
            get => _sQuyetDinhChiTiet;
            set => SetProperty(ref _sQuyetDinhChiTiet, value);
        }

        private ObservableCollection<ComboboxItem> _itemsContractType;
        public ObservableCollection<ComboboxItem> ItemsContractType
        {
            get => _itemsContractType;
            set => SetProperty(ref _itemsContractType, value);
        }

        private ComboboxItem _selectedContractType;
        public ComboboxItem SelectedContractType
        {
            get => _selectedContractType;
            set => SetProperty(ref _selectedContractType, value);
        }


        public ForexContractInfoItemsViewModel ForexContractInfoItemsViewModel { get; set; }
        public bool IsDieuChinhImport { get; private set; }
        public bool IsDetail { get; internal set; }
        public bool IsChecked { get; private set; }

        private ObservableCollection<NhDaHopDongChiPhiModel> _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();
        public ObservableCollection<NhDaHopDongChiPhiModel> ItemsChiPhi
        {
            get => _itemsChiPhi;
            set => SetProperty(ref _itemsChiPhi, value);
        }

        private ObservableCollection<NhDaHopDongChiPhiModel> _itemsChiPhiTemp = new ObservableCollection<NhDaHopDongChiPhiModel>();
        public ObservableCollection<NhDaHopDongChiPhiModel> ItemsChiPhiTemp
        {
            get => _itemsChiPhiTemp;
            set => SetProperty(ref _itemsChiPhiTemp, value);
        }

        private NhDaHopDongChiPhiModel _selectedChiPhi;
        public NhDaHopDongChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private NhDaHopDongChiPhiModel _tongTienChiPhi;
        public NhDaHopDongChiPhiModel TongTienChiPhi
        {
            get => _tongTienChiPhi;
            set => SetProperty(ref _tongTienChiPhi, value);
        }

        public bool IsAdded { get; set; }
        public bool IsEnableSoCuChuongTrinh { get; set; }
        public string SLoaiSoCu { get; set; }
        //chi phí

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGiaDialog;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGiaDialog
        {
            get => _itemsTiGiaDialog;
            set => SetProperty(ref _itemsTiGiaDialog, value);
        }

        private NhDmTiGiaModel _selectedTiGiaDialog;
        public NhDmTiGiaModel SelectedTiGiaDialog
        {
            get => _selectedTiGiaDialog;
            set
            {
                if (SetProperty(ref _selectedTiGiaDialog, value))
                {
                    IsVisibleTiGiaNhap = true;
                    ShowTiGiaNhap();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTietDialog;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTietDialog
        {
            get => _itemsTiGiaChiTietDialog;
            set => SetProperty(ref _itemsTiGiaChiTietDialog, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTietDialog;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTietDialog
        {
            get => _selectedTiGiaChiTietDialog;
            set
            {
                if (SetProperty(ref _selectedTiGiaChiTietDialog, value))
                {
                    IsVisibleTiGiaNhap = false;
                    LoadTiGia(1);
                }
            }
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set
            {
                if (SetProperty(ref _fTiGiaNhap, value))
                {
                    if (ItemsChiPhi != null)
                    {
                        CalculateModel();
                        CalculateChiPhi();
                        CaculatorTotalChiPhi();
                    }
                }
            }
        }

        private bool _isVisibleTiGiaNhap;
        public bool IsVisibleTiGiaNhap
        {
            get => _isVisibleTiGiaNhap;
            set => SetProperty(ref _isVisibleTiGiaNhap, value);
        }

        public ForexContractInfoItemDialogViewModel ForexContractInfoItemDialogViewModel { get; }
        public RelayCommand AddChiPhiCommand { get; set; }
        public RelayCommand DeleteChiPhiCommand { get; set; }
        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public RelayCommand CellEditingCommand { get; }
        public ForexContractInfoDialogViewModel
        (
            ISessionService sessionService,
            IMapper mapper,
            INsDonViService nsDonViService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            INhKhTongTheService nhKhTongTheService,
            INhKhTongTheNhiemVuChiService nhKhTongTheNhiemVuChiService,
            INhKhChiTietHopDongService nhKhChiTietHopDongService,
            INhKhChiTietService nhKhChiTietService,
            INhDaHopDongService nhDaHopDongService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhHdnkCacQuyetDinhService nhHdnkCacQuyetDinhService,
            INhDaHopDongHangMucService nhDaHopDongHangMucService,
            INhDaHopDongChiPhiService nhDaHopDongChiPhiService,
            INsNguonNganSachService nsNguonVonService,
            INhDaGoiThauService nhDaGoiThauService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDaGoiThauChiPhiService nhDaGoiThauChiPhiService,
            INhDaGoiThauNguonVonService nhDaGoiThauNguonVonService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaDuAnService nhDaDuAnService,
            INhHdnkPhuongAnNhapKhauService phuongAnNhapKhauService,
            ILog logger,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDmChiPhiService nhDmChiPhiService,
            ForexContractInfoItemsViewModel forexContractInfoItemsViewModel,
            ForexContractInfoItemDialogViewModel forexContractInfoItemDialogViewModel
        )
        : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhKhTongTheService = nhKhTongTheService;
            _nhKhTongTheNhiemVuChiService = nhKhTongTheNhiemVuChiService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhHdnkCacQuyetDinhService = nhHdnkCacQuyetDinhService;
            _nhDaHopDongChiPhiService = nhDaHopDongChiPhiService;
            _nhDaHopDongHangMucService = nhDaHopDongHangMucService;
            _nsNguonVonService = nsNguonVonService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhDaGoiThauService = nhDaGoiThauService;
            _nhDaGoiThauNguonVonService = nhDaGoiThauNguonVonService;
            _nhDaGoiThauChiPhiService = nhDaGoiThauChiPhiService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaDuAnService = nhDaDuAnService;
            _phuongAnNhapKhauService = phuongAnNhapKhauService;
            _nhDmChiPhiService = nhDmChiPhiService;

            ForexContractInfoItemsViewModel = forexContractInfoItemsViewModel;

            ForexContractInfoItemDialogViewModel = forexContractInfoItemDialogViewModel;
            ForexContractInfoItemDialogViewModel.ParentPage = this;

            AddChiPhiCommand = new RelayCommand(obj => OnAddGoiThauChiPhi(obj), obj => (bool)obj || SelectedChiPhi != null);
            DeleteChiPhiCommand = new RelayCommand(obj => OnDeleteGoiThauChiPhi(), obj => SelectedChiPhi != null);
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail());
            CellEditingCommand = new RelayCommand(obj => OnCellEditEnding(obj));
        }

        public override void Init()
        {
            OnPropertyChanged(nameof(TypeView));
            LoadDefaul();
            LoadKeHoachTongThe();
            LoadDonVi();
            LoadChuongTrinh();
            LoadAttach();
            LoadContractType();
            LoadTiGia(1);
            LoadTiGiaChiTiet(1);
            LoadDanhMucChiPhi();
            LoadData();
            CaculatorTotalChiPhi();
        }

        private void LoadDefaul()
        {
            _sessionInfo = _sessionService.Current;
            _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();
            _tongTienChiPhi = new NhDaHopDongChiPhiModel();
        }

        #region -- Yeu cau moi -----------------------

        public void GoiThauChiPhi_BeginningEditHanlder(DataGridBeginningEditEventArgs e)
        {
            SelectedChiPhi = (NhDaHopDongChiPhiModel)e.Row.Item;
            if (e.Column.SortMemberPath.Equals(nameof(NhDaHopDongChiPhiModel.FGiaTriUsd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongChiPhiModel.FGiaTriVnd)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongChiPhiModel.FGiaTriEur)) ||
                e.Column.SortMemberPath.Equals(nameof(NhDaHopDongChiPhiModel.FGiaTriNgoaiTeKhac)))
            {
                CalculateChiPhi();
                CaculatorTotalChiPhi();
                e.Cancel = ItemsChiPhi.Any(y => y.IIdParentId == SelectedChiPhi.Id);
            }
        }

        public virtual void LoadTiGia(int i)
        {
            if (SelectedTiGiaChiTietDialog != null)
            {
                var data = _nhDmTiGiaService.FindAll().Where(x => x.SMaTienTeGoc.ToUpper() == SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()).ToList();
                var dataUSD = _nhDmTiGiaService.FindAll().Where(x => x.SMaTienTeGoc.ToUpper() == "USD");
                if(dataUSD != null)
                {
                    foreach (var item in dataUSD)
                    {
                        var lstChiTiet = _nhDmTiGiaChiTietService.FindAll().Where(x => x.IIdTiGiaId == item.Id).ToList();
                        if(lstChiTiet != null)
                        {
                            if (lstChiTiet.Any(x => x.SMaTienTeQuyDoi == SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()))
                            {
                                data.Add(item);
                            }
                        }
                    }
                }
                
                ItemsTiGiaDialog = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            }

        }
        public void LoadTiGiaChiTiet(int i)
        {
            _itemsTiGiaChiTietDialog = new ObservableCollection<NhDmTiGiaChiTietModel>();
            var data = _nhDmTiGiaChiTietService.FindAll().Where(x => x.SMaTienTeQuyDoi.ToUpper() != "USD" && x.SMaTienTeQuyDoi.ToUpper() != "VND").GroupBy(x => x.SMaTienTeQuyDoi.ToUpper()).Select(x => x.First());
            _itemsTiGiaChiTietDialog = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            _itemsTiGiaDialog = new ObservableCollection<NhDmTiGiaModel>();
            OnPropertyChanged(nameof(ItemsTiGiaChiTietDialog));
        }

        private void OnAddGoiThauChiPhi(object obj)
        {
            if (_itemsChiPhi == null) _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();

            NhDaHopDongChiPhiModel sourceItem = _selectedChiPhi;
            NhDaHopDongChiPhiModel targetItem = new NhDaHopDongChiPhiModel();
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
            }
            targetItem.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            //targetItem.IsSaved = false;
            targetItem.Id = Guid.NewGuid();
            targetItem.PropertyChanged += GoiThauChiPhi_PropertyChanged;
            _itemsChiPhi.Insert(currentRow + 1, targetItem);

            OrderItemsChiPhi(targetItem.IIdParentId);
            UpdateTreeItemsChiPhi();
            OnPropertyChanged(nameof(ItemsChiPhi));
        }

        public void OnAddGoiThauChiPhi(
            NhDaHopDongChiPhiModel entity,
            bool isParent = true)
        {
            if (_itemsChiPhi == null) _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();

            NhDaHopDongChiPhiModel sourceItem = SelectedChiPhi;
            NhDaHopDongChiPhiModel targetItem = new NhDaHopDongChiPhiModel();
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
            }
            targetItem.Id = Guid.NewGuid();
            targetItem.IsAdded = true;
            //targetItem.IIdQdDauTuHangMucId = entity.IIdQdDauTuHangMucId;
            //targetItem.IsSuggestion = entity.IsSuggestion;
            targetItem.SMaChiPhi = entity.SMaChiPhi;
            targetItem.SMaOrder = entity.SMaOrder;
            targetItem.STenChiPhi = entity.STenChiPhi;
            targetItem.PropertyChanged += GoiThauChiPhi_PropertyChanged;

            ItemsChiPhi.Insert(currentRow + 1, targetItem);
            ItemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>(ItemsChiPhi.OrderBy(h => h.SMaChiPhi));

            OrderItemsChiPhi(targetItem.IIdParentId);
            UpdateTreeItemsChiPhi();
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

        

        private void GoiThauChiPhi_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NhDaHopDongChiPhiModel objectSender = (NhDaHopDongChiPhiModel)sender;
            
            if (e.PropertyName == nameof(NhDaHopDongChiPhiModel.FGiaTriUsd) && FTiGiaNhap != null && FTiGiaNhap != 0)
            {
                objectSender.FGiaTriNgoaiTeKhac = objectSender.FGiaTriUsd / FTiGiaNhap;
            }
            else if (e.PropertyName == nameof(NhDaHopDongChiPhiModel.FGiaTriNgoaiTeKhac) && FTiGiaNhap != null)
            {
                objectSender.FGiaTriUsd = objectSender.FGiaTriNgoaiTeKhac * FTiGiaNhap;
            }
            CalculateChiPhi();
            CaculatorTotalChiPhi();

            OnPropertyChanged(nameof(HasChanged));
        }

        public override void OnCellEditEnding(object obj)
        {
            if (obj is DataGridCellEditEndingEventArgs e)
            {
                // Tính toán chuyển đổi tiền tệ
                if (e.EditAction == DataGridEditAction.Commit && e.Row.Item is CurrencyDetailModelBase item)
                {
                    string propertyName = e.Column.SortMemberPath;
                    if (propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriNgoaiTeKhac)))
                    {
                        if (FTiGiaNhap != null)
                        {
                            item.FGiaTriUsd = item.FGiaTriNgoaiTeKhac * FTiGiaNhap;
                        }
                    }
                    else if (propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriUsd)))
                    {
                        if (FTiGiaNhap != null && FTiGiaNhap != 0)
                        {
                            item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                        }
                    }
                }
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

        private void DeleteTreeItemsChiPhi(NhDaHopDongChiPhiModel currentItem, bool status)
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

        private int CountTreeChildItems(NhDaHopDongChiPhiModel currentItem)
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

        #endregion

        #region -- event chi phí ------------------------------------------------

        private void OnShowHangMucDetail()
        {
            if(!ValidateHangMuc()) return;
            ForexContractInfoItemDialogViewModel.Model = SelectedChiPhi;
            ForexContractInfoItemDialogViewModel.FTiGiaNhap = FTiGiaNhap;
            ForexContractInfoItemDialogViewModel.LstTiGiaChiTiet = null;
            ForexContractInfoItemDialogViewModel.SMaTienTeGoc = SelectedTiGiaDialog != null ? SelectedTiGiaDialog.SMaTienTeGoc : null ;
            ForexContractInfoItemDialogViewModel.SMaTienTeQuyDoi = SelectedTiGiaChiTietDialog != null ? SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi : "";
            ForexContractInfoItemDialogViewModel.IsAdded = IsAdded;
            ForexContractInfoItemDialogViewModel.IsDetail = BIsReadOnly;
            ForexContractInfoItemDialogViewModel.IsEnableSoCuChuongTrinh = IsEnableSoCuChuongTrinh;
            ForexContractInfoItemDialogViewModel.CurrencyExchangeAction = (obj) => GoiThauHangMucCurrencyExchange(obj);
            ForexContractInfoItemDialogViewModel.Init();
            ForexContractInfoItemDialogViewModel.SavedAction = obj =>
            {
                var data = (obj as IEnumerable<NhDaHopDongHangMucModel>).Where(s => !s.IsDeleted);

                // Tính tổng tiền hạng mục
                if (!data.IsEmpty())
                {
                    SelectedChiPhi.HopDongHangMucs = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(data);

                    var goiThauChiPhis = data.Where(s => s.IIdParentId == null && !s.IsDeleted);
                    SelectedChiPhi.FGiaTriUsd = goiThauChiPhis.Sum(s => s.FGiaTriUsd);
                    SelectedChiPhi.FGiaTriEur = goiThauChiPhis.Sum(s => s.FGiaTriEur);
                    SelectedChiPhi.FGiaTriVnd = goiThauChiPhis.Sum(s => s.FGiaTriVnd);
                    SelectedChiPhi.FGiaTriNgoaiTeKhac = goiThauChiPhis.Sum(s => s.FGiaTriNgoaiTeKhac);
                    SelectedChiPhi.IsModified = true;
                    CalculateChiPhi();
                }
            };
            ForexContractInfoItemDialogViewModel.ShowDialogHost("ForexContractInfoItems");

            SuggestionsHangMuc();
        }

        /// <summary>
        /// Gợi ý nhập hạng mục, được lấy theo sở cứ trục tiếp tương ứng
        /// </summary>
        private void SuggestionsHangMuc()
        {
        }

        private void CaculatorTotalChiPhi()
        {
            //if (ItemsChiPhi.IsEmpty()) return;
            //var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
            //if (sumChiPhis.Any())
            if (_itemsChiPhi.IsEmpty())
            {
                _tongTienChiPhi = new NhDaHopDongChiPhiModel();
            }
            else
            {
                var sumChiPhis = ItemsChiPhi.Where(n => !n.IsDeleted && n.IIdParentId == null);
                _tongTienChiPhi.FGiaTriUsd = Model.FGiaTriUsd = sumChiPhis.Sum(n => n.FGiaTriUsd);
                _tongTienChiPhi.FGiaTriEur = Model.FGiaTriEur = sumChiPhis.Sum(n => n.FGiaTriEur);
                _tongTienChiPhi.FGiaTriVnd = Model.FGiaTriVnd = sumChiPhis.Sum(n => n.FGiaTriVnd);
                _tongTienChiPhi.FGiaTriNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac = sumChiPhis.Sum(n => n.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(TongTienChiPhi));
            }
        }
        private void CalculateModel()
        {
            
            if (ItemsChiPhi != null && FTiGiaNhap != null)
            {
                foreach (var item in ItemsChiPhi)
                {
                    if(item.HopDongHangMucs != null && item.HopDongHangMucs.Count() > 0)
                    {
                        var lstHangMucByChiPhi = item.HopDongHangMucs.Where(x => x.IIdHopDongChiPhiId == item.Id).ToList();
                        if (lstHangMucByChiPhi != null)
                        {
                            if (lstHangMucByChiPhi != null && FTiGiaNhap != null)
                            {
                                foreach (var itemHangMuc in lstHangMucByChiPhi)
                                {
                                    if (itemHangMuc.SThanhToanBang == "USD")
                                    {
                                        itemHangMuc.FGiaTriNgoaiTeKhac = itemHangMuc.FGiaTriUsd / FTiGiaNhap;
                                    }
                                    if (itemHangMuc.SThanhToanBang == "NTK")
                                    {
                                        itemHangMuc.FGiaTriUsd = itemHangMuc.FGiaTriNgoaiTeKhac * FTiGiaNhap;
                                    }
                                }
                            }
                            item.FGiaTriUsd = lstHangMucByChiPhi.Sum(x => x.FGiaTriUsd);
                            item.FGiaTriNgoaiTeKhac = lstHangMucByChiPhi.Sum(x => x.FGiaTriNgoaiTeKhac);
                        }
                        else
                        {
                            item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                        }
                    }
                    else
                    {
                        item.FGiaTriNgoaiTeKhac = item.FGiaTriUsd / FTiGiaNhap;
                    }
                    
                }
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

        private void CalculateChiPhi(NhDaHopDongChiPhiModel parentItem)
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

        private void GoiThauHangMucCurrencyExchange(object obj)
        {
            OnCellEditEnding(obj);
        }

        #endregion

        private void LoadDanhMucChiPhi()
        {
            IEnumerable<NhDmChiPhi> data = _nhDmChiPhiService.FindAll();
            _itemsDMChiPhi = _mapper.Map<ObservableCollection<NhDmChiPhiModel>>(data);
            
            OnPropertyChanged(nameof(ItemsDMChiPhi));
        }

        private void LoadKeHoachTongThe()
        {
            IEnumerable<NhKhTongThe> data = _nhKhTongTheService.FindAll(s => s.BIsActive).OrderByDescending(x => x.DNgayTao);
            _itemsKeHoachTongThe = _mapper.Map<ObservableCollection<NhKhTongTheModel>>(data);
            _itemsKeHoachTongThe.ForAll(s =>
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
            OnPropertyChanged(nameof(ItemsKeHoachTongThe));
        }

        private void LoadDonVi()
        {
            _itemsDonVi = new ObservableCollection<DonViModel>();
            if (_selectedKeHoachTongThe != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> data = _nhKhTongTheNhiemVuChiService.FindAllDonViByKhTongTheId(_selectedKeHoachTongThe.Id).Where(x => x.NamLamViec == _sessionInfo.YearOfWork).ToList();
                _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsDonVi));
        }
        private void LoadChuongTrinh()
        {
            _itemsChuongTrinh = new ObservableCollection<NhKhTongTheNhiemVuChiModel>();
            if (_selectedKeHoachTongThe != null && _selectedDonVi != null)
            {
                List<NhKhTongTheNhiemVuChiQuery> listKhTongTheNhiemVuChi = _nhKhTongTheNhiemVuChiService.FindByIdKhTongTheAndIdDonVi(_selectedKeHoachTongThe.Id, _selectedDonVi.Id).ToList();
                _itemsChuongTrinh = _mapper.Map<ObservableCollection<NhKhTongTheNhiemVuChiModel>>(listKhTongTheNhiemVuChi);
            }
            OnPropertyChanged(nameof(ItemsChuongTrinh));
        }

        private void LoadDuAn()
        {
            _itemsDuAn = new ObservableCollection<NhDaDuAnModel>();
            if (_selectedChuongTrinh != null)
            {
                IEnumerable<NhDaDuAn> listDuAn = _nhDaDuAnService.FindAll(s => s.IIdKhttNhiemVuChiId == _selectedChuongTrinh.Id);
                _itemsDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(listDuAn);
            }
            OnPropertyChanged(nameof(ItemsDuAn));
        }

        private void LoadDsGoiThau()
        {
            _itemsGoiThau = new ObservableCollection<NhDaGoiThauModel>();
            if (_selectedDuAn != null)
            {
                IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdDuAnId == _selectedDuAn.Id).Where(s=>s.ILoai == 1 && s.IThuocMenu == 1);
                if (data.Any())
                {
                    _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                }
                List<NhDaGoiThauQuery> listNhDaGoiThauQuery = _nhKhTongTheNhiemVuChiService.FindByIdNhiemVuChi(_selectedChuongTrinh.Id).Where(s => s.ILoai == 1 && s.IThuocMenu == 1 && s.BActive == true).ToList();
                _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(listNhDaGoiThauQuery);
            }
            else
            {
                // Nếu ko có dự án, thì lấy ds gói thầu theo NV chi
                if (_selectedChuongTrinh != null)
                {
                    IEnumerable<NhDaGoiThau> data = _nhDaGoiThauService.FindAll(s => s.IIdKhTongTheNhiemVuChiId == _selectedChuongTrinh.Id).Where(s => s.ILoai == 1 && s.IThuocMenu == 1 && s.BActive == true);
                    if (data.Any())
                    {
                        _itemsGoiThau = _mapper.Map<ObservableCollection<NhDaGoiThauModel>>(data);
                    }
                }
            }
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void LoadSoQDAndSoPhuongAnNK()
        {
            SDonViUyThac = string.Empty;
            SPhuongAnNhapKhau = string.Empty;
            SQuyetDinhChiTiet = string.Empty;
            if (_selectedGoiThau != null)
            {
                if (_selectedGoiThau.IIdNhaThauId.HasValue)
                {
                    SDonViUyThac = _nhDmNhaThauService.FindById(_selectedGoiThau.IIdNhaThauId.Value)?.STenNhaThau;
                }
                if (_selectedGoiThau.IIdPhuongAnNhapKhauId.HasValue)
                {
                    SPhuongAnNhapKhau = _phuongAnNhapKhauService.FindById(_selectedGoiThau.IIdPhuongAnNhapKhauId.Value)?.SSoQuyetDinh;
                }
                if (_selectedGoiThau.IIdQuyetDinhChiTietId.HasValue)
                {
                    SQuyetDinhChiTiet = _nhHdnkCacQuyetDinhService.FindById(_selectedGoiThau.IIdQuyetDinhChiTietId.Value)?.SSoQuyetDinh;
                }
            }
        }

        private void LoadDsChiPhi()
        {
            _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>();

            if (!Model.Id.IsNullOrEmpty())
            {
                IEnumerable<NhDaHopDongChiPhiModel> lstChiPhi = _mapper.Map<ObservableCollection<NhDaHopDongChiPhiModel>>(_nhDaHopDongChiPhiService.FindByIdHopDong(Model.Id));
                    
                if (!lstChiPhi.IsEmpty())
                {
                    _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>(lstChiPhi);
                    _itemsChiPhi.ForAll(s =>
                    {
                        s.PropertyChanged += GoiThauChiPhi_PropertyChanged;
                        s.SMaChiPhi = StringUtils.ConvertMaOrder(s.SMaOrder);
                        var lstHangMuc = _mapper.Map<ObservableCollection<NhDaHopDongHangMucModel>>(_nhDaHopDongHangMucService.FindByHopDongChiPhi(s.Id));
                        s.HopDongHangMucs = lstHangMuc;
                    });

                }
            }
            _itemsChiPhi = new ObservableCollection<NhDaHopDongChiPhiModel>(_itemsChiPhi.OrderBy(x => x.SMaOrder));
            _itemsChiPhi.ForAll(s =>
            {
                s.IsHangCha = ItemsChiPhi.Any(y => y.IIdParentId == s.Id);
                s.ItemsLoaiNoiDungChi = ItemsDMChiPhi;
                s.IsModified = true;
                
            });
            UpdateTreeItemsChiPhi();
            OnPropertyChanged(nameof(ItemsChiPhi));
            CaculateHopDong();
        }

        private void CaculateHopDong()
        {
            if (!_itemsHopDongHangMuc.IsEmpty())
            {
                Model.FGiaTriUsd = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriUsd);
                Model.FGiaTriVnd = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriVnd);
                Model.FGiaTriEur = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriEur);
                Model.FGiaTriNgoaiTeKhac = _itemsHopDongHangMuc.Where(s => s.IsChecked && s.IIdParentId.IsNullOrEmpty()).Sum(s => s.FGiaTriNgoaiTeKhac);
                OnPropertyChanged(nameof(Model));
            }
        }
        private void ShowTiGiaNhap()
        {
            if (SelectedTiGiaDialog != null)
            {
                IEnumerable<NhDmTiGiaChiTiet> tiGiaChiTietList = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGiaDialog.Id);
                NhDmTiGiaChiTiet tiGiaChiTietObj = null;
                if (SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() == "USD")
                {
                    tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper()));
                }
                else
                {
                    tiGiaChiTietObj = tiGiaChiTietList.FirstOrDefault(x => x.SMaTienTeQuyDoi.ToUpper().Equals(LoaiTienTeEnum.TypeCode.USD));
                }
                if (tiGiaChiTietObj != null)
                {
                    if (SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() == "USD")
                    {
                        if(tiGiaChiTietObj.FTiGia != null && tiGiaChiTietObj.FTiGia != 0)
                            FTiGiaNhap = 1/tiGiaChiTietObj.FTiGia;

                        Model.SNgoaiTeGoc = "1 " + SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi.ToUpper() + " ";
                    }
                    else
                    {
                        Model.SNgoaiTeGoc = "1 " + SelectedTiGiaDialog.SMaTienTeGoc.ToUpper() + " ";
                        FTiGiaNhap = tiGiaChiTietObj.FTiGia;
                    }

                }
            }
        }

        public override void LoadData(params object[] args)
        {
            if (!Model.Id.IsNullOrEmpty())
            {
                if (IsReadOnly)
                {
                    Description = "Xem thông tin hợp đồng";
                }
                else
                {
                    Description = "Cập nhật thông tin hợp đồng";
                    if (IsHieuChinhImport)
                    {
                        LoadHopDongImportHieuChinh();
                        return;
                    }
                }
            }
            else
            {
                Description = "Thêm mới thông tin hợp đồng";
                if (IsDieuChinhImport)
                {
                    LoadThongTinImportHieuChinh();
                    return;
                }
            }
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            if (SelectedKeHoachTongThe != null)
            {
                LoadDonVi();
                LoadChuongTrinh();
            }
            LoadDonVi();
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.Id.Equals(Model.IIdDonViQuanLyId));//id đơn vị equals tạm thời
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            LoadDsGoiThau();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsChiPhi();

            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);
            IsVisibleTiGiaNhap = SelectedTiGiaDialog != null;
            FTiGiaNhap = Model.FTiGiaNhap;
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
            OnPropertyChanged(nameof(Description));
        }

        private void LoadHopDongImportHieuChinh()
        {
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            LoadDsGoiThau();
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsChiPhi();
            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
            OnPropertyChanged(nameof(Description));
        }

        private void LoadThongTinImportHieuChinh()
        {
            _selectedKeHoachTongThe = _itemsKeHoachTongThe.FirstOrDefault(s => s.Id == Model.IIdKhTongTheId);
            _selectedDonVi = _itemsDonVi.FirstOrDefault(x => x.IIDMaDonVi.Equals(Model.IIdMaDonViQuanLy));
            LoadChuongTrinh();
            _selectedChuongTrinh = _itemsChuongTrinh.FirstOrDefault(s => s.Id == Model.IIdKhTongTheNhiemVuChiId);
            LoadDuAn();
            _selectedDuAn = _itemsDuAn.FirstOrDefault(x => x.Id == Model.IIdDuAnId);
            LoadDsGoiThau();
            _selectedGoiThau = _itemsGoiThau.FirstOrDefault(s => s.Id == Model.IIdGoiThauId);

            // Load nguon von
            LoadSoQDAndSoPhuongAnNK();
            LoadDsChiPhi();
            _selectedContractType = _itemsContractType.FirstOrDefault(s => s.Id == Model.IIdLoaiHopDongId);
            // Load tỉ giá và ngoại tệ khác
            SelectedTiGiaChiTietDialog = ItemsTiGiaChiTietDialog.FirstOrDefault(x => x.SMaTienTeQuyDoi.Equals(Model.SMaNgoaiTeKhac));
            LoadTiGia(1);
            SelectedTiGiaDialog = ItemsTiGiaDialog.FirstOrDefault(x => x.Id == Model.IIdTiGiaId);

            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(SelectedKeHoachTongThe));
            OnPropertyChanged(nameof(SelectedDonVi));
            OnPropertyChanged(nameof(SelectedChuongTrinh));
            OnPropertyChanged(nameof(SelectedDuAn));
            OnPropertyChanged(nameof(SelectedGoiThau));
            OnPropertyChanged(nameof(SelectedContractType));
            OnPropertyChanged(nameof(SelectedTiGiaDialog));
            OnPropertyChanged(nameof(SelectedTiGiaChiTietDialog));
            OnPropertyChanged(nameof(Description));
        }



        private bool ValidateHangMuc()
        {
            List<string> lstError = new List<string>();
            if (SelectedTiGiaDialog == null && SelectedTiGiaChiTietDialog != null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            }
            //if (SelectedTiGiaChiTietDialog == null)
            //{
            //    lstError.Add("Vui lòng chọn mã ngoại tệ khác");
            //}
            if (lstError.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show(string.Join("\n", lstError),
                    Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private bool ValidationData()
        {
            List<string> lstError = new List<string>();
            if (SelectedKeHoachTongThe == null)
            {
                lstError.Add(Resources.MsgCheckKeHoachTongThe);
            }
            if (SelectedDonVi == null)
            {
                lstError.Add(Resources.MsgCheckDonVi);
            }
            if (SelectedChuongTrinh == null)
            {
                lstError.Add(Resources.MsgCheckChuongTrinh);
            }
            if (SelectedGoiThau == null)
            {
                lstError.Add(Resources.MsgCheckTenGoiThau);
            }
            if (string.IsNullOrEmpty(Model.SSoHopDong))
            {
                lstError.Add(Resources.MsgCheckSoHopDong);
            }
            if (string.IsNullOrEmpty(Model.STenHopDong))
            {
                lstError.Add(Resources.MsgCheckTenHD);
            }
            if (Model.DNgayHopDong == null)
            {
                lstError.Add(Resources.MsgCheckNgayBanHanhKH);
            }
            if (SelectedTiGiaDialog == null && SelectedTiGiaChiTietDialog != null)
            {
                lstError.Add(Resources.MsgCheckTiGiaNgoaiHoi);
            }
            //if (SelectedTiGiaChiTietDialog == null)
            //{
            //    lstError.Add(string.Format("Vui lòng chọn mã ngoại tệ khác"));
            //}
            if (Model.DKetThucDuKien <= Model.DKhoiCongDuKien)
            {
                lstError.Add(Resources.MsgContractDateTime);
            }
            int Count = 0;
            foreach(var item in ItemsChiPhi.Where(x => x.IsDeleted != true))
            {
                if (item.IIdChiPhiId != null)
                {
                    var itemChiPhi = ItemsDMChiPhi.Where(x => x.IIdChiPhi == item.IIdChiPhiId).FirstOrDefault();
                    if (itemChiPhi != null)
                        item.STenChiPhi = itemChiPhi.STenChiPhi;
                }
                
                Count++;
                if (item.IsLoaiNoiDungChi && item.IIdChiPhiId == null)
                {
                    lstError.Add(string.Format("Dòng "+ Count + ": Chưa chọn tên chi phí !"));
                }
                else if (item.IsNoiDungChi && item.STenChiPhi == null)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập tên chi phí !"));
                }
                if (item.FGiaTriUsd == null && item.FGiaTriVnd == null)
                {
                    lstError.Add(string.Format("Dòng " + Count + ": Chưa nhập giá trị hợp đồng !"));
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
            if (!ValidationData()) return;
            if (!ValidateViewModelHelper.Validate(Model)) return;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (SelectedKeHoachTongThe != null)
                {
                    Model.IIdKhTongTheId = SelectedKeHoachTongThe.Id;
                }
                if (SelectedDuAn != null)
                {
                    Model.IIdDuAnId = SelectedDuAn.Id;
                }
                if (SelectedGoiThau != null)
                {
                    Model.IIdGoiThauId = SelectedGoiThau.Id;
                    Model.IIdNhaThauThucHienId = SelectedGoiThau.IIdNhaThauId;
                }
                if (SelectedChuongTrinh != null)
                {
                    Model.IIdKhTongTheNhiemVuChiId = SelectedChuongTrinh.Id;
                }
                if (SelectedDonVi != null)
                {
                    Model.IIdDonViQuanLyId = SelectedDonVi.Id;
                    Model.IIdMaDonViQuanLy = SelectedDonVi.IIDMaDonVi;
                }
                if (SelectedContractType != null)
                {
                    Model.IIdLoaiHopDongId = SelectedContractType.Id;
                }
                if (SelectedTiGiaDialog != null)
                {
                    Model.IIdTiGiaId = SelectedTiGiaDialog.Id;
                }
                if (SelectedTiGiaChiTietDialog != null)
                {
                    Model.SMaNgoaiTeKhac = SelectedTiGiaChiTietDialog.SMaTienTeQuyDoi;
                }

                Model.HopDongChiPhis = ItemsChiPhi;
                Model.ILoai = ILoai;
                Model.IThuocMenu = IThuocMenu;
                Model.FTiGiaNhap = FTiGiaNhap;
                NhDaHopDong entity = _mapper.Map<NhDaHopDong>(Model);

                if (IsInsert)
                {
                    // insert
                    entity.Id = Guid.NewGuid();
                    Model.Id = entity.Id;
                    entity.DNgayTao = DateTime.Now;
                    entity.SNguoiTao = _sessionService.Current.Principal;
                    entity.ILanDieuChinh = 0;
                    entity.BIsActive = true;
                    entity.BIsKhoa = false;
                    entity.BIsGoc = true;
                    entity.FGiaTriHopDongUSD = Model.FGiaTriUsd;
                    entity.FGiaTriHopDongVND = Model.FGiaTriVnd;
                    entity.FGiaTriHopDongEUR = Model.FGiaTriEur;
                    entity.FGiaTriHopDongNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac; 
                    _nhDaHopDongService.AddHDNT(entity);
                    IsInsert = false;
                    e.Result = entity;
                }
                else if (IsEdit)
                {
                    // update
                    entity.DNgaySua = DateTime.Now;
                    entity.SNguoiSua = _sessionService.Current.Principal;
                    entity.FGiaTriHopDongUSD = Model.FGiaTriUsd;
                    entity.FGiaTriHopDongVND = Model.FGiaTriVnd;
                    entity.FGiaTriHopDongEUR = Model.FGiaTriEur;
                    entity.FGiaTriHopDongNgoaiTeKhac = Model.FGiaTriNgoaiTeKhac;

                    _nhDaHopDongService.UpdateHDNT(entity);
                    e.Result = entity;
                }
                else
                {
                    // adjust
                    if (IsDieuChinh)
                    {
                        entity.Id = Guid.NewGuid();
                        entity.IIdParentAdjustId = Model.Id;
                        entity.ILanDieuChinh += 1;
                        entity.BIsActive = true;
                        entity.BIsKhoa = false;
                        entity.BIsGoc = false;
                        entity.DNgayTao = DateTime.Now;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        entity.IIdParentId = Model.IIdParentId != null ? Model.IIdParentId : Model.Id;
                        Model.Id = entity.Id;

                        // nếu đang điều chỉnh thì toàn bộ các bản ghi trong ItemsChiPhi là các bản ghi cần được thêm mới
                        foreach(NhDaHopDongChiPhi cp in entity.HopDongChiPhis)
                        {
                            if(cp.IsDeleted) continue;
                            cp.IsAdded= true;
                            cp.Id = Guid.NewGuid();
                            cp.IIdHopDongId = entity.Id;
                            foreach(NhDaHopDongHangMuc hm in cp.HopDongHangMucs)
                            {
                                if(hm.IsDeleted) continue;
                                hm.IsAdded= true;
                                hm.IIdHopDongId = entity.Id;
                                hm.Id = Guid.NewGuid() ;
                            }
                        }

                        _nhDaHopDongService.AdjustHDNT(entity);
                        e.Result = entity;
                    }
                }
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    SavedAction?.Invoke(Model);
                    LoadData();
                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);
                    if (obj is Window window)
                    {
                        Dispose();
                        window.Close();
                    }
                }
                else
                {
                    MessageBoxHelper.Info(e.Error.Message);
                    _logger.Error(e.Error.Message);
                }
            });
        }

        private void LoadContractType()
        {
            var lstContractType = _nhDmLoaiHopDongService.FindAll();
            if (lstContractType == null) return;
            var drpItem = lstContractType.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIdLoaiHopDongId.ToString(),
                DisplayItem = n.STenLoaiHopDong,
                Id = n.IIdLoaiHopDongId
            });
            _itemsContractType = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(ItemsContractType));
        }


        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
