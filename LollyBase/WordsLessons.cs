using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WordsUnits
    {
        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SWORDUNIT.Remove(item);
                db.SaveChanges();
            }
        }

        public static int Insert(MWORDUNIT row)
        {
            using (var db = new Entities())
            {
                var item = new MWORDUNIT
                {
                    BOOKID = row.BOOKID,
                    UNIT = row.UNIT,
                    PART = row.PART,
                    INDEX = row.INDEX,
                    WORD = row.WORD,
                    NOTE = row.NOTE
                };
                db.SWORDUNIT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void Update(MWORDUNIT row)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.UNIT = row.UNIT;
                item.PART = row.PART;
                item.INDEX = row.INDEX;
                item.WORD = row.WORD;
                item.NOTE = row.NOTE;
                db.SaveChanges();
            }
        }

        public static void UpdateIndex(int index, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.INDEX = index;
                db.SaveChanges();
            }
        }

        public static void UpdateNote(string note, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.NOTE = note;
                db.SaveChanges();
            }
        }

        public static List<MWORDUNIT> GetDataByBookUnitParts(int bookid, int unitpartfrom, int unitpartto)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SWORDUNIT
                    let unitpart = r.UNIT * 10 + r.PART
                    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
                    orderby r.UNIT, r.PART, r.INDEX
                    select r
                ).ToList();
            }
        }
    }
}
