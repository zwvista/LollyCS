using System.Collections.Generic;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class VoiceDataStore : LollyDataStore<MVoice>
    {
        public async Task<List<MVoice>> GetDataByLang(int langid, int idVoiceType) =>
            (await GetDataByUrl<MVoices>($"VVOICES?filter=LANGID,eq,{langid}&filter=VOICETYPEID,eq,{idVoiceType}")).Records;
    }
}
