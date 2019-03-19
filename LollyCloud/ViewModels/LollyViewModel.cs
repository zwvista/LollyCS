using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LollyShared
{
    public class LollyViewModel : BaseViewModel
    {
        public LollyViewModel()
        {
            Title = "Browse";
        }

        protected async Task<List<T>> GetData<T>(Func<Task<List<T>>> func)
        {
            var Items = new List<T>();

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
