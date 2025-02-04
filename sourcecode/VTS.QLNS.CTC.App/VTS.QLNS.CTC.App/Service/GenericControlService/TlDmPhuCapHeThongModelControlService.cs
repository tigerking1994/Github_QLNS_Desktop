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
    public class TlDmPhuCapHeThongModelControlService : GenericControlBaseService<TlDmPhuCapHeThongModel, TlDmPhuCap, TlDmPhuCapHeThongService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(TlDmPhuCapHeThongModel.Parent)))
            {
                var TlDmPhuCapHeThongService = sourceVM._serviceProvider.GetService(typeof(TlDmPhuCapHeThongService));
                GenericControlCustomViewModel<TlDmPhuCapHeThongModel, TlDmPhuCap, TlDmPhuCapHeThongService> dialogVM = new GenericControlCustomViewModel<TlDmPhuCapHeThongModel, TlDmPhuCap, TlDmPhuCapHeThongService>
                    ((TlDmPhuCapHeThongService)TlDmPhuCapHeThongService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
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
                    TlDmPhuCapHeThongModel parent = obj as TlDmPhuCapHeThongModel;
                    sourceVM.SelectedItem.Parent = parent.MaPhuCap;
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
