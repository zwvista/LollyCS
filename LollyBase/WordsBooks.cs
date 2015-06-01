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
        public List<MWORDBOOK> WordsBooks_GetDataByLangWord(int langid, string word)
        {
            //var sql = @"
            //    SELECT   WORDSBOOK.ID, WORDSBOOK.BOOKID, WORDSBOOK.UNIT, WORDSBOOK.PART, WORDSBOOK.ORD, 
            //      WORDSBOOK.WORD, BOOKS.BOOKNAME, WORDSBOOK.[NOTE]
            //    FROM      (WORDSBOOK INNER JOIN BOOKS ON WORDSBOOK.BOOKID = BOOKS.BOOKID)
            //    WHERE   (BOOKS.LANGID = @langid) AND (WORDSBOOK.WORD LIKE '%' + @word + '%')
            //";
            //return db.Query<MWORDBOOK>(sql, langid, word);
            return (
                from rw in db.Table<MWORDUNIT>()
                join rb in db.Table<MBOOK>()
                on rw.BOOKID equals rb.BOOKID
                where rb.LANGID == langid && rw.WORD.Contains(word)
                select new { rw.ID, rw.BOOKID, rw.UNIT, rw.PART, rw.ORD, rw.WORD, rb.BOOKNAME, rw.NOTE }
            ).ToList().ToNonAnonymousList(new List<MWORDBOOK>());
        }

        public List<MWORDBOOK> WordsBooks_GetDataByLangTranslationDictTables(int langid, string word, string[] dictTablesOffline)
        {
            var sql =
                string.Join(" Union ",
                    from dicttable in dictTablesOffline
                    select $@"
                        SELECT ID, WORDSBOOK.BOOKID, BOOKNAME, UNIT, PART, ORD, WORDSBOOK.WORD, NOTE
                        FROM BOOKS INNER JOIN (WORDSBOOK INNER JOIN [{dicttable}]
                        ON WORDSBOOK.WORD = [{dicttable}].WORD) ON BOOKS.BOOKID = WORDSBOOK.BOOKID
                        WHERE LANGID = @langid AND [TRANSLATION] LIKE '%' + @word + '%'"
                );
            return db.Query<MWORDBOOK>(sql, langid, word).ToList();
        }

        public int WordsBooks_GetWordCount(int langid, string word)
        {
            //var sql = @"
	           //         SELECT   COUNT(*)
	           //         FROM      (BOOKS INNER JOIN WORDSBOOK ON BOOKS.BOOKID = WORDSBOOK.BOOKID)
	           //         WHERE   (BOOKS.LANGID = @langid) AND (WORDSBOOK.WORD = @word)
            //        ";
            //return db.Query<int>(sql, langid, word).Single();
            return (
                from rb in db.Table<MBOOK>()
                join rw in db.Table<MWORDUNIT>()
                on rb.BOOKID equals rw.BOOKID
                where rb.LANGID == langid && rw.WORD == word
                select rw
            ).Count();
        }
    }
}
