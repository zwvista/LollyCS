using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCloud
{
    public class LangWordDataStore : LollyDataStore<MLangWord>
    {
        public async Task<List<MLangWord>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=LANGID,eq,{langid}&order=WORD")).Records;

        public async Task<List<MLangWord>> GetDataByLangWord(int langid, string word) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=LANGID,eq,{langid}&filter=WORD,eq,{HttpUtility.UrlEncode(word)}")).Records
        .Where(o => o.WORD == word).ToList();

        public async Task<List<MLangWord>> GetDataById(int id) =>
        (await GetDataByUrl<MLangWords>($"VLANGWORDS?filter=ID,eq,{id}")).Records;

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
