using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers
{
    // 絵でわかる日本語
    public class EdeCrawler : PatternsCrawler
    {
        public override async Task Step1()
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

        public override async Task Step2()
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
    }
}
