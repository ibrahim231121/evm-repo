using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.ExportDetail.Change
{
    public class ChangeExportDetail : ALPRExportDetailMessage
    {
        public long CapturedPlateId { get; set; }
        public long TicketNumber { get; set; }
        public DateTime ExportedOn { get; set; }
        public string ExportPath { get; set; } = null!;
        public string? UriLocation { get; set; }
        public ChangeExportDetail(SysSerial _id) : base(_id)
        {
        }
    }
}