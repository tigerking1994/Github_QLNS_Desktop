using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.App.View.Shared;
using System.Reflection;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class HTQuyenModelControlService : GenericControlBaseService<HTQuyenModel, Core.Domain.HtQuyen, SysAuthorityService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(HTQuyenModel.SysFunctionName)))
            {
                var SysFunctionService = sourceVM._serviceProvider.GetService(typeof(ISysFunctionService));
                GenericControlCustomViewModel<HTChucNangModel, HtChucNang, SysFunctionService> dialogVM = new GenericControlCustomViewModel<HTChucNangModel, HtChucNang, SysFunctionService>
                    ((SysFunctionService)SysFunctionService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh sách chức năng";
                dialogVM.Title = "Danh sách chức năng";
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM._isFirstLoad = true;
                dialogVM.IsMultipleSelect = true;
                SetSelectedChucNang(dialogVM);
                dialogVM._isFirstLoad = false;
                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<HTChucNangModel> data = obj as IEnumerable<HTChucNangModel>;
                    sourceVM.SelectedItem.SysFunctionModels = data.ToList();
                    sourceVM.SelectedItem.SysFunctionName = string.Join(", ", data.Select(s => s.STenChucNang));
                    GenericControlCustomWindow.Close();
                };
                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private void SetSelectedChucNang(GenericControlCustomViewModel<HTChucNangModel, HtChucNang, SysFunctionService> dialogVM)
        {
            IEnumerable<string> funcCode = sourceVM.SelectedItem.SysFunctionModels.Select(t => t.IIDMaChucNang);
            foreach (var model in dialogVM.Items)
            {
                if (funcCode.Contains(model.IIDMaChucNang))
                {
                    model.IsSelected = true;
                }
            }
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo property)
        {
            if(property.Name.Equals(nameof(HTQuyenModel.AuthorityTypeId)))
            {
                List<HTLoaiQuyenModel> authorTypes = sourceVM._mapper.Map<List<HTLoaiQuyenModel>>(sourceVM._service.FindAllAuthorTypes());
                return new ObservableCollection<ComboboxItem>(authorTypes.Select(t => new ComboboxItem { DisplayItem = t.STenLoaiQuyen, ValueItem = t.ID.ToString() }));
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override void CustomViewDetailModelInfo(GenericControlCustomDetailViewModel genericControlCustomDetailViewModel)
        {
            genericControlCustomDetailViewModel.ColumnWidth = 780;
        }
    }
}
