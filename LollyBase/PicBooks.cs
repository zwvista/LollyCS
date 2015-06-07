using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static void PicBooks_Delete(string bookname)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SPICBOOK.SingleOrDefault(r => r.BOOKNAME == bookname);
                if (item == null) return;

                db.SPICBOOK.Remove(item);
                db.SaveChanges();
            }
        }

        public static void PicBooks_Insert(MPICBOOK row)
        {
            using (var db = new LollyEntities())
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

        public static void PicBooks_Update(MPICBOOK row, string original_bookname)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        UPDATE  PICBOOKS
                        SET BOOKNAME = @bookname, FILENAME = @filename, NUMPAGES = @numpages
                        WHERE   (BOOKNAME = @original_bookname)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("bookname", row.BOOKNAME),
                    new SQLiteParameter("filename", row.FILENAME),
                    new SQLiteParameter("numpages", row.NUMPAGES),
                    new SQLiteParameter("original_bookname", original_bookname));
            }
        }

        public static List<MPICBOOK> PicBooks_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return db.SPICBOOK.Where(r => r.LANGID == langid).ToList();
        }
    }
}
