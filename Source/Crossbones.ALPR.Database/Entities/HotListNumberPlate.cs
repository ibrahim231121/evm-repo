using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

public partial class HotListNumberPlate
{
    [Key]
    [Column("SysSerial")]
    public long SysSerial { get; set; }

    [ForeignKey("HotList")]
    [Column("HotListID")]
    public long HotListId { get; set; }

    [ForeignKey("NumberPlate")]
    [Column("NumberPlatesID")]
    public long NumberPlatesId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedOn { get; set; }

    public byte[] LastTimeStamp { get; set; } = null!;

    public virtual Hotlist HotList { get; set; }

    public virtual NumberPlate NumberPlate { get; set; }
}