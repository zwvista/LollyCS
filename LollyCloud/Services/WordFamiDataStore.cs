using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public class WordFamiDataStore : LollyDataStore<MWordFami>
    {
        async Task<IEnumerable<MWordFami>> getDataByUserWord(int userid, int wordid) =>
        (await GetDataByUrl<MWordsFami>($"WORDSFAMI?filter=USERID,eq,{userid}&filter=WORDID,eq,{wordid}")).records;

        async Task<int> Create(MWordFami item) =>
        await CreateByUrl($"WORDSFAMI", item);

        async Task Update(MWordFami item) =>
        Debug.WriteLine(await UpdateByUrl($"WORDSFAMI/{item.ID}", JsonConvert.SerializeObject(item)));

        async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WORDSFAMI/{id}"));

        public async Task Update(int wordid, int level)
        {
            var userid = CommonApi.UserId;
            var lst = (await getDataByUserWord(userid, wordid)).ToList();
            var item = new MWordFami
            {
                USERID = userid,
                WORDID = wordid,
                LEVEL = level
            };
            if (lst.IsEmpty())
                if (level == 0)
                    return;
                else
                    await Create(item);
            else
            {
                var id = lst[0].ID;
                if (level == 0)
                    await Delete(id);
                else
                {
                    item.ID = id;
                    await Update(item);
                }
            }
        }
    }
}
