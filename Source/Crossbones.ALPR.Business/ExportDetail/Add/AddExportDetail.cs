using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.ExportDetail.Add
{
    public class AddExportDetail : RecIdItemMessage
    {
        public long CapturedPlateId { get; set; }
        public long TicketNumber { get; set; }
        public DateTime ExportedOn { get; set; }
        public string ExportPath { get; set; } = null!;
        public string? UriLocation { get; set; }
        public AddExportDetail(RecId recId) : base(recId)
        {
        }
    }
}