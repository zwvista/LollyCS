using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class VoiceDataStore : LollyDataStore<MVoice>
    {
        public async Task<List<MVoice>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MVoices>($"VVOICES?filter=LANGID,eq,{langid}")).Records;
    }
}
