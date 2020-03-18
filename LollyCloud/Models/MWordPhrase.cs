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
    [JsonObject(MemberSerialization.OptOut)]
    public class MWordPhrase : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int WORDID { get; set; }
        [Reactive]
        public int PHRASEID { get; set; }

    }
}
