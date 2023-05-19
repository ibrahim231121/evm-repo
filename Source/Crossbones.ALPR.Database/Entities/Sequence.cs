using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Corssbones.ALPR.Database.Entities;

[PrimaryKey("StartSequence", "EndSequence", "ResourceKey")]
public partial class Sequence
{
    [Key]
    public long StartSequence { get; set; }

    [Key]
    public long EndSequence { get; set; }

    [Key]
    [StringLength(200)]
    public string ResourceKey { get; set; } = null!;
}
