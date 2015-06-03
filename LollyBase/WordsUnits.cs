using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void WordsUnits_Delete(int id) =>
            db.Delete<MWORDUNIT>(id);

        public void WordsUnits_Insert(MWORDUNIT row) =>
            db.Insert(row);

        public void WordsUnits_Update(MWORDUNIT row) =>
            db.Update(row);

        public void WordsUnits_UpdateOrd(int ord, int id)
        {
            var sql = @"
                UPDATE  WORDSBOOK
                SET ORD = @ord
                WHERE (id = @id)
            ";
            db.Execute(sql, ord, id);
        }

        public void WordsUnits_UpdateNote(string note, int id)
        {
            var sql = @"
                UPDATE  WORDSBOOK
                SET NOTE = @note
                WHERE (id = @id)
            ";
            db.Execute(sql, note, id);
        }

        public List<MWORDUNIT> WordsUnits_GetDataByBookUnitParts(int bookid, int unitpartfrom, int unitpartto)
        {
            //return (
            //    from r in db.Table<MWORDUNIT>()
            //    let unitpart = r.UNIT * 10 + r.PART
            //    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
            //    orderby r.UNIT, r.PART, r.ORD
            //    select r
            //).ToList();
            var sql = @"
                SELECT *
                FROM WORDSBOOK
                WHERE BOOKID = @bookid AND UNIT * 10 + PART >= @unitpartfrom AND UNIT * 10 + PART <= @unitpartto
                ORDER BY UNIT, PART, ORD
            ";
            return db.Query<MWORDUNIT>(sql, bookid, unitpartfrom, unitpartto);
        }
    }
}
