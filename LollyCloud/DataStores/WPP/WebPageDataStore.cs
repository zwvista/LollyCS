using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class WebPageDataStore : LollyDataStore<MWebPage>
    {
        public async Task<int> Create(MWebPage item) =>
        await CreateByUrl($"WEBPAGES", item);
        public async Task Update(MWebPage item) =>
        Debug.WriteLine(await UpdateByUrl($"WEBPAGES/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WEBPAGES/{id}"));
    }
}
