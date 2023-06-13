using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
