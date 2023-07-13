using System.ComponentModel.DataAnnotations;

namespace Corssbones.ALPR.Database.Entities;

public partial class State
{
    [Key]
    public byte RecId { get; set; }

    public string StateName { get; set; } = null!;

    public virtual ICollection<NumberPlate> NumberPlates { get; set; } = new List<NumberPlate>();

    public virtual ICollection<NumberPlateTemp> NumberPlatesTemps { get; set; } = new List<NumberPlateTemp>();
}
