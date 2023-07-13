using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers.Patterns.Japanese
{
    // 絵でわかる日本語
    public class EdeCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var client = new HttpClient();
            // (日本語文法リスト（あいうえお順）)
            var html = await client.GetStringAsync("http://www.edewakaru.com/archives/cat_179055.html");
            var reg1 = new Regex(@"href=""(http://www.edewakaru.com/archives/.+?\.html)"".*?>(.*?)<");
            var text = html;
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

        public override async Task Step2() =>
            await Step2(2, "絵で");
    }
}
