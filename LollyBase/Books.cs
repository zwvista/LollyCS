using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class Books
    {
        public static void Delete(int bookid)
        {
            using (var db = new Entities())
            {
                var item = db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
                if (item == null) return;

                db.SBOOK.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(MBOOK row)
        {
            using (var db = new Entities())
            {
                var item = new MBOOK
                {
                    BOOKID = row.BOOKID,
                    LANGID = row.LANGID,
                    BOOKNAME = row.BOOKNAME,
                    NUMLESSONS = row.NUMLESSONS,
                    PARTS = row.PARTS
                };
                db.SBOOK.Add(item);
                db.SaveChanges();
            }
        }

        public static void Update(MBOOK row, int original_bookid)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        UPDATE  BOOKS
                        SET BOOKID = @bookid, BOOKNAME = @bookname, NUMLESSONS = @numlessons, PARTS = @parts
                        WHERE   (BOOKID = @original_bookid)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("bookid", row.BOOKID),
                    new SqlParameter("bookname", row.BOOKNAME),
                    new SqlParameter("numlessons", row.NUMLESSONS),
                    new SqlParameter("parts", row.PARTS),
                    new SqlParameter("original_bookid", original_bookid));
            }
        }

        public static void UpdateLesson(int lessonfrom, int partfrom, int lessonto, int partto, int bookid)
        {
            using (var db = new Entities())
            {
                var item = db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
                if (item == null) return;

                item.LESSONFROM = lessonfrom;
                item.PARTFROM = partfrom;
                item.LESSONTO = lessonto;
                item.PARTTO = partto;
                db.SaveChanges();
            }
        }

        public static MBOOK GetDataByBook(int bookid)
        {
            using (var db = new Entities())
                return db.SBOOK.SingleOrDefault(r => r.BOOKID == bookid);
        }

        public static List<MBOOK> GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return (
                    from r in db.SBOOK
                    where r.LANGID == langid
                    orderby r.BOOKID
                    select r
                ).ToList();
        }
    }
}
