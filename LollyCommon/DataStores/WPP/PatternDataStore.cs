using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon
{
    public class PatternDataStore : LollyDataStore<MPattern>
    {
        public async Task<List<MPattern>> GetDataByLang(int langid,
            string textFilter, string scopeFilter, int? pageNo = null, int? pageSize = null)
        {
            var url = $"PATTERNS?filter=LANGID,eq,{langid}&order=PATTERN";
            if (!string.IsNullOrEmpty(textFilter))
                url += $"&filter={scopeFilter},cs,{HttpUtility.UrlEncode(textFilter)})";
            if (pageNo.HasValue && pageSize.HasValue)
                url += $"&page={pageNo},{pageSize}";
            return (await GetDataByUrl<MPatterns>(url)).Records;
        }

        public async Task<List<MPattern>> GetDataById(int id) =>
            (await GetDataByUrl<MPatterns>($"PATTERNS?filter=ID,eq,{id}")).Records;

        public async Task<List<MPattern>> GetDataByTag(string tag) =>
            (await GetDataByUrl<MPatterns>($"PATTERNS?filter=TAGS,sw,{tag}")).Records;

        public async Task<int> Create(MPattern item) =>
            await CreateByUrl($"PATTERNS", item);

        public async Task UpdateNote(int id, string note) =>
            Debug.WriteLine(await UpdateByUrl($"PATTERNS/{id}", $"NOTE={note}"));

        public async Task Update(MPattern item) =>
            Debug.WriteLine(await UpdateByUrl($"PATTERNS/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"PATTERNS/{id}"));
    }
}
