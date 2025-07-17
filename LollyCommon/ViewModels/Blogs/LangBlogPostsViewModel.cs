using ReactiveUI;
using ReactiveUI.Fody.Helpers;
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
            this.WhenAnyValue(x => x.PostFilter).Subscribe(_ => ApplyPostFilter());
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe( v => ReloadGroups());
            this.WhenAnyValue(x => x.GroupFilter).Subscribe(_ => ApplyGroupFilter());
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
        void ApplyPostFilter()
        {
            PostItems = NoPostFilter ? PostItemsAll : new ObservableCollection<MLangBlogPost>(PostItemsAll.Where(o =>
                 o.TITLE.ToLower().Contains(PostFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(PostItems));
        }
        void ApplyGroupFilter()
        {
            GroupItems = NoGroupFilter ? GroupItemsAll : new ObservableCollection<MLangBlogGroup>(GroupItemsAll.Where(o =>
                 o.GROUPNAME.ToLower().Contains(GroupFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(GroupItems));
        }
    }
}
