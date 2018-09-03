using System;
using System.Collections.Generic;

namespace LollyCloud
{
    public class Dictionary
    {
        public int ID { get; set; }
        public string LANGNAME { get; set; }
        public string DICTTYPENAME { get; set; }
        public string DICTNAME { get; set; }
        public string URL { get; set; }
        public string CHCONV { get; set; }
        public string TRANSFORM_MAC { get; set; }
        public int? WAIT { get; set; }
        public string TEMPLATE { get; set; }
    }

    public class DictsOnline
    {
        public List<DictOnline> VDICTSONLINE { get; set; }
    }
    public class DictOnline : Dictionary { }
    public class DictsOffline
    {
        public List<DictOffline> VDICTSOFFLINE { get; set; }
    }
    public class DictOffline : Dictionary { }
    public class DictsNote
    {
        public List<DictNote> VDICTSNOTE { get; set; }
    }
    public class DictNote : Dictionary { }
}
