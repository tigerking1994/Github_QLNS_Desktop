using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace VTS.QLNS.CTC.Custom
{
    [TemplatePart(Name = TextBoxPartName, Type = typeof(TextBox))]
    public class NumericTextBox : Control
    {
        public const string TextBoxPartName = "PART_TextBox";

        private TextBox _textBox;

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(NumericTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TextPropertyChangedCallback));

        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var customControl = (NumericTextBox)dependencyObject;
            if (customControl._textBox != null)
            {
                customControl.UpdateTextBoxText(dependencyPropertyChangedEventArgs.NewValue as string ?? "");
            }
        }

        public static readonly DependencyProperty StringFormatProperty = DependencyProperty.Register(
            nameof(StringFormat), typeof(string), typeof(NumericTextBox), new FrameworkPropertyMetadata("{0:N0}", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        static NumericTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericTextBox), new FrameworkPropertyMetadata(typeof(NumericTextBox)));
            EventManager.RegisterClassHandler(typeof(NumericTextBox), UIElement.GotFocusEvent, new RoutedEventHandler(OnGotFocus));
        }

        public decimal ActualValue { get; set; }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string StringFormat
        {
            get => (string)GetValue(StringFormatProperty);
            set => SetValue(StringFormatProperty, value);
        }

        public override void OnApplyTemplate()
        {
            if (_textBox != null)
            {
                _textBox.RemoveHandler(KeyDownEvent, new KeyEventHandler(TextBoxOnKeyDown));
                _textBox.RemoveHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(TextBoxOnTextChanged));
                _textBox.RemoveHandler(LostFocusEvent, new RoutedEventHandler(TextBoxOnLostFocus));
            }

            _textBox = GetTemplateChild(TextBoxPartName) as TextBox;
            if (_textBox != null)
            {
                Binding binding = new Binding();
                binding.Path = new PropertyPath(nameof(ActualValue));
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Control), 1);
                binding.StringFormat = StringFormat;
                _textBox.SetBinding(TextBox.TextProperty, binding);

                _textBox.AddHandler(KeyDownEvent, new KeyEventHandler(TextBoxOnKeyDown));
                _textBox.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(TextBoxOnTextChanged));
                _textBox.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBoxOnLostFocus));
                _textBox.Text = Text;
            }

            base.OnApplyTemplate();
        }

        private static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            NumericTextBox picker = (NumericTextBox)sender;
            if ((!e.Handled) && (picker._textBox != null))
            {
                if (e.OriginalSource == picker)
                {
                    picker._textBox.Focus();
                    e.Handled = true;
                }
                else if (e.OriginalSource == picker._textBox)
                {
                    picker._textBox.SelectAll();
                    e.Handled = true;
                }
            }
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = ProcessKey(e) || e.Handled;
        }

        private bool ProcessKey(KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.Enter:
                    {
                        SetTextValue();
                        return true;
                    }
            }
            return false;
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            SetTextValue();
        }

        private void UpdateTextBoxText(string text)
        {
            // Save and restore the cursor position
            if (_textBox is TextBox textBox)
            {
                var caretIndex = textBox.CaretIndex;
                textBox.Text = text;
                textBox.CaretIndex = caretIndex;
            }
        }

        private void SetTextValue()
        {
            if (_textBox != null)
            {
                SetCurrentValue(TextProperty, _textBox.Text);
            }
        }
    }
}
