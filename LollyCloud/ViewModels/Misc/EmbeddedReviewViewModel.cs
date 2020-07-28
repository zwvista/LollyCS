using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace LollyCloud
{
    public class EmbeddedReviewViewModel : ReactiveObject
    {
        public MReviewOptions Options { get; } = new MReviewOptions { IsEmbedded = true, Shuffled = false };
        IDisposable subscriptionTimer;
        public bool IsRunning => subscriptionTimer != null;

        public void Stop()
        {
            subscriptionTimer?.Dispose();
            subscriptionTimer = null;
        }

        public void Start(List<int> ids, Action<int> getOne)
        {
            if (Options.Shuffled)
                ids.Shuffle();
            int nFrom = ids.Count * (Options.GroupSelected - 1) / Options.GroupCount;
            int nTo = ids.Count * Options.GroupSelected / Options.GroupCount;
            ids = ids.Skip(nFrom).Take(nTo - nFrom).ToList();
            subscriptionTimer = Observable.Interval(TimeSpan.FromSeconds(Options.Interval)).ObserveOn(RxApp.MainThreadScheduler).Subscribe(i =>
            {
                if (i < ids.Count)
                    getOne(ids[(int)i]);
                else
                    Stop();
            });
        }
    }
}
