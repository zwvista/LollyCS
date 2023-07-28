using ReactiveUI;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogSelectGroupsViewModel : ReactiveObject
    {
        SettingsViewModel vmSettings;
        LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        LangBlogGPDataStore gpDS = new LangBlogGPDataStore();
        public string PostTitle { get; }
        public ObservableCollection<MLangBlogGroup> GroupsAvailable { get; private set; }
        public ObservableCollection<MLangBlogGroup> GroupsSelected { get; private set; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public LangBlogSelectGroupsViewModel(SettingsViewModel vmSettings, MLangBlogPost item)
        {
            this.vmSettings = vmSettings;
            PostTitle = item.TITLE;
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupsAvailable = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupsAvailable));
            });
            groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, item.ID).ToObservable().Subscribe(lst =>
            {
                GroupsSelected = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupsSelected));
            });
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
            });
        }
    }
}
