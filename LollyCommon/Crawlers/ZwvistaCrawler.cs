using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers
{
    // ZwvistaWordPress
    public class ZwvistaCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"<h2 class=""post-title"">\n\s+<a href=""(https://zwvista.wordpress.com/.+?)"" rel=""bookmark"">【日语句型】(.+?)</a>\n\s+</h2>");
            var client = new HttpClient();
            var lines2 = new List<string>();
            for (int i = 1; i < 100; i++)
            {
                string html;
                try
                {
                    html = await client.GetStringAsync($"https://zwvista.wordpress.com/category/%E6%97%A5%E8%AF%AD%E5%8F%A5%E5%9E%8B/page/{i}/");
                }
                catch (Exception ex)
                {
                    break;
                }
                var ms = reg1.Matches(html).Cast<Match>().ToList();
                foreach (var m in ms)
                {
                    var url = m.Groups[1].Value;
                    var title = m.Groups[2].Value;
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
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "zwvista",
                };
                var wp = new MWebPage
                {
                    TITLE = "【日语句型】" + title,
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
