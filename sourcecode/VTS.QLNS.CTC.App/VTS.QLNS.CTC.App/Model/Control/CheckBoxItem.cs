using System;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class CheckBoxItem : BindableBase
    {
        private Guid _id;
        private string _displayItem;
        private string _valueItem;
        private string _valueItem2;
        private bool _isChecked;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string DisplayItem
        {
            get => _displayItem;
            set => SetProperty(ref _displayItem, value);
        }
        public string ValueItem
        {
            get => _valueItem;
            set => SetProperty(ref _valueItem, value);
        }
        public string ValueItem2
        {
            get => _valueItem2;
            set => SetProperty(ref _valueItem2, value);
        }
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private string _nameItem;
        public string NameItem
        {
            get => _nameItem;
            set => SetProperty(ref _nameItem, value);
        }

        private bool _isNew;
        public bool IsNew
        {
            get => _isNew;
            set => SetProperty(ref _isNew, value);
        }

        private bool _isFilter;
        public bool IsFilter
        {
            get => _isFilter;
            set => SetProperty(ref _isFilter, value);
        }

        public bool IsHitTestVisible { get; set; } = true;
    }
}
