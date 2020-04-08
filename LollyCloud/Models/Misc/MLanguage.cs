using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MLanguages
    {
        public List<MLanguage> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MLanguage : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty("NAME")]
        public string LANGNAME { get; set; }
    }
}
