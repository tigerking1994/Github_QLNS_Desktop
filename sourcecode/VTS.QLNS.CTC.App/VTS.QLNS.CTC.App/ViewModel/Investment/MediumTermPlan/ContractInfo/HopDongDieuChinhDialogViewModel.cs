using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ContractInfo;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo
{
    public class HopDongDieuChinhDialogViewModel : DialogAttachmentViewModelBase<HopDongModel>
    {
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly ILoaiHopDongService _loaiHopDongService;
        private readonly IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IVdtQddtKhlcnhaThauService _khlcntService;
        private readonly IVdtGoiThauService _vdtGoiThauService;
        private readonly IVdtNhaThauService _vdtNhaThauService;
        private readonly IVdtDaTtHopDongService _vdtHopDongService;
        private readonly IApproveProjectService _approveProjectService;
        private static string[] _lstDonViInclude = new string[] { "0", "1" };

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_CONTRACT_INFO_DIALOG;
        public override string Name => "Thông tin hợp đồng";
        public override string Title => "Thông tin hợp đồng";
        public override string Description => !IsUpdated ? "Điều chỉnh thông tin hợp đồng" : "Cập nhật thông tin hợp đồng điều chỉnh";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.ContractInfo.HopDongDieuChinhDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_THONGTIN_HOPDONG;

        #region Items
        private string _sHeaderGiaTriPheDuyet;
        public string SHeaderGiaTriPheDuyet
        {
            get => _sHeaderGiaTriPheDuyet;
            set => SetProperty(ref _sHeaderGiaTriPheDuyet, value);
        }

        public bool IsUpdated { get; set; }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                LoadDuAnByDonVi();
            }
        }

        public ObservableCollection<DonViModel> DonViModels { get; set; }

        private ObservableCollection<VdtDaDuAnModel> _vdtDaDuAnModels;
        public ObservableCollection<VdtDaDuAnModel> VdtDaDuAnModels
        {
            get => _vdtDaDuAnModels;
            set => SetProperty(ref _vdtDaDuAnModels, value);
        }

        private VdtDaDuAnModel _selectedDuAn;
        public VdtDaDuAnModel SelectedDuAn
        {
            get => _selectedDuAn;
            set
            {
                SetProperty(ref _selectedDuAn, value);
                if (_selectedDuAn != null)
                    LoadGoiThauData();
            }
        }

        private ComboboxItem _selectedNhaThauDaiDien;
        public ComboboxItem SelectedNhaThauDaiDien
        {
            get => _selectedNhaThauDaiDien;
            set => SetProperty(ref _selectedNhaThauDaiDien, value);
        }

        private ObservableCollection<HopDongGoiThauQueryModel> _listGoiThau;
        public ObservableCollection<HopDongGoiThauQueryModel> ListGoiThau
        {
            get => _listGoiThau;
            set => SetProperty(ref _listGoiThau, value);
        }

        private HopDongGoiThauQueryModel _selectedGoiThau;
        public HopDongGoiThauQueryModel SelectedGoiThau
        {
            get => _selectedGoiThau;
            set
            {
                SetProperty(ref _selectedGoiThau, value);
            }
        }

        private ObservableCollection<HopDongChiPhiQueryModel> _listChiPhi;
        public ObservableCollection<HopDongChiPhiQueryModel> ListChiPhi
        {
            get => _listChiPhi;
            set => SetProperty(ref _listChiPhi, value);
        }

        private HopDongChiPhiQueryModel _selectedChiPhi;
        public HopDongChiPhiQueryModel SelectedChiPhi
        {
            get => _selectedChiPhi;
            set
            {
                SetProperty(ref _selectedChiPhi, value);
            }
        }

        private ObservableCollection<HopDongChiPhiHangMucQueryModel> _listHangMuc;

        public HopDongChiPhiQueryModel DisplayedChiPhi { get; private set; }
        public ObservableCollection<HopDongChiPhiHangMucQueryModel> ListHangMuc
        {
            get => _listHangMuc;
            set => SetProperty(ref _listHangMuc, value);
        }

        private HopDongChiPhiHangMucQueryModel _selectedHangMuc;
        public HopDongChiPhiHangMucQueryModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set
            {
                SetProperty(ref _selectedHangMuc, value);
            }
        }

        public ThongTinHopDongModel VdtDaTtHopDongModel { get; set; }

        internal bool IsViewOnly;

        public HopDongModel HopDongGocModel { get; set; }
        public HopDongModel HopDongDCModel { get; set; }

        public bool IsEnableGiaTriHopDong => !ListGoiThau.Any(x => x.IsSelected);
        #endregion

        #region RelayCommand
        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand ShowChiPhiCommand { get; }
        public RelayCommand ShowHangMucDetailCommand { get; }
        public ObservableCollection<ComboboxItem> ItemsPhanLoai { get; set; }
        public ComboboxItem SelectedPhanLoai { get; set; }
        public ObservableCollection<ComboboxItem> ItemsNhaThau { get; private set; }
        #endregion

        #region RelayCommand
        public RelayCommand AddHangMucCommand { get; }
        public RelayCommand AddChildCommand { get; }
        public RelayCommand DeleteHangMucCommand { get; }
        #endregion

        public HopDongDieuChinhDialogViewModel(
            IMapper mapper,
            ILog logger,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtDaDuAnService vdtDaDuAnService,
            ILoaiHopDongService loaiHopDongService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IVdtQddtKhlcnhaThauService khlcntService,
            IVdtGoiThauService vdtGoiThauService,
            IVdtNhaThauService vdtNhaThauService,
            IVdtDaTtHopDongService vdtHopDongService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IApproveProjectService approveProjectService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _khlcntService = khlcntService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _loaiHopDongService = loaiHopDongService;
            _vdtGoiThauService = vdtGoiThauService;
            _vdtNhaThauService = vdtNhaThauService;
            _vdtHopDongService = vdtHopDongService;
            _approveProjectService = approveProjectService;

            ShowChiPhiCommand = new RelayCommand(obj => LoadGoiThauChiPhi());
            ShowHangMucDetailCommand = new RelayCommand(obj => LoadHangMucByChiPhi());
            DeleteHangMucCommand = new RelayCommand(obj => SelectedHangMuc.IsDeleted = !SelectedHangMuc.IsDeleted);
            AddHangMucCommand = new RelayCommand(obj => AddHangMuc());
            AddChildCommand = new RelayCommand(obj => AddHangMucChild());
        }

        public override void Init()
        {
            SelectedDuAn = null;
            ListGoiThau = new ObservableCollection<HopDongGoiThauQueryModel>();
            ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>();
            ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
            LoadPhanLoai();
            LoadNhaThau();
            LoadAttach();
            SHeaderGiaTriPheDuyet = "Giá trị được duyệt";
            IEnumerable<DonVi> donvis = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            DonViModels = _mapper.Map<ObservableCollection<DonViModel>>(donvis);
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            VdtDaTtHopDong hopdong = null;
            if (IsUpdated)
            {
                hopdong = _vdtHopDongService.Find(HopDongDCModel.Id);
            }
            else
            {
                hopdong = _vdtHopDongService.Find(HopDongGocModel.Id);
            }
            VdtDaTtHopDongModel = _mapper.Map<ThongTinHopDongModel>(hopdong);
            var duan = _vdtDaDuAnService.Find(VdtDaTtHopDongModel.IIdDuAnId);
            SelectedDonVi = DonViModels.FirstOrDefault(s => s.IIDMaDonVi.Equals(duan.IIdMaDonViThucHienDuAn));
            // gói thầu sẽ được load ra ngay sau dự án
            SelectedDuAn = VdtDaDuAnModels.FirstOrDefault(s => s.Id.Equals(duan.Id));
            SelectedNhaThauDaiDien = ItemsNhaThau.FirstOrDefault(s => s.ValueItem.Equals(hopdong.IIdNhaThauThucHienId?.ToString()));
            SelectedPhanLoai = ItemsPhanLoai.FirstOrDefault(s => s.ValueItem.Equals(hopdong.IIdLoaiHopDongId?.ToString()));
            
        }

        private void LoadDuAnByDonVi()
        {
            if (SelectedDonVi == null)
            {
                VdtDaDuAnModels = new ObservableCollection<VdtDaDuAnModel>();
                return;
            }
            IEnumerable<VdtDaDuAn> vdtDaDuAns = _vdtDaDuAnService.FindByMaDonVi(SelectedDonVi.IIDMaDonVi);
            VdtDaDuAnModels = _mapper.Map<ObservableCollection<VdtDaDuAnModel>>(vdtDaDuAns);
        }

        private void LoadGoiThauData()
        {
            IEnumerable<HopDongGoiThauQuery> data = new ObservableCollection<HopDongGoiThauQuery>();
            if (IsUpdated)
                data = _vdtDaGoiThauService.DCFindGoiThauByHopDong(HopDongGocModel.Id, HopDongDCModel.Id);
            else
                data = _vdtDaGoiThauService.DCFindGoiThauByHopDong(HopDongGocModel.Id);
            ListGoiThau = _mapper.Map<ObservableCollection<HopDongGoiThauQueryModel>>(data);
            foreach (var goithau in ListGoiThau)
            {
                goithau.IsSelected = goithau.FGiaTriGoiThau > 0;
                foreach (var chiphi in goithau.ListChiPhi)
                {
                    chiphi.IsSelected = goithau.IsSelected && chiphi.FGiaTriChiPhi > 0;
                    chiphi.IsHangCha = goithau.ListChiPhi.Any(t => chiphi.IdChiPhi.Equals(t.IdChiPhiParent));
                    chiphi.IsDisableGiatriChiPhi = chiphi.IsHangCha || chiphi.ListHangMuc.Count != 0;
                    //chiphi.PropertyChanged += ChiChiFGiaTriChiPhiPropertyChanged;
                    chiphi.PropertyChanged += ChiPhiPropertyChanged;
                    //chiphi.IsEditHangMuc = !chiphi.IsHangCha;
                    foreach (var hangmuc in chiphi.ListHangMuc)
                    {
                        hangmuc.IsSelected = true;
                        hangmuc.IsHangCha = chiphi.ListHangMuc.Any(t => hangmuc.HangMucId.Equals(t.IID_ParentID));
                        hangmuc.PropertyChanged += HangMucPropertyChanged;
                    }
                }
                goithau.PropertyChanged += GoiThauPropertyChanged;
                goithau.PropertyChanged += GoiThauGiaTriHopDongPropertyChanged;
            }
            VdtDaTtHopDongModel.FGiaTriHopDong = ListGoiThau.Where(t => t.IsSelected).Sum(t => t.FGiaTriHopDong);
            
        }

        private void LoadGoiThauChiPhi()
        {
            if (SelectedGoiThau != null)
            {
                SHeaderGiaTriPheDuyet = "Giá trị được duyệt " + _vdtDaGoiThauService.GetTypeOfGoiThau(SelectedGoiThau.GoiThauId);
                ListChiPhi = SelectedGoiThau.ListChiPhi;
                ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
            }
        }

        private void LoadHangMucByChiPhi()
        {
            if (SelectedChiPhi != null)
            {
                DisplayedChiPhi = SelectedChiPhi.clone();
                ListHangMuc = SelectedChiPhi.ListHangMuc;
            }
        }

        private void LoadPhanLoai()
        {
            List<VdtDmLoaiHopDong> listLoaiHopDong = _loaiHopDongService.FindAll().ToList();
            ItemsPhanLoai = new ObservableCollection<ComboboxItem>();
            foreach (VdtDmLoaiHopDong item in listLoaiHopDong)
            {
                ItemsPhanLoai.Add(new ComboboxItem { ValueItem = item.Id.ToString(), DisplayItem = item.STenLoaiHopDong });
            }
            if (ItemsPhanLoai != null && ItemsPhanLoai.Count > 0)
            {
                SelectedPhanLoai = ItemsPhanLoai.FirstOrDefault();
            }
            OnPropertyChanged(nameof(ItemsPhanLoai));
            OnPropertyChanged(nameof(SelectedPhanLoai));
        }

        private void LoadNhaThau()
        {
            ItemsNhaThau = new ObservableCollection<ComboboxItem>();
            var lstData = _vdtNhaThauService.FindAll();
            if (lstData != null)
            {
                ItemsNhaThau = new ObservableCollection<ComboboxItem>(lstData
                    .Select(n => new ComboboxItem() { DisplayItem = n.STenNhaThau, ValueItem = n.Id.ToString() }));
            }
            OnPropertyChanged(nameof(ItemsNhaThau));
        }

        private void GoiThauGiaTriHopDongPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var goithau = sender as HopDongGoiThauQueryModel;
            if (!e.PropertyName.Equals(nameof(goithau.FGiaTriHopDong)))
            {
                return;
            }
            VdtDaTtHopDongModel.FGiaTriHopDong = ListGoiThau.Where(t => t.IsSelected).Sum(t => t.FGiaTriHopDong);
        }

        private void GoiThauPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var goithau = sender as HopDongGoiThauQueryModel;
            if (!e.PropertyName.Equals(nameof(goithau.IsSelected)))
            {
                return;
            }
            if (!goithau.IsSelected)
            {
                DetachEventChiPhi();
                DetachEventHangMuc();
                goithau.FGiaTriGoiThau = 0;
                foreach (var chiphi in goithau.ListChiPhi)
                {
                    chiphi.IsSelected = false;
                }
                if (goithau.IdHopDongGoiThauNhaThau.Equals(SelectedGoiThau?.IdHopDongGoiThauNhaThau))
                {
                    ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
                    ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>();
                }
                AttachEventChiPhi();
                AttachEventHangMuc();
            }
            if (ListGoiThau.Any(x => x.IsSelected))
                VdtDaTtHopDongModel.FGiaTriHopDong = ListGoiThau.Where(t => t.IsSelected).Sum(t => t.FGiaTriHopDong);
            OnPropertyChanged(nameof(IsEnableGiaTriHopDong));
        }

        private void ChiPhiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var chiphi = sender as HopDongChiPhiQueryModel;
            DetachEventChiPhi();
            DetachEventHangMuc();
            if (e.PropertyName.Equals(nameof(chiphi.IsSelected)))
            {
                if (!chiphi.IsSelected && chiphi.IdChiPhi.Equals(SelectedChiPhi?.IdChiPhi))
                {
                    ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
                }

                ChiPhiChildrenSelectionChanged(chiphi);
                ChiPhiParentSelectionChanged(chiphi);
            }
            else if (e.PropertyName.Equals(nameof(chiphi.FGiaTriChiPhi)))
            {
                ChiPhiParentSelectionChanged(chiphi);
            }
            AttachEventChiPhi();
            AttachEventHangMuc();
        }

        private void ChiPhiChildrenSelectionChanged(HopDongChiPhiQueryModel chiphi)
        {
            var chiphiChilren = ListChiPhi.Where(t => chiphi.IdChiPhi.Equals(t.IdChiPhiParent));
            foreach (var c in chiphiChilren)
            {
                c.IsSelected = chiphi.IsSelected;
                if (!c.IsSelected && chiphi.IdChiPhi.Equals(SelectedChiPhi?.IdChiPhi))
                {
                    ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
                }
                ChiPhiChildrenSelectionChanged(c);
            }
        }

        private void ChiPhiParentSelectionChanged(HopDongChiPhiQueryModel chiphi)
        {
            var parent = ListChiPhi.FirstOrDefault(t => t.IdChiPhi.Equals(chiphi.IdChiPhiParent));
            if (parent != null)
            {
                if (chiphi.IsSelected)
                {
                    parent.IsSelected = true;
                }
                else
                {
                    parent.IsSelected = ListChiPhi.Any(t => parent.IdChiPhi.Equals(t.IdChiPhiParent) && t.IsSelected);
                }
                parent.FGiaTriChiPhi = ListChiPhi.Where(t => parent.IdChiPhi.Equals(t.IdChiPhiParent) && t.IsSelected).Sum(t => t.FGiaTriChiPhi);
                ChiPhiParentSelectionChanged(parent);
            }
            CalculateGoiThauData(chiphi);
        }

        private void HangMucPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var hangmuc = sender as HopDongChiPhiHangMucQueryModel;
            if (!e.PropertyName.Equals(nameof(hangmuc.FGiatriSuDung)) && !e.PropertyName.Equals(nameof(hangmuc.IsDeleted)))
            {
                return;
            }
            DetachEventChiPhi();
            DetachEventHangMuc();

            HangMucParentChanged(hangmuc);
            HangMucChildrenChanged(hangmuc);
            // tinh lai tong gia tri chi phi hien tai va chi phi cha
            var SelectedChiPhi = ListChiPhi.FirstOrDefault(t => t.IdChiPhi.Equals(hangmuc.ChiPhiId) && t.IdHopDongGoiThauNhaThau.Equals(hangmuc.IdHopDongGoiThauNhaThau));
            if (SelectedChiPhi != null)
            {
                if (ListHangMuc.Any(t => !t.IsDeleted))
                { 
                    SelectedChiPhi.FGiaTriChiPhi = SelectedChiPhi.ListHangMuc.Where(t => !t.IsHangCha && !t.IsDeleted).Sum(t => t.FGiatriSuDung);
                    SelectedChiPhi.IsDisableGiatriChiPhi = true;
                }
                else
                {
                    //SelectedChiPhi.FGiaTriChiPhi = 0;
                    SelectedChiPhi.IsDisableGiatriChiPhi = false;
                }
                ChiPhiParentSelectionChanged(SelectedChiPhi);
            }
            AttachEventChiPhi();
            AttachEventHangMuc();
        }

        private void HangMucParentChanged(HopDongChiPhiHangMucQueryModel hangmuc)
        {
            var parent = ListHangMuc.FirstOrDefault(t => t.HangMucId.Equals(hangmuc.IID_ParentID));
            if (parent != null)
            {
                if (hangmuc.IsDeleted)
                {
                    parent.IsDeleted = ListHangMuc.Where(t => parent.HangMucId.Equals(t.IID_ParentID)).All(t => t.IsDeleted);
                }
                else
                {
                    parent.IsDeleted = false;
                }
                parent.FGiatriSuDung = ListHangMuc.Where(t => parent.HangMucId.Equals(t.IID_ParentID) && !t.IsDeleted).Sum(t => t.FGiatriSuDung);
                HangMucParentChanged(parent);
            }
        }

        private void HangMucChildrenChanged(HopDongChiPhiHangMucQueryModel hangmuc)
        {
            var hangmucChildren = ListHangMuc.Where(t => hangmuc.HangMucId.Equals(t.IID_ParentID));
            foreach (var c in hangmucChildren)
            {
                c.IsDeleted = hangmuc.IsDeleted;
                HangMucChildrenChanged(c);
            }
        }

        /*private void ChiChiFGiaTriChiPhiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(HopDongChiPhiQueryModel.FGiaTriChiPhi)))
            {
                var chiphi = sender as HopDongChiPhiQueryModel;
                // TinhGiaTriChiPhiTrongGoiThauKhac(chiphi);
            }
        }*/

        /*private void TinhGiaTriChiPhiTrongGoiThauKhac(HopDongChiPhiQueryModel chiphi)
        {
            var listCPTuongUng = ListGoiThau.SelectMany(t => t.ListChiPhi).Where(cp => cp.IdGoiThau.Equals(chiphi.IdGoiThau)
                                    && chiphi.IdChiPhi.Equals(cp.IdChiPhi));
            foreach (var item in listCPTuongUng)
            {
                item.FGiatriChiPhiTrongGoiThauKhac = listCPTuongUng.Where(t => !t.IdHopDongGoiThauNhaThau.Equals(item.IdHopDongGoiThauNhaThau)).Sum(cp => cp.FGiaTriChiPhi);
            }
        }

        private void TinhGiaTriSuDungTrongGoiThauKhac(HopDongGoiThauQueryModel goithau)
        {
            var listGTTuongUng = ListGoiThau.Where(cp => cp.GoiThauId.Equals(goithau.GoiThauId));
            foreach (var item in listGTTuongUng)
            {
                item.FGiatriSuDungTrongGoiThauKhac = listGTTuongUng.Where(t => !t.IdHopDongGoiThauNhaThau.Equals(item.IdHopDongGoiThauNhaThau)).Sum(cp => cp.FGiaTriGoiThau);
            }
        }*/

        private void CalculateGoiThauData(HopDongChiPhiQueryModel chiphi)
        {
            var SelectedGoiThau = ListGoiThau.FirstOrDefault(t => t.IdHopDongGoiThauNhaThau.Equals(chiphi.IdHopDongGoiThauNhaThau));
            if (SelectedGoiThau != null)
            {
                SelectedGoiThau.FGiaTriGoiThau = SelectedGoiThau.ListChiPhi.Where(t => t.IsSelected && !t.IsHangCha).Sum(t => t.FGiaTriChiPhi);
            }
        }

        public override void OnSave(object obj)
        {
            if (!Validate()) return;
            if (!ValidateGoiThau()) return;
            VdtDaTtHopDong hopDongGoc = _vdtHopDongService.Find(HopDongGocModel.Id);
            hopDongGoc.BActive = false;
            VdtDaTtHopDong hopdongDC = _mapper.Map<VdtDaTtHopDong>(VdtDaTtHopDongModel);
            if (!IsUpdated)
            {
                hopdongDC.Id = Guid.NewGuid();
            }
            hopdongDC.BIsGoc = false;
            hopdongDC.BActive = true;
            hopdongDC.IIdLoaiHopDongId = Guid.Parse(SelectedPhanLoai.ValueItem);
            hopdongDC.IIdNhaThauThucHienId = Guid.Parse(SelectedNhaThauDaiDien.ValueItem);
            hopdongDC.IIdHopDongGocId = hopDongGoc.Id;
            hopdongDC.IIdParentId = hopDongGoc.Id;
            hopdongDC.ILandieuchinh = hopDongGoc.ILandieuchinh.HasValue ? hopDongGoc.ILandieuchinh + 1 : 1;
            hopdongDC.FTienHopDong = VdtDaTtHopDongModel.FGiaTriHopDong;
            var objNhaThau = _vdtNhaThauService.FindById(hopdongDC.IIdNhaThauThucHienId.Value);
            if (objNhaThau != null)
            {
                hopdongDC.SSoTaiKhoan = objNhaThau.SSoTaiKhoan;
                hopdongDC.SNganHang = objNhaThau.SNganHang;
            }
            var savedGoiThau = new ObservableCollection<HopDongGoiThauQueryModel>(ListGoiThau.Where(t => t.IsSelected));
            foreach (var gt in savedGoiThau)
            {
                gt.HopDongId = hopdongDC.Id;
                gt.ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>(gt.ListChiPhi.Where(cp => cp.IsSelected));
                foreach (var cp in gt.ListChiPhi)
                {
                    cp.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>(cp.ListHangMuc.Where(hm => !hm.IsDeleted));
                    // renew hang muc id để không bị trùng với hạng mục trong bàng dự toán hang mục
                    foreach (var hm in cp.ListHangMuc)
                    {
                        Guid newHmId = Guid.NewGuid();
                        IEnumerable<HopDongChiPhiHangMucQueryModel> children = cp.ListHangMuc.Where(t => hm.HangMucId.Equals(t.IID_ParentID));
                        foreach (var child in children)
                        {
                            child.IID_ParentID = newHmId;
                        }
                        hm.HangMucId = newHmId;
                    }
                    cp.ListVdtDaHopDongDmHangMuc = cp.ListHangMuc.Select(hm => hm.ToVdtDaHopDongDmHangMuc()).ToList();
                }
            }
            List<VdtDaHopDongDmHangMuc> vdtDaHopDongDmHangMucs = savedGoiThau.SelectMany(gt => gt.ListChiPhi).SelectMany(cp => cp.ListHangMuc).
                Select(hm => hm.ToVdtDaHopDongDmHangMuc()).ToList();
            hopdongDC.ListGoiThau = _mapper.Map<ObservableCollection<VdtDaHopDongGoiThauNhaThau>>(savedGoiThau);

            _vdtHopDongService.SaveHopDongDC(hopdongDC, hopDongGoc);
            SaveAttachment(hopdongDC.Id);
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            SavedAction?.Invoke(hopdongDC);
            HopDongDieuChinhDialog dlg = obj as HopDongDieuChinhDialog;
            dlg.Close();
        }

        private void AddHangMuc()
        {
            HopDongChiPhiHangMucQueryModel hangmuc = new HopDongChiPhiHangMucQueryModel();
            hangmuc.IdHopDongGoiThauNhaThau = DisplayedChiPhi.IdHopDongGoiThauNhaThau;
            hangmuc.GoiThauId = DisplayedChiPhi.IdGoiThau;
            hangmuc.ChiPhiId = DisplayedChiPhi.IdChiPhi;
            hangmuc.HangMucId = Guid.NewGuid();
            hangmuc.FGiatriSuDung = 0;
            hangmuc.MaOrDer = GetHMOrder(false);
            if (SelectedHangMuc == null)
            {
                hangmuc.STenHangMuc = "";
            }
            else
            {
                hangmuc.IID_ParentID = SelectedHangMuc.IID_ParentID;
                hangmuc.STenHangMuc = SelectedHangMuc.STenHangMuc;
            }
            hangmuc.PropertyChanged += HangMucPropertyChanged;
            ListHangMuc.Add(hangmuc);
            var selectedChiPhi = ListChiPhi.FirstOrDefault(cp => cp.IdHopDongGoiThauNhaThau.Equals(DisplayedChiPhi.IdHopDongGoiThauNhaThau)
                                            && cp.IdChiPhi.Equals(DisplayedChiPhi.IdChiPhi));
            selectedChiPhi.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>(ListHangMuc.OrderBy(t => t.MaOrDer));
            ListHangMuc = selectedChiPhi.ListHangMuc;
        }

        private void AddHangMucChild()
        {
            if (SelectedHangMuc == null)
            {
                return;
            }
            SelectedHangMuc.IsHangCha = true;
            DetachEventChiPhi();
            DetachEventHangMuc();
            SelectedHangMuc.IsHangCha = true;
            HopDongChiPhiHangMucQueryModel hangmuc = new HopDongChiPhiHangMucQueryModel();
            hangmuc.IdHopDongGoiThauNhaThau = DisplayedChiPhi.IdHopDongGoiThauNhaThau;
            hangmuc.GoiThauId = DisplayedChiPhi.IdGoiThau;
            hangmuc.ChiPhiId = DisplayedChiPhi.IdChiPhi;
            hangmuc.HangMucId = Guid.NewGuid();
            hangmuc.FGiatriSuDung = 0;
            hangmuc.IID_ParentID = SelectedHangMuc.HangMucId;
            hangmuc.STenHangMuc = SelectedHangMuc.STenHangMuc;
            hangmuc.MaOrDer = GetHMOrder(true);
            ListHangMuc.Add(hangmuc);
            HangMucParentChanged(hangmuc);
            // tinh lai tong gia tri chi phi hien tai va chi phi cha
            var SelectedChiPhi = ListChiPhi.FirstOrDefault(t => t.IdChiPhi.Equals(hangmuc.ChiPhiId) && t.IdHopDongGoiThauNhaThau.Equals(hangmuc.IdHopDongGoiThauNhaThau));
            SelectedChiPhi.FGiaTriChiPhi = SelectedChiPhi.ListHangMuc.Where(t => !t.IsHangCha && !t.IsDeleted).Sum(t => t.FGiatriSuDung);
            SelectedChiPhi.IsDisableGiatriChiPhi = true;
            ChiPhiParentSelectionChanged(SelectedChiPhi);

            SelectedChiPhi.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>(ListHangMuc.OrderBy(t => t.MaOrDer));
            ListHangMuc = SelectedChiPhi.ListHangMuc;

            AttachEventChiPhi();
            AttachEventHangMuc();
        }

        private string GetHMOrder(bool isAddChild)
        {
            if (ListHangMuc.Count == 0)
            {
                return "1";
            }
            if (!isAddChild)
            {
                if (SelectedHangMuc == null || !SelectedHangMuc.MaOrDer.Contains("_"))
                {
                    int n = 0;
                    var hangmuc = ListHangMuc.Where(t => int.TryParse(t.MaOrDer, out n)).OrderBy(t => int.Parse(t.MaOrDer)).LastOrDefault();
                    if (hangmuc == null)
                    {
                        return "1";
                    }
                    else
                    {
                        int index = ListHangMuc.IndexOf(hangmuc);
                        string order = int.Parse(hangmuc.MaOrDer) + 1 + "";
                        return order;
                    }
                }
                else
                {
                    var currentOrder = SelectedHangMuc.MaOrDer;
                    Guid? hangmucParentID = SelectedHangMuc.IID_ParentID;
                    var hangmucChildren = ListHangMuc.Where(t => t.IID_ParentID.Equals(hangmucParentID)).OrderBy(s => int.Parse(s.MaOrDer.Substring(s.MaOrDer.LastIndexOf("_") + 1)));
                    string prefix = currentOrder.Substring(0, currentOrder.IndexOf("_") + 1);
                    var lastHM = hangmucChildren.Last();
                    var number = lastHM.MaOrDer.Substring(lastHM.MaOrDer.LastIndexOf("_") + 1);
                    int index = ListHangMuc.IndexOf(lastHM);
                    string order = prefix + (int.Parse(number) + 1);
                    return order;
                }
            }
            else
            {
                var currentOrder = SelectedHangMuc.MaOrDer;
                var hangmucChildren = ListHangMuc.Where(t => t.IID_ParentID.Equals(SelectedHangMuc.HangMucId)).OrderBy(s => int.Parse(s.MaOrDer.Substring(s.MaOrDer.LastIndexOf("_") + 1)));
                string prefix = currentOrder + "_";
                var lastHM = hangmucChildren.LastOrDefault();
                if (lastHM != null)
                {
                    var number = lastHM.MaOrDer.Substring(lastHM.MaOrDer.LastIndexOf("_") + 1);
                    int index = ListHangMuc.IndexOf(lastHM);
                    string order = prefix + (int.Parse(number) + 1);
                    return order;
                }
                else
                {
                    string order = prefix + 1;
                    return order;
                }
            }
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();
            if (SelectedDonVi == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị");
            }
            if (SelectedDuAn == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Dự án");
            }
            if (string.IsNullOrEmpty(VdtDaTtHopDongModel.SSoHopDong))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số hợp đồng");
            }
            if (string.IsNullOrEmpty(VdtDaTtHopDongModel.STenHopDong))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Tên hợp đồng");
            }
            if (SelectedPhanLoai == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Phân loại");
            }
            if (VdtDaTtHopDongModel.DNgayHopDong == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày lập");
            }
            if (SelectedNhaThauDaiDien == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nhà thầu đại diện");
            }
            if (VdtDaTtHopDongModel.IThoiGianThucHien <= 0)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire, "TG thực hiện");
            }
            if (messageBuilder.Length != 0)
            {
                System.Windows.Forms.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool ValidateGoiThau()
        {
            List<string> message = new List<string>();
            if (!ListGoiThau.Any(n => n.IsSelected))
            {
                message.Add(string.Format(Resources.MsgErrorDataEmpty, "gói thầu"));
            }
            else
            {
                if (ListGoiThau.Any(n => n.IsSelected && !n.NhaThauId.HasValue))
                {
                    message.Add(string.Format(Resources.MsgErrorDataEmpty, "nhà thầu"));
                }
                foreach (var i in ListGoiThau)
                {
                    if (i.IsSelected && (i.FGiaTriHopDong == 0))
                    {
                        message.Add(string.Format("Giá trị hợp đồng của gói thầu {0} phải lớn hơn 0.", i.STenGoiThau));
                    }
                }
            }

            if (message.Count != 0)
            {
                MessageBox.Show(String.Join(Environment.NewLine, message), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void DetachEventChiPhi()
        {
            foreach (var item in ListGoiThau.SelectMany(t => t.ListChiPhi))
            {
                item.PropertyChanged -= ChiPhiPropertyChanged;
            }
        }

        private void DetachEventHangMuc()
        {
            foreach (var item in ListGoiThau.SelectMany(t => t.ListChiPhi).SelectMany(cp => cp.ListHangMuc))
            {
                item.PropertyChanged -= HangMucPropertyChanged;
            }
        }

        private void AttachEventChiPhi()
        {
            foreach (var item in ListGoiThau.SelectMany(t => t.ListChiPhi))
            {
                item.PropertyChanged += ChiPhiPropertyChanged;
            }
        }

        private void AttachEventHangMuc()
        {
            foreach (var item in ListGoiThau.SelectMany(t => t.ListChiPhi).SelectMany(cp => cp.ListHangMuc))
            {
                item.PropertyChanged += HangMucPropertyChanged;
            }
        }
    }
}
