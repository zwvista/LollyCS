using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyBase
{
    public partial class LollyDB
    {
        public List<MDICTTYPE> DictTypes_GetData() =>
            db.Table<MDICTTYPE>().ToList();
    }
}
