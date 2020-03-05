using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace LollyShared
{
    public class MWordsFami
    {
        public List<MWordFami> records { get; set; }
    }
    public class MWordFami : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int USERID { get; set; }
        [Reactive]
        [JsonProperty]
        public int WORDID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LEVEL { get; set; }
        [Reactive]
        [JsonProperty]
        public int CORRECT { get; set; }
        [Reactive]
        [JsonProperty]
        public int TOTAL { get; set; }
    }
}
