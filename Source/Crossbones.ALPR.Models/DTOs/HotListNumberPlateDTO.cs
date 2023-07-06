using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.DTOs
{
    public class HotListNumberPlateDTO
    {
        public long RecId { get; set; }
        [Required(ErrorMessage = "Hot List Id can not be null or less than 1")]
        public long HotListId { get; set; }
        [Required(ErrorMessage = "Hot List Id can not be null or less than 1")]
        public long NumberPlatesId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public byte[]? LastTimeStamp { get; set; } = null!;
        public virtual HotListDTO? HotList { get; set; }
        public virtual NumberPlateDTO? NumberPlate { get; set; }
    }
}