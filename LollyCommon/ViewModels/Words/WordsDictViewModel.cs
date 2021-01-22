using System;
using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class WordsDictViewModel
    {
        public SettingsViewModel vmSettings { get; }
        [Reactive]
        public List<string> Words { get; set; } = new List<string>();
        [Reactive]
        public int CurrentWordIndex { get; set; }
        public WordsDictViewModel()
        {
        }

        public void Next(int delta) =>
            CurrentWordIndex = (CurrentWordIndex + delta + Words.Count) % Words.Count;
    }
}
