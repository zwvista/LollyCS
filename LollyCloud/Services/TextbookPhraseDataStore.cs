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
    public class TextbookPhraseDataStore : LollyDataStore<MTextbookPhrase>
    {
        public async Task<IEnumerable<MTextbookPhrase>> GetDataByTextbookUnitPart(int langid) =>
        (await GetDataByUrl<MTextbookPhrase>($"VTEXTBOOKPHRASES?transform=1&filter[]=LANGID,eq,{langid}&&order[]=TEXTBOOKID&order[]=UNIT&order[]=PART&order[]=SEQNUM")).VTEXTBOOKPHRASES;
    }
}
