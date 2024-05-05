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
    public class LangBlogPostsViewModel : LangBlogViewModel
    {
        public LangBlogPostsViewModel(SettingsViewModel vmSettings, bool needCopy) : base(vmSettings, needCopy)
        {
            this.WhenAnyValue(x => x.SelectedPostItem, (MLangBlogPost v) => v != null).ToPropertyEx(this, x => x.HasSelectedPostItem);
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                PostContent = (await contentDS.GetDataById(v.ID))?.CONTENT ?? "";
                var lst = await groupDS.GetDataByLangPost(vmSettings.SelectedLang.ID, v.ID);
                GroupItems = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupItems));
            });
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
            Reload();
        }
        public void Reload() =>
            postDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                PostItems = new ObservableCollection<MLangBlogPost>(lst);
                this.RaisePropertyChanged(nameof(PostItems));
            });
    }
}
