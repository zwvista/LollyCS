using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
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
        public List<MCode> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MCode : ReactiveObject
    {
        [Reactive]
        public int CODE { get; set; }
        [Reactive]
        public string NAME { get; set; }
    }
}
