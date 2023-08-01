using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class ExportDetailDTO
    {
        public long RecId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Captured Plate Id should be greater than 0")]
        public long CapturedPlateId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Ticket Number should be greater than 0")]
        public long TicketNumber { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime ExportedOn { get; set; }

        [StringLength(1000, ErrorMessage = "Export Path length should be <= 1",MinimumLength = 1)]
        public string ExportPath { get; set; } = null!;

        //[Required(ErrorMessage = "UriLocation can not be null")]
        //[StringLength(1024, ErrorMessage = "Uri Location should not be greather than 1024 characters")]
        //[MinLength(10, ErrorMessage = "UriLocation should not be lower than 10 characters")]
        public string? UriLocation { get; set; }
    }
}