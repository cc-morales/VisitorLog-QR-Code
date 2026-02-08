namespace VisitorLog.Models
{
    public class VisitorModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string QRCode { get; set; } = string.Empty;
        public string VisitorAlias { get; set; } = string.Empty;
    }
}
