using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Joins;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers.Patterns.Japanese
{
    // 日本語表現文型辞典
    public class HyogenCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"(\d+)\. (.+)");
            // 日本語表現文型辞典.txt
            var lines = File.ReadAllLines("a.txt");
            var lines2 = new List<string>();
            foreach (var s in lines)
            {
                var m = reg1.Match(s);
                if (m.Success)
                {
                    var patternNo = m.Groups[1].Value;
                    var pattern = m.Groups[2].Value.Replace("*", "");
                    var url = $"http://viethuong.web.fc2.com/MONDAI/{patternNo}.html";
                    var t = url + delim + patternNo + delim + pattern + delim + s;
                    lines2.Add(t);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2() =>
            await Step2("表現", a =>
            {
                string url = a[0], patternNo = a[1], pattern = a[2], title = a[3];
                return new MPattern
                {
                    LANGID = 2,
                    PATTERN = pattern,
                    TAGS = $"表現{patternNo}",
                    TITLE = title,
                    URL = url,
                };
            });
    }
}
