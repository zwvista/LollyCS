using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace LollyCloud
{
    public class MWordsFami
    {
        public List<MWordFami> records { get; set; }
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
        public int LEVEL { get; set; }
        [JsonProperty]
        [Reactive]
        public int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public int TOTAL { get; set; }
    }
}
