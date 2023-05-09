using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ALPR.Database.Entities;

[Table("HotlistDataSource")]
public partial class HotlistDataSource
{
    [Key]
    [Column("sysSerial")]
    public int SysSerial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SourceName { get; set; }

    [Column("AgencyID")]
    public int? AgencyId { get; set; }

    [Column("SourceTypeID")]
    public int SourceTypeId { get; set; }

    [Column(TypeName = "decimal(10, 0)")]
    public decimal? SchedulePeriod { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    public bool? IsExpire { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? SchemaDefinition { get; set; }

    [Column("LastUpdateExternalHotListID")]
    public int? LastUpdateExternalHotListId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ConnectionType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Userid { get; set; }

    [Column("locationPath")]
    [StringLength(100)]
    [Unicode(false)]
    public string? LocationPath { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("port")]
    public int? Port { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastRun { get; set; }

    public short Status { get; set; }

    public bool SkipFirstLine { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? StatusDesc { get; set; }

    [InverseProperty("Source")]
    public virtual ICollection<Hotlist> Hotlists { get; } = new List<Hotlist>();

    [ForeignKey("SourceTypeId")]
    [InverseProperty("HotlistDataSources")]
    public virtual SourceType SourceType { get; set; } = null!;
}
