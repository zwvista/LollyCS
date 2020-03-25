using Dragablz;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LollyCloud
{
    // https://stackoverflow.com/questions/6138199/wpf-webbrowser-control-how-to-suppress-script-errors
    public static class WebBrowserExtensions
    {
        public static void SetSilent(this WebBrowser browser, bool silent)
        {
            dynamic activeX = browser.GetType().InvokeMember("ActiveXInstance",
                                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, browser, new object[] { });
            activeX.Silent = silent;
        }
    }
    // https://stackoverflow.com/questions/4734482/button1-performclick-in-wpf
    public static class ButtonExtensions
    {
        public static void PerformClick(this ButtonBase btn)
        {
            btn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
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

}
