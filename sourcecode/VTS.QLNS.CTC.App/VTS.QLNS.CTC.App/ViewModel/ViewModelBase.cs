using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public abstract class ViewModelBase : BindableBase
    {
        public Action<object> SavedAction;
        public virtual string FuncCode { get; set; }
        public virtual Type ContentType { get; }
        public virtual PackIconKind IconKind { get; set; } = PackIconKind.RhombusOutline;
        public virtual string GroupName { get; set; }
        public virtual bool IsUseExpand { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsAuthorized => CheckPermission();

        //private object _content;
        public object Content => CreateContent();

        private ViewModelBase _parentPage;
        public ViewModelBase ParentPage
        {
            get => _parentPage;
            set => SetProperty(ref _parentPage, value);
        }

        private ViewModelBase _currentPage;
        public ViewModelBase CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != null)
                {
                    // Dispose current page
                    _currentPage.Dispose();
                }
                SetProperty(ref _currentPage, value);
                if (_currentPage != null)
                {
                    // Init new page
                    _currentPage.Init();
                }
            }
        }

        private DateTime? _reportDate = DateTime.Now;
        public DateTime? ReportDate
        {
            get => _reportDate;
            set => SetProperty(ref _reportDate, value);
        }

        public string ReportDateTitle { get; } = "Ngày báo cáo";

        private ObservableCollection<ViewModelBase> _documentation;
        public ObservableCollection<ViewModelBase> Documentation
        {
            get => _documentation;
            set => SetProperty(ref _documentation, value);
        }

        private ViewModelBase _documentationSelectedItem;
        public ViewModelBase DocumentationSelectedItem
        {
            get { return _documentationSelectedItem; }
            set
            {
                SetProperty(ref _documentationSelectedItem, value);
                CurrentPage = _documentationSelectedItem;
            }
        }

        private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement;
        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
        {
            get => _horizontalScrollBarVisibilityRequirement;
            set => SetProperty(ref _horizontalScrollBarVisibilityRequirement, value);
        }

        private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Disabled;
        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
        {
            get => _verticalScrollBarVisibilityRequirement;
            set => SetProperty(ref _verticalScrollBarVisibilityRequirement, value);
        }

        private Thickness _marginRequirement = new Thickness(10);
        public virtual Thickness MarginRequirement
        {
            get => _marginRequirement;
            set => SetProperty(ref _marginRequirement, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand ClosingCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand CancelCommand { get; }

        public ViewModelBase()
        {
            SaveCommand = new RelayCommand(obj => OnSave(obj), canExecute());
            CancelCommand = new RelayCommand(obj => OnCancel());
            CloseCommand = new RelayCommand(obj => OnClose(obj));
            ClosingCommand = new RelayCommand(obj => OnClosing());
            IsUseExpand = false;
        }

        public virtual void OnClosing()
        {
        }

        public virtual void OnClose(object obj)
        {
            Dispose();
        }

        public virtual void OnSave()
        {
        }

        public virtual void OnSave(object obj)
        {
            OnSave();
        }

        public virtual void OnCancel()
        {
        }

        public virtual void LoadData(params object[] args)
        {
        }

        public virtual void Init()
        {
            InitReportDefaultDate();
        }


        public void InitReportDefaultDate()
        {
            ReportDate = DateTime.Now;
        }

        public virtual bool CheckPermission()
        {
            return Permission.CheckPermission(FuncCode);
        }

        public virtual void Dispose()
        {
            // Dispose all child page
            if (Documentation != null && Documentation.Count > 0)
            {
                foreach (ViewModelBase item in Documentation)
                {
                    item.Dispose();
                }
            }
            _currentPage = null;
            GC.SuppressFinalize(this);
        }

        private object CreateContent()
        {
            if (ContentType != null)
            {
                object content = Activator.CreateInstance(ContentType);
                if (content is FrameworkElement element)
                {
                    element.DataContext = this;
                }
                return content;
            }
            return null;
        }

        public virtual Func<object, bool> canExecute()
        {
            return x => true;
        }

        public void AddEmptyItems<T>(List<T> listData) where T : new()
        {
            if (listData.Count < DefaultConst.BHXH_10_Rows)
            {
                int rowRemain = DefaultConst.BHXH_10_Rows - listData.Count;
                for (int i = 0; i < rowRemain; i++)
                {
                    listData.Add(new T());
                }
            }
        }
    }
}
