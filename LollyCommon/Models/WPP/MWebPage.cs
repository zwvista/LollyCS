using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MWebPages
    {
        [JsonProperty("records")]
        public List<MWebPage> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MWebPage : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public string TITLE { get; set; }
        [JsonProperty]
        [Reactive]
        public string URL { get; set; }
    }
}
