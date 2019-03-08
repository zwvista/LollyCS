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
        public async Task<IEnumerable<MUnitWord>> GetDataByTextbookUnitPart(int textbookid, int unitPartFrom, int unitPartTo,
            ObservableCollection<MSelectItem> lstUnits, ObservableCollection<MSelectItem> lstParts)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?transform=1&filter[]=TEXTBOOKID,eq,{textbookid}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITWORDS;
            foreach (var o in lst)
            {
                o.lstUnits = lstUnits;
                o.lstParts = lstParts;
            }
            return lst;
        }

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
