using System;
using System.Collections.Generic;
using System.Linq;

namespace LollyXamarinNative
{
    public class MAutoCorrects
    {
        public List<MAutoCorrect> AUTOCORRECT { get; set; }
    }

    public class MAutoCorrect
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public int SEQNUM { get; set; }
        public string INPUT { get; set; }
        public string EXTENDED { get; set; }
        public string BASIC { get; set; }

        public string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
