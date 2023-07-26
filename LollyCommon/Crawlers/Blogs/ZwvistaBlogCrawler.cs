using DynamicData;
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
    public class ZwvistaBlogCrawler
    {
        HttpClient client = new HttpClient();
        public async Task GetLangBlogPosts()
        {
            var blogGroups = await new LangBlogGroupDataStore().GetDataByLang(2);
            var reg1 = new Regex(@"\s+<a href=""(https://zwvista.wordpress.com/.+?)"" rel=""bookmark"">【日语句型】(.+?)</a>");
            var reg2 = new Regex(@"<div class=""entry"">");
            var reg3 = new Regex(@"<div id=""atatags.+?""></div>");
            var reg4 = new Regex(@"<a href=""https://zwvista.wordpress.com/.+?"" rel=""category tag"">(.+?)</a>");
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
                var state = 0;
                foreach (var line in lines)
                {
                    var m = reg1.Match(line);
                    if (m.Success)
                    {
                        url = m.Groups[1].Value;
                        title = m.Groups[2].Value;
                        continue;
                    }
                    m = reg2.Match(line);
                    if (m.Success)
                    {
                        state = 1;
                        continue;
                    }
                    if (state == 1)
                    {
                        if (line.StartsWith("<div>")) state = 2;
                    }
                    if (state == 2)
                    {
                        if (line.IsEmpty() || reg3.Match(line).Success)
                        {
                            state = 0;
                            content = "";
                            continue;
                        }
                        content += line + '\n';
                    }
                    if (state == 0)
                    {
                        var ms = reg4.Matches(line).Cast<Match>().ToList();
                        if (ms.IsEmpty()) continue;
                        var groups = ms.Select(m2 => m2.Groups[1].Value).ToList();
                        var groupIds = groups
                            .Select(g => blogGroups.First(bg => bg.GROUPNAME == g).ID)
                            .ToList();
                        groupIds.ForEach(t =>
                        {

                        });
                    }
                }
            }
        }
    }
}
