using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class ReportViewModelBase<MODEL, DATA, EXPORT_DATA> : ViewModelBase
    {
        public readonly IExportService _exportService;
        private readonly IDanhMucService _danhMucService;
        private ISessionService _sessionService;

        private MODEL _model;
        public MODEL Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        private ObservableCollection<MODEL> _models;
        public ObservableCollection<MODEL> Models
        {
            get => _models;
            set => SetProperty(ref _models, value);
        }

        private ObservableCollection<ComboboxItem> _catUnitTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> CatUnitTypes
        {
            get => _catUnitTypes;
            set => SetProperty(ref _catUnitTypes, value);
        }

        private ComboboxItem _catUnitTypeSelected;
        public ComboboxItem CatUnitTypeSelected
        {
            get => _catUnitTypeSelected;
            set => SetProperty(ref _catUnitTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _paperPrintTypes = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> PaperPrintTypes
        {
            get => _paperPrintTypes;
            set => SetProperty(ref _paperPrintTypes, value);
        }

        private ComboboxItem _paperPrintTypeSelected;
        public ComboboxItem PaperPrintTypeSelected
        {
            get => _paperPrintTypeSelected;
            set => SetProperty(ref _paperPrintTypeSelected, value);
        }

        private ObservableCollection<ComboboxItem> _printTypeMLNS = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> PrintTypeMLNS
        {
            get => _printTypeMLNS;
            set => SetProperty(ref _printTypeMLNS, value);
        }

        private ComboboxItem _selectedPrintTypeMLNS;
        public ComboboxItem SelectedPrintTypeMLNS
        {
            get => _selectedPrintTypeMLNS;
            set => SetProperty(ref _selectedPrintTypeMLNS, value);
        }

        public RelayCommand ExportExcelActionCommand { get; }
        public RelayCommand ExportPdfActionCommand { get; }
        public RelayCommand ExportSignatureActionCommand { get; }
        public RelayCommand PrintActionCommand { get; }

        public ReportViewModelBase()
        {
            ExportExcelActionCommand = new RelayCommand(OnExportExcel);
            ExportPdfActionCommand = new RelayCommand(OnExportPdf);
            ExportSignatureActionCommand = new RelayCommand(OnExportSignature);
            PrintActionCommand = new RelayCommand(OnPrint);
        }

        public ReportViewModelBase(IExportService exportService, IDanhMucService danhMucService, ISessionService sessionService) : this()
        {
            _exportService = exportService;
            _danhMucService = danhMucService;
            _sessionService = sessionService;
        }

        public virtual void Export(object obj, ExportType type)
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                List<ExportResult> results = new List<ExportResult>();
                List<Tuple<string, string, Dictionary<string, object>>> dataExport = new List<Tuple<string, string, Dictionary<string, object>>>();
                switch (type)
                {
                    case ExportType.EXCEL:
                        dataExport = ConvertDataExport(GetData(), StringUtils.EXCEL_EXTENSION);
                        break;
                    case ExportType.PDF:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.WORD:
                        break;
                    case ExportType.BROWSER:
                        dataExport = ConvertDataExport(GetData(), StringUtils.PDF_EXTENSION);
                        break;
                    case ExportType.SIGNATURE:
                        break;
                }
                foreach (var item in dataExport)
                {
                    List<int> hideColumns = new List<int>();
                    if (SelectedPrintTypeMLNS != null)
                        hideColumns = ExportExcelHelper<EXPORT_DATA>.HideColumn(SelectedPrintTypeMLNS.ValueItem);
                    var xlsFile = _exportService.Export<EXPORT_DATA>(item.Item1, item.Item3, hideColumns);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item.Item2);
                    results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                }
                e.Result = results;
            },
            (s, e) =>
            {
                if (e.Error == null && (type.Equals(ExportType.EXCEL) || type.Equals(ExportType.PDF) || type.Equals(ExportType.BROWSER)))
                {
                    var result = (List<ExportResult>)e.Result;
                    if (result.Count() == 0)
                        MessageBoxHelper.Info(Resources.AlertEmptyReport);
                    else _exportService.Open(result, type.Equals(ExportType.EXCEL) ? ExportType.EXCEL : ExportType.PDF);
                }
                IsLoading = false;
            });
            //HandleAfterExport();
        }

        public virtual void HandleAfterExport()
        {
        }

        public virtual string GetFileTemplate(string strPageNumber = "")
        {
            return string.Empty;
        }

        public virtual IEnumerable<DATA> GetData()
        {
            return new List<DATA>();
        }

        public virtual List<Tuple<string, string, Dictionary<string, object>>> ConvertDataExport(IEnumerable<DATA> dataExport, string extension)
        {
            return new List<Tuple<string, string, Dictionary<string, object>>>();
        }

        public virtual void OnExportExcel(object obj)
        {
            Export(obj, ExportType.EXCEL);
        }

        public virtual void OnExportPdf(object obj)
        {
            Export(obj, ExportType.PDF);
        }
        public virtual void OnExportSignature(object obj)
        {
            Export(obj, ExportType.SIGNATURE);
        }

        public virtual void OnPrint(object obj)
        {
            Export(obj, ExportType.BROWSER);
        }

        public virtual void LoadCatUnitTypes()
        {
            var expenseTypes = new List<ComboboxItem>();
            var listDonViTinh = _danhMucService.FindByCondition(x => x.SType.Contains(TypeDanhMuc.DM_DONVITINH) && x.ITrangThai == StatusType.ACTIVE && x.INamLamViec == _sessionService.Current.YearOfWork).OrderBy(n => n.SGiaTri).ToList();
            if (listDonViTinh.Count == 0)
                expenseTypes.Add(new ComboboxItem("Đồng", "1"));
            foreach (var dvt in listDonViTinh)
            {
                ComboboxItem cb = new ComboboxItem();
                cb.DisplayItem = dvt.STen;
                cb.ValueItem = dvt.SGiaTri;
                cb.Type = dvt.SMoTa;
                expenseTypes.Add(new ComboboxItem(dvt.STen, dvt.SGiaTri));
            }
            CatUnitTypes = new ObservableCollection<ComboboxItem>(expenseTypes);
            CatUnitTypeSelected = expenseTypes.ElementAt(0);
        }

        public virtual void LoadPaperPrintTypes()
        {
            var paperPrintTypes = new List<ComboboxItem>
            {
                new ComboboxItem {DisplayItem = "A4 dọc", ValueItem = "1"},
                new ComboboxItem {DisplayItem = "A4 ngang", ValueItem = "2"},
            };

            PaperPrintTypes = new ObservableCollection<ComboboxItem>(paperPrintTypes);
            PaperPrintTypeSelected = paperPrintTypes.ElementAt(0);
        }
    }
}
