using AutoMapper;
using FlexCel.Core;
using FlexCel.XlsAdapter;
using log4net;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.NHHopDongTrongNuoc.ImportHopDong;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamNgoaiThuong.MSNTNHHopDongTrongNuoc.MSNTImportHopDong;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau.MSTNImportHopDongKhongGoiThau;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.MuaSamTrongNuoc.MSTNHopDongTrongNuocKhongGoiThau
{
    public class MSTNHopDongTrongNuocKhongGoiThauIndexViewModel : GridViewModelBase<NhDaHopDongModel>
    {
        private readonly ILogger<MSTNHopDongTrongNuocKhongGoiThauIndexViewModel> _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDaHopDongHangMucService _nhDaHopDongHangMucService;
        private readonly INhDmLoaiHopDongService _nhdmLoaiHopDongService;
        private readonly INhMstnKeHoachDatHangService _nhKhDatHangService;
        private readonly IExportService _exportService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private ICollectionView _itemsCollectionView;

        public override string GroupName => MenuItemContants.GROUP_FOREX_KHONG_HINH_THANH_GOITHAU;
        public override string Name => "Hợp đồng trong nước";
        public override string Title => "Quản lý hợp đồng trong nước";
        public override string Description => "Danh sách hợp đồng trong nước";
        public override Type ContentType => typeof(HopDongTrongNuocKhongGoiThauIndex);

        private ObservableCollection<DonViModel> _itemsDonVi;
        public ObservableCollection<DonViModel> ItemsDonVi
        {
            get => _itemsDonVi;
            set => SetProperty(ref _itemsDonVi, value);
        }

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private NhDaHopDongModel _itemsFilter;
        public NhDaHopDongModel ItemsFilter
        {
            get => _itemsFilter;
            set => SetProperty(ref _itemsFilter, value);
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

        public bool IsEditable => SelectedItem != null && SelectedItem.BIsActive;

        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }
        public object TemplateFileName { get; private set; }

        public MSTNHopDongTrongNuocKhongGoiThauDialogViewModel MSTNHopDongTrongNuocKhongGoiThauDialogViewModel { get; set; }
        public MSTNImportHopDongKhongGoiThauViewModel ImportHopDongViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportDataCommand { get; }
        public RelayCommand ExportDataCTCCommand { get; }
        public RelayCommand CheckRowCommand { get; private set; }
        public RelayCommand CheckAllRowsCommand { get; private set; }
        public RelayCommand OpenPopupExcelCommand { get; }

        public MSTNHopDongTrongNuocKhongGoiThauIndexViewModel(
            ILogger<MSTNHopDongTrongNuocKhongGoiThauIndexViewModel> logger,
            IMapper mapper,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            INhDaHopDongService nhDaHopDongService,
            INhDaHopDongHangMucService nhDaHopDongHangMucService,
            IExportService exportService,
            INhDmLoaiHopDongService nhdmLoaiHopDongService,
            INhMstnKeHoachDatHangService nhKhDatHangService,
            INhDmNhaThauService nhDmNhaThauService,
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel mstnHopDongTrongNuocKhongGoiThauDialogViewModel,
            MSTNImportHopDongKhongGoiThauViewModel importHopDongViewModel)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDaHopDongHangMucService = nhDaHopDongHangMucService;
            _exportService = exportService;
            _nhdmLoaiHopDongService = nhdmLoaiHopDongService;
            _nhKhDatHangService = nhKhDatHangService;
            _nhDmNhaThauService = nhDmNhaThauService;

            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel = mstnHopDongTrongNuocKhongGoiThauDialogViewModel;
            ImportHopDongViewModel= importHopDongViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            UpdateCommand = new RelayCommand(o => OnUpdate(), obj => IsEditable);
            DeleteCommand = new RelayCommand(o => OnDelete(), obj => IsEditable);
            DieuChinhCommand = new RelayCommand(o => OnDieuChinh(), obj => IsEditable);
            OpenPopupExcelCommand = new RelayCommand(obj => IsOpenExcelPopup = true);
            ImportDataCommand = new RelayCommand(obj => OnImportData());
            ExportDataCommand = new RelayCommand(obj => OnExportData(1), obj => Items.Any(x => x.IsChecked));
            ExportDataCTCCommand = new RelayCommand(obj => OnExportData(2), obj => Items.Any(x => x.IsChecked));
            CheckRowCommand = new RelayCommand(o => OnCheckRow());
            CheckAllRowsCommand = new RelayCommand(o => OnCheckAllRows());
        }

        public override void Init()
        {
            LoadDefault();
            LoadDonVi();
            LoadData();
        }

        private void LoadDefault()
        {
            ItemsFilter = new NhDaHopDongModel();
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
                    string fileNameExport = typeExport == 1 ? ExportFileName.PRT_NH_HOPDONGTRONGNUOC_KHONGGOITHAU : ExportFileName.RPT_Hop_Dong_Ngoai_Thuong_CucTaiChinh;
                    string prefixName = typeExport == 1 ? ExportPrefix.PATH_NH_MUASAM : ExportPrefix.PATH_NH_DUAN;
                    templateFileName = Path.Combine(prefixName, fileNameExport);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));

                    List<NhDmLoaiHopDong> dataLoaiHopDongs = _nhdmLoaiHopDongService.FindAll();
                    ObservableCollection<NhDmLoaiHopDongModel> loaiHopDongList = _mapper.Map<ObservableCollection<NhDmLoaiHopDongModel>>(dataLoaiHopDongs);
                    ObservableCollection<NhDaHopDongModel> nhDaHopDongs = new ObservableCollection<NhDaHopDongModel>(Items.Where(x => x.IsChecked));
                    int i = 1;
                    nhDaHopDongs.ForAll(x =>
                    {
                        x.STT = i.ToString();
                        i++;
                    });

                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "Items", nhDaHopDongs }
                    };

                    if (typeExport == 1)
                    {
                        IEnumerable<NhMstnKeHoachDatHangQuery> dataNhKHDHs = _nhKhDatHangService.GetAllMstnKeHoachDatHangIndex().Where(x => x.BIsActive);
                        ObservableCollection<NhMstnKeHoachDatHangModel> khDatHangList = _mapper.Map<ObservableCollection<NhMstnKeHoachDatHangModel>>(dataNhKHDHs);
                        data.Add("ItemsLoaiHopDong", loaiHopDongList);
                        data.Add("ItemsKeHoachDatHang", khDatHangList);
                        ExcelFile xlsFile = _exportService.Export<NhDaHopDongModel, NhDmLoaiHopDongModel, NhMstnKeHoachDatHangModel>(templateFileName, data);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                    else
                    {
                        IEnumerable<NhDmNhaThau> dataNhaThaus = _nhDmNhaThauService.FindAll();
                        ObservableCollection<NhDmNhaThauModel> lstNhaThau = _mapper.Map<ObservableCollection<NhDmNhaThauModel>>(dataNhaThaus);
                        data.Add("ItemsLoaiHD", loaiHopDongList);
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
            var yearOfWork = _sessionService.Current.YearOfWork;
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
                    e.Result = _nhDaHopDongService.FindAllHopDong(IThuocMenu).Where(x => x.ILoai == ILoai).OrderByDescending(x => x.DNgayTao);
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
                    if (!string.IsNullOrEmpty(ItemsFilter.STenHopDong) && !string.IsNullOrEmpty(item.STenHopDong))
                    {
                        result &= item.STenHopDong.Contains(ItemsFilter.STenHopDong, StringComparison.OrdinalIgnoreCase);
                    }
                    if (ItemsFilter.DNgayHopDong != null)
                    {
                        result &= ItemsFilter.DNgayHopDong.Value == item.DNgayHopDong.Value;
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
                return result;
            }
            return false;
        }

        protected override void OnRefresh()
        {
            LoadData();
        }

        private void OnRemoveFilter()
        {
            SelectedDonVi = null;
            ItemsFilter = new NhDaHopDongModel();
            OnRefresh();
        }

        protected override void OnAdd()
        {
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Model = new NhDaHopDongModel();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDetail = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDieuChinh = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.HopDongDieuChinhId = Guid.Empty;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ILoai = ILoai;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IThuocMenu = IThuocMenu;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Init();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Model = SelectedItem;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDetail = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDieuChinh = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.HopDongDieuChinhId = Guid.Empty;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ILoai = ILoai;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IThuocMenu = IThuocMenu;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Init();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Model = SelectedItem;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDetail = true;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDieuChinh = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.HopDongDieuChinhId = Guid.Empty;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ILoai = ILoai;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IThuocMenu = IThuocMenu;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Init();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ShowDialog();
        }

        protected override void OnDieuChinh()
        {
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Model = SelectedItem;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDetail = false;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IsDieuChinh = true;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.HopDongDieuChinhId = SelectedItem.Id;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ILoai = ILoai;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.IThuocMenu = IThuocMenu;
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.Init();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.SavedAction = obj => this.OnRefresh();
            MSTNHopDongTrongNuocKhongGoiThauDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                _nhDaHopDongHangMucService.DeleteByIdHopDong(SelectedItem.Id);
                if (SelectedItem.IIdParentAdjustId != null)
                {
                    var hopDongGoc = _nhDaHopDongService.FindById(SelectedItem.IIdParentAdjustId);
                    if (hopDongGoc != null)
                    {
                        hopDongGoc.BIsActive = true;
                        _nhDaHopDongService.Update(hopDongGoc);
                    }
                }
                _nhDaHopDongService.DeleteHopDong(SelectedItem.Id);
                LoadData();
            }
        }
    }
}
