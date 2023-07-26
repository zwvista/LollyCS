using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class LangBlogDataStore : LollyDataStore<MLangBlogPost>
    {
        public async Task<List<MLangBlogPost>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MLangBlogPosts>($"VLANGBLOGS?filter=LANGID,eq,{langid}")).Records;
        public async Task<List<MLangBlogPost>> GetDataByLangGroup(int langid, int groupid) =>
            (await GetDataByUrl<MLangBlogPosts>($"VLANGBLOGS?filter=LANGID,eq,{langid}&GROUPID,eq,{groupid}")).Records;
        public async Task<int> Create(MLangBlogPost item) =>
            await CreateByUrl($"LANGBLOGS", item);
        public async Task Update(MLangBlogPost item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"LANGBLOGS/{id}"));
    }
}
