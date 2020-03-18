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
    [JsonObject(MemberSerialization.OptOut)]
    public class MVoice : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public int VOICETYPEID { get; set; }
        [Reactive]
        public string VOICELANG { get; set; }
        [Reactive]
        public string VOICENAME { get; set; }
    }
}
