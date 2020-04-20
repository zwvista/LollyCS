using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCloud
{
    public class MUnitWords
    {
        public List<MUnitWord> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class MUnitWord : ReactiveObject, MWordInterface
    {
        [JsonProperty]
        [Reactive]
        public int ID { get; set; }
        [JsonProperty]
        [Reactive]
        public int LANGID { get; set; }
        [JsonProperty]
        [Reactive]
        public int TEXTBOOKID { get; set; }
        [JsonProperty]
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        [JsonProperty]
        [Reactive]
        public int UNIT { get; set; }
        [JsonProperty]
        [Reactive]
        public int PART { get; set; }
        [JsonProperty]
        [Reactive]
        public int SEQNUM { get; set; }
        [JsonProperty]
        [Reactive]
        public int WORDID { get; set; }
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
        [Reactive]
        public bool IsChecked { get; set; }

        public MTextbook Textbook { get; set; }

        public string UNITSTR => Textbook.UNITSTR(UNIT);
        public string PARTSTR => Textbook.PARTSTR(PART);
        public string ACCURACY => TOTAL == 0 ? "N/A" : $"{Math.Floor((double)CORRECT / TOTAL * 1000) / 10}%";

        public MUnitWord()
        {
            this.WhenAnyValue(x => x.LEVEL, v => v != 0).ToPropertyEx(this, x => x.LevelNotZero);
        }
    }
    public class MUnitWord2 : ReactiveValidationObject2<MUnitWord, MUnitWord2>
    {
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public MUnitWord2()
        {
            this.ValidationRule(x => x.VM.WORD, v => !string.IsNullOrWhiteSpace(v), "WORD must not be empty");
            Save = ReactiveCommand.Create(() => { }, this.IsValid());
        }
    }
}
