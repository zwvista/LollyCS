using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCloud
{
    class BlogService
    {
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
        public string MarkedToHtml(string text)
        {
            var lst = text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
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
                    if (isLast || m2 == null || !m2.Success || m2.Groups[1].Value != "**")
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
            return string.Join("\r\n", lst);
        }
        readonly Regex regLine = new Regex("<div>(.*?)</div>");
        Regex regHtmlB => new Regex(HtmlBWith("(.+?)"));
        Regex regHtmlI => new Regex(HtmlIWith("(.+?)"));
        Regex regHtmlEntry => new Regex($"(<li>|<br>){HtmlWordWith("(.*?)")}(?:{HtmlE1With("(.*?)")})?(?:{HtmlE2With("(.*?)")})?(?:</li>)?");
        public string HtmlToMarked(string text)
        {
            var lst = text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
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
            return string.Join("\r\n", lst);
        }
        public string AddTagB(string text) => $"<B>{text}</B>";
        public string AddTagI(string text) => $"<I>{text}</I>";
        public string RemoveTagBI(string text) => new Regex("</?[BI]>").Replace(text, "");
        public string ExchangeTagBI(string text)
        {
            text = new Regex("<(/)?B>").Replace(text, "<$1Temp>");
            text = new Regex("<(/)?I>").Replace(text, "<$1B>");
            text = new Regex("<(/)?Temp>").Replace(text, "<$1I>");
            return text;
        }
        public string GetExplanation(string text) => $"* {text}：：\r\n";
        public string GetHtml(string text) =>
            $@"<!doctype html>
<html>
<head>
  <meta charset=""utf-8"">
  <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">
</head>
<body>
{text}
</body>
</html>";
        public string GetPatternUrl(string patternNo) => $"http://viethuong.web.fc2.com/MONDAI/{patternNo}.html";
        public string GetPatternMarkDown(string patternText) => $"* [{patternText}　文法](https://www.google.com/search?q={patternText}　文法)\n* [{patternText}　句型](https://www.google.com/search?q={patternText}　句型)";
        readonly string bigDigits = "０１２３４５６７８９";
        public async Task<string> AddNotes(NoteViewModel vmNote, string text)
        {
            string F(string s)
            {
                for (int i = 0; i < 10; i++)
                    s = s.Replace((char)(i + '0'), bigDigits[i]);
                return s;
            }
            var items = text.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            await vmNote.GetNotes(items.Count, i =>
            {
                var m = regMarkedEntry.Match(items[i]);
                if (!m.Success) return false;
                var word = m.Groups[2].Value;
                return word.All(ch => ch != '（' && !bigDigits.Contains(ch));
            },
            async i =>
            {
                var m = regMarkedEntry.Match(items[i]);
                var (s1, word, s3, s4) = (m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value);
                var note = await vmNote.GetNote(word);
                int j = note.ToList().FindIndex(char.IsDigit);
                var s21 = j == -1 ? note : note.Substring(0, j);
                var s22 = j == -1 ? "" : F(note.Substring(j));
                var s2 = word + (s21 == word || string.IsNullOrEmpty(s21) ? "" : $"（{s21}）") + s22;
                items[i] = $"{s1} {s2}：{s3}：{s4}";
            });
            var result = string.Join("\r\n", items);
            return result;
        }
    }
}
