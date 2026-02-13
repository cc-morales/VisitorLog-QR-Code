using VisitorLog.Models;

namespace VisitorLog.Services.VisitorManagementService
{
    public interface IVisitorManagementService
    {
        Task<List<VisitorModel>> GetVisitorsAsync();
        Task AddVisitorAsync(VisitorModel visitor);
        Task UpdateVisitorAsync(VisitorModel visitor);
        Task DeleteVisitorAsync(Guid visitorId);
    }
}