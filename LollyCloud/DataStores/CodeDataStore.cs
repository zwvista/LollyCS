using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyShared
{
    public class CodeDataStore : LollyDataStore<MCode>
    {
        public async Task<List<MCode>> GetDictTypes() =>
        (await GetDataByUrl<MCodes>($"CODES?filter=KIND,eq,1")).records;
        public async Task<List<MCode>> GetReadNumberType() =>
        (await GetDataByUrl<MCodes>($"CODES?filter=KIND,eq,3")).records;
    }
}
