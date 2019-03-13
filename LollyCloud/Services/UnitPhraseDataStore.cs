using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class UnitPhraseDataStore : LollyDataStore<MUnitPhrase>
    {
        public async Task<List<MUnitPhrase>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?transform=1&filter[]=TEXTBOOKID,eq,{textbook.ID}&filter[]=UNITPART,bt,{unitPartFrom},{unitPartTo}&order[]=UNITPART&order[]=SEQNUM")).VUNITPHRASES;
            foreach (var o in lst)
            {
                o.lstUnits = textbook.lstUnits;
                o.lstParts = textbook.lstParts;
            }
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?transform=1&filter=LANGID,eq,{langid}&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VUNITPHRASES;
            foreach (var o in lst)
            {
                var o2 = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
                o.lstUnits = o2.lstUnits;
                o.lstParts = o2.lstParts;
            }
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLangPhrase(int phraseid) =>
        (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?transform=1&filter=PHRASEID,eq,{phraseid}")).VUNITPHRASES;

        public async Task<int> Create(MUnitPhrase item) =>
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
