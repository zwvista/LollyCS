using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static List<MWORDLANGORBOOK> WordsLangOrBook_GetDataByBookUnitParts(long bookid, long unitpartfrom, long unitpartto)
        {
            using (var db = new LollyEntities())
            {
                return (
                    from r in db.SWORDUNIT
                    let unitpart = r.UNIT * 10 + r.PART
                    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
                    select r.WORD
                ).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
            }
        }

        public static List<MWORDLANGORBOOK> WordsLangOrBook_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
            {
                return (
                    from r in db.SWORDLANG
                    where r.LANGID == langid
                    select r.WORD
                ).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
            }
        }
    }
}
