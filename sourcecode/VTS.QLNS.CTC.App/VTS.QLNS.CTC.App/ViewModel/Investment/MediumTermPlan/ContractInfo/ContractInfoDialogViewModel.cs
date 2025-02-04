using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Mapper;
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
    public class ContractInfoDialogViewModel : DialogAttachmentViewModelBase<HopDongModel>
    {
        #region Private
        private static string[] _lstDonViInclude = new string[] { "0", "1" };
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaDuAnService _vdtDaDuAnService;
        private readonly ILoaiHopDongService _loaiHopDongService;
        private readonly IVdtGoiThauService _vdtGoiThauService;
        private readonly IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IVdtQddtKhlcnhaThauService _khlcntService;
        private readonly IVdtNhaThauService _vdtNhaThauService;
        private readonly IVdtDaTtHopDongService _vdtHopDongService;
        private readonly IApproveProjectService _approveProjectService;
        private VdtDaDuAn _objDuAn;
        private Dictionary<Guid, List<HopDongHangMucModel>> _dicHangMuc;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_CONTRACT_INFO_DIALOG;
        public override string Name => "Thông tin hợp đồng";
        public override string Title => "Thông tin hợp đồng";
        public override string Description => string.Format("{0} thông tin hợp đồng", IsDetail ? "Chi tiết": (Model.Id != Guid.Empty ? "Cập nhật" : "Thêm mới"));
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.ContractInfo.ContractInfoDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_THONGTIN_HOPDONG;

        #region Items
        private string _sHeaderGiaTriPheDuyet;
        public string SHeaderGiaTriPheDuyet
        {
            get => _sHeaderGiaTriPheDuyet;
            set => SetProperty(ref _sHeaderGiaTriPheDuyet, value);
        }
        private bool _isDetail;
        public bool IsDetail
        {
            get => _isDetail;
            set => SetProperty(ref _isDetail, value);
        }

        private bool isViewOnly;
        public bool IsViewOnly { 
            get => isViewOnly;
            set {
                SetProperty(ref isViewOnly, value);
                IsShowGiaTriConLai = !IsViewOnly;
            } 
        }
        public bool IsShowGiaTriConLai { get; set; }

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

        public bool IsEnableGiaTriHopDong => !ListGoiThau.Any(x => x.IsSelected) && !IsViewOnly;

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
            set { 
                SetProperty(ref _selectedNhaThauDaiDien, value);
                LoadListSTKNhaThau();
            }
        }

        private ObservableCollection<HopDongGoiThauQueryModel> _listGoiThau;
        public ObservableCollection<HopDongGoiThauQueryModel> ListGoiThau
        {
            get => _listGoiThau;
            set => SetProperty(ref _listGoiThau, value);
        }

        private ObservableCollection<ComboboxItem> _listSTKNhaThau;
        public ObservableCollection<ComboboxItem> ListSTKNhaThau
        {
            get => _listSTKNhaThau;
            set => SetProperty(ref _listSTKNhaThau, value);
        }

        private ComboboxItem _selectedSTKNhaThau;
        public ComboboxItem SelectedSTKNhaThau
        {
            get => _selectedSTKNhaThau;
            set
            {
                SetProperty(ref _selectedSTKNhaThau, value);
                if (_selectedSTKNhaThau != null && VdtDaTtHopDongModel != null)                    
                    VdtDaTtHopDongModel.SSoTaiKhoan = _selectedSTKNhaThau.ValueItem;
            }
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
            set
            {
                TaoMaOrders(value.ToList());
                SetProperty(ref _listChiPhi, value);
                OnPropertyChanged(nameof(ListChiPhi));
            }
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

        private Guid iDHopDongGoc;
        public Guid IdHopDongGoc
        {
            get => iDHopDongGoc;
            set => SetProperty(ref iDHopDongGoc, value);
        }

        public ThongTinHopDongModel VdtDaTtHopDongModel { get; set; }
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
        public bool IsEditDuAn => Model.Id.IsNullOrEmpty();
        #endregion

        public ContractInfoDialogViewModel(
            IMapper mapper,
            ILog logger,
            INsDonViService nsDonViService,
            ISessionService sessionService,
            IVdtDaDuAnService vdtDaDuAnService,
            ILoaiHopDongService loaiHopDongService,
            IVdtGoiThauService vdtGoiThauService,
            IVdtNhaThauService vdtNhaThauService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IVdtQddtKhlcnhaThauService khlcntService,
        IVdtDaTtHopDongService vdtHopDongService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
            IApproveProjectService approveProjectService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _khlcntService = khlcntService;
            _nsDonViService = nsDonViService;
            _vdtDaDuAnService = vdtDaDuAnService;
            _loaiHopDongService = loaiHopDongService;
            _vdtGoiThauService = vdtGoiThauService;
            _vdtNhaThauService = vdtNhaThauService;
            _vdtHopDongService = vdtHopDongService;
            _approveProjectService = approveProjectService;

            ShowChiPhiCommand = new RelayCommand(obj => LoadGoiThauChiPhi());
            ShowHangMucDetailCommand = new RelayCommand(obj => LoadHangMucByChiPhi());
            AddCommand = new RelayCommand(obj => AddGoiThauNhaThau());
            RefreshCommand = new RelayCommand(obj => OnRefresh());
        }

        private void LoadHangMucByChiPhi()
        {
            if (SelectedChiPhi != null)
            {
                ListHangMuc = SelectedChiPhi.ListHangMuc;
            }
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

        public override void Init()
        {
            SelectedDuAn = null;
            ListGoiThau = new ObservableCollection<HopDongGoiThauQueryModel>();
            ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>();
            ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
            LoadPhanLoai();
            LoadAttach();
            SHeaderGiaTriPheDuyet = "Giá trị được duyệt";
            IEnumerable<DonVi> donvis = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            List<DonVi> lstUnitViaUser = new List<DonVi>();
            var lstUnitManager = _sessionService.Current.IdsDonViQuanLy;
            List<string> lstDv = new List<string>();
            if (lstUnitManager.Contains(","))
            {
                lstDv = lstUnitManager.Split(",").ToList();
            }
            else
            {
                lstDv.Add(lstUnitManager);
            }

            donvis.Select(item =>
            {
                if (lstDv.Contains(item.IIDMaDonVi))
                {
                    lstUnitViaUser.Add(item);
                }
                return item;
            }).ToList();

            DonViModels = _mapper.Map<ObservableCollection<DonViModel>>(lstUnitViaUser);
            LoadNhaThau();
            LoadData();
            OnPropertyChanged(nameof(IsEditDuAn));
        }

        public override void LoadData(params object[] args)
        {
            if (Model.Id.IsNullOrEmpty())
            {
                VdtDaTtHopDongModel = new ThongTinHopDongModel();
                VdtDaTtHopDongModel.DNgayHopDong = DateTime.Now;
                VdtDaTtHopDongModel.DBatDauBaoLanhHopDong = DateTime.Now;
                VdtDaTtHopDongModel.DKetThucBaoLanhHopDong = DateTime.Now;
            }
            else
            {
                var hopdong = _vdtHopDongService.Find(Model.Id);
                VdtDaTtHopDongModel = _mapper.Map<ThongTinHopDongModel>(hopdong);
                var duan = _vdtDaDuAnService.Find(VdtDaTtHopDongModel.IIdDuAnId);
                SelectedDonVi = DonViModels.FirstOrDefault(s => s.IIDMaDonVi.Equals(duan.IIdMaDonViThucHienDuAn));
                SelectedDuAn = VdtDaDuAnModels.FirstOrDefault(s => s.Id.Equals(duan.Id)); // khi dự án được chọn thì load các gói thầu
                SelectedNhaThauDaiDien = ItemsNhaThau.FirstOrDefault(s => s.ValueItem.Equals(hopdong.IIdNhaThauThucHienId?.ToString()));
                SelectedPhanLoai = ItemsPhanLoai.FirstOrDefault(s => s.ValueItem.Equals(hopdong.IIdLoaiHopDongId?.ToString()));
                if(!String.IsNullOrEmpty(hopdong.SSoTaiKhoan))   SelectedSTKNhaThau = ListSTKNhaThau.FirstOrDefault(x => x.ValueItem.Equals(hopdong.SSoTaiKhoan));
                if (!ListGoiThau.Any(x => x.IsSelected)) VdtDaTtHopDongModel.FGiaTriHopDong = VdtDaTtHopDongModel.FTienHopDong ?? 0;
            }            
        }

        private void LoadDuAnByDonVi()
        {
            if (SelectedDonVi == null)
            {
                VdtDaDuAnModels = new ObservableCollection<VdtDaDuAnModel>();
                return;
            }
            //IEnumerable<VdtDaDuAn> vdtDaDuAns = _vdtDaDuAnService.FindDuanCreatedKHLCNT(SelectedDonVi.IIDMaDonVi).Select(x => { x.STenDuAn = x.SMaDuAn + "-" + x.STenDuAn; return x; });
            IEnumerable<VdtDaDuAn> vdtDaDuAns = _vdtDaDuAnService.FindDuanCreatedKHLCNT(SelectedDonVi.IIDMaDonVi);
            VdtDaDuAnModels = _mapper.Map<ObservableCollection<VdtDaDuAnModel>>(vdtDaDuAns);
        }

        private void LoadGoiThauData()
        {
            IEnumerable<HopDongGoiThauQuery> data = new ObservableCollection<HopDongGoiThauQuery>();
            if (Model.Id.IsNullOrEmpty())
            {
                data = _vdtDaGoiThauService.FindGoiThauByDuAn(SelectedDuAn.Id);
                ListGoiThau = _mapper.Map<ObservableCollection<HopDongGoiThauQueryModel>>(data);
                //foreach(var item in ListGoiThau)
                //{
                //    item.NhaThauId = ItemsNhaThau.Select(x => Guid.Parse( x.ValueItem))?.First();
                //}    


            }
            else
            {
                if (IsViewOnly)
                {
                    data = _vdtDaGoiThauService.DCFindGoiThauByHopDong(IdHopDongGoc, Model.Id);
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
                }
                else { 
                    data = _vdtDaGoiThauService.FindGoiThauByHopDong(Model.Id);
                    ListGoiThau = _mapper.Map<ObservableCollection<HopDongGoiThauQueryModel>>(data);
                    foreach (var goithau in ListGoiThau)
                    {
                        goithau.IsSelected = true;
                        foreach (var chiphi in goithau.ListChiPhi)
                        {
                            TinhGiaTriChiPhiTrongGoiThauKhac(chiphi);
                            chiphi.IsSelected = !chiphi.IsDisabledSelection && chiphi.FGiaTriChiPhi > 0;
                            foreach (var hangmuc in chiphi.ListHangMuc)
                            {
                                TinhGiaTriHangMucTrongGoiThauKhac(hangmuc);
                                hangmuc.IsSelected = !hangmuc.IsDisabledSelection && hangmuc.FGiatriSuDung > 0;
                            }
                        }
                        TinhGiaTriSuDungTrongGoiThauKhac(goithau);   
                    }
                }
                
            }
            foreach (var goithau in ListGoiThau)
            {
                foreach (var chiphi in goithau.ListChiPhi)
                {
                    chiphi.IsHangCha = goithau.ListChiPhi.Any(t => chiphi.IdChiPhi.Equals(t.IdChiPhiParent));
                    chiphi.PropertyChanged += ChiChiFGiaTriChiPhiPropertyChanged;
                    chiphi.PropertyChanged += ChiPhiPropertyChanged;
                    //chiphi.IsEditHangMuc = !chiphi.IsHangCha;
                    foreach (var hangmuc in chiphi.ListHangMuc)
                    {
                        hangmuc.IsHangCha = chiphi.ListHangMuc.Any(t => hangmuc.HangMucId.Equals(t.IID_ParentID));
                        hangmuc.PropertyChanged += HangMucPropertyChanged;
                        hangmuc.PropertyChanged += HangMucFGiatriSuDungPropertyChanged;
                    }
                }
                goithau.PropertyChanged += GoiThauPropertyChanged;
                goithau.PropertyChanged += ThoiGianThucHienGoiThauPropertyChanged;
                goithau.PropertyChanged += GiaTriGoiThauPropertyChanged;
                goithau.PropertyChanged += GoiThauGiaTriHopDongPropertyChanged;
            }

            if (ListGoiThau.Any(x => x.IsSelected)) VdtDaTtHopDongModel.FGiaTriHopDong = ListGoiThau.Where(t => t.IsSelected).Sum(t => t.FGiaTriHopDong);
        }


        private void ThoiGianThucHienGoiThauPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var goithau = sender as HopDongGoiThauQueryModel;
            if (!e.PropertyName.Equals(nameof(goithau.IsSelected)))
            {
                return;
            }
            //int iTongThoiGianThucHien = ListGoiThau.Where(t => t.IsSelected).Sum(t => Convert.ToInt32(t.SThoiGianThucHien));
            //VdtDaTtHopDongModel.IThoiGianThucHien = iTongThoiGianThucHien;
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

        private void LoadListSTKNhaThau()
        {
            ListSTKNhaThau = new ObservableCollection<ComboboxItem>();
            if(SelectedNhaThauDaiDien != null)
            {
                List<string> listSTK = _vdtNhaThauService.GetListSTKByNhaThau(Guid.Parse(SelectedNhaThauDaiDien.ValueItem)).ToList();
                listSTK.ForEach(stk =>
                {
                    if(!String.IsNullOrEmpty(stk))
                    {
                        ComboboxItem stkComboBox = new ComboboxItem() { DisplayItem = stk, ValueItem = stk };
                        ListSTKNhaThau.Add(stkComboBox);
                    }
                });
                SelectedSTKNhaThau = ListSTKNhaThau.FirstOrDefault();
            }            
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
                TinhGiaTriSuDungTrongGoiThauKhac(goithau);
                foreach (var chiphi in goithau.ListChiPhi)
                {
                    chiphi.IsSelected = false;
                    chiphi.FGiaTriChiPhi = 0;
                    foreach (var hm in chiphi.ListHangMuc)
                    {
                        hm.IsSelected = false;
                        hm.FGiatriSuDung = 0;
                    }
                }
                if (goithau.IdHopDongGoiThauNhaThau.Equals(SelectedGoiThau?.IdHopDongGoiThauNhaThau))
                {
                    ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
                    ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>();
                }
                AttachEventChiPhi();
                AttachEventHangMuc();
            }
            if (ListGoiThau.Any(x => x.IsSelected)) VdtDaTtHopDongModel.FGiaTriHopDong = ListGoiThau.Where(t => t.IsSelected).Sum(t => t.FGiaTriHopDong);

            // đổi lại nghiệp vụ là khi chọn gói thầu nào thì chi phí hạng mục đều được chọn hết.
            foreach (var goiThau in ListGoiThau.Where(x => x.IsSelected))
            {
                goiThau.ListChiPhi.ForAll(f =>
                {
                    f.IsSelected = true;
                });
                foreach (var chiPhi in goiThau.ListChiPhi.Where(x => x.IsSelected))
                {
                    chiPhi.ListHangMuc.ForAll(f =>
                    {
                        f.IsSelected = true;
                    });
                }
            }
            OnPropertyChanged(nameof(IsEnableGiaTriHopDong));
        }

        private void ChiPhiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var chiphi = sender as HopDongChiPhiQueryModel;
            if (e.PropertyName.Equals(nameof(chiphi.IsSelected)))
            {
                DetachEventChiPhi();
                DetachEventHangMuc();

                if (chiphi.IsSelected)
                {
                    chiphi.FGiaTriChiPhi = chiphi.FTienCoTheSD - chiphi.FGiatriChiPhiTrongGoiThauKhac;
                }
                else
                {
                    chiphi.FGiaTriChiPhi = 0;
                    // uncheck all hang muc
                    foreach (var hm in chiphi.ListHangMuc)
                    {
                        hm.IsSelected = false;
                        hm.FGiatriSuDung = 0;
                    }
                    // ẩn hạng mục đi nếu uncheck selected chi phí
                    if (chiphi.IdChiPhi.Equals(SelectedChiPhi?.IdChiPhi))
                    {
                        if(ListHangMuc != null)
                        {
                            foreach (var hm in ListHangMuc)
                            {
                                if(hm.ChiPhiId == chiphi.IdChiPhi)
                                {
                                    hm.IsChecked = false;
                                }
                            }
                        }
                    }
                }

                ChiPhiChildrenSelectionChanged(chiphi);
                ChiPhiParentSelectionChanged(chiphi);

                AttachEventChiPhi();
                AttachEventHangMuc();
            }
        }

        private void ChiPhiChildrenSelectionChanged(HopDongChiPhiQueryModel chiphi)
        {
            var chiphiChilren = ListChiPhi.Where(t => chiphi.IdChiPhi.Equals(t.IdChiPhiParent));
            foreach (var c in chiphiChilren)
            {
                if (c.IsDisabledSelection)
                {
                    continue;
                }
                c.IsSelected = chiphi.IsSelected;
                if (chiphi.IsSelected)
                {
                    c.FGiaTriChiPhi = c.FTienCoTheSD - c.FGiatriChiPhiTrongGoiThauKhac;
                }
                else
                {
                    c.FGiaTriChiPhi = 0;
                    // uncheck all hang muc
                    foreach (var hm in chiphi.ListHangMuc)
                    {
                        hm.IsSelected = false;
                        hm.FGiatriSuDung = 0;
                    }
                    // ẩn hạng mục đi nếu uncheck selected chi phí
                    if (chiphi.IdChiPhi.Equals(SelectedChiPhi?.IdChiPhi))
                    {
                        ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
                    }
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

        private void GiaTriGoiThauPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(HopDongGoiThauQueryModel.FGiaTriGoiThau)))
            {
                var goithau = sender as HopDongGoiThauQueryModel;
                TinhGiaTriSuDungTrongGoiThauKhac(goithau);
            }
        }

        private void TinhGiaTriSuDungTrongGoiThauKhac(HopDongGoiThauQueryModel goithau)
        {
            var listGTTuongUng = ListGoiThau.Where(cp => cp.GoiThauId.Equals(goithau.GoiThauId));
            foreach (var item in listGTTuongUng)
            {
                item.FGiatriSuDungTrongGoiThauKhac = listGTTuongUng.Where(t => !t.IdHopDongGoiThauNhaThau.Equals(item.IdHopDongGoiThauNhaThau)).Sum(cp => cp.FGiaTriGoiThau);
            }
        }

        private void ChiChiFGiaTriChiPhiPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(HopDongChiPhiQueryModel.FGiaTriChiPhi)))
            {
                var chiphi = sender as HopDongChiPhiQueryModel;
                TinhGiaTriChiPhiTrongGoiThauKhac(chiphi);
            }
        }

        private void TinhGiaTriChiPhiTrongGoiThauKhac(HopDongChiPhiQueryModel chiphi)
        {
            var listCPTuongUng = ListGoiThau.SelectMany(t => t.ListChiPhi).Where(cp => cp.IdGoiThau.Equals(chiphi.IdGoiThau)
                                    && chiphi.IdChiPhi.Equals(cp.IdChiPhi));
            foreach (var item in listCPTuongUng)
            {
                item.FGiatriChiPhiTrongGoiThauKhac = listCPTuongUng.Where(t => !t.IdHopDongGoiThauNhaThau.Equals(item.IdHopDongGoiThauNhaThau)).Sum(cp => cp.FGiaTriChiPhi);
            }
        }

        private void HangMucFGiatriSuDungPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(HopDongChiPhiHangMucQueryModel.FGiatriSuDung)))
            {
                var hangmuc = sender as HopDongChiPhiHangMucQueryModel;
                TinhGiaTriHangMucTrongGoiThauKhac(hangmuc);
            }
        }

        private void TinhGiaTriHangMucTrongGoiThauKhac(HopDongChiPhiHangMucQueryModel hangmuc)
        {
            var listHangMucTuongUng = ListGoiThau.SelectMany(t => t.ListChiPhi).SelectMany(s => s.ListHangMuc)
                .Where(hm => hm.GoiThauId.Equals(hangmuc.GoiThauId) && hm.ChiPhiId.Equals(hangmuc.ChiPhiId) && hm.HangMucId.Equals(hangmuc.HangMucId));
            foreach (var item in listHangMucTuongUng)
            {
                item.FGiatriSuDungTrongGoiThauKhac = listHangMucTuongUng.Where(t => !t.IdHopDongGoiThauNhaThau.Equals(item.IdHopDongGoiThauNhaThau)).Sum(t => t.FGiatriSuDung);
            }
        }

        private void HangMucPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var hangmuc = sender as HopDongChiPhiHangMucQueryModel;
            if (!e.PropertyName.Equals(nameof(hangmuc.IsSelected)))
            {
                return;
            }
            DetachEventChiPhi();
            DetachEventHangMuc();
            
            if (hangmuc.IsSelected)
            {
                hangmuc.FGiatriSuDung = hangmuc.FTienCoTheSD - hangmuc.FGiatriSuDungTrongGoiThauKhac;
            }
            else
            {
                hangmuc.FGiatriSuDung = 0;
            }
            HangMucChildrenSelectionChanged(hangmuc);
            HangMucParentSelectionChanged(hangmuc);
            // tinh lai tong gia tri chi phi hien tai va chi phi cha
            var SelectedChiPhi = ListChiPhi.FirstOrDefault(t => t.IdChiPhi.Equals(hangmuc.ChiPhiId) && t.IdHopDongGoiThauNhaThau.Equals(hangmuc.IdHopDongGoiThauNhaThau));
            if (SelectedChiPhi != null)
            {
                if (ListHangMuc.Any(t => t.IsSelected))
                    SelectedChiPhi.FGiaTriChiPhi = SelectedChiPhi.ListHangMuc.Where(t => !t.IsHangCha).Sum(t => t.FGiatriSuDung);
                else
                {
                    SelectedChiPhi.FGiaTriChiPhi = SelectedChiPhi.FTienCoTheSD - SelectedChiPhi.FGiatriChiPhiTrongGoiThauKhac;
                }
                ChiPhiParentSelectionChanged(SelectedChiPhi);
            }
            AttachEventChiPhi();
            AttachEventHangMuc();
        }

        private void HangMucChildrenSelectionChanged(HopDongChiPhiHangMucQueryModel hangmuc)
        {
            var hangmucChildren = ListHangMuc.Where(t => hangmuc.HangMucId.Equals(t.IID_ParentID));
            foreach (var c in hangmucChildren)
            {
                if (c.IsDisabledSelection)
                {
                    continue;
                }
                c.IsSelected = hangmuc.IsSelected;
                if (hangmuc.IsSelected)
                {
                    c.FGiatriSuDung = c.FTienCoTheSD - c.FGiatriSuDungTrongGoiThauKhac;
                }
                else
                {
                    c.FGiatriSuDung = 0;
                }
                HangMucChildrenSelectionChanged(c);
            }
        }

        private void HangMucParentSelectionChanged(HopDongChiPhiHangMucQueryModel hangmuc)
        {
            var parent = ListHangMuc.FirstOrDefault(t => t.HangMucId.Equals(hangmuc.IID_ParentID));
            if (parent != null)
            {
                if (hangmuc.IsSelected)
                {
                    parent.IsSelected = true;
                }
                else
                {
                    parent.IsSelected = ListHangMuc.Any(t => parent.HangMucId.Equals(t.IID_ParentID) && t.IsSelected);
                }
                parent.FGiatriSuDung = ListHangMuc.Where(t => parent.HangMucId.Equals(t.IID_ParentID) && t.IsSelected).Sum(t => t.FGiatriSuDung);
                HangMucParentSelectionChanged(parent);
            }
        }

        private void CalculateGoiThauData(HopDongChiPhiQueryModel chiphi)
        {
            var SelectedGoiThau = ListGoiThau.FirstOrDefault(t => t.IdHopDongGoiThauNhaThau.Equals(chiphi.IdHopDongGoiThauNhaThau));
            if (SelectedGoiThau != null)
            {
                SelectedGoiThau.FGiaTriGoiThau = SelectedGoiThau.ListChiPhi.Where(t => t.IsSelected && !t.IsHangCha).Sum(t => t.FGiaTriChiPhi);
            }
        }

        public void OnRefresh()
        {
            LoadGoiThauData();
            ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>();
            ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>();
        }

        private void AddGoiThauNhaThau()
        {
            if (SelectedGoiThau == null)
            {
                return;
            }
            var goithau = SelectedGoiThau.Clone();
            ListGoiThau.Add(goithau);
            foreach (var chiphi in goithau.ListChiPhi)
            {
                chiphi.IsHangCha = goithau.ListChiPhi.Any(t => chiphi.IdChiPhi.Equals(t.IdChiPhiParent));
                chiphi.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                TinhGiaTriChiPhiTrongGoiThauKhac(chiphi);
                chiphi.PropertyChanged += ChiPhiPropertyChanged;
                chiphi.PropertyChanged += ChiChiFGiaTriChiPhiPropertyChanged;
                //chiphi.IsEditHangMuc = !chiphi.IsHangCha;
                foreach (var hangmuc in chiphi.ListHangMuc)
                {
                    hangmuc.IdHopDongGoiThauNhaThau = goithau.IdHopDongGoiThauNhaThau;
                    hangmuc.IsHangCha = chiphi.ListHangMuc.Any(t => hangmuc.HangMucId.Equals(t.IID_ParentID));
                    TinhGiaTriHangMucTrongGoiThauKhac(hangmuc);
                    hangmuc.PropertyChanged += HangMucPropertyChanged;
                    hangmuc.PropertyChanged += HangMucFGiatriSuDungPropertyChanged;
                }
            }
            TinhGiaTriSuDungTrongGoiThauKhac(goithau);
            goithau.PropertyChanged += GoiThauPropertyChanged;
            goithau.PropertyChanged += GiaTriGoiThauPropertyChanged;
            goithau.PropertyChanged += GoiThauGiaTriHopDongPropertyChanged;
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
            if (Model == null || Model.Id == Guid.Empty)
            {
                SelectedNhaThauDaiDien = ItemsNhaThau.FirstOrDefault();
            }    
            OnPropertyChanged(nameof(ItemsNhaThau));
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
            if (VdtDaTtHopDongModel.DKetThucBaoLanhHopDong < VdtDaTtHopDongModel.DBatDauBaoLanhHopDong)
            {
                messageBuilder.AppendFormat(Resources.MsgDateThongTinHopDong);
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
            if (ListGoiThau.Any(n => n.IsSelected))
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

        public override void OnSave(object obj)
        {
            if (!Validate()) return;
            if (!ValidateGoiThau()) return;
            VdtDaTtHopDong hopdong = _mapper.Map<VdtDaTtHopDong>(VdtDaTtHopDongModel);
            if (Model.Id.IsNullOrEmpty())
            {
                hopdong.Id = Guid.NewGuid();
                hopdong.IIdHopDongGocId = hopdong.Id;
                hopdong.BIsGoc = true;
                hopdong.BActive = true;
                hopdong.ITinhTrangHopDong = 1;
                hopdong.SUserCreate = _sessionService.Current.Principal;
                hopdong.DDateCreate = DateTime.Now;
                //hopdong.IsFinal = true;
            }
            else
            {
                hopdong.SUserUpdate = _sessionService.Current.Principal;
                hopdong.DDateUpdate = DateTime.Now;
            }
            hopdong.IIdDuAnId = SelectedDuAn.Id;
            hopdong.IIdLoaiHopDongId = Guid.Parse(SelectedPhanLoai.ValueItem);
            hopdong.IIdNhaThauThucHienId = Guid.Parse(SelectedNhaThauDaiDien.ValueItem);
            hopdong.FTienHopDong = VdtDaTtHopDongModel.FGiaTriHopDong;
            if (hopdong.IIdNhaThauThucHienId.HasValue)
            {
                var objNhaThau = _vdtNhaThauService.FindById(hopdong.IIdNhaThauThucHienId.Value);
                if (objNhaThau != null)
                {
                    //hopdong.SSoTaiKhoan = objNhaThau.SSoTaiKhoan; ---> Do đã cho edit số tài khoản
                    if(SelectedSTKNhaThau != null) hopdong.SSoTaiKhoan = SelectedSTKNhaThau.ValueItem;
                    hopdong.SNganHang = objNhaThau.SNganHang;
                }
            }

            var savedGoiThau = new ObservableCollection<HopDongGoiThauQueryModel>(ListGoiThau.Where(t => t.IsSelected));
            foreach (var gt in savedGoiThau)
            {
                gt.ListChiPhi = new ObservableCollection<HopDongChiPhiQueryModel>(gt.ListChiPhi.Where(cp => cp.IsSelected));
                foreach (var cp in gt.ListChiPhi)
                {
                    cp.ListHangMuc = new ObservableCollection<HopDongChiPhiHangMucQueryModel>(cp.ListHangMuc.Where(hm => hm.IsSelected));
                }
            }
            hopdong.ListGoiThau = _mapper.Map<ObservableCollection<VdtDaHopDongGoiThauNhaThau>>(savedGoiThau);
            
            _vdtHopDongService.SaveHopDong(hopdong);
            SaveAttachment(hopdong.Id);
            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            SavedAction?.Invoke(hopdong);
            ContractInfoDialog contractInfoDialog = obj as ContractInfoDialog;
            contractInfoDialog.Close();
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

        private void TaoMaOrders(List<HopDongChiPhiQueryModel> data)
        {
            int index = 0;
            int currentIdThuTu = 0;
            int subThuTu = 0;
            while (index < data.Count)
            {
                if (!data[index].IdChiPhiParent.HasValue)
                {
                    currentIdThuTu++;
                    data[index].SMaOrder = $"{currentIdThuTu}";
                } else
                {
                    subThuTu++;
                    data[index].SMaOrder = $"{currentIdThuTu}_{subThuTu}";
                }

                index++;
            }
        }
    }
}
