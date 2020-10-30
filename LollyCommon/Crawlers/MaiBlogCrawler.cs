using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon.Crawlers
{
    // まいと学ぼう韓国語
    public class MaiBlogCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"<a href=""(http://00mai00.blog110.fc2.com/blog-entry[^""]+?)"">(.+?)</a>");
            var urlSet = new HashSet<string>();
            var client = new HttpClient();
            var lines2 = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string html;
                try
                {
                    html = await client.GetStringAsync($"http://00mai00.blog110.fc2.com/blog-category-19-{i}.html");
                }
                catch (Exception ex)
                {
                    break;
                }
                var ms = reg1.Matches(html).Cast<Match>().ToList();
                foreach (var m in ms)
                {
                    var url = m.Groups[1].Value;
                    if (urlSet.Contains(url)) continue;
                    urlSet.Add(url);
                    var title = HttpUtility.HtmlDecode(m.Groups[2].Value);
                    var s = url + delim + title;
                    lines2.Add(s);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2()
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                string url = a[0], title = a[1];
                var pt = new MPattern
                {
                    LANGID = 7,
                    PATTERN = title,
                    TAGS = "MaiBlog",
                };
                var wp = new MWebPage
                {
                    TITLE = title,
                    URL = url,
                };
                var wpid = await storewp.Create(wp);
                if (wpid == 0) continue;
                var ptid = await storept.Create(pt);
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
