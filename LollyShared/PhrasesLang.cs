using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static List<MPHRASELANG> PhrasesLang_GetDataByLangPhrase(long langid, string phrase, bool matchWholeWords)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                    SELECT   PHRASES.ID, PHRASES.BOOKID, TEXTBOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.SEQNUM, PHRASES.PHRASE, PHRASES.[TRANSLATION]
                    FROM     PHRASES INNER JOIN TEXTBOOKS ON PHRASES.BOOKID = TEXTBOOKS.BOOKID
                    WHERE   (TEXTBOOKS.LANGID = @langid) AND (PHRASES.PHRASE LIKE @phrase)
                ";
                var lst = db.Database.SqlQuery<MPHRASELANG>(sql,
                    new SQLiteParameter("langid", langid),
                    new SQLiteParameter("phrase", $"%{phrase}%")).ToList();
                //var lst = (
                //    from rp in db.SPHRASEUNIT
                //    join rb in db.SBOOK
                //    on rp.BOOKID equals rb.BOOKID
                //    where rb.LANGID == langid && (phrase == "" || rp.PHRASE.Contains(phrase))
                //    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.SEQNUM, rp.PHRASE, rp.TRANSLATION }
                //).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
                if (matchWholeWords)
                    lst = lst.Where(r => Regex.IsMatch(r.PHRASE, $@"\b{phrase}\b")).ToList();
                return lst;
            }
        }

        public static List<MPHRASELANG> PhrasesLang_GetDataByLangTranslation(long langid, string translation)
        {
            using (var db = new LollyEntities())
            {
                var sql = @"
                    SELECT   PHRASES.ID, PHRASES.BOOKID, TEXTBOOKS.BOOKNAME, PHRASES.UNIT, PHRASES.PART, PHRASES.SEQNUM, PHRASES.PHRASE, PHRASES.[TRANSLATION]
                    FROM      (PHRASES INNER JOIN TEXTBOOKS ON PHRASES.BOOKID = TEXTBOOKS.BOOKID)
                    WHERE   (TEXTBOOKS.LANGID = @langid) AND (PHRASES.[TRANSLATION] LIKE @translation)
                ";
                return db.Database.SqlQuery<MPHRASELANG>(sql,
                    new SQLiteParameter("langid", langid),
                    new SQLiteParameter("translation", $"%{translation}%")).ToList();

                // The following code doesn't work, why?
                //return (
                //    from rp in db.SPHRASEUNIT
                //    join rb in db.SBOOK
                //    on rp.BOOKID equals rb.BOOKID
                //    where rb.LANGID == langid && (translation == "" || rp.TRANSLATION.Contains(translation))
                //    select new { rp.ID, rp.BOOKID, rb.BOOKNAME, rp.UNIT, rp.PART, rp.SEQNUM, rp.PHRASE, rp.TRANSLATION }
                //).ToList().ToNonAnonymousList(new List<MPHRASELANG>());
            }
        }
    }
}
