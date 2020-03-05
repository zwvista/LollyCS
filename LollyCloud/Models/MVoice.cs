using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MVoices
    {
        public List<MVoice> records { get; set; }
    }
    public class MVoice : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int LANGID { get; set; }
        [Reactive]
        [JsonProperty]
        public int VOICETYPEID { get; set; }
        [Reactive]
        [JsonProperty]
        public string VOICELANG { get; set; }
        [Reactive]
        [JsonProperty]
        public string VOICENAME { get; set; }
    }
}
