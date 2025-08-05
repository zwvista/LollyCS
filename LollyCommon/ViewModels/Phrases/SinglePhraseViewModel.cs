using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public partial class SinglePhraseViewModel : ReactiveObject
    {
        UnitPhraseDataStore unitPhraseDS = new();

        public ObservableCollection<MUnitPhrase> PhraseItems { get; private set; } = new ObservableCollection<MUnitPhrase>();

        public SinglePhraseViewModel(string phrase, SettingsViewModel vmSettings) =>
            unitPhraseDS.GetDataByLangPhrase(vmSettings.SelectedLang.ID, phrase, vmSettings.Textbooks).ToObservable().Subscribe(lst =>
            {
                PhraseItems = new ObservableCollection<MUnitPhrase>(lst);
                this.RaisePropertyChanged(nameof(PhraseItems));
            });
    }
}
