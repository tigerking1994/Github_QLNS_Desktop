using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Helper
{
    public static class MessageBoxHelper
    {
        public static void Info(string text, string caption = "Thông báo")
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        public static void Error(string text, string caption = "Thông báo lỗi")
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        public static void Error(Exception ex, string caption = "Thông báo lỗi")
        {
            MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Hand);
        }

        public static void Warning(string text, string caption = "Cảnh báo")
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static MessageBoxResult Confirm(string text, string caption = "Xác nhận") => MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

        public static MessageBoxResult ConfirmCancel(
            string text,
            string caption = "Xác nhận",
            MessageBoxResult defaultResult = MessageBoxResult.Yes)
        {
            return MessageBox.Show(text, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, defaultResult);
        }

        public static void WarningCopyBeforeClose(string text, string caption = "Cảnh báo")
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
