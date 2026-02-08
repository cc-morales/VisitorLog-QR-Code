using Blazored.LocalStorage;

namespace VisitorLog.Services.LocalStrorageService
{
    public class LocalStorageProvider(ILocalStorageService localStorageService) : ILocalStorageProvider
    {
        private readonly ILocalStorageService _localStorageService = localStorageService;

        public async Task<T> GetItemAsync<T>(string key)
        {
            return await _localStorageService.GetItemAsync<T>(key);
        }

        public async Task RemoveItemAsync(string key)
        {
            await _localStorageService.RemoveItemAsync(key);
        }

        public async Task SetItemAsync<T>(string key, T data)
        {
            await _localStorageService.SetItemAsync(key, data);
        }
    }
}
