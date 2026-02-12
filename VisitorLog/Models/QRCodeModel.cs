using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorLog.Models
{
    [Table("qrcodes")]
    public class QRCodeModel
    {
        [Key]
        public Guid QRCodeId { get; set; } = Guid.NewGuid();

        public string QRCode { get; set; } = string.Empty;
        public string QRCodeAlias { get; set; } = string.Empty;
    }
}
