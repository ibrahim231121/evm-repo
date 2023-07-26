using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryStatusDTO : IValidatableObject
    {
        public long SyncId { get; set; }

        [Range(typeof(DateTime), "January 1, 0001", "December 31, 9999", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? LastExecutionDate { get; set; }

        [Range(typeof(DateTime), "January 1, 0001", "December 31, 9999", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? LastExecutionEndDate { get; set; }

        public int? StatusId { get; set; }

        public string? StatusDesc { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (LastExecutionDate > LastExecutionEndDate)
            {
                yield return new ValidationResult("LastExecutionDate date time can not be greater than LastExecutionEndDate");
            }
        }
    }
}
