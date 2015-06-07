using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static List<MPHRASELANG> PhrasesLang_GetDataByLangPhrase(long langid, string phrase)
        {
            using (var db = new LollyEntities())
            {
//                    var sql = @"
//        	            SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.ORD, PHRASES.PHRASE, PHRASES.[TRANSLATION]
//        	            FROM     PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID
//        	            WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.PHRASE LIKE '%' + @phrase + '%')
//                    ";
//                    return db.Database.SqlQuery<MPHRASELANG>(sql,
//                        new SQLiteParameter("langid", langid),
//                        new SQLiteParameter("phrase", phrase));
                return (
                    from rp in db.SPHRASEUNIT
                    join rb in db.SBOOK
                    on rp.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && (phrase == "" || rp.PHRASE.Contains(phrase))
                    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.ORD, rp.PHRASE, rp.TRANSLATION }
                ).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
            }
        }

        public static List<MPHRASELANG> PhrasesLang_GetDataByLangTranslation(long langid, string translation)
        {
            using (var db = new LollyEntities())
            {
//                    var sql = @"
//	                    SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.ORD, PHRASES.PHRASE, PHRASES.[TRANSLATION]
//	                    FROM      (PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID)
//	                    WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.[TRANSLATION] LIKE '%' + @translation + '%')
//                    ";
//                    return db.Database.SqlQuery<MPHRASELANG>(sql,
//                        new SQLiteParameter("langid", langid),
//                        new SQLiteParameter("translation", translation));
                return (
                    from rp in db.SPHRASEUNIT
                    join rb in db.SBOOK
                    on rp.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && (translation == "" || rp.TRANSLATION.Contains(translation))
                    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.ORD, rp.PHRASE, rp.TRANSLATION }
                ).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
            }
        }
    }
}
