using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static void PhrasesUnits_Delete(long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SPHRASEUNIT.Remove(item);
                db.SaveChanges();
            }
        }

        public static void PhrasesUnits_Get(MPHRASELANG row)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                row.BOOKID = item.BOOKID;
                row.UNIT = item.UNIT;
                row.PART = item.PART;
                row.SEQNUM = item.SEQNUM;
                row.PHRASE = item.PHRASE;
                row.TRANSLATION = item.TRANSLATION;
            }
        }

        public static long PhrasesUnits_Insert(MPHRASEUNIT row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MPHRASEUNIT
                {
                    BOOKID = row.BOOKID,
                    UNIT = row.UNIT,
                    PART = row.PART,
                    SEQNUM = row.SEQNUM,
                    PHRASE = row.PHRASE,
                    TRANSLATION = row.TRANSLATION
                };
                db.SPHRASEUNIT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void PhrasesUnits_Update(MPHRASEUNIT row)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.UNIT = row.UNIT;
                item.PART = row.PART;
                item.SEQNUM = row.SEQNUM;
                item.PHRASE = row.PHRASE;
                item.TRANSLATION = row.TRANSLATION;
                db.SaveChanges();
            }
        }

        public static void PhrasesUnits_Update(MPHRASELANG row)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.UNIT = row.UNIT;
                item.PART = row.PART;
                item.SEQNUM = row.SEQNUM;
                item.PHRASE = row.PHRASE;
                item.TRANSLATION = row.TRANSLATION;
                db.SaveChanges();
            }
        }

        public static void PhrasesUnits_UpdateOrd(long ord, long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPHRASEUNIT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.SEQNUM = ord;
                db.SaveChanges();
            }
        }

        public static List<MPHRASEUNIT> PhrasesUnits_GetDataByBookUnitParts(long bookid, long unitpartfrom, long unitpartto)
        {
            using (var db = new LollyEntities())
            {
                return (
                    from r in db.SPHRASEUNIT
                    let unitpart = r.UNIT * 10 + r.PART
                    where r.BOOKID == bookid && unitpart >= unitpartfrom && unitpart <= unitpartto
                    orderby r.UNIT, r.PART, r.SEQNUM
                    select r
                ).ToList();
            }
        }
    }
}
