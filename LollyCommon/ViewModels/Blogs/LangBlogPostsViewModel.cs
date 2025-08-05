using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogPostsViewModel : LangBlogViewModel
    {
        public LangBlogPostsViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe( v => ReloadGroups());
            ReloadPostsCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                PostItemsAll = new ObservableCollection<MLangBlogPost>(await postDS.GetDataByLang(vmSettings.SelectedLang.ID));
                ApplyPostFilter();
                IsBusy = false;
            });
            ReloadGroupsCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsBusy = true;
                GroupItemsAll = new ObservableCollection<MLangBlogGroup>(await groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, SelectedPostItem!.ID));
                ApplyGroupFilter();
                IsBusy = false;
            });
            ReloadPosts();
        }
        public void ReloadPosts() => ReloadPostsCommand.Execute().Subscribe();
        public void ReloadGroups() => ReloadGroupsCommand.Execute().Subscribe();
    }
}
