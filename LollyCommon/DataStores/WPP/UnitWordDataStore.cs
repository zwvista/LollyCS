using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LollyCommon
{
    public class UnitWordDataStore : LollyDataStore<MUnitWord>
    {
        public async Task<List<MUnitWord>> GetDataByTextbookUnitPart(MTextbook textbook, int unitPartFrom, int unitPartTo)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=TEXTBOOKID,eq,{textbook.ID}&filter=UNITPART,bt,{unitPartFrom},{unitPartTo}&order=UNITPART&order=SEQNUM")).Records;
            foreach (var o in lst)
                o.Textbook = textbook;
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByTextbook(MTextbook textbook)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=TEXTBOOKID,eq,{textbook.ID}&order=WORDID")).Records
                .Distinct(o => o.WORDID).ToList();
            foreach (var o in lst)
                o.Textbook = textbook;
            return lst;
        }

        List<MUnitWord> SetTextbook(List<MUnitWord> lst, List<MTextbook> lstTextbooks)
        {
            foreach (var o in lst)
                o.Textbook = lstTextbooks.First(o3 => o3.ID == o.TEXTBOOKID);
            return lst;
        }

        public async Task<List<MUnitWord>> GetDataByLang(int langid, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=LANGID,eq,{langid}&order=TEXTBOOKID&order=UNIT&order=PART&order=SEQNUM")).Records;
            return SetTextbook(lst, lstTextbooks);
        }

        public async Task<MUnitWord> GetDataById(int id, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=ID,eq,{id}")).Records.ToList();
            lst = SetTextbook(lst, lstTextbooks);
            return lst.Any() ? lst[0] : null;
        }

        public async Task<List<MUnitWord>> GetDataByLangWord(int langid, string word, List<MTextbook> lstTextbooks)
        {
            var lst = (await GetDataByUrl<MUnitWords>($"VUNITWORDS?filter=LANGID,eq,{langid}&filter=WORD,eq,{HttpUtility.UrlEncode(word)}")).Records
                .Where(o => o.WORD == word).ToList();
            return SetTextbook(lst, lstTextbooks);
        }

        public async Task<int> Create(MUnitWord item) =>
            (await CallSPByUrl("UNITWORDS_CREATE", item)).NewID.Value;

        public async Task UpdateSeqNum(int id, int seqnum) =>
            Debug.WriteLine(await UpdateByUrl($"UNITWORDS/{id}", $"SEQNUM={seqnum}"));

        public async Task UpdateNote(int id, string note) =>
            Debug.WriteLine(await UpdateByUrl($"UNITWORDS/{id}", $"NOTE={note}"));

        public async Task<MSPResult> Update(MUnitWord item)
        {
            var result = await CallSPByUrl("UNITWORDS_UPDATE", item);
            Debug.WriteLine(result);
            return result;
        }

        public async Task Delete(MUnitWord item) =>
            Debug.WriteLine(await CallSPByUrl("UNITWORDS_DELETE", item));
    }
}
