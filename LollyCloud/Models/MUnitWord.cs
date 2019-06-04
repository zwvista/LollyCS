using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUnitWords
    {
        public List<MUnitWord> records { get; set; }
    }
    public class MUnitWord : ReactiveObject
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
        int _WORDID;
        [JsonProperty]
        public int WORDID
        {
            get => _WORDID;
            set => this.RaiseAndSetIfChanged(ref _WORDID, value);
        }
        string _WORD;
        [JsonProperty]
        public string WORD
        {
            get => _WORD;
            set => this.RaiseAndSetIfChanged(ref _WORD, value);
        }
        string _NOTE;
        [JsonProperty]
        public string NOTE
        {
            get => _NOTE;
            set => this.RaiseAndSetIfChanged(ref _NOTE, value);
        }
        int _FAMIID;
        [JsonProperty]
        public int FAMIID
        {
            get => _FAMIID;
            set => this.RaiseAndSetIfChanged(ref _FAMIID, value);
        }
        int _LEVEL;
        [JsonProperty]
        public int LEVEL
        {
            get => _LEVEL;
            set => this.RaiseAndSetIfChanged(ref _LEVEL, value);
        }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
    }
}
