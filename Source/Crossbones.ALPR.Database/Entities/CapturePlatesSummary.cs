using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[PrimaryKey("CapturePlateId", "UserId")]
[Table("CapturePlatesSummary")]
public partial class CapturePlatesSummary
{
    [Key]
    public long CapturePlateId { get; set; }

    public int StationId { get; set; }

    public int ClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UnitId { get; set; } = null!;

    [Key]
    public long UserId { get; set; }

    [StringLength(110)]
    [Unicode(false)]
    public string LoginId { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime CaptureDate { get; set; }

    public bool HasAlert { get; set; }

    public bool HasTicket { get; set; }
}
