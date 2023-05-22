using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

[Table("CapturePlatesSummaryStatus")]
public partial class CapturePlatesSummaryStatus
{
    [Key]
    public long SyncId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastExecutionDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastExecutionEndDate { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [StringLength(1024)]
    [Unicode(false)]
    public string? StatusDesc { get; set; }
}
