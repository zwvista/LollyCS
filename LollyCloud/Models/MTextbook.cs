using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MTextbooks
    {
        public List<MTextbook> TEXTBOOKS { get; set; }
    }
    public class MTextbook
    {
        public int ID { get; set; }
        public int LANGID { get; set; }
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME { get; set; }
        public string UNITS { get; set; }
        public string PARTS { get; set; }

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;
    }
}
