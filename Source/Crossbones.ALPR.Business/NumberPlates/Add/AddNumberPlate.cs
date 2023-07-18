using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.NumberPlates.Add
{
    public class AddNumberPlate : RecIdItemMessage
    {
        public AddNumberPlate(RecId recId) : base(recId)
        {
        }

        public NumberPlateDTO NumberPlateDTO { get; set; }
    }
}