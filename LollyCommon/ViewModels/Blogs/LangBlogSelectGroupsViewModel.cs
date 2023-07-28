using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace LollyCommon
{
    public class LangBlogSelectGroupsViewModel
    {
        SettingsViewModel vmSettings;
        public ObservableCollection<MDictionary> DictsAvailable { get; }
        public ObservableCollection<MDictionary> DictsSelected { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public LangBlogSelectGroupsViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
            DictsSelected = new ObservableCollection<MDictionary>(vmSettings.SelectedDictsReference);
            DictsAvailable = new ObservableCollection<MDictionary>(vmSettings.DictsReference.Except(vmSettings.SelectedDictsReference));
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
                await vmSettings.UpdateDictsReference(DictsSelected.ToList());
            });
        }
    }
}
