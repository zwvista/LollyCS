using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LollyCommon
{
    public class OnlineTextbooksWebPageViewModel : ReactiveObject
    {
        public List<MOnlineTextbook> OnlineTextbooks { get; }
        [Reactive]
        public int SelectedOnlineTextbookIndex { get; set; }
        public string URL { [ObservableAsProperty] get; }

        public OnlineTextbooksWebPageViewModel(List<MOnlineTextbook> onlineTextbooks, int index)
        {
            OnlineTextbooks = onlineTextbooks;
            SelectedOnlineTextbookIndex = index;
            this.WhenAnyValue(x => x.SelectedOnlineTextbookIndex, (int v) => OnlineTextbooks[v].URL)
                .ToPropertyEx(this, x => x.URL);
        }

        public void Next(int delta) =>
            SelectedOnlineTextbookIndex = (SelectedOnlineTextbookIndex + delta + OnlineTextbooks.Count) % OnlineTextbooks.Count;
    }
}
