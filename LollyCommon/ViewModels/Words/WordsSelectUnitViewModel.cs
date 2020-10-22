using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace LollyCommon
{
    public class WordsSelectUnitViewModel : ReactiveObject
    {
        public WordsUnitViewModel vm { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        string textFilter;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        [Reactive]
        public bool InTextbook { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; }

        public WordsSelectUnitViewModel(SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            this.vmSettings = vmSettings;
            this.textFilter = textFilter;
            this.WhenAnyValue(x => x.InTextbook).Subscribe(_ => Reload());
            InTextbook = true;
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.WordItems)
                    await wordPhraseDS.Link(phraseid, o.ID);
            });
        }
        void Reload()
        {
            vm = new WordsUnitViewModel(vmSettings, InTextbook, false);
            vm.TextFilter = textFilter;
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MUnitWord> selectedItems)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
