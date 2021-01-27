using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class WordsDictViewModel
    {
        public OnlineDictViewModel vmDict { get; set; }
        public SettingsViewModel vmSettings { get; }
        [Reactive]
        public List<string> Words { get; }
        [Reactive]
        public int CurrentWordIndex { get; set; }
        public WordsDictViewModel(SettingsViewModel vmSettings, List<string> Words)
        {
            this.vmSettings = vmSettings;
            this.Words = Words;
        }

        public void InitDictViewModel(OnlineDictViewModel vmDict)
        {
            this.vmDict = vmDict;
            this.WhenAnyValue(x => x.CurrentWordIndex).Skip(1).Subscribe(async v =>
            {
                await vmDict.SearchDict();
            });
            vmSettings.WhenAnyValue(x => x.SelectedDictReference).Subscribe(async v =>
            {
                vmDict.Dict = v;
                await vmDict.SearchDict();
            });
        }

        public void Next(int delta) =>
            CurrentWordIndex = (CurrentWordIndex + delta + Words.Count) % Words.Count;
    }
}
