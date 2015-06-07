using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static void AutoCorrect_Delete(long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SAUTOCORRECT.Remove(item);
                db.SaveChanges();
            }
        }

        public static long AutoCorrect_Insert(MAUTOCORRECT row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MAUTOCORRECT
                {
                    LANGID = row.LANGID,
                    ORD = row.ORD,
                    INPUT = row.INPUT,
                    EXTENDED = row.EXTENDED,
                    BASIC = row.BASIC
                };
                db.SAUTOCORRECT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void AutoCorrect_Update(MAUTOCORRECT row)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.LANGID = row.LANGID;
                item.ORD = row.ORD;
                item.INPUT = row.INPUT;
                item.EXTENDED = row.EXTENDED;
                item.BASIC = row.BASIC;
                db.SaveChanges();
            }
        }

        public static void AutoCorrect_UpdateOrd(long ord, long id)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.ORD = ord;
                db.SaveChanges();
            }
        }

        public static List<MAUTOCORRECT> AutoCorrect_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return db.SAUTOCORRECT.Where(r => r.LANGID == langid).ToList();
        }
    }
}
