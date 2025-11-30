using LollyCommon;

namespace LollyBlazor.Services;

// Services/LocalUserService.cs

public class LocalUserService(AppStateService appState)
{
    public readonly LoginViewModel vm = new();

    public bool IsUserLoggedIn => appState.IsLoggedIn;
    public string? CurrentUserId => CommonApi.UserId;
    
    public async Task<bool> ValidateUserAsync()
    {
        CommonApi.UserId = await vm.Login();
        if (string.IsNullOrEmpty(CommonApi.UserId)) return false;
        await appState.LoginAsync(CommonApi.UserId);
        return true;
    }
    
    public async Task LogoutAsync()
    {
        await appState.LogoutAsync();
    }
}