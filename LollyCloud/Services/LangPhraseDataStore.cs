using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class LangPhraseDataStore : LollyDataStore<MLangPhrase>
    {
        public async Task<List<MLangPhrase>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=LANGID,eq,{langid}&order=PHRASE")).records;

        public async Task<List<MLangPhrase>> GetDataByLangPhrase(int langid, string phrase) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=LANGID,eq,{langid}&filter=PHRASE,eq,{HttpUtility.HtmlEncode(phrase)}")).records;

        public async Task<List<MLangPhrase>> GetDataById(int id) =>
        (await GetDataByUrl<MLangPhrases>($"LANGPHRASES?filter=ID,eq,{id}")).records;

        public async Task<int> Create(MLangPhrase item) =>
        await CreateByUrl($"LANGPHRASES", item);

        public async Task<bool> UpdateTranslation(int id, string translation) =>
        await UpdateByUrl($"LANGPHRASES/{id}", $"TRANSLATION={translation}");

        public async Task<bool> Update(MLangPhrase item) =>
        await UpdateByUrl($"LANGPHRASES/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"LANGPHRASES/{id}");
    }
}
