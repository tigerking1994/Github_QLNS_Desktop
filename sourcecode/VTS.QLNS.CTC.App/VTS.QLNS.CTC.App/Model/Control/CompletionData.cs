﻿using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model.Control
{
    public class CompletionData : ICompletionData
    {
		public CompletionData(string text)
		{
			this.Text = text;
		}

		public System.Windows.Media.ImageSource Image
		{
			get { return null; }
		}

		public string Text { get; private set; }

		// Use this property if you want to show a fancy UIElement in the drop down list.
		public object Content
		{
			get { return this.Text; }
		}

		public object Description
		{
			get { return "Description for " + this.Text; }
		}

		public double Priority { get { return 0; } }

		public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
		{
			textArea.Document.Replace(completionSegment, this.Text);
		}
	}
}
