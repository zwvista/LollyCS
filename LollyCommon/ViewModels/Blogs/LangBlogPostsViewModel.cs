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
        [Reactive]
        public string TitleFilter { get; set; } = "";
        public bool NoFilter => string.IsNullOrEmpty(TitleFilter);
        public LangBlogPostsViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.TitleFilter).Subscribe(_ => ApplyFilters());
            this.WhenAnyValue(x => x.SelectedPostItem, (MLangBlogPost? v) => v != null).ToPropertyEx(this, x => x.HasSelectedPostItem);
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                PostContent = (await contentDS.GetDataById(v.ID))?.CONTENT ?? "";
                var lst = await groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, v.ID);
                GroupItems = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupItems));
            });
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup? v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
            Reload();
        }
        public void Reload() =>
            postDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PostItemsAll = new ObservableCollection<MLangBlogPost>(lst);
                ApplyFilters();
                this.RaisePropertyChanged(nameof(PostItems));
            });
        void ApplyFilters()
        {
            PostItems = NoFilter ? PostItemsAll : new ObservableCollection<MLangBlogPost>(PostItemsAll.Where(o =>
                 o.TITLE.ToLower().Contains(TitleFilter.ToLower()))
            );
            this.RaisePropertyChanged(nameof(PostItems));
        }
    }
}
