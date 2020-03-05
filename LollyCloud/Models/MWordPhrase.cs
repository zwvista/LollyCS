using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MWordsPhrases
    {
        public List<MWordPhrase> records { get; set; }
    }
    public class MWordPhrase : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty]
        public int WORDID { get; set; }
        [Reactive]
        [JsonProperty]
        public int PHRASEID { get; set; }

    }
}
