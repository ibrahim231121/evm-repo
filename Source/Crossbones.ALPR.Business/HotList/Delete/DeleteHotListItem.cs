using Corssbones.ALPR.Business.HotList;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.HotList.Delete
{
    public class DeleteHotListItem : HotListItemMessage
    {
        public DeleteHotListItem(SysSerial id) : base(id)
        {
        }
    }
}
