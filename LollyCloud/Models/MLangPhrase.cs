using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MLangPhrases
    {
        public List<MLangPhrase> LANGPHRASES { get; set; }
    }
    public class MLangPhrase
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public string PHRASE { get; set; }
        public string TRANSLATION { get; set; }

        MLangPhrase() { }
    }
}
