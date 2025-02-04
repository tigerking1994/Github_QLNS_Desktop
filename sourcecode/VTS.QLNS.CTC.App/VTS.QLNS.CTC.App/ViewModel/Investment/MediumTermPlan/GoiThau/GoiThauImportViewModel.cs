using AutoMapper;
using ControlzEx.Standard;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
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
using VTS.QLNS.CTC.Utility.Enum;
using static VTS.QLNS.CTC.Utility.Enum.MediumTermPlan;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.MediumTermPlan.GoiThau
{
    public class GoiThauImportViewModel : ViewModelBase
    {
        #region Private
        private ISessionService _sessionService;
        private IMapper _mapper;
        private IImportExcelService _importService; 
        private INsDonViService _nsDonViService;
        private IVdtDaGoiThauService _vdtDaGoiThauService;
        private readonly IVdtDmNhaThauService _nhathauService;
        private readonly IProjectManagerService _duanService;
        private readonly IExportService _exportService;        
        private List<ImportErrorItem> _lstErrQlDuAn = new List<ImportErrorItem>();
        private Dictionary<string, Dictionary<string, VdtDaNguonVon>> _dicNguonVon;
        private Dictionary<string, Dictionary<string, VdtDaDuAnHangMuc>> _dicHangMuc;
        private Dictionary<string, VdtDmLoaiCongTrinh> _dicLoaiCongTrinh;
        private Dictionary<string, NsNguonNganSach> _dicNsNguonVon;
        Dictionary<string, VdtDmNhaThau> _dicNhaThau;
        Dictionary<string, Guid> _dicDuAn;
        private readonly ILog _logger;
        #endregion

        #region Public
        public override string Name => "IMPORT THÔNG TIN GÓI THẦU";
        public override string Description => "Chọn file Excel, thực hiện kiểm tra và import dữ liệu thông tin chung gói thầu";
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

        private ObservableCollection<VdtDaGoiThauImportModel> _items;
        public ObservableCollection<VdtDaGoiThauImportModel> Items
        {
            get => _items;
            set
            {
                SetProperty(ref _items, value);
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(IsSaveData));
            }
        }

        private VdtDaGoiThauImportModel _selectedItem;
        public VdtDaGoiThauImportModel SelectedItem
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

        public GoiThauImportViewModel(
            ISessionService sessionService,
            IMapper mapper,
            IImportExcelService importService,
            INsDonViService nsDonViService,
            IProjectManagerService projectManagerService,
            IVdtDmDonViThucHienDuAnService vdtDmDonViThucHienDuAnService,
            IVdtDmLoaiCongTrinhService loaicongtrinhService,
            INsNguonNganSachService nsNguonVonService,
            IVdtDaGoiThauService vdtDaGoiThauService,
            IVdtDmNhaThauService nhathauService,
            IExportService exportService,
            ILog logger)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nsDonViService = nsDonViService;
            _vdtDaGoiThauService = vdtDaGoiThauService;
            _nhathauService = nhathauService;
            _duanService = projectManagerService;
            _exportService = exportService;
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
            _items = new ObservableCollection<VdtDaGoiThauImportModel>();

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

                var dataImport = _importService.ProcessData<VdtDaGoiThauImportModel>(FilePath);
                var QlDuAnImportModels = new ObservableCollection<VdtDaGoiThauImportModel>(dataImport.Data);

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

        private void HandleSaveData(List<VdtDaGoiThauImportModel> items)
        {

            if (items.Count <= 0) throw new Exception("Không có bản ghi nào có thể lưu!");
            List<VdtDaGoiThau> result = new List<VdtDaGoiThau>();
            foreach (var item in items)
            {
                Guid iId = Guid.NewGuid();
                var current = new VdtDaGoiThau()
                {
                    Id = iId,
                    IIdDuAnId = _dicDuAn[item.SMaDuAn],
                    SoQuyetDinh = item.SSoQuyetDinh,
                    STenGoiThau = item.STenGoiThau,
                    FTienTrungThau = double.Parse(item.FTienTrungThau.Replace(",", "")),
                    SThoiGianThucHien = item.SThoiGianThucHien,
                    IIdNhaThauId = !string.IsNullOrEmpty(item.SMaNhaThau) ? _dicNhaThau[item.SMaNhaThau].Id : Guid.Empty,
                    BActive = true,
                    BIsGoc = true,
                    DDateCreate = DateTime.Now,
                    SUserCreate = _sessionService.Current.Principal,
                    IIdGoiThauGocId = iId
                };
                if (!string.IsNullOrEmpty(item.DNgayQuyetDinh))
                {
                    current.NgayQuyetDinh = DateUtils.CheckDateFormatAndConverter(item.DNgayQuyetDinh);
                }
                result.Add(current);
            }
            _vdtDaGoiThauService.AddRange(result);
        }

        private List<ImportErrorItem> ValidateItem(VdtDaGoiThauImportModel item, int rowIndex, Dictionary<string, Guid> _dicMaDuAn)
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
                }else if (!_dicDuAn.ContainsKey(item.SMaDuAn))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Mã dự án",
                        Error = string.Format(Resources.MsgErrorDataNotFound, "mã dự án"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.STenGoiThau))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Tên gói thầu",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "tên gói thầu"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.FTienTrungThau))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Giá gói thầu",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Giá gói thầu"),
                        Row = rowIndex
                    });
                }

                if (string.IsNullOrEmpty(item.SThoiGianThucHien))
                {
                    errors.Add(new ImportErrorItem()
                    {
                        ColumnName = "Thời gian thực hiện",
                        Error = string.Format(Resources.MsgErrorDataEmpty, "Thời gian thực hiện"),
                        Row = rowIndex
                    });
                }

                if (!string.IsNullOrEmpty(item.SMaNhaThau))
                {
                    if (!_dicNhaThau.ContainsKey(item.SMaNhaThau))
                    {
                        errors.Add(new ImportErrorItem()
                        {
                            ColumnName = "Mã nhà thầu",
                            Error = string.Format(Resources.MsgErrorDataNotFound, "mã nhà thầu"),
                            Row = rowIndex
                        });
                    }
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
                var lstDataNhaThau = _nhathauService.FindAll(n => 1 == 1);

                data.Add("ItemDuAn", lstDataDuAn);
                data.Add("ItemNhaThau", lstDataNhaThau);

                string templateFileName = Path.Combine(ExportPrefix.PATH_TL_TTGT, MediumTermPlanType.EXPORT_TEMPLATE_IMPORT_THONG_TIN_GOI_THAU);
                var xlsFile = _exportService.Export<ProjectManagerQuery, VdtDmNhaThau>(templateFileName, data);
                e.Result = new ExportResult("Template-Import-Goithau", "Template-Import-Goithau", null, xlsFile);
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

        #endregion
    }
}
