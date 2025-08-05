using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

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
        public List<MCode> Records { get; set; } = null!;
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MCode : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int CODE { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string NAME { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class MSPResult : ReactiveObject
    {
        [JsonProperty("NEW_ID")]
        [Reactive]
        public partial int? NewID { get; set; }
        [JsonProperty("result")]
        [Reactive]
        public partial string Result { get; set; } = null!;

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
