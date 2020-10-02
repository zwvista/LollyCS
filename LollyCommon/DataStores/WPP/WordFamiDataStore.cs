using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordFamiDataStore : LollyDataStore<MWordFami>
    {
        public async Task<List<MWordFami>> GetDataByUserWord(int userid, int wordid) =>
        (await GetDataByUrl<MWordsFami>($"WORDSFAMI?filter=USERID,eq,{userid}&filter=WORDID,eq,{wordid}")).Records;

        async Task<int> Create(MWordFami item) =>
        await CreateByUrl($"WORDSFAMI", item);

        async Task Update(MWordFami item) =>
        Debug.WriteLine(await UpdateByUrl($"WORDSFAMI/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WORDSFAMI/{id}"));

        public async Task<MWordFami> Update(int wordid, bool isCorrect)
        {
            var userid = CommonApi.UserId;
            var lst = (await GetDataByUserWord(userid, wordid)).ToList();
            var item = new MWordFami
            {
                USERID = userid,
                WORDID = wordid,
            };
            if (lst.IsEmpty())
            {
                item.CORRECT = isCorrect ? 1 : 0;
                item.TOTAL = 1;
                await Create(item);
            }
            else
            {
                var o = lst[0];
                item.ID = o.ID;
                item.CORRECT = o.CORRECT + (isCorrect ? 1 : 0);
                item.TOTAL = o.TOTAL + 1;
                await Update(item);
            }
            return item;
        }
    }
}
