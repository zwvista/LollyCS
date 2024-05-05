using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon.Crawlers.Patterns.Korean
{
    // まいと学ぼう韓国語
    public class MaiBlogCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"<a href=""(http://00mai00.blog110.fc2.com/blog-entry[^""]+?)"">(.+?)</a>");
            var urlSet = new HashSet<string>();
            var lines2 = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string html;
                try
                {
                    html = await client.GetStringAsync($"http://00mai00.blog110.fc2.com/blog-category-19-{i}.html");
                }
                catch (Exception)
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

        public override async Task Step2() =>
            await Step2(7, "MaiBlog");
    }
}
