using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogGroupsViewModel : LangBlogViewModel
    {
        public LangBlogGroupsViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(v => ReloadPosts());
            ReloadGroupsCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                GroupItemsAll = new ObservableCollection<MLangBlogGroup>(await groupDS.GetDataByLang(vmSettings.SelectedLang.ID));
                ApplyGroupFilter();
                IsBusy = false;
            });
            ReloadPostsCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PostItemsAll = new ObservableCollection<MLangBlogPost>(await postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, SelectedGroupItem!.ID));
                ApplyPostFilter();
                IsBusy = false;
            });
            ReloadGroups();
        }
        public void ReloadGroups() => ReloadGroupsCommand.Execute().Subscribe();
        public void ReloadPosts() => ReloadPostsCommand.Execute().Subscribe();
    }
}
