namespace LollyBlazor.Services;

// Source - https://stackoverflow.com/q
// Posted by George Korolev, modified by community. See post 'Timeline' for change history
// Retrieved 2025-12-02, License - CC BY-SA 4.0
// https://stackoverflow.com/questions/75472912/how-do-i-read-the-clipboard-with-blazor

using Microsoft.JSInterop;

public sealed class ClipboardService(IJSRuntime jsRuntime)
{
    public ValueTask<string> ReadTextAsync()
    {
        return jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    }

    public ValueTask WriteTextAsync(string text)
    {
        return jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}
