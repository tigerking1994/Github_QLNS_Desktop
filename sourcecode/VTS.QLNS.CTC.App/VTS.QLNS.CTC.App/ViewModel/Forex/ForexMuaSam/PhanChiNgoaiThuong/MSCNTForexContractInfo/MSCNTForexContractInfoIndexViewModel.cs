using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo.MSCNTImportHopDongNgoaiThuong;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo
{
    public class MSCNTForexContractInfoIndexViewModel : GridViewModelBase<NhDaHopDongModel>
    {
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly INhDmLoaiHopDongService _nhDmLoaiHopDongService;
        private readonly INhDmNhaThauService _nhDmNhaThauService;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private ICollectionView _dataIndexFilter;
        private readonly IExportService _exportService;

        public override string Name => "Hợp đồng ngoại thương";
        public override string Description => "Danh sách Hợp đồng ngoại thương";
        public override Type ContentType => typeof(View.Forex.ForexMuaSam.PhanChiNgoaiThuong.MSCNTForexContractInfo.ForexContractInfoIndex);
        public int ILoai { get; set; }
        public int IThuocMenu { get; set; }

        private string _sMoTaChiTietKhttcp;
        public string SMoTaChiTietKhttcp
        {
            get => _sMoTaChiTietKhttcp;
            set => SetProperty(ref _sMoTaChiTietKhttcp, value);
        }

        private ObservableCollection<ComboboxItem> _dataContractType;
        public ObservableCollection<ComboboxItem> DataContractType
        {
            get => _dataContractType;
            set => SetProperty(ref _dataContractType, value);
        }

        private ComboboxItem _selectedContractType;
        public ComboboxItem SelectedContractType
        {
            get => _selectedContractType;
            set => SetProperty(ref _selectedContractType, value);
        }

        public bool IsEdit => SelectedItem != null && !SelectedItem.BIsKhoa && SelectedItem.BIsActive;
        public bool IsLock => SelectedItem != null && SelectedItem.BIsKhoa;
        public string SPlanOverviewType { get; set; }
        private MSCNTImportHopDongNgoaiThuongViewModel ImportHopDongNgoaiThuongViewModel { get; set; }

        private NhDaHopDongFilterModel _nhDaHopDongFilterModel;
        public NhDaHopDongFilterModel NhDaHopDongFilter
        {
            get => _nhDaHopDongFilterModel;
            set => SetProperty(ref _nhDaHopDongFilterModel, value);
        }
        public RelayCommand DieuChinhCommand { get; }
        public RelayCommand ShowAddNewForexContractInfoDialogCommand { get; set; }
        public RelayCommand ShowUpdatePlanOverviewDialogCommand { get; set; }
        public RelayCommand FixPlanOverviewCommand { get; set; }
        public RelayCommand LockUnlockCommand { get; set; }
        public RelayCommand UnlockCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetFilterCommand { get; set; }
        public RelayCommand ViewAttachmentCommand { get; set; }
        public RelayCommand DowloadTemplateCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand ExportCucTaiChinhCommand { get; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public MSCNTForexContractInfoDialogViewModel ForexContractInfoDialogViewModel { get; set; }
        private ObservableCollection<ComboboxItem> _itemsLoaiHopDong;
        public ObservableCollection<ComboboxItem> ItemsLoaiHopDong
        {
            get => _itemsLoaiHopDong;
            set => SetProperty(ref _itemsLoaiHopDong, value);
        }
        private ObservableCollection<ComboboxItem> _itemsLoaiNhiemVuChi;
        public ObservableCollection<ComboboxItem> ItemsLoaiNhiemVuChi
        {
            get => _itemsLoaiNhiemVuChi;
            set => SetProperty(ref _itemsLoaiNhiemVuChi, value);
        }

        private ComboboxItem _selectedLoaiNhiemVuChi;
        public ComboboxItem SelectedLoaiNhiemVuChi
        {
            get => _selectedLoaiNhiemVuChi;
            set => SetProperty(ref _selectedLoaiNhiemVuChi, value);
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

        private Dictionary<Guid, NhDmLoaiHopDong> _dicLoaiHopDong;
        private Dictionary<Guid, NhDmNhaThau> _dicDmNhaThau;
        public MSCNTForexContractInfoIndexViewModel(
            AttachmentViewModel attachmentViewModel,
            INhDaHopDongService nhDaHopDongService,
            INhDmLoaiHopDongService nhDmLoaiHopDongService,
            INhDmNhaThauService nhDmNhaThauService,
            MSCNTImportHopDongNgoaiThuongViewModel importHopDongNgoaiThuongViewModel,
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            MSCNTForexContractInfoDialogViewModel forexContractInfoDialogViewModel,
            IExportService exportService
            )
        {
            ImportHopDongNgoaiThuongViewModel = importHopDongNgoaiThuongViewModel;
            AttachmentViewModel = attachmentViewModel;
            _nhDaHopDongService = nhDaHopDongService;
            _nhDmLoaiHopDongService = nhDmLoaiHopDongService;
            _nhDmNhaThauService = nhDmNhaThauService;
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;
            _exportService = exportService;
            ForexContractInfoDialogViewModel = forexContractInfoDialogViewModel;
            ForexContractInfoDialogViewModel.ParentPage = this;
            UpdateCommand = new RelayCommand(obj => OnUpdate(), obj => IsEdit);
            DieuChinhCommand = new RelayCommand(obj => OnDieuChinh(), obj => IsEdit);
            ShowAddNewForexContractInfoDialogCommand = new RelayCommand(obj => OnAdd());
            SearchCommand = new RelayCommand(obj => OnSearch());
            LockUnlockCommand = new RelayCommand(obj => OnLockUnLock());
            ResetFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment());
            ImportDataCommand = new RelayCommand(obj => OnImportHopDongNgoaiThuongData());
            DowloadTemplateCommand = new RelayCommand(obj => DowloadForm());
            ExportCommand = new RelayCommand(o => OnExport(1));
            ExportCucTaiChinhCommand = new RelayCommand(o => OnExport(2));
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            NhDaHopDongFilter = new NhDaHopDongFilterModel();
            LoadContractType();
            LoadLoaiNhiemVuChi();
            OnLoadLoaiHopDong();
            LoadData();
        }
        public void OnImportHopDongNgoaiThuongData()
        {
            ImportHopDongNgoaiThuongViewModel.Init();
            ImportHopDongNgoaiThuongViewModel.SavedAction = obj =>
            {
                this.LoadData();
            };
            ImportHopDongNgoaiThuongViewModel.ShowDialog();
        }
        private void LoadData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // Main process
                Items = new ObservableCollection<NhDaHopDongModel>();
                e.Result = _nhDaHopDongService.FindAllHopDongNgoaiThuong(IThuocMenu).Where(x => x.ILoai == 1).ToList();
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    Items = _mapper.Map<ObservableCollection<NhDaHopDongModel>>(e.Result);
                    // Process when run completed. e.Result
                    if (Items != null && Items.Count > 0)
                    {
                        SelectedItem = Items.FirstOrDefault();

                        List<NhDmLoaiHopDong> listContractType = _nhDmLoaiHopDongService.FindAll();
                        foreach (var item in Items)
                        {
                            var idLoaiHopDong = item.IIdLoaiHopDongId;
                            if (idLoaiHopDong != null)
                            {
                                item.SLoaiHopDong = listContractType.Where(n=> n.IIdLoaiHopDongId == idLoaiHopDong).Select(n => n.STenLoaiHopDong).FirstOrDefault();
                            }
                        }
                    }
                    _dataIndexFilter = CollectionViewSource.GetDefaultView(Items);
                    _dataIndexFilter.Filter = DataFilter;
                    LoadChuongTrinh();
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        protected override void OnAdd()
        {
            ForexContractInfoDialogViewModel.IsInsert = true;
            ForexContractInfoDialogViewModel.IsReadOnly = false;
            ForexContractInfoDialogViewModel.Model = new NhDaHopDongModel();
            ForexContractInfoDialogViewModel.ILoai = ILoai;
            ForexContractInfoDialogViewModel.IThuocMenu = IThuocMenu;
            ForexContractInfoDialogViewModel.Init();
            ForexContractInfoDialogViewModel.SavedAction = obj => this.OnRefresh();
            ForexContractInfoDialogViewModel.ShowDialog();
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private bool DataFilter(object obj)
        {
            bool result = true;
            var item = (NhDaHopDongModel)obj;
            if (!string.IsNullOrEmpty(NhDaHopDongFilter.SSoHopDong))
            {
                result = !string.IsNullOrEmpty(item.SSoHopDong) && item.SSoHopDong.ToLower().Contains(NhDaHopDongFilter.SSoHopDong.ToLower());
            }
            if (!string.IsNullOrEmpty(NhDaHopDongFilter.STenHopDong))
            {
                result = !string.IsNullOrEmpty(item.STenHopDong) && item.SSoHopDong.ToLower().Contains(NhDaHopDongFilter.STenHopDong.ToLower());
            }
            if (!string.IsNullOrEmpty(NhDaHopDongFilter.SLoaiHopDong))
            {
                result = !string.IsNullOrEmpty(item.SLoaiHopDong) && item.SLoaiHopDong.ToLower().Contains(NhDaHopDongFilter.SLoaiHopDong.ToLower());
            }
            if (SelectedContractType != null)
            {
                result &= result && item.SLoaiHopDong == SelectedContractType.DisplayItem;
            }
            if (SelectedLoaiNhiemVuChi != null)
            {
                result &= result && item.SLoaiNhiemVuChi == SelectedLoaiNhiemVuChi.DisplayItem;
            }
            if (NhDaHopDongFilter.DNgayHopDong.HasValue)
            {
                result = result && item.DNgayHopDong.HasValue && (item.DNgayHopDong.Value == NhDaHopDongFilter.DNgayHopDong.Value);
            }
            if (NhDaHopDongFilter.DKhoiCongDuKien.HasValue)
            {
                result = result && item.DKhoiCongDuKien.HasValue && (item.DKhoiCongDuKien.Value == NhDaHopDongFilter.DKhoiCongDuKien.Value);
            }
            if (NhDaHopDongFilter.DKetThucDuKien.HasValue)
            {
                result = result && item.DKetThucDuKien.HasValue && (item.DKetThucDuKien.Value == NhDaHopDongFilter.DKetThucDuKien.Value);
            }
            if (SelectedChuongTrinh != null)
            {
                result &= result && item.IIdKhTongTheNhiemVuChiId == SelectedChuongTrinh.Id;
            }
            return result;
        }

        private void OnRemoveFilter()
        {
            NhDaHopDongFilter.SSoHopDong = string.Empty;
            NhDaHopDongFilter.STenHopDong = string.Empty;
            NhDaHopDongFilter.DNgayHopDong = null;
            NhDaHopDongFilter.DKhoiCongDuKien = null;
            NhDaHopDongFilter.DKetThucDuKien = null;
            SelectedLoaiNhiemVuChi = null;
            SelectedContractType = null;
            LoadData();
        }
        public void DowloadForm()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;

                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.RPT_NH_DANHMUC_HOPDONGNT);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    var xlsFile = _exportService.Export<NhDaHopDongModel>(templateFileName, new Dictionary<string, object>());
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
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
        private void OnSearch()
        {
            _dataIndexFilter.Refresh();
        }


        public bool? IsAllItemsSelected
        {
            get
            {
                if (Items != null)
                {
                    var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                    return selected.Count == 1 ? selected.Single() : (bool?)null;
                }
                return false;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged();
                }
            }
        }

        private static void SelectAll(bool select, IEnumerable<NhDaHopDongModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(IsEdit));
            OnPropertyChanged(nameof(IsLock));
        }

        protected override void OnLockUnLock()
        {
            string message = IsLock ? Resources.ConfirmUnLockGroups : Resources.ConfirmLockGroups;
            var result = System.Windows.MessageBox.Show(message, Resources.ConfirmTitle, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                LockConfirmEventHandler();

        }

        private void LockConfirmEventHandler()
        {
            // call service to lock , unlock item in DB and reload data table !
            _nhDaHopDongService.LockOrUnlock(SelectedItem.Id, !SelectedItem.BIsKhoa);
            SelectedItem.BIsKhoa = !SelectedItem.BIsKhoa;
            OnPropertyChanged(nameof(IsLock));
            OnPropertyChanged(nameof(IsEdit));
            OnRefresh();

        }

        private void LoadContractType()
        {
            var lstContractType = _nhDmLoaiHopDongService.FindAll();
            if (lstContractType == null) return;
            var drpItem = lstContractType.Select(n => new ComboboxItem()
            {
                ValueItem = n.IIdLoaiHopDongId.ToString(),
                DisplayItem = (n.STenLoaiHopDong),
                Id = n.IIdLoaiHopDongId
            });
            _dataContractType = _mapper.Map<ObservableCollection<ComboboxItem>>(drpItem);
            OnPropertyChanged(nameof(DataContractType));
        }

        private void LoadLoaiNhiemVuChi()
        {
            var drpItem = new ObservableCollection<ComboboxItem>();
            drpItem.Add(new ComboboxItem() { ValueItem = "1", DisplayItem = "Nhiệm vụ chi mua sắm" });
            drpItem.Add(new ComboboxItem() { ValueItem = "2", DisplayItem = "Nhiệm vụ chi dự án" });
            _itemsLoaiNhiemVuChi = drpItem;
            OnPropertyChanged(nameof(ItemsLoaiNhiemVuChi));
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

        protected override void OnUpdate()
        {
            if (SelectedItem != null)
            {
                ForexContractInfoDialogViewModel.IsDieuChinh = false;
                ForexContractInfoDialogViewModel.IsReadOnly = false;
                ForexContractInfoDialogViewModel.IsInsert = false;
                ForexContractInfoDialogViewModel.Model = SelectedItem;
                ForexContractInfoDialogViewModel.ILoai = ILoai;
                ForexContractInfoDialogViewModel.IThuocMenu = IThuocMenu;
                ForexContractInfoDialogViewModel.Init();
                ForexContractInfoDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexContractInfoDialogViewModel.ShowDialog();
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem == null) return;

            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(msgConfirm, Resources.ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // Nếu là xóa bản ghi điều chỉnh thì bản ghi gốc sẽ được update bactive = 1
                if (SelectedItem.IIdParentAdjustId.HasValue)
                {
                    NhDaHopDong hopdongParent = _nhDaHopDongService.FindById(SelectedItem.IIdParentAdjustId.Value);
                    if (hopdongParent != null)
                    {
                        hopdongParent.BIsActive = true;
                        _nhDaHopDongService.Update(hopdongParent);
                    }
                }
                _nhDaHopDongService.DeleteHopDong(SelectedItem.Id);
                OnRefresh();
            }
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            if (SelectedItem != null)
            {
                ForexContractInfoDialogViewModel.IsReadOnly = true;
                ForexContractInfoDialogViewModel.IsDieuChinh = false;
                ForexContractInfoDialogViewModel.IsInsert = false;
                ForexContractInfoDialogViewModel.Model = SelectedItem;
                ForexContractInfoDialogViewModel.ILoai = ILoai;
                ForexContractInfoDialogViewModel.IThuocMenu = IThuocMenu;
                ForexContractInfoDialogViewModel.Init();
                ForexContractInfoDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexContractInfoDialogViewModel.ShowDialog();
            }
        }

        private void OnDieuChinh()
        {
            if (SelectedItem != null)
            {
                ForexContractInfoDialogViewModel.IsDieuChinh = true;
                ForexContractInfoDialogViewModel.IsReadOnly = false;
                ForexContractInfoDialogViewModel.IsInsert = false;
                ForexContractInfoDialogViewModel.IdDieuChinh = SelectedItem.Id;
                ForexContractInfoDialogViewModel.Model = SelectedItem;
                ForexContractInfoDialogViewModel.ILoai = ILoai;
                ForexContractInfoDialogViewModel.IThuocMenu = IThuocMenu;
                ForexContractInfoDialogViewModel.Init();
                ForexContractInfoDialogViewModel.SavedAction = obj => this.OnRefresh();
                ForexContractInfoDialogViewModel.ShowDialog();
            }
        }

        private void OnLoadLoaiHopDong()
        {
            _dicLoaiHopDong = new Dictionary<Guid, NhDmLoaiHopDong>();
            var datas = _nhDaHopDongService.GetAllLoaiHopDong();
            if (datas != null)
            {
                foreach (var item in datas)
                {
                    if (!_dicLoaiHopDong.ContainsKey(item.Id))
                        _dicLoaiHopDong.Add(item.Id, item);
                }
            }

            // danh muc nha thau

            _dicDmNhaThau = new Dictionary<Guid, NhDmNhaThau>();
            var dataNhaThau = _nhDmNhaThauService.FindAll();
            if (dataNhaThau == null) return;
            foreach (var item in dataNhaThau)
            {
                if (!_dicDmNhaThau.ContainsKey(item.Id))
                    _dicDmNhaThau.Add(item.Id, item);
            }
        }

        private void OnExport(int icheck)
        {
            // 1 xuat bao cao thuong 
            // 2 xuat bao cao cho cuc tai chinh
            if (!Items.Any(n => n.IsSelected))
            {
                MessageBoxHelper.Error(Resources.MsgChooseItem);
                return;
            }
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                var lstHopDong = Items.Where(x => x.IsSelected).ToList();
                var iSTT = 0;
                foreach (var item in lstHopDong)
                {
                    iSTT++;
                    item.STT = iSTT.ToString();
                    if (item.IIdLoaiHopDongId.HasValue && _dicLoaiHopDong.ContainsKey(item.IIdLoaiHopDongId.Value))
                        item.SMaLoaiHopDong = _dicLoaiHopDong[item.IIdLoaiHopDongId.Value].SMaLoaiHopDong;

                    if (item.IIdNhaThauThucHienId.HasValue && _dicLoaiHopDong.ContainsKey(item.IIdNhaThauThucHienId.Value))
                        item.SMaNhaThauThucHien = _dicDmNhaThau[item.IIdNhaThauThucHienId.Value].SMaNhaThau;
                }
                List<ExportResult> results = new List<ExportResult>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", lstHopDong);
                data.Add("ItemsLoaiHD", _dicLoaiHopDong.Values);

                string templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, icheck == 1 ? ExportFileName.RPT_FOREX_CONTRACT : ExportFileName.RPT_Hop_Dong_Ngoai_Thuong_CucTaiChinh);
                string fileNamePrefix = string.Format("{0}", icheck == 1 ? ExportFileName.RPT_FOREX_CONTRACT : ExportFileName.RPT_Hop_Dong_Ngoai_Thuong_CucTaiChinh);
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                if (icheck == 2)
                {
                    data.Add("ItemsDmNhaThau", _dicDmNhaThau.Values);
                    var xlsFile = _exportService.Export<NhDaHopDongModel, NhDmLoaiHopDong, NhDmNhaThau>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }
                else
                {
                    var xlsFile = _exportService.Export<NhDaHopDongModel, NhDmLoaiHopDong>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }
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
                _logger.Error(ex.Message, ex);
            }
        }
    }
}

