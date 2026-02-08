namespace VisitorLog.Models
{
    public class LogModel
    {
        public Guid GUid { get; set; } = Guid.NewGuid();
        public required string VisitorsName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public VisitorModel Visitor { get; set; } = new();
    }
}
