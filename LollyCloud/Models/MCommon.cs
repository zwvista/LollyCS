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
    public interface MWordInterface
    {
        int LANGID { get; set; }
        int WORDID { get; }
        string WORD { get; set; }
        string NOTE { get; set; }
        int FAMIID { get; set; }
        int LEVEL { get; set; }
    }
    public interface MPhraseInterface
    {
        int LANGID { get; set; }
        int PHRASEID { get; }
        string PHRASE { get; set; }
        string TRANSLATION { get; set; }
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
