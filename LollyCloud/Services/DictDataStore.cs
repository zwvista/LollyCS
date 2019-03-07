using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyXamarinNative
{
    public class DictOnlineDataStore : LollyDataStore<MDictOnline>
    {
        public async Task<IEnumerable<MDictOnline>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MDictsOnline>($"VDICTSONLINE?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSONLINE;
    }
    public class DictNoteDataStore : LollyDataStore<MDictNote>
    {
        public async Task<IEnumerable<MDictNote>> GetDataByLang(int langid) =>
        (await GetDataByUrl<DictsNote>($"VDICTSNOTE?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSNOTE;
    }
}
