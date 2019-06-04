using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace LollyShared
{
    public class LanguageDataStore : LollyDataStore<MLanguage>
    {
        public async Task<List<MLanguage>> GetData() =>
        (await GetDataByUrl<MLanguages>($"LANGUAGES?filter=ID,neq,0")).records;
    }
}
