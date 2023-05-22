using Corssbones.ALPR.Business.HotList;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Delete
{
    public class DeleteHotListNumberPlate : HotListItemMessage
    {
        public DeleteHotListNumberPlate(SysSerial id) : base(id)
        {
        }
    }
}