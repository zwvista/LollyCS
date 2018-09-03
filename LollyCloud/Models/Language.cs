using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyCloud
{
    public class Languages
    {
        public List<Language> LANGUAGES { get; set; }
    }
    public class Language
    {
        public int ID { get; set; }
        [JsonProperty("NAME")]
        public string LANGNAME { get; set; }
    }
}
