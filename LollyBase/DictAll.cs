using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public List<MDICTALL> DictAll_GetDataByLang(int langid) =>
            db.Table<MDICTALL>().Where(r => r.LANGID == langid).ToList();

        public MDICTALL DictAll_GetDataByLangDict(int langid, string dict) =>
            db.Table<MDICTALL>().SingleOrDefault(r => r.LANGID == langid && r.DICTNAME == dict);

        public List<MDICTALL> DictAll_GetDataByLangDictType(int langid, string dicttype) =>
        (
            from r in db.Table<MDICTALL>()
            where r.LANGID == langid && r.DICTTYPENAME == dicttype
            orderby r.ORD
            select r
        ).ToList();

        public List<MDICTALL> DictAll_GetDataByLangExact(int langid) =>
        (
            from r in db.Table<MDICTALL>()
            where r.LANGID == langid && r.DICTTYPENAME == "OFFLINE-ONLINE"
            orderby r.DICTNAME
            select r
        ).ToList();

        public List<MDICTALL> DictAll_GetDataByLangWeb(int langid) =>
        (
            from r in db.Table<MDICTALL>()
            where r.LANGID == langid && r.DICTTYPENAME == "WEB"
            orderby r.DICTNAME
            select r
        ).ToList();
    }
}
