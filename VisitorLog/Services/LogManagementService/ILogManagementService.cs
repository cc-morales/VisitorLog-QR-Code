using VisitorLog.Models;

namespace VisitorLog.Services.LogManagementService
{
    public interface ILogManagementService
    {
        Task<List<LogModel>> GetLogsAsync();
        Task<LogModel?> GetLastLogByVisitorIdAsync(Guid visitorId);
        Task<QRSetModel?> GetQRSetByQRCodeAsync(string qrCodeText);
        Task AddLogAsync(LogModel log);
    }
}