using Corssbones.ALPR.Database.Entities;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListDataSourceDTO
    {
        public long RecId { get; set; }
        public string Name { get; set; }
        public string SourceName { get; set; }
        public long? AgencyId { get; set; }
        public long SourceTypeId { get; set; }
        public decimal? SchedulePeriod { get; set; }
        public bool? IsExpire { get; set; }
        public string? SchemaDefinition { get; set; }
        public long? LastUpdateExternalHotListId { get; set; }
        public string? ConnectionType { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string? Userid { get; set; }
        public string? LocationPath { get; set; }
        public string? Password { get; set; }
        public int? Port { get; set; }
        public DateTime? LastRun { get; set; }
        public short Status { get; set; }
        public bool SkipFirstLine { get; set; }
        public string? StatusDesc { get; set; }
        public string? SourceTypeName { get; set; }
        public virtual SourceTypeDTO? SourceType { get; set; }

        public virtual ICollection<Hotlist> Hotlists { get; set; }

    }
}
