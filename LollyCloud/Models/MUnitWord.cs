using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LollyShared
{
    public class MUnitWords
    {
        public List<MUnitWord> VUNITWORDS { get; set; }
    }
    public class MUnitWord
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        public int TEXTBOOKID { get; set; }
        public int UNIT { get; set; }
        public int PART { get; set; }
        public int SEQNUM { get; set; }
        public int WORDID { get; set; }
        public string WORD { get; set; }
        public string NOTE { get; set; }
        public int FAMIID { get; set; }
        public int LEVEL { get; set; }

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;

        public string UNITSTR => lstUnits.First(o => o.Value == UNIT).Label;
        public string PARTSTR => lstParts.First(o => o.Value == PART).Label;
    }
}
