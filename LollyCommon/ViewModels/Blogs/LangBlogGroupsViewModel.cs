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
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup? v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(async v => 
            {
                var lst = await postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, v!.ID);
                PostItemsAll = new ObservableCollection<MLangBlogPost>(lst);
                ApplyFilters();
                this.RaisePropertyChanged(nameof(PostItems));
            });
            this.WhenAnyValue(x => x.PostFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.SelectedPostItem, (MLangBlogPost? v) => v != null).ToPropertyEx(this, x => x.HasSelectedPostItem);
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                PostContent = (await contentDS.GetDataById(v!.ID))?.CONTENT ?? "";
            });
            Reload();
        }
        public void Reload() =>
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupItems = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupItems));
            });
        void ApplyFilters()
        {
            PostItems = NoPostFilter ? PostItemsAll : new ObservableCollection<MLangBlogPost>(PostItemsAll.Where(o =>
                 o.TITLE.ToLower().Contains(PostFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(PostItems));
        }
    }
}
