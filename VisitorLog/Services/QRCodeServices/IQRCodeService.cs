using VisitorLog.Models;

namespace VisitorLog.Services.QRCodeServices
{
    public interface IQRCodeService
    {
        public Task<Dictionary<string, VisitorModel>> GetVisitorsAsync();
        public Task UpdateVisitorsAsync();
        public Task AddVisitorAsync(VisitorModel visitor);
        public Task UpdateVisitorAsync(VisitorModel visitor);
        public Task DeleteVisitorAsync(VisitorModel visitor);

    }
}
