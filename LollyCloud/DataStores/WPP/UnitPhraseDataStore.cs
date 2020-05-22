using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCloud
{
    public class UnitPhraseDataStore : LollyDataStore<MUnitPhrase>
    {
        public async Task<List<MUnitPhrase>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=TEXTBOOKID,eq,{textbook.ID}&filter=UNITPART,bt,{unitPartFrom},{unitPartTo}&order=UNITPART&order=SEQNUM")).Records;
            foreach (var o in lst)
                o.Textbook = textbook;
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM")).Records;
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByPhraseId(int phraseid) =>
        (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=PHRASEID,eq,{phraseid}")).Records;
        public async Task<List<MUnitPhrase>> GetDataByLangPhrase(int langid, string phrase, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=LANGID,eq,{langid}&filter=PHRASE,eq,{HttpUtility.UrlEncode(phrase)}")).Records
                .Where(o => o.PHRASE == phrase).ToList();
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<int> Create(MUnitPhrase item) =>
        await CreateByUrl($"UNITPHRASES", item);

        public async Task UpdateSeqNum(int id, int seqnum) =>
        Debug.WriteLine(await UpdateByUrl($"UNITPHRASES/{id}", $"SEQNUM={seqnum}"));

        public async Task UpdateNote(int id, string note) =>
        Debug.WriteLine(await UpdateByUrl($"UNITPHRASES/{id}", $"NOTE={note}"));

        public async Task Update(MUnitPhrase item) =>
        Debug.WriteLine(await UpdateByUrl($"UNITPHRASES/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"UNITPHRASES/{id}"));
    }
}
