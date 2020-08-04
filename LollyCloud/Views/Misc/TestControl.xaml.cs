using Hardcodet.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
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

        const string delim = "@@@@";
        HttpClient client = new HttpClient();

        async void btnTest_Click(object sender, RoutedEventArgs e)
        {
        }

        async void TestCombinePatterns()
        {
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();
            var lst = await storept.GetDataByLang(2);
            var dic = lst.GroupBy(o => o.PATTERN).Where(g => g.Count() > 1).ToDictionary(g => g.Key, g => g.OrderBy(o => o.ID).ToList());
            foreach (var kv in dic)
            {
                var k = kv.Key;
                var lst2 = kv.Value;
                var lst3 = (await lst2.ToObservable().SelectMany(o => storeptwp.GetDataByPattern(o.ID).ToObservable()).ToList().ToTask()).SelectMany(o => o).ToList();
                var o1 = lst2[0];
                var ptid = o1.ID;
                {
                    o1.TAGS = string.Join(",", lst2.SelectMany(o => o.TAGS.Split(',')).OrderBy(s => s));
                    await storept.Update(o1);
                }
                lst2.Where(o => o.ID != ptid).ForEach(async o =>
                {
                    await storept.Delete(o.ID);
                });
                lst3.ForEach(async (o, i) =>
                {
                    o.PATTERNID = ptid;
                    o.SEQNUM = i + 1;
                    await storeptwp.Update(o);
                });
            }
        }
        void TestFor毎日のんびり日本語教師1()
        {
            var reg1 = new Regex(@"font -weight:800; padding-left:10px;"">(.+?)</p>");
            var reg2 = new Regex(@"<a href=""(https://nihongonosensei.net/\?p=.+?)"">(.+?)</a>");
            // source code of https://nihongonosensei.net/?page_id=10246#linkn3 (日本語の文法)
            var lines = File.ReadAllLines("a.txt");
            var lines2 = new List<string>();
            var ptext = "";
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
            var storeptwp = new PatternWebPageDataStore();
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
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
                await storeptwp.Create(ptwp);
            }
        }
        void TestFor絵でわかる日本語1()
        {
            var reg1 = new Regex(@"href=""(http://www.edewakaru.com/archives/.+?\.html)"".*?>(.*?)<");
            // source code of http://www.edewakaru.com/archives/cat_179055.html (日本語文法リスト（あいうえお順）)
            var text = File.ReadAllLines("a.txt").MaxBy(s => s.Length).Single();
            var ms = reg1.Matches(text);
            var lines2 = new List<string>();
            var dic = new Dictionary<string, string> { { "〜", "～" }, { "１", "1" }, { "２", "2" }, { "３", "3" }, { "４", "4" }, { "５", "5" } };
            foreach (Match m in ms)
            {
                var url = m.Groups[1].Value;
                var title = m.Groups[2].Value.Replace(dic);
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
            var storeptwp = new PatternWebPageDataStore();
            var tag = "絵で";
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
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
                await storeptwp.Create(ptwp);
            }
        }
        async void TestFor日本語教師NET1()
        {
            var client = new HttpClient();
            var html = await client.GetStringAsync("https://nihongokyoshi-net.com/category/jlpt/");
            var reg1 = new Regex(@"<a class=""page-numbers"" href=""https://nihongokyoshi-net.com/category/jlpt/page/(\d+)/"">\d+</a> <a class=""next page-numbers""");
            var m = reg1.Match(html);
            if (!m.Success) return;
            var pages = int.Parse(m.Groups[1].Value);
            var reg2 = new Regex(@"<a href=""(https://nihongokyoshi-net.com/[^""]+?)"" title=""(【JLPT[^】]+?】文法・例文：[^""]+?)"" rel=""bookmark"">.+?</a>");
            var lines2 = new List<string>();
            var dic = new Dictionary<string, string> { { "〜", "～" }, { "/", "／" }, { " ", "" }, { "１", "1" }, { "２", "2" }, { "３", "3" }, { "４", "4" }, { "５", "5" }, { "に出ない？", "0" } };
            for (int i = 1; i <= pages; i++)
            {
                html = await client.GetStringAsync($"https://nihongokyoshi-net.com/category/jlpt/page/{i}/");
                var ms = reg2.Matches(html);
                foreach (Match m2 in ms)
                {
                    var url = m2.Groups[1].Value;
                    var title = m2.Groups[2].Value.Replace(dic).Trim();
                    var s = url + delim + title;
                    lines2.Add(s);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }
        async void TestFor日本語教師NET2()
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();
            var reg1 = new Regex(@"【JLPT(N\d)】文法・例文：(.+)");
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                string url = a[0], title = a[1];
                var m = reg1.Match(title);
                string tag = m.Groups[1].Value, title2 = m.Groups[2].Value;
                var pt = new MPattern
                {
                    LANGID = 2,
                    PATTERN = title2,
                    TAGS = "教師" + tag,
                };
                var wp = new MWebPage
                {
                    TITLE = $"【{tag}】{title2}",
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
                await storeptwp.Create(ptwp);
            }
        }

    }
}
