using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class DictOnlineDataStore : LollyDataStore<DictOnline>
    {
        public async Task<IEnumerable<DictOnline>> GetDataByLang(int langid) =>
        (await GetDataByUrl<DictsOnline>($"VDICTSONLINE?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSONLINE;
    }
    public class DictNoteDataStore : LollyDataStore<DictNote>
    {
        public async Task<IEnumerable<DictNote>> GetDataByLang(int langid) =>
        (await GetDataByUrl<DictsNote>($"VDICTSNOTE?transform=1&filter=LANGIDFROM,eq,{langid}")).VDICTSNOTE;
    }
}
