using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

namespace LollyShared
{
    public class WordsLangViewMode : LollyViewModel
    {
        SettingsViewModel vmSettings;
        LangWordDataStore langWordDS = new LangWordDataStore();

        public ObservableCollection<MLangWord> LangWords { get; set; }

        public static async Task<WordsLangViewMode> CreateAsync(SettingsViewModel vmSettings)
        {
            var o = new WordsLangViewMode();
            o.vmSettings = vmSettings;
            o.LangWords = new ObservableCollection<MLangWord>(await o.langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID));
            return o;
        }

        public async Task<bool> Update(MLangWord item) => await langWordDS.Update(item);
        public async Task<int> Create(MLangWord item) => await langWordDS.Create(item);
        public async Task<bool> Delete(int id) => await langWordDS.Delete(id);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };
    }
}
