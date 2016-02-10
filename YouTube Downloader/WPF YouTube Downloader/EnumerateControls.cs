using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_YouTube_Downloader
{
    class EnumerateControls
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static void LockGUI<T>(T obj) where T : Page
        {
            obj.Cursor = Cursors.Wait;
            foreach (var c in EnumerateControls.FindVisualChildren<Control>(obj))
            {
                c.IsEnabled = false;
            }
        }

        public static void UnlockGui<T>(T obj) where T : Page
        {
            obj.Cursor = Cursors.Arrow;
            foreach (var c in EnumerateControls.FindVisualChildren<Control>(obj))
            {
                c.IsEnabled = true;
            }
        }
    }
}
