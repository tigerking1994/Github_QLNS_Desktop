using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace VTS.QLNS.CTC.App.Helper
{
    public class ApplicationHelper
    {
        public static Window GetCurrentMainWindow()
        {
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i] is MainWindow)
                {
                    return Application.Current.Windows[i];
                }
            }
            return null;
        }

        public static void CloseAllButThis(Type curentType)
        {
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i].GetType() != curentType)
                {
                    Application.Current.Windows[i].Close();
                }
            }
        }
    }
}
