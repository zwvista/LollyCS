using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MLanguages
    {
        public List<MLanguage> records { get; set; }
    }
    public class MLanguage : ReactiveObject
    {
        [Reactive]
        [JsonProperty]
        public int ID { get; set; }
        [Reactive]
        [JsonProperty("NAME")]
        public string LANGNAME { get; set; }
    }
}
