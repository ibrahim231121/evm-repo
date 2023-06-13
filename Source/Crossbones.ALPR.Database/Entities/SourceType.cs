﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[Table("SourceType")]
public partial class SourceType
{
    [Key]
    [Column("sysSerial")]
    public long SysSerial { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string SourceTypeName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("SourceType")]
    public virtual ICollection<HotlistDataSource> HotlistDataSources { get; set; } = new List<HotlistDataSource>();
}
