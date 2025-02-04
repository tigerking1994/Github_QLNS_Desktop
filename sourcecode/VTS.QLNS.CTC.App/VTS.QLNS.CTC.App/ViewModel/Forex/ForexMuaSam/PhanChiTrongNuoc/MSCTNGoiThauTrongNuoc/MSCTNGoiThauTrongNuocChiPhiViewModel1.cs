using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiTrongNuoc.MSCTNGoiThauTrongNuoc
{
    public class MSCTNGoiThauTrongNuocChiPhiViewModel : DetailViewModelBase<NhDaGoiThauModel, NhDaKhlcNhaThauModel>
    {
        #region Private
        private readonly INhDmTiGiaService _nhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;
        private readonly INhDaGoiThauHangMucSerrvice _goithauHangMucService;
        #endregion

        #region Public
        public override string Name => "THÔNG TIN GÓI THẦU CHI TIẾT";
        public override string Title => "Quản lý thông tin gói thầu chi tiết";
        public override Type ContentType => typeof(MSCTNGoiThauTrongNuocChiPhi);
        #endregion
        #region Items
        private IEnumerable<NhDmTiGiaChiTiet> _lstTiGiaChiTiet;
        public IEnumerable<NhDmTiGiaChiTiet> LstTiGiaChiTiet
        {
            get => _lstTiGiaChiTiet;
            set => SetProperty(ref _lstTiGiaChiTiet, value);
        }

        public string SHeaderGiaTriPheDuyet => string.Format("GIÁ TRỊ {0} PHÊ DUYỆT",
            SLoaiSoCu == ((int)SO_CU_TRUC_TIEP.TypeValue.DU_TOAN).ToString() ? "DỰ TOÁN"
            : "QUYẾT ĐỊNH ĐẦU TƯ");

        private string _sLoaiSoCu;
        public string SLoaiSoCu
        {
            get => _sLoaiSoCu;
            set => SetProperty(ref _sLoaiSoCu, value);
        }
        public bool BIsEnable => !BIsReadOnly;
        public Guid? IdDieuChinh { get; set; }
        public virtual AttachmentEnum.Type ModuleType { get; }
        public virtual bool IsShowCanCu => ModuleType == AttachmentEnum.Type.VDT_DENGHI_THANHTOAN;

        private bool _bIsReadOnly;
        public virtual bool BIsReadOnly
        {
            get => _bIsReadOnly;
            set => SetProperty(ref _bIsReadOnly, value);
        }
        private double? _fTiGiaNhap;
        public double? FTiGiaNhap
        {
            get => _fTiGiaNhap;
            set => SetProperty(ref _fTiGiaNhap, value);
        }
        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private int? _iLoaiDuToan;
        public int? ILoaiDuToan
        {
            get => _iLoaiDuToan;
            set => SetProperty(ref _iLoaiDuToan, value);
        }

        private string _sMaTienTeGoc;
        public string SMaTienTeGoc
        {
            get => _sMaTienTeGoc;
            set => SetProperty(ref _sMaTienTeGoc, value);
        }

        private string _sMaTienTeQuyDoi;
        public string SMaTienTeQuyDoi
        {
            get => _sMaTienTeQuyDoi;
            set => SetProperty(ref _sMaTienTeQuyDoi, value);
        }

        private ObservableCollection<NhDaGoiThauDetailNguonVonModel> _itemNguonVon;
        public ObservableCollection<NhDaGoiThauDetailNguonVonModel> ItemNguonVon
        {
            get => _itemNguonVon;
            set => SetProperty(ref _itemNguonVon, value);
        }

        private NhDaGoiThauDetailNguonVonModel _selectedNguonVon;
        public NhDaGoiThauDetailNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        private bool _selectAllDuToanFilter;
        public bool SelectAllDuToanFilter
        {
            get => (ItemNguonVon == null || !ItemNguonVon.Any()) ? false : ItemNguonVon.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuToanFilter, value);
                if (ItemNguonVon != null)
                {
                    ItemNguonVon.Select(c => { c.IsChecked = _selectAllDuToanFilter; return c; }).ToList();
                }
            }
        }

        private ObservableCollection<NhDaGoiThauDetailNguonVonModel> _itemNguonVonView;
        public ObservableCollection<NhDaGoiThauDetailNguonVonModel> ItemNguonVonView
        {
            get => _itemNguonVonView;
            set => SetProperty(ref _itemNguonVonView, value);
        }

        private NhDaGoiThauDetailNguonVonModel _selectedNguonVonView;
        public NhDaGoiThauDetailNguonVonModel SelectedNguonVonView
        {
            get => _selectedNguonVonView;
            set => SetProperty(ref _selectedNguonVonView, value);
        }

        private ObservableCollection<NhDaGoiThauDetailChiPhiModel> _itemChiPhi = new ObservableCollection<NhDaGoiThauDetailChiPhiModel>();
        public ObservableCollection<NhDaGoiThauDetailChiPhiModel> ItemChiPhi
        {
            get => _itemChiPhi;
            set => SetProperty(ref _itemChiPhi, value);
        }

        private NhDaGoiThauDetailChiPhiModel _selectedChiPhi;
        public NhDaGoiThauDetailChiPhiModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set => SetProperty(ref _selectedChiPhi, value);
        }

        private ObservableCollection<NhDaGoiThauDetailHangMucModel> _itemHangMuc;
        public ObservableCollection<NhDaGoiThauDetailHangMucModel> ItemHangMuc
        {
            get => _itemHangMuc;
            set => SetProperty(ref _itemHangMuc, value);
        }

        private NhDaGoiThauDetailHangMucModel _selectedHangMuc;
        public NhDaGoiThauDetailHangMucModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set => SetProperty(ref _selectedHangMuc, value);
        }

        private List<NhDaGoiThauDetailHangMucModel> _lstHangMuc;
        public List<NhDaGoiThauDetailHangMucModel> LstHangMuc
        {
            get => _lstHangMuc;
            set => SetProperty(ref _lstHangMuc, value);
        }

        private NhDaGoiThauDetailNguonVonModel _objSumNguonVon;
        public NhDaGoiThauDetailNguonVonModel ObjSumNguonVon
        {
            get => _objSumNguonVon;
            set => SetProperty(ref _objSumNguonVon, value);
        }

        private NhDaGoiThauDetailChiPhiModel _objSumChiPhi;
        public NhDaGoiThauDetailChiPhiModel ObjSumChiPhi
        {
            get => _objSumChiPhi;
            set => SetProperty(ref _objSumChiPhi, value);
        }

        private NhDaGoiThauDetailNguonVonModel _objSubNguonVonChiPhi;
        public NhDaGoiThauDetailNguonVonModel ObjSubNguonVonChiPhi
        {
            get => _objSubNguonVonChiPhi;
            set => SetProperty(ref _objSubNguonVonChiPhi, value);
        }

        private NhDaGoiThauDetailHangMucModel _objSumHangMuc;
        public NhDaGoiThauDetailHangMucModel ObjSumHangMuc
        {
            get => _objSumHangMuc;
            set => SetProperty(ref _objSumHangMuc, value);
        }

        private bool _selectAllChiPhi;
        public bool SelectAllChiPhi
        {
            get => ItemChiPhi == null || !ItemChiPhi.Any() ? false : ItemChiPhi.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllChiPhi, value);
                if (ItemChiPhi != null)
                {
                    ItemChiPhi.Select(c => { c.IsChecked = _selectAllChiPhi; return c; }).ToList();
                }
            }
        }

        private bool _selectAllNguonVon;
        public bool SelectAllNguonVon
        {
            get => ItemNguonVon == null || !ItemNguonVon.Any() ? false : ItemNguonVon.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllNguonVon, value);
                if (ItemNguonVon != null)
                {
                    ItemNguonVon.Select(c => { c.IsChecked = _selectAllNguonVon; return c; }).ToList();
                }
            }
        }

        private bool _selectAllHangMuc;
        public bool SelectAllHangMuc
        {
            get => ItemHangMuc == null || !ItemHangMuc.Any() ? false : ItemHangMuc.All(item => item.IsChecked == true);
            set
            {
                SetProperty(ref _selectAllHangMuc, value);
                if (ItemHangMuc != null)
                {
                    ItemHangMuc.Select(c => { c.IsChecked = _selectAllHangMuc; return c; }).ToList();
                }
            }
        }

        private ObservableCollection<NhDaGoiThauDetailChiPhiModel> _lstChiPhi;
        public ObservableCollection<NhDaGoiThauDetailChiPhiModel> LstChiPhi
        {
            get => _lstChiPhi;
            set => SetProperty(ref _lstChiPhi, value);
        }

        private NhDaGoiThauDetailHangMucModel _nhGoiThauDetailHangMucFilter;
        public NhDaGoiThauDetailHangMucModel NHGoiThauDetailHangMucFilter
        {
            get => _nhGoiThauDetailHangMucFilter;
            set => SetProperty(ref _nhGoiThauDetailHangMucFilter, value);
        }

        private string _sDuToanFilter;
        public string SDuToanFilter
        {
            get => _sDuToanFilter;
            set => SetProperty(ref _sDuToanFilter, value);
        }

        #endregion

        public RelayCommand ShowHangMucDetailCommand { get; set; }
        public RelayCommand SaveDataCommand { get; set; }
        public RelayCommand OnCloseCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public MSCTNGoiThauTrongNuocChiPhiViewModel(
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            INhDaGoiThauHangMucSerrvice goithauHangMucService)
        {
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
            _goithauHangMucService = goithauHangMucService;

            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            OnCloseCommand = new RelayCommand(obj => OnClose(obj));
            ShowHangMucDetailCommand = new RelayCommand(obj => OnShowHangMucDetail());
            SearchCommand = new RelayCommand(obj => OnShowHangMucDetail());
            ResetFilterCommand = new RelayCommand(obj => OnRemoveFilter());
        }

        public override void Init()
        {
            SDuToanFilter = string.Empty;
            ObjSumNguonVon = new NhDaGoiThauDetailNguonVonModel();
            ObjSumChiPhi = new NhDaGoiThauDetailChiPhiModel();
            ObjSumHangMuc = new NhDaGoiThauDetailHangMucModel();
            ObjSubNguonVonChiPhi = new NhDaGoiThauDetailNguonVonModel();
            ItemHangMuc = new ObservableCollection<NhDaGoiThauDetailHangMucModel>();
            ItemChiPhi = new ObservableCollection<NhDaGoiThauDetailChiPhiModel>();
            NHGoiThauDetailHangMucFilter = new NhDaGoiThauDetailHangMucModel();
            ItemNguonVonView = new ObservableCollection<NhDaGoiThauDetailNguonVonModel>();
            foreach (var item in ItemNguonVon)
                item.PropertyChanged += NguonVon_PropertyChanged;
            LoadChiPhi();
            NguonVon();
            OnPropertyChanged(nameof(ItemNguonVon));
            ChiPhi();
            OnPropertyChanged(nameof(ItemChiPhi));
            HangMuc();
            OnPropertyChanged(nameof(ItemHangMuc));
        }

        #region Event
        //loaddata chiphi 
        private void HangMuc()
        {
            var hangMuc = ItemHangMuc;
            foreach (var item in hangMuc)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }

        }
        private void NguonVon()
        {
            var nguonVon = ItemNguonVon;
            foreach (var item in nguonVon)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }

        }
        private void ChiPhi()
        {
            var chiPhi = ItemChiPhi;
            foreach (var item in chiPhi)
            {
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
            }

        }
        private void LoadChiPhi()
        {
            SDuToanFilter = string.Join(", ", ItemNguonVon.Where(n => n.IsChecked).Select(n => n.STenNguonVon));

            foreach (var item in ItemNguonVon)
            {

                if (item.IsChecked)
                {
                    var listChiPhi = LstChiPhi.Where(x => x.IIdGoiThauID == item.IIdGoiThauID && x.IIdNguonVonId == item.Id);
                    foreach (var chiPhi in listChiPhi)
                    {
                        chiPhi.STenNguonVon = item.STenNguonVon;

                        if (chiPhi.FGiaTriUSD >= 0 && chiPhi.FGiaTriVND >= 0 && chiPhi.FGiaTriEUR >= 0 && chiPhi.FGiaTriNgoaiTeKhac >= 0 && !chiPhi.IsChecked || chiPhi.FGiaTriUSD >= 0 && chiPhi.FGiaTriVND >= 0 && chiPhi.FGiaTriEUR >= 0 && chiPhi.FGiaTriNgoaiTeKhac >= 0 && chiPhi.IIdNguonVonId == item.Id)
                        {
                            _itemChiPhi.Add(chiPhi);
                        }
                    }
                    foreach (var chiphi in _itemChiPhi)
                    {

                        if (LstHangMuc != null && LstHangMuc.Any(n => n.IIdChiPhiID == chiphi.IIdChiPhiID))
                        {
                            chiphi.BHaveHangMuc = true;
                        }

                        chiphi.PropertyChanged += ChiPhi_PropertyChanged;
                    }
                    OnPropertyChanged(nameof(ItemChiPhi));
                    CaculatorChiPhi();
                }
                else
                {
                    var listChiPhiDelete = ItemChiPhi.Where(x => x.IIdGoiThauID == item.IIdGoiThauID && x.IIdNguonVonId == item.Id).ToList();
                    foreach (var chiPhi in listChiPhiDelete)
                    {
                        _itemChiPhi.Remove(chiPhi);
                    }
                    OnPropertyChanged(nameof(ItemChiPhi));
                }

                if (item.IsChecked)
                {
                    SetNguonVonByChiPhi();
                }
                else
                {
                    item.FGiaTriPheDuyetVND = 0;
                    item.FGiaTriPheDuyetUSD = 0;
                    item.FGiaTriPheDuyetEUR = 0;
                    item.FGiaTriPheDuyetNgoaiTeKhac = 0;
                }
                OnPropertyChanged(nameof(SDuToanFilter));
                CaculatorNguonVon();
            }
        }


        // Xem chi tiết hạng mục

        private void OnShowHangMucDetail()
        {
            if (SelectedChiPhi == null) return;

            List<NhDaGoiThauDetailHangMucModel> lstData = new List<NhDaGoiThauDetailHangMucModel>();
            List<NhDaGoiThauDetailHangMucModel> lstRemove = new List<NhDaGoiThauDetailHangMucModel>();
            List<NhDaGoiThauDetailHangMucModel> lstItemHangMuc = LstHangMuc.Where(
                    n => n.IIdChiPhiID == SelectedChiPhi.IIdChiPhiID).ToList();


            if (NHGoiThauDetailHangMucFilter.STenHangMuc + "" != "")
            {
                lstData = LstHangMuc.Where(
                    n => n.IIdChiPhiID == SelectedChiPhi.IIdChiPhiID &&
                    n.STenHangMuc.Contains(NHGoiThauDetailHangMucFilter.STenHangMuc, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            foreach (var item in lstItemHangMuc)
            {
                if (!item.IsHangCha && (ILoaiDuToan == 1
                    || (item.FGiaTriPheDuyetVND == 0 && SMaTienTeGoc == LoaiTienTeEnum.TypeCode.VND)
                    || (item.FGiaTriPheDuyetUSD == 0 && SMaTienTeGoc == LoaiTienTeEnum.TypeCode.USD)
                    //|| (item.FGiaTriPheDuyetEUR == 0 && SMaTienTeGoc == LoaiTienTeEnum.TypeCode.EUR)
                    //|| (item.FGiaTriPheDuyetNgoaiTeKhac == 0 && SMaTienTeGoc == LoaiTienTeEnum.TypeCode.NGOAI_TE_KHAC)
                    ))
                {
                    item.FGiaTriPheDuyetVND = item.FGiaTriVND;
                    item.FGiaTriPheDuyetUSD = item.FGiaTriUSD;
                    //item.FGiaTriPheDuyetEUR = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.EUR, SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVND);
                    //item.FGiaTriPheDuyetUSD = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, LoaiTienTeEnum.TypeCode.USD, SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVND);
                    //item.FGiaTriPheDuyetNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(LoaiTienTeEnum.TypeCode.VND, SMaTienTeQuyDoi, SMaTienTeGoc, LstTiGiaChiTiet, item.FGiaTriVND);
                }
                if (item.IsHangCha)
                {
                    if (!item.IsChecked)
                    {
                        item.FGiaTriPheDuyetVND = item.FGiaTriVND;
                        //item.FGiaTriPheDuyetEUR = 0;
                        item.FGiaTriPheDuyetUSD = 0;
                        //item.FGiaTriPheDuyetNgoaiTeKhac = 0;
                    }
                }
                if (!item.IsHangCha && !item.IIdParentID.HasValue)
                {
                    item.IsHangCha = true;
                }
                item.FGiaTriPheDuyetVND = item.FGiaTriVND;
                item.STenChiPhi = SelectedChiPhi.STenChiPhi;
                item.STenNguonVon = SelectedChiPhi.STenNguonVon;
                item.PropertyChanged += HangMuc_PropertyChanged;
                if (NHGoiThauDetailHangMucFilter.STenHangMuc + "" != "")
                {
                    if (!item.STenHangMuc.Contains(NHGoiThauDetailHangMucFilter.STenHangMuc, StringComparison.OrdinalIgnoreCase))
                    {
                        if (item.IsHangCha)
                        {
                            var lstcheck = lstData.Where(x => x.IIdParentID == item.IIdHangMucID).ToList();
                            if (lstcheck.Count == 0)
                            {
                                lstRemove.Add(item);
                            }
                        }
                        else
                        {
                            lstRemove.Add(item);
                        }
                    }
                }
            }
            if (lstRemove != null)
            {
                foreach (var item in lstRemove)
                {
                    lstItemHangMuc = lstItemHangMuc.Where(x => x.IIdHangMucID != item.IIdHangMucID).ToList();
                }
            }
            ItemHangMuc = new ObservableCollection<NhDaGoiThauDetailHangMucModel>(OrderHangMuc(lstItemHangMuc.Where(
            n => n.IIdChiPhiID == SelectedChiPhi.IIdChiPhiID).ToList()));
            HangMuc();
            OnPropertyChanged(nameof(ItemHangMuc));

            CaculatorHangMuc(SelectedChiPhi.IIdChiPhiID.Value);
        }

        //remove fillter

        private void OnRemoveFilter()
        {
            NHGoiThauDetailHangMucFilter.STenHangMuc = string.Empty;
            NHGoiThauDetailHangMucFilter.STenChiPhi = string.Empty;
            NHGoiThauDetailHangMucFilter.STenNguonVon = string.Empty;

            OnShowHangMucDetail();
        }

        // Tính giá trị nguồn vốn ban đầu. (Không dùng nữa)
        private void loadGoiThauByDuToan()
        {
            foreach (var item in ItemNguonVon)
                item.FGiaTriPheDuyetVND = item.FGiaTriVND;
            OnPropertyChanged(nameof(ItemNguonVon));
        }

        // Lưu
        private void OnSaveData(object obj)
        {
            SavedAction?.Invoke(null);
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        // Đóng
        private void OnClose(object obj)
        {
            SetProperty(ref _selectAllNguonVon, false);
            if (ItemNguonVon != null)
            {
                ItemNguonVon.Select(c => { c.IsChecked = _selectAllNguonVon; return c; }).ToList();
            }
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }
        #endregion

        #region Helper

        // ???
        private void CaculatorDetail()
        {
            ObjSubNguonVonChiPhi.FGiaTriPheDuyetUSD = ObjSumNguonVon.FGiaTriPheDuyetUSD - ObjSumChiPhi.FGiaTriPheDuyetUSD;
            ObjSubNguonVonChiPhi.FGiaTriPheDuyetVND = ObjSumNguonVon.FGiaTriPheDuyetVND - ObjSumChiPhi.FGiaTriPheDuyetVND;
            ObjSubNguonVonChiPhi.FGiaTriPheDuyetEUR = ObjSumNguonVon.FGiaTriPheDuyetEUR - ObjSumChiPhi.FGiaTriPheDuyetEUR;
            ObjSubNguonVonChiPhi.FGiaTriPheDuyetNgoaiTeKhac = ObjSumNguonVon.FGiaTriPheDuyetNgoaiTeKhac - ObjSumChiPhi.FGiaTriPheDuyetNgoaiTeKhac;
            OnPropertyChanged(nameof(ObjSubNguonVonChiPhi));
        }


        #region ==================== Event Changed ====================

        // Event change item nguồn vốn
        private void NguonVon_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaGoiThauDetailNguonVonModel)sender;
            if (args.PropertyName == nameof(NhDaGoiThauDetailNguonVonModel.IsChecked))
            {
                SDuToanFilter = string.Join(", ", ItemNguonVon.Where(n => n.IsChecked).Select(n => n.STenNguonVon));
                if (item.IsChecked)
                {
                    var listChiPhi = LstChiPhi.Where(x => x.IIdGoiThauID == item.IIdGoiThauID && x.IIdNguonVonId == item.Id);
                    foreach (var chiPhi in listChiPhi)
                    {
                        chiPhi.STenNguonVon = item.STenNguonVon;

                        if (chiPhi.FGiaTriUSD >= 0 && chiPhi.FGiaTriVND >= 0 && chiPhi.FGiaTriEUR >= 0 && chiPhi.FGiaTriNgoaiTeKhac >= 0 && !chiPhi.IsChecked || chiPhi.FGiaTriUSD >= 0 && chiPhi.FGiaTriVND >= 0 && chiPhi.FGiaTriEUR >= 0 && chiPhi.FGiaTriNgoaiTeKhac >= 0 && chiPhi.IIdNguonVonId == item.Id)
                        {
                            _itemChiPhi.Add(chiPhi);
                        }
                    }
                    foreach (var chiphi in _itemChiPhi)
                    {

                        if (LstHangMuc != null && LstHangMuc.Any(n => n.IIdChiPhiID == chiphi.IIdChiPhiID))
                        {
                            chiphi.BHaveHangMuc = true;
                        }

                        chiphi.PropertyChanged += ChiPhi_PropertyChanged;
                    }
                    OnPropertyChanged(nameof(ItemChiPhi));
                    CaculatorChiPhi();
                }
                else
                {
                    var listChiPhiDelete = ItemChiPhi.Where(x => x.IIdGoiThauID == item.IIdGoiThauID && x.IIdNguonVonId == item.Id).ToList();
                    foreach (var chiPhi in listChiPhiDelete)
                    {
                        _itemChiPhi.Remove(chiPhi);
                    }
                    OnPropertyChanged(nameof(ItemChiPhi));
                }

                if (item.IsChecked)
                {
                    SetNguonVonByChiPhi();
                }
                else
                {
                    item.FGiaTriPheDuyetVND = 0;
                    item.FGiaTriPheDuyetUSD = 0;
                    //item.FGiaTriPheDuyetEUR = 0;
                    //item.FGiaTriPheDuyetNgoaiTeKhac = 0;
                }
                OnPropertyChanged(nameof(SDuToanFilter));
                CaculatorNguonVon();
            }
        }


        // Event change item chi phí
        private void ChiPhi_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaGoiThauDetailChiPhiModel)sender;

            if (args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.IsChecked)
                || args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.FGiaTriPheDuyetUSD)
                || args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.FGiaTriPheDuyetEUR)
                || args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.FGiaTriPheDuyetVND)
                || args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.FGiaTriPheDuyetNgoaiTeKhac))
            {
                string sourceCurrency;
                double value;
                switch (args.PropertyName)
                {
                    //case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetEUR):
                    //    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //    value = item.FGiaTriPheDuyetEUR;
                    //    break;
                    case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetVND):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = item.FGiaTriPheDuyetVND;
                        break;
                    //case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetNgoaiTeKhac):
                    //    sourceCurrency = SMaTienTeQuyDoi;
                    //    value = item.FGiaTriPheDuyetNgoaiTeKhac;
                    //    break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = item.FGiaTriPheDuyetUSD;
                        break;
                }
                if (FTiGiaNhap != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;

                    //item.FGiaTriPheDuyetVND = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, SMaTienTeGoc, LstTiGiaChiTiet, value);
                    //item.FGiaTriPheDuyetEUR = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, SMaTienTeGoc, LstTiGiaChiTiet, value);
                    //item.FGiaTriPheDuyetUSD = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, SMaTienTeGoc, LstTiGiaChiTiet, value);
                    //item.FGiaTriPheDuyetNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, SMaTienTeQuyDoi, SMaTienTeGoc, LstTiGiaChiTiet, value);
                    if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                    {
                        item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                    }
                    if (args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.IsChecked) && item.IsChecked)
                    {
                        CheckedChiPhi(item, args);
                    }
                    else if (args.PropertyName == nameof(NhDaGoiThauDetailChiPhiModel.IsChecked) && !item.IsChecked)
                    {
                        //item.FGiaTriPheDuyetUSD = 0;
                        UnCheckedChiPhi(item, args);
                    }

                    SumValueChildrenChiPhi(item);
                    CaculatorChiPhi();
                    CaculatorNguonVon();
                    SetNguonVonByChiPhi();
                }
            }
        }

        // Event change item hạng mục
        private void HangMuc_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaGoiThauDetailHangMucModel)sender;
            NhDaGoiThauDetailHangMucModel objectSender = (NhDaGoiThauDetailHangMucModel)sender;
            if (args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.IsChecked))
            {
                if (args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.IsChecked) && item.IsChecked)
                {
                    CheckedHangMuc(item, args);
                }
                else if (args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.IsChecked) && !item.IsChecked)
                {
                    //item.FGiaTriPheDuyetUSD = 0;
                    UnCheckedHangMuc(item, args);
                }
                CaculatorHangMuc(item.IIdChiPhiID.Value);
                SetNguonVonByChiPhi();
            }
            else if ((args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetUSD) ||
                    args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetVND) ||
                    // args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetEUR) ||
                    args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetNgoaiTeKhac)) && item.IsChecked)
            {
                var listTiGiaChiTiet = LstTiGiaChiTiet;
                string rootCurrency = SMaTienTeGoc;
                string sourceCurrency;
                string otherCurrency = SMaTienTeQuyDoi;
                double value;
                switch (args.PropertyName)
                {
                    case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetVND):
                        sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                        value = objectSender.FGiaTriPheDuyetVND;
                        break;
                    //case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetEUR):
                    //    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                    //    value = objectSender.FGiaTriPheDuyetEUR;
                    //    break;
                    //case nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetNgoaiTeKhac):
                    //    sourceCurrency = otherCurrency;
                    //    value = objectSender.FGiaTriPheDuyetNgoaiTeKhac;
                    //    break;
                    default:
                        sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                        value = objectSender.FGiaTriPheDuyetUSD;
                        break;
                }
                if (FTiGiaNhap != null && FTiGiaNhap.HasValue && FTiGiaNhap.Value != 0)
                {
                    item.FGiaTriPheDuyetUSD = item.FGiaTriPheDuyetVND / FTiGiaNhap.Value;
                }
                //objectSender.FGiaTriPheDuyetVND = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                //objectSender.FGiaTriPheDuyetEUR = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                //objectSender.FGiaTriPheDuyetUSD = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                //objectSender.FGiaTriPheDuyetNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);

                SumValueParentHangMuc(item);
                CaculatorHangMuc(item.IIdChiPhiID.Value);
                SetNguonVonByChiPhi();

                if (objectSender.FGiaTriPheDuyetUSD > objectSender.FGiaTriUSD && args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetUSD))
                {
                    string msgConfirm = "Giá trị gói thầu đã nhập lớn hơn giá trị dự toán phê duyệt";
                    if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
                    {
                        OnRefresh();
                    }
                }
                else if (objectSender.FGiaTriPheDuyetVND > objectSender.FGiaTriVND && args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetVND))
                {
                    string msgConfirm = "Giá trị gói thầu đã nhập lớn hơn giá trị dự toán phê duyệt";
                    if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
                    {
                        OnRefresh();
                    }
                }
                //else if (SMaTienTeGoc.ToUpper() == "EUR" && objectSender.FGiaTriPheDuyetEUR > objectSender.FGiaTriEUR && args.PropertyName == nameof(NhDaGoiThauDetailHangMucModel.FGiaTriPheDuyetEUR))
                //{
                //    string msgConfirm = "Giá trị gói thầu đã nhập lớn hơn giá trị dự toán phê duyệt";
                //    if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
                //    {
                //        OnRefresh();
                //    }
                //}

            }
        }

        #endregion ==================================================

        #region ==================== Nguồn Vốn ====================

        // Tính tổng nguồn vốn
        private void CaculatorNguonVon()
        {
            ObjSumNguonVon.FGiaTriPheDuyetUSD = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetUSD);
            //ObjSumNguonVon.FGiaTriPheDuyetEUR = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetEUR);
            ObjSumNguonVon.FGiaTriPheDuyetVND = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetVND);
            //ObjSumNguonVon.FGiaTriPheDuyetNgoaiTeKhac = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

            ObjSumNguonVon.FGiaTriUSD = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriUSD);
            //ObjSumNguonVon.FGiaTriEUR = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriEUR);
            ObjSumNguonVon.FGiaTriVND = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriVND);
            //ObjSumNguonVon.FGiaTriNgoaiTeKhac = ItemNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriNgoaiTeKhac);

            ItemNguonVonView = new ObservableCollection<NhDaGoiThauDetailNguonVonModel>();
            ItemNguonVonView.Add(ObjSumNguonVon);
            OnPropertyChanged(nameof(ItemNguonVonView));
            OnPropertyChanged(nameof(ObjSumNguonVon));
            CaculatorDetail();
        }

        #endregion ==================================================

        #region ==================== Chi Phí ====================

        // Tính tổng chi phí
        private void CaculatorChiPhi()
        {
            ObjSumChiPhi.FGiaTriPheDuyetUSD = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriPheDuyetUSD);
            ObjSumChiPhi.FGiaTriPheDuyetEUR = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriPheDuyetEUR);
            ObjSumChiPhi.FGiaTriPheDuyetVND = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriPheDuyetVND);
            ObjSumChiPhi.FGiaTriPheDuyetNgoaiTeKhac = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

            ObjSumChiPhi.FGiaTriUSD = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriUSD);
            ObjSumChiPhi.FGiaTriEUR = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriEUR);
            ObjSumChiPhi.FGiaTriVND = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriVND);
            ObjSumChiPhi.FGiaTriNgoaiTeKhac = ItemChiPhi.Where(n => n.IsChecked && !n.IIdParentID.HasValue).Sum(n => n.FGiaTriNgoaiTeKhac);
            OnPropertyChanged(nameof(ObjSumChiPhi));
            CaculatorDetail();
        }

        // Tính lại giá trị của item chi phí
        private void SumValueChildrenChiPhi(NhDaGoiThauDetailChiPhiModel item)
        {
            var objParent = LstChiPhi.FirstOrDefault(x => x.IIdChiPhiID == item.IIdParentID);

            // Nếu không có thằng cha thì ko cần tính nữa.
            if (objParent == null)
            {
                return;
            }

            var lstChiPhiSelected = LstChiPhi.Where(x => x.IsChecked && x.IIdParentID == objParent.IIdChiPhiID).ToList();
            objParent.FGiaTriPheDuyetVND = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetVND);
            objParent.FGiaTriPheDuyetUSD = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetUSD);
            objParent.FGiaTriPheDuyetEUR = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetEUR);
            objParent.FGiaTriPheDuyetNgoaiTeKhac = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetNgoaiTeKhac);
        }

        // Check chi phí, thay đổi nguồn vốn.
        private void SetNguonVonByChiPhi()
        {
            var lstNguonVonSelected = ItemNguonVon.Where(x => x.IsChecked).ToList();
            foreach (var item in lstNguonVonSelected)
            {
                var lstChiPhiSelected = ItemChiPhi.Where(x => x.IsChecked && !x.IIdParentID.HasValue && x.IIdNguonVonId == item.Id).ToList();
                item.FGiaTriPheDuyetVND = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetVND);
                item.FGiaTriPheDuyetUSD = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetUSD);
                item.FGiaTriPheDuyetEUR = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetEUR);
                item.FGiaTriPheDuyetNgoaiTeKhac = lstChiPhiSelected.Sum(x => x.FGiaTriPheDuyetNgoaiTeKhac);
                OnPropertyChanged(nameof(ItemNguonVon));
            }
        }

        // Checked chi phí
        private void CheckedChiPhi(NhDaGoiThauDetailChiPhiModel item, PropertyChangedEventArgs args)
        {
            var objParent = ItemChiPhi.FirstOrDefault(n => n.IIdChiPhiID == item.IIdParentID);
            // Nếu ko tìm thấy thằng cha thì return, nếu thấy thì tính giá trị phê duyệt.
            if (objParent == null)
            {
                return;
            }

            var lstChiPhiSelected = ItemChiPhi.Where(n => n.IsChecked && n.IIdParentID == item.IIdParentID);
            objParent.FGiaTriPheDuyetUSD = lstChiPhiSelected.Sum(n => n.FGiaTriPheDuyetUSD);
            objParent.FGiaTriPheDuyetVND = lstChiPhiSelected.Sum(n => n.FGiaTriPheDuyetVND);
            objParent.FGiaTriPheDuyetEUR = lstChiPhiSelected.Sum(n => n.FGiaTriPheDuyetEUR);
            objParent.FGiaTriPheDuyetNgoaiTeKhac = lstChiPhiSelected.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

            if (objParent.IsChecked)
            {
                ChiPhi_PropertyChanged(objParent, args);
            }
            else
            {
                objParent.IsChecked = true;
            }
        }

        // Unchecked chi phí
        private void UnCheckedChiPhi(NhDaGoiThauDetailChiPhiModel item, PropertyChangedEventArgs args)
        {
            //if (LstHangMuc.Any(n => n.IIdChiPhiID == item.IIdChiPhiID))
            //{
            //    foreach (var child in LstHangMuc)
            //    {
            //        child.IsChecked = false;
            //        child.FGiaTriPheDuyetUSD = 0;
            //        child.FGiaTriPheDuyetVND = 0;
            //        child.FGiaTriPheDuyetEUR = 0;
            //        child.FGiaTriPheDuyetNgoaiTeKhac = 0;
            //    }
            //}

            var objParent = ItemChiPhi.FirstOrDefault(n => n.IIdChiPhiID == item.IIdParentID);
            if (objParent == null)
            {
                return;
            }
            var lstChiPhiSeleted = ItemChiPhi.Where(n => n.IIdParentID == item.IIdParentID && n.IsChecked);
            objParent.FGiaTriPheDuyetUSD = lstChiPhiSeleted.Sum(n => n.FGiaTriPheDuyetUSD);
            objParent.FGiaTriPheDuyetVND = lstChiPhiSeleted.Sum(n => n.FGiaTriPheDuyetVND);
            objParent.FGiaTriPheDuyetEUR = lstChiPhiSeleted.Sum(n => n.FGiaTriPheDuyetEUR);
            objParent.FGiaTriPheDuyetNgoaiTeKhac = lstChiPhiSeleted.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

            // Nếu ko có thằng con nào checked thì unchecked thằng cha.
            if (!ItemChiPhi.Any(n => n.IIdParentID == item.IIdParentID && n.IsChecked))
            {
                if (objParent.IsChecked)
                {
                    objParent.IsChecked = false;
                }
                else
                {
                    ChiPhi_PropertyChanged(objParent, args);
                }
            }

            // Nếu không có thằng con nào thì return
            if (!ItemChiPhi.Any(n => n.IIdParentID == item.IIdChiPhiID)) return;

            // Nếu có thì unchecked toàn bộ các thằng con.
            foreach (var child in ItemChiPhi.Where(n => n.IIdParentID == item.IIdChiPhiID))
                child.IsChecked = false;
        }

        #endregion ==================================================

        #region ==================== Hạng Mục ====================
        // Tính tổng hạng mục các dòng không có parent (không có dòng cha, dòng lớn nhất)
        private void CaculatorHangMuc(Guid iIdChiPhiId)
        {
            var lstHangMucSelected = ItemHangMuc.Where(n => n.IsChecked && !n.IIdParentID.HasValue);
            ObjSumHangMuc.FGiaTriPheDuyetUSD = lstHangMucSelected.Sum(n => n.FGiaTriPheDuyetUSD);
            ObjSumHangMuc.FGiaTriPheDuyetEUR = lstHangMucSelected.Sum(n => n.FGiaTriPheDuyetEUR);
            ObjSumHangMuc.FGiaTriPheDuyetVND = lstHangMucSelected.Sum(n => n.FGiaTriPheDuyetVND);
            ObjSumHangMuc.FGiaTriPheDuyetNgoaiTeKhac = lstHangMucSelected.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

            OnPropertyChanged(nameof(ObjSumHangMuc));
            SetChiPhiByHangMuc(iIdChiPhiId);
        }

        // Check hạng mục, thay đổi chi phí.
        private void SetChiPhiByHangMuc(Guid iIdChiPhiId)
        {
            var objChiPhi = ItemChiPhi.FirstOrDefault(n => n.IIdChiPhiID == iIdChiPhiId);
            if (objChiPhi == null) return;
            objChiPhi.FGiaTriPheDuyetUSD = ObjSumHangMuc.FGiaTriPheDuyetUSD;
            objChiPhi.FGiaTriPheDuyetVND = ObjSumHangMuc.FGiaTriPheDuyetVND;
            objChiPhi.FGiaTriPheDuyetEUR = ObjSumHangMuc.FGiaTriPheDuyetEUR;
            objChiPhi.FGiaTriPheDuyetNgoaiTeKhac = ObjSumHangMuc.FGiaTriPheDuyetNgoaiTeKhac;
            if (ItemHangMuc.Any(n => n.IIdChiPhiID == iIdChiPhiId && n.IsChecked))
                objChiPhi.BHaveHangMuc = true;
            //else
            //    objChiPhi.BHaveHangMuc = false;
            OnPropertyChanged(nameof(ItemChiPhi));
        }

        // Checked hạng mục
        private void CheckedHangMuc(NhDaGoiThauDetailHangMucModel item, PropertyChangedEventArgs args)
        {
            // Lấy ra toàn bộ thằng con của thằng item để checked nó
            var lstChild = ItemHangMuc.Where(x => x.IIdParentID == item.IIdHangMucID);
            foreach (var child in lstChild)
            {
                child.IsChecked = true;
                CheckedChildrenHangMuc(child);
            }

            // Checked thêm thằng cha bên trên.
            CheckedParentHangMuc(item);

            // Tính tiền cho các thằng con bên dưới
            SumValueChildrenHangMuc(item);

            // Tính tiền cho các thăng cha bên trên
            SumValueParentHangMuc(item);
        }

        // Unchecked hạng mục
        private void UnCheckedHangMuc(NhDaGoiThauDetailHangMucModel item, PropertyChangedEventArgs args)
        {
            // Lấy ra toàn bộ những thằng con rồi unchecked nó.
            var lstChild = ItemHangMuc.Where(x => x.IIdParentID == item.IIdHangMucID);
            foreach (var child in lstChild)
            {
                child.IsChecked = false;
                UncheckedChildrenHangMuc(child);
            }

            UncheckedParentHangMuc(item);
            SumValueParentHangMuc(item);
        }

        // Checked toàn bộ những thằng con bên dưới item.
        private void CheckedChildrenHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            // Tìm tất cả thằng con có (có parentID = item id).
            var lstChild = ItemHangMuc.Where(x => x.IIdParentID == item.IIdHangMucID);
            foreach (var child in lstChild)
            {
                child.IsChecked = true;
                CheckedChildrenHangMuc(child);
            }
        }

        // Checked thằng cha khi chọn thằng item.
        private void CheckedParentHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            var objParent = ItemHangMuc.FirstOrDefault(x => x.IIdHangMucID == item.IIdParentID);
            if (objParent != null)
            {
                objParent.PropertyChanged -= HangMuc_PropertyChanged;
                objParent.IsChecked = true;
                objParent.PropertyChanged += HangMuc_PropertyChanged;

                CheckedParentHangMuc(objParent);
            }
        }

        // Unchecked toàn bộ những thằng con bên dưới item.
        private void UncheckedChildrenHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            // Get list children
            var lstChild = ItemHangMuc.Where(x => x.IIdParentID == item.IIdHangMucID);
            foreach (var child in lstChild)
            {
                child.IsChecked = false;
                UncheckedChildrenHangMuc(child);
            }
        }

        // Unchecked toàn bộ những thằng cha bên trên nếu những thằng cùng cấp tiem không còn thằng nào đang checked.
        private void UncheckedParentHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            var objParent = ItemHangMuc.FirstOrDefault(x => x.IIdHangMucID == item.IIdParentID);
            // Nếu không có thằng cha thì khỏi check tiếp. Nếu có thằng cha thì check xem nếu các thằng con của thằng cha này (cùng cấp với item) mà không còn thằng nào đang checked thì unchecked thằng cha đi).
            if (objParent != null)
            {
                var isAChecked = false;
                // Lấy những thằng con của thằng parent (cùng cấp với item)
                var lstChild = ItemHangMuc.Where(x => x.IIdParentID == objParent.IIdHangMucID);
                foreach (var child in lstChild)
                {
                    // Nếu có bất kì thằng con nào đang checked thì break.
                    if (child.IsChecked)
                    {
                        isAChecked = true;
                        break;
                    }
                }

                // Nếu không có thằng con (cùng cấp với item) nào đang checked thì unchecked thằng cha.
                if (!isAChecked)
                {
                    objParent.IsChecked = false;
                    UncheckedParentHangMuc(objParent);
                }
            }
        }

        // Tính ra thằng item từ những thằng dưới cấp item.
        private NhDaGoiThauDetailHangMucModel SumValueChildrenHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            // Lấy những thằng con của item
            var lstChildren = ItemHangMuc.Where(x => x.IIdParentID == item.IIdHangMucID && x.IsChecked);

            if (lstChildren.Any())
            {
                foreach (var child in lstChildren)
                {
                    // Get value children
                    SumValueChildrenHangMuc(child);
                }

                // Set value item
                item.FGiaTriPheDuyetVND = lstChildren.Sum(x => x.FGiaTriPheDuyetVND);
                item.FGiaTriPheDuyetUSD = lstChildren.Sum(x => x.FGiaTriPheDuyetUSD);
                item.FGiaTriPheDuyetEUR = lstChildren.Sum(x => x.FGiaTriPheDuyetEUR);
                item.FGiaTriPheDuyetNgoaiTeKhac = lstChildren.Sum(x => x.FGiaTriPheDuyetNgoaiTeKhac);
            }

            return item;
        }

        // Tỉnh tổng của những thằng từ cấp item trở lên.
        private void SumValueParentHangMuc(NhDaGoiThauDetailHangMucModel item)
        {
            // Lấy ra thằng cha
            var objParent = ItemHangMuc.FirstOrDefault(n => n.IIdHangMucID == item.IIdParentID);

            // Nếu tìm thấy thằng cha thì tính tổng cho thằng cha.
            if (objParent != null)
            {
                // Lấy ra những thằng con checked của thằng cha (cùng cấp với item)
                var lstChild = ItemHangMuc.Where(x => x.IIdParentID == objParent.IIdHangMucID && x.IsChecked);
                objParent.FGiaTriPheDuyetUSD = lstChild.Sum(n => n.FGiaTriPheDuyetUSD);
                objParent.FGiaTriPheDuyetVND = lstChild.Sum(n => n.FGiaTriPheDuyetVND);
                objParent.FGiaTriPheDuyetEUR = lstChild.Sum(n => n.FGiaTriPheDuyetEUR);
                objParent.FGiaTriPheDuyetNgoaiTeKhac = lstChild.Sum(n => n.FGiaTriPheDuyetNgoaiTeKhac);

                // Sau khi tính xong thì tiếp tục check đến thằng cha.
                SumValueParentHangMuc(objParent);
            }
        }

        private List<NhDaGoiThauDetailHangMucModel> OrderHangMuc(List<NhDaGoiThauDetailHangMucModel> lstData)
        {
            if (lstData == null) return new List<NhDaGoiThauDetailHangMucModel>();
            List<NhDaGoiThauDetailHangMucModel> results = new List<NhDaGoiThauDetailHangMucModel>();
            foreach (var item in lstData.Where(n => !n.IIdParentID.HasValue).OrderBy(n => n.SMaOrder))
            {
                results.AddRange(RecursiveHangMuc(item, lstData));
            }
            return results;
        }

        private List<NhDaGoiThauDetailHangMucModel> RecursiveHangMuc(NhDaGoiThauDetailHangMucModel item, List<NhDaGoiThauDetailHangMucModel> lstData)
        {
            List<NhDaGoiThauDetailHangMucModel> results = new List<NhDaGoiThauDetailHangMucModel>();
            var lstChild = lstData.Where(n => n.IIdParentID == item.IIdHangMucID);
            item.IsHangCha = lstChild.Count() != 0;
            results.Add(item);
            if (!item.IsHangCha) return results;

            foreach (var child in lstChild)
            {
                results.AddRange(RecursiveHangMuc(child, lstData));
            }
            return results;
        }

        #endregion ==================================================

        #endregion
    }
}

