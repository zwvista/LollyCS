using LollyCommon;
using Microsoft.JSInterop;

namespace LollyBlazor.Services;

public class AppStateService(IJSRuntime jsRuntime)
{
    private bool _isInitialized;
    private bool _isInitializing;
    private readonly Queue<Func<Task>> _pendingActions = new();
    
    public event Action? OnChange;

    public string? CurrentUserId => CommonApi.UserId;
    public bool IsLoggedIn => !string.IsNullOrEmpty(CommonApi.UserId);
    public bool IsInitialized => _isInitialized;

    public async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            Console.WriteLine("AppStateService: 开始初始化");
            var storedUserId = await GetUserIdFromStorage();
            Console.WriteLine($"AppStateService: 从存储中读取的 UserId = '{storedUserId}'");
            
            CommonApi.UserId = storedUserId ?? "";
            _isInitialized = true;
            Console.WriteLine($"AppStateService: 初始化完成, CommonApi.UserId = '{CommonApi.UserId}'");
            NotifyStateChanged();
        }
    }

    public async Task LoginAsync(string userId)
    {
        Console.WriteLine($"AppStateService: 登录用户 '{userId}'");
        CommonApi.UserId = userId;
        await SetUserIdToStorage(userId);
        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        Console.WriteLine("AppStateService: 退出登录");
        CommonApi.UserId = "";
        await RemoveUserIdFromStorage();
        NotifyStateChanged();
    }

    private async Task<string?> GetUserIdFromStorage()
    {
        try
        {
            return await jsRuntime.InvokeAsync<string>("localStorage.getItem", "userid");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AppStateService: 读取存储失败 - {ex.Message}");
            return null;
        }
    }

    private async Task SetUserIdToStorage(string userId)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "userid", userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AppStateService: 保存存储失败 - {ex.Message}");
        }
    }

    private async Task RemoveUserIdFromStorage()
    {
        try
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userid");
            Console.WriteLine($"AppStateService: 用户ID已从存储中移除");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AppStateService: 移除存储失败 - {ex.Message}");
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}