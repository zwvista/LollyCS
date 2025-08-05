using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public partial class OnlineTextbooksWebPageViewModel : ReactiveObject
    {
        public List<MOnlineTextbook> OnlineTextbooks { get; }
        [Reactive]
        public partial int SelectedOnlineTextbookIndex { get; set; }
        [ObservableAsProperty]
        public partial string URL { get; }

        public OnlineTextbooksWebPageViewModel(List<MOnlineTextbook> onlineTextbooks, int index)
        {
            OnlineTextbooks = onlineTextbooks;
            SelectedOnlineTextbookIndex = index;
            this.WhenAnyValue(x => x.SelectedOnlineTextbookIndex, (int v) => OnlineTextbooks[v].URL)
                .ToProperty(this, x => x.URL);
        }

        public void Next(int delta) =>
            SelectedOnlineTextbookIndex = (SelectedOnlineTextbookIndex + delta + OnlineTextbooks.Count) % OnlineTextbooks.Count;
    }
}
