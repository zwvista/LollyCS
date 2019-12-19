using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using LollyShared;

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

    // https://stackoverflow.com/questions/47871745/wpf-change-datagrid-cell-background-color-using-a-converter
    public class LevelToBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var vmSettings = values[0] as SettingsViewModel;
            var level = values[1] as int? ?? 0;
            if (level == 0) return Binding.DoNothing;
            var color = (Color)ColorConverter.ConvertFromString("#" + vmSettings.USLEVELCOLORS[level][0]);
            return new SolidColorBrush(color);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LevelToForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var vmSettings = values[0] as SettingsViewModel;
            var level = values[1] as int? ?? 0;
            if (level == 0) return Binding.DoNothing;
            var color = (Color)ColorConverter.ConvertFromString("#" + vmSettings.USLEVELCOLORS[level][1]);
            return new SolidColorBrush(color);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    // https://stackoverflow.com/questions/20707160/data-binding-int-property-to-enum-in-wpf
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            int returnValue = 0;
            if (parameter is Type)
            {
                returnValue = (int)Enum.Parse((Type)parameter, value.ToString());
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            Enum enumValue = default(Enum);
            if (parameter is Type)
            {
                enumValue = (Enum)Enum.Parse((Type)parameter, value.ToString());
            }
            return enumValue;
        }
    }
}
