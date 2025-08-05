using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public class WordsSearchViewModel : WordsBaseViewModel
    {
        public ObservableCollection<MUnitWord> WordItems { get; set; }

        public WordsSearchViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            WordItems = new ObservableCollection<MUnitWord>();
        }
    }
}
