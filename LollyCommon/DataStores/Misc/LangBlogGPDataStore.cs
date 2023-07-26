using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class LangBlogGPDataStore : LollyDataStore<MLangBlogGP>
    {
        public async Task<int> Create(MLangBlogGP item) =>
            await CreateByUrl($"LANGBLOGGP", item);
        public async Task Update(MLangBlogGP item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGBLOGGP/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"LANGBLOGGP/{id}"));
    }
}
