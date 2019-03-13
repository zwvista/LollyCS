using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MLangWords
    {
        public List<MLangWord> VLANGWORDS { get; set; }
    }
    public class MLangWord : ReactiveObject
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

        public MLangWord() { }
        public MLangWord(MUnitWord item)
        {
            ID = item.WORDID;
            LANGID = item.LANGID;
            WORD = item.WORD;
            NOTE = item.NOTE;
        }

        public bool CombineNote(string note)
        {
            var oldNote = NOTE;
            if (!string.IsNullOrEmpty(note))
                if (string.IsNullOrEmpty(NOTE))
                    NOTE = note;
                else
                {
                    var lst = NOTE.Split(',').ToList();
                    if (!lst.Contains(note))
                        lst.Add(note);
                    NOTE = string.Join(",", lst);
                }
            return oldNote != NOTE;
        }
    }
}
