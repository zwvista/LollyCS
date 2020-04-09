﻿using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    public class MVoices
    {
        public List<MVoice> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MVoice : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public int VOICETYPEID { get; set; }
        [JsonProperty]
        [Reactive]
        public string VOICELANG { get; set; }
        [JsonProperty]
        [Reactive]
        public string VOICENAME { get; set; }
    }
}
