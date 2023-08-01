using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryDTO
    {
        public long CapturePlateId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value for StationId should be greater than 0")]
        public int StationId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value for StationId should be greater than 0")]
        public int ClientId { get; set; }

        [MinLength(1, ErrorMessage = "UnitId can not be empty")]
        public string UnitId { get; set; } = null!;

        public long UserId { get; set; }

        [MinLength(1, ErrorMessage = "LoginId can not be empty")]
        public string LoginId { get; set; } = null!;

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime CaptureDate { get; set; }

        public bool HasAlert { get; set; }

        public bool HasTicket { get; set; }
    }
}
