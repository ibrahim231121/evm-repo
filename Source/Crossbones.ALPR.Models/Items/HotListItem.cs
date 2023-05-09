namespace Crossbones.ALPR.Models.Items
{
    public class HotListItem
    {
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