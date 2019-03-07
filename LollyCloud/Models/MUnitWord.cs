using System;
using System.Collections.Generic;

namespace LollyXamarinNative
{
    public class MUnitWords
    {
        public List<MUnitWord> VUNITWORDS { get; set; }
    }
    public class MUnitWord
    {
        public int ID { get; set; }
        public int TEXTBOOKID { get; set; }
        public int UNIT { get; set; }
        public int PART { get; set; }
        public int SEQNUM { get; set; }
        public string WORD { get; set; }
        public string NOTE { get; set; }
        public int UNITPART { get; }
    }
}
