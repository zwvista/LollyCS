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
    // 韩国语基本语法
    public class HGYJBYFCrawler : TextbooksCrawler
    {
        public override async Task Step1()
        {
            var reg1 = new Regex(@"<a href=""http://(www.krlearn.com/.+?)"" target=""_blank"" title=""(.+?)"" style=""color: #039;"">.+?</a>");
            var lines2 = new List<string>();
            for (int i = 1; i < 100; i++)
            {
                string html;
                try
                {
                    html = await client.GetStringAsync($"https://www.krlearn.com/yufafudao/hanguoyujibenyufa/list_{i}.html");
                }
                catch (Exception)
                {
                    break;
                }
                var ms = reg1.Matches(html).Cast<Match>().ToList();
                foreach (var m in ms)
                {
                    var url = "https://" + m.Groups[1].Value;
                    var title = m.Groups[2].Value;
                    var s = url + delim + title;
                    lines2.Insert(0, s);
                }
            }
            File.WriteAllLines("b.txt", lines2);
        }
        public override async Task Step2() =>
            await Step2(712);
    }
}