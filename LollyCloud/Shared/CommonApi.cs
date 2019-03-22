using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Runtime.InteropServices;

namespace LollyShared
{
    public enum DictWebBrowserStatus
    {
        Ready, Navigating, Automating
    }
    public static class CommonApi
    {
        public static string LollyUrl = "https://zwvista.tk/lolly/api.php/";
        public static string CssFolder = "https://zwvista.tk/lolly/css/";
        public static int UserId = 1;
        static readonly Dictionary<string, string> escapes = new Dictionary<string, string>()
        {
            {"<delete>", ""}, {@"\t", "\t"}, {@"\r", "\r"}, {@"\n", "\n"},
        };
        public static string ExtractTextFromHtml(string html, string transfrom, string template, Func<string, string, string> templateHandler)
        {
#if DEBUG_EXTRACT
            var logFolder = Settings.Default.LogFolder + "\\";
            File.WriteAllText(logFolder + "0_raw.html", html);
            transfrom = File.ReadAllText(logFolder + "1_transform.txt");
            template = File.ReadAllText(logFolder + "5_template.txt");
#endif
            var text = "";
            do
            {
                if (string.IsNullOrEmpty(transfrom)) break;
                var arr = transfrom.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var reg = new Regex(arr[0]);
                var match = reg.Match(html);
                if (!match.Success) break;

                text = match.Groups[0].Value;
                void f(string replacer)
                {
                    foreach (var entry in escapes)
                        replacer = replacer.Replace(entry.Key, entry.Value);
                    text = reg.Replace(text, replacer);
                };

                f(arr[1]);
#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "2_extracted.txt", text);
#endif
                for (int i = 2; i < arr.Length;)
                {
                    reg = new Regex(arr[i++]);
                    f(arr[i++]);
                }
#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "4_cooked.txt", text);
#endif
                if (string.IsNullOrEmpty(template)) break;
                text = templateHandler(text, template);

            } while (false);

#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "6_result.html", text);
#endif
            return text;
        }
    }

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

    // https://stackoverflow.com/questions/930433/apply-properties-values-from-one-object-to-another-of-the-same-type-automaticall
    public static class Reflection
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = destination.GetType();
            Type typeSrc = source.GetType();
            // Collect all the valid properties to map
            var results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new { sourceProperty = srcProp, targetProperty = targetProperty };
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
        }
    }
}
