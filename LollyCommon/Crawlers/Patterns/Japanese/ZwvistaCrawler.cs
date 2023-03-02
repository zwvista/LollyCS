using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon
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

        public override async Task Step2() =>
            await Step2(a =>
            {
                string url = a[0], title = a[1];
                return new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "zwvista",
                    TITLE = "【日语句型】" + title,
                    URL = url,
                };
            });
    }
}
