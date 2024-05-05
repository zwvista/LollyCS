using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class WordsDictViewModel : ReactiveObject
    {
        public OnlineDictViewModel vmDict { get; set; } = null!;
        public SettingsViewModel vmSettings { get; }
        public List<string> Words { get; }
        [Reactive]
        public int CurrentWordIndex { get; set; }
        public WordsDictViewModel(SettingsViewModel vmSettings, List<string> words, int index)
        {
            this.vmSettings = vmSettings;
            Words = words;
            CurrentWordIndex = index;
        }

        public void SetOnlineDict(IOnlineDict dict)
        {
            vmDict = new OnlineDictViewModel(vmSettings, dict);
            vmSettings.WhenAnyValue(x => x.SelectedDictReference).Where(v => v != null).Subscribe(async v =>
            {
                vmDict.Dict = v;
                await vmDict.SearchDict();
            });
            this.WhenAnyValue(x => x.CurrentWordIndex).Subscribe(async v =>
            {
                vmDict.Word = Words[CurrentWordIndex];
                await vmDict.SearchDict();
            });
        }

        public void Next(int delta) =>
            CurrentWordIndex = (CurrentWordIndex + delta + Words.Count) % Words.Count;
    }
}
