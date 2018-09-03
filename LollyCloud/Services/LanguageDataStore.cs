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
    public class LanguageDataStore : LollyDataStore<Language>
    {
        public async Task<IEnumerable<Language>> GetData() =>
        (await GetDataByUrl<Languages>($"LANGUAGES?transform=1&filter=ID,neq,0")).LANGUAGES;
    }
}
