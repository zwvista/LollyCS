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
    [JsonObject(MemberSerialization.OptOut)]
    public class MWordFami : ReactiveObject
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int USERID { get; set; }
        [Reactive]
        public int WORDID { get; set; }
        [Reactive]
        public int LEVEL { get; set; }
        [Reactive]
        public int CORRECT { get; set; }
        [Reactive]
        public int TOTAL { get; set; }
    }
}
