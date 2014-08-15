using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WordsLang
    {
        public static void Delete(int langid, string word)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLANG.SingleOrDefault(r => r.LANGID == langid && r.WORD == word);
                if (item == null) return;

                db.SWORDLANG.Remove(item);
                db.SaveChanges();
            }
        }

        public static void Insert(int langid, string word)
        {
            using (var db = new Entities())
            {
                var item = new MWORDLANG
                {
                    LANGID = langid,
                    WORD = word,
                    LEVEL = 0
                };
                db.SWORDLANG.Add(item);
                db.SaveChanges();
            }
        }

        public static void Update(string new_word, int langid, string original_word)
        {
            using (var db = new Entities())
            {
                var sql = @"
                        UPDATE WORDSLANG
                        SET WORD = @new_word
                        WHERE (LANGID = @langid) AND (WORD = @original_word)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("new_word", new_word),
                    new SqlParameter("langid", langid),
                    new SqlParameter("original_word", original_word));

                // Can't update DB using the following code, for 'word' is part of the key
                //var item = db.SWORDLANG.SingleOrDefault(r => r.LANGID == langid && r.WORD == original_word);
                //if (item != null)
                //{
                //    item.WORD = new_word;
                //    db.SaveChanges();
                //}
            }
        }

        public static List<MWORDLANG> GetDataByLangWord(int langid, string word)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SWORDLANG
                    where r.LANGID == langid && r.WORD.Contains(word)
                    orderby r.WORD
                    select r
                ).ToList();
            }
        }

        public static List<MWORDLANG> GetDataByLangTranslationDictTables(int langid, string word, string[] dictTablesOffline)
        {
            using (var db = new Entities())
            {
                var sql = string.Join(" Union ",
                    (from DICTTABLE in dictTablesOffline
                     select string.Format(@"
                             SELECT LANGID, WORDSLANG.WORD, LEVEL
                             FROM WORDSLANG INNER JOIN [{0}] ON WORDSLANG.WORD = [{0}].WORD
                             WHERE LANGID = @langid AND [TRANSLATION] LIKE '%' + @word + '%'"
                         , DICTTABLE)).ToArray());
                return db.Database.SqlQuery<MWORDLANG>(sql,
                    new SqlParameter("langid", langid),
                    new SqlParameter("word", word)).ToList();
            }
        }

        public static int GetWordCount(int langid, string word)
        {
            using (var db = new Entities())
                return db.SWORDLANG.Count(r => r.LANGID == langid && r.WORD == word);
        }

        public static int? GetWordLevel(int langid, string word)
        {
            using (var db = new Entities())
                return
                    (from r in db.SWORDLANG
                     where r.LANGID == langid && r.WORD == word
                     select r.LEVEL).SingleOrDefault();
        }

        public static void UpdateWordLevel(int level, int langid, string word)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLANG.SingleOrDefault(r => r.LANGID == langid && r.WORD == word);
                if (item != null)
                {
                    item.LEVEL = level;
                    db.SaveChanges();
                }
            }
        }
    }
}
