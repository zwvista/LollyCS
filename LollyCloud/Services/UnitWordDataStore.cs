using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class UnitWordDataStore : LollyDataStore<MUnitWord>
    {
        public async Task<List<MUnitWord>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter[]=TEXTBOOKID,eq,{textbook.ID}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITWORDS;
            foreach (var o in lst)
            {
                o.Units = textbook.Units;
                o.Parts = textbook.Parts;
            }
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter=LANGID,eq,{langid}&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VUNITWORDS;
            foreach (var o in lst)
            {
                var o2 = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
                o.Units = o2.Units;
                o.Parts = o2.Parts;
            }
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByLangWord(int wordid) =>
        (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter=WORDID,eq,{wordid}")).VUNITWORDS;

        public async Task<int> Create(MUnitWord item) =>
        await CreateByUrl($"UNITWORDS", item);

        public async Task<bool> UpdateSeqNum(int id, int seqnum) =>
        await UpdateByUrl($"UNITWORDS/{id}", $"SEQNUM={seqnum}");

        public async Task<bool> UpdateNote(int id, string note) =>
        await UpdateByUrl($"UNITWORDS/{id}", $"NOTE={note}");

        public async Task<bool> Update(MUnitWord item) =>
        await UpdateByUrl($"UNITWORDS/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"UNITWORDS/{id}");
    }
}
