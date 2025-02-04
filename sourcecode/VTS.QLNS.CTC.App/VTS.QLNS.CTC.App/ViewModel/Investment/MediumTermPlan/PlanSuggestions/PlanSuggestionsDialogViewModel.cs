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
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions
{
    public class PlanSuggestionsDialogViewModel : DialogAttachmentViewModelBase<VdtKhvKeHoach5NamDeXuatModel>
    {
        private static string[] lstDonViExclude = new string[] { "0", "1" };

        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly IVdtKhvKeHoach5NamDeXuatService _service;
        private readonly IVdtKhvKeHoach5NamDeXuatChiTietService _vdtKhvKeHoach5NamChiTietDexuatService;
        private ICollectionView _duAnView;

        public override string Name => "KẾ HOẠCH TRUNG HẠN ĐỀ XUẤT";
                    
        public override string Description { get
            {
                if (Model.Id == Guid.Empty)
                {
                    if (IsAgregate)
                    {
                        return "Tổng hợp kế hoạch trung hạn đề xuất";
                    }
                    else
                    {
                        return "Thêm mới kế hoạch trung hạn đề xuất";
                    }
                }
                else
                {
                    if (IsDieuChinh)
                    {
                        return "Điều chỉnh kế hoạch trung hạn đề xuất";
                    }
                    else
                    {
                        return "Cập nhật kế hoạch trung hạn đề xuất";
                    }
                }
            } 
        }
public override Type ContentType => typeof(PlanSuggestionsDialog);
        public override AttachmentEnum.Type ModuleType => AttachmentEnum.Type.VDT_KHTH_DEXUAT;
        public override PackIconKind IconKind => PackIconKind.Dollar;

        private bool _isAgregate;
        public bool IsAgregate
        {
            get => _isAgregate;
            set => SetProperty(ref _isAgregate, value);
        }

        private ObservableCollection<DuAnKeHoachTrungHanDeXuatModel> _lstDuAn = new ObservableCollection<DuAnKeHoachTrungHanDeXuatModel>();
        public ObservableCollection<DuAnKeHoachTrungHanDeXuatModel> LstDuAn
        {
            get => _lstDuAn;
            set => SetProperty(ref _lstDuAn, value);
        }

        private ObservableCollection<VdtKhvKeHoach5NamDeXuatModel> _lstVoucherAgregate;
        public ObservableCollection<VdtKhvKeHoach5NamDeXuatModel> LstVoucherAgregate
        {
            get => _lstVoucherAgregate;
            set => SetProperty(ref _lstVoucherAgregate, value);
        }

        public bool IsEditable => Model.Id.IsNullOrEmpty();

        private string _sGiaiDoanTu;
        public string SGiaiDoanTu
        {
            get => _sGiaiDoanTu;
            set
            {
                SetProperty(ref _sGiaiDoanTu, value);
                SGiaiDoanDen = !string.IsNullOrEmpty(value) ? (Int32.Parse(_sGiaiDoanTu) + 4).ToString() : "0";
                Model.IGiaiDoanTu = !string.IsNullOrEmpty(value) ? Int32.Parse(SGiaiDoanTu) : 0;
                Model.IGiaiDoanDen = Int32.Parse(SGiaiDoanDen);
                OnPropertyChanged(nameof(SGiaiDoanDen));
            }
        }

        private string _sGiaiDoanDen;
        public string SGiaiDoanDen
        {
            get => _sGiaiDoanDen;
            set => SetProperty(ref _sGiaiDoanDen, value);
        }

        private ComboboxItem _selectedDonVi;
        public ComboboxItem SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                if (value != null)
                {
                    LoadDuAn();
                }
            }
        }

        private ObservableCollection<ComboboxItem> _itemsDonVi;
        public ObservableCollection<ComboboxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private VoucherTabIndex _tabIndex;
        public VoucherTabIndex TabIndex
        {
            get => _tabIndex;
            set
            {
                SetProperty(ref _tabIndex, value);
            }
        }

        private ObservableCollection<ComboboxItem> _itemsProjectType;
        public ObservableCollection<ComboboxItem> ItemsProjectType
        {
            get => _itemsProjectType;
            set => SetProperty(ref _itemsProjectType, value);
        }

        private ComboboxItem _selectedProjectType;
        public ComboboxItem SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                SetProperty(ref _selectedProjectType, value);
                OnPropertyChanged(nameof(ChuyenTiepVisibility));
            }
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

        public Visibility AgregateVisibility
        {
            get => IsAgregate ? Visibility.Collapsed : Visibility.Visible;
        }

        public Visibility NormalVisibility
        {
            get => IsAgregate ? Visibility.Visible : Visibility.Collapsed;
        }

        public Visibility ChuyenTiepVisibility
        {
            get => (SelectedProjectType != null && SelectedProjectType.ValueItem.Equals(((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString())) ? Visibility.Visible : Visibility.Collapsed;
        }
        public PlanSuggestionsDialogViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtKhvKeHoach5NamDeXuatService vdtKhvKeHoach5NamDeXuatService,
            IVdtKhvKeHoach5NamDeXuatChiTietService vdtKhvKeHoach5NamChiTietDexuatService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
            : base(mapper, storageServiceFactory, attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _service = vdtKhvKeHoach5NamDeXuatService;
            _vdtKhvKeHoach5NamChiTietDexuatService = vdtKhvKeHoach5NamChiTietDexuatService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            DownloadFileCommand = new RelayCommand(obj => OnDownloadFile(), obj => SelectedAttachment != null);
            DownloadAllFileCommand = new RelayCommand(obj => OnDownloadAllFile(), obj => ItemsAttachment != null && ItemsAttachment.Count > 0);
            DeleteFileCommand = new RelayCommand(obj => OnDeleteFile(), obj => SelectedAttachment != null);
        }

        public override void Init()
        {
            try
            {
                LoadAttach();
                LoadProjectType();
                LoadDonVi();
                LoadData();
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
                if (SelectedDonVi != null)
                {
                    List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstDuAnEdit = new List<VdtKhvKeHoach5NamDeXuatChiTietQuery>();
                    if (Model != null && Model.Id != Guid.Empty)
                    {
                        lstDuAnEdit = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(Model.Id.ToString()).ToList();
                    }
                    if (!IsDieuChinh)
                    {
                        var lstDuAn = _vdtKhvKeHoach5NamChiTietDexuatService.FindAllDuAnChuyenTiep(SelectedDonVi.ValueItem);
                        LstDuAn = _mapper.Map<ObservableCollection<DuAnKeHoachTrungHanDeXuatModel>>(lstDuAn);
                    }
                    else
                    {
                        var lstDuAn = _vdtKhvKeHoach5NamChiTietDexuatService.FindAllDuAnChuyenTiepDieuChinh(SelectedDonVi.ValueItem);
                        LstDuAn = _mapper.Map<ObservableCollection<DuAnKeHoachTrungHanDeXuatModel>>(lstDuAn);
                    }

                    foreach (var item in LstDuAn)
                    {
                        if (lstDuAnEdit.Any(n => n.IIdDuAnId == item.IIdDuAnId))
                        {
                            item.IsChecked = true;
                        }
                        item.PropertyChanged += DetailModel_PropertyChanged;
                    }
                    _duAnView = CollectionViewSource.GetDefaultView(LstDuAn);
                    _duAnView.Filter = DuAnFilter;
                }
                OnPropertyChanged(nameof(SCountDuAn));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool DuAnFilter(object obj)
        {
            try
            {
                bool result = true;
                var item = (DuAnKeHoachTrungHanDeXuatModel)obj;
                if (!string.IsNullOrWhiteSpace(_searchDuAn))
                {
                    result = result && !(string.IsNullOrEmpty(item.STenDuAn)) && !(string.IsNullOrEmpty(item.SMaDuAn))
                                && (item.STenDuAn.ToLower().Contains(_searchDuAn.ToLower()) || item.SMaDuAn.ToLower().Contains(_searchDuAn.ToLower()));
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return true;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                DuAnKeHoachTrungHanDeXuatModel item = (DuAnKeHoachTrungHanDeXuatModel)sender;
                switch (args.PropertyName)
                {
                    case nameof(DuAnKeHoachTrungHanDeXuatModel.IsChecked):
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

        private void LoadProjectType()
        {
            ItemsProjectType = new ObservableCollection<ComboboxItem>(new[]
            {
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI), ((int)LoaiDuAnEnum.Type.KHOI_CONG_MOI).ToString()),
                new ComboboxItem(LoaiDuAnEnum.Get((int)LoaiDuAnEnum.Type.CHUYEN_TIEP), ((int)LoaiDuAnEnum.Type.CHUYEN_TIEP).ToString())
            });
        }

        private void LoadDonVi()
        {
            try
            {
                var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                    .Where(n => lstDonViExclude.Contains(n.Loai));
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
                var drpItem = lstUnitViaUser.Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, HiddenValue = n.Id.ToString(), DisplayItem = (n.IIDMaDonVi + " - " + n.TenDonVi) });
                ItemsDonVi = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
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
                if (Model.Id == Guid.Empty)
                {
                    Model.DNgayQuyetDinh = DateTime.Now;
                    //Model.IGiaiDoanTu = _sessionService.Current.YearOfWork;
                    Model.IGiaiDoanTu = _service.FindCurrentPeriod(_sessionService.Current.YearOfWork);
                    Model.IGiaiDoanDen = Model.IGiaiDoanTu + 4;
                    SGiaiDoanTu = Model.IGiaiDoanTu.ToString();
                    SGiaiDoanDen = Model.IGiaiDoanDen.ToString();

                    if (ItemsDonVi != null && ItemsDonVi.Count > 0)
                    {
                        SelectedDonVi = ItemsDonVi.FirstOrDefault();
                    }
                    if (ItemsProjectType != null && ItemsProjectType.Count > 0)
                    {
                        SelectedProjectType = ItemsProjectType.FirstOrDefault();
                    }
                }
                else
                {
                    SGiaiDoanTu = Model.IGiaiDoanTu.ToString();
                    SGiaiDoanDen = Model.IGiaiDoanDen.ToString();

                    SelectedDonVi = ItemsDonVi.FirstOrDefault(x => x.ValueItem.Equals(Model.IIdMaDonVi));
                    if (Model.ILoai != null)
                    {
                        SelectedProjectType = ItemsProjectType.FirstOrDefault(x => x.ValueItem.Equals(Model.ILoai.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                if (!Validate()) return;

                VdtKhvKeHoach5NamDeXuat entity;
                if (Model.Id == Guid.Empty)
                {
                    if (IsAgregate)
                    {
                        List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstQuery = new List<VdtKhvKeHoach5NamDeXuatChiTietQuery>();

                        int intIndex = 0;
                        foreach (var item in LstVoucherAgregate.Select(x => x.Id).ToList())
                        {
                            intIndex += 1;
                            List<VdtKhvKeHoach5NamDeXuatChiTietQuery> lstItem = _vdtKhvKeHoach5NamChiTietDexuatService.FindConditionIndex(item.ToString()).ToList();
                            lstItem.Select(x => { x.SMaOrder = string.Format("{0}_{1}", intIndex.ToString("D3"), x.SMaOrder); x.IIdTongHop = item; return x; }).ToList();
                            lstQuery.AddRange(lstItem);
                        }

                        List<VdtKhvKeHoach5NamDeXuatChiTiet> lstDetail = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(lstQuery);

                        entity = new VdtKhvKeHoach5NamDeXuat();
                        _mapper.Map(Model, entity);
                        entity.Id = Guid.NewGuid();
                        entity.DDateCreate = DateTime.Now;
                        entity.SUserCreate = _sessionService.Current.Principal;
                        entity.FGiaTriKeHoach = lstDetail.Where(x => x.IsStatus.Equals(MediumTermPlanType.PROJECT)).Sum(x => x.FGiaTriKeHoach);
                        entity.IGiaiDoanTu = _lstVoucherAgregate.FirstOrDefault().IGiaiDoanTu;
                        entity.IGiaiDoanDen = _lstVoucherAgregate.FirstOrDefault().IGiaiDoanDen;
                        // Tổng hợp kế hoạch trung hạn
                        _service.Agregate(entity, lstDetail, LstVoucherAgregate.Select(x => x.Id).ToList());
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

                        if (!lstDv.Contains(_selectedDonVi.ValueItem))
                        {
                            MessageBox.Show(string.Format(Resources.UserManagerUnitWarning, _sessionService.Current.Principal, _selectedDonVi.DisplayItem), Resources.Alert);
                            return;
                        }

                        // Thêm mới
                        entity = new VdtKhvKeHoach5NamDeXuat();
                        _mapper.Map(Model, entity);
                        entity.Id = Guid.NewGuid();
                        entity.DDateCreate = DateTime.Now;
                        entity.SUserCreate = _sessionService.Current.Principal;
                        entity.IIdMaDonViQuanLy = _selectedDonVi.ValueItem;
                        entity.IIdDonViQuanLyId = Guid.Parse(_selectedDonVi.HiddenValue);
                        entity.ILoai = int.Parse(_selectedProjectType.ValueItem);
                        entity.NamLamViec = _sessionService.Current.YearOfWork;
                        entity.BIsGoc = true;
                        entity.BActive = true;
                        entity.BKhoa = false;
                        _service.Add(entity);
                    }
                }
                else if (IsDieuChinh)
                {
                    // Details
                    var lstQuery = _vdtKhvKeHoach5NamChiTietDexuatService.FindListVoucherDetailsModified(Model.Id).ToList();
                    List<VdtKhvKeHoach5NamDeXuatChiTiet> lstDetails = _mapper.Map<List<VdtKhvKeHoach5NamDeXuatChiTiet>>(lstQuery);

                    lstDetails.Select(x =>
                    {
                        if (x.IdParent.HasValue)
                        {
                            bool parentInList = lstDetails.Select(x => x.Id).ToList().Contains(x.IdParent.Value);
                            if (!parentInList)
                            {
                                x.IdParent = null;
                            }
                        }
                        return x;
                    }).ToList();

                    //List<Guid> lstIdDuAnSelected = LstDuAn.Select(x => x.IIdDuAnId.Value).ToList();
                    //List<VdtKhvKeHoach5NamDeXuatChiTiet> lstDuAnDuocDuyetSelected =
                    //    lstDetails.Where(x => x.IIdDuAnId.HasValue && lstIdDuAnSelected.Contains(x.IIdDuAnId.Value)).ToList();
                    List<Guid> lstIdDuAnSelected = LstDuAn.Select(x => x.Id_DuAnKhthDeXuat.HasValue ? x.Id_DuAnKhthDeXuat.Value : Guid.Empty).ToList();
                    List<VdtKhvKeHoach5NamDeXuatChiTiet> lstDuAnDuocDuyetSelected =
                        lstDetails.Where(x => x.IIdDuAnId.HasValue && lstIdDuAnSelected.Contains(x.Id)).ToList();
                    var lstDuAnDuocDuyetSelectedDistinct = lstDuAnDuocDuyetSelected.GroupBy(elem => elem.STen).Select(group => group.Last()).ToList();
                    //if (lstDuAnDuocDuyetSelected.Count > 0)
                    if (lstDuAnDuocDuyetSelectedDistinct.Count > 0)
                    {
                        if (!lstIdDuAnSelected.All(x => lstDetails.Any(y => y.IIdDuAnId.HasValue && y.IIdDuAnId.Equals(x))))
                        {
                            MessageBoxResult messageBoxResult = MessageBoxHelper.Confirm(Resources.MsgConfirmVoucherModified);
                            if (messageBoxResult == MessageBoxResult.No)
                            {
                                return;
                            }
                        }

                        entity = new VdtKhvKeHoach5NamDeXuat();
                        _mapper.Map(Model, entity);
                        // Entity
                        entity.Id = Guid.NewGuid();
                        entity.IIdParentId = Model.Id;
                        entity.BActive = true;
                        entity.BIsGoc = false;
                        entity.BKhoa = false;
                        entity.DDateCreate = DateTime.Now;
                        entity.SUserCreate = _sessionService.Current.Principal;

                        // Điều chỉnh kế hoạch trung hạn
                        //_service.Adjust(entity, lstDuAnDuocDuyetSelected);
                        _service.Adjust(entity, lstDuAnDuocDuyetSelectedDistinct);
                    }
                    else
                    {
                        // Thông báo không dc điều chỉnh
                        MessageBox.Show(Resources.MsgVoucherModified, Resources.Alert);
                        return;
                    }
                }
                else
                {
                    // Sửa
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);

                    entity.IIdMaDonViQuanLy = _selectedDonVi.ValueItem;
                    entity.NamLamViec = _sessionService.Current.YearOfWork;
                    entity.IIdDonViQuanLyId = Guid.Parse(_selectedDonVi.HiddenValue);
                    entity.ILoai = int.Parse(_selectedProjectType.ValueItem);
                    entity.BIsGoc = true;
                    entity.BActive = true;
                    entity.DDateUpdate = DateTime.Now;
                    entity.SUserUpdate = _sessionService.Current.Principal;
                    _service.Update(entity);
                }

                // Save attach file
                SaveAttachment(entity.Id);
                DialogHost.Close("RootDialog");
                VdtKhvKeHoach5NamDeXuatModel model = _mapper.Map<VdtKhvKeHoach5NamDeXuatModel>(entity);
                List<Guid> lstIdDuAn = LstDuAn.Where(x => x.IsChecked).Select(x => x.IIdDuAnId.Value).ToList();
                model.LstIdDuAn = lstIdDuAn;
                SavedAction?.Invoke(model);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool Validate()
        {
            StringBuilder messageBuilder = new StringBuilder();

            if (_selectedDonVi == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Đơn vị quản lý");
            }
            if (_selectedProjectType == null)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Loại dự án");
            }
            if (string.IsNullOrEmpty(Model.SSoQuyetDinh))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Số kế hoạch");
            }
            if (!Model.DNgayQuyetDinh.HasValue)
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Ngày lập");
            }
            if (string.IsNullOrEmpty(SGiaiDoanTu))
            {
                messageBuilder.AppendFormat(Resources.MsgErrorRequire + "\n", "Giai đoạn");
            }
            if(Model.IGiaiDoanTu % 5 != 1)
            {
                messageBuilder.AppendFormat("Giai đoạn không hợp lệ ! \n");                
            }

            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return false;
            }

            if (Model.Id.IsNullOrEmpty() && !(IsAgregate || IsDieuChinh) && !CheckDataUnique())
            {
                string message = string.Format(Resources.MsgErrorGiaiDoanExisted, SelectedDonVi.DisplayItem, Model.IGiaiDoanTu, Model.IGiaiDoanDen);
                MessageBox.Show(message, Resources.Alert);
                return false;
            }

            if (Model.Id.IsNullOrEmpty() && !(IsAgregate || IsDieuChinh) && !CheckGiaiDoan())
            {
                MessageBox.Show(Resources.VoucherPeriodInValid, Resources.Alert);
                return false;
            }

            if (!string.IsNullOrEmpty(Model.SSoQuyetDinh) && _service.IsExistSoQuyetDinh(Model.SSoQuyetDinh, (IsDieuChinh || (IsAgregate && TabIndex == VoucherTabIndex.VOUCHER)) ? Guid.Empty : Model.Id))
            {
                MessageBox.Show(Resources.MsgTrungSoKeHoach, Resources.Alert);
                return false;
            }

            return true;
        }

        private bool CheckGiaiDoan()
        {
            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            if (SelectedDonVi != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(SelectedDonVi.ValueItem));
            }
            if (SelectedProjectType != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(SelectedProjectType.ValueItem));
            }
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => string.IsNullOrEmpty(x.STongHop));
            var rs = _service.FindByCondition(predicate).ToList();
            /* 
             * bỏ check gđ
            foreach (var item in rs)
            {
                if (Model.IGiaiDoanTu >= item.IGiaiDoanTu && Model.IGiaiDoanTu <= item.IGiaiDoanDen)
                {
                    return false;
                }
            }
            */
            return true;
        }

        private bool CheckDataUnique()
        {

            var predicate = PredicateBuilder.True<VdtKhvKeHoach5NamDeXuat>();
            if (SelectedDonVi != null)
            {
                predicate = predicate.And(x => x.IIdMaDonViQuanLy.Equals(SelectedDonVi.ValueItem));
            }
            if (SelectedProjectType != null)
            {
                predicate = predicate.And(x => x.ILoai == int.Parse(SelectedProjectType.ValueItem));
            }
            predicate = predicate.And(x => x.IGiaiDoanTu == Model.IGiaiDoanTu);
            predicate = predicate.And(x => x.IGiaiDoanDen == Model.IGiaiDoanDen);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            var rs = _service.FindByCondition(predicate).ToList();
            /* 
             * bỏ check gđ
            if (rs.Count > 0)
            {
                return false;
            }
            */
            return true;
        }

        public override void OnClose(object obj)
        {
            try
            {
                base.OnClose(obj);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
