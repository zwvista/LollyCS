using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class UnitPhraseDataStore : LollyDataStore<MUnitPhrase>
    {
        public async Task<IEnumerable<MUnitPhrase>> GetDataByTextbookUnitPart(int textbookid, int unitPartFrom, int unitPartTo,
            ObservableCollection<MSelectItem> lstUnits, ObservableCollection<MSelectItem> lstParts)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?transform=1&filter[]=TEXTBOOKID,eq,{textbookid}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITPHRASES;
            foreach (var o in lst)
            {
                o.lstUnits = lstUnits;
                o.lstParts = lstParts;
            }
            return lst;
        }

        public async Task<IEnumerable<MUnitPhrase>> GetDataByTextbookUnitPart(int langid) =>
        (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?transform=1&filter[]=LANGID,eq,{langid}&&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VUNITPHRASES;

        public async Task<bool> Create(MUnitPhrase item) =>
        await CreateByUrl($"UNITPHRASES", item);

        public async Task<bool> UpdateSeqNum(int id, int seqnum) =>
        await UpdateByUrl($"UNITPHRASES/{id}", $"SEQNUM={seqnum}");

        public async Task<bool> UpdateNote(int id, string note) =>
        await UpdateByUrl($"UNITPHRASES/{id}", $"NOTE={note}");

        public async Task<bool> Update(MUnitPhrase item) =>
        await UpdateByUrl($"UNITPHRASES/{item.ID}", JsonConvert.SerializeObject(item));

        public async Task<bool> Delete(int id) =>
        await DeleteByUrl($"UNITPHRASES/{id}");
    }
}
