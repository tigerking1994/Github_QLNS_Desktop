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
    public class TlDmPhuCapModelControlService : GenericControlBaseService<TlDmPhuCapModel, Core.Domain.TlDmPhuCap, TlDmPhuCapService>
    {
        public override void InitDialog(PropertyInfo property)
        {
            if (!property.Name.Equals(nameof(TlDmPhuCapModel.Parent)))
                return;
            
            var TlDmPhuCapService = sourceVM._serviceProvider.GetService(typeof(ITlDmPhuCapService));
            GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> dialogVM = new GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService>
                ((TlDmPhuCapService)TlDmPhuCapService, sourceVM._mapper, sourceVM._sessionService, sourceVM._serviceProvider);
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
                case nameof(TlDmPhuCapModel.Parent):
                    dialogVM.SelectedItem = dialogVM.Items.Where(i => i.MaPhuCap.Equals(sourceVM.SelectedItem.Parent)).FirstOrDefault();
                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        TlDmPhuCapModel parent = obj as TlDmPhuCapModel;
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
            if (propertyInfo.Name.Equals(nameof(TlDmPhuCapModel.ILoai)))
            {
                return new ObservableCollection<ComboboxItem>
                {
                    new ComboboxItem {DisplayItem = "Thường xuyên", ValueItem = "1"},
                    new ComboboxItem {DisplayItem = "Nghiệp vụ", ValueItem = "2"},
                    new ComboboxItem {DisplayItem = "Thu nhập khác", ValueItem = "3"},
                    new ComboboxItem {DisplayItem = "Giảm trừ khác", ValueItem = "4"}
                };
            }
            if (propertyInfo.Name.Equals(nameof(TlDmPhuCapModel.IDinhDang)))
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
            List<TlDmPhuCapModel> lstData = sourceVM.Items.ToList();
            if (lstData == null || lstData.Count == 0) return;
            List<TlDmPhuCapModel> results = new List<TlDmPhuCapModel>();
            foreach(var item in lstData.Where(n=> string.IsNullOrEmpty(n.Parent)).OrderBy(n => n.MaPhuCap))
            {
                results.AddRange(Recusive(item, lstData));
            }
            sourceVM.Items = new ObservableCollection<TlDmPhuCapModel>(results);
        }

        public List<TlDmPhuCapModel> Recusive(TlDmPhuCapModel item, List<TlDmPhuCapModel> lstItem)
        {
            List<TlDmPhuCapModel> lstData = new List<TlDmPhuCapModel>();
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
