using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class MUnitWords
    {
        public List<MUnitWord> records { get; set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class MUnitWord : ReactiveObject, MWordInterface
    {
        [Reactive]
        public int ID { get; set; }
        [Reactive]
        public int LANGID { get; set; }
        [Reactive]
        public int TEXTBOOKID { get; set; }
        [Reactive]
        public string TEXTBOOKNAME { get; set; }
        [Reactive]
        public int UNIT { get; set; }
        [Reactive]
        public int PART { get; set; }
        [Reactive]
        public int SEQNUM { get; set; }
        [Reactive]
        public int WORDID { get; set; }
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
}
