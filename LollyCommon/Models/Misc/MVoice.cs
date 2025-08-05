using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class MVoices
    {
        [JsonProperty("records")]
        public List<MVoice> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MVoice : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int VOICETYPEID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string VOICELANG { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string VOICENAME { get; set; } = "";
    }
}
