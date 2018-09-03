using System;
using System.Collections.Generic;

namespace LollyCloud
{
    public class UnitWords
    {
        public List<UnitWord> VUNITWORDS { get; set; }
    }
    public class UnitWord
    {
        public int ID { get; set; }
        public int TEXTBOOKID { get; set; }
        public int UNIT { get; set; }
        public int PART { get; set; }
        public int SEQNUM { get; set; }
        public String WORD { get; set; }
        public String NOTE { get; set; }
        public int UNITPART { get; }
    }
}
