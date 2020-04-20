using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCloud
{
    public class MLangWords
    {
        public List<MLangWord> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MLangWord : ReactiveObject, MWordInterface
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        public int WORDID => ID;
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public string WORD { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public string NOTE { get; set; }
        [JsonProperty]
        [Reactive]
        public int FAMIID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LEVEL { get; set; }
        public bool LevelNotZero { [ObservableAsProperty] get; }
        [JsonProperty]
        [Reactive]
        public int CORRECT { get; set; }
        [JsonProperty]
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
    public class MLangWord2 : ReactiveValidationObject2<MLangWord>
    {
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MLangWord2()
        {
            this.ValidationRule(x => x.VM.WORD, v => !string.IsNullOrWhiteSpace(v), "WORD must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
