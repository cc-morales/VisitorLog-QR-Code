using Microsoft.EntityFrameworkCore;
using QRCoder;
using VisitorLog.ApplicationDBContextService;
using VisitorLog.Models;

namespace VisitorLog.Services.QRCodeManagementService
{
    public class QRCodeManagementService : IQRCodeManagementService
    {
        private readonly AppDbContext _context;

        public QRCodeManagementService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QRCodeModel>> GetQRCodesAsync()
        {   
            try
            {
                return await _context.QRCodes
                    .AsNoTracking()
                    .OrderBy( c => c.QRCodeAlias)
                    .ToListAsync();
            }
            catch (OperationCanceledException)
            {
                return new List<QRCodeModel>();
            }
        }

        public async Task AddQRCodeAsync(QRCodeModel qrCode)
        {
            _context.QRCodes.Add(qrCode);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQRCodeAsync(QRCodeModel qrCode)
        {
            var existing = await _context.QRCodes.FindAsync(qrCode.QRCodeId);
            if (existing != null)
            {
                existing.QRCode = qrCode.QRCode;
                existing.QRCodeAlias = qrCode.QRCodeAlias;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteQRCodeAsync(Guid qrCodeId)
        {
            var qrCode = await _context.QRCodes.FindAsync(qrCodeId);
            if (qrCode != null)
            {
                _context.QRCodes.Remove(qrCode);
                await _context.SaveChangesAsync();
            }
        }

        public byte[] GenerateQRCodeImage(string qrCodeText)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new BitmapByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}