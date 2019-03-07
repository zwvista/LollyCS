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
    public class AutoCorrectDataStore : LollyDataStore<MAutoCorrect>
    {
        public async Task<IEnumerable<MAutoCorrect>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MAutoCorrects>($"AUTOCORRECT?transform=1&filter=LANGID,eq,{langid}")).AUTOCORRECT;
    }
}
