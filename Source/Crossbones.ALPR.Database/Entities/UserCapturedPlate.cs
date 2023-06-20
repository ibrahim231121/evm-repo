using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

public partial class UserCapturedPlate
{
    [Key]
    [Column("RecId")]
    public long RecId { get; set; }

    [Column("UserID")]
    public long UserId { get; set; }

    [Column("CapturedID")]
    public long CapturedId { get; set; }
}
