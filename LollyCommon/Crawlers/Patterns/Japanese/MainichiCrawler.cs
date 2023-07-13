using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers.Patterns.Japanese
{
    // 毎日のんびり日本語教師
    public class MainichiCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"font-weight:800; padding-left:10px; padding-top:40px;"">(.+?)</p>");
            var reg2 = new Regex(@"<a href=""(https://nihongonosensei.net/\?p=.+?)"">(.+?)</a>");
            // 日本語の文法
            var text = await client.GetStringAsync("https://nihongonosensei.net/?page_id=10246");
            var lines = text.Split('\n');
            var lines2 = new List<string>();
            var ptext = "";
            var dic = new Dictionary<string, string> { { "Ｎ", "N" }, { "文法", "" }, { "、", "" }, { "０", "0" }, { "１", "1" }, { "２", "2" }, { "３", "3" }, { "４", "4" }, { "５", "5" } };
            foreach (var s in lines)
            {
                var m = reg1.Match(s);
                if (m.Success)
                {
                    ptext = m.Groups[1].Value.Replace(dic);
                    if (ptext == "敬語")
                        break;
                    continue;
                }
                m = reg2.Match(s);
                if (m.Success)
                {
                    var s1 = m.Groups[1].Value;
                    var s2 = m.Groups[2].Value;
                    var s3 = ptext + delim + s1 + delim + s2;
                    lines2.Add(s3);
                    continue;
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2() =>
            await Step2("毎日", a =>
            {
                string tag = a[0], url = a[1], title = a[2];
                return new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "毎日" + tag,
                    TITLE = $"【{tag}】{title}",
                    URL = url,
                };
            });
    }
}
