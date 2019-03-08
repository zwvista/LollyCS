using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LollyShared
{
    public class MUnitPhrases
    {
        public List<MUnitPhrase> VUNITPHRASES { get; set; }
    }
    public class MUnitPhrase
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

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;

        public string UNITSTR => lstUnits.First(o => o.Value == UNIT).Label;
        public string PARTSTR => lstParts.First(o => o.Value == PART).Label;
    }
}
