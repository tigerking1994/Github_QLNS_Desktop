using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class ComboboxItem : BindableBase
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _displayItem;
        public string DisplayItem
        {
            get => _displayItem;
            set => SetProperty(ref _displayItem, value);
        }

        private string _displayItemOption2;
        public string DisplayItemOption2
        {
            get => _displayItemOption2;
            set => SetProperty(ref _displayItemOption2, value);
        }

        private string _valueItem;
        public string ValueItem
        {
            get => _valueItem;
            set => SetProperty(ref _valueItem, value);
        }

        private string _type;
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string _hiddenValueOption3;
        public string HiddenValueOption3
        {
            get => _hiddenValueOption3;
            set => SetProperty(ref _hiddenValueOption3, value);
        }

        public string _hiddenValueOption2;
        public string HiddenValueOption2
        {
            get => _hiddenValueOption2;
            set => SetProperty(ref _hiddenValueOption2, value);
        }

        public string _hiddenValue;
        public string HiddenValue
        {
            get => _hiddenValue;
            set => SetProperty(ref _hiddenValue, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public ComboboxItem(){}

        public ComboboxItem(string displayItem, string valueItem)
        {
            _displayItem = displayItem;
            _valueItem = valueItem;
        }

        public ComboboxItem(string displayItem, string valueItem, string displayItemOption2)
        {
            _displayItem = displayItem;
            _valueItem = valueItem;
            _displayItemOption2 = displayItemOption2;
        }
    }
}