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
        if (_isInitialized || _isInitializing)
            return;

        _isInitializing = true;
        
        try
        {
            Console.WriteLine("AppStateService: 开始初始化");
            
            // 这里只是标记初始化开始，实际的存储读取将在 OnAfterRenderAsync 中进行
            _isInitialized = true;
            NotifyStateChanged();
        }
        finally
        {
            _isInitializing = false;
        }
    }

    public async Task<bool> LoadFromStorageAsync()
    {
        try
        {
            Console.WriteLine("AppStateService: 从存储中读取用户ID");
            var storedUserId = await GetUserIdFromStorage();
            Console.WriteLine($"AppStateService: 从存储中读取的 UserId = '{storedUserId}'");
            
            if (!string.IsNullOrEmpty(storedUserId))
            {
                CommonApi.UserId = storedUserId;
                NotifyStateChanged();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AppStateService: 从存储加载失败 - {ex.Message}");
        }
        
        return false;
    }

    public async Task LoginAsync(string userId)
    {
        Console.WriteLine($"AppStateService: 登录用户 '{userId}'");
        CommonApi.UserId = userId;
        
        // 将存储操作加入队列，在合适的时候执行
        _pendingActions.Enqueue(async () => await SetUserIdToStorage(userId));
        
        _isInitialized = true;
        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        Console.WriteLine("AppStateService: 退出登录");
        CommonApi.UserId = "";
        
        // 将存储操作加入队列，在合适的时候执行
        _pendingActions.Enqueue(async () => await RemoveUserIdFromStorage());
        
        NotifyStateChanged();
    }

    // 执行所有挂起的存储操作
    public async Task FlushPendingActionsAsync()
    {
        while (_pendingActions.Count > 0)
        {
            var action = _pendingActions.Dequeue();
            await action();
        }
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
            Console.WriteLine($"AppStateService: 用户ID已保存到存储");
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