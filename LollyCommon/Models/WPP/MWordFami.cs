using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace LollyCommon
{
    public class MWordsFami
    {
        [JsonProperty("records")]
        public List<MWordFami> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MWordFami : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int USERID { get; set; }
        [JsonProperty]
        [Reactive]
        public int WORDID { get; set; }
        [JsonProperty]
        [Reactive]
        public int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public int TOTAL { get; set; }
    }
}
