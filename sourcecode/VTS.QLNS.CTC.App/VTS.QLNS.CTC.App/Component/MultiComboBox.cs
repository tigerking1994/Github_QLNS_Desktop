using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Component
{
    [TemplatePart(Name = "PART_textBoxNewItem", Type=typeof(TextBox))]
    [TemplatePart(Name = "PART_labelContentPanel", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_newItemCreatedOkButton", Type = typeof(Button))]
    public class MultiComboBox : BindableListBox
    {
        #region Fields, Events, Constructors

        public event EventHandler ItemAdded;

        TextBlock _txtContentDropDown;
        Popup _popup;

        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }
        
        #endregion // Fields, Events and Constructors

        #region Public Properties

        /// <summary>
        /// Dependency property backing for the IsCreateNewEnabled property.
        /// </summary>
        public static readonly DependencyProperty IsCreateNewEnabledProperty =
            DependencyProperty.Register("IsCreateNewEnabled", typeof(bool), typeof(MultiComboBox));
        /// <summary>
        /// Get or set the value indicating whether the Create New Item button is visible in the drop down.
        /// </summary>
        public bool IsCreateNewEnabled
        {
            get { return (bool)GetValue(IsCreateNewEnabledProperty); }
            set { SetValue(IsCreateNewEnabledProperty, value); }
        }

        /// <summary>
        /// Dependency backing for the MaxDropDownHeight property.
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            ComboBox.MaxDropDownHeightProperty.AddOwner(typeof(MultiComboBox));
        /// <summary>
        /// Gets or sets a value indicating the maximium height of the drop down.
        /// </summary>
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        /// <summary>
        /// Dependency property backing for IsDropDownOpen.
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(MultiComboBox));
        /// <summary>
        /// Gets or sets the value indicating whether the drop down is open.
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        /// <summary>
        /// An object to separate the selected items in the main display.
        /// </summary>
        public object DisplaySeparator
        {
            get;
            set;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Create bindings and event handlers on named items.
        /// </summary>
        public override void OnApplyTemplate()
        {           
            base.OnApplyTemplate();

            _txtContentDropDown = Template.FindName("PART_labelContentPanel", this) as TextBlock;
            _popup = Template.FindName("PART_popup", this) as Popup;

            LoadLabelContents();
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            // Update label contents when selection changes.
            LoadLabelContents();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            // If selection mode is single, this overrides the behavior of keeping the
            // drop down open when a selection is made, which is the desired behavior 
            // for when selection mode is multiple.
            if (SelectionMode == SelectionMode.Single && IsDropDownOpen)
            {
                foreach (object item in this.Items)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)this.ItemContainerGenerator.ContainerFromItem(item);
                    if (listBoxItem != null && listBoxItem.IsMouseOver)
                    { 
                        // Select the item with the mouse over it and close the drop down.
                        SelectedItem = item;
                        IsDropDownOpen = false;
                        break;
                    }                    
                }                
            }
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            if (!IsDropDownOpen)
                return;

            // Set Handled here, popup doesn't recieve the click event so it stays open.
            e.Handled = true;

            if (SelectionMode != SelectionMode.Single)
            {
                // Click event will not reach the list box, so need to manually select or 
                // unselect the item which is under the current mouse position.
                foreach (object item in this.Items)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)this.ItemContainerGenerator.ContainerFromItem(item);
                    if (listBoxItem != null && listBoxItem.IsMouseOver)
                    {
                        listBoxItem.IsSelected = !listBoxItem.IsSelected;
                        return; // exit, no more to do.
                    }
                }
                // If execution reaches here, the mouse was not over any ListBoxItems, so it is either
                // over the toggle button or it is over the CreateNewItem button. If the latter, we want the 
                // mouse click to go through so the button can respond. (It in turn handles the event so the 
                // popup stays open.)
                if (_popup.Child.IsMouseOver)
                    e.Handled = false;
            }

        }

        /// <summary>
        /// Open the drop down with the enter key.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                IsDropDownOpen = !IsDropDownOpen;
                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }

        #endregion // Overrides

        #region Private
        /// <summary>
        /// Anytime the mouse clicks outside of this control, close the drop down.
        /// </summary>
        private void MouseHook_MouseClicked(Point point)
        {
            if (!IsMouseOver && IsDropDownOpen)
            {
                IsDropDownOpen = false;
            }
        }
       
        /// <summary>
        /// Sets the display contents for the label. Inserts the DisplaySeparator object beteen items.
        /// </summary>
        private void LoadLabelContents()
        {
            // Ignore if the panel is not present.
            if (_txtContentDropDown == null)
                return;

            // Clear current contents.
            _txtContentDropDown.Text = string.Empty;

            if (SelectionMode != SelectionMode.Single && SelectedItems != null)
            {                
                for (int x = 0; x < SelectedItems.Count; x++)
                {
                    // For each selected item, create a content control, 
                    // set the content of the control to be the selected item,
                    // and add the content control to the label panel's children.
                    _txtContentDropDown.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#000");
                    _txtContentDropDown.Text = string.Join(", ",((ObservableCollection<ComboboxItem>)SelectedItems).Select(n=>n.DisplayItem));
                }
            }
            else if (SelectedItem != null)
            {
                _txtContentDropDown.Text = ((ComboboxItem)SelectedItem).DisplayItem;
            }
        }

        /// <summary>
        /// Method to deap clone a Visual Object. Seems to do pretty well for 
        /// simple objects I have tried it on, at least. Got from a web posting.
        /// Guy's name is Justin-Josef Angel
        /// </summary>
        private static T Clone<T>(T source)
        {
            T cloned = (T)Activator.CreateInstance(source.GetType());

            foreach (PropertyInfo curPropInfo in source.GetType().GetProperties())
            {
                if (curPropInfo.GetGetMethod() != null
                    && (curPropInfo.GetSetMethod() != null))
                {
                    // Handle Non-indexer properties
                    if (curPropInfo.Name != "Item")
                    {
                        // get property from source
                        object getValue = curPropInfo.GetGetMethod().Invoke(source, new object[] { });
                        // clone if needed
                        if (getValue != null)
                        {
                            DependencyObject dp = getValue as DependencyObject;
                            if (dp != null)
                                getValue = Clone(dp);
                        }

                        // set property on cloned
                        curPropInfo.GetSetMethod().Invoke(cloned, new object[] { getValue });
                    }
                    else // handle indexer
                    {
                        // get count for indexer
                        int numberofItemInColleciton =
                            (int)curPropInfo.ReflectedType.GetProperty("Count").GetGetMethod().Invoke(source, new object[] { });
                        // run on indexer
                        for (int i = 0; i < numberofItemInColleciton; i++)
                        {
                            // get item through Indexer
                            object getValue = curPropInfo.GetGetMethod().Invoke(source, new object[] { i });
                            // clone if needed
                            if (getValue != null)
                            {
                                DependencyObject dp = getValue as DependencyObject;
                                if (dp != null)
                                    getValue = Clone(dp);
                            }

                            // add item to collection
                            curPropInfo.ReflectedType.GetMethod("Add").Invoke(cloned, new object[] { getValue });
                        }
                    }
                }
            }

            return cloned;

        }

        #endregion // Private
    }
}
