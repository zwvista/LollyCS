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
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                var lst = await groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, v!.ID);
                GroupItemsAll = new ObservableCollection<MLangBlogGroup>(lst);
                ApplyGroupFilter();
                this.RaisePropertyChanged(nameof(GroupItems));
            });
            Reload();
        }
        public void Reload() =>
            postDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PostItemsAll = new ObservableCollection<MLangBlogPost>(lst);
                ApplyPostFilter();
                this.RaisePropertyChanged(nameof(PostItems));
            });
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
