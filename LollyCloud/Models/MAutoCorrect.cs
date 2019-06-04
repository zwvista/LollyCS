using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LollyShared
{
    public class MAutoCorrects
    {
        public List<MAutoCorrect> records { get; set; }
    }

    public class MAutoCorrect : ReactiveObject
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
        int _SEQNUM;
        [JsonProperty]
        public int SEQNUM
        {
            get => _SEQNUM;
            set => this.RaiseAndSetIfChanged(ref _SEQNUM, value);
        }
        string _INPUT;
        [JsonProperty]
        public string INPUT
        {
            get => _INPUT;
            set => this.RaiseAndSetIfChanged(ref _INPUT, value);
        }
        string _EXTENDED;
        [JsonProperty]
        public string EXTENDED
        {
            get => _EXTENDED;
            set => this.RaiseAndSetIfChanged(ref _EXTENDED, value);
        }
        string _BASIC;
        [JsonProperty]
        public string BASIC
        {
            get => _BASIC;
            set => this.RaiseAndSetIfChanged(ref _BASIC, value);
        }

        public static string AutoCorrect(string text, List<MAutoCorrect> lstAutoCorrects,
                                  Func<MAutoCorrect, string> colFunc1, Func<MAutoCorrect, string> colFunc2) =>
        lstAutoCorrects.Aggregate(text, (str, row) => str.Replace(colFunc1(row), colFunc2(row)));
    }

}
