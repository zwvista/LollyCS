using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class MLangWords
    {
        [JsonProperty("records")]
        public List<MLangWord> Records { get; set; }
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
        public string NOTE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public int FAMIID { get; set; }
        [JsonProperty]
        [Reactive]
        public int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public int TOTAL { get; set; }
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";

        void WhenAnyValueChanged()
        {
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

        public bool MergeNote(string note)
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
    public class MLangWordEdit : ReactiveValidationObject<MLangWordEdit>
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public string WORD { get; set; } = "";
        [Reactive]
        public string NOTE { get; set; }
        [Reactive]
        public int FAMIID { get; set; }
        public MTextbook Textbook { get; set; }
        [Reactive]
        public string ACCURACY { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangWordEdit()
        {
            this.ValidationRule(x => x.WORD, v => !string.IsNullOrWhiteSpace(v), "WORD must not be empty");
        }
    }
}
