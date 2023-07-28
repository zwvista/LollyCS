using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogSelectPostsViewModel : ReactiveObject
    {
        SettingsViewModel vmSettings;
        LangBlogPostDataStore postDS = new LangBlogPostDataStore();
        LangBlogGPDataStore gpDS = new LangBlogGPDataStore();
        public string GroupName { get; }
        public ObservableCollection<MLangBlogPost> PostsAvailable { get; private set; }
        public ObservableCollection<MLangBlogPost> PostsSelected { get; private set; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public LangBlogSelectPostsViewModel(SettingsViewModel vmSettings, MLangBlogGroup item)
        {
            this.vmSettings = vmSettings;
            GroupName = item.GROUPNAME;
            postDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PostsAvailable = new ObservableCollection<MLangBlogPost>(lst);
                this.RaisePropertyChanged(nameof(PostsAvailable));
            });
            postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, item.ID).ToObservable().Subscribe(lst =>
            {
                PostsSelected = new ObservableCollection<MLangBlogPost>(lst);
                this.RaisePropertyChanged(nameof(PostsSelected));
            });
            Save = ReactiveCommand.CreateFromTask(async () =>
            {
            });
        }
    }
}
