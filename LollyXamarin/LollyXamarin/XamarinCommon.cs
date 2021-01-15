using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;

namespace LollyXamarin
{
    public static class XamarinCommon
    {
        public static async Task GoogleXamarin(this string str) =>
            await Browser.OpenAsync($"https://www.google.com/search?q={HttpUtility.UrlEncode(str)}");
    }
}
