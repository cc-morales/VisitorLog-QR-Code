namespace VisitorLog.Models
{
    public class LogModel
    {
        public Guid LogId { get; set; } = Guid.NewGuid();
        public VisitorModel? Visitor { get; set; }
        public QRCodeModel? QRCode { get; set; };
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
