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
    // Kpedia
    public class KpediaCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var home = "https://www.kpedia.jp";
            var reg1 = new Regex(@"<li class=""w""><a href=""(.+?)"">(.+?)</a></li>");
            var reg2 = new Regex(@"<td width=""25%"" style=""padding-left:8px;""><a href=""(.+?)"" class=""menu_d"">");
            var reg3 = new Regex(@"\s+<tr>\r\n\s+<td>(.+?)</td>\r\n\s+<td>.+\r\n\s+<td><a href=""(/w/.+?)"">(.+?)&nbsp;</a>");
            var urlSet = new HashSet<string>();
            var client = new HttpClient();
            var lines2 = new List<string>();
            {
                var html = await client.GetStringAsync($"https://www.kpedia.jp/p/379?nCP=1");
                var ms2 = reg2.Matches(html).Cast<Match>().ToList();
                foreach (var m2 in ms2)
                    for (int i = 1; i <= 2; i++)
                    {
                        var html2 = await client.GetStringAsync($"{home}{m2.Groups[1].Value}?nCP={i}");
                        var ms3 = reg3.Matches(html2).Cast<Match>().ToList();
                        foreach (var m3 in ms3)
                        {
                            var url = home + m3.Groups[2].Value;
                            if (urlSet.Contains(url)) continue;
                            urlSet.Add(url);
                            var title = $"{m3.Groups[1].Value}（{m3.Groups[3].Value}）";
                            var s = url + delim + title;
                            lines2.Add(s);
                        }
                    }
            }
            for (int i = 1; i <= 2; i++)
            {
                var html = await client.GetStringAsync($"https://www.kpedia.jp/p/379?nCP={i}");
                var ms = reg1.Matches(html).Cast<Match>().ToList();
                foreach (var m in ms)
                {
                    var url = home + m.Groups[1].Value;
                    if (urlSet.Contains(url)) continue;
                    urlSet.Add(url);
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
                    LANGID = 7,
                    PATTERN = title,
                    TAGS = "Kpedia",
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
