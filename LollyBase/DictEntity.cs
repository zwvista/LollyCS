using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class DictEntity
    {
        public static void Delete(string word, string dicttable)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        DELETE FROM [{0}]
                        WHERE   (WORD = @word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SqlParameter("word", word));
            }
        }

        public static void Insert(string word, string dicttable)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        INSERT INTO [{0}] (WORD)
                        VALUES   (@word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SqlParameter("word", word));
            }
        }

        public static void Update(string translation, string word, string dicttable)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        UPDATE  [{0}]
                        SET         [TRANSLATION] = @translation
                        WHERE   (WORD = @word)
                    ";
                db.Database.ExecuteSqlCommand(string.Format(sql, dicttable),
                    new SqlParameter("translation", translation),
                    new SqlParameter("word", word));
            }
        }

        public static MDICTENTITY GetDataByWordDictTable(string word, string dicttable)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        SELECT   WORD, [TRANSLATION]
                        FROM      [{0}]
                        WHERE   (WORD = @word)
                    ";
                return db.Database.SqlQuery<MDICTENTITY>(string.Format(sql, dicttable),
                    new SqlParameter("word", word)).SingleOrDefault();
            }
        }
    }
}
