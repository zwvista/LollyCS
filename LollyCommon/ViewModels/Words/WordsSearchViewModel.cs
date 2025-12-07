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

        public WordsSearchViewModel(SettingsViewModel vmSettings, bool needCopy, bool paged) : base(vmSettings, needCopy, paged)
        {
            WordItems = new ObservableCollection<MUnitWord>();
        }
    }
}
