using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using E = Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Business.NumberPlates.Add
{
    public class AddNumberPlate : RecIdItemMessage
    {
        public AddNumberPlate(RecId id) : base(id)
        {
        }
        public E.NumberPlate Item { get; set; }

        public override string ToString()
        {
            return Item.LicensePlate;
        }

    }
}