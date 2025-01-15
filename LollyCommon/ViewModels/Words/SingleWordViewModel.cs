using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class SingleWordViewModel : ReactiveObject
    {
        UnitWordDataStore unitWordDS = new();

        public ObservableCollection<MUnitWord> WordItems { get; private set; } = new ObservableCollection<MUnitWord>();

        public SingleWordViewModel(string word, SettingsViewModel vmSettings) =>
            unitWordDS.GetDataByLangWord(vmSettings.SelectedLang.ID, word, vmSettings.Textbooks).ToObservable().Subscribe(lst =>
            {
                WordItems = new ObservableCollection<MUnitWord>(lst);
                this.RaisePropertyChanged(nameof(WordItems));
            });
    }
}
