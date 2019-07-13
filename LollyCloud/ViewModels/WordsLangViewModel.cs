using ReactiveUI;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsLangViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();

        public ObservableCollection<MLangWord> Items { get; set; }
        string _NewWord = "";
        public string NewWord
        {
            get => _NewWord;
            set => this.RaiseAndSetIfChanged(ref _NewWord, value);
        }

        public static async Task<WordsLangViewModel> CreateAsync(SettingsViewModel vmSettings)
        {
            var o = new WordsLangViewModel();
            o.vmSettings = vmSettings;
            o.Items = new ObservableCollection<MLangWord>(await o.langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            return o;
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task<int> Create(MLangWord item) => await langWordDS.Create(item);
        public async Task Delete(int id) => await langWordDS.Delete(id);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
