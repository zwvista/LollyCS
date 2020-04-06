using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MLangWords
    {
        public List<MLangWord> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MLangWord : ReactiveObject, MWordInterface
    {
        [Reactive]
        public int ID { get; set; }
        public int WORDID => ID;
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public string WORD { get; set; }
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public int FAMIID { get; set; }
        [Reactive]
        public int LEVEL { get; set; }
        public bool LevelNotZero { [ObservableAsProperty] get; }
        [Reactive]
        public int CORRECT { get; set; }
        [Reactive]
        public int TOTAL { get; set; }
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";

        void WhenAnyValueChanged()
        {
            this.WhenAnyValue(x => x.LEVEL, v => v != 0).ToPropertyEx(this, x => x.LevelNotZero);
        }
        public MLangWord()
        {
            WhenAnyValueChanged();
        }
        public MLangWord(MUnitWord item)
        {
            ID = item.WORDID;
            LANGID = item.LANGID;
            WORD = item.WORD;
            NOTE = item.NOTE;
            WhenAnyValueChanged();
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
