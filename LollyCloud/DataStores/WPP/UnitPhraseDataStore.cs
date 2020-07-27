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

        List<MUnitPhrase> SetTextbook(List<MUnitPhrase> lst, List<MTextbook> lstTextbooks)
        {
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<List<MUnitPhrase>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM")).Records;
            return SetTextbook(lst, lstTextbooks);
        }

        public async Task<MUnitPhrase> GetDataById(int id, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=ID,eq,{id}")).Records.ToList();
            lst = SetTextbook(lst, lstTextbooks);
            return lst.Any() ? lst[0] : null;
        }

        public async Task<List<MUnitPhrase>> GetDataByPhraseId(int phraseid) =>
        (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=PHRASEID,eq,{phraseid}")).Records;
        public async Task<List<MUnitPhrase>> GetDataByLangPhrase(int langid, string phrase, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=LANGID,eq,{langid}&filter=PHRASE,eq,{HttpUtility.UrlEncode(phrase)}")).Records
                .Where(o => o.PHRASE == phrase).ToList();
            return SetTextbook(lst, lstTextbooks);
        }

        public async Task<int> Create(MUnitPhrase item) =>
        (await CallSPByUrl("UNITPHRASES_CREATE", item)).NewID.Value;

        public async Task UpdateSeqNum(int id, int seqnum) =>
        Debug.WriteLine(await UpdateByUrl($"UNITPHRASES/{id}", $"SEQNUM={seqnum}"));

        public async Task UpdateTranslation(int id, string translation) =>
        Debug.WriteLine(await UpdateByUrl($"UNITPHRASES/{id}", $"TRANSLATION={translation}"));

        public async Task Update(MUnitPhrase item) =>
        Debug.WriteLine(await CallSPByUrl("UNITPHRASES_UPDATE", item));

        public async Task Delete(MUnitPhrase item) =>
        Debug.WriteLine(await CallSPByUrl("UNITPHRASES_DELETE", item));
    }
}
