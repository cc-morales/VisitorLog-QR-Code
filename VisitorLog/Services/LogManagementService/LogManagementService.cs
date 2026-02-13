using Microsoft.EntityFrameworkCore;
using VisitorLog.ApplicationDBContextService;
using VisitorLog.Models;

namespace VisitorLog.Services.LogManagementService
{
    public class LogManagementService : ILogManagementService
    {
        private readonly AppDbContext _context;

        public LogManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogModel>> GetLogsAsync()
        {
            try
            {
                return await _context.Logs
                    .AsNoTracking()
                    .Include(x => x.Visitor)
                    .Include(x => x.QRCode)
                    .OrderByDescending(x => x.Timestamp)
                    .ToListAsync();
            }
            catch (OperationCanceledException)
            {
                return new List<LogModel>();
            }
        }

        public async Task<LogModel?> GetLastLogByVisitorIdAsync(Guid visitorId)
        {
            try
            {
                return await _context.Logs
                    .AsNoTracking()
                    .Include(x => x.Visitor)
                    .Include(x => x.QRCode)
                    .Where(x => x.VisitorId == visitorId)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefaultAsync();
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        public async Task<QRSetModel?> GetQRSetByQRCodeAsync(string qrCodeText)
        {
            try
            {
                return await _context.QRSets
                    .AsNoTracking()
                    .Include(x => x.Visitor)
                    .Include(x => x.QRCode)
                    .FirstOrDefaultAsync(x => x.QRCode != null && x.QRCode.QRCode == qrCodeText);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        public async Task AddLogAsync(LogModel log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}