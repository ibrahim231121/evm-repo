using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[Table("Hotlist")]
public partial class Hotlist
{
    [Key]
    [Column("RecId")]
    public long RecId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("SourceID")]
    public long SourceId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? RulesExpression { get; set; }

    public short AlertPriority { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedOn { get; set; }

    public byte[] LastTimeStamp { get; set; } = null!;

    [Column("StationID")]
    public long? StationId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Color { get; set; }

    public short? StorageType { get; set; }

    [Column("URILocation")]
    [StringLength(1024)]
    [Unicode(false)]
    public string? Urilocation { get; set; }

    public short? AudioType { get; set; }

    public int? AudioSize { get; set; }

    [InverseProperty("Hotlists")]
    public virtual HotlistDataSource Source { get; set; }

    public virtual ICollection<HotListNumberPlate> HotListNumberPlates { get; set; } = new List<HotListNumberPlate>();
}
