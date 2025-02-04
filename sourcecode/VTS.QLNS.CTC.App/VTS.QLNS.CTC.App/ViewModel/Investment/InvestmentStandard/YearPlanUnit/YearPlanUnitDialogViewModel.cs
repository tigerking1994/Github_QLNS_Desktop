using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.VonNamDonVi;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.VonNamDonVi
{
    public class YearPlanUnitDialogViewModel : DialogViewModelBase<PhanBoVonDonViModel>
    {
        private static string _sServiceName = "Chứng từ kế hoạch vốn năm đề xuất";
        private readonly INsDonViService _nsDonViService;
        private readonly IMucLucNganSachService _mlNganSachService;
        private readonly IVdtKhvPhanBoVonDonViService _phanBoVonService;
        private readonly INsNguonNganSachService _nsNguonVonService;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private ICollectionView _duAnView;
        private readonly IMapper _mapper;
        private readonly ISysAuditLogService _log;
        private DateTime _dStartDate;

        public Action<object> ClosedAction;
        public override string Name => "Quản lý kế hoạch vốn năm đề xuất";

        public override Type ContentType => typeof(VonNamDonViDialog);
        public bool IsDieuChinh { get; set; }
        public string sNguonVon { get; set; }
        public bool IsInsert => Model.Id == Guid.Empty;

        #region Componer

        public List<PhanBoVonDonViModel> LstVoucherAgregate;

        private ObservableCollection<PhanBoVonDonViModel> _voucherAgregates;
        public ObservableCollection<PhanBoVonDonViModel> VoucherAgregates
        {
            get => _voucherAgregates;
            set => SetProperty(ref _voucherAgregates, value);
        }

        private bool _isAgregate;
        public bool IsAggregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
        }

        private ComboboxItem _cbxLoaiDonViSelected;
        public ComboboxItem CbxLoaiDonViSelected
        {
            get => _cbxLoaiDonViSelected;
            set
            {
                SetProperty(ref _cbxLoaiDonViSelected, value);
                LoadDuAn();
            }
        }

        private ObservableCollection<ComboboxItem> _cbxLoaiDonVi;
        public ObservableCollection<ComboboxItem> CbxLoaiDonVi
        {
            get => _cbxLoaiDonVi;
            set => SetProperty(ref _cbxLoaiDonVi, value);
        }

        private ObservableCollection<ComboboxItem> _drpNguonVon;
        public ObservableCollection<ComboboxItem> DrpNguonVon
        {
            get => _drpNguonVon;
            set => SetProperty(ref _drpNguonVon, value);
        }

        private ComboboxItem _drpNguonVonSelected;
        public ComboboxItem DrpNguonVonSelected
        {
            get => _drpNguonVonSelected;
            set
            {
                SetProperty(ref _drpNguonVonSelected, value);
                LoadDuAn();
            }
        }

        private string _iNamKeHoach;
        public string INamKeHoach
        {
            get => _iNamKeHoach;
            set
            {
                SetProperty(ref _iNamKeHoach, value);
                LoadDuAn();
            }
        }

        private DateTime? _dNgayQuyetDinh;
        public DateTime? DNgayQuyetDinh
        {
            get => _dNgayQuyetDinh;
            set
            {
                SetProperty(ref _dNgayQuyetDinh, value);
                //LoadDuAn();
            }
        }

        private ObservableCollection<DuAnKeHoachVonNamDeXuatModel> _lstDuAn;
        public ObservableCollection<DuAnKeHoachVonNamDeXuatModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
        }

        private bool _selectAllDuAn;
        public bool SelectAllDuAn
        {
            get => (LstDuAn == null || !LstDuAn.Any()) ? false : LstDuAn.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllDuAn, value);
                if (LstDuAn != null)
                {
                    LstDuAn.Select(c => { c.IsChecked = _selectAllDuAn; return c; }).ToList();
                }
            }
        }

        private string _searchDuAn;
        public string SearchDuAn
        {
            get => _searchDuAn;
            set
            {
                SetProperty(ref _searchDuAn, value);
                _duAnView.Refresh();
            }
        }

        private string _sCountDuAn;
        public string SCountDuAn
        {
            get => LstDuAn != null ? string.Format("{0}/{1}", LstDuAn.Count(n => n.IsChecked), LstDuAn.Count) : "0/0";
            set => SetProperty(ref _sCountDuAn, value);
        }

        public Visibility NormalVisibility
        {
            get => IsAggregate ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility AgregateVisibility
        {
            get => IsAggregate ? Visibility.Visible : Visibility.Collapsed;
        }

        private ComboboxItem _selectedFilterHasQDDT;
        public ComboboxItem SelectedFilterHasQDDT
        {
            get => _selectedFilterHasQDDT;
            set
            {
                SetProperty(ref _selectedFilterHasQDDT, value);
                LoadDuAn();
            }
        }

        public ObservableCollection<ComboboxItem> _listFilterHasQDDT;
        public ObservableCollection<ComboboxItem> ListFilterHasQDDT
        {
            get => _listFilterHasQDDT;
            set => SetProperty(ref _listFilterHasQDDT, value);
        }
        #endregion

        public YearPlanUnitDialogViewModel(INsDonViService nsDonViService,
            IMucLucNganSachService mlNganSachService,
            ISessionService sessionService,
            IVdtKhvPhanBoVonDonViService phanBoVonService,
            INsNguonNganSachService nsNguonVonService,
            ILog logger,
            IMapper mapper,
            ISysAuditLogService log,
            YearPlanUnitDetailViewModel VonNamDonViDetailViewModel)
        {
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _mlNganSachService = mlNganSachService;
            _phanBoVonService = phanBoVonService;
            _nsNguonVonService = nsNguonVonService;
            _mapper = mapper;
            _log = log;
        }

        public override void Init()
        {
            try
            {
                LoadData();
                LoadDuAn();
                loadFilterHasQDDT();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                if (LstVoucherAgregate != null)
                {
                    VoucherAgregates = new ObservableCollection<PhanBoVonDonViModel>(LstVoucherAgregate);
                }

                if (Model != null && Model.Id != Guid.Empty)
                {
                    if (IsDieuChinh)
                    {
                        Description = "Điều chỉnh kế hoạch vốn năm đề xuất";
                    }
                    else
                    {
                        Description = "Cập nhật kế hoạch vốn năm đề xuất";
                    }

                    _iNamKeHoach = Model.iNamKeHoach != 0 ? Model.iNamKeHoach.ToString() : string.Empty;
                    _dNgayQuyetDinh = Model.dNgayQuyetDinh;
                    LoadComboBoxLoaiDonVi(Model.iID_MaDonViQuanLy);
                    GetNguonVon(Model.iId_NguonVonId);
                    OnPropertyChanged(nameof(Description));
                }
                else
                {
                    if (IsAggregate)
                    {
                        Description = "Tổng hợp kế hoạch vốn năm đề xuất";
                    }
                    else
                    {
                        Description = "Thêm mới kế hoạch vốn năm đề xuất";
                    }
                    DNgayQuyetDinh = DateTime.Now;
                    _iNamKeHoach = _sessionService.Current.YearOfWork.ToString();
                    LoadComboBoxLoaiDonVi();
                    GetNguonVon();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadDuAn()
        {
            try
            {
                List<VdtKhvPhanBoVonDonViChiTietQuery> lstDuAnEdit = new List<VdtKhvPhanBoVonDonViChiTietQuery>();
                if (!Model.iID_ParentId.HasValue && !Model.Id.IsNullOrEmpty())
                {
                    lstDuAnEdit = _phanBoVonService.GetPhanBoVonChiTietByParentId(Model.Id).ToList();
                }
                else if (Model.iID_ParentId.HasValue)
                {
                    List<VdtKhvPhanBoVonDonViChiTietDieuChinhQuery> itemlstDuAnAdjust = _phanBoVonService.GetPhanBoVonChiTietDieuChinhByParentId(Model.Id).ToList();
                    lstDuAnEdit = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTietQuery>>(itemlstDuAnAdjust);
                }

                List<VdtKhvPhanBoVonDonViChiTietQuery> data = new List<VdtKhvPhanBoVonDonViChiTietQuery>();
                //if (!string.IsNullOrEmpty(INamKeHoach) && DNgayQuyetDinh != null && DNgayQuyetDinh.HasValue && CbxLoaiDonViSelected != null && DrpNguonVonSelected != null)
                 if (!string.IsNullOrEmpty(INamKeHoach) && CbxLoaiDonViSelected != null && DrpNguonVonSelected != null)
                 {
                    int filterHasQDDTLocal = -1;
                    // trong trường hợp SelectedFilterHasQDDT null
                    if (SelectedFilterHasQDDT != null)
                    {
                        if (SelectedFilterHasQDDT.ValueItem != null)
                        {
                            filterHasQDDTLocal = Int32.Parse(SelectedFilterHasQDDT.ValueItem);
                        }
                        if (!IsDieuChinh)
                            data = _phanBoVonService.GetAllDuAnInPhanBoVon(Int32.Parse(INamKeHoach), DNgayQuyetDinh != null ? DNgayQuyetDinh.Value : DateTime.Now, CbxLoaiDonViSelected.ValueItem, Int32.Parse(DrpNguonVonSelected.ValueItem), filterHasQDDTLocal).ToList();
                        else
                            data = _phanBoVonService.GetAllDuAnInPhanBoVon(Int32.Parse(INamKeHoach), DNgayQuyetDinh != null ? DNgayQuyetDinh.Value : DateTime.Now, CbxLoaiDonViSelected.ValueItem, Int32.Parse(DrpNguonVonSelected.ValueItem), filterHasQDDTLocal).ToList();
                    }
                }

                data = data.GroupBy(x => new { x.iID_DuAnID, x.ILoaiDuAn }).Select(grp => grp.FirstOrDefault()).ToList();
                LstDuAn = _mapper.Map<ObservableCollection<DuAnKeHoachVonNamDeXuatModel>>(data);

                foreach (var item in LstDuAn)
                {
                    if (lstDuAnEdit.Any(n => n.iID_DuAnID == item.IIdDuAnId))
                    {
                        item.IsChecked = true;
                    }
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
                _duAnView = CollectionViewSource.GetDefaultView(LstDuAn);
                _duAnView.Filter = DuAnFilter;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                DuAnKeHoachVonNamDeXuatModel item = (DuAnKeHoachVonNamDeXuatModel)sender;
                switch (args.PropertyName)
                {
                    case nameof(DuAnKeHoachVonNamDeXuatModel.IsChecked):
                        SCountDuAn = string.Format("{0}/{1}", LstDuAn.Count(n => n.IsChecked), LstDuAn.Count);
                        if (LstDuAn.Count(n => n.IsChecked) == LstDuAn.Count)
                        {
                            SelectAllDuAn = true;
                        }
                        else if (LstDuAn.Count(n => !n.IsChecked) == LstDuAn.Count)
                        {
                            SelectAllDuAn = false;
                        }
                        break;
                }
                OnPropertyChanged(nameof(SCountDuAn));
                OnPropertyChanged(nameof(SelectAllDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DuAnFilter(object obj)
        {
            var item = (DuAnKeHoachVonNamDeXuatModel)obj;
            var result = true;

            if (!string.IsNullOrWhiteSpace(_searchDuAn))
            {
                result = result && !(string.IsNullOrEmpty(item.STenDuAn)) && !(string.IsNullOrEmpty(item.SMaDuAn))
                                && (item.STenDuAn.ToLower().Contains(_searchDuAn.ToLower()) || item.SMaDuAn.ToLower().Contains(_searchDuAn.ToLower()));
            }
            return result;
        }

        private void loadFilterHasQDDT()
        {
            ListFilterHasQDDT = new ObservableCollection<ComboboxItem>();
            ListFilterHasQDDT.Add(new ComboboxItem { DisplayItem = HasQDDT.Has_QDDT, ValueItem = "1" });
            ListFilterHasQDDT.Add(new ComboboxItem { DisplayItem = HasQDDT.No_QDDT, ValueItem = "0" });
            ListFilterHasQDDT.Add(new ComboboxItem { DisplayItem = HasQDDT.No_CTDT, ValueItem = "2" });
            ListFilterHasQDDT.Add(new ComboboxItem { DisplayItem = HasQDDT.ALL, ValueItem = "-1" });

            SelectedFilterHasQDDT = ListFilterHasQDDT.LastOrDefault();
        }

        #region Relay Command
        public override void OnSave()
        {
            try
            {
                _dStartDate = DateTime.Now;
                string sError = string.Empty;
                StringBuilder messageBuilder = new StringBuilder();
                if (Model == null) Model = new PhanBoVonDonViModel();
                Model.BKhoa = false;
                if (string.IsNullOrEmpty(Model.sSoQuyetDinh))
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Số kế hoạch");
                }
                if (DNgayQuyetDinh == null && !DNgayQuyetDinh.HasValue)
                {
                    messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Ngày lập");
                }

                if (!IsAggregate)
                {
                    if (CbxLoaiDonViSelected == null)
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Đơn vị quản lý");
                    }
                    if (DrpNguonVonSelected == null)
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Nguồn vốn");
                    }
                    if (string.IsNullOrEmpty(INamKeHoach))
                    {
                        messageBuilder.AppendFormat(Resources.MsgErrorRequire, "Năm kế hoạch");
                    }
                }

                if (messageBuilder.Length != 0)
                {
                    MessageBox.Show(messageBuilder.ToString());
                    LoadData();
                    return;
                }

                var dataInsert = _mapper.Map<VdtKhvPhanBoVonDonVi>(Model);
                VdtKhvPhanBoVonDonVi entity = new VdtKhvPhanBoVonDonVi();

                //if (IsAggregate)
                //{
                    dataInsert.INamKeHoach = int.Parse(INamKeHoach);
                    dataInsert.DNgayQuyetDinh = DNgayQuyetDinh.Value;
                //}

                if (dataInsert.Id == Guid.Empty)
                {
                    if (IsAggregate)
                    {
                        List<VdtKhvPhanBoVonDonViChiTietQuery> lstQuery = new List<VdtKhvPhanBoVonDonViChiTietQuery>();

                        foreach (var item in VoucherAgregates.Select(x => x.Id).ToList())
                        {
                            List<VdtKhvPhanBoVonDonViChiTietQuery> lstItem = _phanBoVonService.GetPhanBoVonChiTietByParentId(item).ToList();
                            lstQuery.AddRange(lstItem);
                        }

                        List<VdtKhvPhanBoVonDonViChiTiet> lstDetail = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(lstQuery);

                        entity = new VdtKhvPhanBoVonDonVi();
                        _mapper.Map(Model, entity);
                        entity.Id = Guid.NewGuid();
                        entity.DDateCreate = DateTime.Now;
                        entity.SUserCreate = _sessionService.Current.Principal;
                        entity.DNgayQuyetDinh = DNgayQuyetDinh.Value;
                        // Tổng hợp kế hoạch vốn năm
                        entity.FThuHoiVonUngTruoc = lstDetail.Sum(x => x.FThuHoiVonUngTruoc);
                        entity.FThanhToan = lstDetail.Sum(x => x.FThanhToan);
                        lstDetail.Select(x => { x.IIdPhanBoVonDonVi = entity.Id; return x; }).ToList();
                        _phanBoVonService.Agregate(entity, lstDetail);
                    }
                    else
                    {
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

                        if (!lstDv.Contains(CbxLoaiDonViSelected.ValueItem))
                        {
                            MessageBox.Show(string.Format(Resources.VoucherUserManagerKHVNWarning, _sessionService.Current.Principal, CbxLoaiDonViSelected.DisplayItem), Resources.Alert);
                            return;
                        }

                        dataInsert.Id = Guid.NewGuid();
                        dataInsert.IIdDonViQuanLyId = Guid.Parse(CbxLoaiDonViSelected.HiddenValue);
                        dataInsert.IIdMaDonViQuanLy = CbxLoaiDonViSelected.ValueItem;
                        dataInsert.IIdNguonVonId = int.Parse(DrpNguonVonSelected.ValueItem);
                        dataInsert.BKhoa = false;
                        dataInsert.FThanhToan = 0;
                        dataInsert.FThuHoiVonUngTruoc = 0;
                        if (_phanBoVonService.ExistPhanBoVonBySoQuyetDinhAndDonVi(dataInsert))
                        {
                            messageBuilder.AppendFormat(Resources.MsgErrorDuplicateSoQuyetDinh, CbxLoaiDonViSelected.DisplayItem, Model.sSoQuyetDinh);
                            messageBuilder.AppendLine();
                        }
                        //if (_phanBoVonService.ExistPhanBoVonByNamKeHoachAndDonViQuanLy(dataInsert))
                        //{
                        //    messageBuilder.AppendFormat(Resources.MsgErrorKeHoachNamDonViExist,
                        //        CbxLoaiDonViSelected.DisplayItem,
                        //        DrpNguonVonSelected.DisplayItem,
                        //        INamKeHoach.ToString());
                        //}
                        if (messageBuilder.Length != 0)
                        {
                            MessageBox.Show(messageBuilder.ToString());
                            return;
                        }

                        _phanBoVonService.Insert(dataInsert, _sessionService.Current.Principal, ref sError);
                    }
                }
                else
                {
                    dataInsert.INamKeHoach = int.Parse(INamKeHoach);
                    dataInsert.DNgayQuyetDinh = DNgayQuyetDinh.Value;
                    if (!IsDieuChinh)
                    {
                        if (_phanBoVonService.ExistPhanBoVonBySoQuyetDinhAndDonVi(dataInsert))
                        {
                            MessageBox.Show(string.Format(Resources.MsgErrorDuplicateSoQuyetDinh, CbxLoaiDonViSelected.DisplayItem, Model.sSoQuyetDinh));
                            return;
                        }
                        _phanBoVonService.UpdatePhanBoVon(dataInsert, _sessionService.Current.Principal, ref sError);
                    }
                    else
                    {
                        VdtKhvPhanBoVonDonVi entityDuplicate = _mapper.Map<VdtKhvPhanBoVonDonVi>(Model);
                        entity = new VdtKhvPhanBoVonDonVi();
                        entityDuplicate.CloneObj(entity);

                        entity.Id = Guid.NewGuid();
                        entity.DDateCreate = DateTime.Now;
                        entity.IIdParentId = entityDuplicate.Id;
                        entity.SUserCreate = _sessionService.Current.Principal;
                        entity.IIdMaDonViQuanLy = _cbxLoaiDonViSelected.ValueItem;
                        entity.IIdDonViQuanLyId = Guid.Parse(_cbxLoaiDonViSelected.HiddenValue);
                        entity.BActive = true;
                        entity.BIsGoc = false;

                        List<PhanBoVonDonViQuery> lstQuery = _phanBoVonService.GetPhanBoVonDonViDieuChinh(entityDuplicate != null ? entityDuplicate.Id.ToString() : Guid.Empty.ToString()).ToList();
                        if (lstQuery != null && lstQuery.Count() == 0)
                        {
                            MessageBox.Show(Resources.VoucherAdjustKhvn, Resources.Alert);
                            return;
                        }
                        List<VdtKhvPhanBoVonDonViChiTiet> lstDetail = _mapper.Map<List<VdtKhvPhanBoVonDonViChiTiet>>(lstQuery);
                        _phanBoVonService.Adjust(entity, lstDetail);
                    }
                }
                _log.WriteLog(Resources.ApplicationName, _sServiceName, Model.ActionState, _dStartDate, TransactionStatus.Success, _sessionService.Current.Principal);
                DialogHost.CloseDialogCommand.Execute(null, null);

                PhanBoVonDonViModel model;

                if (IsAggregate) 
                {
                    model = _mapper.Map<PhanBoVonDonViModel>(Model.Id == Guid.Empty ? entity : dataInsert);
                }
                else {
                    model = _mapper.Map<PhanBoVonDonViModel>(IsDieuChinh ? entity : dataInsert);
                }

                model.IsAdjust = IsDieuChinh;
                model.LstDuAnId = LstDuAn.Where(x => x.IsChecked).Select(x => x.IIdDuAnId).ToList();
                model.IsChooseDuAn = true;
                SavedAction?.Invoke(model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion

        #region Helper
        public override void OnClose(object obj)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                ClosedAction?.Invoke(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LoadComboBoxLoaiDonVi(string iIdDonVi = null)
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Where(n => (string.IsNullOrEmpty(iIdDonVi) || n.IIDMaDonVi == iIdDonVi));

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
                cbxLoaiDonViData.Select(item =>
                {
                    if (lstDv.Contains(item.IIDMaDonVi))
                    {
                        lstUnitViaUser.Add(item);
                    }
                    return item;
                }).ToList();

                CbxLoaiDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(lstUnitViaUser);

                if (!string.IsNullOrEmpty(iIdDonVi))
                {
                    CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault(n => n.ValueItem.ToUpper() == iIdDonVi.ToUpper());
                }
                else
                {
                    CbxLoaiDonViSelected = _cbxLoaiDonVi.FirstOrDefault();
                }
                OnPropertyChanged(nameof(CbxLoaiDonVi));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void GetNguonVon(int? iIdNguonVon = null)
        {
            try
            {
                var cbxNguonVonData = _nsNguonVonService.FindNguonNganSach().OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.ToString(), DisplayItem = n.STen });
                _drpNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonData);
                if (iIdNguonVon.HasValue)
                {
                    DrpNguonVonSelected = _drpNguonVon.FirstOrDefault(n => n.ValueItem == iIdNguonVon.Value.ToString());
                }
                else
                {
                    DrpNguonVonSelected = _drpNguonVon.FirstOrDefault();
                }
                OnPropertyChanged(nameof(DrpNguonVon));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        #endregion
    }
}
