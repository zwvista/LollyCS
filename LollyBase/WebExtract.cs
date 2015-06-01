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
        public void WebExtract_Delete(string sitename) =>
            db.Delete<MWEBEXTRACT>(sitename);

        public void WebExtract_Insert(MWEBEXTRACT row) =>
            db.Insert(row);

        public void WebExtract_Update(MWEBEXTRACT row, string original_sitename)
        {
            var sql = @"
                UPDATE  WEBEXTRACT
                SET SITENAME = @sitename, TRANSFORM_WIN = @transform_win, TRANSFORM_MAC = @transfrom_mac, WAIT = @wait
                WHERE   (SITENAME = @original_sitename)
            ";
            db.Execute(sql, row.SITENAME, row.TRANSFORM_WIN ?? (object)DBNull.Value,
                row.TRANSFORM_MAC ?? (object)DBNull.Value, row.WAIT, original_sitename);
        }

        public List<MWEBEXTRACT> WebExtract_GetData() =>
            db.Table<MWEBEXTRACT>().OrderBy(r => r.SITENAME).ToList();
    }
}
