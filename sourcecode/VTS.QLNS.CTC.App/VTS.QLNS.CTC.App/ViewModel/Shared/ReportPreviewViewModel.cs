using FlexCel.Core;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using log4net;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Libs.PDFViewer;
using VTS.QLNS.CTC.Libs.PDFViewer.Enum;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Service.Impl;
using System.Linq;
using FlexCel.Render;
using FlexCel.Pdf;
using FlexCel.XlsAdapter;
using VTS.QLNS.CTC.App.Helper;
using System.Windows;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class ReportPreviewViewModel : GridViewModelBase<PdfFileModel>
    {
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;
        private readonly string _applicationLocation;
        private readonly string _pdfAppPath;
        private ICollectionView _dataCollectionView;
        private readonly string _protectPassword = "123";
        private readonly string _backUpTmpPath;
        private readonly string _tmpPath;

        public override string Name => "REPORT PDF VIEWER";
        public override string Title => Path.GetFileName(SelectedItem.FilePath);
        public override Type ContentType => typeof(ReportPreview);
        public override PackIconKind IconKind => PackIconKind.PrinterPreview;
        //public PageRowDisplayType PageRowDisplay {  get; set; }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if (SelectedItem != null)
            {
                OnPropertyChanged(nameof(Title));
                Open();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetProperty(ref _selectedIndex, value);
            }
        }

        private string _searchKeyword;
        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    _dataCollectionView.Refresh();
                }
            }
        }

        private int _pageIndex;
        public int PageIndex
        {
            get => _pageIndex;
            set => SetProperty(ref _pageIndex, value);
        }

        private int _totalPage;
        public int TotalPage
        {
            get => _totalPage;
            set => SetProperty(ref _totalPage, value);
        }

        private int _currentZoom;
        public int CurrentZoom
        {
            get => _currentZoom;
            set
            {
                SetProperty(ref _currentZoom, value);
            }
        }

        private PdfViewer _pdfViewer;
        public PdfViewer PdfViewer
        {
            get => _pdfViewer;
            set => SetProperty(ref _pdfViewer, value);
        }

        public RelayCommand ZoomInCommand { get; }
        public RelayCommand ZoomOutCommand { get; }
        public RelayCommand CustomZoomCommand { get; }
        public RelayCommand FitWidthCommand { get; }
        public RelayCommand FitHeightCommand { get; }
        public RelayCommand RotateRightCommand { get; }
        public RelayCommand RotateLeftCommand { get; }
        public RelayCommand NextPageCommand { get; }
        public RelayCommand PreviousPageCommand { get; }
        public RelayCommand FirstPageCommand { get; }
        public RelayCommand LastPageCommand { get; }
        public RelayCommand GotoPageCommand { get; }
        public RelayCommand TogglePageDisplayCommand { get; }
        public RelayCommand SinglePageCommand { get; }
        public RelayCommand FacingCommand { get; }
        public RelayCommand BookViewCommand { get; }
        public RelayCommand DownloadCommand { get; }
        public RelayCommand PrintCommand { get; }
        public RelayCommand MovePrevCommand { get; }
        public RelayCommand MoveNextCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand ExportExcelCommand { get; }
        public RelayCommand OpenTemplateFileCommand { get; }
        public RelayCommand RevertTemplateCommand { get; }

        public ReportPreviewViewModel(
            IConfiguration configuration,
            ILog logger)
        {
            _logger = logger;
            _configuration = configuration;
            _applicationLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
#if x86
            _pdfAppPath = Path.Combine(_applicationLocation, _configuration.GetSection("Pdf32Location").Value);
#else
            _pdfAppPath = Path.Combine(_applicationLocation, _configuration.GetSection("Pdf64Location").Value);
#endif
            _backUpTmpPath = Path.Combine(IOExtensions.ApplicationPath, _configuration.GetSection(ConfigHelper.BACKUP_TEMPLATE_XLXSPATH).Value);
            _tmpPath = Path.Combine(IOExtensions.ApplicationPath, _configuration.GetSection(ConfigHelper.TEMPLATE_XLXSPATH).Value);
            ZoomInCommand = new RelayCommand(obj => OnZoomIn(obj));
            ZoomOutCommand = new RelayCommand(obj => OnZoomOut(obj));
            CustomZoomCommand = new RelayCommand(obj => OnCustomZoom(obj));
            FitWidthCommand = new RelayCommand(obj => OnFitWidth(obj));
            FitHeightCommand = new RelayCommand(obj => OnFitHeight(obj));
            RotateRightCommand = new RelayCommand(obj => OnRotateRight(obj));
            RotateLeftCommand = new RelayCommand(obj => OnRotateLeft(obj));
            NextPageCommand = new RelayCommand(obj => OnNextPage(obj));
            PreviousPageCommand = new RelayCommand(obj => OnPreviousPage(obj));
            FirstPageCommand = new RelayCommand(obj => OnFirstPage(obj));
            LastPageCommand = new RelayCommand(obj => OnLastPage(obj));
            GotoPageCommand = new RelayCommand(obj => OnGotoPage(obj));
            TogglePageDisplayCommand = new RelayCommand(obj => OnTogglePageDisplay(obj));
            SinglePageCommand = new RelayCommand(obj => OnSinglePage(obj));
            FacingCommand = new RelayCommand(obj => OnFacing(obj));
            BookViewCommand = new RelayCommand(obj => OnBookView(obj));
            DownloadCommand = new RelayCommand(obj => OnDownload(obj));
            PrintCommand = new RelayCommand(obj => OnPrint(obj), obj => Items != null && Items.Count > 0);
            ExportExcelCommand = new RelayCommand(obj => OnExportExcel());
            MovePrevCommand = new RelayCommand(obj => OnMovePrev(), obj => SelectedIndex != 0);
            MoveNextCommand = new RelayCommand(obj => OnMoveNext(), obj => SelectedIndex != Items.Count - 1);
            SearchCommand = new RelayCommand(obj => OnSearch());
            OpenTemplateFileCommand = new RelayCommand(obj => OnOpenTemplateFile());
            RevertTemplateCommand = new RelayCommand(obj => OnRevertTemplate());
        }

        private void OnRevertTemplate()
        {
            if (Items.Count() == 0 || SelectedItem == null)
                return;
            try
            {
                MessageBoxResult rs = MessageBoxHelper.Confirm("Bạn có chắc chắn muốn rollback file template này?");
                if (MessageBoxResult.Yes.Equals(rs))
                {
                    string filePath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                        SelectedItem.ExcelFile.ActiveFileName);
                    File.Copy(filePath.Replace(_tmpPath, _backUpTmpPath), filePath, true);
                    MessageBoxHelper.Info("Chỉnh sửa thành công");
                }
            }
            catch (IOException)
            {
                MessageBoxHelper.Info("Vui lòng đóng file trước khi chỉnh sửa ");
            }
        }

        private void OnOpenTemplateFile()
        {
            if (Items.Count() == 0 || SelectedItem == null)
                return;
            string filePath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), 
                SelectedItem.ExcelFile.ActiveFileName);
            if (!string.IsNullOrEmpty(SelectedItem.ExcelFile.ActiveFileName) && File.Exists(filePath))
            {
                try
                {
                    /*XlsFile xlsFile = new XlsFile(true);
                    xlsFile.Open(filePath);
                    TSheetProtectionOptions sheetProtectionOptions = new TSheetProtectionOptions(true);
                    sheetProtectionOptions.RowFormatting = true;
                    sheetProtectionOptions.ColumnFormatting = true;
                    sheetProtectionOptions.CellFormatting = true;
                    sheetProtectionOptions.SelectUnlockedCells = true;
                    sheetProtectionOptions.SelectLockedCells = true;
                    xlsFile.Protection.SetSheetProtection(_protectPassword, sheetProtectionOptions);
                    xlsFile.Save(filePath);*/
                    System.Diagnostics.Process.Start(filePath);
                }
                catch (IOException)
                {
                    MessageBoxHelper.Info("Vui lòng đóng file trước khi chỉnh sửa ");
                }
            }
        }

        private void OnSearch()
        {
            _dataCollectionView.Refresh();
        }

        private void OnMoveNext()
        {
            if (SelectedIndex == Items.Count - 1)
                return;
            SelectedItem = Items[SelectedIndex + 1];
        }

        private void OnMovePrev()
        {
            if (SelectedIndex == 0)
                return;
            SelectedItem = Items[SelectedIndex - 1];
        }

        public override void Init()
        {
            base.Init();

            _dataCollectionView = CollectionViewSource.GetDefaultView(Items);
            _dataCollectionView.Filter = ItemsViewFilter;

            SelectedItem = Items[0];
            PdfViewer = new PdfViewer()
            {
                Background = Brushes.LightGray,
                ViewType = ViewType.SinglePage,
                PageRowDisplay = PageRowDisplayType.ContinuousPageRows,
            };
            PdfViewer.Loaded += PdfViewer_Loaded;
            PdfViewer.ZoomChanged += PdfViewer_ZoomChanged;
            PdfViewer.ScrollChanged += PdfViewer_ScrollChanged;
        }

        private bool ItemsViewFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchKeyword))
            {
                return true;
            }
           return obj is PdfFileModel item
                   && item.FileName.ToLower().Contains(_searchKeyword!.ToLower());
        }

        private void PdfViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // First load
            Open();
        }

        private void PdfViewer_ZoomChanged(object sender, EventArgs e)
        {
            CurrentZoom = (int)(PdfViewer.CurrentZoom * 100);
        }

        private void PdfViewer_ScrollChanged(object sender, EventArgs e)
        {
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void Open()
        {
            try
            {
                if (PdfViewer == null) return;
                PdfViewer.OpenFile(SelectedItem.FilePath);
                PdfViewer.ZoomToHeight();
                CurrentZoom = (int)(PdfViewer.CurrentZoom * 100);
                TotalPage = PdfViewer.TotalPages;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnZoomIn(object obj)
        {
            PdfViewer.ZoomIn();
        }

        private void OnZoomOut(object obj)
        {
            PdfViewer.ZoomOut();
        }

        private void OnCustomZoom(object obj)
        {
            double zoomFactor = (double)CurrentZoom / 100;
            PdfViewer.Zoom(zoomFactor);
        }

        private void OnFitWidth(object obj)
        {
            PdfViewer.ZoomToWidth();
        }

        private void OnFitHeight(object obj)
        {
            PdfViewer.ZoomToHeight();
        }

        private void OnRotateRight(object obj)
        {
            PdfViewer.RotateRight();
        }

        private void OnRotateLeft(object obj)
        {
            PdfViewer.RotateLeft();
        }

        private void OnNextPage(object obj)
        {
            PdfViewer.GotoNextPage();
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void OnPreviousPage(object obj)
        {
            PdfViewer.GotoPreviousPage();
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void OnFirstPage(object obj)
        {
            PdfViewer.GotoFirstPage();
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void OnLastPage(object obj)
        {
            PdfViewer.GotoLastPage();
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void OnGotoPage(object obj)
        {
            PdfViewer.GotoPage(_pageIndex);
            PageIndex = PdfViewer.GetCurrentPageNumber();
        }

        private void OnTogglePageDisplay(object obj)
        {
            PdfViewer.TogglePageDisplay();
        }

        private void OnSinglePage(object obj)
        {
        }

        private void OnFacing(object obj)
        {
        }

        private void OnBookView(object obj)
        {
        }

        private void OnDownload(object obj)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = Path.GetFileName(SelectedItem.FilePath);
                dialog.Filter = IOExtensions.FileDialogFilterByExtension(FileExtensionFormats.Pdf);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    string pdfFileMerge = this.PdfFilesMerge();
                    
                    File.Copy(pdfFileMerge, fileName, true);
                    IOExtensions.OpenFiles(fileName);
                }
            }
        }

        private void OnPrint(object obj)
        {
            try
            {
                if (File.Exists(_pdfAppPath))
                {
                    string filename = PdfFilesMerge();
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = _pdfAppPath,
                        Arguments = string.Format("-print-dialog -exit-when-done \"{0}\"", filename)
                    };
                    Process.Start(processStartInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void OnExportExcel()
        {
            try
            {
                if (SelectedItem != null)
                {
                    ExcelFile xlsFile = SelectedItem.ExcelFile;
                    xlsFile.Protection.SetSheetProtectionOptions(null);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectedItem.FilePath);
                    using (var dialog = new SaveFileDialog())
                    {
                        var ext = FileExtensionFormats.Xlsx;
                        dialog.Title = Resources.MsgSaveFile;
                        dialog.RestoreDirectory = true;
                        dialog.FileName = fileNameWithoutExtension + ext;
                        dialog.Filter = IOExtensions.FileDialogFilterByExtension(ext);
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = dialog.FileName;
                            xlsFile.Save(fileName, TFileFormats.Xlsx);
                            IOExtensions.OpenFiles(fileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        private string PdfFilesMerge()
        {
            string filename = IOExtensions.CreateTempFile(Path.GetDirectoryName(SelectedItem.FilePath), FileExtensionFormats.Pdf);
            var excelFiles = Items.Select(x => x.ExcelFile).ToArray();
            if (excelFiles.Length == 1)
            {
                return SelectedItem.FilePath;
            }
            using (FlexCelPdfExport flexCelPdfExport = new FlexCelPdfExport())
            {
                flexCelPdfExport.FontEmbed = TFontEmbed.Embed;
                flexCelPdfExport.FontMapping = TFontMapping.ReplaceStandardFonts;
                flexCelPdfExport.AllowOverwritingFiles = true;
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    flexCelPdfExport.BeginExport(fileStream);
                    for (int index = 0; index < excelFiles.Length; index++)
                    {
                        ExcelFile xlsFile = excelFiles[index];
                        flexCelPdfExport.Workbook = xlsFile;
                        flexCelPdfExport.ExportAllVisibleSheets(true, string.Format("File pdf {0}", index + 1));
                    }
                    flexCelPdfExport.EndExport();
                }
            }
            return filename;
        }
    }
}
