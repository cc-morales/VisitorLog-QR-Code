using Microsoft.EntityFrameworkCore;
using VisitorLog.ApplicationDBContextService;
using VisitorLog.Models;

namespace VisitorLog.Services.QRSetManagementService
{
    public class QRSetManagementService : IQRSetManagementService
    {
        private readonly AppDbContext _context;

        public QRSetManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<QRSetModel?> GetQRSetByVisitorIdAsync(Guid visitorId)
        {
            try
            {
                return await _context.QRSets
                    .AsNoTracking()
                    .Include(x => x.Visitor)
                    .Include(x => x.QRCode)
                    .FirstOrDefaultAsync(x => x.VisitorId == visitorId);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        public async Task AddQRSetAsync(QRSetModel qrSet)
        {
            var existing = await _context.QRSets
                .FirstOrDefaultAsync(x => x.VisitorId == qrSet.VisitorId);

            if (existing != null)
            {
                existing.QRCodeId = qrSet.QRCodeId;
                existing.QRCode = null;
                existing.Visitor = null;
            }
            else
            {
                _context.QRSets.Add(qrSet);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteQRSetAsync(Guid qrSetId)
        {
            var qrSet = await _context.QRSets.FindAsync(qrSetId);
            if (qrSet != null)
            {
                _context.QRSets.Remove(qrSet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteQRSetByVisitorIdAsync(Guid visitorId)
        {
            var qrSet = await _context.QRSets.FirstOrDefaultAsync(x => x.VisitorId == visitorId);
            if (qrSet != null)
            {
                _context.QRSets.Remove(qrSet);
                await _context.SaveChangesAsync();
            }
        }
    }
}