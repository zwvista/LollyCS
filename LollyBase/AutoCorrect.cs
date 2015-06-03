using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void AutoCorrect_Delete(int id) =>
            db.Delete<MAUTOCORRECT>(id);

        public int AutoCorrect_Insert(MAUTOCORRECT row) =>
            db.Insert(row);

        public void AutoCorrect_Update(MAUTOCORRECT row) =>
            db.Update(row);

        public void AutoCorrect_UpdateOrd(int ord, int id)
        {
            var sql = @"
                UPDATE  AUTOCORRECT
                SET ORD = @ord
                WHERE (id = @id)
            ";
            db.Execute(sql, ord, id);
        }

        public List<MAUTOCORRECT> AutoCorrect_GetDataByLang(int langid) =>
            db.Table<MAUTOCORRECT>().Where(r => r.LANGID == langid).ToList();
    }
}
