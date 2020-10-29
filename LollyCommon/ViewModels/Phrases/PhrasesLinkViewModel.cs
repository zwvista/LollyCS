using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace LollyCommon
{
    public class PhrasesLinkViewModel : ReactiveObject
    {
        [Reactive]
        public PhrasesLangViewModel vm { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        string textFilter;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();
        public ReactiveCommand<Unit, Unit> Save { get; }

        public PhrasesLinkViewModel(SettingsViewModel vmSettings, int wordid, string textFilter)
        {
            this.vmSettings = vmSettings;
            this.textFilter = textFilter;
            Reload();
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.PhraseItems)
                    await wordPhraseDS.Link(wordid, o.ID);
            });
        }
        void Reload()
        {
            vm = new PhrasesLangViewModel(vmSettings, false);
            vm.TextFilter = textFilter;
            foreach (var o in vm.PhraseItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MLangPhrase> selectedItems)
        {
            foreach (var o in vm.PhraseItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
