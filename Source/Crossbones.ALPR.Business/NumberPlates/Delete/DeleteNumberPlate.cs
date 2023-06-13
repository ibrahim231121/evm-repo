using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.NumberPlates.Delete
{
    public class DeleteNumberPlate : SysSerialItemMessage
    {
        public DeleteNumberPlate(SysSerial id) : base(id)
        {

        }
    }
}
