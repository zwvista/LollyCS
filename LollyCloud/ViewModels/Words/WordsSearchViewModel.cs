using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using ReactiveUI.Fody.Helpers;

namespace LollyShared
{
    public class WordsSearchViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;

        public ObservableCollection<MUnitWord> WordItems { get; set; }
        [Reactive]
        public string NewWord { get; set; } = "";

        public WordsSearchViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            WordItems = new ObservableCollection<MUnitWord>();
        }
    }
}
