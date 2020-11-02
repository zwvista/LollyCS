using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;

namespace LollyCommon
{
    public class WordsLinkViewModel : ReactiveObject
    {
        public WordsLangViewModel vm { get; set; }
        public SettingsViewModel vmSettings { get; set; }
        string textFilter;
        WordPhraseDataStore wordPhraseDS = new WordPhraseDataStore();
        public ReactiveCommand<Unit, Unit> Save { get; }

        public WordsLinkViewModel(SettingsViewModel vmSettings, int phraseid, string textFilter)
        {
            this.vmSettings = vmSettings;
            this.textFilter = textFilter;
            Reload();
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                foreach (var o in vm.WordItems)
                    if (o.IsChecked)
                        await wordPhraseDS.Link(phraseid, o.ID);
            });
        }
        void Reload()
        {
            vm = new WordsLangViewModel(vmSettings, false);
            vm.TextFilter = textFilter;
            foreach (var o in vm.WordItems)
                o.IsChecked = false;
        }

        public void CheckItems(int n, List<MLangWord> selectedItems)
        {
            foreach (var o in vm.WordItems)
                o.IsChecked = n == 0 ? true : n == 1 ? false :
                    !selectedItems.Contains(o) ? o.IsChecked :
                    n == 2;
        }
    }
}
