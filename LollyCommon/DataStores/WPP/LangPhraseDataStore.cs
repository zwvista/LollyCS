using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon
{
    public class LangPhraseDataStore : LollyDataStore<MLangPhrase>
    {
        public async Task<(List<MLangPhrase>, int)> GetDataByLang(int langid,
            string textFilter, string scopeFilter, int? pageNo = null, int? pageSize = null)
        {
            var url = $"LANGPHRASES?filter=LANGID,eq,{langid}&order=PHRASE";
            if (!string.IsNullOrEmpty(textFilter))
                url += $"&filter={scopeFilter},cs,{HttpUtility.UrlEncode(textFilter)}";
            if (pageNo.HasValue && pageSize.HasValue)
                url += $"&page={pageNo},{pageSize}";
            var o = await GetDataByUrl<MLangPhrases>(url);
            return (o.Records, o.Count);
        }

        public async Task<int> Create(MLangPhrase item) =>
            await CreateByUrl($"LANGPHRASES", item);

        public async Task UpdateTranslation(int id, string translation) =>
            Debug.WriteLine(await UpdateByUrl($"LANGPHRASES/{id}", $"TRANSLATION={translation}"));

        public async Task Update(MLangPhrase item) =>
            Debug.WriteLine(await UpdateByUrl($"LANGPHRASES/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(MLangPhrase item) =>
            Debug.WriteLine(await CallSPByUrl("LANGPHRASES_DELETE", item));
    }
}
