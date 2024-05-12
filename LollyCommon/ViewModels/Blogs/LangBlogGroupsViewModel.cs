using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
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
                var lst = await postDS.GetDataByLangGroup(vmSettings.SelectedLang.ID, v.ID);
                PostItems = new ObservableCollection<MLangBlogPost>(lst);
                this.RaisePropertyChanged(nameof(PostItems));
            });
            this.WhenAnyValue(x => x.SelectedPostItem, (MLangBlogPost? v) => v != null).ToPropertyEx(this, x => x.HasSelectedPostItem);
            this.WhenAnyValue(x => x.SelectedPostItem).Where(v => v != null).Subscribe(async v => 
            {
                PostContent = (await contentDS.GetDataById(v.ID))?.CONTENT ?? "";
            });
            Reload();
        }
        public void Reload() =>
            groupDS.GetDataByLang(vmSettings.SelectedLang.ID).ToObservable().Subscribe(lst =>
            {
                GroupItems = new ObservableCollection<MLangBlogGroup>(lst);
                this.RaisePropertyChanged(nameof(GroupItems));
            });
    }
}
