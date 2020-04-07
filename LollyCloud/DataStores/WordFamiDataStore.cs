using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class WordFamiDataStore : LollyDataStore<MWordFami>
    {
        async Task<IEnumerable<MWordFami>> GetDataByUserWord(int userid, int wordid) =>
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
            var lst = (await GetDataByUserWord(userid, wordid)).ToList();
            var item = new MWordFami
            {
                USERID = userid,
                WORDID = wordid,
                LEVEL = level,
            };
            if (lst.IsEmpty())
            {
                if (level != 0)
                    await Create(item);
            }
            else
            {
                var o = lst[0];
                if (level == 0 && o.CORRECT == 0 && o.TOTAL == 0)
                    await Delete(o.ID);
                else
                {
                    item.ID = o.ID;
                    item.CORRECT = o.CORRECT;
                    item.TOTAL = o.TOTAL;
                    await Update(item);
                }
            }
        }

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
                item.LEVEL = o.LEVEL;
                item.CORRECT = o.CORRECT + (isCorrect ? 1 : 0);
                item.TOTAL = o.TOTAL + 1;
                await Update(item);
            }
            return item;
        }
    }
}
