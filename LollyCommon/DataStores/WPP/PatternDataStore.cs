﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class PatternDataStore : LollyDataStore<MPattern>
    {
        public async Task<List<MPattern>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MPatterns>($"PATTERNS?filter=LANGID,eq,{langid}&order=PATTERN")).Records;

        public async Task<List<MPattern>> GetDataById(int id) =>
        (await GetDataByUrl<MPatterns>($"PATTERNS?filter=ID,eq,{id}")).Records;

        public async Task<int> Create(MPattern item) =>
        await CreateByUrl($"PATTERNS", item);

        public async Task UpdateNote(int id, string note) =>
        Debug.WriteLine(await UpdateByUrl($"PATTERNS/{id}", $"NOTE={note}"));

        public async Task Update(MPattern item) =>
        Debug.WriteLine(await UpdateByUrl($"PATTERNS/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"PATTERNS/{id}"));

        public async Task MergePatterns(MPattern item) =>
        Debug.WriteLine(await CallSPByUrl("PATTERNS_MERGE", item));

        public async Task SplitPattern(MPattern item) =>
        Debug.WriteLine(await CallSPByUrl("PATTERNS_SPLIT", item));
    }
}
