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
        public void WebText_Delete(string sitename) =>
            db.Delete<MWEBTEXT>(sitename);

        public void WebText_Insert(MWEBTEXT row) =>
            db.Insert(row);

        public void WebText_Update(MWEBTEXT row, string original_sitename)
        {
            var sql = @"
                UPDATE  WEBTEXT
                SET SITENAME = @sitename, URL = @url, TEMPLATE = @template, FOLDER = @folder
                WHERE   (SITENAME = @original_sitename)
            ";
            db.Execute(sql, row.SITENAME, row.URL, row.TEMPLATE, row.FOLDER, original_sitename);
        }

        public List<MWEBTEXT> WebText_GetData() =>
            db.Table<MWEBTEXT>().ToList();
    }
}
