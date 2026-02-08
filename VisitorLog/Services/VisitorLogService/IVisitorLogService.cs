using VisitorLog.Models;

namespace VisitorLog.Services.VisitorLogService
{
    public interface IVisitorLogService
    {
        Task<List<LogModel>> GetVisitorLogsAsync();
        Task AddVisitorLogAsync(LogModel visitor);
    }
}