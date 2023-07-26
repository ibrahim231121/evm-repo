using Crossbones.ALPR.Common.ValueObjects;
using Entities = Corssbones.ALPR.Database.Entities;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Add
{
    public class AddHotListNumberPlate : RecIdItemMessage
    {
        public AddHotListNumberPlate(RecId id) : base(id)
        {
        }

        public Entities.HotListNumberPlate Item { get; set; }

        public override string ToString()
        {
            return $"H_ID: {Item.HotListId} N_ID: {Item.NumberPlatesId}";
        }

    }
}