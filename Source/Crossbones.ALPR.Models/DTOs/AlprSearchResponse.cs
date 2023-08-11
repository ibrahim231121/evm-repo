namespace Crossbones.ALPR.Models.DTOs
{
    public class AlprSearchResponse
    {
        public string? CapturedPlateId { get; set; }
        public string? NumberPlateId { get; set; }
        public string? NumberPlate { get; set; }
        public string? HotlistName { get; set; }
        public DateTime? CapturedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? UnitId { get; set; }
        public string? User { get; set; }
        public string? Description { get; set; }
        public string? Confidence { get; set; }
        public string? StateName { get; set; }
        public string? Notes { get; set; }
        public string? TicketNumber { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Distance { get; set; }
        public string? LifeSpan { get; set; }
        public string? LoginId { get; set; }
        public string? NCICNumber { get; set; }
        public string? AgencyId { get; set; }
        public DateTime? DateOfInterest { get; set; }
        public string? CapturePlate { get; set; }
        public string? StateId { get; set; }
        public string? LicenseYear { get; set; }
        public string? LicenseType { get; set; }
        public string? VehicleYear { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleStyle { get; set; }
        public string? VehicleColor { get; set; }
        public string? Note { get; set; }
        public string? InsertType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? Status { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Alias { get; set; }
        public string? ViolationInfo { get; set; }
        public string? ImportSerialId { get; set; }
    }
}