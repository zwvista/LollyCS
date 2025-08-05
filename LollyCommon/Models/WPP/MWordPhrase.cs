using ReactiveUI;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public interface MWordInterface
    {
        int LANGID { get; set; }
        int WORDID { get; }
        string WORD { get; set; }
        string NOTE { get; set; }
        int FAMIID { get; set; }
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
        [JsonProperty("records")]
        public List<MWordPhrase> Records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public partial class MWordPhrase : ReactiveObject
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int WORDID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int PHRASEID { get; set; }

    }
}
