using Aspose.Cells;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class ChooseFileViewModel<TModel, TEntity, TService, TImportModel> : ViewModelBase
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
        where TImportModel : BaseImportModel
    {
        private IExportService _exportService;
        private TService _service;
        private IServiceProvider _provider;

        public override string Name => "1. Chọn tệp nguồn";
        public override Type ContentType => typeof(View.Shared.Import.ChooseFile);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public string DataTemplateFileName { get; set; }
        public ObservableCollection<ComboboxItem> Sheets { get; set; }
        public AuthenticationInfo AuthenticationInfo { get; set; }

        private string _selectedSheet;
        public string SelectedSheet 
        {
            get => _selectedSheet;
            set
            {
                SetProperty(ref _selectedSheet, value);
            }
        }

        public ObservableCollection<ComboboxItem> TitleRows { get; set; }
        public int TitleRow { get; set; }

        public ImportSharingData ImportSharingData { get; set; }

        private bool[] _modeArray = new bool[] { true, false };
        public bool[] ModeArray
        {
            get { return _modeArray; }
        }

        public int ImportMode
        {
            get { return Array.IndexOf(_modeArray, true); }
        }

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ExportTemplateCommand { get; }

        public ChooseFileViewModel(TService service, IServiceProvider serviceProvider)
        {
            _service = service;
            _provider = serviceProvider;
            _exportService = serviceProvider.GetService(typeof(IExportService)) as IExportService;
            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
            ExportTemplateCommand = new RelayCommand(obj => OnExportTemplate());
            TitleRows = new ObservableCollection<ComboboxItem>()
            {
                new ComboboxItem { ValueItem = "1", DisplayItem = "1"},
                new ComboboxItem { ValueItem = "2", DisplayItem = "2"},
                new ComboboxItem { ValueItem = "3", DisplayItem = "3"},
                new ComboboxItem { ValueItem = "4", DisplayItem = "4"},
                new ComboboxItem { ValueItem = "5", DisplayItem = "5"},
                new ComboboxItem { ValueItem = "6", DisplayItem = "6"},
                new ComboboxItem { ValueItem = "7", DisplayItem = "7"},
                new ComboboxItem { ValueItem = "8", DisplayItem = "8"},
                new ComboboxItem { ValueItem = "9", DisplayItem = "9"},
                new ComboboxItem { ValueItem = "10", DisplayItem = "10"},
            };
        }

        public override void Init()
        {
            base.Init();
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn file excel";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ImportSharingData.FileName = openFileDialog.FileName;
            OnPropertyChanged(nameof(ImportSharingData.FileName));
            Workbook workbook = new Workbook(ImportSharingData.FileName);
            WorksheetCollection worksheets = workbook.Worksheets;
            Sheets = new ObservableCollection<ComboboxItem>(worksheets.Select(t => new ComboboxItem { DisplayItem = t.Name, ValueItem = t.Name }));
            OnPropertyChanged("Sheets");
            if (Sheets.Count != 0)
            {
                SelectedSheet = Sheets[0].ValueItem;
                ImportSharingData.SelectedSheet = SelectedSheet;
            }
        }

        public bool IsValidToGoNext()
        {
            return SelectedSheet != null && ImportSharingData.FileName != null && ImportSharingData.TitleRow > 0;
        }

        private void OnExportTemplate()
        {
            try
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    IEnumerable<TEntity> DataTbl = _service.FindDataToExportTemplate(AuthenticationInfo);
                    data.Add("Items", DataTbl);

                    string templateFileName = DataTemplateFileName;
                    string fileNamePrefix = Path.GetFileNameWithoutExtension(DataTemplateFileName);
                    string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                    var xlsFile = _exportService.Export<TEntity>(templateFileName, data);
                    e.Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                }, (s, e) =>
                {
                    if (e.Error == null)
                    {
                        var result = (ExportResult)e.Result;
                        if (result != null)
                        {
                            _exportService.Open(result, Utility.Enum.ExportType.EXCEL);
                        }
                    }
                    else
                    {
                    }
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
