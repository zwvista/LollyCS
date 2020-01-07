using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MPatternPhrases
    {
        public List<MPatternPhrase> records { get; set; }
    }
    public class MPatternPhrase : ReactiveObject
    {
        int _PATTERNID;
        [JsonProperty]
        public int PATTERNID
        {
            get => _PATTERNID;
            set => this.RaiseAndSetIfChanged(ref _PATTERNID, value);
        }
        int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get => _LANGID;
            set => this.RaiseAndSetIfChanged(ref _LANGID, value);
        }
        string _PATTERN;
        [JsonProperty]
        public string PATTERN
        {
            get => _PATTERN;
            set => this.RaiseAndSetIfChanged(ref _PATTERN, value);
        }
        string _NOTE;
        [JsonProperty]
        public string NOTE
        {
            get => _NOTE;
            set => this.RaiseAndSetIfChanged(ref _NOTE, value);
        }
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _SEQNUM;
        [JsonProperty]
        public int SEQNUM
        {
            get => _SEQNUM;
            set => this.RaiseAndSetIfChanged(ref _SEQNUM, value);
        }
        int _PHRASEID;
        [JsonProperty]
        public int PHRASEID
        {
            get => _PHRASEID;
            set => this.RaiseAndSetIfChanged(ref _PHRASEID, value);
        }
        string _PHRASE;
        [JsonProperty]
        public string PHRASE
        {
            get => _PHRASE;
            set => this.RaiseAndSetIfChanged(ref _PHRASE, value);
        }
        string _TRANSLATION;
        [JsonProperty]
        public string TRANSLATION
        {
            get => _TRANSLATION;
            set => this.RaiseAndSetIfChanged(ref _TRANSLATION, value);
        }

    }
}
