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
    // Korean Grammar Dictionary
    public class KGDCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var home = "http://koreangrammaticalforms.com/";
            var html = await client.GetStringAsync($"{home}index.php");
            var reg1 = new Regex(@"<a href=""javascript: GetPage\('(.+?)'\);"">.+?</a>");
            var reg2 = new Regex(@"<li><a href=""(.+?)"" target=""_blank"">(.+?)</a></li>");
            var ms = reg1.Matches(html);
            var lines2 = new List<string>();
            foreach (Match m in ms)
            {
                var html2 = await client.GetStringAsync($"{home}{m.Groups[1].Value}");
                var ms2 = reg2.Matches(html2);
                foreach (Match m2 in ms2)
                {
                    var url = home + m2.Groups[1].Value;
                    var title = m2.Groups[2].Value.Trim();
                    var s = url + delim + title;
                    lines2.Add(s);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2() =>
            await Step2(7, "KGD");
    }
}
