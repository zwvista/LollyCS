using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class PhrasesLessons
    {
        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASELESSON.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SPHRASELESSON.Remove(item);
                db.SaveChanges();
            }
        }

        public static int Insert(MPHRASELESSON row)
        {
            using (var db = new Entities())
            {
                var item = new MPHRASELESSON
                {
                    BOOKID = row.BOOKID,
                    LESSON = row.LESSON,
                    PART = row.PART,
                    INDEX = row.INDEX,
                    PHRASE = row.PHRASE,
                    TRANSLATION = row.TRANSLATION
                };
                db.SPHRASELESSON.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void Update(MPHRASELESSON row)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASELESSON.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.LESSON = row.LESSON;
                item.PART = row.PART;
                item.INDEX = row.INDEX;
                item.PHRASE = row.PHRASE;
                item.TRANSLATION = row.TRANSLATION;
                db.SaveChanges();
            }
        }

        public static void UpdateIndex(int index, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SPHRASELESSON.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.INDEX = index;
                db.SaveChanges();
            }
        }

        public static List<MPHRASELESSON> GetDataByBookLessonParts(int bookid, int lessonpartfrom, int lessonpartto)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SPHRASELESSON
                    let lessonpart = r.LESSON * 10 + r.PART
                    where r.BOOKID == bookid && lessonpart >= lessonpartfrom && lessonpart <= lessonpartto
                    orderby r.LESSON, r.PART, r.INDEX
                    select r
                ).ToList();
            }
        }
    }
}
