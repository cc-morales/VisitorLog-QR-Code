using VisitorLog.Models;
using VisitorLog.Services.LocalStrorageService;

namespace VisitorLog.Services.VisitorLogService
{
    public class VisitorLogService(ILocalStorageProvider localStorage) : IVisitorLogService
    {
        private const string StorageKey = "visitor_list";

        private readonly ILocalStorageProvider _localStorage = localStorage;

        public async Task<List<LogModel>> GetVisitorLogsAsync()
        {
            return await _localStorage.GetItemAsync<List<LogModel>>(StorageKey)?? [];
        }

        public async Task AddVisitorLogAsync(LogModel visitor)
        {
            var list = await _localStorage.GetItemAsync<List<LogModel>>(StorageKey)?? [];

            list.Add(visitor);

            await _localStorage.SetItemAsync(StorageKey, list);
        }
    }
}