using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Component
{
    public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
    {
        private string _defaultStringFormat = "{0:N0}";
        private string _previousText;

        public static double GetTotal(DependencyObject obj)
        {
            return (double)obj.GetValue(TotalProperty);
        }

        public static void SetTotal(DependencyObject obj, double value)
        {
            obj.SetValue(TotalProperty, value);
        }

        public static readonly DependencyProperty TotalProperty = DependencyProperty.RegisterAttached("Total",
            typeof(double), typeof(DataGridTextColumn), new UIPropertyMetadata(0.0));

        public static int GetColumnSpan(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnSpanProperty);
        }

        public static void SetColumnSpan(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnSpanProperty, value);
        }

        public static readonly DependencyProperty ColumnSpanProperty = DependencyProperty.RegisterAttached("ColumnSpan",
            typeof(int), typeof(DataGridTextColumn), new UIPropertyMetadata(1));

        public static string GetColumnSpanTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnSpanTitleProperty);
        }

        public static void SetColumnSpanTitle(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnSpanTitleProperty, value);
        }

        public static readonly DependencyProperty ColumnSpanTitleProperty = DependencyProperty.RegisterAttached("ColumnSpanTitle",
            typeof(string), typeof(DataGridTextColumn), new UIPropertyMetadata(string.Empty));

        public double TotalValue => GetTotal(this);

        #region Override method
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var textBox = (TextBox)base.GenerateEditingElement(cell, dataItem);
            textBox.MaxLength = MaxLength;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.PreviewTextInput += TextBox_PreviewTextInput;

            if (GotFocus != null)
            {
                textBox.GotFocus += GotFocus;
            }
            return textBox;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Set the maximum length for the text field.
        /// </summary>
        /// <remarks>Not a dprop, as is only applied once.</remarks>
        public int MaxLength { get; set; }
        public bool IsNumeric { get; set; }
        public string StringFormat { get; set; }
        public event RoutedEventHandler GotFocus;
        #endregion

        #region Handler method
        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9.,\\-]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var isNumeric = double.TryParse(textBox.Text, out double num);
                if (isNumeric)
                {
                    var converter = textBox.GetBindingExpression(TextBox.TextProperty).ParentBinding.Converter;
                    if (converter is CellNumberIntToStringConverter)
                        textBox.MaxLength = 9;
                    else if (converter is CellNumberToStringConverter)
                        textBox.MaxLength = 19;
                    else if (converter is CellNumberDecimalToStringConverter)
                        textBox.MaxLength = 19;

                    int oldIndex = textBox.CaretIndex;
                    string oldText = textBox.Text;
                    string stringFormat = GetStringFormat(textBox);
                    string newText = string.Format(stringFormat, num);
                    if (!oldText.Equals(newText))
                    {
                        textBox.Text = newText;
                        textBox.Select(ChooseCaretIndex(oldIndex, oldText, newText), 0);
                    }
                    _previousText = textBox.Text;
                }
            }
        }

        private string GetStringFormat(TextBox textBox)
        {
            string stringFormat = null;
            BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                var binding = bindingExpression.ParentBinding;
                stringFormat = binding.StringFormat;
            }
            return string.IsNullOrEmpty(stringFormat) ? _defaultStringFormat : stringFormat;
        }
        #endregion

        #region Private method
        private int ChooseCaretIndex(int oldIndex, string oldText, string newText)
        {
            // There is no exact algorithm for this.  Instead we use some heuristics.
            //   First handle some frequent special cases

            // oldText appears within newText, translate the index
            int index = newText.IndexOf(oldText, StringComparison.Ordinal);
            if (oldText.Length > 0 && index >= 0)
                return index + oldIndex;

            // caret was at one edge of oldText, return corresponding edge
            if (oldIndex == 0)
                return 0;
            if (oldIndex == oldText.Length)
                return newText.Length;

            // newText differs from oldText by a small replacement
            // (this is common when doing conversions to numeric types - adding
            // leading or trailing zeros, decimal separators, thousand separators,
            // etc.).
            // The two strings share a common prefix and suffix - find those
            int prefix, suffix;
            for (prefix = 0;
                    prefix < oldText.Length && prefix < newText.Length;
                    ++prefix)
            {
                if (oldText[prefix] != newText[prefix])
                    break;
            }
            for (suffix = 0;
                    suffix < oldText.Length && suffix < newText.Length;
                    ++suffix)
            {
                if (oldText[oldText.Length - 1 - suffix] != newText[newText.Length - 1 - suffix])
                    break;
            }
            // if the prefix and suffix account for enough of the text, treat the
            // rest as a small replacement
            if (2 * (prefix + suffix) >= Math.Min(oldText.Length, newText.Length))
            {
                // if the caret was in or next to the prefix or suffix, return the
                // corresponding position in newText
                if (oldIndex <= prefix)
                    return oldIndex;
                if (oldIndex >= oldText.Length - suffix)
                    return newText.Length - (oldText.Length - oldIndex);
            }

            // we're left with the hard case - newText is substantially different
            // from oldText.  Look for the longest matching substring that includes
            // the character just before the (old) caret - this is what the user
            // just typed, so it should participate in the match.
            char anchor = oldText[oldIndex - 1];
            int anchorIndex = newText.IndexOf(anchor);
            int bestIndex = -1;
            int bestLength = 1;     // match at least 2 chars

            while (anchorIndex >= 0)
            {
                int matchLength = 1;

                // match backward from the anchor position
                for (index = anchorIndex - 1;
                        index >= 0 && oldIndex - (anchorIndex - index) >= 0;
                        --index)
                {
                    if (newText[index] != oldText[oldIndex - (anchorIndex - index)])
                        break;
                    ++matchLength;
                }

                // match forward from the anchor position
                for (index = anchorIndex + 1;
                        index < newText.Length && oldIndex + (index - anchorIndex) < oldText.Length;
                        ++index)
                {
                    if (newText[index] != oldText[oldIndex + (index - anchorIndex)])
                        break;
                    ++matchLength;
                }

                // remember the best match
                if (matchLength > bestLength)
                {
                    bestIndex = anchorIndex + 1;
                    bestLength = matchLength;
                }

                // advance to the next occurrence of the anchor character
                anchorIndex = newText.IndexOf(anchor, anchorIndex + 1);
            }

            // return the index of the best match.  If none found, put the cursor at the end
            return (bestIndex < 0) ? newText.Length : bestIndex;
        }
        #endregion
    }
}
