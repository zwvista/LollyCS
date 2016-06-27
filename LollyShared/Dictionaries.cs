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
        public static void Dictionaries_Delete(long langid, string dictname)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SDICTIONARY.SingleOrDefault(r => r.LANGID == langid && r.DICTNAME == dictname);
                if (item == null) return;

                db.SDICTIONARY.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Dictionaries_Insert(MDICTIONARY row)
        {
            using (var db = new LollyEntities())
            {
                var item = new MDICTIONARY
                {
                    LANGID = row.LANGID,
                    SEQNUM = row.SEQNUM,
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

        public static void Dictionaries_Update(MDICTIONARY row, string original_dictname)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                    UPDATE  DICTIONARIES
                    SET SEQNUM = @ord, DICTTYPEID = @dicttypeid, DICTNAME = @dictname, LANGIDTO = @langidto,
                        URL = @url, CHCONV = @chconv, AUTOMATION = @automation, AUTOJUMP = @autojump, DICTTABLE = @dicttable, TEMPLATE = @template
                    WHERE   (LANGID = @langid) AND (DICTNAME = @original_dictname)
                ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("ord", row.SEQNUM),
                    new SQLiteParameter("dicttypeid", row.DICTTYPEID),
                    new SQLiteParameter("dictname", row.DICTNAME),
                    new SQLiteParameter("langidto", row.LANGIDTO),
                    new SQLiteParameter("url", row.URL ?? (object)DBNull.Value),
                    new SQLiteParameter("chconv", row.CHCONV ?? (object)DBNull.Value),
                    new SQLiteParameter("automation", row.AUTOMATION ?? (object)DBNull.Value),
                    new SQLiteParameter("autojump", row.AUTOJUMP),
                    new SQLiteParameter("dicttable", row.DICTTABLE ?? (object)DBNull.Value),
                    new SQLiteParameter("template", row.TEMPLATE ?? (object)DBNull.Value),
                    new SQLiteParameter("langid", row.LANGID),
                    new SQLiteParameter("original_dictname", original_dictname));
            }
        }

        public static List<MDICTIONARY> Dictionaries_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return db.SDICTIONARY.Where(r => r.LANGID == langid).ToList();
        }
    }
}
