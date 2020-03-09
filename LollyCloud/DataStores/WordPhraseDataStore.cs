using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyShared
{
    public class WordPhraseDataStore : LollyDataStore<MWordPhrase>
    {
        async Task<List<MWordPhrase>> GetDataByWordPhrase(int wordid, int phraseid) =>
        (await GetDataByUrl<MWordsPhrases>($"WORDSPHRASES?filter=WORDID,eq,{wordid}&filter=PHRASEID,eq,{phraseid}")).records;

        async Task<int> Create(MWordPhrase item) =>
        await CreateByUrl($"WORDSPHRASES", item);

        async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"WORDSPHRASES/{id}"));

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

        public async Task<List<MLangPhrase>> GetPhrasesByWord(int wordid) =>
        (await GetDataByUrl<MLangPhrases>($"VPHRASESWORD?filter=WORDID,eq,{wordid}")).records;

        public async Task<List<MLangWord>> GetWordsByPhrase(int phraseid) =>
        (await GetDataByUrl<MLangWords>($"VPHRASESWORD?filter=PHRASEID,eq,{phraseid}")).records;
    }
}
