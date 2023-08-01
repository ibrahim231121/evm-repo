using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturedPlateDTO
    {
        public long CapturedPlateId { get; set; }

        public long NumberPlateId { get; set; }

        //[Required(ErrorMessage = "Captured plate number is required")]
        [MinLength(6, ErrorMessage = "Captured plate number should be more than 5 characters ")]
        public string NumberPlate { get; set; }

        public string HotlistName { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime CapturedAt { get; set; } = DateTime.MaxValue;

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime LastUpdated { get; set; }

        public DateTime CapturedAtUtc
        {
            get
            {
                return this.CapturedAt.ToUniversalTime();
            }
        }

        public string UnitId { get; set; }

        public long User { get; set; }

        public string Description { get; set; }

        [Range(typeof(int), "1","100",ErrorMessage = "Value for Confidence must be between {1} and {2}")]
        public int Confidence { get; set; }

        public short State { get; set; }

        public string? StateName { get; set; }

        public string Notes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value for TicketNumber should be greater than 0")]
        public long TicketNumber { get; set; }

        [Range(-90, 90, ErrorMessage = "Value for Latitude should be between -90 and 90")]
        public double Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Value for Longitude should be between -180 and 180")]
        public double Longitude { get; set; }

        public double Distance { get; set; }

        public string LifeSpan { get; set; }

        public string LoginId { get; set; }
    }
}
