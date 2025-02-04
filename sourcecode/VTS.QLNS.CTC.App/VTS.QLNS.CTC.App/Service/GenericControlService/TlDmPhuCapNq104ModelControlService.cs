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
using VTS.QLNS.CTC.App.Model.Control;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Utility;
using System.ComponentModel;

namespace VTS.QLNS.CTC.App.Service.GenericControlService
{
    public class TlDmPhuCapNq104ModelControlService : GenericControlBaseService<TlDmPhuCapNq104Model, Core.Domain.TlDmPhuCapNq104, TlDmPhuCapNq104Service>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (!property.Name.Equals(nameof(TlDmPhuCapNq104Model.Parent)))
                return;
            
            var TlDmPhuCapService = sourceVM._serviceProvider.GetService(typeof(ITlDmPhuCapNq104Service));
            GenericControlCustomViewModel<TlDmPhuCapNq104Model, TlDmPhuCapNq104, TlDmPhuCapNq104Service> dialogVM = new GenericControlCustomViewModel<TlDmPhuCapNq104Model, TlDmPhuCapNq104, TlDmPhuCapNq104Service>
                ((TlDmPhuCapNq104Service)TlDmPhuCapService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
            dialogVM.IsDialog = true;
            dialogVM.Description = "Danh mục phụ cấp";
            dialogVM.Title = "Danh mục phụ cấp";
            GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(dialogVM);
            dialogVM.IsMultipleSelect = false;
            GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
            {
                DataContext = genericControlCustomWindowViewModel
            };
            switch (property.Name)
            {
                case nameof(TlDmPhuCapNq104Model.Parent):
                    dialogVM.SelectedItem = dialogVM.Items.Where(i => i.MaPhuCap.Equals(sourceVM.SelectedItem.Parent)).FirstOrDefault();
                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        TlDmPhuCapNq104Model parent = obj as TlDmPhuCapNq104Model;
                        sourceVM.SelectedItem.Parent = parent.MaPhuCap;
                        GenericControlCustomWindow.Close();
                    };
                    break;
            }
            dialogVM.GenericControlCustomWindow = GenericControlCustomWindow;
            GenericControlCustomWindow.Show();
        }

        public override ObservableCollection<ComboboxItem> LoadComboboxData(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Name.Equals(nameof(TlDmPhuCapNq104Model.ILoai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Thường xuyên", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Nghiệp vụ", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Thu nhập khác", ValueItem = "3"},
                    new ComboboxItem {DisplayItem = "Giảm trừ khác", ValueItem = "4"}
                };
            }
            if (propertyInfo.Name.Equals(nameof(TlDmPhuCapNq104Model.IDinhDang)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Hệ số", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Tiền", ValueItem = "0"},
                    new ComboboxItem {DisplayItem = "Khác", ValueItem = "2"}
                };
            }
            return new ObservableCollection<ComboboxItem>();
        }

        public override bool validate()
        {
            var dataToSave = sourceVM.Items.Where(i => i.IsModified || i.IsDeleted);
            List<string> lstMessError = new List<string>();
            foreach (var item in dataToSave)
            {
                var checkExist = sourceVM._service.CheckPhuCapExist(item.MaPhuCap, item.Id);
                if (checkExist && !item.IsDeleted)
                {
                    lstMessError.Add(string.Format("Mã phụ cấp {0} đã tồn tại.", item.MaPhuCap));
                }
            }
            if (lstMessError.Count != 0)
            {
                MessageBoxHelper.Error(string.Join("\n", lstMessError));
                return false;
            }
            return true;
        }

        public override void OrderData()
        {
            List<TlDmPhuCapNq104Model> lstData = sourceVM.Items.ToList();
            if (lstData == null || lstData.Count == 0) return;
            List<TlDmPhuCapNq104Model> results = new List<TlDmPhuCapNq104Model>();
            foreach(var item in lstData.Where(n=> string.IsNullOrEmpty(n.Parent)).OrderBy(n => n.MaPhuCap))
            {
                results.AddRange(Recusive(item, lstData));
            }
            sourceVM.Items = new ObservableCollection<TlDmPhuCapNq104Model>(results);
        }

        public List<TlDmPhuCapNq104Model> Recusive(TlDmPhuCapNq104Model item, List<TlDmPhuCapNq104Model> lstItem)
        {
            List<TlDmPhuCapNq104Model> lstData = new List<TlDmPhuCapNq104Model>();
            lstData.Add(item);
            if(lstItem.Any(n=> n.Parent == item.MaPhuCap))
            {
                foreach(var child in lstItem.Where(n => n.Parent == item.MaPhuCap).OrderBy(n => n.MaPhuCap))
                {
                    lstData.AddRange(Recusive(child, lstItem));
                }
            }
            return lstData;
        }
    }
}
