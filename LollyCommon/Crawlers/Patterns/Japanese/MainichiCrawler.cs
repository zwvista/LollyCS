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
            var start = "<h2>レベル順</h2>";
            var reg1 = new Regex(@"<span class=""n(.)color"">【Ｎ.文法】</span>.+?<a href=""(.+?)"">(.+?)</a>");
            // 日本語の文法
            var text = await client.GetStringAsync("https://mainichi-nonbiri.com/japanese-grammar/");
            text = text.Substring(text.IndexOf(start));
            var lines = text.Split('\n');
            var lines2 = new List<string>();
            foreach (var s in lines)
            {
                var m = reg1.Match(s);
                if (m.Success)
                {
                    var s1 = m.Groups[1].Value;
                    var s2 = m.Groups[2].Value;
                    var s3 = m.Groups[3].Value;
                    var s4 = "N" + s1 + delim + s2 + delim + s3;
                    lines2.Add(s4);
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
