using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class GenericControlCustomEditDetailViewModel : ViewModelBase
    {
        public ILog _logger;
        public MucLucNganSachService _service;

        public override Type ContentType => typeof(GenericControlCustomViewEditDetail);

        public object Model { get; set; }
        public ObservableCollection<CategoryEditDetail> CategoryDetails { get; set; }

        private double _columnWidth;
        public double ColumnWidth
        {
            get => _columnWidth;
            set => SetProperty(ref _columnWidth, value);
        }

        public GenericControlCustomEditDetailViewModel(object model)
        {
            Model = model;
            Init();
        }

        public override void Init()
        {
            base.Init();
            List<CategoryEditDetail> lst = new List<CategoryEditDetail>();
            PropertyInfo[] props = Model.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (Model.GetType().Name == "NsMuclucNgansachModel")
                {
                    if (Attribute.IsDefined(prop, typeof(DisplayDetailInfoAttribute)))
                    {
                        DisplayDetailInfoAttribute attribute = (DisplayDetailInfoAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayDetailInfoAttribute));
                        string info = attribute.Name;
                        object val = prop.GetValue(Model, null);
                        var isVisible = true;
                        var isReadOnly = true;
                        if (val == null || string.IsNullOrEmpty(val.ToString()) || prop.Name == "MoTa")
                        {
                            isReadOnly = false;
                        }
                        lst.Add(new CategoryEditDetail { Info = info, Value = val == null || string.IsNullOrEmpty(val.ToString()) ? " " : val.ToString(), IsVisible = isVisible, IsReadOnly = isReadOnly });
                    }
                    else if (prop.Name == "MlnsId")
                    {
                        string info = prop.Name;
                        object val = prop.GetValue(Model, null);
                        var isVisible = false;
                        var isReadOnly = true;                        
                        lst.Add(new CategoryEditDetail { Info = info, Value = val == null || string.IsNullOrEmpty(val.ToString()) ? " " : val.ToString(), IsVisible = isVisible, IsReadOnly = isReadOnly });
                    }    
                }
                else {
                    if (Attribute.IsDefined(prop, typeof(DisplayDetailInfoAttribute)))
                    {
                        DisplayDetailInfoAttribute attribute = (DisplayDetailInfoAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayDetailInfoAttribute));
                        string info = attribute.Name;
                        object val = prop.GetValue(Model, null);
                        var isVisible = true;
                        var isReadOnly = true;
                        lst.Add(new CategoryEditDetail { Info = info, Value = val == null || string.IsNullOrEmpty(val.ToString()) ? " " : val.ToString(), IsVisible = isVisible, IsReadOnly = isReadOnly });
                    }
                }                
            }
            CategoryDetails = new ObservableCollection<CategoryEditDetail>(lst);
            OnPropertyChanged(nameof(CategoryDetails));
        }

        public override void OnSave(object obj)
        {
            try
            {                
                var time = DateTime.Now;
                string msgConfirm = "Bạn chắc chắn muốn lưu thay đổi ?";
                MessageBoxResult dialogResult = MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        BeforeSave();
                        MessageBoxHelper.Info("Lưu dữ liệu thành công");
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, Resources.Alert, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void BeforeSave()
        {
            if (Model.GetType().Name == "NsMuclucNgansachModel")
            {
                var idOld = CategoryDetails.Where(r => r.Info == "MlnsId").First().Value;
                var mlnsOld = _service.FindById(idOld);
            }
            throw new NotImplementedException();
        }
    }

    public class CategoryEditDetail
    {
        public string Info { get; set; }
        public string Value { get; set; }
        public bool IsVisible { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
