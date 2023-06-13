using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Change
{
    public class ChangeHotListNumberPlate : SysSerialItemMessage
    {
        public long HotListID { get; set; }
        public long NumberPlatesId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte[] LastTimeStamp { get; set; } = null!;
        public ChangeHotListNumberPlate(SysSerial id) : base(id)
        {
        }
    }
}