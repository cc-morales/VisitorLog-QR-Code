using VisitorLog.Models;

namespace VisitorLog.Services.QRSetManagementService
{
    public interface IQRSetManagementService
    {
        Task<QRSetModel?> GetQRSetByVisitorIdAsync(Guid visitorId);
        Task AddQRSetAsync(QRSetModel qrSet);
        Task DeleteQRSetAsync(Guid qrSetId);
        Task DeleteQRSetByVisitorIdAsync(Guid visitorId);
    }
}