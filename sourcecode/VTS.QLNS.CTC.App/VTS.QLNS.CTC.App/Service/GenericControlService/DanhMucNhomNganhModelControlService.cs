using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class DanhMucNhomNganhModelControlService : GenericControlBaseService<DanhMucNhomNganhModel, Core.Domain.DanhMuc, DanhMucNhomNganhService>
    {
        public override void CustomValueProps(DanhMucNhomNganhModel newRow, DanhMucNhomNganhModel currentRow)
        {
            if (newRow.SGiaTri == null)
                newRow.SGiaTri = string.Empty;
            base.CustomValueProps(newRow, currentRow);
        }
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(DanhMucNhomNganhModel.ValueToString)))
            {
                var danhMucNganhService = sourceVM._serviceProvider.GetService(typeof(DanhMucNganhService));
                GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService> dialogVM = new GenericControlCustomViewModel<DanhMucNganhModel, DanhMuc, DanhMucNganhService>
                    ((DanhMucNganhService)danhMucNganhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục chuyên ngành";
                dialogVM.Title = "Danh mục chuyên ngành";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;
                //
                IEnumerable<string> idDanhMuc = sourceVM.SelectedItem.SGiaTri.Split(",").Select(p => p.Trim()).ToList();
                foreach (var model in dialogVM.Items)
                {
                    if (idDanhMuc.Contains(model.IIDMaDanhMuc))
                    {
                        model.IsSelected = true;
                    }
                }
                //
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<DanhMucNganhModel> source = obj as IEnumerable<DanhMucNganhModel>;
                    sourceVM.SelectedItem.Values = source;
                    sourceVM.SelectedItem.SGiaTri = string.Join(",", source.Select(s => s.IIDMaDanhMuc));
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }
    }
}
