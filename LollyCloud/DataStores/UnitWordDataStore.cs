using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public class UnitWordDataStore : LollyDataStore<MUnitWord>
    {
        public async Task<List<MUnitWord>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=TEXTBOOKID,eq,{textbook.ID}&filter=UNITPART,bt,{unitPartFrom},{unitPartTo}&order=UNITPART&order=SEQNUM")).records;
            foreach (var o in lst)
                o.Textbook = textbook;
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM")).records;
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByLangWord(int wordid) =>
        (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=WORDID,eq,{wordid}")).records;

        public async Task<int> Create(MUnitWord item) =>
        await CreateByUrl($"UNITWORDS", item);

        public async Task UpdateSeqNum(int id, int seqnum) =>
        Debug.WriteLine(await UpdateByUrl($"UNITWORDS/{id}", $"SEQNUM={seqnum}"));

        public async Task UpdateNote(int id, string note) =>
        Debug.WriteLine(await UpdateByUrl($"UNITWORDS/{id}", $"NOTE={note}"));

        public async Task Update(MUnitWord item) =>
        Debug.WriteLine(await UpdateByUrl($"UNITWORDS/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"UNITWORDS/{id}"));
    }
}
