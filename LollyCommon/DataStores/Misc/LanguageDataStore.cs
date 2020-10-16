using System.Collections.Generic;
using System.Threading.Tasks;


namespace LollyCommon
{
    public class LanguageDataStore : LollyDataStore<MLanguage>
    {
        public async Task<List<MLanguage>> GetData() =>
        (await GetDataByUrl<MLanguages>($"LANGUAGES")).Records;
    }
}
