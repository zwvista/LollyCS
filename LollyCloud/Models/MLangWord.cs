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
