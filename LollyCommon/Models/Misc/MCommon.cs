using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public enum DictWebBrowserStatus
    {
        Ready, Navigating, Automating
    }
    public enum UnitPartToType
    {
        Unit, Part, To
    }
    public class MSelectItem
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public MSelectItem(int v, string l)
        {
            Value = v;
            Label = l;
        }
    }
    public class MCodes
    {
        [JsonProperty("records")]
        public List<MCode> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MCode : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public int CODE { get; set; }
        [JsonProperty]
        [Reactive]
        public string NAME { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class MSPResult : ReactiveObject
    {
        [JsonProperty("NEW_ID")]
        [Reactive]
        public int? NewID { get; set; }
        [JsonProperty("result")]
        [Reactive]
        public string Result { get; set; } = null!;

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
