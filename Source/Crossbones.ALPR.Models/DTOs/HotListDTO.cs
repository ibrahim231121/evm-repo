using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListDTO
    {
        public long RecId { get; set; }

        [StringLength(50, ErrorMessage = "Export Path length should be <= 1", MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Source Id should be greater than 0")]
        public long SourceId { get; set; }

        public string? RulesExpression { get; set; }

        [Range(1, 25, ErrorMessage = "Alert Priority should be between 1 and 25")]
        public int AlertPriority { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? CreatedOn { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? LastUpdatedOn { get; set; }
        public byte[]? LastTimeStamp { get; set; }
        public long? StationId { get; set; }
        public string? Color { get; set; }
        public string? Audio { get; set; }

        //Addtional Properties
        public string? SourceName { get; set; }
    }
}