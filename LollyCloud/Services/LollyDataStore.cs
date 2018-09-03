using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class LollyDataStore<T> where T: class
    {
        protected HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(App.LollyUrl)
        };
        protected IEnumerable<T> items = new List<T>();

        protected async Task<U> GetDataByUrl<U>(string url)
        {
            var json = await client.GetStringAsync(url);
            U u = await Task.Run(() =>
            {
                try
                {
                    return JsonConvert.DeserializeObject<U>(json);
                }
                catch (JsonException ex)
                {
                    return default(U);
                }
            });
            return u;
        }

        protected async Task<bool> CreateByUrl(string url, T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> UpdateByUrl(string url, string body)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = body;
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri(url), byteContent);

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> DeleteByUrl(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }
}
