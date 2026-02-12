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

        // Nullable FKs so logs can remain if related entities are removed (matches SetNull behaviour)
        public Guid? VisitorId { get; set; }

        [ForeignKey(nameof(VisitorId))]
        public VisitorModel? Visitor { get; set; }

        public Guid? QRCodeId { get; set; }

        [ForeignKey(nameof(QRCodeId))]
        public QRCodeModel? QRCode { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
