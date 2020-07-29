using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LollyCloud
{
    public class HtmlTransformService
    {
        static readonly Dictionary<string, string> escapes = new Dictionary<string, string>()
        {
            {"<delete>", ""}, {@"\t", "\t"}, {@"\n", "\n"},
        };

        public static string RemoveReturns(string html) => html.Replace("\r\n", "\n");

        public static List<MTransformItem> ToTransformItems(string transform)
        {
            var arr = transform.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var lst = arr.Take(arr.Length / 2 * 2).Buffer(2).Select((g, i) => new MTransformItem { Index = i + 1, Extractor = g[0], Replacement = g[1] }).ToList();
            return lst;
        }

        public static string DoTransform(string text, MTransformItem item)
        {
            var reg = new Regex(item.Extractor);
            var replacement = item.Replacement;
            var s = text;
            if (replacement.StartsWith("<extract>"))
            {
                replacement = replacement.Substring("<extract>".Length);
                s = string.Join("", reg.Matches(s).Cast<Match>().Select(m => m.Groups[0]));
            }
            foreach (var entry in escapes)
                replacement = replacement.Replace(entry.Key, entry.Value);
            s = reg.Replace(s, replacement);
            return s;
        }

        public static string ExtractTextFromHtml(string html, string transform, string template, Func<string, string, string> templateHandler)
        {
            var text = RemoveReturns(html);
            do
            {
                if (string.IsNullOrEmpty(transform)) break;
                var items = ToTransformItems(transform);
                foreach (var item in items)
                    text = DoTransform(text, item);
                if (string.IsNullOrEmpty(template)) break;
                text = templateHandler(text, template);
            } while (false);
            return text;
        }
    }
}
