using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _ID;
        [JsonProperty]
        public int ID
        {
            get { return _ID; }
            set { this.RaiseAndSetIfChanged(ref _ID, value); }
        }
        private int _LANGID;
        [JsonProperty]
        public int LANGID
        {
            get { return _LANGID; }
            set { this.RaiseAndSetIfChanged(ref _LANGID, value); }
        }
        private int _TEXTBOOKID;
        [JsonProperty]
        public int TEXTBOOKID
        {
            get { return _TEXTBOOKID; }
            set { this.RaiseAndSetIfChanged(ref _TEXTBOOKID, value); }
        }
        private string _TEXTBOOKNAME;
        [JsonProperty]
        public string TEXTBOOKNAME
        {
            get { return _TEXTBOOKNAME; }
            set { this.RaiseAndSetIfChanged(ref _TEXTBOOKNAME, value); }
        }
        private int _UNIT;
        [JsonProperty]
        public int UNIT
        {
            get { return _UNIT; }
            set { this.RaiseAndSetIfChanged(ref _UNIT, value); }
        }
        private int _PART;
        [JsonProperty]
        public int PART
        {
            get { return _PART; }
            set { this.RaiseAndSetIfChanged(ref _PART, value); }
        }
        private int _SEQNUM;
        [JsonProperty]
        public int SEQNUM
        {
            get { return _SEQNUM; }
            set { this.RaiseAndSetIfChanged(ref _SEQNUM, value); }
        }
        private int _PHRASEID;
        [JsonProperty]
        public int PHRASEID
        {
            get { return _PHRASEID; }
            set { this.RaiseAndSetIfChanged(ref _PHRASEID, value); }
        }
        private string _PHRASE;
        [JsonProperty]
        public string PHRASE
        {
            get { return _PHRASE; }
            set { this.RaiseAndSetIfChanged(ref _PHRASE, value); }
        }
        private string _TRANSLATION;
        [JsonProperty]
        public string TRANSLATION
        {
            get { return _TRANSLATION; }
            set { this.RaiseAndSetIfChanged(ref _TRANSLATION, value); }
        }

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;

        public string UNITSTR => lstUnits.First(o => o.Value == UNIT).Label;
        public string PARTSTR => lstParts.First(o => o.Value == PART).Label;
    }
}
