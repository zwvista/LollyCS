using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class AutoCorrect
    {
        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SAUTOCORRECT.Remove(item);
                db.SaveChanges();
            }
        }

        public static int Insert(MAUTOCORRECT row)
        {
            using (var db = new Entities())
            {
                var item = new MAUTOCORRECT
                {
                    LANGID = row.LANGID,
                    INDEX = row.INDEX,
                    INPUT = row.INPUT,
                    EXTENDED = row.EXTENDED,
                    BASIC = row.BASIC
                };
                db.SAUTOCORRECT.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void Update(MAUTOCORRECT row)
        {
            using (var db = new Entities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.LANGID = row.LANGID;
                item.INDEX = row.INDEX;
                item.INPUT = row.INPUT;
                item.EXTENDED = row.EXTENDED;
                item.BASIC = row.BASIC;
                db.SaveChanges();
            }
        }

        public static void UpdateIndex(int index, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SAUTOCORRECT.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.INDEX = index;
                db.SaveChanges();
            }
        }

        public static List<MAUTOCORRECT> GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return db.SAUTOCORRECT.Where(r => r.LANGID == langid).ToList();
        }
    }
}
