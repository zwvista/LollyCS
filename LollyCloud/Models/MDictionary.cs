using System;
using System.Collections.Generic;

namespace LollyXamarinNative
{
    public class MDictionary
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

    public class MDictsOnline
    {
        public List<MDictOnline> VDICTSONLINE { get; set; }
    }
    public class MDictOnline : MDictionary { }
    public class DictsOffline
    {
        public List<MDictOffline> VDICTSOFFLINE { get; set; }
    }
    public class MDictOffline : MDictionary { }
    public class DictsNote
    {
        public List<MDictNote> VDICTSNOTE { get; set; }
    }
    public class MDictNote : MDictionary { }
}
