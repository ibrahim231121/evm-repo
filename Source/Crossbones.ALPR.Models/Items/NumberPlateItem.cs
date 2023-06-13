namespace Crossbones.ALPR.Models.Items
{
    public class NumberPlateItem
    {
        public long SysSerial { get; set; }
        public string? NCICNumber { get; set; }
        public string? AgencyId { get; set; }
        public string DateOfInterest { get; set; }
        public string LicensePlate { get; set; }
        public byte? StateId { get; set; }
        public string? LicenseYear { get; set; }
        public string? LicenseType { get; set; }
        public string? VehicleYear { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleStyle { get; set; }
        public string? VehicleColor { get; set; }
        public string? Note { get; set; }
        public short? InsertType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public short? Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Alias { get; set; }
        public string? ViolationInfo { get; set; }
        public string? Notes { get; set; }
        public string? ImportSerialId { get; set; }
        public string? HotList { get; set; }
        public string? StateName { get; set; }
    }
}