using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class LangBlogGroupDataStore : LollyDataStore<MLangBlogGroup>
    {
        public async Task<List<MLangBlogGroup>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MLangBlogGroups>($"LANGBLOGGROUPS?filter=LANGID,eq,{langid}")).Records;
        public async Task<int> Create(MLangBlogGroup item) =>
        await CreateByUrl($"LANGBLOGGROUPS", item);
        public async Task Update(MLangBlogGroup item) =>
        Debug.WriteLine(await UpdateByUrl($"LANGBLOGGROUPS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"LANGBLOGGROUPS/{id}"));
    }
}
