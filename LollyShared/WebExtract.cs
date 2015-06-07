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
        public static void WebExtract_Delete(string sitename)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWEBEXTRACT.SingleOrDefault(r => r.SITENAME == sitename);
                if (item == null) return;

                db.SWEBEXTRACT.Remove(item);
                db.SaveChanges();
            }
        }

        public static void WebExtract_Insert(MWEBEXTRACT row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MWEBEXTRACT
                {
                    SITENAME = row.SITENAME,
                    TRANSFORM_WIN = row.TRANSFORM_WIN,
                    TRANSFORM_MAC = row.TRANSFORM_MAC,
                    WAIT = row.WAIT
                };
                db.SWEBEXTRACT.Add(item);
                db.SaveChanges();
            }
        }

        public static void WebExtract_Update(MWEBEXTRACT row, string original_sitename)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                    UPDATE  WEBEXTRACT
                    SET SITENAME = @sitename, TRANSFORM_WIN = @transform_win, TRANSFORM_MAC = @transfrom_mac, WAIT = @wait
                    WHERE   (SITENAME = @original_sitename)
                ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("sitename", row.SITENAME),
                    new SQLiteParameter("transform_win", row.TRANSFORM_WIN ?? (object)DBNull.Value),
                    new SQLiteParameter("transfrom_mac", row.TRANSFORM_MAC ?? (object)DBNull.Value),
                    new SQLiteParameter("wait", row.WAIT),
                    new SQLiteParameter("original_sitename", original_sitename));
            }
        }

        public static List<MWEBEXTRACT> WebExtract_GetData()
        {
            using (var db = new LollyEntities())
            {
                return db.SWEBEXTRACT.OrderBy(r => r.SITENAME).ToList();
            }
        }
    }
}
