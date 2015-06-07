using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static void Languages_UpdateBook(long bookid, long langid)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SLANGUAGE.SingleOrDefault(r => r.LANGID == langid);
                if (item == null) return;

                item.CURBOOKID = bookid;
                db.SaveChanges();
            }
        }

        public static MLANGUAGE Languages_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return db.SLANGUAGE.SingleOrDefault(r => r.LANGID == langid);
        }

        public static List<MLANGUAGE> Languages_GetData()
        {
            using (var db = new LollyEntities())
                return db.SLANGUAGE.ToList();
        }

        public static List<MLANGUAGE> Languages_GetDataNonChinese()
        {
            using (var db = new LollyEntities())
                return db.SLANGUAGE.Where(r => r.LANGID > 0).ToList();
        }
    }
}
