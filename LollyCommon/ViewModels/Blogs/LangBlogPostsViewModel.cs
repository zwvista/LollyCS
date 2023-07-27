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
    public class LangBlogPostsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        LangBlogPostDataStore postDS = new LangBlogPostDataStore();
        LangBlogPostContentDataStore contentDS = new LangBlogPostContentDataStore();
        [Reactive]
        public MLangBlogGroup SelectedGroupItem { get; set; }
        public bool HasSelectedGroupItem { [ObservableAsProperty] get; }
        public ObservableCollection<MLangBlogGroup> GroupItems { get; set; } = new ObservableCollection<MLangBlogGroup>();
        [Reactive]
        public MLangBlogPost SelectedPostItem { get; set; }
        public bool HasSelectedPostItem { [ObservableAsProperty] get; }
        [Reactive]
        public string BlogContent { get; set; } = "";
        public ObservableCollection<MLangBlogPost> PostItems { get; set; } = new ObservableCollection<MLangBlogPost>();
        //public string StatusText => $"{Items.Count} Textbooks in {vmSettings.LANGINFO}";
        public LangBlogPostsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            //this.WhenAnyValue(x => x.GroupItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
            this.WhenAnyValue(x => x.SelectedGroupItem).Where(v => v != null).Subscribe(async v => 
            {
                var lst = await postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, v.ID);
                PostItems = new ObservableCollection<MLangBlogPost>(lst);
                this.RaisePropertyChanged(nameof(PostItems));
            });
            this.WhenAnyValue(x => x.SelectedPostItem, (MLangBlogPost v) => v != null).ToPropertyEx(this, x => x.HasSelectedPostItem);
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                BlogContent = (await contentDS.GetDataById(v.ID))?.CONTENT;
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
        public async Task UpdateBlog(MLangBlogPost item) => await postDS.Update(item);
        public async Task CreateBlog(MLangBlogPost item) => item.ID = await postDS.Create(item);
        public async Task DeleteBlog(int id) => await postDS.Delete(id);
        public MLangBlogPost NewBlog() => new MLangBlogPost
        {
            LANGID = vmSettings.SelectedLang.ID,
        };
    }
}
