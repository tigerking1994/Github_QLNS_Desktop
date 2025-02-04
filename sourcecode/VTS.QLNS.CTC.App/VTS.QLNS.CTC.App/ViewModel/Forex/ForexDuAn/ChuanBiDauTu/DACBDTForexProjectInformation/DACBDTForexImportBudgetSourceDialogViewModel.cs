using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.App.Command;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Windows.Forms;
using System.Windows;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Utility;
using System.IO;
using VTS.QLNS.CTC.Core.Domain;
using FlexCel.XlsAdapter;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation
{
    public class DACBDTForexImportBudgetSourceDialogViewModel : ViewModelBase
    {
        private readonly ISessionService _sessionService;
        private ILog _logger;
        private readonly IImportExcelService _importService;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly INhDmLoaiCongTrinhService _nhDMLoaiCongTrinh;
        private readonly INsNguonNganSachService _nsNguonNganSachService;
        private readonly IExportService _exportService;


        public override Type ContentType => typeof(View.Forex.ForexDuAn.ChuanBiDauTu.DACBDTForexProjectInformation.DACBDTForexImportBudgetSourceDialog);

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

        private ObservableCollection<NhDaDuAnHangMucModel> _itemsDuAnHangMucImport;
        public ObservableCollection<NhDaDuAnHangMucModel> ItemsDuAnHangMucImport
        {
            get => _itemsDuAnHangMucImport;
            set => SetProperty(ref _itemsDuAnHangMucImport, value);
        }

        private List<ImportErrorItem> _importErrors;
        public List<ImportErrorItem> ImportErrors
        {
            get => _importErrors;
            set => SetProperty(ref _importErrors, value);
        }

        private List<NhDmLoaiCongTrinh> _itemsLoaiCongTrinh;
        public List<NhDmLoaiCongTrinh> ItemsLoaiCongTrinh
        {
            get => _itemsLoaiCongTrinh;
            set => SetProperty(ref _itemsLoaiCongTrinh, value);
        }

        private NhDaDuAnHangMucModel _selectedHangMuc;
        public NhDaDuAnHangMucModel SelectedHangMuc
        {
            get => _selectedHangMuc;
            set => SetProperty(ref _selectedHangMuc, value);
        }

        public bool IsSaveData
        {
            get
            {
                if (ItemsDuAnHangMucImport.Count > 0)
                    return !ItemsDuAnHangMucImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus);
                return false;
            }
        }

        public DACBDTForexImportBudgetSourceDialogViewModel(
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
                ImportResult<NhDaDuAnHangMucImportModel> dataImport = _importService.ProcessData<NhDaDuAnHangMucImportModel>(FileName);
                List<NhDaDuAnHangMucModel> listDataResult = _mapper.Map<List<NhDaDuAnHangMucModel>>(dataImport.Data);
                ItemsDuAnHangMucImport = new ObservableCollection<NhDaDuAnHangMucModel>(listDataResult);
                _itemsLoaiCongTrinh = _nhDMLoaiCongTrinh.FindAll().ToList();
                if (ItemsDuAnHangMucImport.Any())
                {
                    ItemsDuAnHangMucImport.ForAll(f =>
                    {
                        if (_itemsLoaiCongTrinh.Any(x => x.SMaLoaiCongTrinh.Equals(f.SMaLoaiCongTrinh.Replace(',', '.'))))
                        {
                            var itemLoaiCT = _itemsLoaiCongTrinh.FirstOrDefault(x => x.SMaLoaiCongTrinh.Equals(f.SMaLoaiCongTrinh.Replace(',', '.')));
                            f.STenLoaiCongTrinh = itemLoaiCT.STenLoaiCongTrinh;
                            f.IIdLoaiCongTrinhId = itemLoaiCT.Id;
                            f.SMaOrder = f.STT;
                            f.SMaHangMuc = f.STT;
                        }
                        else
                        {
                            f.IsWarning = true;
                        }
                    });
                }
                if (dataImport.ImportErrors.Count > 0)
                    errors.AddRange(dataImport.ImportErrors);

                if (!ItemsDuAnHangMucImport.Any())
                {
                    System.Windows.MessageBox.Show(Resources.FileImportEmpty, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (ItemsDuAnHangMucImport.Where(x => !x.IsWarning).Any(x => !x.ImportStatus))
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
            ItemsDuAnHangMucImport = new ObservableCollection<NhDaDuAnHangMucModel>();
            _importErrors = new List<ImportErrorItem>();
            OnPropertyChanged(nameof(FileName));
            OnPropertyChanged(nameof(ImportErrors));
            OnPropertyChanged(nameof(IsSaveData));
        }

        private void ShowErrorHangMuc()
        {
            int rowIndex = ItemsDuAnHangMucImport.IndexOf(SelectedHangMuc);
            List<string> errors = _importErrors.Where(x => x.Row == rowIndex).Select(x => { return string.Format("{0} - {1}", x.ColumnName, x.Error); }).ToList();
            string message = string.Join(Environment.NewLine, errors);
            System.Windows.MessageBox.Show(message, "Thông tin lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void OnSaveData(object obj)
        {
            try
            {
                foreach (var item in ItemsDuAnHangMucImport)
                {
                    item.Id = Guid.NewGuid();
                    item.IsAdded = true;
                }
                MessageBoxHelper.Info("Import dữ liệu thành công");
                SavedAction?.Invoke(ItemsDuAnHangMucImport);
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
                    templateFileName = Path.Combine(ExportPrefix.PATH_NH_DUAN, ExportFileName.RPT_HANGMUC);
                    fileNameWithoutExtension = StringUtils.CreateExportFileName(Path.GetFileNameWithoutExtension(templateFileName));
                    var listLoaiCongTrinh = _nhDMLoaiCongTrinh.FindAll().OrderBy(x => x.SMaLoaiCongTrinh);
                    data.Add("ItemsLoaiCongTrinh", listLoaiCongTrinh);

                    var xlsFile = _exportService.Export<NhDmLoaiCongTrinh>(templateFileName, data);
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
