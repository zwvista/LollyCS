using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class PhrasesUnits
    {
        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SPHRASEUNIT.Remove(item);
                db.SaveChanges();
            }
        }

        public static int Insert(MPHRASEUNIT row)
        {
            using (var db = new Entities())
            {
                var item = new MPHRASEUNIT
                {
                    BOOKID = row.BOOKID,
                    UNIT = row.UNIT,
                    PART = row.PART,
                    INDEX = row.INDEX,
                    PHRASE = row.PHRASE,
                    TRANSLATION = row.TRANSLATION
                };
                db.SPHRASEUNIT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void Update(MPHRASEUNIT row)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.UNIT = row.UNIT;
                item.PART = row.PART;
                item.INDEX = row.INDEX;
                item.PHRASE = row.PHRASE;
                item.TRANSLATION = row.TRANSLATION;
                db.SaveChanges();
            }
        }

        public static void UpdateIndex(int index, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.INDEX = index;
                db.SaveChanges();
            }
        }

        public static List<MPHRASEUNIT> GetDataByBookUnitParts(int bookid, int unitpartfrom, int unitpartto)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SPHRASEUNIT
                    let unitpart = r.UNIT * 10 + r.PART
                    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
                    orderby r.UNIT, r.PART, r.INDEX
                    select r
                ).ToList();
            }
        }
    }
}
