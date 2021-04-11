using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class MUsers
    {
        [JsonProperty("records")]
        public List<MUser> Records { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MUser : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public string USERID { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string USERNAME { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string PASSWORD { get; set; } = "";
    }

}
