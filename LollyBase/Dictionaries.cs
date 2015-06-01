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
        public void Dictionaries_Delete(int langid, string dictname)
        {
            var sql = @"
                DELETE
                FROM DICTIONARIES
                WHERE   (LANGID = @langid) AND (DICTNAME = @dictname)
            ";
            db.Execute(sql, langid, dictname);
        }

        public void Dictionaries_Insert(MDICTIONARY row) =>
            db.Insert(row);

        public void Dictionaries_Update(MDICTIONARY row, string original_dictname)
        {
            var sql = @"
                UPDATE  DICTIONARIES
                SET ORD = @ord, DICTTYPEID = @dicttypeid, DICTNAME = @dictname, LANGIDTO = @langidto,
                    URL = @url, CHCONV = @chconv, AUTOMATION = @automation, AUTOJUMP = @autojump,
                    DICTTABLE = @dicttable, TEMPLATE = @template
                WHERE   (LANGID = @langid) AND (DICTNAME = @original_dictname)
            ";
            db.Execute(sql, row.ORD, row.DICTTYPEID, row.DICTNAME, row.LANGIDTO,
                row.URL ?? (object)DBNull.Value, row.CHCONV, row.AUTOMATION,
                row.AUTOJUMP, row.DICTTABLE ?? (object)DBNull.Value,
                row.TEMPLATE ?? (object)DBNull.Value, row.LANGID, original_dictname);
        }

        public List<MDICTIONARY> Dictionaries_GetDataByLang(int langid) =>
            db.Table<MDICTIONARY>().Where(r => r.LANGID == langid).ToList();
    }
}
