using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.ImportViewModel
{
    public class ExecuteImportDataViewModel<TModel, TEntity, TService, TImportModel> : ViewModelBase
        where TImportModel : BaseImportModel
        where TModel : ModelBase, new()
        where TEntity : EntityBase, new()
        where TService : IService<TEntity>
    {
        private TService _service;
        public AuthenticationInfo AuthenticationInfo { get; set; }

        public override string Name => "4. Thực hiện";
        public override Type ContentType => typeof(View.Shared.Import.ExecuteImportData);

        public ImportSharingData ImportSharingData { get;set; }

        public ImportExcelViewModel<TModel, TEntity, TService, TImportModel> ImportExcelViewModel { get; set; }

        public RelayCommand UpdateCommand { get; }

        public ExecuteImportDataViewModel(TService service)
        {
            _service = service;
            UpdateCommand = new RelayCommand(obj => OnUpdate());
        }

        private void OnUpdate()
        {
            try
            {
                IEnumerable<TEntity> entities = ImportSharingData.Entities.Cast<TEntity>();
                if (ImportSharingData.ImportMode == 0)
                {
                    foreach (var i in entities)
                    {
                        i.Id = Guid.Empty;
                    }
                }
                _service.ImportDataExcel(entities, AuthenticationInfo, ImportSharingData.ImportMode);
                MessageBox.Show("Đẩy dữ liệu thành công", Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImportExcelViewModel.OnNext();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
