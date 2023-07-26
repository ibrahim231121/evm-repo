using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Corssbones.ALPR.Business.ExportDetail.Change
{
    public class ChangeExportDetail : RecIdItemMessage
    {
        public DTO.ExportDetailDTO ItemToUpdate{ get; set; }
        public ChangeExportDetail(RecId _id) : base(_id)
        {
        }
    }
}