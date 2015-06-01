using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void DictEntity_Delete(string word, string dicttable)
        {
            var sql = $@"
                DELETE FROM [{dicttable}]
                WHERE   (WORD = @word)
            ";
            db.Execute(sql, word);
        }

        public void DictEntity_Insert(string word, string dicttable)
        {
            var sql = $@"
                INSERT INTO [{dicttable}] (WORD)
                VALUES   (@word)
            ";
            db.Execute(sql, word);
        }

        public void DictEntity_Update(string translation, string word, string dicttable)
        {
            var sql = $@"
                UPDATE  [{dicttable}]
                SET         [TRANSLATION] = @translation
                WHERE   (WORD = @word)
            ";
            db.Execute(sql, translation, word);
        }

        public MDICTENTITY DictEntity_GetDataByWordDictTable(string word, string dicttable)
        {
            var sql = $@"
                SELECT   WORD, [TRANSLATION]
                FROM      [{dicttable}]
                WHERE   (WORD = @word)
            ";
            return db.Query<MDICTENTITY>(sql, word).SingleOrDefault();
        }
    }
}
