using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class AutoCorrectDataStore : LollyDataStore<MAutoCorrect>
    {
        public async Task<List<MAutoCorrect>> GetDataByLang(int langid) =>
            (await GetDataByUrl<MAutoCorrects>($"AUTOCORRECT?filter=LANGID,eq,{langid}")).Records;
        public string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrect, Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
            lstAutoCorrect.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }
}
