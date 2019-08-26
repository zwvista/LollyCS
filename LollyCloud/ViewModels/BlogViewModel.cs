using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LollyShared
{
    public class BlogViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        NoteViewModel vmNote;
        MDictNote DictNote => vmNote.DictNote;

        public BlogViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            vmNote = new NoteViewModel(this.vmSettings);
        }

        string Html1With(string s) =>
            $"<strong><span style=\"color:#0000ff;\">{s}</span></strong>";
        string HtmlWordWith(string s) => Html1With(s + "：");
        string HtmlBWith(string s) => Html1With(s);
        string HtmlE1With(string s) =>
            $"<span style=\"color:#006600;\">{s}</span>";
        string Html2With(string s) =>
            $"<span style=\"color:#cc00cc;\">{s}</span>";
        string HtmlE2With(string s) => Html2With(s);
        string HtmlIWith(string s) => $"<strong>{Html2With(s)}</strong>";
        readonly string htmlEmptyLine = "<div><br></div>";
        readonly Regex regMarkedEntry = new Regex(@"(\*\*?)\s*(.*?)：(.*?)：(.*)");
        readonly Regex regMarkedB = new Regex("<B>(.+?)</B>");
        readonly Regex regMarkedI = new Regex("<I>(.+?)</I>");
        string MarkedToHtml(string text)
        {
            var lst = text.Split('\n').ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                var s = lst[i];
                var m = regMarkedEntry.Match(s);
                if (m.Success)
                {
                    var s1 = m.Groups[1].Value;
                    var s2 = m.Groups[2].Value;
                    var s3 = m.Groups[3].Value;
                    var s4 = m.Groups[4].Value;
                    s = HtmlWordWith(s2) + (string.IsNullOrEmpty(s3) ? "" : HtmlE1With(s3))
                        + (string.IsNullOrEmpty(s4) ? "" : HtmlE2With(s4));
                    lst[i] = (s1 == "*" ? "<li>" : "<br>") + s;
                    if (i == 0 || lst[i - 1].StartsWith("<div>"))
                        lst.Insert(i++, "<ul>");
                    var isLast = i == lst.Count - 1;
                    var m2 = isLast ? null : regMarkedEntry.Match(lst[i + 1]);
                    if (isLast || m2 == null || !m2.Success || m2.Groups[2].Value != "**")
                        lst[i] += "</li>";
                    if (isLast || m2 == null || !m2.Success)
                        lst.Insert(++i, "</ul>");
                }
                else if (string.IsNullOrEmpty(s))
                    lst[i] = htmlEmptyLine;
                else
                {
                    s = regMarkedB.Replace(s, HtmlBWith("$1"));
                    s = regMarkedI.Replace(s, HtmlIWith("$1"));
                    lst[i] = $"<div>{s}</div>";
                }
            }
            return string.Join("\n", lst);
        }
        readonly Regex regLine = new Regex("<div>(.*?)</div>");
        Regex regHtmlB => new Regex(HtmlBWith("(.+?)"));
        Regex regHtmlI => new Regex(HtmlIWith("(.+?)"));
        Regex regHtmlEntry => new Regex($"(<li>|<br>){HtmlWordWith("(.*?)")}(?:{HtmlE1With("(.*?)")})?(?:{HtmlE2With("(.*?)")})?(?:</li>)?");
        string HtmlToMarked(string text)
        {
            var lst = text.Split('\n').ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                var s = lst[i];
                if (s == "<ul>" || s == "</ul>")
                    lst.RemoveAt(i--);
                else if (s == htmlEmptyLine)
                    lst[i] = "";
                else
                {
                    var m = regLine.Match(s);
                    if (m.Success)
                    {
                        s = m.Groups[1].Value;
                        s = regHtmlB.Replace(s, "<B>$1</B>");
                        s = regHtmlI.Replace(s, "<I>$1</I>");
                        lst[i] = s;
                    }
                    else
                    {
                        m = regHtmlEntry.Match(s);
                        if (m.Success)
                        {
                            var s1 = m.Groups[1].Value;
                            var s2 = m.Groups[2].Value;
                            var s3 = m.Groups[3].Value;
                            var s4 = m.Groups[4].Value;
                            s = (s1 == "<li>" ? "*" : "**") + $" {s2}：{s3}：{s4}";
                            lst[i] = s;
                        }
                    }
                }
            }
            return string.Join("\n", lst);
        }
    }
}
