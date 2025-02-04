using AutoMapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.KHLuaChonNhaThau;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.KHLuaChonNhaThau
{
    public class KHLuaChonNhaThauDialogViewModel : DialogAttachmentViewModelBase<KHLCNhaThauModel>
    {
        #region Private
        private readonly INsDonViService _donviService;
        private readonly IVdtQddtKhlcnhaThauService _service;
        private readonly IDmChuDauTuService _chudautuService;
        private readonly IVdtDaGoiThauService _goithauService;
        private readonly IProjectManagerService _duanService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsNguonVon;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsChiPhi;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsHangMuc;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsNguonVonByGoiThau;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsChiPhiByGoiThau;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsHangMucByGoiThau;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsNguonVonParent;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsChiPhiParent;
        private List<VdtKhlcNhaThauGoiThauDetailModel> _itemsHangMucParent;
        private ICollectionView _allNguonVonView;
        private ICollectionView _allChiPhiView;
        #endregion

        #region Public
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_KH_LUA_CHON_NHA_THAU_DIALOG_MODIFY;
        public override string Name => "THÔNG TIN KẾ HOẠCH LỰA CHỌN NHÀ THẦU";
        public override string Title => "Kế hoạch lựa chọn nhà thầu";
        public override string Description
            => string.Format("{0} kế hoạch lựa chọn nhà thầu", (IsDetail ? "Chi tiết" : (!IsAdd ? "Cập nhật" : (Model.IIdParentId.IsNullOrEmpty() ? "Thêm mới" : "Điều chỉnh"))));
        public override Type ContentType => typeof(KHLuaChonNhaThauDiaLog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_KH_LUACHON_NHATHAU;
        public KHLuaChonNhaThauDetailViewModel KHLuaChonNhaThauDetailViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }
        public bool BIsDieuChinh => IsAdd && Model.IIdParentId.HasValue && !IsDetail;
        public bool IsEdit => !IsAdd || BIsDieuChinh || IsDetail;
        public string SHeaderGiaGoiThau => !BIsDieuChinh ? "Giá gói thầu" : "Giá gói thầu sau điều chỉnh";
        #endregion

        #region Items
        private string _sTitleDuToanName;
        public string STitleDuToanName
        {
            get => _sTitleDuToanName;
            set => SetProperty(ref _sTitleDuToanName, value);
        }

        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private double _fTongGoiThau;
        public double FTongGoiThau
        {
            get => _fTongGoiThau;
            set => SetProperty(ref _fTongGoiThau, value);
        }

        private ObservableCollection<VdtDaGoiThauModel> _itemsGoiThau;
        public ObservableCollection<VdtDaGoiThauModel> ItemsGoiThau
        {
            get => _itemsGoiThau;
            set => SetProperty(ref _itemsGoiThau, value);
        }

        private VdtDaGoiThauModel _selectedGoiThau;
        public VdtDaGoiThauModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set => SetProperty(ref _selectedGoiThau, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCanCu;
        public ObservableCollection<ComboboxItem> ItemsLoaiCanCu
        {
            get => _itemsLoaiCanCu;
            set => SetProperty(ref _itemsLoaiCanCu, value);
        }

        private ComboboxItem _selectedLoaiCanCu;
        public ComboboxItem SelectedLoaiCanCu
        {
            get => _selectedLoaiCanCu;
            set
            {
                if (SetProperty(ref _selectedLoaiCanCu, value))
                {
                    DisplayFileAttach();
                    LoadDuAn();
                    LoadChungTuByLoaiChungTu();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                if (SetProperty(ref _selectedDonVi, value))
                    LoadDuAn();
            }
        }

        private ObservableCollection<VdtDaDuAn> _itemsDuAn;
        public ObservableCollection<VdtDaDuAn> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private VdtDaDuAn _selectedDuAn;
        public VdtDaDuAn SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                SetProperty(ref _selectedDuAn, value);
                FTongMucDauTu = 0;
                SelectedChuDauTu = null;
                if (SelectedDuAn != null)
                {
                    SelectedChuDauTu = ItemsChuDauTu.FirstOrDefault(n => n.ValueItem == _selectedDuAn.IIdMaChuDauTuId);
                    FTongMucDauTu = _selectedDuAn.FTongMucDauTu ?? 0;
                    OnPropertyChanged(nameof(SelectedChuDauTu));
                }
                LoadChungTuByLoaiChungTu();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsChuDauTu;
        public ObservableCollection<ComboboxItem> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private ComboboxItem _selectedChuDauTu;
        public ComboboxItem SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set => SetProperty(ref _selectedChuDauTu, value);
        }

        private ObservableCollection<ComboboxItem> _itemsHinhThuc;
        public ObservableCollection<ComboboxItem> ItemsHinhThuc
        {
            get => _itemsHinhThuc;
            set => SetProperty(ref _itemsHinhThuc, value);
        }

        private ObservableCollection<ComboboxItem> _itemsPhuongThuc;
        public ObservableCollection<ComboboxItem> ItemsPhuongThuc
        {
            get => _itemsPhuongThuc;
            set => SetProperty(ref _itemsPhuongThuc, value);
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiHopDong;
        public ObservableCollection<ComboboxItem> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }

        private ObservableCollection<VdtDuToanModel> _duToanItems;
        public ObservableCollection<VdtDuToanModel> DuToanItems
        {
            get => _duToanItems;
            set => SetProperty(ref _duToanItems, value);
        }

        private ObservableCollection<VdtDaGoiThauModel> _itemsGoiThauGoc;
        public ObservableCollection<VdtDaGoiThauModel> ItemsGoiThauGoc
        {
            get => _itemsGoiThauGoc;
            set => SetProperty(ref _itemsGoiThauGoc, value);
        }

        private ObservableCollection<VdtKhlcNhaThauCanCuModel> _itemsCanCu;
        public ObservableCollection<VdtKhlcNhaThauCanCuModel> ItemsCanCu
        {
            get => _itemsCanCu;
            set => SetProperty(ref _itemsCanCu, value);
        }

        private VdtKhlcNhaThauCanCuModel _selectedCanCu;
        public VdtKhlcNhaThauCanCuModel SelectedCanCu
        {
            get => _selectedCanCu;
            set
            {
                if (SetProperty(ref _selectedCanCu, value))
                {
                    SetValueBySelectCanCu();
                }
            }
        }

        private ObservableCollection<VdtDuToanModel> _itemsDuToanFilter;
        public ObservableCollection<VdtDuToanModel> ItemsDuToanFilter
        {
            get => _itemsDuToanFilter;
            set => SetProperty(ref _itemsDuToanFilter, value);
        }

        private bool _selectAllDuToanFilter;
        public bool SelectAllDuToanFilter
        {
            get => (ItemsDuToanFilter == null || !ItemsDuToanFilter.Any()) ? false : ItemsDuToanFilter.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuToanFilter, value);
                if (ItemsDuToanFilter != null)
                {
                    ItemsDuToanFilter.Select(c => { c.IsChecked = _selectAllDuToanFilter; return c; }).ToList();
                }
            }
        }

        private ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel> _itemsnguonVonAll;
        public ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel> ItemsNguonVonAll
        {
            get => _itemsnguonVonAll;
            set => SetProperty(ref _itemsnguonVonAll, value);
        }

        private ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel> _itemsChiPhiAll;
        public ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel> ItemsChiPhiAll
        {
            get => _itemsChiPhiAll;
            set
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _itemsChiPhiAll, value);
                OnPropertyChanged(nameof(ItemsChiPhiAll));
            }

        }

        private string _sDuToanFilter;
        public string SDuToanFilter
        {
            get => _sDuToanFilter;
            set => SetProperty(ref _sDuToanFilter, value);
        }

        private double _fTongMucDauTu;
        public double FTongMucDauTu
        {
            get => _fTongMucDauTu;
            set => SetProperty(ref _fTongMucDauTu, value);
        }

        private bool _isDisplayFileAttachment;
        public bool IsDisplayFileAttachment
        {
            get => _isDisplayFileAttachment;
            set => SetProperty(ref _isDisplayFileAttachment, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand AddGoiThauCommand { get; set; }
        public RelayCommand DeleteGoiThauCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand SaveDataCommand { get; }
        public RelayCommand ShowGoiThauCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand DetailsThauCommand { get; }
        #endregion

        public KHLuaChonNhaThauDialogViewModel(
            IVdtQddtKhlcnhaThauService service,
            INsDonViService donviService,
            IDmChuDauTuService chudautuService,
            IVdtDaGoiThauService goithauService,
            IProjectManagerService duanService,
            ISessionService sessionService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IMapper mapper,
            KHLuaChonNhaThauDetailViewModel khLuaChonNhaThauDetailViewModel,
            AttachmentViewModel attachmentViewModel) : base(mapper, storageServiceFactory, attachService)
        {
            _service = service;
            _donviService = donviService;
            _chudautuService = chudautuService;
            _goithauService = goithauService;
            _duanService = duanService;
            _sessionService = sessionService;
            _mapper = mapper;
            AttachmentViewModel = attachmentViewModel;

            KHLuaChonNhaThauDetailViewModel = khLuaChonNhaThauDetailViewModel;
            KHLuaChonNhaThauDetailViewModel.ParentPage = this;

            AddGoiThauCommand = new RelayCommand(obj => OnAddGoiThau());
            DeleteGoiThauCommand = new RelayCommand(obj => OnDeleteGoiThau());
            //RefreshCommand = new RelayCommand(obj => OnRefresh());
            SaveDataCommand = new RelayCommand(obj => OnSaveData(obj));
            ShowGoiThauCommand = new RelayCommand(obj => OnShowGoiThau());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj));
            DetailsThauCommand = new RelayCommand(obj => OnShowGoiThau());
        }

        #region Event
        public override void Init()
        {
            SDuToanFilter = string.Empty;
            ItemsDuToanFilter = new ObservableCollection<VdtDuToanModel>();
            ItemsNguonVonAll = new ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>();
            ItemsChiPhiAll = new ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>();

            LoadChuDauTu();
            LoadChungTu();
            LoadDonVi();
            GetHinhThucChonNhaThau();
            GetPhuongThucDauThau();
            GetLoaiHopDong();
            LoadData();
            GetGoiThau();
            GetGoiThauGoc();
            GetParentDetail();
        }

        public override void LoadData(params object[] args)
        {
            BIsReadOnly = IsDetail;
            OnPropertyChanged(nameof(BIsReadOnly));
            LoadAttach();
            LoadDataDefault();
        }

        private void OnAddGoiThau()
        {
            VdtDaGoiThauModel objNew = new VdtDaGoiThauModel();
            objNew.Id = Guid.NewGuid();
            objNew.IsAdded = true;
            objNew.PropertyChanged += GoiThau_PropertyChanged;
            ItemsGoiThau.Add(objNew);
        }

        private void OnDeleteGoiThau()
        {
            if (SelectedGoiThau != null)
                SelectedGoiThau.IsDeleted = !SelectedGoiThau.IsDeleted;
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void OnSaveData(object obj)
        {
            if (!ValidateForm()) return;
            if (!ValidateGoiThau()) return;
            var objKhlcNhaThau = SaveKhlcNhaThauData();
            SaveGoiThau(objKhlcNhaThau.Id);
            SaveDetailGoiThau();
            MessageBoxHelper.Info(Resources.MsgSaveDone);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }
        #endregion

        #region Helper
        private VdtQddtKhlcnhaThau SaveKhlcNhaThauData()
        {
            VdtQddtKhlcnhaThau objNhaThau = new VdtQddtKhlcnhaThau();
            if (Model.Id != Guid.Empty)
            {
                objNhaThau = _service.Find(Model.Id);
                objNhaThau.DDateUpdate = DateTime.Now;
                objNhaThau.SUserUpdate = _sessionService.Current.Principal;
            }
            else
            {
                objNhaThau.DDateCreate = DateTime.Now;
                objNhaThau.SUserCreate = _sessionService.Current.Principal;
                if (Model.IIdParentId.HasValue)
                {
                    objNhaThau.IIdParentId = Model.IIdParentId;
                    objNhaThau.BIsGoc = false;
                    if (Model.IIdLcNhaThauGocId == Guid.Empty)
                    {
                        objNhaThau.IIdLCNhaThauGocID = Model.IIdParentId;
                    }
                    else
                        objNhaThau.IIdLCNhaThauGocID = Model.IIdLcNhaThauGocId;
                    var objParent = _service.Find(Model.IIdParentId.Value);
                    if (objParent != null)
                    {
                        objParent.BActive = false;
                        _service.UpdateLCNT(objParent);
                    }
                }
                else
                {
                    objNhaThau.BIsGoc = true;
                    objNhaThau.IIdLCNhaThauGocID = objNhaThau.Id;
                }
                objNhaThau.BActive = true;

            }

            switch (SelectedLoaiCanCu.ValueItem)
            {
                case LoaiKHLCNTType.DU_TOAN:
                    objNhaThau.IIdDuToanId = SelectedCanCu.Id;
                    break;
                case LoaiKHLCNTType.QDDT:
                    objNhaThau.IIdQDDauTuID = SelectedCanCu.Id;
                    break;
                case LoaiKHLCNTType.CHU_TRUONG_DAU_TU:
                    objNhaThau.IIdChuTruongDauTuID = SelectedCanCu.Id;
                    break;
            }

            objNhaThau.SSoQuyetDinh = Model.SSoQuyetDinh;
            objNhaThau.DNgayQuyetDinh = Model.DNgayQuyetDinh;
            objNhaThau.IIdDonViQuanLyId = Guid.Parse(SelectedDonVi.HiddenValue);
            objNhaThau.IIdMaDonViQuanLy = SelectedDonVi.ValueItem;
            objNhaThau.IIdDuAnId = SelectedDuAn.Id;
            objNhaThau.SMoTa = Model.SMoTa;
            objNhaThau.BActive = true;

            if (Model.Id == Guid.Empty)
                _service.Insert(objNhaThau, _sessionService.Current.Principal);
            else
                _service.Update(objNhaThau, _sessionService.Current.Principal);

            SaveAttachment(objNhaThau.Id);
            return objNhaThau;
        }

        private bool SaveGoiThau(Guid iIdKhlcNhaThauId)
        {
            List<VdtDaGoiThau> lstGoiThauAdd = new List<VdtDaGoiThau>();
            List<VdtDaGoiThau> lstGoiThauUpdate = new List<VdtDaGoiThau>();
            List<VdtDaGoiThau> lstGoiThauDelete = new List<VdtDaGoiThau>();
            List<Guid> lstIdDeleteDetail = ItemsGoiThau.Where(n => n.IsDeleted).Select(n => n.Id).ToList();

            foreach (var item in ItemsGoiThau)
            {
                if (item.IsAdded && !item.IsDeleted)
                    lstGoiThauAdd.Add(SetDataGoiThau(iIdKhlcNhaThauId, item));
                else if (!item.IsAdded && !item.IsDeleted)
                    lstGoiThauUpdate.Add(SetDataGoiThau(iIdKhlcNhaThauId, item));
                else if (!item.IsAdded && item.IsDeleted)
                    lstGoiThauDelete.Add(SetDataGoiThau(iIdKhlcNhaThauId, item));
            }
            Boolean check = false;
            if (lstGoiThauAdd.Count != 0)
                _goithauService.AddRange(lstGoiThauAdd);
            if (lstGoiThauUpdate.Count != 0)
            {
                foreach (var item in lstGoiThauUpdate)
                {
                    if (_goithauService.FindByKhlcNhaThauId(Guid.Parse(item.IIdKhlcnhaThau.ToString())).Count() != 0)
                    {
                        _goithauService.UpdateRange(lstGoiThauUpdate);
                        check = true;
                        break;
                    }
                }
            }
            if (lstGoiThauUpdate.Count != 0 && check == false)
            {
                foreach (var item in lstGoiThauUpdate)
                {
                    item.Id = Guid.Empty;
                }
                _goithauService.AddRange(lstGoiThauUpdate);
            }
            if (lstIdDeleteDetail != null && lstIdDeleteDetail.Count != 0)
                DeleteGoiThauDetailInGoiThauDelete(lstIdDeleteDetail);
            if (lstGoiThauDelete.Count != 0)
                _goithauService.DeleteRange(lstGoiThauDelete);

            if (ItemsGoiThauGoc != null)
            {
                List<VdtDaGoiThau> lstGoiThauGoc = new List<VdtDaGoiThau>();
                foreach (var item in ItemsGoiThauGoc)
                {
                    var obj = _goithauService.FindById(item.Id);
                    if (obj != null)
                    {
                        obj.BActive = false;
                        lstGoiThauGoc.Add(obj);
                    }
                }
                if (lstGoiThauGoc.Count != 0)
                    _goithauService.UpdateRange(lstGoiThauGoc, false);
            }
            return true;
        }

        private void DeleteGoiThauDetailInGoiThauDelete(List<Guid> iIdGoiThaus)
        {
            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauId.Value)).ToList();
            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauId.Value)).ToList();
            _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId.HasValue && !iIdGoiThaus.Contains(n.IIdGoiThauId.Value)).ToList();
        }

        // update lai id goi thầu khi điều chỉnh, vì đã tạo ra các goi thầu mới nên phải gán idgoithau trong các mảng _items(...) ứng với id mới
        private void updateAllGoiThauIdWhenDieuChinh()
        {
            _itemsNguonVonByGoiThau.ForEach(nv =>
            {
                if (nv.IIdGoiThauId != null)
                {
                    nv.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == nv.IIdGoiThauId)?.Id;
                }
            });
            _itemsChiPhiByGoiThau.ForEach(cp =>
            {
                if (cp.IIdGoiThauId != null)
                {
                    cp.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == cp.IIdGoiThauId)?.Id;
                }
            });
            _itemsHangMucByGoiThau.ForEach(hm =>
            {
                if (hm.IIdGoiThauId != null)
                {
                    hm.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == hm.IIdGoiThauId)?.Id;
                }
            });
        }

        private void SaveDetailGoiThau()
        {
            if (BIsDieuChinh)
            {
                updateAllGoiThauIdWhenDieuChinh();
            }
            SaveNguonVon();
            Dictionary<string, Guid> dicGoiThauChiPhi = new Dictionary<string, Guid>();
            SaveChiPhi(ref dicGoiThauChiPhi);
            SaveHangMuc(dicGoiThauChiPhi);
        }

        private void SaveNguonVon()
        {
            var lstNguonVon = _itemsNguonVonByGoiThau.Where(n => n.IsChecked).Select(n => new VdtDaGoiThauNguonVon()
            {
                IIdGoiThauId = n.IIdGoiThauId,
                FTienGoiThau = n.FGiaTriGoiThau,
                IIdNguonVonId = n.IIdNguonVonId
            }).ToList();
            _goithauService.AddRangeGoiThauNguonVon(lstNguonVon);
        }

        private void SaveChiPhi(ref Dictionary<string, Guid> dicGoiThauChiPhi)
        {
            var lstChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IsChecked || SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU).Select(n => new VdtDaGoiThauChiPhi()
            {
                IIdGoiThauId = n.IIdGoiThauId,
                IIdChiPhiId = n.IIdChiPhiId,
                FTienGoiThau = n.FGiaTriGoiThau
            }).ToList();
            _goithauService.AddRangeGoiThauChiPhi(lstChiPhi);

            if (SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
            {
                List<VdtDmDuAnChiPhi> lstUpdate = new List<VdtDmDuAnChiPhi>();
                Dictionary<Guid, Guid> dicUpdate = new Dictionary<Guid, Guid>();
                foreach (var item in _itemsChiPhi.Where(n => !n.IsAdd))
                {
                    if (dicUpdate.ContainsKey(item.IIdChiPhiId.Value)) continue;
                    dicUpdate.Add(item.IIdChiPhiId.Value, item.IIdChiPhiId.Value);
                    lstUpdate.Add(new VdtDmDuAnChiPhi() { Id = item.IIdChiPhiId.Value, STenChiPhi = item.SNoiDung });
                }
                _duanService.UpdateDmDuAnChiPhi(lstUpdate);
            }

            var lstDmChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IsAdd);
            if (lstDmChiPhi == null) return;

            Dictionary<Guid, VdtDmDuAnChiPhi> dicNew = new Dictionary<Guid, VdtDmDuAnChiPhi>();
            List<VdtDmDuAnChiPhi> lstAdd = new List<VdtDmDuAnChiPhi>();
            foreach (var item in lstDmChiPhi)
            {
                if (dicNew.ContainsKey(item.IIdChiPhiId.Value)) continue;
                dicNew.Add(item.IIdChiPhiId.Value, new VdtDmDuAnChiPhi());
                lstAdd.Add(new VdtDmDuAnChiPhi()
                {
                    Id = item.IIdChiPhiId.Value,
                    STenChiPhi = item.SNoiDung,
                    IIdChiPhiParent = item.IIdParentId,
                    IIdChiPhi = item.IIdChiPhiGocId
                });
            }

            _duanService.InsertDmDuAnChiPhi(lstAdd);
        }

        private void SaveHangMuc(Dictionary<string, Guid> dicGoiThauChiPhi)
        {
            var lstHangMuc = _itemsHangMucByGoiThau.Where(n => n.IsChecked).Select(n => new VdtDaGoiThauHangMuc()
            {
                IIdGoiThauId = n.IIdGoiThauId,
                IIdHangMucId = n.IIdHangMucId,
                IIDChiPhiID = n.IIdChiPhiId,
                FTienGoiThau = n.FGiaTriGoiThau
            }).ToList();
            _goithauService.AddRangeGoiThauHangMuc(lstHangMuc);
        }

        private VdtDaGoiThau SetDataGoiThau(Guid iIdKhlcNhaThauId, VdtDaGoiThauModel data)
        {
            VdtDaGoiThau item = new VdtDaGoiThau();
            item.Id = data.Id;
            item.IIdDuAnId = SelectedDuAn.Id;
            item.STenGoiThau = data.STenGoiThau;
            item.SHinhThucChonNhaThau = data.SHinhThucChonNhaThau;
            item.SPhuongThucDauThau = data.SPhuongThucDauThau;
            item.DBatDauChonNhaThau = data.DBatDauChonNhaThau;
            item.SHinhThucHopDong = data.SHinhThucHopDong;
            item.SThoiGianThucHien = data.SThoiGianThucHien;
            if (data.SelectedGoiThauParent != null)
            {
                item.IIdParentId = data.SelectedGoiThauParent.Id;
                item.IIdGoiThauGocId = data.IIdGoiThauGocId;
                item.BIsGoc = false;
            }
            else
            {
                item.IIdGoiThauGocId = data.Id;
                item.BIsGoc = true;
            }
            item.BActive = true;
            item.FTienTrungThau = data.FTienTrungThau;
            item.DDateCreate = DateTime.Now;
            item.SUserCreate = _sessionService.Current.Principal;
            item.IIdKhlcnhaThau = iIdKhlcNhaThauId;
            return item;
        }

        private void DuToanFilter_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (VdtDuToanModel)sender;
            if (args.PropertyName != nameof(VdtDuToanModel.IsChecked)) return;
            SDuToanFilter = string.Join(", ", ItemsDuToanFilter.Where(n => n.IsChecked).Select(n => n.SSoQuyetDinh));
            _allNguonVonView.Refresh();
            _allChiPhiView.Refresh();
            OnPropertyChanged(nameof(SDuToanFilter));
            OnPropertyChanged(nameof(ItemsChiPhiAll));
            OnPropertyChanged(nameof(ItemsNguonVonAll));
        }

        private bool ValidateForm()
        {
            List<string> lstError = new List<string>();

            if (SelectedLoaiCanCu == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Căn cứ"));
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Số quyết định"));
            if (!Model.DNgayQuyetDinh.HasValue)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Ngày quyết định"));
            if (SelectedDonVi == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Đơn vị"));
            if (SelectedDuAn == null)
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Dự án"));
            if (SelectedCanCu == null && SelectedLoaiCanCu != null)
            {
                switch (SelectedLoaiCanCu.ValueItem)
                {
                    case LoaiKHLCNTType.DU_TOAN:
                        lstError.Add(string.Format(Resources.MsgErrorRequire, LoaiKHLCNTTypeName.DU_TOAN));
                        break;
                    case LoaiKHLCNTType.QDDT:
                        lstError.Add(string.Format(Resources.MsgErrorRequire, LoaiKHLCNTTypeName.QDDT));
                        break;
                    case LoaiKHLCNTType.CHU_TRUONG_DAU_TU:
                        lstError.Add(string.Format(Resources.MsgErrorRequire, LoaiKHLCNTTypeName.CHU_TRUONG_DAU_TU));
                        break;
                }
            }
            if (ItemsGoiThau == null || !ItemsGoiThau.Any(n => !n.IsDeleted))
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Gói thầu"));

            if (!string.IsNullOrEmpty(Model.SSoQuyetDinh) && _service.IsExistSoQuyetDinh(Model.SSoQuyetDinh, Model.Id))
                lstError.Add(Resources.MsgTrungSoQuyetDinhs);
                
            if (lstError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstError));
                return false;
            }
            return true;
        }

        private bool ValidateGoiThau()
        {
            List<string> messErrors = new List<string>();
            if (!ItemsGoiThau.Any(n => !n.IsDeleted))
                messErrors.Add(string.Format(Resources.MsgErrorDataEmpty, "gói thầu"));

            if (messErrors.Count > 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            int index = 1;
            foreach (var item in ItemsGoiThau.Where(n => !n.IsDeleted))
            {
                if (string.IsNullOrEmpty(item.STenGoiThau))
                    messErrors.Add(string.Format("Dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Tên gói thầu")));
                /*
                if (string.IsNullOrEmpty(item.SHinhThucChonNhaThau))
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Hình thức chọn nhà thầu")));
                // nếu hình thức lựa chọn nhà thầu là Chỉ định nhà thầu rút gọn thì k cần nhập phương thức lựa chọn nhà thầu
                if (string.IsNullOrEmpty(item.SPhuongThucDauThau) && item.SHinhThucChonNhaThau != HTChonNhaThauTypeName.HT_9) 
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Phương thức chọn nhà thầu")));
                if (string.IsNullOrEmpty(item.SHinhThucHopDong))
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Loại hợp đồng")));
                if (string.IsNullOrEmpty(item.SThoiGianThucHien))
                    messErrors.Add(string.Format("dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorRequire, "Thời gian thực hiện")));
                */

                if ((item.FTienTrungThau ?? 0) == 0)
                    messErrors.Add(string.Format("Dòng thứ {0} : {1}", index, string.Format(Resources.MsgErrorGiaTriGoiThauLessThanZero)));
                index++;
            }
            if (messErrors.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", messErrors));
                return false;
            }
            return true;
        }

        private void GoiThau_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (VdtDaGoiThauModel)sender;
            if (args.PropertyName != nameof(VdtDaGoiThauModel.FTienTrungThau)
                && args.PropertyName != nameof(VdtDaGoiThauModel.IsDeleted)
                && args.PropertyName != nameof(VdtDaGoiThauModel.SelectedGoiThauParent)) return;
            if (args.PropertyName == nameof(VdtDaGoiThauModel.SelectedGoiThauParent))
            {
                selected.FGiaTriTruocDieuChinh = selected.SelectedGoiThauParent.FTienTrungThau ?? 0;
                return;
            }
            FTongGoiThau = ItemsGoiThau.Where(n => !n.IsDeleted).Sum(n => (n.FTienTrungThau ?? 0));
            OnPropertyChanged(nameof(FTongGoiThau));
        }

        private void ChungTu_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var selected = (VdtKhlcNhaThauCanCuModel)sender;
            if (args.PropertyName != nameof(VdtKhlcNhaThauCanCuModel.IsChecked)) return;
            foreach (var item in ItemsCanCu)
            {
                if (item.Id == selected.Id)
                {
                    FTongMucDauTu = selected.FTongGiaTriPheDuyet;
                    OnPropertyChanged(nameof(FTongMucDauTu));
                }

                foreach (var itemDuToan in ItemsDuToanFilter)
                {
                    if (selected.IsChecked && item.Id != selected.Id)
                    {
                        item.IsChecked = false;
                    }
                    if (item.IsChecked && item.Id == itemDuToan.Id)
                    {
                        itemDuToan.IsChecked = true;
                        ItemsGoiThau = _mapper.Map<ObservableCollection<VdtDaGoiThauModel>>(_goithauService.FindByKhlcNhaThauId(selected.Id));
                        List<VdtDaGoiThauModel> lstObject = new List<VdtDaGoiThauModel>();
                        ItemsGoiThau = new ObservableCollection<VdtDaGoiThauModel>(lstObject);
                        _itemsChiPhiByGoiThau = null;
                        FTongGoiThau = 0;
                    }
                    if (!item.IsChecked && item.Id == itemDuToan.Id)
                    {
                        itemDuToan.IsChecked = false;
                    }
                }
            }
                
            OnPropertyChanged(nameof(ItemsCanCu));
        }

        private void LoadDataDefault()
        {
            STitleDuToanName = "DANH SÁCH CHỨNG TỪ";
            if (Model.Id != Guid.Empty)
            {
                if (ItemsLoaiCanCu != null)
                {
                    if (Model.IIdQdDauTuId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.QDDT);
                    if (Model.IIdChuTruongDauTuId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU);
                    if (Model.IIdDuToanId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.DU_TOAN);
                }
                OnPropertyChanged(nameof(SelectedLoaiCanCu));
                // update lại cho đúng
                SetValueBySelectCanCu();
                _itemsNguonVonByGoiThau = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauNguonVonByKhlcNhaThauId(Model.Id));
                _itemsChiPhiByGoiThau = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauChiPhiByKhlcNhaThauId(Model.Id));
                //_itemsHangMucByGoiThau = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByKhlcNhaThauId(Model.Id));
            }
            else
            {
                Model.SSoQuyetDinh = string.Empty;
                Model.DNgayQuyetDinh = DateTime.Now;

                if (BIsDieuChinh)
                {
                    if (Model.IIdDuToanId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.DU_TOAN);
                    if (Model.IIdChuTruongDauTuId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU);
                    if (Model.IIdQdDauTuId.HasValue) SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == LoaiKHLCNTType.QDDT);
                }

                _itemsNguonVonByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
                _itemsChiPhiByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
                _itemsHangMucByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            }
        }

        private void LoadDuAn()
        {
            if (SelectedDonVi == null || SelectedLoaiCanCu == null)
            {
                ItemsDuAn = null;
                SelectedDuAn = null;
                return;
            }
            var lstDuAn = _duanService.FindDuAnByLoaiChungTu(SelectedLoaiCanCu.ValueItem, SelectedDonVi.ValueItem, IsAdd);
            lstDuAn = lstDuAn.Select(n => { n.STenDuAn = string.Format("{0}-{1}", n.SMaDuAn, n.STenDuAn); return n; });
            ItemsDuAn = new ObservableCollection<VdtDaDuAn>(lstDuAn);
            if (Model != null && Model.IIdDuAnId.HasValue)
            {
                SelectedDuAn = ItemsDuAn.FirstOrDefault(n => n.Id == Model.IIdDuAnId);
            }
        }

        private void LoadChungTu()
        {
            List<ComboboxItem> lstLoaiCanCu = new List<ComboboxItem>();
            lstLoaiCanCu.Add(new ComboboxItem() { ValueItem = LoaiKHLCNTType.DU_TOAN, DisplayItem = LoaiKHLCNTTypeName.DU_TOAN });
            lstLoaiCanCu.Add(new ComboboxItem() { ValueItem = LoaiKHLCNTType.QDDT, DisplayItem = LoaiKHLCNTTypeName.QDDT });
            lstLoaiCanCu.Add(new ComboboxItem() { ValueItem = LoaiKHLCNTType.CHU_TRUONG_DAU_TU, DisplayItem = LoaiKHLCNTTypeName.CHU_TRUONG_DAU_TU });
            ItemsLoaiCanCu = new ObservableCollection<ComboboxItem>(lstLoaiCanCu);
            if (Model.Id == Guid.Empty)
                SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault();
            else
                SelectedLoaiCanCu = ItemsLoaiCanCu.FirstOrDefault(n => n.ValueItem == Model.ILoaiCanCu);
        }

        private void LoadChungTuByLoaiChungTu()
        {
            if (SelectedLoaiCanCu != null)
            {
                switch (SelectedLoaiCanCu.ValueItem)
                {
                    case LoaiKHLCNTType.DU_TOAN:
                        STitleDuToanName = "DANH SÁCH TKTC VÀ TỔNG DỰ TOÁN";
                        break;
                    case LoaiKHLCNTType.CHU_TRUONG_DAU_TU:
                        STitleDuToanName = "DANH SÁCH CHỦ TRƯƠNG ĐẦU TƯ";
                        break;
                    case LoaiKHLCNTType.QDDT:
                        STitleDuToanName = "DANH SÁCH PHÊ DUYỆT DỰ ÁN";
                        break;
                }
            }

            ItemsCanCu = new ObservableCollection<VdtKhlcNhaThauCanCuModel>();
            if (SelectedDuAn != null && SelectedLoaiCanCu != null)
            {
                List<VdtKhlcNhaThauCanCuQuery> lstData = new List<VdtKhlcNhaThauCanCuQuery>();
                if (IsAdd && Model.IIdParentId.HasValue)
                {
                    lstData = _duanService.FindChungTuInKeHoachLuaChonNhaThauDieuChinh(Model.IIdParentId.Value, SelectedLoaiCanCu.ValueItem).ToList();
                }
                else
                {
                    lstData = _duanService.FindChungTuInKeHoachLuaChonNhaThauScreen(Model.Id, SelectedDuAn.Id, SelectedLoaiCanCu.ValueItem).ToList();
                }
                ItemsCanCu = _mapper.Map<ObservableCollection<VdtKhlcNhaThauCanCuModel>>(lstData);
                SelectedCanCu = ItemsCanCu.FirstOrDefault();
                if (Model.IIdDuToanId.HasValue)
                    SelectedCanCu = ItemsCanCu.FirstOrDefault(n => n.Id == Model.IIdDuToanId.Value);
                if (Model.IIdChuTruongDauTuId.HasValue)
                    SelectedCanCu = ItemsCanCu.FirstOrDefault(n => n.Id == Model.IIdChuTruongDauTuId.Value);
                if (Model.IIdQdDauTuId.HasValue)
                    SelectedCanCu = ItemsCanCu.FirstOrDefault(n => n.Id == Model.IIdQdDauTuId.Value);

                foreach (var item in ItemsCanCu)
                {
                    item.PropertyChanged += ChungTu_PropertyChanged;
                }
            }
            GetAllCanCuDetail();
            if (SelectedCanCu != null)
                SelectedCanCu.IsChecked = true;
            OnPropertyChanged(nameof(ItemsCanCu));
        }

        private void LoadDonVi()
        {
            List<string> lstUnitManager = _sessionService.Current.IdsDonViQuanLy.Split(",").ToList();

            var lstDonVi = _donviService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(x => lstUnitManager.Any(n => n.Contains(x.IIDMaDonVi)) && (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT));

            ItemsDonVi = new ObservableCollection<ComboboxItem>(lstDonVi.Select(
                n => new ComboboxItem()
                {
                    ValueItem = n.IIDMaDonVi,
                    DisplayItem = n.IIDMaDonVi + " - " + n.TenDonVi,
                    HiddenValue = n.Id.ToString()
                }));

            if (!string.IsNullOrEmpty(Model.IIdMaDonViQuanLy))
                SelectedDonVi = ItemsDonVi.FirstOrDefault(n => n.ValueItem == Model.IIdMaDonViQuanLy);
            else
                SelectedDonVi = null;

            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadChuDauTu()
        {
            //var lstChuDauTu = _chudautuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            var lstChuDauTu = _chudautuService.FindByAll();
            ItemsChuDauTu = new ObservableCollection<ComboboxItem>(lstChuDauTu.Select(
                n => new ComboboxItem()
                {
                    ValueItem = n.IIDMaDonVi,
                    DisplayItem = n.STenDonVi,
                    HiddenValue = n.Id.ToString()
                }));
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void GetHinhThucChonNhaThau()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_1, DisplayItem = HTChonNhaThauTypeName.HT_1 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_2, DisplayItem = HTChonNhaThauTypeName.HT_2 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_3, DisplayItem = HTChonNhaThauTypeName.HT_3 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_4, DisplayItem = HTChonNhaThauTypeName.HT_4 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_5, DisplayItem = HTChonNhaThauTypeName.HT_5 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_6, DisplayItem = HTChonNhaThauTypeName.HT_6 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_7, DisplayItem = HTChonNhaThauTypeName.HT_7 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_8, DisplayItem = HTChonNhaThauTypeName.HT_8 });
            lstData.Add(new ComboboxItem { ValueItem = HTChonNhaThauTypeName.HT_9, DisplayItem = HTChonNhaThauTypeName.HT_9 });
            ItemsHinhThuc = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void GetPhuongThucDauThau()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_1, DisplayItem = PTDauThauTypeName.PT_1 });
            lstData.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_2, DisplayItem = PTDauThauTypeName.PT_2 });
            lstData.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_3, DisplayItem = PTDauThauTypeName.PT_3 });
            lstData.Add(new ComboboxItem { ValueItem = PTDauThauTypeName.PT_4, DisplayItem = PTDauThauTypeName.PT_4 });
            ItemsPhuongThuc = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void GetLoaiHopDong()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            lstData.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_1, DisplayItem = HTHopDongTypeName.HD_1 });
            lstData.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_2, DisplayItem = HTHopDongTypeName.HD_2 });
            lstData.Add(new ComboboxItem { ValueItem = HTHopDongTypeName.HD_3, DisplayItem = HTHopDongTypeName.HD_3 });
            ItemsLoaiHopDong = new ObservableCollection<ComboboxItem>(lstData);
        }

        private void GetGoiThau()
        {
            if (IsDieuChinh == true)
            {
                ItemsGoiThau = _mapper.Map<ObservableCollection<VdtDaGoiThauModel>>(_goithauService.FindByKhlcNhaThauId(Guid.Parse(Model.IIdParentId.ToString())));
                // generate new id cho các gói thầu điều chỉnh
                ItemsGoiThau.ForAll(gt =>
                {
                    gt.IIdGoiThauGocId = gt.Id;
                    gt.Id = Guid.NewGuid();
                    // trường này để SaveGoiThau biết để adđ thêm gói thầu mới
                    gt.IsAdded = true;
                });
            }
            else
                ItemsGoiThau = _mapper.Map<ObservableCollection<VdtDaGoiThauModel>>(_goithauService.FindByKhlcNhaThauId(Model.Id));
            foreach (var item in ItemsGoiThau)
            {
                item.FGiaTriTruocDieuChinh = Double.Parse(item.FTienTrungThau.ToString());
                item.IsUpdate = true;
            }
            if (ItemsGoiThau != null)
            {
                FTongGoiThau = ItemsGoiThau.Where(n => !n.IsDeleted).Sum(n => n.FTienTrungThau ?? 0);
                OnPropertyChanged(nameof(FTongGoiThau));
            }
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void GetParentDetail()
        {
            _itemsNguonVonParent = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            _itemsChiPhiParent = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            _itemsHangMucParent = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            if (!Model.IIdParentId.HasValue) return;
            _itemsNguonVonParent = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauNguonVonByKhlcNhaThauId(Model.IIdParentId.Value));
            _itemsChiPhiParent = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauChiPhiByKhlcNhaThauId(Model.IIdParentId.Value));
            _itemsHangMucParent = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByKhlcNhaThauId(Model.IIdParentId.Value));
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> GetNguonVonByGoiThau(Guid? iIdGoiThauId)
        {
            Dictionary<int?, VdtKhlcNhaThauGoiThauDetailModel> dicNguonVon = new Dictionary<int?, VdtKhlcNhaThauGoiThauDetailModel>();
            Dictionary<int?, double> dicGiaTriTruocDieuChinh = GetDicGiaTriTruocDieuChinhNguonVon(iIdGoiThauId);

            if (_itemsNguonVonByGoiThau == null) _itemsNguonVonByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            if (_itemsNguonVonByGoiThau.Count == 0 && ItemsGoiThauGoc.Count != 0)
                dicNguonVon = _itemsNguonVonParent.Where(n => n.IIdGoiThauId == iIdGoiThauId && (n.FGiaTriGoiThau != 0)).ToDictionary(n => n.IIdNguonVonId, n => n);
            else if (_itemsNguonVonByGoiThau.Any(n => n.IIdGoiThauId == iIdGoiThauId))
            {
                _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.ToList();
                dicNguonVon = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId == iIdGoiThauId && (n.FGiaTriGoiThau != 0)).ToDictionary(n => n.IIdNguonVonId, n => n);
            }


            var lstDiff = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != iIdGoiThauId)
                .GroupBy(n => n.IIdNguonVonId)
                .Select(n => new
                {
                    iIdNguonVonId = n.Key,
                    FGiaTriGoiThau = n.Sum(n => n.FGiaTriGoiThau)
                });

            List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            foreach (var item in _itemsNguonVon.OrderBy(n => n.IIdNguonVonId))
            {
                VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
                {
                    IIdGoiThauId = iIdGoiThauId,
                    IIdNguonVonId = item.IIdNguonVonId,
                    SNoiDung = item.SNoiDung,
                    IsChecked = false
                };

                if (dicGiaTriTruocDieuChinh.ContainsKey(item.IIdNguonVonId))
                    obj.FGiaTriTruocDieuChinh = dicGiaTriTruocDieuChinh[item.IIdNguonVonId];

                if (dicNguonVon.ContainsKey(item.IIdNguonVonId))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriGoiThau = dicNguonVon[item.IIdNguonVonId].FGiaTriGoiThau;
                    obj.FGiaTriTruocDieuChinh = dicNguonVon[item.IIdNguonVonId].FGiaTriGoiThau;
                }
                obj.FGiaTriDuocDuyet = item.FGiaTriDuocDuyet;
                obj.FGiaTriConLai = item.FGiaTriDuocDuyet;

                if (lstDiff.Any(n => n.iIdNguonVonId == item.IIdNguonVonId))
                {
                    var objNguonVonDiff = lstDiff.FirstOrDefault(n => n.iIdNguonVonId == item.IIdNguonVonId);
                    obj.FGiaTriConLai -= objNguonVonDiff.FGiaTriGoiThau;
                }
                if (obj.FGiaTriConLai <= 0 && !obj.IsChecked) continue;
                lstData.Add(obj);
            }

            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != iIdGoiThauId).ToList();
            _itemsNguonVonByGoiThau.AddRange(lstData);
            return new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>(lstData);
        }

        private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> GetChiPhiByGoiThau(Guid iIDGoiThauId)
        {
            Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel> dicChiPhi = new Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel>();
            Dictionary<Guid?, double> dicGiaTriTruocDieuChinh = GetDicGiaTriTruocDieuChinhChiPhi(iIDGoiThauId);
            if (_itemsChiPhiByGoiThau == null) _itemsChiPhiByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            if (_itemsChiPhiByGoiThau.Count == 0 && ItemsGoiThauGoc.Count != 0)
                dicChiPhi = _itemsChiPhiParent.Where(n => n.IIdGoiThauId == iIDGoiThauId && n.FGiaTriGoiThau != 0).ToDictionary(n => n.IIdChiPhiId, n => n);
            else if (_itemsChiPhiByGoiThau != null && _itemsChiPhiByGoiThau.Any(n => n.IIdGoiThauId == iIDGoiThauId))
            {
                // _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.GroupBy(n => n.IIdChiPhiId).Select(group => group.First()).ToList(); //// ????
                // dicChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId == iIDGoiThauId && n.FGiaTriGoiThau != 0).ToDictionary(n => n.IIdChiPhiId, n => n);
                _itemsChiPhiByGoiThau.ForEach(hm =>
                {
                    if (hm.IsChecked && hm.IIdGoiThauId == iIDGoiThauId && !dicChiPhi.ContainsKey(hm.IIdChiPhiId))
                    {
                        dicChiPhi[hm.IIdChiPhiId] = hm;
                    }
                });
            }


            var lstDiff = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId)
                .GroupBy(n => n.IIdChiPhiId)
                .Select(n => new
                {
                    iIdChiPhiId = n.Key,
                    FGiaTriGoiThau = n.Sum(n => n.FGiaTriGoiThau)
                });

            List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            int index = 1;
            foreach (var item in OrderChiPhi(_itemsChiPhi))
            {
                VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
                {
                    IIdGoiThauId = iIDGoiThauId,
                    IIdChiPhiId = item.IIdChiPhiId,
                    SNoiDung = item.SNoiDung,
                    SMaOrder = item.SMaOrder,
                    IsAdd = item.IsAdd,
                    IIdParentId = item.IIdParentId,
                    IsHangCha = item.IsHangCha,
                    IsChecked = false,
                    STT = index++
                };

                if (dicGiaTriTruocDieuChinh.ContainsKey(item.IIdChiPhiId))
                    obj.FGiaTriTruocDieuChinh = dicGiaTriTruocDieuChinh[item.IIdChiPhiId];

                if (dicChiPhi.ContainsKey(item.IIdChiPhiId))
                {
                    obj.IsChecked = true;
                    obj.FGiaTriGoiThau = dicChiPhi[item.IIdChiPhiId].FGiaTriGoiThau;
                }

                obj.FGiaTriDuocDuyet = item.FGiaTriDuocDuyet;
                obj.FGiaTriConLai = item.FGiaTriDuocDuyet;

                if (lstDiff.Any(n => n.iIdChiPhiId == item.IIdChiPhiId))
                {
                    var objChiPhiDiff = lstDiff.FirstOrDefault(n => n.iIdChiPhiId == item.IIdChiPhiId);
                    if (SelectedLoaiCanCu.ValueItem != LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
                        obj.FGiaTriConLai -= objChiPhiDiff.FGiaTriGoiThau;
                }
                if (obj.FGiaTriConLai < 0 && !obj.IsChecked && SelectedLoaiCanCu.ValueItem != LoaiKHLCNTType.CHU_TRUONG_DAU_TU) continue;
                if (obj.FGiaTriDuocDuyet == 0 || obj.FGiaTriConLai > 0) lstData.Add(obj);
            }

            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId).ToList();
            _itemsChiPhiByGoiThau.AddRange(lstData);
            return new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>(lstData);
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> GetHangMucByGoiThau(Guid iIDGoiThauId)
        {
            //Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel> dicHangMuc = new Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel>();
            //Dictionary<Guid?, double> dicGiaTriTruocDieuChinh = GetDicGiaTriTruocDieuChinhHangMuc(iIDGoiThauId);
            //if (_itemsHangMucByGoiThau == null) _itemsHangMucByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            //if (_itemsHangMucByGoiThau.Count == 0 && ItemsGoiThauGoc.Count != 0)
            //    dicHangMuc = _itemsHangMucParent.Where(n => n.IIdGoiThauId == iIDGoiThauId &&( n.FGiaTriGoiThau != 0)).ToDictionary(n => n.IIdHangMucId, n => n);
            //else if (_itemsHangMucByGoiThau.Any(n => n.IIdGoiThauId == iIDGoiThauId))
            //    dicHangMuc = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId == iIDGoiThauId &&(n.FGiaTriTruocDieuChinh!=0 || n.FGiaTriGoiThau != 0)).ToDictionary(n => n.IIdHangMucId, n => n);


            if (BIsDieuChinh)
            {
                // merge như sửa để lấy toàn bộ danh sách các hạng mục
                List<VdtKhlcNhaThauGoiThauDetailModel> _itemsHangMucByChungTu = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByChungTu(SelectedCanCu.Id, SelectedLoaiCanCu.ValueItem));
                _itemsHangMucByChungTu.ForAll(hm =>
                {
                    if (_itemsHangMucParent.Select(x => x.IIdHangMucId).Contains(hm.IIdHangMucId))
                    {
                        hm.FGiaTriConLaiShow = 0;
                        // gán giá trị id gói thầu do danh sách _itemsHangMucByGoiThau là danh sách lấy ra từ qddt
                        hm.IIdGoiThauId = _itemsHangMucParent.Find(h => h.IIdHangMucId == hm.IIdHangMucId).IIdGoiThauId;
                        hm.IsChecked = true;
                        hm.FGiaTriGoiThau = _itemsHangMucParent.Find(h => h.IIdHangMucId == hm.IIdHangMucId).FGiaTriGoiThau;
                    }
                    else
                    {
                        hm.FGiaTriConLaiShow = hm.FGiaTriDuocDuyet;
                        hm.FGiaTriGoiThau = hm.FGiaTriDuocDuyet;
                    }
                });
                _itemsHangMucByGoiThau = _itemsHangMucByChungTu.Clone();
            }

            // danh sách các hạng mục thuộc gói thầu khác, hoặc chưa thuộc gói thầu nào
            List<VdtKhlcNhaThauGoiThauDetailModel> hmConLai = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId).ToList();

            List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();

            // add lại những hạng mục đã được chọn
            foreach (var item in _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId == iIDGoiThauId))
            {
                lstData.Add(item);
            }

            // nếu trong những hm còn lại, hạng mục nào còn tiền thì add vào 
            hmConLai.ForEach(h =>
            {
                if (h.FGiaTriConLaiShow > 0)
                {
                    lstData.Add(h);
                }
            });

            //_itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId).ToList();
            //_itemsHangMucByGoiThau.AddRange(lstData);
            return lstData;
        }

        private void ReCalculateGiaTriConLaiChoHangMucParent()
        {
            _itemsHangMucByGoiThau.ForEach(hm =>
            {
                if (hm.IIdParentId == null)
                {
                    hm.FGiaTriConLaiShow = hm.FGiaTriDuocDuyet - _itemsHangMucByGoiThau.Where(h => h.IIdParentId == hm.IIdHangMucId && h.IsChecked).Sum(s => s.FGiaTriGoiThau);
                }
            });
        }

        private void RecalculateGiaTriConLaiChoHangMucCon()
        {
            _itemsHangMucByGoiThau.ForEach(hm =>
            {
                if (hm.IsChecked)
                {
                    hm.FGiaTriConLaiShow = 0;
                }
                else
                {
                    hm.FGiaTriConLaiShow = hm.FGiaTriDuocDuyet;
                }
            });
        }

        private void OnViewAttachment(object obj)
        {
            if (SelectedLoaiCanCu != null)
            {
                switch (SelectedLoaiCanCu.ValueItem)
                {
                    case LoaiKHLCNTType.DU_TOAN:
                        AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_TKTC_TONGDUTOAN;
                        break;
                    case LoaiKHLCNTType.QDDT:
                        AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_THONGTIN_DUAN;
                        break;
                    case LoaiKHLCNTType.CHU_TRUONG_DAU_TU:
                        AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_CHUTRUONG_DAUTU;
                        break;
                }
                if (SelectedCanCu != null)
                    AttachmentViewModel.ObjectId = SelectedCanCu.Id;
                else
                    AttachmentViewModel.ObjectId = Guid.Empty;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost("KHLuaChonNhaThauDiaLog");
            }
        }

        private void OnShowGoiThau()
        {
            if (SelectedGoiThau == null) return;
            if (SelectedCanCu == null)
            {
                MessageBoxHelper.Error(Resources.MsgErrorChungTuEmpty);
                return;
            }
            KHLuaChonNhaThauDetailViewModel.BIsDieuChinh = BIsDieuChinh;
            KHLuaChonNhaThauDetailViewModel.IsDetail = IsDetail;
            KHLuaChonNhaThauDetailViewModel.SLoaiChungTu = SelectedLoaiCanCu.ValueItem;
            if (ItemsGoiThauGoc.Count == 0)
            {
                KHLuaChonNhaThauDetailViewModel.ItemsNguonVon = GetNguonVonByGoiThau(SelectedGoiThau.Id).Clone();
                KHLuaChonNhaThauDetailViewModel.ItemsChiPhi = GetChiPhiByGoiThau(SelectedGoiThau.Id).Clone();
                KHLuaChonNhaThauDetailViewModel.LstHangMuc = GetHangMucByGoiThau(SelectedGoiThau.Id).Clone();
                KHLuaChonNhaThauDetailViewModel.LstTmpChiPhi = GetChiPhiByGoiThau(SelectedGoiThau.Id).Clone();
            }
            else
            {
                //_itemsNguonVonByGoiThau = GetNguonVonByGoiThau(SelectedGoiThau.IIdGoiThauGocId).Clone().ToList();
                //_itemsChiPhiByGoiThau = GetChiPhiByGoiThau(Guid.Parse(SelectedGoiThau.IIdGoiThauGocId.ToString())).Clone().ToList();
                //_itemsHangMucByGoiThau = GetHangMucByGoiThau(Guid.Parse(SelectedGoiThau.IIdGoiThauGocId.ToString())).Clone().ToList();
                KHLuaChonNhaThauDetailViewModel.ItemsNguonVon = GetNguonVonByGoiThau(SelectedGoiThau.IIdGoiThauGocId).Clone();
                KHLuaChonNhaThauDetailViewModel.ItemsChiPhi = GetChiPhiByGoiThau(Guid.Parse(SelectedGoiThau.IIdGoiThauGocId.ToString())).Clone();
                KHLuaChonNhaThauDetailViewModel.LstHangMuc = GetHangMucByGoiThau(Guid.Parse(SelectedGoiThau.IIdGoiThauGocId.ToString())).Clone();
            }

            KHLuaChonNhaThauDetailViewModel.Model = SelectedGoiThau;
            KHLuaChonNhaThauDetailViewModel.Init();
            KHLuaChonNhaThauDetailViewModel.SavedAction = obj => SaveGoiThauDetail();
            var view = new KHLuaChonNhaThauDetail { DataContext = KHLuaChonNhaThauDetailViewModel };
            view.ShowDialog();
            OnPropertyChanged(nameof(ItemsGoiThau));
        }
        private void SaveGoiThauDetail()
        {
            var objGoiThau = ItemsGoiThau.FirstOrDefault(n => n.Id == KHLuaChonNhaThauDetailViewModel.Model.Id);
            if (objGoiThau == null) return;
            if (BIsDieuChinh == true)
            {
                foreach (var item in _itemsNguonVonByGoiThau)
                {
                    item.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.Id == KHLuaChonNhaThauDetailViewModel.Model.Id).Id;
                }
                foreach (var item in _itemsChiPhiByGoiThau)
                {
                    item.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.Id == KHLuaChonNhaThauDetailViewModel.Model.Id).Id;
                }
                foreach (var item in _itemsHangMucByGoiThau)
                {
                    item.IIdGoiThauId = ItemsGoiThau.FirstOrDefault(n => n.Id == KHLuaChonNhaThauDetailViewModel.Model.Id).Id;
                }
            }
            _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != objGoiThau.Id).ToList();
            _itemsNguonVonByGoiThau.AddRange(KHLuaChonNhaThauDetailViewModel.ItemsNguonVon);

            if (SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
                SetChiPhiChuTruongDauTu(KHLuaChonNhaThauDetailViewModel.ItemsChiPhi.Where(n => n.IsChecked && !n.IsDeleted).ToList());
            _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != objGoiThau.Id && !n.IsDeleted).ToList();
            if (BIsDieuChinh == true)
                _itemsChiPhiByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            _itemsChiPhiByGoiThau.AddRange(KHLuaChonNhaThauDetailViewModel.ItemsChiPhi.Where(n => n.IsChecked && !n.IsDeleted));

            // update lại các hạng mục với kết quả lấy từ LstHangmuc
            KHLuaChonNhaThauDetailViewModel.LstHangMuc.ForEach(hm =>
            {
                VdtKhlcNhaThauGoiThauDetailModel hmUpdate = _itemsHangMucByGoiThau.Find(x => x.IIdHangMucId == hm.IIdHangMucId);
                if (hmUpdate != null)
                {
                    hmUpdate.IIdGoiThauId = hm.IIdGoiThauId;
                    hmUpdate.IsChecked = hm.IsChecked;
                }
            });
            //if (BIsDieuChinh == true)
            //{
            //    _itemsHangMucByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            //}   

            // update toàn bộ các giá trị còn lại của tất cả các hạng mục
            ReCalculateGiaTriConLaiChoHangMucParent();
            RecalculateGiaTriConLaiChoHangMucCon();

            objGoiThau.FTienTrungThau = KHLuaChonNhaThauDetailViewModel.ItemsNguonVon.Where(n => n.IsChecked).Sum(n => n.FGiaTriGoiThau);
            OnPropertyChanged(nameof(ItemsGoiThau));
        }

        private void SetChiPhiChuTruongDauTu(List<VdtKhlcNhaThauGoiThauDetailModel> ItemsChiPhiNew)
        {
            Dictionary<Guid, VdtKhlcNhaThauGoiThauDetailModel> dicRoot = _itemsChiPhi.ToDictionary(n => n.IIdChiPhiId.Value, n => n);
            int index = -1;
            var lstNew = ItemsChiPhiNew.Clone();
            foreach (var item in lstNew)
            {
                index++;
                if (dicRoot.ContainsKey(item.IIdChiPhiId.Value))
                {
                    _itemsChiPhi.FirstOrDefault(n => n.IIdChiPhiId == item.IIdChiPhiId).SNoiDung = item.SNoiDung;
                    continue;
                }
                VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
                {
                    IIdChiPhiGocId = item.IIdChiPhiGocId,
                    IIdChiPhiId = item.IIdChiPhiId,
                    IIdParentId = item.IIdParentId,
                    IsAdd = true,
                    SNoiDung = item.SNoiDung
                };
                _itemsChiPhi.Insert(index, obj);
            }
        }

        private void SetValueBySelectCanCu()
        {
            if (SelectedLoaiCanCu == null || SelectedCanCu == null)
            {
                _itemsNguonVon = new List<VdtKhlcNhaThauGoiThauDetailModel>();
                _itemsChiPhi = new List<VdtKhlcNhaThauGoiThauDetailModel>();
                _itemsHangMuc = new List<VdtKhlcNhaThauGoiThauDetailModel>();
                return;
            }
            _itemsNguonVon = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauNguonVonByChungTu(SelectedCanCu.Id, SelectedLoaiCanCu.ValueItem));
            if (SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU && Model.Id != Guid.Empty)
            {
                _itemsChiPhi = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauChiPhiByChungTuCtdt_KhlcntEdit(Model.Id));
            }
            else
            {
                _itemsChiPhi = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauChiPhiByChungTu(SelectedCanCu.Id, SelectedLoaiCanCu.ValueItem, IsAdd));
            }
            // Trong trường hợp tạo mới thì lấy danh sách hạng mục theo qddt
            if (Model.Id == Guid.Empty)
            {
                _itemsHangMuc = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByChungTu(SelectedCanCu.Id, SelectedLoaiCanCu.ValueItem));
                _itemsHangMucByGoiThau = _itemsHangMuc;
                // set giá trị còn lại = giá trị phê duyệt do k lưu lại gt còn lại
                _itemsHangMucByGoiThau.ForEach(hm =>
                {
                    hm.FGiaTriConLaiShow = hm.FGiaTriDuocDuyet;
                    hm.FGiaTriGoiThau = hm.FGiaTriDuocDuyet;
                });
            }
            // Sửa thì lấy danh sách gói thầu của khlc hiện tại, rồi merge vào itemsHangMuc để tạo ra _itemsHangMucByGoiThau
            else
            {
                _itemsHangMucByGoiThau = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByChungTu(SelectedCanCu.Id, SelectedLoaiCanCu.ValueItem));
                _itemsHangMuc = _mapper.Map<List<VdtKhlcNhaThauGoiThauDetailModel>>(_goithauService.GetGoiThauHangMucByKhlcNhaThauId(Model.Id));
                _itemsHangMucByGoiThau.ForAll(hm =>
                {
                    if (_itemsHangMuc.Select(x => x.IIdHangMucId).Contains(hm.IIdHangMucId))
                    {
                        hm.FGiaTriConLaiShow = 0;
                        // gán giá trị id gói thầu do danh sách _itemsHangMucByGoiThau là danh sách lấy ra từ qddt
                        hm.IIdGoiThauId = _itemsHangMuc.Find(h => h.IIdHangMucId == hm.IIdHangMucId).IIdGoiThauId;
                        hm.IsChecked = true;
                        hm.FGiaTriGoiThau = _itemsHangMuc.Find(h => h.IIdHangMucId == hm.IIdHangMucId).FGiaTriGoiThau;
                    }
                    else
                    {
                        hm.FGiaTriConLaiShow = hm.FGiaTriDuocDuyet;
                        hm.FGiaTriGoiThau = hm.FGiaTriDuocDuyet;
                    }
                });
            }
        }

        private void GetAllCanCuDetail()
        {
            ItemsDuToanFilter = new ObservableCollection<VdtDuToanModel>();
            if (ItemsCanCu == null)
            {
                return;
            }
            List<VdtDaDuToanQuery> data = ItemsCanCu.Clone().Select(n =>
                new VdtDaDuToanQuery()
                {
                    Id = n.Id,
                    SSoQuyetDinh = n.SSoQuyetDinh
                }).ToList();

            ItemsDuToanFilter = _mapper.Map<ObservableCollection<VdtDuToanModel>>(data);
            SDuToanFilter = string.Join(", ", ItemsDuToanFilter.Select(n => n.SSoQuyetDinh));
            foreach (var item in ItemsDuToanFilter)
            {
                item.IsChecked = true;
                item.PropertyChanged += DuToanFilter_PropertyChanged;
            }
            GetAllNguonVonByCanCu();
            GetAllChiPhiByCanCu();
            OnPropertyChanged(nameof(ItemsDuToanFilter));
        }

        private void GetAllNguonVonByCanCu()
        {
            ItemsNguonVonAll = new ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>();
            if (SelectedLoaiCanCu == null) return;
            ItemsNguonVonAll = _mapper.Map<ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>>
                (_goithauService.GetAllNguonVonByLoaiCanCuInKhlcntScreen(ItemsCanCu.Select(n => n.Id).ToList(), SelectedLoaiCanCu.ValueItem));
            _allNguonVonView = CollectionViewSource.GetDefaultView(ItemsNguonVonAll);
            _allNguonVonView.Filter = NguonVonFilter;
        }

        private void GetAllChiPhiByCanCu()
        {
            ItemsChiPhiAll = new ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>();
            if (SelectedLoaiCanCu == null) return;
            ItemsChiPhiAll = _mapper.Map<ObservableCollection<VdtKhlcntChiPhiNguonVonCanCuModel>>
                (OrderChiPhiCanCuAll(_goithauService.GetAllChiPhiByLoaiCanCuInKhlcntScreen(ItemsCanCu.Select(n => n.Id).ToList(), SelectedLoaiCanCu.ValueItem)));
            _allChiPhiView = CollectionViewSource.GetDefaultView(ItemsChiPhiAll);
            _allChiPhiView.Filter = ChiPhiFilter;
        }

        private List<VdtKhlcntChiPhiNguonVonCanCuQuery> OrderChiPhiCanCuAll(IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> datas)
        {
            if (datas == null) return new List<VdtKhlcntChiPhiNguonVonCanCuQuery>();
            List<VdtKhlcntChiPhiNguonVonCanCuQuery> results = new List<VdtKhlcntChiPhiNguonVonCanCuQuery>();
            foreach (var item in datas.Where(n => !n.IIdParentId.HasValue))
            {
                results.AddRange(RecusiveChiPhiCanCuAll(item, datas));
            }
            return results;
        }

        private List<VdtKhlcntChiPhiNguonVonCanCuQuery> RecusiveChiPhiCanCuAll(VdtKhlcntChiPhiNguonVonCanCuQuery item, IEnumerable<VdtKhlcntChiPhiNguonVonCanCuQuery> datas)
        {
            List<VdtKhlcntChiPhiNguonVonCanCuQuery> results = new List<VdtKhlcntChiPhiNguonVonCanCuQuery>();
            results.Add(item);
            if (!datas.Any(n => n.IIdParentId.HasValue && n.IIdParentId == item.Id)) return results;
            foreach (var child in datas.Where(n => n.IIdParentId == item.Id))
                results.AddRange(RecusiveChiPhiCanCuAll(child, datas));
            return results;
        }

        private bool NguonVonFilter(object obj)
        {
            if (!(obj is VdtKhlcntChiPhiNguonVonCanCuModel temp)) return true;
            var bCondition = false;
            if (ItemsDuToanFilter.Any(n => n.IsChecked))
            {
                bCondition |= (ItemsDuToanFilter.Where(n => n.IsChecked).Any(n => n.Id == temp.IIdCanCuId));
            }
            return bCondition;
        }

        private bool ChiPhiFilter(object obj)
        {
            if (!(obj is VdtKhlcntChiPhiNguonVonCanCuModel temp)) return true;
            var bCondition = false;
            if (ItemsDuToanFilter.Any(n => n.IsChecked))
            {
                bCondition |= (ItemsDuToanFilter.Where(n => n.IsChecked).Any(n => n.Id == temp.IIdCanCuId));
            }
            return bCondition;
        }

        private void GetGoiThauGoc()
        {
            ItemsGoiThauGoc = new ObservableCollection<VdtDaGoiThauModel>();
            if (!BIsDieuChinh || !IsAdd || !Model.IIdParentId.HasValue) return;
            var lstGoiThau = _goithauService.FindByKhlcNhaThauId(Model.IIdParentId.Value);
            if (lstGoiThau == null) return;
            ItemsGoiThauGoc = _mapper.Map<ObservableCollection<VdtDaGoiThauModel>>(lstGoiThau);
            OnPropertyChanged(nameof(ItemsGoiThauGoc));
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> OrderChiPhi(List<VdtKhlcNhaThauGoiThauDetailModel> lstData)
        {
            if (lstData == null) return new List<VdtKhlcNhaThauGoiThauDetailModel>();
            List<VdtKhlcNhaThauGoiThauDetailModel> results = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            foreach (var item in lstData.Where(n => !n.IIdParentId.HasValue))
            {
                results.AddRange(RecursiveChiPhi(item, lstData));
            }
            return results;
        }

        private List<VdtKhlcNhaThauGoiThauDetailModel> RecursiveChiPhi(VdtKhlcNhaThauGoiThauDetailModel item, List<VdtKhlcNhaThauGoiThauDetailModel> lstData)
        {
            List<VdtKhlcNhaThauGoiThauDetailModel> results = new List<VdtKhlcNhaThauGoiThauDetailModel>();
            bool bIsParent = lstData.Any(n => n.IIdParentId == item.IIdChiPhiId);
            item.IsHangCha = bIsParent;
            results.Add(item);
            if (!bIsParent) return results;
            foreach (var child in lstData.Where(n => n.IIdParentId == item.IIdChiPhiId))
            {
                results.AddRange(RecursiveChiPhi(child, lstData));
            }
            return results;
        }

        private Dictionary<int?, double> GetDicGiaTriTruocDieuChinhNguonVon(Guid? iIdGoiThauId)
        {
            Dictionary<int?, double> dicData = new Dictionary<int?, double>();
            if (ItemsGoiThauGoc.Count == 0 && (ItemsGoiThau == null || !ItemsGoiThau.Any(n => n.Id == iIdGoiThauId) || ItemsGoiThau.FirstOrDefault(n => n.Id == iIdGoiThauId).SelectedGoiThauParent == null))
                return dicData;
            Guid iIdGoiThauParent = Guid.Empty;
            if (ItemsGoiThau.Any(n => n.IIdGoiThauGocId == iIdGoiThauId && n.SelectedGoiThauParent != null))
                iIdGoiThauParent = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == iIdGoiThauId).SelectedGoiThauParent.Id;
            else
                iIdGoiThauParent = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == iIdGoiThauId).IIdGoiThauGocId.Value;
            foreach (var item in _itemsNguonVonParent.Where(n => n.IIdGoiThauId == iIdGoiThauParent))
            {
                if (!dicData.ContainsKey(item.IIdNguonVonId))
                    dicData.Add(item.IIdNguonVonId, 0);
                dicData[item.IIdNguonVonId] += item.FGiaTriGoiThau;
            }
            return dicData;
        }

        private Dictionary<Guid?, double> GetDicGiaTriTruocDieuChinhChiPhi(Guid iIdGoiThauId)
        {
            Dictionary<Guid?, double> dicData = new Dictionary<Guid?, double>();
            if (ItemsGoiThauGoc.Count == 0 && (ItemsGoiThau == null || !ItemsGoiThau.Any(n => n.Id == iIdGoiThauId) || ItemsGoiThau.FirstOrDefault(n => n.Id == iIdGoiThauId).SelectedGoiThauParent == null))
                return dicData;
            Guid iIdGoiThauParent = Guid.Empty;
            if (ItemsGoiThau.Any(n => n.IIdGoiThauGocId == iIdGoiThauId && n.SelectedGoiThauParent != null))
                iIdGoiThauParent = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == iIdGoiThauId).SelectedGoiThauParent.Id;
            else
                iIdGoiThauParent = ItemsGoiThau.FirstOrDefault(n => n.IIdGoiThauGocId == iIdGoiThauId).IIdGoiThauGocId.Value;
            foreach (var item in _itemsChiPhiParent.Where(n => n.IIdGoiThauId == iIdGoiThauParent))
            {
                if (!dicData.ContainsKey(item.IIdChiPhiId))
                    dicData.Add(item.IIdChiPhiId, 0);
                dicData[item.IIdChiPhiId] += item.FGiaTriGoiThau;
            }
            return dicData;
        }

        private Dictionary<Guid?, double> GetDicGiaTriTruocDieuChinhHangMuc(Guid iIdGoiThauId)
        {
            Dictionary<Guid?, double> dicData = new Dictionary<Guid?, double>();
            if (ItemsGoiThauGoc.Count == 0 && (ItemsGoiThau == null || !ItemsGoiThau.Any(n => n.Id == iIdGoiThauId) || ItemsGoiThau.FirstOrDefault(n => n.Id == iIdGoiThauId).SelectedGoiThauParent == null))
                return dicData;
            Guid iIdGoiThauParent = ItemsGoiThau.FirstOrDefault(n => n.Id == iIdGoiThauId).SelectedGoiThauParent.Id;
            foreach (var item in _itemsHangMucParent.Where(n => n.IIdGoiThauId == iIdGoiThauParent))
            {
                if (!dicData.ContainsKey(item.IIdHangMucId))
                    dicData.Add(item.IIdHangMucId, 0);
                dicData[item.IIdHangMucId] += item.FGiaTriGoiThau;
            }
            return dicData;
        }

        private void DisplayFileAttach()
        {
            if (SelectedLoaiCanCu == null) return;
            if (SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.DU_TOAN)
            {
                IsDisplayFileAttachment = false;
            }
            else
            {
                IsDisplayFileAttachment = true;
            }
        }

        private void TaoMaOrders(List<VdtKhlcntChiPhiNguonVonCanCuModel> data)
        {
            var groupByCanCuId = data.GroupBy(x => x.IIdCanCuId);
            foreach (var groupData in groupByCanCuId)
            {
                int index = 1;
                foreach (var item in groupData)
                {
                    item.SMaOrder = $"{index}";
                    index++;
                }
            }


        }
        #region Tree
        //private void SaveGoiThauDetail()
        //{
        //    var objGoiThau = ItemsGoiThau.FirstOrDefault(n => n.Id == KHLuaChonNhaThauDetailViewModel.Model.Id);
        //    if (objGoiThau == null) return;
        //    _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != objGoiThau.Id).ToList();
        //    _itemsNguonVonByGoiThau.AddRange(KHLuaChonNhaThauDetailViewModel.ItemsNguonVon);

        //    if (SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
        //        SetChiPhiChuTruongDauTu(KHLuaChonNhaThauDetailViewModel.ItemsChiPhi.Where(n => (n.IsChecked ?? true) && !n.IsDeleted).ToList());
        //    _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != objGoiThau.Id && !n.IsDeleted).ToList();
        //    _itemsChiPhiByGoiThau.AddRange(KHLuaChonNhaThauDetailViewModel.ItemsChiPhi.Where(n => (n.IsChecked ?? true) && !n.IsDeleted));

        //    _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId != objGoiThau.Id).ToList();
        //    _itemsHangMucByGoiThau.AddRange(KHLuaChonNhaThauDetailViewModel.LstHangMuc.Where(n => (n.IsChecked ?? true)));

        //    objGoiThau.FTienTrungThau = KHLuaChonNhaThauDetailViewModel.ItemsNguonVon.Where(n => (n.IsChecked ?? false)).Sum(n => n.FGiaTriGoiThau);
        //    OnPropertyChanged(nameof(ItemsGoiThau));
        //}

        //private List<VdtKhlcNhaThauGoiThauDetailModel> GetHangMucByGoiThau(Guid iIDGoiThauId)
        //{
        //    Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel> dicHangMuc = new Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel>();
        //    if (_itemsHangMucByGoiThau == null) _itemsHangMucByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();

        //    if (_itemsHangMucByGoiThau.Any(n => n.IIdGoiThauId == iIDGoiThauId))
        //        dicHangMuc = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId == iIDGoiThauId && n.FGiaTriGoiThau != 0).ToDictionary(n => n.IIdHangMucId, n => n);

        //    var lstDiff = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId)
        //        .GroupBy(n => n.IIdHangMucId)
        //        .Select(n => new
        //        {
        //            iIdHangMucId = n.Key,
        //            FGiaTriGoiThau = n.Sum(n => n.FGiaTriGoiThau)
        //        });

        //    List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();
        //    foreach (var item in _itemsHangMuc.OrderBy(n => n.SMaOrder))
        //    {
        //        VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
        //        {
        //            IIdGoiThauId = iIDGoiThauId,
        //            IIdChiPhiId = item.IIdChiPhiId,
        //            IIdParentId = item.IIdParentId,
        //            IIdHangMucId = item.IIdHangMucId,
        //            SNoiDung = item.SNoiDung,
        //            SMaOrder = item.SMaOrder,
        //            IsChecked = false
        //        };

        //        if (dicHangMuc.ContainsKey(item.IIdHangMucId))
        //        {
        //            obj.IsChecked = true;
        //            obj.FGiaTriGoiThau = dicHangMuc[item.IIdHangMucId].FGiaTriGoiThau;
        //        }

        //        obj.FGiaTriDuocDuyet = item.FGiaTriDuocDuyet;
        //        obj.FGiaTriConLai = item.FGiaTriDuocDuyet;

        //        if (lstDiff.Any(n => n.iIdHangMucId == item.IIdHangMucId))
        //        {
        //            var objHangMucDiff = lstDiff.FirstOrDefault(n => n.iIdHangMucId == item.IIdHangMucId);
        //            obj.FGiaTriConLai -= objHangMucDiff.FGiaTriGoiThau;
        //        }
        //        if (obj.FGiaTriConLai <= 0 && !(obj.IsChecked ?? true)) continue;
        //        lstData.Add(obj);
        //    }

        //    _itemsHangMucByGoiThau = _itemsHangMucByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId).ToList();
        //    _itemsHangMucByGoiThau.AddRange(lstData);
        //    return lstData;
        //}

        //private void SaveNguonVon()
        //{
        //    var lstNguonVon = _itemsNguonVonByGoiThau.Where(n => (n.IsChecked ?? false)).Select(n => new VdtDaGoiThauNguonVon()
        //    {
        //        IIdGoiThauId = n.IIdGoiThauId,
        //        FTienGoiThau = n.FGiaTriGoiThau,
        //        IIdNguonVonId = n.IIdNguonVonId
        //    }).ToList();
        //    _goithauService.AddRangeGoiThauNguonVon(lstNguonVon);
        //}

        //private void SaveChiPhi(ref Dictionary<string, Guid> dicGoiThauChiPhi)
        //{
        //    var lstChiPhi = _itemsChiPhiByGoiThau.Where(n => (n.IsChecked ?? true) || SelectedLoaiCanCu.ValueItem == LoaiKHLCNTType.CHU_TRUONG_DAU_TU).Select(n => new VdtDaGoiThauChiPhi()
        //    {
        //        IIdGoiThauId = n.IIdGoiThauId,
        //        IIdChiPhiId = n.IIdChiPhiId,
        //        FTienGoiThau = n.FGiaTriGoiThau
        //    }).ToList();
        //    _goithauService.AddRangeGoiThauChiPhi(lstChiPhi);

        //    var lstDmChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IsAdd);
        //    if (lstDmChiPhi == null) return;

        //    Dictionary<Guid, VdtDmDuAnChiPhi> dicNew = new Dictionary<Guid, VdtDmDuAnChiPhi>();
        //    List<VdtDmDuAnChiPhi> lstAdd = new List<VdtDmDuAnChiPhi>();
        //    foreach (var item in lstDmChiPhi)
        //    {
        //        if (dicNew.ContainsKey(item.IIdChiPhiId.Value)) continue;
        //        dicNew.Add(item.IIdChiPhiId.Value, new VdtDmDuAnChiPhi());
        //        lstAdd.Add(new VdtDmDuAnChiPhi()
        //        {
        //            Id = item.IIdChiPhiId.Value,
        //            STenChiPhi = item.SNoiDung,
        //            IIdChiPhiParent = item.IIdParentId,
        //            IIdChiPhi = item.IIdChiPhiGocId
        //        });
        //    }

        //    _duanService.InsertDmDuAnChiPhi(lstAdd);
        //}

        //private void SaveHangMuc(Dictionary<string, Guid> dicGoiThauChiPhi)
        //{
        //    var lstHangMuc = _itemsHangMucByGoiThau.Where(n => (n.IsChecked ?? true)).Select(n => new VdtDaGoiThauHangMuc()
        //    {
        //        IIdGoiThauId = n.IIdGoiThauId,
        //        IIdHangMucId = n.IIdHangMucId,
        //        IIDChiPhiID = n.IIdChiPhiId,
        //        FTienGoiThau = n.FGiaTriGoiThau
        //    }).ToList();
        //    _goithauService.AddRangeGoiThauHangMuc(lstHangMuc);
        //}

        //private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> GetNguonVonByGoiThau(Guid? iIdGoiThauId)
        //{
        //    Dictionary<int?, VdtKhlcNhaThauGoiThauDetailModel> dicNguonVon = new Dictionary<int?, VdtKhlcNhaThauGoiThauDetailModel>();
        //    if (_itemsNguonVonByGoiThau == null) _itemsNguonVonByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();

        //    if (_itemsNguonVonByGoiThau.Any(n => n.IIdGoiThauId == iIdGoiThauId))
        //        dicNguonVon = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId == iIdGoiThauId && n.FGiaTriGoiThau != 0).ToDictionary(n => n.IIdNguonVonId, n => n);

        //    var lstDiff = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != iIdGoiThauId)
        //        .GroupBy(n => n.IIdNguonVonId)
        //        .Select(n => new
        //        {
        //            iIdNguonVonId = n.Key,
        //            FGiaTriGoiThau = n.Sum(n => n.FGiaTriGoiThau)
        //        });

        //    List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();
        //    foreach (var item in _itemsNguonVon.OrderBy(n => n.IIdNguonVonId))
        //    {
        //        VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
        //        {
        //            IIdGoiThauId = iIdGoiThauId,
        //            IIdNguonVonId = item.IIdNguonVonId,
        //            SNoiDung = item.SNoiDung,
        //            IsChecked = false
        //        };

        //        if (dicNguonVon.ContainsKey(item.IIdNguonVonId))
        //        {
        //            obj.IsChecked = true;
        //            obj.FGiaTriGoiThau = dicNguonVon[item.IIdNguonVonId].FGiaTriGoiThau;
        //        }

        //        obj.FGiaTriDuocDuyet = item.FGiaTriDuocDuyet;
        //        obj.FGiaTriConLai = item.FGiaTriDuocDuyet;

        //        if (lstDiff.Any(n => n.iIdNguonVonId == item.IIdNguonVonId))
        //        {
        //            var objNguonVonDiff = lstDiff.FirstOrDefault(n => n.iIdNguonVonId == item.IIdNguonVonId);
        //            obj.FGiaTriConLai -= objNguonVonDiff.FGiaTriGoiThau;
        //        }
        //        if (obj.FGiaTriConLai <= 0 && !(obj.IsChecked ?? false)) continue;
        //        lstData.Add(obj);
        //    }

        //    _itemsNguonVonByGoiThau = _itemsNguonVonByGoiThau.Where(n => n.IIdGoiThauId != iIdGoiThauId).ToList();
        //    _itemsNguonVonByGoiThau.AddRange(lstData);
        //    return new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>(lstData);
        //}

        //private ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel> GetChiPhiByGoiThau(Guid iIDGoiThauId)
        //{
        //    Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel> dicChiPhi = new Dictionary<Guid?, VdtKhlcNhaThauGoiThauDetailModel>();
        //    if (_itemsChiPhiByGoiThau == null) _itemsChiPhiByGoiThau = new List<VdtKhlcNhaThauGoiThauDetailModel>();
        //    if (_itemsChiPhiByGoiThau != null && _itemsChiPhiByGoiThau.Any(n => n.IIdGoiThauId == iIDGoiThauId))
        //        dicChiPhi = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId == iIDGoiThauId && n.FGiaTriGoiThau != 0).ToDictionary(n => n.IIdChiPhiId, n => n);

        //    var lstDiff = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId)
        //        .GroupBy(n => n.IIdChiPhiId)
        //        .Select(n => new
        //        {
        //            iIdChiPhiId = n.Key,
        //            FGiaTriGoiThau = n.Sum(n => n.FGiaTriGoiThau)
        //        });

        //    List<VdtKhlcNhaThauGoiThauDetailModel> lstData = new List<VdtKhlcNhaThauGoiThauDetailModel>();
        //    foreach (var item in OrderChiPhi(_itemsChiPhi))
        //    {
        //        VdtKhlcNhaThauGoiThauDetailModel obj = new VdtKhlcNhaThauGoiThauDetailModel()
        //        {
        //            IIdGoiThauId = iIDGoiThauId,
        //            IIdChiPhiId = item.IIdChiPhiId,
        //            SNoiDung = item.SNoiDung,
        //            SMaOrder = item.SMaOrder,
        //            IsAdd = item.IsAdd,
        //            IIdParentId = item.IIdParentId,
        //            IsHangCha = item.IsHangCha,
        //            IsChecked = false
        //        };

        //        if (dicChiPhi.ContainsKey(item.IIdChiPhiId))
        //        {
        //            obj.IsChecked = true;
        //            obj.FGiaTriGoiThau = dicChiPhi[item.IIdChiPhiId].FGiaTriGoiThau;
        //        }

        //        obj.FGiaTriDuocDuyet = item.FGiaTriDuocDuyet;
        //        obj.FGiaTriConLai = item.FGiaTriDuocDuyet;

        //        if (lstDiff.Any(n => n.iIdChiPhiId == item.IIdChiPhiId))
        //        {
        //            var objChiPhiDiff = lstDiff.FirstOrDefault(n => n.iIdChiPhiId == item.IIdChiPhiId);
        //            if (SelectedLoaiCanCu.ValueItem != LoaiKHLCNTType.CHU_TRUONG_DAU_TU)
        //                obj.FGiaTriConLai -= objChiPhiDiff.FGiaTriGoiThau;
        //        }
        //        if (obj.FGiaTriConLai <= 0 && !(obj.IsChecked ?? true) && SelectedLoaiCanCu.ValueItem != LoaiKHLCNTType.CHU_TRUONG_DAU_TU) continue;
        //        lstData.Add(obj);
        //    }

        //    _itemsChiPhiByGoiThau = _itemsChiPhiByGoiThau.Where(n => n.IIdGoiThauId != iIDGoiThauId).ToList();
        //    _itemsChiPhiByGoiThau.AddRange(lstData);
        //    return new ObservableCollection<VdtKhlcNhaThauGoiThauDetailModel>(lstData);
        //}
        #endregion
        #endregion
    }
}
