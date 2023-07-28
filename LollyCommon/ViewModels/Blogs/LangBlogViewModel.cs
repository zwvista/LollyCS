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
    public class LangBlogViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        protected LangBlogPostDataStore postDS = new LangBlogPostDataStore();
        protected LangBlogPostContentDataStore contentDS = new LangBlogPostContentDataStore();
        protected LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        [Reactive]
        public MLangBlogPost SelectedPostItem { get; set; }
        public bool HasSelectedPostItem { [ObservableAsProperty] get; }
        [Reactive]
        public string PostContent { get; set; } = "";
        [Reactive]
        public MLangBlogGroup SelectedGroupItem { get; set; }
        public bool HasSelectedGroupItem { [ObservableAsProperty] get; }
        public ObservableCollection<MLangBlogPost> PostItems { get; set; } = new ObservableCollection<MLangBlogPost>();
        public ObservableCollection<MLangBlogGroup> GroupItems { get; set; } = new ObservableCollection<MLangBlogGroup>();
        //public string StatusText => $"{Items.Count} Textbooks in {vmSettings.LANGINFO}";
        public LangBlogViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            //this.WhenAnyValue(x => x.GroupItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
        }
        public async Task UpdateGroup(MLangBlogGroup item) => await groupDS.Update(item);
        public async Task CreateGroup(MLangBlogGroup item) => item.ID = await groupDS.Create(item);
        public async Task DeleteGroup(int id) => await groupDS.Delete(id);
        public MLangBlogGroup NewGroup() => new MLangBlogGroup
        {
            LANGID = vmSettings.SelectedLang.ID,
        };
        public async Task UpdatePost(MLangBlogPost item) => await postDS.Update(item);
        public async Task CreatePost(MLangBlogPost item) => item.ID = await postDS.Create(item);
        public async Task DeletePost(int id) => await postDS.Delete(id);
        public MLangBlogPost NewPost() => new MLangBlogPost
        {
            LANGID = vmSettings.SelectedLang.ID,
        };
    }
}
