using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class MappingDataViewModel : ViewModelBase
    {
        public override string Name => "2. Ghép dữ liệu";
        public override Type ContentType => typeof(View.Shared.Import.MappingData);

        public ObservableCollection<MappingInfo> MappingInfos { get; set; }
        public ObservableCollection<ComboboxItem> ExcelColumns { get; set; }
        public ObservableCollection<ImportField> ImportFields { get; set; }

        public ImportSharingData ImportSharingData { get; set; }

        public MappingDataViewModel()
        {

        }

        public override void Init()
        {
            base.Init();
            ExcelColumns = new ObservableCollection<ComboboxItem>();

            Worksheet worksheet = new Workbook(ImportSharingData.FileName).Worksheets[ImportSharingData.SelectedSheet];
            int totalColumns = worksheet.Cells.MaxDataColumn + 1;
            int row = ImportSharingData.TitleRow.Value - 1;
            for (int i = 0; i < totalColumns; i++)
            {
                ComboboxItem comboboxItem = new ComboboxItem
                {
                    ValueItem = i.ToString(),
                    DisplayItem = (i + 1).ToString() + " - " + worksheet.Cells[row, i].StringValue,
                };
                ExcelColumns.Add(comboboxItem);
            }
            OnPropertyChanged(nameof(ExcelColumns));

            ImportFields = new ObservableCollection<ImportField>();
            foreach (var prop in ImportSharingData.ImportType.GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(ColumnAttribute)))
                {
                    ColumnAttribute attribute = Attribute.GetCustomAttribute(prop, typeof(ColumnAttribute)) as ColumnAttribute;
                    ImportField importField = new ImportField
                    {
                        IsRequired = false,
                        DataCol = attribute.DBCol,
                        DisplayCol = attribute.ColumnName,
                        ExcelColVal = attribute.ColumnIndex,
                        Description = attribute.Description,
                        ExcelCol = attribute.ColumnIndex + " - " + attribute.ColumnName,
                        PropertyName = prop.Name
                    };
                    ImportFields.Add(importField);
                }
            }
            OnPropertyChanged(nameof(ImportFields));
        }

        public void SetImportFields()
        {
            ImportSharingData.ImportFields = ImportFields;
        }
    }
}
