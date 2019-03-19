using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUnitPhrases
    {
        public List<MUnitPhrase> VUNITPHRASES { get; set; }
    }
    public class MUnitPhrase : ReactiveObject
    {
        int _ID;
        [JsonProperty]
        public int ID
        {
            get => _ID;
            set => this.RaiseAndSetIfChanged(ref _ID, value);
        }
        int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get => _LANGID;
            set => this.RaiseAndSetIfChanged(ref _LANGID, value);
        }
        int _TEXTBOOKID;
        [JsonProperty]
        public int TEXTBOOKID
        {
            get => _TEXTBOOKID;
            set => this.RaiseAndSetIfChanged(ref _TEXTBOOKID, value);
        }
        string _TEXTBOOKNAME;
        [JsonProperty]
        public string TEXTBOOKNAME
        {
            get => _TEXTBOOKNAME;
            set => this.RaiseAndSetIfChanged(ref _TEXTBOOKNAME, value);
        }
        int _UNIT;
        [JsonProperty]
        public int UNIT
        {
            get => _UNIT;
            set => this.RaiseAndSetIfChanged(ref _UNIT, value);
        }
        int _PART;
        [JsonProperty]
        public int PART
        {
            get => _PART;
            set => this.RaiseAndSetIfChanged(ref _PART, value);
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

        public List<MSelectItem> lstUnits;
        public List<MSelectItem> lstParts;

        public string UNITSTR => lstUnits.First(o => o.Value == UNIT).Label;
        public string PARTSTR => lstParts.First(o => o.Value == PART).Label;
    }
}
