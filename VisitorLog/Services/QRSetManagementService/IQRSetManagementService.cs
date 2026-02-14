using VisitorLog.Models;

namespace VisitorLog.Services.QRSetManagementService
{
    public interface IQRSetManagementService
    {
        Task<QRSetModel?> GetQRSetByVisitorIdAsync(Guid visitorId);
        Task<QRSetModel?> GetQRSetByQRCodeIdAsync(Guid qrCodeId);
        Task<bool> AddQRSetAsync(QRSetModel qrSet);
        Task DeleteQRSetAsync(Guid qrSetId);
        Task DeleteQRSetByVisitorIdAsync(Guid visitorId);
    }
}