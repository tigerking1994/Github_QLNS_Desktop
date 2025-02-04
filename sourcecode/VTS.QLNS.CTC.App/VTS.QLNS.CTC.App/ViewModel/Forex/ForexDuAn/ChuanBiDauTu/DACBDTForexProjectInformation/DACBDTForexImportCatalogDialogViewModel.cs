using AutoMapper;
using FlexCel.XlsAdapter;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.IO;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation
{
    public class DACBDTForexImportCatalogDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly INhDmLoaiCongTrinhService _nhDMLoaiCongTrinh;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IExportService _exportService;


        public override Type ContentType => typeof(View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation.DACBDTForexImportCatalogDialog);

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand DownloadTemplateCommand { get; }
        public RelayCommand ProcessFileCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand ResetDataCommand { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ShowErrorHangMucCommand { get; }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private ObservableCollection<NhDaDuAnNguonVonModel> _itemsDuAnNguonVonImport;
        public ObservableCollection<NhDaDuAnNguonVonModel> ItemsDuAnNguonVonImport
        {
            get => _itemsDuAnNguonVonImport;
            set => SetProperty(ref _itemsDuAnNguonVonImport, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private List<NsNguonNganSach> _itemsNguonVon;
        public List<NsNguonNganSach> ItemsNguonVon
        {
            get => _itemsNguonVon;
            set => SetProperty(ref _itemsNguonVon, value);
        }

        private NhDaDuAnNguonVonModel _selectedNguonVon;
        public NhDaDuAnNguonVonModel SelectedNguonVon
        {
            get => _selectedNguonVon;
            set => SetProperty(ref _selectedNguonVon, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (ItemsDuAnNguonVonImport.Count > 0)
                    return !ItemsDuAnNguonVonImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        public DACBDTForexImportCatalogDialogViewModel(
            ISessionService sessionService,
            ILog logger,
            IImportExcelService importService,
            IMapper mapper,
            INhDmLoaiCongTrinhService nhDmLoaiCongTrinhService,
            INsNguonNganSachService nsNguonNganSachService,
            IExportService exportService)
        {
            _logger = logger;
            _sessionService = sessionService;
            _mapper = mapper;
            _importService = importService;
            _nhDMLoaiCongTrinh = nhDmLoaiCongTrinhService;


            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ProcessFileCommand = new RelayCommand(obj => OnProcessFile());
            SaveCommand = new RelayCommand(obj => OnSaveData(obj));
            ResetDataCommand = new RelayCommand(obj => OnResetData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
            ShowErrorHangMucCommand = new RelayCommand(obj => ShowErrorHangMuc());
            DownloadTemplateCommand = new RelayCommand(obj => OnDownloadTemplate());
            _nsNguonNganSachService = nsNguonNganSachService;
            _exportService = exportService;
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            OnResetData();
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = string.Format("Chọn file excel");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            FileName = openFileDialog.FileName;
        }

        private void OnProcessFile()
        {
            string message = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                message = Resources.ErrorFileEmpty;
                goto ShowError;
            }
        ShowError:
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                List<ImportErrorItem> errors = new List<ImportErrorItem>();
                XlsFile xls = new XlsFile(false);
                xls.Open(FileName);
                xls.ActiveSheet = 1;
                ImportResult<NhDaDuAnNguonVonImportModel> dataImport = _importService.ProcessData<NhDaDuAnNguonVonImportModel>(FileName);
                List<NhDaDuAnNguonVonModel> listDataResult = _mapper.Map<List<NhDaDuAnNguonVonModel>>(dataImport.Data);
                ItemsDuAnNguonVonImport = new ObservableCollection<NhDaDuAnNguonVonModel>(listDataResult);
                ItemsNguonVon = _nsNguonNganSachService.FindAll().ToList();
                if (ItemsDuAnNguonVonImport.Any())
                {
                    ItemsDuAnNguonVonImport.ForAll(f =>
                    {
                        if (_itemsNguonVon.Any(x => x.IIdMaNguonNganSach.Equals(f.IIdNguonVonId)))
                        {
                            var itemNguonVon = _itemsNguonVon.FirstOrDefault(x => x.IIdMaNguonNganSach.Equals(f.IIdNguonVonId));
                            f.STenNguonVon = itemNguonVon.STen;
                            f.IIdNguonVonId = itemNguonVon.IIdMaNguonNganSach;
                        }
                        else
                        {
                            f.IsWarning = true;
                        }
                    });
                }
                if (dataImport.ImportErrors.Count > 0)
                    errors.AddRange(dataImport.ImportErrors);

                if (!ItemsDuAnNguonVonImport.Any())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (ItemsDuAnNguonVonImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
                    System.Windows.MessageBox.Show(Resources.AlertDataError, Resources.NotifiTitle, MessageBoxButton.OK, MessageBoxImage.Warning);

                OnPropertyChanged(nameof(IsSaveData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Resources.ErrorImport, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnResetData()
        {
            _fileName = string.Empty;
            ItemsDuAnNguonVonImport = new ObservableCollection<NhDaDuAnNguonVonModel>();
            _importErrors = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void ShowErrorHangMuc()
        {
            int rowIndex = ItemsDuAnNguonVonImport.IndexOf(SelectedNguonVon);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnSaveData(object obj)
        {
            try
            {
                //var lstModel = ConverData(NhGoiThauImportModels.ToList());
                //ItemsDuAnHangMucImport = new ObservableCollection<NhDaDuAnHangMucModel>(ItemsDuAnHangMucImport);
                foreach (var item in ItemsDuAnNguonVonImport)
                {
                    item.Id = Guid.NewGuid();
                    item.IsAdded = true;
                }
                MessageBoxHelper.Info("Import dữ liệu thành công");
                SavedAction?.Invoke(ItemsDuAnNguonVonImport);
                OnCloseWindow(obj);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 3)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private void OnDownloadTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    List<ExportResult> results = new List<ExportResult>();
                    string templateFileName;
                    string fileNameWithoutExtension;
                    var data = new Dictionary<string, object>();
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.RPT_NGUONVON);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    var listNguonVon = _nsNguonNganSachService.FindAll().ToList();
                    data.Add("ItemsNguonVon", listNguonVon);

                    var xlsFile = _exportService.Export<NsNguonNganSach>(templateFileName, data);
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
    }
}
