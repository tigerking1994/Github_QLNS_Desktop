using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;
using VTS.QLNS.CTC.Utility.Enum;
using System.IO;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.ContractInfo
{
    public class ContractInfoImportViewModel : ViewModelBase
    {
        #region Private
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService;
        private readonly IVdtDmNhaThauService _nhathauService;
        private readonly IProjectManagerService _duanService;
        private IVdtDaTtHopDongService _vdtDaTtHopDongService;
        private readonly IExportService _exportService;
        private readonly ILoaiHopDongService _loaiHopDongService;
        private List<ImportErrorItem> _lstErrQlDuAn = new List<ImportErrorItem>();
        Dictionary<string, VdtDmNhaThau> _dicNhaThau;
        Dictionary<string, VdtDmLoaiHopDong> _dicLoaiHopDong;
        Dictionary<string, Guid> _dicDuAn;
        private readonly ILog _logger;
        #endregion

        #region Public
        public override string Name => "IMPORT THÔNG TIN HỢP ĐỒNG";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu thông tin chung hợp đồng";
        #endregion

        #region RelayCommand
        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ShowErrorCommand { get; }
        public RelayCommand ResetDataCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        #endregion

        #region Variables    
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        private ObservableCollection<VdtDaTtHopDongImportModel> _items;
        public ObservableCollection<VdtDaTtHopDongImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private VdtDaTtHopDongImportModel _selectedItem;
        public VdtDaTtHopDongImportModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private bool _isSaveData;
        public bool IsSaveData
        {
            get
            {
                _isSaveData = (Items != null && Items.Count > 0) ? true : false;
                return _isSaveData;
            }
            set
            {
                SetProperty(ref _isSaveData, value);
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        #endregion

        public ContractInfoImportViewModel(
            ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            IProjectManagerService projectManagerService,
            IVdtDmNhaThauService nhathauService,
            IVdtDaTtHopDongService vdtDaTtHopDongService,
            IExportService exportService,
            ILoaiHopDongService loaiHopDongService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nhathauService = nhathauService;
            _duanService = projectManagerService;
            _vdtDaTtHopDongService = vdtDaTtHopDongService;
            _exportService = exportService;
            _loaiHopDongService = loaiHopDongService;
            _logger = logger;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ShowErrorCommand = new RelayCommand(obj => ShowError());
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
        }

        #region Methods
        public override void Init()
        {
            try
            {
                OnLoadDuAn();
                OnLoadNhaThau();
                OnLoadLoaiHopDong();
                OnResetData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnResetData()
        {
            _filePath = string.Empty;
            _items = new ObservableCollection<VdtDaTtHopDongImportModel>();

            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(Items));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void OnUploadFile()
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Chọn file excel",
                    RestoreDirectory = true,
                    DefaultExt = StringUtils.EXCEL_EXTENSION
                };
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                FilePath = openFileDialog.FileName;
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;
                    HandleData();
                }, (s, e) =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.MsgErrorImport, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        private void HandleData()
        {
            Dictionary<string, Guid> _dicMaDuAn = new Dictionary<string, Guid>();
            var lstDuAn = _duanService.FindAll(n => 1 == 1);
            if (lstDuAn != null)
            {
                foreach (var item in lstDuAn)
                {
                    if (!_dicMaDuAn.ContainsKey(item.SMaDuAn))
                    {
                        _dicMaDuAn.Add(item.SMaDuAn, item.Id);
                    }
                }
            }
            try
            {
                XlsFile xls = new XlsFile(false);
                xls.Open(FilePath);
                xls.ActiveSheet = 1;

                var dataImport = _importService.ProcessData<VdtDaTtHopDongImportModel>(FilePath);
                var QlDuAnImportModels = new ObservableCollection<VdtDaTtHopDongImportModel>(dataImport.Data);

                _lstErrQlDuAn = new List<ImportErrorItem>();
                List<string> lstError = new List<string>();

                if (dataImport.ImportErrors.Count > 0)
                {
                    _lstErrQlDuAn.AddRange(dataImport.ImportErrors);
                }

                if (QlDuAnImportModels == null || QlDuAnImportModels.Count <= 0)
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (QlDuAnImportModels.Any(x => !x.ImportStatus))
                {
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (string.IsNullOrEmpty(FilePath))
                {
                    lstError.Add(Resources.ErrorFileEmpty);
                }

                int i = 0;
                foreach (var item in QlDuAnImportModels)
                {
                    ++i;
                    var listError = ValidateItem(item, i, _dicMaDuAn);

                    if (listError.Count > 0)
                    {
                        _lstErrQlDuAn.AddRange(listError);
                        item.ImportStatus = false;

                        if (listError.Any(x => x.IsErrorMLNS))
                        {
                            item.IsError = true;
                        }
                    }
                }

                Items = QlDuAnImportModels;

                if (lstError.Any())
                {
                    string sMessError = string.Join(Environment.NewLine, lstError);
                    System.Windows.MessageBox.Show(sMessError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                FilePath = "";
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave(object obj)
        {
            try
            {
                List<string> lstError = new List<string>();
                HandleSaveData(Items.Where(x => x.ImportStatus).ToList());
                System.Windows.MessageBox.Show(Resources.FileImportStatus);
                SavedAction?.Invoke(null);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnClose(object obj)
        {
            try
            {
                var window = obj as Window;
                window.Close();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void HandleSaveData(List<VdtDaTtHopDongImportModel> items)
        {

            if (items.Count <= 0) throw new Exception("Không có bản ghi nào có thể lưu!");
            List<VdtDaTtHopDong> result = new List<VdtDaTtHopDong>();
            foreach (var item in items)
            {
                Guid iId = Guid.NewGuid();
                int dngay = 0;
                result.Add(new VdtDaTtHopDong()
                {
                    Id = iId,
                    IIdDuAnId = _dicDuAn[item.SMaDuAn],
                    SSoHopDong = item.SSoHopDong,
                    STenHopDong = item.STenHopDong,
                    DNgayHopDong = DateUtils.CheckDateFormatAndConverter(item.DNgayHopDong) ?? DateTime.Now,
                    IIdLoaiHopDongId = _dicLoaiHopDong[item.SMaLoaiHopDong].Id,
                    IIdNhaThauThucHienId = !string.IsNullOrEmpty(item.SMaNhaThauThucHien) ? _dicNhaThau[item.SMaNhaThauThucHien].Id : Guid.Empty,
                    IThoiGianThucHien = int.TryParse(item.IThoiGianThucHien, out dngay) ? dngay : 0,
                    FTienHopDong = double.Parse(item.FTienHopDong.Replace(",", "")),
                    BActive = true,
                    BIsGoc = true, 
                    DDateCreate = DateTime.Now,
                    SUserCreate = _sessionService.Current.Principal,
                    IIdHopDongGocId = iId
                });
            }
            _vdtDaTtHopDongService.AddRange(result);
        }

        private List<ImportErrorItem> ValidateItem(VdtDaTtHopDongImportModel item, int rowIndex, Dictionary<string, Guid> _dicMaDuAn)
        {
            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                if (string.IsNullOrEmpty(item.SMaDuAn))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "mã dự án"),
                        Row = rowIndex
                    });
                }
                else if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = string.Format(Resources.MsgErrorDataNotFound, "mã dự án"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.SSoHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Số hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "số hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.STenHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Tên hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "tên hợp đồng"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.DNgayHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Ngày lập",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "ngày lập"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.SMaLoaiHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã loại hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "mã loại hợp đồng"),
                        Row = rowIndex
                    });
                }else if (!_dicLoaiHopDong.ContainsKey(item.SMaLoaiHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã loại hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataNotFound, "mã loại hợp đồng"),
                        Row = rowIndex
                    });
                }


                if (!string.IsNullOrEmpty(item.SMaNhaThauThucHien))
                {
                    if (!_dicNhaThau.ContainsKey(item.SMaNhaThauThucHien))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã nhà thầu đại diện",
                            Error = string.Format(Resources.MsgErrorDataNotFound, "mã nhà thầu đại diện"),
                            Row = rowIndex
                        });
                    }
                }

                if (string.IsNullOrEmpty(item.FTienHopDong))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Giá trị hợp đồng",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "giá trị hợp đồng"),
                        Row = rowIndex
                    });
                }

                return errors;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);

                return new List<ImportErrorItem>();
            }
        }

        private void ShowError()
        {
            try
            {
                var errors = new HashSet<string>();
                int rowIndex = Items.IndexOf(SelectedItem);
                errors = _lstErrQlDuAn.Where(x => x.Row == (rowIndex + 1)).Select(x => string.Join(StringUtils.DIVISION, x.ColumnName, x.Error)).ToHashSet();
                System.Windows.MessageBox.Show(string.Join(Environment.NewLine, errors), Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnDownloadTemplate()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                Dictionary<string, object> data = new Dictionary<string, object>();

                var lstDataDuAn = _duanService.FindByCondition();
                List<VdtDmLoaiHopDong> listLoaiHopDong = _loaiHopDongService.FindAll().ToList();

                data.Add("ItemDuAn", lstDataDuAn);
                data.Add("ItemHopDong", listLoaiHopDong);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTHD, MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONG_TIN_HOP_DONG);
                var xlsFile = _exportService.Export<ProjectManagerQuery, VdtDmLoaiHopDong>(templateFileName, data);
                e.Result = new ExportResult("Template-Import-HopDong", "Template-Import-HopDong", null, xlsFile);
            }, (s, e) =>
            {
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
                IsLoading = false;
            });
        }

        #endregion

        #region Helper
        private void OnLoadNhaThau()
        {
            _dicNhaThau = new Dictionary<string, VdtDmNhaThau>();
            var datas = _nhathauService.FindAll(n => 1 == 1);
            if (datas == null) return;
            foreach (var item in datas)
            {
                if (!_dicNhaThau.ContainsKey(item.SMaNhaThau))
                    _dicNhaThau.Add(item.SMaNhaThau, item);
            }
        }

        private void OnLoadDuAn()
        {
            _dicDuAn = new Dictionary<string, Guid>();
            var datas = _duanService.FindAll(n => 1 == 1);
            if (datas == null) return;
            _dicDuAn = datas.ToDictionary(n => n.SMaDuAn, n => n.Id);
        }

        private void OnLoadLoaiHopDong()
        {
            _dicLoaiHopDong = new Dictionary<string, VdtDmLoaiHopDong>();
            var datas = _vdtDaTtHopDongService.GetAllLoaiHopDong();
            if (datas == null) return;
            foreach (var item in datas)
            {
                if (!_dicLoaiHopDong.ContainsKey(item.SMaLoaiHopDong))
                    _dicLoaiHopDong.Add(item.SMaLoaiHopDong, item);
            }
        }

        #endregion
    }
}
