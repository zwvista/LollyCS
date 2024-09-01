using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LollyCommon
{
    public class OnlineTextbookDataStore : LollyDataStore<MOnlineTextbook>
    {
        public async Task<List<MOnlineTextbook>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MOnlineTextbooks>($"VONLINETEXTBOOKS?filter=LANGID,eq,{langid}")).Records;
        public async Task<int> Create(MOnlineTextbook item) =>
            await CreateByUrl($"ONLINETEXTBOOKS", item);
        public async Task Update(MOnlineTextbook item) =>
            Debug.WriteLine(await UpdateByUrl($"ONLINETEXTBOOKS/{item.ID}", JsonConvert.SerializeObject(item)));
        public async Task Delete(int id) =>
            Debug.WriteLine(await DeleteByUrl($"ONLINETEXTBOOKS/{id}"));
    }
}
