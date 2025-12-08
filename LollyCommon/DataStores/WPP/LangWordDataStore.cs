using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon
{
    public class LangWordDataStore : LollyDataStore<MLangWord>
    {
        public async Task<(List<MLangWord>, int)> GetDataByLang(int langid,
            string textFilter, string scopeFilter, int? pageNo = null, int? pageSize = null)
        {
            var url = $"VLANGWORDS?filter=LANGID,eq,{langid}&order=WORD";
            if (!string.IsNullOrEmpty(textFilter))
                url += $"&filter={scopeFilter},cs,{HttpUtility.UrlEncode(textFilter)}";
            if (pageNo.HasValue && pageSize.HasValue)
                url += $"&page={pageNo},{pageSize}";
            var o = await GetDataByUrl<MLangWords>(url);
            return (o.Records, o.Count);
        }

        public async Task<int> Create(MLangWord item) =>
            await CreateByUrl($"LANGWORDS", item);

        public async Task UpdateNote(int id, string note) =>
            Debug.WriteLine(await UpdateByUrl($"LANGWORDS/{id}", $"NOTE={note}"));

        public async Task Update(MLangWord item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGWORDS/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(MLangWord item) =>
            Debug.WriteLine(await CallSPByUrl("LANGWORDS_DELETE", item));
    }
}
