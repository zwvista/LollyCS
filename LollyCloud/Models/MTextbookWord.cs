using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MTextbookWords
    {
        public List<MTextbookWord> VTEXTBOOKWORDS { get; set; }
    }
    public class MTextbookWord
    {
        public int ID { get; set; }
        public int TEXTBOOKID { get; set; }
        public int LANGID { get; set; }
        public string TEXTBOOKNAME { get; set; }
        public int UNIT { get; set; }
        public int PART { get; set; }
        public int SEQNUM { get; set; }
        public int WORDID { get; set; }
        public string WORD { get; set; }
        public string NOTE { get; set; }
        public int FAMIID { get; set; }
        public int LEVEL { get; set; }
    }
}
