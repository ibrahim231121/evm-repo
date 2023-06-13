namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryItem
    {
        public long CapturePlateId { get; set; }

        public int StationId { get; set; }

        public int ClientId { get; set; }

        public string UnitId { get; set; } = null!;

        public long UserId { get; set; }

        public string LoginId { get; set; } = null!;

        public DateTime CaptureDate { get; set; }

        public bool HasAlert { get; set; }

        public bool HasTicket { get; set; }
    }
}
