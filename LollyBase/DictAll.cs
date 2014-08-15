using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class DictAll
    {
        public static List<MDICTALL> GetDataByLang(int langid)
        {
            using (var db = new Entities())
                return db.SDICTALL.Where(r => r.LANGID == langid).ToList();
        }

        public static MDICTALL GetDataByLangDict(int langid, string dict)
        {
            using (var db = new Entities())
                return db.SDICTALL.SingleOrDefault(r => r.LANGID == langid && r.DICTNAME == dict);
        }

        public static List<MDICTALL> GetDataByLangDictType(int langid, string dicttype)
        {
            using (var db = new Entities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == dicttype
                    orderby r.INDEX
                    select r
                ).ToList();
        }

        public static List<MDICTALL> GetDataByLangExact(int langid)
        {
            using (var db = new Entities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == "OFFLINE-ONLINE"
                    orderby r.DICTNAME
                    select r
                ).ToList();
        }

        public static List<MDICTALL> GetDataByLangWeb(int langid)
        {
            using (var db = new Entities())
                return (
                    from r in db.SDICTALL
                    where r.LANGID == langid && r.DICTTYPENAME == "WEB"
                    orderby r.DICTNAME
                    select r
                ).ToList();
        }
    }
}
