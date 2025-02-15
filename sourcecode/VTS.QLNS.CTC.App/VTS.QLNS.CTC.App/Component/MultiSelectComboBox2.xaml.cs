﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace VTS.QLNS.CTC.App.Component
{
    /// <summary>
    /// Interaction logic for MultiSelectComboBox2.xaml
    /// </summary>
    public partial class MultiSelectComboBox2 : UserControl, INotifyPropertyChanged
    {
        private const string All = "Tất cả";
        private ObservableCollection<Node> _nodeList;
        public MultiSelectComboBox2()
        {
            InitializeComponent();
            _nodeList = new ObservableCollection<Node>();
        }

        #region Dependency Properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(Dictionary<string, object>), typeof(MultiSelectComboBox2), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(MultiSelectComboBox2.OnItemsSourceChanged)));

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(Dictionary<string, object>), typeof(MultiSelectComboBox2), new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(MultiSelectComboBox2.OnSelectedItemsChanged)));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MultiSelectComboBox2), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(MultiSelectComboBox2), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ChosenTextProperty =
            DependencyProperty.Register("ChosenText", typeof(string), typeof(MultiSelectComboBox2), new UIPropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Dictionary<string, object> ItemsSource
        {
            get { return (Dictionary<string, object>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public Dictionary<string, object> SelectedItems
        {
            get { return (Dictionary<string, object>)GetValue(SelectedItemsProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public string ChosenText
        {
            get { return (string)GetValue(ChosenTextProperty); }
            set { SetValue(ChosenTextProperty, value); }
        }

        #endregion

        #region Events
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox2 control = (MultiSelectComboBox2)d;
            control.DisplayInControl();
            control.SetText();
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox2 control = (MultiSelectComboBox2)d;
            control.SelectNodes();
            control.SetText();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;

            if (clickedBox.Content as string == All)
            {
                foreach (Node node in _nodeList)
                {
                    node.IsSelected = (bool)clickedBox.IsChecked;
                }
            }
            else
            {
                int _selectedCount = 0;
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected && s.Title != All)
                        _selectedCount++;
                }
                if (_selectedCount == _nodeList.Count - 1)
                    _nodeList.FirstOrDefault(i => i.Title == All).IsSelected = true;
                else
                    _nodeList.FirstOrDefault(i => i.Title == All).IsSelected = false;
            }
            SetSelectedItems();
            NotifyPropertyChanged(nameof(SelectedItems));
            SetText();

        }
        #endregion


        #region Methods
        private void SelectNodes()
        {
            foreach (KeyValuePair<string, object> keyValue in SelectedItems)
            {
                Node node = _nodeList.FirstOrDefault(i => i.Title == keyValue.Key);
                if (node != null)
                    node.IsSelected = true;
            }
        }

        private void SetSelectedItems()
        {
            if (SelectedItems == null)
                SelectedItems = new Dictionary<string, object>();
            SelectedItems.Clear();
            foreach (Node node in _nodeList)
            {
                if (node.IsSelected && node.Title != All)
                {
                    if (this.ItemsSource.Count > 0)

                        SelectedItems.Add(node.Title, this.ItemsSource[node.Title]);
                }
            }
        }

        private void DisplayInControl()
        {
            _nodeList.Clear();
            if (this.ItemsSource.Count > 0 && !this.ItemsSource.Any(x => x.Key == All))
                _nodeList.Add(new Node(All));
            foreach (KeyValuePair<string, object> keyValue in this.ItemsSource)
            {
                Node node = new Node(keyValue.Key);
                _nodeList.Add(node);
            }
            MultiSelectCombo.ItemsSource = _nodeList;
        }

        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected && s.Title == All)
                    {
                        displayText = new StringBuilder();
                        displayText.Append(All);
                        break;
                    }
                    else if (s.IsSelected && s.Title != All)
                    {
                        displayText.Append(s.Title);
                        displayText.Append(", ");
                    }
                }

                if (displayText.ToString().Split(',').Length > 6)
                {
                    this.Text = $"{displayText.ToString().Split(',').Length} {ChosenText}";
                }
                else
                {
                    this.Text = displayText.ToString().TrimEnd(new char[] { ',', ' ' });
                } 

                
            }
            // set DefaultText if nothing else selected
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.DefaultText;
            }
        }


        #endregion
    }

    public class Node : INotifyPropertyChanged
    {

        private string _title;
        private bool _isSelected;
        #region ctor
        public Node(string title)
        {
            Title = title;
        }
        #endregion

        #region Properties
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
