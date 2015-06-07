using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static void WordsLang_Delete(long langid, string word)
        {
            using (var db = new LollyEntities())
            {
                var item = db.SWORDLANG.SingleOrDefault(r => r.LANGID == langid && r.WORD == word);
                if (item == null) return;

                db.SWORDLANG.Remove(item);
                db.SaveChanges();
            }
        }

        public static void WordsLang_Insert(long langid, string word)
        {
            using (var db = new LollyEntities())
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

        public static void WordsLang_Update(string new_word, long langid, string original_word)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                        UPDATE WORDSLANG
                        SET WORD = @new_word
                        WHERE (LANGID = @langid) AND (WORD = @original_word)
                    ";
                db.Database.ExecuteSqlCommand(sql,
                    new SQLiteParameter("new_word", new_word),
                    new SQLiteParameter("langid", langid),
                    new SQLiteParameter("original_word", original_word));

                // Can't update DB using the following code, for 'word' is part of the key
                //var item = db.SWORDLANG.SingleOrDefault(r => r.LANGID == langid && r.WORD == original_word);
                //if (item != null)
                //{
                //    item.WORD = new_word;
                //    db.SaveChanges();
                //}
            }
        }

        public static List<MWORDLANG> WordsLang_GetDataByLangWord(long langid, string word)
        {
            using (var db = new LollyEntities())
            {
                return (
                    from r in db.SWORDLANG
                    where r.LANGID == langid && (word == "" || r.WORD.Contains(word))
                    orderby r.WORD
                    select r
                ).ToList();
            }
        }

        public static List<MWORDLANG> WordsLang_GetDataByLangTranslationDictTables(long langid, string word, string[] dictTablesOffline)
        {
            using (var db = new LollyEntities())
            {
                var sql = string.Join(" Union ",
                    (from DICTTABLE in dictTablesOffline
                     select string.Format(@"
                             SELECT LANGID, WORDSLANG.WORD, LEVEL
                             FROM WORDSLANG INNER JOIN [{0}] ON WORDSLANG.WORD = [{0}].WORD
                             WHERE LANGID = @langid AND [TRANSLATION] LIKE '%' + @word + '%'"
                         , DICTTABLE)).ToArray());
                return db.Database.SqlQuery<MWORDLANG>(sql,
                    new SQLiteParameter("langid", langid),
                    new SQLiteParameter("word", word)).ToList();
            }
        }

        public static long WordsLang_GetWordCount(long langid, string word)
        {
            using (var db = new LollyEntities())
                return db.SWORDLANG.Count(r => r.LANGID == langid && r.WORD == word);
        }

        public static long? WordsLang_GetWordLevel(long langid, string word)
        {
            using (var db = new LollyEntities())
                return
                    (from r in db.SWORDLANG
                     where r.LANGID == langid && r.WORD == word
                     select r.LEVEL).SingleOrDefault();
        }

        public static void WordsLang_UpdateWordLevel(long level, long langid, string word)
        {
            using (var db = new LollyEntities())
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
