using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LollyShared
{
    public class WordFamiDataStore : LollyDataStore<MWordFami>
    {
        private async Task<IEnumerable<MWordFami>> getDataByUserWord(int userid, int wordid) =>
        (await GetDataByUrl<MWordsFami>($"WORDSFAMI?transform=1&filter[]=USERID,eq,{userid}&filter[]=WORDID,eq,{wordid}")).WORDSFAMI;

        private async Task<bool> Create(MWordFami item) =>
        await CreateByUrl($"WORDSFAMI", item);

        private async Task<bool> Update(MWordFami item) =>
        await UpdateByUrl($"WORDSFAMI/{item.ID}", JsonConvert.SerializeObject(item));

        private async Task<bool> Delete(int id) =>
        await DeleteByUrl($"WORDSFAMI/{id}");

        public async Task<bool> Update(int wordid, int level)
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
                    return true;
                else
                    return await Create(item);
            else
            {
                var id = lst[0].ID;
                if (level == 0)
                    return await Delete(id);
                else
                {
                    item.ID = id;
                    return await Update(item);
                }
            }
        }
    }
}
