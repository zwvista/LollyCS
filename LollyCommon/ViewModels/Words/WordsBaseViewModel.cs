﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsPhrasesBaseViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; }
        [Reactive]
        public string ScopeFilter { get; set; }
        [Reactive]
        public string TextFilter { get; set; } = "";
        [Reactive]
        public int TextbookFilter { get; set; }
        public bool IsBusy { get; set; } = true;
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }

        public WordsPhrasesBaseViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }
    }
    public class WordsBaseViewModel : WordsPhrasesBaseViewModel
    {
        [Reactive]
        public string NewWord { get; set; } = "";
        [Reactive]
        public MWordInterface SelectedWordItem { get; set; }
        public bool HasSelectedWordItem { [ObservableAsProperty] get; }
        public string SelectedWord => SelectedWordItem?.WORD ?? "";
        public int SelectedWordID => SelectedWordItem?.WORDID ?? 0;
        public WordsBaseViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            ScopeFilter = SettingsViewModel.ScopeWordFilters[0];
            this.WhenAnyValue(x => x.SelectedWordItem, (MWordInterface v) => v != null).ToPropertyEx(this, x => x.HasSelectedWordItem);
        }
    }
}
