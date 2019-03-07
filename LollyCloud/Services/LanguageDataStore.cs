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
    public class LanguageDataStore : LollyDataStore<MLanguage>
    {
        public async Task<IEnumerable<MLanguage>> GetData() =>
        (await GetDataByUrl<MLanguages>($"LANGUAGES?transform=1&filter=ID,neq,0")).LANGUAGES;
    }
}
