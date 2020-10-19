﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;

namespace LollyCommon
{
    public class WordsSelectViewModel : ReactiveObject
    {
        public WordsUnitViewModel vm { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        string textFilter;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();

        [Reactive]
        public bool InTextbook { get; set; } = true;
        public ReactiveCommand<Unit, Unit> Save { get; }

        public WordsSelectViewModel(SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            this.vmSettings = vmSettings;
            this.textFilter = textFilter;
            Reload();
            this.WhenAnyValue(x => x.InTextbook).Skip(1).Subscribe(_ => Reload());
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.WordItems)
                    await wordPhraseDS.Connect(phraseid, o.ID);
            });
        }
        public void Reload()
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
