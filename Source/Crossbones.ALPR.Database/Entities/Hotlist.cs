using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

[Table("Hotlist")]
public partial class Hotlist
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("SourceID")]
    public int? SourceId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? RulesExpression { get; set; }

    public short AlertPriority { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdatedOn { get; set; }

    public byte[] LastTimeStamp { get; set; } = null!;

    [Column("StationID")]
    public int? StationId { get; set; }

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

    [ForeignKey("SourceId")]
    [InverseProperty("Hotlists")]
    public virtual HotlistDataSource? Source { get; set; }
}
