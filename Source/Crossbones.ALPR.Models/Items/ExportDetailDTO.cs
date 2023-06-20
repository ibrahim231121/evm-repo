using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.Items
{
    public class ExportDetailDTO
    {
        public long RecId { get; set; }

        [Required(ErrorMessage = "CapturedPlateId can not null or <= 0")]
        public long CapturedPlateId { get; set; }
        [Required(ErrorMessage = "TicketNumber can not be null or <= 0")]
        public long TicketNumber { get; set; }
        [Required(ErrorMessage = "TicketNumber can not be null")]
        public DateTime ExportedOn { get; set; }

        [Required(ErrorMessage = "TicketNumber can not be null")]
        [StringLength(1000)]
        public string ExportPath { get; set; } = null!;

        [Required(ErrorMessage = "TicketNumber can not be null")]
        [StringLength(1024, ErrorMessage = "UriLocation should not be greather than 1024 characters")]
        [MinLength(10, ErrorMessage = "UriLocation should not be lower than 10 characters")]
        public string? UriLocation { get; set; }
    }
}