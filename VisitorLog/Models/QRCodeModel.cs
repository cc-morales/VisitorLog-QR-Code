namespace VisitorLog.Models
{
    public class QRCodeModel
    {
        public Guid QRCodeId { get; set; } = Guid.NewGuid();
        public string QRCode { get; set; } = string.Empty;
        public string QRCodeAlias { get; set; } = string.Empty;
    }
}
