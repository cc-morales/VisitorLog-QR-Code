using Microsoft.EntityFrameworkCore;
using VisitorLog.ApplicationDBContextService;
using VisitorLog.Models;

namespace VisitorLog.Services.VisitorManagementService
{
    public class VisitorManagementService : IVisitorManagementService
    {
        private readonly AppDbContext _context;

        public VisitorManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VisitorModel>> GetVisitorsAsync()
        {
            try
            {
                return await _context.Visitors
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (OperationCanceledException)
            {
                return new List<VisitorModel>();
            }
        }

        public async Task AddVisitorAsync(VisitorModel visitor)
        {
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVisitorAsync(VisitorModel visitor)
        {
            var existing = await _context.Visitors.FindAsync(visitor.VisitorId);
            if (existing != null)
            {
                existing.Name = visitor.Name;
                existing.ContactNumber = visitor.ContactNumber;
                existing.Address = visitor.Address;
                existing.PurposeOfVisit = visitor.PurposeOfVisit;
                existing.Picture = visitor.Picture;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteVisitorAsync(Guid visitorId)
        {
            var visitor = await _context.Visitors.FindAsync(visitorId);
            if (visitor != null)
            {       
                _context.Visitors.Remove(visitor);
                await _context.SaveChangesAsync();
            }
        }
    }
}