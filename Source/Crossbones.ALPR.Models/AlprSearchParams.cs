namespace Crossbones.ALPR.Models
{
    public class AlprSearchParams
    {
        public string? NumberPlateId { get; set; } = null;
        public string? NumberPlate { get; set; } = null;
        public string? State { get; set; } = null;
        public DateTime? FromDate { get; set; } = null;
        public DateTime? ToDate { get; set; } = null;
        public string? UserId { get; set; } = null;
        public string? UnitId { get; set; } = null;
    }
}
