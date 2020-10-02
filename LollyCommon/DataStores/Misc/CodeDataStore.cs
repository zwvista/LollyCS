using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class CodeDataStore : LollyDataStore<MCode>
    {
        public async Task<List<MCode>> GetDictCodes() =>
        (await GetDataByUrl<MCodes>($"CODES?filter=KIND,eq,1")).Records;
        public async Task<List<MCode>> GetReadNumberCodes() =>
        (await GetDataByUrl<MCodes>($"CODES?filter=KIND,eq,3")).Records;
    }
}
