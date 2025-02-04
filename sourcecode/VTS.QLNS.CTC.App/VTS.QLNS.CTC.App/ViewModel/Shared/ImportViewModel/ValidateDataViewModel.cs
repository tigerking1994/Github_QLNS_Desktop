using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class ValidateDataViewModel<TModel, TEntity, TService, TImportModel> : ViewModelBase 
        where TImportModel : BaseImportModel
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
    {
        private IImportExcelService _importExcelService;
        private TService _service;
        private IServiceProvider _provider;
        private IMapper _mapper;

        public override string Name => "3. Kiểm tra dữ liệu";
        public override Type ContentType => typeof(View.Shared.Import.ValidateData);

        public ImportSharingData ImportSharingData { get; set; }

        public ObservableCollection<TImportModel> ImportResults { get; set; }

        public RelayCommand AutoGeneratingColumnsCommand { get; }
        public RelayCommand ValidateCommand { get; }

        public AuthenticationInfo _authenticationInfo;

        public ValidateDataViewModel(IServiceProvider serviceProvider, TService service)
        {
            _provider = serviceProvider;
            _service = service;
            _mapper = (IMapper)_provider.GetService(typeof(IMapper));
            _importExcelService = (IImportExcelService)_provider.GetService(typeof(IImportExcelService));
            AutoGeneratingColumnsCommand = new RelayCommand(obj => OnAutoGeneratingColumnsCommand(obj));
            ValidateCommand = new RelayCommand(obj => OnValidateData());
        }

        public int TotalRecord
        {
            get => ImportResults == null ? 0 : ImportResults.Count;
        }

        public int TotalValidRecord
        {
            get => ImportResults == null ? 0 : ImportResults.Where(i => i.ImportStatus).Count();
        }

        public int TotalRemovedRecord
        {
            get => ImportResults == null ? 0 : ImportResults.Where(i => !i.IsImported).Count();
        }

        public string ResultDescription
        {
            get => "";
        }

        public override void Init()
        {
            base.Init();
            ImportResult<TImportModel> importResult = _importExcelService.ProcessData<TImportModel>(
                ImportSharingData.FileName, ImportSharingData.SelectedSheet, ImportSharingData.TitleRow.HasValue ? ImportSharingData.TitleRow.Value : 0
                , ImportSharingData.ImportFields.ToList());
            ImportResults = new ObservableCollection<TImportModel>(importResult.Data);
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

        public bool IsValidToImport()
        {
            ImportResults = new ObservableCollection<TImportModel>(_importExcelService.Validate<TImportModel>(ImportResults, ImportSharingData.ImportFields.ToList()));
            OnPropertyChanged(nameof(ImportResults));
            // tìm bản ghi lỗi và được chọn để import
            bool isInvalid = ImportResults.Any(t => t.IsImported && !t.ImportStatus);
            if (isInvalid)
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng xem chi tiết lỗi", Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            IEnumerable<TEntity> entities = _mapper.Map<IEnumerable<TEntity>>(ImportResults.Where(t => t.IsImported).ToList());
            if (ImportSharingData.ImportMode == 0)
            {
                entities = _service.FindAllNew(entities, _authenticationInfo);
                foreach (var i in entities)
                {
                    i.Id = Guid.Empty;
                }
            }
            _service.ValidateDataExcel(entities, _authenticationInfo, ImportSharingData.ImportMode);            
            MessageBox.Show("Dữ liệu không có lỗi!", Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ImportResults = _mapper.Map<ObservableCollection<TImportModel>>(entities.ToList());
            ImportResults.ForAll(r => r.ImportStatus = true);
            ImportResults.ForAll(r => r.IsImported = true);
            return !isInvalid;
        }

        public void SetDataToImport()
        {
            ImportSharingData.Entities = _mapper.Map<IEnumerable<TEntity>>(ImportResults.Where(t => t.IsImported).ToList());
            ImportSharingData.ImportResults = ImportResults.Where(t => t.IsImported).Cast<object>();
            ImportSharingData.TotalRecord = ImportResults.Count();
            ImportSharingData.TotalErrRecord = ImportResults.Where(t => !t.ImportStatus).Count();
            ImportSharingData.TotalUpdatedRecord = ImportResults.Where(t => t.IsImported).Count();
            ImportSharingData.TotalValidRecord = ImportResults.Where(t => t.ImportStatus).Count();
            ImportSharingData.TotalRemoveRecord = ImportResults.Where(t => !t.IsImported).Count();
        }

        public void OnValidateData()
        {
            try
            {
                IsValidToImport();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
