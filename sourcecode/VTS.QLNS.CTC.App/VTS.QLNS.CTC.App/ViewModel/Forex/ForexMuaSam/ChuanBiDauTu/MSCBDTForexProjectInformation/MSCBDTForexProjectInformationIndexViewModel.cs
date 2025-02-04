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
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation.MSCBDTImportThongTinDuAn;
using VTS.QLNS.CTC.App.ViewModel.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexMuaSam.ChuanBiDauTu.MSCBDTForexProjectInformation 
{ 
    public class MSCBDTForexProjectInformationIndexViewModel : GridViewModelBase<NhDaDuAnModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaDuAnNguonVonService _nhDaDuAnNguonVonService;
        private readonly INhDaDuAnHangMucService _nhDaDuAnHangMucService;
        private readonly INsDonViService _nsDonViService;
        private readonly IDmChuDauTuService _dmChuDauTuService;
        private readonly INhDmPhanCapPheDuyetService _nhDmPhanCapPheDuyetService;
        private readonly INhDmLoaiCongTrinhService _nhDmLoaiCongTrinhService;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IExportService _exportService;
        private ICollectionView _itemsCollectionView;
        private Dictionary<Guid, NhDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private List<NhDmLoaiCongTrinh> _lstLoaiCongTrinh;
        private List<NsNguonNganSach> _lstNguonVon;
        public bool IsEnabledExport => Items != null && Items.Any(x => x.IsChecked);

        public override string GroupName => MenuItemContants.GROUP_FOREX_PREPARE_TO_INVEST;
        public override string Name => "Thông tin dự án";
        public override Type ContentType => typeof(ForexProjectInformationIndex);
        public override string Title => "Thông tin dự án";
        public override string Description => "Danh sách thông tin dự án";
        public int ILoai { get; set; }

        private NhDaDuAnModel _itemsFilter;
        public NhDaDuAnModel ItemsFilter
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

        private DonViModel _selectedDonVi;
        public DonViModel SelectedDonVi
        {
            get => _selectedDonVi;
            set => SetProperty(ref _selectedDonVi, value);
        }

        private ObservableCollection<DmChuDauTuModel> _itemsChuDauTu;
        public ObservableCollection<DmChuDauTuModel> ItemsChuDauTu
        {
            get => _itemsChuDauTu;
            set => SetProperty(ref _itemsChuDauTu, value);
        }

        private DmChuDauTuModel _selectedChuDauTu;
        public DmChuDauTuModel SelectedChuDauTu
        {
            get => _selectedChuDauTu;
            set => SetProperty(ref _selectedChuDauTu, value);
        }

        private ObservableCollection<NhDmPhanCapPheDuyetModel> _itemsPhanCapPheDuyet;
        public ObservableCollection<NhDmPhanCapPheDuyetModel> ItemsPhanCapPheDuyet
        {
            get => _itemsPhanCapPheDuyet;
            set => SetProperty(ref _itemsPhanCapPheDuyet, value);
        }

        private NhDmPhanCapPheDuyetModel _selectedPhanCapPheDuyet;
        public NhDmPhanCapPheDuyetModel SelectedPhanCapPheDuyet
        {
            get => _selectedPhanCapPheDuyet;
            set => SetProperty(ref _selectedPhanCapPheDuyet, value);
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

        private MSCBDTForexProjectInformationDialogViewModel ForexProjectInformationDialogViewModel { get; }
        private MSCBDTImportThongTinDuAnViewModel ImportThongTinDuAnViewModel { get; set; }
        public AttachmentViewModel AttachmentViewModel { get; set; }

        public RelayCommand SearchCommand { get; }
        public RelayCommand ViewAttachmentCommand { get; }
        public RelayCommand ImportDataCommand { get; }
        public RelayCommand ExportTemplateCommand { get; }
        public RelayCommand ExportTemplateCTCCommand { get; }
        public RelayCommand RemoveFilterCommand { get; }
        public MSCBDTForexProjectInformationIndexViewModel(
            IMapper mapper,
            ISessionService sessionService,
            ILog logger,
            INhDaDuAnService nhDaDuAnService,
            INhDaDuAnNguonVonService nhDaDuAnNguonVonService,
            INhDaDuAnHangMucService nhDaDuAnHangMucService,
            MSCBDTForexProjectInformationDialogViewModel forexProjectInformationDialogViewModel,
            MSCBDTImportThongTinDuAnViewModel importThongTinDuAnViewModel,
            AttachmentViewModel attachmentViewModel,
            INsDonViService nsDonViService,
            IDmChuDauTuService dmChuDauTuService,
            INhDmPhanCapPheDuyetService nhDmPhanCapPheDuyetService,
            INsNguonNganSachService nsNguonNganSachService,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            IExportService exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaDuAnNguonVonService = nhDaDuAnNguonVonService;
            _nhDaDuAnHangMucService = nhDaDuAnHangMucService;
            _nsDonViService = nsDonViService;
            _dmChuDauTuService = dmChuDauTuService;
            _nhDmPhanCapPheDuyetService = nhDmPhanCapPheDuyetService;
            _nsNguonNganSachService = nsNguonNganSachService;
            _nhDmLoaiCongTrinhService = nhDmLoaiCongTrinhService;
            _exportService = exportService;

            ImportThongTinDuAnViewModel = importThongTinDuAnViewModel;

            SearchCommand = new RelayCommand(obj => _itemsCollectionView.Refresh());
            ViewAttachmentCommand = new RelayCommand(obj => OnViewAttachment(), obj => SelectedItem != null && SelectedItem.TotalFiles > 0);
            ExportTemplateCommand = new RelayCommand(obj => OnExportThongTinDuAnTemplate());
            ExportTemplateCTCCommand = new RelayCommand(obj => OnExportThongTinDuAnCTCTemplate());
            RemoveFilterCommand = new RelayCommand(obj => OnRemoveFilter());
            ImportDataCommand = new RelayCommand(obj => OnImportThongTinDuAnData());
            ForexProjectInformationDialogViewModel = forexProjectInformationDialogViewModel;
            AttachmentViewModel = attachmentViewModel;
        }

        public override void Init()
        {
            base.Init();
            LoadDefault();
            LoadDonVi();
            LoadLoaiCongTrinh();
            LoadNguonVon();
            LoadChuDauTu();
            LoadPhanCapPheDuyet();
            LoadData();
        }
        private void LoadDefault()
        {
            ItemsFilter = new NhDaDuAnModel();
        }

        private void OnRemoveFilter()
        {
            ItemsFilter = new NhDaDuAnModel();
            SelectedDonVi = null;
            SelectedChuDauTu = null;
            SelectedPhanCapPheDuyet = null;
            LoadData();
        }
        private void LoadDonVi()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var data = _nsDonViService.FindByCondition(x => x.NamLamViec == yearOfWork);
            _itemsDonVi = _mapper.Map<ObservableCollection<DonViModel>>(data);
            OnPropertyChanged(nameof(ItemsDonVi));
        }

        private void LoadChuDauTu()
        {
            var yearOfWork = _sessionService.Current.YearOfWork;
            var data = _dmChuDauTuService.FindByNamLamViec(yearOfWork);
            _itemsChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(data);
            OnPropertyChanged(nameof(ItemsChuDauTu));
        }

        private void LoadPhanCapPheDuyet()
        {
            var data = _nhDmPhanCapPheDuyetService.FindAll();
            _itemsPhanCapPheDuyet = _mapper.Map<ObservableCollection<NhDmPhanCapPheDuyetModel>>(data);
            OnPropertyChanged(nameof(ItemsPhanCapPheDuyet));
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    Items = new ObservableCollection<NhDaDuAnModel>();
                    e.Result = _nhDaDuAnService.FindIndex(1).OrderByDescending(x => x.DNgayTao);
                }, (s, e) =>
                 {
                     if (e.Error == null)
                     {
                         var data = _mapper.Map<ObservableCollection<NhDaDuAnModel>>(e.Result).Select(x =>
                         {
                             if (x.SKetThuc != null && x.SKhoiCong != null)
                             {
                                 x.SThoiGian = string.Concat(x.SKhoiCong, " - ", x.SKetThuc);
                             }
                             return x;
                         }).ToList();
                         Items = new ObservableCollection<NhDaDuAnModel>(data);
                         Items.ForAll(x =>
                         {
                             x.PropertyChanged += NhDaDuAn_PropertyChanged;
                         });
                         if (Items != null && Items.Count > 0)
                         {
                             SelectedItem = Items.FirstOrDefault();
                         }
                         _itemsCollectionView = CollectionViewSource.GetDefaultView(Items);
                         _itemsCollectionView.Filter = Items_Filter;
                         LoadChuongTrinh();
                     }
                 });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
        private void NhDaDuAn_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var item = (NhDaDuAnModel)sender;

            if (args.PropertyName == nameof(NhDaDuAnModel.IsChecked))
            {
                OnPropertyChanged(nameof(IsEnabledExport));
            }
        }

        private bool Items_Filter(object obj)
        {
            if (obj is NhDaDuAnModel item)
            {
                bool result = true;
                if (SelectedDonVi != null)
                {
                    result &= item.IIdDonViQuanLyId.Equals(SelectedDonVi.Id);
                }
                if (SelectedChuDauTu != null)
                {
                    result &= item.IIdChuDauTuId.Equals(SelectedChuDauTu.Id);
                }
                if (SelectedPhanCapPheDuyet != null)
                {
                    result &= item.IIdCapPheDuyetId.Equals(SelectedPhanCapPheDuyet.Id);
                }
                if (ItemsFilter != null)
                {
                    if (!string.IsNullOrEmpty(ItemsFilter.STenDuAn))
                    {
                        result &= item.STenDuAn != null && item.STenDuAn.Contains(ItemsFilter.STenDuAn, StringComparison.OrdinalIgnoreCase);
                    }
                    //if (ItemsFilter.SThoiGian != null && item.SThoiGian != null)
                    //{
                    //    result &= item.SThoiGian == ItemsFilter.SThoiGian;
                    //}
                    if (!string.IsNullOrEmpty(ItemsFilter.SThoiGian))
                    {
                        result &= item.SThoiGian != null && item.SThoiGian.Contains(ItemsFilter.SThoiGian, StringComparison.OrdinalIgnoreCase);
                    }
                }
                if (SelectedDonVi != null)
                {
                    result &= item.IIdDonViQuanLyId == SelectedDonVi.Id;
                }
                if (SelectedChuDauTu != null)
                {
                    result &= item.IIdChuDauTuId == SelectedChuDauTu.Id;
                }
                if (SelectedPhanCapPheDuyet != null)
                {
                    result &= item.IIdCapPheDuyetId == SelectedPhanCapPheDuyet.Id;
                }
                if (SelectedChuongTrinh != null)
                {
                    result &= item.IIdKhttNhiemVuChiId == SelectedChuongTrinh.Id;
                }
                return result;
            }
            return false;
        }

        protected override void OnAdd()
        {
            NhDaDuAnModel NhDaDuAn = new NhDaDuAnModel();
            ForexProjectInformationDialogViewModel.IsDetail = false;
            ForexProjectInformationDialogViewModel.Model = NhDaDuAn;
            ForexProjectInformationDialogViewModel.ILoai = ILoai;
            ForexProjectInformationDialogViewModel.SavedAction = obj => OnRefresh();
            ForexProjectInformationDialogViewModel.Init();
            ForexProjectInformationDialogViewModel.ShowDialog();
        }

        protected override void OnUpdate()
        {
            ForexProjectInformationDialogViewModel.Model = SelectedItem;
            ForexProjectInformationDialogViewModel.IsDetail = false;
            ForexProjectInformationDialogViewModel.ILoai = ILoai;
            ForexProjectInformationDialogViewModel.SavedAction = obj => OnRefresh();
            ForexProjectInformationDialogViewModel.Init();
            ForexProjectInformationDialogViewModel.ShowDialog();
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            ForexProjectInformationDialogViewModel.Model = SelectedItem;
            ForexProjectInformationDialogViewModel.IsDetail = true;
            ForexProjectInformationDialogViewModel.ILoai = ILoai;
            ForexProjectInformationDialogViewModel.Init();
            ForexProjectInformationDialogViewModel.ShowDialog();
        }

        protected override void OnDelete()
        {
            string msgConfirm = string.Format(Resources.ConfirmDeleteUsers);
            if (MessageBoxHelper.Confirm(msgConfirm) == MessageBoxResult.Yes)
            {
                if (SelectedItem != null)
                {
                    var nguonVon = _nhDaDuAnNguonVonService.FindByDuAnId(SelectedItem.Id);
                    if (!nguonVon.IsEmpty())
                    {
                        foreach (var item in nguonVon)
                        {
                            _nhDaDuAnNguonVonService.Delete(item);
                        }
                    }
                    var hangMuc = _nhDaDuAnHangMucService.FindByDuAnId(SelectedItem.Id);
                    if (!hangMuc.IsEmpty())
                    {
                        foreach (var item in hangMuc)
                        {
                            _nhDaDuAnHangMucService.Delete(item);
                        }
                    }
                    _nhDaDuAnService.Delete(_mapper.Map<NhDaDuAn>(SelectedItem));
                }
                OnRefresh();
            }
        }

        protected override void OnRefresh()
        {
            Init();
        }

        private void OnViewAttachment()
        {
            if (SelectedItem != null)
            {
                AttachmentViewModel.ModuleType = AttachmentEnum.Type.NH_DA_THONGTIN_DUAN;
                AttachmentViewModel.ObjectId = SelectedItem.Id;
                AttachmentViewModel.Init();
                AttachmentViewModel.ShowDialogHost();
            }
        }
        private List<NhDaThongTinDuAnExportModel> GetDataExport(List<Guid> lstDuAnId)
        {
            List<NhDaThongTinDuAnExportModel> results = new List<NhDaThongTinDuAnExportModel>();
            List<NhDaDuAn> lstDuAn = _nhDaDuAnService.FindAll(n => lstDuAnId.Contains(n.Id)).ToList();
            List<NhDaDuAnNguonVon> lstNguonVon = _nhDaDuAnNguonVonService.FindAll(n => lstDuAnId.Contains(n.IIdDuAnId.Value)).ToList();
            List<NhDaDuAnHangMuc> lstHangMuc = _nhDaDuAnHangMucService.FindAll(n => n.IIdDuAnId.HasValue && lstDuAnId.Contains(n.IIdDuAnId.Value)).ToList();
            Dictionary<Guid, List<NhDaDuAnNguonVon>> dicNguonVon = new Dictionary<Guid, List<NhDaDuAnNguonVon>>();
            Dictionary<Guid, List<NhDaDuAnHangMuc>> dicHangMuc = new Dictionary<Guid, List<NhDaDuAnHangMuc>>();
            if (lstNguonVon != null)
            {
                dicNguonVon = lstNguonVon.GroupBy(n => n.IIdDuAnId.Value).ToDictionary(n => n.Key, n => n.ToList());
            }
            if (lstHangMuc != null)
            {
                dicHangMuc = lstHangMuc.GroupBy(n => n.IIdDuAnId.Value).ToDictionary(n => n.Key, n => n.ToList());
            }
            int iStt = 1;
            foreach (var item in lstDuAn)
            {
                var objChuDauTu = _dmChuDauTuService.FindByCondition(n => n.Id == item.IIdChuDauTuId).FirstOrDefault();
                var objPhanCap = _nhDmPhanCapPheDuyetService.FindAll(n => n.Id == item.IIdCapPheDuyetId).FirstOrDefault();
                NhDaThongTinDuAnExportModel data = new NhDaThongTinDuAnExportModel()
                {
                    IStt = iStt.ToString(),
                    STenDuAn = item.STenDuAn,
                    SMaDuAn = item.SMaDuAn,
                    IIdMaChuDauTu = objChuDauTu.IIDMaDonVi,
                    SMaPhanCapPheDuyet = objPhanCap.SMa,
                    SKhoiCong = item.SKhoiCong,
                    SKetThuc = item.SKetThuc,
                    SDiaDiem = item.SDiaDiem,
                    SMucTieu = item.SMucTieu
                };
                var hangmucs = new List<NhDaDuAnHangMuc>();
                var nguonvons = new List<NhDaDuAnNguonVon>();
                if (dicHangMuc.ContainsKey(item.Id)) hangmucs = dicHangMuc[item.Id];
                if (dicNguonVon.ContainsKey(item.Id)) nguonvons = dicNguonVon[item.Id];
                if (hangmucs.Count() >= nguonvons.Count())
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
        public void OnExportThongTinDuAnTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.TEMPATE_NH_THONGTIN_DUAN);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    List<Guid> lstId = Items.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                    //list du an
                    var dataDuAn = GetDataExport(lstId);
                    if (dataDuAn != null && dataDuAn.Count > 0)
                    {
                        var iSTT = 0;
                        foreach (var item in dataDuAn)
                        {
                            iSTT++;
                            item.IStt = iSTT.ToString();
                        }
                    }
                    //list danh muc chu dau tu
                    var dataChuDauTus = _dmChuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                    var lstChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(dataChuDauTus);
                    //list phan cap phe duyet
                    var dataPhanCaps = _nhDmPhanCapPheDuyetService.FindAll(x => (bool)x.BActive);
                    var lstPhanCap = _mapper.Map<ObservableCollection<NhDmPhanCapPheDuyetModel>>(dataPhanCaps);

                    var data = new Dictionary<string, object>();
                    data.Add("Items", dataDuAn);
                    data.Add("ItemsLoaiCongTrinh", _lstLoaiCongTrinh);
                    data.Add("ItemsNguonVon", _lstNguonVon);
                    data.Add("ItemsChuDauTu", lstChuDauTu);
                    data.Add("ItemsPhanCap", lstPhanCap);
                    var xlsFile = _exportService.Export<NhDaThongTinDuAnExportModel, NhDmLoaiCongTrinh, NsNguonNganSach, DmChuDauTuModel, NhDmPhanCapPheDuyetModel>(templateFileName, data);
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

        public void OnExportThongTinDuAnCTCTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.TEMPATE_NH_THONGTIN_DUAN_CTC);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    List<Guid> lstId = Items.Where(n => n.IsChecked).Select(n => n.Id).ToList();
                    //list du an
                    var dataDuAn = _nhDaDuAnService.GetDuAnExportCTC(ILoai).Where(n => lstId.Contains(n.Id)).ToList();
                    var lstDuAn = _mapper.Map<ObservableCollection<NhDaThongTinDuAnCTCExportModel>>(dataDuAn);
                    if (lstDuAn != null && lstDuAn.Count > 0)
                    {
                        var iSTT = 0;
                        foreach (var item in lstDuAn)
                        {
                            iSTT++;
                            item.IStt = iSTT.ToString();
                        }
                    }
                    //list danh muc chu dau tu
                    var dataChuDauTus = _dmChuDauTuService.FindByNamLamViec(_sessionService.Current.YearOfWork);
                    var lstChuDauTu = _mapper.Map<ObservableCollection<DmChuDauTuModel>>(dataChuDauTus);
                    //list phan cap phe duyet
                    var dataPhanCaps = _nhDmPhanCapPheDuyetService.FindAll(x => (bool)x.BActive);
                    var lstPhanCap = _mapper.Map<ObservableCollection<NhDmPhanCapPheDuyetModel>>(dataPhanCaps);

                    var data = new Dictionary<string, object>();
                    data.Add("ItemsDuAn", lstDuAn);
                    data.Add("ItemsChuDauTu", lstChuDauTu);
                    data.Add("ItemsPhanCap", lstPhanCap);
                    var xlsFile = _exportService.Export<NhDaThongTinDuAnCTCExportModel, DmChuDauTuModel, NhDmPhanCapPheDuyetModel>(templateFileName, data);
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
        public void OnImportThongTinDuAnData()
        {
            ImportThongTinDuAnViewModel.Init();
            ImportThongTinDuAnViewModel.SavedAction = obj =>
            {
                LoadData();
            };
            ImportThongTinDuAnViewModel.ShowDialog();
        }
        private List<NhDaThongTinDuAnExportModel> GetDataByHangMuc(NhDaThongTinDuAnExportModel data, List<NhDaDuAnHangMuc> lstHangMuc, List<NhDaDuAnNguonVon> lstnguonVon)
        {
            List<NhDaThongTinDuAnExportModel> results = new List<NhDaThongTinDuAnExportModel>();
            NhDaDuAnNguonVon objNguonVon = new NhDaDuAnNguonVon();
            int countNguonVon = lstnguonVon.Count();
            int indexNguonVon = 0;
            foreach (var item in lstHangMuc)
            {
                if (indexNguonVon < countNguonVon)
                {
                    objNguonVon = lstnguonVon[indexNguonVon];
                    indexNguonVon++;
                }
                var current = data.Clone();
                current.STenHangMuc = item.STenHangMuc;
                if (item.IIdLoaiCongTrinhId.HasValue && _dicLoaiCongTrinh.ContainsKey(item.IIdLoaiCongTrinhId.Value))
                {
                    current.SMaLoaiCongTrinh = _dicLoaiCongTrinh[item.IIdLoaiCongTrinhId.Value].SMaLoaiCongTrinh;
                }
                current.IIdNguonVonId = objNguonVon.IIdNguonVonId.ToString();
                current.FGiaTriUsd = objNguonVon.FGiaTriUsd.ToString();
                current.FGiaTriVnd = objNguonVon.FGiaTriVnd.ToString();
                results.Add(current);
            }
            return results;
        }

        private List<NhDaThongTinDuAnExportModel> GetDataByNguonVon(NhDaThongTinDuAnExportModel data, List<NhDaDuAnHangMuc> lstHangMuc, List<NhDaDuAnNguonVon> lstnguonVon)
        {
            List<NhDaThongTinDuAnExportModel> results = new List<NhDaThongTinDuAnExportModel>();
            NhDaDuAnHangMuc objHangMuc = new NhDaDuAnHangMuc();
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
                if (objHangMuc.IIdLoaiCongTrinhId.HasValue && _dicLoaiCongTrinh.ContainsKey(objHangMuc.IIdLoaiCongTrinhId.Value))
                {
                    current.SMaLoaiCongTrinh = _dicLoaiCongTrinh[objHangMuc.IIdLoaiCongTrinhId.Value].SMaLoaiCongTrinh;
                }
                current.IIdNguonVonId = item.IIdNguonVonId.ToString();
                current.FGiaTriUsd = item.FGiaTriUsd.ToString();
                current.FGiaTriVnd = item.FGiaTriVnd.ToString();
                results.Add(current);
            }
            return results;
        }

        private void LoadLoaiCongTrinh()
        {
            _dicLoaiCongTrinh = new Dictionary<Guid, NhDmLoaiCongTrinh>();
            var lstLoaiCongTrinh = _nhDmLoaiCongTrinhService.FindAll();
            if (lstLoaiCongTrinh == null) return;
            foreach (var item in lstLoaiCongTrinh)
            {
                if (!_dicLoaiCongTrinh.ContainsKey(item.Id))
                    _dicLoaiCongTrinh.Add(item.Id, item);
            }
            _lstLoaiCongTrinh = _dicLoaiCongTrinh.Values.ToList();
        }
        private void LoadNguonVon()
        {
            _lstNguonVon = _nsNguonNganSachService.FindAll().ToList();
        }

        private void LoadChuongTrinh()
        {
            try
            {
                if (Items == null) return;
                ItemsChuongTrinh = new ObservableCollection<ComboboxItem>(
                                   Items.GroupBy(g => g.IIdKhttNhiemVuChiId)
                                        .Select(n => new ComboboxItem()
                                        { ValueItem = n.First().Id.ToString(), Id = n.First().IIdKhttNhiemVuChiId != null ? n.First().IIdKhttNhiemVuChiId.Value : Guid.Empty, DisplayItem = n.First().STenChuongTrinh }));

                OnPropertyChanged(nameof(ItemsChuongTrinh));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
