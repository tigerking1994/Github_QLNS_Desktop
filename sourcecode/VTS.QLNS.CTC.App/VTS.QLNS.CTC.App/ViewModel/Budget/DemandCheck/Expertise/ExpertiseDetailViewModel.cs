using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise
{
    public class ExpertiseDetailViewModel : DetailViewModelBase<ExpertiseModel, ExpertiseModelDetailModel>
    {
        private ISktNganhThamDinhService _chungTuService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private ISktNganhThamDinhChiTietService _chungTuChiTietService;
        private INsDonViService _nsDonViService;
        private IDanhMucService _danhMucService;
        private readonly ISktMucLucService _sktMucLucService;
        private ICollectionView _dataDetailFilter;
        private ICollectionView _searchPopupView;
        private readonly ILog _logger;
        private readonly INsNguoiDungDonViService _nsNguoiDungDonViService;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        public override Type ContentType => typeof(View.Budget.DemandCheck.Expertise.ExpertiseDetail);
        public override PackIconKind IconKind => PackIconKind.FileDocumentBoxMultiple;
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsEnableButtonDelete => SelectedItem != null && !SelectedItem.IsHangCha;
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public string TitleColumn => (Model != null && Model.ILoaiChungTu.HasValue && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key) ?
            "Mua hàng cấp hiện vật" : "Tự chi";
        public Visibility VisibilityDonVi => (Model != null && Model.ILoaiChungTu.HasValue && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key) ?
            Visibility.Collapsed : Visibility.Visible;
        public int PhanLoai;

        public Visibility VisibleCTCDeNghi => PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisibleNganhThamDinh => PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisibleCTCDeNghiNSDacThu => (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
            ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisibleNganhThamDinhNSDacThu => (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
            ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisibleNganhThamDinhNSSuDung => (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSSD_Key)
            ? Visibility.Visible : Visibility.Collapsed;

        private ObservableCollection<ComboboxItem> _typeDisplays;
        public ObservableCollection<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _selectedTypeDisplays;
        public string SelectedTypeDisplays
        {
            get => _selectedTypeDisplays;
            set
            {
                if (SetProperty(ref _selectedTypeDisplays, value) && _dataDetailFilter != null)
                {
                    OnRefeshFilter();
                }
            }
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set
            {
                SetProperty(ref _isPopupOpen, value);
            }
        }

        private string _popupSearchText;
        public string PopupSearchText
        {
            set
            {
                SetProperty(ref _popupSearchText, value);
                _searchPopupView?.Refresh();
            }
            get => _popupSearchText;
        }

        private NsSktChungTuChiTietModel _nsSktChungTuChiTietSearchModel;
        public NsSktChungTuChiTietModel NsSktChungTuChiTietSearchModel
        {
            get => _nsSktChungTuChiTietSearchModel;
            set => SetProperty(ref _nsSktChungTuChiTietSearchModel, value);
        }

        private ObservableCollection<SktMucLucModel> _sktMucLucModelItems;
        public ObservableCollection<SktMucLucModel> SktMucLucModelItems
        {
            get => _sktMucLucModelItems;
            set => SetProperty(ref _sktMucLucModelItems, value);
        }

        private SktMucLucModel _selectedPopupItem;
        public SktMucLucModel SelectedPopupItem
        {
            get => _selectedPopupItem;
            set
            {
                SetProperty(ref _selectedPopupItem, value);
                NsSktChungTuChiTietSearchModel.SKyHieu = _selectedPopupItem?.SKyHieu;
                OnPropertyChanged(nameof(NsSktChungTuChiTietSearchModel.SKyHieu));
                IsPopupOpen = false;
            }
        }

        private bool _isReadOnlyStatus;
        public bool IsReadOnlyStatus
        {
            get => _isReadOnlyStatus;
            set => SetProperty(ref _isReadOnlyStatus, value);
        }

        public int NamLamViec { get; set; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseWindowCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ClearSearchCommand { get; }

        public ExpertiseDetailViewModel(ISktNganhThamDinhService chungTuService,
           ISktNganhThamDinhChiTietService chungTuChiTietService,
           IDanhMucService danhMucService,
           INsNguoiDungDonViService nsNguoiDungDonViService,
           IMapper mapper,
           ISktMucLucService sktMucLucService,
           ISessionService sessionService,
           INsDonViService nsDonViService,
           ILog logger)
        {
            _mapper = mapper;
            _chungTuService = chungTuService;
            _danhMucService = danhMucService;
            _sktMucLucService = sktMucLucService;
            _chungTuChiTietService = chungTuChiTietService;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nsNguoiDungDonViService = nsNguoiDungDonViService;
            _logger = logger;

            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();

            SaveCommand = new RelayCommand(obj => OnSaveData());
            SearchCommand = new RelayCommand(obj =>
            {
                OnRefeshFilter();
            });
            ClearSearchCommand = new RelayCommand(OnClearSearch);
            CloseWindowCommand = new RelayCommand(obj => OnCloseWindow());
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha && !Model.IsLocked)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        protected override void OnLockUnLock()
        {
            try
            {
                string msgConfirm = string.Format(Model.IsLocked ? Resources.MsgUnLock : Resources.MsgLock, Environment.NewLine, Environment.NewLine);
                string msgDone = Model.IsLocked ? Resources.MsgUnLockDone : Resources.MsgLockDone;
                MessageBoxResult dialogResult = MessageBoxHelper.Confirm(msgConfirm);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    _chungTuService.LockOrUnLock(Model.Id, !Model.IsLocked);
                    Model.IsLocked = !Model.IsLocked;
                    OnPropertyChanged(nameof(IsSaveData));
                    MessageBoxHelper.Info(msgDone);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            if (Model.IsLocked)
            {
                return;
            }
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                //_chungTuChiTietService.DeleteByVoucherId(Model.Id);
                List<ExpertiseModelDetailModel> dataDetailsDelete = Items.Where(x => x.IdChungTu != Guid.Empty && x.IdChungTu != null && x.IsFilter).ToList();
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        if (item.IdDb.HasValue)
                            _chungTuChiTietService.Delete(item.IdDb.Value);
                    }
                }
                LoadData();
                MessageBoxHelper.Info(Resources.MsgDeleteSuccess);
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnRefresh()
        {
            try
            {
                if (IsSaveData)
                {
                    MessageBoxResult dialogResult = MessageBoxHelper.ConfirmCancel(Resources.MsgConfirmEdit);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        OnSaveData();
                        LoadData();
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {
                        LoadData();
                    }
                }
                else
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnSaveData()
        {
            try
            {
                if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD)
                {
                    Items.Select(n => { n.TuChi = n.TuChiNganh; n.SuDungTonKho = n.SuDungTonKhoNganh; n.ChiDacThuNganhPhanCap = n.ChiDacThuNganhPhanCapNganh; return n; }).ToList();
                }
                List<ExpertiseModelDetailModel> dataDetailsAdd = Items.Where(x => x.IsModified && (x.IdChungTu == Guid.Empty || x.IdChungTu == null) && !x.IsDeleted).ToList();
                List<ExpertiseModelDetailModel> dataDetailsUpdate = Items.Where(x => x.IsModified && x.IdChungTu != Guid.Empty && x.IdChungTu != null && !x.IsDeleted).ToList();
                List<ExpertiseModelDetailModel> dataDetailsDelete = Items.Where(x => x.IsDeleted && x.IdChungTu != Guid.Empty && x.IdChungTu != null).ToList();

                // update tong chung tu
                UpdateDemandExpertiseTotal();

                // Thêm mới chứng từ chi tiết
                if (dataDetailsAdd.Count > 0)
                {
                    dataDetailsAdd = dataDetailsAdd.Select(x =>
                    {
                        x.IdChungTu = Model.Id;
                        x.ITrangThai = 1;
                        x.NamLamViec = Model.NamLamViec.Value;
                        x.NamNganSach = Model.NamNganSach.Value;
                        x.NguonNganSach = Model.NguonNganSach.Value;
                        x.IsModified = false;
                        x.DateCreated = DateTime.Now;
                        x.UserCreator = _sessionService.Current.Principal;
                        return x;
                    }).ToList();
                    List<NsSktNganhThamDinhChiTiet> listChungTuChiTiets = new List<NsSktNganhThamDinhChiTiet>();
                    listChungTuChiTiets = _mapper.Map<List<NsSktNganhThamDinhChiTiet>>(dataDetailsAdd);
                    listChungTuChiTiets.Select(n => { n.Id = Guid.Empty; return n; }).ToList();
                    _chungTuChiTietService.AddRange(listChungTuChiTiets);
                }

                // Cập nhật chứng từ chi tiết
                if (dataDetailsUpdate.Count > 0)
                {
                    foreach (var item in dataDetailsUpdate)
                    {
                        item.IsModified = false;
                        item.ITrangThai = 1;
                        if (item.IdDb.HasValue)
                        {
                            NsSktNganhThamDinhChiTiet chungTuChiTiet = _chungTuChiTietService.Find(item.IdDb.Value);
                            if (chungTuChiTiet != null)
                            {
                                _mapper.Map(item, chungTuChiTiet);
                                chungTuChiTiet.DNgaySua = DateTime.Now;
                                chungTuChiTiet.SNguoiSua = _sessionService.Current.Principal;
                                _chungTuChiTietService.Update(chungTuChiTiet);
                            }
                        }
                    }
                }

                // Delete
                if (dataDetailsDelete.Count > 0)
                {
                    foreach (var item in dataDetailsDelete)
                    {
                        if (item.IdDb.HasValue)
                            _chungTuChiTietService.Delete(item.IdDb.Value);
                    }
                }
                OnPropertyChanged(nameof(IsDeleteAll));
                MessageBoxHelper.Info(Resources.MsgSaveDone);
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void UpdateDemandExpertiseTotal()
        {
            if (Model != null)
            {
                var sktThamDinhUpdate = _chungTuService.FindByCondition(x => x.Id.Equals(Model.Id)).FirstOrDefault();
                if (sktThamDinhUpdate != null)
                {
                    sktThamDinhUpdate.FTongTuChiCtc = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.TuChi);
                    sktThamDinhUpdate.FTongTuChiNganh = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.TuChi);
                    sktThamDinhUpdate.FTongHienVatCtc = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.TuChi);
                    sktThamDinhUpdate.FTongHienVatNganh = Items.Where(item => !item.IsHangCha && !item.IsDeleted)
                        .Sum(item => item.TuChi);
                    _chungTuService.Update(sktThamDinhUpdate);
                }
            }
        }

        private Expression<Func<DonVi, bool>> CreatePredicate()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO || x.Loai == LoaiDonVi.ROOT) && x.BCoNSNganh);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            return predicate;
        }

        private List<DonVi> GetListDonVi()
        {
            var predicate = CreatePredicate();
            List<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        private List<DonVi> GetListDonViChild()
        {
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => (x.Loai == LoaiDonVi.NOI_BO) && x.BCoNSNganh);
            predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
            List<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate).ToList();
            return listDonVi;
        }

        public List<ThDChungTuChiTietQuery> GetValueTuChiPrev()
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);

            if (Model.ILoaiChungTu.HasValue && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key);
            }
            else
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSSD_Key);
            }

            NsSktNganhThamDinh chungTuDeNghi = _chungTuService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi == null)
            {
                return new List<ThDChungTuChiTietQuery>();
            }
            else
            {
                List<ThDChungTuChiTietQuery> data = new List<ThDChungTuChiTietQuery>();
                if (Model.ILoaiChungTu.HasValue && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                {
                    data = _chungTuChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                else
                {
                    data = _chungTuChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                     _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                return data.Where(n => n.TuChi != 0 || n.ChiDacThuNganhPhanCap != 0 || n.SuDungTonKho != 0).ToList();
            }
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

        private bool CheckQuanLiDonViCha()
        {
            List<NguoiDungDonVi> nsDungDonVis = GetListNguoiDungDonVi();
            DonVi donVi0 = _nsDonViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            if (nsDungDonVis != null && nsDungDonVis.Count() > 0 && donVi0 != null)
            {
                if (nsDungDonVis.Select(n => n.IIdMaDonVi).ToList().Contains(donVi0.IIDMaDonVi))
                {
                    return true;
                }
            }
            return false;
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);
                if (Model == null || Model.Id == Guid.Empty)
                    return;
                List<ThDChungTuChiTietQuery> data = new List<ThDChungTuChiTietQuery>();
                if (Model.ILoaiChungTu.HasValue && Model.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                {
                    data = _chungTuChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, Model.Id.ToString(),
                    _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                else
                {
                    data = _chungTuChiTietService.FindByCondition(_sessionService.Current.YearOfWork, Model.Id.ToString(),
                      _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }

                var listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
                if (listDanhMuc == null || listDanhMuc.Count == 0)
                {
                    Items = new ObservableCollection<ExpertiseModelDetailModel>();
                    CalculateTotal();
                    return;
                }

                if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD)
                {
                    var predicate = PredicateBuilder.True<DonVi>();
                    predicate = predicate.And(x => x.NamLamViec == _sessionService.Current.YearOfWork);
                    predicate = predicate.And(x => x.ITrangThai == NSEntityStatus.ACTIVED);
                    List<DonVi> listDonVi = _nsDonViService.FindByCondition(predicate).ToList();
                    DonVi donViModel = listDonVi.Where(n => n.IIDMaDonVi == Model.IdDonVi).FirstOrDefault();
                    if (donViModel != null && donViModel.Loai == LoaiDonVi.ROOT)
                    {
                        IsReadOnlyStatus = true;
                    }
                }
                bool isManageParentAgency = CheckQuanLiDonViCha();
                if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN)
                {
                    if (!isManageParentAgency)
                    {
                        List<DonVi> listDonVi = GetListDonViChild();
                        listDanhMuc = listDanhMuc.Where(n => n.SGiaTri.Split(",").Any(m => listDonVi.Select(n => n.IIDMaDonVi).Contains(m))).ToList();
                        data = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).Contains(n.Nganh) || (string.IsNullOrEmpty(n.Nganh) && n.IsHangCha)).ToList();
                    }
                    else
                    {
                        List<DonVi> listDonVi = GetListDonVi();
                        listDanhMuc = listDanhMuc.Where(n => n.SGiaTri.Split(",").Any(m => listDonVi.Select(n => n.IIDMaDonVi).Contains(m))).ToList();
                        data = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).Contains(n.Nganh) || (string.IsNullOrEmpty(n.Nganh) && n.IsHangCha)).ToList();
                    }
                }
                else if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD)
                {
                    if (!IsReadOnlyStatus)
                    {
                        listDanhMuc = listDanhMuc.Where(n => n.SGiaTri.Split(",").Contains(Model.IdDonVi)).ToList();
                        data = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).Contains(n.Nganh) || (string.IsNullOrEmpty(n.Nganh) && n.IsHangCha)).ToList();
                    }
                    else
                    {
                        List<DonVi> listDonVi = GetListDonVi();
                        listDanhMuc = listDanhMuc.Where(n => n.SGiaTri.Split(",").Any(m => listDonVi.Select(n => n.IIDMaDonVi).Contains(m))).ToList();
                        data = data.Where(n => listDanhMuc.Select(m => m.IIDMaDanhMuc).Contains(n.Nganh) || (string.IsNullOrEmpty(n.Nganh) && n.IsHangCha)).ToList();
                    }
                }
                List<string> listKyHieu = new List<string>();
                foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
                {
                    listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
                }
                data = data.Where(n => listKyHieu.Contains(n.KyHieu)).OrderBy(x => x.KyHieu).ThenBy(x => x.IdDonVi).ToList();
                Items = _mapper.Map<ObservableCollection<Model.ExpertiseModelDetailModel>>(data);
                if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD)
                {
                    List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev();
                    if (dataDeNghi != null && dataDeNghi.Count > 0)
                    {
                        foreach (ExpertiseModelDetailModel item in Items)
                        {
                            ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                            if (valueItem != null)
                            {
                                item.TuChiPrev = valueItem.TuChi;
                                item.SuDungTonKhoPrev = valueItem.SuDungTonKho;
                                item.ChiDacThuNganhPhanCapPrev = valueItem.ChiDacThuNganhPhanCap;
                            }
                        }
                    }
                }

                if (PhanLoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTNTD)
                {
                    Items.Select(n => { n.TuChiNganh = n.TuChi; n.SuDungTonKhoNganh = n.SuDungTonKho; n.ChiDacThuNganhPhanCapNganh = n.ChiDacThuNganhPhanCap; return n; }).ToList();
                }
                _dataDetailFilter = CollectionViewSource.GetDefaultView(Items);
                _dataDetailFilter.Filter = DataDetailFilter;
                if (Items != null && Items.Count > 0)
                {
                    SelectedItem = Items.FirstOrDefault();
                }
                foreach (ExpertiseModelDetailModel model in Items)
                {
                    if (!model.IsHangCha)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }
                CalculateData();
                LoadPopupData();
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsReadOnlyStatus));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnRefeshFilter();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CalculateData()
        {
            // Reset value parrent
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChiCTC = 0; x.TuChiNganh = 0; x.TuChi = 0; x.HuyDongCTC = 0; x.HuyDongNganh = 0; x.Tang = 0; x.Giam = 0; x.TuChiPrev = 0;
                    x.SuDungTonKho = 0; x.SuDungTonKhoNganh = 0; x.ChiDacThuNganhPhanCap = 0; x.ChiDacThuNganhPhanCapNganh = 0; x.SuDungTonKhoPrev = 0;
                    x.ChiDacThuNganhPhanCapPrev = 0;
                    return x;
                }).ToList();
            // Caculate value child
            foreach (var item in Items.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.TuChiCTC != 0 || x.TuChiPrev != 0 || x.TuChi != 0
            || x.TuChiNganh != 0 || x.HuyDongCTC != 0 || x.HuyDongNganh != 0
            || x.SuDungTonKho != 0 || x.SuDungTonKhoNganh != 0 || x.ChiDacThuNganhPhanCap != 0 || x.ChiDacThuNganhPhanCapNganh != 0
            || x.SuDungTonKhoPrev != 0 || x.ChiDacThuNganhPhanCapPrev != 0
            )))
            {
                CalculateParent(item, item);
            }
            // Caculate total
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            Model.TongTuChiCTC = 0;
            Model.TongTuChiPrev = 0;
            Model.TongTuChiNganh = 0;
            Model.TongTuChi = 0;

            Model.TongChiDacThuNganhPhanCapPrev = 0;
            Model.TongChiDacThuNganhPhanCap = 0;
            Model.TongChiDacThuNganhPhanCapNganh = 0;

            Model.TongSuDungTonKho = 0;
            Model.TongSuDungTonKhoNganh = 0;
            Model.TongSuDungTonKhoPrev = 0;

            Model.TongHuyDongCTC = 0;
            Model.TongHuyDongNganh = 0;
            Model.TongTang = 0;
            Model.TongGiam = 0;
            List<ExpertiseModelDetailModel> listChildren = Items.Where(x => !x.IsHangCha && !x.IsDeleted && x.IsFilter).ToList();
            foreach (ExpertiseModelDetailModel item in listChildren)
            {
                Model.TongTuChiCTC += item.TuChiCTC;
                Model.TongTuChiPrev += item.TuChiPrev;
                Model.TongTuChiNganh += item.TuChiNganh;
                Model.TongTuChi += item.TuChi;

                Model.TongSuDungTonKho += item.SuDungTonKho;
                Model.TongSuDungTonKhoNganh += item.SuDungTonKhoNganh;
                Model.TongSuDungTonKhoPrev += item.SuDungTonKhoPrev;

                Model.TongChiDacThuNganhPhanCap += item.ChiDacThuNganhPhanCap;
                Model.TongChiDacThuNganhPhanCapNganh += item.ChiDacThuNganhPhanCapNganh;
                Model.TongChiDacThuNganhPhanCapPrev += item.ChiDacThuNganhPhanCapPrev;

                Model.TongHuyDongCTC += item.HuyDongCTC;
                Model.TongHuyDongNganh += item.HuyDongNganh;
                Model.TongTang += item.Tang;
                Model.TongGiam += item.Giam;
            }
        }

        private void CalculateParent(ExpertiseModelDetailModel currentItem, ExpertiseModelDetailModel selfItem)
        {
            var parentItem = Items.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChiCTC += selfItem.TuChiCTC;
            parentItem.TuChiPrev += selfItem.TuChiPrev;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.TuChiNganh += selfItem.TuChiNganh;
            parentItem.HuyDongCTC += selfItem.HuyDongCTC;
            parentItem.HuyDongNganh += selfItem.HuyDongNganh;
            parentItem.Tang += selfItem.Tang;
            parentItem.Giam += selfItem.Giam;
            parentItem.SuDungTonKho += selfItem.SuDungTonKho;
            parentItem.SuDungTonKhoPrev += selfItem.SuDungTonKhoPrev;
            parentItem.SuDungTonKhoNganh += selfItem.SuDungTonKhoNganh;
            parentItem.ChiDacThuNganhPhanCap += selfItem.ChiDacThuNganhPhanCap;
            parentItem.ChiDacThuNganhPhanCapNganh += selfItem.ChiDacThuNganhPhanCapNganh;
            parentItem.ChiDacThuNganhPhanCapPrev += selfItem.ChiDacThuNganhPhanCapPrev;
            CalculateParent(parentItem, selfItem);
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(ExpertiseModelDetailModel.TuChiCTC) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.TuChiNganh) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.TuChiPrev) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.TuChi) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKho) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKhoNganh) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKhoPrev) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCap) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCapNganh) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCapPrev) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.HuyDongCTC) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.GhiChu) ||
                args.PropertyName == nameof(ExpertiseModelDetailModel.HuyDongNganh)
                )
            {
                ExpertiseModelDetailModel item = Items.Where(x => x.Id == ((ExpertiseModelDetailModel)sender).Id).First();
                item.IsModified = true;
                if (args.PropertyName == nameof(ExpertiseModelDetailModel.TuChiCTC) || args.PropertyName == nameof(ExpertiseModelDetailModel.TuChiNganh)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.TuChi)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.HuyDongCTC) || args.PropertyName == nameof(ExpertiseModelDetailModel.HuyDongNganh)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKho) || args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKhoNganh)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.SuDungTonKhoPrev)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCapPrev)
                    || args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCap) || args.PropertyName == nameof(ExpertiseModelDetailModel.ChiDacThuNganhPhanCapNganh)
                    )
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new ObservableCollection<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Có dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            SelectedTypeDisplays = TypeDisplay.TAT_CA;
        }

        private void OnRefeshFilter()
        {
            _dataDetailFilter.Refresh();
            CalculateData();
        }

        private bool PopupFilter(object obj)
        {
            var temp = (SktMucLucModel)obj;
            var condition = true;
            if (!string.IsNullOrEmpty(PopupSearchText))
                condition = condition && (temp.SSTT.ToLower().Contains(PopupSearchText.Trim().ToLower())
                                          || temp.SMoTa.ToLower().Contains(PopupSearchText.Trim().ToLower())
                                          || temp.SKyHieu.ToLower().Contains(PopupSearchText.Trim().ToLower()));
            return condition;
        }

        private void LoadPopupData()
        {
            var predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
            predicate = predicate.And(x => x.IIDMLSKTCha == Guid.Empty || x.IIDMLSKTCha == null);
            var idMucLucParent = _sktMucLucService.FindByCondition(predicate).Select(x => x.IIDMLSKT).ToList().Cast<Guid?>();
            predicate = PredicateBuilder.True<NsSktMucLuc>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork && x.ITrangThai == 1);
            predicate = predicate.And(x => x.IIDMLSKTCha == Guid.Empty || idMucLucParent.Contains(x.IIDMLSKTCha));
            var temp = _sktMucLucService.FindByCondition(predicate).OrderBy(x => x.SKyHieu);
            SktMucLucModelItems = _mapper.Map<ObservableCollection<SktMucLucModel>>(temp);
            _searchPopupView = CollectionViewSource.GetDefaultView(SktMucLucModelItems);
            _searchPopupView.Filter = PopupFilter;
        }

        private void OnClearSearch(object obj)
        {
            NsSktChungTuChiTietSearchModel = new NsSktChungTuChiTietModel();
            _selectedTypeDisplays = TypeDisplay.TAT_CA;
            OnPropertyChanged(nameof(SelectedTypeDisplays));
            _dataDetailFilter.Refresh();
        }

        private bool DataDetailFilter(object obj)
        {
            bool result = true;
            var item = (ExpertiseModelDetailModel)obj;
            if (!string.IsNullOrEmpty(SelectedTypeDisplays))
            {
                if (SelectedTypeDisplays == TypeDisplay.CO_DU_LIEU)
                    result = result && (item.TuChiCTC != 0 || item.TuChiPrev != 0 || item.TuChi != 0 || item.TuChiNganh != 0 || item.HuyDongCTC != 0 || item.HuyDongNganh != 0
                                        || item.SuDungTonKho != 0 || item.SuDungTonKhoPrev != 0 || item.SuDungTonKhoNganh != 0
                                        || item.ChiDacThuNganhPhanCap != 0 || item.ChiDacThuNganhPhanCapNganh != 0 || item.ChiDacThuNganhPhanCapPrev != 0
                        || (item.IsModified && (item.IdChungTu == Guid.Empty || item.IdChungTu == null) && !item.IsDeleted));
            }
            if (!string.IsNullOrEmpty(NsSktChungTuChiTietSearchModel.SKyHieu))
                result = result && item.KyHieu.ToLower().Contains(NsSktChungTuChiTietSearchModel.SKyHieu.Trim().ToLower());
            item.IsFilter = result;
            return result;
        }

        public override void Init()
        {
            try
            {
                IsReadOnlyStatus = false;
                MarginRequirement = new System.Windows.Thickness(10);
                NamLamViec = _sessionService.Current.YearOfWork;
                LoadTypeDisplay();
                LoadData();
                OnPropertyChanged(nameof(VisibleNganhThamDinh));
                OnPropertyChanged(nameof(VisibleCTCDeNghi));
                OnPropertyChanged(nameof(IsReadOnlyStatus));
                OnPropertyChanged(nameof(TitleColumn));
                OnPropertyChanged(nameof(VisibilityDonVi));
                OnPropertyChanged(nameof(VisibleNganhThamDinhNSDacThu));
                OnPropertyChanged(nameof(VisibleCTCDeNghiNSDacThu));
                OnPropertyChanged(nameof(VisibleNganhThamDinhNSSuDung));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnableButtonDelete));
        }

        private void OnCloseWindow()
        {
            DataChangedEventHandler handler = ClosePopup;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
