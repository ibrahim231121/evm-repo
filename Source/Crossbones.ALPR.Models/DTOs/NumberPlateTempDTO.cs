using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class NumberPlateTempDTO
    {
        public long RecId { get; set; }
        public string? NCICNumber { get; set; }
        public string? AgencyId { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime DateOfInterest { get; set; }

        [StringLength(20, ErrorMessage = "License Plate lenght should be between 2 to 20 characters", MinimumLength = 2)]
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

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? CreatedOn { get; set; }
        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? LastUpdatedOn { get; set; }

        public short? Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Alias { get; set; }
        public string? ViolationInfo { get; set; }
        public string? Notes { get; set; }
        public string? ImportSerialId { get; set; }

        //Addtional Properties

        public string? HotList { get; set; }
        public string? StateName { get; set; }
    }
}
