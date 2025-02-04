using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Control;
using Border = System.Windows.Controls.Border;
using Style = System.Windows.Style;

namespace VTS.QLNS.CTC.App.Component
{
    [TemplatePart(Name = "PART_MultiHeader", Type = typeof(DataGrid))]
    public class ExpandedDataGrid : DataGrid
    {
        protected bool _inWidthChange = false;
        protected bool _updatingColumnInfo = false;
        private DataGrid _footerDataGrid;
        private DataGrid _headerDataGrid;
        private bool _columnWidthChanging;
        private int _columnDisplayIndex;
        private const string ScrollViewerNameInTemplate = "DG_ScrollViewer";
        private static PropertyDescriptor pd = DependencyPropertyDescriptor.FromProperty(DataGridColumn.WidthProperty, typeof(DataGridColumn));

        public static readonly DependencyProperty ColumnInfoProperty = DependencyProperty.Register("ColumnInfo",
            typeof(ObservableCollection<ColumnInfo>), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty ElementFrozenColumnProperty = DependencyProperty.Register("ElementFrozenColumn",
            typeof(FrameworkElement), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty ElementDynamicColumnProperty = DependencyProperty.Register("ElementDynamicColumn",
            typeof(FrameworkElement), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty IsSaveDataGridInfoProperty = DependencyProperty.Register("IsSaveDataGridInfo",
            typeof(bool), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty ElementFooterDataGridProperty = DependencyProperty.Register("ElementFooterDataGrid",
            typeof(FrameworkElement), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty ElementHeaderDataGridProperty = DependencyProperty.Register("ElementHeaderDataGrid",
            typeof(FrameworkElement), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColumnInfoChangedCallback)
        );

        public static readonly DependencyProperty EnableFooterDataGridProperty = DependencyProperty.Register("EnableFooterDataGrid",
            typeof(bool), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public static readonly DependencyProperty EnableHeaderDataGridProperty = DependencyProperty.Register("EnableHeaderDataGrid",
            typeof(bool), typeof(ExpandedDataGrid),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public ObservableCollection<ColumnInfo> ColumnInfo
        {
            get { return (ObservableCollection<ColumnInfo>)GetValue(ColumnInfoProperty); }
            set { SetValue(ColumnInfoProperty, value); }
        }

        public FrameworkElement ElementFrozenColumn
        {
            get { return (FrameworkElement)GetValue(ElementFrozenColumnProperty); }
            set { SetValue(ElementFrozenColumnProperty, value); }
        }

        public FrameworkElement ElementDynamicColumn
        {
            get { return (FrameworkElement)GetValue(ElementDynamicColumnProperty); }
            set { SetValue(ElementDynamicColumnProperty, value); }
        }

        public bool EnableFooterDataGrid
        {
            get { return (bool)GetValue(EnableFooterDataGridProperty); }
            set { SetValue(EnableFooterDataGridProperty, value); }
        }

        public bool EnableHeaderDataGrid
        {
            get { return (bool)GetValue(EnableHeaderDataGridProperty); }
            set { SetValue(EnableHeaderDataGridProperty, value); }
        }

        public bool IsSaveDataGridInfo
        {
            get { return (bool)GetValue(IsSaveDataGridInfoProperty); }
            set { SetValue(IsSaveDataGridInfoProperty, value); }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            EventHandler sortDirectionChangedHandler = (sender, x) => UpdateColumnInfo();
            EventHandler widthPropertyChangedHandler = (sender, x) => _inWidthChange = true;
            var sortDirectionPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.SortDirectionProperty, typeof(DataGridColumn));
            var widthPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.WidthProperty, typeof(DataGridColumn));
            Loaded += (sender, x) =>
            {
                if (this.IsVisible)
                {
                    foreach (var column in Columns)
                    {
                        sortDirectionPropertyDescriptor.AddValueChanged(column, sortDirectionChangedHandler);
                        widthPropertyDescriptor.AddValueChanged(column, widthPropertyChangedHandler);
                    }
                    if (Application.Current.Properties.Contains(this.Name))
                    {
                        DataGridInformation dataGridInfo = (DataGridInformation)Application.Current.Properties[this.Name];
                        List<ColumnInfo> listColumnInfo = new List<ColumnInfo>();
                        foreach (var column in Columns)
                        {
                            if (column.Visibility == Visibility.Visible && dataGridInfo.ColumnInfo.Any(x => x.PropertyPath == column.SortMemberPath))
                                listColumnInfo.Add(dataGridInfo.ColumnInfo.First(x => x.PropertyPath == column.SortMemberPath));
                        }
                        ColumnInfo = new ObservableCollection<ColumnInfo>(listColumnInfo);
                        this.FrozenColumnCount = dataGridInfo.FrozenColumnCount;
                    }
                    this.LoadElementFrozenColumnData();
                    this.LoadElementDynamicColumnData();
                    this.LoadHeaderDataGrid();
                    this.LoadFooterDataGrid();
                    this.PreviewKeyDown += ExpandedDataGrid_PreviewKeyDown;
                    this.CurrentCellChanged += DataGrid_CurrentCellsChanged;
                    this.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(MainDataGrid_ScrollChanged));
                }
            };
            Unloaded += (sender, x) =>
            {
                foreach (var column in Columns)
                {
                    sortDirectionPropertyDescriptor.RemoveValueChanged(column, sortDirectionChangedHandler);    
                    widthPropertyDescriptor.RemoveValueChanged(column, widthPropertyChangedHandler);
                }
                this.CurrentCellChanged -= DataGrid_CurrentCellsChanged;
                this.RemoveHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(MainDataGrid_ScrollChanged));
            };
        }

        private void ExpandedDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is CategoryExpandedDataGrid) return;
            var uiElement = e.OriginalSource as UIElement;
            int columnIndex = (int)(((ExpandedDataGrid)sender).CurrentColumn?.DisplayIndex);
            DependencyObject nextUIElement = null;

            switch (e.Key)
            {
                case Key.Right:
                    e.Handled = true;
                    nextUIElement = uiElement.PredictFocus(FocusNavigationDirection.Right);
                    if (nextUIElement != null)
                    {
                        if (nextUIElement.GetType().Equals(typeof(DataGridCell)))
                        {
                            DataGridCellInfo nextCellInfo = new DataGridCellInfo((DataGridCell)nextUIElement);
                            this.SelectedItem = nextCellInfo.Item;
                            this.CurrentCell = nextCellInfo;
                        }
                    }
                    break;
                case Key.Left:
                    if (KeyLeftCheckHandel(uiElement))
                    {
                        e.Handled = true;
                        nextUIElement = uiElement.PredictFocus(FocusNavigationDirection.Left);
                        if (nextUIElement != null)
                        {
                            if (nextUIElement.GetType().Equals(typeof(DataGridCell)))
                            {
                                DataGridCellInfo nextCellInfo = new DataGridCellInfo((DataGridCell)nextUIElement);
                                this.SelectedItem = nextCellInfo.Item;
                                this.CurrentCell = nextCellInfo;
                            }
                        }
                    }
                    break;
                case Key.Up:
                    e.Handled = true;
                    nextUIElement = uiElement.PredictFocus(FocusNavigationDirection.Up);
                    if (nextUIElement != null)
                    {
                        if (nextUIElement.GetType().Equals(typeof(DataGridCell)))
                        {
                            DataGridCellInfo nextCellInfo = new DataGridCellInfo((DataGridCell)nextUIElement);
                            this.SelectedItem = nextCellInfo.Item;
                            this.CurrentCell = nextCellInfo;
                        }
                    }
                    else
                    {
                        int current = Items.IndexOf(((ExpandedDataGrid)sender).CurrentItem);
                        if (current > 0)
                        {
                            ScrollIntoView(Items[current - 1], Columns[0]);
                            this.SelectedItem = Items[current - 1];
                            this.CurrentCell = new DataGridCellInfo(this.Items[current - 1], this.Columns[columnIndex]);
                        }
                    }
                    break;
                case Key.Down:
                    e.Handled = true;
                    nextUIElement = uiElement.PredictFocus(FocusNavigationDirection.Down);
                    if (nextUIElement != null)
                    {
                        if (nextUIElement.GetType().Equals(typeof(DataGridCell)))
                        {
                            DataGridCellInfo nextCellInfo = new DataGridCellInfo((DataGridCell)nextUIElement);
                            this.SelectedItem = nextCellInfo.Item;
                            this.CurrentCell = nextCellInfo;
                        }
                    }
                    else
                    {
                        int current = Items.IndexOf(((ExpandedDataGrid)sender).CurrentItem);
                        if ((Items.Count - 1) > current)
                        {
                            ScrollIntoView(Items[current + 1], Columns[0]);
                            this.SelectedItem = Items[current + 1];
                            this.CurrentCell = new DataGridCellInfo(this.Items[current + 1], this.Columns[columnIndex]);
                        }

                    }
                    break;
            }
        }

        protected static void ColumnInfoChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var grid = (ExpandedDataGrid)dependencyObject;
            if (!grid._updatingColumnInfo) grid.ColumnInfoChanged();
        }

        protected void UpdateColumnInfo()
        {
            _updatingColumnInfo = true;
            List<ColumnInfo> listColumnInfo = new List<ColumnInfo>();
            foreach (var column in Columns)
            {
                if (column.Visibility == Visibility.Visible || (ColumnInfo != null && ColumnInfo.Any(x => x.PropertyPath == column.SortMemberPath)))
                {
                    listColumnInfo.Add(new ColumnInfo(column));
                }
            }
            ColumnInfo = new ObservableCollection<ColumnInfo>(listColumnInfo);
            _updatingColumnInfo = false;
        }

        protected override void OnColumnReordered(DataGridColumnEventArgs e)
        {
            UpdateColumnInfo();
            base.OnColumnReordered(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (_inWidthChange)
            {
                _inWidthChange = false;
                SaveColumnInfo();
            }
            base.OnPreviewMouseLeftButtonUp(e);
        }

        protected virtual void ColumnInfoChanged()
        {
            Items.SortDescriptions.Clear();
            if (ColumnInfo != null)
            {
                foreach (var column in ColumnInfo)
                {
                    Func<DataGridColumn, bool> func = c =>
                    {
                        var headerName = GetHeader(c) == null ? "" : GetHeader(c).ToString();
                        return column.PropertyPath.Equals(c.SortMemberPath) && (column.Header ?? "").Equals(headerName);
                    };
                    var realColumn = Columns.FirstOrDefault(func);
                    if (realColumn is null) { continue; }
                    column.Apply(realColumn, Columns.Count, Items.SortDescriptions);
                }
            }
        }

        protected virtual void LoadElementFrozenColumnData()
        {
            if (ElementFrozenColumn is ComboBox)
            {
                var comboBox = (ComboBox)ElementFrozenColumn;
                Dictionary<int, string> columnDic = new Dictionary<int, string>();
                foreach (var column in this.Columns)
                {
                    if (column.Visibility == Visibility.Visible)
                    {
                        int colSpan = 1;
                        if (column.GetType() == typeof(DataGridTextColumn))
                        {
                            colSpan = DataGridTextColumn.GetColumnSpan(column);
                            if (colSpan == 0)
                                continue;
                        }
                        string contentProperty = string.Empty;
                        if (colSpan > 1)
                            contentProperty = DataGridTextColumn.GetColumnSpanTitle(column);
                        else contentProperty = GetHeader(column)?.ToString() ?? string.Empty;
                        // báo cáo quyết toán nguồn vốn đầu tư
                        if (this.Name == "dgdQuyetToanVDTDetailTongHop")
                        {
                            // với các cột có xuống dòng thì lấy giá trị đầu (là các từ)
                            if (contentProperty.Split('\n').Length > 1)
                            {
                                contentProperty = contentProperty.Split('\n').ElementAt(0).ToString();
                            }
                            // còn lại lấy tất cả
                        }
                        columnDic.Add(column.DisplayIndex + colSpan - 1, contentProperty.ToString());
                    }
                }
                List<ComboboxItem> dataGridColumns = new List<ComboboxItem>();
                foreach (var item in columnDic)
                {
                    dataGridColumns.Add(new ComboboxItem(item.Value, item.Key.ToString()));
                }

                if (this.FrozenColumnCount < dataGridColumns.Count && this.FrozenColumnCount > 0)
                {
                    var selectedItem = dataGridColumns.ElementAt(this.FrozenColumnCount - 1);
                    comboBox.SelectedItem = selectedItem;
                    this.FrozenColumnCount = string.IsNullOrEmpty(selectedItem.ValueItem) ? 1 : (int.Parse(selectedItem.ValueItem) + 1);
                }
                else
                {
                    var selectedItem = dataGridColumns.FirstOrDefault();
                    comboBox.SelectedItem = selectedItem;
                    if (selectedItem != null)
                    {
                        this.FrozenColumnCount = string.IsNullOrEmpty(selectedItem.ValueItem) ? 1 : (int.Parse(selectedItem.ValueItem) + 1);
                    }
                }

                comboBox.ItemsSource = new ObservableCollection<ComboboxItem>(dataGridColumns);

                RoutedEventHandler selectionHandler = (object sender, RoutedEventArgs e) =>
                {
                    var comboBox = (ComboBox)sender;
                    if (comboBox.SelectedItem != null)
                    {
                        this.FrozenColumnCount = int.Parse(((ComboboxItem)comboBox.SelectedItem).ValueItem) + 1;
                        if (_footerDataGrid != null)
                            _footerDataGrid.FrozenColumnCount = this.FrozenColumnCount;
                        if (_headerDataGrid != null)
                            _headerDataGrid.FrozenColumnCount = this.FrozenColumnCount;
                        if (Application.Current.Properties.Contains(this.Name))
                            ((DataGridInformation)Application.Current.Properties[this.Name]).FrozenColumnCount = this.FrozenColumnCount;
                        else
                            Application.Current.Properties.Add(this.Name, new DataGridInformation(ColumnInfo == null ? new ObservableCollection<ColumnInfo>() : ColumnInfo, this.FrozenColumnCount));
                    }
                };
                comboBox.AddHandler(ComboBox.SelectionChangedEvent, selectionHandler);
            }
        }

        protected virtual void LoadElementDynamicColumnData()
        {
            if (ElementDynamicColumn is PopupBox)
            {
                PopupBox popupBox = (PopupBox)ElementDynamicColumn;
                var checkboxDictionary = new Dictionary<string, bool>();
                var stackPanel = new FrameworkElementFactory(typeof(StackPanel));

                if (popupBox.PopupContent is ScrollViewer)
                {
                    ((ScrollViewer)popupBox.PopupContent).ContentTemplate = new DataTemplate
                    {
                        VisualTree = stackPanel
                    };
                }
                else
                {
                    popupBox.PopupContentTemplate = new DataTemplate
                    {
                        VisualTree = stackPanel
                    };
                }

                foreach (var column in this.Columns)
                {
                    if (column.Visibility == Visibility.Visible || (ColumnInfo != null && ColumnInfo.Any(x => x.PropertyPath == column.SortMemberPath)))
                    {
                        int colSpan = 1;
                        if (column.GetType() == typeof(DataGridTextColumn))
                        {
                            colSpan = DataGridTextColumn.GetColumnSpan(column);
                            if (colSpan == 0)
                                continue;
                        }
                        var checkbox = new FrameworkElementFactory(typeof(CheckBox));
                        var style = new Style
                        {
                            TargetType = typeof(CheckBox),
                            BasedOn = (Style)Application.Current.TryFindResource("MaterialDesignCheckBox")
                        };
                        style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(10, 10, 10, 10)));
                        checkbox.SetValue(FrameworkElement.StyleProperty, style);
                        string contentProperty = string.Empty;
                        if (colSpan > 1)
                            contentProperty = DataGridTextColumn.GetColumnSpanTitle(column);
                        else contentProperty = GetHeader(column)?.ToString() ?? string.Empty;
                        checkbox.SetValue(ContentControl.ContentProperty, contentProperty);
                        checkbox.SetValue(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center);

                        if (column.SortMemberPath.Equals("GiamDT") || column.SortMemberPath.Equals("TangDT") ||
                            column.SortMemberPath.Equals("SoKiemTra") || column.SortMemberPath.Equals("TongSoKiemTra") ||
                            column.SortMemberPath.Equals("DuToan") || column.SortMemberPath.Equals("TongCanCuDuToan") ||
                            column.SortMemberPath.Equals("SoKiemTraMHHV") || column.SortMemberPath.Equals("SoKiemTraDT")
                            //|| column.SortMemberPath.Equals("DuToanMHCHV") || column.SortMemberPath.Equals("DuToanDT")
                            )
                        {
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, false);
                            column.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if (Name.Equals("DgDistributionDetail1") && column.SortMemberPath.Equals("DuToan"))
                        {
                            column.Visibility = Visibility.Visible;
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if ((Name.Equals("DgDistributionChildDetail1") || Name.Equals("DgCheckDetail2")) && (column.SortMemberPath.Equals("DuToan") || column.SortMemberPath.Equals("SoKiemTra")))
                        {
                            column.Visibility = Visibility.Visible;
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if ((Name.Equals("DgDistributionDetail1") || Name.Equals("dgdDataPlanBeginYearIndex")) && column.SortMemberPath.Equals("SoKiemTra"))
                        {
                            column.Visibility = Visibility.Visible;
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if ((Name.Equals("DgCheckDetail2") || Name.Equals("DgDistributionDetail1")) && (column.SortMemberPath.Equals("SoKiemTraMHHV") || column.SortMemberPath.Equals("SoKiemTraDT")))
                        {
                            column.Visibility = Visibility.Visible;
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if (Name == "dgdDataAllocationDetail" && column.SortMemberPath.Equals("DuToan"))
                        {
                            column.Visibility = Visibility.Visible;
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, column.Visibility == Visibility.Visible ? true : false);
                        }

                        if (Name == "dgdPheDuyetQuyetToanNamDetail" && column.SortMemberPath.Equals("IMa"))
                        {
                            checkbox.SetValue(ToggleButton.IsCheckedProperty, false);
                            column.Visibility = Visibility.Collapsed;
                        }

                        checkboxDictionary[column.SortMemberPath] = true;

                        // load all checkBox column
                        RoutedEventHandler checkBoxLoaded = (object sender, RoutedEventArgs e) =>
                        {
                            //Do something
                        };

                        // checkBox is checked
                        RoutedEventHandler checkedHandler = (object sender, RoutedEventArgs e) =>
                        {
                            var cb = (CheckBox)e.OriginalSource;
                            if (cb.IsMouseOver)
                            {
                                checkboxDictionary[column.SortMemberPath] = true;
                                this.CancelEdit();
                                column.Visibility = Visibility.Visible;
                                if (_footerDataGrid != null)
                                    _footerDataGrid.Columns[column.DisplayIndex].Visibility = Visibility.Visible;
                                if (_headerDataGrid != null)
                                {
                                    if (column.GetType() == typeof(DataGridTextColumn))
                                    {
                                        int colSpan = DataGridTextColumn.GetColumnSpan(column);
                                        if (colSpan > 1)
                                        {
                                            for (int i = 0; i < colSpan; i++)
                                            {
                                                this.Columns[column.DisplayIndex + i].Visibility = Visibility.Visible;
                                            }
                                        }
                                    }
                                    if(_headerDataGrid.Columns.Count() > 0)
                                        _headerDataGrid.Columns[column.DisplayIndex].Visibility = Visibility.Visible;
                                }
                                SaveColumnInfo();
                                LoadElementFrozenColumnData();
                            }
                        };

                        // checkBox is unChecked
                        RoutedEventHandler uncheckedHandler = (object sender, RoutedEventArgs e) =>
                        {
                            var cb = (CheckBox)e.OriginalSource;
                            if (cb.IsMouseOver)
                            {
                                checkboxDictionary[column.SortMemberPath] = false;
                                this.CancelEdit();
                                column.Visibility = Visibility.Collapsed;
                                if (_footerDataGrid != null)
                                    _footerDataGrid.Columns[column.DisplayIndex].Visibility = Visibility.Collapsed;
                                if (_headerDataGrid != null)
                                {
                                    if (column.GetType() == typeof(DataGridTextColumn))
                                    {
                                        int colSpan = DataGridTextColumn.GetColumnSpan(column);
                                        if (colSpan > 1)
                                        {
                                            for (int i = 0; i < colSpan; i++)
                                            {
                                                this.Columns[column.DisplayIndex + i].Visibility = Visibility.Collapsed;
                                            }
                                        }
                                    }
                                    if (_headerDataGrid.Columns.Count() > 0)
                                        _headerDataGrid.Columns[column.DisplayIndex].Visibility = Visibility.Collapsed;
                                }
                                SaveColumnInfo();
                                LoadElementFrozenColumnData();
                            }
                        };
                        checkbox.AddHandler(FrameworkElement.LoadedEvent, checkBoxLoaded);
                        checkbox.AddHandler(ToggleButton.CheckedEvent, checkedHandler);
                        checkbox.AddHandler(ToggleButton.UncheckedEvent, uncheckedHandler);
                        stackPanel.AppendChild(checkbox);
                    }
                }
                Button btnShowCol = FnCommonUtils.FindChild<Button>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), "btnShowCol");
                if (btnShowCol != null)
                {
                    btnShowCol.Command = new RelayCommand(obj =>
                    {
                        popupBox.IsPopupOpen = true;
                    });
                }
                Button btnShowColSelfPay = FnCommonUtils.FindChild<Button>(Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive), "btnShowColSelfPay");
                if (btnShowColSelfPay != null)
                {
                    btnShowColSelfPay.Command = new RelayCommand(obj =>
                    {
                        popupBox.IsPopupOpen = true;
                    });
                }
            }
        }

        #region config footer datagrid
        protected virtual void LoadFooterDataGrid()
        {
            ScrollViewer associatedScrollViewer = (ScrollViewer)this.Template.FindName(ScrollViewerNameInTemplate, this);
            _footerDataGrid = (DataGrid)associatedScrollViewer.Template.FindName("PART_DataGridFooter", associatedScrollViewer);
            if (_footerDataGrid != null)
            {
                if (EnableFooterDataGrid)
                {
                    _footerDataGrid.FrozenColumnCount = this.FrozenColumnCount;

                    ScrollViewer scrollview = FindVisualChild<ScrollViewer>(this);
                    scrollview.SizeChanged += (s, ev) =>
                    {
                        Visibility verticalVisibility = scrollview.ComputedVerticalScrollBarVisibility;
                        if (verticalVisibility == Visibility.Visible)
                            _footerDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                        else if (verticalVisibility == Visibility.Hidden || verticalVisibility == Visibility.Collapsed)
                            _footerDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    };

                    foreach (var column in this.Columns)
                    {
                        if (column is DataGridBoundColumn boundColumn)
                        {
                            DataGridTextColumn newDC = new DataGridTextColumn();
                            Binding b = (column as DataGridBoundColumn).Binding as Binding;
                            newDC.Width = column.Width;
                            newDC.DisplayIndex = column.DisplayIndex;
                            newDC.Header = column.Header;
                            newDC.Visibility = column.Visibility;
                            if (b != null)
                                newDC.Binding = new Binding
                                {
                                    Path = new PropertyPath(b.Path.Path)
                                };
                            _footerDataGrid.Columns.Add(newDC);
                        }
                        else
                        {
                            DataGridTextColumn newDC = new DataGridTextColumn();
                            newDC.Width = column.Width;
                            newDC.DisplayIndex = column.DisplayIndex;
                            newDC.Header = column.Header;
                            newDC.Visibility = column.Visibility;
                            _footerDataGrid.Columns.Add(newDC);
                        }
                    }
                    BindFooterDataGridColumns();
                    CreateFooterDataGridItemSource();
                }
                else
                {
                    _footerDataGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void BindFooterDataGridColumns()
        {
            int sourceColIndex = 0;
            for (int associatedColIndex = 0; associatedColIndex < _footerDataGrid.Columns.Count; associatedColIndex++)
            {
                var colAssociated = _footerDataGrid.Columns[associatedColIndex];
                if (sourceColIndex >= this.Columns.Count) break;

                var colSource = this.Columns[sourceColIndex];
                Binding bindingWith = new Binding();
                bindingWith.Mode = BindingMode.TwoWay;
                bindingWith.Source = colSource;
                bindingWith.Path = new PropertyPath(DataGridColumn.WidthProperty);
                BindingOperations.SetBinding(colAssociated, DataGridColumn.WidthProperty, bindingWith);

                Binding bindingVisible = new Binding();
                bindingVisible.Mode = BindingMode.TwoWay;
                bindingVisible.Source = colSource;
                bindingVisible.Path = new PropertyPath(DataGridColumn.VisibilityProperty);
                BindingOperations.SetBinding(colAssociated, DataGridColumn.VisibilityProperty, bindingVisible);
                sourceColIndex++;
            }
        }

        private void CreateFooterDataGridItemSource()
        {
            DataTable dt;
            try
            {
                if (this.ItemsSource == null) return;
                dt = new DataTable();
                Type itemType = this.ItemsSource.GetType().GetGenericArguments()[0];
                foreach (PropertyInfo pi in itemType.GetProperties())
                {
                    if (pi.Name != "ExtensionData")
                        dt.Columns.Add(new DataColumn(pi.Name, Type.GetType("System.String")));
                }

                dt.Rows.Add(dt.NewRow());
                _footerDataGrid.ItemsSource = dt.DefaultView;

                SetValueForFooterDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception("CreateFooterItemSource:" + ex.Message);
            }
        }

        private void DataGrid_CurrentCellsChanged(object sender, EventArgs e)
        {
            if (_footerDataGrid != null && EnableFooterDataGrid)
                SetValueForFooterDataGrid();
        }

        private void SetValueForFooterDataGrid()
        {
            foreach (var column in this.Columns)
            {
                if (column is DataGridTextColumn)
                {
                    String prop = ((_footerDataGrid.Columns[column.DisplayIndex] as DataGridBoundColumn).Binding as Binding).Path.Path;
                    double total = ((VTS.QLNS.CTC.App.Component.DataGridTextColumn)column).TotalValue;
                    string totalStr = string.Empty;
                    if (total != 0)
                        totalStr = String.Format("{0:N0}", total);
                    (_footerDataGrid.ItemsSource as DataView)[0][prop] = totalStr;
                }
            }
        }

        private void MainDataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                if (e.HorizontalChange != 0)
                {
                    if (_footerDataGrid != null)
                    {
                        ScrollViewer associatedScrollViewer = (ScrollViewer)_footerDataGrid.Template.FindName(ScrollViewerNameInTemplate, _footerDataGrid);
                        associatedScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
                    }
                    if (_headerDataGrid != null)
                    {
                        ScrollViewer associatedScrollViewer = (ScrollViewer)_headerDataGrid.Template.FindName(ScrollViewerNameInTemplate, _headerDataGrid);
                        associatedScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("MainDataGrid_ScrollChanged:" + ex.Message);
            }
        }
        #endregion

        #region config multiple header datagrid
        protected virtual void LoadHeaderDataGrid()
        {
            ScrollViewer associatedScrollViewer = (ScrollViewer)this.Template.FindName(ScrollViewerNameInTemplate, this);
            _headerDataGrid = (DataGrid)associatedScrollViewer.Template.FindName("PART_MultiHeader", associatedScrollViewer);
            if (this.Name == "dgdQuyetToanVDTDetailTongHop" || this.Name == "dgdQuyetToanVDTDetailPhanTich")
            {
                _headerDataGrid.Columns.Clear();
            }
            if (_headerDataGrid != null)
            {
                if (EnableHeaderDataGrid)
                {
                    ScrollViewer scrollview = FindVisualChild<ScrollViewer>(this);
                    scrollview.SizeChanged += (s, ev) =>
                    {
                        Visibility verticalVisibility = scrollview.ComputedVerticalScrollBarVisibility;
                        if (verticalVisibility == Visibility.Visible)
                            _headerDataGrid.Margin = new Thickness(0, 5, SystemParameters.VerticalScrollBarWidth, 0);
                        else if (verticalVisibility == Visibility.Hidden || verticalVisibility == Visibility.Collapsed)
                            _headerDataGrid.Margin = new Thickness(0, 5, 0, 0);
                    };
                    _headerDataGrid.FrozenColumnCount = this.FrozenColumnCount;
                    foreach (var column in this.Columns)
                    {
                        if (column is DataGridBoundColumn boundColumn)
                        {
                            DataGridTextColumn newDC = new DataGridTextColumn();
                            Binding b = (column as DataGridBoundColumn).Binding as Binding;
                            newDC.Width = column.Width;
                            newDC.DisplayIndex = column.DisplayIndex;
                            newDC.CellStyle = column.CellStyle;
                            newDC.Header = column.Header;
                            newDC.Visibility = column.Visibility;
                            int columnSpan = (int)column.GetValue(DataGridTextColumn.ColumnSpanProperty);
                            if (columnSpan != 0)
                                newDC.Header = (string)column.GetValue(DataGridTextColumn.ColumnSpanTitleProperty);
                            newDC.SetValue(DataGridTextColumn.ColumnSpanProperty, columnSpan);
                            if (b != null)
                                newDC.Binding = new Binding
                                {
                                    Path = new PropertyPath(b.Path.Path)
                                };
                            _headerDataGrid.Columns.Add(newDC);
                        }
                        else
                        {
                            DataGridTextColumn newDC = new DataGridTextColumn();
                            newDC.Width = column.Width;
                            newDC.DisplayIndex = column.DisplayIndex;
                            newDC.CellStyle = column.CellStyle;
                            newDC.Header = column.Header;
                            newDC.Visibility = column.Visibility;
                            int columnSpan = (int)column.GetValue(DataGridTextColumn.ColumnSpanProperty);
                            if (columnSpan != 0)
                                newDC.Header = (string)column.GetValue(DataGridTextColumn.ColumnSpanTitleProperty);
                            newDC.SetValue(DataGridTextColumn.ColumnSpanProperty, columnSpan);
                            _headerDataGrid.Columns.Add(newDC);
                        }
                    }
                    BindheaderDataGridColumns();
                    CreateheaderDataGridItemSource();
                }
                else
                {
                    _headerDataGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void CreateheaderDataGridItemSource()
        {
            DataTable dt;
            try
            {
                if (this.ItemsSource == null) return;
                dt = new DataTable();
                Type itemType = this.ItemsSource.GetType().GetGenericArguments()[0];
                foreach (PropertyInfo pi in itemType.GetProperties())
                {
                    if (pi.Name != "ExtensionData")
                        dt.Columns.Add(new DataColumn(pi.Name, Type.GetType("System.String")));
                }

                dt.Rows.Add(dt.NewRow());
                _headerDataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateFooterItemSource:" + ex.Message);
            }
        }

        private void BindheaderDataGridColumns()
        {
            int sourceColIndex = 0;
            for (int associatedColIndex = 0; associatedColIndex < _headerDataGrid.Columns.Count; associatedColIndex++)
            {
                var colAssociated = _headerDataGrid.Columns[associatedColIndex];
                int columnSpan = DataGridTextColumn.GetColumnSpan(colAssociated);
                if (columnSpan == 0)
                {
                    colAssociated.Visibility = Visibility.Hidden;
                    BindingOperations.SetBinding(colAssociated, DataGridColumn.WidthProperty, new Binding());
                    continue;
                }
                if (sourceColIndex >= this.Columns.Count) break;

                if (columnSpan == 1)
                {
                    var colSource = this.Columns[sourceColIndex];
                    Binding bindingWidth = new Binding();
                    bindingWidth.Mode = BindingMode.TwoWay;
                    bindingWidth.Source = colSource;
                    bindingWidth.Path = new PropertyPath(DataGridColumn.WidthProperty);
                    BindingOperations.SetBinding(colAssociated, DataGridColumn.WidthProperty, bindingWidth);

                    Binding bindingVisible = new Binding();
                    bindingVisible.Mode = BindingMode.TwoWay;
                    bindingVisible.Source = colSource;
                    bindingVisible.Path = new PropertyPath(DataGridColumn.VisibilityProperty);
                    BindingOperations.SetBinding(colAssociated, DataGridColumn.VisibilityProperty, bindingVisible);
                    sourceColIndex++;
                }
                else
                {
                    MultiBinding multiBinding = new MultiBinding();
                    multiBinding.Converter = WidthConverter;
                    for (int i = 0; i < columnSpan; i++)
                    {
                        var colSource = this.Columns[sourceColIndex];
                        Binding binding = new Binding();
                        binding.Source = colSource;
                        binding.Path = new PropertyPath(DataGridColumn.WidthProperty);

                        multiBinding.Bindings.Add(binding);

                        binding = new Binding();
                        binding.Source = colSource;
                        multiBinding.Bindings.Add(binding);

                        sourceColIndex++;
                    }
                    pd.AddValueChanged(colAssociated, new EventHandler(ColumnWidthPropertyChanged));
                    BindingOperations.SetBinding(colAssociated, InternalWidthOnColumnProperty, multiBinding);
                }
            }
        }

        private void ColumnWidthPropertyChanged(object sender, EventArgs e)
        {
            // listen for when the mouse is released
            _columnWidthChanging = true;
            if (sender != null)
            {
                DataGridTextColumn column = (DataGridTextColumn)sender;
                _columnDisplayIndex = column.DisplayIndex;
                if (DataGridTextColumn.GetColumnSpan(column) > 1)
                {
                    Mouse.AddPreviewMouseMoveHandler(_headerDataGrid, headerDataGrid_MouseLeftButtonUp);
                }
            }
        }

        private void headerDataGrid_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            int columnSpan = DataGridTextColumn.GetColumnSpan(_headerDataGrid.Columns[_columnDisplayIndex]);
            double totalWidth = 0.0;
            if (_columnWidthChanging && e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.OriginalSource.GetType() == typeof(System.Windows.Controls.Primitives.Thumb))
                {
                    var mouse = (System.Windows.Controls.Primitives.Thumb)e.OriginalSource;
                    if (mouse.IsDragging)
                    {
                        _columnWidthChanging = false;

                        double subWidth = 0.0;
                        for (int i = 0; i < columnSpan - 1; i++)
                        {
                            subWidth += this.Columns[_columnDisplayIndex + i].ActualWidth;
                        }
                        if (_headerDataGrid.Columns[_columnDisplayIndex].ActualWidth >= subWidth)
                        {
                            this.Columns[_columnDisplayIndex + columnSpan - 1].Width = new DataGridLength(_headerDataGrid.Columns[_columnDisplayIndex].ActualWidth - subWidth);
                            if (this.Columns[_columnDisplayIndex + columnSpan - 1].ActualWidth == this.Columns[_columnDisplayIndex + columnSpan - 1].MinWidth)
                            {
                                _headerDataGrid.Columns[_columnDisplayIndex].MinWidth = _headerDataGrid.Columns[_columnDisplayIndex].ActualWidth;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < columnSpan; i++)
            {
                totalWidth += this.Columns[_columnDisplayIndex + i].ActualWidth;
            }
            _headerDataGrid.Columns[_columnDisplayIndex].Width = new DataGridLength(totalWidth);
        }

        private static readonly DependencyProperty InternalWidthOnColumnProperty =
                            DependencyProperty.RegisterAttached("InternalWidthOnColumn", typeof(Nullable<Double>), typeof(ExpandedDataGrid), new UIPropertyMetadata(null, InternalWidthOnColumnPropertyChanged));

        private static void InternalWidthOnColumnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((DataGridColumn)sender).Width = ((Nullable<double>)e.NewValue).Value;
                ((DataGridColumn)sender).MinWidth = 0;
            }
        }
        #endregion

        public void ReloadElementFrozenColumnData()
        {
            if (this.FrozenColumnCount != 0)
            {
                LoadElementFrozenColumnData();
            }
        }

        public void ReloadData()
        {
            if (this.FrozenColumnCount != 0)
            {
                LoadElementFrozenColumnData();
                LoadElementDynamicColumnData();
                LoadHeaderDataGrid();
                LoadFooterDataGrid();
            }
        }

        protected virtual object GetHeader(DataGridColumn column)
        {
            return column.Header is TextBlock block ? block.Text
                        : column.Header is Border border ? "CheckBox" : column.Header;
        }

        private void SaveColumnInfo()
        {
            if (IsSaveDataGridInfo)
            {
                UpdateColumnInfo();
                if (Application.Current.Properties.Contains(this.Name))
                    ((DataGridInformation)Application.Current.Properties[this.Name]).ColumnInfo = ColumnInfo;
                else
                    Application.Current.Properties.Add(this.Name, new DataGridInformation(ColumnInfo, this.FrozenColumnCount));
            }
        }

        private static IMultiValueConverter WidthConverter = new WidthConverterClass();
        private class WidthConverterClass : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                double result = 0;
                foreach (var value in values)
                {
                    DataGridColumn column = value as DataGridColumn;
                    if (column != null)
                    {
                        result = result + column.ActualWidth;
                    }
                }
                return result;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            {
                return null;
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj, string elementName = null) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem && elementName == null)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private bool KeyLeftCheckHandel(UIElement item)
        {
            if (item == null || !(item is TextBox)) return true;
            var obj = (TextBox)item;
            if (obj.CaretIndex == obj.Text.Length) return true;
            return false;
        }
    }

    public struct ColumnInfo
    {
        public string Header;
        public string PropertyPath;
        public ListSortDirection? SortDirection;
        public int DisplayIndex;
        public double WidthValue;
        public DataGridLengthUnitType WidthType;
        public byte Visibility;

        public ColumnInfo(DataGridColumn column)
        {
            Header = null;
            if (column.Header != null)
            {
                if (column.Header is TextBlock block)
                {
                    Header = block.Text;
                }
                else if (column.Header is Border border)
                {
                    Header = "CheckBox";
                }
                else if (column.Header is StackPanel stackPanel)
                {
                    TextBlock t = FnCommonUtils.FindChild<TextBlock>(stackPanel, "col");
                    Header = t == null ? null : t.Text;
                }
                else
                {
                    Header = column.Header.ToString();
                }
            }
            PropertyPath = column.SortMemberPath;
            WidthValue = column.Width.DisplayValue;
            WidthType = column.Width.UnitType;
            SortDirection = column.SortDirection;
            DisplayIndex = column.DisplayIndex;
            Visibility = (byte)column.Visibility;
        }

        public void Apply(DataGridColumn column, int gridColumnCount, SortDescriptionCollection sortDescriptions)
        {
            if (WidthValue.Equals(double.NaN)) return;
            column.Width = new DataGridLength(WidthValue, WidthType);
            column.SortDirection = SortDirection;
            if (SortDirection != null)
            {
                sortDescriptions.Add(new SortDescription(PropertyPath, SortDirection.Value));
            }
            if (column.DisplayIndex != DisplayIndex)
            {
                var maxIndex = (gridColumnCount == 0) ? 0 : gridColumnCount - 1;
                column.DisplayIndex = (DisplayIndex <= maxIndex) ? DisplayIndex : maxIndex;
            }
            Visibility visibility;
            if (Enum.TryParse(Visibility.ToString(), out visibility))
                column.Visibility = visibility;
        }
    }

    public class DataGridInformation
    {
        public ObservableCollection<ColumnInfo> ColumnInfo { get; set; }
        public int FrozenColumnCount { get; set; }

        public DataGridInformation() { }

        public DataGridInformation(ObservableCollection<ColumnInfo> columnInfo, int frozenColumnCount)
        {
            ColumnInfo = columnInfo;
            FrozenColumnCount = frozenColumnCount;
        }
    }
}
