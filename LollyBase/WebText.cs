using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WebText
    {
        public static void Delete(string sitename)
        {
            using (var db = new Entities())
            {
                var item = db.SWEBTEXT.SingleOrDefault(r => r.SITENAME == sitename);
                if (item == null) return;

                db.SWEBTEXT.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(MWEBTEXT row)
        {
            using (var db = new Entities())
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

        public static void Update(MWEBTEXT row, string original_sitename)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        UPDATE  WEBTEXT
                        SET SITENAME = @sitename, URL = @url, TEMPLATE = @template, FOLDER = @folder
                        WHERE   (SITENAME = @original_sitename)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("sitename", row.SITENAME),
                    new SqlParameter("url", row.URL),
                    new SqlParameter("template", row.TEMPLATE),
                    new SqlParameter("folder", row.FOLDER),
                    new SqlParameter("original_sitename", original_sitename));
            }
        }

        public static List<MWEBTEXT> GetData()
        {
            using (var db = new Entities())
            {
                return db.SWEBTEXT.ToList();
            }
        }
    }
}
