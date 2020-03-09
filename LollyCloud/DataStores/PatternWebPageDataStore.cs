using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

namespace LollyShared
{
    public class PatternWebPageDataStore : LollyDataStore<MPatternWebPage>
    {
        public async Task<List<MPatternWebPage>> GetDataByPattern(int patternid) =>
        (await GetDataByUrl<MPatternWebPages>($"VPATTERNSWEBPAGES?filter=PATTERNID,eq,{patternid}")).records;

        public async Task<List<MPatternWebPage>> GetDataById(int id) =>
        (await GetDataByUrl<MPatternWebPages>($"VPATTERNSWEBPAGES?filter=ID,eq,{id}")).records;

        public async Task<int> Create(MPatternWebPage item) =>
        await CreateByUrl($"PATTERNSWEBPAGES", item);

        public async Task UpdateSeqNum(int id, int seqnum) =>
        Debug.WriteLine(await UpdateByUrl($"PATTERNSWEBPAGES/{id}", $"SEQNUM={seqnum}"));

        public async Task Update(MPatternWebPage item) =>
        Debug.WriteLine(await UpdateByUrl($"PATTERNSWEBPAGES/{item.ID}", JsonConvert.SerializeObject(item)));

        public async Task Delete(int id) =>
        Debug.WriteLine(await DeleteByUrl($"PATTERNSWEBPAGES/{id}"));
    }
}
