using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyShared
{
    public static partial class LollyDB
    {
        public static List<MDICTTYPE> DictTypes_GetData()
        {
            using (var db = new LollyEntities())
                return db.SDICTTYPE.ToList();
        }
    }
}
