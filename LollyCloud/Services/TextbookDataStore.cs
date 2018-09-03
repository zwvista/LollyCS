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
    public class TextbookDataStore : LollyDataStore<Language>
    {
        public async Task<IEnumerable<Textbook>> GetDataByLang(int langid) =>
        (await GetDataByUrl<Textbooks>($"TEXTBOOKS?transform=1&filter=LANGID,eq,{langid}")).TEXTBOOKS;
    }
}
