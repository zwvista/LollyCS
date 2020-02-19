using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LollyShared
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
    public class MCode : ReactiveObject
    {
        int _CODE;
        [JsonProperty]
        public int CODE
        {
            get => _CODE;
            set => this.RaiseAndSetIfChanged(ref _CODE, value);
        }
        string _NAME;
        public string NAME
        {
            get => _NAME;
            set => this.RaiseAndSetIfChanged(ref _NAME, value);
        }
    }
}
