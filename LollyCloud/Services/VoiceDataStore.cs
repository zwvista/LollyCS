using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyShared
{
    public class VoiceDataStore : LollyDataStore<MVoice>
    {
        public async Task<List<MVoice>> getDataByLang(int langid) =>
        (await GetDataByUrl<MVoices>($"VVOICES?transform=1&filter=LANGID,eq,{langid}")).VVOICES;
    }
}
