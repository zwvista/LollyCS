using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public List<MWORDLANGORBOOK> WordsLangOrBook_GetDataByBookUnitParts(int bookid, int unitpartfrom, int unitpartto)
        {
            //return (
            //    from r in db.Table<MWORDUNIT>()
            //    let unitpart = r.UNIT * 10 + r.PART
            //    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
            //    select r.WORD
            //).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
            var sql = @"
                SELECT WORD
                FROM WORDSBOOK
                WHERE BOOKID = @bookid AND UNIT * 10 + PART >= @unitpartfrom AND UNIT * 10 + PART <= @unitpartto
            ";
            return db.Query<MWORDLANGORBOOK>(sql, bookid, unitpartfrom, unitpartto);
        }

        public List<MWORDLANGORBOOK> WordsLangOrBook_GetDataByLang(int langid) =>
        //(
        //    from r in db.Table<MWORDLANG>()
        //    where r.LANGID == langid
        //    select r.WORD
        //).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
        db.Table<MWORDLANG>().Where(r => r.LANGID == langid)
            .Select(r => new MWORDLANGORBOOK { WORD = r.WORD }).ToList();
    }
}
