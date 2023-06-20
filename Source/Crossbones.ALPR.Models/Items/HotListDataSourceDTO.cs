namespace Crossbones.ALPR.Models.Items
{
    public class HotListDataSourceDTO
    {
        public long RecId { get; set; }
        public string? Name { get; set; }
        public string? SourceName { get; set; }
        //public virtual SourceTypeItem SourceType { get; set; }
        public string SourceTypeName { get; set; }
        public decimal? SchedulePeriod { get; set; }
        public string? ConnectionType { get; set; }
        public DateTime? LastRun { get; set; }
        public short Status { get; set; }
        public string? StatusDesc { get; set; }
        public long SourceTypeId { get; set; }
        public string? SchemaDefinition { get; set; }

    }
}
