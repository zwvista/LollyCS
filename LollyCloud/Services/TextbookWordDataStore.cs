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
    public class TextbookWordDataStore : LollyDataStore<MTextbookWord>
    {
        public async Task<IEnumerable<MTextbookWord>> GetDataByLang(int langid) =>
        (await GetDataByUrl<MTextbookWords>($"VTEXTBOOKWORDS?transform=1&filter=LANGID,eq,{langid}&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VTEXTBOOKWORDS;
    }
}
