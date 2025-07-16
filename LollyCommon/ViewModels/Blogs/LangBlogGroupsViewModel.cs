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
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(async v => 
            {
                var lst = await postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, v!.ID);
                PostItemsAll = new ObservableCollection<MLangBlogPost>(lst);
                ApplyPostFilter();
                this.RaisePropertyChanged(nameof(PostItems));
            });
            this.WhenAnyValue(x => x.PostFilter).Subscribe(_ => ApplyPostFilter());
            Reload();
        }
        public void Reload() =>
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupItemsAll = new ObservableCollection<MLangBlogGroup>(lst);
                ApplyGroupFilter();
                this.RaisePropertyChanged(nameof(GroupItems));
            });
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
