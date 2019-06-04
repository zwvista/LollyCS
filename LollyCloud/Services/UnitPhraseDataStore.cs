using System;
using System.Collections.Generic;
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
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=TEXTBOOKID,eq,{textbook.ID}&filter=UNITPART,bt,{unitPartFrom},{unitPartTo}&order=UNITPART&order=SEQNUM")).records;
            foreach (var o in lst)
                o.Textbook = textbook;
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM")).records;
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLangPhrase(int phraseid) =>
        (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=PHRASEID,eq,{phraseid}")).records;

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
