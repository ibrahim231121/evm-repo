﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

[Table("ALPRExportDetail")]
public partial class AlprExportDetail
{
    [Key]
    public long RecId { get; set; }

    public long CapturedPlateId { get; set; }

    public long TicketNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExportedOn { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string ExportPath { get; set; } = null!;

    [StringLength(1024)]
    [Unicode(false)]
    public string? UriLocation { get; set; }
}
