namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryStatusItem
    {
        public long SyncId { get; set; }

        public DateTime? LastExecutionDate { get; set; }

        public DateTime? LastExecutionEndDate { get; set; }

        public int? StatusId { get; set; }

        public string? StatusDesc { get; set; }
    }
}
