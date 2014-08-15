using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class Dictionaries
    {
        public static void Delete(int langid, string dictname)
        {
            using (var db = new Entities())
            {
                var item = db.SDICTIONARY.SingleOrDefault(r => r.LANGID == langid && r.DICTNAME == dictname);
                if (item == null) return;

                db.SDICTIONARY.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(MDICTIONARY row)
        {
            using (var db = new Entities())
            {
                var item = new MDICTIONARY
                {
                    LANGID = row.LANGID,
                    INDEX = row.INDEX,
                    DICTTYPEID = row.DICTTYPEID,
                    DICTNAME = row.DICTNAME,
                    LANGIDTO = row.LANGIDTO,
                    URL = row.URL,
                    CHCONV = row.CHCONV,
                    AUTOMATION = row.AUTOMATION,
                    AUTOJUMP = row.AUTOJUMP,
                    DICTTABLE = row.DICTTABLE,
                    TEMPLATE = row.TEMPLATE
                };
                db.SDICTIONARY.Add(item);
                db.SaveChanges();
            }
        }

        public static void Update(MDICTIONARY row, string original_dictname)
        {
            using (var db = new Entities())
            {
                var sql = @"
                    UPDATE  DICTIONARIES
                    SET [INDEX] = @index, DICTTYPEID = @dicttypeid, DICTNAME = @dictname, LANGIDTO = @langidto,
                        URL = @url, CHCONV = @chconv, AUTOMATION = @automation, AUTOJUMP = @autojump, DICTTABLE = @dicttable, TEMPLATE = @template
                    WHERE   (LANGID = @langid) AND (DICTNAME = @original_dictname)
                ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("index", row.INDEX),
                    new SqlParameter("dicttypeid", row.DICTTYPEID),
                    new SqlParameter("dictname", row.DICTNAME),
                    new SqlParameter("langidto", row.LANGIDTO),
                    new SqlParameter("url", row.URL ?? (object)DBNull.Value),
                    new SqlParameter("chconv", row.CHCONV ?? (object)DBNull.Value),
                    new SqlParameter("automation", row.AUTOMATION ?? (object)DBNull.Value),
                    new SqlParameter("autojump", row.AUTOJUMP),
                    new SqlParameter("dicttable", row.DICTTABLE ?? (object)DBNull.Value),
                    new SqlParameter("template", row.TEMPLATE ?? (object)DBNull.Value),
                    new SqlParameter("langid", row.LANGID),
                    new SqlParameter("original_dictname", original_dictname));
            }
        }

        public static List<MDICTIONARY> GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return db.SDICTIONARY.Where(r => r.LANGID == langid).ToList();
        }
    }
}
