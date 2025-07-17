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
            this.WhenAnyValue(x => x.GroupFilter).Subscribe(_ => ApplyGroupFilter());
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(v => ReloadPosts());
            this.WhenAnyValue(x => x.PostFilter).Subscribe(_ => ApplyPostFilter());
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
        void ApplyGroupFilter()
        {
            GroupItems = NoGroupFilter ? GroupItemsAll : new ObservableCollection<MLangBlogGroup>(GroupItemsAll.Where(o =>
                 o.GROUPNAME.ToLower().Contains(GroupFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(GroupItems));
        }
        void ApplyPostFilter()
        {
            PostItems = NoPostFilter ? PostItemsAll : new ObservableCollection<MLangBlogPost>(PostItemsAll.Where(o =>
                 o.TITLE.ToLower().Contains(PostFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(PostItems));
        }
    }
}
