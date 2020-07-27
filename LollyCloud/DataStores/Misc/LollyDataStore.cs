using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace LollyCloud
{
    public class LollyDataStore<T> where T: class
    {
        protected HttpClient clientAPI = new HttpClient
        {
            BaseAddress = new Uri(CommonApi.LollyUrl)
        };
        protected HttpClient clientSP = new HttpClient
        {
            BaseAddress = new Uri(CommonApi.LollyUrlSP)
        };

        protected async Task<U> GetDataByUrl<U>(string url) where U : class
        {
            if (!CrossConnectivity.Current.IsConnected) return null;

            var json = await clientAPI.GetStringAsync(url);
            U u = await Task.Run(() =>
            {
                try
                {
                    return JsonConvert.DeserializeObject<U>(json);
                }
                catch (JsonException ex)
                {
                    return null;
                }
            });
            return u;
        }

        protected async Task<int> CreateByUrl(string url, T item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return 0;

            // When posting(creating) a new record, its id must be null.
            // Otherwise the generated id will not be returned.
            var serializedItem = JsonConvert.SerializeObject(item).Replace("\"ID\":0,", "").Replace(",\"ID\":0", "");

            var response = await clientAPI.PostAsync(url, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return !response.IsSuccessStatusCode || !int.TryParse(await response.Content.ReadAsStringAsync(), out var v) ? 0 : v;
        }

        protected async Task<bool> UpdateByUrl(string url, string body)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = body;
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await clientAPI.PutAsync(url, byteContent);

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> DeleteByUrl(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return false;

            var response = await clientAPI.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }

        protected async Task<MSPResult> CallSPByUrl(string url, T item)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return null;

            var dic = typeof(T).GetProperties().ToDictionary(o => "P_" + o.Name, o => o.GetValue(item)?.ToString());
            var response = await clientSP.PostAsync(url, new FormUrlEncodedContent(dic));

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var u = await Task.Run(() =>
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<List<MSPResult>>>(json)[0][0];
                }
                catch (JsonException ex)
                {
                    return null;
                }
            });
            return u;
        }
    }
}
