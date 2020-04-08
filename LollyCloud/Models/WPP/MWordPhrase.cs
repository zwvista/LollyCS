using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;

namespace LollyCloud
{
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
