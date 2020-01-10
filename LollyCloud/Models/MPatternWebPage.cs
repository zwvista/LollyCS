using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MPatternWebPages
    {
        public List<MPatternWebPage> records { get; set; }
    }
    public class MPatternWebPage : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
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
        int _SEQNUM;
        [JsonProperty]
        public int SEQNUM
        {
            get => _SEQNUM;
            set => this.RaiseAndSetIfChanged(ref _SEQNUM, value);
        }
        string _WEBPAGE;
        [JsonProperty]
        public string WEBPAGE
        {
            get => _WEBPAGE;
            set => this.RaiseAndSetIfChanged(ref _WEBPAGE, value);
        }

    }
}
