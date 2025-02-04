using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Import;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class ImportResultViewModel<TImportModel> : ViewModelBase where TImportModel : BaseImportModel
    {
        public override string Name => "5. Kết quả";
        public override Type ContentType => typeof(View.Shared.Import.ImportResult);

        public ImportSharingData ImportSharingData { get; set; }

        public ObservableCollection<object> ImportResults { get; set; }

        public RelayCommand AutoGeneratingColumnsCommand { get; }

        public ImportResultViewModel(ImportSharingData importSharingData)
        {
            ImportSharingData = importSharingData;
            AutoGeneratingColumnsCommand = new RelayCommand(obj => OnAutoGeneratingColumnsCommand(obj));
        }

        public override void Init()
        {
            base.Init();
            ImportResults = new ObservableCollection<object>(ImportSharingData.ImportResults);
            OnPropertyChanged(nameof(ImportResults));
        }

        private void OnAutoGeneratingColumnsCommand(object obj)
        {
            if (obj is DataGridAutoGeneratingColumnEventArgs e)
            {
                var property = typeof(TImportModel).GetProperty(e.PropertyName);
                if (e.PropertyDescriptor is PropertyDescriptor descriptor)
                {
                    if (Attribute.IsDefined(property, typeof(DisplayDetailInfoAttribute)))
                    {
                        DisplayDetailInfoAttribute attribute = (DisplayDetailInfoAttribute)Attribute.GetCustomAttribute(property, typeof(DisplayDetailInfoAttribute));
                        e.Column.IsReadOnly = attribute.IsReadOnly;
                        e.Column.Header = attribute.Name;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
