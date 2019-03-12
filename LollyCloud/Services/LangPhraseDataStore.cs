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
        (await GetDataByUrl<MLangPhrases>($"VLANGPHRASE?transform=1&filter=LANGID,eq,{langid}&order=PHRASE")).LANGPHRASES;

        public async Task<List<MLangPhrase>> GetDataByLangPhrase(int langid, string phrase) =>
        (await GetDataByUrl<MLangPhrases>($"VLANGPHRASE?transform=1&filter[]=LANGID,eq,{langid}&filter[]=PHRASE,eq,{HttpUtility.HtmlEncode(phrase)}")).LANGPHRASES;

        public async Task<List<MLangPhrase>> GetDataById(int id) =>
        (await GetDataByUrl<MLangPhrases>($"VLANGPHRASE?transform=1&filter[]=ID,eq,{id}")).LANGPHRASES;

        public async Task<bool> Create(MLangPhrase item) =>
        await CreateByUrl($"LANGPHRASE", item);

        public async Task<bool> UpdateTranslation(int id, string translation) =>
        await UpdateByUrl($"LANGPHRASE/{id}", $"TRANSLATION={translation}");

        public async Task<bool> Update(MLangPhrase item) =>
        await UpdateByUrl($"LANGPHRASE/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"LANGPHRASE/{id}");
    }
}
