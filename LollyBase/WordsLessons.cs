using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class WordsLessons
    {
        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLESSON.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                db.SWORDLESSON.Remove(item);
                db.SaveChanges();
            }
        }

        public static int Insert(MWORDLESSON row)
        {
            using (var db = new Entities())
            {
                var item = new MWORDLESSON
                {
                    BOOKID = row.BOOKID,
                    LESSON = row.LESSON,
                    PART = row.PART,
                    INDEX = row.INDEX,
                    WORD = row.WORD,
                    NOTE = row.NOTE
                };
                db.SWORDLESSON.Add(item);
                db.SaveChanges();
                return item.ID;
            }
        }

        public static void Update(MWORDLESSON row)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLESSON.SingleOrDefault(r => r.ID == row.ID);
                if (item == null) return;

                item.LESSON = row.LESSON;
                item.PART = row.PART;
                item.INDEX = row.INDEX;
                item.WORD = row.WORD;
                item.NOTE = row.NOTE;
                db.SaveChanges();
            }
        }

        public static void UpdateIndex(int index, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLESSON.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.INDEX = index;
                db.SaveChanges();
            }
        }

        public static void UpdateNote(string note, int id)
        {
            using (var db = new Entities())
            {
                var item = db.SWORDLESSON.SingleOrDefault(r => r.ID == id);
                if (item == null) return;

                item.NOTE = note;
                db.SaveChanges();
            }
        }

        public static List<MWORDLESSON> GetDataByBookLessonParts(int bookid, int lessonpartfrom, int lessonpartto)
        {
            using (var db = new Entities())
            {
                return (
                    from r in db.SWORDLESSON
                    let lessonpart = r.LESSON * 10 + r.PART
                    where r.BOOKID == bookid && lessonpart >= lessonpartfrom && lessonpart <= lessonpartto
                    orderby r.LESSON, r.PART, r.INDEX
                    select r
                ).ToList();
            }
        }
    }
}
