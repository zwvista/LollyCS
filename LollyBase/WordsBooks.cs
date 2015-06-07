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
        public static List<MWORDBOOK> WordsBooks_GetDataByLangWord(long langid, string word)
        {
            using (var db = new LollyEntities())
            {
//                    var sql = @"
//        	            SELECT   WORDSBOOK.ID, WORDSBOOK.BOOKID, WORDSBOOK.UNIT, WORDSBOOK.PART, WORDSBOOK.ORD, 
//        					            WORDSBOOK.WORD, BOOKS.BOOKNAME, WORDSBOOK.[NOTE]
//        	            FROM      (WORDSBOOK INNER JOIN BOOKS ON WORDSBOOK.BOOKID = BOOKS.BOOKID)
//        	            WHERE   (BOOKS.LANGID = @langid) AND (WORDSBOOK.WORD LIKE '%' + @word + '%')
//                    ";
//                    return db.Database.SqlQuery<MWORDBOOK>(sql,
//                        new SQLiteParameter("langid", langid),
//                        new SQLiteParameter("word", word));
                return (
                    from rw in db.SWORDUNIT
                    join rb in db.SBOOK
                    on rw.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && (word == "" || rw.WORD.Contains(word))
                    select new { rw.ID, rw.BOOKID, rw.UNIT, rw.PART, rw.ORD, rw.WORD, rb.BOOKNAME, rw.NOTE }
                ).ToList().ToNonAnonymousList(new List<MWORDBOOK>());
            }
        }

        public static List<MWORDBOOK> WordsBooks_GetDataByLangTranslationDictTables(long langid, string word, string[] dictTablesOffline)
        {
            using (var db = new LollyEntities())
            {
                var sql =
                    string.Join(" Union ",
                        from dicttable in dictTablesOffline
                        select string.Format(@"
                                SELECT ID, WORDSBOOK.BOOKID, BOOKNAME, UNIT, PART, ORD, WORDSBOOK.WORD, NOTE
                                FROM BOOKS INNER JOIN (WORDSBOOK INNER JOIN [{0}]
                                ON WORDSBOOK.WORD = [{0}].WORD) ON BOOKS.BOOKID = WORDSBOOK.BOOKID
                                WHERE LANGID = @langid AND [TRANSLATION] LIKE '%' + @word + '%'"
                            , dicttable
                        )
                    );
                return db.Database.SqlQuery<MWORDBOOK>(sql,
                    new SQLiteParameter("langid", langid),
                    new SQLiteParameter("word", word)).ToList();
            }
        }

        public static int WordsBooks_GetWordCount(long langid, string word)
        {
            using (var db = new LollyEntities())
            {
//                    var sql = @"
//	                    SELECT   COUNT(*)
//	                    FROM      (BOOKS INNER JOIN WORDSBOOK ON BOOKS.BOOKID = WORDSBOOK.BOOKID)
//	                    WHERE   (BOOKS.LANGID = @langid) AND (WORDSBOOK.WORD = @word)
//                    ";
//                    return db.Database.SqlQuery<int>(sql,
//                        new SQLiteParameter("langid", langid),
//                        new SQLiteParameter("word", word)).Single();
                return (
                    from rb in db.SBOOK
                    join rw in db.SWORDUNIT
                    on rb.BOOKID equals rw.BOOKID
                    where rb.LANGID == langid && rw.WORD == word
                    select rw
                ).Count();
            }
        }
    }
}
