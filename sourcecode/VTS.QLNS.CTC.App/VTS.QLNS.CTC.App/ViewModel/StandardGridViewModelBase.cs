using AutoMapper;
using log4net;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class StandardGridViewModelBase<T> : StandardViewModelBase
    {
        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                if (SetProperty(ref _items, value))
                {
                    OnItemsChanged();
                }
            }
        }

        private T _selectedItem;
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    OnSelectedItemChanged();
                }
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DieuChinhCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DeleteAllCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand LockUnLockCommand { get; set; }
        public RelayCommand SelectionDoubleClickCommand { get; }

        public StandardGridViewModelBase(ISessionService sessionService,
            IMapper mapper, ILog logger, INsDonViService nsDonViService, IDanhMucService danhMucService) : base(sessionService, mapper, logger, nsDonViService, danhMucService)
        {
            Items = new ObservableCollection<T>();
            AddCommand = new RelayCommand(obj => OnAdd(obj));
            UpdateCommand = new RelayCommand(obj => OnUpdate());
            DieuChinhCommand = new RelayCommand(obj => OnDieuChinh());
            DeleteCommand = new RelayCommand(obj => OnDelete(obj), CanDelete);
            RefreshCommand = new RelayCommand(o => OnRefresh(o));
            LockUnLockCommand = new RelayCommand(obj => OnLockUnLock());
            SelectionDoubleClickCommand = new RelayCommand(obj => OnSelectionDoubleClick(obj));
            DeleteAllCommand = new RelayCommand(obj => OnDeleteAll());
        }

        protected virtual bool CanDelete(object obj)
        {
            return true;
        }

        protected virtual void OnSelectionDoubleClick(object obj)
        {
        }

        protected virtual void OnLockUnLock()
        {
        }

        protected virtual void OnRefresh()
        {
        }

        protected virtual void OnRefresh(object obj)
        {
            OnRefresh();
        }

        protected virtual void OnDelete()
        {
        }

        protected virtual void OnDelete(Object obj)
        {
            DataGrid dataGrid = obj as DataGrid;
            if (dataGrid != null)
            {
                dataGrid.CancelEdit();
            }
            OnDelete();
        }

        protected virtual void OnUpdate()
        {
        }
        protected virtual void OnDieuChinh()
        {
        }

        protected virtual void OnAdd()
        {
        }

        protected virtual void OnAdd(object obj)
        {
            OnAdd();
        }

        protected virtual void OnItemsChanged()
        {

        }

        protected virtual void OnSelectedItemChanged()
        {
        }

        protected virtual void OnDeleteAll()
        {

        }
    }
}
