using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace LollyShared
{
    public static class CommonApi
    {
        public static string LollyUrl = "https://zwvista.tk/lolly/api.php/";
        public static string CssFolder = "https://zwvista.tk/lolly/css/";
        public static int UserId = 1;
        private static readonly Dictionary<string, string> escapes = new Dictionary<string, string>()
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
                var arr = transfrom.Split(new[] { "\r\n" }, StringSplitOptions.None);
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
            } while (false);

#if DEBUG_EXTRACT
            File.WriteAllText(logFolder + "6_result.html", text);
#endif
            return text;
        }
    }
}
