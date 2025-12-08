using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon
{
    public class UnitPhraseDataStore : LollyDataStore<MUnitPhrase>
    {
        public async Task<(List<MUnitPhrase>, int)> GetDataByTextbookUnitPart(
            MTextbook textbook, int unitPartFrom, int unitPartTo, string textFilter, string scopeFilter)
        {
            var url = $"VUNITPHRASES?filter=TEXTBOOKID,eq,{textbook.ID}&filter=UNITPART,bt,{unitPartFrom},{unitPartTo}";
            if (!string.IsNullOrEmpty(textFilter))
                url += $"&filter={scopeFilter},cs,{HttpUtility.UrlEncode(textFilter)}";
            var o = await GetDataByUrl<MUnitPhrases>(url);
            foreach (var o2 in o.Records)
                o2.Textbook = textbook;
            return (o.Records, o.Count);
        }

        public async Task<List<MUnitPhrase>> GetDataByTextbook(MTextbook textbook)
        {
            var lst = (await GetDataByUrl<MUnitPhrases>($"VUNITPHRASES?filter=TEXTBOOKID,eq,{textbook.ID}&order=PHRASEID")).Records
                .Distinct(o => o.PHRASEID).ToList();
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

        public async Task<(List<MUnitPhrase>, int)> GetDataByLang(int langid, List<MTextbook> lstTextbooks,
            string textFilter, string scopeFilter, int textbookFilter, int? pageNo = null, int? pageSize = null)
        {
            var url = $"VUNITPHRASES?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM";
            if (!string.IsNullOrEmpty(textFilter))
                url += $"&filter={scopeFilter},cs,{HttpUtility.UrlEncode(textFilter)})";
            if (textbookFilter != 0)
                url += $"&filter=TEXTBOOKID,eq,{textbookFilter}";
            if (pageNo.HasValue && pageSize.HasValue)
                url += $"&page={pageNo},{pageSize}";
            var o = await GetDataByUrl<MUnitPhrases>(url);
            return (SetTextbook(o.Records, lstTextbooks), o.Count);
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
            (await CallSPByUrl("UNITPHRASES_CREATE", item)).NewID!.Value;

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
