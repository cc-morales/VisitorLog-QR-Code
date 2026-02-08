using VisitorLog.Models;

namespace VisitorLog.Services.QRCodeServices
{
    public interface IQRCodeService
    {
        Task<Dictionary<string, VisitorModel>> GetVisitorsAsync();
        Task UpdateVisitorsAsync();
        Task AddVisitorAsync(VisitorModel visitor);
        Task UpdateVisitorAsync(VisitorModel visitor, string oldQR);
        Task DeleteVisitorAsync(VisitorModel visitor);

        Task<VisitorModel?> GetVisitor(string qrcode);

    }
}
