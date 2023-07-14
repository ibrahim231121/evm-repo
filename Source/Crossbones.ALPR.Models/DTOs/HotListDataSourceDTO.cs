using Corssbones.ALPR.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListDataSourceDTO
    {
        public long RecId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string SourceName { get; set; }
        public long? AgencyId { get; set; }
        public long SourceTypeId { get; set; }
        public decimal? SchedulePeriod { get; set; }
        public bool? IsExpire { get; set; }

        [StringLength(4000)]
        [Unicode(false)]
        public string? SchemaDefinition { get; set; }
        public long? LastUpdateExternalHotListId { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ConnectionType { get; set; }

        [JsonProperty(PropertyName = "userId")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Userid { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string? LocationPath { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? Password { get; set; }
        public int? Port { get; set; }
        public DateTime? LastRun { get; set; }
        public short Status { get; set; }
        public bool SkipFirstLine { get; set; }

        [StringLength(8000)]
        [Unicode(false)]
        public string? StatusDesc { get; set; }
        public string? SourceTypeName { get; set; }
        public virtual SourceTypeDTO? SourceType { get; set; }

        public virtual ICollection<Hotlist> Hotlists { get; set; }

    }
}
