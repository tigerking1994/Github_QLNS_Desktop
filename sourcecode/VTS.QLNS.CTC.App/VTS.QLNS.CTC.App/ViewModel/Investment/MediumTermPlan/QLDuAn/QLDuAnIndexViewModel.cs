using AutoMapper;
using FlexCel.Core;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Report;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.View.Investment.MediumTermPlan.QLDuAn;
using VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.PlanSuggestions;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.QLDuAn
{
    public class QLDuAnIndexViewModel : GridViewModelBase<ProjectManagerModel>
    {
        #region Private
        private IMapper _mapper;
        private ISessionService _sessionService;
        private IProjectManagerService _projectManagerService;
        private INsNguonNganSachService _nsNguonVonService;
        private INsDonViService _nsDonViService;
        private ICollectionView _dataProjectFilter;
        private IVdtDaNguonVonService _nguonVonService;
        private IVdtDuAnHangMucService _duAnHangMucService;
        private IVdtDmLoaiCongTrinhService _loaicongtrinhService;
        private readonly IExportService _exportService;
        private readonly IAttachmentService _iAttachmentService;
        private readonly ILog _logger;
        private Dictionary<Guid, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private List<VdtDmLoaiCongTrinh> _lstLoaiCongTrinh;
        private List<NsNguonNganSach> _lstNguonVon;
        #endregion

        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_QLDA_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGER;
        public override string GroupName => MenuItemContants.GROUP_PROJECT_MANAGEMENT;
        public override string Name => "Thông tin chung của dự án";
        public override string Title => "Thông tin chung của dự án";
        public override string Description => "Danh sách thông tin dự án";
        public override Type ContentType => typeof(QLDuAnIndex);

        public QLDuAnImport QLDuAnImport { get; set; }

        #region Item
        public bool IsLock => SelectedItem != null && SelectedItem.IsLocked;
        public bool IsEdit => SelectedItem != null && !SelectedItem.IsLocked;
        public bool IsEnableLock => SelectedItem != null;

        private ProjectManagerFilterModel _projectFilter;
        public ProjectManagerFilterModel ProjectFilter
        {
            get => _projectFilter;
            set => SetProperty(ref _projectFilter, value);
        }

        private ObservableCollection<CheckBoxItem> _itemsDonVi;
        public ObservableCollection<CheckBoxItem> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private string _selectedDonVi;
        public string SelectedDonVi
        {
            get => _selectedDonVi;
            set
            {
                SetProperty(ref _selectedDonVi, value);
                _dataProjectFilter.Refresh();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsLoaiCongTrinh;
        public ObservableCollection<ComboboxItem> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private string _selectedLoaiCongTrinh;
        public string SelectedLoaiCongTrinh
        {
            get => _selectedLoaiCongTrinh;
            set => SetProperty(ref _selectedLoaiCongTrinh, value);
        }
        #endregion

        #region RelayCommand
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportCommand { get; }
        #endregion

        #region View
        public QLDuAnDialogViewModel QLDuAnDialogViewModel { get; set; }
        public QLDADetailViewModel QLDADetailViewModel { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public QLDuAnImportViewModel QLDuAnImportViewModel { get; set; }
        #endregion

        public QLDuAnIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            IProjectManagerService projectManagerService,
            INsDonViService nsDonViService,
            IVdtDaNguonVonService nguonVonService,
            IVdtDuAnHangMucService duAnHangMucService,
            IAttachmentService iAttachmentService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            INsNguonNganSachService nsNguonVonService,
            IExportService exportService,
            ILog logger,
            QLDuAnDialogViewModel qLDuAnDialogViewModel,
            QLDADetailViewModel qLDADetailViewModel,
            AttachmentViewModel attachmentViewModel,
            QLDuAnImportViewModel qLDuAnImportViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _projectManagerService = projectManagerService;
            _nsDonViService = nsDonViService;
            _nguonVonService = nguonVonService;
            _duAnHangMucService = duAnHangMucService;
            _iAttachmentService = iAttachmentService;
            _loaicongtrinhService = loaicongtrinhService;
            _nsNguonVonService = nsNguonVonService;
            _exportService = exportService;
            _logger = logger;   

            QLDuAnDialogViewModel = qLDuAnDialogViewModel;
            QLDADetailViewModel = qLDADetailViewModel;
            AttachmentViewModel = attachmentViewModel;
            QLDuAnImportViewModel = qLDuAnImportViewModel;


            QLDuAnDialogViewModel.ParentPage = this;
            QLDADetailViewModel.ParentPage = this;
            AttachmentViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(obj => _dataProjectFilter.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(obj), obj => base.SelectedItem != null && base.SelectedItem.TotalFiles > 0);
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportCommand = new RelayCommand(obj => OnExport());
        }

        #region Event
        public override void Init()
        {
            ProjectFilter = new ProjectManagerFilterModel();
            LoadLoaiCongTrinh();
            LoadNguonVon();
            LoadDonVi();
            LoadData();
        }

        private void LoadData()
        {
            IEnumerable<ProjectManagerQuery> data = _projectManagerService.FindByCondition();
            Items = _mapper.Map<ObservableCollection<Model.ProjectManagerModel>>(data);
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
            _dataProjectFilter = CollectionViewSource.GetDefaultView(Items);
            _dataProjectFilter.Filter = ProjectManagerFilter;
        }

        private bool ProjectManagerFilter(object obj)
        {
            bool result = true;
            var item = (ProjectManagerModel)obj;

            if (!string.IsNullOrEmpty(SelectedDonVi))
            {
                result = result && !string.IsNullOrEmpty(item.IIdMaDonViQuanLy) && !string.IsNullOrEmpty(item.IIdMaDonViQuanLy) && item.IIdMaDonViQuanLy == SelectedDonVi;
            }
            if (!string.IsNullOrEmpty(ProjectFilter.TenDuAn))
                result = result && !string.IsNullOrEmpty(item.STenDuAn) && !string.IsNullOrEmpty(item.SMaDuAn)
                    && (item.STenDuAn.ToLower().Contains(ProjectFilter.TenDuAn.ToLower())
                    || (item.SMaDuAn != null && item.SMaDuAn.ToLower().Contains(ProjectFilter.TenDuAn.ToLower())));
            if (!string.IsNullOrEmpty(ProjectFilter.ThoiGianFrom))
                result = result && (!string.IsNullOrEmpty(item.SKhoiCong) && Int32.Parse(item.SKhoiCong) >= Int32.Parse(ProjectFilter.ThoiGianFrom));
            if (!string.IsNullOrEmpty(ProjectFilter.ThoiGianDen))
                result = result && (!string.IsNullOrEmpty(item.SKetThuc) && Int32.Parse(item.SKetThuc) <= Int32.Parse(ProjectFilter.ThoiGianDen));
            return result;
        }

        private void OnRemoveFilter()
        {
            ProjectFilter.TenDuAn = string.Empty;
            ProjectFilter.ThoiGianFrom = null;
            ProjectFilter.ThoiGianDen = null;
            SelectedDonVi = string.Empty;
            LoadData();
        }

        protected override void OnDelete()
        {
            if (_projectManagerService.CheckExitsChuTruongDauTuByDuAnId(SelectedItem.Id))
            {
                MessageBoxHelper.Error(Resources.AlertExistChuTruongDauTu);
                return;
            }
            if (_projectManagerService.CheckExitsQdDauTuByDuAnId(SelectedItem.Id))
            {
                MessageBoxHelper.Error(Resources.AlertExistQdDauTu);
                return;
            }

            if (!CheckCanSuaXoa())
            {
                MessageBoxHelper.Error(string.Format(Resources.MsgRoleDelete, SelectedItem.SUserCreate));
                return;
            }

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                _projectManagerService.Delete(SelectedItem.Id);
                _nguonVonService.DeleteByIdDuAn(SelectedItem.Id);
                _duAnHangMucService.DeleteByDuAnId(SelectedItem.Id);
                _iAttachmentService.DeleteByObjectIdAndModuleType(SelectedItem.Id, (int) AttachmentEnum.Type.VDT_THONGTIN_DUAN);
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnAdd()
        {
            QLDuAnDialogViewModel.Model = new ProjectManagerModel();
            QLDuAnDialogViewModel.Init();
            QLDuAnDialogViewModel.SavedAction = obj => this.OnRefresh();
            QLDuAnDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                if (!CheckCanSuaXoa())
                {
                    MessageBoxHelper.Error(string.Format(Resources.MsgRoleUpdate, SelectedItem.SUserCreate));
                    return;
                }
                QLDuAnDialogViewModel.Model = SelectedItem;
                QLDuAnDialogViewModel.Init();
                QLDuAnDialogViewModel.SavedAction = obj => this.OnRefresh();
                QLDuAnDialogViewModel.ShowDialog();
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

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                QLDADetailViewModel.Model = SelectedItem;
                QLDADetailViewModel.Init();
                QLDADetailViewModel.ShowDialog();
            }
        }

        private void OnViewAttachment(object obj)
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.VDT_THONGTIN_DUAN;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        private void OnImportData()
        {
            try
            {
                QLDuAnImportViewModel.Init();
                QLDuAnImportViewModel.SavedAction = obj =>
                {
                    QLDuAnImport.Close();
                    OnRefresh();
                };

                QLDuAnImport = new QLDuAnImport { DataContext = QLDuAnImportViewModel };
                QLDuAnImport.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExport()
        {
            if(!Items.Any(n => n.IsChecked))
            {
                MessageBoxHelper.Error(Resources.MsgChooseItem);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<Guid> lstId = Items.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", GetDataExport(lstId));
                data.Add("ItemsLoaiCongTrinh", _lstLoaiCongTrinh);
                data.Add("ItemsNguonVon", _lstNguonVon);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_QLDUAN, ExportFileName.RPT_VDT_DA_THONGTINDUAN);
                string fileNamePrefix = string.Format("{0}", ExportFileName.RPT_VDT_DA_THONGTINDUAN);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<VdtDaThongTinDuAnExportModel, VdtDmLoaiCongTrinh, NsNguonNganSach>(templateFileName, data);
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
        #endregion

        #region Helper
        private void LoadDonVi()
        {
            ItemsDonVi = new ObservableCollection<CheckBoxItem>();
            IEnumerable<DonVi> listDonVi = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork);
            ItemsDonVi = _mapper.Map<ObservableCollection<CheckBoxItem>>(listDonVi);
        }

        private List<VdtDaThongTinDuAnExportModel> GetDataExport(List<Guid> lstDuAnId)
        {
            List<VdtDaThongTinDuAnExportModel> results = new List<VdtDaThongTinDuAnExportModel>();
            List<VdtDaDuAn> lstDuAn = _projectManagerService.FindAll(n => lstDuAnId.Contains(n.Id)).ToList();
            List<VdtDaNguonVon> lstNguonVon = _nguonVonService.FindAll(n => lstDuAnId.Contains(n.IIdDuAn)).ToList();
            List<VdtDaDuAnHangMuc> lstHangMuc = _duAnHangMucService.FindAll(n => n.IIdDuAnId.HasValue && lstDuAnId.Contains(n.IIdDuAnId.Value)).ToList();
            Dictionary<Guid, List<VdtDaNguonVon>> dicNguonVon = new Dictionary<Guid, List<VdtDaNguonVon>>();
            Dictionary<Guid, List<VdtDaDuAnHangMuc>> dicHangMuc = new Dictionary<Guid, List<VdtDaDuAnHangMuc>>();
            if(lstNguonVon != null)
            {
                dicNguonVon = lstNguonVon.GroupBy(n => n.IIdDuAn).ToDictionary(n => n.Key, n => n.ToList());
            }
            if(lstHangMuc != null)
            {
                dicHangMuc = lstHangMuc.GroupBy(n => n.IIdDuAnId.Value).ToDictionary(n => n.Key, n => n.ToList());
            }
            int iStt = 1;
            foreach(var item in lstDuAn)
            {
                VdtDaThongTinDuAnExportModel data = new VdtDaThongTinDuAnExportModel()
                {
                    IStt = iStt.ToString(),
                    STenDuAn = item.STenDuAn,
                    SMaDuAn = item.SMaDuAn,
                    SKhoiCong = item.SKhoiCong,
                    SKetThuc = item.SKetThuc,
                    SDiaDiem = item.SDiaDiem,
                    SMucTieu = item.SMucTieu,
                    FHanMucDauTu = (item.FHanMucDauTu ?? 0).ToString()
                };
                var hangmucs = new List<VdtDaDuAnHangMuc>();
                var nguonvons = new List<VdtDaNguonVon>();
                if (dicHangMuc.ContainsKey(item.Id)) hangmucs = dicHangMuc[item.Id];
                if (dicNguonVon.ContainsKey(item.Id)) nguonvons = dicNguonVon[item.Id];
                if(hangmucs.Count() >= nguonvons.Count())
                {
                    results.AddRange(GetDataByHangMuc(data, hangmucs, nguonvons));
                }
                else
                {
                    results.AddRange(GetDataByNguonVon(data, hangmucs, nguonvons));
                }
                iStt++;
            }

            return results;
        }

        private List<VdtDaThongTinDuAnExportModel> GetDataByHangMuc(VdtDaThongTinDuAnExportModel data, List<VdtDaDuAnHangMuc> lstHangMuc, List<VdtDaNguonVon> lstnguonVon)
        {
            List<VdtDaThongTinDuAnExportModel> results = new List<VdtDaThongTinDuAnExportModel>();
            VdtDaNguonVon objNguonVon = new VdtDaNguonVon();
            int countNguonVon = lstnguonVon.Count();
            int indexNguonVon = 0;
            foreach (var item in lstHangMuc)
            {
                if(indexNguonVon < countNguonVon)
                {
                    objNguonVon = lstnguonVon[indexNguonVon];
                    indexNguonVon++;
                }
                var current = data.Clone();
                current.STenHangMuc = item.STenHangMuc;
                if(item.IdLoaiCongTrinh.HasValue && _dicLoaiCongTrinh.ContainsKey(item.IdLoaiCongTrinh.Value))
                {
                    current.SMaLoaiCongTrinh = _dicLoaiCongTrinh[item.IdLoaiCongTrinh.Value].SMaLoaiCongTrinh;
                }
                current.IIdNguonVonId = objNguonVon.IIdNguonVonId.ToString();
                results.Add(current);
            }
            return results;
        }

        private List<VdtDaThongTinDuAnExportModel> GetDataByNguonVon(VdtDaThongTinDuAnExportModel data, List<VdtDaDuAnHangMuc> lstHangMuc, List<VdtDaNguonVon> lstnguonVon)
        {
            List<VdtDaThongTinDuAnExportModel> results = new List<VdtDaThongTinDuAnExportModel>();
            VdtDaDuAnHangMuc objHangMuc = new VdtDaDuAnHangMuc();
            int countHangMuc = lstHangMuc.Count();
            int indexHangMuc = 0;
            foreach (var item in lstnguonVon)
            {
                if (indexHangMuc < countHangMuc)
                {
                    objHangMuc = lstHangMuc[indexHangMuc];
                    indexHangMuc++;
                }
                var current = data.Clone();
                current.STenHangMuc = objHangMuc.STenHangMuc;
                if (objHangMuc.IdLoaiCongTrinh.HasValue && _dicLoaiCongTrinh.ContainsKey(objHangMuc.IdLoaiCongTrinh.Value))
                {
                    current.SMaLoaiCongTrinh = _dicLoaiCongTrinh[objHangMuc.IdLoaiCongTrinh.Value].SMaLoaiCongTrinh;
                }
                current.IIdNguonVonId = item.IIdNguonVonId.ToString();
                results.Add(current);
            }
            return results;
        }

        private void LoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<Guid, VdtDmLoaiCongTrinh>();
            var lstLoaiCongTrinh = _loaicongtrinhService.FindAll();
            if (lstLoaiCongTrinh == null) return;
            foreach(var item in lstLoaiCongTrinh)
            {
                if (!_dicLoaiCongTrinh.ContainsKey(item.IIdLoaiCongTrinh))
                    _dicLoaiCongTrinh.Add(item.IIdLoaiCongTrinh, item);
            }
            _lstLoaiCongTrinh = _dicLoaiCongTrinh.Values.ToList();
        }

        private void LoadNguonVon()
        {
            _lstNguonVon = _nsNguonVonService.FindNguonNganSach().ToList();
        }

        #endregion
    }
}
