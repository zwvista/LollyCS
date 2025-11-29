using LollyCommon;

namespace LollyBlazor.Services;

// Services/LocalUserService.cs
using Microsoft.JSInterop;

public class LocalUserService
{
    private readonly IJSRuntime _jsRuntime;
    public readonly LoginViewModel vm = new();
    
    public LocalUserService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<bool> IsUserLoggedInAsync()
    {
        var userId = await GetUserIdFromLocalStorage();
        return !string.IsNullOrEmpty(userId);
    }
    
    public async Task<string?> GetUserIdAsync()
    {
        return await GetUserIdFromLocalStorage();
    }
    
    public async Task<bool> ValidateUserAsync()
    {
        CommonApi.UserId = await vm.Login();
        if (!string.IsNullOrEmpty(CommonApi.UserId))
        {
            await SetUserIdToLocalStorage(CommonApi.UserId);
            return true;
        }
        
        return false;
    }
    
    public async Task LogoutAsync()
    {
        await RemoveUserIdFromLocalStorage();
    }
    
    private async Task<string?> GetUserIdFromLocalStorage()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userid");
        }
        catch
        {
            // 如果 JavaScript 互操作失败，返回 null
            return null;
        }
    }
    
    private async Task SetUserIdToLocalStorage(string userId)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userid", userId);
        }
        catch
        {
            // 处理 JavaScript 互操作失败的情况
        }
    }
    
    private async Task RemoveUserIdFromLocalStorage()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userid");
        }
        catch
        {
            // 处理 JavaScript 互操作失败的情况
        }
    }
}