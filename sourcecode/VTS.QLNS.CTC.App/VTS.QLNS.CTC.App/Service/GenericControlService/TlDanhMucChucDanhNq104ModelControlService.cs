using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.ViewModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class TlDanhMucChucDanhNq104ModelControlService : GenericControlBaseService<TlDanhMucChucDanhNq104Model, TlDmChucVuNq104, TLDanhMucChucVuNq104Service>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (property.Name.Equals(nameof(TlDanhMucChucDanhNq104Model.TienNangLuong)) || property.Name.Equals(nameof(TlDanhMucChucDanhNq104Model.TienLuong)))
            {
                var DanhMucNganhService = sourceVM._serviceProvider.GetService(typeof(TLDanhMucChucVuNq104Service));
                GenericControlCustomViewModel<TlDanhMucChucDanhNq104Model, TlDmChucVuNq104, TLDanhMucChucVuNq104Service> dialogVM = new GenericControlCustomViewModel<TlDanhMucChucDanhNq104Model, TlDmChucVuNq104, TLDanhMucChucVuNq104Service>
                    ((TLDanhMucChucVuNq104Service)DanhMucNganhService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);

                dialogVM.IsDialog = true;
                dialogVM.Description = "Danh mục chức danh";
                dialogVM.Title = "Danh mục chức danh";
                dialogVM.IsLoaiChucVu = true;
                dialogVM.IsPopup = true;
                dialogVM.IsShowTyLeTangLuong = true;
                GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
                dialogVM.IsMultipleSelect = true;

                GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                {
                    DataContext = genericControlCustomWindowViewModel
                };
                GenericControlCustomWindow.SavedAction = obj =>
                {
                    IEnumerable<TlDanhMucChucVuNq104Model> lstData = obj as IEnumerable<TlDanhMucChucVuNq104Model>;
                    var GetDataConText = GenericControlCustomWindow.DataContext;
                    var ViewModel = GetDataConText as GenericControlCustomWindowViewModel;
                    int ITyLeTang = 0;
                    if (ViewModel != null)
                    {
                        var currenPage = ViewModel.CurrentPage;
                        ITyLeTang = (int)currenPage.GetPropertyValue("ITyLeTang");
                    }
                    foreach (var item in lstData)
                    {
                        var data = sourceVM.Items.Where(x => x.Ma == item.Ma).FirstOrDefault();
                        if (data != null)
                        {
                            data.TienLuong = item.TienLuong + ((item.TienLuong * ITyLeTang) / 100);
                            data.TienNangLuong = item.TienNangLuong + ((item.TienNangLuong * ITyLeTang) / 100);
                            data.IsModified = true;
                            data.IsAdded = true;
                        }

                    }
                    GenericControlCustomWindow.Close();
                };

                dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
                GenericControlCustomWindow.Show();
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter == null)
                return;
        }
    }
}
