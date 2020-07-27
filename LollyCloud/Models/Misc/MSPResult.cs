using ReactiveUI;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MSPResult : ReactiveObject
    {
        [JsonProperty("NEW_ID")]
        [Reactive]
        public int? NewID { get; set; }
        [JsonProperty("result")]
        [Reactive]
        public string Result { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
