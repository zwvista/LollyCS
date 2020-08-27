using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
        public async Task<List<MWebPage>> GetDataById(int id) =>
        (await GetDataByUrl<MWebPages>($"WEBPAGES?filter=ID,eq,{id}")).Records;
        public async Task<int> Create(MPatternWebPage item)
        {
            var item2 = new MWebPage
            {
                TITLE = item.TITLE,
                URL = item.URL,
            };
            return await CreateByUrl($"WEBPAGES", item2);
        }
        public async Task Update(MPatternWebPage item)
        {
            var item2 = new MWebPage
            {
                ID = item.WEBPAGEID,
                TITLE = item.TITLE,
                URL = item.URL,
            };
            Debug.WriteLine(await UpdateByUrl($"WEBPAGES/{item2.ID}", JsonConvert.SerializeObject(item2)));
        }
        public async Task<int> Create(MWebPage item) =>
        await CreateByUrl($"WEBPAGES", item);
        public async Task Update(MWebPage item) =>
        Debug.WriteLine(await UpdateByUrl($"WEBPAGES/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WEBPAGES/{id}"));
    }
}
