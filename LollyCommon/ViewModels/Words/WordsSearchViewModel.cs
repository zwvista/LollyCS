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

        public WordsSearchViewModel(SettingsViewModel vmSettings, bool needCopy, bool paginated) : base(vmSettings, needCopy, paginated)
        {
            WordItems = new ObservableCollection<MUnitWord>();
        }
    }
}
