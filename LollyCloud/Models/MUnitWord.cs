using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MUnitWords
    {
        public List<MUnitWord> VUNITWORDS { get; set; }
    }
    public class MUnitWord : ReactiveObject
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
        private int _WORDID;
        [JsonProperty]
        public int WORDID
        {
            get { return _WORDID; }
            set { this.RaiseAndSetIfChanged(ref _WORDID, value); }
        }
        private string _WORD;
        [JsonProperty]
        public string WORD
        {
            get { return _WORD; }
            set { this.RaiseAndSetIfChanged(ref _WORD, value); }
        }
        private string _NOTE;
        [JsonProperty]
        public string NOTE
        {
            get { return _NOTE; }
            set { this.RaiseAndSetIfChanged(ref _NOTE, value); }
        }
        private int _FAMIID;
        [JsonProperty]
        public int FAMIID
        {
            get { return _FAMIID; }
            set { this.RaiseAndSetIfChanged(ref _FAMIID, value); }
        }
        private int _LEVEL;
        [JsonProperty]
        public int LEVEL
        {
            get { return _LEVEL; }
            set { this.RaiseAndSetIfChanged(ref _LEVEL, value); }
        }

        public ObservableCollection<MSelectItem> lstUnits;
        public ObservableCollection<MSelectItem> lstParts;

        public string UNITSTR => lstUnits.First(o => o.Value == UNIT).Label;
        public string PARTSTR => lstParts.First(o => o.Value == PART).Label;
    }
}
