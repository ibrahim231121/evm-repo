namespace Crossbones.ALPR.Models.Items
{
    public class HotListDTO
    {
        public long RecId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public long? SourceId { get; set; }

        public string SourceName { get; set; }

        public string RulesExpression { get; set; }

        public int AlertPriority { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public byte[] LastTimeStamp { get; set; }

        public long? StationId { get; set; }

        public string Color { get; set; }

        public string Audio { get; set; }
    }
}