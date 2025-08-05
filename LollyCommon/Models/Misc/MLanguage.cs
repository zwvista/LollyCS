using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MLanguages
    {
        [JsonProperty("records")]
        public List<MLanguage> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MLanguage : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty("NAME")]
        [Reactive]
        public partial string LANGNAME { get; set; } = "";
    }
}
