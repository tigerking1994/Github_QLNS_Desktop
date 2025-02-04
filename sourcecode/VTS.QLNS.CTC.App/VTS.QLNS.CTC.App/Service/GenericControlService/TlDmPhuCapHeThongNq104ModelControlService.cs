using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class TlDmPhuCapHeThongNq104ModelControlService : GenericControlBaseService<TlDmPhuCapHeThongNq104Model, TlDmPhuCapNq104, TlDmPhuCapHeThongNq104Service>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(TlDmPhuCapHeThongNq104Model.Parent)))
            {
                var TlDmPhuCapHeThongService = sourceVM._serviceProvider.GetService(typeof(TlDmPhuCapHeThongNq104Service));
                GenericControlCustomViewModel<TlDmPhuCapHeThongNq104Model, TlDmPhuCapNq104, TlDmPhuCapHeThongNq104Service> dialogVM = new GenericControlCustomViewModel<TlDmPhuCapHeThongNq104Model, TlDmPhuCapNq104, TlDmPhuCapHeThongNq104Service>
                    ((TlDmPhuCapHeThongNq104Service)TlDmPhuCapHeThongService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục phụ cấp";
                dialogVM.Title = "Danh mục phụ cấp";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = false;
                dialogVM.SelectedItem = dialogVM.Items.Where(i => i.MaPhuCap.Equals(sourceVM.SelectedItem.Parent)).FirstOrDefault();
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    TlDmPhuCapHeThongNq104Model parent = obj as TlDmPhuCapHeThongNq104Model;
                    sourceVM.SelectedItem.Parent = parent.MaPhuCap;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
