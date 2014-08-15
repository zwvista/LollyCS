using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class PicBooks
    {
        public static void Delete(string bookname)
        {
            using (var db = new Entities())
            {
                var item = db.SPICBOOK.SingleOrDefault(r => r.BOOKNAME == bookname);
                if (item == null) return;

                db.SPICBOOK.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(MPICBOOK row)
        {
            using (var db = new Entities())
            {
                var item = new MPICBOOK
                {
                    LANGID = row.LANGID,
                    BOOKNAME = row.BOOKNAME,
                    FILENAME = row.FILENAME,
                    NUMPAGES = row.NUMPAGES
                };
                db.SPICBOOK.Add(item);
                db.SaveChanges();
            }
        }

        public static void Update(MPICBOOK row, string original_bookname)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        UPDATE  PICBOOKS
                        SET BOOKNAME = @bookname, FILENAME = @filename, NUMPAGES = @numpages
                        WHERE   (BOOKNAME = @original_bookname)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("bookname", row.BOOKNAME),
                    new SqlParameter("filename", row.FILENAME),
                    new SqlParameter("numpages", row.NUMPAGES),
                    new SqlParameter("original_bookname", original_bookname));
            }
        }

        public static List<MPICBOOK> GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return db.SPICBOOK.Where(r => r.LANGID == langid).ToList();
        }
    }
}
