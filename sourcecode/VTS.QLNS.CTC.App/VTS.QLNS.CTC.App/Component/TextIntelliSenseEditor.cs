using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Component
{
    public class TextIntelliSenseEditor : TextEditor
    {
        private CompletionWindow _completionWindow;
        private int _completionStartOffset;
        private bool _textIsUpdating;

        public static readonly DependencyProperty SuggestionWordsProperty = DependencyProperty.Register("SuggestionWords",
            typeof(IEnumerable<string>), typeof(TextIntelliSenseEditor), new FrameworkPropertyMetadata(null)
        );

        public IEnumerable<string> SuggestionWords
        {
            get { return (IEnumerable<string>)GetValue(SuggestionWordsProperty); }
            set { SetValue(SuggestionWordsProperty, value); }
        }

        public TextIntelliSenseEditor()
        {
            this.PreviewKeyDown += new KeyEventHandler(this.OnPreviewKeyDown);
            this.TextArea.TextEntering += new TextCompositionEventHandler(this.OnTextEntering);
            this.TextArea.TextEntered += new TextCompositionEventHandler(this.OnTextEntered);
            this.TextArea.Caret.PositionChanged += new EventHandler(this.OnCaretPositionChanged);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs eventArgs)
        {
            if (eventArgs.Key != Key.Space || Keyboard.Modifiers != ModifierKeys.Control)
                return;
            this.RequestCompletionWindow(char.MinValue, true);
            eventArgs.Handled = true;
        }

        private void OnTextEntering(object sender, TextCompositionEventArgs eventArgs)
        {
            this._textIsUpdating = true;
            string text = eventArgs.Text;
            if (string.IsNullOrEmpty(text) || text.Length != 1)
                return;
            this.RequestCompletionWindow(text[0], false);
        }

        private void OnTextEntered(object sender, TextCompositionEventArgs eventArgs) => this._textIsUpdating = false;

        private void OnCaretPositionChanged(object sender, EventArgs eventArgs)
        {
            if (this._textIsUpdating)
                return;
            if (this._completionWindow != null && this.CaretOffset < this._completionStartOffset)
                this._completionWindow.Close();
            if (this._completionWindow == null)
                return;
            this.RequestCompletionWindow(char.MinValue, false);
        }

        private void RequestCompletionWindow(char key, bool explicitRequest)
        {
            TextDocument document = this.Document;
            int caretOffset = this.CaretOffset;
            bool flag = this.IsIdentifierCharacter(key);
            if (key != char.MinValue && !flag)
            {
                this._completionWindow?.Close();
            }
            else
            {
                int offset1 = this.FindStartOfIdentifier(document, caretOffset - 1);
                string str1;
                if (offset1 >= 0)
                {
                    str1 = document.GetText(offset1, caretOffset - offset1);
                }
                else
                {
                    str1 = string.Empty;
                    offset1 = caretOffset;
                }
                int offset2 = offset1 - 1;
                char ch = offset2 >= 0 ? document.GetCharAt(offset2) : char.MinValue;
                if (key == char.MinValue && ch == ' ' && !explicitRequest && this._completionWindow == null)
                    return;
                string str2 = !string.IsNullOrEmpty(str1) ? (key <= char.MinValue ? str1 : str1 + key.ToString()) : (key != char.MinValue ? key.ToString() : string.Empty);
                CompletionData[] completionData1 = this.GetCompletionData(this.Text, str2);
                if (completionData1 == null || completionData1.Length == 0)
                {
                    this._completionWindow?.Close();
                }
                else
                {
                    IOrderedEnumerable<CompletionData> orderedEnumerable = ((IEnumerable<CompletionData>)completionData1).OrderBy(d => d.Text);
                    if (this._completionWindow == null)
                    {
                        CompletionWindow completionWindow = new CompletionWindow(this.TextArea);
                        completionWindow.MaxHeight = 300.0;
                        this._completionWindow = completionWindow;
                        this._completionWindow.Closed += (_param1, _param2) => this._completionWindow = null;
                    }
                    this._completionWindow.CompletionList.CompletionData.Clear();
                    string str3 = null;
                    foreach (CompletionData completionData2 in orderedEnumerable)
                    {
                        if (completionData2.Text != str2 && completionData2.Text != str3)
                            this._completionWindow.CompletionList.CompletionData.Add(completionData2);
                        str3 = completionData2.Text;
                    }
                    if (this._completionWindow.CompletionList.CompletionData.Count == 0)
                    {
                        this._completionWindow?.Close();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(str2))
                            this._completionWindow.CompletionList.SelectItem(str2);
                        if (str1 != null)
                        {
                            this._completionStartOffset = this.CaretOffset - str1.Length;
                            this._completionWindow.StartOffset = this._completionStartOffset;
                        }
                        else
                            this._completionStartOffset = this.CaretOffset;
                        this._completionWindow.Show();
                        this._completionWindow.InvalidateMeasure();
                    }
                }
            }
        }

        private CompletionData[] GetCompletionData(string text, string selectionText)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            int length = text.Length;
            StringBuilder stringBuilder = new StringBuilder();
            List<string> stringList = new List<string>();
            for (int index = 0; index < length; ++index)
            {
                char c = text[index];
                if (this.IsIdentifierCharacter(c))
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    if (stringBuilder.Length > 3)
                    {
                        string str = stringBuilder.ToString();
                        if (str.StartsWith(selectionText, StringComparison.Ordinal))
                            stringList.Add(str);
                    }
                    stringBuilder.Clear();
                }
            }
            if (SuggestionWords != null)
            {
                stringList.AddRange(SuggestionWords.ToList());
            }
            return stringList.Distinct().Where(x => x.StartsWith(selectionText)).Select(word => new CompletionData(word)).ToArray();
        }

        private bool IsIdentifierCharacter(char c) => TextUtilities.GetCharacterClass(c) == CharacterClass.IdentifierPart;

        private int FindStartOfIdentifier(ITextSource document, int offset)
        {
            if (offset < 0 || document.TextLength <= offset || !this.IsIdentifierCharacter(document.GetCharAt(offset)))
                return -1;
            while (0 < offset && this.IsIdentifierCharacter(document.GetCharAt(offset - 1)))
                --offset;
            return offset;
        }
    }
}
