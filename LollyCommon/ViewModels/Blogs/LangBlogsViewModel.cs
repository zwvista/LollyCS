using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class LangBlogsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        LangBlogDataStore blogDS = new LangBlogDataStore();
        LangBlogContentDataStore blogContentDS = new LangBlogContentDataStore();
        [Reactive]
        public MLangBlogGroup SelectedGroupItem { get; set; }
        public bool HasSelectedGroupItem { [ObservableAsProperty] get; }
        public ObservableCollection<MLangBlogGroup> GroupItems { get; set; } = new ObservableCollection<MLangBlogGroup>();
        [Reactive]
        public MLangBlog SelectedBlogItem { get; set; }
        public bool HasSelectedBlogItem { [ObservableAsProperty] get; }
        [Reactive]
        public string BlogContent { get; set; } = "";
        public ObservableCollection<MLangBlog> BlogItems { get; set; } = new ObservableCollection<MLangBlog>();
        //public string StatusText => $"{Items.Count} Textbooks in {vmSettings.LANGINFO}";
        public LangBlogsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            //this.WhenAnyValue(x => x.GroupItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(async v => 
            {
                var lst = await blogDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, v.ID);
                BlogItems = new ObservableCollection<MLangBlog>(lst);
                this.RaisePropertyChanged(nameof(BlogItems));
            });
            this.WhenAnyValue(x => x.SelectedBlogItem, (MLangBlog v) => v != null).ToPropertyEx(this, x => x.HasSelectedBlogItem);
            this.WhenAnyValue(x => x.SelectedBlogItem).Where(v => v != null).Subscribe(async v => 
            {
                BlogContent = (await blogContentDS.GetDataById(v.ID))?.CONTENT;
            });
            Reload();
        }
        public void Reload() =>
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupItems = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupItems));
            });
        public async Task UpdateGroup(MLangBlogGroup item) => await groupDS.Update(item);
        public async Task CreateGroup(MLangBlogGroup item) => item.ID = await groupDS.Create(item);
        public async Task DeleteGroup(int id) => await groupDS.Delete(id);
        public MLangBlogGroup NewGroup() => new MLangBlogGroup
        {
            LANGID = vmSettings.SelectedLang.ID,
        };
        public async Task UpdateBlog(MLangBlog item) => await blogDS.Update(item);
        public async Task CreateBlog(MLangBlog item) => item.ID = await blogDS.Create(item);
        public async Task DeleteBlog(int id) => await blogDS.Delete(id);
        public MLangBlog NewBlog() => new MLangBlog
        {
            LANGID = vmSettings.SelectedLang.ID,
            GROUPID = SelectedGroupItem.ID,
        };
    }
}
