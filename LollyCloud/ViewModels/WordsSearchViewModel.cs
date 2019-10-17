using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsSearchViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;

        public ObservableCollection<MUnitWord> WordItems { get; set; }
        string _NewWord = "";
        public string NewWord
        {
            get => _NewWord;
            set => this.RaiseAndSetIfChanged(ref _NewWord, value);
        }

        // https://stackoverflow.com/questions/15907356/how-to-initialize-an-object-using-async-await-pattern
        public WordsSearchViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            WordItems = new ObservableCollection<MUnitWord>();
        }
    }
}
