using System;
using System.Collections.Generic;
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
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement
{
    /// <summary>
    /// Interaction logic for RequestSettlementDialog.xaml
    /// </summary>
    public partial class RequestSettlementDialog : UserControl
    {
        private string _defaultStringFormat = "{0:N0}";
        public RequestSettlementDialog()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var isNumeric = double.TryParse(textBox.Text, out double num);
                if (isNumeric)
                {
                    int oldIndex = textBox.CaretIndex;
                    string oldText = textBox.Text;
                    string stringFormat = GetStringFormat(textBox);
                    string newText = string.Format(stringFormat, num);
                    if (!oldText.Equals(newText))
                    {
                        textBox.Text = newText;
                        textBox.Select(StringUtils.ChooseCaretIndex(oldIndex, oldText, newText), 0);
                    }
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
    }
}