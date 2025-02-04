using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using VTS.QLNS.CTC.App.Component;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.View.Shared;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class GenericControlCustomDetailViewModel : ViewModelBase
    {
        public override Type ContentType => typeof(GenericControlCustomViewDetail);

        public object Model { get; set; }
        public ObservableCollection<CategoryDetail> CategoryDetails { get; set; }

        private double _columnWidth;
        public double ColumnWidth
        {
            get => _columnWidth;
            set => SetProperty(ref _columnWidth, value);
        }

        public GenericControlCustomDetailViewModel(object model)
        {
            Model = model;
            Init();
        }

        public override void Init()
        {
            base.Init();
            List<CategoryDetail> lst = new List<CategoryDetail>();
            PropertyInfo[] props = Model.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(DisplayDetailInfoAttribute)))
                {
                    DisplayDetailInfoAttribute attribute = (DisplayDetailInfoAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayDetailInfoAttribute));
                    string info = attribute.Name;
                    object val = prop.GetValue(Model, null);
                    lst.Add(new CategoryDetail { Info = info, Value = val == null || string.IsNullOrEmpty(val.ToString()) ? " " : val.ToString() });
                }
            }
            CategoryDetails = new ObservableCollection<CategoryDetail>(lst);
            OnPropertyChanged(nameof(CategoryDetails));
        }
    }

    public class CategoryDetail
    {
        public string Info { get; set; }
        public string Value { get; set; }
    }
}
