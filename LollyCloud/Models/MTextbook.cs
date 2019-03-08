using System;
using System.Collections.Generic;
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
        public string LANGNAME { get; set; }
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME { get; set; }
        public string UNITS { get; set; }
        public string PARTS { get; set; }
    }
}
