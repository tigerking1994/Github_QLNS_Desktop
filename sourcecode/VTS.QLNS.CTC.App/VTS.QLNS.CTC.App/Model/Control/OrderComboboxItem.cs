using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class OrderComboboxItem : BindableBase
    {
        private string _displayItem;
        public string DisplayItem
        {
            get => _displayItem;
            set => SetProperty(ref _displayItem, value);
        }

        private string _valueItem;
        public string ValueItem
        {
            get => _valueItem;
            set => SetProperty(ref _valueItem, value);
        }

        private int _columnOrderIndex;
        public int ColumnOrderIndex
        {
            get => _columnOrderIndex;
            set => SetProperty(ref _columnOrderIndex, value);
        }

        private ListSortDirection _sortDirection;
        public ListSortDirection SortDirection
        {
            get => _sortDirection;
            set => SetProperty(ref _sortDirection, value);
        }

        private string _sortMemberPath;
        public string SortMemberPath
        {
            get => _sortMemberPath;
            set => SetProperty(ref _sortMemberPath, value);
        }
    }
}
