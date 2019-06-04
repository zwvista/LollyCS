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
    public class LangWordDataStore : LollyDataStore<MLangWord>
    {
        public async Task<List<MLangWord>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=LANGID,eq,{langid}&order=WORD")).records;

        public async Task<List<MLangWord>> GetDataByLangWord(int langid, string word) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=LANGID,eq,{langid}&filter=WORD,eq,{HttpUtility.HtmlEncode(word)}")).records;

        public async Task<List<MLangWord>> GetDataById(int id) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=ID,eq,{id}")).records;

        public async Task<int> Create(MLangWord item) =>
        await CreateByUrl($"LANGWORDS", item);

        public async Task<bool> UpdateNote(int id, string note) =>
        await UpdateByUrl($"LANGWORDS/{id}", $"NOTE={note}");

        public async Task<bool> Update(MLangWord item) =>
        await UpdateByUrl($"LANGWORDS/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"LANGWORDS/{id}");
    }
}
