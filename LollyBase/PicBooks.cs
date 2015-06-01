using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public void PicBooks_Delete(string bookname) =>
            db.Delete<MPICBOOK>(bookname);

        public void PicBooks_Insert(MPICBOOK row) =>
            db.Insert(row);

        public void PicBooks_Update(MPICBOOK row, string original_bookname)
        {
            var sql = @"
                UPDATE  PICBOOKS
                SET BOOKNAME = @bookname, FILENAME = @filename, NUMPAGES = @numpages
                WHERE   (BOOKNAME = @original_bookname)
            ";
            db.Execute(sql, row.BOOKNAME, row.FILENAME, row.NUMPAGES, original_bookname);
        }

        public List<MPICBOOK> PicBooks_GetDataByLang(int langid) =>
            db.Table<MPICBOOK>().Where(r => r.LANGID == langid).ToList();
    }
}
