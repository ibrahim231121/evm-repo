using System.ComponentModel.DataAnnotations;

namespace Crossbones.ALPR.Models.Items
{
    public class HotListNumberPlateItem
    {
        public long SysSerial { get; set; }
        [Required(ErrorMessage = "Hot List Id can not be null or less than 1")]
        public long HotListId { get; set; }
        [Required(ErrorMessage = "Hot List Id can not be null or less than 1")]
        public long NumberPlatesId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public byte[]? LastTimeStamp { get; set; } = null!;
        public virtual HotListItem? HotList { get; set; }
        public virtual NumberPlates? NumberPlate { get; set; }
    }
}