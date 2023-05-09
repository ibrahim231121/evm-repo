using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ALPR.Database.Entities;

[Table("SourceType")]
public partial class SourceType
{
    [Key]
    [Column("sysSerial")]
    public int SysSerial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SourceTypeName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("SourceType")]
    public virtual ICollection<HotlistDataSource> HotlistDataSources { get; } = new List<HotlistDataSource>();
}
