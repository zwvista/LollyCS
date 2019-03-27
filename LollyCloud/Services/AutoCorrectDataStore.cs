using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LollyShared
{
    public class AutoCorrectDataStore : LollyDataStore<MAutoCorrect>
    {
        public async Task<List<MAutoCorrect>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MAutoCorrects>($"AUTOCORRECT?transform=1&filter=LANGID,eq,{langid}")).AUTOCORRECT;
        public string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrect, Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
            lstAutoCorrect.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }
}
