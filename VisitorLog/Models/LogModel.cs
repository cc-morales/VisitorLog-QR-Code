using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorLog.Models
{
    [Table("logs")]
    public class LogModel
    {
        [Key]
        public Guid LogId { get; set; } = Guid.NewGuid();

        public Guid? VisitorId { get; set; }

        [ForeignKey(nameof(VisitorId))]
        public VisitorModel? Visitor { get; set; }

        public Guid? QRCodeId { get; set; }

        [ForeignKey(nameof(QRCodeId))]
        public QRCodeModel? QRCode { get; set; }

        public LogEntryType EntryType { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
