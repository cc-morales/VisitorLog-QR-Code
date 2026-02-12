namespace VisitorLog.Models
{
    public class QRSetModel
    {
        public Guid QRSetId { get; set; } = Guid.NewGuid();
        public VisitorModel? Visitor { get; set; }
        public QRCodeModel? QRCode { get; set; }
    }
}
