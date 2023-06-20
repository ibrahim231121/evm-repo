namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturedPlateDTO
    {
        public long CapturedPlateId { get; set; }

        public string NumberPlate { get; set; }

        public string HotlistName { get; set; }

        public DateTime CapturedAt { get; set; } = DateTime.MaxValue;

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

        public int Confidence { get; set; }

        public short State { get; set; }

        public string Notes { get; set; }

        public long TicketNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance { get; set; }

        public string LifeSpan { get; set; }

        public string LoginId { get; set; }
    }
}
