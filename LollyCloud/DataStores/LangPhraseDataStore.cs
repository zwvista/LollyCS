using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace LollyCloud
{
    public class LangPhraseDataStore : LollyDataStore<MLangPhrase>
    {
        public async Task<List<MLangPhrase>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=LANGID,eq,{langid}&order=PHRASE")).records;

        public async Task<List<MLangPhrase>> GetDataByLangPhrase(int langid, string phrase) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=LANGID,eq,{langid}&filter=PHRASE,eq,{HttpUtility.UrlEncode(phrase)}")).records;

        public async Task<List<MLangPhrase>> GetDataById(int id) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=ID,eq,{id}")).records;

        public async Task<int> Create(MLangPhrase item) =>
        await CreateByUrl($"LANGPHRASES", item);

        public async Task UpdateTranslation(int id, string translation) =>
        Debug.WriteLine(await UpdateByUrl($"LANGPHRASES/{id}", $"TRANSLATION={translation}"));

        public async Task Update(MLangPhrase item) =>
        Debug.WriteLine(await UpdateByUrl($"LANGPHRASES/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"LANGPHRASES/{id}"));
    }
}
