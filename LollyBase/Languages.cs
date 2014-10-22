using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class Languages
    {
        public static void UpdateBook(int bookid, int langid)
        {
            using (var db = new Entities())
            {
                var item = db.SLANGUAGE.SingleOrDefault(r => r.LANGID == langid);
                if (item == null) return;

                item.CURBOOKID = bookid;
                db.SaveChanges();
            }
        }

        public static MLANGUAGE GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return db.SLANGUAGE.SingleOrDefault(r => r.LANGID == langid);
        }

        public static List<MLANGUAGE> GetData()
        {
            using (var db = new Entities())
                return db.SLANGUAGE.Where(r => r.LANGID > 0).ToList();
        }
    }
}
