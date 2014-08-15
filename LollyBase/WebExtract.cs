using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WebExtract
    {
        public static void Delete(string sitename)
        {
            using (var db = new Entities())
            {
                var item = db.SWEBEXTRACT.SingleOrDefault(r => r.SITENAME == sitename);
                if (item == null) return;

                db.SWEBEXTRACT.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(MWEBEXTRACT row)
        {
            using (var db = new Entities())
            {
                var item = new MWEBEXTRACT
                {
                    SITENAME = row.SITENAME,
                    TRANSFORM_WIN = row.TRANSFORM_WIN,
                    TRANSFORM_MAC = row.TRANSFORM_MAC,
                    WAIT = row.WAIT,
                    BODY = row.BODY
                };
                db.SWEBEXTRACT.Add(item);
                db.SaveChanges();
            }
        }

        public static void Update(MWEBEXTRACT row, string original_sitename)
        {
            using (var db = new Entities())
            {
                var sql = @"
                    UPDATE  WEBEXTRACT
                    SET SITENAME = @sitename, TRANSFORM_WIN = @transform_win, TRANSFORM_MAC = @transfrom_mac, WAIT = @wait, BODY = @body
                    WHERE   (SITENAME = @original_sitename)
                ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("sitename", row.SITENAME),
                    new SqlParameter("transform_win", row.TRANSFORM_WIN ?? (object)DBNull.Value),
                    new SqlParameter("transfrom_mac", row.TRANSFORM_MAC ?? (object)DBNull.Value),
                    new SqlParameter("wait", row.WAIT),
                    new SqlParameter("body", row.BODY),
                    new SqlParameter("original_sitename", original_sitename));
            }
        }

        public static List<MWEBEXTRACT> GetData()
        {
            using (var db = new Entities())
            {
                return db.SWEBEXTRACT.ToList();
            }
        }
    }
}
