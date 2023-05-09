using Corssbones.ALPR.Business.HotList;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItem : HotListItemMessage
    {
        public AddHotListItem(SysSerial id) : base(id)
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public int SourceId { get; set; }

        public string RulesExpression { get; set; }

        public short AlertPriority { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public TimeSpan LastTimeStamp { get; set; }

        public int StationId { get; set; }
    }
}
