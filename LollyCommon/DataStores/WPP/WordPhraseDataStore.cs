using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class WordPhraseDataStore : LollyDataStore<MWordPhrase>
    {
        async Task<List<MWordPhrase>> GetDataByWordPhrase(int wordid, int phraseid) =>
        (await GetDataByUrl<MWordsPhrases>($"WORDSPHRASES?filter=WORDID,eq,{wordid}&filter=PHRASEID,eq,{phraseid}")).Records;

        public async Task<List<MLangPhrase>> GetPhrasesByWordId(int wordid) =>
        (await GetDataByUrl<MLangPhrases>($"VPHRASESWORD?filter=WORDID,eq,{wordid}")).Records;

        public async Task<List<MLangWord>> GetWordsByPhraseId(int phraseid) =>
        (await GetDataByUrl<MLangWords>($"VWORDSPHRASE?filter=PHRASEID,eq,{phraseid}")).Records;

        async Task<int> Create(MWordPhrase item) =>
        await CreateByUrl($"WORDSPHRASES", item);

        async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WORDSPHRASES/{id}"));

        public async Task DeleteByWordId(int wordid)
        {
            var lst = await GetPhrasesByWordId(wordid);
            if (lst.IsEmpty()) return;
            var ids = string.Join(",", lst.Select(o => o.ID));
            Debug.WriteLine(await DeleteByUrl($"WORDSPHRASES/{ids}"));
        }

        public async Task Connect(int wordid, int phraseid)
        {
            var lst = await GetDataByWordPhrase(wordid, phraseid);
            if (lst.Any()) return;
            var item = new MWordPhrase
            {
                WORDID = wordid,
                PHRASEID = phraseid
            };
            Debug.WriteLine(Create(item));
        }

        public async Task Disconnect(int wordid, int phraseid)
        {
            var lst = await GetDataByWordPhrase(wordid, phraseid);
            foreach (var o in lst)
                await Delete(o.ID);
        }
    }
}
