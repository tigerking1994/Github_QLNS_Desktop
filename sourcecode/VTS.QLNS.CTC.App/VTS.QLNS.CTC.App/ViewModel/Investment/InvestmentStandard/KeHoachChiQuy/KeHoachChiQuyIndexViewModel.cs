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
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachChiQuy;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy.PrintDialog;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachChiQuy.PrintDialog;
using VTS.QLNS.CTC.App.Service.UserFunction;
using System.Windows;
using System.Net;
using VTS.QLNS.CTC.App.View.Investment.InvestmentStandard.KeHoachVonUngDeXuat;
using VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachVonUngDeXuat;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentStandard.KeHoachChiQuy
{
    public class KeHoachChiQuyIndexViewModel : GridViewModelBase<VdtNcNhuCauChiModel>
    {
        public override string FuncCode => NSFunctionCode.INVESTMENT_IMPLEMENTATION_KE_HOACH_CHI_QUY_INDEX;
        public override string GroupName => MenuItemContants.GROUP_CAPITAL_PLAN_OF_YEAR;
        public override string Name => "Kế hoạch chi Quý";
        public override string Description => "Danh sách thông tin kế hoạch chi Quý";
        public override Type ContentType => typeof(View.Investment.InvestmentStandard.KeHoachChiQuy.KeHoachChiQuyIndex);
        public bool IsEdit => SelectedItem != null && SelectedItem.Id != Guid.Empty;



        #region Private
        private readonly IVdtNcNhuCauChiService _service;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _nsDonViService;
        private readonly INsNguonNganSachService _nguonVonService;
        private readonly IDanhMucService _danhMucService;
        private readonly IExportService _exportService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private ICollectionView _deNghiThanhToanView;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IVdtFtpRootService _ftpService;

        #endregion

        #region declare RelayCommand
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ResetFilterCommand { get; }
        public RelayCommand PrintReportCommand { get; }
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand GetDataFileCommand { get; }
        public RelayCommand ImportDataCommand { get; }

        #endregion

        #region Componer
        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get => (Items == null || !Items.Any()) ? false : Items.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _isAllItemsSelected, value);
                if (Items != null)
                {
                    Items.Select(c => { c.IsChecked = _isAllItemsSelected; return c; }).ToList();
                }
            }
        }

        private string _sNamKeHoach;
        public string SNamKeHoach
        {
            get => _sNamKeHoach;
            set
            {
                SetProperty(ref _sNamKeHoach, value);
            }
        }

        private DateTime? _dNgayDeNghiFrom;
        public DateTime? DNgayDeNghiFrom
        {
            get => _dNgayDeNghiFrom;
            set
            {
                SetProperty(ref _dNgayDeNghiFrom, value);
                OnSearch();
            }
        }

        private DateTime? _dNgayDeNghiTo;
        public DateTime? DNgayDeNghiTo
        {
            get => _dNgayDeNghiTo;
            set
            {
                SetProperty(ref _dNgayDeNghiTo, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _drpDonViQuanLy;
        public ObservableCollection<ComboboxItem> DrpDonViQuanLy
        {
            get => _drpDonViQuanLy;
            set => SetProperty(ref _drpDonViQuanLy, value);
        }

        private ComboboxItem _drpDonViQuanLySelected;
        public ComboboxItem DrpDonViQuanLySelected
        {
            get => _drpDonViQuanLySelected;
            set
            {
                SetProperty(ref _drpDonViQuanLySelected, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsQuy;
        public ObservableCollection<ComboboxItem> ItemsQuy
        {
            get => _itemsQuy;
            set => SetProperty(ref _itemsQuy, value);
        }

        private ComboboxItem _selectedQuy;
        public ComboboxItem SelectedQuy
        {
            get => _selectedQuy;
            set
            {
                SetProperty(ref _selectedQuy, value);
                OnSearch();
            }
        }

        private ObservableCollection<ComboboxItem> _itemsNguonVon;
        public ObservableCollection<ComboboxItem> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private ComboboxItem _selectedNguonVon;
        public ComboboxItem SelectedNguonVon
        {
            get => _selectedNguonVon;
            set
            {
                SetProperty(ref _selectedNguonVon, value);
                OnSearch();
            }
        }
        
        private ObservableCollection<ComboboxItem> _DataDonViTinh;
        public ObservableCollection<ComboboxItem> DataDonViTinh
        {
            get => _DataDonViTinh;
            set => SetProperty(ref _DataDonViTinh, value);
        }

        private ComboboxItem _selectedDonViTinh;
        public ComboboxItem SelectedDonViTinh
        {
            get => _selectedDonViTinh;
            set => SetProperty(ref _selectedDonViTinh, value);
        }
        #endregion

        public KeHoachChiQuyDialogViewModel KeHoachChiQuyDialogViewModel { get; set; }
        public KeHoachChiQuyServerFtpViewModel KeHoachChiQuyServerFtpViewModel { get; set; }
        public KeHoachChiQuyDetailViewModel KeHoachChiQuyDetailViewModel { get; set; }
        public KeHoachChiQuyPrintDialogViewModel KeHoachChiQuyPrintDialogViewModel { get; set; }
        public KeHoachChiQuyImportViewModel KeHoachChiQuyImportViewModel { get; set; }
        public KeHoachChiQuyImport KeHoachChiQuyImport { get; set; }


        public KeHoachChiQuyIndexViewModel(
            KeHoachChiQuyDialogViewModel keHoachChiQuyDialogViewModel,
            KeHoachChiQuyDetailViewModel keHoachChiQuyDetailViewModel,
            KeHoachChiQuyImportViewModel keHoachChiQuyImportViewModel,
            KeHoachChiQuyServerFtpViewModel keHoachChiQuyServerFtpViewModel,
            KeHoachChiQuyPrintDialogViewModel keHoachChiQuyPrintDialogViewModel,            
            IVdtNcNhuCauChiService service,
            IExportService exportService,
            INsNguonNganSachService nguonVonService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            FtpStorageService ftpStorageService,
            IVdtFtpRootService ftpService,
            IMapper mapper,
            ILog logger)
        {
            _exportService = exportService;
            _service = service;
            _sessionService = sessionService;
            _nsDonViService = nsDonViService;
            _nguonVonService = nguonVonService;
            _danhMucService = danhMucService;
            _ftpService = ftpService;
            _ftpStorageService = ftpStorageService;
            _mapper = mapper;
            _logger = logger;

            KeHoachChiQuyServerFtpViewModel = keHoachChiQuyServerFtpViewModel;
            KeHoachChiQuyServerFtpViewModel.ParentPage = this;
            KeHoachChiQuyDialogViewModel = keHoachChiQuyDialogViewModel;
            KeHoachChiQuyDialogViewModel.ParentPage = this;
            KeHoachChiQuyDetailViewModel = keHoachChiQuyDetailViewModel;
            KeHoachChiQuyDetailViewModel.ParentPage = this;
            KeHoachChiQuyPrintDialogViewModel = keHoachChiQuyPrintDialogViewModel;
            KeHoachChiQuyPrintDialogViewModel.ParentPage = this;
            KeHoachChiQuyImportViewModel = keHoachChiQuyImportViewModel;
            KeHoachChiQuyImportViewModel.ParentPage = this;

            SearchCommand = new RelayCommand(obj => OnSearch());
            ResetFilterCommand = new RelayCommand(obj => onResetFilter());
            ExportCommand = new RelayCommand(obj => OnExportExcel());
            PrintReportCommand = new RelayCommand(obj => OnPrintReport());
            UploadFileCommand = new RelayCommand(obj => OnUpload());
            GetDataFileCommand = new RelayCommand(obj => OnGetDataFtp());
            ImportDataCommand = new RelayCommand(obj => OnImportData());
        }

        #region RelayCommand Event
        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            GetDonViQuanLy();
            GetQuy();
            GetNguonVon();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            DataDonViTinh = new ObservableCollection<ComboboxItem>();
            List<DanhMuc> listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE
                                && x.INamLamViec == _sessionService.Current.YearOfWork).ToList();
            if (listDonViTinh == null || listDonViTinh.Count <= 0)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = DonViTinh.DONG_VALUE, DisplayItem = DonViTinh.DONG });
            }
            foreach (var dvt in listDonViTinh)
            {
                DataDonViTinh.Add(new ComboboxItem { ValueItem = dvt.SGiaTri.ToString(), DisplayItem = dvt.STen });
            }
            SelectedDonViTinh = DataDonViTinh.FirstOrDefault();
            var listChungTu = _service.GetNhuCauChiIndex();
            var lstItem = _mapper.Map<List<VdtNcNhuCauChiModel>>(listChungTu);
            lstItem = lstItem.Select(n => { n.iRowIndex = lstItem.IndexOf(n) + 1; return n; }).ToList();
            Items = _mapper.Map<ObservableCollection<VdtNcNhuCauChiModel>>(lstItem);
            _deNghiThanhToanView = CollectionViewSource.GetDefaultView(Items);
            _deNghiThanhToanView.Filter = VdtTtDeNghiThanhToanFilter;
            if (Items != null && Items.Count > 0)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        public void OnSearch()
        {
            _deNghiThanhToanView.Refresh();
        }

        protected override void OnAdd()
        {
            KeHoachChiQuyDialogViewModel.Model = new VdtNcNhuCauChiModel();
            KeHoachChiQuyDialogViewModel.Init();
            KeHoachChiQuyDialogViewModel.DNgayDeNghi = null;
            KeHoachChiQuyDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtNcNhuCauChiModel>((VdtNcNhuCauChi)obj));
            };
            var view = new KeHoachChiQuyDialog
            {
                DataContext = KeHoachChiQuyDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }
        private void OnGetDataFtp()
        {
            KeHoachChiQuyServerFtpViewModel.Model = new VdtNcNhuCauChiModel();
            KeHoachChiQuyServerFtpViewModel.Init();
            var view = new KeHoachChiQuyServerFtpDialog
            {
                DataContext = KeHoachChiQuyServerFtpViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        private void OnImportData()
        {
            try
            {
                KeHoachChiQuyImportViewModel.SavedAction = obj => this.LoadData();
                KeHoachChiQuyImportViewModel.Init();
                //
                KeHoachChiQuyImport = new KeHoachChiQuyImport { DataContext = KeHoachChiQuyImportViewModel };

                KeHoachChiQuyImport.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnUpdate()
        {
            KeHoachChiQuyDialogViewModel.Model = SelectedItem;
            KeHoachChiQuyDialogViewModel.Init();
            KeHoachChiQuyDialogViewModel.SavedAction = obj =>
            {
                this.OnRefresh();
                OnOpenDisbursementPaymentDetail(_mapper.Map<VdtNcNhuCauChiModel>((VdtNcNhuCauChi)obj));
            };
            var view = new KeHoachChiQuyDialog
            {
                DataContext = KeHoachChiQuyDialogViewModel
            };
            DialogHost.Show(view, "RootDialog");
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(Resources.MsgConfirmDeleteKeHoachChiQuy, SelectedItem.sSoDeNghi);
            var messageBox = new NSMessageBoxViewModel(messageBuilder.ToString(), "Xác nhận", NSMessageBoxButtons.YesNo, DeleteEventHandler);
            DialogHost.Show(messageBox.Content, "RootDialog");
        }

        private void onResetFilter()
        {
            SNamKeHoach = null;
            DNgayDeNghiFrom = null;
            DNgayDeNghiTo = null;
            DrpDonViQuanLySelected = null;
            SelectedNguonVon = null;
            SelectedQuy = null;
            OnPropertyChanged(nameof(SNamKeHoach));
            OnPropertyChanged(nameof(DNgayDeNghiFrom));
            OnPropertyChanged(nameof(DNgayDeNghiTo));
            OnPropertyChanged(nameof(DrpDonViQuanLySelected));
            OnPropertyChanged(nameof(SelectedNguonVon));
            OnPropertyChanged(nameof(SelectedQuy));
            OnSearch();
        }

        public int GetDonViTinh()
        {
            if (SelectedDonViTinh == null || string.IsNullOrEmpty(SelectedDonViTinh.ValueItem))
                return 1;
            return int.Parse(SelectedDonViTinh.ValueItem);
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEdit));
        }

        protected override void OnSelectionDoubleClick(object obj)
        {
            base.OnSelectionDoubleClick(obj);
            OnOpenDisbursementPaymentDetail((VdtNcNhuCauChiModel)obj, true);
        }

        private void OnExportExcel()
        {
            if (Items == null || !Items.Any(n => n.IsChecked)) return;
            foreach (var item in Items.Where(n => n.IsChecked))
            {
                ExportItem(item, ExportType.EXCEL);
            }
        }

        private void OnPrintReport()
        {
            if (Items == null || !Items.Any(n => n.IsChecked)) return;
            KeHoachChiQuyPrintDialogViewModel.VdtNcNhuCauChiModels = Items.Where(n => n.IsChecked).ToList();
            KeHoachChiQuyPrintDialogViewModel.Init();
            object content = new KeHoachChiQuyPrintDialog
            {
                DataContext = KeHoachChiQuyPrintDialogViewModel
            };
            DialogHost.Show(content, DemandCheckScreen.ROOT_DIALOG, null, null);
            /*foreach (var item in Items.Where(n => n.IsChecked))
            {
                ExportItem(item, ExportType.PDF);
            }*/
        }
        #endregion

        #region Helper
        private void ExportItem(VdtNcNhuCauChiModel item, ExportType exportType)
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    var lstData = _mapper.Map<List<VdtNcNhuCauChiChiTietModel>>(_service.GetNhuCauChiDetail(item.iID_MaDonViQuanLy, item.iNamKeHoach.Value, item.iID_NguonVonID.Value, item.iQuy));
                    lstData = lstData.Select(n => { n.iStt = lstData.IndexOf(n) + 1; return n; }).ToList();
                    var detailData = _service.GetDetailByParent(item.Id);
                    int donViTinh = GetDonViTinh();
                    double tongtien = 0;
                    if (detailData != null)
                    {
                        foreach (var child in detailData)
                        {
                            var currentData = lstData.FirstOrDefault(n => n.iID_DuAnId == child.IIdDuAnId && n.sLoaiThanhToan == child.SLoaiThanhToan);
                            if (currentData != null)
                            {
                                currentData.fGiaTriDeNghi = child.FGiaTriDeNghi ?? 0;
                                currentData.sGhiChu = child.SGhiChu;
                                tongtien += child.FGiaTriDeNghi ?? 0;
                            }
                        }
                    }
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    FormatNumber formatNumber = new FormatNumber(1, exportType);
                    data.Add("FormatNumber", formatNumber);
                    data.Add("sTenDonVi", item.sTenDonVi == null? string.Empty: item.sTenDonVi.ToUpper());
                    data.Add("iQuy", item.iQuy);
                    data.Add("iNam", item.iNamKeHoach);
                    data.Add("sTenNguonVon", item.sTenNguonVon);
                    data.Add("sNgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
                    data.Add("Items", lstData);
                    data.Add("TongTienBangChu", StringUtils.NumberToText(tongtien, true));
                    data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
                    data.Add("ThoiGian", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHCQ, ExportFileName.RPT_VDT_NC_NHUCAUCHI);
                    string fileNamePrefix = ExportFileName.RPT_VDT_NC_NHUCAUCHI;
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<VdtNcNhuCauChiChiTietModel>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, exportType);
                        }
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
        private void OnUpload()
        {
            List<VdtNcNhuCauChiModel> listExport = Items.Where(x => x.IsChecked).ToList();
            if (listExport.GroupBy(x => x.iNamKeHoach).Count() > 1)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn bản ghi kế hoạch chi quý");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            VdtNcNhuCauChiModel item = Items.Where(x => x.IsChecked).FirstOrDefault();
            if (item == null || item.Id ==  null || item.IsChecked == false)
            {
                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.AppendFormat("Vui lòng chọn bản ghi");
                MessageBox.Show(messageBuilder.ToString());
                return;
            }
            var lstData = _mapper.Map<List<VdtNcNhuCauChiChiTietModel>>(_service.GetNhuCauChiDetail(item.iID_MaDonViQuanLy, item.iNamKeHoach.Value, item.iID_NguonVonID.Value, item.iQuy));
            lstData = lstData.Select(n => { n.iStt = lstData.IndexOf(n) + 1; return n; }).ToList();
            var detailData = _service.GetDetailByParent(item.Id);
            int donViTinh = GetDonViTinh();
            double tongtien = 0;
            if (detailData != null)
            {
                foreach (var child in detailData)
                {
                    var currentData = lstData.FirstOrDefault(n => n.iID_DuAnId == child.IIdDuAnId && n.sLoaiThanhToan == child.SLoaiThanhToan);
                    if (currentData != null)
                    {
                        currentData.fGiaTriDeNghi = child.FGiaTriDeNghi ?? 0;
                        currentData.sGhiChu = child.SGhiChu;
                        tongtien += child.FGiaTriDeNghi ?? 0;
                    }
                }
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            FormatNumber formatNumber = new FormatNumber(1, ExportType.EXCEL);
            data.Add("FormatNumber", formatNumber);
            data.Add("sTenDonVi", item.sTenDonVi == null ? string.Empty : item.sTenDonVi.ToUpper());
            data.Add("iQuy", item.iQuy);
            data.Add("iNam", item.iNamKeHoach);
            data.Add("sTenNguonVon", item.sTenNguonVon);
            data.Add("sNgayThangNam", string.Format("Ngày {0} tháng {1} năm {2}", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy")));
            data.Add("Items", lstData);
            data.Add("TongTienBangChu", StringUtils.NumberToText(tongtien * donViTinh, true));
            data.Add("Header1", SelectedDonViTinh != null ? SelectedDonViTinh.DisplayItem : string.Empty);
            data.Add("ThoiGian", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            string templateFileName = Path.Combine(ExportPrefix.PATH_TL_KHCQ, ExportFileName.RPT_VDT_NC_NHUCAUCHI);
            string fileNamePrefix = ExportFileName.RPT_VDT_NC_NHUCAUCHI;
            string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
            var xlsFile = _exportService.Export<VdtNcNhuCauChiChiTietModel>(templateFileName, data);
            var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);

            //code download file
            var objdv = _nsDonViService.FindByMaDonViAndNamLamViec(item.iID_MaDonViQuanLy, Convert.ToInt32(item.iNamKeHoach));
            string sStage = string.Empty;
            string filePathLocal = string.Empty;
            string precious = item.iQuy.ToString();
            if (SelectedItem != null)
            {
                sStage = item.iNamKeHoach.ToString();
            }
            _exportService.Open(Result, ExportType.EXCEL, ref filePathLocal);
            string sFolderRoot = ConstantUrlPathPhanHe.UrlKhcqWinformReceive;
            var strUrl = string.Format("{0}/{1}/{2}/{3}", objdv.IIDMaDonVi, sFolderRoot, sStage, precious);
            if (!File.Exists(strUrl))
            {
                string strActiveFileName = "";
                string[] splitActiveFiName = xlsFile.ActiveFileName.Split("\\");
                if (strActiveFileName != null && splitActiveFiName.Length != 0)
                {
                    strActiveFileName = splitActiveFiName[splitActiveFiName.Length - 1];
                }
                VdtFtpRoot dataRoot = new VdtFtpRoot();
              
                List<string> configCodes = new List<string>()
                {
                    STORAGE_CONFIG.FTP_HOST
                };
                var rs = _danhMucService.FindByCodes(configCodes).ToList();
                var SIpAddress = rs.FirstOrDefault(x => STORAGE_CONFIG.FTP_HOST.Equals(x.IIDMaDanhMuc)).SGiaTri;
                dataRoot = _ftpService.GetVdtFtpRoot(objdv.IIDMaDonVi, SIpAddress, sFolderRoot);
                if (dataRoot == null)
                {
                    dataRoot = new VdtFtpRoot()
                    {
                        SMaDonVi = objdv.IIDMaDonVi,
                        SIpAddress = SIpAddress, // vd: ftp:\\10.60.108.246
                        SFolderRoot = sFolderRoot,
                        SNguoiTao = _sessionService.Current.Principal,
                        DNgayTao = DateTime.Now
                    };
                    _ftpService.Add(dataRoot);
                }
                var result = _ftpStorageService.UploadCommand(dataRoot.Id, filePathLocal, strActiveFileName, strUrl);
                if (result != 1)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Gửi dữ liệu thất bại");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
                else
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Gửi dữ liệu thành công");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }
            }

        }
        private void GetDonViQuanLy()
        {
            var cbxLoaiDonViData = _nsDonViService.FindByNamLamViec(_sessionService.Current.YearOfWork)
                .Select(n => new ComboboxItem() { ValueItem = n.IIDMaDonVi, DisplayItem = string.Format("{0}-{1}", n.IIDMaDonVi, n.TenDonVi) });
            _drpDonViQuanLy = new ObservableCollection<ComboboxItem>(cbxLoaiDonViData);
        }

        private void GetNguonVon()
        {
            var cbxNguonVonViData = _nguonVonService.FindNguonNganSach()
                .OrderBy(n => n.IIdMaNguonNganSach)
                .Select(n => new ComboboxItem() { ValueItem = n.IIdMaNguonNganSach.Value.ToString(), DisplayItem = n.STen });
            ItemsNguonVon = new ObservableCollection<ComboboxItem>(cbxNguonVonViData);
            OnPropertyChanged(nameof(ItemsNguonVon));
        }

        private void GetQuy()
        {
            List<ComboboxItem> lstQuy = new List<ComboboxItem>();
            lstQuy.Add( new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_1, ValueItem = ((int)LoaiQuyEnum.Type.QUY_1).ToString() });
            lstQuy.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_2, ValueItem = ((int)LoaiQuyEnum.Type.QUY_2).ToString() });
            lstQuy.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_3, ValueItem = ((int)LoaiQuyEnum.Type.QUY_3).ToString() });
            lstQuy.Add(new ComboboxItem() { DisplayItem = LoaiQuyEnum.TypeName.QUY_4, ValueItem = ((int)LoaiQuyEnum.Type.QUY_4).ToString() });
            ItemsQuy = new ObservableCollection<ComboboxItem>(lstQuy);
            OnPropertyChanged(nameof(ItemsQuy));
        }

        private bool VdtTtDeNghiThanhToanFilter(object obj)
        {
            if (!(obj is VdtNcNhuCauChiModel temp)) return true;
            var bCondition = true;
            int iNamKeHoach = 0;
            if (!string.IsNullOrEmpty(SNamKeHoach) && int.TryParse(SNamKeHoach, out iNamKeHoach))
            {
                bCondition &= (temp.iNamKeHoach == iNamKeHoach);
            }
            if (DNgayDeNghiFrom.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi >= DNgayDeNghiFrom);
            }
            if (DNgayDeNghiTo.HasValue)
            {
                bCondition &= (temp.dNgayDeNghi.HasValue && temp.dNgayDeNghi <= DNgayDeNghiTo);
            }
            if (DrpDonViQuanLySelected != null)
            {
                bCondition &= (temp.iID_MaDonViQuanLy == DrpDonViQuanLySelected.ValueItem);
            }
            if (SelectedNguonVon != null)
            {
                bCondition &= (temp.iID_NguonVonID == int.Parse(SelectedNguonVon.ValueItem));
            }
            if (SelectedQuy != null)
            {
                bCondition &= (temp.iQuy == int.Parse(SelectedQuy.ValueItem));
            }
            return bCondition;
        }

        private void OnOpenDisbursementPaymentDetail(VdtNcNhuCauChiModel SelectedItem, bool bIsDetail = false)
        {
            KeHoachChiQuyDetailViewModel.BIsDetail = bIsDetail;
            KeHoachChiQuyDetailViewModel.Model = SelectedItem;
            KeHoachChiQuyDetailViewModel.Init();
            var view = new KeHoachChiQuyDetail { DataContext = KeHoachChiQuyDetailViewModel };
            //view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
            LoadData();
        }

        private void DeleteEventHandler(NSDialogResult result)
        {
            if (result != NSDialogResult.Yes) return;
            _service.DeleteKeHoachChiQuy(SelectedItem.Id);
            LoadData();
        }
        #endregion
    }
}
