using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WordsLangViewModel : WordsBaseViewModel
    {
        LangWordDataStore langWordDS = new LangWordDataStore();

        List<MLangWord> WordItemsAll { get; set; } = new List<MLangWord>();
        public ObservableCollection<MLangWord> WordItems { get; set; } = new ObservableCollection<MLangWord>();
        public string StatusText => $"{WordItems.Count} Words in {vmSettings.LANGINFO}";

        public WordsLangViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.TextFilter, x => x.ScopeFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.WordItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            ReloadCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                WordItemsAll = await langWordDS.GetDataByLang(vmSettings.SelectedTextbook.LANGID);
                ApplyFilters();
                IsBusy = false;
            });
            Reload();
        }
        public void Reload() => ReloadCommand.Execute().Subscribe();
        void ApplyFilters()
        {
            WordItems = new ObservableCollection<MLangWord>(string.IsNullOrEmpty(TextFilter) ? WordItemsAll : WordItemsAll.Where(o =>
                string.IsNullOrEmpty(TextFilter) || (ScopeFilter == "Word" ? o.WORD : o.NOTE ?? "").ToLower().Contains(TextFilter.ToLower())
            ));
            this.RaisePropertyChanged(nameof(WordItems));
        }

        public async Task Update(MLangWord item) => await langWordDS.Update(item);
        public async Task Create(MLangWord item)
        {
            await langWordDS.Create(item);
            WordItemsAll.Add(item);
            ApplyFilters();
        }
        public async Task Delete(MLangWord item) => await langWordDS.Delete(item);

        public MLangWord NewLangWord() =>
            new MLangWord
            {
                LANGID = vmSettings.SelectedLang.ID,
            };

    }
}
