using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MAutoCorrects
    {
        public List<MAutoCorrect> AUTOCORRECT { get; set; }
    }

    public class MAutoCorrect : ReactiveObject
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
        private int _SEQNUM;
        [JsonProperty]
        public int SEQNUM
        {
            get { return _SEQNUM; }
            set { this.RaiseAndSetIfChanged(ref _SEQNUM, value); }
        }
        private string _INPUT;
        [JsonProperty]
        public string INPUT
        {
            get { return _INPUT; }
            set { this.RaiseAndSetIfChanged(ref _INPUT, value); }
        }
        private string _EXTENDED;
        [JsonProperty]
        public string EXTENDED
        {
            get { return _EXTENDED; }
            set { this.RaiseAndSetIfChanged(ref _EXTENDED, value); }
        }
        private string _BASIC;
        [JsonProperty]
        public string BASIC
        {
            get { return _BASIC; }
            set { this.RaiseAndSetIfChanged(ref _BASIC, value); }
        }

        public static string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
