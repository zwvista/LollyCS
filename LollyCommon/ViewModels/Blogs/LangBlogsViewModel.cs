using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;

namespace LollyCommon
{
    public class LangBlogsViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        LangBlogGroupDataStore groupDS = new LangBlogGroupDataStore();
        LangBlogDataStore blogDS = new LangBlogDataStore();
        [Reactive]
        public MLangBlogGroup SelectedGroupItem { get; set; }
        public bool HasSelectedGroupItem { [ObservableAsProperty] get; }
        public ObservableCollection<MLangBlogGroup> GroupItems { get; set; } = new ObservableCollection<MLangBlogGroup>();
        public ObservableCollection<MLangBlog> BlogItems { get; set; } = new ObservableCollection<MLangBlog>();
        //public string StatusText => $"{Items.Count} Textbooks in {vmSettings.LANGINFO}";
        public LangBlogsViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            //this.WhenAnyValue(x => x.GroupItems).Subscribe(_ => this.RaisePropertyChanged(nameof(StatusText)));
            this.WhenAnyValue(x => x.SelectedGroupItem, (MLangBlogGroup v) => v != null).ToPropertyEx(this, x => x.HasSelectedGroupItem);
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
