using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Hospital;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Hospital
{
    public class HospitalDetailViewModel : DetailViewModelBase<DtChungTuModel, DtChungTuChiTietModel>
    {
        private readonly INsDtChungTuChiTietService _dtChungTuChiTietService;
        private readonly INsDtChungTuService _dtChungTuService;
        private readonly INsDonViService _donViService;
        private readonly ISessionService _sessionService;
        private readonly ILogger<HospitalDetailViewModel> _logger;
        private readonly INsMucLucNganSachService _mlnsService;
        private readonly IMapper _mapper;
        private ICollectionView _budgetCatalogItemsView;
        private ICollectionView _chungTuChiTietItemsView;
        private EstimationVoucherDetailCriteria _searchCondition;
        private List<DtChungTuChiTietModel> _listCompare;
        private List<NsDtChungTuChiTietQuery> _listChungTuChiTiet;
        private bool _isCompareData;
        private List<NsMucLucNganSach> _listMLNS;
        private SessionInfo _sessionInfo;
        private List<DanhMuc> _listDanhMucNganh;
        private ICollection<DtChungTuChiTietModel> _filterResult = new HashSet<DtChungTuChiTietModel>();
        private string xnmConcatenation = "";

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler UpdateVoucherEvent;
        public override Type ContentType => typeof(HospitalDetail);
        public bool IsSaveData => Items.Any(item => item.IsModified || item.IsDeleted);
        public bool IsDeleteAll => Items.Any(item => !item.IsModified && item.HasData);
        public bool IsEditByRole => Model.SNguoiTao == _sessionInfo.Principal;

        public int NamLamViec { get; set; }
        private DivisionColumnVisibility _columnVisibility;
        public DivisionColumnVisibility ColumnVisibility
        {
            get => _columnVisibility;
            set => SetProperty(ref _columnVisibility, value);
        }

        private List<ComboboxItem> _typeDisplays;
        public List<ComboboxItem> TypeDisplays
        {
            get => _typeDisplays;
            set => SetProperty(ref _typeDisplays, value);
        }

        private string _typeDisplaysselected;
        public string TypeDisplaysSelected
        {
            get => _typeDisplaysselected;
            set
            {
                if (SetProperty(ref _typeDisplaysselected, value) && _chungTuChiTietItemsView != null)
                {
                    BeForeRefresh();
                    _chungTuChiTietItemsView.Refresh();
                    CalculateData();
                }
            }
        }

        private string _searchLNS;
        public string SearchLNS
        {
            get => _searchLNS;
            set
            {
                if (SetProperty(ref _searchLNS, value))
                {
                    _budgetCatalogItemsView.Refresh();
                    CalculateData();
                }
            }
        }

        private bool _isOpenLnsPopup;
        public bool IsOpenLnsPopup
        {
            get => _isOpenLnsPopup;
            set
            {
                SetProperty(ref _isOpenLnsPopup, value);
            }
        }

        private string _selectedLNS;
        public string SelectedLNS
        {
            get => _selectedLNS;
            set => SetProperty(ref _selectedLNS, value);
        }

        private EstimationDetailCriteria _detailFilter;
        public EstimationDetailCriteria DetailFilter
        {
            get => _detailFilter;
            set => SetProperty(ref _detailFilter, value);
        }

        private ObservableCollection<DtChungTuChiTietModel> _budgetCatalogItems;
        public ObservableCollection<DtChungTuChiTietModel> BudgetCatalogItems
        {
            get => _budgetCatalogItems;
            set => SetProperty(ref _budgetCatalogItems, value);
        }

        private DtChungTuChiTietModel _selectedBudgetCatalog;
        public DtChungTuChiTietModel SelectedBudgetCatalog
        {
            get => _selectedBudgetCatalog;
            set
            {
                if (SetProperty(ref _selectedBudgetCatalog, value))
                {
                    if (_selectedBudgetCatalog != null)
                        SelectedLNS = _selectedBudgetCatalog.SLns;
                    BeForeRefresh();
                    _chungTuChiTietItemsView.Refresh();
                }
                CalculateTotal();
                IsOpenLnsPopup = false;
            }
        }

        private DivisionTotalModel _divisionTotal;
        public DivisionTotalModel DivisionTotal
        {
            get => _divisionTotal;
            set => SetProperty(ref _divisionTotal, value);
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand CloseCommand { get; }

        public HospitalDetailViewModel(
            IMapper mapper,
            ILogger<HospitalDetailViewModel> logger,
            ISessionService sessionService,
            INsDtChungTuChiTietService dtChungTuChiTietService,
            INsDtChungTuService dtChungTuService,
            INsDonViService donViService,
            INsMucLucNganSachService mlnsService,
            IDanhMucService danhMucService) : base(danhMucService, sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _dtChungTuChiTietService = dtChungTuChiTietService;
            _dtChungTuService = dtChungTuService;
            _donViService = donViService;
            _mlnsService = mlnsService;

            SearchCommand = new RelayCommand(obj =>
            {
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
                CalculateTotal();
            });
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            NamLamViec = _sessionService.Current.YearOfWork;
            ResetConditionSearch();
            LoadControlVisibility();
            LoadTypeDisplay();
            LoadData();
            if (!IsEditByRole)
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
        }

        private void ResetConditionSearch()
        {
            _searchLNS = string.Empty;
            _selectedLNS = string.Empty;
            xnmConcatenation = string.Empty;
            _filterResult = new HashSet<DtChungTuChiTietModel>();
            _listDanhMucNganh = new List<DanhMuc>();
            Items = new ObservableCollection<DtChungTuChiTietModel>();
            DetailFilter = new EstimationDetailCriteria();
            _searchCondition = new EstimationVoucherDetailCriteria
            {
                VoucherId = Model.Id,
                LNS = Model.SDslns,
                YearOfWork = Model.INamLamViec,
                YearOfBudget = Model.INamNganSach,
                BudgetSource = Model.IIdMaNguonNganSach,
                VoucherDate = Model.DNgayChungTu,
                UserName = _sessionService.Current.Principal
            };
            OnPropertyChanged(nameof(SearchLNS));
            OnPropertyChanged(nameof(SelectedLNS));
        }

        private void LoadControlVisibility()
        {
            _listMLNS = _mlnsService.FindByListLnsDonVi(Model.SDslns, _sessionService.Current.YearOfWork).ToList();
            _columnVisibility = new DivisionColumnVisibility();
            _columnVisibility.IsDisplayTuChi = _listMLNS.Any(x => x.BTuChi);
            _columnVisibility.IsDisplayHienVat = _listMLNS.Any(x => x.BHienVat);
            _columnVisibility.IsDisplayDuPhong = _listMLNS.Any(x => x.BDuPhong);
            _columnVisibility.IsDisplayHangMua = _listMLNS.Any(x => x.BHangMua);
            _columnVisibility.IsDisplayHangNhap = _listMLNS.Any(x => x.BHangNhap);
            _columnVisibility.IsDisplayPhanCap = _listMLNS.Any(x => x.BPhanCap);
            OnPropertyChanged(nameof(ColumnVisibility));
        }

        private void LoadTypeDisplay()
        {
            TypeDisplays = new List<ComboboxItem>();
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.CO_DU_LIEU, DisplayItem = "Đã nhập dữ liệu" });
            TypeDisplays.Add(new ComboboxItem { ValueItem = TypeDisplay.TAT_CA, DisplayItem = "Hiển thị tất cả" });
            TypeDisplaysSelected = TypeDisplay.TAT_CA;
        }

        private void LoadLNSIndexCondition()
        {
            List<DtChungTuChiTietModel> listLNS = Items.Where(x => string.IsNullOrEmpty(x.SL) &&
                string.IsNullOrEmpty(x.SK) &&
                string.IsNullOrEmpty(x.SM) &&
                string.IsNullOrEmpty(x.STm) &&
                string.IsNullOrEmpty(x.STtm) &&
                string.IsNullOrEmpty(x.SNg) &&
                string.IsNullOrEmpty(x.STng)).ToList();
            listLNS.Insert(0, new DtChungTuChiTietModel
            {
                SLns = string.Empty,
                SMoTa = "-- TẤT CẢ --"
            });
            BudgetCatalogItems = new ObservableCollection<DtChungTuChiTietModel>(listLNS);
            _budgetCatalogItemsView = CollectionViewSource.GetDefaultView(BudgetCatalogItems);
            _budgetCatalogItemsView.Filter = BudgetCatalogItemsFilter;
        }

        private bool BudgetCatalogItemsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLNS))
                return true;
            return obj is DtChungTuChiTietModel item && (item.SLns.StartsWith(_searchLNS, StringComparison.Ordinal) || item.SMoTa.StartsWith(_searchLNS, StringComparison.Ordinal));
        }

        public override void LoadData(params object[] args)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                _isCompareData = false;
                _searchCondition.IDuLieuNhan = null;
                _listChungTuChiTiet = _dtChungTuChiTietService.FindByCondition(_searchCondition).ToList();
                if (_listDanhMucNganh != null && _listDanhMucNganh.Count > 0)
                {
                    var listXauNoiMa = StringUtils.GetListXauNoiMaParent(_listChungTuChiTiet.Where(x => !x.BHangCha && _listDanhMucNganh.Select(x => x.IIDMaDanhMuc).Contains(x.SNg)).Select(x => x.SXauNoiMa).ToList());
                    _listChungTuChiTiet = _listChungTuChiTiet.Where(x => listXauNoiMa.Contains(x.SXauNoiMa)).ToList();
                }
            }, (s, e) =>
            {
                Items = _mapper.Map<ObservableCollection<DtChungTuChiTietModel>>(_listChungTuChiTiet);
                // Using collection view
                _chungTuChiTietItemsView = CollectionViewSource.GetDefaultView(Items);
                _chungTuChiTietItemsView.Filter = ItemsViewFilter;

                SettingEditable();
                CalculateData();

                if (Items != null)
                {
                    SelectedItem = Items.Where(x => !x.IsHangCha).FirstOrDefault();
                }
                foreach (var model in Items)
                {
                    if (model.IsEditable)
                    {
                        model.PropertyChanged += DetailModel_PropertyChanged;
                    }
                }

                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
                OnPropertyChanged(nameof(Items));
                LoadLNSIndexCondition();
                IsLoading = false;
            });
        }

        private void SettingEditable()
        {
            foreach (var item in Items.Where(x => !x.IsHangCha))
            {
                var mlns = _listMLNS.Where(x => x.Lns == item.SLns).FirstOrDefault();
                if (mlns != null)
                {
                    item.IsEditTuChi = mlns.BTuChi;
                    item.IsEditHienVat = mlns.BHienVat;
                    item.IsEditHangNhap = mlns.BHangNhap;
                    item.IsEditHangMua = mlns.BHangMua;
                    item.IsEditDuPhong = mlns.BDuPhong;
                    item.IsEditPhanCap = mlns.BPhanCap;
                }
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                || args.PropertyName == nameof(DtChungTuChiTietModel.SGhiChu)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong))
            {
                DtChungTuChiTietModel item = (DtChungTuChiTietModel)sender;
                item.IsModified = true;
                if (args.PropertyName == nameof(DtChungTuChiTietModel.FTuChi)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHienVat)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangNhap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FHangMua)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FPhanCap)
                    || args.PropertyName == nameof(DtChungTuChiTietModel.FDuPhong))
                {
                    CalculateData();
                }
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void CalculateData()
        {
            Items.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHienVat = 0;
                    x.FHangNhap = 0;
                    x.FHangMua = 0;
                    x.FPhanCap = 0;
                    x.FDuPhong = 0;
                    return x;
                }).ToList();

            foreach (var item in Items.Where(x => x.IsEditable && x.IsFilter && (x.FTuChi != 0 || x.FHienVat != 0 || x.FHangNhap != 0 || x.FHangMua != 0 || x.FDuPhong != 0 || x.FPhanCap != 0)))
            {
                CalculateParent(item, item);
            }
            CalculateTotal();
        }

        private void CalculateParent(DtChungTuChiTietModel currentItem, DtChungTuChiTietModel seftItem)
        {
            var parrentItem = Items.Where(x => x.IIdMlns == currentItem.IIdMlnsCha).FirstOrDefault();
            if (parrentItem == null) return;
            parrentItem.FTuChi += seftItem.FTuChi;
            parrentItem.FHienVat += seftItem.FHienVat;
            parrentItem.FHangNhap += seftItem.FHangNhap;
            parrentItem.FHangMua += seftItem.FHangMua;
            parrentItem.FPhanCap += seftItem.FPhanCap;
            parrentItem.FDuPhong += seftItem.FDuPhong;
            CalculateParent(parrentItem, seftItem);
        }

        private void CalculateTotal()
        {
            DivisionTotal = new DivisionTotalModel();
            var listChildren = Items.Where(x => x.IsEditable && x.IsFilter).ToList();
            foreach (var item in listChildren)
            {
                DivisionTotal.FTongTuChi += item.FTuChi;
                DivisionTotal.FTongHienVat += item.FHienVat;
                DivisionTotal.FTongHangNhap += item.FHangNhap;
                DivisionTotal.FTongHangMua += item.FHangMua;
                DivisionTotal.FTongPhanCap += item.FPhanCap;
                DivisionTotal.FTongDuPhong += item.FDuPhong;
            }
        }

        private bool ItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            result = ChungTuChiTietItemsViewFilter(item);
            if (!result && item.IsHangCha)
            {
                if (string.IsNullOrEmpty(item.SL))
                    result = xnmConcatenation.StartsWith(item.SLns);
                else result = xnmConcatenation.Contains(item.SXauNoiMa);
            }
            if (result)
                item.IsFilter = result;
            return result;
        }

        private bool ChungTuChiTietItemsViewFilter(object obj)
        {
            bool result = true;
            var item = (DtChungTuChiTietModel)obj;
            if (!string.IsNullOrEmpty(SelectedLNS))
                result = result && item.SLns.ToLower().StartsWith(SelectedLNS.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.L))
                result = result && item.SL.ToLower().StartsWith(DetailFilter.L.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.K))
                result = result && item.SK.ToLower().StartsWith(DetailFilter.K.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.M))
                result = result && item.SM.ToLower().StartsWith(DetailFilter.M.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TM))
                result = result && item.STm.ToLower().StartsWith(DetailFilter.TM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TTM))
                result = result && item.STtm.ToLower().StartsWith(DetailFilter.TTM.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.NG))
                result = result && item.SNg.ToLower().StartsWith(DetailFilter.NG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG))
                result = result && item.STng.ToLower().StartsWith(DetailFilter.TNG.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG1))
                result = result && item.STng1.ToLower().StartsWith(DetailFilter.TNG1.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG2))
                result = result && item.STng2.ToLower().StartsWith(DetailFilter.TNG2.ToLower());
            if (!string.IsNullOrEmpty(DetailFilter.TNG3))
                result = result && item.STng3.ToLower().StartsWith(DetailFilter.TNG3.ToLower());

            if (!string.IsNullOrEmpty(TypeDisplaysSelected))
            {
                bool hasInputData = item.FTuChi != 0 || item.FHienVat != 0 || item.FDuPhong != 0 || item.FHangNhap != 0 || item.FHangMua != 0 || item.FPhanCap != 0;
                if (TypeDisplaysSelected == TypeDisplay.DA_NHAP)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
                else if (TypeDisplaysSelected == TypeDisplay.CO_DU_LIEU)
                    result = result && (hasInputData || (item.IsModified && (item.IIdDtchungTu == Guid.Empty || item.IIdDtchungTu == null) && !item.IsDeleted));
            }

            if (_isCompareData)
            {
                result = result && _listCompare.Any(x => x.SXauNoiMa == item.SXauNoiMa);
            }

            item.IsFilter = result;
            return result;
        }

        private void BeForeRefresh()
        {
            _filterResult = Items.Where(item => ChungTuChiTietItemsViewFilter(item)).Where(item => !item.IsHangCha).ToList();
            xnmConcatenation = string.Join(";", _filterResult.Select(i => i.SXauNoiMa).ToHashSet());
        }

        protected override void OnDelete()
        {
            if (Items != null && Items.Count > 0 && SelectedItem != null && !SelectedItem.IsHangCha)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
                CalculateData();
                OnPropertyChanged(nameof(IsSaveData));
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        protected override void OnLockUnLock()
        {
            if (!IsEditByRole)
            {
                MessageBoxHelper.Info(string.Format(Resources.AlertRoleEditDetail, Model.SNguoiTao));
                return;
            }
            if (Model.BKhoa)
            {
                //chỉ có đơn vị cha mới được mở khóa chứng từ
                List<DonVi> userAgency = _donViService.FindByUser(_sessionInfo.Principal, _sessionInfo.YearOfWork, LoaiDonVi.ROOT);
                if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                {
                    MessageBoxHelper.Warning(Resources.MsgRoleUnlock);
                    return;
                }
            }
            else
            {
                //chỉ có người tạo chứng từ mới được khóa chứng từ
                if (Model.SNguoiTao != _sessionInfo.Principal)
                {
                    MessageBoxHelper.Warning(string.Format(Resources.MsgRoleLock, SelectedItem.SNguoiTao));
                    return;
                }
            }

            var msgConfirm = Model.BKhoa ? Resources.LockChungTu : Resources.UnlockChungTu;
            var msgDone = Model.BKhoa ? Resources.MsgUnLockDone : Resources.MsgLockDone;
            var result = MessageBoxHelper.Confirm(msgConfirm);

            if (result == MessageBoxResult.Yes)
            {
                var rs = _dtChungTuService.LockOrUnLock(Model.Id, !Model.BKhoa);
                if (rs == DBContextSaveChangeState.SUCCESS)
                {
                    Model.BKhoa = !Model.BKhoa;
                    OnPropertyChanged(nameof(IsSaveData));
                    OnPropertyChanged(nameof(IsDeleteAll));
                }
                MessageBox.Show(msgDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public override void OnSave()
        {
            var session = _sessionService.Current;
            List<DtChungTuChiTietModel> listChungTuChiTietAdd = Items.Where(x => x.IsEditable && x.IsModified && x.IIdDtchungTu == null).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietUpdate = Items.Where(x => x.IsEditable && x.IsModified && x.IIdDtchungTu != null).ToList();
            List<DtChungTuChiTietModel> listChungTuChiTietDelete = Items.Where(x => x.IsDeleted).ToList();

            // Thêm mới chứng từ chi tiết
            if (listChungTuChiTietAdd.Count > 0)
            {
                listChungTuChiTietAdd = listChungTuChiTietAdd.Select(x =>
                {
                    x.Id = Guid.NewGuid();
                    x.IIdDtchungTu = Model.Id;
                    x.IIdMaDonVi = Model.SDsidMaDonVi;
                    x.INamNganSach = Model.INamNganSach;
                    x.INamLamViec = Model.INamLamViec;
                    x.IIdMaNguonNganSach = Model.IIdMaNguonNganSach;
                    x.IPhanCap = NSDuToan.IPHANCAP_NHAN_PHANBO;
                    x.SNguoiTao = session.Principal;
                    x.DNgayTao = DateTime.Now;
                    return x;
                }).ToList();

                List<NsDtChungTuChiTiet> listChungTuChiTiets = _mapper.Map<List<NsDtChungTuChiTiet>>(listChungTuChiTietAdd);
                _dtChungTuChiTietService.AddRange(listChungTuChiTiets);
            }

            // Cập nhật chứng từ chi tiết
            if (listChungTuChiTietUpdate.Count > 0)
            {
                foreach (var item in listChungTuChiTietUpdate)
                {
                    NsDtChungTuChiTiet chungTuChiTiet = _dtChungTuChiTietService.FindById(item.Id);
                    _mapper.Map(item, chungTuChiTiet);
                    _dtChungTuChiTietService.Update(chungTuChiTiet);
                }
            }

            // Xóa chứng từ chi tiết
            if (listChungTuChiTietDelete.Count > 0)
            {
                foreach (var item in listChungTuChiTietDelete)
                {
                    _dtChungTuChiTietService.Delete(item.Id);

                    ResetVoucherDetailData(item);

                    // Reset flag
                    item.IsModified = false;
                    item.IsDeleted = false;
                    item.IIdDtchungTu = null;
                }
            }
            // Refresh state form
            Items.Select(x => { x.IsModified = false; x.IsDeleted = false; return x; }).ToList();

            //Cập nhật thông tin chứng từ
            UpdateChungTu();

            OnPropertyChanged(nameof(IsSaveData));
            OnPropertyChanged(nameof(IsDeleteAll));
            MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            //refresh dữ liệu ở màn index
            DataChangedEventHandler handler = UpdateVoucherEvent;

            if (handler != null)
                handler(Model, new EventArgs());
        }

        private DtChungTuChiTietModel ResetVoucherDetailData(DtChungTuChiTietModel voucherDetail)
        {
            voucherDetail.FTuChi = 0;
            voucherDetail.FHienVat = 0;
            voucherDetail.FHangNhap = 0;
            voucherDetail.FHangMua = 0;
            voucherDetail.FPhanCap = 0;
            voucherDetail.FDuPhong = 0;
            voucherDetail.FTonKho = 0;
            voucherDetail.SGhiChu = string.Empty;
            return voucherDetail;
        }

        private void UpdateChungTu()
        {
            NsDtChungTu chungTu = _dtChungTuService.FindById(Model.Id);
            var itemsHasData = Items.Where(x => x.HasData).ToList();
            chungTu.FTongTuChi = itemsHasData.Sum(x => x.FTuChi);
            chungTu.FTongHienVat = itemsHasData.Sum(x => x.FHienVat);
            chungTu.FTongHangNhap = itemsHasData.Sum(x => x.FHangNhap);
            chungTu.FTongHangMua = itemsHasData.Sum(x => x.FHangMua);
            chungTu.FTongPhanCap = itemsHasData.Sum(x => x.FPhanCap);
            chungTu.FTongDuPhong = itemsHasData.Sum(x => x.FDuPhong);
            _dtChungTuService.Update(chungTu);

            Model.FTongTuChi = chungTu.FTongTuChi;
            Model.FTongHienVat = chungTu.FTongHienVat;
            Model.FTongHangNhap = chungTu.FTongHangNhap;
            Model.FTongHangMua = chungTu.FTongHangMua;
            Model.FTongPhanCap = chungTu.FTongPhanCap;
            Model.FTongDuPhong = chungTu.FTongDuPhong;
        }

        private void OnResetFilter()
        {
            DetailFilter = new EstimationDetailCriteria();
            SelectedLNS = string.Empty;
            if (_chungTuChiTietItemsView != null)
            {
                BeForeRefresh();
                _chungTuChiTietItemsView.Refresh();
                CalculateData();
            }
        }

        protected override void OnRefresh()
        {
            if (IsSaveData)
            {
                var result = MessageBoxHelper.ConfirmCancel(Resources.ConfirmReloadData);
                if (result == MessageBoxResult.Cancel)
                    return;
                else if (result == MessageBoxResult.Yes)
                    OnSave();
                else LoadData();
            }
            else LoadData();
        }

        protected override void OnDeleteAll()
        {
            base.OnDeleteAll();
            var result = MessageBoxHelper.Confirm(Resources.DeleteAllChungTuChiTiet);
            if (result == MessageBoxResult.No)
                return;
            else if (result == MessageBoxResult.Yes)
            {
                if (Items != null)
                {
                    Items.Where(x => x.IsFilter && !x.IsHangCha && x.HasData).ForAll(x => x.IsDeleted = true);
                    OnSave();
                }
                OnPropertyChanged(nameof(IsDeleteAll));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }
    }
}
