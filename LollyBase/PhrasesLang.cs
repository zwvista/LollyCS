using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public List<MPHRASELANG> PhrasesLang_GetDataByLangPhrase(int langid, string phrase)
        {
            var sql = @"
                SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.ORD, PHRASES.PHRASE, PHRASES.[TRANSLATION]
                FROM     PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID
                WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.PHRASE LIKE @phrase)
            ";
            return db.Query<MPHRASELANG>(sql, langid, $"%{phrase}%");
            //return (
            //    from rp in db.Table<MPHRASEUNIT>()
            //    join rb in db.Table<MBOOK>()
            //    on rp.BOOKID equals rb.BOOKID
            //    where rb.LANGID == langid && rp.PHRASE.Contains(phrase)
            //    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.ORD, rp.PHRASE, rp.TRANSLATION }
            //).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
        }

        public List<MPHRASELANG> PhrasesLang_GetDataByLangTranslation(int langid, string translation)
        {
            var sql = @"
                SELECT   PHRASES.ID, PHRASES.BOOKID, BOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.ORD, PHRASES.PHRASE, PHRASES.[TRANSLATION]
                FROM      (PHRASES INNER JOIN BOOKS ON PHRASES.BOOKID = BOOKS.BOOKID)
                WHERE   (BOOKS.LANGID = @langid) AND (PHRASES.[TRANSLATION] LIKE @translation)
            ";
            return db.Query<MPHRASELANG>(sql, langid, $"%{translation}%");
            //return (
            //    from rp in db.Table<MPHRASEUNIT>()
            //    join rb in db.Table<MBOOK>()
            //    on rp.BOOKID equals rb.BOOKID
            //    where rb.LANGID == langid && rp.TRANSLATION.Contains(translation)
            //    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.ORD, rp.PHRASE, rp.TRANSLATION }
            //).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
        }
    }
}
