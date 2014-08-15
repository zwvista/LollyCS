using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WordsLangOrBook
    {
        public static List<MWORDLANGORBOOK> GetDataByBookLessonParts(int bookid, int lessonpartfrom, int lessonpartto)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SWORDLESSON
                    let lessonpart = r.LESSON * 10 + r.PART
                    where r.BOOKID == bookid && lessonpart >= lessonpartfrom && lessonpart <= lessonpartto
                    select r.WORD
                ).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
            }
        }

        public static List<MWORDLANGORBOOK> GetDataByLang(int langid)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SWORDLANG
                    where r.LANGID == langid
                    select r.WORD
                ).ToList().Select(w => new MWORDLANGORBOOK { WORD = w }).ToList();
            }
        }
    }
}
