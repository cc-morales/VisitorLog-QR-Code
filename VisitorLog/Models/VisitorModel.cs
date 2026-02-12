namespace VisitorLog.Models
{
    public class VisitorModel
    {
        public Guid VisitorId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PurposeOfVisit { get; set; } = string.Empty;
        public byte[] Picture { get; set; } = Array.Empty<byte>();
    }
}
