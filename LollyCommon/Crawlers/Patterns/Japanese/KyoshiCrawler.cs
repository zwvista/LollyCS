﻿using LollyCommon.Crawlers.Patterns;
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
    // 日本語教師NET
    public class KyoshiCrawler : PatternsCrawler
    {
        public override async Task Step1()
        {
            var html = await client.GetStringAsync("https://nihongokyoshi-net.com/category/jlpt/");
            var reg1 = new Regex(@"<a class=""page-numbers"" href=""https://nihongokyoshi-net.com/category/jlpt/page/(\d+)/"">\d+</a></div></main>");
            var m = reg1.Match(html);
            if (!m.Success) return;
            var pages = int.Parse(m.Groups[1].Value);
            var reg2 = new Regex(@"<a href=""(https://nihongokyoshi-net.com/[^""]+?)"" title=""【JLPT([^】]+?)】文法・例文：([^""]+?)"" rel=""bookmark"">.+?</a>");
            var lines2 = new List<string>();
            var dic = new Dictionary<string, string> { { "〜", "～" }, { "/", "／" }, { " ", "" }, { "１", "1" }, { "２", "2" }, { "３", "3" }, { "４", "4" }, { "５", "5" }, { "に出ない？", "0" } };
            for (int i = 1; i <= pages; i++)
            {
                html = await client.GetStringAsync($"https://nihongokyoshi-net.com/category/jlpt/page/{i}/");
                var ms = reg2.Matches(html);
                foreach (Match m2 in ms)
                {
                    var url = m2.Groups[1].Value;
                    var tag = m2.Groups[2].Value.Replace(dic).Trim();
                    var title = m2.Groups[3].Value.Replace(dic).Trim();
                    if (!tag.StartsWith("N")) tag = "N0";
                    var s = url + delim + tag + delim + title;
                    lines2.Add(s);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }

        public override async Task Step2() =>
            await Step2("教師", a =>
            {
                string url = a[0], tag = a[1], title = a[2];
                return new MPattern
                {
                    LANGID = 2,
                    PATTERN = title,
                    TAGS = "教師" + tag,
                    TITLE = $"【{tag}】{title}",
                    URL = url,
                };
            });
    }
}
