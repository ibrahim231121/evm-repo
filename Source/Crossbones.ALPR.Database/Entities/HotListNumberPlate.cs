using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Corssbones.ALPR.Database.Entities;

public partial class HotListNumberPlate
{
    public long RecId { get; set; }

    public long HotListId { get; set; }

    [Column("NumberPlatesId")]
    public long NumberPlateId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }

    public byte[]? LastTimeStamp { get; set; }

    public virtual Hotlist HotList { get; set; } = null!;

    public virtual NumberPlate NumberPlate { get; set; } = null!;
}