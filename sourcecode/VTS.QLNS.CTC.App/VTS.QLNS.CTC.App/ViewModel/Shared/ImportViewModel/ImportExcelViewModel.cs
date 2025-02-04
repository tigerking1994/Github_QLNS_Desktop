using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class ImportExcelViewModel<TModel, TEntity, TService, TImportModel> : ViewModelBase
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
        where TImportModel : BaseImportModel
    {
        private IServiceProvider _serviceProvider;
        private TService _service;

        public override string Name => "Import dữ liệu";
        public override Type ContentType => typeof(View.Shared.Import.ImportExcelView);
        public override PackIconKind IconKind => PackIconKind.Dollar;

        public string DataTemplateFileName { get; set; }

        private const int ChooseFileViewModel_Index = 0;
        private const int MappingDataViewModel_Index = 1;
        private const int ValidateDataViewModel_Index = 2;
        private const int ExecuteImportDataViewModel_Index = 3;
        private const int ImportResultViewModel_Index = 4;

        private int _currentIndex = 0;
        private AuthenticationInfo _authenticationInfo;

        public int SelectedIndex
        {
            get => _currentIndex;
            set
            {
                SetProperty(ref _currentIndex, value);
                OnPropertyChanged(nameof(IsEnableNextBtn));
                OnPropertyChanged(nameof(IsEnablePreviousBtn));
            }
        }

        public ImportSharingData ImportSharingData { get; set; }

        public bool IsEnableNextBtn
        {
            get => SelectedIndex != ExecuteImportDataViewModel_Index && SelectedIndex != ImportResultViewModel_Index;
        }

        public bool IsEnablePreviousBtn
        {
            get => SelectedIndex != ImportResultViewModel_Index && SelectedIndex != ChooseFileViewModel_Index;
        }

        public ObservableCollection<ViewModelBase> ViewModelBases { get; set; }
        public ChooseFileViewModel<TModel, TEntity, TService, TImportModel> ChooseFileViewModel { get; set; }
        public MappingDataViewModel MappingDataViewModel { get; set; }
        public ValidateDataViewModel<TModel, TEntity, TService, TImportModel> ValidateDataViewModel { get; set; }
        public ExecuteImportDataViewModel<TModel, TEntity, TService, TImportModel> ExecuteImportDataViewModel { get; set; }
        public ImportResultViewModel<TImportModel> ImportResultViewModel { get; set; }

        public RelayCommand NextCommand { get; }
        public RelayCommand PreviousCommand { get; }
        public RelayCommand CloseWindowCommand { get; }

        public ImportExcelViewModel(IServiceProvider serviceProvider, TService service, AuthenticationInfo authenticationInfo)
        {
            _authenticationInfo = authenticationInfo;
            _serviceProvider = serviceProvider;
            _service = service;
            NextCommand = new RelayCommand(obj => OnNext());
            PreviousCommand = new RelayCommand(obj => OnPrevious());
            CloseWindowCommand = new RelayCommand(obj => OnClose(obj));
        }

        private void OnClose(object obj)
        {
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        public void OnNext()
        {
            switch(SelectedIndex)
            {
                case ChooseFileViewModel_Index:
                    if (!ChooseFileViewModel.IsValidToGoNext())
                    {
                        MessageBox.Show("Vui lòng chọn đầy đủ dữ liệu để tiếp tục", Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ImportSharingData.ImportMode = ChooseFileViewModel.ImportMode;
                    break;
                case MappingDataViewModel_Index:
                    MappingDataViewModel.SetImportFields();
                    break;
                case ValidateDataViewModel_Index:
                    try
                    {
                        ValidateDataViewModel._authenticationInfo = _authenticationInfo;
                        if (!ValidateDataViewModel.IsValidToImport())
                        {
                            return;
                        }
                        ValidateDataViewModel.SetDataToImport();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    break;
            }
            if (SelectedIndex != ImportResultViewModel_Index)
            {
                CurrentPage = ViewModelBases[++SelectedIndex];
            }
        }

        private void OnPrevious()
        {
            if (_currentIndex != ChooseFileViewModel_Index)
            {
                CurrentPage = ViewModelBases[--SelectedIndex];
            }
        }

        public override void Init()
        {
            base.Init();
            ImportSharingData = new ImportSharingData();
            ImportSharingData.ImportType = typeof(TImportModel);
            ChooseFileViewModel = new ChooseFileViewModel<TModel, TEntity, TService, TImportModel>(_service, _serviceProvider)
            {
                ImportSharingData = this.ImportSharingData,
                DataTemplateFileName = DataTemplateFileName,
                AuthenticationInfo = _authenticationInfo
            };
            MappingDataViewModel = new MappingDataViewModel()
            {
                ImportSharingData = this.ImportSharingData
            };
            ValidateDataViewModel = new ValidateDataViewModel<TModel, TEntity, TService, TImportModel>(_serviceProvider, _service)
            {
                _authenticationInfo = this._authenticationInfo,
                ImportSharingData = this.ImportSharingData
            };
            ExecuteImportDataViewModel = new ExecuteImportDataViewModel<TModel, TEntity, TService, TImportModel>(_service)
            {
                ImportSharingData = this.ImportSharingData,
                AuthenticationInfo = _authenticationInfo,
                ImportExcelViewModel = this
            };
            ImportResultViewModel = new ImportResultViewModel<TImportModel>(ImportSharingData);
            ViewModelBases = new ObservableCollection<ViewModelBase>
            {
                ChooseFileViewModel, MappingDataViewModel, ValidateDataViewModel, ExecuteImportDataViewModel, ImportResultViewModel
            };
            CurrentPage = ViewModelBases[SelectedIndex];
            OnPropertyChanged(nameof(SelectedIndex));
        }
    }

    public class ImportSharingData : BindableBase
    {
        public Type ImportType { get; set; }

        private string _fileName;
        public string FileName 
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _selectedSheet;
        public string SelectedSheet 
        {
            get => _selectedSheet;
            set => SetProperty(ref _selectedSheet, value);
        }

        private int? _titleRow;
        public int? TitleRow 
        {
            get => _titleRow;
            set => SetProperty(ref _titleRow, value);
        }

        public int ImportMode { get; set; }

        public ObservableCollection<ImportField> ImportFields { get; set; }

        public IEnumerable<Object> Entities { get; set; }

        private int _totalRecord;
        public int TotalRecord
        {
            get => _totalRecord;
            set => SetProperty(ref _totalRecord, value);
        }

        private int _totalErrRecord;
        public int TotalErrRecord
        {
            get => _totalErrRecord;
            set => SetProperty(ref _totalErrRecord, value);
        }

        public int _totalUpdatedRecord;
        public int TotalUpdatedRecord
        {
            get => _totalUpdatedRecord;
            set => SetProperty(ref _totalUpdatedRecord, value);
        }

        private int _totalRemoveRecord;
        public int TotalRemoveRecord
        {
            get => _totalRemoveRecord;
            set => SetProperty(ref _totalRemoveRecord, value);
        }

        public int _totalValidRecord;
        public int TotalValidRecord
        {
            get => _totalValidRecord;
            set => SetProperty(ref _totalValidRecord, value);
        }

        public IEnumerable<object> ImportResults { get; set; }

    }

    public class ImportField : BindableBase
    {
        private bool _isRequired;
        public bool IsRequired
        {
            get => _isRequired;
            set => SetProperty(ref _isRequired, value);
        }

        public string DataCol { get; set; }
        public string DisplayCol { get; set; }
        public string ExcelCol { get; set; }

        private int? _excelColVal;
        public int? ExcelColVal 
        { 
            get => _excelColVal;
            set => SetProperty(ref _excelColVal, value);
        }
        public string Description { get; set; }
        public string PropertyName { get; set; }
    }

    public class MappingInfo
    {
        public string PropertyName { get; set; }
        public bool IsRequired { get; set; }
        public int ColIndex { get; set; }
    }
}
