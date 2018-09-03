using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LollyCloud
{
    public class LollyViewModel : BaseViewModel
    {
        public LollyViewModel()
        {
            Title = "Browse";
        }

        protected async Task<ObservableCollection<T>> GetData<T>(Func<Task<IEnumerable<T>>> func)
        {
            var Items = new ObservableCollection<T>();

            if (!IsBusy)
            {
                IsBusy = true;

                try
                {
                    Items.Clear();
                    var items = await func();
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }

            return Items;
        }
    }
}
