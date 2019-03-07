using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LollyShared
{
    public class ItemBaseViewModel : BaseViewModel
    {
        public IDataStore<Item> DataStore => ServiceLocator.Instance.Get<IDataStore<Item>>() ?? new MockDataStore();
    }
}
