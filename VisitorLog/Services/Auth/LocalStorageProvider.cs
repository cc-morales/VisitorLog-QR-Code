using Blazored.LocalStorage;

namespace VisitorLog.Services.Auth;

public class LocalStorageProvider
{
    private readonly ILocalStorageService _localStorage;
    private const string TokenKey = "authToken";
    private const string ExpirationKey = "tokenExpiration";

    public LocalStorageProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SetTokenAsync(string token)
    {
        await _localStorage.SetItemAsStringAsync(TokenKey, token);
        var expiration = DateTime.UtcNow.AddHours(1);
        await _localStorage.SetItemAsync(ExpirationKey, expiration);
    }

    public async Task<string?> GetTokenAsync()
    {
        var expiration = await _localStorage.GetItemAsync<DateTime?>(ExpirationKey);
        
        if (expiration == null || expiration < DateTime.UtcNow)
        {
            await ClearTokenAsync();
            return null;
        }

        return await _localStorage.GetItemAsStringAsync(TokenKey);
    }

    public async Task ClearTokenAsync()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
        await _localStorage.RemoveItemAsync(ExpirationKey);
    }

    public async Task<bool> IsTokenValidAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }
}