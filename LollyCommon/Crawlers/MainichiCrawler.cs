using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon.Crawlers
{
    // 毎日のんびり日本語教師
    public class MainichiCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"font -weight:800; padding-left:10px;"">(.+?)</p>");
            var reg2 = new Regex(@"<a href=""(https://nihongonosensei.net/\?p=.+?)"">(.+?)</a>");
            // source code of https://nihongonosensei.net/?page_id=10246#linkn3 (日本語の文法)
            var lines = File.ReadAllLines("a.txt");
            var lines2 = new List<string>();
            var ptext = "";
            foreach (var s in lines)
            {
                var m = reg1.Match(s);
                if (m.Success)
                {
                    ptext = m.Groups[1].Value;
                    if (ptext == "Ｎ１　似ている文法の比較")
                        ptext = "Ｎ１文法の比較";
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

        public override async Task Step2()
        {
            var lines = File.ReadAllLines("b.txt");
            var storewp = new WebPageDataStore();
            var storept = new PatternDataStore();
            var storeptwp = new PatternWebPageDataStore();
            foreach (var s in lines)
            {
                var a = s.Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
                string tag = a[0], url = a[1], title = a[2];
                var pt = new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "毎日" + tag,
                };
                var wp = new MWebPage
                {
                    TITLE = $"【{tag}】{title}",
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
