using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.NumberPlates.Change
{
    public class ChangeNumberPlate : NumberPlatesMessage
    {
        public ChangeNumberPlate(SysSerial id) : base(id) { }
        public string ImportSerialId { get; set; }
        public string Ncicnumber { get; set; }
        public string AgencyId { get; set; }
        public DateTime DateOfInterest { get; set; }
        public string NumberPlate { get; set; }
        public string StateId { get; set; }
        public string LicenseYear { get; set; }
        public string LicenseType { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleStyle { get; set; }
        public string VehicleColor { get; set; }
        public string Note { get; set; }
        public short InsertType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte[] LastTimeStamp { get; set; } = null!;
        public short Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string ViolationInfo { get; set; }
        public string Notes { get; set; }
    }
}
