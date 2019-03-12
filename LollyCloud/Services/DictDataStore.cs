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
    public class DictMeanDataStore : LollyDataStore<MDictMean>
    {
        public async Task<List<MDictMean>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsMean>($"VDICTSMEAN?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSMEAN;
    }
    public class DictNoteDataStore : LollyDataStore<MDictNote>
    {
        public async Task<List<MDictNote>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsNote>($"VDICTSNOTE?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSNOTE;
    }
}
