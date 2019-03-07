using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyXamarinNative
{
    public class MLanguages
    {
        public List<MLanguage> LANGUAGES { get; set; }
    }
    public class MLanguage
    {
        public int ID { get; set; }
        [JsonProperty("NAME")]
        public string LANGNAME { get; set; }
    }
}
