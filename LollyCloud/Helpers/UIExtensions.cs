using CefSharp.Wpf;
using Dragablz;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LollyCloud
{
    public static class ControlExtensions
    {
        // https://stackoverflow.com/questions/4734482/button1-performclick-in-wpf
        public static void PerformClick(this ButtonBase btn)
        {
            btn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        // https://github.com/cefsharp/CefSharp/issues/2788
        public static void LoadLargeHtml(this ChromiumWebBrowser wb, string html)
        {
            var base64EncodedHtml = Convert.ToBase64String(Encoding.UTF8.GetBytes(html));
            wb.Load("data:text/html;base64," + base64EncodedHtml);
        }
    }
    public static class UIHelper
    {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the queried item.</param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null reference is being returned.</returns>
        public static T FindVisualParent<T>(DependencyObject child)
          where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                // use recursion to proceed with next level
                return FindVisualParent<T>(parentObject);
            }
        }
    }
    // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
    internal static class WindowExtensions
    {
        // from winuser.h
        private const int GWL_STYLE = -16,
                          WS_MAXIMIZEBOX = 0x10000,
                          WS_MINIMIZEBOX = 0x20000;

        [DllImport("user32.dll")]
        extern private static int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        extern private static int SetWindowLong(IntPtr hwnd, int index, int value);

        internal static void HideMinimizeAndMaximizeButtons(this Window window)
        {
            IntPtr hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
            var currentStyle = GetWindowLong(hwnd, GWL_STYLE);

            SetWindowLong(hwnd, GWL_STYLE, (currentStyle & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX));
        }
    }
    // https://stackoverflow.com/questions/43528152/how-to-close-tab-with-a-close-button-in-wpf
    // This class will be the Tab in the TabControl
    public class ActionTabItem
    {
        // This will be the text in the tab control
        public string Header { get; set; }
        // This will be the content of the tab control It is a UserControl whits you need to create manually
        public UserControl Content { get; set; }
    }
    // https://github.com/ButchersBoy/Dragablz/issues/13
    public class ActionInterTabClient : DefaultInterTabClient
    {
        public override TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window) =>
            TabEmptiedResponse.DoNothing;
    }
    // https://stackoverflow.com/questions/1759372/where-is-button-dialogresult-in-wpf
    public class ButtonHelper
    {
        // Boilerplate code to register attached property "bool? DialogResult"
        public static bool? GetDialogResult(DependencyObject obj) { return (bool?)obj.GetValue(DialogResultProperty); }
        public static void SetDialogResult(DependencyObject obj, bool? value) { obj.SetValue(DialogResultProperty, value); }
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(ButtonHelper), new UIPropertyMetadata
        {
            PropertyChangedCallback = (obj, e) =>
            {
                // Implementation of DialogResult functionality
                Button button = obj as Button;
                if (button == null)
                    throw new InvalidOperationException(
                      "Can only use ButtonHelper.DialogResult on a Button control");
                button.Click += (sender, e2) =>
                {
                    Window.GetWindow(button).DialogResult = GetDialogResult(button);
                };
            }
        });
    }
    // https://stackoverflow.com/questions/2872916/wpf-bind-datagrid-to-liststring
    public class StringWrapper
    {
        public string Value { get; set; }
    }
}
