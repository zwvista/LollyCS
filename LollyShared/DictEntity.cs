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
        public static void DictEntity_Delete(string word, string dicttable)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        DELETE FROM [{0}]
                        WHERE   (WORD = @word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SQLiteParameter("word", word));
            }
        }

        public static void DictEntity_Insert(string word, string dicttable)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        INSERT INTO [{0}] (WORD)
                        VALUES   (@word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SQLiteParameter("word", word));
            }
        }

        public static void DictEntity_Update(string translation, string word, string dicttable)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        UPDATE  [{0}]
                        SET         [TRANSLATION] = @translation
                        WHERE   (WORD = @word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SQLiteParameter("translation", translation),
                    new SQLiteParameter("word", word));
            }
        }

        public static MDICTENTITY DictEntity_GetDataByWordDictTable(string word, string dicttable)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        SELECT   WORD, [TRANSLATION]
                        FROM      [{0}]
                        WHERE   (WORD = @word)
                    ";
                return db.Database.SqlQuery<MDICTENTITY>(string.Format(sql, dicttable),
                    new SQLiteParameter("word", word)).SingleOrDefault();
            }
        }
    }
}
