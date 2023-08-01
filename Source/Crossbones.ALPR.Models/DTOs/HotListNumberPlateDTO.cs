using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListNumberPlateDTO
    {
        public long RecId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "HotList Id should be greater than 0")]
        public long HotListId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Number Plate Id should be greater than 0")]
        public long NumberPlateId { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? CreatedOn { get; set; }

        [Range(typeof(DateTime), "January 1, 2010", "December 31, 2030", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? LastUpdatedOn { get; set; }
        public byte[]? LastTimeStamp { get; set; } = null!;
        public virtual HotListDTO? HotList { get; set; }
        public virtual NumberPlateDTO? NumberPlate { get; set; }
    }
}