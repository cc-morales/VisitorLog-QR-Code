using VisitorLog.Models;

namespace VisitorLog.Services.QRCodeManagementService
{
    public interface IQRCodeManagementService
    {
        Task<List<QRCodeModel>> GetQRCodesAsync();
        Task AddQRCodeAsync(QRCodeModel qrCode);
        Task UpdateQRCodeAsync(QRCodeModel qrCode);
        Task DeleteQRCodeAsync(Guid qrCodeId);
        byte[] GenerateQRCodeImage(string qrCodeText);
    }
}