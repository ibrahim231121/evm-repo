using Entities = Corssbones.ALPR.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListDataSourceDTO
    {
        public long RecId { get; set; }

        [StringLength(50, ErrorMessage = "Name length should be <= 1", MinimumLength = 1)]
        [Unicode(false)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "SourceName length should be <= 1", MinimumLength = 1)]
        [Unicode(false)]
        public string? SourceName { get; set; }
        public long? AgencyId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Source Type Id should be greater than 0")]
        public long SourceTypeId { get; set; }
        public string? SchedulePeriod { get; set; }

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

        [Range(0, 1, ErrorMessage = "Status should be either 0 or 1")]
        public short Status { get; set; }

        public bool SkipFirstLine { get; set; }

        //[StringLength(8000, ErrorMessage = "Status Desc length should be <= 1", MinimumLength = 1)]
        [Unicode(false)]
        public string? StatusDesc { get; set; }
        public string? SourceTypeName { get; set; }
        public virtual SourceTypeDTO? SourceType { get; set; }

        public virtual ICollection<Entities.Hotlist>? Hotlists { get; set; }

    }
}