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
    // Korean Grammar Dictionary
    public class KGDCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var client = new HttpClient();
            var reg1 = new Regex(@"<span id=""ENTRYNAME"">(.+?)</span> ");
            var lines2 = new List<string>();
            for (int i = 1; i <= 10000; i++)
            {
                var url = $"http://koreangrammaticalforms.com/entry.php?eid={i:D10}";
                var html = await client.GetStringAsync(url);
                var m = reg1.Match(html);
                if (!m.Success) break;
                var title = m.Groups[1].Value.Trim();
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
