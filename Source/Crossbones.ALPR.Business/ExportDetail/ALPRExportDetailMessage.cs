using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.ExportDetail
{
    public class ALPRExportDetailMessage : MessageBase
    {
        public SysSerial Id { get; }
        public ALPRExportDetailMessage(SysSerial _id) => Id = _id;
    }
}