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
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?transform=1&filter=LANGID,eq,{langid}&order=WORD")).VLANGWORDS;

        public async Task<List<MLangWord>> GetDataByLangWord(int langid, string word) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?transform=1&filter[]=LANGID,eq,{langid}&filter[]=WORD,eq,{HttpUtility.HtmlEncode(word)}")).VLANGWORDS;

        public async Task<List<MLangWord>> GetDataById(int id) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?transform=1&filter=ID,eq,{id}")).VLANGWORDS;

        public async Task<bool> Create(MLangWord item) =>
        await CreateByUrl($"LANGWORDS", item);

        public async Task<bool> UpdateNote(int id, string note) =>
        await UpdateByUrl($"LANGWORDS/{id}", $"NOTE={note}");

        public async Task<bool> Update(MLangWord item) =>
        await UpdateByUrl($"LANGWORDS/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"LANGWORDS/{id}");
    }
}
