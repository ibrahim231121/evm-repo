
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItem : RecIdItemMessage
    {

        public ChangeHotListItem(RecId id, HotListDTO itemToUpdate) : base(id)
        {
            ItemToUpdate = itemToUpdate;
        }

        public HotListDTO ItemToUpdate { get; }
    }
}
