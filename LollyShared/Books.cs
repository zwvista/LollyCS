using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static void Books_Delete(long bookid)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
                if (item == null) return;

                db.SBOOK.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Books_Insert(MBOOK row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MBOOK
                {
                    BOOKID = row.BOOKID,
                    LANGID = row.LANGID,
                    BOOKNAME = row.BOOKNAME,
                    UNITSINBOOK = row.UNITSINBOOK,
                    PARTS = row.PARTS
                };
                db.SBOOK.Add(item);
                db.SaveChanges();
            }
        }

        public static void Books_Update(MBOOK row, long original_bookid)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        UPDATE  BOOKS
                        SET BOOKID = @bookid, BOOKNAME = @bookname, UNITSINBOOK = @unitsinbook, PARTS = @parts
                        WHERE   (BOOKID = @original_bookid)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("bookid", row.BOOKID),
                    new SQLiteParameter("bookname", row.BOOKNAME),
                    new SQLiteParameter("unitsinbook", row.UNITSINBOOK),
                    new SQLiteParameter("parts", row.PARTS),
                    new SQLiteParameter("original_bookid", original_bookid));
            }
        }

        public static void Books_UpdateUnit(long unitfrom, long partfrom, long unitto, long partto, long bookid)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
                if (item == null) return;

                item.UNITFROM = unitfrom;
                item.PARTFROM = partfrom;
                item.UNITTO = unitto;
                item.PARTTO = partto;
                db.SaveChanges();
            }
        }

        public static MBOOK Books_GetDataByBook(long bookid)
        {
            using (var db = new LollyEntities())
                return db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
        }

        public static List<MBOOK> Books_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return (
                    from r in db.SBOOK
                    where r.LANGID == langid
                    orderby r.BOOKID
                    select r
                ).ToList();
        }
    }
}
