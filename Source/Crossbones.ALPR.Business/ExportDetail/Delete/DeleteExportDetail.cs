using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.ExportDetail.Delete
{
    public class DeleteExportDetail : SysSerialItemMessage
    {
        public DeleteExportDetail(SysSerial _id) : base(_id)
        {
        }
    }
}