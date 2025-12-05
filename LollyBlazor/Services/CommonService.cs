using System.Web;

namespace LollyBlazor.Services;

using Microsoft.JSInterop;

public sealed class CommonService(IJSRuntime jsRuntime)
{
    public ValueTask<string> ReadTextAsync()
    {
        return jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    }

    public ValueTask WriteTextAsync(string text)
    {
        return jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
    public ValueTask<object> OpenPageAsync(string url)
    {
        return jsRuntime.InvokeAsync<object>("open", url, "_blank");
    }
    public ValueTask<object> GoogleStringAsync(string str)
    {
        return OpenPageAsync("https://www.google.com/search?q=" + HttpUtility.UrlEncode(str));
    }
}
