using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class PhrasesLang
    {
        public static List<MPHRASELANG> GetDataByLangPhrase(int langid, string phrase)
        {
            using (var db = new Entities())
            {
//                    var sql = @"
//        	            SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.[INDEX], PHRASES.PHRASE, PHRASES.[TRANSLATION]
//        	            FROM     PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID
//        	            WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.PHRASE LIKE '%' + @phrase + '%')
//                    ";
//                    return db.Database.SqlQuery<MPHRASELANG>(sql,
//                        new SqlParameter("langid", langid),
//                        new SqlParameter("phrase", phrase));
                return (
                    from rp in db.SPHRASEUNIT
                    join rb in db.SBOOK
                    on rp.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && rp.PHRASE.Contains(phrase)
                    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.INDEX, rp.PHRASE, rp.TRANSLATION }
                ).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
            }
        }

        public static List<MPHRASELANG> GetDataByLangTranslation(int langid, string translation)
        {
            using (var db = new Entities())
            {
//                    var sql = @"
//	                    SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.[INDEX], PHRASES.PHRASE, PHRASES.[TRANSLATION]
//	                    FROM      (PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID)
//	                    WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.[TRANSLATION] LIKE '%' + @translation + '%')
//                    ";
//                    return db.Database.SqlQuery<MPHRASELANG>(sql,
//                        new SqlParameter("langid", langid),
//                        new SqlParameter("translation", translation));
                return (
                    from rp in db.SPHRASEUNIT
                    join rb in db.SBOOK
                    on rp.BOOKID equals rb.BOOKID
                    where rb.LANGID == langid && rp.TRANSLATION.Contains(translation)
                    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.INDEX, rp.PHRASE, rp.TRANSLATION }
                ).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
            }
        }
    }
}
