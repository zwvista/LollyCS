using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void PhrasesUnits_Delete(int id) =>
            db.Delete<MPHRASEUNIT>(id);

        public void PhrasesUnits_Insert(MPHRASEUNIT row) =>
            db.Insert(row);

        public void PhrasesUnits_Update(MPHRASEUNIT row) =>
            db.Update(row);


        public void PhrasesUnits_UpdateOrd(int ord, int id)
        {
            var sql = @"
                UPDATE  PHRASES
                SET ORD = @ord
                WHERE (id = @id)
            ";
            db.Execute(sql, ord, id);
        }

        public List<MPHRASEUNIT> PhrasesUnits_GetDataByBookUnitParts(int bookid, int unitpartfrom, int unitpartto)
        {
            //return (
            //    from r in db.Table<MPHRASEUNIT>()
            //    let unitpart = r.UNIT * 10 + r.PART
            //    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
            //    orderby r.UNIT, r.PART, r.ORD
            //    select r
            //).ToList();
            var sql = @"
                SELECT *
                FROM PHRASES
                WHERE BOOKID = @bookid AND UNIT * 10 + PART >= @unitpartfrom AND UNIT * 10 + PART <= @unitpartto
                ORDER BY UNIT, PART, ORD
            ";
            return db.Query<MPHRASEUNIT>(sql, bookid, unitpartfrom, unitpartto);
        }
    }
}
