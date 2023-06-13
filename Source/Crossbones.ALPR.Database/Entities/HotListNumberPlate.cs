namespace Corssbones.ALPR.Database.Entities;

public partial class HotListNumberPlate
{
    public long SysSerial { get; set; }

    public long HotListId { get; set; }

    public long NumberPlatesId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? LastUpdatedOn { get; set; }

    public byte[]? LastTimeStamp { get; set; }

    public virtual Hotlist HotList { get; set; } = null!;

    public virtual NumberPlate NumberPlate { get; set; } = null!;

}