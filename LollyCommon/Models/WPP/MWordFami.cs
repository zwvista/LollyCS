using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.Generic;

namespace LollyCommon
{
    public class MWordsFami
    {
        [JsonProperty("records")]
        public List<MWordFami> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MWordFami : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string USERID { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int WORDID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int TOTAL { get; set; }
    }
}
