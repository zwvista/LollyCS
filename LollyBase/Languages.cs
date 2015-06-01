using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void Languages_UpdateBook(int bookid, int langid)
        {
            var sql = @"
                UPDATE  LANGUAGES
                SET CURBOOKID = ?
                WHERE   (LANGID = ?)
            ";
            db.Execute(sql, bookid, langid);
        }

        public MLANGUAGE Languages_GetDataByLang(int langid) =>
            db.Table<MLANGUAGE>().SingleOrDefault(r => r.LANGID == langid);

        public List<MLANGUAGE> Languages_GetData() =>
            db.Table<MLANGUAGE>().ToList();

        public List<MLANGUAGE> Languages_GetDataNonChinese() =>
            db.Table<MLANGUAGE>().Where(r => r.LANGID > 0).ToList();
    }
}
