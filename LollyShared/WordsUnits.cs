using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static void WordsUnits_Delete(long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SWORDUNIT.Remove(item);
                db.SaveChanges();
            }
        }

        public static long WordsUnits_Insert(MWORDUNIT row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MWORDUNIT
                {
                    BOOKID = row.BOOKID,
                    UNIT = row.UNIT,
                    PART = row.PART,
                    ORD = row.ORD,
                    WORD = row.WORD,
                    NOTE = row.NOTE
                };
                db.SWORDUNIT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void WordsUnits_Update(MWORDUNIT row)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.UNIT = row.UNIT;
                item.PART = row.PART;
                item.ORD = row.ORD;
                item.WORD = row.WORD;
                item.NOTE = row.NOTE;
                db.SaveChanges();
            }
        }

        public static void WordsUnits_UpdateOrd(long ord, long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.ORD = ord;
                db.SaveChanges();
            }
        }

        public static void WordsUnits_UpdateNote(string note, long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.NOTE = note;
                db.SaveChanges();
            }
        }

        public static List<MWORDUNIT> WordsUnits_GetDataByBookUnitParts(long bookid, long unitpartfrom, long unitpartto)
        {
            using (var db = new LollyEntities())
            {
                return (
                    from r in db.SWORDUNIT
                    let unitpart = r.UNIT * 10 + r.PART
                    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
                    orderby r.UNIT, r.PART, r.ORD
                    select r
                ).ToList();
            }
        }
    }
}
