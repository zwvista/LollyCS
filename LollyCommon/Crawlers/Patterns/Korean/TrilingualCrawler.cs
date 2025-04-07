using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers.Patterns.Korean
{
    // トリランガルの韓国語講座
    public class TrilingualCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var home = "https://trilingual.jp/";
            var html = await client.GetStringAsync(home);
            var reg1 = new Regex(@"<div class=""pt-cv-title""><a href=""(.+?)"" class=""_self"" target=""_self"" >(【.級韓国語講座 第\d+回】.+?)</a></div></div></div>");
            var ms = reg1.Matches(html);
            var lines2 = new List<string>();
            foreach (Match m in ms)
            {
                var url = m.Groups[1].Value;
                var title = m.Groups[2].Value.Trim();
                var s = url + delim + title;
                lines2.Add(s);
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2() =>
            await Step2(7, "Trilingual");
    }
}
