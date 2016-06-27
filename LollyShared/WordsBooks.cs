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
        public static List<MWORDBOOK> WordsBooks_GetDataByLangWord(long langid, string word)
        {
            using (var db = new LollyEntities())
            {
//                    var sql = @"
//        	            SELECT   WORDSBOOK.ID, WORDSBOOK.BOOKID, WORDSBOOK.UNIT, WORDSBOOK.PART, WORDSBOOK.SEQNUM, 
//        					            WORDSBOOK.WORD, TEXTBOOKS.BOOKNAME, WORDSBOOK.[NOTE]
//        	            FROM      (WORDSBOOK INNER JOIN TEXTBOOKS ON WORDSBOOK.BOOKID = TEXTBOOKS.BOOKID)
//        	            WHERE   (TEXTBOOKS.LANGID = @langid) AND (WORDSBOOK.WORD LIKE '%' + @word + '%')
//                    ";
//                    return db.Database.SqlQuery<MWORDBOOK>(sql,
//                        new SQLiteParameter("langid", langid),
//                        new SQLiteParameter("word", word));
                return (
                    from rw in db.SWORDUNIT
                    join rb in db.SBOOK
                    on rw.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && (word == "" || rw.WORD.Contains(word))
                    select new { rw.ID, rw.BOOKID, rw.UNIT, rw.PART, rw.SEQNUM, rw.WORD, rb.BOOKNAME, rw.NOTE }
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
                                SELECT ID, WORDSBOOK.BOOKID, BOOKNAME, UNIT, PART, SEQNUM, WORDSBOOK.WORD, NOTE
                                FROM TEXTBOOKS INNER JOIN (WORDSBOOK INNER JOIN [{0}]
                                ON WORDSBOOK.WORD = [{0}].WORD) ON TEXTBOOKS.BOOKID = WORDSBOOK.BOOKID
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
//	                    FROM      (TEXTBOOKS INNER JOIN WORDSBOOK ON TEXTBOOKS.BOOKID = WORDSBOOK.BOOKID)
//	                    WHERE   (TEXTBOOKS.LANGID = @langid) AND (WORDSBOOK.WORD = @word)
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
