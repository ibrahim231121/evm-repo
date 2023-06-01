using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListItem : SysSerialItemMessage
    {
        public DeleteHotListItem(SysSerial id) : base(id)
        {
        }
    }
}
