using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class LangBlogDataStore : LollyDataStore<MLangBlog>
    {
        public async Task<List<MLangBlog>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MLangBlogs>($"VLANGBLOGS?filter=LANGID,eq,{langid}")).Records;
        public async Task<List<MLangBlog>> GetDataByLangGroup(int langid, int groupid) =>
            (await GetDataByUrl<MLangBlogs>($"VLANGBLOGS?filter=LANGID,eq,{langid}&GROUPID,eq,{groupid}")).Records;
        public async Task<int> Create(MLangBlog item) =>
            await CreateByUrl($"LANGBLOGS", item);
        public async Task Update(MLangBlog item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"LANGBLOGS/{id}"));
    }
}
