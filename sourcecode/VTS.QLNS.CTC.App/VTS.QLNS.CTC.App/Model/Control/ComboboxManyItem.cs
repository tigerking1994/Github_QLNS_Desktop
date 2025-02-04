using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class ComboboxManyItem : BindableBase
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

        private string _displayItem1;
        public string DisplayItem1
        {
            get => _displayItem1;
            set => SetProperty(ref _displayItem1, value);
        }

        private string _displayItem2;
        public string DisplayItem2
        {
            get => _displayItem2;
            set => SetProperty(ref _displayItem2, value);
        }

        private string _displayItem3;
        public string DisplayItem3
        {
            get => _displayItem3;
            set => SetProperty(ref _displayItem3, value);
        }

        private string _displayItem4;
        public string DisplayItem4
        {
            get => _displayItem4;
            set => SetProperty(ref _displayItem4, value);
        }

        private string _valueItem;
        public string ValueItem
        {
            get => _valueItem;
            set => SetProperty(ref _valueItem, value);
        }

        private int? _indexItem;
        public int? IndexItem
        {
            get => _indexItem;
            set => SetProperty(ref _indexItem, value);
        }

        private string _type;
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public ComboboxManyItem() { }

        public ComboboxManyItem(string displayItem, string valueItem)
        {
            _displayItem = displayItem;
            _valueItem = valueItem;
        }
    }
}
