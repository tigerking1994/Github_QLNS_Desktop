using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Utility;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Domain;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Model;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.TransferCadres;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Service.Impl;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData
{
    public class TransferDataIndexViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ITlDmDonViService _tlDmDonViService;
        private readonly ITlDmMapPcDetailService _tlDmMapPcDetailService;
        private readonly ITlMapColumnConfigService _tlMapColumnConfigService;
        private readonly ITlDmCanBoService _cadresService;
        private readonly ITlCanBoPhuCapService _tlCanBoPhuCapService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;
        private readonly ITlDmCapBacService _tlDmCapBacService;
        private readonly ITlDmChucVuService _tlDmChucVuService;
        private readonly IExportService _exportService;
        private ObservableCollection<TlDmDonViModel> _lstDonVi;
        private ObservableCollection<CadresModel> _lstCadres;
        private ObservableCollection<TlCanBoPhuCapModel> _lstCanBoPhuCap;
        private List<TlPhuCapFoxproNotMappingModel> _tlPhuCapFoxproNotMappingModels;
        private DataTable _dtChuaMap;

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU_DOI_TUONG_INDEX;
        public override string GroupName => MenuItemContants.GROUP_TRANSFER_FROM_FOXPRO;
        public override string Name => "Chuyển đổi dữ liệu đối tượng";
        public override Type ContentType => typeof(View.Salary.TransferData.TransferCadres.TransferDataIndex);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;
        public override string Title => "Chuyển đổi dữ liệu đối tượng hưởng lương, phụ cấp từ FoxPro";
        public override string Description => "Chuyển đổi dữ liệu đối tượng hưởng lương, phụ cấp từ FoxPro";

        private DataTable _dtDonVi;
        public DataTable DtDonVi
        {
            get => _dtDonVi;
            set => SetProperty(ref _dtDonVi, value);
        }

        private DataTable _dtDoiTuong;
        public DataTable DtDoiTuong
        {
            get => _dtDoiTuong;
            set
            {
                SetProperty(ref _dtDoiTuong, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private DataTable _dtDmluong;
        public DataTable DtDmLuong
        {
            get => _dtDmluong;
            set => SetProperty(ref _dtDmluong, value);
        }

        public bool IsEnabled => DtDoiTuong != null && DtDoiTuong.Rows.Count > 0;

        private int _month;
        public int Month
        {
            get => _month;
            set => _month = value;
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => _year = value;
        }

        private string warningMessage;

        public TransferDataDialogViewModel TransferDataDialogViewModel { get; }
        public TransferDataDetailViewModel TransferDataDetailViewModel { get; }

        public RelayCommand OpenCommand { get; }
        public RelayCommand ExportCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public TransferDataIndexViewModel(
            ISessionService sessionService,
            IMapper mapper,
            ILog logger,
            TransferDataDialogViewModel transferDataDialogViewModel,
            TransferDataDetailViewModel transferDataDetailViewModel,
            ITlDmDonViService tlDmDonViService,
            ITlDmMapPcDetailService tlDmMapPcDetailService,
            ITlMapColumnConfigService tlMapColumnConfigService,
            ITlDmCanBoService cadresService,
            ITlCanBoPhuCapService tlCanBoPhuCapService,
            ITlDmPhuCapService tlDmPhuCapService,
            ITlDmCapBacService tlDmCapBacService,
            ITlDmChucVuService tlDmChucVuService,
            IExportService exportService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;

            _tlDmDonViService = tlDmDonViService;
            _tlDmMapPcDetailService = tlDmMapPcDetailService;
            _tlMapColumnConfigService = tlMapColumnConfigService;
            _cadresService = cadresService;
            _tlCanBoPhuCapService = tlCanBoPhuCapService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _tlDmCapBacService = tlDmCapBacService;
            _tlDmChucVuService = tlDmChucVuService;
            _exportService = exportService;

            OpenCommand = new RelayCommand(o => OnOpen());
            ExportCommand = new RelayCommand(o => OnExport());
            RefreshCommand = new RelayCommand(o => OnRefresh());

            TransferDataDialogViewModel = transferDataDialogViewModel;
            TransferDataDetailViewModel = transferDataDetailViewModel;
        }

        public override void Init()
        {
            base.Init();
            LoadData();
        }

        private void OnOpen()
        {
            TransferDataDialogViewModel.Init();
            TransferDataDialogViewModel.ChooseAction = obj =>
            {
                this.DtDonVi = TransferDataDialogViewModel.DtDonVi;
                this.DtDoiTuong = TransferDataDialogViewModel.DtDoiTuong;
                this.DtDmLuong = TransferDataDialogViewModel.DtDmLuong;
                Month = int.Parse(TransferDataDialogViewModel.MonthSelected.ValueItem);
                Year = int.Parse(TransferDataDialogViewModel.YearSelected.ValueItem);
                _lstCadres = TransferDataDialogViewModel.lstCadres;
                _lstDonVi = TransferDataDialogViewModel.lstDonVi;
                _lstCanBoPhuCap = TransferDataDialogViewModel.lstCanBoPhuCap;
                _tlPhuCapFoxproNotMappingModels = TransferDataDialogViewModel.tlPhuCapFoxproNotMappingModels;
                _dtChuaMap = TransferDataDialogViewModel.DtChuaMap;
                warningMessage = TransferDataDialogViewModel.warningMessage;
                OnPropertyChanged(nameof(DtDonVi));
                OnPropertyChanged(nameof(DtDoiTuong));
            };
            TransferDataDialogViewModel.ShowDialogHost();
        }

        private void OnRefresh()
        {
            DtDonVi = new DataTable();
            DtDoiTuong = new DataTable();
            DtDmLuong = new DataTable();
            _lstCadres = new ObservableCollection<CadresModel>();
            _lstDonVi = new ObservableCollection<TlDmDonViModel>();
            _lstCanBoPhuCap = new ObservableCollection<TlCanBoPhuCapModel>();
            _tlPhuCapFoxproNotMappingModels = new List<TlPhuCapFoxproNotMappingModel>();
            _dtChuaMap = new DataTable();
            warningMessage = string.Empty;
            OnPropertyChanged(nameof(DtDonVi));
            OnPropertyChanged(nameof(DtDoiTuong));
        }

        private void OnExport()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("Items", _tlPhuCapFoxproNotMappingModels);
                data.Add("Items1", _dtChuaMap);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_LUONG, ExportFileName.RPT_TL_DANHSACH_FOXPRO);
                string fileNamePrefix = "rptLuong_DanhSach_PhuCap_Va_DoiTuong_ChuaMap";
                string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                var xlsFile = _exportService.Export<DataTable, TlPhuCapFoxproNotMappingModel>(templateFileName, data);
                e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    var result = (ExportResult)e.Result;
                    if (result != null)
                    {
                        _exportService.Open(result, ExportType.EXCEL);
                    }
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
            });
        }

        public override void OnSave()
        {
            MessageBoxResult dialogResult = new MessageBoxResult();
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(warningMessage) && _lstCadres.Count > 0 && _lstCadres != null)
                {
                    dialogResult = MessageBoxHelper.Confirm(string.Format("{0}\nĐồng chí có muốn tiếp tục?", warningMessage));
                    if (dialogResult == MessageBoxResult.No)
                    {
                        return;
                    }
                }
            }, (s, e) =>
            {
                IsLoading = false;
                if (dialogResult == MessageBoxResult.No)
                {
                    return;
                }
                if (e.Error == null)
                {
                    if (_lstCadres == null || _lstCadres.Count == 0)
                    {
                        MessageBoxHelper.Info(Resources.NoneCadresesImport);
                    }
                    else
                    {
                        var canBo = _lstCadres.FirstOrDefault();
                        TransferDataDetailViewModel.Model = canBo;
                        TransferDataDetailViewModel.NgayNhapNgu = canBo.NgayNn;
                        TransferDataDetailViewModel.NgayXuatNgu = canBo.NgayXn;
                        TransferDataDetailViewModel.NgayTaiNgu = canBo.NgayTn;
                        TransferDataDetailViewModel.NamThamNien = canBo.NamTn == null ? 0 : (int)canBo.NamTn;
                        TransferDataDetailViewModel.ThangThamNienNghe = canBo.ThangTnn == null ? 0 : (int)canBo.ThangTnn;
                        TransferDataDetailViewModel.ViewState = Utility.Enum.FormViewState.DETAIL;
                        TransferDataDetailViewModel.LstCanBo = _lstCadres;
                        TransferDataDetailViewModel.DonViItems = _lstDonVi;
                        TransferDataDetailViewModel.LstCanBoPhuCap = _lstCanBoPhuCap;
                        TransferDataDetailViewModel.SearchTenPhuCap = string.Empty;
                        TransferDataDetailViewModel.SearchMaPhuCap = string.Empty;
                        TransferDataDetailViewModel.SelectedDonVi = _lstDonVi.FirstOrDefault(x => x.MaDonVi == canBo.Parent);
                        TransferDataDetailViewModel.SelectedCanBo = canBo;
                        TransferDataDetailViewModel.SavedAction = obj =>
                        {
                            DtDoiTuong = null;
                            DtDonVi = null;
                            DtDmLuong = null;
                        };
                        TransferDataDetailViewModel.Init();
                        var view = new View.Salary.TransferData.TransferCadres.DataTransferDetail
                        {
                            DataContext = TransferDataDetailViewModel
                        };
                        view.ShowDialog();
                    }
                }
                else
                {
                    MessageBoxHelper.Warning("Có lỗi xảy ra");
                    _logger.Error(e.Error.Message);
                }
            });
        }
    }
}
