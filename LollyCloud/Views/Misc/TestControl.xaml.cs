using Hardcodet.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace LollyCloud
{
    /// <summary>
    /// TestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TestControl : UserControl
    {
        public TestControl()
        {
            InitializeComponent();
        }

        async void btnTest_Click(object sender, RoutedEventArgs e)
        {
        }

        void TestFor毎日のんびり日本語教師1()
        {
            var reg1 = new Regex(@"font -weight:800; padding-left:10px;"">(.+?)</p>");
            var reg2 = new Regex(@"<a href=""(https://nihongonosensei.net/\?p=.+?)"">(.+?)</a>");
            // source code of https://nihongonosensei.net/?page_id=10246#linkn3 (日本語の文法)
            var lines = File.ReadAllLines("a.txt");
            var lines2 = new List<string>();
            var ptext = "";
            var delim = "@@@@";
            foreach (var s in lines)
            {
                var m = reg1.Match(s);
                if (m.Success)
                {
                    ptext = m.Groups[1].Value;
                    if (ptext == "Ｎ１　似ている文法の比較")
                        ptext = "Ｎ１文法の比較";
                    continue;
                }
                m = reg2.Match(s);
                if (m.Success)
                {
                    var s1 = m.Groups[1].Value;
                    var s2 = m.Groups[2].Value;
                    var s3 = ptext + delim + s1 + delim + s2;
                    lines2.Add(s3);
                    continue;
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        async void TestFor毎日のんびり日本語教師2()
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storewppt = new PatternWebPageDataStore();
            foreach (var s in lines)
            {
                var a = s.Split(new[] { "@@@@" }, StringSplitOptions.RemoveEmptyEntries);
                string tag = a[0], url = a[1], title = a[2];
                var pt = new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "毎日" + tag,
                };
                var wp = new MWebPage
                {
                    TITLE = $"【{tag}】{title}",
                    URL = url,
                };
                var ptid = await storept.Create(pt);
                var wpid = await storewp.Create(wp);
                var ptwp = new MPatternWebPage
                {
                    PATTERNID = ptid,
                    WEBPAGEID = wpid,
                    SEQNUM = 1,
                };
                await storewppt.Create(ptwp);
            }
        }
        void TestFor絵でわかる日本語1()
        {
            var reg1 = new Regex(@"href=""(http://www.edewakaru.com/archives/.+?\.html)"".*?>(.*?)<");
            // source code of http://www.edewakaru.com/archives/cat_179055.html (日本語文法リスト（あいうえお順）)
            var text = File.ReadAllLines("a.txt").MaxBy(s => s.Length).Single();
            var delim = "@@@@";
            var ms = reg1.Matches(text);
            var lines2 = new List<string>();
            foreach (Match m in ms)
            {
                var url = m.Groups[1].Value;
                var title = m.Groups[2].Value.Replace("〜", "～").Replace("・", "／").Trim();
                if (title.IsEmpty()) continue;
                var s = url + delim + title;
                lines2.Add(s);
            }
            File.WriteAllLines("b.txt", lines2);
        }
        async void TestFor絵でわかる日本語2()
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storewppt = new PatternWebPageDataStore();
            var tag = "絵で";
            foreach (var s in lines)
            {
                var a = s.Split(new[] { "@@@@" }, StringSplitOptions.RemoveEmptyEntries);
                string url = a[0], title = a[1];
                var pt = new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = tag,
                };
                var wp = new MWebPage
                {
                    TITLE = title,
                    URL = url,
                };
                var ptid = await storept.Create(pt);
                var wpid = await storewp.Create(wp);
                var ptwp = new MPatternWebPage
                {
                    PATTERNID = ptid,
                    WEBPAGEID = wpid,
                    SEQNUM = 1,
                };
                await storewppt.Create(ptwp);
            }
        }

    }
}
