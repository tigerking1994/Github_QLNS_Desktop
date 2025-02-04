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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau
{
    public class GoiThauIndexViewModel : GridViewModelBase<VdtDuToanModel>
    {
        private readonly ILog _logger;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private INsDonViService _nsDonViService;
        private IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IVdtDmNhaThauService _nhathauService;
        private readonly IProjectManagerService _duanService;
        private ICollectionView _dataGoiThauFilter;
        private readonly IExportService _exportService;
        private IVdtDaTtHopDongService _hopDongService;
        Dictionary<Guid, VdtDmNhaThau> _dicNhaThau;
        Dictionary<Guid, string> _dicDuAn;
        private VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDetail view;
        private VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDieuChinhDetail viewDC;

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_GOI_THAU_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        public override string Name => "Thông tin gói thầu";
        public override string Title => "Thông tin gói thầu";
        public override string Description => "Danh sách thông tin gói thầu";

        public override Type ContentType => typeof(View.Investment.MediumTermPlan.GoiThau.GoiThauIndex);

        private VdtGoiThauFilterModel _vdtGoiThauFilter;
        public VdtGoiThauFilterModel VdtGoiThauFilter
        {
            get => _vdtGoiThauFilter;
            set => SetProperty(ref _vdtGoiThauFilter, value);
        }

        private ObservableCollection<VdtDaGoiThauModel> _dataVdtGoiThau;
        public ObservableCollection<VdtDaGoiThauModel> DataVdtGoiThau
        {
            get => _dataVdtGoiThau;
            set => SetProperty(ref _dataVdtGoiThau, value);
        }

        private VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel _goiThau;
        public VTS.QLNS.CTC.App.Model.VdtDaGoiThauModel SelectedGoiThau
        {
            get => _goiThau;
            set
            {
                SetProperty(ref _goiThau, value);
                OnPropertyChanged(nameof(IsEdit));
                OnPropertyChanged(nameof(IsLock));
            }
        }

        public bool IsLock => SelectedGoiThau != null && SelectedGoiThau.BKhoa == true;
        public bool IsEdit => SelectedGoiThau != null && !SelectedGoiThau.BKhoa;

        private ObservableCollection<CheckBoxItem> _dataDonVi;
        public ObservableCollection<CheckBoxItem> DataDonVi
        {
            get => _dataDonVi;
            set => SetProperty(ref _dataDonVi, value);
        }

        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
            }
        }

        public bool IsEditable => SelectedGoiThau != null && SelectedGoiThau.BActive.Value && SelectedGoiThau.BKhoa == false;

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ShowPopupDieuChinhCommand { get; }

        public GoiThauDialogViewModel GoiThauDialogViewModel { get; }
        public GoiThauDetailViewModel GoiThauDetailViewModel { get; }
        public GoiThauDieuChinhViewModel GoiThauDieuChinhViewModel { get; }
        public GoiThauDieuChinhDetailViewModel GoiThauDieuChinhDetailViewModel { get; }
        public GoiThauImportViewModel GoiThauImportViewModel { get; set; }
        public GoiThauImport GoiThauImport { get; set; }

        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportCommand { get; }

        public GoiThauIndexViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IVdtDaTtHopDongService hopDongService,
            IExportService exportService,
            IVdtDmNhaThauService nhathauService,
            IProjectManagerService duanService,
            GoiThauDialogViewModel goiThauDialogViewModel,
            GoiThauDetailViewModel goiThauDetailViewModel,
            GoiThauDieuChinhViewModel goiThauDieuChinhViewModel,
            GoiThauDieuChinhDetailViewModel goiThauDieuChinhDetailViewModel,
            GoiThauImportViewModel goiThauImportViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _hopDongService = hopDongService;
            _exportService = exportService;
            _nhathauService = nhathauService;
            _duanService = duanService;

            SearchCommand = new RelayCommand(obj => _dataGoiThauFilter.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ShowPopupDieuChinhCommand = new RelayCommand(o => OnShowPopupDieuChinh());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            ExportCommand = new RelayCommand(n => OnExport());
            ImportDataCommand = new RelayCommand(obj => OnImportData());

            GoiThauDialogViewModel = goiThauDialogViewModel;
            GoiThauDetailViewModel = goiThauDetailViewModel;
            GoiThauDieuChinhViewModel = goiThauDieuChinhViewModel;
            GoiThauDieuChinhDetailViewModel = goiThauDieuChinhDetailViewModel;
            GoiThauImportViewModel = goiThauImportViewModel;
        }

        public override void Init()
        {
            VdtGoiThauFilter = new VdtGoiThauFilterModel();
            OnLoadNhaThau();
            OnLoadDuAn();
            LoadDonVi();
            LoadData();
            GoiThauDieuChinhDetailViewModel.ClosePopup += RefreshAfterClosePopup;
            GoiThauDetailViewModel.ClosePopup += RefreshAfterClosePopup;
        }

        private void LoadDonVi()
        {
            DataDonVi = new ObservableCollection<CheckBoxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            DataDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

        private void LoadData()
        {
            IEnumerable<VdtDaGoiThauQuery> data = _vdtDaGoiThauService.FindByCondition(_sessionService.Current.YearOfWork);
            DataVdtGoiThau = _mapper.Map<ObservableCollection<Model.VdtDaGoiThauModel>>(data);
            if (DataVdtGoiThau != null && DataVdtGoiThau.Count > 0)
            {
                SelectedGoiThau = DataVdtGoiThau.FirstOrDefault();
            }
            _dataGoiThauFilter = CollectionViewSource.GetDefaultView(DataVdtGoiThau);
            _dataGoiThauFilter.Filter = GoiThauFilterMethod;
        }

        private bool GoiThauFilterMethod(object obj)
        {
            bool result = true;
            var item = (VdtDaGoiThauModel)obj;

            if (!string.IsNullOrEmpty(VdtGoiThauFilter.TenGoiThau))
                result = result && !string.IsNullOrEmpty(item.STenGoiThau) && item.STenGoiThau.ToLower().Contains(VdtGoiThauFilter.TenGoiThau.ToLower());
            if (!string.IsNullOrEmpty(VdtGoiThauFilter.DuAn) && !string.IsNullOrEmpty(item.STenDuAn))
                result = result && !string.IsNullOrEmpty(item.STenDuAn) && item.STenDuAn.ToLower().Contains(VdtGoiThauFilter.DuAn.ToLower());
            if (VdtGoiThauFilter.GiaTriFrom != null )
                result = result && item.FTongTienSauDieuChinh != null && (item.FTongTienSauDieuChinh.Value >= VdtGoiThauFilter.GiaTriFrom);
            if (VdtGoiThauFilter.GiaTriTo != null)
                result = result && item.FTongTienSauDieuChinh != null && (item.FTongTienSauDieuChinh.Value <= VdtGoiThauFilter.GiaTriTo);
            return result;
        }

        private void OnRemoveFilter()
        {
            VdtGoiThauFilter.TenGoiThau = string.Empty;
            VdtGoiThauFilter.DuAn = string.Empty;
            VdtGoiThauFilter.GiaTriFrom = null;
            VdtGoiThauFilter.GiaTriTo = null;
            LoadData();
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnDelete()
        {
            if (SelectedGoiThau != null && SelectedGoiThau.BKhoa == true)
            {
                return;
            }
            
            if (_hopDongService.CheckExistHopDongByGoiThai(SelectedGoiThau.Id))
            {
                MessageBoxHelper.Error(Resources.MsgErrorGoiThauExistHopDong);
                return;
            }

            if (!CheckCanSuaXoa())
            {
                MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedGoiThau.SUserCreate));
                return;
            }

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _vdtDaGoiThauService.DeleteGoiThauChiTiet(SelectedGoiThau.Id);
                OnRefresh();
            }
        }

        protected override void OnAdd()
        {
            GoiThauDialogViewModel.GoiThau = new Model.VdtDaGoiThauModel();
            GoiThauDialogViewModel.Init();
            GoiThauDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnShowDetailGoiThau((VdtDaGoiThauModel)obj);
            };
            var view = new GoiThauDialog
            {
                DataContext = GoiThauDialogViewModel
            };
            DialogHost.Show(view, "RootDialog", null, null);
        }

        protected override void OnUpdate()
        {
            if (SelectedGoiThau != null)
            {
                if (SelectedGoiThau.BKhoa == true)
                {
                    return;
                }
                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Error(string.Format(Resources.MsgRoleUpdate, SelectedGoiThau.SUserCreate));
                    return;
                }
                this.GoiThauDialogViewModel.GoiThau = SelectedGoiThau;
                GoiThauDialogViewModel.Init();
                GoiThauDialogViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                };
                
                GoiThauDialogViewModel.ShowDialog();
            }
        }

        private bool CheckCanSuaXoa()
        {
            var user = _sessionService.Current.Principal;

            if (user == SelectedGoiThau.SUserCreate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);

            GoiThauDialogViewModel.GoiThau = (VdtDaGoiThauModel)obj;
            GoiThauDialogViewModel.IsDetail = true;
            GoiThauDialogViewModel.Init();
            var view = new GoiThauDialog
            {
                DataContext = GoiThauDialogViewModel
            };
            GoiThauDialogViewModel.ShowDialog();
            GoiThauDialogViewModel.IsDetail = false;
        }

        public void OnShowDetailGoiThau(VdtDaGoiThauModel goiThauDetail)
        {
            if (goiThauDetail == null)
                return;
            GoiThauDetailViewModel.Model = goiThauDetail;
            GoiThauDetailViewModel.Init();
            view = new VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDetail
            {
                DataContext = GoiThauDetailViewModel
            };
            view.ShowDialog();
        }

        private async void OnShowPopupDieuChinh()
        {
            if (SelectedGoiThau != null)
            {
                this.GoiThauDieuChinhViewModel.GoiThau = SelectedGoiThau;
                GoiThauDieuChinhViewModel.Init();
                GoiThauDieuChinhViewModel.SavedAction = obj =>
                {
                    this.OnRefresh();
                    OnShowDieuChinhDetailGoiThau((VdtDaGoiThauModel)obj);
                };
                var view = new GoiThauDieuChinh
                {
                    DataContext = GoiThauDieuChinhViewModel
                };
                var result = await DialogHost.Show(view, "RootDialog", null, null);
            }
        }

        public void OnShowDieuChinhDetailGoiThau(VdtDaGoiThauModel goiThauDieuChinhDetail)
        {
            if (goiThauDieuChinhDetail == null)
                return;
            GoiThauDieuChinhDetailViewModel.Model = goiThauDieuChinhDetail;
            GoiThauDieuChinhDetailViewModel.Init();
            viewDC = new VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.GoiThau.GoiThauDieuChinhDetail
            {
                DataContext = GoiThauDieuChinhDetailViewModel
            };
            viewDC.ShowDialog();
        }

        private void RefreshAfterClosePopup(object sender, EventArgs e)
        {
            view.Close();
            OnRefresh();
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
                    if (SelectedGoiThau.SUserCreate != _sessionService.Current.Principal)
                    {
                        MessageBoxHelper.Error(string.Format(Resources.MsgRoleLock, SelectedGoiThau.SUserCreate));
                        return;
                    }

                    if (SelectedGoiThau.BActive != true)
                    {
                        MessageBoxHelper.Error(string.Format(Resources.VoucherLockModified, SelectedGoiThau.SUserCreate));
                        return;
                    }
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

        private void OnImportData()
        {
            try
            {
                GoiThauImportViewModel.Init();
                GoiThauImportViewModel.SavedAction = obj =>
                {
                    GoiThauImport.Close();
                    OnRefresh();
                };

                GoiThauImport = new GoiThauImport { DataContext = GoiThauImportViewModel };
                GoiThauImport.ShowDialog();
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
                _vdtDaGoiThauService.LockOrUnlock(SelectedGoiThau.Id, !SelectedGoiThau.BKhoa);
                SelectedGoiThau.BKhoa = !SelectedGoiThau.BKhoa;

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
        }

        private void OnExport()
        {
            if (!DataVdtGoiThau.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Error(Resources.MsgChooseItem);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<Guid> lstId = DataVdtGoiThau.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", GetDataExport(lstId));
                data.Add("ItemsNhaThau", _dicNhaThau.Values);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTDUAN, ExportFileName.RPT_IMPORT_GOITHAU_DUAN);
                string fileNamePrefix = string.Format("{0}", ExportFileName.RPT_IMPORT_GOITHAU_DUAN);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<VdtDaGoiThauImportModel, VdtDmNhaThau>(templateFileName, data);
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

        private List<VdtDaGoiThauImportModel> GetDataExport(List<Guid> lstId)
        {
            List<VdtDaGoiThauImportModel> results = new List<VdtDaGoiThauImportModel>();
            var lstData = _vdtDaGoiThauService.FindAll(n => lstId.Contains(n.Id));
            if(lstData == null) return results;
            int iStt = 0;
            foreach(var item in lstData) 
            {
                iStt++;
                VdtDaGoiThauImportModel obj = new VdtDaGoiThauImportModel()
                {
                    IStt = iStt.ToString(),
                    SSoQuyetDinh = item.SoQuyetDinh,
                    DNgayQuyetDinh = item.NgayQuyetDinh.HasValue ? item.NgayQuyetDinh.Value.ToString("dd/MM/yyyy") : string.Empty,
                    STenGoiThau = item.STenGoiThau,
                    FTienTrungThau = (item.FTienTrungThau ?? 0).ToString(),
                    SThoiGianThucHien = item.SThoiGianThucHien
                };
                if (item.IIdDuAnId.HasValue && _dicDuAn.ContainsKey(item.IIdDuAnId.Value))
                    obj.SMaDuAn = _dicDuAn[item.IIdDuAnId.Value];
                if (item.IIdNhaThauId.HasValue && _dicNhaThau.ContainsKey(item.IIdNhaThauId.Value))
                    obj.SMaNhaThau = _dicNhaThau[item.IIdNhaThauId.Value].SMaNhaThau;
                results.Add(obj);
            }
            return results;
        }
    }
}
