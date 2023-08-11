using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Models.DTOs
{
    public class NumberPlateHistoryDTO
    {
        public long Id { get; set; }

        public string NumberPlate { get; set; }

        public string NumberPlateThumbnail { get; set; }

        public string HotlistName { get; set; }

        public DateTime CapturedAt { get; set; }

        public string CapturedAtStr { get; set; }

        public int Confidence { get; set; }

        public string State { get; set; }

        public long TicketNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance { get; set; }

        public string? VehicleYear { get; set; }

        public string? VehicleMake { get; set; }

        public string? VehicleModel { get; set; }

        public long UserId { get; set; }

        public string Unit { get; set; }

        public string Notes { get; set; }
    }
}
