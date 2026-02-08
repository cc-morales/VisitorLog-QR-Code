namespace VisitorLog.Services.LocalStrorageService
{
    public interface ILocalStorageProvider
    {
        public Task<T> GetItemAsync<T>(string key);
        public Task SetItemAsync<T>(string key, T data);
        public Task RemoveItemAsync(string key);
    }
}
