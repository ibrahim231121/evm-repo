using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Business.NumberPlates.Change
{
    public class ChangeNumberPlate : RecIdItemMessage
    {
        public ChangeNumberPlate(RecId recId) : base(recId) { }
        public NumberPlateDTO NumberPlateDTO { get; set; }
    }
}