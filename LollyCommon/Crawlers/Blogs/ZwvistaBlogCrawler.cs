using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class ZwvistaBlogCrawler
    {
        HttpClient client = new HttpClient();
        public async Task GetLangBlogPosts()
        {
            var reg1 = new Regex(@"\s+<a href=""(https://zwvista.wordpress.com/.+?)"" rel=""bookmark"">【日语句型】(.+?)</a>");
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
                var lines = html.Split('\n');
                string url = "", title = "", content = "";
                foreach (var line in lines)
                {
                    var m = reg1.Match(line);
                    if (m.Success)
                    {
                        url = m.Groups[1].Value;
                        title = m.Groups[2].Value;
                    }
                }
            }
        }
    }
}
