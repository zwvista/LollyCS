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
        public int CurrentOnlineTextbookIndex { get; set; }
        public string URL { [ObservableAsProperty] get; }

        public OnlineTextbooksWebPageViewModel(List<MOnlineTextbook> onlineTextbooks, int index)
        {
            OnlineTextbooks = onlineTextbooks;
            CurrentOnlineTextbookIndex = index;
            this.WhenAnyValue(x => x.CurrentOnlineTextbookIndex, (int v) => OnlineTextbooks[v].URL)
                .ToPropertyEx(this, x => x.URL);
        }

        public void Next(int delta) =>
            CurrentOnlineTextbookIndex = (CurrentOnlineTextbookIndex + delta + OnlineTextbooks.Count) % OnlineTextbooks.Count;
    }
}
