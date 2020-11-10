using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class WebTextbookDataStore : LollyDataStore<MWebTextbook>
    {
        public async Task<List<MWebTextbook>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MWebTextbooks>($"VWEBTEXTBOOKS?filter=LANGID,eq,{langid}")).Records;
        public async Task<int> Create(MWebTextbook item) =>
        await CreateByUrl($"WEBTEXTBOOKS", item);
        public async Task Update(MWebTextbook item) =>
        Debug.WriteLine(await UpdateByUrl($"WEBTEXTBOOKS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WEBTEXTBOOKS/{id}"));
    }
}
