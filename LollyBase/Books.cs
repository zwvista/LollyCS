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
        public void Books_Delete(int bookid) =>
            db.Delete<MBOOK>(bookid);

        public void Books_Insert(MBOOK row) =>
            db.Insert(row);

        public void Books_Update(MBOOK row, int original_bookid)
        {
            var sql = @"
                UPDATE  BOOKS
                SET BOOKID = @bookid, BOOKNAME = @bookname, UNITSINBOOK = @unitsinbook, PARTS = @parts
                WHERE   (BOOKID = ?)
            ";
            db.Execute(sql, row.BOOKID, row.BOOKNAME, row.UNITSINBOOK, row.PARTS, original_bookid);
        }

        public void Books_UpdateUnit(int unitfrom, int partfrom, int unitto, int partto, int bookid)
        {
            var sql = @"
                UPDATE  BOOKS
                SET UNITFROM = @unitfrom, PARTFROM = @partfrom, UNITTO = @unitto, PARTTO = @partto
                WHERE   (BOOKID = ?)
            ";
            db.Execute(sql, unitfrom, partfrom, unitto, partto, bookid);
        }

        public MBOOK Books_GetDataByBook(int bookid) =>
            db.Table<MBOOK>().SingleOrDefault(r => r.BOOKID == bookid);

        public List<MBOOK> Books_GetDataByLang(int langid) =>
        (
            from r in db.Table<MBOOK>()
            where r.LANGID == langid
            orderby r.BOOKID
            select r
        ).ToList();
    }
}
