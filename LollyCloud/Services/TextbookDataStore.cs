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
    public class TextbookDataStore : LollyDataStore<MTextbook>
    {
        public async Task<IEnumerable<MTextbook>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MTextbooks>($"TEXTBOOKS?transform=1&filter=LANGID,eq,{langid}")).TEXTBOOKS;
    }
}
