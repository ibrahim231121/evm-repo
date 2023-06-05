using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Business.HotList.Add
{
    public class AddHotListItem : SysSerialItemMessage
    {
        public AddHotListItem(SysSerial id, HotListItem itemToAdd) : base(id)
        {
            this.ItemToAdd = itemToAdd;
        }

        public HotListItem ItemToAdd { get; }
    }
}
