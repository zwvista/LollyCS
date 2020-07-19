using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class WebPageDataStore : LollyDataStore<MWebPage>
    {
        public async Task<List<MWebPage>> GetDataBySearch(string title, string url)
        {
            var filter = "";
            if (!string.IsNullOrEmpty(title)) filter += $"?filter=TITLE,cs,{HttpUtility.UrlEncode(title)}";
            if (!string.IsNullOrEmpty(url)) filter += (filter.IsEmpty() ? "?" : "&") + $"filter=URL,cs,{HttpUtility.UrlEncode(url)}";
            return (await GetDataByUrl<MWebPages>($"WEBPAGES{filter}")).Records;
        }
        public async Task<int> Create(MWebPage item) =>
        await CreateByUrl($"WEBPAGES", item);
        public async Task Update(MWebPage item) =>
        Debug.WriteLine(await UpdateByUrl($"WEBPAGES/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WEBPAGES/{id}"));
    }
}
