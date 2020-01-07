﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace LollyShared
{
    public class PatternPhraseDataStore : LollyDataStore<MPatternPhrase>
    {
        public async Task<List<MPatternPhrase>> GetDataByPatternId(int patternid) =>
        (await GetDataByUrl<MPatternPhrases>($"VPATTERNSPHRASES?filter=PATTERNID,eq,{patternid}")).records;

        public async Task<List<MPatternPhrase>> GetDataByPatternIdPhraseId(int patternid, int phraseid) =>
        (await GetDataByUrl<MPatternPhrases>($"VPATTERNSPHRASES?filter=PATTERNID,eq,{patternid}&filter=PHRASEID,eq,{phraseid}")).records;

        public async Task<List<MPatternPhrase>> GetDataByPhraseId(int phraseid) =>
        (await GetDataByUrl<MPatternPhrases>($"VPATTERNSPHRASES?filter=PHRASEID,eq,{phraseid}")).records;

        public async Task<List<MPatternPhrase>> GetDataBy(int id) =>
        (await GetDataByUrl<MPatternPhrases>($"VPATTERNSPHRASES?filter=ID,eq,{id}")).records;

        public async Task<int> Create(MPatternPhrase item) =>
        await CreateByUrl($"PATTERNSPHRASES", item);

        public async Task Update(MPattern item) =>
        Debug.WriteLine(await UpdateByUrl($"PATTERNSPHRASES/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"PATTERNCreateSPHRASES/{id}"));
        public async Task DeleteByPhraseId(int phraseid)
        {
            var items = await GetDataByPhraseId(phraseid);
            if (items.IsEmpty()) return;
            var ids = string.Join(",", items.Select(o => o.ID.ToString()));
            Debug.WriteLine(await DeleteByUrl($"PATTERNSPHRASES/{ids}"));
        }
        public async Task Connect(int patternid, int phraseid)
        {
            var items = await GetDataByPatternIdPhraseId(patternid, phraseid);
            if (items.Any()) return;
            int n = (await GetDataByPatternId(patternid)).Count + 1;
            var item = new MPatternPhrase
            {
                PATTERNID = patternid,
                PHRASEID = phraseid,
                SEQNUM = n
            };
            Debug.WriteLine(await Create(item));
        }
        public async Task Disconnect(int patternid, int phraseid)
        {
            var items = await GetDataByPatternIdPhraseId(patternid, phraseid);
            foreach (var item in items)
                await Delete(item.ID);
        }
    }
}