using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class NumberPlateDTO
    {
        public long RecId { get; set; }
        public string? NCICNumber { get; set; }
        public string? AgencyId { get; set; }

        [Required(ErrorMessage = "Date Of Interest can not be null")]
        public string DateOfInterest { get; set; }

        [Required(ErrorMessage = "License Plate can not be null")]
        [StringLength(10, ErrorMessage = "License Plate lenght should be between 6 to 20 characters", MinimumLength = 6)]
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
        public bool NeedFullInsertion { get; set; }
    }
}