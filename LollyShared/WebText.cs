using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public partial class LollyDB
    {
        public static void WebText_Delete(string sitename)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWEBTEXT.SingleOrDefault(r => r.SITENAME == sitename);
                if (item == null) return;

                db.SWEBTEXT.Remove(item);
                db.SaveChanges();
            }
        }

        public static void WebText_Insert(MWEBTEXT row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MWEBTEXT
                {
                    SITENAME = row.SITENAME,
                    URL = row.URL,
                    TEMPLATE = row.TEMPLATE,
                    FOLDER = row.FOLDER
                };
                db.SWEBTEXT.Add(item);
                db.SaveChanges();
            }
        }

        public static void WebText_Update(MWEBTEXT row, string original_sitename)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        UPDATE  WEBTEXT
                        SET SITENAME = @sitename, URL = @url, TEMPLATE = @template, FOLDER = @folder
                        WHERE   (SITENAME = @original_sitename)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("sitename", row.SITENAME),
                    new SQLiteParameter("url", row.URL),
                    new SQLiteParameter("template", row.TEMPLATE),
                    new SQLiteParameter("folder", row.FOLDER),
                    new SQLiteParameter("original_sitename", original_sitename));
            }
        }

        public static List<MWEBTEXT> WebText_GetData()
        {
            using (var db = new LollyEntities())
            {
                return db.SWEBTEXT.ToList();
            }
        }
    }
}
