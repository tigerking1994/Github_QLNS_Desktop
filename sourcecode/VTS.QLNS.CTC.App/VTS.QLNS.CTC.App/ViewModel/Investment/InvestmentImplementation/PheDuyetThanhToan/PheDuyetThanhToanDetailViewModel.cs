using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Investment.InvestmentImplementation.PheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan.PrintDialog;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.ThongTriCapPhat.PrintDialog;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.PheDuyetThanhToan
{
    public class PheDuyetThanhToanDetailViewModel : DetailViewModelBase<VdtTtDeNghiThanhToanModel, PheDuyetThanhToanChiTietModel>
    {
        #region Private
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IVdtTtPheDuyetThanhToanChiTietService _pheDuyetThanhToanChiTietService;
        private readonly IVdtTtDeNghiThanhToanKhvService _keHoachVonDeNghiThanhToanService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private readonly INsMucLucNganSachService _mlService;
        private readonly IVdtTtPheDuyetThanhToanService _service;
        private readonly ISessionService _sessionService;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;
        private IMapper _mapper;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly IVdtDuAnHangMucService _vdtDuAnHangMucService;

        private List<int> _lstTypeKhvNamTruoc = new List<int>() { (int)LoaiKeHoachVon.KHVN_NAMTRUOC, (int)LoaiKeHoachVon.KHVU_NAMTRUOC };
        private Dictionary<string, NsMucLucNganSach> _dicMucLuc;
        private Dictionary<Guid, List<MlnsByKeHoachVonQuery>> _dicMucLucByKHV;
        private List<MlnsByKeHoachVonQuery> _lstMucLucAll;
        private List<VdtTtKeHoachVonQuery> _lstKeHoachVonThanhToan;
        private List<VdtTtKeHoachVonQuery> _lstKeHoachVonThuHoi;
        private double _fTamUngNamTruocTN;
        private double _fTamUngNamTruocNN;
        public bool IsEnableButton => !IsReadOnlyTable && !BIsDetail;
        #endregion

        public override string Title => "Quản lý phê duyệt thanh toán";

        private string _name;
        public override string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private Visibility _showLoaiThanhToan;
        public Visibility ShowLoaiThanhToan
        {
            get => _showLoaiThanhToan;
            set => SetProperty(ref _showLoaiThanhToan, value);
        }

        private bool _bIsChangeNgayPheDuyet;
        public bool BIsChangeNgayPheDuyet
        {
            get => _bIsChangeNgayPheDuyet;
            set => SetProperty(ref _bIsChangeNgayPheDuyet, value);
        }

        private bool _bIsDetail;
        public bool BIsDetail
        {
            get => _bIsDetail;
            set => SetProperty(ref _bIsDetail, value);
        }

        private bool _isReadOnlyTable;
        public bool IsReadOnlyTable
        {
            get => _isReadOnlyTable;
            set => SetProperty(ref _isReadOnlyTable, value);
        }

        private DateTime? _dNgayPheDuyet;
        public DateTime? DNgayPheDuyet
        {
            get => _dNgayPheDuyet;
            set => SetProperty(ref _dNgayPheDuyet, value);
        }

        #region data combobox
        private ObservableCollection<ComboboxItem> _itemsPaymentType;
        public ObservableCollection<ComboboxItem> ItemsPaymentType
        {
            get => _itemsPaymentType;
            set => SetProperty(ref _itemsPaymentType, value);
        }

        private ObservableCollection<ComboboxItem> _itemsYearType;
        public ObservableCollection<ComboboxItem> ItemsYearType
        {
            get => _itemsYearType;
            set => SetProperty(ref _itemsYearType, value);
        }
        #endregion

        public RelayCommand SaveDataCommand { get; }
        public RelayCommand PrintReportCommand { get; }

        public PheDuyetThanhToanPrintDialogViewModel PheDuyetThanhToanPrintDialogViewModel { get; set; }

        public PheDuyetThanhToanDetailViewModel(
           IVdtTtDeNghiThanhToanKhvService keHoachVonDeNghiThanhToanService,
           IVdtTtDeNghiThanhToanService deNghiThanhToanService,
           ITongHopNguonNSDauTuService tonghopService,
           INsMucLucNganSachService mlService,
           IVdtTtPheDuyetThanhToanService service,
           IVdtTtPheDuyetThanhToanChiTietService pheDuyetThanhToanChiTietService,
           IExportService exportService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           ISessionService sessionService,
           IVdtDuAnHangMucService vdtDuAnHangMucService,
           IMapper mapper,
           PheDuyetThanhToanPrintDialogViewModel pheDuyetThanhToanPrintDialogViewModel,
           ILog logger)
        {
            _pheDuyetThanhToanChiTietService = pheDuyetThanhToanChiTietService;
            _keHoachVonDeNghiThanhToanService = keHoachVonDeNghiThanhToanService;
            _deNghiThanhToanService = deNghiThanhToanService;
            _tonghopService = tonghopService;
            _mlService = mlService;
            _service = service;
            _exportService = exportService;
            _sessionService = sessionService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _vdtDuAnHangMucService = vdtDuAnHangMucService;
            _mapper = mapper;
            _logger = logger;
            PheDuyetThanhToanPrintDialogViewModel = pheDuyetThanhToanPrintDialogViewModel;
            PheDuyetThanhToanPrintDialogViewModel.ParentPage = this;

            SaveDataCommand = new RelayCommand(obj => OnSaveData());
            PrintReportCommand = new RelayCommand(obj => OnShowPrintReportDialog());
        }

        private void OnShowPrintReportDialog()
        {
            List<VdtTtDeNghiThanhToanModel> lstBaoCaoChiTiet = new List<VdtTtDeNghiThanhToanModel>();
            lstBaoCaoChiTiet.Add(Model);
            PheDuyetThanhToanPrintDialogViewModel.VdtTtDeNghiThanhToanModels = lstBaoCaoChiTiet;
            PheDuyetThanhToanPrintDialogViewModel.Init();
            object content = new PheDuyetThanhToanPrintDialog
            {
                DataContext = PheDuyetThanhToanPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            ResetDataPrivate();
            if (!Model.dNgayPheDuyet.HasValue && !BIsDetail)
            {
                BIsChangeNgayPheDuyet = true;
                OnPropertyChanged(nameof(BIsChangeNgayPheDuyet));
            }

            DNgayPheDuyet = Model.dNgayPheDuyet;
            if (Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN)
            {
                Name = "Thông tin phê duyệt thanh toán chi tiết";
            }
            else
            {
                Name = "Thông tin phê duyệt tạm ứng chi tiết";
            }
            ShowLoaiThanhToan = Model.iLoaiThanhToan == 0 ? Visibility.Hidden : Visibility.Visible;
            LoadKeHoachVonThanhToan();
            LoadKeHoachVonTamUng();
            GetDataDropdownPaymentType();
            GetDataDropdownYearType();
            GetMucLucNganSach();
            LoadData();
            OnPropertyChanged(nameof(ShowLoaiThanhToan));
        }

        private List<NguoiDungDonVi> GetListNguoiDungDonVi()
        {
            var predicate = PredicateBuilder.True<NguoiDungDonVi>();
            predicate = predicate.And(x => x.IIDMaNguoiDung.Equals(_sessionService.Current.Principal));
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            List<NguoiDungDonVi> nsDungDonVis = _nsNguoiDungDonViService.FindAll(predicate).ToList();
            return nsDungDonVis;
        }

        private bool CheckReadonly(string idDonVi)
        {
            List<NguoiDungDonVi> listNguoiDungDonVi = GetListNguoiDungDonVi();
            if (listNguoiDungDonVi == null || listNguoiDungDonVi.Count() == 0)
            {
                return true;
            }
            if (!listNguoiDungDonVi.Select(n => n.IIdMaDonVi).ToList().Contains(idDonVi))
            {
                return true;
            }
            return false;
        }

        #region Event
        public override void LoadData(params object[] args)
        {
            Description = "Cấp phát cấp thanh toán trong chỉ tiêu chi tiết";
            var data = _service.GetAllVdtTTPheDuyetThanhToanChiTiet(Model.Id);

            if (data != null && data.Count != 0)
                Model.IsEdit = true;
            else
                Model.IsEdit = false;
            IsReadOnlyTable = false;
            if (Model.BKhoa || CheckReadonly(Model.iID_MaDonViQuanLy))
            {
                IsReadOnlyTable = true;
            }
            Items = _mapper.Map<ObservableCollection<PheDuyetThanhToanChiTietModel>>(data);
            if (Items == null || Items.Count == 0)
            {
                SetDefaultValue();
            }
            else
            {
                foreach (var item in Items)
                {
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
                    {
                        item.FDefaultValueTN = Model.fGiaTriThanhToanTN;
                        item.FDefaultValueNN = Model.fGiaTriThanhToanNN;
                        var itemKhv = _lstKeHoachVonThanhToan.FirstOrDefault(n => n.IIdKeHoachVonId == item.IIdKeHoachVonID);
                        if (itemKhv != null)
                        {
                            item.ILoaiNamKeHoach = itemKhv.ILoaiNamKhv;
                            item.ItemsKeHoachVon = new ObservableCollection<VdtTtKeHoachVonQuery>(_lstKeHoachVonThanhToan);
                            if (item.ItemsKeHoachVon != null)
                            {
                                item.SelectedKeHoachVon = item.ItemsKeHoachVon.FirstOrDefault(n => n.IIdKeHoachVonId == item.IIdKeHoachVonID);
                                if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_TRUOC)
                                    SetDataDropdownMucLucNganSach(item, _lstMucLucAll);
                                else if (_dicMucLucByKHV.ContainsKey(item.IIdKeHoachVonID.Value))
                                    SetDataDropdownMucLucNganSach(item, _dicMucLucByKHV[item.IIdKeHoachVonID.Value]);
                                item.FTongVonDaTamUng = itemKhv.FGiaTriKHV;
                                item.FTongVonDaThuHoi = itemKhv.FGiaTriKHVDaThanhToan;
                            }
                        }
                    }
                    else
                    {
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        {
                            item.FDefaultValueTN = Model.FGiaTriThuHoiUngTruocTn;
                            item.FDefaultValueNN = Model.FGiaTriThuHoiUngTruocNn;
                        }
                        else
                        {
                            item.FDefaultValueTN = Model.fGiaTriThuHoiTN;
                            item.FDefaultValueNN = Model.fGiaTriThuHoiNN;
                        }

                        item.ItemsKeHoachVon = new ObservableCollection<VdtTtKeHoachVonQuery>(_lstKeHoachVonThanhToan.Where(n => n.IIdKeHoachVonId == Model.iID_PhanBoVonID));
                        if (item.ItemsKeHoachVon != null)
                        {
                            item.SelectedKeHoachVon = item.ItemsKeHoachVon.FirstOrDefault(n => n.IIdKeHoachVonId == item.IIdKeHoachVonID);
                            if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_TRUOC)
                                SetDataDropdownMucLucNganSach(item, _lstMucLucAll);
                            else if (_dicMucLucByKHV.ContainsKey(item.IIdKeHoachVonID.Value))
                                SetDataDropdownMucLucNganSach(item, _dicMucLucByKHV[item.IIdKeHoachVonID.Value]);
                        }
                    }
                    item.FTongSo = item.FGiaTriTrongNuoc + item.FGiaTriNgoaiNuoc;
                    item.PropertyChanged += DetailModel_PropertyChanged;
                    SetMlnsDefault(item);
                }
            }
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsReadOnlyTable));
            OnPropertyChanged(nameof(IsEnableButton));
        }

        private void SetDefaultValue()
        {
            if (_lstKeHoachVonThanhToan == null || _lstKeHoachVonThanhToan.Count() == 0) return;
            var itemKhv = _lstKeHoachVonThanhToan.FirstOrDefault();
            PheDuyetThanhToanChiTietModel item = new PheDuyetThanhToanChiTietModel();
            if (Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN)
                item.ILoaiDeNghi = (int)PaymentTypeEnum.Type.THANH_TOAN;
            else
                item.ILoaiDeNghi = (int)PaymentTypeEnum.Type.TAM_UNG;
            item.FDefaultValueTN = Model.fGiaTriThanhToanTN;
            item.FDefaultValueNN = Model.fGiaTriThanhToanNN;
            item.ILoaiNamKeHoach = itemKhv.ILoaiNamKhv;
            item.IIdKeHoachVonID = itemKhv.IIdKeHoachVonId;
            item.ItemsKeHoachVon = new ObservableCollection<VdtTtKeHoachVonQuery>(_lstKeHoachVonThanhToan);
            item.SelectedKeHoachVon = item.ItemsKeHoachVon.FirstOrDefault();
            item.FGiaTriTrongNuoc = Model.fGiaTriThanhToanTN;
            item.FGiaTriNgoaiNuoc = Model.fGiaTriThanhToanNN;
            item.FTongSo = item.FGiaTriTrongNuoc + item.FGiaTriNgoaiNuoc;
            item.PropertyChanged += DetailModel_PropertyChanged;
            Items.Add(item);
            if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_NAY)
            {
                if (_dicMucLucByKHV.ContainsKey(item.IIdKeHoachVonID.Value))
                {
                    SetDataDropdownMucLucNganSach(item, _dicMucLucByKHV[item.IIdKeHoachVonID.Value]);
                    int length = _dicMucLucByKHV[item.IIdKeHoachVonID.Value].Count();
                    item.LNS = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].LNS;
                    item.L = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].L;
                    item.K = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].K;
                    item.M = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].M;
                    item.TM = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].TM;
                    item.TTM = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].TTM;
                    item.NG = _dicMucLucByKHV[item.IIdKeHoachVonID.Value][length - 1].NG;
                    SetMlnsDefault(item);
                }
            }
            else
            {
                SetDataDropdownMucLucNganSach(item, _lstMucLucAll);
            }
        }

        protected override void OnAdd()
        {
            if (IsReadOnlyTable)
            {
                return;
            }
            PheDuyetThanhToanChiTietModel newItem = new PheDuyetThanhToanChiTietModel();
            int currentRow = -1;
            if (SelectedItem != null)
                currentRow = Items.IndexOf(SelectedItem);
            newItem.PropertyChanged += DetailModel_PropertyChanged;
            if (Model.iLoaiThanhToan != (int)PaymentTypeEnum.Type.THANH_TOAN)
            {
                newItem.ILoaiDeNghi = (int)PaymentTypeEnum.Type.TAM_UNG;
                if (_lstKeHoachVonThanhToan != null && _lstKeHoachVonThanhToan.Any())
                    newItem.ILoaiNamKeHoach = _lstKeHoachVonThanhToan.FirstOrDefault().ILoaiNamKhv;
            }
            Items.Insert(currentRow + 1, newItem);
            SelectedItem = newItem;
            SelectedItem.SelectedKeHoachVon = _lstKeHoachVonThanhToan.FirstOrDefault();
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(SelectedItem.SelectedKeHoachVon));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            if (IsReadOnlyTable)
            {
                return;
            }
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            PheDuyetThanhToanChiTietModel item = (PheDuyetThanhToanChiTietModel)sender;
            switch (args.PropertyName)
            {
                case nameof(PheDuyetThanhToanChiTietModel.ILoaiDeNghi):
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
                    {
                        item.FDefaultValueTN = Model.fGiaTriThanhToanTN;
                        item.FDefaultValueNN = Model.fGiaTriThanhToanNN;
                    }
                    else
                    {
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        {
                            item.FDefaultValueTN = Model.FGiaTriThuHoiUngTruocTn;
                            item.FDefaultValueNN = Model.FGiaTriThuHoiUngTruocNn;
                        }
                        else
                        {
                            item.FDefaultValueTN = Model.fGiaTriThuHoiTN;
                            item.FDefaultValueNN = Model.fGiaTriThuHoiNN;
                        }
                    }
                    SetKeHoachVon(item);
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.ILoaiNamKeHoach):
                    SetKeHoachVon(item);
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedKeHoachVon):
                    if (item.SelectedKeHoachVon != null)
                    {
                        if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_NAY)
                        {
                            if (_dicMucLucByKHV.ContainsKey(item.SelectedKeHoachVon.IIdKeHoachVonId))
                            {
                                SetDataDropdownMucLucNganSach(item, _dicMucLucByKHV[item.SelectedKeHoachVon.IIdKeHoachVonId]);
                            }
                            item.IIdKeHoachVonID = item.SelectedKeHoachVon.IIdKeHoachVonId;
                        }
                        else
                        {
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll);

                            // set default
                            item.SelectedLNS = item.ItemsLNS.Where(i => i.ValueItem.Equals("1030100")).FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedLNS));
                            item.SelectedL = item.ItemsL.FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedL));
                            item.SelectedK = item.ItemsK.FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedK));
                            item.SelectedM = item.ItemsM.Where(i => i.ValueItem.Equals("9300")).FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedM));
                            item.SelectedTM = item.ItemsTM.FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedTM));
                            item.SelectedTTM = item.ItemsTTM.FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedTTM));
                            item.SelectedNG = item.ItemsNG.FirstOrDefault();
                            SetDataDropdownMucLucNganSach(item, _lstMucLucAll, nameof(item.SelectedNG));

                        }
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
                        {
                            item.FTongVonDaTamUng = item.SelectedKeHoachVon.FGiaTriKHV;
                            item.FTongVonDaThuHoi = item.SelectedKeHoachVon.FGiaTriKHVDaThanhToan;
                        }
                    }
                    else
                    {
                        SetDataDropdownMucLucNganSach(item, new List<MlnsByKeHoachVonQuery>());
                        item.IIdKeHoachVonID = null;
                        item.FTongVonDaTamUng = 0;
                        item.FTongVonDaThuHoi = 0;
                    }
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedLNS):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedL):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedK):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedM):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedTM):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedTTM):
                case nameof(PheDuyetThanhToanChiTietModel.SelectedNG):
                    if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_NAY)
                    {
                        if (item.SelectedKeHoachVon != null)
                            SetDataDropdownMucLucNganSach(item, _dicMucLucByKHV[item.SelectedKeHoachVon.IIdKeHoachVonId], args.PropertyName);
                    }
                    else
                        SetDataDropdownMucLucNganSach(item, _lstMucLucAll, args.PropertyName);
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.FGiaTriTrongNuoc):
                case nameof(PheDuyetThanhToanChiTietModel.FGiaTriNgoaiNuoc):
                    item.FTongSo = item.FGiaTriNgoaiNuoc + item.FGiaTriTrongNuoc;
                    break;
            }
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            if (!Validate()) return;
            var objPheDuyet = _deNghiThanhToanService.Find(Model.Id);
            if (objPheDuyet != null)
            {
                objPheDuyet.SGhiChuPheDuyet = Model.SGhiChuPheDuyet;
                objPheDuyet.DNgayPheDuyet = DNgayPheDuyet;
                objPheDuyet.FChuyenTienBaoHanhDuocDuyet = Model.FChuyenTienBaoHanhDuocDuyet;
                objPheDuyet.FThueGiaTriGiaTangDuocDuyet = Model.FThueGiaTriGiaTangDuocDuyet;
                objPheDuyet.SLyDoTuChoi = Model.SLyDoTuChoi;
                objPheDuyet.DDateUpdate = DateTime.Now;
                objPheDuyet.SUserUpdate = _sessionService.Current.Principal;
                _deNghiThanhToanService.Update(objPheDuyet);
            }
            List<VdtTtPheDuyetThanhToanChiTiet> lstData = new List<VdtTtPheDuyetThanhToanChiTiet>();
            foreach (var item in Items.Where(n => !n.IsDeleted && (n.FGiaTriNgoaiNuoc != 0 || n.FGiaTriTrongNuoc != 0)))
            {
                lstData.Add(ConvertData(item));
            }
            _service.InsertDetailData(Model.Id, lstData);
            ProcessXuLyDuLieu();
            MessageBox.Show(Resources.MsgSaveDone);
            LoadData();
        }

        private void OnPrintReport()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNamePrefix;
                    string fileNameWithoutExtension;

                    List<VdtTtPheDuyetThanhToanChiTiet> listData = _pheDuyetThanhToanChiTietService.FindByDeNghiThanhToanId(Model.Id);
                    List<VdtTtPheDuyetThanhToanChiTiet> listDataThuHoi = listData.Where(n => (string.IsNullOrEmpty(n.M) && string.IsNullOrEmpty(n.Tm)
                                                                                && string.IsNullOrEmpty(n.Ttm) && string.IsNullOrEmpty(n.Ng))).ToList();
                    double thuHoiNamTrcTN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                        listDataThuHoi.Select(n => n.FGiaTriThuHoiNamTruocTn.HasValue ? n.FGiaTriThuHoiNamTruocTn.Value : 0).Sum() : 0;
                    double thuHoiNamTrcNN = (listDataThuHoi != null && listDataThuHoi.Count > 0) ?
                        listDataThuHoi.Select(n => n.FGiaTriThuHoiNamTruocNn.HasValue ? n.FGiaTriThuHoiNamTruocNn.Value : 0).Sum() : 0;
                    double thuHoiNamNayTN = (listData != null && listData.Count > 0) ?
                        listData.Select(n => n.FGiaTriThuHoiNamNayTn.HasValue ? n.FGiaTriThuHoiNamNayTn.Value : 0).Sum() : 0;
                    double thuHoiNamNayNN = (listData != null && listData.Count > 0) ?
                        listData.Select(n => n.FGiaTriThuHoiNamNayNn.HasValue ? n.FGiaTriThuHoiNamNayNn.Value : 0).Sum() : 0;

                    var objGiaTriPheDuyet = _deNghiThanhToanService.LoadGiaTriPheDuyetThanhToanByParentId(Model.Id);
                    var fGiaTriTuChoiTN = Model.fGiaTriThanhToanTN - ((Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanTN : objGiaTriPheDuyet.TamUngTN);
                    var fGiaTriTuChoiNN = Model.fGiaTriThanhToanNN - ((Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN) ? objGiaTriPheDuyet.ThanhToanNN : objGiaTriPheDuyet.TamUngNN);

                    var fTraDonViThuHuongTN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanTn ?? 0)
                            - thuHoiNamTrcTN - thuHoiNamNayTN;
                    var fTraDonViThuHuongNN = listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).Sum(n => n.FGiaTriThanhToanNn ?? 0)
                        - thuHoiNamTrcNN - thuHoiNamNayNN;
                    var fTongTraDonViThuHuong = fTraDonViThuHuongTN + fTraDonViThuHuongNN;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, Utility.Enum.ExportType.EXCEL);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("Items", listData.Where(n => (!string.IsNullOrEmpty(n.M) || !string.IsNullOrEmpty(n.Tm) || !string.IsNullOrEmpty(n.Ttm) || !string.IsNullOrEmpty(n.Ng))).ToList());
                    data.Add("ThuHoiNamTrcTN", thuHoiNamTrcTN);
                    data.Add("ThuHoiNamTrcNN", thuHoiNamTrcNN);
                    data.Add("ThuHoiNamNayTN", thuHoiNamNayTN);
                    data.Add("ThuHoiNamNayNN", thuHoiNamNayNN);
                    data.Add("ThuHoiNamTrcTong", (thuHoiNamTrcTN + thuHoiNamTrcNN));
                    data.Add("ThuHoiNamNayTong", (thuHoiNamNayTN + thuHoiNamNayNN));
                    data.Add("GiaTriTuChoiTN", fGiaTriTuChoiTN);
                    data.Add("GiaTriTuChoiNN", fGiaTriTuChoiNN);
                    data.Add("TongGiaTriTuChoi", (fGiaTriTuChoiTN + fGiaTriTuChoiNN));
                    data.Add("TraDonViThuHuongTN", fTraDonViThuHuongTN);
                    data.Add("TraDonViThuHuongNN", fTraDonViThuHuongNN);
                    data.Add("TongTraDonViThuHuong", fTongTraDonViThuHuong);
                    data.Add("TongTraDonViThuHuongText", StringUtils.NumberToText(fTongTraDonViThuHuong));
                    data.Add("LyDoTuChoi", Model.SLyDoTuChoi);
                    data.Add("GhiChuPheDuyet", Model.SGhiChuPheDuyet);
                    data.Add("Ngay", Model.dNgayPheDuyet.HasValue ? Model.dNgayPheDuyet.Value.ToString("dd/MM/yyyy") : string.Empty);

                    if (Model.iLoaiThanhToan == 1)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDTQUANLYCAPPHATTHANHTOANTHANHTOAN);
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_CPTT, ExportFileName.RPT_VDT_QUANLYCAPPHATTHANHTOAN);
                    }
                    fileNamePrefix = string.Format("rptQuanLyCapPhatThanhToan_{0}", Model.sSoDeNghi);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtTtPheDuyetThanhToanChiTiet>(templateFileName, data);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.PDF);
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        private bool Validate()
        {
            List<string> lstError = new List<string>();
            if (!DNgayPheDuyet.HasValue)
            {
                lstError.Add(string.Format(Resources.MsgErrorRequire, "Ngày phê duyệt"));
            }
            var lstItem = Items.Where(n => !n.IsDeleted && (n.FGiaTriNgoaiNuoc != 0 || n.FGiaTriTrongNuoc != 0) && n.SelectedKeHoachVon != null);
            foreach (var item in lstItem)
            {
                if (item.ILoaiDeNghi != (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC)
                {
                    if (item.SelectedLNS == null && item.SelectedL == null && item.SelectedK == null && item.SelectedM == null
                        && item.SelectedTM == null && item.SelectedTTM == null && item.SelectedNG == null)
                    {
                        lstError.Add(string.Format(Resources.MsgErrorDataEmpty, "mục lục ngân sách"));
                        break;
                    }
                    else if (!item.IIdMuc.HasValue && !item.IIdTieuMuc.HasValue && !item.IIdTietMuc.HasValue && !item.IIdNganh.HasValue)
                    {
                        lstError.Add(Resources.MsgErrorMucLucNganSachNotExist);
                        break;
                    }
                }
            }
            if (lstError.Count != 0)
            {
                MessageBox.Show(string.Join("\n", lstError));
                return false;
            }
            OnPropertyChanged(nameof(Items));
            return true;
        }

        private VdtTtPheDuyetThanhToanChiTiet ConvertData(PheDuyetThanhToanChiTietModel item)
        {
            VdtTtPheDuyetThanhToanChiTiet result = new VdtTtPheDuyetThanhToanChiTiet();
            result.Id = Guid.NewGuid();
            result.IIDDeNghiThanhToanID = Model.Id;
            result.IIdMucId = item.IIdMuc;
            result.IIdTieuMucId = item.IIdTieuMuc;
            result.IIdTietMucId = item.IIdTietMuc;
            result.IIdNganhId = item.IIdNganh;
            result.M = item.M; result.Tm = item.TM; result.Ttm = item.TTM; result.Ng = item.NG;
            result.SGhiChu = item.SGhiChu;

            if (item.ILoaiNamKeHoach == (int)NamKeHoachEnum.Type.NAM_TRUOC && item.SelectedKeHoachVon.ILoaiKeHoachVon <= 2)
            {
                result.ILoaiKeHoachVon = item.SelectedKeHoachVon.ILoaiKeHoachVon + 2;
            }
            else
            {
                result.ILoaiKeHoachVon = item.SelectedKeHoachVon.ILoaiKeHoachVon;
            }

            result.IIDKeHoachVonID = item.SelectedKeHoachVon.IIdKeHoachVonId;
            if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
            {
                result.FGiaTriThanhToanNn = item.FGiaTriNgoaiNuoc;
                result.FGiaTriThanhToanTn = item.FGiaTriTrongNuoc;
            }
            else
            {
                if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY)
                {
                    result.FGiaTriThuHoiUngTruocNamNayNn = item.FGiaTriNgoaiNuoc;
                    result.FGiaTriThuHoiUngTruocNamNayTn = item.FGiaTriTrongNuoc;
                }
                if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                {
                    result.FGiaTriThuHoiUngTruocNamTruocNn = item.FGiaTriNgoaiNuoc;
                    result.FGiaTriThuHoiUngTruocNamTruocTn = item.FGiaTriTrongNuoc;
                }
                else if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY)
                {
                    result.FGiaTriThuHoiNamNayNn = item.FGiaTriNgoaiNuoc;
                    result.FGiaTriThuHoiNamNayTn = item.FGiaTriTrongNuoc;
                }
                else if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC)
                {
                    result.FGiaTriThuHoiNamTruocNn = item.FGiaTriNgoaiNuoc;
                    result.FGiaTriThuHoiNamTruocTn = item.FGiaTriTrongNuoc;
                }
            }
            return result;
        }

        private List<TongHopNguonNSDauTuQuery> ConvertDataTongHop(PheDuyetThanhToanChiTietModel item, ref TongHopNguonNSDauTuQuery data)
        {
            List<TongHopNguonNSDauTuQuery> results = new List<TongHopNguonNSDauTuQuery>();
            data.iID_ChungTu = Model.Id;
            data.fGiaTri = item.FGiaTriTrongNuoc + item.FGiaTriNgoaiNuoc;
            data.bIsLog = false;
            data.iID_DuAnID = Model.iID_DuAnId ?? Guid.Empty;
            data.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
            data.IIDMucID = item.IIdMuc;
            data.IIDTieuMucID = item.IIdTieuMuc;
            data.IIDTietMucID = item.IIdTietMuc;
            data.IIDNganhID = item.IIdNganh;
            data.IIdLoaiCongTrinh = item.SelectedKeHoachVon?.ID_DuAn_HangMuc != null ? _vdtDuAnHangMucService.FindById(item.SelectedKeHoachVon.ID_DuAn_HangMuc).IdLoaiCongTrinh : Guid.Empty;
            if (item.SelectedKeHoachVon != null)
            {
                data.iId_MaNguonCha = item.SelectedKeHoachVon.IIdKeHoachVonId;
                data.sMaNguonCha = GetMaNguonChaByKeHoachVon(item.SelectedKeHoachVon);

            }
            switch (Model.iCoQuanThanhToan)
            {
                case (int)CoQuanThanhToanEnum.Type.KHO_BAC:
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN)
                        data.sMaDich = LOAI_CHUNG_TU.TT_THANHTOAN_KHOBAC;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
                        data.sMaDich = LOAI_CHUNG_TU.TT_UNG_KHOBAC;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                    {
                        data.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                        data.sMaNguon = LOAI_CHUNG_TU.TT_UNG_KHOBAC;
                        data.iId_MaNguonCha = item.SelectedKeHoachVon.IIdKeHoachVonId;
                        data.sMaNguonCha = GetMaNguonChaByKeHoachVon(item.SelectedKeHoachVon);
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        {
                            data.iThuHoiTUCheDo = 1;
                        }
                        else
                        {
                            data.iThuHoiTUCheDo = 2;
                        }
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY)
                            data.ILoaiUng = 2;
                        else

                            data.ILoaiUng = 1;
                    }
                    break;

                case (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI:
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN)
                        data.sMaDich = LOAI_CHUNG_TU.TT_THANHTOAN_TONKHOANDONVI;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG)
                        data.sMaDich = LOAI_CHUNG_TU.TT_UNG_TONKHOANDONVI;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                    {
                        data.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                        data.sMaNguon = LOAI_CHUNG_TU.TT_UNG_TONKHOANDONVI;
                        data.iId_MaNguonCha = item.SelectedKeHoachVon.IIdKeHoachVonId;
                        data.sMaNguonCha = GetMaNguonChaByKeHoachVon(item.SelectedKeHoachVon);
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        {
                            data.iThuHoiTUCheDo = 1;
                        }
                        else
                        {
                            data.iThuHoiTUCheDo = 2;
                        }
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY)
                            data.ILoaiUng = 2;
                        else

                            data.ILoaiUng = 1;
                    }
                    break;

                case (int)CoQuanThanhToanEnum.Type.CQTC:
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THANH_TOAN) data.sMaDich = LOAI_CHUNG_TU.TT_THANHTOAN_LENHCHI;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.TAM_UNG) data.sMaDich = LOAI_CHUNG_TU.TT_UNG_LENHCHI;
                    if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY
                        || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                    {
                        data.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                        data.sMaNguon = LOAI_CHUNG_TU.TT_UNG_LENHCHI;
                        data.iId_MaNguonCha = item.SelectedKeHoachVon.IIdKeHoachVonId;
                        data.sMaNguonCha = GetMaNguonChaByKeHoachVon(item.SelectedKeHoachVon);
                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        {
                            data.iThuHoiTUCheDo = 1;
                        }
                        else
                        {
                            data.iThuHoiTUCheDo = 2;
                        }

                        if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC
                            || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY)
                            data.ILoaiUng = 2;
                        else

                            data.ILoaiUng = 1;
                    }
                    break;
            }

            if (Model.BHoanTraUngTruoc)
            {
                TongHopNguonNSDauTuQuery childThuHoiUng = new TongHopNguonNSDauTuQuery();
                TongHopNguonNSDauTuQuery childThuHoiKhoBac = new TongHopNguonNSDauTuQuery();
                childThuHoiUng.iID_ChungTu = Model.Id;
                childThuHoiUng.fGiaTri = item.FGiaTriTrongNuoc + item.FGiaTriNgoaiNuoc;
                childThuHoiUng.bIsLog = false;
                childThuHoiUng.iID_DuAnID = Model.iID_DuAnId ?? Guid.Empty;
                childThuHoiUng.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                childThuHoiUng.IIDMucID = item.IIdMuc;
                childThuHoiUng.IIDTieuMucID = item.IIdTieuMuc;
                childThuHoiUng.IIDTietMucID = item.IIdTietMuc;
                childThuHoiUng.IIDNganhID = item.IIdNganh;

                childThuHoiKhoBac.iID_ChungTu = Model.Id;
                childThuHoiKhoBac.fGiaTri = item.FGiaTriTrongNuoc + item.FGiaTriNgoaiNuoc;
                childThuHoiKhoBac.bIsLog = false;
                childThuHoiKhoBac.iID_DuAnID = Model.iID_DuAnId ?? Guid.Empty;
                childThuHoiKhoBac.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                childThuHoiKhoBac.IIDMucID = item.IIdMuc;
                childThuHoiKhoBac.IIDTieuMucID = item.IIdTieuMuc;
                childThuHoiKhoBac.IIDTietMucID = item.IIdTietMuc;
                childThuHoiKhoBac.IIDNganhID = item.IIdNganh;
                if (Model.iCoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC)
                {
                    childThuHoiUng.sMaNguon = LOAI_CHUNG_TU.TT_UNG_KHOBAC;
                    childThuHoiUng.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiUng.sMaNguonCha = LOAI_CHUNG_TU.KHVN_KHOBAC;
                    childThuHoiUng.ILoaiUng = 2;
                    childThuHoiUng.iThuHoiTUCheDo = 1;


                    childThuHoiKhoBac.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiKhoBac.sMaDich = LOAI_CHUNG_TU.KHVU_KHOBAC;
                    childThuHoiKhoBac.sMaNguonCha = LOAI_CHUNG_TU.KHVN_KHOBAC;
                }
                else if (Model.iCoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI)
                {
                    childThuHoiUng.sMaNguon = LOAI_CHUNG_TU.TT_UNG_TONKHOANDONVI;
                    childThuHoiUng.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiUng.sMaNguonCha = LOAI_CHUNG_TU.KHVN_TONKHOANTAIDONVI;
                    childThuHoiUng.ILoaiUng = 2;
                    childThuHoiUng.iThuHoiTUCheDo = 1;


                    childThuHoiKhoBac.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiKhoBac.sMaDich = LOAI_CHUNG_TU.KHVU_TONKHOANTAIDONVI;
                    childThuHoiKhoBac.sMaNguonCha = LOAI_CHUNG_TU.KHVN_TONKHOANTAIDONVI;
                }
                else
                {
                    childThuHoiUng.sMaNguon = LOAI_CHUNG_TU.TT_UNG_LENHCHI;
                    childThuHoiUng.sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiUng.sMaNguonCha = LOAI_CHUNG_TU.KHVN_LENHCHI;
                    childThuHoiUng.ILoaiUng = 2;
                    childThuHoiUng.iThuHoiTUCheDo = 1;

                    childThuHoiKhoBac.sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU;
                    childThuHoiKhoBac.sMaDich = LOAI_CHUNG_TU.KHVU_LENHCHI;
                    childThuHoiKhoBac.sMaNguonCha = LOAI_CHUNG_TU.KHVN_LENHCHI;
                }
                results.Add(childThuHoiKhoBac);
                results.Add(childThuHoiUng);
            }
            return results;
        }

        private string GetMaNguonChaByKeHoachVon(VdtTtKeHoachVonQuery item)
        {
            if (item.ILoaiKeHoachVon == (int)LoaiKeHoachVonEnum.Type.KE_HOACH_VON_NAM_NAY)
            {
                if (item.ILoaiNamKhv == (int)NamKeHoachEnum.Type.NAM_TRUOC)
                {
                    return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.QT_KHOBAC_CHUYENNAMTRUOC :
                           item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.QT_TONKHOANDONVI_CHUYENNAMTRUOC :
                           LOAI_CHUNG_TU.QT_LENHCHI_CHUYENNAMTRUOC;
                }
                else
                {
                    return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.KHVN_KHOBAC :
                           item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.KHVN_TONKHOANTAIDONVI :
                           LOAI_CHUNG_TU.KHVN_LENHCHI;
                }
            }
            if (item.ILoaiKeHoachVon == (int)LoaiKeHoachVonEnum.Type.KE_HOACH_VON_UNG_NAM_NAY)
            {
                if (item.ILoaiNamKhv == (int)NamKeHoachEnum.Type.NAM_TRUOC)
                {
                    return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.QT_UNG_KHOBAC_CHUYENNAMTRUOC :
                           item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.QT_UNG_TONKHOANDONVI_CHUYENNAMTRUOC :
                           LOAI_CHUNG_TU.QT_UNG_LENHCHI_CHUYENNAMTRUOC;
                }
                else
                {
                    return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.KHVU_KHOBAC :
                           item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.KHVN_TONKHOANTAIDONVI :
                           LOAI_CHUNG_TU.KHVU_LENHCHI;
                }
            }
            if (item.ILoaiKeHoachVon == (int)LoaiKeHoachVonEnum.Type.KE_HOACH_VON_NAM_TRUOC_CHUYEN_SANG)
            {
                return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.QT_KHOBAC_CHUYENNAMTRUOC :
                       item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.QT_TONKHOANDONVI_CHUYENNAMTRUOC :
                       LOAI_CHUNG_TU.QT_LENHCHI_CHUYENNAMTRUOC;
            }
            if (item.ILoaiKeHoachVon == (int)LoaiKeHoachVonEnum.Type.KE_HOACH_VON_UNG_NAM_TRUOC_CHUYEN_SANG)
            {
                return item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.KHO_BAC ? LOAI_CHUNG_TU.QT_UNG_KHOBAC_CHUYENNAMTRUOC :
                       item.ICoQuanThanhToan == (int)CoQuanThanhToanEnum.Type.TONKHOAN_DONVI ? LOAI_CHUNG_TU.QT_UNG_TONKHOANDONVI_CHUYENNAMTRUOC : 
                    LOAI_CHUNG_TU.QT_UNG_LENHCHI_CHUYENNAMTRUOC;
            }
            return string.Empty;
        }

        private void SetKeHoachVon(PheDuyetThanhToanChiTietModel item)
        {
            item.ItemsKeHoachVon = new ObservableCollection<VdtTtKeHoachVonQuery>(_lstKeHoachVonThanhToan.Where(n => n.IIdKeHoachVonId == Model.iID_PhanBoVonID));
            item.SelectedKeHoachVon = item.ItemsKeHoachVon.FirstOrDefault();
        }

        private void LoadKeHoachVonTamUng()
        {
            var lstKhv = _keHoachVonDeNghiThanhToanService.GetKhvDeNghiTamUng(
                Model.iID_DuAnId.Value,
                Model.iID_NguonVonID,
                Model.dNgayDeNghi.Value,
                Model.iNamKeHoach,
                Model.iCoQuanThanhToan ?? 0,
                Model.Id,
                Model.ID_DuAn_HangMuc);
            if (lstKhv == null) return;
            _lstKeHoachVonThuHoi = lstKhv;
            _fTamUngNamTruocNN = lstKhv.Where(n => n.ILoaiNamTamUng == (int)NamKeHoachEnum.Type.NAM_TRUOC).Sum(n => n.FGiaTriThanhToanNN - n.FGiaTriThuHoiNgoaiNuoc);
            _fTamUngNamTruocTN = lstKhv.Where(n => n.ILoaiNamTamUng == (int)NamKeHoachEnum.Type.NAM_TRUOC).Sum(n => n.FGiaTriThanhToanTN - n.FGiaTriThuHoiTrongNuoc);
            GetMucLucNganSachInKeHoachVon(lstKhv.Where(n => !_lstTypeKhvNamTruoc.Contains(n.ILoaiKeHoachVon)).ToList());
        }

        private void LoadKeHoachVonThanhToan()
        {
            var lstKhv = _keHoachVonDeNghiThanhToanService.GetKhvDeNghiThanhToan(
                Model.iID_DuAnId.Value,
                Model.iID_NguonVonID,
                Model.dNgayDeNghi.Value,
                Model.iNamKeHoach,
                Model.iCoQuanThanhToan ?? 0,
                Model.Id,
                Model.ID_DuAn_HangMuc);
            if (lstKhv == null) return;
            _lstKeHoachVonThanhToan = lstKhv;
            GetMucLucNganSachInKeHoachVon(lstKhv.Where(n => !_lstTypeKhvNamTruoc.Contains(n.ILoaiKeHoachVon)).ToList());
        }

        private void GetMucLucNganSachInKeHoachVon(List<VdtTtKeHoachVonQuery> lstKeHoachVon)
        {
            if (lstKeHoachVon == null || lstKeHoachVon.Count == 0) return;
            List<TongHopNguonNSDauTuQuery> lstChungTu = lstKeHoachVon.Select(n => new TongHopNguonNSDauTuQuery()
            {
                iID_ChungTu = n.IIdKeHoachVonId,
                iID_DuAnID = Model.iID_DuAnId.Value,
                sMaNguon = n.ILoaiKeHoachVon == 1 ? LOAI_CHUNG_TU.KE_HOACH_VON_NAM : LOAI_CHUNG_TU.KE_HOACH_VON_UNG,
                IIdLoaiCongTrinh = n.ID_DuAn_HangMuc
            }).ToList();
            var lstData = _keHoachVonDeNghiThanhToanService.GetMucLucNganSachByKeHoachVon(_sessionService.Current.YearOfWork, lstChungTu);
            if (lstData == null) return;
            foreach (var item in lstData)
            {
                if (!_dicMucLucByKHV.ContainsKey(item.IidKeHoachVonId))
                    _dicMucLucByKHV.Add(item.IidKeHoachVonId, new List<MlnsByKeHoachVonQuery>());
                _dicMucLucByKHV[item.IidKeHoachVonId].Add(item);
            }
        }

        private void GetIdMucLucNganSach(PheDuyetThanhToanChiTietModel item)
        {
            item.IIdMuc = item.IIdTieuMuc = item.IIdTietMuc = item.IIdNganh = null;
            string sKey = item.LNS + "\t" + item.L + "\t" + item.K + "\t" + item.M + "\t" + item.TM + "\t" + item.TTM + "\t" + item.NG;
            if (!_dicMucLuc.ContainsKey(sKey)) return;
            if (!string.IsNullOrEmpty(item.NG))
            {
                item.IIdNganh = _dicMucLuc[sKey].Id;
                item.STenMucLuc = _dicMucLuc[sKey].MoTa;
                return;
            }
            if (!string.IsNullOrEmpty(item.TTM))
            {
                item.IIdTietMuc = _dicMucLuc[sKey].Id;
                item.STenMucLuc = _dicMucLuc[sKey].MoTa;
                return;
            }
            if (!string.IsNullOrEmpty(item.TM))
            {
                item.IIdTieuMuc = _dicMucLuc[sKey].Id;
                item.STenMucLuc = _dicMucLuc[sKey].MoTa;
                return;
            }
            if (!string.IsNullOrEmpty(item.M))
            {
                item.IIdMuc = _dicMucLuc[sKey].Id;
                item.STenMucLuc = _dicMucLuc[sKey].MoTa;
                return;
            }
        }

        private void GetDataDropdownPaymentType()
        {
            List<ComboboxItem> lstData = new List<ComboboxItem>();
            if (Model.iLoaiThanhToan == (int)PaymentTypeEnum.Type.THANH_TOAN)
            {
                lstData.Add(new ComboboxItem()
                {
                    ValueItem = ((int)PaymentTypeEnum.Type.THANH_TOAN).ToString(),
                    DisplayItem = PaymentTypeEnum.TypeName.THANH_TOAN_KLHT
                });
                if (Model.fGiaTriThuHoiNN != 0 || Model.fGiaTriThuHoiTN != 0)
                {
                    lstData.Add(new ComboboxItem()
                    {
                        ValueItem = ((int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC).ToString(),
                        DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC)
                    });
                    lstData.Add(new ComboboxItem()
                    {
                        ValueItem = ((int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY).ToString(),
                        DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY)
                    });
                }
                if (Model.FGiaTriThuHoiUngTruocNn != 0 || Model.FGiaTriThuHoiUngTruocTn != 0)
                {
                    if (_lstKeHoachVonThanhToan != null && _lstKeHoachVonThanhToan.Any()
                        && (_lstKeHoachVonThanhToan.FirstOrDefault().ILoaiKeHoachVon == (int)LOAI_KHV.Type.KE_HOACH_VON_NAM
                        || _lstKeHoachVonThanhToan.FirstOrDefault().ILoaiKeHoachVon == (int)LOAI_KHV.Type.KE_HOACH_VON_NAM_CHUYEN_SANG))
                    {
                        lstData.Add(new ComboboxItem()
                        {
                            ValueItem = ((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC).ToString(),
                            DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        });
                    }
                    else
                    {
                        lstData.Add(new ComboboxItem()
                        {
                            ValueItem = ((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY).ToString(),
                            DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY)
                        });
                    }
                }
            }
            else
            {
                if (Model.FGiaTriThuHoiUngTruocNn != 0 || Model.FGiaTriThuHoiUngTruocTn != 0)
                {
                    if (_lstKeHoachVonThanhToan != null
                        && (_lstKeHoachVonThanhToan.FirstOrDefault().ILoaiKeHoachVon == (int)LOAI_KHV.Type.KE_HOACH_VON_NAM
                        || _lstKeHoachVonThanhToan.FirstOrDefault().ILoaiKeHoachVon == (int)LOAI_KHV.Type.KE_HOACH_VON_NAM_CHUYEN_SANG))
                    {
                        lstData.Add(new ComboboxItem()
                        {
                            ValueItem = ((int)PaymentTypeEnum.Type.TAM_UNG).ToString(),
                            DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.TAM_UNG)
                        });

                        lstData.Add(new ComboboxItem()
                        {
                            ValueItem = ((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC).ToString(),
                            DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                        });
                        ShowLoaiThanhToan = Visibility.Visible;
                    }
                }
                else
                {
                    lstData.Add(new ComboboxItem()
                    {
                        ValueItem = ((int)PaymentTypeEnum.Type.TAM_UNG).ToString(),
                        DisplayItem = PaymentTypeEnum.Get((int)PaymentTypeEnum.Type.TAM_UNG)
                    });
                }
            }
            ItemsPaymentType = new ObservableCollection<ComboboxItem>(lstData);
            OnPropertyChanged(nameof(ShowLoaiThanhToan));
            OnPropertyChanged(nameof(ItemsPaymentType));
        }

        private void GetDataDropdownYearType()
        {
            ObservableCollection<ComboboxItem> lstData = new ObservableCollection<ComboboxItem>();
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = NamKeHoachEnum.TypeName.NAM_TRUOC,
                ValueItem = ((int)NamKeHoachEnum.Type.NAM_TRUOC).ToString()
            });
            lstData.Add(new ComboboxItem()
            {
                DisplayItem = NamKeHoachEnum.TypeName.NAM_NAY,
                ValueItem = ((int)NamKeHoachEnum.Type.NAM_NAY).ToString()
            });
            ItemsYearType = lstData;
            OnPropertyChanged(nameof(ItemsYearType));
        }

        private void GetMucLucNganSach()
        {
            _dicMucLuc = new Dictionary<string, NsMucLucNganSach>();
            var data = _mlService.FindAll(Model.iNamKeHoach);
            if (data != null)
            {
                foreach (var item in data)
                {
                    string key = item.Lns + "\t" + item.L + "\t" + item.K + "\t" + item.M + "\t" + item.Tm + "\t" + item.Ttm + "\t" + item.Ng;
                    if (!_dicMucLuc.ContainsKey(key))
                    {
                        _dicMucLuc.Add(key, item);
                    }
                }
                _lstMucLucAll = data.Select(n => new MlnsByKeHoachVonQuery()
                {
                    LNS = n.Lns,
                    L = n.L,
                    K = n.K,
                    M = n.M,
                    TM = n.Tm,
                    TTM = n.Ttm,
                    NG = n.Ng
                }).ToList();
            }
        }

        private void ResetDataPrivate()
        {
            _fTamUngNamTruocTN = 0;
            _fTamUngNamTruocNN = 0;
            _dicMucLuc = new Dictionary<string, NsMucLucNganSach>();
            _dicMucLucByKHV = new Dictionary<Guid, List<MlnsByKeHoachVonQuery>>();
            _lstKeHoachVonThanhToan = new List<VdtTtKeHoachVonQuery>();
            _lstKeHoachVonThuHoi = new List<VdtTtKeHoachVonQuery>();
            _lstMucLucAll = new List<MlnsByKeHoachVonQuery>();
        }

        private void ProcessXuLyDuLieu()
        {
            List<TongHopNguonNSDauTuQuery> lstDataInsert = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstChungTuNguon = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstChungTuAppend = new List<TongHopNguonNSDauTuQuery>();

            foreach (var item in
                Items.Where(n => !n.IsDeleted && (n.FGiaTriNgoaiNuoc != 0 || n.FGiaTriTrongNuoc != 0)
                && n.SelectedKeHoachVon != null))
            {
                TongHopNguonNSDauTuQuery data = new TongHopNguonNSDauTuQuery();
                var lstAppend = ConvertDataTongHop(item, ref data);
                lstDataInsert.Add(data);
                if (lstAppend != null && lstAppend.Count != 0)
                    lstChungTuAppend.AddRange(lstAppend);

                if (item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_NAY
                    || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC
                    || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_NAY
                    || item.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_UNG_TRUOC_NAM_TRUOC)
                {
                    lstChungTuNguon.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = item.SelectedKeHoachVon.IIdKeHoachVonId,
                        sMaDich = data.sMaNguon,
                        sMaNguon = LOAI_CHUNG_TU.CHU_DAU_TU,
                        IIdLoaiCongTrinh = item.SelectedKeHoachVon?.ID_DuAn_HangMuc != null ? _vdtDuAnHangMucService.FindById(item.SelectedKeHoachVon.ID_DuAn_HangMuc).IdLoaiCongTrinh : Guid.Empty,
                    });
                }
                else
                {
                    lstChungTuNguon.Add(new TongHopNguonNSDauTuQuery()
                    {
                        iID_ChungTu = item.SelectedKeHoachVon.IIdKeHoachVonId,
                        sMaNguon = GetMaNguonChaByKeHoachVon(item.SelectedKeHoachVon),
                        sMaDich = LOAI_CHUNG_TU.CHU_DAU_TU,
                        IIdLoaiCongTrinh = item.SelectedKeHoachVon?.ID_DuAn_HangMuc != null ? _vdtDuAnHangMucService.FindById(item.SelectedKeHoachVon.ID_DuAn_HangMuc).IdLoaiCongTrinh : Guid.Empty,
                    });
                }
            }

            if (lstDataInsert.Count != 0)
            {
                _tonghopService.InsertTongHopNguonDauTu_ThanhToan_Giam(LOAI_CHUNG_TU.CAP_THANH_TOAN, (int)TypeExecute.Update, Model.Id, lstDataInsert, lstChungTuNguon, lstChungTuAppend, Model.FThueGiaTriGiaTangDuocDuyet, Model.FChuyenTienBaoHanhDuocDuyet);
            }
        }

        private void SetDataDropdownMucLucNganSach(PheDuyetThanhToanChiTietModel item, List<MlnsByKeHoachVonQuery> lstMucLuc, string sLoaiMucLucSelected = "")
        {
            switch (sLoaiMucLucSelected)
            {
                case "":
                    item.ItemsLNS = new ObservableCollection<ComboboxItem>(lstMucLuc
                        .Where(n => !string.IsNullOrEmpty(n.LNS)).GroupBy(n => n.LNS)
                        .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    item.ItemsL = new ObservableCollection<ComboboxItem>();
                    item.ItemsK = new ObservableCollection<ComboboxItem>();
                    item.ItemsM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();

                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedLNS):
                    if (item.SelectedLNS != null)
                    {
                        item.LNS = item.SelectedLNS.ValueItem;
                        item.ItemsL = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && !string.IsNullOrEmpty(n.L)).GroupBy(n => n.L)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.LNS = string.Empty;
                        item.ItemsL = new ObservableCollection<ComboboxItem>();
                    }
                    item.ItemsK = new ObservableCollection<ComboboxItem>();
                    item.ItemsM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedL):
                    if (item.SelectedL != null)
                    {
                        item.L = item.SelectedL.ValueItem;
                        item.ItemsK = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && n.L == item.L && !string.IsNullOrEmpty(n.K)).GroupBy(n => n.K)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.L = string.Empty;
                        item.ItemsK = new ObservableCollection<ComboboxItem>();
                    }
                    item.ItemsM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedK):
                    if (item.SelectedK != null)
                    {
                        item.K = item.SelectedK.ValueItem;
                        item.ItemsM = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && n.L == item.L && n.K == item.K && !string.IsNullOrEmpty(n.M)).GroupBy(n => n.M)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.K = string.Empty;
                        item.ItemsM = new ObservableCollection<ComboboxItem>();
                    }
                    item.ItemsTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedM):
                    if (item.SelectedM != null)
                    {
                        item.M = item.SelectedM.ValueItem;
                        item.ItemsTM = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && n.L == item.L && n.K == item.K && n.M == item.M
                            && !string.IsNullOrEmpty(n.TM)).GroupBy(n => n.TM)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.M = string.Empty;
                        item.ItemsTM = new ObservableCollection<ComboboxItem>();
                    }
                    item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedTM):
                    if (item.SelectedTM != null)
                    {
                        item.TM = item.SelectedTM.ValueItem;
                        item.ItemsTTM = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && n.L == item.L && n.K == item.K && n.M == item.M
                                && n.TM == item.TM && !string.IsNullOrEmpty(n.TTM)).GroupBy(n => n.TTM)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.TM = string.Empty;
                        item.ItemsTTM = new ObservableCollection<ComboboxItem>();
                    }
                    item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedTTM):
                    if (item.SelectedTTM != null)
                    {
                        item.TTM = item.SelectedTTM.ValueItem;
                        item.ItemsNG = new ObservableCollection<ComboboxItem>(lstMucLuc
                            .Where(n => n.LNS == item.LNS && n.L == item.L && n.K == item.K
                                && n.M == item.M && n.TM == item.TM && n.TTM == item.TTM
                                && !string.IsNullOrEmpty(n.NG)).GroupBy(n => n.NG)
                            .Select(n => new ComboboxItem() { DisplayItem = n.Key, ValueItem = n.Key }));
                    }
                    else
                    {
                        item.TTM = string.Empty;
                        item.ItemsNG = new ObservableCollection<ComboboxItem>();
                    }
                    break;
                case nameof(PheDuyetThanhToanChiTietModel.SelectedNG):
                    if (item.SelectedNG != null)
                        item.NG = item.SelectedNG.ValueItem;
                    else
                        item.NG = string.Empty;
                    break;
            }
            GetIdMucLucNganSach(item);
            OnPropertyChanged(nameof(Items));
        }

        private void SetMlnsDefault(PheDuyetThanhToanChiTietModel item)
        {
            if (item.ItemsLNS != null && !string.IsNullOrEmpty(item.LNS))
                item.SelectedLNS = item.ItemsLNS.FirstOrDefault(n => n.ValueItem == item.LNS);
            if (item.ItemsL != null && !string.IsNullOrEmpty(item.L))
                item.SelectedL = item.ItemsL.FirstOrDefault(n => n.ValueItem == item.L);
            if (item.ItemsK != null && !string.IsNullOrEmpty(item.K))
                item.SelectedK = item.ItemsK.FirstOrDefault(n => n.ValueItem == item.K);
            if (item.ItemsM != null && !string.IsNullOrEmpty(item.M))
                item.SelectedM = item.ItemsM.FirstOrDefault(n => n.ValueItem == item.M);
            if (item.ItemsTM != null && !string.IsNullOrEmpty(item.TM))
                item.SelectedTM = item.ItemsTM.FirstOrDefault(n => n.ValueItem == item.TM);
            if (item.ItemsTTM != null && !string.IsNullOrEmpty(item.TTM))
                item.SelectedTTM = item.ItemsTTM.FirstOrDefault(n => n.ValueItem == item.TTM);
            if (item.ItemsNG != null && !string.IsNullOrEmpty(item.NG))
                item.SelectedNG = item.ItemsNG.FirstOrDefault(n => n.ValueItem == item.NG);
        }

        public bool CheckRequireMlns()
        {
            if (SelectedItem == null) return false;
            List<string> sErrors = new List<string>();
            //if (SelectedItem.ILoaiDeNghi == (int)PaymentTypeEnum.Type.THU_HOI_NAM_TRUOC)
            //{
            //    SelectedItem.LNS = SelectedItem.L = SelectedItem.K = SelectedItem.M = SelectedItem.TM = SelectedItem.TTM = SelectedItem.NG = string.Empty;
            //    SelectedItem.SelectedLNS = null;
            //    SelectedItem.IIdMuc = SelectedItem.IIdTieuMuc = SelectedItem.IIdTietMuc = SelectedItem.IIdNganh = null;
            //    OnPropertyChanged(nameof(Items));
            //    return true;
            //}
            if (SelectedItem.SelectedLNS == null && SelectedItem.SelectedL == null && SelectedItem.SelectedK == null && SelectedItem.SelectedM == null
                 && SelectedItem.SelectedTM == null && SelectedItem.SelectedTTM == null && SelectedItem.SelectedNG == null)
            {
                sErrors.Add("mục lục ngân sách");
            }
            if (sErrors.Count() != 0)
            {
                MessageBox.Show(string.Format(Resources.MsgErrorDataEmpty, string.Join("\n", sErrors)));
                return false;
            }
            return true;
        }
        #endregion
    }
}
