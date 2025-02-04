using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.ContractInfo;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo
{
    public class ContractInfoIndexViewModel : GridViewModelBase<HopDongModel>
    {
        private IMapper _mapper;
        private ICollectionView _dataIndexFilter;
        private IVdtDaTtHopDongService _vdtDaTtHopDongService;
        private ISessionService _sessionService;
        private IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IExportService _exportService;
        private readonly ILog _logger;
        private readonly INsDonViService _nsDonViService;
        private readonly IProjectManagerService _duanService;
        private readonly IVdtDmNhaThauService _nhathauService;
        Dictionary<Guid, VdtDmNhaThau> _dicNhaThau;
        private Dictionary<Guid, string> _dicDuAn;
        private Dictionary<Guid, VdtDmLoaiHopDong> _dicLoaiHopDong;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_CONTRACT_INFO_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        public override string Title => "Thông tin hợp đồng";
        public override string Name => "Thông tin hợp đồng";
        public override string Description => "Danh sách thông tin hợp đồng";
        public override Type ContentType => typeof(View.Investment.MediumTermPlan.ContractInfo.ContractInfoIndex);
        public bool IsEdit => SelectedItem != null && !SelectedItem.BKhoa;
        public bool IsLock => SelectedItem != null && SelectedItem.BKhoa;

        private string _sTenHopDongSearch;
        public string STenHopDongSearch 
        {
            get => _sTenHopDongSearch;
            set => SetProperty(ref _sTenHopDongSearch, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _soHopDongSearch;
        public string SoHopDongSearch
        {
            get => _soHopDongSearch;
            set => SetProperty(ref _soHopDongSearch, value);
        }

        private string _duAnSearch;
        public string DuAnSearch
        {
            get => _duAnSearch;
            set => SetProperty(ref _duAnSearch, value);
        }

        private DateTime? _ngayLapTuSearch;
        public DateTime? NgayLapTuSearch
        {
            get => _ngayLapTuSearch;
            set => SetProperty(ref _ngayLapTuSearch, value);
        }

        private DateTime? _ngayLapDenSearch;
        public DateTime? NgayLapDenSearch
        {
            get => _ngayLapDenSearch;
            set => SetProperty(ref _ngayLapDenSearch, value);
        }

        private double? _giaTriHopDongTuSearch;
        public double? GiaTriHopDongTuSearch
        {
            get => _giaTriHopDongTuSearch;
            set => SetProperty(ref _giaTriHopDongTuSearch, value);
        }

        private double? _giaTriHopDongDenSearch;
        public double? GiaTriHopDongDenSearch
        {
            get => _giaTriHopDongDenSearch;
            set => SetProperty(ref _giaTriHopDongDenSearch, value);
        }

        private ObservableCollection<HopDongModel> _displayItems;
        public ObservableCollection<HopDongModel> DisplayItems
        {
            get => _displayItems;
            set => SetProperty(ref _displayItems, value);
        }

        public ObservableCollection<DonViModel> DonViModels { get; set; }

        public ContractInfoDialogViewModel ContractInfoDialogViewModel { get; }
        public HopDongDieuChinhDialogViewModel HopDongDieuChinhDialogViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public ContractInfoImportViewModel ContractInfoImportViewModel { get; set; }
        public ContractInfoImport ContractInfoImport { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand RefeshCommand { get; }
        public RelayCommand AdjustedCommand { get; }
        public RelayCommand ShowRootCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand ShowPopupDieuChinhCommand { get; }

        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportCommand { get; }

        public ContractInfoIndexViewModel(
            IVdtDaTtHopDongService vdtDaTtHopDongService,
            ISessionService sessionService,
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IMapper mapper,
            ILog logger,
            INsDonViService nsDonViService,
            IExportService exportService,
            IVdtDmNhaThauService nhathauService,
            IProjectManagerService duanService,
            ContractInfoDialogViewModel contractInfoDialogViewModel,
            HopDongDieuChinhDialogViewModel hopDongDieuChinhDialogViewModel,
            AttachmentViewModel attachmentViewModel,
            ContractInfoImportViewModel contractInfoImportViewModel)
        {
            _vdtDaTtHopDongService = vdtDaTtHopDongService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _deNghiThanhToanService = deNghiThanhToanService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _nhathauService = nhathauService;
            _duanService = duanService;

            ContractInfoDialogViewModel = contractInfoDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
            HopDongDieuChinhDialogViewModel = hopDongDieuChinhDialogViewModel;
            ContractInfoImportViewModel = contractInfoImportViewModel;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => OnResetFilter());
            RefeshCommand = new RelayCommand(obj => LoadData());
            AdjustedCommand = new RelayCommand(obj => OnAdjusted());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);
            ShowPopupDieuChinhCommand = new RelayCommand(o => OnShowPopupDieuChinh());
            ShowRootCommand = new RelayCommand(o => OnViewHopDongGoc());
            ExportCommand = new RelayCommand(o => OnExport());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        private void OnSearch()
        {
            _dataIndexFilter.Refresh();
            OnPropertyChanged(nameof(Items));
        }

        private void OnResetFilter()
        {
            TenDonVi = string.Empty;
            STenHopDongSearch = string.Empty;
            SoHopDongSearch = string.Empty;
            DuAnSearch = string.Empty;
            NgayLapTuSearch = null;
            NgayLapDenSearch = null;
            GiaTriHopDongTuSearch = null;
            GiaTriHopDongDenSearch = null;
            LoadData();
        }

        public override void Init()
        {
            TenDonVi = string.Empty;
            STenHopDongSearch = string.Empty;
            SoHopDongSearch = string.Empty;
            DuAnSearch = string.Empty;
            NgayLapTuSearch = null;
            NgayLapDenSearch = null;
            GiaTriHopDongTuSearch = null;
            GiaTriHopDongDenSearch = null;
            IEnumerable<DonVi> donvis = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            DonViModels = _mapper.Map<ObservableCollection<DonViModel>>(donvis);
            OnLoadNhaThau();
            OnLoadDuAn();
            OnLoadLoaiHopDong();
            LoadData();
        }

        private void OnExpand()
        {
            int currentIndex = DisplayItems.IndexOf(SelectedItem);
            SelectedItem.IsShowChildren = true;
            SelectedItem.IsExpandGroup = true;
            IEnumerable<HopDongModel> children = new List<HopDongModel>(Items.Where(t => t.IdHopDongGoc.Equals(SelectedItem.Id) && !t.IdHopDongGoc.Equals(t.Id)));
            var stt = 0;
            foreach (var item in children)
            {
                //item.Stt = SelectedItem.Stt + "_" + ++stt;
                item.AncestorIds = new HashSet<Guid>();
                item.IsExpandGroup = true;
                foreach (var ancestor in SelectedItem.AncestorIds)
                {
                    item.AncestorIds.Add(ancestor);
                }
                item.AncestorIds.Add(SelectedItem.Id);
                item.HasChildren = Items.Any(t => t.IdHopDongGoc.Equals(item.Id) && !t.IdHopDongGoc.Equals(t.Id));
                if (item.HasChildren)
                {
                    item.PropertyChanged += HopDongModelPropertyChanged;
                }
                DisplayItems.Insert(++currentIndex, item);
            }
            foreach (var item in DisplayItems)
            {
                item.Stt = (++stt).ToString();
            }
            //OnPropertyChanged(nameof(DisplayItems));
        }

        private void OnCollapse()
        {
            SelectedItem.IsShowChildren = false;
            SelectedItem.IsExpandGroup = false;
            var collapseItems = DisplayItems.Where(t => t.AncestorIds.Contains(SelectedItem.Id));
            foreach (var item in collapseItems)
            {
                item.IsExpandGroup = false;
                item.IsShowChildren = false;
                item.PropertyChanged -= HopDongModelPropertyChanged;
            }
            DisplayItems = new ObservableCollection<HopDongModel>(DisplayItems.Where(t => !t.AncestorIds.Contains(SelectedItem.Id)));
            var stt = 0;
            foreach (var item in DisplayItems)
            {
                item.Stt = (++stt).ToString();
            }
            //OnPropertyChanged(nameof(DisplayItems));
        }

        private void LoadData()
        {
            try
            {
                IEnumerable<HopDongQuery> data = _vdtDaTtHopDongService.FindAllHopDongByNamLamViec(_sessionService.Current.YearOfWork).ToList();
                Items = new ObservableCollection<HopDongModel>();
                Items = _mapper.Map<ObservableCollection<Model.HopDongModel>>(data);
                foreach (var item in Items)
                {
                    if (!item.Id.Equals(item.IdHopDongGoc))
                    {
                        item.SoHDGoc = Items.FirstOrDefault(t => t.Id.Equals(item.IdHopDongGoc))?.SoHopDong;
                    }
                }
                DisplayItems = new ObservableCollection<HopDongModel>(Items.Where(t => t.Id.Equals(t.IdHopDongGoc)));
                foreach (var item in DisplayItems)
                {
                    item.HasChildren = Items.Any(t => t.IdHopDongGoc.Equals(item.Id) && !t.IdHopDongGoc.Equals(t.Id));
                    if (item.HasChildren)
                    {
                        item.PropertyChanged += HopDongModelPropertyChanged;
                    }
                    item.AncestorIds = new HashSet<Guid>();
                }
                DisplayItems.Select(n => { n.Stt = (DisplayItems.IndexOf(n) + 1).ToString(); return n; }).ToList();
                _dataIndexFilter = CollectionViewSource.GetDefaultView(DisplayItems);
                _dataIndexFilter.Filter = DataFilter;
                if (DisplayItems != null && DisplayItems.Count > 0)
                {
                    SelectedItem = DisplayItems.FirstOrDefault();
                }
                OnPropertyChanged(nameof(IsLock));
                OnPropertyChanged(nameof(IsEdit));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HopDongModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(HopDongModel.IsShowChildren)))
            {
                HopDongModel model = sender as HopDongModel;
                if (model.IsShowChildren)
                {
                    OnExpand();
                }
                else
                {
                    OnCollapse();
                }
            }
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (HopDongModel)obj;
            if (!string.IsNullOrEmpty(STenHopDongSearch))
                result = result && !string.IsNullOrEmpty(item.TenHopDong) && item.TenHopDong.ToLower().Contains(STenHopDongSearch.ToLower());
            if (!string.IsNullOrEmpty(SoHopDongSearch))
                result = result && !string.IsNullOrEmpty(item.SoHopDong) && item.SoHopDong.ToLower().Contains(SoHopDongSearch.ToLower());
            if (!string.IsNullOrEmpty(TenDonVi))
                result = result && !string.IsNullOrEmpty(item.TenDonVi) && item.TenDonVi.ToLower().Contains(TenDonVi.ToLower());
            if (!string.IsNullOrEmpty(DuAnSearch))
                result = result && !string.IsNullOrEmpty(item.TenDuAn) && item.TenDuAn.ToLower().Contains(DuAnSearch.ToLower());
            if (NgayLapTuSearch != null)
                result = result && item.NgayHopDong != null && item.NgayHopDong.Date.Equals(NgayLapTuSearch.Value.Date);
            /*if (NgayLapDenSearch != null)
                result = result && item.NgayHopDong != null && item.NgayHopDong.Date <= NgayLapDenSearch.Value.Date;*/
            if (GiaTriHopDongTuSearch != null)
                result = result && item.GiaTriSauDieuChinh == GiaTriHopDongTuSearch;
            /*if (GiaTriHopDongDenSearch != null && GiaTriHopDongDenSearch != 0)
                result = result && item.GiaTriSauDieuChinh <= GiaTriHopDongDenSearch;*/
            return result;
        }

        protected override void OnAdd()
        {
            try
            {
                ContractInfoDialogViewModel.Model = new HopDongModel();
                ContractInfoDialogViewModel.IsViewOnly = false;
                //ContractInfoDialogViewModel.ITypeStatus = (int)TypeExecute.Insert;
                ContractInfoDialogViewModel.Init();
                ContractInfoDialogViewModel.SavedAction = obj => this.LoadData();
                ContractInfoDialogViewModel.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        protected override void OnUpdate()
        {
            try
            {
                if (SelectedItem == null)
                    return;

                if (SelectedItem.BKhoa == true)
                {
                    return;
                }

                if (SelectedItem.HasChildren)
                {
                    MessageBoxHelper.Error(Resources.UnableToModifyHD);
                    return;
                }

                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Error(string.Format(Resources.MsgRoleUpdate, SelectedItem.SUserCreate));
                    return;
                }
                if (SelectedItem.BIsGoc)
                {
                    ContractInfoDialogViewModel.IsViewOnly = false;
                    ContractInfoDialogViewModel.Model = new HopDongModel();
                    ContractInfoDialogViewModel.Model.Id = SelectedItem.Id;
                    ContractInfoDialogViewModel.Model.IdDuAn = SelectedItem.IdDuAn;
                    ContractInfoDialogViewModel.Model.BIsGoc = SelectedItem.BIsGoc;
                    //ContractInfoDialogViewModel.ITypeStatus = (int)TypeExecute.Update;
                    ContractInfoDialogViewModel.Init();
                    ContractInfoDialogViewModel.SavedAction = obj => this.LoadData();
                    ContractInfoDialogViewModel.ShowDialog();
                    LoadData();
                }
                else
                {
                    OnShowPopupDieuChinh(true);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnViewHopDongGoc()
        {
            if (SelectedItem.IdHopDongGoc.IsNullOrEmpty() || SelectedItem.IdHopDongGoc.Equals(SelectedItem.Id))
            {
                return;
            }
            var lstHopDongGoc = _vdtDaTtHopDongService.FindAll(t => SelectedItem.IdHopDongGoc.Equals(t.Id)).ToList();
            if (lstHopDongGoc.Count > 0)
            {
                var hopDongGoc = lstHopDongGoc.First();
                if (hopDongGoc.BIsGoc.HasValue && hopDongGoc.BIsGoc.Value)
                {
                    ContractInfoDialogViewModel.Model = new HopDongModel();
                    ContractInfoDialogViewModel.Model.Id = hopDongGoc.Id;
                    ContractInfoDialogViewModel.Model.IdDuAn = hopDongGoc.IIdDuAnId;
                    ContractInfoDialogViewModel.Model.BIsGoc = true;
                    ContractInfoDialogViewModel.IsViewOnly = true;
                    //ContractInfoDialogViewModel.ITypeStatus = (int)TypeExecute.Update;
                    ContractInfoDialogViewModel.Init();
                    ContractInfoDialogViewModel.SavedAction = obj => { };
                    ContractInfoDialogViewModel.ShowDialog();
                }
                else
                {
                    HopDongDieuChinhDialogViewModel.Model = new HopDongModel();
                    HopDongDieuChinhDialogViewModel.Model.Id = hopDongGoc.Id;
                    HopDongDieuChinhDialogViewModel.Model.IdDuAn = hopDongGoc.IIdDuAnId;
                    HopDongDieuChinhDialogViewModel.Model.BIsGoc = false;
                    HopDongDieuChinhDialogViewModel.Init();
                    HopDongDieuChinhDialogViewModel.SavedAction = obj => { };
                    HopDongDieuChinhDialogViewModel.ShowDialog();
                }
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            try
            {
                if (SelectedItem == null)
                    return;
                ContractInfoDialogViewModel.Model = new HopDongModel();
                ContractInfoDialogViewModel.Model.Id = SelectedItem.Id;
                ContractInfoDialogViewModel.Model.IdDuAn = SelectedItem.IdDuAn;
                ContractInfoDialogViewModel.Model.BIsGoc = true;
                //ContractInfoDialogViewModel.ITypeStatus = (int)TypeExecute.Update;
                ContractInfoDialogViewModel.IsViewOnly = true;
                ContractInfoDialogViewModel.IsDetail = true;
                ContractInfoDialogViewModel.IdHopDongGoc = SelectedItem.IdHopDongGoc;
                ContractInfoDialogViewModel.Init();
                //ContractInfoDialogViewModel.SavedAction = obj => this.LoadData();
                ContractInfoDialogViewModel.ShowDialog();
                ContractInfoDialogViewModel.IsDetail = false;
                //LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool CheckCanSuaXoa()
        {
            var user = _sessionService.Current.Principal;

            if (user == SelectedItem.SUserCreate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnDelete()
        {
            try
            {
                if (SelectedItem!=null && SelectedItem.BKhoa == true)
                {
                    return;
                }

                if (SelectedItem.HasChildren)
                {
                    MessageBoxHelper.Error(Resources.UnableToDeleteHD);
                    return;
                }

                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedItem.SUserCreate));
                    return;
                }

                if (MessageBoxHelper.Confirm(Resources.ConfirmDeleteUsers) == MessageBoxResult.Yes)
                {
                    if (SelectedItem != null)
                    {
                        VdtTtDeNghiThanhToan deNghiThanhToan = _deNghiThanhToanService.FindByHopDongId(SelectedItem.Id);
                        if (deNghiThanhToan == null)
                        {
                            _vdtDaTtHopDongService.Delete(SelectedItem.Id);
                            LoadData();
                        }
                        else
                        {
                            MessageBoxHelper.Error(Resources.MsgHopDongDaDangKiThanhToan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected void OnAdjusted()
        {
            try
            {
                if (SelectedItem == null)
                    return;
                ContractInfoDialogViewModel.Model = new HopDongModel();
                ContractInfoDialogViewModel.Model.Id = SelectedItem.Id;
                ContractInfoDialogViewModel.Model.IdDuAn = SelectedItem.IdDuAn;
                // ContractInfoDialogViewModel.ITypeStatus = (int)TypeExecute.Adjust;
                ContractInfoDialogViewModel.Init();
                ContractInfoDialogViewModel.SavedAction = obj => this.LoadData();
                ContractInfoDialogViewModel.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_THONGTIN_HOPDONG;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnLockUnLock()
        {
            try
            {
                if (IsLock)
                {
                    List<DonVi> userAgency = _nsDonViService.FindByUser(_sessionService.Current.Principal, _sessionService.Current.YearOfWork, LoaiDonVi.ROOT);
                    if (!userAgency.Any(x => x.Loai == LoaiDonVi.ROOT))
                    {
                        MessageBoxHelper.Error(Resources.MsgRoleUnlock);
                        return;
                    }
                }
                else
                {
                    if (SelectedItem.SUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleLock, SelectedItem.SUserCreate));
                        return;
                    }

                    //if (SelectedItem.BActive != true)
                    //{
                    //    MessageBoxHelper.Error(string.Format(Resources.VoucherLockModified, SelectedItem.SUserCreate), Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }

                string message = IsLock ? Resources.UnlockChungTu : Resources.LockChungTu;
                if (MessageBoxHelper.Confirm(message) == MessageBoxResult.Yes)
                    LockConfirmEventHandler();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void LockConfirmEventHandler()
        {
            try
            {
                _vdtDaTtHopDongService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BKhoa);
                SelectedItem.BKhoa = !SelectedItem.BKhoa;

                OnPropertyChanged(nameof(IsLock));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        private void OnShowPopupDieuChinh(bool isUpdate = false)
        {
            try
            {
                if (SelectedItem == null)
                    return;

                if (SelectedItem.HasChildren)
                {
                    MessageBoxHelper.Error(Resources.UnableToCopyHD);
                    return;
                }

                HopDongDieuChinhDialogViewModel.Model = new HopDongModel();
                HopDongDieuChinhDialogViewModel.Model.Id = SelectedItem.Id;
                HopDongDieuChinhDialogViewModel.Model.IdDuAn = SelectedItem.IdDuAn;
                HopDongDieuChinhDialogViewModel.Model.BIsGoc = SelectedItem.BIsGoc;
                HopDongDieuChinhDialogViewModel.IsUpdated = isUpdate;
                HopDongDieuChinhDialogViewModel.HopDongGocModel = new HopDongModel();
                if (isUpdate)
                {
                    HopDongDieuChinhDialogViewModel.HopDongGocModel.Id = SelectedItem.IdHopDongGoc;
                    HopDongDieuChinhDialogViewModel.HopDongDCModel = new HopDongModel();
                    HopDongDieuChinhDialogViewModel.HopDongDCModel.Id = SelectedItem.Id;
                }
                else
                {
                    HopDongDieuChinhDialogViewModel.HopDongGocModel.Id = SelectedItem.Id;
                }
                HopDongDieuChinhDialogViewModel.Init();
                HopDongDieuChinhDialogViewModel.SavedAction = obj => this.LoadData();
                HopDongDieuChinhDialogViewModel.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }


        private void OnLoadLoaiHopDong()
        {
            _dicLoaiHopDong = new Dictionary<Guid, VdtDmLoaiHopDong>();
            var datas = _vdtDaTtHopDongService.GetAllLoaiHopDong();
            if (datas == null) return;
            foreach (var item in datas)
            {
                if (!_dicLoaiHopDong.ContainsKey(item.Id))
                    _dicLoaiHopDong.Add(item.Id, item);
            }
        }

        private void OnLoadNhaThau()
        {
            _dicNhaThau = new Dictionary<Guid, VdtDmNhaThau>();
            var datas = _nhathauService.FindAll(n => 1 == 1);
            if (datas == null) return;
            foreach (var item in datas)
            {
                if (!_dicNhaThau.ContainsKey(item.Id))
                    _dicNhaThau.Add(item.Id, item);
            }
        }

        private void OnLoadDuAn()
        {
            _dicDuAn = new Dictionary<Guid, string>();
            var datas = _duanService.FindAll(n => 1 == 1);
            if (datas == null) return;
            _dicDuAn = datas.ToDictionary(n => n.Id, n => n.SMaDuAn);
        }


        private void OnExport()
        {
            if (!DisplayItems.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Error(Resources.MsgChooseItem);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<Guid> lstId = DisplayItems.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", GetDataExport(lstId));
                data.Add("ItemsLoaiHD", _dicLoaiHopDong.Values);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTDUAN, ExportFileName.RPT_IMPORT_HOPDONG_DUAN);
                string fileNamePrefix = string.Format("{0}", ExportFileName.RPT_IMPORT_HOPDONG_DUAN);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<VdtDaTtHopDongImportModel, VdtDmLoaiHopDong>(templateFileName, data);
                e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private List<VdtDaTtHopDongImportModel> GetDataExport(List<Guid> lstId)
        {
            List<VdtDaTtHopDongImportModel> results = new List<VdtDaTtHopDongImportModel>();
            var lstData = _vdtDaTtHopDongService.FindAll(n => lstId.Contains(n.Id));
            if (lstData == null) return results;
            int iStt = 0;
            foreach (var item in lstData)
            {
                iStt++;
                VdtDaTtHopDongImportModel obj = new VdtDaTtHopDongImportModel()
                {
                    IStt = iStt.ToString(),
                    SSoHopDong = item.SSoHopDong,
                    STenHopDong = item.STenHopDong,

                    DNgayHopDong = item.DNgayHopDong.ToString("dd/MM/yyyy"),
                    IThoiGianThucHien = item.IThoiGianThucHien.ToString(),
                    FTienHopDong = (item.FTienHopDong ?? 0).ToString()
                };

                if (item.IIdDuAnId.HasValue && _dicDuAn.ContainsKey(item.IIdDuAnId.Value))
                    obj.SMaDuAn = _dicDuAn[item.IIdDuAnId.Value];
                if (item.IIdLoaiHopDongId.HasValue && _dicLoaiHopDong.ContainsKey(item.IIdLoaiHopDongId.Value))
                    obj.SMaLoaiHopDong = _dicLoaiHopDong[item.IIdLoaiHopDongId.Value].SMaLoaiHopDong;
                if (item.IIdNhaThauThucHienId.HasValue && _dicNhaThau.ContainsKey(item.IIdNhaThauThucHienId.Value))
                    obj.SMaNhaThauThucHien = _dicNhaThau[item.IIdNhaThauThucHienId.Value].SMaNhaThau;
                results.Add(obj);
            }
            return results;
        }

        private void OnImportData()
        {
            try
            {
                ContractInfoImportViewModel.Init();
                ContractInfoImportViewModel.SavedAction = obj =>
                {
                    ContractInfoImport.Close();
                    OnRefresh();
                };

                ContractInfoImport = new ContractInfoImport { DataContext = ContractInfoImportViewModel };
                ContractInfoImport.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }
    }
}