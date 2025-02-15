﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Component
{
    public class CategoryExpandedDataGrid : ExpandedDataGrid
    {
        public static readonly DependencyProperty ModelNameInfoProperty = DependencyProperty.Register("ModelNameInfo",
                typeof(string), typeof(CategoryExpandedDataGrid),
                new FrameworkPropertyMetadata(SetModelName)
            );

        public static readonly DependencyProperty IsReferencePopupProperty = DependencyProperty.Register("IsReferencePopup",
                typeof(bool), typeof(CategoryExpandedDataGrid),
                new FrameworkPropertyMetadata(SetIsReferencePopup)
            );

        private bool _isReferencePopup { get; set; }

        public FrameworkElement ModelNameInfo
        {
            get { return (FrameworkElement)GetValue(ModelNameInfoProperty); }
            set { SetValue(ModelNameInfoProperty, value); }
        }

        public FrameworkElement IsReferencePopup
        {
            get { return (FrameworkElement)GetValue(IsReferencePopupProperty); }
            set { SetValue(IsReferencePopupProperty, value); }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.FrozenColumnCount = 1;
            if (_isReferencePopup)
            {
                return;
            }
            EventHandler sortDirectionChangedHandler = (sender, x) => UpdateColumnInfo();
            EventHandler widthPropertyChangedHandler = (sender, x) => _inWidthChange = true;
            var sortDirectionPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.SortDirectionProperty, typeof(DataGridColumn));
            var widthPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(DataGridColumn.WidthProperty, typeof(DataGridColumn));
            Loaded += (sender, x) =>
            {
                if (_isReferencePopup)
                {
                    return;
                }
                FrozenColumnCount = 1;
                LoadColumnInfo(sortDirectionPropertyDescriptor, widthPropertyDescriptor, sortDirectionChangedHandler, widthPropertyChangedHandler);
            };
            AutoGeneratedColumns += (sender, x) =>
            {
                if (_isReferencePopup)
                {
                    return;
                }
                FrozenColumnCount = 1;
                LoadColumnInfo(sortDirectionPropertyDescriptor, widthPropertyDescriptor, sortDirectionChangedHandler, widthPropertyChangedHandler);
            };
            Unloaded += (sender, x) =>
            {
                foreach (var column in Columns)
                {
                    sortDirectionPropertyDescriptor.RemoveValueChanged(column, sortDirectionChangedHandler);
                    widthPropertyDescriptor.RemoveValueChanged(column, widthPropertyChangedHandler);
                }
            };
        }

        private void LoadColumnInfo(DependencyPropertyDescriptor sortDirectionPropertyDescriptor, DependencyPropertyDescriptor widthPropertyDescriptor
            , EventHandler sortDirectionChangedHandler, EventHandler widthPropertyChangedHandler)
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
                    ColumnInfo = dataGridInfo.ColumnInfo;
                    this.FrozenColumnCount = dataGridInfo.FrozenColumnCount;
                }
                this.LoadElementFrozenColumnData();
                this.LoadElementDynamicColumnData();
                this.ColumnInfoChanged();
            }
        }

        private static void SetModelName(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var grid = (CategoryExpandedDataGrid)dependencyObject;
            grid.Name = e.NewValue.ToString();
        }

        protected override object GetHeader(DataGridColumn c)
        {
            StackPanel panel = c.Header as StackPanel;
            TextBlock t = FnCommonUtils.FindChild<TextBlock>(panel, "");
            return t == null ? string.Empty : t.Text;
        }

        private static void SetIsReferencePopup(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var grid = (CategoryExpandedDataGrid)dependencyObject;
            grid._isReferencePopup = (bool)e.NewValue;
        }

        protected override void ColumnInfoChanged()
        {
            if (_isReferencePopup)
            {
                return;
            }
            base.ColumnInfoChanged();
        }

        protected override void LoadElementFrozenColumnData()
        {
            if (_isReferencePopup)
            {
                return;
            }
            base.LoadElementFrozenColumnData();
        }

        protected override void LoadElementDynamicColumnData()
        {
            if (_isReferencePopup)
            {
                return;
            }
            base.LoadElementDynamicColumnData();
        }
    }
}
