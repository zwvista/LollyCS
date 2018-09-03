using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyCloud
{
    public class Textbooks
    {
        public List<Textbook> TEXTBOOKS { get; set; }
    }
    public class Textbook
    {
        public int ID { get; set; }
        public string LANGNAME { get; set; }
        [JsonProperty("NAME")]
        public string TEXTBOOKNAME { get; set; }
        public int UNITS { get; set; }
        public string PARTS { get; set; }
    }
}
