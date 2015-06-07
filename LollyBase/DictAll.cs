using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static partial class LollyDB
    {
        public static List<MDICTALL> DictAll_GetDataByLang(long langid)
        {
            using (var db = new LollyEntities())
                return db.SDICTALL.Where(r => r.LANGID == langid).ToList();
        }

        public static MDICTALL DictAll_GetDataByLangDict(long langid, string dict)
        {
            using (var db = new LollyEntities())
                return db.SDICTALL.SingleOrDefault(r => r.LANGID == langid && r.DICTNAME == dict);
        }

        public static List<MDICTALL> DictAll_GetDataByLangDictType(long langid, string dicttype)
        {
            using (var db = new LollyEntities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == dicttype
                    orderby r.ORD
                    select r
                ).ToList();
        }

        public static List<MDICTALL> DictAll_GetDataByLangExact(long langid)
        {
            using (var db = new LollyEntities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == "OFFLINE-ONLINE"
                    orderby r.DICTNAME
                    select r
                ).ToList();
        }

        public static List<MDICTALL> DictAll_GetDataByLangWeb(long langid)
        {
            using (var db = new LollyEntities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == "WEB"
                    orderby r.DICTNAME
                    select r
                ).ToList();
        }
    }
}
