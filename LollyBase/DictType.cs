using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public static class DictType
    {
        public static List<MDICTTYPE> GetData()
        {
            using (var db = new Entities())
                return db.SDICTTYPE.ToList();
        }
    }
}
