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
        public void WordsLang_Delete(int langid, string word)
        {
            var sql = @"
                DELETE
                FROM WORDSLANG
                WHERE   (LANGID = @langid) AND (WORD = @word)
            ";
            db.Execute(sql, langid, word);
        }

        public void WordsLang_Insert(int langid, string word) =>
            db.Insert(new MWORDLANG
            {
                LANGID = langid,
                WORD = word,
                LEVEL = 0
            });

        public void WordsLang_Update(string new_word, int langid, string original_word)
        {
            var sql = @"
                UPDATE WORDSLANG
                SET WORD = @new_word
                WHERE (LANGID = @langid) AND (WORD = @original_word)
            ";
            db.Execute(sql, new_word, langid, original_word);
        }

        public List<MWORDLANG> WordsLang_GetDataByLangWord(int langid, string word) =>
        (
            from r in db.Table<MWORDLANG>()
            where r.LANGID == langid && r.WORD.Contains(word)
            orderby r.WORD
            select r
        ).ToList();

        public List<MWORDLANG> WordsLang_GetDataByLangTranslationDictTables(int langid, string word, string[] dictTablesOffline)
        {
            var sql = string.Join(" Union ",
                from dicttable in dictTablesOffline
                select $@"
                    SELECT LANGID, WORDSLANG.WORD, LEVEL
                    FROM WORDSLANG INNER JOIN [{dicttable}] ON WORDSLANG.WORD = [{dicttable}].WORD
                    WHERE LANGID = @langid AND [TRANSLATION] LIKE @word"
            );
            return db.Query<MWORDLANG>(sql, langid, $"%{word}%").ToList();
        }

        public int WordsLang_GetWordCount(int langid, string word) =>
            db.Table<MWORDLANG>().Count(r => r.LANGID == langid && r.WORD == word);

        public int? WordsLang_GetWordLevel(int langid, string word) =>
        (
            from r in db.Table<MWORDLANG>()
            where r.LANGID == langid && r.WORD == word
            select r.LEVEL
        ).SingleOrDefault();

        public void WordsLang_UpdateWordLevel(int level, int langid, string word)
        {
            var sql = @"
                UPDATE WORDSLANG
                SET LEVEL = @level
                WHERE   (LANGID = @langid) AND (WORD = @word)
            ";
            db.Execute(sql, level, langid, word);
        }
    }
}
