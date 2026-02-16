using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorLog.Models;

[Table("qrsets")]
public class QRSetModel
{
    [Key]
    public Guid QRSetId { get; set; } = Guid.NewGuid();

    // Foreign key to Visitor
    [Required]
    public Guid VisitorId { get; set; }

    [ForeignKey(nameof(VisitorId))]
    public VisitorModel? Visitor { get; set; }

    // Foreign key to QRCode
    [Required]
    public Guid QRCodeId { get; set; }

    [ForeignKey(nameof(QRCodeId))]
    public QRCodeModel? QRCode { get; set; }

    // Account FullName who set the QR code
    [Required]
    public string AccountFullName { get; set; } = string.Empty;
}
