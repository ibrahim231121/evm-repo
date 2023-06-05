
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotList.Change
{
    public class ChangeHotListItem : SysSerialItemMessage
    {

        public ChangeHotListItem(SysSerial id, HotListItem itemToUpdate) : base(id)
        {
            ItemToUpdate = itemToUpdate;
        }

        public HotListItem ItemToUpdate { get; }
    }
}
