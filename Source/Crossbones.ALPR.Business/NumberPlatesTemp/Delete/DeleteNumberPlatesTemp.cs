using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Delete
{
    public class DeleteNumberPlatesTemp: NumberPlatesTempMessage
    {
        public DeleteNumberPlatesTemp(SysSerial id): base(id) { }
    }
}
