using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItem : SysSerialItemMessage
    {
        public AddHotListItem(SysSerial id) : base(id)
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public long? SourceId { get; set; }

        public string RulesExpression { get; set; }

        public short AlertPriority { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public byte[] LastTimeStamp { get; set; }

        public long? StationId { get; set; }
    }
}
