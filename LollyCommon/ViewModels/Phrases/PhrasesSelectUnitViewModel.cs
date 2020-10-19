using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace LollyCommon
{
    public class PhrasesSelectUnitViewModel : ReactiveObject
    {
        [Reactive]
        public PhrasesUnitViewModel vm { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        string textFilter;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        [Reactive]
        public bool InTextbook { get; set; } = true;
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PhrasesSelectUnitViewModel(SettingsViewModel vmSettings, int wordid, string textFilter)
        {
            this.vmSettings = vmSettings;
            this.textFilter = textFilter;
            Reload();
            this.WhenAnyValue(x => x.InTextbook).Subscribe(_ => Reload());
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.PhraseItems)
                    await wordPhraseDS.Connect(wordid, o.ID);
            });
        }
        public void Reload()
        {
            vm = new PhrasesUnitViewModel(vmSettings, InTextbook, false);
            vm.TextFilter = textFilter;
            foreach (var o in vm.PhraseItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MUnitPhrase> selectedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
