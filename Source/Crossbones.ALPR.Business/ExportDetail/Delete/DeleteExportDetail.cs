using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.ExportDetail.Delete
{
    public class DeleteExportDetail : RecIdItemMessage
    {
        public DeleteExportDetail(RecId _id) : base(_id)
        {
        }
    }
}