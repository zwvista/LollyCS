using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
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
    public partial class MLangWord : ReactiveObject, MWordInterface
    {
        [JsonProperty]
        [Reactive]
        public partial int ID { get; set; }
        public int WORDID => ID;
        [JsonProperty]
        [Reactive]
        public partial int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial string WORD { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial string NOTE { get; set; } = "";
        [JsonProperty]
        [Reactive]
        public partial int FAMIID { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int CORRECT { get; set; }
        [JsonProperty]
        [Reactive]
        public partial int TOTAL { get; set; }
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";
        [Reactive]
        public partial bool IsChecked { get; set; }

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
    public partial class MLangWordEdit : ReactiveValidationObject
    {
        [Reactive]
        public partial int ID { get; set; }
        [Reactive]
        public partial string WORD { get; set; } = "";
        [Reactive]
        public partial string NOTE { get; set; }
        [Reactive]
        public partial int FAMIID { get; set; }
        public MTextbook Textbook { get; set; }
        [Reactive]
        public partial string ACCURACY { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public MLangWordEdit()
        {
            this.ValidationRule(x => x.WORD, v => !string.IsNullOrWhiteSpace(v), "WORD must not be empty");
        }
    }
}
