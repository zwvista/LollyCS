using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MLanguages
    {
        [JsonProperty("records")]
        public List<MLanguage> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLanguage : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public string LANGNAME { get; set; }
    }
}
