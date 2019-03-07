using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MTextbookPhrases
    {
        public List<MTextbookPhrase> VTEXTBOOKPHRASES { get; set; }
    }
    public class MTextbookPhrase
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public int TEXTBOOKID { get; set; }
        public int UNIT { get; set; }
        public int PART { get; set; }
        public int SEQNUM { get; set; }
        public int PHRASEID { get; set; }
        public string PHRASE { get; set; }
        public string TRANSLATION { get; set; }
    }
}
