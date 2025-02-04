using AutoMapper;
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
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;
using log4net;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc.ImportHopDong;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service.Impl;
using FlexCel.Core;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc
{
    public class NHHopDongTrongNuocIndexViewModel : GridViewModelBase<NhDaHopDongModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILogger<NHHopDongTrongNuocIndexViewModel> _logger;
        private ICollectionView _itemsCollectionView;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaGoiThauHangMucSerrvice _nhDaGoiThauHangMucSerrvice;
        private readonly INhDaHopDongGoiThauNhaThauService _nhDaHopDongGoiThauNhaThauService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDmLoaiHopDongService _nhdmLoaiHopDongService;
        private readonly IExportService _exportService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly INhDaDuAnService _nhDaDuAnService;

        public override string Name => "Hợp đồng trong nước";
        public override string Title => "Quản lý Hợp đồng trong nước";
        public override string Description => "Danh sách Hợp đồng trong nước";
        public override Type ContentType => typeof(NHHopDongTrongNuocIndex);

        public NHHopDongTrongNuocDialogViewModel NHHopDongTrongNuocDialogViewModel { get; }
        public int ILoaiMenu { get; set; }

        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive && !SelectedItem.BIsKhoa;
        public bool IsTenDuAn => (IThuocMenu == 4 || IThuocMenu == 1);
        public bool IsTenChuongTrinh => (IThuocMenu == 3 || IThuocMenu == 2);
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public bool IsShowDuAn { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand LockUnlockCommand { get; set; }
        public RelayCommand ViewAttachmentCommand { get; }


        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;

        private ObservableCollection<NhDmLoaiHopDongModel> _itemsLoaiHopDongExport;
        public ObservableCollection<NhDmLoaiHopDongModel> ItemsLoaiHopDongExport
        {
            get => _itemsLoaiHopDongExport;
            set => SetProperty(ref _itemsLoaiHopDongExport, value);
        }

        private NhDaHopDongModel _itemsFilter;
        public NhDaHopDongModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
        }

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }
        private ObservableCollection<NhDmNhaThauModel> _itemsNhaThau;
        public ObservableCollection<NhDmNhaThauModel> ItemsNhaThau
        {
            get => _itemsNhaThau;
            set => SetProperty(ref _itemsNhaThau, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private bool _isOpenExcelPopup;
        public bool IsOpenExcelPopup
        {
            get => _isOpenExcelPopup;
            set => SetProperty(ref _isOpenExcelPopup, value);
        }

        private bool? _isAllSelected;

        public bool? IsAllSelected
        {
            get => _isAllSelected;
            set => SetProperty(ref _isAllSelected, value);
        }

        private ObservableCollection<ComboboxItem> _itemsChuongTrinh;
        public ObservableCollection<ComboboxItem> ItemsChuongTrinh
        {
            get => _itemsChuongTrinh;
            set => SetProperty(ref _itemsChuongTrinh, value);
        }

        private ComboboxItem _selectedChuongTrinh;
        public ComboboxItem SelectedChuongTrinh
        {
            get => _selectedChuongTrinh;
            set => SetProperty(ref _selectedChuongTrinh, value);
        }
        
        private ObservableCollection<ComboboxItem> _itemsDuAn;
        public ObservableCollection<ComboboxItem> ItemsDuAn
        {
            get => _itemsDuAn;
            set => SetProperty(ref _itemsDuAn, value);
        }

        private ComboboxItem _selectedDuAn;
        public ComboboxItem SelectedDuAn
        {
            get => _selectedDuAn;
            set => SetProperty(ref _selectedDuAn, value);
        }

        public ImportHopDongViewModel ImportHopDongViewModel { get; set; }
        public RelayCommand ImportDataCommand { get; }
        //public RelayCommand DowloadTemplateCommand { get; }
        public object TemplateFileName { get; private set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ExportDataCTCCommand { get; }
        public RelayCommand CheckRowCommand { get; private set; }
        public RelayCommand CheckAllRowsCommand { get; private set; }
        public RelayCommand OpenPopupExcelCommand { get; }

        public NHHopDongTrongNuocIndexViewModel(

            IMapper mapper,
            IExportService exportService,
            ISessionService sessionService,
            ILogger<NHHopDongTrongNuocIndexViewModel> logger,
            INhDaHopDongService nhDaHopDongService,
            INhDmNhaThauService nhDmNhaThauService,
            INhDaGoiThauHangMucSerrvice nhDaGoiThauHangMucSerrvice,
            INhDaHopDongGoiThauNhaThauService nhDaHopDongGoiThauNhaThauService,
            INsDonViService nsDonViService,
            INhDaDuAnService nhDaDuAnService,
            INhDmLoaiHopDongService nhdmLoaiHopDongService,
            NHHopDongTrongNuocDialogViewModel nHHopDongTrongNuocDialogViewModel,
            AttachmentViewModel attachmentViewModel,
            ImportHopDongViewModel importHopDongViewModel)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nhDaHopDongService = nhDaHopDongService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _nhdmLoaiHopDongService = nhdmLoaiHopDongService;
            _nhDaGoiThauHangMucSerrvice = nhDaGoiThauHangMucSerrvice;
            _nhDaHopDongGoiThauNhaThauService = nhDaHopDongGoiThauNhaThauService;
            _nhDaDuAnService = nhDaDuAnService;

            ImportHopDongViewModel = importHopDongViewModel;
            AttachmentViewModel = attachmentViewModel;
            NHHopDongTrongNuocDialogViewModel = nHHopDongTrongNuocDialogViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            LockUnlockCommand = new RelayCommand(obj => OnLockUnLock());
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => 
            Items != null && SelectedItem.TotalFiles > 0);
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            //DowloadTemplateCommand = new RelayCommand(obj => OnDowloadTemplate());
            OpenPopupExcelCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            ExportDataCommand = new RelayCommand(obj => OnExportData(1), obj => Items.Any(x => x.IsChecked));
            ExportDataCTCCommand = new RelayCommand(obj => OnExportData(2), obj => Items.Any(x => x.IsChecked));
            CheckRowCommand = new RelayCommand(o => OnCheckRow());
            CheckAllRowsCommand = new RelayCommand(o => OnCheckAllRows());
        }

        public override void Init()
        {
            OnPropertyChanged(nameof(IsTenDuAn));
            OnPropertyChanged(nameof(IsTenChuongTrinh));
            LoadDefault();
            LoadDonVi();
            LoadData();
            //LoadNhaThau();
            //LoadLoaiHopDong();
        }

        private void LoadLoaiHopDong()
        {
            var data = _nhdmLoaiHopDongService.FindAll();
            _itemsLoaiHopDongExport = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(data);
            OnPropertyChanged(nameof(ItemsLoaiHopDongExport));
        }

        private void LoadNhaThau()
        {
            var data = _nhDmNhaThauService.FindAll();
            _itemsNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(data);
            OnPropertyChanged(nameof(ItemsNhaThau));
        }

        private void OnCheckAllRows()
        {
            if (Items != null)
            {
                if (IsAllSelected == true)
                {
                    Items.ToList().ForEach(x => x.IsChecked = true);
                }
                else
                {
                    Items.ToList().ForEach(x => x.IsChecked = false);
                }
            }
        }

        private void OnCheckRow()
        {
            if (Items != null)
            {
                if (Items.ToList().All(x => x.IsChecked))
                {
                    IsAllSelected = true;
                }
                else if (Items.ToList().All(x => !x.IsChecked))
                {
                    IsAllSelected = false;
                }
                else
                {
                    IsAllSelected = null;
                }
            }
        }

        public void OnImportData()
        {
            ImportHopDongViewModel.ILoai = ILoai;
            ImportHopDongViewModel.IThuocMenu = IThuocMenu;
            ImportHopDongViewModel.Init();
            ImportHopDongViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            ImportHopDongViewModel.ShowDialog();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaHopDongModel();
        }

        public void OnExportData(int typeExport)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    string fileNameExport = typeExport == 1 ? ExportFileName.RPT_NH_DANHMUC_HOPDONG_DUAN : ExportFileName.RPT_Hop_Dong_Ngoai_Thuong_CucTaiChinh;
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, fileNameExport);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));

                    //var dataDonVis = _nsDonViService.FindAll().Where(x => x.NamLamViec == _sessionService.Current.YearOfWork).OrderBy(x => x.IIDMaDonVi);
                    //var lstDonVi = _mapper.Map<ObservableCollection<DonViModel>>(dataDonVis);
                    List<NhDmLoaiHopDong> dataLoaiHopDongs = _nhdmLoaiHopDongService.FindAll();
                    ObservableCollection<NhDmLoaiHopDongModel> lstLoaiHopDong = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(dataLoaiHopDongs);
                    IEnumerable<NhDmNhaThau> dataNhaThaus = _nhDmNhaThauService.FindAll();
                    ObservableCollection<NhDmNhaThauModel> lstNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(dataNhaThaus);
                    ObservableCollection<NhDaHopDongModel> nhDaHopDongs = new ObservableCollection<NhDaHopDongModel>(Items.Where(x => x.IsChecked));
                    int i = 1;
                    nhDaHopDongs.ForAll(x => {
                        x.STT = i.ToString();
                        i++;
                    });

                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        //data.Add("ItemsDonVi", lstDonVi);
                        { "Items", nhDaHopDongs }
                    };

                    if (typeExport == 1)
                    {
                        IEnumerable<NhDaDuAnQuery> dataDuAns = _nhDaDuAnService.FindIndex(2);
                        ObservableCollection<NhDaDuAnModel> lstDuAn = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(dataDuAns);
                        data.Add("ItemsLoaiHopDong", lstLoaiHopDong);
                        data.Add("ItemsNhaThau", lstNhaThau);
                        data.Add("ItemsDuAn", lstDuAn);
                        ExcelFile xlsFile = _exportService.Export<NhDaHopDongModel, NhDmLoaiHopDongModel, NhDmNhaThauModel, NhDaDuAnModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        data.Add("ItemsLoaiHD", lstLoaiHopDong);
                        data.Add("ItemsDmNhaThau", lstNhaThau);
                        ExcelFile xlsFile = _exportService.Export<NhDaHopDongModel, NhDmLoaiHopDongModel, NhDmNhaThauModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }

                    e.Result = results;
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (List<ExportResult>)e.Result;
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                    else
                    {
                        _logger.LogError(e.Error.Message);
                    }

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        private void LoadDonVi()
        {
            int yearOfWork = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == yearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhDaHopDongModel>();
                    e.Result = _nhDaHopDongService.FindAllHopDongtrongnuoc(IThuocMenu).Where(x => x.ILoai == ILoai).OrderByDescending(x => x.DNgayTao);
                }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(e.Result);
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();
                    }
                    _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                    _itemsCollectionView.Filter = Items_Filter;
                    LoadChuongTrinh();
                    LoadDuAn();
                }
            });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhDaHopDongModel item)
            {
                bool result = true;
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.SSoHopDong) && !string.IsNullOrEmpty(item.SSoHopDong))
                    {
                        result &= item.SSoHopDong.Contains(ItemsFilter.SSoHopDong, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayHopDong != null)
                    {
                        result &= ItemsFilter.DNgayHopDong.Value == item.DNgayHopDong.Value;
                    }
                    //if (!string.IsNullOrEmpty(ItemsFilter.SMoTa) && !string.IsNullOrEmpty(item.SMoTa))
                    //{
                    //    result &= item.SMoTa.Contains(ItemsFilter.SMoTa, StringComparison.OrdinalIgnoreCase);
                    //}
                    if (!string.IsNullOrEmpty(ItemsFilter.STenDuAn) && !string.IsNullOrEmpty(item.STenDuAn))
                    {
                        result &= item.STenDuAn.Contains(ItemsFilter.STenDuAn, StringComparison.OrdinalIgnoreCase);
                    }
                    if (!string.IsNullOrEmpty(ItemsFilter.STenChuongTrinh) && !string.IsNullOrEmpty(item.STenChuongTrinh))
                    {
                        result &= item.STenChuongTrinh.Contains(ItemsFilter.STenChuongTrinh, StringComparison.OrdinalIgnoreCase);
                    }
                }
                if (SelectedDonVi != null)
                {
                    result &= item.IIdDonViQuanLyId.HasValue && item.IIdDonViQuanLyId.Value.Equals(SelectedDonVi.Id);
                }
                if (SelectedDuAn != null)
                {
                    result &= item.IIdDuAnId.HasValue && item.IIdDuAnId.Value.Equals(SelectedDuAn.Id);
                }
                if (SelectedChuongTrinh != null)
                {
                    result &= item.IIdKhTongTheNhiemVuChiId.HasValue && item.IIdKhTongTheNhiemVuChiId.Value.Equals(SelectedChuongTrinh.Id);
                }
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.ConfirmUnLockGroups : Resources.ConfirmLockGroups;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _nhDaHopDongService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
                SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
                OnPropertyChanged(nameof(IsLock));
            }
            OnRefresh();
        }

        protected override void OnAdd()
        {
            NHHopDongTrongNuocDialogViewModel.Model = new NhDaHopDongModel
            {
                Id = Guid.NewGuid()
            };
            NHHopDongTrongNuocDialogViewModel.IsDieuChinh = false;
            NHHopDongTrongNuocDialogViewModel.IsDetail = false;
            NHHopDongTrongNuocDialogViewModel.IsAdd = true;
            NHHopDongTrongNuocDialogViewModel.ILoai = ILoai;
            NHHopDongTrongNuocDialogViewModel.IThuocMenu = IThuocMenu;
            NHHopDongTrongNuocDialogViewModel.IsShowDuAn = IsShowDuAn;
            NHHopDongTrongNuocDialogViewModel.ILoaiMenu = ILoaiMenu;
            NHHopDongTrongNuocDialogViewModel.Init();
            NHHopDongTrongNuocDialogViewModel.SavedAction = obj => this.OnRefresh();
            NHHopDongTrongNuocDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            NHHopDongTrongNuocDialogViewModel.Model = SelectedItem;
            NHHopDongTrongNuocDialogViewModel.IsDieuChinh = false;
            NHHopDongTrongNuocDialogViewModel.IsAdd = false;
            NHHopDongTrongNuocDialogViewModel.IsDetail = false;
            NHHopDongTrongNuocDialogViewModel.ILoai = ILoai;
            NHHopDongTrongNuocDialogViewModel.IThuocMenu = IThuocMenu;
            NHHopDongTrongNuocDialogViewModel.IsShowDuAn = IsShowDuAn;
            NHHopDongTrongNuocDialogViewModel.ILoaiMenu = ILoaiMenu;
            NHHopDongTrongNuocDialogViewModel.Init();
            NHHopDongTrongNuocDialogViewModel.SavedAction = obj => this.OnRefresh();
            NHHopDongTrongNuocDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            NHHopDongTrongNuocDialogViewModel.Model = SelectedItem;
            NHHopDongTrongNuocDialogViewModel.IsDieuChinh = false;
            NHHopDongTrongNuocDialogViewModel.IsAdd = false;
            NHHopDongTrongNuocDialogViewModel.IsDetail = true;
            NHHopDongTrongNuocDialogViewModel.ILoai = ILoai;
            NHHopDongTrongNuocDialogViewModel.IThuocMenu = IThuocMenu;
            NHHopDongTrongNuocDialogViewModel.IsShowDuAn = IsShowDuAn;
            NHHopDongTrongNuocDialogViewModel.ILoaiMenu = ILoaiMenu;
            NHHopDongTrongNuocDialogViewModel.Init();
            NHHopDongTrongNuocDialogViewModel.SavedAction = obj => this.OnRefresh();
            NHHopDongTrongNuocDialogViewModel.ShowDialog();
        }

        protected override void OnDieuChinh()
        {
            NHHopDongTrongNuocDialogViewModel.Model = SelectedItem;
            NHHopDongTrongNuocDialogViewModel.IsDieuChinh = true;
            NHHopDongTrongNuocDialogViewModel.IsAdd = false;
            NHHopDongTrongNuocDialogViewModel.IsDetail = false;
            NHHopDongTrongNuocDialogViewModel.ILoai = ILoai;
            NHHopDongTrongNuocDialogViewModel.IThuocMenu = IThuocMenu;
            NHHopDongTrongNuocDialogViewModel.IsShowDuAn = IsShowDuAn;
            NHHopDongTrongNuocDialogViewModel.ILoaiMenu = ILoaiMenu;
            NHHopDongTrongNuocDialogViewModel.IsVisibleTiGiaNhap = true;
            NHHopDongTrongNuocDialogViewModel.HopDongDieuChinhId = SelectedItem.Id;
            NHHopDongTrongNuocDialogViewModel.Init();
            NHHopDongTrongNuocDialogViewModel.SavedAction = obj => this.OnRefresh();
            NHHopDongTrongNuocDialogViewModel.ShowDialog();
        }

        private void OnViewAttachment()
        {
            if (base.SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_DA_HOPDONG;
                AttachmentViewModel.ObjectId = base.SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var data = _nhDaHopDongGoiThauNhaThauService.FindByIdHopDong(SelectedItem.Id);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        if (item != null)
                        {
                            var listHangMuc = _nhDaGoiThauHangMucSerrvice.FindByGoiThauId(item.IIdGoiThauId.Value);
                            listHangMuc = listHangMuc.Where(x => (x.IsCheck == 1 && x.IIDGoiThauCheck == item.Id));
                            if (listHangMuc != null)
                            {
                                foreach (var items in listHangMuc)
                                {
                                    NhDaGoiThauHangMuc itemsave = _nhDaGoiThauHangMucSerrvice.FindHangMucById(items.Id);
                                    itemsave.IIDGoiThauCheck = null;
                                    itemsave.IsCheck = 0;
                                    _nhDaGoiThauHangMucSerrvice.Update(itemsave);
                                }
                            }
                        }
                    }

                }
                _nhDaHopDongService.DeleteHopDong(SelectedItem.Id);
                LoadData();
            }
            else if (dialogResult == DialogResult.No)
            {
                LoadData();
            }
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKhTongTheNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKhTongTheNhiemVuChiId != null ? n.First().IIdKhTongTheNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        private void LoadDuAn()
        {
            try
            {
                if (Items == null) return;
                ItemsDuAn = new ObservableCollection<ComboboxItem>(Items.GroupBy(g => g.IIdDuAnId).Select(n => new ComboboxItem() { ValueItem = n.First().Id.ToString(), Id = n.First().IIdDuAnId != null ? n.First().IIdDuAnId.Value : Guid.Empty, DisplayItem = n.First().STenDuAn }));
                OnPropertyChanged(nameof(ItemsDuAn));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}