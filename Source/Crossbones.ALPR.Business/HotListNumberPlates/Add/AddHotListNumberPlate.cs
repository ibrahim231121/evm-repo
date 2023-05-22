using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Add
{
    public class AddHotListNumberPlate : HotListNumberPlatesMessage
    {
        public long HotListID { get; set; }
        public long NumberPlatesId { get; set; }
        public DateTime CreatedOn { get; set; }
        public AddHotListNumberPlate(SysSerial id) : base(id)
        {
        }        
    }
}