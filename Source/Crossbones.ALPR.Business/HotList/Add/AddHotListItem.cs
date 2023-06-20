using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItem : RecIdItemMessage
    {
        public AddHotListItem(RecId id, HotListDTO itemToAdd) : base(id)
        {
            this.ItemToAdd = itemToAdd;
        }

        public HotListDTO ItemToAdd { get; }
    }
}
