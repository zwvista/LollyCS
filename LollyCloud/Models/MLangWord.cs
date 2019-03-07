using System;
using System.Collections.Generic;

namespace LollyShared
{
    public class MLangWords
    {
        public List<MLangWord> VLANGWORDS { get; set; }
    }
    public class MLangWord
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public string WORD { get; set; }
        public string NOTE { get; set; }
        public int FAMIID { get; set; }
        public int LEVEL { get; set; }
    }
}
