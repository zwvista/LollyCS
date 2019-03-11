using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class UnitWordDataStore : LollyDataStore<MUnitWord>
    {
        public async Task<IEnumerable<MUnitWord>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter[]=TEXTBOOKID,eq,{textbook.ID}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITWORDS;
            foreach (var o in lst)
            {
                o.lstUnits = textbook.lstUnits;
                o.lstParts = textbook.lstParts;
            }
            return lst;
        }

        public async Task<IEnumerable<MUnitWord>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter=LANGID,eq,{langid}&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VUNITWORDS;

        public async Task<bool> Create(MUnitWord item) =>
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
