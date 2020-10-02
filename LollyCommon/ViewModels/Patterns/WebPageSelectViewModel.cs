using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class WebPageSelectViewModel : ReactiveObject
    {
        WebPageDataStore webPageDS = new WebPageDataStore();
        [Reactive]
        public string TITLE { get; set; }
        [Reactive]
        public string URL { get; set; }
        [Reactive]
        public List<MWebPage> WebPageItems { get; set; } = new List<MWebPage>();
        [Reactive]
        public MWebPage SelectedWebPage { get; set; }
        public ReactiveCommand<Unit, Unit> Search { get; }
        public ReactiveCommand<Unit, Unit> Save { get; }
        public WebPageSelectViewModel()
        {
            Search = ReactiveCommand.CreateFromTask(async () =>
            {
                WebPageItems = await webPageDS.GetDataBySearch(TITLE, URL);
                SelectedWebPage = null;
            });
            Search.Execute().Subscribe();
            var q = this.WhenAnyValue(x => x.SelectedWebPage, (MWebPage v) => v != null);
            Save = ReactiveCommand.Create(() => { }, q);
        }
    }
}
